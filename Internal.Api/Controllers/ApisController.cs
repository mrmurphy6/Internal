using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Common;
using Internal.Api.App_Start;
using Internal.BLL;
using Internal.Entity;

namespace Internal.Api.Controllers
{
    [AllowCrossSiteJson]
    public class ApisController : Controller
    {
        //获取短信验证码
        public ActionResult GetVCode()
        {
            VCodeParamter param = GetRequestParamter<VCodeParamter>();

            //ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            //if (isret)
            //    return ar;

            if (param.mobile.IsEmpty() || !param.mobile.IsValidTel())
                return RetJsonResult(ApiResponseCodeEnum.ParamterFormatError, "手机号格式不正确");

            if (param.func <= 0)
                return RetJsonResult(ApiResponseCodeEnum.ParamterFormatError, "验证码功能code不正确");

            tMobileMsgEntity _msg = tMobileMsgBLL.Instance.GetLastUnUseMsgByMobile(param.mobile);
            if (_msg != null)
            {
                if ((DateTime.Now - _msg.sendTime).TotalSeconds < 60)
                {
                    return RetJsonResult(ApiResponseCodeEnum.ParamterFormatError, "操作太频繁，请稍后再试");
                }
            }

            string rannum = "123456"; //RandHelp.GetRnd(6);
            _msg = new tMobileMsgEntity()
            {
                mobileNumber = param.mobile,
                msgContent = string.Format("您的验证码是{0}，5分钟内有效。如非本人操作，请忽略本短信", rannum),
                validCode = rannum,
                sendTime = DateTime.Now,
                expireTime = DateTime.Now.AddMinutes(5),
                msgpfResponse = string.Empty,
                msgpfStatusCode = string.Empty,
                useStatus = YesNoEnum.No.GetHashCode(),
                msgFuncId = param.func
            };

            //SMSHelper sms = new SMSHelper
            //{
            //    ToTel = param.mobile,
            //    Info = _msg.msgContent
            //};

            //string error = string.Empty, msgId = string.Empty, statuscode = string.Empty;
            //sms.Send(ref error, ref msgId, ref statuscode);

            //_msg.msgpfResponse = error;
            //_msg.msgpfStatusCode = statuscode;
            //_msg.msgpfmsgId = msgId;

            //if (!statuscode.Equals("0"))
            //{
            //    return RetJsonResult(ApiResponseCodeEnum.ParamterFormatError, "短信发送失败");
            //}

            if (GetOneInstance<tMobileMsgBLL>.Instance.SubmitForm(_msg, _msg.msgId))
            {
                return RetJsonResult(ApiResponseCodeEnum.Success, "", rannum);
            }
            return RetJsonResult(ApiResponseCodeEnum.Fail, "获取失败");
        }


        #region 会员信息

        //会员个人信息
        public ActionResult BaseInfo()
        {
            ApiParamter param = GetRequestParamter<ApiParamter>();

            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            return RetJsonResult(ApiResponseCodeEnum.Success, "", new
            {
                userNo = member.mbUserNo,
                realName = member.mbRealName,
                inviteNo = member.mbInviteUserNo,
                mobile = member.mbMobileNum,
                leadLevel = ((MemberLeadLevelEnum)member.mbLeadLevel.Value).GetDescription(),
                inviteUrl = string.Format("{0}?userno={1}", WebConfigHelper.WebDomain, member.mbUserNo),
                qrImg = string.Format("{0}{1}", WebConfigHelper.FileDomain, member.mbInviteQrImg)
            });
        }

        //修改密码
        public ActionResult MPwd()
        {
            ModifyTowPwdParamter param = GetRequestParamter<ModifyTowPwdParamter>();

            //ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            //if (isret)
            //    return ar;

            tMobileMsgEntity _msg = tMobileMsgBLL.Instance.GetLastUnUseMsgByMobile(param.mobile, MobileMsgFuncEnum.FindPwd.GetHashCode());
            if (_msg == null || !param.vcode.Equals(_msg.validCode))
                return RetJsonResult(ApiResponseCodeEnum.Fail, "验证码不正确");

            if (param.pwd.Length < 8)
                return RetJsonResult(ApiResponseCodeEnum.Fail, "密码不能少于8位");
            if (!param.pwd.IsValidPwd())
                return RetJsonResult(ApiResponseCodeEnum.Fail, "密码格式不正确");
            if (!param.pwd.Equals(param.surepwd))
                return RetJsonResult(ApiResponseCodeEnum.Fail, "密码与确认密码不一致");

            tMembersEntity _member = tMembersBLL.Instance.GetModelByMobile(param.mobile);
            if (_member == null)
                return RetJsonResult(ApiResponseCodeEnum.Fail, "手机号未注册");
            int _temp_userId = _member.mbId;
            _member = new tMembersEntity()
            {
                mbId = _temp_userId,
                mbPwd = MD5Helper.PwdEncryption(param.pwd)
            };
            if (tMembersBLL.Instance.SubmitForm(_member, _member.mbId))
                return RetJsonResult(ApiResponseCodeEnum.Success, "修改成功");
            return RetJsonResult(ApiResponseCodeEnum.Fail, "修改失败");
        }


        //银行卡列表
        public ActionResult Banks()
        {
            ApiParamter param = GetRequestParamter<ApiParamter>();
            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            var where = LinqExtensions.True<tBanksEntity>();
            where = where.And(b => b.mbId.Equals(param.user.Id));
            where = where.And(b => b.bankState.Equals(1));
            List<tBanksEntity> list = tBanksBLL.Instance.GetList(where);
            return RetJsonResult(ApiResponseCodeEnum.Success, "", list.Select(r => new
            {
                id = r.bankId,
                name = r.bankName,
                code = r.bankCode,
                realname = r.accountName,
                addr = r.bankAddr
            }));
        }

        #endregion

        #region 收益记录

        public ActionResult DayBonusRecord()
        {
            ProfitRecordParamter param = GetRequestParamter<ProfitRecordParamter>();

            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            Pagination pg = new Pagination
            {
                page = param.page,
                rows = param.size,
                sidx = "settleTime",
                sord = "desc"
            };
            pg.AddWhere("mbId", member.mbId);
            if (param.sdate.IsDateString())
            {
                pg.AddWhere("settleTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 00:00:00", param.sdate)), SqlDbType.BigInt, CompareSymbolEnum.MoreThanEqual);
            }
            if (param.edate.IsDateString())
            {
                pg.AddWhere("settleTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 23:59:59", param.edate)), SqlDbType.BigInt, CompareSymbolEnum.LessThanEqual);
            }
            List<tUserDayBonusRecordEntity> list = tUserDayBonusRecordBLL.Instance.GetList(pg);
            return RetJsonResult(ApiResponseCodeEnum.Success, "",
                new
                {
                    list = list.Select(r => new
                    {
                        r.recordId,
                        date = r.settleDate.ToString("yyyy-MM-dd"),
                        time = DateHelper.IntToDateTime(r.settleTime).ToString("yyyy-MM-dd HH:mm:ss"),
                        ratio = string.Format("{0}%", r.settleRatio.ToDecimal() / 10000),
                        amount = r.settleAmount.ToDecimal() / 10000,
                        desc = "每日分红",
                        detail = new
                        {
                            no = r.investNo,
                            amount = r.investAmount,
                            time = DateHelper.IntToDateTime(r.investTime).ToString("yyyy-MM-dd HH:mm:ss"),
                            level = r.mbpkLevelName,
                            state = ((InvestStopStateEnum)r.stopState).GetDescription(),
                            stime = DateHelper.IntToDateTime(r.stopTime).ToString("yyyy-MM-dd HH:mm:ss")
                        }
                    }),
                    total = pg.records,
                    pages = pg.total
                });
        }


        //直推奖收益记录
        public ActionResult IvBonusRecord()
        {
            ProfitRecordParamter param = GetRequestParamter<ProfitRecordParamter>();

            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            Pagination pg = new Pagination
            {
                page = param.page,
                rows = param.size,
                sidx = "bonusTime",
                sord = "desc"
            };
            pg.AddWhere("mbId", member.mbId);
            if (param.sdate.IsDateString())
            {
                pg.AddWhere("bonusTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 00:00:00", param.sdate)), SqlDbType.BigInt, CompareSymbolEnum.MoreThanEqual);
            }
            if (param.edate.IsDateString())
            {
                pg.AddWhere("bonusTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 23:59:59", param.edate)), SqlDbType.BigInt, CompareSymbolEnum.LessThanEqual);
            }
            List<tUserInviteBonusRecordEntity> list = tUserInviteBonusRecordBLL.Instance.GetList(pg);
            return RetJsonResult(ApiResponseCodeEnum.Success, "",new
            {
                list = list.Select(r => new
                {
                    r.recordId,
                    time = DateHelper.IntToDateTime(r.bonusTime).ToString("yyyy-MM-dd HH:mm:ss"),
                    ratio = string.Format("{0}%", r.bonusRatio.ToDecimal() / 10000),
                    amount = r.bonusAmount.ToDecimal() / 10000,
                    desc = string.Format("直推奖({0})", r.inviteUserNo),
                    detail = new
                    {
                        no = r.investNo,
                        userNo = r.inviteUserNo,
                        amount = r.investAmount,
                        time = DateHelper.IntToDateTime(r.investTime).ToString("yyyy-MM-dd HH:mm:ss"),
                        level = r.mbpkLevelName,
                        state = ((InvestStopStateEnum)r.stopState).GetDescription(),
                        stime = DateHelper.IntToDateTime(r.stopTime).ToString("yyyy-MM-dd HH:mm:ss")
                    }
                }),
                total = pg.records,
                pages = pg.total
            });
        }

        #endregion

        //团队图谱
        public ActionResult Team()
        {
            TeamParamter param = GetRequestParamter<TeamParamter>();

            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            List<tMemberTeamInvestProfitEntity> list = tMembersBLL.Instance.GetMemberTeamInvestProfitList(member.mbId, 0, param.retLevel);
            return RetJsonResult(ApiResponseCodeEnum.Success, "", list.Select(r => new
            {
                r.mbId,
                r.mbUserNo,
                r.mbRealName,
                r.inviteCount,
                r.investAmount,
                r.teamAmount,
                r.dayAmount,
                r.teamCount
            }));
        }

        //钱包余额 投资总额 总充值 总提现 收益总额
        public ActionResult Wallet()
        {
            BonusShareHomePageParamter param = GetRequestParamter<BonusShareHomePageParamter>();

            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            return RetJsonResult(ApiResponseCodeEnum.Success, "", new
            {
                activeB = member.mbActivateWalletBalance.ToDecimal() / 10000,
                cashB = member.mbCashWalletBalance.ToDecimal() / 10000,
                gCashB = member.mbGoldCashWalletBalance.ToDecimal() / 10000,
                fundB = member.mbFundWalletBalance.ToDecimal() / 10000,
                invest = member.mbInvestAmount.ToDecimal() / 10000,
                recharge = member.mbRechargeAmount.ToDecimal()/10000,
                withdraw = member.mbWithdrawAmount.ToDecimal() / 10000,
                sProfit = member.mbStaticProfitAmount.ToDecimal() / 10000,
                profit = member.mbProfitAmount.ToDecimal() / 10000
            });
        }


        //各钱包账务明细
        public ActionResult WalletRecord()
        {
            WalletRecordParamter param = GetRequestParamter<WalletRecordParamter>();

            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            Pagination pg = new Pagination
            {
                page = param.page,
                rows = param.size,
                sidx = "happendTime",
                sord = "desc"
            };
            pg.AddWhere("wallet", param.wallet <= 0 ? 1 : param.wallet);
            pg.AddWhere("mbId", member.mbId);
            if (param.sdate.IsDateString())
            {
                pg.AddWhere("happendTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 00:00:00", param.sdate)), SqlDbType.BigInt, CompareSymbolEnum.MoreThanEqual);
            }
            if (param.edate.IsDateString())
            {
                pg.AddWhere("happendTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 23:59:59", param.edate)), SqlDbType.BigInt, CompareSymbolEnum.LessThanEqual);
            }
            List<tUserAccountRecordEntity> list = tUserAccountRecordBLL.Instance.GetList(pg);
            return RetJsonResult(ApiResponseCodeEnum.Success, "", new
            {
                list = list.Select(r => new
                {
                    r.recordId,
                    time = DateHelper.IntToDateTime(r.happendTime).ToString("yyyy-MM-dd HH:mm:ss"),
                    amount = r.realAmount.ToDecimal() / 10000,
                    desc = r.operaDesc,
                    detail = new
                    {
                        userNo = r.mbUserNo,
                        userName = r.mbUserName,
                        realName = r.mbRealName,
                        direction = ((AccountDirectionEnum)r.direction).GetDescription(),
                        ioType = ((AccountIOTypeEnum)r.ioType).GetDescription(),
                        wallet = ((WalletCateEnum)r.Wallet).GetDescription(),
                        balance = r.WalletBalance.ToDecimal() / 10000
                    }
                }),
                total = pg.records,
                pages = pg.total
            });
        }

        #region 充值

        //充值申请
        public ActionResult Recharge()
        {
            RechargeParamter param = GetRequestParamter<RechargeParamter>();

            bool isret;
            tMembersEntity member;
            ActionResult ar = AnalysisToken(param, out isret, out member);
            if (isret)
                return ar;

            if (param.img.IsEmpty())
            {
                return RetJsonResult(ApiResponseCodeEnum.Fail, "请上传转款凭证");
            }

            if (!MD5Helper.PwdEncryption(param.pwd).Equals(member.mbTwoPwd))
                return RetJsonResult(ApiResponseCodeEnum.Fail, "二级密码不正确");

            tUserRechargeRecordEntity entity = new tUserRechargeRecordEntity()
            {
                recordGuid = param.reguId,
                mbId = param.user.Id,
                bankId = param.bankId,
                rechargeAmount = param.amount * 10000,
                transferImg = param.img,
                ip = IPAddressHelper.GetClientIPAddress,
                client = param.clientType
            };
            string ret;
            if (tUserRechargeRecordBLL.Instance.Recharge(entity, out ret))
            {
                return RetJsonResult(ApiResponseCodeEnum.Success, "提交成功");
            }
            return RetJsonResult(ApiResponseCodeEnum.Success, ret.IsEmpty() ? "提交失败" : ret);
        }

        //充值记录
        public ActionResult RechargeRecord()
        {
            RechargeRecordParamter param = GetRequestParamter<RechargeRecordParamter>();

            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            Pagination pg = new Pagination
            {
                page = param.page,
                rows = param.size,
                sidx = "rechargeTime",
                sord = "desc"
            };
            pg.AddWhere("mbId", member.mbId);
            if (param.sdate.IsDateString())
            {
                pg.AddWhere("rechargeTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 00:00:00", param.sdate)), SqlDbType.BigInt, CompareSymbolEnum.MoreThanEqual);
            }
            if (param.edate.IsDateString())
            {
                pg.AddWhere("rechargeTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 23:59:59", param.edate)), SqlDbType.BigInt, CompareSymbolEnum.LessThanEqual);
            }
            List<tUserRechargeRecordEntity> list = tUserRechargeRecordBLL.Instance.GetList(pg);
            return RetJsonResult(ApiResponseCodeEnum.Success, "", new
            {
                list = list.Select(r => new
                {
                    r.recordId,
                    time = DateHelper.IntToDateTime(r.rechargeTime).ToString("yyyy-MM-dd HH:mm:ss"),
                    ratio = string.Format("{0}%", ProcessAmountToPointFour(r.exchangeRatio)),
                    amount = ProcessAmountToPointFour(r.rechargeAmount),
                    state = ((RechargeStateEnum)r.rechargeState).GetDescription(),
                    detail = new
                    {
                        r.bankName,
                        r.bankCode,
                        atime = DateHelper.IntToDateTime(r.auditTime).ToString("yyyy-MM-dd HH:mm:ss")
                    }
                }),
                total = pg.records,
                pages = pg.total
            });
        }

        #endregion

        #region 提现

        //提现申请
        public ActionResult Withdraw()
        {
            WithdrawParamter param = GetRequestParamter<WithdrawParamter>();

            bool isret;
            tMembersEntity member;
            ActionResult ar = AnalysisToken(param, out isret, out member);
            if (isret)
                return ar;

            if (!MD5Helper.PwdEncryption(param.pwd).Equals(member.mbTwoPwd))
                return RetJsonResult(ApiResponseCodeEnum.Fail, "二级密码不正确");

            tUserWithdrawRecordEntity entity = new tUserWithdrawRecordEntity()
            {
                mbId = param.user.Id,
                bankId = param.bankId,
                withdrawAmount = param.amount*10000,
                ip = IPAddressHelper.GetClientIPAddress,
                client = param.clientType
            };
            string ret;
            if (tUserWithdrawRecordBLL.Instance.Withdraw(entity, out ret))
            {
                return RetJsonResult(ApiResponseCodeEnum.Success, "提交成功");
            }
            return RetJsonResult(ApiResponseCodeEnum.Success, ret.IsEmpty() ? "提交失败" : ret);
        }

        //提现记录
        public ActionResult WithdrawRecord()
        {
            WithdrawRecordParamter param = GetRequestParamter<WithdrawRecordParamter>();

            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            Pagination pg = new Pagination
            {
                page = param.page,
                rows = param.size,
                sidx = "withdrawTime",
                sord = "desc"
            };
            pg.AddWhere("mbId", member.mbId);
            if (param.sdate.IsDateString())
            {
                pg.AddWhere("withdrawTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 00:00:00", param.sdate)), SqlDbType.BigInt, CompareSymbolEnum.MoreThanEqual);
            }
            if (param.edate.IsDateString())
            {
                pg.AddWhere("withdrawTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 23:59:59", param.edate)), SqlDbType.BigInt, CompareSymbolEnum.LessThanEqual);
            }
            List<tUserWithdrawRecordEntity> list = tUserWithdrawRecordBLL.Instance.GetList(pg);
            return RetJsonResult(ApiResponseCodeEnum.Success, "", new
            {
                list = list.Select(r => new
                {
                    r.recordId,
                    time = DateHelper.IntToDateTime(r.withdrawTime).ToString("yyyy-MM-dd HH:mm:ss"),
                    ratio = string.Format("{0}%", ProcessAmountToPointFour(r.exchangeRatio)),
                    amount = ProcessAmountToPointFour(r.withdrawAmount),
                    state = ((WithdrawStateEnum)r.withdrawState).GetDescription(),
                    detail = new
                    {
                        r.bankName,
                        r.bankCode,
                        fratio = string.Format("{0}%", r.feeRatio.ToDecimal() / 10000),
                        fee = string.Format("{0}%", r.fee.ToDecimal() / 10000),
                        atime = DateHelper.IntToDateTime(r.auditTime).ToString("yyyy-MM-dd HH:mm:ss")
                    }
                }),
                total = pg.records,
                pages = pg.total
            });
        }

        #endregion

        #region 投资理财

        //投资理财套餐
        public ActionResult InvestPackage()
        {
            ApiParamter param = GetRequestParamter<ApiParamter>();

            bool isret;
            tMembersEntity member;
            ActionResult ar = AnalysisToken(param, out isret, out member);
            if (isret)
                return ar;

            var where = LinqExtensions.True<tInvestPackageEntity>();
            where = where.And(p => p.pkState.Equals(1));
            var list = tInvestPackageBLL.Instance.GetList(where).OrderBy(p => p.pkLevel);
            return RetJsonResult(ApiResponseCodeEnum.Success, "", list.Select(r => new
            {
                r.pkId,
                pkName = string.Format("{0}{1}$-{2}$", r.pkName, r.pkMinAmount, r.pkMaxAmount)
            }));
        }


        //投资理财
        public ActionResult Invest()
        {
            InvestParamter param = GetRequestParamter<InvestParamter>();

            bool isret;
            tMembersEntity member;
            ActionResult ar = AnalysisToken(param, out isret, out member);
            if (isret)
                return ar;

            if (!MD5Helper.PwdEncryption(param.pwd).Equals(member.mbTwoPwd))
                return RetJsonResult(ApiResponseCodeEnum.Fail, "二级密码不正确");

            tUserInvestRecordEntity entity = new tUserInvestRecordEntity()
            {
                mbId = param.user.Id,
                investAmount = param.amount*10000,
                investIp = IPAddressHelper.GetClientIPAddress,
                investClient = param.clientType
            };
            string ret;
            if (tUserInvestRecordBLL.Instance.Invest(entity, out ret))
            {
                return RetJsonResult(ApiResponseCodeEnum.Success, "投资成功");
            }
            return RetJsonResult(ApiResponseCodeEnum.Success, ret.IsEmpty() ? "投资失败" : ret);
        }

        //投资理财记录
        public ActionResult InvestRecord()
        {
            InvestRecordParamter param = GetRequestParamter<InvestRecordParamter>();

            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            Pagination pg = new Pagination
            {
                page = param.page,
                rows = param.size,
                sidx = "investTime",
                sord = "desc"
            };
            pg.AddWhere("mbId", member.mbId);
            if (param.sdate.IsDateString())
            {
                pg.AddWhere("investTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 00:00:00", param.sdate)), SqlDbType.BigInt, CompareSymbolEnum.MoreThanEqual);
            }
            if (param.edate.IsDateString())
            {
                pg.AddWhere("investTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 23:59:59", param.edate)), SqlDbType.BigInt, CompareSymbolEnum.LessThanEqual);
            }
            List<tUserInvestRecordEntity> list = tUserInvestRecordBLL.Instance.GetList(pg);
            return RetJsonResult(ApiResponseCodeEnum.Success, "", new
            {
                list = list.Select(r => new
                {
                    r.recordId,
                    time = DateHelper.IntToDateTime(r.investTime).ToString("yyyy-MM-dd HH:mm:ss"),
                    amount = ProcessAmountToPointFour(r.investAmount),
                    state = ((InvestStopStateEnum)r.stopState).GetDescription(),
                    detail = new
                    {
                        level = r.pkName,//投资套餐级别
                        ssProfit = ProcessAmountToPointFour(r.stopStaticProfitAmount),//分红出局静态收益额
                        sProfit = ProcessAmountToPointFour(r.stopProfitAmount.ToDecimal()),//分红出局总收益额
                        stProfit = ProcessAmountToPointFour(r.staticProfitAmount.ToDecimal()),//静态收益额
                        profit = ProcessAmountToPointFour(r.profitAmount.ToDecimal()),//总收益额
                        stime = DateHelper.IntToDateTime(r.stopTime).ToString("yyyy-MM-dd HH:mm:ss")//出局时间
                    }
                }),
                total = pg.records,
                pages = pg.total
            });
        }

        #endregion

        #region 购买基金

        //基金列表
        public ActionResult Funds()
        {
            ApiParamter param = GetRequestParamter<ApiParamter>();

            bool isret;
            tMembersEntity member;
            ActionResult ar = AnalysisToken(param, out isret, out member);
            if (isret)
                return ar;

            var where = LinqExtensions.True<tFundsEntity>();
            where = where.And(p => p.fundState.Equals(1));
            var list = tFundsBLL.Instance.GetList(where);

            var where_period = LinqExtensions.True<tFundPeriodEntity>();
            where_period = where_period.And(p => p.periodState.Equals(1));
            var list_period = tFundPeriodBLL.Instance.GetList(where_period).OrderBy(p => p.periodDays);

            return RetJsonResult(ApiResponseCodeEnum.Success, "", list.Select(r => new
            {
                id = r.fundId,
                name = r.fundName,
                periods = list_period.Where(p => p.fundId.Equals(r.fundId)).Select(p => new
                {
                    id = p.periodId,
                    name = string.Format("{0}天{1}%", p.periodDays, ProcessAmountToPointFour(p.periodRatio, 2)),
                    min = p.periodMinAmount
                })
            }));
        }

        //投资理财
        public ActionResult BuyFund()
        {
            FundsParamter param = GetRequestParamter<FundsParamter>();
            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            if (!MD5Helper.PwdEncryption(param.pwd).Equals(member.mbTwoPwd))
                return RetJsonResult(ApiResponseCodeEnum.Fail, "二级密码不正确");

            tUserBuyFundRecordEntity entity = new tUserBuyFundRecordEntity()
            {
                mbId = param.user.Id,
                fundId = param.fundId,
                periodId = param.periodId,
                buyAmount = param.amount * 10000,
                ip = IPAddressHelper.GetClientIPAddress,
                client = param.clientType
            };
            string ret;
            if (tUserBuyFundRecordBLL.Instance.BuyFund(entity, out ret))
            {
                return RetJsonResult(ApiResponseCodeEnum.Success, "购买成功");
            }
            return RetJsonResult(ApiResponseCodeEnum.Success, ret.IsEmpty() ? "购买失败" : ret);
        }

        //投资理财记录
        public ActionResult FundsRecord()
        {
            FundsRecordParamter param = GetRequestParamter<FundsRecordParamter>();

            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            Pagination pg = new Pagination
            {
                page = param.page,
                rows = param.size,
                sidx = "buyTime",
                sord = "desc"
            };
            pg.AddWhere("mbId", member.mbId);
            if (param.sdate.IsDateString())
            {
                pg.AddWhere("buyTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 00:00:00", param.sdate)), SqlDbType.BigInt, CompareSymbolEnum.MoreThanEqual);
            }
            if (param.edate.IsDateString())
            {
                pg.AddWhere("buyTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 23:59:59", param.edate)), SqlDbType.BigInt, CompareSymbolEnum.LessThanEqual);
            }
            List<tUserBuyFundRecordEntity> list = tUserBuyFundRecordBLL.Instance.GetList(pg);
            return RetJsonResult(ApiResponseCodeEnum.Success, "", new
            {
                list = list.Select(r => new
                {
                    r.recordId,
                    time = DateHelper.IntToDateTime(r.buyTime).ToString("yyyy-MM-dd HH:mm:ss"),
                    amount = ProcessAmountToPointFour(r.buyAmount),
                    ratio = string.Format("{0}%", ProcessAmountToPointFour(r.bonusRation, 2)),
                    state = ((YesNoEnum)r.settleState).GetDescription(),
                    detail = new
                    {
                        r.fundName,
                        r.fundEnName,
                        r.periodDays,
                        sAmount = ProcessAmountToPointFour(r.settleAmount),
                        stime = DateHelper.IntToDateTime(r.settleTime).ToString("yyyy-MM-dd HH:mm:ss")//结算时间
                    }
                }),
                total = pg.records,
                pages = pg.total
            });
        }

        #endregion

        #region 购买股权

        //投资理财
        public ActionResult BuyStock()
        {
            StockParamter param = GetRequestParamter<StockParamter>();
            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            if (!MD5Helper.PwdEncryption(param.pwd).Equals(member.mbTwoPwd))
                return RetJsonResult(ApiResponseCodeEnum.Fail, "二级密码不正确");

            tUserStockRightBuyRecordEntity entity = new tUserStockRightBuyRecordEntity()
            {
                mbId = param.user.Id,
                shares =Convert.ToInt64(param.shares * 10000),
                ip = IPAddressHelper.GetClientIPAddress,
                client = param.clientType
            };
            if (tUserStockRightBuyRecordBLL.Instance.BuyStock(entity, out string ret))
            {
                return RetJsonResult(ApiResponseCodeEnum.Success, "购买成功");
            }
            return RetJsonResult(ApiResponseCodeEnum.Success, ret.IsEmpty() ? "购买失败" : ret);
        }

        //投资理财记录
        public ActionResult StockRecord()
        {
            StockRecordParamter param = GetRequestParamter<StockRecordParamter>();

            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            Pagination pg = new Pagination
            {
                page = param.page,
                rows = param.size,
                sidx = "buyTime",
                sord = "desc"
            };
            pg.AddWhere("mbId", member.mbId);
            if (param.sdate.IsDateString())
            {
                pg.AddWhere("buyTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 00:00:00", param.sdate)), SqlDbType.BigInt, CompareSymbolEnum.MoreThanEqual);
            }
            if (param.edate.IsDateString())
            {
                pg.AddWhere("buyTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 23:59:59", param.edate)), SqlDbType.BigInt, CompareSymbolEnum.LessThanEqual);
            }
            List<tUserStockRightBuyRecordEntity> list = tUserStockRightBuyRecordBLL.Instance.GetList(pg);
            return RetJsonResult(ApiResponseCodeEnum.Success, "", new
            {
                list = list.Select(r => new
                {
                    r.recordId,
                    time = DateHelper.IntToDateTime(r.buyTime).ToString("yyyy-MM-dd HH:mm:ss"),
                    price = ProcessAmountToPointFour(r.price),
                    shares = ProcessAmountToPointFour(r.shares),
                    amount = ProcessAmountToPointFour(r.total),
                    state = ((StockRightRecordState)r.recordState).GetDescription(),
                    detail = new
                    {
                        rprice = r.redeemPrice,
                        ramount = r.redeemTotal,
                        rtime = DateHelper.IntToDateTime(r.redeemTime).ToString("yyyy-MM-dd HH:mm:ss")//赎回时间
                    }
                }),
                total = pg.records,
                pages = pg.total
            });
        }

        #endregion

        #region 客服聊天

        public ActionResult Talk()
        {
            TalkParamter param = GetRequestParamter<TalkParamter>();
            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            if (param.toMbId < 0)
                return RetJsonResult(ApiResponseCodeEnum.Fail, "收件人不存在");

            if (param.content.IsEmpty())
                return RetJsonResult(ApiResponseCodeEnum.Fail, "请输入消息内容");

            tTalkMsgEntity entity = new tTalkMsgEntity()
            {
                sendMbId = param.user.Id,
                sendTime = DateHelper.GetTimeStamp_Seconds(),
                msgContent = param.content,
                receiveMbId = param.toMbId,
                isReplay = YesNoEnum.Yes.GetHashCode(),
                msgState = YesNoEnum.Yes.GetHashCode()
            };

            return tTalkMsgBLL.Instance.SubmitForm(entity, entity.msgId) ?
                 RetJsonResult(ApiResponseCodeEnum.Success, "发送成功") :
                 RetJsonResult(ApiResponseCodeEnum.Fail, "发送失败");
        }

        public ActionResult TalkRecord()
        {
            TalkRecordParamter param = GetRequestParamter<TalkRecordParamter>();
            ActionResult ar = AnalysisToken(param, out bool isret, out tMembersEntity member);
            if (isret)
                return ar;

            Pagination pg = new Pagination
            {
                page = param.page,
                rows = param.size,
                sidx = "sendTime",
                sord = "desc"
            };
            //发送人
            pg.AddWhere("sendMbId", member.mbId);
            //接收人
            pg.AddWhere("receiveMbId", param.toMbId);
            if (param.sdate.IsDateString())
            {
                pg.AddWhere("sendTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 00:00:00", param.sdate)), SqlDbType.BigInt, CompareSymbolEnum.MoreThanEqual);
            }
            if (param.edate.IsDateString())
            {
                pg.AddWhere("sendTime", DateHelper.GetTimeStamp_Seconds(string.Format("{0} 23:59:59", param.edate)), SqlDbType.BigInt, CompareSymbolEnum.LessThanEqual);
            }
            List<tTalkMsgEntity> list = tTalkMsgBLL.Instance.GetList(pg);
            return RetJsonResult(ApiResponseCodeEnum.Success, "", new
            {
                list = list.Select(r => new
                {
                    r.msgId,
                    time = DateHelper.IntToDateTime(r.sendTime).ToString("yyyy-MM-dd HH:mm:ss"),
                    content = r.msgContent
                }),
                total = pg.records,
                pages = pg.total
            });
        }

        #endregion

        //处理以毫为单位的金额 保留4位小数
        decimal ProcessAmountToPointFour(object amount, int afterpoint = 4)
        {
            decimal d = amount.ToDecimal() / 10000m;
            if (afterpoint == 4)
            { return d; }
            return Math.Round(d, afterpoint);
        }

        T GetRequestParamter<T>()
        {
            string json = "";
            using (StreamReader sr = new StreamReader(Request.InputStream))
            {
                json = sr.ReadToEnd();
            }
            return JsonAdapter.Deserialize<T>(json);
        }

        //解析token
        ActionResult AnalysisToken(ApiParamter paramter, out bool isret, out tMembersEntity member)
        {
            member = null;
            isret = true;

            if (paramter == null)
                return RetToLogin("登录过期，请重新登录");

            if (paramter.token.IsEmpty())
                return RetToLogin("登录过期，请重新登录");

            paramter.token = DES3Encrypt.Decrypt(paramter.token);

            paramter.user = JsonAdapter.Deserialize<UserToken>(paramter.token);
            if (paramter.user == null)
                return RetJsonResult(ApiResponseCodeEnum.Fail, "非法请求");

            if (DateHelper.GetTimeStamp_Seconds() - paramter.user.time > (30 * 60))
                return RetToLogin("登录过期，请重新登录");

            if (paramter.user.Id <= 0)
                return RetJsonResult(ApiResponseCodeEnum.Fail, "非法请求");

            member = tMembersBLL.Instance.GetModel(paramter.user.Id);
            if (member == null)
                return RetJsonResult(ApiResponseCodeEnum.Fail, "非法请求");

            if (member.mbState.Value.Equals(YesNoEnum.No.GetHashCode()))
                return RetToLogin("账户被禁用");

            paramter.clientType = ClientHelper.IsPC(Request.UserAgent) ? "PC" : "H5";

            isret = false;
            return RetJsonResult(ApiResponseCodeEnum.Success);
        }

        //给客户端返回json串
        private ActionResult RetJsonResult(ApiResponseCodeEnum status, string msg = "", object data = null)
        {
            if (data == null)
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        success = status.Equals(ApiResponseCodeEnum.Success),
                        msg = msg.IsNullOrWhiteSpace() ? status.GetDescription() : msg
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };
            }

            return new JsonResult()
            {
                Data = new
                {
                    success = status.Equals(ApiResponseCodeEnum.Success),
                    msg = msg.IsNullOrWhiteSpace() ? status.GetDescription() : msg,
                    data = data
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }

        //给客户端返回可以直接跳转到登录页的json串
        ActionResult RetToLogin(string msg)
        {
            return RetJsonResult(ApiResponseCodeEnum.Fail, msg, new { golg = true });
        }
    }

}
