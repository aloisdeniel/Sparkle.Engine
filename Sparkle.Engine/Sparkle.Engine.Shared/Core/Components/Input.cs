namespace Sparkle.Engine.Core.Components
{
    using Microsoft.Xna.Framework.Input;
    using Sparkle.Engine.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A component that represents the set of inputs the entity needs to observe.
    /// </summary>
    public class Input : Component
    {
        public Input ()
        {
            this.Commands = new List<Command>();
        }

        #region Keyboard

        public class Command
        {
            public String Name { get; set; }

            public Trigger Trigger { get; set; }
        }

        public class KeyboardState : Command
        {
            public Keys Key { get; set; }
        }

        /// <summary>
        /// Gets all the state of observed keyboard keys.
        /// </summary>
        public List<Command> Commands { get; private set; }
        
        /// <summary>
        /// Adds a keyboard key to the set of observed keys.
        /// </summary>
        /// <param name="key"></param>
        public void ObserveKey(String command, Keys key)
        {
            var c = new KeyboardState()
            {
                Name = command,
                Key = key,
                Trigger = Trigger.Inactive,
            };
            this.Commands.Add(c);
        }

        /// <summary>
        /// Removes a keyboard key from the set of observed keys.
        /// </summary>
        /// <param name="key"></param>
        public void ResetCommand(String command)
        {
            var state = this.Commands.FirstOrDefault((s) => s.Name == command);

            if(state != null)
            {
                this.Commands.Remove(state);
            }
        }

        public Trigger GetState(string command)
        {
            var state = this.Commands.FirstOrDefault((s) => s.Name == command); // TODO : merge states when multiple command with same name

            if (state == null)
                return Trigger.Inactive;

            return state.Trigger;

        }

        #endregion
    }
}