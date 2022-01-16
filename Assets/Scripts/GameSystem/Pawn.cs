using System;
using UnityEngine;

namespace GameSystem
{
	public class PawnEventArgs<TTile> : EventArgs
	{
		public TTile Tile { get; }

		public PawnEventArgs(TTile tile)
			=>Tile = tile;
	}

	public class ActivateEventArgs : EventArgs
	{
		public bool Status { get; }

		public ActivateEventArgs(bool status)
			=> Status = status;
	}

	public class ClickEventArgs<TTile> : EventArgs where TTile : MonoBehaviour, ITile
	{
		public Pawn<TTile> Piece { get; }

		public ClickEventArgs(Pawn<TTile> piece)
			=> Piece = piece;
	}

	public class Pawn<TTile> : MonoBehaviour where TTile : MonoBehaviour, ITile
	{
        public event EventHandler<PawnEventArgs<TTile>> Placed;
		public event EventHandler<PawnEventArgs<TTile>> Moved;
		public event EventHandler<PawnEventArgs<TTile>> Taken;

		internal bool _hasMoved { get; set; }

		public void PlaceAt(TTile tile)
			=> OnPlaced(new PawnEventArgs<TTile>(tile));

		public void MoveTo(TTile tile)
			=> OnMoved(new PawnEventArgs<TTile>(tile));

		public void TakeFrom(TTile tile)
			=> OnTaken(new PawnEventArgs<TTile>(tile));

		protected virtual void OnPlaced(PawnEventArgs<TTile> eventArgs)
		{
			EventHandler<PawnEventArgs<TTile>> handler = Placed;
			handler?.Invoke(this, eventArgs);

			transform.position = eventArgs.Tile.transform.position;
			gameObject.SetActive(true);
		}

		protected virtual void OnMoved(PawnEventArgs<TTile> eventArgs)
		{
			EventHandler<PawnEventArgs<TTile>> handler = Moved;
			handler?.Invoke(this, eventArgs);

			transform.position = eventArgs.Tile.transform.position;
		}

		protected virtual void OnTaken(PawnEventArgs<TTile> eventArgs)
		{
			EventHandler<PawnEventArgs<TTile>> handler = Taken;
			handler?.Invoke(this, eventArgs);

			gameObject.SetActive(false);
		}
	}
}