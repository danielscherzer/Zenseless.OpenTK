using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;
using static Zenseless.OpenTK.ColorExtensions;

namespace Zenseless.OpenTK.Tests;

[TestClass()]
public class ColorExtensionsTests
{
	[TestMethod()]
	[DataRow(new float[] { 0f, 0f, 0f, 1f }, "#000000")]
	[DataRow(new float[] { 0f, 0f, 0f, 0f }, "#00000000")]
	[DataRow(new float[] { 1f, 1f, 1f, 1f }, "#FFFFFF")]
	[DataRow(new float[] { 1f, 1f, 1f, 1f }, "white")]
	[DataRow(new float[] { 1f, 0f, 0f, 1f }, "red")]
	[DataRow(new float[] { 1f, 0f, 0f, 1f }, "#FF0000")]
	[DataRow(new float[] { 0f, 1f, 0f, 1f }, "lime")]
	[DataRow(new float[] { 0f, 1f, 0f, 1f }, "#00FF00")]
	[DataRow(new float[] { 0f, 0f, 1f, 1f }, "blue")]
	[DataRow(new float[] { 0f, 0f, 1f, 1f }, "#0000FF")]
	[DataRow(new float[] { 1f, 1f, 1f, 0f }, "transparent")]
	public void FromHexCodeTest(float[] color, string hexColor)
	{
		Assert.AreEqual(color.ToColor4(), FromHexCode(hexColor));
	}

	[TestMethod()]
	[DataRow(new float[] { 1f, 0.5f, 0.25f, 0.1f })]
	public void ToColor4Test(float[] input)
	{
		var color = input.ToColor4();
		Assert.AreEqual(input[0], color.R);
		Assert.AreEqual(input[1], color.G);
		Assert.AreEqual(input[2], color.B);
		Assert.AreEqual(input[3], color.A);
	}

	[TestMethod()]
	public void MixTest()
	{
		Assert.AreEqual(Color4.Red, Mix(Color4.Red, Color4.Black, 0f));
		Assert.AreEqual(Color4.Black, Mix(Color4.Red, Color4.Black, 1f));

		var result = Mix(Color4.White, Color4.Black, 0.5f);
		var gray = new Color4(0.5f, 0.5f, 0.5f, 1f);
		Assert.AreEqual(gray, result);
	}
}