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

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : SparkleGame
    {
        public Game1 ()
        {
            Content.RootDirectory = "Content";

            // Vader
            var vader = new Character("darthvader", 10, 10);
            var inputs = vader.AddComponent<Input>();
            inputs.ObserveKey(InputMovement.UpCommand, Keys.Up);
            inputs.ObserveKey(InputMovement.DownCommand, Keys.Down);
            inputs.ObserveKey(InputMovement.RightCommand, Keys.Right);
            inputs.ObserveKey(InputMovement.LeftCommand, Keys.Left);
            vader.AddComponent<InputMovement>();
            this.Scene.AddEntity(vader);

            // Stormtrooper
            var stormtrooper = new Character("stormtrooper", 100, 100);
            var npc = stormtrooper.AddComponent<Follower>();
            npc.Target = vader.GetComponent<Body>();
            this.Scene.AddEntity(stormtrooper);

        }

    }
}