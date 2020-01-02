using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using System.Collections;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;
using System.Data;

namespace com.digitalwave.iCare.middletier.LIS
{
    /// <summary>
    /// 检验数据采集中间件
    /// </summary>
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsLIS_DataAcquisitionServ : clsMiddleTierBase
    {
        #region SQL
        private const string c_strAddLabResult = @"insert into t_opr_lis_result
  (idx_int,
   deviceid_chr,
   device_sampleid_chr,
   check_dat,
   device_check_item_name_vchr,
   result_vchr,
   unit_vchr,
   refrange_vchr,
   min_val_dec,
   max_val_dec,
   abnormal_flag_vchr,
   graph_img,
   graph_format_name_vchr,
   is_graph_result_num,
   result2_vchr)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

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

        #endregion

        #region 增加检验仪器结果
        /// <summary>
        /// 增加检验仪器结果
        /// </summary>
        /// <param name="arlResult"></param>
        /// <param name="p_arlResultOut"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngAddLabResult(List<clsLIS_Device_Test_ResultVO> p_arlResult, out List<clsLIS_Device_Test_ResultVO> p_arlResultOut)
        {
            p_arlResultOut = null;
            clsLIS_Device_Test_ResultVO[] objResultArr = p_arlResult.ToArray();
            clsLIS_Device_Test_ResultVO[] objResultOutArr = null;

            clsLIS_Svc objLIS_Svc = new clsLIS_Svc();

            long lngRes = objLIS_Svc.lngAddLabResult(p_arlResult, out p_arlResultOut);
            //if (lngRes > 0)
            //{
            //    p_arlResultOut = new ArrayList();
            //    p_arlResultOut.AddRange(objResultOutArr);
            //}
            return lngRes;
        }
        /// <summary>
        /// 增加检验仪器结果
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_objOutResultArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long lngAddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr)
        {
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

            long lngRes = 1;
            long lngRecEff = -1;
            string strSQL = null;

            #region 判断是追加结果还是新增一次结果 用于Stago、CentaurCP等仪器
            /* 原理：获取上次取得该样本结果的最大序列
               根据序列，仪器和标本号查找log表得起始值
               找到结果进行结果校对哈希比较，如果不一致立即跳过进行原操作
               否则追加插入结果，修改req日期.log起始值和日期 */
            //<--------------------------------------------------------  
            string[] strConditionList = null; //当blnFlag为false时有值

            clsLIS_QueryDataAcquisitionServ objQueryServ = new clsLIS_QueryDataAcquisitionServ();
            bool blnFlag = objQueryServ.m_blnIsAppendResult(ref has, p_objResultArr, out strConditionList);
            //--------------------------------------------------------------------------------------->
            #endregion

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                clsLIS_Device_Test_ResultVO[] objResultList = p_objResultArr;


                DateTime dtmNow = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                DateTime dtmCheck = dtmNow;
                string strCheckDateTime = null;
                #region 检验时间
                if (objResultList[0].strCheck_Date != null)
                    objResultList[0].strCheck_Date = objResultList[0].strCheck_Date.Trim();

                if (Microsoft.VisualBasic.Information.IsDate(objResultList[0].strCheck_Date))
                {
                    dtmCheck = System.DateTime.Parse(objResultList[0].strCheck_Date);
                }
                strCheckDateTime = dtmCheck.ToString("yyyy-MM-dd HH:mm:ss");
                dtmCheck = Convert.ToDateTime(strCheckDateTime);
                #endregion
                string strDevice_ID = objResultList[0].strDevice_ID.Trim();
                string strDevice_Sample_ID = objResultList[0].strDevice_Sample_ID.Trim();
                clsLogText objLogger = new clsLogText();

                int intImportReq = -1;
                if (blnFlag)
                {
                    //objLogger.Log2File("D:\\code\\logData.txt", "结果新增");

                    lngRes = clsPublicSvc.m_lngGetSequence("seq_lis_device_result_log", out intImportReq);

                    if (lngRes == 1)
                    {
                        #region 写 t_opr_lis_result_import_req 表
                        System.Data.IDataParameter[] objDPArr5 = null;
                        objHRPSvc.CreateDatabaseParameter(7, out objDPArr5);
                        objDPArr5[0].Value = strDevice_ID;
                        objDPArr5[1].Value = intImportReq;
                        objDPArr5[2].Value = strDevice_Sample_ID;
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
                        int[] intIdxArr = null;
                        lngRes = clsPublicSvc.m_lngGetSequenceArr("seq_lis_device_result", objResultList.Length, out intIdxArr);
                        if (lngRes <= 0)
                        {
                            ContextUtil.SetAbort();
                            return lngRes;
                        }
                        int intBEGIN_IDX_INT = intIdxArr[0];
                        int intEND_IDX_INT = intIdxArr[intIdxArr.Length - 1];

                        #region 写 t_opr_lis_result 表
                        DbType[] m_dbType = new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.DateTime,
                            DbType.String, DbType.String, DbType.String, DbType.String,
                            DbType.Double, DbType.Double, DbType.String, DbType.Byte,
                            DbType.String, DbType.Int32 };

                        object[][] objValues = new object[14][];
                        for (int i = 0; i < objValues.Length; i++)
                        {
                            objValues[i] = new object[objResultList.Length];
                        }
                        clsLIS_Device_Test_ResultVO objResultTemp = null;
                        for (int iRow = 0; iRow < objResultList.Length; iRow++)
                        {
                            objResultTemp = objResultList[iRow];
                            objResultTemp.intIndex = intIdxArr[iRow];

                            objValues[0][iRow] = objResultTemp.intIndex;
                            objValues[1][iRow] = strDevice_ID;
                            objValues[2][iRow] = strDevice_Sample_ID;

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

                        }
                        lngRes = 0;
                        lngRes = objHRPSvc.m_lngSaveArrayWithParameters(c_strAddLabResult, objValues, m_dbType);

                        #endregion

                        if (lngRes == 1)
                        {
                            #region 写 t_opr_lis_result_log 表

                            if (intBEGIN_IDX_INT > intEND_IDX_INT)
                            {
                                int tmpIdx = intEND_IDX_INT;
                                intEND_IDX_INT = intBEGIN_IDX_INT;
                                intBEGIN_IDX_INT = tmpIdx;
                            }

                            System.Data.IDataParameter[] objDPArr1 = null;
                            objHRPSvc.CreateDatabaseParameter(7, out objDPArr1);
                            objDPArr1[0].Value = strDevice_ID;
                            objDPArr1[1].Value = strDevice_Sample_ID;
                            objDPArr1[2].DbType = DbType.DateTime;
                            objDPArr1[2].Value = Convert.ToDateTime(strCheckDateTime);
                            objDPArr1[3].Value = intBEGIN_IDX_INT;
                            objDPArr1[4].Value = intEND_IDX_INT;
                            objDPArr1[5].Value = "1";
                            objDPArr1[6].Value = intImportReq;

                            lngRes = 0;
                            lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddLabResultLog, ref lngRecEff, objDPArr1);

                            #endregion
                        }
                    }
                }
                else //追加结果AppendResult
                {
                    //objLogger.Log2File("D:\\code\\logData.txt", "结果追加");

                    #region 写 t_opr_lis_result 表
                    strCheckDateTime = strConditionList[3].Trim();
                    dtmCheck = Convert.ToDateTime(strCheckDateTime);
                    intImportReq = Convert.ToInt32(strConditionList[2]);

                    //int intIdx = m_mthGetNewResultIndex(objResultList.Length, false) + 1;
                    //int intBEGIN_IDX_INT = intIdx;
                    //int intEND_IDX_INT = intBEGIN_IDX_INT + objResultList.Length - 1;


                    int[] intIdxArr = null;
                    lngRes = clsPublicSvc.m_lngGetSequenceArr("seq_lis_device_result", objResultList.Length, out intIdxArr);
                    if (lngRes <= 0)
                    {
                        ContextUtil.SetAbort();
                        return lngRes;
                    }
                    List<int> lstSeq = new List<int>();
                    lstSeq.AddRange(intIdxArr);
                    lstSeq.Sort();
                    int intBEGIN_IDX_INT = lstSeq[0]; //intIdxArr[0];
                    int intEND_IDX_INT = lstSeq[lstSeq.Count - 1]; //intIdxArr[intIdxArr.Length - 1];

                    DbType[] m_dbType1 = new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.DateTime,
                            DbType.String, DbType.String, DbType.String, DbType.String,
                            DbType.Double, DbType.Double, DbType.String, DbType.Byte,
                            DbType.String, DbType.Int32 };

                    object[][] objValues1 = new object[14][];
                    for (int i = 0; i < objValues1.Length; i++)
                    {
                        objValues1[i] = new object[objResultList.Length];
                    }
                    clsLIS_Device_Test_ResultVO objResultTemp = null;
                    for (int iRow = 0; iRow < objResultList.Length; iRow++)
                    {
                        objResultTemp = objResultList[iRow];
                        objResultTemp.intIndex = intIdxArr[iRow];

                        objValues1[0][iRow] = objResultTemp.intIndex;
                        objValues1[1][iRow] = strDevice_ID;
                        objValues1[2][iRow] = strDevice_Sample_ID;

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

                    }
                    lngRes = 0;
                    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(c_strAddLabResult, objValues1, m_dbType1);

                    #endregion

                    if (lngRes == 1)
                    {
                        #region 更新 t_opr_lis_result_log 表
                        strSQL = @"update t_opr_lis_result_log
   set end_idx_int = ?
 where deviceid_chr = ?
   and trim(device_sampleid_chr) = ?
   and check_dat = ?
   and import_req_int = ?";

                        System.Data.IDataParameter[] objDPArr1 = null;
                        objHRPSvc.CreateDatabaseParameter(5, out objDPArr1);
                        objDPArr1[0].Value =  intEND_IDX_INT  ;
                        objDPArr1[1].Value = strConditionList[1]; //仪器ID
                        objDPArr1[2].Value = strConditionList[0]; //仪器样本ID
                        objDPArr1[3].DbType = DbType.DateTime;
                        objDPArr1[3].Value = Convert.ToDateTime(strConditionList[3]); //检验日期
                        objDPArr1[4].Value = Convert.ToInt32(strConditionList[2]); //系统内部结果序列

                        lngRes = 0;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDPArr1);
                        if (lngRecEff < 1)
                        {
                            System.EnterpriseServices.ContextUtil.SetAbort();
                        }
                        #endregion
                    }
                }
                System.Data.DataTable dtbRelation = null;
                if (lngRes == 1)//无论此步骤成功与否都不应影响原始数据的进入.
                {
                    #region  查找核收表（t_opr_lis_device_relation）表中要做关联的记录

                    strSQL = @"select deviceid_chr, seq_id_device_chr, sample_id_chr
  from t_opr_lis_device_relation
 where status_int = 1
   and deviceid_chr = ?
   and device_sampleid_chr = ?
   and check_dat between ? and ?";

                    System.Data.IDataParameter[] objDPArrs3 = null;
                    objHRPSvc.CreateDatabaseParameter(4, out objDPArrs3);
                    objDPArrs3[0].Value = strDevice_ID;
                    objDPArrs3[1].Value = strDevice_Sample_ID;
                    objDPArrs3[2].DbType = DbType.DateTime;
                    objDPArrs3[2].Value = (Convert.ToDateTime(strCheckDateTime)).Date;
                    objDPArrs3[3].DbType = DbType.DateTime;
                    objDPArrs3[3].Value = (Convert.ToDateTime(strCheckDateTime)).Date.AddHours(24);

                    lngRes = 0;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbRelation, objDPArrs3);

                    #endregion
                    if (lngRes == 1)
                    {
                        #region 更新 t_opr_lis_device_relation 表

                        if (dtbRelation != null && dtbRelation.Rows.Count != 0)
                        {
                            string strSeq = dtbRelation.Rows[0]["seq_id_device_chr"].ToString().Trim();

                            strSQL = @"update t_opr_lis_device_relation
   set device_sampleid_chr = ?,
       check_dat           = ?,
       import_req_int      = ?,
       status_int          = 2
 where seq_id_device_chr = ?
   and deviceid_chr = ?";

                            System.Data.IDataParameter[] objDPArrs2 = null;
                            objHRPSvc.CreateDatabaseParameter(5, out objDPArrs2);
                            objDPArrs2[0].Value = strDevice_Sample_ID;
                            objDPArrs2[1].DbType = DbType.DateTime;
                            objDPArrs2[1].Value = Convert.ToDateTime(strCheckDateTime);
                            objDPArrs2[2].Value = intImportReq;
                            objDPArrs2[3].Value = strSeq.Trim();
                            objDPArrs2[4].Value = strDevice_ID.Trim();

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
            p_objOutResultArr = p_objResultArr;
            return lngRes;
        }


        /// <summary>
        /// 增加检验仪器结果, 多样本
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_blnMuiltySample"> TRUE = 多样本</param>
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
                    strSampleID = p_objResultArr[idx].strDevice_Sample_ID + ";" + p_objResultArr[idx].strCheck_Date;
                    if (strSampleID != strSampleIDTemp)
                    {
                        if (!lstSampleID.Contains(strSampleID))
                        {
                            lstSampleID.Add(strSampleID);
                        }
                        strSampleIDTemp = strSampleID;
                    }
                }
                clsLIS_Svc objLIS_Svc = new clsLIS_Svc();
                List<clsLIS_Device_Test_ResultVO> p_arlResult = null;
                List<clsLIS_Device_Test_ResultVO> p_arlResultOut = null;
                string[] strDataArr = null;
                string strCheckDate = null;
                foreach (string str in lstSampleID)
                {
                    strDataArr = str.Split(';');
                    if (strDataArr == null || strDataArr.Length != 2)
                    {
                        continue;
                    }
                    strSampleID = strDataArr[0];
                    strCheckDate = strDataArr[1];
                    lstResult.Clear();
                    for (idx = 0; idx < p_objResultArr.Length; idx++)
                    {

                        if (strSampleID == p_objResultArr[idx].strDevice_Sample_ID && strCheckDate == p_objResultArr[idx].strCheck_Date)
                        {
                            lstResult.Add(p_objResultArr[idx]);
                        }
                    }
                    if (lstResult.Count > 0)
                    {
                        p_arlResult = null;
                        p_arlResult = new List<clsLIS_Device_Test_ResultVO>();
                        p_arlResultOut = null;
                        p_arlResult.AddRange(lstResult.ToArray());
                        lngRes = objLIS_Svc.lngAddLabResult(p_arlResult, out p_arlResultOut);
                        if (lngRes > 0 && p_arlResultOut != null && p_arlResultOut.Count > 0)
                        {
                            clsLIS_Device_Test_ResultVO[] objRes = p_arlResultOut.ToArray();
                            lstOutResult.AddRange(objRes);
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

        #region 获取最大索引
        //        /// <summary>
        //        /// 获取最大索引
        //        /// </summary>
        //        /// <param name="p_intRowNum"></param>
        //        /// <param name="p_blnNext"></param>
        //        /// <returns></returns>
        //        [AutoComplete]
        //        public int m_mthGetNewResultIndex(int p_intRowNum, bool p_blnNext)
        //        {
        //            string strSQL_Update = @"update t_aid_table_sequence_id
        //   set max_sequence_id_chr = to_number(max_sequence_id_chr) + " + p_intRowNum + @"
        // where table_name_vchr = 't_opr_lis_result'
        //   and col_name_vchr = 'idx_int'";

        //            string strSQL_Get = @"select max_sequence_id_chr
        //  from t_aid_table_sequence_id
        // where lower(trim(table_name_vchr)) = 't_opr_lis_result'
        //   and lower(trim(col_name_vchr)) = 'idx_int'";

        //            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = null;
        //            long lngRes = 0;
        //            try
        //            {
        //                objHRPSvc = new clsHRPTableService();
        //                lngRes = objHRPSvc.DoExcute(strSQL_Update);
        //                if (lngRes == 1)
        //                {
        //                    DataTable dtbResult = null;
        //                    lngRes = 0;
        //                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL_Get, ref dtbResult);
        //                    objHRPSvc.Dispose();
        //                    if (lngRes == 1 && dtbResult != null && dtbResult.Rows.Count > 0)
        //                    {
        //                        string strMaxID = dtbResult.Rows[0]["max_sequence_id_chr"].ToString().Trim();
        //                        if (Microsoft.VisualBasic.Information.IsNumeric(strMaxID))
        //                        {
        //                            if (p_blnNext)
        //                            {
        //                                return (int.Parse(strMaxID) - p_intRowNum + 1);
        //                            }
        //                            else
        //                            {
        //                                return (int.Parse(strMaxID) - p_intRowNum);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            finally
        //            {
        //                objHRPSvc = null;
        //            }
        //            throw new Exception("Can not generate new MaxID.");
        //            //			return -1;
        //        } 
        #endregion

        #region 更新检验编号，同时保存T_OPR_LIS_DEVICE_RELATION表
        /// <summary>
        /// 更新检验编号，同时保存T_OPR_LIS_DEVICE_RELATION表
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strDeviceNO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateAppCheckNO(string p_strBarCode, string p_strDeviceID, string p_strDeviceNO, string p_strDeviceSampleID)
        {
            long lngRes = 0;

            if (string.IsNullOrEmpty(p_strBarCode) || string.IsNullOrEmpty(p_strDeviceID) || string.IsNullOrEmpty(p_strDeviceNO) || string.IsNullOrEmpty(p_strDeviceSampleID))
                return lngRes;

            clsHRPTableService objHRPServ = null;

            try
            {
                string strSQL = @"select a.sample_id_chr,a.sample_id_chr, a.application_id_chr
  from t_opr_lis_sample a
 where a.status_int > 0
   and a.barcode_vchr = ?";
                objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strBarCode;

                DataTable dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);
                if (lngRes > 0 && dtResult == null && dtResult.Rows.Count > 0)
                {
                    string strApplicationID = dtResult.Rows[0]["application_id_chr"].ToString().Trim();
                    string strSampleID = dtResult.Rows[0]["sample_id_chr"].ToString().Trim();

                    strSQL = @"update t_opr_lis_application a
   set a.application_form_no_chr = ?
 where a.pstatus_int = 2
   and a.application_id_chr = ?";

                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = p_strDeviceNO + p_strDeviceSampleID;
                    objDPArr[1].Value = strApplicationID;

                    long lngAffect = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffect, objDPArr);
                    if (lngRes <= 0)
                    {
                        ContextUtil.SetAbort();
                        return lngRes;
                    }

                    strSQL = @"update t_opr_lis_device_relation
   set status_int = 0
 where status_int > 0
   and sample_id_chr = ?";

                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = strSampleID;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffect, objDPArr);
                    if (lngRes <= 0)
                    {
                        ContextUtil.SetAbort();
                        return lngRes;
                    }

                    int iDeviceSeqID = 0;
                    lngRes = clsPublicSvc.m_lngGetSequence("seq_lis_device_relation", out iDeviceSeqID);
                    if (lngRes <= 0)
                    {
                        ContextUtil.SetAbort();
                        return lngRes;
                    }
                    string strDeviceSeqID = iDeviceSeqID.ToString();
                    strSQL = @"insert into t_opr_lis_device_relation
  (deviceid_chr,
   seq_id_chr,
   reception_dat,
   device_sampleid_chr,
   check_dat,
   sample_id_chr,
   positionid_chr,
   status_int,
   seq_id_device_chr,
   bind_method_int,
   import_req_int)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    objHRPServ.CreateDatabaseParameter(11, out objDPArr);
                    DateTime dtCurrentDate = DateTime.Now;
                    objDPArr[0].Value = p_strDeviceID;
                    objDPArr[1].Value = strDeviceSeqID;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = dtCurrentDate;
                    objDPArr[3].Value = p_strDeviceSampleID;
                    objDPArr[4].DbType = DbType.DateTime;
                    objDPArr[4].Value = dtCurrentDate;
                    objDPArr[5].Value = strSampleID;
                    objDPArr[6].Value = "";
                    objDPArr[7].Value = 1;
                    objDPArr[8].Value = strDeviceSeqID;
                    objDPArr[9].Value = "";
                    objDPArr[10].Value = "";

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
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                objHRPServ = null;
                p_strBarCode = null;
                p_strBarCode = null;
                p_strDeviceNO = null;
                p_strDeviceSampleID = null;
            }
            return lngRes;
        }
        #endregion

        #region 更新检验编号，同时保存T_OPR_LIS_DEVICE_RELATION表，用于双工自动绑定检验编号 yongchao.li 2012-03-20
        /// <summary>
        /// 更新检验编号，同时保存T_OPR_LIS_DEVICE_RELATION表,用于双工自动绑定检验编号
        /// </summary>
        /// <param name="p_strBarCode"></param>
        /// <param name="p_strDeviceID"></param>
        /// <param name="p_strDeviceNO"></param>
        /// <param name="p_strDeviceSampleID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateAppCheckSampleNO(string p_strBarCode, string p_strDeviceID, string p_strDeviceNO, string p_strDeviceSampleID)
        {
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strBarCode) || string.IsNullOrEmpty(p_strDeviceID) || string.IsNullOrEmpty(p_strDeviceNO) || string.IsNullOrEmpty(p_strDeviceSampleID))
                return lngRes;

            clsHRPTableService objHRPServ = null;
            try
            {


                string strSQL = @"select a.sample_id_chr, a.application_id_chr
  from t_opr_lis_sample a
 where a.barcode_vchr = ?
   and a.status_int > 0";

                objHRPServ = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strBarCode;

                DataTable dtResult = null;
                lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dtResult, objDPArr);

                if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                {
                    string strApplicationID = dtResult.Rows[0]["application_id_chr"].ToString().Trim();
                    string strSampleID = dtResult.Rows[0]["sample_id_chr"].ToString().Trim();
                    strSQL = @"select a.application_form_no_chr, a.application_id_chr
  from t_opr_lis_application a
 where a.application_id_chr = ?
   and a.pstatus_int = 2";
                    DataTable dt = null;
                    objHRPServ.CreateDatabaseParameter(1, out objDPArr);
                    objDPArr[0].Value = strApplicationID;
                    lngRes = objHRPServ.lngGetDataTableWithParameters(strSQL, ref dt, objDPArr);
                    string strCheckNO = null;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        strCheckNO = dt.Rows[0]["application_form_no_chr"].ToString().Trim();
                        if (!string.IsNullOrEmpty(strCheckNO))
                        {
                            string[] strDataArr = strCheckNO.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                            if (strDataArr != null && strDataArr.Length > 0)
                            {
                                foreach (string strTemp in strDataArr)
                                {
                                    if (strTemp.StartsWith(p_strDeviceNO))
                                    {
                                        strCheckNO = strCheckNO.Replace(strTemp, p_strDeviceNO + p_strDeviceSampleID);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                strCheckNO = p_strDeviceNO + p_strDeviceSampleID;
                            }
                        }
                        else
                        {
                            strCheckNO = p_strDeviceNO + p_strDeviceSampleID;
                        }
                    }
                    else
                    {
                        strCheckNO = p_strDeviceNO + p_strDeviceSampleID;
                    }

                    strSQL = @"update t_opr_lis_application a
   set a.application_form_no_chr = ?
 where a.pstatus_int = 2
   and a.application_id_chr = ?";

                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = strCheckNO;
                    objDPArr[1].Value = strApplicationID;

                    long lngAffect = 0;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffect, objDPArr);
                    if (lngRes <= 0)
                    {
                        ContextUtil.SetAbort();
                        return lngRes;
                    }

                    strSQL = @"update t_opr_lis_device_relation a
   set status_int = 0
 where status_int > 0
   and sample_id_chr = ?
   and a.deviceid_chr = ?";

                    objHRPServ.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = strSampleID;
                    objDPArr[1].Value = p_strDeviceID;
                    lngRes = objHRPServ.lngExecuteParameterSQL(strSQL, ref lngAffect, objDPArr);
                    if (lngRes <= 0)
                    {
                        ContextUtil.SetAbort();
                        return lngRes;
                    }

                    int iDeviceSeqID = 0;
                    lngRes = clsPublicSvc.m_lngGetSequence("seq_lis_device_relation", out iDeviceSeqID);
                    if (lngRes <= 0)
                    {
                        ContextUtil.SetAbort();
                        return lngRes;
                    }
                    long lngSeq = 0;
                    lngRes = clsPublicSvc.m_lngGetSequence("seq_lis_device_relation", out lngSeq);
                    if (lngSeq <= 0)
                    {
                        ContextUtil.SetAbort();
                        return lngRes;
                    }

                    string strSeq = lngSeq.ToString().PadLeft(10, '0');
                    string strDeviceSeqID = iDeviceSeqID.ToString().PadLeft(10, '0');
                    strSQL = @"insert into t_opr_lis_device_relation
  (deviceid_chr,
   seq_id_chr,
   reception_dat,
   device_sampleid_chr,
   check_dat,
   sample_id_chr,
   positionid_chr,
   status_int,
   seq_id_device_chr,
   bind_method_int,
   import_req_int)
values
  (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    objHRPServ.CreateDatabaseParameter(11, out objDPArr);
                    DateTime dtCurrentDate = DateTime.Now;
                    objDPArr[0].Value = p_strDeviceID;
                    objDPArr[1].Value = strSeq;
                    objDPArr[2].DbType = DbType.DateTime;
                    objDPArr[2].Value = dtCurrentDate;
                    objDPArr[3].Value = p_strDeviceSampleID;
                    objDPArr[4].DbType = DbType.DateTime;
                    objDPArr[4].Value = dtCurrentDate;
                    objDPArr[5].Value = strSampleID;
                    objDPArr[6].Value = "";
                    objDPArr[7].Value = 1;
                    objDPArr[8].Value = strDeviceSeqID;
                    objDPArr[9].Value = -1;
                    objDPArr[10].Value = -1;

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
                clsLogText objLogger = new clsLogText();
                objLogger.LogDetailError(objEx, true);
            }
            finally
            {
                objHRPServ = null;
                p_strBarCode = null;
                p_strBarCode = null;
                p_strDeviceNO = null;
                p_strDeviceSampleID = null;
            }
            return lngRes;
        }
        #endregion

        #region 向LIS数据库中的 t_atb_ResultExe 表插入数据
        /// <summary>
        /// 插入ATB结果t_atb_ResultExe报表
        /// </summary>
        /// <param name="p_intFlag">插入标志</param>
        /// <param name="p_dt">插入数据表信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lnginsertResultExe(int p_intFlag, DataTable p_dt)
        {
            long lngRes = 0;

            #region 写表

            string Sqltxt = @" insert into  t_atb_ResultExe(
                        ReqNo,ExeNo,Veracity,MicPer,MicNum,
                        ExeDate,IfMic,IfPaper,ExeWay,ExeWayName,
                        GermID,MicEName,MicName,MicExplain,ExeMome,
                        Express,CardName,CardNo   
                        )  
                        values(
                        ?,?,?,?,?,
                        ?,?,?,?,?,
                        ?,?,?,?,?,
                        ?,?,?   
                        )";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            DbType[] m_dbType = new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.String,
                            DbType.String, DbType.DateTime, DbType.Int32, DbType.Int32,
                            DbType.Int32, DbType.String, DbType.String, DbType.String,
                            DbType.String, DbType.String,DbType.String,DbType.String,
                            DbType.String,DbType.String
                                };
            int countrow = 0;
            countrow = p_dt.Rows.Count;

            object[][] objValues = new object[18][];
            for (int i = 0; i < 18; i++)
            {
                objValues[i] = new object[countrow];
            }
            for (int iRow = 0; iRow < countrow; iRow++)
            {
                objValues[0][iRow] = Convert.ToInt32(p_dt.Rows[iRow]["ReqNo"]);
                objValues[1][iRow] = p_dt.Rows[iRow]["ExeNo"].ToString();
                objValues[2][iRow] = p_dt.Rows[iRow]["Veracity"].ToString();
                objValues[3][iRow] = p_dt.Rows[iRow]["MicPer"].ToString();
                objValues[4][iRow] = p_dt.Rows[iRow]["MicNum"].ToString();
                objValues[5][iRow] = Convert.ToDateTime(p_dt.Rows[iRow]["ExeDate"].ToString());
                objValues[6][iRow] = Convert.ToInt32(p_dt.Rows[iRow]["IfMic"].ToString());
                objValues[7][iRow] = Convert.ToInt32(p_dt.Rows[iRow]["IfPaper"].ToString());
                objValues[8][iRow] = Convert.ToInt32(p_dt.Rows[iRow]["ExeWay"].ToString());
                objValues[9][iRow] = p_dt.Rows[iRow]["ExeWayName"].ToString();
                objValues[10][iRow] = p_dt.Rows[iRow]["GermID"].ToString();
                objValues[11][iRow] = p_dt.Rows[iRow]["MicEName"].ToString();
                objValues[12][iRow] = p_dt.Rows[iRow]["MicName"].ToString();
                objValues[13][iRow] = p_dt.Rows[iRow]["MicExplain"].ToString();
                objValues[14][iRow] = p_dt.Rows[iRow]["ExeMome"].ToString();
                objValues[15][iRow] = p_dt.Rows[iRow]["Express"].ToString();
                objValues[16][iRow] = p_dt.Rows[iRow]["CardName"].ToString();
                objValues[17][iRow] = p_dt.Rows[iRow]["CardNo"].ToString();
            }

            lngRes = objHRPSvc.m_lngSaveArrayWithParameters(Sqltxt, objValues, m_dbType);

            return lngRes;
            //object[][] tempobjValues = new object[18][];
            //for (int i = 0; i < 18; i++)
            //{
            //    tempobjValues[i] = new object[6];
            //}
            //for (int j = 0; j < countrow; j++)
            //{
            //    if (countrow-j - 6 > 0)
            //    {
            //        for (int iRow = 0; iRow < 6; iRow++)
            //        {
            //            tempobjValues[0][iRow] = objValues[0][j];
            //            tempobjValues[1][iRow] = objValues[1][j];
            //            tempobjValues[2][iRow] = objValues[2][j];
            //            tempobjValues[3][iRow] = objValues[3][j];
            //            tempobjValues[4][iRow] = objValues[4][j];
            //            tempobjValues[5][iRow] = objValues[5][j];
            //            tempobjValues[6][iRow] = objValues[6][j];
            //            tempobjValues[7][iRow] = objValues[7][j];
            //            tempobjValues[8][iRow] = objValues[8][j];
            //            tempobjValues[9][iRow] = objValues[9][j];
            //            tempobjValues[10][iRow] = objValues[10][j];
            //            tempobjValues[11][iRow] = objValues[11][j];
            //            tempobjValues[12][iRow] = objValues[12][j];
            //            tempobjValues[13][iRow] = objValues[13][j];
            //            tempobjValues[14][iRow] = objValues[14][j];
            //            tempobjValues[15][iRow] = objValues[15][j];
            //            tempobjValues[16][iRow] = objValues[16][j];
            //            tempobjValues[17][iRow] = objValues[17][j];
            //            j++;
            //        }
            //        lngRes = objHRPSvc.m_lngSaveArrayWithParameters(Sqltxt, tempobjValues, m_dbType);
            //    }
            //    else 
            //{
            //    object[][] temp_objValues = new object[18][];
            //    for (int i = 0; i < 18; i++)
            //    {
            //        temp_objValues[i] = new object[countrow - j];
            //    }
            //    int temprows = countrow - j;
            //    for (int iRow = 0; iRow < temprows; iRow++)
            //    {
            //        temp_objValues[0][iRow] = objValues[0][j];
            //        temp_objValues[1][iRow] = objValues[1][j];
            //        temp_objValues[2][iRow] = objValues[2][j];
            //        temp_objValues[3][iRow] = objValues[3][j];
            //        temp_objValues[4][iRow] = objValues[4][j];
            //        temp_objValues[5][iRow] = objValues[5][j];
            //        temp_objValues[6][iRow] = objValues[6][j];
            //        temp_objValues[7][iRow] = objValues[7][j];
            //        temp_objValues[8][iRow] = objValues[8][j];
            //        temp_objValues[9][iRow] = objValues[9][j];
            //        temp_objValues[10][iRow] = objValues[10][j];
            //        temp_objValues[11][iRow] = objValues[11][j];
            //        temp_objValues[12][iRow] = objValues[12][j];
            //        temp_objValues[13][iRow] = objValues[13][j];
            //        temp_objValues[14][iRow] = objValues[14][j];
            //        temp_objValues[15][iRow] = objValues[15][j];
            //        temp_objValues[16][iRow] = objValues[16][j];
            //        temp_objValues[17][iRow] = objValues[17][j];
            //        j++;
            //    }
            //    lngRes = objHRPSvc.m_lngSaveArrayWithParameters(Sqltxt, temp_objValues, m_dbType);
            //}

            //}
            #endregion


        }
        #endregion

        #region 向LIS数据库中的 t_atb_ResultMic 表插入数据
        /// <summary>
        /// 插入ATB结果t_atb_ResultMic报表
        /// </summary>
        /// <param name="p_intFlag">插入标志</param>
        /// <param name="p_dt">插入数据表信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lnginsertResultMic(int p_intFlag, DataTable p_dt)
        {
            long lngRes = 0;

            #region 写表

            string Sqltxt = @" insert into  t_atb_ResultMic(
                        ReqNo,ExeNo,GermID,AntiID ,AntiDate,
                        AntiEName,AntiName,TestNo,Test,MicRNo,
                        MicResult,SusDesc,ResShow,MicExplain,MicMome,
                        RangeNo,AntiUnit,FloorValue,CeilingValue,Range,
                        DispOrder,IsEChoice,EQuan ,ExcepAnnu,YNTrue,
                        IShow,RShow,SShow,TOMIC,Dosage,
                        Surem,Urine )  
                        values(
                        ?,?,?,?,?,
                        ?,?,?,?,?,
                        ?,?,?,?,?,
                        ?,?,?,?,?,
                        ?,?,?,?,?,
                        ?,?,?,?,?,
                        ?,?   
                        )";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            DbType[] m_dbType = new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.String,
                            DbType.DateTime, DbType.String, DbType.String, DbType.String,
                            DbType.String, DbType.Int32, DbType.Decimal, DbType.String,
                            DbType.String, DbType.String,DbType.String,DbType.Int32,
                            DbType.String,DbType.Double,DbType.String,DbType.String,
                            DbType.Int32,DbType.Int32,DbType.Int32,DbType.String,
                            DbType.Int32,DbType.String,DbType.String,DbType.String,
                            DbType.String,DbType.String,DbType.String,DbType.String
                                };
            int countrow = 0;
            countrow = p_dt.Rows.Count;

            object[][] objValues = new object[32][];
            for (int i = 0; i < 32; i++)
            {
                objValues[i] = new object[countrow];
            }
            for (int iRow = 0; iRow < countrow; iRow++)
            {
                objValues[0][iRow] = Convert.ToInt32(p_dt.Rows[iRow]["ReqNo"]);
                objValues[1][iRow] = p_dt.Rows[iRow]["ExeNo"].ToString();
                objValues[2][iRow] = p_dt.Rows[iRow]["GermID"].ToString();
                objValues[3][iRow] = p_dt.Rows[iRow]["AntiID"].ToString();
                objValues[4][iRow] = Convert.ToDateTime(p_dt.Rows[iRow]["AntiDate"]);
                objValues[5][iRow] = p_dt.Rows[iRow]["AntiEName"].ToString();
                objValues[6][iRow] = p_dt.Rows[iRow]["AntiName"].ToString();
                objValues[7][iRow] = p_dt.Rows[iRow]["TestNo"].ToString();
                objValues[8][iRow] = p_dt.Rows[iRow]["Test"].ToString();
                objValues[9][iRow] = Convert.ToInt32(p_dt.Rows[iRow]["MicRNo"].ToString());
                if (p_dt.Rows[iRow]["MicResult"].ToString() == "")
                {
                    objValues[10][iRow] = 0;
                }
                else
                {
                    objValues[10][iRow] = Convert.ToDecimal(p_dt.Rows[iRow]["MicResult"].ToString());
                }

                objValues[11][iRow] = p_dt.Rows[iRow]["SusDesc"].ToString();
                objValues[12][iRow] = p_dt.Rows[iRow]["ResShow"].ToString();
                objValues[13][iRow] = p_dt.Rows[iRow]["MicExplain"].ToString();
                objValues[14][iRow] = p_dt.Rows[iRow]["MicMome"].ToString();
                objValues[15][iRow] = Convert.ToInt32(p_dt.Rows[iRow]["RangeNo"].ToString());
                objValues[16][iRow] = p_dt.Rows[iRow]["AntiUnit"].ToString();
                objValues[17][iRow] = Convert.ToDouble(p_dt.Rows[iRow]["FloorValue"].ToString());
                objValues[18][iRow] = p_dt.Rows[iRow]["CeilingValue"].ToString();
                objValues[19][iRow] = p_dt.Rows[iRow]["Range"].ToString();
                objValues[20][iRow] = Convert.ToInt32(p_dt.Rows[iRow]["DispOrder"].ToString());
                objValues[21][iRow] = Convert.ToInt32(p_dt.Rows[iRow]["IsEChoice"].ToString());
                objValues[22][iRow] = Convert.ToInt32(p_dt.Rows[iRow]["EQuan"].ToString());
                objValues[23][iRow] = p_dt.Rows[iRow]["ExcepAnnu"].ToString();
                objValues[24][iRow] = Convert.ToInt32(p_dt.Rows[iRow]["YNTrue"].ToString());
                objValues[25][iRow] = p_dt.Rows[iRow]["IShow"].ToString();
                objValues[26][iRow] = p_dt.Rows[iRow]["RShow"].ToString();
                objValues[27][iRow] = p_dt.Rows[iRow]["SShow"].ToString();
                objValues[28][iRow] = p_dt.Rows[iRow]["TOMIC"].ToString();
                objValues[29][iRow] = p_dt.Rows[iRow]["Dosage"].ToString();
                objValues[30][iRow] = p_dt.Rows[iRow]["Surem"].ToString();
                objValues[31][iRow] = p_dt.Rows[iRow]["Urine"].ToString();

            }
            lngRes = 0;

            lngRes = objHRPSvc.m_lngSaveArrayWithParameters(Sqltxt, objValues, m_dbType);

            #endregion

            return lngRes;
        }
        #endregion

        #region 向LIS数据库中的 t_atb_AntiResultBill 表插入数据
        /// <summary>
        /// 插入ATB结果t_atb_AntiResultBill报表
        /// </summary>
        /// <param name="p_intFlag">插入标志</param>
        /// <param name="p_dt">插入数据表信息</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lnginsertResultbill(int p_intFlag, DataTable p_dt)
        {
            long lngRes = 0;

            #region 写表

            string Sqltxt = @" insert into  t_atb_AntiResultBill(
                        ReqNo,SamNo,PatNo )  
                        values(
                        ?,?,?
                        )";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            DbType[] m_dbType = new DbType[] { DbType.Int32, DbType.String, DbType.String };
            int countrow = 0;
            countrow = p_dt.Rows.Count;

            object[][] objValues = new object[3][];
            for (int i = 0; i < 3; i++)
            {
                objValues[i] = new object[countrow];
            }
            for (int iRow = 0; iRow < countrow; iRow++)
            {
                objValues[0][iRow] = Convert.ToInt32(p_dt.Rows[iRow]["ReqNo"]);
                objValues[1][iRow] = p_dt.Rows[iRow]["SamNo"].ToString();
                objValues[2][iRow] = p_dt.Rows[iRow]["PatNo"].ToString();
            }
            lngRes = 0;

            lngRes = objHRPSvc.m_lngSaveArrayWithParameters(Sqltxt, objValues, m_dbType);

            #endregion

            return lngRes;
        }
        #endregion

    }
}
