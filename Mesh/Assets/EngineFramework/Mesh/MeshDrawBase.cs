using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EngineFramework.Mesh
{
    public abstract class MeshDrawBase : MonoBehaviour
    {

        protected MeshFilter targetFilter;  // 持有网格
        protected UnityEngine.Mesh mh; // 网格
        protected int[] tris;
        protected Vector2[] uvs;
        protected Vector3[] normals;

        void Awake()
        {
            targetFilter = GetComponent<MeshFilter>();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            DrawMesh();
        }

        protected abstract void DrawMesh();
    }
}
