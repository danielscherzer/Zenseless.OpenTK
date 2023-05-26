using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// Contains helper settings to enable the old immediate mode for OpenGL
	/// </summary>
	public static class ImmediateMode
	{
		/// <summary>
		/// Returns an instance of <see cref="NativeWindowSettings"/> for immediate mode rendering
		/// </summary>
		public static NativeWindowSettings NativeWindowSettings => new() { Flags = ContextFlags.Default, Profile = ContextProfile.Compatability };
	}
}
