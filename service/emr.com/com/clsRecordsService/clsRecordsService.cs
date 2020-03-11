using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using System.Data;
using weCare.Core.Entity; 

namespace com.digitalwave.clsRecordsService
{
	/// <summary>
	/// 操作多个记录类型中间件的父类。
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public abstract class clsRecordsService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{

		/// <summary>
		///  获取指定记录的内容。
		/// </summary>
        /// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_objTansDataInfoArr">返回的记录内容</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetTransDataInfoArr(
			string p_strInPatientID,
			string p_strInPatientDate,
			out clsTransDataInfo[] p_objTansDataInfoArr)
		{
			p_objTansDataInfoArr = null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRecordsService", "m_lngGetTransDataInfoArr");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //参数检查                     
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                lngRes = m_lngGetTransDataInfoArrWithServ(p_strInPatientID, p_strInPatientDate, objHRPServ, out p_objTansDataInfoArr);

            }
            catch (Exception objEx)
            {
                 com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally 
            {
                //objHRPServ.Dispose();
            }
			//返回
			return lngRes;
        }
        #region 不用
        /// <summary>
        ///  获取指定记录的内容（茶山有摄入排出的护理表单用）。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strFormID">表单ID</param>
        /// <param name="p_objTansDataInfoArr">返回的记录内容</param>
        /// <returns></returns>
        //[AutoComplete]
        //public long m_lngGetTransDataInfoArr(
        //    string p_strInPatientID,
        //    string p_strInPatientDate,string p_strFormID,
        //    out clsTransDataInfo[] p_objTansDataInfoArr)
        //{
        //    p_objTansDataInfoArr = null;
        //    long lngRes = 0;
        //    clsHRPTableService objHRPServ = new clsHRPTableService();
        //    try
        //    {
        //        //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRecordsService", "m_lngGetTransDataInfoArr");
        //        //if (lngCheckRes <= 0)
        //        //return lngCheckRes;

        //        //参数检查                     
        //        if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
        //            return (long)enmOperationResult.Parameter_Error;

        //        lngRes = m_lngGetTransDataInfoArrWithServ(p_strInPatientID, p_strInPatientDate,p_strFormID, objHRPServ, out p_objTansDataInfoArr);

        //    }
        //    catch (Exception objEx)
        //    {
        //        com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
        //        bool blnRes = objLogger.LogError(objEx);
        //    }
        //    finally
        //    {
        //        //objHRPServ.Dispose();
        //    }
        //    //返回
        //    return lngRes;
        //}
        #endregion
        /// <summary>
        ///  获取指定记录的内容,使用住院登记号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_objTansDataInfoArr">返回的记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetTransDataInfoArr(
            string p_strRegisterId,out clsTransDataInfo[] p_objTansDataInfoArr)
        {
            p_objTansDataInfoArr = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRecordsService", "m_lngGetTransDataInfoArr");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                //参数检查                     
                if (string.IsNullOrEmpty(p_strRegisterId))
                    return (long)enmOperationResult.Parameter_Error;

                lngRes = m_lngGetTransDataInfoArrWithServ(p_strRegisterId,1, out p_objTansDataInfoArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPServ = null;
                //objHRPServ.Dispose();
            }
            //返回
            return lngRes;
        }


		/// <summary>
		/// 获取指定记录的内容
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_objHRPServ"></param>
		/// <param name="p_objTansDataInfoArr">返回的记录内容</param>
		/// <returns></returns>
        [AutoComplete]
        protected virtual long m_lngGetTransDataInfoArrWithServ(string p_strInPatientID,
            string p_strInPatientDate,
            clsHRPTableService p_objHRPServ,
            out clsTransDataInfo[] p_objTansDataInfoArr)
        {
            p_objTansDataInfoArr = null;
            return 0;
        }
        #region 不用
        /// <summary>
        /// 获取指定记录的内容（茶山有摄入排出的护理表单用）
        /// </summary>
        /// <param name="p_strInPatientID">住院号</param>
        /// <param name="p_strInPatientDate">入院日期</param>
        /// <param name="p_strFormID">表单ID</param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objTansDataInfoArr">返回的记录内容</param>
        /// <returns></returns>
        //[AutoComplete]
        //protected virtual long m_lngGetTransDataInfoArrWithServ(string p_strInPatientID,
        //    string p_strInPatientDate,string p_strFormID,
        //    clsHRPTableService p_objHRPServ,
        //    out clsTransDataInfo[] p_objTansDataInfoArr)
        //{
        //    p_objTansDataInfoArr = null;
        //    return 0;
        //}
        #endregion
        /// <summary>
        /// 获取指定记录的内容,使用住院登记号
        /// </summary>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strStatus">状态0＝作废记录；1＝正常记录</param>
        /// <param name="p_objTansDataInfoArr">返回的记录内容</param>
        /// <returns></returns>
        [AutoComplete]
        protected virtual long m_lngGetTransDataInfoArrWithServ(string p_strRegisterId,
            int p_intStatus,
            out clsTransDataInfo[] p_objTansDataInfoArr)
        {
            p_objTansDataInfoArr = null;
            return 0;
        }


		/// <summary>
		///  查看当前记录是否最新的记录。
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		protected abstract long m_lngCheckLastModifyRecord(int p_intRecordType,
			clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo);


		/// <summary>
		/// 删除记录。
		/// 1.生成 HRPServ。
		/// 2.删除记录。(m_lngDeleteRecordWithServ)
		/// </summary>
		/// <param name="p_intRecordType">当前要删除的记录</param>
		/// <param name="p_objRecordContent">当前记录内容</param>
		/// <param name="p_objModifyInfo">若当前要删除的记录不是最新的记录,返回该最新记录的操作信息,否则为空</param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngDeleteRecord(
			int p_intRecordType,
			clsTrackRecordContent p_objRecordContent,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo = null;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRecordsService", "m_lngDeleteRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //参数检查                     
                if (p_intRecordType < 0 || p_objRecordContent == null)
                    return (long)enmOperationResult.Parameter_Error;
                lngRes = m_lngDeleteRecordWithServ(p_intRecordType, p_objRecordContent, objHRPServ, out p_objModifyInfo);

            }
            catch (Exception objEx)
            {
                 com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//返回
			return lngRes;
					}

		/// <summary>
		/// 删除记录。
		/// 1.查看当前记录是否最新的记录。（m_lngCheckLastModifyRecord）
		/// 2.把记录从数据中“删除”。(m_lngDeleteRecord2DB)
		/// </summary>
		/// <param name="p_intRecordType">当前要删除的记录</param>
		/// <param name="p_objRecordContent">当前记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo">若当前要删除的记录不是最新的记录,返回该最新记录的操作信息,否则为空</param>
		/// <returns></returns>
		[AutoComplete]
		protected long m_lngDeleteRecordWithServ(int p_intRecordType,
			clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo = null;
			long lngRes = 0;
			try
			{
				if(p_intRecordType < 0 || p_objRecordContent == null || p_objHRPServ == null)
					return (long)enmOperationResult.Parameter_Error;
			
				lngRes = m_lngCheckLastModifyRecord(p_intRecordType,p_objRecordContent,p_objHRPServ,out p_objModifyInfo);           
			
				if(lngRes <= 0)
					return lngRes;                     
			
				lngRes= m_lngDeleteRecord2DB(p_intRecordType,p_objRecordContent,p_objHRPServ);

			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//返回
			return lngRes;
		}

		/// <summary>
		///  把记录从数据中“删除”。
		/// </summary>
		/// <param name="p_intRecordType">当前要删除的记录</param>
		/// <param name="p_objRecordContent">当前记录内容</param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected abstract long m_lngDeleteRecord2DB(int p_intRecordType,
			clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ);

		/// <summary>
		/// 获取打印信息。
		/// 1.获取打印内容：如果输入参数p_dtmModifyDate不是最新的ModifyDate，输出变量 p_objContent
		///   会存放最新的内容；否则，输出变量为null。
		/// 2.获取打印时间：输出变量 p_dtmFirstPrintDate 存放首次打印时间。p_blnIsFirstPrint标记
		///   是否首次打印，如果是为true，客户端在打印后需要保存p_dtmFirstPrintDate到数据库。
		/// </summary>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_objTransDataInfoArr"></param>
		/// <param name="p_dtmFirstPrintDateArr"></param>
		/// <param name="p_blnIsFirstPrintArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetPrintInfo(
			string p_strInPatientID,
			string p_strInPatientDate,
			out clsTransDataInfo[] p_objTransDataInfoArr,
			out DateTime[] p_dtmFirstPrintDateArr,
			out bool[] p_blnIsFirstPrintArr)
		{
			p_objTransDataInfoArr = null;
			p_dtmFirstPrintDateArr = null;        
			p_blnIsFirstPrintArr = null;
			long lngRes = 0;
            clsHRPTableService objHRP = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRecordsService", "m_lngGetPrintInfo");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //参数检查                     
                if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "")
                    return (long)enmOperationResult.Parameter_Error;

                 lngRes = m_lngGetTransDataInfoArrWithServ(p_strInPatientID, p_strInPatientDate, objHRP, out p_objTransDataInfoArr);

                if (lngRes <= 0)
                    return lngRes;

                p_dtmFirstPrintDateArr = new DateTime[p_objTransDataInfoArr.Length];
                p_blnIsFirstPrintArr = new bool[p_objTransDataInfoArr.Length];

                for (int i = 0; i < p_objTransDataInfoArr.Length; i++)
                {
                    //判断p_objTransDataInfoArr[i].m_dtmFirstPrintDate是否为DateTime.MinValue
                    //如果是
                    if (p_objTransDataInfoArr[i].m_objRecordContent.m_dtmFirstPrintDate == DateTime.MinValue)
                    {
                        p_dtmFirstPrintDateArr[i] = DateTime.Now;
                        p_blnIsFirstPrintArr[i] = true;
                    }
                    else
                    {
                        //如果不是
                        p_dtmFirstPrintDateArr[i] = p_objTransDataInfoArr[i].m_objRecordContent.m_dtmFirstPrintDate;
                        p_blnIsFirstPrintArr[i] = false;
                    }
                }


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRP.Dispose();

            }
			//返回
			return lngRes;
		}
        /// <summary>
        /// 获取打印信息，根据住院登记号。
        /// 1.获取打印内容：如果输入参数p_dtmModifyDate不是最新的ModifyDate，输出变量 p_objContent
        ///   会存放最新的内容；否则，输出变量为null。
        /// 2.获取打印时间：输出变量 p_dtmFirstPrintDate 存放首次打印时间。p_blnIsFirstPrint标记
        ///   是否首次打印，如果是为true，客户端在打印后需要保存p_dtmFirstPrintDate到数据库。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_strCreatedDate"></param>
        /// <param name="p_strStatus"></param>
        /// <param name="p_objTransDataInfoArr"></param>
        /// <param name="p_dtmFirstPrintDateArr"></param>
        /// <param name="p_blnIsFirstPrintArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPrintInfo(
            string p_strRegisterId,int p_intStatus,
            out clsTransDataInfo[] p_objTransDataInfoArr,
            out DateTime[] p_dtmFirstPrintDateArr,
            out bool[] p_blnIsFirstPrintArr)
        {
            p_objTransDataInfoArr = null;
            p_dtmFirstPrintDateArr = null;
            p_blnIsFirstPrintArr = null;
            long lngRes = 0;
            clsHRPTableService objHRP = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsRecordsService", "m_lngGetPrintInfo");
                //if (lngCheckRes <= 0)
                //return lngCheckRes;

                //参数检查                     
                if (string.IsNullOrEmpty(p_strRegisterId))
                    return (long)enmOperationResult.Parameter_Error;

                lngRes = m_lngGetTransDataInfoArrWithServ(p_strRegisterId, p_intStatus, out p_objTransDataInfoArr);

                if (lngRes <= 0)
                    return lngRes;

                p_dtmFirstPrintDateArr = new DateTime[p_objTransDataInfoArr.Length];
                p_blnIsFirstPrintArr = new bool[p_objTransDataInfoArr.Length];

                for (int i = 0 ; i < p_objTransDataInfoArr.Length ; i++)
                {
                    //判断p_objTransDataInfoArr[i].m_dtmFirstPrintDate是否为DateTime.MinValue
                    //如果是
                    if (p_objTransDataInfoArr[i].m_objRecordContent.m_dtmFirstPrintDate == DateTime.MinValue)
                    {
                        p_dtmFirstPrintDateArr[i] = DateTime.Now;
                        p_blnIsFirstPrintArr[i] = true;
                    }
                    else
                    {
                        //如果不是
                        p_dtmFirstPrintDateArr[i] = p_objTransDataInfoArr[i].m_objRecordContent.m_dtmFirstPrintDate;
                        p_blnIsFirstPrintArr[i] = false;
                    }
                }


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRP.Dispose();

            }
            //返回
            return lngRes;
        }

		/// <summary>
		/// 更新数据库中的首次打印时间。
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_intRecordTypeArr"></param>
		/// <param name="p_dtmOpenDateArr"></param>
		/// <param name="p_dtmFirstPrintDate"></param>
		/// <returns></returns>
        [AutoComplete]
        public virtual long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            int[] p_intRecordTypeArr,
            DateTime[] p_dtmOpenDateArr,
            DateTime p_dtmFirstPrintDate)
        {
            return 0;
        }
        /// <summary>
        /// 更新数据库中的首次打印时间,根据住院登记号。
        /// </summary>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_strCreatedDate">创建日期</param>
        /// <param name="p_strStatus"></param>
        /// <param name="p_intRecordTypeArr"></param>
        /// <param name="p_dtmOpenDateArr"></param>
        /// <param name="p_dtmFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public virtual long m_lngUpdateFirstPrintDate(
            string p_strRegisterId,
            int[] p_intRecordTypeArr,
            DateTime[] p_dtmCreatedDateArr,
            DateTime p_dtmFirstPrintDate)
        {
            return 0;
        }

        /// <summary>
        /// 获取一次住院全部作废记录
        /// </summary>
        /// <param name="p_strInpatientId"></param>
        /// <param name="p_dtmInpatientDate"></param>
        /// <param name="p_objInactiveRecordInfoArr"></param>
        /// <returns></returns>
        public virtual long m_lngGetAllInactiveInfo(string p_strSQL, string p_strInpatientId, DateTime p_dtmInpatientDate, out clsInactiveRecordInfo_VO[] p_objInactiveRecordInfoArr)
        {
            p_objInactiveRecordInfoArr = null;
            if (string.IsNullOrEmpty(p_strInpatientId) || p_dtmInpatientDate == DateTime.MinValue || string.IsNullOrEmpty(p_strSQL)) return -1;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                DataTable dtExist = new DataTable();

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInpatientId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmInpatientDate;

                lngRes = objHRPServ.lngGetDataTableWithParameters(p_strSQL, ref dtExist, objDPArr);
                if (lngRes > 0 && dtExist.Rows.Count > 0)
                {
                    p_objInactiveRecordInfoArr = new clsInactiveRecordInfo_VO[dtExist.Rows.Count];
                    DataRow objRow = null;
                    DateTime dtmTemp = DateTime.MinValue;
                    for (int i = 0; i < dtExist.Rows.Count; i++)
                    {
                        objRow = dtExist.Rows[i];
                        clsInactiveRecordInfo_VO objInfo = new clsInactiveRecordInfo_VO();
                        objInfo.m_StrInpatientId = p_strInpatientId;
                        objInfo.m_DtmInpatientDate = p_dtmInpatientDate;
                        if (DateTime.TryParse(objRow["createdate"].ToString(), out dtmTemp))
                            objInfo.m_DtmCreatedDate = dtmTemp;
                        if (DateTime.TryParse(objRow["opendate"].ToString(), out dtmTemp))
                            objInfo.m_DtmOpenDate = dtmTemp;
                        if (DateTime.TryParse(objRow["deactiveddate"].ToString(), out dtmTemp))
                            objInfo.m_DtmDeactiveDate = dtmTemp;
                        objInfo.m_StrCreatedUser = objRow["createdusername"].ToString();
                        objInfo.m_StrDeactiveUser = objRow["deactiveusername"].ToString();
                        p_objInactiveRecordInfoArr[i] = objInfo;
                    }
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 转科信息查询
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_intdetpID"></param>
        /// <param name="p_objModifyInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        public virtual long m_lngGetTransferInfo(
                  string p_strRegisterID, string p_intdetpID, out DateTime[] p_objModifyInfo)
        {
            p_objModifyInfo = null;
            return 0;
        }
	}
}
