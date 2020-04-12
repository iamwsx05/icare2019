using System;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.Utility.SQLConvert;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;
//using com.digitalwave.DiseaseTrackService;

namespace com.digitalwave.LabAnalysisOrderService
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
    public class clsLabAnalysisOrderService : com.digitalwave.iCare.middletier.clsMiddleTierBase
	{
		/// <summary>
		/// 
		/// </summary>
        private const string c_strGetAllLabCheckAliasSQL = @"select distinct item_alias, item_name, subitem_name from labcheckalias";


		/// <summary>
		/// 
		/// </summary>
		private const string c_strGetTimeListSQL = @"select distinct createdate  from labcheck_order where inpatientid = ? and inpatientdate = ? and status = 0";

		/// <summary>
		/// 
		/// </summary>
		private const string c_strGetSendTimeAndBarCodeListSQL = @"select distinct sdate,barcode  from labcheck_order where inpatientid = ? and inpatientdate = ? and status = 0";

		/// <summary>
		/// 
		/// </summary>
        private const string c_strCheckCreateDateSQL_Oracle = @"select inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       barcode,
       createuserid,
       specimen,
       dignose,
       sdate,
       sdocid,
       recdocid,
       remark,
       status,
       deactiveoperatorid,
       deactivedate,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       firstprintdate
  from (select inpatientid,
               inpatientdate,
               createdate,
               modifydate,
               barcode,
               createuserid,
               specimen,
               dignose,
               sdate,
               sdocid,
               recdocid,
               remark,
               status,
               deactiveoperatorid,
               deactivedate,
               ifconfirm,
               confirmreason,
               confirmreasonxml,
               firstprintdate
          from labcheck_order
         where trim(inpatientid) = ?
           and inpatientdate = ?
           and createdate = ?
           and status = 0
         order by modifydate desc)
 where rownum = 1";

        private const string c_strCheckCreateDateSQL_ODBC = @"select top 1 inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       barcode,
       createuserid,
       specimen,
       dignose,
       sdate,
       sdocid,
       recdocid,
       remark,
       status,
       deactiveoperatorid,
       deactivedate,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       firstprintdate
  from labcheck_order
 where inpatientid = ?
   and inpatientdate = ?
   and createdate = ?
   and status = 0
 order by modifydate desc";
		/// <summary>
		/// 
		/// </summary>
		private const string c_strAddNewContentSQL = @"insert into labcheck_order 
												(inpatientid,inpatientdate,createdate,modifydate,barcode,createuserid,specimen,dignose,sdate,sdocid,recdocid,remark,status,ifconfirm,confirmreason,confirmreasonxml)
												values
												(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

		/// <summary>
		/// 
		/// </summary>
		private const string c_strAddNewSubContentSQL = @"insert into labcheck_order_item (barcode,modifydate,item_id,inpatientid,inpatientdate,createdate,groupid)
												values
												(?,?,?,?,?,?,?)";

		/// <summary>
		/// 
		/// </summary>
        private const string c_strGetContenSQL_Oracle = @"select inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       barcode,
       createuserid,
       specimen,
       dignose,
       sdate,
       sdocid,
       recdocid,
       remark,
       status,
       deactiveoperatorid,
       deactivedate,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       firstprintdate
  from (select inpatientid,
               inpatientdate,
               createdate,
               modifydate,
               barcode,
               createuserid,
               specimen,
               dignose,
               sdate,
               sdocid,
               recdocid,
               remark,
               status,
               deactiveoperatorid,
               deactivedate,
               ifconfirm,
               confirmreason,
               confirmreasonxml,
               firstprintdate
          FROM LabCheck_Order
         Where trim(InPatientID) = ?
           AND InPatientDate = ?
           AND CreateDate = ?
           AND Status = 0
         ORDER BY ModifyDate DESC)
 where rownum = 1";

        private const string c_strGetContenSQL_ODBC = @"select top 1 inpatientid,
       inpatientdate,
       createdate,
       modifydate,
       barcode,
       createuserid,
       specimen,
       dignose,
       sdate,
       sdocid,
       recdocid,
       remark,
       status,
       deactiveoperatorid,
       deactivedate,
       ifconfirm,
       confirmreason,
       confirmreasonxml,
       firstprintdate
  from labcheck_order
 where inpatientid = ?
   and inpatientdate = ?
   and createdate = ?
   and status = 0
 order by modifydate desc";

		/// <summary>
		/// 
		/// </summary>
		private const string c_strGetSubContenSQL = @"select loi.groupid, loi.item_id,lid.item_name,lgd.group_name
												from labcheck_order_item loi left outer join labcheck_item_desc lid on loi.item_id = lid.item_id
												left outer join labcheck_group_desc lgd on loi.groupid = lgd.group_id
												where inpatientid = ? and inpatientdate = ? and createdate = ? and modifydate = ?";

		/// <summary>
		/// 
		/// </summary>
		private const string c_strDeleteContenSQL = @"update labcheck_order set status = ? ,deactiveoperatorid = ? ,deactivedate = ?
												where inpatientid = ? and inpatientdate = ? and createdate = ? ";

		/// <summary>
		/// 
		/// </summary>
		private const string c_strGetMaxBarCode = @"select max(barcode) as barcode from labcheck_order ";


		/// <summary>
		/// 
		/// </summary>
		private const string c_strGetBarCodeList = @"select distinct barcode as barcode from labcheck_order where status = 0 and barcode like ? ";

		/// <summary>
		/// 这里的BarCode相当于检验号Pat_ID
		/// </summary>
		private const string c_strGetBarCodeList_Pat_ID = @"select pat_id,pat_d_name from jy_brzl where pat_id like ? ";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngAddNewRecord2DB(
			 clsLabCheckOrderContent p_objRecordContent)
		{
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngAddNewRecord2DB");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			
			

			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null)
				return (long)enmOperationResult.Parameter_Error;

			p_objRecordContent.m_dtmModifyDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(16, out objDPArr);

            objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
            objDPArr[2].DbType = DbType.DateTime;
            objDPArr[2].Value = p_objRecordContent.m_dtmCreateDate;
            objDPArr[3].DbType = DbType.DateTime;
			objDPArr[3].Value = p_objRecordContent.m_dtmModifyDate;
			objDPArr[4].Value = p_objRecordContent.m_strBarCode;
			objDPArr[5].Value = p_objRecordContent.m_strCreateUserID;
			objDPArr[6].Value = p_objRecordContent.m_strSpecimen;
            objDPArr[7].Value = p_objRecordContent.m_strDignose;
            objDPArr[8].DbType = DbType.DateTime;
			objDPArr[8].Value = p_objRecordContent.m_dtmSDate;
			objDPArr[9].Value = p_objRecordContent.m_strSDocID;
			objDPArr[10].Value = p_objRecordContent.m_strRecDocID;
			objDPArr[11].Value = p_objRecordContent.m_strRemark;
			objDPArr[12].Value = p_objRecordContent.m_bytStatus;
			objDPArr[13].Value = p_objRecordContent.m_bytIfConfirm;

			if(p_objRecordContent.m_strConfirmReason == null)
				objDPArr[14].Value = DBNull.Value;
			else
				objDPArr[14].Value = p_objRecordContent.m_strConfirmReason;

			if(p_objRecordContent.m_strConfirmReasonXML == null)
				objDPArr[15].Value = DBNull.Value;
			else 
				objDPArr[15].Value = p_objRecordContent.m_strConfirmReasonXML;

			long lngEff=0;
			long lngRes = -1;
			try
			{
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewContentSQL, ref lngEff, objDPArr);
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			
			if(lngRes<=0)return lngRes;

			if(p_objRecordContent.m_strItem_IDArr != null && p_objRecordContent.m_strItem_IDArr.Length != 0)
			{
				for(int i = 0; i < p_objRecordContent.m_strItem_IDArr.Length; i++)
				{
					IDataParameter[] objDPArr2 = null;
                    objHRPServ.CreateDatabaseParameter(7, out objDPArr2);

                    objDPArr2[0].Value = p_objRecordContent.m_strBarCode;
                    objDPArr2[1].DbType = DbType.DateTime;
					objDPArr2[1].Value = p_objRecordContent.m_dtmModifyDate; 
					objDPArr2[2].Value = p_objRecordContent.m_strItem_IDArr[i];
                    objDPArr2[3].Value = p_objRecordContent.m_strInPatientID;
                    objDPArr2[4].DbType = DbType.DateTime;
                    objDPArr2[4].Value = p_objRecordContent.m_dtmInPatientDate;
                    objDPArr2[5].DbType = DbType.DateTime;
					objDPArr2[5].Value = p_objRecordContent.m_dtmCreateDate;
					objDPArr2[6].Value = p_objRecordContent.m_strItem_GroupIDArr[i];

					try
					{
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strAddNewSubContentSQL, ref lngEff, objDPArr2);
					}
					catch(Exception objEx)
					{
						com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
						bool blnRes = objLogger.LogError(objEx);					
					}
					
					if(lngRes<=0)return lngRes;
				}
			}
            //objHRPServ.Dispose();
			return lngRes;
		}

		[AutoComplete]
		public long m_lngUpdateFirstPrintDate(
			string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, System.DateTime p_dtmFirstPrintDate)
		{
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngUpdateFirstPrintDate");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			
			

			return 0;
		}

		[AutoComplete]
		public long m_lngModifyRecord2DB(
			 clsLabCheckOrderContent p_objRecordContent)
		{
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngModifyRecord2DB");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			
			

			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strInPatientDate"></param>
		/// <param name="p_strCreateDateArr"></param>
		/// <param name="p_strOpenDateArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetRecordTimeList(
			string p_strInPatientID, string p_strInPatientDate, out string[] p_strCreateDateArr, out string[] p_strOpenDateArr)
		{
			p_strCreateDateArr = null;
			p_strOpenDateArr = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetRecordTimeList");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			
			

			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);

            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
			objDPArr[1].Value = DateTime.Parse(DateTime.Parse(p_strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss"));

			DataTable dtbValue = new DataTable();
			long lngRes = -1;

			try
			{
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetTimeListSQL, ref dtbValue, objDPArr);

				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{
					p_strCreateDateArr = new string[dtbValue.Rows.Count];
					p_strOpenDateArr = new string[dtbValue.Rows.Count];

					for(int i=0;i<dtbValue.Rows.Count;i++)
					{
						//设置结果
						p_strCreateDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
						p_strOpenDateArr[i] = DateTime.Parse(dtbValue.Rows[i]["CREATEDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
					}				
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}			

			return lngRes;
		}

		[AutoComplete]
		public long m_lngGetSendTimeAndBarCodeList(
			string p_strInPatientID, string p_strInPatientDate, out string[] p_strSendDateArr,out string[] p_strBarCodeArr)
		{
			p_strSendDateArr = null;
			p_strBarCodeArr=null;
			long lngRes = -1;

//			//long lngCheckRes = new com.digitalwave.security.security.clsPrivilegeHandleService.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetSendTimeAndBarCodeList");
//			////if (lngCheckRes <= 0)
//				//return lngCheckRes;

			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate=="")
				return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(2, out objDPArr);

            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
			objDPArr[1].Value = DateTime.Parse(DateTime.Parse(p_strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss"));

			DataTable dtbValue = new DataTable();

			try
			{
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetSendTimeAndBarCodeListSQL, ref dtbValue, objDPArr);

				if(lngRes > 0 && dtbValue.Rows.Count >0)
				{				
					p_strSendDateArr = new string[dtbValue.Rows.Count];
					p_strBarCodeArr = new string[dtbValue.Rows.Count];

					for(int i=0;i<dtbValue.Rows.Count;i++)
					{
						//设置结果
						p_strSendDateArr[i]=DateTime.Parse(dtbValue.Rows[i]["SDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
						p_strBarCodeArr[i] = dtbValue.Rows[i]["BARCODE"].ToString();
					}				
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}			

			return lngRes;
		}

		[AutoComplete]
		public long m_lngGetRecordContentWithServ(
			string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out  clsLabCheckOrderContent p_objRecordContent)
		{
			p_objRecordContent = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetRecordContentWithServ");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			
			

			//检查参数
			if(p_strInPatientID==null||p_strInPatientID==""|| p_strInPatientDate==null||p_strInPatientDate==""||p_strCreateDate==null||p_strCreateDate=="")
				return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);

            objDPArr[0].Value = p_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = DateTime.Parse(DateTime.Parse(p_strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss"));
            objDPArr[2].DbType = DbType.DateTime;
			objDPArr[2].Value = DateTime.Parse(DateTime.Parse(p_strCreateDate).ToString("yyyy-MM-dd HH:mm:ss"));		

			DataTable dtbValue = new DataTable();
			long lngRes = -1;

			try
			{
				if(clsHRPTableService.bytDatabase_Selector == 0)
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetContenSQL_ODBC, ref dtbValue, objDPArr);
				else
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetContenSQL_Oracle, ref dtbValue, objDPArr);

				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					clsLabCheckOrderContent objRecordContent = new clsLabCheckOrderContent();

					objRecordContent.m_strInPatientID = p_strInPatientID;
					objRecordContent.m_dtmInPatientDate = DateTime.Parse(p_strInPatientDate);
					objRecordContent.m_dtmCreateDate = DateTime.Parse(p_strCreateDate);

					objRecordContent.m_dtmModifyDate = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
					objRecordContent.m_strBarCode = dtbValue.Rows[0]["BARCODE"].ToString();
					objRecordContent.m_strDignose = dtbValue.Rows[0]["DIGNOSE"].ToString();
					objRecordContent.m_strSpecimen = dtbValue.Rows[0]["SPECIMEN"].ToString();
					objRecordContent.m_dtmSDate = DateTime.Parse(dtbValue.Rows[0]["SDATE"].ToString());
					objRecordContent.m_strSDocID = dtbValue.Rows[0]["SDOCID"].ToString();
					objRecordContent.m_strRecDocID = dtbValue.Rows[0]["RECDOCID"].ToString();
					objRecordContent.m_strCreateUserID = dtbValue.Rows[0]["CREATEUSERID"].ToString();
					objRecordContent.m_strRemark = dtbValue.Rows[0]["REMARK"].ToString();

					if(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString()=="")
						objRecordContent.m_dtmFirstPrintDate=DateTime.MinValue;
					else 
						objRecordContent.m_dtmFirstPrintDate=DateTime.Parse(dtbValue.Rows[0]["FIRSTPRINTDATE"].ToString());

					if(dtbValue.Rows[0]["IFCONFIRM"].ToString()=="")
						objRecordContent.m_bytIfConfirm=0;
					else 
						objRecordContent.m_bytIfConfirm=Byte.Parse(dtbValue.Rows[0]["IFCONFIRM"].ToString());

					if(dtbValue.Rows[0]["STATUS"].ToString()=="")
						objRecordContent.m_bytStatus=0;
					else 
						objRecordContent.m_bytStatus=Byte.Parse(dtbValue.Rows[0]["STATUS"].ToString());

					objRecordContent.m_strConfirmReason=dtbValue.Rows[0]["CONFIRMREASON"].ToString();
					objRecordContent.m_strConfirmReasonXML=dtbValue.Rows[0]["CONFIRMREASONXML"].ToString();
					

					IDataParameter[] objDPArr2 = null;
                    objHRPServ.CreateDatabaseParameter(4, out objDPArr2);

                    objDPArr2[0].Value = p_strInPatientID;
                    objDPArr2[1].DbType = DbType.DateTime;
                    objDPArr2[1].Value = DateTime.Parse(DateTime.Parse(p_strInPatientDate).ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr2[2].DbType = DbType.DateTime;
                    objDPArr2[2].Value = DateTime.Parse(DateTime.Parse(p_strCreateDate).ToString("yyyy-MM-dd HH:mm:ss"));
                    objDPArr2[3].DbType = DbType.DateTime;
					objDPArr2[3].Value = objRecordContent.m_dtmModifyDate;

					DataTable dtbValue1 = new DataTable();
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetSubContenSQL, ref dtbValue1, objDPArr2);
					
					if(lngRes > 0 && dtbValue1.Rows.Count > 0)
					{
						string[] strItemIDArr = new string[dtbValue1.Rows.Count];
						string[] strItemGroupIDArr = new string[dtbValue1.Rows.Count];
						string[] strItemItemNameArr = new string[dtbValue1.Rows.Count];
						string[] strItemGroupNameArr = new string[dtbValue1.Rows.Count];

						for(int i = 0; i < dtbValue1.Rows.Count; i++)
						{
							strItemIDArr[i] = dtbValue1.Rows[i]["ITEM_ID"].ToString();
							strItemGroupIDArr[i] = dtbValue1.Rows[i]["GROUPID"].ToString();
							strItemItemNameArr[i] = dtbValue1.Rows[i]["ITEM_NAME"].ToString();
							strItemGroupNameArr[i] = dtbValue1.Rows[i]["GROUP_NAME"].ToString();
						}

						objRecordContent.m_strItem_IDArr = strItemIDArr;
						objRecordContent.m_strItem_GroupIDArr = strItemGroupIDArr;
						objRecordContent.m_strItem_NameArr = strItemItemNameArr;
						objRecordContent.m_strItem_GroupNameArr = strItemGroupNameArr;
					}
					p_objRecordContent=	objRecordContent;	
				
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			
			return lngRes;
		}

		[AutoComplete]
		public long m_lngGetModifyDateAndFirstPrintDate(
			string p_strInPatientID, string p_strInPatientDate, string p_strCreateDate, out System.DateTime p_dtmModifyDate, out string p_strFirstPrintDate)
		{
			p_dtmModifyDate = new System.DateTime();
			p_strFirstPrintDate = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetModifyDateAndFirstPrintDate");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			
			

			return 0;
		}

		[AutoComplete]
		public long m_lngGetDeleteRecordTimeListAll(
			string p_strInPatientID, string p_strInPatientDate, out string[] p_strCreateRecordTimeArr, out string[] p_strOpenRecordTimeArr)
		{
			p_strCreateRecordTimeArr = null;
			p_strOpenRecordTimeArr = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetDeleteRecordTimeListAll");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;	
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);	
							
			}
			
			

			return 0;
		}

		[AutoComplete]
		public long m_lngGetDeleteRecordTimeList(
			string p_strInPatientID, string p_strInPatientDate, string p_strDeleteUserID, out string[] p_strCreateRecordTimeArr, out string[] p_strOpenRecordTimeArr)
		{
			p_strCreateRecordTimeArr = null;
			p_strOpenRecordTimeArr = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetDeleteRecordTimeList");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;	
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
								
			}
			
			

			return 0;
		}

		[AutoComplete]
		public long m_lngGetDeleteRecordContentWithServ(string p_strInPatientID, string p_strInPatientDate, string p_strOpenRecordTime,out clsLabCheckOrderContent p_objRecordContent)
		{
			p_objRecordContent = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetDeleteRecordContentWithServ");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;	
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
								
			}
			
			

			return 0;
		}

		[AutoComplete]
		public long m_lngDeleteRecord2DB(
			 clsLabCheckOrderContent p_objRecordContent)
		{
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngDeleteRecord2DB");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;	
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
								
			}
			
			
			if(p_objRecordContent == null)
				return (long)enmOperationResult.Parameter_Error;

			p_objRecordContent.m_dtmDeActiveDate = DateTime.Parse( DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(6, out objDPArr);
 
			objDPArr[0].Value = p_objRecordContent.m_bytStatus;
            objDPArr[1].Value = p_objRecordContent.m_strDeActiveOperatorID;
            objDPArr[2].DbType = DbType.DateTime;
			objDPArr[2].Value = p_objRecordContent.m_dtmDeActiveDate;
            objDPArr[3].Value = p_objRecordContent.m_strInPatientID;
            objDPArr[4].DbType = DbType.DateTime;
            objDPArr[4].Value = p_objRecordContent.m_dtmInPatientDate;
            objDPArr[5].DbType = DbType.DateTime;
			objDPArr[5].Value = p_objRecordContent.m_dtmCreateDate;

			long lngAff = 0;
			long lngRes = -1;
			try
			{
                lngRes = objHRPServ.lngExecuteParameterSQL(c_strDeleteContenSQL, ref lngAff, objDPArr);
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			return lngRes;
		}

		[AutoComplete]
		public long m_lngCheckLastModifyRecord(
			 clsLabCheckOrderContent p_objRecordContent, out clsPreModifyInfo p_objModifyInfo)
		{
			p_objModifyInfo = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngCheckLastModifyRecord");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;	
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);
								
			}
			
			

			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_objRecordContent"></param>
		/// <param name="p_objHRPServ"></param>
		/// <param name="p_objPreModifyInfo"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngCheckCreateDate(
			 clsLabCheckOrderContent p_objRecordContent, out clsPreModifyInfo p_objPreModifyInfo)
		{
			p_objPreModifyInfo = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngCheckCreateDate");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			
			

			//检查参数
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null)
				return (long)enmOperationResult.Parameter_Error;

			//获取IDataParameter数组
            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(3, out objDPArr);
            objDPArr[0].Value = p_objRecordContent.m_strInPatientID;
            objDPArr[1].DbType = DbType.DateTime;
            objDPArr[1].Value = p_objRecordContent.m_dtmInPatientDate;
            objDPArr[2].DbType = DbType.DateTime;
			objDPArr[2].Value=p_objRecordContent.m_dtmCreateDate;

			DataTable dtbValue = new DataTable();
			long lngRes = -1; 
			try
			{
				if(clsHRPTableService.bytDatabase_Selector == 0)
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL_ODBC, ref dtbValue, objDPArr);
                else if (clsHRPTableService.bytDatabase_Selector ==2)
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL_Oracle, ref dtbValue, objDPArr);
                else if (clsHRPTableService.bytDatabase_Selector == 4)
                    lngRes = objHRPServ.lngGetDataTableWithParameters(c_strCheckCreateDateSQL_Oracle, ref dtbValue, objDPArr);

				if(lngRes > 0 && dtbValue.Rows.Count ==1)
				{
					p_objPreModifyInfo=new clsPreModifyInfo();
					p_objPreModifyInfo.m_strActionUserID=dtbValue.Rows[0]["CREATEUSERID"].ToString();
					p_objPreModifyInfo.m_dtmActionTime=DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
					return (long)enmOperationResult.Record_Already_Exist;
				}
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			return lngRes;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strBarCode"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetMaxBarCode(
			out string p_strBarCode)
		{
			p_strBarCode = null;
			long lngRes = -1;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal, "clsLabAnalysisOrderService", "m_lngGetMaxBarCode");
                //if (lngCheckRes <= 0)
                    //return lngCheckRes;

                DataTable dtbValue = new DataTable();

                lngRes = objHRPServ.DoGetDataTable(c_strGetMaxBarCode, ref dtbValue);

                if (lngRes < 0 || dtbValue.Rows.Count != 1)
                    return 0;

                p_strBarCode = (dtbValue.Rows[0]["BARCODE"].ToString() == "") ? "0000000000" : dtbValue.Rows[0]["BARCODE"].ToString();
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
		/// 
		/// </summary>
		/// <param name="p_strBarCode"></param>
		/// <param name="p_strBarCodeArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetBarCodeList(
			string p_strBarCode,out string[] p_strBarCodeArr)
		{
			p_strBarCodeArr = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetBarCodeList");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			
			
			if(p_strBarCode == null || p_strBarCode == "")
				return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
 
			objDPArr[0].Value = p_strBarCode + "%";

			DataTable dtbValue = new DataTable();
			long lngRes = -1;

             try
            {
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetBarCodeList, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strBarCodeArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strBarCodeArr[i] = dtbValue.Rows[i]["BARCODE"].ToString();
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

		/// <summary>
		/// 获取所有的检验号
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strBarCode"></param>
		/// <param name="p_strBarCodeArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetBarCodeList_Pat_ID(
			string p_strBarCode,out string[] p_strBarCodeArr,out string[] p_strDeptNameArr)
		{
			p_strBarCodeArr = null;
			p_strDeptNameArr = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetBarCodeList_Pat_ID");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			
			
			if(p_strBarCode == null || p_strBarCode == "")
				return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
 			objDPArr[0].Value = p_strBarCode + "%";

			DataTable dtbValue = new DataTable();
			long lngRes = -1;
 
            try
            {
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetBarCodeList_Pat_ID, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_strBarCodeArr = new string[dtbValue.Rows.Count];
                    p_strDeptNameArr = new string[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //设置结果
                        p_strBarCodeArr[i] = dtbValue.Rows[i]["PAT_ID"].ToString().Trim();
                        p_strDeptNameArr[i] = dtbValue.Rows[i]["PAT_D_NAME"].ToString().Trim();
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



        private const string c_strGetJY_BRZLContenSQL = @"select pat_id,
       pat_in_no,
       pat_name,
       pat_sex,
       pat_d_name,
       pat_c_name,
       pat_bed_no,
       pat_date,
       pat_sdate,
       pat_diag,
       pat_rem,
       pat_i_name,
       pat_s_name,
       pat_chk,
       pat_sid,
       pat_age,
       pat_doct,
       pat_mid,
       pat_checkorder,
       pat_rep_form
  from jy_brzl
 where pat_id = ?";

        private const string c_strGetJY_BRZLSQL = @"select pat_id,
       pat_in_no,
       pat_name,
       pat_sex,
       pat_d_name,
       pat_c_name,
       pat_bed_no,
       pat_date,
       pat_sdate,
       pat_diag,
       pat_rem,
       pat_i_name,
       pat_s_name,
       pat_chk,
       pat_sid,
       pat_age,
       pat_doct,
       pat_mid,
       pat_checkorder,
       pat_rep_form
  from jy_brzl
 where pat_in_no = ?";

        private const string c_strGetJY_JGContentSQL = @"select res_id,
       res_date,
       res_it_ecd,
       res_chr,
       res_chr1,
       res_exp,
       res_ref1,
       res_ref2,
       res_ref3,
       res_ref4,
       res_unit,
       res_name
  from jy_jg
 where res_id = ?";

        private const string c_strGetJY_QXContentSQL = @"select res_id, res_date, res_wbc, res_rbc, res_plt
  from jy_qxjg
 where res_id = ?";

        private const string c_strGetJY_DYContentSQL = @"select res_id, res_date, res_cur1, res_cur2, res_cur3, res_cur4, res_cur5
  from jy_dyjg
 where res_id = ?";

        private const string c_strGetJY_MSJGContentSQL = @"select res_id, res_date, res_cname from jy_msjg where res_id = ?";

        private const string c_strGetJY_YMJGContentSQL = @"select res_id,
       res_date,
       res_barname,
       res_barcname,
       res_bcnt,
       res_antiname,
       res_anticname,
       res_mic,
       res_smic
  from jy_ymjg
 where res_id = ?";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p_strBarCode"></param>
		/// <param name="p_objPatient"></param>
		/// <param name="p_objResultArr"></param>
		/// <param name="p_objQXResultArr"></param>
		/// <param name="p_objDYResultArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetReportInfomation(
			string p_strBarCode, out clsJY_BRZL p_objPatient, out clsJY_JG[] p_objResultArr, out clsJY_QXJG[] p_objQXResultArr, out clsJY_DYJG[] p_objDYResultArr)
		{
			p_objPatient = null;
			p_objResultArr = null;
			p_objQXResultArr = null;
			p_objDYResultArr = null;

			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetReportInfomation");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			if(p_strBarCode == null || p_strBarCode == "")
				return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
 
			//JY_BRZL
			objDPArr[0].Value = p_strBarCode;

			DataTable dtbValue = new DataTable();
            try
            {
                long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetJY_BRZLContenSQL, ref dtbValue, objDPArr);

                if (lngRes <= 0 || dtbValue.Rows.Count == 0)
                    return 0;
                p_objPatient = new clsJY_BRZL();
                p_objPatient.m_dtmPat_sdate = DateTime.Parse(dtbValue.Rows[0]["PAT_SDATE"].ToString());
                p_objPatient.m_dtmPat_date = DateTime.Parse(dtbValue.Rows[0]["PAT_DATE"].ToString());
                p_objPatient.m_strPat_age = dtbValue.Rows[0]["PAT_AGE"].ToString();
                p_objPatient.m_strPat_bed_no = dtbValue.Rows[0]["PAT_BED_NO"].ToString();
                p_objPatient.m_strPat_c_name = dtbValue.Rows[0]["PAT_C_NAME"].ToString().Trim();
                p_objPatient.m_strPat_chk = dtbValue.Rows[0]["PAT_CHK"].ToString();
                p_objPatient.m_strPat_d_name = dtbValue.Rows[0]["PAT_D_NAME"].ToString();
                p_objPatient.m_strPat_diag = dtbValue.Rows[0]["PAT_DIAG"].ToString();
                p_objPatient.m_strPat_doct = dtbValue.Rows[0]["PAT_DOCT"].ToString();
                p_objPatient.m_strPat_I_name = dtbValue.Rows[0]["PAT_I_NAME"].ToString();
                p_objPatient.m_strPat_id = dtbValue.Rows[0]["PAT_ID"].ToString().Trim();
                p_objPatient.m_strPat_in_no = dtbValue.Rows[0]["PAT_IN_NO"].ToString();
                p_objPatient.m_strPat_mid = dtbValue.Rows[0]["PAT_MID"].ToString();
                p_objPatient.m_strPat_name = dtbValue.Rows[0]["PAT_NAME"].ToString();
                p_objPatient.m_strPat_rem = dtbValue.Rows[0]["PAT_REM"].ToString();
                p_objPatient.m_strPat_s_name = dtbValue.Rows[0]["PAT_S_NAME"].ToString();
                p_objPatient.m_strPat_sex = dtbValue.Rows[0]["PAT_SEX"].ToString();
                p_objPatient.m_strPat_sid = dtbValue.Rows[0]["PAT_SID"].ToString();

                if (p_objPatient.m_strPat_id == null || p_objPatient.m_strPat_id == "")
                    return 0;

                IDataParameter[] objDPArr1 = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr1);

                objDPArr1[0].Value = p_objPatient.m_strPat_id;
                DataTable dtbValue1 = new DataTable();
                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetJY_JGContentSQL, ref dtbValue1, objDPArr1);
                if (lngRes > 0 && dtbValue1.Rows.Count > 0)
                {
                    p_objResultArr = new clsJY_JG[dtbValue1.Rows.Count];
                    for (int i = 0; i < dtbValue1.Rows.Count; i++)
                    {
                        p_objResultArr[i] = new clsJY_JG();
                        p_objResultArr[i].m_strRes_id = dtbValue1.Rows[i]["RES_ID"].ToString();
                        p_objResultArr[i].m_dtmRes_date = DateTime.Parse(dtbValue1.Rows[i]["RES_DATE"].ToString());
                        p_objResultArr[i].m_strRes_chr = dtbValue1.Rows[i]["RES_CHR"].ToString();
                        p_objResultArr[i].m_strRes_chr1 = dtbValue1.Rows[i]["RES_CHR1"].ToString();
                        p_objResultArr[i].m_strRes_exp = dtbValue1.Rows[i]["RES_EXP"].ToString();
                        p_objResultArr[i].m_strRes_it_ecd = dtbValue1.Rows[i]["RES_IT_ECD"].ToString();
                        p_objResultArr[i].m_strRes_name = dtbValue1.Rows[i]["RES_NAME"].ToString();
                        p_objResultArr[i].m_strRes_ref1 = dtbValue1.Rows[i]["RES_REF1"].ToString();
                        p_objResultArr[i].m_strRes_ref2 = dtbValue1.Rows[i]["RES_REF2"].ToString();
                        p_objResultArr[i].m_strRes_ref3 = dtbValue1.Rows[i]["RES_REF3"].ToString();
                        p_objResultArr[i].m_strRes_ref4 = dtbValue1.Rows[i]["RES_REF4"].ToString();
                        p_objResultArr[i].m_strRes_unit = dtbValue1.Rows[i]["RES_UNIT"].ToString();
                    }
                }

                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr2);

                objDPArr2[0].Value = p_objPatient.m_strPat_id;
                DataTable dtbValue2 = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetJY_QXContentSQL, ref dtbValue2, objDPArr2);
                if (lngRes > 0 && dtbValue1.Rows.Count > 0)
                {
                    p_objQXResultArr = new clsJY_QXJG[dtbValue2.Rows.Count];
                    for (int i = 0; i < dtbValue2.Rows.Count; i++)
                    {
                        p_objQXResultArr[i] = new clsJY_QXJG();

                        p_objQXResultArr[i].m_strRes_id = dtbValue2.Rows[i]["RES_ID"].ToString();
                        p_objQXResultArr[i].m_dtmRes_date = DateTime.Parse(dtbValue2.Rows[i]["RES_DATE"].ToString());
                        p_objQXResultArr[i].m_strRes_wbc = dtbValue2.Rows[i]["RES_WBC"].ToString();
                        p_objQXResultArr[i].m_strRes_rbc = dtbValue2.Rows[i]["RES_RBC"].ToString();
                        p_objQXResultArr[i].m_strRes_plt = dtbValue2.Rows[i]["RES_PLT"].ToString();
                    }
                }


                IDataParameter[] objDPArr3 = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr3);

                objDPArr3[0].Value = p_objPatient.m_strPat_id;
                DataTable dtbValue3 = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetJY_DYContentSQL, ref dtbValue3, objDPArr3);
                if (lngRes > 0 && dtbValue3.Rows.Count > 0)
                {
                    p_objDYResultArr = new clsJY_DYJG[dtbValue3.Rows.Count];
                    for (int i = 0; i < dtbValue3.Rows.Count; i++)
                    {
                        p_objDYResultArr[i] = new clsJY_DYJG();

                        p_objDYResultArr[i].m_strRes_id = dtbValue3.Rows[i]["RES_ID"].ToString();
                        p_objDYResultArr[i].m_dtmRes_date = DateTime.Parse(dtbValue3.Rows[i]["RES_DATE"].ToString());
                        p_objDYResultArr[i].m_strRes_cur1 = dtbValue3.Rows[i]["RES_CUR1"].ToString();
                        p_objDYResultArr[i].m_strRes_cur2 = dtbValue3.Rows[i]["RES_CUR2"].ToString();
                        p_objDYResultArr[i].m_strRes_cur3 = dtbValue3.Rows[i]["RES_CUR3"].ToString();
                        p_objDYResultArr[i].m_strRes_cur4 = dtbValue3.Rows[i]["RES_CUR4"].ToString();
                        p_objDYResultArr[i].m_strRes_cur5 = dtbValue3.Rows[i]["RES_CUR5"].ToString();
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
			

			return 1;
		}


		/// <summary>
		/// 得到所有的检验结果的别名
		/// </summary>
		/// <param name="p_strAliasArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetAllLabCheckAlias(out clsPublicIDAndName[] p_strAliasArr)
		{
			p_strAliasArr=null;
			
//			//long lngCheckRes = new com.digitalwave.security.security.clsPrivilegeHandleService.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetAllLabCheckAlias");
//			////if (lngCheckRes <= 0)
//				//return lngCheckRes;
		
			DataTable dtbResult=null;
			long lngRes = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                lngRes = objHRPServ.DoGetDataTable(c_strGetAllLabCheckAliasSQL, ref dtbResult);
                if (lngRes <= 0) return lngRes;
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    p_strAliasArr = new clsPublicIDAndName[dtbResult.Rows.Count];
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        p_strAliasArr[i] = new clsPublicIDAndName();
                        p_strAliasArr[i].m_strName = dtbResult.Rows[i]["ITEM_ALIAS"].ToString();
                        p_strAliasArr[i].m_strID = dtbResult.Rows[i]["ITEM_ID"].ToString();
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

		/// <summary>
		/// 根据病历号获取检验项目
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_objPatientArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetCheckItem(
			string p_strInPatientID, out clsJY_BRZL[] p_objPatientArr)
		{
			p_objPatientArr = null;
			
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetCheckItem");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			if(p_strInPatientID == null || p_strInPatientID == "")
				return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
 
			//JY_BRZL
			objDPArr[0].Value = p_strInPatientID;

			DataTable dtbValue = new DataTable();

             try
            {
                long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetJY_BRZLSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    //记住，new完数组后还要new数组里的每个元素
                    p_objPatientArr = new clsJY_BRZL[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        //如果不new就是空引用了
                        p_objPatientArr[i] = new clsJY_BRZL();
                        p_objPatientArr[i].m_dtmPat_sdate = DateTime.Parse(dtbValue.Rows[i]["PAT_SDATE"].ToString());
                        p_objPatientArr[i].m_dtmPat_date = DateTime.Parse(dtbValue.Rows[i]["PAT_DATE"].ToString());
                        p_objPatientArr[i].m_strPat_age = dtbValue.Rows[i]["PAT_AGE"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_bed_no = dtbValue.Rows[i]["PAT_BED_NO"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_c_name = dtbValue.Rows[i]["PAT_C_NAME"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_chk = dtbValue.Rows[0]["PAT_CHK"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_d_name = dtbValue.Rows[i]["PAT_D_NAME"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_diag = dtbValue.Rows[i]["PAT_DIAG"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_doct = dtbValue.Rows[i]["PAT_DOCT"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_I_name = dtbValue.Rows[i]["PAT_I_NAME"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_id = dtbValue.Rows[i]["PAT_ID"].ToString().Trim().Trim();
                        p_objPatientArr[i].m_strPat_in_no = p_strInPatientID;
                        p_objPatientArr[i].m_strPat_mid = dtbValue.Rows[i]["PAT_MID"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_name = dtbValue.Rows[i]["PAT_NAME"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_rem = dtbValue.Rows[i]["PAT_REM"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_s_name = dtbValue.Rows[i]["PAT_S_NAME"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_sex = dtbValue.Rows[i]["PAT_SEX"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_sid = dtbValue.Rows[i]["PAT_SID"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_CheckOrder = dtbValue.Rows[i]["PAT_CHECKORDER"].ToString().Trim();
                        p_objPatientArr[i].m_strPat_rep_form = dtbValue.Rows[i]["PAT_REP_FORM"].ToString().Trim();
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
			

			return 1;
		}

		/// <summary>
		/// 根据检验号获取所有检验结果
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strPatID"></param>
		/// <param name="p_objResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetCheckResult(
			string p_strPatID,out clsJY_JG[] p_objResultArr)
		{
			p_objResultArr = null;
			
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetCheckResult");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			if(p_strPatID == null || p_strPatID == "")
				return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
 			//JY_JG
			objDPArr[0].Value = p_strPatID;

			DataTable dtbValue = new DataTable();

             try
            {
                 string SQL = @"select res_id,
       res_date,
       res_it_ecd,
       res_chr,
       res_chr1,
       res_exp,
       res_ref1,
       res_ref2,
       res_ref3,
       res_ref4,
       res_unit,
       res_name
  from jy_jg
 where res_id = ?";

                 objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                 objDPArr[0].Value = p_strPatID.Replace("'", "''");

                 long lngRes = objHRPServ.lngGetDataTableWithParameters(SQL, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objResultArr = new clsJY_JG[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_objResultArr[i] = new clsJY_JG();
                        p_objResultArr[i].m_strRes_id = dtbValue.Rows[i]["RES_ID"].ToString().Trim();
                        p_objResultArr[i].m_dtmRes_date = DateTime.Parse(dtbValue.Rows[i]["RES_DATE"].ToString());
                        p_objResultArr[i].m_strRes_chr = dtbValue.Rows[i]["RES_CHR"].ToString().Trim();
                        p_objResultArr[i].m_strRes_chr1 = dtbValue.Rows[i]["RES_CHR1"].ToString().Trim();
                        p_objResultArr[i].m_strRes_exp = dtbValue.Rows[i]["RES_EXP"].ToString().Trim();
                        p_objResultArr[i].m_strRes_it_ecd = dtbValue.Rows[i]["RES_IT_ECD"].ToString().Trim();
                        p_objResultArr[i].m_strRes_name = dtbValue.Rows[i]["RES_NAME"].ToString().Trim();
                        p_objResultArr[i].m_strRes_ref1 = dtbValue.Rows[i]["RES_REF1"].ToString().Trim();
                        p_objResultArr[i].m_strRes_ref2 = dtbValue.Rows[i]["RES_REF2"].ToString().Trim();
                        p_objResultArr[i].m_strRes_ref3 = dtbValue.Rows[i]["RES_REF3"].ToString().Trim();
                        p_objResultArr[i].m_strRes_ref4 = dtbValue.Rows[i]["RES_REF4"].ToString().Trim();
                        p_objResultArr[i].m_strRes_unit = dtbValue.Rows[i]["RES_UNIT"].ToString().Trim();
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
			

			return 1;
		}

		/// <summary>
		/// 根据检验号获取所有描述结果
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strPatID"></param>
		/// <param name="p_objResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetJY_MSJG(
			string p_strPatID,out clsJY_MSJG[] p_objResultArr)
		{
			p_objResultArr = null;
			
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetCheckResult");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			if(p_strPatID == null || p_strPatID == "")
				return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
 			//JY_MSJG
			objDPArr[0].Value = p_strPatID;

			DataTable dtbValue = new DataTable();

             try
            {
                long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetJY_MSJGContentSQL, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objResultArr = new clsJY_MSJG[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_objResultArr[i] = new clsJY_MSJG();
                        p_objResultArr[i].m_strRes_id = dtbValue.Rows[i]["RES_ID"].ToString().Trim();
                        p_objResultArr[i].m_dtmRes_date = DateTime.Parse(dtbValue.Rows[i]["RES_DATE"].ToString());
                        p_objResultArr[i].m_strRes_cname = dtbValue.Rows[i]["RES_CNAME"].ToString();
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
			

			return 1;
		}

		/// <summary>
		/// 根据检验号获取所有药敏结果
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strPatID"></param>
		/// <param name="p_objResult"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetJY_YMJG(
			string p_strPatID,out clsJY_YMJG[] p_objResultArr)
		{
			p_objResultArr = null;
			
			try
			{
				//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetCheckResult");
				////if (lngCheckRes <= 0)
					//return lngCheckRes;
			}
			catch(Exception objEx)
			{
				com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
				bool blnRes = objLogger.LogError(objEx);					
			}
			

			if(p_strPatID == null || p_strPatID == "")
				return (long)enmOperationResult.Parameter_Error;

            clsHRPTableService objHRPServ = new clsHRPTableService();
			IDataParameter[] objDPArr = null;
            objHRPServ.CreateDatabaseParameter(1, out objDPArr);
 			//JY_YMJG
			objDPArr[0].Value = p_strPatID;

			DataTable dtbValue = new DataTable();

             try
            {
                long lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetJY_YMJGContentSQL, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objResultArr = new clsJY_YMJG[dtbValue.Rows.Count];
                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_objResultArr[i] = new clsJY_YMJG();
                        p_objResultArr[i].m_strRes_id = dtbValue.Rows[i]["RES_ID"].ToString().Trim();
                        p_objResultArr[i].m_dtmRes_date = DateTime.Parse(dtbValue.Rows[i]["RES_DATE"].ToString());
                        p_objResultArr[i].m_strRes_barname = dtbValue.Rows[i]["RES_BARNAME"].ToString().Trim();
                        p_objResultArr[i].m_strRec_barcname = dtbValue.Rows[i]["RES_BARCNAME"].ToString().Trim();
                        p_objResultArr[i].m_strRes_bcnt = dtbValue.Rows[i]["RES_BCNT"].ToString().Trim();
                        p_objResultArr[i].m_strRes_antiname = dtbValue.Rows[i]["RES_ANTINAME"].ToString().Trim();
                        p_objResultArr[i].m_strRes_anticname = dtbValue.Rows[i]["RES_ANTICNAME"].ToString().Trim();
                        p_objResultArr[i].m_strRes_mic = dtbValue.Rows[i]["RES_MIC"].ToString().Trim();
                        p_objResultArr[i].m_strRes_smic = dtbValue.Rows[i]["RES_SMIC"].ToString().Trim();
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
			

			return 1;
		}

		/// <summary>
		/// 获取所有检验项目的结果
		/// </summary>
		/// <param name="p_objPrincipal"></param>
		/// <param name="p_strInPatientID"></param>
		/// <param name="p_strSendDate"></param>
		/// <param name="p_strItemNameArr"></param>
		/// <param name="p_objRecordContentArr"></param>
		/// <returns></returns>
		[AutoComplete]
		public long m_lngGetLabCheckItemResultArr(
			string  p_strResID,string[] p_strItemNameArr,
			out clsJY_JG[] p_objRecordContentArr)
		{
			p_objRecordContentArr = null;

//			//long lngCheckRes = new com.digitalwave.security.security.clsPrivilegeHandleService.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsLabAnalysisOrderService","m_lngGetLabCheckItemResultArr");
//			////if (lngCheckRes <= 0)
//				//return lngCheckRes;

			DataTable dtbValue = new DataTable();

			string strItemName="";
			if(p_strItemNameArr !=null)
				for(int i=0;i<p_strItemNameArr.Length;i++)
				{
					if(i==0)
						strItemName="'"+ p_strItemNameArr[i]+ "'";
					else 
						strItemName +=",'"+ p_strItemNameArr[i]+ "'";
				}

            string c_strGetLabCheckItemsResultSQL = @"select jg.res_id,
       jg.res_date,
       jg.res_it_ecd,
       jg.res_chr,
       jg.res_chr1,
       jg.res_exp,
       jg.res_ref1,
       jg.res_ref2,
       jg.res_ref3,
       jg.res_ref4,
       jg.res_unit,
       jg.res_name,
       alias.item_alias
  from jy_jg jg, labcheckaliasforjy alias
 where jg.res_it_ecd = alias.itm_ecd
   and alias.item_alias in (" +strItemName+@")
   and res_id='"+p_strResID+@"'";

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                long lngRes = objHRPServ.DoGetDataTable(c_strGetLabCheckItemsResultSQL, ref dtbValue);

                if (lngRes < 0 || dtbValue.Rows.Count == 0)
                    return 0;

                p_objRecordContentArr = new clsJY_JG[dtbValue.Rows.Count];

                for (int i = 0; i < dtbValue.Rows.Count; i++)
                {
                    p_objRecordContentArr[i] = new clsJY_JG();

                    if (dtbValue.Rows[i]["RES_DATE"].ToString() != "")
                        p_objRecordContentArr[i].m_dtmRes_date = DateTime.Parse(dtbValue.Rows[i]["RES_DATE"].ToString());
                    else
                        p_objRecordContentArr[i].m_dtmRes_date = DateTime.MinValue;
                    p_objRecordContentArr[i].m_strRes_chr = dtbValue.Rows[i]["RES_CHR"].ToString();
                    p_objRecordContentArr[i].m_strRes_chr1 = dtbValue.Rows[i]["RES_CHR1"].ToString();
                    p_objRecordContentArr[i].m_strRes_exp = dtbValue.Rows[i]["RES_EXP"].ToString();
                    p_objRecordContentArr[i].m_strRes_id = dtbValue.Rows[i]["RES_ID"].ToString();
                    p_objRecordContentArr[i].m_strRes_it_ecd = dtbValue.Rows[i]["RES_IT_ECD"].ToString();
                    /*使用别名*/
                    p_objRecordContentArr[i].m_strRes_name = dtbValue.Rows[i]["ITEM_ALIAS"].ToString();
                    p_objRecordContentArr[i].m_strRes_ref1 = dtbValue.Rows[i]["RES_REF1"].ToString();
                    p_objRecordContentArr[i].m_strRes_ref2 = dtbValue.Rows[i]["RES_REF2"].ToString();
                    p_objRecordContentArr[i].m_strRes_ref3 = dtbValue.Rows[i]["RES_REF3"].ToString();
                    p_objRecordContentArr[i].m_strRes_ref4 = dtbValue.Rows[i]["RES_REF4"].ToString();
                    p_objRecordContentArr[i].m_strRes_unit = dtbValue.Rows[i]["RES_UNIT"].ToString();

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
			
			
			return 1;
		}

		[AutoComplete]
		public long m_lngGetLabCheckItemChoiceArr(
			string  p_strInPatientID,DateTime p_dtmCreateDate,
			out clsJY_ItemChoice[] p_objRecordContentArr)
		{
			p_objRecordContentArr = null;			

			DataTable dtbValue = new DataTable();
			
			string c_strGetLabCheckItemChoiceSQL = @"select distinct  a.pat_c_name,a.pat_sdate,a.pat_id 
													from jy_brzl a inner join jy_jg b
													on a.pat_id =b. res_id
													where a.pat_in_no=? and b.res_date >=? and b.res_date <=?";

			long lngRes = -1;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_strInPatientID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmCreateDate.AddDays(-1);
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmCreateDate.AddDays(1);

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetLabCheckItemChoiceSQL, ref dtbValue, objDPArr);

                if (dtbValue.Rows.Count > 0)
                {
                    p_objRecordContentArr = new clsJY_ItemChoice[dtbValue.Rows.Count];

                    for (int i = 0; i < dtbValue.Rows.Count; i++)
                    {
                        p_objRecordContentArr[i] = new clsJY_ItemChoice();

                        if (dtbValue.Rows[i]["PAT_SDATE"].ToString() != "")
                            p_objRecordContentArr[i].m_dtmPat_sdate = DateTime.Parse(dtbValue.Rows[i]["PAT_SDATE"].ToString());
                        else
                            p_objRecordContentArr[i].m_dtmPat_sdate = DateTime.MinValue;
                        p_objRecordContentArr[i].m_strPat_c_name = dtbValue.Rows[i]["PAT_C_NAME"].ToString();
                        p_objRecordContentArr[i].m_strRes_id = dtbValue.Rows[i]["PAT_ID"].ToString();
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
	}
}
