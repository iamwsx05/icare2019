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
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsShowBillRequired_Svc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsShowBillRequired_Svc()
        {
            //
            // TODO: 在此添加构造函数逻辑
            //
        }

        #region 保存票据所有信息
        /// <summary>
        /// 保存票据所有信息
        /// </summary>
        /// <param name="objMainVO"></param>
        /// <param name="lstBillDetail"></param>
        /// <param name="p_strSeqID">流水号</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveBillInfo(ref clsMainBillInfo_VO objMainVO, ref List<clsBillChargeDetail_VO> lstBillDetail, List<clsBillCalcSumde_VO> lstBillCalcSumde, out string p_strSeqID)
        {
            long lngRes = 0;
            p_strSeqID = "";
            string strSQL = @"select count(*) from t_opr_mainbill a where a.billno_vchr = ? and a.billtypeid_int = ?";
            try
            {
                string SeqID = string.Empty;
                DataTable dtbTemp = null;
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                DbType[] dbtypes = null;
                object[][] objValues = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = objMainVO.m_strBillNo;
                objDPArr[1].Value = objMainVO.m_strBillTypeID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbTemp, objDPArr);
                if (lngRes > 0 && dtbTemp != null)
                {
                    if (Convert.ToInt16(dtbTemp.Rows[0][0].ToString()) > 0)
                    {
                        return -2;
                    }
                }

                objDPArr = null;
                strSQL = @"select seq_billprintseqid.nextval from dual";
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbTemp);
                if (lngRes > 0 && dtbTemp != null)
                {
                    SeqID = dtbTemp.Rows[0][0].ToString();
                    p_strSeqID = SeqID;
                    objMainVO.m_strSeqID = SeqID;
                    for (int i3 = 0; i3 < lstBillDetail.Count; i3++)
                    {
                        lstBillDetail[i3].m_strSeqID = SeqID;
                    }
                }

                strSQL = @"insert into t_opr_mainbill
            (seqid_chr, billno_vchr, recorddate_dat, status_int,
             payer_chr, totalsum_mny, balance_dat, advicenoteno_chr,
             paytype_int, notes_chr, operemp_chr, payee_chr, billtypeid_int,
             billdate_dat, sbsum_mny, extentdate_dat
            )
     values (?, ?, ?, ?,
             ?, ?, ?, ?,
             ?, ?, ?, ?, ?,
             ?, ?, ?
            )";
                objHRPSvc.CreateDatabaseParameter(16, out objDPArr);
                objDPArr[0].Value = SeqID;
                objDPArr[1].Value = objMainVO.m_strBillNo;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = objMainVO.m_datRecDate;
                objDPArr[3].Value = objMainVO.m_intStatus;
                objDPArr[4].Value = objMainVO.m_strPayer;
                objDPArr[5].Value = objMainVO.m_decTotalMny;
                objDPArr[6].Value = DBNull.Value;
                objDPArr[7].Value = objMainVO.m_strAdviceNoteno;
                objDPArr[8].Value = objMainVO.m_strPaymentID;
                objDPArr[9].Value = objMainVO.m_strNotes;
                objDPArr[10].Value = objMainVO.m_strOperEmp;
                objDPArr[11].Value = objMainVO.m_strPayee;
                objDPArr[12].Value = objMainVO.m_strBillTypeID;
                objDPArr[13].DbType = DbType.DateTime;
                objDPArr[13].Value = DateTime.Parse(objMainVO.m_strBillDate);
                objDPArr[14].Value = objMainVO.m_decSbSumMny;
                objDPArr[15].DbType = DbType.DateTime;
                objDPArr[15].Value = DateTime.Parse(objMainVO.m_strExtendDate);
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, objDPArr);

                if (lstBillDetail.Count > 0)
                {
                    strSQL = @"insert into t_opr_mainbillde
            (seqid_chr, itemid_chr, itemcode_vchr, itemname_vchr,
             execdeptid_chr, execdeptcode_chr, itemunit_chr, tolqty_dec,
             itemprice_mny, tolprice_mny, execdeptname_chr
            )
     values (?, ?, ?, ?,
             ?, ?, ?, ?,
             ?, ?, ?
            )";
                    dbtypes = new DbType[] { DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.String };
                    objValues = new object[11][];
                    for (int i1 = 0; i1 < objValues.Length; i1++)
                    {
                        objValues[i1] = new object[lstBillDetail.Count];
                    }

                    for (int i2 = 0; i2 < lstBillDetail.Count; i2++)
                    {
                        int n = 0;
                        objValues[n++][i2] = SeqID;
                        objValues[n++][i2] = lstBillDetail[i2].m_strItemID;
                        objValues[n++][i2] = lstBillDetail[i2].m_strItemCode;
                        objValues[n++][i2] = lstBillDetail[i2].m_strItemName;
                        objValues[n++][i2] = lstBillDetail[i2].m_strExecDeptID;
                        objValues[n++][i2] = lstBillDetail[i2].m_strExecDeptCode;
                        objValues[n++][i2] = lstBillDetail[i2].m_strItemUnit;
                        objValues[n++][i2] = lstBillDetail[i2].m_decTolQty;
                        objValues[n++][i2] = lstBillDetail[i2].m_decItemPrice;
                        objValues[n++][i2] = lstBillDetail[i2].m_decTolPrice;
                        objValues[n++][i2] = lstBillDetail[i2].m_strExecDeptName;
                    }
                    lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(strSQL, objValues, ref lngRecordsAffected, dbtypes);
                }

                if (lstBillCalcSumde != null)
                {
                    if (lstBillCalcSumde.Count > 0)
                    {
                        strSQL = @"insert into t_opr_billcalcsumde
  (seqid_chr, billno_vchr, itemcatid_chr, tolfee_mny)
values
  (?, ?, ?, ?)";
                        dbtypes = new DbType[] { DbType.String, DbType.String, DbType.String, DbType.Decimal };
                        objValues = new object[4][];
                        for (int i3 = 0; i3 < objValues.Length; i3++)
                        {
                            objValues[i3] = new object[lstBillCalcSumde.Count];
                        }

                        for (int i4 = 0; i4 < lstBillCalcSumde.Count; i4++)
                        {
                            int n = 0;
                            objValues[n++][i4] = SeqID;
                            objValues[n++][i4] = lstBillCalcSumde[i4].m_strBillNo;
                            objValues[n++][i4] = lstBillCalcSumde[i4].m_strItemCatID;
                            objValues[n++][i4] = lstBillCalcSumde[i4].m_decTolFeeMny;
                        }
                        lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(strSQL, objValues, ref lngRecordsAffected, dbtypes);
                    }
                }
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

        #region 作废票据
        /// <summary>
        /// 作废票据
        /// </summary>
        /// <param name="p_strSeqID"></param>
        /// <param name="p_lngRecordsAffected"></param>
        /// <param name="strOperEmp"></param>
        /// <param name="SeqID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCancelBillBySeqID(string p_strSeqID, ref long p_lngRecordsAffected, string strOperEmp, out string SeqID)
        {
            long lngRes = 0;
            SeqID = string.Empty;
            p_lngRecordsAffected = -1;

            try
            { 
                DateTime datCurTime = DateTime.Now;

                string strSQL = @"update t_opr_mainbill
   set status_int = -1, extentdate_dat = ?
 where seqid_chr = ? and status_int = 2";

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = datCurTime;
                objDPArr[1].Value = p_strSeqID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref p_lngRecordsAffected, objDPArr);

                strSQL = @"select seq_billprintseqid.nextval from dual";
                DataTable dtbTemp = null;                
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbTemp);
                if (lngRes > 0 && dtbTemp != null)
                {
                    SeqID = dtbTemp.Rows[0][0].ToString();
                }

                strSQL = @"insert into t_opr_mainbill
            (seqid_chr, billno_vchr, recorddate_dat, status_int, payer_chr,
             totalsum_mny, advicenoteno_chr, paytype_int, notes_chr,
             operemp_chr, payee_chr, billtypeid_int, billdate_dat, sbsum_mny,
             extentdate_dat)
   select ?, billno_vchr, recorddate_dat, 0, payer_chr, -totalsum_mny,
          advicenoteno_chr, paytype_int, notes_chr, ?, payee_chr,
          billtypeid_int, billdate_dat, -sbsum_mny, ?
     from t_opr_mainbill
    where seqid_chr = ?";

                objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = SeqID;
                objDPArr[1].Value = strOperEmp;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = datCurTime;
                //objDPArr[2].DbType = DbType.DateTime;
                //objDPArr[2].Value = datCurTime;
                objDPArr[3].Value = p_strSeqID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref p_lngRecordsAffected, objDPArr);

                strSQL = @"insert into t_opr_mainbillde
            (seqid_chr, itemid_chr, itemcode_vchr, itemname_vchr,
             execdeptid_chr, execdeptcode_chr, itemunit_chr, tolqty_dec,
             itemprice_mny, tolprice_mny, execdeptname_chr)
   select ?, itemid_chr, itemcode_vchr, itemname_vchr, execdeptid_chr,
          execdeptcode_chr, itemunit_chr, tolqty_dec, -itemprice_mny,
          -tolprice_mny, execdeptname_chr
     from t_opr_mainbillde
    where seqid_chr = ?";

                objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = SeqID;
                objDPArr[1].Value = p_strSeqID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref p_lngRecordsAffected, objDPArr);

                strSQL = @"insert into t_opr_billcalcsumde
            (seqid_chr, billno_vchr, itemcatid_chr, tolfee_mny)
   select ?, billno_vchr, itemcatid_chr, -tolfee_mny
     from t_opr_billcalcsumde
    where seqid_chr = ?";

                objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = SeqID;
                objDPArr[1].Value = p_strSeqID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref p_lngRecordsAffected, objDPArr);
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

        #region 恢复票据
        /// <summary>
        /// 恢复票据
        /// </summary>
        /// <param name="p_strSeqID"></param>
        /// <param name="p_lngRecordsAffected"></param>
        /// <param name="strOperEmp"></param>
        /// <param name="SeqID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngResumeBillBySeqID(string p_strSeqID, ref long p_lngRecordsAffected, string strOperEmp, out string SeqID)
        {
            long lngRes = 0;
            SeqID = string.Empty;
            p_lngRecordsAffected = -1;

            try
            { 
                DateTime datCurTime = DateTime.Now;

                string strSQL = @"select seq_billprintseqid.nextval from dual";
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                DataTable dtbTemp = null;
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtbTemp);
                if (lngRes > 0 && dtbTemp != null)
                {
                    SeqID = dtbTemp.Rows[0][0].ToString();
                }

                strSQL = @"insert into t_opr_mainbill
            (seqid_chr, billno_vchr, recorddate_dat, status_int, payer_chr,
             totalsum_mny, advicenoteno_chr, paytype_int, notes_chr,
             operemp_chr, payee_chr, billtypeid_int, billdate_dat, sbsum_mny,
             extentdate_dat)
   select ?, billno_vchr, recorddate_dat, 3, payer_chr, -totalsum_mny,
          advicenoteno_chr, paytype_int, notes_chr, ?, payee_chr,
          billtypeid_int, billdate_dat, -sbsum_mny, ?
     from t_opr_mainbill
    where seqid_chr = ?";

                objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out objDPArr);
                objDPArr[0].Value = SeqID;
                objDPArr[1].Value = strOperEmp;
                objDPArr[2].DbType = DbType.DateTime;
                objDPArr[2].Value = datCurTime;
                //objDPArr[2].DbType = DbType.DateTime;
                //objDPArr[2].Value = datCurTime;
                objDPArr[3].Value = p_strSeqID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref p_lngRecordsAffected, objDPArr);

                strSQL = @"insert into t_opr_mainbillde
            (seqid_chr, itemid_chr, itemcode_vchr, itemname_vchr,
             execdeptid_chr, execdeptcode_chr, itemunit_chr, tolqty_dec,
             itemprice_mny, tolprice_mny, execdeptname_chr)
   select ?, itemid_chr, itemcode_vchr, itemname_vchr, execdeptid_chr,
          execdeptcode_chr, itemunit_chr, tolqty_dec, -itemprice_mny,
          -tolprice_mny, execdeptname_chr
     from t_opr_mainbillde
    where seqid_chr = ?";

                objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = SeqID;
                objDPArr[1].Value = p_strSeqID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref p_lngRecordsAffected, objDPArr);

                strSQL = @"insert into t_opr_billcalcsumde
            (seqid_chr, billno_vchr, itemcatid_chr, tolfee_mny)
   select ?, billno_vchr, itemcatid_chr, -tolfee_mny
     from t_opr_billcalcsumde
    where seqid_chr = ?";

                objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = SeqID;
                objDPArr[1].Value = p_strSeqID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref p_lngRecordsAffected, objDPArr);
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

        #region 保存收费项目
        /// <summary>
        /// 保存收费项目
        /// </summary>
        /// <param name="objItemID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveChargeItem(clsBillChargeItem_VO objItemID)
        {
            long lngRes = 0;
            string strSQL = string.Empty;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                if (objItemID.m_strItemID == "")
                {
                    strSQL = @"select max(to_number(itemid_chr)) from  t_bse_billitem";
                    DataTable dtTemp = null;
                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtTemp);
                    if (lngRes > 0 && dtTemp != null)
                    {
                        if (dtTemp.Rows.Count == 0)
                        {
                            objItemID.m_strItemID = "0001";
                        }
                        else
                        {
                            if (dtTemp.Rows[0][0] != DBNull.Value)
                            {
                                objItemID.m_strItemID = (Convert.ToInt64(dtTemp.Rows[0][0].ToString()) + 1).ToString();
                            }
                            else
                            {
                                objItemID.m_strItemID = "0001";
                            }
                        }
                    }

                    strSQL = @"insert into t_bse_billitem
            (itemid_chr, itemname_vchr, itemcode_vchr, itemunit_chr,
             itemprice_mny, execdeptid_chr, itemcalctype_chr, itempycode_chr,
             itemwbcode_chr, usercode_chr, ifstop_int, billtypeid_int)
     values (?, ?, ?, ?,
             ?, ?, ?, ?,
             ?, ?, ?, ?)";
                    objHRPSvc.CreateDatabaseParameter(12, out objDPArr);
                    objDPArr[0].Value = objItemID.m_strItemID;
                    objDPArr[1].Value = objItemID.m_strItemName;
                    objDPArr[2].Value = objItemID.m_strItemCode;
                    objDPArr[3].Value = objItemID.m_strItemUnit;
                    objDPArr[4].Value = (string.IsNullOrEmpty(objItemID.m_strItemPrice) ? 0 : decimal.Parse(objItemID.m_strItemPrice));
                    objDPArr[5].Value = objItemID.m_strExecDeptID;
                    objDPArr[6].Value = objItemID.m_strItemCalctype;
                    objDPArr[7].Value = objItemID.m_strPyCode;
                    objDPArr[8].Value = objItemID.m_strWbCode;
                    objDPArr[9].Value = objItemID.m_strUserCode;
                    objDPArr[10].Value = objItemID.m_intIfStop;
                    objDPArr[11].Value = objItemID.m_strBillTypeID;
                }
                else
                {
                    strSQL = @"update t_bse_billitem
   set itemname_vchr = ?,
       itemcode_vchr = ?,
       itemunit_chr = ?,
       itemprice_mny = ?,
       execdeptid_chr = ?,
       itemcalctype_chr = ?,
       itempycode_chr = ?,
       itemwbcode_chr = ?,
       usercode_chr = ?,
       ifstop_int = ?,
       billtypeid_int = ?
 where itemid_chr = ?";
                    objHRPSvc.CreateDatabaseParameter(12, out objDPArr);
                    objDPArr[0].Value = objItemID.m_strItemName;
                    objDPArr[1].Value = objItemID.m_strItemCode;
                    objDPArr[2].Value = objItemID.m_strItemUnit;
                    objDPArr[3].Value = (string.IsNullOrEmpty(objItemID.m_strItemPrice) ? 0 : decimal.Parse(objItemID.m_strItemPrice));
                    objDPArr[4].Value = objItemID.m_strExecDeptID;
                    objDPArr[5].Value = objItemID.m_strItemCalctype;
                    objDPArr[6].Value = objItemID.m_strPyCode;
                    objDPArr[7].Value = objItemID.m_strWbCode;
                    objDPArr[8].Value = objItemID.m_strUserCode;
                    objDPArr[9].Value = objItemID.m_intIfStop;
                    objDPArr[10].Value = objItemID.m_strBillTypeID;
                    objDPArr[11].Value = objItemID.m_strItemID;
                }
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref  lngRecordsAffected, objDPArr);
                lngRes = lngRecordsAffected;
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

        #region 删除项目
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="strItemID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDeleteItem(string strItemID)
        {
            long lngRes = 0;
            if (string.IsNullOrEmpty(strItemID))
            {
                return -1;
            }
            string strSQL = string.Empty;

            try
            {
                strSQL = @"delete from t_bse_billitem where itemid_chr = ?";
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = strItemID;
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, objDPArr);
                lngRes = lngRecordsAffected;
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

        #region 更新票据信息
        /// <summary>
        /// 更新票据信息
        /// </summary>
        /// <param name="objMainVO"></param>
        /// <param name="lstBillDetail"></param>
        /// <param name="p_strSeqID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateChargeFlag(ref clsMainBillInfo_VO objMainVO, ref List<clsBillChargeDetail_VO> lstBillDetail, List<clsBillCalcSumde_VO> lstBillCalcSumde, string p_strSeqID)
        {
            long lngRes = 0;
            if (string.IsNullOrEmpty(p_strSeqID))
            {
                return -1;
            }
            string strSQL = string.Empty;

            try
            {
                strSQL = @"select count(*) from t_opr_mainbill a where a.status_int = 2 and a.billno_vchr = ? and a.billtypeid_int = ?";

                DataTable dtbTemp = null;
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                DbType[] dbtypes = null;
                object[][] objValues = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = objMainVO.m_strBillNo;
                objDPArr[1].Value = objMainVO.m_strBillTypeID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtbTemp, objDPArr);
                if (lngRes > 0 && dtbTemp != null)
                {
                    if (Convert.ToInt16(dtbTemp.Rows[0][0].ToString()) > 0)
                    {
                        return -2;
                    }
                }

                strSQL = @"update t_opr_mainbill
   set billno_vchr = ?,       
       status_int = 2,
       payer_chr = ?,
       totalsum_mny = ?,
       balance_dat = ?,
       advicenoteno_chr = ?,
       paytype_int = ?,
       notes_chr = ?,
       operemp_chr = ?,
       payee_chr = ?,
       billtypeid_int = ?,
       billdate_dat = ?,
       balanceflag_int = 0,
       sbsum_mny = ?,
       extentdate_dat = ?
 where seqid_chr = ?";
                objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(14, out objDPArr);
                objDPArr[0].Value = objMainVO.m_strBillNo;
                //objDPArr[1].DbType = DbType.DateTime;
                //objDPArr[1].Value = objMainVO.m_datRecDate;
                objDPArr[1].Value = objMainVO.m_strPayer;
                objDPArr[2].Value = objMainVO.m_decTotalMny;
                objDPArr[3].Value = DBNull.Value;
                objDPArr[4].Value = objMainVO.m_strAdviceNoteno;
                objDPArr[5].Value = objMainVO.m_strPaymentID;
                objDPArr[6].Value = objMainVO.m_strNotes;
                objDPArr[7].Value = objMainVO.m_strOperEmp;
                objDPArr[8].Value = objMainVO.m_strPayee;
                objDPArr[9].Value = objMainVO.m_strBillTypeID;
                objDPArr[10].DbType = DbType.DateTime;
                objDPArr[10].Value = DateTime.Parse(objMainVO.m_strBillDate);
                objDPArr[11].Value = objMainVO.m_decSbSumMny;
                objDPArr[12].DbType = DbType.DateTime;
                objDPArr[12].Value = DateTime.Parse(objMainVO.m_strExtendDate);
                objDPArr[13].Value = objMainVO.m_strSeqID;
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, objDPArr);
                lngRes = lngRecordsAffected;

                strSQL = @"delete from t_opr_mainbillde where seqid_chr = ?";
                objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = objMainVO.m_strSeqID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, objDPArr);

                if (lstBillDetail.Count > 0)
                {
                    strSQL = @"insert into t_opr_mainbillde
            (seqid_chr, itemid_chr, itemcode_vchr, itemname_vchr,
             execdeptid_chr, execdeptcode_chr, itemunit_chr, tolqty_dec,
             itemprice_mny, tolprice_mny, execdeptname_chr
            )
     values (?, ?, ?, ?,
             ?, ?, ?, ?,
             ?, ?, ?
            )";
                    dbtypes = new DbType[] { DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Decimal, DbType.Decimal, DbType.Decimal, DbType.String };
                    objValues = new object[11][];
                    for (int i1 = 0; i1 < objValues.Length; i1++)
                    {
                        objValues[i1] = new object[lstBillDetail.Count];
                    }

                    for (int i2 = 0; i2 < lstBillDetail.Count; i2++)
                    {
                        int n = 0;
                        objValues[n++][i2] = p_strSeqID;
                        objValues[n++][i2] = lstBillDetail[i2].m_strItemID;
                        objValues[n++][i2] = lstBillDetail[i2].m_strItemCode;
                        objValues[n++][i2] = lstBillDetail[i2].m_strItemName;
                        objValues[n++][i2] = lstBillDetail[i2].m_strExecDeptID;
                        objValues[n++][i2] = lstBillDetail[i2].m_strExecDeptCode;
                        objValues[n++][i2] = lstBillDetail[i2].m_strItemUnit;
                        objValues[n++][i2] = lstBillDetail[i2].m_decTolQty;
                        objValues[n++][i2] = lstBillDetail[i2].m_decItemPrice;
                        objValues[n++][i2] = lstBillDetail[i2].m_decTolPrice;
                        objValues[n++][i2] = lstBillDetail[i2].m_strExecDeptName;
                    }
                    lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(strSQL, objValues, ref lngRecordsAffected, dbtypes);
                }

                if (lstBillCalcSumde != null)
                {
                    if (lstBillCalcSumde.Count > 0)
                    {
                        strSQL = @"insert into t_opr_billcalcsumde
  (seqid_chr, billno_vchr, itemcatid_chr, tolfee_mny)
values
  (?, ?, ?, ?)";
                        dbtypes = new DbType[] { DbType.String, DbType.String, DbType.String, DbType.Decimal };
                        objValues = new object[4][];
                        for (int i3 = 0; i3 < objValues.Length; i3++)
                        {
                            objValues[i3] = new object[lstBillCalcSumde.Count];
                        }

                        for (int i4 = 0; i4 < lstBillCalcSumde.Count; i4++)
                        {
                            int n = 0;
                            objValues[n++][i4] = p_strSeqID;
                            objValues[n++][i4] = lstBillCalcSumde[i4].m_strBillNo;
                            objValues[n++][i4] = lstBillCalcSumde[i4].m_strItemCatID;
                            objValues[n++][i4] = lstBillCalcSumde[i4].m_decTolFeeMny;
                        }
                        lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(strSQL, objValues, ref lngRecordsAffected, dbtypes);
                    }
                }
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

        #region 删除票据信息
        /// <summary>
        /// 删除票据信息
        /// </summary>
        /// <param name="p_strSeqid"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_mthDeleteBill(string p_strSeqid)
        {
            long lngRes = 0;
            string strSQL = string.Empty;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                strSQL = @"delete from t_opr_mainbillde where seqid_chr = ?";
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strSeqid;
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, objDPArr);

                strSQL = @"delete from t_opr_mainbill where seqid_chr = ?";
                objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objDPArr);
                objDPArr[0].Value = p_strSeqid;
                lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, objDPArr);
                lngRes = lngRecordsAffected;
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

        #region 日结
        /// <summary>
        /// 日结
        /// </summary>
        /// <param name="strEmpId"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckOutOfDay(string strEmpId)
        {
            long lngRes = 0;
            string strSQL = @"update t_opr_mainbill
   set balance_dat = ?, balanceflag_int = 1
 where status_int <> 1
   and balanceflag_int = 0
   and operemp_chr = ?";

            try
            { 
                DateTime datCurTime = DateTime.Now;

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].DbType = DbType.DateTime;
                objDPArr[0].Value = datCurTime;
                objDPArr[1].Value = strEmpId;
                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, objDPArr);
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
    }
}