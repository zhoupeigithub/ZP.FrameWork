using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swfit.Common.Log4net
{
    //==============================================================
    //  作者：Swfit_zp
    //  时间：2019/4/11 15:56:53
    //  文件名：LogsMessage
    //  版本：V1.0.1  
    //  说明：日志信息类      
    //==============================================================
    public class LogsMessage
    {
        public string Message { get; set; }
        public LogsLevel Level { get; set; }
        public Exception Exception { get; set; }
    }
}
