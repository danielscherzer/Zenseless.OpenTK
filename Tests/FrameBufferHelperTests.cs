using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using System.Linq;

namespace Zenseless.OpenTK.Tests
{
	[TestClass()]
	public class FrameBufferHelperTests
	{
		[TestMethod(), TestCategory("OpenGL")]
		public void ToByteArrayTest()
		{
			const int res = 128;

			static byte[] Execute(GameWindow window)
			{
				GL.ClearColor(Color4.Gray);
				GL.Clear(ClearBufferMask.ColorBufferBit);
				return FrameBufferHelper.ToByteArray(0, 0, res, res);
			}
			var buffer = Helper.ExecuteOnOpenGL(res, res, Execute);
			Assert.IsTrue(buffer.All(value => value == 128));
			//var settings = new PixelReadSettings(size, size, StorageType.Char, PixelMapping.RGB);
			//using var image = new MagickImage(buffer, settings);
		}
	}
}