using RogueTutorial;
using System;

namespace CSharpRogueTutorial
{
    class AIMethods
    {
        public static void BasicMonster(GameObject self)
        {
            if (FoV.InFov(self.X, self.Y, Rogue.GameWorld.Player.X, Rogue.GameWorld.Player.Y, self))
            {
                self.Fighter.SeenPlayerX = Rogue.GameWorld.Player.X;
                self.Fighter.SeenPlayerY = Rogue.GameWorld.Player.Y;

                if (CommonMethods.DistanceBetween(self.X, self.Y, Rogue.GameWorld.Player.X, Rogue.GameWorld.Player.Y) < 2 && Rogue.GameWorld.Player.Fighter.HP > 0)
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
    }
}
