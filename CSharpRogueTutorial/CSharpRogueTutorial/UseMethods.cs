using BearLib;
using RogueTutorial;

namespace CSharpRogueTutorial
{
    class UseMethods
    {
        public static bool HealingPotion(GameObject user)
        {
            if (Rogue.GameWorld.Player.Fighter.HP == Rogue.GameWorld.Player.Fighter.Max_HP)
            {
                Rogue.GameWorld.MessageLog.AddMessage("You are already at full health", "white");
                return false;
            }

            int amount = 6;

            Rogue.GameWorld.MessageLog.AddMessage(user.Name + " is healed for " + amount.ToString() + " Hit Points." , "white");

            Rogue.GameWorld.Player.Fighter.Heal(amount);

            return true;
        }

        public static bool LightningBolt(GameObject user)
        {
            Rogue.GameWorld.MessageLog.AddMessage(user.Name + " casts Lightning Bolt.", "white");

            GameObject target = CommonMethods.ClosestMonster(user);

            if (target != null)
            {
                int amount = 12;

                Rogue.GameWorld.MessageLog.AddMessage(target.Name + " is damaged for " + amount.ToString() + " Hit Points.", "white");

                target.Fighter.TakeDamage(amount);
            }

            return true;
        }

        public static bool Fireball(GameObject user)
        {
            Coordinate targetCoordinate = Targeting("Fireball");

            if (targetCoordinate.X != -1)
            {
                Rogue.GameWorld.MessageLog.AddMessage(user.Name + " casts Fireball.", "white");

                foreach (GameObject target in CommonMethods.AllMonstersWithinDistance(user, targetCoordinate.X, targetCoordinate.Y, 3))
                {
                    int amount = 12;

                    Rogue.GameWorld.MessageLog.AddMessage(target.Name + " is damaged for " + amount.ToString() + " Hit Points.", "white");

                    target.Fighter.TakeDamage(amount);
                }

                return true;
            }

            return false;
        }

        public static bool Confusion(GameObject user)
        {
            Rogue.GameWorld.MessageLog.AddMessage(user.Name + " casts Confusion.", "white");

            GameObject target = CommonMethods.ClosestMonster(user);

            if (target != null)
            {
                Rogue.GameWorld.MessageLog.AddMessage(target.Name + " is confused for 5 turns.", "white");

                target.Fighter.AI.OldAIType = target.Fighter.AI.Type;
                target.Fighter.AI.Type = Constants.AI.ConfusedMonster;
                target.Fighter.AI.TempAILength = 5;
            }

            return true;
        }

        public static Coordinate Targeting(string type)
        {
            Rogue.GameWorld.MessageLog.AddMessage(type + ": Select target");

            while (true)
            {
                Rendering.RenderAll();

                int mouseX = Terminal.State(Terminal.TK_MOUSE_X) - 1;
                int mouseY = Terminal.State(Terminal.TK_MOUSE_Y) - 1;

                if (mouseX >= 0 && mouseY >= 0 && mouseX <= 61 && mouseY <= 25)
                {
                    if (mouseX % 2 != 0)
                    {
                        mouseX -= 1;
                    }
                    if (mouseY % 2 != 0)
                    {
                        mouseY -= 1;
                    }

                    Coordinate coord = Camera.CameraToCoordinate(mouseX / 2, mouseY / 2);

                    foreach (GameObject obj in Rogue.GameWorld.Objects)
                    {
                        if (obj.X == coord.X && obj.Y == coord.Y)
                        {
                            Terminal.Print(mouseX, mouseY, obj.Name);
                        }
                    }

                    Terminal.Refresh();

                    int key = Terminal.Read();

                    if (key == Terminal.TK_MOUSE_LEFT)
                    {
                        return new Coordinate(coord.X, coord.Y);
                    }
                    if (key == Terminal.TK_ESCAPE)
                    {
                        break;
                    }
                }
            }

            return new Coordinate(-1, -1);
        }
    }
}
