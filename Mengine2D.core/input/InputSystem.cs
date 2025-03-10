using DefaultEcs;

using Mengine2D.core.ecs;
using Mengine2D.core.events;

namespace Mengine2D.core.input;

public class InputSystem : BaseSystem
{
    public InputManager Input { get; set; }

    public InputSystem(World world, InputManager input) : base(world)
    {
        Input = input;
    }

    public override void Update(float deltaTime)
    {
        Input.Update();
    }

    [Subscribe]
    public void OnKey(in KeyEvent eventData)
    {
        Input.SetKeyState(eventData.Key, eventData.IsDown);
    }
}
