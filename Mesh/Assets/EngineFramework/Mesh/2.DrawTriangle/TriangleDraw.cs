using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EngineFramework.Mesh
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    // 画三角形步骤 1.顶点 2.指定三角形顺序 3.UV，法线，切线
    public class TriangleDraw : MeshDrawBase
    {
        [Header("顶点数组")]
        public List<Vector3> vts = new List<Vector3>();

        private void Start()
        {
            mh = new UnityEngine.Mesh();

            // 获取顶点
            mh.vertices = vts.ToArray();
            // 三角形，按照顺时针画或者按照逆时针画
            // 按照顺时针原则画图，逆时针画出的图形可能看不到
            tris = new int[3];
            tris[0] = 0;
            tris[1] = 1;
            tris[2] = 2;

            mh.triangles = tris;
            // 重新计算包围盒
            mh.RecalculateBounds();
            // 重新计算法线
            mh.RecalculateNormals();
            // 重新计算切线
            mh.RecalculateTangents();

            targetFilter.mesh = mh;
        }

        protected override void DrawMesh()
        {

        }
    }

}
