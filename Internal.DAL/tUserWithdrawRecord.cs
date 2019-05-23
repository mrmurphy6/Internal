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
	 	//tUserWithdrawRecord
		public class tUserWithdrawRecordDAL:RepositoryFactory
	{
		/// <summary>
        /// 获取单体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public tUserWithdrawRecordEntity GetModel(int keyValue)
        {
            return this.BaseRepository().FindEntity<tUserWithdrawRecordEntity>(keyValue);
        }

        public List<tUserWithdrawRecordEntity> GetList(Pagination pagination)
        {
            List<SqlParameter> param;
            string where = SearchHelper.AnalysisSearchModelMosaicSql(pagination.WhereSearchItems, false, out param, "and", null);

            return this.BaseRepository().FindList<tUserWithdrawRecordEntity>(string.Format("select * from view_tUserWithdrawRecord where 1=1 {0}", where), pagination) as List<tUserWithdrawRecordEntity>;
        }

        //提现
        public bool Withdraw(tUserWithdrawRecordEntity entity, out string ret)
        {
            ret = this.BaseRepository().ExecuteByProc<string>("proc_Withdraw", new
            {
                @ret = "",
                @mbId = entity.mbId,
                @bankId = entity.bankId,
                @amount = entity.withdrawAmount,
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
           	return this.BaseRepository().Delete<tUserWithdrawRecordEntity>(t => t.recordId == keyValue)>0;
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public bool SubmitForm(tUserWithdrawRecordEntity entity, int keyValue)
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