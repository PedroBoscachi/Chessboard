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
                ChessMatch match = new ChessMatch();//cria nova partida

                while (!match.Finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.ShowMatch(match);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadPositionChess().ToPosition();
                        match.ValidatedOriginPosition(origin);//checa se a posição é válida

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

                Console.Clear();
                Screen.ShowBoard(match.Board);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine("Winner: " + match.CurrentPlayer);
                Console.ReadLine();



            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
