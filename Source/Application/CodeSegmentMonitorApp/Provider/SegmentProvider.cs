#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/5/2 14:33:27 
 * 文件名：SegmentProvider 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using CodeSegmentMonitorApp.ViewModel;
using HeBianGu.Base.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace CodeSegmentMonitorApp.Provider
{
    class SegmentProvider
    {
        public static SegmentProvider Instance = new SegmentProvider();

        public SegmentItemViewModel Create(string path)
        {

            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            SegmentItemViewModel segment = this.DeSerialize(xml);

            segment.FileName = Path.GetFileNameWithoutExtension(path);

            segment.FilePath = path;

            return segment;
        }


        public void Save(SegmentItemViewModel model, string path)
        {
            string formatStr = Properties.Resources.SegmentTemplate;

            string text = string.Format(formatStr, model.Title, model.Shortcut, model.Description, model.Author, model.SnippetTypes, model.Snippet);

            File.WriteAllText(path, text);
        }


        SegmentItemViewModel DeSerialize(XmlDocument xml)
        {
            SegmentItemViewModel model = new SegmentItemViewModel();

            var collection = model.GetType().GetProperties();

            foreach (var item in collection)
            {
                XmlNodeList nodelist = xml.GetElementsByTagName(item.Name);

                if (nodelist.Count > 0)
                {
                    if (item.CanWrite)
                    {
                        item.SetValue(model, nodelist[0].InnerText);
                    }
                }
            }

            var snippets = xml.GetElementsByTagName("Snippet");

            if (snippets == null || snippets.Count == 0)
            {
                return model;
            }

            model.Snippet = xml.GetElementsByTagName("Snippet")[0].OuterXml;

            model.Snippet = FormatXml(model.Snippet);

            return model;
        }



        [Obsolete("不用了")]
        XmlDocument Serialize(SegmentItemViewModel model)
        {
            XmlDocument xml = new XmlDocument();

            XmlNode codesnippet = xml.CreateElement("CodeSnippet");

            xml.AppendChild(codesnippet);

            #region - header-

            XmlNode header = xml.CreateElement("Header");

            codesnippet.AppendChild(header);

            XmlNode title = xml.CreateElement("Title");
            header.AppendChild(title);

            XmlNode shortcut = xml.CreateElement("Shortcut");

            header.AppendChild(shortcut);

            XmlNode description = xml.CreateElement("Description");
            header.AppendChild(description);

            XmlNode author = xml.CreateElement("Author");

            header.AppendChild(author);

            XmlNode snippettypes = xml.CreateElement("SnippetTypes");

            header.AppendChild(snippettypes);

            XmlNode snippettype = xml.CreateElement("SnippetType");

            snippettypes.AppendChild(snippettype);

            #endregion

            #region - snippet -

            XmlNode snippet = xml.CreateElement("Snippet");

            codesnippet.AppendChild(snippet);

            #endregion

            var collection = model.GetType().GetProperties();

            foreach (var item in collection)
            {
                XmlNodeList nodelist = xml.GetElementsByTagName(item.Name);

                if (nodelist.Count > 0)
                {
                    if (item.CanWrite)
                    {
                        nodelist[0].InnerText = item.GetValue(model).ToString();
                    }
                }
            }


            return xml;
        }

        public string FormatXml(string sUnformattedXml, bool clearTitle = false)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(sUnformattedXml);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xtw.Indentation = 1;
                xtw.IndentChar = '\t';
                xd.WriteTo(xtw);
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
            }

            return sb.ToString();
        }


        /// <summary> 取出默认配置 </summary>
        public string GetDefaultPath()
        {
            return File.ReadAllText(GetPath);
        }

        string GetPath
        {
            get
            {
                string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string name = Assembly.GetEntryAssembly().GetName().Name;

                string path = Path.Combine(docFolder, name);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string configerPath = Path.Combine(path, "Config.ini");

                if (!File.Exists(configerPath))
                {
                    File.WriteAllText(configerPath, @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\VC#\Snippets\2052\Visual C#");
                }

                return configerPath;
            }
          
        }

        /// <summary> 写入配置路径 </summary>
        public void SetDefaultPath(string value)
        {
            File.WriteAllText(GetPath,value);
        }

    }
}
