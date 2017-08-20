namespace CSharpRogueTutorial
{
    class Tiles
    {
        public struct Player
        {
            public static int PlayerTile = 0xE000;
        }

        public struct Terrain
        {
            public static int WallTile = 0xE200;
            public static int FloorTile = 0xE201;
        }

        public struct Object
        {
            public static int DownTile = 0xE500;
            public static int UpTile = 0xE501;
        }

        public struct Enemy
        {
            public static int OrcTile = 0xE400;
            public static int TrollTile = 0xE401;
            public static int CorpseTile = 0xE402;
        }

        public struct Item
        {
            public static int HealingPotionTile = 0xE300;
            public static int ScrollTile = 0xE301;
            public static int SwordTile = 0xE302;
        }

        public struct Effect
        {
            public static int FireballTile = 0xE600;
        }

        public struct Symbols
        {
            public const int VerticalBar = 0xE104;
            public const int HorizonalBar = 0xE105;
            public const int SW = 0xE102;
            public const int NW = 0xE100;
            public const int SE = 0xE103;
            public const int NE = 0xE101;

            public const int DownCross = 0xE109;
            public const int UpCross = 0xE108;
            public const int RightCross = 0xE106;
            public const int LeftCross = 0xE107;

            public const int LeftEnd = 0xE10A;
            public const int RightEnd = 0xE10B;

            public const int Empty = 0xE110;
        }
    }
}
