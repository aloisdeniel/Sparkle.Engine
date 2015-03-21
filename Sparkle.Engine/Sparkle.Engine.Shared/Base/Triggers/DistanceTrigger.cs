using Sparkle.Engine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sparkle.Engine.Base.Triggers
{
    public class DistanceTrigger : ITrigger
    {
        public DistanceTrigger(Entity entity,Entity other ,double min) : this(entity,other,min, min)
        {

        }

        public DistanceTrigger(Entity entity, Entity other, double min, double max)
        {
            this.Entity = entity;
            this.Other = other;
            this.MinDistance = min;
            this.MaxDistance = max;
        }

        /// <summary>
        /// Becomes inactive only when other entity's distance from entity is bigger than this value.
        /// </summary>
        public double MaxDistance { get; set; }

        /// <summary>
        /// Becomes active only when other entity's distance from entity is less than this value.
        /// </summary>
        public double MinDistance { get; set; }

        public Entity Entity { get; private set; }

        public Entity Other { get; private set; }

        private TriggerState? lastState;

        public TriggerState State
        {
            get {

                var distance = (Other.Position.Value - Entity.Position.Value).Length();

                TriggerState result = TriggerState.Inactive;

                if (distance < this.MinDistance && ( lastState == null || lastState == TriggerState.Inactive ) )
                    result = TriggerState.Started;
                else if (distance > this.MaxDistance && (lastState == null || lastState == TriggerState.Active))
                    result = TriggerState.Stopped;
                else if (lastState != null && lastState == TriggerState.Started)
                    result = TriggerState.Active;
                else if (lastState != null && lastState == TriggerState.Stopped)
                    result = TriggerState.Inactive;
                else if (lastState != null)
                    result = (TriggerState)lastState;

                lastState = result;

                return result;

            
            }
        }
    }
}
