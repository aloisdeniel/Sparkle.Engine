namespace Sparkle.Engine.Base.Geometry
{
	using System;
	using Microsoft.Xna.Framework;

	/// <summary>
	/// Represents a 2D circle.
	/// </summary>
	public struct Circle : IShape
	{
		/// <summary>
		/// Constructs a new circle.
		/// </summary>
		public Circle (Vector2 position, float radius)
		{
			Center = position;
			Radius = radius;
		}

		/// <summary>
		/// Center position of the circle.
		/// </summary>
		public Vector2 Center;

		/// <summary>
		/// Radius of the circle.
		/// </summary>
		public float Radius;
        
        #region IShape

        public Frame Aabb
        {
            get
            {
                return new Frame(
                    this.Center.X - this.Radius,
                    this.Center.Y - this.Radius,
                    2 * this.Radius,
                    2 * this.Radius
                    );
            }
        }

        public bool Contains(Vector2 point)
        {
            return (this.Center - point).LengthSquared() <= (this.Radius * this.Radius);
        }

        public bool Contains(Frame shape)
        {
            return shape.Contains(this);
        }

        public bool Contains(Circle shape)
        {
            var distance = (this.Center - shape.Center).Length() + this.Radius + this.Radius;
            return (distance <= 2 * this.Radius);
        }

		public bool Intersects (Frame shape)
		{
			return shape.Intersects (this);
		}

		public bool Intersects (Circle circle)
		{
			return (circle.Center - this.Center).Length () <= (this.Radius + circle.Radius);
		}

        public bool Contains(Segment shape)
        {
            return this.Contains(shape.Start) && this.Contains(shape.End);
        }

        public bool Intersects(Segment shape)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}

