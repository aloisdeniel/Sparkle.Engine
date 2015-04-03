namespace Sparkle.Engine.Core.Components
{
    using Microsoft.Xna.Framework.Input;
    using Sparkle.Engine.Base;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A component that represents the set of inputs the entity needs to observe.
    /// </summary>
    public class Input : Component
    {
        public Input ()
        {
            this.Keyboard = new List<KeyboardState>();
        }

        #region Keyboard

        public class KeyboardState
        {
            public Keys Key { get; set; }

            public Trigger Trigger { get; set; }
        }

        /// <summary>
        /// Gets all the state of observed keyboard keys.
        /// </summary>
        public List<KeyboardState> Keyboard { get; private set;}

        /// <summary>
        /// Adds a keyboard key to the set of observed keys.
        /// </summary>
        /// <param name="key"></param>
        public void ObserveKey(Keys key)
        {
            if (!this.Keyboard.Any((s) => s.Key == key))
            {
                this.Keyboard.Add(new KeyboardState()
                {
                    Key = key,
                    Trigger = Trigger.Inactive,
                });
            }
        }

        /// <summary>
        /// Removes a keyboard key from the set of observed keys.
        /// </summary>
        /// <param name="key"></param>
        public void UnobserveKey(Keys key)
        {
            var state = this.Keyboard.FirstOrDefault((s) => s.Key == key);

            if(state != null)
            {
                this.Keyboard.Remove(state);
            }
        }

        #endregion
    }
}