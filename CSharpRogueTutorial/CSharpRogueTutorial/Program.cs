﻿using CSharpRogueTutorial;
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
            Terminal.Set("window: size=" + Constants.ScreenWidth.ToString() + "x" + Constants.ScreenHeight.ToString() + "; font: MainFont.png, size=16x16; input.filter={keyboard, mouse}");
            Terminal.Set("0xE000: PlayerTileset.png, size=64x64");
            Terminal.Set("0xE100: UI.png, size=16x16");
            Terminal.Set("0xE200: TerrainTileset.png, size=64x64");
            Terminal.Set("0xE300: ItemTileset.png, size=64x64");
            Terminal.Set("0xE400: EnemyTileset.png, size=64x64");
            Terminal.Set("0xE500: ObjectTileset.png, size=64x64");
            Terminal.Set("0xE600: EffectTileset.png, size=64x64");

            PreCalcFov();
            AddLayers();
        }

        private static void AddLayers()
        {
            Constants.Layers.Add("Map", 0);
            Constants.Layers.Add("MapFeatures", 1);
            Constants.Layers.Add("Items", 2);
            Constants.Layers.Add("Monsters", 3);
            Constants.Layers.Add("Player", 4);
            Constants.Layers.Add("Spells", 5);
            Constants.Layers.Add("UI", 6);
            Constants.Layers.Add("Messages", 7);
        }

        private static void PreCalcFov()
        {
            for (int i = -720; i < 721; i += Constants.FoVSteps)
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

            GameWorld.Player = new GameObject("Player", Tiles.Player.PlayerTile, 0, 0);
            GameWorld.Objects.Add(GameWorld.Player);

            GameWorld.Player.Fighter = new Fighter(GameWorld.Player, 24, 4, 4, 0, Constants.AI.Player, Constants.Death.PlayerDeath);

            MapMethods.MakeMap(Constants.Direction.Down);
            //MapMethods.MakeMaze();

            GameWorld.State = Constants.GameState.Playing;
        }

        private static void LoadGame()
        {
            GameWorld = File.LoadGame();
            
            GameWorld.State = Constants.GameState.Playing;

            MessageLog.AddMessage("Game loaded.", "white");
        }

        private static void MainLoop()
        {
            while (true)
            {
                Rendering.RenderAll();

                Constants.PlayerAction action = Controls.HandleKeys();

                if (action == Constants.PlayerAction.ExitGame || action == Constants.PlayerAction.ExitWithoutSave)
                {
                    if (GameWorld.State == Constants.GameState.Playing && action == Constants.PlayerAction.ExitGame)
                    {
                        File.SaveGame();
                    }
                    if (GameWorld.State == Constants.GameState.Playing && action == Constants.PlayerAction.ExitWithoutSave)
                    {
                        if (System.IO.File.Exists("World.bin"))
                        {
                            System.IO.File.Delete("World.bin");
                        }
                    }

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
            int? choice = Menu.BasicMenu("Main Menu", new List<string>() { "New Game", "Load Game" }, "Exit");

            if (choice == 0)
            {
                NewGame();
                MainLoop();
            }
            else if (choice == 1)
            {
                if (System.IO.File.Exists("World.bin"))
                {
                    LoadGame();
                    MainLoop();
                }
                else
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
