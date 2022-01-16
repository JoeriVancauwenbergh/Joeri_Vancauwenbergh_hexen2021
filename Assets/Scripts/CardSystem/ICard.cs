using System.Collections.Generic;
using BoardSystem;

namespace CardSystem
{
    public interface ICard<TPiece, TPosition>
    {
        void Initialize(Board<TPiece, TPosition> board, Grid<TPosition> grid);

        bool Execute(TPiece piece, TPosition position);

        List<TPosition> Positions(TPiece piece, TPosition position);
    }
}