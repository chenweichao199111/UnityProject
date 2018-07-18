using Framework.Pattern;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Unity.UI
{
    /// <summary>
    /// 单例模式的核心：
    /// 1.只在该类内定义一个静态的对象，该对象在外界访问，在内部构造
    /// 2.构造函数私有化
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {
        public enum CanvasType
        {
            World,
            Screen
        }

        public struct CanvasConfig
        {
            public string CanvasPath;
            public Transform canvasTransform;
            public Transform CanvasTransform
            {
                get
                {
                    if (canvasTransform == null)
                    {
                        canvasTransform = GameObject.Find(CanvasPath).transform;
                    }
                    return canvasTransform;
                }
            }
        }

        public override void Init()
        {
            mWorldCanvas.CanvasPath = "UI/Base";
            mScreenCanvas.CanvasPath = "UI/ScreenCanvas";
            ParseUIPanelTypeJson(); //构造该类时会解析Json
        }

        private Dictionary<UIPanelType, string> panelPathDict;//存储所有Perfab面板的路径
        private Dictionary<UIPanelType, BasePanel> panelDict;//借助BasePanel脚本保存所有实例化出来的面板物体（因为BasePanel脚本被所有面板预设物体的自己的脚本所继承，所以需要的时候可以根据BasePanel脚本来实例化每一个面板对象）
        private Stack<BasePanel> panelStack;//这是一个栈，用来保存实例化出来（显示出来）的面板

        private CanvasConfig mWorldCanvas = new CanvasConfig();
        private CanvasConfig mScreenCanvas = new CanvasConfig();

        //页面入栈，即把页面显示在界面上
        public BasePanel PushPanel(UIPanelType panelType, CanvasType uiStayCanvas)
        {
            if (panelStack == null)//如果栈不存在，就实例化一个空栈
            {
                panelStack = new Stack<BasePanel>();
            }
            if (panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();//取出栈顶元素保存起来，但是不移除
                if (topPanel != null)
                {
                    topPanel.OnPause();//使该页面暂停，不可交互
                }
                else
                {
                    panelStack.Pop();
                }
            }
            BasePanel panelTemp = GetPanel(panelType, uiStayCanvas);
            panelTemp.transform.SetAsLastSibling();
            panelTemp.OnEnter();//页面进入显示，可交互
            panelStack.Push(panelTemp);

            DebugUI();
            return panelTemp;
        }

        //页面出栈，把倒数第一个页面移除，显示倒数第二个页面
        public void PopPanel()
        {
            if (panelStack == null)
            {
                panelStack = new Stack<BasePanel>();
            }
            if (panelStack.Count <= 0) return;
            //关闭栈顶页面的显示
            BasePanel topPanel1 = panelStack.Pop();
            topPanel1.OnExit();

            DebugUI();
            if (panelStack.Count <= 0) return;
            BasePanel topPanel2 = panelStack.Peek();
            if (topPanel2 != null)
            {
                topPanel2.OnResume();//使第二个栈里的页面显示出来，并且可交互
            }
        }

        // 隐藏其他页面，显示指定的页面
        public BasePanel PopAllAndPushPanel(UIPanelType panelType, CanvasType uiStayCanvas)
        {
            if (panelStack == null)//如果栈不存在，就实例化一个空栈
            {
                panelStack = new Stack<BasePanel>();
            }

            var tempPanel = UIManager.Instance.GetPanelFromCache(panelType);
            if (tempPanel == null || !tempPanel.gameObject.activeSelf)
            {
                HideAll();
            }

            if (tempPanel == null)
            {
                tempPanel = UIManager.Instance.PushPanel(panelType, uiStayCanvas);
            }
            else
            {
                tempPanel.transform.SetAsLastSibling();
                tempPanel.OnEnter();//页面进入显示，可交互
                if (GetCurrentPanel() != tempPanel)
                {
                    panelStack.Push(tempPanel);
                }

                DebugUI();
            }

            
            return tempPanel;
        }

        /// <summary>
        /// 判断栈顶页面是否和指定页面相同，如果相同出栈，慎用
        /// </summary>
        /// <param name="varPanel"></param>
        public void PopPanel(BasePanel varPanel)
        {
            if (panelStack.Count <= 0) return;
            if (panelStack.Peek() == varPanel)
            {
                panelStack.Pop();
            }
            DebugUI();
        }

        /// <summary>
        /// 显示页面，栈不发生变化
        /// </summary>
        /// <param name="panelType"></param>
        /// <param name="uiStayCanvas"></param>
        /// <returns></returns>
        public BasePanel ShowPanel(UIPanelType panelType, CanvasType uiStayCanvas)
        {
            BasePanel panelTemp = GetPanel(panelType, uiStayCanvas);
            panelTemp.transform.SetAsLastSibling();
            panelTemp.OnEnter();//页面进入显示，可交互
            DebugUI();
            return panelTemp;
        }

        public BasePanel GetCurrentPanel()
        {
            if (panelStack.Count <= 0) return null;
            BasePanel topPanel = panelStack.Peek();
            return topPanel;
        }

        // 所有显示的页面隐藏
        public void HideAll()
        {
            if (panelDict != null)
            {
                foreach (var kvp in panelDict)
                {
                    if (kvp.Value != null)
                    {
                        kvp.Value.OnExit();
                    }
                }
            }
        }

        // 所有显示的页面隐藏，所有页面出栈
        public void PopAll()
        {
            if (panelDict != null)
            {
                foreach (var kvp in panelDict)
                {
                    if (kvp.Value != null)
                    {
                        kvp.Value.OnExit();
                    }
                }
            }

            if (panelStack != null)
            {
                panelStack.Clear();
            }
        }

        // 所有显示的页面摧毁，所有页面出栈
        public void DestroyAll()
        {
            if (panelDict != null)
            {
                foreach (var kvp in panelDict)
                {
                    if (kvp.Value != null)
                    {
                        kvp.Value.Destory();
                    }
                }

                panelDict.Clear();
            }

            if (panelStack != null)
            {
                panelStack.Clear();
            }
        }

        public GameObject GetLoadObject(UIPanelType panelType)
        {
            string path = null;
            panelPathDict.TryGetValue(panelType, out path);
            return (Resources.Load(path) as GameObject);
        }

        public Transform GetParent(CanvasType uiStayCanvas)
        {
            Transform tempParent = null;
            if (uiStayCanvas == CanvasType.World)
            {
                tempParent = this.mWorldCanvas.CanvasTransform;
            }
            else if (uiStayCanvas == CanvasType.Screen)
            {
                tempParent = this.mScreenCanvas.CanvasTransform;
            }
            return tempParent;
        }

        // 加载面板显示在屏幕，但不存在栈中
        public BasePanel LoadPanel(UIPanelType panelType)
        {
            BasePanel tempPanel = GetPanel(panelType, CanvasType.Screen);
            tempPanel.OnEnter();
            return tempPanel;
        }

        //根据面板类型UIPanelType得到实例化的面板
        private BasePanel GetPanel(UIPanelType panelType, CanvasType uiStayCanvas)
        {
            if (panelDict == null)//如果panelDict字典为空，就实例化一个空字典
            {
                panelDict = new Dictionary<UIPanelType, BasePanel>();
            }
            BasePanel panel = null;
            panelDict.TryGetValue(panelType, out panel);//不为空就根据类型得到Basepanel
            if (panel == null)//如果得到的panel为空，那就去panelPathDict字典里面根据路径path找到，然后加载，接着实例化
            {
                string path = null;
                panelPathDict.TryGetValue(panelType, out path);
                GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;//根据路径加载并实例化面板
                if (uiStayCanvas == CanvasType.World)
                {
                    instPanel.transform.SetParent(this.mWorldCanvas.CanvasTransform, false);//设置为Canvas的子物体,false表示实例化的子物体坐标以Canvas为准
                }
                else if (uiStayCanvas == CanvasType.Screen)
                {
                    instPanel.transform.SetParent(this.mScreenCanvas.CanvasTransform, false);//设置为Canvas的子物体,false表示实例化的子物体坐标以Canvas为准
                }

                //TODO
                panel = instPanel.GetComponent<BasePanel>();
                if (panelDict.ContainsKey(panelType))
                {
                    panelDict[panelType] = panel;
                }
                else
                {
                    panelDict.Add(panelType, panel);
                }
            }
            return panel;
        }

        public BasePanel GetPanelFromCache(UIPanelType panelType)
        {
            if (panelDict == null)
            {
                return null;
            }
            BasePanel panel = null;
            panelDict.TryGetValue(panelType, out panel);//不为空就根据类型得到Basepanel
            return panel;
        }

        //解析UIPanelType.json的信息
        private void ParseUIPanelTypeJson()
        {
            panelPathDict = new Dictionary<UIPanelType, string>();//实例化一个字典对象
            TextAsset ta = Resources.Load<TextAsset>("UIPanelType"); //获取UIPanelType.json文件的文本信息
            UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);//把UIPanel.json文本信息转化为一个内部类的对象，对象里面的链表里面对应的是每个Json信息对应的类

            for (int i = 0; i < jsonObject.infoList.Count; i++)
            {
                UIPanelInfo tempInfo = jsonObject.infoList[i];
                panelPathDict.Add(tempInfo.panelType, tempInfo.path);//把每一个进过json文件转化过来的类存入字典里面(键值对的形式)
            }

        }

        public void DebugUI()
        {
            /*
            if (panelStack != null && panelStack.Count > 0)
            {
                string tempStr = "Debug开始\n";
                foreach (var target in panelStack)
                {
                    if (target != null)
                    {
                        tempStr = string.Format("{0}{1}", tempStr, target.name);
                    }
                }
                Debug.Log(tempStr);
            }
            else
            {
                Debug.Log("UI堆栈是空");
            }
            */
        }
    }
}