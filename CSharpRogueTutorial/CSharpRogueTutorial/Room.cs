using RogueTutorial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRogueTutorial
{
    struct Room
    {
        private int StartX;
        private int StartY;
        private int EndX;
        private int EndY;
        private Dictionary<Constants.Monsters, int> MonsterChances;
        private Dictionary<Constants.Items, int> ItemChances;

        public Room(int x, int y, int width, int height)
        {
            StartX = x;
            StartY = y;
            EndX = x + width;
            EndY = y + height;

            MonsterChances = new Dictionary<Constants.Monsters, int>();
            MonsterChances.Add(Constants.Monsters.Orc, 80);
            MonsterChances.Add(Constants.Monsters.Troll, FromDungeonLevel(new List<Tuple<int, int>> { new Tuple<int, int>(15, 3) }));

            ItemChances = new Dictionary<Constants.Items, int>();
            ItemChances.Add(Constants.Items.Nothing, 40);
            ItemChances.Add(Constants.Items.HealingPotion, 40);
            ItemChances.Add(Constants.Items.Fireball, 20);
        }

        private static int RandomChoiceIndex(Random rand, int[] chances)
        {
            int die = rand.Next(0, chances.Sum() + 1);

            int choice = 0;
            int runningSum = 0;

            foreach (int c in chances)
            {
                runningSum += c;

                if (die <= runningSum)
                {
                    return choice;
                }

                choice += 1;
            }

            return choice;
        }

        private static Constants.Items RandomChoiceItems(Random rand, Dictionary<Constants.Items, int> dict)
        {
            return dict.Keys.ToArray()[RandomChoiceIndex(rand, dict.Values.ToArray())];
        }

        private static Constants.Monsters RandomChoiceMonsters(Random rand, Dictionary<Constants.Monsters, int> dict)
        {
            return dict.Keys.ToArray()[RandomChoiceIndex(rand, dict.Values.ToArray())];
        }

        private static int FromDungeonLevel(List<Tuple<int, int>> table)
        {
            foreach (Tuple<int,int> item in table.Reverse<Tuple<int,int>>())
            {
                if (Rogue.GameWorld.DungeonLevel >= item.Item2)
                {
                    return item.Item1;
                }
            }

            return 0;
        }

        internal void PlaceMonsters(Random rand)
        {
            Coordinate center = Center();

            int monsterCount = rand.Next(0, 3);

            for (int i = 0; i < monsterCount; i++)
            {
                Constants.Monsters choice = RandomChoiceMonsters(rand, MonsterChances);

                GameObject monster = null;

                if (choice == Constants.Monsters.Orc)
                {
                    monster = new GameObject("Orc", Constants.Tiles.OrcTile, center.X + rand.Next(-1, 2), center.Y + rand.Next(-1, 2));
                    monster.Fighter = new Fighter(monster, 8, 5, 2, 2, Constants.AI.BasicMonster, Constants.Death.GenericDeath);
                }
                else if (choice == Constants.Monsters.Troll)
                {
                    monster = new GameObject("Troll", Constants.Tiles.TrollTile, center.X + rand.Next(-1, 2), center.Y + rand.Next(-1, 2));
                    monster.Fighter = new Fighter(monster, 10, 6, 3, 3, Constants.AI.BasicMonster, Constants.Death.GenericDeath);
                }

                if (monster != null && !GameMap.Blocked(monster.X, monster.Y))
                {
                    Rogue.GameWorld.Objects.Add(monster);
                }
            }
        }

        internal void PlaceItems(Random rand)
        {
            Coordinate center = Center();

            GameObject item = null;

            Constants.Items choice = RandomChoiceItems(rand, ItemChances);

            if (choice == Constants.Items.Fireball)
            {
                item = new GameObject("Scroll of Fireball", Constants.Tiles.ScrollTile, center.X + rand.Next(-1, 2), center.Y + rand.Next(-1, 2));
                item.Item = new Item(item, 1, Constants.UseFunctions.Fireball);
            }
            else if (choice == Constants.Items.HealingPotion)
            {
                item = new GameObject("Healing Potion", Constants.Tiles.HealingPotionTile, center.X + rand.Next(-1, 2), center.Y + rand.Next(-1, 2));
                item.Item = new Item(item, 1, Constants.UseFunctions.HealingPotion);
            }

            if (item != null)
            {
                Rogue.GameWorld.Objects.Add(item);
            }
        }

        internal Coordinate Center()
        {
            int centerX = (StartX + EndX) / 2;
            int centerY = (StartY + EndY) / 2;

            return new Coordinate(centerX, centerY);
        }

        internal void CarveRoomToMap()
        {
            for (int x = StartX + 1; x < EndX; x++)
            {
                for (int y = StartY + 1; y < EndY; y++)
                {
                    Rogue.GameWorld.Map.Tiles[x, y].Blocked = false;
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
