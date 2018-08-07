using UnityEditor;
using UnityEngine;

namespace Framework.Unity.Editor
{
    public class MeshEditor
    {
        [MenuItem("GameObject/Get Mesh BoundSize", priority = 0)]
        static void GetMeshBoundSize()
        {
            var tempArray = Selection.gameObjects;
            if (tempArray != null && tempArray.Length > 0)
            {
                var tempMeshFilter = tempArray[0].GetComponent<MeshFilter>();
                if (tempMeshFilter != null)
                {
                    Debug.Log(tempMeshFilter.sharedMesh.bounds.size);
                }
                else
                {
                    Debug.LogError("选择的物体不存在MeshFilter组件");
                }
            }
        }
    }
}
