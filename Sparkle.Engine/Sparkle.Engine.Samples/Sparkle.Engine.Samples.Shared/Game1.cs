namespace Sparkle.Engine.Samples
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Sparkle.Engine.Core;
    using Sparkle.Engine.Base;
    using Sparkle.Engine.Samples.Shared.Entities;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : SparkleGame
    {
        public Game1 ()
        {
            Content.RootDirectory = "Content";

            var character = new Character(10, 10);

            this.Scene.AddEntity(character);
        }

    }
}