using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DbData;
using Common;
using Internal.Entity;

namespace Internal.DAL 
{
	 	//tUserLeadBonusRecord
		public class tUserLeadBonusRecordDAL:RepositoryFactory
	{
		/// <summary>
        /// 获取单体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public tUserLeadBonusRecordEntity GetModel(int keyValue)
        {
            return this.BaseRepository().FindEntity<tUserLeadBonusRecordEntity>(keyValue);
        }

        public List<tUserLeadBonusRecordEntity> GetList(Pagination pagination)
        {
            List<SqlParameter> param;
            string where = SearchHelper.AnalysisSearchModelMosaicSql(pagination.WhereSearchItems, false, out param, "and", null);

            return this.BaseRepository().FindList<tUserLeadBonusRecordEntity>(string.Format("select * from view_tUserLeadBonusRecord where 1=1 {0}", where), pagination) as List<tUserLeadBonusRecordEntity>;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public bool Delete(int keyValue)
        {
           	return this.BaseRepository().Delete<tUserLeadBonusRecordEntity>(t => t.recordId == keyValue)>0;
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public bool SubmitForm(tUserLeadBonusRecordEntity entity, int keyValue)
        {
            if (keyValue>0      )
            {
               return this.BaseRepository().Update(entity)>0;
            }
            else
            {
                return this.BaseRepository().Insert(entity)>0;
            }
        }
	}
}