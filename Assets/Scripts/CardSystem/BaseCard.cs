using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using BoardSystem;

namespace CardSystem
{
    public class BaseCard<TPiece, TPosition> :
        MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, ICard<TPiece, TPosition>
    {
        protected Board<TPiece, TPosition> _board;
        protected Grid<TPosition> _grid;
        protected List<TPosition> _validTiles = new List<TPosition>();

        public bool DragOnSurfaces = true;

        private GameObject _draggingIcon;
        private RectTransform _draggingPlane;

        public void Initialize(Board<TPiece, TPosition> board, Grid<TPosition> grid)
        {
            _board = board;
            _grid = grid;

            gameObject.SetActive(false);
        }

        public virtual bool Execute(TPiece piece, TPosition position)
        {
            gameObject.SetActive(false);

            return true;
        }

        public virtual List<TPosition> Positions(TPiece piece, TPosition position)
        {
            throw new NotImplementedException();
        }

        protected void TakePiecesOnValidTiles()
        {
            foreach (TPosition tile in _validTiles)
            {
                if (_board.TryGetPiece(tile, out TPiece pieceInRange))
                    _board.Take(pieceInRange);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var canvas = FindInParents<Canvas>(gameObject);
            if (canvas == null)
                return;

            // We have clicked something that can be dragged.
            // What we want to do is create an icon for this.
            _draggingIcon = new GameObject("icon");

            _draggingIcon.transform.SetParent(canvas.transform, false);
            _draggingIcon.transform.SetAsLastSibling();
            _draggingIcon.transform.localScale = new Vector3 (0.148f, 0.148f, 1f);

            var image = _draggingIcon.AddComponent<Image>();

            image.sprite = GetComponent<Image>().sprite;
            image.SetNativeSize();

            if (DragOnSurfaces)
                _draggingPlane = transform as RectTransform;
            else
                _draggingPlane = canvas.transform as RectTransform;

            SetDraggedPosition(eventData);
        }

        public void OnDrag(PointerEventData data)
        {
            if (_draggingIcon != null)
                SetDraggedPosition(data);
        }

        private void SetDraggedPosition(PointerEventData data)
        {
            if (DragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
                _draggingPlane = data.pointerEnter.transform as RectTransform;

            var rt = _draggingIcon.GetComponent<RectTransform>();
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggingPlane, data.position, data.pressEventCamera, out globalMousePos))
            {
                rt.position = globalMousePos;
                rt.rotation = _draggingPlane.rotation;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_draggingIcon != null)
                Destroy(_draggingIcon);
            //////////////////////////////////////////////////////////////////
            this.gameObject.SetActive(false);
            //////////////////////////////////////////////////////////////////
        }

        static public T FindInParents<T>(GameObject go) where T : Component
        {
            if (go == null) return null;
            var comp = go.GetComponent<T>();

            if (comp != null)
                return comp;

            Transform t = go.transform.parent;
            while (t != null && comp == null)
            {
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
            return comp;
        }
    }
}