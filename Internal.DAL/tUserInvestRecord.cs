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
    //tUserInvestRecord
    public class tUserInvestRecordDAL : RepositoryFactory
    {
        /// <summary>
        /// 获取单体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public tUserInvestRecordEntity GetModel(int keyValue)
        {
            return this.BaseRepository().FindEntity<tUserInvestRecordEntity>(keyValue);
        }

        public List<tUserInvestRecordEntity> GetOneMemberOfHaveUnSettledInvestRecord(string settleDate)
        {
            List<tUserInvestRecordEntity> list = this.BaseRepository().ExecuteByProc<List<tUserInvestRecordEntity>>("proc_GetOneMemberOfHaveUnSettledInvestRecord", new
            {
                @settledate = settleDate
            });

            return list;
        }

        public List<tUserInvestRecordEntity> GetList(Pagination pagination)
        {
            List<SqlParameter> param;
            string where = SearchHelper.AnalysisSearchModelMosaicSql(pagination.WhereSearchItems, false, out param, "and", null);

            return this.BaseRepository().FindList<tUserInvestRecordEntity>(string.Format("select * from view_tUserInvestRecord where 1=1 {0}", where), pagination) as List<tUserInvestRecordEntity>;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public bool Delete(int keyValue)
        {
            return this.BaseRepository().Delete<tUserInvestRecordEntity>(t => t.recordId == keyValue) > 0;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public bool SubmitForm(tUserInvestRecordEntity entity, int keyValue)
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

        //投资
        public bool Invest(tUserInvestRecordEntity entity, out string ret)
        {
            ret = this.BaseRepository().ExecuteByProc<string>("proc_Invest", new
            {
                @ret = "",
                @mbId = entity.mbId,
                @amount = entity.investAmount,
                @ip = entity.investIp,
                @client = entity.investClient
            });

            return ret.IsEmpty();
        }

        //结算
        public bool Settle(tUserInvestRecordEntity entity,string settledate, out string ret)
        {
            ret = this.BaseRepository().ExecuteByProc<string>("proc_SettleInvestRecord", new
            {
                @ret = "",
                @settledate = settledate,
                @investrecordId = entity.recordId
            });

            return ret.IsEmpty();
        }
    }
}