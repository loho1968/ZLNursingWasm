using System;
using System.Collections.Generic;
using System.Text;

namespace NursingCommon
{
    /// <summary>
    /// 手动获取注入对象
    /// </summary>
    public class ServiceLocator
    {
        public static IServiceProvider Instance { get; set; }
    }
}
