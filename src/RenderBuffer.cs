using OpenTK.Graphics.OpenGL4;
using Zenseless.Patterns;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// OpenGL Objects that contain images. They are created and used specifically with Framebuffer Objects. They are optimized for use as render targets.
	/// </summary>
	/// <seealso cref="Disposable" />
	/// <exception cref="OpenGLException">When the program handle could not be created.</exception>
	public class RenderBuffer : Disposable, IObjectHandle<RenderBuffer>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RenderBuffer"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		public RenderBuffer(RenderbufferStorage type, int width, int height)
		{
			GL.CreateRenderbuffers(1, out int handle);
			Handle = handle.CheckValidHandle<RenderBuffer>();
			GL.NamedRenderbufferStorage(handle, type, width, height);
		}

		/// <summary>
		/// Returns the OpenGL object handle
		/// </summary>
		public Handle<RenderBuffer> Handle { get; }

		/// <summary>
		/// Will be called from the default Dispose method.
		/// </summary>
		protected override void DisposeResources() => GL.DeleteRenderbuffer(Handle);
	}
}
