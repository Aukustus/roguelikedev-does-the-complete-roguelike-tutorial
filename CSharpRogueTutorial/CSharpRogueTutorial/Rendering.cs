using BearLib;
using RogueTutorial;

namespace CSharpRogueTutorial
{
    class Rendering
    {
        private static void DrawMap()
        {
            FoV.RayCast();

            for (int x = 0 + Rogue.GameWorld.Player.CameraX; x < Constants.CameraWidth + Rogue.GameWorld.Player.CameraX + 1; x++)
            {
                for (int y = 0 + Rogue.GameWorld.Player.CameraY; y < Constants.CameraHeight + Rogue.GameWorld.Player.CameraY + 1; y++)
                {
                    int drawX = (x - Rogue.GameWorld.Player.CameraX) * 2 + 1;
                    int drawY = (y - Rogue.GameWorld.Player.CameraY) * 2 + 1;

                    if (FoV.InFov(Rogue.GameWorld.Player.x, Rogue.GameWorld.Player.y, x, y))
                    {
                        Terminal.Color(Terminal.ColorFromName("white"));
                        if (Rogue.GameWorld.Map.tiles[x, y].blocked)
                        {
                            DrawMapTile(x, y, Constants.Tiles.WallTile, "white");
                        }
                        else
                        {
                            DrawMapTile(x, y, Constants.Tiles.FloorTile, "white");
                        }
                    }
                    else if (GameMap.MapExplored(x, y))
                    {
                        if (Rogue.GameWorld.Map.tiles[x, y].blocked)
                        {
                            DrawMapTile(x, y, Constants.Tiles.WallTile, "grey");
                        }
                        else
                        {
                            DrawMapTile(x, y, Constants.Tiles.FloorTile, "grey");
                        }
                    }
                }
            }
        }

        private static void DrawMapTile(int x, int y, char tile, string color)
        {
            Terminal.Layer(2);

            int drawX = (x - Rogue.GameWorld.Player.CameraX) * 2 + 1;
            int drawY = (y - Rogue.GameWorld.Player.CameraY) * 2 + 1;

            Terminal.Color(Terminal.ColorFromName(color));

            Terminal.PutExt(drawX, drawY, 8, 8, tile);
            Terminal.Color(Terminal.ColorFromName("white"));
        }

        private static void DrawObjects()
        {
            foreach (GameObject obj in Rogue.GameWorld.Objects)
            {
                if (FoV.InFov(Rogue.GameWorld.Player.x, Rogue.GameWorld.Player.y, obj.x, obj.y))
                {
                    obj.Draw();
                }
            }

            Rogue.GameWorld.Player.Draw();
        }

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

        public static void RenderAll()
        {
            Terminal.Clear();

            DrawBorders();

            DrawMap();

            DrawObjects();

            Terminal.Refresh();
        }
    }
}
