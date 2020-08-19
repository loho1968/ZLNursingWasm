﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NursingCommon
{
    public class RandomStringBuilder
    {
        /// <summary>
        /// 生成单个随机数字
        /// </summary>
        private static int createNum()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int num = random.Next(10);
            return num;
        }

        /// <summary>
        /// 生成单个大写随机字母
        /// </summary>
        private static string createBigAbc()
        {
            //A-Z的 ASCII值为65-90
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int num = random.Next(65, 91);
            string abc = Convert.ToChar(num).ToString();
            return abc;
        }

        /// <summary>
        /// 生成单个小写随机字母
        /// </summary>
        private static string createSmallAbc()
        {
            //a-z的 ASCII值为97-122
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int num = random.Next(97, 123);
            string abc = Convert.ToChar(num).ToString();
            return abc;
        }


        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length">字符串的长度</param>
        /// <returns></returns>
        public static string Create(int length)
        {
            // 创建一个StringBuilder对象存储密码
            StringBuilder sb = new StringBuilder();
            //使用for循环把单个字符填充进StringBuilder对象里面变成14位密码字符串
            for (int i = 0; i < length; i++)
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                //随机选择里面其中的一种字符生成
                switch (random.Next(3))
                {
                    case 0:
                        //调用生成生成随机数字的方法
                        sb.Append(createNum());
                        break;
                    case 1:
                        //调用生成生成随机小写字母的方法
                        sb.Append(createSmallAbc());
                        break;
                    case 2:
                        //调用生成生成随机大写字母的方法
                        sb.Append(createBigAbc());
                        break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 从wwwroot/ServiceAddress/address.txt中读取服务器ip+端口号
        /// </summary>
        /// <returns></returns>
        public static string ReadAddressinfo()
        {
            string line = "";
            string path = Directory.GetCurrentDirectory() + @"/wwwroot/ServiceAddress/address.txt";
            FileStream fileStream = new FileStream(path, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                line = reader.ReadLine();
            }
            if (line == null)
            {
                line = "0.0.0.0:8088";
            }
            return line;
        }
    }
}
