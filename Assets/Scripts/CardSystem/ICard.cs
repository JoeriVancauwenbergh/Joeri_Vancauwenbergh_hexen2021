using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    public interface ICard<TPostion>
    {
        public void SetActive(bool active);

        //public void SelectedCard();
        //public void Position(TPiece, TPosition);

        public void Execute(TPostion cardPosition);
    }
}