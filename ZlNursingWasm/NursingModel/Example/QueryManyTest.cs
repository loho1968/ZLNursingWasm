using System;
using System.Collections.Generic;
using System.Text;

namespace NursingModel
{
    /// <summary>
    /// 多表关联测试
    /// </summary>
   public class QueryManyTest
    {
        /// <summary>
        /// 来自于DrugExecuteConfig表
        /// </summary>
        public string DrugName { get; set; }

        /// <summary>
        /// 来自于TimingList表
        /// </summary>
        public string ZlhisId { get; set; }
    }
}
