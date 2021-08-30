using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;

namespace Zenseless.OpenTK.Tests
{
	class Helper
	{
		internal static T ExecuteOnOpenGL<T>(int width, int height, Func<T> action)
		{
			var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings 
			{
				Flags = ContextFlags.Debug,
				APIVersion = new Version(4, 5),
				Profile = ContextProfile.Compatability,
				//Profile = ContextProfile.Core,
				StartVisible = false
			})
			{
				Size = new Vector2i(width, height),
			};
			DebugOutputGL debugOutput = new();
			var result = action();
			debugOutput.Dispose();
			window.Close();
			return result;
		}
	}
}
