﻿using System;
using Xadrez.BoardFolder.Entities;
using Xadrez.BoardFolder.Entities.Enums;
using Xadrez.GameFolder.Entities;
using Xadrez.BoardFolder.BoardExceptions;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);

                board.PlacePiece(new King(Color.Black, board), new Position(1, 3));
                board.PlacePiece(new Tower(Color.Black, board), new Position(2, 7));
                board.PlacePiece(new Tower(Color.White, board), new Position(6, 4));

                Screen.ShowBoard(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
