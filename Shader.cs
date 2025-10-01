using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace GAM531
{
    public class Shader : IDisposable
    {
        private int handle;
        private bool disposed = false;

        public Shader(string vertPath, string fragPath)
        {
            // Load shader source from files or use default
            string vertexShaderSource = LoadShaderSource(vertPath, DefaultVertexShader);
            string fragmentShaderSource = LoadShaderSource(fragPath, DefaultFragmentShader);

            // Compile shaders
            int vertexShader = CreateShader(ShaderType.VertexShader, vertexShaderSource);
            int fragmentShader = CreateShader(ShaderType.FragmentShader, fragmentShaderSource);

            // Create program
            handle = GL.CreateProgram();
            GL.AttachShader(handle, vertexShader);
            GL.AttachShader(handle, fragmentShader);
            GL.LinkProgram(handle);

            // Check for linking errors
            GL.GetProgram(handle, GetProgramParameterName.LinkStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(handle);
                throw new Exception($"Shader program linking failed: {infoLog}");
            }

            // Clean up individual shaders
            GL.DetachShader(handle, vertexShader);
            GL.DetachShader(handle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        private string LoadShaderSource(string path, string defaultSource)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                Console.WriteLine($"Shader file not found: {path}. Using default shader.");
                return defaultSource;
            }
        }

        private int CreateShader(ShaderType type, string source)
        {
            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, source);
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"{type} compilation failed: {infoLog}");
            }

            return shader;
        }

        public void Use()
        {
            GL.UseProgram(handle);
        }

        public void SetInt(string name, int value)
        {
            int location = GL.GetUniformLocation(handle, name);
            GL.Uniform1(location, value);
        }

        public void SetFloat(string name, float value)
        {
            int location = GL.GetUniformLocation(handle, name);
            GL.Uniform1(location, value);
        }

        public void SetMatrix4(string name, Matrix4 matrix)
        {
            int location = GL.GetUniformLocation(handle, name);
            GL.UniformMatrix4(location, false, ref matrix);
        }

        public void SetVector3(string name, Vector3 vector)
        {
            int location = GL.GetUniformLocation(handle, name);
            GL.Uniform3(location, vector);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                GL.DeleteProgram(handle);
                disposed = true;
            }
        }

        // Default shaders embedded in code
        private const string DefaultVertexShader = @"
            #version 330 core
            
            layout(location = 0) in vec3 aPosition;
            layout(location = 1) in vec2 aTexCoord;
            
            out vec2 texCoord;
            
            uniform mat4 model;
            uniform mat4 view;
            uniform mat4 projection;
            
            void main()
            {
                texCoord = aTexCoord;
                gl_Position = vec4(aPosition, 1.0) * model * view * projection;
            }
        ";

        private const string DefaultFragmentShader = @"
            #version 330 core
            
            in vec2 texCoord;
            out vec4 FragColor;
            
            uniform sampler2D texture0;
            
            void main()
            {
                FragColor = texture(texture0, texCoord);
            }
        ";
    }
}