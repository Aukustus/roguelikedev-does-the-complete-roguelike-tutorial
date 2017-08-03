using BearLib;
using RogueTutorial;

namespace CSharpRogueTutorial
{
    class UI
    {
        private static void DrawBorders()
        {
            Terminal.Layer(Constants.Layers["UI"]);

            Terminal.Put(0, 0, Constants.Symbols.NW);

            for (int x = 1; x < 79; x++)
            {
                Terminal.Put(x, 0, Constants.Symbols.HorizonalBar);
                Terminal.Put(x, Constants.ScreenHeight - 1, Constants.Symbols.HorizonalBar);
            }

            Terminal.Put(61, 0, Constants.Symbols.DownCross);
            Terminal.Put(79, 0, Constants.Symbols.NE);

            for (int y = 1; y < Constants.ScreenHeight - 1; y++)
            {
                Terminal.Put(0, y, Constants.Symbols.VerticalBar);
                Terminal.Put(61, y, Constants.Symbols.VerticalBar);
                Terminal.Put(79, y, Constants.Symbols.VerticalBar);
            }

            Terminal.Put(0, Constants.ScreenHeight - 1, Constants.Symbols.SW);
            Terminal.Put(61, Constants.ScreenHeight - 1, Constants.Symbols.UpCross);

            Terminal.Put(79, Constants.ScreenHeight - 1, Constants.Symbols.SE);

            Terminal.Put(61, Constants.ScreenHeight - 8, Constants.Symbols.LeftCross);
            Terminal.Put(0, Constants.ScreenHeight - 8, Constants.Symbols.RightCross);

            for (int x = 1; x < 61; x++)
            {
                Terminal.Put(x, Constants.ScreenHeight - 8, Constants.Symbols.HorizonalBar);
            }

            for (int x = 1; x < 5; x++)
            {
                Terminal.Put(x, Constants.ScreenHeight - 1, Constants.Symbols.HorizonalBar);
            }

            for (int x = 66; x < 79; x++)
            {
                Terminal.Put(x, Constants.ScreenHeight - 1, Constants.Symbols.HorizonalBar);
            }

            for (int x = Constants.ScreenWidth - 18; x < Constants.ScreenWidth - 1; x++)
            {
                for (int y = 1; y < Constants.ScreenHeight - 1; y++)
                {
                    Terminal.Put(x, y, Constants.Symbols.Empty);
                }
            }

            for (int x = 1; x < 61; x++)
            {
                for (int y = Constants.ScreenHeight - 7; y < Constants.ScreenHeight - 1; y++)
                {
                    Terminal.Put(x, y, Constants.Symbols.Empty);
                }
            }
        }

        private static void DrawStats()
        {
            Terminal.Layer(Constants.Layers["Messages"]);

            Terminal.Print(64, 1, "Health: " + Rogue.GameWorld.Player.Fighter.HP + "/" + Rogue.GameWorld.Player.Fighter.Max_HP);
            Terminal.Print(64, 2, "Attack: " + Rogue.GameWorld.Player.Fighter.Attack);
            Terminal.Print(64, 3, "Defense: " + Rogue.GameWorld.Player.Fighter.Defense);
        }

        private static void DrawMessages()
        {
            Terminal.Layer(Constants.Layers["Messages"]);

            int y = 38;

            foreach (Message message in Rogue.GameWorld.MessageLog.Messages)
            {
                Terminal.Color(Terminal.ColorFromName(message.Color));

                Terminal.Print(1, y, message.Text);

                y += 1;
            }

            Terminal.Color(Terminal.ColorFromName("white"));
        }

        private static void MouseHoverLook()
        {
            Terminal.Layer(Constants.Layers["UI"]);

            int mouseX = Terminal.State(Terminal.TK_MOUSE_X) - 1;
            int mouseY = Terminal.State(Terminal.TK_MOUSE_Y) - 1;

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
                        if ((GameMap.MapExplored(obj.X, obj.Y) && obj.AlwaysVisible) || FoV.InFov(Rogue.GameWorld.Player.X, Rogue.GameWorld.Player.Y, obj.X, obj.Y, Rogue.GameWorld.Player))
                        {
                            Terminal.Print(mouseX + 1, mouseY + 1, obj.Name);
                        }
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
