using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;
using Zenseless.Patterns;

namespace Zenseless.OpenTK;

/// <summary>
/// <see cref="ShaderProgram"/> extension methods to work with attributes and uniforms.
/// </summary>
public static class ShaderProgramExtensions
{
	/// <summary>
	/// Gets the attribute location for the given shader program and name. Writes a message to the <see cref="Trace"/> output if the attribute name is unknown.
	/// </summary>
	/// <param name="shaderProgram">A <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The name of the attribute.</param>
	/// <returns>A location.</returns>
	public static int GetCheckedAttributeLocation(this ShaderProgram shaderProgram, string name)
	{
		var location = GL.GetAttribLocation(shaderProgram.Handle, name);
		Trace.WriteLineIf(-1 == location, $"Attribute '{name}' not found in shader program {shaderProgram.GetType().Name}({shaderProgram.Handle})");
		return location;
	}

	/// <summary>
	/// Gets the shader storage block location for the given shader program and name. Writes a message to the <see cref="Trace"/> output if the name is unknown.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The name of the uniform.</param>
	/// <returns>A location.</returns>
	public static int GetCheckedShaderStorageBlockLocation(this ShaderProgram shaderProgram, string name)
	{
		var location = GL.GetProgramResourceIndex(shaderProgram.Handle, ProgramInterface.ShaderStorageBlock, name);
		Trace.WriteLineIf(-1 == location, $"Shader storage block '{name}' not found in shader program {shaderProgram.GetType().Name}({shaderProgram.Handle})");
		return location;
	}

	/// <summary>
	/// Gets the uniform location for the given shader program and name. Writes a message to the <see cref="Trace"/> output if the uniform name is unknown.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The name of the uniform.</param>
	/// <returns>A location.</returns>
	public static int GetCheckedUniformLocation(this ShaderProgram shaderProgram, string name)
	{
		var location = GL.GetUniformLocation(shaderProgram.Handle, name);
		Trace.WriteLineIf(-1 == location, $"Uniform '{name}' not found in shader program {shaderProgram.GetType().Name}({shaderProgram.Handle})");
		return location;
	}

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="location">The uniforms location.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, int location, uint value)
	{
		GL.ProgramUniform1(shaderProgram.Handle, location, value);
	}

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="location">The uniforms location.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, int location, int value)
	{
		GL.ProgramUniform1(shaderProgram.Handle, location, value);
	}

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="location">The uniforms location.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, int location, float value)
	{
		GL.ProgramUniform1(shaderProgram.Handle, location, value);
	}

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="location">The uniforms location.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, int location, float[] value)
	{
		GL.ProgramUniform1(shaderProgram.Handle, location, value.Length, value.ToFloatArray());
	}

	/// <summary>
	/// Sets a samplers texture unit for a given shader program.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="location">The samplers location.</param>
	/// <param name="textureUnit">The texture unit the samples should be using.</param>
	public static void SamplerUnit(this ShaderProgram shaderProgram, int location, int textureUnit) => shaderProgram.Uniform(location, textureUnit);

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The uniforms name.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, string name, uint value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The uniforms name.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, string name, int value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The uniforms name.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, string name, float value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The uniforms name.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, string name, float[] value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);

	/// <summary>
	/// Sets a samplers texture unit for a given shader program.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The samplers name.</param>
	/// <param name="textureUnit">The texture unit the samples should be using.</param>
	public static void SamplerUnit(this ShaderProgram shaderProgram, string name, int textureUnit) => shaderProgram.SamplerUnit(shaderProgram.GetCheckedUniformLocation(name), textureUnit);
}
