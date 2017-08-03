using System.Collections.Generic;

namespace CSharpRogueTutorial
{
    class Constants
    {
        public const int ScreenWidth = 80;
        public const int ScreenHeight = 45;

        public const int MapWidth = 100;
        public const int MapHeight = 35;

        public const int TorchRadius = 3;
        public const int CameraWidth = 15;
        public const int CameraHeight = 8;

        public const int FoVSteps = 1;
        public const int TurnSteps = 6;

        public const int SpellRange = 6;

        public const int MoveSmoothSteps = 4;

        public const int MessageLogLength = 6;

        public static Dictionary<int, double> PreCalcSin = new Dictionary<int, double>();
        public static Dictionary<int, double> PreCalcCos = new Dictionary<int, double>();

        public enum PlayerAction { UsedTurn, NotUsedTurn, ExitGame };
        public enum AI { None, Player, BasicMonster, ConfusedMonster };
        public enum Death { PlayerDeath, GenericDeath };
        public enum GameState { Playing, Dead };
        public enum UseFunctions { None, HealingPotion, LightningBolt, Confusion, Fireball }

        public static int[] Angles = { 0, 45, 90, 135, 180, 225, 270, 315 };

        public static Dictionary<string, int> Layers = new Dictionary<string, int>();

        public struct Symbols
        {
            public const int VerticalBar = (char)0xE104;
            public const int HorizonalBar = (char)0xE105;
            public const int SW = (char)0xE102;
            public const int NW = (char)0xE100;
            public const int SE = (char)0xE103;
            public const int NE = (char)0xE101;

            public const int DownCross = (char)0xE109;
            public const int UpCross = (char)0xE108;
            public const int RightCross = (char)0xE106;
            public const int LeftCross = (char)0xE107;

            public const int Empty = (char)0xE110;
        }

        public struct Tiles
        {
            public const char WallTile = (char)0xE000;
            public const char FloorTile = (char)0xE001;
            public const char PlayerTile = (char)0xE002;
            public const char OrcTile = (char)0xE004;
            public const char TrollTile = (char)0xE005;
            public const char CorpseTile = (char)0xE006;

            public const char HealingPotionTile = (char)0xE008;
            public const char ScrollTile = (char)0xE009;
            public const char FireballTile = (char)0xE00C;
        }
    }
}
