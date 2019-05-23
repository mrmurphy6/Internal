using Common;
using Internal.BLL;
using Internal.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Internal.SettleFund
{
    public partial class Form1 : Form
    {
        static bool IsStop = true;

        //下次重新获取静态日分红结算时间的时间点
        static long NextGetStaticDayBonusSettleTimeAgainTimeStamp = 0;
        //是否已开始执行静态日分红结算任务
        static bool IsDoStaticDayBonusSettleTask = false;
        //静态日分红结算时间
        static string StaticDayBonusSettleTime = "";
        //结算当日收益 or 结算昨日收益
        static string StaticDayBonusSettleTodayProfit = "1";
        //收益归属日期
        static string StaticDayBonusProfitBelongToDate = "";

        public Form1()
        {
            InitializeComponent();
            this.Text = WebConfigHelper.WindowTitle;
        }

        //停止
        private void btnStop_Click(object sender, EventArgs e)
        {
            IsStop = true;
            SetbtnStartEnabled(btnStop, false);
            SetbtnStartEnabled(btnStart, true);
            SetText("正在停止服务。。。。。。\r\n");
        }

        //开始
        private void btnStart_Click(object sender, EventArgs e)
        {
            IsStop = false;
            SetbtnStartEnabled(btnStop, true);
            SetbtnStartEnabled(btnStart, false);
        }

        //FromLoad
        private void Form1_Load(object sender, EventArgs e)
        {
            Logger.LogInfo("", string.Format("{0}程序启动，开始执行任务", WebConfigHelper.WindowTitle));
            SetText(string.Format("{0}程序启动，开始执行任务", WebConfigHelper.WindowTitle));

            btnStart_Click(sender, e);

            #region 起一个线程定时执行结算任务

            Task.Factory.StartNew(() => {
                SetText("基金结算任务开始");
                while (true)
                {
                    if (IsStop)
                    {
                        SetText("服务已停止。。。。。。 Sleep一会");
                        Thread.Sleep(10000);
                        continue;
                    }

                    ToDoWork();

                    Thread.Sleep(1000);
                }
            });

            #endregion
        }

        void ToDoWork()
        {
            tUserBuyFundRecordEntity entity = tUserBuyFundRecordBLL.Instance.GetOneExpiredFundRecord();
            if (tUserBuyFundRecordBLL.Instance.Settle(entity, out string ret))
            {
                SetText(string.Format("基金购买记录：[{0}]结算失败，会员：{1}，信息：{2}\r\n", entity.recordId, entity.mbUserName, ret));
            }
        }

        delegate void labDelegate(string str);
        private void SetText(string str)
        {
            if (txt_info.InvokeRequired)
            {
                try
                {
                    Invoke(new labDelegate(SetText), new string[] { str });
                }
                catch { }
            }
            else
            {
                txt_info.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + str + "\r\n" + txt_info.Text + "\r\n";
            }
        }

        delegate void btnStartDelegate(Control c, bool enabled);
        private void SetbtnStartEnabled(Control c, bool enabled)
        {
            if (c.InvokeRequired)
            {
                try
                {
                    Invoke(new btnStartDelegate(SetbtnStartEnabled), new object[] { c, enabled });
                }
                catch { }
            }
            else
            {
                c.Enabled = enabled;
            }
        }
    }
}
