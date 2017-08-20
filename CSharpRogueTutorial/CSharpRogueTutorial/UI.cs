using BearLib;
using RogueTutorial;

namespace CSharpRogueTutorial
{
    class UI
    {
        private static void DrawBorders()
        {
            Terminal.Layer(Constants.Layers["UI"]);

            Terminal.Put(0, 0, Tiles.Symbols.NW);

            for (int x = 1; x < 79; x++)
            {
                Terminal.Put(x, 0, Tiles.Symbols.HorizonalBar);
                Terminal.Put(x, Constants.ScreenHeight - 1, Tiles.Symbols.HorizonalBar);
            }

            Terminal.Put(61, 0, Tiles.Symbols.DownCross);
            Terminal.Put(79, 0, Tiles.Symbols.NE);

            for (int y = 1; y < Constants.ScreenHeight - 1; y++)
            {
                Terminal.Put(0, y, Tiles.Symbols.VerticalBar);
                Terminal.Put(61, y, Tiles.Symbols.VerticalBar);
                Terminal.Put(79, y, Tiles.Symbols.VerticalBar);
            }

            Terminal.Put(0, Constants.ScreenHeight - 1, Tiles.Symbols.SW);
            Terminal.Put(61, Constants.ScreenHeight - 1, Tiles.Symbols.UpCross);

            Terminal.Put(79, Constants.ScreenHeight - 1, Tiles.Symbols.SE);

            Terminal.Put(61, Constants.ScreenHeight - 8, Tiles.Symbols.LeftCross);
            Terminal.Put(0, Constants.ScreenHeight - 8, Tiles.Symbols.RightCross);

            for (int x = 1; x < 61; x++)
            {
                Terminal.Put(x, Constants.ScreenHeight - 8, Tiles.Symbols.HorizonalBar);
            }

            for (int x = 1; x < 5; x++)
            {
                Terminal.Put(x, Constants.ScreenHeight - 1, Tiles.Symbols.HorizonalBar);
            }

            for (int x = 66; x < 79; x++)
            {
                Terminal.Put(x, Constants.ScreenHeight - 1, Tiles.Symbols.HorizonalBar);
            }

            for (int x = Constants.ScreenWidth - 18; x < Constants.ScreenWidth - 1; x++)
            {
                for (int y = 1; y < Constants.ScreenHeight - 1; y++)
                {
                    Terminal.Put(x, y, Tiles.Symbols.Empty);
                }
            }

            for (int x = 1; x < 61; x++)
            {
                for (int y = Constants.ScreenHeight - 7; y < Constants.ScreenHeight - 1; y++)
                {
                    Terminal.Put(x, y, Tiles.Symbols.Empty);
                }
            }

            Terminal.Put(64, 0, Tiles.Symbols.LeftEnd);
            Terminal.Put(76, 0, Tiles.Symbols.LeftEnd);
            Terminal.Print(65, 0, "Information");
        }

        private static void DrawStats()
        {
            Terminal.Color(Terminal.ColorFromName("white"));

            Terminal.Layer(Constants.Layers["Messages"]);

            Terminal.Print(63, 2, "Health: " + Rogue.GameWorld.Player.Fighter.HP + "/" + Rogue.GameWorld.Player.Fighter.Max_HP);
            Terminal.Print(63, 3, "Attack: " + Rogue.GameWorld.Player.Fighter.Attack);
            Terminal.Print(63, 4, "Armor:  " + Rogue.GameWorld.Player.Fighter.Defense);
            Terminal.Print(63, 5, "Level:  " + Rogue.GameWorld.Player.Fighter.Level);
            Terminal.Print(63, 6, "XP:     " + Rogue.GameWorld.Player.Fighter.XP + "/" + Constants.LevelProgression[Rogue.GameWorld.Player.Fighter.Level - 1]);
            Terminal.Print(63, 7, "Floor:  " + Rogue.GameWorld.DungeonLevel);
        }

        private static void PrintMessages()
        {
            Terminal.Layer(Constants.Layers["Messages"]);

            int y = 38;

            foreach (Message message in Rogue.GameWorld.MessageLog.Messages)
            {
                Terminal.Color(Terminal.ColorFromName(message.Color));

                Terminal.Print(1, y, message.Text);

                y += 1;
            }
        }

        private static void MouseHoverLook()
        {
            Terminal.Color(Terminal.ColorFromName("white"));

            Terminal.Layer(Constants.Layers["Messages"]);

            int mouseX = Terminal.State(Terminal.TK_MOUSE_X) - 1;
            int mouseY = Terminal.State(Terminal.TK_MOUSE_Y) - 1;

            if (mouseX >= 0 && mouseY >= 0 && mouseX <= 60 && mouseY <= 34)
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
            PrintMessages();
            MouseHoverLook();
        }
    }
}
