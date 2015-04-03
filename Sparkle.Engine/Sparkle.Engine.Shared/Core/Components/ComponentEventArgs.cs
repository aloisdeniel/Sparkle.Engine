using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Components
{
    public class ComponentEventArgs : EventArgs
    {
        public Component Component { get; set; }
    }
}
