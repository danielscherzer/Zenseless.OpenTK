using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zenseless.Resources;

namespace Zenseless.OpenTK.Tests
{
	[TestClass()]
	public class Texture2DLoaderTests
	{
		[TestMethod()]
		public void LoadTest()
		{
			EmbeddedResourceDirectory resourceDirectory = new("Zenseless.OpenTK.Tests.Content");
			using var stream = resourceDirectory.Resource("roughness.png").Open();
			Helper.ExecuteOnOpenGL(() => 
			{
				var tex = Texture2DLoader.Load(stream);
				Assert.AreEqual(1024, tex.Width);
				Assert.AreEqual(1024, tex.Height);
				tex.Dispose();
				return 0;
			});
		}
	}
}