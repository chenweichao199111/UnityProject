using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EngineFramework.Mesh
{
    public class ConvexPolygon : MeshDrawBase
    {
        [Header("顶点数组")]
        public List<Vector3> vts = new List<Vector3>();

        protected override void DrawMesh()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                DrawPolygon();
                AddPhysic();
            }
        }

        // 画凸多边形
        private void DrawPolygon()
        {
            mh = new UnityEngine.Mesh();

            // 获取顶点
            mh.vertices = vts.ToArray();
            // 三角形，按照顺时针画或者按照逆时针画
            // 按照顺时针原则画图，逆时针画出的图形可能看不到
            int tempTrisLength = (vts.Count - 2) * 3;
            tris = new int[tempTrisLength];
            for (int i = 0, index = 1; i < tempTrisLength; i += 3, index++)
            {
                tris[i] = 0;
                tris[i + 1] = index;
                tris[i + 2] = index + 1;
            }

            mh.triangles = tris;

            // 法线
            normals = new Vector3[vts.Count];
            for (var i = 0; i < vts.Count; i++)
            {
                normals[i] = new Vector3(0, 0, 1);
            }
            mh.normals = normals;

            // 重新计算包围盒
            mh.RecalculateBounds();
            // 重新计算法线
            mh.RecalculateNormals();
            // 重新计算切线
            mh.RecalculateTangents();

            targetFilter.mesh = mh;
        }

        private void AddPhysic()
        {
            var tempRig = gameObject.AddComponent<Rigidbody>();
            Destroy(tempRig, 1.5f);
            Invoke("Reset", 1.5f);
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit tempHit;
                if (Physics.Raycast(ray, out tempHit, Mathf.Infinity))
                {
                    vts.Add(tempHit.point);
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reset();
            }
        }

        private void Reset()
        {
            vts.Clear();
            targetFilter.mesh = null;
            Destroy(mh);
        }

        private void OnGUI()
        {
            if (vts.Count == 0) return;
            GUI.color = Color.red;

            for (var i = 0; i < vts.Count; i++)
            {
                Vector3 tempScreenPoint = Camera.main.WorldToScreenPoint(vts[i]);
                GUI.Label(new Rect(tempScreenPoint.x, Camera.main.pixelHeight - tempScreenPoint.y, 100, 80), i.ToString());
            }

        }

        private void OnDrawGizmos()
        {
            if (vts.Count == 0) return;
            Gizmos.color = Color.cyan;

            for (var i = 0; i < vts.Count; i++)
            {
                Gizmos.DrawWireSphere(vts[i], 0.2f);
            }
        }
    }

}
