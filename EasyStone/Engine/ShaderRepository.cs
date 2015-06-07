using System.Collections.Generic;
using System.IO;
using Tao.OpenGl;

namespace EasyStone.Engine
{
    enum ShaderType
    {
        Vertex,
        Fragment
    }

    class ShaderRepository
    {
        private static ShaderRepository instance;

        public static ShaderRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new ShaderRepository();
                return instance;
            }
        }

        public ShaderRepository()
        {
            shaders = new Dictionary<string, int>();
        }

        Dictionary<string, int> shaders;

        public void LoadShader(string filename, string id, ShaderType type)
        {
            string text = File.ReadAllText(filename);

            int intType = (type == ShaderType.Fragment) ? Gl.GL_FRAGMENT_SHADER : Gl.GL_VERTEX_SHADER;

            int shader = Gl.glCreateShader(intType);
            string[] textRef = new string[] { text };
            Gl.glShaderSource(shader, 1, ref textRef, new int[] { 0 });
            Gl.glCompileShader(shader);

            shaders[id] = shader;
        }

        public int GetShaderId(string shaderName)
        {
            return shaders[shaderName];
        }

        public static int CompileProgram(int vertexShader, int fragmentShader)
        {
            int program = Gl.glCreateProgram();
            Gl.glAttachShader(program, vertexShader);
            Gl.glAttachShader(program, fragmentShader);

            Gl.glLinkProgram(program);

            return program;
        }
    }
}
