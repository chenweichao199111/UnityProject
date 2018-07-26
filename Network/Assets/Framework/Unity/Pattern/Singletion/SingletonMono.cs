using System.Runtime.InteropServices;
using UnityEngine;

namespace Framework.Unity.Pattern
{
    /// <summary>
    /// MonoBehaviour单例模式
    /// </summary>
    public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static object _lock = new object();

        public static T Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    Debuger.LogWarning("[Singleton] Instance '" + typeof(T) +
                        "' already destroyed on application quit." +
                        " Won't create again - returning null.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = GameObject.FindObjectOfType<T>();
                        GameObject singleton = null;
                        if (_instance == null)
                        {
                            singleton = new GameObject(typeof(T).Name);
                            _instance = singleton.AddComponent<T>();
                        }
                        else
                        {
                            singleton = _instance.gameObject;
                        }

                        DontDestroyOnLoad(singleton);
                    }

                    return _instance;
                }
            }
            set
            {
                if (_instance != null)
                {
                    Destroy(value);
                    return;
                }
                _instance = value;
                DontDestroyOnLoad(_instance);
            }
        }

        public static bool applicationIsQuitting = false;


        /// <summary>  
        /// When Unity quits, it destroys objects in a random order.  
        /// In principle, a Singleton is only destroyed when application quits.  
        /// If any script calls Instance after it have been destroyed,  
        ///   it will create a buggy ghost object that will stay on the Editor scene  
        ///   even after stopping playing the Application. Really bad!  
        /// So, this was made to be sure we're not creating that buggy ghost object.  
        /// </summary>  
        public virtual void OnDestroy()
        {
            applicationIsQuitting = true;
        }
    }
}