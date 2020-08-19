using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZlNursingServices.Controllers
{
    /// <summary>
    /// 自定义属性标识控制器
    /// </summary>
    public class DisplayName: Attribute
    {
        public string name { get; set; }
    }

    public class Note : Attribute
    {
        public string name { get; set; }
    }
    public class SchemaVal : Attribute
    {
        public string name { get; set; }
    }
    public class SchemaContent : Attribute
    {
        public string name { get; set; }
    }

    public class ParaOutName : Attribute
    {
        public string name { get; set; }
    }
}
