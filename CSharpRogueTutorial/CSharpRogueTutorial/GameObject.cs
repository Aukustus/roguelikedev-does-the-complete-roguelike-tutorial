using BearLib;
using RogueSharp;
using RogueTutorial;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSharpRogueTutorial
{
    [Serializable]
    class GameObject
    {
        public char Tile;
        public int X;
        public int Y;
        public bool Blocks;
        public int CameraX = 0;
        public int CameraY = 0;
        public string Name;
        public bool AlwaysVisible;

        public int OffsetX = 0;
        public int OffsetY = 0;

        public Fighter Fighter = null;
        public Item Item = null;
        public bool Spell = false;

        public bool Downstairs = false;
        public bool Upstairs = false;

        public GameObject(string name, char tile, int x, int y, bool blocks = true)
        {
            Name = name;
            X = x;
            Y = y;
            Tile = tile;
            Blocks = blocks;
            AlwaysVisible = false;
        }

        internal void Draw(string color, bool movingObject = false)
        {
            if (this == Rogue.GameWorld.Player)
                Terminal.Layer(Constants.Layers["Player"]);
            else if (Fighter != null)
                Terminal.Layer(Constants.Layers["Monsters"]);
            else if (Spell != false)
                Terminal.Layer(Constants.Layers["Spells"]);
            else if (Downstairs || Upstairs)
                Terminal.Layer(Constants.Layers["MapFeatures"]);
            else
                Terminal.Layer(Constants.Layers["Items"]);

            int drawX = (X - Rogue.GameWorld.Player.CameraX) * 4 + 5;
            int drawY = (Y - Rogue.GameWorld.Player.CameraY) * 4 + 5;

            Terminal.Color(Terminal.ColorFromName(color));

            int offsetX = -40;
            int offsetY = -40;

            if (movingObject == true)
            {
                offsetX += OffsetX;
                offsetY += OffsetY;
            }
            else
            {
                if (this != Rogue.GameWorld.Player)
                {
                    offsetX -= Rogue.GameWorld.Player.OffsetX;
                    offsetY -= Rogue.GameWorld.Player.OffsetY;
                }
            }

            Terminal.Print(drawX, drawY, "[offset=" + offsetX.ToString() + "," + offsetY.ToString() + "]" + Tile.ToString());
        }

        internal void PlayerMoveOrAttack(int dx, int dy)
        {
            int targetX = X + dx;
            int targetY = Y + dy;

            if (GameMap.MapBlocked(X + dx, Y + dy))
            {
                return;
            }

            foreach (GameObject obj in Rogue.GameWorld.Objects)
            {
                if (obj.X == targetX && obj.Y == targetY && obj.Fighter != null && obj.Fighter.AI.Type != Constants.AI.None)
                {
                    Fighter.MeleeAttack(obj);
                    return;
                }
            }

            Move(dx, dy);
        }

        internal void Move(int dx, int dy)
        {
            if (this != Rogue.GameWorld.Player)
            {
                if (dx == 0 && dy == -1)
                {
                    if (Fighter.Direction != 0)
                    {
                        Fighter.Direction = 0;
                        return;
                    }
                }
                if (dx == -1 && dy == -1)
                {
                    if (Fighter.Direction != 45)
                    {
                        Fighter.Direction = 45;
                        return;
                    }
                }
                if (dx == -1 && dy == 0)
                {
                    if (Fighter.Direction != 90)
                    {
                        Fighter.Direction = 90;
                        return;
                    }
                }
                if (dx == -1 && dy == 1)
                {
                    if (Fighter.Direction != 135)
                    {
                        Fighter.Direction = 135;
                        return;
                    }
                }
                if (dx == 0 && dy == 1)
                {
                    if (Fighter.Direction != 180)
                    {
                        Fighter.Direction = 180;
                        return;
                    }
                }
                if (dx == 1 && dy == 1)
                {
                    if (Fighter.Direction != 225)
                    {
                        Fighter.Direction = 225;
                        return;
                    }
                }
                if (dx == 1 && dy == 0)
                {
                    if (Fighter.Direction != 270)
                    {
                        Fighter.Direction = 270;
                        return;
                    }
                }
                if (dx == 1 && dy == -1)
                {
                    if (Fighter.Direction != 315)
                    {
                        Fighter.Direction = 315;
                        return;
                    }
                }
            }

            if (!GameMap.Blocked(X + dx, Y + dy))
            {
                if (this == Rogue.GameWorld.Player || FoV.InFov(Rogue.GameWorld.Player.X, Rogue.GameWorld.Player.Y, X, Y, Rogue.GameWorld.Player))
                {
                    if (dx == 1 || dx == -1)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            OffsetX += Constants.MoveSmoothSteps * dx;
                            if (this != Rogue.GameWorld.Player)
                            {
                                Rendering.RenderAll(this);
                                Draw("white", true);
                                Terminal.Refresh();
                            }
                            else
                            {
                                Rendering.RenderAll();
                            }
                        }

                        OffsetX = 0;
                    }
                    if (dy == 1 || dy == -1)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            OffsetY += Constants.MoveSmoothSteps * dy;
                            if (this != Rogue.GameWorld.Player)
                            {
                                Rendering.RenderAll(this);
                                Draw("white", true);
                                Terminal.Refresh();
                            }
                            else
                            {
                                Rendering.RenderAll();
                            }
                        }

                        OffsetY = 0;
                    }

                    if (this == Rogue.GameWorld.Player)
                    {
                        Camera.HandleMoveCamera(dx, dy);
                    }
                }

                X += dx;
                Y += dy;
            }
        }

        internal void SendToBack()
        {
            Rogue.GameWorld.Objects.Remove(this);
            Rogue.GameWorld.Objects.Insert(0, this);
        }

        internal void MoveTowards(int targetX, int targetY)
        {
            IMap pathMap = new Map(Constants.MapWidth, Constants.MapHeight);

            for (int x = 0; x < Constants.MapWidth; x++)
            {
                for (int y = 0; y < Constants.MapHeight; y++)
                {
                    if (!Rogue.GameWorld.Map.Tiles[x, y].Blocked)
                    {
                        pathMap.SetCellProperties(x, y, true, true);
                    }
                }
            }

            foreach (GameObject obj in Rogue.GameWorld.Objects)
            {
                if (obj.Blocks)
                {
                    pathMap.SetCellProperties(obj.X, obj.Y, false, false);
                }
            }

            pathMap.SetCellProperties(X, Y, true, true);
            pathMap.SetCellProperties(targetX, targetY, true, true);

            PathFinder pFinder = new PathFinder(pathMap);

            RogueSharp.Path path = pFinder.TryFindShortestPath(pathMap.GetCell(X, Y), pathMap.GetCell(targetX, targetY));

            if (path != null)
            {
                ICell first = path.TryStepForward();

                if (first != null)
                {
                    Move(first.X - X, first.Y - Y);
                }
            }

            path = null;

            return;
        }

        internal GameObject Clone()
        {
            IFormatter formatter = new BinaryFormatter();

            using (Stream stream = new MemoryStream())
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (GameObject)formatter.Deserialize(stream);
            }
        }
    }
}
