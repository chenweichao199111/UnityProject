using UnityEngine;

namespace Framework.Unity
{
    public class AppStart : MonoBehaviour
    {
        static GameObject mSingleObj;

        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            mSingleObj = new GameObject(typeof(AppStart).Name, typeof(AppStart));
            //mSingleObj.hideFlags = HideFlags.HideInHierarchy;
            GameObject.DontDestroyOnLoad(mSingleObj);

#if UNITY_EDITOR
            // 如果使用Debuger.Log or Debuger.LogWarnning or Debuger.LogError打印日志, 你可以一键关闭这些日志Hidebug.EnableDebuger(false).
            HiDebug.EnableDebuger(true);
            // 打印日志到屏幕
            HiDebug.EnableOnScreen(true);
            // 将会记录日志和堆栈信息到text,默认路径是Application.persistentDataPath
            HiDebug.EnableOnText(true);
            HiDebug.FontSize = 20;
#else
            HiDebug.EnableDebuger(false);
#endif
        }

        public static void SetParent(GameObject varTarget)
        {
            varTarget.transform.parent = mSingleObj.transform;
        }
    }
}