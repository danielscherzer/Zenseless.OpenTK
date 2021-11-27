using Zenseless.Patterns;

namespace Zenseless.OpenTK
{
	internal static class HandleExtensions
	{
		public static Handle<TType> CreateValidHandle<TType>(this int handleId)
		{
			if (0 == handleId) throw new OpenGLException($"Error creating OpenGL object of type {typeof(TType).Name}.");
			return new(handleId);
		}
	}
}
