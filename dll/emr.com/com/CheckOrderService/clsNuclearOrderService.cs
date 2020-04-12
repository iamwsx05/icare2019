using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;


namespace com.digitalwave.CheckOrderService
{
	/// <summary>
	/// Summary description for clsNuclearOrderService.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsNuclearOrderService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		//不允许有实例变量。所有函数的返回值都是long，传出值用out
		private const string m_strGetAllTimeInfoByPatient=@"select distinct createdate from nuclearorder 
			where inpatientid=? and status = '0' and inpatientdate=?";

        private const string m_strGetNuclearOrderSQL_Oracle = @"select inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       createuserid,
       telephone,
       address,
       lastcheck,
       requestdate,
       bespeak,
       numbers,
       maindescription,
       isnosesnore,
       nosesnorelevel,
       nosesnorebeginyear,
       nosesnorebeginmonth,
       issnooze,
       snoozelevel,
       snoozebeginyear,
       snoozebeginmonth,
       gowithsymptom,
       sleep,
       other,
       diseasehistory,
       irritability,
       height,
       weight,
       bloodpressure,
       headneck,
       headneckother,
       heart,
       lung,
       otherpart,
       clinicaldiagnose,
       lastmedicines,
       requestersign,
       status,
       deactiveddate,
       deactivedoperatorid
  from (select a.inpatientid,
               a.inpatientdate,
               a.createdate,
               a.modifydate,
               a.createuserid,
               a.telephone,
               a.address,
               a.lastcheck,
               a.requestdate,
               a.bespeak,
               a.numbers,
               a.maindescription,
               a.isnosesnore,
               a.nosesnorelevel,
               a.nosesnorebeginyear,
               a.nosesnorebeginmonth,
               a.issnooze,
               a.snoozelevel,
               a.snoozebeginyear,
               a.snoozebeginmonth,
               a.gowithsymptom,
               a.sleep,
               a.other,
               a.diseasehistory,
               a.irritability,
               a.height,
               a.weight,
               a.bloodpressure,
               a.headneck,
               a.headneckother,
               a.heart,
               a.lung,
               a.otherpart,
               a.clinicaldiagnose,
               a.lastmedicines,
               a.requestersign,
               a.status,
               a.deactiveddate,
               a.deactivedoperatorid
          from nuclearorder a
         where trim(a.inpatientid) = ?
           and a.status = '0'
           and a.createdate = ?
           and a.inpatientdate = ?
         order by modifydate desc)
 where rownum = 1";

        private const string m_strGetNuclearOrderSQL_ODBC = @"select top 1 a.inpatientid,
       a.inpatientdate,
       a.createdate,
       a.modifydate,
       a.createuserid,
       a.telephone,
       a.address,
       a.lastcheck,
       a.requestdate,
       a.bespeak,
       a.numbers,
       a.maindescription,
       a.isnosesnore,
       a.nosesnorelevel,
       a.nosesnorebeginyear,
       a.nosesnorebeginmonth,
       a.issnooze,
       a.snoozelevel,
       a.snoozebeginyear,
       a.snoozebeginmonth,
       a.gowithsymptom,
       a.sleep,
       a.other,
       a.diseasehistory,
       a.irritability,
       a.height,
       a.weight,
       a.bloodpressure,
       a.headneck,
       a.headneckother,
       a.heart,
       a.lung,
       a.otherpart,
       a.clinicaldiagnose,
       a.lastmedicines,
       a.requestersign,
       a.status,
       a.deactiveddate,
       a.deactivedoperatorid
  from nuclearorder a
 where inpatientid = ?
   and a.status = '0'
   and a.createdate = ?
   and a.inpatientdate = ?
 order by modifydate desc";

		private const string m_InsertRecordSQL = @"insert into nuclearorder 
			(inpatientid,inpatientdate,createdate,modifydate,createuserid,telephone,address,lastcheck,
			requestdate,bespeak,numbers,maindescription,isnosesnore,nosesnorelevel,nosesnorebeginyear,
		nosesnorebeginmonth,issnooze,snoozelevel,snoozebeginyear,snoozebeginmonth,gowithsymptom,
		sleep,other,diseasehistory,irritability,height,weight,bloodpressure,headneck,headneckother,heart,lung,
		otherpart,clinicaldiagnose,lastmedicines,requestersign,status)
			values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		///系统中采用的删除操作是通过更新操作，更改数据表的Status属性，将属性由0设为1
		/// </summary>
		private const string m_DeleteRecordSQL=@"update nuclearorder set status=1,deactiveddate=?,deactivedoperatorid=? 
			where inpatientid=? and createdate=? and inpatientdate=?";


		

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
			string p_strInPatientID,string p_strInPatientDate,ref string[]  p_strDateInfo)
		{
			//			p_strDateInfo=new String[1];
			DataTable dtbValue = new DataTable();

//			long lngCheckRes = new com.digitalwave.PrivilegeSystemService.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsNuclearOrderService","m_lngGetTimeInfoOfAPatient");
//			////if (lngCheckRes <= 0)
//				//return lngCheckRes;

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);

            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
			objDPArr[1].Value=DateTime.Parse(DateTime.Parse(p_strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss"));

             try
            {
                long lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetAllTimeInfoByPatient, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strDateInfo = new String[dtbValue.Rows.Count];
                    for (int i1 = 0; i1 < dtbValue.Rows.Count; i1++)
                        p_strDateInfo[i1] = dtbValue.Rows[i1][0].ToString().Trim();

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
					
			return (long)enmOperationResult.DB_Succeed;

		}


		/// <summary>
		/// 获取病人的Nuclear记录
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_objNuclearOrderArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetNuclearOrder(
			string p_strPatientID,string p_strInPatientDate,
			string p_strCreateDate,out clsNuclearOrder p_objNuclearOrder)
		{		
			p_objNuclearOrder = new clsNuclearOrder();
			DataTable dtbValue = new DataTable();

//			long lngCheckRes = new com.digitalwave.PrivilegeSystemService.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsNuclearOrderService","m_lngGetNuclearOrder");
//			////if (lngCheckRes <= 0)
//				//return lngCheckRes;

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);

            objDPArr[0].Value = p_strPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(DateTime.Parse(p_strCreateDate).ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr[2].DbType = DbType.DateTime;
			objDPArr[2].Value=DateTime.Parse(DateTime.Parse(p_strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss"));

			try
			{
				long lngRes = -1;
				if(clsHRPTableService.bytDatabase_Selector == 0)
					lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetNuclearOrderSQL_ODBC,ref dtbValue,objDPArr);
				else
                    lngRes = objHRPServ.lngGetDataTableWithParameters(m_strGetNuclearOrderSQL_Oracle, ref dtbValue, objDPArr);
				
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					p_objNuclearOrder= new clsNuclearOrder();
					
					p_objNuclearOrder.strInPatientID=dtbValue.Rows[0]["INPATIENTID"].ToString();
					p_objNuclearOrder.strInPatientDate=dtbValue.Rows[0]["INPATIENTDATE"].ToString();
					p_objNuclearOrder.strCreateDate=dtbValue.Rows[0]["CREATEDATE"].ToString();
					p_objNuclearOrder.strCreateUserID=dtbValue.Rows[0]["CREATEUSERID"].ToString();
					p_objNuclearOrder.m_strTelephone=dtbValue.Rows[0]["TELEPHONE"].ToString();
					p_objNuclearOrder.m_strAddress=dtbValue.Rows[0]["ADDRESS"].ToString();
					p_objNuclearOrder.m_strLastCheck=dtbValue.Rows[0]["LASTCHECK"].ToString();
					p_objNuclearOrder.m_strRequestDate=dtbValue.Rows[0]["REQUESTDATE"].ToString();
					p_objNuclearOrder.m_strBespeak=dtbValue.Rows[0]["BESPEAK"].ToString();
					p_objNuclearOrder.m_strNumber=dtbValue.Rows[0]["NUMBERS"].ToString();
					p_objNuclearOrder.m_strMainDescription=dtbValue.Rows[0]["MAINDESCRIPTION"].ToString();
					try
					{
						p_objNuclearOrder.m_blnIsNoseSnore=bool.Parse(dtbValue.Rows[0]["ISNOSESNORE"].ToString() == "0"?"false":"true");}
					catch{p_objNuclearOrder.m_blnIsNoseSnore = false;}
					
					switch(int.Parse(dtbValue.Rows[0]["NOSESNORELEVEL"].ToString()))
					{
						case 0:
							p_objNuclearOrder.m_enmNoseSnoreLevel=enmLevel.enmNone;
							break;
						case 1:
							p_objNuclearOrder.m_enmNoseSnoreLevel=enmLevel.enmLitter ;
							break;

						case 2:
							p_objNuclearOrder.m_enmNoseSnoreLevel=enmLevel.enmSome ;
							break;
						case 3:
							p_objNuclearOrder.m_enmNoseSnoreLevel=enmLevel.enmMany;
							break;
					}
					
					p_objNuclearOrder.m_strNoseSnoreBeginYear=dtbValue.Rows[0]["NOSESNOREBEGINYEAR"].ToString();
					p_objNuclearOrder.m_strNoseSnoreBeginMonth=dtbValue.Rows[0]["NOSESNOREBEGINMONTH"].ToString();
					try
					{
						p_objNuclearOrder.m_blnIsSnooze=bool.Parse(dtbValue.Rows[0]["ISSNOOZE"].ToString() == "0"?"false":"true");}
					catch{p_objNuclearOrder.m_blnIsSnooze = false;}
					
					switch(int.Parse(dtbValue.Rows[0]["SNOOZELEVEL"].ToString()))
					{
						case 0:
							p_objNuclearOrder.m_enmSnoozeLevel=enmLevel.enmNone;
							break;
						case 1:
							p_objNuclearOrder.m_enmSnoozeLevel=enmLevel.enmLitter ;
							break;

						case 2:
							p_objNuclearOrder.m_enmSnoozeLevel=enmLevel.enmSome ;
							break;
						case 3:
							p_objNuclearOrder.m_enmSnoozeLevel=enmLevel.enmMany;
							break;
					}

					p_objNuclearOrder.m_strSnoozeBeginYear=dtbValue.Rows[0]["SNOOZEBEGINYEAR"].ToString();
					p_objNuclearOrder.m_strSnoozeBeginMonth=dtbValue.Rows[0]["SNOOZEBEGINMONTH"].ToString();
					p_objNuclearOrder.m_strGoWithSymptom=dtbValue.Rows[0]["GOWITHSYMPTOM"].ToString();
					p_objNuclearOrder.m_strSleep=dtbValue.Rows[0]["SLEEP"].ToString();
					p_objNuclearOrder.m_strOther=dtbValue.Rows[0]["OTHER"].ToString();
					p_objNuclearOrder.m_strDiseaseHistory=dtbValue.Rows[0]["DISEASEHISTORY"].ToString();
					p_objNuclearOrder.m_strIrritability=dtbValue.Rows[0]["IRRITABILITY"].ToString();
					p_objNuclearOrder.m_strHeight=dtbValue.Rows[0]["HEIGHT"].ToString();
					p_objNuclearOrder.m_strWeight=dtbValue.Rows[0]["WEIGHT"].ToString();
					p_objNuclearOrder.m_strBloodPressure=dtbValue.Rows[0]["BLOODPRESSURE"].ToString();
					p_objNuclearOrder.m_strHeadNeck=dtbValue.Rows[0]["HEADNECK"].ToString();
					p_objNuclearOrder.m_strHeadNeckOther =dtbValue.Rows[0]["HEADNECKOTHER"].ToString();
					p_objNuclearOrder.m_strHeart=dtbValue.Rows[0]["HEART"].ToString();
					p_objNuclearOrder.m_strLung=dtbValue.Rows[0]["LUNG"].ToString();
					p_objNuclearOrder.m_strOtherPart=dtbValue.Rows[0]["OTHERPART"].ToString();
					p_objNuclearOrder.m_strClinicalDiagnose=dtbValue.Rows[0]["CLINICALDIAGNOSE"].ToString();
					p_objNuclearOrder.m_strLastMedicines=dtbValue.Rows[0]["LASTMEDICINES"].ToString();
					p_objNuclearOrder.m_strRequesterSign=dtbValue.Rows[0]["REQUESTERSIGN"].ToString();
					p_objNuclearOrder.strStatus=int.Parse(dtbValue.Rows[0]["STATUS"].ToString());

				}		
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
     		    
	        finally
	        {
	          //objHRPServ.Dispose();

	        }
			return (long)enmOperationResult.DB_Succeed;

		}

		/// <summary>
		///更新记录。包含新增，修改，删除操作
		/// </summary>
		/// <param name="p_objNuclearOrder"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngUpdateNuclearOrder(clsNuclearOrder p_objNuclearOrder,enmUpdateAction p_enmAction)
		{
             clsHRPTableService objHRPServ = new clsHRPTableService() ;
			try
			{
				long lngAffectedRows=0;
				long lngRe=0;
				IDataParameter[] objDPArr;
               
				DataTable dtbValue = new DataTable();

				switch(p_enmAction)
				{
					case enmUpdateAction.enmAddNew:
					case enmUpdateAction.enmEdit:
 						objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(37, out objDPArr);

                        objDPArr[0].Value = p_objNuclearOrder.strInPatientID;
                        objDPArr[1].DbType = DbType.DateTime;
                        objDPArr[1].Value = DateTime.Parse(DateTime.Parse(p_objNuclearOrder.strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = DateTime.Parse(DateTime.Parse(p_objNuclearOrder.strCreateDate).ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[3].DbType = DbType.DateTime;
						objDPArr[3].Value=DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
						objDPArr[4].Value=p_objNuclearOrder.strCreateUserID  ;
						objDPArr[5].Value=p_objNuclearOrder.m_strTelephone  ;
                        objDPArr[6].Value = p_objNuclearOrder.m_strAddress;
                        objDPArr[7].DbType = DbType.DateTime;
                        objDPArr[7].Value = DateTime.Parse(DateTime.Parse(p_objNuclearOrder.m_strLastCheck).ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[8].DbType = DbType.DateTime;
                        objDPArr[8].Value = DateTime.Parse(DateTime.Parse(p_objNuclearOrder.m_strRequestDate).ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[9].DbType = DbType.DateTime;
						objDPArr[9].Value=DateTime.Parse(DateTime.Parse(p_objNuclearOrder.m_strBespeak).ToString("yyyy-MM-dd HH:mm:ss")) ;
						objDPArr[10].Value=p_objNuclearOrder.m_strNumber ;
						objDPArr[11].Value=p_objNuclearOrder.m_strMainDescription  ;
						objDPArr[12].Value=(p_objNuclearOrder.m_blnIsNoseSnore ? 1 : 0 );

					switch(p_objNuclearOrder.m_enmNoseSnoreLevel)
					{
						case enmLevel.enmNone :
							objDPArr[13].Value=0;
							break;
						case enmLevel.enmLitter :
							objDPArr[13].Value=1;
							break;
						case enmLevel.enmSome :
							objDPArr[13].Value=2;
							break;
						case enmLevel.enmMany :
							objDPArr[13].Value=3;
							break;
					}
						
						objDPArr[14].Value=p_objNuclearOrder.m_strNoseSnoreBeginYear  ;
						objDPArr[15].Value=p_objNuclearOrder.m_strNoseSnoreBeginMonth  ;
						objDPArr[16].Value=(p_objNuclearOrder.m_blnIsSnooze ? 1 : 0 );

					switch(p_objNuclearOrder.m_enmSnoozeLevel)
					{
						case enmLevel.enmNone :
							objDPArr[17].Value=0;
							break;
						case enmLevel.enmLitter :
							objDPArr[17].Value=1;
							break;
						case enmLevel.enmSome :
							objDPArr[17].Value=2;
							break;
						case enmLevel.enmMany :
							objDPArr[17].Value=3;
							break;
					}
						
						objDPArr[18].Value=p_objNuclearOrder.m_strSnoozeBeginYear  ;
						objDPArr[19].Value=p_objNuclearOrder.m_strSnoozeBeginMonth  ;
						objDPArr[20].Value=p_objNuclearOrder.m_strGoWithSymptom  ;
						objDPArr[21].Value=p_objNuclearOrder.m_strSleep ;
						objDPArr[22].Value=p_objNuclearOrder.m_strOther;
						objDPArr[23].Value=p_objNuclearOrder.m_strDiseaseHistory ;
						objDPArr[24].Value=p_objNuclearOrder.m_strIrritability  ;
						objDPArr[25].Value=p_objNuclearOrder.m_strHeight  ;
						objDPArr[26].Value=p_objNuclearOrder.m_strWeight ;
						objDPArr[27].Value=p_objNuclearOrder.m_strBloodPressure;
						objDPArr[28].Value=p_objNuclearOrder.m_strHeadNeck ;
						objDPArr[29].Value=p_objNuclearOrder.m_strHeadNeckOther ;
						objDPArr[30].Value=p_objNuclearOrder.m_strHeart;
						objDPArr[31].Value=p_objNuclearOrder.m_strLung  ;
						objDPArr[32].Value=p_objNuclearOrder.m_strOtherPart  ;
						objDPArr[33].Value=p_objNuclearOrder.m_strClinicalDiagnose  ;
						objDPArr[34].Value=p_objNuclearOrder.m_strLastMedicines  ;
						objDPArr[35].Value=p_objNuclearOrder.m_strRequesterSign  ;
						objDPArr[36].Value=p_objNuclearOrder.strStatus;

                        lngRe = objHRPServ.lngExecuteParameterSQL(m_InsertRecordSQL, ref lngAffectedRows, objDPArr);

						break;
					case enmUpdateAction.enmDelete:
 						objDPArr = null;
                        objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                        objDPArr[0].DbType = DbType.DateTime;
 						objDPArr[0].Value=p_objNuclearOrder.m_dtmDeActivedDate  ;
						objDPArr[1].Value=p_objNuclearOrder.m_strDeActivedOperatorID.Trim() ;
                        objDPArr[2].Value = p_objNuclearOrder.strInPatientID.Trim();
                        objDPArr[3].DbType = DbType.DateTime;
                        objDPArr[3].Value = DateTime.Parse(DateTime.Parse(p_objNuclearOrder.strCreateDate).ToString("yyyy-MM-dd HH:mm:ss"));
                        objDPArr[4].DbType = DbType.DateTime;
						objDPArr[4].Value=DateTime.Parse(DateTime.Parse(p_objNuclearOrder.strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss")) ;

                        lngRe = objHRPServ.lngExecuteParameterSQL(m_DeleteRecordSQL, ref lngAffectedRows, objDPArr);

						break;
				}
					
				
				if (lngRe>0 && lngAffectedRows>0)
				{
					return 1;
				}
				else
				{
					return 0;
				}

			}
		
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);	
				return 0;
			}
 		    
	        finally
	        {
	          //objHRPServ.Dispose();

	        }

		}

	}

}
