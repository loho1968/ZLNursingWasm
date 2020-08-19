using System;
namespace NursingModel.Form
{

    /// <summary>
    /// 单据状态值域
    /// </summary>
    public static class ObservDataStatus
    {
        //登记
        public const string aaa = "registered";
        public const string 登记 = "registered";
        //更新
        public const string 更新 = "corrected";
        //作废
        public const string 作废 = "cancelled";
    }
    /// <summary>
    /// 病人所见项中的rel_proc值域
    /// </summary>

    public static class ObservProc
    {
        public const string 护理记录单 = "护理记录单";

        public const string 评估表 = "评估表";

        public const string 入出量 = "入出量";
        public const string 附着物评估 = "附着物评估";

        public const string 评分表 = "评分表";




    }
    /// <summary>
    /// 就诊场景值域
    /// </summary>
    public static class EncType
    {
        public const int 门诊 = 1;
        public const int 住院 = 2;
    }
    public static class SpecialObservItem
    {
        public const string 今日大便次数 = "69BFF987B67C409EAE65C9D1130126FC";
        public const string 血压 = "1721e94f-b19f-47f7-81e0-9e312e1d04e1,f3e0052b-3abb-4834-affb-d01ebd5a65a5";

    }
    /// <summary>
    /// 组合项目是否选项
    /// </summary>
    public static class ComboDetailValue
    {
        public const string 是 = "373066001";
        public const string 否 = "373067005";
    }
    /// <summary>
    /// 风险等级
    /// </summary>
    public static class ScaleRiskLevel
    {
        public const string 高 = "LA14637-5,LA14637-5,LA14637-5,LA9193-9,LA9193-9,LA13040-3,LA9193-9";
        public const string 中 = "LA19709-7,LA6751-7,LA6751-7,LA13039-5,LA6751-7,LA6751-7";
        public const string 低 = "LA6752-5,LA6752-5,LA13038-7,LA6752-5,LA19983-8,LA19952-3";
    }
    public static class BloodItem
    {
        public const string 血压项目 = "f3e0052b-3abb-4834-affb-d01ebd5a65a5,1721e94f-b19f-47f7-81e0-9e312e1d04e1";
    }

    public static class SupdevStatus
    {
        //病人附着物状态|1-使用,2-移除,3-作废
        public const int 使用 = 1;
        public const int 移除 = 2;
        public const int 作废 = 3;
    }
}
