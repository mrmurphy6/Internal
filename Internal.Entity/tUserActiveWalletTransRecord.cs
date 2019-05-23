using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Entity{
	public class tUserActiveWalletTransRecordEntity
	{
      	/// <summary>
		/// 主键
        /// </summary>        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int recordId{ get; set;}  
        
		/// <summary>
		/// 转出会员
        /// </summary>        
        
        public int fromMbId{ get; set;}  
        
		/// <summary>
		/// 转入会员
        /// </summary>        
        
        public int toMbId{ get; set;}

        /// <summary>
        /// 金额，以毫为单位，1就存10000
        /// </summary>        

        public long transAmount{ get; set;}  
        
		/// <summary>
		/// 时间
        /// </summary>        
        
        public long transTime{ get; set;}  
        
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