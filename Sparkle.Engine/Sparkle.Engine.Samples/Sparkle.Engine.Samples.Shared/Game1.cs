namespace Sparkle.Engine.Samples
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Sparkle.Engine.Core;
    using Sparkle.Engine.Base;

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : SparkleGame
	{

		public Game1 ()
		{
            Graphics.IsFullScreen = true;
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
        protected override void Initialize()
        {
        }
	}
}
