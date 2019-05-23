using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DbData;
using Common;
using Internal.Entity;

namespace Internal.DAL 
{
	 	//tZBBonusRatioConfig
		public class tZBBonusRatioConfigDAL:RepositoryFactory
	{
		/// <summary>
        /// 获取单体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public tZBBonusRatioConfigEntity GetModel(int keyValue)
        {
            return this.BaseRepository().FindEntity<tZBBonusRatioConfigEntity>(keyValue);
        }
        
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public bool Delete(int keyValue)
        {
           	return this.BaseRepository().Delete<tZBBonusRatioConfigEntity>(t => t.configId == keyValue)>0;
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public bool SubmitForm(tZBBonusRatioConfigEntity entity, int keyValue)
        {
            entity.modifyTime = DateTime.Now;
            if (keyValue > 0)
            {
                return this.BaseRepository().Update(entity) > 0;
            }
            else
            {
                entity.configState = YesNoEnum.Yes.GetHashCode();
                entity.createTime = DateTime.Now;
                return this.BaseRepository().Insert(entity) > 0;
            }
        }
	}
}