using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.BoardFolder.Entities;

namespace Xadrez.GameFolder.Entities
{
    class PositionChess
    {
        public char Column { get; set; }
        public int Line { get; set; }

        public PositionChess()
        {
        }

        public PositionChess(char column, int line)
        {
            Column = column;
            Line = line;
        }

        public Position ToPosition()//converte os valores da posição do tabuleiro em posições para a matriz
        {
            return new Position(8 - Line, Column - 'a');//a é lido como um valor inteiro internamente
        }

        public override string ToString()
        {
            return "" + Column + Line;
        }
    }
}
