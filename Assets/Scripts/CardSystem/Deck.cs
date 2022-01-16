using System;
using System.Collections.Generic;
using UnityEngine;
using BoardSystem;

namespace CardSystem
{
    public class CardEventArgs<TCard> : EventArgs
    {
        public TCard Card { get; }

        public CardEventArgs(TCard card)
        {
            Card = card;
        }
    }

    public class Deck<TCard, TPiece, TPosition>
        where TCard : MonoBehaviour, ICard<TPiece, TPosition>
    {
        public event EventHandler<CardEventArgs<TCard>> CardPlayed;

        private Board<TPiece, TPosition> _board;
        private Grid<TPosition> _grid;

        private List<TCard> _cards = new List<TCard>();
        private int _amountInHandCards;

        public Deck(Board<TPiece, TPosition> board, Grid<TPosition> grid)
        {
            _board = board;
            _grid = grid;
        }

        public void Register(TCard card)
        {
            _cards.Add(card);
            card.Initialize(_board, _grid);
        }

        public void FillHand(int amountInHandCards)
        {
            int activeCards = 0;
            _amountInHandCards = amountInHandCards;

            foreach (TCard card in _cards)
            {
                if (activeCards == amountInHandCards)
                    break;

                if (!card.gameObject.activeInHierarchy)
                    card.gameObject.SetActive(true);

                activeCards++;
            }
        }

        public void PlayCard(TCard card, TPiece piece, TPosition position)
        {
            if (card.Execute(piece, position))
            {
                _cards.Remove(card);
                FillHand(_amountInHandCards);
            }
        }

        public void OnCardPlayed(CardEventArgs<TCard> eventArgs)
        {
            EventHandler<CardEventArgs<TCard>> handler = CardPlayed;
            handler?.Invoke(this, eventArgs);
        }
    }
}