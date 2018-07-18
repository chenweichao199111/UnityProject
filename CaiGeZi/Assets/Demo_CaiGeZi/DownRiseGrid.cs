using System.Collections.Generic;
using UnityEngine;

namespace ZsCLJX
{
    public class DownRiseGrid : MonoBehaviour, IDownRise
    {
        public int mIndex;
        public List<Vector3> downRiseList = new List<Vector3>();
        private List<Vector3> mMoveCaches = new List<Vector3>();

        public List<Vector3> DownRiseList
        {
            get
            {
                return downRiseList;
            }
        }

        public Vector3 WorldPos
        {
            get
            {
                return transform.position;
            }
        }

        public void DownRise()
        {
            if (mMoveCaches.Count == 0)
            {
                mMoveCaches.AddRange(downRiseList);
            }
            InvokeRepeating("MoveToNext", 0, 0.1f);
        }

        private void MoveToNext()
        {
            transform.position = mMoveCaches[0];
            mMoveCaches.RemoveAt(0);

            if (mMoveCaches == null || mMoveCaches.Count == 0)
            {
                CancelInvoke("MoveToNext");
            }
        }

        [ContextMenu("SetDownRise")]
        private void SetDownRiseList()
        {
            Vector3 currentPos = transform.position;
            downRiseList.Add(currentPos - 0.1f * Vector3.up);
            downRiseList.Add(currentPos);
        }
    }
}