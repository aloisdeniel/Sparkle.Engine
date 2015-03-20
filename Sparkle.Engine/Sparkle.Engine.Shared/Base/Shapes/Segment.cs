namespace Sparkle.Engine.Base.Shapes
{
    using System;
    using Microsoft.Xna.Framework;

	public struct Segment
	{
		public Segment (Vector2 start, Vector2 end)
		{
			Start = start;
			End = end;
		}

		public Segment (float x, float y, float endX, float endY)
			: this (new Vector2 (x, y), new Vector2 (endX, endY))
		{
		}

		public Vector2 Start;

		public Vector2 End;

        public bool Intersects(Frame shape)
        {
            return shape.Intersects(this);
        }

		public bool Intersects (Segment shape)
		{
			throw new NotImplementedException ();
		}
	}
}

