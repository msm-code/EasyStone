using Msm.Geometry;
namespace EasyStone.Engine
{
    struct Polygon
    {
        private int id;
        private float rotation;
        private Vector2 position;
        private float size;

        public Polygon(int id)
            :this()
        {
            this.id = id;
            this.size = 1;
        }

        public void Rotate(float angle)
        {
            rotation += angle;
        }

        public void Move(Vector2 offset)
        {
            this.position += offset;
        }

        public void Resize(float scale)
        {
            this.size *= scale;
        }

        public int Id { get {return id;} }
        public float Rotation { get { return rotation; } }
        public Vector2 Position { get { return position; } }
        public float Size { get { return size; } }
    }
}
