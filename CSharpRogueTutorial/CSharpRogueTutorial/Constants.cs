using System.Collections.Generic;

namespace CSharpRogueTutorial
{
    class Constants
    {
        public const int ScreenWidth = 80;
        public const int ScreenHeight = 36;

        public const int MapWidth = 80;
        public const int MapHeight = 25;

        public const int TorchRadius = 6;
        public const int FoVSteps = 1;
        public const int CameraWidth = 30;
        public const int CameraHeight = 12;

        public const int MessageLogLength = 7;

        public static Dictionary<int, double> PreCalcSin = new Dictionary<int, double>();
        public static Dictionary<int, double> PreCalcCos = new Dictionary<int, double>();

        public enum PlayerAction { UsedTurn, NotUsedTurn, ExitGame };
        public enum AI { None, BasicMonster };
        public enum Death { PlayerDeath, GenericDeath };
        public enum GameState { Playing, Dead };

        public static int[] Angles = { 0, 45, 90, 135, 180, 225, 270, 315 };

        public static Dictionary<string, int> Layers = new Dictionary<string, int>();

        public struct Symbols
        {
            public const int VerticalBar = 179;
            public const int HorizonalBar = 196;
            public const int SW = 192;
            public const int NW = 218;
            public const int SE = 217;
            public const int NE = 191;

            public const int DownCross = 194;
            public const int UpCross = 193;
            public const int RightCross = 195;
            public const int LeftCross = 180;
        }

        public struct Tiles
        {
            public const char WallTile = (char)0xE000;
            public const char FloorTile = (char)0xE001;
            public const char PlayerTile = (char)0xE002;
            public const char OrcTile = (char)0xE004;
            public const char TrollTile = (char)0xE005;
            public const char CorpseTile = (char)0xE006;
        }
    }
}
