using System;
namespace EasyStone
{
    class GameStateChangedEventArgs : EventArgs
    {
        public GameStateChangedEventArgs(IGameState newState)
        {
            this.NewState = newState;
        }

        public IGameState NewState { get; set; }
    }

    interface IGameState
    {
        void Update(float delta);
        void Redraw();

        void KeyDown(byte code);
        void KeyUp(byte code);
        void MouseClick(int button, int state, int x, int y);
        void MouseMove(int x, int y);

        event EventHandler<GameStateChangedEventArgs> GameStateChanged;
    }
}
