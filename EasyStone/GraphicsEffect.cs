namespace EasyStone
{
    interface GraphicsEffect
    {
        void Redraw();
        bool Update(float delta);
        void Detach();
    }
}
