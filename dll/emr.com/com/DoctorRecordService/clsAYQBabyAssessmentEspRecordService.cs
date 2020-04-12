
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
    /// 爱婴区婴儿评估表
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsAYQBabyAssessmenEspRecordService : clsBaseCaseHistorySevice
    {
        public clsAYQBabyAssessmenEspRecordService()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private const string c_strGetTimeListSQL = "select inpatientdate,createdate,opendate from ayqbabyassessmentesprec where inpatientid = ?  and status=0 order by opendate desc";

        private const string c_strGetRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.modifydate,
       a.modifyuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.esprecordcontent,
       a.esprecordcontentxml,
       a.recorddate,
a.recordsigh,
a.esprecordcontent2,
       a.esprecordcontentxml2,
       a.recorddate2,
a.recordsigh2,
a.esprecordcontent3,
       a.esprecordcontentxml3,
       a.recorddate3,
a.recordsigh3,
a.esprecordcontent4,
       a.esprecordcontentxml4,
       a.recorddate4,
a.recordsigh4
  from ayqbabyassessmentesprec a, t_bse_employee tbe
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and a.createuserid = tbe.empno_chr";

        private const string c_strGetDeletedRecordContentSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createuserid,
       a.modifydate,
       a.modifyuserid,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
        a.esprecordcontent,
       a.esprecordcontentxml,
       a.recorddate,
a.recordsigh,
a.esprecordcontent2,
       a.esprecordcontentxml2,
       a.recorddate2,
a.recordsigh2,
a.esprecordcontent3,
       a.esprecordcontentxml3,
       a.recorddate3,
a.recordsigh3,
a.esprecordcontent4,
       a.esprecordcontentxml4,
       a.recorddate4,
a.recordsigh4
  from ayqbabyassessmentesprec a, t_bse_employee tbe
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 1
   and a.createuserid = tbe.empno_chr";


        private const string c_strCheckCreateDateSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       modifydate,
       modifyuserid,
       status,
       deactiveddate,
       deactivedoperatorid,
       firstprintdate,
        a.esprecordcontent,
       a.esprecordcontentxml,
       a.recorddate,
a.recordsigh,
a.esprecordcontent2,
       a.esprecordcontentxml2,
       a.recorddate2,
a.recordsigh2,
a.esprecordcontent3,
       a.esprecordcontentxml3,
       a.recorddate3,
a.recordsigh3,
a.esprecordcontent4,
       a.esprecordcontentxml4,
       a.recorddate4,
a.recordsigh4
  from ayqbabyassessmentesprec
 where inpatientid = ?
   and inpatientdate = ?
   and status = 0";


        /// <summary>
        /// 更新GeneralDiseaseRecord中FirstPrintDate
        /// </summary>
        private const string c_strUpdateFirstPrintDateSQL = "update  ayqbabyassessmentesprec set firstprintdate= ? where inpatientid= ? and inpatientdate= ? and opendate=? and firstprintdate is null and status=0";

        private const string c_strGetDeleteRecordSQL = "";

        private const string c_strGetModifyDateAndFirstPrintDateSQL = @"select firstprintdate,modifydate
																			from ayqbabyassessmentesprec
																			where inpatientid = ?
																			and inpatientdate = ?
																			and status = 0";

        private const string c_strModifyRecordSQL = @"update ayqbabyassessmentesprec set modifydate=?,modifyuserid=?,esprecordcontent =?,esprecordcontentxml =?,recorddate =?,recordsigh =?,
esprecordcontent2 =?,esprecordcontentxml2 =?,recorddate2 =?,recordsigh2 =?,
esprecordcontent3 =?,esprecordcontentxml3 =?,recorddate3 =?,recordsigh3 =?,
esprecordcontent4 =?,esprecordcontentxml4 =?,recorddate4 =?,recordsigh4 =?
			where inpatientid=? and inpatientdate=? and opendate=? and status=0";//21

        private const string c_strDeleteRecordSQL = "update ayqbabyassessmentesprec set status=1,deactiveddate=?,deactivedoperatorid=? where inpatientid=? and inpatientdate=? and opendate=? and status=0";

        private const string c_strAddNewRecordSQL = @"insert into ayqbabyassessmentesprec (inpatientid,inpatientdate,opendate,createdate,
			createuserid,modifydate,modifyuserid,status,esprecordcontent,esprecordcontentxml,recorddate,recordsigh,
esprecordcontent2,esprecordcontentxml2,recorddate2,recordsigh2,
esprecordcontent3,esprecordcontentxml3,recorddate3,recordsigh3,
esprecordcontent4,esprecordcontentxml4,recorddate4,recordsigh4
) 
			values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";//24

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
            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsayqbabyassessmentesprecService","m_lngGetRecordTimeList");
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

            }       //返回
            return lngRes;


        }

        /// <summary>
        ///  更新数据库中的首次打印时间。
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        /// <param name="p_dtmFirstPrintDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public override long m_lngUpdateFirstPrintDate(
            string p_strInPatientID,
            string p_strInPatientDate,
            string p_strOpenDate,
            DateTime p_dtmFirstPrintDate)
        {
            long lngRes = 0;

            //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsayqbabyassessmentesprecService","m_lngUpdateFirstPrintDate");
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsayqbabyassessmentesprecService","m_lngGetDeleteRecordTimeList");
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
            p_objRecordContent = null;
            p_objPicValueArr = null;
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
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsAYQBabyAssessmentContent_EspRecord p_objContent = new clsAYQBabyAssessmentContent_EspRecord();

                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        #region 获取结果
                        p_objContent.m_strInPatientID = p_strInPatientID;
                        p_objContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);
                        p_objContent.m_dtmOpenDate = Convert.ToDateTime(dtbValue.Rows[i]["OPENDATE"]);
                        p_objContent.m_dtmCreateDate = Convert.ToDateTime(dtbValue.Rows[i]["CREATEDATE"]);
                        p_objContent.m_strCreateUserID = dtbValue.Rows[i]["CREATEUSERID"].ToString();

                        if (dtbValue.Rows[i]["FIRSTPRINTDATE"].ToString() == "")
                            p_objContent.m_dtmFirstPrintDate = DateTime.MinValue;
                        else p_objContent.m_dtmFirstPrintDate = Convert.ToDateTime(dtbValue.Rows[i]["FIRSTPRINTDATE"]);
                        if (dtbValue.Rows[i]["STATUS"].ToString() == "")
                            p_objContent.m_bytStatus = 0;
                        else p_objContent.m_bytStatus = Byte.Parse(dtbValue.Rows[i]["STATUS"].ToString());

                        p_objContent.m_strEspRecord = dtbValue.Rows[i]["esprecordcontent"].ToString();
                        p_objContent.m_strEspRecordXML = dtbValue.Rows[i]["esprecordcontentxml"].ToString();
                        p_objContent.m_strRecordSign = dtbValue.Rows[i]["recordsigh"].ToString();
                        p_objContent.m_dtmRecordDate = dtbValue.Rows[i]["recorddate"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dtbValue.Rows[i]["recorddate"].ToString());

                        p_objContent.m_strEspRecord2 = dtbValue.Rows[i]["esprecordcontent2"].ToString();
                        p_objContent.m_strEspRecordXML2 = dtbValue.Rows[i]["esprecordcontentxml2"].ToString();
                        p_objContent.m_strRecordSign2 = dtbValue.Rows[i]["recordsigh2"].ToString();
                        p_objContent.m_dtmRecordDate2 = dtbValue.Rows[i]["recorddate2"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dtbValue.Rows[i]["recorddate2"].ToString());

                        p_objContent.m_strEspRecord3 = dtbValue.Rows[i]["esprecordcontent3"].ToString();
                        p_objContent.m_strEspRecordXML3 = dtbValue.Rows[i]["esprecordcontentxml3"].ToString();
                        p_objContent.m_strRecordSign3 = dtbValue.Rows[i]["recordsigh3"].ToString();
                        p_objContent.m_dtmRecordDate3 = dtbValue.Rows[i]["recorddate3"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dtbValue.Rows[i]["recorddate3"].ToString());

                        p_objContent.m_strEspRecord4 = dtbValue.Rows[i]["esprecordcontent4"].ToString();
                        p_objContent.m_strEspRecordXML4 = dtbValue.Rows[i]["esprecordcontentxml4"].ToString();
                        p_objContent.m_strRecordSign4 = dtbValue.Rows[i]["recordsigh4"].ToString();
                        p_objContent.m_dtmRecordDate4 = dtbValue.Rows[i]["recorddate4"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dtbValue.Rows[i]["recorddate4"].ToString());

                        p_objContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
                        p_objContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
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
                //objHRPServ.Dispose();

            }           //返回
            return lngRes;

        }

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
                c_strCheckLastModifyRecordSQL = "select top 1 modifydate,modifyuserid from ayqbabyassessmentesprec where inpatientid = ? and inpatientdate= ? and opendate= ? and status=0 order by modifydate desc";
            }
            else
            {
                c_strCheckLastModifyRecordSQL = @"select modifydate, modifyuserid
  from (select modifydate, modifyuserid
          from ayqbabyassessmentesprec
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

            clsAYQBabyAssessmentContent_EspRecord m_objContent = (clsAYQBabyAssessmentContent_EspRecord)p_objRecordContent;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(24, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].Value = m_objContent.m_strInPatientID;
                objLisAddItemRefArr[1].DbType = DbType.DateTime;
                objLisAddItemRefArr[1].Value = m_objContent.m_dtmInPatientDate;
                objLisAddItemRefArr[2].DbType = DbType.DateTime;
                objLisAddItemRefArr[2].Value = m_objContent.m_dtmOpenDate;
                objLisAddItemRefArr[3].DbType = DbType.DateTime;
                objLisAddItemRefArr[3].Value = m_objContent.m_dtmCreateDate;
                objLisAddItemRefArr[4].Value = m_objContent.m_strCreateUserID;
                objLisAddItemRefArr[5].DbType = DbType.DateTime;
                objLisAddItemRefArr[5].Value = m_objContent.m_dtmModifyDate;
                objLisAddItemRefArr[6].Value = m_objContent.m_strModifyUserID;
                objLisAddItemRefArr[7].Value = 0;
                objLisAddItemRefArr[8].Value = m_objContent.m_strEspRecord;
                objLisAddItemRefArr[9].Value = m_objContent.m_strEspRecordXML;
                objLisAddItemRefArr[10].DbType = DbType.DateTime;
                objLisAddItemRefArr[10].Value = m_objContent.m_dtmRecordDate;
                objLisAddItemRefArr[11].Value = m_objContent.m_strRecordSign;

                objLisAddItemRefArr[12].Value = m_objContent.m_strEspRecord2;
                objLisAddItemRefArr[13].Value = m_objContent.m_strEspRecordXML2;
                objLisAddItemRefArr[14].DbType = DbType.DateTime;
                objLisAddItemRefArr[14].Value = m_objContent.m_dtmRecordDate2;
                objLisAddItemRefArr[15].Value = m_objContent.m_strRecordSign2;

                objLisAddItemRefArr[16].Value = m_objContent.m_strEspRecord3;
                objLisAddItemRefArr[17].Value = m_objContent.m_strEspRecordXML3;
                objLisAddItemRefArr[18].DbType = DbType.DateTime;
                objLisAddItemRefArr[18].Value = m_objContent.m_dtmRecordDate3;
                objLisAddItemRefArr[19].Value = m_objContent.m_strRecordSign3;

                objLisAddItemRefArr[20].Value = m_objContent.m_strEspRecord4;
                objLisAddItemRefArr[21].Value = m_objContent.m_strEspRecordXML4;
                objLisAddItemRefArr[22].DbType = DbType.DateTime;
                objLisAddItemRefArr[22].Value = m_objContent.m_dtmRecordDate4;
                objLisAddItemRefArr[23].Value = m_objContent.m_strRecordSign4;
                long lngRecEff = -1;
                //往表增加记录
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewRecordSQL, ref lngRecEff, objLisAddItemRefArr);
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

            clsAYQBabyAssessmentContent_EspRecord m_objContent = (clsAYQBabyAssessmentContent_EspRecord)p_objRecordContent;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objLisAddItemRefArr = null;
                objHRPServ.CreateDatabaseParameter(21, out objLisAddItemRefArr);

                objLisAddItemRefArr[0].DbType = DbType.DateTime;
                objLisAddItemRefArr[0].Value = m_objContent.m_dtmModifyDate;
                objLisAddItemRefArr[1].Value = m_objContent.m_strModifyUserID;
                objLisAddItemRefArr[2].Value = m_objContent.m_strEspRecord;
                objLisAddItemRefArr[3].Value = m_objContent.m_strEspRecordXML;
                objLisAddItemRefArr[4].DbType = DbType.DateTime;
                objLisAddItemRefArr[4].Value = m_objContent.m_dtmRecordDate;
                objLisAddItemRefArr[5].Value = m_objContent.m_strRecordSign;

                objLisAddItemRefArr[6].Value = m_objContent.m_strEspRecord2;
                objLisAddItemRefArr[7].Value = m_objContent.m_strEspRecordXML2;
                objLisAddItemRefArr[8].DbType = DbType.DateTime;
                objLisAddItemRefArr[8].Value = m_objContent.m_dtmRecordDate2;
                objLisAddItemRefArr[9].Value = m_objContent.m_strRecordSign2;

                objLisAddItemRefArr[10].Value = m_objContent.m_strEspRecord3;
                objLisAddItemRefArr[11].Value = m_objContent.m_strEspRecordXML3;
                objLisAddItemRefArr[12].DbType = DbType.DateTime;
                objLisAddItemRefArr[12].Value = m_objContent.m_dtmRecordDate3;
                objLisAddItemRefArr[13].Value = m_objContent.m_strRecordSign3;

                objLisAddItemRefArr[14].Value = m_objContent.m_strEspRecord4;
                objLisAddItemRefArr[15].Value = m_objContent.m_strEspRecordXML4;
                objLisAddItemRefArr[16].DbType = DbType.DateTime;
                objLisAddItemRefArr[16].Value = m_objContent.m_dtmRecordDate4;
                objLisAddItemRefArr[17].Value = m_objContent.m_strRecordSign4;

                objLisAddItemRefArr[18].Value = m_objContent.m_strInPatientID;
                objLisAddItemRefArr[19].DbType = DbType.DateTime;
                objLisAddItemRefArr[19].Value = m_objContent.m_dtmInPatientDate;
                objLisAddItemRefArr[20].DbType = DbType.DateTime;
                objLisAddItemRefArr[20].Value = m_objContent.m_dtmOpenDate;
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strModifyRecordSQL, ref lngEff, objLisAddItemRefArr);
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
            /*
			p_dtmModifyDate=DateTime.MinValue ;
			p_strFirstPrintDate=null;
			
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
                if (lngRes > 0 && dtbValue.Rows.Count == 1)
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
             */
            p_dtmModifyDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            p_strFirstPrintDate = "";
            return 1;
        }


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
            p_objRecordContent = null;
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);


                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strOpenRecordTime);

                //生成DataTable
                DataTable dtbValue = new DataTable();

                //执行查询，填充结果到DataTable
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeletedRecordContentSQL, ref dtbValue, objDPArr);

                //从DataTable.Rows中获取结果
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    clsAYQBabyAssessmentContent_EspRecord p_objContent = new clsAYQBabyAssessmentContent_EspRecord();

                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        #region 获取结果
                        p_objContent.m_strInPatientID = p_strInPatientID;
                        p_objContent.m_dtmInPatientDate = Convert.ToDateTime(p_strInPatientDate);
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

                        p_objContent.m_strEspRecord = dtbValue.Rows[i]["esprecordcontent"].ToString();
                        p_objContent.m_strEspRecordXML = dtbValue.Rows[i]["esprecordcontentxml"].ToString();
                        p_objContent.m_strRecordSign = dtbValue.Rows[i]["recordsigh"].ToString();
                        p_objContent.m_dtmRecordDate = dtbValue.Rows[i]["recorddate"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dtbValue.Rows[i]["recorddate"].ToString());

                        p_objContent.m_strEspRecord2 = dtbValue.Rows[i]["esprecordcontent2"].ToString();
                        p_objContent.m_strEspRecordXML2 = dtbValue.Rows[i]["esprecordcontentxml2"].ToString();
                        p_objContent.m_strRecordSign2 = dtbValue.Rows[i]["recordsigh2"].ToString();
                        p_objContent.m_dtmRecordDate2 = dtbValue.Rows[i]["recorddate2"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dtbValue.Rows[i]["recorddate2"].ToString());

                        p_objContent.m_strEspRecord3 = dtbValue.Rows[i]["esprecordcontent3"].ToString();
                        p_objContent.m_strEspRecordXML3 = dtbValue.Rows[i]["esprecordcontentxml3"].ToString();
                        p_objContent.m_strRecordSign3 = dtbValue.Rows[i]["recordsigh3"].ToString();
                        p_objContent.m_dtmRecordDate3 = dtbValue.Rows[i]["recorddate3"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dtbValue.Rows[i]["recorddate3"].ToString());

                        p_objContent.m_strEspRecord4 = dtbValue.Rows[i]["esprecordcontent4"].ToString();
                        p_objContent.m_strEspRecordXML4 = dtbValue.Rows[i]["esprecordcontentxml4"].ToString();
                        p_objContent.m_strRecordSign4 = dtbValue.Rows[i]["recordsigh4"].ToString();
                        p_objContent.m_dtmRecordDate4 = dtbValue.Rows[i]["recorddate4"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dtbValue.Rows[i]["recorddate4"].ToString());


                        p_objContent.m_dtmModifyDate = Convert.ToDateTime(dtbValue.Rows[i]["MODIFYDATE"]);
                        p_objContent.m_strModifyUserID = dtbValue.Rows[i]["MODIFYUSERID"].ToString();
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

            }       //返回
            return lngRes;

        }

    }

}
