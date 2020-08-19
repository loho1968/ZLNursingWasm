
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;


namespace NursingCommon
{
    /// <summary>
    /// 数据加密或解密
    /// </summary>
    public class Security
    {

        #region 对称加密
        private SymmetricAlgorithm _MobjCryptoService;
        private string _Key;
        /// <summary>     
        /// 对称加密类的构造函数     
        /// </summary>     
        public Security()
        {
            _MobjCryptoService = new RijndaelManaged();
            _Key = "Guz(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76*h%(HilJ$lyh!y6&(*jkP87jH7";
        }

        /// <summary>     
        /// 获得密钥     
        /// </summary>     
        /// <returns>密钥</returns>     
        private byte[] GetLegalKey()
        {
            string sTemp = _Key;
            _MobjCryptoService.GenerateKey();
            byte[] bytTemp = _MobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
                sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
                sTemp = sTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>     
        /// 获得初始向量IV     
        /// </summary>     
        /// <returns>初试向量IV</returns>     
        private byte[] GetLegalIV()
        {
            string sTemp = "E4ghj*Ghg7!rNIfb&95GUY86GfghUb#er57HBh(u%g6HJ($jhWk7&!hg4ui%$hjk";
            _MobjCryptoService.GenerateIV();
            byte[] bytTemp = _MobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
                sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
                sTemp = sTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>     
        /// 对称加密方法     
        /// </summary>     
        /// <param name="source">待加密的串</param>     
        /// <returns>经过加密的串</returns>     
        public string Encrypto(string source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(source);
            MemoryStream ms = new MemoryStream();
            _MobjCryptoService.Key = GetLegalKey();
            _MobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = _MobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();
            return Convert.ToBase64String(bytOut);
        }

        /// <summary>     
        /// 对称解密方法     
        /// </summary>     
        /// <param name="source">待解密的串</param>     
        /// <returns>经过解密的串</returns>     
        public string Decrypto(string source)
        {
            byte[] bytIn = Convert.FromBase64String(source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            _MobjCryptoService.Key = GetLegalKey();
            _MobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = _MobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        #endregion

        #region DES加密
        //默认密钥向量  
        private static byte[] _Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>  
        /// DES加密字符串  
        /// </summary>  
        /// <param name="encryptString">待加密的字符串</param>  
        /// <param name="encryptKey">加密密钥,要求为8位</param>  
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>  
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = _Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>      
        /// DES解密字符串      
        /// </summary>      
        /// <param name="decryptString">待解密的字符串</param>      
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>      
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>      
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = _Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        #endregion
    }
}
