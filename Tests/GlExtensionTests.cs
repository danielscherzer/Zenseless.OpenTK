using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zenseless.OpenTK.Tests;

[TestClass()]
public class GlExtensionTests
{
	[TestMethod(), TestCategory("OpenGL")]
	public void IsSupportedTest()
	{
		var result = Helper.ExecuteOnOpenGL(window =>
		{
			// query some extension every hardware supports
			return GlExtension.IsSupported("GL_ARB_FRAGMENT_SHADER") && GlExtension.IsSupported("gl_arb_fragment_shader");
		});
		Assert.IsTrue(result);
	}
}