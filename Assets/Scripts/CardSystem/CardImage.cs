using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardSystem
{
    public class CardImage: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        private Canvas _canvas;
        private GameObject _draggingIcon;
        private RectTransform _draggingPlane;
        private bool _isDraggingOnSurfaces = true;

        void Start()
        {
            _canvas = FindObjectOfType<Canvas>();
            transform.SetParent(_canvas.transform.GetChild(0));
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_canvas == null)
                return;

            // We have clicked something that can be dragged.
            // What we want to do is create an icon for this.
            _draggingIcon = new GameObject("icon");

            _draggingIcon.transform.SetParent(_canvas.transform, false);
            _draggingIcon.transform.SetAsLastSibling();
            _draggingIcon.transform.localScale = new Vector3(0.145f, 0.145f, 1f);

            var image = _draggingIcon.AddComponent<Image>();

            image.sprite = GetComponent<Image>().sprite;
            image.SetNativeSize();

            if (_isDraggingOnSurfaces)
                _draggingPlane = transform as RectTransform;
            else
                _draggingPlane = _canvas.transform as RectTransform;

            SetDraggedPosition(eventData);
        }

        public void OnDrag(PointerEventData data)
        {
            if (_draggingIcon != null)
                SetDraggedPosition(data);
        }

        private void SetDraggedPosition(PointerEventData data)
        {
            if (_isDraggingOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
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
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                Debug.Log("Dropped object was: " + eventData.pointerDrag);
            }
        }
    }
}