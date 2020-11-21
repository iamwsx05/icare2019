using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using weCare.Core.Dac;

namespace com.digitalwave.iCare.middletier.LIS
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsLIS_Svc : com.digitalwave.iCare.middletier.clsMiddleTierBase//MiddleTierBase.dll
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
       d.dataacquisitioncomputerip_chr
  from t_bse_lis_serialsetup  s,
       t_bse_lis_device       d,
       t_bse_lis_device_model m
 where d.device_model_id_chr = s.device_model_id_chr
   and d.device_model_id_chr = m.device_model_id_chr
   and d.end_date_dat is null
";

        private const string c_strAddLabResult = @"insert into t_opr_lis_result
  (idx_int,deviceid_chr,device_sampleid_chr,check_dat,device_check_item_name_vchr,
   result_vchr,unit_vchr,refrange_vchr,min_val_dec,max_val_dec,
   abnormal_flag_vchr,graph_img,graph_format_name_vchr,is_graph_result_num,result2_vchr,
   doctorexpress, barcode)
values
  (?, ?, ?, ?, ?,
   ?, ?, ?, ?, ?,
   ?, ?, ?, ?, ?,
   ?, ?)";

        private const string c_strAddLabResultLog = @"insert into t_opr_lis_result_log
  (deviceid_chr,
   device_sampleid_chr,
   check_dat,
   begin_idx_int,
   end_idx_int,
   use_flag_chr,
   import_req_int)
values
  (?, ?, ?, ?, ?, ?, ?)";

        private const string c_strAddResultImportReq = @"insert into t_opr_lis_result_import_req
  (deviceid_chr,
   import_req_int,
   device_sampleid_chr,
   check_dat,
   status_int,
   is_autobind_endpointer_int,
   recheck_flag_chr)
values
  (?, ?, ?, ?, ?, ?, ?)";

        private const string c_strUpdateImportReq = @"update t_opr_lis_result_import_req
   set recheck_flag_chr = 0
 where deviceid_chr = ?
   and import_req_int = ?";

        public clsLIS_Svc()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region comment
        //[AutoComplete]
        //public long lngGetInstrumentSerialSetting(string strData_Acquisition_Computer_IP, out clsLIS_Equip_ConfigVO[] objConfig_List)
        //{
        //    string strSQL = c_strGetInstrumentSerialSetting + "   and lower(d.dataacquisitioncomputerip_chr) = lower(?)";

        //    objConfig_List = null;

        //    long lngRes = 0;
        //    try
        //    {
        //        System.Data.DataTable objDT_LIS_Instrument_Info = null;
        //        IDataParameter[] objDPArr = null;
        //        com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
        //        objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
        //        objDPArr[0].Value = strData_Acquisition_Computer_IP;


        //        lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref objDT_LIS_Instrument_Info, objDPArr);
        //        objHRPSvc.Dispose();

        //        if(lngRes > 0)
        //        {
        //            int intRowCount = objDT_LIS_Instrument_Info.Rows.Count;
        //            if (intRowCount > 0)
        //            {
        //                objConfig_List = new clsLIS_Equip_ConfigVO[intRowCount];
        //                clsLIS_Equip_ConfigVO objTemp = null;
        //                System.Data.DataRow objRow = null;
        //                for (int i = 0; i < intRowCount; i++)
        //                {
        //                    objRow = objDT_LIS_Instrument_Info.Rows[i];
        //                    objTemp = new clsLIS_Equip_ConfigVO();

        //                    if (objRow["DEVICEID_CHR"] != System.DBNull.Value)
        //                    { objTemp.strLIS_Instrument_ID = objRow["DEVICEID_CHR"].ToString().Trim(); }

        //                    if (objRow["DEVICENAME_VCHR"] != System.DBNull.Value)
        //                    { objTemp.strLIS_Instrument_Name = objRow["DEVICENAME_VCHR"].ToString().Trim(); }

        //                    if (objRow["DEVICE_MODEL_DESC_VCHR"] != System.DBNull.Value)
        //                    { objTemp.strLIS_Instrument_Model = objRow["DEVICE_MODEL_DESC_VCHR"].ToString().Trim(); }

        //                    if (objRow["COMNO_CHR"] != System.DBNull.Value)
        //                    { objTemp.strCOM_No = objRow["COMNO_CHR"].ToString().Trim(); }

        //                    if (objRow["BAULRATE_CHR"] != System.DBNull.Value)
        //                    { objTemp.strBaud_Rate = objRow["BAULRATE_CHR"].ToString().Trim(); }

        //                    if (objRow["DATABIT_CHR"] != System.DBNull.Value)
        //                    { objTemp.strData_Bit = objRow["DATABIT_CHR"].ToString().Trim(); }

        //                    if (objRow["STOPBIT_CHR"] != System.DBNull.Value)
        //                    { objTemp.strStop_Bit = objRow["STOPBIT_CHR"].ToString().Trim(); }

        //                    if (objRow["PARITY_CHR"] != System.DBNull.Value)
        //                    { objTemp.strParity = objRow["PARITY_CHR"].ToString().Trim(); }

        //                    if (objRow["FLOWCONTROL_CHR"] != System.DBNull.Value)
        //                    { objTemp.strFlow_Control = objRow["FLOWCONTROL_CHR"].ToString().Trim(); }

        //                    if (objRow["RECEIVEBUFFER_CHR"] != System.DBNull.Value)
        //                    { objTemp.strReceive_Buffer = objRow["RECEIVEBUFFER_CHR"].ToString().Trim(); }

        //                    if (objRow["SENDBUFFER_CHR"] != System.DBNull.Value)
        //                    { objTemp.strSend_Buffer = objRow["SENDBUFFER_CHR"].ToString().Trim(); }

        //                    if (objRow["SENDCOMMAND_CHR"] != System.DBNull.Value)
        //                    { objTemp.strSend_Command = objRow["SENDCOMMAND_CHR"].ToString(); }

        //                    if (objRow["SENDCOMMANDINTERNAL_CHR"] != System.DBNull.Value)
        //                    { objTemp.strSend_Command_Internal = objRow["SENDCOMMANDINTERNAL_CHR"].ToString().Trim(); }

        //                    if (objRow["DATAANALYSISDLL_VCHR"] != System.DBNull.Value)
        //                    { objTemp.strData_Analysis_DLL = objRow["DATAANALYSISDLL_VCHR"].ToString().Trim(); }

        //                    if (objRow["DATAANALYSISNAMESPACE_VCHR"] != System.DBNull.Value)
        //                    { objTemp.strData_Analysis_Namespace = objRow["DATAANALYSISNAMESPACE_VCHR"].ToString().Trim(); }

        //                    if (objRow["DATAACQUISITIONCOMPUTERIP_CHR"] != System.DBNull.Value)
        //                    { objTemp.strData_Acquisition_Computer_IP = objRow["DATAACQUISITIONCOMPUTERIP_CHR"].ToString().Trim(); }

        //                    objConfig_List[i] = objTemp;
        //                }
        //            }
        //        }
        //    }
        //    catch(System.Exception objEx)
        //    {
        //        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //        bool blnRes = objLogger.LogDetailError(objEx, true);
        //    }
        //    return lngRes;
        //}

        //private void ConstructDeviceVO(System.Data.DataRow objRow, ref clsLIS_Equip_ConfigVO objDeviceVO)
        //{

        //}
        #endregion

        #region ��ȡָ�������ŵļ�����Ŀͨ���ţ������ü�����
        /// <summary>
        /// ��ȡָ�������ŵļ�����Ŀͨ���ţ������ü�����
        /// </summary>
        /// <param name="p_strCheckSampleNO">����������</param>
        /// <param name="p_strSampleID">����ID</param>
        /// <param name="p_strDeviceID">����ID</param>
        /// <param name="p_strDeviceNO">��������</param>
        /// <param name="p_strCheckItemstring"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSampleCheckItems(string p_strCheckSampleNO, string p_strSampleID, string p_strDeviceID, string p_strDeviceNO, out string p_strCheckItemstring)
        {
            p_strCheckItemstring = null;
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strCheckSampleNO) || string.IsNullOrEmpty(p_strDeviceID) || string.IsNullOrEmpty(p_strDeviceNO))
                return lngRes;

            clsHRPTableService objHRPServ = null;

            try
            {
                string strSQL;
                objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                DataTable dtResult = null;

                if (string.IsNullOrEmpty(p_strSampleID))
                {
                    strSQL = @"select d.device_check_item_no_vchr
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

                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = DateTime.Now.Date;
                    objDPArr[1].Value = p_strDeviceNO + p_strCheckSampleNO;

                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                }
                else
                {
                    strSQL = @"select d.device_check_item_no_vchr
  from t_opr_lis_sample a
 inner join t_opr_lis_app_check_item b on b.application_id_chr =
                                          a.application_id_chr
 inner join t_bse_lis_check_item_dev_item c on c.check_item_id_chr =
                                               b.check_item_id_chr
 inner join t_bse_lis_device_check_item d on d.device_check_item_id_chr =
                                             c.device_check_item_id_chr
                                         and d.device_model_id_chr =
                                             c.device_model_id_chr
 where a.status_int >= 3
   and a.appl_dat > ?
   and a.barcode_vchr = ?
 order by d.device_check_item_no_vchr";

                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = DateTime.Now.Date;
                    objDPArr[1].Value = p_strSampleID;

                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                }

                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    p_strCheckItemstring = "";
                    for (int idx = 0; idx < dtResult.Rows.Count; idx++)
                    {
                        p_strCheckItemstring += dtResult.Rows[idx]["device_check_item_no_vchr"].ToString().Trim();
                    }

                    clsT_LIS_DeviceRelationVO objDevRelation = new clsT_LIS_DeviceRelationVO();
                    objDevRelation.m_strDEVICEID_CHR = p_strDeviceID;
                    objDevRelation.m_strDEVICE_SAMPLEID_CHR = p_strCheckSampleNO;
                    objDevRelation.m_strCHECK_DAT = DateTime.Now.ToLongTimeString();
                    objDevRelation.m_strRECEPTION_DAT = DateTime.Now.ToLongTimeString();
                    objDevRelation.m_strSAMPLE_ID_CHR = p_strSampleID;
                    objDevRelation.m_intSTATUS_INT = 1;

                    clsSampleSvc objServ = new clsSampleSvc();
                    lngRes = objServ.m_lngDelDevicRelation(p_strSampleID);
                    if (lngRes <= 0)
                    {
                        ContextUtil.SetAbort();
                        return lngRes;
                    }

                    lngRes = objServ.m_lngAddNewDeviceRelation(objDevRelation);
                    if (lngRes <= 0)
                    {
                        ContextUtil.SetAbort();
                        return lngRes;
                    }

                    strSQL = @"update t_opr_lis_application a
   set a.application_form_no_chr = ?
 where a.pstatus_int > 0
   and a.application_id_chr in
       (select b.application_id_chr
          from t_opr_lis_sample b
         where b.status_int > 0
           and b.sample_id_chr = ?)";

                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_strDeviceNO + p_strCheckSampleNO;
                    objDPArr[1].Value = p_strSampleID;

                    long lngAffect = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffect, objDPArr);
                    if (lngRes <= 0)
                    {
                        ContextUtil.SetAbort();
                        return lngRes;
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

        #endregion

        [AutoComplete]
        public long lngAddLabResult(List<clsLIS_Device_Test_ResultVO> arlResult, out List<clsLIS_Device_Test_ResultVO> p_arlResultOut)
        {
            p_arlResultOut = null;
            clsLIS_Device_Test_ResultVO[] objResultArr = arlResult.ToArray();
            clsLIS_Device_Test_ResultVO[] objResultOutArr = null;

            long lngRes = lngAddLabResult(objResultArr, out objResultOutArr);
            if (lngRes > 0)
            {
                p_arlResultOut = new List<clsLIS_Device_Test_ResultVO>();
                p_arlResultOut.AddRange(objResultOutArr);
            }
            return lngRes;

            #region ����

            //            p_arlResultOut = null;
            //            System.Collections.Hashtable has = new System.Collections.Hashtable();
            //            if (arlResult == null || arlResult.Count <= 0)
            //            {
            //                return -1;
            //            }
            //            //<--------------------------------------------------------  baojian.mo add in 2007.11.22
            //            else
            //            {
            //                foreach (clsLIS_Device_Test_ResultVO objRes in arlResult)
            //                {
            //                    has.Add(objRes.strDevice_Check_Item_Name, objRes.strResult);
            //                }
            //            }
            //            //--------------------------------------------------------------------------------------->

            //            long lngRes = 1;
            //            long lngRecEff = -1;
            //            string strSQL = null;

            //            #region �ж���׷�ӽ����������һ�ν�� ����Stago��CentaurCP������ baojian.mo add
            //            /* ԭ����ȡ�ϴ�ȡ�ø�����������������
            //               �������У������ͱ걾�Ų���log�����ʼֵ
            //               �ҵ�������н��У�Թ�ϣ�Ƚϣ������һ��������������ԭ����
            //               ����׷�Ӳ��������޸�req����.log��ʼֵ������ */
            //            //<--------------------------------------------------------  baojian.mo add in 2007.11.22
            //            string[] strConditionList = null; //��blnFlagΪfalseʱ��ֵ
            //            bool blnFlag = this.m_blnIsAppendResult(ref has, arlResult, out strConditionList);            
            //            //--------------------------------------------------------------------------------------->
            //            #endregion

            //            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //            try
            //            {
            //                clsLIS_Device_Test_ResultVO[] objResultList = (clsLIS_Device_Test_ResultVO[])arlResult.ToArray(typeof(clsLIS_Device_Test_ResultVO));


            //                DateTime dtmNow = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //                DateTime dtmCheck = dtmNow;
            //                string strCheckDateTime = null;
            //                #region ����ʱ��
            //                if (objResultList[0].strCheck_Date != null)
            //                    objResultList[0].strCheck_Date = objResultList[0].strCheck_Date.Trim();

            //                if (Microsoft.VisualBasic.Information.IsDate(objResultList[0].strCheck_Date))
            //                {
            //                    dtmCheck = System.DateTime.Parse(objResultList[0].strCheck_Date);
            //                }
            //                strCheckDateTime = dtmCheck.ToString("yyyy-MM-dd HH:mm:ss");
            //                dtmCheck = Convert.ToDateTime(strCheckDateTime);
            //                #endregion
            //                string strDid = objResultList[0].strDevice_ID.Trim();
            //                string strSid = objResultList[0].strDevice_Sample_ID.Trim();
            //                clsLogText objLogger = new clsLogText();

            //                int intImportReq = -1;
            //                if (blnFlag)
            //                {
            //                    objLogger.Log2File("D:\\logData.txt", "�������");
            //                    if (lngRes == 1)
            //                    {
            //                        #region �õ����������Ĳɼ����
            //                        DataTable dtbReq = null;
            //                        strSQL = @"select max(import_req_int) + 1 as import_req_int
            //  from t_opr_lis_result_import_req
            // where deviceid_chr = ?
            // group by deviceid_chr";

            //                        System.Data.IDataParameter[] objDPArr4 = null;
            //                        objHRPSvc.CreateDatabaseParameter(1, out objDPArr4);
            //                        objDPArr4[0].Value = objResultList[0].strDevice_ID;

            //                        lngRes = 0;
            //                        lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbReq, objDPArr4);
            //                        if (lngRes == 1 && dtbReq != null && dtbReq.Rows.Count != 0)
            //                        {
            //                            intImportReq = int.Parse(dtbReq.Rows[0]["import_req_int"].ToString());
            //                        }
            //                        else if (lngRes == 1)
            //                        {
            //                            intImportReq = 0;
            //                        }
            //                        else
            //                        {
            //                            lngRes = 0;
            //                        }
            //                        #endregion
            //                    }
            //                    if (lngRes == 1)
            //                    {
            //                        #region д t_opr_lis_result_import_req ��
            //                        System.Data.IDataParameter[] objDPArr5 = null;
            //                        objHRPSvc.CreateDatabaseParameter(7, out objDPArr5);
            //                        objDPArr5[0].Value = strDid;
            //                        objDPArr5[1].Value = intImportReq;
            //                        objDPArr5[2].Value = strSid;
            //                        objDPArr5[3].DbType = DbType.DateTime;
            //                        objDPArr5[3].Value = Convert.ToDateTime(strCheckDateTime);
            //                        objDPArr5[4].Value = 1;
            //                        objDPArr5[5].Value = 0;
            //                        objDPArr5[6].Value = "0";
            //                        lngRes = 0;
            //                        lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddResultImportReq, ref lngRecEff, objDPArr5);
            //                        #endregion
            //                    }
            //                    if (lngRes == 1)
            //                    {
            //                        int intIdx = m_mthGetNewResultIndex(objResultList.Length, false) + 1;
            //                        int intBEGIN_IDX_INT = intIdx;
            //                        int intEND_IDX_INT = intBEGIN_IDX_INT + objResultList.Length - 1;

            //                        #region д t_opr_lis_result ��
            //                        DbType[] m_dbType = new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.DateTime,
            //                            DbType.String, DbType.String, DbType.String, DbType.String,
            //                            DbType.Double, DbType.Double, DbType.String, DbType.Object,
            //                            DbType.String, DbType.Int32 };

            //                        object[][] objValues = new object[14][];
            //                        for (int i = 0; i < objValues.Length; i++)
            //                        {
            //                            objValues[i] = new object[objResultList.Length];
            //                        }
            //                        clsLIS_Device_Test_ResultVO objResultTemp = null;
            //                        for (int iRow = 0; iRow < objResultList.Length; iRow++)
            //                        {
            //                            objResultTemp = objResultList[iRow];
            //                            objResultTemp.intIndex = intIdx++;

            //                            objValues[0][iRow] = objResultTemp.intIndex;
            //                            objValues[1][iRow] = strDid;
            //                            objValues[2][iRow] = strSid;

            //                            objResultTemp.strCheck_Date = strCheckDateTime;
            //                            objValues[3][iRow] = Convert.ToDateTime(strCheckDateTime);
            //                            objValues[4][iRow] = objResultTemp.strDevice_Check_Item_Name;

            //                            objValues[5][iRow] = objResultTemp.strResult;
            //                            objValues[6][iRow] = objResultTemp.strUnit;
            //                            objValues[7][iRow] = objResultTemp.strRefRange;

            //                            if (objResultTemp.strMinVal != null)
            //                            {
            //                                if (Microsoft.VisualBasic.Information.IsNumeric(objResultTemp.strMinVal.Trim()))
            //                                {
            //                                    objValues[8][iRow] = double.Parse(objResultTemp.strMinVal.Trim());
            //                                }
            //                            }
            //                            if (objResultTemp.strMaxVal != null)
            //                            {
            //                                if (Microsoft.VisualBasic.Information.IsNumeric(objResultTemp.strMaxVal.Trim()))
            //                                {
            //                                    objValues[9][iRow] = double.Parse(objResultTemp.strMaxVal.Trim());
            //                                }
            //                            }

            //                            objValues[10][iRow] = objResultTemp.strAbnormal_Flag;
            //                            objValues[11][iRow] = objResultTemp.bytGraph;
            //                            objValues[12][iRow] = objResultTemp.strGraphFormatName;
            //                            objValues[13][iRow] = objResultTemp.intIsGraphResult;
            //                        }
            //                        lngRes = 0;
            //                        lngRes = objHRPSvc.m_lngSaveArrayWithParameters(c_strAddLabResult, objValues, m_dbType);

            //                        #endregion

            //                        #region д t_opr_lis_result �� ����
            //                        //for (int i = 0; i < objResultList.Length; i++)
            //                        //{
            //                        //    #region д t_opr_lis_result ��

            //                        //    System.Data.IDataParameter[] objDPArr = null;
            //                        //    objHRPSvc.CreateDatabaseParameter(14, out objDPArr);

            //                        //    //							objResultList[i].intIndex = objHRPSvc.intGetNewNumericID("IDX_INT", "T_OPR_LIS_RESULT");
            //                        //    objResultList[i].intIndex = intIdx++;

            //                        //    objDPArr[0].Value = objResultList[i].intIndex;

            //                        //    objDPArr[1].Value = strDid;
            //                        //    objDPArr[2].Value = strSid;

            //                        //    objResultList[i].strCheck_Date = strCheckDateTime;
            //                        //    objDPArr[3].DbType = DbType.DateTime;
            //                        //    objDPArr[3].Value = Convert.ToDateTime(strCheckDateTime);

            //                        //    objDPArr[4].Value = objResultList[i].strDevice_Check_Item_Name;
            //                        //    objDPArr[5].Value = objResultList[i].strResult;
            //                        //    objDPArr[6].Value = objResultList[i].strUnit;
            //                        //    objDPArr[7].Value = objResultList[i].strRefRange;
            //                        //    if (objResultList[i].strMinVal != null)
            //                        //    {
            //                        //        if (Microsoft.VisualBasic.Information.IsNumeric(objResultList[i].strMinVal.Trim()))
            //                        //        {
            //                        //            objDPArr[8].Value = double.Parse(objResultList[i].strMinVal.Trim());
            //                        //        }
            //                        //    }
            //                        //    if (objResultList[i].strMaxVal != null)
            //                        //    {
            //                        //        if (Microsoft.VisualBasic.Information.IsNumeric(objResultList[i].strMaxVal.Trim()))
            //                        //        {
            //                        //            objDPArr[9].Value = double.Parse(objResultList[i].strMaxVal.Trim());
            //                        //        }
            //                        //    }

            //                        //    objDPArr[10].Value = objResultList[i].strAbnormal_Flag;
            //                        //    objDPArr[11].Value = objResultList[i].bytGraph;
            //                        //    objDPArr[12].Value = objResultList[i].strGraphFormatName;
            //                        //    objDPArr[13].Value = objResultList[i].intIsGraphResult;

            //                        //    lngRes = 0;
            //                        //    lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddLabResult, ref lngRecEff, objDPArr);
            //                        //    if (lngRes <= 0)
            //                        //    {
            //                        //        break;
            //                        //    }

            //                        //    #endregion
            //                        //}
            //                        #endregion

            //                        if (lngRes == 1)
            //                        {
            //                            #region д t_opr_lis_result_log ��

            //                            System.Data.IDataParameter[] objDPArr1 = null;
            //                            objHRPSvc.CreateDatabaseParameter(7, out objDPArr1);
            //                            objDPArr1[0].Value = strDid;
            //                            objDPArr1[1].Value = strSid;
            //                            objDPArr1[2].DbType = DbType.DateTime;
            //                            objDPArr1[2].Value = Convert.ToDateTime(strCheckDateTime);
            //                            objDPArr1[3].Value = intBEGIN_IDX_INT;
            //                            objDPArr1[4].Value = intEND_IDX_INT;
            //                            objDPArr1[5].Value = "1";
            //                            objDPArr1[6].Value = intImportReq;

            //                            lngRes = 0;
            //                            lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddLabResultLog, ref lngRecEff, objDPArr1);

            //                            #endregion
            //                        }
            //                    }
            //                }
            //                else //׷�ӽ��AppendResult
            //                {
            //                    objLogger.Log2File("D:\\logData.txt", "���׷��");

            //                    #region д t_opr_lis_result ��
            //                    strCheckDateTime = strConditionList[3].Trim();
            //                    dtmCheck = Convert.ToDateTime(strCheckDateTime);
            //                    intImportReq = Convert.ToInt32(strConditionList[2]);
            //                    int intIdx = m_mthGetNewResultIndex(objResultList.Length, false) + 1;
            //                    int intBEGIN_IDX_INT = intIdx;
            //                    int intEND_IDX_INT = intBEGIN_IDX_INT + objResultList.Length - 1;


            //                    DbType[] m_dbType1 = new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.DateTime,
            //                            DbType.String, DbType.String, DbType.String, DbType.String,
            //                            DbType.Double, DbType.Double, DbType.String, DbType.Object,
            //                            DbType.String, DbType.Int32 };

            //                    object[][] objValues1 = new object[14][];
            //                    for (int i = 0; i < objValues1.Length; i++)
            //                    {
            //                        objValues1[i] = new object[objResultList.Length];
            //                    }
            //                    clsLIS_Device_Test_ResultVO objResultTemp = null;
            //                    for (int iRow = 0; iRow < objResultList.Length; iRow++)
            //                    {
            //                        objResultTemp = objResultList[iRow];
            //                        objResultTemp.intIndex = intIdx++;

            //                        objValues1[0][iRow] = objResultTemp.intIndex;
            //                        objValues1[1][iRow] = strDid;
            //                        objValues1[2][iRow] = strSid;

            //                        objResultTemp.strCheck_Date = strCheckDateTime;
            //                        objValues1[3][iRow] = Convert.ToDateTime(strCheckDateTime);
            //                        objValues1[4][iRow] = objResultTemp.strDevice_Check_Item_Name;

            //                        objValues1[5][iRow] = objResultTemp.strResult;
            //                        objValues1[6][iRow] = objResultTemp.strUnit;
            //                        objValues1[7][iRow] = objResultTemp.strRefRange;

            //                        if (objResultTemp.strMinVal != null)
            //                        {
            //                            if (Microsoft.VisualBasic.Information.IsNumeric(objResultTemp.strMinVal.Trim()))
            //                            {
            //                                objValues1[8][iRow] = double.Parse(objResultTemp.strMinVal.Trim());
            //                            }
            //                        }
            //                        if (objResultTemp.strMaxVal != null)
            //                        {
            //                            if (Microsoft.VisualBasic.Information.IsNumeric(objResultTemp.strMaxVal.Trim()))
            //                            {
            //                                objValues1[9][iRow] = double.Parse(objResultTemp.strMaxVal.Trim());
            //                            }
            //                        }

            //                        objValues1[10][iRow] = objResultTemp.strAbnormal_Flag;
            //                        objValues1[11][iRow] = objResultTemp.bytGraph;
            //                        objValues1[12][iRow] = objResultTemp.strGraphFormatName;
            //                        objValues1[13][iRow] = objResultTemp.intIsGraphResult;
            //                    }
            //                    lngRes = 0;
            //                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(c_strAddLabResult, objValues1, m_dbType1);

            //                    #endregion

            //                    #region д t_opr_lis_result �� ����
            //                    //strCheckDateTime = strConditionList[3].Trim();
            //                    //dtmCheck = Convert.ToDateTime(strCheckDateTime);
            //                    //intImportReq = Convert.ToInt32(strConditionList[2]);
            //                    //int intIdx = m_mthGetNewResultIndex(objResultList.Length, false) + 1;
            //                    //int intBEGIN_IDX_INT = intIdx;
            //                    //int intEND_IDX_INT = intBEGIN_IDX_INT + objResultList.Length - 1;
            //                    //for (int i = 0; i < objResultList.Length; i++)
            //                    //{
            //                    //    System.Data.IDataParameter[] objDPArr = null;
            //                    //    objHRPSvc.CreateDatabaseParameter(14, out objDPArr);

            //                    //    objResultList[i].intIndex = intIdx++;

            //                    //    objDPArr[0].Value = objResultList[i].intIndex;

            //                    //    objDPArr[1].Value = strConditionList[1]; //����ID
            //                    //    objDPArr[2].Value = strConditionList[0]; //��������ID

            //                    //    objResultList[i].strCheck_Date = strConditionList[3]; //��������
            //                    //    objDPArr[3].Value = strConditionList[3]; //��������

            //                    //    objDPArr[4].Value = objResultList[i].strDevice_Check_Item_Name;
            //                    //    objDPArr[5].Value = objResultList[i].strResult;
            //                    //    objDPArr[6].Value = objResultList[i].strUnit;
            //                    //    objDPArr[7].Value = objResultList[i].strRefRange;
            //                    //    if (objResultList[i].strMinVal != null)
            //                    //    {
            //                    //        if (Microsoft.VisualBasic.Information.IsNumeric(objResultList[i].strMinVal.Trim()))
            //                    //        {
            //                    //            objDPArr[8].Value = double.Parse(objResultList[i].strMinVal.Trim());
            //                    //        }
            //                    //    }
            //                    //    if (objResultList[i].strMaxVal != null)
            //                    //    {
            //                    //        if (Microsoft.VisualBasic.Information.IsNumeric(objResultList[i].strMaxVal.Trim()))
            //                    //        {
            //                    //            objDPArr[9].Value = double.Parse(objResultList[i].strMaxVal.Trim());
            //                    //        }
            //                    //    }

            //                    //    objDPArr[10].Value = objResultList[i].strAbnormal_Flag;
            //                    //    objDPArr[11].Value = objResultList[i].bytGraph;
            //                    //    objDPArr[12].Value = objResultList[i].strGraphFormatName;
            //                    //    objDPArr[13].Value = objResultList[i].intIsGraphResult;

            //                    //    lngRes = 0;
            //                    //    lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddLabResult, ref lngRecEff, objDPArr);
            //                    //    if (lngRes <= 0)
            //                    //    {
            //                    //        break;
            //                    //    }
            //                    //}
            //                    #endregion

            //                    if (lngRes == 1)
            //                    {
            //                        #region ���� t_opr_lis_result_log ��
            //                        strSQL = @"update t_opr_lis_result_log
            //   set end_idx_int = ?
            // where deviceid_chr = ?
            //   and trim(device_sampleid_chr) = ?
            //   and check_dat = ?
            //   and import_req_int = ?";

            //                        System.Data.IDataParameter[] objDPArr1 = null;
            //                        objHRPSvc.CreateDatabaseParameter(5, out objDPArr1);
            //                        objDPArr1[0].Value = intEND_IDX_INT;
            //                        objDPArr1[1].Value = strConditionList[1]; //����ID
            //                        objDPArr1[2].Value = strConditionList[0]; //��������ID
            //                        objDPArr1[3].DbType = DbType.DateTime;
            //                        objDPArr1[3].Value = Convert.ToDateTime(strConditionList[3]); //��������
            //                        objDPArr1[4].Value = Convert.ToInt32(strConditionList[2]); //ϵͳ�ڲ��������

            //                        lngRes = 0;
            //                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDPArr1);
            //                        if (lngRecEff < 1)
            //                        {
            //                            System.EnterpriseServices.ContextUtil.SetAbort();
            //                        }
            //                        #endregion
            //                    }
            //                }
            //                System.Data.DataTable dtbRelation = null;
            //                if (lngRes == 1)//���۴˲���ɹ���񶼲�ӦӰ��ԭʼ���ݵĽ���.
            //                {
            //                    #region  ���Һ��ձ�t_opr_lis_device_relation������Ҫ�������ļ�¼

            //                    strSQL = @"select deviceid_chr, seq_id_device_chr, sample_id_chr
            //  from t_opr_lis_device_relation
            // where status_int = 1
            //   and deviceid_chr = ?
            //   and trim(device_sampleid_chr) = ?
            //   and check_dat between ? and ?";

            //                    System.Data.IDataParameter[] objDPArrs3 = null;
            //                    objHRPSvc.CreateDatabaseParameter(4, out objDPArrs3);
            //                    objDPArrs3[0].Value = strDid;
            //                    objDPArrs3[1].Value = strSid;
            //                    objDPArrs3[2].DbType = DbType.DateTime;
            //                    objDPArrs3[2].Value = (Convert.ToDateTime(strCheckDateTime)).Date;
            //                    objDPArrs3[3].DbType = DbType.DateTime;
            //                    objDPArrs3[3].Value = (Convert.ToDateTime(strCheckDateTime)).Date.AddHours(24);

            //                    lngRes = 0;
            //                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbRelation, objDPArrs3);

            //                    #endregion
            //                    if (lngRes == 1)
            //                    {
            //                        #region ���� t_opr_lis_device_relation ��

            //                        if (dtbRelation != null && dtbRelation.Rows.Count != 0)
            //                        {
            //                            string strSeq = dtbRelation.Rows[0]["seq_id_device_chr"].ToString().Trim();

            //                            strSQL = @"update t_opr_lis_device_relation
            //   set device_sampleid_chr = ?,
            //       check_dat           = ?,
            //       import_req_int      = ?,
            //       status_int          = 2
            // where trim(seq_id_device_chr) = ?
            //   and trim(deviceid_chr) = ?";

            //                            System.Data.IDataParameter[] objDPArrs2 = null;
            //                            objHRPSvc.CreateDatabaseParameter(5, out objDPArrs2);
            //                            objDPArrs2[0].Value = strSid;
            //                            objDPArrs2[1].DbType = DbType.DateTime;
            //                            objDPArrs2[1].Value = Convert.ToDateTime(strCheckDateTime);
            //                            objDPArrs2[2].Value = intImportReq;
            //                            objDPArrs2[3].Value = strSeq.Trim();
            //                            objDPArrs2[4].Value = strDid.Trim();

            //                            lngRecEff = 0;
            //                            lngRes = 0;
            //                            lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDPArrs2);

            //                        }
            //                        #endregion
            //                    }
            //                }
            //            }
            //            catch (System.Exception objEx)
            //            {
            //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //                bool blnRes = objLogger.LogError(objEx);
            //            }
            //            finally
            //            {
            //                objHRPSvc.Dispose();
            //            }
            //            if (lngRes <= 0)
            //            {
            //                ContextUtil.SetAbort();
            //            }
            //            p_arlResultOut = arlResult;
            //            return lngRes;

            #endregion
        }

        [AutoComplete]
        public long lngAddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr)
        {
            Dictionary<string, List<clsLIS_Device_Test_ResultVO>> m_dictSample = new Dictionary<string, List<clsLIS_Device_Test_ResultVO>>();
            foreach (clsLIS_Device_Test_ResultVO objTemp in p_objResultArr)
            {
                if (m_dictSample.ContainsKey(objTemp.strDevice_Sample_ID))
                {
                    m_dictSample[objTemp.strDevice_Sample_ID].Add(objTemp);
                }
                else
                {
                    m_dictSample.Add(objTemp.strDevice_Sample_ID, new List<clsLIS_Device_Test_ResultVO>());
                    m_dictSample[objTemp.strDevice_Sample_ID].Add(objTemp);
                }
            }
            long lngRes = 0;


            foreach (KeyValuePair<string, List<clsLIS_Device_Test_ResultVO>> objTemp in m_dictSample)
            {
                p_objResultArr = objTemp.Value.ToArray();


                p_objOutResultArr = null;
                Dictionary<string, string> has = new Dictionary<string, string>();
                if (p_objResultArr == null || p_objResultArr.Length <= 0)
                {
                    return -1;
                }
                //<-------------------------------------------------------- 
                else
                {
                    foreach (clsLIS_Device_Test_ResultVO objRes in p_objResultArr)
                    {
                        has.Add(objRes.strDevice_Check_Item_Name, objRes.strResult);
                    }
                }
                //--------------------------------------------------------------------------------------->

                lngRes = 1;
                long lngRecEff = -1;
                string strSQL = null;

                #region �ж���׷�ӽ����������һ�ν�� ����Stago��CentaurCP������
                /* ԭ����ȡ�ϴ�ȡ�ø�����������������
               �������У������ͱ걾�Ų���log�����ʼֵ
               �ҵ�������н��У�Թ�ϣ�Ƚϣ������һ��������������ԭ����
               ����׷�Ӳ��������޸�req����.log��ʼֵ������ */
                //<--------------------------------------------------------  
                string[] strConditionList = null; //��blnFlagΪfalseʱ��ֵ
                bool blnFlag = this.m_blnIsAppendResult(ref has, p_objResultArr, out strConditionList);
                //--------------------------------------------------------------------------------------->
                #endregion

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                try
                {
                    clsLIS_Device_Test_ResultVO[] objResultList = p_objResultArr;


                    DateTime dtmNow = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    DateTime dtmCheck = dtmNow;
                    string strCheckDateTime = null;
                    #region ����ʱ��
                    if (objResultList[0].strCheck_Date != null)
                        objResultList[0].strCheck_Date = objResultList[0].strCheck_Date.Trim();

                    if (Microsoft.VisualBasic.Information.IsDate(objResultList[0].strCheck_Date))
                    {
                        dtmCheck = System.DateTime.Parse(objResultList[0].strCheck_Date);
                    }
                    strCheckDateTime = dtmCheck.ToString("yyyy-MM-dd HH:mm:ss");
                    dtmCheck = Convert.ToDateTime(strCheckDateTime);
                    #endregion
                    string strDid = objResultList[0].strDevice_ID.Trim();
                    string strSid = objResultList[0].strDevice_Sample_ID.Trim();
                    clsLogText objLogger = new clsLogText();

                    int intImportReq = -1;
                    if (blnFlag)
                    {
                        //objLogger.Log2File("D:\\logData.txt", "�������");
                        if (lngRes == 1)
                        {
                            #region �õ����������Ĳɼ����
                            DataTable dtbReq = null;
                            strSQL = @"select max(import_req_int) + 1 as import_req_int
  from t_opr_lis_result_import_req
 where deviceid_chr = ?
 group by deviceid_chr";

                            System.Data.IDataParameter[] objDPArr4 = null;
                            objHRPSvc.CreateDatabaseParameter(1, out objDPArr4);
                            objDPArr4[0].Value = objResultList[0].strDevice_ID;

                            lngRes = 0;
                            lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbReq, objDPArr4);
                            if (lngRes == 1 && dtbReq != null && dtbReq.Rows.Count != 0)
                            {
                                intImportReq = int.Parse(dtbReq.Rows[0]["import_req_int"].ToString());
                            }
                            else if (lngRes == 1)
                            {
                                intImportReq = 0;
                            }
                            else
                            {
                                lngRes = 0;
                            }
                            #endregion
                        }
                        if (lngRes == 1)
                        {
                            #region д t_opr_lis_result_import_req ��
                            System.Data.IDataParameter[] objDPArr5 = null;
                            objHRPSvc.CreateDatabaseParameter(7, out objDPArr5);
                            objDPArr5[0].Value = strDid;
                            objDPArr5[1].Value = intImportReq;
                            objDPArr5[2].Value = strSid;
                            objDPArr5[3].DbType = DbType.DateTime;
                            objDPArr5[3].Value = Convert.ToDateTime(strCheckDateTime);
                            objDPArr5[4].Value = 1;
                            objDPArr5[5].Value = 0;
                            objDPArr5[6].Value = "0";
                            lngRes = 0;
                            lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddResultImportReq, ref lngRecEff, objDPArr5);
                            #endregion
                        }
                        if (lngRes == 1)
                        {
                            //int intIdx = m_mthGetNewResultIndex(objResultList.Length, false) + 1;

                            int[] intIdxArr = null;
                            // 2019-11-22: ����9i/11g����11g server�쳣����ʱ�� seq_lis_result_11g ��� seq_lis_result  --> ȡ�� 2020-1-13
                            lngRes = m_lngGetSequenceArr("seq_lis_result", objResultList.Length, out intIdxArr);
                            if (lngRes <= 0 || intIdxArr == null)
                            {
                                return lngRes;
                            }
                            if (intIdxArr.Length <= 0)
                            {
                                return -1;
                            }
                            int maxIdx = 0;
                            int minIdx = 0;
                            #region д t_opr_lis_result ��
                            DbType[] m_dbType = new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.DateTime,
                            DbType.String, DbType.String, DbType.String, DbType.String,
                            DbType.Double, DbType.Double, DbType.String, DbType.Byte,
                            DbType.String, DbType.Int32,DbType.String,DbType.String,DbType.String };

                            object[][] objValues = new object[m_dbType.Length][];
                            for (int i = 0; i < objValues.Length; i++)
                            {
                                objValues[i] = new object[objResultList.Length];
                            }
                            clsLIS_Device_Test_ResultVO objResultTemp = null;
                            for (int iRow = 0; iRow < objResultList.Length; iRow++)
                            {
                                objResultTemp = objResultList[iRow];
                                objResultTemp.intIndex = intIdxArr[iRow];

                                if (iRow == 0)
                                {
                                    minIdx = objResultTemp.intIndex;
                                }
                                minIdx = Math.Min(minIdx, objResultTemp.intIndex);
                                maxIdx = Math.Max(maxIdx, objResultTemp.intIndex);

                                objValues[0][iRow] = objResultTemp.intIndex;
                                objValues[1][iRow] = strDid;
                                objValues[2][iRow] = strSid;

                                objResultTemp.strCheck_Date = strCheckDateTime;
                                objValues[3][iRow] = Convert.ToDateTime(strCheckDateTime);
                                objValues[4][iRow] = objResultTemp.strDevice_Check_Item_Name;

                                objValues[5][iRow] = objResultTemp.strResult;
                                objValues[6][iRow] = objResultTemp.strUnit;
                                objValues[7][iRow] = objResultTemp.strRefRange;

                                if (objResultTemp.strMinVal != null)
                                {
                                    if (Microsoft.VisualBasic.Information.IsNumeric(objResultTemp.strMinVal.Trim()))
                                    {
                                        objValues[8][iRow] = double.Parse(objResultTemp.strMinVal.Trim());
                                    }
                                }
                                if (objResultTemp.strMaxVal != null)
                                {
                                    if (Microsoft.VisualBasic.Information.IsNumeric(objResultTemp.strMaxVal.Trim()))
                                    {
                                        objValues[9][iRow] = double.Parse(objResultTemp.strMaxVal.Trim());
                                    }
                                }

                                objValues[10][iRow] = objResultTemp.strAbnormal_Flag;
                                objValues[11][iRow] = objResultTemp.bytGraph;
                                objValues[12][iRow] = objResultTemp.strGraphFormatName;
                                objValues[13][iRow] = objResultTemp.intIsGraphResult;
                                objValues[14][iRow] = objResultTemp.strResult2;
                                objValues[15][iRow] = objResultTemp.strDoctorExpress;
                                objValues[16][iRow] = objResultTemp.barCode;
                            }
                            lngRes = 0;
                            lngRes = objHRPSvc.m_lngSaveArrayWithParameters(c_strAddLabResult, objValues, m_dbType);

                            #endregion

                            //if (lngRes == 1)
                            //{
                            #region д t_opr_lis_result_log ��

                            System.Data.IDataParameter[] objDPArr1 = null;
                            objHRPSvc.CreateDatabaseParameter(7, out objDPArr1);
                            objDPArr1[0].Value = strDid;
                            objDPArr1[1].Value = strSid;
                            objDPArr1[2].DbType = DbType.DateTime;
                            objDPArr1[2].Value = Convert.ToDateTime(strCheckDateTime);
                            objDPArr1[3].Value = minIdx;
                            objDPArr1[4].Value = maxIdx;
                            objDPArr1[5].Value = "1";
                            objDPArr1[6].Value = intImportReq;

                            if (strDid == "000041" || strDid == "000047")
                            {
                                Log.Output("new: " + strDid + " importReqId:" + intImportReq.ToString() + " beginIdx:" + minIdx.ToString() + "  endIdx:" + maxIdx.ToString());
                            }

                            lngRes = 0;
                            lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddLabResultLog, ref lngRecEff, objDPArr1);

                            #endregion
                            //}
                        }
                    }
                    else //׷�ӽ��AppendResult
                    {
                        //objLogger.Log2File("D:\\logData.txt", "���׷��");

                        #region д t_opr_lis_result ��
                        strCheckDateTime = strConditionList[3].Trim();
                        dtmCheck = Convert.ToDateTime(strCheckDateTime);
                        intImportReq = Convert.ToInt32(strConditionList[2]);
                        //int intIdx = m_mthGetNewResultIndex(objResultList.Length, false) + 1;
                        int[] intIdxArr = null;
                        // 2019-11-22: ����9i/11g����11g server�쳣����ʱ�� seq_lis_result_11g ��� seq_lis_result -->ȡ�� 2020-1-13
                        lngRes = m_lngGetSequenceArr("seq_lis_result", objResultList.Length, out intIdxArr);
                        if (lngRes <= 0 || intIdxArr == null)
                        {
                            return lngRes;
                        }
                        if (intIdxArr.Length <= 0)
                        {
                            return -1;
                        }
                        int maxIdx2 = 0;

                        DbType[] m_dbType1 = new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.DateTime,
                            DbType.String, DbType.String, DbType.String, DbType.String,
                            DbType.Double, DbType.Double, DbType.String, DbType.Byte,
                            DbType.String, DbType.Int32,DbType.String,DbType.String,DbType.String  };

                        object[][] objValues1 = new object[m_dbType1.Length][];
                        for (int i = 0; i < objValues1.Length; i++)
                        {
                            objValues1[i] = new object[objResultList.Length];
                        }
                        clsLIS_Device_Test_ResultVO objResultTemp = null;
                        for (int iRow = 0; iRow < objResultList.Length; iRow++)
                        {
                            objResultTemp = objResultList[iRow];
                            objResultTemp.intIndex = intIdxArr[iRow];

                            maxIdx2 = Math.Max(maxIdx2, objResultTemp.intIndex);

                            objValues1[0][iRow] = objResultTemp.intIndex;
                            objValues1[1][iRow] = strDid;
                            objValues1[2][iRow] = strSid;

                            objResultTemp.strCheck_Date = strCheckDateTime;
                            objValues1[3][iRow] = Convert.ToDateTime(strCheckDateTime);
                            objValues1[4][iRow] = objResultTemp.strDevice_Check_Item_Name;

                            objValues1[5][iRow] = objResultTemp.strResult;
                            objValues1[6][iRow] = objResultTemp.strUnit;
                            objValues1[7][iRow] = objResultTemp.strRefRange;

                            if (objResultTemp.strMinVal != null)
                            {
                                if (Microsoft.VisualBasic.Information.IsNumeric(objResultTemp.strMinVal.Trim()))
                                {
                                    objValues1[8][iRow] = double.Parse(objResultTemp.strMinVal.Trim());
                                }
                            }
                            if (objResultTemp.strMaxVal != null)
                            {
                                if (Microsoft.VisualBasic.Information.IsNumeric(objResultTemp.strMaxVal.Trim()))
                                {
                                    objValues1[9][iRow] = double.Parse(objResultTemp.strMaxVal.Trim());
                                }
                            }

                            objValues1[10][iRow] = objResultTemp.strAbnormal_Flag;
                            objValues1[11][iRow] = objResultTemp.bytGraph;
                            objValues1[12][iRow] = objResultTemp.strGraphFormatName;
                            objValues1[13][iRow] = objResultTemp.intIsGraphResult;
                            objValues1[14][iRow] = objResultTemp.strResult2;
                            objValues1[15][iRow] = objResultTemp.strDoctorExpress;
                            objValues1[16][iRow] = objResultTemp.barCode;
                        }
                        lngRes = 0;
                        lngRes = objHRPSvc.m_lngSaveArrayWithParameters(c_strAddLabResult, objValues1, m_dbType1);

                        #endregion

                        //if (lngRes == 1)
                        //{
                        #region ���� t_opr_lis_result_log ��

                        System.Data.IDataParameter[] objDPArr1 = null;
                        if (strConditionList[1] == "000041" || strConditionList[1] == "000047")
                        {
                            strSQL = @"update t_opr_lis_result_log
                                               set end_idx_int = ?
                                             where deviceid_chr = ? 
                                               and check_dat = ?
                                               and import_req_int = ?";

                            objHRPSvc.CreateDatabaseParameter(4, out objDPArr1);
                            objDPArr1[0].Value = maxIdx2;
                            objDPArr1[1].Value = strConditionList[1]; //����ID 
                            objDPArr1[2].DbType = DbType.DateTime;
                            objDPArr1[2].Value = Convert.ToDateTime(strConditionList[3]); //��������
                            objDPArr1[3].Value = Convert.ToInt32(strConditionList[2]); //ϵͳ�ڲ��������

                            Log.Output("append: " + strConditionList[1] + " importReqId:" + strConditionList[2] + "  endIdx:" + maxIdx2.ToString());
                        }
                        else
                        {
                            strSQL = @"update t_opr_lis_result_log
                                               set end_idx_int = ?
                                             where deviceid_chr = ?
                                               and trim(device_sampleid_chr) = ?
                                               and check_dat = ?
                                               and import_req_int = ?";

                            objHRPSvc.CreateDatabaseParameter(5, out objDPArr1);
                            objDPArr1[0].Value = maxIdx2;
                            objDPArr1[1].Value = strConditionList[1]; //����ID
                            objDPArr1[2].Value = strConditionList[0]; //��������ID
                            objDPArr1[3].DbType = DbType.DateTime;
                            objDPArr1[3].Value = Convert.ToDateTime(strConditionList[3]); //��������
                            objDPArr1[4].Value = Convert.ToInt32(strConditionList[2]); //ϵͳ�ڲ��������
                        }
                        lngRes = 0;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDPArr1);
                        if (lngRecEff < 1)
                        {
                            System.EnterpriseServices.ContextUtil.SetAbort();
                        }
                        #endregion
                        //}
                    }
                    System.Data.DataTable dtbRelation = null;
                    if (lngRes == 1)//���۴˲���ɹ���񶼲�ӦӰ��ԭʼ���ݵĽ���.
                    {
                        #region  ���Һ��ձ�t_opr_lis_device_relation������Ҫ�������ļ�¼

                        strSQL = @"select deviceid_chr, seq_id_device_chr, sample_id_chr
                                      from t_opr_lis_device_relation
                                     where status_int in (1, 2) 
                                       and deviceid_chr = ?
                                       and trim(device_sampleid_chr) = ?
                                       and check_dat between ? and ?";

                        System.Data.IDataParameter[] objDPArrs3 = null;
                        objHRPSvc.CreateDatabaseParameter(4, out objDPArrs3);
                        objDPArrs3[0].Value = strDid;
                        objDPArrs3[1].Value = strSid;
                        objDPArrs3[2].DbType = DbType.DateTime;
                        objDPArrs3[2].Value = (Convert.ToDateTime(strCheckDateTime)).Date;
                        objDPArrs3[3].DbType = DbType.DateTime;
                        objDPArrs3[3].Value = (Convert.ToDateTime(strCheckDateTime)).Date.AddHours(24);

                        lngRes = 0;
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbRelation, objDPArrs3);

                        #endregion
                        if (lngRes == 1)
                        {
                            #region ���� t_opr_lis_device_relation ��

                            if (dtbRelation != null && dtbRelation.Rows.Count != 0)
                            {
                                string strSeq = dtbRelation.Rows[0]["seq_id_device_chr"].ToString().Trim();

                                strSQL = @"update t_opr_lis_device_relation
                                               set device_sampleid_chr = ?,
                                                   check_dat           = ?,
                                                   import_req_int      = ?,
                                                   status_int          = 2
                                             where trim(seq_id_device_chr) = ?
                                               and trim(deviceid_chr) = ?";

                                System.Data.IDataParameter[] objDPArrs2 = null;
                                objHRPSvc.CreateDatabaseParameter(5, out objDPArrs2);
                                objDPArrs2[0].Value = strSid;
                                objDPArrs2[1].DbType = DbType.DateTime;
                                objDPArrs2[1].Value = Convert.ToDateTime(strCheckDateTime);
                                objDPArrs2[2].Value = intImportReq;
                                objDPArrs2[3].Value = strSeq.Trim();
                                objDPArrs2[4].Value = strDid.Trim();

                                lngRecEff = 0;
                                lngRes = 0;
                                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDPArrs2);

                            }
                            #endregion
                        }
                    }
                }
                catch (System.Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                finally
                {
                    objHRPSvc.Dispose();
                }
                if (lngRes <= 0)
                {
                    ContextUtil.SetAbort();
                }
            }

            p_objOutResultArr = p_objResultArr;
            return lngRes;
        }

        #region ���Ӽ����������, ������
        /// <summary>
        /// ���Ӽ����������, ������
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_blnMuiltySample"> TRUE = ������</param>
        /// <param name="p_objOutResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngAddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, bool p_blnMuiltySample, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr)
        {
            p_objOutResultArr = null;
            long lngRes = 0;
            if (p_objResultArr == null || p_objResultArr.Length <= 0)
            {
                return lngRes;
            }

            if (p_blnMuiltySample)
            {
                List<string> lstSampleID = new List<string>();
                List<clsLIS_Device_Test_ResultVO> lstResult = new List<clsLIS_Device_Test_ResultVO>();
                List<clsLIS_Device_Test_ResultVO> lstOutResult = new List<clsLIS_Device_Test_ResultVO>();

                string strSampleID = "";
                string strSampleIDTemp = null;
                int idx = 0;
                for (idx = 0; idx < p_objResultArr.Length; idx++)
                {
                    strSampleID = p_objResultArr[idx].strDevice_Sample_ID;
                    if (strSampleID != strSampleIDTemp)
                    {
                        if (!lstSampleID.Contains(strSampleID))
                        {
                            lstSampleID.Add(strSampleID);
                        }
                        strSampleIDTemp = strSampleID;
                    }
                }

                clsLIS_Device_Test_ResultVO[] objResultTempArr = null;
                foreach (string str in lstSampleID)
                {
                    lstResult.Clear();
                    for (idx = 0; idx < p_objResultArr.Length; idx++)
                    {
                        if (str == p_objResultArr[idx].strDevice_Sample_ID)
                        {
                            lstResult.Add(p_objResultArr[idx]);

                        }
                    }
                    if (lstResult.Count > 0)
                    {
                        lngRes = lngAddLabResult(lstResult.ToArray(), out objResultTempArr);
                        if (lngRes > 0 && objResultTempArr != null && objResultTempArr.Length > 0)
                        {
                            lstOutResult.AddRange(objResultTempArr);
                        }
                    }
                }
                p_objOutResultArr = lstOutResult.ToArray();
            }
            else
            {
                lngRes = lngAddLabResult(p_objResultArr, out p_objOutResultArr);
            }

            return lngRes;
        }
        #endregion

        #region m_mthGetNewResultIndex
        [AutoComplete]
        public int m_mthGetNewResultIndex(int p_intRowNum, bool p_blnNext)
        {
            string strSQL_Update = @"update t_aid_table_sequence_id
   set max_sequence_id_chr = to_number(max_sequence_id_chr) + " + p_intRowNum + @"
 where table_name_vchr = 't_opr_lis_result'
   and col_name_vchr = 'idx_int'";

            string strSQL_Get = @"select max_sequence_id_chr
  from t_aid_table_sequence_id
 where lower(trim(table_name_vchr)) = 't_opr_lis_result'
   and lower(trim(col_name_vchr)) = 'idx_int'";

            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = null;
            long lngRes = 0;
            try
            {
                objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL_Update);
                if (lngRes == 1)
                {
                    DataTable dtbResult = null;
                    lngRes = 0;
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL_Get, ref dtbResult);
                    objHRPSvc.Dispose();
                    if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
                    {
                        string strMaxID = dtbResult.Rows[0]["max_sequence_id_chr"].ToString().Trim();
                        if (Microsoft.VisualBasic.Information.IsNumeric(strMaxID))
                        {
                            if (p_blnNext)
                            {
                                return (int.Parse(strMaxID) - p_intRowNum + 1);
                            }
                            else
                            {
                                return (int.Parse(strMaxID) - p_intRowNum);
                            }
                        }
                    }
                }
            }
            finally
            {
                objHRPSvc = null;
            }
            throw new Exception("Can not generate new MaxID.");
            //			return -1;
        }
        #endregion

        #region m_blnIsAppendResult
        [AutoComplete]
        public bool m_blnIsAppendResult(ref Dictionary<string, string> has, clsLIS_Device_Test_ResultVO[] p_objResultArr, out string[] strConditionList)
        {
            long lngRes = 1;
            string strSQL = null;
            strConditionList = null;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            #region �ж���׷�ӽ����������һ�ν�� ����Stago��CentaurCP������
            /* ԭ����ȡ�ϴ�ȡ�ø�����������������
               �������У������ͱ걾�Ų���log�����ʼֵ
               �ҵ�������н��У�Թ�ϣ�Ƚϣ������һ��������������ԭ����
               ����׷�Ӳ��������޸�req����.log��ʼֵ������ */

            bool blnFlag = true;  //true-����  false-׷��
            try
            {
                DataTable dtbTmp = new DataTable();
                clsLIS_Device_Test_ResultVO objTmp = p_objResultArr[0];
                string m_strSID = objTmp.strDevice_Sample_ID.Trim();
                string m_strDevID = objTmp.strDevice_ID;
                string m_strDateBegin = DateTime.Now.ToShortDateString() + " 00:00:00";
                string m_strDateEnd = DateTime.Now.ToShortDateString() + " 23:59:59";

                strSQL = @"select a.check_dat, a.device_sampleid_chr, a.deviceid_chr, a.import_req_int
  from t_opr_lis_result_import_req a
 where a.check_dat between ? and ?
   and a.deviceid_chr = ?
   and trim(a.device_sampleid_chr) = ?
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
   and trim(device_sampleid_chr) = ?
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
                                    strConditionList[0] = m_strSID; //��������ID
                                    strConditionList[1] = m_strDevID; //����ID
                                    strConditionList[2] = m_intImport_Req.ToString(); //ϵͳ�ڲ��������
                                    strConditionList[3] = m_strCheckDate_req; //��������
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
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return blnFlag;

            #endregion
        }
        #endregion

        #region ��ȡ������к�
        /// <summary>
        /// ��ȡ������к�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strSeqName">������</param>
        /// <param name="p_intNumber">����</param>
        /// <param name="p_lngSeqIdArr">���к�</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSequenceArr(string p_strSeqName, int p_intNumber, out int[] p_intSeqIdArr)
        {
            p_intSeqIdArr = null;
            long lngRes = 0;
            if (p_intNumber <= 0 || string.IsNullOrEmpty(p_strSeqName))
            {
                return lngRes;
            }

            try
            {
                DataTable dt = null;
                p_intSeqIdArr = new int[p_intNumber];
                string Sql = string.Format("select {0}.nextval from dual", p_strSeqName);
                clsHRPTableService svc = new clsHRPTableService();
                for (int i = p_intNumber - 1; i >= 0; i--)
                {
                    svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                    p_intSeqIdArr[i] = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
                return 1;


                //if (clsHRPTableService.bytDatabase_Selector == (byte)clsHRPTableService.enumDatabase_Selector.bytOracle)
                //{
                //    string strSQL = "select getseq(?,?) from dual";

                //    clsHRPTableService objHRPServ = new clsHRPTableService();
                //    DataTable dtValue = null;

                //    IDataParameter[] objDPArr = null;
                //    objHRPServ.CreateDatabaseParameter(2, out objDPArr);

                //    objDPArr[0].Value = p_strSeqName;
                //    objDPArr[1].Value = p_intNumber;

                //    lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtValue, objDPArr);
                //    if (lngRes > 0 && dtValue != null && dtValue.Rows.Count == 1)
                //    {
                //        p_intSeqIdArr = new int[p_intNumber];
                //        int m_intSeqId = Convert.ToInt32(dtValue.Rows[0][0]);
                //        for (int index = p_intNumber - 1; index >= 0; index--)
                //        {
                //            p_intSeqIdArr[index] = m_intSeqId--;
                //        }
                //    }
                //    else
                //    {
                //        p_intSeqIdArr = new int[1];
                //        p_intSeqIdArr[0] = 1;
                //    }
                //}
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }

        #endregion

        #region ҽ��ִ�к��޸��շ�״̬
        /// <summary>
        /// ҽ��ִ�к��޸��շ�״̬
        /// </summary>
        /// <param name="p_objStrArr">ҽ��ID</param>
        /// <param name="p_intPatientType">�������1��סԺ��2������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateStatus(string[] p_objStrArr, int p_intPatientType)
        {
            long lngRes = 0;
            clsHRPTableService objHRPSvc = null;
            string strSQL = null;
            try
            {
                strSQL = @"update t_opr_attachrelation a
   set a.status_int = 1
 where a.sourceitemid_vchr = ?
   and a.sysfrom_int = ?";
                objHRPSvc = new clsHRPTableService();
                object[][] objValue = new object[2][];
                for (int i = 0; i < objValue.Length; i++)
                {
                    objValue[i] = new object[p_objStrArr.Length];
                }
                DbType[] dbTypeArr = new DbType[] { DbType.String, DbType.Int32 };

                for (int i = 0; i < p_objStrArr.Length; i++)
                {
                    objValue[0][i] = p_objStrArr[i];
                    objValue[1][i] = p_intPatientType;
                }
                long lngEff = 0;
                lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(strSQL, objValue, ref lngEff, dbTypeArr);
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region ���ӿ�

        #region �����Ľӿ�
        /// <summary>
        /// �����Ľӿ�
        /// </summary>
        /// <param name="patVo">����</param>
        /// <param name="dtPe">������</param>
        /// <returns></returns>
        [AutoComplete]
        public bool PEItf(clsLisApplMainVO patVo, DataTable dtPe, out List<clsLisApplMainVO> lstApp)
        {
            long affectRows = 0;
            string Sql = string.Empty;
            lstApp = new List<clsLisApplMainVO>();
            clsHRPTableService svcLis = null;
            try
            {
                svcLis = new clsHRPTableService();
                IDataParameter[] parm = null;
                string groupCode = string.Empty;
                if (dtPe != null && dtPe.Rows.Count > 0)
                {
                    List<string> lstGroupItem = new List<string>();
                    foreach (DataRow dr in dtPe.Rows)
                    {
                        groupCode = dr["as_group"].ToString();
                        if (!string.IsNullOrEmpty(groupCode))
                        {
                            if (lstGroupItem.IndexOf(groupCode) < 0)
                                lstGroupItem.Add(groupCode);
                        }
                    }
                    string groupCodeArr = string.Empty;
                    if (lstGroupItem.Count > 0)
                    {
                        foreach (string item in lstGroupItem)
                        {
                            groupCodeArr += "'" + item + "',";
                        }
                        groupCodeArr = groupCodeArr.TrimEnd(',');

                        #region t_bse_lis_check_item
                        Sql = @"select t1.rptno_chr,
                                       t1.pycode_chr,
                                       t1.unit_chr,
                                       t1.check_item_name_vchr,
                                       t1.is_sex_related_chr,
                                       t1.check_item_english_name_vchr,
                                       t1.is_age_related_chr,
                                       t1.is_sample_related_chr,
                                       t1.formula_vchr,
                                       t1.test_methods_vchr,
                                       t1.clinic_meaning_vchr,
                                       t1.check_item_id_chr,
                                       t1.shortname_chr,
                                       t1.is_qc_required_chr,
                                       t1.resulttype_chr,
                                       t1.ref_value_range_vchr,
                                       t1.wbcode_chr,
                                       t1.assist_code01_chr,
                                       t1.assist_code02_chr,
                                       t1.is_no_food_required_chr,
                                       t1.is_physical_exam_required_chr,
                                       t1.is_reservation_required_chr,
                                       t1.sample_valid_time_dec,
                                       t1.sample_valid_time_unit_chr,
                                       t1.modify_dat,
                                       t1.operatorid_chr,
                                       t1.check_category_id_chr,
                                       t1.ref_max_val_vchr,
                                       t1.ref_min_val_vchr,
                                       t1.sampletype_vchr,
                                       t1.is_menses_related_chr,
                                       t1.is_calculated_chr,
                                       t1.formula_user_vchr,
                                       t1.alarm_low_val_vchr,
                                       t1.alarm_up_val_vchr,
                                       t1.alert_value_range_vchr,
                                       t1.itemprice_mny,
                                       t2.apply_unit_id_chr,
                                       t2.print_seq_int,
                                       t3.apply_unit_name_vchr
                                  from t_bse_lis_check_item t1
                                 inner join t_aid_lis_apply_unit_detail t2
                                    on t2.check_item_id_chr = t1.check_item_id_chr
                                 inner join t_aid_lis_apply_unit t3
                                    on t2.apply_unit_id_chr = t3.apply_unit_id_chr
                                 where t3.apply_unit_id_chr in ({0})
                                 order by t2.print_seq_int
                                ";
                        #endregion

                        DataTable dtCheckItem = null;
                        Sql = string.Format(Sql, groupCodeArr);
                        svcLis.lngGetDataTableWithoutParameters(Sql, ref dtCheckItem);

                        #region t_aid_lis_group_sample_type
                        Sql = @"select t1.sample_group_id_chr,
                                       t1.py_code_chr,
                                       t1.wb_code_chr,
                                       t1.assist_code01_chr,
                                       t1.assist_code02_chr,
                                       t1.is_hand_work_int,
                                       t1.device_model_id_chr,
                                       t1.remark_vchr,
                                       t1.check_category_id_chr,
                                       t1.sample_group_name_chr,
                                       t1.print_title_vchr,
                                       t1.print_seq_int,
                                       t2.apply_unit_id_chr,
                                       t3.sample_type_id_chr,
                                       t4.sample_type_desc_vchr 
                                  from t_aid_lis_sample_group t1
                                 inner join t_aid_lis_sample_group_unit t2
                                    on t1.sample_group_id_chr = t2.sample_group_id_chr
                                  left join t_aid_lis_group_sample_type t3
                                    on t1.sample_group_id_chr = t3.sample_group_id_chr
                                  left join t_aid_lis_sampletype t4 
                                    on t3.sample_type_id_chr = t4.sample_type_id_chr 
                                 where t2.apply_unit_id_chr in ({0}) ";
                        #endregion

                        string sampleGroupIdArr = string.Empty;
                        Dictionary<string, string> dicGroupSample = new Dictionary<string, string>();
                        DataTable dtSample = null;
                        Sql = string.Format(Sql, groupCodeArr);
                        svcLis.lngGetDataTableWithoutParameters(Sql, ref dtSample);
                        foreach (DataRow dr in dtSample.Rows)
                        {
                            sampleGroupIdArr += "'" + dr["sample_group_id_chr"].ToString() + "',";
                            if (!dicGroupSample.ContainsKey(dr["apply_unit_id_chr"].ToString()))
                            {
                                dicGroupSample.Add(dr["apply_unit_id_chr"].ToString(), dr["sample_group_id_chr"].ToString());
                            }
                        }
                        sampleGroupIdArr = sampleGroupIdArr.TrimEnd(',');

                        #region report_group_id_chr
                        Sql = @"select t1.report_group_id_chr,
                                       t1.report_group_name_vchr,
                                       t1.print_title_vchr,
                                       t1.print_category_id_chr,
                                       t2.sample_group_id_chr 
                                  from t_aid_lis_report_group t1, t_aid_lis_report_group_detail t2
                                 where t1.report_group_id_chr = t2.report_group_id_chr
                                   and t2.sample_group_id_chr in ({0}) ";
                        #endregion

                        DataTable dtReport = null;
                        Sql = string.Format(Sql, sampleGroupIdArr);
                        svcLis.lngGetDataTableWithoutParameters(Sql, ref dtReport);

                        int n = -1;
                        string applyId = string.Empty;
                        string sampleId = string.Empty;
                        string sampleGroupId = string.Empty;
                        DataRow[] drr1 = null;
                        DataRow[] drr2 = null;
                        DataRow[] drr3 = null;
                        for (int i = 0; i < lstGroupItem.Count; i++)
                        {
                            sampleGroupId = string.Empty;
                            groupCode = lstGroupItem[i];
                            svcLis.m_lngGenerateNewID("t_opr_lis_application", "APPLICATION_ID_CHR", out applyId);
                            svcLis.m_lngGenerateNewID("t_opr_lis_sample", "sample_id_chr", out sampleId);
                            // ����Ϣ
                            patVo.m_strAPPLICATION_ID = applyId;
                            patVo.m_strSampleID = sampleId;

                            if (dicGroupSample.ContainsKey(groupCode))
                            {
                                sampleGroupId = dicGroupSample[groupCode];
                            }
                            else
                            {
                                continue;
                            }

                            drr1 = dtCheckItem.Select("apply_unit_id_chr = '" + groupCode + "'");
                            drr2 = dtSample.Select("apply_unit_id_chr = '" + groupCode + "'");
                            drr3 = dtReport.Select("sample_group_id_chr = '" + sampleGroupId + "'");
                            if (drr3 == null || drr3.Length == 0) continue; ;

                            // 1. ���������
                            #region t_opr_lis_application
                            Sql = @"insert into t_opr_lis_application
                                          (application_id_chr,
                                           modify_dat,
                                           patientid_chr,
                                           application_dat,
                                           sex_chr, 
                                           patient_name_vchr,
                                           patient_subno_chr,
                                           age_chr,
                                           patient_type_id_chr,
                                           diagnose_vchr, 
                                           bedno_chr,
                                           icdcode_chr,
                                           patientcardid_chr,
                                           application_form_no_chr, 
                                           operator_id_chr,
                                           appl_empid_chr,
                                           appl_deptid_chr,
                                           summary_vchr,
                                           pstatus_int,
                                           emergency_int,           
                                           special_int,
                                           form_int,
                                           patient_inhospitalno_chr,
                                           sample_type_id_chr,      
                                           sample_type_vchr,
                                           check_content_vchr,
                                           oringin_dat,
                                           charge_info_vchr,
                                           orderunitrelation_vchr,
                                           printed_num,
                                           printed_date)
                                        values
                                          (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,  
                                           ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 
                                           ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
                                        ";
                            n = -1;
                            svcLis.CreateDatabaseParameter(31, out parm);
                            parm[++n].Value = patVo.m_strAPPLICATION_ID;
                            parm[++n].Value = DateTime.Now;
                            parm[++n].Value = patVo.m_strPatientID;
                            parm[++n].Value = DateTime.Now;
                            parm[++n].Value = patVo.m_strSex;
                            parm[++n].Value = patVo.m_strPatient_Name;
                            parm[++n].Value = null;
                            parm[++n].Value = patVo.m_strAge;
                            parm[++n].Value = patVo.m_strPatientType;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = patVo.m_strOperator_ID;
                            parm[++n].Value = patVo.m_strAppl_EmpID;
                            parm[++n].Value = patVo.m_strAppl_DeptID;
                            parm[++n].Value = null;
                            parm[++n].Value = patVo.m_intPStatus_int;
                            parm[++n].Value = patVo.m_intEmergency;        // 20 
                            parm[++n].Value = patVo.m_intSpecial;
                            parm[++n].Value = 0;
                            parm[++n].Value = patVo.m_strPatient_inhospitalno_chr;
                            if (drr1 != null && drr1.Length > 0 && drr2 != null && drr2.Length > 0)
                            {
                                parm[++n].Value = drr2[0]["sample_type_id_chr"].ToString();
                                parm[++n].Value = drr2[0]["sample_type_desc_vchr"].ToString();
                                parm[++n].Value = drr1[0]["apply_unit_name_vchr"].ToString();
                                sampleGroupId = drr2[0]["sample_group_id_chr"].ToString();
                                patVo.m_strSampleTypeID = drr2[0]["sample_type_id_chr"].ToString();
                                patVo.m_strSampleType = drr2[0]["sample_type_desc_vchr"].ToString();
                                patVo.m_strCheckContent = drr1[0]["apply_unit_name_vchr"].ToString();
                            }
                            else
                            {
                                return false;
                            }
                            parm[++n].Value = DateTime.Now;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = 0;
                            parm[++n].Value = null;
                            svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);

                            #endregion

                            // 2. ���뵥�ı�����
                            #region t_opr_lis_app_report
                            Sql = @"insert into t_opr_lis_app_report
                                      (application_id_chr,
                                       report_group_id_chr,
                                       modify_dat,
                                       summary_vchr,
                                       operator_id_chr,
                                       status_int,
                                       report_dat,
                                       reportor_id_chr,
                                       confirm_dat,
                                       confirmer_id_chr,
                                       xml_summary_vchr,
                                       annotation_vchr,
                                       xml_annotation_vchr)
                                    values
                                      (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
                                       ?, ?, ?) ";
                            n = -1;
                            svcLis.CreateDatabaseParameter(13, out parm);
                            parm[++n].Value = patVo.m_strAPPLICATION_ID;
                            parm[++n].Value = drr3[0]["report_group_id_chr"].ToString();
                            parm[++n].Value = DateTime.Now;
                            parm[++n].Value = null;
                            parm[++n].Value = patVo.m_strOperator_ID;
                            parm[++n].Value = 1;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                            // ����״̬
                            patVo.m_intReportStatus = 1;
                            #endregion

                            // 3. ���뵥Ҫ���ı걾��
                            #region t_opr_lis_app_sample
                            Sql = @"insert into t_opr_lis_app_sample
                                          (application_id_chr,
                                           sample_group_id_chr,
                                           report_group_id_chr,
                                           sample_id_chr)
                                        values
                                          (?, ?, ?, ?)";

                            n = -1;
                            svcLis.CreateDatabaseParameter(4, out parm);
                            parm[++n].Value = patVo.m_strAPPLICATION_ID;
                            parm[++n].Value = drr2[0]["sample_group_id_chr"].ToString();
                            parm[++n].Value = drr3[0]["report_group_id_chr"].ToString();
                            parm[++n].Value = patVo.m_strSampleID;
                            svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);

                            #endregion

                            // 4. ���뵥Ҫ���ı걾���Ӧ�ļ�����Ŀ
                            #region t_opr_lis_app_check_item
                            foreach (DataRow dr1 in drr1)
                            {
                                Sql = @"insert into t_opr_lis_app_check_item
                                              (check_item_id_chr,
                                               sample_group_id_chr,
                                               report_group_id_chr,
                                               application_id_chr,
                                               itemprice_mny)
                                            values
                                              (?, ?, ?, ?, ?)";
                                n = -1;
                                svcLis.CreateDatabaseParameter(5, out parm);
                                parm[++n].Value = dr1["check_item_id_chr"].ToString();
                                parm[++n].Value = drr2[0]["sample_group_id_chr"].ToString();
                                parm[++n].Value = drr3[0]["report_group_id_chr"].ToString();
                                parm[++n].Value = patVo.m_strAPPLICATION_ID;
                                parm[++n].Value = dr1["itemprice_mny"].ToString();
                                svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                            }
                            #endregion

                            // 5. ���뵥��Ӧ���뵥Ԫ
                            #region t_opr_lis_app_apply_unit
                            Sql = @"insert into t_opr_lis_app_apply_unit
                                          (application_id_chr, user_group_string, apply_unit_id_chr)
                                        values
                                          (?, ?, ?)";
                            n = -1;
                            svcLis.CreateDatabaseParameter(3, out parm);
                            parm[++n].Value = patVo.m_strAPPLICATION_ID;
                            parm[++n].Value = ">>" + groupCode;
                            parm[++n].Value = groupCode;
                            svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                            #endregion

                            // 6. ���뵥Ԫ��Ӧ����Ŀ
                            #region t_opr_lis_app_unit_item
                            foreach (DataRow dr1 in drr1)
                            {
                                Sql = @"insert into t_opr_lis_app_unit_item
                                          (application_id_chr, check_item_id_chr, apply_unit_id_chr)
                                        values
                                          (?, ?, ?)";

                                n = -1;
                                svcLis.CreateDatabaseParameter(3, out parm);
                                parm[++n].Value = patVo.m_strAPPLICATION_ID;
                                parm[++n].Value = dr1["check_item_id_chr"].ToString();
                                parm[++n].Value = groupCode;
                                svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                            }
                            #endregion

                            // 7. �걾��Ϣ
                            #region t_opr_lis_sample
                            Sql = @"insert into t_opr_lis_sample
                                          (appl_dat,
                                           sex_chr,
                                           patient_name_vchr,
                                           patient_subno_chr,
                                           age_chr,
                                           patient_type_chr,
                                           diagnose_vchr,
                                           sampletype_vchr,
                                           samplestate_vchr,
                                           bedno_chr,
                                           icd_vchr,
                                           patientcardid_chr,
                                           barcode_vchr,
                                           sample_id_chr,
                                           patientid_chr,
                                           sampling_date_dat,
                                           operator_id_chr,
                                           modify_dat,
                                           appl_empid_chr,
                                           appl_deptid_chr,
                                           status_int,
                                           sample_type_id_chr,
                                           qcsampleid_chr,
                                           samplekind_chr,
                                           check_date_dat,
                                           accept_dat,
                                           acceptor_id_chr,
                                           application_id_chr,
                                           patient_inhospitalno_chr,
                                           confirm_dat,
                                           confirmer_id_chr,
                                           collector_id_chr,
                                           checker_id_chr,
                                           sendsample_empid_chr)
                                        values
                                          (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 
                                           ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 
                                           ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 
                                           ?, ?, ?, ? )
                                        ";
                            n = -1;
                            svcLis.CreateDatabaseParameter(34, out parm);
                            parm[++n].Value = DateTime.Now;
                            parm[++n].Value = patVo.m_strSex;
                            parm[++n].Value = patVo.m_strPatient_Name;
                            parm[++n].Value = null;
                            parm[++n].Value = patVo.m_strAge;
                            parm[++n].Value = patVo.m_strPatientType;
                            parm[++n].Value = null;
                            parm[++n].Value = drr2[0]["sample_type_desc_vchr"].ToString();
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = patVo.m_strSampleID;
                            parm[++n].Value = patVo.m_strPatientID;
                            parm[++n].Value = DateTime.Now;
                            parm[++n].Value = patVo.m_strOperator_ID;
                            parm[++n].Value = DateTime.Now;
                            parm[++n].Value = patVo.m_strAppl_EmpID;
                            parm[++n].Value = patVo.m_strAppl_DeptID;       // 20
                            parm[++n].Value = 3;
                            parm[++n].Value = drr2[0]["sample_type_id_chr"].ToString();
                            parm[++n].Value = -1;
                            parm[++n].Value = 1;
                            parm[++n].Value = null;
                            parm[++n].Value = DateTime.Now;
                            parm[++n].Value = patVo.m_strOperator_ID;
                            parm[++n].Value = patVo.m_strAPPLICATION_ID;
                            parm[++n].Value = null;     // סԺ�� ?Ҫ��Ҫ������
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            parm[++n].Value = null;
                            svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                            #endregion

                            #region clone
                            clsLisApplMainVO tmpVo = new clsLisApplMainVO();
                            tmpVo.m_blnHasBarcode = patVo.m_blnHasBarcode;
                            tmpVo.m_intChargeState = patVo.m_intChargeState;
                            tmpVo.m_intEmergency = patVo.m_intEmergency;
                            tmpVo.m_intForm_int = patVo.m_intForm_int;
                            tmpVo.m_intIsGreen = patVo.m_intIsGreen;
                            tmpVo.m_intPStatus_int = patVo.m_intPStatus_int;
                            tmpVo.m_intReportPrint = patVo.m_intReportPrint;
                            tmpVo.m_intReportStatus = patVo.m_intReportStatus;
                            tmpVo.m_intSampleStatus = patVo.m_intSampleStatus;
                            tmpVo.m_intSpecial = patVo.m_intSpecial;
                            tmpVo.m_isPrinted = patVo.m_isPrinted;
                            tmpVo.m_strAcceptDate = patVo.m_strAcceptDate;
                            tmpVo.m_strAge = patVo.m_strAge;
                            tmpVo.m_strAppl_Dat = patVo.m_strAppl_Dat;
                            tmpVo.m_strAppl_DeptID = patVo.m_strAppl_DeptID;
                            tmpVo.m_strAppl_EmpID = patVo.m_strAppl_EmpID;
                            tmpVo.m_strApplication_Form_NO = patVo.m_strApplication_Form_NO;
                            tmpVo.m_strAPPLICATION_ID = patVo.m_strAPPLICATION_ID;
                            tmpVo.m_strBarcode = patVo.m_strBarcode;
                            tmpVo.m_strBedNO = patVo.m_strBedNO;
                            tmpVo.m_strBirthDay = patVo.m_strBirthDay;
                            tmpVo.m_strChargeInfo = patVo.m_strChargeInfo;
                            tmpVo.m_strCheckContent = patVo.m_strCheckContent;
                            tmpVo.m_strDiagnose = patVo.m_strDiagnose;
                            tmpVo.m_strICD = patVo.m_strICD;
                            tmpVo.m_strMODIFY_DAT = patVo.m_strMODIFY_DAT;
                            tmpVo.m_strOperator_ID = patVo.m_strOperator_ID;
                            tmpVo.m_strOrderArr = patVo.m_strOrderArr;
                            tmpVo.m_strOrderunitrelation = patVo.m_strOrderunitrelation;
                            tmpVo.m_strOriginDate = patVo.m_strOriginDate;
                            tmpVo.m_strPatient_inhospitalno_chr = patVo.m_strPatient_inhospitalno_chr;
                            tmpVo.m_strPatient_Name = patVo.m_strPatient_Name;
                            tmpVo.m_strPatient_SubNO = patVo.m_strPatient_SubNO;
                            tmpVo.m_strPatientcardID = patVo.m_strPatientcardID;
                            tmpVo.m_strPatientID = patVo.m_strPatientID;
                            tmpVo.m_strPatientType = patVo.m_strPatientType;
                            tmpVo.m_strPrintDate = patVo.m_strPrintDate;
                            tmpVo.m_strReportDate = patVo.m_strReportDate;
                            tmpVo.m_strReportGroupID = patVo.m_strReportGroupID;
                            tmpVo.m_strSample_Back_Reason = patVo.m_strSample_Back_Reason;
                            tmpVo.m_strSampleID = patVo.m_strSampleID;
                            tmpVo.m_strSampleStatus = patVo.m_strSampleStatus;
                            tmpVo.m_strSampleType = patVo.m_strSampleType;
                            tmpVo.m_strSampleTypeID = patVo.m_strSampleTypeID;
                            tmpVo.m_strSamplingDate = patVo.m_strSamplingDate;
                            tmpVo.m_strSex = patVo.m_strSex;
                            tmpVo.m_strSummary = patVo.m_strSummary;
                            lstApp.Add(tmpVo);
                            #endregion
                        }
                        return true;
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
                svcLis.Dispose();
                svcLis = null;
            }
            return false;
        }
        #endregion

        #region ���-��ȡ���������Ϣ
        /// <summary>
        /// ���-��ȡ���������Ϣ
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        //        [AutoComplete]
        //        public DataTable GetPeSample(string barCode)
        //        {
        //            string Sql = string.Empty;
        //            DataTable dt = null;
        //            clsHRPTableService svcPe = null;
        //            try
        //            {
        //                #region Sql
        //                Sql = @"select a.reg_no,
        //                               b.pat_code,
        //                               b.pat_name,
        //                               b.sex,
        //                               b.age,
        //                               c.comb_code,
        //                               c.samp_no,
        //                               d.as_group,
        //                               d.as_group_name
        //                          from tj_register a
        //                         inner join tj_patient b
        //                            on a.pat_code = b.pat_code
        //                         inner join as_samp_cen c
        //                            on a.reg_no = c.reg_no
        //                          left join def_comb_tj_as d
        //                            on c.comb_code = d.comb_code
        //                         where c.samp_no = ?";
        //                #endregion

        //                svcPe = new clsHRPTableService();
        //                svcPe.m_mthSetDataBase_Selector(1, 15);
        //                IDataParameter[] parm = null;
        //                svcPe.CreateDatabaseParameter(1, out parm);
        //                parm[0].Value = barCode;
        //                svcPe.lngGetDataTableWithParameters(Sql, ref dt, parm);
        //                //dt = this.GetDataTable(svcPe.GetPeConnStr, Sql, parm);
        //                //dt = this.GetDataTable(Sql.Replace("?", "'" + barCode + "'"));
        //                if (dt != null) dt.TableName = "pe";
        //            }
        //            catch (Exception objEx)
        //            {
        //                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
        //                objLogger.LogError(objEx);
        //            }
        //            finally
        //            {
        //                svcPe.Dispose();
        //                svcPe = null;
        //            }
        //            return dt;
        //        }
        #endregion

        #region ���-У���Ƿ��Ѵ��
        /// <summary>
        /// ���-У���Ƿ��Ѵ��
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool SamplePackIsExist(string barCode)
        {
            string Sql = string.Empty;
            bool isExist = false;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                Sql = @"select t.packid,
                              t.typeid,
                              t.barcode
                         from t_samplepack t
                        where t.barcode = ?";

                DataTable dtMain = null;
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = barCode;
                svc.lngGetDataTableWithParameters(Sql, ref dtMain, parm);
                if (dtMain != null && dtMain.Rows.Count > 0)
                {
                    isExist = true;
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return isExist;
        }
        #endregion

        #region ���-����
        /// <summary>
        /// ���-����
        /// </summary>
        /// <param name="lstSamplePack"></param>
        /// <param name="lstSamplePackDet"></param>
        /// <param name="packId"></param>
        /// <returns></returns>
        [AutoComplete]
        public int SamplePackInsert(List<EntitySamplePack> lstSamplePack, List<EntitySamplePackDetail> lstSamplePackDet, int bizType, out decimal packId)
        {
            long lngRes = -1;
            string Sql = string.Empty;
            packId = 0;
            try
            {
                List<EntitySamplePack> lstSamplePackNew = new List<EntitySamplePack>();
                List<EntitySamplePack> lstSamplePackUpdate = new List<EntitySamplePack>();
                // 0 ��ʱ����� 1 ��ʽ���
                int packType = lstSamplePack[0].status;
                foreach (EntitySamplePack item in lstSamplePack)
                {
                    if (item.packId > 0)
                    {
                        packId = item.packId;
                        if (packType == 0)
                            lstSamplePackNew.Add(item);
                        else
                            lstSamplePackUpdate.Add(item);
                    }
                    else
                    {
                        lstSamplePackNew.Add(item);
                    }
                    if (item.packId2 > 0 && packId == 0) packId = item.packId2;
                }
                DataTable dt = null;
                clsHRPTableService svc = new clsHRPTableService();
                if (packId == 0)
                {
                    Sql = @"select seq_samplepack_packid.nextval from dual";
                    svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                    packId = Convert.ToDecimal(dt.Rows[0][0].ToString());
                }
                else
                {
                    if (lstSamplePackNew.Count > 0)
                    {
                        foreach (EntitySamplePack item in lstSamplePack)
                        {
                            item.packId = packId;
                        }
                    }
                }
                DateTime dtmNow = DateTime.Now;
                DbType[] dbTypes = null;
                object[][] objValues = null;
                int n = 0;
                int j = 0;

                #region New
                if (lstSamplePackNew.Count > 0)
                {
                    Sql = @"insert into t_samplepack
                              (packid,
                               typeid,
                               barcode,
                               peno,
                               patname,
                               sex,
                               age,
                               checkerid,
                               checkdate,
                               recorderid,
                               recorddate,
                               packoperid,
                               packdate,
                               status,
                               floorno,
                               peSamplingDate)
                            values
                              (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    dbTypes = new DbType[] { DbType.Decimal, DbType.Decimal, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.DateTime, DbType.String,
                                             DbType.DateTime, DbType.String, DbType.DateTime, DbType.Decimal, DbType.Decimal, DbType.DateTime };
                    objValues = new object[dbTypes.Length][];

                    for (int i = 0; i < objValues.Length; i++)
                    {
                        objValues[i] = new object[lstSamplePackNew.Count];
                    }

                    n = 0;
                    j = 0;
                    foreach (EntitySamplePack item in lstSamplePackNew)
                    {
                        n = 0;
                        objValues[n++][j] = packId;
                        objValues[n++][j] = item.typeId;
                        objValues[n++][j] = item.barCode;
                        objValues[n++][j] = item.peNo;
                        objValues[n++][j] = item.patName;
                        objValues[n++][j] = item.sex;
                        objValues[n++][j] = item.age;
                        objValues[n++][j] = item.checkerId;
                        objValues[n++][j] = item.checkDate;
                        objValues[n++][j] = item.recorderId;
                        objValues[n++][j] = item.recordDate;
                        if (packType == 1)
                        {
                            objValues[n++][j] = item.packOperId;
                            objValues[n++][j] = dtmNow;
                        }
                        else
                        {
                            objValues[n++][j] = null;
                            objValues[n++][j] = null;
                        }
                        objValues[n++][j] = packType;
                        objValues[n++][j] = item.floorNo;
                        if (packType == 1)
                        {
                            objValues[n++][j] = null;
                        }
                        else
                        {
                            objValues[n++][j] = dtmNow;
                        }
                        j++;
                    }
                    lngRes = svc.m_lngSaveArrayWithParameters(Sql, objValues, dbTypes);

                    Sql = @"insert into t_samplepack_detail
                              (packdetid, barcode, itemcode, itemname)
                            values
                              (seq_samplepackdet_packdetid.nextval, ?, ?, ?)";
                    dbTypes = new DbType[] { DbType.String, DbType.String, DbType.String };
                    objValues = new object[dbTypes.Length][];

                    for (int i = 0; i < objValues.Length; i++)
                    {
                        objValues[i] = new object[lstSamplePackDet.Count];
                    }
                    j = 0;
                    foreach (EntitySamplePackDetail item in lstSamplePackDet)
                    {
                        n = 0;
                        objValues[n++][j] = item.barCode;
                        objValues[n++][j] = item.itemCode;
                        objValues[n++][j] = item.itemName;
                        j++;
                    }
                    lngRes = svc.m_lngSaveArrayWithParameters(Sql, objValues, dbTypes);
                }
                #endregion

                #region update

                if (lstSamplePackUpdate.Count > 0 && packType == 1)
                {
                    Sql = @"update t_samplepack set packoperid = ?, packdate = ?, status = ? where barcode = ?";
                    dbTypes = new DbType[] { DbType.String, DbType.DateTime, DbType.Decimal, DbType.String };
                    objValues = new object[dbTypes.Length][];
                    for (int i = 0; i < objValues.Length; i++)
                    {
                        objValues[i] = new object[lstSamplePackUpdate.Count];
                    }
                    j = 0;
                    foreach (EntitySamplePack item in lstSamplePackUpdate)
                    {
                        n = 0;
                        objValues[n++][j] = item.packOperId;
                        objValues[n++][j] = dtmNow;
                        objValues[n++][j] = 1;
                        objValues[n++][j] = item.barCode;
                        j++;
                    }
                    lngRes = svc.m_lngSaveArrayWithParameters(Sql, objValues, dbTypes);
                }
                #endregion

                // סԺ.���²���ʱ��
                if (bizType == 1 && lstSamplePackNew.Count > 0)
                {
                    Sql = @"update t_opr_lis_sample set sampling_date_dat = ? where barcode_vchr = ?";
                    dbTypes = new DbType[] { DbType.DateTime, DbType.String };
                    objValues = new object[dbTypes.Length][];
                    for (int i = 0; i < objValues.Length; i++)
                    {
                        objValues[i] = new object[lstSamplePackNew.Count];
                    }
                    j = 0;
                    foreach (EntitySamplePack item in lstSamplePackNew)
                    {
                        n = 0;
                        objValues[n++][j] = item.recordDate; //dtmNow; // DateTime.Now;
                        objValues[n++][j] = item.barCode;
                        j++;
                    }
                    lngRes = svc.m_lngSaveArrayWithParameters(Sql, objValues, dbTypes);
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return (int)lngRes;
        }
        #endregion

        #region ���-ɾ��
        /// <summary>
        /// ���-ɾ��
        /// </summary>
        /// <param name="lstBarCode"></param>
        /// <returns></returns>
        [AutoComplete]
        public int SamplePackDel(List<string> lstBarCode)
        {
            long lngRes = -1;
            string Sql = string.Empty;
            try
            {
                string sub = string.Empty;
                foreach (string code in lstBarCode)
                {
                    sub += "'" + code + "',";
                }
                sub = sub.TrimEnd(',');

                clsHRPTableService svc = new clsHRPTableService();
                Sql = @"delete from t_samplepack where barcode in ({0})";
                Sql = string.Format(Sql, sub);
                lngRes = svc.DoExcute(Sql);

                Sql = @"delete from t_samplepack_detail where barcode in ({0})";
                Sql = string.Format(Sql, sub);
                lngRes = svc.DoExcute(Sql);

            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return (int)lngRes;
        }
        #endregion

        #region ���-��ѯ��ʱ��
        /// <summary>
        /// ���-��ѯ��ʱ��
        /// </summary>
        /// <param name="floorNo"></param>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool SamplePackQueryTemp(decimal floorNo, out string barCode)
        {
            barCode = string.Empty;
            bool isHave = false;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                Sql = @"select t.packid,
                               t.typeid,
                               t.barcode,
                               t.peno,
                               t.patname,
                               t.sex,
                               t.age,
                               t.checkerid,
                               t.checkdate,
                               t.recorderid,
                               t.recorddate,
                               t.packoperid,
                               t.packdate,
                               t.status
                          from t_samplepack t
                         where (t.recorddate between ? and ?)
                           and t.floorno = ?
                           and t.status = 0";

                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(3, out parm);
                parm[0].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                parm[1].Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                parm[2].Value = floorNo;
                DataTable dt = null;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    isHave = true;
                    barCode = dt.Rows[0]["barcode"].ToString();
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return isHave;
        }
        #endregion

        #region ���-��ѯ
        /// <summary>
        /// ���-��ѯ
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="bizType">1 �������2 סԺ�����3 �����գ�4 סԺ���� </param>
        /// <returns></returns>
        [AutoComplete]
        public List<EntitySamplePack> SamplePackQuery(string barCode, int bizType)
        {
            string Sql = string.Empty;
            List<EntitySamplePack> data = new List<EntitySamplePack>();
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                if (bizType == 2 || bizType == 4)
                {
                    Sql = @"select distinct t.packid,
                                            t.typeid,
                                            t.barcode,
                                            t.peno,
                                            t.patname,
                                            t.sex,
                                            t.age,
                                            t.checkerid,
                                            t.checkdate,
                                            t.recorderid,
                                            t.recorddate,
                                            a.lastname_vchr     as checkername,
                                            b.lastname_vchr     as recordername,
                                            app.patientid_chr   as patientId,
                                            app.appl_empid_chr  as applyEmpId,
                                            app.appl_deptid_chr as applyDeptId,
                                            t.recorddate        as applyDate,
                                            t.packoperid,
                                            b1.lastname_vchr    as packOperName,
                                            t.packdate,
                                            t.status
                              from t_samplepack t
                             inner join t_opr_lis_sample app
                                on t.barcode = app.barcode_vchr
                              left join t_bse_employee a
                                on t.checkerid = a.empid_chr
                              left join t_bse_employee b
                                on t.recorderid = b.empid_chr
                              left join t_bse_employee b1
                                on t.packoperid = b1.empid_chr
                             where app.appl_deptid_chr is not null
                               and t.packid = (select packid from t_samplepack where barcode = ?) 
                              ";
                    if (bizType == 4) Sql += " and t.status = 1 ";
                }
                else
                {
                    Sql = @"select distinct t.packid,
                                            t.typeid,
                                            t.barcode,
                                            t.peno,
                                            t.patname,
                                            t.sex,
                                            t.age,
                                            t.checkerid,
                                            t.checkdate,
                                            t.recorderid,
                                            t.recorddate,
                                            a.lastname_vchr     as checkername,
                                            b.lastname_vchr     as recordername,
                                            app.patientid_chr   as patientId,
                                            app.appl_empid_chr  as applyEmpId,
                                            app.appl_deptid_chr as applyDeptId,
                                            t.recorddate        as applyDate,
                                            t.packoperid,
                                            b1.lastname_vchr    as packOperName,
                                            t.packdate,
                                            t.status
                              from t_samplepack t
                             inner join t_opr_lis_application app
                                on t.peno = app.patient_inhospitalno_chr
                              left join t_bse_employee a
                                on t.checkerid = a.empid_chr
                              left join t_bse_employee b
                                on t.recorderid = b.empid_chr
                              left join t_bse_employee b1
                                on t.packoperid = b1.empid_chr
                             where t.packid = (select packid from t_samplepack where barcode = ?) 
                                ";

                    if (bizType == 3) Sql += " and t.status = 1 ";
                }

                DataTable dtMain = null;
                DataTable dtDet = null;
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = barCode;
                svc.lngGetDataTableWithParameters(Sql, ref dtMain, parm);
                if (dtMain == null || dtMain.Rows.Count == 0) return data;

                Sql = @" select t.barcode, t.itemcode, t.itemname
                          from t_samplepack_detail t
                         where t.barcode in
                               (select a.barcode
                                  from t_samplepack a
                                 where a.packid =
                                       (select packid from t_samplepack where barcode = ?))";
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = barCode;
                svc.lngGetDataTableWithParameters(Sql, ref dtDet, parm);
                if (dtMain != null && dtMain.Rows.Count > 0 && dtDet != null && dtDet.Rows.Count > 0)
                {
                    int n = 0;
                    EntitySamplePack vo = null;
                    DataRow[] drr = null;
                    List<string> lstBarcode = new List<string>();
                    foreach (DataRow dr1 in dtMain.Rows)
                    {
                        vo = new EntitySamplePack();
                        vo.packId = Convert.ToDecimal(dr1["packid"].ToString());
                        vo.barCode = dr1["barcode"].ToString();

                        if (lstBarcode.IndexOf(vo.barCode) < 0)
                            lstBarcode.Add(vo.barCode);
                        else
                            continue;       // ���������������ظ��ͬһ���������˿��ܻ���ڿ�/��2����¼

                        vo.peNo = dr1["peno"].ToString();
                        vo.patName = dr1["patname"].ToString();
                        vo.sex = dr1["sex"].ToString();
                        vo.age = dr1["age"].ToString();
                        vo.checkerId = dr1["checkerid"].ToString();
                        vo.checkerName = dr1["checkername"].ToString();
                        if (dr1["checkdate"] != DBNull.Value) vo.checkDate = Convert.ToDateTime(dr1["checkdate"]);
                        vo.recorderId = dr1["recorderid"].ToString();
                        vo.recorderName = dr1["recordername"].ToString();
                        vo.recordDate = Convert.ToDateTime(dr1["recorddate"].ToString());
                        vo.patientId = dr1["patientId"].ToString();
                        vo.applyDeptId = (dr1["applyDeptId"] == DBNull.Value ? "0000195" : dr1["applyDeptId"].ToString());  // ����(����)
                        vo.applyEmpId = dr1["applyEmpId"] == DBNull.Value ? "1000958" : dr1["applyEmpId"].ToString();       // ����(��)
                        vo.applyDate = Convert.ToDateTime(dr1["applyDate"].ToString());
                        vo.packOperId = dr1["packoperid"].ToString();
                        if (dr1["packdate"] != DBNull.Value) vo.packDate = Convert.ToDateTime(dr1["packdate"]);
                        vo.packOperName = dr1["packOperName"].ToString();
                        vo.status = dr1["status"] == DBNull.Value ? 1 : Convert.ToInt32(dr1["status"]);
                        vo.sortNo = ++n;

                        drr = dtDet.Select("barcode = '" + vo.barCode + "'");
                        if (drr != null && drr.Length > 0)
                        {
                            foreach (DataRow dr2 in drr)
                            {
                                vo.itemCode += dr2["itemcode"].ToString() + ",";
                                vo.itemName += dr2["itemname"].ToString() + ",";
                            }
                            vo.itemCode = vo.itemCode.TrimEnd(',');
                            vo.itemName = vo.itemName.TrimEnd(',');
                        }
                        data.Add(vo);
                    }
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return data;
        }
        #endregion

        #region SamplePackCheck
        /// <summary>
        /// ���-����
        /// </summary>
        /// <param name="sampleVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool SamplePackCheck(EntitySamplePack sampleVo)
        {
            long affectRows = 0;
            string Sql = string.Empty;
            clsHRPTableService svcLis = null;
            DataRow drPat = null;
            try
            {             
                DateTime? peSamplingDate = null;
                sampleVo.checkDate = DateTime.Now;

                svcLis = new clsHRPTableService();
                IDataParameter[] parm = null;
                int n = -1;

                #region סԺ��������

                if (sampleVo.typeId == 1)
                {
                    #region ��ȡ���߻�������
                    Sql = @"select nvl(a.diagnose_vchr, '') as diagnose,
                                   nvl(a.bedno_chr, '') as bedNo,
                                   nvl(a.patientcardid_chr, '') as cardNo,
                                   nvl(a.patient_inhospitalno_chr, '') as ipNo  
                              from t_opr_lis_sample a
                             where a.barcode_vchr = ?";
                    svcLis.CreateDatabaseParameter(1, out parm);
                    parm[0].Value = sampleVo.barCode;
                    DataTable dtPat = null;
                    svcLis.lngGetDataTableWithParameters(Sql, ref dtPat, parm);
                    if (dtPat != null && dtPat.Rows.Count > 0)
                    {
                        drPat = dtPat.Rows[0];
                    }
                    #endregion

                    // 1. ���ʶ
                    #region t_opr_lis_sample
                    Sql = @"update t_opr_lis_sample
                               set status_int           = ?,
                                   accept_dat           = ?,
                                   acceptor_id_chr      = ?,
                                   sendsample_empid_chr = ?,
                                   issampleback         = ?,
                                   sample_back_reason   = ?
                             where barcode_vchr = ?";
                    n = -1;
                    svcLis.CreateDatabaseParameter(7, out parm);
                    parm[++n].Value = 3;
                    parm[++n].Value = sampleVo.checkDate.Value;
                    parm[++n].Value = sampleVo.checkerId;
                    parm[++n].Value = sampleVo.applyEmpId;
                    parm[++n].Value = 0;
                    parm[++n].Value = "";
                    parm[++n].Value = sampleVo.barCode;
                    svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                    #endregion

                    // 2. �����Ϣ
                    #region t_samplepack
                    Sql = @"update t_samplepack set checkerid = ?, checkdate = ? where barcode = ?";
                    n = -1;
                    svcLis.CreateDatabaseParameter(3, out parm);
                    parm[++n].Value = sampleVo.checkerId;
                    parm[++n].Value = sampleVo.checkDate.Value;
                    parm[++n].Value = sampleVo.barCode;
                    svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                    #endregion

                    // ����2��Ŀ 1) 0000041 - AU680
                    SaveBarcode2Item(sampleVo.barCode);

                    //// һ��Ҫֱ�ӷ��أ�����ִ���������
                    //return true;
                }
                else if (sampleVo.typeId == 1)      // ������ʱ��
                {
                    DataTable dtSamp = null;
                    Sql = @"select peSamplingDate from t_samplepack where barcode = ?";
                    n = -1;
                    svcLis.CreateDatabaseParameter(1, out parm);
                    parm[++n].Value = sampleVo.barCode;
                    svcLis.lngGetDataTableWithParameters(Sql, ref dtSamp, parm);
                    if (dtSamp != null && dtSamp.Rows.Count > 0)
                    {
                        if (dtSamp.Rows[0]["peSamplingDate"] != DBNull.Value)
                            peSamplingDate = Convert.ToDateTime(dtSamp.Rows[0]["peSamplingDate"]);
                    }

                    #region ��ֹ���������Ӧ��סԺ���ա�=������������

                    Sql = @"select 1 from t_opr_lis_sample where barcode_vchr = ?";
                    svcLis.CreateDatabaseParameter(1, out parm);
                    parm[0].Value = sampleVo.barCode;
                    DataTable dt3 = null;
                    svcLis.lngGetDataTableWithParameters(Sql, ref dt3, parm);
                    if (dt3 != null && dt3.Rows.Count > 0)
                    {
                        return false;
                    }

                    #endregion
                }

                #endregion

                string groupCode = string.Empty;
                string groupCodeArr = sampleVo.itemCode;

                #region t_bse_lis_check_item
                Sql = @"select t1.rptno_chr,
                               t1.pycode_chr,
                               t1.unit_chr,
                               t1.check_item_name_vchr,
                               t1.is_sex_related_chr,
                               t1.check_item_english_name_vchr,
                               t1.is_age_related_chr,
                               t1.is_sample_related_chr,
                               t1.formula_vchr,
                               t1.test_methods_vchr,
                               t1.clinic_meaning_vchr,
                               t1.check_item_id_chr,
                               t1.shortname_chr,
                               t1.is_qc_required_chr,
                               t1.resulttype_chr,
                               t1.ref_value_range_vchr,
                               t1.wbcode_chr,
                               t1.assist_code01_chr,
                               t1.assist_code02_chr,
                               t1.is_no_food_required_chr,
                               t1.is_physical_exam_required_chr,
                               t1.is_reservation_required_chr,
                               t1.sample_valid_time_dec,
                               t1.sample_valid_time_unit_chr,
                               t1.modify_dat,
                               t1.operatorid_chr,
                               t1.check_category_id_chr,
                               t1.ref_max_val_vchr,
                               t1.ref_min_val_vchr,
                               t1.sampletype_vchr,
                               t1.is_menses_related_chr,
                               t1.is_calculated_chr,
                               t1.formula_user_vchr,
                               t1.alarm_low_val_vchr,
                               t1.alarm_up_val_vchr,
                               t1.alert_value_range_vchr,
                               t1.itemprice_mny,
                               t2.apply_unit_id_chr,
                               t2.print_seq_int,                            
                               t3.apply_unit_name_vchr, 
                               t3.jclx_jj,
                               t3.jclx_jtj 
                          from t_bse_lis_check_item t1
                         inner join t_aid_lis_apply_unit_detail t2
                            on t2.check_item_id_chr = t1.check_item_id_chr
                         inner join t_aid_lis_apply_unit t3
                            on t2.apply_unit_id_chr = t3.apply_unit_id_chr
                         where t3.apply_unit_id_chr in ({0})
                         order by t2.print_seq_int ";
                #endregion

                DataTable dtCheckItem = null;
                Sql = string.Format(Sql, groupCodeArr);
                svcLis.lngGetDataTableWithoutParameters(Sql, ref dtCheckItem);

                #region ȫ�Զ�ѪҺ������/ȫ�Զ���������
                LabomanWrite2File(sampleVo.barCode);
                #endregion

                #region 2020--11-05 סԺ����: һ��Ҫֱ�ӷ��أ�����ִ���������
                if (sampleVo.typeId == 1)
                {
                    return true;
                }
                #endregion

                #region t_aid_lis_group_sample_type
                Sql = @"select t1.sample_group_id_chr,
                               t1.py_code_chr,
                               t1.wb_code_chr,
                               t1.assist_code01_chr,
                               t1.assist_code02_chr,
                               t1.is_hand_work_int,
                               t1.device_model_id_chr,
                               t1.remark_vchr,
                               t1.check_category_id_chr,
                               t1.sample_group_name_chr,
                               t1.print_title_vchr,
                               t1.print_seq_int,
                               t2.apply_unit_id_chr,
                               t3.sample_type_id_chr,
                               t4.sample_type_desc_vchr 
                          from t_aid_lis_sample_group t1
                         inner join t_aid_lis_sample_group_unit t2
                            on t1.sample_group_id_chr = t2.sample_group_id_chr
                          left join t_aid_lis_group_sample_type t3
                            on t1.sample_group_id_chr = t3.sample_group_id_chr
                          left join t_aid_lis_sampletype t4 
                            on t3.sample_type_id_chr = t4.sample_type_id_chr 
                         where t2.apply_unit_id_chr in ({0}) ";
                #endregion

                string sgid = string.Empty;
                List<string> lstSgid = new List<string>();
                string sampleGroupIdArr = string.Empty;
                Dictionary<string, string> dicGroupSample = new Dictionary<string, string>();
                DataTable dtSample = null;
                Sql = string.Format(Sql, groupCodeArr);
                svcLis.lngGetDataTableWithoutParameters(Sql, ref dtSample);
                foreach (DataRow dr in dtSample.Rows)
                {
                    sgid = dr["sample_group_id_chr"].ToString();
                    if (lstSgid.IndexOf(sgid) < 0)
                    {
                        lstSgid.Add(sgid);
                        sampleGroupIdArr += "'" + sgid + "',";
                    }
                    if (!dicGroupSample.ContainsKey(dr["apply_unit_id_chr"].ToString()))
                    {
                        dicGroupSample.Add(dr["apply_unit_id_chr"].ToString(), sgid);
                    }
                }
                sampleGroupIdArr = sampleGroupIdArr.TrimEnd(',');

                #region report_group_id_chr
                Sql = @"select t1.report_group_id_chr,
                               t1.report_group_name_vchr,
                               t1.print_title_vchr,
                               t1.print_category_id_chr,
                               t2.sample_group_id_chr 
                          from t_aid_lis_report_group t1, t_aid_lis_report_group_detail t2
                         where t1.report_group_id_chr = t2.report_group_id_chr
                           and t2.sample_group_id_chr in ({0}) ";
                #endregion

                DataTable dtReport = null;
                Sql = string.Format(Sql, sampleGroupIdArr);
                svcLis.lngGetDataTableWithoutParameters(Sql, ref dtReport);

                string applyId = string.Empty;
                string sampleId = string.Empty;
                string sampleGroupId = string.Empty;

                svcLis.m_lngGenerateNewID("t_opr_lis_application", "APPLICATION_ID_CHR", out applyId);
                svcLis.m_lngGenerateNewID("t_opr_lis_sample", "sample_id_chr", out sampleId);

                // 1. ���������
                #region t_opr_lis_application
                Sql = @"insert into t_opr_lis_application
                                          (application_id_chr,
                                           modify_dat,
                                           patientid_chr,
                                           application_dat,
                                           sex_chr, 
                                           patient_name_vchr,
                                           patient_subno_chr,
                                           age_chr,
                                           patient_type_id_chr,
                                           diagnose_vchr, 
                                           bedno_chr,
                                           icdcode_chr,
                                           patientcardid_chr,
                                           application_form_no_chr, 
                                           operator_id_chr,
                                           appl_empid_chr,
                                           appl_deptid_chr,
                                           summary_vchr,
                                           pstatus_int,
                                           emergency_int,           
                                           special_int,
                                           form_int,
                                           patient_inhospitalno_chr,
                                           sample_type_id_chr,      
                                           sample_type_vchr,
                                           check_content_vchr,
                                           oringin_dat,
                                           charge_info_vchr,
                                           orderunitrelation_vchr,
                                           printed_num,
                                           printed_date)
                                        values
                                          (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,  
                                           ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 
                                           ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
                                        ";

                string checkContent = string.Empty;
                List<string> lstContent = new List<string>();
                n = -1;
                svcLis.CreateDatabaseParameter(31, out parm);
                parm[++n].Value = applyId;
                parm[++n].Value = sampleVo.checkDate.Value;
                parm[++n].Value = sampleVo.patientId;
                parm[++n].Value = sampleVo.checkDate.Value;
                parm[++n].Value = sampleVo.sex;
                parm[++n].Value = sampleVo.patName;
                parm[++n].Value = null;
                parm[++n].Value = sampleVo.age;
                parm[++n].Value = "3";          // 1 סԺ; 2 ����; 3 ���
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = sampleVo.checkerId;
                parm[++n].Value = sampleVo.applyEmpId;
                parm[++n].Value = sampleVo.applyDeptId;
                parm[++n].Value = null;
                parm[++n].Value = 2;            // pstatus_int
                parm[++n].Value = 0;            // 20 
                parm[++n].Value = 0;
                parm[++n].Value = 1;            // �������: 0 ���ܱ༭; 1 �ɱ༭��
                parm[++n].Value = sampleVo.peNo;
                if (dtCheckItem != null && dtCheckItem.Rows.Count > 0 && dtSample != null && dtSample.Rows.Count > 0)
                {
                    foreach (DataRow dr3 in dtCheckItem.Rows)
                    {
                        if (lstContent.IndexOf(dr3["apply_unit_name_vchr"].ToString()) < 0)
                        {
                            lstContent.Add(dr3["apply_unit_name_vchr"].ToString());
                            checkContent += dr3["apply_unit_name_vchr"].ToString() + ",";
                        }
                    }
                    checkContent = checkContent.TrimEnd(',');
                    parm[++n].Value = dtSample.Rows[0]["sample_type_id_chr"].ToString();
                    parm[++n].Value = dtSample.Rows[0]["sample_type_desc_vchr"].ToString();
                    parm[++n].Value = checkContent;
                    sampleGroupId = dtSample.Rows[0]["sample_group_id_chr"].ToString();
                    sampleVo.sampleTypeId = dtSample.Rows[0]["sample_type_id_chr"].ToString();
                    sampleVo.sampleType = dtSample.Rows[0]["sample_type_desc_vchr"].ToString();
                    sampleVo.checkContent = checkContent;
                }
                else
                {
                    return false;
                }
                parm[++n].Value = sampleVo.checkDate.Value;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = 0;
                parm[++n].Value = null;
                svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);

                #endregion

                // 2. ���뵥�ı�����
                #region t_opr_lis_app_report
                Sql = @"insert into t_opr_lis_app_report
                                      (application_id_chr,
                                       report_group_id_chr,
                                       modify_dat,
                                       summary_vchr,
                                       operator_id_chr,
                                       status_int,
                                       report_dat,
                                       reportor_id_chr,
                                       confirm_dat,
                                       confirmer_id_chr,
                                       xml_summary_vchr,
                                       annotation_vchr,
                                       xml_annotation_vchr)
                                    values
                                      (?, ?, ?, ?, ?, ?, ?, ?, ?, ?,
                                       ?, ?, ?) ";
                n = -1;
                svcLis.CreateDatabaseParameter(13, out parm);
                parm[++n].Value = applyId;
                parm[++n].Value = dtReport.Rows[0]["report_group_id_chr"].ToString();
                parm[++n].Value = sampleVo.checkDate.Value;
                parm[++n].Value = null;
                parm[++n].Value = sampleVo.checkerId;
                parm[++n].Value = 1;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                // ����״̬
                //patVo.m_intReportStatus = 1;
                #endregion

                // 3. ���뵥Ҫ���ı걾��
                #region t_opr_lis_app_sample
                string[] sidArr = sampleGroupIdArr.Replace("'", "").Split(',');
                foreach (string sid in sidArr)
                {
                    Sql = @"insert into t_opr_lis_app_sample
                                          (application_id_chr,
                                           sample_group_id_chr,
                                           report_group_id_chr,
                                           sample_id_chr)
                                        values
                                          (?, ?, ?, ?)";

                    n = -1;
                    svcLis.CreateDatabaseParameter(4, out parm);
                    parm[++n].Value = applyId;
                    parm[++n].Value = sid;
                    parm[++n].Value = dtReport.Rows[0]["report_group_id_chr"].ToString();
                    parm[++n].Value = sampleId;
                    svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                }
                #endregion

                // 4.5.6. ���뵥Ҫ���ı걾���Ӧ�ļ�����Ŀ. ���뵥��Ӧ���뵥Ԫ . ���뵥Ԫ��Ӧ����Ŀ
                #region t_opr_lis_app_check_item . t_opr_lis_app_apply_unit . t_opr_lis_app_unit_item
                List<string> lstApplyUnit = new List<string>();
                foreach (DataRow dr1 in dtCheckItem.Rows)
                {
                    // ���뵥��Ӧ���뵥Ԫ
                    string applyUnitId = dr1["apply_unit_id_chr"].ToString();

                    // ���뵥Ҫ���ı걾���Ӧ�ļ�����Ŀ
                    Sql = @"insert into t_opr_lis_app_check_item
                                              (check_item_id_chr,
                                               sample_group_id_chr,
                                               report_group_id_chr,
                                               application_id_chr,
                                               itemprice_mny)
                                            values
                                              (?, ?, ?, ?, ?)";
                    n = -1;
                    svcLis.CreateDatabaseParameter(5, out parm);
                    parm[++n].Value = dr1["check_item_id_chr"].ToString();
                    parm[++n].Value = dicGroupSample[applyUnitId]; //dtSample.Rows[0]["sample_group_id_chr"].ToString();
                    parm[++n].Value = dtReport.Rows[0]["report_group_id_chr"].ToString();
                    parm[++n].Value = applyId;
                    parm[++n].Value = dr1["itemprice_mny"].ToString();
                    svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);


                    if (lstApplyUnit.IndexOf(applyUnitId) < 0)
                    {
                        lstApplyUnit.Add(applyUnitId);

                        Sql = @"insert into t_opr_lis_app_apply_unit
                                          (application_id_chr, user_group_string, apply_unit_id_chr)
                                        values
                                          (?, ?, ?)";
                        n = -1;
                        svcLis.CreateDatabaseParameter(3, out parm);
                        parm[++n].Value = applyId;
                        parm[++n].Value = ">>" + applyUnitId;
                        parm[++n].Value = applyUnitId;
                        svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                    }

                    // ���뵥Ԫ��Ӧ����Ŀ
                    Sql = @"insert into t_opr_lis_app_unit_item
                                          (application_id_chr, check_item_id_chr, apply_unit_id_chr)
                                        values
                                          (?, ?, ?)";

                    n = -1;
                    svcLis.CreateDatabaseParameter(3, out parm);
                    parm[++n].Value = applyId;
                    parm[++n].Value = dr1["check_item_id_chr"].ToString();
                    parm[++n].Value = applyUnitId;
                    svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                }
                #endregion

                // 7. �걾��Ϣ
                #region t_opr_lis_sample
                Sql = @"insert into t_opr_lis_sample
                                  (appl_dat,
                                   sex_chr,
                                   patient_name_vchr,
                                   patient_subno_chr,
                                   age_chr,
                                   patient_type_chr,
                                   diagnose_vchr,
                                   sampletype_vchr,
                                   samplestate_vchr,
                                   bedno_chr,
                                   icd_vchr,
                                   patientcardid_chr,
                                   barcode_vchr,
                                   sample_id_chr,
                                   patientid_chr,
                                   sampling_date_dat,
                                   operator_id_chr,
                                   modify_dat,
                                   appl_empid_chr,
                                   appl_deptid_chr,
                                   status_int,
                                   sample_type_id_chr,
                                   qcsampleid_chr,
                                   samplekind_chr,
                                   check_date_dat,
                                   accept_dat,
                                   acceptor_id_chr,
                                   application_id_chr,
                                   patient_inhospitalno_chr,
                                   confirm_dat,
                                   confirmer_id_chr,
                                   collector_id_chr,
                                   checker_id_chr,
                                   sendsample_empid_chr)
                                values
                                  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 
                                   ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 
                                   ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 
                                   ?, ?, ?, ? )
                                ";

                n = -1;
                svcLis.CreateDatabaseParameter(34, out parm);
                parm[++n].Value = sampleVo.checkDate.Value;
                parm[++n].Value = sampleVo.sex;
                parm[++n].Value = sampleVo.patName;
                parm[++n].Value = null;
                parm[++n].Value = sampleVo.age;
                parm[++n].Value = "3";      // ���
                parm[++n].Value = null;
                parm[++n].Value = dtSample.Rows[0]["sample_type_desc_vchr"].ToString();
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = sampleVo.barCode;         // barCode
                parm[++n].Value = sampleId;
                parm[++n].Value = sampleVo.patientId;
                parm[++n].Value = peSamplingDate == null ? DateTime.Now : peSamplingDate.Value;
                parm[++n].Value = sampleVo.checkerId;
                parm[++n].Value = sampleVo.checkDate.Value;
                parm[++n].Value = sampleVo.applyEmpId;
                parm[++n].Value = sampleVo.applyDeptId;       // 20
                parm[++n].Value = 3;                          // ��¼״̬  -1:��ʷ��¼ 0 -- ��Ч 1:��ʼ״̬ 2:�Ѳɼ� 3:�Ѻ��� 4:�Ѽ��� 5:�Ѵ����� 6:����� 7:���˻�
                parm[++n].Value = dtSample.Rows[0]["sample_type_id_chr"].ToString();
                parm[++n].Value = -1;
                parm[++n].Value = 1;
                parm[++n].Value = null;
                parm[++n].Value = sampleVo.checkDate.Value;     // ����ʱ��
                parm[++n].Value = sampleVo.checkerId;           // ������
                parm[++n].Value = applyId;
                parm[++n].Value = sampleVo.peNo;                // סԺ�� ?Ҫ��Ҫ������
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                #endregion

                // 8. �����Ϣ
                #region t_samplepack
                Sql = @"update t_samplepack set checkerid = ?, checkdate = ? where barcode = ?";
                n = -1;
                svcLis.CreateDatabaseParameter(3, out parm);
                parm[++n].Value = sampleVo.checkerId;
                parm[++n].Value = sampleVo.checkDate.Value;
                parm[++n].Value = sampleVo.barCode;
                svcLis.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                #endregion

                // ����2��Ŀ 1) 0000041 - AU680
                SaveBarcode2Item(sampleVo.barCode);

                return true;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            finally
            {
                svcLis.Dispose();
                svcLis = null;
            }

            return false;
        }
        #endregion

        #region SaveBarcode2Item
        /// <summary>
        /// SaveBarcode2Item
        /// </summary>
        /// <param name="barCode"></param>
        [AutoComplete]
        bool SaveBarcode2Item(string barCode)
        {
            long affectRows = 0;
            string Sql = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;

            try
            {
                Sql = @"select distinct d.device_model_id_chr, c.check_item_id_chr, d.device_check_item_name_vchr
                          from t_opr_lis_sample a
                         inner join t_opr_lis_app_check_item b
                            on b.application_id_chr = a.application_id_chr
                         inner join t_bse_lis_check_item_dev_item c
                            on c.check_item_id_chr = b.check_item_id_chr
                         inner join t_bse_lis_device_check_item d
                            on d.device_check_item_id_chr = c.device_check_item_id_chr
                           and d.device_model_id_chr = c.device_model_id_chr
                         where a.status_int >= 3
                            and d.device_model_id_chr  in ( '0000041','0000046','0000055', '0000040',
                                    '0000039', '0000034','0000021','0000031','0000026')
                           and a.barcode_vchr = ?
                         order by c.check_item_id_chr";

                DataTable dtItem = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = barCode;
                svc.lngGetDataTableWithParameters(Sql, ref dtItem, parm);
                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    Sql = @"delete from t_opr_lis_barcode2item where barcode = ?";
                    svc.CreateDatabaseParameter(1, out parm);
                    parm[0].Value = barCode;
                    svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);

                    Sql = @"insert into t_opr_lis_barcode2item
                              (barcode, itemid, itemname, checktime,deviceModelId)
                            values
                              (?, ?, ?, ?, ?)";
                    DateTime dtmNow = DateTime.Now;
                    foreach (DataRow dr in dtItem.Rows)
                    {
                        svc.CreateDatabaseParameter(5, out parm);
                        parm[0].Value = barCode;
                        parm[1].Value = dr["check_item_id_chr"].ToString();
                        parm[2].Value = dr["device_check_item_name_vchr"].ToString();
                        parm[3].Value = dtmNow;
                        parm[4].Value = dr["device_model_id_chr"].ToString();
                        svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                objLogger.LogError(ex);
            }
            return false;
        }
        #endregion

        #region סԺ������Ŀ��ѯ
        /// <summary>
        /// סԺ������Ŀ��ѯ
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetIpSample(string barCode)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            clsHRPTableService svc = null;
            try
            {
                #region Sql
                Sql = @"select a.patient_inhospitalno_chr as reg_no,
                               a.patientid_chr            as pat_code,
                               a.patient_name_vchr        as pat_name,
                               a.sex_chr                  as sex,
                               a.age_chr                  as age,
                               b.apply_unit_id_chr        as comb_code,
                               a.barcode_vchr             as samp_no,
                               b.apply_unit_id_chr        as as_group,
                               c.apply_unit_name_vchr     as as_group_name
                          from t_opr_lis_sample a
                         inner join t_opr_lis_app_apply_unit b
                            on a.application_id_chr = b.application_id_chr
                         inner join t_aid_lis_apply_unit c
                            on b.apply_unit_id_chr = c.apply_unit_id_chr
                         where ((a.accept_dat is null and a.acceptor_id_chr is null) or 
                                (a.issampleback = 1 and a.accept_dat is not null and a.acceptor_id_chr is not null))
                           and a.barcode_vchr = ?";
                #endregion

                svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = barCode;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null) dt.TableName = "ip";
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

        #endregion

        #region ���������ѯ
        /// <summary>
        /// ���������ѯ
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="ipNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable QueryAreaReport(string deptId, string startDate, string endDate, string ipNo)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            try
            {
                Sql = @"select a.name_vchr,
                               c.application_id_chr,
                               c.appl_dat,
                               c.patient_name_vchr,
                               c.sex_chr,
                               c.age_chr,
                               c.patient_inhospitalno_chr,
                               c.bedno_chr,
                               c.barcode_vchr,
                               d.report_group_id_chr,
                               d.report_print_chr,
                               d.report_dat,
                               d.confirm_dat,
                               e.apply_unit_id_chr
                          from t_opr_bih_order a
                         inner join t_opr_attachrelation b
                            on a.orderid_chr = b.sourceitemid_vchr
                         inner join t_opr_lis_sample c
                            on b.attachid_vchr = c.application_id_chr
                         inner join t_opr_lis_app_report d
                            on c.application_id_chr = d.application_id_chr
                         inner join t_opr_lis_app_apply_unit e
                            on d.application_id_chr = e.application_id_chr
                         where {0} 
                               (c.appl_dat between ? and ?)
                           and d.report_dat is not null 
                           {1}
                        ";

                Sql = @"select distinct a.name_vchr,
                               c.application_id_chr,
                               c.appl_dat,
                               c.patient_name_vchr,
                               c.sex_chr,
                               c.age_chr,
                               c.patient_inhospitalno_chr,
                               c.bedno_chr,
                               c.barcode_vchr,
                               d.report_group_id_chr,
                               d.report_print_chr,
                               d.report_dat,
                               d.confirm_dat
                          from t_opr_bih_order a
                         inner join t_opr_attachrelation b
                            on a.orderid_chr = b.sourceitemid_vchr
                         inner join t_opr_lis_sample c
                            on b.attachid_vchr = c.application_id_chr
                         inner join t_opr_lis_app_report d
                            on c.application_id_chr = d.application_id_chr
                         where {0} 
                               (c.appl_dat between ? and ?)
                           and d.report_dat is not null 
                           {1}
                        order by c.appl_dat, c.patient_inhospitalno_chr 
                        ";

                IDataParameter[] parm = null;
                DateTime dtmStart = Convert.ToDateTime(startDate + " 00:00:00");
                DateTime dtmEnd = Convert.ToDateTime(endDate + " 23:59:59");
                if (!string.IsNullOrEmpty(deptId))
                {
                    if (deptId.IndexOf(",") > 0)
                    {
                        if (!string.IsNullOrEmpty(ipNo))
                        {
                            Sql = string.Format(Sql, "c.appl_deptid_chr in (" + deptId + ") and ", "and c.patient_inhospitalno_chr = ?");
                            svc.CreateDatabaseParameter(3, out parm);
                            parm[0].Value = dtmStart;
                            parm[1].Value = dtmEnd;
                            parm[2].Value = ipNo;
                        }
                        else
                        {
                            Sql = string.Format(Sql, "c.appl_deptid_chr in (" + deptId + ") and ", "");
                            svc.CreateDatabaseParameter(2, out parm);
                            parm[0].Value = dtmStart;
                            parm[1].Value = dtmEnd;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ipNo))
                        {
                            Sql = string.Format(Sql, "c.appl_deptid_chr = ? and ", "and c.patient_inhospitalno_chr = ?");
                            svc.CreateDatabaseParameter(4, out parm);
                            parm[0].Value = deptId;
                            parm[1].Value = dtmStart;
                            parm[2].Value = dtmEnd;
                            parm[3].Value = ipNo;
                        }
                        else
                        {
                            Sql = string.Format(Sql, "c.appl_deptid_chr = ? and ", "");
                            svc.CreateDatabaseParameter(3, out parm);
                            parm[0].Value = deptId;
                            parm[1].Value = dtmStart;
                            parm[2].Value = dtmEnd;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(ipNo))
                    {
                        Sql = string.Format(Sql, "", "and c.patient_inhospitalno_chr = ?");
                        svc.CreateDatabaseParameter(3, out parm);
                        parm[0].Value = dtmStart;
                        parm[1].Value = dtmEnd;
                        parm[2].Value = ipNo;
                    }
                    else
                    {
                        Sql = string.Format(Sql, "", "");
                        svc.CreateDatabaseParameter(2, out parm);
                        parm[0].Value = dtmStart;
                        parm[1].Value = dtmEnd;
                    }
                }
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null) dt.TableName = "arealisreport";
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return dt;
        }
        #endregion

        #region ͨ�����������뵥��
        /// <summary>
        /// ͨ�����������뵥��
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [AutoComplete]
        public string GetApplicationIdByBarcode(string barCode)
        {
            string Sql = string.Empty;
            string appId = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                Sql = @"select t.application_id_chr 
                         from t_opr_lis_sample t
                        where t.barcode_vchr = ?";

                DataTable dt = null;
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = barCode;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    appId = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return appId;
        }
        #endregion

        #region ����Դд���ȡ�ļ���¼
        /// <summary>
        /// ����Դд���ȡ�ļ���¼
        /// </summary>
        /// <param name="lstFileFullName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long SaveAllergenRec(List<string> lstFileFullName)
        {
            long ret = 0;
            string Sql = string.Empty;
            try
            {
                Sql = @"insert into t_opr_lis_allergen_rec
                          (recid, filefullname, recdate)
                        values
                          (seq_allergen_recid.nextval, ?, sysdate)";

                IDataParameter[] parm = null;
                clsHRPTableService svc = new clsHRPTableService();
                foreach (string fileFullName in lstFileFullName)
                {
                    svc.CreateDatabaseParameter(1, out parm);
                    parm[0].Value = fileFullName;
                    svc.lngExecuteParameterSQL(Sql, ref ret, parm);
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return ret;
        }
        #endregion

        #region  ϣɭ����ȫ�Զ�ѪҺ�����Ǽ�� д�ļ�
        /// <summary>
        /// 
        /// </summary>
        /// <param name="barCode"></param>
        void LabomanWrite2File(string barCode)
        {
            if (string.IsNullOrEmpty(barCode))
                return;

            string filePath = string.Empty;
            string user = string.Empty;
            string pwd = string.Empty;
            clsHRPTableService svcLis = null;

            try
            {
                string Sql = @"select a.parmcode_chr, a.parmvalue_vchr, a.note_vchr
                          from t_bse_sysparm a
                         where a.status_int = 1
                           and a.parmcode_chr = '7013'";
                svcLis = new clsHRPTableService();
                DataTable dtParm = null;
                svcLis.lngGetDataTableWithoutParameters(Sql, ref dtParm);
                if (dtParm != null && dtParm.Rows.Count > 0)
                {
                    DataRow drParm = dtParm.Rows[0];
                    if (drParm["parmvalue_vchr"] != DBNull.Value && drParm["parmvalue_vchr"].ToString().Trim() != "" &&
                           drParm["note_vchr"] != DBNull.Value && drParm["note_vchr"].ToString().Trim() != "")
                    {
                        filePath = drParm["note_vchr"].ToString().Split(';')[0];
                        user = drParm["note_vchr"].ToString().Split(';')[1];
                        pwd = drParm["note_vchr"].ToString().Split(';')[2];
                    }
                }
                else
                    return;

                Sql = @"select a.barcode_vchr,
                                    b.apply_unit_id_chr,
                                    d.device_model_id_chr,
                                    e.devicename_vchr 
                                    from 
                                    t_opr_lis_sample a
                                    left join t_opr_lis_app_apply_unit b
                                    on a.application_id_chr = b.application_id_chr
                                    left join t_aid_lis_sample_group_unit c
                                    on b.apply_unit_id_chr = c.apply_unit_id_chr
                                    left join t_aid_lis_sample_group_model d
                                    on c.sample_group_id_chr = d.sample_group_id_chr
                                    left join t_bse_lis_device e
                                    on d.device_model_id_chr = e.device_model_id_chr
                                    where a.status_int > 1 and a.barcode_vchr = {0}";
                Sql = string.Format(Sql, barCode);
                DataTable dt = null;
                svcLis.lngGetDataTableWithoutParameters(Sql, ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string strApplyUnit = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        string deviceName = dr["devicename_vchr"].ToString();
                        string appplyUnit = dr["apply_unit_id_chr"].ToString();
                        if (!string.IsNullOrEmpty(appplyUnit) && deviceName == "Laboman")
                        {
                            strApplyUnit += "'" + appplyUnit + "',";
                        }
                    }
                    if (!string.IsNullOrEmpty(strApplyUnit))
                    {
                          Sql = @"select distinct c.check_item_id_chr, d.device_check_item_name_vchr
                                  from t_opr_lis_sample a
                                 inner join t_opr_lis_app_check_item b
                                    on b.application_id_chr = a.application_id_chr
                                 inner join t_bse_lis_check_item_dev_item c
                                    on c.check_item_id_chr = b.check_item_id_chr
                                 inner join t_bse_lis_device_check_item d
                                    on d.device_check_item_id_chr = c.device_check_item_id_chr
                                   and d.device_model_id_chr = c.device_model_id_chr
                                 where a.status_int >= 3
                                   and a.barcode_vchr = '{0}'
                                   and a.status_int > 0
                                 order by c.check_item_id_chr";

                        Sql = string.Format(Sql, barCode);
                        svcLis.lngGetDataTableWithoutParameters(Sql, ref dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string content = ",CBC";
                            foreach(DataRow dr in dt.Rows)
                            {
                                string itemName = dr["device_check_item_name_vchr"].ToString();
                                if (itemName.Contains("#") && !content.Contains("DIFF"))
                                    content += "+DIFF";
                                if (itemName.Contains("%") && !content.Contains("RET"))
                                    content += "+RET";
                            }
                            content += "+NRBC|";
                            bool status = connectState(filePath, user, pwd);
                            if (status)
                            {
                                //�����ļ��е�Ŀ¼
                                DirectoryInfo theFolder = new DirectoryInfo(filePath);
                                string filename = theFolder.ToString() + "\\RET.txt";
                                FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.None);
                                if (fs != null)
                                {
                                    StreamWriter sw = new StreamWriter(fs);
                                    sw.Write(barCode + content);
                                    sw.Flush();
                                    sw.Close();
                                    fs.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svcLis = null;
            }
        }
        #endregion

        #region ����Զ�̹����ļ���
        /// <summary>
        /// ����Զ�̹����ļ���
        /// </summary>
        /// <param name="path">Զ�̹����ļ��е�·��</param>
        /// <param name="userName">�û���</param>
        /// <param name="passWord">����</param>
        /// <returns></returns>
        public static bool connectState(string path, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = "net use " + path + " " + passWord + " /user:" + userName;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
                else
                {
                    ExceptionLog.OutPutException(errormsg);
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }
        #endregion

        
    }
    /// <summary>
    /// clsLIS_Svc2
    /// </summary>
    [Transaction(TransactionOption.NotSupported)]
    [ObjectPooling(Enabled = true)]
    public class clsLIS_Svc2 : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ���-��ȡ���������Ϣ
        /// <summary>
        /// ���-��ȡ���������Ϣ
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetPeSample(string barCode)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            clsHRPTableService svcPe = null;
            try
            {
                #region Sql
                Sql = @"select distinct a.reg_no,
                               b.pat_code,
                               b.pat_name,
                               b.sex,
                               b.age,
                               c.comb_code,
                               c.samp_no,
                               d.as_group,
                               d.as_group_name
                          from tj_register a
                         inner join tj_patient b
                            on a.pat_code = b.pat_code
                         inner join as_samp_cen c
                            on a.reg_no = c.reg_no
                          left join def_comb_tj_as d
                            on c.comb_code = d.comb_code
                         where c.samp_no = ?";
                #endregion

                svcPe = new clsHRPTableService();
                svcPe.m_mthSetDataBase_Selector(1, 15);
                IDataParameter[] parm = null;
                svcPe.CreateDatabaseParameter(1, out parm);
                parm[0].Value = barCode;
                svcPe.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null) dt.TableName = "pe";
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            finally
            {
                svcPe.Dispose();
                svcPe = null;
            }
            return dt;
        }
        #endregion

        #region ����Ƿ��(΢��)
        /// <summary>
        /// ΢�ż���Ƿ��
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool IsWechatBanding(string cardNo)
        {
            string Sql = @"select t.cardno from opRegWeChatBinding t where t.cardno = ? and t.status = 1";
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = cardNo;

                DataTable dt = null;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();

                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;    // ���ڰ�
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return false;
        }
        #endregion

        #region ͨ�����뵥���ұ걾��Ϣ(΢��)
        /// <summary>
        /// ͨ�����뵥���ұ걾��Ϣ(΢��)
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetWechatSampleInfo(string applicationId)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            try
            {
                Sql = @"select a.application_id_chr,
                               a.patientcardid_chr,
                               a.patientid_chr,
                               a.patient_inhospitalno_chr,
                               a.appl_dat,
                               a.accept_dat,
                               a.confirm_dat, 
                               b.check_content_vchr
                          from t_opr_lis_sample a
                          left join t_opr_lis_application b
                            on a.application_id_chr = b.application_id_chr
                         where a.application_id_chr = ?
                           and a.patientcardid_chr is not null
                           and a.confirm_dat is not null";

                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = applicationId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null) dt.TableName = "WechatSample";
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return dt;
        }
        #endregion

        #region ��ȡ�걾����ԭ��
        /// <summary>
        /// ��ȡ�걾����ԭ��
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetRejectReason()
        {
            string Sql = string.Empty;
            DataTable dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            try
            {
                Sql = @"select t.dictname_vchr from t_aid_dict t where t.dictkind_chr = '0000000066' and t.dictid_chr <> '0'";
                svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                if (dt != null) dt.TableName = "reject";
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return dt;
        }
        #endregion

        #region ͨ�������ŷ������ƿ���
        /// <summary>
        /// ͨ�������ŷ������ƿ���
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [AutoComplete]
        public string GetCardNoByRecipeId(string recipeId)
        {
            string Sql = string.Empty;
            string CardNo = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            try
            {
                Sql = @"select b.patientcardid_chr
                          from t_opr_outpatientrecipe a
                         inner join t_bse_patientcard b
                            on a.patientid_chr = b.patientid_chr
                         where a.outpatrecipeid_chr = ?";

                DataTable dt = null;
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = recipeId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CardNo = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return CardNo;
        }
        #endregion

        #region ��ȡҽ���ֵ�.���뵥Ԫ.��������
        /// <summary>
        /// ��ȡҽ���ֵ�.���뵥Ԫ.��������
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="applyUnitId"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetOrderDicSamplingTimes(string orderId, string applyUnitId)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            try
            {
                Sql = @"select a.orderid_chr, b.lisapplyunitid_chr as appUnitId, b.samplingtimes
                          from t_opr_bih_order a
                         inner join t_bse_bih_orderdic b
                            on a.orderdicid_chr = b.orderdicid_chr
                         where a.orderid_chr = ?
                           and b.lisapplyunitid_chr = ?
                           and b.samplingtimes > 1";

                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = orderId;
                parm[1].Value = applyUnitId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null) dt.TableName = "samplingtimes";
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return dt;
        }
        #endregion

        #region ��ȡ���뵥�����ˡ������
        /// <summary>
        /// ��ȡ���뵥�����ˡ������
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetApplicationOperInfo(string applicationId)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            try
            {
                Sql = @"select t.status_int,
                               t.confirmer_id_chr,
                               t.operator_id_chr,
                               t.checker_id_chr
                          from t_opr_lis_sample t
                         where t.application_id_chr = ?
                           and t.status_int > 0";

                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = applicationId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null) dt.TableName = "report";
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return dt;
        }
        #endregion

        #region ��ѯ������ĿID��ʷֵ
        /// <summary>
        /// ��ѯ������ĿID��ʷֵ
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="itemIdArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetCheckItemHistoryValue(string applicationId, string itemIdArr)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            clsHRPTableService svc = new clsHRPTableService();
            try
            {
                Sql = @"select c.check_item_id_chr    as itemId,
                               c.check_item_name_vchr as itemName,
                               c.result_vchr          as itemResult,
                               c.modify_dat           as itemDate
                          from t_opr_lis_application a
                         inner join t_opr_lis_sample b
                            on a.application_id_chr = b.application_id_chr
                         inner join t_opr_lis_check_result c
                            on b.sample_id_chr = c.sample_id_chr
                         where c.status_int = 1
                           and c.check_item_id_chr in ({0})
                           and a.patientid_chr in
                               (select t.patientid_chr
                                  from t_opr_lis_sample t
                                 where t.application_id_chr = ?)";

                Sql = string.Format(Sql, itemIdArr);
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = applicationId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null) dt.TableName = "LisItemHistoryValue";
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return dt;
        }
        #endregion

        #region ��ѯ:ҽ��->������Ŀ-�������뵥Ԫ(һ�Զ�,��:������)
        /// <summary>
        /// ��ѯ:ҽ��->������Ŀ-�������뵥Ԫ(һ�Զ�,��:������)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetOrderDicLisApplyUnitByOrderId(string orderId)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            try
            {
                Sql = @"select a.orderid_chr          as orderId,
                               b.orderdicid_chr       as orderDicId,
                               b.lisapplyunitid_chr   as mainLisApplyUnitId,
                               c.lisapplyunitid_chr   as plusLisApplyUnitId,
                               c.sortno_int           as sortNo,
                               d.apply_unit_name_vchr as applyUnitName,
                               0                      as isUsed 
                          from t_opr_bih_order a
                         inner join t_bse_bih_orderdic b
                            on a.orderdicid_chr = b.orderdicid_chr
                         inner join t_bse_bih_orderdic_applyunit c
                            on b.orderdicid_chr = c.orderdicid_chr
                         inner join t_aid_lis_apply_unit d
                            on c.lisapplyunitid_chr = d.apply_unit_id_chr
                         where a.orderid_chr = ?
                         order by c.sortno_int ";

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(1, out param);
                param[0].Value = orderId;
                objHRPSvc.lngGetDataTableWithParameters(Sql, ref dt, param);
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dt;
        }
        #endregion

        #region ��ѯ:������Ŀ-�������뵥Ԫ(һ�Զ�,��:������)
        /// <summary>
        /// ��ѯ:������Ŀ-�������뵥Ԫ(һ�Զ�,��:������)
        /// </summary>
        /// <param name="orderDicId"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetOrderDicLisApplyUnit(string orderDicId)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            try
            {
                Sql = @"select ''                     as orderId,
                               b.orderdicid_chr       as orderDicId,
                               b.lisapplyunitid_chr   as mainLisApplyUnitId,
                               c.lisapplyunitid_chr   as plusLisApplyUnitId,
                               c.sortno_int           as sortNo,
                               d.apply_unit_name_vchr as applyUnitName,
                               0                      as isUsed 
                          from t_bse_bih_orderdic b
                         inner join t_bse_bih_orderdic_applyunit c
                            on b.orderdicid_chr = c.orderdicid_chr
                         inner join t_aid_lis_apply_unit d
                            on c.lisapplyunitid_chr = d.apply_unit_id_chr
                         where b.orderdicid_chr = ?
                         order by c.sortno_int ";

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(1, out param);
                param[0].Value = orderDicId;
                objHRPSvc.lngGetDataTableWithParameters(Sql, ref dt, param);
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dt;
        }
        #endregion

        #region GetInstrumentSerialSetting
        /// <summary>
        /// GetInstrumentSerialSetting
        /// </summary>
        /// <param name="strData_Acquisition_Computer_IP"></param>
        /// <param name="objConfig_List"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngGetInstrumentSerialSetting(string strData_Acquisition_Computer_IP, out clsLIS_Equip_ConfigVO[] objConfig_List)
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
                              from t_bse_lis_device d
                              left join t_bse_lis_serialsetup s
                                on d.device_model_id_chr = s.device_model_id_chr
                             inner join t_bse_lis_device_model m
                                on d.device_model_id_chr = m.device_model_id_chr
                             where d.end_date_dat is null 
                               and lower(d.dataacquisitioncomputerip_chr) = lower(?) ";

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
        #endregion

        #region У�����Դ�Ƿ��Ѷ�ȡ�ļ�
        /// <summary>
        /// У�����Դ�Ƿ��Ѷ�ȡ�ļ�
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool AllergenIsRead(string fileFullName)
        {
            string Sql = string.Empty;
            bool isRead = false;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();

                Sql = @"select 1 from t_opr_lis_allergen_rec t where t.filefullname = ?";

                DataTable dt = null;
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = fileFullName;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    isRead = true;
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return isRead;
        }
        #endregion

        #region ���뵥ID��Ӧ�����뵥ԪID
        /// <summary>
        /// ���뵥ID��Ӧ�����뵥ԪID
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        [AutoComplete]
        public List<string> GetAppUnitIdByAppId(string applicationId)
        {
            string Sql = string.Empty;
            List<string> lstAppUnitId = new List<string>();
            try
            {
                clsHRPTableService svc = new clsHRPTableService();

                Sql = @"select a.apply_unit_id_chr
                          from t_opr_lis_app_apply_unit a
                         where a.application_id_chr = ?";

                DataTable dt = null;
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = applicationId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        lstAppUnitId.Add(dr["apply_unit_id_chr"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return lstAppUnitId;
        }
        #endregion

        #region �����������AU680��Ŀ
        /// <summary>
        /// �����������AU680��Ŀ
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetAu680ItemByBarCode(string barCode)
        {
            string Sql = string.Empty;
            DataTable dt = null;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();

                Sql = @"select distinct c.check_item_id_chr, d.device_check_item_name_vchr
                          from t_opr_lis_sample a
                         inner join t_opr_lis_app_check_item b
                            on b.application_id_chr = a.application_id_chr
                         inner join t_bse_lis_check_item_dev_item c
                            on c.check_item_id_chr = b.check_item_id_chr
                         inner join t_bse_lis_device_check_item d
                            on d.device_check_item_id_chr = c.device_check_item_id_chr
                           and d.device_model_id_chr = c.device_model_id_chr
                         where a.status_int >= 3
                            and d.device_model_id_chr  in ( '0000041','0000046','0000055', '0000040',
                                     '0000039', '0000034','0000021','0000031','0000026')
                           and a.barcode_vchr = ?
                           and a.status_int > 0
                         order by c.check_item_id_chr";

                Sql = @"select deviceModelId,
                               itemid   as device_check_item_id_chr,
                               itemname as device_check_item_name_vchr
                          from t_opr_lis_barcode2item
                         where barcode = ?
                         order by itemid";

                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = barCode;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt == null)
                    dt = new DataTable();
                dt.TableName = "au680";
            }
            catch (Exception ex)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(ex);
            }
            return dt;
        }
        #endregion
    }
}
