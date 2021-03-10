using System;

namespace Zenseless.OpenTK
{
	public interface IObjectGL : IDisposable
	{
		int Handle { get; }
	}
}