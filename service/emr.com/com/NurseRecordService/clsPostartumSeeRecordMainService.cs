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
	/// 中期妊娠引产后观察记录  中间件
	/// </summary>
	[Transaction(TransactionOption.Required)]
	[ObjectPooling(true)]
	public class clsPostartumSeeRecordMainService: clsRecordsService
	{
		public clsPostartumSeeRecordMainService()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region SQL语句
        private const string c_strUpdateFirstPrintDateSQL = @"update icuacad_postpartumseerecord t
   set t.firstprintdate_dat = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and t.firstprintdate_dat is null
   and t.status_int = 1";

        private const string c_strGetRecordContentSQL = @"select distinct f_getempnameandtechbyid(t1.createuserid_vchr) as createusername,
                t1.createdate_dat,
                t1.createuserid_vchr,
                t1.deactiveddate_dat,
                t1.deactivedoperatorid_vchr,
                t1.firstprintdate_dat,
                t1.sequence_int,
                t1.status_int,
                t1.recorduserid_vchr,
                t1.recorddate_dat,
                t1.ifconfirm_int,
                t1.bloodpressure_chr,
                t1.bodytemparture_chr,
                t1.pulse_chr,
                t1.uterus_chr,
                t1.blooded_chr,
                t1.breakwater_chr,
                t1.embryo_chr,
                t1.uterussize_chr,
                t1.bloodpressure_chrxml,
                t1.bodytemparture_chrxml,
                t1.pulse_chrxml,
                t1.uterus_chrxml,
                t1.blooded_chrxml,
                t1.breakwater_chrxml,
                t1.embryo_chrxml,
                t1.uterussize_chrxml,
                t1.registerid_chr,
                t1.RECORDDATE,
                t3.status_int,
                t3.modifydate,
                t3.modifyuserid,
                t3.bloodpressure_chr_right,
                t3.bodytemparture_chr_right,
                t3.pulse_chr_right,
                t3.uterus_chr_right,
                t3.blooded_chr_right,
                t3.breakwater_chr_right,
                t3.embryo_chr_right,
                t3.uterussize_chr_right
  from icuacad_postpartumseerecord t1, icuacad_postpartumseecontent t3
 where t1.registerid_chr = t3.registerid_chr
   and t1.createdate_dat = t3.createdate_dat
   and t3.status_int = 1
   and t1.status_int = 1
   and t1.registerid_chr = ?
 order by t1.createdate_dat";

        private const string c_strGetRecordContentSQL_Single = @"select distinct f_getempnameandtechbyid(t1.createuserid_vchr) as createusername,
                t1.createdate_dat,
                t1.createuserid_vchr,
                t1.deactiveddate_dat,
                t1.deactivedoperatorid_vchr,
                t1.firstprintdate_dat,
                t1.sequence_int,
                t1.status_int,
                t1.recorduserid_vchr,
                t1.recorddate_dat,
                t1.ifconfirm_int,
                t1.bloodpressure_chr,
                t1.bodytemparture_chr,
                t1.pulse_chr,
                t1.uterus_chr,
                t1.blooded_chr,
                t1.breakwater_chr,
                t1.embryo_chr,
                t1.uterussize_chr,
                t1.bloodpressure_chrxml,
                t1.bodytemparture_chrxml,
                t1.pulse_chrxml,
                t1.uterus_chrxml,
                t1.blooded_chrxml,
                t1.breakwater_chrxml,
                t1.embryo_chrxml,
                t1.uterussize_chrxml,             
                t1.registerid_chr,
                t1.RECORDDATE,
                t3.status_int,
                t3.modifydate,
                t3.modifyuserid,
                t3.bloodpressure_chr_right,
                t3.bodytemparture_chr_right,
                t3.pulse_chr_right,
                t3.uterus_chr_right,
                t3.blooded_chr_right,
                t3.breakwater_chr_right,
                t3.embryo_chr_right,
                t3.uterussize_chr_right
  from icuacad_postpartumseerecord t1, icuacad_postpartumseecontent t3
 where t1.registerid_chr = t3.registerid_chr
   and t1.createdate_dat = t3.createdate_dat
   and t3.status_int = 1
   and t1.status_int = 1
   and t1.registerid_chr = ?
   and t1.createdate_dat = ?
 order by t1.createdate_dat";

        private const string c_strGetDeleteRecordSQL = @"select t.deactiveddate_dat, t.deactivedoperatorid_vchr
  from icuacad_postpartumseerecord t
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 0";

        private const string c_strDeleteRecordSQL = @"update icuacad_postpartumseerecord t
   set status_int                 = 0,
       t.deactiveddate_dat        = ?,
       t.deactivedoperatorid_vchr = ?
 where t.registerid_chr = ?
   and t.createdate_dat = ?
   and status_int = 1";
		#endregion
        
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
			
			
			long lngRes = 0;
			clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                //获取IDataParameter数组
                IDataParameter[] objDPArr = null;
                if (clsHRPTableService.bytDatabase_Selector != (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                {
                    for (int i = 0 ; i < p_dtmCreatedDateArr.Length ; i++)
                    {
                        objHRPServ.CreateDatabaseParameter(3, out objDPArr);
                        objDPArr[0].DbType = DbType.DateTime;
                        objDPArr[0].Value = p_dtmFirstPrintDate;
                        objDPArr[1].Value = p_strRegisterId;
                        objDPArr[2].DbType = DbType.DateTime;
                        objDPArr[2].Value = p_dtmCreatedDateArr[i];
                        //执行SQL
                        long lngEff = 0;
                        lngRes = objHRPServ.lngExecuteParameterSQL(c_strUpdateFirstPrintDateSQL, ref lngEff, objDPArr);
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
                            strValues[j] = new string[p_dtmCreatedDateArr.Length];
                        }
                        for (int k1 = 0 ; k1 < p_dtmCreatedDateArr.Length ; k1++)
                        {
                            strValues[0][k1] = p_dtmFirstPrintDate.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        for (int k2 = 0 ; k2 < p_dtmCreatedDateArr.Length ; k2++)
                        {
                            strValues[1][k2] = p_strRegisterId;
                        }
                        for (int k3 = 0 ; k3 < p_dtmCreatedDateArr.Length ; k3++)
                        {
                            strValues[2][k3] = p_dtmCreatedDateArr[k3].ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        lngRes = objHRPServ.m_lngSaveArrayWithParameters(c_strUpdateFirstPrintDateSQL, strValues, dbTypes);
                    }
                }
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex,true);
            }
            finally
            {
                //objHRPServ.Dispose();

            }
 
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
        public long m_lngGetRecordContent(string p_strRegisterId,string p_strCreatedDate,
            out clsIcuACAD_PostPartumseeRecord_VO p_objTansDataInfo)
		{
			p_objTansDataInfo=null;
			//检查参数
            if (string.IsNullOrEmpty(p_strRegisterId) || string.IsNullOrEmpty(p_strCreatedDate))
				return (long)enmOperationResult.Parameter_Error;
			
			long lngRes = 0;
            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;
                objDPArr[1].DbType = DbType.DateTime;
                objDPArr[1].Value = DateTime.Parse(p_strCreatedDate);

                DataTable dtbContent = new DataTable();

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL_Single, ref dtbContent, objDPArr);
                if (lngRes > 0 && dtbContent.Rows.Count > 0)
                {
                    clsIcuACAD_PostPartumseeRecord_VO objRecordContent = null;
                    for (int i = 0; i < dtbContent.Rows.Count; i++)
                    {
                        #region set values
                        objRecordContent = new clsIcuACAD_PostPartumseeRecord_VO();

                        objRecordContent.m_strRegisterID = p_strRegisterId;
                        DateTime dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbContent.Rows[i]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                        objRecordContent.m_dtmCreateDate = dtmTemp;
                        
                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbContent.Rows[i]["MODIFYDATE"].ToString(), out dtmTemp);
                        objRecordContent.m_dtmModifyDate = dtmTemp;

                        //objRecordContent.m_dtmOpenDate = DateTime.Parse(dtbContent.Rows[i]["OpenDate"].ToString());

                        dtmTemp = DateTime.MinValue;
                        DateTime.TryParse(dtbContent.Rows[i]["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                        objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                        objRecordContent.m_strCreateUserID = dtbContent.Rows[i]["CREATEUSERID_VCHR"].ToString();
                        objRecordContent.m_strRecordUserID = dtbContent.Rows[i]["RECORDUSERID_VCHR"].ToString();
                        objRecordContent.m_strModifyUserID = dtbContent.Rows[i]["MODIFYUSERID"].ToString();

                        int intTemp = 0;
                        int.TryParse(dtbContent.Rows[i]["IFCONFIRM_INT"].ToString(), out intTemp);
                        objRecordContent.m_bytIfConfirm = intTemp;

                        intTemp = 0;
                        int.TryParse(dtbContent.Rows[i]["STATUS_INT"].ToString(), out intTemp);
                        objRecordContent.m_bytStatus = intTemp;
                        
                        objRecordContent.m_strBLOODPRESSURE_CHR = dtbContent.Rows[i]["BLOODPRESSURE_CHR"].ToString();
                        objRecordContent.m_strBLOODPRESSURE_CHR_RIGHT = dtbContent.Rows[i]["BLOODPRESSURE_CHR_RIGHT"].ToString();
                        objRecordContent.m_strBLOODPRESSURE_CHRXML = dtbContent.Rows[i]["BLOODPRESSURE_CHRXML"].ToString();

                        objRecordContent.m_strBODYTEMPARTURE_CHR = dtbContent.Rows[i]["BODYTEMPARTURE_CHR"].ToString();
                        objRecordContent.m_strBODYTEMPARTURE_CHR_RIGHT = dtbContent.Rows[i]["BODYTEMPARTURE_CHR_RIGHT"].ToString();
                        objRecordContent.m_strBODYTEMPARTURE_CHRXML = dtbContent.Rows[i]["BODYTEMPARTURE_CHRXML"].ToString();

                        objRecordContent.m_strPULSE_CHR = dtbContent.Rows[i]["PULSE_CHR"].ToString();
                        objRecordContent.m_strPULSE_CHR_RIGHT = dtbContent.Rows[i]["PULSE_CHR_RIGHT"].ToString();
                        objRecordContent.m_strPULSE_CHRXML = dtbContent.Rows[i]["PULSE_CHRXML"].ToString();

                        objRecordContent.m_strUTERUS_CHR = dtbContent.Rows[i]["UTERUS_CHR"].ToString();
                        objRecordContent.m_strUTERUS_CHR_RIGHT = dtbContent.Rows[i]["UTERUS_CHR_RIGHT"].ToString();
                        objRecordContent.m_strUTERUS_CHRXML = dtbContent.Rows[i]["UTERUS_CHRXML"].ToString();

                        objRecordContent.m_strBLOODED_CHR = dtbContent.Rows[i]["BLOODED_CHR"].ToString();
                        objRecordContent.m_strBLOODED_CHR_RIGHT = dtbContent.Rows[i]["BLOODED_CHR_RIGHT"].ToString();
                        objRecordContent.m_strBLOODED_CHRXML = dtbContent.Rows[i]["BLOODED_CHRXML"].ToString();

                        objRecordContent.m_strBREAKWATER_CHR = dtbContent.Rows[i]["BREAKWATER_CHR"].ToString();
                        objRecordContent.m_strBREAKWATER_CHR_RIGHT = dtbContent.Rows[i]["BREAKWATER_CHR_RIGHT"].ToString();
                        objRecordContent.m_strBREAKWATER_CHRXML = dtbContent.Rows[i]["BREAKWATER_CHRXML"].ToString();

                        objRecordContent.m_strEMBRYO_CHR = dtbContent.Rows[i]["EMBRYO_CHR"].ToString();
                        objRecordContent.m_strEMBRYO_CHR_RIGHT = dtbContent.Rows[i]["EMBRYO_CHR_RIGHT"].ToString();
                        objRecordContent.m_strEMBRYO_CHRXML = dtbContent.Rows[i]["EMBRYO_CHRXML"].ToString();


                        objRecordContent.m_strUTERUSSIZE_CHR = dtbContent.Rows[i]["UTERUSSIZE_CHR"].ToString();
                        objRecordContent.m_strUTERUSSIZE_CHR_RIGHT = dtbContent.Rows[i]["UTERUSSIZE_CHR_RIGHT"].ToString();
                        objRecordContent.m_strUTERUSSIZE_CHRXML = dtbContent.Rows[i]["UTERUSSIZE_CHRXML"].ToString();
                        objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbContent.Rows[i]["RECORDDATE"]);
                        //获取签名集合
                        long lngS = 0;
                        if (long.TryParse(dtbContent.Rows[0]["SEQUENCE_INT"].ToString(), out lngS))
                        {
                            com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objSign = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier();
                            long lngTemp = objSign.m_lngGetSign(lngS, out objRecordContent.objSignerArr);

                            //释放
                            objSign = null;
                        }

                        p_objTansDataInfo = objRecordContent;
                        #endregion
                    }
                }

            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                return 0;
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

            clsHRPTableService objHRPServ = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strRegisterId;

                //				DataTable dtbDetail = new DataTable();//引产观察记录内容
                DataTable dtbValue = new DataTable();//引产观察记录内容  

                lngRes = objHRPServ.lngGetDataTableWithParameters(c_strGetRecordContentSQL, ref dtbValue, objDPArr);

                if (lngRes > 0 && dtbValue.Rows.Count > 0)
                {
                    ArrayList arlTransDataSub = new ArrayList(10);
                    ArrayList arlTransData = new ArrayList(10);
                    DateTime dtmOpenDate = DateTime.MinValue;
                    for (int j = 0 ; j < dtbValue.Rows.Count ; j++)
                    {
                        #region
                        //获取当前DataTable记录的OpenDate，记录在dtmOpenDate
                        dtmOpenDate = DateTime.Parse(dtbValue.Rows[j]["CREATEDATE_DAT"].ToString()).Date;
                        while (j < dtbValue.Rows.Count && DateTime.Parse(dtbValue.Rows[j]["CREATEDATE_DAT"].ToString()).Date == dtmOpenDate)
                        {
                            #region 从DataTable.Rows中获取结果

                            clsIcuACAD_PostPartumseeRecord_VO objRecordContent = new clsIcuACAD_PostPartumseeRecord_VO();
                            objRecordContent.m_strRegisterID = p_strRegisterId;
                            DateTime dtmTemp = DateTime.MinValue;
                            DateTime.TryParse(dtbValue.Rows[j]["CREATEDATE_DAT"].ToString(), out dtmTemp);
                            objRecordContent.m_dtmCreateDate = dtmTemp;

                            dtmTemp = DateTime.MinValue;
                            DateTime.TryParse(dtbValue.Rows[0]["RECORDDATE_DAT"].ToString(), out dtmTemp);
                            objRecordContent.m_dtmRecordDate = dtmTemp;

                            dtmTemp = DateTime.MinValue;
                            DateTime.TryParse(dtbValue.Rows[j]["FIRSTPRINTDATE_DAT"].ToString(), out dtmTemp);
                            objRecordContent.m_dtmFirstPrintDate = dtmTemp;

                            objRecordContent.m_strCreateUserID = dtbValue.Rows[j]["CREATEUSERID_VCHR"].ToString();
                            objRecordContent.m_strRecordUserID = dtbValue.Rows[j]["RECORDUSERID_VCHR"].ToString();
                            objRecordContent.m_strModifyUserID = dtbValue.Rows[j]["MODIFYUSERID"].ToString();

                            dtmTemp = DateTime.MinValue;
                            DateTime.TryParse(dtbValue.Rows[j]["MODIFYDATE"].ToString(), out dtmTemp);
                            objRecordContent.m_dtmModifyDate = dtmTemp;


                            int intTemp = 0;
                            int.TryParse(dtbValue.Rows[j]["IFCONFIRM_INT"].ToString(), out intTemp);
                            objRecordContent.m_bytIfConfirm = intTemp;
                            intTemp = 0;
                            int.TryParse(dtbValue.Rows[j]["STATUS_INT"].ToString(), out intTemp);
                            objRecordContent.m_bytStatus = intTemp;

                            objRecordContent.m_strBLOODPRESSURE_CHR = dtbValue.Rows[j]["BLOODPRESSURE_CHR"].ToString();
                            objRecordContent.m_strBLOODPRESSURE_CHR_RIGHT = dtbValue.Rows[j]["BLOODPRESSURE_CHR_RIGHT"].ToString();
                            objRecordContent.m_strBLOODPRESSURE_CHRXML = dtbValue.Rows[j]["BLOODPRESSURE_CHRXML"].ToString();

                            objRecordContent.m_strBODYTEMPARTURE_CHR = dtbValue.Rows[j]["BODYTEMPARTURE_CHR"].ToString();
                            objRecordContent.m_strBODYTEMPARTURE_CHR_RIGHT = dtbValue.Rows[j]["BODYTEMPARTURE_CHR_RIGHT"].ToString();
                            objRecordContent.m_strBODYTEMPARTURE_CHRXML = dtbValue.Rows[j]["BODYTEMPARTURE_CHRXML"].ToString();

                            objRecordContent.m_strPULSE_CHR = dtbValue.Rows[j]["PULSE_CHR"].ToString();
                            objRecordContent.m_strPULSE_CHR_RIGHT = dtbValue.Rows[j]["PULSE_CHR_RIGHT"].ToString();
                            objRecordContent.m_strPULSE_CHRXML = dtbValue.Rows[j]["PULSE_CHRXML"].ToString();

                            objRecordContent.m_strUTERUS_CHR = dtbValue.Rows[j]["UTERUS_CHR"].ToString();
                            objRecordContent.m_strUTERUS_CHR_RIGHT = dtbValue.Rows[j]["UTERUS_CHR_RIGHT"].ToString();
                            objRecordContent.m_strUTERUS_CHRXML = dtbValue.Rows[j]["UTERUS_CHRXML"].ToString();

                            objRecordContent.m_strBLOODED_CHR = dtbValue.Rows[j]["BLOODED_CHR"].ToString();
                            objRecordContent.m_strBLOODED_CHR_RIGHT = dtbValue.Rows[j]["BLOODED_CHR_RIGHT"].ToString();
                            objRecordContent.m_strBLOODED_CHRXML = dtbValue.Rows[j]["BLOODED_CHRXML"].ToString();

                            objRecordContent.m_strBREAKWATER_CHR_RIGHT = dtbValue.Rows[j]["BREAKWATER_CHR_RIGHT"].ToString();
                            objRecordContent.m_strBREAKWATER_CHR = dtbValue.Rows[j]["BREAKWATER_CHR"].ToString();
                            objRecordContent.m_strBREAKWATER_CHRXML = dtbValue.Rows[j]["BREAKWATER_CHRXML"].ToString();

                            objRecordContent.m_strEMBRYO_CHR = dtbValue.Rows[j]["EMBRYO_CHR"].ToString();
                            objRecordContent.m_strEMBRYO_CHR_RIGHT = dtbValue.Rows[j]["EMBRYO_CHR_RIGHT"].ToString();
                            objRecordContent.m_strEMBRYO_CHRXML = dtbValue.Rows[j]["EMBRYO_CHRXML"].ToString();


                            objRecordContent.m_strUTERUSSIZE_CHR = dtbValue.Rows[j]["UTERUSSIZE_CHR"].ToString();
                            objRecordContent.m_strUTERUSSIZE_CHR_RIGHT = dtbValue.Rows[j]["UTERUSSIZE_CHR_RIGHT"].ToString();
                            objRecordContent.m_strUTERUSSIZE_CHRXML = dtbValue.Rows[j]["UTERUSSIZE_CHRXML"].ToString();
                            if (dtbValue.Rows[j]["RECORDDATE"].ToString() == null)
                            {
                                objRecordContent.m_dtmRecordDate = DateTime.Now;
                            }
                            else
                            {
                                dtmTemp = DateTime.MinValue;
                                DateTime.TryParse(dtbValue.Rows[j]["RECORDDATE"].ToString(), out dtmTemp);
                                objRecordContent.m_dtmRecordDate = dtmTemp;

                              //  objRecordContent.m_dtmRecordDate = Convert.ToDateTime(dtbValue.Rows[j]["RECORDDATE"].ToString());
                            }
                            //获取签名集合
                            long lngS = 0;
                            if (long.TryParse(dtbValue.Rows[j]["SEQUENCE_INT"].ToString(), out lngS))
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
                        clsICUACAD_POSTPARTUMSEERECORDContentDataInfo objdateInfo = new clsICUACAD_POSTPARTUMSEERECORDContentDataInfo();
                        objdateInfo.m_objRecordArr = (clsIcuACAD_PostPartumseeRecord_VO[])arlTransDataSub.ToArray(typeof(clsIcuACAD_PostPartumseeRecord_VO));
                        objdateInfo.m_objRecordContent = objdateInfo.m_objRecordArr[objdateInfo.m_objRecordArr.Length - 1];
                        arlTransData.Add(objdateInfo);
                        arlTransDataSub.Clear();
                    }
                    p_objTansDataInfoArr = (clsICUACAD_POSTPARTUMSEERECORDContentDataInfo[])arlTransData.ToArray(typeof(clsICUACAD_POSTPARTUMSEERECORDContentDataInfo));
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
  from icuacad_postpartumseerecord t1, icuacad_postpartumseecontent t2
 where t1.registerid_chr = t2.registerid_chr
   and t1.createdate_dat = t2.createdate_dat
   and t1.status_int = 1
   and t2.status_int = 1
   and t1.registerid_chr = ?
   and t1.createdate_dat = ?";

				
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
                    objDPArr = null;
                    p_objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_objRecordContent.m_strRegisterID;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = p_objRecordContent.m_dtmCreateDate;

                    lngRes = p_objHRPServ.lngGetDataTableWithParameters(c_strGetDeleteRecordSQL, ref dtbValue, objDPArr);

                    if (lngRes > 0 && dtbValue.Rows.Count == 1)
                    {
                        p_objModifyInfo = new clsPreModifyInfo();
                        p_objModifyInfo.m_strActionUserID = dtbValue.Rows[0]["DEACTIVEDOPERATORID_VCHR"].ToString();
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
                lngRes = p_objHRPServ.lngExecuteParameterSQL(c_strDeleteRecordSQL, ref lngEff, objDPArr);
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
