using SGF;
using SGF.Module;
using SGF.Unity.UI;

namespace Snaker.Modules
{
    public class ExampleAModule:GeneralModule
    {

        public override void Create(object args = null)
        {
            base.Create(args);

            Debuger.Log("该模式被创建了！！");
        }


        protected override void Show(object arg)
        {
            base.Show(arg);

            UIManager.Instance.OpenPage("ExampleA/UIExampleAPage");
        }



    }
}