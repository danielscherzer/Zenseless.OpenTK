﻿using ImageMagick;
using OpenTK.Graphics.OpenGL4;
using System.IO;
using System.Linq;

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
	/// <param name="mipMap">Are mipmaps created.</param>
	/// <returns>A Texture.</returns>
	public static Texture2D Load(Stream stream, bool mipMap = true)
	{
		using var image = new MagickImage(stream);
		return Load(image, mipMap);
	}

	/// <summary>
	/// Load a texture from the given <seealso cref="MagickImage"/>.
	/// </summary>
	/// <param name="image">A <seealso cref="MagickImage"/>.</param>
	/// <param name="mipMap">Are mipmaps created.</param>
	/// <returns>A Texture.</returns>
	public static Texture2D Load(MagickImage image, bool mipMap = true)
	{
		var format = PixelFormat.Rgb;
		var internalFormat = SizedInternalFormat.Rgb8;
		var channelCount = image.Channels.Count();
		switch (channelCount)
		{
			case 1: format = PixelFormat.Red; internalFormat = SizedInternalFormat.R8; break;
			case 2: format = PixelFormat.Rg; internalFormat = SizedInternalFormat.Rg8; break;
			case 3: break;
			case 4: format = PixelFormat.Rgba; internalFormat = SizedInternalFormat.Rgba8; break;
			default: throw new InvalidDataException($"Unexpected image format with {image.ChannelCount} channels");
		}
		image.Flip();
		var bytes = image.GetPixelsUnsafe().ToArray();
		var texture = new Texture2D(image.Width, image.Height, internalFormat)
		{
			Function = TextureFunction.ClampToEdge,
			MagFilter = TextureMagFilter.Linear,
			MinFilter = mipMap ? TextureMinFilter.LinearMipmapLinear : TextureMinFilter.Linear
		};
		GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1); // some image sizes will cause memory acceptions otherwise
		GL.TextureSubImage2D(texture.Handle, 0, 0, 0, image.Width, image.Height, format, PixelType.UnsignedByte, bytes);
		if(mipMap) GL.GenerateTextureMipmap(texture.Handle);
		return texture;
	}
}
