using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MasterMind
{
    public class Records
    {
        public string RecordsPath { get; set; } = "";
        public static Records FromFile(string FileName)
        {
            Records records;
            string fullPath = System.IO.Path.Combine(Application.UserAppDataPath, FileName + ".json");
            if (File.Exists(fullPath))
            {
                string jsonString = File.ReadAllText(fullPath);
                JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
                records = JsonSerializer.Deserialize<Records>(jsonString, options);
                if (!records.AllPlayers.Any(pi => pi.ID < 0))
                {
                    records.AllPlayers.Add(new PlayerInfo("Renaldo") { ID = -500 });
                    records.AllPlayers.Add(new PlayerInfo("Úrsula") { ID = -400 });
                    records.AllPlayers.Add(new PlayerInfo("Andrés") { ID = -300 });
                    records.AllPlayers.Add(new PlayerInfo("Tati") { ID = -200 });
                    records.AllPlayers.Add(new PlayerInfo("Pepito") { ID = -100 });
                    records.WriteRecords();
                }
            }
            else
            {
                records = new Records() { RecordsPath = fullPath };
                records.AllPlayers.Add(new PlayerInfo("Renaldo") { ID = -500 });
                records.AllPlayers.Add(new PlayerInfo("Úrsula") { ID = -400 });
                records.AllPlayers.Add(new PlayerInfo("Andrés") { ID = -300 });
                records.AllPlayers.Add(new PlayerInfo("Tati") { ID = -200 });
                records.AllPlayers.Add(new PlayerInfo("Pepito") { ID = -100 });
                records.AllPlayers.Add(new PlayerInfo("Guest") { ID = 0 });
                records.WriteRecords();
            }
            return records!;
        }
        public void WriteRecords()
        {
            using (FileStream fileStream = System.IO.File.Create(RecordsPath))
            {
                JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
                JsonSerializer.Serialize(fileStream, this, options);
            }
        }

        public class Header
        {
            public int Version { get; } = 0;
        }
        public class PlayerInfo
        {
            public PlayerInfo(string PlayerName) : base()
            {
                this.PlayerName = PlayerName;
            }
            [JsonInclude]
            public int ID;
            public string PlayerName { get; set; } = "";
            [JsonInclude]
            public List<PuzzleInfo> PuzzleHistory = new List<PuzzleInfo>();
            [JsonIgnore]
            public bool IsAI { get { return ID < 0; } }
            [JsonIgnore]
            public bool IsHuman { get { return ID >= 0; } }
            public float AverageTurnsPerWin()
            {
                int puzzleCount = PuzzleHistory.Count(ph => ph.Won);
                if (puzzleCount > 0)
                {
                    return PuzzleHistory.Sum(ph => ph.Turns) / puzzleCount;
                }
                else
                {
                    return 0;
                }
            }
            public float WonLossPercentage()
            {
                if (PuzzleHistory.Count() > 0)
                {
                    return PuzzleHistory.Count(ph => ph.Won) / PuzzleHistory.Count();
                }
                else
                {
                    return 0;
                }
            }
            public float AverageTurnsPerWin(int[] ColorIndices)
            {
                string colors = Records.PuzzleToString(ColorIndices);
                IEnumerable<PuzzleInfo> puzzleHistory = PuzzleHistory.Where(ph => ph.Colors == colors);
                int wins = puzzleHistory.Count(ph => ph.Won);
                if (wins > 0)
                {
                    return puzzleHistory.Sum(ph => ph.Turns) / wins;
                }
                else
                {
                    return 0;
                }
            }
            public float WonLossPercentage(int[] ColorIndices)
            {
                string colors = Records.PuzzleToString(ColorIndices);
                IEnumerable<PuzzleInfo> puzzleHistory = PuzzleHistory.Where(ph => ph.Colors == colors);
                int gameCount = puzzleHistory.Count();
                if (gameCount > 0)
                {
                    return puzzleHistory.Count(ph => ph.Won) / gameCount;
                }
                else
                {
                    return 0;
                }
            }
        }
        [JsonInclude]
        public List<PlayerInfo> AllPlayers = new List<PlayerInfo>();
        public static PlayerInfo Player(string Name)
        {
            return Globals.Records.AllPlayers.FirstOrDefault(p => p.PlayerName.Equals(Name, StringComparison.CurrentCultureIgnoreCase));
        }
        public static PlayerInfo Player(int ID)
        {
            return Globals.Records.AllPlayers.FirstOrDefault(p => p.ID == ID);
        }
        public class PuzzleInfo
        {
            [JsonConstructor]
            public PuzzleInfo(string colors, string agnosticPatternByOrder, string agnosticPatternByColor, bool won, int turns, DateTime timeStamp) =>
                (Colors, AgnosticPatternByOrder, AgnosticPatternByColor, Won, Turns, TimeStamp) = (colors, agnosticPatternByOrder, agnosticPatternByColor, won, turns, timeStamp);
            public PuzzleInfo(int[] ColorIndices, bool won, int turns) : base()
            {
                TimeStamp = DateTime.Now;
                string colors, agnosticOrder, agnosticColor;
                Records.PuzzleToStrings(ColorIndices, out colors, out agnosticOrder, out agnosticColor);
                Colors = colors;
                AgnosticPatternByOrder = agnosticOrder;
                AgnosticPatternByColor = agnosticColor;
                Won = won;
                Turns = turns;
            }

            [JsonIgnore]
            public int PegCount { get { return Colors.Count(); } }
            public string Colors { get;  }
            public string AgnosticPatternByOrder { get;  }
            public string AgnosticPatternByColor { get;  }
            public bool Won { get; }
            public int Turns { get; }
            public DateTime TimeStamp { get; }
        }
        public static string PuzzleToString(int[] colorIndices)
        {
            return colorIndices.Select(c => c.ToString()).Aggregate((ag, c) => $"{ag}{c}");
        }
        public static void PuzzleToStrings(int[] colorIndices, out string colorString, out string agnosticOrder, out string agnosticColors)
        {
            colorString = colorIndices.Select(c => c.ToString()).Aggregate((ag, c) => $"{ag}{c}");

            Dictionary<int, int> indexToPosition = new Dictionary<int, int>();
            int[] colorCounts = new int[Globals.ColorsInUse.Count];
            int pos = 0;
            agnosticOrder = "";
            for (int i = 0; i < colorIndices.Length; i++)
            {
                colorCounts[colorIndices[i]] += 1;
                if (!indexToPosition.ContainsKey(colorIndices[i]))
                {
                    indexToPosition.Add(colorIndices[i], pos++);
                }
                agnosticOrder += indexToPosition[colorIndices[i]].ToString();
            }
            agnosticColors = colorCounts.Select(c => c.ToString()).Aggregate((ag, c) => $"{ag}{c}");
        }
        public PlayerInfo AddPlayer(string PlayerName)
        {
            PlayerInfo pi = new PlayerInfo(PlayerName);
            pi.ID = AllPlayers.Max(p => p.ID) + 1;
            AllPlayers.Add(pi);
            WriteRecords();
            return pi;
        }
        public void AddGameResult(string PlayerName, int[] ColorIndices, bool Won, int Turns)
        {
            PlayerInfo playerInfo = AllPlayers.First(pi => pi.PlayerName.Equals(PlayerName, StringComparison.OrdinalIgnoreCase));
            playerInfo.PuzzleHistory.Add(new PuzzleInfo(ColorIndices, Won, Turns));
            string colors = PuzzleToString(ColorIndices);
            WriteRecords();
        }
    }
}
