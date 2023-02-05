using OpenTK.Mathematics;

namespace Zenseless.OpenTK
{
	/// <summary>
	/// Contains more methods to help to work with <see cref="Box2"/>
	/// </summary>
	public static class Box2Extensions
	{
		/// <summary>
		/// Create a box that is at least size with x height, but has aspect ratio newWidth2heigth
		/// </summary>
		/// <param name="width">minimal width</param>
		/// <param name="height">minimal height</param>
		/// <param name="aspect">new aspect ratio</param>
		/// <returns>A box that is at least size with x height, but has aspect ratio newWidth2heigth</returns>
		public static Box2 CreateContainingBox(float width, float height, float aspect)
		{
			float boxAspect = width / height;
			if(aspect < boxAspect) // check if landscape
			{
				float outputHeight = width / aspect;
				return CreateFromMinSize(0f, (height - outputHeight) * 0.5f, width, outputHeight);

			}
			else
			{
				float outputWidth = height * aspect;
				return CreateFromMinSize((width - outputWidth) * 0.5f, 0f, outputWidth, (float)height);

			}
		}

		/// <summary>
		/// Checks if <paramref name="a"/> contains <paramref name="b"/> including borders.
		/// </summary>
		/// <param name="a">The first box.</param>
		/// <param name="b">The second box.</param>
		/// <returns><c>true</c> if <paramref name="a"/> contains <paramref name="b"/></returns>
		public static bool Contains(this Box2 a, Box2 b)
		{
			return a.Min.X <= b.Min.X && b.Max.X <= a.Max.X
				&& a.Min.Y <= b.Min.Y && b.Max.Y <= a.Max.Y;
		}

		/// <summary>
		/// Create a new instance of <see cref="Box2"/> from its center and a size vector
		/// </summary>
		/// <param name="center">The center.</param>
		/// <param name="size">A size vector.</param>
		/// <returns>A new instance of <see cref="Box2"/></returns>
		public static Box2 CreateFromCenterSize(Vector2 center, Vector2 size)
		{
			var sizeH = 0.5f * size;
			return new(center - sizeH, center + sizeH);
		}

		/// <summary>
		/// Create a new instance of <see cref="Box2"/> from its center and a size vector
		/// </summary>
		/// <param name="centerX">The center point x-coordinate.</param>
		/// <param name="centerY">The cetner point y-coordinate.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns>A new instance of <see cref="Box2"/></returns>
		public static Box2 CreateFromCenterSize(float centerX, float centerY, float width, float height)
		{
			return CreateFromCenterSize(new Vector2(centerX, centerY), new Vector2(width, height));
		}

		/// <summary>
		/// Create a new instance of <see cref="Box2"/> from a minimal corner point and a size vector
		/// </summary>
		/// <param name="min">A minimal corner point.</param>
		/// <param name="size">A size vector.</param>
		/// <returns>A new instance of <see cref="Box2"/></returns>
		public static Box2 CreateFromMinSize(Vector2 min, Vector2 size) => new(min, min + size);

		/// <summary>
		/// Create a new instance of <see cref="Box2"/> from a minimal corner point and width and height
		/// </summary>
		/// <param name="minX">The minimal corner point x-coordinate.</param>
		/// <param name="minY">The minimal corner point y-coordinate.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns>A new instance of <see cref="Box2"/></returns>
		public static Box2 CreateFromMinSize(float minX, float minY, float width, float height) => new(minX, minY, minX + width, minY + height);


		/// <summary>
		/// Checks if the two given boxes overlap
		/// </summary>
		/// <param name="a">The first box.</param>
		/// <param name="b">The second box.</param>
		/// <returns><c>true</c> if the two boxes do overlap.</returns>
		public static bool Overlaps(this Box2 a, Box2 b)
		{
			bool noXoverlap = a.Max.X <= b.Min.X || a.Min.X >= b.Max.X;
			bool noYoverlap = a.Max.Y <= b.Min.Y || a.Min.Y >= b.Max.Y;
			return !(noXoverlap || noYoverlap);
		}

		/// <summary>
		/// Return a translated <see cref="Box2"/>
		/// </summary>
		/// <param name="box">The box</param>
		/// <param name="tx">The translation vector x-coordinate.</param>
		/// <param name="ty">The translation vector y-coordinate.</param>
		/// <returns></returns>
		public static Box2 Translated(this Box2 box, float tx, float ty) => box.Translated(new Vector2(tx, ty));

		/// <summary>
		/// Calculates a translated version of the <paramref name="boxA"/> that is moved in the minimal direction to undo any overlap between the two input boxes.
		/// </summary>
		/// <param name="boxA">The box for wich a translated version should be calculated.</param>
		/// <param name="boxB">Another box that may overlaps the first box. <paramref name="boxA"/></param>
		/// <returns></returns>
		public static Box2 UndoOberlap(this Box2 boxA, Box2 boxB)
		{
			Vector2[] directions = new Vector2[]
			{
				new Vector2(boxB.Max.X - boxA.Min.X, 0), // push distance A in positive X-direction
				new Vector2(boxB.Min.X - boxA.Max.X, 0), // push distance A in negative X-direction
				new Vector2(0, boxB.Max.Y - boxA.Min.Y), // push distance A in positive Y-direction
				new Vector2(0, boxB.Min.Y - boxA.Max.Y)  // push distance A in negative Y-direction
			};
			float[] pushDistSqrd = new float[4];
			for (int i = 0; i < 4; ++i)
			{
				pushDistSqrd[i] = directions[i].LengthSquared;
			}
			//find minimal positive overlap amount
			int minId = 0;
			for (int i = 1; i < 4; ++i)
			{
				minId = pushDistSqrd[i] < pushDistSqrd[minId] ? i : minId;
			}

			return CreateFromMinSize(boxA.Min + directions[minId], boxA.Size);
		}
	}
}
