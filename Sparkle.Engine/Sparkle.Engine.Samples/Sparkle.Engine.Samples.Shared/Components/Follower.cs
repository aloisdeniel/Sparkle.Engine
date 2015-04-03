using Microsoft.Xna.Framework;
using Sparkle.Engine.Core.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Samples.Shared.Components
{
    public class Follower : Behavior
    {
        public Follower()
        {
            this.Speed = 50.0f;
        }

        public float Speed { get; set; }

        public Body Target { get; set; }

        public override void Update(Microsoft.Xna.Framework.GameTime time)
        {
            base.Update(time);

            if(Target != null)
            {
                var body = this.Owner.GetComponent<Body>();

                if (body != null)
                {
                    var dir = (this.Target.Position - body.Position);

                    if (dir != Vector3.Zero)
                    {
                        dir.Normalize();
                        body.Velocity = dir * this.Speed;
                    }
                    else
                    {
                        body.Velocity = Vector3.Zero;
                    }
                }
            }
        }
    }
}
