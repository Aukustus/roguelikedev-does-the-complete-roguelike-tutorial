using RogueTutorial;
using System;

namespace CSharpRogueTutorial
{
    [Serializable]
    class Tile
    {
        public bool blocked;
        public bool explored;
        public bool visited;

        public Tile(bool Blocked)
        {
            blocked = Blocked;
            explored = false;
            visited = false;
        }
    }

    [Serializable]
    class GameMap
    {
        public Tile[,] tiles;

        public GameMap(bool solid)
        {
            tiles = BlankTiles(solid);
        }

        private static Tile[,] BlankTiles(bool Blocked)
        {
            Tile[,] map = new Tile[Constants.MapWidth, Constants.MapHeight];

            for (int x = 0; x < Constants.MapWidth; x++)
            {
                for (int y = 0; y < Constants.MapHeight; y++)
                {
                    map[x, y] = new Tile(Blocked);
                }
            }

            return map;
        }

        internal void CreateHorizontalTunnel(int x1, int x2, int y)
        {
            for (int x = Math.Min(x1, x2); x < Math.Max(x1, x2) + 1; x++)
            {
                tiles[x, y].blocked = false;
            }
        }

        internal void CreateVerticalTunnel(int y1, int y2, int x)
        {
            for (int y = Math.Min(y1, y2); y < Math.Max(y1, y2) + 1; y++)
            {
                tiles[x, y].blocked = false;
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
                if (obj.x == x && obj.y == y && obj.blocks)
                {
                    return true;
                }
            }

            return Rogue.GameWorld.Map.tiles[x, y].blocked;
        }

        public static bool MapBlocked(int x, int y)
        {
            if (x < 0 || y < 0 && x >= Constants.MapWidth || y >= Constants.MapHeight)
            {
                return true;
            }

            return Rogue.GameWorld.Map.tiles[x, y].blocked;
        }

        public static bool MapExplored(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Constants.MapWidth || y >= Constants.MapHeight)
            {
                return false;
            }

            return Rogue.GameWorld.Map.tiles[x, y].explored;
        }
    }
}
