using System.Diagnostics;
using System.Drawing;

namespace MasterMind
{
    internal class ComputerPlayer
    {
        //public ComputerPlayer() : base()
        //{
        //    PlayerName = "";
        //}
        public string PlayerName { get; set; } = "";
        private int[] ColorIndices = new int[6];

        const bool TestingTati = true;

        public void Solve()
        {
            if (TestingTati)
            {
                for (int i = 0; i < 6; i++)
                {
                    ColorIndices[i] = i;
                }
                //Globals.CurrentGame.InitializeGame(new int[] { 0, 0, 0, 0 });
            }
            else
            {
                List<int> IndicesRemaining = new List<int>(Enumerable.Range(0, 6));
                int i = 0;
                int idx;
                do
                {
                    idx = IndicesRemaining.ElementAt(Globals.rnd.Next(0, IndicesRemaining.Count));
                    ColorIndices[i++] = idx;
                    IndicesRemaining.Remove(idx);
                } while (IndicesRemaining.Count > 1);
                ColorIndices[i] = IndicesRemaining.First();
            }

            switch (PlayerName)
            {
                case "Renaldo":
                    break;
                case "Úrsula":
                    PlayUrsula();
                    break;
                case "Andrés":
                    break;
                case "Tati":
                    PlayTati();
                    break;
                case "Pepito":
                    break;
            }
        }

        public  void PlayUrsula()
        {

        }

#if false
        public void PlayTati()
        {
            // 1st turn
            for (int i = 0; i < 4;i++)
            {
                Globals.CurrentGame.PlacePeg(ColorIndices[0], i);
            }
            if (Globals.CurrentGame.EvaluateTurn())
            {
                // won in 1 turn
                return;
            }

            CurrentGame.Guess thisTurn = Globals.CurrentGame.Turns[0];
            List<CurrentGame.Guess> Turns = new List<CurrentGame.Guess>() { thisTurn };
            List<PegPositionInfo> Placements = new List<PegPositionInfo>(Enumerable.Range(0, 4).Select(i => new PegPositionInfo(i) ));
            if (thisTurn.Placed == 0)
            {
                // 1st color isn't used
                foreach (PegPositionInfo ppi in Placements)
                {
                    ppi.IsNot.Add(ColorIndices[0]);
                }
            }
            else
            {
                // 1st color is used, we don't know where
                foreach (PegPositionInfo ppi in Placements)
                {
                    ppi.CanBe.Add(ColorIndices[0]);
                }
            }

            // 2nd turn
            Globals.CurrentGame.PlacePeg(ColorIndices[0], 0);
            Globals.CurrentGame.PlacePeg(ColorIndices[0], 1);
            Globals.CurrentGame.PlacePeg(ColorIndices[1], 2);
            Globals.CurrentGame.PlacePeg(ColorIndices[1], 3);
            if (Globals.CurrentGame.EvaluateTurn())
            {
                // won in 2 turns
                return;
            }

            // placements after first 2 turns
            thisTurn = Globals.CurrentGame.Turns[1];
            Turns.Add(thisTurn);
            if (thisTurn.Placed <= Turns[0].CorrectlyPlaced)    // this isn't quite right; if the second color is used it could be replacing a correctly-placed peg
            {
                // 2nd color isn't used
                foreach (PegPositionInfo ppi in Placements)
                {
                    ppi.IsNot.Add(ColorIndices[1]);
                }
            }
            else if (Globals.CurrentGame.Turns[0].CorrectlyPlaced == 3 && thisTurn.Placed == 4)
            {
                // there were 3 of the first color and this is the other color, it's in the 1st or 2nd position
                Placements[0].CanBe.Add(ColorIndices[1]);
                Placements[1].CanBe.Add(ColorIndices[1]);
                Placements[2].IsNot.Add(ColorIndices[1]);
                Placements[3].IsNot.Add(ColorIndices[1]);
            }
            else if(Globals.CurrentGame.Turns[0].CorrectlyPlaced <= thisTurn.Placed)
            {
                if (thisTurn.CorrectlyPlaced == 0)
                {
                    // the second color can't be in positions 3 or 4
                    Placements[0].CanBe.Add(ColorIndices[1]);
                    Placements[1].CanBe.Add(ColorIndices[1]);
                    Placements[2].IsNot.Add(ColorIndices[1]);
                    Placements[3].IsNot.Add(ColorIndices[1]);
                }
                else if (thisTurn.Placed > Turns[0].CorrectlyPlaced)
                {
                    int NewlyCorrectlyPlaced = thisTurn.CorrectlyPlaced - Turns[0].CorrectlyPlaced;
                    // one or two of the second color are in the correct place
                    if (NewlyCorrectlyPlaced == 1)
                    {
                        // the 2nd color can be anywhere
                        Placements[0].CanBe.Add(ColorIndices[1]);
                        Placements[1].CanBe.Add(ColorIndices[1]);
                        Placements[2].CanBe.Add(ColorIndices[1]);
                        Placements[3].CanBe.Add(ColorIndices[1]);
                    }
                    else
                    {
                        // solved for the 2nd color
                        Placements[0].IsNot.Add(ColorIndices[1]);
                        Placements[1].IsNot.Add(ColorIndices[1]);
                        Placements[2].PegColor = ColorIndices[1];
                        Placements[3].PegColor = ColorIndices[1];
                    }
                }
                else
                {
                    throw new Exception($"2nd played piece, don't know what to do with it, {Globals.CurrentGame.Turns[0].CorrectlyPlaced},{thisTurn.CorrectlyPlaced},{thisTurn.IncorrectlyPlaced}");
                }
            }

            // distribution after 2 turns
            List<MinMax> PossibleColumnCounts = new List<MinMax>(Enumerable.Range(0, 6).Select(r => new MinMax(0, 4)));
            MinMax thisCounts = PossibleColumnCounts.ElementAt(ColorIndices[0]);
            thisCounts.MinCount = Globals.CurrentGame.Turns[0].CorrectlyPlaced;
            thisCounts.MaxCount = Globals.CurrentGame.Turns[0].CorrectlyPlaced;

            for (int i = 1; i < 6; i++)
            {
                PossibleColumnCounts.ElementAt(ColorIndices[i]).MaxCount = 4 - Globals.CurrentGame.Turns[0].CorrectlyPlaced;
            }
            // check this make sure it's correct in every case:
            thisCounts = PossibleColumnCounts.ElementAt(ColorIndices[1]);
            thisCounts.MinCount = thisTurn.Placed - Globals.CurrentGame.Turns[0].CorrectlyPlaced;
            if (thisTurn.Placed - Globals.CurrentGame.Turns[0].CorrectlyPlaced == 2)
            {
                thisCounts.MaxCount = 4 - Globals.CurrentGame.Turns[0].CorrectlyPlaced;
            }
            else
            {
                thisCounts.MaxCount = thisTurn.Placed - Turns[0].CorrectlyPlaced;
            }

            // continue trying to find the count of each color
            int turn = 2;
            while (PossibleColumnCounts.Sum(pcc => pcc.MinCount) < 4 && turn < 6)
            {
                Globals.CurrentGame.PlacePeg(ColorIndices[0], 0);
                Globals.CurrentGame.PlacePeg(ColorIndices[0], 1);
                Globals.CurrentGame.PlacePeg(ColorIndices[turn], 2);
                Globals.CurrentGame.PlacePeg(ColorIndices[turn], 3);
                if (Globals.CurrentGame.EvaluateTurn())
                {
                    return;
                }

                thisTurn = Globals.CurrentGame.Turns[turn];
                Turns.Add(thisTurn);

                // update placements
                ////////////////////////////////////////////////////////////////////////////////////////////
                // DO THIS NEXT - this code is unedited and can't work
                if (thisTurn.Placed == Math.Min(2,Globals.CurrentGame.Turns[0].CorrectlyPlaced))
                {
                    // 2nd color isn't used
                    foreach (PegPositionInfo ppi in Placements)
                    {
                        ppi.IsNot.Add(ColorIndices[turn]);
                    }
                }
                else if (Globals.CurrentGame.Turns[0].CorrectlyPlaced == 3 && thisTurn.Placed == 4)
                {
                    // there were 3 of the first color and this is the other color, it's in the 1st or 2nd position
                    Placements[0].CanBe.Add(ColorIndices[turn]);
                    Placements[1].CanBe.Add(ColorIndices[turn]);
                    Placements[2].IsNot.Add(ColorIndices[turn]);
                    Placements[3].IsNot.Add(ColorIndices[turn]);
                }
                else if (Globals.CurrentGame.Turns[0].CorrectlyPlaced <= thisTurn.Placed)
                {
                    if (thisTurn.CorrectlyPlaced == 0)
                    {
                        // the second color can't be in positions 3 or 4
                        Placements[0].CanBe.Add(ColorIndices[turn]);
                        Placements[1].CanBe.Add(ColorIndices[turn]);
                        Placements[2].IsNot.Add(ColorIndices[turn]);
                        Placements[3].IsNot.Add(ColorIndices[turn]);
                    }
                    else if (thisTurn.Placed - Globals.CurrentGame.Turns[0].CorrectlyPlaced > 0)
                    {
                        // one or two of the second color are in the correct place
                        int NewlyCorrectlyPlaced = thisTurn.CorrectlyPlaced - Globals.CurrentGame.Turns[0].CorrectlyPlaced;
                        if (NewlyCorrectlyPlaced == 1)
                        {
                            // the 2nd color can be anywhere
                            Placements[0].CanBe.Add(ColorIndices[turn]);
                            Placements[1].CanBe.Add(ColorIndices[turn]);
                            Placements[2].CanBe.Add(ColorIndices[turn]);
                            Placements[3].CanBe.Add(ColorIndices[turn]);
                        }
                        else
                        {
                            Debug.Assert(NewlyCorrectlyPlaced == 2);
                            // solved for the 2nd color
                            Placements[0].IsNot.Add(ColorIndices[turn]);
                            Placements[1].IsNot.Add(ColorIndices[turn]);
                            Placements[2].PegColor = ColorIndices[turn];
                            Placements[3].PegColor = ColorIndices[turn];
                        }
                    }
                    else
                    {
                        throw new Exception($"{turn+1}th played piece, don't know what to do with it, {Globals.CurrentGame.Turns[0].CorrectlyPlaced},{thisTurn.CorrectlyPlaced},{thisTurn.IncorrectlyPlaced}");
                    }
                }
                ////////////////////////////////////////////////////////////////////////////////////////////

                // update distribution
                if (thisTurn.Placed - Turns.ElementAt(0).CorrectlyPlaced == 2)
                {
                    PossibleColumnCounts.ElementAt(ColorIndices[turn]).MaxCount = 4 - PossibleColumnCounts.Sum(pcc => pcc.MinCount);
                    PossibleColumnCounts.ElementAt(ColorIndices[turn]).MinCount = 2;
                }
                else
                {
                    PossibleColumnCounts.ElementAt(ColorIndices[turn]).MinCount = thisTurn.Placed - Turns.ElementAt(0).CorrectlyPlaced;
                    PossibleColumnCounts.ElementAt(ColorIndices[turn]).MaxCount = thisTurn.Placed - Turns.ElementAt(0).CorrectlyPlaced;
                }
                turn++;
            }
            if (turn == 6)
            {
                // one of the pegs appears 3-4 times
            }

            for (int i = 0; i < 6; i++)
            {
                Debug.WriteLine($"{i}:{PossibleColumnCounts[i]}");
            }
            Debug.WriteLine("--");
            for (int i = 0; i < 4; i++)
            {
                Debug.WriteLine($"{i}:{Placements.ElementAt(i).ToString()}");
            }
            Debug.WriteLine(new String('-', 10));

            // all colors are known
            // figure out where they go
            List<int> MasterListOfColors = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < PossibleColumnCounts[i].MinCount; j++)
                {
                    MasterListOfColors.Add(j);
                }
            }
            List<int> CopyOfListOfColors;
            List<PegPositionInfo> Solved = new List<PegPositionInfo>(Placements.Where(p => p.Solved));
            List<PegPositionInfo> Unsolved = new List<PegPositionInfo>(Placements.Where(p => !p.Solved));
            List<PegPositionInfo> CopyOfUnsolved;
            foreach (PegPositionInfo ppi in Solved)
            {
                MasterListOfColors.Remove(ppi.PegColor);
            }
            do
            {
                CopyOfListOfColors = new List<int>(MasterListOfColors);

                // place the known pegs
                foreach (PegPositionInfo ppi in Solved)
                {
                    Globals.CurrentGame.PlacePeg(ppi.PegColor, ppi.Position);
                }

                // look for positions that can be only one color
                CopyOfUnsolved = new List<PegPositionInfo>(Unsolved);
                foreach (PegPositionInfo ppi in CopyOfUnsolved)
                {
                    if (ppi.CanBe.Distinct().Count() == 1)
                    {
                        Debug.Assert(CopyOfListOfColors.Contains(ppi.CanBe[0]));
                        ppi.PegColor = ppi.CanBe[0];
                        CopyOfListOfColors.Remove(ppi.CanBe[0]);
                        MasterListOfColors.Remove(ppi.CanBe[0]);
                        Globals.CurrentGame.PlacePeg(ppi.PegColor, ppi.Position);
                        
                        Solved.Add(ppi);
                        Unsolved.Remove(ppi);
                    }
                }
                if (Unsolved.Count() == 1)
                {
                    Unsolved.First().PegColor = CopyOfListOfColors[0];
                    Globals.CurrentGame.PlacePeg(CopyOfListOfColors[0], Unsolved.First().Position);
                }
                else
                {
                    // go through the remaining places and solve for colors that are in IsNot in all remaining positions except one.
                    List<int> YetAnotherCopyOfListOfColors = new List<int>(CopyOfListOfColors);
                    for (int i = 0; i < CopyOfListOfColors.Count; i++)
                    {
                        if (Unsolved.Where(u => !u.IsNot.Contains(CopyOfListOfColors[i])).Count() == 1)
                        {
                            PegPositionInfo ppi = Unsolved.First(u => !u.IsNot.Contains(YetAnotherCopyOfListOfColors[i]));
                            Globals.CurrentGame.PlacePeg(ppi.PegColor, ppi.Position);
                            Unsolved.Remove(ppi);
                            Solved.Add(ppi);
                        }
                    }
                }
                if (Unsolved.Count == 0)
                {
                    break;      // winner!
                }
                else
                {
                    // take a guess, the remaining pegs' positions haven't been determined; it could be 2,3,4 pegs
                    return;
                }

                turn++;
            } while (Globals.CurrentGame.Turns.Count < 10);

            //for (int ti = 0; ti < Globals.CurrentGame.Turns.Count(); ti++)
            //{
            //    thisTurn = Turns[ti];

            //    for (int redPeg = 0; redPeg < thisTurn.CorrectlyPlaced; redPeg++)
            //    {
            //        for (int i = 0; i < 4; i++)
            //        {
            //            if (!Placements.ElementAt(i).Solved)
            //            {
            //                PegPositionInfo ppi = Placements.ElementAt(i);
            //                if (!ppi.IsNot.Contains(thisTurn.Guesses[i]))
            //                {
            //                    ppi.CanBe.Add(thisTurn.Guesses[i]);
            //                }
            //            }
            //        }
            //    }
            //    for (int whitePeg = 0; whitePeg < thisTurn.IncorrectlyPlaced; whitePeg++)
            //    {
            //        for (int i = 0; i < 4; i++)
            //        {
            //            if (!Placements.ElementAt(i).Solved)
            //            {
            //                PegPositionInfo ppi = Placements.ElementAt(i);
            //                if (!ppi.IsNot.Contains(thisTurn.Guesses[i]))
            //                {
            //                    ppi.CanBe.Add(thisTurn.Guesses[i]);
            //                }
            //            }
            //        }
            //    }
            //} while (Globals.CurrentGame.Turns.Count < 10);
        }
        private class MinMax
        {
            public MinMax(int min, int max)
            {
                MinCount = min;
                MaxCount = max;
            }
            public int MinCount;
            public int MaxCount;
            public override string ToString()
            {
                return $"Min={MinCount},Max={MaxCount}";
            }
        }
        private class PegPositionInfo
        {
            public PegPositionInfo(int position) : base()
            {
                Position = position;
            }
            public bool Solved { get { return PegColor >= 0; } }
            public int Position;
            public int PegColor = -1;
            public List<int> CanBe = new List<int>();
            public List<int> IsNot = new List<int>();
            public override string ToString()
            {
                return ($"Solved:{Solved},PegColor:{PegColor},CanBe:{CanBe.Select(cb => cb.ToString()).Aggregate((ag,c)=>$"{ag}-{c}").Trim('-')},IsNot:{IsNot.Select(cb => cb.ToString()).Aggregate((ag, c) => $"{ag}-{c}").Trim('-')}");
            }
        }
#else
        public void PlayRenaldo()
        {
            #region "1st turn"
            for (int i = 0; i < 4; i++)
            {
                Globals.CurrentGame.PlacePeg(ColorIndices[0], i);
            }
            if (Globals.CurrentGame.EvaluateTurn())
            {
                // won in 1 turn
                return;
            }

            CurrentGame.Guess lastTurn = Globals.CurrentGame.Turns[0];
            List<CurrentGame.Guess> Turns = new List<CurrentGame.Guess>() { lastTurn };
            List<PegPositionInfo> Placements = new List<PegPositionInfo>(Enumerable.Range(0, 4).Select(i => new PegPositionInfo(i)));

            List<MinMax> MinMaxes = new List<MinMax>(Enumerable.Range(0, 6).Select(i => new MinMax(0, 4)));
            MinMax lastMinMax = MinMaxes[ColorIndices[0]];
            lastMinMax.MinCount = lastTurn.Placed;
            lastMinMax.MaxCount = lastTurn.Placed;
            if (lastTurn.Placed == 0)
            {
                // 1st color isn't used
                foreach (PegPositionInfo ppi in Placements)
                {
                    ppi.IsNot.Add(ColorIndices[0]);
                }
            }
            else
            {
                // 1st color is used, we don't know where
                foreach (PegPositionInfo ppi in Placements)
                {
                    ppi.CanBe.Add(ColorIndices[0]);
                }
                // all other maxes affected
                for (int i = 1; i < 6; i++)
                {
                    MinMaxes[i].MaxCount = 4 - lastTurn.Placed;
                }
            }
            #endregion

            #region "2nd turn"
            Globals.CurrentGame.PlacePeg(ColorIndices[0], 0);
            Globals.CurrentGame.PlacePeg(ColorIndices[0], 1);
            Globals.CurrentGame.PlacePeg(ColorIndices[1], 2);
            Globals.CurrentGame.PlacePeg(ColorIndices[1], 3);
            if (Globals.CurrentGame.EvaluateTurn())
            {
                // won in 2 turns
                return;
            }

            // placements after first 2 turns
            lastTurn = Globals.CurrentGame.Turns[1];
            Turns.Add(lastTurn);
            lastMinMax = MinMaxes[ColorIndices[1]];

            switch (Turns[0].CorrectlyPlaced)
            {
                case 0:
                    switch (lastTurn.CorrectlyPlaced)
                    {
                        case 0:
                            switch (lastTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Placements[i].IsNot.Add(ColorIndices[1]);
                                    }
                                    lastMinMax.MaxCount = 0;
                                    break;
                                case 1:
                                    Placements[0].CanBe.Add(ColorIndices[1]);
                                    Placements[1].CanBe.Add(ColorIndices[1]);
                                    Placements[2].IsNot.Add(ColorIndices[1]);
                                    Placements[3].IsNot.Add(ColorIndices[1]);
                                    lastMinMax.MinCount = 1;
                                    lastMinMax.MaxCount = 1;
                                    break;
                                case 2:
                                    Placements[0].SetPegColor(ColorIndices[1]);
                                    Placements[1].SetPegColor(ColorIndices[1]);
                                    Placements[2].IsNot.Add(ColorIndices[1]);
                                    Placements[3].IsNot.Add(ColorIndices[1]);
                                    lastMinMax.MinCount = 2;
                                    lastMinMax.MaxCount = 2;
                                    break;
                                default:
                                    throw new Exception(ExceptionMsg(1));
                            }
                            break;
                        case 1:
                            switch (lastTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    Placements[0].IsNot.Add(ColorIndices[1]);
                                    Placements[1].IsNot.Add(ColorIndices[1]);
                                    Placements[2].CanBe.Add(ColorIndices[1]);
                                    Placements[3].CanBe.Add(ColorIndices[1]);
                                    lastMinMax.MinCount = 1;
                                    lastMinMax.MaxCount = 1;
                                    break;
                                case 1:
                                    Placements[0].CanBe.Add(ColorIndices[1]);
                                    Placements[1].CanBe.Add(ColorIndices[1]);
                                    Placements[2].CanBe.Add(ColorIndices[1]);
                                    Placements[3].CanBe.Add(ColorIndices[1]);
                                    lastMinMax.MinCount = 2;
                                    lastMinMax.MaxCount = 2;
                                    break;
                                default:
                                    throw new Exception(ExceptionMsg(1));
                            }
                            break;
                        case 2:
                            switch (lastTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    Placements[0].IsNot.Add(ColorIndices[1]);
                                    Placements[1].IsNot.Add(ColorIndices[1]);
                                    Placements[2].SetPegColor(ColorIndices[1]);
                                    Placements[3].SetPegColor(ColorIndices[1]);
                                    lastMinMax.MinCount = 2;
                                    lastMinMax.MaxCount = 2;
                                    // this would be a place to make a beeline to looking for the other two colors
                                    break;
                                default:
                                    throw new Exception(ExceptionMsg(1));
                            }
                            break;
                        default:
                            throw new Exception(ExceptionMsg(1));
                    }
                    break;
                case 1:
                    switch (lastTurn.CorrectlyPlaced)
                    {
                        case 0:
                            // went from one found to none found, which isn't possible
#if true
                            throw new Exception(ExceptionMsg(1));
#else
                            switch (lastTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Placements[i].IsNot.Add(ColorIndices[1]);
                                    }
                                    Placements[2].CanBe.Remove(ColorIndices[0]);
                                    Placements[3].CanBe.Remove(ColorIndices[0]);
                                    Placements[2].IsNot.Add(ColorIndices[0]);
                                    Placements[3].IsNot.Add(ColorIndices[0]);
                                    thisMinMax.MaxCount = 0;
                                    break;
                                case 1:
                                    // this could be the 1 got moved from left to right half, or the new one is left half and the 1st correct one is in right half
                                    Placements[0].CanBe.Remove(ColorIndices[0]);
                                    Placements[1].CanBe.Remove(ColorIndices[0]);
                                    Placements[0].IsNot.Add(ColorIndices[0]);
                                    Placements[1].IsNot.Add(ColorIndices[0]);
                                    Placements[0].CanBe.Add(ColorIndices[1]);
                                    Placements[1].CanBe.Add(ColorIndices[1]);
                                    Placements[0].IsNot.Add(ColorIndices[1]);
                                    Placements[1].IsNot.Add(ColorIndices[1]);
                                    thisMinMax.MaxCount = 1;
                                    break;
                                case 2:
                                    // 1st color is on right half
                                    // other two colors are in left half
                                    Placements[0].SetPegColor(ColorIndices[1]);
                                    Placements[1].SetPegColor(ColorIndices[1]);
                                    thisMinMax.MinCount = 2;
                                    thisMinMax.MaxCount = 2;
                                    break;
                                default:
                                    throw new Exception();
                            }
                            break;
                            #endif
                        case 1:
                            switch (lastTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    // 1 was correctly placed 1st turn, 1 is correctly placed this turn, it's the same 1.
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Placements[0].IsNot.Add(ColorIndices[1]);
                                    }
                                    lastMinMax.MinCount = 0;
                                    lastMinMax.MaxCount = 0;
                                    Placements[2].CanBe.Remove(ColorIndices[0]);
                                    Placements[3].CanBe.Remove(ColorIndices[0]);
                                    Placements[2].IsNot.Add(ColorIndices[0]);
                                    Placements[3].IsNot.Add(ColorIndices[0]);
                                    break;
                                case 1:
                                    // 1 was correctly placed 1st turn, c0 is correctly placed this turn, c1 is placed incorrectly.
                                    // one correct place and one incorrect place:
                                    // could be left half contains one of each c0 and c1
                                    // could be left half contains c1 and right half contains c0
                                    Placements[0].CanBe.Add(ColorIndices[1]);
                                    Placements[1].CanBe.Add(ColorIndices[1]);
                                    Placements[2].IsNot.Add(ColorIndices[1]);
                                    Placements[3].IsNot.Add(ColorIndices[1]);
                                    lastMinMax.MinCount = 1;
                                    lastMinMax.MaxCount = 1;
                                    break;
                                case 2:
                                    // c1 was correctly placed 2nd turn, c0 is incorrectly placed twice, revealing its true position
                                    Placements[0].SetPegColor(ColorIndices[1]);
                                    Placements[1].SetPegColor(ColorIndices[1]);
                                    lastMinMax.MinCount = 2;
                                    lastMinMax.MaxCount = 2;
                                    // now knowing 3 colors, one could diverge here into a quicker way to find the 4th color
                                    break;
                                default:
                                    throw new Exception(ExceptionMsg(1));
                            }
                            break;
                        case 2:
                            // c0 appeared in 1st turn, this turn also has c1 on right half
                            switch (lastTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    // c0 is on left side, c1 is on right side
                                    Placements[0].CanBe.Remove(ColorIndices[0]);
                                    Placements[1].CanBe.Remove(ColorIndices[0]);
                                    Placements[2].IsNot.Add(ColorIndices[0]);
                                    Placements[3].IsNot.Add(ColorIndices[0]);
                                    Placements[0].CanBe.Add(ColorIndices[1]);
                                    Placements[1].CanBe.Add(ColorIndices[1]);
                                    Placements[2].IsNot.Add(ColorIndices[1]);
                                    Placements[3].IsNot.Add(ColorIndices[1]);
                                    lastMinMax.MinCount = 1;
                                    lastMinMax.MaxCount = 1;
                                    break;
                                case 1:
                                    // c0 and c1 on left side, c1 on right side
                                    Placements[0].CanBe.Remove(ColorIndices[0]);
                                    Placements[1].CanBe.Remove(ColorIndices[0]);
                                    Placements[2].IsNot.Add(ColorIndices[0]);
                                    Placements[3].IsNot.Add(ColorIndices[0]);
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Placements[i].CanBe.Add(ColorIndices[1]);
                                    }
                                    lastMinMax.MinCount = 2;
                                    lastMinMax.MaxCount = 2;
                                    break;
                                default:
                                    throw new Exception(ExceptionMsg(1));
                            }
                            break;
                        default:
                            throw new Exception(ExceptionMsg(1));
                    }
                    break;
                case 2:
                    switch (lastTurn.CorrectlyPlaced)
                    {
                        case 0:
                            // 2-0-? - the two 1st colors have to be in the right half
                            Placements[0].CanBe.Remove(ColorIndices[0]);
                            Placements[1].CanBe.Remove(ColorIndices[0]);
                            Placements[2].SetPegColor(ColorIndices[0]);
                            Placements[3].SetPegColor(ColorIndices[0]);
                            switch (lastTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    Placements[0].IsNot.Add(ColorIndices[1]);
                                    Placements[1].IsNot.Add(ColorIndices[1]);
                                    lastMinMax.MaxCount = 0;
                                    break;
                                case 1:
                                    Placements[0].CanBe.Add(ColorIndices[1]);
                                    Placements[1].CanBe.Add(ColorIndices[1]);
                                    lastMinMax.MaxCount = 1;
                                    break;
                                case 2:
                                    Globals.CurrentGame.PlacePeg(ColorIndices[1], 0);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[1], 1);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[0], 2);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[0], 3);
                                    if (Globals.CurrentGame.EvaluateTurn())
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        throw new Exception(ExceptionMsg(1));
                                    }
                                default:
                                    throw new Exception(ExceptionMsg(1));
                            }
                            break;
                        case 1:
                            // 2-1-?
                            // original color appears once in right half, once in left half, 2nd color does not appear
                            for (int i = 0; i < 4; i++)
                            {
                                Placements[i].IsNot.Add(ColorIndices[1]);
                            }
                            switch (lastTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    // c0 appeared twice in first turn
                                    // TODO
                                    // all we know here is they can't both be in the same half
                                    break;
                                case 1:
                                    // TODO
                                    // one of the first color is in left half, the other position in left half is 2nd color, and another of the first color is in the second half
                                    break;
                                default:
                                    throw new Exception(ExceptionMsg(1));
                            }
                            break;
                        case 2:
                            // 2 correct 1st turn, 2 correct this turn, could be same 2
                            //  may need to backtrack to find out how many are new.
                            switch (lastTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    // either the sides can be swapped or the left side is correct, 2nd color not known
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Placements[i].CanBe.Add(ColorIndices[1]);
                                    }
                                    // this may be a place to diverge the strategy
                                    break;
                                case 1:
                                    // I think this is impossible
                                    throw new Exception(ExceptionMsg(1));
                                case 2:
                                    // puzzle solved - the two new are first two positions, the other positions are last turn
                                    Globals.CurrentGame.PlacePeg(ColorIndices[1], 0);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[1], 1);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[0], 2);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[0], 3);
                                    if (Globals.CurrentGame.EvaluateTurn())
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        throw new Exception(ExceptionMsg(1));
                                    }
                                default:
                                    throw new Exception(ExceptionMsg(1));
                            }
                            break;
                        case 3:
                            switch (lastTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    {
                                        // 3-0
                                        Placements[0].SetPegColor (ColorIndices[0]);
                                        Placements[1].SetPegColor ( ColorIndices[0]);
                                        Placements[2].CanBe.Add(ColorIndices[1]);
                                        Placements[3].CanBe.Add(ColorIndices[1]);
                                        lastMinMax.MinCount = 1;
                                        for (int i = 1; i < 6; i++)
                                        {
                                            MinMaxes[i].MaxCount = 1;
                                        }
                                        // first two are c0, one of the other two is c1, try to find the 4th color
                                        Globals.CurrentGame.PlacePeg(ColorIndices[2], 0);
                                        Globals.CurrentGame.PlacePeg(ColorIndices[3], 1);
                                        Globals.CurrentGame.PlacePeg(ColorIndices[4], 2);
                                        Globals.CurrentGame.PlacePeg(ColorIndices[4], 3);
                                        if (Globals.CurrentGame.EvaluateTurn())
                                        {
                                            return;
                                        }
                                        lastTurn = Globals.CurrentGame.Turns[2];
                                        switch (lastTurn.Placed)
                                        {
                                            case 0:
                                                // 4th color is c5
                                                Globals.CurrentGame.PlacePeg(ColorIndices[0], 0);
                                                Globals.CurrentGame.PlacePeg(ColorIndices[0], 1);
                                                Globals.CurrentGame.PlacePeg(ColorIndices[1], 2);
                                                Globals.CurrentGame.PlacePeg(ColorIndices[5], 3);
                                                if (Globals.CurrentGame.EvaluateTurn())
                                                {
                                                    return;
                                                }
                                                Globals.CurrentGame.PlacePeg(ColorIndices[0], 0);
                                                Globals.CurrentGame.PlacePeg(ColorIndices[0], 1);
                                                Globals.CurrentGame.PlacePeg(ColorIndices[5], 2);
                                                Globals.CurrentGame.PlacePeg(ColorIndices[1], 3);
                                                if (Globals.CurrentGame.EvaluateTurn())
                                                {
                                                    return;
                                                }
                                                else
                                                {
                                                    throw new Exception(ExceptionMsg(1));
                                                }
                                            case 1:
                                                {
                                                    switch (lastTurn.CorrectlyPlaced)
                                                    {
                                                        case 0:
                                                            // c2 or c3 is the 4th color
                                                            Globals.CurrentGame.PlacePeg(ColorIndices[0], 0);
                                                            Globals.CurrentGame.PlacePeg(ColorIndices[0], 1);
                                                            Globals.CurrentGame.PlacePeg(ColorIndices[1], 2);
                                                            Globals.CurrentGame.PlacePeg(ColorIndices[2], 3);
                                                            if (Globals.CurrentGame.EvaluateTurn())
                                                            {
                                                                return;
                                                            }
                                                            lastTurn = Globals.CurrentGame.Turns[3];
                                                            if (lastTurn.Placed == 4)
                                                            {
                                                                Globals.CurrentGame.PlacePeg(ColorIndices[0], 0);
                                                                Globals.CurrentGame.PlacePeg(ColorIndices[0], 1);
                                                                Globals.CurrentGame.PlacePeg(ColorIndices[2], 2);
                                                                Globals.CurrentGame.PlacePeg(ColorIndices[1], 3);
                                                            }
                                                            else if (lastTurn.IncorrectlyPlaced == 0)
                                                            {
                                                                Globals.CurrentGame.PlacePeg(ColorIndices[0], 0);
                                                                Globals.CurrentGame.PlacePeg(ColorIndices[0], 1);
                                                                Globals.CurrentGame.PlacePeg(ColorIndices[1], 2);
                                                                Globals.CurrentGame.PlacePeg(ColorIndices[3], 3);
                                                            }
                                                            else
                                                            {
                                                                Globals.CurrentGame.PlacePeg(ColorIndices[0], 0);
                                                                Globals.CurrentGame.PlacePeg(ColorIndices[0], 1);
                                                                Globals.CurrentGame.PlacePeg(ColorIndices[3], 2);
                                                                Globals.CurrentGame.PlacePeg(ColorIndices[1], 3);
                                                            }
                                                            if (Globals.CurrentGame.EvaluateTurn())
                                                            {
                                                                return;
                                                            }
                                                            else
                                                            {
                                                                throw new Exception(ExceptionMsg(1));
                                                            }
                                                        case 1:
                                                            // c4 is the 4th color
                                                            Placements[2].CanBe.Add(ColorIndices[4]);
                                                            Placements[3].CanBe.Add(ColorIndices[4]);
                                                            if (DeduceFromFourColors(Placements, MinMaxes))
                                                            {
                                                                return;
                                                            }
                                                            else
                                                            {
                                                                throw new Exception(ExceptionMsg(1));
                                                            }
                                                    }
                                                }
                                                break;
                                        }
                                        break;
                                    }
                                case 1:
                                    // 3-1
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Placements[i].CanBe.Add(ColorIndices[1]);
                                    }
                                    lastMinMax.MinCount = 2;
                                    lastMinMax.MaxCount = 2;
                                    if (!DeduceFromFourColors(Placements, MinMaxes))
                                    {
                                        throw new Exception(ExceptionMsg(1));
                                    };
                                    return;
                                default:
                                    throw new Exception(ExceptionMsg(1));
                            }
                            break;
                    }
                    break;
                case 3:
                    if (lastTurn.IncorrectlyPlaced == 1)
                    {
                        // found all four colors
                        Placements[0].CanBe.Add(ColorIndices[1]);
                        Placements[1].CanBe.Add(ColorIndices[1]);
                        Placements[2].SetPegColor ( ColorIndices[0]);
                        Placements[3].SetPegColor ( ColorIndices[1]);
                        lastMinMax.MinCount = 1;
                        lastMinMax.MaxCount = 1;
                        if (!DeduceFromFourColors(Placements, MinMaxes))
                        {
                            throw new Exception();
                        }
                        return;
                    }
                    else
                    {
                        for (int i = 0;i < 4; i++)
                        {
                            Placements[i].IsNot.Add(ColorIndices[1]);
                        }
                        lastMinMax.MaxCount = 0;
                    }
                    break;
            }

            // continue for the remaining colors
            string ExceptionMsg(int turn)
            {
                return $"Turn {turn}: {Turns[0].CorrectlyPlaced}-{lastTurn.CorrectlyPlaced}-{lastTurn.IncorrectlyPlaced}";
            }
            for (int color = 2; color < 6; color++)
            {
                Globals.CurrentGame.PlacePeg(ColorIndices[0], 0);
                Globals.CurrentGame.PlacePeg(ColorIndices[0], 1);
                Globals.CurrentGame.PlacePeg(ColorIndices[color], 2);
                Globals.CurrentGame.PlacePeg(ColorIndices[color], 3);
                if (Globals.CurrentGame.EvaluateTurn())
                {
                    return;
                }
                lastTurn = Globals.CurrentGame.Turns[color];
                lastMinMax= MinMaxes[color];
                switch (Turns[0].CorrectlyPlaced)
                {
                    case 0:
                        switch (lastTurn.CorrectlyPlaced)
                        {
                            case 0:
                                switch (lastTurn.IncorrectlyPlaced)
                                {
                                    case 0:
                                        for (int i = 0; i <4; i++)
                                        {
                                            Placements[i].IsNot.Add(ColorIndices[color]);
                                        }
                                        lastMinMax.MinCount = 0;
                                        lastMinMax.MaxCount = 0;
                                        break;
                                    case 1:
                                        Placements[0].CanBe.Add(ColorIndices[color]);
                                        Placements[1].CanBe.Add(ColorIndices[color]);
                                        Placements[2].IsNot.Add(ColorIndices[color]);
                                        Placements[3].IsNot.Add(ColorIndices[color]);
                                        lastMinMax.MinCount = 1;
                                        lastMinMax.MaxCount = 1;
                                        break;
                                    case 2:
                                        Placements[0].SetPegColor(ColorIndices[color]);
                                        Placements[1].SetPegColor(ColorIndices[color]);
                                        Placements[2].CanBe.Add(ColorIndices[color]);
                                        Placements[3].CanBe.Add(ColorIndices[color]);
                                        lastMinMax.MinCount = 2;
                                        switch (MinMaxes.Sum(mm => mm.MinCount))
                                        {
                                            case 2:
                                                lastMinMax.MaxCount = 2;
                                                break;
                                            case 3:
                                                lastMinMax.MaxCount = 1;
                                                break;
                                            default:
                                                throw new Exception(ExceptionMsg(color));
                                        }
                                        break;
                                    default:
                                        throw new Exception(ExceptionMsg(color));
                                }
                                break;
                            case 1:
                                switch (lastTurn.IncorrectlyPlaced)
                                {
                                    case 0:
                                        for (int i = 0; i < 4; i++)
                                        {
                                            Placements[i].IsNot.Add(ColorIndices[color]);
                                        }
                                        MinMaxes[ColorIndices[0]].MaxCount = 1;   // just realized this can be done other places; also can apply to other colors
                                        lastMinMax.MinCount = 0;
                                        lastMinMax.MaxCount = 0;
                                        break;
                                   case 1:
                                        for (int i = 0; i < 4; i++)
                                        {
                                            Placements[i].CanBe.Add(ColorIndices[color]);
                                        }
                                        lastMinMax.MinCount = 2;
                                        lastMinMax.MaxCount = 2;
                                        break;
                                    case 2:
                                        Placements[0].SetPegColor(ColorIndices[0]);
                                        Placements[1].SetPegColor(ColorIndices[color]);
                                        Placements[2].CanBe.Add(ColorIndices[color]);
                                        Placements[2].CanBe.Add(ColorIndices[color]);
                                        lastMinMax.MinCount = 3;
                                        switch (MinMaxes.Sum(mm => mm.MinCount))
                                        {
                                            case 3:
                                                lastMinMax.MaxCount = 4;
                                                break;
                                            case 4:
                                                lastMinMax.MaxCount = 3;
                                                if (DeduceFromFourColors(Placements, MinMaxes))
                                                {
                                                    return;
                                                }
                                                throw new Exception(ExceptionMsg(color));
                                            default:
                                                throw new Exception(ExceptionMsg(color));
                                        }
                                        break;
                                    default:
                                        throw new Exception(ExceptionMsg(color));
                                }
                                break;
                            case 2:
                                switch (lastTurn.IncorrectlyPlaced)
                                {
                                    case 0:
                                        Placements[0].IsNot.Add(ColorIndices[color]);
                                        Placements[1].IsNot.Add(ColorIndices[color]);
                                        Placements[2].SetPegColor(ColorIndices[color]);
                                        Placements[3].SetPegColor(ColorIndices[color]);
                                        lastMinMax.MinCount = 2;
                                        lastMinMax.MaxCount = 2;
                                        break;
                                    default:
                                        throw new Exception(ExceptionMsg(color));
                                }
                                break;
                            default:
                                throw new Exception(ExceptionMsg(color));
                        }
                        break;
                    case 1:
                        switch (lastTurn.CorrectlyPlaced)
                        {
                            case 0:
                                switch (lastTurn.IncorrectlyPlaced)
                                {
                                    case 0:
                                        for (int i = 0; i < 4; i++)
                                        {
                                            Placements[i].IsNot.Add(ColorIndices[color]);
                                        }
                                        lastMinMax.MinCount = 0;
                                        lastMinMax.MaxCount = 0;
                                        break;
                                    default:
                                        throw new Exception();
                                }
                                break;
                            case 1:
                                switch (lastTurn.IncorrectlyPlaced)
                                {
                                    case 0:
                                        for (int i = 0; i < 4; i++)
                                        {
                                            Placements[i].IsNot.Add(ColorIndices[color]);
                                        }
                                        lastMinMax.MinCount = 0;
                                        lastMinMax.MaxCount = 0;
                                        break;
                                    case 1:
                                        Placements[0].CanBe.Add(ColorIndices[color]);
                                        Placements[1].CanBe.Add(ColorIndices[color]);
                                        Placements[2].IsNot.Add(ColorIndices[color]);
                                        Placements[3].IsNot.Add(ColorIndices[color]);
                                        lastMinMax.MinCount = 1;
                                        lastMinMax.MaxCount = 1;
                                        break;
                                    default:
                                        throw new Exception();
                                }
                                break;
                            case 2:
                                switch (lastTurn.IncorrectlyPlaced)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        break;
                                    case 2:
                                        break;
                                    case 3:
                                        break;
                                    default:
                                        throw new Exception(ExceptionMsg(color));
                                }
                                break;
                            default:
                                throw new Exception(ExceptionMsg(color));
                        }
                        break;
                    case 2:
                        switch (lastTurn.CorrectlyPlaced)
                        {
                            case 0:
                                switch (lastTurn.IncorrectlyPlaced)
                                {
                                    case 0:
                                    case 1:
                                        Placements[0].IsNot.Add(ColorIndices[color]);
                                        Placements[1].IsNot.Add(ColorIndices[color]);
                                        Placements[2].SetPegColor(ColorIndices[0]);
                                        Placements[3].SetPegColor(ColorIndices[0]);
                                        lastMinMax.MinCount = 0;
                                        lastMinMax.MaxCount = 0;
                                        break;
                                    case 2:
                                        Placements[0].SetPegColor(ColorIndices[color]);
                                        Placements[1].SetPegColor(ColorIndices[color]);
                                        Placements[2].SetPegColor(ColorIndices[0]);
                                        Placements[3].SetPegColor(ColorIndices[0]);
                                        if (Globals.CurrentGame.EvaluateTurn())
                                        {
                                            return;
                                        }
                                        throw new Exception();
                                }
                                break;
                            case 1:
                                switch (lastTurn.IncorrectlyPlaced)
                                {
                                    case 0:
                                        Placements[0].IsNot.Add(ColorIndices[color]);
                                        Placements[1].IsNot.Add(ColorIndices[color]);
                                        Placements[2].IsNot.Add(ColorIndices[color]);
                                        Placements[3].IsNot.Add(ColorIndices[color]);
                                        lastMinMax.MinCount = 0;
                                        lastMinMax.MaxCount = 0;
                                        break;
                                    case 1:
                                        Placements[0].CanBe.Add(ColorIndices[color]);
                                        Placements[1].CanBe.Add(ColorIndices[color]);
                                        Placements[2].CanBe.Add(ColorIndices[color]);
                                        Placements[3].CanBe.Add(ColorIndices[color]);
                                        lastMinMax.MinCount =1;
                                        lastMinMax.MaxCount =1;
                                        break;
                                    case 2:
                                        Placements[0].SetPegColor(ColorIndices[color]);
                                        Placements[1].SetPegColor(ColorIndices[color]);
                                        lastMinMax.MinCount =2;
                                        lastMinMax.MaxCount =2;
                                        break;
                                    default:
                                        throw new Exception();
                                }
                                break;
                            case 2:
                                switch (lastTurn.IncorrectlyPlaced)
                                {
                                    case 0:
                                        for (int i = 0; i < 4; i++)
                                        {
                                            Placements[i].IsNot.Add(ColorIndices[color]);
                                        }
                                        lastMinMax.MinCount = 0;
                                        lastMinMax.MaxCount = 0;
                                        break;
                                    default:
                                        throw new Exception(ExceptionMsg(color));
                                }
                                break;
                            case 3:
                                switch (lastTurn.IncorrectlyPlaced)
                                {
                                    case 0:
                                        Placements[0].IsNot.Add(ColorIndices[color]);
                                        Placements[1].IsNot.Add(ColorIndices[color]);
                                        Placements[2].CanBe.Add(ColorIndices[color]);
                                        Placements[3].CanBe.Add(ColorIndices[color]);
                                        lastMinMax.MinCount = 1;
                                        lastMinMax.MaxCount = 1;
                                        break;
                                    default:
                                        throw new Exception(ExceptionMsg(color));
                                }
                                break;
                            default:
                                throw new Exception(ExceptionMsg(color));
                        }
                        break;
                    case 3:
                        switch (lastTurn.CorrectlyPlaced)
                        {
                            case 0:
                            case 1:
                                throw new Exception();
                            case 2:
                                // lost one correct one and this color isn't involved
                                Placements[0].SetPegColor(ColorIndices[0]);
                                Placements[1].SetPegColor(ColorIndices[0]);
                                Placements[2].IsNot.Add(ColorIndices[color]);
                                Placements[3].IsNot.Add(ColorIndices[color]);
                                lastMinMax.MinCount = 0;
                                lastMinMax.MaxCount = 0;
                                break;
                            case 3:
                                // this is the other color
                                Placements[0].SetPegColor(ColorIndices[0]);
                                Placements[1].SetPegColor(ColorIndices[0]);
                                Placements[2].CanBe.Add(ColorIndices[color]);
                                Placements[3].CanBe.Add(ColorIndices[color]);
                                lastMinMax.MinCount = 1;
                                lastMinMax.MaxCount = 1;
                                if (DeduceFromFourColors(Placements, MinMaxes))
                                {
                                    return;
                                }
                                throw new Exception();
                            default:
                                throw new Exception();
                        }
                        break;
                }

                // try to solve with the given info
                if (MinMaxes.Sum(mm=>mm.MinCount) == 4)
                {
                    if (DeduceFromFourColors(Placements, MinMaxes))
                    {
                        return;
                    }
                }
            }

            return;
#if true
#else
            // Everything here is wrong. There's no taking into account how many this time vs. last time.
            switch (Turns[0].CorrectlyPlaced)
            {
                case 0:
                    // 0 placed 1st turn
                    switch (thisTurn.CorrectlyPlaced)
                    {
                        case 0:
                            // 0 placed 1st turn, 0 correctly placed this turn
                            switch (thisTurn.IncorrectlyPlaced)
                            {
                                // 0 placed 1st turn, 0 correctly placed this turn, 0 incorrectly placed this turn
                                case 0:
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Placements[i].IsNot.Add(ColorIndices[1]);
                                    }
                                    thisMinMax.MinCount = 0;
                                    thisMinMax.MaxCount = 0;
                                    break;
                                case 1:
                                    Placements[0].CanBe.Add(ColorIndices[1]);
                                    Placements[1].CanBe.Add(ColorIndices[1]);
                                    Placements[2].IsNot.Add(ColorIndices[1]);
                                    Placements[3].IsNot.Add(ColorIndices[1]);
                                    thisMinMax.MinCount = 1;
                                    thisMinMax.MaxCount = 1;
                                    break;
                                case 2:
                                    Placements[0].PegColor = ColorIndices[1];
                                    Placements[1].PegColor = ColorIndices[1];
                                    Placements[2].IsNot.Add(ColorIndices[1]);
                                    Placements[3].IsNot.Add(ColorIndices[1]);
                                    thisMinMax.MinCount = 2;
                                    thisMinMax.MaxCount = 2;
                                    break;
                            }
                            break;
                        case 1:
                            switch (thisTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    // 0 placed 1st turn, 1 correctly placed this turn, 0 incorrectly placed this turn
                                    Placements[0].IsNot.Add(ColorIndices[1]);
                                    Placements[1].IsNot.Add(ColorIndices[1]);
                                    Placements[2].CanBe.Add(ColorIndices[1]);
                                    Placements[3].CanBe.Add(ColorIndices[1]);
                                    thisMinMax.MinCount = 1;
                                    thisMinMax.MaxCount = 1;
                                    break;
                                case 1:
                                    // there's one in columns 0 or 1, another in columns 2 or 3
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Placements[i].CanBe.Add(ColorIndices[1]);
                                    }
                                    thisMinMax.MinCount = 2;
                                    thisMinMax.MaxCount = 2;
                                    break;
                                case 2:
                                    Placements[0].PegColor = ColorIndices[1];
                                    Placements[1].PegColor = ColorIndices[1];
                                    Placements[2].CanBe.Add(ColorIndices[1]);
                                    Placements[3].CanBe.Add(ColorIndices[1]);
                                    thisMinMax.MinCount = 3;
                                    thisMinMax.MaxCount = 3;
                                    break;
                            }
                            break;
                        case 2:
                            switch (thisTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    Placements[0].IsNot.Add(ColorIndices[0]);
                                    Placements[1].IsNot.Add(ColorIndices[1]);
                                    Placements[2].PegColor = ColorIndices[1];
                                    Placements[3].PegColor = ColorIndices[1];
                                    thisMinMax.MinCount = 2;
                                    thisMinMax.MaxCount = 2;
                                    break;
                                case 1:
                                    Placements[0].CanBe.Add(ColorIndices[0]);
                                    Placements[1].CanBe.Add(ColorIndices[1]);
                                    Placements[2].PegColor = ColorIndices[1];
                                    Placements[3].PegColor = ColorIndices[1];
                                    thisMinMax.MinCount = 3;
                                    thisMinMax.MaxCount = 3;
                                    break;
                            }
                            break;
                    }
                    break;
                case 1:
                    // 1 placed 1st turn
                    switch (thisTurn.CorrectlyPlaced)
                    {
                        case 0:
                            switch (thisTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    // 1 placed 1st turn, 0 correctly placed this turn, 0 incorrectly placed this turn
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Placements[i].IsNot.Add(ColorIndices[1]);
                                    }
                                    thisMinMax.MinCount = 0;
                                    thisMinMax.MaxCount = 0;
                                    break;
                                case 1:
                                    Placements[0].CanBe.Add(ColorIndices[1]);
                                    Placements[1].CanBe.Add(ColorIndices[1]);
                                    Placements[2].IsNot.Add(ColorIndices[1]);
                                    Placements[3].IsNot.Add(ColorIndices[1]);
                                    thisMinMax.MinCount = 1;
                                    thisMinMax.MaxCount = 1;
                                    break;
                                case 2:
                                    Placements[0].PegColor = ColorIndices[1];
                                    Placements[1].PegColor = ColorIndices[1];
                                    Placements[2].IsNot.Add(ColorIndices[1]);
                                    Placements[3].IsNot.Add(ColorIndices[1]);
                                    thisMinMax.MinCount = 2;
                                    thisMinMax.MaxCount = 2;
                                    break;
                            }
                            break;
                        case 1:
                            // 1 placed 1st turn, 1 correctly placed this turn
                            switch (thisTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    Placements[0].IsNot.Add(ColorIndices[1]);
                                    Placements[1].IsNot.Add(ColorIndices[1]);
                                    Placements[2].CanBe.Add(ColorIndices[1]);
                                    Placements[3].CanBe.Add(ColorIndices[1]);
                                    thisMinMax.MinCount = 1;
                                    thisMinMax.MaxCount = 1;
                                    break;
                                case 1:
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Placements[i].CanBe.Add(ColorIndices[1]);
                                    }
                                    thisMinMax.MinCount = 2;
                                    thisMinMax.MaxCount = 2;
                                    break;
                                case 2:
                                    Placements[0].PegColor = ColorIndices[1];
                                    Placements[1].PegColor = ColorIndices[1];
                                    Placements[2].CanBe.Add(ColorIndices[1]);
                                    Placements[3].CanBe.Add(ColorIndices[1]);
                                    // solved in 2 turns - we have all the colors
                                    Globals.CurrentGame.PlacePeg(ColorIndices[1], 0);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[1], 1);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[1], 2);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[0], 3);
                                    if (Globals.CurrentGame.EvaluateTurn())
                                    {
                                        // solved
                                        return;
                                    }
                                    Globals.CurrentGame.PlacePeg(ColorIndices[1], 0);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[1], 1);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[0], 2);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[1], 3);
                                    if (Globals.CurrentGame.EvaluateTurn())
                                    {
                                        // solved
                                        return;
                                    }
                                    else
                                    {
                                        throw new Exception("2nd turn, 1 correct 1st turn, 1+2 found 2nd turn, swapped 3-4");
                                    }
                            }
                            break;
                        case 2:
                            // 1 placed first time, 1 correctly this time, other 2 go into the first 2 holes
                            Globals.CurrentGame.PlacePeg(ColorIndices[1], 0);
                            Globals.CurrentGame.PlacePeg(ColorIndices[1], 1);
                            Globals.CurrentGame.PlacePeg(ColorIndices[1], 2);
                            Globals.CurrentGame.PlacePeg(ColorIndices[0], 3);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                // solved
                                return;
                            }
                            Globals.CurrentGame.PlacePeg(ColorIndices[1], 0);
                            Globals.CurrentGame.PlacePeg(ColorIndices[1], 1);
                            Globals.CurrentGame.PlacePeg(ColorIndices[0], 2);
                            Globals.CurrentGame.PlacePeg(ColorIndices[1], 3);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                // solved
                                return;
                            }
                            else
                            {
                                throw new Exception("2nd turn, 1 correct 1st turn, 1+2 found 2nd turn, swapped 3-4");
                            }
                    }
                    break;
                case 2:
                    // 2 placed 1st turn
                    switch (thisTurn.CorrectlyPlaced)
                    {
                        case 0:
                            switch (thisTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    // 2-0-0
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Placements[i].IsNot.Add(ColorIndices[1]);
                                    }
                                    thisMinMax.MinCount = 0;
                                    thisMinMax.MaxCount = 0;
                                    break;
                                case 1:
                                    Placements[0].CanBe.Add(ColorIndices[1]);
                                    Placements[1].CanBe.Add(ColorIndices[1]);
                                    Placements[2].IsNot.Add(ColorIndices[1]);
                                    Placements[3].IsNot.Add(ColorIndices[1]);
                                    thisMinMax.MinCount = 1;
                                    thisMinMax.MaxCount = 1;
                                    break;
                                case 2:
                                    Globals.CurrentGame.PlacePeg(ColorIndices[1], 0);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[1], 1);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[0], 2);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[0], 3);
                                    if (Globals.CurrentGame.EvaluateTurn())
                                    {
                                        // solved
                                        return;
                                    }
                                    else
                                    {
                                        throw new Exception("2nd turn, 2 correct 1st turn, 0+2 found 2nd turn");
                                    }
                            }
                            break;
                        case 1:
                            switch (thisTurn.IncorrectlyPlaced)
                            {
                                case 0:
                                    // 2-1-0
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Placements[i].CanBe.Add(ColorIndices[1]);
                                    }
                                    thisMinMax.MinCount = 1;
                                    thisMinMax.MaxCount = 1;
                                    break;
                                case 1:
                                    // we have all 4 colors, split into two halves
                                    // placed 2 first time, 1 second time with 1 incorrectly placed
                                    Globals.CurrentGame.PlacePeg(ColorIndices[0], 0);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[1], 1);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[1], 2);
                                    Globals.CurrentGame.PlacePeg(ColorIndices[0], 3);
                                    if (Globals.CurrentGame.EvaluateTurn())
                                    {
                                        // solved
                                        return;
                                    }
                                    thisTurn = Globals.CurrentGame.Turns[2];
                                    switch (thisTurn.CorrectlyPlaced)
                                    {
                                        case 0:
                                            Globals.CurrentGame.PlacePeg(ColorIndices[1], 0);
                                            Globals.CurrentGame.PlacePeg(ColorIndices[0], 1);
                                            Globals.CurrentGame.PlacePeg(ColorIndices[0], 2);
                                            Globals.CurrentGame.PlacePeg(ColorIndices[1], 3);
                                            if (Globals.CurrentGame.EvaluateTurn())
                                            {
                                                // solved
                                                return;
                                            }
                                            else
                                            {
                                                throw new Exception("3rd turn should be solved-g");
                                            }
                                        case 2:
                                            Globals.CurrentGame.PlacePeg(ColorIndices[1], 0);
                                            Globals.CurrentGame.PlacePeg(ColorIndices[0], 1);
                                            Globals.CurrentGame.PlacePeg(ColorIndices[1], 2);
                                            Globals.CurrentGame.PlacePeg(ColorIndices[0], 3);
                                            if (Globals.CurrentGame.EvaluateTurn())
                                            {
                                                // solved
                                                return;
                                            }
                                            else
                                            {
                                                Globals.CurrentGame.PlacePeg(ColorIndices[0], 0);
                                                Globals.CurrentGame.PlacePeg(ColorIndices[1], 1);
                                                Globals.CurrentGame.PlacePeg(ColorIndices[0], 2);
                                                Globals.CurrentGame.PlacePeg(ColorIndices[1], 3);
                                                if (Globals.CurrentGame.EvaluateTurn())
                                                {
                                                    // solved
                                                    return;
                                                }
                                                else
                                                {
                                                    throw new Exception("3rd turn should be solved-m");
                                                }
                                            }
                                        default:
                                            throw new Exception("3rd turn should be solved (default)");
                                    }
                            }
                            break;
                        case 2:
                            // 2-2-?
                            Globals.CurrentGame.PlacePeg(ColorIndices[0], 0);
                            Globals.CurrentGame.PlacePeg(ColorIndices[0], 1);
                            Globals.CurrentGame.PlacePeg(ColorIndices[1], 2);
                            Globals.CurrentGame.PlacePeg(ColorIndices[1], 3);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            else
                            {
                                throw new Exception();
                            }
                        default:
                            throw new Exception($"2/{thisTurn.CorrectlyPlaced}/-");
                    }
                    break;
                case 3:
                    // 3 placed 1st turn
                    for (int i = 0; i < 4; i++)
                    {
                        Placements[i].IsNot.Add(ColorIndices[1]);
                    }
                    thisMinMax.MinCount = 0;
                    thisMinMax.MaxCount = 0;
                    break;
            }
#endif
#endregion
        }
        private bool DeduceFromFourColors(IEnumerable<PegPositionInfo> ppiSource, IEnumerable<MinMax> mmSource)
        {
            // reduce list to positions that have only one possible color
            List<PegPositionInfo> ppiLocal = new List<PegPositionInfo>(ppiSource.Select(ppi => ppi.Clone()));
            List<MinMax> mmLocal = new List<MinMax>(mmSource.Select(mm => mm.Clone()));
            for (int i = 0; i < 4; i++)
            {
                if (ppiLocal[i].Solved)
                {
                    mmLocal[ppiLocal[i].PegColor].MinCount--;
                    mmLocal[ppiLocal[i].PegColor].MaxCount--;
                }
            }
            int color;
            //bool FoundSingleton;
            //do
            //{
            //    FoundSingleton = false;
            //    for (int i = 0; i < 4; i++)
            //    {
            //        if (ppiLocal[i].CanBe.Count() == 1)
            //        {
            //            color = ppiLocal[i].CanBe[0];
            //            ppiLocal[i].SetPegColor ( color);
            //            ppiLocal[i].CanBe.Clear();
            //            mmLocal[color].MinCount--;
            //            if (--mmLocal[color].MaxCount == 0)
            //            {
            //                for (int j = 0; j < 4; j++)
            //                {
            //                    ppiLocal[j].CanBe.Remove(ppiLocal[i].PegColor);
            //                }
            //            }
            //            FoundSingleton = true;
            //        }
            //    }
            //} while (FoundSingleton);

            if (ppiLocal.Any(ppi => !ppi.Solved))
            {
                Debug.Assert(ppiLocal.Where(ppi => !ppi.Solved).All(ppi => ppi.CanBe.Count() > 1));
                // fill the other positions sorta randomly
                //List<PegPositionInfo> ppiNew = new List<PegPositionInfo>(ppiSource.Where(ppi => !ppi.Solved).Select(ppi => ppi.Clone()));
                //List<MinMax> mmNew = new List<MinMax>(mmLocal.Select(mm => mm.Clone()));
                List<PegPositionInfo> ppisToRestoreThisColor = new List<PegPositionInfo>();
                List<int> canBesToRestore;
                foreach (PegPositionInfo ppi in ppiLocal.Where(ppi => !ppi.Solved))
                {
                    canBesToRestore = new List<int>(ppi.CanBe);
                    do
                    {
                        color = ppi.CanBe[0];
                        ppi.CanBe.Remove(color);
                        ppi.SetPegColor(color, false); // (fixed?)BUG: This reduces ppi.CanBe to 0 count, so it never iterates past it
                        mmLocal[color].MinCount--;
                        ppisToRestoreThisColor.Clear();
                        if (--mmLocal[color].MaxCount == 0)
                        {
                            // remove from CanBe everywhere
                            for (int j = 0; j < ppiLocal.Count(); j++)
                            {
                                if (ppiLocal[j].CanBe.Remove(color))
                                {
                                    ppisToRestoreThisColor.Add(ppiLocal[j]);
                                }
                            }

                            // recurse to see if that direction solves problem
                        }
                        if (ppiLocal.All(ppin => ppin.Solved))
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Globals.CurrentGame.PlacePeg(ppiLocal[i].PegColor, i);
                            }
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return true;
                            }
                            else if(Globals.CurrentGame.Turns.Count() >= 10)
                            {
                                // ran out of turns
                                return false;
                            }
                        }
                        else
                        {
                            if ( DeduceFromFourColors(ppiLocal, mmSource))
                            {
                                return true;
                            }
                        }
                        // didn't solve; retry with different configuration
                        foreach (PegPositionInfo ppiRestore in ppisToRestoreThisColor)
                        {
                            ppiRestore.CanBe.Add(color);
                        }
                    } while (ppi.CanBe.Count() > 0);
                    ppi.CanBe = canBesToRestore;
                }

                // yikes, couldn't solve, there's a bug
                return false;
            }
            else
            {
                for (  int i = 0; i < 4; i++)
                {
                    Globals.CurrentGame.PlacePeg(ppiLocal[i].PegColor, i);
                }
                return Globals.CurrentGame.EvaluateTurn();
            }
        }
        private class MinMax
        {
            public MinMax(int min, int max)
            {
                MinCount = min;
                MaxCount = max;
            }
            public int MinCount;
            public int MaxCount;
            public MinMax Clone()
            {
                return new MinMax(MinCount, MaxCount);
            }
            public override string ToString()
            {
                return $"Min={MinCount}, Max={MaxCount}";
            }
        }
        private class PegPositionInfo
        {
            public PegPositionInfo(int position) : base()
            {
                Position = position;
            }
            public bool Solved { get { return PegColor >= 0; } }
            public int Position;
            //private int PegColor = -1;   // change this to public to shut down errors
            public int PegColor { get; private set; } = -1;
            public void SetPegColor(int color, bool clearAdditonalData = true)
            {
                PegColor = color;
                if (clearAdditonalData)
                {
                    CanBe.Clear();
                    IsNot.Clear();
                    IsNot.AddRange(Enumerable.Range(0, 6).Where(i => i != color));
                }
            }
            public List<int> CanBe = new List<int>();
            public List<int> IsNot = new List<int>();
            public PegPositionInfo Clone()
            {
                PegPositionInfo clone = new PegPositionInfo(Position);
                clone.PegColor = PegColor;
                clone.CanBe = new List<int>(CanBe);
                clone.IsNot = new List<int>(IsNot);

                return clone;
            }
            public override string ToString()
            {
                return ($"Solved:{Solved}, PegColor:{PegColor}, CanBe:{CanBe.Select(cb => cb.ToString()).Aggregate((ag, c) => $"{ag}, {c}").Trim(',').Trim()}, IsNot:{IsNot.Select(cb => cb.ToString()).Aggregate((ag, c) => $"{ag}, {c}").Trim(',').Trim()}");
            }
        }

#if false
        // On the first turn there are 0,1,2,3 correct.
        // Keep track of all the possible combinations of where it is.
        // When info is learned about another color, store that then compare that info to what was already learned, making decision about which combinations are eliminated
        // When it's down to only one possible combination for a color, place those pegs.
        // Probably need to make everything linked in both directions, parents and children.
        private class PuzzleCrumb
        {
            public int PegCount { get { return Positions.Count; } }
            public List<int> Positions = new List<int>();
        }
        private class PuzzleHints
        {
            public PuzzleHints() { }

            public List<PuzzleCrumb> Crumbs = new List<PuzzleCrumb>();
            public void AddPositions(int count)
            {
                switch (count)
                {
                    case 1:
                        // 1,2;1,3;1,4;2,3,2,4
                        break;
                    case 2:
                        // 1,2,3;1,2,4;1,3,4;2,3,4
                        break;
                    case 3:
                        // 
                        break;
                }
            }
        }
        private class AllHints
        {
            public AllHints() { }

            public int[] Colors = new int[4] { Enumerable.Range(0, 4).Select(i => -1) };
        }
#endif
#endif

        private void PlayTati()
        {
            {
                int color = 0;
                List<PegPositionInfo> Placements = new List<PegPositionInfo>(Enumerable.Range(0, 4).Select(i => new PegPositionInfo(i)));
                List<MinMax> MinMaxes = new List<MinMax>(Enumerable.Range(0, 6).Select(i => new MinMax(0, 4)));
                CurrentGame.Guess lastTurn;
                do
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Globals.CurrentGame.PlacePeg(ColorIndices[color], i);
                    }
                    if (Globals.CurrentGame.EvaluateTurn())
                    {
                        return;
                    }
                    lastTurn = Globals.CurrentGame.Turns[color];
                    if (lastTurn.CorrectlyPlaced > 0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Placements[i].CanBe.Add(ColorIndices[color]);
                        }
                    }
                    MinMaxes[ColorIndices[color]].MinCount = lastTurn.CorrectlyPlaced;
                    color++;
                } while (MinMaxes.Sum(mm => mm.MinCount) < 4);

                // all the colors are known
                // there's no info about positions, make a guess
                switch (MinMaxes.Count(mm => mm.MinCount > 0))
                {
                    case 2:
                        // either a 3/1 split or 2/2 split
                        if (MinMaxes.Any(mm => mm.MinCount == 3))
                        {
                            SolveTati_3_1(MinMaxes);
                        }
                        else
                        {
                            SolveTati_2_2(MinMaxes);
                        }
                        break;
                    case 3:
                        // pair of one color and two singletons
                        SolveTati_2_1_1(MinMaxes);
                        break;
                    case 4:
                        // all singletons
                        SolveTati_1_1_1_1(MinMaxes);
                        break;
                }
            }
        }

        private void SolveTati_3_1(List<MinMax> MinMaxes)
        {
            // guaranteed min 2, max 3 turns to solve
            int color3 = MinMaxes.IndexOf(MinMaxes.First(mm => mm.MinCount == 3));
            int color1 = MinMaxes.IndexOf(MinMaxes.First(mm => mm.MinCount == 1));

            Globals.CurrentGame.PlacePeg(color3, 0);
            Globals.CurrentGame.PlacePeg(color3, 1);
            Globals.CurrentGame.PlacePeg(color1, 2);
            Globals.CurrentGame.PlacePeg(color1, 3);
            Globals.CurrentGame.EvaluateTurn(); // this will have 3 correct
            switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
            {
                case 1:
                    // oddball on the left
                    int i = Globals.rnd.Next(0, 2);
                    Globals.CurrentGame.PlacePeg(color1, i);
                    Globals.CurrentGame.PlacePeg(color3, 1 - i);
                    Globals.CurrentGame.PlacePeg(color3, 2);
                    Globals.CurrentGame.PlacePeg(color3, 3);
                    if (Globals.CurrentGame.EvaluateTurn())
                    {
                        return;
                    }
                    Globals.CurrentGame.PlacePeg(color3, i);
                    Globals.CurrentGame.PlacePeg(color1, 1 - i);
                    Globals.CurrentGame.PlacePeg(color3, 2);
                    Globals.CurrentGame.PlacePeg(color3, 3);
                    if (Globals.CurrentGame.EvaluateTurn())
                    {
                        return;
                    }
                    throw new Exception();
                case 3:
                    // oddball on the right
                    i = Globals.rnd.Next(2, 4);
                    Globals.CurrentGame.PlacePeg(color3, 0);
                    Globals.CurrentGame.PlacePeg(color3, 1);
                    Globals.CurrentGame.PlacePeg(color3, i);
                    Globals.CurrentGame.PlacePeg(color1, 5 - i);
                    if (Globals.CurrentGame.EvaluateTurn())
                    {
                        return;
                    }
                    Globals.CurrentGame.PlacePeg(color3, 0);
                    Globals.CurrentGame.PlacePeg(color3, 1);
                    Globals.CurrentGame.PlacePeg(color1, i);
                    Globals.CurrentGame.PlacePeg(color3, 5 - i);
                    if (Globals.CurrentGame.EvaluateTurn())
                    {
                        return;
                    }
                    throw new Exception();
            }
            throw new Exception();
        }

        private void SolveTati_2_2(List<MinMax> MinMaxes)
        {
            // guaranteed min 3, max 4 turns to solve
            int colorA = MinMaxes.IndexOf(MinMaxes.First(mm => mm.MinCount == 2));
            int colorB = MinMaxes.IndexOf(MinMaxes.Skip(colorA + 1).First(mm => mm.MinCount == 2));

            Globals.CurrentGame.PlacePeg(colorA, 0);
            Globals.CurrentGame.PlacePeg(colorA, 1);
            Globals.CurrentGame.PlacePeg(colorA, 2);
            Globals.CurrentGame.PlacePeg(colorB, 3);
            Globals.CurrentGame.EvaluateTurn(); // this will have 3 correct
            switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
            {
                case 1:
                    // pos 3 == color1
                    Globals.CurrentGame.PlacePeg(colorB, 0);
                    Globals.CurrentGame.PlacePeg(colorA, 1);
                    Globals.CurrentGame.PlacePeg(colorA, 2);
                    Globals.CurrentGame.PlacePeg(colorA, 3);
                    Globals.CurrentGame.EvaluateTurn(); // this will have 3 correct
                    switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
                    {
                        case 1:
                            Globals.CurrentGame.PlacePeg(colorA, 0);
                            Globals.CurrentGame.PlacePeg(colorB, 1);
                            Globals.CurrentGame.PlacePeg(colorB, 2);
                            Globals.CurrentGame.PlacePeg(colorA, 3);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            break;
                        case 3:
                            int i = Globals.rnd.Next(1, 3);
                            // pos 0 == color2, pos 3 == color1
                            Globals.CurrentGame.PlacePeg(colorB, 0);
                            Globals.CurrentGame.PlacePeg(colorB, i);
                            Globals.CurrentGame.PlacePeg(colorA, 3-i);
                            Globals.CurrentGame.PlacePeg(colorA, 3);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            Globals.CurrentGame.PlacePeg(colorB, 0);
                            Globals.CurrentGame.PlacePeg(colorA, i);
                            Globals.CurrentGame.PlacePeg(colorB, 3 - i);
                            Globals.CurrentGame.PlacePeg(colorA, 3);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            break;
                    }
                    throw new Exception();
                case 3:
                    // pos 3 == color2
                    Globals.CurrentGame.PlacePeg(colorA, 0);
                    Globals.CurrentGame.PlacePeg(colorB, 1);
                    Globals.CurrentGame.PlacePeg(colorB, 2);
                    Globals.CurrentGame.PlacePeg(colorA, 3);
                    Globals.CurrentGame.EvaluateTurn(); // this will have 3 correct
                    switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
                    {
                        case 0:
                            // swap
                            Globals.CurrentGame.PlacePeg(colorB, 0);
                            Globals.CurrentGame.PlacePeg(colorA, 1);
                            Globals.CurrentGame.PlacePeg(colorA, 2);
                            Globals.CurrentGame.PlacePeg(colorB, 3);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            break;
                        case 2:
                            // pos 0 == color1, pos 3 == color2
                            int i = Globals.rnd.Next(1, 3);
                            Globals.CurrentGame.PlacePeg(colorA, 0);
                            Globals.CurrentGame.PlacePeg(colorA, i);
                            Globals.CurrentGame.PlacePeg(colorB, 3-i);
                            Globals.CurrentGame.PlacePeg(colorB, 3);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            Globals.CurrentGame.PlacePeg(colorA, 0);
                            Globals.CurrentGame.PlacePeg(colorB, i);
                            Globals.CurrentGame.PlacePeg(colorA, 3-i);
                            Globals.CurrentGame.PlacePeg(colorB, 3);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            break;
                    }
                    throw new Exception();
            }
            throw new Exception();
        }

        private void SolveTati_2_1_1(List<MinMax> MinMaxes)
        {
            // guaranteed min 3, max 4 turns to solve
            int color2 = MinMaxes.IndexOf(MinMaxes.First(mm => mm.MinCount == 2));
            int colorA = MinMaxes.IndexOf(MinMaxes.First(mm => mm.MinCount == 1));
            int colorB = MinMaxes.IndexOf(MinMaxes.Skip(colorA + 1).First(mm => mm.MinCount == 1));

            Globals.CurrentGame.PlacePeg(colorA, 0);
            Globals.CurrentGame.PlacePeg(colorA, 1);
            Globals.CurrentGame.PlacePeg(colorB, 2);
            Globals.CurrentGame.PlacePeg(colorB, 3);
            Globals.CurrentGame.EvaluateTurn(); // this will have 3 correct
            int i = Globals.rnd.Next(2, 4);
            switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
            {
                case 0:
                    // left side color2 and colorB and right side color2 and colorA
                    Globals.CurrentGame.PlacePeg(color2, 0);
                    Globals.CurrentGame.PlacePeg(colorB, 1);
                    Globals.CurrentGame.PlacePeg(colorA, 2);
                    Globals.CurrentGame.PlacePeg(colorA, 3);
                    Globals.CurrentGame.EvaluateTurn(); // this will fail
                    switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
                    {
                        case 1:
                            Globals.CurrentGame.PlacePeg(colorB, 0);
                            Globals.CurrentGame.PlacePeg(color2, 1);
                            Globals.CurrentGame.PlacePeg(color2, i);
                            Globals.CurrentGame.PlacePeg(colorA, 5-i);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            Globals.CurrentGame.PlacePeg(colorB, 0);
                            Globals.CurrentGame.PlacePeg(color2, 1);
                            Globals.CurrentGame.PlacePeg(colorA, i);
                            Globals.CurrentGame.PlacePeg(color2, 5-i);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            break;
                        case 3:
                            Globals.CurrentGame.PlacePeg(color2, 0);
                            Globals.CurrentGame.PlacePeg(colorB, 1);
                            Globals.CurrentGame.PlacePeg(color2, i);
                            Globals.CurrentGame.PlacePeg(colorA, 5-i);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            Globals.CurrentGame.PlacePeg(color2, 0);
                            Globals.CurrentGame.PlacePeg(colorB, 1);
                            Globals.CurrentGame.PlacePeg(colorA, i);
                            Globals.CurrentGame.PlacePeg(color2, 5-i);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            break;
                    }
                    break;
                case 1:
                    // color2 on left or right
                    Globals.CurrentGame.PlacePeg(color2, 0);
                    Globals.CurrentGame.PlacePeg(colorB, 1);
                    Globals.CurrentGame.PlacePeg(colorA, 2);
                    Globals.CurrentGame.PlacePeg(colorA, 3);
                    Globals.CurrentGame.EvaluateTurn(); // this will fail
                    switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
                    {
                        case 0:
                            Globals.CurrentGame.PlacePeg(colorB, 0);
                            Globals.CurrentGame.PlacePeg(colorA, 1);
                            Globals.CurrentGame.PlacePeg(color2, 2);
                            Globals.CurrentGame.PlacePeg(color2, 3);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            break;
                        case 1:
                            Globals.CurrentGame.PlacePeg(colorA, 0);
                            Globals.CurrentGame.PlacePeg(colorB, 1);
                            Globals.CurrentGame.PlacePeg(color2, 2);
                            Globals.CurrentGame.PlacePeg(color2, 3);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            break;
                        case 2:
                            Globals.CurrentGame.PlacePeg(color2, 0);
                            Globals.CurrentGame.PlacePeg(color2, 1);
                            Globals.CurrentGame.PlacePeg(colorA, i);
                            Globals.CurrentGame.PlacePeg(colorB, 5-i);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            Globals.CurrentGame.PlacePeg(color2, 0);
                            Globals.CurrentGame.PlacePeg(color2, 1);
                            Globals.CurrentGame.PlacePeg(colorB, i);
                            Globals.CurrentGame.PlacePeg(colorA, 5-i);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            break;
                    }
                    break;
                case 2:
                    // left side color2 and colorA and right side color2 and colorB
                    Globals.CurrentGame.PlacePeg(color2, 0);
                    Globals.CurrentGame.PlacePeg(colorB, 1);
                    Globals.CurrentGame.PlacePeg(colorA, 2);
                    Globals.CurrentGame.PlacePeg(colorA, 3);
                    Globals.CurrentGame.EvaluateTurn(); // this will fail
                    switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
                    {
                        case 0:
                            Globals.CurrentGame.PlacePeg(colorA, 0);
                            Globals.CurrentGame.PlacePeg(color2, 1);
                            Globals.CurrentGame.PlacePeg(color2, i);
                            Globals.CurrentGame.PlacePeg(colorB, 5 - i);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            Globals.CurrentGame.PlacePeg(colorA, 0);
                            Globals.CurrentGame.PlacePeg(color2, 1);
                            Globals.CurrentGame.PlacePeg(colorB, i);
                            Globals.CurrentGame.PlacePeg(color2, 5 - i);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            break;
                        case 1:
                            Globals.CurrentGame.PlacePeg(color2, 0);
                            Globals.CurrentGame.PlacePeg(colorA, 1);
                            Globals.CurrentGame.PlacePeg(color2, i);
                            Globals.CurrentGame.PlacePeg(colorB, 5-i);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            Globals.CurrentGame.PlacePeg(color2, 0);
                            Globals.CurrentGame.PlacePeg(colorA, 1);
                            Globals.CurrentGame.PlacePeg(colorB, i);
                            Globals.CurrentGame.PlacePeg(color2, 5-i);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            break;
                        case 2:
                            Globals.CurrentGame.PlacePeg(colorA, 0);
                            Globals.CurrentGame.PlacePeg(color2, 1);
                            Globals.CurrentGame.PlacePeg(color2, i);
                            Globals.CurrentGame.PlacePeg(colorB, 5-i);
                            if (Globals.CurrentGame.EvaluateTurn())
                            {
                                return;
                            }
                            Globals.CurrentGame.PlacePeg(colorA, 0);
                            Globals.CurrentGame.PlacePeg(color2, 1);
                            Globals.CurrentGame.PlacePeg(colorB, 1);
                            Globals.CurrentGame.PlacePeg(color2, 5-1);
                            break;
                    }
                    break;
            }
            //throw new Exception();
        }

        private void SolveTati_1_1_1_1(List<MinMax> MinMaxes)
        {
            // min 3 turns to win, max 4, always succeeds in solving
            List<int> colors = new List<int>(Enumerable.Range(0, 6).Where(i => MinMaxes[i].MinCount == 1).Select(i => i));
            int colorA = colors[0];
            int colorB = colors[1];
            int colorC = colors[2];
            int colorD = colors[3];

            Globals.CurrentGame.PlacePeg(colorA, 0);
            Globals.CurrentGame.PlacePeg(colorB, 1);
            Globals.CurrentGame.PlacePeg(colorA, 2);
            Globals.CurrentGame.PlacePeg(colorB, 3);
            Globals.CurrentGame.EvaluateTurn();     // guaranteed to fail - worst case turn 7
            int FirstCorrectCount = Globals.CurrentGame.LastTurn().CorrectlyPlaced;
            if (FirstCorrectCount == 1)
            {
                // ACBA
                Globals.CurrentGame.PlacePeg(colorA, 0);
                Globals.CurrentGame.PlacePeg(colorC, 1);
                Globals.CurrentGame.PlacePeg(colorB, 2);
                Globals.CurrentGame.PlacePeg(colorA, 3);
                Globals.CurrentGame.EvaluateTurn();     // guaranteed to fail - worst case turn 8
            }
            else
            {
                // CDCD
                Globals.CurrentGame.PlacePeg(colorC, 0);
                Globals.CurrentGame.PlacePeg(colorD, 1);
                Globals.CurrentGame.PlacePeg(colorC, 2);
                Globals.CurrentGame.PlacePeg(colorD, 3);
                Globals.CurrentGame.EvaluateTurn();     // guaranteed to fail - worst case turn 8
            }
            int SecondCorrectCount = Globals.CurrentGame.LastTurn().CorrectlyPlaced;
            Debug.Write($"{FirstCorrectCount} / {SecondCorrectCount}");
            switch (FirstCorrectCount)
            {
                case 0:
                    // BADD
                    Globals.CurrentGame.PlacePeg(colorB, 0);
                    Globals.CurrentGame.PlacePeg(colorA, 1);
                    Globals.CurrentGame.PlacePeg(colorD, 2);
                    Globals.CurrentGame.PlacePeg(colorD, 3);
                    Globals.CurrentGame.EvaluateTurn();
                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                    switch (SecondCorrectCount)
                    {
                        case 0:
                            switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
                            {
                                case 0:
                                    // DCBA
                                    Globals.CurrentGame.PlacePeg(colorD, 0);
                                    Globals.CurrentGame.PlacePeg(colorC, 1);
                                    Globals.CurrentGame.PlacePeg(colorB, 2);
                                    Globals.CurrentGame.PlacePeg(colorA, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                                case 1:
                                    // DABC
                                    Globals.CurrentGame.PlacePeg(colorD, 0);
                                    Globals.CurrentGame.PlacePeg(colorA, 1);
                                    Globals.CurrentGame.PlacePeg(colorB, 2);
                                    Globals.CurrentGame.PlacePeg(colorC, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                                case 2:
                                    // BCDA
                                    Globals.CurrentGame.PlacePeg(colorB, 0);
                                    Globals.CurrentGame.PlacePeg(colorC, 1);
                                    Globals.CurrentGame.PlacePeg(colorD, 2);
                                    Globals.CurrentGame.PlacePeg(colorA, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                                case 3:
                                    // BADC
                                    Globals.CurrentGame.PlacePeg(colorB, 0);
                                    Globals.CurrentGame.PlacePeg(colorA, 1);
                                    Globals.CurrentGame.PlacePeg(colorD, 2);
                                    Globals.CurrentGame.PlacePeg(colorC, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                            }
                            break;
                        case 2:
                            switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
                            {
                                case 0:
                                    // CDBA
                                    Globals.CurrentGame.PlacePeg(colorC, 0);
                                    Globals.CurrentGame.PlacePeg(colorD, 1);
                                    Globals.CurrentGame.PlacePeg(colorB, 2);
                                    Globals.CurrentGame.PlacePeg(colorA, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                                case 1:
                                    // BDCA
                                    Globals.CurrentGame.PlacePeg(colorB, 0);
                                    Globals.CurrentGame.PlacePeg(colorD, 1);
                                    Globals.CurrentGame.PlacePeg(colorC, 2);
                                    Globals.CurrentGame.PlacePeg(colorA, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                                case 2:
                                    // CABD
                                    Globals.CurrentGame.PlacePeg(colorC, 0);
                                    Globals.CurrentGame.PlacePeg(colorA, 1);
                                    Globals.CurrentGame.PlacePeg(colorB, 2);
                                    Globals.CurrentGame.PlacePeg(colorD, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                                case 3:
                                    // BACD
                                    Globals.CurrentGame.PlacePeg(colorB, 0);
                                    Globals.CurrentGame.PlacePeg(colorA, 1);
                                    Globals.CurrentGame.PlacePeg(colorC, 2);
                                    Globals.CurrentGame.PlacePeg(colorD, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                            }
                            break;
                    }
                    break;
                case 1:
                    switch (SecondCorrectCount)
                    {
                        case 0:
                            // CADB
                            Globals.CurrentGame.PlacePeg(colorC, 0);
                            Globals.CurrentGame.PlacePeg(colorA, 1);
                            Globals.CurrentGame.PlacePeg(colorD, 2);
                            Globals.CurrentGame.PlacePeg(colorB, 3);
                            if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                            {
                                Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                return;
                            }
                            switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
                            {
                                case 0:
                                    // BDAC
                                    Globals.CurrentGame.PlacePeg(colorB, 0);
                                    Globals.CurrentGame.PlacePeg(colorD, 1);
                                    Globals.CurrentGame.PlacePeg(colorA, 2);
                                    Globals.CurrentGame.PlacePeg(colorC, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    break;
                                case 2:
                                    // DACB
                                    Globals.CurrentGame.PlacePeg(colorD, 0);
                                    Globals.CurrentGame.PlacePeg(colorA, 1);
                                    Globals.CurrentGame.PlacePeg(colorC, 2);
                                    Globals.CurrentGame.PlacePeg(colorB, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    break;
                            }
                            Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                            break;
                        case 1:
                            // CBDA
                            Globals.CurrentGame.PlacePeg(colorC, 0);
                            Globals.CurrentGame.PlacePeg(colorB, 1);
                            Globals.CurrentGame.PlacePeg(colorD, 2);
                            Globals.CurrentGame.PlacePeg(colorA, 3);
                            if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                            {
                                Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                return;
                            }
                            switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
                            {
                                case 0:
                                    // BCAD
                                    Globals.CurrentGame.PlacePeg(colorB, 0);
                                    Globals.CurrentGame.PlacePeg(colorC, 1);
                                    Globals.CurrentGame.PlacePeg(colorA, 2);
                                    Globals.CurrentGame.PlacePeg(colorD, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    break;
                                case 2:
                                    // DBCA
                                    Globals.CurrentGame.PlacePeg(colorD, 0);
                                    Globals.CurrentGame.PlacePeg(colorB, 1);
                                    Globals.CurrentGame.PlacePeg(colorC, 2);
                                    Globals.CurrentGame.PlacePeg(colorA, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    break;
                            }
                            Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                            break;
                        case 2:
                            // ADBC
                            Globals.CurrentGame.PlacePeg(colorA, 0);
                            Globals.CurrentGame.PlacePeg(colorD, 1);
                            Globals.CurrentGame.PlacePeg(colorB, 2);
                            Globals.CurrentGame.PlacePeg(colorC, 3);
                            if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                            {
                                Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                return;
                            }
                            break;
                        case 3:
                            //ACBD
                            Globals.CurrentGame.PlacePeg(colorA, 0);
                            Globals.CurrentGame.PlacePeg(colorC, 1);
                            Globals.CurrentGame.PlacePeg(colorB, 2);
                            Globals.CurrentGame.PlacePeg(colorD, 3);
                            if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                            {
                                Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                return;
                            }
                            break;
                    }
                    break;
                case 2:
                    // ABDD
                    Globals.CurrentGame.PlacePeg(colorA, 0);
                    Globals.CurrentGame.PlacePeg(colorB, 1);
                    Globals.CurrentGame.PlacePeg(colorD, 2);
                    Globals.CurrentGame.PlacePeg(colorD, 3);
                    Globals.CurrentGame.EvaluateTurn();
                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                    switch (SecondCorrectCount)
                    {
                        case 0:
                            switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
                            {
                                case 0:
                                    // DCAB
                                    Globals.CurrentGame.PlacePeg(colorD, 0);
                                    Globals.CurrentGame.PlacePeg(colorC, 1);
                                    Globals.CurrentGame.PlacePeg(colorA, 2);
                                    Globals.CurrentGame.PlacePeg(colorB, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                                case 1:
                                    // DBAC
                                    Globals.CurrentGame.PlacePeg(colorD, 0);
                                    Globals.CurrentGame.PlacePeg(colorB, 1);
                                    Globals.CurrentGame.PlacePeg(colorA, 2);
                                    Globals.CurrentGame.PlacePeg(colorC, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                                case 2:
                                    // ACDB
                                    Globals.CurrentGame.PlacePeg(colorA, 0);
                                    Globals.CurrentGame.PlacePeg(colorC, 1);
                                    Globals.CurrentGame.PlacePeg(colorD, 2);
                                    Globals.CurrentGame.PlacePeg(colorB, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                                case 3:
                                    // ABDC
                                    Globals.CurrentGame.PlacePeg(colorA, 0);
                                    Globals.CurrentGame.PlacePeg(colorB, 1);
                                    Globals.CurrentGame.PlacePeg(colorD, 2);
                                    Globals.CurrentGame.PlacePeg(colorC, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                            }
                            break;
                        case 2:
                            switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
                            {
                                case 0:
                                    // CDAB
                                    Globals.CurrentGame.PlacePeg(colorC, 0);
                                    Globals.CurrentGame.PlacePeg(colorD, 1);
                                    Globals.CurrentGame.PlacePeg(colorA, 2);
                                    Globals.CurrentGame.PlacePeg(colorB, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                                case 1:
                                    // ADCB
                                    Globals.CurrentGame.PlacePeg(colorA, 0);
                                    Globals.CurrentGame.PlacePeg(colorD, 1);
                                    Globals.CurrentGame.PlacePeg(colorC, 2);
                                    Globals.CurrentGame.PlacePeg(colorB, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                                case 2:
                                    // CBAD
                                    Globals.CurrentGame.PlacePeg(colorC, 0);
                                    Globals.CurrentGame.PlacePeg(colorB, 1);
                                    Globals.CurrentGame.PlacePeg(colorA, 2);
                                    Globals.CurrentGame.PlacePeg(colorD, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                                case 3:
                                    // ABCD
                                    Globals.CurrentGame.PlacePeg(colorA, 0);
                                    Globals.CurrentGame.PlacePeg(colorB, 1);
                                    Globals.CurrentGame.PlacePeg(colorC, 2);
                                    Globals.CurrentGame.PlacePeg(colorD, 3);
                                    if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                    {
                                        Debug.WriteLine($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                        return;
                                    }
                                    Debug.Write($" / {Globals.CurrentGame.LastTurn().CorrectlyPlaced}");
                                    break;
                            }
                            break;
                    }
                    break;
            }
            Debug.WriteLine(" ");
        }

    }
}
