using Microsoft.Xna.Framework;
using Sparkle.Engine.Base.Triggers;
using Sparkle.Engine.Core;
using Sparkle.Engine.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Samples.Shared.Entities.Controllers
{
    public class CameraController : Controller
    {
        public enum CameraMode
        {
            Fixed,
            Following,
        }

        public CameraController(Camera camera, Character observed)
        {
            this.Mode = CameraMode.Following;
            this.Observed = observed;
            this.Camera = camera;

            var cam = this.Camera.Position.Value;
            cam.X = observed.X;
            cam.Y = observed.Y;
            this.Camera.Position.Value = cam;
            this.Camera.Position.Friction = new Microsoft.Xna.Framework.Vector3(1, 1, 1) * (0.14f);

            var distance = new DistanceTrigger(this.Camera, this.Observed, 30,120);

            this.AddCommand(Command.Relay.OnInactive("Follow",distance,ObservedIsFar));
        }

        protected override void DoUpdate(GameTime gameTime)
        {
            this.Camera.Position.Acceleration = Vector3.Zero;

            base.DoUpdate(gameTime);
        }

        public CameraMode Mode { get; set; }

        public Camera Camera { get; set; }

        public Character Observed { get; set; }

        
        public void ObservedIsFar()
        {
            if(this.Mode == CameraMode.Following)
            {
                var direction = (this.Observed.Position.Value - this.Camera.Position.Value);
                direction.Z = 0;
                direction.Normalize();

                this.Camera.Position.Acceleration = direction * Observed.Speed;
            }
        }
    }
}
