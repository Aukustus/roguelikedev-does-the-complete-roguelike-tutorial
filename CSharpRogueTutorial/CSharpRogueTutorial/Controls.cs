using BearLib;
using RogueTutorial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRogueTutorial
{
    class Controls
    {
        static int[] LeftMovement = { Terminal.TK_LEFT, Terminal.TK_KP_4, Terminal.TK_H };
        static int[] RightMovement = { Terminal.TK_RIGHT, Terminal.TK_KP_6, Terminal.TK_L };
        public static int[] UpMovement = { Terminal.TK_UP, Terminal.TK_KP_8, Terminal.TK_K };
        public static int[] DownMovement = { Terminal.TK_DOWN, Terminal.TK_KP_2, Terminal.TK_J };

        static int[] RotateLeft = { Terminal.TK_KP_7, Terminal.TK_Y };
        static int[] RotateRight = { Terminal.TK_KP_9, Terminal.TK_U };

        static int[] SkipTurn = { Terminal.TK_PERIOD, Terminal.TK_KP_5 };
        static int[] InventoryKey = { Terminal.TK_I};

        public static int[] EscapeKeys = { Terminal.TK_ESCAPE };
        public static int[] ActionKeys = { Terminal.TK_SPACE };

        public static Constants.PlayerAction HandleKeys()
        {
            int key = Terminal.Read();

            if (Rogue.GameWorld.State == Constants.GameState.Playing)
            {
                if (RotateLeft.Contains(key))
                {
                    for (int i = 0; i < 90; i += Constants.TurnSteps)
                    {
                        Rogue.GameWorld.Player.Fighter.Direction += Constants.TurnSteps;
                        if (Rogue.GameWorld.Player.Fighter.Direction >= 360)
                        {
                            Rogue.GameWorld.Player.Fighter.Direction -= 360;
                        }
                        Rendering.RenderAll();
                    }
                    return Constants.PlayerAction.UsedTurn;
                }
                else if (RotateRight.Contains(key))
                {
                    for (int i = 0; i < 90; i += Constants.TurnSteps)
                    {
                        Rogue.GameWorld.Player.Fighter.Direction -= Constants.TurnSteps;
                        if (Rogue.GameWorld.Player.Fighter.Direction < 0)
                        {
                            Rogue.GameWorld.Player.Fighter.Direction += 360;
                        }
                        Rendering.RenderAll();
                    }
                    return Constants.PlayerAction.UsedTurn;
                }
                else if (LeftMovement.Contains(key))
                {
                    if (Rogue.GameWorld.Player.Fighter.Direction == 90)
                    {
                        Rogue.GameWorld.Player.PlayerMoveOrAttack(-1, 0);
                    }
                    else
                    {
                        Rogue.GameWorld.Player.Move(-1, 0);
                    }
                    return Constants.PlayerAction.UsedTurn;
                }
                else if (RightMovement.Contains(key))
                {
                    if (Rogue.GameWorld.Player.Fighter.Direction == 270)
                    {
                        Rogue.GameWorld.Player.PlayerMoveOrAttack(1, 0);
                    }
                    else
                    {
                        Rogue.GameWorld.Player.Move(1, 0);
                    }
                    return Constants.PlayerAction.UsedTurn;
                }
                else if (UpMovement.Contains(key))
                {
                    if (Rogue.GameWorld.Player.Fighter.Direction == 0)
                    {
                        Rogue.GameWorld.Player.PlayerMoveOrAttack(0, -1);
                    }
                    else
                    {
                        Rogue.GameWorld.Player.Move(0, -1);
                    }
                    return Constants.PlayerAction.UsedTurn;
                }
                else if (DownMovement.Contains(key))
                {
                    if (Rogue.GameWorld.Player.Fighter.Direction == 180)
                    {
                        Rogue.GameWorld.Player.PlayerMoveOrAttack(0, 1);
                    }
                    else
                    {
                        Rogue.GameWorld.Player.Move(0, 1);
                    }
                    return Constants.PlayerAction.UsedTurn;
                }
                else if (InventoryKey.Contains(key))
                {
                    Item.DisplayInventory();
                }
                else if (SkipTurn.Contains(key))
                {
                    return Constants.PlayerAction.UsedTurn;
                }
                else if(key == Terminal.TK_G)
                {
                    UseMethods.Fireball(Rogue.GameWorld.Player);
                }
                else if (ActionKeys.Contains(key))
                {
                    GameObject toPick = null;

                    foreach (GameObject item in Rogue.GameWorld.Objects)
                    {
                        if (item.Item != null && item.X == Rogue.GameWorld.Player.X && item.Y == Rogue.GameWorld.Player.Y)
                        {
                            toPick = item;
                            break;
                        }
                    }

                    if (toPick != null)
                    {
                        toPick.Item.Pick(Rogue.GameWorld.Player);
                        return Constants.PlayerAction.UsedTurn;
                    }
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
