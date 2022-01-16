using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardSystem;

namespace GameSystem
{
    public class PushbackCard : MonoBehaviour, ICard<Tile>
    {
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SelectedCard()
        {

        }

        public List<Tile> Positions(Tile atPosition)
        {
            return null;
        }

        public void Execute(Tile atPosition)
        {

        }
    }
}