using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sparkle.Engine.Base.Dynamics
{
    public class DynamicAnimationComposite<T> : UpdatableBase, IDynamicAnimation<T>
    {

        public DynamicAnimationComposite()
        {
            this.Children = new List<IDynamicAnimation<T>>();
        }

        public List<IDynamicAnimation<T>> Children { get; private set; }

        public void  AddAnimation(IDynamicAnimation<T> animation)
        {
            this.Children.Add(animation);
        }

        private int currentAnimationIndex;

        public T StartValue
        {
            get {
                if (this.Children.Count > 0)
                    return this.Children[0].StartValue;

                return default(T);
            }
        }

        public T EndValue
        {
            get
            {
                if (this.Children.Count > 0)
                    return this.Children[0].StartValue;

                return default(T);
            }
        }

        public T Value
        {
            get
            {
                if (this.Children.Count > 0)
                    return this.Children[currentAnimationIndex].Value;

                return default(T);
            }
        }

        public bool IsFinished
        {
            get;
            private set;
        }

        public bool IsStarted
        {
            get;
            private set;
        }

        public Curve.Mode Curve
        {
            get;
            private set;
        }

        public TimeSpan Duration
        {
            get
            {
                return TimeSpan.FromMilliseconds(this.Children.Sum((a) => a.Duration.TotalMilliseconds));
            }
        }

        public RepeatMode Repeat
        {
            get;
            set;
        }

        public void Start(RepeatMode repeat)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        protected override void DoUpdate(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
