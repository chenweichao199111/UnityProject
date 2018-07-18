using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework.Unity.UGUI_Expand
{
    public class TweenScaleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Vector3 mStartScale = Vector3.one;
        public Vector3 mEndScale = 1.2f * Vector3.one;
        public float mDuration = 0.2f;

        public void OnPointerEnter(PointerEventData eventData)
        {
            // 按钮放大;
            transform.DOScale(mEndScale, mDuration);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // 按钮变小;
            transform.DOScale(mStartScale, mDuration);
        }
    }
}
