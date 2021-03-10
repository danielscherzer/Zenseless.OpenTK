using System;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// Interface for all OpenGL objects that can be accessed via handle
	/// </summary>
	public interface IObjectGL : IDisposable
	{
		/// <summary>
		/// Returns the OpenGL object handle
		/// </summary>
		int Handle { get; }
	}
}