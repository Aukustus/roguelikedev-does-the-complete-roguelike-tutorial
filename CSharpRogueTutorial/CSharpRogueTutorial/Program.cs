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
            Terminal.Set("window: size=" + Constants.ScreenWidth.ToString() + "x" + Constants.ScreenHeight.ToString() + "; font: Cheepicus.png, size=16x16; input.filter={keyboard, mouse}");
            Terminal.Set("0xE000: Tileset.png, size=32x32");

            PreCalcFov();
            AddLayers();
        }

        private static void AddLayers()
        {
            Constants.Layers.Add("Map", 0);
            Constants.Layers.Add("Items", 1);
            Constants.Layers.Add("Monsters", 2);
            Constants.Layers.Add("Player", 3);
        }

        private static void PreCalcFov()
        {
            for (int i = -360; i < 361; i += Constants.FoVSteps)
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
            GameWorld.MessageLog = new MessageLog();
            GameWorld.Player = new GameObject("Player", Constants.Tiles.PlayerTile, 0, 0);
            GameWorld.Objects.Add(GameWorld.Player);

            GameWorld.Player.Fighter = new Fighter(GameWorld.Player, 24, 6, 4, Constants.AI.None, Constants.Death.PlayerDeath);

            GameWorld.Map = MapMethods.MakeMap();

            GameWorld.State = Constants.GameState.Playing;
        }

        private static void LoadGame()
        {
            GameWorld = File.LoadGame();
            
            GameWorld.State = Constants.GameState.Playing;

            GameWorld.MessageLog.AddMessage("Game loaded.", "white");
        }

        private static void MainLoop()
        {
            while (true)
            {
                Rendering.RenderAll();

                Constants.PlayerAction action = Controls.HandleKeys();

                if (action == Constants.PlayerAction.ExitGame)
                {
                    File.SaveGame();
                    MainMenu();
                    break;
                }

                if (GameWorld.State == Constants.GameState.Playing && action == Constants.PlayerAction.UsedTurn)
                {
                    foreach (GameObject obj in GameWorld.Objects)
                    {
                        if (obj != GameWorld.Player && obj.Fighter != null)
                        {
                            obj.Fighter.TakeTurn();
                        }
                    }
                }
            }

            Terminal.Close();
        }

        public static void MainMenu()
        {
            int? choice = Menu.BasicMenu("Game", new List<string>() { "New Game", "Load Game" }, "Exit");

            if (choice == 0)
            {
                NewGame();
                MainLoop();
            }
            else if (choice == 1)
            {
                try
                {
                    LoadGame();
                    MainLoop();
                }
                catch
                {
                    MainMenu();
                }
                
            }
            else if (choice == null)
            {
                Terminal.Close();
            }
        }

        static void Main(string[] args)
        {
            Initialize();
            MainMenu();
        }
    }
}
