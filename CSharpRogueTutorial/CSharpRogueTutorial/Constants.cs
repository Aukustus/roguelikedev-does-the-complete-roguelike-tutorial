using System.Collections.Generic;

namespace CSharpRogueTutorial
{
    class Constants
    {
        public const int ScreenWidth = 80;
        public const int ScreenHeight = 45;

        public const int MapWidth = 100;
        public const int MapHeight = 35;

        public const int CameraWidth = 15;
        public const int CameraHeight = 8;

        public const int FoVSteps = 1;
        public const int TurnSteps = 6;

        public const int SpellRange = 6;

        public const int MoveSmoothSteps = 4;

        public const int MessageLogLength = 6;

        public static Dictionary<int, double> PreCalcSin = new Dictionary<int, double>();
        public static Dictionary<int, double> PreCalcCos = new Dictionary<int, double>();

        public enum PlayerAction { UsedTurn, NotUsedTurn, ExitGame, ExitWithoutSave };
        public enum AI { None, Player, BasicMonster, ConfusedMonster };
        public enum Death { PlayerDeath, GenericDeath };
        public enum GameState { Playing, Dead };
        public enum UseFunctions { None, HealingPotion, Fireball, Equip };

        public enum Direction { Up, Down };

        public enum Items { Nothing, HealingPotion, Fireball, Sword };
        public enum Monsters { Orc, Troll };

        public enum Slots { MainHand }

        public enum Terrain
        {
            TileWall,
            TileFloor
        }

        public static int[] Angles = { 0, 90, 180, 270 };

        public static int[] LevelProgression = { 10, 30, 60, 100, 150 };

        public static Dictionary<string, int> Layers = new Dictionary<string, int>();
    }
}
