using UnityEngine;

namespace Framework.Unity.UI
{
    public class UGUIPanel : BasePanel
    {
        public CanvasGroup mCanvasGroup;

        /// <summary>
        /// 页面进入显示，可交互
        /// </summary>
        public override void OnEnter()
        {
            //EventEnable(true);

            gameObject.SetActive(true);
        }

        /// <summary>
        /// 页面暂停（弹出了其他页面），不可交互
        /// </summary>
        public override void OnPause()
        {
            EventEnable(false);
        }

        /// <summary>
        /// 页面继续显示（其他页面关闭），可交互
        /// </summary>
        public override void OnResume()
        {
            EventEnable(true);

            gameObject.SetActive(true);
        }

        /// <summary>
        /// 本页面被关闭（移除），不再显示在界面上
        /// </summary>
        public override void OnExit()
        {
            gameObject.SetActive(false);
        }

        public override void Destory()
        {
            GameObject.Destroy(gameObject);
        }

        protected void EventEnable(bool enabled)
        {
            if (mCanvasGroup != null)
            {
                mCanvasGroup.blocksRaycasts = enabled;
            }
        }

        private void DelayEventEnable()
        {
            EventEnable(true);
        }
    }
}
