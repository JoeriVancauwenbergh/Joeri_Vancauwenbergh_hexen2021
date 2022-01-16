using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardSystem;

namespace GameSystem
{
    public class PushbackCard : MonoBehaviour, ICard<Tile>
    {
        [SerializeField]
        private Configuration _configuration;

        public void SetActive(bool active)
            => gameObject.SetActive(active);

        public void Execute(Tile cardPosition)
        {


        }
    }
}