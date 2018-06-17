using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EngineFramework.Mesh
{
    public class MeshInformationPrint : MeshDrawBase
    {
        protected override void DrawMesh()
        {
            
        }

        // 编辑器下自动调用
        private void OnDrawGizmos()
        {
            targetFilter = GetComponent<MeshFilter>();
            mh = targetFilter.mesh;
            Gizmos.color = Color.red;
            for (var i = 0; i < mh.vertices.Length; i++)
            {
                Vector3 tempWorldPos = transform.TransformPoint(mh.vertices[i]);
                Gizmos.DrawSphere(tempWorldPos, 0.2f);
            }
        }
    }
}
