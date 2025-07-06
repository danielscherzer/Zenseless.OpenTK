namespace Zenseless.OpenTK;

/// <summary>
/// Extension methods for intersection tests.
/// </summary>
public static class Intersection
{
	/// <summary>
	/// Test if two circles intersect.
	/// </summary>
	/// <param name="a">First circle</param>
	/// <param name="b">Second circle</param>
	/// <returns></returns>
	public static bool Overlaps(this Circle a, Circle b)
	{
		var rSum = a.Radius + b.Radius;
		var diff = a.Center - b.Center;
		return rSum * rSum >= diff.LengthSquared;
	}
}
