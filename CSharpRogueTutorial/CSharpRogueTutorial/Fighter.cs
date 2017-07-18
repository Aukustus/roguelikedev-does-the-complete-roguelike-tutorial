using RogueTutorial;
using System;

namespace CSharpRogueTutorial
{
    [Serializable]
    class Fighter
    {
        public int HP;
        public int Max_HP;
        public int Attack;
        public int Defense;

        public int? SeenPlayerX = null;
        public int? SeenPlayerY = null;

        public int Direction = 0;
        public int? TurnDirection = null;
        public Constants.AI AI;
        public Constants.Death DeathFunction;

        public GameObject Owner;

        public Fighter(GameObject owner, int hp, int attack, int defense, Constants.AI ai, Constants.Death death)
        {
            Owner = owner;
            HP = hp;
            Max_HP = hp;
            Attack = attack;
            Defense = defense;
            AI = ai;
            DeathFunction = death;
        }

        internal void MeleeAttack(GameObject target)
        {
            int damage = Attack - target.Fighter.Defense;
            Rogue.GameWorld.MessageLog.AddMessage(Owner.Name + " hits " + target.Name + " for " + damage.ToString() + " Hit Points.", "white");

            target.Fighter.TakeDamage(damage);

            if (target != Rogue.GameWorld.Player)
            {
                target.Fighter.TurnToFaceAttacker(Owner);
            }
        }

        internal void TurnToFaceAttacker(GameObject attacker)
        {
            if (attacker.X == Owner.X && attacker.Y == Owner.Y - 1 && Direction != 0)
            {
                TurnDirection = 0;
                Rogue.GameWorld.MessageLog.AddMessage(Owner.Name + " turns.", "white");
            }
            else if (attacker.X == Owner.X - 1 && attacker.Y == Owner.Y - 1 && Direction != 45)
            {
                TurnDirection = 45;
                Rogue.GameWorld.MessageLog.AddMessage(Owner.Name + " turns.", "white");
            }
            else if (attacker.X == Owner.X - 1 && attacker.Y == Owner.Y && Direction != 90)
            {
                TurnDirection = 90;
                Rogue.GameWorld.MessageLog.AddMessage(Owner.Name + " turns.", "white");
            }
            else if (attacker.X == Owner.X - 1 && attacker.Y == Owner.Y + 1 && Direction != 135)
            {
                TurnDirection = 135;
                Rogue.GameWorld.MessageLog.AddMessage(Owner.Name + " turns.", "white");
            }
            else if (attacker.X == Owner.X && attacker.Y == Owner.Y + 1 && Direction != 180)
            {
                TurnDirection = 180;
                Rogue.GameWorld.MessageLog.AddMessage(Owner.Name + " turns.", "white");
            }
            else if (attacker.X == Owner.X + 1 && attacker.Y == Owner.Y + 1 && Direction != 225)
            {
                TurnDirection = 225;
                Rogue.GameWorld.MessageLog.AddMessage(Owner.Name + " turns.", "white");
            }
            else if (attacker.X == Owner.X + 1 && attacker.Y == Owner.Y && Direction != 270)
            {
                TurnDirection = 270;
                Rogue.GameWorld.MessageLog.AddMessage(Owner.Name + " turns.", "white");
            }
            else if (attacker.X == Owner.X + 1 && attacker.Y == Owner.Y - 1 && Direction != 315)
            {
                TurnDirection = 315;
                Rogue.GameWorld.MessageLog.AddMessage(Owner.Name + " turns.", "white");
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
                    Death();
                }
            }
        }

        internal void Death()
        {
            switch (DeathFunction)
            {
                case Constants.Death.PlayerDeath:
                    DeathMethods.PlayerDeath(Owner);
                    break;
                case Constants.Death.GenericDeath:
                    DeathMethods.GenericDeath(Owner);
                    break;
            }
        }

        internal void TakeTurn()
        {
            switch (AI)
            {
                case Constants.AI.BasicMonster:
                    AIMethods.BasicMonster(Owner);
                    break;
            }
        }
    }
}
