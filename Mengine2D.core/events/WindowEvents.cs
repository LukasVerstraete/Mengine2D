namespace Mengine2D.core.events;

public abstract class WindowEvent
{ }

public class WindowCloseEvent : WindowEvent
{ }

public class WindowResizeEvent : WindowEvent
{
    public int Width { get; set; }
    public int Height { get; set; }
}
