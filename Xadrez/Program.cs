using System;
using Xadrez.BoardFolder.Entities;
using Xadrez.BoardFolder.Entities.Enums;
using Xadrez.GameFolder.Entities;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8,8);

            board.PlacePiece(new King(Color.Black, board), new Position(1, 3));
            board.PlacePiece(new Tower(Color.Black, board), new Position(1, 7));
            board.PlacePiece(new Tower(Color.Black, board), new Position(4, 1));

            Screen.ShowBoard(board);
        }
    }
}
