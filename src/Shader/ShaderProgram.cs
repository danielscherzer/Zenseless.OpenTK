using OpenTK.Graphics.OpenGL4;
using Zenseless.Patterns;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// OpenGL shader program class
	/// </summary>
	/// <seealso cref="Disposable" />
	/// <seealso cref="IObjectHandle{Type}" />
	public class ShaderProgram : Disposable, IObjectHandle<ShaderProgram>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShaderProgram" /> class.
		/// </summary>
		/// <exception cref="OpenGLException">When the program handle could not be created.</exception>
		public ShaderProgram() => Handle = GL.CreateProgram().CheckValidHandle<ShaderProgram>();

		/// <summary>
		/// Returns the OpenGL object handle
		/// </summary>
		public Handle<ShaderProgram> Handle { get; }

		/// <summary>
		/// Activates this shader. Only one shader may be active at one time.
		/// </summary>
		public void Bind()
		{
			GL.UseProgram(Handle);
		}

		/// <summary>
		/// Will be called from the default Dispose method.
		/// Implementers should dispose all their resources her.
		/// </summary>
		protected override void DisposeResources()
		{
			GL.DeleteProgram(Handle);
		}
	}
}
