using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Swfit.Common.ExtFile
{
    //==============================================================
    //  作者：Swfit_zp
    //  时间：2019/4/11 13:41:57
    //  文件名：XmlHelper
    //  版本：V1.0.1  
    //  说明：xml文档帮助类
    //==============================================================
    public static class XmlHelper
    {
        /// <summary>
        /// 创建XML文件
        /// </summary>
        /// <param name=”fliepath”>文件路径</param>
        /// <param name=”RootEle”>根元素</param>
        /// <param name=”eles”>一级元素</param>
        public static void CreateXML(string fliepath, string RootEle, string[] eles)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            //创建一个根节点（一级）
            XmlElement root = doc.CreateElement(RootEle);
            doc.AppendChild(root);
            //创建节点（二级）
            for (int i = 0; i < eles.Length; i++)
            {
                XmlNode Nodel = doc.CreateElement(eles[i]);
                Nodel.InnerText = "";
                root.AppendChild(Nodel);
            }
            doc.Save(fliepath);
        }

        /// <summary>
        /// 创建XML文件
        /// </summary>
        /// <param name=”fliepath”>文件路径</param>
        /// <param name=”RootEle”>根元素</param>
        /// <param name=”eles”>一级元素</param>
        /// <param name=”elesvalue”>一级元素的值</param>
        public static void CreateXML(string fliepath, string RootEle, string[] eles, string[] elesvalue)
        {
            if (eles.Length == elesvalue.Length)
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                //创建一个根节点（一级）
                XmlElement root = doc.CreateElement(RootEle);
                doc.AppendChild(root);
                //创建节点（二级）
                for (int i = 0; i < eles.Length; i++)
                {
                    XmlNode Nodel = doc.CreateElement(eles[i]);
                    Nodel.InnerText = elesvalue[i];
                    root.AppendChild(Nodel);
                }
                doc.Save(fliepath);
            }
            else
            {

                throw new Exception("eles和elesvalue的长度不一致");
            }
        }


        /// <summary>
        /// 向XML指定元素内添加一组元素
        /// </summary>
        /// <param name=”fliepath”>文件路径</param>
        /// <param name=”elePath”>元素路径param>
        /// <param name=”eles”>元素</param>
        public static void AddXmlElementsToOneElement(string fliepath, string elePath, string[] eles)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fliepath);
            XmlElement root = doc.DocumentElement;
            XmlNodeList list = root.SelectNodes(elePath);
            for (int i = 0; i < list.Count; i++)
            {

                for (int j = 0; j < eles.Length; j++)
                {
                    XmlNode Nodel = doc.CreateElement(eles[j]);
                    Nodel.InnerText = "";
                    list[i].AppendChild(Nodel);
                }

            }
            doc.Save(fliepath);
        }

        /// <summary>
        /// 向XML指定元素内添加一组元素
        /// </summary>
        /// <param name=”fliepath”>文件路径</param>
        /// <param name=”elePath”>元素路径param>
        /// <param name=”eles”>元素</param>
        /// <param name=”elesvalue”>元素值</param>
        public static void AddXmlElementsToOneElement(string fliepath, string elePath, string[] eles, string[] elesvalue)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fliepath);
            XmlElement root = doc.DocumentElement;
            XmlNodeList list = root.SelectNodes(elePath);
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < eles.Length; j++)
                {
                    XmlNode Nodel = doc.CreateElement(eles[j]);
                    Nodel.InnerText = elesvalue[j];
                    list[i].AppendChild(Nodel);
                }

            }
            doc.Save(fliepath);
        }

        /// <summary>
        /// 从一个指定元素节点获取指定子元素的值
        /// </summary>
        /// <param name=”fliepath”>文件路径</param>
        /// <param name=”elePath”>元素路径param>
        /// <param name=”eles”>元素名称</param>
        /// <returns></returns>
        public static string[] ReadXmlElmentsFromOneElement(string fliepath, string elePath, string[] eles)
        {
            string[] redN = new string[eles.Length];
            XmlDocument doc = new XmlDocument();
            doc.Load(fliepath);
            XmlElement root = doc.DocumentElement;
            XmlNodeList list = root.SelectNodes(elePath);

            if (list.Count > 0)
            {
                for (int j = 0; j < eles.Length; j++)
                {
                    redN[j] = list[0].SelectNodes(eles[j])[0].InnerText;
                }
            }
            return redN;
        }

        /// <summary>
        /// 从一个指定的元素节点获取相同路径下指定的节点
        /// </summary>
        /// <param name=”fliepath”>文件路径</param>
        /// <param name=”elePath”>元素路径</param>
        /// <param name=”elePath2″>相同元素路径</param>
        /// <param name=”eles”>元素名称</param>
        /// <returns></returns>
        public static List<string> ReadALLXmlElmentsFromOneElement(string fliepath, string elePath, string elePath2, string[] eles)
        {
            List<string> redN = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(fliepath);
            XmlElement root = doc.DocumentElement;
            XmlNodeList list = root.SelectNodes(elePath);

            if (list.Count > 0)
            {
                XmlNodeList list1 = list[0].SelectNodes(elePath2);
                for (int i = 0; i < list1.Count; i++)
                {
                    for (int j = 0; j < eles.Length; j++)
                    {
                        string mk = list1[i].SelectNodes(eles[j])[0].InnerText;
                        redN.Add(mk);
                    }
                }
            }
            return redN;
        }

        /// <summary>
        /// 修改某一路径下的所有eles中指定的元素值为elesvalue
        /// </summary>
        /// <param name=”fliepath”>文件路径</param>
        /// <param name=”elePath”>元素路径</param>
        /// <param name=”eles”>元素名称</param>
        /// <param name=”elesvalue”>元素值</param>
        public static void ModifyXmlelments(string fliepath, string elePath, string[] eles, string[] elesvalue)
        {
            if (eles.Length == elesvalue.Length)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fliepath);
                XmlElement root = doc.DocumentElement;
                XmlNodeList list = root.SelectNodes(elePath);
                for (int i = 0; i < eles.Length; i++)
                {

                    list[0].SelectNodes(eles[i])[0].InnerText = elesvalue[i];
                }
                doc.Save(fliepath);
            }
            else
            {
                throw new Exception("eles和elesvalue的长度不一致");
            }
        }

        /// <summary>
        /// 删除指定路径下的所有指定元素
        /// </summary>
        /// <param name=”fliepath”>文件路径</param>
        /// <param name=”elePath”>元素路径</param>
        /// <param name=”eles”>元素名称</param>
        public static void DeleteXmlelemnts(string fliepath, string elePath, string[] eles)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fliepath);
            XmlElement root = doc.DocumentElement;
            XmlNodeList list = root.SelectNodes(elePath);
            for (int i = 0; i < eles.Length; i++)
            {
                XmlNode mmk = list[0].SelectNodes(eles[i])[0];
                list[0].RemoveChild(mmk);
            }
            doc.Save(fliepath);
        }
    }
}
