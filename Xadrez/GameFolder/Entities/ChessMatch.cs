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
        public bool Check { get; private set; }//condição do cheque
        public Piece VulnerableEnPassant { get; private set;  }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;//sempre inicia com o jogador branco
            Finished = false;
            VulnerableEnPassant = null;
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

            // #jogadaespecial roque pequeno
            if(p is King && destiny.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinyT = new Position(origin.Line, origin.Column + 1);
                Piece T = Board.RemovePiece(originT);
                T.IncrementMovimentAmount();
                Board.PlacePiece(T, destinyT);
            }

            // #jogadaespecial roque grande
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinyT = new Position(origin.Line, origin.Column - 1);
                Piece T = Board.RemovePiece(originT);
                T.IncrementMovimentAmount();
                Board.PlacePiece(T, destinyT);
            }

            // #jogadaespecial en passant
            if(p is Pawn)
            {
                if(origin.Column != destiny.Column && capturedPiece == null)
                {
                    Position posP;
                    if(p.Color == Color.White)
                    {
                        posP = new Position(destiny.Line + 1, destiny.Column);
                    } else
                    {
                        posP = new Position(destiny.Line - 1, destiny.Column);
                    }
                    capturedPiece = Board.RemovePiece(posP);
                    CapturedPieces.Add(capturedPiece);
                }
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

            //é necessário o roque no desfaz para caso ele resulte em cheque do rei próprio
            // #jogadaespecial roque pequeno
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinyT = new Position(origin.Line, origin.Column + 1);
                Piece T = Board.RemovePiece(destinyT);
                T.DecrementMovimentAmount();
                Board.PlacePiece(T, originT);
            }

            // #jogadaespecial roque grande
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinyT = new Position(origin.Line, origin.Column - 1);
                Piece T = Board.RemovePiece(destinyT);
                T.DecrementMovimentAmount();
                Board.PlacePiece(T, originT);
            }

            // #jogadaespecia en passant
            if(p is Pawn)
            {
                if(origin.Column != destiny.Column && capturedPiece == VulnerableEnPassant)
                {
                    Pawn pawn = (Pawn)Board.RemovePiece(destiny);
                    Position posP;
                    if(p.Color == Color.White)
                    {
                        posP = new Position(3, destiny.Column);
                    } else
                    {
                        posP = new Position(4, destiny.Column);
                    }
                    Board.PlacePiece(pawn, posP);
                    CapturedPieces.Remove(capturedPiece);
                }
            }
        }
        
        public void Play(Position origin, Position destiny)//executa a jogada
        {
            Piece capturedPiece = Move(origin, destiny);//recebe a peça capturada

            if (IsInCheck(CurrentPlayer))//se a jogada resultar em check no próprio rei, desfaz ela
            {
                UndoMove(origin, destiny, capturedPiece);
                throw new BoardException("You cannot put yourself in check!");
            }

            Piece p = Board.PositionPiece(destiny);

            // #jogadaespecial promoção
            if(p is Pawn)
            {
                if((p.Color == Color.White && destiny.Line == 0) || (p.Color == Color.Black && destiny.Line == 7))
                {
                    p = Board.RemovePiece(destiny);
                    Pieces.Remove(p);
                    Piece queen = new Queen(p.Color, Board);
                    Board.PlacePiece(queen, destiny);
                    Pieces.Add(queen);
                }
            }

            if (IsInCheck(Enemy(CurrentPlayer)))//se o rei inimigo estiver em cheque
            {
                Check = true;
            } else
            {
                Check = false;
            }

            if (TestCheckmate(Enemy(CurrentPlayer)))//se der chequemate finaliza o programa
            {        
                Finished = true;
            } else
            {
                Shift++;//incrementa o turno
                ChangePlayer();//troca de jogador
            }          

            // #jogada especial en passant
            if(p is Pawn && (destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2))
            {
                VulnerableEnPassant = p;
            } else
            {
                VulnerableEnPassant = null;
            }
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
            if (!Board.PositionPiece(origin).PossibleMovement(destiny))
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

        public bool TestCheckmate(Color color)
        {
            if (!IsInCheck(color))//se não estiver em cheque já corta o método
            {
                return false;
            }

            foreach(Piece x in PiecesInGame(color))//para cada peça aliada no jogo, testa se existe algum
                //movimento possível para salvar o rei
            {
                bool[,] matriz = x.PossibleMovements();

                for(int i = 0; i < Board.Lines; i++)
                {
                    for(int j = 0; j < Board.Columns; j++)
                    {
                        if (matriz[i, j])//se for true
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = Move(origin, destiny);//a peça x vai para o destino
                            bool testCheck = IsInCheck(color);//testa se o check continua
                            UndoMove(origin, destiny, capturedPiece);//desfaz o movimento
                            if (!testCheck)//se não está mais em check, existe um movimento que salva o rei
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PlaceNewPiece(char column, int line, Piece piece)//método para facilitar 
        {
            Board.PlacePiece(piece, new PositionChess(column, line).ToPosition());
            Pieces.Add(piece);//adiciona a peça no conjunto
        }
        
        private void PlacePieces()
        {
            PlaceNewPiece('a', 1, new Tower(Color.White, Board));
            PlaceNewPiece('b', 1, new Horse(Color.White, Board));
            PlaceNewPiece('c', 1, new Bishop(Color.White, Board));
            PlaceNewPiece('d', 1, new Queen(Color.White, Board));
            PlaceNewPiece('e', 1, new King(Color.White, Board, this));
            PlaceNewPiece('f', 1, new Bishop(Color.White, Board));
            PlaceNewPiece('g', 1, new Horse(Color.White, Board));
            PlaceNewPiece('h', 1, new Tower(Color.White, Board));
            PlaceNewPiece('a', 2, new Pawn(Color.White, Board, this));
            PlaceNewPiece('b', 2, new Pawn(Color.White, Board, this));
            PlaceNewPiece('c', 2, new Pawn(Color.White, Board, this));
            PlaceNewPiece('d', 2, new Pawn(Color.White, Board, this));
            PlaceNewPiece('e', 2, new Pawn(Color.White, Board, this));
            PlaceNewPiece('f', 2, new Pawn(Color.White, Board, this));
            PlaceNewPiece('g', 2, new Pawn(Color.White, Board, this));
            PlaceNewPiece('h', 2, new Pawn(Color.White, Board, this));

            PlaceNewPiece('a', 8, new Tower(Color.Black, Board));
            PlaceNewPiece('b', 8, new Horse(Color.Black, Board));
            PlaceNewPiece('c', 8, new Bishop(Color.Black, Board));
            PlaceNewPiece('d', 8, new Queen(Color.Black, Board));
            PlaceNewPiece('e', 8, new King(Color.Black, Board, this));
            PlaceNewPiece('f', 8, new Bishop(Color.Black, Board));
            PlaceNewPiece('g', 8, new Horse(Color.Black, Board));
            PlaceNewPiece('h', 8, new Tower(Color.Black, Board));
            PlaceNewPiece('a', 7, new Pawn(Color.Black, Board, this));
            PlaceNewPiece('b', 7, new Pawn(Color.Black, Board, this));
            PlaceNewPiece('c', 7, new Pawn(Color.Black, Board, this));
            PlaceNewPiece('d', 7, new Pawn(Color.Black, Board, this));
            PlaceNewPiece('e', 7, new Pawn(Color.Black, Board, this));
            PlaceNewPiece('f', 7, new Pawn(Color.Black, Board, this));
            PlaceNewPiece('g', 7, new Pawn(Color.Black, Board, this));
            PlaceNewPiece('h', 7, new Pawn(Color.Black, Board, this));
        }
    }
}
