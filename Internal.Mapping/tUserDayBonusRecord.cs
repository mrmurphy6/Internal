using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Internal.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Internal.Mapping{
	public class tUserDayBonusRecordMap:EntityTypeConfiguration<tUserDayBonusRecordEntity>
	{
      	public tUserDayBonusRecordMap()
        {
            this.ToTable("tUserDayBonusRecord");
            this.HasKey(t => t.recordId);
        } 
   
	}
}