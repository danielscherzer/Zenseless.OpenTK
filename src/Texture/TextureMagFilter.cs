namespace Zenseless.OpenTK
{
	/// <summary>
	/// Contains the available filter modes for magnification filtering of a texture
	/// </summary>
	public enum TextureMagFilter
	{
		/// <summary>
		/// Nearest neighbor filtering
		/// </summary>
		Nearest = global::OpenTK.Graphics.OpenGL4.TextureMagFilter.Nearest,
		/// <summary>
		/// Linear filtering
		/// </summary>
		Linear = global::OpenTK.Graphics.OpenGL4.TextureMagFilter.Linear
	}
}
