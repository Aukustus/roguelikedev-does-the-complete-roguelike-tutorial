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
            Rogue.GameWorld.MessageLog.AddMessage("Fireball: " + user.Name + " casts Fireball.", "white");

            GameObject spell = new GameObject("Fireball", Constants.Tiles.FireballTile, user.X, user.Y);
            spell.Spell = true;
            spell.AlwaysVisible = false;

            Rogue.GameWorld.Objects.Add(spell);

            Rendering.RenderAll();
            Terminal.Refresh();

            for (int times = 0; times < Constants.SpellRange; times++)
            {
                if (Rogue.GameWorld.Player.Fighter.Direction == 90 || Rogue.GameWorld.Player.Fighter.Direction == 270)
                {
                    int dx = Rogue.GameWorld.Player.Fighter.Direction == 90 ? -1 : 1;

                    if (GameMap.MapBlocked(spell.X + dx, spell.Y))
                    {
                        break;
                    }
                    if (CommonMethods.TargetInCoordinate(spell.X, spell.Y))
                    {
                        break;
                    }

                    for (int x = 0; x < 16; x++)
                    {
                        Rendering.RenderAll(spell);
                        spell.OffsetX += Constants.MoveSmoothSteps * dx;

                        if (GameMap.MapExplored(spell.X + dx, spell.Y) || FoV.InFov(Rogue.GameWorld.Player.X, Rogue.GameWorld.Player.Y, spell.X + dx, spell.Y, Rogue.GameWorld.Player))
                        {
                            spell.Draw("white", true);
                        }

                        Terminal.Refresh();
                    }

                    spell.X += dx;
                    spell.OffsetX = 0;
                }
                else if (Rogue.GameWorld.Player.Fighter.Direction == 0 || Rogue.GameWorld.Player.Fighter.Direction == 180)
                {
                    int dy = Rogue.GameWorld.Player.Fighter.Direction == 0 ? -1 : 1;

                    if (GameMap.MapBlocked(spell.X, spell.Y + dy))
                    {
                        break;
                    }
                    if (CommonMethods.TargetInCoordinate(spell.X, spell.Y))
                    {
                        break;
                    }

                    for (int x = 0; x < 16; x++)
                    {
                        Rendering.RenderAll(spell);
                        spell.OffsetY += Constants.MoveSmoothSteps * dy;

                        if (GameMap.MapExplored(spell.X, spell.Y + dy) || FoV.InFov(Rogue.GameWorld.Player.X, Rogue.GameWorld.Player.Y, spell.X, spell.Y + dy, Rogue.GameWorld.Player))
                        {
                            spell.Draw("white", true);
                        }

                        Terminal.Refresh();
                    }

                    spell.Y += dy;
                    spell.OffsetY = 0;
                }
            }

            Rogue.GameWorld.MessageLog.AddMessage("Fireball: The fireball explodes.", "white");

            FireballDamage(spell.X, spell.Y);

            Rogue.GameWorld.Objects.Remove(spell);
      
            return false;
        }

        private static bool FireballDamage(int x, int y)
        {
            bool damaged = false;

            foreach (GameObject obj in Rogue.GameWorld.Objects)
            {
                if (obj.Fighter != null && obj.Fighter.AI.Type != Constants.AI.None && obj.X == x && obj.Y == y)
                {
                    int amount = 12;
                    Rogue.GameWorld.MessageLog.AddMessage("Fireball: " + obj.Name + " is damaged for " + amount.ToString() + " Hit Points.", "white");

                    obj.Fighter.TakeDamage(amount);
                    damaged = true;
                    break;
                }
            }

            return damaged;
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
