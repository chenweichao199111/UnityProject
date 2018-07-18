using UnityEngine;
using UnityEngine.SceneManagement;

namespace Framework.Unity.Tools
{
    /// <summary>
    /// 传送门，用于切换场景
    /// </summary>
    public class SceneGate : MonoBehaviour
    {
        [Header("跳转场景")]
        public string mSceneName;
        private bool mEnter;

        public void OnTriggerEnter(Collider other)
        {
            mEnter = true;
            CancelInvoke("KeepStay");
            Invoke("KeepStay", 1f);
        }

        public void OnTriggerExit(Collider other)
        {
            mEnter = false;
            CancelInvoke("KeepStay");
        }

        private void KeepStay()
        {
            if (mEnter)
            {
                SceneListener.Instance.LoadScene(mSceneName, LoadSceneMode.Single);
            }
        }
    }
}