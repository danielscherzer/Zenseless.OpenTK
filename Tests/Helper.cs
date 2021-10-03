using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;

namespace Zenseless.OpenTK.Tests
{
	internal class Helper
	{
		internal static T ExecuteOnOpenGL<T>(Func<GameWindow, T> action, Version apiVersion, ContextFlags flags = ContextFlags.Debug, int width = 256, int height = 256)
		{
			var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings
			{
				Flags = flags,
				APIVersion = apiVersion,
				Profile = ContextProfile.Core,
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
		internal static T ExecuteOnOpenGL<T>(int width, int height, Func<GameWindow, T> action) => ExecuteOnOpenGL(action, new Version(4, 5), ContextFlags.Debug, width, height);
	}
}