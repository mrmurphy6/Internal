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
    //tUserStockRightBuyRecord
    public class tUserStockRightBuyRecordBLL
    {
        static tUserStockRightBuyRecordDAL dal = new tUserStockRightBuyRecordDAL();

        public static tUserStockRightBuyRecordBLL Instance
        {
            get
            {
                return Autofacs<tUserStockRightBuyRecordBLL>.IocAutofac();
            }
        }

        public tUserStockRightBuyRecordEntity GetModel(int keyValue)
        {
            return dal.GetModel(keyValue);
        }

        public tUserStockRightBuyRecordEntity GetModel(Expression<Func<tUserStockRightBuyRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindEntity<tUserStockRightBuyRecordEntity>(condition);
        }

        public List<tUserStockRightBuyRecordEntity> GetList(Pagination pagination)
        {
            return dal.GetList(pagination);
        }

        public List<tUserStockRightBuyRecordEntity> GetList(Expression<Func<tUserStockRightBuyRecordEntity, bool>> condition)
        {
            return dal.BaseRepository().FindList<tUserStockRightBuyRecordEntity>(condition) as List<tUserStockRightBuyRecordEntity>;
        }

        public List<tUserStockRightBuyRecordEntity> GetList(Expression<Func<tUserStockRightBuyRecordEntity, bool>> condition, Pagination pagination)
        {
            return dal.BaseRepository().FindList<tUserStockRightBuyRecordEntity>(condition, pagination) as List<tUserStockRightBuyRecordEntity>;
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
        public bool SubmitForm(tUserStockRightBuyRecordEntity entity, int keyValue)
        {
            return dal.SubmitForm(entity, keyValue);
        }

        //购买
        public bool BuyStock(tUserStockRightBuyRecordEntity entity, out string ret)
        {
            return dal.BuyStock(entity, out ret);
        }
    }
}