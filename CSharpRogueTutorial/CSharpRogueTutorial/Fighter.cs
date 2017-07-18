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
            Rogue.GameWorld.MessageLog.AddMessage(Owner.Name + " hits " + target.Name + " for " + damage.ToString() + " Hit Points.");

            target.Fighter.TakeDamage(damage);
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
