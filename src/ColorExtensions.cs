﻿using OpenTK.Mathematics;
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
		/// Convert a color string into a <seealso cref="Color"/>.
		/// Converts named colors like 'white', 'black, 'red'.
		/// or hex strings like '#FFF', '#FFFF', '#FFFFFF' or with alpha '#FFFFFFFF'
		/// </summary>
		/// <param name="hexColor"></param>
		/// <returns></returns>
		public static Color4 FromHexCode(string hexColor)
		{
			var sysColor = (System.Drawing.Color)_converter.ConvertFromString(hexColor);
			return new Color4(sysColor.R, sysColor.G, sysColor.B, sysColor.A);
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
		/// Converts a float array into a <see cref="Color4"/>
		/// </summary>
		/// <param name="color">The input float array to convert.</param>
		/// <returns>A <see cref="Color4"/></returns>
		public static Color4 ToColor4(this float[] color) => new(color[0], color[1], color[2], color[3]);

		private static readonly System.Drawing.ColorConverter _converter = new();
	}
}
