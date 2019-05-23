using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Internal.Entity{
	public class tZBBonusRatioConfigEntity
	{
      	/// <summary>
		/// configId
        /// </summary>        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int configId{ get; set;}  
        
		/// <summary>
		/// configName
        /// </summary>        
        
        public string configName{ get; set;}  
        
		/// <summary>
		/// 不能重复，1就是1代，2就是2代，以此类推
        /// </summary>        
        
        public int? configLevel{ get; set;}  
        
		/// <summary>
		/// 以毫为单位，12就存120000
        /// </summary>        

        public int? configRatio { get; set; }  
        
		/// <summary>
		/// 1正常 2禁用
        /// </summary>        

        public int? configState { get; set; }  
        
		/// <summary>
		/// createTime
        /// </summary>        

        public DateTime? createTime { get; set; }  
        
		/// <summary>
		/// modifyTime
        /// </summary>        

        public DateTime? modifyTime { get; set; }  
        
		   
	}
}