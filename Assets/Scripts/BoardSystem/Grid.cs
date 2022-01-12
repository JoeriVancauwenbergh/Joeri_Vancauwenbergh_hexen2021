using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.Commons;
using System;

namespace BoardSystem
{
    public class Grid<TPosition>
    {
        private BidirectionalDictionary<TPosition, (float x, float y)> _positions
                    = new BidirectionalDictionary<TPosition, (float x, float y)>();

        public int Radius { get; }

        public Grid(int radius)
        {
            Radius = radius;
        }

        public bool TryGetPositionAt(float x, float y, out TPosition position)
           => _positions.TryGetKey((x, y), out position);

        public bool TryGetCoordinateAt(TPosition position, out (float x, float y) coordinate)
           => _positions.TryGetValue(position, out coordinate);

        public void Register(TPosition position, float x, float y)
        {
            _positions.Add(position, (x, y));
            //DebugGrid(_positions);
        }
        internal void DebugGrid(BidirectionalDictionary<TPosition, (float x, float y)> positions)
        {
            foreach (KeyValuePair<TPosition, (float x, float y)> kvp in positions)
                Debug.Log("GridPosition -> Key = " + kvp.Key + " Value = " + kvp.Value);
        }
    }
}