namespace Sparkle.Engine.Base.Dynamics
{
	using Microsoft.Xna.Framework;
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// A basic float dynamic value implementation.
	/// </summary>
	public class Dynamic : UpdatableBase, IDynamic<float>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Sparkle.Engine.Core.Dynamic"/> class.
		/// </summary>
		public Dynamic () : this (0.0f)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Sparkle.Engine.Core.Dynamic"/> class with a default value.
		/// </summary>
		/// <param name="value">Value.</param>
		public Dynamic (float value)
		{
			this.MinVelocity = float.MinValue;
			this.MaxVelocity = float.MaxValue;
			this.MinAcceleration = float.MinValue;
			this.MaxAcceleration = float.MaxValue;
			this.MinValue = float.MinValue;
			this.MaxValue = float.MaxValue;
			this.Value = value;
		}

		private float acceleration;

		private float velocity;

		private float value;

        public float Friction { get; set; }

		/// <summary>
		/// Gets or sets the acceleration.
		/// </summary>
		/// <value>The acceleration.</value>
		public float Acceleration {
			get { return this.acceleration; }
			set { this.acceleration = value.Clamp (this.MinAcceleration, this.MaxAcceleration); }
		}

		/// <summary>
		/// Gets or sets the velocity.
		/// </summary>
		/// <value>The velocity.</value>
		public float Velocity {
			get { return this.velocity; }
			set { this.velocity = value.Clamp (this.MinVelocity, this.MaxVelocity); }
		}

		/// <summary>
		/// Gets or sets the current value.
		/// </summary>
		/// <value>The value.</value>
		public float Value {
			get { return this.value; }
			set { this.value = value.Clamp (this.MinValue, this.MaxValue); }
		}

		/// <summary>
		/// Gets or sets the maximum value.
		/// </summary>
		/// <value>The max value.</value>
		public float MaxValue { get; set; }

		/// <summary>
		/// Gets or sets the minimum value.
		/// </summary>
		/// <value>The minimum value.</value>
		public float MinValue { get; set; }

		/// <summary>
		/// Gets or sets the maximum velocity.
		/// </summary>
		/// <value>The maximum velocity.</value>
		public float MaxVelocity { get; set; }

		/// <summary>
		/// Gets or sets the minimum velocity.
		/// </summary>
		/// <value>The minimum velocity.</value>
		public float MinVelocity { get; set; }

		/// <summary>
		/// Gets or sets the max acceleration.
		/// </summary>
		/// <value>The max acceleration.</value>
		public float MaxAcceleration { get; set; }

		/// <summary>
		/// Gets or sets the minimum acceleration.
		/// </summary>
		/// <value>The minimum acceleration.</value>
		public float MinAcceleration { get; set; }

		protected override void DoUpdate (GameTime time)
		{
            if(this.Animation != null && this.Animation.IsStarted)
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
			this.Acceleration = 0.0f;
			this.Velocity = 0.0f;
		}

		public bool IsAnimated {
			get { return this.Velocity != 0; }
		}

		public void Animate (TimeSpan duration, Sparkle.Engine.Base.Curve.Mode curve, float start, float end)
		{
			throw new NotImplementedException ();
		}

		public void Animate (TimeSpan duration, Sparkle.Engine.Base.Curve.Mode curve, float end)
		{
			throw new NotImplementedException ();
		}


        public IDynamicAnimation<float> Animation
        {
            get;
            set;
        }
    }
}