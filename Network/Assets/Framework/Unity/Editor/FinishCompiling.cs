using System;
using UnityEditor;
using UnityEngine;

namespace Framework.Unity.Editor
{
    [InitializeOnLoad]
	public class FinishCompiling
	{
	    const string compilingKey = "Compiling";
	    static bool compiling;

        static FinishCompiling()
	    {
	        compiling = EditorPrefs.GetBool(compilingKey, false);
	        EditorApplication.update += Update;
	    }

	    static void Update()
	    {
	        if (compiling && !EditorApplication.isCompiling)
	        {
                Debug.Log(string.Format("编译结束于 {0}", DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss")));
                compiling = false;
	            EditorPrefs.SetBool(compilingKey, false);
	        }
	        else if (!compiling && EditorApplication.isCompiling)
	        {
                Debug.Log(string.Format("编译开始于 {0}", DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss")));
                compiling = true;
	            EditorPrefs.SetBool(compilingKey, true);
	        }
	    }
	}
}