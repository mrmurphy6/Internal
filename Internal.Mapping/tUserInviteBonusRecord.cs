using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Internal.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Internal.Mapping{
	public class tUserInviteBonusRecordMap:EntityTypeConfiguration<tUserInviteBonusRecordEntity>
	{
      	public tUserInviteBonusRecordMap()
        {
            this.ToTable("tUserInviteBonusRecord");
            this.HasKey(t => t.recordId);
        } 
   
	}
}