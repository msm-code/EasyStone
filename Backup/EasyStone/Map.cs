using System.Collections.Generic;
using Tao.OpenGl;
using Msm.Geometry;
using EasyStone.Engine;

namespace EasyStone
{
    class Map
    {
        private List<DynamicObject> units;
        private List<GraphicsEffect> effects;

        public Map()
        {
            units = new List<DynamicObject>();
            effects = new List<GraphicsEffect>();
        }

        public void Add(DynamicObject unit)
        {
            units.Add(unit);

            unit.Killed += (sender, e) => units.Remove((DynamicObject)sender);
        }

        public void AddEffect(GraphicsEffect effect)
        {
            this.effects.Add(effect);
        }

        public void Update(float delta) 
        {
            for (int i = 0; i < effects.Count; i++)
            {
                effects[i].Update(delta);
            }

            for(int i = 0; i < units.Count; i++)
            {
                units[i].Update(delta);
            }

            ResolveCollisions();
        }

        private void ResolveCollisions()
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (!InBounds(units[i].Position))
                {
                    units[i].Kill();
                    i--;
                    continue;
                }

                for (int j = i + 1; j < units.Count; j++)
                {
                    Vector2 collision = units[i].CollisionForce(units[j]);
                    if (collision != Vector2.Zero)
                    {
                        DynamicObject.PerformCollision(units[i], units[j], collision);
                    }
                }
            }
        }

        public void Redraw() 
        {
            IPolygonSurface surface = Renderer.Instance.CreatePolygonSurface();

            foreach (DynamicObject obj in units)
            {
                obj.Redraw(surface);
            }

            Renderer.Instance.CommitSurface(surface);

            foreach (GraphicsEffect effect in effects)
            {
                effect.Redraw();
            }
        }

        public IEnumerable<DynamicObject> Units { get { return units; } }

        public bool InBounds(Vector2 vec)
        {
            return (vec.X > -30 && vec.X < 30 && vec.Y > -30 && vec.Y < 30);
        }
    }
}
