using System;

namespace CSharpRogueTutorial
{
    [Serializable]
    class Equipment
    {
        public bool IsEquipped = false;
        public Constants.Slots Slot;
        public Item Owner;

        public int AttackBonus;

        public Equipment(Item owner, Constants.Slots slot, int attackbonus)
        {
            Owner = owner;
            Slot = slot;
            AttackBonus = attackbonus;
        }

        internal void EquipToggle(GameObject user)
        {
            if (IsEquipped)
            {
                UnEquip(user);
            }
            else if (!IsEquipped)
            {
                GameObject previousItem = Item.GetEquippedInSlot(user, Slot);

                if (previousItem != null)
                {
                    previousItem.Item.Equipment.UnEquip(user);
                }

                Equip(user);
            }
        }

        internal void Equip(GameObject user)
        {
            IsEquipped = true;
            MessageLog.AddMessage(user.Name + " equips " + Owner.Owner.Name + ".", "white");
        }

        internal void UnEquip(GameObject user)
        {
            IsEquipped = false;
            MessageLog.AddMessage(user.Name + " unequips " + Owner.Owner.Name + ".", "white");
        }
    }
}
