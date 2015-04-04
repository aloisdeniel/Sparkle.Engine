using Sparkle.Engine.Base.Animation;
using Sparkle.Engine.Core.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Samples.Shared.Components
{
    public class MovingSprite : Behavior
    {
        public const string WalkUpAnim = "Walk-Up";

        public const string WalkDownAnim = "Walk-Down";

        public const string WalkLeftAnim = "Walk-Left";

        public const string WalkRightAnim = "Walk-Right";

        public override void Update(Microsoft.Xna.Framework.GameTime time)
        {
            base.Update(time);

            var body = this.Owner.GetComponent<Body>();
            var animation = this.Owner.GetComponent<SpriteAnimation>();

            if (body != null && animation != null)
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
                        animation.Play(WalkLeftAnim, Repeat.Mode.Loop);
                    }
                    else if (body.Velocity.X > 0)
                    {
                        animation.Play(WalkRightAnim, Repeat.Mode.Loop);
                    }
                }
                else
                {
                    if (body.Velocity.Y < 0)
                    {
                        animation.Play(WalkUpAnim, Repeat.Mode.Loop);
                    }
                    else if (body.Velocity.Y > 0)
                    {
                        animation.Play(WalkDownAnim, Repeat.Mode.Loop);
                    }
                }
            }
        }

    }
}
