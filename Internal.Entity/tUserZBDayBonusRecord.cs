using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Entity{
	public class tUserZBDayBonusRecordEntity
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
		/// investMbId
        /// </summary>        
        
        public int investMbId{ get; set;}  

        [NotMapped]
        public string investUserNo { get; set; }
        
		/// <summary>
		/// investRecordId
        /// </summary>        
        
        public int investRecordId{ get; set;}

        [NotMapped]
        public string investNo { get; set; }

        [NotMapped]
        public long investAmount { get; set; }

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
        /// dayBonusRecordId
        /// </summary>        

        public int dayBonusRecordId{ get; set;}  
        
		/// <summary>
		/// 投资会员是被奖励会员的第几代
        /// </summary>        
        
        public int level{ get; set;}  
        
		/// <summary>
		/// 以毫为单位，1就存10000
        /// </summary>        
        
        public long ratio { get; set;}  
        
		/// <summary>
		/// 以毫为单位，1就存10000
        /// </summary>        
        
        public long dayBonusAmount { get; set;}  
        
		/// <summary>
		/// 以毫为单位，1就存10000
        /// </summary>        
        
        public long rewardAmount { get; set;}  
        
		/// <summary>
		/// rewardTime
        /// </summary>        
        
        public long rewardTime{ get; set;}  
        
		   
	}
}