namespace Sparkle.Engine.Physics
{
    using System;
    using Sparkle.Engine.Base;
    using Microsoft.Xna.Framework;
    using Sparkle.Engine.Base.Dynamics;

	public abstract class Body
	{
		public Body ()
		{
		}

		public float Mass { get; set; }

		public Dynamic2 Position { get; set; }

		public abstract bool IsColliding (BoxBody other);

		public abstract bool IsColliding (CircleBody other);
	}
}

