#define fromDir

using cs2.Game.Features;
using cs2.Game.Objects;
using cs2.GameOverlay;
using cs2.Offsets;
using System.Data;


namespace cs2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("-fromDir"))
                OffsetsLoader.type = LoadType.FROM_DIR;
            else
                OffsetsLoader.type = LoadType.FROM_GIT;

            if (!OffsetsLoader.Initialize())
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
            ReadKeyThr();
            new Overlay();
        }

        private static void ReadKeyThr()
        {
            new Thread(() =>
            {
                while(true)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.D1)
                    {
                        int i = 0;
                        foreach(var entity in Entities)
                        {
                            Console.WriteLine($"{i} | {entity.Nickname}. wins: {Memory.Read<int>(entity.ControllerBase + OffsetsLoader.CCSPlayerController.m_iCompetitiveWins)}. Predicted_Win: {Memory.Read<int>(entity.ControllerBase + OffsetsLoader.CCSPlayerController.m_iCompetitiveRankingPredicted_Win)}");
                        }
                    }
                    else
                    {
                        Log("Unknown command");
                    }
                }
            }).Start();
        }

        public static void Log(string text, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static LocalPlayer LocalPlayer
        {
            get; private set;
        } = new LocalPlayer();

        public static List<Entity> Entities
        {
            get; set;
        } = new List<Entity>();

        public static int ScreenW
        {
            get; set;
        } = 1920;

        public static int ScreenH
        {
            get; set;
        } = 1080;
    }
}
