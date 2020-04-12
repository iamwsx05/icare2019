using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;

namespace com.digitalwave.iCare.Public.MenuExtend.Service
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsInPatientCaseHisoryDefaultService2 : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		#region 获得所有表InPatientCaseHisoryDefault,Service层,刘颖源,2003-5-20 15:29:59
		[AutoComplete]
		public long lngGetAllInPatientCaseHisoryDefault(string p_strInPaitentID,string p_strInPatientDate, out clsInPatientCaseHisoryDefaultValue [] p_objIPCaseHistory_HistoryContentValue)
		{

			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@" b.*,a.CreateDate FROM InPatientCaseHistory_History a 
inner join IPCaseHistory_HistoryContent b
on a.InPatientID = b.InPatientID
and a.InPatientDate = b.InPatientDate
and a.OpenDate=b.OpenDate 
WHERE (a.InPatientID = '"+p_strInPaitentID+"') AND (a.InPatientDate = "+clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate)+@")
and a.status = 0
ORDER BY b.LastModifyDate DESC"+clsDatabaseSQLConvert.s_StrRownum;	
			p_objIPCaseHistory_HistoryContentValue=null;

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege("clsInPatientCaseHisoryDefaultService","lngGetAllInPatientCaseHisoryDefault");
			//if(lngCheckRes <= 0)
			//	return lngCheckRes;

			DataTable objDataTableResult=new DataTable ();
			long lngRes = new clsHRPTableService().DoGetDataTable (strSQL, ref objDataTableResult);
			if(lngRes > 0 && objDataTableResult.Rows.Count >= 1)
			{
				p_objIPCaseHistory_HistoryContentValue = new clsInPatientCaseHisoryDefaultValue [objDataTableResult.Rows.Count];
				for(int i=0;i<p_objIPCaseHistory_HistoryContentValue.Length ;i++)
				{
					p_objIPCaseHistory_HistoryContentValue[i]=new clsInPatientCaseHisoryDefaultValue ();
					p_objIPCaseHistory_HistoryContentValue[i].m_strInPatientID=objDataTableResult.Rows[i]["INPATIENTID"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strInPatientDate=objDataTableResult.Rows[i]["INPATIENTDATE"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strOpenDate=objDataTableResult.Rows[i]["OPENDATE"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strLastModifyDate=objDataTableResult.Rows[i]["LASTMODIFYDATE"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strLastModifyUserID=objDataTableResult.Rows[i]["LASTMODIFYUSERID"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strDeActivedDate=objDataTableResult.Rows[i]["DEACTIVEDDATE"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strDeActivedOperatorID=objDataTableResult.Rows[i]["DEACTIVEDOPERATORID"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strStatus=objDataTableResult.Rows[i]["STATUS"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strMainDescription=objDataTableResult.Rows[i]["MAINDESCRIPTION"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strCurrentStatus=objDataTableResult.Rows[i]["CURRENTSTATUS"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strBeforetimeStatus=objDataTableResult.Rows[i]["BEFORETIMESTATUS"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strOwnHistory=objDataTableResult.Rows[i]["OWNHISTORY"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strMarriageHistory=objDataTableResult.Rows[i]["MARRIAGEHISTORY"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strCatameniaHistory=objDataTableResult.Rows[i]["CATAMENIAHISTORY"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strFamilyHistory=objDataTableResult.Rows[i]["FAMILYHISTORY"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strSummary=objDataTableResult.Rows[i]["SUMMARY"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strTemperature=objDataTableResult.Rows[i]["TEMPERATURE"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strPulse=objDataTableResult.Rows[i]["PULSE"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strBreath=objDataTableResult.Rows[i]["BREATH"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strSys=objDataTableResult.Rows[i]["SYS"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strDia=objDataTableResult.Rows[i]["DIA"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strBloodPressureUnit=objDataTableResult.Rows[i]["BLOODPRESSUREUNIT"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strMedical=objDataTableResult.Rows[i]["MEDICAL"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strProfessionalCheck=objDataTableResult.Rows[i]["PROFESSIONALCHECK"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strLabCheck=objDataTableResult.Rows[i]["LABCHECK"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strCreateDate=objDataTableResult.Rows[i]["CREATEDATE"].ToString();

					string strSQL2 = "SELECT PrimaryDiagnose " + 
						" FROM IPCaseHistCont_PrimaryDiagnose " + 
						" WHERE (InPatientID = '" + p_strInPaitentID + "') AND (InPatientDate = " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + ")" + 
						" AND (OpenDate = " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_objIPCaseHistory_HistoryContentValue[i].m_strOpenDate) + ") AND (LastModifyDate = " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_objIPCaseHistory_HistoryContentValue[i].m_strLastModifyDate) + ")";			
											
					DataTable dtbResult=null;
					string strResult="";
					long lngRes2 = new clsHRPTableService().DoGetDataTable (strSQL2, ref dtbResult);
					if(lngRes2 > 0 && dtbResult.Rows.Count >0)
					{
						for(int j2=0;j2<dtbResult.Rows.Count;j2++)
						{							
							strResult += dtbResult.Rows[j2][0].ToString();
							if(j2 !=dtbResult.Rows.Count -1)
								strResult +=";";
						}
					}

					p_objIPCaseHistory_HistoryContentValue[i].m_strPrimaryDiagnose=strResult;
					
					string strSQL3 = "SELECT FinallyDiagnose " + 
						" FROM IPCaseHistCont_FinallyDiagnose " + 
						" WHERE (InPatientID = '" + p_strInPaitentID + "') AND (InPatientDate = " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_strInPatientDate) + ")" + 
						" AND (OpenDate = " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_objIPCaseHistory_HistoryContentValue[i].m_strOpenDate) + ") AND (LastModifyDate = " + clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_objIPCaseHistory_HistoryContentValue[i].m_strLastModifyDate) + ")";			
											
					dtbResult=null;
					strResult="";
					lngRes2 = new clsHRPTableService().DoGetDataTable (strSQL3, ref dtbResult);
					if(lngRes2 > 0 && dtbResult.Rows.Count >0)
					{
						for(int j2=0;j2<dtbResult.Rows.Count;j2++)
						{							
							strResult += dtbResult.Rows[j2][0].ToString();
							if(j2 !=dtbResult.Rows.Count -1)
								strResult +=";";
						}
					}
					p_objIPCaseHistory_HistoryContentValue[i].m_strFinallyDiagnose=strResult;
					
				}
			}
			return(lngRes );
		}
		#endregion

		//		#region 获得所有表InPatientCaseHisoryDefault,Service层,刘颖源,2003-5-14 17:36:49
//		[AutoComplete]
//		public long lngGetAllInPatientCaseHisoryDefault(string p_strInPaitentID,string p_strInPatientDate, out clsInPatientCaseHisoryDefaultValue [] p_objValue)
//		{
//			string strSQL = "SELECT TOP 1 InPatientID, InPatientDate, OpenDate, LastModifyDate, FinallyDiagnose " + 
//				" FROM IPCaseHistory_HistoryContent " + 
//				" WHERE (InPatientID = '" + p_strInPaitentID + "') AND (InPatientDate = '" + p_strInPatientDate + "')" + 
//				" ORDER BY OpenDate DESC,LastModifyDate DESC";
//			p_objValue=null;
//			DataTable objDataTableResult=new DataTable ();
//			long lngRes = new clsHRPTableService().DoGetDataTable (strSQL, ref objDataTableResult);
//
//			if(lngRes > 0 && objDataTableResult.Rows.Count >= 1)
//			{
//				p_objValue = new clsInPatientCaseHisoryDefaultValue [objDataTableResult.Rows.Count];
//				for(int i=0;i<p_objValue.Length ;i++)
//				{
//					p_objValue[i]=new clsInPatientCaseHisoryDefaultValue ();
//					p_objValue[i].m_strInPatientID=objDataTableResult.Rows[i]["INPATIENTID"].ToString();
//					p_objValue[i].m_strInPatientDate=objDataTableResult.Rows[i]["INPATIENTDATE"].ToString();
//					p_objValue[i].m_strOpenDate=objDataTableResult.Rows[i]["OPENDATE"].ToString();
//					p_objValue[i].m_strLastModifyDate=objDataTableResult.Rows[i]["LASTMODIFYDATE"].ToString();
//					p_objValue[i].m_strFinallyDiagnose=objDataTableResult.Rows[i]["FINALLYDIAGNOSE"].ToString();
//
//				}
//			}
//
//			return lngRes;
//		}
//
//		#endregion
	}
}
