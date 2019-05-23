using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Internal.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Internal.Mapping{
	public class tUserStockRightBuyRecordMap:EntityTypeConfiguration<tUserStockRightBuyRecordEntity>
	{
      	public tUserStockRightBuyRecordMap()
        {
            this.ToTable("tUserStockRightBuyRecord");
            this.HasKey(t => t.recordId);
        } 
   
	}
}