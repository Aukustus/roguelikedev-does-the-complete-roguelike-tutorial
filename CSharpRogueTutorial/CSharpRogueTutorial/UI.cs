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
            {
                Terminal.Put(x, 0, Constants.Symbols.HorizonalBar);
                Terminal.Put(x, Constants.ScreenHeight - 1, Constants.Symbols.HorizonalBar);
            }

            for (int x = 5; x < 66; x++)
            {
                Terminal.Put(x, 4, Constants.Symbols.HorizonalBar);
            }

            Terminal.Put(65, 0, Constants.Symbols.DownCross);
            Terminal.Put(4, 0, Constants.Symbols.DownCross);
            Terminal.Put(79, 0, Constants.Symbols.NE);

            for (int y = 1; y < Constants.ScreenHeight - 1; y++)
            {
                Terminal.Put(0, y, Constants.Symbols.VerticalBar);
                Terminal.Put(4, y, Constants.Symbols.VerticalBar);
                Terminal.Put(65, y, Constants.Symbols.VerticalBar);
                Terminal.Put(79, y, Constants.Symbols.VerticalBar);
            }

            Terminal.Put(0, Constants.ScreenHeight - 1, Constants.Symbols.SW);
            Terminal.Put(65, Constants.ScreenHeight - 1, Constants.Symbols.UpCross);

            Terminal.Put(79, Constants.ScreenHeight - 1, Constants.Symbols.SE);

            Terminal.Put(65, Constants.ScreenHeight - 9, Constants.Symbols.LeftCross);

            for (int x = 5; x < 65; x++)
            {
                Terminal.Put(x, Constants.ScreenHeight - 9, Constants.Symbols.HorizonalBar);
            }

            for (int x = 1; x < 5; x++)
            {
                Terminal.Put(x, Constants.ScreenHeight - 1, Constants.Symbols.HorizonalBar);
            }

            for (int x = 66; x < 79; x++)
            {
                Terminal.Put(x, Constants.ScreenHeight - 1, Constants.Symbols.HorizonalBar);
            }

            Terminal.Put(4, Constants.ScreenHeight - 1, Constants.Symbols.UpCross);
            Terminal.Put(4, Constants.ScreenHeight - 9, Constants.Symbols.RightCross);
            Terminal.Put(4, 4, Constants.Symbols.RightCross);
            Terminal.Put(65, 4, Constants.Symbols.LeftCross);

            for (int x = 1; x < 4; x++)
            {
                for (int y = 1; y < Constants.ScreenHeight - 1; y++)
                {
                    Terminal.Put(x, y, Constants.Symbols.Empty);
                }
            }

            for (int x = Constants.ScreenWidth - 14; x < Constants.ScreenWidth - 1; x++)
            {
                for (int y = 1; y < Constants.ScreenHeight - 1; y++)
                {
                    Terminal.Put(x, y, Constants.Symbols.Empty);
                }
            }

            for (int x = 5; x < 65; x++)
            {
                for (int y = 1; y < 4; y++)
                {
                    Terminal.Put(x, y, Constants.Symbols.Empty);
                }
            }

            for (int x = 5; x < 65; x++)
            {
                for (int y = Constants.ScreenHeight - 8; y < Constants.ScreenHeight - 1; y++)
                {
                    Terminal.Put(x, y, Constants.Symbols.Empty);
                }
            }
        }

        private static void DrawStats()
        {
            Terminal.Print(66, 1, "Health: " + Rogue.GameWorld.Player.Fighter.HP + "/" + Rogue.GameWorld.Player.Fighter.Max_HP);
            Terminal.Print(66, 2, "Attack: " + Rogue.GameWorld.Player.Fighter.Attack);
            Terminal.Print(66, 3, "Defense: " + Rogue.GameWorld.Player.Fighter.Defense);
        }

        private static void DrawMessages()
        {
            int y = 34;

            foreach (Message message in Rogue.GameWorld.MessageLog.Messages)
            {
                Terminal.Color(Terminal.ColorFromName(message.Color));

                Terminal.Print(5, y, message.Text);

                y += 1;
            }

            Terminal.Color(Terminal.ColorFromName("white"));
        }

        private static void MouseHoverLook()
        {
            int mouseX = Terminal.State(Terminal.TK_MOUSE_X) - 1;
            int mouseY = Terminal.State(Terminal.TK_MOUSE_Y) - 5;

            if (mouseX >= 0 && mouseY >= 0 && mouseX <= 61 && mouseY <= 25)
            {
                if (mouseX % 2 != 0)
                {
                    mouseX -= 1;
                }

                if (mouseY % 2 != 0)
                {
                    mouseY -= 1;
                }

                Coordinate coord = Camera.CameraToCoordinate(mouseX / 4, mouseY / 4);

                foreach (GameObject obj in Rogue.GameWorld.Objects)
                {
                    if (obj.X == coord.X && obj.Y == coord.Y)
                    {
                        Terminal.Print(mouseX + 1, mouseY + 4, obj.Name);
                    }
                }
            }
        }

        public static void DrawUI()
        {
            DrawBorders();
            DrawStats();
            DrawMessages();
            MouseHoverLook();
        }
    }
}
