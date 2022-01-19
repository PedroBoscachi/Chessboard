using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.BoardFolder.Entities;
using Xadrez.BoardFolder.Entities.Enums;
using Xadrez.GameFolder.Entities;

namespace Xadrez
{
    class Screen
    {
        public static void ShowMatch(ChessMatch match)
        {
            ShowBoard(match.Board);
            Console.WriteLine();
            ShowCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Shift: " + match.Shift);
            if (!match.Finished)
            {
                Console.WriteLine("Waiting for play: " + match.CurrentPlayer);
                if (match.Check)
                {
                    Console.WriteLine("CHECK !");
                }
            } 
        }

        public static void ShowCapturedPieces(ChessMatch match)
        {
            Console.WriteLine();
            Console.WriteLine("Captured Pieces: ");
            Console.Write("White: ");
            ShowGroup(match.ChessCapturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ShowGroup(match.ChessCapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void ShowGroup(HashSet<Piece> group)
        {
            Console.Write("[");
            foreach(Piece x in group)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void ShowBoard(Board board) //mostra o tabuleiro
        {
            for(int i = 0; i < board.Lines; i++)
            {

                Console.Write(8 - i + " ");
                
                for(int j = 0; j < board.Columns; j++)
                {
                        ShowPiece(board.PositionPiece(i, j));                 
                }

                Console.WriteLine();
            }

            Console.Write("  a b c d e f g h");
        }

        public static void ShowBoard(Board board, bool[,] possibleMoviments) //sobrecarga do método
            //método que mostra os possíveis movimentos da peça
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor modifiedBackground = ConsoleColor.DarkGray;
            
            for (int i = 0; i < board.Lines; i++)
            {

                Console.Write(8 - i + " ");

                for (int j = 0; j < board.Columns; j++)
                {
                    if (possibleMoviments[i, j])//se a posição é válida para a peça
                    {
                        Console.BackgroundColor = modifiedBackground;
                    } else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    ShowPiece(board.PositionPiece(i, j));
                    Console.BackgroundColor = originalBackground;
                }

                Console.WriteLine();
            }

            Console.Write("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }

        public static PositionChess ReadPositionChess()
        {
            string s = Console.ReadLine();
            char column = s[0];//pega a letra e coloca como coluna
            int line = int.Parse(s[1] + "");//aspas sem espaço força a ser lido como string
            return new PositionChess(column, line);
        }
        
        public static void ShowPiece(Piece piece)
        {
            if(piece == null)
            {
                Console.Write("- ");
            } else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                } else
                {
                    ConsoleColor aux = Console.ForegroundColor; // se a cor da peça for preta
                    Console.ForegroundColor = ConsoleColor.Yellow;//coloca o console
                    Console.Write(piece);//com  a cor amarela
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
            
            
        }
    }
}
