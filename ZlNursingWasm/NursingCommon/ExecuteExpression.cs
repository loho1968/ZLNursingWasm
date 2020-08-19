using System;
using DynamicExpresso;

namespace NursingCommon
{
    public class ExecuteExpression
    {
        /// <summary>
        /// 执行文本表达式，返回bool结果
        /// </summary>
        /// <param name="rule">文本表达式</param>
        /// <returns></returns>
        public static bool Exec(string rule)
        {
            Interpreter exp = new Interpreter();

            var obj = exp.Eval(rule);
            bool rs = bool.Parse(obj.ToString());
            return rs;
        }
    }
}
