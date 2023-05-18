using OpenTK.Graphics.OpenGL4;

namespace Zenseless.OpenTK;

/// <summary>
/// Format conversion operations
/// </summary>
public static class TextureExtensions
{
	/// <summary>
	/// Returns the number of channels for a given <see cref="PixelFormat"/>
	/// </summary>
	/// <param name="pixelFormat">A <see cref="PixelFormat"/>.</param>
	/// <returns></returns>
	/// <exception cref="OpenGLException"></exception>
	public static byte ChannelCountFrom(PixelFormat pixelFormat) => pixelFormat switch
	{
		PixelFormat.Red => 1,
		PixelFormat.Rg => 2,
		PixelFormat.Rgb => 3,
		PixelFormat.Rgba => 4,
		_ => throw new OpenGLException("Unsupported pixel format")
	};

	/// <summary>
	/// Returns the <see cref="SizedInternalFormat"/> calculated from channel count and if floating point
	/// </summary>
	/// <param name="channelCount">The number of channels.</param>
	/// <param name="floatingPoint">if set to <c>true</c> a 32bit floating point format is returned.</param>
	/// <returns></returns>
	/// <exception cref="OpenGLException">Invalid Format only 1-4 components allowed</exception>
	public static SizedInternalFormat InternalFormatFromColorChannels(byte channelCount, bool floatingPoint = false) => channelCount switch
	{
		1 => floatingPoint ? SizedInternalFormat.R32f : SizedInternalFormat.R8,
		2 => floatingPoint ? SizedInternalFormat.Rg32f : SizedInternalFormat.Rg8,
		3 => floatingPoint ? SizedInternalFormat.Rgb32f : SizedInternalFormat.Rgb8,
		4 => floatingPoint ? SizedInternalFormat.Rgba32f : SizedInternalFormat.Rgba8,
		_ => throw new OpenGLException("Invalid format! Only 1-4 color channels are supported."),
	};

	/// <summary>
	/// Converts the given <see cref="SizedInternalFormat"/> into <see cref="PixelFormat"/>
	/// </summary>
	/// <param name="internalFormat">A <see cref="SizedInternalFormat"/>.</param>
	/// <returns></returns>
	/// <exception cref="OpenGLException"></exception>
	public static PixelFormat PixelFormatFrom(SizedInternalFormat internalFormat) => internalFormat switch
	{
		SizedInternalFormat.R8 => PixelFormat.Red,
		SizedInternalFormat.R32f => PixelFormat.Red,
		SizedInternalFormat.Rg8 => PixelFormat.Rg,
		SizedInternalFormat.Rg32f => PixelFormat.Rg,
		SizedInternalFormat.Srgb8 => PixelFormat.Rgb,
		SizedInternalFormat.Rgb8 => PixelFormat.Rgb,
		SizedInternalFormat.Rgb32f => PixelFormat.Rgb,
		SizedInternalFormat.Rgba8 => PixelFormat.Rgba,
		SizedInternalFormat.Rgba32f => PixelFormat.Rgba,
		_ => throw new OpenGLException("Unsopported internal format."),
	};

	/// <summary>
	/// Returns the <see cref="SizedInternalFormat"/> of the given texture
	/// </summary>
	/// <param name="texture">The texture to query.</param>
	/// <returns></returns>
	public static SizedInternalFormat InternalFormat(this Texture texture)
	{
		GL.GetTextureLevelParameter(texture.Handle, 0, GetTextureParameter.TextureInternalFormat, out int format);
		return (SizedInternalFormat)format;
	}
}
