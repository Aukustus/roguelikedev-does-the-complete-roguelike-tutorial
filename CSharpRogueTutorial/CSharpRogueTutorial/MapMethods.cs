using RogueTutorial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRogueTutorial
{
    struct Coordinate
    {
        public int x;
        public int y;

        public Coordinate(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }

    class MapMethods
    {
        public static Random rand = new Random();          

        public static GameMap MakeMap()
        {
            GameMap map = new GameMap(true);

            List<Room> roomList = new List<Room>();

            int roomCount = 0;

            for (int i = 0; i < 20; i++)
            {
                int width = rand.Next(5, 8);
                int height = rand.Next(5, 8);

                int x = rand.Next(0, Constants.MapWidth - width - 1);
                int y = rand.Next(0, Constants.MapHeight - height - 1);

                Room newRoom = new Room(x, y, width, height);

                if (!newRoom.Intersects(roomList))
                {
                    newRoom.CarveRoomToMap(ref map.tiles);

                    Coordinate newCenter = newRoom.Center();

                    if (roomCount == 0)
                    {
                        Rogue.GameWorld.Player.x = newCenter.x;
                        Rogue.GameWorld.Player.y = newCenter.y;
                    }
                    else
                    {
                        Coordinate previousCenter = roomList[roomCount - 1].Center();

                        if (rand.Next(0, 2) == 0)
                        {
                            map.CreateHorizontalTunnel(previousCenter.x, newCenter.x, previousCenter.y);
                            map.CreateVerticalTunnel(previousCenter.y, newCenter.y, newCenter.x);
                        }
                        else
                        {
                            map.CreateVerticalTunnel(previousCenter.y, newCenter.y, previousCenter.x);
                            map.CreateHorizontalTunnel(previousCenter.x, newCenter.x, newCenter.y);
                        }

                        newRoom.PlaceObjects();
                    }

                    roomList.Add(newRoom);
                    roomCount += 1;
                }
            }

            Camera.SetCamera();

            return map;
        }

        public static GameMap MakeMaze()
        {
            GameMap map = new GameMap(true);

            for (int x = 0; x < Constants.MapWidth - 1; x++)
            {
                for (int y = 0; y < Constants.MapHeight - 1; y++)
                {
                    if (x % 2 != 0 && y % 2 != 0)
                    {
                        map.tiles[x, y] = new Tile(false);
                    }
                }
            }

            CarveMaze(1, 1, ref map.tiles);

            Rogue.GameWorld.Player.x = 1;
            Rogue.GameWorld.Player.y = 1;

            Camera.SetCamera();

            return map;
        }

        public static void CarveMaze(int startx, int starty, ref Tile[,] tiles)
        {
            tiles[startx, starty].visited = true;

            foreach (Coordinate tile in GetMazeNeighbours(startx, starty))
            {
                if (tiles[tile.x, tile.y].visited == false)
                {
                    tiles[(tile.x + startx) / 2, (tile.y + starty) / 2].blocked = false;

                    CarveMaze(tile.x, tile.y, ref tiles);
                }
            }
        }

        public static List<Coordinate> GetMazeNeighbours(int x, int y)
        {
            List<Coordinate> neighbours = new List<Coordinate>();

            if (x - 2 >= 0) neighbours.Add(new Coordinate(x - 2, y));
            if (x + 2 <= Constants.MapWidth - 2) neighbours.Add(new Coordinate(x + 2, y));
            if (y - 2 >= 0) neighbours.Add(new Coordinate(x, y - 2));
            if (y + 2 <= Constants.MapHeight - 2) neighbours.Add(new Coordinate(x, y + 2));

            return neighbours.OrderBy(a => rand.Next()).ToList();
        }
    }
}
