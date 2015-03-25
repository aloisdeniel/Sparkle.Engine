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
    using Sparkle.Engine.Core.Entities;
    using Sparkle.Engine.Core.Tiles;
    using Sparkle.Engine.Samples.Shared.Identifiers;

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : SparkleGame
	{

		public Game1 ()
		{
			Graphics = new GraphicsDeviceManager (this);
            Graphics.IsFullScreen = true;
			Content.RootDirectory = "Content";
		}

        public Character Hero { get; private set; }

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			var screen = new Size (this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height);

            // 1. Creating World

            this.World = new World(10240, 10240, screen);
            this.World.IsDebugging = true;
            this.World.Background.Value = Color.CornflowerBlue;

            // 2. Setting up tiles

            var sheet = new TileSheet("tileset", 128 / 8, 128 / 8);
            sheet.RegisterTile(Id.Tile.Tile1, 0, 0);
            sheet.RegisterTile(Id.Tile.Tile2, 1, 0);
            sheet.RegisterTile(Id.Tile.Tile3, 4, 0);

            var layer = this.World.Tiles.CreateLayer(sheet);

            layer.AddTile(Id.Tile.Tile1, 1, 1);
            layer.AddTile(Id.Tile.Tile1, 1, 2);
            layer.AddTile(Id.Tile.Tile1, 2, 3);
            layer.AddTile(Id.Tile.Tile3, 3, 3);
            layer.AddTile(Id.Tile.Tile2, 4, 4, 3, 3);
            layer.AddTile(Id.Tile.Tile3, 20, 20, 3, 3);

            // 2. Creating entity factories

			this.World.RegisterEntity<GreenGuy> (Id.Entities.GreenGuy);
            this.World.RegisterEntity<OrangeGuy>(Id.Entities.OrangeGuy);
            this.World.RegisterEntity(Id.Entities.Vader, () => new Character("darthvader", this.World));
            this.World.RegisterEntity(Id.Entities.StormTrooper, () =>
            {
                var character = new Character("stormtrooper", this.World)
                {
                    Speed = 1500,
                };

                this.World.Controllers.Add(new FollowingNpcController(character, this.Hero));

                return character;
            });

            // 3. Spawning various entities

            var center = this.World.Bounds.Center;

            this.Hero = this.World.SpawnEntity(Id.Entities.Vader, center.X, center.Y) as Character;

            this.World.SpawnEntity(Id.Entities.GreenGuy, center.X - 150, center.Y - 150);
            this.World.SpawnEntity(Id.Entities.OrangeGuy, center.X - 200, center.Y - 150);

            const int number = 5;
            const float interval = 100;
            for (int i = 0; i < number; i++)
            {
                this.World.SpawnEntity(Id.Entities.StormTrooper, center.X - ((number * interval) / 2) + i * interval, center.Y + 250);
            }
            
            // 4. Setting up controllers

            this.World.Controllers.Add(new CharacterController(this.Hero));
            this.World.Controllers.Add(new CameraController(this.World.Camera, this.Hero));

			base.Initialize ();
		}
	}
}
