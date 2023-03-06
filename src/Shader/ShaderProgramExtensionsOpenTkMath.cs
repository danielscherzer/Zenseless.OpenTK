using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Zenseless.Patterns;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// <see cref="ShaderProgram"/> extension methods to work with attributes and uniforms.
	/// </summary>
	public static class ShaderProgramExtensionsOpenTkMath
	{
		/// <summary>
		/// Sets an uniform of a given shader program to the given value.
		/// </summary>
		/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
		/// <param name="location">The uniforms location.</param>
		/// <param name="value">The new value to set.</param>
		public static void Uniform(this ShaderProgram shaderProgram, int location, Vector2 value)
		{
			GL.ProgramUniform2(shaderProgram.Handle, location, value);
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
			GL.ProgramUniform3(shaderProgram.Handle, location, value);
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
		public static void Uniform(this ShaderProgram shaderProgram, int location, Color4 value)
		{
			GL.ProgramUniform4(shaderProgram.Handle, location, value);
		}

		/// <summary>
		/// Sets an uniform of a given shader program to the given value.
		/// </summary>
		/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
		/// <param name="location">The uniforms location.</param>
		/// <param name="value">The new value to set.</param>
		public static void Uniform(this ShaderProgram shaderProgram, int location, Vector4 value)
		{
			GL.ProgramUniform4(shaderProgram.Handle, location, value);
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
		public static void Uniform(this ShaderProgram shaderProgram, int location, Matrix4 value)
		{
			GL.ProgramUniformMatrix4(shaderProgram.Handle, location, false, ref value);
		}

		/// <summary>
		/// Sets an uniform of a given shader program to the given value.
		/// </summary>
		/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
		/// <param name="location">The uniforms location.</param>
		/// <param name="value">The new value to set.</param>
		public static void Uniform(this ShaderProgram shaderProgram, int location, Matrix4[] value)
		{
			GL.ProgramUniformMatrix4(shaderProgram.Handle, location, value.Length, false, value.ToFloatArray());
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
		public static void Uniform(this ShaderProgram shaderProgram, string name, Color4 value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);

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
		public static void Uniform(this ShaderProgram shaderProgram, string name, Matrix4 value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);

		/// <summary>
		/// Sets an uniform of a given shader program to the given value.
		/// </summary>
		/// <param name="shaderProgram">The <see cref="ShaderProgram"/>.</param>
		/// <param name="name">The uniforms name.</param>
		/// <param name="value">The new value to set.</param>
		public static void Uniform(this ShaderProgram shaderProgram, string name, Matrix4[] value) => shaderProgram.Uniform(shaderProgram.GetCheckedUniformLocation(name), value);
	}
}
