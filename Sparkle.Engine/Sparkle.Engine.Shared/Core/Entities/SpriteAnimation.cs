namespace Sparkle.Engine.Core.Entities
{
	using System;
	using System.Linq;
	using Microsoft.Xna.Framework;
	using System.Collections.Generic;
	using Sparkle.Engine.Base;

	/// <summary>
	/// Represents an animation for a sprite sheet.
	/// The common way for creating an animation is by using factory methods like :
	///   var anim = SpriteAnimation.Create(1024,1024,4,4).From(0,0).And(0,1).To(4,1).During(500.0);
	/// </summary>
	public class SpriteAnimation : UpdatableBase
	{
		/// <summary>
		/// Instanciate an animation.
		/// </summary>
		/// <param name="width">Width of the spritesheet texture</param>
		/// <param name="height">Height of the spritesheet texture</param>
		/// <param name="columns">Number of columns into the sheet</param>
		/// <param name="rows">Number of rows into the sheet</param>
		public SpriteAnimation (Sprite sprite, int columns, int rows)
		{
			this.Parent = sprite;
			this.Columns = columns;
			this.Rows = rows;
			this.Steps = new List<Point> ();
		}

		/// <summary>
		/// Width of an animation frame.
		/// </summary>
		public int Width { get { return this.Parent.Texture.Width / this.Columns; } }

		/// <summary>
		/// Height of an animation frame.
		/// </summary>
		public int Height { get { return this.Parent.Texture.Height / this.Rows; } }

		/// <summary>
		/// Number of columns into this sheet.
		/// </summary>
		public int Columns { get; set; }

		/// <summary>
		/// Number of rows into this sheet.
		/// </summary>
		public int Rows { get; set; }

		/// <summary>
		/// The current source area of the texture.
		/// </summary>
		public Rectangle Source {
			get {
				var step = (this.Steps.Count == 0) ? Point.Zero : this.Steps [index];
				return new Rectangle (step.X * this.Width, step.Y * this.Height, this.Width, this.Height);
			}
		}

		/// <summary>
		/// All the animation steps.
		/// </summary>
		public List<Point> Steps { get; set; }

		/// <summary>
		/// The repeat mode of the animation.
		/// </summary>
		public RepeatMode Mode { get; set; }

		/// <summary>
		/// Gets or sets the parent.
		/// </summary>
		/// <value>The parent.</value>
		public Sprite Parent { get; set; }

		#region Fields

		/// <summary>
		/// Current step index.
		/// </summary>
		private int index;

		/// <summary>
		/// Indicates whether the animation has already been initialized and can't be modified anymore.
		/// </summary>
		private bool isLocked;

		#endregion

		public bool IsStarted { get; private set; }

		public double CurrentTime { get; set; }

		public double Interval { get; set; }

		#region Factory

		/// <summary>
		/// Adds a step.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public SpriteAnimation And (int x, int y)
		{
			if (isLocked) {
				throw new NotSupportedException ("You can't add animation steps after duration definition.");
			}

			this.Steps.Add (new Point (x, y));
			return this;
		}

		/// <summary>
		/// Clears the steps and define the first one.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public SpriteAnimation From (int x, int y)
		{
			if (isLocked) {
				throw new NotSupportedException ("You can't add animation steps after duration definition.");
			}

			this.Steps.Clear ();
			return this.And (x, y);
		}

		/// <summary>
		/// Add all steps to frame with X coordinates on current Y.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public SpriteAnimation To (int x)
		{
			var last = this.Steps.Last ();
			return this.To (x, last.Y);
		}

		/// <summary>
		/// Add all steps from last added step to given coordinates with a X priority.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public SpriteAnimation To (int x, int y)
		{
			var last = this.Steps.Last ();
			var min = new Point (Math.Min (x, last.X), Math.Min (y, last.Y));
			var max = new Point (Math.Max (x, last.X), Math.Max (y, last.Y));
			var current = new Point (min.X, min.Y);

			while (!(current.X == max.X && current.Y == max.Y)) {

				current.X++;

				if (current.X >= this.Columns) {

					current.X = min.X;
					current.Y++;
				}

				this.Steps.Add (current);
			}

			return this;
		}

		/// <summary>
		/// Defines the interval beetween steps.
		/// </summary>
		/// <param name="ms"></param>
		/// <returns></returns>
		public SpriteAnimation WithInterval (double ms)
		{
			if (this.Steps.Count == 0) {
				throw new NotSupportedException ("You can't define animation duration before definition of steps.");
			}

			this.Interval = ms;
			this.isLocked = true;
			return this;
		}

		/// <summary>
		/// Defines the total duration of the animation.
		/// </summary>
		/// <param name="ms"></param>
		/// <returns></returns>
		public SpriteAnimation During (double ms)
		{
			return this.WithInterval (ms / this.Steps.Count);
		}

		#endregion

		protected override void DoUpdate (GameTime gameTime)
		{
			if (this.IsStarted) {
				this.CurrentTime += gameTime.ElapsedGameTime.Milliseconds;
				this.index = (int)(CurrentTime / this.Interval);

                if (this.Mode == RepeatMode.Reverse)
                    this.index = this.Steps.Count - this.index - 1;
				else if (this.Mode == RepeatMode.Loop)
					this.index %= this.Steps.Count;
				else if (this.Mode == RepeatMode.LoopWithReverse)
					this.index %= this.Steps.Count * 2;

				if ((this.Mode == RepeatMode.Once && this.index >= this.Steps.Count) ||
                    (this.Mode == RepeatMode.Reverse && this.index <= 0) ||
				    (this.Mode == RepeatMode.OnceWithReverse && this.index >= 2 * this.Steps.Count)) {
					this.Stop ();
				} else if ((this.Mode == RepeatMode.OnceWithReverse || this.Mode == RepeatMode.LoopWithReverse)
				           && this.index >= this.Steps.Count) {
					this.index = this.Steps.Count * 2 - this.index - 1;
				}
			}
		}

		public void Play (RepeatMode mode)
		{
			this.Mode = mode;
			this.IsStarted = true;
		}

		public void Play ()
		{
			this.IsStarted = true;
		}

		public void Stop ()
		{
			this.IsStarted = false;
			this.CurrentTime = 0.0;
			this.index = 0;
		}

		public void Pause ()
		{
			this.IsStarted = false;
		}
	}
}

