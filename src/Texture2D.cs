using OpenTK.Graphics.OpenGL4;
using Zenseless.Patterns;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// Class that encapsulated an OpenGL 2d texture object
	/// </summary>
	public class Texture2D : Texture
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Texture2D" /> class.
		/// </summary>
		/// <param name="width">the width of the texture in texels</param>
		/// <param name="height">the height of the texture in texels</param>
		/// <param name="format">the internal format of the texture</param>
		/// <param name="levels">the number of mip map levels to store in the texture</param>
		/// <param name="target">The texture target</param>
		public Texture2D(int width, int height, SizedInternalFormat format = SizedInternalFormat.Rgba8, int levels = 0, TextureTarget target = TextureTarget.Texture2D) : base(target)
		{
			Width = width;
			Height = height;
			if (0 == levels) levels = MathHelper.MipMapLevelCount(width, height);
			GL.TextureStorage2D(Handle, levels, format, width, height);
		}

		/// <summary>
		/// The width of the texture in texels
		/// </summary>
		public int Width { get; }

		/// <summary>
		/// The height of the texture in texels
		/// </summary>
		public int Height { get; }
	}
}
