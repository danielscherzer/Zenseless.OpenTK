using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK.Mathematics;

namespace Zenseless.OpenTK.Tests;

[TestClass()]
public class IntersectionTests
{
	[TestMethod()]
	[DataRow(0, 0, 0,  0, 0, 0)]
	[DataRow(0, 0, 1,  1, 0, 1)]
	[DataRow(0, 0, 0.1f, 0.1f, 0.1f, 0.1f)]
	public void OverlappingCircleTest(float cx, float cy, float r, float cx2, float cy2, float r2)
	{
		var a = new Circle(new Vector2(cx, cy), r);
		var b = new Circle(new Vector2(cx2, cy2), r2);
		Assert.IsTrue(a.Overlaps(b));
		Assert.IsTrue(b.Overlaps(a));
	}

	[TestMethod()]
	[DataRow(-1, 0, 1, 1.0001f, 0, 1)]
	[DataRow(-1, 0, 1, 1f, 0, 0.999f)]
	[DataRow(0, 0, 0.99f, 2f, 0f, 1f)]
	[DataRow(0, 0, 0.1f, 0.2f, 0.1f, 0.1f)]
	public void NotOverlappingCircleTest(float cx, float cy, float r, float cx2, float cy2, float r2)
	{
		var a = new Circle(new Vector2(cx, cy), r);
		var b = new Circle(new Vector2(cx2, cy2), r2);
		Assert.IsFalse(a.Overlaps(b));
		Assert.IsFalse(b.Overlaps(a));
	}
}