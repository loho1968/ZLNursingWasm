using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NursingCommon
{
    /// <summary>
    /// XML操作
    /// </summary>
    public class DataTableToXML
    {
        public static string ConvertDataTableToXML(DataTable xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {

                
                stream = new MemoryStream();
                writer = new XmlTextWriter(stream, Encoding.Default);
                xmlDS.TableName = "PATIENT";
                xmlDS.WriteXml(writer);



                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UTF8Encoding utf = new UTF8Encoding();
                return utf.GetString(arr).Trim() ;
                //return "<OUTPUT>"+utf.GetString(arr).Trim()+ "</OUTPUT>";
                /*
                StringBuilder strXml = new StringBuilder();
                strXml.AppendLine("<MonitorData>");
                for (int i = 0; i < xmlDS.Rows.Count; i++)
                {
                    strXml.AppendLine("<rows>");
                    for (int j = 0; j < xmlDS.Columns.Count; j++)
                    {
                        strXml.AppendLine("<" + xmlDS.Columns[j].ColumnName + ">" + xmlDS.Rows[i][j] + "</" + xmlDS.Columns[j].ColumnName + ">");
                    }
                    strXml.AppendLine("</rows>");
                }
                strXml.AppendLine("</MonitorData>");

                return strXml.ToString();*/

            }
            catch (System.Exception ex)
            {
                //return String.Empty;
                throw ex;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }
        public static DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        /// <summary>
        /// xml转成jsonObject类型
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string XmlStringToJson(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
            
            return json;
            
        }
        /// <summary>
        /// 将xml转换为List对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="rootName"></param>
        /// <returns></returns>
        public static List<T> XmlToList<T>(string xml, string rootName) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(rootName));
            using (StringReader sr = new StringReader(xml))
            {
                List<T> list = serializer.Deserialize(sr) as List<T>;
                return list;
            }
        }

    }
}
