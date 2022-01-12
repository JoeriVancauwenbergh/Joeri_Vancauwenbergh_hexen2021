using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        public void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}