using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// Extension methods to load and compile, link shader programs.
	/// </summary>
	public static class ShaderProgramLoader
	{
		/// <summary>
		/// Creates and compiles a shader.
		/// </summary>
		/// <param name="shaderType">The shader type.</param>
		/// <param name="shaderSource">The sourcce code of the shader.</param>
		/// <returns>OpenGL handle of the shader.</returns>
		/// <exception cref="ShaderException"/>
		public static int CreateShader(ShaderType shaderType, string shaderSource)
		{
			var handle = GL.CreateShader(shaderType);
			GL.ShaderSource(handle, shaderSource);
			GL.CompileShader(handle);
			var log = GetShaderLog(handle);
			if (!string.IsNullOrEmpty(log))
			{
				throw new ShaderException(shaderType, log);
			}
			return handle;
		}

		/// <summary>
		/// Returns the compilation log of a shader
		/// </summary>
		/// <param name="shader">OpenGL handle of the shader.</param>
		/// <returns>The text of the log or <see cref="string.Empty"/> if no log was produced.</returns>
		public static string GetShaderLog(int shader)
		{
			GL.GetShader(shader, ShaderParameter.CompileStatus, out int status_code);
			if (1 == status_code)
			{
				return string.Empty;
			}
			else
			{
				return GL.GetShaderInfoLog(shader);
			}
		}

		/// <summary>
		/// Returns the compilation log of a <see cref="ShaderProgram"/>.
		/// </summary>
		/// <param name="shaderProgram">A <see cref="ShaderProgram"/>.</param>
		/// <returns>The text of the log or <see cref="string.Empty"/> if no log was produced.</returns>
		public static string GetShaderProgramLog(this ShaderProgram shaderProgram)
		{
			GL.GetProgram(shaderProgram.Handle, GetProgramParameterName.LinkStatus, out int status_code);
			if (1 == status_code)
			{
				return string.Empty;
			}
			else
			{
				return GL.GetProgramInfoLog(shaderProgram.Handle);
			}
		}

		/// <summary>
		/// Compile shaders and link a shader program from a list of shader sources.
		/// </summary>
		/// <param name="shaderProgram">The shader program to link the shaders to.</param>
		/// <param name="shaders">A list of shader types and sources</param>
		/// <returns>The input shader program, but in a linked state.</returns>
		/// <exception cref="ShaderProgramException"/>
		/// <exception cref="ShaderException"/>
		public static ShaderProgram CompileLink(this ShaderProgram shaderProgram, IEnumerable<(ShaderType, string)> shaders)
		{
			var shaderId = new List<int>();
			var unique = shaders.ToDictionary(data => data.Item1, data => data.Item2); // make sure each shader type is only present once
			if (unique.Count < 1) throw new ShaderProgramException("Empty set of shaders for shader program");
			foreach ((ShaderType type, string sourceCode) in unique)
			{
				var shader = CreateShader(type, sourceCode);
				GL.AttachShader(shaderProgram.Handle, shader);
				shaderId.Add(shader);
			}
			GL.LinkProgram(shaderProgram.Handle);
			foreach (var shader in shaderId)
			{
				GL.DeleteShader(shader);
			}
			var log = shaderProgram.GetShaderProgramLog();
			if (!string.IsNullOrEmpty(log))
			{
				throw new ShaderProgramException(log);
			}
			return shaderProgram;
		}
	}
}
