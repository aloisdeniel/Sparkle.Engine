﻿using Microsoft.Xna.Framework;
using Sparkle.Engine.Base.Triggers;
using Sparkle.Engine.Core;
using Sparkle.Engine.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            this.Camera.Position.Friction = observed.Position.Friction;

            var distance = new DistanceTrigger(this.Camera, this.Observed, 30,120);
            var down = new KeyboardTrigger(Microsoft.Xna.Framework.Input.Keys.NumPad2);
            var up = new KeyboardTrigger(Microsoft.Xna.Framework.Input.Keys.NumPad8);
            var left = new KeyboardTrigger(Microsoft.Xna.Framework.Input.Keys.NumPad4);
            var right = new KeyboardTrigger(Microsoft.Xna.Framework.Input.Keys.NumPad6);

            this.AddCommand(Command.Relay.OnActiveAndInactive("Follow",distance,ObservedIsFar));
            this.AddCommand(Command.Relay.OnActive("Zoom +", down, this.ZoomMore));
            this.AddCommand(Command.Relay.OnActive("Zoom -", up, this.ZoomLess));
            this.AddCommand(Command.Relay.OnActive("Rotate left", left, this.RotateLeft));
            this.AddCommand(Command.Relay.OnActive("Rotate right", right, this.RotateRight));
        }

        protected override void DoUpdate(GameTime gameTime)
        {
            this.Camera.Rotation.Velocity = 0;
            this.Camera.Position.Acceleration = Vector3.Zero;

            base.DoUpdate(gameTime);
        }
        
        public void ZoomMore()
        {
            var vel = this.Camera.Position.Velocity;
            vel.Z = 0.1f;
            this.Camera.Position.Velocity = vel;
        }

        public void ZoomLess()
        {
            var vel = this.Camera.Position.Velocity;
            vel.Z = -0.1f;
            this.Camera.Position.Velocity = vel;
        }

        public void RotateLeft()
        {
            this.Camera.Rotation.Velocity = -0.1f;
        }

        public void RotateRight()
        {
            this.Camera.Rotation.Velocity = 0.1f;
        }

        public CameraMode Mode { get; set; }

        public Camera Camera { get; set; }

        public Character Observed { get; set; }

        
        public void ObservedIsFar(bool isNear)
        {
            if(this.Mode == CameraMode.Following)
            {
                if (!isNear)
                {
                    var direction = (this.Observed.Position.Value - this.Camera.Position.Value);
                    direction.Z = 0;
                    direction.Normalize();

                    this.Camera.Position.Acceleration = direction * Observed.Speed;
                }
                else
                {
                    this.Camera.Position.Stop();
                }
            }
        }
    }
}
