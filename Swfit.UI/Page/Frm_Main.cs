using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Swfit.Model;
using Swfit.Common;
using Swfit.Common.Json;
using Swfit.Common.Log4net;
using Rabbit.InvokeHelper;
using System.Threading.Tasks;
using System.Threading;

namespace Swfit.UI
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Model.Test_Model> ltm = new List<Test_Model>();
            Model.Test_Model tm = new Test_Model();
            tm.lgd = new List<Grade>();

            for (int i = 0; i < 10; i++)
            {
                tm.Id = "Id" + i;
                tm.lgd.Add(new Grade() 
                { 
                    En = "en" + i,
                    Sx = "sx" + i, 
                    Yw = "yw" + i 
                });
                tm.Name = "ws" + i;
                tm.Teacher = "tsi" + i;
                ltm.Add(tm);
            }
            string json = Swfit.Common.Json.JsonHelper.SerializeObject(ltm);
            List<Model.Test_Model> R = JsonHelper.DeserializeJsonToObject<List<Test_Model>>(json);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Applogs.Error("程序错误");
            Applogs.Fatal("程序错误_系统奔溃");
            Applogs.Debug("调试异常，已排查");
            Applogs.Info("PDCA上传完成");
        }

        private  void button3_Click(object sender, EventArgs e)
        {
            //1
            Task.Factory.StartNew(() => {
                GetValue();
                BackgroundWorker bwk = new BackgroundWorker();
                bwk.RunWorkerAsync();
            });
            //2
            object k =1;
            ThreadPool.QueueUserWorkItem(GetData, k);
            ThreadPool.QueueUserWorkItem(DownLoadFile_My);

            //3
            Thread t = new Thread(DownLoadFile_My);
            t.Start();

            //4
            Action<string> strs = str;
            Func<string, string> RetBook = new Func<string, string>(FuncBook);
            Console.WriteLine(RetBook("aaa"));

            Func<int, int, int> gets = new Func<int, int, int>(getsum);
            gets(1, 2);
            Action<string> strd = new Action<string>(str);
            strd("1");
        }

        public void str(string str1)
        {

        }
        public static string FuncBook(string BookName)
        {
            return BookName;
        }

        public static int getsum(int i, int j)
        {
            return i + j;
        }
      
        void GetValue()
        {
            Thread.Sleep(2000);
            //InvokeHelper.Invoke(this, "GetData", t);//演示invoke方法
            InvokeHelper.Set(textBox1, "Text", "兔");//演示set方法
            //object temp = InvokeHelper.Get(label1, "Tag"); //演示Get方法
            //InvokeHelper.Set(label2, "Text", temp);
        }



        void DownLoadFile_My(object state)
        {
            Console.WriteLine("开始下载...    线程ID：" + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            Console.WriteLine("下载完成！");
        }
        public void GetData(object k)
        { 
            textBox1.Text = k.ToString();
        }
 
        private void button8_Click(object sender, EventArgs e)
        {

        }   
    }
}
