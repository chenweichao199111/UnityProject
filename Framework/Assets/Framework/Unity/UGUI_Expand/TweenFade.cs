using DG.Tweening;
using UnityEngine;

namespace Framework.Unity.UGUI_Expand
{
    public class TweenFade : MonoBehaviour
    {
        [Header("起始大小")]
        public Vector3 mStartScale = Vector3.zero;
        [Header("结束大小")]
        public Vector3 mEndScale = Vector3.one;
        [Header("持续时间")]
        public float mDuration = 0.2f;

        private void OnEnable()
        {
            transform.localScale = mStartScale;
            transform.DOScale(mEndScale, mDuration);
        }

        public void PlayReverse()
        {
            if (gameObject.activeSelf)
            {
                transform.localScale = mEndScale;
                transform.DOScale(mStartScale, mDuration).onComplete = PlayReverseFinish;
            }
        }

        private void PlayReverseFinish()
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
