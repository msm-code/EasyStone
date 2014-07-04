using Tao.FreeGlut;
using Tao.OpenGl;
using Msm.Geometry;
using EasyStone.Menus;
using EasyStone.Engine;

namespace EasyStone
{
    class Program
    {
        static IGameState gameState;
        static int wndId;

        static void Main(string[] args)
        {
            SetState(new Menu());
            InitGlut();
        }

        static void SetState(IGameState newState)
        {
            if (newState.GetType() == typeof(ExitGame))
            {
                Glut.glutDestroyWindow(wndId);
            }

            gameState = newState;
            gameState.GameStateChanged += (sender, e) => { SetState(e.NewState); };
        }

        static void InitGlut()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_RGBA | Glut.GLUT_DEPTH);

            Glut.glutInitWindowSize(800, 800);
            Glut.glutInitWindowPosition(200, 200);
            wndId = Glut.glutCreateWindow("Welcome!");

            Glut.glutDisplayFunc(Render);
            Glut.glutMouseFunc(Mouse);
            Glut.glutMotionFunc(MouseMoveFunc);
            Glut.glutKeyboardFunc(KeyDownFunc);
            Glut.glutKeyboardUpFunc(KeyUpFunc);
            Glut.glutMainLoop();
        }

        static void Mouse(int button, int state, int x, int y)
        {
            gameState.MouseClick(button, state, x, y);
        }

        static void MouseMoveFunc(int x, int y)
        {
            gameState.MouseMove(x, y);
        }

        static void KeyDownFunc(byte code, int x, int y)
        {
            gameState.KeyDown(code);
        }

        static void KeyUpFunc(byte code, int x, int y)
        {
            gameState.KeyUp(code);
        }

        private static int lastTime;
        static int[] times = new int[100000];
        static int ndx = 0;
        static void Render()
        {
            int time = Glut.glutGet(Glut.GLUT_ELAPSED_TIME);
            float delta = (time - lastTime) / 1000.0f;

            gameState.Update(delta);
            GlobalInterface.Update(delta);

            lastTime = time;
            times[ndx++] = time;

            Renderer.Instance.BeginScene();
            gameState.Redraw();
            GlobalInterface.Redraw();
            Renderer.Instance.EndScene();

            Glut.glutPostRedisplay();
        }
    }
}
