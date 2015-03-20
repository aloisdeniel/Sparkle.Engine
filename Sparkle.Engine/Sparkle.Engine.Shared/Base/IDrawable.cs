namespace Sparkle.Engine.Base
{
	using System;
	using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Represents an element that could be drawn on the screen.
    /// </summary>
	public interface IDrawable
	{
		/// <summary>
		/// Gets a value indicating whether this instance is visible.
		/// </summary>
		/// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
		bool IsVisible { get; }

		/// <summary>
		/// Gets the draw order.
		/// </summary>
		/// <value>The draw order.</value>
		int DrawOrder { get; }

		/// <summary>
		/// Draw the sprite with the specified sb.
		/// </summary>
		/// <param name="sb">Sb.</param>
		void Draw (SpriteBatch sb);
	}
}

