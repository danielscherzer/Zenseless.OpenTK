using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Linq;

namespace Zenseless.OpenTK.Tests;

[TestClass()]
public class OpenTKCompatibilityTests
{
	[TestMethod(), TestCategory("OpenGL")]
	public void QuadTest()
	{
		GLFWProvider.CheckForMainThread = false; // https://github.com/opentk/opentk/issues/1206
		var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings
		{
			Profile = ContextProfile.Compatability,
			StartVisible = false
		})
		{
			Size = new Vector2i(128, 128),
		};
		GL.Viewport(0, 0, window.Size.X, window.Size.Y);
		GL.ClearColor(0f, 0f, 0f, 0f);
		GL.Clear(ClearBufferMask.ColorBufferBit);
		GL.Color4(Color4.White);
		//draw a quad
		GL.Begin(PrimitiveType.Quads);
		GL.Vertex2(-1f, -1f);
		GL.Vertex2(1f, -1f);
		GL.Vertex2(1f, 1f);
		GL.Vertex2(-1f, 1f);
		GL.End();
		var result = FrameBufferHelper.ToByteArray(0, 0, 128, 128);
		Assert.IsTrue(result.All(value => value == 255));
		window.Close();
	}
}