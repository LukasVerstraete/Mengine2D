using DefaultEcs;
using DefaultEcs.System;

namespace Mengine2D.core.ecs;

public abstract class BaseSystem : ISystem<float>
{
    public bool IsEnabled { get; set; }
    public World World { get; init; }

    public BaseSystem(World world)
    {
        World = world;
    }

    public void Dispose() 
    {}

    public abstract void Update(float state);
}