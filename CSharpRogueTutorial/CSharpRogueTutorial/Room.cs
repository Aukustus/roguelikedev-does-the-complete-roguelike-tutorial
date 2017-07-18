using RogueTutorial;
using System;
using System.Collections.Generic;

namespace CSharpRogueTutorial
{
    struct Room
    {
        public int StartX;
        public int StartY;
        public int EndX;
        public int EndY;

        public Room(int x, int y, int width, int height)
        {
            StartX = x;
            StartY = y;
            EndX = x + width;
            EndY = y + height;
        }

        internal void PlaceObjects()
        {
            Random rand = new Random();

            Coordinate center = Center();

            GameObject monster;

            if (rand.Next(0, 2) == 1)
            {
                monster = new GameObject("Orc", Constants.Tiles.OrcTile, center.X, center.Y);
                monster.Fighter = new Fighter(monster, 8, 5, 2, Constants.AI.BasicMonster, Constants.Death.GenericDeath);
            }
            else
            {
                monster = new GameObject("Troll", Constants.Tiles.TrollTile, center.X, center.Y);
                monster.Fighter = new Fighter(monster, 10, 6, 3, Constants.AI.BasicMonster, Constants.Death.GenericDeath);
            }

            Rogue.GameWorld.Objects.Add(monster);
        }

        internal Coordinate Center()
        {
            int centerX = (StartX + EndX) / 2;
            int centerY = (StartY + EndY) / 2;

            return new Coordinate(centerX, centerY);
        }

        internal void CarveRoomToMap(ref Tile[,] tiles)
        {
            for (int x = StartX + 1; x < EndX; x++)
            {
                for (int y = StartY + 1; y < EndY; y++)
                {
                    tiles[x, y].Blocked = false;
                }
            }
        }

        internal bool Intersects(List<Room> roomList)
        {
            foreach (Room otherRoom in roomList)
            {
                if (StartX <= otherRoom.EndX && EndX >= otherRoom.StartX && StartY <= otherRoom.EndY && EndY >= otherRoom.StartY)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
