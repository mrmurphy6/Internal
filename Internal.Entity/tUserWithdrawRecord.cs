using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Entity{
	public class tUserWithdrawRecordEntity
	{
      	/// <summary>
		/// recordId
        /// </summary>        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int recordId{ get; set;}  
        
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
		/// 兑换比例，1美元兑换多少RMB，以毫为单位，1就存10000
        /// </summary>        
        
        public int exchangeRatio{ get; set;}

        /// <summary>
        /// withdrawAmount，以毫为单位，1就存10000
        /// </summary>        

        public long withdrawAmount{ get; set;}  
        
		/// <summary>
		/// bankId
        /// </summary>        
        
        public int bankId{ get; set;}

        [NotMapped]
        public string bankName { get; set; }

        [NotMapped]
        public string bankCode { get; set; }

        /// <summary>
        /// 以毫为单位，1就存10000
        /// </summary>        

        public long feeRatio{ get; set;}  
        
		/// <summary>
		/// 以毫为单位，1就存10000
        /// </summary>        
        
        public long fee { get; set;}  
        
		/// <summary>
		/// 1审核中 2已打款 3驳回 4会员取消
        /// </summary>        
        
        public int withdrawState{ get; set;}  
        
		/// <summary>
		/// withdrawTime
        /// </summary>        
        
        public long withdrawTime{ get; set;}  
        
		/// <summary>
		/// auditTime
        /// </summary>        
        
        public long auditTime{ get; set;}  
        
		   
        public string ip { get; set; }

        public string client { get; set; }
	}
}