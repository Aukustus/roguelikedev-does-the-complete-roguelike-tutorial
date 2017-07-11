using BearLib;
using RogueTutorial;
using System.Linq;

namespace CSharpRogueTutorial
{
    class Controls
    {
        static int[] LeftMovement = { Terminal.TK_LEFT, Terminal.TK_KP_4, Terminal.TK_H };
        static int[] RightMovement = { Terminal.TK_RIGHT, Terminal.TK_KP_6, Terminal.TK_L };
        static int[] UpMovement = { Terminal.TK_UP, Terminal.TK_KP_8, Terminal.TK_K };
        static int[] DownMovement = { Terminal.TK_DOWN, Terminal.TK_KP_2, Terminal.TK_J };

        public static int[] EscapeKeys = { Terminal.TK_ESCAPE };

        public static Constants.PlayerAction HandleKeys()
        {
            int key = Terminal.Read();

            if (LeftMovement.Contains(key))
            {
                Rogue.GameWorld.Player.PlayerMoveOrAttack(-1, 0);
                return Constants.PlayerAction.UsedTurn;
            }
            else if (RightMovement.Contains(key))
            {
                Rogue.GameWorld.Player.PlayerMoveOrAttack(1, 0);
                return Constants.PlayerAction.UsedTurn;
            }
            else if (UpMovement.Contains(key))
            {
                Rogue.GameWorld.Player.PlayerMoveOrAttack(0, -1);
                return Constants.PlayerAction.UsedTurn;
            }
            else if (DownMovement.Contains(key))
            {
                Rogue.GameWorld.Player.PlayerMoveOrAttack(0, 1);
                return Constants.PlayerAction.UsedTurn;
            }

            else if (EscapeKeys.Contains(key))
            {
                return Constants.PlayerAction.ExitGame;
            }

            return Constants.PlayerAction.NotUsedTurn;
        }
    }
}
