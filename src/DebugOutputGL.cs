using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Zenseless.Patterns;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// A debugger for OpenGL needs an OpenGL context created with the debug flag
	/// </summary>
	public class DebugOutputGL : Disposable
	{
		private readonly DebugProc debugCallback;

		/// <summary>
		/// Initializes a new instance of the <see cref="DebugOutputGL"/> class.
		/// </summary>
		public DebugOutputGL(DebugSeverityControl debugSeverityControl = DebugSeverityControl.DebugSeverityLow)
		{
			Trace.WriteLine($"{GL.GetString(StringName.Renderer)} running OpenGL " +
				$"Version {GL.GetInteger(GetPName.MajorVersion)}.{GL.GetInteger(GetPName.MinorVersion)} with ");
			Trace.WriteLine($"Shading Language Version {GL.GetString(StringName.ShadingLanguageVersion)}");
			GL.Enable(EnableCap.DebugOutput);
			GL.Enable(EnableCap.DebugOutputSynchronous);
			Trace.WriteLine(GL.GetString(StringName.Extensions));
			debugCallback = DebugCallback; //need to keep an instance, otherwise delegate is garbage collected
			GL.DebugMessageCallback(debugCallback, IntPtr.Zero);
			GL.DebugMessageControl(DebugSourceControl.DontCare, DebugTypeControl.DontCare, debugSeverityControl, 0, Array.Empty<int>(), true);
		}

		/// <summary>
		/// Will be called from the default Dispose method. Implementers should dispose all their resources her.
		/// </summary>
		protected override void DisposeResources()
		{
			GL.Disable(EnableCap.DebugOutput);
			GL.Disable(EnableCap.DebugOutputSynchronous);
			GL.DebugMessageCallback(null, IntPtr.Zero);
		}

		private void DebugCallback(DebugSource source, DebugType type, int id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
		{
			var errorMessage = Marshal.PtrToStringAnsi(message, length);
			var meta = $"OpenGL {type} from {source} with id={id} of {severity} with message";
			Trace.WriteLine(meta);
			Trace.Indent();
			Trace.WriteLine(errorMessage);
			Trace.Unindent();
			if (DebugSeverity.DebugSeverityHigh == severity) Debugger.Break();
		}
	}
}
