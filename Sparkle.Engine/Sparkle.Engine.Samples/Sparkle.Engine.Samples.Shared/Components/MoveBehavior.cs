using Microsoft.Xna.Framework;
using Sparkle.Engine.Base.Animation;
using Sparkle.Engine.Core.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Samples.Shared.Components
{
    public class MoveBehavior : Behavior
    {
        public const string UpCommand = "Up";

        public const string DownCommand = "Down";

        public const string LeftCommand = "Left";

        public const string RightCommand = "Right";

        public const string WalkUpAnim = "Walk-Up";

        public const string WalkDownAnim = "Walk-Down";

        public const string WalkLeftAnim = "Walk-Left";

        public const string WalkRightAnim = "Walk-Right";

        public float Speed { get; set; }

        public MoveBehavior()
        {
            this.Speed = 100.0f;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime time)
        {
            this.updateVelocity();

            this.updateSpriteAnimation();
        }

        private void updateVelocity()
        {
            var inputs = this.Owner.GetComponent<Input>();
            var body = this.Owner.GetComponent<Body>();

            if (inputs != null && body != null)
            {
                var dir = Vector2.Zero;

                if (inputs.GetState(UpCommand) == Base.Trigger.Active)
                {
                    dir.Y--;
                }
                if (inputs.GetState(DownCommand) == Base.Trigger.Active)
                {
                    dir.Y++;
                }
                if (inputs.GetState(LeftCommand) == Base.Trigger.Active)
                {
                    dir.X--;
                }
                if (inputs.GetState(RightCommand) == Base.Trigger.Active)
                {
                    dir.X++;
                }

                if (dir != Vector2.Zero)
                {
                    dir.Normalize();
                    body.Velocity = new Vector3(dir * this.Speed, 0);
                }
                else
                {
                    body.Velocity = Vector3.Zero;
                }
            }
        }

        private void updateSpriteAnimation()
        {
            var body = this.Owner.GetComponent<Body>();
            var animation = this.Owner.GetComponent<SpriteAnimation>();

            if(body != null && animation != null)
            {
                const double minVelocity = 0.30;

                var absX = Math.Abs(body.Velocity.X);
                var absY = Math.Abs(body.Velocity.Y);

                if (absX < minVelocity && absY < minVelocity)
                {
                    animation.Stop();
                }
                else if (absX > absY)
                {
                    if (body.Velocity.X < 0)
                    {
                        animation.Play(WalkLeftAnim, RepeatMode.Loop);
                    }
                    else if (body.Velocity.X > 0)
                    {
                        animation.Play(WalkRightAnim, RepeatMode.Loop);
                    }
                }
                else
                {
                    if (body.Velocity.Y < 0)
                    {
                        animation.Play(WalkUpAnim, RepeatMode.Loop);
                    }
                    else if (body.Velocity.Y > 0)
                    {
                        animation.Play(WalkDownAnim, RepeatMode.Loop);
                    }
                }
            }
        }
    }
}
