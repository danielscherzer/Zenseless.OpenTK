using OpenTK.Graphics.OpenGL4;
using System;
using System.Numerics;
using Zenseless.Patterns;

namespace Zenseless.OpenTK;

/// <summary>
/// <see cref="ShaderProgram"/> extension methods to work with attributes and uniforms.
/// </summary>
public static class ShaderProgramExtensionsSysNum
{
	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="location">The uniforms location.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, int location, Vector2 value)
	{
		GL.ProgramUniform2(shaderProgram.Handle, location, value.X, value.Y);
	}

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="location">The uniforms location.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, int location, Vector2[] value)
	{
		GL.ProgramUniform2(shaderProgram.Handle, location, value.Length, value.ToFloatArray());
	}

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="location">The uniforms location.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, int location, Vector3 value)
	{
		GL.ProgramUniform3(shaderProgram.Handle, location, value.X, value.Y, value.Z);
	}

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="location">The uniforms location.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, int location, Vector3[] value)
	{
		GL.ProgramUniform3(shaderProgram.Handle, location, value.Length, value.ToFloatArray());
	}

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="location">The uniforms location.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, int location, Vector4 value)
	{
		GL.ProgramUniform4(shaderProgram.Handle, location, value.X, value.Y, value.Z, value.W);
	}

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="location">The uniforms location.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, int location, Vector4[] value)
	{
		GL.ProgramUniform4(shaderProgram.Handle, location, value.Length, value.ToFloatArray());
	}

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="location">The uniforms location.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, int location, Matrix4x4 value)
	{
		//TODO: Add test code
		//global::OpenTK.Mathematics.Matrix4 m4 = new(m.M11, m.M12, m.M13, m.M14, m.M21, m.M22, m.M23, m.M24, m.M31, m.M32, m.M33, m.M34, m.M41, m.M42, m.M43, m.M44);
		GL.ProgramUniformMatrix4(shaderProgram.Handle, location, 1, false, value.ToArray());
	}

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="location">The uniforms location.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, int location, Matrix4x4[] value)
	{
		//TODO: Add test code
		// warning Matrix4x4 has a weird order of its cells
		float[] buffer = new float[16 * value.Length];
		for (int i = 0; i < value.Length; i++)
		{
			var m = value[i].ToArray();
			Array.Copy(m, 0, buffer, i * 16, 16);
		}
		GL.ProgramUniformMatrix4(shaderProgram.Handle, location, value.Length, false, buffer);
	}

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The uniforms name.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, string name, Vector2 value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The uniforms name.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, string name, Vector2[] value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The uniforms name.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, string name, Vector3 value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The uniforms name.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, string name, Vector3[] value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The uniforms name.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, string name, Vector4 value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The uniforms name.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, string name, Vector4[] value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The uniforms name.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, string name, Matrix4x4 value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);

	/// <summary>
	/// Sets an uniform of a given shader program to the given value.
	/// </summary>
	/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
	/// <param name="name">The uniforms name.</param>
	/// <param name="value">The new value to set.</param>
	public static void Uniform(this ShaderProgram shaderProgram, string name, Matrix4x4[] value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);
}
