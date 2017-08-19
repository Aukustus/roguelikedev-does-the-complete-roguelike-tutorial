using RogueTutorial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRogueTutorial
{
    [Serializable]
    struct Coordinate
    {
        public int X;
        public int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class MapMethods
    {
        public static Random rand = new Random();          

        public static void MakeFixedMap(List<string> map)
        {
            Rogue.GameWorld.Map = new GameMap(true);

            Rogue.GameWorld.Objects = new List<GameObject>();
            Rogue.GameWorld.Objects.Add(Rogue.GameWorld.Player);

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 35; j++)
                {
                    if (map.ElementAt(j)[i] == '.')
                    {
                        Rogue.GameWorld.Map.Tiles[i, j].Blocked = false;
                    }
                }
            }

            Rogue.GameWorld.Player.X = 11;
            Rogue.GameWorld.Player.Y = 6;

            Camera.SetCamera();
        }

        public static void MakeMap(Constants.Direction direction)
        {
            Rogue.GameWorld.Map = new GameMap(true);

            Rogue.GameWorld.Objects = new List<GameObject>();
            Rogue.GameWorld.Objects.Add(Rogue.GameWorld.Player);

            List<Room> roomList = new List<Room>();

            int roomCount = 0;

            for (int i = 0; i < 20; i++)
            {
                int width = rand.Next(4, 7);
                int height = rand.Next(4, 7);

                int x = rand.Next(10, Constants.MapWidth - width - 1 - 10);
                int y = rand.Next(5, Constants.MapHeight - height - 1 - 5);

                Room newRoom = new Room(x, y, width, height);

                if (!newRoom.Intersects(roomList))
                {
                    newRoom.CarveRoomToMap();

                    Coordinate newCenter = newRoom.Center();

                    if (roomCount == 0)
                    {
                        Rogue.GameWorld.Player.X = newCenter.X;
                        Rogue.GameWorld.Player.Y = newCenter.Y;
                        Rogue.GameWorld.Player.Fighter.Direction = 180;

                        if (direction == Constants.Direction.Down)
                        {
                            GameObject up = new GameObject("Upstairs", Constants.Tiles.UpTile, newCenter.X, newCenter.Y, false);
                            up.Upstairs = true;
                            up.AlwaysVisible = true;
                            Rogue.GameWorld.Objects.Add(up);
                        }
                        else
                        {
                            GameObject down = new GameObject("Downstairs", Constants.Tiles.DownTile, newCenter.X, newCenter.Y, false);
                            down.Downstairs = true;
                            down.AlwaysVisible = true;
                            Rogue.GameWorld.Objects.Add(down);
                        }
                    }
                    else
                    {
                        Coordinate previousCenter = roomList[roomCount - 1].Center();

                        if (rand.Next(0, 2) == 0)
                        {
                            Rogue.GameWorld.Map.CreateHorizontalTunnel(previousCenter.X, newCenter.X, previousCenter.Y);
                            Rogue.GameWorld.Map.CreateVerticalTunnel(previousCenter.Y, newCenter.Y, newCenter.X);
                        }
                        else
                        {
                            Rogue.GameWorld.Map.CreateVerticalTunnel(previousCenter.Y, newCenter.Y, previousCenter.X);
                            Rogue.GameWorld.Map.CreateHorizontalTunnel(previousCenter.X, newCenter.X, newCenter.Y);
                        }

                        newRoom.PlaceMonsters(rand);
                        newRoom.PlaceItems(rand);
                    }

                    roomList.Add(newRoom);
                    roomCount += 1;
                }
            }

            Room lastRoom = roomList.Last();

            if (direction == Constants.Direction.Down)
            {
                GameObject downstairs = new GameObject("Downstairs", Constants.Tiles.DownTile, lastRoom.Center().X, lastRoom.Center().Y, false);
                downstairs.Downstairs = true;
                downstairs.AlwaysVisible = true;
                Rogue.GameWorld.Objects.Add(downstairs);
            }
            else
            {
                GameObject upstairs = new GameObject("Upstairs", Constants.Tiles.UpTile, lastRoom.Center().X, lastRoom.Center().Y, false);
                upstairs.Upstairs = true;
                upstairs.AlwaysVisible = true;
                Rogue.GameWorld.Objects.Add(upstairs);
            }

            Camera.SetCamera();
        }

        public static void MakeMaze()
        {
            Rogue.GameWorld.Map = new GameMap(true);

            Rogue.GameWorld.Objects = new List<GameObject>();
            Rogue.GameWorld.Objects.Add(Rogue.GameWorld.Player);

            for (int x = 10; x < Constants.MapWidth - 1 - 10; x++)
            {
                for (int y = 5; y < Constants.MapHeight - 1 - 5; y++)
                {
                    if (x % 2 != 0 && y % 2 == 0)
                    {
                        Rogue.GameWorld.Map.Tiles[x, y] = new Tile(false);
                    }
                }
            }

            CarveMaze(11, 6, ref Rogue.GameWorld.Map.Tiles);

            Rogue.GameWorld.Player.X = 11;
            Rogue.GameWorld.Player.Y = 6;

            Camera.SetCamera();

            CreateBorders(ref Rogue.GameWorld.Map.Tiles);
        }

        public static void CreateBorders(ref Tile[,] tiles)
        {
            for (int x = 10; x < Constants.MapWidth - 1 - 10; x++)
            {
                tiles[x, 5].Blocked = true;
            }
            for (int y = 5; y < Constants.MapHeight - 1 - 5; y++)
            {
                tiles[10, y].Blocked = true;
            }
        }

        public static void CarveMaze(int startx, int starty, ref Tile[,] tiles)
        {
            tiles[startx, starty].Visited = true;

            foreach (Coordinate tile in GetMazeNeighbours(startx, starty))
            {
                if (tiles[tile.X, tile.Y].Visited == false)
                {
                    tiles[(tile.X + startx) / 2, (tile.Y + starty) / 2].Blocked = false;

                    CarveMaze(tile.X, tile.Y, ref tiles);
                }
            }
        }

        public static List<Coordinate> GetMazeNeighbours(int x, int y)
        {
            List<Coordinate> neighbours = new List<Coordinate>();

            if (x - 2 >= 10) neighbours.Add(new Coordinate(x - 2, y));
            if (x + 2 <= Constants.MapWidth - 2 - 10) neighbours.Add(new Coordinate(x + 2, y));
            if (y - 2 >= 5) neighbours.Add(new Coordinate(x, y - 2));
            if (y + 2 <= Constants.MapHeight - 2 - 5) neighbours.Add(new Coordinate(x, y + 2));

            return neighbours.OrderBy(a => rand.Next()).ToList();
        }

        public static void NextLevel()
        {
            Rogue.GameWorld.StoreLevel();

            Rogue.GameWorld.DungeonLevel += 1;
            MessageLog.AddMessage("You descend deeper into the dungeon.");

            LevelMemory loadedLevel = Rogue.GameWorld.LoadLevel();
            if (loadedLevel != null)
            {
                Rogue.GameWorld.Map = loadedLevel.Map;
                Rogue.GameWorld.Objects = loadedLevel.Objects;
                Rogue.GameWorld.Objects.Add(Rogue.GameWorld.Player);

                Rogue.GameWorld.Player.X = loadedLevel.Upstairs.X;
                Rogue.GameWorld.Player.Y = loadedLevel.Upstairs.Y;

                Camera.SetCamera();
            }
            else
            {
                MakeMap(Constants.Direction.Down);
            }
        }

        public static Constants.PlayerAction PreviousLevel()
        {
            if (Rogue.GameWorld.DungeonLevel == 1)
            {
                int? choice = Menu.BasicMenu("Are you sure you want to exit the dungeon?", new List<string>() { "Yes" }, "No");

                if (choice != null)
                {
                    return Constants.PlayerAction.ExitWithoutSave;
                }
                else
                {
                    return Constants.PlayerAction.NotUsedTurn;
                }
            }
            else
            {
                Rogue.GameWorld.StoreLevel();

                Rogue.GameWorld.DungeonLevel -= 1;
                MessageLog.AddMessage("You ascend higher into the dungeon.");

                LevelMemory loadedLevel = Rogue.GameWorld.LoadLevel();
                if (loadedLevel != null)
                {
                    Rogue.GameWorld.Map = loadedLevel.Map;
                    Rogue.GameWorld.Objects = loadedLevel.Objects;
                    Rogue.GameWorld.Objects.Add(Rogue.GameWorld.Player);

                    Rogue.GameWorld.Player.X = loadedLevel.Downstairs.X;
                    Rogue.GameWorld.Player.Y = loadedLevel.Downstairs.Y;

                    Camera.SetCamera();
                }
                else
                {
                    MakeMap(Constants.Direction.Up);
                }

                return Constants.PlayerAction.UsedTurn;
            }
        }
    }
}
