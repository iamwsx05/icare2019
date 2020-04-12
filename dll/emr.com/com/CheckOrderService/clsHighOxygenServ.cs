using System.EnterpriseServices;
using System;
using System.Text ;
using System.Xml;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility.SQLConvert;
using System.Data;

namespace com.digitalwave.HighOxygenServ
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsHighOxygenServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		 
		/// <summary>
		/// 所有的HighOxygen申请的时间
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
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsHighOxygenServ","m_lngGetTimeInfoOfAPatient");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;	
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
									
			}
			
			

			string strCommand="";
			long lngRes = -1;

            strCommand = @"select base.createdate as createdate
  from hightoxygencheckorder hightoxygen,
       (select max(modifydate) as modifydate,
               createdate,
               inpatientid,
               inpatientdate,
               status
          from hightoxygencheckorder
         group by createdate, inpatientid, inpatientdate, status) base
 where hightoxygen.inpatientid = base.inpatientid
   and base.status = 0
   and hightoxygen.inpatientdate = base.inpatientdate
   and hightoxygen.createdate = base.createdate
   and hightoxygen.modifydate = base.modifydate
   and hightoxygen.inpatientid = ?
   and hightoxygen.inpatientdate = ?";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
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

		[AutoComplete]
		public long m_lngAddNewRecord(string strXML)
		{
			long lngRes = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsHighOxygenServ", "m_lngAddNewRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (strXML == "" || strXML == null)
                    return -1;

                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                StringBuilder sbdTempl = null;

                sbdTempl = new StringBuilder(strXML);
                int intIndex = strXML.IndexOf(" ");
                sbdTempl.Insert(intIndex, " ModifyDate =  '" + strCurrentTime + "' ");
                strXML = sbdTempl.ToString();

                lngRes = objHRPServ.add_new_record("HightOxygenCheckOrder", strXML);
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

		[AutoComplete]
		public long lngSelectNewRecord(
			string strInPatientID, string strInPatientDate,string strCreateDate,ref string strReceivedXML,ref int intReturnRows)
		{
			long lngRes = -1;
			string strCommand = "";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsHighOxygenServ", "lngSelectNewRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (clsHRPTableService.bytDatabase_Selector == 0)
                    strCommand = @"select top 1 inpatientid,
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
       resume,
       cliniccheck,
       assistantct,
       assistantmr,
       assistanteeg,
       assistantekg,
       assistantother,
       cliniccure,
       highoxygen,
       clinicdiagnose,
       highoxygentime,
       applydocid,
       docid,
       orderid
  from hightoxygencheckorder
 where inpatientid = ?
   and inpatientdate = ?
   and createdate = ?
 order by modifydate desc";
                else
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
       resume,
       cliniccheck,
       assistantct,
       assistantmr,
       assistanteeg,
       assistantekg,
       assistantother,
       cliniccure,
       highoxygen,
       clinicdiagnose,
       highoxygentime,
       applydocid,
       docid,
       orderid
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
               resume,
               cliniccheck,
               assistantct,
               assistantmr,
               assistanteeg,
               assistantekg,
               assistantother,
               cliniccure,
               highoxygen,
               clinicdiagnose,
               highoxygentime,
               applydocid,
               docid,
               orderid
          from HightOxygenCheckOrder
         Where InPatientID = ?
           and InPatientDate = ?
           and CreateDate = ?
         order by ModifyDate desc)
 where rownum = 1";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(strCreateDate);

                lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref strReceivedXML, ref intReturnRows, objDPArr);
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
