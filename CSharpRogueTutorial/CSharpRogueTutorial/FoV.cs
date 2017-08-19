using RogueTutorial;
using System;

namespace CSharpRogueTutorial
{
    class FoV
    {
        public static void RayCast()
        {
            int angle = Rogue.GameWorld.Player.Fighter.Direction;

            for (int i = angle - 45; i < angle + 45 + 1; i += Constants.FoVSteps)
            {
                double ax = Constants.PreCalcSin[i];
                double ay = Constants.PreCalcCos[i];

                double x = Rogue.GameWorld.Player.X;
                double y = Rogue.GameWorld.Player.Y;

                Rogue.GameWorld.Map.Tiles[(int)x, (int)y].Explored = true;

                for (int j = 0; j < Rogue.GameWorld.Player.Fighter.Sight; j++)
                {
                    x -= ax;
                    y -= ay;

                    if (x < 0 || y < 0 || x > Constants.MapWidth - 1 || y > Constants.MapHeight - 1)
                    {
                        break;
                    }

                    Rogue.GameWorld.Map.Tiles[(int)Math.Round(x), (int)Math.Round(y)].Explored = true;

                    if (Rogue.GameWorld.Map.Tiles[(int)Math.Round(x), (int)Math.Round(y)].BlocksSight)
                    {
                        break;
                    }
                }
            }
        }

        public static bool InFov(int sourceX, int sourceY, int targetX, int targetY, GameObject owner)
        {
            if (sourceX == targetX && sourceY == targetY)
            {
                return true;
            }

            int angle = owner.Fighter.Direction;

            for (int i = angle - 45; i < angle + 45 + 1; i += Constants.FoVSteps)
            {
                double ax = Constants.PreCalcSin[i];
                double ay = Constants.PreCalcCos[i];

                double x = sourceX;
                double y = sourceY;

                for (int j = 0; j < owner.Fighter.Sight; j++)
                {
                    x -= ax;
                    y -= ay;

                    if (x < 0 || y < 0 || x > Constants.MapWidth - 1 || y > Constants.MapHeight - 1)
                    {
                        break;
                    }

                    if ((int)Math.Round(x) == targetX && (int)Math.Round(y) == targetY)
                    {
                        return true;
                    }

                    if (Rogue.GameWorld.Map.Tiles[(int)Math.Round(x), (int)Math.Round(y)].BlocksSight)
                    {
                        break;
                    }
                }
            }

            return false;
        }
    }
}
