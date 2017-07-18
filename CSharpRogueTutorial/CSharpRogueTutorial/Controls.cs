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

        static int[] UpLeftMovement = { Terminal.TK_KP_7, Terminal.TK_Y };
        static int[] UpRightMovement = { Terminal.TK_KP_9, Terminal.TK_U };
        static int[] DownLeftMovement = { Terminal.TK_KP_1, Terminal.TK_B };
        static int[] DownRightMovement = { Terminal.TK_KP_3, Terminal.TK_N };

        static int[] SkipTurn = { Terminal.TK_PERIOD, Terminal.TK_KP_5 };

        public static int[] EscapeKeys = { Terminal.TK_ESCAPE };

        public static Constants.PlayerAction HandleKeys()
        {
            int key = Terminal.Read();

            if (Rogue.GameWorld.State == Constants.GameState.Playing)
            {
                if (LeftMovement.Contains(key))
                {
                    if (Rogue.GameWorld.Player.Fighter.Direction == 90)
                    {
                        Rogue.GameWorld.Player.PlayerMoveOrAttack(-1, 0);
                    }

                    Rogue.GameWorld.Player.Fighter.Direction = 90;
                    return Constants.PlayerAction.UsedTurn;
                }
                else if (RightMovement.Contains(key))
                {
                    if (Rogue.GameWorld.Player.Fighter.Direction == 270)
                    {
                        Rogue.GameWorld.Player.PlayerMoveOrAttack(1, 0);
                    }

                    Rogue.GameWorld.Player.Fighter.Direction = 270;
                    return Constants.PlayerAction.UsedTurn;
                }
                else if (UpMovement.Contains(key))
                {
                    if (Rogue.GameWorld.Player.Fighter.Direction == 0)
                    {
                        Rogue.GameWorld.Player.PlayerMoveOrAttack(0, -1);
                    }

                    Rogue.GameWorld.Player.Fighter.Direction = 0;
                    return Constants.PlayerAction.UsedTurn;
                }
                else if (DownMovement.Contains(key))
                {
                    if (Rogue.GameWorld.Player.Fighter.Direction == 180)
                    {
                        Rogue.GameWorld.Player.PlayerMoveOrAttack(0, 1);
                    }

                    Rogue.GameWorld.Player.Fighter.Direction = 180;
                    return Constants.PlayerAction.UsedTurn;
                }
                else if (UpLeftMovement.Contains(key))
                {
                    if (Rogue.GameWorld.Player.Fighter.Direction == 45)
                    {
                        Rogue.GameWorld.Player.PlayerMoveOrAttack(-1, -1);
                    }

                    Rogue.GameWorld.Player.Fighter.Direction = 45;
                    return Constants.PlayerAction.UsedTurn;
                }
                else if (UpRightMovement.Contains(key))
                {
                    if (Rogue.GameWorld.Player.Fighter.Direction == 315)
                    {
                        Rogue.GameWorld.Player.PlayerMoveOrAttack(1, -1);
                    }

                    Rogue.GameWorld.Player.Fighter.Direction = 315;
                    return Constants.PlayerAction.UsedTurn;
                }
                else if (DownLeftMovement.Contains(key))
                {
                    if (Rogue.GameWorld.Player.Fighter.Direction == 135)
                    {
                        Rogue.GameWorld.Player.PlayerMoveOrAttack(-1, 1);
                    }

                    Rogue.GameWorld.Player.Fighter.Direction = 135;
                    return Constants.PlayerAction.UsedTurn;
                }
                else if (DownRightMovement.Contains(key))
                {
                    if (Rogue.GameWorld.Player.Fighter.Direction == 225)
                    {
                        Rogue.GameWorld.Player.PlayerMoveOrAttack(1, 1);
                    }

                    Rogue.GameWorld.Player.Fighter.Direction = 225;
                    return Constants.PlayerAction.UsedTurn;
                }
                else if (SkipTurn.Contains(key))
                {
                    return Constants.PlayerAction.UsedTurn;
                }
            }

            if (EscapeKeys.Contains(key))
            {
                return Constants.PlayerAction.ExitGame;
            }

            return Constants.PlayerAction.NotUsedTurn;
        }
    }
}
