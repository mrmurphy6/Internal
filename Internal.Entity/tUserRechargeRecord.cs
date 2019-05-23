using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Entity{
	public class tUserRechargeRecordEntity
	{
      	/// <summary>
		/// recordId
        /// </summary>        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int recordId{ get; set;}  

        /// <summary>
        /// Guid
        /// </summary>
        public string recordGuid { get; set; }

        /// <summary>
        /// mbId
        /// </summary>        

        public int mbId{ get; set;}  

        [NotMapped]
        public string mbUserName { get; set; }

        [NotMapped]
        public string mbUserNo { get; set; }

        [NotMapped]
        public string mbRealName { get; set; }

        /// <summary>
        /// bankId
        /// </summary>        

        public int bankId{ get; set;}

        [NotMapped]
        public string bankName { get; set; }

        [NotMapped]
        public string bankCode { get; set; }

        /// <summary>
        /// rechargeAmount，以毫为单位，1就存10000
        /// </summary>        

        public long rechargeAmount{ get; set;}  
        
		/// <summary>
		/// 兑换比例，1美元兑换多少RMB，以毫为单位，1就存10000
        /// </summary>        
        
        public int exchangeRatio{ get; set;}  
        
		/// <summary>
		/// transferImg
        /// </summary>        
        
        public string transferImg{ get; set;}  
        
		/// <summary>
		/// 1审核中 2审核通过 3驳回 4会员取消
        /// </summary>        
        
        public int rechargeState{ get; set;}  
        
		/// <summary>
		/// rechargeTime
        /// </summary>        
        
        public long rechargeTime{ get; set;}  
        
		/// <summary>
		/// auditTime
        /// </summary>        
        
        public long auditTime{ get; set;}  
        
		   
        public string ip { get; set; }

        public string client { get; set; }
	}
}