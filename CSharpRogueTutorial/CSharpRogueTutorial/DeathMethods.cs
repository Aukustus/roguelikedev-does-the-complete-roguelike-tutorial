using RogueTutorial;

namespace CSharpRogueTutorial
{
    class DeathMethods
    {
        public static void PlayerDeath(GameObject owner)
        {
            owner.Tile = Constants.Tiles.CorpseTile;
            owner.Name = "Remains of " + owner.Name;
            Rogue.GameWorld.State = Constants.GameState.Dead;
        }

        public static void GenericDeath(GameObject owner)
        {
            owner.Fighter.AI.Type = Constants.AI.None;
            owner.Blocks = false;
            owner.AlwaysVisible = true;
            owner.SendToBack();
            owner.Name = "Remains of " + owner.Name;
            owner.Tile = Constants.Tiles.CorpseTile;
        }
    }
}
