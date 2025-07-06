using OpenTK.Mathematics;

namespace Zenseless.OpenTK;

/// <summary>
/// A struct that represents a circle in 2D space.
/// </summary>
/// <param name="Center">The circle center</param>
/// <param name="Radius">The circle radius</param>
public record struct Circle(Vector2 Center, float Radius);
