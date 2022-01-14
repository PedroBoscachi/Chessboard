using System;

namespace Xadrez.BoardFolder.BoardExceptions
{
    class BoardException : Exception
    {
        public BoardException(string message) : base(message)
        {
        }
    }
}
