using System;
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
                ChessMatch match = new ChessMatch();


                

                Screen.ShowBoard(match.Board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
