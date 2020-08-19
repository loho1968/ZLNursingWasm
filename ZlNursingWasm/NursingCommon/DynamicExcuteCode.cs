using Jint;
using System;
using System.Collections.Generic;
using System.Text;

namespace NursingCommon
{
    /// <summary>
    /// 动态执行c#代码
    /// </summary>
    public class DynamicExcuteCode
    {
       
        /// <summary>
        /// 执行js代码（表达式等）
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
       public object ExcuteExpresionCode(string code)
        {
            Jint.Engine engine = new Jint.Engine();
            object result = engine.Execute(code).GetCompletionValue().ToObject();
            return result;
        }

        /// <summary>
        /// 执行js函数
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public void ExcuteFunctionCode(string code,out object outvalue)
        {
            outvalue = null;
            var engine = new Engine().SetValue("outvalue", new Action<object>(Console.WriteLine));
            engine.Execute(code);
        }

    }
}
