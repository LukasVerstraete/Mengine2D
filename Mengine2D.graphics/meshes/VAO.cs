namespace Mengine2D.graphics.meshes
{
    public class VAO
    {
        public uint Id { get; private set; }
        public int ElementCount { get; set; }

        public VAO() : this(0) { }

        public VAO(int elementCount)
        {
            var vaoId = new uint[1];
            OpenGL.Instance.GenVertexArrays(1, vaoId);
            Id = vaoId[0];
            ElementCount = elementCount;
        }

        public void Bind()
        {
            OpenGL.Instance.BindVertexArray(Id);
        }

        public void UnBind()
        {
            OpenGL.Instance.BindVertexArray(0);
        }

        public void Delete()
        {
            OpenGL.Instance.DeleteVertexArrays(1, [Id]);
        }
    }
}
