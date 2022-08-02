using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;

namespace Zenseless.OpenTK.Tests
{
	[TestClass()]
	public class ViewportTests
	{

		[DataTestMethod(), TestCategory("OpenGL")]
		[DataRow(512, 512, 1f)]
		[DataRow(1024, 512, 0.5f)]
		public void InvAspectRatioTest(int width, int height, float invAspect)
		{
			Viewport viewport = new();
			Helper.ExecuteOnOpenGL(width, height, window =>
			{
				viewport.Resize(window.Size.X, window.Size.Y);
				Assert.AreEqual(invAspect, viewport.InvAspectRatio);
				return 0;
			});
		}

		[TestMethod(), TestCategory("OpenGL")]
		public void InvViewportMatrixTest()
		{
			Viewport viewport = new();
			Helper.ExecuteOnOpenGL(1024, 512, window =>
			{
				viewport.Resize(window.Size.X, window.Size.Y);
				// check corners
				Assert.AreEqual(new Vector2(-1, 1), Vector2.Zero.Transform(viewport.InvViewportMatrix));
				Assert.AreEqual(new Vector2(1, -1), new Vector2(window.Size.X - 1, window.Size.Y - 1).Transform(viewport.InvViewportMatrix));
				Assert.AreEqual(new Vector2(-1, -1), new Vector2(0f, window.Size.Y - 1).Transform(viewport.InvViewportMatrix));
				Assert.AreEqual(new Vector2(1, 1), new Vector2(window.Size.X - 1, 0).Transform(viewport.InvViewportMatrix));
				return 0;
			});
		}
	}
}