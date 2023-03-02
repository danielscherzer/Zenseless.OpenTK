using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics.OpenGL4;
using System.Linq;
using Zenseless.Resources;

namespace Zenseless.OpenTK.Tests;

[TestClass()]
public class Texture2DLoaderTests
{
	[DataTestMethod(), TestCategory("OpenGL")]
	//[DataRow("test.jpg", 335, 1024, SizedInternalFormat.Rgb8)]
	//[DataRow("roughness.png", 1024, 1024, SizedInternalFormat.R8)]
	[DataRow("grass.png", 320, 224, SizedInternalFormat.Rgba8)]
	public void LoadJpgTest(string name, int width, int height, SizedInternalFormat expectedFormat)
	{
		EmbeddedResourceDirectory resourceDirectory = new("Zenseless.OpenTK.Tests.Content");
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
			GL.GetTextureImage(tex.Handle, 0, pixelFormat, PixelType.UnsignedByte, buffer.Length, buffer);
			CollectionAssert.AreEqual(pixels, buffer);
			//Assert.IsTrue(buffer.Any(value => 0 != value));
			tex.Dispose();
			return 0;
		});
	}
}