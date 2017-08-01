using System;

namespace CSharpRogueTutorial
{
    [Serializable]
    class AI
    {
        public Constants.AI Type;
        public Constants.AI OldAIType = Constants.AI.None;
        public int TempAILength = 0;

        public AI(Constants.AI type)
        {
            Type = type;
        }
    }
}
