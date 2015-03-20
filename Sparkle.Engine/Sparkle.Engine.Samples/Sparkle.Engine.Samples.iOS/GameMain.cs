using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Microsoft.Xna.Framework;


namespace Sparkle.Engine.Samples.iOS
{
	[Register ("AppDelegate")]
	class Program : UIApplicationDelegate
	{
		private Game1 game;

		public override void FinishedLaunching (UIApplication app)
		{
			// Fun begins..
			game = new Game1 ();
			game.Graphics.IsFullScreen = true;
			game.Graphics.ApplyChanges ();
			game.Graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;

			game.Run ();
		}

		static void Main (string[] args)
		{
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
