using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;

namespace GAM531
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Controls:");
            Console.WriteLine("W/A/S/D - Move camera");
            Console.WriteLine("Mouse - Look around");
            Console.WriteLine("Arrow Keys - Move light");
            Console.WriteLine("R - Auto-rotate cube");
            Console.WriteLine("ESC - Exit");
            Console.WriteLine();

            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 600),
                Title = "Phong Lighting Demo - OpenTK",
                Flags = ContextFlags.ForwardCompatible,
            };
            using (var game = new Game(GameWindowSettings.Default, nativeWindowSettings))
            {
                game.Run();
            }
        }
    }
}