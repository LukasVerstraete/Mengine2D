using System.Numerics;

using DefaultEcs;
using DefaultEcs.System;

using Mengine2D.core.components;
using Mengine2D.core.ecs;
using Mengine2D.extensions;
using Mengine2D.graphics.shaders;

namespace Mengine2D.core.systems;

[With(typeof(Transform))]
[With(typeof(CameraComponent))]
public class CameraSystem : BaseComponentSystem
{
    public CameraSystem(World world) : base(world)
    {
    }

    protected override void Update(float _, in Entity entity)
    {
        var transform = entity.Get<Transform>();
        var camera = entity.Get<CameraComponent>();
        // Console.WriteLine($"Width: {camera.Width}, Height: {camera.Height}, ZNear: {camera.ZNear}, ZFar: {camera.ZFar}");

        // var projectionMatrix = Matrix4x4.CreateOrthographic(
        //     camera.Width,
        //     camera.Height,
        //     camera.ZNear,
        //     camera.ZFar
        // );

		var projectionMatrix = CreateOrthoProjection(camera);

		 // Get parent transformation
		Matrix4x4.Invert(transform.ParentMatrix, out var parentInverse);

		// Compute inverse translation (move world instead of camera)
		Matrix4x4 translationMatrix = Matrix4x4.CreateTranslation(-transform.Position);

		// Compute inverse rotation (negate angles, apply in reverse order)
		Matrix4x4 rotationMatrix = Matrix4x4.Transpose(transform.GetRotationMatrix());

		// Compute inverse scale (zooming effect)
		Matrix4x4 scaleMatrix = Matrix4x4.CreateScale(1 / transform.Scale.X, 1 / transform.Scale.Y, 1); // No scaling on Z in 2D

		// Final View Matrix (including scale for zooming)
		var viewMatrix = parentInverse * scaleMatrix * rotationMatrix * translationMatrix;

		Console.WriteLine(projectionMatrix);

        // var projectionMatrix = new Matrix4x4(
        //     2f / 1920f, 0, 0, 0,
        //     0, 2f / 1080f, 0, 0,
        //     0, 0, -2f / (1000f - 0.01f), -(1000f + 0.01f) / (1000f - 0.01f),
        //     0, 0, 0, 1
        // );

        // Console.WriteLine($"projectionMatrix:");
        // foreach (var value in Matrix4x4.Transpose(projectionMatrix).ToArray())
        // {
        //     Console.Write($"{value} ");
        // }
        // Console.WriteLine();

        var shader = ShaderLoader.GetShader("basic");

        if (shader is null)
        {
            return;
        }

        // Console.WriteLine($"viewMatrix:");
        // foreach (var value in Matrix4x4.Transpose(viewMatrix).ToArray())
        // {
        //     Console.Write($"{value} ");
        // }
        // Console.WriteLine();

        shader.Bind();
        shader.LoadUniform("view", viewMatrix);
        shader.LoadUniform("projection", projectionMatrix);
        shader.Detach();

    }

	private static Matrix4x4 CreateOrthoProjection(CameraComponent camera)
	{
		// | 2 / (right - left)   0   				   	0   				-(right + left) / (right - left) |
		// | 0   				  2 / (top - bottom)   	0   				-(top + bottom) / (top - bottom) |
		// | 0   				  0   					-2 / (far - near)   -(far + near) / (far - near) |
		// | 0   				  0   					0  					1 |

		var left = -(camera.Width / 2);
		var right = camera.Width / 2;
		var top = camera.Height / 2;
		var bottom = -(camera.Height / 2);
		var zNear = camera.ZNear;
		var ZFar = camera.ZFar;

		return new Matrix4x4
		{
			M11 = 2.0f / (right - left),
			M12 = 0.0f,
			M13 = 0.0f,
			M14 = -(right + left) / (right - left),

			M21 = 0.0f,
			M22 = 2.0f / (top - bottom),
			M23 = 0.0f,
			M24 = -(top + bottom) / (top - bottom),

			M31 = 0.0f,
			M32 = 0.0f,
			M33 = -2.0f / (ZFar - zNear),
			M34 = -(ZFar + zNear) / (ZFar - zNear),

			M41 = 0.0f,
			M42 = 0.0f,
			M43 = 0.0f,
			M44 = 1.0f
		};
	}
}
