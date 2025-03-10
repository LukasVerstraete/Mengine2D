using Mengine2D.core.ecs;
using Mengine2D.core.events;
using Mengine2D.core.input;
using Mengine2D.core.systems;
using Mengine2D.graphics;
using Mengine2D.graphics.shaders;

using SDL3;

using SharpGL.Shaders;

using static SDL3.SDL3;

namespace Mengine2D.core;

public class Engine
{
    public bool IsRunning { get; private set; } = false;

    public Game Game { get; private set; }

    public Window Window { get; set; }
    public EventLoop? EventLoop { get; set; }
    public InputManager InputManager { get; set; }
    public EntityManager EntityManager { get; set; }
    public Renderer Renderer { get; set; }

    private int FPSCounter { get; set; } = 0;
    private float FPSTimeCounter { get; set; } = 0;

    public Engine(Game game)
    {
        Game = game;
        Window = new Window();
        Renderer = new Renderer(Window);
        EntityManager = new EntityManager();
        InputManager = new InputManager(EntityManager);
    }

    public void Init()
    {
        SDL_Init(SDL_InitFlags.Video);

        Window.Init(Game.Config.Title, Game.Config.InitialWidth, Game.Config.InitialHeight);
        Renderer.Init();
        EventLoop = new EventLoop(EntityManager, InputManager);

        EntityManager.On<WindowCloseEvent>(OnClose);

        InitBaseSystems();

        Game.Init(this);

        IsRunning = true;

        Run();
    }

    public void InitBaseSystems()
    {
        ShaderLoader.LoadShader("../Mengine2D.core/assets", "basic");
        EntityManager.RegisterSystem(new InputSystem(EntityManager.World, InputManager));
        EntityManager.RegisterSystem(new CameraSystem(EntityManager.World));
        EntityManager.RegisterSystem(new BasicRenderSystem(EntityManager.World));
    }

    private void Run()
    {
        FrameTimer.Timer.EndFrame();
        while (IsRunning)
        {
            CountFrames();

            Input();
            Update(FrameTimer.Timer.DeltaTime());
            FrameTimer.Timer.EndFrame();
        }
        Stop();
    }

    private void CountFrames()
    {
        FPSTimeCounter += FrameTimer.Timer.DeltaTime();
        FPSCounter++;
        if (FPSTimeCounter >= 1f)
        {
            Console.WriteLine(FPSCounter);

            FPSTimeCounter -= 1f;
            FPSCounter = 0;
        }
    }

    private void OnClose(in WindowCloseEvent _)
    {
        Stop();
    }

    public void Input()
    {
        EventLoop!.Update();
    }

    public void Update(float deltaTime)
    {
        Renderer.Prepare();
        InputManager.Update();
        Renderer.Prepare();
        EntityManager.Update(deltaTime);
        Renderer.Render();
    }

    public void Stop()
    {
        IsRunning = false;
        Window.Close();
        SDL_Quit();
    }
}