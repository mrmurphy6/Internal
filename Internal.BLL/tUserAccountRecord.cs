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
    //tUserAccountRecord
    public class tUserAccountRecordBLL
    {
        static tUserAccountRecordDAL dal = new tUserAccountRecordDAL();

        public static tUserAccountRecordBLL Instance
        {
            get
            {
                return Autofacs<tUserAccountRecordBLL>.IocAutofac();
            }
        }

        public tUserAccountRecordEntity GetModel(int keyValue)
        {
            return dal.GetModel(keyValue);
        }

        public tUserAccountRecordEntity GetModel(Expression<Func<tUserAccountRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindEntity<tUserAccountRecordEntity>(condition);
        }
        public List<tUserAccountRecordEntity> GetList(Pagination pagination)
        {
            return dal.GetList(pagination);
        }

        public List<tUserAccountRecordEntity> GetList(Expression<Func<tUserAccountRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindList<tUserAccountRecordEntity>(condition) as List<tUserAccountRecordEntity>;
        }

        public List<tUserAccountRecordEntity> GetList(Expression<Func<tUserAccountRecordEntity, bool>> condition, Pagination pagination)
        {
            return dal.BaseRepository().FindList<tUserAccountRecordEntity>(condition, pagination) as List<tUserAccountRecordEntity>;
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
        public bool SubmitForm(tUserAccountRecordEntity entity, int keyValue)
        {
            return dal.SubmitForm(entity, keyValue);
        }
    }
}