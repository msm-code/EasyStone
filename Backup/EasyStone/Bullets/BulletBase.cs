using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Msm.Geometry;
using EasyStone.Engine;

namespace EasyStone.Bullets
{
    class BulletBase : DynamicObject
    {
        protected DynamicObject parent;

        public BulletBase(Vector2 position, Vector2 velocity, DynamicObject parent, Map world)
            : base(position, world)
        {
            this.velocity = velocity;
            this.parent = parent;
        }

        protected override void Collision(DynamicObject other, Vector2 force)
        {
            if (other != parent)
            {
                other.Hit();
                this.Hit();
            }
        }

        protected override float ObjectSize
        { get { return 0.6f; } }

        protected override Color4 Color
        {
            get { return new Color4(100, 100, 100); }
        }

        protected override int Texture
        { get { return TextureRepository.Instance.GetTexture("bullet"); } }

        public override bool IsSolid
        { get { return false; } }
    }
}
