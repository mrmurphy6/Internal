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
	 	//tZBBonusRatioConfig
		public class tZBBonusRatioConfigBLL
	{
   		static tZBBonusRatioConfigDAL dal = new tZBBonusRatioConfigDAL();

        public static tZBBonusRatioConfigBLL Instance
        {
            get {
                return Autofacs<tZBBonusRatioConfigBLL>.IocAutofac();
            }
        }
		
		public tZBBonusRatioConfigEntity GetModel(int keyValue)
        {
            return dal.GetModel(keyValue);
        }
        
        public tZBBonusRatioConfigEntity GetModel(Expression<Func<tZBBonusRatioConfigEntity, bool>> condition)
        {
            return dal.BaseRepository().FindEntity<tZBBonusRatioConfigEntity>(condition);
        }

        public List<tZBBonusRatioConfigEntity> GetList(Expression<Func<tZBBonusRatioConfigEntity, bool>> condition)
        {
            return dal.BaseRepository().FindList<tZBBonusRatioConfigEntity>(condition) as List<tZBBonusRatioConfigEntity>;
        } 

        public List<tZBBonusRatioConfigEntity> GetList(Expression<Func<tZBBonusRatioConfigEntity, bool>> condition, Pagination pagination)
        {
            return dal.BaseRepository().FindList<tZBBonusRatioConfigEntity>(condition,pagination) as List<tZBBonusRatioConfigEntity>;
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
        public bool SubmitForm(tZBBonusRatioConfigEntity entity, int keyValue)
        {
            return dal.SubmitForm(entity,keyValue);
        }
	}
}