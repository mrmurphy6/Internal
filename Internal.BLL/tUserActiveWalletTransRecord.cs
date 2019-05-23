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
    //tUserActiveWalletTransRecord
    public class tUserActiveWalletTransRecordBLL
    {
        static tUserActiveWalletTransRecordDAL dal = new tUserActiveWalletTransRecordDAL();

        public static tUserActiveWalletTransRecordBLL Instance
        {
            get
            {
                return Autofacs<tUserActiveWalletTransRecordBLL>.IocAutofac();
            }
        }

        public tUserActiveWalletTransRecordEntity GetModel(int keyValue)
        {
            return dal.GetModel(keyValue);
        }

        public tUserActiveWalletTransRecordEntity GetModel(Expression<Func<tUserActiveWalletTransRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindEntity<tUserActiveWalletTransRecordEntity>(condition);
        }

        public List<tUserActiveWalletTransRecordEntity> GetList(Expression<Func<tUserActiveWalletTransRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindList<tUserActiveWalletTransRecordEntity>(condition) as List<tUserActiveWalletTransRecordEntity>;
        }

        public List<tUserActiveWalletTransRecordEntity> GetList(Expression<Func<tUserActiveWalletTransRecordEntity, bool>> condition, Pagination pagination)
        {
            return dal.BaseRepository().FindList<tUserActiveWalletTransRecordEntity>(condition, pagination) as List<tUserActiveWalletTransRecordEntity>;
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
        public bool SubmitForm(tUserActiveWalletTransRecordEntity entity, int keyValue)
        {
            return dal.SubmitForm(entity, keyValue);
        }

        //激活币钱包互转
        public bool Trans(tUserActiveWalletTransRecordEntity entity, out string ret)
        {
            ret = "";
            return dal.Trans(entity,out ret);
        }
    }
}