using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Entity{
    public class tUserAccountRecordEntity
    {
        /// <summary>
        /// 主键
        /// </summary>        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int recordId { get; set; }

        /// <summary>
        /// mbId
        /// </summary>        

        public int mbId { get; set; }

        public string mbUserName { get; set; }
        public string mbUserNo { get; set; }

        public string mbRealName { get; set; }

        /// <summary>
        /// 1收入 2支出
        /// </summary>        

        public int direction { get; set; }

        /// <summary>
        /// 10充值 20投资 30日分红 40分红代数奖励 
        /// 50直推奖励 60代理领导奖励
        /// 70购买基金 80基金到期收益 90提现
        /// 100签到奖励 110邀请奖励 120随机红包
        /// </summary>        

        public int ioType { get; set; }

        /// <summary>
        /// outWallet
        /// </summary>        

        public int Wallet { get; set; }

        /// <summary>
        /// 以毫为单位，1就存10000
        /// </summary>        

        public long WalletBalance { get; set; }

        /// <summary>
        /// 以毫为单位，1就存10000
        /// </summary>        

        public long happendAmount { get; set; }

        /// <summary>
        /// 收入是正数，支出是负数
        /// </summary>
        public long realAmount { get; set; }

        /// <summary>
        /// happendTime
        /// </summary>        

        public long happendTime { get; set; }

        /// <summary>
        /// 对应操作记录Id
        /// </summary>
        public int operaRecordId { get; set; }

        public string operaDesc { get; set; }
    }
}