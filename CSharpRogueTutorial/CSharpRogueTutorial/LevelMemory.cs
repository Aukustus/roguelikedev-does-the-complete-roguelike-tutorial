using System;
using System.Collections.Generic;

namespace CSharpRogueTutorial
{
    [Serializable]
    class LevelMemory
    {
        public List<GameObject> Objects;
        public GameMap Map;
        public int DungeonLevel;

        public Coordinate Upstairs;
        public Coordinate Downstairs;

        public LevelMemory(List<GameObject> objects, GameMap map, int dungeonLevel, Coordinate upstairs, Coordinate downstairs)
        {
            Objects = objects;
            Map = map;
            DungeonLevel = dungeonLevel;
            Upstairs = upstairs;
            Downstairs = downstairs;
        }
    }
}
