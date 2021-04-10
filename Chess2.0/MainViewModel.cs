using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chess2._0
{
    partial class MainViewModel : NotifyPropertyChanged
    {
        private bool order;
        private Board _board = new Board();
        private ICommand _newGameCommand;
        private ICommand _clearCommand;
        private ICommand _cellCommand;

        public IEnumerable<Char> Numbers => "87654321";
        public IEnumerable<Char> Letters => "ABCDEFGH";

        public Board Board
        {
            get => _board;
            set
            {
                _board = value;
                OnPropertyChanged();
            }
        }

        public ICommand NewGameCommand => _newGameCommand ??= new RelayCommand(parameter => SetupBoard());
        public ICommand ClearCommand => _clearCommand ??= new RelayCommand(parameter =>
        {
            Board = new Board();
        });
        public ICommand CellCommand => _cellCommand ??= new RelayCommand(parameter =>
        {
            Cell cell = (Cell)parameter;
            Cell activeCell = Board.FirstOrDefault(x => x.Active);
            if (activeCell != null)
            {
                if (Board.CheckMove(activeCell, cell))
                {
                    cell.State = activeCell.State;
                    activeCell.State = State.Empty;
                    order = !order;
                }
                activeCell.Active = false;
            }
            else if (cell.State != State.Empty)
            {
                if(order && cell.State >=State.WhiteKing && cell.State <=State.WhitePawn || !order && cell.State>=State.BlackKing && cell.State<=State.BlackPawn)
                    cell.Active = !cell.Active;
            }
        }, parameter => parameter is Cell cell && (Board.Any(x=>x.Active) ||cell.State!=State.Empty));
        private void SetupBoard()
        {
            order = true;
            Board board = new Board();
            board[0, 0] = State.WhiteRook;
            board[0, 1] = State.WhiteKnight;
            board[0, 2] = State.WhiteBishop;
            board[0, 3] = State.WhiteKing;
            board[0, 4] = State.WhiteQueen;
            board[0, 5] = State.WhiteBishop;
            board[0, 6] = State.WhiteKnight;
            board[0, 7] = State.WhiteRook;
            board[7, 0] = State.BlackRook;
            board[7, 1] = State.BlackKnight;
            board[7, 2] = State.BlackBishop;
            board[7, 3] = State.BlackKing;
            board[7, 4] = State.BlackQueen;
            board[7, 5] = State.BlackBishop;
            board[7, 6] = State.BlackKnight;
            board[7, 7] = State.BlackRook;
            for(int i=0; i<8; i++)
            {
                board[1, i] = State.WhitePawn;
                board[6, i] = State.BlackPawn;
            }
            Board = board;
        }

    }
}
