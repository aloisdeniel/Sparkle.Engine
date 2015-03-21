using Microsoft.Xna.Framework;
using Sparkle.Engine.Base.Triggers;
using Sparkle.Engine.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sparkle.Engine.Samples.Shared.Entities.Controllers
{
    public class FollowingNpcController : Controller
    {
        public enum FollowingState
        {
            None,
            Following,
        }

        public FollowingNpcController(Character entity, Character observed)
        {
            this.Entity = entity;
            this.Observed = observed;

            var distance = new DistanceTrigger(entity, observed, 20);
            var distance2 = new DistanceTrigger(entity, observed, 50);

            this.AddCommand(Command.Relay.OnStart("StartObserving",distance,ObservedIsNear));
            this.AddCommand(Command.Relay.OnInactive("Follow",distance2,ObservedIsFar));
        }

        protected override void DoUpdate(GameTime gameTime)
        {
            this.Entity.Position.Acceleration = Vector3.Zero;

            base.DoUpdate(gameTime);
        }

        public FollowingState State { get; set; }

        public Character Entity { get; set; }

        public Character Observed { get; set; }

        public void ObservedIsNear()
        {
            this.State = FollowingState.Following;
            Debug.WriteLine("START");
        }

        public void ObservedIsFar()
        {
            if(this.State == FollowingState.Following)
            {
                this.Entity.Move(this.Observed.Position.Value - this.Entity.Position.Value);
            }
        }
    }
}
