using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Internal.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Internal.Mapping{
	public class tUserRechargeRecordMap:EntityTypeConfiguration<tUserRechargeRecordEntity>
	{
      	public tUserRechargeRecordMap()
        {
            this.ToTable("tUserRechargeRecord");
            this.HasKey(t => t.recordId);
        } 
   
	}
}