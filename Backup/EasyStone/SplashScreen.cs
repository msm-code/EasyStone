using System;
using EasyStone.Engine;
using Tao.FreeGlut;
using Msm.Geometry;
using EasyStone.Menus;
namespace EasyStone
{
    class SplashScreen : IGameState
    {
        public void Update(float delta)
        { }

        public void Redraw()
        {
            Renderer.Instance.BlitText(0, 0, Glut.GLUT_BITMAP_TIMES_ROMAN_24, "lolololololl", Color4.Black);
        }

        public void KeyDown(byte code)
        {
            if (GameStateChanged != null)
                GameStateChanged(this, new GameStateChangedEventArgs(new Menu()));
        }

        public void KeyUp(byte code)
        { }

        public void MouseClick(int button, int state, int x, int y)
        { }

        public void MouseMove(int x, int y)
        { }

        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;
    }
}
