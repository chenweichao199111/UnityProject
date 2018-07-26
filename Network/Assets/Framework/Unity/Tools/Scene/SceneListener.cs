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
            string tempStr = string.Format("加载完{0}场景用时{1}秒", varScene.name, Time.realtimeSinceStartup - mLoadTime);
            Debuger.Log(tempStr);
            mLoadTime = Time.realtimeSinceStartup;
        }


        private void SceneUnloaded(Scene varScene)
        {
            string tempStr = string.Format("卸载完{0}场景用时{1}秒", varScene.name, Time.realtimeSinceStartup - mUnLoadTime);
            Debuger.Log(tempStr);

            mUnLoadTime = Time.realtimeSinceStartup;
            mLoadTime = Time.realtimeSinceStartup;
        }


        private void ActiveSceneChange(Scene var1, Scene var2)
        {
            Debuger.Log(string.Format("第一个场景是{0}", var1.name));
            Debuger.Log(string.Format("第二个场景是{0}", var2.name));
        }
    }
}
