using Msm.Geometry;
using System;

namespace EasyStone.Menus
{
    class MenuButton
    {
        public MenuButton(string text, Action action)
        {
            this.Text = text;
            this.Action = action;
        }

        public Action Action { get; set; }
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
    }
}
