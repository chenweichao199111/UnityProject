using SGF.Unity.UI;

namespace Snaker.Modules.ExampleA.UI
{
    public class UIExampleAPage:UIPage
    {
        protected override void OnOpen(object arg = null)
        {
            base.OnOpen(arg);

            this.AddUIClickListener("btnShowMsgBox", OnBtnShowMsgBox);
            this.AddUIClickListener("btnShowMsgTips", OnBtnShowMsgTips);
        }

        private void OnBtnShowMsgBox()
        {
            UIManager.Instance.OpenWindow("ExampleA/UIMsgBox","我是一个MsgBox");
        }

        private void OnBtnShowMsgTips()
        {
            UIManager.Instance.OpenWidget("ExampleA/UIMsgTips", "我是一个Tips");
        }
    }
}