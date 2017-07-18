using BearLib;
using RogueTutorial;

namespace CSharpRogueTutorial
{
    class UI
    {
        private static void DrawBorders()
        {
            Terminal.Put(0, 0, Constants.Symbols.NW);

            for (int x = 1; x < 79; x++)
                Terminal.Put(x, 0, Constants.Symbols.HorizonalBar);

            Terminal.Put(63, 0, Constants.Symbols.DownCross);
            Terminal.Put(79, 0, Constants.Symbols.NE);

            for (int y = 1; y < Constants.ScreenHeight - 1; y++)
            {
                Terminal.Put(0, y, Constants.Symbols.VerticalBar);
                Terminal.Put(79, y, Constants.Symbols.VerticalBar);
            }

            Terminal.Put(0, Constants.ScreenHeight - 1, Constants.Symbols.SW);
            Terminal.Put(63, Constants.ScreenHeight - 1, Constants.Symbols.UpCross);
            Terminal.Put(79, Constants.ScreenHeight - 1, Constants.Symbols.SE);

            for (int y = 1; y < Constants.ScreenHeight - 1; y++)
                Terminal.Put(63, y, Constants.Symbols.VerticalBar);

            Terminal.Put(0, Constants.ScreenHeight - 9, Constants.Symbols.RightCross);
            Terminal.Put(63, Constants.ScreenHeight - 9, Constants.Symbols.LeftCross);

            for (int x = 1; x < 63; x++)
            {
                Terminal.Put(x, Constants.ScreenHeight - 9, Constants.Symbols.HorizonalBar);
                Terminal.Put(x, Constants.ScreenHeight - 1, Constants.Symbols.HorizonalBar);
            }

            for (int x = 64; x < 79; x++)
                Terminal.Put(x, Constants.ScreenHeight - 1, Constants.Symbols.HorizonalBar);
        }

        private static void DrawStats()
        {
            Terminal.Print(65, 1, "Health: " + Rogue.GameWorld.Player.Fighter.HP + "/" + Rogue.GameWorld.Player.Fighter.Max_HP);
            Terminal.Print(65, 2, "Attack: " + Rogue.GameWorld.Player.Fighter.Attack);
            Terminal.Print(65, 3, "Defense: " + Rogue.GameWorld.Player.Fighter.Defense);
        }

        private static void DrawMessages()
        {
            int y = 28;

            foreach (string message in Rogue.GameWorld.MessageLog.Messages)
            {
                Terminal.Print(1, y, message);
                y += 1;
            }
        }

        public static void DrawUI()
        {
            DrawBorders();
            DrawStats();
            DrawMessages();
        }
    }
}
