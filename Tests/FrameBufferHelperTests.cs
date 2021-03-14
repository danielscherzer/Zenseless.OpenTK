using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Linq;

namespace Zenseless.OpenTK.Tests
{
	[TestClass()]
	public class FrameBufferHelperTests
	{
		private const int size = 256;

		private static T ExecuteOnOpenGL<T>(Func<T> action)
		{
			var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability, StartVisible = false })
			{
				Size = new Vector2i(size),
			};
			var result = action();
			window.Close();
			return result;
		}

		[TestMethod()]
		public void ToByteArrayTest()
		{
			static byte[] Execute()
			{
				GL.ClearColor(Color4.Gray);
				GL.Clear(ClearBufferMask.ColorBufferBit);
				return FrameBufferHelper.ToByteArray(0, 0, size, size);
			}
			var buffer = ExecuteOnOpenGL(Execute);
			Assert.IsTrue(buffer.All(value => value == 128));
			//var settings = new PixelReadSettings(size, size, StorageType.Char, PixelMapping.RGB);
			//using var image = new MagickImage(buffer, settings);
		}
	}
}