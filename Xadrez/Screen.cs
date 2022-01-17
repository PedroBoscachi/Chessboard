using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.BoardFolder.Entities;
using Xadrez.BoardFolder.Entities.Enums;

namespace Xadrez
{
    class Screen
    {
        public static void ShowBoard(Board board) //mostra o tabuleiro
        {
            for(int i = 0; i < board.Lines; i++)
            {

                Console.Write(8 - i + " ");
                
                for(int j = 0; j < board.Columns; j++)
                {
                    if(board.PositionPiece(i, j) == null)//se a posição estiver vazia coloca um traço
                    {
                        Console.Write("- ");
                    } else
                    {
                        ShowPiece(board.PositionPiece(i, j));
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }

            Console.Write("  a b c d e f g h");
        }

        public static void ShowPiece(Piece piece)
        {
            if(piece.Color == Color.White)
            {
                Console.Write(piece);
            } else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
