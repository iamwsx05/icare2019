using System.EnterpriseServices;
using System;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.BaseCaseHistorySevice;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Drawing;
using System.Collections;

namespace com.digitalwave.InPatientCaseHistoryServ
{
    /// <summary>
    /// 中期妊娠三合一
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsGestationMisbirthsthreeRelationVOService : clsBaseCaseHistorySevice
    {
        #region SQL
        private const string c_strGetTimeListSQL = @"select inpatientdate,createdate,opendate 
from gestationmisbirthsthree 
where inpatientid = ? and inpatientdate = ?
and status=0 order by opendate desc";

        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,a.registerid_chr,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.operationdate,
       a.operationready,
       a.laborinduction,
       a.medicname,
       a.medicdose,
       a.diluentdose,
       a.mediclot,
       a.abdominalpuncture,
       a.noneedle,
       a.puncturesize,
       a.amniotic,
       a.amnioticcolor,
       a.amnioticother,
       a.vaginalcervix,
       a.insertiondepth,
       a.meilan,
       a.vaginalgauze,
       a.cystictime,
       a.outof,
       a.aftersurgery,
       a.surgicalbleeding,
       a.notes,
       a.contractionstime,
       a.brokenwatertime,
       a.fetalditime,
       a.fetaldimethod,
       a.fetal,
       a.fetallength,
       a.fetalweight,
       a.placenta,
       a.palace,
       a.palacecontent,
       a.postpartumbleeding,
       a.contractionsagent,
       a.dose,
       a.birthcheck,
       a.shexiang,
       a.treatment,
       a.surgeon,
       a.handler,
       a.modifydate,
       a.modifyuserid,
       a.sequence_int,
       tbe.lastname_vchr
  from gestationmisbirthsthree a, t_bse_employee tbe
 where a.inpatientid = ? and a.inpatientdate = ?
   and a.status = 0
   and a.createuserid = tbe.empno_chr";

        private const string c_strGetDeletedRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,a.registerid_chr,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.operationdate,
       a.operationready,
       a.laborinduction,
       a.medicname,
       a.medicdose,
       a.diluentdose,
       a.mediclot,
       a.abdominalpuncture,
       a.noneedle,
       a.puncturesize,
       a.amniotic,
       a.amnioticcolor,
       a.amnioticother,
       a.vaginalcervix,
       a.insertiondepth,
       a.meilan,
       a.vaginalgauze,
       a.cystictime,
       a.outof,
       a.aftersurgery,
       a.surgicalbleeding,
       a.notes,
       a.contractionstime,
       a.brokenwatertime,
       a.fetalditime,
       a.fetaldimethod,
       a.fetal,
       a.fetallength,
       a.fetalweight,
       a.placenta,
       a.palace,
       a.palacecontent,
       a.postpartumbleeding,
       a.contractionsagent,
       a.dose,
       a.birthcheck,
       a.shexiang,
       a.treatment,
       a.surgeon,
       a.handler,
       a.modifydate,
       a.modifyuserid,
       a.sequence_int,
       tbe.lastname_vchr
  from gestationmisbirthsthree a, t_bse_employee tbe
 where a.inpatientid = ? and a.inpatientdate = ?
   and a.status = 1
   and a.createuserid = tbe.empno_chr";


        private const string c_strCheckCreateDateSQL = @"select inpatientid,
       inpatientdate,registerid_chr,
       opendate,
       createdate,
       createuserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       firstprintdate,
       operationdate,
       operationready,
       laborinduction,
       medicname,
       medicdose,
       diluentdose,
       mediclot,
       abdominalpuncture,
       noneedle,
       puncturesize,
       amniotic,
       amnioticcolor,
       amnioticother,
       vaginalcervix,
       insertiondepth,
       meilan,
       vaginalgauze,
       cystictime,
       outof,
       aftersurgery,
       surgicalbleeding,
       notes,
       contractionstime,
       brokenwatertime,
       fetalditime,
       fetaldimethod,
       fetal,
       fetallength,
       fetalweight,
       placenta,
       palace,
       palacecontent,
       postpartumbleeding,
       contractionsagent,
       dose,
       birthcheck,
       shexiang,
       treatment,
       surgeon,
       handler,
       modifydate,
       modifyuserid,
       sequence_int,
  from gestationmisbirthsthree
 where inpatientid = ? and inpatientdate = ?
   and status = 0";



        private const string c_strGetDeleteRecordSQL = "";

        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select firstprintdate,modifydate
																			from gestationmisbirthsthree
																			where inpatientid = ? and inpatientdate = ?
																			and status = 0";

        #endregion

        #region 获取病人的该记录时间列表
        /// <summary>
        ///  获取病人的该记录时间列表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDateArr"></param>
        /// <param name="p_strCreateRecordTimeArr"></param>
        /// <param name="p_strOpenRecordTimeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetRecordTimeList(
            string p_strInPatientID,
            out string[] p_strInPatientDateArr,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strInPatientDateArr = null;
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            long lngRes = 0;
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsNewBabyInRoomRecordService","m_lngGetRecordTimeList");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数
            if (p_strInPatientID == null || p_strInPatientID == "")
                return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strInPatientDateArr = new string[dtbValue.Rows.Count];
                    p_strCreateRecordTimeArr = new string[dtbValue.Rows.Count];
                    p_strOpenRecordTimeArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strInPatientDateArr[i] = dtbValue.Rows[i]["INPATIENTDATE"].ToString();
                        p_strCreateRecordTimeArr[i] = dtbValue.Rows[i]["CREATEDATE"].ToString();
                        p_strOpenRecordTimeArr[i] = dtbValue.Rows[i]["OPENDATE"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }		//返回
            return lngRes;


        }
        #endregion

        #region 获取病人的已经被删除记录时间列表
        /// <summary>
        ///  获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strDeleteUserID"></param>
        /// <param name="p_strCreateRecordTimeArr"></param>
        /// <param name="p_strOpenRecordTimeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeList(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strDeleteUserID,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            long lngRes = 0;
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsNewBabyInRoomRecordService","m_lngGetDeleteRecordTimeList");
                //if(lngCheckRes <= 0)
                //return lngCheckRes;	
                lngRes = (long)enmOperationResult.DB_Succeed;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;

        }
        #endregion

        #region 新添加 registerid_chr
        /// <summary>
        ///  获取病人的已经被删除记录时间列表。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strCreateRecordTimeArr"></param>
        /// <param name="p_strOpenRecordTimeArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngGetDeleteRecordTimeListAll(
            string p_strInPatientID,
            string p_strInPatientDate,
            out string[] p_strCreateRecordTimeArr,
            out string[] p_strOpenRecordTimeArr)
        {
            p_strCreateRecordTimeArr = null;
            p_strOpenRecordTimeArr = null;
            long lngRes = 0;
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInPatientCaseHistoryServ","m_lngGetDeleteRecordTimeListAll");
                //if(lngCheckRes <= 0)
                //return lngCheckRes;	

                lngRes = (long)enmOperationResult.DB_Succeed;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            //返回
            return lngRes;

        }
        #endregion

        #region 获取指定记录的内容
        /// <summary>
        /// 获取指定记录的内容。
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objPicValueArr"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetRecordContentWithServ(
            string p_strInPatientID,
            string p_strInPatientDate,
            clsHRPTableService p_objHRPServ,
            out clsBaseCaseHistoryInfo p_objRecordContent,
            out clsPictureBoxValue[] p_objPicValueArr)
        {

            #region SQL
            string strGetRecordContentSQLByRegisterID = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.operationdate,
       a.operationready,
       a.laborinduction,
       a.medicname,
       a.medicdose,
       a.diluentdose,
       a.mediclot,
       a.abdominalpuncture,
       a.noneedle,
       a.puncturesize,
       a.amniotic,
       a.amnioticcolor,
       a.amnioticother,
       a.vaginalcervix,
       a.insertiondepth,
       a.meilan,
       a.vaginalgauze,
       a.cystictime,
       a.outof,
       a.aftersurgery,
       a.surgicalbleeding,
       a.notes,
       a.notes_xml,
       a.contractionstime,
       a.brokenwatertime,
       a.fetalditime,
       a.fetaldimethod,
       a.fetal,
       a.fetallength,
       a.fetalweight,
       a.placenta,
       a.palace,
       a.palacecontent,
       a.postpartumbleeding,
       a.contractionsagent,
       a.dose,
       a.birthcheck,
       a.shexiang,
       a.shexiang_xml,
       a.treatment,
       a.treatment_xml,
       a.surgeon,
       a.handler,
       a.modifydate,
       a.modifyuserid,
       a.sequence_int,
       b.notes_right,
       b.shexiang_right,
       b.treatment_right,
       tbe.lastname_vchr
  from gestationmisbirthsthree a,gestationmisbirthsthree_con b, t_bse_employee tbe
 where a.inpatientid = b.inpatientid
	    and a.inpatientdate = b.inpatientdate
	    and a.opendate = b.opendate and a.inpatientid = ? and a.inpatientdate = ?
   and a.status = 0
   and a.createuserid = tbe.empno_chr and b.modifydate = (select max(modifydate)
                                                                  from gestationmisbirthsthree_con
                                                                 where inpatientid = a.inpatientid
                                                                   and opendate = a.opendate) ";
            #endregion
            p_objRecordContent = null;
            p_objPicValueArr = null;
            long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strGetRecordContentSQLByRegisterID, ref dtbValue, objDPArr);
                p_objHRPServ.Dispose();
                p_objHRPServ = null;

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsGestationMisbirthsthreeRelationVO p_objContent = new clsGestationMisbirthsthreeRelationVO();
                    
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        #region 获取结果
                        p_objContent.m_strInPatientID = p_strInPatientID;
                        p_objContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[i]["inpatientdate"]);
                        p_objContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[i]["OPENDATE"]);
                        p_objContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
                        p_objContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();

                        if (dtbValue.Rows[i]["FIRSTPRINTDATE"].ToString() == "")
                            p_objContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else p_objContent.m_dtmFirstPrintDate = Convert.ToDateTime(dtbValue.Rows[i]["FIRSTPRINTDATE"]);
                        if (dtbValue.Rows[i]["STATUS"].ToString() == "")
                            p_objContent.m_bytStatus = 0;
                        else p_objContent.m_bytStatus = Byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());
                        p_objContent.m_strOPERATIONDATE = dtbValue.Rows[i]["OPERATIONDATE"].ToString().Trim();
                        p_objContent.m_strOPERATIONREADY = dtbValue.Rows[i]["OPERATIONREADY"].ToString().Trim();
                        p_objContent.m_strLABORINDUCTION = dtbValue.Rows[i]["LABORINDUCTION"].ToString().Trim();
                        p_objContent.m_strMEDICNAME = dtbValue.Rows[i]["MEDICNAME"].ToString().Trim();
                        p_objContent.m_strMEDICDOSE = dtbValue.Rows[i]["MEDICDOSE"].ToString().Trim();
                        p_objContent.m_strDILUENTDOSE = dtbValue.Rows[i]["DILUENTDOSE"].ToString().Trim();
                        p_objContent.m_strMEDICLOT = dtbValue.Rows[i]["MEDICLOT"].ToString().Trim();
                        p_objContent.m_strABDOMINALPUNCTURE = dtbValue.Rows[i]["ABDOMINALPUNCTURE"].ToString().Trim();
                        p_objContent.m_strNONEEDLE = dtbValue.Rows[i]["NONEEDLE"].ToString().Trim();
                        p_objContent.m_strPUNCTURESIZE = dtbValue.Rows[i]["PUNCTURESIZE"].ToString().Trim();
                        p_objContent.m_strAMNIOTIC = dtbValue.Rows[i]["AMNIOTIC"].ToString().Trim();
                        p_objContent.m_strAMNIOTICCOLOR = dtbValue.Rows[i]["AMNIOTICCOLOR"].ToString().Trim();
                        p_objContent.m_strAMNIOTICOTHER = dtbValue.Rows[i]["AMNIOTICOTHER"].ToString().Trim();
                        p_objContent.m_strVAGINALCERVIX = dtbValue.Rows[i]["VAGINALCERVIX"].ToString().Trim();
                        p_objContent.m_strINSERTIONDEPTH = dtbValue.Rows[i]["INSERTIONDEPTH"].ToString().Trim();
                        p_objContent.m_strMEILAN = dtbValue.Rows[i]["MEILAN"].ToString().Trim();
                        p_objContent.m_strVAGINALGAUZE = dtbValue.Rows[i]["VAGINALGAUZE"].ToString().Trim();
                        p_objContent.m_strCYSTICTIME = dtbValue.Rows[i]["CYSTICTIME"].ToString().Trim();
                        p_objContent.m_strOUTOF = dtbValue.Rows[i]["OUTOF"].ToString().Trim();
                        p_objContent.m_strAFTERSURGERY = dtbValue.Rows[i]["AFTERSURGERY"].ToString().Trim();
                        p_objContent.m_strSURGICALBLEEDING = dtbValue.Rows[i]["SURGICALBLEEDING"].ToString().Trim();
                        p_objContent.m_strNOTES = dtbValue.Rows[i]["NOTES"].ToString().Trim();
                        p_objContent.m_strNOTES_RIGHT = dtbValue.Rows[i]["NOTES_RIGHT"].ToString().Trim();
                        p_objContent.m_strNOTES_XML = dtbValue.Rows[i]["NOTES_XML"].ToString().Trim();
                        p_objContent.m_strCONTRACTIONSTIME = dtbValue.Rows[i]["CONTRACTIONSTIME"].ToString().Trim();
                        p_objContent.m_strBROKENWATERTIME = dtbValue.Rows[i]["BROKENWATERTIME"].ToString().Trim();
                        p_objContent.m_strFETALDITIME = dtbValue.Rows[i]["FETALDITIME"].ToString().Trim();
                        p_objContent.m_strFETALDIMETHOD = dtbValue.Rows[i]["FETALDIMETHOD"].ToString().Trim();
                        p_objContent.m_strFETAL = dtbValue.Rows[i]["FETAL"].ToString().Trim();
                        p_objContent.m_strFETALLENGTH = dtbValue.Rows[i]["FETALLENGTH"].ToString().Trim();
                        p_objContent.m_strFETALWEIGHT = dtbValue.Rows[i]["FETALWEIGHT"].ToString().Trim();
                        p_objContent.m_strPLACENTA = dtbValue.Rows[i]["PLACENTA"].ToString().Trim();
                        p_objContent.m_strPALACE = dtbValue.Rows[i]["PALACE"].ToString().Trim();
                        p_objContent.m_strPALACECONTENT = dtbValue.Rows[i]["PALACECONTENT"].ToString().Trim();
                        p_objContent.m_strPOSTPARTUMBLEEDING = dtbValue.Rows[i]["POSTPARTUMBLEEDING"].ToString().Trim();
                        p_objContent.m_strCONTRACTIONSAGENT = dtbValue.Rows[i]["CONTRACTIONSAGENT"].ToString().Trim();
                        p_objContent.m_strDOSE = dtbValue.Rows[i]["DOSE"].ToString().Trim();
                        p_objContent.m_strBIRTHCHECK = dtbValue.Rows[i]["BIRTHCHECK"].ToString().Trim();
                        p_objContent.m_strSHEXIANG = dtbValue.Rows[i]["SHEXIANG"].ToString().Trim();
                        p_objContent.m_strSHEXIANG_RIGHT = dtbValue.Rows[i]["SHEXIANG_RIGHT"].ToString().Trim();
                        p_objContent.m_strSHEXIANG_XML = dtbValue.Rows[i]["SHEXIANG_XML"].ToString().Trim();
                        p_objContent.m_strTREATMENT = dtbValue.Rows[i]["TREATMENT"].ToString().Trim();
                        p_objContent.m_strTREATMENT_RIGHT = dtbValue.Rows[i]["TREATMENT_RIGHT"].ToString().Trim();
                        p_objContent.m_strTREATMENT_XML = dtbValue.Rows[i]["TREATMENT_XML"].ToString().Trim();
                        //p_objContent.m_strSURGEON = dtbValue.Rows[i]["SURGEON"].ToString().Trim();
                        //p_objContent.m_strHANDLER = dtbValue.Rows[i]["HANDLER"].ToString().Trim();
                        p_objContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
                        p_objContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
                        if (dtbValue.Rows[i]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                            long lngTemp = objSign.m_lngGetSign(lngS, out p_objContent.objSignerArr);

                            //释放
                            objSign = null;
                        }
                        #endregion
                    }
                    p_objRecordContent = p_objContent;
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //p_objHRPServ.Dispose();

            }			//返回
            return lngRes;
        }
        #endregion

        #region 查看是否有相同的记录
        /// <summary>
        /// 查看是否有相同的记录
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objPreModifyInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckCreateDate(clsBaseCaseHistoryInfo p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objPreModifyInfo)
        {
            p_objPreModifyInfo = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {


                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss");

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objPreModifyInfo = new clsPreModifyInfo();
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_objPreModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[i]["OPENDATE"].ToString());
                        p_objPreModifyInfo.m_strActionUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();
                    }
                }

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
        #endregion

        #region 查看是否最新记录
        /// <summary>
        /// 查看是否最新记录
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objModifyInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngCheckLastModifyRecord(clsBaseCaseHistoryInfo p_objRecordContent,
            clsHRPTableService p_objHRPServ,
            out clsPreModifyInfo p_objModifyInfo)
        {
            p_objModifyInfo = null;
            long lngRes = 0;
            string c_strCheckLastModifyRecordSQL = null;
            if (clsHRPTableService.bytDatabase_Selector == 0)
            {
                c_strCheckLastModifyRecordSQL = "select top 1 modifydate,modifyuserid from gestationmisbirthsthree where inpatientid = ? and inpatientdate= ? and opendate= ? and status=0 order by modifydate desc";
            }
            else
            {
                c_strCheckLastModifyRecordSQL = @"select modifydate, modifyuserid
  from (select modifydate, modifyuserid
          from gestationmisbirthsthree
         where inpatientid = ?
           and inpatientdate = ?
           and opendate = ?
           and status = 0
         order by modifydate desc)
 where rownum = 1";
            }


            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);

                objDPArr[0].Value = p_objRecordContent.m_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_objRecordContent.m_dtmOpenDate;

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckLastModifyRecordSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objModifyInfo = new clsPreModifyInfo();
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果,
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[i]["ModifyDate"].ToString());
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[i]["ModifyUserID"].ToString();
                    }
                }
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
        #endregion

        #region 获取首次打印时间及修改时间
        /// <summary>
        /// 获取首次打印时间及修改时间
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_dtmModifyDate"></param>
        /// <param name="p_strFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetModifyDateAndFirstPrintDate
            (string p_strInPatientID,
            string p_strInPatientDate, clsHRPTableService p_objHRPServ,
            out DateTime p_dtmModifyDate,
            out string p_strFirstPrintDate)
        {
            p_dtmModifyDate = DateTime.MinValue;
            p_strFirstPrintDate = null;

            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                //			objDPArr[2].Value=DateTime.Parse(p_strOpenRecordTime);
                //生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetModifyDateAndFirstPrintDateSQL, ref dtbValue, objDPArr);
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count >= 1)
                {
                    //设置结果
                    p_strFirstPrintDate = dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString();
                    p_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                }
                //返回
                return lngRes;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }	//返回
            return lngRes;
        }
        #endregion

        #region 获取指定的已删除记录
        /// <summary>
        /// 获取指定的已删除记录
        /// </summary>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenRecordTime"></param>
        /// <param name="p_objHRPServ"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngGetDeleteRecordContentWithServ(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenRecordTime,
            clsHRPTableService p_objHRPServ,
            out clsBaseCaseHistoryInfo p_objRecordContent)
        {
            #region SQL
            string strGetDeletedRecordContentSQLByRegisterID = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.operationdate,
       a.operationready,
       a.laborinduction,
       a.medicname,
       a.medicdose,
       a.diluentdose,
       a.mediclot,
       a.abdominalpuncture,
       a.noneedle,
       a.puncturesize,
       a.amniotic,
       a.amnioticcolor,
       a.amnioticother,
       a.vaginalcervix,
       a.insertiondepth,
       a.meilan,
       a.vaginalgauze,
       a.cystictime,
       a.outof,
       a.aftersurgery,
       a.surgicalbleeding,
       a.notes,
       a.contractionstime,
       a.brokenwatertime,
       a.fetalditime,
       a.fetaldimethod,
       a.fetal,
       a.fetallength,
       a.fetalweight,
       a.placenta,
       a.palace,
       a.palacecontent,
       a.postpartumbleeding,
       a.contractionsagent,
       a.dose,
       a.birthcheck,
       a.shexiang,
       a.treatment,
       a.surgeon,
       a.handler,
       a.modifydate,
       a.modifyuserid,
       a.sequence_int,
       tbe.lastname_vchr
  from gestationmisbirthsthree a, t_bse_employee tbe
 where a.inpatientid = ? and a.inpatientdate = ?
   and a.status = 1
   and a.createuserid = tbe.empno_chr";
            #endregion
            p_objRecordContent = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);


                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = objHRPServ.lngGetDataTableWithParameters(strGetDeletedRecordContentSQLByRegisterID, ref dtbValue, objDPArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsGestationMisbirthsthreeRelationVO p_objContent = new clsGestationMisbirthsthreeRelationVO();

                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        #region 获取结果
                        p_objContent.m_strInPatientID = p_strInPatientID;
                        p_objContent.m_strInPatientID = dtbValue.Rows[i]["INPATIENTID"].ToString(); ;
                        p_objContent.m_dtmInPatientDate = Convert.ToDateTime(dtbValue.Rows[i]["inpatientdate"]);
                        p_objContent.m_dtmDeActivedDate = Convert.ToDateTime(dtbValue.Rows[i]["DEACTIVEDDATE"]);
                        p_objContent.m_strDeActivedOperatorID = dtbValue.Rows[i]["DEACTIVEDOPERATORID"].ToString();
                        p_objContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[i]["OPENDATE"]);
                        p_objContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
                        p_objContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();

                        if (dtbValue.Rows[i]["FIRSTPRINTDATE"].ToString() == "")
                            p_objContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else p_objContent.m_dtmFirstPrintDate = Convert.ToDateTime(dtbValue.Rows[i]["FIRSTPRINTDATE"]);
                        if (dtbValue.Rows[i]["STATUS"].ToString() == "")
                            p_objContent.m_bytStatus = 0;
                        else p_objContent.m_bytStatus = Byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());
                        p_objContent.m_strOPERATIONDATE = dtbValue.Rows[i]["OPERATIONDATE"].ToString().Trim();
                        p_objContent.m_strOPERATIONREADY = dtbValue.Rows[i]["OPERATIONREADY"].ToString().Trim();
                        p_objContent.m_strLABORINDUCTION = dtbValue.Rows[i]["LABORINDUCTION"].ToString().Trim();
                        p_objContent.m_strMEDICNAME = dtbValue.Rows[i]["MEDICNAME"].ToString().Trim();
                        p_objContent.m_strMEDICDOSE = dtbValue.Rows[i]["MEDICDOSE"].ToString().Trim();
                        p_objContent.m_strDILUENTDOSE = dtbValue.Rows[i]["DILUENTDOSE"].ToString().Trim();
                        p_objContent.m_strMEDICLOT = dtbValue.Rows[i]["MEDICLOT"].ToString().Trim();
                        p_objContent.m_strABDOMINALPUNCTURE = dtbValue.Rows[i]["ABDOMINALPUNCTURE"].ToString().Trim();
                        p_objContent.m_strNONEEDLE = dtbValue.Rows[i]["NONEEDLE"].ToString().Trim();
                        p_objContent.m_strPUNCTURESIZE = dtbValue.Rows[i]["PUNCTURESIZE"].ToString().Trim();
                        p_objContent.m_strAMNIOTIC = dtbValue.Rows[i]["AMNIOTIC"].ToString().Trim();
                        p_objContent.m_strAMNIOTICCOLOR = dtbValue.Rows[i]["AMNIOTICCOLOR"].ToString().Trim();
                        p_objContent.m_strAMNIOTICOTHER = dtbValue.Rows[i]["AMNIOTICOTHER"].ToString().Trim();
                        p_objContent.m_strVAGINALCERVIX = dtbValue.Rows[i]["VAGINALCERVIX"].ToString().Trim();
                        p_objContent.m_strINSERTIONDEPTH = dtbValue.Rows[i]["INSERTIONDEPTH"].ToString().Trim();
                        p_objContent.m_strMEILAN = dtbValue.Rows[i]["MEILAN"].ToString().Trim();
                        p_objContent.m_strVAGINALGAUZE = dtbValue.Rows[i]["VAGINALGAUZE"].ToString().Trim();
                        p_objContent.m_strCYSTICTIME = dtbValue.Rows[i]["CYSTICTIME"].ToString().Trim();
                        p_objContent.m_strOUTOF = dtbValue.Rows[i]["OUTOF"].ToString().Trim();
                        p_objContent.m_strAFTERSURGERY = dtbValue.Rows[i]["AFTERSURGERY"].ToString().Trim();
                        p_objContent.m_strSURGICALBLEEDING = dtbValue.Rows[i]["SURGICALBLEEDING"].ToString().Trim();
                        p_objContent.m_strNOTES = dtbValue.Rows[i]["NOTES"].ToString().Trim();
                        p_objContent.m_strCONTRACTIONSTIME = dtbValue.Rows[i]["CONTRACTIONSTIME"].ToString().Trim();
                        p_objContent.m_strBROKENWATERTIME = dtbValue.Rows[i]["BROKENWATERTIME"].ToString().Trim();
                        p_objContent.m_strFETALDITIME = dtbValue.Rows[i]["FETALDITIME"].ToString().Trim();
                        p_objContent.m_strFETALDIMETHOD = dtbValue.Rows[i]["FETALDIMETHOD"].ToString().Trim();
                        p_objContent.m_strFETAL = dtbValue.Rows[i]["FETAL"].ToString().Trim();
                        p_objContent.m_strFETALLENGTH = dtbValue.Rows[i]["FETALLENGTH"].ToString().Trim();
                        p_objContent.m_strFETALWEIGHT = dtbValue.Rows[i]["FETALWEIGHT"].ToString().Trim();
                        p_objContent.m_strPLACENTA = dtbValue.Rows[i]["PLACENTA"].ToString().Trim();
                        p_objContent.m_strPALACE = dtbValue.Rows[i]["PALACE"].ToString().Trim();
                        p_objContent.m_strPALACECONTENT = dtbValue.Rows[i]["PALACECONTENT"].ToString().Trim();
                        p_objContent.m_strPOSTPARTUMBLEEDING = dtbValue.Rows[i]["POSTPARTUMBLEEDING"].ToString().Trim();
                        p_objContent.m_strCONTRACTIONSAGENT = dtbValue.Rows[i]["CONTRACTIONSAGENT"].ToString().Trim();
                        p_objContent.m_strDOSE = dtbValue.Rows[i]["DOSE"].ToString().Trim();
                        p_objContent.m_strBIRTHCHECK = dtbValue.Rows[i]["BIRTHCHECK"].ToString().Trim();
                        p_objContent.m_strSHEXIANG = dtbValue.Rows[i]["SHEXIANG"].ToString().Trim();
                        p_objContent.m_strTREATMENT = dtbValue.Rows[i]["TREATMENT"].ToString().Trim();
                        p_objContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
                        p_objContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
                        //获取签名集合
                        if (dtbValue.Rows[i]["SEQUENCE_INT"] != DBNull.Value)
                        {
                            long lngS = long.Parse(dtbValue.Rows[0]["SEQUENCE_INT"].ToString());
                            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                            long lngTemp = objSign.m_lngGetSign(lngS, out p_objContent.objSignerArr);

                            //释放
                            objSign = null;
                        }
                        #endregion
                    }
                    p_objRecordContent = p_objContent;
                }

                //返回
                return lngRes;

            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }		//返回
            return lngRes;

        }
        #endregion

        #region SQL
        /// <summary>
        /// 更新GeneralDiseaseRecord中FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = @"update gestationmisbirthsthree
   set firstprintdate = ?
 where inpatientid = ? and inpatientdate = ?
   and opendate = ?
   and firstprintdate is null
   and status = 0
";

        private const string c_strModifyRecordSQL = @"update gestationmisbirthsthree
   set modifydate         = ?,
       modifyuserid       = ?,
       operationdate      = ?,
       operationready     = ?,
       laborinduction     = ?,
       medicname          = ?,
       medicdose          = ?,
       diluentdose        = ?,
       mediclot           = ?,
       abdominalpuncture  = ?,
       noneedle           = ?,
       puncturesize       = ?,
       amniotic           = ?,
       amnioticcolor      = ?,
       amnioticother      = ?,
       vaginalcervix      = ?,
       insertiondepth     = ?,
       meilan             = ?,
       vaginalgauze       = ?,
       cystictime         = ?,
       outof              = ?,
       aftersurgery       = ?,
       surgicalbleeding   = ?,
       notes              = ?,
       contractionstime   = ?,
       brokenwatertime    = ?,
       fetalditime        = ?,
       fetaldimethod      = ?,
       fetal              = ?,
       fetallength        = ?,
       fetalweight        = ?,
       placenta           = ?,
       palace             = ?,
       palacecontent      = ?,
       postpartumbleeding = ?,
       contractionsagent  = ?,
       dose               = ?,
       birthcheck         = ?,
       shexiang           = ?,
       treatment          = ?,
       surgeon            = ?,
       handler            = ?,
       notes_xml          = ?,
       shexiang_xml       = ?,
       treatment_xml      = ?,
       sequence_int       = ?
 where inpatientid = ? and inpatientdate = ?
   and opendate = ?
   and status = 0
";

        private const string c_strDeleteRecordSQL = @"update gestationmisbirthsthree
   set status = 1 , deactiveddate = ?, deactivedoperatorid = ?
 where inpatientid = ? and inpatientdate = ?
   and opendate = ?
   and status = 0
";

        private const string c_strAddNewRecordSQL = @"insert into gestationmisbirthsthree
  (inpatientid,             --0
   inpatientdate,           --1
   opendate,                --2
   createdate,              --3
   createuserid,            --4
   status,                  --5
   modifydate,              --6
   modifyuserid,            --7
   operationdate,
   operationready,
   laborinduction,
   medicname,
   medicdose,
   diluentdose,
   mediclot,
   abdominalpuncture,
   noneedle,
   puncturesize,
   amniotic,
   amnioticcolor,
   amnioticother,
   vaginalcervix,
   insertiondepth,
   meilan,
   vaginalgauze,
   cystictime,
   outof,
   aftersurgery,
   surgicalbleeding,
   notes,
   contractionstime,
   brokenwatertime,
   fetalditime,
   fetaldimethod,
   fetal,
   fetallength,
   fetalweight,
   placenta,
   palace,
   palacecontent,
   postpartumbleeding,
   contractionsagent,
   dose,
   birthcheck,
   shexiang,
   treatment,
   surgeon,
   handler,
   notes_xml,
   shexiang_xml,
   treatment_xml,
   sequence_int,            --68
registerid_chr                  --69
)
values (
?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,
?,?,?,?,?,?,?,?,?,?,
?,?,?)";

        private const string c_strAddNewRecordSQL_Con = @"insert into gestationmisbirthsthree_con
  (inpatientid,
   inpatientdate,
   opendate,
   modifydate,
   modifyuserid,
   notes_right,
   shexiang_right,
   treatment_right)
values
  (?, ?, ?, ?, ?, ?, ?, ?)";
        #endregion

        #region 更新数据库中的首次打印时间
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            long lngRes = 0;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsNewBabyInRoomRecordService","m_lngUpdateFirstPrintDate");
            //if(lngCheckRes <= 0)
            //return lngCheckRes;	

            //检查参数                              
            if (p_strInPatientID == null || p_strInPatientID == "" || p_strInPatientDate == null || p_strInPatientDate == "" || p_strOpenDate == null || p_strOpenDate == "")
                return (long)enmOperationResult.Parameter_Error;
            //把p_objRecordContent造型为需要使用的子类，以使用对应的字段值			

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strOpenDate);

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);

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
        #endregion

        #region 添加记录
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objPicValueArr"></param>
        /// <param name="p_strDiseaseID"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngAddNewRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
            clsPictureBoxValue[] p_objPicValueArr, string p_strDiseaseID, string p_strDeptID,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;

            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngSequence = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
            clsGestationMisbirthsthreeRelationVO m_objContent = (clsGestationMisbirthsthreeRelationVO)p_objRecordContent;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(53, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = m_objContent.m_strInPatientID;
                objLisAddItemRefArr[1].DbType = DbType.DateTime;
                objLisAddItemRefArr[1].Value = m_objContent.m_dtmInPatientDate;
                objLisAddItemRefArr[2].DbType = DbType.DateTime;
                objLisAddItemRefArr[2].Value = m_objContent.m_dtmOpenDate;
                objLisAddItemRefArr[3].DbType = DbType.DateTime;
                objLisAddItemRefArr[3].Value = m_objContent.m_dtmCreateDate;
                objLisAddItemRefArr[4].Value = m_objContent.m_strCreateUserID;
                objLisAddItemRefArr[5].Value = 0;
                objLisAddItemRefArr[6].DbType = DbType.DateTime;
                objLisAddItemRefArr[6].Value = m_objContent.m_dtmModifyDate;
                objLisAddItemRefArr[7].Value = m_objContent.m_strModifyUserID;
                objLisAddItemRefArr[8].Value = m_objContent.m_strOPERATIONDATE;
                objLisAddItemRefArr[9].Value = m_objContent.m_strOPERATIONREADY;
                objLisAddItemRefArr[10].Value = m_objContent.m_strLABORINDUCTION;
                objLisAddItemRefArr[11].Value = m_objContent.m_strMEDICNAME;
                objLisAddItemRefArr[12].Value = m_objContent.m_strMEDICDOSE;
                objLisAddItemRefArr[13].Value = m_objContent.m_strDILUENTDOSE;
                objLisAddItemRefArr[14].Value = m_objContent.m_strMEDICLOT;
                objLisAddItemRefArr[15].Value = m_objContent.m_strABDOMINALPUNCTURE;
                objLisAddItemRefArr[16].Value = m_objContent.m_strNONEEDLE;
                objLisAddItemRefArr[17].Value = m_objContent.m_strPUNCTURESIZE;
                objLisAddItemRefArr[18].Value = m_objContent.m_strAMNIOTIC;
                objLisAddItemRefArr[19].Value = m_objContent.m_strAMNIOTICCOLOR;
                objLisAddItemRefArr[20].Value = m_objContent.m_strAMNIOTICOTHER;
                objLisAddItemRefArr[21].Value = m_objContent.m_strVAGINALCERVIX;
                objLisAddItemRefArr[22].Value = m_objContent.m_strINSERTIONDEPTH;
                objLisAddItemRefArr[23].Value = m_objContent.m_strMEILAN;
                objLisAddItemRefArr[24].Value = m_objContent.m_strVAGINALGAUZE;
                objLisAddItemRefArr[25].Value = m_objContent.m_strCYSTICTIME;
                objLisAddItemRefArr[26].Value = m_objContent.m_strOUTOF;
                objLisAddItemRefArr[27].Value = m_objContent.m_strAFTERSURGERY;
                objLisAddItemRefArr[28].Value = m_objContent.m_strSURGICALBLEEDING;
                objLisAddItemRefArr[29].Value = m_objContent.m_strNOTES;
                objLisAddItemRefArr[30].Value = m_objContent.m_strCONTRACTIONSTIME;
                objLisAddItemRefArr[31].Value = m_objContent.m_strBROKENWATERTIME;
                objLisAddItemRefArr[32].Value = m_objContent.m_strFETALDITIME;
                objLisAddItemRefArr[33].Value = m_objContent.m_strFETALDIMETHOD;
                objLisAddItemRefArr[34].Value = m_objContent.m_strFETAL;
                objLisAddItemRefArr[35].Value = m_objContent.m_strFETALLENGTH;
                objLisAddItemRefArr[36].Value = m_objContent.m_strFETALWEIGHT;
                objLisAddItemRefArr[37].Value = m_objContent.m_strPLACENTA;
                objLisAddItemRefArr[38].Value = m_objContent.m_strPALACE;
                objLisAddItemRefArr[39].Value = m_objContent.m_strPALACECONTENT;
                objLisAddItemRefArr[40].Value = m_objContent.m_strPOSTPARTUMBLEEDING;
                objLisAddItemRefArr[41].Value = m_objContent.m_strCONTRACTIONSAGENT;
                objLisAddItemRefArr[42].Value = m_objContent.m_strDOSE;
                objLisAddItemRefArr[43].Value = m_objContent.m_strBIRTHCHECK;
                objLisAddItemRefArr[44].Value = m_objContent.m_strSHEXIANG;
                objLisAddItemRefArr[45].Value = m_objContent.m_strTREATMENT;
                objLisAddItemRefArr[46].Value = "";//m_objContent.m_strSURGEON;
                objLisAddItemRefArr[47].Value = "";// m_objContent.m_strHANDLER;
                objLisAddItemRefArr[48].Value = m_objContent.m_strNOTES_XML;
                objLisAddItemRefArr[49].Value = m_objContent.m_strSHEXIANG_XML;
                objLisAddItemRefArr[50].Value = m_objContent.m_strTREATMENT_XML;
                objLisAddItemRefArr[51].Value = lngSequence;
                objLisAddItemRefArr[52].Value = "";

                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngRecEff, objLisAddItemRefArr);
                objHRPServ.Dispose();
                objHRPServ = null;
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(m_objContent.objSignerArr, lngSequence);

                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(8, out objDPArr2);
                objDPArr2[0].Value = m_objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = m_objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = m_objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = m_objContent.m_dtmModifyDate;
                objDPArr2[4].Value = m_objContent.m_strModifyUserID;
                objDPArr2[5].Value = m_objContent.m_strNOTES_RIGHT;
                objDPArr2[6].Value = m_objContent.m_strSHEXIANG_RIGHT;
                objDPArr2[7].Value = m_objContent.m_strTREATMENT_RIGHT;
                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL_Con, ref lngRecEff, objDPArr2);
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
        #endregion

        #region 修改内容
        /// <summary>
        /// 修改内容
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objPicValueArr"></param>
        /// <param name="p_strDiseaseID"></param>
        /// <param name="p_strDeptID"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngModifyRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
            clsPictureBoxValue[] p_objPicValueArr, string p_strDiseaseID, string p_strDeptID,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;

            //检查参数                              
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                return (long)enmOperationResult.Parameter_Error;
            long lngSequence = 0;
            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
            lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);
            clsGestationMisbirthsthreeRelationVO m_objContent = (clsGestationMisbirthsthreeRelationVO)p_objRecordContent;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(49, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].DbType = DbType.DateTime;
                objLisAddItemRefArr[0].Value = m_objContent.m_dtmModifyDate;
                objLisAddItemRefArr[1].Value = m_objContent.m_strModifyUserID;
                objLisAddItemRefArr[2].Value = m_objContent.m_strOPERATIONDATE;
                objLisAddItemRefArr[3].Value = m_objContent.m_strOPERATIONREADY;
                objLisAddItemRefArr[4].Value = m_objContent.m_strLABORINDUCTION;
                objLisAddItemRefArr[5].Value = m_objContent.m_strMEDICNAME;
                objLisAddItemRefArr[6].Value = m_objContent.m_strMEDICDOSE;
                objLisAddItemRefArr[7].Value = m_objContent.m_strDILUENTDOSE;
                objLisAddItemRefArr[8].Value = m_objContent.m_strMEDICLOT;
                objLisAddItemRefArr[9].Value = m_objContent.m_strABDOMINALPUNCTURE;
                objLisAddItemRefArr[10].Value = m_objContent.m_strNONEEDLE;
                objLisAddItemRefArr[11].Value = m_objContent.m_strPUNCTURESIZE;
                objLisAddItemRefArr[12].Value = m_objContent.m_strAMNIOTIC;
                objLisAddItemRefArr[13].Value = m_objContent.m_strAMNIOTICCOLOR;
                objLisAddItemRefArr[14].Value = m_objContent.m_strAMNIOTICOTHER;
                objLisAddItemRefArr[15].Value = m_objContent.m_strVAGINALCERVIX;
                objLisAddItemRefArr[16].Value = m_objContent.m_strINSERTIONDEPTH;
                objLisAddItemRefArr[17].Value = m_objContent.m_strMEILAN;
                objLisAddItemRefArr[18].Value = m_objContent.m_strVAGINALGAUZE;
                objLisAddItemRefArr[19].Value = m_objContent.m_strCYSTICTIME;
                objLisAddItemRefArr[20].Value = m_objContent.m_strOUTOF;
                objLisAddItemRefArr[21].Value = m_objContent.m_strAFTERSURGERY;
                objLisAddItemRefArr[22].Value = m_objContent.m_strSURGICALBLEEDING;
                objLisAddItemRefArr[23].Value = m_objContent.m_strNOTES;
                objLisAddItemRefArr[24].Value = m_objContent.m_strCONTRACTIONSTIME;
                objLisAddItemRefArr[25].Value = m_objContent.m_strBROKENWATERTIME;
                objLisAddItemRefArr[26].Value = m_objContent.m_strFETALDITIME;
                objLisAddItemRefArr[27].Value = m_objContent.m_strFETALDIMETHOD;
                objLisAddItemRefArr[28].Value = m_objContent.m_strFETAL;
                objLisAddItemRefArr[29].Value = m_objContent.m_strFETALLENGTH;
                objLisAddItemRefArr[30].Value = m_objContent.m_strFETALWEIGHT;
                objLisAddItemRefArr[31].Value = m_objContent.m_strPLACENTA;
                objLisAddItemRefArr[32].Value = m_objContent.m_strPALACE;
                objLisAddItemRefArr[33].Value = m_objContent.m_strPALACECONTENT;
                objLisAddItemRefArr[34].Value = m_objContent.m_strPOSTPARTUMBLEEDING;
                objLisAddItemRefArr[35].Value = m_objContent.m_strCONTRACTIONSAGENT;
                objLisAddItemRefArr[36].Value = m_objContent.m_strDOSE;
                objLisAddItemRefArr[37].Value = m_objContent.m_strBIRTHCHECK;
                objLisAddItemRefArr[38].Value = m_objContent.m_strSHEXIANG;
                objLisAddItemRefArr[39].Value = m_objContent.m_strTREATMENT;
                objLisAddItemRefArr[40].Value = "";//m_objContent.m_strSURGEON;
                objLisAddItemRefArr[41].Value = ""; //m_objContent.m_strHANDLER;
                objLisAddItemRefArr[42].Value = m_objContent.m_strNOTES_XML;
                objLisAddItemRefArr[43].Value = m_objContent.m_strSHEXIANG_XML;
                objLisAddItemRefArr[44].Value = m_objContent.m_strTREATMENT_XML;
                objLisAddItemRefArr[45].Value = lngSequence;
                objLisAddItemRefArr[46].Value = m_objContent.m_strInPatientID;
                objLisAddItemRefArr[47].DbType = DbType.DateTime;
                objLisAddItemRefArr[47].Value = m_objContent.m_dtmInPatientDate;
                objLisAddItemRefArr[48].DbType = DbType.DateTime;
                objLisAddItemRefArr[48].Value = m_objContent.m_dtmOpenDate;

                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objLisAddItemRefArr);
                objHRPServ.Dispose();
                objHRPServ = null;

                if (lngRes <= 0) return lngRes;
                //保存签名集合
                lngRes = objSign.m_lngAddSign(m_objContent.objSignerArr, lngSequence);

                IDataParameter[] objDPArr2 = null;
                p_objHRPServ.CreateDatabaseParameter(8, out objDPArr2);
                objDPArr2[0].Value = m_objContent.m_strInPatientID;
                objDPArr2[1].DbType = DbType.DateTime;
                objDPArr2[1].Value = m_objContent.m_dtmInPatientDate;
                objDPArr2[2].DbType = DbType.DateTime;
                objDPArr2[2].Value = m_objContent.m_dtmOpenDate;
                objDPArr2[3].DbType = DbType.DateTime;
                objDPArr2[3].Value = m_objContent.m_dtmModifyDate;
                objDPArr2[4].Value = m_objContent.m_strModifyUserID;
                objDPArr2[5].Value = m_objContent.m_strNOTES_RIGHT;
                objDPArr2[6].Value = m_objContent.m_strSHEXIANG_RIGHT;
                objDPArr2[7].Value = m_objContent.m_strTREATMENT_RIGHT;

                //执行SQL			
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL_Con, ref lngEff, objDPArr2);
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
        #endregion

        #region 删除记录
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <param name="p_objHRPServ"></param>
        /// <returns></returns>
        [AutoComplete]
        protected override long m_lngDeleteRecord2DB(clsBaseCaseHistoryInfo p_objRecordContent,
            clsHRPTableService p_objHRPServ)
        {
            long lngRes = 0;


            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_strInPatientID == null)
                return (long)enmOperationResult.Parameter_Error;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
                objDPArr[4].Value = p_objRecordContent.m_dtmOpenDate;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);
                if (lngEff < 0) return -1;
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
        #endregion
    }
}
