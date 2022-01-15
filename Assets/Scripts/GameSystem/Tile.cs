using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameSystem
{
    public class Tile : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private UnityEvent OnActivate;

        [SerializeField]
        private UnityEvent OnDeactivate;

        public void OnPointerClick(PointerEventData eventData)
            =>FindObjectOfType<GameLoop>().DebugTile(this);

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
    }
}