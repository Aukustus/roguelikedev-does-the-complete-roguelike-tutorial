using BearLib;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRogueTutorial
{
    class Menu
    {
        private static int HeaderY = 12;

        public static int? Inventory(string header, List<GameObject> options, string exitText)
        {
            Terminal.Clear();

            int selector = 0;

            Terminal.Print(5, HeaderY, header);

            while (true)
            {
                int y = HeaderY + 2;

                foreach (GameObject option in options)
                {

                    Terminal.Print(5, y, "( ) " + option.Name + " (x" + option.Item.Count.ToString() + ")");

                    y += 1;
                }

                Terminal.Print(5, y + 1, "( ) " + exitText);

                if (selector == options.Count())
                    Terminal.Print(6, HeaderY + 2 + selector + 1, "*");
                else
                    Terminal.Print(6, HeaderY + 2 + selector, "*");

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
            Terminal.Clear();

            int selector = 0;

            Terminal.Print(5, HeaderY, header);

            while (true)
            {
                int y = HeaderY + 2;

                foreach (string option in options)
                {

                    Terminal.Print(5, y, "( ) " + option);

                    y += 1;
                }

                Terminal.Print(5, y + 1, "( ) " + exitText);

                if (selector == options.Count())
                    Terminal.Print(6, HeaderY + 2 + selector + 1, "*");
                else
                    Terminal.Print(6, HeaderY + 2 + selector, "*");

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
                int y = HeaderY + 2;

                foreach (string option in options)
                {

                    Terminal.Print(5, y, "( ) " + option);

                    y += 1;
                }

                Terminal.Print(5, y + 1, "( ) " + exitText);

                if (selector == options.Count())
                    Terminal.Print(6, HeaderY + 2 + selector + 1, "*");
                else
                    Terminal.Print(6, HeaderY + 2 + selector, "*");

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
