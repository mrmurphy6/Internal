using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using Internal.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Internal.Mapping{
	public class tZBBonusRatioConfigMap:EntityTypeConfiguration<tZBBonusRatioConfigEntity>
	{
      	public tZBBonusRatioConfigMap()
        {
            this.ToTable("tZBBonusRatioConfig");
            this.HasKey(t => t.configId);
        } 
   
	}
}