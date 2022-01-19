using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.BoardFolder.Entities;
using Xadrez.BoardFolder.Entities.Enums;

namespace Xadrez.GameFolder.Entities
{
    class Queen : Piece
    {
        public Queen(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "Q";
        }

        public bool CanMove(Position pos)
        {
            Piece p = Board.PositionPiece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] matriz = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            //esquerda
            pos.SetValues(Position.Line, Position.Column - 1);
            while(Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
                if(Board.PositionPiece(pos) != null && Board.PositionPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line, pos.Column - 1);
            }

            //direita
            pos.SetValues(Position.Line, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
                if (Board.PositionPiece(pos) != null && Board.PositionPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line, pos.Column + 1);
            }

            //acima
            pos.SetValues(Position.Line - 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
                if (Board.PositionPiece(pos) != null && Board.PositionPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line - 1, pos.Column);
            }

            //abaixo
            pos.SetValues(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
                if (Board.PositionPiece(pos) != null && Board.PositionPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line + 1, pos.Column);
            }

            //NO
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
                if (Board.PositionPiece(pos) != null && Board.PositionPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line - 1, pos.Column - 1);
            }

            //NE
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
                if (Board.PositionPiece(pos) != null && Board.PositionPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line - 1, pos.Column + 1);
            }

            //SE
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
                if (Board.PositionPiece(pos) != null && Board.PositionPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line + 1, pos.Column + 1);
            }

            //SO
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
                if (Board.PositionPiece(pos) != null && Board.PositionPiece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line + 1, pos.Column - 1);
            }

            return matriz;
        }
    }
}
