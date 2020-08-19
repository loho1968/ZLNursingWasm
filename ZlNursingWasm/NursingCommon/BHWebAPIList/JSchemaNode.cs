using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;

namespace ZlNursingCommon
{
    public class JSchemaNode
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 主要有文本、数字、是否、对象类型。数组作为节点的属性处理，不作为一个单独的数据类型。
        /// </summary>
        public string DataType;

        /// <summary>
        /// 是否数组
        /// </summary>
        public bool IsArray;

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool Reqierd;

        /// <summary>
        /// 说明
        /// </summary>
        public string Description;

        private List<JSchemaNode> children;
        /// <summary>
        /// 子节点
        /// </summary>
        public List<JSchemaNode> Children
        {
            get
            {
                if (children == null)
                {
                    children = new List<JSchemaNode>();
                }

                return children;
            }
        }

        public override string ToString()
        {
            JObject jObject = ToJObj(this, true);
            if (jObject == null)
            {
                return null;
            }

            return jObject.ToString(Newtonsoft.Json.Formatting.None);
        }

        public string ToStringWidthIndent()
        {
            JObject jObject = ToJObj(this, true);
            if (jObject == null)
            {
                return null;
            }

            return jObject.ToString();
        }

        private static JObject ToJObj(JSchemaNode node, bool isRoot)
        {
            JObject jObj = new JObject();
            if (isRoot)
            {
                jObj.Add("$schema", new JValue("http://json-schema.org/draft-03/schema#"));
            }

            if (!string.IsNullOrWhiteSpace(node.Description))
            {
                jObj.Add("description", new JValue(node.Description));
            }

            //节点类型
            if (node.IsArray)
            {
                jObj.Add("type", new JValue("array"));
            }
            else
            {
                jObj.Add("type", new JValue(node.DataType));
            }

            if (node.Reqierd)
            {
                jObj.Add("required", new JValue(true));
            }

            if (node.IsArray)
            {
                //解析数组成员的定义
                JSchemaNode temp = new JSchemaNode();
                temp.IsArray = false;
                temp.Name = node.Name;
                temp.Reqierd = node.Reqierd;
                temp.DataType = node.DataType;
                temp.children = node.children;

                //递归调用
                JObject child = ToJObj(temp, false);
                jObj.Add("items", child);
            }
            else if (node.DataType == "object")
            {
                //解析对象的属性节点
                if (node.Children.Count > 0)
                {
                    JObject proValue = new JObject();
                    jObj.Add("properties", proValue);

                    foreach (JSchemaNode item in node.Children)
                    {
                        //递归调用
                        JObject child = ToJObj(item, false);
                        proValue.Add(item.Name, child);
                    }
                }
            }

            return jObj;
        }

        public static JSchemaNode LoadFromJSchemaText(string jSchemaText)
        {
            if (string.IsNullOrWhiteSpace(jSchemaText))
            {
                return null;
            }

            JObject jObj = JObject.Parse(jSchemaText);
            return LoadFromJSchemaObj(jObj);
        }

        private static JSchemaNode LoadFromJSchemaObj(JObject jSchemaObj)
        {
            if (jSchemaObj == null)
            {
                return null;
            }

            JSchemaNode result = new JSchemaNode();
            JProperty proRequired = jSchemaObj.Property("required");
            if (proRequired != null)
            {
                result.Reqierd = proRequired.Value.Value<bool>();
            }

            JProperty proDescription = jSchemaObj.Property("description");
            if (proDescription != null)
            {
                result.Description = proDescription.Value.Value<string>();
            }

            JProperty proType = jSchemaObj.Property("type");
            if (proType == null)
            {
                result.DataType = "null";
                return result;
            }

            string dataType = proType.Value.Value<string>();
            result.DataType = dataType;

            if (dataType == "array")
            {
                result.IsArray = true;
                result.DataType = "string";

                JProperty temp = jSchemaObj.Property("items");
                if (temp == null)
                {
                    return result;
                }

                JObject childrenInfo = null;
                if (temp.Value is JObject)
                {
                    childrenInfo = (JObject)temp.Value;
                }
                else if (temp.Value is JArray)
                {
                    JArray array = (JArray)temp.Value;
                    if (array.Count > 0)
                    {
                        childrenInfo = array.First as JObject;
                    }
                }

                if (childrenInfo == null)
                {
                    return result;
                }

                JSchemaNode arrayNode = LoadFromJSchemaObj(childrenInfo);
                arrayNode.IsArray = true;
                arrayNode.Reqierd = result.Reqierd;
                arrayNode.Description = result.Description;
                return arrayNode;
            }
            else if (dataType == "object")
            {
                JProperty temp = jSchemaObj.Property("properties");
                if (temp == null)
                {
                    return result;
                }

                JObject childrenInfo = temp.Value as JObject;
                if (childrenInfo == null)
                {
                    return result;
                }

                foreach (JProperty child in childrenInfo.Properties())
                {
                    if (!(child.Value is JObject))
                    {
                        continue;
                    }

                    //递归调用
                    JSchemaNode childNode = LoadFromJSchemaObj((JObject)child.Value);
                    childNode.Name = child.Name;
                    result.Children.Add(childNode);
                }
            }

            return result;
        }

        public static string ValidateJsonSchema(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
            {
                return null;
            }

            try
            {
                JObject jObj = JToken.Parse(txt) as JObject;
                JsonSchema.Parse(txt);

                if (jObj == null || jObj.Property("type") == null)
                {
                    return "不是有效的JsonSchema文本，或者属性名称包含大写字符。";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return null;
        }
    }
}
