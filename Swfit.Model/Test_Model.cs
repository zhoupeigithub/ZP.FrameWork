using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swfit.Model
{
    //==============================================================
    //  作者：Swfit_zp
    //  时间：2019/4/11 11:07:01
    //  文件名：Test_Model
    //  版本：V1.0.1  
    //  说明：测试  
    //==============================================================
    public class Test_Model
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Grade> lgd { get; set; }
        public string Teacher { get; set; }
    }

    public class Grade
    {
        public string Yw{get;set;}
         public string Sx{get;set;}
         public string En{get;set;}
    }
}
