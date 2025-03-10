using Mengine2D.core.events;
using Mengine2D.core.input;

using SDL3;

using static SDL3.SDL3;

namespace Mengine2D.core;

public class EventLoop
{
    public IEventDispatcher Dispatcher { get; set; }
    public InputManager InputManager { get; set; }

    public EventLoop(IEventDispatcher dispatcher, InputManager inputManager)
    {
        Dispatcher = dispatcher;
        InputManager = inputManager;
    }

    public void Update()
    {
        while (SDL_PollEvent(out SDL_Event sdlEvent) == SDLBool.True)
        {

            switch (sdlEvent.type)
            {
                case SDL_EventType.Quit:
                    OnWindowClose(sdlEvent);
                    break;
                case SDL_EventType.WindowResized:
                    OnWindowResize(sdlEvent);
                    break;
                case SDL_EventType.KeyDown:
                    OnKeyDown(sdlEvent);
                    break;
                case SDL_EventType.KeyUp:
                    OnKeyUp(sdlEvent);
                    break;
                case SDL_EventType.MouseMotion:
                    OnMouseMove(sdlEvent);
                    break;
                case SDL_EventType.MouseButtonDown:
                    OnMouseDown(sdlEvent);
                    break;
                case SDL_EventType.MouseButtonUp:
                    OnMouseUp(sdlEvent);
                    break;
            }
        }
    }

    private void OnKeyDown(SDL_Event sdlEvent)
    {
        var keyEvent = new KeyEvent
        {
            Key = sdlEvent.key.key,
            IsDown = true,
            InputState = InputManager
        };

        Dispatcher.DispatchEvent(keyEvent);
    }

    private void OnKeyUp(SDL_Event sdlEvent)
    {
        var keyEvent = new KeyEvent
        {
            Key = sdlEvent.key.key,
            IsDown = false,
            InputState = InputManager
        };

        Dispatcher.DispatchEvent(keyEvent);
    }

    private void OnMouseMove(SDL_Event sdlEvent)
    {
        var mouseEvent = new MouseMoveEvent
        {
            X = sdlEvent.motion.x,
            Y = sdlEvent.motion.y,
            DeltaX = sdlEvent.motion.xrel,
            DeltaY = sdlEvent.motion.yrel
        };

        Dispatcher.DispatchEvent(mouseEvent);
    }

    private void OnMouseDown(SDL_Event sdlEvent)
    {
        var mouseEvent = new MouseDownEvent
        {
            X = sdlEvent.motion.x,
            Y = sdlEvent.motion.y,
            Button = sdlEvent.button.button
        };

        Dispatcher.DispatchEvent(mouseEvent);
    }

    private void OnMouseUp(SDL_Event sdlEvent)
    {
        var mouseEvent = new MouseUpEvent
        {
            X = sdlEvent.motion.x,
            Y = sdlEvent.motion.y,
            Button = sdlEvent.button.button
        };

        Dispatcher.DispatchEvent(mouseEvent);
    }

    private void OnWindowClose(SDL_Event sdlEvent)
    {
        var windowEvent = new WindowCloseEvent();

        Dispatcher.DispatchEvent(windowEvent);
    }

    private void OnWindowResize(SDL_Event sdlEvent)
    {
        var windowEvent = new WindowResizeEvent
        {
            Width = sdlEvent.window.data1,
            Height = sdlEvent.window.data2
        };

        Dispatcher.DispatchEvent(windowEvent);
    }
}
