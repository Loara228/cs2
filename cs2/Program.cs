using cs2.Game.Features;
using cs2.Game.Objects;
using cs2.GameOverlay;
using cs2.Offsets;
using cs2.PInvoke;
using System.Numerics;
using System;
using System.Runtime.InteropServices;

//todo:
//wh: aimdir on key (line steps)?
//sound esp
//config and offsets UI
//dmg
//hit sound
//

//wh toggle
//conf form
//controls: rowContainer, tip, msgBox
//offsets loader from dir. (ui)
// CCollisionProperty
// C_Inferno - molotov

namespace cs2
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Input.Initialize();
            LocalPlayer.Initialize();
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
            WorldEsp.Start();
            new Thread(() =>
            {
                Program.Log($"success", ConsoleColor.Green);
                for (int i = 3; i > 0; i--)
                {
                    Program.Log(i);
                    Thread.Sleep(1000);
                }
                Console.Clear();
            }).Start();
            new Overlay(); // thr join
        }

        public static void Log(string text, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void Log(int integer, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(integer.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static List<Entity> Entities
        {
            get; set;
        } = new List<Entity>();

        public static int ENTITY_LIST_COUNT = 32;
    }
}
