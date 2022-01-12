using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem
{
    public class Pawn : MonoBehaviour, IPiece<Tile>
    {
        public void OnMoved(Tile position)
        {
            transform.position = position.transform.position;
        }

        public void OnTaken()
        {
            gameObject.SetActive(false);
        }
    }
}
