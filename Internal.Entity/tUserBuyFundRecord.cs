using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Entity
{
    public class tUserBuyFundRecordEntity
    {
        /// <summary>
        /// recordId
        /// </summary>        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int recordId { get; set; }

        /// <summary>
        /// 会员Id
        /// </summary>

        public int mbId { get; set; }

        [NotMapped]
        public string mbUserName { get; set; }

        [NotMapped]
        public string mbUserNo { get; set; }

        [NotMapped]
        public string mbRealName { get; set; }

        /// <summary>
        /// 基金Id
        /// </summary>        

        public int fundId { get; set; }

        [NotMapped]
        public string fundName { get; set; }

        [NotMapped]
        public string fundEnName { get; set; }

        public int periodId { get; set; }

        [NotMapped]
        public int periodDays { get; set; }

        /// <summary>
        /// 以毫为单位，1就存10000
        /// </summary>        

        public long buyAmount { get; set; }

        /// <summary>
        /// 购买时间
        /// </summary>        

        public long buyTime { get; set; }

        /// <summary>
        /// 以毫为单位，1就存10000
        /// </summary>        

        public int bonusRation { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>        

        public long expireTime { get; set; }

        /// <summary>
        /// 1已到期 2未到期
        /// </summary>        

        public int expireState { get; set; }

        /// <summary>
        /// 结算时间
        /// </summary>        

        public long settleTime { get; set; }

        /// <summary>
        /// 结算金额 以毫为单位，1就存10000
        /// </summary>
        public long settleAmount { get; set; }

        /// <summary>
        /// 1已结算 2未结算
        /// </summary>
        public int settleState { get; set; }

        /// <summary>
        /// investIp
        /// </summary>        

        public string ip { get; set; }

        /// <summary>
        /// PC H5
        /// </summary>        

        public string client { get; set; }

        /// <summary>
        /// 锁定状态 1锁定 2未锁定
        /// </summary>
        public int lockState { get; set; }

        /// <summary>
        /// 锁定时间
        /// </summary>
        public DateTime lockTime { get; set; }
    }
}