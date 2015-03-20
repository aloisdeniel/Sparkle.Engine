using Microsoft.Xna.Framework;
using Sparkle.Engine.Base.Triggers;
using Sparkle.Engine.Core.Controllers;
using Sparkle.Engine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Samples.Shared.Entities.Controllers
{
    public class GuyController : Controller
    {
        public GuyController(Entity entity)
        {
            this.Entity = entity;

            var left = new KeyboardTrigger(Microsoft.Xna.Framework.Input.Keys.Left);
            var right = new KeyboardTrigger(Microsoft.Xna.Framework.Input.Keys.Right);
            var up = new KeyboardTrigger(Microsoft.Xna.Framework.Input.Keys.Up);
            var down = new KeyboardTrigger(Microsoft.Xna.Framework.Input.Keys.Down);

            this.AddCommand(Command.Relay.OnActive ("Left", left, this.Left ));
            this.AddCommand(Command.Relay.OnActive("Right", right, this.Right));
            this.AddCommand(Command.Relay.OnActive("Up", up, this.Up));
            this.AddCommand(Command.Relay.OnActive("Down", down, this.Down));
        }

        protected override void DoUpdate(GameTime gameTime)
        {
            this.Entity.Position.Acceleration = Vector3.Zero;

            base.DoUpdate(gameTime);
        }

        private float speed = 1200.0f;

        public void Left()
        {
            var acc = this.Entity.Position.Acceleration;
            acc.X -= speed;
            this.Entity.Position.Acceleration = acc;
        }

        public void Up()
        {
            var acc = this.Entity.Position.Acceleration;
            acc.Y -= speed;
            this.Entity.Position.Acceleration = acc;
        }

        public void Right()
        {
            var acc = this.Entity.Position.Acceleration;
            acc.X += speed;
            this.Entity.Position.Acceleration = acc;

        }

        public void Down()
        {
            var acc = this.Entity.Position.Acceleration;
            acc.Y += speed;
            this.Entity.Position.Acceleration = acc;
        }


        public Entity Entity { get; set; }
    }
}
