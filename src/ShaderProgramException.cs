using System;
using System.Runtime.Serialization;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// The exception class for shader programs.
	/// </summary>
	/// <seealso cref="Exception" />
	[Serializable]
	public class ShaderProgramException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShaderProgramException"/> class.
		/// </summary>
		public ShaderProgramException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShaderException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ShaderProgramException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShaderException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		public ShaderProgramException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShaderException"/> class with serialized data.
		/// </summary>
		/// <param name="info">Holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">Contains contextual information about the source or destination.</param>
		protected ShaderProgramException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}