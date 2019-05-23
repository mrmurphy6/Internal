using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Entity{
	public class tUserInviteBonusRecordEntity
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
		/// mbpkLevel
        /// </summary>        
        
        public int mbpkLevel{ get; set;}  
        
		/// <summary>
		/// 以分为单位，1就存10000
        /// </summary>        
        
        public int bonusRatio{ get; set;}  
        
		/// <summary>
		/// inviteMbId
        /// </summary>        
        
        public int inviteMbId { get; set; }

        [NotMapped]
        public string inviteUserNo { get; set; }

        /// <summary>
        /// investId
        /// </summary>        

        public int investId{ get; set;}  

        [NotMapped]
        public string investNo { get; set; }

        [NotMapped]
        public long investTime { get; set; }

        /// <summary>
        /// 投资人的投资套餐级别
        /// </summary>
        [NotMapped]
        public string mbpkLevelName { get; set; }

        [NotMapped]
        public int stopState { get; set; }

        [NotMapped]
        public long stopTime { get; set; }

        /// <summary>
        /// 以毫为单位，1就存10000
        /// </summary>        

        public long investAmount{ get; set;}

        /// <summary>
        /// 以毫为单位，1就存10000
        /// </summary>        

        public long bonusAmount{ get; set;}  
        
		/// <summary>
		/// bonusTime
        /// </summary>        
        
        public long bonusTime{ get; set;}  
        
		   
	}
}