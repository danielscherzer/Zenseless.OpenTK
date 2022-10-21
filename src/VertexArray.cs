using OpenTK.Graphics.OpenGL4;
using System;
using Zenseless.Patterns;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// A vertex array class for interpreting buffer data.
	/// </summary>
	public class VertexArray : Disposable, IObjectHandle<VertexArray>
	{
		/// <summary>
		/// Create a new vertex array object
		/// </summary>
		/// <exception cref="OpenGLException">When the program handle could not be created.</exception>
		public VertexArray()
		{
			GL.CreateVertexArrays(1, out int handle);
			Handle = handle.CheckValidHandle<VertexArray>();
		}

		/// <summary>
		/// Returns the OpenGL object handle
		/// </summary>
		public Handle<VertexArray> Handle { get; }

		/// <summary>
		/// Binds the given buffer as an element buffer to the vertex array object
		/// </summary>
		/// <param name="buffer">the buffer to bind</param>
		public void BindIndices(Buffer buffer)
		{
			GL.VertexArrayElementBuffer(Handle, buffer.Handle);
		}

		/// <summary>
		/// Activate the vertex array
		/// </summary>
		public void Bind()
		{
			GL.BindVertexArray(Handle); // activate vertex array; from now on state is stored;
		}

		/// <summary>
		/// Binds a buffer as an attribute.
		/// </summary>
		/// <param name="attributeLocation">binding location</param>
		/// <param name="buffer">the buffer with the attribute data</param>
		/// <param name="baseTypeCount">Each buffer element consists of a type that is made up of multiple base types like for Vector3 the base type count is 3.</param>
		/// <param name="stride">Byte distance from one buffer element to next. OFten the size in byte of one element, except for interleaved buffers.</param>
		/// <param name="type">Element base type</param>
		/// <param name="perInstance">Is this attribute per instance</param>
		/// <param name="normalized">Should the input data be normalized</param>
		/// <param name="offset">Offset into the buffer</param>
		/// <returns><c>true</c> if successfull, <c>false</c> if the attribute location is invalid</returns>
		public bool BindAttribute(int attributeLocation, Buffer buffer, int baseTypeCount, int stride, VertexAttribType type, bool perInstance = false, bool normalized = false, int offset = 0)
		{
			if (-1 == attributeLocation) return false;
			GL.EnableVertexArrayAttrib(Handle, attributeLocation);
			GL.VertexArrayVertexBuffer(Handle, attributeLocation, buffer.Handle, new IntPtr(offset), stride);
			GL.VertexArrayAttribBinding(Handle, attributeLocation, attributeLocation);
			GL.VertexArrayAttribFormat(Handle, attributeLocation, baseTypeCount, type, normalized, 0);
			if (perInstance)
			{
				GL.VertexArrayBindingDivisor(Handle, attributeLocation, 1);
			}
			return true;
		}

		/// <summary>
		/// Will be called from the default Dispose method.
		/// Implementers should dispose all their resources her.
		/// </summary>
		protected override void DisposeResources() => GL.DeleteVertexArray(Handle);
	}
}