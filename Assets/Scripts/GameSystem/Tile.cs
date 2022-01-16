using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameSystem
{
    public class TileEventArgs : EventArgs
    {
        public Tile Tile { get; }

        public TileEventArgs(Tile tile)
            => Tile = tile;
    }

    public class Tile : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler, ITile
    {
        public event EventHandler<TileEventArgs> Entered;
        public event EventHandler<TileEventArgs> Exited;

        [SerializeField] private UnityEvent OnActivate;
        [SerializeField] private UnityEvent OnDeactivate;

        public Tile Tile_ { get; set; }

        public bool Highlight
        {
            set
            {
                if (value)
                    OnActivate.Invoke();
                else
                    OnDeactivate.Invoke();
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
            => GameLoop.Instance.Highlight(this);

        public void OnPointerExit(PointerEventData eventData)
            => GameLoop.Instance.UnhighlightAll();

        protected virtual void OnEntered(TileEventArgs eventArgs)
        {
            EventHandler<TileEventArgs> handler = Entered;
            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnExited(TileEventArgs eventArgs)
        {
            EventHandler<TileEventArgs> handler = Exited;
            handler?.Invoke(this, eventArgs);
        }
    }
}