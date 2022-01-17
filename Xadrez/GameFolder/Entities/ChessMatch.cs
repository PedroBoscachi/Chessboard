using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.BoardFolder.Entities;
using Xadrez.BoardFolder.Entities.Enums;

namespace Xadrez.GameFolder.Entities
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        private int Shift;//Turno
        private Color CurrentPlayer;
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;//sempre inicia com o jogador branco
            Finished = false;
            PlacePieces();
        }

        public void Move(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementMovimentAmount();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PlacePiece(p, destiny);
        }

        private void PlacePieces()
        {
            Board.PlacePiece(new Tower(Color.Black, Board), new PositionChess('c', 1).ToPosition());
            Board.PlacePiece(new Tower(Color.Black, Board), new PositionChess('c', 2).ToPosition());
            Board.PlacePiece(new Tower(Color.White, Board), new PositionChess('d', 2).ToPosition());
            Board.PlacePiece(new Tower(Color.Black, Board), new PositionChess('e', 2).ToPosition());
            Board.PlacePiece(new Tower(Color.Black, Board), new PositionChess('e', 1).ToPosition());
            Board.PlacePiece(new King(Color.White, Board), new PositionChess('d', 1).ToPosition());

            Board.PlacePiece(new Tower(Color.Black, Board), new PositionChess('c', 7).ToPosition());
            Board.PlacePiece(new Tower(Color.Black, Board), new PositionChess('c', 8).ToPosition());
            Board.PlacePiece(new Tower(Color.White, Board), new PositionChess('d', 7).ToPosition());
            Board.PlacePiece(new Tower(Color.Black, Board), new PositionChess('e', 7).ToPosition());
            Board.PlacePiece(new Tower(Color.Black, Board), new PositionChess('e', 8).ToPosition());
            Board.PlacePiece(new King(Color.White, Board), new PositionChess('d', 8).ToPosition());
        }
    }
}
