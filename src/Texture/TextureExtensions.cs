using OpenTK.Graphics.OpenGL4;
using System;

namespace Zenseless.OpenTK;

public static class TextureExtensions
{
	/// <summary>
	/// Converts the specified components.
	/// </summary>
	/// <param name="channelCount">The number of color channels</param>
	/// <returns></returns>
	/// <exception cref="ArgumentOutOfRangeException">Invalid Format only 1-4 components allowed</exception>
	public static PixelFormat PixelFormatFromColorChannels(byte channelCount)
	{
		return channelCount switch
		{
			1 => PixelFormat.Red,
			2 => PixelFormat.Rg,
			3 => PixelFormat.Rgb,
			4 => PixelFormat.Rgba,
			_ => throw new ArgumentOutOfRangeException("Invalid format! Only 1-4 color channels are supported."),
		};
	}

	public static byte ChannelCountFrom(PixelFormat pixelFormat)
	{
		return pixelFormat switch
		{
			PixelFormat.Red => 1,
			PixelFormat.Rg => 2,
			PixelFormat.Rgb => 3,
			PixelFormat.Rgba => 4,
			_ => throw new ArgumentOutOfRangeException("Unsupported pixel format")
		}; ; ;
	}

	/// <summary>
	/// Converts the specified components.
	/// </summary>
	/// <param name="channelCount">The components.</param>
	/// <param name="floatingPoint">if set to <c>true</c> [floating point].</param>
	/// <returns></returns>
	/// <exception cref="ArgumentOutOfRangeException">Invalid Format only 1-4 components allowed</exception>
	public static SizedInternalFormat InternalFormatFromColorChannels(byte channelCount, bool floatingPoint = false)
	{
		return channelCount switch
		{
			1 => floatingPoint ? SizedInternalFormat.R32f : SizedInternalFormat.R8,
			2 => floatingPoint ? SizedInternalFormat.Rg32f : SizedInternalFormat.Rg8,
			3 => floatingPoint ? SizedInternalFormat.Rgb32f : SizedInternalFormat.Rgb8,
			4 => floatingPoint ? SizedInternalFormat.Rgba32f : SizedInternalFormat.Rgba8,
			_ => throw new ArgumentOutOfRangeException("Invalid format! Only 1-4 color channels are supported."),
		};
	}

	public static PixelFormat PixelFormatFrom(SizedInternalFormat internalFormat)
	{
		return internalFormat switch
		{
			SizedInternalFormat.R8 => PixelFormat.Red,
			SizedInternalFormat.R32f => PixelFormat.Red,
			SizedInternalFormat.Rg8 => PixelFormat.Rg,
			SizedInternalFormat.Rg32f => PixelFormat.Rg,
			SizedInternalFormat.Rgb8 => PixelFormat.Rgb,
			SizedInternalFormat.Rgb32f => PixelFormat.Rgb,
			SizedInternalFormat.Rgba8 => PixelFormat.Rgba,
			SizedInternalFormat.Rgba32f => PixelFormat.Rgba,
			_ => throw new ArgumentOutOfRangeException("Unsopported internal format."),
		};
	}
}
