using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardSystem
{
    public class Board<TPosition>
    {
        public void MoveTo(IPiece<TPosition> piece, TPosition position)
        {
            //...

            piece.OnMoved(position);
        }

        public void Take(IPiece<TPosition> piece)
        {
            //...

            piece.OnTaken();
        }

    }
}