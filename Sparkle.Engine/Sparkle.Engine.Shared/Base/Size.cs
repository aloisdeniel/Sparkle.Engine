namespace Sparkle.Engine.Base
{
    using Microsoft.Xna.Framework;
    using System;

    /// <summary>
    /// Represents a rectangular size of an element.
    /// </summary>
	public struct Size
	{
		public Size (float width, float height)
		{
			Width = width;
			Height = height;
		}

		public float Width;

		public float Height;

        public Vector2 Center { get { return new Vector2(this.Width / 2, this.Height / 2); } }
	}
}

