using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;


namespace com.digitalwave.DataShareService
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsInPatientCaseHisoryDefaultService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		public clsInPatientCaseHisoryDefaultService()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region 获得所有表InPatientCaseHisoryDefault,Service层,刘颖源,2003-5-20 15:29:59
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPaitentID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_objIPCaseHistory_HistoryContentValue"></param>
		/// <returns></returns>
		[AutoComplete]
		public long lngGetAllInPatientCaseHisoryDefault(
			string p_strInPaitentID,string p_strInPatientDate, out clsInPatientCaseHisoryDefaultValue [] p_objIPCaseHistory_HistoryContentValue)
		{

            clsHRPTableService objHRPServ = new clsHRPTableService();
            string strSQL = clsDatabaseSQLConvert.s_StrTop1 + @" b.inpatientid,
       b.inpatientdate,
       b.opendate,
       b.lastmodifydate,
       b.lastmodifyuserid,
       b.deactiveddate,
       b.deactivedoperatorid,
       b.status,
       b.maindescription,
       b.currentstatus,
       b.beforetimestatus,
       b.ownhistory,
       b.marriagehistory,
       b.catameniahistory,
       b.familyhistory,
       b.summary,
       b.temperature,
       b.pulse,
       b.breath,
       b.sys,
       b.dia,
       b.bloodpressureunit,
       b.medical,
       b.professionalcheck,
       b.labcheck,
       b.firstcatamenia,
       b.catamenialasttime,
       b.catameniacycle,
       b.lastcatameniatime,
       b.catameniacase,
       b.yjs,
       b.contrahistory,
       b.shys,
       b.lcqk,
       b.cqjc,
       b.pregtimes,
       b.borntimes,
       b.careplan,
       b.chargedoctor,
       b.diretdoctor,
       b.oldmaternitysuffer,
       b.midwife,
       b.modifydiagnose,
       b.adddiagnose,
       b.diagnosenor,a.createdate from inpatientcasehistory_history a 
inner join ipcasehistory_historycontent b
on a.inpatientid = b.inpatientid
and a.inpatientdate = b.inpatientdate
and a.opendate=b.opendate 
where (a.inpatientid = ?) and (a.inpatientdate = ?)
and a.status = 0
order by b.lastmodifydate desc" + clsDatabaseSQLConvert.s_StrRownum;			
			p_objIPCaseHistory_HistoryContentValue=null;

			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsInPatientCaseHisoryDefaultService","lngGetAllInPatientCaseHisoryDefault");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;
			DataTable objDataTableResult=new DataTable ();

            IDataParameter[] objDPArr = null;

            objHRPServ.CreateDatabaseParameter(2, out objDPArr);
            objDPArr[0].Value = p_strInPaitentID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
            long lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref objDataTableResult, objDPArr);

			if(lngRes > 0 && objDataTableResult.Rows.Count >= 1)
			{
                DataRow objRow = null;
				p_objIPCaseHistory_HistoryContentValue = new clsInPatientCaseHisoryDefaultValue [objDataTableResult.Rows.Count];
				for(int i=0;i<p_objIPCaseHistory_HistoryContentValue.Length ;i++)
				{
                    objRow = objDataTableResult.Rows[i];
					p_objIPCaseHistory_HistoryContentValue[i]=new clsInPatientCaseHisoryDefaultValue ();
					p_objIPCaseHistory_HistoryContentValue[i].m_strInPatientID=objRow["INPATIENTID"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strInPatientDate=objRow["INPATIENTDATE"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strOpenDate=objRow["OPENDATE"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strLastModifyDate=objRow["LASTMODIFYDATE"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strLastModifyUserID=objRow["LASTMODIFYUSERID"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strDeActivedDate=objRow["DEACTIVEDDATE"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strDeActivedOperatorID=objRow["DEACTIVEDOPERATORID"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strStatus=objRow["STATUS"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strMainDescription=objRow["MAINDESCRIPTION"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strCurrentStatus=objRow["CURRENTSTATUS"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strBeforetimeStatus=objRow["BEFORETIMESTATUS"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strOwnHistory=objRow["OWNHISTORY"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strMarriageHistory=objRow["MARRIAGEHISTORY"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strCatameniaHistory=objRow["CATAMENIAHISTORY"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strFamilyHistory=objRow["FAMILYHISTORY"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strSummary=objRow["SUMMARY"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strTemperature=objRow["TEMPERATURE"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strPulse=objRow["PULSE"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strBreath=objRow["BREATH"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strSys=objRow["SYS"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strDia=objRow["DIA"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strBloodPressureUnit=objRow["BLOODPRESSUREUNIT"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strMedical=objRow["MEDICAL"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strProfessionalCheck=objRow["PROFESSIONALCHECK"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strLabCheck=objRow["LABCHECK"].ToString();
					p_objIPCaseHistory_HistoryContentValue[i].m_strCreateDate=objRow["CREATEDATE"].ToString();
                    p_objIPCaseHistory_HistoryContentValue[i].m_strModifyDiagnose = objRow["MODIFYDIAGNOSE"].ToString();
                    p_objIPCaseHistory_HistoryContentValue[i].m_strAddDiagnose = objRow["ADDDIAGNOSE"].ToString();

					string strSQL2 = "select primarydiagnose " + 
						" from ipcasehistcont_primarydiagnose " + 
						" where (inpatientid = ?) and (inpatientdate = ?)" + 
						" and (opendate = ?) and (lastmodifydate = ?)";			
										
					DataTable dtbResult=null;

                    IDataParameter[] objDPArr2 = null;

                    objHRPServ.CreateDatabaseParameter(4, out objDPArr2);
                    objDPArr2[0].Value = p_strInPaitentID;
                    objDPArr2[1].DbType = DbType.DateTime;
                    objDPArr2[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr2[2].DbType = DbType.DateTime;
                    objDPArr2[2].Value = DateTime.Parse(p_objIPCaseHistory_HistoryContentValue[i].m_strOpenDate);
                    objDPArr2[3].DbType = DbType.DateTime;
                    objDPArr2[3].Value = DateTime.Parse(p_objIPCaseHistory_HistoryContentValue[i].m_strLastModifyDate);

                    long lngRes2 = objHRPServ.lngGetDataTableWithParameters(strSQL2, ref dtbResult, objDPArr2);

					string strResult="";

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
					
					string strSQL3 = "select finallydiagnose " + 
						" from ipcasehistcont_finallydiagnose " + 
						" where (inpatientid = ?) and (inpatientdate = ?)" + 
						" and (opendate = ?) and (lastmodifydate = ?)";			
										
					dtbResult=null;

                    IDataParameter[] objDPArr3 = null;

                    objHRPServ.CreateDatabaseParameter(4, out objDPArr3);
                    objDPArr3[0].Value = p_strInPaitentID;
                    objDPArr3[1].DbType = DbType.DateTime;
                    objDPArr3[1].Value = DateTime.Parse(p_strInPatientDate);
                    objDPArr3[2].DbType = DbType.DateTime;
                    objDPArr3[2].Value = DateTime.Parse(p_objIPCaseHistory_HistoryContentValue[i].m_strOpenDate);
                    objDPArr3[3].DbType = DbType.DateTime;
                    objDPArr3[3].Value = DateTime.Parse(p_objIPCaseHistory_HistoryContentValue[i].m_strLastModifyDate);

                    lngRes2 = objHRPServ.lngGetDataTableWithParameters(strSQL3, ref dtbResult, objDPArr3);

					strResult="";
                    
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
            //objTabService.Dispose();
			return(lngRes );
		}
		#endregion

		#region 获得所有表InPatientCaseHisoryDefault,Service层,刘颖源,2003-5-14 17:36:49
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
		#endregion
	}
}
