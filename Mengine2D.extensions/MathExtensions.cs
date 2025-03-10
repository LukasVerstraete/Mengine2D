namespace Mengine2D.extensions;

public static class MathExtensions
{
    public static float ToRadians(this float value)
    {
        return (float)(Math.PI / 180.0f) * value;
    }

    public static float ToDegrees(this float value)
    {
        return (float)(value * (180.0f / Math.PI));
    }

    public static float Clamp(this float value, float min, float max)
    {
        return Math.Min(Math.Max(value, min), max);
    }

    public static double Clamp(this double value, double min, double max)
    {
        return Math.Min(Math.Max(value, min), max);
    }
}
