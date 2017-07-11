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
}
