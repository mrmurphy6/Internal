using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Internal.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Internal.Mapping{
	public class tUserActiveWalletTransRecordMap:EntityTypeConfiguration<tUserActiveWalletTransRecordEntity>
	{
      	public tUserActiveWalletTransRecordMap()
        {
            this.ToTable("tUserActiveWalletTransRecord");
            this.HasKey(t => t.recordId);
        } 
   
	}
}