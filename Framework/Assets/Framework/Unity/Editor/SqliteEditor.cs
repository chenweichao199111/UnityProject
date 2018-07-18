using Framework.Unity.Sqlite;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Framework.Unity.Editor
{
    public class SqliteEditor
    {
        private static string CreateFolder = string.Format("{0}/Scripts/Model", Application.dataPath);
        private static string DbPath = string.Format("data source={0}/StreamingAssets/data.db", Application.dataPath);
        private static string NameSpace = "Model";

        [MenuItem("Tools/DB/Create Struct By DataBase")]
        public static void CreateStructBySql()
        {
           SqliteSyntax.CreateStructBySql(CreateFolder, NameSpace, DbPath);
        }

        [MenuItem("Tools/DB/Restore DataBase By Sql")]
        public static void CreateDataBaseBySql()
        {
            string tempSqlPath = EditorUtility.OpenFilePanel("Load sql of Directory", Application.dataPath, "sql");
            try
            {
                string tempSql = File.ReadAllText(tempSqlPath);
                SQLiteHelper tempHelper = new SQLiteHelper(DbPath);
                Debug.Log(tempSql);
                tempHelper.ExecuteQuery(tempSql);
                tempHelper.CloseConnection();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}
