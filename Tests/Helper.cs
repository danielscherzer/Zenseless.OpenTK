using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;

namespace Zenseless.OpenTK.Tests;

internal class Helper
{
	internal static T ExecuteOnOpenGL<T>(Func<GameWindow, T> action, Version apiVersion, int width = 256, int height = 256)
	{
		GLFWProvider.CheckForMainThread = false; // https://github.com/opentk/opentk/issues/1206
		var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings
		{
			Flags = ContextFlags.Debug,
			APIVersion = apiVersion,
			Profile = ContextProfile.Core,
			StartVisible = false
		})
		{
			ClientRectangle = new Box2i(0, 0, width, height),
		};
		DebugOutputGL debugOutput = new();
		var result = action(window);
		window.SwapBuffers();
		debugOutput.Dispose();
		window.Close();
		return result;
	}

	internal static T ExecuteOnOpenGLIM<T>(Func<GameWindow, T> action, Version apiVersion, int width = 256, int height = 256)
	{
		GLFWProvider.CheckForMainThread = false; // https://github.com/opentk/opentk/issues/1206
		var settings = ImmediateMode.NativeWindowSettings;
		settings.APIVersion = apiVersion;
		settings.StartVisible = false;

		var window = new GameWindow(GameWindowSettings.Default, settings)
		{
			ClientRectangle = new Box2i(0, 0, width, height),
		};
		var result = action(window);
		window.SwapBuffers();
		window.Close();
		return result;
	}

	internal static T ExecuteOnOpenGL<T>(Func<GameWindow, T> action) => ExecuteOnOpenGL(action, new Version(4, 5));

	internal static T ExecuteOnOpenGL<T>(int width, int height, Func<GameWindow, T> action) => ExecuteOnOpenGL(action, new Version(4, 5), width, height);
}