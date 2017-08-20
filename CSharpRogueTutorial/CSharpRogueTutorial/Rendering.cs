using BearLib;
using RogueTutorial;

namespace CSharpRogueTutorial
{
    class Rendering
    {
        private static void DrawMap()
        {
            FoV.RayCast();

            for (int x = Rogue.GameWorld.Player.CameraX - 1; x < Constants.CameraWidth + Rogue.GameWorld.Player.CameraX + 1 + 1; x++)
            {
                for (int y = Rogue.GameWorld.Player.CameraY - 1; y < Constants.CameraHeight + Rogue.GameWorld.Player.CameraY + 1 + 1; y++)
                {
                    if (FoV.InFov(Rogue.GameWorld.Player.X, Rogue.GameWorld.Player.Y, x, y, Rogue.GameWorld.Player))
                    {
                        if (GameMap.IsTerrain(x, y, Constants.Terrain.TileWall))
                        {
                            DrawMapTile(x, y, Tiles.Terrain.WallTile, "white");
                        }
                        else if (GameMap.IsTerrain(x, y, Constants.Terrain.TileFloor))
                        {
                            DrawMapTile(x, y, Tiles.Terrain.FloorTile, "white");
                        }
                    }
                    else if (GameMap.MapExplored(x, y))
                    {
                        if (GameMap.IsTerrain(x, y, Constants.Terrain.TileWall))
                        {
                            DrawMapTile(x, y, Tiles.Terrain.WallTile, "grey");
                        }
                        else if (GameMap.IsTerrain(x, y, Constants.Terrain.TileFloor))
                        {
                            DrawMapTile(x, y, Tiles.Terrain.FloorTile, "grey");
                        }
                    }
                }
            }
        }

        private static void DrawMapTile(int x, int y, int tile, string color)
        {
            Terminal.Layer(Constants.Layers["Map"]);

            int drawX = (x - Rogue.GameWorld.Player.CameraX) * 4 + 5;
            int drawY = (y - Rogue.GameWorld.Player.CameraY) * 4 + 5;

            Terminal.Color(Terminal.ColorFromName(color));

            int offsetX = -40 - Rogue.GameWorld.Player.OffsetX;
            int offsetY = -40 - Rogue.GameWorld.Player.OffsetY;

            Terminal.PutExt(drawX, drawY, offsetX, offsetY, tile);
        }

        private static void DrawObjects(GameObject skip)
        {
            foreach (GameObject obj in Rogue.GameWorld.Objects)
            {
                if (skip != null && obj == skip)
                {
                    continue;
                }
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

        public static void RenderAll(GameObject skip = null)
        {
            Terminal.Clear();

            UI.DrawUI();

            DrawMap();

            DrawObjects(skip);

            if (skip == null)
            {
                Terminal.Refresh();
            }
        }
    }
}
