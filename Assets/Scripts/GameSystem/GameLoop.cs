using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My.Unity.Commons;
using BoardSystem;
//joeri
namespace GameSystem
{
    public class GameLoop : SingletonMonoBehaviour<GameLoop>
    {
        [SerializeField]
        private PositionHelper _positionHelper;

        [SerializeField]
        private Configuration _configuration;

        private int _boardRadius;
        private Grid<Tile> _grid;
        private Board<Pawn, Tile> _board;

        void Start()
        {
            _positionHelper.Awake();

            _boardRadius = _configuration.BoardRadius;
            _grid = new Grid<Tile>(_boardRadius);
            _board = new Board<Pawn, Tile>();

            ConnectTile();
            ConnectPawn();
        }

        private void ConnectTile()
        {
            var tiles = FindObjectsOfType<Tile>();
            
            foreach (var tile in tiles)
            {
                var (x, y) = _positionHelper.WorldToGridCoordinates( _grid, tile.transform.localPosition);
                _grid.Register(tile, x, y);

                //DebugTile(tile);
            }
        }

        private void ConnectPawn()
        {
            var pawns = FindObjectsOfType<Pawn>();
            
            foreach (var pawn in pawns)
            {
                var (x, y) = _positionHelper.WorldToGridCoordinates(_grid, pawn.transform.localPosition);

                if (_grid.TryGetPositionAt(x, y, out var tile))
                {
                    _board.Place(pawn, tile);

                    //DebugPawn(pawn, tile);
                }
            }
        }

        // DEBUG /////////////////////////////////////////////////////////////////////////////////////////////////
        internal void DebugTile(Tile tile)
        {
            var gridCoord = _positionHelper.WorldToGridCoordinates(_grid, tile.transform.localPosition);
            var worldPos = _positionHelper.GridToWorldPosition(_grid, gridCoord.x, gridCoord.y);
            var cubeCoord = _positionHelper.GridToCubeCoordinates(_grid, gridCoord.x, gridCoord.y);
            var cubetoGridCoord = _positionHelper.CubeToGridCoordinates(_grid, cubeCoord.q, cubeCoord.r);
            Debug.Log($"{tile.name} at GC {gridCoord} and WP {worldPos} and CC {cubeCoord} and C2G {cubetoGridCoord}");
        }

        internal void DebugPawn(Pawn pawn, Tile tile)
        {
            var pawnWorldPos = pawn.transform.localPosition;
            var tileWorldPos = tile.transform.localPosition;

            Debug.Log($"{pawn.name} at WP {pawnWorldPos} and {tile.name} at WP {tileWorldPos}");
        }
    }
}