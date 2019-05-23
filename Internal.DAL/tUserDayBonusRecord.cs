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
	 	//tUserDayBonusRecord
		public class tUserDayBonusRecordDAL:RepositoryFactory
	{
		/// <summary>
        /// 获取单体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public tUserDayBonusRecordEntity GetModel(int keyValue)
        {
            return this.BaseRepository().FindEntity<tUserDayBonusRecordEntity>(keyValue);
        }

        public List<tUserDayBonusRecordEntity> GetList(Pagination pagination)
        {
            List<SqlParameter> param;
            string where = SearchHelper.AnalysisSearchModelMosaicSql(pagination.WhereSearchItems, false, out param, "and", null);

            return this.BaseRepository().FindList<tUserDayBonusRecordEntity>(string.Format("select * from view_tUserDayBonusRecord where 1=1 {0}", where), pagination) as List<tUserDayBonusRecordEntity>;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public bool Delete(int keyValue)
        {
           	return this.BaseRepository().Delete<tUserDayBonusRecordEntity>(t => t.recordId == keyValue)>0;
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public bool SubmitForm(tUserDayBonusRecordEntity entity, int keyValue)
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