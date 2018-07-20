using UnityEngine;

namespace Framework.Unity
{
    [System.Serializable]
    public struct Point
    {
        [SerializeField]
        public Vector3 mPosition;
        [SerializeField]
        public Vector3 mEulerAngles;
        [SerializeField]
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