using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.BoardFolder.Entities;

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
                        Console.Write(board.PositionPiece(i, j) + " ");//printa a peça na posição
                    }
                }

                Console.WriteLine();
            }

            Console.Write("  a b c d e f g h");
        }
    }
}
