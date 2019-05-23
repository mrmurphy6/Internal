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
    //tUserWithdrawRecord
    public class tUserWithdrawRecordBLL
    {
        static tUserWithdrawRecordDAL dal = new tUserWithdrawRecordDAL();

        public static tUserWithdrawRecordBLL Instance
        {
            get
            {
                return Autofacs<tUserWithdrawRecordBLL>.IocAutofac();
            }
        }

        public tUserWithdrawRecordEntity GetModel(int keyValue)
        {
            return dal.GetModel(keyValue);
        }

        public tUserWithdrawRecordEntity GetModel(Expression<Func<tUserWithdrawRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindEntity<tUserWithdrawRecordEntity>(condition);
        }


        public List<tUserWithdrawRecordEntity> GetList(Pagination pagination)
        {
            return dal.GetList(pagination);
        }

        public List<tUserWithdrawRecordEntity> GetList(Expression<Func<tUserWithdrawRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindList<tUserWithdrawRecordEntity>(condition) as List<tUserWithdrawRecordEntity>;
        }

        public List<tUserWithdrawRecordEntity> GetList(Expression<Func<tUserWithdrawRecordEntity, bool>> condition, Pagination pagination)
        {
            return dal.BaseRepository().FindList<tUserWithdrawRecordEntity>(condition, pagination) as List<tUserWithdrawRecordEntity>;
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
        public bool SubmitForm(tUserWithdrawRecordEntity entity, int keyValue)
        {
            return dal.SubmitForm(entity, keyValue);
        }

        //提现
        public bool Withdraw(tUserWithdrawRecordEntity entity, out string ret)
        {
            return dal.Withdraw(entity, out ret);
        }
    }
}