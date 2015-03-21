namespace Sparkle.Engine.Base.Dynamics
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A base dynamic value abstract class for implementations.
    /// </summary>
    public abstract class DynamicBase<T> : UpdatableBase, IDynamic<T> where T : struct
    {
        public DynamicBase(T value)
        {
            this.Value = value;
        }
        
        private T acceleration;

        private T velocity;

        private T value;

        public T Friction { get; set; }
        
        protected abstract T GetMinValue(T value, T other);

        protected abstract T GetMaxValue(T value, T other);
        
        protected abstract T AddValues(T value, T other);

        protected abstract T SubstractValues(T value, T other);

        protected abstract T MultiplyValues(T value, T other);

        protected abstract T MultiplyValues(T value, double v);

        protected abstract T GetZeroValue();

        /// <summary>
        /// Gets or sets the acceleration.
        /// </summary>
        /// <value>The acceleration.</value>
        public T Acceleration
        {
            get { return this.acceleration; }
            set {
                if (this.MinAcceleration != null) value = this.GetMaxValue(value, (T)this.MinAcceleration);
                if (this.MaxAcceleration != null) value = this.GetMinValue(value, (T)this.MaxAcceleration);
                this.acceleration = value;
            }
        }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        /// <value>The velocity.</value>
        public T Velocity
        {
            get { return this.velocity; }
            set
            {
                if (this.MinVelocity != null) value = this.GetMaxValue(value, (T)this.MinVelocity);
                if (this.MaxVelocity != null) value = this.GetMinValue(value, (T)this.MaxVelocity);
                this.velocity = value;
            }
        }

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        /// <value>The value.</value>
        public T Value
        {
            get { return this.value; }
            set
            {
                if (this.MinValue != null) value = this.GetMaxValue(value, (T)this.MinValue);
                if (this.MaxValue != null) value = this.GetMinValue(value, (T)this.MaxValue);
                this.value = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The max value.</value>
        public T? MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        public T? MinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum velocity.
        /// </summary>
        /// <value>The maximum velocity.</value>
        public T? MaxVelocity { get; set; }

        /// <summary>
        /// Gets or sets the minimum velocity.
        /// </summary>
        /// <value>The minimum velocity.</value>
        public T? MinVelocity { get; set; }

        /// <summary>
        /// Gets or sets the max acceleration.
        /// </summary>
        /// <value>The max acceleration.</value>
        public T? MaxAcceleration { get; set; }

        /// <summary>
        /// Gets or sets the minimum acceleration.
        /// </summary>
        /// <value>The minimum acceleration.</value>
        public T? MinAcceleration { get; set; }

        protected override void DoUpdate(GameTime time)
        {
            if (this.Animation != null && this.Animation.IsStarted)
            {
                this.Animation.Update(time);
                this.Value = this.Animation.Value;
            }
            else
            {
                this.Velocity = this.AddValues(this.Velocity, this.MultiplyValues(this.Acceleration, 0.5f * Default.AccelerationMultiplier * (float)Math.Pow(time.ElapsedGameTime.Milliseconds, 2)));
                this.Velocity = this.SubstractValues(this.Velocity, this.MultiplyValues(this.Friction, this.Velocity));
                this.Value = this.AddValues(this.Value, this.MultiplyValues(this.Velocity, Default.VelocityMultiplier * time.ElapsedGameTime.Milliseconds));
            }
        }

        public void Stop()
        {
            this.Acceleration = this.GetZeroValue();
            this.Velocity = this.GetZeroValue();
        }

        public bool IsAnimated
        {
            get { return !EqualityComparer<T>.Default.Equals(this.Velocity,this.GetZeroValue()); }
        }

        public void Animate(TimeSpan duration, Sparkle.Engine.Base.Curve.Mode curve, T start, T end)
        {
            throw new NotImplementedException();
        }

        public void Animate(TimeSpan duration, Sparkle.Engine.Base.Curve.Mode curve, T end)
        {
            throw new NotImplementedException();
        }


        public IDynamicAnimation<T> Animation
        {
            get;
            set;
        }
    }
}