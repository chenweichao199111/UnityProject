using UnityEngine;

namespace Framework.Unity.Tools
{
    public class StringUtils
    {
        public static Vector3 StringToVector(string varStr)
        {
            try
            {
                string[] tempArray = varStr.Split(',');
                return new Vector3(float.Parse(tempArray[0]), float.Parse(tempArray[1]), float.Parse(tempArray[2]));
            }
            catch
            {
                Debug.LogErrorFormat("字符串{0}格式不正确，不能转成Vector", varStr);
                return Vector3.zero;
            }
        }
    }
}
