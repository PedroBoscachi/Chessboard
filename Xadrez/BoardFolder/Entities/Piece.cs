using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.BoardFolder.Entities.Enums;

namespace Xadrez.BoardFolder.Entities
{
    abstract class Piece
    {
        public Position Position { get; set; }//posição da peça
        public Color Color { get; protected set; }//cor da peça
        public int MovimentAmount { get; protected set; }//quantidade de movimentos que a peça fez
        public Board Board { get; protected set; }//tabuleiro em que se encontra

        public Piece()
        {
        }

        public Piece(Color color, Board board)
        {
            Position =null;//posição inicia nula
            Color = color;
            Board = board;
            MovimentAmount = 0;//movimento inicia com zero
        }

        public void IncrementMovimentAmount()
        {
            MovimentAmount++;
        }

        public bool ExistPossibleMovements()
        {
            bool[,] matriz = PossibleMoviments();
            for(int i = 0; i < Board.Lines; i++)
            {
                for(int j = 0; j < Board.Columns; j++)
                {
                    if (matriz[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossibleMoviments()[pos.Line, pos.Column];
        }

        public abstract bool[,] PossibleMoviments();//abstract diz que esse método não tem 
        //implementação nessa clase
        

    }
}
