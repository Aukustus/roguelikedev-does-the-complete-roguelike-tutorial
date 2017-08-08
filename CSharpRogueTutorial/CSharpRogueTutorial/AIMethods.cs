using RogueTutorial;
using System;

namespace CSharpRogueTutorial
{
    class AIMethods
    {
        public static void BasicMonster(GameObject self)
        {
            if (self.Fighter.HP <= 0)
            {
                return;
            }

            if (self.Fighter.TurnDirection != null)
            {
                self.Fighter.Direction = self.Fighter.TurnDirection.Value;
                self.Fighter.TurnDirection = null;
            }
            else if (FoV.InFov(self.X, self.Y, Rogue.GameWorld.Player.X, Rogue.GameWorld.Player.Y, self))
            {
                self.Fighter.SeenPlayerX = Rogue.GameWorld.Player.X;
                self.Fighter.SeenPlayerY = Rogue.GameWorld.Player.Y;

                if (CommonMethods.DistanceBetween(self.X, self.Y, Rogue.GameWorld.Player.X, Rogue.GameWorld.Player.Y) < 1.41 && Rogue.GameWorld.Player.Fighter.HP > 0)
                {
                    self.Fighter.MeleeAttack(Rogue.GameWorld.Player);
                }
                else
                {
                    self.MoveTowards(Rogue.GameWorld.Player.X, Rogue.GameWorld.Player.Y);
                }
            }
            else if (self.Fighter.SeenPlayerX != null && self.Fighter.SeenPlayerY != null)
            {
                self.MoveTowards(self.Fighter.SeenPlayerX.Value, self.Fighter.SeenPlayerY.Value);

                if (self.X == self.Fighter.SeenPlayerX.Value && self.Y == self.Fighter.SeenPlayerY.Value)
                {
                    self.Fighter.SeenPlayerX = null;
                    self.Fighter.SeenPlayerY = null;
                }
            }
            else
            {
                int choice = Constants.Angles[new Random().Next(0, Constants.Angles.Length)];

                if (choice == self.Fighter.Direction)
                {
                    if (self.Fighter.Direction == 0)
                    {
                        self.Move(0, -1);
                    }
                    else if (self.Fighter.Direction == 45)
                    {
                        self.Move(-1, -1);
                    }
                    else if (self.Fighter.Direction == 90)
                    {
                        self.Move(-1, 0);
                    }
                    else if (self.Fighter.Direction == 135)
                    {
                        self.Move(-1, 1);
                    }
                    else if (self.Fighter.Direction == 180)
                    {
                        self.Move(0, 1);
                    }
                    else if (self.Fighter.Direction == 225)
                    {
                        self.Move(1, 1);
                    }
                    else if (self.Fighter.Direction == 270)
                    {
                        self.Move(1, 0);
                    }
                    else if (self.Fighter.Direction == 315)
                    {
                        self.Move(1, -1);
                    }
                }
                else
                {
                    self.Fighter.Direction = choice;
                }
            }
        }
            
        public static void ConfusedMonster(GameObject self)
        {
            if (self.Fighter.HP <= 0)
            {
                return;
            }

            self.Fighter.AI.TempAILength -= 1;

            if (self.Fighter.AI.TempAILength == 0)
            {
                self.Fighter.AI.Type = self.Fighter.AI.OldAIType;

                MessageLog.AddMessage(self.Name + " is no longer confused.");
                return;
            }

            int choice = Constants.Angles[new Random().Next(0, Constants.Angles.Length)];

            if (choice == self.Fighter.Direction)
            {
                if (self.Fighter.Direction == 0)
                {
                    self.Move(0, -1);
                }
                else if (self.Fighter.Direction == 45)
                {
                    self.Move(-1, -1);
                }
                else if (self.Fighter.Direction == 90)
                {
                    self.Move(-1, 0);
                }
                else if (self.Fighter.Direction == 135)
                {
                    self.Move(-1, 1);
                }
                else if (self.Fighter.Direction == 180)
                {
                    self.Move(0, 1);
                }
                else if (self.Fighter.Direction == 225)
                {
                    self.Move(1, 1);
                }
                else if (self.Fighter.Direction == 270)
                {
                    self.Move(1, 0);
                }
                else if (self.Fighter.Direction == 315)
                {
                    self.Move(1, -1);
                }
            }
            else
            {
                self.Fighter.Direction = choice;
            }
        }
    }
}
