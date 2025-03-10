namespace Mengine2D.graphics.meshes;

public static class VAOFactory
{

    public static VAO CreateVAO(float[] vertices, uint[] indices)
    {
        var vao = new VAO();
        vao.Bind();

        var vboVertices = BindVertices(vertices);
        OpenGL.Instance.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vboVertices);

        OpenGL.Instance.EnableVertexAttribArray(0);
        OpenGL.Instance.VertexAttribPointer(
            0,
            3,
            OpenGL.GL_FLOAT,
            false,
            3 * sizeof(float),
            0
        );

        OpenGL.Instance.EnableVertexAttribArray(1);
        OpenGL.Instance.VertexAttribPointer(
            1,
            2,
            OpenGL.GL_FLOAT,
            false,
            (int)Mesh.VERTEX_SIZE * sizeof(float),
            3 * sizeof(float)
        );

        var ibo = BindIndices(indices);
        OpenGL.Instance.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, ibo);

        Console.WriteLine($"Mesh.VERTEX_SIZE: {Mesh.VERTEX_SIZE}");
        Console.WriteLine($"VAO: {vao.Id}, VBO: {vboVertices}, EBO: {ibo}");
        Console.WriteLine("Vertex Data:");
        for (var i = 0; i < vertices.Length; i++)
        {
            Console.Write($"{vertices[i]} ");
        }
        Console.WriteLine();

        Console.WriteLine("Index Data:");
        for (var i = 0; i < indices.Length; i++)
        {
            Console.Write($"{indices[i]} ");
        }
        Console.WriteLine();

        vao.UnBind();

        vao.ElementCount = indices.Length;

        return vao;

    }

    private unsafe static uint BindVertices(float[] vertices)
    {
        var vbo = new uint[1];

        fixed (float* verticesPtr = vertices)
        {
            Console.WriteLine($"Vertices Buffer Size: {vertices.Length * sizeof(float)} bytes");
            OpenGL.Instance.GenBuffers(1, vbo);
            OpenGL.Instance.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vbo[0]);
            OpenGL.Instance.BufferData(
                OpenGL.GL_ARRAY_BUFFER,
                vertices.Length * sizeof(float),
                (nint)verticesPtr,
                OpenGL.GL_STATIC_DRAW
            );

            var error = OpenGL.Instance.GetError();
            if (error != OpenGL.GL_NO_ERROR)
            {
                Console.WriteLine($"OpenGL Error in BindVertices: {error}");
            }

        }

        return vbo[0];
    }

    private unsafe static uint BindIndices(uint[] indices)
    {
        var ibo = new uint[1];

        fixed (uint* indicesPtr = indices)
        {
            Console.WriteLine($"Indices Buffer Size: {indices.Length * sizeof(uint)} bytes");
            OpenGL.Instance.GenBuffers(1, ibo);
            OpenGL.Instance.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, ibo[0]);
            OpenGL.Instance.BufferData(
                OpenGL.GL_ELEMENT_ARRAY_BUFFER,
                indices.Length * sizeof(uint),
                (nint)indicesPtr,
                OpenGL.GL_STATIC_DRAW
            );

            var error = OpenGL.Instance.GetError();
            if (error != OpenGL.GL_NO_ERROR)
            {
                Console.WriteLine($"OpenGL Error in BindIndices: {error}");
            }
        }

        return ibo[0];
    }
}
