using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace My.Unity.Commons
{
    /// <summary>
    /// Inherit from this base class to create a singleton.
    /// e.g. public class MyClassName : Singleton<MyClassName> {}
    /// </summary>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        // Check to see if we're about to be destroyed.
        private static bool m_ShuttingDown = false;
        private static object m_Lock = new object();
        private static T m_Instance;

        /// <summary>
        /// Access singleton instance through this propriety.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (m_ShuttingDown)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                        "' already destroyed. Returning null.");
                    return null;
                }

                lock (m_Lock)
                {
                    if (m_Instance == null)
                    {
                        // Search for existing instance.
                        m_Instance = (T)FindObjectOfType(typeof(T));

                        // Create new instance if one doesn't already exist.
                        if (m_Instance == null)
                        {
                            // Need to create a new GameObject to attach the singleton to.
                            var singletonObject = new GameObject();
                            m_Instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";

                            // Make instance persistent.
                            //DontDestroyOnLoad(singletonObject);
                        }

                        if (m_Instance.GetComponent<DontDestroyOnLoad>() == null)
                            m_Instance.gameObject.AddComponent<DontDestroyOnLoad>();
                    }

                    return m_Instance;
                }
            }
        }


        private void OnApplicationQuit()
        {
#if !UNITY_EDITOR
            m_ShuttingDown = true;
#endif
        }


        private void OnDestroy()
        {
#if !UNITY_EDITOR
            m_ShuttingDown = true;
#endif
        }

    }
}