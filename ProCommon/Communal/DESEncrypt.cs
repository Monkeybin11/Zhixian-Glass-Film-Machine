using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       DESEncrypt
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProCommon.Communal
 * File      Name：       DESEncrypt
 * Creating  Time：       4/21/2020 1:22:27 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProCommon.Communal
{
    /// <summary>
    /// DES加/解密类
    /// </summary>
    public class DESEncrypt
    {
        #region +++++++++加密+++++++++
        public static string Encrypt(string txt)
        {
            return Encrypt(txt, "xYz_Albert");
        }

        public static string Encrypt(string txt, string sKey)
        {
            System.Security.Cryptography.DESCryptoServiceProvider dESCryptoServiceProvider = new System.Security.Cryptography.DESCryptoServiceProvider();
            byte[] inputByteArr = Encoding.Default.GetBytes(txt);
#pragma warning disable CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
            dESCryptoServiceProvider.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
#pragma warning restore CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
#pragma warning disable CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
            dESCryptoServiceProvider.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
#pragma warning restore CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            //加密器
            System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
            cryptoStream.Write(inputByteArr, 0, inputByteArr.Length);
            cryptoStream.FlushFinalBlock();
            StringBuilder strBuilder = new StringBuilder();
            foreach (byte b in memoryStream.ToArray())
            {
                strBuilder.AppendFormat("{0:X2}", b);
            }
            return strBuilder.ToString();
        }
        #endregion

        #region ---------解密---------
        public static string Decrypt(string txt)
        {
            return Decrypt(txt, "xYz_Albert");
        }

        public static string Decrypt(string txt, string sKey)
        {
            System.Security.Cryptography.DESCryptoServiceProvider dESCryptoServiceProvider = new System.Security.Cryptography.DESCryptoServiceProvider();
            int len = txt.Length / 2;
            byte[] inputByteArr = new byte[len];
            int i, j;
            for (i = 0; i < len; i++)
            {
                j = Convert.ToInt32(txt.Substring(i * 2, 2), 16);
                inputByteArr[i] = (byte)j;
            }

            dESCryptoServiceProvider.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            dESCryptoServiceProvider.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            //解密器
            System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
            cryptoStream.Write(inputByteArr, 0, inputByteArr.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.Default.GetString(memoryStream.ToArray());
        }
        #endregion
    }
}
