using SGF.Unity.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Snaker.Modules.ExampleA.UI
{
    public class UIMsgTips:UIWidget
    {
        public Text txtContent;
        public Image imgBackgroud;

        private float m_alpha = 1;

        protected override void OnOpen(object arg = null)
        {
            base.OnOpen(arg);
            txtContent.text = arg as string;

            m_alpha = 1;
            UpdateView();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            m_alpha -= 0.01f;
            if (m_alpha <= 0)
            {
                m_alpha = 0;
                this.Close();
            }

            UpdateView();
        }

        private void UpdateView()
        {
            Color c = imgBackgroud.color;
            c.a = m_alpha;
            imgBackgroud.color = c;
            
        }
    }
}