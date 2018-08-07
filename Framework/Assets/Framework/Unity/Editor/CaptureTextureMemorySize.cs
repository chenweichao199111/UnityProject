using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

namespace Framework.Unity.Editor
{
    class CaptureTextureMemorySize
    {
        [MenuItem("Tools/Get Texture MemorySize")]
        public static void GetTextureMemorySize()
        {
            Texture target = Selection.activeObject as Texture;
            var type = Assembly.Load("UnityEditor.dll").GetType("UnityEditor.TextureUtil");

            MethodInfo methodInfo = type.GetMethod("GetStorageMemorySize", BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);

            Debug.Log("内存占用：" + EditorUtility.FormatBytes(Profiler.GetRuntimeMemorySizeLong(Selection.activeObject)));
            Debug.Log("硬盘占用：" + EditorUtility.FormatBytes((int)methodInfo.Invoke(null, new object[] { target })));
        }
    }
}
