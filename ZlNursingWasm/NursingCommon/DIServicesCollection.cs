using System;
using System.Collections.Generic;
using System.Text;

namespace NursingCommon
{
    /// <summary>
    /// 注入对象
    /// </summary>
    public class DIServicesCollection
    {
        //注入实例
        public static IServiceProvider Instance { get; set; }
    }
}
