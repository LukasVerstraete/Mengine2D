namespace Mengine2D.graphics.meshes
{
    public class Mesh
    {
        public static readonly uint VERTEX_SIZE = 5;

        public VAO _vao;
        public Texture Texture { get; set; }
		public bool UseTexCoords { get; set; }

        public int ElementCount
        {
            get
            {
                return _vao?.ElementCount ?? 0;
            }
        }

        public Mesh() : this([], [], new Texture(0), false)
        { }

        public Mesh(float[] vertices, uint[] indexes, Texture texture, bool useTexCoords = true)
        {
            Texture = texture;
			UseTexCoords = useTexCoords;
            UpdateMesh(vertices, indexes);
        }

        public void UpdateMesh(float[] vertices, uint[] indices)
        {
            _vao?.Delete();
            if (vertices.Length > 0 && indices.Length > 0)
            {
                _vao = VAOFactory.CreateVAO(vertices, indices, UseTexCoords);
            }
        }

        public void Bind()
        {
            Texture.Bind();
            _vao?.Bind();
        }

        public void UnBind()
        {
            _vao?.UnBind();
            Texture.UnBind();
        }
    }
}
