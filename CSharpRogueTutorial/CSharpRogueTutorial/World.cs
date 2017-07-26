using System;
using System.Collections.Generic;

namespace CSharpRogueTutorial
{
    [Serializable]
    class World
    {
        public GameMap Map;
        public GameObject Player;
        public List<GameObject> Objects;
        public MessageLog MessageLog;
        public Constants.GameState State;

        public World()
        {
        }
    }
}
