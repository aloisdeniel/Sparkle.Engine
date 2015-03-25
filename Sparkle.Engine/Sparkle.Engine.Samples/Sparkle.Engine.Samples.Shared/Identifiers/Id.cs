using Sparkle.Engine.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Samples.Shared.Identifiers
{
    public static class Id
    {
        public static class Tile
        {
            public static int Tile1 = Identifier.Generate();
            public static int Tile2 = Identifier.Generate();
            public static int Tile3 = Identifier.Generate();
        }

        public static class Entities
        {
            public static int Vader = Identifier.Generate();
            public static int StormTrooper = Identifier.Generate();
            public static int OrangeGuy = Identifier.Generate();
            public static int GreenGuy = Identifier.Generate();
        }
    }
}
