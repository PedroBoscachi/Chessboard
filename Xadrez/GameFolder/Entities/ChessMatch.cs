using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.BoardFolder.Entities;
using Xadrez.BoardFolder.BoardExceptions;
using Xadrez.BoardFolder.Entities.Enums;

namespace Xadrez.GameFolder.Entities
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Shift { get; set; }//Turno
        public Color CurrentPlayer { get; set; }
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

        public void Play(Position origin, Position destiny)
        {
            Move(origin, destiny);
            Shift++;
            ChangePlayer();
        }

        public void ValidatedOriginPosition(Position pos)
        {
            if (!Board.ExistPiece(pos))
            {
                throw new BoardException("Choose a valid board position!");
            }
            
            if(Board.PositionPiece(pos) == null)
            {
                throw new BoardException("There isn't any piece in the chosed origin position!");
            }

            if(CurrentPlayer != Board.PositionPiece(pos).Color)
            {
                throw new BoardException("The chosen piece is not yours!");
            }

            if (!Board.PositionPiece(pos).ExistPossibleMovements())
            {
                throw new BoardException("There isn't any possible movements for the chosen piece");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.PositionPiece(origin).CanMoveTo(destiny))
            {
                throw new BoardException("Destiny position invailable!");
            }
        }

        public void ChangePlayer()
        {
            if(CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            } else
            {
                CurrentPlayer = Color.White;
            }
        }
        
        private void PlacePieces()
        {
            Board.PlacePiece(new Tower(Color.White, Board), new PositionChess('c', 1).ToPosition());
            Board.PlacePiece(new Tower(Color.White, Board), new PositionChess('c', 2).ToPosition());
            Board.PlacePiece(new Tower(Color.White, Board), new PositionChess('d', 2).ToPosition());
            Board.PlacePiece(new Tower(Color.White, Board), new PositionChess('e', 2).ToPosition());
            Board.PlacePiece(new Tower(Color.White, Board), new PositionChess('e', 1).ToPosition());
            Board.PlacePiece(new King(Color.White, Board), new PositionChess('d', 1).ToPosition());

            Board.PlacePiece(new Tower(Color.Black, Board), new PositionChess('c', 7).ToPosition());
            Board.PlacePiece(new Tower(Color.Black, Board), new PositionChess('c', 8).ToPosition());
            Board.PlacePiece(new Tower(Color.Black, Board), new PositionChess('d', 7).ToPosition());
            Board.PlacePiece(new Tower(Color.Black, Board), new PositionChess('e', 7).ToPosition());
            Board.PlacePiece(new Tower(Color.Black, Board), new PositionChess('e', 8).ToPosition());
            Board.PlacePiece(new King(Color.Black, Board), new PositionChess('d', 8).ToPosition());
        }
    }
}
