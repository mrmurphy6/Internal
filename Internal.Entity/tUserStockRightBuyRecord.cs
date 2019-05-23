using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Entity{
	public class tUserStockRightBuyRecordEntity
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
		/// 股数，以毫为单位，比如1就存10000
        /// </summary>        
        
        public long shares{ get; set;}  
        
		/// <summary>
		/// 价格，以毫为单位，比如1就存10000
        /// </summary>        
        
        public long price{ get; set;}  
        
		/// <summary>
		/// 所需积分，以毫为单位，比如1就存10000
        /// </summary>        
        
        public long total{ get; set;}  
        
		/// <summary>
		/// buyTime
        /// </summary>        
        
        public long buyTime{ get; set;}  
        
		/// <summary>
		/// 1正常 2赎回
        /// </summary>        
        
        public int recordState{ get; set;}  
        
		/// <summary>
		/// redeemTime
        /// </summary>        
        
        public long redeemTime{ get; set;}  
        
		/// <summary>
		/// 赎回价格，以毫为单位，比如1就存10000
        /// </summary>        
        
        public long redeemPrice{ get; set;}  
        
		/// <summary>
		/// 赎回总额，以毫为单位，比如1就存10000
        /// </summary>        
        
        public long redeemTotal{ get; set;}  
        
		/// <summary>
		/// ip
        /// </summary>        
        
        public string ip{ get; set;}  
        
		/// <summary>
		/// PC H5
        /// </summary>        
        
        public string client{ get; set;}  
        
		   
	}
}