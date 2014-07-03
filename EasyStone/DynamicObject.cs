using Msm.Geometry;
using System;
using EasyStone.Engine;

namespace EasyStone
{
    abstract class DynamicObject
    {
        protected Vector2 position;
        protected Vector2 velocity;
        protected Map world;

        public DynamicObject(Vector2 position, Map world)
        {
            this.position = position;
            this.world = world;
        }

        public void Update(float deltatime)
        {
            BeforeUpdate(deltatime);
            this.position += velocity * deltatime;
            AfterUpdate(deltatime);
        }

        protected virtual void BeforeUpdate(float delta) { }
        protected virtual void AfterUpdate(float delta) { }

        public virtual void Hit()
        {
            Kill();
        }

        public virtual void Kill()
        {
            if (Killed != null)
                OnKilled();
        }

        protected virtual void OnKilled()
        {
            Killed(this, EventArgs.Empty);
        }

        protected virtual void Collision(DynamicObject other, Vector2 force)
        { }

        public virtual bool IsSolid
        { get { return true; } }

        public void Redraw(IPolygonSurface surface) 
        {
            surface.DrawPolygon(Polygon, Color, Texture);
        }

        protected virtual int Texture
        {
            get { return TextureRepository.Instance.GetTexture("white"); }
        }

        protected virtual ShapeType ShapeType
        { get { return ShapeType.Rectangle; } }

        protected Polygon Polygon 
        { get { return PolygonManager.Instance.Get(ShapeType, Position, ObjectSize, Rotation); } }

        protected virtual float ObjectSize { get { return 1; } }

        protected abstract Color4 Color { get; }

        protected virtual float Rotation
        { get { return (float)velocity.Rotation.Degree; } }

        public Vector2 Position { get { return position; } }

        public event EventHandler<EventArgs> Killed;

        public Vector2 CollisionForce(DynamicObject other)
        {
            bool collision = (other.position - this.position).Length < (other.ObjectSize + this.ObjectSize) / (Math.PI / 2);
            if (collision) return other.position - this.position;
            else return Vector2.Zero;
        }

        public static void PerformCollision(DynamicObject obj1, DynamicObject obj2, Vector2 force)
        {
            if (obj2.IsSolid)
                obj1.Collision(obj2, force);
            if (obj1.IsSolid)
                obj2.Collision(obj1, force);
        }

        public Map World
        { get { return world; } }
    }
}
