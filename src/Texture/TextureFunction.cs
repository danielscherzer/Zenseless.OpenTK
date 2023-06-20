using OpenTK.Graphics.OpenGL4;

namespace Zenseless.OpenTK;

/// <summary>
/// The texture function (wrap mode)
/// </summary>
public enum TextureFunction
{
	/// <summary>
	/// Repeated wrap mode
	/// </summary>
	Repeat = TextureWrapMode.Repeat,

	/// <summary>
	/// Mirrored repeated wrap mode
	/// </summary>
	MirroredRepeat = TextureWrapMode.MirroredRepeat,

	/// <summary>
	/// Repeat the color at the edge of the texture.
	/// </summary>
	ClampToEdge = TextureWrapMode.ClampToEdge,

	/// <summary>
	/// Use the border color of the texture. Has to be set separatly. 
	/// </summary>
	ClampToBorder = TextureWrapMode.ClampToBorder
}
