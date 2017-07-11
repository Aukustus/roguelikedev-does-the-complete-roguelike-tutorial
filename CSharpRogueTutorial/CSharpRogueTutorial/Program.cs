using CSharpRogueTutorial;
using System.Collections.Generic;
using BearLib;
using System;

namespace RogueTutorial
{
    class Rogue
    {
        public static World GameWorld;

        private static void Initialize()
        {
            Terminal.Open();
            Terminal.Set("window: size=" + Constants.ScreenWidth.ToString() + "x" + Constants.ScreenHeight.ToString() + "; font: Cheepicus.png, size=16x16");
            Terminal.Set("0xE000: Tileset.png, size=32x32");

            PreCalcFov();
        }

        private static void PreCalcFov()
        {
            for (int i = 0; i < 360; i += 3)
            {
                double ax = Math.Sin(i / (180 / Math.PI));
                double ay = Math.Cos(i / (180 / Math.PI));

                Constants.PreCalcSin.Add(i, ax);
                Constants.PreCalcCos.Add(i, ay);
            }
        }

        private static void NewGame()
        {
            GameWorld = new World();

            GameWorld.Objects = new List<GameObject>();

            GameWorld.Player = new GameObject(Constants.Tiles.PlayerTile, "white", 0, 0);
            GameWorld.Objects.Add(GameWorld.Player);

            GameWorld.Map = MapMethods.MakeMap();
        }

        private static void MainLoop()
        {
            while (true)
            {
                Rendering.RenderAll();

                Constants.PlayerAction action = Controls.HandleKeys();

                if (action == Constants.PlayerAction.ExitGame)
                {
                    break;
                }

                if (action == Constants.PlayerAction.UsedTurn)
                {
                    foreach (GameObject obj in GameWorld.Objects)
                    {
                        if (obj != GameWorld.Player)
                        {

                        }
                    }
                }
            }

            Terminal.Close();
        }

        static void Main(string[] args)
        {
            Initialize();
            NewGame();
            MainLoop();
        }
    }
}
