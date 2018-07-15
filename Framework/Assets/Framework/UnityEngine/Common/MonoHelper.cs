/*
 * 描述：
 * 作者：slicol
*/
using System;
using System.Collections;
using UnityEngine;

namespace Framework.UnityEngine
{
    public delegate void MonoUpdaterEvent();

    public class MonoHelper : MonoSingleton<MonoHelper>
    {

        //===========================================================

        private event MonoUpdaterEvent UpdateEvent;
        private event MonoUpdaterEvent FixedUpdateEvent;

        public static void AddUpdateListener(MonoUpdaterEvent listener)
        {
            if (Instance != null)
            {
                Instance.UpdateEvent += listener;
            }
        }

        public static void RemoveUpdateListener(MonoUpdaterEvent listener)
        {
            if (Instance != null)
            {
                Instance.UpdateEvent -= listener;
            }
        }

        public static void AddFixedUpdateListener(MonoUpdaterEvent listener)
        {
            if (Instance != null)
            {
                Instance.FixedUpdateEvent += listener;
            }
        }

        public static void RemoveFixedUpdateListener(MonoUpdaterEvent listener)
        {
            if (Instance != null)
            {
                Instance.FixedUpdateEvent -= listener;
            }
        }

        void Update()
        {
            if (UpdateEvent != null)
            {
                try
                {
                    UpdateEvent();
                }
                catch (Exception e)
                {
                    string tempStr = string.Format("MonoHelper Update() Error:{0}\n{1}", e.Message, e.StackTrace);
                    Debuger.LogError(tempStr);
                }
            }
        }

        void FixedUpdate()
        {
            if (FixedUpdateEvent != null)
            {
                try
                {
                    FixedUpdateEvent();
                }
                catch (Exception e)
                {
                    string tempStr = string.Format("MonoHelper FixedUpdate() Error:{0}\n{1}", e.Message, e.StackTrace);
                    Debuger.LogError(tempStr);
                }
            }
        }

        //===========================================================

        public static void StartCoroutine(IEnumerator routine)
        {
            MonoBehaviour mono = Instance;
            mono.StartCoroutine(routine);
        }
    }
}
