using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace ZsCLJX
{
    public class ActivePlayer : MonoBehaviour
    {
        [Serializable]
        public sealed class StayEvent : UnityEvent<GameObject> { }

        public DownRiseGrid mCurrentGrid;
        public DownRiseGridRelation mPathConfig;
        [Header("准备起跳时间")]
        public float mJumpPrepateTime = 0.208f;
        [Header("人物在空中跳的时间包含落地")]
        public float mJumpTime = 0.625f;
        [Header("一只脚到两只脚落地时间")]
        public float mDownGridTime = 0.208f;
        [Header("人物双脚落地到静止的时间")]
        public float mUpGridTime = 0.417f;
        [Header("人物跳跃动画总时间")]
        public float mJumpTotalTime = 1.5f;

        public Vector3 faceRight = new Vector3(0, 90, 0);
        public Vector3 faceLeft = new Vector3(0, -90, 0);
        public Vector3 faceForward = new Vector3(0, 0, 0);
        public Vector3 faceBackward = new Vector3(0, 180, 0);
        public StayEvent mStayEvent = new StayEvent();
        public const float LeftJumpCondition = 1.0f;
        public const float RightJumpCondition = 2.0f;

        private bool mIsMove;
        private float mInitY;
        private bool moveLeftFoot = true;  // 先迈左脚

        void Awake()
        {
            mInitY = transform.position.y;
        }

        void Update()
        {
            ListenMouseDown();
        }


        private void SetDirection(Vector3 targetPos)
        {
            Vector3 tempDisPos = targetPos - transform.position;
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
                    && !mIsMove)
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

            float tempRemainTime = mJumpTotalTime - mJumpPrepateTime - mJumpTime - mUpGridTime;
            Sequence tempSeq = DOTween.Sequence();

            for (int i = 0; i < tempList.Count; i++)
            {
                var drg = tempList[i];
                float tempDownOneTime = i * mJumpTotalTime + mJumpPrepateTime + mJumpTime - mDownGridTime;

                Vector3 tempGridPos = drg.transform.position;
                Vector3 tempPos = tempGridPos;
                tempPos.y = mInitY;
                tempSeq.AppendCallback(
                    () => {
                        SetDirection(tempPos);
                        SwitchJumpState();
                    }
                ).
                AppendInterval(mJumpPrepateTime).Append(transform.DOJump(tempPos, 0.3f, 1, mJumpTime).OnComplete(() => { mCurrentGrid = drg; }))
                .Insert(tempDownOneTime, transform.DOMoveY(mInitY - 0.1f, mDownGridTime))
                .Insert(i * mJumpTotalTime + mJumpPrepateTime + mJumpTime, transform.DOMoveY(mInitY, mUpGridTime));
                if (tempRemainTime > 0)
                {
                    tempSeq.AppendInterval(tempRemainTime);
                }

                DOTween.Sequence().AppendInterval(tempDownOneTime)
                    .Append(drg.transform.DOMoveY(tempGridPos.y - 0.1f, mDownGridTime))
                    .Append(drg.transform.DOMoveY(tempGridPos.y, mUpGridTime)).Play().timeScale = 3f;
            }
            tempSeq.AppendCallback(StopJump).Play().timeScale = 3f;

            mIsMove = true;
        }

        private void SwitchJumpState()
        {
            moveLeftFoot = !moveLeftFoot;
            Animator tempA = GetComponent<Animator>();
            if (tempA != null)
            {
                tempA.SetBool("Idel", false);
                if (moveLeftFoot)
                {
                    tempA.SetFloat("Jump", LeftJumpCondition);
                }
                else
                {
                    tempA.SetFloat("Jump", RightJumpCondition);
                }
            }
        }

        private void StopJump()
        {
            SwitchIdelState();
            mIsMove = false;
            if (mStayEvent != null)
            {
                mStayEvent.Invoke(mCurrentGrid.gameObject);
            }
        }

        private void SwitchIdelState()
        {
            Animator tempA = GetComponent<Animator>();
            if (tempA != null)
            {
                tempA.SetBool("Idel", true);
                tempA.SetFloat("Jump", 0f);
            }
        }

        #region 公共方法
        public void TriggerRaycastHit(RaycastHit varHit)
        {
            CatchHitObj(varHit.transform);
        }
        #endregion
    }
}