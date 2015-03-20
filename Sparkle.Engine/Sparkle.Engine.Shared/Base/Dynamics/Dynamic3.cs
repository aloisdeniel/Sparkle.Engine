namespace Sparkle.Engine.Base.Dynamics
{
	using Microsoft.Xna.Framework;
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Diagnostics;

	/// <summary>
	/// A basic vector with three dimensions dynamic value implementation.
	/// </summary>
	public class Dynamic3 : UpdatableBase, IDynamic<Vector3>
	{
		public Dynamic3 ()
		{
			this.MinVelocity = new Vector3 (float.MinValue, float.MinValue, float.MinValue);
			this.MaxVelocity = new Vector3 (float.MaxValue, float.MaxValue, float.MaxValue);
			this.MinAcceleration = new Vector3 (float.MinValue, float.MinValue, float.MinValue);
			this.MaxAcceleration = new Vector3 (float.MaxValue, float.MaxValue, float.MaxValue);
			this.MinValue = new Vector3 (float.MinValue, float.MinValue, float.MinValue);
			this.MaxValue = new Vector3 (float.MaxValue, float.MaxValue, float.MaxValue);
		}

		public Dynamic3 (float x, float y, float z) : this ()
		{
			this.Value = new Vector3 (x, y, z);
		}

		private Vector3 acceleration;

		private Vector3 velocity;

		private Vector3 value;

        public Vector3 Friction { get; set; }

		/// <summary>
		/// Gets or sets the acceleration.
		/// </summary>
		/// <value>The acceleration.</value>
		public Vector3 Acceleration {
			get { return this.acceleration; }
			set { this.acceleration = value.Clamp (this.MinAcceleration, this.MaxAcceleration); }
		}

		/// <summary>
		/// Gets or sets the velocity.
		/// </summary>
		/// <value>The velocity.</value>
		public Vector3 Velocity {
			get { return this.velocity; }
			set { this.velocity = value.Clamp (this.MinVelocity, this.MaxVelocity); }
		}

		/// <summary>
		/// Gets or sets the current value.
		/// </summary>
		/// <value>The value.</value>
		public Vector3 Value {
			get { return this.value; }
			set { 
				var v = value;
				v = value.Clamp (this.MinValue, this.MaxValue);
				this.value = v; 
			}
		}

		/// <summary>
		/// Gets or sets the maximum value.
		/// </summary>
		/// <value>The max value.</value>
		public Vector3 MaxValue { get; set; }

		/// <summary>
		/// Gets or sets the minimum value.
		/// </summary>
		/// <value>The minimum value.</value>
		public Vector3 MinValue { get; set; }

		/// <summary>
		/// Gets or sets the maximum velocity.
		/// </summary>
		/// <value>The maximum velocity.</value>
		public Vector3 MaxVelocity { get; set; }

		/// <summary>
		/// Gets or sets the minimum velocity.
		/// </summary>
		/// <value>The minimum velocity.</value>
		public Vector3 MinVelocity { get; set; }

		/// <summary>
		/// Gets or sets the max acceleration.
		/// </summary>
		/// <value>The max acceleration.</value>
		public Vector3 MaxAcceleration { get; set; }

		/// <summary>
		/// Gets or sets the minimum acceleration.
		/// </summary>
		/// <value>The minimum acceleration.</value>
		public Vector3 MinAcceleration { get; set; }

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
			this.Acceleration = Vector3.Zero;
			this.Velocity = Vector3.Zero;
		}

		public bool IsAnimated {
			get { return this.Velocity != Vector3.Zero; }
		}

		public void Animate (TimeSpan duration, Sparkle.Engine.Base.Curve.Mode curve, Vector3 start, Vector3 end)
		{
			throw new NotImplementedException ();
		}

		public void Animate (TimeSpan duration, Sparkle.Engine.Base.Curve.Mode curve, Vector3 end)
		{
			throw new NotImplementedException ();
		}


        public IDynamicAnimation<Vector3> Animation
        {
            get;
            set;
        }
    }
}
