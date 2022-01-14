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
        public static void ShowBoard(Board board)
        {
            for(int i = 0; i < board.Lines; i++)
            {
                for(int j = 0; j < board.Columns; j++)
                {
                    if(board.PositionPiece(i, j) == null)
                    {
                        Console.Write("- ");
                    } else
                    {
                        Console.Write(board.PositionPiece(i, j) + " ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
