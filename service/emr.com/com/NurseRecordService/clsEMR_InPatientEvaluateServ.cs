using System;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.InPatientEvaluateServ
{
	/// <summary>
	/// 病人入院评估表
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsEMR_InPatientEvaluateServ : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsEMR_InPatientEvaluateServ()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		[AutoComplete]
		public long m_lngGetFirstPrintDate(
			string p_strInPatientID,string p_strInPatientDate,out string p_strFirstPrintDate)
		{		
			p_strFirstPrintDate="";

            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsEMR_InPatientEvaluateServ","m_lngGetFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			long lngRes = -1;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			try
			{
				//查询第一次打印时间				
				string strCommand = "select firstprintdate  from t_opr_emr_inpatientevaluate where status=0 and inpatientid=? and inpatientdate=? ";

                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = Convert.ToDateTime(p_strInPatientDate);

				System.Data.DataTable  dtbResult=new System.Data.DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(strCommand, ref dtbResult, objDPArr);
				p_strFirstPrintDate="";
				if(lngRes>0 && dtbResult.Rows.Count>0)
					p_strFirstPrintDate=dtbResult.Rows[0][0].ToString();
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
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
		public long m_lngUpdateFirstPrintDate(
			string p_strInPatientID,string p_strInPatientDate)
		{
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsEMR_InPatientEvaluateServ","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;
            
			clsHRPTableService objHRPServ =new clsHRPTableService();
            DateTime dtNow = DateTime.Now;
			//更新第一次打印时间				
			string strCommand = "update t_opr_emr_inpatientevaluate set firstprintdate=?  where firstprintdate is null and status=0 and inpatientid=? and inpatientdate=?";

            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].DbType = DbType.DateTime;
            objDPArr[0].Value = Convert.ToDateTime(dtNow.ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr[1].Value = p_strInPatientID;
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = Convert.ToDateTime(p_strInPatientDate);

            long lngEff = -1;
            long lngRes = objHRPServ.lngExecuteParameterSQL(strCommand, ref lngEff,objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		/// <summary>
		/// 获得表中记录的住院时间
		/// </summary>
		[AutoComplete]		
		public long m_lngGetAllRecordDateArr(
			string p_strInPatientID,out string p_strXML,out int p_intRows)
		{
			p_strXML="";
			p_intRows=0;	
	
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsEMR_InPatientEvaluateServ","m_lngGetAllRecordDateArr");
			//if(lngCheckRes <= 0)
            //return lngCheckRes;
            clsHRPTableService objHRPServ = new clsHRPTableService();

			string strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       paymentmethod,
       educationdegree,
       inhospitalmethod,
       inhospitaldiagnose,
       casehistory,
       familyhistory,
       chiefcomplaint,
       sensitivehistory,
       sensitivehistory_other,
       bodytemperature,
       bp_shrink,
       bp_extend,
       pulse,
       heartrhythm,
       avoirdupois,
       shengao,
       consciousness,
       complexion,
       physique,
       physique_other,
       emotion,
       skin,
       skin_other,
       limbsactivity,
       limbsactivity_other,
       bitesup,
       appetite,
       sleep,
       stool,
       astriction,
       diarrhea,
       stool_other,
       pee,
       hobby,
       hobby_other,
       selfsolve,
       feeling,
       job,
       inhospitalworry,
       inhospitalworry_other,
       healthneed,
       familyform,
       familyform_other,
       knowdisease,
       specializedcheck,
       pipinstance,
       woodinstance,
       tendplan,
       status,
       modifydate,
       modifyuserid,
       firstprintdate,
       deactiveddate,
       deactivedoperatorid,
       nurseid,
       inhospitaldiagnosexml,
       casehistoryxml,
       chiefcomplaintxml,
       sensitivehistory_otherxml,
       physique_otherxml,
       skin_otherxml,
       limbsactivity_otherxml,
       stool_otherxml,
       hobby_otherxml,
       inhospitalworry_otherxml,
       familyform_otherxml,
       specializedcheckxml,
       pipinstancexml,
       woodinstancexml,
       tendplanxml,
       familyhistoryxml
  from t_opr_emr_inpatientevaluate
 where status = 0
   and inpatientid = ?
 order by inpatientdate";

            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		/// <summary>
		/// 查出时间对应的表信息
		/// </summary>	
		[AutoComplete]	
		private long m_lngGetLatestRecord(string p_strInPatientID,string p_strInPatientDate,out string p_strXML,out int p_intRows)
		{
			p_strXML="";
			p_intRows=0;
            string strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       paymentmethod,
       educationdegree,
       inhospitalmethod,
       inhospitaldiagnose,
       casehistory,
       familyhistory,
       chiefcomplaint,
       sensitivehistory,
       sensitivehistory_other,
       bodytemperature,
       bp_shrink,
       bp_extend,
       pulse,
       heartrhythm,
       avoirdupois,
       shengao,
       consciousness,
       complexion,
       physique,
       physique_other,
       emotion,
       skin,
       skin_other,
       limbsactivity,
       limbsactivity_other,
       bitesup,
       appetite,
       sleep,
       stool,
       astriction,
       diarrhea,
       stool_other,
       pee,
       hobby,
       hobby_other,
       selfsolve,
       feeling,
       job,
       inhospitalworry,
       inhospitalworry_other,
       healthneed,
       familyform,
       familyform_other,
       knowdisease,
       specializedcheck,
       pipinstance,
       woodinstance,
       tendplan,
       status,
       modifydate,
       modifyuserid,
       firstprintdate,
       deactiveddate,
       deactivedoperatorid,
       nurseid,
       inhospitaldiagnosexml,
       casehistoryxml,
       chiefcomplaintxml,
       sensitivehistory_otherxml,
       physique_otherxml,
       skin_otherxml,
       limbsactivity_otherxml,
       stool_otherxml,
       hobby_otherxml,
       inhospitalworry_otherxml,
       familyform_otherxml,
       specializedcheckxml,
       pipinstancexml,
       woodinstancexml,
       tendplanxml,
       familyhistoryxml
  from t_opr_emr_inpatientevaluate a
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and a.modifydate =
       (select max(modifydate)
          from t_opr_emr_inpatientevaluate
         where inpatientid = a.inpatientid
           and inpatientdate = a.inpatientdate)";
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(p_strInPatientDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		[AutoComplete]	
		private long m_lngGetLatestHealthEduRecord(string p_strInPatientID,string p_strInPatientDate,out string p_strXML,out int p_intRows)
		{
			p_strXML="";
			p_intRows=0;
            string strCommand = @"select inpatientid,
       inpatientdate,
       hedu_first,
       hedu_second,
       hedu_three,
       opendate,
       status,
       deactiveddate,
       deactivedoperatorid,
       modifydate,
       modifyuserid,
       writeformdate
  from t_opr_emr_healthedu a
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and a.modifydate =
       (select max(modifydate)
          from t_opr_emr_healthedu
         where inpatientid = a.inpatientid
           and inpatientdate = a.inpatientdate)";
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(p_strInPatientDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		[AutoComplete]	
		private long m_lngGetLatestPatientOutRecord(string p_strInPatientID,string p_strInPatientDate,out string p_strXML,out int p_intRows)
		{
			p_strXML="";
			p_intRows=0;
            string strCommand = @"select a.inpatientid,
       a.inpatientdate,
       a.opendate,
       a.outhospitaldiagnose,
       a.outhospitaldiagnosexml,
       a.liveability,
       a.dieteticcircs,
       a.outhospitalmode,
       a.isnursesyndrome,
       a.nursesyndrome,
       a.nursesyndromexml,
       a.isnurseissue,
       a.nurseissue,
       a.nurseissuexml,
       a.commonlycoach,
       a.advicedrug,
       a.isspecialtiescoach,
       a.specialtiescoach,
       a.specialtiescoachxml,
       a.nursesign_id,
       a.chargenurse_id,
       a.inpatientdays,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid,
       a.modifydate,
       a.modifyuserid
  from t_opr_emr_inpatientoutevaluate a
 where a.inpatientid = ?
   and a.inpatientdate = ?
   and a.status = 0
   and a.modifydate =
       (select max(modifydate)
          from t_opr_emr_inpatientoutevaluate
         where inpatientid = a.inpatientid
           and inpatientdate = a.inpatientdate)";
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(p_strInPatientDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		[AutoComplete]
		public long m_lngGetLatestRecord_All(
			string p_strInPatientID,string p_strInPatientDate,out string p_strXML,out int p_intRows,out string p_strXMLEdu,out int p_intRowsEdu,out string p_strXMLOut,out int p_intRowsOut)
		{
			p_strXML="";
			p_strXMLEdu="";
			p_strXMLOut="";
			p_intRows=0;
			p_intRowsEdu = 0;
			p_intRowsOut = 0;
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsEMR_InPatientEvaluateServ","m_lngGetLatestRecord_All");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			long lngRes=m_lngGetLatestRecord(p_strInPatientID,p_strInPatientDate,out p_strXML,out p_intRows);
			if(lngRes>0 && p_intRows>0)
			{
				lngRes=m_lngGetLatestHealthEduRecord(p_strInPatientID,p_strInPatientDate,out p_strXMLEdu,out p_intRowsEdu);
				lngRes=m_lngGetLatestPatientOutRecord(p_strInPatientID,p_strInPatientDate,out p_strXMLOut,out p_intRowsOut);
			}
			return lngRes;
		}

		[AutoComplete]
		public long m_lngGetLatestDeleteRecord_All(
			string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string p_strXML,out int p_intRows,out string p_strXMLEdu,out int p_intRowsEdu,out string p_strXMLOut,out int p_intRowsOut)
		{
			p_strXML="";
			p_intRows=0;
			p_strXMLEdu="";
			p_intRowsEdu=0;
			p_strXMLOut="";
			p_intRowsOut=0;
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsEMR_InPatientEvaluateServ","m_lngGetLatestDeleteRecord_All");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;

			long lngRes=m_lngGetLatestDeleteRecord(p_strInPatientID,p_strInPatientDate,p_strOpenDate,out p_strXML,out p_intRows);
			if(lngRes>0 && p_intRows>0)
			{
				lngRes=m_lngGetLatestEduDeleteRecord(p_strInPatientID,p_strInPatientDate,p_strOpenDate,out p_strXMLEdu,out p_intRowsEdu);
				lngRes=m_lngGetLatestOutDeleteRecord(p_strInPatientID,p_strInPatientDate,p_strOpenDate,out p_strXMLOut,out p_intRowsOut);
			}
			return lngRes;
		}

		/// <summary>
		/// 查出时间对应的表信息
		/// </summary>	
		[AutoComplete]	
		private long m_lngGetLatestDeleteRecord(string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string p_strXML,out int p_intRows)
		{
			p_strXML="";
			p_intRows=0;
            string strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       createdate,
       createuserid,
       paymentmethod,
       educationdegree,
       inhospitalmethod,
       inhospitaldiagnose,
       casehistory,
       familyhistory,
       chiefcomplaint,
       sensitivehistory,
       sensitivehistory_other,
       bodytemperature,
       bp_shrink,
       bp_extend,
       pulse,
       heartrhythm,
       avoirdupois,
       shengao,
       consciousness,
       complexion,
       physique,
       physique_other,
       emotion,
       skin,
       skin_other,
       limbsactivity,
       limbsactivity_other,
       bitesup,
       appetite,
       sleep,
       stool,
       astriction,
       diarrhea,
       stool_other,
       pee,
       hobby,
       hobby_other,
       selfsolve,
       feeling,
       job,
       inhospitalworry,
       inhospitalworry_other,
       healthneed,
       familyform,
       familyform_other,
       knowdisease,
       specializedcheck,
       pipinstance,
       woodinstance,
       tendplan,
       status,
       modifydate,
       modifyuserid,
       firstprintdate,
       deactiveddate,
       deactivedoperatorid,
       nurseid,
       inhospitaldiagnosexml,
       casehistoryxml,
       chiefcomplaintxml,
       sensitivehistory_otherxml,
       physique_otherxml,
       skin_otherxml,
       limbsactivity_otherxml,
       stool_otherxml,
       hobby_otherxml,
       inhospitalworry_otherxml,
       familyform_otherxml,
       specializedcheckxml,
       pipinstancexml,
       woodinstancexml,
       tendplanxml,
       familyhistoryxml
  from t_opr_emr_inpatientevaluate
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 1";
			
            clsHRPTableService objHRPServ = new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(p_strInPatientDate);
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = Convert.ToDateTime(p_strOpenDate);

            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		[AutoComplete]	
		private long m_lngGetLatestEduDeleteRecord(string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string p_strXML,out int p_intRows)
		{
			p_strXML="";
			p_intRows=0;
            string strCommand = @"select inpatientid,
       inpatientdate,
       hedu_first,
       hedu_second,
       hedu_three,
       opendate,
       status,
       deactiveddate,
       deactivedoperatorid,
       modifydate,
       modifyuserid,
       writeformdate
  from t_opr_emr_healthedu
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 1";
            
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(p_strInPatientDate);
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = Convert.ToDateTime(p_strOpenDate);
            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		[AutoComplete]	
		private long m_lngGetLatestOutDeleteRecord(string p_strInPatientID,string p_strInPatientDate,string p_strOpenDate,out string p_strXML,out int p_intRows)
		{
			p_strXML="";
			p_intRows=0;
            string strCommand = @"select inpatientid,
       inpatientdate,
       opendate,
       outhospitaldiagnose,
       outhospitaldiagnosexml,
       liveability,
       dieteticcircs,
       outhospitalmode,
       isnursesyndrome,
       nursesyndrome,
       nursesyndromexml,
       isnurseissue,
       nurseissue,
       nurseissuexml,
       commonlycoach,
       advicedrug,
       isspecialtiescoach,
       specialtiescoach,
       specialtiescoachxml,
       nursesign_id,
       chargenurse_id,
       inpatientdays,
       status,
       deactiveddate,
       deactivedoperatorid,
       modifydate,
       modifyuserid
  from t_opr_emr_inpatientoutevaluate
 where inpatientid = ?
   and inpatientdate = ?
   and opendate = ?
   and status = 1";
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(p_strInPatientDate);
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = Convert.ToDateTime(p_strOpenDate);
            long lngRes = objHRPServ.lngGetXMLTableWithParameter(strCommand, ref p_strXML, ref p_intRows, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}

		/// <summary>
		/// 添加信息
		/// </summary>
		[AutoComplete]	
		public long m_lngAddNew(
			string p_strXml,string p_strXmlEdu,string p_strXmlOut)
		{
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsMainGeneralNurseRecordService","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;
            
			clsHRPTableService objHRPServ =new clsHRPTableService();
			//添加记录时，主从表同时添加一条记录			
			if(p_strXml=="")return -1;			
			long lngRes=objHRPServ.add_new_record("T_OPR_EMR_INPATIENTEVALUATE",p_strXml);
			if(lngRes == 1)
			{
                if (!string.IsNullOrEmpty(p_strXmlEdu))
                {
                    lngRes = objHRPServ.add_new_record("T_OPR_EMR_HEALTHEDU", p_strXmlEdu);
                }
                if (!string.IsNullOrEmpty(p_strXmlOut))
                {
                    lngRes = objHRPServ.add_new_record("T_OPR_EMR_INPATIENTOUTEVALUATE", p_strXmlOut);
                }
			}
            //objHRPServ.Dispose();
			return lngRes;
		}
		   
		/// <summary>
		/// 修改信息
		/// </summary>	
		[AutoComplete]	
		public long m_lngModify(
			string p_strXml,string p_strXmlEdu,string p_strXmlOut)
		{
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsEMR_InPatientEvaluateServ","m_lngModify");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;
            
			clsHRPTableService objHRPServ =new clsHRPTableService();
			//修改记录时，主表更新原来记录中的相关XML记录，同时从表添加一条记录	
			if(p_strXml=="")return -1;			
			long lngRes=objHRPServ.modify_record("T_OPR_EMR_INPATIENTEVALUATE",p_strXml,"INPATIENTID","INPATIENTDATE","OPENDATE"); 
			if(lngRes == 1)
			{
                if (!string.IsNullOrEmpty(p_strXmlEdu))
                {
                    lngRes = objHRPServ.modify_record("T_OPR_EMR_HEALTHEDU", p_strXmlEdu, "INPATIENTID", "INPATIENTDATE", "OPENDATE");
                }
                if (!string.IsNullOrEmpty(p_strXmlOut))
                {
                    lngRes = objHRPServ.modify_record("T_OPR_EMR_INPATIENTOUTEVALUATE", p_strXmlOut, "INPATIENTID", "INPATIENTDATE", "OPENDATE");
                }
			}
            //objHRPServ.Dispose();
			return lngRes;
		}

		/// <summary>
		/// 删除
		/// </summary>	
		[AutoComplete]		
		public long m_lngDelete(string p_strTableName,string p_strDeActivedOperatorID,string p_strInPatientID,string p_strInPatientDate)
		{
			string strSQL="update "+p_strTableName+ " set status=1 , deactivedoperatorid=? , deactiveddate= ?"+
				" where inpatientid=? and inpatientdate=? and status=0" ;
			
			clsHRPTableService objHRPServ =new clsHRPTableService();
            IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(4, out objDPArr);
            objDPArr[0].Value = p_strDeActivedOperatorID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr[2].Value = p_strInPatientID;
            objDPArr[3].DbType = DbType.DateTime;
            objDPArr[3].Value = Convert.ToDateTime(p_strInPatientDate);

            long lngEff = -1;
            long lngRes = objHRPServ.lngExecuteParameterSQL(strSQL,ref lngEff, objDPArr);
            //objHRPServ.Dispose();
            return lngRes;
		}


	}
}
