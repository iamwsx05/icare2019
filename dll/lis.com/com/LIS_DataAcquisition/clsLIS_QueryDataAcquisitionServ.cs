using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.Utility;
using System.EnterpriseServices;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data; 

namespace com.digitalwave.iCare.middletier.LIS
{
    /// <summary>
    /// 检验数据采集中间件
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsLIS_QueryDataAcquisitionServ : clsMiddleTierBase
    {
        #region 判断是追加结果还是新增一次结果 用于Stago、CentaurCP等仪器
        /// <summary>
        ///  判断是追加结果还是新增一次结果 用于Stago、CentaurCP等仪器
        /// </summary>
        /// <param name="has"></param>
        /// <param name="p_objResultArr"></param>
        /// <param name="strConditionList"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnIsAppendResult(ref Dictionary<string, string> has, clsLIS_Device_Test_ResultVO[] p_objResultArr, out string[] strConditionList)
        {
            long lngRes = 1;
            string strSQL = null;
            strConditionList = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            #region 判断是追加结果还是新增一次结果 用于Stago、CentaurCP等仪器
            /* 原理：获取上次取得该样本结果的最大序列
               根据序列，仪器和标本号查找log表得起始值
               找到结果进行结果校对哈希比较，如果不一致立即跳过进行原操作
               否则追加插入结果，修改req日期.log起始值和日期 */

            bool blnFlag = true;  //true-新增  false-追加
            try
            {
                DataTable dtbTmp = new DataTable();
                clsLIS_Device_Test_ResultVO objTmp = p_objResultArr[0];
                string m_strSID = objTmp.strDevice_Sample_ID;
                string m_strDevID = objTmp.strDevice_ID;
                string m_strDateBegin = DateTime.Now.ToShortDateString() + " 00:00:00";
                string m_strDateEnd = DateTime.Now.ToShortDateString() + " 23:59:59";

                strSQL = @"select a.check_dat, a.device_sampleid_chr, a.deviceid_chr, a.import_req_int
  from t_opr_lis_result_import_req a
 where a.check_dat between ? and ?
   and a.deviceid_chr = ?
   and a.device_sampleid_chr = ?
 order by a.import_req_int desc";

                IDataParameter[] objParamArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objParamArr);
                objParamArr[0].DbType = DbType.DateTime;
                objParamArr[0].Value = Convert.ToDateTime(m_strDateBegin);
                objParamArr[1].DbType = DbType.DateTime;
                objParamArr[1].Value = Convert.ToDateTime(m_strDateEnd);
                objParamArr[2].Value = m_strDevID;
                objParamArr[3].Value = m_strSID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbTmp, objParamArr);
                if (lngRes > 0 && dtbTmp.Rows.Count > 0)
                {
                    Int32 m_intImport_Req = 0;
                    string m_strCheckDate_req = "";
                    m_strCheckDate_req = Convert.ToDateTime(dtbTmp.Rows[0]["check_dat"]).ToString("yyyy-MM-dd HH:mm:ss");
                    m_intImport_Req = Convert.ToInt32(dtbTmp.Rows[0]["import_req_int"]);

                    strSQL = @"select a.begin_idx_int,
       a.check_dat,
       a.device_sampleid_chr,
       a.import_req_int,
       a.end_idx_int,
       a.deviceid_chr,
       a.use_flag_chr
  from t_opr_lis_result_log a
 where a.check_dat = ?
   and a.import_req_int = ?
   and a.deviceid_chr = ?";

                    objParamArr = null;
                    dtbTmp = null;
                    objHRPSvc.CreateDatabaseParameter(3, out objParamArr);
                    objParamArr[0].DbType = DbType.DateTime;
                    objParamArr[0].Value = Convert.ToDateTime(m_strCheckDate_req);
                    objParamArr[1].Value = m_intImport_Req;
                    objParamArr[2].Value = m_strDevID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbTmp, objParamArr);
                    if (lngRes > 0 && dtbTmp.Rows.Count > 0)
                    {
                        Int64 intIdxBegin = Convert.ToInt64(dtbTmp.Rows[0]["begin_idx_int"]);
                        Int64 intIdxEnd = Convert.ToInt64(dtbTmp.Rows[0]["end_idx_int"]);
                        strSQL = @"select idx_int, result_vchr, device_check_item_name_vchr
  from t_opr_lis_result
 where deviceid_chr = ?
   and device_sampleid_chr = ?
   and check_dat = ?
   and idx_int >= ?
   and idx_int <= ?";
                        objParamArr = null;
                        objTmp = null;
                        objHRPSvc.CreateDatabaseParameter(5, out objParamArr);
                        objParamArr[0].Value = m_strDevID;
                        objParamArr[1].Value = m_strSID;
                        objParamArr[2].DbType = DbType.DateTime;
                        objParamArr[2].Value = Convert.ToDateTime(m_strCheckDate_req);
                        objParamArr[3].Value = intIdxBegin;
                        objParamArr[4].Value = intIdxEnd;
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbTmp, objParamArr);
                        objHRPSvc.Dispose();
                        if (lngRes > 0 && dtbTmp.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtbTmp.Rows)
                            {
                                if (has.ContainsKey(dr["device_check_item_name_vchr"].ToString()))
                                {
                                }
                                else
                                {
                                    strConditionList = new string[4];
                                    strConditionList[0] = m_strSID; //仪器样本ID
                                    strConditionList[1] = m_strDevID; //仪器ID
                                    strConditionList[2] = m_intImport_Req.ToString(); //系统内部结果序列
                                    strConditionList[3] = m_strCheckDate_req; //检验日期
                                    blnFlag = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return blnFlag;

            #endregion
        }
        #endregion

        #region 获取仪器参数
        /// <summary>
        /// 获取仪器参数
        /// </summary>
        /// <param name="strData_Acquisition_Computer_IP"></param>
        /// <param name="objConfig_List"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetInstrumentSerialSetting(string strData_Acquisition_Computer_IP, out clsLIS_Equip_Base[] objConfig_List)
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
   and d.dataacquisitioncomputerip_chr = ?";

                DataTable dtResult = null;
                objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strData_Acquisition_Computer_IP;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

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
   and b.dataacquisitioncomputerip_chr = ?";

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

        /// <summary>
        /// 获取仪器项目名称与通道号对应关系、仪器结果转换信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_objDeviceItemNameNOArr"></param>
        /// <param name="p_objItemConvertArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeviceNOConvertInfo(string p_strDeviceID,             out clsDeviceItemNameItemNO[] p_objDeviceItemNameNOArr, out clsDeviceItemValueConvert_VO[] p_objItemConvertArr)
        {
            p_objDeviceItemNameNOArr = null;
            p_objItemConvertArr = null;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strDeviceID))
                return lngRes;

            lngRes = m_lngQueryDeviceItemNO(p_strDeviceID, out p_objDeviceItemNameNOArr);
            if (lngRes <= 0)
                return lngRes;
            lngRes = m_lngQueryDeviceItemValueConvert(p_strDeviceID, out p_objItemConvertArr);

            return lngRes;
        }


        #region 获取仪器项目名称与通道号对应关系
        /// <summary>
        /// 获取仪器项目名称与通道号对应关系
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_objDeviceItemNameNOArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryDeviceItemNO(string p_strDeviceID, out clsDeviceItemNameItemNO[] p_objDeviceItemNameNOArr)
        {
            p_objDeviceItemNameNOArr = null;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strDeviceID))
            {
                return lngRes;
            } 
            clsHRPTableService objHRPServ = null;

            try
            {
                string strSQL = @"select a.device_check_item_name_vchr, a.device_check_item_no_vchr
  from t_bse_lis_device_check_item a
 inner join t_bse_lis_device b on a.device_model_id_chr =
                                  b.device_model_id_chr
 where a.device_check_item_no_vchr is not null
   and b.deviceid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ = new clsHRPTableService();
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeviceID;

                DataTable dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    int iRowCount = dtResult.Rows.Count;
                    p_objDeviceItemNameNOArr = new clsDeviceItemNameItemNO[iRowCount];

                    clsDeviceItemNameItemNO objTemp = null;
                    DataRow drTemp = null;
                    for (int idx = 0; idx < iRowCount; idx++)
                    {
                        drTemp = dtResult.Rows[idx];
                        objTemp = new clsDeviceItemNameItemNO();
                        objTemp.m_strDeviceItemName = drTemp["device_check_item_name_vchr"].ToString().Trim();
                        objTemp.m_strDeviceItemNO = drTemp["device_check_item_no_vchr"].ToString().Trim();

                        p_objDeviceItemNameNOArr[idx] = objTemp;
                    }
                }

            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            finally
            {
                objHRPServ = null;
                p_strDeviceID = null;
            }
            return lngRes;
        }

        #endregion

        #region 获取仪器结果转换信息
        /// <summary>
        /// 获取仪器结果转换信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_objItemConvertArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryDeviceItemValueConvert(string p_strDeviceID, out clsDeviceItemValueConvert_VO[] p_objItemConvertArr)
        {
            p_objItemConvertArr = null;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strDeviceID))
                return lngRes; 
            clsHRPTableService objHRPServ = null;
            try
            {
                string strSQL = @"select a.item_value_vchr,
       a.item_convert_value_vchr,
       c.device_check_item_name_vchr
  from t_bse_lis_device_item_convert a,
       t_bse_lis_device              b,
       t_bse_lis_device_check_item   c
 where a.device_model_id_chr = b.device_model_id_chr
   and c.device_model_id_chr = a.device_model_id_chr
   and c.device_check_item_id_chr = a.device_check_item_id_chr
   and b.deviceid_chr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ = new clsHRPTableService();
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strDeviceID;

                DataTable dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    int iRowCount = dtResult.Rows.Count;
                    DataRow drTemp = null;
                    clsDeviceItemValueConvert_VO objTemp = null;
                    p_objItemConvertArr = new clsDeviceItemValueConvert_VO[iRowCount];

                    for (int idx = 0; idx < iRowCount; idx++)
                    {
                        drTemp = dtResult.Rows[idx];
                        objTemp = new clsDeviceItemValueConvert_VO();
                        objTemp.m_strDeviceItemName = drTemp["device_check_item_name_vchr"].ToString().Trim();
                        objTemp.m_strDeviceItemValue = drTemp["item_value_vchr"].ToString().Trim();
                        objTemp.m_strDeviceConvertValue = drTemp["item_convert_value_vchr"].ToString().Trim();

                        p_objItemConvertArr[idx] = objTemp;
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                p_strDeviceID = null;
                objHRPServ = null;
            }
            return lngRes;
        }

        #endregion

        #endregion

        #region  m_lngGetDeviceData 根据指定的仪器编号,检验日期,和仪器样本编号,及开始索引和结束索引提取仪器数据
        /// <summary>
        ///  根据指定的仪器编号,检验日期,和仪器样本编号,及开始索引和结束索引提取仪器数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strDeviceSampleID"></param>
        /// <param name="p_strCheckDate"></param>
        /// <param name="p_intBeginIndex"></param>
        /// <param name="p_intEndIndex"></param>
        /// <param name="p_objDeviceResultList"></param>
        /// <returns>
        /// 小于等于 0 : 查询失败; 
        /// 400:指定的仪器样本无原始数据
        /// 其它: 成功返回 
        /// </returns>
        [AutoComplete]
        public long m_lngGetDeviceData(string p_strDeviceID, string p_strDeviceSampleID,            string p_strCheckDate, int p_intBeginIndex, int p_intEndIndex, out clsDeviceReslutVO[] p_objDeviceResultList)
        {
            long lngRes = 0;
            p_objDeviceResultList = null;
            DataTable dtbResult = null;
            string strSQL = null; 
            if (p_strDeviceID == null || p_strDeviceSampleID == null || !Microsoft.VisualBasic.Information.IsDate(p_strCheckDate))
                return -1;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();

                DateTime m_dtDate;
                DateTime.TryParse(p_strCheckDate, out m_dtDate);
                strSQL = @"select idx_int,--1111111
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
       is_graph_result_num
  from t_opr_lis_result
 where deviceid_chr = ?
   and device_sampleid_chr = ?
   and check_dat = ?
   and idx_int >= ?
   and idx_int <= ?";

                lngRes = 0;
                System.Data.IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].Value = p_strDeviceID.PadRight(6, ' ');
                objDPArr[1].Value = p_strDeviceSampleID.Trim();
                objDPArr[2].Value = m_dtDate;
                objDPArr[3].Value = Math.Min(p_intBeginIndex, p_intEndIndex);
                objDPArr[4].Value = Math.Max(p_intBeginIndex, p_intEndIndex);

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);

                /*<================================*/

                if (lngRes > 0 && (dtbResult == null || dtbResult.Rows.Count == 0))
                {
                    return 400;//指定的仪器样本无原始数据
                }
                else if (lngRes > 0)
                {
                    p_objDeviceResultList = new clsDeviceReslutVO[dtbResult.Rows.Count];
                    clsDeviceReslutVO objDeviceResultVO = null;
                    DataRow drTemp = null;
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        drTemp = dtbResult.Rows[i];
                        objDeviceResultVO = new clsDeviceReslutVO();
                        objDeviceResultVO.m_strAbnormalFlag = drTemp["ABNORMAL_FLAG_VCHR"].ToString().Trim().ToString().Trim();
                        objDeviceResultVO.m_strCheckDat = drTemp["CHECK_DAT"].ToString().Trim();
                        objDeviceResultVO.m_strDeviceCheckItemName = drTemp["DEVICE_CHECK_ITEM_NAME_VCHR"].ToString().Trim();
                        objDeviceResultVO.m_strDeviceID = drTemp["DEVICEID_CHR"].ToString().Trim();
                        objDeviceResultVO.m_strDeviceSampleID = drTemp["DEVICE_SAMPLEID_CHR"].ToString().Trim();
                        objDeviceResultVO.m_strIdx = drTemp["IDX_INT"].ToString().Trim();
                        objDeviceResultVO.m_strMaxVal = drTemp["MAX_VAL_DEC"].ToString().Trim();
                        objDeviceResultVO.m_strMinVal = drTemp["MIN_VAL_DEC"].ToString().Trim();
                        objDeviceResultVO.m_strPstatus = drTemp["PSTATUS_INT"].ToString().Trim();
                        objDeviceResultVO.m_strRefRange = drTemp["REFRANGE_VCHR"].ToString().Trim();
                        objDeviceResultVO.m_strResult = drTemp["RESULT_VCHR"].ToString().Trim();
                        objDeviceResultVO.m_strUnit = drTemp["UNIT_VCHR"].ToString().Trim();

                        objDeviceResultVO.strGraphFormatName = drTemp["GRAPH_FORMAT_NAME_VCHR"].ToString().Trim();
                        if (drTemp["GRAPH_IMG"] != DBNull.Value)
                        {
                            objDeviceResultVO.bytGraph = (byte[])drTemp["GRAPH_IMG"];
                        }
                        if (drTemp["IS_GRAPH_RESULT_NUM"] != DBNull.Value)
                        {
                            objDeviceResultVO.intIsGraphResult = Convert.ToInt32(drTemp["IS_GRAPH_RESULT_NUM"].ToString().Trim());
                        }

                        p_objDeviceResultList[i] = objDeviceResultVO;
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);//要在LogError方法中抛出异常。 
                lngRes = -1;
            }
            return lngRes;
        }
        #endregion



        #region 通过条码获得申请单信息与检验项目对应的通道号
        /// <summary>
        /// 通过条码获得申请单信息与检验项目对应的通道号
        /// </summary>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_objAppMainVO"></param>
        /// <param name="p_strDeviceNOArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryAppInfoAndDeviceNO(string p_strDeviceID, string p_strBarCode, out clsLisApplMainVO p_objAppMainVO, out  string[] p_strDeviceNOArr)
        {
            p_objAppMainVO = null;
            p_strDeviceNOArr = null;
            long lngRes = 0;
            lngRes = m_lngQueryAppDeviceNOByBarCode(p_strDeviceID, p_strBarCode, out p_strDeviceNOArr);
            if (lngRes <= 0)
                return lngRes;

            lngRes = m_lngGetAppInfoByBarCode(p_strBarCode, out p_objAppMainVO);
            return lngRes;
        }


        #region 通过条码获得申请单检验项目对应的通道号
        /// <summary>
        /// 通过条码获得申请单检验项目对应的通道号
        /// </summary>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_strDeviceNOArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryAppDeviceNOByBarCode(string p_strDeviceID, string p_strBarCode, out string[] p_strDeviceNOArr)
        {
            p_strDeviceNOArr = null;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strDeviceID) || string.IsNullOrEmpty(p_strBarCode))
                return lngRes;

            clsHRPTableService objHRPServ = null;
            try
            {
                string strSQL = @"select e.device_check_item_name_vchr
  from t_bse_lis_check_item_dev_item a
 inner join t_bse_lis_device_check_item e
    on a.device_check_item_id_chr = e.device_check_item_id_chr
   and a.device_model_id_chr = e.device_model_id_chr
 inner join t_opr_lis_app_check_item b
    on a.check_item_id_chr = b.check_item_id_chr
 inner join t_opr_lis_sample c
    on c.application_id_chr = b.application_id_chr
 where c.barcode_vchr = ?
   and c.status_int > 0";

                IDataParameter[] objDPArr = null;
                objHRPServ = new clsHRPTableService();
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strBarCode;
                //objDPArr[1].Value = p_strBarCode;

                DataTable dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    List<string> lstStrResult = new List<string>();
                    string strTemp = null;

                    for (int idx = 0; idx < dtResult.Rows.Count; idx++)
                    {
                        strTemp = dtResult.Rows[idx]["device_check_item_name_vchr"].ToString().Trim();
                        if (!string.IsNullOrEmpty(strTemp))
                            lstStrResult.Add(strTemp);
                    }
                    p_strDeviceNOArr = lstStrResult.ToArray();
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                objHRPServ = null;
                p_strBarCode = null;
                p_strDeviceID = null;
            }
            return lngRes;
        }
        #endregion

        #region 通过条码获得申请单信息
        /// <summary>
        /// 通过条码获得申请单信息
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_objAppMainVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAppInfoByBarCode(string p_strBarCode, out clsLisApplMainVO p_objAppMainVO)
        {
            p_objAppMainVO = null;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strBarCode))
                return lngRes;

            clsHRPTableService objHRPServ = null;
            try
            {
                string strSQL = @"select a.application_id_chr,
       a.patientid_chr,
       a.application_dat,
       a.sex_chr,
       a.patient_name_vchr,
       a.patient_subno_chr,
       a.age_chr,
       a.patient_type_id_chr,
       a.diagnose_vchr,
       a.patientcardid_chr,
       a.patient_inhospitalno_chr
  from t_opr_lis_application a
 inner join t_opr_lis_sample b on a.application_id_chr =
                                  b.application_id_chr
 where a.pstatus_int = 2
   and b.status_int > 0
   and b.barcode_vchr = ?";

                IDataParameter[] objDPArr = null;
                objHRPServ = new clsHRPTableService();
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strBarCode;

                DataTable dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_objAppMainVO = new clsLisApplMainVO();
                    DataRow drTemp = dtResult.Rows[0];

                    p_objAppMainVO.m_strAPPLICATION_ID = drTemp["application_id_chr"].ToString().Trim();
                    p_objAppMainVO.m_strPatientID = drTemp["patientid_chr"].ToString().Trim();
                    p_objAppMainVO.m_strAppl_Dat = drTemp["application_dat"].ToString().Trim();
                    p_objAppMainVO.m_strSex = drTemp["sex_chr"].ToString().Trim();
                    p_objAppMainVO.m_strPatient_Name = drTemp["patient_name_vchr"].ToString().Trim();
                    p_objAppMainVO.m_strPatient_SubNO = drTemp["patient_subno_chr"].ToString().Trim();
                    p_objAppMainVO.m_strAge = drTemp["age_chr"].ToString().Trim();

                    p_objAppMainVO.m_strPatientType = drTemp["patient_type_id_chr"].ToString().Trim();
                    p_objAppMainVO.m_strDiagnose = drTemp["diagnose_vchr"].ToString().Trim();
                    p_objAppMainVO.m_strPatientcardID = drTemp["patientcardid_chr"].ToString().Trim();
                    p_objAppMainVO.m_strPatient_inhospitalno_chr = drTemp["patient_inhospitalno_chr"].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                objHRPServ = null;
                p_strBarCode = null;
            }
            return lngRes;
        }

        #endregion
        #endregion


        #region 通过条码获取病人相关信息,提供给仪器SP-1000使用 yongchao.li 添加注释 2012-2-21
        /// <summary>
        /// 通过病人卡号获取病人相关信息
        /// </summary>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_objAppMainVO"></param>
        /// <param name="p_strDeviceNOArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryPatientInfo(string p_strBarCode_vchr, out clsT_OPR_LIS_SAMPLE_VO p_objSampleVO)
        {
            p_objSampleVO = null;
            long lngRes = 0;
            lngRes = m_lngGetPatientInfoByCardID(p_strBarCode_vchr, out p_objSampleVO);
            if(lngRes<=0)
            {
                return lngRes;
            }
            return lngRes;
        }
        /// <summary>
        /// 通过病人卡号获取病人信息
        /// </summary>
        /// <param name="p_strPatientCardID"></param>
        /// <param name="p_objSampleVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfoByCardID(string p_strBarCode_vchr, out clsT_OPR_LIS_SAMPLE_VO p_objSampleVO)
        {
            p_objSampleVO = null;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strBarCode_vchr))
                return lngRes;
            clsHRPTableService objHRPServ = null;
            try
            {
                string strSQL = @"select appl_dat,sex_chr,patient_name_vchr,patient_subno_chr,age_chr,patient_type_chr,diagnose_vchr,
                                sampletype_vchr,samplestate_vchr,bedno_chr,icd_vchr,patientcardid_chr,barcode_vchr,
                                sample_id_chr,patientid_chr,sampling_date_dat,operator_id_chr,modify_dat,
                                appl_empid_chr,appl_deptid_chr,status_int,sample_type_id_chr,qcsampleid_chr,
                                samplekind_chr,check_date_dat,accept_dat,acceptor_id_chr,application_id_chr,
                                patient_inhospitalno_chr,confirm_dat,confirmer_id_chr,collector_id_chr,
                                checker_id_chr
                                from t_opr_lis_sample where barcode_vchr=?";
                IDataParameter[] objDPArr = null;
                objHRPServ = new clsHRPTableService();
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strBarCode_vchr;

                DataTable dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_objSampleVO = new clsT_OPR_LIS_SAMPLE_VO();
                    DataRow drTemp = dtResult.Rows[0];
                    p_objSampleVO.m_strAPPL_DAT = drTemp["appl_dat"].ToString().Trim();
                    p_objSampleVO.m_strSEX_CHR = drTemp["sex_chr"].ToString().Trim();
                    p_objSampleVO.m_strPATIENT_NAME_VCHR = drTemp["patient_name_vchr"].ToString().Trim();
                    p_objSampleVO.m_strPATIENT_SUBNO_CHR = drTemp["patient_subno_chr"].ToString().Trim();
                    p_objSampleVO.m_strAGE_CHR = drTemp["age_chr"].ToString().Trim();
                    p_objSampleVO.m_strPATIENT_TYPE_CHR = drTemp["patient_type_chr"].ToString().Trim();
                    p_objSampleVO.m_strDIAGNOSE_VCHR = drTemp["diagnose_vchr"].ToString().Trim();
                    p_objSampleVO.m_strSAMPLETYPE_VCHR = drTemp["sampletype_vchr"].ToString().Trim();
                    p_objSampleVO.m_strSAMPLESTATE_VCHR = drTemp["samplestate_vchr"].ToString().Trim();
                    p_objSampleVO.m_strBEDNO_CHR = drTemp["bedno_chr"].ToString().Trim();
                    p_objSampleVO.m_strICD_VCHR = drTemp["icd_vchr"].ToString().Trim();
                    p_objSampleVO.m_strPATIENTCARDID_CHR = drTemp["patientcardid_chr"].ToString().Trim();
                    p_objSampleVO.m_strBARCODE_VCHR = drTemp["barcode_vchr"].ToString().Trim();
                    p_objSampleVO.m_strSAMPLE_ID_CHR = drTemp["sample_id_chr"].ToString().Trim();
                    p_objSampleVO.m_strPATIENTID_CHR = drTemp["patientid_chr"].ToString().Trim();
                    p_objSampleVO.m_strSAMPLING_DATE_DAT = drTemp["sampling_date_dat"].ToString().Trim();
                    p_objSampleVO.m_strOPERATOR_ID_CHR = drTemp["operator_id_chr"].ToString().Trim();
                    p_objSampleVO.m_strMODIFY_DAT = drTemp["modify_dat"].ToString().Trim();
                    p_objSampleVO.m_strAPPL_EMPID_CHR = drTemp["appl_empid_chr"].ToString().Trim();
                    p_objSampleVO.m_strAPPL_DEPTID_CHR = drTemp["appl_deptid_chr"].ToString().Trim();
                    p_objSampleVO.m_intSTATUS_INT =Convert.ToInt32(drTemp["status_int"]);
                    p_objSampleVO.m_strSAMPLE_TYPE_ID_CHR = drTemp["sample_type_id_chr"].ToString().Trim();
                    p_objSampleVO.m_strQCSAMPLEID_CHR = drTemp["qcsampleid_chr"].ToString().Trim();
                    p_objSampleVO.m_strSAMPLEKIND_CHR = drTemp["samplekind_chr"].ToString().Trim();
                    p_objSampleVO.m_strCHECK_DATE_DAT = drTemp["check_date_dat"].ToString().Trim();
                    p_objSampleVO.m_strCHECKER_ID_CHR = drTemp["checker_id_chr"].ToString().Trim();

                    p_objSampleVO.m_strACCEPT_DAT = drTemp["accept_dat"].ToString().Trim();
                    p_objSampleVO.m_strACCEPTOR_ID_CHR = drTemp["acceptor_id_chr"].ToString().Trim();
                    p_objSampleVO.m_strAPPLICATION_ID_CHR = drTemp["application_id_chr"].ToString().Trim();
                    p_objSampleVO.m_strPATIENT_INHOSPITALNO_CHR = drTemp["patient_inhospitalno_chr"].ToString().Trim();

                    p_objSampleVO.m_strCOLLECTOR_ID_CHR = drTemp["collector_id_chr"].ToString().Trim();
                    //p_objSampleVO.m_strApplBackDate = drTemp["applybackdate"].ToString().Trim();
                    //p_objSampleVO.m_strApplBackEmpID = drTemp["applybackempid"].ToString().Trim();
                    //p_objSampleVO.m_strChangeInfoEmpID = drTemp["changesinfoempid"].ToString().Trim();
                }

            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                objHRPServ = null;
                p_strBarCode_vchr = null;
            }
            return lngRes;
        }

        #endregion



        #region 通过条码号获取仪器检验序号
        /// <summary>
        /// 通过条码号获取仪器检验序号
        /// </summary>
        /// <param name="p_strBarcode"></param>
        /// <param name="p_strCheckNO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckNOByBarcode(string p_strBarcode, out string p_strCheckNO)
        {
            p_strCheckNO = null;
            long lngRes = 0;
            string strSQL = null;
            clsHRPTableService objHRPSvc = null;
            try
            {
                strSQL = @"select a.idx_int, a.status_int
  from t_opr_lis_xe5000 a
 where a.sample_id = ?";
                IDataParameter[] objDPArr = null;
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strBarcode;
                DataTable dtResult = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_strCheckNO = dtResult.Rows[0]["idx_int"].ToString().Trim();
                }

            }
            catch(Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, false);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                strSQL = null;
            }
            return lngRes;
        }
        #endregion
    }
}
