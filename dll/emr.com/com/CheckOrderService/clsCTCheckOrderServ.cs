using System.EnterpriseServices;
using System;
using System.Text ;
using System.Xml;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.PACSService;
using com.digitalwave.Utility.SQLConvert;
using System.Data;

namespace com.digitalwave.CTCheckOrderServ
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsCTCheckOrderServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsCTCheckOrderServ()
		{
			//
			// TODO: Add constructor logic here
			//
		}

//		
//		public long m_lngAddNewRecord(string strXML)
//
//		{
//			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsCTCheckOrderServ","m_lngAddNewRecord");
//			////if (lngCheckRes <= 0)
//				//return lngCheckRes;	
//
//			if(strXML == "" || strXML == null)
//				return -1;
//
//			string strCurrentTime=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
//			StringBuilder sbdTempl=null;
//
//			sbdTempl=new StringBuilder(strXML); 
//			int intIndex=strXML.IndexOf(" ");
//			sbdTempl.Insert(intIndex," ModifyDate =  '"+strCurrentTime + "'");
//			strXML=sbdTempl.ToString();
//
//			return new clsHRPTableService().add_new_record("CTCheckOrder",strXML);	
//		}


		[AutoComplete]
		public long m_lngSaveRecord(string strXML,
			ImageRequest p_objApplicationInfo,ref string p_strApplicationID,bool p_bnlIsNew)

		{	
			string m_strApplicationID="";
			long lngRes = -1;
			long m_lngRe = -1;

			clsPACSService m_PACSSer=new clsPACSService();
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {

                if (p_bnlIsNew == true)
                {
                    ///在PS_ImageApplication表中插入一条新记录

                    m_lngRe = m_PACSSer.m_lngAddNewPACSApplication(p_objApplicationInfo, out m_strApplicationID);

                    p_strApplicationID = m_strApplicationID;
                }
                else
                {
                    ///在PS_ImageApplication表中更改相应的记录

                    if (p_strApplicationID != null)
                        m_strApplicationID = p_strApplicationID;

                    m_lngRe = m_PACSSer.m_lngUpdatePACSApplication(p_objApplicationInfo, m_strApplicationID);

                }
                //				m_strApplicationID=p_strApplicationID;
                ///////
                ///
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCTCheckOrderServ", "m_lngAddNewRecord");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                if (strXML == "" || strXML == null)
                    return -1;

                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                StringBuilder sbdTempl = null;

                sbdTempl = new StringBuilder(strXML);
                int intIndex = strXML.IndexOf(" ");
                sbdTempl.Insert(intIndex, " ModifyDate =  '" + strCurrentTime + "'  ApplicationID='" + m_strApplicationID + "'");
                strXML = sbdTempl.ToString();

                lngRes = objHRPServ.add_new_record("CTCheckOrder", strXML);
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
		public long lngSelectNewRecord(string strInPatientID, string strInPatientDate,string strCreateDate,ref string strReceivedXML,ref int intReturnRows)
		{
			long lngRes = -1;
			string strCommand = "";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCTCheckOrderServ", "lngSelectNewRecord");
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
       resumehave,
       resumenone,
       resumeasthmahave,
       resumeasthmanone,
       tool,
       walk,
       particular,
       clinic,
       checkpart,
       applydotorid,
       liver,
       albumen,
       fetus,
       red,
       akp,
       cancer,
       fecula,
       pee17,
       blood,
       blooddoop,
       phlegm,
       emiction,
       breast,
       pancreas,
       scan,
       bladder,
       ultrasonic,
       other,
       idea,
       advancetime,
       advanceid,
       checkmoneycontent,
       ctno,
       photomontycontent,
       applicationid
  from ctcheckorder
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
       resumehave,
       resumenone,
       resumeasthmahave,
       resumeasthmanone,
       tool,
       walk,
       particular,
       clinic,
       checkpart,
       applydotorid,
       liver,
       albumen,
       fetus,
       red,
       akp,
       cancer,
       fecula,
       pee17,
       blood,
       blooddoop,
       phlegm,
       emiction,
       breast,
       pancreas,
       scan,
       bladder,
       ultrasonic,
       other,
       idea,
       advancetime,
       advanceid,
       checkmoneycontent,
       ctno,
       photomontycontent,
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
               resumehave,
               resumenone,
               resumeasthmahave,
               resumeasthmanone,
               tool,
               walk,
               particular,
               clinic,
               checkpart,
               applydotorid,
               liver,
               albumen,
               fetus,
               red,
               akp,
               cancer,
               fecula,
               pee17,
               blood,
               blooddoop,
               phlegm,
               emiction,
               breast,
               pancreas,
               scan,
               bladder,
               ultrasonic,
               other,
               idea,
               advancetime,
               advanceid,
               checkmoneycontent,
               ctno,
               photomontycontent,
               applicationid
          from ctcheckorder
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

		[AutoComplete]
		public long lngSelectNewRecord(string strInPatientID,string strCreateDate,ref string strReceivedXML,ref int intReturnRows)
		{
			long lngRes = -1;
			string strCommand = "";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsCTCheckOrderServ", "lngSelectNewRecord");
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
       resumehave,
       resumenone,
       resumeasthmahave,
       resumeasthmanone,
       tool,
       walk,
       particular,
       clinic,
       checkpart,
       applydotorid,
       liver,
       albumen,
       fetus,
       red,
       akp,
       cancer,
       fecula,
       pee17,
       blood,
       blooddoop,
       phlegm,
       emiction,
       breast,
       pancreas,
       scan,
       bladder,
       ultrasonic,
       other,
       idea,
       advancetime,
       advanceid,
       checkmoneycontent,
       ctno,
       photomontycontent,
       applicationid
  from CTCheckOrder
 Where InPatientID = ?
   and CreateDate = ?
 order by ModifyDate desc";
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
       resumehave,
       resumenone,
       resumeasthmahave,
       resumeasthmanone,
       tool,
       walk,
       particular,
       clinic,
       checkpart,
       applydotorid,
       liver,
       albumen,
       fetus,
       red,
       akp,
       cancer,
       fecula,
       pee17,
       blood,
       blooddoop,
       phlegm,
       emiction,
       breast,
       pancreas,
       scan,
       bladder,
       ultrasonic,
       other,
       idea,
       advancetime,
       advanceid,
       checkmoneycontent,
       ctno,
       photomontycontent,
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
               resumehave,
               resumenone,
               resumeasthmahave,
               resumeasthmanone,
               tool,
               walk,
               particular,
               clinic,
               checkpart,
               applydotorid,
               liver,
               albumen,
               fetus,
               red,
               akp,
               cancer,
               fecula,
               pee17,
               blood,
               blooddoop,
               phlegm,
               emiction,
               breast,
               pancreas,
               scan,
               bladder,
               ultrasonic,
               other,
               idea,
               advancetime,
               advanceid,
               checkmoneycontent,
               ctno,
               photomontycontent,
               applicationid
          from CTCheckOrder
         Where InPatientID = ?
           and CreateDate = ?
         order by ModifyDate desc)
 where rownum = 1";
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(strCreateDate);
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
		/// 所有的CT申请的时间
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetTimeInfoOfAPatient(string p_strInPatientID,string p_strInPatientDate,ref string  p_strXml,ref int  intRows)
		{
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsCTCheckOrderServ","m_lngGetTimeInfoOfAPatient");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;	
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
				
			}
            
            clsHRPTableService objHRPServ = new clsHRPTableService();
			string strCommand="";
			long lngRes = -1;
            IDataParameter[] objDPArr = null;
			if(p_strInPatientDate==null || p_strInPatientDate.Trim()=="")
			{
                strCommand = @"select base.createdate as createdate
  from ctcheckorder ct,
       (select max(modifydate) as modifydate,
               createdate,
               inpatientid,
               inpatientdate,
               status,
               applicationid
          from ctcheckorder
         group by createdate,
                  inpatientid,
                  inpatientdate,
                  status,
                  applicationid) base
 where ct.inpatientid = base.inpatientid
   and base.status = 0
   and ct.inpatientdate = base.inpatientdate
   and ct.createdate = base.createdate
   and ct.modifydate = base.modifydate
   and ct.inpatientid = '" + p_strInPatientID + "'";
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
			}
			else
			{
                strCommand = @"select base.createdate as createdate
  from ctcheckorder ct,
       (select max(modifydate) as modifydate,
               createdate,
               inpatientid,
               inpatientdate,
               status,
               applicationid
          from ctcheckorder
         group by createdate,
                  inpatientid,
                  inpatientdate,
                  status,
                  applicationid) base
 where ct.inpatientid = base.inpatientid
   and base.status = 0
   and ct.inpatientdate = base.inpatientdate
   and ct.createdate = base.createdate
   and ct.modifydate = base.modifydate
   and ct.inpatientid = ?
   and ct.inpatientdate = ?";
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
			}
            try
            {
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
		public long m_lngGetApplicationID(ref string  p_strXml,ref int  intRows)
		{
			string strCommand="";
			long lngRes = -1;
			//strCommand="select seq_ct_applicationid.nextval as applicationid from dual";
			if(clsHRPTableService.bytDatabase_Selector == 0)
				strCommand="select max(cast(right(applicationid, len(applicationid) - 1) as bigint))  as applicationid  from ctcheckorder";
            else if (clsHRPTableService.bytDatabase_Selector == 2)
				strCommand="select nvl(max(applicationid),0) as applicationid from ctcheckorder";
            else if (clsHRPTableService.bytDatabase_Selector == 4)
                strCommand = "select coalesce(max(applicationid),0) as applicationid from ctcheckorder";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = objHRPServ.lngGetXMLTable(strCommand, ref p_strXml, ref intRows);
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
