using System.Numerics;

namespace Mengine2D.extensions;

public static class VectorExtensions
{
    public static float[] ToArray(this Vector3 value)
    {
        var array = new float[3];
        value.CopyTo(array);

        return array;
    }
}