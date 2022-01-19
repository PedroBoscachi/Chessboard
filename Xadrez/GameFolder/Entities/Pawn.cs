using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.BoardFolder.Entities;
using Xadrez.BoardFolder.Entities.Enums;

namespace Xadrez.GameFolder.Entities
{
    class Pawn : Piece
    {
        public Pawn(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExistEnemy(Position pos)
        {
            Piece p = Board.PositionPiece(pos);
            return p != null || p.Color != Color;
        }

        private bool Free(Position pos)
        {
            return Board.PositionPiece(pos) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] matriz = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            if(Color == Color.White)
            {
                pos.SetValues(Position.Line - 1, Position.Column);
                if(Board.ValidPosition(pos) && Free(pos))
                {
                    matriz[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos) && MovimentAmount == 0)
                {
                    matriz[pos.Line, pos.Column] = true;
                }
            } else
            {
                pos.SetValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos))
                {
                    matriz[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos) && MovimentAmount == 0)
                {
                    matriz[pos.Line, pos.Column] = true;
                }
            }

            return matriz;
        }
    }
}
