using System;
using System.Collections.Generic;
using System.Text;

namespace ZlNursingCommon
{
    [Serializable]
    public class Parameter
    {
        /// <summary>
        /// 
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string original_Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schemaVal { get; set; }

        public string schemaContent { get; set; }

    }
    [Serializable]
    public class ServicesStructure
    {
        /// <summary>
        /// 
        /// </summary>
        /// 

        public string name { get; set; }

        public string displayname
        {
            get; set;   
        }

        public string note { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string methodRequest { get; set; }
        public List<Parameter> parameter { get; set; }
    }

}
