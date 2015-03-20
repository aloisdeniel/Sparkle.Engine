using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Base.Dynamics
{
    public class DynamicAnimation2 : UpdatableBase, IDynamicAnimation<Vector2>
    {
        public DynamicAnimation2(Vector2 start, Vector2 end, double durationMs)
        {
            this.time = TimeSpan.Zero;
            this.Duration = TimeSpan.FromMilliseconds(durationMs);
            this.StartValue = start;
            this.Value = start;
            this.EndValue = end;
            this.Curve = Base.Curve.Mode.EaseInOut;
        }

        public Vector2 StartValue { get; private set; }

        public Vector2 Value { get; private set; }

        public Vector2 EndValue { get; private set; }

        public bool IsFinished { get; private set; }

        public bool IsStarted { get; private set; }

        public Curve.Mode Curve { get; set; }

        public TimeSpan Duration { get; set; }

        public RepeatMode Repeat { get; set; }

        private TimeSpan time;

        public void Start(RepeatMode repeat)
        {
            if (!this.IsStarted)
            {
                this.Repeat = repeat;
                this.time = TimeSpan.Zero;
                this.IsStarted = true;
                this.Value = this.StartValue;
            }
        }

        public void Stop()
        {
            if (this.IsStarted)
            {
                this.IsStarted = false;
                this.IsFinished = true;
            }
        }

        protected override void DoUpdate(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (this.IsStarted)
            {
                this.time += gameTime.ElapsedGameTime;

                var amount = this.time.TotalMilliseconds / this.Duration.TotalMilliseconds;
                var localamount = amount;

                if (this.Repeat == RepeatMode.Loop)
                {
                    localamount = amount.Clamp(0, 1);
                }
                else if (this.Repeat == RepeatMode.Reverse)
                {
                    localamount = 1 - amount.Clamp(0, 1);
                }
                else if ((this.Repeat == RepeatMode.OnceWithReverse || this.Repeat == RepeatMode.LoopWithReverse))
                {
                    localamount %= 2;
                    if (localamount > 1)
                        localamount = (2 - localamount).Clamp(0, 1);
                }

                this.Value = this.StartValue + Sparkle.Engine.Base.Curve.Calculate(this.Curve, (float)localamount) * (this.EndValue - this.StartValue);

                if (((this.Repeat == RepeatMode.Once || this.Repeat == RepeatMode.Reverse) && amount >= 1) || (this.Repeat == RepeatMode.OnceWithReverse && amount >= 2))
                {
                    this.Stop();
                }
            }
        }
    }
}
