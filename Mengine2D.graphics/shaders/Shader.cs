using System.Numerics;

using Mengine2D.extensions;

namespace Mengine2D.graphics.shaders;

public class Shader(uint handle)
{
    public uint Handle { get; protected set; } = handle;

    public void Bind()
    {
        OpenGL.Instance.UseProgram(Handle);
    }

    public void Detach()
    {
        OpenGL.Instance.UseProgram(0);
    }

    public int GetUniformLocation(string uniformName)
    {
        return OpenGL.Instance.GetUniformLocation(Handle, uniformName);
    }

    public void LoadUniform(string uniformName, Matrix4x4 matrix)
    {
        OpenGL.Instance.UniformMatrix4(GetUniformLocation(uniformName), 1, true, matrix.ToArray());
    }

    public void LoadUniform(string uniformName, float value)
    {
        OpenGL.Instance.Uniform1(GetUniformLocation(uniformName), value);
    }

    public void LoadUniform(string uniformName, Vector3 value)
    {

        OpenGL.Instance.Uniform3(GetUniformLocation(uniformName), value.X, value.Y, value.Z);
    }

    public void EnableVertexAttribArray(string name)
    {
        var attribLocation = OpenGL.Instance.GetAttribLocation(Handle, name);
        OpenGL.Instance.EnableVertexAttribArray((uint)attribLocation);
    }
}