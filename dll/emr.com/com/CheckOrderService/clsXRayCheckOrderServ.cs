using System;
using System.Text ;
using System.EnterpriseServices;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.PACSService;
using com.digitalwave.Utility.SQLConvert;
using System.Data;

namespace com.digitalwave.XRayCheckOrderServ
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsXRayCheckOrderServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
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
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsXRayCheckOrderServ", "m_lngGetTimeInfoOfAPatient");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                strCommand = "select distinct createdate from xraycheckorder where inpatientid=? and status =0 and inpatientdate=?";

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
		public long m_lngAddNew(
			string p_strMainXml,string[] p_strCommondXml,string[] p_strSpecialXml,string[] p_strOperatorXml,
			ImageRequest p_objApplicationInfo,ref string p_strApplicationID,bool p_bnlIsNew)
		{
			string m_strApplicationID="";
			long m_lngRe;
			clsPACSService m_PACSSer=new clsPACSService();
			long lngRes = -1;

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

                    ///在PS_ImageApplication表中更新一条记录
                    m_lngRe = m_PACSSer.m_lngUpdatePACSApplication(p_objApplicationInfo, m_strApplicationID);

                }

                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsXRayCheckOrderServ", "m_lngAddNew");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                //添加记录时，主从表同时添加一条记录			
                if (p_strMainXml == "")
                    return -1;

                string strCurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                int intMainIndex = p_strMainXml.IndexOf(' ');

                p_strMainXml = p_strMainXml.Insert(intMainIndex, " ModifyDate ='" + strCurrentTime + "'  ApplicationID='" + m_strApplicationID + "' ");


                lngRes = objHRPServ.add_new_record("XRayCheckOrder", p_strMainXml);
                if (lngRes == 1)
                {
                    for (int i = 0; i < p_strCommondXml.Length; i++)
                    {
                        int intCommondIndex = p_strCommondXml[i].IndexOf(' ');

                        p_strCommondXml[i] = p_strCommondXml[i].Insert(intCommondIndex, " ModifyDate='" + strCurrentTime + "'");

                        lngRes = objHRPServ.add_new_record("XRayCommonRecord", p_strCommondXml[i]);
                        if (lngRes != 1)
                            break;
                    }
                }

                if (lngRes == 1)
                {
                    for (int i = 0; i < p_strSpecialXml.Length; i++)
                    {
                        int intSpecialIndex = p_strSpecialXml[i].IndexOf(' ');

                        p_strSpecialXml[i] = p_strSpecialXml[i].Insert(intSpecialIndex, " ModifyDate = '" + strCurrentTime + "' ");

                        lngRes = objHRPServ.add_new_record("XRaySpecialRecord", p_strSpecialXml[i]);
                        if (lngRes != 1)
                            break;
                    }
                }

                if (lngRes == 1)
                {
                    for (int i = 0; i < p_strOperatorXml.Length; i++)
                    {
                        int intOperatorIndex = p_strOperatorXml[i].IndexOf(' ');

                        p_strOperatorXml[i] = p_strOperatorXml[i].Insert(intOperatorIndex, " ModifyDate ='" + strCurrentTime + "' ");

                        lngRes = objHRPServ.add_new_record("XRayCheckOperator", p_strOperatorXml[i]);
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
		public long GetXRayCheckOrder(
			string p_strPatientID,string p_strInPatientDate,
			string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsXRayCheckOrderServ", "GetXRayCheckOrder");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                if (clsHRPTableService.bytDatabase_Selector == 0)
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
       a.history,
       a.clinicalcheckandresult,
       a.clinicaldignose,
       a.checkaim,
       a.checkplace,
       a.clairvoyance,
       a.photo,
       a.nothaveoldphoto,
       a.haveoldphoto,
       a.haveoldphotoout,
       a.charge,
       a.additioncharge,
       a.checkpartselection,
       a.applicationid,
       a.xrayno,
       a.insuranceno,
       a.othercheckinfo,
       a.contactinfo
  from xraycheckorder a
 where a.inpatientid = ?
   and a.status = 0
   and a.createdate = ?
   and a.inpatientdate = ?
 order by modifydate desc";
                else if (clsHRPTableService.bytDatabase_Selector == 2)
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
       history,
       clinicalcheckandresult,
       clinicaldignose,
       checkaim,
       checkplace,
       clairvoyance,
       photo,
       nothaveoldphoto,
       haveoldphoto,
       haveoldphotoout,
       charge,
       additioncharge,
       checkpartselection,
       applicationid,
       xrayno,
       insuranceno,
       othercheckinfo,
       contactinfo
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
               a.history,
               a.clinicalcheckandresult,
               a.clinicaldignose,
               a.checkaim,
               a.checkplace,
               a.clairvoyance,
               a.photo,
               a.nothaveoldphoto,
               a.haveoldphoto,
               a.haveoldphotoout,
               a.charge,
               a.additioncharge,
               a.checkpartselection,
               a.applicationid,
               a.xrayno,
               a.insuranceno,
               a.othercheckinfo,
               a.contactinfo
          from xraycheckorder a
         where a.inpatientid = ?
           and a.status = 0
           and a.createdate = ?
           and a.inpatientdate = ?
         order by modifydate desc)
 where rownum = 1";
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                    strCommand = @"select a.inpatientid,
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
       a.history,
       a.clinicalcheckandresult,
       a.clinicaldignose,
       a.checkaim,
       a.checkplace,
       a.clairvoyance,
       a.photo,
       a.nothaveoldphoto,
       a.haveoldphoto,
       a.haveoldphotoout,
       a.charge,
       a.additioncharge,
       a.checkpartselection,
       a.applicationid,
       a.xrayno,
       a.insuranceno,
       a.othercheckinfo,
       a.contactinfo
  from xraycheckorder a
 where a.inpatientid = ?
   and a.status = 0
   and a.createdate = ?
   and a.inpatientdate = ?
 order by modifydate desc fetch first 1 row only";

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
		/// 获得从表的记录 -- Commond Record
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long GetXRayCommonRecor(
			string p_strPatientID,string p_strInPatientDate,
			string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsXRayCheckOrderServ", "GetXRayCommonRecor");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                strCommand = @"select inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       photoid,
       checkplace,
       mappingplace,
       photoarea,
       photothickness,
       distance,
       voltage,
       electricity,
       disposetime,
       bucky
  from xraycommonrecord
 where inpatientid = ?
   and inpatientdate = ?
   and createdate = ?
   and modifydate = (select max(modifydate)
                       from xraycheckorder
                      where inpatientid = ?
                        and inpatientdate = ?
                        and createdate = ?)
 order by photoid";
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

		/// <summary>
		/// 获得从表的记录 -- Special Record
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long GetXRaySpecialRecor(string p_strPatientID,string p_strInPatientDate,
			string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsXRayCheckOrderServ", "GetXRaySpecialRecor");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                strCommand = @"select inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       photoid,
       photoseq,
       checkplace,
       timeofafterinject,
       fisrtoperatorid,
       remark
  from xrayspecialrecord
 where inpatientid = ?
   and inpatientdate = ?
   and createdate = ?
   and modifydate = (select max(modifydate)
                       from xraycheckorder
                      where inpatientid = ?
                        and inpatientdate = ?
                        and createdate = ?)
 order by photoid";

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

		/// <summary>
		/// 获得从表的记录 -- Special Record
		/// </summary>
		/// <param name="p_strPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDate"></param>
		/// <param name="p_strXml"></param>
		/// <param name="intRows"></param>
		/// <returns></returns>
		[AutoComplete]
		public long GetXRayOperator(
			string p_strPatientID,string p_strInPatientDate,
			string p_strCreateDate,ref string  p_strXml,ref int  intRows)
		{
			long lngRes = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsXRayCheckOrderServ", "GetXRayOperator");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                string strCommand = "";

                strCommand = @"select inpatientid, inpatientdate, createdate, modifydate, operatorid
  from xraycheckoperator
 where inpatientid = ?
   and inpatientdate = ?
   and createdate = ?
   and modifydate = (select max(modifydate)
                       from xraycheckorder
                      where inpatientid = ?
                        and inpatientdate = ?
                        and createdate = ?)";
                
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
