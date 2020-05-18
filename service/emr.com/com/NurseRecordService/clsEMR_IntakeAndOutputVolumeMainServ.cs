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
    /// 出入量登记表
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsEMR_IntakeAndOutputVolumeMainServ : clsRecordsService
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
            string strSql = @"update t_emr_intakeandoutputvolume
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

            clsEMR_IntakeAndOutputVolumeDataInfo objDataInfo = new clsEMR_IntakeAndOutputVolumeDataInfo();

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
       a.recordtime_vchr,
       a.stool_vchr,
       a.urine_vchr,
       a.gastricjuice_vchr,
       a.bile_vchr,
       a.intestinaljuice_vchr,
       a.chestfluid_vchr,
       a.otheroutput_vchr,
       a.drinkingwater_vchr,
       a.food_vchr,
       a.transfusion_vchr,
       a.sugarwater_vchr,
       a.salinewater_vchr,
       a.otherintake_vchr,
       a.stool_xml,
       a.urine_xml,
       a.gastricjuice_xml,
       a.bile_xml,
       a.intestinaljuice_xml,
       a.chestfluid_xml,
       a.otheroutput_xml,
       a.drinkingwater_xml,
       a.food_xml,
       a.transfusion_xml,
       a.sugarwater_xml,
       a.salinewater_xml,
       a.otherintake_xml,
       a.index_int,
       a.markstatus,
       a.otheroutput_name,
       a.otherintake_name,
       b.status_int,
       b.modifydate,
       b.modifyuserid,
       b.stool_right,
       b.urine_right,
       b.gastricjuice_right,
       b.bile_right,
       b.intestinaljuice_right,
       b.chestfluid_right,
       b.otheroutput_right,
       b.drinkingwater_right,
       b.food_right,
       b.transfusion_right,
       b.sugarwater_right,
       b.salinewater_right,
       b.otherintake_right
  from t_emr_intakeandoutputvolume a
 inner join t_emr_intakeandoutput_right b on a.registerid_chr =
                                             b.registerid_chr
                                         and a.createdate_dat =
                                             b.createdate_dat
 where a.status_int = 1
   and a.registerid_chr = ?
 order by a.recorddate_dat, a.index_int, b.modifydate";
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
                    int intTableCount = dtbValue.Rows.Count;
                    DataRow dtrSelected = null;
                    objDataInfo.m_objRecordArr = new clsEMR_IntakeAndOutputVolumeValue[intTableCount];

                    for (int i = 0; i < intTableCount; i++)
                    {
                        dtrSelected = dtbValue.Rows[i];
                        objDataInfo.m_objRecordArr[i] = new clsEMR_IntakeAndOutputVolumeValue();
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

                        objDataInfo.m_objRecordArr[i].m_strRECORDTIME_VCHR = dtrSelected["RECORDTIME_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strSTOOL_VCHR = dtrSelected["STOOL_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strURINE_VCHR = dtrSelected["URINE_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strGASTRICJUICE_VCHR = dtrSelected["GASTRICJUICE_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strBILE_VCHR = dtrSelected["BILE_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strINTESTINALJUICE_VCHR = dtrSelected["INTESTINALJUICE_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strCHESTFLUID_VCHR = dtrSelected["CHESTFLUID_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strOTHEROUTPUT_VCHR = dtrSelected["OTHEROUTPUT_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strDRINKINGWATER_VCHR = dtrSelected["DRINKINGWATER_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strFOOD_VCHR = dtrSelected["FOOD_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strTRANSFUSION_VCHR = dtrSelected["TRANSFUSION_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strSUGARWATER_VCHR = dtrSelected["SUGARWATER_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strSALINEWATER_VCHR = dtrSelected["SALINEWATER_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strOTHERINTAKE_VCHR = dtrSelected["OTHERINTAKE_VCHR"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strSTOOL_XML = dtrSelected["STOOL_XML"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strURINE_XML = dtrSelected["URINE_XML"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strGASTRICJUICE_XML = dtrSelected["GASTRICJUICE_XML"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strBILE_XML = dtrSelected["BILE_XML"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strINTESTINALJUICE_XML = dtrSelected["INTESTINALJUICE_XML"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strCHESTFLUID_XML = dtrSelected["CHESTFLUID_XML"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strOTHEROUTPUT_XML = dtrSelected["OTHEROUTPUT_XML"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strDRINKINGWATER_XML = dtrSelected["DRINKINGWATER_XML"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strFOOD_XML = dtrSelected["FOOD_XML"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strTRANSFUSION_XML = dtrSelected["TRANSFUSION_XML"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strSUGARWATER_XML = dtrSelected["SUGARWATER_XML"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strSALINEWATER_XML = dtrSelected["SALINEWATER_XML"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strOTHERINTAKE_XML = dtrSelected["OTHERINTAKE_XML"].ToString();
                        objDataInfo.m_objRecordArr[i].m_intINDEX_INT = Convert.ToInt32(dtrSelected["INDEX_INT"]);

                        objDataInfo.m_objRecordArr[i].m_strSTOOL_RIGHT = dtrSelected["STOOL_RIGHT"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strURINE_RIGHT = dtrSelected["URINE_RIGHT"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strGASTRICJUICE_RIGHT = dtrSelected["GASTRICJUICE_RIGHT"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strBILE_RIGHT = dtrSelected["BILE_RIGHT"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strINTESTINALJUICE_RIGHT = dtrSelected["INTESTINALJUICE_RIGHT"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strCHESTFLUID_RIGHT = dtrSelected["CHESTFLUID_RIGHT"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strOTHEROUTPUT_RIGHT = dtrSelected["OTHEROUTPUT_RIGHT"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strDRINKINGWATER_RIGHT = dtrSelected["DRINKINGWATER_RIGHT"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strFOOD_RIGHT = dtrSelected["FOOD_RIGHT"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strTRANSFUSION_RIGHT = dtrSelected["TRANSFUSION_RIGHT"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strSUGARWATER_RIGHT = dtrSelected["SUGARWATER_RIGHT"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strSALINEWATER_RIGHT = dtrSelected["SALINEWATER_RIGHT"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strOTHERINTAKE_RIGHT = dtrSelected["OTHERINTAKE_RIGHT"].ToString();

                        objDataInfo.m_objRecordArr[i].m_strOTHEROUTPUT_NAME = dtrSelected["OTHEROUTPUT_NAME"].ToString();
                        objDataInfo.m_objRecordArr[i].m_strOTHERINTAKE_NAME = dtrSelected["OTHERINTAKE_NAME"].ToString();
                        //获取签名集合
                        long lngS = 0;
                        if (long.TryParse(dtrSelected["SEQUENCE_INT"].ToString(), out lngS))
                        {
                            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                            long lngTemp = objSign.m_lngGetSign(lngS, out objDataInfo.m_objRecordArr[i].objSignerArr);

                            //释放
                            objSign = null;
                        }
                    }
                }
                strSql = @"select a.registerid_chr,
       a.createdate_dat,
       a.createuserid_chr,
       a.status_int,
       a.deactiveddate_dat,
       a.deactivedoperatorid_chr,
       a.recorduserid_vchr,
       a.recorddate_dat,
       a.sequence_int,
       a.allurine,
       a.allurinexml,
       a.alloutput,
       a.alloutputxml,
       a.specificgravity,
       a.specificgravityxml,
       a.allintake,
       a.allintakexml,
       a.modifydate,
       a.modifyuserid,
       a.markstatus
  from t_emr_intakeandoutput_sum a
 where a.status_int = 1
   and a.registerid_chr = ?";

                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;

                DataTable dtbSum = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbSum, objDPArr);

                if (lngRes > 0 && dtbSum.Rows.Count > 0)
                {
                    int intSumCount = dtbSum.Rows.Count;
                    DataRow dtrSelected1 = null;
                    objDataInfo.m_objSummaryInfo = new clsEMR_IntakeAndOutputVolumeSum[intSumCount];

                    for (int j1 = 0; j1 < intSumCount; j1++)
                    {
                        objDataInfo.m_objSummaryInfo[j1] = new clsEMR_IntakeAndOutputVolumeSum();
                        dtrSelected1 = dtbSum.Rows[j1];

                        objDataInfo.m_objSummaryInfo[j1].m_strRegisterID = p_strRegisterId;
                        DateTime dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtrSelected1["CREATEDATE_DAT"].ToString(), out dtmTemp);
                        objDataInfo.m_objSummaryInfo[j1].m_dtmCreateDate = dtmTemp;

                        objDataInfo.m_objSummaryInfo[j1].m_strCreateUserID = dtrSelected1["CREATEUSERID_CHR"].ToString();
                        objDataInfo.m_objSummaryInfo[j1].m_strModifyUserID = dtrSelected1["MODIFYUSERID"].ToString();
                        objDataInfo.m_objSummaryInfo[j1].m_strRecordUserID = dtrSelected1["RECORDUSERID_VCHR"].ToString();

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtrSelected1["MODIFYDATE"].ToString(), out dtmTemp);
                        objDataInfo.m_objSummaryInfo[j1].m_dtmModifyDate = dtmTemp;
                        int intTemp = 0;
                        int.TryParse(dtrSelected1["STATUS_INT"].ToString(), out intTemp);
                        objDataInfo.m_objSummaryInfo[j1].m_bytStatus = intTemp;

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtrSelected1["RECORDDATE_DAT"].ToString(), out dtmTemp);
                        objDataInfo.m_objSummaryInfo[j1].m_dtmRecordDate = dtmTemp;

                        objDataInfo.m_objSummaryInfo[j1].m_strALLURINE = dtrSelected1["ALLURINE"].ToString();
                        objDataInfo.m_objSummaryInfo[j1].m_strALLURINEXML = dtrSelected1["ALLURINEXML"].ToString();
                        objDataInfo.m_objSummaryInfo[j1].m_strALLOUTPUT = dtrSelected1["ALLOUTPUT"].ToString();
                        objDataInfo.m_objSummaryInfo[j1].m_strALLOUTPUTXML = dtrSelected1["ALLOUTPUTXML"].ToString();
                        objDataInfo.m_objSummaryInfo[j1].m_strSPECIFICGRAVITY = dtrSelected1["SPECIFICGRAVITY"].ToString();
                        objDataInfo.m_objSummaryInfo[j1].m_strSPECIFICGRAVITYXML = dtrSelected1["SPECIFICGRAVITYXML"].ToString();
                        objDataInfo.m_objSummaryInfo[j1].m_strALLINTAKE = dtrSelected1["ALLINTAKE"].ToString();
                        objDataInfo.m_objSummaryInfo[j1].m_strALLINTAKEXML = dtrSelected1["ALLINTAKEXML"].ToString();

                        //获取签名集合
                        long lngS = 0;
                        if (long.TryParse(dtrSelected1["SEQUENCE_INT"].ToString(), out lngS))
                        {
                            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                            long lngTemp = objSign.m_lngGetSign(lngS, out objDataInfo.m_objSummaryInfo[j1].objSignerArr);

                            //释放
                            objSign = null;
                        }
                    }                    
                }

                if (objDataInfo.m_objRecordArr != null)
                {
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
  from t_emr_intakeandoutputvolume t1, t_emr_intakeandoutput_right t2
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
  from t_emr_intakeandoutputvolume t
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
            string strSql = @"update t_emr_intakeandoutputvolume t
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

        #region 设置自定义列名

        /// <summary>
        /// 设置自定义列名

        /// </summary>
        /// <param name="p_strRegisterID">入院登记号</param>
        /// <param name="p_strColumnIndex">更改的字段名</param>
        /// <param name="p_strColumnName">更改的标头文字</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetCustomColumnName(string p_strRegisterID,
            string p_strColumnIndex,
            string p_strColumnName)
        {
            long lngRes = 0;
            clsHRPTableService p_objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = @"update t_emr_intakeandoutputvolume set " + p_strColumnIndex + @"=? 
								where registerid_chr = ?";
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strColumnName;
                objDPArr[1].Value = p_strRegisterID;

                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSQL, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {

                //p_objHRPServ.Dispose();
            }
            return lngRes;
        } 
        #endregion
    }
}
