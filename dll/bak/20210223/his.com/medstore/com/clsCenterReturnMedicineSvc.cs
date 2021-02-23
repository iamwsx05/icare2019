using System;
using System.EnterpriseServices;
using System.Data;
using System.Text;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.Collections;
using System.Collections.Generic;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 中心药房退药业务
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsCenterReturnMedicineSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 查询病区信息
        /// <summary>
        /// 查询已摆药和退药信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strMedStoreid"></param>
        /// <param name="dtmBegin"></param>
        /// <param name="dtmEnd"></param>
        /// <param name="m_dtReuslt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAreaInfomation(out DataTable m_dtReuslt)
        {
            m_dtReuslt = null;
            long lngRes = 0;

            string strSQL = @"select deptid_chr, modify_dat, deptname_vchr, category_int,
                               inpatientoroutpatient_int, operatorid_chr, address_vchr, pycode_chr,
                               attributeid, parentid, createdate_dat, status_int, deactivate_dat,
                               wbcode_chr, code_vchr, extendid_vchr, shortno_chr, stdbed_count_int,
                               putmed_int
                          from t_bse_deptdesc 
                         where status_int = 1 
                           and ((inpatientoroutpatient_int = 1 or inpatientoroutpatient_int = 2) or parentid = 0)
                           and attributeid = '0000003'
                      order by code_vchr
";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref m_dtReuslt);
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
        #region 查询已摆药和退药信息
        /// <summary>
        /// 查询已摆药和退药信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strMedStoreid"></param>
        /// <param name="dtmBegin"></param>
        /// <param name="dtmEnd"></param>
        /// <param name="m_dtReuslt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetReturnMedAndPutedMedInfoByConditions(string m_strMedStoreid, DateTime dtmBegin, DateTime dtmEnd, bool p_blnReprint, out DataTable m_dtReuslt)
        {
            m_dtReuslt = null;
            long lngRes = 0;
            //            string strSQL = @"
            //		select 1 as returnflag_int,
            //       a.putmeddetailid_chr,
            //       a.pchargeid_chr,
            //       a.recipeno_int,
            //       a.unitprice_mny,
            //       a.unitprice_mny * a.get_dec as OrgTaltolMny,
            //       a.unit_vchr,
            //       a.unit_vchr as returnunit,
            //       a.get_dec,
            //       -b.amount_dec as returnamount,
            //       a.unitprice_mny * (-b.amount_dec) as ReturnTotalMny,
            //       a.isput_int,
            //       a.pubdate_dat,
            //       c.medicineid_chr,
            //       c.medicinename_vchr,
            //       c.medspec_vchr,
            //       c.assistcode_chr,
            //       c.packqty_dec,
            //       c.ipchargeflg_int,
            //       c.medicinetypeid_chr,
            //       a.medstoreid_chr,
            //       d.deptid_chr as drugstoreid_chr
            //  from t_bih_opr_putmeddetail  a,
            //       t_opr_bih_patientcharge b,
            //       t_bse_medicine          c,
            //       t_bse_medstore          d
            // where a.pchargeid_chr = b.pchargeidorg_chr
            //   and a.medid_chr = c.medicineid_chr(+)
            //   and a.medstoreid_chr = d.medstoreid_chr(+)
            //   and a.isput_int = 1
            //   and a.pubdate_dat between ? and ?
            //   and a.medstoreid_chr = ?
            //   and a.registerid_chr = ?
            //union all
            //select 0 as returnflag_int,
            //       a.putmeddetailid_chr,
            //       a.pchargeid_chr,
            //       a.recipeno_int,
            //       a.unitprice_mny,
            //       a.unitprice_mny * a.get_dec as OrgTaltolMny,
            //       a.unit_vchr,
            //       a.unit_vchr as returnunit,
            //       a.get_dec,
            //       0 as returnamount,
            //       0 as ReturnTotalMny,
            //       a.isput_int,
            //       a.pubdate_dat,
            //       c.medicineid_chr,
            //       c.medicinename_vchr,
            //       c.medspec_vchr,
            //       c.assistcode_chr,
            //       c.packqty_dec,
            //       c.ipchargeflg_int,
            //       c.medicinetypeid_chr,
            //       a.medstoreid_chr,
            //       d.deptid_chr as drugstoreid_chr
            //  from t_bih_opr_putmeddetail a, t_bse_medicine c, t_bse_medstore d
            // where not exists (select 1
            //          from t_opr_bih_patientcharge b
            //         where a.pchargeid_chr = b.pchargeidorg_chr)
            //   and a.medid_chr = c.medicineid_chr(+)
            //   and a.medstoreid_chr = d.medstoreid_chr(+)
            //   and a.isput_int = 1
            //   and a.pubdate_dat between ? and ?
            //   and a.medstoreid_chr = ?
            //   and a.registerid_chr = ?";
            string strSQL = @"select 1 as returnflag_int,
                                   a.putmeddetailid_chr,
                                   b.pchargeid_chr,
                                   a.recipeno_int,
                                   a.unitprice_mny,
                                   a.unitprice_mny * a.get_dec as OrgTaltolMny,
                                   a.unit_vchr,
                                   a.unit_vchr as returnunit,
                                   a.get_dec,
                                   b.amount_dec as returnamount,
                                   a.unitprice_mny * (b.amount_dec) as ReturnTotalMny,
                                   a.isput_int,
                                   a.pubdate_dat,
                                   c.medicineid_chr,
                                   c.medicinename_vchr,
                                   c.medspec_vchr,
                                   c.assistcode_chr,
                                   c.packqty_dec,
                                   c.ipchargeflg_int,
                                   c.medicinetypeid_chr,
                                   a.medstoreid_chr,
                                   d.deptid_chr as drugstoreid_chr,
                                   b.returnmedbillno, 
                                   a.examreturnmed_int,
                                   a.examreturnmed_dat,
                                   a.registerid_chr,
                                   e.deptname_vchr as areaname,
                                   f.code_chr,
                                   h.lastname_vchr,
                                   g.inpatientid_chr,
                                   a.areaid_chr,
                                   a.registerid_chr,
                                   b.manyreturnmedill_int,
                                   b.isconfirmrefundment,
                                   decode(nvl(a.pretestdays,0),0,'','是') as isPretest 
                              from t_bih_opr_putmeddetail   a,
                                   t_opr_bih_patientcharge  b,
                                   t_bse_medicine           c,
                                   t_bse_medstore           d,
                                   t_bse_deptdesc           e,
                                   t_bse_bed                f,
                                   t_opr_bih_register       g,
                                   t_opr_bih_registerdetail h
                             where a.pchargeid_chr = b.pchargeidorg_chr
                               and a.medid_chr = c.medicineid_chr(+)
                               and a.medstoreid_chr = d.medstoreid_chr(+)
                               and a.isput_int = 1
                               and b.putmedicineflag_int <> -1
                               and b.status_int=1
                               and g.status_int=1
                               and e.status_int=1
                               and a.status_int=1
                               and (b.isconfirmrefundment is null or b.isconfirmrefundment in (1,2)) ";
            if (p_blnReprint)
            {
                strSQL += " and (a.examreturnmed_dat between to_date(?,'yyyy-MM-dd hh24:mi:ss') and to_date(?,'yyyy-MM-dd hh24:mi:ss')) ";
            }
            else
            {
                strSQL += " and (b.chargeactive_dat between to_date(?,'yyyy-MM-dd hh24:mi:ss') and to_date(?,'yyyy-MM-dd hh24:mi:ss')) ";
            }
            strSQL += @" and a.medstoreid_chr = ?
                         and a.areaid_chr = e.deptid_chr(+)
                         and a.registerid_chr = g.registerid_chr
                         and g.registerid_chr = h.registerid_chr
                         and h.registerid_chr = f.bihregisterid_chr(+)
                       order by e.deptname_vchr,a.registerid_chr,b.returnmedbillno desc
                        ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] objLisAddItemRefArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out objLisAddItemRefArr);
                objLisAddItemRefArr[0].Value = dtmBegin.ToString("yyyy-MM-dd HH:mm:ss");
                objLisAddItemRefArr[1].Value = dtmEnd.ToString("yyyy-MM-dd HH:mm:ss");
                objLisAddItemRefArr[2].Value = m_strMedStoreid;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_dtReuslt, objLisAddItemRefArr);
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
        #region 更新退药信息
        /// <summary>
        /// 更新退药信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_strEmpid"></param>
        /// <param name="m_dtbData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateReturnMedicine(string m_strSecondLevelMode, string m_strEmpid, DataTable m_dtbData)
        {

            long lngRes = 0;
            List<DataRow> m_objListRow = new List<DataRow>(m_dtbData.Rows.Count);
            foreach (DataRow dr in m_dtbData.Rows)
            {
                m_objListRow.Add(dr);
            }
            System.Data.IDataParameter[] objLisAddItemRefArr = null;
            DataTable m_dtReuslt = null;
            string Sql = string.Empty;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            DbType[] dbTypes = null;
            object[][] objValues = null;
            if (m_strSecondLevelMode == "1")
            {
                Sql = @"select a.seriesid_int, a.iprealgross_int, a.oprealgross_int
                          from t_ds_storage_detail a
                         where a.drugstoreid_chr = ?
                           and a.medicineid_chr = ?
                         order by a.validperiod_dat desc";

                dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Double, DbType.Double, DbType.Int64 };
                objValues = new object[5][];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[m_objListRow.Count];
                }
                for (int k1 = 0; k1 < m_objListRow.Count; k1++)
                {
                    if (m_objListRow[k1]["ipchargeflg_int"].ToString() == "0")
                    {
                        objValues[0][k1] = Convert.ToDouble(m_objListRow[k1]["returnamount"]) * Convert.ToDouble(m_objListRow[k1]["packqty_dec"]);
                        objValues[1][k1] = Convert.ToDouble(m_objListRow[k1]["returnamount"]);
                        objValues[2][k1] = Convert.ToDouble(m_objListRow[k1]["returnamount"]) * Convert.ToDouble(m_objListRow[k1]["packqty_dec"]);
                        objValues[3][k1] = Convert.ToDouble(m_objListRow[k1]["returnamount"]);
                    }
                    else
                    {
                        objValues[0][k1] = Convert.ToDouble(m_objListRow[k1]["returnamount"]);
                        objValues[1][k1] = Math.Round(Convert.ToDouble(m_objListRow[k1]["returnamount"]) / Convert.ToDouble(m_objListRow[k1]["packqty_dec"]), 4); ;
                        objValues[2][k1] = Convert.ToDouble(m_objListRow[k1]["returnamount"]);
                        objValues[3][k1] = Math.Round(Convert.ToDouble(m_objListRow[k1]["returnamount"]) / Convert.ToDouble(m_objListRow[k1]["packqty_dec"]), 4); ;
                    }
                    objLisAddItemRefArr = null;
                    m_dtReuslt = null;
                    objHRPSvc.CreateDatabaseParameter(2, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].Value = m_objListRow[k1]["drugstoreid_chr"].ToString();
                    objLisAddItemRefArr[1].Value = m_objListRow[k1]["medicineid_chr"].ToString();
                    objHRPSvc.lngGetDataTableWithParameters(Sql, ref m_dtReuslt, objLisAddItemRefArr);
                    objValues[4][k1] = Convert.ToInt64(m_dtReuslt.Rows[0]["seriesid_int"]);     // 库存补到最新有效期第一个药品
                }
                Sql = @"update t_ds_storage_detail a
                           set a.iprealgross_int      = a.iprealgross_int - ?,
                               a.oprealgross_int      = a.oprealgross_int - ?,
                               a.ipavailablegross_num = a.ipavailablegross_num - ?,
                               a.opavailablegross_num = a.opavailablegross_num - ?
                         where a.seriesid_int = ?";
                try
                {
                    long lngRecEff = -1;
                    lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(Sql, objValues, ref lngRecEff, dbTypes);
                    if (lngRecEff <= 0)
                        throw new Exception("更新不了相应药品库存明细！");
                    objHRPSvc.Dispose();
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }

                dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.String, DbType.String };
                objValues = new object[4][];
                for (int j = 0; j < objValues.Length; j++)
                {
                    objValues[j] = new object[m_objListRow.Count];//初始化
                }

                for (int k1 = 0; k1 < m_objListRow.Count; k1++)
                {
                    if (m_objListRow[k1]["ipchargeflg_int"].ToString() == "0")
                    {
                        objValues[0][k1] = Convert.ToDouble(m_objListRow[k1]["returnamount"]) * Convert.ToDouble(m_objListRow[k1]["packqty_dec"]);
                        objValues[1][k1] = Convert.ToDouble(m_objListRow[k1]["returnamount"]);
                    }
                    else
                    {
                        objValues[0][k1] = Convert.ToDouble(m_objListRow[k1]["returnamount"]);
                        objValues[1][k1] = Math.Round(Convert.ToDouble(m_objListRow[k1]["returnamount"]) / Convert.ToDouble(m_objListRow[k1]["packqty_dec"]), 4);
                    }
                    objValues[2][k1] = m_objListRow[k1]["drugstoreid_chr"].ToString();
                    objValues[3][k1] = m_objListRow[k1]["medicineid_chr"].ToString();
                }
                Sql = @"update t_ds_storage a
                           set a.ipcurrentgross_num = a.ipcurrentgross_num - ?,
                               a.opcurrentgross_num = a.opcurrentgross_num - ?
                         where a.drugstoreid_chr = ?
                           and a.medicineid_chr = ?";
                try
                {
                    long lngRecEff = -1;
                    lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(Sql, objValues, ref lngRecEff, dbTypes);
                    if (lngRecEff <= 0)
                        throw new Exception("更新不了相应药品主表库存！");
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            dbTypes = new DbType[] { DbType.String };
            objValues = new object[1][];
            for (int j1 = 0; j1 < objValues.Length; j1++)
            {
                objValues[j1] = new object[m_objListRow.Count];//初始化
            }

            for (int k2 = 0; k2 < m_objListRow.Count; k2++)
            {
                objValues[0][k2] = m_objListRow[k2]["putmeddetailid_chr"].ToString();
            }
            Sql = @"update t_bih_opr_putmeddetail a
                       set a.examreturnmed_int = 1, a.examreturnmed_dat = sysdate
                     where a.putmeddetailid_chr = ?";
            try
            {
                long lngRecEff = -1;
                lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(Sql, objValues, ref lngRecEff, dbTypes);
                if (lngRes <= 0 || lngRecEff <= 0)
                {
                    throw new Exception("更新退药明细表退药审核标志错误！");
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            dbTypes = new DbType[] { DbType.String, DbType.String };
            objValues = new object[2][];
            for (int j = 0; j < objValues.Length; j++)
            {
                objValues[j] = new object[m_objListRow.Count];
            }

            for (int k1 = 0; k1 < m_objListRow.Count; k1++)
            {
                objValues[0][k1] = m_strEmpid;
                objValues[1][k1] = m_objListRow[k1]["pchargeid_chr"].ToString();
            }
            Sql = @"update t_opr_bih_patientcharge t
                       set t.isconfirmrefundment = 2,
                           t.refundmentchecker   = ?,
                           t.refundmentdate      = sysdate
                     where t.pchargeid_chr = ?";
            try
            {
                long lngRecEff = -1;
                lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(Sql, objValues, ref lngRecEff, dbTypes);
                if (lngRes <= 0 || lngRecEff <= 0)
                {
                    throw new Exception("更新退药明细表退药审核标志错误！");
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            objHRPSvc.Dispose();
            objHRPSvc = null;
            return lngRes;
        }
        #endregion
    }
}
