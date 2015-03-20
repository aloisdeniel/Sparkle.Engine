namespace Sparkle.Engine.Base
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Base implementation of an updated object.
    /// </summary>
	public abstract class UpdatableBase : IUpdateable
	{
		public UpdatableBase ()
		{
			this.Enabled = true;
		}

        #region Fields

        private bool enabled;

        private int updateOrder;

        #endregion

        #region Properties

        public bool Enabled {
			get { return enabled; }
			set { 
				if (enabled != value) {
					enabled = value;
					if (this.EnabledChanged != null)
						this.EnabledChanged (this, EventArgs.Empty);
				}
			}
		}

		public int UpdateOrder {
			get { return updateOrder; }
			set {
				if (updateOrder != value) {
					updateOrder = value;
					if (this.UpdateOrderChanged != null)
						this.UpdateOrderChanged (this, EventArgs.Empty);
				}
			}
		}

        #endregion

        #region Events

        public event EventHandler<EventArgs> EnabledChanged;

		public event EventHandler<EventArgs> UpdateOrderChanged;

        #endregion

        #region Methods

        public void Update (GameTime gameTime)
		{
			if (this.Enabled) {
				this.DoUpdate (gameTime);
			}
		}

		protected abstract void DoUpdate (GameTime gameTime);

        #endregion
    }
}
