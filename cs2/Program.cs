using cs2.Game.Objects;
using cs2.GameOverlay;
using cs2.Offsets;

namespace cs2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log("init...");
            OffsetsLoader.Initialize();
            if (!Memory.Initialize())
            {
                Log("game not found", ConsoleColor.Red);
                Console.ReadKey();
                return;
            }
            LocalPlayer = new LocalPlayer();
            new Overlay();
        }

        public static void Log(string text, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
        }

        public static LocalPlayer LocalPlayer
        {
            get; private set;
        } = null!;

        public static List<Entity> Entities
        {
            get; set;
        } = new List<Entity>();
    }
}
