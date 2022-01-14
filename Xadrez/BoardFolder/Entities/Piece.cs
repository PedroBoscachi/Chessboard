using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.BoardFolder.Entities.Enums;

namespace Xadrez.BoardFolder.Entities
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovimentAmount { get; protected set; }
        public Board Board { get; protected set; }

        public Piece()
        {
        }

        public Piece(Color color, Board board)
        {
            Position =null;
            Color = color;
            Board = board;
            MovimentAmount = 0;
        }

    }
}
