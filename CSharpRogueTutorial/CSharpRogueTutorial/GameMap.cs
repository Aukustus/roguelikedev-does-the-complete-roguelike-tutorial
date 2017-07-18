using RogueTutorial;
using System;

namespace CSharpRogueTutorial
{
    [Serializable]
    class Tile
    {
        public bool Blocked;
        public bool Explored;
        public bool Visited;

        public Tile(bool blocked)
        {
            this.Blocked = blocked;
            Explored = false;
            Visited = false;
        }
    }

    [Serializable]
    class GameMap
    {
        public Tile[,] Tiles;

        public GameMap(bool solid)
        {
            Tiles = BlankTiles(solid);
        }

        private static Tile[,] BlankTiles(bool blocked)
        {
            Tile[,] map = new Tile[Constants.MapWidth, Constants.MapHeight];

            for (int x = 0; x < Constants.MapWidth; x++)
            {
                for (int y = 0; y < Constants.MapHeight; y++)
                {
                    map[x, y] = new Tile(blocked);
                }
            }

            return map;
        }

        internal void CreateHorizontalTunnel(int x1, int x2, int y)
        {
            for (int x = Math.Min(x1, x2); x < Math.Max(x1, x2) + 1; x++)
            {
                Tiles[x, y].Blocked = false;
            }
        }

        internal void CreateVerticalTunnel(int y1, int y2, int x)
        {
            for (int y = Math.Min(y1, y2); y < Math.Max(y1, y2) + 1; y++)
            {
                Tiles[x, y].Blocked = false;
            }
        }

        public static bool Blocked(int x, int y)
        {
            if (x < 0 || y < 0 && x >= Constants.MapWidth || y >= Constants.MapHeight)
            {
                return true;
            }

            foreach (GameObject obj in Rogue.GameWorld.Objects)
            {
                if (obj.X == x && obj.Y == y && obj.Blocks)
                {
                    return true;
                }
            }

            return Rogue.GameWorld.Map.Tiles[x, y].Blocked;
        }

        public static bool MapBlocked(int x, int y)
        {
            if (x < 0 || y < 0 && x >= Constants.MapWidth || y >= Constants.MapHeight)
            {
                return true;
            }

            return Rogue.GameWorld.Map.Tiles[x, y].Blocked;
        }

        public static bool MapExplored(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Constants.MapWidth || y >= Constants.MapHeight)
            {
                return false;
            }

            return Rogue.GameWorld.Map.Tiles[x, y].Explored;
        }
    }
}
