using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Common;
using DbData;
using Internal.Entity;
using Internal.DAL;
using System.Linq.Expressions;

namespace Internal.BLL {
	 	//tUserFundSettleRecord
		public class tUserFundSettleRecordBLL
	{
   		static tUserFundSettleRecordDAL dal = new tUserFundSettleRecordDAL();

        public static tUserFundSettleRecordBLL Instance
        {
            get {
                return Autofacs<tUserFundSettleRecordBLL>.IocAutofac();
            }
        }
		
		public tUserFundSettleRecordEntity GetModel(int keyValue)
        {
            return dal.GetModel(keyValue);
        }
        
        public tUserFundSettleRecordEntity GetModel(Expression<Func<tUserFundSettleRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindEntity<tUserFundSettleRecordEntity>(condition);
        }

        public List<tUserFundSettleRecordEntity> GetList(Expression<Func<tUserFundSettleRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindList<tUserFundSettleRecordEntity>(condition) as List<tUserFundSettleRecordEntity>;
        } 

        public List<tUserFundSettleRecordEntity> GetList(Expression<Func<tUserFundSettleRecordEntity, bool>> condition, Pagination pagination)
        {
            return dal.BaseRepository().FindList<tUserFundSettleRecordEntity>(condition,pagination) as List<tUserFundSettleRecordEntity>;
        }        
        
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public bool Delete(int keyValue)
        {
            return dal.Delete(keyValue);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public bool SubmitForm(tUserFundSettleRecordEntity entity, int keyValue)
        {
            return dal.SubmitForm(entity,keyValue);
        }
	}
}