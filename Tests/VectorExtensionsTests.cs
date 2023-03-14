using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;
using System;

namespace Zenseless.OpenTK.Tests
{
	[TestClass()]
	public class VectorExtensionsTests
	{
		[TestMethod()]
		public void PackUnorm4x8Test()
		{
			for (uint x = 0; x < 256; x += 5)
			{
				for (uint y = 0; y < 256; y += 5)
				{
					for (uint z = 0; z < 256; z += 5)
					{
						for (uint w = 0; w < 256; w += 5)
						{
							var input = new Vector4(x, y, z, w);
							var output = VectorExtensions.UnpackUnorm4x8(VectorExtensions.PackUnorm4x8(input / 255f));
							output *= 255f;
							var delta = .0001f;
							Assert.AreEqual(input.X, output.X, delta);
							Assert.AreEqual(input.Y, output.Y, delta);
							Assert.AreEqual(input.Z, output.Z, delta);
							Assert.AreEqual(input.W, output.W, delta);
						}
					}
				}
			}
		}

		[DataTestMethod()]
		[DataRow(0, 0, 0, 0, 0, 0)]
		[DataRow(0.3f, 0.4f, 0.5f, 0.3f, 0.4f, 0.5f)]
		[DataRow(1.3f, 2.4f, 3.5f, 0.3f, 0.4f, 0.5f)]
		[DataRow(-1.3f, -2.4f, -3.5f, 0.3f, 0.4f, 0.5f)]
		public void FractTest(float x, float y, float z, float expectedX, float expectedY, float expectedZ)
		{
			var input = new Vector3(x, y, z);
			var delta = 0.0000001f;
			Assert.AreEqual(expectedX, input.Fract().X, delta);
			Assert.AreEqual(expectedY, input.Fract().Y, delta);
			Assert.AreEqual(expectedZ, input.Fract().Z, delta);
		}

		[DataTestMethod()]
		[DataRow(0, 0, 0, 0)]
		[DataRow(1, 0, 0, 1)]
		[DataRow(0, 1, 0.5f * MathF.PI, 1)] //90°
		[DataRow(0, 4, 0.5f * MathF.PI, 4)] //90°
		[DataRow(-1, 0, MathF.PI, 1)] //180°
		[DataRow(-2, 0, MathF.PI, 2)] //180°
		[DataRow(0, -1, -1f / 2f * MathF.PI, 1)] //270°
		[DataRow(0, -2, -1f / 2f * MathF.PI, 2)] // 270°
		public void ToPolarTest(double inputX, double inputY, double expectedX, double expectedY)
		{
			var input = new Vector2((float)inputX, (float)inputY);
			var expected = new Vector2((float)expectedX, (float)expectedY);
			Assert.AreEqual(expected, VectorExtensions.ToPolar(input));
		}

		[DataTestMethod()]
		[DataRow(0, 0, 0, 0, 0)]
		[DataRow(1, 0, 3, 1, 0)]
		[DataRow(2, 0, 1, 1, 0)]
		[DataRow(0, 4, 2, 0, 2)]
		public void LimitTest(double inputX, double inputY, double limit, double expectedX, double expectedY)
		{
			var input = new Vector2((float)inputX, (float)inputY);
			var expected = new Vector2((float)expectedX, (float)expectedY);
			Assert.AreEqual(expected, VectorExtensions.Limit(input, (float)limit));
		}
	}
}