#define fromDir

using cs2.Game.Features;
using cs2.Game.Objects;
using cs2.GameOverlay;
using cs2.Offsets;

//todo: overlay_ui, tb, radar
//wh: FLASHES, RELOADING, SCOPED, AMMO, NICKNAMES
//wh: aimdir on key?
//world esp: nades, weapons
//sound esp
//dmg
//bhop
//hit sound

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

            Input.Initialize();
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
            AimAssist.Start();
            new Overlay();
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

    }
}
