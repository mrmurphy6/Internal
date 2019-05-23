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

namespace Internal.SettleInvest
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

            Task.Factory.StartNew(()=> {
                while (true)
                {
                    if (StaticDayBonusSettleTime.IsEmpty())
                    {
                        //5分钟后重新获取静态日分红结算时间
                        NextGetStaticDayBonusSettleTimeAgainTimeStamp = DateHelper.GetTimeStamp_Seconds(DateTime.Now.AddMinutes(5));

                        tSysSettingEntity _temp_set = tSysSettingBLL.Instance.GetModelByKey("StaticDayBonusSettleTime");
                        if (_temp_set == null || !_temp_set.setValue.IsTimeString())
                        {
                            SetText("未获取到静态日分红结算时间或结算时间格式不正确，5分钟后重新获取");
                            continue;
                        }
                        StaticDayBonusSettleTime = string.Format("yyyy-MM-dd {0}", _temp_set.setValue);

                        //结算的收益归属当天还是昨天
                        _temp_set = tSysSettingBLL.Instance.GetModelByKey("StaticDayBonusProfitBelongToAttr");
                        StaticDayBonusSettleTodayProfit = (_temp_set == null || _temp_set.setValue.IsEmpty()) ? "1" : _temp_set.setValue;
                    }

                    DateTime _temp_settletime = DateTime.Parse(StaticDayBonusSettleTime);
                    DateTime _temp_currenttime = DateTime.Now;
                    //结算时间已到或已过，并且还未开始执行静态日分红结算任务
                    if (_temp_settletime >= _temp_currenttime && !IsDoStaticDayBonusSettleTask)
                    {
                        IsDoStaticDayBonusSettleTask = true;
                        //收益归属当日
                        if (StaticDayBonusSettleTodayProfit.Equals("1"))
                        {
                            StaticDayBonusProfitBelongToDate = _temp_currenttime.ToString("yyyy-MM-dd");
                        }
                        else//收益归属昨日
                        {
                            StaticDayBonusProfitBelongToDate = _temp_currenttime.AddDays(-1).ToString("yyyy-MM-dd");
                        }
                        SetText("静态日分红结算任务开始");
                        while (true)
                        {
                            if (IsStop)
                            {
                                SetText("服务已停止。。。。。。 Sleep一会");
                                Thread.Sleep(10000);
                                continue;
                            }

                            //true表示所有记录都已结算完成
                            if (FinishStaticDayBonusSettle(StaticDayBonusProfitBelongToDate))
                            {
                                SetText("静态日分红结算任务结束");
                                break;
                            }

                            Thread.Sleep(1000);
                        }
                    }

                    Thread.Sleep(5000);
                }
            });

            #endregion
        }

        bool FinishStaticDayBonusSettle(string settleDate)
        {
            List<tUserInvestRecordEntity> list = tUserInvestRecordBLL.Instance.GetOneMemberOfHaveUnSettledInvestRecord(settleDate);
            //没有获取到默认为所有会员的投资记录都已结算
            if (list == null || list.Count == 0)
            {
                return true;
            }

            foreach (tUserInvestRecordEntity record in list)
            {
                tUserInvestRecordBLL.Instance.Settle(record, settleDate, out string ret);
                if (ret.Equals("Out"))
                {
                    SetText(string.Format("投资记录：[{0} {1}]已出局，会员：{2}，信息：{3} \r\n", record.recordId, record.recordNo, record.mbUserName, ret));
                }
                else if (ret.Equals("Settled"))
                {
                    SetText(string.Format("投资记录：[{0} {1}]已结算，会员：{2}，信息：{3}\r\n", record.recordId, record.recordNo, record.mbUserName, ret));
                }
                else
                {
                    SetText(string.Format("投资记录：[{0} {1}]结算失败，会员：{2}，信息：{3}\r\n", record.recordId, record.recordNo, record.mbUserName, ret));
                }
            }

            return false;
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
