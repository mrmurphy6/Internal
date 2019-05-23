using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Internal.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Internal.Mapping{
	public class tUserBuyFundRecordMap:EntityTypeConfiguration<tUserBuyFundRecordEntity>
	{
      	public tUserBuyFundRecordMap()
        {
            this.ToTable("tUserBuyFundRecord");
            this.HasKey(t => t.recordId);
        } 
   
	}
}