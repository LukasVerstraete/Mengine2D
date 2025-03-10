using DefaultEcs;
using DefaultEcs.System;

using Mengine2D.core.events;

namespace Mengine2D.core.ecs;

public class EntityManager : IEventDispatcher
{
    public World World { get; set; }
    private List<ISystem<float>> _systems { get; set; }

    public EntityManager()
    {
        World = new World();
        _systems = [];
    }

    public Entity CreateEntity() => World.CreateEntity();

    public void Update(float deltaTime)
    {
        _systems.ForEach(system => system.Update(deltaTime));
    }

    public void DispatchEvent<T>(T eventData)
    {
        World.Publish(eventData);
    }

    public void On<T>(MessageHandler<T> handler)
    {
        World.Subscribe(handler);
    }

    public void RegisterSystem(ISystem<float> system)
    {
        World.Subscribe(system);
        _systems.Add(system);
    }
}