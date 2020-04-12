using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;

namespace com.digitalwave.clsRecordsService
{
    /// <summary>
    /// 快速微量血糖检测记录表
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_MicroBooldSugarCheckMainServ : clsRecordsService
    {
        #region 更新数据库中的首次打印时间

        /// <summary>
        /// 更新数据库中的首次打印时间。

        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
        /// <param name="p_intRecordTypeArr">记录类型</param>
        /// <param name="p_dtmCreatedDateArr">记录时间(与记录类型及其位置一一对应)</param>
        /// <param name="p_dtmFirstPrintDate">首次打印时间</param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strRegisterId,
            int[] p_intRecordTypeArr,
            DateTime[] p_dtmCreatedDateArr,
            DateTime p_dtmFirstPrintDate)
        {
            //检查参数

            if (string.IsNullOrEmpty(p_strRegisterId) || p_dtmFirstPrintDate == DateTime.MinValue)
                return (long)enmOperationResult.Parameter_Error;
            string strSql = @"update t_emr_microbooldsugarcheck
   set firstprintdate_dat = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0; i < p_dtmCreatedDateArr.Length; i++)
                    {
                        objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                        objDPArr[0].DbType = DbType.DateTime;
                        objDPArr[0].Value = p_dtmFirstPrintDate;
                        objDPArr[1].Value = p_strRegisterId;
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = p_dtmCreatedDateArr[i];
                        //执行SQL
                        long lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                    }
                }
                else
                {
                    DbType[] dbTypes = new DbType[] { DbType.Date, DbType.String, DbType.Date };
                    string[][] strValues = new string[3][];
                    if (p_dtmCreatedDateArr.Length > 0)
                    {
                        for (int j = 0; j < strValues.Length; j++)
                        {
                            strValues[j] = new string[p_dtmCreatedDateArr.Length];
                        }
                        for (int k1 = 0; k1 < p_dtmCreatedDateArr.Length; k1++)
                        {
                            strValues[0][k1] = p_dtmFirstPrintDate.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        for (int k2 = 0; k2 < p_dtmCreatedDateArr.Length; k2++)
                        {
                            strValues[1][k2] = p_strRegisterId;
                        }
                        for (int k3 = 0; k3 < p_dtmCreatedDateArr.Length; k3++)
                        {
                            strValues[2][k3] = p_dtmCreatedDateArr[k3].ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSql, strValues, dbTypes);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, true);
            }
            finally
            {
                objHRPServ = null;
            }

            return (long)enmOperationResult.DB_Succeed;
        }
        #endregion 

        #region 获取指定记录的内容

        /// <summary>
        /// 获取指定记录的内容。

        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_intStatus"></param>
        /// <param name="p_objTansDataInfoArr"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetTransDataInfoArrWithServ(string p_strRegisterId, int p_intStatus, out clsTransDataInfo[] p_objTansDataInfoArr)
        {
            p_objTansDataInfoArr = null;
            long lngRes = -1;
            //检查参数

            if (string.IsNullOrEmpty(p_strRegisterId))
                return (long)enmOperationResult.Parameter_Error;
            string strSql = @"select a.registerid_chr,
       a.createdate_dat,
       a.createuserid_chr,
       a.ifconfirm_int,
       a.status_int,
       a.deactiveddate_dat,
       a.deactivedoperatorid_chr,
       a.firstprintdate_dat,
       a.recorduserid_vchr,
       a.recorddate_dat,
       a.sequence_int,
       a.markstatus,
       a.checktime_vchr,
       a.checktime_xml,
       a.checkresult_vchr,
       a.checkresult_xml,
       b.status_int,
       b.modifydate,
       b.modifyuserid,
       b.checktime_right,
       b.checkresult_right
  from t_emr_microbooldsugarcheck a
 inner join t_emr_microbooldsugarcheckcon b on a.registerid_chr =
                                               b.registerid_chr
                                           and a.createdate_dat =
                                               b.createdate_dat
 where a.status_int = 1
   and b.status_int = 1
   and a.registerid_chr = ?
 order by a.recorddate_dat, b.modifydate";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;

                DataTable dtbValue = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsEMR_MicroBooldSugarCheckDataInfo objDataInfo = new clsEMR_MicroBooldSugarCheckDataInfo();
                    int intTableCount = dtbValue.Rows.Count;
                    DataRow dtrSelected = null;
                    objDataInfo.m_objRecordArr = new clsEMR_MicroBooldSugarCheckValue[intTableCount];

                    for (int i = 0; i < intTableCount; i++)
                    {
                        dtrSelected = dtbValue.Rows[i];
                        objDataInfo.m_objRecordArr[i] = new clsEMR_MicroBooldSugarCheckValue();
                        objDataInfo.m_objRecordArr[i].m_strRegisterID = p_strRegisterId;
                        DateTime dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtrSelected["CREATEDATE_DAT"].ToString(), out dtmTemp);
                        objDataInfo.m_objRecordArr[i].m_dtmCreateDate = dtmTemp;

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtrSelected["RECORDDATE_DAT"].ToString(), out dtmTemp);
                        objDataInfo.m_objRecordArr[i].m_dtmRecordDate = dtmTemp;

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtrSelected["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                        objDataInfo.m_objRecordArr[i].m_dtmFirstPrintDate = dtmTemp;

                        objDataInfo.m_objRecordArr[i].m_strCreateUserID = dtrSelected["CREATEUSERID_CHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strRecordUserID = dtrSelected["RECORDUSERID_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strModifyUserID = dtrSelected["MODIFYUSERID"].ToString();

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtrSelected["MODIFYDATE"].ToString(), out dtmTemp);
                        objDataInfo.m_objRecordArr[i].m_dtmModifyDate = dtmTemp;


                        int intTemp = 0;
                        int.TryParse(dtrSelected["IFCONFIRM_INT"].ToString(), out intTemp);
                        objDataInfo.m_objRecordArr[i].m_bytIfConfirm = intTemp;
                        intTemp = 0;
                        int.TryParse(dtrSelected["STATUS_INT"].ToString(), out intTemp);
                        objDataInfo.m_objRecordArr[i].m_bytStatus = intTemp;

                        objDataInfo.m_objRecordArr[i].m_strCHECKTIME_VCHR = dtrSelected["CHECKTIME_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strCHECKTIME_XML = dtrSelected["CHECKTIME_XML"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strCHECKRESULT_VCHR = dtrSelected["CHECKRESULT_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strCHECKRESULT_XML = dtrSelected["CHECKRESULT_XML"].ToString();

                        objDataInfo.m_objRecordArr[i].m_strCHECKTIME_RIGHT = dtrSelected["CHECKTIME_RIGHT"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strCHECKRESULT_RIGHT = dtrSelected["CHECKRESULT_RIGHT"].ToString();
                        //获取签名集合
                        long lngS = 0;
                        if (long.TryParse(dtrSelected["SEQUENCE_INT"].ToString(), out lngS))
                        {
                            if (lngS != -1)//从旧表导过来的数据没有电子签名，省去查询的步骤

                            {
                                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                                long lngTemp = objSign.m_lngGetSign(lngS, out objDataInfo.m_objRecordArr[i].objSignerArr);

                                //释放
                                objSign = null;
                            }                            
                        }
                    }
                    p_objTansDataInfoArr = new clsTransDataInfo[] { objDataInfo };
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
                return 0;
            }
            finally
            {
                objHRPServ = null;
            }
            return (long)enmOperationResult.DB_Succeed;
        }
        #endregion

        #region 查看当前记录是否最新的记录
        /// <summary>
        /// 查看当前记录是否最新的记录。

        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objModifyInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(int p_intRecordType,
            clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;

            //检查参数          
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null || p_objHRPServ == null)
                return (long)enmOperationResult.Parameter_Error;

            string strSQL = @"select t2.modifydate, t2.modifyuserid
  from t_emr_microbooldsugarcheck t1, t_emr_microbooldsugarcheckcon t2
 where t1.registerid_chr = t2.registerid_chr
   and t1.createdate_dat = t2.createdate_dat
   and t1.status_int = 1
   and t2.status_int = 1
   and t1.registerid_chr = ?
   and t1.createdate_dat = ? ";


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;
                //使用strSQL生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable            
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    strSQL = @"select t.deactiveddate_dat, t.deactivedoperatorid_chr
  from t_emr_microbooldsugarcheck t
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 0";
                    objDPArr = null;
                    p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_objRecordContent.m_strRegisterID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;

                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID_CHR"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE_DAT"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    //if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
                    return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
                    //p_objModifyInfo = new clsPreModifyInfo();
                    //p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    //p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                    //return (long)enmOperationResult.Record_Already_Modify;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                objHRPServ = null;
            }
            return lngRes;

        }
        #endregion

        #region 把记录从数据中“删除”。

        /// <summary>
        /// 把记录从数据中“删除”。

        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(int p_intRecordType,
            clsTrackRecordContent p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            //检查参数

            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngRes = 0;
            string strSql = @"update t_emr_microbooldsugarcheck t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_chr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 1";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmCreateDate;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion
    }
}
