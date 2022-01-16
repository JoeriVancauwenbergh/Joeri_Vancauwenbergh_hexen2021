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

        public Board<Pawn, Tile> Board { get; set; }
        public Grid<Tile> Grid { get; set; }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SelectedCard()
        {

        }

        public List<Tile> Positions(Tile atPosition)
        {
            List<Tile> positions = new List<Tile>();
            
            if (!Board.TryGetPosition(_configuration.Player, out var playerPosition)
                && !Board.TryGetPosition(_configuration.Enemy, out var enemyPosition))
            {
                positions.Add(atPosition);

                Debug.Log(atPosition);
            }

            return positions;
        }

        public void Execute(Tile atPosition)
        {

        }
    }
}