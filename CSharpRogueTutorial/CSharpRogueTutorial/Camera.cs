using RogueTutorial;

namespace CSharpRogueTutorial
{
    class Camera
    {
        public static void HandleMoveCamera(int dx, int dy)
        {
            if (dx == -1)
            {
                if (Rogue.GameWorld.Player.X > Constants.CameraWidth / 2 - 1 && Rogue.GameWorld.Player.X < Constants.MapWidth - 1 - Constants.CameraWidth / 2)
                    MoveCamera(-1, 0);
            }
            else if (dx == 1)
            {
                if (Rogue.GameWorld.Player.X > Constants.CameraWidth / 2 && Rogue.GameWorld.Player.X < Constants.MapWidth - Constants.CameraWidth / 2)
                    MoveCamera(1, 0);
            }
            if (dy == -1)
            {
                if (Rogue.GameWorld.Player.Y > Constants.CameraHeight / 2 - 1 && Rogue.GameWorld.Player.Y < Constants.MapHeight - Constants.CameraHeight / 2 - 1)
                    MoveCamera(0, -1);
            }
            else if (dy == 1)
            {
                if (Rogue.GameWorld.Player.Y > Constants.CameraHeight / 2 && Rogue.GameWorld.Player.Y < Constants.MapHeight - Constants.CameraHeight / 2)
                    MoveCamera(0, 1);
            }
        }

        private static void MoveCamera(int dx, int dy)
        {
            Rogue.GameWorld.Player.CameraX += dx;
            Rogue.GameWorld.Player.CameraY += dy;
        }

        public static Coordinate CameraToCoordinate(int x, int y)
        {
            return new Coordinate(x + Rogue.GameWorld.Player.CameraX, y + Rogue.GameWorld.Player.CameraY);
        }

        public static void SetCamera()
        {
            int x = Rogue.GameWorld.Player.X - Constants.CameraWidth / 2;
            int y = Rogue.GameWorld.Player.Y - Constants.CameraHeight / 2;

            if (x < 0)
            {
                x = 0;
            }
            if (y < 0)
            {
                y = 0;
            }
            if (x > Constants.MapWidth - Constants.CameraWidth - 1)
            {
                x = Constants.MapWidth - Constants.CameraWidth - 1;
            }
            if (y > Constants.MapHeight - Constants.CameraHeight - 1)
            {
                y = Constants.MapHeight - Constants.CameraHeight - 1;
            }

            Rogue.GameWorld.Player.CameraX = x;
            Rogue.GameWorld.Player.CameraY = y;
        }

        public static bool WithinCamera(int x, int y)
        {
            int drawX = (x - Rogue.GameWorld.Player.CameraX + 1) * 2 + 1;
            int drawY = (y - Rogue.GameWorld.Player.CameraY + 1) * 2 + 1;

            return drawX >= 0 && drawX < 62 && drawY >= 0 && drawY < 27;
        }
    }
}
