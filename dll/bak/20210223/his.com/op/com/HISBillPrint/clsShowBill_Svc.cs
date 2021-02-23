using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.CryptographyLib;

namespace com.digitalwave.iCare.middletier.billprint
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsShowBill_Svc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsShowBill_Svc()
        {
            //
            // TODO: 在此添加构造函数逻辑
            //
        }

        #region 获取所有票据收费项目
        /// <summary>
        /// 获取所有票据收费项目
        /// </summary>
        /// <param name="dtbChargeItem"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeItemList(out DataTable p_dtbChargeItem)
        {
            long lngRes = 0;
            p_dtbChargeItem = null;

            string strSQL = @"select a.itemid_chr, a.itemname_vchr, a.itemcode_vchr, a.itemunit_chr,
       a.itemprice_mny, a.execdeptid_chr, a.itemcalctype_chr,
       a.itempycode_chr, a.itemwbcode_chr, a.usercode_chr, a.ifstop_int, a.billtypeid_int,
       b.code_vchr, b.deptname_vchr, c.typename_vchr
  from t_bse_billitem a, t_bse_deptdesc b, t_bse_chargeitemextype c
 where a.execdeptid_chr = b.deptid_chr(+)
   and a.itemcalctype_chr = c.typeid_chr(+)
   and c.flag_int = 9";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref p_dtbChargeItem);
                objHRPSvc.Dispose();

                if (lngRes > 0 && p_dtbChargeItem != null)
                {
                    DataView dv = p_dtbChargeItem.DefaultView;
                    dv.Sort = "itemcode_vchr asc";
                    p_dtbChargeItem = dv.ToTable();
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取所有科室
        /// <summary>
        /// 获取所有科室
        /// </summary>
        /// <param name="p_dtbDepartment"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptList(out DataTable p_dtbDepartment)
        {
            long lngRes = 0;
            p_dtbDepartment = null;

            string strSQL = @"select deptid_chr, deptname_vchr, pycode_chr, wbcode_chr, code_vchr,
       usercode_vchr
  from t_bse_deptdesc
 where status_int = 1";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref p_dtbDepartment);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取所有收费员
        /// <summary>
        /// 获取所有收费员
        /// </summary>
        /// <param name="p_dtbChargeEmp"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeEmp(out DataTable p_dtbChargeEmp)
        {
            long lngRes = 0;
            p_dtbChargeEmp = null;

            string strSQL = @"select a.empid_chr, a.empno_chr, a.lastname_vchr, a.pycode_chr
  from t_bse_employee a
 where exists (select 1 from t_opr_mainbill b where a.empid_chr = b.operemp_chr)";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref p_dtbChargeEmp);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取当前最新票据号
        /// <summary>
        /// 获取当前最新票据号
        /// </summary>
        /// <param name="p_strEmpid"></param>
        /// <param name="type">票据类型</param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetMaxBillNo(string p_strEmpid, int type)
        {
            long lngRes = 0;
            string m_strBillNo = string.Empty;
            string strSQL = @"select billno_vchr
  from t_opr_mainbill a
 where a.billtypeid_int = ?
   and a.operemp_chr = ?
   and a.recorddate_dat =
       (select max(b.recorddate_dat)
          from t_opr_mainbill b
         where b.billtypeid_int = ?
           and b.operemp_chr = ?)";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = type;
                objDPArr[1].Value = p_strEmpid;
                objDPArr[2].Value = type;
                objDPArr[3].Value = p_strEmpid;
                DataTable dtbResult = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult != null)
                {
                    if (dtbResult.Rows.Count > 0)
                    {
                        m_strBillNo = dtbResult.Rows[0][0].ToString();
                        //提取数字
                        string strTempContent = System.Text.RegularExpressions.Regex.Replace(m_strBillNo, @"^[A-Za-z]*", "");
                        Int64 n = Convert.ToInt64(strTempContent) + 1;
                        //字母+数字
                        m_strBillNo = m_strBillNo.Replace(strTempContent, "") + n.ToString().PadLeft(8, '0');
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return m_strBillNo;
        }
        #endregion

        #region 获取票据信息
        /// <summary>
        /// 获取票据信息
        /// </summary>
        /// <param name="p_strSeqID"></param>
        /// <param name="objMainVO">主表</param>
        /// <param name="lstBillDetail">明细</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBillData(string p_strSeqID, out clsMainBillInfo_VO objMainVO, out List<clsBillChargeDetail_VO> lstBillDetail)
        {
            long lngRes = 0;
            objMainVO = null;
            lstBillDetail = null;
            DataTable dtbTemp = null;
            string strSQL = string.Empty;
            try
            {
                strSQL = @"select seqid_chr, billno_vchr, recorddate_dat, status_int, payer_chr,
       totalsum_mny, balance_dat, advicenoteno_chr, paytype_int, notes_chr,
       operemp_chr, payee_chr, billtypeid_int, billdate_dat
  from t_opr_mainbill
 where seqid_chr = ?";

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strSeqID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbTemp, objDPArr);

                if (lngRes > 0 && dtbTemp != null)
                {
                    DataRow dr = null;
                    if (dtbTemp.Rows.Count > 0)
                    {
                        dr = dtbTemp.Rows[0];
                        objMainVO = new clsMainBillInfo_VO();
                        objMainVO.m_strSeqID = dr["seqid_chr"].ToString();
                        objMainVO.m_strBillNo = dr["billno_vchr"].ToString();
                        objMainVO.m_datRecDate = DateTime.Parse(dr["recorddate_dat"].ToString());
                        objMainVO.m_intStatus = int.Parse(dr["status_int"].ToString());
                        objMainVO.m_strPayer = dr["payer_chr"].ToString();
                        objMainVO.m_decTotalMny = Convert.ToDecimal(dr["totalsum_mny"].ToString());
                        //objMainVO.m_datBalanceDate = dr["balance_dat"].ToString();
                        objMainVO.m_strAdviceNoteno = dr["advicenoteno_chr"].ToString();
                        objMainVO.m_strPaymentID = dr["paytype_int"].ToString();
                        objMainVO.m_strNotes = dr["notes_chr"].ToString();
                        objMainVO.m_strOperEmp = dr["operemp_chr"].ToString();
                        objMainVO.m_strPayee = dr["payee_chr"].ToString();
                        objMainVO.m_strBillTypeID = dr["billtypeid_int"].ToString();
                        DateTime dattime;
                        if (DateTime.TryParse(dr["billdate_dat"].ToString(), out dattime))
                        {
                            objMainVO.m_strBillDate = dattime.ToString("yyyy-MM-dd");
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }

                strSQL = @"select seqid_chr, itemid_chr, itemcode_vchr, itemname_vchr, execdeptid_chr,
       execdeptcode_chr, itemunit_chr, tolqty_dec, itemprice_mny,
       tolprice_mny, execdeptname_chr
  from t_opr_mainbillde
 where seqid_chr = ?";
                objDPArr = null;
                dtbTemp = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strSeqID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbTemp, objDPArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbTemp != null)
                {
                    DataRow dr = null;
                    lstBillDetail = new List<clsBillChargeDetail_VO>();
                    clsBillChargeDetail_VO objRecord = null;
                    for (int i1 = 0; i1 < dtbTemp.Rows.Count; i1++)
                    {
                        dr = dtbTemp.Rows[i1];
                        objRecord = new clsBillChargeDetail_VO();
                        objRecord.m_strSeqID = dr["seqid_chr"].ToString();
                        objRecord.m_strItemID = dr["itemid_chr"].ToString();
                        objRecord.m_strItemCode = dr["itemcode_vchr"].ToString();
                        objRecord.m_strItemName = dr["itemname_vchr"].ToString();
                        objRecord.m_strExecDeptID = dr["execdeptid_chr"].ToString();
                        objRecord.m_strExecDeptCode = dr["execdeptcode_chr"].ToString();
                        objRecord.m_strItemUnit = dr["itemunit_chr"].ToString();
                        objRecord.m_decTolQty = Convert.ToDecimal(dr["tolqty_dec"].ToString());
                        objRecord.m_decItemPrice = Convert.ToDecimal(dr["itemprice_mny"].ToString());
                        objRecord.m_decTolPrice = Convert.ToDecimal(dr["tolprice_mny"].ToString());
                        objRecord.m_strExecDeptName = dr["execdeptname_chr"].ToString();
                        lstBillDetail.Add(objRecord);
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 主模块检索数据
        /// <summary>
        /// 主模块检索数据
        /// </summary>
        /// <param name="objConditionVO">检索条件</param>
        /// <param name="arrBillVO">返回记录</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindData(clsFindDataCondition_VO objConditionVO, out clsMainBillInfo_VO[] arrBillVO)
        {
            long lngRes = 0;
            arrBillVO = null;
            DataTable dtbResult = null;
            //必须指定查询种类
            if (objConditionVO.m_intType == -1)
            {
                return -1;
            }

            string strSQL = string.Empty;
            System.Collections.ArrayList arrParameters = new System.Collections.ArrayList();
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;

                if (objConditionVO.m_intType == 0 || objConditionVO.m_intType == 1)
                {
                    strSQL = @"select seqid_chr, billno_vchr, recorddate_dat, status_int, billdate_dat
  from t_opr_mainbill
 where status_int <> -1
   and recorddate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
       to_date(?, 'yyyy-mm-dd hh24:mi:ss')
   and billtypeid_int = ?";
                    arrParameters.Add(objConditionVO.m_strDateTimeBegin);
                    arrParameters.Add(objConditionVO.m_strDateTimeEnd);
                    arrParameters.Add(objConditionVO.m_intBillTypeid);
                }
                if (objConditionVO.m_strChargeEmpID != "")
                {
                    strSQL += @"
   and payee_chr = ?";
                    arrParameters.Add(objConditionVO.m_strChargeEmpID);
                }

                objHRPSvc.CreateDatabaseParameter(arrParameters.Count, out objDPArr);
                for (int i1 = 0; i1 < arrParameters.Count; i1++)
                {
                    objDPArr[i1].Value = arrParameters[i1];
                }
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult != null)
                {
                    DataView dv = dtbResult.DefaultView;
                    dv.Sort = "recorddate_dat desc";
                    dtbResult = dv.ToTable();
                    List<clsMainBillInfo_VO> lstBillVO = new List<clsMainBillInfo_VO>();
                    clsMainBillInfo_VO objRecord = null;
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        objRecord = new clsMainBillInfo_VO();
                        objRecord.m_strSeqID = dtbResult.Rows[i1]["seqid_chr"].ToString();
                        objRecord.m_strBillNo = dtbResult.Rows[i1]["billno_vchr"].ToString();
                        objRecord.m_intStatus = int.Parse(dtbResult.Rows[i1]["status_int"].ToString());
                        DateTime dattime;
                        if (DateTime.TryParse(dtbResult.Rows[i1]["billdate_dat"].ToString(), out dattime))
                        {
                            objRecord.m_strBillDate = dattime.ToString("yyyy-MM-dd");
                        }
                        switch (objRecord.m_intStatus)
                        {
                            case 0:
                                objRecord.m_strStatusDescription = "作废";
                                break;
                            case 1:
                                objRecord.m_strStatusDescription = "保存";
                                break;
                            case 2:
                                objRecord.m_strStatusDescription = "收费";
                                break;
                            case 3:
                                objRecord.m_strStatusDescription = "恢复";
                                break;
                            default:
                                objRecord.m_strStatusDescription = "未知";
                                break;
                        };
                        lstBillVO.Add(objRecord);
                    }
                    arrBillVO = lstBillVO.ToArray();
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取发票的结算状态
        /// <summary>
        /// 获取发票的结算状态
        /// </summary>
        /// <param name="p_strBillNo"></param>
        /// <param name="objMainVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBillMainInfo(string p_strBillNo, out clsMainBillInfo_VO objMainVO)
        {   
            long lngRes = 0;
            objMainVO = null;

            string strSQL = @"select a.seqid_chr, a.billno_vchr, a.recorddate_dat, a.status_int,
       a.payer_chr, a.totalsum_mny, a.balance_dat, a.advicenoteno_chr,
       a.paytype_int, a.notes_chr, a.operemp_chr, a.payee_chr,
       a.billtypeid_int, a.billdate_dat, a.balanceflag_int
  from t_opr_mainbill a
 where a.billno_vchr = ?
order by a.recorddate_dat desc";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strBillNo;
                DataTable dt = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, objDPArr);
                objHRPSvc.Dispose();
                if (lngRes > 0 && dt != null)
                {   
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        objMainVO = new clsMainBillInfo_VO();
                        objMainVO.m_strSeqID = dr["seqid_chr"].ToString();
                        objMainVO.m_strBillNo = dr["billno_vchr"].ToString();
                        objMainVO.m_datRecDate = DateTime.Parse(dr["recorddate_dat"].ToString());
                        objMainVO.m_intStatus = int.Parse(dr["status_int"].ToString());
                        objMainVO.m_strPayer = dr["payer_chr"].ToString();
                        objMainVO.m_decTotalMny = Convert.ToDecimal(dr["totalsum_mny"].ToString());
                        //objMainVO.m_datBalanceDate = dr["balance_dat"].ToString();
                        objMainVO.m_strAdviceNoteno = dr["advicenoteno_chr"].ToString();
                        objMainVO.m_strPaymentID = dr["paytype_int"].ToString();
                        objMainVO.m_strNotes = dr["notes_chr"].ToString();
                        objMainVO.m_strOperEmp = dr["operemp_chr"].ToString();
                        objMainVO.m_strPayee = dr["payee_chr"].ToString();
                        objMainVO.m_strBillTypeID = dr["billtypeid_int"].ToString();
                        DateTime dattime;
                        if (DateTime.TryParse(dr["billdate_dat"].ToString(), out dattime))
                        {
                            objMainVO.m_strBillDate = dattime.ToString("yyyy-MM-dd");
                        }
                        objMainVO.m_intBalanceFlag = Convert.ToInt32(dr["balanceflag_int"].ToString());
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 验证密码
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strID"></param>
        /// <param name="dt"></param>
        /// <param name="strEx"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthGetEmployeeInfo(string strID, out DataTable dt, string strEx)
        {
            dt = null;
            long lngRes = 0;

            string strSQL = @"select empid_chr, firstname_vchr, lastname_vchr, empidcard_chr, pycode_chr,
       sex_chr, birthdate_dat, psw_chr
  from t_bse_employee
 where status_int = 1 and empno_chr = ?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strID.Replace("'", "‘");
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, objDPArr);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    clsSymmetricAlgorithm objAlgorithm = new clsSymmetricAlgorithm();
                    string strDecryptPwd = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strDecryptPwd = objAlgorithm.m_strDecrypt(dt.Rows[i]["psw_chr"].ToString(), clsSymmetricAlgorithm.enmSymmetricAlgorithmType.DES);
                        dt.Rows[i]["psw_chr"] = strDecryptPwd;
                    }
                    dt.AcceptChanges();
                }
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取核算分类列表
        /// <summary>
        /// 获取核算分类列表
        /// </summary>
        /// <param name="p_dtbCalcType"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCalcTypeList(out DataTable p_dtbCalcType)
        {
            long lngRes = 0;
            p_dtbCalcType = null;

            string strSQL = @"select typeid_chr, typename_vchr, usercode_chr
  from t_bse_chargeitemextype
 where flag_int = 9";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref p_dtbCalcType);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据卡号获取病人姓名
        /// <summary>
        /// 根据卡号获取病人姓名
        /// </summary>
        /// <param name="p_strCardNO"></param>
        /// <param name="strName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientNameByCardNO(string p_strCardNO, out string strName)
        {
            long lngRes = 0;
            strName = p_strCardNO;
            string strSQL=@"select b.patientid_chr, b.lastname_vchr
  from t_bse_patientcard a ,t_bse_patient b
 where a.patientid_chr = b.patientid_chr
   and a.status_int > 0
   and a.patientcardid_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strCardNO;
                DataTable dtbTemp = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbTemp, objDPArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbTemp != null)
                {
                    if (dtbTemp.Rows.Count > 0)
                        strName = dtbTemp.Rows[0]["lastname_vchr"].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据票据号查询所有票据信息
        /// <summary>
        /// 根据票据号查询所有票据信息
        /// </summary>
        /// <param name="p_intBillTypeid"></param>
        /// <param name="p_strBillNo"></param>
        /// <param name="arrBillVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSourceByBillNo(int p_intBillTypeid, string p_strBillNo, out clsMainBillInfo_VO[] arrBillVO)
        {
            long lngRes = 0;
            string strSQL = @"select seqid_chr, billno_vchr, recorddate_dat, status_int, billdate_dat
  from t_opr_mainbill
 where billtypeid_int = ?
   and billno_vchr = ?";
            arrBillVO = new clsMainBillInfo_VO[0];

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_intBillTypeid;
                objDPArr[1].Value = p_strBillNo;
                DataTable dtbTemp = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbTemp, objDPArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbTemp != null)
                {
                    List<clsMainBillInfo_VO> lstBillVO = new List<clsMainBillInfo_VO>();
                    clsMainBillInfo_VO objRecord = null;
                    for (int i1 = 0; i1 < dtbTemp.Rows.Count; i1++)
                    {
                        objRecord = new clsMainBillInfo_VO();
                        objRecord.m_strSeqID = dtbTemp.Rows[i1]["seqid_chr"].ToString();
                        objRecord.m_strBillNo = dtbTemp.Rows[i1]["billno_vchr"].ToString();
                        objRecord.m_intStatus = int.Parse(dtbTemp.Rows[i1]["status_int"].ToString());
                        DateTime dattime;
                        if (DateTime.TryParse(dtbTemp.Rows[i1]["billdate_dat"].ToString(), out dattime))
                        {
                            objRecord.m_strBillDate = dattime.ToString("yyyy-MM-dd");
                        }
                        lstBillVO.Add(objRecord);
                    }
                    arrBillVO = lstBillVO.ToArray();
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取收费员未结账记录
        /// <summary>
        /// 获取收费员未结账记录
        /// </summary>
        /// <param name="strEmpId"></param>
        /// <param name="dtbCalc"></param>
        /// <param name="dtbCheckOut"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBalanceData(string strEmpId, out DataTable dtbCalc, out DataTable dtbCheckOut)
        {
            long lngRes = 0;
            dtbCalc = null;
            dtbCheckOut = null;
            string strSQL = @"select t1.coltotalmny, t2.typeid_chr, t2.typename_vchr as colcalctype
  from (select a.itemcatid_chr, sum(a.tolfee_mny) as coltotalmny
          from t_opr_billcalcsumde a
         where exists (select 1
                  from t_opr_mainbill b
                 where a.seqid_chr = b.seqid_chr                   
                   and b.BALANCEFLAG_INT = 0
                   and b.OPEREMP_CHR = ?)
         group by a.itemcatid_chr) t1,
       t_bse_chargeitemextype t2
 where t2.typeid_chr = t1.itemcatid_chr(+)
   and t2.flag_int = 9";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strEmpId;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbCalc, objDPArr);
                if (lngRes > 0 && dtbCalc != null)
                {
                    DataView dv = dtbCalc.DefaultView;
                    dv.Sort = "typeid_chr asc";
                    dtbCalc = dv.ToTable();
                }

                strSQL = @"select seqid_chr, billno_vchr, recorddate_dat, status_int, payer_chr,
       totalsum_mny, balance_dat, advicenoteno_chr, paytype_int, notes_chr,
       operemp_chr, payee_chr, billtypeid_int, billdate_dat, balanceflag_int,
       sbsum_mny
  from t_opr_mainbill
 where status_int <> 1 and balanceflag_int = 0 and operemp_chr = ?";
                objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strEmpId;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbCheckOut, objDPArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbCheckOut != null)
                {
                    DataView dv = dtbCheckOut.DefaultView;
                    dv.Sort = "billdate_dat, billno_vchr";
                    dtbCheckOut = dv.ToTable();

                    DataTable dtbTemp = new DataTable();
                    dtbTemp.Columns.Add("totalsum");//应收合计
                    dtbTemp.Columns.Add("returnmny");//作废金额
                    dtbTemp.Columns.Add("resumemny");//恢复金额
                    dtbTemp.Columns.Add("actualIncome");//实收合计
                    dtbTemp.Columns.Add("cashtotalmny");//现金
                    dtbTemp.Columns.Add("banktotalmny");//银行卡
                    dtbTemp.Columns.Add("checktotalmny");//支票
                    dtbTemp.Columns.Add("ictotalmny");//IC卡(其他)
                    dtbTemp.Columns.Add("pieces");
                    dtbTemp.Columns.Add("normalbillno");
                    dtbTemp.Columns.Add("cancelbillno");
                    dtbTemp.Columns.Add("resumebillno");

                    //获取发票起址段
                    string strNormalSegment = string.Empty;//正常起止票据号码
                    string strCancelSegment = string.Empty;//作废起止票据号码
                    string strResumeSegment = string.Empty;//恢复起止票据号码
                    string strNumber = string.Empty;//数字部分
                    System.Collections.Hashtable hasLetter = new System.Collections.Hashtable();
                    decimal decTotalTmp = 0;
                    DataTable dtTmp = null;
                    string m_strBillNo = string.Empty;
                    for (int i2 = 0; i2 < dtbCheckOut.Rows.Count; i2++)
                    {
                        //提取首字母
                        m_strBillNo = dtbCheckOut.Rows[i2]["billno_vchr"].ToString();
                        string strInitLetter = System.Text.RegularExpressions.Regex.Replace(m_strBillNo, @"[^A-Za-z]*", "");
                        if (!hasLetter.ContainsKey(strInitLetter))
                        {
                            hasLetter.Add(strInitLetter, strInitLetter);

                            #region 生成正常起止票据号码
                            decTotalTmp = 0;
                            DataView dvTmp = dtbCheckOut.DefaultView;
                            dvTmp.RowFilter = "billno_vchr like '" + strInitLetter + "%' and status_int = 2";
                            dvTmp.Sort = "billno_vchr";
                            dtTmp = dvTmp.ToTable();

                            string lastbill = string.Empty;
                            string lastNumber = string.Empty;
                            string typeName = string.Empty;
                            for (int i3 = 0; i3 < dtTmp.Rows.Count; i3++)
                            {
                                m_strBillNo = dtTmp.Rows[i3]["billno_vchr"].ToString();
                                strNumber = System.Text.RegularExpressions.Regex.Replace(m_strBillNo, @"^[A-Za-z]*", "");
                                if (lastNumber != "")
                                {
                                    if (Int64.Parse(lastNumber) + 1 == Int64.Parse(strNumber) && i3 != dtTmp.Rows.Count - 1)
                                    {
                                        decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i3]["totalsum_mny"].ToString());
                                    }
                                    else
                                    {
                                        if (strNormalSegment.Contains(lastbill) && i3 != dtTmp.Rows.Count - 1)
                                        {
                                            strNormalSegment = strNormalSegment.Substring(0, strNormalSegment.Length - 1) + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
                                            decTotalTmp = Convert.ToDecimal(dtTmp.Rows[i3]["totalsum_mny"].ToString());
                                        }
                                        else
                                        {
                                            if (Int64.Parse(lastNumber) + 1 == Int64.Parse(strNumber))
                                            {
                                                decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i3]["totalsum_mny"].ToString());
                                                strNormalSegment += m_strBillNo + "-";
                                            }
                                            else
                                            {
                                                strNormalSegment += lastbill + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
                                                decTotalTmp = Convert.ToDecimal(dtTmp.Rows[i3]["totalsum_mny"].ToString());
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (strNormalSegment != "")
                                    {
                                        strNormalSegment += ", " + m_strBillNo + "-";
                                    }
                                    else
                                    {
                                        strNormalSegment += m_strBillNo + "-";
                                    }

                                    decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i3]["totalsum_mny"].ToString());
                                    this.m_mthTranslateType(dtTmp.Rows[i3]["billtypeid_int"].ToString().Trim(), ref typeName);
                                }
                                lastNumber = strNumber;
                                lastbill = m_strBillNo;

                                if (i3 == dtTmp.Rows.Count - 1)
                                {
                                    strNormalSegment = strNormalSegment.Substring(0, strNormalSegment.Length - 1) + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + ")";
                                }
                            }
                            #endregion

                            #region 生成作废起止票据号码
                            dvTmp.RowFilter = "billno_vchr like '" + strInitLetter + "%' and status_int = 0";
                            dvTmp.Sort = "billno_vchr";
                            dtTmp = dvTmp.ToTable();

                            lastbill = string.Empty;
                            lastNumber = string.Empty;
                            decTotalTmp = 0;
                            for (int i4 = 0; i4 < dtTmp.Rows.Count; i4++)
                            {
                                m_strBillNo = dtTmp.Rows[i4]["billno_vchr"].ToString();
                                strNumber = System.Text.RegularExpressions.Regex.Replace(m_strBillNo, @"^[A-Za-z]*", "");
                                if (lastNumber != "")
                                {
                                    if (Int64.Parse(lastNumber) + 1 == Int64.Parse(strNumber) && i4 != dtTmp.Rows.Count - 1)
                                    {
                                        decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i4]["totalsum_mny"].ToString());
                                    }
                                    else
                                    {
                                        if (strCancelSegment.Contains(lastbill) && i4 != dtTmp.Rows.Count - 1)
                                        {
                                            strCancelSegment = strCancelSegment.Substring(0, strCancelSegment.Length - 1) + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
                                            decTotalTmp = Convert.ToDecimal(dtTmp.Rows[i4]["totalsum_mny"].ToString());
                                        }
                                        else
                                        {
                                            if (Int64.Parse(lastNumber) + 1 == Int64.Parse(strNumber))
                                            {
                                                decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i4]["totalsum_mny"].ToString());
                                                strCancelSegment += m_strBillNo + "-";
                                            }
                                            else
                                            {
                                                strCancelSegment += lastbill + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
                                                decTotalTmp = Convert.ToDecimal(dtTmp.Rows[i4]["totalsum_mny"].ToString());
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (strCancelSegment != "")
                                    {
                                        strCancelSegment += ", " + m_strBillNo + "-";
                                    }
                                    else
                                    {
                                        strCancelSegment += m_strBillNo + "-";
                                    }

                                    decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i4]["totalsum_mny"].ToString());
                                    this.m_mthTranslateType(dtTmp.Rows[i4]["billtypeid_int"].ToString().Trim(), ref typeName);
                                }
                                lastNumber = strNumber;
                                lastbill = m_strBillNo;

                                if (i4 == dtTmp.Rows.Count - 1)
                                {
                                    strCancelSegment = strCancelSegment.Substring(0, strCancelSegment.Length - 1) + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + ")";
                                }
                            }
                            #endregion

                            #region 生成恢复起止票据号码
                            dvTmp.RowFilter = "billno_vchr like '" + strInitLetter + "%' and status_int = 3";
                            dvTmp.Sort = "billno_vchr";
                            dtTmp = dvTmp.ToTable();

                            lastbill = string.Empty;
                            lastNumber = string.Empty;
                            decTotalTmp = 0;
                            for (int i5 = 0; i5 < dtTmp.Rows.Count; i5++)
                            {
                                m_strBillNo = dtTmp.Rows[i5]["billno_vchr"].ToString();
                                strNumber = System.Text.RegularExpressions.Regex.Replace(m_strBillNo, @"^[A-Za-z]*", "");
                                if (lastNumber != "")
                                {
                                    if (Int64.Parse(lastNumber) + 1 == Int64.Parse(strNumber) && i5 != dtTmp.Rows.Count - 1)
                                    {
                                        decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i5]["totalsum_mny"].ToString());
                                    }
                                    else
                                    {
                                        if (strResumeSegment.Contains(lastbill) && i5 != dtTmp.Rows.Count - 1)
                                        {
                                            strResumeSegment = strResumeSegment.Substring(0, strResumeSegment.Length - 1) + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
                                            decTotalTmp = Convert.ToDecimal(dtTmp.Rows[i5]["totalsum_mny"].ToString());
                                        }
                                        else
                                        {
                                            if (Int64.Parse(lastNumber) + 1 == Int64.Parse(strNumber))
                                            {
                                                decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i5]["totalsum_mny"].ToString());
                                                strResumeSegment += m_strBillNo + "-";
                                            }
                                            else
                                            {
                                                strResumeSegment += lastbill + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
                                                decTotalTmp = Convert.ToDecimal(dtTmp.Rows[i5]["totalsum_mny"].ToString());
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (strResumeSegment != "")
                                    {
                                        strResumeSegment += ", " + m_strBillNo + "-";
                                    }
                                    else
                                    {
                                        strResumeSegment += m_strBillNo + "-";
                                    }

                                    decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i5]["totalsum_mny"].ToString());
                                    this.m_mthTranslateType(dtTmp.Rows[i5]["billtypeid_int"].ToString().Trim(), ref typeName);
                                }
                                lastNumber = strNumber;
                                lastbill = m_strBillNo;

                                if (i5 == dtTmp.Rows.Count - 1)
                                {
                                    strResumeSegment = strResumeSegment.Substring(0, strResumeSegment.Length - 1) + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + ")";
                                }
                            }
                            #endregion
                        }
                    }

                    DataRow dr = dtbTemp.NewRow();
                    dr["totalsum"] = m_decConvertToDecimal(dtbCheckOut.Compute("sum(totalsum_mny)", "status_int = -1 or status_int = 2"));
                    dr["returnmny"] = m_decConvertToDecimal(dtbCheckOut.Compute("sum(totalsum_mny)", "status_int = 0"));
                    dr["resumemny"] = m_decConvertToDecimal(dtbCheckOut.Compute("sum(totalsum_mny)", "status_int = 3"));
                    dr["actualIncome"] = m_decConvertToDecimal(dr["totalsum"]) + m_decConvertToDecimal(dr["returnmny"]) + m_decConvertToDecimal(dr["resumemny"]);
                    dr["cashtotalmny"] = m_decConvertToDecimal(dtbCheckOut.Compute("sum(totalsum_mny)", "paytype_int = 0"));
                    dr["banktotalmny"] = m_decConvertToDecimal(dtbCheckOut.Compute("sum(totalsum_mny)", "paytype_int = 1"));
                    dr["checktotalmny"] = m_decConvertToDecimal(dtbCheckOut.Compute("sum(totalsum_mny)", "paytype_int = 2"));
                    dr["ictotalmny"] = m_decConvertToDecimal(dtbCheckOut.Compute("sum(totalsum_mny)", "paytype_int not in (0, 1, 2)"));
                    dr["pieces"] = dtbCheckOut.Compute("count(billno_vchr)", "status_int = 2 or status_int = 3");
                    dr["normalbillno"] = strNormalSegment;
                    dr["cancelbillno"] = strCancelSegment;
                    dr["resumebillno"] = strResumeSegment;
                    dtbTemp.Rows.Add(dr);
                    dtbTemp.AcceptChanges();
                    dtbCheckOut = dtbTemp;
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }

        private void m_mthTranslateType(string parameter, ref string typeName)
        {
            switch (parameter)
            {
                case "1":
                    typeName = "往来";
                    break;
                case "2":
                    typeName = "统一";
                    break;
                default:
                    typeName = "";
                    break;
            };
        }

        private decimal m_decConvertToDecimal(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    return Convert.ToDecimal(obj);
                }
                catch
                {
                    return 0;
                }
            }
        }
        #endregion

        #region 获取已结账列表
        /// <summary>
        /// 获取已结账列表
        /// </summary>
        /// <param name="strDateTimeBegin"></param>
        /// <param name="strDateTimeEnd"></param>
        /// <param name="strOperEmpid"></param>
        /// <param name="strDateArray"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBalanceList(string strDateTimeBegin,string strDateTimeEnd,string strOperEmpid, out string[] strDateArray)
        {
            long lngRes = 0;
            strDateArray = null;
            string strSQL = @"select distinct a.balance_dat
  from t_opr_mainbill a
 where a.balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
       to_date(?, 'yyyy-mm-dd hh24:mi:ss')
   and a.balanceflag_int = 1
   and a.operemp_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = strDateTimeBegin;
                objDPArr[1].Value = strDateTimeEnd;
                objDPArr[2].Value = strOperEmpid;
                DataTable dtbResult = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbResult != null)
                {
                    System.Data.DataView objDV = new DataView(dtbResult);
                    objDV.Sort = "balance_dat desc";
                    dtbResult = null;
                    dtbResult = objDV.ToTable();
                    strDateArray = new string[dtbResult.Rows.Count];
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        strDateArray[i1] = DateTime.Parse(dtbResult.Rows[i1][0].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 读取历史日结记录
        /* 注意：对于已结的status=2的票据，日结后给作废掉，它的status=-1但balance_dat < extentdate_dat
           读取的时候，依然视为已收费发票。这才能保证读取到的历史数据与当时日结的数据一致 */
        /// <summary>
        /// 读取历史日结记录
        /// </summary>
        /// <param name="strEmpId"></param>
        /// <param name="strBalanceDate"></param>
        /// <param name="p_strDateTo">结束日期</param>
        /// <param name="dtbCalc"></param>
        /// <param name="dtbCheckOut"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBalanceDataHistory(string strEmpId, string strBalanceDate, string p_strDateTo, out DataTable dtbCalc, out DataTable dtbCheckOut)
        {
            long lngRes = 0;
            dtbCalc = null;
            dtbCheckOut = null;
            string strSQL = string.Empty;
            string strSub = @"
                   and b.operemp_chr = ?";
            List<string> lstParameters = new List<string>();
            if (string.IsNullOrEmpty(p_strDateTo))
            {
                strSQL = @"select t1.coltotalmny, t2.typeid_chr, t2.typename_vchr as colcalctype
  from (select a.itemcatid_chr, sum(a.tolfee_mny) as coltotalmny
          from t_opr_billcalcsumde a
         where exists
         (select 1
                  from t_opr_mainbill b
                 where a.seqid_chr = b.seqid_chr
                   and (b.status_int = 2 or b.status_int = 3 or
                       (b.status_int = -1 and
                       b.balance_dat < b.extentdate_dat))
                   and b.balance_dat =
                       to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                   and b.balanceflag_int = 1
                   and b.operemp_chr = ?)
         group by a.itemcatid_chr) t1,
       t_bse_chargeitemextype t2
 where t2.typeid_chr = t1.itemcatid_chr(+)
   and t2.flag_int = 9";
                lstParameters.AddRange(new string[] { strBalanceDate, strEmpId });
            }
            else
            {
                strSQL = @"select t1.coltotalmny, t2.typeid_chr, t2.typename_vchr as colcalctype
  from (select a.itemcatid_chr, sum(a.tolfee_mny) as coltotalmny
          from t_opr_billcalcsumde a
         where exists
         (select 1
                  from t_opr_mainbill b
                 where a.seqid_chr = b.seqid_chr
                   and (b.status_int = 2 or b.status_int = 3 or
                       (b.status_int = -1 and
                       b.balance_dat < b.extentdate_dat))
                   and b.balance_dat between
                       to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                   and b.balanceflag_int = 1{0})
         group by a.itemcatid_chr) t1,
       t_bse_chargeitemextype t2
 where t2.typeid_chr = t1.itemcatid_chr(+)
   and t2.flag_int = 9";
                
                if (string.IsNullOrEmpty(strEmpId))
                {
                    strSQL = string.Format(strSQL, "");
                    lstParameters.AddRange(new string[] { strBalanceDate, p_strDateTo });
                }
                else
                {
                    strSQL = string.Format(strSQL, strSub);
                    lstParameters.AddRange(new string[] { strBalanceDate, p_strDateTo, strEmpId });
                }
            }

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(lstParameters.Count, out objDPArr);
                for (int j1 = 0; j1 < objDPArr.Length; j1++)
                {
                    objDPArr[j1].Value = lstParameters[j1];
                }
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbCalc, objDPArr);
                if (lngRes > 0 && dtbCalc != null)
                {
                    DataView dv = dtbCalc.DefaultView;
                    dv.Sort = "typeid_chr asc";
                    dtbCalc = dv.ToTable();
                }

                lstParameters.Clear();
                strSub = @"
   and operemp_chr = ?";
                if (string.IsNullOrEmpty(p_strDateTo))
                {
                    strSQL = @"select seqid_chr, billno_vchr, recorddate_dat, status_int, payer_chr,
       totalsum_mny, balance_dat, advicenoteno_chr, paytype_int, notes_chr,
       operemp_chr, payee_chr, billtypeid_int, billdate_dat, balanceflag_int,
       sbsum_mny, extentdate_dat
  from t_opr_mainbill
 where balance_dat = to_date(?, 'yyyy-mm-dd hh24:mi:ss')
   and balanceflag_int = 1
   and operemp_chr = ?";
                    lstParameters.AddRange(new string[] { strBalanceDate, strEmpId });
                }
                else
                {
                    strSQL = @"select seqid_chr, billno_vchr, recorddate_dat, status_int, payer_chr,
       totalsum_mny, balance_dat, advicenoteno_chr, paytype_int, notes_chr,
       operemp_chr, payee_chr, billtypeid_int, billdate_dat, balanceflag_int,
       sbsum_mny, extentdate_dat
  from t_opr_mainbill
 where balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')
   and balanceflag_int = 1{0}";

                    if (string.IsNullOrEmpty(strEmpId))
                    {
                        strSQL = string.Format(strSQL, "");
                        lstParameters.AddRange(new string[] { strBalanceDate, p_strDateTo });
                    }
                    else
                    {
                        strSQL = string.Format(strSQL, strSub);
                        lstParameters.AddRange(new string[] { strBalanceDate, p_strDateTo, strEmpId });
                    }
                }
                objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(lstParameters.Count, out objDPArr);
                for (int j2 = 0; j2 < objDPArr.Length; j2++)
                {
                    objDPArr[j2].Value = lstParameters[j2];
                }
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbCheckOut, objDPArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbCheckOut != null)
                {
                    DataView dv = dtbCheckOut.DefaultView;
                    dv.Sort = "billdate_dat, billno_vchr";
                    dtbCheckOut = dv.ToTable();

                    DataTable dtbTemp = new DataTable();
                    dtbTemp.Columns.Add("totalsum");//应收合计
                    dtbTemp.Columns.Add("returnmny");//作废金额
                    dtbTemp.Columns.Add("resumemny");//恢复金额
                    dtbTemp.Columns.Add("actualIncome");//实收合计
                    dtbTemp.Columns.Add("cashtotalmny");//现金
                    dtbTemp.Columns.Add("banktotalmny");//银行卡
                    dtbTemp.Columns.Add("checktotalmny");//支票
                    dtbTemp.Columns.Add("ictotalmny");//IC卡(其他)
                    dtbTemp.Columns.Add("pieces");
                    dtbTemp.Columns.Add("normalbillno");
                    dtbTemp.Columns.Add("cancelbillno");
                    dtbTemp.Columns.Add("resumebillno");

                    //获取发票起址段
                    string strNormalSegment = string.Empty;//正常起止票据号码
                    string strCancelSegment = string.Empty;//作废起止票据号码
                    string strResumeSegment = string.Empty;//恢复起止票据号码
                    string strNumber = string.Empty;//数字部分
                    System.Collections.Hashtable hasLetter = new System.Collections.Hashtable();
                    decimal decTotalTmp = 0;
                    DataTable dtTmp = null;
                    string m_strBillNo = string.Empty;
                    for (int i2 = 0; i2 < dtbCheckOut.Rows.Count; i2++)
                    {
                        //提取首字母
                        m_strBillNo = dtbCheckOut.Rows[i2]["billno_vchr"].ToString();
                        string strInitLetter = System.Text.RegularExpressions.Regex.Replace(m_strBillNo, @"[^A-Za-z]*", "");
                        if (!hasLetter.ContainsKey(strInitLetter))
                        {
                            hasLetter.Add(strInitLetter, strInitLetter);

                            #region 生成正常起止票据号码
                            decTotalTmp = 0;
                            DataView dvTmp = dtbCheckOut.DefaultView;
                            dvTmp.RowFilter = "billno_vchr like '" + strInitLetter + "%' and (status_int = 2 or (status_int = -1 and balance_dat < extentdate_dat))";
                            dvTmp.Sort = "billno_vchr";
                            dtTmp = dvTmp.ToTable();

                            string lastbill = string.Empty;
                            string lastNumber = string.Empty;
                            string typeName = string.Empty;
                            for (int i3 = 0; i3 < dtTmp.Rows.Count; i3++)
                            {
                                m_strBillNo = dtTmp.Rows[i3]["billno_vchr"].ToString();
                                strNumber = System.Text.RegularExpressions.Regex.Replace(m_strBillNo, @"^[A-Za-z]*", "");
                                if (lastNumber != "")
                                {
                                    if (Int64.Parse(lastNumber) + 1 == Int64.Parse(strNumber) && i3 != dtTmp.Rows.Count - 1)
                                    {
                                        decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i3]["totalsum_mny"].ToString());
                                    }
                                    else
                                    {
                                        if (strNormalSegment.Contains(lastbill) && i3 != dtTmp.Rows.Count - 1)
                                        {
                                            strNormalSegment = strNormalSegment.Substring(0, strNormalSegment.Length - 1) + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
                                            decTotalTmp = Convert.ToDecimal(dtTmp.Rows[i3]["totalsum_mny"].ToString());
                                        }
                                        else
                                        {
                                            if (Int64.Parse(lastNumber) + 1 == Int64.Parse(strNumber))
                                            {
                                                decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i3]["totalsum_mny"].ToString());
                                                strNormalSegment += m_strBillNo + "-";
                                            }
                                            else
                                            {
                                                strNormalSegment += lastbill + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
                                                decTotalTmp = Convert.ToDecimal(dtTmp.Rows[i3]["totalsum_mny"].ToString());
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (strNormalSegment != "")
                                    {
                                        strNormalSegment += ", " + m_strBillNo + "-";
                                    }
                                    else
                                    {
                                        strNormalSegment += m_strBillNo + "-";
                                    }

                                    decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i3]["totalsum_mny"].ToString());
                                    this.m_mthTranslateType(dtTmp.Rows[i3]["billtypeid_int"].ToString().Trim(), ref typeName);
                                }
                                lastNumber = strNumber;
                                lastbill = m_strBillNo;

                                if (i3 == dtTmp.Rows.Count - 1)
                                {
                                    strNormalSegment = strNormalSegment.Substring(0, strNormalSegment.Length - 1) + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + ")";
                                }
                            }
                            #endregion

                            #region 生成作废起止票据号码
                            dvTmp.RowFilter = "billno_vchr like '" + strInitLetter + "%' and status_int = 0";
                            dvTmp.Sort = "billno_vchr";
                            dtTmp = dvTmp.ToTable();

                            lastbill = string.Empty;
                            lastNumber = string.Empty;
                            decTotalTmp = 0;
                            for (int i4 = 0; i4 < dtTmp.Rows.Count; i4++)
                            {
                                m_strBillNo = dtTmp.Rows[i4]["billno_vchr"].ToString();
                                strNumber = System.Text.RegularExpressions.Regex.Replace(m_strBillNo, @"^[A-Za-z]*", "");
                                if (lastNumber != "")
                                {
                                    if (Int64.Parse(lastNumber) + 1 == Int64.Parse(strNumber) && i4 != dtTmp.Rows.Count - 1)
                                    {
                                        decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i4]["totalsum_mny"].ToString());
                                    }
                                    else
                                    {
                                        if (strCancelSegment.Contains(lastbill) && i4 != dtTmp.Rows.Count - 1)
                                        {
                                            strCancelSegment = strCancelSegment.Substring(0, strCancelSegment.Length - 1) + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
                                            decTotalTmp = Convert.ToDecimal(dtTmp.Rows[i4]["totalsum_mny"].ToString());
                                        }
                                        else
                                        {
                                            if (Int64.Parse(lastNumber) + 1 == Int64.Parse(strNumber))
                                            {
                                                decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i4]["totalsum_mny"].ToString());
                                                strCancelSegment += m_strBillNo + "-";
                                            }
                                            else
                                            {
                                                strCancelSegment += lastbill + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
                                                decTotalTmp = Convert.ToDecimal(dtTmp.Rows[i4]["totalsum_mny"].ToString());
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (strCancelSegment != "")
                                    {
                                        strCancelSegment += ", " + m_strBillNo + "-";
                                    }
                                    else
                                    {
                                        strCancelSegment += m_strBillNo + "-";
                                    }

                                    decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i4]["totalsum_mny"].ToString());
                                    this.m_mthTranslateType(dtTmp.Rows[i4]["billtypeid_int"].ToString().Trim(), ref typeName);
                                }
                                lastNumber = strNumber;
                                lastbill = m_strBillNo;

                                if (i4 == dtTmp.Rows.Count - 1)
                                {
                                    strCancelSegment = strCancelSegment.Substring(0, strCancelSegment.Length - 1) + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + ")";
                                }
                            }
                            #endregion

                            #region 生成恢复起止票据号码
                            dvTmp.RowFilter = "billno_vchr like '" + strInitLetter + "%' and status_int = 3";
                            dvTmp.Sort = "billno_vchr";
                            dtTmp = dvTmp.ToTable();

                            lastbill = string.Empty;
                            lastNumber = string.Empty;
                            decTotalTmp = 0;
                            for (int i5 = 0; i5 < dtTmp.Rows.Count; i5++)
                            {
                                m_strBillNo = dtTmp.Rows[i5]["billno_vchr"].ToString();
                                strNumber = System.Text.RegularExpressions.Regex.Replace(m_strBillNo, @"^[A-Za-z]*", "");
                                if (lastNumber != "")
                                {
                                    if (Int64.Parse(lastNumber) + 1 == Int64.Parse(strNumber) && i5 != dtTmp.Rows.Count - 1)
                                    {
                                        decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i5]["totalsum_mny"].ToString());
                                    }
                                    else
                                    {
                                        if (strResumeSegment.Contains(lastbill) && i5 != dtTmp.Rows.Count - 1)
                                        {
                                            strResumeSegment = strResumeSegment.Substring(0, strResumeSegment.Length - 1) + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
                                            decTotalTmp = Convert.ToDecimal(dtTmp.Rows[i5]["totalsum_mny"].ToString());
                                        }
                                        else
                                        {
                                            if (Int64.Parse(lastNumber) + 1 == Int64.Parse(strNumber))
                                            {
                                                decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i5]["totalsum_mny"].ToString());
                                                strResumeSegment += m_strBillNo + "-";
                                            }
                                            else
                                            {
                                                strResumeSegment += lastbill + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
                                                decTotalTmp = Convert.ToDecimal(dtTmp.Rows[i5]["totalsum_mny"].ToString());
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (strResumeSegment != "")
                                    {
                                        strResumeSegment += ", " + m_strBillNo + "-";
                                    }
                                    else
                                    {
                                        strResumeSegment += m_strBillNo + "-";
                                    }

                                    decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i5]["totalsum_mny"].ToString());
                                    this.m_mthTranslateType(dtTmp.Rows[i5]["billtypeid_int"].ToString().Trim(), ref typeName);
                                }
                                lastNumber = strNumber;
                                lastbill = m_strBillNo;

                                if (i5 == dtTmp.Rows.Count - 1)
                                {
                                    strResumeSegment = strResumeSegment.Substring(0, strResumeSegment.Length - 1) + string.Format("({0}:", typeName) + decTotalTmp.ToString("0.00") + ")";
                                }
                            }
                            #endregion
                        }
                    }

                    DataRow dr = dtbTemp.NewRow();
                    dr["totalsum"] = m_decConvertToDecimal(dtbCheckOut.Compute("sum(totalsum_mny)", "status_int = -1 or status_int = 2"));
                    dr["returnmny"] = m_decConvertToDecimal(dtbCheckOut.Compute("sum(totalsum_mny)", "status_int = 0"));
                    dr["resumemny"] = m_decConvertToDecimal(dtbCheckOut.Compute("sum(totalsum_mny)", "status_int = 3"));
                    dr["actualIncome"] = m_decConvertToDecimal(dr["totalsum"]) + m_decConvertToDecimal(dr["returnmny"]) + m_decConvertToDecimal(dr["resumemny"]);
                    dr["cashtotalmny"] = m_decConvertToDecimal(dtbCheckOut.Compute("sum(totalsum_mny)", "paytype_int = 0"));
                    dr["banktotalmny"] = m_decConvertToDecimal(dtbCheckOut.Compute("sum(totalsum_mny)", "paytype_int = 1"));
                    dr["checktotalmny"] = m_decConvertToDecimal(dtbCheckOut.Compute("sum(totalsum_mny)", "paytype_int = 2"));
                    dr["ictotalmny"] = m_decConvertToDecimal(dtbCheckOut.Compute("sum(totalsum_mny)", "paytype_int not in (0, 1, 2)"));
                    dr["pieces"] = dtbCheckOut.Compute("count(billno_vchr)", "status_int = 2 or status_int = 3 or (status_int = -1 and balance_dat < extentdate_dat)");
                    dr["normalbillno"] = strNormalSegment;
                    dr["cancelbillno"] = strCancelSegment;
                    dr["resumebillno"] = strResumeSegment;
                    dtbTemp.Rows.Add(dr);
                    dtbTemp.AcceptChanges();
                    dtbCheckOut = dtbTemp;
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 判断发票是否领用
        /// <summary>
        /// 判断发票是否领用
        /// </summary>
        /// <param name="p_strBillNo"></param>
        /// <param name="p_strOperatorID"></param>
        /// <param name="p_intBillType"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnCheckIsOccupy(string p_strBillNo, string p_strOperatorID, int p_intBillType)
        {
            long lngRes = 0;
            bool blnFlag = false;
            string strLetter = System.Text.RegularExpressions.Regex.Replace(p_strBillNo, @"[^A-Za-z]*", "");
            Int64 intNumeric = 0;
            Int64.TryParse(System.Text.RegularExpressions.Regex.Replace(p_strBillNo, @"^[A-Za-z]*", ""), out intNumeric);
            try
            {
                string strSQL = @"select appid_chr, invoicenofrom_vchr, invoicenoto_vchr, apply_dat,
       appuserid_chr, operatorid_chr, canceluserid_chr, status_int,
       cancel_dat, invoicetypeid_int
  from t_opr_opinvoiceman
 where invoicetypeid_int = ?
   and appuserid_chr = ?
   and invoicenofrom_vchr like ?
   and status_int = 0";
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                objDPArr[0].Value = p_intBillType;
                objDPArr[1].Value = p_strOperatorID;
                objDPArr[2].Value = strLetter + "%";
                DataTable dtbTemp = null;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbTemp, objDPArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbTemp != null)
                {
                    DataRow dr = null;
                    Int64 intfrom = 0;
                    Int64 intto = 0;
                    for (int i1 = 0; i1 < dtbTemp.Rows.Count; i1++)
                    {
                        dr = dtbTemp.Rows[i1];
                        Int64.TryParse(System.Text.RegularExpressions.Regex.Replace(dr["invoicenofrom_vchr"].ToString().Trim(), "^[A-Za-z]*", ""), out intfrom);
                        Int64.TryParse(System.Text.RegularExpressions.Regex.Replace(dr["invoicenoto_vchr"].ToString().Trim(), "^[A-Za-z]*", ""), out intto);
                        if (intNumeric >= intfrom && intNumeric <= intto)
                        {
                            blnFlag = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return blnFlag;
        }
        #endregion
    }
}