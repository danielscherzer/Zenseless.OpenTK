namespace Zenseless.OpenTK
{
	/// <summary>
	/// Magnification filter for texture mapping
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
