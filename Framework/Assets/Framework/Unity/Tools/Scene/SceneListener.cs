using Framework.Pattern;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Framework.Unity.Tools
{
    public class SceneListener : Singleton<SceneListener>
    {
        float mLoadTime;
        float mUnLoadTime;

        public SceneListener()
        {
            RefreshTime();

            SceneManager.sceneLoaded += SceneLoaded;
            SceneManager.sceneUnloaded += SceneUnloaded;
            //SceneManager.activeSceneChanged += ActiveSceneChange;
            // 加载场景顺序是SceneUnloaded, ActiveSceneChange, SceneLoaded
        }

        public void RefreshTime()
        {
            mLoadTime = Time.realtimeSinceStartup;
            mUnLoadTime = Time.realtimeSinceStartup;
        }

        public void LoadScene(string varSceneName, LoadSceneMode varMode)
        {
            if (Application.isEditor)
            {
                RefreshTime();
            }
            
            SceneManager.LoadScene(varSceneName, varMode);
        }

        public void LoadSceneAsync(string varSceneName, LoadSceneMode varMode)
        {
            if (Application.isEditor)
            {
                RefreshTime();
            }
            SceneManager.LoadSceneAsync(varSceneName, varMode);
        }


        private void SceneLoaded(Scene varScene, LoadSceneMode varMode)
        {
            Debug.LogFormat("加载完{0}场景用时{1}秒", varScene.name, Time.realtimeSinceStartup - mLoadTime);
            mLoadTime = Time.realtimeSinceStartup;
        }


        private void SceneUnloaded(Scene varScene)
        {
            Debug.LogFormat("卸载完{0}场景用时{1}秒", varScene.name, Time.realtimeSinceStartup - mUnLoadTime);
            mUnLoadTime = Time.realtimeSinceStartup;
            mLoadTime = Time.realtimeSinceStartup;
        }


        private void ActiveSceneChange(Scene var1, Scene var2)
        {
            Debug.Log("第一个场景是" + var1.name);
            Debug.Log("第二个场景是" + var2.name);
        }
    }
}
