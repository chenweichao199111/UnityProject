using SGF;
using SGF.Unity.UI;
using UnityEngine.UI;

namespace Snaker.Modules.ExampleA.UI
{
    public class UIMsgBox:UIWindow
    {
        public Text txtContent;

        protected override void OnOpen(object arg = null)
        {
            base.OnOpen(arg);
            this.AddUIClickListener("btnOK", OnBtnOK);
            txtContent.text = arg as string;
            
        }

        private void OnBtnOK()
        {
            Debuger.Log("你点击了OK按钮！");
        }
    }
}