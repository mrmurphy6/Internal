using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internal.Api
{
    public class UserToken
    {
        public int Id { get; set; }

        public long time { get; set; }
    }

    public class ApiParamter
    {
        public string token { get; set; }

        public UserToken user { get; set; }

        public string clientType { get; set; }
    }



    //获取短信验证码接口参数类
    public class VCodeParamter : ApiParamter
    {
        public string mobile { get; set; }

        public int func { get; set; }
    }

    //分红理财首页数据接口参数
    public class BonusShareHomePageParamter : ApiParamter
    {
    }

    //公告列表接口参数
    public class NoticesParamter : ApiParamter
    {
        public int page { get; set; }

        public int size { get; set; }
    }

    //修改二级密码接口参数
    public class ModifyTowPwdParamter : ApiParamter
    {
        public string mobile { get; set; }

        public string vcode { get; set; }

        public string pwd { get; set; }

        public string surepwd { get; set; }
    }

    //添加银行卡
    public class AddBankParamter : ApiParamter
    {
        public string name { get; set; }

        public string code { get; set; }

        public string realname { get; set; }

        public string addr { get; set; }
    }

    //收益记录接口参数
    public class ProfitRecordParamter : ApiParamter
    {
        public int page { get; set; }

        public int size { get; set; }

        /// <summary>
        /// 不传或传其他未知值默认为1，1日分红奖收益记录 2分红代数奖收益记录 3直推奖收益记录 4代理领导奖收益记录
        /// </summary>
        //public int cate { get; set; }

        public string sdate { get; set; }

        public string edate { get; set; }
    }

    //团队图谱接口参数
    public class TeamParamter : ApiParamter
    {
        public int retLevel { get; set; }
    }

    //激活币互转接口参数
    public class ActiveWalletTransParamter : ApiParamter
    {
        public int toMbId { get; set; }

        public int amount { get; set; }

        public string pwd { get; set; }
    }

    //现金转彩金接口参数
    public class CashToGoldTransParamter : ApiParamter
    {
        public int amount { get; set; }

        public string pwd { get; set; }
    }

    //各钱包账务明细接口参数
    public class WalletRecordParamter : ApiParamter
    {
        /// <summary>
        /// 钱包类型 1激活币钱包 2现金钱包 3彩金钱包 4基金积分钱包 不传默认为1
        /// </summary>
        public int wallet { get; set; }

        public int page { get; set; }

        public int size { get; set; }

        public string sdate { get; set; }

        public string edate { get; set; }
    }

    //充值接口参数
    public class RechargeParamter : ApiParamter
    {
        /// <summary>
        /// 充值人民币金额
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 转款银行Id
        /// </summary>
        public int bankId { get; set; }

        /// <summary>
        /// 二级密码
        /// </summary>
        public string pwd { get; set; }

        /// <summary>
        /// 充值记录
        /// </summary>
        public string reguId { get; set; }

        /// <summary>
        /// 转款凭证路径
        /// </summary>
        public string img { get; set; }
    }

    //充值记录接口参数
    public class RechargeRecordParamter : ApiParamter
    {
        public int page { get; set; }

        public int size { get; set; }

        public string sdate { get; set; }

        public string edate { get; set; }
    }

    //提现接口参数
    public class WithdrawParamter : ApiParamter
    {
        /// <summary>
        /// 提现金额（美元）
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 收款银行Id
        /// </summary>
        public int bankId { get; set; }

        /// <summary>
        /// 二级密码
        /// </summary>
        public string pwd { get; set; }
    }

    //提现记录接口参数
    public class WithdrawRecordParamter : ApiParamter
    {
        public int page { get; set; }

        public int size { get; set; }

        public string sdate { get; set; }

        public string edate { get; set; }
    }

    //投资理财接口参数
    public class InvestParamter : ApiParamter
    {
        /// <summary>
        /// 投资金额 美金
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 二级密码
        /// </summary>
        public string pwd { get; set; }
    }

    //投资理财记录接口参数
    public class InvestRecordParamter : ApiParamter
    {
        public int page { get; set; }

        public int size { get; set; }

        public string sdate { get; set; }

        public string edate { get; set; }
    }

    //购买基金接口参数
    public class FundsParamter : ApiParamter
    {
        public int fundId { get; set; }

        public int periodId { get; set; }

        /// <summary>
        /// 金额 美金
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 二级密码
        /// </summary>
        public string pwd { get; set; }
    }

    //购买基金记录接口参数
    public class FundsRecordParamter : ApiParamter
    {
        public int page { get; set; }

        public int size { get; set; }

        public string sdate { get; set; }

        public string edate { get; set; }
    }

    //购买股权接口参数
    public class StockParamter : ApiParamter
    {
        /// <summary>
        /// 份额
        /// </summary>
        public decimal shares { get; set; }

        /// <summary>
        /// 二级密码
        /// </summary>
        public string pwd { get; set; }
    }

    //购买基金记录接口参数
    public class StockRecordParamter : ApiParamter
    {
        public int page { get; set; }

        public int size { get; set; }

        public string sdate { get; set; }

        public string edate { get; set; }
    }

    //与客服聊天接口参数
    public class TalkParamter : ApiParamter
    {
        /// <summary>
        /// 消息接收人
        /// </summary>
        public int toMbId { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string content { get; set; }
    }

    //购买基金记录接口参数
    public class TalkRecordParamter : ApiParamter
    {
        /// <summary>
        /// 消息接收人
        /// </summary>
        public int toMbId { get; set; }

        public int page { get; set; }

        public int size { get; set; }

        public string sdate { get; set; }

        public string edate { get; set; }
    }
}