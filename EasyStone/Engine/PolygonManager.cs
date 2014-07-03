using Tao.OpenGl;
using System;
using System.Collections.Generic;
using Msm.Geometry;

namespace EasyStone.Engine
{
    public enum ShapeType
    {
        Rectangle,
        Triangle,
        Ship,
        Octagon,
        Armor
    }

    class PolygonManager
    {
        #region Singleton
        private static PolygonManager instance;

        public static PolygonManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new PolygonManager();
                return instance;
            }
        }
        #endregion

        private Dictionary<ShapeType, Polygon> polygons;
        private Dictionary<ShapeType, double[][]> verticles;
        private Dictionary<ShapeType, double[][]> textures;

        private PolygonTesselator tesselator;

        private PolygonManager()
        {
            tesselator = new PolygonTesselator();
            polygons = new Dictionary<ShapeType, Polygon>();
            verticles = new Dictionary<ShapeType, double[][]>();
            textures = new Dictionary<ShapeType, double[][]>();

            InitVerticles();
        }

        private void InitVerticles()
        {
            double[][] rect = new double[4][];
            rect[0] = new double[] { -0.5, -0.5, 0 };
            rect[1] = new double[] { -0.5, 0.5, 0 };
            rect[2] = new double[] { 0.5, 0.5, 0 };
            rect[3] = new double[] { 0.5, -0.5, 0 };

            double[][] triangle = new double[3][];
            triangle[0] = new double[] { -0.6, 0.5, 0 };
            triangle[1] = new double[] { 0.6, 0.5, 0 };
            triangle[2] = new double[] { 0, -0.5, 0 };

            double[][] ship = new double[4][];
            ship[0] = new double[] { -0.7, 0.5, 0 };
            ship[1] = new double[] { 0, 0, 0 };
            ship[2] = new double[] { 0.7, 0.5, 0 };
            ship[3] = new double[] { 0, -0.8, 0 };

            double[][] octagon = new double[8][];
            octagon[0] = new double[] { -0.5, +0.2, 0 };
            octagon[1] = new double[] { -0.2, +0.5, 0 };
            octagon[2] = new double[] { +0.2, +0.5, 0 };
            octagon[3] = new double[] { +0.5, +0.2, 0 };
            octagon[4] = new double[] { +0.5, -0.2, 0 };
            octagon[5] = new double[] { +0.2, -0.5, 0 };
            octagon[6] = new double[] { -0.2, -0.5, 0 };
            octagon[7] = new double[] { -0.5, -0.2, 0 };

            double[][] armor = new double[4][];
            armor[0] = new double[] { -0.5, -0.2, 0 };
            armor[1] = new double[] {  0.5, -0.2, 0 };
            armor[2] = new double[] {  0.45, 0.2, 0 };
            armor[3] = new double[] { -0.45, 0.2, 0 };

            double[][] rectTextures = new double[4][];
            rectTextures[0] = new double[] { 0, 0 };
            rectTextures[1] = new double[] { 0, 1 };
            rectTextures[2] = new double[] { 1, 1 };
            rectTextures[3] = new double[] { 1, 0 };

            double[][] triTextures = new double[3][];
            triTextures[0] = new double[] { 0, 0 };
            triTextures[1] = new double[] { 1, 1 };
            triTextures[2] = new double[] { 1, 0 };

            double[][] octagonTextures = new double[8][];
            octagonTextures[0] = new double[] { 0, 0.7};
            octagonTextures[1] = new double[] { 0.7, 1 };
            octagonTextures[2] = new double[] { 0.7,1 };
            octagonTextures[3] = new double[] { 1, 0.7 };
            octagonTextures[4] = new double[] { 1, 0.3 };
            octagonTextures[5] = new double[] { 0.7, 0 };
            octagonTextures[6] = new double[] { 0.3, 0 };
            octagonTextures[7] = new double[] { 0, 0.3 };

            verticles[ShapeType.Rectangle] = rect;
            verticles[ShapeType.Triangle] = triangle;
            verticles[ShapeType.Ship] = ship;
            verticles[ShapeType.Octagon] = octagon;
            verticles[ShapeType.Armor] = armor;

            textures[ShapeType.Rectangle] = rectTextures;
            textures[ShapeType.Triangle] = triTextures;
            textures[ShapeType.Ship] = rectTextures;
            textures[ShapeType.Octagon] = octagonTextures;
            textures[ShapeType.Armor] = rectTextures;
        }

        // TODO (OPTIONAL): Możnaby rozdzielić na metody GET (zwracającą czysty polygon) i GET_TEXTURED - 
        // która zwracałaby polygon oteksturowany, minimalnie wolniejszy.
        // Ale na razie niech jest jak jest.
        public Polygon Get(ShapeType type, Vector2 position, float size, float rotation)
        {
            if (!polygons.ContainsKey(type))
            {
                double[][] verts = verticles[type];
                double[][] texs = textures[type];

                int id = tesselator.TesselateTextured(verts, texs, verts.GetLength(0));

                Polygon poly = new Polygon(id);

                polygons.Add(type, poly);
            }

            Polygon result = polygons[type];
            result.Move(position);
            result.Resize(size);
            result.Rotate(rotation);

            return result;
        }
    }
}
