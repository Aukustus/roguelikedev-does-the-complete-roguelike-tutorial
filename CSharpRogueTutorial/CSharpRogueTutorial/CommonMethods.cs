using RogueTutorial;
using System;
using System.Collections.Generic;

namespace CSharpRogueTutorial
{
    class CommonMethods
    {
        public static double DistanceBetween(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public static GameObject ClosestMonster(GameObject owner)
        {
            GameObject closestObject = null;
            int closestDist = Constants.TorchRadius;

            foreach (GameObject obj in Rogue.GameWorld.Objects)
            {
                if (obj != owner && obj.Fighter != null && obj.Fighter.AI.Type != Constants.AI.None && FoV.InFov(owner.X, owner.Y, obj.X, obj.Y, owner))
                {
                    int distance = (int)DistanceBetween(owner.X, owner.Y, obj.X, obj.Y);
                    if (distance < closestDist)
                    {
                        closestObject = obj;
                        closestDist = distance;
                    }
                }
            }

            return closestObject;
        }

        public static List<GameObject> AllMonstersWithinDistance(GameObject owner, int x, int y, int distance)
        {
            List<GameObject> objects = new List<GameObject>();

            foreach (GameObject obj in Rogue.GameWorld.Objects)
            {
                if (obj.Fighter != null && obj.Fighter.AI.Type != Constants.AI.None)
                {
                    if (DistanceBetween(x, y, obj.X, obj.Y) <= distance)
                    {
                        objects.Add(obj);
                    }
                }
            }

            return objects;
        }

        public static bool TargetInCoordinate(int x, int y)
        {
            foreach (GameObject obj in Rogue.GameWorld.Objects)
            {
                if (obj != Rogue.GameWorld.Player && obj.Fighter != null && obj.Fighter.AI.Type != Constants.AI.None)
                {
                    if (obj.X == x && obj.Y == y)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
