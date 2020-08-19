using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace NursingCommon
{
    /// <summary>
    /// HTTP请求帮助类
    /// </summary>
    public class HttpRequest
    {
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="authType">认证类型</param>
        /// <param name="secrect">密钥，若无认证，则不传</param>
        public static string RequestData(string POSTURL, string PostData, AuthType authType, string secrect = "")
        {
            //发送请求的数据
            WebRequest myHttpWebRequest = WebRequest.Create(POSTURL);
            myHttpWebRequest.Method = "POST";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byte1 = encoding.GetBytes(PostData);
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";//application/x-www-form-urlencoded
            myHttpWebRequest.ContentLength = byte1.Length;
            if (authType == AuthType.Basic)
            {
                WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
                webHeaderCollection.Add("Authorization", "Basic " + secrect);
                myHttpWebRequest.Headers = webHeaderCollection;
            }
            else if (authType == AuthType.Oauth)
            {
                WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
                webHeaderCollection.Add("Authorization", "Bearer " + secrect);
                myHttpWebRequest.Headers = webHeaderCollection;
            }
            Stream newStream = myHttpWebRequest.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            newStream.Close();

            //发送成功后接收返回的XML信息
            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            string lcHtml = string.Empty;
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, enc);
            lcHtml = streamReader.ReadToEnd();
            return lcHtml;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="authType">认证类型</param>
        /// <param name="secrect">密钥，若无认证，则不传</param>
        /// <returns></returns>
        public static string HttpGet(string url, AuthType authType, string secrect = "")
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Method = "GET";
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                if (authType == AuthType.Basic)
                {
                    WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
                    webHeaderCollection.Add("Authorization", "Basic " + secrect);
                    wbRequest.Headers = webHeaderCollection;
                }
                else if (authType == AuthType.Oauth)
                {
                    WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
                    webHeaderCollection.Add("Authorization", "Bearer " + secrect);
                    wbRequest.Headers = webHeaderCollection;
                }
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sReader = new StreamReader(responseStream))
                    {
                        result = sReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return result;
        }
    }

    /// <summary>
    /// 认证类型
    /// </summary>
    public enum AuthType
    {
        None,
        Basic,
        Oauth
    }
}
