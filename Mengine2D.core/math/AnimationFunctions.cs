using Mengine2D.extensions;

namespace Mengine2D.core.math;

public static class AnimationFunctions
{
    public static double Lerp(double from, double to, double delta)
    {
        delta = delta.Clamp(0, 1);

        return from + (to - from) * delta;
    }

    public static double EaseIn(double from, double to, double delta)
    {
        return Lerp(from, to, delta * delta);
    }
}
