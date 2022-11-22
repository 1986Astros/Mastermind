using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace MasterMind
{
    internal class ComputerPlayer
    {
        public ComputerPlayer(string playerName) : base()
        {
            PlayerName = playerName;
        }
        public string PlayerName { get; set; } = "";
        private int[] ColorIndices = new int[6];
        public CurrentGame CurrentGame;

        const bool Testing = false;

        public void Solve()
        {
            if (Testing)
            {
                for (int i = 0; i < 6; i++)
                {
                    ColorIndices[i] = i;
                }
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
                    PlayRenaldo();
                    break;
                case "Úrsula":
                    PlayÚrsula();
                    break;
                case "Andrés":
                    PlayAndrés();
                    break;
                case "Tati":
                    PlayTati();
                    break;
                case "Pepito":
                    break;
            }
            CurrentGame = Globals.CurrentGame;
            //Globals.CurrentGame = new CurrentGame(CurrentGame.Pattern);
        }

        public void PlayRenaldo()
        {
            Renaldo renaldo = new Renaldo(new List<int>(ColorIndices));
            renaldo.Solve();
        }

        public  void PlayÚrsula()
        {

        }

        public void PlayAndrés()
        {
            Andrés andrés = new Andrés(new List<int>(ColorIndices));
            andrés.Solve();
        }

       // this is code that may have useful parts for another player
#if false
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
#endif


        private void PlayTati()
        {
            Tati tati = new Tati(new List<int>(ColorIndices));
            tati.Solve();
        }

        abstract private class EspookyPlayer
        {
            public EspookyPlayer(List<int> ColorIndices) : base()
            {
                this.ColorIndices = ColorIndices;
            }
            protected List<int> ColorIndices { get; }
            protected List<int> colors;

            abstract protected bool FindColors();
            public void Solve()
            {
                if (FindColors())
                {
                    return; // solved while looking for colors
                }

                if (Globals.CurrentGame.Turns.Count >= 10)
                {
                    return;
                }

                switch (colors.Count(c => c > 0))
                {
                    case 2:
                        // either a 3/1 split or 2/2 split
                        if (colors.Any(c => c == 3))
                        {
                            Solve_3_1();
                        }
                        else
                        {
                            Solve_2_2();
                        }
                        break;
                    case 3:
                        // pair of one color and two singletons
                        Solve_2_1_1();
                        break;
                    case 4:
                        // all singletons
                        Solve_1_1_1_1();
                        break;
                }
            }

            protected virtual void Solve_3_1()
            {
                // guaranteed min 2, max 3 turns to solve
                int color3 = colors.IndexOf(colors.First(mm => mm == 3));
                int color1 = colors.IndexOf(colors.First(mm => mm == 1));

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
                        if (Globals.CurrentGame.Turns.Count >= 10)
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
                        break;
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
                        if (Globals.CurrentGame.Turns.Count >= 10)
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
                        break;
                }
            }
            protected virtual void Solve_2_2()
            {
                // guaranteed min 3, max 4 turns to solve
                int colorA = colors.IndexOf(colors.First(mm => mm == 2));
                int colorB = colors.IndexOf(colors.Skip(colorA + 1).First(mm => mm == 2),colorA + 1);

                Globals.CurrentGame.PlacePeg(colorA, 0);
                Globals.CurrentGame.PlacePeg(colorA, 1);
                Globals.CurrentGame.PlacePeg(colorA, 2);
                Globals.CurrentGame.PlacePeg(colorB, 3);
                Globals.CurrentGame.EvaluateTurn(); // this will have 3 correct
                if (Globals.CurrentGame.Turns.Count >= 10)
                {
                    return;
                }
                switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
                {
                    case 1:
                        // pos 3 == color1
                        Globals.CurrentGame.PlacePeg(colorB, 0);
                        Globals.CurrentGame.PlacePeg(colorA, 1);
                        Globals.CurrentGame.PlacePeg(colorA, 2);
                        Globals.CurrentGame.PlacePeg(colorA, 3);
                        Globals.CurrentGame.EvaluateTurn(); // this will have 3 correct
                        if (Globals.CurrentGame.Turns.Count >= 10)
                        {
                            return;
                        }
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
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
                                    return;
                                }
                                break;
                            case 3:
                                int i = Globals.rnd.Next(1, 3);
                                // pos 0 == color2, pos 3 == color1
                                Globals.CurrentGame.PlacePeg(colorB, 0);
                                Globals.CurrentGame.PlacePeg(colorB, i);
                                Globals.CurrentGame.PlacePeg(colorA, 3 - i);
                                Globals.CurrentGame.PlacePeg(colorA, 3);
                                if (Globals.CurrentGame.EvaluateTurn())
                                {
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
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
                                if (Globals.CurrentGame.Turns.Count >= 10)
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
                        if (Globals.CurrentGame.Turns.Count >= 10)
                        {
                            return;
                        }
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
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
                                    return;
                                }
                                break;
                            case 2:
                                // pos 0 == color1, pos 3 == color2
                                int i = Globals.rnd.Next(1, 3);
                                Globals.CurrentGame.PlacePeg(colorA, 0);
                                Globals.CurrentGame.PlacePeg(colorA, i);
                                Globals.CurrentGame.PlacePeg(colorB, 3 - i);
                                Globals.CurrentGame.PlacePeg(colorB, 3);
                                if (Globals.CurrentGame.EvaluateTurn())
                                {
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
                                    return;
                                }
                                Globals.CurrentGame.PlacePeg(colorA, 0);
                                Globals.CurrentGame.PlacePeg(colorB, i);
                                Globals.CurrentGame.PlacePeg(colorA, 3 - i);
                                Globals.CurrentGame.PlacePeg(colorB, 3);
                                if (Globals.CurrentGame.EvaluateTurn())
                                {
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
                                    return;
                                }
                                break;
                        }
                        break;
                }
            }
            protected virtual void Solve_2_1_1()
            {
                // guaranteed min 3, max 4 turns to solve
                int color2 = colors.IndexOf(colors.First(mm => mm == 2));
                int colorA = colors.IndexOf(colors.First(mm => mm == 1));
                int colorB = colors.IndexOf(colors.Skip(colorA + 1).First(mm => mm == 1), colorA + 1);

                Globals.CurrentGame.PlacePeg(colorA, 0);
                Globals.CurrentGame.PlacePeg(colorA, 1);
                Globals.CurrentGame.PlacePeg(colorB, 2);
                Globals.CurrentGame.PlacePeg(colorB, 3);
                Globals.CurrentGame.EvaluateTurn(); // this will have 3 correct
                if (Globals.CurrentGame.Turns.Count >= 10)
                {
                    return;
                }
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
                        if (Globals.CurrentGame.Turns.Count >= 10)
                        {
                            return;
                        }
                        switch (Globals.CurrentGame.LastTurn().CorrectlyPlaced)
                        {
                            case 1:
                                Globals.CurrentGame.PlacePeg(colorB, 0);
                                Globals.CurrentGame.PlacePeg(color2, 1);
                                Globals.CurrentGame.PlacePeg(color2, i);
                                Globals.CurrentGame.PlacePeg(colorA, 5 - i);
                                if (Globals.CurrentGame.EvaluateTurn())
                                {
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
                                    return;
                                }
                                Globals.CurrentGame.PlacePeg(colorB, 0);
                                Globals.CurrentGame.PlacePeg(color2, 1);
                                Globals.CurrentGame.PlacePeg(colorA, i);
                                Globals.CurrentGame.PlacePeg(color2, 5 - i);
                                if (Globals.CurrentGame.EvaluateTurn())
                                {
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
                                    return;
                                }
                                break;
                            case 3:
                                Globals.CurrentGame.PlacePeg(color2, 0);
                                Globals.CurrentGame.PlacePeg(colorB, 1);
                                Globals.CurrentGame.PlacePeg(color2, i);
                                Globals.CurrentGame.PlacePeg(colorA, 5 - i);
                                if (Globals.CurrentGame.EvaluateTurn())
                                {
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
                                    return;
                                }
                                Globals.CurrentGame.PlacePeg(color2, 0);
                                Globals.CurrentGame.PlacePeg(colorB, 1);
                                Globals.CurrentGame.PlacePeg(colorA, i);
                                Globals.CurrentGame.PlacePeg(color2, 5 - i);
                                if (Globals.CurrentGame.EvaluateTurn())
                                {
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
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
                        if (Globals.CurrentGame.Turns.Count >= 10)
                        {
                            return;
                        }
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
                                if (Globals.CurrentGame.Turns.Count >= 10)
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
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
                                    return;
                                }
                                break;
                            case 2:
                                Globals.CurrentGame.PlacePeg(color2, 0);
                                Globals.CurrentGame.PlacePeg(color2, 1);
                                Globals.CurrentGame.PlacePeg(colorA, i);
                                Globals.CurrentGame.PlacePeg(colorB, 5 - i);
                                if (Globals.CurrentGame.EvaluateTurn())
                                {
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
                                    return;
                                }
                                Globals.CurrentGame.PlacePeg(color2, 0);
                                Globals.CurrentGame.PlacePeg(color2, 1);
                                Globals.CurrentGame.PlacePeg(colorB, i);
                                Globals.CurrentGame.PlacePeg(colorA, 5 - i);
                                if (Globals.CurrentGame.EvaluateTurn())
                                {
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
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
                        if (Globals.CurrentGame.Turns.Count >= 10)
                        {
                            return;
                        }
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
                                if (Globals.CurrentGame.Turns.Count >= 10)
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
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
                                    return;
                                }
                                break;
                            case 1:
                                Globals.CurrentGame.PlacePeg(color2, 0);
                                Globals.CurrentGame.PlacePeg(colorA, 1);
                                Globals.CurrentGame.PlacePeg(color2, i);
                                Globals.CurrentGame.PlacePeg(colorB, 5 - i);
                                if (Globals.CurrentGame.EvaluateTurn())
                                {
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
                                    return;
                                }
                                Globals.CurrentGame.PlacePeg(color2, 0);
                                Globals.CurrentGame.PlacePeg(colorA, 1);
                                Globals.CurrentGame.PlacePeg(colorB, i);
                                Globals.CurrentGame.PlacePeg(color2, 5 - i);
                                if (Globals.CurrentGame.EvaluateTurn())
                                {
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
                                    return;
                                }
                                break;
                            case 2:
                                Globals.CurrentGame.PlacePeg(colorA, 0);
                                Globals.CurrentGame.PlacePeg(color2, 1);
                                Globals.CurrentGame.PlacePeg(color2, i);
                                Globals.CurrentGame.PlacePeg(colorB, 5 - i);
                                if (Globals.CurrentGame.EvaluateTurn())
                                {
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
                                    return;
                                }
                                Globals.CurrentGame.PlacePeg(colorA, 0);
                                Globals.CurrentGame.PlacePeg(color2, 1);
                                Globals.CurrentGame.PlacePeg(colorB, 1);
                                Globals.CurrentGame.PlacePeg(color2, 5 - 1);
                                if (Globals.CurrentGame.EvaluateTurn())
                                {
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
                                    return;
                                }
                                break;
                        }
                        break;
                }
            }

            protected virtual void Solve_1_1_1_1()
            {
                // min 3 turns to win, max 4, always succeeds in solving
                List<int> colorsUsed = new List<int>(Enumerable.Range(0, 6).Where(i => colors[i] == 1).Select(i => i));
                int colorA = colorsUsed[0];
                int colorB = colorsUsed[1];
                int colorC = colorsUsed[2];
                int colorD = colorsUsed[3];

                Globals.CurrentGame.PlacePeg(colorA, 0);
                Globals.CurrentGame.PlacePeg(colorB, 1);
                Globals.CurrentGame.PlacePeg(colorA, 2);
                Globals.CurrentGame.PlacePeg(colorB, 3);
                Globals.CurrentGame.EvaluateTurn();     // guaranteed to fail
                if (Globals.CurrentGame.Turns.Count >= 10)
                {
                    return;
                }
                int FirstCorrectCount = Globals.CurrentGame.LastTurn().CorrectlyPlaced;
                if (FirstCorrectCount == 1)
                {
                    // ACBA
                    Globals.CurrentGame.PlacePeg(colorA, 0);
                    Globals.CurrentGame.PlacePeg(colorC, 1);
                    Globals.CurrentGame.PlacePeg(colorB, 2);
                    Globals.CurrentGame.PlacePeg(colorA, 3);
                    Globals.CurrentGame.EvaluateTurn();     // guaranteed to fail
                }
                else
                {
                    // CDCD
                    Globals.CurrentGame.PlacePeg(colorC, 0);
                    Globals.CurrentGame.PlacePeg(colorD, 1);
                    Globals.CurrentGame.PlacePeg(colorC, 2);
                    Globals.CurrentGame.PlacePeg(colorD, 3);
                    Globals.CurrentGame.EvaluateTurn();     // guaranteed to fail
                }
                if (Globals.CurrentGame.Turns.Count >= 10)
                {
                    return;
                }
                int SecondCorrectCount = Globals.CurrentGame.LastTurn().CorrectlyPlaced;
                switch (FirstCorrectCount)
                {
                    case 0:
                        // BADD
                        Globals.CurrentGame.PlacePeg(colorB, 0);
                        Globals.CurrentGame.PlacePeg(colorA, 1);
                        Globals.CurrentGame.PlacePeg(colorD, 2);
                        Globals.CurrentGame.PlacePeg(colorD, 3);
                        Globals.CurrentGame.EvaluateTurn();
                        if (Globals.CurrentGame.Turns.Count >= 10)
                        {
                            return;
                        }
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
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
                                        break;
                                    case 1:
                                        // DABC
                                        Globals.CurrentGame.PlacePeg(colorD, 0);
                                        Globals.CurrentGame.PlacePeg(colorA, 1);
                                        Globals.CurrentGame.PlacePeg(colorB, 2);
                                        Globals.CurrentGame.PlacePeg(colorC, 3);
                                        if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                        {
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
                                        break;
                                    case 2:
                                        // BCDA
                                        Globals.CurrentGame.PlacePeg(colorB, 0);
                                        Globals.CurrentGame.PlacePeg(colorC, 1);
                                        Globals.CurrentGame.PlacePeg(colorD, 2);
                                        Globals.CurrentGame.PlacePeg(colorA, 3);
                                        if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                        {
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
                                        break;
                                    case 3:
                                        // BADC
                                        Globals.CurrentGame.PlacePeg(colorB, 0);
                                        Globals.CurrentGame.PlacePeg(colorA, 1);
                                        Globals.CurrentGame.PlacePeg(colorD, 2);
                                        Globals.CurrentGame.PlacePeg(colorC, 3);
                                        if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                        {
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
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
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
                                        break;
                                    case 1:
                                        // BDCA
                                        Globals.CurrentGame.PlacePeg(colorB, 0);
                                        Globals.CurrentGame.PlacePeg(colorD, 1);
                                        Globals.CurrentGame.PlacePeg(colorC, 2);
                                        Globals.CurrentGame.PlacePeg(colorA, 3);
                                        if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                        {
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
                                        break;
                                    case 2:
                                        // CABD
                                        Globals.CurrentGame.PlacePeg(colorC, 0);
                                        Globals.CurrentGame.PlacePeg(colorA, 1);
                                        Globals.CurrentGame.PlacePeg(colorB, 2);
                                        Globals.CurrentGame.PlacePeg(colorD, 3);
                                        if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                        {
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
                                        break;
                                    case 3:
                                        // BACD
                                        Globals.CurrentGame.PlacePeg(colorB, 0);
                                        Globals.CurrentGame.PlacePeg(colorA, 1);
                                        Globals.CurrentGame.PlacePeg(colorC, 2);
                                        Globals.CurrentGame.PlacePeg(colorD, 3);
                                        if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                        {
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
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
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
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
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
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
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
                                        break;
                                }
                                break;
                            case 1:
                                // CBDA
                                Globals.CurrentGame.PlacePeg(colorC, 0);
                                Globals.CurrentGame.PlacePeg(colorB, 1);
                                Globals.CurrentGame.PlacePeg(colorD, 2);
                                Globals.CurrentGame.PlacePeg(colorA, 3);
                                if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                {
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
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
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
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
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
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
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
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
                                    return;
                                }
                                if (Globals.CurrentGame.Turns.Count >= 10)
                                {
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
                        if (Globals.CurrentGame.Turns.Count >= 10)
                        {
                            return;
                        }
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
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
                                        break;
                                    case 1:
                                        // DBAC
                                        Globals.CurrentGame.PlacePeg(colorD, 0);
                                        Globals.CurrentGame.PlacePeg(colorB, 1);
                                        Globals.CurrentGame.PlacePeg(colorA, 2);
                                        Globals.CurrentGame.PlacePeg(colorC, 3);
                                        if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                        {
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
                                        break;
                                    case 2:
                                        // ACDB
                                        Globals.CurrentGame.PlacePeg(colorA, 0);
                                        Globals.CurrentGame.PlacePeg(colorC, 1);
                                        Globals.CurrentGame.PlacePeg(colorD, 2);
                                        Globals.CurrentGame.PlacePeg(colorB, 3);
                                        if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                        {
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
                                        break;
                                    case 3:
                                        // ABDC
                                        Globals.CurrentGame.PlacePeg(colorA, 0);
                                        Globals.CurrentGame.PlacePeg(colorB, 1);
                                        Globals.CurrentGame.PlacePeg(colorD, 2);
                                        Globals.CurrentGame.PlacePeg(colorC, 3);
                                        if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                        {
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
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
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
                                        break;
                                    case 1:
                                        // ADCB
                                        Globals.CurrentGame.PlacePeg(colorA, 0);
                                        Globals.CurrentGame.PlacePeg(colorD, 1);
                                        Globals.CurrentGame.PlacePeg(colorC, 2);
                                        Globals.CurrentGame.PlacePeg(colorB, 3);
                                        if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                        {
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
                                        break;
                                    case 2:
                                        // CBAD
                                        Globals.CurrentGame.PlacePeg(colorC, 0);
                                        Globals.CurrentGame.PlacePeg(colorB, 1);
                                        Globals.CurrentGame.PlacePeg(colorA, 2);
                                        Globals.CurrentGame.PlacePeg(colorD, 3);
                                        if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                        {
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
                                        break;
                                    case 3:
                                        // ABCD
                                        Globals.CurrentGame.PlacePeg(colorA, 0);
                                        Globals.CurrentGame.PlacePeg(colorB, 1);
                                        Globals.CurrentGame.PlacePeg(colorC, 2);
                                        Globals.CurrentGame.PlacePeg(colorD, 3);
                                        if (Globals.CurrentGame.EvaluateTurn()) // worst case turn 9
                                        {
                                            return;
                                        }
                                        if (Globals.CurrentGame.Turns.Count >= 10)
                                        {
                                            return;
                                        }
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }
        }
        private class Renaldo : EspookyPlayer
        {
            public Renaldo(List<int> ColorIndices) : base(ColorIndices)
            {

            }
            protected override bool FindColors()
            {
                int color = 0;
                colors = new List<int>(Enumerable.Range(0, 6).Select(i => 0));
                do
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Globals.CurrentGame.PlacePeg(ColorIndices[color], i);
                    }
                    if (Globals.CurrentGame.EvaluateTurn())
                    {
                        return true;
                    }
                    colors[ColorIndices[color]] = Globals.CurrentGame.Turns[color].CorrectlyPlaced;
                    if (++color == 5)
                    {
                        colors[ColorIndices[5]] = 4 - colors.Sum();
                    }
                } while (colors.Sum() < 4);
                return false;
            }
        }
        private class Andrés : EspookyPlayer
        {
            public Andrés(List<int> ColorIndices) : base(ColorIndices)
            {

            }
            protected override bool FindColors()
            {
                // find the color index for the bluest color
                // move that index to the front of the ColorIndices so that it's guessed first
                // guess it one extra time for no reason
                int bluestColor = int.MinValue;
                int bluestIndex = -1;
                int blueness;
                for (int colorIndex = 0; colorIndex < 6; colorIndex++)
                {
                    blueness = Globals.ColorsInUse[colorIndex].B - Globals.ColorsInUse[colorIndex].R - Globals.ColorsInUse[colorIndex].G;
                    if (blueness > bluestColor)
                    {
                        bluestColor = blueness;
                        bluestIndex = colorIndex;
                    }
                }
                if (ColorIndices[0] != bluestIndex)
                {
                    int blueIndex = ColorIndices.IndexOf(bluestIndex);
                    ColorIndices[blueIndex] = ColorIndices[0];
                    ColorIndices[0] = bluestIndex;
                }

                int color = 0;
                bool guessedBlueAgain = false;
                colors = new List<int>(Enumerable.Range(0, 6).Select(i => 0));
                do
                {
                    if (color > 1 && !guessedBlueAgain && Globals.rnd.Next(0, 2) == 0)
                    {
                        // guess blue again
                        for (int i = 0; i < 4; i++)
                        {
                            Globals.CurrentGame.PlacePeg(bluestIndex, i);
                        }
                        Globals.CurrentGame.EvaluateTurn();
                        guessedBlueAgain = true;
                    }
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Globals.CurrentGame.PlacePeg(ColorIndices[color], i);
                        }
                        if (Globals.CurrentGame.EvaluateTurn())
                        {
                            return true;
                        }
                        colors[ColorIndices[color]] = Globals.CurrentGame.LastTurn().CorrectlyPlaced;
                        if (++color == 5)
                        {
                            colors[ColorIndices[5]] = 4 - colors.Sum();
                        }
                    }
                } while (colors.Sum() < 4);
                if (!guessedBlueAgain)
                {
                    // guess blue again, "just in case"
                    for (int i = 0; i < 4; i++)
                    {
                        Globals.CurrentGame.PlacePeg(bluestIndex, i);
                    }
                    Globals.CurrentGame.EvaluateTurn();
                    guessedBlueAgain = true;
                }

                return false;
            }
        }
        private class Tati : EspookyPlayer
        {
            public Tati(List<int> ColorIndices) : base(ColorIndices)
            {

            }
            protected override bool FindColors()
            {
                // for Tati time goes in a circle so she knows the colors
                colors = new List<int>(Enumerable.Range(0, 6).Select(c => Globals.CurrentGame.Pattern.Where(p => p == c).Count()));
                for (int i = 0;i < 6; i++)
                {
                    if (colors[i] > 0)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            Globals.CurrentGame.PlacePeg(i, j);
                        }
                        if (Globals.CurrentGame.EvaluateTurn())
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}

#if false
    Úrsula - should be the best player
    Pepito - how would a pervert play the game?
#endif