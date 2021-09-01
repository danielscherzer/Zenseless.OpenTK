using OpenTK.Graphics.OpenGL4;
using System;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// Event data for a debug event
	/// </summary>
	public class DebugEventArgs : EventArgs
	{
		/// <summary>
		/// Create a new instance
		/// </summary>
		/// <param name="source">The <see cref="DebugSource"/> for this debug message.</param>
		/// <param name="type">The <see cref="DebugType"/> for this debug message.</param>
		/// <param name="id">The id for this debug message.</param>
		/// <param name="severity">The <see cref="DebugSeverity"/> for this debug message.</param>
		/// <param name="message">The debug message.</param>
		public DebugEventArgs(DebugSource source, DebugType type, int id, DebugSeverity severity, string message)
		{
			Source = source;
			Type = type;
			Id = id;
			Severity = severity;
			Message = message;
		}

		/// <summary>
		/// The <see cref="DebugSource"/> for this debug message.
		/// </summary>
		public DebugSource Source { get; }

		/// <summary>
		/// The <see cref="DebugType"/> for this debug message.
		/// </summary>
		public DebugType Type { get; }

		/// <summary>
		/// The id for this debug message.
		/// </summary>
		public int Id { get; }

		/// <summary>
		/// The <see cref="DebugSeverity"/> for this debug message.
		/// </summary>
		public DebugSeverity Severity { get; }

		/// <summary>
		/// The debug message.
		/// </summary>
		public string Message { get; }
	}
}