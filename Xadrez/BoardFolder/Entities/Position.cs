
namespace Xadrez.BoardFolder.Entities
{
    class Position
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public Position()
        {
        }

        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public void SetValues(int line, int column)
        //método criado apenas para setar os valores e poder muda-los depois
        {
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            return Line
                + " - "
                + Column;
        }
    }
}
