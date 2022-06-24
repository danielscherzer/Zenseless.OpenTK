using System;
using Zenseless.Patterns;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// Interface for all OpenGL objects that can be accessed via handle
	/// </summary>
	public interface IObjectHandle<TType> : IDisposable
	{
		/// <summary>
		/// Returns the OpenGL object handle
		/// </summary>
		Handle<TType> Handle { get; }
	}
}