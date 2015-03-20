namespace Sparkle.Engine.Base.Dynamics
{
	using Microsoft.Xna.Framework;
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// Represents a dynamic value, that have a velocity and an acceleration.
	/// </summary>
	public interface IDynamic<T> : IUpdateable
	{
        /// <summary>
        /// Defines a friction coeficient that increase with velocity. Even if there's no acceleration it will always be this friction agains velocity.
        /// </summary>
        T Friction { get; set; }

		/// <summary>
		/// Gets or sets the current value.
		/// </summary>
		/// <value>The value.</value>
		T Value { get; set; }

		/// <summary>
		/// Gets or sets the maximum value.
		/// </summary>
		/// <value>The max value.</value>
		T MaxValue { get; set; }

		/// <summary>
		/// Gets or sets the minimum value.
		/// </summary>
		/// <value>The minimum value.</value>
		T MinValue { get; set; }

		/// <summary>
		/// Gets or sets the current velocity.
		/// </summary>
		/// <value>The velocity.</value>
		T Velocity { get; set; }

		/// <summary>
		/// Gets or sets the maximum velocity.
		/// </summary>
		/// <value>The maximum velocity.</value>
		T MaxVelocity { get; set; }

		/// <summary>
		/// Gets or sets the minimum velocity.
		/// </summary>
		/// <value>The minimum velocity.</value>
		T MinVelocity { get; set; }

		/// <summary>
		/// Gets or sets the current acceleration.
		/// </summary>
		/// <value>The acceleration.</value>
		T Acceleration { get; set; }

		/// <summary>
		/// Gets or sets the max accleration.
		/// </summary>
		/// <value>The max accleration.</value>
		T MaxAcceleration { get; set; }

		/// <summary>
		/// Gets or sets the minimum acceleration.
		/// </summary>
		/// <value>The minimum acceleration.</value>
		T MinAcceleration { get; set; }

        /// <summary>
        /// Gets or sets the current animation. If an animation is started, the acceleration/velocity/position values will not affect current value.
        /// </summary>
        /// <value>The current animation.</value>
        IDynamicAnimation<T> Animation { get; set; }

		/// <summary>
		/// Stop this instance.
		/// </summary>
		void Stop ();

		/// <summary>
		/// Gets a value indicating whether this instance is animated.
		/// </summary>
		/// <value><c>true</c> if this instance is animated; otherwise, <c>false</c>.</value>
		bool IsAnimated { get; }

		/// <summary>
		/// Animate the specified value with given duration, curve mode, start and end values.
		/// </summary>
		/// <param name="duration">Duration.</param>
		/// <param name="curve">Curve.</param>
		/// <param name="start">Start.</param>
		/// <param name="end">End.</param>
		void Animate (TimeSpan duration, Sparkle.Engine.Base.Curve.Mode curve, T start, T end);

		/// <summary>
		/// Animate the specified value with given duration, curve mode and end value.
		/// </summary>
		/// <param name="duration">Duration.</param>
		/// <param name="curve">Curve.</param>
		/// <param name="end">End.</param>
        void Animate(TimeSpan duration, Sparkle.Engine.Base.Curve.Mode curve, T end);
	}
}
