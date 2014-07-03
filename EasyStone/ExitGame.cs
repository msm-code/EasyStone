namespace EasyStone
{
    class ExitGame : IGameState
    {
        public void Update(float delta)
        { throw new System.NotImplementedException(); }

        public void Redraw()
        { throw new System.NotImplementedException(); }

        public void KeyDown(byte code)
        { throw new System.NotImplementedException(); }

        public void KeyUp(byte code)
        { throw new System.NotImplementedException(); }

        public void MouseClick(int button, int state, int x, int y)
        { throw new System.NotImplementedException(); }

        public void MouseMove(int x, int y)
        { throw new System.NotImplementedException(); }

        public event System.EventHandler<GameStateChangedEventArgs> GameStateChanged
        { add { } remove { } }
    }
}
