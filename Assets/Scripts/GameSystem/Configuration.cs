using UnityEngine;

namespace GameSystem
{
    [CreateAssetMenu(menuName = "Hexen/Configuration")]

    public class Configuration : ScriptableObject
    {
        [SerializeField] internal float TileDimension;
        [SerializeField] internal Pawn<Tile> Player;
        [SerializeField] internal Pawn<Tile> Enemy;
        [SerializeField] internal int BoardRadius;
        [SerializeField] internal int DeckSize;
        [SerializeField] internal int AmountCardTypes;
        [SerializeField] internal int AmountInHandCards;
        [SerializeField] internal PushbackCard PushbackCard;
        [SerializeField] internal SlashCard SlashCard;
        [SerializeField] internal SwipeCard SwipeCard;
        [SerializeField] internal TeleportCard TeleportCard;
    }
}