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
    //tUserBuyFundRecord
    public class tUserBuyFundRecordBLL
    {
        static tUserBuyFundRecordDAL dal = new tUserBuyFundRecordDAL();

        public static tUserBuyFundRecordBLL Instance
        {
            get
            {
                return Autofacs<tUserBuyFundRecordBLL>.IocAutofac();
            }
        }

        public tUserBuyFundRecordEntity GetModel(int keyValue)
        {
            return dal.GetModel(keyValue);
        }

        public tUserBuyFundRecordEntity GetModel(Expression<Func<tUserBuyFundRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindEntity<tUserBuyFundRecordEntity>(condition);
        }

        /// <summary>
        /// 获取一条到期未结算的基金投购买记录
        /// </summary>
        /// <returns></returns>
        public tUserBuyFundRecordEntity GetOneExpiredFundRecord()
        {
            return dal.GetOneExpiredFundRecord();
        }

        public List<tUserBuyFundRecordEntity> GetList(Pagination pagination)
        {
            return dal.GetList(pagination);
        }

        public List<tUserBuyFundRecordEntity> GetList(Expression<Func<tUserBuyFundRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindList<tUserBuyFundRecordEntity>(condition) as List<tUserBuyFundRecordEntity>;
        }

        public List<tUserBuyFundRecordEntity> GetList(Expression<Func<tUserBuyFundRecordEntity, bool>> condition, Pagination pagination)
        {
            return dal.BaseRepository().FindList<tUserBuyFundRecordEntity>(condition, pagination) as List<tUserBuyFundRecordEntity>;
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
        public bool SubmitForm(tUserBuyFundRecordEntity entity, int keyValue)
        {
            return dal.SubmitForm(entity, keyValue);
        }

        //购买基金
        public bool BuyFund(tUserBuyFundRecordEntity entity, out string ret)
        {
            return dal.BuyFund(entity, out ret);
        }

        /// <summary>
        /// 基金到期结算
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool Settle(tUserBuyFundRecordEntity entity, out string ret)
        {
            return dal.Settle(entity, out ret);
        }
    }
}