using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Zenseless.Resources;

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
				GL.DeleteShader(handle);
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
		//[HandleProcessCorruptedStateExceptions]
		//[SecurityCritical]
		public static ShaderProgram CompileLink(this ShaderProgram shaderProgram, IEnumerable<(ShaderType, string)> shaders)
		{
			List<int> shaderId = new();
			var unique = shaders.ToDictionary(data => data.Item1, data => data.Item2); // make sure each shader type is only present once
			if (unique.Count < 1) throw new ShaderProgramException("Empty set of shaders for shader program");
			foreach ((ShaderType type, string sourceCode) in unique)
			{
				var shader = CreateShader(type, sourceCode);
				GL.AttachShader(shaderProgram.Handle, shader);
				shaderId.Add(shader);
			}
			try
			{
				GL.LinkProgram(shaderProgram.Handle);
			}
			catch (Exception e)
			{
				// INTEL drivers have some serious problems with some aspects of GLSL (NVIDA works)
				throw new ShaderProgramException("Linker exception", e);
			}
			finally
			{
				foreach (var shader in shaderId)
				{
					GL.DeleteShader(shader);
				}
			}
			var log = shaderProgram.GetShaderProgramLog();
			if (!string.IsNullOrEmpty(log))
			{
				throw new ShaderProgramException(log);
			}
			return shaderProgram;
		}

		/// <summary>
		/// Compile shaders and link a shader program from a list of shader sources.
		/// </summary>
		/// <param name="shaderProgram">The shader program to link the shaders to.</param>
		/// <param name="shaders">A list of shader types and sources</param>
		/// <returns>The input shader program, but in a linked state.</returns>
		/// <exception cref="ShaderProgramException"/>
		/// <exception cref="ShaderException"/>
		//[HandleProcessCorruptedStateExceptions]
		//[SecurityCritical]
		public static ShaderProgram CompileLink(this ShaderProgram shaderProgram, params (ShaderType, string)[] shaders) => shaderProgram.CompileLink(shaders as IEnumerable<(ShaderType, string)>);

		/// <summary>
		/// Get a list of shader sourcecodes from a list of resource names
		/// </summary>
		/// <param name="resourceDirectory">The <see cref="IResourceDirectory"/>.</param>
		/// <param name="shaderTypeResourceName">A list of <see cref="ShaderType"/> and resource names.</param>
		public static List<(ShaderType, string)> GetShaderProgramSources(this IResourceDirectory resourceDirectory, IEnumerable<(ShaderType, string)> shaderTypeResourceName)
		{
			List<(ShaderType, string)> shaderTypeSourceTuples = new();
			foreach ((ShaderType type, string resourceName) in shaderTypeResourceName)
			{
				Trace.WriteLine($"Loading shader '{type}' from resource {resourceName}");
				string sourceCode = resourceDirectory.Resource(resourceName).AsString();
				shaderTypeSourceTuples.Add((type, sourceCode));
			}
			return shaderTypeSourceTuples;
		}

		/// <summary>
		/// Get a list of shader sourcecodes from a list of resource names. Shader types are infered from resource name extensions.
		/// (.frag,.vert, .geom, .tesc, .tese, .comp)
		/// </summary>
		/// <param name="resourceDirectory">The <see cref="IResourceDirectory"/>.</param>
		/// <param name="shaderResourceNames">The resource names of the shaders.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">If an invalid resource extensions is found.</exception>
		public static List<(ShaderType, string)> GetShaderProgramSources(this IResourceDirectory resourceDirectory, params string[] shaderResourceNames)
		{
			List<(ShaderType, string)> shaderTypeNameTuples = new();
			foreach (var shaderName in shaderResourceNames)
			{
				string ext = Path.GetExtension(shaderName);
				if (dicShaderExtensionTypeTuple.TryGetValue(ext[1..], out var type))
				{
					shaderTypeNameTuples.Add((type, shaderName));
				}
				else
				{
					throw new ArgumentException($"Invalid shader extension '{ext}' for '{shaderName}'");
				}
			}
			return resourceDirectory.GetShaderProgramSources(shaderTypeNameTuples);
		}

		/// <summary>
		/// Get a list of shader sourcecodes from a resource name without extension. Shader types are infered by adding well known shader extensions, like
		/// .frag,.vert, .geom, .tesc, .tese or .comp.
		/// </summary>
		/// <param name="resourceDirectory">The <see cref="IResourceDirectory"/>.</param>
		/// <param name="nameWithoutExtension">A resource name without extension.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">If no resource name with a well kknown shader extension is found.</exception>
		public static List<(ShaderType, string)> GetShaderProgramSources(this IResourceDirectory resourceDirectory, string nameWithoutExtension)
		{
			List<(ShaderType, string)> shaderTypeSourceTuples = new();
			foreach ((string extension, ShaderType type) in shaderExtensionTypeTuple)
			{
				string shaderName = $"{nameWithoutExtension}.{extension}";
				if (resourceDirectory.Exists(shaderName))
				{
					Trace.WriteLine($"Loading shader '{type}' from resource {shaderName}");
					string sourceCode = resourceDirectory.Resource(shaderName).AsString();
					shaderTypeSourceTuples.Add((type, sourceCode));
				}
			}
			if (0 == shaderTypeSourceTuples.Count) throw new ArgumentException($"Name '{nameWithoutExtension}' did not match any shaders in the resource directory.");
			return shaderTypeSourceTuples;
		}

		/// <summary>
		/// Create a <see cref="ShaderProgram"/> from a list of shader sources.
		/// </summary>
		/// <param name="shaderSources">A list of shader sources.</param>
		/// <returns>A <see cref="ShaderProgram"/></returns>
		public static ShaderProgram CreaterShaderProgram(this IEnumerable<(ShaderType, string)> shaderSources)
		{
			var shaderProgram = new ShaderProgram();
			return shaderProgram.CompileLink(shaderSources);
		}

		private static readonly (string, ShaderType)[] shaderExtensionTypeTuple = new (string, ShaderType)[]
{
			("frag", ShaderType.FragmentShader),
			("vert", ShaderType.VertexShader),
			("geom", ShaderType.GeometryShader),
			("tesc", ShaderType.TessControlShader),
			("tese", ShaderType.TessEvaluationShader),
			("comp", ShaderType.ComputeShader),
};

		private static readonly IReadOnlyDictionary<string, ShaderType> dicShaderExtensionTypeTuple =
			new Dictionary<string, ShaderType>(shaderExtensionTypeTuple.Select(v => new KeyValuePair<string, ShaderType>(v.Item1, v.Item2)));
	}
}