using OpenTK.Graphics.OpenGL4;
using Zenseless.Patterns;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// Class that abstracts OpenGL Queries
	/// </summary>
	/// <seealso cref="Disposable" />
	public class Query : Disposable, IObjectHandle<Query>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Query"/> class.
		/// </summary>
		/// <param name="target">The query target.</param>
		public Query(QueryTarget target)
		{
			GL.CreateQueries(target, 1, out int handle);
			Target = target;
			Handle = handle.CheckValidHandle<Query>();
		}

		/// <summary>
		/// Begins a query.
		/// </summary>
		public void Begin()
		{
			GL.BeginQuery(Target, Handle);
		}

		/// <summary>
		/// Ends a query.
		/// </summary>
		public void End()
		{
			GL.EndQuery(Target);
		}

		/// <summary>
		/// Checks if the query has finished.
		/// </summary>
		public bool IsFinished
		{
			get
			{
				GL.GetQueryObject(Handle, GetQueryObjectParam.QueryResultAvailable, out int isFinished);
				return 1 == isFinished;
			}
		}

		/// <summary>
		/// Returns the result of the query. Will wait for the query to finish.
		/// </summary>
		public int Result
		{
			get
			{
				GL.GetQueryObject(Handle, GetQueryObjectParam.QueryResult, out int result);
				return result;
			}
		}

		/// <summary>
		/// Returns the result of the query. Will wait for the query to finish.
		/// </summary>
		public long ResultLong
		{
			get
			{
				GL.GetQueryObject(Handle, GetQueryObjectParam.QueryResult, out long result);
				return result;
			}
		}

		/// <summary>
		/// The query target
		/// </summary>
		public QueryTarget Target { get; private set; }

		/// <summary>
		/// Returns the OpenGL object handle
		/// </summary>
		public Handle<Query> Handle { get; }

		/// <summary>
		/// Tries to return the result of the query without waiting for the query to finish.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <returns><c>true</c> if query has finished.</returns>
		public bool TryGetResult(out int result)
		{
			GL.GetQueryObject(Handle, GetQueryObjectParam.QueryResultNoWait, out result);
			return -1 != result;
		}

		/// <summary>
		/// Tries to return the result of the query without waiting for the query to finish.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <returns><c>true</c> if query has finished.</returns>
		public bool TryGetResult(out long result)
		{
			GL.GetQueryObject(Handle, GetQueryObjectParam.QueryResultNoWait, out result);
			return -1 != result;
		}

		/// <summary>
		/// Will be called from the default Dispose method.
		/// Implementers should dispose all their resources her.
		/// </summary>
		protected override void DisposeResources() => GL.DeleteQuery(Handle);
	}
}
