using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind
{
    public class CurrentGame
    {
        public CurrentGame() : base()
        {

        }
        public CurrentGame(IEnumerable<int> Puzzle) : base()
        {
            InitializeGame(Puzzle);
        }
        public List<int> Pattern = new List<int>();
        public List<Guess> Turns = new List<Guess>();
        public bool Completed = false;
        public bool Solved = false;
        public bool Lost = false;

        public class Guess
        {
            public Guess() : base()
            {
                Guesses.AddRange(Enumerable.Repeat<int>(1, Globals.PegsPerRow).Select(r => -1));
            }
            public List<int> Guesses = new List<int>();
            public int CorrectlyPlaced = 0;
            public int IncorrectlyPlaced = 0;
            public int Placed { get { return CorrectlyPlaced + IncorrectlyPlaced; } }
            public bool Completed = false;
        }

        public void InitializeGame()
        {
            InitializeGame(Enumerable.Range(0, Globals.PegsPerRow).Select(c => Globals.rnd.Next(0, Globals.ColorsInUse.Count())));
        }
        public void InitializeGame(IEnumerable<int> Puzzle)
        {
            Pattern = new List<int>(Puzzle);
            Turns.Clear();
            Completed = false;
            Solved = false;
            Lost = false;
        }
        public  int CurrentTurnIndex()
        {
            if (Turns.Count == 0)
            {
                return 0;
            }
            else if (Turns.Last().Completed)
            {
                return Turns.Count;
            }
            else
            {
                return Turns.Count - 1;
            }
        }
        public Guess LastTurn()
        {
            if (Turns.Count == 0)
            {
                return null;
            }
            else if (Turns.Last().Completed)
            {
                return Turns.Last();
            }
            else
            {
                return Turns[Turns.Count - 1];  
            }
        }
        public void PlacePeg(int Color, int Position)
        {
            if (Turns.Count ==0 || Turns.Last().Completed)
            {
                Turns.Add(new Guess());
            }
            Turns.Last().Guesses[Position] = Color;
        }
        public void RemovePeg(int Position)
        {
            Turns.Last().Guesses[Position] = -1;
        }
        public void ClearPegs()
        {
            for (int i = 0; i < Globals.PegsPerRow; i++)
            {
                Turns.Last().Guesses[i] = -1;
            }
        }
        public bool EvaluateTurn()
        {
            Guess LastGuess = Turns.Last();
            if (LastGuess.Guesses.Count(g => g >= 0) < Globals.PegsPerRow)
            {
                return false;
            }
            LastGuess.CorrectlyPlaced = 0;
            LastGuess.IncorrectlyPlaced = 0;

            // count the pegs correct
            int[] Solution = new List<int>(Pattern).ToArray<int>();
            List<int> OtherColors = new List<int>();
            for (int i = 0; i < Globals.PegsPerRow; i++)
            {
                if (LastGuess.Guesses[i] == Solution[i])
                {
                    LastGuess.CorrectlyPlaced++;
                    Solution[i] = -1;
                }
                else
                {
                    OtherColors.Add(LastGuess.Guesses[i]);
                }
            }
            foreach (int ColorIndex in OtherColors)
            {
                if (Solution.Any(c => ColorIndex == c))
                {
                    LastGuess.IncorrectlyPlaced++;
                    for (int i = 0; i < Globals.PegsPerRow; i++)
                    {
                        if (Solution[i] == ColorIndex)
                        {
                            Solution[i] = -1;
                            break;
                        }
                    }
                }
            }

            LastGuess.Completed = true;

            // return true if solved.
            Globals.CurrentGame.Solved = LastGuess.CorrectlyPlaced == Globals.PegsPerRow;
            return Globals.CurrentGame.Solved;
        }
    }
}
