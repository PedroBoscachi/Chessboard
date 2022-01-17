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

                while (!match.Finished)
                {
                    Console.Clear();
                    Screen.ShowBoard(match.Board);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadPositionChess().ToPosition();
                    Console.Write("Destiny: ");
                    Position destiny = Screen.ReadPositionChess().ToPosition();

                    match.Move(origin, destiny);
                }
                

                Screen.ShowBoard(match.Board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
