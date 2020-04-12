using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.TISSValuationServ
{
	/// <summary>
	/// TISS-28评分的中间层
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsTISSValuationServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsTISSValuationServ()
		{
			//
			// TODO: Add constructor logic here
			//
		}
				
		/// <summary>
		/// 添加记录
		/// </summary>
		/// <param name="p_strXMLRecord">XML串(由表TISSValuation和界面信息组成)</param>
		/// <returns></returns>
		[AutoComplete]
		public long lngAddRecord(
			string p_strXMLRecord)
		{
			long lngResult = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsTISSValuationServ", "lngAddRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_strXMLRecord == null || p_strXMLRecord.Equals(""))
                {
                    return -1;
                }
                lngResult = clsTablService.add_new_record("TISSValuation", p_strXMLRecord);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                clsTablService.Dispose();
            }
			
			return lngResult;
			
		}

		/// <summary>
		/// 修改记录
		/// </summary>
		/// <param name="p_strMainRecord">XML串(由表TISSValuation和界面信息组成)</param>
		/// <returns></returns>
		[AutoComplete]
		public long modify_record(
			string p_strMainRecord)
		{
			long lngResult = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();

            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsTISSValuationServ", "modify_record");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_strMainRecord == null || p_strMainRecord.Equals(""))
                    return -1;

                lngResult = clsTablService.modify_record("TISSValuation", p_strMainRecord, "INPATIENTNO", "ACTIVITYTIME");
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                clsTablService.Dispose();
            }
			return lngResult;			
		}
				
		/// <summary>
		/// 删除记录
		/// </summary>
		/// <param name="p_strInHospitalNO">住院号</param>
		/// <param name="p_strActivityTime">评分时间</param>
		/// <param name="p_strOperatorID">删除者</param>
		/// <returns></returns>
		[AutoComplete]
		public long deactive_record(
			string p_strInHospitalNO,string p_strActivityTime,string p_strOperatorID)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsTISSValuationServ", "deactive_record");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_strInHospitalNO.Trim().Length == 0 || p_strActivityTime.Length == 0 || p_strActivityTime == null)
                    return -1;

                string strCommand = "update TISSValuation set status=1,DEACTIVEDOPERATORID='" + p_strOperatorID + "' ,DEACTIVATEDDATE={fn CURDATE()} where INPATIENTNO='" + p_strInHospitalNO + "'and ActivityTime=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strActivityTime);

                lngRes = clsTablService.DoExcute(strCommand);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                clsTablService.Dispose();
            }
			return lngRes;
		}

		/// <summary>
		/// 由住院号查询表TISSValuation,得到评分时间
		/// </summary>
		/// <param name="p_strInHospitalNO">住院号</param>
		/// <param name="p_strXMLTable">XML串</param>
		/// <param name="p_intRows">影响行数</param>
		/// <returns></returns>
		[AutoComplete]
		public long lngGetActivityTimeByInHospitalNO(string p_strInHospitalNO,ref string p_strXMLTable,ref int p_intRows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsTISSValuationServ", "lngGetActivityTimeByInHospitalNO");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "SELECT ActivityTime FROM TISSValuation where Status=0and INPATIENTNO='" + p_strInHospitalNO + "'";
                lngRes = clsTablService.lngGetXMLTable(strCommand, ref p_strXMLTable, ref p_intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                clsTablService.Dispose();
            }
			return lngRes;
		}

		/// <summary>
		/// 由住院号和评分时间得到记录信息
		/// </summary>
		/// <param name="p_strInHospitalNO">住院号</param>
		/// <param name="p_strActivityTime">评分时间</param>
		/// <param name="p_strXMLTable">XML串</param>
		/// <param name="p_intRows">影响行数</param>
		/// <returns>long型的值</returns>
		[AutoComplete]
		public long lngGetInfo(
			string p_strInHospitalNO,string p_strActivityTime,ref string p_strXMLTable,ref int p_intRows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsTISSValuationServ","lngGetInfo");
                //if(lngCheckRes <= 0)
					//return lngCheckRes;

				string strCommand = "SELECT * FROM TISSValuation where Status=0and INPATIENTNO='"+p_strInHospitalNO+"'and ActivityTime="+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strActivityTime);
                lngRes = clsTablService.lngGetXMLTable(strCommand, ref p_strXMLTable, ref p_intRows);
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
            finally
            {
                clsTablService.Dispose();
            }
			return lngRes;
		}

		#region New
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strFromDate"></param>
		/// <param name="p_strToDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetTimeInfoOfAPatient(
			string p_strInPatientID,string p_strInPatientDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsTISSValuationServ", "m_lngGetTimeInfoOfAPatient");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                //			strCommand="SELECT distinct ActivityTime FROM TISSValuation WHERE InPatientNO ='" +
                //				p_strInPatientID +"'" +" AND Status =0 and InPatientDate="+clsHRPTableService.s_strOracleDateTime(p_strInPatientDate)+" "  ;
                strCommand = "SELECT distinct ActivityTime FROM TISSValuation WHERE InPatientNO ='" +
                    p_strInPatientID + "'" + " AND Status =0 and InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + " ";

                lngRes = clsTablService.lngGetXMLTable(strCommand, ref p_strXml, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                clsTablService.Dispose();
            }
			return lngRes;
		}

		[AutoComplete]
		public long m_lngAddNew(
			string p_strMainXml)
		{	
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsTISSValuationServ", "m_lngAddNew");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (p_strMainXml == null || p_strMainXml == "")
                    return -1;

                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                int intIndex = p_strMainXml.IndexOf(' ');

                p_strMainXml = p_strMainXml.Insert(intIndex, " ModifyDate=\"" + strCurrentTime + "\"");

                lngRes = clsTablService.add_new_record("TISSValuation", p_strMainXml);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                clsTablService.Dispose();
            }
			return lngRes;
		}

		[AutoComplete]
		public long m_lngGetTISSValuation(string p_strPatientID,string p_strInPatientDate,string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;
            clsHRPTableService clsTablService = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsTISSValuationServ", "m_lngGetTISSValuation");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strCommand = "select top 1 *  from TISSValuation a  " +
                        "  WHERE a.InPatientNO='" +
                        p_strPatientID + "'" + " AND a.Status =0 and a.ActivityTime=" +
                        clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCreateDate) + " and a.InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + " order by ModifyDate desc";
                else
                    strCommand = "select * from(select *  from TISSValuation a  " +
                        "  WHERE a.InPatientNO='" +
                        p_strPatientID + "'" + " AND a.Status =0 and a.ActivityTime=" +
                        clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strCreateDate) + " and a.InPatientDate=" + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + " order by ModifyDate desc)where rownum = 1";

                lngRes = clsTablService.lngGetXMLTable(strCommand, ref p_strXml, ref intRows);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                clsTablService.Dispose();
            }
			return lngRes;
		}
		#endregion
	}
}
