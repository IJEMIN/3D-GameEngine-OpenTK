using System;
using System.Drawing;
using System.Collections.Generic;
//using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform.MacOS;
using OpenTK;
using System.Drawing.Imaging;




namespace GLEngine
{

	public class Loader
	{

		// Storing IDs for vao,vbo,texture
		private List<int> vaos = new List<int>();
		private List<int> vbos = new List<int>();
		private List<int> textures = new List<int>();

		public RawModel LoadToVAO(float[] positions, float[] textureCoords, int[] indices)
		{


			int vaoID = CreateVAO();

			BindIndicesBuffer(indices);

			StoreDataInAttributeList(0, 3,positions);
			StoreDataInAttributeList(1, 2, textureCoords);

			UnbindVAO();

			return new RawModel(vaoID,indices.Length);

		}

		public int LoadTexture(string fileName)
		{
			
			Bitmap bitmap = new Bitmap( fileName + ".jpg");

			int textureID;

			GL.GenTextures(1, out textureID);


			GL.BindTexture(TextureTarget.Texture2D, textureID);


			BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
			                                  ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, data.Width,data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgb, PixelType.UnsignedByte,data.Scan0);

			bitmap.UnlockBits(data);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			textures.Add(textureID);

			return textureID;
		}

		public int CreateVAO()
		{
			int id;
			GL.GenVertexArrays(1,out id);

			GL.BindVertexArray(id);
			vaos.Add(id);
			return id;

		}

		public void CleanUp()
		{
			foreach (int vao in vaos)
			{
				GL.DeleteVertexArray(vao);
			}

			foreach (int vbo in vbos)
			{
				GL.DeleteBuffer(vbo);
			}

			foreach (int texture in textures)
			{
				GL.DeleteTexture(texture);
			}
		}

		private void StoreDataInAttributeList(int attributeNumber, int coordinateSize, float[] data)
		{
			int vboID;
			GL.GenBuffers(1, out vboID);
			vbos.Add(vboID);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);

			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * sizeof(float)) ,data, BufferUsageHint.StaticDraw);

			GL.VertexAttribPointer(attributeNumber, coordinateSize,VertexAttribPointerType.Float, false, 0, 0);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);



		}

		private void BindIndicesBuffer(int[] indices)
		{
			int vboID;
			GL.GenBuffers(1,out vboID);
			vbos.Add(vboID);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboID);

			GL.BufferData(BufferTarget.ElementArrayBuffer,(IntPtr)(indices.Length * sizeof(int)), indices, BufferUsageHint.StaticDraw);
		}

		private void UnbindVAO()
		{
			GL.BindVertexArray(0);
		}
	}
}