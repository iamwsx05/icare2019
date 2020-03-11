using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using com.digitalwave.iCare.ValueObject;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;
using System.EnterpriseServices;

namespace com.digitalwave.iCare.middletier.DataExchangeSystem_Svc
{
    /// <summary>
    /// 茶山万能转账系统取数据类
    /// </summary>

    [ObjectPooling(Enabled = true)]
    [Transaction(TransactionOption.Supported)]
    public class clsGetExchangeDataSVC
    {
        #region 获取入库数据
        /// <summary>
        /// 获取入库数据
        /// </summary>
        /// <param name="p_dtmBegin"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_ArrResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInStorageData(DateTime p_dtmBegin, DateTime p_dtmEnd, out clsInStorageData_VO[] p_ArrResult)
        {
            long lngRes = -1;
            p_ArrResult = null;
            try
            {
                string strSQL = @"select a.instorageid_vchr,
                                     a.formtype_int,
                                     a.account_dat instoragedate_dat,
                                     a.storageid_chr,
                                     b.medicineid_chr,
                                     b.medicinename_vch,
                                     b.callprice_int,
                                     b.retailprice_int,
                                     b.amount,
                                     d.vendorid_chr,
                                     d.vendorname_vchr,
                                     e.deptname_vchr,
                                     f.medicinetypename_vchr
                                from t_ms_instorage            a,
                                     t_ms_instorage_detal      b,
                                     t_ms_medicinestoreroomset c,
                                     t_bse_vendor              d,
                                     t_bse_deptdesc            e,
                                     t_aid_medicinetype        f
                               where c.medicineroomid = a.storageid_chr
                                 and a.seriesid_int = b.seriesid2_int(+)
                                 and a.vendorid_chr = d.vendorid_chr(+)
                                 and c.medicinetypeid_chr = f.medicinetypeid_chr
                                 and c.deptid_chr = e.deptid_chr 
                                 and b.status = 1
                                and a.state_int > 1
                                and a.account_dat between to_date(?,'yyyy-mm-dd hh24:mi:ss') and to_date( ?,'yyyy-mm-dd hh24:mi:ss')";
                clsHRPTableService objSvc = new clsHRPTableService();
                IDataParameter[] parm = null;

                DataTable dtbResult = null;
                objSvc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = p_dtmBegin.ToString("yyyy-MM-dd 00:00:00");
                parm[1].Value = p_dtmEnd.ToString("yyyy-MM-dd 23:59:59");

                List<clsInStorageData_VO> lisInStorageData = new List<clsInStorageData_VO>();


                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parm);
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        clsInStorageData_VO InStorage = new clsInStorageData_VO();
                        /*业务类别,单据编号,日期,药库号,药库名称,单位编号,单位名称,项目编号,项目名称,买入金额,零售金额,进零差价,标识  */
                        InStorage.YWLB = dtbResult.Rows[i]["deptname_vchr"].ToString() + "入库";
                        InStorage.DJBH = dtbResult.Rows[i]["instorageid_vchr"].ToString();
                        InStorage.RQ = Convert.ToDateTime(dtbResult.Rows[i]["instoragedate_dat"].ToString());
                        InStorage.YKH = dtbResult.Rows[i]["storageid_chr"].ToString().Trim();
                        InStorage.YKMC = dtbResult.Rows[i]["deptname_vchr"].ToString().Trim();
                        InStorage.DWBH = dtbResult.Rows[i]["vendorid_chr"].ToString().Trim();
                        InStorage.DWMC = dtbResult.Rows[i]["vendorname_vchr"].ToString().Trim();
                        InStorage.XMBH = dtbResult.Rows[i]["medicineid_chr"].ToString().Trim();
                        InStorage.XMMC = dtbResult.Rows[i]["medicinetypename_vchr"].ToString().Trim();
                        InStorage.MRJE = str2fla(dtbResult.Rows[i]["callprice_int"].ToString().Trim()) * str2fla(dtbResult.Rows[i]["amount"].ToString().Trim());
                        InStorage.LSJE = str2fla(dtbResult.Rows[i]["retailprice_int"].ToString().Trim()) * str2fla(dtbResult.Rows[i]["amount"].ToString().Trim());
                        InStorage.JLCJ = InStorage.LSJE - InStorage.MRJE;
                        if (dtbResult.Rows[i]["formtype_int"].ToString().Trim() == "2")
                        {
                            InStorage.BZ = "2";
                        }
                        else
                        {
                            InStorage.BZ = "1";
                        }
                        lisInStorageData.Add(InStorage);
                    }
                }
                if (lisInStorageData.Count > 0)
                {
                    p_ArrResult = lisInStorageData.ToArray();
                }

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

        #region 获取出库数据
        /// <summary>
        /// 获取出库数据
        /// </summary>
        /// <param name="p_dtmBegin"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_ArrResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOutStorageData(DateTime p_dtmBegin, DateTime p_dtmEnd, out clsOutStorageData_VO[] p_ArrResult)
        {
            long lngRes = -1;
            p_ArrResult = null;
            try
            {
                string strSQL = @"select a.outstorageid_vchr,
                                           a.formtype,
                                           a.inaccountdate_dat outstoragedate_dat,
                                           a.storageid_chr,
                                           b.medicineid_chr,
                                           b.medicinename_vch,
                                           b.callprice_int,
                                           b.retailprice_int,
                                           b.netamount_int,
                                           case
                                             when d.code_vchr is null then
                                              g.vendorid_chr
                                             else
                                              d.code_vchr
                                           end as code_vchr,
                                           case
                                             when d.code_vchr is null then
                                              g.vendorname_vchr
                                             else
                                              d.deptname_vchr
                                           end as dwmc,
                                           e.deptname_vchr,
                                           f.medicinetypename_vchr
                                      from t_ms_outstorage a
                                     inner join t_ms_outstorage_detail b
                                        on a.seriesid_int = b.seriesid2_int
                                     inner join (select distinct mss.medicineroomid, mss.deptid_chr
                                                   from t_ms_medicinestoreroomset mss) c
                                        on a.storageid_chr = c.medicineroomid
                                     inner join t_bse_medicine m
                                        on b.medicineid_chr = m.medicineid_chr
                                     inner join t_aid_medicinetype f
                                        on m.medicinetypeid_chr = f.medicinetypeid_chr
                                      left join t_bse_vendor g
                                        on b.vendorid_chr = g.vendorid_chr
                                      left join t_bse_deptdesc d
                                        on a.askdept_chr = d.deptid_chr
                                      left join t_bse_deptdesc e
                                        on c.deptid_chr = e.deptid_chr
                                     where (a.inaccountdate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                           to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                       and a.status > 1
                                       and b.status = 1
                                       and b.netamount_int > 0";
                clsHRPTableService objSvc = new clsHRPTableService();
                IDataParameter[] parm = null;

                DataTable dtbResult = null;
                objSvc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = p_dtmBegin.ToString("yyyy-MM-dd 00:00:00");
                parm[1].Value = p_dtmEnd.ToString("yyyy-MM-dd 23:59:59");

                List<clsOutStorageData_VO> lisOutStorage = new List<clsOutStorageData_VO>();


                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parm);

                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        clsOutStorageData_VO OutStorage = new clsOutStorageData_VO();
                        /*业务类别,单据编号,日期,药库号,药库名称,单位编号,单位名称,项目编号,项目名称,买入金额,零售金额,进零差价,标识*/

                        OutStorage.YWLB = dtbResult.Rows[i]["deptname_vchr"].ToString() + "出库";
                        OutStorage.DJBH = dtbResult.Rows[i]["outstorageid_vchr"].ToString();
                        OutStorage.RQ = Convert.ToDateTime(dtbResult.Rows[i]["outstoragedate_dat"].ToString());
                        OutStorage.YKH = dtbResult.Rows[i]["storageid_chr"].ToString().Trim();
                        OutStorage.YKMC = dtbResult.Rows[i]["deptname_vchr"].ToString().Trim();
                        OutStorage.DWBH = dtbResult.Rows[i]["code_vchr"].ToString().Trim();
                        OutStorage.DWMC = dtbResult.Rows[i]["dwmc"].ToString().Trim();
                        OutStorage.XMBH = dtbResult.Rows[i]["medicineid_chr"].ToString().Trim();
                        OutStorage.XMMC = dtbResult.Rows[i]["medicinetypename_vchr"].ToString().Trim();
                        OutStorage.MRJE = str2fla(dtbResult.Rows[i]["callprice_int"].ToString().Trim()) * str2fla(dtbResult.Rows[i]["netamount_int"].ToString().Trim());
                        OutStorage.LSJE = str2fla(dtbResult.Rows[i]["retailprice_int"].ToString().Trim()) * str2fla(dtbResult.Rows[i]["netamount_int"].ToString().Trim());
                        OutStorage.JLCJ = OutStorage.LSJE - OutStorage.MRJE;
                        if (dtbResult.Rows[i]["formtype"].ToString().Trim() == "2")
                        {
                            OutStorage.BZ = "2";
                        }
                        else
                        {
                            OutStorage.BZ = "1";
                        }


                        lisOutStorage.Add(OutStorage);
                    }
                }
                if (lisOutStorage.Count > 0)
                {
                    p_ArrResult = lisOutStorage.ToArray();
                }

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

        #region 获取门诊收入数据
        /// <summary>
        /// 获取门诊收入数据
        /// </summary>
        /// <param name="p_dtmBegin"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_ArrResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOutpatient(DateTime p_dtmBegin, DateTime p_dtmEnd, out clsOutpatient_VO[] p_ArrResult)
        {
            long lngRes = -1;
            string strSQL = string.Empty;
            p_ArrResult = null;
            try
            {
                clsHRPTableService objSvc = new clsHRPTableService();
                IDataParameter[] parm = null;
                DataTable dtbResult = null;
                List<clsOutpatient_VO> lisOutpatient = new List<clsOutpatient_VO>();

                #region 由于让利金额（药品/材料）有问题，停用

                #region 西药
                //                strSQL = @"select a.invoiceno_vchr,
//       a.balance_dat,
//       e.code_vchr,
//       e.deptname_vchr,
//       f.empno_chr,
//       f.lastname_vchr,
//       g.itemcode_vchr,
//       c.typename_vchr,
//       (decode(a.status_int, 2, -1, 1) * b.tolprice_mny + decode(a.status_int, 2, -1, 1) * round(nvl(b.toldiffprice_mny,0),2)) tolprice_mny,
//       a.chargedeptid_chr
//  from t_opr_outpatientrecipeinv   a,
//       t_opr_outpatientpwmrecipede b,
//       t_bse_chargeitemextype      c,
//       t_bse_deptdesc              e,
//       t_bse_employee              f,
//       t_bse_chargeitem            g
// where b.outpatrecipeid_chr = a.outpatrecipeid_chr
//   and b.itemid_chr = g.itemid_chr
//   and c.typeid_chr = g.itemopinvtype_chr
//   and f.empid_chr = a.doctorid_chr
//   and e.deptid_chr = a.deptid_chr
//   and a.status_int <>0
//   and a.balanceflag_int = 1
//   and (a.isvouchers_int < 2 or a.isvouchers_int is null)
//   and a.balance_dat  between to_date(?,'yyyy-mm-dd hh24:mi:ss') and to_date( ?,'yyyy-mm-dd hh24:mi:ss')";
               
//                objSvc.CreateDatabaseParameter(2, out parm);
//                parm[0].Value = p_dtmBegin.ToString("yyyy-MM-dd 00:00:00");
//                parm[1].Value = p_dtmEnd.ToString("yyyy-MM-dd 23:59:59");
                
//                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parm);
//                if (dtbResult != null && dtbResult.Rows.Count > 0)
//                {
//                    for (int i = 0; i < dtbResult.Rows.Count; i++)
//                    {
//                        clsOutpatient_VO Outpatient = new clsOutpatient_VO();
//                        /*单据编号,标识,日期,部门编号,部门名称,医生编号,医生名称,项目编号,项目名称,项目金额*/
//                        Outpatient.DJBH = dtbResult.Rows[i]["invoiceno_vchr"].ToString().Trim();
//                        Outpatient.BZ = "1";
//                        Outpatient.RQ = Convert.ToDateTime(dtbResult.Rows[i]["balance_dat"].ToString().Trim());
//                        Outpatient.BMBH = dtbResult.Rows[i]["code_vchr"].ToString().Trim();
//                        Outpatient.BMMC = dtbResult.Rows[i]["deptname_vchr"].ToString().Trim();
//                        Outpatient.YSBH = dtbResult.Rows[i]["empno_chr"].ToString().Trim();
//                        Outpatient.YSMC = dtbResult.Rows[i]["lastname_vchr"].ToString().Trim();
//                        Outpatient.XMBH = dtbResult.Rows[i]["itemcode_vchr"].ToString().Trim();
//                        Outpatient.XMMC = dtbResult.Rows[i]["typename_vchr"].ToString().Trim();
//                        Outpatient.XMJE = str2fla(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());// *str2fla(dtbResult.Rows[i]["unitprice_mny"].ToString().Trim());
//                        Outpatient.SFCBZ = dtbResult.Rows[i]["chargedeptid_chr"].ToString().Trim();

//                        lisOutpatient.Add(Outpatient);
//                    }
//                }
                #endregion

                #region 中药
//                strSQL = @"select a.invoiceno_vchr,
//       a.balance_dat,
//       e.code_vchr,
//       e.deptname_vchr,
//       f.empno_chr,
//       f.lastname_vchr,
//       g.itemcode_vchr,
//       c.typename_vchr,
//       (decode(a.status_int, 2, -1, 1) * b.tolprice_mny + decode(a.status_int, 2, -1, 1) * round(nvl(b.toldiffprice_mny,0),2)) tolprice_mny,
//       a.chargedeptid_chr
//  from t_opr_outpatientrecipeinv   a,
//       t_opr_outpatientcmrecipede b,
//       t_bse_chargeitemextype      c,
//       t_bse_deptdesc              e,
//       t_bse_employee              f,
//       t_bse_chargeitem            g
// where b.outpatrecipeid_chr = a.outpatrecipeid_chr
//   and b.itemid_chr = g.itemid_chr
//   and c.typeid_chr = g.itemopinvtype_chr
//   and f.empid_chr = a.doctorid_chr
//   and e.deptid_chr = a.deptid_chr
//   and a.Status_Int <>0
//   and a.balanceflag_int = 1
//   and (a.isvouchers_int < 2 or a.isvouchers_int is null)
//   and a.balance_dat between to_date(?,'yyyy-mm-dd hh24:mi:ss') and to_date( ?,'yyyy-mm-dd hh24:mi:ss') ";

//                parm = null;

//                dtbResult = null;
//                objSvc.CreateDatabaseParameter(2, out parm);
//                parm[0].Value = p_dtmBegin.ToString("yyyy-MM-dd 00:00:00");
//                parm[1].Value = p_dtmEnd.ToString("yyyy-MM-dd 23:59:59");

//                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parm);
//                if (dtbResult != null && dtbResult.Rows.Count > 0)
//                {
//                    for (int i = 0; i < dtbResult.Rows.Count; i++)
//                    {
//                        clsOutpatient_VO Outpatient = new clsOutpatient_VO();
//                        /*单据编号,标识,日期,部门编号,部门名称,医生编号,医生名称,项目编号,项目名称,项目金额*/
//                        Outpatient.DJBH = dtbResult.Rows[i]["invoiceno_vchr"].ToString().Trim();
//                        Outpatient.BZ = "1";
//                        Outpatient.RQ = Convert.ToDateTime(dtbResult.Rows[i]["balance_dat"].ToString().Trim());
//                        Outpatient.BMBH = dtbResult.Rows[i]["code_vchr"].ToString().Trim();
//                        Outpatient.BMMC = dtbResult.Rows[i]["deptname_vchr"].ToString().Trim();
//                        Outpatient.YSBH = dtbResult.Rows[i]["empno_chr"].ToString().Trim();
//                        Outpatient.YSMC = dtbResult.Rows[i]["lastname_vchr"].ToString().Trim();
//                        Outpatient.XMBH = dtbResult.Rows[i]["itemcode_vchr"].ToString().Trim();
//                        Outpatient.XMMC = dtbResult.Rows[i]["typename_vchr"].ToString().Trim();
//                        Outpatient.XMJE = str2fla(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
//                        Outpatient.SFCBZ = dtbResult.Rows[i]["chargedeptid_chr"].ToString().Trim();

//                        lisOutpatient.Add(Outpatient);
//                    }
//                }
                #endregion

                #region 检验
//                strSQL = @"select a.invoiceno_vchr,
//       a.balance_dat,
//       e.code_vchr,
//       e.deptname_vchr,
//       f.empno_chr,
//       f.lastname_vchr,
//       g.itemcode_vchr,
//       c.typename_vchr,
//       (decode(a.status_int, 2, -1, 1) * b.tolprice_mny) tolprice_mny,
//       a.chargedeptid_chr
//  from t_opr_outpatientrecipeinv   a,
//       t_opr_outpatientchkrecipede b,
//       t_bse_chargeitemextype      c,
//       t_bse_deptdesc              e,
//       t_bse_employee              f,
//       t_bse_chargeitem            g
// where b.outpatrecipeid_chr = a.outpatrecipeid_chr
//   and b.itemid_chr = g.itemid_chr
//   and c.typeid_chr = g.itemopinvtype_chr
//   and f.empid_chr = a.doctorid_chr
//   and e.deptid_chr = a.deptid_chr
//   and a.Status_Int <>0
//   and a.balanceflag_int = 1
//   and (a.isvouchers_int < 2 or a.isvouchers_int is null)
//   and a.balance_dat between to_date(?,'yyyy-mm-dd hh24:mi:ss') and to_date( ?,'yyyy-mm-dd hh24:mi:ss') ";
//                parm = null;

//                dtbResult = null;
//                objSvc.CreateDatabaseParameter(2, out parm);
//                parm[0].Value = p_dtmBegin.ToString("yyyy-MM-dd 00:00:00");
//                parm[1].Value = p_dtmEnd.ToString("yyyy-MM-dd 23:59:59");




//                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parm);
//                if (dtbResult != null && dtbResult.Rows.Count > 0)
//                {
//                    for (int i = 0; i < dtbResult.Rows.Count; i++)
//                    {
//                        clsOutpatient_VO Outpatient = new clsOutpatient_VO();
//                        /*单据编号,标识,日期,部门编号,部门名称,医生编号,医生名称,项目编号,项目名称,项目金额*/
//                        Outpatient.DJBH = dtbResult.Rows[i]["invoiceno_vchr"].ToString().Trim();
//                        Outpatient.BZ = "1";
//                        Outpatient.RQ = Convert.ToDateTime(dtbResult.Rows[i]["balance_dat"].ToString().Trim());
//                        Outpatient.BMBH = dtbResult.Rows[i]["code_vchr"].ToString().Trim();
//                        Outpatient.BMMC = dtbResult.Rows[i]["deptname_vchr"].ToString().Trim();
//                        Outpatient.YSBH = dtbResult.Rows[i]["empno_chr"].ToString().Trim();
//                        Outpatient.YSMC = dtbResult.Rows[i]["lastname_vchr"].ToString().Trim();
//                        Outpatient.XMBH = dtbResult.Rows[i]["itemcode_vchr"].ToString().Trim();
//                        Outpatient.XMMC = dtbResult.Rows[i]["typename_vchr"].ToString().Trim();
//                        Outpatient.XMJE = str2fla(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
//                        Outpatient.SFCBZ = dtbResult.Rows[i]["chargedeptid_chr"].ToString().Trim();

//                        lisOutpatient.Add(Outpatient);
//                    }
//                }
                #endregion

                #region 检查
//                strSQL = @"select a.invoiceno_vchr,
//       a.balance_dat,
//       e.code_vchr,
//       e.deptname_vchr,
//       f.empno_chr,
//       f.lastname_vchr,
//       g.itemcode_vchr,
//       c.typename_vchr,
//       (decode(a.status_int, 2, -1, 1) * b.tolprice_mny) tolprice_mny,
//       a.chargedeptid_chr
//  from t_opr_outpatientrecipeinv   a,
//       t_opr_outpatienttestrecipede b,
//       t_bse_chargeitemextype      c,
//       t_bse_deptdesc              e,
//       t_bse_employee              f,
//       t_bse_chargeitem            g
// where b.outpatrecipeid_chr = a.outpatrecipeid_chr
//   and b.itemid_chr = g.itemid_chr
//   and c.typeid_chr = g.itemopinvtype_chr
//   and f.empid_chr = a.doctorid_chr
//   and e.deptid_chr = a.deptid_chr
//   and a.Status_Int <>0
//   and a.balanceflag_int = 1
//   and (a.isvouchers_int < 2 or a.isvouchers_int is null)
//   and a.balance_dat between to_date(?,'yyyy-mm-dd hh24:mi:ss') and to_date( ?,'yyyy-mm-dd hh24:mi:ss') ";
//                parm = null;

//                dtbResult = null;
//                objSvc.CreateDatabaseParameter(2, out parm);
//                parm[0].Value = p_dtmBegin.ToString("yyyy-MM-dd 00:00:00");
//                parm[1].Value = p_dtmEnd.ToString("yyyy-MM-dd 23:59:59");

//                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parm);
//                if (dtbResult != null && dtbResult.Rows.Count > 0)
//                {
//                    for (int i = 0; i < dtbResult.Rows.Count; i++)
//                    {
//                        clsOutpatient_VO Outpatient = new clsOutpatient_VO();
//                        /*单据编号,标识,日期,部门编号,部门名称,医生编号,医生名称,项目编号,项目名称,项目金额*/
//                        Outpatient.DJBH = dtbResult.Rows[i]["invoiceno_vchr"].ToString().Trim();
//                        Outpatient.BZ = "1";
//                        Outpatient.RQ = Convert.ToDateTime(dtbResult.Rows[i]["balance_dat"].ToString().Trim());
//                        Outpatient.BMBH = dtbResult.Rows[i]["code_vchr"].ToString().Trim();
//                        Outpatient.BMMC = dtbResult.Rows[i]["deptname_vchr"].ToString().Trim();
//                        Outpatient.YSBH = dtbResult.Rows[i]["empno_chr"].ToString().Trim();
//                        Outpatient.YSMC = dtbResult.Rows[i]["lastname_vchr"].ToString().Trim();
//                        Outpatient.XMBH = dtbResult.Rows[i]["itemcode_vchr"].ToString().Trim();
//                        Outpatient.XMMC = dtbResult.Rows[i]["typename_vchr"].ToString().Trim();
//                        Outpatient.XMJE = str2fla(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
//                        Outpatient.SFCBZ = dtbResult.Rows[i]["chargedeptid_chr"].ToString().Trim();

//                        lisOutpatient.Add(Outpatient);
//                    }
//                }
                #endregion

                #region 手术/治疗
//                strSQL = @"select a.invoiceno_vchr,
//       a.balance_dat,
//       e.code_vchr,
//       e.deptname_vchr,
//       f.empno_chr,
//       f.lastname_vchr,
//       g.itemcode_vchr,
//       c.typename_vchr,
//       (decode(a.status_int, 2, -1, 1) * b.tolprice_mny) tolprice_mny,
//       a.chargedeptid_chr
//  from t_opr_outpatientrecipeinv   a,
//       t_opr_outpatientopsrecipede b,
//       t_bse_chargeitemextype      c,
//       t_bse_deptdesc              e,
//       t_bse_employee              f,
//       t_bse_chargeitem            g
// where b.outpatrecipeid_chr = a.outpatrecipeid_chr
//   and b.itemid_chr = g.itemid_chr
//   and c.typeid_chr = g.itemopinvtype_chr
//   and f.empid_chr = a.doctorid_chr
//   and e.deptid_chr = a.deptid_chr
//   and a.Status_Int <>0
//   and a.balanceflag_int = 1
//   and (a.isvouchers_int < 2 or a.isvouchers_int is null)
//   and a.balance_dat between to_date(?,'yyyy-mm-dd hh24:mi:ss') and to_date( ?,'yyyy-mm-dd hh24:mi:ss') ";
//                parm = null;

//                dtbResult = null;
//                objSvc.CreateDatabaseParameter(2, out parm);
//                parm[0].Value = p_dtmBegin.ToString("yyyy-MM-dd 00:00:00");
//                parm[1].Value = p_dtmEnd.ToString("yyyy-MM-dd 23:59:59");

//                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parm);
//                if (dtbResult != null && dtbResult.Rows.Count > 0)
//                {
//                    for (int i = 0; i < dtbResult.Rows.Count; i++)
//                    {
//                        clsOutpatient_VO Outpatient = new clsOutpatient_VO();
//                        /*单据编号,标识,日期,部门编号,部门名称,医生编号,医生名称,项目编号,项目名称,项目金额*/
//                        Outpatient.DJBH = dtbResult.Rows[i]["invoiceno_vchr"].ToString().Trim();
//                        Outpatient.BZ = "1";
//                        Outpatient.RQ = Convert.ToDateTime(dtbResult.Rows[i]["balance_dat"].ToString().Trim());
//                        Outpatient.BMBH = dtbResult.Rows[i]["code_vchr"].ToString().Trim();
//                        Outpatient.BMMC = dtbResult.Rows[i]["deptname_vchr"].ToString().Trim();
//                        Outpatient.YSBH = dtbResult.Rows[i]["empno_chr"].ToString().Trim();
//                        Outpatient.YSMC = dtbResult.Rows[i]["lastname_vchr"].ToString().Trim();
//                        Outpatient.XMBH = dtbResult.Rows[i]["itemcode_vchr"].ToString().Trim();
//                        Outpatient.XMMC = dtbResult.Rows[i]["typename_vchr"].ToString().Trim();
//                        Outpatient.XMJE = str2fla(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
//                        Outpatient.SFCBZ = dtbResult.Rows[i]["chargedeptid_chr"].ToString().Trim();


//                        lisOutpatient.Add(Outpatient);
//                    }
//                }
                #endregion

                #region 其他
//                strSQL = @"select a.invoiceno_vchr,
//       a.balance_dat,
//       e.code_vchr,
//       e.deptname_vchr,
//       f.empno_chr,
//       f.lastname_vchr,
//       g.itemcode_vchr,
//       c.typename_vchr,      
//       (decode(a.status_int, 2, -1, 1) * b.tolprice_mny + decode(a.status_int, 2, -1, 1) * round(nvl(b.toldiffprice_mny,0),2)) tolprice_mny, 
//       a.chargedeptid_chr
//  from t_opr_outpatientrecipeinv   a,
//       t_opr_outpatientothrecipede b,
//       t_bse_chargeitemextype      c,
//       t_bse_deptdesc              e,
//       t_bse_employee              f,
//       t_bse_chargeitem            g
// where b.outpatrecipeid_chr = a.outpatrecipeid_chr
//   and b.itemid_chr = g.itemid_chr
//   and c.typeid_chr = g.itemopinvtype_chr
//   and f.empid_chr = a.doctorid_chr
//   and e.deptid_chr = a.deptid_chr
//   and a.Status_Int <>0
//   and a.balanceflag_int = 1
//   and (a.isvouchers_int < 2 or a.isvouchers_int is null)
//   and a.balance_dat between to_date(?,'yyyy-mm-dd hh24:mi:ss') and to_date( ?,'yyyy-mm-dd hh24:mi:ss') ";

//                //-- (decode(a.status_int, 2, -1, 1) * b.tolprice_mny) tolprice_mny,
//                parm = null;
//                dtbResult = null;
//                objSvc.CreateDatabaseParameter(2, out parm);
//                parm[0].Value = p_dtmBegin.ToString("yyyy-MM-dd 00:00:00");
//                parm[1].Value = p_dtmEnd.ToString("yyyy-MM-dd 23:59:59");

//                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parm);
//                if (dtbResult != null && dtbResult.Rows.Count > 0)
//                {
//                    for (int i = 0; i < dtbResult.Rows.Count; i++)
//                    {
//                        clsOutpatient_VO Outpatient = new clsOutpatient_VO();
//                        /*单据编号,标识,日期,部门编号,部门名称,医生编号,医生名称,项目编号,项目名称,项目金额*/
//                        Outpatient.DJBH = dtbResult.Rows[i]["invoiceno_vchr"].ToString().Trim();
//                        Outpatient.BZ = "1";
//                        Outpatient.RQ = Convert.ToDateTime(dtbResult.Rows[i]["balance_dat"].ToString().Trim());
//                        Outpatient.BMBH = dtbResult.Rows[i]["code_vchr"].ToString().Trim();
//                        Outpatient.BMMC = dtbResult.Rows[i]["deptname_vchr"].ToString().Trim();
//                        Outpatient.YSBH = dtbResult.Rows[i]["empno_chr"].ToString().Trim();
//                        Outpatient.YSMC = dtbResult.Rows[i]["lastname_vchr"].ToString().Trim();
//                        Outpatient.XMBH = dtbResult.Rows[i]["itemcode_vchr"].ToString().Trim();
//                        Outpatient.XMMC = dtbResult.Rows[i]["typename_vchr"].ToString().Trim();
//                        Outpatient.XMJE = str2fla(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
//                        Outpatient.SFCBZ = dtbResult.Rows[i]["chargedeptid_chr"].ToString().Trim();

//                        lisOutpatient.Add(Outpatient);
//                    }
//                }
                #endregion

                #endregion

                #region 处方结算明细

                strSQL = @"select a.invoiceno_vchr,
                                   a.balance_dat,
                                   e.code_vchr,
                                   e.deptname_vchr,
                                   f.empno_chr,
                                   f.lastname_vchr,
                                   g.itemcode_vchr,
                                   c.typename_vchr,
                                   (decode(a.status_int, 2, -1, 1) * 
                                   decode(b.tolprice_mny,
                                           0,
                                           round(b.price_mny * b.qty_dec, 2),
                                           b.tolprice_mny) -
                                   (decode(a.status_int, 2, -1, 1) *
                                   decode(substr(a.invoiceno_vchr, 0, 2),
                                            'WX',
                                            (round(b.price_mny * b.qty_dec, 2) -
                                            round(b.buyprice_dec * b.qty_dec, 2)),
                                            round((b.price_mny - b.buyprice_dec) * b.qty_dec, 2)))) tolprice_mny,
                                   a.chargedeptid_chr
                              from t_opr_outpatientrecipeinv a,
                                   t_opr_oprecipeitemde      b,
                                   t_bse_chargeitemextype    c,
                                   t_bse_deptdesc            e,
                                   t_bse_employee            f,
                                   t_bse_chargeitem          g
                             where a.outpatrecipeid_chr = b.outpatrecipeid_chr
                               and b.itemid_chr = g.itemid_chr
                               and c.typeid_chr = g.itemopinvtype_chr
                               and a.doctorid_chr = f.empid_chr
                               and e.deptid_chr = a.deptid_chr
                               and a.status_int <> 0
                               and a.balanceflag_int = 1
                               and (a.isvouchers_int < 2 or a.isvouchers_int is null)
                               and a.balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                   to_date(?, 'yyyy-mm-dd hh24:mi:ss')";

                objSvc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = p_dtmBegin.ToString("yyyy-MM-dd 00:00:00");
                parm[1].Value = p_dtmEnd.ToString("yyyy-MM-dd 23:59:59");

                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parm);
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        clsOutpatient_VO Outpatient = new clsOutpatient_VO();
                        /*单据编号,标识,日期,部门编号,部门名称,医生编号,医生名称,项目编号,项目名称,项目金额*/
                        Outpatient.DJBH = dtbResult.Rows[i]["invoiceno_vchr"].ToString().Trim();
                        Outpatient.BZ = "1";
                        Outpatient.RQ = Convert.ToDateTime(dtbResult.Rows[i]["balance_dat"].ToString().Trim());
                        Outpatient.BMBH = dtbResult.Rows[i]["code_vchr"].ToString().Trim();
                        Outpatient.BMMC = dtbResult.Rows[i]["deptname_vchr"].ToString().Trim();
                        Outpatient.YSBH = dtbResult.Rows[i]["empno_chr"].ToString().Trim();
                        Outpatient.YSMC = dtbResult.Rows[i]["lastname_vchr"].ToString().Trim();
                        Outpatient.XMBH = dtbResult.Rows[i]["itemcode_vchr"].ToString().Trim();
                        Outpatient.XMMC = dtbResult.Rows[i]["typename_vchr"].ToString().Trim();
                        Outpatient.XMJE = str2fla(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());// *str2fla(dtbResult.Rows[i]["unitprice_mny"].ToString().Trim());
                        Outpatient.SFCBZ = dtbResult.Rows[i]["chargedeptid_chr"].ToString().Trim();

                        lisOutpatient.Add(Outpatient);
                    }
                }

                #endregion

                #region 行政票据
                strSQL = @"select a.billno_vchr,
       a.balance_dat,
       d.code_vchr,
       d.deptname_vchr,
       c.empno_chr,
       c.lastname_vchr,
       b.itemcode_vchr,
       f.typename_vchr,
       b.tolprice_mny,
       '0000192' chargedeptid_chr
  from t_opr_mainbill         a,
       t_opr_mainbillde       b,
       t_bse_employee         c,
       t_bse_deptdesc         d,
       t_bse_billitem         e,
       t_bse_chargeitemextype f
 where a.seqid_chr = b.seqid_chr
   and b.execdeptcode_chr = d.code_vchr(+)
   and a.operemp_chr = c.empid_chr
   and b.itemid_chr = e.itemid_chr
   and e.itemcalctype_chr = f.typeid_chr
   and a.status_int not in (-1, 1)
   and a.balance_dat between
       to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
       to_date(?, 'yyyy-mm-dd hh24:mi:ss') ";
                parm = null;

                dtbResult = null;
                objSvc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = p_dtmBegin.ToString("yyyy-MM-dd 00:00:00");
                parm[1].Value = p_dtmEnd.ToString("yyyy-MM-dd 23:59:59");

                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parm);
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        clsOutpatient_VO Outpatient = new clsOutpatient_VO();
                        /*单据编号,标识,日期,部门编号,部门名称,医生编号,医生名称,项目编号,项目名称,项目金额*/
                        Outpatient.DJBH = dtbResult.Rows[i]["billno_vchr"].ToString().Trim();
                        Outpatient.BZ = "1";
                        Outpatient.RQ = Convert.ToDateTime(dtbResult.Rows[i]["balance_dat"].ToString().Trim());
                        Outpatient.BMBH = dtbResult.Rows[i]["code_vchr"].ToString().Trim();
                        Outpatient.BMMC = dtbResult.Rows[i]["deptname_vchr"].ToString().Trim();
                        Outpatient.YSBH = dtbResult.Rows[i]["empno_chr"].ToString().Trim();
                        Outpatient.YSMC = dtbResult.Rows[i]["lastname_vchr"].ToString().Trim();
                        Outpatient.XMBH = dtbResult.Rows[i]["itemcode_vchr"].ToString().Trim();
                        Outpatient.XMMC = dtbResult.Rows[i]["typename_vchr"].ToString().Trim();
                        Outpatient.XMJE = str2fla(dtbResult.Rows[i]["tolprice_mny"].ToString().Trim());
                        Outpatient.SFCBZ = dtbResult.Rows[i]["chargedeptid_chr"].ToString().Trim();


                        lisOutpatient.Add(Outpatient);
                    }
                }
                #endregion

                if (lisOutpatient.Count > 0)
                {
                    p_ArrResult = lisOutpatient.ToArray();
                }

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

        #region 获取住院收入数据
        /// <summary>
        /// 获取住院收入数据
        /// </summary>
        /// <param name="p_dtmBegin"></param>
        /// <param name="p_dtmEnd"></param>
        /// <param name="p_ArrResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInHospital(DateTime p_dtmBegin, DateTime p_dtmEnd, out clsInHospital_VO[] p_ArrResult)
        {
            long lngRes = -1;
            p_ArrResult = null;
            try
            {
                #region 2010-12-25 注释
                //                string strSQL = @"select a.pchargeid_chr,
                //                                   a.chargeactive_dat,
                //                                   b.code_vchr,
                //                                   b.deptname_vchr,
                //                                   c.empno_chr,
                //                                   c.lastname_vchr,
                //                                   d.itemcode_vchr,
                //                                   d.itemname_vchr,
                //                                   a.amount_dec,
                //                                   a.unitprice_dec
                //                              from t_opr_bih_patientcharge a,
                //                                   t_bse_deptdesc          b,
                //                                   t_bse_employee          c,
                //                                   t_bse_chargeitem        d
                //                             where b.deptid_chr = a.clacarea_chr
                //                               and c.empid_chr = a.chargedoctorid_chr(+)
                //                               and d.itemid_chr = a.chargeitemid_chr
                //                                  and a.status_int=1
                //                                  and a.pstatus_int<>0
                //                                and a.chargeactive_dat between ? and ? ";
                #endregion

                //已结算
                string strSQL = @"select f.invoiceno_vchr,
       g.recdate_dat,
       b.code_vchr,
       b.deptname_vchr,
       c.empno_chr,
       c.lastname_vchr,
       d.itemcode_vchr,
       e.typename_vchr,
       round(a.amount_dec * a.unitprice_dec, 2) + round(nvl(a.totaldiffcostmoney_dec,0), 2)  totalmoney_dec
  from t_opr_bih_chargeitementry a,
       t_bse_deptdesc            b,
       t_bse_employee            c,
       t_bse_chargeitem          d,
       t_bse_chargeitemextype    e,
       t_opr_bih_chargedefinv    f,
       t_opr_bih_charge          g
 where g.chargeno_chr = a.chargeno_chr
   and a.chargeno_chr = f.chargeno_chr
   and a.curareaid_chr = b.deptid_chr
   and a.chargedoctorid_chr = c.empid_chr(+)
   and a.chargeitemid_chr = d.itemid_chr
   and d.itemipinvtype_chr = e.typeid_chr
   and g.recdate_dat between to_date( ?,'yyyy-mm-dd hh24:mi:ss') and to_date( ?,'yyyy-mm-dd hh24:mi:ss') ";
                clsHRPTableService objSvc = new clsHRPTableService();
                IDataParameter[] parm = null;

                DataTable dtbResult = null;
                objSvc.CreateDatabaseParameter(2, out parm);
              
                parm[0].Value = p_dtmBegin.ToString("yyyy-MM-dd 00:00:00");
                parm[1].Value = p_dtmEnd.ToString("yyyy-MM-dd 23:59:59");

                List<clsInHospital_VO> lisInHospital = new List<clsInHospital_VO>();


                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parm);
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        clsInHospital_VO InHospital = new clsInHospital_VO();
                        /*单据编号,标识,日期,部门编号,部门名称,医生编号,医生名称,项目编号,项目名称,项目金额*/
                        InHospital.DJBH = dtbResult.Rows[i]["invoiceno_vchr"].ToString();
                        InHospital.BZ = "1";
                        InHospital.RQ = Convert.ToDateTime(dtbResult.Rows[i]["recdate_dat"].ToString());
                        InHospital.BMBH = dtbResult.Rows[i]["code_vchr"].ToString();
                        InHospital.BMMC = dtbResult.Rows[i]["deptname_vchr"].ToString();
                        InHospital.YSBH = dtbResult.Rows[i]["empno_chr"].ToString();
                        InHospital.YSMC = dtbResult.Rows[i]["lastname_vchr"].ToString();
                        InHospital.XMBH = dtbResult.Rows[i]["itemcode_vchr"].ToString();
                        InHospital.XMMC = dtbResult.Rows[i]["typename_vchr"].ToString();
                        InHospital.XMJE = str2fla(dtbResult.Rows[i]["totalmoney_dec"].ToString()); //* str2fla(dtbResult.Rows[i]["unitprice_dec"].ToString());

                        lisInHospital.Add(InHospital);
                    }
                }

                //未结算

                strSQL = @" select a.chargeactive_dat,
       b.code_vchr,
       b.deptname_vchr,
       c.empno_chr,
       c.lastname_vchr,
       d.itemcode_vchr,
       d.itemname_vchr,
       round(a.amount_dec * a.unitprice_dec, 2) + round(nvl(a.totaldiffcostmoney_dec,0), 2)  totalmoney_dec,
       e.typename_vchr
  from t_opr_bih_patientcharge a,
       t_bse_deptdesc          b,
       t_bse_employee          c,
       t_bse_chargeitem        d,
       t_bse_chargeitemextype   e
 where a.curareaid_chr  = b.deptid_chr
   and a.chargedoctorid_chr =  c.empid_chr(+)
   and a.chargeitemid_chr  = d.itemid_chr
   and d.itemipinvtype_chr =  e.typeid_chr
   and a.status_int = 1
   and a.pstatus_int <> 0 
   and a.chargeactive_dat between to_date( ?,'yyyy-mm-dd hh24:mi:ss') and to_date( ?,'yyyy-mm-dd hh24:mi:ss') ";

                parm = null;
                dtbResult = null;

                objSvc.CreateDatabaseParameter(2, out parm);
                // parm[0].DbType = DbType.DateTime;
                parm[0].Value = p_dtmBegin.ToString("yyyy-MM-dd 00:00:00");
                //parm[1].DbType = DbType.DateTime;
                parm[1].Value = p_dtmEnd.ToString("yyyy-MM-dd 23:59:59");

                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parm);
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        clsInHospital_VO InHospital = new clsInHospital_VO();
                        /*单据编号,标识,日期,部门编号,部门名称,医生编号,医生名称,项目编号,项目名称,项目金额*/
                        InHospital.DJBH = "WJ99999999";
                        InHospital.BZ = "3";
                        InHospital.RQ = Convert.ToDateTime(dtbResult.Rows[i]["chargeactive_dat"].ToString());
                        InHospital.BMBH = dtbResult.Rows[i]["code_vchr"].ToString();
                        InHospital.BMMC = dtbResult.Rows[i]["deptname_vchr"].ToString();
                        InHospital.YSBH = dtbResult.Rows[i]["empno_chr"].ToString();
                        InHospital.YSMC = dtbResult.Rows[i]["lastname_vchr"].ToString().Trim();
                        InHospital.XMBH = dtbResult.Rows[i]["itemcode_vchr"].ToString().Trim();
                        InHospital.XMMC = dtbResult.Rows[i]["typename_vchr"].ToString().Trim();
                        InHospital.XMJE = str2fla(dtbResult.Rows[i]["totalmoney_dec"].ToString()); //str2fla(dtbResult.Rows[i]["amount_dec"].ToString()) * str2fla(dtbResult.Rows[i]["unitprice_dec"].ToString());

                        lisInHospital.Add(InHospital);
                    }
                }



                //预交金
                strSQL = @"select a.prepayinv_vchr,
       a.create_dat,
       b.code_vchr,
       b.deptname_vchr,
       a.money_dec,
       c.empno_chr,
       c.lastname_vchr
  from t_opr_bih_prepay a, t_bse_deptdesc b, t_bse_employee c
 where b.deptid_chr = a.areaid_chr
   and c.empid_chr = a.creatorid_chr
   and a.create_dat between to_date(?,'yyyy-mm-dd hh24:mi:ss') and to_date( ?,'yyyy-mm-dd hh24:mi:ss') ";
                parm = null;

                dtbResult = null;
                objSvc.CreateDatabaseParameter(2, out parm);
               // parm[0].DbType = DbType.DateTime;
                parm[0].Value = p_dtmBegin.ToString("yyyy-MM-dd 00:00:00");
                //parm[1].DbType = DbType.DateTime;
                parm[1].Value = p_dtmEnd.ToString("yyyy-MM-dd 23:59:59");

                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtbResult, parm);
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        clsInHospital_VO InHospital = new clsInHospital_VO();
                        /*单据编号,标识,日期,部门编号,部门名称,医生编号,医生名称,项目编号,项目名称,项目金额*/
                        InHospital.DJBH = dtbResult.Rows[i]["prepayinv_vchr"].ToString();
                        InHospital.BZ = "2";
                        InHospital.RQ = Convert.ToDateTime(dtbResult.Rows[i]["create_dat"].ToString());
                        InHospital.BMBH = dtbResult.Rows[i]["code_vchr"].ToString();
                        InHospital.BMMC = dtbResult.Rows[i]["deptname_vchr"].ToString();
                        InHospital.YSBH = dtbResult.Rows[i]["empno_chr"].ToString();
                        InHospital.YSMC = dtbResult.Rows[i]["lastname_vchr"].ToString();
                        InHospital.XMBH = "000000";
                        InHospital.XMMC = "预交金";
                        InHospital.XMJE = str2fla(dtbResult.Rows[i]["money_dec"].ToString()); //* str2fla(dtbResult.Rows[i]["unitprice_dec"].ToString());

                        lisInHospital.Add(InHospital);
                    }
                }


                if (lisInHospital.Count > 0)
                {
                    p_ArrResult = lisInHospital.ToArray();
                }

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

        #region string转换为float
        /// <summary>
        /// 把string转化成float
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public float str2fla(string str)
        {
            float fla = 0;
            if (!float.TryParse(str, out fla))
            {
                fla = 0;
            }
            return fla;
        }
        #endregion
    }
}
