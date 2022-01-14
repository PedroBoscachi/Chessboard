using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez.BoardFolder.Entities
{
    class Board
    {
        public int Line { get; set; }
        public int Column { get; set; }
        private Piece[,] Pieces;

        public Board()
        {
        }

        public Board(int line, int column)
        {
            Line = line;
            Column = column;
            Pieces = new Piece[line, column];
        }

        public Piece PositionPiece(int line, int column)
        {
            return Pieces[line, column];
        }

        public void PlacePiece(Piece piece, Position position)
        {
            Pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }
    }
}
