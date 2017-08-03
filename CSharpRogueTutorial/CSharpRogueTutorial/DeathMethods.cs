using RogueTutorial;

namespace CSharpRogueTutorial
{
    class DeathMethods
    {
        public static void PlayerDeath(GameObject owner)
        {
            Rogue.GameWorld.MessageLog.AddMessage(owner.Name + " dies.", "red");
            owner.Tile = Constants.Tiles.CorpseTile;
            Rogue.GameWorld.State = Constants.GameState.Dead;
        }

        public static void GenericDeath(GameObject owner)
        {
            Rogue.GameWorld.MessageLog.AddMessage(owner.Name + " dies.", "red");

            owner.Fighter.AI.Type = Constants.AI.None;
            owner.Blocks = false;
            owner.AlwaysVisible = true;
            owner.SendToBack();
            owner.Name = "Remains of " + owner.Name;
            owner.Tile = Constants.Tiles.CorpseTile;
        }
    }
}
