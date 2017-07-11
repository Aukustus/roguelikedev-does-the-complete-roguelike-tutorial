using RogueTutorial;

namespace CSharpRogueTutorial
{
    class Camera
    {
        public static void HandleMoveCamera(int dx, int dy)
        {
            if (dx == -1 && dy == 0)
            {
                if (Rogue.GameWorld.Player.x > Constants.CameraWidth / 2 - 1 && Rogue.GameWorld.Player.x < Constants.MapWidth - 1 - Constants.CameraWidth / 2)
                    MoveCamera(-1, 0);
            }
            else if (dx == 1 && dy == 0)
            {
                if (Rogue.GameWorld.Player.x > Constants.CameraWidth / 2 && Rogue.GameWorld.Player.x < Constants.MapWidth - Constants.CameraWidth / 2)
                    MoveCamera(1, 0);
            }
            else if (dx == 0 && dy == -1)
            {
                if (Rogue.GameWorld.Player.y > Constants.CameraHeight / 2 - 1 && Rogue.GameWorld.Player.y < Constants.MapHeight - Constants.CameraHeight / 2 - 1)
                    MoveCamera(0, -1);
            }
            else if (dx == 0 && dy == 1)
            {
                if (Rogue.GameWorld.Player.y > Constants.CameraHeight / 2 && Rogue.GameWorld.Player.y < Constants.MapHeight - Constants.CameraHeight / 2)
                    MoveCamera(0, 1);
            }
            else if (dx == -1 && dy == -1)
            {
                if (Rogue.GameWorld.Player.x > Constants.CameraWidth / 2 - 1 && Rogue.GameWorld.Player.x < Constants.MapWidth - 1 - Constants.CameraWidth / 2)
                    MoveCamera(-1, 0);
                if (Rogue.GameWorld.Player.y > Constants.CameraHeight / 2 - 1 && Rogue.GameWorld.Player.y < Constants.MapHeight - Constants.CameraHeight / 2 - 1)
                    MoveCamera(0, -1);
            }
            else if (dx == 1 && dy == -1)
            {
                if (Rogue.GameWorld.Player.x > Constants.CameraWidth / 2 && Rogue.GameWorld.Player.x < Constants.MapWidth - Constants.CameraWidth / 2)
                    MoveCamera(1, 0);
                if (Rogue.GameWorld.Player.y > Constants.CameraHeight / 2 - 1 && Rogue.GameWorld.Player.y < Constants.MapHeight - Constants.CameraHeight / 2 - 1)
                    MoveCamera(0, -1);
            }
            else if (dx == -1 && dy == 1)
            {
                if (Rogue.GameWorld.Player.x > Constants.CameraWidth / 2 - 1 && Rogue.GameWorld.Player.x < Constants.MapWidth - 1 - Constants.CameraWidth / 2)
                    MoveCamera(-1, 0);
                if (Rogue.GameWorld.Player.y > Constants.CameraHeight / 2 && Rogue.GameWorld.Player.y < Constants.MapHeight - Constants.CameraHeight / 2)
                    MoveCamera(0, 1);
            }
            else if (dx == 1 && dy == 1)
            {
                if (Rogue.GameWorld.Player.x > Constants.CameraWidth / 2 && Rogue.GameWorld.Player.x < Constants.MapWidth - Constants.CameraWidth / 2)
                    MoveCamera(1, 0);
                if (Rogue.GameWorld.Player.y > Constants.CameraHeight / 2 && Rogue.GameWorld.Player.y < Constants.MapHeight - Constants.CameraHeight / 2)
                    MoveCamera(0, 1);
            }
        }

        public static void MoveCamera(int dx, int dy)
        {
            Rogue.GameWorld.Player.CameraX += dx;
            Rogue.GameWorld.Player.CameraY += dy;
        }

        public static void SetCamera()
        {
            int x = Rogue.GameWorld.Player.x - Constants.CameraWidth / 2;
            int y = Rogue.GameWorld.Player.y - Constants.CameraHeight / 2;

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
    }
}
