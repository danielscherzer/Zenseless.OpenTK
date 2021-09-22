namespace Zenseless.OpenTK
{
	/// <summary>
	/// Contains the available filter modes for minification filtering of a texture
	/// </summary>
	public enum TextureMinFilter
	{
		/// <summary>
		/// Nearest neighbor filtering
		/// </summary>
		Nearest = global::OpenTK.Graphics.OpenGL4.TextureMinFilter.Nearest,
		/// <summary>
		/// Linear filtering
		/// </summary>
		Linear = global::OpenTK.Graphics.OpenGL4.TextureMinFilter.Linear,
		/// <summary>
		/// Chooses the mipmap that most closely matches the size of the pixel being textured and uses the nearest criterion (the texture element nearest to the center of the pixel) to produce a texture value.
		/// </summary>
		NearestMipmapNearest = global::OpenTK.Graphics.OpenGL4.TextureMinFilter.NearestMipmapNearest,
		/// <summary>
		/// Chooses the two mipmaps that most closely match the size of the pixel being textured and uses the nearest criterion (the texture element nearest to the center of the pixel) to produce a texture value from each mipmap. The final texture value is a weighted average of those two values.
		/// </summary>
		NearestMipmapLinear = global::OpenTK.Graphics.OpenGL4.TextureMinFilter.NearestMipmapLinear,
		/// <summary>
		/// Chooses the mipmap that most closely matches the size of the pixel being textured and uses the linear criterion (a weighted average of the four texture elements that are closest to the center of the pixel) to produce a texture value.
		/// </summary>
		LinearMipmapNearest = global::OpenTK.Graphics.OpenGL4.TextureMinFilter.LinearMipmapNearest,
		/// <summary>
		/// Chooses the two mipmaps that most closely match the size of the pixel being textured and uses the linear criterion (a weighted average of the four texture elements that are closest to the center of the pixel) to produce a texture value from each mipmap. The final texture value is a weighted average of those two values.
		/// </summary>
		LinearMipmapLinear = global::OpenTK.Graphics.OpenGL4.TextureMinFilter.LinearMipmapLinear,
	}
}
