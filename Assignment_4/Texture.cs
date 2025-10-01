using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace GAM531.Assignment_4
{
    public class Texture : IDisposable
    {
        private int handle;
        private bool disposed = false;

        public Texture(string path)
        {
            handle = GL.GenTexture();
            Use();

            // Set texture parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            // Load texture
            if (File.Exists(path))
            {
                LoadFromFile(path);
            }
            else
            {
                Console.WriteLine($"Texture file not found: {path}. Using procedural texture.");
                GenerateProceduralTexture();
            }
        }

        private void LoadFromFile(string path)
        {
            // Flip image vertically for OpenGL
            StbImage.stbi_set_flip_vertically_on_load(1);

            // Load image
            ImageResult image = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);

            // Upload to GPU
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            Console.WriteLine($"Texture loaded: {path} ({image.Width}x{image.Height})");
        }

        private void GenerateProceduralTexture()
        {
            int width = 256;
            int height = 256;
            byte[] pixels = GenerateCheckerboard(width, height);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            Console.WriteLine($"Generated procedural texture ({width}x{height})");
        }

        private byte[] GenerateCheckerboard(int width, int height)
        {
            byte[] pixels = new byte[width * height * 4];
            int checkSize = 32;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = (y * width + x) * 4;
                    bool isWhite = (x / checkSize + y / checkSize) % 2 == 0;

                    if (isWhite)
                    {
                        // White with slight blue tint
                        pixels[index] = 240;     // R
                        pixels[index + 1] = 240; // G
                        pixels[index + 2] = 255; // B
                    }
                    else
                    {
                        // Dark blue
                        pixels[index] = 40;      // R
                        pixels[index + 1] = 40;  // G
                        pixels[index + 2] = 80;  // B
                    }
                    pixels[index + 3] = 255;     // A
                }
            }

            return pixels;
        }

        public void Use(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, handle);
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
                GL.DeleteTexture(handle);
                disposed = true;
            }
        }
    }
}