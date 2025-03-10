using System.Runtime.InteropServices;

using SharpGL;

public static class OpenGL
{
    private static SharpGL.OpenGL _instance;

    public static SharpGL.OpenGL Instance
    {
        get
        {
            if (_instance is not null)
            {
                return _instance;
            }
            _instance = new SharpGL.OpenGL();
            return _instance;
        }
    }

    public const uint GL_INFO_LOG_LENGTH = SharpGL.OpenGL.GL_INFO_LOG_LENGTH;
    public const uint GL_VERTEX_SHADER = SharpGL.OpenGL.GL_VERTEX_SHADER;
    public const uint GL_FRAGMENT_SHADER = SharpGL.OpenGL.GL_FRAGMENT_SHADER;
    public const uint GL_DEPTH_TEST = SharpGL.OpenGL.GL_DEPTH_TEST;
    public const uint GL_COLOR_BUFFER_BIT = SharpGL.OpenGL.GL_COLOR_BUFFER_BIT;
    public const uint GL_DEPTH_BUFFER_BIT = SharpGL.OpenGL.GL_DEPTH_BUFFER_BIT;
    public const uint GL_TEXTURE_2D = SharpGL.OpenGL.GL_TEXTURE_2D;
    public const uint GL_TEXTURE0 = SharpGL.OpenGL.GL_TEXTURE0;
    public const uint GL_PERSPECTIVE_CORRECTION_HINT = SharpGL.OpenGL.GL_PERSPECTIVE_CORRECTION_HINT;
    public const uint GL_NICEST = SharpGL.OpenGL.GL_NICEST;
    public const uint GL_RGBA = SharpGL.OpenGL.GL_RGBA;
    public const uint GL_BGRA = SharpGL.OpenGL.GL_BGRA;
    public const uint GL_RGBA8 = SharpGL.OpenGL.GL_RGBA8;
    public const uint GL_UNSIGNED_BYTE = SharpGL.OpenGL.GL_UNSIGNED_BYTE;
    public const uint GL_UNSIGNED_INT = SharpGL.OpenGL.GL_UNSIGNED_INT;
    public const uint GL_TEXTURE_MIN_FILTER = SharpGL.OpenGL.GL_TEXTURE_MIN_FILTER;
    public const uint GL_TEXTURE_MAG_FILTER = SharpGL.OpenGL.GL_TEXTURE_MAG_FILTER;
    public const uint GL_TEXTURE_WRAP_S = SharpGL.OpenGL.GL_TEXTURE_WRAP_S;
    public const uint GL_TEXTURE_WRAP_T = SharpGL.OpenGL.GL_TEXTURE_WRAP_T;
    public const uint GL_NEAREST = SharpGL.OpenGL.GL_NEAREST;
    public const uint GL_REPEAT = SharpGL.OpenGL.GL_REPEAT;
    public const uint GL_ARRAY_BUFFER = SharpGL.OpenGL.GL_ARRAY_BUFFER;
    public const uint GL_ELEMENT_ARRAY_BUFFER = SharpGL.OpenGL.GL_ELEMENT_ARRAY_BUFFER;
    public const uint GL_STATIC_DRAW = SharpGL.OpenGL.GL_STATIC_DRAW;
    public const uint GL_FLOAT = SharpGL.OpenGL.GL_FLOAT;
    public const uint GL_CULL_FACE = SharpGL.OpenGL.GL_CULL_FACE;
    public const uint GL_BACK = SharpGL.OpenGL.GL_BACK;
    public const uint GL_TRIANGLES = SharpGL.OpenGL.GL_TRIANGLES;
    public const uint GL_VERSION = SharpGL.OpenGL.GL_VERSION;
    public const uint GL_VENDOR = SharpGL.OpenGL.GL_VENDOR;
    public const uint GL_RENDERER = SharpGL.OpenGL.GL_RENDERER;
    public const uint GL_SHADING_LANGUAGE_VERSION = SharpGL.OpenGL.GL_SHADING_LANGUAGE_VERSION;
    public const uint GL_CURRENT_PROGRAM = SharpGL.OpenGL.GL_CURRENT_PROGRAM;
    public const uint GL_NO_ERROR = SharpGL.OpenGL.GL_NO_ERROR;
}