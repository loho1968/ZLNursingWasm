using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZlNursingCommon
{
    public static class GenerateJSchema
    {
        public static JSchemaNode JsonToJSchema(JToken jsonData)
        {
            if (jsonData == null)
            {
                return null;
            }

            if (jsonData is JObject)
            {
                JSchemaNode jSchemaNode = new JSchemaNode();
                jSchemaNode.DataType = "object";

                JObject jObj = (JObject)jsonData;
                IEnumerable<JProperty> proList = jObj.Properties();
                foreach (JProperty pro in proList)
                {
                    //递归调用
                    JSchemaNode cNode = JsonToJSchema(pro.Value);
                    cNode.Name = pro.Name;
                    jSchemaNode.Children.Add(cNode);
                }

                return jSchemaNode;
            }
            else if (jsonData is JArray)
            {
                JArray jArray = (JArray)jsonData;
                if (jArray.Count > 0)
                {
                    //递归调用
                    JSchemaNode jSchemaNode = JsonToJSchema(jArray.First);
                    jSchemaNode.IsArray = true;
                    return jSchemaNode;
                }
                else
                {
                    JSchemaNode jSchemaNode = new JSchemaNode();
                    jSchemaNode.IsArray = true;
                    jSchemaNode.DataType = "string";
                    return jSchemaNode;
                }
            }
            else if (jsonData is JValue)
            {
                JSchemaNode jSchemaNode = new JSchemaNode();
                if (jsonData.Type == JTokenType.String)
                {
                    jSchemaNode.DataType = "string";
                }
                else if (jsonData.Type == JTokenType.Integer
                    || jsonData.Type == JTokenType.Float)
                {
                    //json的数据类型并没有区分整数和浮点数，因此统一使用浮点数定义数字类型。
                    jSchemaNode.DataType = "number";
                    if (jsonData.Type == JTokenType.Integer)
                    {
                        jSchemaNode.Description = "int";
                    }
                }
                else if (jsonData.Type == JTokenType.Boolean)
                {
                    jSchemaNode.DataType = "boolean";
                }
                else
                {
                    jSchemaNode.DataType = "string";
                }

                return jSchemaNode;
            }
            else
            {
                throw new Exception(string.Format("从Json生成JSchema时，发现不能识别的节点类型：{0}", jsonData.GetType().FullName));
            }
        }

        public static string JsonToJSchemaText(string jsonText)
        {
            if (string.IsNullOrWhiteSpace(jsonText))
            {
                return null;
            }

            JToken token = JToken.Parse(jsonText);
            JSchemaNode jSchema = JsonToJSchema(token);
            return jSchema.ToString();
        }
    }
}
