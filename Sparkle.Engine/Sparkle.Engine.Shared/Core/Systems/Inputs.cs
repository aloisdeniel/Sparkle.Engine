using Microsoft.Xna.Framework.Input;
using Sparkle.Engine.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sparkle.Engine.Core.Components;
using System.Diagnostics;

namespace Sparkle.Engine.Core.Systems
{
    public class Inputs : System, Base.IUpdateable
    {
        public Inputs(SparkleGame game)
            : base(game)
        {
            keyboard = new List<Input.KeyboardState>();
        }


        public void Update(Microsoft.Xna.Framework.GameTime time)
        {
            this.UpdateKeyboard();
        }

        #region Keyboard

        private List<Input.KeyboardState> keyboard;

        public void UpdateKeyboard()
        {
            // 1. Updating internal key states

            var keyboard = Keyboard.GetState().GetPressedKeys();

            var inputs = keyboard.Select((key) => {
                var prec = this.keyboard.FirstOrDefault((input) => input.Key == key);
                return new Input.KeyboardState(){
                    Key = key,
                    Trigger = (prec == null) ? Trigger.Started : Trigger.Active,
                };
            }).ToList();
            
            foreach (var item in this.keyboard.Where((i) => null == inputs.FirstOrDefault((i2) => i.Key == i2.Key)))
            {
                if (item.Trigger == Trigger.Started || item.Trigger == Trigger.Active)
                {
                    item.Trigger = Trigger.Stopped;
                    inputs.Add(item);
                }
            }

            this.keyboard = inputs;

            // 2. Updating each Input component

            var components = this.Game.Scene.GetComponents<Input>();

            foreach (var component in components)
            {
                foreach (var input in component.Commands.OfType<Input.KeyboardState>())
                {
                    var keyboardInput = this.keyboard.FirstOrDefault((i) => i.Key == input.Key);
                    input.Trigger = keyboardInput == null ? Trigger.Inactive : keyboardInput.Trigger;
                }
            }
        }

        #endregion
    }
}
