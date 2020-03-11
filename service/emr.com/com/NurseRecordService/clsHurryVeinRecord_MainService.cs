
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
	/// 催产素脉点滴观察表
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsHurryVeinRecord_MainService : clsRecordsService
	{
		#region constructor
		public clsHurryVeinRecord_MainService()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#endregion

		#region SQL语句
		private const string c_strUpdateFirstPrintDateSQL=@"update icuacad_hurryveinrecord
																set firstprintdate = ?
															where inpatientid = ?
																and inpatientdate = ?
																and opendate = ?
																and firstprintdate is null
																and status = 0";

        private const string c_strGetRecordContentSQL = @"select distinct f_getempnamebyno(t1.createuserid) as createusername,
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
                t1.chroma_chr,
                t1.chroma_chrxml,
                t1.dropcount_chr,
                t1.dropcount_chrxml,
                t1.palaceshrink_chr,
                t1.palaceshrink_chrxml,
                t1.embryoheart_chr,
                t1.embryoheart_chrxml,
                t1.expand_chr,
                t1.expand_chrxml,
                t1.presentation_chr,
                t1.presentation_chrxml,
                t1.bloodpressure_chr,
                t1.bloodpressure_chrxml,
                t1.specialrecord_chr,
                t1.specialrecord_chrxml,
                t1.signature_chr,
                t1.signature_chrxml,
                t1.laycount_chr,
                t1.laycount_chrxml,
                t1.pregnantweek_chr,
                t1.pregnantweek_chrxml,
                t1.scorecount_chr,
                t1.scorecount_chrxml,
                t1.rdbneckexpand_chr,
                t1.rdbneckexpand_chrxml,
                t1.rdbneckshink_chr,
                t1.rdbneckshink_chrxml,
                t1.rdbhighlow_chr,
                t1.rdbhighlow_chrxml,
                t1.rdbneckhard_chr,
                t1.rdbneckhard_chrxml,
                t1.droppingcase_chr,
                t1.droppingcase_chrxml,
                t1.indicate_chr,
                t1.indicate_chrxml,
                t1.usecount_chr,
                t1.usecount_chrxml,
                t1.layway_chr,
                t1.layway_chrxml,
                t1.rdbnecklocation_chr,
                t1.rdbnecklocation_chrxml,
                t3.modifydate,
                t3.modifyuserid,
                t3.chroma_chr_right,
                t3.dropcount_chr_right,
                t3.palaceshrink_chr_right,
                t3.embryoheart_chr_right,
                t3.expand_chr_right,
                t3.presentation_chr_right,
                t3.bloodpressure_chr_right,
                t3.specialrecord_chr_right,
                t3.signature_chr_right
  from icuacad_hurryveinrecord t1, icuacad_hurryveincontent t3
 where t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = '0'
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
 order by t1.createdate";

        private const string c_strGetRecordContentSQL_Single = @"select distinct f_getempnamebyno(t1.createuserid) as createusername,
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
                t1.chroma_chr,
                t1.chroma_chrxml,
                t1.dropcount_chr,
                t1.dropcount_chrxml,
                t1.palaceshrink_chr,
                t1.palaceshrink_chrxml,
                t1.embryoheart_chr,
                t1.embryoheart_chrxml,
                t1.expand_chr,
                t1.expand_chrxml,
                t1.presentation_chr,
                t1.presentation_chrxml,
                t1.bloodpressure_chr,
                t1.bloodpressure_chrxml,
                t1.specialrecord_chr,
                t1.specialrecord_chrxml,
                t1.signature_chr,
                t1.signature_chrxml,
                t1.laycount_chr,
                t1.laycount_chrxml,
                t1.pregnantweek_chr,
                t1.pregnantweek_chrxml,
                t1.scorecount_chr,
                t1.scorecount_chrxml,
                t1.rdbneckexpand_chr,
                t1.rdbneckexpand_chrxml,
                t1.rdbneckshink_chr,
                t1.rdbneckshink_chrxml,
                t1.rdbhighlow_chr,
                t1.rdbhighlow_chrxml,
                t1.rdbneckhard_chr,
                t1.rdbneckhard_chrxml,
                t1.droppingcase_chr,
                t1.droppingcase_chrxml,
                t1.indicate_chr,
                t1.indicate_chrxml,
                t1.usecount_chr,
                t1.usecount_chrxml,
                t1.layway_chr,
                t1.layway_chrxml,
                t1.rdbnecklocation_chr,
                t1.rdbnecklocation_chrxml,
                t3.modifydate,
                t3.modifyuserid,
                t3.chroma_chr_right,
                t3.dropcount_chr_right,
                t3.palaceshrink_chr_right,
                t3.embryoheart_chr_right,
                t3.expand_chr_right,
                t3.presentation_chr_right,
                t3.bloodpressure_chr_right,
                t3.specialrecord_chr_right,
                t3.signature_chr_right
  from icuacad_hurryveinrecord t1, icuacad_hurryveincontent t3
 where t1.inpatientid = t3.inpatientid
   and t1.inpatientdate = t3.inpatientdate
   and t1.opendate = t3.opendate
   and t1.status = '0'
   and t1.inpatientid = ?
   and t1.inpatientdate = ?
   and t1.opendate = ?
 order by t1.createdate";

		private const string c_strGetDeleteRecordSQL = @"select deactiveddate, deactivedoperatorid
															from icuacad_hurryveinrecord
														where inpatientid = ?
															and inpatientdate = ?
															and opendate = ?
															and status = 1";

		private const string c_strDeleteRecordSQL=@"update icuacad_hurryveinrecord
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
       t.recordcontent,
       t.recordcontentxml,
       t.status,
       t.recordcontent_right,
       t.recorddate,
       t.modifydate,
       t.deactiveddate,
       t.deactivedoperatorid,
       t.class,
       f_getempnamebyno(t.createuserid) as lastname_vchr
  from generalnurserecord_gxdetail t
 where t.status = 0
   and inpatientid = ?
   and t.inpatientdate = ?
 order by t.recorddate";
		#endregion
        
		#region 更新数据库中的首次打印时间
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
            ////long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsHurryVeinRecord_MainService","m_lngUpdateFirstPrintDate");
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
		#endregion 

		#region 修改或添加一条记录时读数据库
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
			out clsHurryVeinRecord[] p_objTansDataInfo)
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
				objHRPServ.CreateDatabaseParameter(3,out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);
                objDPArr[2].DbType = DbType.DateTime;
				objDPArr[2].Value=Convert.ToDateTime(p_strRecordOpenDate);
					
				//生成DataTable
				DataTable dtbValue = new DataTable();
				//执行查询，填充结果到DataTable       
				lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Single,ref dtbValue,objDPArr);
				//循环DataTable
				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					p_objTansDataInfo = new clsHurryVeinRecord[dtbValue.Rows.Count];
					clsHurryVeinRecord objRecordContent= null;
				
					for(int j=0;j<dtbValue.Rows.Count;j++)
					{
						//获取当前DataTable记录的OpenDate，记录在dtmOpenDate
						dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["OPENDATE"].ToString()).Date;
						while(j<dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["OPENDATE"].ToString()).Date == dtmOpenDate)
						{
							#region 从DataTable.Rows中获取结果    
						
							objRecordContent=new clsHurryVeinRecord();
							objRecordContent.m_strInPatientID=p_strInPatientID;
							objRecordContent.m_dtmInPatientDate=DateTime.Parse(p_strInPatientDate);
							objRecordContent.m_dtmOpenDate=DateTime.Parse(dtbValue.Rows[j]["OPENDATE"].ToString());
							objRecordContent.m_dtmCreateDate=DateTime.Parse(dtbValue.Rows[j]["CREATEDATE"].ToString());
							objRecordContent.m_dtmModifyDate=DateTime.Parse(dtbValue.Rows[j]["MODIFYDATE"].ToString());
				
							if(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString()=="")
								objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
							else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbValue.Rows[j]["FIRSTPRINTDATE"].ToString());
							objRecordContent.m_strCreateUserID=dtbValue.Rows[j]["CREATEUSERID"].ToString();
							//							objRecordContent.m_strContentCreateUserName = dtbValue.Rows[j]["CreateUserName"].ToString();
							objRecordContent.m_strModifyUserID=dtbValue.Rows[j]["MODIFYUSERID"].ToString();
							objRecordContent.m_strModifyUserName=dtbValue.Rows[j]["MODIFYUSERNAME"].ToString();
							if(dtbValue.Rows[j]["IFCONFIRM"].ToString()=="")
								objRecordContent.m_bytIfConfirm=0;
							else objRecordContent.m_bytIfConfirm=Byte.Parse(dtbValue.Rows[j]["IFCONFIRM"].ToString());
							if(dtbValue.Rows[j]["STATUS"].ToString()=="")
								objRecordContent.m_bytStatus=0;
							else objRecordContent.m_bytStatus=Byte.Parse(dtbValue.Rows[j]["STATUS"].ToString());

							objRecordContent.m_strConfirmReason=dtbValue.Rows[j]["CONFIRMREASON"].ToString();
							objRecordContent.m_strConfirmReasonXML=dtbValue.Rows[j]["CONFIRMREASONXML"].ToString();
				
							objRecordContent.m_strCHROMA_CHR=dtbValue.Rows[j]["CHROMA_CHR"].ToString();
							objRecordContent.m_strCHROMA_CHR_RIGHT=dtbValue.Rows[j]["CHROMA_CHR_RIGHT"].ToString();
							objRecordContent.m_strCHROMA_CHRXML=dtbValue.Rows[j]["CHROMA_CHRXML"].ToString();
							
							objRecordContent.m_strDROPCOUNT_CHR=dtbValue.Rows[j]["DROPCOUNT_CHR"].ToString();
							objRecordContent.m_strDROPCOUNT_CHR_RIGHT=dtbValue.Rows[j]["DROPCOUNT_CHR_RIGHT"].ToString();
							objRecordContent.m_strDROPCOUNT_CHRXML=dtbValue.Rows[j]["DROPCOUNT_CHRXML"].ToString();

							objRecordContent.m_strPALACESHRINK_CHR=dtbValue.Rows[j]["PALACESHRINK_CHR"].ToString();
							objRecordContent.m_strPALACESHRINK_CHR_RIGHT=dtbValue.Rows[j]["PALACESHRINK_CHR_RIGHT"].ToString();
							objRecordContent.m_strPALACESHRINK_CHRXML=dtbValue.Rows[j]["PALACESHRINK_CHRXML"].ToString();

							objRecordContent.m_strEMBRYOHEART_CHR=dtbValue.Rows[j]["EMBRYOHEART_CHR"].ToString();
							objRecordContent.m_strEMBRYOHEART_CHR_RIGHT=dtbValue.Rows[j]["EMBRYOHEART_CHR_RIGHT"].ToString();
							objRecordContent.m_strEMBRYOHEART_CHRXML=dtbValue.Rows[j]["EMBRYOHEART_CHRXML"].ToString();

							objRecordContent.m_strEXPAND_CHR_RIGHT=dtbValue.Rows[j]["EXPAND_CHR_RIGHT"].ToString();
							objRecordContent.m_strEXPAND_CHR=dtbValue.Rows[j]["EXPAND_CHR"].ToString();
							objRecordContent.m_strEXPAND_CHRXML=dtbValue.Rows[j]["EXPAND_CHRXML"].ToString();

							objRecordContent.m_strPRESENTATION_CHR=dtbValue.Rows[j]["PRESENTATION_CHR"].ToString();
							objRecordContent.m_strPRESENTATION_CHR_RIGHT=dtbValue.Rows[j]["PRESENTATION_CHR_RIGHT"].ToString();
							objRecordContent.m_strPRESENTATION_CHRXML=dtbValue.Rows[j]["PRESENTATION_CHRXML"].ToString();


							objRecordContent.m_strBLOODPRESSURE_CHR=dtbValue.Rows[j]["BLOODPRESSURE_CHR"].ToString();
							objRecordContent.m_strBLOODPRESSURE_CHR_RIGHT=dtbValue.Rows[j]["BLOODPRESSURE_CHR_RIGHT"].ToString();
							objRecordContent.m_strBLOODPRESSURE_CHRXML=dtbValue.Rows[j]["BLOODPRESSURE_CHRXML"].ToString();

							objRecordContent.m_strSPECIALRECORD_CHR=dtbValue.Rows[j]["SPECIALRECORD_CHR"].ToString();
							objRecordContent.m_strSPECIALRECORD_CHR_RIGHT=dtbValue.Rows[j]["SPECIALRECORD_CHR_RIGHT"].ToString();
							objRecordContent.m_strSPECIALRECORD_CHRXML=dtbValue.Rows[j]["SPECIALRECORD_CHRXML"].ToString();

							objRecordContent.m_strSIGNATURE_CHR=dtbValue.Rows[j]["SIGNATURE_CHR"].ToString();
							objRecordContent.m_strSIGNATURE_CHR_RIGHT=dtbValue.Rows[j]["SIGNATURE_CHR_RIGHT"].ToString();
							objRecordContent.m_strSIGNATURE_CHRXML=dtbValue.Rows[j]["SIGNATURE_CHRXML"].ToString();

	

							objRecordContent.m_strLAYCOUNT_CHR =dtbValue.Rows[j]["LAYCOUNT_CHR"].ToString();							
							objRecordContent.m_strLAYCOUNT_CHRXML =dtbValue.Rows[j]["LAYCOUNT_CHRXML"].ToString();

							objRecordContent.m_strPREGNANTWEEK_CHR =dtbValue.Rows[j]["PREGNANTWEEK_CHR"].ToString();							
							objRecordContent.m_strPREGNANTWEEK_CHRXML =dtbValue.Rows[j]["PREGNANTWEEK_CHRXML"].ToString();

							objRecordContent.m_strSCORECOUNT_CHR =dtbValue.Rows[j]["SCORECOUNT_CHR"].ToString();							
							objRecordContent.m_strSCORECOUNT_CHRXML =dtbValue.Rows[j]["SCORECOUNT_CHRXML"].ToString();

							objRecordContent.m_strRDBNECKEXPAND_CHR =dtbValue.Rows[j]["RDBNECKEXPAND_CHR"].ToString();							
							objRecordContent.m_strRDBNECKEXPAND_CHRXML =dtbValue.Rows[j]["RDBNECKEXPAND_CHRXML"].ToString();

							objRecordContent.m_strRDBNECKSHINK_CHR =dtbValue.Rows[j]["RDBNECKSHINK_CHR"].ToString();							
							objRecordContent.m_strRDBNECKSHINK_CHRXML =dtbValue.Rows[j]["RDBNECKSHINK_CHRXML"].ToString();

							objRecordContent.m_strRDBHIGHLOW_CHR =dtbValue.Rows[j]["RDBHIGHLOW_CHR"].ToString();							
							objRecordContent.m_strRDBHIGHLOW_CHRXML =dtbValue.Rows[j]["RDBHIGHLOW_CHRXML"].ToString();
							

							objRecordContent.m_strRDBNECKHARD_CHR =dtbValue.Rows[j]["RDBNECKHARD_CHR"].ToString();							
							objRecordContent.m_strRDBNECKHARD_CHRXML =dtbValue.Rows[j]["RDBNECKHARD_CHRXML"].ToString();

							objRecordContent.m_strRDBNECKLOCATION_CHR =dtbValue.Rows[j]["RDBNECKLOCATION_CHR"].ToString();							
							objRecordContent.m_strRDBNECKLOCATION_CHRXML =dtbValue.Rows[j]["RDBNECKLOCATION_CHRXML"].ToString();

							objRecordContent.m_strDROPPINGCASE_CHR =dtbValue.Rows[j]["DROPPINGCASE_CHR"].ToString();							
							objRecordContent.m_strDROPPINGCASE_CHRXML =dtbValue.Rows[j]["DROPPINGCASE_CHRXML"].ToString();

							
							objRecordContent.m_strINDICATE_CHR =dtbValue.Rows[j]["INDICATE_CHR"].ToString();							
							objRecordContent.m_strINDICATE_CHRXML =dtbValue.Rows[j]["INDICATE_CHRXML"].ToString();

							objRecordContent.m_strUSECOUNT_CHR =dtbValue.Rows[j]["USECOUNT_CHR"].ToString();							
							objRecordContent.m_strUSECOUNT_CHRXML =dtbValue.Rows[j]["USECOUNT_CHRXML"].ToString();

							
							objRecordContent.m_strLAYWAY_CHR =dtbValue.Rows[j]["LAYWAY_CHR"].ToString();							
							objRecordContent.m_strLAYWAY_CHRXML =dtbValue.Rows[j]["LAYWAY_CHRXML"].ToString();
							#endregion
						}       
				
						p_objTansDataInfo[j] = objRecordContent;
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
		#endregion 

		#region 获取指定记录的内容
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
			clsHurryVeinRecord[] p_objGeneralNurseRecordArr = null;
			//			clsGeneralNurseRecordContent_GXDetail[] p_objGeneralNurseDetailArr = null;
			p_objIntensiveTendInfoArr = new clsTransDataInfo[1];
			long lngRes = -1;
			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			clsHurryVeinRecordContentDataInfo objDataInfo = new clsHurryVeinRecordContentDataInfo();
			
			try
			{
				IDataParameter[] objDPArr = null;
				p_objHRPServ.CreateDatabaseParameter(2,out objDPArr);
                objDPArr[0].Value = p_strInPatientID.Trim();
                objDPArr[1].DbType = DbType.DateTime;
				objDPArr[1].Value=DateTime.Parse(p_strInPatientDate);	

				//				DataTable dtbDetail = new DataTable();//病情记录内容
				DataTable dtbContent = new DataTable();//护理记录内容  

				lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbContent, objDPArr);
				if(lngRes > 0 && dtbContent.Rows.Count > 0)
				{
					clsHurryVeinRecord objRecordContent = null;
					p_objGeneralNurseRecordArr = new clsHurryVeinRecord[dtbContent.Rows.Count];
					for(int i=0; i<dtbContent.Rows.Count; i++)
					{
						#region set values
						objRecordContent = new clsHurryVeinRecord();
						objRecordContent.m_dtmCreateDate = DateTime.Parse(dtbContent.Rows[i]["CREATEDATE"].ToString());
						objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbContent.Rows[i]["MODIFYDATE"].ToString());
						objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbContent.Rows[i]["OpenDate"].ToString());

						if(dtbContent.Rows[i]["FIRSTPRINTDATE"].ToString()=="")
							objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
						else objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbContent.Rows[i]["FIRSTPRINTDATE"].ToString());
						objRecordContent.m_strCreateUserID=dtbContent.Rows[i]["CREATEUSERID"].ToString();
						objRecordContent.m_strModifyUserID=dtbContent.Rows[i]["MODIFYUSERID"].ToString();
						//						objRecordContent.m_strContentCreateUserName = dtbContent.Rows[i]["CreateUserName"].ToString();
						if(dtbContent.Rows[i]["IFCONFIRM"].ToString()=="")
							objRecordContent.m_bytIfConfirm=0;
						else objRecordContent.m_bytIfConfirm=Byte.Parse(dtbContent.Rows[i]["IFCONFIRM"].ToString());
						if(dtbContent.Rows[i]["STATUS"].ToString()=="")
							objRecordContent.m_bytStatus=0;
						else objRecordContent.m_bytStatus=Byte.Parse(dtbContent.Rows[i]["STATUS"].ToString());

																	
							
						objRecordContent.m_strCHROMA_CHR=dtbContent.Rows[i]["CHROMA_CHR"].ToString();
						objRecordContent.m_strCHROMA_CHR_RIGHT=dtbContent.Rows[i]["CHROMA_CHR_RIGHT"].ToString();
						objRecordContent.m_strCHROMA_CHRXML=dtbContent.Rows[i]["CHROMA_CHRXML"].ToString();
							
						objRecordContent.m_strDROPCOUNT_CHR=dtbContent.Rows[i]["DROPCOUNT_CHR"].ToString();
						objRecordContent.m_strDROPCOUNT_CHR_RIGHT=dtbContent.Rows[i]["DROPCOUNT_CHR_RIGHT"].ToString();
						objRecordContent.m_strDROPCOUNT_CHRXML=dtbContent.Rows[i]["DROPCOUNT_CHRXML"].ToString();

						objRecordContent.m_strPALACESHRINK_CHR=dtbContent.Rows[i]["PALACESHRINK_CHR"].ToString();
						objRecordContent.m_strPALACESHRINK_CHR_RIGHT=dtbContent.Rows[i]["PALACESHRINK_CHR_RIGHT"].ToString();
						objRecordContent.m_strPALACESHRINK_CHRXML=dtbContent.Rows[i]["PALACESHRINK_CHRXML"].ToString();

						objRecordContent.m_strEMBRYOHEART_CHR=dtbContent.Rows[i]["EMBRYOHEART_CHR"].ToString();
						objRecordContent.m_strEMBRYOHEART_CHR_RIGHT=dtbContent.Rows[i]["EMBRYOHEART_CHR_RIGHT"].ToString();
						objRecordContent.m_strEMBRYOHEART_CHRXML=dtbContent.Rows[i]["EMBRYOHEART_CHRXML"].ToString();

						objRecordContent.m_strEXPAND_CHR_RIGHT=dtbContent.Rows[i]["EXPAND_CHR_RIGHT"].ToString();
						objRecordContent.m_strEXPAND_CHR=dtbContent.Rows[i]["EXPAND_CHR"].ToString();
						objRecordContent.m_strEXPAND_CHRXML=dtbContent.Rows[i]["EXPAND_CHRXML"].ToString();

						objRecordContent.m_strPRESENTATION_CHR=dtbContent.Rows[i]["PRESENTATION_CHR"].ToString();
						objRecordContent.m_strPRESENTATION_CHR_RIGHT=dtbContent.Rows[i]["PRESENTATION_CHR_RIGHT"].ToString();
						objRecordContent.m_strPRESENTATION_CHRXML=dtbContent.Rows[i]["PRESENTATION_CHRXML"].ToString();


						objRecordContent.m_strBLOODPRESSURE_CHR=dtbContent.Rows[i]["BLOODPRESSURE_CHR"].ToString();
						objRecordContent.m_strBLOODPRESSURE_CHR_RIGHT=dtbContent.Rows[i]["BLOODPRESSURE_CHR_RIGHT"].ToString();
						objRecordContent.m_strBLOODPRESSURE_CHRXML=dtbContent.Rows[i]["BLOODPRESSURE_CHRXML"].ToString();

						objRecordContent.m_strSPECIALRECORD_CHR=dtbContent.Rows[i]["SPECIALRECORD_CHR"].ToString();
						objRecordContent.m_strSPECIALRECORD_CHR_RIGHT=dtbContent.Rows[i]["SPECIALRECORD_CHR_RIGHT"].ToString();
						objRecordContent.m_strSPECIALRECORD_CHRXML=dtbContent.Rows[i]["SPECIALRECORD_CHRXML"].ToString();

						objRecordContent.m_strSIGNATURE_CHR=dtbContent.Rows[i]["SIGNATURE_CHR"].ToString();
						objRecordContent.m_strSIGNATURE_CHR_RIGHT=dtbContent.Rows[i]["SIGNATURE_CHR_RIGHT"].ToString();
						objRecordContent.m_strSIGNATURE_CHRXML=dtbContent.Rows[i]["SIGNATURE_CHRXML"].ToString();

	

						objRecordContent.m_strLAYCOUNT_CHR =dtbContent.Rows[i]["LAYCOUNT_CHR"].ToString();							
						objRecordContent.m_strLAYCOUNT_CHRXML =dtbContent.Rows[i]["LAYCOUNT_CHRXML"].ToString();

						objRecordContent.m_strPREGNANTWEEK_CHR =dtbContent.Rows[i]["PREGNANTWEEK_CHR"].ToString();							
						objRecordContent.m_strPREGNANTWEEK_CHRXML =dtbContent.Rows[i]["PREGNANTWEEK_CHRXML"].ToString();

						objRecordContent.m_strSCORECOUNT_CHR =dtbContent.Rows[i]["SCORECOUNT_CHR"].ToString();							
						objRecordContent.m_strSCORECOUNT_CHRXML =dtbContent.Rows[i]["SCORECOUNT_CHRXML"].ToString();

						objRecordContent.m_strRDBNECKEXPAND_CHR =dtbContent.Rows[i]["RDBNECKEXPAND_CHR"].ToString();							
						objRecordContent.m_strRDBNECKEXPAND_CHRXML =dtbContent.Rows[i]["RDBNECKEXPAND_CHRXML"].ToString();

						objRecordContent.m_strRDBNECKSHINK_CHR =dtbContent.Rows[i]["RDBNECKSHINK_CHR"].ToString();							
						objRecordContent.m_strRDBNECKSHINK_CHRXML =dtbContent.Rows[i]["RDBNECKSHINK_CHRXML"].ToString();

						objRecordContent.m_strRDBHIGHLOW_CHR =dtbContent.Rows[i]["RDBHIGHLOW_CHR"].ToString();							
						objRecordContent.m_strRDBHIGHLOW_CHRXML =dtbContent.Rows[i]["RDBHIGHLOW_CHRXML"].ToString();
							

						objRecordContent.m_strRDBNECKHARD_CHR =dtbContent.Rows[i]["RDBNECKHARD_CHR"].ToString();							
						objRecordContent.m_strRDBNECKHARD_CHRXML =dtbContent.Rows[i]["RDBNECKHARD_CHRXML"].ToString();

						objRecordContent.m_strRDBNECKLOCATION_CHR =dtbContent.Rows[i]["RDBNECKLOCATION_CHR"].ToString();							
						objRecordContent.m_strRDBNECKLOCATION_CHRXML =dtbContent.Rows[i]["RDBNECKLOCATION_CHRXML"].ToString();

							
						objRecordContent.m_strDROPPINGCASE_CHR =dtbContent.Rows[i]["DROPPINGCASE_CHR"].ToString();							
						objRecordContent.m_strDROPPINGCASE_CHRXML =dtbContent.Rows[i]["DROPPINGCASE_CHRXML"].ToString();

							
						objRecordContent.m_strINDICATE_CHR =dtbContent.Rows[i]["INDICATE_CHR"].ToString();							
						objRecordContent.m_strINDICATE_CHRXML =dtbContent.Rows[i]["INDICATE_CHRXML"].ToString();

						objRecordContent.m_strUSECOUNT_CHR =dtbContent.Rows[i]["USECOUNT_CHR"].ToString();							
						objRecordContent.m_strUSECOUNT_CHRXML =dtbContent.Rows[i]["USECOUNT_CHRXML"].ToString();

							
						objRecordContent.m_strLAYWAY_CHR =dtbContent.Rows[i]["LAYWAY_CHR"].ToString();							
						objRecordContent.m_strLAYWAY_CHRXML =dtbContent.Rows[i]["LAYWAY_CHRXML"].ToString();

						p_objGeneralNurseRecordArr[i] = objRecordContent;
						#endregion
					}
				}
				objDataInfo.m_objRecordArr = p_objGeneralNurseRecordArr;
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
		#endregion

		#region 查看当前记录是否最新的记录
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

			//检查参数          
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null|| p_objHRPServ==null)
				return (long)enmOperationResult.Parameter_Error;
			
			string strSQL = clsDatabaseSQLConvert.s_StrTop1+@" t2.modifydate,t2.modifyuserid from 
											icuacad_hurryveinrecord t1,icuacad_hurryveincontent t2
											where t1.inpatientid = t2.inpatientid and t1.inpatientdate = t2.inpatientdate
											and t1.opendate = t2.opendate and t1.status = '0'
											and t1.inpatientid = ? and t1.inpatientdate = ? and t1.opendate = ? order by t2.modifydate desc "+clsDatabaseSQLConvert.s_StrRownum;

				
			long lngRes = 0;
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
		#endregion

		#region 把记录从数据中“删除”。
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
				IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
				//按顺序给IDataParameter赋值
				//			for(int i=0;i<objDPArr.Length;i++)
				//				objDPArr[i]=new Oracle.DataAccess.Client.OracleParameter();
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
		#endregion
	}
}
