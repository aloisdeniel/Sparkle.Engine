using Microsoft.Xna.Framework;
using Sparkle.Engine.Base.Triggers;
using Sparkle.Engine.Core.Controllers;
using Sparkle.Engine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Samples.Shared.Entities.Controllers
{
    public class CharacterController : Controller
    {
        public CharacterController(Character entity)
        {
            this.Entity = entity;

            var left = new KeyboardTrigger(Microsoft.Xna.Framework.Input.Keys.Left);
            var right = new KeyboardTrigger(Microsoft.Xna.Framework.Input.Keys.Right);
            var up = new KeyboardTrigger(Microsoft.Xna.Framework.Input.Keys.Up);
            var down = new KeyboardTrigger(Microsoft.Xna.Framework.Input.Keys.Down);

            this.AddCommand(Command.Relay.OnActive ("Left", left, this.Entity.MoveLeft ));
            this.AddCommand(Command.Relay.OnActive("Right", right, this.Entity.MoveRight));
            this.AddCommand(Command.Relay.OnActive("Up", up, this.Entity.MoveUp));
            this.AddCommand(Command.Relay.OnActive("Down", down, this.Entity.MoveDown));
        }

        protected override void DoUpdate(GameTime gameTime)
        {
            this.Entity.Position.Acceleration = Vector3.Zero;

            base.DoUpdate(gameTime);
        }

        public Character Entity { get; set; }
    }
}
