using System.Drawing;

using SkiaSharp;

namespace Mengine2D.graphics.meshes;

public static class TextureLoader
{
    private static Mutex mutex = new Mutex();

    public static Texture LoadTexture(string path)
    {
        mutex.WaitOne();

        var gl = OpenGL.Instance;

        gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);

        var textureId = new uint[1];
        gl.GenTextures(1, textureId);
        gl.BindTexture(OpenGL.GL_TEXTURE_2D, textureId[0]);

        var fileInfo = new FileInfo(path);

        using (var fileStream = fileInfo.OpenRead())
        {
            var bitMap = SKBitmap.Decode(fileStream);

            gl.TexImage2D(
                OpenGL.GL_TEXTURE_2D,
                0,
                OpenGL.GL_RGBA,
                bitMap.Width,
                bitMap.Height,
                0,
                OpenGL.GL_BGRA,
                OpenGL.GL_UNSIGNED_BYTE,
                bitMap.GetPixelSpan().ToArray()
            );

            Console.WriteLine($"Texture: {textureId[0]}, ColorType: {bitMap.ColorType}");
        }

        // Error Checking
        var error = gl.GetError();
        if (error != OpenGL.GL_NO_ERROR)
        {
            Console.WriteLine($"OpenGL Error after TexImage2D: {error}");
        }

        gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_NEAREST);
        gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_NEAREST);
        gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
        gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);


        ///TODO find a way to do mipmapping
        // gl.GenerateMipmapEXT(OpenGL.GL_TEXTURE_2D);
        // gl.GenerateTextureMipmapEXT(textureId);


        mutex.ReleaseMutex();

        return new Texture(textureId[0]);
    }
}
