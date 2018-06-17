using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EngineFramework.Mesh
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class QuadDraw : MeshDrawBase
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
            tris = new int[6];
            // 第一个三角形
            tris[0] = 0;
            tris[1] = 1;
            tris[2] = 3;
            // 第二个三角形
            tris[3] = 3;
            tris[4] = 1;
            tris[5] = 2;

            // 将UV对应顶点, U代表x轴，V代表Y轴, 中心点（0, 0）, x轴水平向右，y轴垂直向上
            uvs = new Vector2[vts.Count];
            uvs[0] = new Vector2(0, 0);
            uvs[1] = new Vector2(0, 1);
            uvs[2] = new Vector2(1, 1);
            uvs[3] = new Vector2(1, 0);

            mh.uv = uvs;

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
