using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace GAM531.Assignment_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 600),
                Title = "Texture Mapping Assignment - OpenTK",
                Flags = ContextFlags.ForwardCompatible,
            };

            using (var window = new TexturedCube(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}