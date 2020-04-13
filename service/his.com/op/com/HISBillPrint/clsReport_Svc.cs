using System;
using System.Collections.Generic;
using System.Text;
using System.EnterpriseServices;
using System.Data;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.billprint
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsReport_Svc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsReport_Svc()
        {
            //
            // TODO: 在此添加构造函数逻辑
            //
        }

        #region 获取往来结算阶段性汇总数据
        /// <summary>
        /// 获取往来结算阶段性汇总数据
        /// </summary>
        /// <param name="flag">票据类型 1-往来结算票据 2-收费统一票据</param>
        /// <param name="p_strBillNoStart"></param>
        /// <param name="p_strBillNoEnd"></param>
        /// <param name="dtbTotalSum"></param>
        /// <param name="dtbItemSum"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetSummaryData(string flag, string p_strBillNoStart, string p_strBillNoEnd, out DataTable dtbTotalSum, out DataTable dtbItemSum)
        {
            long lngRes = 0;
            dtbTotalSum = null;
            dtbItemSum = null;
            DataTable dtbTemp = null;
            DataRow dr = null;
            string strSQL = string.Empty;

            try
            {
                string header = System.Text.RegularExpressions.Regex.Replace(p_strBillNoStart, @"[^A-Za-z]*", "");
                Int64 intheader = Int64.Parse(System.Text.RegularExpressions.Regex.Replace(p_strBillNoStart, @"^[A-Za-z]*", ""));
                Int64 intend = Int64.Parse(System.Text.RegularExpressions.Regex.Replace(p_strBillNoEnd, @"^[A-Za-z]*", ""));
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;

                strSQL = @"select a.seqid_chr, a.itemid_chr, a.itemcode_vchr, a.itemname_vchr,
       a.execdeptid_chr, a.execdeptcode_chr, a.itemunit_chr, a.tolqty_dec,
       a.itemprice_mny, a.tolprice_mny, a.execdeptname_chr
  from t_opr_mainbillde a, t_opr_mainbill b
 where a.seqid_chr = b.seqid_chr
   and b.status_int <> 1
   and billtypeid_int = ?
   and balanceflag_int = 1
   and to_number (replace (b.billno_vchr, ?, '')) between ? and ?
   and b.billno_vchr like ?";
                objHRPSvc.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.Int32;
                objDPArr[0].Value = int.Parse(flag);
                objDPArr[1].DbType = DbType.String;
                objDPArr[1].Value = header;
                objDPArr[2].DbType = DbType.Int64;
                objDPArr[2].Value = intheader;
                objDPArr[3].DbType = DbType.Int64;
                objDPArr[3].Value = intend;
                objDPArr[4].DbType = DbType.String;
                objDPArr[4].Value = header + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbItemSum, objDPArr);

                strSQL = @"select seqid_chr, billno_vchr, recorddate_dat, t_opr_mainbill.status_int, payer_chr,
       totalsum_mny, balance_dat, advicenoteno_chr, paytype_int, notes_chr,
       operemp_chr, payee_chr, billtypeid_int, billdate_dat, balanceflag_int,
       sbsum_mny, t_bse_employee.lastname_vchr
  from t_opr_mainbill, t_bse_employee
 where t_opr_mainbill.operemp_chr = t_bse_employee.empid_chr(+)
   and t_opr_mainbill.status_int <> 1
   and billtypeid_int = ?
   and balanceflag_int = 1
   and to_number (replace (billno_vchr, ?, '')) between ? and ?
   and billno_vchr like ?";
                objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(5, out objDPArr);
                objDPArr[0].DbType = DbType.Int32;
                objDPArr[0].Value = int.Parse(flag);
                objDPArr[1].DbType = DbType.String;
                objDPArr[1].Value = header;
                objDPArr[2].DbType = DbType.Int64;
                objDPArr[2].Value = intheader;
                objDPArr[3].DbType = DbType.Int64;
                objDPArr[3].Value = intend;
                objDPArr[4].DbType = DbType.String;
                objDPArr[4].Value = header + "%";
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbTotalSum, objDPArr);
                objHRPSvc.Dispose();

                //生成项目汇总数据
                dtbTemp = new DataTable();
                dtbTemp.Columns.Add("itemname");
                dtbTemp.Columns.Add("itemtotalmny");
                if (dtbItemSum != null)
                {
                    System.Collections.Hashtable hasAdded = new System.Collections.Hashtable();
                    for (int i1 = 0; i1 < dtbItemSum.Rows.Count; i1++)
                    {
                        dr = dtbItemSum.Rows[i1];
                        if (!hasAdded.ContainsKey(dr["ITEMID_CHR"].ToString()))
                        {
                            hasAdded.Add(dr["ITEMID_CHR"].ToString(), dr["ITEMNAME_VCHR"].ToString());
                            dtbTemp.Rows.Add(new object[] { dr["ITEMNAME_VCHR"].ToString(), dtbItemSum.Compute("sum(tolprice_mny)", "ITEMID_CHR = '" + dr["ITEMID_CHR"].ToString() + "'") });
                        }
                    }
                }
                dtbTemp.AcceptChanges();
                dtbItemSum = dtbTemp;

                //生成票据汇总数据
                dtbTemp = new DataTable();
                dtbTemp.Columns.Add("billstart");
                dtbTemp.Columns.Add("billend");
                dtbTemp.Columns.Add("totalnum");
                dtbTemp.Columns.Add("returnbill");
                dtbTemp.Columns.Add("returnnum");
                dtbTemp.Columns.Add("bandbill");
                dtbTemp.Columns.Add("bandnum");
                dtbTemp.Columns.Add("operbillemp");
                if (lngRes > 0 && dtbTotalSum != null)
                {
                    DataView dv = dtbTotalSum.DefaultView;
                    dv.Sort = "billno_vchr";
                    dtbTotalSum = dv.ToTable();

                    dr = dtbTemp.NewRow();
                    string strCancelSegment = string.Empty;//作废起止票据号码
                    string strBandSegment = string.Empty;//空白票据号码

                    if (dtbTotalSum.Rows.Count > 0)
                    {
                        DataRow dr0 = dtbTotalSum.Rows[0];

                        dr["billstart"] = p_strBillNoStart;
                        dr["billend"] = p_strBillNoEnd;
                        dr["totalnum"] = intend - intheader + 1;

                        #region 生成作废起止票据号码
                        DataView dvTmp = dtbTotalSum.DefaultView;
                        dvTmp.RowFilter = "status_int = 0";
                        dvTmp.Sort = "billno_vchr";
                        DataTable dtTmp = dvTmp.ToTable();

                        string lastbill = string.Empty;
                        string lastNumber = string.Empty;
                        decimal decTotalTmp = 0;
                        string m_strBillNo = string.Empty;
                        string strNumber = string.Empty;
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
                                        strCancelSegment = strCancelSegment.Substring(0, strCancelSegment.Length - 1) + "(" + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
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
                                            strCancelSegment += lastbill + "(" + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
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
                                //this.m_mthTranslateType(dtTmp.Rows[i4]["billtypeid_int"].ToString().Trim(), ref typeName);
                            }
                            lastNumber = strNumber;
                            lastbill = m_strBillNo;

                            if (i4 == dtTmp.Rows.Count - 1)
                            {
                                strCancelSegment = strCancelSegment.Substring(0, strCancelSegment.Length - 1) + "(" + decTotalTmp.ToString("0.00") + ")";
                            }
                        }
                        #endregion

                        dr["returnbill"] = strCancelSegment;
                        dr["returnnum"] = dtTmp.Rows.Count;

                        #region 生成空白票据号码
                        dvTmp.RowFilter = "totalsum_mny = 0";
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
                                    if (strBandSegment.Contains(lastbill) && i5 != dtTmp.Rows.Count - 1)
                                    {
                                        strBandSegment = strBandSegment.Substring(0, strBandSegment.Length - 1) + "(" + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
                                        decTotalTmp = Convert.ToDecimal(dtTmp.Rows[i5]["totalsum_mny"].ToString());
                                    }
                                    else
                                    {
                                        if (Int64.Parse(lastNumber) + 1 == Int64.Parse(strNumber))
                                        {
                                            decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i5]["totalsum_mny"].ToString());
                                            strBandSegment += m_strBillNo + "-";
                                        }
                                        else
                                        {
                                            strBandSegment += lastbill + "(" + decTotalTmp.ToString("0.00") + "), " + m_strBillNo + "-";
                                            decTotalTmp = Convert.ToDecimal(dtTmp.Rows[i5]["totalsum_mny"].ToString());
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (strBandSegment != "")
                                {
                                    strBandSegment += ", " + m_strBillNo + "-";
                                }
                                else
                                {
                                    strBandSegment += m_strBillNo + "-";
                                }

                                decTotalTmp += Convert.ToDecimal(dtTmp.Rows[i5]["totalsum_mny"].ToString());
                                //this.m_mthTranslateType(dtTmp.Rows[i5]["billtypeid_int"].ToString().Trim(), ref typeName);
                            }
                            lastNumber = strNumber;
                            lastbill = m_strBillNo;

                            if (i5 == dtTmp.Rows.Count - 1)
                            {
                                strBandSegment = strBandSegment.Substring(0, strBandSegment.Length - 1) + "(" + decTotalTmp.ToString("0.00") + ")";
                            }
                        }
                        #endregion

                        dr["bandbill"] = strBandSegment;
                        dr["bandnum"] = dtTmp.Rows.Count;
                        dr["operbillemp"] = dr0["lastname_vchr"].ToString();

                        dtbTemp.Rows.Add(dr);
                    }
                }
                dtbTemp.AcceptChanges();
                dtbTotalSum = dtbTemp;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取收入日志数据
        /// <summary>
        /// 获取收入日志数据
        /// </summary>
        /// <param name="mode">0 按收费时间 =1 按日结时间</param>
        /// <param name="strBeginDate"></param>
        /// <param name="strEndDate"></param>
        /// <param name="strEmpID"></param>
        /// <param name="dtbSource"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetItemIncomeLog(int mode, string strBeginDate, string strEndDate, string strEmpID, out DataTable dtbSource)
        {
            long lngRes = 0;
            dtbSource = null;
            string strSQL = string.Empty;

            try
            {
                strSQL = @"select a.seqid_chr,
       a.billno_vchr,
       a.status_int,
       a.payer_chr,
       a.operemp_chr,
       a.billdate_dat,
       a.billtypeid_int,
       a.extentdate_dat,
       b.itemcatid_chr,
       b.tolfee_mny
  from t_opr_mainbill a, t_opr_billcalcsumde b
 where a.seqid_chr = b.seqid_chr
   and a.status_int <> 1{0}
   and a.{1} between
       to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
       to_date(?, 'yyyy-mm-dd hh24:mi:ss'){2}";

                string strByEmpid = string.Empty;
                string strByDate = string.Empty;
                string strRecFlag = string.Empty;
                if (strEmpID != "")
                {
                    strByEmpid = @"
   and a.operemp_chr = ?";
                }

                if (mode == 0)
                {
                    strByDate = "extentdate_dat";
                }
                else
                {
                    strByDate = "balance_dat";
                    strRecFlag = @"
   and a.balanceflag_int = 1";
                }

                clsHRPTableService objHRPSvc = new clsHRPTableService();                
                IDataParameter[] objDPArr = null;
                strSQL = string.Format(strSQL, strByEmpid, strByDate, strRecFlag);
                if (strEmpID != "")
                {
                    objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].Value = strEmpID;
                    objDPArr[1].Value = strBeginDate;
                    objDPArr[2].Value = strEndDate;
                }
                else
                {
                    objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].Value = strBeginDate;
                    objDPArr[1].Value = strEndDate;
                }
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbSource, objDPArr);
                objHRPSvc.Dispose();

                if (lngRes > 0 && dtbSource != null)
                {
                    DataView dv = dtbSource.DefaultView;
                    dv.Sort = "extentdate_dat, seqid_chr";
                    dtbSource = dv.ToTable();
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

        #region 收费员票据月汇总表
        /// <summary>
        /// 收费员票据月汇总表
        /// </summary>
        /// <param name="datStart"></param>
        /// <param name="datEnd"></param>
        /// <param name="strEmpid"></param>
        /// <param name="dtbResult"></param>
        [AutoComplete]
        public long m_lngGetMonthlyCollect(DateTime datStart, DateTime datEnd, string strEmpid, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            string strSQL = @"select a.seqid_chr, a.billno_vchr, a.recorddate_dat, a.status_int,
       a.payer_chr, a.totalsum_mny, a.balance_dat, a.advicenoteno_chr,
       a.paytype_int, a.notes_chr, a.operemp_chr, a.payee_chr,
       a.billtypeid_int, a.billdate_dat, a.balanceflag_int, a.sbsum_mny,
       a.extentdate_dat, b.lastname_vchr
  from t_opr_mainbill a, t_bse_employee b
 where a.operemp_chr = b.empid_chr(+)
   and a.status_int <> 1
   and a.balance_dat between ? and ?
   and a.balanceflag_int = 1";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;

                if (string.IsNullOrEmpty(strEmpid))
                {
                    objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = datStart;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = datEnd;
                }
                else
                {
                    strSQL = string.Concat(strSQL, " and a.operemp_chr = ?");
                    objHRPSvc.CreateDatabaseParameter(3, out objDPArr);
                    objDPArr[0].DbType = DbType.DateTime;
                    objDPArr[0].Value = datStart;
                    objDPArr[1].DbType = DbType.DateTime;
                    objDPArr[1].Value = datEnd;
                    objDPArr[2].Value = strEmpid;
                }

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, objDPArr);
                objHRPSvc.Dispose();
                
                DataTable dtbTemp = new DataTable();
                dtbTemp.Columns.Add("colstatus", typeof(string));
                dtbTemp.Columns.Add("colbilllno", typeof(string));
                dtbTemp.Columns.Add("colpieces", typeof(int));
                dtbTemp.Columns.Add("coltotalmny", typeof(decimal));
                dtbTemp.Columns.Add("colsbmny", typeof(decimal));
                dtbTemp.Columns.Add("colacctmny", typeof(decimal));
                dtbTemp.Columns.Add("colpayee", typeof(string));
                dtbTemp.Columns.Add("colempid", typeof(string));
                if (lngRes > 0 && dtbResult != null)
                {
                    DataView dv = dtbResult.DefaultView;
                    dv.Sort = "operemp_chr, status_int, billno_vchr";
                    dtbResult = dv.ToTable();

                    System.Collections.Hashtable hasEmp = new System.Collections.Hashtable();
                    System.Collections.Hashtable hasLetter = new System.Collections.Hashtable();
                    string strOperEmp = string.Empty;
                    
                    DataRow dr = null;
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        strOperEmp = dtbResult.Rows[i1]["operemp_chr"].ToString().Trim();
                        if (!hasEmp.ContainsKey(strOperEmp) && strOperEmp != "")
                        {
                            hasEmp.Add(strOperEmp, strOperEmp);

                            DataView dv2 = dtbResult.DefaultView;
                            dv2.RowFilter = "operemp_chr = '" + strOperEmp + "'";
                            DataTable dtbTmp2 = dv2.ToTable();
                            
                            //正常票据
                            this.m_mthGenerateContent("2", "正常", dtbResult.Rows[i1]["lastname_vchr"].ToString().Trim(), strOperEmp, ref dtbTmp2, ref dr, ref dtbTemp);

                            //原始票据
                            this.m_mthGenerateContent("-1", "原始", dtbResult.Rows[i1]["lastname_vchr"].ToString().Trim(), strOperEmp, ref dtbTmp2, ref dr, ref dtbTemp);

                            //作废票据
                            this.m_mthGenerateContent("0", "退费", dtbResult.Rows[i1]["lastname_vchr"].ToString().Trim(), strOperEmp, ref dtbTmp2, ref dr, ref dtbTemp);

                            //恢复票据
                            this.m_mthGenerateContent("3", "恢复", dtbResult.Rows[i1]["lastname_vchr"].ToString().Trim(), strOperEmp, ref dtbTmp2, ref dr, ref dtbTemp);
                        }
                    }
                }
                dtbTemp.AcceptChanges();
                dtbResult = dtbTemp;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 生成报表内容
        /// </summary>
        /// <param name="status">状态值</param>
        /// <param name="strDescription">状态描述</param>
        /// <param name="strChargeEmpName">收费员姓名</param>
        /// <param name="strChargeEmpID">收费员ID</param>
        /// <param name="dtbTmp2">原始数据源</param>
        /// <param name="dr"></param>
        /// <param name="dtbTemp">返回数据源</param>
        public void m_mthGenerateContent(string status, string strDescription, string strChargeEmpName, string strChargeEmpID, ref DataTable dtbTmp2, ref DataRow dr, ref DataTable dtbTemp)
        {
            string m_strBillNo = string.Empty;
            string strInitLetter = string.Empty;
            string strNumber = string.Empty;

            System.Collections.Hashtable hasLetter = new System.Collections.Hashtable();
            DataRow[] drArray = dtbTmp2.Select("status_int = " + status, "billno_vchr");
            string lastbill = string.Empty;
            string lastNumber = string.Empty;
            for (int i2 = 0; i2 < drArray.Length; i2++)
            {
                //提取首字母
                m_strBillNo = drArray[i2]["billno_vchr"].ToString();
                strInitLetter = System.Text.RegularExpressions.Regex.Replace(m_strBillNo, @"[^A-Za-z]*", "");
                strNumber = System.Text.RegularExpressions.Regex.Replace(m_strBillNo, @"^[A-Za-z]*", "");

                if (!hasLetter.ContainsKey(strInitLetter))
                {
                    hasLetter.Add(strInitLetter, strInitLetter);
                    if (lastbill != "")
                    {
                        dr["colbilllno"] = this.m_strConcat(dr["colbilllno"].ToString(), lastbill);
                        dtbTemp.Rows.Add(dr);
                    }
                    dr = dtbTemp.NewRow();
                    dr["colstatus"] = strDescription;
                    dr["colbilllno"] = m_strBillNo + "-";
                    dr["colpieces"] = 1;
                    dr["coltotalmny"] = m_decConvertToDecimal(drArray[i2]["totalsum_mny"]);
                    dr["colsbmny"] = m_decConvertToDecimal(drArray[i2]["sbsum_mny"]);
                    dr["colacctmny"] = m_decConvertToDecimal(dr["coltotalmny"]) - m_decConvertToDecimal(dr["colsbmny"]);
                    dr["colpayee"] = strChargeEmpName;
                    dr["colempid"] = strChargeEmpID;
                }
                else
                {
                    if (Int64.Parse(lastNumber) + 1 == Int64.Parse(strNumber))
                    {
                        dr["colpieces"] = m_decConvertToDecimal(dr["colpieces"]) + 1;
                        dr["coltotalmny"] = m_decConvertToDecimal(dr["coltotalmny"]) + m_decConvertToDecimal(drArray[i2]["totalsum_mny"]);
                        dr["colsbmny"] = m_decConvertToDecimal(dr["colsbmny"]) + m_decConvertToDecimal(drArray[i2]["sbsum_mny"]);
                        dr["colacctmny"] = m_decConvertToDecimal(dr["coltotalmny"]) - m_decConvertToDecimal(dr["colsbmny"]);
                    }
                    else
                    {
                        dr["colbilllno"] = this.m_strConcat(dr["colbilllno"].ToString(), lastbill);
                        dtbTemp.Rows.Add(dr);

                        dr = dtbTemp.NewRow();
                        dr["colstatus"] = strDescription;
                        dr["colbilllno"] = m_strBillNo + "-";
                        dr["colpieces"] = 1;
                        dr["coltotalmny"] = m_decConvertToDecimal(drArray[i2]["totalsum_mny"]);
                        dr["colsbmny"] = m_decConvertToDecimal(drArray[i2]["sbsum_mny"]);
                        dr["colacctmny"] = m_decConvertToDecimal(dr["coltotalmny"]) - m_decConvertToDecimal(dr["colsbmny"]);
                        dr["colpayee"] = strChargeEmpName;
                        dr["colempid"] = strChargeEmpID;
                    }
                }

                lastNumber = strNumber;
                lastbill = m_strBillNo;
            }
            if (dr.RowState != DataRowState.Added)
            {
                dr["colbilllno"] = this.m_strConcat(dr["colbilllno"].ToString(), lastbill);
                dtbTemp.Rows.Add(dr);
            }
        }

        public string m_strConcat(string strSourceString, string strAppendString)
        {
            if (strSourceString.Contains(strAppendString))
            {
                return strSourceString.Replace("-", "");
            }
            else
            {
                return string.Concat(strSourceString, strAppendString);
            }
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
    }
}
