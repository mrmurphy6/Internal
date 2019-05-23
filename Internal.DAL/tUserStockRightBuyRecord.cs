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
	 	//tUserStockRightBuyRecord
		public class tUserStockRightBuyRecordDAL:RepositoryFactory
	{
		/// <summary>
        /// 获取单体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public tUserStockRightBuyRecordEntity GetModel(int keyValue)
        {
            return this.BaseRepository().FindEntity<tUserStockRightBuyRecordEntity>(keyValue);
        }

        public List<tUserStockRightBuyRecordEntity> GetList(Pagination pagination)
        {
            List<SqlParameter> param;
            string where = SearchHelper.AnalysisSearchModelMosaicSql(pagination.WhereSearchItems, false, out param, "and", null);

            return this.BaseRepository().FindList<tUserStockRightBuyRecordEntity>(string.Format("select * from view_tUserStockRightBuyRecord where 1=1 {0}", where), pagination) as List<tUserStockRightBuyRecordEntity>;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public bool Delete(int keyValue)
        {
           	return this.BaseRepository().Delete<tUserStockRightBuyRecordEntity>(t => t.recordId == keyValue)>0;
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public bool SubmitForm(tUserStockRightBuyRecordEntity entity, int keyValue)
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

        //购买基金
        public bool BuyStock(tUserStockRightBuyRecordEntity entity, out string ret)
        {
            ret = this.BaseRepository().ExecuteByProc<string>("proc_BuyStockRight", new
            {
                @ret = "",
                entity.mbId,
                entity.shares,
                entity.ip,
                entity.client
            });

            return ret.IsEmpty();
        }
    }
}