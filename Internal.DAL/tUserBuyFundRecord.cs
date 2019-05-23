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
    //tUserBuyFundRecord
    public class tUserBuyFundRecordDAL : RepositoryFactory
    {
        /// <summary>
        /// 获取单体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public tUserBuyFundRecordEntity GetModel(int keyValue)
        {
            return this.BaseRepository().FindEntity<tUserBuyFundRecordEntity>(keyValue);
        }

        public tUserBuyFundRecordEntity GetOneExpiredFundRecord()
        {
            tUserBuyFundRecordEntity entity = this.BaseRepository().ExecuteByProc<tUserBuyFundRecordEntity>("proc_GetOneExpiredFundRecord");

            return entity;
        }

        public List<tUserBuyFundRecordEntity> GetList(Pagination pagination)
        {
            List<SqlParameter> param;
            string where = SearchHelper.AnalysisSearchModelMosaicSql(pagination.WhereSearchItems, false, out param, "and", null);

            return this.BaseRepository().FindList<tUserBuyFundRecordEntity>(string.Format("select * from view_tUserBuyFundRecord where 1=1 {0}", where), pagination) as List<tUserBuyFundRecordEntity>;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public bool Delete(int keyValue)
        {
            return this.BaseRepository().Delete<tUserBuyFundRecordEntity>(t => t.recordId == keyValue) > 0;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public bool SubmitForm(tUserBuyFundRecordEntity entity, int keyValue)
        {
            if (keyValue > 0)
            {
                return this.BaseRepository().Update(entity) > 0;
            }
            else
            {
                return this.BaseRepository().Insert(entity) > 0;
            }
        }

        //购买基金
        public bool BuyFund(tUserBuyFundRecordEntity entity, out string ret)
        {
            ret = this.BaseRepository().ExecuteByProc<string>("proc_BuyFund", new
            {
                @ret = "",
                entity.mbId,
                entity.fundId,
                entity.periodId,
                @amount = entity.buyAmount,
                entity.ip,
                entity.client
            });

            return ret.IsEmpty();
        }

        public bool Settle(tUserBuyFundRecordEntity entity, out string ret)
        {
            ret = this.BaseRepository().ExecuteByProc<string>("proc_SettleFundRecord", new
            {
                @ret = "",
                entity.recordId,
                entity.mbId
            });

            return ret.IsEmpty();
        }
    }
}