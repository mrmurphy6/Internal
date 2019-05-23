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
    //tUserInvestRecord
    public class tUserInvestRecordBLL
    {
        static tUserInvestRecordDAL dal = new tUserInvestRecordDAL();

        public static tUserInvestRecordBLL Instance
        {
            get
            {
                return Autofacs<tUserInvestRecordBLL>.IocAutofac();
            }
        }

        public tUserInvestRecordEntity GetModel(int keyValue)
        {
            return dal.GetModel(keyValue);
        }


        public tUserInvestRecordEntity GetModel(Expression<Func<tUserInvestRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindEntity<tUserInvestRecordEntity>(condition);
        }

        /// <summary>
        /// 获取某会员所有的指定日期未结算的投资记录
        /// </summary>
        /// <param name="settleDate"></param>
        /// <returns></returns>
        public List<tUserInvestRecordEntity> GetOneMemberOfHaveUnSettledInvestRecord(string settleDate)
        {
            return dal.GetOneMemberOfHaveUnSettledInvestRecord(settleDate);
        }


        public List<tUserInvestRecordEntity> GetList(Pagination pagination)
        {
            return dal.GetList(pagination);
        }

        public List<tUserInvestRecordEntity> GetList(Expression<Func<tUserInvestRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindList<tUserInvestRecordEntity>(condition) as List<tUserInvestRecordEntity>;
        }

        public List<tUserInvestRecordEntity> GetList(Expression<Func<tUserInvestRecordEntity, bool>> condition, Pagination pagination)
        {
            return dal.BaseRepository().FindList<tUserInvestRecordEntity>(condition, pagination) as List<tUserInvestRecordEntity>;
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
        public bool SubmitForm(tUserInvestRecordEntity entity, int keyValue)
        {
            return dal.SubmitForm(entity, keyValue);
        }

        //投资
        public bool Invest(tUserInvestRecordEntity entity, out string ret)
        {
            return dal.Invest(entity, out ret);
        }

        //结算
        public bool Settle(tUserInvestRecordEntity entity, string settledate, out string ret)
        {
            return dal.Settle(entity, settledate, out ret);
        }
    }
}