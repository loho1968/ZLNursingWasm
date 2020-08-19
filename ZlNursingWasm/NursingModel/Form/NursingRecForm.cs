using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NursingModel.Form
{
    //护理记录单
    [Table("NURSE_DATA_FORM")]
    public class NursingRecForm
    {
        ////表单格式|BH中使用
        //[Column("FORM_STYLE")]
        //[JsonProperty("FORM_STYLE")]
        //public string FormStyle { get; set; }

        //护理记录单类型|记录、查看
        [Column("FORM_TYPE")]
        [JsonProperty("FORM_TYPE")]
        public string FormType { get; set; }

        //护理记录单id
        [Column("NURSE_DATA_FORM_ID")]
        [JsonProperty("NURSE_DATA_FORM_ID")]
        public string FormId { get; set; }

        //护理记录单名称
        [Column("NURSE_DATA_FORM_NAME")]
        [JsonProperty("NURSE_DATA_FORM_NAME")]
        public string FormName { get; set; }

        //婴儿记录单标识|1-大人使用，2-婴儿使用，3-通用
        [Column("USCOPE")]
        [JsonProperty("USCOPE")]
        public int Uscope { get; set; }

        //表单项目

        public List<NursingRecFormItem> FormItems { get; set; }

    }
}
