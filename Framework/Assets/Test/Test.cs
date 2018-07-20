using UnityEngine;
using Framework.Unity.Sqlite;
using System.Text;
using System;
using Framework.Security;

public class Test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        string tempText = "1234567890";
        {
            MD5 temp = new MD5();
            Debug.Log("MD5加密后" + temp.MD5Encrypt(tempText));
        }
        {
            RSA temp = new RSA();
            string tempMiText = temp.RSAEncrypt(tempText);
            Debug.Log("RSA加密后" + tempMiText);
            Debug.Log("RSA解密后" + temp.RSADecrypt(tempMiText));
        }
        {
            DES temp = new DES();
            string tempMiText = temp.DesEncrypt(tempText);

            Debug.Log("DES加密后" + tempMiText);
            Debug.Log("DES解密后" + temp.DesDecrypt(tempMiText));
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
