using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.Serialization;

namespace Zenseless.OpenTK;

/// <summary>
/// The exception class for shaders.
/// </summary>
/// <seealso cref="OpenGLException" />
[Serializable]
public class ShaderException : OpenGLException
{
	/// <summary>
	/// The type of shader.
	/// </summary>
	public ShaderType ShaderType { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ShaderException"/> class.
	/// </summary>
	public ShaderException()
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ShaderException"/> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public ShaderException(string message) : base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ShaderException"/> class.
	/// </summary>
	/// <param name="shaderType">The type of shader.</param>
	/// <param name="message">The message that describes the error.</param>
	public ShaderException(ShaderType shaderType, string message) : base(message)
	{
		ShaderType = shaderType;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ShaderException"/> class.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
	public ShaderException(string message, Exception innerException) : base(message, innerException)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ShaderException"/> class with serialized data.
	/// </summary>
	/// <param name="info">Holds the serialized object data about the exception being thrown.</param>
	/// <param name="context">Contains contextual information about the source or destination.</param>
	protected ShaderException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
		ShaderType = (ShaderType)(info.GetValue(nameof(ShaderType), typeof(ShaderType)) ?? throw new SerializationException());
	}
}