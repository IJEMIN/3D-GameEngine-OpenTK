using System;
namespace GLEngine
{
	public class TexturedModel
	{

		private RawModel rawModel;

		private ModelTexture texture;

		public TexturedModel(RawModel model, ModelTexture textures)
		{
			this.rawModel = model;
			this.texture = textures;
		}

		public RawModel GetRawModel()
		{
			return rawModel;
		}

		public ModelTexture GetTexture()
		{
			return texture;
		}
	}
}
