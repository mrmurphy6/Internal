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
	 	//tUserZBDayBonusRecord
		public class tUserZBDayBonusRecordBLL
	{
   		static tUserZBDayBonusRecordDAL dal = new tUserZBDayBonusRecordDAL();

        public static tUserZBDayBonusRecordBLL Instance
        {
            get {
                return Autofacs<tUserZBDayBonusRecordBLL>.IocAutofac();
            }
        }
		
		public tUserZBDayBonusRecordEntity GetModel(int keyValue)
        {
            return dal.GetModel(keyValue);
        }
        
        public tUserZBDayBonusRecordEntity GetModel(Expression<Func<tUserZBDayBonusRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindEntity<tUserZBDayBonusRecordEntity>(condition);
        }

        public List<tUserZBDayBonusRecordEntity> GetList(Expression<Func<tUserZBDayBonusRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindList<tUserZBDayBonusRecordEntity>(condition) as List<tUserZBDayBonusRecordEntity>;
        }
        public List<tUserZBDayBonusRecordEntity> GetList(Pagination pagination)
        {
            return dal.GetList(pagination);
        }

        public List<tUserZBDayBonusRecordEntity> GetList(Expression<Func<tUserZBDayBonusRecordEntity, bool>> condition, Pagination pagination)
        {
            return dal.BaseRepository().FindList<tUserZBDayBonusRecordEntity>(condition,pagination) as List<tUserZBDayBonusRecordEntity>;
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
        public bool SubmitForm(tUserZBDayBonusRecordEntity entity, int keyValue)
        {
            return dal.SubmitForm(entity,keyValue);
        }
	}
}