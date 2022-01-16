using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    public interface ICard<TPosition>
    {
        public void SetActive(bool active);

        public void SelectedCard();

        public List<TPosition> Positions (TPosition atPosition);

        public void Execute(TPosition atPosition);
    }
}