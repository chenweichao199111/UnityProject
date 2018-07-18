using UnityEngine; 

namespace Framework.Unity.Tools
{
    public class FollowObj : MonoBehaviour
    {
        [Header("要跟随的物体")]
        public Transform mFollowTrans;

        private void Update()
        {
            if (mFollowTrans != null)
            {
                transform.position = mFollowTrans.position;
                transform.eulerAngles = mFollowTrans.eulerAngles;
            }
        }
    }
}
