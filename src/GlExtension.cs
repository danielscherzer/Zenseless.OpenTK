using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// Class for OpenGL extension queries
	/// </summary>
	public static class GlExtension
	{
		private static readonly HashSet<string> _extensions = new();

		/// <summary>
		/// Returns a list of all extensions supported by the current OpenGL context. This is a <see cref="IReadOnlySet{T}"/> of lowercase strings.
		/// This list is generated at the first call.
		/// </summary>
		public static IReadOnlySet<string> List
		{
			get
			{
				if (0 == _extensions.Count) UpdateExtensions();
				return _extensions;
			}
		}

		/// <summary>
		/// Check if an extensions given by its canonical name is supported by the current OpenGL context
		/// </summary>
		/// <param name="extensionName">The canonical extension name from the registry (https://www.khronos.org/registry/OpenGL/extensions/)</param>
		public static bool IsSupported(string extensionName) => List.Contains(extensionName.ToLowerInvariant());

		private static void UpdateExtensions()
		{
			int n = GL.GetInteger(GetPName.NumExtensions);
			for (int i = 0; i < n; ++i)
			{
				string extension = GL.GetString(StringNameIndexed.Extensions, i);
				_extensions.Add(extension.ToLowerInvariant());
			}
		}
	}
}
