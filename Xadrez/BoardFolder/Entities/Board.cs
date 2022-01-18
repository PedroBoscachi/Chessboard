using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.BoardFolder.BoardExceptions;

namespace Xadrez.BoardFolder.Entities
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board()
        {
        }

        public Board(int line, int column)
        {
            Lines = line;
            Columns = column;
            Pieces = new Piece[line, column];//cria uma matriz do tamanho do tabuleiro capaz de receber
            //peças em suas casas
        }

        public Piece PositionPiece(int line, int column)//retorna a peça que está nessa posição
        {
            return Pieces[line, column];
        }

        public Piece PositionPiece(Position position) //sobrecarga do método
        {
            return Pieces[position.Line, position.Column];
        }

        public bool ExistPiece(Position position)
        {
            ValidatedPosition(position);//se a posição for inválida o método é cortada e uma exceção lançada
            return PositionPiece(position) != null;//retorna true se a posição da peça for diferente de nulo
        }
        
        public void PlacePiece(Piece piece, Position position)
        {
            if (ExistPiece(position))//checa se tem uma peça na posição e se a posição é válida
            {
                throw new BoardException("There is already a piece in this position");
            }
            
            Pieces[position.Line, position.Column] = piece;//a matriz na linha n e coluna m recebe a peça
            piece.Position = position; //a posição da peça é alterada para a posição recebida
        }

        public Piece RemovePiece(Position pos)
        {
            if(PositionPiece(pos) == null)
            {
                return null;
            }

            Piece aux = PositionPiece(pos);
            aux.Position = null;
            Pieces[pos.Line, pos.Column] = null;
            return aux;
        }

        public bool ValidPosition(Position position)//checa se é uma posição possível no tabuleiro
        //ou seja, se essa posição existe no tabuleiro, se ela não é menor que 0 ou maior que a maior casa
        //possível
        {
            if(position.Line < 0 || position.Line >= Lines || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public void ValidatedPosition(Position position)
        {
            if (!ValidPosition(position))//se a posição não for válida joga uma exceção
            {
                throw new BoardException("Invalid position");
            }
        }
    }
}
