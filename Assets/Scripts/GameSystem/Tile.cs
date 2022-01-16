using CardSystem;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameSystem
{
    public class CardEnterTileEventArgs : EventArgs
    {
        public Tile Tile { get; }

        public ICard<Tile> Card { get; }

        public CardEnterTileEventArgs(ICard<Tile> card, Tile tile)
        {
            Card = card;
            Tile = tile;
        }
    }

    public class Tile : MonoBehaviour, IPointerClickHandler
    {
        public event EventHandler<CardEnterTileEventArgs> CardEnterTile;

        [SerializeField]
        private UnityEvent OnActivate;

        [SerializeField]
        private UnityEvent OnDeactivate;

       





        //[SerializeField]
        //private GameLoop _loop;

        /*   private Position _model;

           public Position Model
           {
               set
               {
                   if (_model != null)
                   {
                       _model.Activated -= PositionActivated;
                       _model.Deactivated -= PositionDeactivated;
                   }

                   _model = value;

                   if (_model != null)
                   {
                       _model.Activated += PositionActivated;
                       _model.Deactivated += PositionDeactivated;
                   }

               }
               get
               {
                   return _model;
               }
           }*/

        private void PositionDeactivated(object sender, EventArgs e)
            => OnDeactivate.Invoke();

        private void PositionActivated(object sender, EventArgs e)
            => OnActivate.Invoke();

        public void OnPointerClick(PointerEventData eventData)
        {
            FindObjectOfType<GameLoop>().DebugTile(this);
        }

        public void OnPointerEnterTile(PointerEventData eventData)
        {
            Debug.Log("lkd");
            if (eventData.dragging)
            {
                OnCardEnterTile(new CardEnterTileEventArgs(eventData.pointerDrag.GetComponent<ICard<Tile>>(), this));
            }
        }

         protected virtual void OnCardEnterTile(CardEnterTileEventArgs eventArgs)
         {
             var handler = CardEnterTile;
             handler?.Invoke(this, eventArgs);
         }









        /*public event EventHandler HighlightStatusChanged;

        [SerializeField]
        private UnityEvent OnActivate;

        private bool _isHighlighted = false;

        [SerializeField]
        private UnityEvent OnDeactivate;

        [SerializeField]
        private Material _highlightMaterial;
       
        [SerializeField]
        private Material _originalMaterial;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        public bool IsHighlighted
        {
            get => _isHighlighted;
            
            internal set
            {
                _isHighlighted = value;
                OnHighlightStatusChanged(EventArgs.Empty);
            }
        }

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

        public void OnPointerClick(PointerEventData eventData)
        //=> FindObjectOfType<GameLoop>().DebugTile(this);
        {
            if (_isHighlighted)
                _isHighlighted = false;
            else
                _isHighlighted = true;

            if (IsHighlighted)
                _meshRenderer.material = _highlightMaterial;
            else
                _meshRenderer.material = _originalMaterial;

            Debug.Log(_isHighlighted);
        }

        protected virtual void OnHighlightStatusChanged(EventArgs args)
        {
            EventHandler handler = HighlightStatusChanged;
            handler?.Invoke(this, args);
        }

        private void HighlightStatusChange(object sender, EventArgs e)
        {
            if (IsHighlighted)
                _meshRenderer.material = _highlightMaterial;
            else
                _meshRenderer.material = _originalMaterial;
        }*/
    }
}