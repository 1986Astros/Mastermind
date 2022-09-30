using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind
{
    public class Globals
    {
        public static bool RightHanded = true;
        public static bool BottomToTop = true;
        public static Cradle.Orientations CradleOrientation = Cradle.Orientations.Vertical;
        public static int MaxTurns = 8;
        public static List<Color> ColorsInUse = new List<Color>() { Color.White, Color.Black, Color.Red, Color.Blue, Color.Yellow, Color.Green };
        public static int PegsPerRow = 4;
        public static int GamePegDiameter = 20;
        public static int ResultsPegDiameter = 8;
        public static CurrentGame CurrentGame = new CurrentGame();
        public static Random rnd = new Random();
        public static bool CheatMode = false;
    }
}
