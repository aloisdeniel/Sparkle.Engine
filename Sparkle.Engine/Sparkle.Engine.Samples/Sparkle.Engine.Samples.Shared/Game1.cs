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
    using Sparkle.Engine.Core.Entities;

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

            var transforms = new Transforms();
            var anim = transforms.Add("square");
            anim.AddPosition(TimeSpan.FromSeconds(0), new Vector3(-100, -100, 0));
            anim.AddPosition(TimeSpan.FromSeconds(1), new Vector3(100, -100, 0), Base.Animation.Curve.Mode.EaseIn);
            anim.AddPosition(TimeSpan.FromSeconds(2), new Vector3(100, 100, 0), Base.Animation.Curve.Mode.EaseOut);
            anim.AddPosition(TimeSpan.FromSeconds(3), new Vector3(-100, 100, 0), Base.Animation.Curve.Mode.EaseInOut);
            anim.AddRotation(TimeSpan.FromSeconds(0), 0);
            anim.AddRotation(TimeSpan.FromSeconds(3), 6.28f, Base.Animation.Curve.Mode.EaseInOut);
            anim.AddColor(TimeSpan.FromSeconds(0), Color.White);
            anim.AddColor(TimeSpan.FromSeconds(3), Color.Red, Base.Animation.Curve.Mode.EaseInOut);

            anim = transforms.Add("small");
            anim.AddPosition(TimeSpan.FromSeconds(0), new Vector3(-50, 0, 0));
            anim.AddPosition(TimeSpan.FromSeconds(1), new Vector3(50, 0, 0), Base.Animation.Curve.Mode.EaseInOut);
            anim.AddScale(TimeSpan.FromSeconds(0), new Vector3(0.2f, 0.2f, 0.2f));
            anim.AddScale(TimeSpan.FromSeconds(1), new Vector3(0.5f, 0.5f, 0.5f), Base.Animation.Curve.Mode.EaseInOut);
            anim.AddRotation(TimeSpan.FromSeconds(0), 0);
            anim.AddRotation(TimeSpan.FromSeconds(1), 2 * 6.28f, Base.Animation.Curve.Mode.EaseInOut);
            anim.AddColor(TimeSpan.FromSeconds(0), Color.White);
            anim.AddColor(TimeSpan.FromSeconds(1), Color.Blue, Base.Animation.Curve.Mode.EaseInOut);

            // Vader
            vader = new Character(vaderSprite, 10, 10);
            var inputs = vader.AddComponent<Input>();
            inputs.ObserveKey(InputMovement.UpCommand, Keys.Up);
            inputs.ObserveKey(InputMovement.DownCommand, Keys.Down);
            inputs.ObserveKey(InputMovement.RightCommand, Keys.Right);
            inputs.ObserveKey(InputMovement.LeftCommand, Keys.Left);
            vader.AddComponent<InputMovement>();
            this.Scene.EntityManager.AddEntity(vader);

            //Emitter test
            emitter = vader.AddComponent<ParticleEmitter>();
            emitter.Sprite = vaderSprite;
            emitter.MinLifetime = TimeSpan.FromMilliseconds(2000);
            emitter.MaxLifetime = TimeSpan.FromMilliseconds(5000);
            emitter.SourceArea = new Rectangle(0, 0, 32, 48);
            emitter.StartColor = Color.Black;
            emitter.EndColor = new Color(Color.Red,0f);
            emitter.MinEndScale = 0.5f;
            emitter.MaxEndScale = 0.8f;
            emitter.MinAcceleration = new Vector3(0, -0.000010f, 0);
            emitter.MaxAcceleration = new Vector3(0, -0.000025f, 0); 
            emitter.StartArea = new Base.Geometry.Frame(0, 0, 0, 0);
            emitter.EndArea = new Base.Geometry.Frame(0, -30, 50, 50);

            //Transform animation
            var test = new Character(vaderSprite, 10, 10);
            var tanim = test.AddComponent<TransformAnimation>();
            tanim.Sheet = transforms;
            tanim.Play("square",Base.Animation.Repeat.Mode.LoopWithReverse);
            this.Scene.EntityManager.AddEntity(test);
            
            //Relative Transform animation
            var test2 = new Character(stormtrooperSprite, 0, 0);
            var transform = test2.GetComponent<Transform>();
            transform.Parent = test.GetComponent<Transform>();
            tanim = test2.AddComponent<TransformAnimation>();
            tanim.Sheet = transforms;
            tanim.Play("small",Base.Animation.Repeat.Mode.LoopWithReverse);
            this.Scene.EntityManager.AddEntity(test2);

            //Test parralax
            var test3 = new Entity();
            var renderer = test3.AddComponent<SpriteRenderer>();
            renderer.Sprite = stormtrooperSprite;
            renderer.Width = 32;
            renderer.Height = 48;
            var animation = test3.AddComponent<SpriteAnimation>();
            animation.Sheet = stormtrooperSprite;
            animation.Interval = 200;
            var body = test3.AddComponent<Transform>();
            body.Position = new Microsoft.Xna.Framework.Vector3(100, 100, 0);
            body.Scale = new Vector3(2, 2, 1);
            test3.AddComponent<Parralax>();
            this.Scene.EntityManager.AddEntity(test3);
            test3 = new Entity();
            renderer = test3.AddComponent<SpriteRenderer>();
            renderer.Sprite = stormtrooperSprite;
            renderer.Width = 32;
            renderer.Height = 48;
            animation = test3.AddComponent<SpriteAnimation>();
            animation.Sheet = stormtrooperSprite;
            animation.Interval = 200;
            body = test3.AddComponent<Transform>();
            body.Color = Color.Red;
            body.Position = new Microsoft.Xna.Framework.Vector3(100, 100, -3);
            body.Scale = new Vector3(2, 2, 1);
            var parralax = test3.AddComponent<Parralax>();
            //parralax.Power = Vector2.UnitX;
            parralax.Scaling = 8f;
            this.Scene.EntityManager.AddEntity(test3);


            var cam = this.Scene.Camera.AddComponent<FollowingCamera>();
            cam.Target = vader.GetComponent<Body>();
            
        }
        private ParticleEmitter emitter;
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

            if(ms > 5000)
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

            this.emitter.Emit(25);

            return stormtrooper;
        }

    }
}