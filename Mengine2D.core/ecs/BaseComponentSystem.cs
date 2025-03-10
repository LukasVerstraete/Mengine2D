using DefaultEcs;
using DefaultEcs.System;

namespace Mengine2D.core.ecs;

public abstract class BaseComponentSystem: AEntitySetSystem<float>
{
    public BaseComponentSystem(EntitySet entitySet): base(entitySet) { }

    public BaseComponentSystem(World world) : base(world) { }
}