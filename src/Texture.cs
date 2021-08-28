﻿using OpenTK.Graphics.OpenGL4;
using Zenseless.Patterns;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// Class that encapsulated an OpenGL texture object
	/// </summary>
	public class Texture : Disposable, IObjectHandle<Texture>
	{
		/// <summary>
		/// Depth component format
		/// </summary>
		public const SizedInternalFormat DepthComponent32f = (SizedInternalFormat)All.DepthComponent32f;

		/// <summary>
		/// Initializes a new instance of the <see cref="Texture" /> class.
		/// </summary>
		/// <param name="target">The texture target</param>
		public Texture(TextureTarget target = TextureTarget.Texture2D)
		{
			GL.CreateTextures(target, 1, out int handle);
			Handle = new(handle);
		}

		/// <summary>
		/// Returns the OpenGL object handle
		/// </summary>
		public Handle<Texture> Handle { get; }

		/// <summary>
		/// Set/get the texture function (wrap mode)
		/// </summary>
		public TextureWrapMode Function
		{
			get => _function;
			set
			{
				_function = value;
				GL.TextureParameter(Handle, TextureParameterName.TextureWrapS, (int)value);
				GL.TextureParameter(Handle, TextureParameterName.TextureWrapT, (int)value);
				GL.TextureParameter(Handle, TextureParameterName.TextureWrapR, (int)value);
			}
		}

		/// <summary>
		/// Set/get the minification filter
		/// </summary>
		public TextureMinFilter MinFilter
		{
			get => _minFilter;
			set
			{
				_minFilter = value;
				GL.TextureParameter(Handle, TextureParameterName.TextureMinFilter, (int)value);
			}
		}

		/// <summary>
		/// Set/get the magnification filter
		/// </summary>
		public TextureMagFilter MagFilter
		{
			get => _magFilter;
			set
			{
				_magFilter = value;
				GL.TextureParameter(Handle, TextureParameterName.TextureMagFilter, (int)value);
			}
		}

		/// <summary>
		/// Will be called from the default Dispose method.
		/// Implementers should dispose all their resources her.
		/// </summary>
		protected override void DisposeResources() => GL.DeleteTexture(Handle);

		private TextureWrapMode _function;
		private TextureMinFilter _minFilter;
		private TextureMagFilter _magFilter;
	}
}
