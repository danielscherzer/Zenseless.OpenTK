using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Zenseless.OpenTK.Tests
{
	[TestClass()]
	public class DebugOutputGLTests
	{
		[TestMethod()]
		public void DebugOutputGLTest()
		{
			static int Execute()
			{
				DebugOutputGL _ = new();
				return 0;
			}
			//Assert.ThrowsException<Exception>(() => Helper.ExecuteOnOpenGL(Execute, new Version(4, 3), ContextFlags.Default)); //run this first; context seems to be reused for default flag
			Assert.AreEqual(0, Helper.ExecuteOnOpenGL(Execute, new Version(4, 3)));
			Assert.ThrowsException<Exception>(() => Helper.ExecuteOnOpenGL(Execute, new Version(3, 3)));
		}
	}
}