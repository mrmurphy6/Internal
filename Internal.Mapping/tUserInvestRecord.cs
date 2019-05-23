using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Internal.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Internal.Mapping{
	public class tUserInvestRecordMap:EntityTypeConfiguration<tUserInvestRecordEntity>
	{
      	public tUserInvestRecordMap()
        {
            this.ToTable("tUserInvestRecord");
            this.HasKey(t => t.recordId);
        } 
   
	}
}