using UnityEngine;

namespace Framework.Unity
{
    // 主程序启动类
    public class AppMain : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            GameObject tempSingleObj = new GameObject(typeof(AppMain).Name);
            //tempSingleObj.hideFlags = HideFlags.HideInHierarchy;
            AppMain tempScript = tempSingleObj.AddComponent<AppMain>();
            GameObject.DontDestroyOnLoad(tempSingleObj);

#if UNITY_EDITOR
            tempScript.OpenLog();
#else
            tempScript.CloseLog();
#endif
        }

        private void OpenLog()
        {
            Debuger.EnableLog = true;
            Debug.unityLogger.logEnabled = true;
            gameObject.AddComponent<DebugShow>();
        }

        private void CloseLog()
        {
            Debuger.EnableLog = false;
            Debug.unityLogger.logEnabled = false;
        }
    }
}