using RogueTutorial;
using System;
using System.Collections.Generic;

namespace CSharpRogueTutorial
{
    [Serializable]
    class Fighter
    {
        public int HP;
        public int Max_HP;

        private int attack;
        public int Attack
        {
            get
            {
                int bonus = 0;

                foreach (GameObject item in Inventory)
                {
                    if (item.Item.Equipment != null && item.Item.Equipment.IsEquipped)
                    {
                        bonus += item.Item.Equipment.AttackBonus;
                    }
                }

                return attack + bonus;
            }
            set
            {

            }
        }

        public int Defense;
        public int XP;

        public List<GameObject> Inventory = new List<GameObject>();

        public int? SeenPlayerX = null;
        public int? SeenPlayerY = null;

        public int Direction = 0;
        public int? TurnDirection = null;

        public AI AI;

        public Constants.Death DeathFunction;

        public int Level = 1;

        public int Sight = 3;

        public GameObject Owner;

        public Fighter(GameObject owner, int hp, int attack, int defense, int xp, Constants.AI ai, Constants.Death death)
        {
            Owner = owner;
            HP = hp;
            Max_HP = hp;
            this.attack = attack;
            Defense = defense;
            XP = xp;
            AI = new AI(ai);
            DeathFunction = death;
        }

        internal void MeleeAttack(GameObject target)
        {
            int damage = Attack - target.Fighter.Defense;
            MessageLog.AddMessage(Owner.Name + " hits " + target.Name + " for " + damage.ToString() + " Hit Points.", "white");

            target.Fighter.TakeDamage(damage);

            if (target != Rogue.GameWorld.Player)
            {
                target.Fighter.TurnToFaceAttacker(Owner);
            }
        }

        internal void Heal(int amount)
        {
            HP += amount;

            if (HP > Max_HP)
            {
                HP = Max_HP;
            }
        }

        internal void TurnToFaceAttacker(GameObject attacker)
        {
            if (attacker.X == Owner.X && attacker.Y == Owner.Y - 1 && Direction != 0)
            {
                TurnDirection = 0;
            }
            else if (attacker.X == Owner.X - 1 && attacker.Y == Owner.Y - 1 && Direction != 45)
            {
                TurnDirection = 45;
            }
            else if (attacker.X == Owner.X - 1 && attacker.Y == Owner.Y && Direction != 90)
            {
                TurnDirection = 90;
            }
            else if (attacker.X == Owner.X - 1 && attacker.Y == Owner.Y + 1 && Direction != 135)
            {
                TurnDirection = 135;
            }
            else if (attacker.X == Owner.X && attacker.Y == Owner.Y + 1 && Direction != 180)
            {
                TurnDirection = 180;
            }
            else if (attacker.X == Owner.X + 1 && attacker.Y == Owner.Y + 1 && Direction != 225)
            {
                TurnDirection = 225;
            }
            else if (attacker.X == Owner.X + 1 && attacker.Y == Owner.Y && Direction != 270)
            {
                TurnDirection = 270;
            }
            else if (attacker.X == Owner.X + 1 && attacker.Y == Owner.Y - 1 && Direction != 315)
            {
                TurnDirection = 315;
            }
            else
            {
                TurnDirection = null;
            }
        }

        internal void TakeDamage(int amount)
        {
            if (amount > 0)
            {
                HP -= amount;

                if (HP <= 0)
                {
                    if (Owner != Rogue.GameWorld.Player)
                    {
                        MessageLog.AddMessage(string.Format("{0} dies. {1} receives {2} XP.", Owner.Name, Rogue.GameWorld.Player.Name, XP), "red");
                        Rogue.GameWorld.Player.Fighter.AddXP(XP);
                    }

                    Death();
                }
            }
        }

        internal void Death()
        {
            typeof(DeathMethods).GetMethod(DeathFunction.ToString()).Invoke(null, new object[] { Owner });
        }

        internal void AddXP(int amount)
        {
            XP += amount;
            if (XP >= Constants.LevelProgression[Level - 1])
            {
                MessageLog.AddMessage(string.Format("Level up! You reached level {0}.", Level + 1));

                XP -= Constants.LevelProgression[Level - 1];
                Level += 1;
                Max_HP += 6;
                HP += 6;
                Attack += 1;
                Defense += 1;
            }
        }

        internal void TakeTurn()
        {
            if (AI.Type != Constants.AI.None)
            {
                typeof(AIMethods).GetMethod(AI.Type.ToString()).Invoke(null, new object[] { Owner });
            }
        }
    }
}
