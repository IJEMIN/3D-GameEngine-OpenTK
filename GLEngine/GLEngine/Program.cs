using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK;

namespace GLEngine
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			GameWindow window = new GameWindow();
			DisplayManager displayer = new DisplayManager(window);

			window.Run();
		}
	}
}
