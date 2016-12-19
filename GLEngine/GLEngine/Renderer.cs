using System;
using OpenTK.Graphics.OpenGL;
namespace GLEngine
{
	public class Renderer
	{
		public void Prepare()
		{

			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.ClearColor(0.0f, 0.5f, 0.5f, 1.0f);
	
		}

		public void Render(TexturedModel texturedModel)
		{
			GL.Enable(EnableCap.Texture2D);
			RawModel model = texturedModel.GetRawModel();
			GL.BindVertexArray(1);

			GL.EnableVertexAttribArray(0);
			GL.EnableVertexAttribArray(1);
			GL.ActiveTexture(TextureUnit.Texture0);

            GL.BindTexture(TextureTarget.Texture2D, texturedModel.GetTexture().GetID());
            //GL.BindTexture(TextureTarget.Texture2D,0);
            //Console.WriteLine(texturedModel.GetTexture().GetID());

			GL.DrawElements( BeginMode.Triangles, model.GetVertexCount(),  DrawElementsType.UnsignedInt, 0);
			GL.DisableVertexAttribArray(0);
			GL.DisableVertexAttribArray(1);
			GL.BindVertexArray(0);
			GL.Disable(EnableCap.Texture2D);
		}
	}
}
