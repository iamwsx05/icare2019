using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.iCare.middletier.HRPService;
namespace com.digitalwave.QCRecordService
{
	/// <summary>
	/// ²¡°¸ÖÊÁ¿ÆÀ·Ö(¾É°æ)
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsQCRecordService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strResultXml"></param>
		/// <param name="p_intResultRows"></param>
		/// <returns>
		/// </returns>
		[AutoComplete]
		public long m_lngGetQCRecord(
			string p_strInPatientID,string p_strInPatientDate,ref string p_strResultXml,ref int p_intResultRows)
		{

			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsQCRecordService", "m_lngGetQCRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                string strSQL = null;

                strSQL = @"select f_getempnamebyno_1stofall(content.writedoctorid) writedoctorname,
       f_getempnamebyno_1stofall(content.filecheckerid) filecheckername,
       f_getempnamebyno_1stofall(content.checkdoctorid) checkdoctorname,
       main.createid,
       main.ifconfirm,
       main.confirmreason,
       main.confirmreasonxmlstring,
       content.inpatientid,
       content.inpatientdate,
       content.opendate,
       content.modifydate,
       content.modifyuserid,
       content.writedoctorid,
       content.filecheckerid,
       content.checkdoctorid,
       content.firstpagetidyvalue,
       content.firstpagetidyreason,
       content.litigantvalue,
       content.litigantreason,
       content.casehistoryvalue,
       content.casehistoryreason,
       content.checkvalue,
       content.checkreason,
       content.diagnosevalue,
       content.diagnosereason,
       content.curevalue,
       content.curereason,
       content.stateillnessvalue,
       content.stateillnessreason,
       content.otherrecordvalue,
       content.otherrecordreason,
       content.doctoradvicevalue,
       content.doctoradvicereason,
       content.nursevalue,
       content.nursereason,
       content.totalvalue,
       content.recorderid
  from qcrecord main, qcrecordcontent content
 where main.inpatientid = content.inpatientid
   and main.inpatientdate = content.inpatientdate
   and main.opendate = content.opendate
   and main.inpatientid = ?
   and main.inpatientdate = ?
   and main.status = 0
 order by modifydate desc";

                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

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

            }
			//è¿”å›ž
			return lngRes;
		}
        [AutoComplete]
		public long m_lngGetDeleteQCRecord(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,ref string p_strResultXml,ref int p_intResultRows)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsQCRecordService", "m_lngGetDeleteQCRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                string strSQL = null;
                strSQL = @"select f_getempnamebyno_1stofall(content.writedoctorid) writedoctorname,
       f_getempnamebyno_1stofall(content.filecheckerid) filecheckername,
       f_getempnamebyno_1stofall(content.checkdoctorid) checkdoctorname,
       main.createid,
       main.ifconfirm,
       main.confirmreason,
       main.confirmreasonxmlstring,
       content.inpatientid,
       content.inpatientdate,
       content.opendate,
       content.modifydate,
       content.modifyuserid,
       content.writedoctorid,
       content.filecheckerid,
       content.checkdoctorid,
       content.firstpagetidyvalue,
       content.firstpagetidyreason,
       content.litigantvalue,
       content.litigantreason,
       content.casehistoryvalue,
       content.casehistoryreason,
       content.checkvalue,
       content.checkreason,
       content.diagnosevalue,
       content.diagnosereason,
       content.curevalue,
       content.curereason,
       content.stateillnessvalue,
       content.stateillnessreason,
       content.otherrecordvalue,
       content.otherrecordreason,
       content.doctoradvicevalue,
       content.doctoradvicereason,
       content.nursevalue,
       content.nursereason,
       content.totalvalue,
       content.recorderid
  from qcrecord main, qcrecordcontent content
 where main.inpatientid = content.inpatientid
   and main.inpatientdate = content.inpatientdate
   and main.opendate = content.opendate
   and main.inpatientid = ?
   and main.inpatientdate = ?
   and main.status = 1
 order by modifydate desc";

                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

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

            }
			//è¿”å›ž
			return lngRes;

		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strMainXml"></param>
		/// <returns>
		/// </returns>
		[AutoComplete]
		public long m_lngAddNew(
			string p_strMainXml,string p_strContentXml)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsQCRecordService", "m_lngAddNew");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                 lngRes = objHRPServ.add_new_record("QCRecord", p_strMainXml);

                if (lngRes <= 0)
                    return lngRes;

                lngRes = objHRPServ.add_new_record("QCRecordContent", p_strContentXml);

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
			//è¿”å›ž
			return lngRes;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strOpenDate"></param>
		/// <param name="p_blnCheckResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckNewOpenDate(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out bool p_blnIsAddNew)
		{
			p_blnIsAddNew = false;
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsQCRecordService", "m_lngCheckNewOpenDate");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

//                string strSQL = @"select * 
//									from QCRecord
//									Where InPatientID='" + p_strInPatientID + @"'
//									and InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + @"
//									--and OpenDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strOpenDate) + @"
//									and Status='0'";

                string strSQL = @"select inpatientid,
       inpatientdate,
       opendate,
       createid,
       ifconfirm,
       confirmreason,
       confirmreasonxmlstring,
       status,
       deactiveddate,
       deactivedoperatorid
  from qcrecord
 where inpatientid = ?
   and inpatientdate = ?
   and status = '0'";

                DataTable dtResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                p_blnIsAddNew = false;

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

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			//è¿”å›ž
			return lngRes;



		}

		/// <summary>
		/// 
		/// </summary>		
		[AutoComplete]
		private long m_lngDeleteInMainTable(string p_strTableName,string p_strDeActivedOperatorID,string p_strInPatientID,string p_strInPatientDate)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                string strSQL = null;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSQL = "update " + p_strTableName + " set status=1 , deactivedoperatorid=? , deactiveddate=? "  +
                       " where inpatientid=? and inpatientdate=? and status=0";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strSQL = "update " + p_strTableName + " set status=1 , deactivedoperatorid=? , deactiveddate=? " +
                       " where inpatientid=? and inpatientdate=? and status=0";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strSQL = "update " + p_strTableName + " set status=1 , deactivedoperatorid=? , deactiveddate=? " +
                       " where inpatientid=? and inpatientdate=? and status=0";
                }

                DataTable dtResult = new DataTable();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = p_strDeActivedOperatorID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                objDPArr[2].Value = p_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);

                long lngEff = -1;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSQL,ref lngEff,objDPArr);

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
			//è¿”å›ž
			return lngRes;

		}
        [AutoComplete]
		public long m_lngDelete(
			string p_strDeActivedOperatorID,string p_strInPatientID,string p_strInPatientDate)
		{
			long lngRes = 0;
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsQCRecordService","m_lngDelete");
                //if(lngCheckRes <= 0)
					//return lngCheckRes;

				lngRes= m_lngDeleteInMainTable("QCRecord",p_strDeActivedOperatorID,p_strInPatientID,p_strInPatientDate);
				
			}
			catch(Exception objEx)
			{
				
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}
			//è¿”å›ž
			return lngRes;

		}

        [AutoComplete]
		public long m_lngGetIDandTimeOfDeletedRecord(
			string p_strInPatientID,string p_strInPatientDate,ref string p_strResultXml,ref int p_intResultRows)
		{
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsQCRecordService", "m_lngGetIDandTimeOfDeletedRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                string strSQL = null;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strSQL = @"select top 1 main.deactiveddate,main.deactivedoperatorid 
									from qcrecord main,qcrecordcontent content
									where main.inpatientid = content.inpatientid
									and main.inpatientdate = content.inpatientdate
									and main.opendate = content.opendate
									and main.inpatientid = ?
									and main.inpatientdate = ?
									and main.status = 1
									order by content.modifydate desc";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 2)
                {
                    strSQL = @"select deactiveddate, deactivedoperatorid
  from (select main.deactiveddate, main.deactivedoperatorid
          from qcrecord main, qcrecordcontent content
         where main.inpatientid = content.inpatientid
           and main.inpatientdate = content.inpatientdate
           and main.opendate = content.opendate
           and main.inpatientid = ?
           and main.inpatientdate = ?
           and main.status = 1
         order by content.modifydate desc)
 where rownum = 1";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strSQL = @" select main.deactiveddate,main.deactivedoperatorid 
									from qcrecord main,qcrecordcontent content
									where main.inpatientid = content.inpatientid
									and main.inpatientdate = content.inpatientdate
									and main.opendate = content.opendate
									and main.inpatientid = ?
									and main.inpatientdate = ?
									and main.status = 1
									order by content.modifydate desc fetch first 1 row only";
                }

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

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

            }
			//è¿”å›ž
			return lngRes;

		}

	}
}