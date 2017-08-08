using RogueTutorial;
using System;
using System.Collections.Generic;

namespace CSharpRogueTutorial
{
    [Serializable]
    class Item
    {
        public GameObject Owner;
        public int Count;
        public Constants.UseFunctions UseMethod;

        public Item(GameObject owner, int count, Constants.UseFunctions useMethod)
        {
            Owner = owner;
            Count = count;
            Owner.Blocks = false;
            Owner.AlwaysVisible = true;
            UseMethod = useMethod;
        }

        internal void Use(GameObject user)
        {
            bool used = (bool)(typeof(UseMethods).GetMethod(UseMethod.ToString()).Invoke(null, new object[] { user }));

            if (used)
            {
                Count -= 1;

                if (Count == 0)
                {
                    Rogue.GameWorld.Player.Fighter.Inventory.Remove(Owner);
                }
            }
        }

        internal void Throw(GameObject user)
        {
            UseMethods.Throw(Owner, user);

            Count -= 1;

            if (Count == 0)
            {
                Rogue.GameWorld.Player.Fighter.Inventory.Remove(Owner);
            }
        }

        internal void Drop(GameObject user)
        {
            user.Fighter.Inventory.Remove(Owner);
            Rogue.GameWorld.Objects.Add(Owner);
            Owner.X = user.X;
            Owner.Y = user.Y;

            MessageLog.AddMessage(user.Name + " drops " + Owner.Name + ".", "white");
        }

        internal void Pick(GameObject user)
        {
            MessageLog.AddMessage(user.Name + " picks up " + Owner.Name + ".", "white");

            Rogue.GameWorld.Objects.Remove(Owner);

            foreach (GameObject existingItem in user.Fighter.Inventory)
            {
                if (existingItem.Name == Owner.Name)
                {
                    existingItem.Item.Count += Count;
                    return;
                }
            }

            user.Fighter.Inventory.Add(Owner);
        }

        public static Constants.PlayerAction DisplayInventory()
        {
            Constants.PlayerAction actionTaken = Constants.PlayerAction.NotUsedTurn;

            while (true)
            {
                int? selected = Menu.Inventory("Inventory", Rogue.GameWorld.Player.Fighter.Inventory, "Close");

                if (selected != null)
                {
                    GameObject selectedItem = Rogue.GameWorld.Player.Fighter.Inventory[selected.Value];

                    if (selectedItem.Item.UseMethod != Constants.UseFunctions.None)
                    {
                        int? itemAction = Menu.ItemMenu(selectedItem.Name, new List<string> { "Use", "Throw", "Drop" }, "Return");

                        if (itemAction == 0)
                        {
                            selectedItem.Item.Use(Rogue.GameWorld.Player);
                            actionTaken = Constants.PlayerAction.UsedTurn;
                            break;

                        }
                        if (itemAction == 1)
                        {
                            selectedItem.Item.Throw(Rogue.GameWorld.Player);
                            actionTaken = Constants.PlayerAction.UsedTurn;
                            break;
                        }
                        if (itemAction == 2)
                        {
                            selectedItem.Item.Drop(Rogue.GameWorld.Player);
                            actionTaken = Constants.PlayerAction.UsedTurn;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            return actionTaken;
        }
    }
}
