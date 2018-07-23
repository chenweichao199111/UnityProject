using System;
using SGF;
using SGF.Module;
using SGF.Time;
using SGF.Unity.UI;
using Snaker.Services.Online;
using UnityEngine;

namespace Snaker
{
    public class AppMain:MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            //初始化时间
            SGFTime.DateTimeAppStart = DateTime.Now;

            //初始化Debuger
            InitDebuger();

            //初始化AppConfig
            AppConfig.Init();

            //初始化版本
            InitVersion();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.playmodeStateChanged -= OnEditorPlayModeChanged;
            UnityEditor.EditorApplication.playmodeStateChanged += OnEditorPlayModeChanged;
#endif
        }


#if UNITY_EDITOR
        private void OnEditorPlayModeChanged()
        {
            if (Application.isPlaying == false)
            {
                UnityEditor.EditorApplication.playmodeStateChanged -= OnEditorPlayModeChanged;
                //退出游戏逻辑
                Exit("Editor的播放模式变化！");
            }
        }
#endif

        private void Exit(string reason)
        {
            //清理模块管理器
            ModuleManager.Instance.Clean();
            //清理UI管理器
            UIManager.Instance.Clean();
            //清理在线管理器
            OnlineManager.Instance.Clean();
            //清楚IRL管理器
            //清楚版本管理器
        }


        private void InitDebuger()
        {
            //初始化Debuger的日志开关
            Debuger.Init(Application.persistentDataPath + "/DebugerLog/", new UnityDebugerConsole());
            Debuger.EnableLog = true;
            Debuger.EnableSave = true;
            Debuger.Log();
        }

        private void InitVersion()
        {
            //进行版本更新

            //当版本更新完成后，或者版本检查完成后，初始化服务层的模块
            InitServices();
        }

        private void InitServices()
        {
            //初始化ILRuntime

            //初始化模块管理器
            ModuleManager.Instance.Init();
            ModuleManager.Instance.RegisterModuleActivator(new NativeModuleActivator(ModuleDef.Namespace,
                ModuleDef.NativeAssemblyName));

            //初始化UI管理器
            UIManager.Instance.Init("UI/");
            UIManager.SceneLoading = "UISceneLoading";

            //初始化在线管理器
            OnlineManager.Instance.Init();

            //显示登录界面

            //如果登录成功了，初始化普通业务模块
            GlobalEvent.onLogin += OnLogin;


            //显示ExampleA
            ModuleManager.Instance.CreateModule("ExampleAModule");
            ModuleManager.Instance.ShowModule("ExampleAModule");
        }


        private void OnLogin(bool success)
        {
            GlobalEvent.onLogin -= OnLogin;

            if (success)
            {
                //隐藏登录界面

                //通过ILRScript来启动业务模块
                /*
                bool ret = ILRManager.Instance.Invoke("Snaker.ScriptMain", "Init");
                if (ret)
                {
                    //显示“初始化业务模块失败！”
                }
                */
                
            }
            else
            {
                //显示“登录失败！”
            }
        }




        private void Update()
        {
            GlobalEvent.onUpdate.Invoke();
        }

        private void FixedUpdate()
        {
            GlobalEvent.onFixedUpdate.Invoke();
        }


    }
}