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

namespace Internal.BLL
{
    //tUserRechargeRecord
    public class tUserRechargeRecordBLL
    {
        static tUserRechargeRecordDAL dal = new tUserRechargeRecordDAL();

        public static tUserRechargeRecordBLL Instance
        {
            get
            {
                return Autofacs<tUserRechargeRecordBLL>.IocAutofac();
            }
        }

        public tUserRechargeRecordEntity GetModel(int keyValue)
        {
            return dal.GetModel(keyValue);
        }

        public tUserRechargeRecordEntity GetModel(Expression<Func<tUserRechargeRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindEntity<tUserRechargeRecordEntity>(condition);
        }

        public List<tUserRechargeRecordEntity> GetList(Pagination pagination)
        {
            return dal.GetList(pagination);
        }

        public List<tUserRechargeRecordEntity> GetList(Expression<Func<tUserRechargeRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindList<tUserRechargeRecordEntity>(condition) as List<tUserRechargeRecordEntity>;
        }

        public List<tUserRechargeRecordEntity> GetList(Expression<Func<tUserRechargeRecordEntity, bool>> condition, Pagination pagination)
        {
            return dal.BaseRepository().FindList<tUserRechargeRecordEntity>(condition, pagination) as List<tUserRechargeRecordEntity>;
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
        public bool SubmitForm(tUserRechargeRecordEntity entity, int keyValue)
        {
            return dal.SubmitForm(entity, keyValue);
        }

        //充值
        public bool Recharge(tUserRechargeRecordEntity entity, out string ret)
        {
            return dal.Recharge(entity, out ret);
        }
    }
}