using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using Newtonsoft.Json;

namespace NursingCommon
{
    public static class ModelConvert
    {
        /// <summary> 
        /// DataSet装换为泛型集合 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="p_DataSet">DataSet</param> 
        /// <param name="p_TableIndex">待转换数据表索引</param> 
        /// <returns></returns> 
        ///
        public static IList<T> DataSetToIList<T>(DataSet p_DataSet, int p_TableIndex)
        {
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (p_TableIndex > p_DataSet.Tables.Count - 1)
                return null;
            if (p_TableIndex < 0)
                p_TableIndex = 0;

            DataTable p_Data = p_DataSet.Tables[p_TableIndex];
            // 返回值初始化 
            IList<T> result = new List<T>();
            for (int j = 0; j < p_Data.Rows.Count; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    for (int i = 0; i < p_Data.Columns.Count; i++)
                    {
                        // 属性与字段名称一致的进行赋值 
                        if (pi.Name.ToUpper().Equals(p_Data.Columns[i].ColumnName.ToUpper()))
                        {
                            // 数据库NULL值单独处理 
                            if (p_Data.Rows[j][i] != DBNull.Value)
                                pi.SetValue(_t, p_Data.Rows[j][i], null);
                            else
                                pi.SetValue(_t, null, null);
                            break;
                        }
                    }
                }
                result.Add(_t);
            }
            return result;
        }

        /// <summary>
        /// DataTable装换为泛型集合 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> DataTableToIList<T>(DataTable dt)
        {
            var list = new List<T>();
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());
            foreach (DataRow item in dt.Rows)
            {
                T s = Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name.ToUpper() == dt.Columns[i].ColumnName.ToUpper());
                    if (info != null)
                    {
                        try
                        {
                            if (!Convert.IsDBNull(item[i]))
                            {
                                object v = null;
                                if (info.PropertyType.ToString().Contains("System.Nullable"))
                                {
                                    v = Convert.ChangeType(item[i], Nullable.GetUnderlyingType(info.PropertyType));
                                }
                                else
                                {
                                    v = Convert.ChangeType(item[i], info.PropertyType);
                                }
                                info.SetValue(s, v, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("字段[" + info.Name + "]转换出错," + ex.Message);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }
        /// <summary>
        /// DataTable装换为Dictionary集合 (字典)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<Dictionary<object, object>> DataTableToDic(DataTable dt)
        {
            IList<Dictionary<object, object>> keyValuePairs = new List<Dictionary<object, object>>();

            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<object, object> temp = new Dictionary<object, object>();
                foreach (DataColumn item in dt.Columns)
                {
                    temp.Add(item.ColumnName, dr[item]);
                }
                keyValuePairs.Add(temp);
            }
            return keyValuePairs;

        }
        /// <summary>
        /// 通过JSON中转，把DataTable转LIST，但是需要定义Class的Json属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">转换的DataTable</param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(this DataTable dt)
        {
            List<T> ts = new List<T>();// 定义集合
            
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            var json = JsonConvert.SerializeObject(dt,settings);
            ts = JsonConvert.DeserializeObject<List<T>>(json);
            return ts;
        }

        //public static List<T> DataTableToList<T>(this DataTable dataTable) where T : new()
        //{
        //    var dataList = new List<T>();

        //    //Define what attributes to be read from the class
        //    const System.Reflection.BindingFlags flags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;

        //    //Read Attribute Names and Types
        //    var objFieldNames = typeof(T).GetProperties(flags).Cast<System.Reflection.PropertyInfo>().
        //        Select(item => new
        //        {
        //            Name = item.Name,
        //            Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
        //        }).ToList();

        //    //Read Datatable column names and types
        //    var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
        //        Select(item => new
        //        {
        //            Name = item.ColumnName,
        //            Type = item.DataType
        //        }).ToList();

        //    foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
        //    {
        //        var classObj = new T();

        //        foreach (var dtField in dtlFieldNames)
        //        {
        //            System.Reflection.PropertyInfo propertyInfos = classObj.GetType().GetProperties().Where(o=>o.Name.ToUpper()==dtField.Name.ToUpper()).FirstOrDefault();

        //            //var a=classObj.GetType().GetProperties().ElementAt(0).GetCustomAttributes(false).ElementAt(0) as ColumnAttribute;


        //            var field = objFieldNames.Find(x => x.Name.ToUpper() == dtField.Name);

        //            if (field != null && propertyInfos!=null)
        //            {

        //                if (propertyInfos.PropertyType == typeof(DateTime))
        //                {
        //                    propertyInfos.SetValue
        //                    (classObj, convertToDateTime(dataRow[dtField.Name]), null);
        //                }
        //                else if (propertyInfos.PropertyType == typeof(Nullable<DateTime>))
        //                {
        //                    propertyInfos.SetValue
        //                    (classObj, convertToDateTime(dataRow[dtField.Name]), null);
        //                }
        //                else if (propertyInfos.PropertyType == typeof(int))
        //                {
        //                    propertyInfos.SetValue
        //                    (classObj, ConvertToInt(dataRow[dtField.Name]), null);
        //                }
        //                else if (propertyInfos.PropertyType == typeof(long))
        //                {
        //                    propertyInfos.SetValue
        //                    (classObj, ConvertToLong(dataRow[dtField.Name]), null);
        //                }
        //                else if (propertyInfos.PropertyType == typeof(decimal))
        //                {
        //                    propertyInfos.SetValue
        //                    (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
        //                }
        //                else if (propertyInfos.PropertyType == typeof(String))
        //                {
        //                    if (dataRow[dtField.Name].GetType() == typeof(DateTime))
        //                    {
        //                        propertyInfos.SetValue
        //                        (classObj, ConvertToDateString(dataRow[dtField.Name]), null);
        //                    }
        //                    else
        //                    {
        //                        propertyInfos.SetValue
        //                        (classObj, ConvertToString(dataRow[dtField.Name]), null);
        //                    }
        //                }
        //                else
        //                {

        //                    propertyInfos.SetValue
        //                        (classObj, Convert.ChangeType(dataRow[dtField.Name], propertyInfos.PropertyType), null);

        //                }
        //            }
        //        }
        //        dataList.Add(classObj);
        //    }
        //    return dataList;
        //}

        //#region 扩展方法仅供DataTableToList 使用
        //public static string ConvertToDateString(object date)
        //{
        //    if (date == null)
        //        return string.Empty;

        //    return  Convert.ToDateTime(date).ToString("yyyy-MM-dd HH:mm:ss.fff");
        //}

        //private static string ConvertToString(object value)
        //{
        //    return Convert.ToString(ReturnEmptyIfNull(value));
        //}

        //private static int ConvertToInt(object value)
        //{
        //    return Convert.ToInt32(ReturnZeroIfNull(value));
        //}

        //private static long ConvertToLong(object value)
        //{
        //    return Convert.ToInt64(ReturnZeroIfNull(value));
        //}

        //private static decimal ConvertToDecimal(object value)
        //{
        //    return Convert.ToDecimal(ReturnZeroIfNull(value));
        //}

        //private static DateTime convertToDateTime(object date)
        //{
        //    return Convert.ToDateTime(ReturnDateTimeMinIfNull(date));
        //}

        //public static object ReturnEmptyIfNull(this object value)
        //{
        //    if (value == DBNull.Value)
        //        return string.Empty;
        //    if (value == null)
        //        return string.Empty;
        //    return value;
        //}
        //public static object ReturnZeroIfNull(this object value)
        //{
        //    if (value == DBNull.Value)
        //        return 0;
        //    if (value == null)
        //        return 0;
        //    return value;
        //}
        //public static object ReturnDateTimeMinIfNull(this object value)
        //{
        //    if (value == DBNull.Value)
        //        return DateTime.MinValue;
        //    if (value == null)
        //        return DateTime.MinValue;
        //    return value;
        //}
        //#endregion
    }
}
