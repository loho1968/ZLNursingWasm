using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NursingCommon
{
    public class PdfHelper
    {
        //PDF文件转Stream流
        //public static Stream FileToStream(string fileName) //fileName 文件地址/文件全名（xxx.pdf）
        //{
        //    // 打开文件
        //    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
        //    {
        //        //读取文件的 byte[]
        //        byte[] bytes = new byte[fs.Length];
        //        fs.Read(bytes, 0, bytes.Length);
        //        fs.Close();
        //        //把 byte[] 转换成 Stream
        //        Stream stream = new MemoryStream(bytes);
        //        return stream;
        //    }

        //}
        /// <summary>
        /// 读取文件。
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] FileToStream(string filePath)
        {
            //StringBuilder arrText = new StringBuilder();
            //using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            //{
            //    //读取文件的 byte[]
            //    byte[] bytes = new byte[fs.Length];
            //    fs.Read(bytes, 0, bytes.Length);
            //    fs.Close();
            //    //把 byte[] 转换成 Stream
            //    Stream stream = new MemoryStream(bytes);
            //    return stream;
            //}
            FileStream fs = new FileStream(filePath, FileMode.Open,FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs);
            byte[] byData = br.ReadBytes((int)fs.Length);
            fs.Close();
            return byData;
        }
        //Stream流转PDF文件
        public static void StreamToFile(Stream stream, string fileName) //stream PDF转化的流 fileName 文件地址/文件全名（xxx.pdf）
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        public static void ByteToFile(object c)
        {
            byte[] Files = (Byte[])c;
            BinaryWriter bw = new BinaryWriter(File.Open(@"D:\111.pdf", FileMode.OpenOrCreate));
            bw.Write(Files);
            bw.Close();
        }
    }
}
