using SDL3;

using static SDL3.SDL3;

namespace Mengine2D.graphics;

public class Renderer
{
    public SDL_GLContext SDLOpenGLContextHandle { get; private set; }
    public Window Window { get; set; }

    public Renderer(Window window)
    {
        Window = window;
    }

    public void Init()
    {
        SDLOpenGLContextHandle = SDL_GL_CreateContext(Window.Handle);
        SDL_GL_MakeCurrent(Window.Handle, SDLOpenGLContextHandle);
        OpenGL.Instance.Viewport(0, 0, Window.Width, Window.Height);
        OpenGL.Instance.ClearColor(1f, 0.5f, 0.8f, 1.0f);
        OpenGL.Instance.Enable(OpenGL.GL_DEPTH_TEST);


    }

    public void Prepare()
    {
        OpenGL.Instance.ClearColor(1f, 0.5f, 0.8f, 1.0f);
        OpenGL.Instance.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
    }

    public void Render()
    {
        SDL_GL_SwapWindow(Window.Handle);
    }
}
