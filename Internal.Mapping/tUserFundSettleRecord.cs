using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Internal.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Internal.Mapping{
	public class tUserFundSettleRecordMap:EntityTypeConfiguration<tUserFundSettleRecordEntity>
	{
      	public tUserFundSettleRecordMap()
        {
            this.ToTable("tUserFundSettleRecord");
            this.HasKey(t => t.recordId);
        } 
   
	}
}