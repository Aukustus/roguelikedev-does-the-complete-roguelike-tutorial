using RogueTutorial;
using System;
using System.Collections.Generic;

namespace CSharpRogueTutorial
{
    struct Room
    {
        public int startX;
        public int startY;
        public int endX;
        public int endY;

        public Room(int X, int Y, int Width, int Height)
        {
            startX = X;
            startY = Y;
            endX = X + Width;
            endY = Y + Height;
        }

        internal void PlaceObjects()
        {
            Random rand = new Random();

            Coordinate center = Center();

            GameObject monster;

            if (rand.Next(0, 2) == 1)
            {
                monster = new GameObject(Constants.Tiles.OrcTile, center.x, center.y);
            }
            else
            {
                monster = new GameObject(Constants.Tiles.TrollTile, center.x, center.y);
            }

            Rogue.GameWorld.Objects.Add(monster);
        }

        internal Coordinate Center()
        {
            int centerX = (startX + endX) / 2;
            int centerY = (startY + endY) / 2;

            return new Coordinate(centerX, centerY);
        }

        internal void CarveRoomToMap(ref Tile[,] tiles)
        {
            for (int x = startX + 1; x < endX; x++)
            {
                for (int y = startY + 1; y < endY; y++)
                {
                    tiles[x, y].blocked = false;
                }
            }
        }

        internal bool Intersect(Room otherRoom)
        {
            return (startX <= otherRoom.endX && endX >= otherRoom.startX && startY <= otherRoom.endY && endY >= otherRoom.startY);
        }

        internal bool Intersects(List<Room> roomList)
        {
            foreach (Room otherRoom in roomList)
            {
                if (Intersect(otherRoom))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
