using Sparkle.Engine.Tools;

namespace Sparkle.Engine.Samples
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Sparkle.Engine.Core;
	using Sparkle.Engine.Samples.Shared;
	using Sparkle.Engine.Base;
	using Sparkle.Engine.Samples.Shared.Entities.Controllers;
    using Sparkle.Engine.Samples.Shared.Entities;

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : SparkleGame
	{

		public Game1 ()
		{
			Graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			var screen = new Size (this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height);

			this.World = new World (screen.Height, screen.Height, screen);
			this.World.IsDebugging = true;

			this.World.RegisterEntity<GreenGuy> ("greenguy");
            this.World.RegisterEntity<OrangeGuy>("orangeguy");
            this.World.RegisterEntity<Character>("hero");

			this.World.SpawnEntity ("greenguy", 150, 150);
            this.World.SpawnEntity("orangeguy", 200, 150);
            var hero = this.World.SpawnEntity("hero", 200, 100) as Character;

			this.World.Camera.Position.Acceleration = new Vector3(0,0,1);
            this.World.Camera.Position.MaxVelocity = new Vector3(0, 0, 1);
            var maxValue = new Vector3(500, 500, 2);
            this.World.Camera.Position.MaxValue = maxValue;

			this.World.Controllers.Add (new CharacterController (hero));

            this.World.Background.Value = Color.CornflowerBlue;

			base.Initialize ();
		}




	}
}
