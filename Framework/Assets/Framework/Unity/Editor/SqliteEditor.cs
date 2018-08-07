using Framework.Unity.Sqlite;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Framework.Unity.Editor
{
    public class SqliteEditor
    {
        private static string DbPath = string.Format("data source={0}/StreamingAssets/data.db", Application.dataPath);

        [MenuItem("Tools/DB/Restore DataBase By Sql")]
        public static void CreateDataBaseBySql()
        {
            string tempSqlPath = EditorUtility.OpenFilePanel("Load sql of Directory", Application.dataPath, "sql");
            try
            {
                string tempSql = File.ReadAllText(tempSqlPath);
                SqliteHelper tempHelper = new SqliteHelper(DbPath);
                Debug.Log(tempSql);
                tempHelper.ExecuteQuery(tempSql);
                tempHelper.CloseConnection();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        [MenuItem("Tools/DB/Open excel2json.exe")]
        public static void OpenExcel2JsonExe()
        {
            string tempPath = typeof(SqliteEditor).Namespace.Replace('.', '/');
            tempPath = string.Format("{0}/{1}/.excel2json/excel2json.exe", Application.dataPath, tempPath);
            tempPath = tempPath.Replace('/', '\\');
            System.Diagnostics.Process.Start(tempPath);
        }
    }
}