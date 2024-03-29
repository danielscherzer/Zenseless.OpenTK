﻿using OpenTK.Mathematics;

namespace Zenseless.OpenTK;

/// <summary>
/// Helper class for calculating transformations.
/// </summary>
public static class Transformation2d
{
	/// <summary>
	/// Calculates the combined transformation by first applying the applyToCoordinatesFirst and afterwards the applyToCoordinatesSecond to the input points.
	/// </summary>
	/// <param name="applyToCoordinatesFirst">The transformation that will be applied first.</param>
	/// <param name="applyToCoordinatesSecond">The transformation that will be applied second.</param>
	/// <returns></returns>
	public static Matrix4 Combine(Matrix4 applyToCoordinatesFirst, Matrix4 applyToCoordinatesSecond) => applyToCoordinatesFirst * applyToCoordinatesSecond;

	/// <summary>
	/// Calculates the combined transformation by applying first transformations[0] then transformations[1] and so on to the input points.
	/// </summary>
	/// <param name="transformations">An array of transformations.</param>
	/// <returns></returns>
	public static Matrix4 Combine(params Matrix4[] transformations)
	{
		var result = transformations[0];
		for (int i = 1; i < transformations.Length; ++i)
		{
			result = Combine(result, transformations[i]);
		}
		return result;
	}

	/// <summary>
	/// Creates a 2d rotation transformation.
	/// </summary>
	/// <param name="angleRadiant">The angle to rotate in radiants</param>
	/// <returns></returns>
	public static Matrix4 Rotation(float angleRadiant) => Matrix4.CreateRotationZ(angleRadiant);

	/// <summary>
	/// Create a 2d scale transformation
	/// </summary>
	/// <param name="sx">x-scale factor</param>
	/// <param name="sy">y-scale factor</param>
	/// <returns></returns>
	public static Matrix4 Scale(float sx, float sy) => Matrix4.CreateScale(sx, sy, 1f);

	/// <summary>
	/// Create a 2d scale transformation
	/// </summary>
	/// <param name="scale">The vector of scale factors</param>
	/// <returns></returns>
	public static Matrix4 Scale(Vector2 scale) => Scale(scale.X, scale.Y);

	/// <summary>
	/// Create a 2d scale transformation
	/// </summary>
	/// <param name="uniformScaleFactor">The unfiorm scale factor for x and y-axis</param>
	/// <returns></returns>
	public static Matrix4 Scale(float uniformScaleFactor) => Matrix4.CreateScale(uniformScaleFactor);

	/// <summary>
	/// Creates a 2d translation transform.
	/// </summary>
	/// <param name="tx">The translation factor in x-direction.</param>
	/// <param name="ty">The translation factor in y-direction.</param>
	/// <returns></returns>
	public static Matrix4 Translate(float tx, float ty) => Matrix4.CreateTranslation(tx, ty, 0f);

	/// <summary>
	/// Creates a 2d translation transform.
	/// </summary>
	/// <param name="translation">The translation vector</param>
	/// <returns></returns>
	public static Matrix4 Translate(Vector2 translation) => Translate(translation.X, translation.Y);

	/// <summary>
	/// Transform the given input location vector by the given transformation
	/// </summary>
	/// <param name="input">The input vector to transform</param>
	/// <param name="transformation">The transformation to apply</param>
	/// <returns></returns>
	public static Vector2 Transform(this Vector2 input, Matrix4 transformation) => Vector4.TransformRow(new Vector4(input.X, input.Y, 0f, 1f), transformation).Xy;

	/// <summary>
	/// Transform the given input direction vector by the given transformation
	/// </summary>
	/// <param name="input">The input vector to transform</param>
	/// <param name="transformation">The transformation to apply</param>
	/// <returns></returns>
	public static Vector2 TransformDirection(this Vector2 input, Matrix4 transformation) => Vector4.TransformRow(new Vector4(input.X, input.Y, 0f, 0f), transformation).Xy;
}
