using Microsoft.Xna.Framework;
using MonoGame.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Sparkle.Engine.WindowsPhone
{
    public class SparklePage<T> : SwapChainBackgroundPanel where T : SparkleGame, new()
    {
        protected T Game { get; private set;}

        public SparklePage(string launchArguments)
        {
            this.Game = XamlGame<T>.Create(launchArguments, Window.Current.CoreWindow, this);
        }
    }
}
