using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Entity{
	public class tUserFundSettleRecordEntity
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
        
		/// <summary>
		/// buyRecordId
        /// </summary>        
        
        public int buyRecordId{ get; set;}  
        
		/// <summary>
		/// 以毫为单位，1就存10000
        /// </summary>        
        
        public long settleAmount { get; set;}  
        
		/// <summary>
		/// settleTime
        /// </summary>        
        
        public long settleTime{ get; set;}  
        
		   
	}
}