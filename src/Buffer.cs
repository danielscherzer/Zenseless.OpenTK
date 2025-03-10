﻿using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Zenseless.Patterns;

namespace Zenseless.OpenTK;

/// <summary>
/// A class that encapsulates an OpenGL buffer object.
/// </summary>
/// <seealso cref="Disposable" />
public class Buffer : Disposable, IObjectHandle<Buffer>
{
	/// <summary>
	/// Constructs a new OpenGL buffer object.
	/// </summary>
	/// <exception cref="OpenGLException">When the program handle could not be created.</exception>
	public Buffer()
	{
		GL.CreateBuffers(1, out int handle);
		Handle = handle.CheckValidHandle<Buffer>();
	}

	/// <summary>
	/// Returns the OpenGL handle
	/// </summary>
	public Handle<Buffer> Handle { get; }

	/// <summary>
	/// Copies the given data into a buffer object on the GPU.
	/// </summary>
	/// <typeparam name="DataType">The value data type of each array element</typeparam>
	/// <param name="data">The data array</param>
	/// <param name="usageHint">How will this buffer object be used</param>
	public void Set<DataType>(DataType[] data, BufferUsageHint usageHint = BufferUsageHint.StaticDraw) where DataType : struct
	{
		if (0 == data.Length) throw new ArgumentException("Empty array");
		var elementSize = Marshal.SizeOf(data[0]);
		var byteSize = elementSize * data.Length;
		GL.NamedBufferData(Handle, byteSize, data, usageHint); //copy data over to GPU
	}

	/// <summary>
	/// Copies the given data into a buffer object on the GPU.
	/// </summary>
	/// <typeparam name="DataType">The value data type of each array element</typeparam>
	/// <param name="data">The data as a <see cref="List{DataType}"/></param>
	/// <param name="usageHint">How will this buffer object be used</param>
	public void Set<DataType>(List<DataType> data, BufferUsageHint usageHint = BufferUsageHint.StaticDraw) where DataType : struct
	{
		if (0 == data.Count) throw new ArgumentException("Empty array");
		var elementSize = Marshal.SizeOf(data[0]);
		var byteSize = elementSize * data.Count;
		var span = CollectionsMarshal.AsSpan(data);
		GL.NamedBufferData(Handle, byteSize, ref span[0], usageHint); //copy data over to GPU
	}

	/// <summary>
	/// Copies the given data into a buffer object on the GPU.
	/// </summary>
	/// <param name="data">A pointer to the data</param>
	/// <param name="byteSize">Size of the data in bytes</param>
	/// <param name="usageHint">How will this buffer object be used</param>
	public void Set(IntPtr data, int byteSize, BufferUsageHint usageHint = BufferUsageHint.StaticDraw)
	{
		if (0 == byteSize) throw new ArgumentException($"{nameof(byteSize)} = 0");
		GL.NamedBufferData(Handle, byteSize, data, usageHint); //copy data over to GPU
	}

	/// <summary>
	/// Will be called from the default Dispose method.
	/// Implementers should dispose all their resources her.
	/// </summary>
	protected override void DisposeResources() => GL.DeleteBuffer(Handle);
}
