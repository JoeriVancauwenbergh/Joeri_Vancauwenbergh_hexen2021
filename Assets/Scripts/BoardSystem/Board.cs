using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DAE.Commons;

namespace BoardSystem
{
    public class PiecePlacedEventArgs<TPiece, TPosition> : EventArgs
    {
        public TPiece Piece { get; }

        public TPosition AtPosition { get; }

        public PiecePlacedEventArgs(TPiece piece, TPosition atPosition)
        {
            Piece = piece;
            AtPosition = atPosition;
        }
    }

    public class Board<TPiece, TPosition>
    {
        public event EventHandler<PiecePlacedEventArgs<TPiece, TPosition>> PiecePlaced;

        private BidirectionalDictionary<TPiece, TPosition> _pieces
            = new BidirectionalDictionary<TPiece, TPosition>();

        public bool TryGetPiece(TPosition position, out TPiece piece)
            => _pieces.TryGetKey(position, out piece);

        public bool TryGetPosition(TPiece piece, out TPosition position)
            => _pieces.TryGetValue(piece, out position);

        public void Place(TPiece piece, TPosition position)
        {
            if (_pieces.ContainsKey(piece))
                return;

            if (_pieces.ContainsValue(position))
                return;

            _pieces.Add(piece, position);
            OnPiecePlaced(new PiecePlacedEventArgs<TPiece, TPosition>(piece, position));

            //DebugBoard(_pieces);
        }

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

        protected virtual void OnPiecePlaced(PiecePlacedEventArgs<TPiece, TPosition> eventArgs)
        {
            var handler = PiecePlaced;
            handler?.Invoke(this, eventArgs);
        }


        // DEBUG /////////////////////////////////////////////////////////////////////////////////////////////////
        internal void DebugBoard(BidirectionalDictionary<TPiece, TPosition> pieces)
        {
            foreach (KeyValuePair<TPiece, TPosition> kvp in pieces)
                Debug.Log("Key = " + kvp.Key + " Value = " + kvp.Value);
        }
    }
}