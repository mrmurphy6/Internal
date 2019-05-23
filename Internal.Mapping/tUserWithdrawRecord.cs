using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Internal.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Internal.Mapping{
	public class tUserWithdrawRecordMap:EntityTypeConfiguration<tUserWithdrawRecordEntity>
	{
      	public tUserWithdrawRecordMap()
        {
            this.ToTable("tUserWithdrawRecord");
            this.HasKey(t => t.recordId);
        } 
   
	}
}