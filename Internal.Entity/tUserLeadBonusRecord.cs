using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Entity{
	public class tUserLeadBonusRecordEntity
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
		/// 1初级 2中级 3高级 4高级平级 5全球级
        /// </summary>        
        
        public int leadBonusLevel{ get; set;}  
        
		/// <summary>
		/// 1代会员的会员Id
        /// </summary>        
        
        public int captionMbId{ get; set;}  
        
		/// <summary>
		/// investMbId
        /// </summary>        
        
        public int investMbId{ get; set;}  

        /// <summary>
        /// 投资人用户编号
        /// </summary>
        [NotMapped]
        public string investUserNo { get; set; }

        /// <summary>
        /// investRecordId
        /// </summary>        

        public int investRecordId{ get; set;}  

        [NotMapped]
        public string investNo { get; set; }

        [NotMapped]
        public long investTime { get; set; }

        /// <summary>
        /// 投资人的投资套餐级别
        /// </summary>
        [NotMapped]
        public string mbpkLevelName { get; set; }

        /// <summary>
        /// 投资记录状态
        /// </summary>
        [NotMapped]
        public int stopState { get; set; }

        /// <summary>
        /// 投资记录出局时间
        /// </summary>
        [NotMapped]
        public long stopTime { get; set; }

        /// <summary>
        /// investAmount 以毫为单位，1就存10000
        /// </summary>        

        public long investAmount{ get; set;}  
        
		/// <summary>
		/// 以毫为单位，1就存10000
        /// </summary>        
        
        public long bonusRatio{ get; set;}  
        
		/// <summary>
		/// 以毫为单位，1就存10000
        /// </summary>        
        
        public long bonusAmount { get; set;}  
        
		/// <summary>
		/// bonusTime
        /// </summary>        
        
        public long bonusTime{ get; set;}  
        
		   
	}
}