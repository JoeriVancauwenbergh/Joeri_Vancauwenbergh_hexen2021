using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem
{
    public class Tile : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
            =>FindObjectOfType<GameLoop>().DebugTile(this);
    }
}