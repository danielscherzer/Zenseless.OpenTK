using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics.OpenGL4;
using Zenseless.Resources;

namespace Zenseless.OpenTK.Tests
{
	[TestClass()]
	public class Texture2DLoaderTests
	{
		[DataTestMethod(), TestCategory("OpenGL")]
		[DataRow("test.jpg", 335, 1024, SizedInternalFormat.Rgb8)]
		[DataRow("roughness.png", 1024, 1024, SizedInternalFormat.R8)]
		[DataRow("grass.png", 320, 224, SizedInternalFormat.Rgba8)]
		public void LoadJpgTest(string name, int width, int height, SizedInternalFormat expectedFormat)
		{
			EmbeddedResourceDirectory resourceDirectory = new("Zenseless.OpenTK.Tests.Content");
			using var stream = resourceDirectory.Resource(name).Open();
			Helper.ExecuteOnOpenGL(window =>
			{
				var tex = Texture2DLoader.Load(stream);
				Assert.AreEqual(width, tex.Width);
				Assert.AreEqual(height, tex.Height);
				GL.GetTextureLevelParameter(tex.Handle, 0, GetTextureParameter.TextureInternalFormat, out int format);
				Assert.AreEqual((int)expectedFormat, format);
				tex.Dispose();
				return 0;
			});
		}
	}
}