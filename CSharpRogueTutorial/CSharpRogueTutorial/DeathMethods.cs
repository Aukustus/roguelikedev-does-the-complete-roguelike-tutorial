using RogueTutorial;

namespace CSharpRogueTutorial
{
    class DeathMethods
    {
        public static void PlayerDeath(GameObject owner)
        {
            Rogue.GameWorld.MessageLog.AddMessage(owner.Name + " dies.");
            owner.Tile = Constants.Tiles.CorpseTile;
            Rogue.GameWorld.State = Constants.GameState.Dead;
        }

        public static void GenericDeath(GameObject owner)
        {
            Rogue.GameWorld.MessageLog.AddMessage(owner.Name + " dies.");

            owner.Fighter.AI = Constants.AI.None;
            owner.Blocks = false;
            owner.AlwaysVisible = true;
            owner.SendToBack();
            owner.Tile = Constants.Tiles.CorpseTile;
        }
    }
}
