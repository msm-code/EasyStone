using Msm.Geometry;

namespace EasyStone.Engine
{
    interface ISurface { }

    interface ISurfaceImplementation : ISurface
    {
        void Draw();
        void Clear();
    }

    interface ILowLewelSurface : ISurface
    {
        void SetNextVertexPosition(Vector2 position, Color4 color);
    }

    interface IRectangleSurface : ISurface
    {
        void DrawRectangle(Vector2 position, Vector2 size, Color4 color);
    }

    interface ITriangleSurface : ISurface
    {
        void DrawTriangle(Vector2 p1, Vector2 p2, Vector2 p3, Color4 c1, Color4 c2, Color4 c3);
    }

    interface IPolygonSurface : ISurface
    {
        void DrawPolygon(Polygon shape, Color4 color);
        void DrawPolygon(Polygon shape, Color4 color, int texture);
    }

    interface IHighLevelSurface : ISurface
    {
        void BindTo(ISurface surface);
    }
}
