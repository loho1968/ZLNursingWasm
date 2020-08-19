using System;
using System.Collections.Generic;

namespace NursingModel
{
    /// <summary>
    /// 示例类
    /// </summary>
    public class Example
    {
        /// <summary>
        /// 名称
        /// 
        /// </summary>
        public string FormName { get; set; }

        public string FormID { get; set; }

    }

 

        [Serializable]
        public class AdviceList
        {
            //来源与：zl_ADVICE_Lists


            /// <summary>
            /// 类别,1=,2=,医嘱计算出来中生成的，用于表示医嘱是否有detail
            /// 原来：LB
            /// /// </summary>
            public int Type { get; set; }


            /// <summary>
            /// 医嘱ID
            /// 原来：YZID
            /// </summary>
            public long Id { get; set; }

            

            /// <summary>
            /// 子医嘱明细
            /// 原来：ITEMLIST
            /// </summary>
            public List<AdviceDetail> Details { get; set; }
        }
        [Serializable]
        public class AdviceDetail
        {
            /// <summary>
            /// 名称
            /// 原来：MC
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 规格
            /// 原来：
            /// </summary>
            public string Specific { get; set; }

            /// <summary>
            /// 总量
            /// 原来：ZL
            /// </summary>
            public decimal TotalUsage { get; set; }

            /// <summary>
            /// 总量单位
            /// 原来：ZLDW
            /// </summary>
            public string TotalUnit { get; set; }

            /// <summary>
            /// 单次用量
            /// 原来：DL
            /// </summary>
            public decimal OnceUsage { get; set; }

            /// <summary>
            /// 单位
            /// 原来：DW
            /// </summary>
            public string Unit { get; set; }
            /// <summary>
            /// 高危药品
            /// 原来：GW
            /// </summary>
            public int HighRiskDrug { get; set; }

        }
    }

