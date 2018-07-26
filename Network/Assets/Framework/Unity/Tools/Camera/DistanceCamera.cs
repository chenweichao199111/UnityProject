using UnityEngine;

namespace Framework.Unity.Tools
{
    public class DistanceCamera : MonoBehaviour
    {
        Camera mCamera;
        [Header("最低高度,防止物体插入地面看不到")]
        public float mMinY = 2.3f;
        [Header("相机位置偏移")]
        public Vector3 mOffset;
        [Header("角度是否要朝向摄像机")]
        public bool mForwardCamera;
        [Header("是否每帧更新")]
        public bool mUpdate;

        private void OnDisable()
        {
            mCamera = null;
        }

        private void Update()
        {
            if (mCamera == null)
            {
                mCamera = Camera.main;
                if (!mUpdate)
                {
                    GotoDistanceCamera();
                }
            }

            if (mUpdate)
            {
                GotoDistanceCamera();
            }
        }

        public void GotoDistanceCamera()
        {
            if (mCamera == null)
            {
                return;
            }
            Vector3 relativePos = (mCamera.transform.rotation * Vector3.forward).normalized * 0.5f;

            Vector3 tempPos = mCamera.transform.position + relativePos;
            transform.position = tempPos;

            Vector3 tempLocalPos = transform.localPosition;
            tempLocalPos += mOffset;
            if (tempLocalPos.y < mMinY)
            {
                tempLocalPos.y = mMinY;
            }

            transform.localPosition = tempLocalPos;
            if (mForwardCamera)
            {
                transform.eulerAngles = mCamera.transform.eulerAngles;
            }
        }
    }
}
