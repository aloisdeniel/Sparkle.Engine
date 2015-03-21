using Microsoft.Xna.Framework;
using Sparkle.Engine.Core;
using Sparkle.Engine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Samples.Shared.Entities
{
    public class Character : Entity
	{
        public Character(string sprite, World w)
            : base(w)
		{
            this.Sprite = this.CreateSprite(sprite, 128, 192);

            this.Speed = 1800;
            
            //Move animations
            this.Sprite.AddAnimation(AnimMoveUp, 4, 4).From(0, 3).To(3).WithInterval(250);
            this.Sprite.AddAnimation(AnimMoveDown, 4, 4).From(0, 0).To(3).WithInterval(250);
            this.Sprite.AddAnimation(AnimMoveLeft, 4, 4).From(0, 1).To(3).WithInterval(250);
            this.Sprite.AddAnimation(AnimMoveRight, 4, 4).From(0, 2).To(3).WithInterval(250);

            this.Position.Friction = new Microsoft.Xna.Framework.Vector3(1, 1, 1) * (0.14f);

		}

        public float Speed { get; set; }

        protected Sprite Sprite { get; set; }

        protected override void DoUpdate(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.DoUpdate(gameTime);

            //1. Updates Z order (for a top-down game)

            var position = this.Position.Value;
            position.Z = -this.Position.Value.Y;
            this.Position.Value = position;

            //2. Updates moving animation from velocity direction

            this.updateSpriteAnimation();
        }

        #region Sprite animation

        public const string AnimMoveUp = "MoveUp";

        public const string AnimMoveDown = "MoveDown";

        public const string AnimMoveLeft = "MoveLeft";

        public const string AnimMoveRight = "MoveRight";

        private void updateSpriteAnimation()
        {
            const double minVelocity = 0.30;

            var absX = Math.Abs(this.Position.Velocity.X);
            var absY = Math.Abs(this.Position.Velocity.Y);

            if (absX < minVelocity && absY < minVelocity)
            {
                this.Sprite.StopAnimation();
            }
            else if (absX > absY)
            {
                if (this.Position.Velocity.X < 0)
                {
                    this.Sprite.PlayAnimation(AnimMoveLeft, Base.RepeatMode.Loop);
                }
                else if (this.Position.Velocity.X > 0)
                {
                    this.Sprite.PlayAnimation(AnimMoveRight, Base.RepeatMode.Loop);
                }
            }
            else
            {
                if (this.Position.Velocity.Y < 0)
                {
                    this.Sprite.PlayAnimation(AnimMoveUp, Base.RepeatMode.Loop);
                }
                else if (this.Position.Velocity.Y > 0)
                {
                    this.Sprite.PlayAnimation(AnimMoveDown, Base.RepeatMode.Loop);
                }
            }
        }

        #endregion

        #region Moving the character

        public void Move(Vector3 direction)
        {
            direction.Normalize();

            this.Position.Acceleration = direction * Speed;
        }

        public void MoveLeft()
        {
            var acc = this.Position.Acceleration;
            acc.X -= Speed;
            this.Position.Acceleration = acc;
        }

        public void MoveUp()
        {
            var acc = this.Position.Acceleration;
            acc.Y -= Speed;
            this.Position.Acceleration = acc;
        }

        public void MoveRight()
        {
            var acc = this.Position.Acceleration;
            acc.X += Speed;
            this.Position.Acceleration = acc;
        }

        public void MoveDown()
        {
            var acc = this.Position.Acceleration;
            acc.Y += Speed;
            this.Position.Acceleration = acc;
        }

        #endregion

	}
}
