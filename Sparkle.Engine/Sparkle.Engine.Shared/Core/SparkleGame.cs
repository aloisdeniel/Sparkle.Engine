using System;
using Microsoft.Xna.Framework;
using Sparkle.Engine.Tools;
using Sparkle.Engine.Core;
using Microsoft.Xna.Framework.Graphics;

namespace Sparkle.Engine
{
	public class SparkleGame : Game
	{
		public SparkleGame ()
		{
		}

		public World World { get; protected set; }

		public GraphicsDeviceManager Graphics { get; protected set; }

		public SpriteBatch SpriteBatch { get; protected set; }

		private FrameCounter Fps = new FrameCounter ();

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			SpriteBatch = new SpriteBatch (GraphicsDevice);

			Primitives.LoadContent (GraphicsDevice);

			if (this.World != null)
				this.World.LoadContent (this.Content);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent ()
		{
			if (this.World != null)
				this.World.UnloadContent (this.Content);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{

			if (this.World != null)
				this.World.Update (gameTime);

			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			GraphicsDevice.Clear (Color.CornflowerBlue);

			if (this.World != null)
				this.World.Draw (SpriteBatch);

			this.Fps.Draw (this.SpriteBatch, gameTime);

			base.Draw (gameTime);
		}
	}
}

