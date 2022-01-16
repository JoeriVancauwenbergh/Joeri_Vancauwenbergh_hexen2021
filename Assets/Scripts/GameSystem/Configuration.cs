using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardSystem;

namespace GameSystem
{
    [CreateAssetMenu(menuName = "Hexen/Configuration")]

    public class Configuration : ScriptableObject
    {
        [SerializeField]
        internal float TileDimension;

        [SerializeField]
        internal Pawn Player;

        [SerializeField]
        internal Pawn Enemy;

        [SerializeField]
        internal int BoardRadius;

        [SerializeField]
        internal int DeckSize;

        [SerializeField]
        internal int AmountCardTypes;

        [SerializeField]
        internal int AmountInHandCards;

        [SerializeField]
        internal PushbackCard PushbackCard;

        [SerializeField]
        internal SlashCard SlashCard;

        [SerializeField]
        internal SwipeCard SwipeCard;

        [SerializeField]
        internal TeleportCard TeleportCard;
    }
}