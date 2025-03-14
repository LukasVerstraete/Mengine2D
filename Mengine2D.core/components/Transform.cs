using System.Numerics;

using Mengine2D.extensions;

namespace Mengine2D.core.components;

public class Transform
{
    public Transform? Parent { get; set; }
    public Matrix4x4 ParentMatrix => Parent != null ? Parent.Transformation : Matrix4x4.Identity;

    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;

    public Matrix4x4 Transformation => GetTransformation();
    public Vector3 TransformedPosition => Position + (Parent == null ? new Vector3(0, 0, 0) : Parent.TransformedPosition);
    public Vector3 Forward => GetForward();
    public Vector3 Right => GetRight();

    public Transform()
    {
        Position = new Vector3(0, 0, 0);
        Rotation = new Vector3(0, 0, 0);
        Scale = new Vector3(1, 1, 1);
    }

    private Matrix4x4 GetTransformation()
    {
        var translation = Matrix4x4.CreateTranslation(Position);
        var rotation = GetRotationMatrix();
        var scale = Matrix4x4.CreateScale(Scale);

        return scale * rotation * translation * ParentMatrix;
    }

    public Matrix4x4 GetRotationMatrix()
    {
        var rotationX = Matrix4x4.CreateRotationX(Rotation.X.ToRadians());
        var rotationY = Matrix4x4.CreateRotationY(Rotation.Y.ToRadians());
        var rotationZ = Matrix4x4.CreateRotationZ(Rotation.Z.ToRadians());

        return rotationZ * rotationY * rotationX;
    }

    public Matrix4x4 GetTranslationMatrix()
    {
        return Matrix4x4.CreateTranslation(Position);
    }

    public Matrix4x4 GetScaleMatrix()
    {
        return Matrix4x4.CreateScale(Scale);
    }

    private Vector3 GetForward()
    {
        return Vector3.Normalize(new Vector3(
            (float)Math.Cos(Rotation.X.ToRadians()) * (float)Math.Cos(Rotation.Y.ToRadians()),
            (float)Math.Sin(Rotation.X.ToRadians()),
            (float)Math.Cos(Rotation.X.ToRadians()) * (float)Math.Sin(Rotation.Y.ToRadians())
        ));
    }

    private Vector3 GetRight()
    {
        return Vector3.Cross(Forward, Vector3.UnitY);
    }

    public void RotateX(float angle)
    {
        Rotation.X += angle;
    }

    public void RotateY(float angle)
    {
        Rotation.Y += angle;
    }

    public void RotateZ(float angle)
    {
        Rotation.Z += angle;
    }

    public void Rotate(Vector3 rotation)
    {
        Rotation += rotation;
    }

    public void TranslateX(float translation)
    {
        Position.X += translation;
    }

    public void TranslateY(float translation)
    {
        Position.Y += translation;
    }

    public void TranslateZ(float translation)
    {
        Position.Z += translation;
    }

    public void Translate(Vector3 translation)
    {
        Position += translation;
    }

    public void ScaleX(float scalar)
    {
        Scale.X += scalar;
    }

    public void ScaleY(float scalar)
    {
        Scale.Y += scalar;
    }

    public void ScaleZ(float scalar)
    {
        Scale.Z += scalar;
    }

    public void Resize(Vector3 scalar)
    {
        Scale += scalar;
    }
}
