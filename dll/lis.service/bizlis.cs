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
                else
                {
                    foreach (clsLIS_Device_Test_ResultVO objRes in p_objResultArr)
                    {
                        has.Add(objRes.strDevice_Check_Item_Name, objRes.strResult);
                    }
                }

                lngRes = 1;
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
                    clsLogText objLogger = new clsLogText();

                    int intImportReq = -1;
                    if (blnFlag)
                    {
                        if (lngRes == 1)
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

                            lngRes = 0;
                            dtbReq = svc.GetDataTable(strSQL, objDPArr4);
                            if (dtbReq != null && dtbReq.Rows.Count != 0)
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
                            lngRes = 0;

                            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, c_strAddResultImportReq, objDPArr5));
                            //lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddResultImportReq, ref lngRecEff, objDPArr5);
                            #endregion
                        }
                        if (lngRes == 1)
                        {
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
                            lngRes = 0;
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

                            lngRes = 0;
                            //lngRes = objHRPSvc.lngExecuteParameterSQL(c_strAddLabResultLog, ref lngRecEff, objDPArr1);
                            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, c_strAddLabResultLog, objDPArr1));

                            #endregion
                            //}
                        }
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
                        lngRes = 0;
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
                        lngRes = 0;
                        //lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDPArr1);
                        lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, strSQL, objDPArr1));

                        #endregion
                        //}
                    }
                    System.Data.DataTable dtbRelation = null;
                    if (lngRes == 1)//无论此步骤成功与否都不应影响原始数据的进入.
                    {
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

                        lngRes = 0;
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

                            lngRes = 0;
                            //lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecEff, objDPArrs2);
                            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, strSQL, objDPArrs2));

                        }
                        #endregion
                    }
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
