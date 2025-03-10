namespace Mengine2D.core;

public abstract class Game(GameConfig config)
{
    public GameConfig Config { get; set; } = config;

    public abstract void Init(Engine engine);
}