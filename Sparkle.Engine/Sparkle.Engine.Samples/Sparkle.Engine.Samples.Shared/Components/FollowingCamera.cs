using Microsoft.Xna.Framework;
using Sparkle.Engine.Core.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Samples.Shared.Components
{
    public class FollowingCamera : Behavior
    {
        public FollowingCamera()
        {
            this.Speed = 1.0f;
        }

        public float Speed { get; set; }

        public Transform Target { get; set; }

        private bool isRefocusing;

        public override void Update(Microsoft.Xna.Framework.GameTime time)
        {
            base.Update(time);

            if(Target != null)
            {
                var transform = this.Owner.GetComponent<Transform>();

                if (transform != null)
                {
                    var dir = (this.Target.Position - transform.Position);
                    dir = new Vector3(dir.X, dir.Y, 0);

                    if (this.isRefocusing)
                    {
                        if (dir.Length() < 50)
                        {
                            this.isRefocusing = true;
                        }
                        else if (dir != Vector3.Zero)
                        {
                            dir.Normalize();
                            transform.Position += dir * this.Speed;
                        }
                    }
                    else if(dir.Length() > 120)
                    {
                        this.isRefocusing = true;
                    }
                }
            }
        }
    }
}
