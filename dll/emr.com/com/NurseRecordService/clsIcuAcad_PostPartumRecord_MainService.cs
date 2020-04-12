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
	/// 产后记录
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsPostPartumRecord_MainService : clsRecordsService
	{
        
		#region 更新数据库中的首次打印时间
		/// <summary>
		/// 更新数据库中的首次打印时间。
		/// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId">住院登记号</param>
		/// <param name="p_intRecordTypeArr">记录类型</param>
		/// <param name="p_dtmOpenDateArr">记录时间(与记录类型及其位置一一对应)</param>
		/// <param name="p_dtmFirstPrintDate">首次打印时间</param>
		/// <returns></returns>
		[AutoComplete]
		public override long m_lngUpdateFirstPrintDate(
            string p_strRegisterId,
			int[] p_intRecordTypeArr,
            DateTime[] p_dtmCreatedDateArr,
			DateTime p_dtmFirstPrintDate)
		{
			//long lngCheckRes = new com.digitalwave.security.clsPrivilegeHandleService().m_lngCheckCallPrivilege(p_objPrincipal,"clsPostartumSeeRecordMainService","m_lngUpdateFirstPrintDate");
			//if(lngCheckRes <= 0)
				//return lngCheckRes;	
			

			//检查参数
            if (string.IsNullOrEmpty(p_strRegisterId) || p_dtmFirstPrintDate == DateTime.MinValue)
				return (long)enmOperationResult.Parameter_Error;
            string strSql = @"update t_emr_postpartum_all
   set firstprintdate_dat = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";
			
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0 ; i < p_dtmCreatedDateArr.Length-1 ; i++)
                    {
                        objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                        objDPArr[0].DbType = DbType.DateTime;
                        objDPArr[0].Value = p_dtmFirstPrintDate;
                        objDPArr[1].Value = p_strRegisterId;
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = p_dtmCreatedDateArr[i];
                        //执行SQL
                        long lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                        //if (lngRes <= 0) return lngRes;
                    }
                }
                else
                {
                    //objDPArr = null;
                    //objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                    DbType[] dbTypes = new DbType[] { DbType.Date, DbType.String, DbType.Date };
                    string[][] strValues = new string[3][];
                    //ArrayList arlItems = new ArrayList(p_objItems.Length);
                    //for (int i1 = 0 ; i1 < p_dtmCreatedDateArr.Length ; i1++)
                    //{
                    //    arlItems.Add(p_objItems[i1]);
                    //}
                    if (p_dtmCreatedDateArr.Length > 0)
                    {
                        for (int j = 0 ; j < strValues.Length ; j++)
                        {
                            strValues[j] = new string[p_dtmCreatedDateArr.Length-1];
                        }
                        for (int k1 = 0 ; k1 < p_dtmCreatedDateArr.Length-1 ; k1++)
                        {
                            strValues[0][k1] = p_dtmFirstPrintDate.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        for (int k2 = 0 ; k2 < p_dtmCreatedDateArr.Length - 1 ; k2++)
                        {
                            strValues[1][k2] = p_strRegisterId;
                        }
                        for (int k3 = 0 ; k3 < p_dtmCreatedDateArr.Length - 1 ; k3++)
                        {
                            strValues[2][k3] = p_dtmCreatedDateArr[k3].ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        lngRes = objHRPServ.m_lngSaveArrayWithParameters(strSql, strValues, dbTypes);
                    }
                }
                strSql = @"update t_emr_postpartumannoall
   set firstprintdate_dat = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";
                objDPArr = null;
                objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_dtmFirstPrintDate;
                objDPArr[1].Value = p_strRegisterId;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = p_dtmCreatedDateArr[p_dtmCreatedDateArr.Length - 1];
                //执行SQL
                long lngEff2 = 0;
                long lngRes2 = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff2, objDPArr);
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex,true);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
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
        protected override long m_lngGetTransDataInfoArrWithServ(string p_strRegisterId, int p_intStatus, out clsTransDataInfo[] p_objTansDataInfoArr)
		{
            p_objTansDataInfoArr = null;
			long lngRes = -1;
			//检查参数
            if (string.IsNullOrEmpty(p_strRegisterId))
				return (long)enmOperationResult.Parameter_Error;
            string strSql = @"select a.postportum_num_chr,
       a.uterusbottom_chr,
       a.uteruspinch_chr,
       a.milknum_chr,
       a.breastbulge_chr,
       a.nipple_chr,
       a.dewnum_chr,
       a.dewcolor_chr,
       a.dewfuck_chr,
       a.perineum_chr,
       a.bp_chr,
       a.urine_chr,
       a.annotations_chr,
       a.postportum_num_chrxml,
       a.uterusbottom_chrxml,
       a.uteruspinch_chrxml,
       a.milknum_chrxml,
       a.breastbulge_chrxml,
       a.nipple_chrxml,
       a.dewnum_chrxml,
       a.dewcolor_chrxml,
       a.dewfuck_chrxml,
       a.perineum_chrxml,
       a.bp_chrxml,
       a.urine_chrxml,
       a.annotations_chrxml,
       a.registerid_chr,
       a.createdate_dat,
       a.createuserid_chr,
       a.ifconfirm_int,
       a.status_int,
       a.deactiveddate_dat,
       a.deactivedoperatorid_chr,
       a.firstprintdate_dat,
       a.recorduserid_vchr,
       a.recorddate_dat,
       a.sequence_int,
       b.modifydate,
       b.modifyuserid,
       b.postportum_num_chr_right,
       b.uterusbottom_chr_right,
       b.uteruspinch_chr_right,
       b.milknum_chr_right,
       b.breastbulge_chr_right,
       b.nipple_chr_right,
       b.dewnum_chr_right,
       b.dewcolor_chr_right,
       b.dewfuck_chr_right,
       b.perineum_chr_right,
       b.bp_chr_right,
       b.urine_chr_right,
       b.annotations_chr_right
  from t_emr_postpartum_all a
 inner join t_emr_postpartum_right b on a.registerid_chr = b.registerid_chr
                                    and a.createdate_dat = b.createdate_dat
 where a.status_int = 1
   and b.status_int = 1
   and a.registerid_chr = ?";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;

                //				DataTable dtbDetail = new DataTable();//引产观察记录内容
                DataTable dtbValue = new DataTable();//引产观察记录内容  

                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    ArrayList arlTransDataSub = new ArrayList(10);
                    ArrayList arlTransData = new ArrayList(10);
                    DateTime dtmOpenDate = DateTime.MinValue;
                    DataRow objRow = null;
                    for (int j = 0 ; j < dtbValue.Rows.Count ; j++)
                    {
                        objRow = dtbValue.Rows[j];
                        #region
                        //获取当前DataTable记录的OpenDate，记录在dtmOpenDate
                        dtmOpenDate = DateTime.Parse(objRow["RECORDDATE_DAT"].ToString()).Date;
                        while (j < dtbValue.Rows.Count && DateTime.Parse(objRow["RECORDDATE_DAT"].ToString()).Date == dtmOpenDate)
                        {
                            objRow = dtbValue.Rows[j];
                            #region 从DataTable.Rows中获取结果

                            clsIcuAcad_PostPartumRecord_Value objRecordContent = new clsIcuAcad_PostPartumRecord_Value();
                            objRecordContent.m_strRegisterID = p_strRegisterId;
                            DateTime dtmTemp = DateTime.MinValue;
                            DateTime.TryParse(objRow["CREATEDATE_DAT"].ToString(), out dtmTemp);
                            objRecordContent.m_dtmCreateDate = dtmTemp;

                            dtmTemp = DateTime.MinValue;
                            DateTime.TryParse(objRow["RECORDDATE_DAT"].ToString(), out dtmTemp);
                            objRecordContent.m_dtmRecordDate = dtmTemp;

                            dtmTemp = DateTime.MinValue;
                            DateTime.TryParse(objRow["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                            objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                            objRecordContent.m_strCreateUserID = objRow["CREATEUSERID_CHR"].ToString();
                            objRecordContent.m_strRecordUserID = objRow["RECORDUSERID_VCHR"].ToString();
                            objRecordContent.m_strModifyUserID = objRow["MODIFYUSERID"].ToString();

                            dtmTemp = DateTime.MinValue;
                            DateTime.TryParse(objRow["MODIFYDATE"].ToString(), out dtmTemp);
                            objRecordContent.m_dtmModifyDate = dtmTemp;


                            int intTemp = 0;
                            int.TryParse(objRow["IFCONFIRM_INT"].ToString(), out intTemp);
                            objRecordContent.m_bytIfConfirm = intTemp;
                            intTemp = 0;
                            int.TryParse(objRow["STATUS_INT"].ToString(), out intTemp);
                            objRecordContent.m_bytStatus = intTemp;

                            objRecordContent.m_strANNOTATIONS_CHR = objRow["ANNOTATIONS_CHR"].ToString();
                            objRecordContent.m_strANNOTATIONS_CHR_RIGHT = objRow["ANNOTATIONS_CHR_RIGHT"].ToString();
                            objRecordContent.m_strANNOTATIONS_CHRXML = objRow["ANNOTATIONS_CHRXML"].ToString();

                            objRecordContent.m_strBP_CHR = objRow["BP_CHR"].ToString();
                            objRecordContent.m_strBP_CHR_RIGHT = objRow["BP_CHR_RIGHT"].ToString();
                            objRecordContent.m_strBP_CHRXML = objRow["BP_CHRXML"].ToString();

                            objRecordContent.m_strBREASTBULGE_CHR = objRow["BREASTBULGE_CHR"].ToString();
                            objRecordContent.m_strBREASTBULGE_CHR_RIGHT = objRow["BREASTBULGE_CHR_RIGHT"].ToString();
                            objRecordContent.m_strBREASTBULGE_CHRXML = objRow["BREASTBULGE_CHRXML"].ToString();

                            objRecordContent.m_strDEWCOLOR_CHR = objRow["DEWCOLOR_CHR"].ToString();
                            objRecordContent.m_strDEWCOLOR_CHR_RIGHT = objRow["DEWCOLOR_CHR_RIGHT"].ToString();
                            objRecordContent.m_strDEWCOLOR_CHRXML = objRow["DEWCOLOR_CHRXML"].ToString();

                            objRecordContent.m_strDEWFUCK_CHR = objRow["DEWFUCK_CHR"].ToString();
                            objRecordContent.m_strDEWFUCK_CHR_RIGHT = objRow["DEWFUCK_CHR_RIGHT"].ToString();
                            objRecordContent.m_strDEWFUCK_CHRXML = objRow["DEWFUCK_CHRXML"].ToString();

                            objRecordContent.m_strDEWNUM_CHR = objRow["DEWNUM_CHR"].ToString();
                            objRecordContent.m_strDEWNUM_CHR_RIGHT = objRow["DEWNUM_CHR_RIGHT"].ToString();
                            objRecordContent.m_strDEWNUM_CHRXML = objRow["DEWNUM_CHRXML"].ToString();

                            objRecordContent.m_strMILKNUM_CHR = objRow["MILKNUM_CHR"].ToString();
                            objRecordContent.m_strMILKNUM_CHR_RIGHT = objRow["MILKNUM_CHR_RIGHT"].ToString();
                            objRecordContent.m_strMILKNUM_CHRXML = objRow["MILKNUM_CHRXML"].ToString();

                            objRecordContent.m_strNIPPLE_CHR = objRow["NIPPLE_CHR"].ToString();
                            objRecordContent.m_strNIPPLE_CHR_RIGHT = objRow["NIPPLE_CHR_RIGHT"].ToString();
                            objRecordContent.m_strNIPPLE_CHRXML = objRow["NIPPLE_CHRXML"].ToString();

                            objRecordContent.m_strPERINEUM_CHR = objRow["PERINEUM_CHR"].ToString();
                            objRecordContent.m_strPERINEUM_CHR_RIGHT = objRow["PERINEUM_CHR_RIGHT"].ToString();
                            objRecordContent.m_strPERINEUM_CHRXML = objRow["PERINEUM_CHRXML"].ToString();

                            objRecordContent.m_strPOSTPORTUM_NUM_CHR = objRow["POSTPORTUM_NUM_CHR"].ToString();
                            objRecordContent.m_strPOSTPORTUM_NUM_CHR_RIGHT = objRow["POSTPORTUM_NUM_CHR_RIGHT"].ToString();
                            objRecordContent.m_strPOSTPORTUM_NUM_CHRXML = objRow["POSTPORTUM_NUM_CHRXML"].ToString();

                            objRecordContent.m_strURINE_CHR = objRow["URINE_CHR"].ToString();
                            objRecordContent.m_strURINE_CHR_RIGHT = objRow["URINE_CHR_RIGHT"].ToString();
                            objRecordContent.m_strURINE_CHRXML = objRow["URINE_CHRXML"].ToString();

                            objRecordContent.m_strUTERUSBOTTOM_CHR = objRow["UTERUSBOTTOM_CHR"].ToString();
                            objRecordContent.m_strUTERUSBOTTOM_CHR_RIGHT = objRow["UTERUSBOTTOM_CHR_RIGHT"].ToString();
                            objRecordContent.m_strUTERUSBOTTOM_CHRXML = objRow["UTERUSBOTTOM_CHRXML"].ToString();

                            objRecordContent.m_strUTERUSPINCH_CHR = objRow["UTERUSPINCH_CHR"].ToString();
                            objRecordContent.m_strUTERUSPINCH_CHR_RIGHT = objRow["UTERUSPINCH_CHR_RIGHT"].ToString();
                            objRecordContent.m_strUTERUSPINCH_CHRXML = objRow["UTERUSPINCH_CHRXML"].ToString();

                            //获取签名集合
                            long lngS = 0;
                            if (long.TryParse(objRow["SEQUENCE_INT"].ToString(), out lngS))
                            {
                                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                                long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                                //释放
                                objSign = null;
                            }
                            j++;
                            arlTransDataSub.Add(objRecordContent);
                            #endregion
                        }
                        j--;
                        #endregion
                        clsPostPartumArr objdateInfo = new clsPostPartumArr();
                        objdateInfo.m_objRecordArr = (clsIcuAcad_PostPartumRecord_Value[])arlTransDataSub.ToArray(typeof(clsIcuAcad_PostPartumRecord_Value));
                        objdateInfo.m_objRecordContent = objdateInfo.m_objRecordArr[objdateInfo.m_objRecordArr.Length - 1];
                        arlTransData.Add(objdateInfo);
                        arlTransDataSub.Clear();
                    }
                    clsIcuAcad_PostPartumContentValueContentDataInfo[] objTansDataInfoArr = new clsIcuAcad_PostPartumContentValueContentDataInfo[1];
                    objTansDataInfoArr[0] = new clsIcuAcad_PostPartumContentValueContentDataInfo();
                    objTansDataInfoArr[0].m_objPostPartumValues = (clsPostPartumArr[])arlTransData.ToArray(typeof(clsPostPartumArr));
                    p_objTansDataInfoArr = objTansDataInfoArr;
                }
                clsPostPartumManno_Value objPostPartumManno = null;
                lngRes = m_lngGetRecord( p_strRegisterId, out objPostPartumManno);
                if (lngRes > 0 && objPostPartumManno != null)
                {
                    clsIcuAcad_PostPartumContentValueContentDataInfo objjTansDataInfo = null;
                    if (p_objTansDataInfoArr != null && p_objTansDataInfoArr.Length > 0)
                        objjTansDataInfo = (clsIcuAcad_PostPartumContentValueContentDataInfo)p_objTansDataInfoArr[0];
                    else
                        objjTansDataInfo = new clsIcuAcad_PostPartumContentValueContentDataInfo();
                    objjTansDataInfo.m_objMannoValue = objPostPartumManno;

                    p_objTansDataInfoArr = new clsTransDataInfo[] { objjTansDataInfo };
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx,true);
                return 0;
            }
            finally
            {
                objHRPServ = null;
                //objHRPServ.Dispose();

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

            string strSQL = @"select t2.modifydate, t2.modifyuserid
  from t_emr_postpartum_all t1, t_emr_postpartum_right t2
 where t1.registerid_chr = t2.registerid_chr
   and t1.createdate_dat = t2.createdate_dat
   and t1.status_int = 1
   and t2.status_int = 1
   and t1.registerid_chr = ?
   and t1.createdate_dat = ? ";

				
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;
                //使用strSQL生成DataTable
                DataTable dtbValue = new DataTable();
                //执行查询，填充结果到DataTable            
                lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                //如果DataTable.Rows.Count等于0，代表查找的内容已经被删除，返回Record_Already_Delete                                 
                if (lngRes > 0 && dtbValue.Rows.Count == 0)
                {
                    strSQL = @"select t.deactiveddate_dat, t.deactivedoperatorid_chr
  from t_emr_postpartum_all t
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 0";
                    objDPArr = null;
                    p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_objRecordContent.m_strRegisterID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;

                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID_CHR"].ToString();
                        p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["DEACTIVEDDATE_DAT"].ToString());
                    }
                    return (long)enmOperationResult.Record_Already_Delete;
                }
                //从DataTable中获取ModifyDate，使之于p_objRecordContent.m_dtmModifyDate比较
                else if (lngRes > 0 && dtbValue.Rows.Count == 1)
                {
                    //如果相同，返回DB_Succees
                    if (p_objRecordContent.m_dtmModifyDate == DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString()))
                        return (long)enmOperationResult.DB_Succeed;

                    //否则，返回Record_Already_Modify
                    p_objModifyInfo = new clsPreModifyInfo();
                    p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["MODIFYUSERID"].ToString();
                    p_objModifyInfo.m_dtmActionTime = DateTime.Parse(dtbValue.Rows[0]["MODIFYDATE"].ToString());
                    return (long)enmOperationResult.Record_Already_Modify;
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
                objHRPServ = null;
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
			if(p_objRecordContent==null || p_objRecordContent.m_strInPatientID==null)
				return (long)enmOperationResult.Parameter_Error;
			long lngRes = 0;
            string strSql = @"update t_emr_postpartum_all t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_chr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 1";
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmCreateDate;

                //执行SQL
                long lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);

                strSql = @"update t_emr_postpartumannoall t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_chr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 1";
                objDPArr = null;
                p_objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmCreateDate;

                //执行SQL
                lngEff = 0;
                lngRes = p_objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            } 
            return lngRes;
		}
		#endregion

        #region 附注内容的添加修改删除操作

        #region Get
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRecord(string p_strRegisterId,out clsPostPartumManno_Value p_objRecordContent)
        {
            p_objRecordContent = null;
            //检查参数                              
            if (string.IsNullOrEmpty(p_strRegisterId))
                return (long)enmOperationResult.Parameter_Error;
            string strSql = @"select a.registerid_chr,
       a.createdate_dat,
       a.status_int,
       a.totalbloodnum_vchr,
       a.totalbloodnumxml_vchr,
       a.sewpin_vchr,
       a.sewpinxml_chr,
       a.period_vchr,
       a.periodxml_vchr,
       a.specialrecord_vchr,
       a.especialrecordxml_vchr,
       a.sequence_int,
       a.recorduserid_vchr,
       a.recorddate_dat,
       a.createuserid_chr,
       a.firstprintdate_dat,
       a.deactiveddate_dat,
       a.deactivedoperatorid_chr,
       a.ifconfirm_int,
       b.modifydate_dat,
       b.modifyuserid_chr,
       b.totalbloodnumright_int,
       b.sewpinright_int,
       b.periodright_vchr,
       b.specialrecordtight_vchr,
       b.childbirthingdate_dat
  from t_emr_postpartumannoall a
 inner join t_emr_postpartumannoa_r b on a.registerid_chr = b.registerid_chr
                                    and a.createdate_dat = b.createdate_dat
 where a.status_int = 1
   and b.status_int = 1
   and a.registerid_chr = ?";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;

                DataTable dtbValue = new DataTable();
                DataRow objRow = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSql, ref dtbValue, objDPArr);
                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    p_objRecordContent = new clsPostPartumManno_Value();
                    objRow = dtbValue.Rows[0];
                    #region 
                    p_objRecordContent.m_strRegisterID = p_strRegisterId;
                    DateTime dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(objRow["CREATEDATE_DAT"].ToString(), out dtmTemp);
                    p_objRecordContent.m_dtmCreateDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(objRow["RECORDDATE_DAT"].ToString(), out dtmTemp);
                    p_objRecordContent.m_dtmRecordDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(objRow["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                    p_objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                    p_objRecordContent.m_strCreateUserID = objRow["CREATEUSERID_CHR"].ToString();
                    p_objRecordContent.m_strRecordUserID = objRow["RECORDUSERID_VCHR"].ToString();
                    p_objRecordContent.m_strModifyUserID = objRow["MODIFYUSERID_CHR"].ToString();

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(objRow["MODIFYDATE_DAT"].ToString(), out dtmTemp);
                    p_objRecordContent.m_dtmModifyDate = dtmTemp;

                    dtmTemp = DateTime.MinValue;
                    DateTime.TryParse(objRow["CHILDBIRTHINGDATE_DAT"].ToString(), out dtmTemp);
                    p_objRecordContent.m_dtmChildBirthingDate = dtmTemp;

                    int intTemp = 0;
                    int.TryParse(objRow["IFCONFIRM_INT"].ToString(), out intTemp);
                    p_objRecordContent.m_bytIfConfirm = intTemp;
                    intTemp = 0;
                    int.TryParse(objRow["STATUS_INT"].ToString(), out intTemp);
                    p_objRecordContent.m_bytStatus = intTemp;

                    p_objRecordContent.m_strESPECIALRECORD_CHR = objRow["SPECIALRECORD_VCHR"].ToString();
                    p_objRecordContent.m_strESPECIALRECORD_CHR_RIGHT = objRow["SPECIALRECORDTIGHT_VCHR"].ToString();
                    p_objRecordContent.m_strESPECIALRECORD_CHRXML = objRow["ESPECIALRECORDXML_VCHR"].ToString();

                    p_objRecordContent.m_strPERIOD_CHR = objRow["PERIOD_VCHR"].ToString();
                    p_objRecordContent.m_strPERIOD_CHR_RIGHT = objRow["PERIODRIGHT_VCHR"].ToString();
                    p_objRecordContent.m_strPERIOD_CHRXML = objRow["PERIODXML_VCHR"].ToString();

                    p_objRecordContent.m_strSEWPIN_CHR = objRow["SEWPIN_VCHR"].ToString();
                    p_objRecordContent.m_strSEWPIN_CHR_RIGHT = objRow["SEWPINRIGHT_INT"].ToString();
                    p_objRecordContent.m_strSEWPIN_CHRXML = objRow["SEWPINXML_CHR"].ToString();

                    p_objRecordContent.m_strTOTALBLOODNUM_CHR = objRow["TOTALBLOODNUM_VCHR"].ToString();
                    p_objRecordContent.m_strTOTALBLOODNUM_CHR_RIGHT = objRow["TOTALBLOODNUMRIGHT_INT"].ToString();
                    p_objRecordContent.m_strTOTALBLOODNUM_CHRXML = objRow["TOTALBLOODNUMXML_VCHR"].ToString();

                    //获取签名集合
                    long lngS = 0;
                    if (long.TryParse(objRow["SEQUENCE_INT"].ToString(), out lngS))
                    {
                        com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign2 = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                        long lngTemp = objSign2.m_lngGetSign(lngS, out p_objRecordContent.objSignerArr);

                        //释放
                        objSign2 = null;
                    }
                    #endregion 
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
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion Get

        #region 添加
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAddNewRecord(clsPostPartumManno_Value p_objRecordContent)
        {
            //检查参数                              
            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;
            string strSql = @"insert into t_emr_postpartumannoall
  (registerid_chr,
   createdate_dat,
   status_int,
   totalbloodnum_vchr,
   totalbloodnumxml_vchr,
   sewpin_vchr,
   sewpinxml_chr,
   period_vchr,
   periodxml_vchr,
   specialrecord_vchr,
   especialrecordxml_vchr,
   sequence_int,
   recorduserid_vchr,
   recorddate_dat,
   createuserid_chr,
   ifconfirm_int)
values
  (?,?,1,?,?,?,?,?,?,?,?,?,?,?,?,0)";//14
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                #region 付值
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(14, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[1].DbType = DbType.Date;
                objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;
                objDPArr[2].Value = p_objRecordContent.m_strTOTALBLOODNUM_CHR;
                objDPArr[3].Value = p_objRecordContent.m_strTOTALBLOODNUM_CHRXML;
                objDPArr[4].Value = p_objRecordContent.m_strSEWPIN_CHR;
                objDPArr[5].Value = p_objRecordContent.m_strSEWPIN_CHRXML;
                objDPArr[6].Value = p_objRecordContent.m_strPERIOD_CHR;
                objDPArr[7].Value = p_objRecordContent.m_strPERIOD_CHRXML;
                objDPArr[8].Value = p_objRecordContent.m_strESPECIALRECORD_CHR;
                objDPArr[9].Value = p_objRecordContent.m_strESPECIALRECORD_CHRXML;

                //objDPArr[10].DbType = DbType.Date;
                //objDPArr[10].Value = p_objRecordContent.m_dtmChildBirthingDate;
                objDPArr[10].Value = lngSequence;
                objDPArr[11].Value = p_objRecordContent.m_strRecordUserID;
                objDPArr[12].DbType = DbType.Date;
                objDPArr[12].Value = p_objRecordContent.m_dtmRecordDate;
                objDPArr[13].Value = p_objRecordContent.m_strCreateUserID;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(p_objRecordContent.objSignerArr, lngSequence);

                lngRes = m_lngAddRecordRight(p_objRecordContent);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }
            return lngRes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngAddRecordRight(clsPostPartumManno_Value p_objRecordContent)
        {
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
                string strSql = @"insert into t_emr_postpartumannoa_r
  (registerid_chr,
   createdate_dat,
   status_int,
   modifydate_dat,
   modifyuserid_chr,
   totalbloodnumright_int,
   sewpinright_int,
   periodright_vchr,
   specialrecordtight_vchr,
   childbirthingdate_dat)
values
  (?,?,1,?,?,?,?,?,?,?)";//9
            try
            {
                IDataParameter[] objDPArr2 = null;
                objHRPServ.CreateDatabaseParameter(9, out objDPArr2);
                objDPArr2[0].Value = p_objRecordContent.m_strRegisterID;
                objDPArr2[1].DbType = DbType.Date;
                objDPArr2[1].Value = p_objRecordContent.m_dtmCreateDate;
                objDPArr2[2].DbType = DbType.Date;
                objDPArr2[2].Value = p_objRecordContent.m_dtmModifyDate;
                objDPArr2[3].Value = p_objRecordContent.m_strModifyUserID;
                objDPArr2[4].Value = p_objRecordContent.m_strTOTALBLOODNUM_CHR_RIGHT;
                objDPArr2[5].Value = p_objRecordContent.m_strSEWPIN_CHR_RIGHT;
                objDPArr2[6].Value = p_objRecordContent.m_strPERIOD_CHR_RIGHT;
                objDPArr2[7].Value = p_objRecordContent.m_strESPECIALRECORD_CHR_RIGHT;
                objDPArr2[8].DbType = DbType.Date;
                objDPArr2[8].Value = p_objRecordContent.m_dtmChildBirthingDate;
                #endregion
                long lngEff = 0;
                //执行SQL			
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr2);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion 添加

        #region 修改
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngModifyRecord( clsPostPartumManno_Value p_objRecordContent)
        {
            //检查参数
            if (p_objRecordContent == null || p_objRecordContent.m_dtmCreateDate == DateTime.MinValue || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;
            string strSql = @"update t_emr_postpartumannoall
   set totalbloodnum_vchr = ?,
       totalbloodnumxml_vchr = ?,
       sewpin_vchr = ?,
       sewpinxml_chr = ?,
       period_vchr = ?,
       periodxml_vchr = ?,
       specialrecord_vchr = ?,
       especialrecordxml_vchr = ?,
       sequence_int = ?,
       recorduserid_vchr = ?,
       recorddate_dat = ?
 where registerid_chr = ?
   and createdate_dat = ?
   and status_int = 1";//14
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取签名流水号
                long lngSequence = 0;
                com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                lngRes = objSign.m_lngGetSequenceValue("SEQ_EMR_SIGN", out lngSequence);

                #region set value
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(13, out objDPArr);
                objDPArr[0].Value = p_objRecordContent.m_strTOTALBLOODNUM_CHR;
                objDPArr[1].Value = p_objRecordContent.m_strTOTALBLOODNUM_CHRXML;
                objDPArr[2].Value = p_objRecordContent.m_strSEWPIN_CHR;
                objDPArr[3].Value = p_objRecordContent.m_strSEWPIN_CHRXML;
                objDPArr[4].Value = p_objRecordContent.m_strPERIOD_CHR;
                objDPArr[5].Value = p_objRecordContent.m_strPERIOD_CHRXML;
                objDPArr[6].Value = p_objRecordContent.m_strESPECIALRECORD_CHR;
                objDPArr[7].Value = p_objRecordContent.m_strESPECIALRECORD_CHRXML;

                //objDPArr[8].DbType = DbType.Date;
                //objDPArr[8].Value = p_objRecordContent.m_dtmChildBirthingDate;
                objDPArr[8].Value = lngSequence;
                objDPArr[9].Value = p_objRecordContent.m_strRecordUserID;
                objDPArr[10].DbType = DbType.Date;
                objDPArr[10].Value = p_objRecordContent.m_dtmRecordDate;
                objDPArr[11].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[12].DbType = DbType.Date;
                objDPArr[12].Value = p_objRecordContent.m_dtmCreateDate;

                #endregion
                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
                if (lngRes <= 0) return lngRes;

                //保存签名集合
                lngRes = objSign.m_lngAddSign(p_objRecordContent.objSignerArr, lngSequence);

                #region set value
                //置旧记录状态为0
                lngRes = m_lngDeleteContentInfo(p_objRecordContent.m_strRegisterID, p_objRecordContent.m_dtmCreateDate);
                //插入新纪录
                lngRes = m_lngAddRecordRight(p_objRecordContent);

                #endregion
            }
            catch (Exception objEx)
            {

                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            finally
            {
                //objHRPServ.Dispose();

            } return lngRes;
        }
        /// <summary>
        /// 置旧记录状态为0
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="p_dtmCreatedDate"></param>
        /// <returns></returns>
        [AutoComplete]
        private long m_lngDeleteContentInfo(string p_strRegisterId, DateTime p_dtmCreatedDate)
        {
            if (string.IsNullOrEmpty(p_strRegisterId) || p_dtmCreatedDate == DateTime.MaxValue)
                return -1;
            string strSql = @" update t_emr_postpartumannoa_r t set t.status_int = 0
 where t.registerid_chr = ? and t.createdate_dat = ?";
            long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = p_dtmCreatedDate;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;
            }
            return lngRes;
        }
        #endregion 修改

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objRecordContent"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteRecord( clsPostPartumManno_Value p_objRecordContent)
        {
            //检查参数
            if (p_objRecordContent == null || string.IsNullOrEmpty(p_objRecordContent.m_strRegisterID))
                return (long)enmOperationResult.Parameter_Error;
            string strSql = @"update t_emr_postpartumannoall t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_chr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 1";
             long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;//new Oracle.DataAccess.Client.OracleParameter[5];
                objHRPServ.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = p_objRecordContent.m_dtmDeActivedDate;
                objDPArr[1].Value = p_objRecordContent.m_strDeActivedOperatorID;
                objDPArr[2].Value = p_objRecordContent.m_strRegisterID;
                objDPArr[3].DbType = DbType.DateTime;
                objDPArr[3].Value = p_objRecordContent.m_dtmCreateDate;

                //执行SQL
                long lngEff = 0;
                lngRes = objHRPServ.lngExecuteParameterSQL(strSql, ref lngEff, objDPArr);

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                //objHRPServ.Dispose();
                objHRPServ = null;

            } return lngRes;
        }
        #endregion 删除

        #endregion 附注内容的添加修改删除操作
    }
}
