using System;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using System.EnterpriseServices;
using System.Collections;
using System.Data;
using System.Text;
using System.Collections.Generic;
using weCare.Core.Utils;
using System.Linq;

namespace com.digitalwave.iCare.middletier.HIS
{
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsYBCSQuerySVC : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsYBCSQuerySVC()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 获取门诊结算需传入的数据
        /// <summary>
        /// 获取门诊结算需传入的数据
        /// </summary>
        /// <param name="strRecipeID">处方id</param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDgmzjsdata(string strRecipeID, out DataTable dtResult)
        {
            dtResult = null;
            long lngRes = -1;
            //需要关联表t_ins_deptrel把his的科室id对应转换成社保科室代码
            //需要获取挂号编号zyh、门诊诊断cyzd、就诊日期（处方日期）jzrq、就诊科室（需取对应表）cybqdm、联系电话lxdh、医院科室名称yyryks、身份证号gmsfhm
            string sql = @"select  a.idcard_chr        as gmsfhm,
                                   b.diag_vchr         as cyzd,
                                   c.recorddate_dat    as jzrq,
                                   d.empno_chr         as ysgh,
                                   e.deptname_vchr     as yyryks,
                                   f.homephone_vchr    as lxdh,
                                   g.patientcardid_chr as zyh,
                                   h.insdeptcode_vchr  as cybqdm,
                                   i.sdywh,
                                   i.ylfyze,
                                   i.tczf,
                                   i.grzfze, i.jzjlh
                              from t_bse_patient           a,
                                   t_opr_outpatientcasehis b,
                                   t_opr_outpatientrecipe  c,
                                   t_bse_employee          d,
                                   t_bse_deptdesc          e,
                                   t_bse_patientidx        f,
                                   t_bse_patientcard       g,
                                   t_ins_deptrel           h,
                                   t_ins_chargemz_csyb     i
                             where a.patientid_chr = c.patientid_chr
                               and c.diagdr_chr = d.empid_chr
                               and c.casehisid_chr = b.casehisid_chr(+)
                               and c.diagdept_chr = e.deptid_chr(+)
                               and a.patientid_chr = g.patientid_chr
                               and a.patientid_chr = f.patientid_chr
                               and c.diagdept_chr = h.hosdeptid_vchr(+)
                               and c.outpatrecipeid_chr = i.cfh(+)
                               and c.outpatrecipeid_chr = ?";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = strRecipeID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtResult, paraArr);
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取住院医保登记所需数据
        /// <summary>
        /// 获取住院医保登记所需数据
        /// </summary>
        /// <param name="strRegisterId"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetZYYBRegister(string strRegisterId, out DataTable dtResult)
        {
            dtResult = new DataTable();
            long lngRes = -1;
            //need modify 参照登记界面和住院医保登记vo字段来取
            //此处左连接t_ins_cszyreg，如果查出的JZJLH（就诊记录号）为空，说明该病人还没进行医保登记
            string sql = @"select  a.registerid_chr,
                                   a.deptid_chr as areaid_chr,
                                   a.bedid_chr,
                                   decode(a.inareadate_dat,null,a.inpatient_dat,a.inareadate_dat) as inpatient_dat,
                                   a.inpatientid_chr,
                                   decode(a.mzdiagnose_vchr,null,'未知',a.mzdiagnose_vchr) as mzdiagnose_vchr,
                                   b.idcard_chr,
                                   b.lastname_vchr,
                                   decode(b.mobile_chr, null, b.homephone_vchr,b.mobile_chr) as contactphone,
                                   c.deptname_vchr,
                                   d.insdeptcode_vchr,
                                   e.code_chr as bed_no,
                                   f.lastname_vchr as doctorname_vchr,
                                   f.empno_chr,
                                   ' ' as jzjlh_vchr,
                                   ' ' as zylb_vchr,
                                   ' ' as jzlb_vchr,
                                   ' ' as wsbz_vchr,
                                   ' ' as zqqrqk_vchr,
                                   ' ' as zqqrsbh_vchr,
                                   ' ' as cybz_vchr,
                                   ' ' as rydyzdby_vchr,
                                   t.outhospital_dat,
                                   ' ' as icd10_1,
                                   ' ' as icd10_2,
                                   ' ' as icd10_3,
                                   ' ' as inreason,
                                   ' ' as assitype,
                                   ' ' as outstatus,
                                   ' ' as cbdtcqbm_vchr  
                              from t_opr_bih_register       a,
                                   t_opr_bih_registerdetail b,
                                   t_bse_deptdesc           c,
                                   t_ins_deptrel            d,
                                   t_bse_bed                e,
                                   t_bse_employee           f,
                                (select registerid_chr,
                                            outhospital_dat
                                        from t_opr_bih_leave
                                        where status_int = 1) t
                             where a.registerid_chr = b.registerid_chr
                               and a.registerid_chr = t.registerid_chr(+)
                               and a.areaid_chr = c.deptid_chr
                               and c.deptid_chr = d.hosdeptid_vchr(+)
                               and a.bedid_chr = e.bedid_chr(+)
                               and a.casedoctor_chr = f.empid_chr(+)
                               and a.registerid_chr = ? ";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = strRegisterId;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtResult, paraArr);
                if (dtResult.Rows.Count > 0)
                {
                    sql = @"select g.jzjlh_vchr,
                                   g.zylb_vchr,
                                   g.jzlb_vchr,
                                   g.wsbz_vchr,
                                   g.zqqrqk_vchr,
                                   g.zqqrsbh_vchr,
                                   g.cybz_vchr,
                                   g.rydyzdby_vchr,
                                   g.icd10_1,
                                   g.icd10_2,
                                   g.icd10_3,
                                   g.inreason,
                                   g.assitype,
                                   g.outstatus,
                                   g.cbdtcqbm_vchr  
                              from t_ins_cszyreg g
                             where g.cybz_vchr <> '3'
                               and g.registerid_vchr = ?";
                    DataTable dtTemp = new DataTable();
                    paraArr = null;
                    objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                    paraArr[0].Value = strRegisterId;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtTemp, paraArr);
                    if (dtTemp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtResult.Columns.Count; i++)
                        {
                            dtResult.Columns[i].ReadOnly = false;
                        }

                        dtResult.Rows[0]["jzjlh_vchr"] = dtTemp.Rows[0]["jzjlh_vchr"].ToString();
                        dtResult.Rows[0]["zylb_vchr"] = dtTemp.Rows[0]["zylb_vchr"].ToString();
                        dtResult.Rows[0]["jzlb_vchr"] = dtTemp.Rows[0]["jzlb_vchr"].ToString();
                        dtResult.Rows[0]["wsbz_vchr"] = dtTemp.Rows[0]["wsbz_vchr"].ToString();
                        dtResult.Rows[0]["zqqrqk_vchr"] = dtTemp.Rows[0]["zqqrqk_vchr"].ToString();
                        dtResult.Rows[0]["zqqrsbh_vchr"] = dtTemp.Rows[0]["zqqrsbh_vchr"].ToString();
                        dtResult.Rows[0]["cybz_vchr"] = dtTemp.Rows[0]["cybz_vchr"].ToString();
                        dtResult.Rows[0]["rydyzdby_vchr"] = dtTemp.Rows[0]["rydyzdby_vchr"].ToString();
                        dtResult.Rows[0]["icd10_1"] = dtTemp.Rows[0]["icd10_1"].ToString();
                        dtResult.Rows[0]["icd10_2"] = dtTemp.Rows[0]["icd10_2"].ToString();
                        dtResult.Rows[0]["icd10_3"] = dtTemp.Rows[0]["icd10_3"].ToString();
                        dtResult.Rows[0]["inreason"] = dtTemp.Rows[0]["inreason"].ToString();
                        dtResult.Rows[0]["assitype"] = dtTemp.Rows[0]["assitype"].ToString();
                        dtResult.Rows[0]["outstatus"] = dtTemp.Rows[0]["outstatus"].ToString();
                        dtResult.Rows[0]["cbdtcqbm_vchr"] = dtTemp.Rows[0]["cbdtcqbm_vchr"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取住院医保待遇享受所需数据
        /// <summary>
        /// 获取住院医保待遇享受所需数据
        /// </summary>
        /// <param name="strRegisterId"></param>
        /// <param name="objDgzydyxsVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetZYYBDyxs(string strJslb, string strRegisterId, ref string strName, ref string strZyh, ref string strStatus, out clsDGZydyxs_VO objDgzydyxsVo, out clsDGZyjsfh_VO objDgzyjsfhVo, out decimal decZyfyze, out decimal decGrzfeije)
        {
            #region bak
            //            long lngRes = -1;
            //            decZyfyze = 0;
            //            decGrzfeije = 0;
            //            //need modify 
            //            string sql = @" select a.inpatientid_chr,
            //                                   b.lastname_vchr,
            //                                   b.idcard_chr,
            //                                   c.zylb_vchr,
            //                                   c.jzjlh_vchr,
            //                                   c.status_chr,
            //                                   d.zyfyze,
            //                                   d.grzfeije,
            //                                   d.sdywh,
            //                                   d.charge_status
            //                              from t_opr_bih_register       a,
            //                                   t_opr_bih_registerdetail b,
            //                                   t_ins_cszyreg            c,
            //                                   t_ins_chargezy_csyb      d
            //                             where a.registerid_chr = b.registerid_chr
            //                               and b.registerid_chr = c.registerid_vchr(+)
            //                               and b.registerid_chr = d.registerid_chr(+)
            //                               and a.registerid_chr = ?
            //                               and d.charge_status = 1
            //                               order by d.createtime desc";
            //            objDgzydyxsVo = new clsDGZydyxs_VO();
            //            objDgzyjsfhVo = new clsDGZyjsfh_VO();
            //            try
            //            {
            //                clsHRPTableService objHRPSvc = new clsHRPTableService();
            //                IDataParameter[] paraArr = null;
            //                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
            //                paraArr[0].Value = strRegisterId;
            //                DataTable dtResult = new DataTable();
            //                lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtResult, paraArr);
            //                if (lngRes > 0)
            //                {
            //                    if (dtResult.Rows.Count > 0)
            //                    {
            //                        DataRow dr = null;
            //                        objDgzydyxsVo = new clsDGZydyxs_VO();
            //                        //for (int i = 0; i < dtResult.Rows.Count; i++) //charge_status
            //                        //{
            //                        dr = dtResult.Rows[0];

            //                        strName = dr["lastname_vchr"].ToString();//病人姓名
            //                        strZyh = dr["inpatientid_chr"].ToString();//住院号
            //                        objDgzydyxsVo.GMSFHM = dr["idcard_chr"].ToString();//身份证号
            //                        objDgzydyxsVo.ZYLB = dr["zylb_vchr"].ToString().Split('-')[0].ToString();//住院类别.t_ins_cszyreg
            //                        objDgzydyxsVo.JZJLH = dr["jzjlh_vchr"].ToString();//就诊记录号
            //                        objDgzydyxsVo.JSRQ = DateTime.Now.ToString("yyyyMMdd");//YYYYMMDD对应结算终止日期，（不确定，暂传当前日期）
            //                        strStatus = dr["status_chr"].ToString();//状态标志 -1无效，0登记，1医保出院登记，2医保结算，3医保退费
            //                        if (dr["charge_status"].ToString().Trim().Equals("1"))
            //                        {
            //                            decZyfyze = this.Dec(dr["zyfyze"].ToString().Trim().Length == 0 ? "0" : dr["zyfyze"].ToString().Trim());
            //                            decGrzfeije = this.Dec(dr["grzfeije"].ToString().Trim().Length == 0 ? "0" : dr["grzfeije"].ToString().Trim());
            //                            objDgzyjsfhVo.SDYWH = dr["sdywh"].ToString().Trim().Length == 0 ? "0" : dr["sdywh"].ToString().Trim();
            //                           // i = dtResult.Rows.Count;
            //                        }

            //                        //  }

            //                    }

            //                }

            //            }
            //            catch (Exception objEx)
            //            {
            //                clsLogText objLogger = new clsLogText();
            //                bool blnRes = objLogger.LogError(objEx);
            //            }
            //            return lngRes;
            #endregion

            long lngRes = -1;
            decZyfyze = 0;
            decGrzfeije = 0;
            //need modify 
            string sql = @" select a.inpatientid_chr,
                                    b.lastname_vchr,
                                    b.idcard_chr,
                                    c.zylb_vchr,
                                    c.jzjlh_vchr,
                                    c.status_chr,
                                    c.rydyzdby_vchr,
                                    c.jsdyzdby_vchr,
                                    c.jzlb_vchr,
                                    c.cbdtcqbm_vchr  
                            from t_opr_bih_register       a,
                                    t_opr_bih_registerdetail b,
                                    t_ins_cszyreg            c
                            where a.registerid_chr = b.registerid_chr
                                and b.registerid_chr = c.registerid_vchr
                                and c.cybz_vchr<>'3' 
                                and a.registerid_chr = ?";

            objDgzydyxsVo = null;
            objDgzyjsfhVo = null;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = strRegisterId;
                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtResult, paraArr);
                if (lngRes > 0)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        sql = @"select d.registerid_chr,
                                       d.zyfyze,
                                       d.grzfeije,
                                       d.sdywh,
                                       d.charge_status
                                  from t_ins_chargezy_csyb d
                                 where d.charge_status = 1
                                   {0}
                                   and d.registerid_chr = ?
                                 order by d.createtime desc";

                        if (strJslb.Equals("1"))//出院结算
                        {
                            sql = sql.Replace("{0}", "and d.jslx='1' ");
                        }
                        else //中途结算
                        {
                            sql = sql.Replace("{0}", "and d.jslx='2' ");
                        }

                        objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                        paraArr[0].Value = strRegisterId;
                        DataTable dtTemp = new DataTable();
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtTemp, paraArr);

                        DataRow dr = dtResult.Rows[0];
                        objDgzydyxsVo = new clsDGZydyxs_VO();
                        objDgzyjsfhVo = new clsDGZyjsfh_VO();
                        strName = dr["lastname_vchr"].ToString();//病人姓名
                        strZyh = dr["inpatientid_chr"].ToString();//住院号
                        objDgzydyxsVo.GMSFHM = dr["idcard_chr"].ToString();//身份证号
                        objDgzydyxsVo.ZYLB = dr["zylb_vchr"].ToString().Split('-')[0].ToString();//住院类别.t_ins_cszyreg
                        objDgzydyxsVo.JZJLH = dr["jzjlh_vchr"].ToString();//就诊记录号
                        objDgzydyxsVo.JSRQ = DateTime.Now.ToString("yyyyMMdd");//YYYYMMDD对应结算终止日期，（不确定，暂传当前日期）
                        strStatus = dr["status_chr"].ToString();//状态标志 -1无效，0登记，1医保出院登记，2医保结算，3医保退费
                        objDgzydyxsVo.RYDYZDBY = dr["rydyzdby_vchr"].ToString();
                        objDgzydyxsVo.JSDYZDBY = dr["jsdyzdby_vchr"].ToString();
                        objDgzydyxsVo.JZLB = dr["jzlb_vchr"].ToString();    // 就诊类别
                        objDgzydyxsVo.CBDTCQBM = dr["cbdtcqbm_vchr"].ToString();
                        if (dtTemp.Rows.Count > 0)
                        {
                            dr = dtTemp.Rows[0];
                            decZyfyze = this.Dec(dr["zyfyze"].ToString().Trim().Length == 0 ? "0" : dr["zyfyze"].ToString().Trim());
                            decGrzfeije = this.Dec(dr["grzfeije"].ToString().Trim().Length == 0 ? "0" : dr["grzfeije"].ToString().Trim());
                            objDgzyjsfhVo.SDYWH = dr["sdywh"].ToString().Trim().Length == 0 ? "0" : dr["sdywh"].ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据住院病人ID获取住院传送明细数据
        /// <summary>
        /// 根据住院病人ID获取住院传送明细数据
        /// </summary>
        /// <param name="strRegisterId"></param>
        /// <param name="lstDgzyjsxxxxVo"></param>
        /// <param name="p_blnDiffCostOn">是否启用让利</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDgzyxmcs(string strRegisterId, out List<clsDGZyxmcs_VO> lstDgzyxmcsVo, bool p_blnDiffCostOn, decimal decHISTotalSum, List<string> lstPChargeId)
        {
            lstDgzyxmcsVo = null;
            DataTable dtResult = new DataTable();
            long lngRes = -1;
            string sql = string.Empty;
            #region bak 160905
            //need modify 参照vo字段来取
            /*
             sql = @"select a.pchargeid_chr,
                                      g.itemcode_vchr as chargeitemid_chr,
                                      a.chargeitemname_chr,
                                      a.spec_vchr,
                                      a.unitprice_dec,
                                      a.amount_dec,
                                      round(a.unitprice_dec*a.amount_dec, 2) as totalmoney_dec,
                                      a.invcateid_chr,
                                      a.discount_dec,
                                      to_char(a.chargeactive_dat,'yyyy-mm-dd hh24:mi:ss') as chargeactive_dat,
                                      b.inpatientid_chr,
                                      c.idcard_chr,
                                      d.empno_chr,
                                      e.jzjlh_vchr,
                                      decode(f.itemchargetype_vchr,3,3,2) as fhxzbz,
                                      a.totaldiffcostmoney_dec,
                                      DECODE (g.ipchargeflg_int,
                                     1, round (g.tradeprice_mny / g.packqty_dec, 4),
                                     0, g.tradeprice_mny,
                                 round (g.tradeprice_mny / g.packqty_dec, 4)
                                ) tradeprice_mny
                                 from t_opr_bih_patientcharge a,
                                      t_opr_bih_register      b,
                                      t_opr_bih_registerdetail c,
                                      t_bse_employee          d,
                                      t_ins_cszyreg           e,
                                      t_opr_bih_orderchargedept f,
                                      t_bse_chargeitem g
                                where a.registerid_chr = b.registerid_chr
                                  and b.registerid_chr=c.registerid_chr
                                  and b.casedoctor_chr = d.empid_chr
                                  and a.registerid_chr=e.registerid_vchr(+)
                                  and a.chargeitemid_chr = f.chargeitemid_chr(+)
                                  and a.orderid_chr = f.orderid_chr(+)
                                  and a.chargeitemid_chr=g.itemid_chr
                                  and a.sendflag_int=0
                                  and (a.pstatus_int = 1 or a.pstatus_int = 2)
                                  and a.status_int = 1
                                  and e.cybz_vchr = 1
                                  and a.chargeactive_dat is not null
                                  and a.registerid_chr = ? "; */
            #endregion

            sql = @"select a.pchargeid_chr,
                           g.itemcode_vchr as chargeitemid_chr,
                           a.chargeitemname_chr,
                           a.spec_vchr,
                           a.unitprice_dec,
                           a.amount_dec,
                           round(a.unitprice_dec * a.amount_dec, 2) as totalmoney_dec,
                           a.invcateid_chr,
                           a.discount_dec,
                           to_char(a.chargeactive_dat, 'yyyy-mm-dd hh24:mi:ss') as chargeactive_dat,
                           b.inpatientid_chr,
                           c.idcard_chr,
                           d.empno_chr,
                           e.jzjlh_vchr,
                           e.cbdtcqbm_vchr,
                           decode(f.itemchargetype_vchr, '3', 3, 2) as fhxzbz,
                           a.totaldiffcostmoney_dec,
                           h.chargetotalsum,
                           DECODE(g.ipchargeflg_int,
                                  1,
                                  round(g.tradeprice_mny / g.packqty_dec, 4),
                                  0,
                                  g.tradeprice_mny,
                                  round(g.tradeprice_mny / g.packqty_dec, 4)) as tradeprice_mny,
                           nvl(a.buyprice_dec,0) as buyprice_dec
                      from t_opr_bih_patientcharge a
                     inner join t_opr_bih_register b
                        on a.registerid_chr = b.registerid_chr
                     inner join t_opr_bih_registerdetail c
                        on b.registerid_chr = c.registerid_chr
                     inner join t_bse_employee d
                        on b.casedoctor_chr = d.empid_chr
                      left join t_ins_cszyreg e
                        on a.registerid_chr = e.registerid_vchr
                       and e.cybz_vchr = 1
                      left join t_opr_bih_orderchargedept f
                        on a.chargeitemid_chr = f.chargeitemid_chr
                       and a.orderid_chr = f.orderid_chr
                       and f.itemchargetype_vchr = '3' 
                     inner join t_bse_chargeitem g
                        on a.chargeitemid_chr = g.itemid_chr
                      left join (select a.pchargeid_chr, sum(a.totalmoney_dec) as chargetotalsum
                                   from t_opr_bih_chargeitementry a,
                                        t_opr_bih_register        b,
                                        t_opr_bih_charge          c
                                  where a.registerid_chr = b.registerid_chr
                                    and a.chargeno_chr = c.chargeno_chr
                                    and b.status_int = 1
                                    and b.feestatus_int = 4
                                    and c.class_int = 3
                                    and a.registerid_chr = ?
                                  group by a.pchargeid_chr) h
                        on a.pchargeid_chr = h.pchargeid_chr
                     where (a.pstatus_int = 1 or a.pstatus_int = 2)
                       and a.status_int = 1
                       and a.chargeactive_dat is not null
                       and a.registerid_chr = ?";

            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = strRegisterId;
                parm[1].Value = strRegisterId;
                lngRes = svc.lngGetDataTableWithParameters(sql, ref dtResult, parm);
                if (lngRes > 0)
                {
                    List<EntityUpData> lstUp = new List<EntityUpData>();
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        EntityUpData vo2 = null;
                        foreach (DataRow dr in dtResult.Rows)
                        {
                            vo2 = new EntityUpData();
                            vo2.pchargeid_chr = dr["pchargeid_chr"].ToString();
                            vo2.chargeitemid_chr = dr["chargeitemid_chr"].ToString();
                            vo2.chargeitemname_chr = dr["chargeitemname_chr"].ToString();
                            vo2.spec_vchr = dr["spec_vchr"].ToString();
                            vo2.unitprice_dec = Function.Dec(dr["unitprice_dec"].ToString());
                            vo2.amount_dec = Function.Dec(dr["amount_dec"].ToString());
                            vo2.totalmoney_dec = Function.Dec(dr["totalmoney_dec"].ToString());
                            vo2.invcateid_chr = dr["invcateid_chr"].ToString();
                            vo2.discount_dec = Function.Dec(dr["discount_dec"].ToString());
                            vo2.chargeactive_dat = dr["chargeactive_dat"].ToString();
                            vo2.inpatientid_chr = dr["inpatientid_chr"].ToString();
                            vo2.idcard_chr = dr["idcard_chr"].ToString();
                            vo2.empno_chr = dr["empno_chr"].ToString();
                            vo2.jzjlh_vchr = dr["jzjlh_vchr"].ToString();
                            vo2.cbdtcqbm_vchr = dr["cbdtcqbm_vchr"].ToString();
                            vo2.fhxzbz = Function.Int(dr["fhxzbz"].ToString());
                            vo2.totaldiffcostmoney_dec = Function.Dec(dr["totaldiffcostmoney_dec"].ToString());
                            vo2.chargetotalsum = Function.Dec(dr["chargetotalsum"].ToString());
                            vo2.tradeprice_mny = Function.Dec(dr["tradeprice_mny"].ToString());
                            vo2.buyprice_dec = Function.Dec(dr["buyprice_dec"].ToString());
                            lstUp.Add(vo2);
                        }
                    }
                    if (lstUp != null && lstUp.Count > 0)
                    {
                        List<EntityUpData> lstClone = new List<EntityUpData>();
                        foreach (EntityUpData item in lstUp)
                        {
                            if (item.amount_dec == 0) continue;
                            if (lstClone.Any(t => t.chargeitemid_chr == item.chargeitemid_chr && t.unitprice_dec == item.unitprice_dec && t.buyprice_dec == item.buyprice_dec))
                            {
                                EntityUpData tmpVo = lstClone.FirstOrDefault(t => t.chargeitemid_chr == item.chargeitemid_chr && t.unitprice_dec == item.unitprice_dec && t.buyprice_dec == item.buyprice_dec);
                                tmpVo.amount_dec += item.amount_dec;
                                tmpVo.totalmoney_dec += item.totalmoney_dec;
                                tmpVo.totaldiffcostmoney_dec += item.totaldiffcostmoney_dec;
                                tmpVo.chargetotalsum += item.chargetotalsum;
                            }
                            else
                            {
                                lstClone.Add(item);
                            }
                        }

                        lstDgzyxmcsVo = new List<clsDGZyxmcs_VO>();
                        int intRowCount = dtResult.Rows.Count;
                        clsDGZyxmcs_VO vo = null;
                        int intSortno = 0;
                        decimal decTotalMoney = 0;
                        decimal decTotalDiffCostMoney = 0;
                        decimal decDiscount = 0;
                        foreach (EntityUpData item in lstClone)
                        {
                            if (item.amount_dec == 0) continue;
                            // 注意有些字段有长度限制 
                            // 中途结算
                            if (lstPChargeId != null && lstPChargeId.Count > 0)
                            {
                                if (lstPChargeId.IndexOf(item.pchargeid_chr) < 0) continue;
                            }
                            ++intSortno;
                            vo = new clsDGZyxmcs_VO();
                            vo.JZJLH = item.jzjlh_vchr;           // 就诊记录号
                            vo.GRSHBZH = item.idcard_chr;         // 身份证号
                            vo.ZYH = item.inpatientid_chr;        // 住院号                               
                            vo.XMXH = intSortno.ToString().PadLeft(6, '0');      // 项目序号
                            vo.CFXMWYH = item.pchargeid_chr;      // 处方项目唯一号
                            vo.YYXMBM = item.chargeitemid_chr;    // 医院项目编码
                            vo.XMMC = item.chargeitemname_chr;    // 项目名称
                            vo.YYFLDM = item.invcateid_chr;       // 医院分类代码
                            vo.YPGG = item.spec_vchr;             // 规格
                            vo.YPJX = "";                                        // 剂型
                            vo.MCYL = item.amount_dec;     //数量 NUMBER	(8,2)
                            decTotalMoney = this.Round(item.unitprice_dec * item.amount_dec, 2) - this.Round(item.chargetotalsum, 2);
                            decTotalDiffCostMoney = string.IsNullOrEmpty(item.totaldiffcostmoney_dec.ToString()) ? 0 : this.Round(item.totaldiffcostmoney_dec, 2);
                            if (p_blnDiffCostOn)
                            {
                                decTotalMoney += decTotalDiffCostMoney;
                                // 2019-10-09
                                if (decTotalDiffCostMoney != 0)
                                {
                                    if (item.buyprice_dec != 0)
                                    {
                                        vo.JG = item.buyprice_dec;
                                    }
                                    else
                                    {
                                        if (vo.MCYL == 0)
                                        {
                                            vo.JG = item.unitprice_dec;
                                        }
                                        else
                                        {
                                            vo.JG = this.Round(decTotalMoney / vo.MCYL, 4);       //价格统一计算。合计金额/数量。
                                        }
                                    }
                                }
                                else
                                {
                                    vo.JG = item.unitprice_dec;
                                }
                            }
                            else
                            {
                                vo.JG = item.unitprice_dec;    //单价 NUMBER	(12,4),若启用让利则改为按批发上传
                            }
                            vo.JE = decTotalMoney;                                           //金额 NUMBER	(12,2) 该条记录的总费用金额
                            decDiscount = item.discount_dec / 100;
                            vo.ZFEIBL = decDiscount;                                         // 自费比例	NUMBER	(5,2)		0< = X <= 1 医保应该不会按这个比例计算，可传0
                            vo.ZFEIJE = decTotalMoney * decDiscount;                         // 自费金额	NUMBER	(16,2)
                            vo.FHXZBZ = item.fhxzbz.ToString();                          // 符合限制标志
                            vo.JZSJ = Convert.ToDateTime(item.chargeactive_dat).ToString("yyyyMMddHHmmss");//记账时间	NUMBER	14	N	yyyymmddhh24miss
                            vo.YSGH = item.empno_chr;                         // 医师工号
                            vo.BZ3 = "";                                                     // 备注3
                            vo.CBDTCQBM = item.cbdtcqbm_vchr;                 // 参保地统筹区编码

                            //以下异常处方传送将会提示报错：  2019-11-14
                            //1、单价或数量为0、金额不为0；
                            //2、总金额 / 数量的绝对值 与单价的绝对值误差在10元以上；
                            //3、费用计算时，单条处方自费金额大于总金额；
                            if ((vo.JG == 0 || vo.MCYL == 0) && vo.JE != 0)
                            {
                                continue;
                            }
                            // 加收判断 2019-11-14  ( 儿童价格加收...)
                            if (vo.MCYL == 1)
                            {
                                if (vo.JE - vo.JG > 10)
                                {
                                    vo.JG = vo.JE;
                                }
                            }
                            else if (vo.MCYL > 1)
                            {
                                decimal tmpPrice = weCare.Core.Utils.Function.Round(vo.JE / vo.MCYL, 2);
                                if (tmpPrice - vo.JG > 10)
                                {
                                    vo.JG = tmpPrice;
                                }
                            }

                            lstDgzyxmcsVo.Add(vo);
                        }
                    }

                    #region bak 2019-12-09
                    //if (dtResult != null && dtResult.Rows.Count > 0)
                    //{
                    //    DataTable dtClone = dtResult.Clone();
                    //    DataRow drR = null;
                    //    DataRow[] drr = null;
                    //    for (int i = 0; i < dtResult.Rows.Count; i++)
                    //    {
                    //        drR = dtResult.Rows[i];
                    //        drr = dtClone.Select(string.Format("chargeitemid_chr = '{0}' and unitprice_dec = {1} and buyprice_dec = {2}", drR["chargeitemid_chr"].ToString(), drR["unitprice_dec"].ToString(), drR["buyprice_dec"].ToString()));
                    //        if (drr == null || drr.Length == 0)
                    //        {
                    //            dtClone.LoadDataRow(drR.ItemArray, true);
                    //        }
                    //        else
                    //        {
                    //            drr[0]["amount_dec"] = this.Dec(drr[0]["amount_dec"]) + this.Dec(drR["amount_dec"]);
                    //            drr[0]["totalmoney_dec"] = this.Dec(drr[0]["totalmoney_dec"]) + this.Dec(drR["totalmoney_dec"]);
                    //            drr[0]["totaldiffcostmoney_dec"] = this.Dec(drr[0]["totaldiffcostmoney_dec"]) + this.Dec(drR["totaldiffcostmoney_dec"]);
                    //            drr[0]["chargetotalsum"] = this.Dec(drr[0]["chargetotalsum"]) + this.Dec(drR["chargetotalsum"]);
                    //            dtClone.AcceptChanges();
                    //        }
                    //    }
                    //    for (int i = dtClone.Rows.Count - 1; i >= 0; i--)
                    //    {
                    //        if (this.Dec(dtClone.Rows[i]["amount_dec"].ToString()) == 0)
                    //        {
                    //            dtClone.Rows.RemoveAt(i);
                    //        }
                    //    }
                    //    if (dtClone != null && dtClone.Rows.Count > 0)
                    //    {
                    //        lstDgzyxmcsVo = new List<clsDGZyxmcs_VO>();
                    //        int intRowCount = dtResult.Rows.Count;
                    //        clsDGZyxmcs_VO vo = null;
                    //        int intSortno = 0;
                    //        decimal decTotalMoney = 0;
                    //        decimal decTotalDiffCostMoney = 0;
                    //        decimal decDiscount = 0;
                    //        foreach (DataRow dr in dtClone.Rows)
                    //        {
                    //            //need modify 赋值，注意有些字段有长度限制 
                    //            // 中途结算
                    //            if (lstPChargeId != null && lstPChargeId.Count > 0)
                    //            {
                    //                if (lstPChargeId.IndexOf(dr["pchargeid_chr"].ToString()) < 0) continue;
                    //            }
                    //            ++intSortno;
                    //            vo = new clsDGZyxmcs_VO();
                    //            vo.JZJLH = dr["jzjlh_vchr"].ToString();           // 就诊记录号
                    //            vo.GRSHBZH = dr["idcard_chr"].ToString();         // 身份证号
                    //            vo.ZYH = dr["inpatientid_chr"].ToString();        // 住院号                               
                    //            vo.XMXH = intSortno.ToString().PadLeft(6, '0');      // 项目序号
                    //            vo.CFXMWYH = dr["pchargeid_chr"].ToString();      // 处方项目唯一号
                    //            vo.YYXMBM = dr["chargeitemid_chr"].ToString();    // 医院项目编码
                    //            vo.XMMC = dr["chargeitemname_chr"].ToString();    // 项目名称
                    //            vo.YYFLDM = dr["invcateid_chr"].ToString();       // 医院分类代码
                    //            vo.YPGG = dr["spec_vchr"].ToString();             // 规格
                    //            vo.YPJX = "";                                        // 剂型
                    //            vo.MCYL = this.Dec(dr["amount_dec"].ToString());     //数量 NUMBER	(8,2)
                    //            decTotalMoney = this.Round(this.Dec(dr["unitprice_dec"].ToString()) * this.Dec(dr["amount_dec"].ToString()), 2) - this.Round(this.Dec(dr["chargetotalsum"].ToString()), 2);
                    //            decTotalDiffCostMoney = string.IsNullOrEmpty(dr["totaldiffcostmoney_dec"].ToString()) ? 0 : this.Round(this.Dec(dr["totaldiffcostmoney_dec"].ToString()), 2);
                    //            if (p_blnDiffCostOn)
                    //            {
                    //                decTotalMoney += decTotalDiffCostMoney;
                    //                // 2019-10-09
                    //                if (decTotalDiffCostMoney != 0)
                    //                {
                    //                    if (this.Dec(dr["buyprice_dec"].ToString()) != 0)
                    //                    {
                    //                        vo.JG = this.Dec(dr["buyprice_dec"].ToString());
                    //                    }
                    //                    else
                    //                    {
                    //                        if (vo.MCYL == 0)
                    //                        {
                    //                            vo.JG = this.Dec(dr["unitprice_dec"].ToString());
                    //                        }
                    //                        else
                    //                        {
                    //                            vo.JG = this.Round(decTotalMoney / vo.MCYL, 4);       //价格统一计算。合计金额/数量。
                    //                        }
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    vo.JG = this.Dec(dr["unitprice_dec"].ToString());
                    //                }
                    //            }
                    //            else
                    //            {
                    //                vo.JG = this.Dec(dr["unitprice_dec"].ToString());    //单价 NUMBER	(12,4),若启用让利则改为按批发上传
                    //            }
                    //            vo.JE = decTotalMoney;                                           //金额 NUMBER	(12,2) 该条记录的总费用金额
                    //            decDiscount = this.Dec(dr["discount_dec"].ToString()) / 100;
                    //            vo.ZFEIBL = decDiscount;                                         // 自费比例	NUMBER	(5,2)		0< = X <= 1 医保应该不会按这个比例计算，可传0
                    //            vo.ZFEIJE = decTotalMoney * decDiscount;                         // 自费金额	NUMBER	(16,2)
                    //            vo.FHXZBZ = dr["fhxzbz"].ToString();                          // 符合限制标志
                    //            vo.JZSJ = Convert.ToDateTime(dr["chargeactive_dat"].ToString()).ToString("yyyyMMddHHmmss");//记账时间	NUMBER	14	N	yyyymmddhh24miss
                    //            vo.YSGH = dr["empno_chr"].ToString();                         // 医师工号
                    //            vo.BZ3 = "";                                                     // 备注3
                    //            vo.CBDTCQBM = dr["cbdtcqbm_vchr"].ToString();                 // 参保地统筹区编码

                    //            //以下异常处方传送将会提示报错：  2019-11-14
                    //            //1、单价或数量为0、金额不为0；
                    //            //2、总金额 / 数量的绝对值 与单价的绝对值误差在10元以上；
                    //            //3、费用计算时，单条处方自费金额大于总金额；
                    //            if ((vo.JG == 0 || vo.MCYL == 0) && vo.JE != 0)
                    //            {
                    //                continue;
                    //            }
                    //            // 加收判断 2019-11-14  ( 儿童价格加收...)
                    //            if (vo.MCYL == 1)
                    //            {
                    //                if (vo.JE - vo.JG > 10)
                    //                {
                    //                    vo.JG = vo.JE;
                    //                }
                    //            }
                    //            else if (vo.MCYL > 1)
                    //            {
                    //                decimal tmpPrice = weCare.Core.Utils.Function.Round(vo.JE / vo.MCYL, 2);
                    //                if (tmpPrice - vo.JG > 10)
                    //                {
                    //                    vo.JG = tmpPrice;
                    //                }
                    //            }

                    //            lstDgzyxmcsVo.Add(vo);
                    //        }
                    //    }
                    //}
                    #endregion

                    #region 2019-10-09
                    /* decimal decNewSum = 0;
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        lstDgzyxmcsVo = new List<clsDGZyxmcs_VO>();
                        int intRowCount = dtResult.Rows.Count;
                        DataRow drTmp = null;
                        clsDGZyxmcs_VO objDgzyxmcsVo = null;
                        int intSortno = 1;
                        decimal decTotalMoney = 0;
                        decimal decTotalDiffCostMoney = 0;
                        decimal decDiscount = 0;
                        for (int i = 0; i < intRowCount; i++)
                        {
                            //need modify 赋值，注意有些字段有长度限制
                            drTmp = dtResult.Rows[i];
                            // 中途结算
                            if (lstPChargeId != null && lstPChargeId.Count > 0)
                            {
                                if (lstPChargeId.IndexOf(drTmp["pchargeid_chr"].ToString()) < 0) continue;
                            }
                            objDgzyxmcsVo = new clsDGZyxmcs_VO();
                            objDgzyxmcsVo.JZJLH = drTmp["jzjlh_vchr"].ToString();           // 就诊记录号
                            objDgzyxmcsVo.GRSHBZH = drTmp["idcard_chr"].ToString();         // 身份证号
                            objDgzyxmcsVo.ZYH = drTmp["inpatientid_chr"].ToString();        // 住院号
                            intSortno = intSortno + i;
                            objDgzyxmcsVo.XMXH = intSortno.ToString().PadLeft(6, '0');      // 项目序号
                            objDgzyxmcsVo.CFXMWYH = drTmp["pchargeid_chr"].ToString();      // 处方项目唯一号
                            objDgzyxmcsVo.YYXMBM = drTmp["chargeitemid_chr"].ToString();    // 医院项目编码
                            objDgzyxmcsVo.XMMC = drTmp["chargeitemname_chr"].ToString();    // 项目名称
                            objDgzyxmcsVo.YYFLDM = drTmp["invcateid_chr"].ToString();       // 医院分类代码
                            objDgzyxmcsVo.YPGG = drTmp["spec_vchr"].ToString();             // 规格
                            objDgzyxmcsVo.YPJX = "";                                        // 剂型
                            objDgzyxmcsVo.MCYL = this.Dec(drTmp["amount_dec"].ToString());     //数量 NUMBER	(8,2)
                            decTotalMoney = this.Round(this.Dec(drTmp["unitprice_dec"].ToString()) * this.Dec(drTmp["amount_dec"].ToString()), 2) - this.Round(this.Dec(drTmp["chargetotalsum"].ToString()), 2);
                            //decTotalMoney = this.Dec(drTmp["totalmoney_dec"].ToString());
                            decTotalDiffCostMoney = string.IsNullOrEmpty(drTmp["totaldiffcostmoney_dec"].ToString()) ? 0 : this.Round(this.Dec(drTmp["totaldiffcostmoney_dec"].ToString()), 2);
                            if (p_blnDiffCostOn)
                            {
                                decTotalMoney += decTotalDiffCostMoney;
                                // 2019-10-09
                                if (decTotalDiffCostMoney != 0)
                                {
                                    if (this.Dec(drTmp["buyprice_dec"].ToString()) != 0)
                                    {
                                        objDgzyxmcsVo.JG = this.Dec(drTmp["buyprice_dec"].ToString());
                                    }
                                    else
                                    {
                                        if (objDgzyxmcsVo.MCYL == 0)
                                        {
                                            objDgzyxmcsVo.JG = this.Dec(drTmp["unitprice_dec"].ToString());
                                        }
                                        else
                                        {
                                            objDgzyxmcsVo.JG = this.Round(decTotalMoney / objDgzyxmcsVo.MCYL, 4);       //价格统一计算。合计金额/数量。
                                        }
                                    }
                                }
                                else
                                {
                                    objDgzyxmcsVo.JG = this.Dec(drTmp["unitprice_dec"].ToString());
                                }
                                //if (objDgzyxmcsVo.MCYL == 0)
                                //{
                                //    objDgzyxmcsVo.JG = this.Dec(drTmp["unitprice_dec"].ToString());
                                //}
                                //else
                                //{
                                //    objDgzyxmcsVo.JG = this.Round(decTotalMoney / objDgzyxmcsVo.MCYL, 4);       //价格统一计算。合计金额/数量。
                                //}
                            }
                            else
                            {
                                objDgzyxmcsVo.JG = this.Dec(drTmp["unitprice_dec"].ToString());    //单价 NUMBER	(12,4),若启用让利则改为按批发上传
                            }
                            objDgzyxmcsVo.JE = decTotalMoney;                                               //金额 NUMBER	(12,2) 该条记录的总费用金额

                            #region 160905
                            //防止两边金额不一致，假如上传总费用与HIS总费用一致的话，就用原来的金额，不致的话最后一条记录就为（HIS总金额） -（最后一条记录之前的所有费用的总和）                            
                            //if (i != intRowCount - 1)
                            //{
                            //    objDgzyxmcsVo.JE = decTotalMoney;//金额 NUMBER	(12,2) 该条记录的总费用金额
                            //    decNewSum += decTotalMoney;
                            //}
                            //else
                            //{
                            //    if (decHISTotalSum == decNewSum + decTotalMoney)
                            //    {
                            //        objDgzyxmcsVo.JE = decTotalMoney;
                            //    }
                            //    else
                            //    {
                            //        objDgzyxmcsVo.JE = decHISTotalSum - decNewSum;
                            //    }
                            //    objDgzyxmcsVo.JG = this.Round(objDgzyxmcsVo.JE / objDgzyxmcsVo.MCYL, 4);
                            //    decTotalMoney = objDgzyxmcsVo.JE;
                            //}  
                            #endregion

                            decDiscount = this.Dec(drTmp["discount_dec"].ToString()) / 100;
                            objDgzyxmcsVo.ZFEIBL = decDiscount;                                         // 自费比例	NUMBER	(5,2)		0< = X <= 1 医保应该不会按这个比例计算，可传0
                            objDgzyxmcsVo.ZFEIJE = decTotalMoney * decDiscount;                         // 自费金额	NUMBER	(16,2)
                            objDgzyxmcsVo.FHXZBZ = drTmp["fhxzbz"].ToString();                          // 符合限制标志
                            objDgzyxmcsVo.JZSJ = Convert.ToDateTime(drTmp["chargeactive_dat"].ToString()).ToString("yyyyMMddHHmmss");//记账时间	NUMBER	14	N	yyyymmddhh24miss
                            objDgzyxmcsVo.YSGH = drTmp["empno_chr"].ToString();                         // 医师工号
                            objDgzyxmcsVo.BZ3 = "";                                                     // 备注3
                            objDgzyxmcsVo.CBDTCQBM = drTmp["cbdtcqbm_vchr"].ToString();                 // 参保地统筹区编码
                            lstDgzyxmcsVo.Add(objDgzyxmcsVo);
                        }
                    } */
                    #endregion
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据时间获取住院传送明细数据

        /// <summary>
        /// 根据时间获取住院传送明细数据
        /// </summary>
        /// <param name="strRegisterId"></param>
        /// <param name="lstDgzyjsxxxxVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDgzyxmcs(DateTime p_dateBegin, DateTime p_dateEnd, out List<clsDGZyxmcs_VO> lstDgzyxmcsVo, bool p_blnDiffCostOn)
        {
            return m_lngGetDgzyxmcs2(p_dateBegin, p_dateEnd, out lstDgzyxmcsVo, p_blnDiffCostOn, string.Empty);
        }

        /// <summary>
        /// 根据时间获取住院传送明细数据
        /// </summary>
        /// <param name="strRegisterId"></param>
        /// <param name="lstDgzyjsxxxxVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDgzyxmcs2(DateTime p_dateBegin, DateTime p_dateEnd, out List<clsDGZyxmcs_VO> lstDgzyxmcsVo, bool p_blnDiffCostOn, string jzjlh)
        {
            lstDgzyxmcsVo = null;
            DataTable dtResult = new DataTable();
            long lngRes = -1;
            //need modify 参照vo字段来取
            string sql = @"select distinct a.pchargeid_chr,
                                   g.itemcode_vchr as chargeitemid_chr,
                                   a.chargeitemname_chr,
                                   a.spec_vchr,
                                   a.unitprice_dec,
                                   a.amount_dec,
                                   round(a.unitprice_dec * a.amount_dec, 2) as totalmoney_dec,
                                   a.invcateid_chr,
                                   a.discount_dec,
                                   to_char(a.chargeactive_dat,'yyyy-mm-dd hh24:mi:ss') as chargeactive_dat,
                                   b.inpatientid_chr,
                                   c.idcard_chr,
                                   d.empno_chr,
                                   e.jzjlh_vchr,
                                   decode(f.itemchargetype_vchr, 3, 3, 2) as fhxzbz,
                                   a.totaldiffcostmoney_dec,
                                   DECODE (g.ipchargeflg_int,
                                     1, round (g.tradeprice_mny / g.packqty_dec, 4),
                                     0, g.tradeprice_mny,
                                 round (g.tradeprice_mny / g.packqty_dec, 4)
                                ) tradeprice_mny
                              from t_opr_bih_patientcharge   a,
                                   t_opr_bih_register        b,
                                   t_opr_bih_registerdetail  c,
                                   t_bse_employee            d,
                                   t_ins_cszyreg             e,
                                   t_opr_bih_orderchargedept f,
                                   t_bse_chargeitem          g
                             where a.registerid_chr = b.registerid_chr
                               and b.registerid_chr = c.registerid_chr
                               and b.casedoctor_chr = d.empid_chr
                               and a.registerid_chr = e.registerid_vchr(+)
                               and a.chargeitemid_chr = f.chargeitemid_chr
                               and a.orderid_chr = f.orderid_chr
                               and a.chargeitemid_chr = g.itemid_chr
                               and (a.pstatus_int = 1 or a.pstatus_int = 2)
                               and a.status_int = 1
                               and a.chargeactive_dat is not null
                               and a.sendflag_int = 0
                               and e.cybz_vchr=1
                               and a.create_dat between
                                   to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                   to_date(?, 'yyyy-mm-dd hh24:mi:ss') 
                                ";
            //and a.create_dat between ? and ? ";

            // 171111 modify
            sql = @"select a.pchargeid_chr,
                           g.itemcode_vchr as chargeitemid_chr,
                           a.chargeitemname_chr,
                           a.spec_vchr,
                           a.unitprice_dec,
                           a.amount_dec,
                           round(a.unitprice_dec * a.amount_dec, 2) as totalmoney_dec,
                           a.invcateid_chr,
                           a.discount_dec,
                           to_char(a.chargeactive_dat, 'yyyy-mm-dd hh24:mi:ss') as chargeactive_dat,
                           b.inpatientid_chr,
                           c.idcard_chr,
                           d.empno_chr,
                           e.jzjlh_vchr,
                           e.cbdtcqbm_vchr,  
                           decode(f.itemchargetype_vchr, '3', 3, 2) as fhxzbz,
                           a.totaldiffcostmoney_dec,
                           DECODE(g.ipchargeflg_int,
                                  1,
                                  round(g.tradeprice_mny / g.packqty_dec, 4),
                                  0,
                                  g.tradeprice_mny,
                                  round(g.tradeprice_mny / g.packqty_dec, 4)) tradeprice_mny
                      from t_opr_bih_patientcharge a
                     inner join t_opr_bih_register b
                        on a.registerid_chr = b.registerid_chr
                     inner join t_opr_bih_registerdetail c
                        on b.registerid_chr = c.registerid_chr
                     inner join t_bse_employee d
                        on b.casedoctor_chr = d.empid_chr
                      left join t_ins_cszyreg e
                        on a.registerid_chr = e.registerid_vchr
                       and e.cybz_vchr = 1
                      left join t_opr_bih_orderchargedept f
                        on a.chargeitemid_chr = f.chargeitemid_chr
                       and a.orderid_chr = f.orderid_chr
                       and f.itemchargetype_vchr = '3'
                     inner join t_bse_chargeitem g
                        on a.chargeitemid_chr = g.itemid_chr
                     where (a.pstatus_int = 1 or a.pstatus_int = 2)
                       and a.status_int = 1
                       and a.chargeactive_dat is not null
                       and a.sendflag_int = 0
                       and a.chargeactive_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                           to_date(?, 'yyyy-mm-dd hh24:mi:ss')";

            if (!string.IsNullOrEmpty(jzjlh))
            {
                sql += "and e.jzjlh_vchr = '" + jzjlh + "'";
            }

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out paraArr);

                paraArr[0].DbType = DbType.String;
                paraArr[0].Value = p_dateBegin.ToString("yyyy-MM-dd") + " 00:00:00";

                paraArr[1].DbType = DbType.String;
                paraArr[1].Value = p_dateEnd.ToString("yyyy-MM-dd") + " 23:59:59";

                //paraArr[2].DbType = DbType.DateTime;
                //paraArr[2].Value = DateTime.Parse(p_dateBegin.ToString("yyyy-MM-dd") + " 00:00:00");

                //paraArr[3].DbType = DbType.DateTime;
                //paraArr[3].Value = DateTime.Parse(p_dateEnd.ToString("yyyy-MM-dd") + " 23:59:59");

                lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtResult, paraArr);
                if (lngRes > 0)
                {
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        lstDgzyxmcsVo = new List<clsDGZyxmcs_VO>();
                        int intRowCount = dtResult.Rows.Count;
                        DataRow drTmp = null;
                        clsDGZyxmcs_VO objDgzyxmcsVo = null;
                        int intSortno = 1;
                        decimal decTotalMoney = 0, decTotalDiffCostMoney = 0, decUPrice = 0;
                        decimal decDiscount = 0;
                        for (int i = 0; i < intRowCount; i++)
                        {
                            //need modify 赋值，注意有些字段有长度限制
                            drTmp = dtResult.Rows[i];
                            objDgzyxmcsVo = new clsDGZyxmcs_VO();
                            objDgzyxmcsVo.JZJLH = drTmp["jzjlh_vchr"].ToString();//就诊记录号
                            objDgzyxmcsVo.GRSHBZH = drTmp["idcard_chr"].ToString();//身份证号
                            objDgzyxmcsVo.ZYH = drTmp["inpatientid_chr"].ToString();//住院号
                            intSortno = intSortno + i;
                            objDgzyxmcsVo.XMXH = intSortno.ToString().PadLeft(6, '0');//项目序号
                            objDgzyxmcsVo.CFXMWYH = drTmp["pchargeid_chr"].ToString();//处方项目唯一号
                            objDgzyxmcsVo.YYXMBM = drTmp["chargeitemid_chr"].ToString();//医院项目编码
                            objDgzyxmcsVo.XMMC = drTmp["chargeitemname_chr"].ToString();//项目名称
                            objDgzyxmcsVo.YYFLDM = drTmp["invcateid_chr"].ToString();//医院分类代码
                            objDgzyxmcsVo.YPGG = drTmp["spec_vchr"].ToString();//规格
                            objDgzyxmcsVo.YPJX = "";//drTmp[""].ToString();//剂型

                            objDgzyxmcsVo.MCYL = this.Dec(drTmp["amount_dec"].ToString());//数量 NUMBER	(8,2)
                            decTotalMoney = this.Dec(drTmp["totalmoney_dec"].ToString());
                            decTotalDiffCostMoney = string.IsNullOrEmpty(drTmp["totaldiffcostmoney_dec"].ToString()) ? 0 : this.Round(this.Dec(drTmp["totaldiffcostmoney_dec"].ToString()), 2);
                            if (p_blnDiffCostOn)
                            {
                                //decTotalMoney -= Math.Abs(decTotalDiffCostMoney);
                                decTotalMoney += decTotalDiffCostMoney;
                                //decUPrice = this.Dec(drTmp["tradeprice_mny"].ToString());
                                //if (decUPrice == 0)
                                //    decUPrice = this.Dec(drTmp["unitprice_dec"].ToString());
                                //objDgzyxmcsVo.JG = decUPrice;//单价 NUMBER	(12,4)

                                if (objDgzyxmcsVo.MCYL == 0)
                                {
                                    objDgzyxmcsVo.JG = this.Dec(drTmp["unitprice_dec"].ToString());
                                }
                                else
                                {
                                    objDgzyxmcsVo.JG = this.Round(decTotalMoney / objDgzyxmcsVo.MCYL, 4);       //价格统一计算。合计金额/数量。
                                }
                            }
                            else
                                objDgzyxmcsVo.JG = this.Dec(drTmp["unitprice_dec"].ToString());//单价 NUMBER	(12,4)
                            objDgzyxmcsVo.JE = decTotalMoney;//金额 NUMBER	(12,2) 该条记录的总费用金额
                            decDiscount = this.Dec(drTmp["discount_dec"].ToString()) / 100;
                            objDgzyxmcsVo.ZFEIBL = decDiscount;//自费比例	NUMBER	(5,2)		0< = X <= 1 医保应该不会按这个比例计算，可传0
                            objDgzyxmcsVo.ZFEIJE = decTotalMoney * decDiscount;//自费金额	NUMBER	(16,2)
                            objDgzyxmcsVo.FHXZBZ = drTmp["fhxzbz"].ToString();//符合限制标志
                            objDgzyxmcsVo.JZSJ = Convert.ToDateTime(drTmp["chargeactive_dat"].ToString()).ToString("yyyyMMddHHmmss");//记账时间	NUMBER	14	N	yyyymmddhh24miss
                            objDgzyxmcsVo.YSGH = drTmp["empno_chr"].ToString();//医师工号
                            objDgzyxmcsVo.BZ3 = "";//备注3
                            objDgzyxmcsVo.CBDTCQBM = drTmp["cbdtcqbm_vchr"].ToString();     // 参保地统筹区编码 
                            lstDgzyxmcsVo.Add(objDgzyxmcsVo);
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取住院医保结算所需数据

        /// <summary>
        /// 获取住院医保结算所需数据
        /// </summary>
        /// <param name="strJslb">界面上传入1 出院结算 2 中途结算3 预结算</param>
        /// <param name="strInvNo">发票号码</param>
        /// <param name="strRegisterId">住院登记号</param>
        /// <param name="objDgzydyxsVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetZYYBjs(string strJslb, string strInvNo, string strZDZMHM, decimal decTotal, string strRegisterId, out clsDGZyjs_VO objDgzyjsVo, bool p_blnDiffOn)
        {
            long lngRes = -1;
            //need modify 
            string sql = @"select a.inpatientid_chr,
                                   b.lastname_vchr,
                                   b.idcard_chr,
                                   decode(b.mobile_chr, null, b.homephone_vchr, b.mobile_chr) as contactphone,
                                   c.zylb_vchr,
                                   c.jzlb_vchr,
                                   c.jzjlh_vchr,
                                   c.cbdtcqbm_vchr, 
                                   c.outstatus,  
                                   '' diagnose_vchr,
                                   d.outhospital_dat,
                                   (to_date(to_char(nvl(d.outhospital_dat, sysdate), 'yyyy-mm-dd'),
                                            'yyyy-mm-dd') -
                                   to_date(to_char(a.inpatient_dat, 'yyyy-mm-dd'), 'yyyy-mm-dd')) indays,
                                   e.insdeptcode_vchr,
                                   e.hosdeptname_vchr,
                                   f.code_chr
                              from t_opr_bih_register       a,
                                   t_opr_bih_registerdetail b,
                                   t_ins_cszyreg            c,
                                   t_opr_bih_leave          d,
                                   t_ins_deptrel            e,
                                   t_bse_bed                f,
                                   t_opr_bih_transfer       g
                             where a.registerid_chr = b.registerid_chr
                               and b.registerid_chr = c.registerid_vchr
                               and a.registerid_chr = d.registerid_chr(+)
                               and c.cybz_vchr <> '3'  
                               {0}
                               and a.registerid_chr = g.registerid_chr
                               and g.targetbedid_chr = f.bedid_chr
                               {1}
                               and a.registerid_chr = ?
                             order by g.modify_dat desc";

            if (strJslb.Equals("1"))
            {
                sql = sql.Replace("{0}", "and d.outareaid_chr = e.hosdeptid_vchr");
                sql = sql.Replace("{1}", "and g.type_int = 7");
            }
            else
            {
                sql = sql.Replace("{0}", "and a.areaid_chr= e.hosdeptid_vchr");
                sql = sql.Replace("{1}", "and g.type_int = 5");
            }
            objDgzyjsVo = new clsDGZyjs_VO();
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = strRegisterId;
                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtResult, paraArr);
                string strMaxDate;
                string strMinDate;
                lngRes = m_lngGetZYFYSJ(strRegisterId, out strMaxDate, out strMinDate);
                if (lngRes > 0)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        DataRow dr = dtResult.Rows[0];
                        objDgzyjsVo = new clsDGZyjs_VO();
                        objDgzyjsVo.JZJLH = dr["jzjlh_vchr"].ToString();//就诊记录号
                        objDgzyjsVo.GMSFHM = dr["idcard_chr"].ToString();//公民身份号码
                        objDgzyjsVo.JZLB = dr["jzlb_vchr"].ToString().Split('-')[0].ToString();//就诊类别
                        objDgzyjsVo.JSLX = strJslb;//界面上传入1 出院结算 2 中途结算3 预结算
                        objDgzyjsVo.CYKS = dr["insdeptcode_vchr"].ToString();//出院科室 t_ins_deptrel
                        objDgzyjsVo.YYCYKS = dr["hosdeptname_vchr"].ToString();//医院出院科室.医院自己命名的科室名称
                        objDgzyjsVo.JDZFBL = 0; //降低支付比例 0<=X <= 1X为两位小数，初始可默认为0
                        objDgzyjsVo.CWH = dr["code_chr"].ToString();//床位号
                        objDgzyjsVo.CYZD = dr["diagnose_vchr"].ToString().Length == 0 ? "中途结算" : dr["diagnose_vchr"].ToString();//出院诊断
                        objDgzyjsVo.JSQSRQ = Convert.ToDateTime(strMinDate.Length == 0 ? System.DateTime.Now.ToString() : strMinDate).ToString("yyyyMMdd");//结算起始日期（是否指费用发生的最早日期） YYYYMMDD不可大于当前日期
                        objDgzyjsVo.JSZZRQ = Convert.ToDateTime(strMaxDate.Length == 0 ? System.DateTime.Now.ToString() : strMaxDate).ToString("yyyyMMdd");//结算终止日期（是否指费用发生的最晚日期） YYYYMMDD不可大于当前日期
                        int intDays = int.Parse(dr["indays"].ToString());
                        objDgzyjsVo.JSTS = intDays == 0 ? 1 : intDays;//结算天数 出院日期 – 入院日期 = 0 则住院天数取1，否则：住院天数取出院日期 – 入院日期
                        if (dr["outhospital_dat"].ToString().Length > 0)
                        {
                            objDgzyjsVo.CYRQ = Convert.ToDateTime(dr["outhospital_dat"].ToString()).ToString("yyyyMMdd");
                        }
                        else
                        {
                            objDgzyjsVo.CYRQ = System.DateTime.Now.ToString("yyyyMMdd");
                        }
                        objDgzyjsVo.FPHM = strInvNo;//发票号码 
                        objDgzyjsVo.ZDZMHM = strZDZMHM;//界面录入 诊断证明号码 “结算类型” 选择 “出院结算”则必录 
                        objDgzyjsVo.LXDH = dr["contactphone"].ToString();//联系电话
                        objDgzyjsVo.BZ = "";//备注
                        objDgzyjsVo.ZYFYZE = decTotal;//也可以从界面传入 住院总金额	NUMBER	(16,2)	N	录入本次结算对应的医疗总费用，用于后台校验
                        objDgzyjsVo.CBDTCQBM = dr["cbdtcqbm_vchr"].ToString(); //参保地统筹区编码 
                        objDgzyjsVo.CYYY = dr["outstatus"].ToString(); //出院原因 字典项【异地就医平台预留字段】 
                        objDgzyjsVo.JBR = "";//界面传入

                        //                        if (p_blnDiffOn)
                        //                            sql = @"select sum(round(a.unitprice_dec*a.amount_dec,2)) as totalSum, sum(round(a.totaldiffcostmoney_dec,2)) as totaldiffmoney
                        //                                  from t_opr_bih_patientcharge a
                        //                                 where a.pstatus_int <> 0
                        //                                   and a.status_int = 1
                        //                                   and a.chargeactive_dat is not null
                        //                                   and a.registerid_chr = ?";
                        //                        else
                        //                            sql = @"select sum(round(a.unitprice_dec*a.amount_dec, 2))as totalmoney
                        //                                  from t_opr_bih_patientcharge a
                        //                                 where a.pstatus_int <> 0
                        //                                   and a.status_int = 1
                        //                                   and a.chargeactive_dat is not null
                        //                                   and a.registerid_chr = ?";
                        //                        objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                        //                        paraArr[0].Value = strRegisterId;
                        //                        dtResult = new DataTable();
                        //                        lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtResult, paraArr);
                        //                        if (dtResult.Rows.Count > 0)
                        //                        {
                        //                            if (p_blnDiffOn)
                        //                            {
                        //                                decimal decTotalSum = string.IsNullOrEmpty(dtResult.Rows[0]["totalSum"].ToString().Trim()) ? 0 : this.Dec(dtResult.Rows[0]["totalSum"].ToString().Trim());
                        //                                decimal decTotalDiffSum = string.IsNullOrEmpty(dtResult.Rows[0]["totaldiffmoney"].ToString().Trim()) ? 0 : this.Dec(dtResult.Rows[0]["totaldiffmoney"].ToString().Trim());
                        //                                objDgzyjsVo.ZYFYZE = decTotalSum +decTotalDiffSum;
                        //                            }
                        //                            else
                        //                                objDgzyjsVo.ZYFYZE = string.IsNullOrEmpty(dtResult.Rows[0]["totalmoney"].ToString().Trim()) ? 0 : this.Dec(dtResult.Rows[0]["totalmoney"].ToString().Trim());//住院总金额 录入本次结算对应的医疗总费用，用于后台校验
                        //                            //if (Math.Abs(objDgzyjsVo.ZYFYZE - decTotal) <= this.Dec(0.1))//由于在界面的数据是四舍五入之后的,故会存在有误差,在这里作一个比较,若误差范围在5分钱之内的,则按界面的值为准,以通过医保校验,用于茶山2015-1-1零差价上线时的紧急方式,后待进一步更严谨的方法。 Add by 吴汉明 2014-12-31 
                        //                            //    objDgzyjsVo.ZYFYZE = decTotal;
                        //                            if (objDgzyjsVo.ZYFYZE - decTotal >=1 || objDgzyjsVo.ZYFYZE - decTotal<=-1)
                        //                            {
                        //                                return -1;
                        //                            }
                        objDgzyjsVo.ZYFYZE = decTotal;//界面传入 
                        //}
                    }
                }

            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取住院医保结算所需数据
        /// </summary>
        /// <param name="strJslb">界面上传入1 出院结算 2 中途结算3 预结算</param>
        /// <param name="strInvNo">发票号码</param>
        /// <param name="strRegisterId">住院登记号</param>
        /// <param name="objDgzydyxsVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetZYYBjs(string strJslb, string strInvNo, string strZDZMHM, decimal decTotal, string strRegisterId, out clsDGZyjs_VO objDgzyjsVo)
        {
            long lngRes = -1;
            //need modify 
            string sql = @"select a.inpatientid_chr,
                                   b.lastname_vchr,
                                   b.idcard_chr,
                                   decode(b.mobile_chr, null, b.homephone_vchr, b.mobile_chr) as contactphone,
                                   c.zylb_vchr,
                                   c.jzlb_vchr,
                                   c.jzjlh_vchr,
                                   '' diagnose_vchr,
                                   d.outhospital_dat,
                                   (to_date(to_char(nvl(d.outhospital_dat, sysdate), 'yyyy-mm-dd'),
                                            'yyyy-mm-dd') -
                                   to_date(to_char(a.inpatient_dat, 'yyyy-mm-dd'), 'yyyy-mm-dd')) indays,
                                   e.insdeptcode_vchr,
                                   e.hosdeptname_vchr,
                                   f.code_chr
                              from t_opr_bih_register       a,
                                   t_opr_bih_registerdetail b,
                                   t_ins_cszyreg            c,
                                   t_opr_bih_leave          d,
                                   t_ins_deptrel            e,
                                   t_bse_bed                f,
                                   t_opr_bih_transfer       g
                             where a.registerid_chr = b.registerid_chr
                               and b.registerid_chr = c.registerid_vchr
                               and a.registerid_chr = d.registerid_chr(+)
                               and c.cybz_vchr <> '3'  
                               {0}
                               and a.registerid_chr = g.registerid_chr
                               and g.targetbedid_chr = f.bedid_chr
                               and rownum = 1
                               {1}
                               and a.registerid_chr = ?
                             order by g.modify_dat desc";

            if (strJslb.Equals("1"))
            {
                sql = sql.Replace("{0}", "and d.outareaid_chr = e.hosdeptid_vchr");
                sql = sql.Replace("{1}", "and g.type_int = 7");
            }
            else
            {
                sql = sql.Replace("{0}", "and a.areaid_chr= e.hosdeptid_vchr");
                sql = sql.Replace("{1}", "and g.type_int = 5");
            }
            objDgzyjsVo = new clsDGZyjs_VO();
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = strRegisterId;
                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtResult, paraArr);
                string strMaxDate;
                string strMinDate;
                lngRes = m_lngGetZYFYSJ(strRegisterId, out strMaxDate, out strMinDate);
                if (lngRes > 0)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        objDgzyjsVo = new clsDGZyjs_VO();
                        objDgzyjsVo.JZJLH = dtResult.Rows[0]["jzjlh_vchr"].ToString();//就诊记录号
                        objDgzyjsVo.GMSFHM = dtResult.Rows[0]["idcard_chr"].ToString();//公民身份号码
                        objDgzyjsVo.JZLB = dtResult.Rows[0]["jzlb_vchr"].ToString().Split('-')[0].ToString();//就诊类别
                        objDgzyjsVo.JSLX = strJslb;//界面上传入1 出院结算 2 中途结算3 预结算
                        objDgzyjsVo.CYKS = dtResult.Rows[0]["insdeptcode_vchr"].ToString();//出院科室 t_ins_deptrel
                        objDgzyjsVo.YYCYKS = dtResult.Rows[0]["hosdeptname_vchr"].ToString();//医院出院科室.医院自己命名的科室名称
                        objDgzyjsVo.JDZFBL = 0; //降低支付比例 0<=X <= 1X为两位小数，初始可默认为0
                        objDgzyjsVo.CWH = dtResult.Rows[0]["code_chr"].ToString();//床位号
                        objDgzyjsVo.CYZD = dtResult.Rows[0]["diagnose_vchr"].ToString().Length == 0 ? "中途结算" : dtResult.Rows[0]["diagnose_vchr"].ToString();//出院诊断
                        objDgzyjsVo.JSQSRQ = Convert.ToDateTime(strMinDate.Length == 0 ? System.DateTime.Now.ToString() : strMinDate).ToString("yyyyMMdd");//结算起始日期（是否指费用发生的最早日期） YYYYMMDD不可大于当前日期
                        objDgzyjsVo.JSZZRQ = Convert.ToDateTime(strMaxDate.Length == 0 ? System.DateTime.Now.ToString() : strMaxDate).ToString("yyyyMMdd");//结算终止日期（是否指费用发生的最晚日期） YYYYMMDD不可大于当前日期
                        int intDays = int.Parse(dtResult.Rows[0]["indays"].ToString());
                        objDgzyjsVo.JSTS = intDays == 0 ? 1 : intDays;//结算天数 出院日期 – 入院日期 = 0 则住院天数取1，否则：住院天数取出院日期 – 入院日期
                        if (dtResult.Rows[0]["outhospital_dat"].ToString().Length > 0)
                        {
                            objDgzyjsVo.CYRQ = Convert.ToDateTime(dtResult.Rows[0]["outhospital_dat"].ToString()).ToString("yyyyMMdd");
                        }
                        else
                        {
                            objDgzyjsVo.CYRQ = System.DateTime.Now.ToString("yyyyMMdd");
                        }
                        objDgzyjsVo.FPHM = strInvNo;//发票号码 
                        objDgzyjsVo.ZDZMHM = strZDZMHM;//界面录入 诊断证明号码 “结算类型” 选择 “出院结算”则必录 
                        objDgzyjsVo.LXDH = dtResult.Rows[0]["contactphone"].ToString();//联系电话
                        objDgzyjsVo.BZ = "";//备注
                        objDgzyjsVo.ZYFYZE = decTotal;//也可以从界面传入 住院总金额	NUMBER	(16,2)	N	录入本次结算对应的医疗总费用，用于后台校验
                        objDgzyjsVo.CBDTCQBM = "";//参保地统筹区编码
                        objDgzyjsVo.CYYY = "";//出院原因 字典项【异地就医平台预留字段】
                        objDgzyjsVo.JBR = "";//界面传入

                        sql = @"select sum(round(a.unitprice_dec*a.amount_dec, 2)) as totalmoney
                                  from t_opr_bih_patientcharge a
                                 where a.pstatus_int <> 0
                                   and a.status_int = 1
                                   and a.chargeactive_dat is not null
                                   and a.registerid_chr = ?";

                        objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                        paraArr[0].Value = strRegisterId;
                        dtResult = new DataTable();
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtResult, paraArr);
                        if (dtResult.Rows.Count > 0)
                        {
                            objDgzyjsVo.ZYFYZE = this.Dec(dtResult.Rows[0]["totalmoney"].ToString().Trim());//住院总金额 录入本次结算对应的医疗总费用，用于后台校验
                        }
                    }
                }

            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取费用最大日期和最小日期
        /// <summary>
        /// 获取住院医保结算所需数据
        /// </summary>
        /// <param name="strRegisterId">住院登记号</param>
        /// <param name="strMaxDate">费用最大时间</param>
        /// <param name="strMinDate">费用最小时间</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetZYFYSJ(string strRegisterId, out string strMaxDate, out string strMinDate)
        {
            long lngRes = -1;
            strMaxDate = string.Empty;
            strMinDate = string.Empty;
            string sql = @"select max(a.create_dat) as maxdate, min(a.create_dat) as mindate
                                  from t_opr_bih_patientcharge a
                                 where a.status_int=1
                                   and a.registerid_chr = ?";
            // 由创建时间->生效时间(医保上传用的是生效时间). 数据库中存在create_dat大于chargeactive_dat的情况
            sql = @"select max(a.chargeactive_dat) as maxdate, min(a.chargeactive_dat) as mindate
                                  from t_opr_bih_patientcharge a
                                 where a.status_int=1
                                   and a.registerid_chr = ?";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = strRegisterId;
                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtResult, paraArr);
                if (lngRes > 0)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        strMaxDate = dtResult.Rows[0]["maxdate"].ToString();//最大费用时间
                        strMinDate = dtResult.Rows[0]["mindate"].ToString();//最小费用时间
                    }
                }

            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #endregion

        #region 获取住院医保出院登记所需数据
        /// <summary>
        /// 获取住院医保出院登记所需数据
        /// </summary>
        /// <param name="strRegisterId"></param>
        /// <param name="strJZJLH"></param>
        /// <param name="objDgzycydjVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetZYYBCydj(string strRegisterId, string strJZJLH, out clsDGZycydj_VO objDgzycydjVo)
        {
            long lngRes = -1;
            //need modify 
            string sql = @"select  a.inpatientid_chr,
                                   b.lastname_vchr,
                                   b.idcard_chr,
                                   decode(b.mobile_chr, null, b.homephone_vchr, b.mobile_chr) as contactphone,
                                   c.zylb_vchr,
                                   c.jzlb_vchr,
                                   c.jzjlh_vchr,
                                   c.icd10_1,
                                   c.icd10_2,
                                   c.icd10_3,
                                   c.outstatus,
                                   c.cbdtcqbm_vchr, 
                                   '' diagnose_vchr,
                                   d.outhospital_dat,
                                   (to_date(to_char(nvl(d.outhospital_dat, sysdate), 'yyyy-mm-dd'),
                                            'yyyy-mm-dd') -
                                   to_date(to_char(a.inpatient_dat, 'yyyy-mm-dd'), 'yyyy-mm-dd')) indays,                                   
                                   e.insdeptcode_vchr,
                                   e.hosdeptname_vchr,
                                   f.code_chr
                              from t_opr_bih_register       a,
                                   t_opr_bih_registerdetail b,
                                   t_ins_cszyreg            c,
                                   t_opr_bih_leave          d,
                                   t_ins_deptrel            e,
                                   t_bse_bed                f,
                                   t_opr_bih_transfer       g
                             where a.registerid_chr = b.registerid_chr
                               and b.registerid_chr = c.registerid_vchr
                               and a.registerid_chr = d.registerid_chr
                               and d.outareaid_chr = e.hosdeptid_vchr
                               and a.registerid_chr = g.registerid_chr
                               and g.targetbedid_chr = f.bedid_chr
                               and c.cybz_vchr <> '3'   
                               and rownum = 1
                               and g.type_int = 7
                               and d.status_int = 1
                               and a.registerid_chr = ?
                             order by g.modify_dat desc";
            objDgzycydjVo = new clsDGZycydj_VO();
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = strRegisterId;
                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(sql, ref dtResult, paraArr);
                if (lngRes > 0)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        DataRow dr = dtResult.Rows[0];
                        objDgzycydjVo.JZJLH = strJZJLH;// dtResult.Rows[0]["jzjlh_vchr"].ToString();//就诊记录号
                        objDgzycydjVo.GMSFHM = dr["idcard_chr"].ToString();//公民身份号码
                        objDgzycydjVo.JZLB = dr["jzlb_vchr"].ToString();//就诊类别
                        objDgzycydjVo.CYKS = dr["insdeptcode_vchr"].ToString();//出院科室 t_ins_deptrel
                        objDgzycydjVo.YYCYKS = dr["hosdeptname_vchr"].ToString();//医院出院科室.医院自己命名的科室名称
                        objDgzycydjVo.CWH = dr["code_chr"].ToString();//床位号
                        objDgzycydjVo.CYZD = dr["diagnose_vchr"].ToString().Length == 0 ? "出院诊断" : dr["diagnose_vchr"].ToString();//出院诊断 
                        int intDays = int.Parse(dr["indays"].ToString());
                        objDgzycydjVo.ZYTS = intDays == 0 ? 1 : intDays;//住院天数 出院日期 – 入院日期 = 0 则住院天数取1，否则：住院天数取出院日期 – 入院日期
                        objDgzycydjVo.CYRQ = Convert.ToDateTime(dr["outhospital_dat"].ToString()).ToString("yyyyMMdd");
                        objDgzycydjVo.LXDH = dr["contactphone"].ToString();//联系电话
                        objDgzycydjVo.BZ = "";//备注
                        objDgzycydjVo.CBDTCQBM = dr["cbdtcqbm_vchr"].ToString();//参保地统筹区编码
                        objDgzycydjVo.CYYY = dr["outstatus"].ToString();//出院原因 字典项【异地就医平台预留字段】
                        objDgzycydjVo.Icd10_1 = dr["icd10_1"].ToString();
                        objDgzycydjVo.Icd10_2 = dr["icd10_2"].ToString();
                        objDgzycydjVo.Icd10_3 = dr["icd10_3"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 加载医院经办人注册(记录)
        /// <summary>
        /// 加载医院经办人注册(记录)
        /// </summary>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetUserRegister(out clsDGYBjbrzc_VO[] p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new clsDGYBjbrzc_VO[0];
            string strSQL = @"select a.yybh, a.jbr, a.jbrlx, a.xm, a.gmsfhm, a.ssks, a.bglx, a.jlsj
                                  from t_ins_registeruser a
                                 where a.bglx in (1, 2)";
            clsHRPTableService objHRPSvc = null;
            try
            {
                DataTable dtbResult = new DataTable();
                objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    if (intRow > 0)
                    {
                        p_dtbResult = new clsDGYBjbrzc_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            p_dtbResult[i] = new clsDGYBjbrzc_VO();
                            p_dtbResult[i].YYBH = dtbResult.Rows[i]["yybh"].ToString().Trim();
                            p_dtbResult[i].JBR = dtbResult.Rows[i]["jbr"].ToString().Trim();
                            p_dtbResult[i].JBRLX = dtbResult.Rows[i]["jbrlx"].ToString().Trim();
                            p_dtbResult[i].XM = dtbResult.Rows[i]["xm"].ToString().Trim();
                            p_dtbResult[i].GMSFHM = dtbResult.Rows[i]["gmsfhm"].ToString().Trim();
                            p_dtbResult[i].SSKS = dtbResult.Rows[i]["ssks"].ToString().Trim();
                            p_dtbResult[i].BGLX = dtbResult.Rows[i]["bglx"].ToString().Trim();
                            p_dtbResult[i].JLSJ = dtbResult.Rows[i]["jlsj"].ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取门诊结算退款的相关信息
        /// <summary>
        /// 获取门诊结算退款的相关信息
        /// </summary>
        /// <param name="p_strSywh"></param>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCSYBChargeCancel(string p_strSywh, int flag, out clsDGExtra_VO objDgextraVo, out bool p_blRefund)
        {
            long lngRes = 0;
            p_blRefund = false;
            objDgextraVo = new clsDGExtra_VO();
            string strSQL = string.Empty;
            if (flag == 0)
            {
                strSQL = @"select a.jzjlh,a.sdywh
                              from t_opr_shm_paybilltrade a, t_opr_outpatientrecipeinv b
                             where a.RPID_VCHR = b.outpatrecipeid_chr
                             and b.isvouchers_int = 1
                             and b.invoiceno_vchr = ?";
            }
            else
            {
                strSQL = @"select  a.yybh,a.jzjlh,a.sdywh   from t_ins_chargemz_csyb a, t_opr_outpatientrecipeinv b
                                 where a.cfh = b.outpatrecipeid_chr
                                   and b.status_int = 1
                                   and b.invoiceno_vchr = ?";
            }
            clsHRPTableService objHRPSvc = null;
            try
            {
                DataTable dtResult = new DataTable();
                objHRPSvc = new clsHRPTableService();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = p_strSywh;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, paraArr);
                if (lngRes > 0 && dtResult != null)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        if (flag == 0)
                        {
                            if (!string.IsNullOrEmpty(dtResult.Rows[0]["jzjlh"].ToString()))
                            {
                                objDgextraVo.YYBH = "711014";
                                objDgextraVo.JZJLH = dtResult.Rows[0]["jzjlh"].ToString();
                                objDgextraVo.SDYWH = dtResult.Rows[0]["sdywh"].ToString();
                                p_blRefund = true;
                            }
                        }
                        else
                        {
                            objDgextraVo.YYBH = dtResult.Rows[0]["yybh"].ToString();
                            objDgextraVo.JZJLH = dtResult.Rows[0]["jzjlh"].ToString();
                            objDgextraVo.SDYWH = dtResult.Rows[0]["sdywh"].ToString();
                            p_blRefund = true;
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取医保结算单打印相关信息
        /// <summary>
        /// 获取医保结算单打印相关信息
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <param name="objDgextraVo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetYBChargeZY(string p_strRegisterId, out clsDGExtra_VO objDgextraVo)
        {
            long lngRes = 0;
            objDgextraVo = new clsDGExtra_VO();
            string strSQL = @"select  t.sdywh,
                                      t.jzjlh
                                 from t_ins_chargezy_csyb t
                                where t.registerid_chr=?";
            clsHRPTableService objHRPSvc = null;
            try
            {
                DataTable dtResult = new DataTable();
                objHRPSvc = new clsHRPTableService();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = p_strRegisterId;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, paraArr);
                if (lngRes > 0 && dtResult != null)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        objDgextraVo.SDYWH = dtResult.Rows[0]["sdywh"].ToString();
                        objDgextraVo.JZJLH = dtResult.Rows[0]["jzjlh"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取病人的基本信息
        /// <summary>
        /// 获取病人的基本信息ID
        /// </summary>
        /// <param name="p_InpatientID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfo(string p_InpatientID, string strFlag, out DataTable dtResult)
        {
            long lngRes = 0;
            dtResult = null;
            string strSub = string.Empty;
            string strSQL = @"select a.registerid_chr,
                                       a.patientid_chr,
                                       a.inpatientid_chr,
                                       a.pstatus_int,
                                       a.inpatientcount_int,
                                       a.feestatus_int,
                                       b.lastname_vchr,
                                       b.sex_chr,
                                       b.birth_dat,
                                       to_char(b.birth_dat, 'yyyy/mm/dd') as cssj,
                                       b.idcard_chr,
                                       to_char(a.inpatient_dat, 'yyyy/mm/dd hh24:mi:ss') as rysj,
                                       e.deptname_vchr,
                                       d.code_chr,
                                       to_char(c.outhospital_dat, 'yyyy/mm/dd hh24:mi:ss') as cysj,
                                       a.inpatientnotype_int,
                                       b.homeaddress_vchr,
                                       b.employer_vchr,
                                       f.patientcardid_chr,a.CASEDOCTOR_CHR,a.DIAGNOSEID_CHR,a.OUTDIAGNOSE_VCHR,h.lastname_vchr,b.homephone_vchr,m.CBDTCQBM_VCHR
                                  from t_opr_bih_register a,
                                       t_opr_bih_registerdetail b,
                                       (select registerid_chr, outhospital_dat
                                          from t_opr_bih_leave
                                         where status_int = 1) c,
                                       t_bse_bed d,
                                       t_bse_deptdesc e,
                                       t_bse_patientcard f,
                                       t_bse_employee h,
                                       t_ins_cszyreg m
                                 where a.registerid_chr = b.registerid_chr
                                   and a.patientid_chr = f.patientid_chr(+)
                                   and a.status_int = 1
                                   and a.registerid_chr = c.registerid_chr(+)
                                   and a.registerid_chr = d.bihregisterid_chr(+)
                                   and h.empid_chr = a.CASEDOCTOR_CHR(+)
                                   and a.bedid_chr = d.bedid_chr(+)
                                   and d.areaid_chr = e.deptid_chr(+)
                                   and a.registerid_chr = m.registerid_vchr(+)
                                   and ([strSub])
                                 order by a.patientid_chr, a.inpatient_dat ";
            if (strFlag == "0")
            {
                strSub = "a.inpatientid_chr = ?";
            }
            else if (strFlag == "1")
            {
                strSub = "f.patientcardid_chr = ?";
            }
            else
            {
                strSub = "b.idcard_chr = ?";
            }
            strSQL = strSQL.Replace("[strSub]", strSub);
            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                IDataParameter[] objIDParr = null;
                objHRPSvc.CreateDatabaseParameter(1, out objIDParr);
                objIDParr[0].Value = p_InpatientID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, objIDParr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取病人是否社保登记病人
        /// <summary>
        /// 获取病人是否社保登记病人
        /// </summary>
        /// <param name="p_InpatientID"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_lngGetPatientInfo(string p_strRegisterId)
        {
            bool blIsSbbr = false;
            string strSQL = @"select 1 from t_ins_cszyreg a where a.cybz_vchr=1 and a.status_chr<>-1 and a.registerid_vchr=?";
            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                DataTable dtResult = new DataTable();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = p_strRegisterId;
                long lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, paraArr);
                if (dtResult.Rows.Count > 0)
                {
                    blIsSbbr = true;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return blIsSbbr;
        }
        #endregion

        #region 获取病人资料
        /// <summary>
        /// 获取病人资料
        /// </summary>
        /// <param name="p_InpatientID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientInfo(string p_strID, out DataTable dt)
        {
            long lngRes = -1;
            dt = new DataTable();
            string strSQL = @"select *
                              from t_bse_patient a, t_bse_patientcard b
                             where a.patientid_chr = b.patientid_chr
                               and b.status_int > 0
                               and b.patientcardid_chr = ?";
            clsHRPTableService objHRPSvc = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = p_strID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, paraArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 检测是否已填住院诊断
        /// <summary>
        /// 新的住院诊断检测
        /// </summary>
        /// <param name="p_strCheckOutType"></param>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckDiagnose2(string p_strCheckOutType, string p_strRegisterID, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL1 = @"select t.maindiagnosis
                              from inhospitalmainrecord_content t, t_opr_bih_register r
                             where t.inpatientid = r.inpatientid_chr
                             and r.registerid_chr = ?
                             and t.lastmodifydate > r.inpatient_dat
                             order by t.lastmodifydate desc ";

            string strSQL2 = @"select t.primarydiagnose
                              from ipcasehistcont_primarydiagnose t, t_opr_bih_register r
                             where t.inpatientid = r.inpatientid_chr
                             and r.registerid_chr = ?
                             and t.lastmodifydate > r.inpatient_dat
                             order by t.lastmodifydate desc ";

            string strSQL = @"select a.inpatientid,
                                   b.inhospitaldiagnose_right,
                                   b.outhospitaldiagnose_right
                              from outhospitalrecord a, outhospitalrecordcontent b,t_opr_bih_register c,t_bse_hisemr_relation e
                             where c.registerid_chr = ?
                             and c.registerid_chr = e.registerid_chr 
                             and a.inpatientdate = e.emrinpatientdate
                               and a.status = 0
                               and b.inpatientid = a.inpatientid
                               and b.inpatientdate = a.inpatientdate
                               and b.opendate = a.opendate
                               and b.modifydate = (select max(modifydate)
                                                     from outhospitalrecordcontent
                                                    where inpatientid = a.inpatientid
                                                      and inpatientdate = a.inpatientdate
                                                      and opendate = a.opendate)
                                                      order by a.opendate desc";

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {

                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = p_strRegisterID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, paraArr);
                if (p_dtResult == null || p_dtResult.Rows.Count == 0)
                {
                    strSQL = @" select a.inpatientid,
                                       a.inhospitaldiagnose1  as inhospitaldiagnose_right,
                                       a.outhospitaldiagnose1 as outhospitaldiagnose_right
                                  from t_emr_outhospitalin24hours a
                                 inner join t_opr_bih_register c
                                    on a.inpatientid = c.inpatientid_chr
                                   and c.registerid_chr = ?
                                 inner join t_bse_hisemr_relation e
                                    on c.registerid_chr = e.registerid_chr
                                   and a.inpatientdate = e.emrinpatientdate
                                 where a.status = 0
                                 order by a.opendate desc";

                    objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                    paraArr[0].Value = p_strRegisterID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, paraArr);
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                if (objHRPSvc != null)
                {
                    objHRPSvc.Dispose();
                    objHRPSvc = null;
                }
            }
            return lngRes;
        }
        #endregion

        #region 通过住院号来获取社保病人的就诊记录号
        /// <summary>
        /// 通过住院号来获取社保病人的就诊记录号
        /// </summary>
        /// <param name="p_strID"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetJZJLHbyInpatientID(string p_strID, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @"select b.jzjlh_vchr, b.registerid_vchr, b.jzjlh_vchr
                                      from t_opr_bih_register a, t_ins_cszyreg b
                                     where a.registerid_chr = b.registerid_vchr
                                       and a.inpatientid_chr = ?
                                       and b.cybz_vchr <> '3'   
                                     order by b.operatime_dat desc";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = p_strID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, paraArr);
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 通过就诊记录号来获取社保病人的结算号
        /// <summary>
        /// 通过就诊记录号来获取社保病人的结算号
        /// </summary>
        /// <param name="p_strID"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetJSHbyJZJLH(string p_strID, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @" select * from t_ins_chargezy_csyb t  where t.jzjlh = ?";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] paraArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out paraArr);
                paraArr[0].Value = p_strID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, paraArr);
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 获取社保、医院科室或病人身份对应情况数据
        /// <summary>
        /// 获取社保、医院科室或病人身份对应情况数据
        /// </summary>
        /// <param name="intType">1:科室； 2:病人身份</param>
        /// <param name="dtHosp"></param>
        /// <param name="dtItemsCorr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetHosYBData(int intType, out DataTable dtHosp, out DataTable dtItemsCorr)
        {
            long lngRes = 0;
            dtHosp = new DataTable();
            dtItemsCorr = new DataTable();
            string strSQL = "";
            string strSQL1 = "";
            if (intType == 1)
            {
                strSQL = @"select t.deptid_chr id,t.deptname_vchr name from t_bse_deptdesc t order by t.deptid_chr";
                strSQL1 = @"select * from t_ins_deptrel t where t.type_int = 1";
            }
            else
            {
                strSQL = @" select t.paytypeid_chr id ,t.paytypename_vchr name from t_bse_patientpaytype t order by t.paytypeid_chr";
                strSQL1 = @"select * from t_ins_deptrel t where t.type_int = 2";
            }
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtHosp);
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL1, ref dtItemsCorr);
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 根据就诊记录号来获取社保卡验证信息
        /// <summary>
        /// 根据就诊记录号来获取社保卡验证信息
        /// </summary>
        /// <param name="strJzjlh"></param>
        /// <param name="objDGExtraVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetYBPswCheckInfo(string strJzjlh, ref clsDGExtra_VO objDGExtraVO, out DateTime dtmFyrq)
        {
            long lngRes = -1;
            dtmFyrq = DateTime.Now;
            string strSQL = @"select a.jzlb_vchr,a.zylb_vchr,b.idcard_chr from t_ins_cszyreg a, t_opr_bih_registerdetail b where a.registerid_vchr = b.registerid_chr
and  a.jzjlh_vchr = ? and a.cybz_vchr <> '3'  
and a.status_chr >= 0";
            clsHRPTableService objHRPSvc = null;
            try
            {
                IDataParameter[] objIDParam = null;
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out objIDParam);
                objIDParam[0].Value = strJzjlh;
                DataTable dtTemp = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtTemp, objIDParam);
                if (lngRes > 0 && dtTemp.Rows.Count > 0)
                {
                    objDGExtraVO.GMSFHM = dtTemp.Rows[0]["idcard_chr"].ToString().Trim();
                    objDGExtraVO.JZLB = dtTemp.Rows[0]["jzlb_vchr"].ToString().Trim();
                    objDGExtraVO.ZYLB = dtTemp.Rows[0]["zylb_vchr"].ToString().Trim();

                    strSQL = @" select max(t.active_dat) as fyrq
                                   from t_opr_bih_patientcharge t
                                  where t.registerid_chr in
                                        (select b.registerid_chr
                                           from t_ins_cszyreg a, t_opr_bih_registerdetail b
                                          where a.registerid_vchr = b.registerid_chr
                                            and a.jzjlh_vchr = ? 
                                            and a.cybz_vchr <> '3'
                                            and a.status_chr >= 0)";
                    objHRPSvc.CreateDatabaseParameter(1, out objIDParam);
                    objIDParam[0].Value = strJzjlh;
                    dtTemp = new DataTable();
                    objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtTemp, objIDParam);
                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        dtmFyrq = Convert.ToDateTime(dtTemp.Rows[0]["fyrq"].ToString());
                    }
                }
            }
            catch (Exception objEx)
            {
                new clsLogText().LogError(objEx);
            }
            finally
            {
                if (objHRPSvc != null)
                {
                    objHRPSvc.Dispose();
                    objHRPSvc = null;
                }
            }
            return lngRes;
        }
        #endregion

        #region ICD10诊断
        /// <summary>
        /// ICD10诊断
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetIcd10()
        {
            string Sql = string.Empty;
            DataTable dt = null;
            try
            {
                Sql = @"select 1 as typeid,
                               t.icdcode_chr as icdcode,
                               t.icdname_vchr as icdname,
                               t.pycode_chr as pycode,
                               '' as wbcode
                          from t_aid_icd10 t";

                clsHRPTableService svc = new clsHRPTableService();
                svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                svc = null;
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx.Message);
            }
            return dt;
        }
        #endregion

        #region 将数值四舍五入
        /// <summary>
        /// 将数值四舍五入
        /// </summary>
        /// <param name="d">数值</param>
        /// <param name="decimals">小数位数</param>
        /// <returns></returns>
        public decimal Round(decimal d, int decimals)
        {
            try
            {
                if (decimals < 1)
                {
                    return this.Dec(Convert.ToInt32(d));
                }
                else
                {
                    string s = "0.";
                    for (int i = 0; i < decimals; i++)
                    {
                        s += "0";
                    }
                    return this.Dec(d.ToString(s));
                }
            }
            catch
            {
                return d;
            }
        }
        #endregion

        #region 行政区域代码
        /// <summary>
        /// 行政区域代码
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetAdministrative()
        {
            DataTable dt = new DataTable();
            string Sql = @"select fcode, fname from code_administrative";
            clsHRPTableService svc = null;
            try
            {
                svc = new clsHRPTableService();
                svc.lngGetDataTableWithoutParameters(Sql, ref dt);
            }
            catch (Exception ex)
            {
                clsLogText log = new clsLogText();
                log.LogError(ex);
            }
            finally
            {
                svc.Dispose();
                svc = null;
            }
            return dt;
        }
        #endregion

        #region Dec
        public decimal Dec(string str)
        {
            decimal d = 0;
            decimal.TryParse(str, out d);
            return d;
        }
        public decimal Dec(object obj)
        {
            if (obj == null)
                return 0;
            else
                return Dec(obj.ToString());
        }
        #endregion

        #region 使用启用儿童价格
        /// <summary>
        /// 使用启用儿童价格
        /// </summary>
        /// <returns></returns>
        [AutoComplete]
        public bool IsUseChildPrice()
        {
            try
            {
                DataTable dt = null;
                string Sql = @"select t.parmvalue_vchr from t_bse_sysparm t where t.parmcode_chr = '9015' and t.status_int = 1";
                clsHRPTableService svc = new clsHRPTableService();
                svc.lngGetDataTableWithoutParameters(Sql, ref dt);
                svc = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    int val = 0;
                    int.TryParse(dt.Rows[0]["parmvalue_vchr"].ToString(), out val);
                    if (val == 1)
                    {
                        return true;
                    }
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx.Message);
            }
            return false;
        }
        #endregion
    }

    #region EntityUpData
    /// <summary>
    /// EntityUpData
    /// </summary>
    public class EntityUpData
    {
        public string pchargeid_chr { get; set; }
        public string chargeitemid_chr { get; set; }
        public string chargeitemname_chr { get; set; }
        public string spec_vchr { get; set; }
        public decimal unitprice_dec { get; set; }
        public decimal amount_dec { get; set; }
        public decimal totalmoney_dec { get; set; }
        public string invcateid_chr { get; set; }
        public decimal discount_dec { get; set; }
        public string chargeactive_dat { get; set; }
        public string inpatientid_chr { get; set; }
        public string idcard_chr { get; set; }
        public string empno_chr { get; set; }
        public string jzjlh_vchr { get; set; }
        public string cbdtcqbm_vchr { get; set; }
        public int fhxzbz { get; set; }
        public decimal totaldiffcostmoney_dec { get; set; }
        public decimal chargetotalsum { get; set; }
        public decimal tradeprice_mny { get; set; }
        public decimal buyprice_dec { get; set; }
    }
    #endregion
}
