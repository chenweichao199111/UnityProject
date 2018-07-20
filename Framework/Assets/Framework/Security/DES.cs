using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Security
{
    public class DES
    {
        private string _iv = "12345678";
        private string _key = "123==ABC";
        /// <summary>
        /// DES加密偏移量，必须是>=8位长的字符串
        /// </summary>
        public string Iv
        {
            get { return _iv; }
            set { _iv = value; }
        }

        /// <summary>
        /// DES加密的私钥，必须是8位长的字符串
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        /// <summary>
        /// 对字符串进行DES加密
        /// </summary>
        /// <param name="sourceString">待加密的字符串</param>
        /// <returns>加密后的BASE64编码的字符串</returns>
        public string DesEncrypt(string sourceString)
        {
            byte[] btKey = Encoding.Default.GetBytes(_key);
            byte[] btIv = Encoding.Default.GetBytes(_iv);
            var des = new DESCryptoServiceProvider();
            using (var ms = new MemoryStream())
            {
                byte[] inData = Encoding.Default.GetBytes(sourceString);
                try
                {
                    using (var cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIv), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 对DES加密后的字符串进行解密
        /// </summary>
        /// <param name="encryptedString">待解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public string DesDecrypt(string encryptedString)
        {
            byte[] btKey = Encoding.Default.GetBytes(_key);
            byte[] btIv = Encoding.Default.GetBytes(_iv);
            var des = new DESCryptoServiceProvider();

            using (var ms = new MemoryStream())
            {
                try
                {
                    byte[] inData = Convert.FromBase64String(encryptedString);
                    using (var cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIv), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Encoding.Default.GetString(ms.ToArray());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

}
