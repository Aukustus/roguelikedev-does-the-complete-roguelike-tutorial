using BearLib;
using RogueTutorial;
using System;
using System.Collections.Generic;

namespace CSharpRogueTutorial
{
    [Serializable]
    class GameObject
    {
        public char tile;
        public string color;
        public int x;
        public int y;
        public bool blocks;
        public int CameraX = 0;
        public int CameraY = 0;

        public IEnumerable<GameObject> GameWorld { get; private set; }

        public GameObject(char Tile, string Color, int X, int Y, bool Blocks = true)
        {
            x = X;
            y = Y;
            tile = Tile;
            color = Color;
            blocks = Blocks;
        }

        internal void Draw()
        {
            Terminal.Layer(3);
            int drawX = (x - Rogue.GameWorld.Player.CameraX) * 2 + 1;
            int drawY = (y - Rogue.GameWorld.Player.CameraY) * 2 + 1;

            Terminal.Color(Terminal.ColorFromName(color));
            Terminal.Print(drawX, drawY, "[offset=8,8]" +  tile.ToString());

            Terminal.Color(Terminal.ColorFromName("white"));
        }

        internal void Move(int dx, int dy)
        {
            if (!MapMethods.Blocked(x + dx, y + dy))
            {
                x += dx;
                y += dy;

                if (this == Rogue.GameWorld.Player)
                    Camera.HandleMoveCamera(dx, dy);
            }
        }

        internal void PlayerMoveOrAttack(int dx, int dy)
        {
            int targetX = x + dx;
            int targetY = y + dy;

            if (MapMethods.MapBlocked(x + dx, y + dy))
            {
                return;
            }

            foreach (GameObject obj in Rogue.GameWorld.Objects)
            {
                if (obj.x == targetX && obj.y == targetY)
                {
                    Attack(obj);
                    return;
                }
            }

            Move(dx, dy);
        }

        internal void Attack(GameObject target)
        {

        }
    }
}
