using System.Diagnostics;

namespace Mengine2D.core;
public class FrameTimer
{
    public static FrameTimer Timer = new FrameTimer();

    private long _lastFrameTicks = 0;
    private long _currentFrameTicks = 0;
    private Stopwatch _stopwatch;

    private FrameTimer()
    {
        _stopwatch = new Stopwatch();
        _stopwatch.Start();
    }

    public void EndFrame()
    {
        _lastFrameTicks = _currentFrameTicks;
        _currentFrameTicks = _stopwatch.ElapsedTicks;
    }

    public float DeltaTime()
    {
        return (_currentFrameTicks - _lastFrameTicks) / (float)TimeSpan.TicksPerSecond;
    }

    public long CurrentTime()
    {
        return _stopwatch.ElapsedMilliseconds;
    }
}
