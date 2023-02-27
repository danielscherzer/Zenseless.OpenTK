using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Zenseless.Patterns;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// A debugger for OpenGL needs an OpenGL context created with the debug flag
	/// </summary>
	public class DebugOutputGL : Disposable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DebugOutputGL"/> class.
		/// </summary>
		/// <param name="filterDebugSeverity">The lowest severity type that should cause a <see cref="DebugEvent"/></param>
		public DebugOutputGL(DebugSeverity filterDebugSeverity = DebugSeverity.DebugSeverityLow)
		{
			Version version = new(GL.GetInteger(GetPName.MajorVersion), GL.GetInteger(GetPName.MinorVersion));
			if (version < new Version(4, 3)) throw new OpenGLException("OpenGL version too low for debug output. Use glError instead.");
			ContextFlagMask flags = (ContextFlagMask)GL.GetInteger(GetPName.ContextFlags);
			var debugFlag = ContextFlagMask.ContextFlagDebugBit & flags;
			if (0 == debugFlag) throw new OpenGLException("OpenGL context is not in debug mode.");
			_debugCallback = DebugCallback; //need to keep an instance, otherwise delegate is garbage collected
			GL.Enable(EnableCap.DebugOutput);
			GL.Enable(EnableCap.DebugOutputSynchronous);
			GL.DebugMessageCallback(_debugCallback, IntPtr.Zero);
			switch (filterDebugSeverity)
			{
				case DebugSeverity.DebugSeverityHigh:
					_filter.Add(DebugSeverity.DebugSeverityHigh);
					break;
				case DebugSeverity.DebugSeverityMedium:
					_filter.Add(DebugSeverity.DebugSeverityHigh);
					_filter.Add(DebugSeverity.DebugSeverityMedium);
					break;
				case DebugSeverity.DebugSeverityLow:
					_filter.Add(DebugSeverity.DebugSeverityHigh);
					_filter.Add(DebugSeverity.DebugSeverityMedium);
					_filter.Add(DebugSeverity.DebugSeverityLow);
					break;
				case DebugSeverity.DebugSeverityNotification:
					_filter.Add(DebugSeverity.DebugSeverityHigh);
					_filter.Add(DebugSeverity.DebugSeverityMedium);
					_filter.Add(DebugSeverity.DebugSeverityLow);
					_filter.Add(DebugSeverity.DebugSeverityNotification);
					break;
				case DebugSeverity.DontCare:
					_filter.Add(DebugSeverity.DebugSeverityHigh);
					_filter.Add(DebugSeverity.DebugSeverityMedium);
					_filter.Add(DebugSeverity.DebugSeverityLow);
					_filter.Add(DebugSeverity.DebugSeverityNotification);
					_filter.Add(DebugSeverity.DontCare);
					break;
			}
		}

		/// <summary>
		/// Event will be called each time a debug event occurs
		/// </summary>
		public event EventHandler<DebugEventArgs>? DebugEvent;

		/// <summary>
		/// Will be called from the default Dispose method. Implementers should dispose all their resources her.
		/// </summary>
		protected override void DisposeResources()
		{
			GL.Disable(EnableCap.DebugOutput);
			GL.Disable(EnableCap.DebugOutputSynchronous);
			GL.DebugMessageCallback(null, IntPtr.Zero);
		}

		private readonly DebugProc _debugCallback; //need to keep an instance, otherwise delegate is garbage collected
		private readonly HashSet<DebugSeverity> _filter = new();

		private void DebugCallback(DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
		{
			if (!_filter.Contains(severity)) return;
			var errorMessage = Marshal.PtrToStringAnsi(message, length);
			DebugEventArgs e = new(source, type, id, severity, errorMessage);
			if(DebugEvent is null)
			{
				throw new OpenGLException($"[{e.Severity}, {e.Id}]: {e.Message} [Type:{e.Type}] from [{e.Source}]");
			}
			else
			{
				DebugEvent.Invoke(this, e);
			}
		}
	}
}
