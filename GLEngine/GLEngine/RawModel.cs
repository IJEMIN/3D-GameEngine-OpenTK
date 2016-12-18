using System;
namespace GLEngine
{
	public class RawModel
	{

		private int m_vaoID;
		private int m_vertexCount;

		public RawModel(int vaoID, int vertexCount)
		{
			this.m_vaoID = vaoID;
			this.m_vertexCount = vertexCount;
		}

		public int GetVaoID()
		{
			return m_vaoID;
		}

		public int GetVertexCount()
		{
			return m_vertexCount;
		}

	}
}
