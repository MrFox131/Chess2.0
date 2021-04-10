using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess2._0
{
    partial class Board
    {
        bool a1, a8, h1, h8, BlackKing, WhiteKing, elPassanteEdited = false;
        Cell capturingEnPassantCell = null;
        Cell capturingEnPassantPiece = null;
        public bool CheckMove(Cell fcell, Cell scell)
        {
            bool answer = false;
            bool color = fcell.State <= State.WhitePawn ? true : false;
            State initialStateOfTargetCell = scell.State;
            scell.State = fcell.State;
            fcell.State = State.Empty;
            Pair <int, int> KingCoordinates = new Pair<int,int>(0,0);
            for(int i=0; i<8; i++)
            {
                for(int j=0; j<8; j++)
                {
                    if (color)
                    {
                        if (_area[i, j].State == State.WhiteKing)
                        {
                            KingCoordinates = new Pair<int, int>(i, j);
                            break;
                        }
                    } else
                    {
                        if(_area[i,j].State == State.BlackKing)
                        {
                            KingCoordinates = new Pair<int, int>(i, j);
                            break;
                        }
                    }
                }
            }
            //Pawn checks
            if (color)
            {
                if (KingCoordinates.First + 1 < 8)
                {
                    if (KingCoordinates.Second - 1 >= 0)
                    {
                        if (_area[KingCoordinates.First+1, KingCoordinates.Second - 1].State == State.BlackPawn)
                        {
                            fcell.State = scell.State;
                            scell.State = initialStateOfTargetCell;
                            return false;
                        }
                    }
                    if (KingCoordinates.Second + 1 < 8)
                    {
                        if (_area[KingCoordinates.First + 1, KingCoordinates.Second + 1].State == State.BlackPawn)
                        {
                            fcell.State = scell.State;
                            scell.State = initialStateOfTargetCell;
                            return false;
                        }
                    }
                }
            } 
            else
            {
                if (KingCoordinates.First - 1 > 0)
                {
                    if (KingCoordinates.Second - 1 >= 0)
                    {
                        if (_area[KingCoordinates.First - 1, KingCoordinates.Second - 1].State == State.WhitePawn)
                        {
                            fcell.State = scell.State;
                            scell.State = initialStateOfTargetCell;
                            return false;
                        }
                    }
                    if (KingCoordinates.Second + 1 < 8)
                    {
                        if (_area[KingCoordinates.First - 1, KingCoordinates.Second + 1].State == State.WhitePawn)
                        {
                            fcell.State = scell.State;
                            scell.State = initialStateOfTargetCell;
                            return false;
                        }
                    }
                }
            }
            //Knights checks
            List<int> twos = new List<int>();
            twos.Add(2);
            twos.Add(-2);
            List<int> ones = new List<int>();
            ones.Add(1);
            ones.Add(-1);
            if (color)
            {
                foreach(int i in twos)
                {
                    foreach(int j in ones)
                    {
                        if (KingCoordinates.First + i>=0 && KingCoordinates.First + i < 8 && KingCoordinates.Second + j>=0 && KingCoordinates.Second + j < 8 && _area[KingCoordinates.First+i, KingCoordinates.Second + j].State == State.BlackKnight)
                        {
                            fcell.State = scell.State;
                            scell.State = initialStateOfTargetCell;
                            return false;
                        }
                    }
                }
            }
            else
            {
                foreach (int i in twos)
                {
                    foreach (int j in ones)
                    {
                        if (KingCoordinates.First + i >= 0 && KingCoordinates.First + i < 8 && KingCoordinates.Second + j >= 0 && KingCoordinates.Second + j < 8 && _area[KingCoordinates.First + i, KingCoordinates.Second + j].State == State.WhiteKnight)
                        {
                            fcell.State = scell.State;
                            scell.State = initialStateOfTargetCell;
                            return false;
                        }
                    }
                }
            }
            //horizontal checks
            List<State> linearChecksForWhite = new List<State>();
            linearChecksForWhite.Add(State.BlackRook);
            linearChecksForWhite.Add(State.BlackQueen);
            List<State> linearChecksForBlack = new List<State>();
            linearChecksForBlack.Add(State.WhiteRook);
            linearChecksForBlack.Add(State.WhiteQueen);
            for (int i = KingCoordinates.Second-1; i>=0; i--)
            {
                if (color)
                {
                    
                    if(linearChecksForWhite.Contains(_area[KingCoordinates.First, i].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                } else
                {
                    if (linearChecksForBlack.Contains(_area[KingCoordinates.First, i].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                if (_area[KingCoordinates.First, i].State != State.Empty)
                {
                    break;
                }
            }
            for (int i = KingCoordinates.Second+1; i < 8; i++)
            {
                if (color)
                {

                    if (linearChecksForWhite.Contains(_area[KingCoordinates.First, i].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                else
                {
                    if (linearChecksForBlack.Contains(_area[KingCoordinates.First, i].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                if (_area[KingCoordinates.First, i].State != State.Empty)
                {
                    break;
                }
            }
            //vertical checks
            for (int i = KingCoordinates.First - 1; i >= 0; i--)
            {
                if (color)
                {

                    if (linearChecksForWhite.Contains(_area[i, KingCoordinates.Second].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                else
                {
                    if (linearChecksForBlack.Contains(_area[i, KingCoordinates.Second].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                if (_area[i, KingCoordinates.Second].State != State.Empty)
                {
                    break;
                }
            }
            for (int i = KingCoordinates.First + 1; i < 8; i++)
            {
                if (color)
                {

                    if (linearChecksForWhite.Contains(_area[i, KingCoordinates.Second].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                else
                {
                    if (linearChecksForBlack.Contains(_area[i, KingCoordinates.Second].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                if (_area[i, KingCoordinates.Second].State != State.Empty)
                {
                    break;
                }
            }
            //Diagonal checks
            List<State> diagonalChecksForWhite = new List<State>();
            diagonalChecksForWhite.Add(State.BlackBishop);
            diagonalChecksForWhite.Add(State.BlackQueen);
            List<State> diagonalChecksForBlack = new List<State>();
            diagonalChecksForBlack.Add(State.WhiteBishop);
            diagonalChecksForBlack.Add(State.WhiteQueen);
            for(Pair<int,int> diagonal = new Pair<int, int>(KingCoordinates.First+1, KingCoordinates.Second+1); diagonal.First<8 && diagonal.Second<8; diagonal.First++, diagonal.Second++)
            {
                if (color)
                {
                    if (diagonalChecksForWhite.Contains(_area[diagonal.First, diagonal.Second].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                else
                {
                    if (diagonalChecksForBlack.Contains(_area[diagonal.First, diagonal.Second].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                if(_area[diagonal.First, diagonal.Second].State != State.Empty)
                {
                    break;
                }
            }
            for (Pair<int, int> diagonal = new Pair<int, int>(KingCoordinates.First + 1, KingCoordinates.Second - 1); diagonal.First < 8 && diagonal.Second >= 0; diagonal.First++, diagonal.Second--)
            {
                if (color)
                {
                    if (diagonalChecksForWhite.Contains(_area[diagonal.First, diagonal.Second].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                else
                {
                    if (diagonalChecksForBlack.Contains(_area[diagonal.First, diagonal.Second].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                if (_area[diagonal.First, diagonal.Second].State != State.Empty)
                {
                    break;
                }
            }
            for (Pair<int, int> diagonal = new Pair<int, int>(KingCoordinates.First - 1, KingCoordinates.Second + 1); diagonal.First >= 0 && diagonal.Second < 8; diagonal.First--, diagonal.Second++)
            {
                if (color)
                {
                    if (diagonalChecksForWhite.Contains(_area[diagonal.First, diagonal.Second].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                else
                {
                    if (diagonalChecksForBlack.Contains(_area[diagonal.First, diagonal.Second].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                if (_area[diagonal.First, diagonal.Second].State != State.Empty)
                {
                    break;
                }
            }
            for (Pair<int, int> diagonal = new Pair<int, int>(KingCoordinates.First - 1, KingCoordinates.Second - 1); diagonal.First >=0 && diagonal.Second >=0; diagonal.First--, diagonal.Second--)
            {
                if (color)
                {
                    if (diagonalChecksForWhite.Contains(_area[diagonal.First, diagonal.Second].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                else
                {
                    if (diagonalChecksForBlack.Contains(_area[diagonal.First, diagonal.Second].State))
                    {
                        fcell.State = scell.State;
                        scell.State = initialStateOfTargetCell;
                        return false;
                    }
                }
                if (_area[diagonal.First, diagonal.Second].State != State.Empty)
                {
                    break;
                }
            }
            fcell.State = scell.State;
            scell.State = initialStateOfTargetCell;
            switch (fcell.State) {
                case State.BlackBishop:
                    answer = BishopChecker(fcell, scell);
                    break;
                case State.BlackKing:
                    answer = KingChecker(fcell, scell);
                    break;
                case State.BlackKnight:
                    answer = KnightChecker(fcell, scell);
                    break;
                case State.BlackPawn:
                    answer = PawnChecker(fcell, scell);
                    break;
                case State.BlackQueen:
                    answer = QueenChecker(fcell, scell);
                    break;
                case State.BlackRook:
                    answer = RookChecker(fcell, scell);
                    break;
                case State.WhiteBishop:
                    answer = BishopChecker(fcell, scell);
                    break;
                case State.WhiteKing:
                    answer = KingChecker(fcell, scell);
                    break;
                case State.WhiteKnight:
                    answer = KnightChecker(fcell, scell);
                    break;
                case State.WhitePawn:
                    answer = PawnChecker(fcell, scell);
                    break;
                case State.WhiteQueen:
                    answer = QueenChecker(fcell, scell);
                    break;
                case State.WhiteRook:
                    answer = RookChecker(fcell, scell);
                    break;
            }
            if (!elPassanteEdited)
            {
                capturingEnPassantPiece = null;
                capturingEnPassantCell = null;
            }
            elPassanteEdited = false;
            return answer;
        }
        bool PawnChecker(Cell fcell, Cell scell)
        {
            Pair<int, int> fcoords = GetCoordinates(fcell);
            Pair<int, int> scoords = GetCoordinates(scell);
            if (Math.Abs(scoords.Second - fcoords.Second) > 1)
            {
                return false;
            }
            if (fcell.State == State.WhitePawn)
            {
                if(Math.Abs(scoords.Second - fcoords.Second) == 1)
                {
                    if (scoords.First - fcoords.First == 1)
                    {
                        if (scell.State >= State.BlackKing || capturingEnPassantPiece!=null && capturingEnPassantPiece.State>= State.BlackKing && capturingEnPassantCell == scell)
                        {
                            if(capturingEnPassantPiece!=null && capturingEnPassantCell == scell)
                                capturingEnPassantPiece.State = State.Empty;
                            return true;
                        }
                    } 
                } else if (scoords.First - fcoords.First == 1 && scell.State == State.Empty)
                {
                    return true;
                } else if(scoords.First-fcoords.First == 2 && fcoords.First == 1 && scell.State == State.Empty)
                {
                    elPassanteEdited = true;
                    capturingEnPassantCell = _area[fcoords.First + 1, fcoords.Second];
                    capturingEnPassantPiece = scell;
                    return true;
                }
            } else
            {
                if (Math.Abs(scoords.Second - fcoords.Second) == 1)
                {
                    if (scoords.First - fcoords.First == -1)
                    {
                        if (scell.State <= State.WhitePawn || capturingEnPassantPiece != null && capturingEnPassantPiece.State <= State.WhitePawn && capturingEnPassantCell == scell)
                        {
                            if(capturingEnPassantPiece != null && capturingEnPassantCell == scell)
                            {
                                capturingEnPassantPiece.State = State.Empty;
                            }
                            return true;
                        }
                    }
                }
                else if (scoords.First - fcoords.First == -1 && scell.State == State.Empty)
                {
                    return true;
                }
                else if (scoords.First - fcoords.First == -2 && fcoords.First == 6 && scell.State == State.Empty)
                {
                    elPassanteEdited = true;
                    capturingEnPassantCell = _area[fcoords.First - 1, fcoords.Second];
                    capturingEnPassantPiece = scell;
                    return true;
                }
            }
            return false;
        }
        bool RookChecker(Cell fcell, Cell scell)
        {
            Pair<int, int> fcoords = GetCoordinates(fcell);
            Pair<int, int> scoords = GetCoordinates(scell);
            if (fcoords.First == 0 && fcoords.Second == 0)
            {
                h1 = true;
            } else if(fcoords.First == 0 && fcoords.Second == 7)
            {
                a1 = true;
            } else if(fcoords.First == 7 && fcoords.Second == 0)
            {
                h8 = true;
            } else if(fcoords.First == 7 && fcoords.Second == 7)
            {
                h1 = true;
            }
            if (fcoords.First != scoords.First && scoords.Second != fcoords.Second)
            {
                return false;
            }
            if (scell.State!=State.Empty && (fcell.State <= State.WhitePawn && scell.State <= State.WhitePawn || fcell.State >= State.BlackKing && scell.State >= State.BlackKing))
            {
                return false;
            }
            if (fcoords.First == scoords.First)
            {
                for(int i=fcoords.Second+ (scoords.Second - fcoords.Second) / Math.Abs(scoords.Second - fcoords.Second); Math.Abs(i-scoords.Second)>0; i+=(scoords.Second-fcoords.Second)/Math.Abs(scoords.Second - fcoords.Second))
                {
                    if (_area[fcoords.First, i].State != State.Empty)
                    {
                        return false;
                    }
                }
            }
            if (fcoords.Second == scoords.Second)
            {
                for (int i = fcoords.First+ (scoords.First - fcoords.First) / Math.Abs(scoords.First - fcoords.First); Math.Abs(i - scoords.First)>0; i += (scoords.First - fcoords.First) / Math.Abs(scoords.First - fcoords.First))
                {
                    if (_area[i, fcoords.Second].State != State.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        bool KnightChecker(Cell fcell, Cell scell)
        {
            Pair<int, int> fcoords = GetCoordinates(fcell);
            Pair<int, int> scoords = GetCoordinates(scell);
            Pair<int, int> theDifference = new Pair<int, int>(Math.Abs(fcoords.First - scoords.First), Math.Abs(fcoords.Second - scoords.Second));
            if (theDifference.First == 2 && theDifference.Second==1 || theDifference.First==1 && theDifference.Second == 2)
            {
                if (scell.State == State.Empty)
                {
                    return true;
                }
                if(fcell.State<=State.WhitePawn && scell.State >= State.BlackKing || scell.State <= State.WhitePawn && fcell.State >= State.BlackKing)
                {
                    return true;
                }
            }
            return false;
        }
        bool BishopChecker(Cell fcell, Cell scell)
        {
            Pair<int, int> fcoords = GetCoordinates(fcell);
            Pair<int, int> scoords = GetCoordinates(scell);
            if(scell.State != State.Empty && (fcell.State <= State.WhitePawn && scell.State <= State.WhitePawn || fcell.State >= State.BlackKing && scell.State >= State.BlackKing))
            {
                return false;
            }
            int firstCoordinateModifier;
            int secondCoordinateModifier;
            try { 
            firstCoordinateModifier = (scoords.First - fcoords.First) / Math.Abs(scoords.First - fcoords.First);
            secondCoordinateModifier = (scoords.Second - fcoords.Second) / Math.Abs(scoords.Second - fcoords.Second);
            }
            catch(DivideByZeroException e)
            {
                return false;
            }
            fcoords.First += firstCoordinateModifier;
            fcoords.Second += secondCoordinateModifier;
            for(;Math.Abs(fcoords.First-scoords.First)>0 && Math.Abs(fcoords.Second-scoords.Second)>0;)
            {
                if(_area[fcoords.First, fcoords.Second].State != State.Empty)
                {
                    return false;
                }
                fcoords.First += firstCoordinateModifier;
                fcoords.Second += secondCoordinateModifier;
            }
            return true;
        }
        bool QueenChecker(Cell fcell, Cell scell)
        {
            Pair<int, int> fcoords = GetCoordinates(fcell);
            Pair<int, int> scoords = GetCoordinates(scell);
            if (scell.State != State.Empty && (fcell.State <= State.WhitePawn && scell.State <= State.WhitePawn || fcell.State >= State.BlackKing && scell.State >= State.BlackKing))
            {
                return false;
            }
            for (int i = fcoords.First - 1; i>=0; i--)
            {
                if (_area[i, fcoords.Second] == scell)
                {
                    return true;
                }
                if (_area[i, fcoords.Second].State != State.Empty)
                {
                    break;
                }
            }
            for (int i = fcoords.First + 1; i < 8; i++)
            {
                if (_area[i, fcoords.Second] == scell)
                {
                    return true;
                }
                if (_area[i, fcoords.Second].State != State.Empty)
                {
                    break;
                }
            }
            for(Pair<int, int> i = new Pair<int, int>(fcoords.First+1, fcoords.Second+1); i.First < 8 && i.Second < 8; i.First++, i.Second++)
            {
                if(_area[i.First, i.Second] == scell)
                {
                    return true;
                }
                if(_area[i.First, i.Second].State != State.Empty)
                {
                    break;
                }
            }
            for (Pair<int, int> i = new Pair<int, int>(fcoords.First + 1, fcoords.Second - 1); i.First < 8 && i.Second >= 0; i.First++, i.Second--)
            {
                if (_area[i.First, i.Second] == scell)
                {
                    return true;
                }
                if (_area[i.First, i.Second].State != State.Empty)
                {
                    break;
                }
            }
            for (Pair<int, int> i = new Pair<int, int>(fcoords.First - 1, fcoords.Second + 1); i.First >= 0 && i.Second < 8; i.First--, i.Second++)
            {
                if (_area[i.First, i.Second] == scell)
                {
                    return true;
                }
                if (_area[i.First, i.Second].State != State.Empty)
                {
                    break;
                }
            }
            for (Pair<int, int> i = new Pair<int, int>(fcoords.First - 1, fcoords.Second - 1); i.First >= 0 && i.Second >= 0; i.First--, i.Second--)
            {
                if (_area[i.First, i.Second] == scell)
                {
                    return true;
                }
                if (_area[i.First, i.Second].State != State.Empty)
                {
                    break;
                }
            }

            return false;
        }
        bool KingChecker(Cell fcell, Cell scell)
        {
            Pair<int, int> fcoords = GetCoordinates(fcell);
            Pair<int, int> scoords = GetCoordinates(scell);
            Pair<int, int> theDifference = new Pair<int, int>(Math.Abs(fcoords.First - scoords.First), Math.Abs(fcoords.Second - scoords.Second));
            if (scell.State != State.Empty && (fcell.State <= State.WhitePawn && scell.State <= State.WhitePawn || fcell.State >= State.BlackKing && scell.State >= State.BlackKing))
            {
                return false;
            }
            if(Math.Abs(theDifference.First)>1 || Math.Abs(theDifference.Second) > 1)
            {
                return false;
            }
            if (fcell.State == State.BlackKing)
            {
                BlackKing = true;
            }
            else
            {
                WhiteKing = true;
            }
            return true;
        }
        Pair<int, int> GetCoordinates(Cell cell)
        {
            Pair<int, int> coords = new Pair<int,int>(0, 0);
            int i, j=0;
            bool found = false;
            for(i=0; i<8; i++)
            {
                for(j=0; j<8; j++)
                {
                    if(_area[i, j] == cell)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }
            coords.First = i;
            coords.Second = j;
            return coords;
        }
    }
}
