using EasyStone.Engine;
using Msm.Geometry;
using System;
using System.Linq;

namespace EasyStone.Menus
{
    class Menu : IGameState
    {
        ButtonCollection buttons;
        int selectedNdx;
        bool initialized;

        public Menu()
        {
            buttons = new ButtonCollection();
            buttons.Add(new MenuButton("New Game!", () => 
            {
                if (GameStateChanged != null)
                    GameStateChanged(this, new GameStateChangedEventArgs(new Game(new Map())));
            }));
            buttons.Add(new MenuButton("Exit", () => 
            {
                if (GameStateChanged != null)
                    GameStateChanged(this, new GameStateChangedEventArgs(new ExitGame())); 
            }));
            buttons.FixPositions();
        }
        
        public void Update(float delta)
        {
            if (!initialized) {
                GlobalInterface.AddInfoBox("Welcome to EASY STONE");
                GlobalInterface.AddInfoBox("WSAD to move");
                GlobalInterface.AddInfoBox("Mouse to look");
                GlobalInterface.AddInfoBox("Enter to select option");
                GlobalInterface.AddInfoBox("1-6 to select weapon");
                initialized = true;
            }
        }

        public void Redraw()
        {
            IRectangleSurface surface = Renderer.Instance.CreateRectangleSurface();

            for (int i = 0; i < buttons.Count; i++)
            {
                Color4 color;
                if (i == selectedNdx)
                    color = new Color4(230, 120, 250);
                else
                    color = new Color4(200, 220, 255);

                surface.DrawRectangle(buttons[i].Position, buttons[i].Size, color);
            }
            Renderer.Instance.CommitSurface(surface);

            for (int i = 0; i < buttons.Count; i++)
            {
                Renderer.Instance.BlitText(buttons[i].Position.X + 1, buttons[i].Position.Y + 1, Tao.FreeGlut.Glut.GLUT_BITMAP_HELVETICA_18,
                buttons[i].Text, Color4.Black);
            }
        }

        public void KeyDown(byte code)
        {
            if (code == 's')
                if (selectedNdx + 1 < buttons.Count)
                    selectedNdx++;
            if (code == 'w')
                if (selectedNdx > 0)
                    selectedNdx--;

            if (code == '\r')
            {
                buttons[selectedNdx].Action();
            }
        }

        public void KeyUp(byte code)
        {
        }

        public void MouseClick(int button, int state, int x, int y)
        {
        }

        public void MouseMove(int x, int y)
        {
        }

        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;
    }
}
