using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Entity{
	public class tUserInvestRecordEntity
	{
      	/// <summary>
		/// recordId
        /// </summary>        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int recordId{ get; set;}  

        public string recordNo { get; set; }
        
		/// <summary>
		/// mbId
        /// </summary>        
        
        public int mbId { get; set; }

        [NotMapped]
        public string mbUserName { get; set; }

        [NotMapped]
        public string mbUserNo { get; set; }

        [NotMapped]
        public string mbRealName { get; set; }
        
		/// <summary>
		/// investAmount
        /// </summary>        
        
        public long investAmount{ get; set;}  
        
		/// <summary>
		/// mbpkLevel
        /// </summary>        
        
        public int mbpkLevel{ get; set;}  

        [NotMapped]
        public string pkName { get; set; }

        [NotMapped]
        public string pkEnName { get; set; }

        /// <summary>
        /// stopStaticProfitAmount 以毫为单位，1就存10000
        /// </summary>        

        public long stopStaticProfitAmount { get; set;}

        /// <summary>
        /// stopProfitAmount 以毫为单位，1就存10000
        /// </summary>        

        public long stopProfitAmount { get; set;}

        /// <summary>
        /// staticProfitAmount 以毫为单位，1就存10000
        /// </summary>        

        public long staticProfitAmount { get; set;}  
        
		/// <summary>
		/// 以毫为单位，1就存10000
        /// </summary>        
        
        public long profitAmount{ get; set;}  
        
		/// <summary>
		/// investTime
        /// </summary>        
        
        public long investTime{ get; set;}  
        
		/// <summary>
		/// investIp
        /// </summary>        
        
        public string investIp{ get; set;}  
        
		/// <summary>
		/// PC H5
        /// </summary>        
        
        public string investClient{ get; set;}  
        
		/// <summary>
		/// 1未出局 2静态出局 3动态出局
        /// </summary>        
        
        public int stopState{ get; set;}  
        
		/// <summary>
		/// stopTime
        /// </summary>        
        
        public long stopTime{ get; set;}

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