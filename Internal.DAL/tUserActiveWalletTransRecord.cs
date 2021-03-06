﻿using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DbData;
using Common;
using Internal.Entity;

namespace Internal.DAL 
{
	 	//tUserActiveWalletTransRecord
		public class tUserActiveWalletTransRecordDAL:RepositoryFactory
	{
		/// <summary>
        /// 获取单体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public tUserActiveWalletTransRecordEntity GetModel(int keyValue)
        {
            return this.BaseRepository().FindEntity<tUserActiveWalletTransRecordEntity>(keyValue);
        }
        
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public bool Delete(int keyValue)
        {
           	return this.BaseRepository().Delete<tUserActiveWalletTransRecordEntity>(t => t.recordId == keyValue)>0;
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public bool SubmitForm(tUserActiveWalletTransRecordEntity entity, int keyValue)
        {
            if (keyValue>0      )
            {
               return this.BaseRepository().Update(entity)>0;
            }
            else
            {
                return this.BaseRepository().Insert(entity)>0;
            }
        }

        //激活币钱包互转
        public bool Trans(tUserActiveWalletTransRecordEntity entity, out string ret)
        {
            ret = this.BaseRepository().ExecuteByProc<string>("proc_ActiveWalletTrans", new
            {
                @ret = "",
                @fromMbId = entity.fromMbId,
                @toMbId = entity.toMbId,
                @amount = entity.transAmount,
                @ip = entity.ip,
                @client = entity.client
            });

            return ret.IsEmpty();
        }
	}
}