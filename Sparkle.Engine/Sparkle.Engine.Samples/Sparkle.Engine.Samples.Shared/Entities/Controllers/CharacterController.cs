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

            this.AddCommand(Command.Relay.OnActive ("Left", left, this.MoveLeft ));
            this.AddCommand(Command.Relay.OnActive("Right", right, this.MoveRight));
            this.AddCommand(Command.Relay.OnActive("Up", up, this.MoveUp));
            this.AddCommand(Command.Relay.OnActive("Down", down, this.MoveDown));
        }

        protected override void DoUpdate(GameTime gameTime)
        {
            this.Entity.MovingDirection = Vector3.Zero;

            base.DoUpdate(gameTime);
        }

        public Character Entity { get; set; }
        
        public void MoveLeft()
        {
            var dir = this.Entity.MovingDirection;
            dir.X -= 1;
            this.Entity.MovingDirection = dir;
        }

        public void MoveUp()
        {
            var dir = this.Entity.MovingDirection;
            dir.Y -= 1;
            this.Entity.MovingDirection = dir;
        }

        public void MoveRight()
        {
            var dir = this.Entity.MovingDirection;
            dir.X += 1;
            this.Entity.MovingDirection = dir;
        }

        public void MoveDown()
        {
            var dir = this.Entity.MovingDirection;
            dir.Y += 1;
            this.Entity.MovingDirection = dir;
        }
    }
}
