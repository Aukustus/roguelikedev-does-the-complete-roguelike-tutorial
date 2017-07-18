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
                    if (FoV.InFov(Rogue.GameWorld.Player.X, Rogue.GameWorld.Player.Y, x, y, Rogue.GameWorld.Player))
                    {
                        Terminal.Color(Terminal.ColorFromName("white"));
                        if (Rogue.GameWorld.Map.Tiles[x, y].Blocked)
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
                        if (Rogue.GameWorld.Map.Tiles[x, y].Blocked)
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
            Terminal.Layer(Constants.Layers["Map"]);

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
                if (FoV.InFov(Rogue.GameWorld.Player.X, Rogue.GameWorld.Player.Y, obj.X, obj.Y, Rogue.GameWorld.Player))
                {
                    obj.Draw("white");
                }
                else if (Rogue.GameWorld.Map.Tiles[obj.X, obj.Y].Explored && obj.AlwaysVisible && Camera.WithinCamera(obj.X, obj.Y))
                {
                    obj.Draw("grey");
                }
            }

            Rogue.GameWorld.Player.Draw("white");
        }

        public static void RenderAll()
        {
            Terminal.Clear();

            DrawMap();

            DrawObjects();

            UI.DrawUI();

            Terminal.Refresh();
        }
    }
}
