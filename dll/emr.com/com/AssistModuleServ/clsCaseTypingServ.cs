using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using weCare.Core.Entity;
//using CustomDataSet = com.digitalwave.emr.AssistModuleVO.CustomDataSet;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using ServMain = com.digitalwave.iCare.middletier.HRPService;//10g
//using Serv8i = com.digitalwave.iCare.middletier.HRPService_Orders;//8i
using SQLHelper=com.digitalwave.emr.AssistModuleSev.SQLHelper;
using com.digitalwave.Utility;

namespace com.digitalwave.emr.AssistModuleSev
{
    /// <summary>
    /// 病历分型中间件
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsCaseTypingServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
    { 
        /// <summary>
        /// 从8i数据库获取数据
        /// </summary>
        /// <param name="p_dtmStart"></param>
        /// <param name="p_dtmEnd"></param>
        /// <returns></returns>
        //[AutoComplete]
        //public CustomDataSet::clsCaseDataSet_GX m_objExecuteDataSetFor8iTable(DateTime p_dtmStart, DateTime p_dtmEnd)
        //{
        //    if (p_dtmStart == DateTime.MinValue || p_dtmEnd == DateTime.MinValue || p_dtmStart > p_dtmEnd) return null;
        //    long lngRes = 0;
        //    CustomDataSet::clsCaseDataSet_GX objDataSet = null;
        //    try
        //    {
        //        Serv8i.clsHRPTableService objHRP8i = new Serv8i.clsHRPTableService();
        //        objDataSet = new CustomDataSet::clsCaseDataSet_GX();
        //        DataTable dt2 = objDataSet.m_DtbPAT_VISIT;
        //        DataTable dt3 = objDataSet.m_DtbIDENTITY_DICT;
        //        DataTable dt4 = objDataSet.m_DtbCHARGE_TYPE_DICT;

        //        IDataParameter[] objSeqArr2 = null;
        //        objHRP8i.CreateDatabaseOledbParameter(2, out objSeqArr2);
        //        objSeqArr2[0].DbType = DbType.DateTime;
        //        objSeqArr2[0].Value = p_dtmStart;
        //        objSeqArr2[1].DbType = DbType.DateTime;
        //        objSeqArr2[1].Value = p_dtmEnd;

        //        lngRes = objHRP8i.lngGetDataTableWithParameters(SQLHelper::clsSqlStringHelper.s_strGetQueryPAT_VISIT, ref dt2, objSeqArr2);
        //        if (lngRes > 0)
        //        {
        //            lngRes = objHRP8i.lngGetDataTableWithoutParameters(SQLHelper::clsSqlStringHelper.s_strGetQueryIDENTITY_DIC, ref dt3);
        //            if (lngRes > 0)
        //                lngRes = objHRP8i.lngGetDataTableWithoutParameters(SQLHelper::clsSqlStringHelper.s_strGetQueryCHARGE_TYPE_DICT, ref dt4);
        //        }
        //        if (lngRes <= 0)
        //            objDataSet = null;
        //    }
        //    catch (Exception objEx)
        //    {
        //        clsLogText objLogger = new clsLogText();
        //        objLogger.LogDetailError(objEx, true);
        //    }
        //    return objDataSet;
        //}
        /// <summary>
        /// 从数据库获取首页数据
        /// </summary>
        /// <param name="p_dtmStart"></param>
        /// <param name="p_dtmEnd"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngExecuteDataForMain(DateTime p_dtmStart, DateTime p_dtmEnd,ref DataTable p_dtbResult)
        {
            p_dtbResult = null;
            if (p_dtmStart == DateTime.MinValue || p_dtmEnd == DateTime.MinValue || p_dtmStart > p_dtmEnd) return -1;
            long lngRes = 0;
            try
            {
                ServMain.clsHRPTableService objHRPMain = new ServMain.clsHRPTableService();

                IDataParameter[] objSeqArr1 = null;
                objHRPMain.CreateDatabaseParameter(2, out objSeqArr1);
                objSeqArr1[0].DbType = DbType.DateTime;
                objSeqArr1[0].Value = p_dtmStart;
                objSeqArr1[1].DbType = DbType.DateTime;
                objSeqArr1[1].Value = p_dtmEnd;

                lngRes = objHRPMain.lngGetDataTableWithParameters(SQLHelper::clsSqlStringHelper.s_strGetQueryInHospitalMainRec, ref p_dtbResult, objSeqArr1);
               
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
    }
}
