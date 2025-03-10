using DefaultEcs;

namespace Mengine2D.core.events;

public interface IEventDispatcher
{
    public void DispatchEvent<T>(T eventData);

    public void On<T>(MessageHandler<T> handler);
}