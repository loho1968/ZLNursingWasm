using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ZlNursingCommon;

namespace NursingBaseFunc
{

    public class BaseFunction
    {
        
        public static List<ServicesStructure> GetControllerInfo<T>()
        {
            
            List<ServicesStructure> servicesStructures = new List<ServicesStructure>();
            var type = typeof(T);
            var methods = type.GetMethods();
            var listMethod = new List<MethodInfo>();
            foreach (var item in methods)
            {
                foreach (var attr in item.CustomAttributes)
                {
                    if (attr.AttributeType.Name == "HttpGetAttribute" || attr.AttributeType.Name == "HttpPostAttribute")
                    {
                        listMethod.Add(item);
                    }
                }
            }

            foreach (var item in listMethod)
            {
                if (item.Name != "Get")
                {
                    ServicesStructure servicesStructure = new ServicesStructure();
                    string str_schemaVal = "";
                    string srt_paraoutname = "";
                    string str_schemaContent = "";
                    foreach (var attributes in item.CustomAttributes)
                    {
                        if (attributes.AttributeType.Name == "DisplayName")
                        {
                            servicesStructure.displayname = attributes.NamedArguments[0].TypedValue.Value.ToString();
                        }
                        if (attributes.AttributeType.Name == "Note")
                        {
                            servicesStructure.note = attributes.NamedArguments[0].TypedValue.Value.ToString();
                        }
                        if (attributes.AttributeType.Name == "ParaOutName")
                        {
                            srt_paraoutname = attributes.NamedArguments[0].TypedValue.Value.ToString();
                        }
                        if (attributes.AttributeType.Name == "SchemaVal")
                        {
                            str_schemaVal = attributes.NamedArguments[0].TypedValue.Value.ToString();
                        }
                        if (attributes.AttributeType.Name == "SchemaContent")
                        {
                            str_schemaContent = attributes.NamedArguments[0].TypedValue.Value.ToString();

                        }
                        if (attributes.AttributeType.Name == "HttpGetAttribute")
                        {
                            servicesStructure.methodRequest = "Get";
                        }
                        if (attributes.AttributeType.Name == "HttpPostAttribute")
                        {
                            servicesStructure.methodRequest = "Post";
                        }
                    }

                    servicesStructure.name = item.Name;


                    List<Parameter> parameters = new List<Parameter>();
                    int i = 1;
                    foreach (var param in item.GetParameters())
                    {
                        Parameter parameter = new Parameter();
                        var type1 = param.ParameterType;
                        parameter.Order = i;
                        parameter.name = param.Name;
                        if (param.ParameterType.Name.IndexOf("Deatil", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            parameter.original_Type = "string";
                        }
                        else
                        {
                            parameter.original_Type = param.ParameterType.Name == "Int32" ? "int" : param.ParameterType.Name;
                        }
                        parameter.direction = "in";
                        parameter.schemaVal = "";
                        parameters.Add(parameter);
                        i++;
                    }
                    Parameter parameterOut = new Parameter();
                    parameterOut.Order = i;
                    parameterOut.name = srt_paraoutname;
                    parameterOut.original_Type = item.ReturnType.Name == "Int32" ? "int" : item.ReturnType.Name;
                    parameterOut.direction = "out";
                    if (str_schemaContent != "")
                    {

                        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NursingModel.dll");
                        var assembly = Assembly.LoadFrom(path);
                       
                        Object obj = null;
                        GetClassJson(str_schemaContent, assembly, null, ref obj);
                        string str_classjson = JsonConvert.SerializeObject(obj);
                        string str_schema = GenerateJSchema.JsonToJSchemaText(str_classjson);

                        parameterOut.schemaVal = str_schema;
                    }
                    else
                    {
                        parameterOut.schemaVal = str_schemaVal;
                    }

                    parameters.Add(parameterOut);
                    servicesStructure.parameter = parameters;
                    servicesStructures.Add(servicesStructure);
                }
            }
            
            return servicesStructures;
        }

        private static void GetClassJson(string str_classname, Assembly assembly, Object subObj, ref Object obj)
        {
            Type o;
            if (str_classname != "")
            {
                 o = assembly.GetType("NursingModel." + str_classname);//加载类型
            }
            else {
                 o = assembly.GetType("NursingModel");//加载类型
            }

            object sub_obj = Activator.CreateInstance(o, true);//根据类型创建实例
            if (obj == null)
            {
                obj = sub_obj;
            }
            else
            {
                //PropertyInfo pi = obj.GetType().GetProperties().SingleOrDefault(m => m.PropertyType.GenericTypeArguments[0].Name == str_classname);
                foreach (var pi in subObj.GetType().GetProperties())
                {
                    var isGenericType = pi.PropertyType.IsGenericType;
                    var list = pi.PropertyType.GetInterface("IEnumerable", false);
                    if ((isGenericType && list != null) /*|| prop.PropertyType.IsArray*/)
                    {
                        if (pi.PropertyType.GenericTypeArguments[0].Name == str_classname)
                        {
                            var listType = Activator.CreateInstance(pi.PropertyType, true);
                            var methodType = pi.PropertyType.GetMethod("Add");
                            methodType.Invoke(listType, new object[] { sub_obj });
                            pi.SetValue(subObj, listType);
                            break;
                        }
                    }
                }
            }

            foreach (var prop in sub_obj.GetType().GetProperties())
            {
                var isGenericType = prop.PropertyType.IsGenericType;
                var list = prop.PropertyType.GetInterface("IEnumerable", false);
                if ((isGenericType && list != null) /*|| prop.PropertyType.IsArray*/)
                {
                    string str_subclassname = prop.PropertyType.GenericTypeArguments[0].Name;
                    GetClassJson(str_subclassname, assembly, sub_obj, ref obj);
                }
            }
        }

    }
}
