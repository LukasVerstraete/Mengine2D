using System.Numerics;

using DefaultEcs;

using Mengine2D.core;
using Mengine2D.core.components;
using Mengine2D.core.ecs;
using Mengine2D.core.events;
using Mengine2D.core.input;
using Mengine2D.graphics.meshes;
using Mengine2D.graphics.shaders;

namespace Mengine2D.testapp;

public class TestApp
{
    public static void Main()
    {
        var engine = new Engine(new TestGame());
        engine.Init();
    }
}

public class TestGame : Game
{
    public TestGame() : base(new GameConfig("Test", 1080, 720)) { }

    public override void Init(Engine engine)
    {
        var camera = engine.EntityManager.CreateEntity();

        camera.Set(new CameraComponent(engine.Window.Width, engine.Window.Height, 0.01f, 1000f));
        camera.Set(new Transform());

        var texture = TextureLoader.LoadTexture("./assets/girl.png");
        float[] vertices = {
            // Position (x, y, z)  | Texture Coords (u, v)
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f, // Top-left
            0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // Top-right
            0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // Bottom-right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f  // Bottom-left
        };

        uint[] indices = {
            0, 1, 2, // First triangle
            2, 3, 0  // Second triangle
        };

        var mesh = new Mesh(vertices, indices, texture);
        var renderable = engine.EntityManager.CreateEntity();
        var transform = new Transform();
        transform.ScaleX(100f);
        transform.ScaleY(100f);
        // transform.TranslateZ(1f);
        renderable.Set(new RenderComponent(mesh));
        renderable.Set(transform);

        engine.EntityManager.RegisterSystem(new CameraMoveSystem(engine.EntityManager.World));
    }
}

public class CameraMoveSystem : BaseSystem
{
	public Vector3 Direction {get; set;} = Vector3.Zero;
	private const float CAMERA_SPEED = 0.5f;

	public CameraMoveSystem(World world) : base(world)
	{}

	public override void Update(float state)
	{
		var entities = World.GetEntities().With<CameraComponent>().With<Transform>().AsEnumerable();

		foreach(var entity in entities)
		{
			var transform = entity.Get<Transform>();
			transform.Translate(Direction * state * CAMERA_SPEED);
		}
	}

	[Subscribe]
	public void OnKey(in KeyEvent eventData)
	{
		InputManager state = eventData.InputState;

		var direction = new Vector3(0, 0, 0);

		if (state.IsDown(Keys.FORWARD)) { direction.Y += 1; }
		if (state.IsDown(Keys.BACKWARD)) { direction.Y -= 1; }
		if (state.IsDown(Keys.LEFT)) { direction.X -= 1; }
		if (state.IsDown(Keys.RIGHT)) { direction.X += 1; }


		if (direction.Length() == 0) 
		{
			Direction = Vector3.Zero;
		}
		else
		{
			Direction = Vector3.Normalize(direction);
		}
	}
}

// public class TestSystem : BaseSystem
// {
//     private Shader _basicShader;
//     private VAO _vao;

//     public TestSystem(World world) : base(world)
//     {
//         _basicShader = ShaderLoader.LoadShader("./assets", "basic");
//         _vao = VAOFactory.CreateVAO(GetVertices(), GetIndices(), false);
//     }

//     public override void Update(float state)
//     {
//         _basicShader.Bind();

//         _vao.Bind();

//         OpenGL.Instance.DrawElements(OpenGL.GL_TRIANGLES, _vao.ElementCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);

//         _vao.UnBind();

//         _basicShader.Detach();
//     }

//     private static float[] GetVertices()
//     {
//         return [
//             -0.5f, -0.5f, 0.0f, // Left
//              0.5f, -0.5f, 0.0f, // Right
//              0.0f,  0.5f, 0.0f  // Top
//         ];
//     }

//     private static uint[] GetIndices()
//     {
//         return [0, 1, 2];
//     }
// }
