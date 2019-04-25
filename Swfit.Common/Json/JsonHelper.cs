using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Swfit.Common.Json
{
    //==============================================================
    //  作者：Swfit_zp
    //  时间：2019/4/11 10:46:50
    //  文件名：JsonHelper
    //  版本：V1.0.1  
    //  说明：Json数据请求类
    //==============================================================
    public static class JsonHelper
    {
        /// <summary>
        /// Post 带有参数请求Json的方法
        /// </summary>
        /// <param name="PARAMETER">请求的参数</param>
        /// <param name="POSTURL">请求的URL地址</param>
        /// <returns>返回json字符串</returns>
        public static JObject RequestJsonByUrlPost(string PARAMETER, string POSTURL)
        {
            JObject jo = new JObject();
            try
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(PARAMETER);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(POSTURL);
                myRequest.Method = "Post";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = data.Length;
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string sJson = reader.ReadToEnd();
                jo = JObject.Parse(sJson);
            }
            catch (Exception ex)
            {
                throw;
            }
            return jo;
        }

        /// <summary>
        /// Get 请求的方法
        /// </summary>
        /// <param name="geturl">请求的URL地址</param>
        /// <returns>返回json字符串</returns>
        public static JObject RequestJsonByUrlGet(string geturl)
        {
            JObject jo = new JObject();
            try
            {
                HttpWebRequest http = WebRequest.Create(geturl) as HttpWebRequest;
                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string sJson = reader.ReadToEnd();
                http.Abort();
                ArrayList ObjList = new ArrayList();
                //将字符串转换成Json对象
                jo = JObject.Parse(sJson);
            }
            catch (Exception ex)
            {
                throw;
            }
            return jo;
        }

        
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = JsonConvert.SerializeObject(o);
            return json;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }
    }
}
