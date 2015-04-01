namespace Sparkle.Engine.Base.Shapes
{
	using System;
	using Microsoft.Xna.Framework;

	/// <summary>
	/// Representents a rectangular shape on the screen.
	/// </summary>
	public struct Frame : IShape
	{
		public Frame (float x, float y, float w, float h)
		{
			X = x;
			Y = y;
			Width = w;
			Height = h;
		}

		/// <summary>
		/// Top left X coordinates of the frame.
		/// </summary>
		public float X;

		/// <summary>
		/// Top left Y coordinates of the frame.
		/// </summary>
		public float Y;

		/// <summary>
		/// Width of the frame.
		/// </summary>
		public float Width;

		/// <summary>
		/// Height of the frame.
		/// </summary>
		public float Height;

		/// <summary>
		/// The left edge X value.
		/// </summary>
		public float Left { get { return X; } }

		/// <summary>
		/// The right edge X value.
		/// </summary>
		public float Right { get { return X + Width; } }

		/// <summary>
		/// The top edge Y value.
		/// </summary>
		public float Top { get { return Y; } }

		/// <summary>
		/// The bottom edge Y value.
		/// </summary>
		public float Bottom { get { return Y + Height; } }

		/// <summary>
		/// The left edge of the frame.
		/// </summary>
		public Segment LeftEdge { get { return new Segment (this.Left, this.Top, this.Left, this.Bottom); } }

		/// <summary>
		/// The right edge of the frame.
		/// </summary>
		public Segment RightEdge { get { return new Segment (this.Right, this.Top, this.Right, this.Bottom); } }

		/// <summary>
		/// The top edge of the frame.
		/// </summary>
		public Segment TopEdge{ get { return new Segment (this.Left, this.Top, this.Right, this.Top); } }

		/// <summary>
		/// The bottom edge of the frame.
		/// </summary>
		public Segment BottomEdge { get { return new Segment (this.Left, this.Bottom, this.Right, this.Bottom); } }

		/// <summary>
		/// Represents the center position.
		/// </summary>
		public Vector2 Center { get { return new Vector2 (X + this.Width / 2, Y + this.Height / 2); } }

		/// <summary>
		/// Represents the top left verticle.
		/// </summary>
		public Vector2 TopLeft { get { return new Vector2 (X, Y); } }

		/// <summary>
		/// Represents the top right verticle.
		/// </summary>
		public Vector2 TopRight { get { return new Vector2 (X + this.Width, Y); } }

		/// <summary>
		/// Represents the bottom right verticle.
		/// </summary>
		public Vector2 BottomRight { get { return new Vector2 (X + this.Width, Y + this.Height); } }

		/// <summary>
		/// Represents the bottom left verticle.
		/// </summary>
		public Vector2 BottomLeft { get { return new Vector2 (X, Y + this.Height); } }
        
		/// <summary>
		/// Conversion to a rectangle with integer values.
		/// </summary>
		public Rectangle Rectangle { get { return new Rectangle ((int)X, (int)Y, (int)Width, (int)Height); } }

		public Frame Translate (Vector2 offset)
		{
			return new Frame (this.X + offset.X, this.Y + offset.Y, this.Width, this.Height);
		}

		#region IShape

		public Frame Aabb { get { return this; } }

		public bool Contains (Vector2 point)
		{
			return point.X >= this.Left && point.X <= this.Right && point.Y >= this.Top && point.Y <= this.Bottom;
		}

		public bool Contains (Frame other)
		{
			return other.Left >= this.Left && other.Right <= this.Right && other.Top >= this.Top && other.Bottom <= this.Bottom;
		}

		public bool Contains (Circle shape)
		{
			return this.Contains (shape.Aabb);
		}

		public bool Contains (Segment shape)
		{
			return this.Contains (shape.Start) && this.Contains (shape.End);
		}

		public bool Intersects (Frame other)
		{
			return !((other.X >= this.Right) || (other.Right <= this.X) || (other.Y >= this.Bottom) || (other.Bottom <= this.Y));
		}

		public bool Intersects (Circle shape)
		{
			Vector2 v = new Vector2 (
				            MathHelper.Clamp (shape.Center.X, this.Left, this.Right),
				            MathHelper.Clamp (shape.Center.Y, this.Top, this.Bottom));
			Vector2 direction = shape.Center - v;
			float distanceSquared = direction.LengthSquared ();
			return ((distanceSquared > 0) && (distanceSquared < shape.Radius * shape.Radius));
		}

		public bool Intersects (Segment shape)
		{
			return shape.Intersects (this.LeftEdge) || shape.Intersects (this.RightEdge) || shape.Intersects (this.TopEdge) || shape.Intersects (this.BottomEdge);
		}

		#endregion
	}
}

