namespace Mengine2D.graphics.meshes
{
    public class Texture
    {
        public readonly uint TextureId;

        public Texture(uint textureId)
        {
            TextureId = textureId;
        }

        public void Bind()
        {
            OpenGL.Instance.BindTexture(OpenGL.GL_TEXTURE_2D, TextureId);
        }

        public void UnBind()
        {
            OpenGL.Instance.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
        }
    }
}
