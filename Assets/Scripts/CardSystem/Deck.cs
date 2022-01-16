using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    public class Deck<TCard, TPosition>
        where TCard : ICard<TPosition>
    {
        private List<TCard> _deck = new List<TCard>();
        private List<TCard> _hand = new List<TCard>();
        private bool _isInHand;

        public void FillDeck(TCard card)
        {
            card.SetActive(false);
            _deck.Add(card);
        }

        public void FillHand(int amountInHandCards)
        {
            for(int count = 0; count < amountInHandCards; count++)
            {
                _deck[count].SetActive(true);
            }
        }

        // DEBUG /////////////////////////////////////////////////////////////////////////////////////////////////
        public void DebugDeck()
        {
            foreach (TCard card in _deck)
                Debug.Log(card);
        }
    }
}