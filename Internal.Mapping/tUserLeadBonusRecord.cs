using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Internal.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Internal.Mapping{
	public class tUserLeadBonusRecordMap:EntityTypeConfiguration<tUserLeadBonusRecordEntity>
	{
      	public tUserLeadBonusRecordMap()
        {
            this.ToTable("tUserLeadBonusRecord");
            this.HasKey(t => t.recordId);
        } 
   
	}
}