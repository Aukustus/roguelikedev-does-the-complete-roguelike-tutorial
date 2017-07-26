using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRogueTutorial
{
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
