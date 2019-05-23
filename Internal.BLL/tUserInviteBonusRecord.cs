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
	 	//tUserInviteBonusRecord
		public class tUserInviteBonusRecordBLL
	{
   		static tUserInviteBonusRecordDAL dal = new tUserInviteBonusRecordDAL();

        public static tUserInviteBonusRecordBLL Instance
        {
            get {
                return Autofacs<tUserInviteBonusRecordBLL>.IocAutofac();
            }
        }
		
		public tUserInviteBonusRecordEntity GetModel(int keyValue)
        {
            return dal.GetModel(keyValue);
        }
        
        public tUserInviteBonusRecordEntity GetModel(Expression<Func<tUserInviteBonusRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindEntity<tUserInviteBonusRecordEntity>(condition);
        }

        public List<tUserInviteBonusRecordEntity> GetList(Expression<Func<tUserInviteBonusRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindList<tUserInviteBonusRecordEntity>(condition) as List<tUserInviteBonusRecordEntity>;
        }

        public List<tUserInviteBonusRecordEntity> GetList(Pagination pagination)
        {
            return dal.GetList(pagination);
        }

        public List<tUserInviteBonusRecordEntity> GetList(Expression<Func<tUserInviteBonusRecordEntity, bool>> condition, Pagination pagination)
        {
            return dal.BaseRepository().FindList<tUserInviteBonusRecordEntity>(condition,pagination) as List<tUserInviteBonusRecordEntity>;
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
        public bool SubmitForm(tUserInviteBonusRecordEntity entity, int keyValue)
        {
            return dal.SubmitForm(entity,keyValue);
        }
	}
}