using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    [CreateAssetMenu(menuName = "Hexen/Configuration")]

    public class Configuration : ScriptableObject
    {
        [SerializeField]
        internal float TileDimension;

        [SerializeField]
        internal int BoardRadius;
    }
}