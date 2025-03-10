#version 330 core
out vec4 color;

in vec2 texCoordOut;
uniform sampler2D texture0;

void main()
{
	vec4 textureColor = texture(texture0, texCoordOut);
	vec4 resultColor = textureColor;

	color = resultColor;
}

// out vec4 color;

// void main()
// {
//     color = vec4(1.0, 0.0, 0.0, 1.0); // Red color
// }