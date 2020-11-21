using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Text;
using weCare.Core.Entity;
using weCare.Core.Dac;

namespace Lis.Service
{
    /// <summary>
    /// BizLis
    /// </summary>
    public class BizLis : IDisposable
    {
        #region var/property

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

        #endregion

        #region 获取多个序列号
        /// <summary>
        /// 获取多个序列号
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="seqName">序列名</param>
        /// <param name="number">数量</param>
        /// <param name="p_lngSeqIdArr">序列号</param>
        /// <returns></returns>
        public long GetSequenceArr(string seqName, int number, out int[] seqIdArr)
        {
            seqIdArr = null;
            long lngRes = 0;
            if (number <= 0 || string.IsNullOrEmpty(seqName))
            {
                return lngRes;
            }
            try
            {
                DataTable dt = null;
                seqIdArr = new int[number];
                string Sql = string.Format("select {0}.nextval from dual", seqName);
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                for (int i = number - 1; i >= 0; i--)
                {
                    dt = svc.GetDataTable(Sql);
                    seqIdArr[i] = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
                return 1;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return lngRes;
        }
        #endregion

        #region IsAppendResult
        /// <summary>
        /// IsAppendResult
        /// </summary>
        /// <param name="has"></param>
        /// <param name="p_objResultArr"></param>
        /// <param name="strConditionList"></param>
        /// <returns></returns> 
        public bool IsAppendResult(ref Dictionary<string, string> has, clsLIS_Device_Test_ResultVO[] p_objResultArr, out string[] strConditionList)
        {
            long lngRes = 1;
            string strSQL = null;
            strConditionList = null;
            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);

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
                objParamArr = svc.CreateParm(4);
                objParamArr[0].DbType = DbType.DateTime;
                objParamArr[0].Value = Convert.ToDateTime(m_strDateBegin);
                objParamArr[1].DbType = DbType.DateTime;
                objParamArr[1].Value = Convert.ToDateTime(m_strDateEnd);
                objParamArr[2].Value = m_strDevID;
                objParamArr[3].Value = m_strSID;
                dtbTmp = svc.GetDataTable(strSQL, objParamArr);
                if (dtbTmp != null && dtbTmp.Rows.Count > 0)
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
                    objParamArr = svc.CreateParm(3);
                    objParamArr[0].DbType = DbType.DateTime;
                    objParamArr[0].Value = Convert.ToDateTime(m_strCheckDate_req);
                    objParamArr[1].Value = m_intImport_Req;
                    objParamArr[2].Value = m_strDevID;
                    dtbTmp = svc.GetDataTable(strSQL, objParamArr);
                    if (dtbTmp != null && dtbTmp.Rows.Count > 0)
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
                        objParamArr = svc.CreateParm(5);
                        objParamArr[0].Value = m_strDevID;
                        objParamArr[1].Value = m_strSID;
                        objParamArr[2].DbType = DbType.DateTime;
                        objParamArr[2].Value = Convert.ToDateTime(m_strCheckDate_req);
                        objParamArr[3].Value = intIdxBegin;
                        objParamArr[4].Value = intIdxEnd;
                        dtbTmp = svc.GetDataTable(strSQL, objParamArr);
                        svc.Dispose();
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
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogDetailError(objEx, true);
            }
            return blnFlag;

            #endregion
        }
        #endregion

        #region AddLabResult
        /// <summary>
        /// AddLabResult
        /// </summary>
        /// <param name="arlResult"></param>
        /// <param name="p_arlResultOut"></param>
        /// <returns></returns>
        public long AddLabResult(List<clsLIS_Device_Test_ResultVO> arlResult, out List<clsLIS_Device_Test_ResultVO> p_arlResultOut)
        {
            p_arlResultOut = null;
            clsLIS_Device_Test_ResultVO[] objResultArr = arlResult.ToArray();
            clsLIS_Device_Test_ResultVO[] objResultOutArr = null;

            long lngRes = AddLabResult(objResultArr, out objResultOutArr);
            if (lngRes > 0)
            {
                p_arlResultOut = new List<clsLIS_Device_Test_ResultVO>();
                p_arlResultOut.AddRange(objResultOutArr);
            }
            return lngRes;
        }
        #endregion

        #region AddLabResult
        /// <summary>
        /// AddLabResult
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_objOutResultArr"></param>
        /// <returns></returns>
        public long AddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr)
        {
            long lngRes = 0;
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
            foreach (KeyValuePair<string, List<clsLIS_Device_Test_ResultVO>> objTemp in m_dictSample)
            {
                p_objResultArr = objTemp.Value.ToArray();
                p_objOutResultArr = null;
                Dictionary<string, string> has = new Dictionary<string, string>();
                if (p_objResultArr == null || p_objResultArr.Length <= 0)
                {
                    return -1;
                }
                else
                {
                    foreach (clsLIS_Device_Test_ResultVO objRes in p_objResultArr)
                    {
                        if (has.ContainsKey(objRes.strDevice_Check_Item_Name) == false)
                        {
                            has.Add(objRes.strDevice_Check_Item_Name, objRes.strResult);
                        }
                    }
                }
                string strSQL = null;

                #region 判断是追加结果还是新增一次结果 用于Stago、CentaurCP等仪器
                /* 原理：获取上次取得该样本结果的最大序列
               根据序列，仪器和标本号查找log表得起始值
               找到结果进行结果校对哈希比较，如果不一致立即跳过进行原操作
               否则追加插入结果，修改req日期.log起始值和日期 */
                //<--------------------------------------------------------  
                string[] strConditionList = null; //当blnFlag为false时有值
                bool blnFlag = this.IsAppendResult(ref has, p_objResultArr, out strConditionList);
                #endregion

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                List<DacParm> lstParm = new List<DacParm>();
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
                    string strDid = objResultList[0].strDevice_ID.Trim();
                    string strSid = objResultList[0].strDevice_Sample_ID.Trim();
                    string barCode = objResultList[0].barCode;
                    clsLogText objLogger = new clsLogText();

                    int intImportReq = -1;
                    if (blnFlag)
                    {

                        #region 得到仪器样本的采集序号
                        DataTable dtbReq = null;
                        strSQL = @"select max(import_req_int) + 1 as import_req_int
                                              from t_opr_lis_result_import_req
                                             where deviceid_chr = ?
                                             group by deviceid_chr";

                        System.Data.IDataParameter[] objDPArr4 = null;
                        objDPArr4 = svc.CreateParm(1);
                        objDPArr4[0].Value = objResultList[0].strDevice_ID;

                        dtbReq = svc.GetDataTable(strSQL, objDPArr4);
                        if (dtbReq != null && dtbReq.Rows.Count != 0)
                        {
                            intImportReq = int.Parse(dtbReq.Rows[0]["import_req_int"].ToString());
                        }
                        else
                        {
                            intImportReq = 1;
                        }
                        #endregion

                        #region 写 t_opr_lis_result_import_req 表
                        System.Data.IDataParameter[] objDPArr5 = svc.CreateParm(7);
                        objDPArr5[0].Value = strDid;
                        objDPArr5[1].Value = intImportReq;
                        objDPArr5[2].Value = strSid;
                        objDPArr5[3].DbType = DbType.DateTime;
                        objDPArr5[3].Value = Convert.ToDateTime(strCheckDateTime);
                        objDPArr5[4].Value = 1;
                        objDPArr5[5].Value = 0;
                        objDPArr5[6].Value = "0";

                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, c_strAddResultImportReq, objDPArr5));
                        //lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddResultImportReq, ref lngRecEff, objDPArr5);
                        #endregion

                        //int intIdx = m_mthGetNewResultIndex(objResultList.Length, false) + 1;

                        int[] intIdxArr = null;
                        // 2019-11-22: 由于9i/11g连接11g server异常，暂时用 seq_lis_result_11g 替代 seq_lis_result --> cancel 19/11/29
                        GetSequenceArr("seq_lis_result", objResultList.Length, out intIdxArr);

                        if (intIdxArr.Length <= 0)
                        {
                            return -1;
                        }
                        int maxIdx = 0;
                        int minIdx = 0;
                        #region 写 t_opr_lis_result 表
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
                        //lngRes = objHRPSvc.m_lngSaveArrayWithParameters(c_strAddLabResult, objValues, m_dbType);
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSqlForBatchSimpleInsert, c_strAddLabResult, objValues, m_dbType));
                        #endregion

                        //if (lngRes == 1)
                        //{
                        #region 写 t_opr_lis_result_log 表

                        System.Data.IDataParameter[] objDPArr1 = svc.CreateParm(7);
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

                        //lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddLabResultLog, ref lngRecEff, objDPArr1);
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, c_strAddLabResultLog, objDPArr1));

                        #endregion
                        //}

                    }
                    else //追加结果AppendResult
                    {
                        //objLogger.Log2File("D:\\logData.txt", "结果追加");

                        #region 写 t_opr_lis_result 表
                        strCheckDateTime = strConditionList[3].Trim();
                        dtmCheck = Convert.ToDateTime(strCheckDateTime);
                        intImportReq = Convert.ToInt32(strConditionList[2]);
                        //int intIdx = m_mthGetNewResultIndex(objResultList.Length, false) + 1;
                        int[] intIdxArr = null;
                        // 2019-11-22: 由于9i/11g连接11g server异常，暂时用 seq_lis_result_11g 替代 seq_lis_result, 19/11/29 cancel
                        GetSequenceArr("seq_lis_result", objResultList.Length, out intIdxArr);

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
                        //lngRes = objHRPSvc.m_lngSaveArrayWithParameters(c_strAddLabResult, objValues1, m_dbType1);
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSqlForBatchSimpleInsert, c_strAddLabResult, objValues1, m_dbType1));
                        #endregion

                        //if (lngRes == 1)
                        //{
                        #region 更新 t_opr_lis_result_log 表

                        System.Data.IDataParameter[] objDPArr1 = null;
                        if (strConditionList[1] == "000041" || strConditionList[1] == "000047")
                        {
                            strSQL = @"update t_opr_lis_result_log
                                               set end_idx_int = ?
                                             where deviceid_chr = ? 
                                               and check_dat = ?
                                               and import_req_int = ?";

                            objDPArr1 = svc.CreateParm(4);
                            objDPArr1[0].Value = maxIdx2;
                            objDPArr1[1].Value = strConditionList[1]; //仪器ID 
                            objDPArr1[2].DbType = DbType.DateTime;
                            objDPArr1[2].Value = Convert.ToDateTime(strConditionList[3]); //检验日期
                            objDPArr1[3].Value = Convert.ToInt32(strConditionList[2]); //系统内部结果序列

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

                            objDPArr1 = svc.CreateParm(5);
                            objDPArr1[0].Value = maxIdx2;
                            objDPArr1[1].Value = strConditionList[1]; //仪器ID
                            objDPArr1[2].Value = strConditionList[0]; //仪器样本ID
                            objDPArr1[3].DbType = DbType.DateTime;
                            objDPArr1[3].Value = Convert.ToDateTime(strConditionList[3]); //检验日期
                            objDPArr1[4].Value = Convert.ToInt32(strConditionList[2]); //系统内部结果序列
                        }
                        //lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDPArr1);
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, strSQL, objDPArr1));

                        #endregion
                        //}
                    }
                    System.Data.DataTable dtbRelation = null;
                    //if (lngRes == 1)//无论此步骤成功与否都不应影响原始数据的进入.
                    //{
                    #region  查找核收表（t_opr_lis_device_relation）表中要做关联的记录

                    strSQL = @"select deviceid_chr, seq_id_device_chr, sample_id_chr
                                      from t_opr_lis_device_relation
                                     where status_int = 1
                                       and deviceid_chr = ?
                                       and trim(device_sampleid_chr) = ?
                                       and check_dat between ? and ?";

                    System.Data.IDataParameter[] objDPArrs3 = svc.CreateParm(4);
                    objDPArrs3[0].Value = strDid;
                    objDPArrs3[1].Value = strSid;
                    objDPArrs3[2].DbType = DbType.DateTime;
                    objDPArrs3[2].Value = (Convert.ToDateTime(strCheckDateTime)).Date;
                    objDPArrs3[3].DbType = DbType.DateTime;
                    objDPArrs3[3].Value = (Convert.ToDateTime(strCheckDateTime)).Date.AddHours(24);

                    //lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbRelation, objDPArrs3);
                    dtbRelation = svc.GetDataTable(strSQL, objDPArrs3);

                    #endregion

                    #region 更新 t_opr_lis_device_relation 表

                    if (dtbRelation != null && dtbRelation.Rows.Count > 0)
                    {
                        string strSeq = dtbRelation.Rows[0]["seq_id_device_chr"].ToString().Trim();

                        strSQL = @"update t_opr_lis_device_relation
                                               set device_sampleid_chr = ?,
                                                   check_dat           = ?,
                                                   import_req_int      = ?,
                                                   status_int          = 2
                                             where trim(seq_id_device_chr) = ?
                                               and trim(deviceid_chr) = ?";

                        System.Data.IDataParameter[] objDPArrs2 = svc.CreateParm(5);
                        objDPArrs2[0].Value = strSid;
                        objDPArrs2[1].DbType = DbType.DateTime;
                        objDPArrs2[1].Value = Convert.ToDateTime(strCheckDateTime);
                        objDPArrs2[2].Value = intImportReq;
                        objDPArrs2[3].Value = strSeq.Trim();
                        objDPArrs2[4].Value = strDid.Trim();

                        //lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDPArrs2);
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, strSQL, objDPArrs2));

                    }
                    #endregion
                    //}
                    if (lstParm.Count > 0)
                    {
                        lngRes = svc.Commit(lstParm);
                    }
                }
                catch (System.Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                finally
                {
                    svc.Dispose();
                }
            }
            p_objOutResultArr = p_objResultArr;
            return lngRes;
        }
        #endregion

        #region 增加检验仪器结果, 多样本
        /// <summary>
        /// 增加检验仪器结果, 多样本
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_blnMuiltySample"> TRUE = 多样本</param>
        /// <param name="p_objOutResultArr"></param>
        /// <returns></returns>
        public long AddLabResult(clsLIS_Device_Test_ResultVO[] p_objResultArr, bool p_blnMuiltySample, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr)
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
                        lngRes = AddLabResult(lstResult.ToArray(), out objResultTempArr);
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
                lngRes = AddLabResult(p_objResultArr, out p_objOutResultArr);
            }
            return lngRes;
        }
        #endregion

        #region 增加检验仪器结果 + 图片
        /// <summary>
        /// lngAddLabResultWithBytGraph
        /// </summary>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_objOutResultArr"></param>
        /// <returns></returns>
        public long lngAddLabResultWithBytGraph(clsLIS_Device_Test_ResultVO[] p_objResultArr,Byte[] bytGraph, out clsLIS_Device_Test_ResultVO[] p_objOutResultArr)
        {
            long lngRes = 0;
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
            foreach (KeyValuePair<string, List<clsLIS_Device_Test_ResultVO>> objTemp in m_dictSample)
            {
                p_objResultArr = objTemp.Value.ToArray();
                p_objOutResultArr = null;
                Dictionary<string, string> has = new Dictionary<string, string>();
                if (p_objResultArr == null || p_objResultArr.Length <= 0)
                {
                    return -1;
                }
                else
                {
                    foreach (clsLIS_Device_Test_ResultVO objRes in p_objResultArr)
                    {
                        if (has.ContainsKey(objRes.strDevice_Check_Item_Name) == false)
                        {
                            has.Add(objRes.strDevice_Check_Item_Name, objRes.strResult);
                        }
                    }
                }
                string strSQL = null;

                #region 判断是追加结果还是新增一次结果 用于Stago、CentaurCP等仪器
                /* 原理：获取上次取得该样本结果的最大序列
               根据序列，仪器和标本号查找log表得起始值
               找到结果进行结果校对哈希比较，如果不一致立即跳过进行原操作
               否则追加插入结果，修改req日期.log起始值和日期 */
                //<--------------------------------------------------------  
                string[] strConditionList = null; //当blnFlag为false时有值
                bool blnFlag = this.IsAppendResult(ref has, p_objResultArr, out strConditionList);
                #endregion

                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                List<DacParm> lstParm = new List<DacParm>();
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
                    string strDid = objResultList[0].strDevice_ID.Trim();
                    string strSid = objResultList[0].strDevice_Sample_ID.Trim();
                    string barcode = objResultList[0].barCode;
                    clsLogText objLogger = new clsLogText();

                    int intImportReq = -1;
                    if (blnFlag)
                    {

                        #region 得到仪器样本的采集序号
                        DataTable dtbReq = null;
                        strSQL = @"select max(import_req_int) + 1 as import_req_int
                                              from t_opr_lis_result_import_req
                                             where deviceid_chr = ?
                                             group by deviceid_chr";

                        System.Data.IDataParameter[] objDPArr4 = null;
                        objDPArr4 = svc.CreateParm(1);
                        objDPArr4[0].Value = objResultList[0].strDevice_ID;

                        dtbReq = svc.GetDataTable(strSQL, objDPArr4);
                        if (dtbReq != null && dtbReq.Rows.Count != 0)
                        {
                            intImportReq = int.Parse(dtbReq.Rows[0]["import_req_int"].ToString());
                        }
                        else
                        {
                            intImportReq = 1;
                        }
                        #endregion

                        #region 写 t_opr_lis_result_import_req 表
                        System.Data.IDataParameter[] objDPArr5 = svc.CreateParm(7);
                        objDPArr5[0].Value = strDid;
                        objDPArr5[1].Value = intImportReq;
                        objDPArr5[2].Value = strSid;
                        objDPArr5[3].DbType = DbType.DateTime;
                        objDPArr5[3].Value = Convert.ToDateTime(strCheckDateTime);
                        objDPArr5[4].Value = 1;
                        objDPArr5[5].Value = 0;
                        objDPArr5[6].Value = "0";

                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, c_strAddResultImportReq, objDPArr5));
                        //lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddResultImportReq, ref lngRecEff, objDPArr5);
                        #endregion

                        //int intIdx = m_mthGetNewResultIndex(objResultList.Length, false) + 1;

                        int[] intIdxArr = null;
                        // 2019-11-22: 由于9i/11g连接11g server异常，暂时用 seq_lis_result_11g 替代 seq_lis_result --> cancel 19/11/29
                        GetSequenceArr("seq_lis_result", objResultList.Length, out intIdxArr);

                        if (intIdxArr.Length <= 0)
                        {
                            return -1;
                        }
                        int maxIdx = 0;
                        int minIdx = 0;
                        #region 写 t_opr_lis_result 表
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
                        //lngRes = objHRPSvc.m_lngSaveArrayWithParameters(c_strAddLabResult, objValues, m_dbType);
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSqlForBatchSimpleInsert, c_strAddLabResult, objValues, m_dbType));
                        #endregion

                        //if (lngRes == 1)
                        //{
                        #region 写 t_opr_lis_result_log 表

                        System.Data.IDataParameter[] objDPArr1 = svc.CreateParm(7);
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

                        //lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddLabResultLog, ref lngRecEff, objDPArr1);
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, c_strAddLabResultLog, objDPArr1));

                        #endregion
                        //}

                    }
                    else //追加结果AppendResult
                    {
                        //objLogger.Log2File("D:\\logData.txt", "结果追加");

                        #region 写 t_opr_lis_result 表
                        strCheckDateTime = strConditionList[3].Trim();
                        dtmCheck = Convert.ToDateTime(strCheckDateTime);
                        intImportReq = Convert.ToInt32(strConditionList[2]);
                        //int intIdx = m_mthGetNewResultIndex(objResultList.Length, false) + 1;
                        int[] intIdxArr = null;
                        // 2019-11-22: 由于9i/11g连接11g server异常，暂时用 seq_lis_result_11g 替代 seq_lis_result, 19/11/29 cancel
                        GetSequenceArr("seq_lis_result", objResultList.Length, out intIdxArr);

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
                        //lngRes = objHRPSvc.m_lngSaveArrayWithParameters(c_strAddLabResult, objValues1, m_dbType1);
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSqlForBatchSimpleInsert, c_strAddLabResult, objValues1, m_dbType1));
                        #endregion

                        //if (lngRes == 1)
                        //{
                        #region 更新 t_opr_lis_result_log 表

                        System.Data.IDataParameter[] objDPArr1 = null;
                        if (strConditionList[1] == "000041" || strConditionList[1] == "000047")
                        {
                            strSQL = @"update t_opr_lis_result_log
                                               set end_idx_int = ?
                                             where deviceid_chr = ? 
                                               and check_dat = ?
                                               and import_req_int = ?";

                            objDPArr1 = svc.CreateParm(4);
                            objDPArr1[0].Value = maxIdx2;
                            objDPArr1[1].Value = strConditionList[1]; //仪器ID 
                            objDPArr1[2].DbType = DbType.DateTime;
                            objDPArr1[2].Value = Convert.ToDateTime(strConditionList[3]); //检验日期
                            objDPArr1[3].Value = Convert.ToInt32(strConditionList[2]); //系统内部结果序列

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

                            objDPArr1 = svc.CreateParm(5);
                            objDPArr1[0].Value = maxIdx2;
                            objDPArr1[1].Value = strConditionList[1]; //仪器ID
                            objDPArr1[2].Value = strConditionList[0]; //仪器样本ID
                            objDPArr1[3].DbType = DbType.DateTime;
                            objDPArr1[3].Value = Convert.ToDateTime(strConditionList[3]); //检验日期
                            objDPArr1[4].Value = Convert.ToInt32(strConditionList[2]); //系统内部结果序列
                        }
                        //lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDPArr1);
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, strSQL, objDPArr1));

                        #endregion
                        //}
                    }
                    System.Data.DataTable dtbRelation = null;
                    //if (lngRes == 1)//无论此步骤成功与否都不应影响原始数据的进入.
                    //{
                    #region  查找核收表（t_opr_lis_device_relation）表中要做关联的记录

                    strSQL = @"select deviceid_chr, seq_id_device_chr, sample_id_chr
                                      from t_opr_lis_device_relation
                                     where status_int = 1
                                       and deviceid_chr = ?
                                       and trim(device_sampleid_chr) = ?
                                       and check_dat between ? and ?";

                    System.Data.IDataParameter[] objDPArrs3 = svc.CreateParm(4);
                    objDPArrs3[0].Value = strDid;
                    objDPArrs3[1].Value = strSid;
                    objDPArrs3[2].DbType = DbType.DateTime;
                    objDPArrs3[2].Value = (Convert.ToDateTime(strCheckDateTime)).Date;
                    objDPArrs3[3].DbType = DbType.DateTime;
                    objDPArrs3[3].Value = (Convert.ToDateTime(strCheckDateTime)).Date.AddHours(24);

                    //lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbRelation, objDPArrs3);
                    dtbRelation = svc.GetDataTable(strSQL, objDPArrs3);

                    #endregion

                    #region 更新 t_opr_lis_device_relation 表

                    if (dtbRelation != null && dtbRelation.Rows.Count > 0)
                    {
                        string strSeq = dtbRelation.Rows[0]["seq_id_device_chr"].ToString().Trim();

                        strSQL = @"update t_opr_lis_device_relation
                                               set device_sampleid_chr = ?,
                                                   check_dat           = ?,
                                                   import_req_int      = ?,
                                                   status_int          = 2
                                             where trim(seq_id_device_chr) = ?
                                               and trim(deviceid_chr) = ?";

                        System.Data.IDataParameter[] objDPArrs2 = svc.CreateParm(5);
                        objDPArrs2[0].Value = strSid;
                        objDPArrs2[1].DbType = DbType.DateTime;
                        objDPArrs2[1].Value = Convert.ToDateTime(strCheckDateTime);
                        objDPArrs2[2].Value = intImportReq;
                        objDPArrs2[3].Value = strSeq.Trim();
                        objDPArrs2[4].Value = strDid.Trim();

                        //lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDPArrs2);
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, strSQL, objDPArrs2));

                    }
                    #endregion

                    if (bytGraph != null)
                    {
                        strSQL = @"insert into T_CHECKRESULT_IMG values(?,?,?,?,?,?)";
                        System.Data.IDataParameter[] parms = null;
                        parms = svc.CreateParm(6);
                        parms[0].Value = strDid;
                        parms[1].Value = strSid;
                        parms[2].Value = barcode;
                        parms[3].Value = bytGraph;
                        parms[4].Value = 0;
                        parms[5].Value = Convert.ToDateTime(strCheckDateTime);
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, strSQL, parms));
                    }

                    //}
                    if (lstParm.Count > 0)
                    {
                        lngRes = svc.Commit(lstParm);
                    }
                }
                catch (System.Exception objEx)
                {
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                finally
                {
                    svc.Dispose();
                }
            }
            p_objOutResultArr = p_objResultArr;
            return lngRes;
        }
        #endregion

        #region 打包-核收
        /// <summary>
        /// 打包-核收
        /// </summary>
        /// <param name="sampleVo"></param>
        /// <returns></returns>
        public bool SamplePackCheck(EntitySamplePack sampleVo)
        {
            long affectRows = 0;
            string Sql = string.Empty;
            string sqlConnStr = string.Empty;
            clsHRPTableService svcLis = null;
            SqlHelper svc = null;
            SqlHelper svcLis2 = null;
            try
            {
                DataRow drPat = null;
                DataTable dtItem = null;
                DataTable dtParm = null;
                DataTable dt = null;
                bool isJj = false;
                bool isJtj = false;
                DateTime? peSamplingDate = null;
                sampleVo.checkDate = DateTime.Now;
                List<DacParm> lstParm = new List<DacParm>();
                svcLis = new clsHRPTableService();
                svc = new SqlHelper(EnumBiz.onlineDB);
                IDataParameter[] parm = null;
                int n = -1;

                #region 住院样本核收

                if (sampleVo.typeId == 1)
                {
                    #region 获取患者基本资料
                    Sql = @"select nvl(a.diagnose_vchr, '') as diagnose,
                                   nvl(a.bedno_chr, '') as bedNo,
                                   nvl(a.patientcardid_chr, '') as cardNo,
                                   nvl(a.patient_inhospitalno_chr, '') as ipNo  
                              from t_opr_lis_sample a
                             where a.barcode_vchr = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = sampleVo.barCode;
                    DataTable dtPat = null;
                    dtPat = svc.GetDataTable(Sql, parm);
                    if (dtPat != null && dtPat.Rows.Count > 0)
                    {
                        drPat = dtPat.Rows[0];
                    }
                    #endregion

                    // 1. 打标识
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
                    parm = svc.CreateParm(7);
                    parm[++n].Value = 3;
                    parm[++n].Value = sampleVo.checkDate.Value;
                    parm[++n].Value = sampleVo.checkerId;
                    parm[++n].Value = sampleVo.applyEmpId;
                    parm[++n].Value = 0;
                    parm[++n].Value = "";
                    parm[++n].Value = sampleVo.barCode;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                    #endregion

                    // 2. 打包信息
                    #region t_samplepack
                    Sql = @"update t_samplepack set checkerid = ?, checkdate = ? where barcode = ?";
                    n = -1;
                    parm = svc.CreateParm(3);
                    parm[++n].Value = sampleVo.checkerId;
                    parm[++n].Value = sampleVo.checkDate.Value;
                    parm[++n].Value = sampleVo.barCode;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                    #endregion

                    #region 条码2项目 1) 0000041 - AU680
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
                         order by c.check_item_id_chr";

                    parm = svc.CreateParm(1);
                    parm[0].Value = sampleVo.barCode;
                    dtItem = svc.GetDataTable(Sql, parm);
                    if (dtItem != null && dtItem.Rows.Count > 0)
                    {
                        Sql = @"delete from t_opr_lis_barcode2item where barcode = ?";
                        parm = svc.CreateParm(1);
                        parm[0].Value = sampleVo.barCode;
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));

                        Sql = @"insert into t_opr_lis_barcode2item
                              (barcode, itemid, itemname, checktime)
                            values
                              (?, ?, ?, ?)";
                        DateTime dtmNow = DateTime.Now;
                        foreach (DataRow dr in dtItem.Rows)
                        {
                            parm = svc.CreateParm(4);
                            parm[0].Value = sampleVo.barCode;
                            parm[1].Value = dr["check_item_id_chr"].ToString();
                            parm[2].Value = dr["device_check_item_name_vchr"].ToString();
                            parm[3].Value = dtmNow;
                            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                        }
                    }
                    #endregion

                   

                }
                else if (sampleVo.typeId == 1)      // 体检采样时间
                {
                    DataTable dtSamp = null;
                    Sql = @"select peSamplingDate from t_samplepack where barcode = ?";
                    n = -1;
                    parm = svc.CreateParm(1);
                    parm[++n].Value = sampleVo.barCode;
                    dtSamp = svc.GetDataTable(Sql, parm);
                    if (dtSamp != null && dtSamp.Rows.Count > 0)
                    {
                        if (dtSamp.Rows[0]["peSamplingDate"] != DBNull.Value)
                            peSamplingDate = Convert.ToDateTime(dtSamp.Rows[0]["peSamplingDate"]);
                    }

                    #region 防止误操作：本应【住院核收】=》【体检操作】

                    Sql = @"select 1 from t_opr_lis_sample where barcode_vchr = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = sampleVo.barCode;
                    DataTable dt3 = svc.GetDataTable(Sql, parm);
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
                dtCheckItem = svc.GetDataTable(Sql);

                //FB200R 粪便分析仪-写数据库
                FB200RExecpro(sampleVo, drPat);

                #region 2020--11-05 住院核收: 一定要直接发回，不再执行体检流程
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
                dtSample = svc.GetDataTable(Sql);
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
                dtReport = svc.GetDataTable(Sql);

                string applyId = string.Empty;
                string sampleId = string.Empty;
                string sampleGroupId = string.Empty;

                svcLis.m_lngGenerateNewID("t_opr_lis_application", "APPLICATION_ID_CHR", out applyId);
                svcLis.m_lngGenerateNewID("t_opr_lis_sample", "sample_id_chr", out sampleId);

                // 1. 检验申请表
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
                parm = svc.CreateParm(31);
                parm[++n].Value = applyId;
                parm[++n].Value = sampleVo.checkDate.Value;
                parm[++n].Value = sampleVo.patientId;
                parm[++n].Value = sampleVo.checkDate.Value;
                parm[++n].Value = sampleVo.sex;
                parm[++n].Value = sampleVo.patName;
                parm[++n].Value = null;
                parm[++n].Value = sampleVo.age;
                parm[++n].Value = "3";          // 1 住院; 2 门诊; 3 体检
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
                parm[++n].Value = 1;            // 检验编码: 0 不能编辑; 1 可编辑。
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
                lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));

                #endregion

                // 2. 申请单的报告组
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
                parm = svc.CreateParm(13);
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
                lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                // 报告状态
                //patVo.m_intReportStatus = 1;
                #endregion

                // 3. 申请单要做的标本组
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
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                }
                #endregion

                // 4.5.6. 申请单要做的标本组对应的检验项目. 申请单对应申请单元 . 申请单元对应的项目
                #region t_opr_lis_app_check_item . t_opr_lis_app_apply_unit . t_opr_lis_app_unit_item
                List<string> lstApplyUnit = new List<string>();
                foreach (DataRow dr1 in dtCheckItem.Rows)
                {
                    // 申请单对应申请单元
                    string applyUnitId = dr1["apply_unit_id_chr"].ToString();

                    // 申请单要做的标本组对应的检验项目
                    Sql = @"insert into t_opr_lis_app_check_item
                                              (check_item_id_chr,
                                               sample_group_id_chr,
                                               report_group_id_chr,
                                               application_id_chr,
                                               itemprice_mny)
                                            values
                                              (?, ?, ?, ?, ?)";
                    n = -1;
                    parm = svc.CreateParm(5);
                    parm[++n].Value = dr1["check_item_id_chr"].ToString();
                    parm[++n].Value = dicGroupSample[applyUnitId]; //dtSample.Rows[0]["sample_group_id_chr"].ToString();
                    parm[++n].Value = dtReport.Rows[0]["report_group_id_chr"].ToString();
                    parm[++n].Value = applyId;
                    parm[++n].Value = dr1["itemprice_mny"].ToString();
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));

                    if (lstApplyUnit.IndexOf(applyUnitId) < 0)
                    {
                        lstApplyUnit.Add(applyUnitId);

                        Sql = @"insert into t_opr_lis_app_apply_unit
                                          (application_id_chr, user_group_string, apply_unit_id_chr)
                                        values
                                          (?, ?, ?)";
                        n = -1;
                        parm = svc.CreateParm(3);
                        parm[++n].Value = applyId;
                        parm[++n].Value = ">>" + applyUnitId;
                        parm[++n].Value = applyUnitId;
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                    }

                    // 申请单元对应的项目
                    Sql = @"insert into t_opr_lis_app_unit_item
                                          (application_id_chr, check_item_id_chr, apply_unit_id_chr)
                                        values
                                          (?, ?, ?)";

                    n = -1;
                    svcLis.CreateDatabaseParameter(3, out parm);
                    parm[++n].Value = applyId;
                    parm[++n].Value = dr1["check_item_id_chr"].ToString();
                    parm[++n].Value = applyUnitId;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                }
                #endregion

                // 7. 标本信息
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
                parm = svc.CreateParm(34);
                parm[++n].Value = sampleVo.checkDate.Value;
                parm[++n].Value = sampleVo.sex;
                parm[++n].Value = sampleVo.patName;
                parm[++n].Value = null;
                parm[++n].Value = sampleVo.age;
                parm[++n].Value = "3";      // 体检
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
                parm[++n].Value = 3;                          // 记录状态  -1:历史记录 0 -- 无效 1:初始状态 2:已采集 3:已核收 4:已检验 5:已处理结果 6:已审核 7:已退回
                parm[++n].Value = dtSample.Rows[0]["sample_type_id_chr"].ToString();
                parm[++n].Value = -1;
                parm[++n].Value = 1;
                parm[++n].Value = null;
                parm[++n].Value = sampleVo.checkDate.Value;     // 核收时间
                parm[++n].Value = sampleVo.checkerId;           // 核收人
                parm[++n].Value = applyId;
                parm[++n].Value = sampleVo.peNo;                // 住院号 ?要不要填体检号
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = null;
                lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                #endregion

                // 8. 打包信息
                #region t_samplepack
                Sql = @"update t_samplepack set checkerid = ?, checkdate = ? where barcode = ?";
                n = -1;
                parm = svc.CreateParm(3);
                parm[++n].Value = sampleVo.checkerId;
                parm[++n].Value = sampleVo.checkDate.Value;
                parm[++n].Value = sampleVo.barCode;
                lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                #endregion

                #region 条码2项目 1) 0000041 - AU680
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
                         order by c.check_item_id_chr";

                parm = svc.CreateParm(1);
                parm[0].Value = sampleVo.barCode;
                dtItem = svc.GetDataTable(Sql, parm);
                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    Sql = @"delete from t_opr_lis_barcode2item where barcode = ?";
                    parm = svc.CreateParm(1);
                    parm[0].Value = sampleVo.barCode;
                    lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));

                    Sql = @"insert into t_opr_lis_barcode2item
                              (barcode, itemid, itemname, checktime)
                            values
                              (?, ?, ?, ?)";
                    DateTime dtmNow = DateTime.Now;
                    foreach (DataRow dr in dtItem.Rows)
                    {
                        parm = svc.CreateParm(4);
                        parm[0].Value = sampleVo.barCode;
                        parm[1].Value = dr["check_item_id_chr"].ToString();
                        parm[2].Value = dr["device_check_item_name_vchr"].ToString();
                        parm[3].Value = dtmNow;
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parm));
                    }
                }
                #endregion

                if (lstParm.Count > 0)
                {
                    affectRows = svc.Commit(lstParm);
                }
                if (affectRows > 0)
                    return true;
                else
                    return false;
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
                svc.Dispose();
                svc = null;
            }
            return false;
        }
        #endregion

        #region 2000R粪便全自动分析仪
        /// <summary>
        /// 
        /// </summary>
        /// <param name="barCode"></param>
        void FB200RExecpro(EntitySamplePack sampleVo, DataRow drPat)
        {
            string sqlConnStr = string.Empty;
            SqlHelper svc = null;
            SqlHelper svcLis2 = null;
            IDataParameter[] parms = null;
            DataTable dtParm = null;
            int affectRows = -1;
            bool isJj = false;
            bool isJtj = false;
            List<DacParm> lstParm = new List<DacParm>();

            try
            {

                string Sql = @"select a.parmcode_chr, a.parmvalue_vchr, a.note_vchr
                          from t_bse_sysparm a
                         where a.status_int = 1
                           and a.parmcode_chr = '7014'";
                svc = new SqlHelper(EnumBiz.onlineDB);
                dtParm = svc.GetDataTable(Sql);
                if (dtParm != null && dtParm.Rows.Count > 0)
                {
                    DataRow drParm = dtParm.Rows[0];
                    sqlConnStr = drParm["note_vchr"].ToString();
                }
                else
                    return;
                svcLis2 = new SqlHelper(EnumBiz.lisDB);
                //仪器库中已存在该条码
                Sql = @"select * from tb_sample_info where sample_no = " + sampleVo.barCode;
                DataTable dt = svcLis2.GetDataTable(Sql);
                if (dt != null && dt.Rows.Count > 0)
                    return;

                Sql = @"select a.barcode_vchr, b.apply_unit_id_chr, c.jclx_jj, c.jclx_jtj
                          from t_opr_lis_sample a
                          left join t_opr_lis_app_apply_unit b
                            on a.application_id_chr = b.application_id_chr
                          left join t_aid_lis_apply_unit c
                            on b.apply_unit_id_chr = c.apply_unit_id_chr
                         where a.status_int > 0
                           and a.barcode_vchr = '{0}'";

                Sql = string.Format(Sql, sampleVo.barCode);

                dt = svc.GetDataTable(Sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string jj = dr["jclx_jj"].ToString();
                        string jtj = dr["jclx_jtj"].ToString();
                        if (jj == "1")
                            isJj = true;
                        if (jtj == "1")
                            isJtj = true;
                    }
                }

                if (isJj || isJtj)
                {
                    parms = svcLis2.CreateParm(11);
                    Sql = @"exec proc_Receive_Sample ?,?,'','','',?,?,'粪便',?,'','','',?,'',8,13,23,32,?,?,?,?,? ";
                    parms[0].Value = sampleVo.patName;
                    parms[1].Value = sampleVo.sex.Trim();
                    parms[2].Value = (drPat == null ? "" : drPat["cardNo"].ToString());
                    parms[3].Value = (drPat == null ? "" : drPat["ipNo"].ToString());
                    parms[4].Value = sampleVo.barCode;
                    parms[5].Value = DateTime.Now.ToString("yyyy-MM-dd");
                    parms[6].Value = (isJj == true ? "1" : "0");
                    parms[7].Value = (isJtj == true ? "15" : "0");
                    parms[8].Value = (isJtj == true ? "18" : "0");
                    parms[9].Value = (isJtj == true ? "59" : "0");
                    parms[10].Value = (isJtj == true ? "79" : "0");
                    affectRows = svcLis2.ExecSql(Sql, parms);
                }
                else
                    return;

                Sql = @"delete from T_FB2000R where barcode = '" + sampleVo.barCode + "'";
                lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql));
                Sql = @"insert into T_FB2000R values (?, ?, ?)";
                parms = svc.CreateParm(3);
                parms[0].Value = sampleVo.barCode;
                parms[1].Value = DateTime.Now;
                parms[2].Value = affectRows > 0 ? 1:0 ;
                lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, Sql, parms));

                if (lstParm.Count > 0)
                    svc.Commit(lstParm);
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
                parms = null;
                svcLis2 = null;
            }
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
