using System;
using OpenTK.Graphics.ES30;
using OpenTK;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;


namespace GLEngine
{
	public class DisplayManager
	{

		public GameWindow window;

		private Renderer renderer;
		private StaticShader shader;
		private TexturedModel texturedModel;
		private Loader loader;

		public DisplayManager(GameWindow windowInput)
		{
			this.window = windowInput;

			window.Load += Window_Load;
			window.RenderFrame += Window_RenderFrame;
			window.UpdateFrame += Window_UpdateFrame;
			window.Closing += Window_Closing;
		}

		void Window_Load(Object sender, EventArgs e)
		{
			
			loader = new Loader();
			renderer = new Renderer();

			shader = new StaticShader();


			float[] vertices = {
				-0.5f,0.5f,0,   //V0
                -0.5f,-0.5f,0,  //V1
                0.5f,-0.5f,0,   //V2
                0.5f,0.5f,0     //V3
	        };

			int[] indices = {
					0,1,3,  //Top left triangle (V0,V1,V3)
	                3,1,2   //Bottom right triangle (V3,V1,V2)
	        };

			float[] textureCoords = {
					0,0, // V0
	                0,1, //V1
	                1,1, //V2
	                1,0 //V3
	        };


			RawModel model = loader.LoadToVAO(vertices, textureCoords, indices);

			GL.ActiveTexture(TextureUnit.Texture0);
			ModelTexture texture = new ModelTexture(loader.LoadTexture("image"));
			texturedModel = new TexturedModel(model, texture);

						
			//Console.WriteLine(GL.GetError());


		}

		void Window_UpdateFrame(object sender, FrameEventArgs e)
		{

			renderer.Prepare();



			shader.Start();

			GL.Enable(EnableCap.Texture2D);

	
			renderer.Render(texturedModel);

			GL.Disable(EnableCap.Texture2D);
			shader.Stop();

			GL.Flush();


			window.SwapBuffers();
		}

		void Window_RenderFrame(object sender, FrameEventArgs e)
		{
			


		}

		void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			shader.CleanUp();
			loader.CleanUp();
		}

	}
}
