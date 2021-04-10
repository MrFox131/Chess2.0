using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess2._0
{
    partial class Board : IEnumerable<Cell>
    {
        private readonly Cell[,] _area;

        public State this[int row, int column]
        {
            get => _area[row, column].State;
            set => _area[row, column].State = value;
        }

        public Board()
        {
            _area = new Cell[8,8];
            for(int i=0; i<8; i++)
            {
                for(int j=0; j< 8; j++)
                {
                    _area[i, j] = new Cell();
                }
            }
        }
        public IEnumerator<Cell> GetEnumerator()
        {
            return _area.Cast<Cell>().Reverse<Cell>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _area.Cast<object>().Reverse<object>().GetEnumerator();
        }
    }
}
