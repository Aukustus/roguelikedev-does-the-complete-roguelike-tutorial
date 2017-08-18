using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRogueTutorial
{
    [Serializable]
    class World
    {
        public GameMap Map;
        public GameObject Player;
        public List<GameObject> Objects = new List<GameObject>();
        public MessageLog MessageLog = new MessageLog();
        public Constants.GameState State;
        public int DungeonLevel = 1;
        public List<LevelMemory> LevelMemory = new List<LevelMemory>();

        public World()
        {
        }

        internal void StoreLevel()
        {
            LevelMemory storedLevel = (from mem in LevelMemory
                                       where mem.DungeonLevel == DungeonLevel
                                       select mem).FirstOrDefault();

            if (storedLevel == null)
            {
                Coordinate upstairs = (from obj in Objects
                                       where obj.Upstairs == true
                                       select new Coordinate
                                       {
                                           X = obj.X,
                                           Y = obj.Y
                                       }).FirstOrDefault();

                Coordinate downstairs = (from obj in Objects
                                         where obj.Downstairs == true
                                         select new Coordinate
                                         {
                                             X = obj.X,
                                             Y = obj.Y
                                         }).FirstOrDefault();

                LevelMemory level = new LevelMemory(Objects, Map, DungeonLevel, upstairs, downstairs);
                level.Objects.Remove(Player);
                LevelMemory.Add(level);
            }
            else
            {
                storedLevel.Objects = Objects;
                storedLevel.Objects.Remove(Player);
            }
        }

        internal LevelMemory LoadLevel()
        {
            LevelMemory storedLevel = (from mem in LevelMemory
                                       where mem.DungeonLevel == DungeonLevel
                                       select mem).FirstOrDefault();

            return storedLevel;
        }
    }
}
