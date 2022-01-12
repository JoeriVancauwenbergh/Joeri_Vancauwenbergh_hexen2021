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

        [SerializeField]
        private Transform _boardParent;

        private int _boardRadius;
        private Grid<Tile> _grid;
        //private Board<Tile> _board;

        void Start()
        {
            _positionHelper.Awake();

            _boardRadius = _configuration.BoardRadius;
            _grid = new Grid<Tile>(_boardRadius);
            //_board = new Board<Tile>();

            ConnectTile();
        }

        private void ConnectTile()
        {
            var tiles = FindObjectsOfType<Tile>();
            
            foreach (var tile in tiles)
            {
                var (x, y) = _positionHelper.WorldToGridCoordinates( _grid, tile.transform.localPosition);
                _grid.Register(tile, x, y);
            }
        }

        internal void DebugTile(Tile tile)
        {
            var gridCoord = _positionHelper.WorldToGridCoordinates(_grid, tile.transform.localPosition);
            var worldPos = _positionHelper.GridToWorldPosition(_grid, gridCoord.x, gridCoord.y);
            var cubeCoord = _positionHelper.GridToCubeCoordinates(_grid, gridCoord.x, gridCoord.y);
            var cubetoGridCoord = _positionHelper.CubeToGridCoordinates(_grid, cubeCoord.q, cubeCoord.r);
            Debug.Log($"{tile.name} at GC {gridCoord} and WP {worldPos} and CC {cubeCoord} and C2G {cubetoGridCoord}");
        }
    }
}