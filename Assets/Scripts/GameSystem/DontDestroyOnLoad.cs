using UnityEngine;

namespace GameSystem
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        public void Start()
            => DontDestroyOnLoad(gameObject);
    }
}