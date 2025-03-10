using Mengine2D.core.events;
using Mengine2D.graphics;

using SDL3;

using static SDL3.SDL3;

namespace Mengine2D.core.input;

public class InputManager
{
    private IEventDispatcher dispatcher;

    private Dictionary<SDL_Keycode, bool> keys;
    private Dictionary<SDL_Keycode, bool> pressed;

    public bool IsMouseLocked { get; private set; }
    private float mousePositionX = 0;
    private float mousePositionY = 0;

    public float MouseDeltaX { get; private set; }
    public float MouseDeltaY { get; private set; }

    public InputManager(IEventDispatcher dispatcher)
    {
        keys = new Dictionary<SDL_Keycode, bool>();
        pressed = new Dictionary<SDL_Keycode, bool>();
        IsMouseLocked = false;
        this.dispatcher = dispatcher;
    }

    public void Update()
    {
        pressed.Clear();
        ResetMouseDelta();
    }

    public void SetKeyState(SDL_Keycode key, bool isDown)
    {
        keys[key] = isDown;
        if (!isDown)
        {
            SetKeyPressed(key);
        }
    }

    public void SetKeyPressed(SDL_Keycode key)
    {
        pressed[key] = true;

        KeyPressedEvent keyEvent = new KeyPressedEvent();
        keyEvent.Key = key;
        keyEvent.Repeat = 1;
        keyEvent.IsDown = false;

        dispatcher.DispatchEvent(keyEvent);
    }

    public bool IsDown(SDL_Keycode key)
    {
        if (keys.ContainsKey(key))
        {
            return keys[key];
        }
        return false;
    }

    public bool IsPressed(SDL_Keycode key)
    {
        if (pressed.ContainsKey(key))
        {
            return pressed[key];
        }
        return false;
    }

    public void SetMouseDelta(float x, float y)
    {
        MouseDeltaX = x;
        MouseDeltaY = y;
    }

    private void ResetMouseDelta()
    {
        MouseDeltaX = 0;
        MouseDeltaY = 0;
    }

    public void SetMousePosition(float x, float y)
    {
        mousePositionX = x;
        mousePositionY = y;
    }

    public void SwitchMouseLock(Window window)
    {
        if (IsMouseLocked)
        {
            UnLockMouse(window);
        }
        else
        {
            LockMouse(window);
        }
    }

    public void LockMouse(Window window)
    {
        IsMouseLocked = true;
        SDL_SetWindowRelativeMouseMode(window.Handle, SDLBool.True);
    }

    public void UnLockMouse(Window window)
    {
        IsMouseLocked = false;
        SDL_SetWindowRelativeMouseMode(window.Handle, SDLBool.False);
    }
}

public static class Keys
{
    public static readonly SDL_Keycode FORWARD = SDL_Keycode.W;
    public static readonly SDL_Keycode BACKWARD = SDL_Keycode.S;
    public static readonly SDL_Keycode LEFT = SDL_Keycode.A;
    public static readonly SDL_Keycode RIGHT = SDL_Keycode.D;
    public static readonly SDL_Keycode UP = SDL_Keycode.Space;
    public static readonly SDL_Keycode DOWN = SDL_Keycode.LeftShift;
    public static readonly SDL_Keycode LOCK = SDL_Keycode.L;
}