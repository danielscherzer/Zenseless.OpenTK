using ImageMagick;
using OpenTK.Graphics.OpenGL4;
using System.IO;

namespace Zenseless.OpenTK;

/// <summary>
/// Class for loading textures from images
/// </summary>
public static class Texture2DLoader
{
	/// <summary>
	/// Load a texture from the given stream.
	/// </summary>
	/// <param name="stream">A stream containing an image.</param>
	/// <returns>A Texture.</returns>
	public static Texture2D Load(Stream stream)
	{
		using var image = new MagickImage(stream);
		return Load(image);
	}

	/// <summary>
	/// Load a texture from the given <seealso cref="MagickImage"/>.
	/// </summary>
	/// <param name="image">A <seealso cref="MagickImage"/>.</param>
	/// <returns>A Texture.</returns>
	public static Texture2D Load(MagickImage image)
	{
		var format = PixelFormat.Rgb;
		var internalFormat = SizedInternalFormat.Rgb8;
		switch (image.ChannelCount)
		{
			case 2: format = PixelFormat.Rg; internalFormat = SizedInternalFormat.Rg8; break;
			case 3: break;
			case 4: format = PixelFormat.Rgba; internalFormat = SizedInternalFormat.Rgba8; break;
			default: throw new InvalidDataException("Unexpected image format");
		}
		image.Flip();
		var bytes = image.GetPixelsUnsafe().ToArray();
		var texture = new Texture2D(image.Width, image.Height, internalFormat)
		{
			Function = TextureFunction.ClampToEdge,
			MagFilter = TextureMagFilter.Linear,
			MinFilter = TextureMinFilter.LinearMipmapLinear
		};
		GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1); // some image sizes will cause memory acceptions otherwise
		GL.TextureSubImage2D(texture.Handle, 0, 0, 0, image.Width, image.Height, format, PixelType.UnsignedByte, bytes);
		GL.GenerateTextureMipmap(texture.Handle);
		return texture;
	}
}
