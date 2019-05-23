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
    //tUserRechargeRecord
    public class tUserRechargeRecordDAL : RepositoryFactory
    {
        /// <summary>
        /// 获取单体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public tUserRechargeRecordEntity GetModel(int keyValue)
        {
            return this.BaseRepository().FindEntity<tUserRechargeRecordEntity>(keyValue);
        }

        public List<tUserRechargeRecordEntity> GetList(Pagination pagination)
        {
            List<SqlParameter> param;
            string where = SearchHelper.AnalysisSearchModelMosaicSql(pagination.WhereSearchItems, false, out param, "and", null);

            return this.BaseRepository().FindList<tUserRechargeRecordEntity>(string.Format("select * from view_tUserRechargeRecord where 1=1 {0}", where), pagination) as List<tUserRechargeRecordEntity>;
        }

        //充值
        public bool Recharge(tUserRechargeRecordEntity entity, out string ret)
        {
            ret = this.BaseRepository().ExecuteByProc<string>("proc_Recharge", new
            {
                @ret = "",
                @mbId = entity.mbId,
                @guid = entity.recordGuid,
                @img = entity.transferImg,
                @bankId = entity.bankId,
                @amount = entity.rechargeAmount,
                @ip = entity.ip,
                @client = entity.client
            });

            return ret.IsEmpty();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public bool Delete(int keyValue)
        {
            return this.BaseRepository().Delete<tUserRechargeRecordEntity>(t => t.recordId == keyValue) > 0;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public bool SubmitForm(tUserRechargeRecordEntity entity, int keyValue)
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
    }
}