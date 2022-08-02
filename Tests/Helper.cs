using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;

namespace Zenseless.OpenTK.Tests
{
	internal class Helper
	{
		internal static T ExecuteOnOpenGL<T>(Func<GameWindow, T> action, Version apiVersion, int width = 256, int height = 256, ContextProfile profile = ContextProfile.Core)
		{
			GLFWProvider.CheckForMainThread = false; // https://github.com/opentk/opentk/issues/1206
			var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings
			{
				Flags = ContextFlags.Debug,
				APIVersion = apiVersion,
				Profile = profile,
				StartVisible = false
			})
			{
				Size = new Vector2i(width, height),
			};
			DebugOutputGL debugOutput = new();
			var result = action(window);
			debugOutput.Dispose();
			window.Close();
			return result;
		}
		internal static T ExecuteOnOpenGL<T>(Func<GameWindow, T> action) => ExecuteOnOpenGL(action, new Version(4, 5));
		internal static T ExecuteOnOpenGL<T>(int width, int height, Func<GameWindow, T> action) => ExecuteOnOpenGL(action, new Version(4, 5), width, height);
	}
}