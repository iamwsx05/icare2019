using System;
using System.Text ;
using System.EnterpriseServices;
using com.digitalwave.PACSService;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.BUltrasonicCheckOrderServ
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsBUltrasonicCheckOrderServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
        

		#region Save
		/// <summary>
		/// 添加信息
		/// </summary>		
		[AutoComplete]
		public long m_lngAddNew(string p_strMainXml,
							    ImageRequest p_objApplicationInfo,ref string p_strApplicationID,bool p_bnlIsNew)
		{
			string m_strApplicationID="";
			long m_lngRe;
			long lngRes = -1;
			clsPACSService m_PACSSer=new clsPACSService();

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                if (p_bnlIsNew)
                {
                    ///在PS_ImageApplication表中插入一条新记录

                    m_lngRe = m_PACSSer.m_lngAddNewPACSApplication(p_objApplicationInfo, out m_strApplicationID);

                    p_strApplicationID = m_strApplicationID;

                }
                else
                {
                    m_strApplicationID = p_strApplicationID;

                    m_lngRe = m_PACSSer.m_lngUpdatePACSApplication(p_objApplicationInfo, m_strApplicationID);

                }

                ///添加一条新记录
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBUltrasonicCheckOrderServ", "m_lngAddNew");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //添加记录时，主从表同时添加一条记录			
                if (p_strMainXml == "")
                    return -1;

                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                int intIndex = p_strMainXml.IndexOf(' ');

                p_strMainXml = p_strMainXml.Insert(intIndex, " ModifyDate='" + strCurrentTime + "'  ApplicationID='" + m_strApplicationID + "'");

                lngRes = objHRPServ.add_new_record("BUltrasonicCheckOrder", p_strMainXml);


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
			return lngRes;

			
		}
		#endregion

		#region 读取初始化信息
		/// <summary>
		/// 获得所有Create Date
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetTimeInfoOfAPatient(string p_strInPatientID,string p_strInPatientDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBUltrasonicCheckOrderServ", "m_lngGetTimeInfoOfAPatient");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                strCommand = "select distinct createdate from bultrasoniccheckorder where inpatientid=? and status = 0 and inpatientdate=?";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr);
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
			return lngRes;

		}
		#endregion

		/// <summary>
		/// 获得主表的记录
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long GetBUltrasonicCheckOrder(string p_strPatientID,string p_strInPatientDate,
			string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			string strCommand="";
			long lngRes = -1;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsBUltrasonicCheckOrderServ", "GetBUltrasonicCheckOrder");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;
                if (clsHRPTableService.bytDatabase_Selector == 0)
                {
                    strCommand = @"select top 1 a.inpatientid,
       a.inpatientdate,
       a.createdate,
       a.modifydate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.checknumber,
       a.history,
       a.bodycheck,
       a.xray,
       a.xraydate,
       a.xraynumber,
       a.labcheck,
       a.othercheck,
       a.clinicaldisgonse,
       a.checkplace,
       a.patholydisgonsedate,
       a.operationdate,
       a.operationinformation,
       a.createuserdeptid,
       a.applicationid
  from bultrasoniccheckorder a
 where a.inpatientid = ?
   and a.status = 0
   and a.createdate = ?
   and a.inpatientdate = ?
 order by modifydate desc";
                }
                else if (clsHRPTableService.bytDatabase_Selector ==2)
                {
                    strCommand = @"select inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       createuserid,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       firstprintdate,
       deactiveddate,
       deactivedoperatorid,
       status,
       checknumber,
       history,
       bodycheck,
       xray,
       xraydate,
       xraynumber,
       labcheck,
       othercheck,
       clinicaldisgonse,
       checkplace,
       patholydisgonsedate,
       operationdate,
       operationinformation,
       createuserdeptid,
       applicationid
  from (select inpatientid,
               inpatientdate,
               createdate,
               modifydate,
               createuserid,
               ifconfirm,
               confirmreason,
               confirmreasonxml,
               firstprintdate,
               deactiveddate,
               deactivedoperatorid,
               status,
               checknumber,
               history,
               bodycheck,
               xray,
               xraydate,
               xraynumber,
               labcheck,
               othercheck,
               clinicaldisgonse,
               checkplace,
               patholydisgonsedate,
               operationdate,
               operationinformation,
               createuserdeptid,
               applicationid
          from bultrasoniccheckorder a
         where a.inpatientid = ?
           and a.status = 0
           and a.createdate = ?
           and a.inpatientdate = ?
         order by modifydate desc)
 where rownum = 1";
                }
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                {
                    strCommand = @" select a.inpatientid,
       a.inpatientdate,
       a.createdate,
       a.modifydate,
       a.createuserid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.firstprintdate,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.status,
       a.checknumber,
       a.history,
       a.bodycheck,
       a.xray,
       a.xraydate,
       a.xraynumber,
       a.labcheck,
       a.othercheck,
       a.clinicaldisgonse,
       a.checkplace,
       a.patholydisgonsedate,
       a.operationdate,
       a.operationinformation,
       a.createuserdeptid,
       a.applicationid
  from bultrasoniccheckorder a
 where a.inpatientid = ?
   and a.status = 0
   and a.createdate = ?
   and a.inpatientdate = ?
 order by modifydate desc fetch first 1 row only";
                }
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreateDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXml, ref intRows, objDPArr);
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
			
			return lngRes;
		}
	}
}
