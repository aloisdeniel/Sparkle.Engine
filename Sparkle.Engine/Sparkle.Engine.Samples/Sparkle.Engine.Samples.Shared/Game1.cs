namespace Sparkle.Engine.Samples
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Sparkle.Engine.Core;
    using Sparkle.Engine.Base;
    using Sparkle.Engine.Samples.Shared.Entities;
    using Sparkle.Engine.Samples.Shared.Components;
    using Sparkle.Engine.Core.Components;
    using Microsoft.Xna.Framework.Input;
    using Sparkle.Engine.Core.Resources;
    using System;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : SparkleGame
    {
        public Game1 ()
        {
            Content.RootDirectory = "Content";

            //Resources
            var vaderSprite = this.CreateCharactersheet("darthvader");
            this.stormtrooperSprite = this.CreateCharactersheet("stormtrooper");

            // Vader
            vader = new Character(vaderSprite, 10, 10);
            var inputs = vader.AddComponent<Input>();
            inputs.ObserveKey(InputMovement.UpCommand, Keys.Up);
            inputs.ObserveKey(InputMovement.DownCommand, Keys.Down);
            inputs.ObserveKey(InputMovement.RightCommand, Keys.Right);
            inputs.ObserveKey(InputMovement.LeftCommand, Keys.Left);
            vader.AddComponent<InputMovement>();
            this.Scene.EntityManager.AddEntity(vader);

            var cam = this.Scene.Camera.AddComponent<FollowingCamera>();
            cam.Target = vader.GetComponent<Body>();
            
        }

        private double ms;
        private Random random = new Random();

        private Spritesheet CreateCharactersheet(string texture)
        {
            var sprite = new Spritesheet(texture);
            sprite.Add(MovingSprite.WalkDownAnim, 32, 48, new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0));
            sprite.Add(MovingSprite.WalkLeftAnim, 32, 48, new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(3, 1));
            sprite.Add(MovingSprite.WalkRightAnim, 32, 48, new Point(0, 2), new Point(1, 2), new Point(2, 2), new Point(3, 2));
            sprite.Add(MovingSprite.WalkUpAnim, 32, 48, new Point(0, 3), new Point(1, 3), new Point(2, 3), new Point(3, 3));
            this.Scene.ResourceManager.AddResource(sprite);

            return sprite;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            ms += gameTime.ElapsedGameTime.TotalMilliseconds;

            if(ms > 500)
            {
                ms = 0;
                this.SpawnStormtrooper(random.Next(-400, 400), random.Next(-400, 400));
            }
        }

        Character vader;

        Spritesheet stormtrooperSprite;

        private Character SpawnStormtrooper(int x, int y)
        {
            var stormtrooper = new Character(stormtrooperSprite, x, y);
            var npc = stormtrooper.AddComponent<Follower>();
            npc.Target = vader.GetComponent<Body>();
            this.Scene.EntityManager.AddEntity(stormtrooper);

            return stormtrooper;
        }

    }
}