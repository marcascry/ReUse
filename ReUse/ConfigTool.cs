using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static ReUse.Option;

namespace ReUse
{
    public class ConfigTool
    {
        public static string SettingPath = $@"{AppDomain.CurrentDomain.BaseDirectory}Setting.xml";

        public static void Create_NewSettingFile()
        {
            try
            {
                if (!File.Exists(SettingPath))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlElement setting = xmlDoc.CreateElement("Setting");
                    xmlDoc.AppendChild(setting);
                    xmlDoc.Save(SettingPath);
                }
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
            }
        }

        public static void AddElement(string element)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(SettingPath);

                XmlElement setting = (XmlElement)xmlDoc.SelectSingleNode("Setting");
                XmlElement newElement = xmlDoc.CreateElement(element);

                setting.AppendChild(newElement);
                xmlDoc.Save(SettingPath);
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
            }
        }

        public static void AddAttribute(String element, String attribute, String value = "")
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(SettingPath);
                XmlElement setting = (XmlElement)xmlDoc.SelectSingleNode("Setting");
                XmlElement selectedElement = (XmlElement)setting.SelectSingleNode(element);
                selectedElement.SetAttribute(attribute, value);
                xmlDoc.Save(SettingPath);
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
            }
        }

        public static void ChangeSetting(String element, String attribute, String value)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(SettingPath);
                XmlElement setting = (XmlElement)xmlDoc.SelectSingleNode("Setting");
                XmlElement selectedElement = (XmlElement)setting.SelectSingleNode(element);
                selectedElement.SetAttribute(attribute, value);
                xmlDoc.Save(SettingPath);
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
            }
        }

        public static void CheckExist(String element, String attribute)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(SettingPath);
                XmlNode setting = document.SelectSingleNode("Setting");
                XmlNode theElement = setting.SelectSingleNode(element);
                if (theElement == null)
                {
                    AddElement(element);
                }
                XmlElement theAttribute = theElement[attribute];
                if (theAttribute == null)
                {
                    AddAttribute(element, attribute);
                }
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
            }
        }

        public static String LoadSetting(String element, String attribute)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(SettingPath);
                XmlElement setting = (XmlElement)xmlDoc.SelectSingleNode("Setting");
                XmlElement selectedElement = (XmlElement)setting.SelectSingleNode(element);
                String value = selectedElement.Attributes.GetNamedItem(attribute).Value.ToString();
                return value;
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
                return "";
            }
        }

        public static void DeleteSetting()
        {
            try
            {
                if (File.Exists(SettingPath))
                {
                    File.Delete(SettingPath);
                }
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
