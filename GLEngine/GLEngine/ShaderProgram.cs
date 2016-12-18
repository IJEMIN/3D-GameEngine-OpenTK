using System;
using System.Text;
using System.IO;
using OpenTK.Graphics.OpenGL4;
namespace GLEngine
{
	public abstract class ShaderProgram
	{
		private int programID;
		private int vertexShaderID;
		private int fragmentShaderID;

		public ShaderProgram(string vertexFile, string fragmentFile)
		{
			vertexShaderID = LoadShader(vertexFile, ShaderType.VertexShader);
			fragmentShaderID = LoadShader(fragmentFile, ShaderType.FragmentShader);
			programID = GL.CreateProgram();
			GL.AttachShader(programID, vertexShaderID);
			GL.AttachShader(programID, fragmentShaderID);
			BindAttributes();
			GL.LinkProgram(programID);
			GL.ValidateProgram(programID);
		}

		public void Start()
		{
			GL.UseProgram(programID);
		}

		public void Stop()
		{
			GL.UseProgram(0);
		}

		public void CleanUp()
		{
			Stop();
			GL.DetachShader(programID, vertexShaderID);
			GL.DetachShader(programID, fragmentShaderID);
			GL.DeleteShader(vertexShaderID);
			GL.DeleteShader(fragmentShaderID);
			GL.DeleteProgram(programID);
		}

		protected abstract void BindAttributes();

		protected void BindAttribute(int attribute, string variableName)
		{
			GL.BindAttribLocation(programID, attribute, variableName);
		}

		private static int LoadShader(string file, ShaderType shaderType)
		{
			StringBuilder shaderSource = new StringBuilder();


			StreamReader reader = new StreamReader(file);


			string line = string.Empty;

			while (!reader.EndOfStream)
			{
				line = reader.ReadLine();

				if (String.IsNullOrEmpty(line))
				{

					shaderSource.Append(line);
				}
			}


			reader.Close();

			int shaderID = GL.CreateShader(shaderType);

			GL.ShaderSource(shaderID, shaderSource.ToString());
			GL.CompileShader(shaderID);

			int status;

			GL.GetShader(shaderID,ShaderParameter.CompileStatus, out status);


			if (status == (int) All.False)
			{
				Console.WriteLine("Could not compile Shader!");
				string infoLog;

				GL.GetShaderInfoLog(shaderID, out infoLog);

				Console.WriteLine(infoLog);

			}

			return shaderID;


		}
	}
}
