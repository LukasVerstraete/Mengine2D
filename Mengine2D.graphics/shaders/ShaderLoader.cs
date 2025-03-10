using System.Text;

namespace Mengine2D.graphics.shaders;

public static class ShaderLoader
{
    private static readonly Dictionary<string, Shader> Shaders = [];

    public static Shader LoadShader(string shaderFolderPath, string shaderName)
    {
        var gl = OpenGL.Instance;

        var shaderNameNormalized = shaderName.ToLower();

        var vertexShaderPath = $"{shaderFolderPath}/{shaderNameNormalized}_vertex.shader";
        var vertexShaderSource = LoadShaderSource(new FileInfo(vertexShaderPath));
        var vertexShaderHandle = CreateShaderProgram(ShaderType.VertexShader, vertexShaderSource);

        var fragmentShaderPath = $"{shaderFolderPath}/{shaderNameNormalized}_fragment.shader";
        var fragmentShaderSource = LoadShaderSource(new FileInfo(fragmentShaderPath));
        var fragmentShaderHandle = CreateShaderProgram(ShaderType.FragmentShader, fragmentShaderSource);

        var shaderHandle = gl.CreateProgram();
        gl.AttachShader(shaderHandle, vertexShaderHandle);
        gl.AttachShader(shaderHandle, fragmentShaderHandle);
        gl.LinkProgram(shaderHandle);

        CleanUp(shaderHandle, vertexShaderHandle, fragmentShaderHandle);
        var shader = new Shader(shaderHandle);

        Shaders.Add(shaderName, shader);

        return shader;
    }

    public static Shader GetShader(string shaderName)
    {
        Shaders.TryGetValue(shaderName, out var shader);

        return shader is null ? throw new ArgumentException($"Missing shader :{shaderName}") : shader;
    }

    private static string LoadShaderSource(FileInfo fileInfo)
    {
        string shaderSource;
        using (var reader = fileInfo.OpenText())
        {
            shaderSource = reader.ReadToEnd();
        }
        return shaderSource;
    }

    private static uint CreateShaderProgram(ShaderType shaderType, string source)
    {
        var shaderHandle = OpenGL.Instance.CreateShader((uint)shaderType);
        OpenGL.Instance.ShaderSource(shaderHandle, source);
        OpenGL.Instance.CompileShader(shaderHandle);

        var parameters = new int[1];
        OpenGL.Instance.GetShader(shaderHandle, OpenGL.GL_INFO_LOG_LENGTH, parameters);
        var compileErrors = new StringBuilder();
        var compileErrorsLength = 0;
        OpenGL.Instance.GetShaderInfoLog(shaderHandle, parameters[0], compileErrorsLength, compileErrors);
        if (compileErrors.Length != 0)
        {
            throw new Exception(@$"
                Error occurder while trying to load shader of type {shaderType},
                {compileErrors}
            ");
        }
        return shaderHandle;
    }

    private static void CleanUp(uint shaderHandle, uint vertexShaderHandle, uint fragmentShaderHandle)
    {
        OpenGL.Instance.DetachShader(shaderHandle, vertexShaderHandle);
        OpenGL.Instance.DetachShader(shaderHandle, fragmentShaderHandle);
        OpenGL.Instance.DeleteShader(vertexShaderHandle);
        OpenGL.Instance.DeleteShader(fragmentShaderHandle);
    }
}
