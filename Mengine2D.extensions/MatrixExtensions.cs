using System.Numerics;
using System.Runtime.InteropServices;

namespace Mengine2D.extensions;

public static class MatrixExtensions
{
    public static float[] ToArray(this Matrix4x4 value)
    {
        return MemoryMarshal.CreateSpan(ref value.M11, 16).ToArray();
    }
}