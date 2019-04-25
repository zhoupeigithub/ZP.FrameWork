using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swfit.Common.DataManager
{
    //==============================================================
    //  作者：Swfit_zp
    //  时间：2019/4/11 16:01:16
    //  文件名：CommonDataManager
    //  版本：V1.0.1  
    //  说明：公共数据管理器
    //==============================================================
    public  sealed class CommonDataManager
    {
        private static readonly CommonDataManager instance = new CommonDataManager();
        CommonDataManager()
        {

        }
        public static CommonDataManager Instance
        {
            get { return instance; }
        }
    }
}
