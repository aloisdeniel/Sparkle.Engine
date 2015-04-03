namespace Sparkle.Engine
{
    using System;
    using Microsoft.Xna.Framework;
    using Sparkle.Engine.Core;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;
    using System.Linq;
    using Sparkle.Engine.Base;

	public class SparkleGame : Game
	{
		public SparkleGame ()
        {
            this.Systems = new List<Core.Systems.System>();
            this.Graphics = new GraphicsDeviceManager(this);
		}

        public GraphicsDeviceManager Graphics { get; protected set; }

        public SpriteBatch SpriteBatch { get; protected set; }

        public List<Core.Systems.System> Systems { get; private set; }

        public Scene Scene { get; set; }

        protected override void Initialize()
        {
            base.Initialize();

            foreach (var system in this.Systems.OfType<IInitializable>())
            {
                system.Initialize();
            }
        }

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
        {
            this.SpriteBatch = new SpriteBatch(this.GraphicsDevice);

            foreach (var system in this.Systems.OfType<ILoadable>())
            {
                system.LoadContent(this.Content);
            }

            base.LoadContent();
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent ()
        {
            foreach (var system in this.Systems.OfType<ILoadable>())
            {
                system.UnloadContent(this.Content);
            }

            base.UnloadContent();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
            foreach (var system in this.Systems.OfType<Base.IUpdateable>())
            {
                system.Update(gameTime);
            }

			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
            base.Draw(gameTime);

            foreach (var system in this.Systems.OfType<Base.IDrawable>())
            {
                system.Draw(this.SpriteBatch);
            }
		}
	}
}

