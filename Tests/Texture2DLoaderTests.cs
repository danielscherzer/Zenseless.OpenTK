using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using Zenseless.Resources;
using GLold = OpenTK.Graphics.OpenGL.GL;

namespace Zenseless.OpenTK.Tests;

[TestClass()]
public class Texture2DLoaderTests
{
	[DataTestMethod(), TestCategory("OpenGL")]
	[DataRow(".rgb.jpg", 115, 101, SizedInternalFormat.Rgb8)]
	[DataRow("srgb.jpg", 171, 153, SizedInternalFormat.Rgb8)]
	[DataRow("grayscale.png", 533, 657, SizedInternalFormat.R8)]
	[DataRow("paletteAlpha.png", 320, 224, SizedInternalFormat.Rgba8)]
	[DataRow("rgba.png", 320, 224, SizedInternalFormat.Rgba8)]
	public void LoadImageTest(string name, int width, int height, SizedInternalFormat expectedFormat)
	{
		var resourceDirectory = new ShortestMatchResourceDirectory(new EmbeddedResourceDirectory());
		_ = Helper.ExecuteOnOpenGLIM(window =>
		{
			using var stream = resourceDirectory.Resource(name).Open();
			using var image = new MagickImage(stream);
			using var tex = Texture2DLoader.LoadTexture(image);
			var pixels = image.GetPixelsUnsafe().ToArray();
			Assert.IsNotNull(pixels);
			Assert.AreEqual(width, tex.Width);
			Assert.AreEqual(height, tex.Height);
			var format = tex.InternalFormat();
			Assert.AreEqual(expectedFormat, format);
			var pixelFormat = TextureExtensions.PixelFormatFrom(format);
			var channelCount = TextureExtensions.ChannelCountFrom(pixelFormat);
			// copy texture back to buffer
			byte[] buffer = new byte[width * height * channelCount];
			GL.PixelStore(PixelStoreParameter.PackAlignment, 1); // some image sizes will cause memory acceptions otherwise
			GL.GetTextureImage(tex.Handle, 0, pixelFormat, PixelType.UnsignedByte, buffer.Length, buffer);
			// Are buffers the same?
			CollectionAssert.AreEqual(pixels, buffer);
			// render texture
			RenderTexture(tex, pixels, pixelFormat, channelCount);
			return 0;
		}, new Version(3, 3), width, height);
	}

	private static void RenderTexture(Texture2D tex, byte[] pixels, PixelFormat pixelFormat, byte channelCount)
	{
		GL.Viewport(0, 0, tex.Width, tex.Height);
		GL.ClearColor(0f, 0f, 0f, 0f);
		GL.Clear(ClearBufferMask.ColorBufferBit);
		GL.Enable(EnableCap.Texture2D);
		tex.Bind();

		GLold.Color4(Color4.White);
		//draw a quad
		GLold.Begin(global::OpenTK.Graphics.OpenGL.PrimitiveType.Quads);
		GLold.TexCoord2(0f, 0f); GLold.Vertex2(-1f, -1f);
		GLold.TexCoord2(1f, 0f); GLold.Vertex2(1f, -1f);
		GLold.TexCoord2(1f, 1f); GLold.Vertex2(1f, 1f);
		GLold.TexCoord2(0f, 1f); GLold.Vertex2(-1f, 1f);
		GLold.End();
		var result = FrameBufferHelper.ToByteArray(0, 0, tex.Width, tex.Height, pixelFormat, channelCount);
		// Are buffers the same?
		CollectionAssert.AreEqual(pixels, result);
	}
}