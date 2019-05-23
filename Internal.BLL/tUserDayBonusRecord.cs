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
	 	//tUserDayBonusRecord
		public class tUserDayBonusRecordBLL
	{
   		static tUserDayBonusRecordDAL dal = new tUserDayBonusRecordDAL();

        public static tUserDayBonusRecordBLL Instance
        {
            get {
                return Autofacs<tUserDayBonusRecordBLL>.IocAutofac();
            }
        }
		
		public tUserDayBonusRecordEntity GetModel(int keyValue)
        {
            return dal.GetModel(keyValue);
        }
        
        public tUserDayBonusRecordEntity GetModel(Expression<Func<tUserDayBonusRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindEntity<tUserDayBonusRecordEntity>(condition);
        }

        public List<tUserDayBonusRecordEntity> GetList(Expression<Func<tUserDayBonusRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindList<tUserDayBonusRecordEntity>(condition) as List<tUserDayBonusRecordEntity>;
        }
        public List<tUserDayBonusRecordEntity> GetList(Pagination pagination)
        {
            return dal.GetList(pagination);
        }

        public List<tUserDayBonusRecordEntity> GetList(Expression<Func<tUserDayBonusRecordEntity, bool>> condition, Pagination pagination)
        {
            return dal.BaseRepository().FindList<tUserDayBonusRecordEntity>(condition,pagination) as List<tUserDayBonusRecordEntity>;
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
        public bool SubmitForm(tUserDayBonusRecordEntity entity, int keyValue)
        {
            return dal.SubmitForm(entity,keyValue);
        }
	}
}