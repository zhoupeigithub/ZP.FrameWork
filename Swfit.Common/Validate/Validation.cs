using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace Swfit.Common.Validate
{
    //==============================================================
    //  作者：Swfit_zp
    //  时间：2019/4/11 9:29:02
    //  文件名：Validation
    //  版本：V1.0.1  
    //  说明：系统验证管理    
    //==============================================================
    public static class Validation
    {

        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //等价于^[+-]?\d+[.]?\d+$
        private static Regex RegEmail = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
        private static Regex regch = new Regex(@"^[\u4e00-\u9fa5]{0,}$");   //汉字
        private static Regex regennum = new Regex(@"^[A-Za-z0-9]+$ 或 ^[A-Za-z0-9]{4,40}$");   //英文和数字
        private static Regex reg1 = new Regex(@"^.{3,20}$"); //长度为3-20的所有字符
        private static Regex reg2 = new Regex(@"^[A-Za-z]+$");  //由26个英文字母组成的字符串
        private static Regex reg3 = new Regex(@"^[A-Z]+$");  //由26个大写英文字母组成的字符串
        private static Regex reg4 = new Regex(@"^[a-z]+$");   //由26个小写英文字母组成的字符串

        /// <summary>
        /// 检测程序是否打开运行
        /// </summary>
        /// <returns>true为运行，false为未运行</returns>
        public static bool ProgramIsRunning()
        {
            if (System.Diagnostics.Process.GetProcessesByName("Swfit.UI").ToList().Count > 0)
                return true;
            else
                return false;
        }



        /// <summary>
        /// 正则表达式匹配SN
        /// </summary>
        /// <param name="str">传入一个字符串</param>
        /// <returns>ture表示符合SN要求，false则不符合</returns>
        public static bool IsDigitOrNumber(string str)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(str.ToUpper(), @"(?i)^[0-9A-Z]+$") && str.ToString().Trim().Length == 17)
                return true;
            else
                return false;
        }


        /// <summary>
        /// IP有效性
        /// </summary>
        /// <param name="ip">传入IP地址</param>
        /// <returns></returns>
        public static bool IsValidIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string stringValue)
        {
            return Encoding.Default.GetBytes(stringValue).Length;
        }


        /// <summary>
        /// 判断输入的字符是否为日期
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static bool IsDate(string strValue)
        {
            return Regex.IsMatch(strValue, @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))");
        }

        /// <summary>
        /// 判断输入的字符是否为日期,如2004-07-12 14:25|||1900-01-01 00:00|||9999-12-31 23:59
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static bool IsDateHourMinute(string strValue)
        {
            return Regex.IsMatch(strValue, @"^(19[0-9]{2}|[2-9][0-9]{3})-((0(1|3|5|7|8)|10|12)-(0[1-9]|1[0-9]|2[0-9]|3[0-1])|(0(4|6|9)|11)-(0[1-9]|1[0-9]|2[0-9]|30)|(02)-(0[1-9]|1[0-9]|2[0-9]))\x20(0[0-9]|1[0-9]|2[0-3])(:[0-5][0-9]){1}$");
        }


        /// <summary>
        /// 检测用户名格式是否有效
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsValidUserName(string userName)
        {
            int userNameLength = GetStringLength(userName);
            if (userNameLength >= 4 && userNameLength <= 20 && Regex.IsMatch(userName, @"^([\u4e00-\u9fa5A-Za-z_0-9]{0,})$"))
            {   // 判断用户名的长度（4-20个字符）及内容（只能是汉字、字母、下划线、数字）是否合法
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 密码有效性
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^[A-Za-z_0-9]{6,16}$");
        }

        /// <summary>
        /// int有效性
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        static public bool IsValidInt(string val)
        {
            return Regex.IsMatch(val, @"^[1-9]\d*\.?[0]*$");
        }

        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            Match m = RegNumber.Match(inputData);
            return m.Success;
        }
    }
}
