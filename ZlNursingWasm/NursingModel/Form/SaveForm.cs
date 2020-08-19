using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace NursingModel.Form
{
    //如果好用，请收藏地址，帮忙分享。
    public class SaveForm
    {
        /// <summary>
        /// 
        /// </summary>
        public string form_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string form_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? scale_score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scale_result_id { get; set; }
        /// <summary>
        /// 高危
        /// </summary>
        public string risk_level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string result_exp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pat_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? enc_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string encounter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nb_sno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ward_id { get; set; }
        /// <summary>
        /// 骨科一病区
        /// </summary>
        public string ward_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recorder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recorder_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string form_rec_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string observ_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string supdev_type_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string supdev_type_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pat_supdev_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pat_supdev_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hasValue { get; set; }

        public List<FormNi> form_ni { get; set; }

        public List<Result> result { get; set; }

        public string EditType { get; set; }

        public string NursingStep { get; set; }
    }

    //如果好用，请收藏地址，帮忙分享。
    public class Result
    {
        /// <summary>
        /// 
        /// </summary>
        public string item_score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string item_id { get; set; }
        /// <summary>
        /// 感知能力
        /// </summary>
        public string item_name { get; set; }
        /// <summary>
        /// 非常受限
        /// </summary>
        public string item_value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string detail_value_id { get; set; }

        public string observ_item_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string observ_item_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string observ_item_code { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public List<ComboChildren> item_children { get; set; }


    }

    public class ComboChildren
    {
        public string observ_itemid { get; set; }
        public string item_value { get; set; }
    }

    public class FormNi
    {
        public string ni_list_id { get; set; }
    }

}
