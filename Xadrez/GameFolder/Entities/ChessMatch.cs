using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.BoardFolder.Entities;
using Xadrez.BoardFolder.BoardExceptions;
using Xadrez.BoardFolder.Entities.Enums;

namespace Xadrez.GameFolder.Entities
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Shift { get; set; }//Turno
        public Color CurrentPlayer { get; set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> Pieces;//isso é um conjunto de peças
        private HashSet<Piece> CapturedPieces;//conjunto de peças capturadas
        public bool Check { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;//sempre inicia com o jogador branco
            Finished = false;
            Pieces = new HashSet<Piece>();//instancia os conjuntos
            CapturedPieces = new HashSet<Piece>();
            PlacePieces();
        }

        public Piece Move(Position origin, Position destiny)//movimenta a peça
        {
            Piece p = Board.RemovePiece(origin);//remove a peça escolhida temporiaramente
            p.IncrementMovimentAmount();//acrescenta o número de movimentos da peça
            Piece capturedPiece = Board.RemovePiece(destiny);//remove a peça do destino e guarda ela
            Board.PlacePiece(p, destiny);//coloca a peça de origem na posição de destino
            if(capturedPiece != null)//se a peça capturada não for nula adiciona ela no conjunto
            {
                CapturedPieces.Add(capturedPiece);
            }

            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destiny, Piece capturedPiece)//desfaz movimento
        {
            Piece p = Board.RemovePiece(destiny);//remove a peça da posição de destino
            p.DecrementMovimentAmount();//decrementa o número de movimentos
            if(capturedPiece != null)// se a peça capturada não for nula
            {
                Board.PlacePiece(capturedPiece, destiny);//coloca ela de volta na posição de destino
                CapturedPieces.Remove(capturedPiece);//remove ela do  conjunto de capturadas
            }
            Board.PlacePiece(p, origin);//coloca a peça p de volta na posição de origem
        }
        
        public void Play(Position origin, Position destiny)//executa a jogada
        {
            Piece capturedPiece = Move(origin, destiny);//recebe a peça capturada

            if (IsInCheck(CurrentPlayer))//se a jogada resultar em check no próprio rei, desfaz ela
            {
                UndoMove(origin, destiny, capturedPiece);
                throw new BoardException("You cannot put yourself in check!");
            }

            if (IsInCheck(Enemy(CurrentPlayer)))//se o rei inimigo estiver em cheque
            {
                Check = true;
            } else
            {
                Check = false;
            }
            Shift++;//incrementa o turno
            ChangePlayer();//troca de jogador
        }

        public void ValidatedOriginPosition(Position pos)//erros que podem ocorrer ao escolher a origem
        {
            if (!Board.ExistPiece(pos))
            {
                throw new BoardException("Choose a valid board position!");
            }
            
            if(Board.PositionPiece(pos) == null)
            {
                throw new BoardException("There isn't any piece in the chosed origin position!");
            }

            if(CurrentPlayer != Board.PositionPiece(pos).Color)
            {
                throw new BoardException("The chosen piece is not yours!");
            }

            if (!Board.PositionPiece(pos).ExistPossibleMovements())
            {
                throw new BoardException("There isn't any possible movements for the chosen piece");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)//erros que podem ocorrer
            //ao escolher o destino
        {
            if (!Board.PositionPiece(origin).CanMoveTo(destiny))
            {
                throw new BoardException("Destiny position invailable!");
            }
        }

        public void ChangePlayer()
        {
            if(CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            } else
            {
                CurrentPlayer = Color.White;
            }
        }

        public HashSet<Piece> ChessCapturedPieces(Color color)//método que retorna um conjunto de peças
            //capturadas
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in CapturedPieces)
            {
                if(x.Color == color)//se a cor da peça for a cor do argumento adiciona no conjunto auxiliar
                {
                    aux.Add(x);
                }
            }
            return aux;//retorna o conjunto auxiliar
        }

        public HashSet<Piece> PiecesInGame(Color color)//método que retorna as peças que estão em jogo
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in Pieces)
            {
                if(x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(ChessCapturedPieces(color));//tira as peças capturadas do conjunto, ou seja,
            //resta apenas as que estão em jogo
            return aux;
        }

        private Color Enemy(Color color)//retorna cor do inimigo
        {
            if(Color.White == color)
            {
                return Color.Black;
            } else
            {
                return Color.White;
            }
        }

        private Piece King(Color color)
        {
            foreach(Piece x in PiecesInGame(color))//percorre as peças em jogo de um time
            {
                if(x is King)//quando encontrar o rei, retorna ele
                {
                    return x;
                }
            }

            return null;//retornar nulo é só para legibilidade do código, pois deve existir um rei
        }

        public bool IsInCheck(Color color)
        {
            Piece king = King(color);//recebe um rei da cor informada
            if(king == null)
            {
                throw new BoardException("There is no " + king.Color + "king in the board!");
            }

            foreach(Piece x in PiecesInGame(Enemy(color)))//peças do time inimigo em jogo
            {
                bool[,] matriz = x.PossibleMovements();//retorna possíveis movimentos de uma peça inimiga
                if(matriz[king.Position.Line, king.Position.Column])//se a posição do rei der true retorna true
                {
                    return true;
                }

            }
            return false;
        }

        public void PlaceNewPiece(char column, int line, Piece piece)//método para facilitar 
        {
            Board.PlacePiece(piece, new PositionChess(column, line).ToPosition());
            Pieces.Add(piece);//adiciona a peça no conjunto
        }
        
        private void PlacePieces()
        {
            PlaceNewPiece('c', 1, new Tower(Color.White, Board));
            PlaceNewPiece('c', 2, new Tower(Color.White, Board));
            PlaceNewPiece('d', 2, new Tower(Color.White, Board));
            PlaceNewPiece('e', 2, new Tower(Color.White, Board));
            PlaceNewPiece('e', 1, new Tower(Color.White, Board));
            PlaceNewPiece('d', 1, new King(Color.White, Board));

            PlaceNewPiece('c', 7, new Tower(Color.Black, Board));
            PlaceNewPiece('c', 8, new Tower(Color.Black, Board));
            PlaceNewPiece('d', 7, new Tower(Color.Black, Board));
            PlaceNewPiece('e', 7, new Tower(Color.Black, Board));
            PlaceNewPiece('e', 8, new Tower(Color.Black, Board));
            PlaceNewPiece('d', 8, new King(Color.Black, Board));
        }
    }
}
