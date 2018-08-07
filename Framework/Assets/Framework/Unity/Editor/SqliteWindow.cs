using Framework.Unity.Sqlite;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Framework.Unity.Editor
{
    public class SqliteWindow : EditorWindow
    {
        public string myCreateFolder;
        public string myNameSpace = "Model";
        public string myDbPath;
        public int mLength = 0;
        public List<string> mNotCreateTable;

        [MenuItem("Tools/DB/Open Sqlite Window")]
        static void OpenSqliteWindow()
        {
            var window = EditorWindow.GetWindow<SqliteWindow>();
            window.myCreateFolder = string.Format("{0}/Scripts/Model", Application.dataPath);
            window.myDbPath = string.Format("data source={0}/StreamingAssets/data.db", Application.dataPath);
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("创建的位置");
            myCreateFolder = GUILayout.TextField(myCreateFolder);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("脚本的命名空间");
            myNameSpace = GUILayout.TextField(myNameSpace);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("db的位置");
            myDbPath = GUILayout.TextField(myDbPath);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("不用创建结构体的表数量");
            string tempStr = GUILayout.TextField(mLength.ToString());
            int.TryParse(tempStr, out mLength);
            GUILayout.EndHorizontal();

            if (mLength > 0)
            {
                if (mNotCreateTable == null || mNotCreateTable.Count != mLength)
                {
                    mNotCreateTable = new List<string>();
                    for (int i = 0; i < mLength; i++)
                    {
                        mNotCreateTable.Add("");
                    }
                }
                for (int i = 0; i < mLength; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(string.Format("第{0}个", i + 1));
                    mNotCreateTable[i] = GUILayout.TextField(mNotCreateTable[i]);
                    GUILayout.EndHorizontal();
                }
            }

            if (GUILayout.Button("根据表结构创建结构体") == true)
            {
                SqliteSyntax.CreateStructBySql(myCreateFolder, myNameSpace, myDbPath, mNotCreateTable);
                AssetDatabase.Refresh();
            }
        }
    }
}