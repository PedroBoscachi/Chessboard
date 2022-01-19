using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.BoardFolder.Entities;
using Xadrez.BoardFolder.Entities.Enums;

namespace Xadrez.GameFolder.Entities
{
    class Horse : Piece
    {
        public Horse(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "H";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.PositionPiece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] matriz = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            pos.SetValues(Position.Line - 1, Position.Column - 2);
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
            }
            pos.SetValues(Position.Line - 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
            }
            pos.SetValues(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
            }
            pos.SetValues(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
            }
            pos.SetValues(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
            }
            pos.SetValues(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
            }
            pos.SetValues(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
            }
            pos.SetValues(Position.Line + 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
            }

            return matriz;
        }
    }
}
