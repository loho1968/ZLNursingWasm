using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;


namespace NursingCommon
{

    /// <summary>
    /// 文件操作
    /// </summary>
    public class FileOperate
    {
        private static Dictionary<long, long> lockDic = new Dictionary<long, long>();
        #region 读写
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="contents">保存内容</param>
        /// <param name="filePath">保存路径</param>
        /// <param name="replaceFile">是否替换原文件</param>
        /// <param name="addDatetime">保存路径</param>
        public static void SaveStringToFile(string contents, string filePath)
        {
            string dirName = Path.GetDirectoryName(filePath);
            if (Directory.Exists(dirName) == false)
                Directory.CreateDirectory(dirName);
            if (!System.IO.File.Exists(filePath))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(filePath))
                {
                    fs.Close();
                }
            }
            using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite, 8, System.IO.FileOptions.Asynchronous))
            {
                //Byte[] dataArray = System.Text.Encoding.ASCII.GetBytes(System.DateTime.Now.ToString() + content + "/r/n");  
                Byte[] dataArray = System.Text.Encoding.Default.GetBytes(contents);
                bool flag = true;
                long slen = dataArray.Length;
                long len = 0;
                while (flag)
                {
                    try
                    {
                        if (len >= fs.Length)
                        {
                            //fs.Lock(len, slen);
                            lockDic[len] = slen;
                            flag = false;
                        }
                        else
                        {
                            len = fs.Length;
                        }
                    }
                    catch (Exception ex)
                    {
                        while (!lockDic.ContainsKey(len))
                        {
                            len += lockDic[len];
                        }
                    }
                }
                fs.Seek(len, System.IO.SeekOrigin.Begin);
                fs.Write(dataArray, 0, dataArray.Length);
                fs.Close();
            }
        }



        /// <summary>
        /// 读取文件。
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadFile(string filePath)
        {
            StringBuilder arrText = new StringBuilder();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, System.Text.UTF8Encoding.UTF8))
                {
                    string sLine = "";
                    while (sLine != null)
                    {
                        sLine = sr.ReadLine();
                        if (sLine != null)
                            arrText.Append(sLine);
                    }
                    sr.Close();
                }
            }
            return arrText.ToString();
        }
        #endregion

        #region 文件转二进制互操作
        /// <summary>
        /// 二进制转文件
        /// </summary>
        /// <param name="c"></param>
        public static void ByteToFile(object c)
        {
            try
            {
                byte[] Files = (Byte[])c;
                using (BinaryWriter bw = new BinaryWriter(File.Open(@"D:\111.pdf", FileMode.OpenOrCreate)))
                {
                    bw.Write(Files);
                    bw.Close();
                }
            }
            catch
            {
                throw new Exception("二进制转文件出错！");
            }

        }
        /// <summary>
        /// 文件转二进制
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] FileToStream(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                BinaryReader br = new BinaryReader(fs);
                byte[] byData = br.ReadBytes((int)fs.Length);
                fs.Close();
                return byData;
            }
        }


        #endregion

    }
}
