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
        private ChessMatch Match;
        
        public Pawn(Color color, Board board, ChessMatch match) : base(color, board)
        {
            Match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExistEnemy(Position pos)
        {
            Piece p = Board.PositionPiece(pos);
            return p != null && p.Color != Color;
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
                pos.SetValues(Position.Line - 1, Position.Column - 1);
                if(Board.ValidPosition(pos) && ExistEnemy(pos))
                {
                    matriz[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line - 1, Position.Column + 1);
                if(Board.ValidPosition(pos) && ExistEnemy(pos))
                {
                    matriz[pos.Line, pos.Column] = true;
                }

                // #jogadaespecia en passant
                if(Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if(Board.ValidPosition(left) && ExistEnemy(left) && Board.PositionPiece(left) == Match.VulnerableEnPassant)
                    {
                        matriz[left.Line - 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && ExistEnemy(right) && Board.PositionPiece(right) == Match.VulnerableEnPassant)
                    {
                        matriz[right.Line - 1, right.Column] = true;
                    }
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
                pos.SetValues(Position.Line + 1, Position.Column - 1);
                if(Board.ValidPosition(pos) && ExistEnemy(pos))
                {
                    matriz[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line + 1, Position.Column + 1);
                if(Board.ValidPosition(pos) && ExistEnemy(pos))
                {
                    matriz[pos.Line, pos.Column] = true;
                }

                // #jogadaespecial en passant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && ExistEnemy(left) && Board.PositionPiece(left) == Match.VulnerableEnPassant)
                    {
                        matriz[left.Line + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && ExistEnemy(right) && Board.PositionPiece(right) == Match.VulnerableEnPassant)
                    {
                        matriz[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return matriz;
        }
    }
}
