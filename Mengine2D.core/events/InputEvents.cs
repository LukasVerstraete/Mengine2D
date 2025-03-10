using System.Numerics;
using SDL3;

using Mengine2D.core.input;

namespace Mengine2D.core.events;

public class KeyEvent
{
    public InputManager? InputState { get; set; }
    public SDL_Keycode Key { get; set; }
    public bool IsDown { get; set; }
}

public class KeyPressedEvent : KeyEvent
{
    public int Repeat { get; set; }
}

public abstract class MouseEvent
{
    public float X { get; set; }
    public float Y { get; set; }
    public Vector2 MousePosition => new Vector2(X, Y);

}

public class MouseDownEvent : MouseEvent
{
    public int Button { get; set; }
}

public class MouseUpEvent : MouseEvent
{
    public int Button { get; set; }
}

public class MouseMoveEvent : MouseEvent
{
    public float DeltaX { get; set; }
    public float DeltaY { get; set; }
    public Vector2 Delta => new Vector2(DeltaX, Y);
}
