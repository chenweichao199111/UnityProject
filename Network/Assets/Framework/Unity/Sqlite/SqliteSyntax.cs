using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Framework.Unity.Sqlite
{
    public class SqliteSyntax : MonoBehaviour
    {
        [Header("创建的位置")]
        public string myCreateFolder = "Scripts/Model";
        [Header("脚本的命名空间")]
        public string myNameSpace = "Model";
        [Header("db的位置")]
        public string myDbPath = "StreamingAssets/data.db";
        [Header("不用创建结构体的表")]
        public List<string> mNotCreateTable;

        [ContextMenu("Create Struct By DataBase")]
        public void CreateStruct()
        {
            string tempCreateFolder = string.Format("{0}/{1}", Application.dataPath, myCreateFolder);
            string tempPath = string.Format("data source={0}/{1}", Application.dataPath, myDbPath);
            CreateStructBySql(tempCreateFolder, myNameSpace, tempPath, mNotCreateTable);
        }

        public static void CreateStructBySql(string varCreateFolder, string varNameSpace, string varDbPath, List<string> varNotCreateTable = null)
        {
            if (Directory.Exists(varCreateFolder))
            {
                Directory.Delete(varCreateFolder, true);
            }
            Directory.CreateDirectory(varCreateFolder);

            List<string> tempTableList = new List<string>();
            string tempTabString = "    ";

            SQLiteHelper tempHelper = new SQLiteHelper(varDbPath);

            SqliteDataReader tempReader = tempHelper.ExecuteQuery("SELECT name FROM sqlite_master WHERE TYPE='table' ");
            while (tempReader.Read())
            {
                string tempTableName = tempReader.GetString(0);
                if (varNotCreateTable != null && varNotCreateTable.Contains(tempTableName))
                {
                    continue;
                }
                tempTableList.Add(tempTableName);
            }
            tempHelper.CloseConnection();

            // 遍历所有的表，查询字段
            for (int i = 0; i < tempTableList.Count; i++)
            {
                string tempTableName = tempTableList[i];
                string tempSql = string.Format("PRAGMA table_info ({0})", tempTableName);
                tempReader = tempHelper.ExecuteQuery(tempSql);
                Dictionary<string, string> tempFieldDic = new Dictionary<string, string>();
                while (tempReader.Read())
                {
                    string tempKey = tempReader[tempReader.GetOrdinal("name")].ToString();
                    string tempValue = tempReader[tempReader.GetOrdinal("type")].ToString();
                    tempFieldDic[tempKey] = GetFiledType(tempValue);
                }

                StringBuilder tempBuilder = new StringBuilder();
                // 构造引用命名空间
                tempBuilder.Append("using Mono.Data.Sqlite;");
                tempBuilder.AppendLine();
                tempBuilder.Append("using System.Collections.Generic;");
                tempBuilder.AppendLine();
                tempBuilder.AppendLine();

                // 构造命名空间
                tempBuilder.AppendFormat("namespace {0}", varNameSpace);
                tempBuilder.AppendLine();
                tempBuilder.Append("{");
                tempBuilder.AppendLine();

                // 构造结构体名字
                tempBuilder.AppendFormat("{0}public struct {1}", tempTabString, tempTableName);
                tempBuilder.AppendLine();
                tempBuilder.AppendFormat("{0}", tempTabString);
                tempBuilder.Append("{");

                Debug.Log("Sqlite数据类型->C#数据类型");
                // 构造成员变量
                foreach (var kvp in tempFieldDic)
                {
                    tempBuilder.AppendLine();
                    tempBuilder.AppendFormat("{0}{0}private {1} {2};", tempTabString, kvp.Value, kvp.Key);
                }

                tempBuilder.AppendLine();

                // 构造对应的属性
                foreach (var kvp in tempFieldDic)
                {
                    // 再换一行
                    tempBuilder.AppendLine();

                    string tempKey = kvp.Key;
                    string tempPropertyName = tempKey.Substring(0, 1).ToUpper();
                    if (tempKey.Length > 1)
                    {
                        tempPropertyName = string.Format("{0}{1}", tempPropertyName, tempKey.Substring(1, tempKey.Length - 1));
                    }
                    if (tempPropertyName == tempKey)
                    {
                        Debug.LogErrorFormat("字段{0}的首字母应该小写", tempKey);
                        tempPropertyName = string.Format("_{0}", tempPropertyName);
                    }

                    tempBuilder.AppendFormat("{0}{0}public {1} {2}", tempTabString, kvp.Value, tempPropertyName);
                    tempBuilder.AppendLine();
                    tempBuilder.AppendFormat("{0}{0}", tempTabString);
                    tempBuilder.Append("{");
                    tempBuilder.AppendLine();

                    // 设置get
                    tempBuilder.AppendFormat("{0}{0}{0}get", tempTabString);
                    tempBuilder.AppendLine();
                    tempBuilder.AppendFormat("{0}{0}{0}", tempTabString);
                    tempBuilder.Append("{");
                    tempBuilder.AppendLine();
                    tempBuilder.AppendFormat("{0}{0}{0}{0} return {1};", tempTabString, tempKey);
                    tempBuilder.AppendLine();
                    tempBuilder.AppendFormat("{0}{0}{0}", tempTabString);
                    tempBuilder.Append("}");
                    tempBuilder.AppendLine();
                    tempBuilder.AppendLine();

                    // 设置set
                    tempBuilder.AppendFormat("{0}{0}{0}set", tempTabString);
                    tempBuilder.AppendLine();
                    tempBuilder.AppendFormat("{0}{0}{0}", tempTabString);
                    tempBuilder.Append("{");
                    tempBuilder.AppendLine();
                    tempBuilder.AppendFormat("{0}{0}{0}{0} {1} = value;", tempTabString, tempKey);
                    tempBuilder.AppendLine();
                    tempBuilder.AppendFormat("{0}{0}{0}", tempTabString);
                    tempBuilder.Append("}");
                    tempBuilder.AppendLine();

                    tempBuilder.AppendFormat("{0}{0}", tempTabString);
                    tempBuilder.Append("}");
                    tempBuilder.AppendLine();
                }

                // 构造反序列方法
                tempBuilder.AppendLine();
                tempBuilder.AppendFormat("{0}{0}public static List<{1}> Deserialize(SqliteDataReader varReader)", tempTabString, tempTableName);
                tempBuilder.AppendLine();
                tempBuilder.AppendFormat("{0}{0}", tempTabString);
                tempBuilder.Append("{");
                tempBuilder.AppendLine();

                // 构造反序列方法方法体
                tempBuilder.AppendFormat("{0}{0}{0}", tempTabString);
                tempBuilder.AppendFormat("List<{0}> tempList = null;", tempTableName);
                tempBuilder.AppendLine();
                tempBuilder.AppendFormat("{0}{0}{0}", tempTabString);
                tempBuilder.Append("while(varReader.Read())");
                tempBuilder.AppendLine();
                tempBuilder.AppendFormat("{0}{0}{0}", tempTabString);
                tempBuilder.Append("{");
                tempBuilder.AppendLine();
                tempBuilder.AppendFormat("{0}{0}{0}{0}{1} obj = new {1}();", tempTabString, tempTableName);
                tempBuilder.AppendLine();

                foreach (var kvp in tempFieldDic)
                {
                    tempBuilder.AppendFormat("{0}{0}{0}{0}obj.{1} = ", tempTabString, kvp.Key);
                    string tempMethod = GetMethodByField(kvp.Value);

                    tempBuilder.AppendFormat("varReader.{0}(varReader.GetOrdinal(\"", tempMethod);
                    tempBuilder.Append(kvp.Key);
                    tempBuilder.Append("\"))");

                    if (tempMethod.Equals("GetValue") && !kvp.Value.Equals("System.Object"))
                    {
                        tempBuilder.AppendFormat(" as {0}", kvp.Value);
                    }
                    tempBuilder.Append(";");

                    tempBuilder.AppendLine();
                }

                tempBuilder.AppendLine();
                tempBuilder.AppendFormat("{0}{0}{0}{0}", tempTabString);
                tempBuilder.AppendFormat("if (tempList == null) tempList = new List<{0}>();", tempTableName);
                tempBuilder.AppendLine();
                tempBuilder.AppendFormat("{0}{0}{0}{0}", tempTabString);
                tempBuilder.Append("tempList.Add(obj);");
                tempBuilder.AppendLine();
                tempBuilder.AppendFormat("{0}{0}{0}", tempTabString);
                tempBuilder.Append("}");
                tempBuilder.AppendFormat("{0}{0}{0}", tempTabString);
                tempBuilder.AppendLine();
                tempBuilder.AppendFormat("{0}{0}{0}return tempList;", tempTabString);
                tempBuilder.AppendLine();

                // 反序列方法构造结束
                tempBuilder.AppendFormat("{0}{0}", tempTabString);
                tempBuilder.Append("}");
                tempBuilder.AppendLine();

                // 构造ToString方法
                tempBuilder.AppendLine();
                tempBuilder.AppendFormat("{0}{0}public override string ToString()", tempTabString, tempTableName);
                tempBuilder.AppendLine();
                tempBuilder.AppendFormat("{0}{0}", tempTabString);
                tempBuilder.Append("{");
                tempBuilder.AppendLine();

                //  构造ToString方法体
                tempBuilder.AppendFormat("{0}{0}{0}", tempTabString);
                if (tempFieldDic.Count == 0)
                {
                    tempBuilder.Append("return \"\"");
                }
                else
                {
                    tempBuilder.Append("string str=string.Format(\"");
                    int index = 0;
                    foreach (var kvp in tempFieldDic)
                    {
                        tempBuilder.Append(kvp.Key);
                        tempBuilder.Append("={");
                        tempBuilder.Append(index);
                        tempBuilder.Append("}");
                        index++;
                        if (index < tempFieldDic.Count)
                        {
                            tempBuilder.Append(",");
                        }
                    }
                    tempBuilder.Append("\"");
                    foreach (var kvp in tempFieldDic)
                    {
                        tempBuilder.Append(",");
                        tempBuilder.Append(kvp.Key);
                    }
                    tempBuilder.Append(");");
                    tempBuilder.AppendLine();
                    tempBuilder.AppendFormat("{0}{0}{0}", tempTabString);
                    tempBuilder.Append("return str;");
                }
                tempBuilder.AppendLine();

                // ToString方法构造结束
                tempBuilder.AppendFormat("{0}{0}", tempTabString);
                tempBuilder.Append("}");
                tempBuilder.AppendLine();

                // 结构体构造结束
                tempBuilder.AppendFormat("{0}", tempTabString);
                tempBuilder.Append("}");
                tempBuilder.AppendLine();
                // 命名空间构造结束
                tempBuilder.Append("}");
                tempBuilder.AppendLine();

                Debug.Log(tempBuilder.ToString());

                string tempFilePath = string.Format("{0}/{1}.cs", varCreateFolder, tempTableName);
                File.WriteAllText(tempFilePath, tempBuilder.ToString());
            }
            tempHelper.CloseConnection();

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

        public static string GetMethodBySql(string varSqlType)
        {
            // Sqlite数据类型->C#数据类型
            // BIGINT和INTEGER->System.Int64
            // BLOB->System.Byte[]
            // BOOLEAN->System.Boolean
            // CHAR和STRING和TEXT和VARCHAR->System.String
            // DATE和DATETIME和TIME->System.DateTime
            // DECIMAL和NUMERIC->System.Decimal
            // DOUBLE->System.Double
            // INT->System.Int32
            // None->System.Object
            // REAL->System.Single
            string tempStr = null;
            switch (varSqlType)
            {
                case "BIGINT":
                case "INTEGER":
                    tempStr = "GetInt64";
                    break;
                case "BLOB":
                    tempStr = "GetValue";  // 这里要强制转成System.Byte[]
                    break;
                case "BOOLEAN":
                    tempStr = "GetBoolean";
                    break;
                case "CHAR":
                case "STRING":
                case "TEXT":
                case "VARCHAR":
                    tempStr = "GetString";
                    break;
                case "DATE":
                case "DATETIME":
                case "TIME":
                    tempStr = "GetDateTime";
                    break;
                case "DECIMAL":
                case "NUMERIC":
                    tempStr = "GetDecimal";
                    break;
                case "DOUBLE":
                    tempStr = "GetDouble";
                    break;
                case "INT":
                    tempStr = "GetInt32";
                    break;
                case "NONE":
                    tempStr = "GetValue";
                    break;
                case "REAL":
                    tempStr = "GetFloat";
                    break;
            }
            return tempStr;
        }

        public static string GetMethodByField(string varFieldType)
        {
            // Sqlite数据类型->C#数据类型
            // BIGINT和INTEGER->System.Int64
            // BLOB->System.Byte[]
            // BOOLEAN->System.Boolean
            // CHAR和STRING和TEXT和VARCHAR->System.String
            // DATE和DATETIME和TIME->System.DateTime
            // DECIMAL和NUMERIC->System.Decimal
            // DOUBLE->System.Double
            // INT->System.Int32
            // None->System.Object
            // REAL->System.Single
            string tempStr = null;
            switch (varFieldType)
            {
                case "System.Int64":
                    tempStr = "GetInt64";
                    break;
                case "System.Byte[]":
                    tempStr = "GetValue";  // 引用类型对应GetValue方法
                    break;
                case "System.Boolean":
                    tempStr = "GetBoolean";
                    break;
                case "System.String":
                    tempStr = "GetString";
                    break;
                case "System.DateTime":
                    tempStr = "GetDateTime";
                    break;
                case "System.Decimal":
                    tempStr = "GetDecimal";
                    break;
                case "System.Double":
                    tempStr = "GetDouble";
                    break;
                case "System.Int32":
                    tempStr = "GetInt32";
                    break;
                case "System.Object":
                    tempStr = "GetValue";
                    break;
                case "System.Single":
                    tempStr = "GetFloat";
                    break;
            }
            return tempStr;
        }

        public static string GetFiledType(string varSqlType)
        {
            // Sqlite数据类型->C#数据类型
            // BIGINT和INTEGER->System.Int64
            // BLOB->System.Byte[]
            // BOOLEAN->System.Boolean
            // CHAR和STRING和TEXT和VARCHAR->System.String
            // DATE和DATETIME和TIME->System.DateTime
            // DECIMAL和NUMERIC->System.Decimal
            // DOUBLE->System.Double
            // INT->System.Int32
            // NONE->System.Object
            // REAL->System.Single
            string tempStr = null;
            switch (varSqlType)
            {
                case "BIGINT":
                case "INTEGER":
                    tempStr = "System.Int64";
                    break;
                case "BLOB":
                    tempStr = "System.Byte[]";
                    break;
                case "BOOLEAN":
                    tempStr = "System.Boolean";
                    break;
                case "CHAR":
                case "STRING":
                case "TEXT":
                case "VARCHAR":
                    tempStr = "System.String";
                    break;
                case "DATE":
                case "DATETIME":
                case "TIME":
                    tempStr = "System.DateTime";
                    break;
                case "DECIMAL":
                case "NUMERIC":
                    tempStr = "System.Decimal";
                    break;
                case "DOUBLE":
                    tempStr = "System.Double";
                    break;
                case "INT":
                    tempStr = "System.Int32";
                    break;
                case "NONE":
                    tempStr = "System.Object";
                    break;
                case "REAL":
                    tempStr = "System.Single";
                    break;
            }
            return tempStr;
        }
    }
}