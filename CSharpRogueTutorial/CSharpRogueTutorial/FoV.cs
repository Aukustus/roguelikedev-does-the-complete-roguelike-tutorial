using RogueTutorial;
using System;

namespace CSharpRogueTutorial
{
    class FoV
    {
        public static void RayCast()
        {
            for (int i = 0; i < 360; i += Constants.FoVSteps)
            {
                double ax = Constants.PreCalcSin[i];
                double ay = Constants.PreCalcCos[i];

                double x = Rogue.GameWorld.Player.x;
                double y = Rogue.GameWorld.Player.y;

                Rogue.GameWorld.Map.tiles[(int)x, (int)y].explored = true;

                for (int j = 0; j < Constants.TorchRadius; j++)
                {
                    x -= ax;
                    y -= ay;

                    if (x < 0 || y < 0 || x > Constants.MapWidth - 1 || y > Constants.MapHeight - 1)
                    {
                        break;
                    }

                    Rogue.GameWorld.Map.tiles[(int)Math.Round(x), (int)Math.Round(y)].explored = true;

                    if (Rogue.GameWorld.Map.tiles[(int)Math.Round(x), (int)Math.Round(y)].blocked)
                    {
                        break;
                    }
                }
            }
        }

        public static bool InFov(int sourceX, int sourceY, int targetX, int targetY)
        {
            if (sourceX == targetX && sourceY == targetY)
            {
                return true;
            }

            for (int i = 0; i < 360; i += Constants.FoVSteps)
            {
                double ax = Constants.PreCalcSin[i];
                double ay = Constants.PreCalcCos[i];

                double x = sourceX;
                double y = sourceY;

                for (int j = 0; j < Constants.TorchRadius; j++)
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

                    if (Rogue.GameWorld.Map.tiles[(int)Math.Round(x), (int)Math.Round(y)].blocked)
                    {
                        break;
                    }
                }
            }

            return false;
        }
    }
}
