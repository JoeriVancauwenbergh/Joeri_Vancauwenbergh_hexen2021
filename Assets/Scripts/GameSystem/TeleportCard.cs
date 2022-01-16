using System.Collections.Generic;
using CardSystem;
using System.Linq;

namespace GameSystem
{
	public class TeleportCard : BaseCard<Pawn<Tile>, Tile>
	{
		public override List<Tile> Positions(Pawn<Tile> pawn, Tile tile)
		{
			List<Tile> tiles = _grid.GetTiles()
				.Where(tile => _board.TryGetPiece(tile, out _) == false)
				.ToList();

			if (tiles.Contains(tile))
			{
				_validTiles = new List<Tile> { tile };
			}
			else
			{
				_validTiles = new List<Tile>();
			}

			return _validTiles;
		}

		public override bool Execute(Pawn<Tile> pawn, Tile tile)
		{
			if (!_validTiles.Contains(tile)) return false;

			_board.Move(pawn, tile);

			base.Execute(pawn, tile);

			return true;
		}
	}
}