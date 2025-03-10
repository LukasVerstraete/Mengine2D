using SDL3;

using static SDL3.SDL3;

namespace Mengine2D.graphics;

public class Window
{
    public SDL_Window Handle { get; set; }
    public string? Title { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Window() { }

    public Window(string title, int width, int height)
    {
        Title = title;
        Width = width;
        Height = height;
    }

    public void Init(string title, int width, int height)
    {
        Title = title;
        Width = width;
        Height = height;

        SDL_GL_SetAttribute(SDL_GLAttr.ContextMajorVersion, 4);
        SDL_GL_SetAttribute(SDL_GLAttr.ContextMinorVersion, 5);
        SDL_GL_SetAttribute(SDL_GLAttr.ContextProfileMask, (int)SDL_GLProfile.Core);

        Handle = SDL_CreateWindow(
            Title,
            Width,
            Height,
            SDL_WindowFlags.OpenGL | SDL_WindowFlags.Resizable
        );

        SDL_SetWindowPosition(Handle, SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED);
    }

    public void Close()
    {
        SDL_DestroyWindow(Handle);
    }
}
