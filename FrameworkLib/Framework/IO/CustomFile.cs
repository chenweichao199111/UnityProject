using System;
using System.IO;
using System.Security.Cryptography;

namespace Framework.IO
{
    public static class CustomFile
    {
        public static bool IsValidFileContent(string filePath1, string filePath2)
        {
            //创建一个哈希算法对象 
            using (HashAlgorithm hash = HashAlgorithm.Create())
            {
                using (FileStream file1 = new FileStream(filePath1, FileMode.Open), file2 = new FileStream(filePath2, FileMode.Open))
                {
                    byte[] hashByte1 = hash.ComputeHash(file1);//哈希算法根据文本得到哈希码的字节数组 
                    byte[] hashByte2 = hash.ComputeHash(file2);
                    string str1 = BitConverter.ToString(hashByte1);//将字节数组转换为字符串 
                    string str2 = BitConverter.ToString(hashByte2);
                    return (str1 == str2);//比较哈希码 
                }
            }
        }

        /// <summary>
        /// 计算文件的hash值 用于比较两个文件是否相同
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件hash值</returns>
        public static string GetFileHash(string filePath)
        {
            //创建一个哈希算法对象 
            using (HashAlgorithm hash = HashAlgorithm.Create())
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open))
                {
                    //哈希算法根据文本得到哈希码的字节数组 
                    byte[] hashByte = hash.ComputeHash(file);
                    //将字节数组装换为字符串  
                    return BitConverter.ToString(hashByte);
                }
            }
        }

        /// <summary>
        /// 计算文件的hash值 用于比较两个文件是否相同
        /// </summary>
        /// <param name="varArray">文件字节数组</param>
        /// <returns>文件hash值</returns>
        public static string GetBytesHash(byte[] varArray)
        {
            //创建一个哈希算法对象 
            using (HashAlgorithm hash = HashAlgorithm.Create())
            {
                //哈希算法根据文本得到哈希码的字节数组 
                byte[] hashByte = hash.ComputeHash(varArray);
                //将字节数组装换为字符串  
                return BitConverter.ToString(hashByte);
            }
        }
    }
}
