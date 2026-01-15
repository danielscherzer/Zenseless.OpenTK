using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace Zenseless.OpenTK.Tests;

[TestClass()]
public class QueryTests
{
	[TestMethod(), TestCategory("OpenGL")]
	public void QueryTest()
	{
		//TODO: Make test run in Core profile, not IM
		Helper.ExecuteOnOpenGLIM(window =>
		{
			GL.Enable(EnableCap.DepthTest);
			GL.Clear(ClearBufferMask.DepthBufferBit);
			Query queryA = new(QueryTarget.SamplesPassed);
			Query queryB = new(QueryTarget.SamplesPassed);
			queryA.Begin();
			DrawBox(0f, 0f, 1f, 1f, 0f);
			queryA.End();
			Assert.IsGreaterThan(0, queryA.Result);
			queryB.Begin();
			DrawBox(0f, 0f, 1f, 1f, 0.5f);
			queryB.End();
			Assert.AreEqual(0, queryB.Result);
			return 0;
		}, new Version(4, 3), 512, 512);
	}

	private static void DrawBox(float minX, float minY, float maxX, float maxY, float depth)
	{
		Vector3[] points =
		[
			new Vector3(maxX, minY, depth),
			new Vector3(maxX, maxY, depth),
			new Vector3(minX, minY, depth),
			new Vector3(minX, maxY, depth)
		];
		GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, points);
		GL.EnableVertexAttribArray(0);
		GL.DrawArrays(PrimitiveType.TriangleStrip, 0, points.Length); // draw with vertex array data
		GL.DisableVertexAttribArray(0);
	}
}