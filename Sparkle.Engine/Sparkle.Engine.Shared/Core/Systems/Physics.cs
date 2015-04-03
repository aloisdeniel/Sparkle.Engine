using Sparkle.Engine.Core.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Systems
{
    public class Physics : System, Base.IUpdateable
    {
        public void Update(Microsoft.Xna.Framework.GameTime time)
        {
            var components = this.Game.Scene.GetComponents<Body>();

            var dt = time.ElapsedGameTime.Milliseconds;

            foreach (var body in components)
            {
                body.Inertia += dt * body.Torques / body.Mass;
                body.Rotation += dt * body.Inertia;

                body.Velocity += dt * body.Forces / body.Mass;
                body.Position += dt * body.Velocity;
            }

            // TODO : update all Transform components from Body compoments
        }
    }
}
