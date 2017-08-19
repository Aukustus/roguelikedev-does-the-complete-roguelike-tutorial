using BearLib;
using RogueTutorial;

namespace CSharpRogueTutorial
{
    class UseMethods
    {
        private static void ThrowingAnimation(GameObject obj, GameObject user)
        {
            for (int times = 0; times < Constants.SpellRange; times++)
            {
                if (user.Fighter.Direction == 90 || user.Fighter.Direction == 270)
                {
                    int dx = user.Fighter.Direction == 90 ? -1 : 1;

                    if (GameMap.MapBlocked(obj.X + dx, obj.Y))
                    {
                        break;
                    }
                    if (CommonMethods.TargetInCoordinate(obj.X, obj.Y))
                    {
                        break;
                    }

                    for (int x = 0; x < 16; x++)
                    {
                        Rendering.RenderAll(obj);
                        obj.OffsetX += Constants.MoveSmoothSteps * dx;

                        if (GameMap.MapExplored(obj.X + dx, obj.Y) || FoV.InFov(user.X, user.Y, obj.X + dx, obj.Y, user))
                        {
                            obj.Draw("white", true);
                        }

                        Terminal.Refresh();
                    }

                    obj.X += dx;
                    obj.OffsetX = 0;
                }
                else if (user.Fighter.Direction == 0 || user.Fighter.Direction == 180)
                {
                    int dy = user.Fighter.Direction == 0 ? -1 : 1;

                    if (GameMap.MapBlocked(obj.X, obj.Y + dy))
                    {
                        break;
                    }
                    if (CommonMethods.TargetInCoordinate(obj.X, obj.Y))
                    {
                        break;
                    }

                    for (int x = 0; x < 16; x++)
                    {
                        Rendering.RenderAll(obj);
                        obj.OffsetY += Constants.MoveSmoothSteps * dy;

                        if (GameMap.MapExplored(obj.X, obj.Y + dy) || FoV.InFov(user.X, user.Y, obj.X, obj.Y + dy, user))
                        {
                            obj.Draw("white", true);
                        }

                        Terminal.Refresh();
                    }

                    obj.Y += dy;
                    obj.OffsetY = 0;
                }
            }
        }

        public static bool Throw(GameObject item, GameObject user)
        {
            MessageLog.AddMessage("Throw: " + user.Name + " throws " + item.Name + ".", "white");

            GameObject thrownItem = item.Clone();
            thrownItem.Item.Count = 1;
            thrownItem.X = user.X;
            thrownItem.Y = user.Y;

            Rogue.GameWorld.Objects.Add(thrownItem);

            Rendering.RenderAll();
            Terminal.Refresh();

            ThrowingAnimation(thrownItem, user);

            return false;
        }

        public static bool HealingPotion(GameObject user)
        {
            if (Rogue.GameWorld.Player.Fighter.HP == Rogue.GameWorld.Player.Fighter.Max_HP)
            {
                MessageLog.AddMessage("You are already at full health", "white");
                return false;
            }

            int amount = 6;

            MessageLog.AddMessage(user.Name + " is healed for " + amount.ToString() + " Hit Points." , "white");

            Rogue.GameWorld.Player.Fighter.Heal(amount);

            return true;
        }

        public static bool Fireball(GameObject user)
        {
            MessageLog.AddMessage("Fireball: " + user.Name + " casts Fireball.", "white");

            GameObject spell = new GameObject("Fireball", Constants.Tiles.Effect.FireballTile, user.X, user.Y);
            spell.Spell = true;
            spell.AlwaysVisible = false;

            Rogue.GameWorld.Objects.Add(spell);

            Rendering.RenderAll();
            Terminal.Refresh();

            ThrowingAnimation(spell, user);

            MessageLog.AddMessage("Fireball: The fireball explodes.", "white");

            foreach (GameObject obj in Rogue.GameWorld.Objects)
            {
                if (obj.Fighter != null && obj.Fighter.AI.Type != Constants.AI.None && obj.X == spell.X && obj.Y == spell.Y)
                {
                    int amount = 12;
                    MessageLog.AddMessage("Fireball: " + obj.Name + " is damaged for " + amount.ToString() + " Hit Points.", "white");

                    obj.Fighter.TakeDamage(amount);
                    break;
                }
            }

            Rogue.GameWorld.Objects.Remove(spell);
      
            return false;
        }
    }
}
