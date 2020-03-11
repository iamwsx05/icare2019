using System.EnterpriseServices;
using System;
using System.Text ;
using System.Xml;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility.SQLConvert;
using System.Data;

namespace com.digitalwave.SPECTCheckOrderServ
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsSPECTCheckOrderServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		 

		[AutoComplete]
		public long m_lngAddNewRecord(string strXML)
		{
			long lngRes = -1;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsSPECTCheckOrderServ", "m_lngAddNewRecord");
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

                lngRes = objHRPServ.add_new_record("SPECTCheckOrder", strXML);
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsSPECTCheckOrderServ", "lngSelectNewRecord");
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
       paymentpulbic,
       paymentcompany,
       paymentself,
       checkno,
       hypothyroiddisply,
       hypothyroidknubdisply,
       kidneydisply,
       hypothyroidcancer,
       hypothyroidside,
       pneumonicdaerate,
       pneumonicdblood,
       pneumonicdknub,
       heart,
       heartblood,
       dbody,
       body,
       bone,
       bonetr,
       courage,
       couragefaultage,
       couragepool,
       pulse,
       meikl,
       esophagus,
       enteron,
       overbody,
       spleen,
       lymph,
       depcancer,
       overcancer,
       metabolize,
       brainmetabolize,
       kidneydin,
       kidneystr,
       kidneyball,
       kidneyblood,
       bladder,
       bloodpool,
       ovum,
       breastcancer,
       braincancer,
       brainblood,
       tear,
       nose,
       tell,
       tellcontent,
       history,
       checklab,
       disgonse
  from spectcheckorder
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
       paymentpulbic,
       paymentcompany,
       paymentself,
       checkno,
       hypothyroiddisply,
       hypothyroidknubdisply,
       kidneydisply,
       hypothyroidcancer,
       hypothyroidside,
       pneumonicdaerate,
       pneumonicdblood,
       pneumonicdknub,
       heart,
       heartblood,
       dbody,
       body,
       bone,
       bonetr,
       courage,
       couragefaultage,
       couragepool,
       pulse,
       meikl,
       esophagus,
       enteron,
       overbody,
       spleen,
       lymph,
       depcancer,
       overcancer,
       metabolize,
       brainmetabolize,
       kidneydin,
       kidneystr,
       kidneyball,
       kidneyblood,
       bladder,
       bloodpool,
       ovum,
       breastcancer,
       braincancer,
       brainblood,
       tear,
       nose,
       tell,
       tellcontent,
       history,
       checklab,
       disgonse
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
               paymentpulbic,
               paymentcompany,
               paymentself,
               checkno,
               hypothyroiddisply,
               hypothyroidknubdisply,
               kidneydisply,
               hypothyroidcancer,
               hypothyroidside,
               pneumonicdaerate,
               pneumonicdblood,
               pneumonicdknub,
               heart,
               heartblood,
               dbody,
               body,
               bone,
               bonetr,
               courage,
               couragefaultage,
               couragepool,
               pulse,
               meikl,
               esophagus,
               enteron,
               overbody,
               spleen,
               lymph,
               depcancer,
               overcancer,
               metabolize,
               brainmetabolize,
               kidneydin,
               kidneystr,
               kidneyball,
               kidneyblood,
               bladder,
               bloodpool,
               ovum,
               breastcancer,
               braincancer,
               brainblood,
               tear,
               nose,
               tell,
               tellcontent,
               history,
               checklab,
               disgonse
          from spectcheckorder
         where inpatientid = ?
           and inpatientdate = ?
           and createdate = ?
         order by modifydate desc)
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
		/// <summary>
		/// 所有的SPECT申请的时间
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsSPECTCheckOrderServ", "m_lngGetTimeInfoOfAPatient");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                strCommand = @"select base.createdate as createdate
  from spectcheckorder spect,
       (select max(modifydate) as modifydate,
               createdate,
               inpatientid,
               inpatientdate,
               status
          from spectcheckorder
         group by createdate, inpatientid, inpatientdate, status) base
 where spect.inpatientid = base.inpatientid
   and base.status = 0
   and spect.inpatientdate = base.inpatientdate
   and spect.createdate = base.createdate
   and spect.modifydate = base.modifydate
   and spect.inpatientid = ?
   and spect.inpatientdate = ?";
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
		public long m_lngDeActive(
			string p_strDeactiveXml,string p_strTableName)
		{
			long lngRes = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsSPECTCheckOrderServ", "m_lngDeActive");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                int intIndex = p_strDeactiveXml.IndexOf(' ');
                p_strDeactiveXml = p_strDeactiveXml.Insert(intIndex, " DeActivedDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

                lngRes = objHRPServ.modify_record(p_strTableName, p_strDeactiveXml, "INPATIENTID", "INPATIENTDATE", "CREATEDATE");
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
