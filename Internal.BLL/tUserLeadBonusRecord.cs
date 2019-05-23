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
	 	//tUserLeadBonusRecord
		public class tUserLeadBonusRecordBLL
	{
   		static tUserLeadBonusRecordDAL dal = new tUserLeadBonusRecordDAL();

        public static tUserLeadBonusRecordBLL Instance
        {
            get {
                return Autofacs<tUserLeadBonusRecordBLL>.IocAutofac();
            }
        }
		
		public tUserLeadBonusRecordEntity GetModel(int keyValue)
        {
            return dal.GetModel(keyValue);
        }
        
        public tUserLeadBonusRecordEntity GetModel(Expression<Func<tUserLeadBonusRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindEntity<tUserLeadBonusRecordEntity>(condition);
        }

        public List<tUserLeadBonusRecordEntity> GetList(Expression<Func<tUserLeadBonusRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindList<tUserLeadBonusRecordEntity>(condition) as List<tUserLeadBonusRecordEntity>;
        }

        public List<tUserLeadBonusRecordEntity> GetList(Pagination pagination)
        {
            return dal.GetList(pagination);
        }
        public List<tUserLeadBonusRecordEntity> GetList(Expression<Func<tUserLeadBonusRecordEntity, bool>> condition, Pagination pagination)
        {
            return dal.BaseRepository().FindList<tUserLeadBonusRecordEntity>(condition,pagination) as List<tUserLeadBonusRecordEntity>;
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
        public bool SubmitForm(tUserLeadBonusRecordEntity entity, int keyValue)
        {
            return dal.SubmitForm(entity,keyValue);
        }
	}
}