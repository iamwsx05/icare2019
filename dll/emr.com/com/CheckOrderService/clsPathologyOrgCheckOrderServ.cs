using System;
using System.Text ;
using System.EnterpriseServices;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;

namespace com.digitalwave.PathologyOrgCheckOrderServ
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsPathologyOrgCheckOrderServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		 

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
		public long m_lngGetTimeInfoOfAPatient(
			string p_strInPatientID,string p_strInPatientDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsPathologyOrgCheckOrderServ", "m_lngGetTimeInfoOfAPatient");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                strCommand = "select distinct createdate from pathologyorgcheckorder where inpatientid=? and status =0 and inpatientdate=?";
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

		#region Save
		/// <summary>
		/// 添加信息
		/// </summary>	
		[AutoComplete]	
		public long m_lngAddNew(string p_strMainXml,string[] strOperatorXML)
		{
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPathologyOrgCheckOrderServ","m_lngAddNew");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			
			
			//添加记录时，主从表同时添加记录			
			if(p_strMainXml=="" )
				return -1;

			string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

			int intMainIndex = p_strMainXml.IndexOf(' ');

			p_strMainXml = p_strMainXml.Insert(intMainIndex, " ModifyDate = '" + strCurrentTime + "' ");

			long lngRes = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = objHRPServ.add_new_record("PathologyOrgCheckOrder", p_strMainXml);
                if (lngRes == 1)
                {
                    for (int i = 0; i < strOperatorXML.Length; i++)
                    {
                        if (strOperatorXML[i] == null || strOperatorXML[i].Trim() == "")
                            continue;
                        int intSubIndex = strOperatorXML[i].IndexOf(' ');

                        strOperatorXML[i] = strOperatorXML[i].Insert(intSubIndex, " ModifyDate = '" + strCurrentTime + "' ");

                        lngRes = objHRPServ.add_new_record("pathologyorgchkord_operator", strOperatorXML[i]);
                        if (lngRes != 1)
                            break;
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
			
			return lngRes;
		}
		#endregion

		#region 读取信息
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
		public long GetPathologyOrgCheckOrder(
			string p_strPatientID,string p_strInPatientDate,
			string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;

			
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPathologyOrgCheckOrderServ","GetPathologyOrgCheckOrder");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;

				string strCommand="";

				if(clsHRPTableService.bytDatabase_Selector == 0)
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
       a.medicalcheckno,
       a.hospitalname,
       a.lastchecknumber,
       a.sendthings,
       a.frombody,
       a.sickenperiod,
       a.history,
       a.clinicalinfo,
       a.operationinfo,
       a.checkaim,
       a.biologychemistry,
       a.blood,
       a.xray,
       a.bloodserum,
       a.other,
       a.clinicaldignose,
       a.senddate,
       a.receivedate,
       a.colorandslice,
       a.eyecheck,
       a.organiseburyfull,
       a.organisestay,
       a.eyesample,
       a.pathologydignose,
       a.reportdate,
       a.doctorname,
       a.doctorid
  from pathologyorgcheckorder a
 where a.inpatientid = ?
   and a.status = 0
   and a.createdate = ?
   and a.inpatientdate = ?
 order by modifydate desc";
				}
                else if (clsHRPTableService.bytDatabase_Selector == 2)
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
       medicalcheckno,
       hospitalname,
       lastchecknumber,
       sendthings,
       frombody,
       sickenperiod,
       history,
       clinicalinfo,
       operationinfo,
       checkaim,
       biologychemistry,
       blood,
       xray,
       bloodserum,
       other,
       clinicaldignose,
       senddate,
       receivedate,
       colorandslice,
       eyecheck,
       organiseburyfull,
       organisestay,
       eyesample,
       pathologydignose,
       reportdate,
       doctorname,
       doctorid
  from (select a.inpatientid,
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
               a.medicalcheckno,
               a.hospitalname,
               a.lastchecknumber,
               a.sendthings,
               a.frombody,
               a.sickenperiod,
               a.history,
               a.clinicalinfo,
               a.operationinfo,
               a.checkaim,
               a.biologychemistry,
               a.blood,
               a.xray,
               a.bloodserum,
               a.other,
               a.clinicaldignose,
               a.senddate,
               a.receivedate,
               a.colorandslice,
               a.eyecheck,
               a.organiseburyfull,
               a.organisestay,
               a.eyesample,
               a.pathologydignose,
               a.reportdate,
               a.doctorname,
               a.doctorid
          from pathologyorgcheckorder a
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
       a.medicalcheckno,
       a.hospitalname,
       a.lastchecknumber,
       a.sendthings,
       a.frombody,
       a.sickenperiod,
       a.history,
       a.clinicalinfo,
       a.operationinfo,
       a.checkaim,
       a.biologychemistry,
       a.blood,
       a.xray,
       a.bloodserum,
       a.other,
       a.clinicaldignose,
       a.senddate,
       a.receivedate,
       a.colorandslice,
       a.eyecheck,
       a.organiseburyfull,
       a.organisestay,
       a.eyesample,
       a.pathologydignose,
       a.reportdate,
       a.doctorname,
       a.doctorid
  from pathologyorgcheckorder a
 where a.inpatientid = ?
   and a.status = 0
   and a.createdate = ?
   and a.inpatientdate = ?
 order by modifydate desc fetch first 1 row only";
                }
                clsHRPTableService objHRPServ = new clsHRPTableService();
                try
                {
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

		/// <summary>
		/// 获得从表的记录 -- Operator
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long GetPathologyOrgOperator(
			string p_strPatientID,string p_strInPatientDate,
			string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;
			
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPathologyOrgCheckOrderServ","GetPathologyOrgOperator");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;

				string strCommand = "";

                strCommand = @"select inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       operatorid,
       operatorflag
  from pathologyorgchkord_operator
 where inpatientid = ?
   and inpatientdate = ?
   and createdate = ?
   and modifydate = (select max(modifydate)
                       from pathologyorgchkord_operator
                      where inpatientid = ?
                        and inpatientdate = ?
                        and createdate = ?)
 order by operatorflag";
                clsHRPTableService objHRPServ = new clsHRPTableService();
                try
                {
                    IDataParameter[] objDPArr = null;
                    objHRPServ.CreateDatabaseParameter(6, out objDPArr);
                    objDPArr[0].Value = p_strPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = DateTime.Parse(p_strCreateDate);
                    objDPArr[3].Value = p_strPatientID;
                    objDPArr[4].DbType = DbType.DateTime;
                    objDPArr[4].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr[5].DbType = DbType.DateTime;
                    objDPArr[5].Value = DateTime.Parse(p_strCreateDate);

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
	}
}
