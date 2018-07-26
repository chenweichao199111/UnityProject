using UnityEngine;
using UnityEngine.Events;

namespace Framework.Unity.Tools
{
    /// <summary>
    /// 传送门, 只暴露事件
    /// </summary>
    public class Teleport : MonoBehaviour
    {
        private bool mEnter;
        public UnityEvent mEnterTeleport;

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
                if (mEnterTeleport != null)
                {
                    mEnterTeleport.Invoke();
                }
            }
        }
    }
}