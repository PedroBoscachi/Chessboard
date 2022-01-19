using Xadrez.BoardFolder.Entities;
using Xadrez.BoardFolder.Entities.Enums;

namespace Xadrez.GameFolder.Entities
{
    class King : Piece
    {
        private ChessMatch Match;
        
        public King(Color color, Board board, ChessMatch match) : base(color, board)
        {
            Match = match;
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

        private bool TestTowerForRoque(Position pos)
        {
            Piece p = Board.PositionPiece(pos);
            return p != null && p is Tower && p.Color == Color && p.MovimentAmount == 0;
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

            //jogada especial
            if(MovimentAmount == 0 && !Match.Check)
            {
                // #jogadaespecial roque pequeno
                Position posT1 = new Position(Position.Line, Position.Column + 3);
                if (TestTowerForRoque(posT1))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);
                    if(Board.PositionPiece(p1) == null && Board.PositionPiece(p2) == null)
                    {
                        matriz[Position.Line, Position.Column + 2] = true;
                    }
                }

                // #jogadaespecial roque grande
                Position posT2 = new Position(Position.Line, Position.Column - 4);
                if (TestTowerForRoque(posT2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.PositionPiece(p1) == null && Board.PositionPiece(p2) == null && Board.PositionPiece(p3) == null)
                    {
                        matriz[Position.Line, Position.Column - 2] = true;
                    }
                }
            }

            return matriz;
        }
    }
}
