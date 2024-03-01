using cs2.Game.Features;
using cs2.Game.Objects;
using cs2.GameOverlay;
using cs2.Offsets;
using cs2.PInvoke;
using System.Numerics;
using System;
using System.Runtime.InteropServices;
using SharpDX.XAudio2;
using System.Text;

#region notes
// controls: rowContainer, tip, msgBox
// CCollisionProperty
// C_Inferno - molotov
#endregion

namespace cs2
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Initialize();
            ParseArguments(args);
            HitMarker.Initialize();
            Input.Initialize();
            LocalPlayer.Initialize();
            HitMarker.Initialize();
            if (!OffsetsLoader.Initialize(LoadType.FROM_GIT))
            {
                Log("offsets init failed", ConsoleColor.Red);
                Console.ReadKey();
                Environment.Exit(0);
            }
            if (!Memory.Initialize())
            {
                Log("memory init failed", ConsoleColor.Red);
                Console.ReadKey();
                Environment.Exit(0);
            }
            AimAssist.Start();
            Bhop.Start();
            WorldEsp.Start();
            new Thread(() =>
            {
                Console.Beep();
                Thread.Sleep(1000);
                for (int i = 3; i > 0; i--)
                {
                    Program.Log(i);
                    Thread.Sleep(1000);
                }
                Console.Clear();
            }).Start();
            new Overlay(); // thr join
        }

        static void Initialize()
        {
            Console.Title = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Replace("0", "").Replace("L", "");
            string currentPath = Directory.GetCurrentDirectory();
            string soundsPath = Path.Combine(currentPath, "sounds");
            string offsetsPath = Path.Combine(currentPath, "generated");
            string configsPath = Path.Combine(currentPath, "configs");

            void Dir(string path)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    Log(path, ConsoleColor.Green);
                }
            }

            Dir(soundsPath);
            Dir(offsetsPath);
            Dir(configsPath);
        }

        static void ParseArguments(string[] args)
        {
            List<string> arguments = new List<string>();
            foreach (var arg in args)
                arguments.Add(arg.ToLower());

            int index = 0;
            if ((index = arguments.IndexOf("angle")) != -1)
            {
                AimAssist.AnglePerPixel = double.Parse(arguments[index + 1]);
                Program.Log($"AnglePerPixel: {AimAssist.AnglePerPixel}", ConsoleColor.Green);
            }
        }

        public static void Log(string text, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void Log(int integer, ConsoleColor color = ConsoleColor.Gray) => Log(integer.ToString(), color);

        public static List<Entity> Entities
        {
            get; set;
        } = new List<Entity>();

        public static readonly int ENTITY_LIST_COUNT = 32;
    }
}
