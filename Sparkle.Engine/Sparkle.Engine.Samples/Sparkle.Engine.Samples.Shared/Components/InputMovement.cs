using Microsoft.Xna.Framework;
using Sparkle.Engine.Base.Animation;
using Sparkle.Engine.Core.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Samples.Shared.Components
{
    public class InputMovement : Behavior
    {
        public const string UpCommand = "Up";

        public const string DownCommand = "Down";

        public const string LeftCommand = "Left";

        public const string RightCommand = "Right";

        public float Speed { get; set; }

        public InputMovement()
        {
            this.Speed = 100.0f;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime time)
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

    }
}
