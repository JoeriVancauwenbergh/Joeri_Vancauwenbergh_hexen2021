using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardSystem;
using BoardSystem;

namespace GameSystem
{
    public class TeleportCard : MonoBehaviour, ICard<Tile>
    {
        [SerializeField]
        private Configuration _configuration;


        public Board<Pawn,Tile> Board { get; }

        public void SetActive(bool active)
            => gameObject.SetActive(active);

        public void Execute(Tile cardPosition)
        {
            Board.MoveTo(_configuration.Player, cardPosition);
        }
    }
}