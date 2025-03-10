using DefaultEcs;

using Mengine2D.core.components;
using Mengine2D.core.ecs;
using Mengine2D.graphics.shaders;

namespace Mengine2D.core.systems;

public class BasicRenderSystem : BaseSystem
{
    public Shader Shader { get; set; }

    public BasicRenderSystem(World world) : base(world)
    {
        Shader = ShaderLoader.GetShader("basic");
    }

    public override void Update(float delta)
    {
        // var entities = World.GetEntities().With<Transform>().With<RenderComponent>().AsEnumerable();

        // var gl = OpenGL.Instance;

        // gl.Enable(OpenGL.GL_CULL_FACE);
        // gl.CullFace(OpenGL.GL_BACK);
        // Shader.Bind();

        // foreach (var entity in entities)
        // {

        //     OpenGL.Instance.Disable(OpenGL.GL_CULL_FACE);
        //     var transform = entity.Get<Transform>().Transformation;
        //     Shader.LoadUniform("transform", transform);
        //     gl.ActiveTexture(OpenGL.GL_TEXTURE0);
        //     Shader.LoadUniform("texture0", 0);
        //     var mesh = entity.Get<RenderComponent>().Mesh;

        //     // Console.WriteLine($"Binding VAO: {mesh._vao.Id}");
        //     mesh.Bind();
        //     // Console.WriteLine($"glDrawElements(GL_TRIANGLES, {mesh.ElementCount}, GL_UNSIGNED_INT, 0)");
        //     gl.DrawElements(OpenGL.GL_TRIANGLES, mesh.ElementCount, OpenGL.GL_UNSIGNED_INT, 0);
        //     mesh.UnBind();
        // }

        // Shader.Detach();

        // In BasicRenderSystem, replace existing mesh setup with:
        uint[] vaoId = new uint[1];
        OpenGL.Instance.GenVertexArrays(1, vaoId);
        OpenGL.Instance.BindVertexArray(vaoId[0]);

        float[] hardcodedVertices = {
    -0.5f, -0.5f, 0.0f, // Position
    0.5f, -0.5f, 0.0f,
    0.0f, 0.5f, 0.0f
};

        uint[] vboId = new uint[1];
        OpenGL.Instance.GenBuffers(1, vboId);
        OpenGL.Instance.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vboId[0]);
        OpenGL.Instance.BufferData(OpenGL.GL_ARRAY_BUFFER, hardcodedVertices, OpenGL.GL_STATIC_DRAW);

        OpenGL.Instance.EnableVertexAttribArray(0);
        OpenGL.Instance.VertexAttribPointer(0, 3, OpenGL.GL_FLOAT, false, 3 * sizeof(float), 0);

        uint[] hardcodedIndices = { 0, 1, 2 };
        uint[] eboId = new uint[1];
        OpenGL.Instance.GenBuffers(1, eboId);
        OpenGL.Instance.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, eboId[0]);
        fixed
        {

            OpenGL.Instance.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, hardcodedIndices.Length * sizeof(uint), hardcodedIndices, OpenGL.GL_STATIC_DRAW);
        }


        // In BasicRenderSystem, replace glDrawElements with:
        OpenGL.Instance.DrawElements(OpenGL.GL_TRIANGLES, 3, OpenGL.GL_UNSIGNED_INT, 0);

        // In BasicRenderSystem, add glDeleteVertexArrays and glDeleteBuffers calls to clean up the hardcoded VAO and VBO at the end of the update function.
        OpenGL.Instance.DeleteVertexArrays(1, vaoId);
        OpenGL.Instance.DeleteBuffers(1, vboId);
        OpenGL.Instance.DeleteBuffers(1, eboId);
    }
}