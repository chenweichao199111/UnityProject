using UnityEngine;

namespace Framework.Unity.UI
{
    public class UILoad : MonoBehaviour
    {
        public UIPanelType mPanelType;
        public UIManager.CanvasType mUIStayCanvas = UIManager.CanvasType.World;

        void Start()
        {
            UIManager.Instance.PushPanel(mPanelType, mUIStayCanvas);
        }
    }
}
