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

        var projectionMatrix = new Matrix4x4(
            2f / 1920f, 0, 0, 0,
            0, 2f / 1080f, 0, 0,
            0, 0, -2f / (1000f - 0.01f), -(1000f + 0.01f) / (1000f - 0.01f),
            0, 0, 0, 1
        );

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

        Matrix4x4.Invert(transform.Transformation, out var viewMatrix);

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
}