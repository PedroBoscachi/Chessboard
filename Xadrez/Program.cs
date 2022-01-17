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
                    try
                    {
                        Console.Clear();
                        Screen.ShowBoard(match.Board);
                        Console.WriteLine(" ");
                        Console.WriteLine(" ");
                        Console.WriteLine("Shift: " + match.Shift);
                        Console.WriteLine("Waiting movement: " + match.CurrentPlayer);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadPositionChess().ToPosition();
                        match.ValidatedOriginPosition(origin);

                        bool[,] possiblePositions = match.Board.PositionPiece(origin).PossibleMovements();

                        Console.Clear();
                        Screen.ShowBoard(match.Board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadPositionChess().ToPosition();
                        match.ValidateDestinyPosition(origin, destiny);

                        match.Play(origin, destiny);
                    }
                    catch(BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
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
