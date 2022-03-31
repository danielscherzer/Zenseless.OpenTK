﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Graphics.OpenGL4;
using Zenseless.Resources;

namespace Zenseless.OpenTK.Tests
{
	[TestClass()]
	public class Texture2DLoaderTests
	{
		[TestMethod(), TestCategory("OpenGL")]
		public void LoadPngTest()
		{
			EmbeddedResourceDirectory resourceDirectory = new("Zenseless.OpenTK.Tests.Content");
			using var stream = resourceDirectory.Resource("roughness.png").Open();
			Helper.ExecuteOnOpenGL(window =>
			{
				var tex = Texture2DLoader.Load(stream);
				Assert.AreEqual(1024, tex.Width);
				Assert.AreEqual(1024, tex.Height);
				GL.GetTextureLevelParameter(tex.Handle, 0, GetTextureParameter.TextureInternalFormat, out int format);
				Assert.AreEqual((int)SizedInternalFormat.Rg8, format);
				tex.Dispose();
				return 0;
			});
		}

		[TestMethod(), TestCategory("OpenGL")]
		public void LoadJpgTest()
		{
			EmbeddedResourceDirectory resourceDirectory = new("Zenseless.OpenTK.Tests.Content");
			using var stream = resourceDirectory.Resource("test.jpg").Open();
			Helper.ExecuteOnOpenGL(window =>
			{
				var tex = Texture2DLoader.Load(stream);
				Assert.AreEqual(335, tex.Width);
				Assert.AreEqual(1024, tex.Height);
				GL.GetTextureLevelParameter(tex.Handle, 0, GetTextureParameter.TextureInternalFormat, out int format);
				Assert.AreEqual((int)All.Rgb8, format);
				tex.Dispose();
				return 0;
			});
		}
	}
}