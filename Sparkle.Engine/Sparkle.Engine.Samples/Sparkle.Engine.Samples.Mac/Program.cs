using System;
using System.Collections.Generic;
using System.Linq;

using MonoMac.AppKit;
using MonoMac.Foundation;
using Sparkle.Engine.Mac;

namespace Sparkle.Engine.Samples.Mac
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main (string[] args)
		{
			NSApplication.Init ();

			using (var p = new NSAutoreleasePool ()) {
				NSApplication.SharedApplication.Delegate = new SparkleApplication (new Game1 ());

				// Set our Application Icon
				//NSImage appIcon = NSImage.ImageNamed ("Icon.png");
				//NSApplication.SharedApplication.ApplicationIconImage = appIcon;

				NSApplication.Main (args);
			}

		}
	}


}