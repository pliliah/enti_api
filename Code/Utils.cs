using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace enti_api.Code
{
    public static class Utils
    {
        public static string ObjectToXML(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = new XmlTextWriter(sw);
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());   
                serializer.Serialize(tw, o);
                var result = sw.ToString();
                result = result.Remove(0, result.IndexOf("?>") + 2);
                return result;
            }
            catch (Exception ex)
            {
                //Handle Exception Code
                return "";
            }
            finally
            {
                sw.Close();
                tw.Close();
            }
        }
    }
}