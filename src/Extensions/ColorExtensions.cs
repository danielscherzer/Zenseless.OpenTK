using OpenTK.Mathematics;
using System;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// Contains GLSL-style mathematical static/extension methods for OpenTK Vector types.
	/// Operations include Ceiling, Clamp, Round, Lerp, Floor, Mod...
	/// </summary>
	public static class ColorExtensions
	{
		/// <summary>
		/// Convert a color string into a <seealso cref="Color4"/>.
		/// Converts named colors like 'white', 'black, 'red'.
		/// or hex strings like '#FFF', '#FFFF', '#FFFFFF' or with alpha '#FFFFFFFF'
		/// </summary>
		/// <param name="hexColor"></param>
		/// <returns><see cref="Color4.White"/> if convertion was not possible.</returns>
		public static Color4 FromHexCode(string hexColor)
		{
			if (_converter.ConvertFromString(hexColor) is System.Drawing.Color sysColor)
			{
				return new Color4(sysColor.R, sysColor.G, sysColor.B, sysColor.A);

			}
			else
			{
				return Color4.White;
			}
		}

		/// <summary>
		/// Converts HSB (Hue, Saturation and Brightness) color value into RGB
		/// </summary>
		/// <param name="hue">Hue [0..1]</param>
		/// <param name="saturation">Saturation [0..1]</param>
		/// <param name="brightness">Brightness [0..1]</param>
		/// <returns>
		/// RGB color
		/// </returns>
		public static (float red, float green, float blue) Hsb2rgb(float hue, float saturation, float brightness)
		{
			saturation = Math.Clamp(saturation, 0f, 1f);
			brightness = Math.Clamp(brightness, 0f, 1f);
			var v3 = new Vector3(3f);
			var i = hue * 6f;
			var j = new Vector3(i, i + 4f, i + 2f).Mod(6f);
			var k = (j - v3).Abs();
			var l = k - Vector3.One;
			var rgb = l.Clamp(0f, 1f);
			var result = brightness * Vector3.Lerp(Vector3.One, rgb, saturation);
			return (result.X, result.Y, result.Z);
		}

		/// <summary>
		/// Mixes (linearly interpolates) two colors.
		/// If <paramref name="weight"/> == 0 <paramref name="colorA"/> is returned
		/// If <paramref name="weight"/> == 1 <paramref name="colorB"/> is returned
		/// </summary>
		/// <param name="colorA">The first input color.</param>
		/// <param name="colorB">The second input color.</param>
		/// <param name="weight">The weight [0,1].</param>
		/// <returns></returns>
		public static Color4 Mix(Color4 colorA, Color4 colorB, float weight)
		{
			float r = MathHelper.Lerp(colorA.R, colorB.R, weight);
			float g = MathHelper.Lerp(colorA.G, colorB.G, weight);
			float b = MathHelper.Lerp(colorA.B, colorB.B, weight);
			float a = MathHelper.Lerp(colorA.A, colorB.A, weight);
			return new(r, g, b, a);
		}

		/// <summary>
		/// Converts a float array into a <see cref="Color4"/>
		/// </summary>
		/// <param name="color">The input float array to convert.</param>
		/// <returns>A <see cref="Color4"/></returns>
		public static Color4 ToColor4(this float[] color) => new(color[0], color[1], color[2], color[3]);

		private static readonly System.Drawing.ColorConverter _converter = new();
	}
}
