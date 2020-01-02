using System;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility;
using System.EnterpriseServices;
using Microsoft.VisualBasic;
using System.Data; 
using System.Collections.Generic;

namespace com.digitalwave.iCare.middletier.LIS
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(Enabled = true)]
    public class clsQueryLIS_Svc : com.digitalwave.iCare.middletier.clsMiddleTierBase//MiddleTierBase.dll
    {
        private const string c_strGetInstrumentSerialSetting = @"select d.deviceid_chr,
                                                                       m.device_model_desc_vchr,
                                                                       s.comno_chr,
                                                                       s.baulrate_chr,
                                                                       s.databit_chr,
                                                                       s.stopbit_chr,
                                                                       s.parity_chr,
                                                                       s.flowcontrol_chr,
                                                                       s.receivebuffer_chr,
                                                                       s.sendbuffer_chr,
                                                                       s.sendcommand_chr,
                                                                       s.sendcommandinternal_chr,
                                                                       s.dataanalysisdll_vchr,
                                                                       s.dataanalysisnamespace_vchr,
                                                                       d.devicename_vchr,
                                                                       d.dataacquisitioncomputerip_chr,
                                                                       d.device_code_chr
                                                                  from t_bse_lis_device d
                                                                  left join t_bse_lis_serialsetup s
                                                                    on d.device_model_id_chr = s.device_model_id_chr
                                                                 inner join t_bse_lis_device_model m
                                                                    on d.device_model_id_chr = m.device_model_id_chr
                                                                 where d.end_date_dat is null";

        public clsQueryLIS_Svc()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 获取仪器参数 
        /// </summary>
        /// <param name="strData_Acquisition_Computer_IP"></param>
        /// <param name="objConfig_List"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetInstrumentSerialSetting2(string strData_Acquisition_Computer_IP, out clsLIS_Equip_Base[] objConfig_List)
        {
            objConfig_List = null;
            long lngRes = 0;
            if (string.IsNullOrEmpty(strData_Acquisition_Computer_IP))
                return lngRes;

            clsHRPTableService objHRPServ = null;

            try
            {

                string strSQL = @"select d.deviceid_chr,
       m.device_model_desc_vchr,
       s.comno_chr,
       s.baulrate_chr,
       s.databit_chr,
       s.stopbit_chr,
       s.parity_chr,
       s.flowcontrol_chr,
       s.receivebuffer_chr,
       s.sendbuffer_chr,
       s.sendcommand_chr,
       s.sendcommandinternal_chr,
       s.dataanalysisdll_vchr,
       s.dataanalysisnamespace_vchr,
       d.devicename_vchr,
       d.dataacquisitioncomputerip_chr,
       d.device_code_chr
  from t_bse_lis_serialsetup  s,
       t_bse_lis_device       d,
       t_bse_lis_device_model m
 where d.device_model_id_chr = s.device_model_id_chr
   and d.device_model_id_chr = m.device_model_id_chr
   and d.end_date_dat is null
   and trim(d.dataacquisitioncomputerip_chr) = ?";

                DataTable dtResult = null;
                objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strData_Acquisition_Computer_IP;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                //List<clsLIS_Equip_Base> lstEquip = new List<clsLIS_Equip_Base>();
                List<clsLIS_Equip_Base> lstEquip = new List<clsLIS_Equip_Base>();
                DataRow drTemp = null;
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    #region clsLIS_Equip_ConfigVO
                    clsLIS_Equip_ConfigVO2 objTemp = null;
                    for (int iRow = 0; iRow < dtResult.Rows.Count; iRow++)
                    {
                        drTemp = dtResult.Rows[iRow];
                        objTemp = new clsLIS_Equip_ConfigVO2();

                        if (drTemp["DEVICEID_CHR"] != System.DBNull.Value)
                        { objTemp.strLIS_Instrument_ID = drTemp["DEVICEID_CHR"].ToString().Trim(); }

                        if (drTemp["DEVICENAME_VCHR"] != System.DBNull.Value)
                        { objTemp.strLIS_Instrument_Name = drTemp["DEVICENAME_VCHR"].ToString().Trim(); }

                        if (drTemp["DEVICE_MODEL_DESC_VCHR"] != System.DBNull.Value)
                        { objTemp.strLIS_Instrument_Model = drTemp["DEVICE_MODEL_DESC_VCHR"].ToString().Trim(); }

                        if (drTemp["COMNO_CHR"] != System.DBNull.Value)
                        { objTemp.strCOM_No = drTemp["COMNO_CHR"].ToString().Trim(); }

                        if (drTemp["BAULRATE_CHR"] != System.DBNull.Value)
                        { objTemp.strBaud_Rate = drTemp["BAULRATE_CHR"].ToString().Trim(); }

                        if (drTemp["DATABIT_CHR"] != System.DBNull.Value)
                        { objTemp.strData_Bit = drTemp["DATABIT_CHR"].ToString().Trim(); }

                        if (drTemp["STOPBIT_CHR"] != System.DBNull.Value)
                        { objTemp.strStop_Bit = drTemp["STOPBIT_CHR"].ToString().Trim(); }

                        if (drTemp["PARITY_CHR"] != System.DBNull.Value)
                        { objTemp.strParity = drTemp["PARITY_CHR"].ToString().Trim(); }

                        if (drTemp["FLOWCONTROL_CHR"] != System.DBNull.Value)
                        { objTemp.strFlow_Control = drTemp["FLOWCONTROL_CHR"].ToString().Trim(); }

                        if (drTemp["RECEIVEBUFFER_CHR"] != System.DBNull.Value)
                        { objTemp.strReceive_Buffer = drTemp["RECEIVEBUFFER_CHR"].ToString().Trim(); }

                        if (drTemp["SENDBUFFER_CHR"] != System.DBNull.Value)
                        { objTemp.strSend_Buffer = drTemp["SENDBUFFER_CHR"].ToString().Trim(); }

                        if (drTemp["SENDCOMMAND_CHR"] != System.DBNull.Value)
                        { objTemp.strSend_Command = drTemp["SENDCOMMAND_CHR"].ToString(); }

                        if (drTemp["SENDCOMMANDINTERNAL_CHR"] != System.DBNull.Value)
                        { objTemp.strSend_Command_Internal = drTemp["SENDCOMMANDINTERNAL_CHR"].ToString().Trim(); }

                        if (drTemp["DATAANALYSISDLL_VCHR"] != System.DBNull.Value)
                        { objTemp.strData_Analysis_DLL = drTemp["DATAANALYSISDLL_VCHR"].ToString().Trim(); }

                        if (drTemp["DATAANALYSISNAMESPACE_VCHR"] != System.DBNull.Value)
                        { objTemp.strData_Analysis_Namespace = drTemp["DATAANALYSISNAMESPACE_VCHR"].ToString().Trim(); }

                        if (drTemp["DATAACQUISITIONCOMPUTERIP_CHR"] != System.DBNull.Value)
                        { objTemp.strData_Acquisition_Computer_IP = drTemp["DATAACQUISITIONCOMPUTERIP_CHR"].ToString().Trim(); }

                        objTemp.strLIS_Instrument_NO = drTemp["device_code_chr"].ToString().Trim();

                        lstEquip.Add(objTemp);

                    }
                    #endregion
                }

                strSQL = @"select a.device_model_id_chr,
       a.online_module_chr,
       a.online_dns_vchr,
       a.work_module_chr,
       a.work_auto_internal_vchr,
       a.pic_path_vchr,
       a.other_pram_vchr,
       a.dataanalysisdll_vchr,
       a.dataanalysisnamespace_vchr,
       b.deviceid_chr,
       b.devicename_vchr,
       b.device_code_chr,
       b.dataacquisitioncomputerip_chr,
       c.device_model_desc_vchr
  from t_bse_lis_interface_online a,
       t_bse_lis_device           b,
       t_bse_lis_device_model     c
 where a.device_model_id_chr = c.device_model_id_chr
   and b.device_model_id_chr = a.device_model_id_chr
   and b.end_date_dat is null
   and trim(b.dataacquisitioncomputerip_chr) = ?";

                dtResult = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strData_Acquisition_Computer_IP;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    #region clsLIS_Equip_DB_ConfigVO
                    clsLIS_Equip_DB_ConfigVO objTempDB = null;
                    for (int idx = 0; idx < dtResult.Rows.Count; idx++)
                    {
                        drTemp = dtResult.Rows[idx];
                        objTempDB = new clsLIS_Equip_DB_ConfigVO();
                        objTempDB.strLIS_Instrument_Model = drTemp["device_model_desc_vchr"].ToString().Trim();
                        objTempDB.strONLINE_MODULE_CHR = drTemp["online_module_chr"].ToString().Trim();
                        objTempDB.strONLINE_DNS_VCHR = drTemp["online_dns_vchr"].ToString().Trim();
                        objTempDB.strWORK_MODULE_CHR = drTemp["work_module_chr"].ToString().Trim();
                        objTempDB.strWORK_AUTO_INTERNAL_VCHR = drTemp["work_auto_internal_vchr"].ToString().Trim();
                        objTempDB.strPIC_PATH_VCHR = drTemp["pic_path_vchr"].ToString().Trim();
                        objTempDB.strOTHER_PRAM_VCHR = drTemp["other_pram_vchr"].ToString().Trim();
                        objTempDB.strData_Analysis_DLL = drTemp["dataanalysisdll_vchr"].ToString().Trim();
                        objTempDB.strData_Analysis_Namespace = drTemp["dataanalysisnamespace_vchr"].ToString().Trim();
                        objTempDB.strLIS_Instrument_ID = drTemp["deviceid_chr"].ToString().Trim();
                        objTempDB.strLIS_Instrument_Name = drTemp["devicename_vchr"].ToString().Trim();
                        objTempDB.strLIS_Instrument_NO = drTemp["device_code_chr"].ToString().Trim();
                        objTempDB.strData_Acquisition_Computer_IP = drTemp["dataacquisitioncomputerip_chr"].ToString().Trim();

                        lstEquip.Add(objTempDB);

                    }
                    #endregion
                }

                objConfig_List = lstEquip.ToArray();
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                objHRPServ = null;
                strData_Acquisition_Computer_IP = null;
            }
            return lngRes;
        }



        [AutoComplete]
        public long lngGetInstrumentSerialSetting(string strData_Acquisition_Computer_IP, out clsLIS_Equip_ConfigVO[] objConfig_List)
        {
            string strSQL = c_strGetInstrumentSerialSetting + "   and lower(d.dataacquisitioncomputerip_chr) = lower(?)";

            objConfig_List = null;

            long lngRes = 0;
            try
            {
                System.Data.DataTable objDT_LIS_Instrument_Info = null;
                IDataParameter[] objDPArr = null;
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].DbType = DbType.String;
                objDPArr[0].Value = strData_Acquisition_Computer_IP;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref objDT_LIS_Instrument_Info, objDPArr);
                objHRPSvc.Dispose();

                if (lngRes > 0)
                {
                    int intRowCount = objDT_LIS_Instrument_Info.Rows.Count;
                    if (intRowCount > 0)
                    {
                        objConfig_List = new clsLIS_Equip_ConfigVO[intRowCount];
                        clsLIS_Equip_ConfigVO objTemp = null;
                        System.Data.DataRow objRow = null;
                        for (int i = 0; i < intRowCount; i++)
                        {
                            objRow = objDT_LIS_Instrument_Info.Rows[i];
                            objTemp = new clsLIS_Equip_ConfigVO();

                            if (objRow["DEVICEID_CHR"] != System.DBNull.Value)
                            { objTemp.strLIS_Instrument_ID = objRow["DEVICEID_CHR"].ToString().Trim(); }

                            if (objRow["DEVICENAME_VCHR"] != System.DBNull.Value)
                            { objTemp.strLIS_Instrument_Name = objRow["DEVICENAME_VCHR"].ToString().Trim(); }

                            if (objRow["DEVICE_MODEL_DESC_VCHR"] != System.DBNull.Value)
                            { objTemp.strLIS_Instrument_Model = objRow["DEVICE_MODEL_DESC_VCHR"].ToString().Trim(); }

                            if (objRow["COMNO_CHR"] != System.DBNull.Value)
                            { objTemp.strCOM_No = objRow["COMNO_CHR"].ToString().Trim(); }

                            if (objRow["BAULRATE_CHR"] != System.DBNull.Value)
                            { objTemp.strBaud_Rate = objRow["BAULRATE_CHR"].ToString().Trim(); }

                            if (objRow["DATABIT_CHR"] != System.DBNull.Value)
                            { objTemp.strData_Bit = objRow["DATABIT_CHR"].ToString().Trim(); }

                            if (objRow["STOPBIT_CHR"] != System.DBNull.Value)
                            { objTemp.strStop_Bit = objRow["STOPBIT_CHR"].ToString().Trim(); }

                            if (objRow["PARITY_CHR"] != System.DBNull.Value)
                            { objTemp.strParity = objRow["PARITY_CHR"].ToString().Trim(); }

                            if (objRow["FLOWCONTROL_CHR"] != System.DBNull.Value)
                            { objTemp.strFlow_Control = objRow["FLOWCONTROL_CHR"].ToString().Trim(); }

                            if (objRow["RECEIVEBUFFER_CHR"] != System.DBNull.Value)
                            { objTemp.strReceive_Buffer = objRow["RECEIVEBUFFER_CHR"].ToString().Trim(); }

                            if (objRow["SENDBUFFER_CHR"] != System.DBNull.Value)
                            { objTemp.strSend_Buffer = objRow["SENDBUFFER_CHR"].ToString().Trim(); }

                            if (objRow["SENDCOMMAND_CHR"] != System.DBNull.Value)
                            { objTemp.strSend_Command = objRow["SENDCOMMAND_CHR"].ToString(); }

                            if (objRow["SENDCOMMANDINTERNAL_CHR"] != System.DBNull.Value)
                            { objTemp.strSend_Command_Internal = objRow["SENDCOMMANDINTERNAL_CHR"].ToString().Trim(); }

                            if (objRow["DATAANALYSISDLL_VCHR"] != System.DBNull.Value)
                            { objTemp.strData_Analysis_DLL = objRow["DATAANALYSISDLL_VCHR"].ToString().Trim(); }

                            if (objRow["DATAANALYSISNAMESPACE_VCHR"] != System.DBNull.Value)
                            { objTemp.strData_Analysis_Namespace = objRow["DATAANALYSISNAMESPACE_VCHR"].ToString().Trim(); }

                            if (objRow["DATAACQUISITIONCOMPUTERIP_CHR"] != System.DBNull.Value)
                            { objTemp.strData_Acquisition_Computer_IP = objRow["DATAACQUISITIONCOMPUTERIP_CHR"].ToString().Trim(); }

                            objTemp.strLIS_Instrument_NO = objRow["device_code_chr"].ToString().Trim();

                            objConfig_List[i] = objTemp;
                        }
                    }
                }
            }
            catch (System.Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }

        private void ConstructDeviceVO(System.Data.DataRow objRow, ref clsLIS_Equip_ConfigVO objDeviceVO)
        {

        }

        /// <summary>
        /// 获取指定检验编号的检验项目通道号数组
        /// </summary>
        /// <param name="p_strCheckSampleNO"></param>
        /// <param name="p_strCheckItemstring"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSampleCheckItems(string p_strCheckSampleNO, out string p_strCheckItemstring)
        {
            p_strCheckItemstring = null;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strCheckSampleNO))
                return lngRes;

            clsHRPTableService objHRPServ = null;

            try
            {
                string strSQL = @"select d.device_check_item_no_vchr
  from t_opr_lis_application a
 inner join t_opr_lis_app_check_item b on b.application_id_chr =
                                          a.application_id_chr
 inner join t_bse_lis_check_item_dev_item c on c.check_item_id_chr =
                                               b.check_item_id_chr
 inner join t_bse_lis_device_check_item d on d.device_check_item_id_chr =
                                             c.device_check_item_id_chr
                                         and d.device_model_id_chr =
                                             c.device_model_id_chr
 where a.pstatus_int = 2
   and a.application_dat > ?
   and a.application_form_no_chr = ?
 order by d.device_check_item_no_vchr";

                objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = DateTime.Now.Date;
                objDPArr[1].Value = p_strCheckSampleNO;

                DataTable dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_strCheckItemstring = "";
                    for (int idx = 0; idx < dtResult.Rows.Count; idx++)
                    {
                        p_strCheckItemstring += dtResult.Rows[idx]["device_check_item_no_vchr"].ToString().Trim();
                    }
                }
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                p_strCheckItemstring = null;
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                p_strCheckSampleNO = null;
                objHRPServ = null;
            }
            return lngRes;
        }

        #region GetOttomanAppItems
        /// <summary>
        /// GetOttomanAppItems
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetOttomanAppItems(string barCode)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            clsHRPTableService svc = null;
            try
            {
                Sql = @"select t1.sex_chr                      as sex,
                               t1.age_chr                      as age,
                               t3.check_item_id_chr            as itemId,
                               t6.check_item_english_name_vchr as itemName
                          from t_opr_lis_sample t1
                         inner join t_opr_lis_app_sample t2
                            on t1.sample_id_chr = t2.sample_id_chr
                         inner join t_opr_lis_app_check_item t3
                            on t1.application_id_chr = t3.application_id_chr
                           and t2.sample_group_id_chr = t3.sample_group_id_chr
                           and t2.report_group_id_chr = t3.report_group_id_chr
                         inner join t_bse_lis_check_item t6
                            on t3.check_item_id_chr = t6.check_item_id_chr
                         where t1.barcode_vchr = ? 
                           and t1.status_int > 0";

                svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = barCode;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null) dt.TableName = "ottoman";
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            finally
            {
                svc.Dispose();
                svc = null;
            }
            return dt;
        }
        #endregion

        #region GetOttomanCheckResult
        /// <summary>
        /// GetOttomanCheckResult
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [AutoComplete]
        public List<clsDeviceReslutVO> GetOttomanCheckResult(string barCode)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            List<clsDeviceReslutVO> data = null;
            clsHRPTableService svc = null;
            try
            {
                Sql = @"select idx_int,
                               device_check_item_name_vchr,
                               device_sampleid_chr,
                               abnormal_flag_vchr,
                               check_dat,
                               min_val_dec,
                               max_val_dec,
                               deviceid_chr,
                               pstatus_int,
                               refrange_vchr,
                               result_vchr,
                               unit_vchr,
                               graph_img,
                               graph_format_name_vchr,
                               is_graph_result_num,
                               result2_vchr,
                               doctorexpress
                          from t_opr_lis_result
                         where barcode = ?";

                svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = barCode;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    clsDeviceReslutVO vo = null;
                    data = new List<clsDeviceReslutVO>();
                    DataView dv = new DataView(dt);
                    dv.Sort = "check_dat desc";
                    dt = dv.ToTable();
                    List<string> lstItemName = new List<string>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        vo = new clsDeviceReslutVO();
                        vo.m_strAbnormalFlag = "";
                        vo.m_strCheckDat = Convert.ToDateTime(dr["check_dat"]).ToString("yyyy-MM-dd HH:mm:ss");
                        vo.m_strDeviceCheckItemName = dr["device_check_item_name_vchr"].ToString();
                        vo.m_strDeviceID = dr["deviceid_chr"].ToString();
                        vo.m_strDeviceSampleID = dr["device_sampleid_chr"].ToString();
                        vo.m_strIdx = dr["idx_int"].ToString(); ;
                        vo.m_strMaxVal = dr["max_val_dec"].ToString();
                        vo.m_strMinVal = dr["min_val_dec"].ToString();
                        vo.m_strRefRange = string.Empty;
                        vo.m_strResult = dr["result_vchr"].ToString();
                        if (lstItemName.IndexOf(vo.m_strDeviceCheckItemName) < 0)
                        {
                            lstItemName.Add(vo.m_strDeviceCheckItemName);
                            data.Add(vo);
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            finally
            {
                svc.Dispose();
                svc = null;
            }
            return data;
        }
        #endregion
    }
}
