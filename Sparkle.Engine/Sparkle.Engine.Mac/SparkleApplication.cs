using System;
using Microsoft.Xna.Framework;
using MonoMac.AppKit;

namespace Sparkle.Engine.Mac
{
	public class SparkleApplication : NSApplicationDelegate
	{
		Game game;

		public SparkleApplication (Game game)
		{
			this.game = game;
		}

		public override void FinishedLaunching (MonoMac.Foundation.NSObject notification)
		{
			game.Run ();
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
		{
			return true;
		}
	}
}

