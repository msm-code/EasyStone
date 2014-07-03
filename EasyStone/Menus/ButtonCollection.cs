using Msm.Geometry;
using System.Collections.Generic;

namespace EasyStone.Menus
{
    class ButtonCollection
    {
        List<MenuButton> buttons;

        public ButtonCollection()
        {
            buttons = new List<MenuButton>();
        }

        public void Add(MenuButton b)
        {
            buttons.Add(b);
        }

        public void FixPositions()
        {
            Vector2 position = new Vector2(-10, 12);

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Position = position;
                buttons[i].Size = new Vector2(20, 3);
                position.Y -= 5;
            }
        }

        public int Count
        { get { return buttons.Count; } }

        public MenuButton this[int ndx]
        {
            get { return buttons[ndx]; }
        }
    }
}
