using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZsCLJX
{
    public class ActivePlayer : MonoBehaviour, IDownRise
    {
        [Serializable]
        public sealed class StayEvent : UnityEvent<GameObject> { }

        public DownRiseGrid mCurrentGrid;
        public DownRiseGridRelation mPathConfig;
        public Vector3 faceRight = new Vector3(0, 90, 0);
        public Vector3 faceLeft = new Vector3(0, -90, 0);
        public Vector3 faceForward = new Vector3(0, 0, 0);
        public Vector3 faceBackward = new Vector3(0, 180, 0);
        public StayEvent mStayEvent = new StayEvent();

        private float mInitY = float.NaN;
        private List<Vector3> mMoveCaches = new List<Vector3>();
        private List<DownRiseGrid> mMoveGridCaches = new List<DownRiseGrid>();

        public List<Vector3> DownRiseList
        {
            get
            {
                return null;
            }
        }

        public Vector3 WorldPos
        {
            get
            {
                return transform.position;
            }
        }

        void Awake()
        {
            mInitY = transform.position.y;
        }

        void Update()
        {
            ListenMouseDown();
        }

        public void DownRise()
        {
        }

        private void MoveToNext()
        {
            Vector3 tempDisPos = mMoveCaches[0] - transform.position;
            if (tempDisPos.x != 0)
            {
                if (tempDisPos.x > 0)
                {
                    transform.localEulerAngles = faceRight;
                }
                else
                {
                    transform.localEulerAngles = faceLeft;
                }
            }
            else if (tempDisPos.z != 0)
            {
                if (tempDisPos.z > 0)
                {
                    transform.localEulerAngles = faceForward;
                }
                else
                {
                    transform.localEulerAngles = faceBackward;
                }
            }

            transform.position = mMoveCaches[0];
            if (mMoveCaches.Count % 3 == 0)
            {
                mMoveGridCaches[0].DownRise();
                mCurrentGrid = mMoveGridCaches[0];
                mMoveGridCaches.RemoveAt(0);
            }
            mMoveCaches.RemoveAt(0);

            if (mMoveCaches.Count == 0)
            {
                // 设置角色为待机状态
                SwitchIdelState();
                // 触发事件，停留在格子上
                if (mStayEvent != null)
                {
                    mStayEvent.Invoke(mCurrentGrid.gameObject);
                }
                CancelInvoke("MoveToNext");
            }
        }

        private void SetDownRiseList(Vector3 varPos)
        {
            varPos.y = mInitY;
            mMoveCaches.Add(varPos - 0.1f * Vector3.up);
            mMoveCaches.Add(varPos);
        }

        private void ListenMouseDown()
        {
            //判断鼠标左键是否按下
            if (Input.GetMouseButtonDown(0))
            {
                //获取由主摄像机位置到鼠标点击位置的一条射线
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                bool isCollider = Physics.Raycast(ray, out hitInfo);
                //判断射线是否成功发射且是否触发目标物体
                if (isCollider && hitInfo.collider.CompareTag("Ground")
                    && mMoveCaches.Count == 0)
                {
                    CatchHitObj(hitInfo.transform);
                }
            }
        }

        private void CatchHitObj(Transform varTrans)
        {
            var tempScript = varTrans.GetComponent<DownRiseGrid>();
            if (tempScript == null)
            {
                return;
            }
            List<DownRiseGrid> tempList = mPathConfig.GetMovePath(mCurrentGrid, tempScript);
            if (tempList == null || tempList.Count == 0)
            {
                return;
            }
            foreach (var drg in tempList)
            {
                mMoveGridCaches.Add(drg);
                Vector3 tempPos = drg.WorldPos;
                tempPos.y = mInitY;
                mMoveCaches.Add(tempPos);
                SetDownRiseList(tempPos);
            }
            // 设置角色为跳状态
            SwitchJumpState();
            InvokeRepeating("MoveToNext", 0, 0.1f);
        }

        private void SwitchJumpState()
        {

        }

        private void SwitchIdelState()
        {

        }
    }
}