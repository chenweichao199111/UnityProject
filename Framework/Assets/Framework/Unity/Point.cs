using UnityEngine;

namespace Framework
{
    [System.Serializable]
    public struct Point
    {
        public Vector3 mPosition;
        public Vector3 mEulerAngles;
        public Vector3 mScale;

        public void Init(Transform varTrans)
        {
            mPosition = varTrans.position;
            mEulerAngles = varTrans.eulerAngles;
            mScale = varTrans.localScale;
        }

        public void SetTransform(Transform varTrans)
        {
            varTrans.position = mPosition;
            varTrans.eulerAngles = mEulerAngles;
            varTrans.localScale = mScale;
        }
    }
}