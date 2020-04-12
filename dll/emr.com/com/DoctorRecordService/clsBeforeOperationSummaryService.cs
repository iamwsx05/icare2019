using System;
using System.EnterpriseServices;
using System.Data;
using System.Text ;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.PublicMiddleTier;


namespace com.digitalwave.BeforeOperationSummaryService
{
	/// <summary>
	/// ��ǰС��

	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsBeforeOperationSummaryService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		#region FirstPrint
		[AutoComplete] 
		public long m_lngUpdateFirstPrintDate(string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,string p_strFirstPrintDate)
		{	
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBeforeOperationSummaryService", "m_lngUpdateFirstPrintDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //更新第一次打印时�?

                string strCommand = "update beforeoperationsummary set firstprintdate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strFirstPrintDate) + " where firstprintdate is null and status=0 and inpatientid='" + p_strInPatientID + "' and inpatientdate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + " and createdate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCreateDate);
                lngRes = objHRPServ.DoExcute(strCommand);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;
	
		}		

		#endregion 
	
		#region readAllTime
		[AutoComplete] 
		public long m_lngGetOperationDate(string p_strInPatientID,string p_strInPatientDate,out string p_strResultXml,out int p_intResultRows)
		{			
			p_strResultXml="";
			p_intResultRows=0;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBeforeOperationSummaryService", "m_lngGetOperationDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strSQL = @"select main.createdate
									from beforeoperationsummary main
									where main.inpatientid = '" + p_strInPatientID + @"'
									and main.inpatientdate = " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + "" + " and status=0 order by createdate desc ";

                lngRes = objHRPServ.lngGetXMLTable(strSQL, ref p_strResultXml, ref p_intResultRows);


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

		#region checkCreateDate
		/// <summary>
		/// 检查是否已经有记录
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_blnCheckResult"></param>
		/// <returns></returns>
		[AutoComplete] 
		public long m_lngCheckNewCreateDate(string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out bool p_blnIsAddNew)
		{
            string strSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createid,
       diagnosexml,
       diagnosegistxml,
       bodyinfoxml,
       specialhandlexml,
       preparationxml,
       patientnotionxml,
       anaesthesiaxml,
       afternoticexml,
       discussnotionxml,
       ifconfirm,
       confirmreason,
       confirmreasonxmlstring,
       deactiveddate,
       deactivedoperatorid,
       firstprintdate,
       status 
								from beforeoperationsummary
								where inpatientid='" +p_strInPatientID+@"'
								and inpatientdate="+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@"
								and createdate="+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCreateDate)+@"
								and status='0'";
			long lngRes = 0;
			p_blnIsAddNew = false;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                DataTable dtResult = new DataTable();

                lngRes = objHRPServ.DoGetDataTable(strSQL, ref dtResult);

                p_blnIsAddNew = false;

                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBeforeOperationSummaryService", "m_lngCheckNewCreateDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (lngRes <= 0)
                    return lngRes;
                else
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        p_blnIsAddNew = false;
                    }
                    else
                    {
                        p_blnIsAddNew = true;
                    }

                    lngRes = 1;
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
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}
		
		#endregion

		#region addNew
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strMainXml"></param>
		/// <returns>
		/// 操作结果�?

		/// 0：失败�?

		/// 1：成功�?

		/// </returns>
		[AutoComplete] 
		public long m_lngAddNew(string p_strMainXml,string p_strContentXml)
		{
			

			clsHRPTableService objHRPServ = new clsHRPTableService();
			StringBuilder sbdTemp=null;
			string strCurrentTime=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			long lngRes = 0;
             try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBeforeOperationSummaryService", "m_lngAddNew");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                sbdTemp = new StringBuilder(p_strMainXml);
                int intIndex = p_strMainXml.IndexOf(" ");
                sbdTemp.Insert(intIndex, " OpenDate = '" + strCurrentTime + "'");
                p_strMainXml = sbdTemp.ToString();

                lngRes = objHRPServ.add_new_record("BeforeOperationSummary", p_strMainXml);

                if (lngRes <= 0)
                    return lngRes;

                sbdTemp = new StringBuilder(p_strContentXml);
                intIndex = p_strContentXml.IndexOf(" ");
                sbdTemp.Insert(intIndex, " OpenDate = '" + strCurrentTime + "' ModifyDate='" + strCurrentTime + "'");
                p_strContentXml = sbdTemp.ToString();
                lngRes = objHRPServ.add_new_record("BeforeOperationSummaryContent", p_strContentXml);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;


		}
		#endregion

		#region checkModifyDate
				
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_blnCheckResult"></param>
		/// <returns></returns>
		[AutoComplete] 
		public long m_lngCheckLastModifyDate(string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string strLastModifyDate ,out string strModifyUser)
		{
			

			strLastModifyDate="";
		    strModifyUser="";
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBeforeOperationSummaryService", "m_lngCheckLastModifyDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                string strSQL = null;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSQL = @"select top 1  b.modifydate as lastmodifydate , b.modifyuserid
								from beforeoperationsummary a inner join
								beforeoperationsummarycontent b on a.inpatientid = b.inpatientid and 
								a.inpatientdate = b.inpatientdate and a.opendate = b.opendate
								where (a.status =0) and (a.inpatientid = '" + p_strInPatientID + @"') and (a.inpatientdate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + @") and 
								(a.opendate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strOpenDate) + @") order by b.modifydate desc";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strSQL = @"select lastmodifydate, modifyuserid from(select b.modifydate as lastmodifydate , b.modifyuserid
								from beforeoperationsummary a inner join
								beforeoperationsummarycontent b on a.inpatientid = b.inpatientid and 
								a.inpatientdate = b.inpatientdate and a.opendate = b.opendate
								where (a.status =0) and (a.inpatientid = '" + p_strInPatientID + @"') and (a.inpatientdate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + @") and 
								(a.opendate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strOpenDate) + @") order by b.modifydate desc)where rownum = 1";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strSQL = @" select b.modifydate as lastmodifydate , b.modifyuserid
								from beforeoperationsummary a inner join
								beforeoperationsummarycontent b on a.inpatientid = b.inpatientid and 
								a.inpatientdate = b.inpatientdate and a.opendate = b.opendate
								where (a.status =0) and (a.inpatientid = '" + p_strInPatientID + @"') and (a.inpatientdate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + @") and 
								(a.opendate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strOpenDate) + @") order by b.modifydate desc fetch first 1 row only";
                }

                DataTable dtResult = new DataTable();

                lngRes = objHRPServ.DoGetDataTable(strSQL, ref dtResult);

                if (lngRes <= 0)
                    return lngRes;
                else
                {
                    strLastModifyDate = DateTime.Parse(dtResult.Rows[0][0].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    strModifyUser = dtResult.Rows[0][1].ToString();
                    lngRes = 1;
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

            }			//返回
			return lngRes;

		}

		#endregion 

		#region Read
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strResultXml">返回的结�?/param>
		/// <param name="p_intResultRows">记录的数�?/param>
		/// <returns>
		/// 操作结果�?

		/// 0：失败�?

		/// 1：成功�?

		/// </returns>
		[AutoComplete] 
		public long m_lngGetSummaryInfo(string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out string p_strResultXml,out int p_intResultRows)
		{		

			p_strResultXml="";
			p_intResultRows=0;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBeforeOperationSummaryService", "m_lngGetSummaryInfo");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                string strSQL = null;
                #region sql
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSQL = @"select top 1 a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createid,
       a.diagnosexml,
       a.diagnosegistxml,
       a.bodyinfoxml,
       a.specialhandlexml,
       a.preparationxml,
       a.patientnotionxml,
       a.anaesthesiaxml,
       a.afternoticexml,
       a.discussnotionxml,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxmlstring,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.status,
       b.modifydate,
       b.modifyuserid,
       b.operatedoctorid,
       b.chargedoctorid,
       b.diagnose,
       b.diagnosegist,
       b.bodyinfo,
       b.specialhandle,
       b.preparation,
       b.patientnotion,
       b.anaesthesia,
       b.afternotice,
       b.discussnotion,
       b.operationdate,
       f_getempnamebyno_1stofall(b.operatedoctorid) operatedoctorname,
       f_getempnamebyno_1stofall(b.chargedoctorid) chargedoctorname
  from beforeoperationsummary a, beforeoperationsummarycontent b
 where a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.opendate = b.opendate
   and a.inpatientid = ?
   and a.inpatientdate = ?
   and a.createdate = ?
   and a.status = 0
 order by b.modifydate desc";

                }
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createid,
       diagnosexml,
       diagnosegistxml,
       bodyinfoxml,
       specialhandlexml,
       preparationxml,
       patientnotionxml,
       anaesthesiaxml,
       afternoticexml,
       discussnotionxml,
       ifconfirm,
       confirmreason,
       confirmreasonxmlstring,
       deactiveddate,
       deactivedoperatorid,
       firstprintdate,
       status,
       modifydate,
       modifyuserid,
       operatedoctorid,
       chargedoctorid,
       diagnose,
       diagnosegist,
       bodyinfo,
       specialhandle,
       preparation,
       patientnotion,
       anaesthesia,
       afternotice,
       discussnotion,
       operationdate,
       operatedoctorname,
       chargedoctorname
  from (select a.inpatientid,
               a.inpatientdate,
               a.opendate,
               a.createdate,
               a.createid,
               a.diagnosexml,
               a.diagnosegistxml,
               a.bodyinfoxml,
               a.specialhandlexml,
               a.preparationxml,
               a.patientnotionxml,
               a.anaesthesiaxml,
               a.afternoticexml,
               a.discussnotionxml,
               a.ifconfirm,
               a.confirmreason,
               a.confirmreasonxmlstring,
               a.deactiveddate,
               a.deactivedoperatorid,
               a.firstprintdate,
               a.status,
               b.modifydate,
               b.modifyuserid,
               b.operatedoctorid,
               b.chargedoctorid,
               b.diagnose,
               b.diagnosegist,
               b.bodyinfo,
               b.specialhandle,
               b.preparation,
               b.patientnotion,
               b.anaesthesia,
               b.afternotice,
               b.discussnotion,
               b.operationdate,
               f_getempnamebyno_1stofall(b.operatedoctorid) operatedoctorname,
               f_getempnamebyno_1stofall(b.chargedoctorid) chargedoctorname
          from beforeoperationsummary a, beforeoperationsummarycontent b
         where a.inpatientid = b.inpatientid
           and a.inpatientdate = b.inpatientdate
           and a.opendate = b.opendate
           and a.inpatientid = ?
           and a.inpatientdate = ?
           and a.createdate = ?
           and a.status = 0
         order by b.modifydate desc)
 where rownum = 1
";

                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createid,
       a.diagnosexml,
       a.diagnosegistxml,
       a.bodyinfoxml,
       a.specialhandlexml,
       a.preparationxml,
       a.patientnotionxml,
       a.anaesthesiaxml,
       a.afternoticexml,
       a.discussnotionxml,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxmlstring,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.status,
       b.modifydate,
       b.modifyuserid,
       b.operatedoctorid,
       b.chargedoctorid,
       b.diagnose,
       b.diagnosegist,
       b.bodyinfo,
       b.specialhandle,
       b.preparation,
       b.patientnotion,
       b.anaesthesia,
       b.afternotice,
       b.discussnotion,
       b.operationdate,
       f_getempnamebyno_1stofall(b.operatedoctorid) operatedoctorname,
       f_getempnamebyno_1stofall(b.chargedoctorid) chargedoctorname
  from beforeoperationsummary a, beforeoperationsummarycontent b
 where a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.opendate = b.opendate
   and a.inpatientid = ?
   and a.inpatientdate = ?
   and a.createdate = ?
   and a.status = 0
 order by b.modifydate desc fetch first 1 row only";


                }


                #endregion
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}
/// <summary>
/// 
/// </summary>
/// <param name="p_objPrincipal"></param>
/// <param name="p_strInPatientID"></param>
/// <param name="p_strInPatientDate"></param>
/// <param name="p_strCreateDate"></param>
/// <param name="p_strResultXml"></param>
/// <param name="p_intResultRows"></param>
/// <returns></returns>
[AutoComplete] 
		public long m_lngGetDeletedSummaryInfo(string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out string p_strResultXml,out int p_intResultRows)
		{		

			p_strResultXml="";
			p_intResultRows=0;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBeforeOperationSummaryService", "m_lngGetDeletedSummaryInfo");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                string strSQL = null;
                #region sql
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSQL = @"select top 1 a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createid,
       a.diagnosexml,
       a.diagnosegistxml,
       a.bodyinfoxml,
       a.specialhandlexml,
       a.preparationxml,
       a.patientnotionxml,
       a.anaesthesiaxml,
       a.afternoticexml,
       a.discussnotionxml,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxmlstring,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.status,
       b.modifydate,
       b.modifyuserid,
       b.operatedoctorid,
       b.chargedoctorid,
       b.diagnose,
       b.diagnosegist,
       b.bodyinfo,
       b.specialhandle,
       b.preparation,
       b.patientnotion,
       b.anaesthesia,
       b.afternotice,
       b.discussnotion,
       b.operationdate,
       f_getempnamebyno_1stofall(b.operatedoctorid) operatedoctorname,
       f_getempnamebyno_1stofall(b.chargedoctorid) chargedoctorname
  from beforeoperationsummary a, beforeoperationsummarycontent b
 where a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.opendate = b.opendate
   and a.inpatientid = ?
   and a.inpatientdate = ?
   and a.createdate = ?
   and a.status = 1
 order by b.modifydate desc";

                }
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createid,
       diagnosexml,
       diagnosegistxml,
       bodyinfoxml,
       specialhandlexml,
       preparationxml,
       patientnotionxml,
       anaesthesiaxml,
       afternoticexml,
       discussnotionxml,
       ifconfirm,
       confirmreason,
       confirmreasonxmlstring,
       deactiveddate,
       deactivedoperatorid,
       firstprintdate,
       status,
       modifydate,
       modifyuserid,
       operatedoctorid,
       chargedoctorid,
       diagnose,
       diagnosegist,
       bodyinfo,
       specialhandle,
       preparation,
       patientnotion,
       anaesthesia,
       afternotice,
       discussnotion,
       operationdate,
       operatedoctorname,
       chargedoctorname
  from (select a.inpatientid,
               a.inpatientdate,
               a.opendate,
               a.createdate,
               a.createid,
               a.diagnosexml,
               a.diagnosegistxml,
               a.bodyinfoxml,
               a.specialhandlexml,
               a.preparationxml,
               a.patientnotionxml,
               a.anaesthesiaxml,
               a.afternoticexml,
               a.discussnotionxml,
               a.ifconfirm,
               a.confirmreason,
               a.confirmreasonxmlstring,
               a.deactiveddate,
               a.deactivedoperatorid,
               a.firstprintdate,
               a.status,
               b.modifydate,
               b.modifyuserid,
               b.operatedoctorid,
               b.chargedoctorid,
               b.diagnose,
               b.diagnosegist,
               b.bodyinfo,
               b.specialhandle,
               b.preparation,
               b.patientnotion,
               b.anaesthesia,
               b.afternotice,
               b.discussnotion,
               b.operationdate,
               f_getempnamebyno_1stofall(b.operatedoctorid) operatedoctorname,
               f_getempnamebyno_1stofall(b.chargedoctorid) chargedoctorname
          from beforeoperationsummary a, beforeoperationsummarycontent b
         where a.inpatientid = b.inpatientid
           and a.inpatientdate = b.inpatientdate
           and a.opendate = b.opendate
           and a.inpatientid = ?
           and a.inpatientdate = ?
           and a.createdate = ?
           and a.status = 1
         order by b.modifydate desc)
 where rownum = 1";

                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.createdate,
       a.createid,
       a.diagnosexml,
       a.diagnosegistxml,
       a.bodyinfoxml,
       a.specialhandlexml,
       a.preparationxml,
       a.patientnotionxml,
       a.anaesthesiaxml,
       a.afternoticexml,
       a.discussnotionxml,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxmlstring,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.firstprintdate,
       a.status,
       b.modifydate,
       b.modifyuserid,
       b.operatedoctorid,
       b.chargedoctorid,
       b.diagnose,
       b.diagnosegist,
       b.bodyinfo,
       b.specialhandle,
       b.preparation,
       b.patientnotion,
       b.anaesthesia,
       b.afternotice,
       b.discussnotion,
       b.operationdate,
       f_getempnamebyno_1stofall(b.operatedoctorid) operatedoctorname,
       f_getempnamebyno_1stofall(b.chargedoctorid) chargedoctorname
  from beforeoperationsummary a, beforeoperationsummarycontent b
 where a.inpatientid = b.inpatientid
   and a.inpatientdate = b.inpatientdate
   and a.opendate = b.opendate
   and a.inpatientid = ?
   and a.inpatientdate = ?
   and a.createdate = ?
   and a.status = 1
 order by b.modifydate desc fetch first 1 row only";


                }

                #endregion
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            }			//返回
			return lngRes;

		}
		#endregion
        
		#region Modify
[AutoComplete] 
		public long m_lngModify(string p_strMainXml,string p_strContentXml)
		{

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBeforeOperationSummaryService", "m_lngModify");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                 lngRes = objHRPServ.modify_record("BeforeOperationSummary", p_strMainXml, "INPATIENTID", "INPATIENTDATE", "OPENDATE");

                if (lngRes <= 0)
                    return lngRes;

                StringBuilder sbdTemp = null;
                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                sbdTemp = new StringBuilder(p_strContentXml);
                int intIndex = p_strContentXml.IndexOf(" ");
                sbdTemp.Insert(intIndex, " ModifyDate =  '" + strCurrentTime + "' ");
                p_strContentXml = sbdTemp.ToString();
                lngRes = objHRPServ.add_new_record("BeforeOperationSummaryContent", p_strContentXml);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
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

		#region Delete
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strMainXml"></param>
		/// <returns>
		/// 操作结果�?

		/// 0：失败�?

		/// 1：成功�?
		/// </returns>
		[AutoComplete] 
		public long m_lngDelete(string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate ,string p_strDeActiveID)
		{
			long lngRes = 0;
            clsPublicMiddleTier objHRPServ = new clsPublicMiddleTier();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBeforeOperationSummaryService", "m_lngDelete");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                lngRes = objHRPServ.m_lngDeleteRecord( "BeforeOperationSummary", p_strInPatientID, p_strInPatientDate, p_strOpenDate, p_strDeActiveID);

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