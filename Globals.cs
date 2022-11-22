using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind
{
    public class Globals
    {
        private static bool Initialized = Initialize();

        public static int MaxTurns = 10;
        public static List<Color> ColorsInUse = new List<Color>() { Color.White, Color.Black, Color.Red, Color.Blue, Color.Yellow, Color.Green };
        public static int PegsPerRow = 4;
        public static int GamePegDiameter = 20;
        public static int ResultsPegDiameter = 8;
        public static CurrentGame CurrentGame = new CurrentGame();
        public static Random rnd = new Random();
        public static Records Records = Records.FromFile("MasterMindRecords");
        public static IEnumerable<string> LastPlayers
        {
            get
            {
                if (!LastPlayersHasBeenTested)
                {
                    List<string> Players = new List<string>(((string)(Registry.GetValue("General", "LastPlayers", Records.AllPlayers.First(pi => pi.ID>=0).PlayerName))).Split(',').Where(pn => Records.AllPlayers.Select(p => p.PlayerName).Contains(pn)));
                    if (Players.Count == 0)
                    {
                        Registry.SetValue("General", "LastPlayers", Records.AllPlayers.First(pi => pi.ID >= 0).PlayerName);
                    }
                    else
                    {
                        Registry.SetValue("General", "LastPlayers", Players.Aggregate<string, string>("", (ag, s) => $"{ag},{s}").TrimStart(','));
                    }
                    LastPlayersHasBeenTested = true;
                }
                return ((string)(Registry.GetValue("General", "LastPlayers", Records.AllPlayers.First(pi => pi.ID >= 0).PlayerName))).Split(',');
            }
            set
            {
                Registry.SetValue("General", "LastPlayers", value.Aggregate<string, string>("", (ag, s) => $"{ag},{s}").TrimStart(','));
            }
        }
        private static bool LastPlayersHasBeenTested = false;

        public static List<PlayerControl> NamePlates = new List<PlayerControl>();
        private static bool Initialize()
        {
            MemoryStream ms;
            ms = new MemoryStream(MasterMind.Properties.Resources.PegCopy);
            PegCopyCursor = new Cursor(ms);
            ms = new MemoryStream(MasterMind.Properties.Resources.PegMove);
            PegMoveCursor = new Cursor(ms);
            ms = new MemoryStream(MasterMind.Properties.Resources.PegSwap);
            PegSwapCursor = new Cursor(ms);

            return true;
        }
        public static Cursor PegCopyCursor;
        public static Cursor PegMoveCursor;
        public static Cursor PegSwapCursor;

        public static RegistryExtensions.RegistryEx Registry = new RegistryExtensions.RegistryEx(RegistryExtensions.RegistryEx.HKey.CurrentUser, "Shark In Seine", "MasterMind");
        public static bool RightHanded
        {
            get
            {
                return (bool)Registry.GetValue("General", "RightHanded", true);
            }
            set
            {
                Registry.SetValue("General", "RightHanded", value);
            }
        }
        public static bool LeftHanded
        {
            get
            {
                return !RightHanded;
            }
            set
            {
                RightHanded = false;
            }
        }
        public static bool BottomToTop
        {
            get
            {
                return (bool)Registry.GetValue("General", "BottomToTop", true);
            }
            set
            {
                Registry.SetValue("General", "BottomToTop", value);
            }
        }
        public static bool TopToBottom
        {
            get
            {
                return !BottomToTop;
            }
            set
            {
                BottomToTop = false;
            }
        }
        public static Cradle.Orientations CradleOrientation
        {
            get
            {
                return (Cradle.Orientations)Registry.GetValue("General", "CradleOrientation", Cradle.Orientations.Vertical);
            }
            set
            {
                Registry.SetValue("General", "CradleOrientation", value);
            }
        }            
    }
}
