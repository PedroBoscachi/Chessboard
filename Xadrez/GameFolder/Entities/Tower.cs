using Xadrez.BoardFolder.Entities;
using Xadrez.BoardFolder.Entities.Enums;

namespace Xadrez.GameFolder.Entities
{
    class Tower : Piece
    {
        public Tower(Color color, Board board) : base(color, board)
        {
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.PositionPiece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] matriz = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            //acima
            pos.SetValues(Position.Line - 1, Position.Column);
            while(Board.ValidPosition(pos) && CanMove(pos))//o while é necessário pois a torre
            //pode andar tanto na vertical como na horizontal, ou seja, enquanto a posição for válida
            //ela pode ir
            {
                matriz[pos.Line, pos.Column] = true;//coloca true na posição para mostrar que é válida
                if(Board.PositionPiece(pos) != null && Board.PositionPiece(pos).Color != Color)
                //se a posição não for nula e a peça nela for inimiga, a torre pode ir até essa posição
                {
                    break;
                }
                pos.Line = pos.Line - 1;//senão continua indo para cima
            }

            //abaixo
            pos.SetValues(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
                if (Board.PositionPiece(pos) != null && Board.PositionPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Line = pos.Line + 1;
            }

            //direita
            pos.SetValues(Position.Line, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
                if (Board.PositionPiece(pos) != null && Board.PositionPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Column = pos.Column + 1;
            }

            //esquerda
            pos.SetValues(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                matriz[pos.Line, pos.Column] = true;
                if (Board.PositionPiece(pos) != null && Board.PositionPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Column = pos.Column - 1;
            }


            return matriz;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
