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

        public bool ExistPossibleMovements()//esse método checa se existe algum movimento possível para
            //a peça, ou seja, se ela não está bloqueada
        {
            bool[,] matriz = PossibleMovements();//matriz que recebe a matriz com os booleanos de movimento
            for(int i = 0; i < Board.Lines; i++)
            {
                for(int j = 0; j < Board.Columns; j++)
                {
                    if (matriz[i, j])//se existe ao menos uma posição com true, retorna true e finaliza o
                        //método
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos)//possíveis posições que a peça pode ir
        {
            return PossibleMovements()[pos.Line, pos.Column];//o operador dentro de[] acessa a matriz e
            //retorna um valor booleano, pois  PossibleMoviments retorna uma matriz, ou seja,
            //retorna o valor que está nas coordenas passadas dentro dos colchetes
        }

        public abstract bool[,] PossibleMovements();//abstract diz que esse método não tem 
        //implementação nessa clase, esse método retorna uma matriz de booleanas, contendo true
        //onde é possível a peça se mover, por isso, sua implementação precisa ser feita na classe
        //da peça especifica
        

    }
}
