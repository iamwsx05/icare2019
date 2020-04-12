using System;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.MRIApplyServ
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsMRIApplyServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
 		 

		[AutoComplete]
		public long m_lngGetAllCreateDate(
			string p_strInPatientID,string p_strInPatientDate,out string p_strResultXml,out int p_intResultRows)
		{
             //dick 2003-3-27
			p_strResultXml="";
			p_intResultRows=0;
			string strCommand="";

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsMRIApplyServ","m_lngGetAllCreateDate");
			////if (lngCheckRes <= 0)
				//return lngCheckRes;

            strCommand = @"select base.createdate as createdate
  from mriapply spect,
       (select max(modifydate) as modifydate,
               createdate,
               inpatientid,
               inpatientdate,
               status
          from mriapply
         group by createdate, inpatientid, inpatientdate, status) base
 where spect.inpatientid = base.inpatientid
   and base.status = 0
   and spect.inpatientdate = base.inpatientdate
   and spect.createdate = base.createdate
   and spect.modifydate = base.modifydate
   and spect.inpatientid = ?
   and spect.inpatientdate = ?";


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate) ;
                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strResultXml, ref p_intResultRows, objDPArr);

            }
            finally
            {
                //objHRPServ.Dispose();

            }
            return lngRes;
        }
		
		[AutoComplete]
		public long m_lngGetMRIApply_All(
			string p_strInPatientID,string p_strInPatientDate,string p_strCreateDate,out string p_strResultXml,out int p_intResultRows,out string p_strSubXML,out int p_intSubRows,out string p_strSub2XML,out int p_intSub2Rows)
		{
			p_strResultXml=p_strSubXML=p_strSub2XML="";
			p_intResultRows=p_intSubRows=p_intSub2Rows=0;

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsMRIApplyServ","m_lngGetMRIApply_All");
			////if (lngCheckRes <= 0)
				//return lngCheckRes;
			
			string strSQL = "";
			if(clsHRPTableService.bytDatabase_Selector == 0)
                strSQL = @"select top 1 a.inpatientid,
       a.inpatientdate,
       a.createdate,
       a.modifydate,
       a.createuserid,
       a.applydeptid,
       a.checkprice,
       a.mr_id,
       a.sickhistandbodycharacter,
       a.othercheckresultandregisterid,
       a.clinicdiagnose,
       a.checkpart,
       a.hasoperationhistory,
       a.hasmetalinbodyandpart,
       a.makeshadowqty,
       a.patientreactioninscan,
       a.scantime,
       a.techniciansignid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.weitht,
       (select top 1 lastname_vchr as firstname
          from t_bse_employee
         where empno_chr = a.createuserid
           and status_int <> -1
         order by isemployee_int desc, empid_chr desc) as createusername,
       (select top 1 lastname_vchr as firstname
          from t_bse_employee
         where empno_chr = a.techniciansignid
           and status_int <> -1
         order by isemployee_int desc, empid_chr desc) as techniciansignname,
       (select top 1 lastname_vchr as firstname
          from t_bse_employee
         where empno_chr = a.applydeptid
           and status_int <> -1
         order by isemployee_int desc, empid_chr desc) as applydeptname
  from mriapply a
 where inpatientid = ?
   and inpatientdate = ?
   and createdate = ?
 order by modifydate desc";
            else if (clsHRPTableService.bytDatabase_Selector == 2)
                strSQL = @"select inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       createuserid,
       applydeptid,
       checkprice,
       mr_id,
       sickhistandbodycharacter,
       othercheckresultandregisterid,
       clinicdiagnose,
       checkpart,
       hasoperationhistory,
       hasmetalinbodyandpart,
       makeshadowqty,
       patientreactioninscan,
       scantime,
       techniciansignid,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       status,
       deactiveddate,
       deactivedoperatorid,
       weitht,
       createusername,
       techniciansignname,
       applydeptname
  from (select a.inpatientid,
               a.inpatientdate,
               a.createdate,
               a.modifydate,
               a.createuserid,
               a.applydeptid,
               a.checkprice,
               a.mr_id,
               a.sickhistandbodycharacter,
               a.othercheckresultandregisterid,
               a.clinicdiagnose,
               a.checkpart,
               a.hasoperationhistory,
               a.hasmetalinbodyandpart,
               a.makeshadowqty,
               a.patientreactioninscan,
               a.scantime,
               a.techniciansignid,
               a.ifconfirm,
               a.confirmreason,
               a.confirmreasonxml,
               a.status,
               a.deactiveddate,
               a.deactivedoperatorid,
               a.weitht,
               (select lastname_vchr
                  from (select lastname_vchr,
                               empid_chr,
                               isemployee_int,
                               empno_chr
                          from t_bse_employee
                         where empno_chr = a.createuserid
                           and status_int <> -1
                         order by isemployee_int desc, empid_chr desc)
                 where empno_chr = a.createuserid
                   and rownum = 1) as createusername,
               (select lastname_vchr
                  from (select lastname_vchr,
                               empid_chr,
                               isemployee_int,
                               empno_chr
                          from t_bse_employee
                         where empno_chr = a.techniciansignid
                           and status_int <> -1
                         order by isemployee_int desc, empid_chr desc)
                 where empno_chr = a.techniciansignid
                   and rownum = 1) as techniciansignname,
               (select lastname_vchr
                  from (select lastname_vchr,
                               empid_chr,
                               isemployee_int,
                               empno_chr
                          from t_bse_employee
                         where empno_chr = a.applydeptid
                           and status_int <> -1
                         order by isemployee_int desc, empid_chr desc)
                 where empno_chr = a.applydeptid
                   and rownum = 1) as applydeptname
          from mriapply a
         where inpatientid = ?
           and inpatientdate = ?
           and createdate = ?
         order by modifydate desc)
 where rownum = 1";
            else if (clsHRPTableService.bytDatabase_Selector == 4)
                strSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.createdate,
       a.modifydate,
       a.createuserid,
       a.applydeptid,
       a.checkprice,
       a.mr_id,
       a.sickhistandbodycharacter,
       a.othercheckresultandregisterid,
       a.clinicdiagnose,
       a.checkpart,
       a.hasoperationhistory,
       a.hasmetalinbodyandpart,
       a.makeshadowqty,
       a.patientreactioninscan,
       a.scantime,
       a.techniciansignid,
       a.ifconfirm,
       a.confirmreason,
       a.confirmreasonxml,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.weitht,
       (select lastname_vchr
          from t_bse_employee
         where empno_chr = a.createuserid
           and status_int <> -1
         order by isemployee_int desc, empid_chr desc fetch first 1 row only) createusername,
       
       (select lastname_vchr
          from t_bse_employee
         where empno_chr = a.techniciansignid
           and status_int <> -1
         order by isemployee_int desc, empid_chr desc fetch first 1 row only) techniciansignname,
       (select lastname_vchr
          from t_bse_employee
         where empno_chr = a.applydeptid
           and status_int <> -1
         order by isemployee_int desc, empid_chr desc fetch first 1 row only) applydeptname
  from mriapply a
 where inpatientid = ?
   and inpatientdate = ?
   and createdate = ?
 order by modifydate desc fetch first 1 row only";
	    clsHRPTableService objHRPServ = new clsHRPTableService();
        long lngRes = 0;
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strResultXml, ref p_intResultRows, objDPArr);
			if(lngRes<=0)
				return lngRes;

            strSQL = @"select inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       serialno,
       partandline,
       pulseserial,
       param,
       fix,
       layernum,
       layerheight,
       layerdistance
  from MRIApply_MRRoom a
 Where InPatientID = ?
   and InPatientDate = ?
   and CreateDate = ?
   and ModifyDate = (select Max(ModifyDate)
                       from MRIApply_MRRoom
                      Where InPatientID = a.InPatientID
                        and InPatientDate = a.InPatientDate
                        and CreateDate = a.CreateDate)";


            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

            lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strSubXML, ref p_intSubRows, objDPArr);
                if (lngRes <= 0)
                    return lngRes;

                strSQL = @"select a.inpatientid,
       a.inpatientdate,
       a.createdate,
       a.modifydate,
       a.operationhistorytime
  from mriapply_operationtime a
 where inpatientid = ?
   and inpatientdate = ?
   and createdate = ?
   and modifydate = (select max(modifydate)
                       from mriapply_operationtime
                      where inpatientid = a.inpatientid
                        and inpatientdate = a.inpatientdate
                        and createdate = a.createdate)";
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strCreateDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strSQL, ref p_strSub2XML, ref p_intSub2Rows, objDPArr); 
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			
			
			return lngRes;
		}

		[AutoComplete]
		public long m_lngAddNew(
			string p_strMainXml,string[] p_strSubXmlArr,string[] p_strDateXMLArr)
		{			
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsMRIApplyServ","m_lngAddNew");
			////if (lngCheckRes <= 0)
				//return lngCheckRes;


            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                 lngRes = objHRPServ.add_new_record("MRIApply", p_strMainXml);
                if (lngRes <= 0)
                    return lngRes;
                if (p_strSubXmlArr != null)
                {
                    for (int i = 0; i < p_strSubXmlArr.Length; i++)
                    {
                        lngRes = objHRPServ.add_new_record("MRIApply_MRRoom", p_strSubXmlArr[i]);
                        if (lngRes <= 0)
                            return lngRes;
                    }
                }

                if (p_strDateXMLArr != null)
                {
                    for (int i = 0; i < p_strDateXMLArr.Length; i++)
                    {
                        lngRes = objHRPServ.add_new_record("MRIApply_OperationTime", p_strDateXMLArr[i]);
                        if (lngRes <= 0)
                            return lngRes;
                    }
                } 
            }
            finally
            {
                //objHRPServ.Dispose();

            }
			
					
			return lngRes;
		}		

		[AutoComplete]
		public long m_lngModify(
			string p_strMainXml,string[] p_strSubXmlArr,string[] p_strDateXMLArr)
		{
			long lngRes = -1;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsMRIApplyServ","m_lngModify");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;

				lngRes = m_lngAddNew( p_strMainXml,p_strSubXmlArr,p_strDateXMLArr);
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			return lngRes;
		}	

	}
}
