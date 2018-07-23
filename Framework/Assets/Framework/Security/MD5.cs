using System;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Security
{
    /// <summary>
    /// MD5加密不可逆，不需要加密向量
    /// </summary>
    public class MD5
    {
        public string MD5Encrypt(string normalTxt)
        {
            var bytes = Encoding.UTF8.GetBytes(normalTxt);//求Byte[]数组
            var Md5 = new MD5CryptoServiceProvider().ComputeHash(bytes);//求哈希值
            return Convert.ToBase64String(Md5);//将Byte[]数组转为净荷明文(其实就是字符串)
        }
    }
}
