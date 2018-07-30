using Framework.Security;
using UnityEngine;
using System.IO;
using Framework.Unity.Tools;

public class Test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
#if develop
        Debug.Log(NetUtils.SelfIP);
#endif
        string tempText = "1234567890";
        {
            MD5 temp = new MD5();
            Debug.Log("MD5加密后" + temp.MD5Encrypt(tempText));
        }
        {
            RSA temp = new RSA();
            string tempMiText = temp.RSAEncrypt(tempText);
            
            //Debug.Log("RSA加密后" + tempMiText);
            //Debug.Log("RSA解密后" + temp.RSADecrypt(tempMiText));

            string tempPath = Application.persistentDataPath + "/rsa.txt";
            File.WriteAllText(tempPath, tempMiText);
            string tempFileText = File.ReadAllText(tempPath);
            if (tempMiText == tempFileText)
            {
                Debug.Log("RSA加密成功");
            }
            string tempTarget = temp.RSADecrypt(tempFileText);
            Debug.Log("RSA解密后" + tempTarget);
            if (tempText == tempTarget)
            {
                Debug.Log("RSA解密成功");
            }
        }
        {
            DES temp = new DES();
            string tempMiText = temp.DESEncrypt(tempText);

            //Debug.Log("DES加密后" + tempMiText);
            //Debug.Log("DES解密后" + temp.DesDecrypt(tempMiText));

            string tempPath = Application.persistentDataPath + "/des.txt";
            File.WriteAllText(tempPath, tempMiText);
            string tempFileText = File.ReadAllText(tempPath);
            if (tempMiText == tempFileText)
            {
                Debug.Log("DES加密成功");
            }
            string tempTarget = temp.DESDecrypt(tempFileText);
            Debug.Log("DES解密后" + tempTarget);
            if (tempText == tempTarget)
            {
                Debug.Log("DES解密成功");
            }
        }

    }
}
