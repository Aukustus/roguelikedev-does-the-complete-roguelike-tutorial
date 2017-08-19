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

        public static int[] Angles = { 0, 45, 90, 135, 180, 225, 270, 315 };

        public static int[] LevelProgression = { 10, 30, 60, 100, 150 };

        public static Dictionary<string, int> Layers = new Dictionary<string, int>();

        public static int[] PUA = { 0xE000, 0xE001, 0xE002, 0xE003, 0xE004, 0xE005,
                                    0xE006, 0xE007, 0xE008, 0xE009, 0xE00A, 0xE00B,
                                    0xE00C, 0xE00D, 0xE00E, 0xE00F, 0xE010, 0xE011,
                                    0xE012, 0xE013, 0xE014, 0xE015, 0xE016, 0xE017,
                                    0xE018, 0xE019, 0xE01A, 0xE01B, 0xE01C, 0xE01D,
                                    0xE01E, 0xE01F, 0xE020, 0xE021, 0xE022, 0xE023 };

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

            public const int LeftEnd = (char)0xE10A;
            public const int RightEnd = (char)0xE10B;

            public const int Empty = (char)0xE110;
        }

        public struct Tiles
        {
            public struct Player
            {
                public static char PlayerTile = (char)0xE000;

            }

            public struct Terrain
            {
                public static char WallTile = (char)0xE200;
                public static char FloorTile = (char)0xE201;
                public static char DownTile = (char)0xE202;
                public static char UpTile = (char)0xE203;
            }

            public struct Enemy
            {
                public static char OrcTile = (char)0xE400;
                public static char TrollTile = (char)0xE401;
                public static char CorpseTile = (char)0xE402;
            }

            public struct Item
            {
                public static char HealingPotionTile = (char)0xE300;
                public static char ScrollTile = (char)0xE301;
                public static char SwordTile = (char)0xE302;
            }

            public struct Effect
            {
                public static char FireballTile = (char)0xE600;
            }
        }
    }
}
