using UnityEngine;

namespace Framework.Unity.Tools
{
    public class FollowCamera : MonoBehaviour
    {
        private Transform mTrans;
        private Transform mMainCameraTrans;

        // Use this for initialization
        void Start()
        {
            mTrans = transform;
            Camera tempC = Camera.main;
            if (tempC != null)
            {
                mMainCameraTrans = tempC.transform;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (mMainCameraTrans == null)
            {
                Camera tempC = Camera.main;
                if (tempC == null)
                {
                    return;
                }
                mMainCameraTrans = tempC.transform;
            }

            Vector3 relativePos = mTrans.position - mMainCameraTrans.position;
            Quaternion lookAtRotation = Quaternion.LookRotation(relativePos, Vector3.up);
            mTrans.rotation = Quaternion.Lerp(mTrans.rotation, lookAtRotation, 1.5f * Time.deltaTime);
        }
    }
}
