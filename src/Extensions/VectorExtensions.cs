using OpenTK.Mathematics;
using System;
using Zenseless.Patterns;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// Contains GLSL-style mathematical static/extension methods for OpenTK Vector types.
	/// Operations include Ceiling, Clamp, Round, Lerp, Floor, Mod...
	/// </summary>
	public static class VectorExtensions
	{
		/// <summary>
		/// Returns for each component the absolute value.
		/// </summary>
		/// <param name="value">The input value.</param>
		/// <returns></returns>
		public static Vector3 Abs(this in Vector3 value) => new(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z));

		/// <summary>
		/// Returns for each component the smallest integer bigger than or equal to the specified floating-point number.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static Vector2 Ceiling(in Vector2 value) => new(MathF.Ceiling(value.X), MathF.Ceiling(value.Y));

		/// <summary>
		/// Clamp each component of the input vector v in between min and max. 
		/// </summary>
		/// <param name="v">input vector that will be clamped component-wise</param>
		/// <param name="min">lower limit</param>
		/// <param name="max">upper limit</param>
		/// <returns>clamped version of v</returns>
		public static Vector2 Clamp(this in Vector2 v, float min, float max)
		{
			return new Vector2(v.X.Clamp(min, max), v.Y.Clamp(min, max));
		}

		/// <summary>
		/// Clamp each component of the input vector v in between min and max. 
		/// </summary>
		/// <param name="v">input vector that will be clamped component-wise</param>
		/// <param name="min">lower limit</param>
		/// <param name="max">upper limit</param>
		/// <returns>clamped version of v</returns>
		public static Vector3 Clamp(this in Vector3 v, float min, float max)
		{
			return new Vector3(v.X.Clamp(min, max), v.Y.Clamp(min, max), v.Z.Clamp(min, max));
		}

		/// <summary>
		/// Clamp each component of the input vector v in between min and max. 
		/// </summary>
		/// <param name="v">input vector that will be clamped component-wise</param>
		/// <param name="min">lower limit</param>
		/// <param name="max">upper limit</param>
		/// <returns>clamped version of v</returns>
		public static Vector4 Clamp(this in Vector4 v, float min, float max)
		{
			return new Vector4(v.X.Clamp(min, max), v.Y.Clamp(min, max), v.Z.Clamp(min, max), v.W.Clamp(min, max));
		}

		/// <summary>
		/// Calculates the determinant of the two vectors.
		/// </summary>
		/// <param name="a">Vector a.</param>
		/// <param name="b">Vector b.</param>
		/// <returns>The determinant</returns>
		public static float Determinant(in Vector2 a, in Vector2 b) => a.X * b.Y - a.Y * b.X;

		/// <summary>
		/// Clock-wise normal to input vector.
		/// </summary>
		/// <param name="v">The input vector.</param>
		/// <returns>A vector normal to the input vector</returns>
		public static Vector2 CwNormalTo(this in Vector2 v) => new(v.Y, -v.X);

		/// <summary>
		/// Counter-clock-wise normal to input vector.
		/// </summary>
		/// <param name="v">The input vector.</param>
		/// <returns>A vector normal to the input vector</returns>
		public static Vector2 CcwNormalTo(this in Vector2 v) => new(-v.Y, v.X);

		/// <summary>
		/// Limit the magnitude of a vector to a given threshold.
		/// </summary>
		/// <param name="v">The input vector.</param>
		/// <param name="limit">The limit for the magnitude.</param>
		/// <returns>A vector whit a magnitude less or equal to <paramref name="limit"/></returns>
		public static Vector2 Limit(this in Vector2 v, float limit)
		{
			var length2 = v.LengthSquared;
			return (length2 > limit * limit) ? v * limit / MathF.Sqrt(length2) : v;
		}

		/// <summary>
		/// Normalizes each input uint from range [0,255] into float in range [0,1]
		/// </summary>
		/// <param name="x">input in range [0,255]</param>
		/// <param name="y">input in range [0,255]</param>
		/// <param name="z">input in range [0,255]</param>
		/// <param name="w">input in range [0,255]</param>
		/// <returns>vector with each component in range [0,1]</returns>
		public static Vector4 Normalize(uint x, uint y, uint z, uint w) => new Vector4(x, y, z, w) / 255f;

		/// <summary>
		/// Returns for each component the integer part of the specified floating-point number. 
		/// Works not for constructs like <code>1f - float.epsilon</code> because this is outside of floating point precision
		/// </summary>
		/// <param name="value">Input floating-point vector</param>
		/// <returns>The integer parts.</returns>
		public static Vector2 Truncate(in Vector2 value) => new(value.X.FastTruncate(), value.Y.FastTruncate());

		/// <summary>
		/// For each component returns the largest integer less than or equal to the specified floating-point number.
		/// </summary>
		/// <param name="v">Input vector</param>
		/// <returns>For each component returns the largest integer less than or equal to the specified floating-point number.</returns>
		public static Vector3 Floor(this in Vector3 v) => new(MathF.Floor(v.X), MathF.Floor(v.Y), MathF.Floor(v.Z));

		/// <summary>
		/// Returns the value of x modulo y. This is computed as x - y * floor(x/y). 
		/// </summary>
		/// <param name="x">Dividend</param>
		/// <param name="y">Divisor</param>
		/// <returns>Returns the value of x modulo y.</returns>
		public static Vector3 Mod(this in Vector3 x, float y)
		{
			var div = x / y;
			return x - y * div.Floor();
		}

		/// <summary>
		/// packs normalized floating-point values into an unsigned integer.  
		/// </summary>
		/// <param name="v">Input normalized floating-point vector. Will be clamped</param>
		/// <returns>The first component of the vector will be written to the least significant bits of the output; 
		/// the last component will be written to the most significant bits.</returns>
		public static uint PackUnorm4x8(this in Vector4 v)
		{
			var r = (Vector4.Clamp(v, Vector4.Zero, Vector4.One) * 255.0f).Round();
			var x = (uint)r.X;
			var y = (uint)r.Y;
			var z = (uint)r.Z;
			var w = (uint)r.W;
			return (w << 24) + (z << 16) + (y << 8) + x;
		}

		/// <summary>
		/// Unpacks normalized floating-point values from an unsigned integer.
		/// </summary>
		/// <param name="i">Specifies an unsigned integer containing packed floating-point values.</param>
		/// <returns>The first component of the returned vector will be extracted from the least significant bits of the input; 
		/// the last component will be extracted from the most significant bits. </returns>
		public static Vector4 UnpackUnorm4x8(uint i)
		{
			var x = i & 0x000000ff;
			var y = (i & 0x0000ff00) >> 8;
			var z = (i & 0x00ff0000) >> 16;
			var w = (i & 0xff000000) >> 24;
			var v = new Vector4(x, y, z, w);
			return v / 255.0f;
		}

		/// <summary>
		/// Rounds each component of a floating-point vector (using <see cref="MathF.Round(float)"/>) to the nearest integral value.
		/// </summary>
		/// <param name="v">A floating-point vector to be rounded component-wise.</param>
		/// <returns>Component-wise rounded vector</returns>
		public static Vector3 Round(this in Vector3 v) => new(MathF.Round(v.X), MathF.Round(v.Y), MathF.Round(v.Z));

		/// <summary>
		/// Rounds each component of a floating-point vector (using <see cref="MathF.Round(float)"/>) to the nearest integral value.
		/// </summary>
		/// <param name="v">A floating-point vector to be rounded component-wise.</param>
		/// <returns>Component-wise rounded vector</returns>
		public static Vector4 Round(this in Vector4 v) => new(MathF.Round(v.X), MathF.Round(v.Y), MathF.Round(v.Z), MathF.Round(v.W));

		/// <summary>
		/// Converts given Cartesian coordinates into a polar angle.
		/// Returns an angle [-PI, PI].
		/// </summary>
		/// <param name="cartesian">Cartesian input coordinates</param>
		/// <returns>An angle [-PI, PI].</returns>
		public static float PolarAngle(this in Vector2 cartesian) => MathF.Atan2(cartesian.Y, cartesian.X);

		/// <summary>
		/// Converts a Vector to a array of float
		/// </summary>
		/// <param name="q">The input vector.</param>
		/// <returns></returns>
		public static float[] ToArray(this in Quaternion q) => new float[] { q.X, q.Y, q.Z, q.W };

		/// <summary>
		/// Converts a Vector to a array of float
		/// </summary>
		/// <param name="vector">The input vector.</param>
		/// <returns></returns>
		public static float[] ToArray(this in Vector2 vector) => new float[] { vector.X, vector.Y };

		/// <summary>
		/// Converts a Vector to a array of float
		/// </summary>
		/// <param name="vector">The input vector.</param>
		/// <returns></returns>
		public static float[] ToArray(this in Vector3 vector) => new float[] { vector.X, vector.Y, vector.Z };

		/// <summary>
		/// Converts a Vector to a array of float
		/// </summary>
		/// <param name="vector">The input vector.</param>
		/// <returns></returns>
		public static float[] ToArray(this in Vector4 vector) => new float[] { vector.X, vector.Y, vector.Z, vector.W };

		/// <summary>
		/// Converts the given polar coordinates to Cartesian.
		/// </summary>
		/// <param name="polar">The polar coordinates. A vector with first component angle [-PI, PI] and second component radius.</param>
		/// <returns>A Cartesian coordinate vector.</returns>
		public static Vector2 ToCartesian(this in Vector2 polar)
		{
			float x = polar.Y * MathF.Cos(polar.X);
			float y = polar.Y * MathF.Sin(polar.X);
			return new Vector2(x, y);
		}

		/// <summary>
		/// Converts given Cartesian coordinates into polar coordinates.
		/// Returns a vector with first component angle [-PI, PI] and second component radius.
		/// </summary>
		/// <param name="cartesian">Cartesian input coordinates</param>
		/// <returns>A vector with first component angle [-PI, PI] and second component radius.</returns>
		public static Vector2 ToPolar(this in Vector2 cartesian)
		{
			float angle = cartesian.PolarAngle();
			float radius = cartesian.Length;
			return new Vector2(angle, radius);
		}

		/// <summary>
		/// Converts <see cref="Vector2"/> into <see cref="System.Numerics.Vector2"/>
		/// </summary>
		/// <param name="vec">The input vector to convert</param>
		/// <returns></returns>
		public static System.Numerics.Vector2 ToSystemNumerics(this Vector2 vec) => new(vec.X, vec.Y);

		/// <summary>
		/// Converts <see cref="Vector3"/> into <see cref="System.Numerics.Vector3"/>
		/// </summary>
		/// <param name="vec">The input vector to convert</param>
		/// <returns></returns>
		public static System.Numerics.Vector3 ToSystemNumerics(this Vector3 vec) => new(vec.X, vec.Y, vec.Z);

		/// <summary>
		/// Converts <see cref="Vector4"/> into <see cref="System.Numerics.Vector4"/>
		/// </summary>
		/// <param name="vec">The input vector to convert</param>
		/// <returns></returns>
		public static System.Numerics.Vector4 ToSystemNumerics(this Vector4 vec) => new(vec.X, vec.Y, vec.Z, vec.W);

		/// <summary>
		/// Converts <see cref="System.Numerics.Vector2"/> into <see cref="Vector2"/>
		/// </summary>
		/// <param name="vec">The input vector to convert</param>
		/// <returns></returns>
		public static Vector2 ToOpenTK(this System.Numerics.Vector2 vec) => new(vec.X, vec.Y);

		/// <summary>
		/// Converts <see cref="System.Numerics.Vector3"/> into <see cref="Vector3"/>
		/// </summary>
		/// <param name="vec">The input vector to convert</param>
		/// <returns></returns>
		public static Vector3 ToOpenTK(this System.Numerics.Vector3 vec) => new(vec.X, vec.Y, vec.Z);

		/// <summary>
		/// Converts <see cref="System.Numerics.Vector4"/> into <see cref="Vector4"/>
		/// </summary>
		/// <param name="vec">The input vector to convert</param>
		/// <returns></returns>
		public static Vector4 ToOpenTK(this System.Numerics.Vector4 vec) => new(vec.X, vec.Y, vec.Z, vec.W);

	}
}
