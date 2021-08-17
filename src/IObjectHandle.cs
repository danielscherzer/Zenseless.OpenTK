using System;
using Zenseless.Patterns;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// Interface for all OpenGL objects that can be accessed via handle
	/// </summary>
	public interface IObjectHandle<Type> : IDisposable
	{
		/// <summary>
		/// Returns the OpenGL object handle
		/// </summary>
		Handle<Type> Handle { get; }
	}
}