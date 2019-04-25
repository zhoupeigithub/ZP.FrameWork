using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace Swfit.Common.Log4net
{
    //==============================================================
    //  作者：Swfit_zp
    //  时间：2019/4/11 15:54:10
    //  文件名：Applogs
    //  版本：V1.0.1  
    //  说明：日志管理器
    //==============================================================
    public sealed class Applogs
    {
         /// <summary>
        /// 记录消息Queue
        /// </summary>
        private readonly ConcurrentQueue<LogsMessage> _que;

        /// <summary>
        /// 信号
        /// </summary>
        private readonly ManualResetEvent _mre;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog _log;
        /// <summary>
        /// 日志
        /// </summary>
        private static Applogs _flashLog = new Applogs();
        private Applogs()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "");
            var configFile = new FileInfo(Path.Combine(path, "log4net.config"));
            if (!configFile.Exists)
            {
                throw new Exception("未配置log4net配置文件！");
            }
            // 设置日志配置文件路径
            XmlConfigurator.Configure(configFile);
            _que = new ConcurrentQueue<LogsMessage>();
            _mre = new ManualResetEvent(false);
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        }

        /// <summary>
        /// 实现单例
        /// </summary>
        /// <returns></returns>
        public static Applogs Instance()
        {
            return _flashLog;
        }

        /// <summary>
        /// 另一个线程记录日志，只在程序初始化时调用一次
        /// </summary>
        public void Register()
        {
            Thread t = new Thread(new ThreadStart(WriteLog));
            t.IsBackground = false;
            t.Start();
        }

        /// <summary>
        /// 从队列中写日志至磁盘
        /// </summary>
        private void WriteLog()
        {
            while (true)
            {
                // 等待信号通知
                _mre.WaitOne();

                LogsMessage msg;
                // 判断是否有内容,从列队中获取内容，并删除列队中的内容
                while (_que.Count > 0 && _que.TryDequeue(out msg))
                {
                    // 判断日志等级，然后写日志
                    switch (msg.Level)
                    {
                        case LogsLevel.Debug:
                            _log.Debug(msg.Message, msg.Exception);
                            break;
                        case LogsLevel.Info:
                            _log.Info(msg.Message, msg.Exception);
                            break;
                        case LogsLevel.Error:
                            _log.Error(msg.Message, msg.Exception);
                            break;
                        case LogsLevel.Warn:
                            _log.Warn(msg.Message, msg.Exception);
                            break;
                        case LogsLevel.Fatal:
                            _log.Fatal(msg.Message, msg.Exception);
                            break;
                      
                    }
                }

                // 重新设置信号
                _mre.Reset();
                Thread.Sleep(3);
            }
        }


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message">日志文本</param>
        /// <param name="level">等级</param>
        /// <param name="ex">Exception</param>
        public void EnqueueMessage(string message, LogsLevel level, Exception ex = null)
        {
            if (
                (level == LogsLevel.Debug && _log.IsDebugEnabled)
                 || (level == LogsLevel.Error && _log.IsErrorEnabled)
                 || (level == LogsLevel.Fatal && _log.IsFatalEnabled)
                 || (level == LogsLevel.Info && _log.IsInfoEnabled)
                 || (level == LogsLevel.Warn && _log.IsWarnEnabled)
                )
            {
                _que.Enqueue(new LogsMessage
                {
                    Message = message,
                    Level = level,
                    Exception = ex
                });
                // 通知线程往磁盘中写日志
                _mre.Set();
            }
        }

        public static void Debug(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, LogsLevel.Debug, ex);
        }

        public static void Error(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, LogsLevel.Error, ex);
        }

        public static void Fatal(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, LogsLevel.Fatal, ex);
        }

        public static void Info(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, LogsLevel.Info, ex);
        }

        public static void Warn(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, LogsLevel.Warn, ex);
        }
    }

    public static class SyncLevel
    {
        public static readonly log4net.Core.Level SYNCLevel = new log4net.Core.Level(50000, "SYNC");

        public static void Sync(this ILog log, string message)
        {
            log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                SYNCLevel, message, null);
        }
        public static void Sync(this ILog log, string message, Exception ex = null)
        {
            log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                SYNCLevel, message, null);
        }
        public static void SyncFormat(this ILog log, string message, params object[] args)
        {
            string formattedMessage = string.Format(message, args);
            log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                SYNCLevel, formattedMessage, null);
        }
    }
}
