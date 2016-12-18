using System;
namespace GLEngine
{
	public class StaticShader: ShaderProgram
	{
		const string VERTEX_FILE = "shaders/vertexShader";
		const string FRAGMENT_FILE = "shaders/fragmentShader";

		public StaticShader() : base(VERTEX_FILE, FRAGMENT_FILE) { }

		protected override void BindAttributes()
		{
			base.BindAttribute(0, "position");
			base.BindAttribute(1, "textureCoords");
		}
	}
}
