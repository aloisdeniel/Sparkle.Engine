using Sparkle.Engine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sparkle.Engine.Base.Triggers
{
    public class DistanceTrigger : ITrigger
    {
        public DistanceTrigger(Entity entity,Entity other ,double distance)
        {
            this.Entity = entity;
            this.Other = other;
            this.Distance = distance;
        }

        public double Distance { get; set; }

        public Entity Entity { get; private set; }

        public Entity Other { get; private set; }

        private double? lastDistance;

        public TriggerState State
        {
            get {

                var distance = (Other.Position.Value - Entity.Position.Value).Length();

                TriggerState result = TriggerState.Inactive;

                if (distance < this.Distance && ( lastDistance == null || (double)lastDistance > this.Distance ) )
                    result = TriggerState.Started;
                else if (distance > this.Distance && (lastDistance != null && (double)lastDistance < this.Distance))
                    result = TriggerState.Stopped;
                else if (distance < this.Distance)
                    result = TriggerState.Active;

                lastDistance = distance;

                return result;

            
            }
        }
    }
}
