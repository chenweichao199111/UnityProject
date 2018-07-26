using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.Unity.UGUI_Expand
{
    public class TweenMove : MonoBehaviour
    {
        public Vector3 mStartPos = Vector3.zero;
        public Vector3 mEndPos = Vector3.zero;
        public float mDuration = 0.2f;
        public RectTransform mRectTrans;
        [Header("移动完成")]
        public UnityEvent onMoveFinish;

        private void OnEnable()
        {
            if (mRectTrans != null)
            {
                mRectTrans.anchoredPosition3D = mStartPos;
            }
        }

        public void Play()
        {
            // 按钮移动到目标位置;
            if (mRectTrans != null)
            {
                mRectTrans.DOAnchorPos3D(mEndPos, mDuration).OnComplete(Finish);
            }
        }

        private void Finish()
        {
            if (onMoveFinish != null)
            {
                onMoveFinish.Invoke();
            }
        }
    }
}
