using Xadrez.BoardFolder.Entities;
using Xadrez.BoardFolder.Entities.Enums;

namespace Xadrez.GameFolder.Entities
{
    class King : Piece
    {
        public King(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position pos)//checa se a peça pode mover
        {
            Piece p = Board.PositionPiece(pos);
            return p == null || p.Color != Color;//instancia uma peça p e guarda nela a peça que está em pos
            //e retorna true caso essa posição dessa peça for vazia ou nessa posição estiver uma peça
            //do time inimigo, ou seja, o true significa que a peça atual pode mover para a posição
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] matriz = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);//instancia uma posição na posição inicial

            //acima
            pos.SetValues(Position.Line - 1, Position.Column);//seta os valores para checar
            if(Board.ValidPosition(pos) && CanMove(pos))
            {
                //se a posição é possível e a peça consegue mover para lá, coloca true na posição
                matriz[pos.Line, pos.Column] = true;
            }


            //nordeste
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
            }


            //direita
            pos.SetValues(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
            }


            //sudeste
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
            }


            //abaixo
            pos.SetValues(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
            }


            //esquerda
            pos.SetValues(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
            }


            //noroeste
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
            }

            return matriz;
        }
    }
}
