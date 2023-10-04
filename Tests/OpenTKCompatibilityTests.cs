using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Linq;

namespace Zenseless.OpenTK.Tests;

[TestClass()]
public class OpenTKCompatibilityTests
{
	[TestMethod(), TestCategory("OpenGL")]
	public void QuadTest()
	{
		Helper.ExecuteOnOpenGLIM(window =>
		{
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
			return 0;
		}, new Version(3, 3), 128, 128);
	}
}