using System;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;
using weCare.Core.Entity;
using System.Data;
using com.digitalwave.Utility.SQLConvert;
using System.Collections;
using com.digitalwave.Utility;

namespace com.digitalwave.clsRecordsService
{
	/// <summary>
	/// 危重患者护理记录
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsIntensiveTendRecord_GXMainService : clsRecordsService
	{
		#region SQL语句
		private const string c_strUpdateFirstPrintDateSQL=@"update t_emr_intensivetendrecord_gx
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

        private const string c_strGetRecordContentSQL = @"select (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_intensivetendrecord_gx t
                 where e.empno_chr = t.createuserid
                   and e.status_int <> -1
                    and t.status = 0
                   and t.inpatientid = ?
                   and t.inpatientdate = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t1.createuserid
           and rownum = 1) createusername,
       t1.inpatientid,
       t1.inpatientdate,
       t1.opendate,
       t1.createdate,
       t1.createuserid,
       t1.ifconfirm,
       t1.confirmreason,
       t1.confirmreasonxml,
       t1.status,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.firstprintdate,
       t1.recorddate,
       t1.initem,
       t1.initemxml,
       t1.infact,
       t1.infactxml,
       t1.outpiss,
       t1.outpissxml,
       t1.outstool,
       t1.outstoolxml,
       t1.checkt,
       t1.checktxml,
       t1.checkp,
       t1.checkpxml,
       t1.checkr,
       t1.checkrxml,
       t1.checkbpa,
       t1.checkbpaxml,
       t1.checkbps,
       t1.checkbpsxml,
       t1.nursesignid,
       t1.diagnose,
       t1.custom1name,
       t1.custom2name,
       t1.custom3name,
       t1.custom4name,
       t1.custom1,
       t1.custom1xml,
       t1.custom2,
       t1.custom2xml,
       t1.custom3,
       t1.custom3xml,
       t1.custom4,
       t1.custom4xml,
       t1.stat_status,
       t1.isstat,
       t1.sumin,
       t1.sumout,
       t1.sumintime,
       t1.sumouttime,
       t1.autosumin,
       t1.autosumout,
       t1.startstattime,
       t3.modifydate,
       t3.modifyuserid,
       t3.initem_right,
       t3.infact_right,
       t3.outpiss_right,
       t3.outstool_right,
       t3.checkt_right,
       t3.checkp_right,
       t3.checkr_right,
       t3.checkbpa_right,
       t3.checkbps_right,
       t3.custom1_right,
       t3.custom2_right,
       t3.custom3_right,
       t3.custom4_right
  from t_emr_intensivetendrecord_gx t1, t_emr_intensivetendcontent_gx t3
 where t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
 order by t1.recorddate, t3.modifydate";

        private const string c_strGetRecordContentSQL_Single = @"select (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_intensivetendrecord_gx t
                 where e.empno_chr = t.createuserid
                   and e.status_int <> -1
                    and t.status = 0
                   and t.inpatientid = ?
                   and t.inpatientdate = ?
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t1.createuserid
           and rownum = 1) createusername,
       t1.inpatientid,
       t1.inpatientdate,
       t1.opendate,
       t1.createdate,
       t1.createuserid,
       t1.ifconfirm,
       t1.confirmreason,
       t1.confirmreasonxml,
       t1.status,
       t1.deactiveddate,
       t1.deactivedoperatorid,
       t1.firstprintdate,
       t1.recorddate,
       t1.initem,
       t1.initemxml,
       t1.infact,
       t1.infactxml,
       t1.outpiss,
       t1.outpissxml,
       t1.outstool,
       t1.outstoolxml,
       t1.checkt,
       t1.checktxml,
       t1.checkp,
       t1.checkpxml,
       t1.checkr,
       t1.checkrxml,
       t1.checkbpa,
       t1.checkbpaxml,
       t1.checkbps,
       t1.checkbpsxml,
       t1.nursesignid,
       t1.diagnose,
       t1.custom1name,
       t1.custom2name,
       t1.custom3name,
       t1.custom4name,
       t1.custom1,
       t1.custom1xml,
       t1.custom2,
       t1.custom2xml,
       t1.custom3,
       t1.custom3xml,
       t1.custom4,
       t1.custom4xml,
       t1.stat_status,
       t1.isstat,
       t1.sumin,
       t1.sumout,
       t1.sumintime,
       t1.sumouttime,
       t1.autosumin,
       t1.autosumout,
       t1.startstattime,
       t3.modifydate,
       t3.modifyuserid,
       t3.initem_right,
       t3.infact_right,
       t3.outpiss_right,
       t3.outstool_right,
       t3.checkt_right,
       t3.checkp_right,
       t3.checkr_right,
       t3.checkbpa_right,
       t3.checkbps_right,
       t3.custom1_right,
       t3.custom2_right,
       t3.custom3_right,
       t3.custom4_right
  from t_emr_intensivetendrecord_gx t1, t_emr_intensivetendcontent_gx t3
 where t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = 0
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
 order by t1.recorddate, t3.modifydate";

		private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
															from t_emr_intensivetendrecord_gx
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

		private const string c_strDeleteRecordSQL=@"update t_emr_intensivetendrecord_gx
														set status = 1, deactiveddate = ?, deactivedoperatorid = ?
													where inpatientid = ?
														and inpatientdate = ?
														and opendate = ?
														and status = 0";

        private const string c_strGetDetailSQL = @"select t.inpatientid,
       t.inpatientdate,
       t.opendate,
       t.createdate,
       t.createuserid,
       t.modifyuserid,
       t.modifydate,
       t.detailrecorddate,
       t.detailcontent,
       t.detailcontentxml,
       t.detailsignid,
       t.status,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.stat_status,
       (select lastname_vchr
          from (select e.lastname_vchr, e.empid_chr, e.isemployee_int, e.empno_chr
                  from t_bse_employee e,t_emr_intensivetenddetail_gx i
                 where e.empno_chr = i.detailsignid
                   and e.status_int <> -1
                 order by e.isemployee_int desc, e.empid_chr desc)
         where empno_chr = t.detailsignid
           and rownum = 1) createusername
  from t_emr_intensivetenddetail_gx t
 where t.status = 0
   and inpatientid = ?
   and t.inpatientdate = ?
 order by t.detailrecorddate";
		#endregion
		/// <summary>
		/// 更新数据库中的首次打印时间。
		/// </summary>
		/// <param name="p_strInPatientID">住院号</param>
		/// <param name="p_strInPatientDate">入院日期</param>
		/// <param name="p_intRecordTypeArr">记录类型</param>
		/// <param name="p_dtmOpenDateArr">记录时间(与记录类型及其位置一一对应)</param>
		/// <param name="p_dtmFirstPrintDate">首次打印时间</param>
		/// <returns></returns>
		[AutoComplete]
		public override long m_lngUpdateFirstPrintDate(
			string p_strInPatientID,
			string p_strInPatientDate,
			int[] p_intRecordTypeArr,
			DateTime[] p_dtmOpenDateArr,
			DateTime p_dtmFirstPrintDate)
		{
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsIntensiveTendRecord_GXMainService","m_lngUpdateFirstPrintDate");
            ////if(lngCheckRes <= 0)
            //    //return lngCheckRes;				

			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""||p_strInPatientDate==null||p_strInPatientDate==""||
				p_dtmOpenDateArr==null||p_dtmFirstPrintDate==DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
			clsHRPTableService objHRPServ =new clsHRPTableService();
			//获取IDataParameter数组
			IDataParameter[] objDPArr = null;
			for(int i=0; i<p_dtmOpenDateArr.Length; i++)
			{
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strInPatientID;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[3].DbType = DbType.DateTime;
				objDPArr[3].Value=p_dtmOpenDateArr[i];
				//执行SQL
				long lngRes=0;				
				long lngEff=0;
				lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL,ref lngEff,objDPArr);
                if (lngRes <= 0)
                {
                    //objHRPServ.Dispose();
                    return lngRes;
                }
			}
            //objHRPServ.Dispose();
			return (long)enmOperationResult.DB_Succeed;
		}

		/// <summary>
		/// 修改或添加一条记录时读数据库
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strRecordOpenDate"></param>
		/// <param name="p_objTansDataInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecordContent(string p_strInPatientID,
			string p_strInPatientDate,
			string p_strRecordOpenDate,
			out clsIntensiveTendRecord_GX[] p_objTansDataInfo)
		{
			p_objTansDataInfo=null;
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
				clsHRPTableService objHRPServ =new clsHRPTableService();

			try
			{
				ArrayList arlTransData = new ArrayList();  
				ArrayList arlModifyData = new ArrayList();
				DateTime dtmOpenDate;

				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
				objHRPServ.CreateDatabaseParameter(5,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);
                objDPArr[4].DbType = DbType.DateTime;		
				objDPArr[4].Value=DateTime.Parse(p_strRecordOpenDate);
					
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable       
				lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Single,ref dtbValue,objDPArr);
				//循环DataTable
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					p_objTansDataInfo = new clsIntensiveTendRecord_GX[dtbValue.Rows.Count];
					clsIntensiveTendRecord_GX objRecordContent= null;
                    DataRow objRow = null;
					for(int i=0;i<dtbValue.Rows.Count;i++)
					{
                        objRow = dtbValue.Rows[i];
						objRecordContent = new clsIntensiveTendRecord_GX();
						#region 从DataTable.Rows中获取结果
						dtmOpenDate = Convert.ToDateTime(objRow["OPENDATE"]).Date;
						while(i<dtbValue.Rows.Count && Convert.ToDateTime(objRow["OPENDATE"]).Date == dtmOpenDate)
						{
							objRecordContent.m_strInPatientID = p_strInPatientID;
							objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
							objRecordContent.m_dtmOpenDate = Convert.ToDateTime(objRow["OPENDATE"]);
							objRecordContent.m_dtmCreateDate = Convert.ToDateTime(objRow["CREATEDATE"]);
							objRecordContent.m_strCreateUserID = objRow["CREATEUSERID"].ToString();
							objRecordContent.m_strConfirmReason = objRow["CONFIRMREASON"].ToString();
							objRecordContent.m_strConfirmReasonXML = objRow["CONFIRMREASONXML"].ToString();
							if(objRow["FIRSTPRINTDATE"].ToString()=="")
								objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
							else objRecordContent.m_dtmFirstPrintDate=Convert.ToDateTime(objRow["FIRSTPRINTDATE"]);
							objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(objRow["RECORDDATE"]);
							objRecordContent.m_strINITEM = objRow["INITEM"].ToString();
							objRecordContent.m_strINITEMXML = objRow["INITEMXML"].ToString();
							objRecordContent.m_strINFACT = objRow["INFACT"].ToString();
							objRecordContent.m_strINFACTXML = objRow["INFACTXML"].ToString();
							objRecordContent.m_strOUTPISS = objRow["OUTPISS"].ToString();
							objRecordContent.m_strOUTPISSXML = objRow["OUTPISSXML"].ToString();
							objRecordContent.m_strOUTSTOOL = objRow["OUTSTOOL"].ToString();
							objRecordContent.m_strOUTSTOOLXML = objRow["OUTSTOOLXML"].ToString();
							objRecordContent.m_strCHECKT = objRow["CHECKT"].ToString();
							objRecordContent.m_strCHECKTXML = objRow["CHECKTXML"].ToString();
							objRecordContent.m_strCHECKP = objRow["CHECKP"].ToString();
							objRecordContent.m_strCHECKPXML = objRow["CHECKPXML"].ToString();
							objRecordContent.m_strCHECKR = objRow["CHECKR"].ToString();
							objRecordContent.m_strCHECKRXML = objRow["CHECKRXML"].ToString();
							objRecordContent.m_strCHECKBPA = objRow["CHECKBPA"].ToString();
							objRecordContent.m_strCHECKBPAXML = objRow["CHECKBPAXML"].ToString();
							objRecordContent.m_strCHECKBPS = objRow["CHECKBPS"].ToString();
							objRecordContent.m_strCHECKBPSXML = objRow["CHECKBPSXML"].ToString();
							objRecordContent.m_strNURSESIGNID = objRow["NURSESIGNID"].ToString();
                            objRecordContent.m_strNURSESIGNNAME = objRow["CreateUserName"].ToString();
							objRecordContent.m_strDIAGNOSE = objRow["DIAGNOSE"].ToString();
							objRecordContent.m_strCUSTOM1 = objRow["CUSTOM1"].ToString();
							objRecordContent.m_strCUSTOM1XML = objRow["CUSTOM1XML"].ToString();
							objRecordContent.m_strCUSTOM2 = objRow["CUSTOM2"].ToString();
							objRecordContent.m_strCUSTOM2XML = objRow["CUSTOM2XML"].ToString();
							objRecordContent.m_strCUSTOM3 = objRow["CUSTOM3"].ToString();
							objRecordContent.m_strCUSTOM3XML = objRow["CUSTOM3XML"].ToString();
							objRecordContent.m_strCUSTOM4 = objRow["CUSTOM4"].ToString();
							objRecordContent.m_strCUSTOM4XML = objRow["CUSTOM4XML"].ToString();
							objRecordContent.m_intSTAT_STATUS = objRow["STAT_STATUS"]==DBNull.Value?0:Convert.ToInt32(objRow["STAT_STATUS"]);
							objRecordContent.m_strCUSTOM1NAME = objRow["CUSTOM1NAME"].ToString();
							objRecordContent.m_strCUSTOM2NAME = objRow["CUSTOM2NAME"].ToString();
							objRecordContent.m_strCUSTOM3NAME = objRow["CUSTOM3NAME"].ToString();
							objRecordContent.m_strCUSTOM4NAME = objRow["CUSTOM4NAME"].ToString();
							objRecordContent.m_intISSTAT = objRow["ISSTAT"] == DBNull.Value ? -1:Convert.ToInt32(objRow["ISSTAT"]);
							objRecordContent.m_intSUMINTIME = objRow["SUMINTIME"] == DBNull.Value ? -1:Convert.ToInt32(objRow["SUMINTIME"]);
							objRecordContent.m_intSUMOUTTIME = objRow["SUMOUTTIME"] == DBNull.Value ? -1:Convert.ToInt32(objRow["SUMOUTTIME"]);
							objRecordContent.m_strAUTOSUMIN = objRow["AUTOSUMIN"].ToString();
							objRecordContent.m_strAUTOSUMOUT = objRow["AUTOSUMOUT"].ToString();
							objRecordContent.m_strSUMIN = objRow["SUMIN"].ToString();
							objRecordContent.m_strSUMOUT = objRow["SUMOUT"].ToString();
							objRecordContent.m_dtmSTARTSTATTIME = objRow["STARTSTATTIME"]==DBNull.Value ? DateTime.MinValue:Convert.ToDateTime(objRow["STARTSTATTIME"]);

							objRecordContent.m_dtmModifyDate = Convert.ToDateTime(objRow["MODIFYDATE"]);
							objRecordContent.m_strModifyUserID = objRow["MODIFYUSERID"].ToString();
							objRecordContent.m_strINITEM_RIGHT = objRow["INITEM_RIGHT"].ToString();
							objRecordContent.m_strINFACT_RIGHT = objRow["INFACT_RIGHT"].ToString();
							objRecordContent.m_strOUTPISS_RIGHT = objRow["OUTPISS_RIGHT"].ToString();
							objRecordContent.m_strOUTSTOOL_RIGHT = objRow["OUTSTOOL_RIGHT"].ToString();
							objRecordContent.m_strCHECKT_RIGHT = objRow["CHECKT_RIGHT"].ToString();
							objRecordContent.m_strCHECKP_RIGHT = objRow["CHECKP_RIGHT"].ToString();
							objRecordContent.m_strCHECKR_RIGHT = objRow["CHECKR_RIGHT"].ToString();
							objRecordContent.m_strCHECKBPA_RIGHT = objRow["CHECKBPA_RIGHT"].ToString();
							objRecordContent.m_strCHECKBPS_RIGHT = objRow["CHECKBPS_RIGHT"].ToString();
							objRecordContent.m_strCUSTOM4_RIGHT = objRow["CUSTOM4_RIGHT"].ToString();
							objRecordContent.m_strCUSTOM3_RIGHT = objRow["CUSTOM3_RIGHT"].ToString();
							objRecordContent.m_strCUSTOM2_RIGHT = objRow["CUSTOM2_RIGHT"].ToString();
							objRecordContent.m_strCUSTOM1_RIGHT = objRow["CUSTOM1_RIGHT"].ToString();
						}
						p_objTansDataInfo[i] = objRecordContent;
						#endregion
					}
				}		
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
				return 0;
			}
                finally
                {

                    //objHRPServ.Dispose();
                }
			return (long)enmOperationResult.DB_Succeed;
		}

		/// <summary>
		/// 获取指定记录的内容。
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objGeneralNurseRecordArr"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngGetTransDataInfoArrWithServ(string p_strInPatientID,
			string p_strInPatientDate,
			clsHRPTableService p_objHRPServ,
			out clsTransDataInfo[] p_objIntensiveTendInfoArr)
		{
			clsIntensiveTendRecord_GX[] p_objIntensiveTendRecordArr = null;
			clsIntensiveTendRecordDetail_GX[] p_objIntensiveTendDetailArr = null;
			p_objIntensiveTendInfoArr = new clsTransDataInfo[1];
			long lngRes = -1;
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

			clsIntensiveTendRecord_GXDataInfo objDataInfo = new clsIntensiveTendRecord_GXDataInfo();

			try
			{
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(4,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
                objDPArr[2].Value = p_strInPatientID.Trim();
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = DateTime.Parse(p_strInPatientDate);

				DataTable dtbValue = new DataTable();//护理记录内容  
				DataTable dtbDetail = new DataTable();//病情记录内容

				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);
				if(lngRes > 0 && dtbValue.Rows.Count > 0)
				{
                    DataRow objRow = null;
					clsIntensiveTendRecord_GX objRecordContent = null;
					p_objIntensiveTendRecordArr = new clsIntensiveTendRecord_GX[dtbValue.Rows.Count];
					for(int i=0; i<dtbValue.Rows.Count; i++)
					{
                        objRow = dtbValue.Rows[i];
						objRecordContent = new clsIntensiveTendRecord_GX();
						objRecordContent.m_strInPatientID = p_strInPatientID;
						objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
						objRecordContent.m_dtmOpenDate = Convert.ToDateTime(objRow["OPENDATE"]);
						objRecordContent.m_dtmCreateDate = Convert.ToDateTime(objRow["CREATEDATE"]);
						objRecordContent.m_strCreateUserID = objRow["CREATEUSERID"].ToString();
						if(objRow["FIRSTPRINTDATE"].ToString()=="")
							objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
						else objRecordContent.m_dtmFirstPrintDate=Convert.ToDateTime(objRow["FIRSTPRINTDATE"]);
						if(objRow["IFCONFIRM"].ToString()=="")
							objRecordContent.m_bytIfConfirm=0;
						else objRecordContent.m_bytIfConfirm=Byte.Parse(objRow["IFCONFIRM"].ToString());
						if(objRow["STATUS"].ToString()=="")
							objRecordContent.m_bytStatus=0;
						else objRecordContent.m_bytStatus=Byte.Parse(objRow["STATUS"].ToString());

						objRecordContent.m_strConfirmReason=objRow["CONFIRMREASON"].ToString();
						objRecordContent.m_strConfirmReasonXML=objRow["CONFIRMREASONXML"].ToString();

						objRecordContent.m_dtmRECORDDATE = Convert.ToDateTime(objRow["RECORDDATE"]);
						objRecordContent.m_strINITEM = objRow["INITEM"].ToString();
						objRecordContent.m_strINITEMXML = objRow["INITEMXML"].ToString();
						objRecordContent.m_strINFACT = objRow["INFACT"].ToString();
						objRecordContent.m_strINFACTXML = objRow["INFACTXML"].ToString();
						objRecordContent.m_strOUTPISS = objRow["OUTPISS"].ToString();
						objRecordContent.m_strOUTPISSXML = objRow["OUTPISSXML"].ToString();
						objRecordContent.m_strOUTSTOOL = objRow["OUTSTOOL"].ToString();
						objRecordContent.m_strOUTSTOOLXML = objRow["OUTSTOOLXML"].ToString();
						objRecordContent.m_strCHECKT = objRow["CHECKT"].ToString();
						objRecordContent.m_strCHECKTXML = objRow["CHECKTXML"].ToString();
						objRecordContent.m_strCHECKP = objRow["CHECKP"].ToString();
						objRecordContent.m_strCHECKPXML = objRow["CHECKPXML"].ToString();
						objRecordContent.m_strCHECKR = objRow["CHECKR"].ToString();
						objRecordContent.m_strCHECKRXML = objRow["CHECKRXML"].ToString();
						objRecordContent.m_strCHECKBPA = objRow["CHECKBPA"].ToString();
						objRecordContent.m_strCHECKBPAXML = objRow["CHECKBPAXML"].ToString();
						objRecordContent.m_strCHECKBPS = objRow["CHECKBPS"].ToString();
						objRecordContent.m_strCHECKBPSXML = objRow["CHECKBPSXML"].ToString();
						objRecordContent.m_strNURSESIGNID = objRow["NURSESIGNID"].ToString();
                        objRecordContent.m_strNURSESIGNNAME = objRow["CreateUserName"].ToString();
						objRecordContent.m_strDIAGNOSE = objRow["DIAGNOSE"].ToString();
						objRecordContent.m_strCUSTOM1 = objRow["CUSTOM1"].ToString();
						objRecordContent.m_strCUSTOM1XML = objRow["CUSTOM1XML"].ToString();
						objRecordContent.m_strCUSTOM2 = objRow["CUSTOM2"].ToString();
						objRecordContent.m_strCUSTOM2XML = objRow["CUSTOM2XML"].ToString();
						objRecordContent.m_strCUSTOM3 = objRow["CUSTOM3"].ToString();
						objRecordContent.m_strCUSTOM3XML = objRow["CUSTOM3XML"].ToString();
						objRecordContent.m_strCUSTOM4 = objRow["CUSTOM4"].ToString();
						objRecordContent.m_strCUSTOM4XML = objRow["CUSTOM4XML"].ToString();
						objRecordContent.m_intSTAT_STATUS = objRow["STAT_STATUS"]==DBNull.Value?0:Convert.ToInt32(objRow["STAT_STATUS"]);
						objRecordContent.m_strCUSTOM1NAME = objRow["CUSTOM1NAME"].ToString();
						objRecordContent.m_strCUSTOM2NAME = objRow["CUSTOM2NAME"].ToString();
						objRecordContent.m_strCUSTOM3NAME = objRow["CUSTOM3NAME"].ToString();
						objRecordContent.m_strCUSTOM4NAME = objRow["CUSTOM4NAME"].ToString();
						objRecordContent.m_intISSTAT = objRow["ISSTAT"] == DBNull.Value ? -1:Convert.ToInt32(objRow["ISSTAT"]);
						objRecordContent.m_intSUMINTIME = objRow["SUMINTIME"] == DBNull.Value ? -1:Convert.ToInt32(objRow["SUMINTIME"]);
						objRecordContent.m_intSUMOUTTIME = objRow["SUMOUTTIME"] == DBNull.Value ? -1:Convert.ToInt32(objRow["SUMOUTTIME"]);
						objRecordContent.m_strAUTOSUMIN = objRow["AUTOSUMIN"].ToString();
						objRecordContent.m_strAUTOSUMOUT = objRow["AUTOSUMOUT"].ToString();
						objRecordContent.m_strSUMIN = objRow["SUMIN"].ToString();
						objRecordContent.m_strSUMOUT = objRow["SUMOUT"].ToString();
						objRecordContent.m_dtmSTARTSTATTIME = objRow["STARTSTATTIME"]==DBNull.Value ? DateTime.MinValue:Convert.ToDateTime(objRow["STARTSTATTIME"]);

						objRecordContent.m_dtmModifyDate = Convert.ToDateTime(objRow["MODIFYDATE"]);
						objRecordContent.m_strModifyUserID = objRow["MODIFYUSERID"].ToString();
						objRecordContent.m_strINITEM_RIGHT = objRow["INITEM_RIGHT"].ToString();
						objRecordContent.m_strINFACT_RIGHT = objRow["INFACT_RIGHT"].ToString();
						objRecordContent.m_strOUTPISS_RIGHT = objRow["OUTPISS_RIGHT"].ToString();
						objRecordContent.m_strOUTSTOOL_RIGHT = objRow["OUTSTOOL_RIGHT"].ToString();
						objRecordContent.m_strCHECKT_RIGHT = objRow["CHECKT_RIGHT"].ToString();
						objRecordContent.m_strCHECKP_RIGHT = objRow["CHECKP_RIGHT"].ToString();
						objRecordContent.m_strCHECKR_RIGHT = objRow["CHECKR_RIGHT"].ToString();
						objRecordContent.m_strCHECKBPA_RIGHT = objRow["CHECKBPA_RIGHT"].ToString();
						objRecordContent.m_strCHECKBPS_RIGHT = objRow["CHECKBPS_RIGHT"].ToString();
						objRecordContent.m_strCUSTOM4_RIGHT = objRow["CUSTOM4_RIGHT"].ToString();
						objRecordContent.m_strCUSTOM3_RIGHT = objRow["CUSTOM3_RIGHT"].ToString();
						objRecordContent.m_strCUSTOM2_RIGHT = objRow["CUSTOM2_RIGHT"].ToString();
						objRecordContent.m_strCUSTOM1_RIGHT = objRow["CUSTOM1_RIGHT"].ToString();

						p_objIntensiveTendRecordArr[i] = objRecordContent;
					}
					
					objDataInfo.m_objRecordContent = p_objIntensiveTendRecordArr[p_objIntensiveTendRecordArr.Length-1];
				}
				objDataInfo.m_objRecordArr = p_objIntensiveTendRecordArr;

				IDataParameter[] objDPArr1 = null;
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr1);
				objDPArr1[0].Value=p_strInPatientID.Trim();
                objDPArr1[1].DbType = DbType.DateTime;
				objDPArr1[1].Value=DateTime.Parse(p_strInPatientDate);

				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDetailSQL, ref dtbDetail, objDPArr1);

				if(lngRes > 0 && dtbDetail.Rows.Count > 0)
                {
                    DataRow objRow = null;
					clsIntensiveTendRecordDetail_GX objDetail = null;
					p_objIntensiveTendDetailArr = new clsIntensiveTendRecordDetail_GX[dtbDetail.Rows.Count];
					for(int j=0; j<dtbDetail.Rows.Count; j++)
					{
                        objRow = dtbDetail.Rows[j];
						objDetail = new clsIntensiveTendRecordDetail_GX();

						objDetail.m_strInPatientID = p_strInPatientID;
						objDetail.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
						objDetail.m_dtmOpenDate = Convert.ToDateTime(objRow["OPENDATE"]);
						objDetail.m_dtmCreateDate = Convert.ToDateTime(objRow["CREATEDATE"]);
						objDetail.m_strCreateUserID = objRow["CREATEUSERID"].ToString();
						objDetail.m_strModifyUserID = objRow["MODIFYUSERID"].ToString();
						objDetail.m_dtmModifyDate = Convert.ToDateTime(objRow["MODIFYDATE"]);
						objDetail.m_dtmDETAILRECORDDATE = Convert.ToDateTime(objRow["DETAILRECORDDATE"]);
						objDetail.m_strDETAILCONTENT = objRow["DETAILCONTENT"].ToString();
						objDetail.m_strDETAILCONTENTXML = objRow["DETAILCONTENTXML"].ToString();
						objDetail.m_strDETAILSIGNID = objRow["DETAILSIGNID"].ToString();
						objDetail.m_strDETAILSIGNNAME = objRow["CreateUserName"].ToString();
						if(objRow["STATUS"].ToString()=="")
							objDetail.m_bytStatus=0;
						else objDetail.m_bytStatus=Byte.Parse(objRow["STATUS"].ToString());
						objDetail.m_intSTAT_STATUS = objRow["STAT_STATUS"]==DBNull.Value ? 0:Convert.ToInt32(objRow["STAT_STATUS"]);

						p_objIntensiveTendDetailArr[j] = objDetail;
					}
				}
				objDataInfo.m_objDetailArr = p_objIntensiveTendDetailArr;
//				objDataInfo.m_intFlag = (int)enmRecordsType.IntensiveTendRecord_GX;
				
				if(objDataInfo.m_objRecordArr == null)
				{
					objDataInfo.m_objRecordContent = new clsIntensiveTendRecord_GX();
					objDataInfo.m_objRecordContent.m_dtmFirstPrintDate = DateTime.MinValue;
				}
				p_objIntensiveTendInfoArr[0] = objDataInfo;
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
				return 0;
			}
			return (long)enmOperationResult.DB_Succeed;
		}

		/// <summary>
		/// 查看当前记录是否最新的记录。
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objModifyInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngCheckLastModifyRecord(int p_intRecordType,
			clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ,
			out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo=null;
			long lngRes = 0;

			//检查参数          
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;

			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from 
											t_emr_intensivetendrecord_gx t1,t_emr_intensivetendcontent_gx t2
											where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
											and t1.opendate = t2.opendate and t1.status = 0
											and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc"+clsDatabaseSQLConvert.s_StrRownum;

			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=p_objRecordContent.m_dtmOpenDate;
				//使用strSQL生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable            
				lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL,ref dtbValue,objDPArr);
			   
				//如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
				if(lngRes > 0 && dtbValue.Rows.Count ==0)
				{
					p_objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                    objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
                    objDPArr[2].DbType = DbType.DateTime;
					objDPArr[2].Value=p_objRecordContent.m_dtmOpenDate;

					lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL,ref dtbValue,objDPArr);
				
					if(lngRes>0 && dtbValue.Rows.Count ==1)
					{
						p_objModifyInfo=new clsPreModifyInfo();
						p_objModifyInfo.m_strActionUserID=dtbValue.Rows[0]["DEACTIVEDOPERATORID"].ToString();
						p_objModifyInfo.m_dtmActionTime=DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE"].ToString());
					}
					return (long)enmOperationResult.Record_Already_Delete;
				}	
					//从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
				else if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					//如果相同，返回DB_Succees
                    //if(p_objRecordContent.m_dtmModifyDate==DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
						return (long)enmOperationResult.DB_Succeed;

					//否则，返回Record_Already_Modify
                    //p_objModifyInfo=new clsPreModifyInfo();
                    //p_objModifyInfo.m_strActionUserID=dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    //p_objModifyInfo.m_dtmActionTime=DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                    //return (long)enmOperationResult.Record_Already_Modify;
				}
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;	
			
		}

		/// <summary>
		/// 把记录从数据中“删除”。
		/// </summary>
		/// <param name="p_intRecordType"></param>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		protected override long m_lngDeleteRecord2DB(int p_intRecordType,
			clsTrackRecordContent p_objRecordContent,
			clsHRPTableService p_objHRPServ)
		{
			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
			try
			{
				//获取IDataParameter数组
				IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
				objDPArr[0].Value=p_objRecordContent.m_dtmDeActivedDate;
				objDPArr[1].Value=p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strInPatientID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmInPatientDate;
                objDPArr[4].DbType = DbType.DateTime;
				objDPArr[4].Value=p_objRecordContent.m_dtmOpenDate;

				//执行SQL
				long lngEff=0;
				lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL,ref lngEff,objDPArr);
			}
			catch(Exception objEx)
			{
				string strTmp=objEx.Message;
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
			}

			return lngRes;
		}
	}
}
