﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Internal.SettleFund
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();

            #region 方法一:使用互斥量

            // createdNew:  
            // 在此方法返回时，如果创建了局部互斥体（即，如果 name 为 null 或空字符串）或指定的命名系统互斥体，则包含布尔值 true；  
            // 如果指定的命名系统互斥体已存在，则为false  
            using (Mutex mutex = new Mutex(true, WebConfigHelper.AppName, out bool createNew))
            {
                if (createNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
                // 程序已经运行的情况，则弹出消息提示并终止此次运行  
                else
                {
                    //Logger.LogInfo("",string.Format("应用程序已经在运行中..."));
                    MessageBox.Show("应用程序已经在运行中...");
                    System.Threading.Thread.Sleep(1000);
                    // 终止此进程并为基础操作系统提供指定的退出代码。  
                    System.Environment.Exit(1);
                }
            }

            #endregion
        }
    }
}
