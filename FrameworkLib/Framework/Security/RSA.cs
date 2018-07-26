using System;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Security
{
    public class RSA
    {
        private readonly string CspKeyContainerName = "oa_erp_dowork";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="normaltxt"></param>
        /// <returns></returns>
        public string RSAEncrypt(string normaltxt)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = CspKeyContainerName;//密匙容器的名称，保持加密解密一致才能解密成功
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] plaindata = Encoding.Default.GetBytes(normaltxt);//将要加密的字符串转换为字节数组
                byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
            }
        }

        public string RSADecrypt(string securityTxt)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = CspKeyContainerName;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] encryptdata = Convert.FromBase64String(securityTxt);
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                return Encoding.Default.GetString(decryptdata);
            }
        }

    }
}
