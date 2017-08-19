using RogueTutorial;
using System;

namespace CSharpRogueTutorial
{
    [Serializable]
    class Tile
    {
        public bool Blocked;
        public bool BlocksSight;
        public bool Explored;
        public bool Visited;
        public Constants.Terrain Terrain;

        public Tile(bool blocked, bool blocksSight)
        {
            Blocked = blocked;
            BlocksSight = blocksSight;
            Explored = false;
            Visited = false;
        }
    }

    [Serializable]
    class GameMap
    {
        public Tile[,] Tiles;

        public GameMap()
        {
            Tiles = BlankTiles();
        }

        private static Tile[,] BlankTiles()
        {
            Tile[,] map = new Tile[Constants.MapWidth, Constants.MapHeight];

            for (int x = 0; x < Constants.MapWidth; x++)
            {
                for (int y = 0; y < Constants.MapHeight; y++)
                {
                    map[x, y] = new Tile(true, true);
                }
            }

            return map;
        }

        internal void CreateHorizontalTunnel(int x1, int x2, int y)
        {
            for (int x = Math.Min(x1, x2); x < Math.Max(x1, x2) + 1; x++)
            {
                Tiles[x, y].Blocked = false;
                Tiles[x, y].BlocksSight = false;
            }
        }

        internal void CreateVerticalTunnel(int y1, int y2, int x)
        {
            for (int y = Math.Min(y1, y2); y < Math.Max(y1, y2) + 1; y++)
            {
                Tiles[x, y].Blocked = false;
                Tiles[x, y].BlocksSight = false;
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

        public static bool IsTerrain(int x, int y, Constants.Terrain type)
        {
            if (x < 0 || y < 0 && x >= Constants.MapWidth || y >= Constants.MapHeight)
            {
                return true;
            }

            return Rogue.GameWorld.Map.Tiles[x, y].Terrain == type;
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
