using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Internal.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Internal.Mapping{
	public class tUserZBDayBonusRecordMap:EntityTypeConfiguration<tUserZBDayBonusRecordEntity>
	{
      	public tUserZBDayBonusRecordMap()
        {
            this.ToTable("tUserZBDayBonusRecord");
            this.HasKey(t => t.recordId);
        } 
   
	}
}