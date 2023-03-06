using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics.OpenGL4;
using Zenseless.Resources;

namespace Zenseless.OpenTK.Tests;

[TestClass()]
public class Texture2DLoaderTests
{
	[DataTestMethod(), TestCategory("OpenGL")]
	[DataRow("rgb.jpg", 115, 101, SizedInternalFormat.Rgb8)]
	[DataRow("grayscale.png", 533, 657, SizedInternalFormat.R8)]
	[DataRow("paletteAlpha.png", 320, 224, SizedInternalFormat.Rgba8)]
	[DataRow("rgba.png", 320, 224, SizedInternalFormat.Rgba8)]
	public void LoadJpgTest(string name, int width, int height, SizedInternalFormat expectedFormat)
	{
		var resourceDirectory = new ShortestMatchResourceDirectory(new EmbeddedResourceDirectory());
		Helper.ExecuteOnOpenGL(window =>
		{
			using var stream = resourceDirectory.Resource(name).Open();
			using var image = new MagickImage(stream);
			var tex = Texture2DLoader.Load(image);
			var pixels = image.GetPixelsUnsafe().ToArray();
			Assert.AreEqual(width, tex.Width);
			Assert.AreEqual(height, tex.Height);
			GL.GetTextureLevelParameter(tex.Handle, 0, GetTextureParameter.TextureInternalFormat, out int format);
			Assert.AreEqual(expectedFormat, (SizedInternalFormat)format);
			var pixelFormat = TextureExtensions.PixelFormatFrom((SizedInternalFormat)format);
			byte[] buffer = new byte[width * height * TextureExtensions.ChannelCountFrom(pixelFormat)];
			GL.PixelStore(PixelStoreParameter.PackAlignment, 1); // some image sizes will cause memory acceptions otherwise
			GL.GetTextureImage(tex.Handle, 0, pixelFormat, PixelType.UnsignedByte, buffer.Length, buffer);
			CollectionAssert.AreEqual(pixels, buffer);
			tex.Dispose();
			return 0;
		});
	}
}