using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Base.Triggers
{
    public class TimeTrigger : UpdatableBase, ITrigger
    {
        public TimeTrigger(TimeSpan span)
        {
            this.ElapsedTime = 0;
            this.Span = span;
            this.State = TriggerState.Inactive;
        }

        public double ElapsedTime { get; private set; }

        public TimeSpan Span { get; private set; }

        public TriggerState State { get; private set; }

        protected override void DoUpdate(GameTime gameTime)
        {
            if(this.State != TriggerState.Active)
            {
                this.ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

                if (ElapsedTime >= this.Span.TotalMilliseconds)
                {
                    if (this.State == TriggerState.Inactive)
                        this.State = TriggerState.Started;
                    else if (this.State == TriggerState.Started)
                        this.State = TriggerState.Active;
                }
            }
        }
    }
}
