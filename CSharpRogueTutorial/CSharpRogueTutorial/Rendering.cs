using BearLib;
using RogueTutorial;

namespace CSharpRogueTutorial
{
    class Rendering
    {
        private static void DrawMap()
        {
            for (int x = 0; x < Constants.MapWidth; x++)
            {
                for (int y = 0; y < Constants.MapHeight; y++)
                {
                    if (Rogue.GameWorld.Map.tiles[x,y].blocked)
                    {
                        Terminal.PutExt(x, y, 0, 0, '#');
                    }
                    else
                    {
                        Terminal.PutExt(x, y, 0, 0, '.');
                    }
                }
            }
        }

        private static void DrawObjects()
        {
            foreach (GameObject obj in Rogue.GameWorld.Objects)
            {
                obj.Draw();
            }
        }

        public static void RenderAll()
        {
            Terminal.Clear();

            DrawMap();

            DrawObjects();

            Terminal.Refresh();
        }
    }
}
