using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Entity{
	public class tUserDayBonusRecordEntity
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
		/// investId
        /// </summary>        
        
        public int investId{ get; set;}  

        [NotMapped]
        public string investNo { get; set; }

        [NotMapped]
        public int investAmount { get; set; }

        [NotMapped]
        public long investTime { get; set; }

        [NotMapped]
        public int mbpkLevel { get; set; }

        [NotMapped]
        public string mbpkLevelName { get; set; }

        /// <summary>
        /// 1未出局 2静态出局 3动态出局
        /// </summary>
        [NotMapped]
        public int stopState { get; set; }

        /// <summary>
        /// stopTime
        /// </summary>
        [NotMapped]
        public long stopTime { get; set; }

        /// <summary>
        /// settleDate
        /// </summary>        

        public DateTime settleDate{ get; set;}  
        
		/// <summary>
		/// settleTime
        /// </summary>        
        
        public long settleTime{ get; set;}  
        
		/// <summary>
		/// 以毫为单位，1就存10000
        /// </summary>        
        
        public int settleRatio{ get; set;}  
        
		/// <summary>
		/// 以毫为单位，1就存10000
        /// </summary>        
        
        public long settleAmount{ get; set;}  
        
		   
	}
}