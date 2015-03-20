namespace Sparkle.Engine.Base.Dynamics
{
	using System;
	using Sparkle.Engine.Core;
	using Microsoft.Xna.Framework;

	/// <summary>
	/// A basic vector with two dimensions dynamic value implementation.
	/// </summary>
	public class Dynamic2 : UpdatableBase, IDynamic<Vector2>
	{
		public Dynamic2 ()
		{
			this.MinVelocity = new Vector2 (float.MinValue, float.MinValue);
			this.MaxVelocity = new Vector2 (float.MaxValue, float.MaxValue);
			this.MinAcceleration = new Vector2 (float.MinValue, float.MinValue);
			this.MaxAcceleration = new Vector2 (float.MaxValue, float.MaxValue);
			this.MinValue = new Vector2 (float.MinValue, float.MinValue);
			this.MaxValue = new Vector2 (float.MaxValue, float.MaxValue);
		}

		public Dynamic2 (float x, float y) : this ()
		{
			this.Value = new Vector2 (x, y);
		}

		private Vector2 acceleration;

		private Vector2 velocity;

		private Vector2 value;

        public Vector2 Friction { get; set; }

		/// <summary>
		/// Gets or sets the acceleration.
		/// </summary>
		/// <value>The acceleration.</value>
		public Vector2 Acceleration {
			get { return this.acceleration; }
			set { this.acceleration = value.Clamp (this.MinAcceleration, this.MaxAcceleration); }
		}

		/// <summary>
		/// Gets or sets the velocity.
		/// </summary>
		/// <value>The velocity.</value>
		public Vector2 Velocity {
			get { return this.velocity; }
			set { this.velocity = value.Clamp (this.MinVelocity, this.MaxVelocity); }
		}

		/// <summary>
		/// Gets or sets the current value.
		/// </summary>
		/// <value>The value.</value>
		public Vector2 Value {
			get { return this.value; }
			set { this.value = value.Clamp (this.MinValue, this.MaxValue); }
		}

		/// <summary>
		/// Gets or sets the maximum value.
		/// </summary>
		/// <value>The max value.</value>
		public Vector2 MaxValue { get; set; }

		/// <summary>
		/// Gets or sets the minimum value.
		/// </summary>
		/// <value>The minimum value.</value>
		public Vector2 MinValue { get; set; }

		/// <summary>
		/// Gets or sets the maximum velocity.
		/// </summary>
		/// <value>The maximum velocity.</value>
		public Vector2 MaxVelocity { get; set; }

		/// <summary>
		/// Gets or sets the minimum velocity.
		/// </summary>
		/// <value>The minimum velocity.</value>
		public Vector2 MinVelocity { get; set; }

		/// <summary>
		/// Gets or sets the max acceleration.
		/// </summary>
		/// <value>The max acceleration.</value>
		public Vector2 MaxAcceleration { get; set; }

		/// <summary>
		/// Gets or sets the minimum acceleration.
		/// </summary>
		/// <value>The minimum acceleration.</value>
		public Vector2 MinAcceleration { get; set; }

		protected override void DoUpdate (GameTime time)
		{
            if (this.Animation != null && this.Animation.IsStarted)
            {
                this.Animation.Update(time);
                this.Value = this.Animation.Value;
            }
            else
            {
                this.Velocity += (0.5f * this.Acceleration * Default.AccelerationMultiplier * (float)Math.Pow(time.ElapsedGameTime.Milliseconds, 2));
                this.Velocity -= this.Friction * this.Velocity;
                this.Value += this.Velocity * Default.VelocityMultiplier * time.ElapsedGameTime.Milliseconds;
            }
		}

		public void Stop ()
		{
			this.Acceleration = Vector2.Zero;
			this.Velocity = Vector2.Zero;
		}

		public bool IsAnimated {
			get { return this.Velocity != Vector2.Zero; }
		}

		public void Animate (TimeSpan duration, Sparkle.Engine.Base.Curve.Mode curve, Vector2 start, Vector2 end)
		{
			throw new NotImplementedException ();
		}

		public void Animate (TimeSpan duration, Sparkle.Engine.Base.Curve.Mode curve, Vector2 end)
		{
			throw new NotImplementedException ();
		}


        public IDynamicAnimation<Vector2> Animation
        {
            get;
            set;
        }
    }
}

