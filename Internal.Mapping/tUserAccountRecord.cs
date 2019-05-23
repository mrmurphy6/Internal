using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Internal.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Internal.Mapping{
	public class tUserAccountRecordMap:EntityTypeConfiguration<tUserAccountRecordEntity>
	{
      	public tUserAccountRecordMap()
        {
            this.ToTable("tUserAccountRecord");
            this.HasKey(t => t.recordId);
        } 
   
	}
}