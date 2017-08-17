using BearLib;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRogueTutorial
{
    class Menu
    {
        private static int HeaderY = 12;

        private static void ClearMainRectangle()
        {
            Terminal.Layer(Constants.Layers["Map"]);
            Terminal.ClearArea(1, 1, 62, 37);
            Terminal.Layer(Constants.Layers["MapFeatures"]);
            Terminal.ClearArea(1, 1, 62, 37);
            Terminal.Layer(Constants.Layers["Player"]);
            Terminal.ClearArea(1, 1, 62, 37);
            Terminal.Layer(Constants.Layers["Monsters"]);
            Terminal.ClearArea(1, 1, 62, 37);
            Terminal.Layer(Constants.Layers["Items"]);
            Terminal.ClearArea(1, 1, 62, 37);
            Terminal.Layer(Constants.Layers["Messages"]);
            Terminal.ClearArea(1, 1, 62, 37);
        }

        public static int? Inventory(string header, List<GameObject> options, string exitText)
        {
            ClearMainRectangle();

            int selector = 0;

            Terminal.Print(5, HeaderY, header);

            while (true)
            {
                int y = HeaderY + 3;

                foreach (GameObject option in options)
                {
                    if (option.Item.Equipment != null)
                    {
                        if (option.Item.Equipment.IsEquipped)
                        {
                            Terminal.Print(5, y, "( ) " + option.Name + " (Equipped)");
                        }
                        else
                        {
                            Terminal.Print(5, y, "( ) " + option.Name);
                        }
                        
                    }
                    else
                    {
                        Terminal.Print(5, y, "( ) " + option.Name + " (x" + option.Item.Count.ToString() + ")");
                    }

                    y += 1;
                }

                Terminal.Print(5, y + 1, "( ) " + exitText);

                if (selector == options.Count())
                    Terminal.Print(6, HeaderY + 3 + selector + 1, "*");
                else
                    Terminal.Print(6, HeaderY + 3 + selector, "*");

                Terminal.Refresh();

                while (true)
                {
                    int key = Terminal.Read();

                    if (Controls.DownMovement.Contains(key))
                    {
                        if (selector == options.Count()) selector = 0;
                        else selector += 1;
                        break;
                    }
                    else if (Controls.UpMovement.Contains(key))
                    {
                        if (selector == 0) selector = options.Count();
                        else selector -= 1;
                        break;
                    }
                    else if (Controls.ActionKeys.Contains(key))
                    {
                        if (selector == options.Count()) return null;
                        return selector;
                    }
                    else if (Controls.EscapeKeys.Contains(key))
                    {
                        return null;
                    }
                }
            }
        }

        public static int? ItemMenu(string header, List<string> options, string exitText)
        {
            ClearMainRectangle();

            int selector = 0;

            Terminal.Print(5, HeaderY, header);

            while (true)
            {
                int y = HeaderY + 3;

                foreach (string option in options)
                {
                    Terminal.Print(5, y, "( ) " + option);

                    y += 1;
                }

                Terminal.Print(5, y + 1, "( ) " + exitText);

                if (selector == options.Count())
                    Terminal.Print(6, HeaderY + 3 + selector + 1, "*");
                else
                    Terminal.Print(6, HeaderY + 3 + selector, "*");

                Terminal.Refresh();

                while (true)
                {
                    int key = Terminal.Read();

                    if (Controls.DownMovement.Contains(key))
                    {
                        if (selector == options.Count()) selector = 0;
                        else selector += 1;
                        break;
                    }
                    else if (Controls.UpMovement.Contains(key))
                    {
                        if (selector == 0) selector = options.Count();
                        else selector -= 1;
                        break;
                    }
                    else if (Controls.ActionKeys.Contains(key))
                    {
                        if (selector == options.Count()) return null;
                        return selector;
                    }
                    else if (Controls.EscapeKeys.Contains(key))
                    {
                        return null;
                    }
                }
            }
        }

        public static int? BasicMenu(string header, List<string> options, string exitText)
        {
            Terminal.Clear();

            int selector = 0;

            Terminal.Print(5, HeaderY, header);

            while (true)
            {
                int y = HeaderY + 3;

                foreach (string option in options)
                {
                    Terminal.Print(5, y, "( ) " + option);

                    y += 1;
                }

                Terminal.Print(5, y + 1, "( ) " + exitText);

                if (selector == options.Count())
                    Terminal.Print(6, HeaderY + 3 + selector + 1, "*");
                else
                    Terminal.Print(6, HeaderY + 3 + selector, "*");

                Terminal.Refresh();

                while (true)
                {
                    int key = Terminal.Read();

                    if (Controls.DownMovement.Contains(key))
                    {
                        if (selector == options.Count()) selector = 0;
                        else selector += 1;
                        break;
                    }
                    else if (Controls.UpMovement.Contains(key))
                    {
                        if (selector == 0) selector = options.Count();
                        else selector -= 1;
                        break;
                    }
                    else if (Controls.ActionKeys.Contains(key))
                    {
                        if (selector == options.Count()) return null;
                        return selector;
                    }
                    else if (Controls.EscapeKeys.Contains(key))
                    {
                        return null;
                    }
                }
            }
        }
    }
}
