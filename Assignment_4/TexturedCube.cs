using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace GAM531.Assignment_4
{
    public class TexturedCube : GameWindow
    {
        private Cube cube;
        private Shader shader;
        private Texture texture;
        private Camera camera;

        private float rotationAngle = 0.0f;
        private bool enableAnimation = true;

        public TexturedCube(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            // Initialize components
            cube = new Cube();
            shader = new Shader("Shaders/vertex.glsl", "Shaders/fragment.glsl");

            // Try multiple texture file names
            string[] textureFiles = { "texture.jpg", "texture.png", "Textures/texture.jpg", "Textures/texture.png" };
            string textureToLoad = "texture.jpg";

            foreach (var file in textureFiles)
            {
                if (File.Exists(file))
                {
                    textureToLoad = file;
                    Console.WriteLine($"Found texture file: {file}");
                    break;
                }
            }

            texture = new Texture(textureToLoad);
            camera = new Camera(new Vector3(0, 0, 3), Size.X / (float)Size.Y);

            // Set the texture uniform
            shader.Use();
            shader.SetInt("texture0", 0);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Animate rotation
            if (enableAnimation)
            {
                rotationAngle += 45.0f * (float)args.Time;
            }

            // Create transformation matrices
            Matrix4 model = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotationAngle)) *
                           Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotationAngle * 0.5f));

            // Use shader and set matrices
            shader.Use();
            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());

            // Bind texture and draw
            texture.Use(TextureUnit.Texture0);
            cube.Draw();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Toggle animation with spacebar
            if (KeyboardState.IsKeyPressed(Keys.Space))
            {
                enableAnimation = !enableAnimation;
                Console.WriteLine($"Animation: {(enableAnimation ? "ON" : "OFF")}");
            }

            // Exit with Escape
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            camera.AspectRatio = Size.X / (float)Size.Y;
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            cube?.Dispose();
            shader?.Dispose();
            texture?.Dispose();
        }
    }
}