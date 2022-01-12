using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardSystem;

namespace GameSystem
{
    [CreateAssetMenu(menuName = "Hexen/PositionHelper")]

    public class PositionHelper : ScriptableObject
    {
        [SerializeField]
        private Configuration _configuration;

        private float _tileDimension;
        private float _offsetFactor = 0.875f;

        public void Awake()
            => _tileDimension = _configuration.TileDimension;

        public (float x, float y) WorldToGridCoordinates(Grid<Tile> grid, Vector3 worldPos)
        {
            // for pointy top hexgrid /////////////////////////////////////////////////////////////

            var scaledWorldPos = worldPos / _tileDimension;

            var gridPosX = scaledWorldPos.x + grid.Radius;
            var gridPosY = Mathf.Abs(scaledWorldPos.z + (grid.Radius * _offsetFactor));
           
            return (gridPosX, gridPosY);
        }

        public Vector3 GridToWorldPosition(Grid<Tile> grid, float x, float y)
        {
            // for pointy top hexgrid /////////////////////////////////////////////////////////////

            var scaledWorldPosX = x - grid.Radius;
            var scaledWorldPosZ = y - (grid.Radius * _offsetFactor);

            var worldPosX = scaledWorldPosX * _tileDimension;
            var worldPosZ = scaledWorldPosZ * _tileDimension;

            return new Vector3(worldPosX, 0, worldPosZ);
        }

        public (int q, int r, int s) GridToCubeCoordinates(Grid<Tile> grid, float x, float y)
        {
            // for pointy top hexgrid /////////////////////////////////////////////////////////////

            //Set HexGridCenter to Origin (0,0)
            var newPosY = y - (grid.Radius * _offsetFactor);
            var newPosX = x - grid.Radius;

            //Calculate Row- & ColumnNumbers
            var rowNumber = newPosY / _offsetFactor;
            var columnNumber = newPosX * 2;

            //Calculate CubeCoordinates q, r, s
            var r = (int)-rowNumber;
            var q = (int) ((columnNumber - r) / 2);
            var s = -q - r;

            return (q, r, s);
        }

        public (float x, float y) CubeToGridCoordinates(Grid<Tile> grid, int q, int r)
        {
            // for pointy top hexgrid /////////////////////////////////////////////////////////////
            var x = grid.Radius + (q + r / 2f);
            var y = _offsetFactor * (grid.Radius - r);

            return (x, y);
        }
    }
}