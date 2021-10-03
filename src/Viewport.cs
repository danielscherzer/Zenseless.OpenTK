using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// Class that handles viewport transformation and resize
	/// </summary>
	public class Viewport
	{
		/// <summary>
		/// Should be called each time the viewport changes
		/// </summary>
		/// <param name="width">The width of the viewport in pixel.</param>
		/// <param name="height">The height of the viewport in pixel.</param>
		public void Resize(int width, int height)
		{
			GL.Viewport(0, 0, width, height);
			InvAspectRatio = height / (float)width;

			var translate = Transformation2d.Translate(-1f, 1f);
			var scale = Transformation2d.Scale(2f / (width - 1), -2f / (height - 1));
			InvViewportMatrix = scale * translate;
		}

		/// <summary>
		/// The viewport aspect ratio in height / width
		/// </summary>
		public float InvAspectRatio { get; private set; } = 1f;

		/// <summary>
		/// The matrix that transforms from window coordinates (pixel) into OpenGL coordinates (normalized coordinates).
		/// </summary>
		public Matrix4 InvViewportMatrix { get; private set; } = Matrix4.Identity;
	}
}
