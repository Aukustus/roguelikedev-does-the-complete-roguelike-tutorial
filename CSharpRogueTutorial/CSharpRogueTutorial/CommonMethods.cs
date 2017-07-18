using RogueTutorial;
using System;

namespace CSharpRogueTutorial
{
    class CommonMethods
    {
        public static int DistanceBetween(int x1, int y1, int x2, int y2)
        {
            return (int)(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2)));
        }
    }
}
