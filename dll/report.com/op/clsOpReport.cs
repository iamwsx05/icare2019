using com.digitalwave.iCare.middletier.HRPService;
using com.digitalwave.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Text;
using weCare.Core.Entity;

namespace Report.Com
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsOpReport : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region 通过发票段统计发票数据
        /// <summary>
        /// 通过发票段统计发票数据 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOperatorId"></param>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRegisterStatDataByInvoArr(string p_strOperatorId, string p_strStartDate, string p_strEndDate, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();

            String strSQL = string.Empty;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] objParamerArr = null;

            try
            {
                if (p_strOperatorId.Trim().Length > 0)
                {
                    strSQL = @"
                        select a.registerid_chr,
                               a.invno_chr,
                               a.flag_int,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '001') as rpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '002') as dpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '003') as gpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '004') as cpayment_mny,

                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '001') as rcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '002') as dcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '003') as gcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '004') as ccharge,
                               a.registeremp_chr as empid_chr,
                               b.lastname_vchr
                        from t_opr_patientregister    a,
                             t_bse_employee           b
                        where a.registeremp_chr = b.empid_chr(+)
                          and a.balanceemp_chr is not null
                          and a.returnemp_chr is null 
                          and a.registeremp_chr = ?    
                          and a.invno_chr between ? and ?

                     union all

                        select a.registerid_chr,
                               a.invno_chr,
                               a.flag_int,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '001') as rpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '002') as dpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '003') as gpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '004') as cpayment_mny,

                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '001') as rcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '002') as dcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '003') as gcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '004') as ccharge,
                               a.returnemp_chr as empid_chr,
                               b.lastname_vchr
                        from t_opr_patientregister    a,
                             t_bse_employee           b
                        where a.returnemp_chr = b.empid_chr(+)
                          and a.balanceemp_chr is not null
                          and a.returnemp_chr is not null    
                          and a.returnemp_chr = ?
                          and a.invno_chr between ? and ?
                    order by invno_chr asc";

                    objHRPSvc.CreateDatabaseParameter(6, out objParamerArr);

                    objParamerArr[0].Value = p_strOperatorId;
                    objParamerArr[1].DbType = DbType.String;
                    objParamerArr[1].Value = p_strStartDate;
                    objParamerArr[2].DbType = DbType.String;
                    objParamerArr[2].Value = p_strEndDate;

                    objParamerArr[3].Value = p_strOperatorId;
                    objParamerArr[4].DbType = DbType.String;
                    objParamerArr[4].Value = p_strStartDate;
                    objParamerArr[5].DbType = DbType.String;
                    objParamerArr[5].Value = p_strEndDate;


                }
                else
                {
                    strSQL = @"
                        select a.registerid_chr,
                               a.invno_chr,
                               a.flag_int,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '001') as rpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '002') as dpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '003') as gpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '004') as cpayment_mny,

                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '001') as rcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '002') as dcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '003') as gcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '004') as ccharge,
                               a.registeremp_chr as empid_chr,
                               b.lastname_vchr
                       from t_opr_patientregister    a,
                            t_bse_employee           b
                       where a.registeremp_chr = b.empid_chr(+)
                         and a.balanceemp_chr is not null
                         and a.returnemp_chr is null     
                         and a.invno_chr between ? and ?

                     union all

                      select a.registerid_chr,
                             a.invno_chr,
                             a.flag_int,
                             (select payment_mny 
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '001') as rpayment_mny,
                             (select payment_mny 
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '002') as dpayment_mny,
                             (select payment_mny 
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '003') as gpayment_mny,
                             (select payment_mny 
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '004') as cpayment_mny,

                             (select payment_mny * discount_dec
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '001') as rcharge,
                             (select payment_mny * discount_dec
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '002') as dcharge,
                             (select payment_mny * discount_dec
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '003') as gcharge,
                             (select payment_mny * discount_dec
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '004') as ccharge,
                             a.returnemp_chr as empid_chr,
                             b.lastname_vchr
                        from t_opr_patientregister    a,
                             t_bse_employee           b
                        where a.returnemp_chr = b.empid_chr(+)
                          and a.balanceemp_chr is not null
                          and a.returnemp_chr is not null     
                          and a.invno_chr between ? and ?
                        order by invno_chr asc";

                    objHRPSvc.CreateDatabaseParameter(4, out objParamerArr);

                    objParamerArr[0].DbType = DbType.String;
                    objParamerArr[0].Value = p_strStartDate;
                    objParamerArr[1].DbType = DbType.String;
                    objParamerArr[1].Value = p_strEndDate;
                    objParamerArr[2].DbType = DbType.String;
                    objParamerArr[2].Value = p_strStartDate;
                    objParamerArr[3].DbType = DbType.String;
                    objParamerArr[3].Value = p_strEndDate;
                }


                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objParamerArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            objHRPSvc.Dispose();
            return lngRes;
        }

        #endregion

        #region 根据操作员Id和发票段查找门诊重打挂号发票信息
        /// <summary>
        /// 根据操作员Id和发票段查找门诊重打挂号发票信息 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOperatorId"></param>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetRegisterBillReprintByInvoArr(string p_strOperatorId, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            StringBuilder strSQL = new StringBuilder(@"
                            select a.sourceinvono_vchr,
                                   a.repprninvono_vchr,
                                   a.printemp_chr
                            from t_opr_invoicerepeatprint a,t_opr_patientregister b
                            where a.sourceinvono_vchr = b.invno_chr
                              and a.type_chr = 2
                              and a.printstatus_int = 0
                              and b.balanceemp_chr is not null
                              and b.returnemp_chr is null      
                              and b.invno_chr between ? and ?");

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] objParamerArr = null;
            IDataParameter[] tmp_objParamerArr = null;
            objHRPSvc.CreateDatabaseParameter(3, out tmp_objParamerArr);
            int m_intParamCount = 2;
            tmp_objParamerArr[0].DbType = DbType.String;
            tmp_objParamerArr[0].Value = p_strStartDate;
            tmp_objParamerArr[1].DbType = DbType.String;
            tmp_objParamerArr[1].Value = p_strEndDate;

            try
            {
                if (p_strOperatorId.Trim().Length > 0)
                {
                    strSQL.Append(@" and a.printemp_chr = ?");
                    ++m_intParamCount;
                    tmp_objParamerArr[m_intParamCount - 1].Value = p_strOperatorId;
                }

                objHRPSvc.CreateDatabaseParameter(m_intParamCount, out objParamerArr);
                for (int i1 = 0; i1 < m_intParamCount; i1++)
                {
                    objParamerArr[i1].Value = tmp_objParamerArr[i1].Value;
                    objParamerArr[i1].DbType = tmp_objParamerArr[i1].DbType;
                }


                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL.ToString(), ref p_dtResult, objParamerArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            objHRPSvc.Dispose();
            return lngRes;
        }

        #endregion

        #region 获取所有的收费员数据
        /// <summary>
        /// 获取所有的收费员数据
        /// </summary> 
        /// <param name="dtCheckMan"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckMan(out DataTable dtCheckMan)
        {
            long lngRes = 0;
            dtCheckMan = new DataTable();
            string strSQL = @"select distinct a.BALANCEEMP_CHR,b.lastname_vchr from T_OPR_PATIENTREGISTER a,t_bse_employee b where a.BALANCEEMP_CHR=b.empid_chr and a.BALANCEEMP_CHR is not null";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtCheckMan);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region 门诊未日结汇总报表
        /// <summary>
        /// 门诊未日结汇总报表
        /// </summary>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetNOCheckOutInvoice(string startDate, string endDate, out DataTable p_dtResult)
        {
            long lngRes = -1;
            p_dtResult = new DataTable();
            p_dtResult = null;

            string strSQL = @"select m.lastname_vchr,m.totalsum_mny, max(t.balance_dat) balance_dat
                              from t_opr_outpatientrecipeinv t,
                                   (select sum(a.totalsum_mny) totalsum_mny, a.opremp_chr,b.lastname_vchr
                                      from t_opr_outpatientrecipeinv a,t_bse_employee b
                                     where a.opremp_chr=b.empid_chr
                                     and a.balanceflag_int = 0
                                       and a.recorddate_dat between
                                           to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                           to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                     group by a.opremp_chr,b.lastname_vchr) m
                             where t.opremp_chr = m.opremp_chr 
                             group by m.totalsum_mny,t.opremp_chr,m.lastname_vchr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] arrPara = null;
                objHRPSvc.CreateDatabaseParameter(2, out arrPara);
                arrPara[0].Value = startDate;
                arrPara[1].Value = endDate;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrPara);
                objHRPSvc.Dispose();

            }
            catch (Exception objEx)
            {
                string strTem = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region 通过日期查询欠费病人
        /// <summary>
        /// 通过日期查询欠费病人
        /// </summary> 
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryArrearsPatientByDate(string p_strStartDate, string p_strEndDate, out DataTable p_dtResult, bool p_blnALL)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            clsHRPTableService objSvc = null;
            IDataParameter[] objParamArr = null;
            try
            {
                string strSQL = string.Empty;
                if (p_blnALL)
                {
                    strSQL = @"select a.patientname_chr,a.invoiceno_vchr,
                                   d.patientcardid_chr,
                                   b.sex_chr,
                                   a.outpatrecipeid_chr,
                                   b.idcard_chr,
                                   a.deptname_chr,
                                   b.homephone_vchr,
                                   a.totalsum_mny
                              from t_opr_outpatientrecipeinv a,
                                   t_bse_patient b,
                                   t_bse_patientcard d
                             where a.recorddate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                   to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                               and a.patientid_chr = b.patientid_chr
                               and a.patientid_chr = d.patientid_chr
                               and a.isvouchers_int = 2
                               and not exists
                               (select t.invoiceno_vchr
                                  from t_opr_outpatientrecipeinv t
                                 where t.isvouchers_int = 2
                                   and t.status_int = 2
                                   and a.invoiceno_vchr = t.invoiceno_vchr)";
                    objSvc = new clsHRPTableService();
                    objSvc.CreateDatabaseParameter(2, out objParamArr);
                    objParamArr[0].Value = p_strStartDate;
                    objParamArr[1].Value = p_strEndDate;
                    lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, objParamArr);
                }
                else
                {
                    strSQL = @"select m.outpatrecipeid_chr id, m.tolprice_mny --药品类
                  from (select a.tolprice_mny,
                               a.outpatrecipeid_chr,
                               'WM' medtype
                          from t_tmp_outpatientpwmrecipede a,
                               t_opr_outpatientrecipeinv   b
                         where a.outpatrecipeid_chr = b.outpatrecipeid_chr
                           and b.recorddate_dat between
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss')
                               and b.recorddate_dat between  ? and  ?
                               and b.isvouchers_int = 2
                        union all
                        select a.tolprice_mny,
                               a.outpatrecipeid_chr,
                               'CM' medtype
                          from t_tmp_outpatientcmrecipede a,
                               t_opr_outpatientrecipeinv  b
                         where a.outpatrecipeid_chr = b.outpatrecipeid_chr
                           and b.recorddate_dat between
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss')
                               and b.recorddate_dat between  ? and  ?
                               and b.isvouchers_int = 2
                        union all
                        select a.tolprice_mny,
                               a.outpatrecipeid_chr,
                               'QTH' medtype
                          from t_tmp_outpatientothrecipede a,
                               t_opr_outpatientrecipeinv   b
                         where a.outpatrecipeid_chr = b.outpatrecipeid_chr
                           and b.recorddate_dat between
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss')
                           and b.recorddate_dat between  ? and  ?
                           and b.isvouchers_int = 2) m,
                       (select a.outpatrecipeid_chr,
                               c.medicnetype_int,
                               decode(c.medicnetype_int,
                                      1,
                                      'WM',
                                      2,
                                      'CM',
                                      3,
                                      'QTH',
                                      4,
                                      '') as medtype,
                               decode(b.pstatus_int, 3, 1, 0) as issendmed
                          from t_opr_recipesendentry a,
                               t_opr_recipesend      b,
                               t_bse_medstore        c
                         where a.sid_int = b.sid_int
                           and b.medstoreid_chr = c.medstoreid_chr
                           and not exists (select t.outpatrecipeid_chr
                                  from t_opr_returnmed t
                                 where t.outpatrecipeid_chr = a.outpatrecipeid_chr)) n
                 where m.outpatrecipeid_chr = n.outpatrecipeid_chr
                   and m.medtype = n.medtype
                   and n.issendmed = 1
                union all
                select c.id, c.price totalsum_mny --检验、检查、手术、材料
                  from (select trim(a.orderid_int) outpatrecipedeid_chr,
                               a.outpatrecipeid_chr ID,
                               a.qty_dec * a.pricemny_dec price
                          from t_opr_outpatient_orderdic a,
                          t_opr_outpatientrecipeinv      b
                         where a.outpatrecipeid_chr = b.outpatrecipeid_chr
                          and b.recorddate_dat between
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss')
                          and b.recorddate_dat between  ? and  ?
                          and b.isvouchers_int = 2
                        union all
                        select a.outpatrecipedeid_chr,
                               a.outpatrecipeid_chr   id,
                               a.unitprice_mny        price
                          from t_tmp_outpatientothrecipede a,
                               t_opr_outpatientrecipeinv   b
                         where a.outpatrecipeid_chr = b.outpatrecipeid_chr
                           and (a.usageparentid_vchr like '[PK]%' or
                               a.usageparentid_vchr is null)
                                and b.recorddate_dat between
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?,
                                       'yyyy-mm-dd hh24:mi:ss')
                               and b.recorddate_dat between  ? and  ?
                               and b.isvouchers_int = 2) c,
                       (select *
                          from t_opr_itemconfirm t
                         where t.status_int = 1) d
                 where c.id = d.outpatrecipeid_chr
                   and c.outpatrecipedeid_chr = d.outpatrecipedeid_chr(+)";
                    objSvc = new clsHRPTableService();
                    objSvc.CreateDatabaseParameter(20, out objParamArr);
                    objParamArr[0].Value = p_strStartDate;
                    objParamArr[1].Value = p_strEndDate;
                    objParamArr[2].Value = Convert.ToDateTime(p_strStartDate);
                    objParamArr[3].Value = Convert.ToDateTime(p_strEndDate);
                    objParamArr[4].Value = p_strStartDate;
                    objParamArr[5].Value = p_strEndDate;
                    objParamArr[6].Value = Convert.ToDateTime(p_strStartDate);
                    objParamArr[7].Value = Convert.ToDateTime(p_strEndDate);
                    objParamArr[8].Value = p_strStartDate;
                    objParamArr[9].Value = p_strEndDate;
                    objParamArr[10].Value = Convert.ToDateTime(p_strStartDate);
                    objParamArr[11].Value = Convert.ToDateTime(p_strEndDate);
                    objParamArr[12].Value = p_strStartDate;
                    objParamArr[13].Value = p_strEndDate;
                    objParamArr[14].Value = Convert.ToDateTime(p_strStartDate);
                    objParamArr[15].Value = Convert.ToDateTime(p_strEndDate);
                    objParamArr[16].Value = p_strStartDate;
                    objParamArr[17].Value = p_strEndDate;
                    objParamArr[18].Value = Convert.ToDateTime(p_strStartDate);
                    objParamArr[19].Value = Convert.ToDateTime(p_strEndDate);
                    DataTable dtTemp = new DataTable();
                    lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref dtTemp, objParamArr);
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    if (lngRes > 0 && dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            if (!dic.ContainsKey(dr["id"].ToString()))
                            {
                                dic.Add(dr["id"].ToString(), dtTemp.Compute("sum(tolprice_mny)", "id =" + dr["id"].ToString()).ToString());
                            }
                        }
                    }
                    else
                    {
                        return lngRes;
                    }
                    strSQL = @"select a.patientname_chr,
                                       a.invoiceno_vchr,
                                       d.patientcardid_chr,
                                       b.sex_chr,
                                       a.outpatrecipeid_chr,
                                       b.idcard_chr,
                                       a.deptname_chr,
                                       b.homephone_vchr,
                                       0 totalsum_mny
                                  from t_opr_outpatientrecipeinv a,
                                       t_bse_patient b,
                                       t_bse_patientcard d
                                 where a.patientid_chr = b.patientid_chr
                                   and a.patientid_chr = d.patientid_chr
                                   and a.isvouchers_int = 2
                                   and a.recorddate_dat between
                                                               to_date(?,
                                                                       'yyyy-mm-dd hh24:mi:ss') and
                                                               to_date(?,
                                                                       'yyyy-mm-dd hh24:mi:ss')
                                   and a.recorddate_dat between  ? and  ?
                                   and not exists (select t.invoiceno_vchr
                                          from t_opr_outpatientrecipeinv t
                                         where t.isvouchers_int = 2
                                           and t.status_int = 2
                                           and a.invoiceno_vchr = t.invoiceno_vchr)";
                    objParamArr = null;
                    objSvc.CreateDatabaseParameter(4, out objParamArr);
                    objParamArr[0].Value = p_strStartDate;
                    objParamArr[1].Value = p_strEndDate;
                    objParamArr[2].Value = Convert.ToDateTime(p_strStartDate);
                    objParamArr[3].Value = Convert.ToDateTime(p_strEndDate);
                    lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, objParamArr);
                    if (lngRes > 0 && p_dtResult.Rows.Count > 0)
                    {
                        foreach (DataRow dr in p_dtResult.Rows)
                        {
                            foreach (KeyValuePair<string, string> kvp in dic)
                            {
                                if (dr["outpatrecipeid_chr"].ToString() == kvp.Key.ToString())
                                {
                                    dr["totalsum_mny"] = Convert.ToDecimal(kvp.Value.ToString());
                                }
                            }
                        }
                    }
                    DataView dv = p_dtResult.DefaultView;
                    dv.RowFilter = "totalsum_mny > 0";
                    p_dtResult = dv.ToTable();
                }

            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx.Message);
            }
            finally
            {
                objSvc.Dispose();
                objSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 通过日期查询缴费病人
        /// <summary>
        /// 通过日期查询缴费病人
        /// </summary> 
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngQueryPayedPatientByDate(string p_strStartDate, string p_strEndDate, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();

            string strSQL = @"select a.patientname_chr,a.invoiceno_vchr,
                                       d.patientcardid_chr,
                                       b.sex_chr,
                                       a.outpatrecipeid_chr,
                                       b.idcard_chr,
                                       a.deptname_chr,
                                       b.homephone_vchr,
                                       a.totalsum_mny
                                  from t_opr_outpatientrecipeinv a, t_bse_patient b, t_bse_patientcard d
                                 where a.recorddate_dat between
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                   and a.patientid_chr = b.patientid_chr
                                   and a.patientid_chr = d.patientid_chr
                                   and exists (select t.outpatrecipeid_chr
                                          from t_opr_outpatientrecipeinv t
                                         where t.isvouchers_int = 2
                                           and t.status_int = 2
                                           and a.outpatrecipeid_chr = t.outpatrecipeid_chr)
                                   and not exists (select t.invoiceno_vchr
                                          from t_opr_outpatientrecipeinv t
                                         where t.isvouchers_int = 2
                                           and t.status_int = 2
                                           and a.invoiceno_vchr = t.invoiceno_vchr)";
            string strSQL1 = @"select a.patientname_chr,
                                       a.invoiceno_vchr,
                                       a.isvouchers_int,
                                       d.patientcardid_chr,
                                       b.sex_chr,
                                       a.outpatrecipeid_chr,
                                       b.idcard_chr,
                                       a.deptname_chr,
                                       b.homephone_vchr,
                                       a.totalsum_mny
                                  from t_opr_outpatientrecipeinv a, t_bse_patient b, t_bse_patientcard d
                                 where a.recorddate_dat between
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                   and a.patientid_chr = b.patientid_chr
                                   and a.patientid_chr = d.patientid_chr
                                   and exists
                                 (select m.outpatrecipedeid_old_chr
                                          from t_opr_recordselectfeeoperation m
                                         where a.outpatrecipeid_chr = m.outpatrecipedeid_new_chr)";

            clsHRPTableService objSvc = null;
            IDataParameter[] objParamArr = null;
            try
            {
                objSvc = new clsHRPTableService();
                objSvc.CreateDatabaseParameter(2, out objParamArr);
                objParamArr[0].Value = p_strStartDate;
                objParamArr[1].Value = p_strEndDate;
                lngRes = objSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, objParamArr);

                objParamArr = null;
                objSvc.CreateDatabaseParameter(2, out objParamArr);
                objParamArr[0].Value = p_strStartDate;
                objParamArr[1].Value = p_strEndDate;
                DataTable dtTemp = new DataTable();
                lngRes = objSvc.lngGetDataTableWithParameters(strSQL1, ref dtTemp, objParamArr);
                if (lngRes > 0 && dtTemp.Rows.Count > 0)
                {
                    p_dtResult.Merge(dtTemp);
                }
            }
            catch (Exception objEx)
            {
                clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx.Message);
            }
            finally
            {
                objSvc.Dispose();
                objSvc = null;
            }
            return lngRes;
        }
        #endregion

        #region 获取病区信息
        /// <summary>
        /// 获取病区信息
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <param name="Flag">1 科室 2 病区</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptArea(out DataTable dt)
        {
            long lngRes = 0;
            string SQL = "";

            SQL = @"select deptid_chr, modify_dat, deptname_vchr, category_int,
                               inpatientoroutpatient_int, operatorid_chr, address_vchr, pycode_chr,
                               attributeid, parentid, createdate_dat, status_int, deactivate_dat,
                               wbcode_chr, code_vchr, extendid_vchr, shortno_chr, stdbed_count_int,
                               putmed_int
                          from t_bse_deptdesc 
                         where status_int = 1 
                           
                      order by code_vchr";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
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

        #region 门诊医生绩效统计报表
        /// <summary>
        /// 门诊医生绩效统计报表
        /// </summary>
        /// <param name="p_beginDate"></param>
        /// <param name="p_endDate"></param>
        /// <param name="p_strStatType"></param>
        /// <param name="p_strDoctorID"></param>
        /// <param name="DeptIDArr">科室ID，数组</param>
        /// <param name="intFlag">标识，0按医生统计，1按科室统计</param>
        /// <param name="dtResult">返回datatable</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRptDoctorPerformance(string p_beginDate, string p_endDate, string p_strStatType, string p_strDoctorID, List<string> DeptIDArr, int intFlag, ref DataTable dtResult)
        {
            long lngRes = 0;
            p_beginDate += " 00:00:00";
            p_endDate += " 23:59:59";

            string strSQL = string.Empty;
            string strSQLSub = string.Empty;
            string strsqlsub = string.Empty;
            string strSQLSub1 = string.Empty;
            object[] objs = null;
            //            string strSQL0 = @"select parmvalue_vchr
            //                                              from t_bse_sysparm t
            //                                             where t.parmcode_chr = '3069'
            //                                               and t.status_int = 1";
            //            try
            //            {
            //                clsHRPTableService objSvc = new clsHRPTableService();
            //                DataTable dtTemp = new DataTable();
            //                long l = objSvc.lngGetDataTableWithoutParameters(strSQL0, ref dtTemp);
            //                if (l > 0 && dtTemp.Rows.Count > 0)
            //                {
            //                    string strValue = dtTemp.Rows[0][0].ToString();
            //                    string[] Val = strValue.Split('*');
            //                    strValue = string.Empty;
            //                    for (int k = 0; k < Val.Length; k++)
            //                    {
            //                        strValue = "'" + Val[k] + "',";
            //                    }
            //                    strSQLSub1 = @"and d.INSURANCETYPE_VCHR in (" + strValue.Substring(0, strValue.Length) + ")";
            //                }
            //            }
            //            catch (Exception objEx)
            //            {
            //                clsLogText objLog = new clsLogText();
            //                objLog.LogError(objEx);
            //            }

            DataTable dtT1 = new DataTable();
            DataTable dtT2 = new DataTable();
            DataTable dtT3 = new DataTable();
            DataTable dtT4 = new DataTable();
            DataTable dtT9 = new DataTable();
            DataTable dtT10 = new DataTable();
            DataTable dtT11 = new DataTable();
            DataTable dtT21 = new DataTable();

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (intFlag == 0)//按医生
            {
                #region T1
                string strSQLT1 = @"select mm.typeid_chr,
       mm.typename_vchr,
       mm.doctorid_chr,
       mm.empno_chr,
       mm.lastname_vchr,
       mm.code_vchr,
       mm.deptname_vchr,
       (mm.tolfee_mny +  nvl(tt.diffSum,0)) tolfee_mny,
       mm.jxywl,
       tt.diffSum
  from (select g.typeid_chr,
                                           g.typename_vchr,
                                           a.doctorid_chr, 
                                           e.empno_chr, 
                                           e.lastname_vchr,
                                           e.code_vchr,
                                           e.deptname_vchr,
                                           sum (b.tolfee_mny) tolfee_mny,
                                           sum(b.tolfee_mny*f.percentage) jxywl
                                      from t_opr_outpatientrecipeinv a,
                                           t_opr_outpatientrecipesumde b,
                                           (select e.empid_chr,
                                                       e.empno_chr,
                                                       e.lastname_vchr,
                                                       r.deptid_chr,
                                                       d.code_vchr,
                                                       d.deptname_vchr
                                                  from t_bse_employee e,
                                                       t_bse_deptemp r,
                                                       t_bse_deptdesc d
                                                 where r.deptid_chr = d.deptid_chr
                                                   and e.empid_chr = r.empid_chr
                                                   and r.default_dept_int = 1
                                                union all
                                                select e2.empid_chr,
                                                       e2.empno_chr,
                                                       e2.lastname_vchr,
                                                       '' deptid_chr,
                                                       '' code_vchr,
                                                       '' deptname_vchr
                                                  from t_bse_employee e2
                                                 where not exists (select ''
                                                          from t_bse_deptemp r2
                                                         where r2.empid_chr = e2.empid_chr
                                                           and r2.default_dept_int = 1)) e,
                                             t_opr_drachformula  f,
                                             t_bse_chargeitemextype g
                                     where a.seqid_chr = b.seqid_chr(+)
                                       and b.itemcatid_chr = g.typeid_chr
                                       and b.itemcatid_chr = f.typeid_chr(+)
                                       and g.flag_int = 1
                                       and a.doctorid_chr = e.empid_chr
                                       and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                       {0}
                                       and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp '" + p_endDate + @"' 
                                       and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                       group by g.typeid_chr,
                                                g.typename_vchr,
                                                a.doctorid_chr, 
                                                e.empno_chr, 
                                                e.lastname_vchr,
                                                e.code_vchr,
                                                e.deptname_vchr) mm,
       
       (select ss.itcatid,
       ss.doctorid_chr,
       ss.empno_chr,
       ss.lastname_vchr,
       ss.code_vchr,
       ss.deptname_vchr,
       sum(ss.diffSum) diffSum
  from (select '1003' itcatid,
               m.doctorid_chr,
               e.empno_chr,
               e.lastname_vchr,
               e.code_vchr,
               e.deptname_vchr,
               sum(decode(m.STATUS_INT,
                          0,
                          (-1) * nvl(n.toldiffprice_mny, 0),
                          2,
                          (-1) * nvl(n.toldiffprice_mny, 0),
                          round(nvl(n.toldiffprice_mny, 0),2))) diffSum
          from t_opr_outpatientrecipeinv m,
               t_opr_outpatientpwmrecipede n,
               (select e.empid_chr,
                       e.empno_chr,
                       e.lastname_vchr,
                       r.deptid_chr,
                       d.code_vchr,
                       d.deptname_vchr
                  from t_bse_employee e, t_bse_deptemp r, t_bse_deptdesc d
                 where r.deptid_chr = d.deptid_chr
                   and e.empid_chr = r.empid_chr
                   and r.default_dept_int = 1
                union all
                select e2.empid_chr,
                       e2.empno_chr,
                       e2.lastname_vchr,
                       '' deptid_chr,
                       '' code_vchr,
                       '' deptname_vchr
                  from t_bse_employee e2
                 where not exists (select ''
                          from t_bse_deptemp r2
                         where r2.empid_chr = e2.empid_chr
                           and r2.default_dept_int = 1)) e
         where m.outpatrecipeid_chr = n.outpatrecipeid_chr
           and m.doctorid_chr = e.empid_chr
           and( m.isvouchers_int <> 2 or m.isvouchers_int is null)
           {1}
           and m.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp '" + p_endDate + @"' 
           and m.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
         group by m.outpatrecipeid_chr,
                  e.empno_chr,
                  e.lastname_vchr,
                  e.code_vchr,
                  e.deptname_vchr,
                  m.doctorid_chr
        union all
        select '1006' itcatid,
               m.doctorid_chr,
               e.empno_chr,
               e.lastname_vchr,
               e.code_vchr,
               e.deptname_vchr,
               sum(decode(m.status_int,
                          0,
                          (-1) * nvl(n.toldiffprice_mny, 0),
                          2,
                          (-1) * nvl(n.toldiffprice_mny, 0),
                         round(nvl(n.toldiffprice_mny, 0),2))) diffSum
          from t_opr_outpatientrecipeinv m,
               t_opr_outpatientcmrecipede n,
               (select e.empid_chr,
                       e.empno_chr,
                       e.lastname_vchr,
                       r.deptid_chr,
                       d.code_vchr,
                       d.deptname_vchr
                  from t_bse_employee e, t_bse_deptemp r, t_bse_deptdesc d
                 where r.deptid_chr = d.deptid_chr
                   and e.empid_chr = r.empid_chr
                   and r.default_dept_int = 1
                union all
                select e2.empid_chr,
                       e2.empno_chr,
                       e2.lastname_vchr,
                       '' deptid_chr,
                       '' code_vchr,
                       '' deptname_vchr
                  from t_bse_employee e2
                 where not exists (select ''
                          from t_bse_deptemp r2
                         where r2.empid_chr = e2.empid_chr
                           and r2.default_dept_int = 1)) e
         where m.outpatrecipeid_chr = n.outpatrecipeid_chr
           and m.doctorid_chr = e.empid_chr
            {2}
           and( m.isvouchers_int <> 2 or m.isvouchers_int is null)
           and m.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp '" + p_endDate + @"' 
           and m.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
         group by m.outpatrecipeid_chr,
                  m.doctorid_chr,
                  e.empno_chr,
                  e.lastname_vchr,
                  e.code_vchr,
                  e.deptname_vchr
        union all
        select '1026' itcatid,
               m.doctorid_chr,
               e.empno_chr,
               e.lastname_vchr,
               e.code_vchr,
               e.deptname_vchr,
               sum(decode(m.STATUS_INT,
                          0,
                          (-1) * nvl(n.toldiffprice_mny, 0),
                          2,
                          (-1) * nvl(n.toldiffprice_mny, 0),
                        round(nvl(n.toldiffprice_mny, 0),2))) diffSum
          from t_opr_outpatientrecipeinv m,
               t_opr_outpatientothrecipede n,
               (select e.empid_chr,
                       e.empno_chr,
                       e.lastname_vchr,
                       r.deptid_chr,
                       d.code_vchr,
                       d.deptname_vchr
                  from t_bse_employee e, t_bse_deptemp r, t_bse_deptdesc d
                 where r.deptid_chr = d.deptid_chr
                   and e.empid_chr = r.empid_chr
                   and r.default_dept_int = 1
                union all
                select e2.empid_chr,
                       e2.empno_chr,
                       e2.lastname_vchr,
                       '' deptid_chr,
                       '' code_vchr,
                       '' deptname_vchr
                  from t_bse_employee e2
                 where not exists (select ''
                          from t_bse_deptemp r2
                         where r2.empid_chr = e2.empid_chr
                           and r2.default_dept_int = 1)) e
         where m.outpatrecipeid_chr = n.outpatrecipeid_chr
           and m.doctorid_chr = e.empid_chr
           and( m.isvouchers_int <> 2 or m.isvouchers_int is null)
           {3}
           and m.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp '" + p_endDate + @"' 
           and m.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
         group by m.outpatrecipeid_chr,
                  e.empno_chr,
                  e.lastname_vchr,
                  e.code_vchr,
                  e.deptname_vchr,
                  m.doctorid_chr) ss
                  group by ss.itcatid,
       ss.doctorid_chr,
       ss.empno_chr,
       ss.lastname_vchr,
       ss.code_vchr,
       ss.deptname_vchr) tt
 where mm.typeid_chr = tt.itcatid(+)
      and mm.doctorid_chr = tt.doctorid_chr(+)
      and mm.code_vchr = tt.code_vchr(+)";

                if (p_strDoctorID != string.Empty)
                {
                    strSQLSub = @"and a.doctorid_chr in (" + p_strDoctorID + ")";
                    strsqlsub = @"and m.doctorid_chr in (" + p_strDoctorID + ")";
                }
                objs = new object[4] { strSQLSub, strsqlsub, strsqlsub, strsqlsub };
                strSQLT1 = string.Format(strSQLT1, objs);
                if (p_strStatType == "1")
                {
                    strSQLT1 = strSQLT1.Replace("recorddate_dat", "balance_dat");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT1, ref dtT1);
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T1表异常：" + e.Message);
                }
                #endregion

                #region T2
                string strSQLT2 = @"select   a.doctorid_chr,
                                             sum (case a.status_int
                                                       when 1
                                                          then 1
                                                       when 3
                                                          then 1
                                                       when 2
                                                          then -1
                                                    end
                                                   ) as zfs
                                        from t_opr_outpatientrecipeinv a,
                                             t_opr_reciperelation b,
                                             t_opr_outpatientrecipe c
                                       where a.outpatrecipeid_chr = b.seqid
                                         and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                         and a.balanceflag_int = 1
                                         and c.recipeflag_int = 1
                                         and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                         and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp '" + p_endDate + @"'
                                         and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                         group by a.doctorid_chr";

                if (p_strStatType == "1")
                {
                    strSQLT2 = strSQLT2.Replace("recorddate_dat", "balance_dat");
                }
                else
                {
                    strSQLT2 = strSQLT2.Replace("and a.balanceflag_int = 1", "");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT2, ref dtT2);
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T2表异常：" + e.Message);
                }
                #endregion

                #region T21
                string strSQLT21 = string.Empty;

                strSQLT21 = @"select a.doctorid_chr,
                                       (case a.status_int
                                         when 1 then
                                          1
                                         when 3 then
                                          1
                                         when 2 then
                                          -1
                                       end) as kjrs,
                                       a.patientid_chr as pid,
                                       to_char(a.recorddate_dat, 'yyyy-mm-dd') as recDate
                                  from t_opr_outpatientrecipeinv a,
                                       t_opr_reciperelation      b,
                                       t_opr_outpatientrecipe    c
                                 where a.outpatrecipeid_chr = b.seqid
                                   and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                   and a.balanceflag_int = 1
                                --   and c.recipeflag_int = 1
                                   and (a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                   and exists
                                 (select 1
                                          from t_opr_oprecipeitemde t1,
                                               t_bse_chargeitem     t2,
                                               t_bse_medicine       t3
                                         where c.outpatrecipeid_chr = t1.outpatrecipeid_chr
                                           and t1.itemid_chr = t2.itemid_chr
                                           and t2.itemsrcid_vchr = t3.medicineid_chr
                                           and t3.medicinetypeid_chr = 2
                                           and t3.pharmaid_chr in ('00001', '00005', '00006', '00013'))
                                   and (a.recorddate_dat between timestamp'{0}' and timestamp'{1}')
                                   and (a.recorddate_dat between to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date('{3}', 'yyyy-mm-dd hh24:mi:ss'))
                                 order by a.doctorid_chr, a.patientid_chr, a.recorddate_dat";
                // group by a.doctorid_chr
                objs = new object[4] { p_beginDate, p_endDate, p_beginDate, p_endDate };
                strSQLT21 = string.Format(strSQLT21, objs);

                if (p_strStatType == "1")
                {
                    strSQLT21 = strSQLT21.Replace("recorddate_dat", "balance_dat");
                }
                else
                {
                    strSQLT21 = strSQLT21.Replace("and a.balanceflag_int = 1", "");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT21, ref dtT21);
                    if (dtT21 != null && dtT21.Rows.Count > 0)
                    {
                        List<string> lstDoctId = new List<string>();
                        Dictionary<string, int> dicDoct = new Dictionary<string, int>();
                        for (int i = dtT21.Rows.Count - 1; i >= 0; i--)
                        {
                            if (i > 0)
                            {
                                if (dtT21.Rows[i]["doctorid_chr"] == dtT21.Rows[i - 1]["doctorid_chr"] && dtT21.Rows[i]["pid"] == dtT21.Rows[i - 1]["pid"] && dtT21.Rows[i]["recDate"] == dtT21.Rows[i - 1]["recDate"] && dtT21.Rows[i]["kjrs"] == dtT21.Rows[i - 1]["kjrs"])
                                {
                                    dtT21.Rows.RemoveAt(i);
                                }
                            }
                            if (dicDoct.ContainsKey(dtT21.Rows[i]["doctorid_chr"].ToString()) == false)
                            {
                                dicDoct.Add(dtT21.Rows[i]["doctorid_chr"].ToString(), 0);
                                lstDoctId.Add(dtT21.Rows[i]["doctorid_chr"].ToString());
                            }
                        }
                        foreach (string doctId in lstDoctId)
                        {
                            foreach (DataRow dr in dtT21.Rows)
                            {
                                if (dr["doctorid_chr"].ToString() == doctId)
                                {
                                    dicDoct[doctId] += Convert.ToInt32(dr["kjrs"].ToString());
                                }
                            }
                        }
                        dtT21.Rows.Clear();
                        foreach (string doctId in lstDoctId)
                        {
                            DataRow drNew = dtT21.NewRow();
                            drNew["doctorid_chr"] = doctId;
                            drNew["kjrs"] = dicDoct[doctId];
                            dtT21.Rows.Add(drNew);
                        }
                        dtT21.AcceptChanges();
                    }
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T21表异常：" + e.Message);
                }
                #endregion

                #region T3
                string strSQLT3 = @"select   a.doctorid_chr,
                                             sum (case a.status_int
                                                       when 1
                                                          then 1
                                                       when 3
                                                          then 1
                                                       when 2
                                                          then -1
                                                   end
                                                  ) as cfs
                                        from t_opr_outpatientrecipeinv a,
                                             t_opr_reciperelation b,
                                             t_opr_outpatientrecipe c
                                       where a.outpatrecipeid_chr = b.seqid
                                         and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                         and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                         and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp '" + p_endDate + @"'
                                         and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                         group by a.doctorid_chr";

                if (p_strStatType == "1")
                {
                    strSQLT3 = strSQLT3.Replace("recorddate_dat", "balance_dat");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT3, ref dtT3);
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T3表异常：" + e.Message);
                }
                #endregion

                #region T4
                string strSQLT4 = @"select t5.diagdr_chr,
                                           t5.tolprice_mny,
                                           t6.xytolprice_mny,
                                           round((t5.tolprice_mny / t6.xytolprice_mny) * 100, 2) as kjybl,
                                           round((t5.kjcfs / t6.zcfs)*100, 2) as kjcfbl
                                      from (select g.lastname_vchr,
                                                   c.diagdr_chr,
                                                   sum(bb.tolprice_mny + decode(d.opchargeflg_int,
                      0,
                      cc.tradeprice_mny - cc.itemprice_mny,
                      1,
                      round((cc.tradeprice_mny - cc.itemprice_mny) /
                            d.packqty_dec,
                            4))* bb.qty_dec) tolprice_mny,
                                                   sum (case a.status_int
                                                             when 1
                                                                then 1
                                                             when 3
                                                                then 1
                                                             when 2
                                                                then -1
                                                         end
                                                        ) as kjcfs
                                              from t_opr_outpatientrecipeinv a,
                                                   t_opr_reciperelation b,
                                                   t_opr_outpatientrecipe c,
                                                   t_opr_oprecipeitemde bb,
                                                   t_bse_chargeitem cc,
                                                   t_bse_medicine d,
                                                   t_bse_deptdesc f,
                                                   t_bse_employee g,
                                                   (select t1.empid_chr, t1.deptid_chr, t2.deptname_vchr
                                                      from t_bse_deptemp t1, t_bse_deptdesc t2
                                                     where t1.default_dept_int = 1
                                                       and t1.deptid_chr = t2.deptid_chr) e
                                             where a.outpatrecipeid_chr = b.seqid
                                               and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                               and c.outpatrecipeid_chr = bb.outpatrecipeid_chr
                                               and bb.itemid_chr = cc.itemid_chr
                                               and c.diagdr_chr = e.empid_chr(+)
                                               and c.diagdept_chr = f.deptid_chr
                                               and c.diagdr_chr = g.empid_chr
                                               and c.pstauts_int in (2, 3)
                                               and cc.itemsrcid_vchr = d.medicineid_chr
                                               and d.medicinetypeid_chr = 2
                                               and d.pharmaid_chr in ('00001', '00005', '00006', '00013')
                                               and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                               and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp '" + p_endDate + @"'
                                               and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                             group by g.lastname_vchr, c.diagdr_chr) t5,
                                           (select g.lastname_vchr,
                                                   c.diagdr_chr,
                                                    sum(bb.tolprice_mny +
                                                       decode(d.opchargeflg_int,
                                                              0,
                                                              cc.tradeprice_mny - cc.itemprice_mny,
                                                              1,
                                                              round((cc.tradeprice_mny - cc.itemprice_mny) /
                                                                    d.packqty_dec,
                                                                    4))* bb.qty_dec) xytolprice_mny,
                                                   sum (case a.status_int
                                                             when 1
                                                                then 1
                                                             when 3
                                                                then 1
                                                             when 2
                                                                then -1
                                                         end
                                                        ) as zcfs
                                              from t_opr_outpatientrecipeinv a,
                                                   t_opr_reciperelation b,
                                                   t_opr_outpatientrecipe c,
                                                   t_opr_oprecipeitemde bb,
                                                   t_bse_chargeitem cc,
                                                   t_bse_medicine d,
                                                   t_bse_deptdesc f,
                                                   t_bse_employee g,
                                                   (select t1.empid_chr, t1.deptid_chr, t2.deptname_vchr
                                                      from t_bse_deptemp t1, t_bse_deptdesc t2
                                                     where t1.default_dept_int = 1
                                                       and t1.deptid_chr = t2.deptid_chr) e
                                             where a.outpatrecipeid_chr = b.seqid
                                               and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                               and c.outpatrecipeid_chr = bb.outpatrecipeid_chr
                                               and bb.itemid_chr = cc.itemid_chr
                                               and c.diagdr_chr = e.empid_chr(+)
                                               and c.diagdept_chr = f.deptid_chr
                                               and c.diagdr_chr = g.empid_chr
                                               and c.pstauts_int in (2, 3)
                                               and cc.itemsrcid_vchr = d.medicineid_chr
                                               and d.medicinetypeid_chr = 2
                                               and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                               and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp '" + p_endDate + @"'
                                               and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                               group by g.lastname_vchr, c.diagdr_chr) t6
                                     where t5.diagdr_chr = t6.diagdr_chr
                                     group by t5.diagdr_chr, t5.tolprice_mny, t6.xytolprice_mny, t6.zcfs, t5.kjcfs";

                if (p_strStatType == "1")
                {
                    strSQLT4 = strSQLT4.Replace("recorddate_dat", "balance_dat");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT4, ref dtT4);
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T4表异常：" + e.Message);
                }
                #endregion

                #region T9
                string strSQLT9 = @"select t7.diagdr_chr,
                                           t7.tolprice_mny as tolprice_jby,
                                           t8.xytolprice_mny as xytolprice_jby,
                                           round((t7.tolprice_mny / t8.xytolprice_mny) * 100, 2) as jbybl
                                      from (select g.lastname_vchr,
                                                   c.diagdr_chr,
                                                   sum(bb.tolprice_mny + decode(d.opchargeflg_int,
                      0,
                      cc.tradeprice_mny - cc.itemprice_mny,
                      1,
                      round((cc.tradeprice_mny - cc.itemprice_mny) /
                            d.packqty_dec,
                            4))* bb.qty_dec) tolprice_mny
                                              from t_opr_outpatientrecipeinv a,
                                                   t_opr_reciperelation b,
                                                   t_opr_outpatientrecipe c,
                                                   t_opr_oprecipeitemde bb,
                                                   t_bse_chargeitem cc,
                                                   t_bse_medicine d,
                                                   t_bse_deptdesc f,
                                                   t_bse_employee g,
                                                   (select t1.empid_chr, t1.deptid_chr, t2.deptname_vchr
                                                      from t_bse_deptemp t1, t_bse_deptdesc t2
                                                     where t1.default_dept_int = 1
                                                       and t1.deptid_chr = t2.deptid_chr) e
                                             where a.outpatrecipeid_chr = b.seqid
                                               and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                               and c.outpatrecipeid_chr = bb.outpatrecipeid_chr
                                               and bb.itemid_chr = cc.itemid_chr
                                               and c.diagdr_chr = e.empid_chr(+)
                                               and c.diagdept_chr = f.deptid_chr
                                               and c.diagdr_chr = g.empid_chr
                                               and c.pstauts_int in (2, 3)
                                               and cc.itemsrcid_vchr = d.medicineid_chr
                                               and d.medicinetypeid_chr = 2
                                               and d.insurancetype_vchr in ('1006', '1007', '1008')
                                               and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                               and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp '" + p_endDate + @"'
                                               and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                             group by g.lastname_vchr, c.diagdr_chr) t7,
                                           (select g.lastname_vchr,
                                                   c.diagdr_chr,
                                                   sum(bb.tolprice_mny +
                   decode(d.opchargeflg_int,
                          0,
                          cc.tradeprice_mny - cc.itemprice_mny,
                          1,
                          round((cc.tradeprice_mny - cc.itemprice_mny) /
                                d.packqty_dec,
                                4))* bb.qty_dec) xytolprice_mny
                                              from t_opr_outpatientrecipeinv a,
                                                   t_opr_reciperelation b,
                                                   t_opr_outpatientrecipe c,
                                                   t_opr_oprecipeitemde bb,
                                                   t_bse_chargeitem cc,
                                                   t_bse_medicine d,
                                                   t_bse_deptdesc f,
                                                   t_bse_employee g,
                                                   (select t1.empid_chr, t1.deptid_chr, t2.deptname_vchr
                                                      from t_bse_deptemp t1, t_bse_deptdesc t2
                                                     where t1.default_dept_int = 1
                                                       and t1.deptid_chr = t2.deptid_chr) e
                                             where a.outpatrecipeid_chr = b.seqid
                                               and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                               and c.outpatrecipeid_chr = bb.outpatrecipeid_chr
                                               and bb.itemid_chr = cc.itemid_chr
                                               and c.diagdr_chr = e.empid_chr(+)
                                               and c.diagdept_chr = f.deptid_chr
                                               and c.diagdr_chr = g.empid_chr
                                               and c.pstauts_int in (2, 3)
                                               and cc.itemsrcid_vchr = d.medicineid_chr
                                               and d.medicinetypeid_chr = 2
                                               and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                               and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp '" + p_endDate + @"'
                                               and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                             group by g.lastname_vchr, c.diagdr_chr) t8
                                     where t7.diagdr_chr = t8.diagdr_chr
                                     group by t7.diagdr_chr, t7.tolprice_mny, t8.xytolprice_mny";

                if (p_strStatType == "1")
                {
                    strSQLT9 = strSQLT9.Replace("recorddate_dat", "balance_dat");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT9, ref dtT9);
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T9表异常：" + e.Message);
                }
                #endregion

                #region T10
                string strSQLT10 = @"select m.doctorid_chr, count(m.outpatrecipeid_chr) Counts_KJY
  from (select n.doctorid_chr, n.outpatrecipeid_chr,sum(n.status) as statussum
  from (select b.doctorid_chr, t.outpatrecipeid_chr,decode(b.status_int,0,-1,2,-1,1) as status
          from t_opr_outpatientpwmrecipede t,
               t_opr_reciperelation        a,
               t_opr_outpatientrecipeinv   b,
               t_bse_medicine              c,
               t_bse_chargeitem            e
         where t.outpatrecipeid_chr = a.outpatrecipeid_chr
           and a.seqid = b.outpatrecipeid_chr
           and t.itemid_chr = e.itemid_chr
           and e.itemsrcid_vchr = c.medicineid_chr
           and c.pharmaid_chr in ('00001', '00005', '00006', '00013') ---抗菌药分类  
           and( b.isvouchers_int <> 2 or b.isvouchers_int is null)
           and b.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp '" + p_endDate + @"' 
           and b.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')) n
               group by n.doctorid_chr, n.outpatrecipeid_chr) m
                       where m.statussum<>0
 group by m.doctorid_chr";

                if (p_strStatType == "1")
                {
                    strSQLT10 = strSQLT10.Replace("recorddate_dat", "balance_dat");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT10, ref dtT10);
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T10表异常：" + e.Message);
                }
                #endregion

                #region T11
                string strSQLT11 = @"select tab1.doctorid_chr,
                                            sum(case tab1.status_int
                                                     when 1 then
                                                      1
                                                     when 3 then
                                                      1
                                                     when 2 then
                                                      -1
                                                 end
                                                )as xycfs
                                      from (select distinct a.outpatrecipeid_chr,
                                                            a.status_int,
                                                            a.doctorid_chr
                                              from t_opr_outpatientrecipeinv a,
                                                   t_opr_reciperelation      b,
                                                   t_opr_outpatientrecipe    c,
                                                   t_opr_outpatientpwmrecipede t,
                                                   t_bse_chargeitem            e,
                                                   t_bse_medicine              m
                                             where a.outpatrecipeid_chr = b.seqid
                                               and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                               and t.outpatrecipeid_chr = b.outpatrecipeid_chr
                                               and t.itemid_chr = e.itemid_chr
                                               and e.itemsrcid_vchr = m.medicineid_chr
                                               and m.medicinetypeid_chr = '2'
                                               and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                               and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp '" + p_endDate + @"'
                                               and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                             ) tab1
                                      group by tab1.doctorid_chr";

                if (p_strStatType == "1")
                {
                    strSQLT11 = strSQLT11.Replace("recorddate_dat", "balance_dat");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT11, ref dtT11);
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T11表异常：" + e.Message);
                }
                #endregion

                #region 数据合并处理整合为和原查询语句得到的datatable相同

                string[] strDtT2JoinColArr = new string[] { "zfs" };
                dtT1 = m_dtbDataTableLeftJoin(dtT1, dtT2, "doctorid_chr", "doctorid_chr", strDtT2JoinColArr);

                string[] strDtT21JoinColArr = new string[] { "kjrs" };
                dtT1 = m_dtbDataTableLeftJoin(dtT1, dtT21, "doctorid_chr", "doctorid_chr", strDtT21JoinColArr);

                string[] strDtT3JoinColArr = new string[] { "cfs" };
                dtT1 = m_dtbDataTableLeftJoin(dtT1, dtT3, "doctorid_chr", "doctorid_chr", strDtT3JoinColArr);

                string[] strDtT4JoinColArr = new string[] { "kjybl", "tolprice_mny", "xytolprice_mny" };
                dtT1 = m_dtbDataTableLeftJoin(dtT1, dtT4, "doctorid_chr", "diagdr_chr", strDtT4JoinColArr);

                string[] strDtT9JoinColArr = new string[] { "jbybl", "tolprice_jby", "xytolprice_jby" };
                dtT1 = m_dtbDataTableLeftJoin(dtT1, dtT9, "doctorid_chr", "diagdr_chr", strDtT9JoinColArr);

                string[] strDtT10JoinColArr = new string[] { "Counts_KJY" };
                dtT1 = m_dtbDataTableLeftJoin(dtT1, dtT10, "doctorid_chr", "doctorid_chr", strDtT10JoinColArr);

                string[] strDtT11JoinColArr = new string[] { "xycfs" };
                dtT1 = m_dtbDataTableLeftJoin(dtT1, dtT11, "doctorid_chr", "doctorid_chr", strDtT11JoinColArr);

                DataView dvJoinResult = dtT1.DefaultView;
                dvJoinResult.Sort = "empno_chr, lastname_vchr, typeid_chr, typename_vchr, zfs, kjrs, cfs, code_vchr, deptname_vchr, tolprice_mny, xytolprice_mny, kjybl, counts_kjy, xycfs, jbybl";
                DataTable dtFinalResult = dvJoinResult.ToTable();

                for (int i = 0; i < dtFinalResult.Rows.Count - 1; i++)
                {
                    DataRow drFirst = dtFinalResult.Rows[i];
                    DataRow drSecond = dtFinalResult.Rows[i + 1];

                    if (drFirst["empno_chr"] == drSecond["empno_chr"] && drFirst["lastname_vchr"] == drSecond["lastname_vchr"] && drFirst["typeid_chr"] == drSecond["typeid_chr"])
                    {
                        if (drFirst["typename_vchr"] == drSecond["typename_vchr"] && drFirst["zfs"] == drSecond["zfs"] && drFirst["kjrs"] == drSecond["kjrs"] && drFirst["cfs"] == drSecond["cfs"] && drFirst["xytolprice_mnyc"] == drSecond["xytolprice_mnyc"])
                        {
                            if (drFirst["code_vchr"] == drSecond["code_vchr"] && drFirst["deptname_vchr"] == drSecond["deptname_vchr"] && drFirst["tolprice_mny"] == drSecond["tolprice_mny"] && drFirst["tolprice_jby"] == drSecond["tolprice_jby"] && drFirst["xytolprice_jby"] == drSecond["xytolprice_jby"])
                            {
                                if (drFirst["kjybl"] == drSecond["kjybl"] && drFirst["counts_kjy"] == drSecond["counts_kjy"] && drFirst["xycfs"] == drSecond["xycfs"] && drFirst["jbybl"] == drSecond["jbybl"])
                                {
                                    if (dtFinalResult.Rows[i]["xytolprice_mny"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i]["xytolprice_mny"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i]["xytolprice_jby"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i]["xytolprice_jby"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i]["tolfee_mny"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i]["tolfee_mny"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i]["tolfee_jby"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i]["tolfee_jby"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i]["jxywl"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i]["jxywl"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i + 1]["xytolprice_mny"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i + 1]["xytolprice_mny"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i + 1]["xytolprice_jby"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i + 1]["xytolprice_jby"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i + 1]["tolfee_mny"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i + 1]["tolfee_mny"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i + 1]["tolfee_jby"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i + 1]["tolfee_jby"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i + 1]["jxywl"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i + 1]["jxywl"] = 0;
                                    }
                                    dtFinalResult.Rows[i]["tolfee_mny"] = Convert.ToDecimal(dtFinalResult.Rows[i]["tolfee_mny"]) + Convert.ToDecimal(dtFinalResult.Rows[i + 1]["tolfee_mny"]);
                                    dtFinalResult.Rows[i]["tolfee_jby"] = Convert.ToDecimal(dtFinalResult.Rows[i]["tolfee_jby"]) + Convert.ToDecimal(dtFinalResult.Rows[i + 1]["tolfee_jby"]);
                                    dtFinalResult.Rows[i]["xytolprice_mny"] = Convert.ToDecimal(dtFinalResult.Rows[i]["xytolprice_mny"]) + Convert.ToDecimal(dtFinalResult.Rows[i + 1]["xytolprice_mny"]);
                                    dtFinalResult.Rows[i]["xytolprice_jby"] = Convert.ToDecimal(dtFinalResult.Rows[i]["xytolprice_jby"]) + Convert.ToDecimal(dtFinalResult.Rows[i + 1]["xytolprice_jby"]);
                                    dtFinalResult.Rows[i]["jxywl"] = Convert.ToDecimal(dtFinalResult.Rows[i]["jxywl"]) + Convert.ToDecimal(dtFinalResult.Rows[i + 1]["jxywl"]);
                                    dtFinalResult.Rows.Remove(drSecond);
                                    dtFinalResult.AcceptChanges();
                                    i = i - 1;
                                }
                            }
                        }
                    }
                }

                dtResult.Columns.Add("typeid_chr", typeof(string));
                dtResult.Columns.Add("typename_vchr", typeof(string));
                dtResult.Columns.Add("empno_chr", typeof(string));
                dtResult.Columns.Add("lastname_vchr", typeof(string));
                dtResult.Columns.Add("zfs", typeof(int));
                dtResult.Columns.Add("kjrs", typeof(int));
                dtResult.Columns.Add("cfs", typeof(int));
                dtResult.Columns.Add("code_vchr", typeof(string));
                dtResult.Columns.Add("deptname_vchr", typeof(string));
                dtResult.Columns.Add("tolfee_mny", typeof(decimal));
                dtResult.Columns.Add("jxywl", typeof(decimal));
                dtResult.Columns.Add("kjybl", typeof(string));
                dtResult.Columns.Add("tolprice_mny", typeof(decimal));
                dtResult.Columns.Add("xytolprice_mny", typeof(decimal));
                //dtResult.Columns.Add("kjcfbl", typeof(decimal));
                dtResult.Columns.Add("kjysybl", typeof(string));
                dtResult.Columns.Add("jbybl", typeof(string));
                dtResult.Columns.Add("tolprice_jby", typeof(decimal));
                dtResult.Columns.Add("xytolprice_jby", typeof(decimal));

                for (int i = 0; i < dtFinalResult.Rows.Count; i++)
                {
                    DataRow drResu = dtResult.NewRow();
                    drResu["typeid_chr"] = dtFinalResult.Rows[i]["typeid_chr"];
                    drResu["typename_vchr"] = dtFinalResult.Rows[i]["typename_vchr"];
                    drResu["empno_chr"] = dtFinalResult.Rows[i]["empno_chr"];
                    drResu["lastname_vchr"] = dtFinalResult.Rows[i]["lastname_vchr"];
                    drResu["zfs"] = dtFinalResult.Rows[i]["zfs"];
                    drResu["kjrs"] = dtFinalResult.Rows[i]["kjrs"];
                    drResu["cfs"] = dtFinalResult.Rows[i]["cfs"];
                    drResu["code_vchr"] = dtFinalResult.Rows[i]["code_vchr"];
                    drResu["deptname_vchr"] = dtFinalResult.Rows[i]["deptname_vchr"];
                    drResu["tolfee_mny"] = dtFinalResult.Rows[i]["tolfee_mny"];
                    drResu["jxywl"] = dtFinalResult.Rows[i]["jxywl"];
                    drResu["kjybl"] = dtFinalResult.Rows[i]["kjybl"] == DBNull.Value || Convert.ToDecimal(dtFinalResult.Rows[i]["kjybl"]) == 0 ? "" : dtFinalResult.Rows[i]["kjybl"].ToString() + "%";
                    drResu["tolprice_mny"] = dtFinalResult.Rows[i]["tolprice_mny"];
                    drResu["xytolprice_mny"] = dtFinalResult.Rows[i]["xytolprice_mny"];
                    drResu["jbybl"] = dtFinalResult.Rows[i]["jbybl"] == DBNull.Value || Convert.ToDecimal(dtFinalResult.Rows[i]["jbybl"]) == 0 ? "" : dtFinalResult.Rows[i]["jbybl"].ToString() + "%";
                    drResu["tolprice_jby"] = dtFinalResult.Rows[i]["tolprice_jby"];
                    drResu["xytolprice_jby"] = dtFinalResult.Rows[i]["xytolprice_jby"];
                    //if (dtFinalResult.Rows[i]["Counts_KJY"] != DBNull.Value && dtFinalResult.Rows[i]["xycfs"] != DBNull.Value)
                    //{
                    //    drResu["kjcfbl"] = Math.Round((Convert.ToDecimal(dtFinalResult.Rows[i]["Counts_KJY"]) / Convert.ToDecimal(dtFinalResult.Rows[i]["xycfs"])) * 100, 2);
                    //}
                    if (dtFinalResult.Rows[i]["kjrs"] != DBNull.Value && dtFinalResult.Rows[i]["zfs"] != DBNull.Value && Convert.ToInt32(dtFinalResult.Rows[i]["zfs"]) != 0)
                    {
                        drResu["kjysybl"] = this.Round((Convert.ToDecimal(dtFinalResult.Rows[i]["kjrs"]) / Convert.ToDecimal(dtFinalResult.Rows[i]["zfs"])) * 100, 2).ToString() + "%";
                    }
                    dtResult.Rows.Add(drResu);
                }

                dtResult.AcceptChanges();
                #endregion
            }

            if (intFlag == 1)//按科室
            {
                #region T1
                string strSQLT1 = @"select mm.typeid_chr,
       mm.typename_vchr,
       mm.doctorid_chr,
       mm.empno_chr,
       mm.lastname_vchr,
       mm.code_vchr,
       mm.deptname_vchr,
       (mm.tolfee_mny +  nvl(tt.diffSum,0)) tolfee_mny,
       mm.jxywl,
       tt.diffSum
  from (select g.typeid_chr,
                                           g.typename_vchr,
                                           a.doctorid_chr,
                                           e.empno_chr,
                                           e.lastname_vchr,
                                           e.code_vchr,
                                           e.deptname_vchr,
                                           sum(b.tolfee_mny) tolfee_mny,
                                           sum(b.tolfee_mny * f.percentage) jxywl
                                      from t_opr_outpatientrecipeinv a,
                                           t_opr_outpatientrecipesumde b,
                                           (select e.empid_chr,
                                                   e.empno_chr,
                                                   e.lastname_vchr,
                                                   r.deptid_chr,
                                                   d.code_vchr,
                                                   d.deptname_vchr
                                              from t_bse_employee e, t_bse_deptemp r, t_bse_deptdesc d
                                             where r.deptid_chr = d.deptid_chr
                                               and e.empid_chr = r.empid_chr
                                               and r.default_dept_int = 1
                                            union all
                                            select e2.empid_chr,
                                                   e2.empno_chr,
                                                   e2.lastname_vchr,
                                                   '' deptid_chr,
                                                   '' code_vchr,
                                                   '' deptname_vchr
                                              from t_bse_employee e2
                                             where not exists (select ''
                                                      from t_bse_deptemp r2
                                                     where r2.empid_chr = e2.empid_chr
                                                       and r2.default_dept_int = 1)) e,
                                           t_opr_drachformula f,
                                           t_bse_chargeitemextype g
                                     where a.seqid_chr = b.seqid_chr(+)
                                       and b.itemcatid_chr = g.typeid_chr
                                       and b.itemcatid_chr = f.typeid_chr(+)
                                       and g.flag_int = 1
                                       and a.doctorid_chr = e.empid_chr
                                        {0} 
                                       and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                       and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp'" + p_endDate + @"'
                                       and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                     group by g.typeid_chr,
                                              g.typename_vchr,
                                              a.doctorid_chr,
                                              e.empno_chr,
                                              e.lastname_vchr,
                                              e.code_vchr,
                                              e.deptname_vchr) mm,
       
       (select ss.itcatid,
       ss.doctorid_chr,
       ss.empno_chr,
       ss.lastname_vchr,
       ss.code_vchr,
       ss.deptname_vchr,
       sum(ss.diffSum) diffSum
  from (select '1003' itcatid,
               m.doctorid_chr,
               e.empno_chr,
               e.lastname_vchr,
               e.code_vchr,
               e.deptname_vchr,
               sum(decode(m.STATUS_INT,
                          0,
                          (-1) * nvl(n.toldiffprice_mny, 0),
                          2,
                          (-1) * nvl(n.toldiffprice_mny, 0),
                        round(nvl(n.toldiffprice_mny, 0),2))) diffSum
          from t_opr_outpatientrecipeinv m,
               t_opr_outpatientpwmrecipede n,
               (select e.empid_chr,
                       e.empno_chr,
                       e.lastname_vchr,
                       r.deptid_chr,
                       d.code_vchr,
                       d.deptname_vchr
                  from t_bse_employee e, t_bse_deptemp r, t_bse_deptdesc d
                 where r.deptid_chr = d.deptid_chr
                   and e.empid_chr = r.empid_chr
                   and r.default_dept_int = 1
                union all
                select e2.empid_chr,
                       e2.empno_chr,
                       e2.lastname_vchr,
                       '' deptid_chr,
                       '' code_vchr,
                       '' deptname_vchr
                  from t_bse_employee e2
                 where not exists (select ''
                          from t_bse_deptemp r2
                         where r2.empid_chr = e2.empid_chr
                           and r2.default_dept_int = 1)) e
         where m.outpatrecipeid_chr = n.outpatrecipeid_chr
           and m.doctorid_chr = e.empid_chr
            {1}
           and( m.isvouchers_int <> 2 or m.isvouchers_int is null)
           and m.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp'" + p_endDate + @"'
           and m.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
         group by m.outpatrecipeid_chr,
                  e.empno_chr,
                  e.lastname_vchr,
                  e.code_vchr,
                  e.deptname_vchr,
                  m.doctorid_chr
        union all
        select '1006' itcatid,
               m.doctorid_chr,
               e.empno_chr,
               e.lastname_vchr,
               e.code_vchr,
               e.deptname_vchr,
               sum(decode(m.status_int,
                          0,
                          (-1) * nvl(n.toldiffprice_mny, 0),
                          2,
                          (-1) * nvl(n.toldiffprice_mny, 0),
                        round(nvl(n.toldiffprice_mny, 0),2))) diffSum
          from t_opr_outpatientrecipeinv m,
               t_opr_outpatientcmrecipede n,
               (select e.empid_chr,
                       e.empno_chr,
                       e.lastname_vchr,
                       r.deptid_chr,
                       d.code_vchr,
                       d.deptname_vchr
                  from t_bse_employee e, t_bse_deptemp r, t_bse_deptdesc d
                 where r.deptid_chr = d.deptid_chr
                   and e.empid_chr = r.empid_chr
                   and r.default_dept_int = 1
                union all
                select e2.empid_chr,
                       e2.empno_chr,
                       e2.lastname_vchr,
                       '' deptid_chr,
                       '' code_vchr,
                       '' deptname_vchr
                  from t_bse_employee e2
                 where not exists (select ''
                          from t_bse_deptemp r2
                         where r2.empid_chr = e2.empid_chr
                           and r2.default_dept_int = 1)) e
         where m.outpatrecipeid_chr = n.outpatrecipeid_chr
           and m.doctorid_chr = e.empid_chr
            {2}
           and( m.isvouchers_int <> 2 or m.isvouchers_int is null)
           and m.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp'" + p_endDate + @"'
           and m.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
         group by m.outpatrecipeid_chr,
                  m.doctorid_chr,
                  e.empno_chr,
                  e.lastname_vchr,
                  e.code_vchr,
                  e.deptname_vchr
        union all
        select '1026' itcatid,
               m.doctorid_chr,
               e.empno_chr,
               e.lastname_vchr,
               e.code_vchr,
               e.deptname_vchr,
               sum(decode(m.STATUS_INT,
                          0,
                          (-1) * nvl(n.toldiffprice_mny, 0),
                          2,
                          (-1) * nvl(n.toldiffprice_mny, 0),
                        round(nvl(n.toldiffprice_mny, 0),2))) diffSum
          from t_opr_outpatientrecipeinv m,
               t_opr_outpatientothrecipede n,
               (select e.empid_chr,
                       e.empno_chr,
                       e.lastname_vchr,
                       r.deptid_chr,
                       d.code_vchr,
                       d.deptname_vchr
                  from t_bse_employee e, t_bse_deptemp r, t_bse_deptdesc d
                 where r.deptid_chr = d.deptid_chr
                   and e.empid_chr = r.empid_chr
                   and r.default_dept_int = 1
                union all
                select e2.empid_chr,
                       e2.empno_chr,
                       e2.lastname_vchr,
                       '' deptid_chr,
                       '' code_vchr,
                       '' deptname_vchr
                  from t_bse_employee e2
                 where not exists (select ''
                          from t_bse_deptemp r2
                         where r2.empid_chr = e2.empid_chr
                           and r2.default_dept_int = 1)) e
         where m.outpatrecipeid_chr = n.outpatrecipeid_chr
           and m.doctorid_chr = e.empid_chr
            {3}
           and( m.isvouchers_int <> 2 or m.isvouchers_int is null)
           and m.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp'" + p_endDate + @"'
           and m.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
         group by m.outpatrecipeid_chr,
                  e.empno_chr,
                  e.lastname_vchr,
                  e.code_vchr,
                  e.deptname_vchr,
                  m.doctorid_chr) ss
                  group by ss.itcatid,
       ss.doctorid_chr,
       ss.empno_chr,
       ss.lastname_vchr,
       ss.code_vchr,
       ss.deptname_vchr) tt
 where mm.typeid_chr = tt.itcatid(+)
      and mm.doctorid_chr = tt.doctorid_chr(+)
      and mm.code_vchr = tt.code_vchr(+)";

                if (DeptIDArr != null && DeptIDArr.Count > 0)
                {
                    string str = "";
                    for (int i = 0; i < DeptIDArr.Count; i++)
                    {
                        str += "'" + DeptIDArr[i].ToString() + "',";
                    }
                    strSQLSub = @"and a.deptid_chr in (" + str.Substring(0, str.Length - 1) + ")";
                    strsqlsub = @"and m.deptid_chr in (" + str.Substring(0, str.Length - 1) + ")";
                }
                objs = new object[4] { strSQLSub, strsqlsub, strsqlsub, strsqlsub };
                strSQLT1 = string.Format(strSQLT1, objs);
                if (p_strStatType == "1")
                {
                    strSQLT1 = strSQLT1.Replace("recorddate_dat", "balance_dat");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT1, ref dtT1);
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T1表异常：" + e.Message);
                }
                #endregion

                #region T2
                string strSQLT2 = @"select a.doctorid_chr,
                                           sum(case a.status_int
                                                 when 1 then
                                                  1
                                                 when 3 then
                                                  1
                                                 when 2 then
                                                  -1
                                               end) as zfs
                                      from t_opr_outpatientrecipeinv a,
                                           t_opr_reciperelation      b,
                                           t_opr_outpatientrecipe    c
                                     where a.outpatrecipeid_chr = b.seqid
                                       and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                       and c.recipeflag_int = 1
                                       and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                       and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp'" + p_endDate + @"'
                                       and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                     group by a.doctorid_chr";

                if (p_strStatType == "1")
                {
                    strSQLT2 = strSQLT2.Replace("recorddate_dat", "balance_dat");
                }
                else
                {
                    strSQLT2 = strSQLT2.Replace("and a.balanceflag_int = 1", "");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT2, ref dtT2);
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T2表异常：" + e.Message);
                }
                #endregion

                #region T21
                string strSQLT21 = string.Empty;

                strSQLT21 = @"select a.doctorid_chr,
                                       (case a.status_int
                                         when 1 then
                                          1
                                         when 3 then
                                          1
                                         when 2 then
                                          -1
                                       end) as kjrs,
                                       a.patientid_chr as pid,
                                       to_char(a.recorddate_dat, 'yyyy-mm-dd') as recDate 
                                  from t_opr_outpatientrecipeinv a,
                                       t_opr_reciperelation      b,
                                       t_opr_outpatientrecipe    c
                                 where a.outpatrecipeid_chr = b.seqid
                                   and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                              --     and c.recipeflag_int = 1
                                   and (a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                   and exists
                                 (select 1
                                          from t_opr_oprecipeitemde t1,
                                               t_bse_chargeitem     t2,
                                               t_bse_medicine       t3
                                         where c.outpatrecipeid_chr = t1.outpatrecipeid_chr
                                           and t1.itemid_chr = t2.itemid_chr
                                           and t2.itemsrcid_vchr = t3.medicineid_chr
                                           and t3.medicinetypeid_chr = 2
                                           and t3.pharmaid_chr in ('00001', '00005', '00006', '00013'))
                                   and (a.recorddate_dat between timestamp'{0}' and timestamp'{1}')
                                   and (a.recorddate_dat between to_date('{2}', 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date('{3}', 'yyyy-mm-dd hh24:mi:ss'))
                                 order by a.doctorid_chr, a.patientid_chr, a.recorddate_dat";
                // group by a.doctorid_chr
                objs = new object[4] { p_beginDate, p_endDate, p_beginDate, p_endDate };
                strSQLT21 = string.Format(strSQLT21, objs);

                if (p_strStatType == "1")
                {
                    strSQLT21 = strSQLT21.Replace("recorddate_dat", "balance_dat");
                }
                else
                {
                    strSQLT21 = strSQLT21.Replace("and a.balanceflag_int = 1", "");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT21, ref dtT21);
                    if (dtT21 != null && dtT21.Rows.Count > 0)
                    {
                        List<string> lstDoctId = new List<string>();
                        Dictionary<string, int> dicDoct = new Dictionary<string, int>();
                        for (int i = dtT21.Rows.Count - 1; i >= 0; i--)
                        {
                            if (i > 0)
                            {
                                if (dtT21.Rows[i]["doctorid_chr"] == dtT21.Rows[i - 1]["doctorid_chr"] && dtT21.Rows[i]["pid"] == dtT21.Rows[i - 1]["pid"] && dtT21.Rows[i]["recDate"] == dtT21.Rows[i - 1]["recDate"] && dtT21.Rows[i]["kjrs"] == dtT21.Rows[i - 1]["kjrs"])
                                {
                                    dtT21.Rows.RemoveAt(i);
                                }
                            }
                            if (dicDoct.ContainsKey(dtT21.Rows[i]["doctorid_chr"].ToString()) == false)
                            {
                                dicDoct.Add(dtT21.Rows[i]["doctorid_chr"].ToString(), 0);
                                lstDoctId.Add(dtT21.Rows[i]["doctorid_chr"].ToString());
                            }
                        }
                        foreach (string doctId in lstDoctId)
                        {
                            foreach (DataRow dr in dtT21.Rows)
                            {
                                if (dr["doctorid_chr"].ToString() == doctId)
                                {
                                    dicDoct[doctId] += Convert.ToInt32(dr["kjrs"].ToString());
                                }
                            }
                        }
                        dtT21.Rows.Clear();
                        foreach (string doctId in lstDoctId)
                        {
                            DataRow drNew = dtT21.NewRow();
                            drNew["doctorid_chr"] = doctId;
                            drNew["kjrs"] = dicDoct[doctId];
                            dtT21.Rows.Add(drNew);
                        }
                        dtT21.AcceptChanges();
                    }
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T21表异常：" + e.Message);
                }
                #endregion

                #region T3
                string strSQLT3 = @"select a.doctorid_chr,
                                           sum(case a.status_int
                                                 when 1 then
                                                  1
                                                 when 3 then
                                                  1
                                                 when 2 then
                                                  -1
                                               end) as cfs
                                      from t_opr_outpatientrecipeinv a,
                                           t_opr_reciperelation      b,
                                           t_opr_outpatientrecipe    c
                                     where a.outpatrecipeid_chr = b.seqid
                                       and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                       and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                       and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp'" + p_endDate + @"'
                                       and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                     group by a.doctorid_chr";

                if (p_strStatType == "1")
                {
                    strSQLT3 = strSQLT3.Replace("recorddate_dat", "balance_dat");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT3, ref dtT3);
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T3表异常：" + e.Message);
                }
                #endregion

                #region T4
                string strSQLT4 = @"select t5.diagdr_chr,
                                           t5.tolprice_mny,
                                           t6.xytolprice_mny,
                                           round((t5.tolprice_mny / t6.xytolprice_mny) * 100, 2) as kjybl,
                                           round((t5.kjcfs / t6.zcfs)*100, 2) as kjcfbl
                                      from (select g.lastname_vchr,
                                                   c.diagdr_chr,
                                                   sum(bb.tolprice_mny + decode(d.opchargeflg_int,
                      0,
                      cc.tradeprice_mny - cc.itemprice_mny,
                      1,
                      round((cc.tradeprice_mny - cc.itemprice_mny) /
                            d.packqty_dec,
                            4))* bb.qty_dec) tolprice_mny,
                                                   sum (case a.status_int
                                                       when 1
                                                          then 1
                                                       when 3
                                                          then 1
                                                       when 2
                                                          then -1
                                                    end
                                                   ) as kjcfs
                                              from t_opr_outpatientrecipeinv a,
                                                   t_opr_reciperelation b,
                                                   t_opr_outpatientrecipe c,
                                                   t_opr_oprecipeitemde bb,
                                                   t_bse_chargeitem cc,
                                                   t_bse_medicine d,
                                                   t_bse_deptdesc f,
                                                   t_bse_employee g,
                                                   (select t1.empid_chr, t1.deptid_chr, t2.deptname_vchr
                                                      from t_bse_deptemp t1, t_bse_deptdesc t2
                                                     where t1.default_dept_int = 1
                                                       and t1.deptid_chr = t2.deptid_chr) e
                                             where a.outpatrecipeid_chr = b.seqid
                                               and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                               and c.outpatrecipeid_chr = bb.outpatrecipeid_chr
                                               and bb.itemid_chr = cc.itemid_chr
                                               and c.diagdr_chr = e.empid_chr(+)
                                               and c.diagdept_chr = f.deptid_chr
                                               and c.diagdr_chr = g.empid_chr
                                               and c.pstauts_int in (2, 3)
                                               and cc.itemsrcid_vchr = d.medicineid_chr
                                               and d.medicinetypeid_chr = 2
                                               and d.pharmaid_chr in ('00001', '00005', '00006', '00013')
                                               and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                               and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp'" + p_endDate + @"'
                                               and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                             group by g.lastname_vchr, c.diagdr_chr) t5,
                                           (select g.lastname_vchr,
                                                   c.diagdr_chr,
                                                    sum(bb.tolprice_mny +
                   decode(d.opchargeflg_int,
                          0,
                          cc.tradeprice_mny - cc.itemprice_mny,
                          1,
                          round((cc.tradeprice_mny - cc.itemprice_mny) /
                                d.packqty_dec,
                                4))* bb.qty_dec) xytolprice_mny,
                                                   sum (case a.status_int
                                                       when 1
                                                          then 1
                                                       when 3
                                                          then 1
                                                       when 2
                                                          then -1
                                                    end
                                                   ) as zcfs
                                              from t_opr_outpatientrecipeinv a,
                                                   t_opr_reciperelation b,
                                                   t_opr_outpatientrecipe c,
                                                   t_opr_oprecipeitemde bb,
                                                   t_bse_chargeitem cc,
                                                   t_bse_medicine d,
                                                   t_bse_deptdesc f,
                                                   t_bse_employee g,
                                                   (select t1.empid_chr, t1.deptid_chr, t2.deptname_vchr
                                                      from t_bse_deptemp t1, t_bse_deptdesc t2
                                                     where t1.default_dept_int = 1
                                                       and t1.deptid_chr = t2.deptid_chr) e
                                             where a.outpatrecipeid_chr = b.seqid
                                               and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                               and c.outpatrecipeid_chr = bb.outpatrecipeid_chr
                                               and bb.itemid_chr = cc.itemid_chr
                                               and c.diagdr_chr = e.empid_chr(+)
                                               and c.diagdept_chr = f.deptid_chr
                                               and c.diagdr_chr = g.empid_chr
                                               and c.pstauts_int in (2, 3)
                                               and cc.itemsrcid_vchr = d.medicineid_chr
                                               and d.medicinetypeid_chr = 2
                                               and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                               and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp'" + p_endDate + @"'
                                               and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                             group by g.lastname_vchr, c.diagdr_chr) t6
                                     where t5.diagdr_chr = t6.diagdr_chr
                                     group by t5.diagdr_chr, t5.tolprice_mny, t6.xytolprice_mny,t6.zcfs,t5.kjcfs";

                // and d.pharmaid_chr in ('00001' 抗菌药类, '00005' 非限制使用, '00006' 限制使用, '00013' 特殊使用)
                if (p_strStatType == "1")
                {
                    strSQLT4 = strSQLT4.Replace("recorddate_dat", "balance_dat");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT4, ref dtT4);
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T4表异常：" + e.Message);
                }
                #endregion

                #region T9
                string strSQLT9 = @"select t7.diagdr_chr,
                                           t7.tolprice_mny as tolprice_jby,
                                           t8.xytolprice_mny as xytolprice_jby,
                                           round((t7.tolprice_mny / t8.xytolprice_mny) * 100, 2) as jbybl
                                      from (select g.lastname_vchr,
                                                   c.diagdr_chr,
                                                   sum(bb.tolprice_mny + decode(d.opchargeflg_int,
                      0,
                      cc.tradeprice_mny - cc.itemprice_mny,
                      1,
                      round((cc.tradeprice_mny - cc.itemprice_mny) /
                            d.packqty_dec,
                            4))* bb.qty_dec) tolprice_mny
                                              from t_opr_outpatientrecipeinv a,
                                                   t_opr_reciperelation b,
                                                   t_opr_outpatientrecipe c,
                                                   t_opr_oprecipeitemde bb,
                                                   t_bse_chargeitem cc,
                                                   t_bse_medicine d,
                                                   t_bse_deptdesc f,
                                                   t_bse_employee g,
                                                   (select t1.empid_chr, t1.deptid_chr, t2.deptname_vchr
                                                      from t_bse_deptemp t1, t_bse_deptdesc t2
                                                     where t1.default_dept_int = 1
                                                       and t1.deptid_chr = t2.deptid_chr) e
                                             where a.outpatrecipeid_chr = b.seqid
                                               and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                               and c.outpatrecipeid_chr = bb.outpatrecipeid_chr
                                               and bb.itemid_chr = cc.itemid_chr
                                               and c.diagdr_chr = e.empid_chr(+)
                                               and c.diagdept_chr = f.deptid_chr
                                               and c.diagdr_chr = g.empid_chr
                                               and c.pstauts_int in (2, 3)
                                               and cc.itemsrcid_vchr = d.medicineid_chr
                                               and d.medicinetypeid_chr = 2
                                               --and d.pharmaid_chr in ('00001', '00005', '00006', '00013')
                                               and d.insurancetype_vchr in ('1006', '1007', '1008')
                                               and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                               and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp'" + p_endDate + @"'
                                               and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                             group by g.lastname_vchr, c.diagdr_chr) t7,
                                           (select g.lastname_vchr,
                                                   c.diagdr_chr,
                                                   sum(bb.tolprice_mny +
                   decode(d.opchargeflg_int,
                          0,
                          cc.tradeprice_mny - cc.itemprice_mny,
                          1,
                          round((cc.tradeprice_mny - cc.itemprice_mny) /
                                d.packqty_dec,
                                4))* bb.qty_dec) xytolprice_mny
                                              from t_opr_outpatientrecipeinv a,
                                                   t_opr_reciperelation b,
                                                   t_opr_outpatientrecipe c,
                                                   t_opr_oprecipeitemde bb,
                                                   t_bse_chargeitem cc,
                                                   t_bse_medicine d,
                                                   t_bse_deptdesc f,
                                                   t_bse_employee g,
                                                   (select t1.empid_chr, t1.deptid_chr, t2.deptname_vchr
                                                      from t_bse_deptemp t1, t_bse_deptdesc t2
                                                     where t1.default_dept_int = 1
                                                       and t1.deptid_chr = t2.deptid_chr) e
                                             where a.outpatrecipeid_chr = b.seqid
                                               and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                               and c.outpatrecipeid_chr = bb.outpatrecipeid_chr
                                               and bb.itemid_chr = cc.itemid_chr
                                               and c.diagdr_chr = e.empid_chr(+)
                                               and c.diagdept_chr = f.deptid_chr
                                               and c.diagdr_chr = g.empid_chr
                                               and c.pstauts_int in (2, 3)
                                               and cc.itemsrcid_vchr = d.medicineid_chr
                                               and d.medicinetypeid_chr = 2
                                               and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                               and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp'" + p_endDate + @"'
                                               and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                             group by g.lastname_vchr, c.diagdr_chr) t8
                                     where t7.diagdr_chr = t8.diagdr_chr
                                     group by t7.diagdr_chr, t7.tolprice_mny, t8.xytolprice_mny";

                // t_aid_medicaretype.typeid_chr = d.insurancetype_vchr in ('1006' 自费(基), '1007' 甲类(基), '1008' 乙类(基))
                if (p_strStatType == "1")
                {
                    strSQLT9 = strSQLT9.Replace("recorddate_dat", "balance_dat");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT9, ref dtT9);
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T9表异常：" + e.Message);
                }
                #endregion

                #region T10
                string strSQLT10 = @"select m.doctorid_chr, count(m.outpatrecipeid_chr) Counts_KJY
  from (select n.doctorid_chr, n.outpatrecipeid_chr,sum(n.status) as statussum
  from (select b.doctorid_chr, t.outpatrecipeid_chr,decode(b.status_int,0,-1,2,-1,1) as status
          from t_opr_outpatientpwmrecipede t,
               t_opr_reciperelation        a,
               t_opr_outpatientrecipeinv   b,
               t_bse_medicine              c,
               t_bse_chargeitem            e
         where t.outpatrecipeid_chr = a.outpatrecipeid_chr
           and a.seqid = b.outpatrecipeid_chr
           and t.itemid_chr = e.itemid_chr
           and e.itemsrcid_vchr = c.medicineid_chr
           and c.pharmaid_chr in ('00001', '00005', '00006', '00013')  
           and( b.isvouchers_int <> 2 or b.isvouchers_int is null)
           and b.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp '" + p_endDate + @"' 
           and b.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')) n
               group by n.doctorid_chr, n.outpatrecipeid_chr) m
                       where m.statussum<>0
 group by m.doctorid_chr";

                if (p_strStatType == "1")
                {
                    strSQLT10 = strSQLT10.Replace("recorddate_dat", "balance_dat");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT10, ref dtT10);
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T10表异常：" + e.Message);
                }
                #endregion

                #region T11
                string strSQLT11 = @"select tab1.doctorid_chr, sum(case tab1.status_int
                                                                     when 1 then
                                                                      1
                                                                     when 3 then
                                                                      1
                                                                     when 2 then
                                                                      -1
                                                                   end) as xycfs
                                              from (select distinct a.outpatrecipeid_chr,
                                                                    a.status_int,
                                                                    a.doctorid_chr
                                                      from t_opr_outpatientrecipeinv a,
                                                           t_opr_reciperelation      b,
                                                           t_opr_outpatientrecipe    c,
                                                           t_opr_outpatientpwmrecipede t,
                                                           t_bse_chargeitem            e,
                                                           t_bse_medicine              m
                                                     where a.outpatrecipeid_chr = b.seqid
                                                       and b.outpatrecipeid_chr = c.outpatrecipeid_chr
                                                       and t.outpatrecipeid_chr = b.outpatrecipeid_chr
                                                       and t.itemid_chr = e.itemid_chr
                                                       and e.itemsrcid_vchr = m.medicineid_chr
                                                       and m.medicinetypeid_chr = '2'
                                                       and( a.isvouchers_int <> 2 or a.isvouchers_int is null)
                                                       and a.recorddate_dat between timestamp '" + p_beginDate + @"' and timestamp'" + p_endDate + @"'
                                                       and a.recorddate_dat between to_date('" + p_beginDate + @"', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + p_endDate + @"', 'yyyy-mm-dd hh24:mi:ss')
                                                    ) tab1
                                               group by tab1.doctorid_chr";

                if (p_strStatType == "1")
                {
                    strSQLT11 = strSQLT11.Replace("recorddate_dat", "balance_dat");
                }

                try
                {
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQLT11, ref dtT11);
                }
                catch (Exception e)
                {
                    clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError("获取T11表异常：" + e.Message);
                }
                #endregion

                #region 数据合并处理整合为和原查询语句得到的datatable相同

                string[] strDtT2JoinColArr = new string[] { "zfs" };
                dtT1 = m_dtbDataTableLeftJoin(dtT1, dtT2, "doctorid_chr", "doctorid_chr", strDtT2JoinColArr);

                string[] strDtT21JoinColArr = new string[] { "kjrs" };
                dtT1 = m_dtbDataTableLeftJoin(dtT1, dtT21, "doctorid_chr", "doctorid_chr", strDtT21JoinColArr);

                string[] strDtT3JoinColArr = new string[] { "cfs" };
                dtT1 = m_dtbDataTableLeftJoin(dtT1, dtT3, "doctorid_chr", "doctorid_chr", strDtT3JoinColArr);

                string[] strDtT4JoinColArr = new string[] { "kjybl", "tolprice_mny", "xytolprice_mny" };
                dtT1 = m_dtbDataTableLeftJoin(dtT1, dtT4, "doctorid_chr", "diagdr_chr", strDtT4JoinColArr);

                string[] strDtT9JoinColArr = new string[] { "jbybl", "tolprice_jby", "xytolprice_jby" };
                dtT1 = m_dtbDataTableLeftJoin(dtT1, dtT9, "doctorid_chr", "diagdr_chr", strDtT9JoinColArr);

                string[] strDtT10JoinColArr = new string[] { "Counts_KJY" };
                dtT1 = m_dtbDataTableLeftJoin(dtT1, dtT10, "doctorid_chr", "doctorid_chr", strDtT10JoinColArr);

                string[] strDtT11JoinColArr = new string[] { "xycfs" };
                dtT1 = m_dtbDataTableLeftJoin(dtT1, dtT11, "doctorid_chr", "doctorid_chr", strDtT11JoinColArr);

                DataView dvJoinResult = dtT1.DefaultView;
                dvJoinResult.Sort = "empno_chr, lastname_vchr, typeid_chr, typename_vchr, zfs, kjrs, cfs, code_vchr, deptname_vchr, tolprice_mny, xytolprice_mny, kjybl, counts_kjy, xycfs, jbybl";
                DataTable dtFinalResult = dvJoinResult.ToTable();

                for (int i = 0; i < dtFinalResult.Rows.Count - 1; i++)
                {
                    DataRow drFirst = dtFinalResult.Rows[i];
                    DataRow drSecond = dtFinalResult.Rows[i + 1];

                    if (drFirst["empno_chr"] == drSecond["empno_chr"] && drFirst["lastname_vchr"] == drSecond["lastname_vchr"] && drFirst["typeid_chr"] == drSecond["typeid_chr"])
                    {
                        if (drFirst["typename_vchr"] == drSecond["typename_vchr"] && drFirst["zfs"] == drSecond["zfs"] && drFirst["kjrs"] == drSecond["kjrs"] && drFirst["cfs"] == drSecond["cfs"] && drFirst["xytolprice_mny"] == drSecond["xytolprice_mny"] && drFirst["xytolprice_jby"] == drSecond["xytolprice_jby"])
                        {
                            if (drFirst["code_vchr"] == drSecond["code_vchr"] && drFirst["deptname_vchr"] == drSecond["deptname_vchr"] && drFirst["tolprice_mny"] == drSecond["tolprice_mny"] && drFirst["tolprice_jby"] == drSecond["tolprice_jby"])
                            {
                                if (drFirst["kjybl"] == drSecond["kjybl"] && drFirst["counts_kjy"] == drSecond["counts_kjy"] && drFirst["xycfs"] == drSecond["xycfs"] && drFirst["jbybl"] == drSecond["jbybl"])
                                {
                                    if (dtFinalResult.Rows[i]["xytolprice_mny"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i]["xytolprice_mny"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i]["xytolprice_jby"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i]["xytolprice_jby"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i]["tolfee_mny"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i]["tolfee_mny"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i]["tolfee_jby"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i]["tolfee_jby"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i]["jxywl"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i]["jxywl"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i + 1]["xytolprice_mny"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i + 1]["xytolprice_mny"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i + 1]["xytolprice_jby"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i + 1]["xytolprice_jby"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i + 1]["tolfee_mny"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i + 1]["tolfee_mny"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i + 1]["tolfee_jby"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i + 1]["tolfee_jby"] = 0;
                                    }
                                    if (dtFinalResult.Rows[i + 1]["jxywl"] == DBNull.Value)
                                    {
                                        dtFinalResult.Rows[i + 1]["jxywl"] = 0;
                                    }
                                    dtFinalResult.Rows[i]["tolfee_mny"] = Convert.ToDecimal(dtFinalResult.Rows[i]["tolfee_mny"]) + Convert.ToDecimal(dtFinalResult.Rows[i + 1]["tolfee_mny"]);
                                    dtFinalResult.Rows[i]["tolfee_jby"] = Convert.ToDecimal(dtFinalResult.Rows[i]["tolfee_jby"]) + Convert.ToDecimal(dtFinalResult.Rows[i + 1]["tolfee_jby"]);
                                    dtFinalResult.Rows[i]["xytolprice_mny"] = Convert.ToDecimal(dtFinalResult.Rows[i]["xytolprice_mny"]) + Convert.ToDecimal(dtFinalResult.Rows[i + 1]["xytolprice_mny"]);
                                    dtFinalResult.Rows[i]["xytolprice_jby"] = Convert.ToDecimal(dtFinalResult.Rows[i]["xytolprice_jby"]) + Convert.ToDecimal(dtFinalResult.Rows[i + 1]["xytolprice_jby"]);
                                    dtFinalResult.Rows[i]["jxywl"] = Convert.ToDecimal(dtFinalResult.Rows[i]["jxywl"]) + Convert.ToDecimal(dtFinalResult.Rows[i + 1]["jxywl"]);
                                    dtFinalResult.Rows.Remove(drSecond);
                                    dtFinalResult.AcceptChanges();
                                    i = i - 1;
                                }
                            }
                        }
                    }
                }
                dtResult.Columns.Add("typeid_chr", typeof(string));
                dtResult.Columns.Add("typename_vchr", typeof(string));
                dtResult.Columns.Add("empno_chr", typeof(string));
                dtResult.Columns.Add("lastname_vchr", typeof(string));
                dtResult.Columns.Add("zfs", typeof(int));
                dtResult.Columns.Add("kjrs", typeof(int));
                dtResult.Columns.Add("cfs", typeof(int));
                dtResult.Columns.Add("code_vchr", typeof(string));
                dtResult.Columns.Add("deptname_vchr", typeof(string));
                dtResult.Columns.Add("tolfee_mny", typeof(decimal));
                dtResult.Columns.Add("jxywl", typeof(decimal));
                dtResult.Columns.Add("kjybl", typeof(string));
                dtResult.Columns.Add("tolprice_mny", typeof(decimal));
                dtResult.Columns.Add("xytolprice_mny", typeof(decimal));
                //dtResult.Columns.Add("kjcfbl", typeof(decimal));
                dtResult.Columns.Add("kjysybl", typeof(string));
                dtResult.Columns.Add("jbybl", typeof(string));
                dtResult.Columns.Add("tolprice_jby", typeof(decimal));
                dtResult.Columns.Add("xytolprice_jby", typeof(decimal));
                for (int i = 0; i < dtFinalResult.Rows.Count; i++)
                {
                    DataRow drResu = dtResult.NewRow();
                    drResu["typeid_chr"] = dtFinalResult.Rows[i]["typeid_chr"];
                    drResu["typename_vchr"] = dtFinalResult.Rows[i]["typename_vchr"];
                    drResu["empno_chr"] = dtFinalResult.Rows[i]["empno_chr"];
                    drResu["lastname_vchr"] = dtFinalResult.Rows[i]["lastname_vchr"];
                    drResu["zfs"] = dtFinalResult.Rows[i]["zfs"];
                    drResu["kjrs"] = dtFinalResult.Rows[i]["kjrs"];
                    drResu["cfs"] = dtFinalResult.Rows[i]["cfs"];
                    drResu["code_vchr"] = dtFinalResult.Rows[i]["code_vchr"];
                    drResu["deptname_vchr"] = dtFinalResult.Rows[i]["deptname_vchr"];
                    drResu["tolfee_mny"] = dtFinalResult.Rows[i]["tolfee_mny"];
                    drResu["jxywl"] = dtFinalResult.Rows[i]["jxywl"];
                    drResu["kjybl"] = dtFinalResult.Rows[i]["kjybl"] == DBNull.Value || Convert.ToDecimal(dtFinalResult.Rows[i]["kjybl"]) == 0 ? "" : dtFinalResult.Rows[i]["kjybl"].ToString() + "%";
                    drResu["tolprice_mny"] = dtFinalResult.Rows[i]["tolprice_mny"];
                    drResu["xytolprice_mny"] = dtFinalResult.Rows[i]["xytolprice_mny"];
                    drResu["jbybl"] = dtFinalResult.Rows[i]["jbybl"] == DBNull.Value || Convert.ToDecimal(dtFinalResult.Rows[i]["jbybl"]) == 0 ? "" : dtFinalResult.Rows[i]["jbybl"].ToString() + "%";
                    drResu["tolprice_jby"] = dtFinalResult.Rows[i]["tolprice_jby"];
                    drResu["xytolprice_jby"] = dtFinalResult.Rows[i]["xytolprice_jby"];
                    //if (dtFinalResult.Rows[i]["Counts_KJY"] != DBNull.Value && dtFinalResult.Rows[i]["xycfs"] != DBNull.Value)
                    //{
                    //    drResu["kjcfbl"] = Math.Round((Convert.ToDecimal(dtFinalResult.Rows[i]["Counts_KJY"]) / Convert.ToDecimal(dtFinalResult.Rows[i]["xycfs"])) * 100, 2);
                    //}
                    if (dtFinalResult.Rows[i]["kjrs"] != DBNull.Value && dtFinalResult.Rows[i]["zfs"] != DBNull.Value && Convert.ToInt32(dtFinalResult.Rows[i]["zfs"]) != 0)
                    {
                        drResu["kjysybl"] = this.Round((Convert.ToDecimal(dtFinalResult.Rows[i]["kjrs"]) / Convert.ToDecimal(dtFinalResult.Rows[i]["zfs"])) * 100, 2).ToString() + "%";
                    }
                    dtResult.Rows.Add(drResu);
                }

                dtResult.AcceptChanges();
                #endregion
            }
            return lngRes;
        }
        #endregion

        #region 两个datatable左连接
        /// <summary>
        /// 两个datatable左连接
        /// </summary>
        /// <param name="dtMain"></param>
        /// <param name="dtSub"></param>
        /// <param name="strLinkColumn"></param>
        /// <param name="strJoinColumnArr"></param>
        /// <returns></returns>
        public DataTable m_dtbDataTableLeftJoin(DataTable dtMain, DataTable dtSub, string strMainIDColumn, string strSubIDColumn, string[] strJoinColumnArr)
        {
            for (int i = 0; i < strJoinColumnArr.Length; i++)
            {
                dtMain.Columns.Add(strJoinColumnArr[i]);
            }

            for (int i = 0; i < dtMain.Rows.Count; i++)
            {
                string strMainID = dtMain.Rows[i][strMainIDColumn].ToString();
                DataRow[] drEqualArr = dtSub.Select(strSubIDColumn + " = " + strMainID);
                if (drEqualArr != null && drEqualArr.Length > 0)
                {
                    DataRow drEqu = drEqualArr[0];
                    for (int j = 0; j < strJoinColumnArr.Length; j++)
                    {
                        dtMain.Rows[i][strJoinColumnArr[j]] = drEqu[strJoinColumnArr[j]];
                    }
                    dtSub.AcceptChanges();
                }
            }
            dtMain.AcceptChanges();
            return dtMain;
        }
        #endregion

        #region 将数值四舍五入
        /// <summary>
        /// 将数值四舍五入
        /// </summary>
        /// <param name="d">数值</param>
        /// <param name="decimals">小数位数</param>
        /// <returns></returns>
        decimal Round(decimal d, int decimals)
        {
            try
            {
                if (decimals < 1)
                {
                    return Convert.ToDecimal(Convert.ToInt32(d));
                }
                else
                {
                    string s = "0.";
                    for (int i = 0; i < decimals; i++)
                    {
                        s += "0";
                    }
                    return Convert.ToDecimal(d.ToString(s));
                }
            }
            catch
            {
                return d;
            }
        }
        #endregion

        #region 按时间统计收费 
        /// <summary>
        /// 按时间统计收费
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_dtm1">开始时间</param>
        /// <param name="p_dtm2">结束时间</param>
        /// <param name="p_dtb">查询得到的结果</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetStatiticsData(string p_dtm1, string p_dtm2, out DataTable p_dtb)
        {
            p_dtb = new DataTable();
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"SELECT t1.*, t2.typename_vchr
									FROM (SELECT   b.itemcatid_chr, SUM (b.tolfee_mny) AS totalmoney
											 FROM t_opr_outpatientrecipesumde b, t_opr_outpatientrecipeinv a
											 WHERE b.seqid_chr = a.seqid_chr
													 AND a.recorddate_dat BETWEEN to_date('" + p_dtm1 + " 00:00:00" + "','yyyy-mm-dd hh24:mi:ss')";
            strSQL += "	AND to_date('" + p_dtm2 + " 23:59:59" + "','yyyy-mm-dd hh24:mi:ss')";
            strSQL += " AND a.balanceflag_int = 1";
            strSQL += " GROUP BY b.itemcatid_chr) t1,";
            strSQL += " t_bse_chargeitemextype t2";
            strSQL += " WHERE t1.itemcatid_chr = t2.typeid_chr";
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref p_dtb);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            objHRPSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region GetCheckOutData
        /// <summary>
        /// 收费结算日报表(未结账的发票信息)
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strDate"></param>
        /// <param name="dtCheckOut"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetCheckOutData(string OPREMPID, string strDate, string strRptId, out DataTable dtCheckOut)
        {
            if (strRptId == null || strRptId == "")
            {
                throw new Exception("报表的Id号为空，请从功能菜单传入报表Id号。");
            }
            long lngRes = 0;
            dtCheckOut = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            //string strSQL = @"Select * From t_bse_chargeitemextype  Where flag_int='1' order by SORTCODE_INT";
            //try
            //{
            //    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtPayType);
            //}
            //catch (Exception objEx)
            //{
            //    string strTmp = objEx.Message;
            //    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
            //    bool blnRes = objLogger.LogError(objEx);
            //}
            string strSQL = @"SELECT a.invoiceno_vchr,
                                       a.recorddate_dat,
                                       a.opremp_chr,
                                       a.status_int,
                                       a.seqid_chr,
                                       a.balanceemp_chr,
                                       a.paytypeid_chr,
                                       a.acctsum_mny,
                                       a.sbsum_mny,
                                       a.totalsum_mny,
                                       a.paytype_int,
                                       f.internalflag_int,
                                       b.itemcatid_chr,
                                       b.tolfee_mny,
                                       e.groupid_chr,
                                       e.groupname_chr
                                  FROM t_opr_outpatientrecipeinv   a,
                                       t_opr_outpatientrecipesumde b,
                                       t_bse_patientpaytype f,
                                       (select c.typeid_chr, d.groupid_chr, d.groupname_chr from t_aid_rpt_gop_rla           c,
                                                     t_aid_rpt_gop_def           d
                                                     where  D.GROUPID_CHR = C.GROUPID_CHR 
                                                           AND D.RPTID_CHR = C.RPTID_CHR
                                                           AND d.rptid_chr = ?) e
                                 WHERE a.invoiceno_vchr = b.invoiceno_vchr
                                   AND a.seqid_chr = b.seqid_chr
                                   AND b.itemcatid_chr = e.TYPEID_CHR(+)
                                   AND A.PAYTYPEID_CHR = F.PAYTYPEID_CHR
                                   AND a.balanceflag_int = 0
                                    and exists (select 1
                                              from t_opr_invoicerepeatprint t2
                                             where t2.sourceinvono_vchr = a.invoiceno_vchr
                                               and t2.type_chr = '1'
                                               and t2.printstatus_int >= 0)
                                   AND a.recorddate_dat < TO_DATE(?, 'yyyy-mm-dd HH24:mi:ss')
                                   AND a.recordemp_chr = ?
                                 ORDER BY a.INVOICENO_VCHR,a.seqid_chr";

            try
            {
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                paramArr[0].Value = strRptId;
                paramArr[1].Value = strDate;
                paramArr[2].Value = OPREMPID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtCheckOut, paramArr);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 根据指定的条件查询已日结报表
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="intMode">1按结算时间，0按发票时间</param>
        /// <param name="OPREMPID">收费员ID</param>
        /// <param name="strStartDate">开始时间</param>
        /// <param name="strEndDate">结束时间</param>
        /// <param name="strRptId">报表字段分类</param>
        /// <param name="dtCheckOut"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetCheckOutData(int intMode, string OPREMPID, string strStartDate, string strEndDate, string strRptId, out DataTable dtCheckOut)
        {
            if (strRptId == null || strRptId == "")
            {
                throw new Exception("报表的Id号为空，请从功能菜单传入报表Id号。");
            }
            long lngRes = 0;
            dtCheckOut = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"SELECT a.invoiceno_vchr,
                                       a.recorddate_dat,
                                       a.opremp_chr,
                                       a.status_int,
                                       a.seqid_chr,
                                       a.balanceemp_chr,
                                       a.paytypeid_chr,
                                       a.acctsum_mny,
                                       a.sbsum_mny,
                                       a.totalsum_mny,
                                       a.paytype_int,
                                       f.internalflag_int,
                                       b.itemcatid_chr,
                                       b.tolfee_mny,
                                       e.groupid_chr,
                                       e.groupname_chr
                                  FROM t_opr_outpatientrecipeinv   a,
                                       t_opr_outpatientrecipesumde b,
                                       t_bse_patientpaytype f,
                                       (select c.typeid_chr, d.groupid_chr, d.groupname_chr from t_aid_rpt_gop_rla           c,
                                                     t_aid_rpt_gop_def           d
                                                     where  D.GROUPID_CHR = C.GROUPID_CHR 
                                                           AND D.RPTID_CHR = C.RPTID_CHR
                                                           AND d.rptid_chr = ?) e
                                 WHERE a.invoiceno_vchr = b.invoiceno_vchr
                                   AND a.seqid_chr = b.seqid_chr
                                   AND b.itemcatid_chr = e.TYPEID_CHR(+)
                                   AND A.PAYTYPEID_CHR = F.PAYTYPEID_CHR
                                   AND a.{date} between TO_DATE(?, 'yyyy-mm-dd HH24:mi:ss') and TO_DATE(?, 'yyyy-mm-dd HH24:mi:ss')
                                   {emp}
                                 ORDER BY a.INVOICENO_VCHR,a.seqid_chr";

            try
            {
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter((OPREMPID == "1000" ? 3 : 4), out paramArr);
                paramArr[0].Value = strRptId;
                paramArr[1].Value = strStartDate;
                paramArr[2].Value = strEndDate;
                if (OPREMPID != "1000")
                {
                    paramArr[3].Value = OPREMPID;
                    strSQL = strSQL.Replace("{emp}", "AND a.OPREMP_CHR='" + OPREMPID + "'");
                }
                else
                {
                    strSQL = strSQL.Replace("{emp}", "");
                }
                if (intMode == 0)
                {
                    strSQL = strSQL.Replace("{date}", "recorddate_dat");
                }
                else
                {
                    strSQL = strSQL.Replace("{date}", "balance_dat");
                }
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtCheckOut, paramArr);
                objHRPSvc.Dispose();
                objHRPSvc = null;

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

        #region 根据结帐人、结帐时间获取相应的重打发票信息
        /// <summary>
        /// 根据结帐人、结帐时间获取相应的重打发票信息
        /// </summary>
        /// <param name="BalanceEmp"></param>
        /// <param name="BalanceTime"></param>
        /// <returns></returns>
        [AutoComplete]
        public void m_mthGetbalancerepeatinvoinfo(string BalanceEmp, string BalanceTime, out string[] InvonoArr, int status)
        {
            InvonoArr = null;
            DataTable dt = new DataTable();
            long lngRes = 0;
            string SQL = "";

            //未结帐
            if (status == 0)
            {
                SQL = @"select b.repprninvono_vchr
                      from t_opr_outpatientrecipeinv a,
                           t_opr_invoicerepeatprint b
                     where a.seqid_chr = b.seqid_chr 
                       and a.balanceflag_int = 0 
                       and b.type_chr = '1' 
                       and exists (select 1
                          from t_opr_invoicerepeatprint t
                          where t.seqid_chr = a.seqid_chr
                           and t.sourceinvono_vchr = a.invoiceno_vchr
                         group by t.sourceinvono_vchr
                        having count(t.sourceinvono_vchr) > 1)
                       and a.recordemp_chr = ? 
                       and a.recorddate_dat < to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                    order by b.repprninvono_vchr";
            }
            //已结帐
            else if (status == 1)
            {
                SQL = @"select b.repprninvono_vchr
                      from t_opr_outpatientrecipeinv a,
                           t_opr_invoicerepeatprint b
                     where a.seqid_chr = b.seqid_chr 
                       and a.balanceflag_int = 1 
                       and b.type_chr = '1' 
                       and exists (select 1
                              from t_opr_invoicerepeatprint t
                              where t.seqid_chr = a.seqid_chr
                               and t.sourceinvono_vchr = a.invoiceno_vchr
                             group by t.sourceinvono_vchr
                            having count(t.sourceinvono_vchr) > 1)
                       and a.balanceemp_chr = ?
                       and a.balance_dat = to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                    order by b.repprninvono_vchr";
            }
            else
            {
                return;
            }

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = BalanceEmp;
                ParamArr[1].Value = BalanceTime;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            if (lngRes > 0 && dt.Rows.Count > 0)
            {
                InvonoArr = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    InvonoArr[i] = dt.Rows[i][0].ToString();
                }
            }
        }
        /// <summary>
        /// 根据结帐人、结帐时间获取相应的重打发票信息
        /// </summary>
        /// <param name="BalanceEmp">结帐人ID</param>
        /// <param name="strBeginDate">开始时间</param>
        /// <param name="strEndDate">结束时间</param>
        /// <param name="InvonoArr">返回结果</param>
        /// <param name="intMode">0为发票时间，1为结算时间</param>
        /// <returns></returns>
        [AutoComplete]
        public void m_mthGetbalancerepeatinvoinfo(string BalanceEmp, string strBeginDate, string strEndDate, out string[] InvonoArr, int intMode)
        {
            InvonoArr = null;
            DataTable dt = new DataTable();
            long lngRes = 0;
            string SQL = @"select b.repprninvono_vchr
                      from t_opr_outpatientrecipeinv a,
                           t_opr_invoicerepeatprint b
                     where a.seqid_chr = b.seqid_chr 
                       and a.balanceflag_int = 0 
                       and b.type_chr = '1' 
                       {emp}
                       and a.{date} between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                    order by b.repprninvono_vchr";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                if (BalanceEmp != "1000")
                {
                    SQL = SQL.Replace("{emp}", "a.recordemp_chr = ?");
                    objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                    ParamArr[0].Value = BalanceEmp;
                    ParamArr[1].Value = strBeginDate;
                    ParamArr[2].Value = strEndDate;
                }
                else
                {
                    SQL = SQL.Replace("{emp}", "");
                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = strBeginDate;
                    ParamArr[1].Value = strEndDate;
                }
                if (intMode == 0)
                {
                    SQL = SQL.Replace("{date}", "recorddate_dat");
                }
                else
                {
                    SQL = SQL.Replace("{date}", "balance_dat");
                }

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                objHRPSvc.Dispose();
                objHRPSvc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            if (lngRes > 0 && dt.Rows.Count > 0)
            {
                InvonoArr = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    InvonoArr[i] = dt.Rows[i][0].ToString();
                }
            }
        }
        #endregion

        #region GetCheckOutHistory
        /// <summary>
        /// GetCheckOutHistory
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="BALANCEEMP"></param>
        /// <param name="strRptId"></param>
        /// <param name="dtCheckOut"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetCheckOutHistory(string strDate, string BALANCEEMP, string strRptId, out DataTable dtCheckOut)
        {
            if (strRptId == null || strRptId == "")
            {
                throw new Exception("报表的Id号为空，请从功能菜单传入报表Id号。");
            }

            long lngRes = 0;
            dtCheckOut = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL;

            strSQL = @"SELECT a.invoiceno_vchr,
                           a.recorddate_dat,
                           a.opremp_chr,
                           a.status_int,
                           a.seqid_chr,
                           a.balanceemp_chr,
                           a.paytypeid_chr,
                           a.acctsum_mny,
                           a.sbsum_mny,
                           a.totalsum_mny,
                           a.paytype_int,
                           f.internalflag_int,
                           b.itemcatid_chr,
                           b.tolfee_mny,
                           e.groupid_chr,
                           e.groupname_chr
                      FROM t_opr_outpatientrecipeinv   a,
                           t_opr_outpatientrecipesumde b,
                           t_bse_patientpaytype f,
                           (select c.typeid_chr, d.groupid_chr, d.groupname_chr 
                                    from t_aid_rpt_gop_rla           c,
                                         t_aid_rpt_gop_def           d
                                 where  D.GROUPID_CHR = C.GROUPID_CHR 
                                       AND D.RPTID_CHR = C.RPTID_CHR
                                       AND d.rptid_chr = ?) e
                     WHERE a.invoiceno_vchr = b.invoiceno_vchr
                       AND a.seqid_chr = b.seqid_chr
                       AND b.itemcatid_chr = e.TYPEID_CHR(+)
                       AND A.PAYTYPEID_CHR = F.PAYTYPEID_CHR(+)
                       AND a.balanceflag_int = 1
                       and exists (select 1
                                              from t_opr_invoicerepeatprint t2
                                             where  t2.sourceinvono_vchr = a.invoiceno_vchr
                                               and t2.type_chr = '1'
                                               and t2.printstatus_int >= 0)
                       AND a.balance_dat = TO_DATE(?, 'yyyy-mm-dd HH24:mi:ss')
                       AND a.balanceemp_chr = ?
                     order by a.INVOICENO_VCHR, a.SEQID_CHR";
            try
            {

                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                paramArr[0].Value = strRptId;
                paramArr[1].Value = strDate;
                paramArr[2].Value = BALANCEEMP;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtCheckOut, paramArr);
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

        #region 历史查询
        [AutoComplete]
        public long m_lngGetHistor(string startDate, string endDate, string checkMan, out DataTable dt)
        {
            dt = null;
            long lngRes = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

            strSQL = @"select distinct BALANCE_DAT from t_opr_outpatientrecipeinv where BALANCE_DAT between to_date(?,'yyyy-mm-dd hh24:mi:ss') and to_date(?,'yyyy-mm-dd hh24:mi:ss') and BALANCEEMP_CHR=? and BALANCEFLAG_INT=1 order by BALANCE_DAT";
            try
            {
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                paramArr[0].Value = startDate + " 00:00:00";
                paramArr[1].Value = endDate + " 23:59:59";
                paramArr[2].Value = checkMan;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, paramArr);

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

        #region 结帐(新)
        /// <summary>
        /// 结帐(新)
        /// </summary>
        /// <param name="OperID">收款员ID</param>
        /// <param name="CheckDate">日结时间</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckData(string OperID, out string CheckDate)
        {
            long lngRes = 0;
            string strSQL = "";

            CheckDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            strSQL = @"update t_opr_outpatientrecipeinv 
                          set balanceemp_chr = ?, 
                              balance_dat = to_date(?, 'yyyy-mm-dd hh24:mi:ss'), 
                              balanceflag_int = 1 
                        where balanceflag_int = 0
                          and recordemp_chr = ?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                paramArr[0].Value = OperID;
                paramArr[1].Value = CheckDate;
                paramArr[2].Value = OperID;

                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }

        /// <summary>
        /// 指定日期前结帐
        /// </summary>
        /// <param name="OperID">收款员ID</param>
        /// <param name="strIdentCheckDate">指定日期</param>
        /// <param name="CheckDate">日结时间</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckDataByDate(string OperID, string strIdentCheckDate, out string CheckDate)
        {
            long lngRes = 0;
            string strSQL = "";

            CheckDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            strSQL = @"update t_opr_outpatientrecipeinv 
                          set balanceemp_chr = ?, 
                              balance_dat = to_date(?, 'yyyy-mm-dd hh24:mi:ss'), 
                              balanceflag_int = 1 
                        where balanceflag_int = 0
                          and recordemp_chr = ?
                          and recorddate_dat < to_date(?, 'yyyy-mm-dd hh24:mi:ss')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(4, out paramArr);
                paramArr[0].Value = OperID;
                paramArr[1].Value = CheckDate;
                paramArr[2].Value = OperID;
                paramArr[3].Value = strIdentCheckDate;

                long lngRecordsAffected = -1;
                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngRecordsAffected, paramArr);
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

        #region 根据传入条件获取已结帐数据信息
        /// <summary>
        /// 根据传入条件获取已结帐数据信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_intStatDateType"></param>
        /// <param name="m_strCheckManID"></param>
        /// <param name="m_strBalanceDeptID"></param>
        /// <param name="m_strBeginTime"></param>
        /// <param name="m_strEndTime"></param>
        /// <param name="strRptId"></param>
        /// <param name="fysfcdeptidARR"></param>
        /// <param name="m_dtCheckOutData"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetCheckedOutDataByCondition(int m_intStatDateType, string m_strCheckManID, string m_strBalanceDeptID, string m_strBeginTime, string m_strEndTime, string strRptId, List<string> fysfcdeptidARR, out DataTable m_dtCheckOutData, out DataTable dtTemp)
        {
            long lngRes = 0;
            m_dtCheckOutData = null;
            dtTemp = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select a.seqid_chr,  a.invoiceno_vchr, a.recorddate_dat, a.opremp_chr, a.status_int,
         a.seqid_chr, a.balanceemp_chr, a.paytypeid_chr, a.acctsum_mny,
         a.sbsum_mny, a.totalsum_mny, a.paytype_int, f.internalflag_int,
         b.itemcatid_chr, b.tolfee_mny, e.groupid_chr, e.groupname_chr,a.totaldiffcost_mny
    from t_opr_outpatientrecipeinv a,
         t_opr_outpatientrecipesumde b,
         t_bse_patientpaytype f,
         (select c.typeid_chr, d.groupid_chr, d.groupname_chr
            from t_aid_rpt_gop_rla c, t_aid_rpt_gop_def d
           where d.groupid_chr = c.groupid_chr
             and d.rptid_chr = c.rptid_chr
             and d.rptid_chr = ?) e
   where a.invoiceno_vchr = b.invoiceno_vchr
     and a.seqid_chr = b.seqid_chr
     and b.itemcatid_chr = e.typeid_chr(+)
     and a.paytypeid_chr = f.paytypeid_chr
     and ( a.isvouchers_int < 2 or a.isvouchers_int is null)
     and a.balance_dat between to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                           and to_date (?, 'yyyy-mm-dd hh24:mi:ss')
order by a.invoiceno_vchr, a.seqid_chr";

            string SubStr = "";
            if (fysfcdeptidARR != null && fysfcdeptidARR.Count > 0)
            {
                string str = "";
                for (int i = 0; i < fysfcdeptidARR.Count; i++)
                {
                    str += " '" + fysfcdeptidARR[i].ToString() + "',";
                }
                str = str.Trim();

                SubStr = "and a.chargedeptid_chr not in (" + str.Substring(0, str.Length - 1) + ")";
            }

            if (m_strCheckManID.Trim() == "1000" && m_strBalanceDeptID.Trim() == "1000")
            {
                if (m_intStatDateType == 0)
                {
                    strSQL = strSQL.Replace("balance_dat", "recorddate_dat");
                }
                try
                {
                    System.Data.IDataParameter[] paramArr = null;
                    objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                    paramArr[0].Value = strRptId;
                    paramArr[1].Value = m_strBeginTime;
                    paramArr[2].Value = m_strEndTime;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_dtCheckOutData, paramArr);

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            else if (m_strCheckManID.Trim() == "1000" && m_strBalanceDeptID.Trim() != "1000")
            {
                strSQL = @"select a.seqid_chr,a.invoiceno_vchr,
       a.recorddate_dat,
       a.opremp_chr,
       a.status_int,
       a.seqid_chr,
       a.balanceemp_chr,
       a.paytypeid_chr,
       a.acctsum_mny,
       a.sbsum_mny,
       a.totalsum_mny,
       a.paytype_int,
       f.internalflag_int,
       b.itemcatid_chr,
       b.tolfee_mny,
       e.groupid_chr,
       e.groupname_chr,a.totaldiffcost_mny
  from t_opr_outpatientrecipeinv a,
       t_opr_outpatientrecipesumde b,
       t_bse_patientpaytype f,
       (select c.typeid_chr, d.groupid_chr, d.groupname_chr
          from t_aid_rpt_gop_rla c, t_aid_rpt_gop_def d
         where d.groupid_chr = c.groupid_chr
           and d.rptid_chr = c.rptid_chr
           and d.rptid_chr = ?) e
 where a.invoiceno_vchr = b.invoiceno_vchr
   and a.seqid_chr = b.seqid_chr
   and b.itemcatid_chr = e.typeid_chr(+)
   and a.paytypeid_chr = f.paytypeid_chr
   and a.chargedeptid_chr=?
   and ( a.isvouchers_int < 2 or a.isvouchers_int is null)
   and a.balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
       to_date(?, 'yyyy-mm-dd hh24:mi:ss')
 order by a.invoiceno_vchr, a.seqid_chr
";
                if (m_intStatDateType == 0)
                {
                    strSQL = strSQL.Replace("balance_dat", "recorddate_dat");
                }
                try
                {
                    System.Data.IDataParameter[] paramArr = null;
                    objHRPSvc.CreateDatabaseParameter(4, out paramArr);
                    paramArr[0].Value = strRptId;
                    paramArr[1].Value = m_strBalanceDeptID;
                    paramArr[2].Value = m_strBeginTime;
                    paramArr[3].Value = m_strEndTime;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_dtCheckOutData, paramArr);

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }
            else if (m_strCheckManID.Trim() != "1000" && m_strBalanceDeptID.Trim() == "1000")
            {

                if (m_strCheckManID.Trim() == "2000")
                {
                    strSQL = @"select a.seqid_chr,  a.invoiceno_vchr, a.recorddate_dat, a.opremp_chr, a.status_int,
         a.seqid_chr, a.balanceemp_chr, a.paytypeid_chr, a.acctsum_mny,
         a.sbsum_mny, a.totalsum_mny, a.paytype_int, f.internalflag_int,
         b.itemcatid_chr, b.tolfee_mny, e.groupid_chr, e.groupname_chr,a.totaldiffcost_mny
    from t_opr_outpatientrecipeinv a,
         t_opr_outpatientrecipesumde b,
         t_bse_patientpaytype f,
         (select c.typeid_chr, d.groupid_chr, d.groupname_chr
            from t_aid_rpt_gop_rla c, t_aid_rpt_gop_def d
           where d.groupid_chr = c.groupid_chr
             and d.rptid_chr = c.rptid_chr
             and d.rptid_chr = ?) e
   where a.invoiceno_vchr = b.invoiceno_vchr
     and a.seqid_chr = b.seqid_chr
     and b.itemcatid_chr = e.typeid_chr(+)
     and a.paytypeid_chr = f.paytypeid_chr
     [condition]
     and (a.isvouchers_int < 2 or a.isvouchers_int is null)
     and a.balance_dat between to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                           and to_date (?, 'yyyy-mm-dd hh24:mi:ss')
order by a.invoiceno_vchr, a.seqid_chr";
                    if (m_intStatDateType == 0)
                    {
                        strSQL = strSQL.Replace("balance_dat", "recorddate_dat");
                    }

                    strSQL = strSQL.Replace("[condition]", SubStr);

                    try
                    {
                        System.Data.IDataParameter[] paramArr = null;
                        objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                        paramArr[0].Value = strRptId;
                        paramArr[1].Value = m_strBeginTime;
                        paramArr[2].Value = m_strEndTime;
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_dtCheckOutData, paramArr);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                }
                else
                {
                    strSQL = @"select  a.seqid_chr, a.invoiceno_vchr, a.recorddate_dat, a.opremp_chr, a.status_int,
             a.seqid_chr, a.balanceemp_chr, a.paytypeid_chr, a.acctsum_mny,
             a.sbsum_mny, a.totalsum_mny, a.paytype_int, f.internalflag_int,
             b.itemcatid_chr, b.tolfee_mny, e.groupid_chr, e.groupname_chr,a.totaldiffcost_mny
        from t_opr_outpatientrecipeinv a,
             t_opr_outpatientrecipesumde b,
             t_bse_patientpaytype f,
             (select c.typeid_chr, d.groupid_chr, d.groupname_chr
                from t_aid_rpt_gop_rla c, t_aid_rpt_gop_def d
               where d.groupid_chr = c.groupid_chr
                 and d.rptid_chr = c.rptid_chr
                 and d.rptid_chr = ?) e
       where a.invoiceno_vchr = b.invoiceno_vchr
         and a.seqid_chr = b.seqid_chr
         and b.itemcatid_chr = e.typeid_chr(+)
         and a.paytypeid_chr = f.paytypeid_chr
         and a.balanceemp_chr=?
         and ( a.isvouchers_int < 2 or a.isvouchers_int is null)
         and a.balance_dat between to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                               and to_date (?, 'yyyy-mm-dd hh24:mi:ss')
    order by a.invoiceno_vchr, a.seqid_chr";
                    if (m_intStatDateType == 0)
                    {
                        strSQL = strSQL.Replace("balance_dat", "recorddate_dat");
                    }

                    try
                    {
                        System.Data.IDataParameter[] paramArr = null;
                        objHRPSvc.CreateDatabaseParameter(4, out paramArr);
                        paramArr[0].Value = strRptId;
                        paramArr[1].Value = m_strCheckManID;
                        paramArr[2].Value = m_strBeginTime;
                        paramArr[3].Value = m_strEndTime;
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_dtCheckOutData, paramArr);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                }

            }
            else
            {

                if (m_strCheckManID.Trim() == "2000")
                {
                    strSQL = @"select a.seqid_chr,a.invoiceno_vchr,a.recorddate_dat,a.opremp_chr,a.status_int,
        a.seqid_chr,a.balanceemp_chr,a.paytypeid_chr,a.acctsum_mny, a.sbsum_mny,
         a.totalsum_mny,a.paytype_int, f.internalflag_int,
        b.itemcatid_chr, b.tolfee_mny, e.groupid_chr, e.groupname_chr,a.totaldiffcost_mny
          from t_opr_outpatientrecipeinv a,
               t_opr_outpatientrecipesumde b,
               t_bse_patientpaytype f,
               (select c.typeid_chr, d.groupid_chr, d.groupname_chr
                  from t_aid_rpt_gop_rla c, t_aid_rpt_gop_def d
                 where d.groupid_chr = c.groupid_chr
                   and d.rptid_chr = c.rptid_chr
                   and d.rptid_chr = ?) e
         where a.invoiceno_vchr = b.invoiceno_vchr
           and a.seqid_chr = b.seqid_chr
           and b.itemcatid_chr = e.typeid_chr(+)
           and a.paytypeid_chr = f.paytypeid_chr
           and a.chargedeptid_chr=?
           [condition]
           and ( a.isvouchers_int < 2 or a.isvouchers_int is null)
           and a.balance_dat between
               to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
               to_date(?, 'yyyy-mm-dd hh24:mi:ss')
         order by a.invoiceno_vchr, a.seqid_chr";

                    if (m_intStatDateType == 0)
                    {
                        strSQL = strSQL.Replace("balance_dat", "recorddate_dat");
                    }


                    strSQL = strSQL.Replace("[condition]", SubStr);

                    try
                    {
                        System.Data.IDataParameter[] paramArr = null;
                        objHRPSvc.CreateDatabaseParameter(4, out paramArr);
                        paramArr[0].Value = strRptId;
                        paramArr[1].Value = m_strBalanceDeptID;
                        paramArr[2].Value = m_strBeginTime;
                        paramArr[3].Value = m_strEndTime;
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_dtCheckOutData, paramArr);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                }
                else
                {
                    strSQL = @"select a.seqid_chr,a.invoiceno_vchr,a.recorddate_dat,a.opremp_chr,a.status_int,
        a.seqid_chr,a.balanceemp_chr,a.paytypeid_chr,a.acctsum_mny, a.sbsum_mny,
         a.totalsum_mny,a.paytype_int, f.internalflag_int,
        b.itemcatid_chr, b.tolfee_mny, e.groupid_chr, e.groupname_chr,a.totaldiffcost_mny
          from t_opr_outpatientrecipeinv a,
               t_opr_outpatientrecipesumde b,
               t_bse_patientpaytype f,
               (select c.typeid_chr, d.groupid_chr, d.groupname_chr
                  from t_aid_rpt_gop_rla c, t_aid_rpt_gop_def d
                 where d.groupid_chr = c.groupid_chr
                   and d.rptid_chr = c.rptid_chr
                   and d.rptid_chr = ?) e
         where a.invoiceno_vchr = b.invoiceno_vchr
           and a.seqid_chr = b.seqid_chr
           and b.itemcatid_chr = e.typeid_chr(+)
           and a.paytypeid_chr = f.paytypeid_chr
           and a.balanceemp_chr = ?
           and a.chargedeptid_chr=?
           and ( a.isvouchers_int < 2 or a.isvouchers_int is null)
           and a.balance_dat between
               to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
               to_date(?, 'yyyy-mm-dd hh24:mi:ss')
         order by a.invoiceno_vchr, a.seqid_chr";
                    if (m_intStatDateType == 0)
                    {
                        strSQL = strSQL.Replace("balance_dat", "recorddate_dat");
                    }

                    try
                    {
                        System.Data.IDataParameter[] paramArr = null;
                        objHRPSvc.CreateDatabaseParameter(5, out paramArr);
                        paramArr[0].Value = strRptId;
                        paramArr[1].Value = m_strCheckManID;
                        paramArr[2].Value = m_strBalanceDeptID;
                        paramArr[3].Value = m_strBeginTime;
                        paramArr[4].Value = m_strEndTime;
                        lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref m_dtCheckOutData, paramArr);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                }


            }
            if (m_dtCheckOutData != null && m_dtCheckOutData.Rows.Count > 0)
            {
                DataView dv = m_dtCheckOutData.DefaultView;
                dtTemp = dv.ToTable(true, new string[] { "seqid_chr", "invoiceno_vchr", "totaldiffcost_mny" });
                // dtTemp = dtSource.DefaultView.ToTable(true, new string[] { "invoiceno_vchr", "totaldiffcost_mny" });
                //                DataTable dtSSS = new DataTable();
                //                DataSet ds = this.SplitDataTable(dtSourceTwo, 900);
                //                foreach (DataTable dtTT in ds.Tables)
                //                {
                //                    string strSql = @"select sum( a.totaldiffcost_mny) diffPriceSum
                //                                      from t_opr_outpatientrecipeinv a
                //                                     where a.invoiceno_vchr in( [strTemp])";
                //                    string strTemp = string.Empty;
                //                    string[] arr = new string[dtTT.Rows.Count];
                //                    for (int i = 0; i < dtTT.Rows.Count; i++)
                //                    {
                //                        arr[i] = dtTT.Rows[i]["invoiceno_vchr"].ToString();
                //                    }
                //                    dtSSS = new DataTable();
                //                    strTemp = "'" + string.Join("','", arr) + "'";
                //                    strSql = strSql.Replace("[strTemp]", strTemp);
                //                    objHRPSvc.lngGetDataTableWithoutParameters(strSql, ref dtSSS);
                //                    if (dtSSS.Rows.Count > 0)
                //                    {
                //                        dtTemp.Merge(dtSSS);
                //                    }
                //                }
            }
            return lngRes;
        }

        #endregion

        #region 获得所有结帐员数据
        [AutoComplete]
        public long m_lngGetCheckMan(out DataTable dtEmpAll, string strINTERNALFLAG)
        {
            dtEmpAll = null;
            long lngRes = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            if (strINTERNALFLAG == "-1")
            {
                strSQL = @"select distinct a.BALANCEEMP_CHR,b.LASTNAME_VCHR from t_opr_outpatientrecipeinv a,t_bse_employee b where BALANCEFLAG_INT=1 and a.BALANCEEMP_CHR=b.EMPID_CHR ";
            }
            else
            {
                strSQL = @"select distinct a.BALANCEEMP_CHR,b.LASTNAME_VCHR from t_opr_outpatientrecipeinv a,t_bse_employee b where BALANCEFLAG_INT=1  and a.BALANCEEMP_CHR=b.EMPID_CHR and a.INTERNALFLAG_INT=" + strINTERNALFLAG;
            }
            try
            {

                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtEmpAll);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #region 获取收费员所在部门
        [AutoComplete]
        public long m_lngGetRegdept(out DataTable dtdept, string strEmpId)
        {
            string strSQL = "";
            long lngRes = 0;
            dtdept = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;

                strSQL = @"select c.deptid_chr, c.deptname_vchr  from t_bse_employee a, t_bse_deptemp b, t_bse_deptdesc c "
                  + " where a.empid_chr = b.empid_chr  and b.deptid_chr = c.deptid_chr  and a.empid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out paramArr);
                paramArr[0].Value = strEmpId;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtdept, paramArr);
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
        /// <summary>
        /// 获取所有收款员
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtEmp"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetAllCheckMan(out DataTable dtEmp)
        {
            dtEmp = null;
            long lngRes = 0;
            lngRes = 0;
            string strSQL = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            strSQL = @"select distinct(b.empno_chr), b.lastname_vchr, a.balanceemp_chr
  from t_opr_outpatientrecipeinv a, t_bse_employee b
 where a.balanceemp_chr = b.empid_chr order by b.empno_chr";
            try
            {
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dtEmp);
                if (lngRes > 0 && dtEmp.Rows.Count > 0)
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add("工 号", typeof(System.String));
                    dtTemp.Columns.Add("姓      名", typeof(System.String));
                    dtTemp.Columns.Add("员工ID", typeof(System.String));
                    dtTemp.BeginLoadData();
                    for (int i1 = 0; i1 < dtEmp.Rows.Count; i1++)
                    {
                        dtTemp.LoadDataRow(dtEmp.Rows[i1].ItemArray, true);
                    }
                    dtTemp.EndLoadData();
                    dtTemp.AcceptChanges();
                    dtEmp = dtTemp;
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

        /////////////////////////////////////////////////////////////////////////

        #region 根据操作员Id和日期查找门诊发票信息
        /// <summary>
        /// 根据操作员Id和日期查找门诊发票信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOperatorId"></param>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetInvoiceInfoByDate(string p_strOperatorId, string p_strStartDate, string p_strEndDate, string p_strBalanceDeptID, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @"select a.invoiceno_vchr,
                                   a.recorddate_dat,
                                   a.opremp_chr,
                                   a.status_int,
                                   a.seqid_chr,
                                   a.balanceemp_chr,
                                   a.paytypeid_chr,
                                   a.acctsum_mny,
                                   a.sbsum_mny,
                                   a.totalsum_mny - nvl(a.totaldiffcost_mny,0) totalsum_mny,
                                   a.paytype_int,
                                   a.opremp_chr,
                                   b.empid_chr,
                                   b.empno_chr,
                                   b.lastname_vchr
                              from t_opr_outpatientrecipeinv   a,
                                   t_bse_employee b
                             where a.balanceemp_chr = b.empid_chr
                               and a.balanceflag_int = 1
                               and (a.isvouchers_int < 2 or a.isvouchers_int is null)
                               and a.balanceemp_chr = ?
                               and a.balance_dat >= ?
                               and a.balance_dat <= ?
                               and a.balance_dat between to_date(?,'yyyy-mm-dd hh24:mi:ss')
                               and to_date(?,'yyyy-mm-dd hh24:mi:ss')
                             order by b.empno_chr,a.invoiceno_vchr";

            string strSQL1 = @"select a.invoiceno_vchr,
       a.recorddate_dat,
       a.opremp_chr,
       a.status_int,
       a.seqid_chr,
       a.balanceemp_chr,
       a.paytypeid_chr,
       a.acctsum_mny,
       a.sbsum_mny,
       a.totalsum_mny - nvl(a.totaldiffcost_mny,0) totalsum_mny,
       a.paytype_int,
       a.opremp_chr,
       b.empid_chr,
       b.empno_chr,
       b.lastname_vchr
  from t_opr_outpatientrecipeinv a, t_bse_employee b
 where a.balanceemp_chr = b.empid_chr
   and a.chargedeptid_chr=?
   and a.balanceflag_int = 1
   and (a.isvouchers_int < 2 or a.isvouchers_int is null)
   and a.balanceemp_chr = ?
   AND a.balance_dat >= ?
   AND a.balance_dat <= ?
   and a.balance_dat between to_date(?,'yyyy-mm-dd hh24:mi:ss')
   and to_date(?,'yyyy-mm-dd hh24:mi:ss')
 order by b.empno_chr, a.invoiceno_vchr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                System.Data.IDataParameter[] arrParams = null;
                if (p_strBalanceDeptID.Trim() == "1000")
                {
                    new clsHRPTableService().CreateDatabaseParameter(5, out arrParams);
                    arrParams[0].Value = p_strOperatorId;
                    arrParams[1].Value = System.DateTime.Parse(p_strStartDate);
                    arrParams[2].Value = System.DateTime.Parse(p_strEndDate);
                    arrParams[3].Value = p_strStartDate;
                    arrParams[4].Value = p_strEndDate;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
                }
                else
                {
                    new clsHRPTableService().CreateDatabaseParameter(6, out arrParams);
                    arrParams[0].Value = p_strBalanceDeptID;
                    arrParams[1].Value = p_strOperatorId;
                    arrParams[2].Value = System.DateTime.Parse(p_strStartDate);
                    arrParams[3].Value = System.DateTime.Parse(p_strEndDate);
                    arrParams[4].Value = p_strStartDate;
                    arrParams[5].Value = p_strEndDate;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL1, ref p_dtResult, arrParams);
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

        #region 根据日期查找门诊发票信息
        /// <summary>
        /// 根据日期查找门诊发票信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOperatorId"></param>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetInvoiceInfoByDate(string p_strStartDate, string p_strEndDate, string p_strBalanceDeptID, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @"select a.invoiceno_vchr,
                                   a.recorddate_dat,
                                   a.opremp_chr,
                                   a.status_int,
                                   a.seqid_chr,
                                   a.balanceemp_chr,
                                   a.paytypeid_chr,
                                   a.acctsum_mny,
                                   a.sbsum_mny,
                                   a.totalsum_mny - nvl(a.totaldiffcost_mny,0) totalsum_mny,
                                   a.paytype_int,
                                   a.opremp_chr,
                                   b.empid_chr,
                                   b.empno_chr,
                                   b.lastname_vchr
                              from t_opr_outpatientrecipeinv   a,
                                   t_bse_employee b
                             where a.balanceemp_chr = b.empid_chr
                               and a.balanceflag_int = 1
                               and (a.isvouchers_int < 2 or a.isvouchers_int is null)
                               and a.balance_dat >= ?
                               and a.balance_dat <= ?
                               and a.balance_dat between to_date(?,'yyyy-mm-dd hh24:mi:ss')
                               and to_date(?,'yyyy-mm-dd hh24:mi:ss')
                             order by b.empno_chr,a.invoiceno_vchr";

            string strSQL1 = @"select a.invoiceno_vchr,
                                   a.recorddate_dat,
                                   a.opremp_chr,
                                   a.status_int,
                                   a.seqid_chr,
                                   a.balanceemp_chr,
                                   a.paytypeid_chr,
                                   a.acctsum_mny,
                                   a.sbsum_mny,
                                   a.totalsum_mny - nvl(a.totaldiffcost_mny,0) totalsum_mny,
                                   a.paytype_int,
                                   a.opremp_chr,
                                   b.empid_chr,
                                   b.empno_chr,
                                   b.lastname_vchr
                              from t_opr_outpatientrecipeinv   a,
                                   t_bse_employee b
                             where a.balanceemp_chr = b.empid_chr
                               and a.chargedeptid_chr=?
                               and a.balanceflag_int = 1
                               and (a.isvouchers_int < 2 or a.isvouchers_int is null)
                               and a.balance_dat >= ?
                               and a.balance_dat <= ?
                               and a.balance_dat between to_date(?,'yyyy-mm-dd hh24:mi:ss')
                               and to_date(?,'yyyy-mm-dd hh24:mi:ss')
                             order by b.empno_chr,a.invoiceno_vchr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                if (p_strBalanceDeptID == "1000")
                {
                    System.Data.IDataParameter[] arrParams = null;
                    new clsHRPTableService().CreateDatabaseParameter(4, out arrParams);
                    arrParams[0].Value = System.DateTime.Parse(p_strStartDate);
                    arrParams[1].Value = System.DateTime.Parse(p_strEndDate);
                    arrParams[2].Value = p_strStartDate;
                    arrParams[3].Value = p_strEndDate;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
                }
                else
                {
                    System.Data.IDataParameter[] arrParams = null;
                    new clsHRPTableService().CreateDatabaseParameter(5, out arrParams);
                    arrParams[0].Value = p_strBalanceDeptID;
                    arrParams[1].Value = System.DateTime.Parse(p_strStartDate);
                    arrParams[2].Value = System.DateTime.Parse(p_strEndDate);
                    arrParams[3].Value = p_strStartDate;
                    arrParams[4].Value = p_strEndDate;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL1, ref p_dtResult, arrParams);
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

        #region 根据操作员Id和日期查找门诊重打发票信息
        /// <summary>
        /// 根据操作员Id和日期查找门诊重打发票信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOperatorId"></param>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetInvoiceReprintByDate(string p_strOperatorId, string p_strStartDate, string p_strEndDate, string p_strBalanceDeptID, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @"SELECT a.sourceinvono_vchr,
                                     a.repprninvono_vchr,
                                     a.printemp_chr,
                                     a.printstatus_int,
                                     b.empid_chr,
                                     b.empno_chr,
                                     b.lastname_vchr
                             FROM t_opr_invoicerepeatprint a,
                                  t_bse_employee b
                             WHERE a.printemp_chr = b.empid_chr
                               AND a.type_chr = 1
                               AND a.printemp_chr = ?
                               AND a.printdate_dat >= ?
                               AND a.printdate_dat <= ?";
            string strSQL1 = @"SELECT a.sourceinvono_vchr,
       a.repprninvono_vchr,
       a.printemp_chr,
       a.printstatus_int,
       b.empid_chr,
       b.empno_chr,
       b.lastname_vchr
  FROM t_opr_invoicerepeatprint a,
       t_bse_employee           b,
       t_opr_outpatientrecipeinv f
 WHERE a.printemp_chr = b.empid_chr
   and a.seqid_chr=f.seqid_chr
   and f.chargedeptid_chr=?
   AND a.type_chr = 1
   AND a.printemp_chr = ?
   AND a.printdate_dat >= ?
   AND a.printdate_dat <= ?
";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                System.Data.IDataParameter[] arrParams = null;
                if (p_strBalanceDeptID.Trim() == "1000")
                {
                    new clsHRPTableService().CreateDatabaseParameter(3, out arrParams);
                    arrParams[0].Value = p_strOperatorId;
                    arrParams[1].Value = System.DateTime.Parse(p_strStartDate);
                    arrParams[2].Value = System.DateTime.Parse(p_strEndDate);
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
                }
                else
                {
                    new clsHRPTableService().CreateDatabaseParameter(4, out arrParams);
                    arrParams[0].Value = p_strBalanceDeptID;
                    arrParams[1].Value = p_strOperatorId;
                    arrParams[2].Value = System.DateTime.Parse(p_strStartDate);
                    arrParams[3].Value = System.DateTime.Parse(p_strEndDate);
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL1, ref p_dtResult, arrParams);
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

        #region 根据日期查找门诊重打发票信息
        /// <summary>
        /// 根据日期查找门诊重打发票信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOperatorId"></param>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetInvoiceReprintByDate(string p_strStartDate, string p_strEndDate, string p_strBalanceDeptID, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            string strSQL = @"SELECT a.sourceinvono_vchr,
                                     a.repprninvono_vchr,
                                     a.printemp_chr,
                                     a.printstatus_int,
                                     b.empid_chr,
                                     b.empno_chr,
                                     b.lastname_vchr
                             FROM t_opr_invoicerepeatprint a,
                                  t_bse_employee b
                             WHERE a.printemp_chr = b.empid_chr
                               AND a.type_chr = 1
                               AND a.printdate_dat >= ?
                               AND a.printdate_dat <= ?";
            string strSQL1 = @"SELECT a.sourceinvono_vchr,
       a.repprninvono_vchr,
       a.printemp_chr,
       a.printstatus_int,
       b.empid_chr,
       b.empno_chr,
       b.lastname_vchr
  FROM t_opr_invoicerepeatprint a,
       t_bse_employee           b,
       t_opr_outpatientrecipeinv f
 WHERE a.printemp_chr = b.empid_chr
   and a.seqid_chr=f.seqid_chr
   and f.chargedeptid_chr=?
   AND a.type_chr = 1
   AND a.printdate_dat >= ?
   AND a.printdate_dat <= ?";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = 0;
                //lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtResult);
                System.Data.IDataParameter[] arrParams = null;
                if (p_strBalanceDeptID.Trim() == "1000")
                {
                    new clsHRPTableService().CreateDatabaseParameter(2, out arrParams);
                    arrParams[0].Value = System.DateTime.Parse(p_strStartDate);
                    arrParams[1].Value = System.DateTime.Parse(p_strEndDate);
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtResult, arrParams);
                }
                else
                {
                    new clsHRPTableService().CreateDatabaseParameter(3, out arrParams);
                    arrParams[0].Value = p_strBalanceDeptID;
                    arrParams[1].Value = System.DateTime.Parse(p_strStartDate);
                    arrParams[2].Value = System.DateTime.Parse(p_strEndDate);
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL1, ref p_dtResult, arrParams);
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

        #region 按接诊医生统计门诊挂号费及诊金报表
        /// <summary>
        /// 1.按接诊医生统计门诊挂号费及诊金报表
        /// </summary>
        /// <param name="strBeginDat"></param>
        /// <param name="strEndDat"></param>
        /// <param name="m_dtbReport"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSelectDoctorEarning(string strBeginDat, string strEndDat, out DataTable m_dtbReport)
        {
            long lngRes = -1;
            m_dtbReport = new DataTable();

            string strSql = @"
                        select   t4.empno_chr, t4.lastname_vchr,
                                 sum (t2.payment_mny * t2.discount_dec) as totalghmny,
                                 sum (t3.payment_mny * t3.discount_dec) as totalzcmny
                            from (select c.doctorid_chr, a.registerid_chr
                                    from t_opr_outpatientrecipe a,
                                         t_opr_reciperelation b,
                                         t_opr_outpatientrecipeinv c
                                   where a.outpatrecipeid_chr = b.outpatrecipeid_chr
                                     and b.seqid = c.outpatrecipeid_chr
                                     and a.recipeflag_int = 1
                                     and (a.pstauts_int = 2 or a.pstauts_int = 3)
                                     and a.registerid_chr is not null
                                     and c.balance_dat > ?
                                     and c.balance_dat < ?
                                     and a.recorddate_dat =
                                            (select min (recorddate_dat)
                                               from t_opr_outpatientrecipe
                                              where registerid_chr = a.registerid_chr
                                                and recipeflag_int = 1
                                                and (pstauts_int = 2 or pstauts_int = 3))) t1,
                                         (select m.registerid_chr,m.chargeid_chr,m.payment_mny,m.discount_dec from t_opr_patientregdetail m where chargeid_chr = '001') t2,
                                         (select n.registerid_chr,n.chargeid_chr,n.payment_mny,n.discount_dec from t_opr_patientregdetail n where chargeid_chr = '002') t3,
                                 t_bse_employee t4
                           where t1.registerid_chr = t2.registerid_chr(+)
                             and t1.registerid_chr = t3.registerid_chr(+)
                             and t1.doctorid_chr = t4.empid_chr(+)
                        group by t4.empno_chr, t4.lastname_vchr
                    ";
            //            string strSql = @"select   t4.empno_chr, t4.lastname_vchr,
            //                                 sum (t2.payment_mny * t2.discount_dec) as totalghmny,
            //                                 sum (t3.payment_mny * t3.discount_dec) as totalzcmny
            //                            from (select c.doctorid_chr, a.registerid_chr
            //                                    from t_opr_outpatientrecipe a,
            //                                         t_opr_reciperelation b,
            //                                         t_opr_charge c
            //                                   where a.outpatrecipeid_chr = b.outpatrecipeid_chr
            //                                     and c.chargeno_chr = b.chargeno_chr
            //                                     and a.recipeflag_int = 1
            //                                     and (a.pstauts_int = 2 or a.pstauts_int = 3)
            //                                     and a.registerid_chr is not null
            //                                     and c.recdate_dat > ?
            //                                     and c.recdate_dat < ?
            //                                     and a.recorddate_dat =
            //                                            (select min (recorddate_dat)
            //                                               from t_opr_outpatientrecipe
            //                                              where registerid_chr = a.registerid_chr
            //                                                and recipeflag_int = 1
            //                                                and (pstauts_int = 2 or pstauts_int = 3))) t1,
            //                                         (select m.registerid_chr,m.chargeid_chr,m.payment_mny,m.discount_dec from t_opr_patientregdetail m where chargeid_chr = '001') t2,
            //                                         (select n.registerid_chr,n.chargeid_chr,n.payment_mny,n.discount_dec from t_opr_patientregdetail n where chargeid_chr = '002') t3,
            //                                 t_bse_employee t4
            //                           where t1.registerid_chr = t2.registerid_chr(+)
            //                             and t1.registerid_chr = t3.registerid_chr(+)
            //                             and t1.doctorid_chr = t4.empid_chr(+)
            //                        group by t4.empno_chr, t4.lastname_vchr";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();

                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(2, out arrParams);
                int n = -1;
                arrParams[++n].Value = Convert.ToDateTime(strBeginDat + " 00:00:00");
                arrParams[++n].Value = Convert.ToDateTime(strEndDat + " 23:59:59");

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSql, ref m_dtbReport, arrParams);

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

        #region 取核算分类
        [AutoComplete]
        public long m_lngGetTypeID(string p_strRptID, string p_strGroupID, out DataTable p_dtbTypeID)
        {
            long lngRes = -1;

            p_dtbTypeID = new DataTable();
            string strSQL = @"select b.typeid_chr
                        from t_aid_rpt_gop_def a, t_aid_rpt_gop_rla b
                        where a.groupid_chr = b.groupid_chr(+)
                            and a.rptid_chr = b.rptid_chr
                            and a.rptid_chr = ?
                            and a.groupid_chr = ?
                        order by b.typeid_chr asc";
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            try
            {
                IDataParameter[] objDPArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out objDPArr);
                objDPArr[0].Value = p_strRptID;
                objDPArr[1].Value = p_strGroupID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbTypeID, objDPArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);

            }
            objHRPSvc.Dispose();
            return lngRes;
        }
        #endregion

        #region 医生挂号费及诊金汇总表(新)
        /// <summary>
        /// 3.医生挂号费及诊金汇总表
        /// </summary>
        /// <param name="strBeginDat"></param>
        /// <param name="strEndDat"></param>
        /// <param name="m_strTypeOfGh"></param>
        /// <param name="m_strTypeOfZc"></param>
        /// <param name="m_dtbReport"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDoctorEarningCollect(string strBeginDat, string strEndDat, string[] p_strTypeIDArr1, string[] p_strTypeIDArr2, out DataTable m_dtbReport)
        {
            long lngRes = -1;
            m_dtbReport = new DataTable();
            StringBuilder sbdSql = new StringBuilder(
                    @"select t4.empno_chr,
                             t4.lastname_vchr,
                             t7.totalghmny,
                             t7.totalzcmny,
                             t5.totaloghmny,
                             t6.totalozcmny
                     from (select t1.doctorid_chr,
                       sum(t2.payment_mny * t2.discount_dec) as totalghmny,
                       sum(t3.payment_mny * t3.discount_dec) as totalzcmny
                     from (select c.doctorid_chr, a.registerid_chr
                     from t_opr_outpatientrecipe    a,
                          t_opr_reciperelation      b,
                          t_opr_outpatientrecipeinv c
                     where a.outpatrecipeid_chr = b.outpatrecipeid_chr
                        and b.seqid = c.outpatrecipeid_chr
                        and a.recipeflag_int = 1
                        and (a.pstauts_int = 2 or a.pstauts_int = 3)
                        and a.registerid_chr is not null
                        and c.balance_dat >= ?
                        and c.balance_dat <= ?
                        and a.recorddate_dat =
                       (select min(recorddate_dat)
                         from t_opr_outpatientrecipe
                         where registerid_chr = a.registerid_chr
                           and recipeflag_int = 1
                           and (pstauts_int = 2 or pstauts_int = 3))) t1,
                      (select m.registerid_chr,m.chargeid_chr,m.payment_mny,m.discount_dec from t_opr_patientregdetail m where chargeid_chr = '001') t2,
         (select n.registerid_chr,n.chargeid_chr,n.payment_mny,n.discount_dec from t_opr_patientregdetail n where chargeid_chr = '002') t3
                    where t1.registerid_chr = t2.registerid_chr(+)
                        and t1.registerid_chr = t3.registerid_chr(+)
                        group by t1.doctorid_chr) t7,
                        t_bse_employee t4,

                    (select c1.doctorid_chr, sum(e1.tolfee_mny) as totaloghmny
                    from t_opr_outpatientrecipeinv c1, t_opr_outpatientrecipesumde e1
                    where c1.seqid_chr = e1.seqid_chr 
                     and (e1.itemcatid_chr = ?");
            //            StringBuilder sbdSql = new StringBuilder(@"select t4.empno_chr, t4.lastname_vchr, t7.totalghmny, t7.totalzcmny,
            //       t5.totaloghmny, t6.totalozcmny
            //  from (select   t1.diagdr_chr,
            //                 sum (t2.payment_mny * t2.discount_dec) as totalghmny,
            //                 sum (t3.payment_mny * t3.discount_dec) as totalzcmny
            //            from (select a.diagdr_chr, a.registerid_chr
            //                    from t_opr_outpatientrecipe a,
            //                         t_opr_reciperelation b,
            //                         t_opr_charge c
            //                   where a.outpatrecipeid_chr = b.outpatrecipeid_chr
            //                     and c.chargeno_chr = b.chargeno_chr
            //                     and a.recipeflag_int = 1
            //                     and (a.pstauts_int = 2 or a.pstauts_int = 3)
            //                     and a.registerid_chr is not null
            //                     and c.recdate_dat >=?
            //                     and c.recdate_dat <=?
            //                     and a.recorddate_dat =
            //                            (select min (recorddate_dat)
            //                               from t_opr_outpatientrecipe
            //                              where registerid_chr = a.registerid_chr
            //                                and recipeflag_int = 1
            //                                and (pstauts_int = 2 or pstauts_int = 3))) t1,
            //                 (select m.registerid_chr, m.chargeid_chr, m.payment_mny,
            //                         m.discount_dec
            //                    from t_opr_patientregdetail m
            //                   where chargeid_chr = '001') t2,
            //                 (select n.registerid_chr, n.chargeid_chr, n.payment_mny,
            //                         n.discount_dec
            //                    from t_opr_patientregdetail n
            //                   where chargeid_chr = '002') t3
            //           where t1.registerid_chr = t2.registerid_chr(+)
            //                 and t1.registerid_chr = t3.registerid_chr(+)
            //        group by t1.diagdr_chr) t7,
            //       t_bse_employee t4,
            //       (select   f1.diagdr_chr, sum (e1.tolfee_mny) as totaloghmny
            //            from t_opr_charge c1,
            //                 t_opr_reciperelation d1,
            //                 t_opr_outpatientrecipe f1,
            //                 t_opr_outpatientrecipesumde e1
            //           where c1.chargeno_chr = d1.chargeno_chr
            //             and f1.outpatrecipeid_chr = d1.outpatrecipeid_chr
            //             and c1.chargeno_chr = e1.chargeno_chr
            //             and (e1.itemcatid_chr = ?");
            for (int i1 = 1; i1 < p_strTypeIDArr1.Length; i1++)
            {
                sbdSql.Append(@" or e1.itemcatid_chr = ?");
            }

            sbdSql.Append(@") and c1.balance_dat >= ?
                         and c1.balance_dat <= ?
                    group by c1.doctorid_chr) t5,
                    (select c2.doctorid_chr, sum(e2.tolfee_mny) as totalozcmny
                    from t_opr_outpatientrecipeinv c2, t_opr_outpatientrecipesumde e2
                    where c2.seqid_chr = e2.seqid_chr
                      and (e2.itemcatid_chr = ?");
            //            sbdSql.Append(@") and c1.recdate_dat >= ?
            //                         and c1.recdate_dat <= ?
            //                    group by f1.diagdr_chr) t5,
            //                    (SELECT   f.diagdr_chr, SUM (e2.tolfee_mny) AS totalozcmny
            //            FROM t_opr_charge c2,
            //                 t_opr_reciperelation d,
            //                 t_opr_outpatientrecipe f,
            //                 t_opr_outpatientrecipesumde e2
            //           WHERE c2.chargeno_chr = d.chargeno_chr
            //             AND f.outpatrecipeid_chr = d.outpatrecipeid_chr
            //             AND c2.chargeno_chr = e2.chargeno_chr
            //             AND (e2.itemcatid_chr = ?");
            for (int i1 = 1; i1 < p_strTypeIDArr2.Length; i1++)
            {
                sbdSql.Append(@" or e2.itemcatid_chr = ?");
            }

            sbdSql.Append(@") and c2.balance_dat >= ?
                         and c2.balance_dat <= ?
                    group by c2.doctorid_chr) t6
                    where t4.empid_chr = t7.doctorid_chr(+)
                      and t4.empid_chr = t5.doctorid_chr(+)
                      and t4.empid_chr = t6.doctorid_chr(+)");
            //       sbdSql.Append(@") and c2.recdate_dat >= ?
            //                         and c2.recdate_dat <= ?
            //                    group by f.diagdr_chr) t6
            //                    where t4.empid_chr = t7.diagdr_chr(+)
            //                      and t4.empid_chr = t5.diagdr_chr(+)
            //                      and t4.empid_chr = t6.diagdr_chr(+)"); 

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();


                System.Data.IDataParameter[] arrParams = null;
                objHRPSvc.CreateDatabaseParameter(6 + p_strTypeIDArr1.Length + p_strTypeIDArr2.Length, out arrParams);
                int n = -1;

                arrParams[0].Value = Convert.ToDateTime(strBeginDat + " 00:00:00");
                arrParams[0].DbType = DbType.Date;
                arrParams[1].Value = Convert.ToDateTime(strEndDat + " 23:59:59");
                arrParams[1].DbType = DbType.Date;
                int intIndex = 2;
                for (int i1 = 0; i1 < p_strTypeIDArr1.Length; i1++)
                {
                    arrParams[i1 + intIndex].Value = p_strTypeIDArr1[i1];

                }
                intIndex += p_strTypeIDArr1.Length
                    ;
                arrParams[intIndex].Value = Convert.ToDateTime(strBeginDat + " 00:00:00");
                arrParams[intIndex].DbType = DbType.Date;
                intIndex++;
                arrParams[intIndex].Value = Convert.ToDateTime(strEndDat + " 23:59:59");
                arrParams[intIndex].DbType = DbType.Date;
                intIndex++;

                for (int i1 = 0; i1 < p_strTypeIDArr2.Length; i1++)
                {
                    arrParams[i1 + intIndex].Value = p_strTypeIDArr2[i1];

                }

                intIndex += p_strTypeIDArr2.Length;

                arrParams[intIndex].Value = Convert.ToDateTime(strBeginDat + " 00:00:00");
                arrParams[intIndex].DbType = DbType.Date;
                intIndex++;
                arrParams[intIndex].Value = Convert.ToDateTime(strEndDat + " 23:59:59");
                arrParams[intIndex].DbType = DbType.Date;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(sbdSql.ToString(), ref m_dtbReport, arrParams);

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

        #region 收费处挂号发票月统计报表(新)
        [AutoComplete]
        public long m_lngGetRegisterStatData(string p_strOperatorId, string p_strStartDate, string p_strEndDate, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();

            String strSQL = string.Empty;
            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] objParamerArr = null;

            try
            {
                if (p_strOperatorId.Trim().Length > 0)
                {
                    strSQL = @"
                        select a.registerid_chr,
                               a.invno_chr,
                               a.flag_int,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '001') as rpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '002') as dpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '003') as gpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '004') as cpayment_mny,

                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '001') as rcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '002') as dcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '003') as gcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '004') as ccharge,
                               a.registeremp_chr as empid_chr,
                               b.lastname_vchr
                        from t_opr_patientregister    a,
                             t_bse_employee           b
                        where a.registeremp_chr = b.empid_chr(+)
                          and a.balanceemp_chr is not null
                          and a.returnemp_chr is null 
                          and a.registeremp_chr = ?    
                          and a.balance_dat >= ?
                          and a.balance_dat <= ?

                     union all

                        select a.registerid_chr,
                               a.invno_chr,
                               a.flag_int,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '001') as rpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '002') as dpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '003') as gpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '004') as cpayment_mny,

                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '001') as rcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '002') as dcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '003') as gcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '004') as ccharge,
                               a.returnemp_chr as empid_chr,
                               b.lastname_vchr
                        from t_opr_patientregister    a,
                             t_bse_employee           b
                        where a.returnemp_chr = b.empid_chr(+)
                          and a.balanceemp_chr is not null
                          and a.returnemp_chr is not null    
                          and a.returnemp_chr = ?
                          and a.balance_dat >= ?
                          and a.balance_dat <= ?
                    order by invno_chr asc";

                    objHRPSvc.CreateDatabaseParameter(6, out objParamerArr);

                    objParamerArr[0].Value = p_strOperatorId;
                    objParamerArr[1].DbType = DbType.Date;
                    objParamerArr[1].Value = Convert.ToDateTime(p_strStartDate);
                    objParamerArr[2].DbType = DbType.Date;
                    objParamerArr[2].Value = Convert.ToDateTime(p_strEndDate);

                    objParamerArr[3].Value = p_strOperatorId;
                    objParamerArr[4].DbType = DbType.Date;
                    objParamerArr[4].Value = Convert.ToDateTime(p_strStartDate);
                    objParamerArr[5].DbType = DbType.Date;
                    objParamerArr[5].Value = Convert.ToDateTime(p_strEndDate);


                }
                else
                {
                    strSQL = @"
                        select a.registerid_chr,
                               a.invno_chr,
                               a.flag_int,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '001') as rpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '002') as dpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '003') as gpayment_mny,
                               (select payment_mny 
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '004') as cpayment_mny,

                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '001') as rcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '002') as dcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '003') as gcharge,
                               (select payment_mny * discount_dec
                                from t_opr_patientregdetail d
                                where d.registerid_chr = a.registerid_chr
                                  and d.chargeid_chr = '004') as ccharge,
                               a.registeremp_chr as empid_chr,
                               b.lastname_vchr
                       from t_opr_patientregister    a,
                            t_bse_employee           b
                       where a.registeremp_chr = b.empid_chr(+)
                         and a.balanceemp_chr is not null
                         and a.returnemp_chr is null     
                         and a.balance_dat >= ?
                         and a.balance_dat <= ?

                     union all

                      select a.registerid_chr,
                             a.invno_chr,
                             a.flag_int,
                             (select payment_mny 
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '001') as rpayment_mny,
                             (select payment_mny 
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '002') as dpayment_mny,
                             (select payment_mny 
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '003') as gpayment_mny,
                             (select payment_mny 
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '004') as cpayment_mny,

                             (select payment_mny * discount_dec
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '001') as rcharge,
                             (select payment_mny * discount_dec
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '002') as dcharge,
                             (select payment_mny * discount_dec
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '003') as gcharge,
                             (select payment_mny * discount_dec
                              from t_opr_patientregdetail d
                              where d.registerid_chr = a.registerid_chr
                                and d.chargeid_chr = '004') as ccharge,
                             a.returnemp_chr as empid_chr,
                             b.lastname_vchr
                        from t_opr_patientregister    a,
                             t_bse_employee           b
                        where a.returnemp_chr = b.empid_chr(+)
                          and a.balanceemp_chr is not null
                          and a.returnemp_chr is not null     
                          and a.balance_dat >= ?
                          and a.balance_dat <= ?
                        order by invno_chr asc";

                    objHRPSvc.CreateDatabaseParameter(4, out objParamerArr);

                    objParamerArr[0].DbType = DbType.Date;
                    objParamerArr[0].Value = Convert.ToDateTime(p_strStartDate);
                    objParamerArr[1].DbType = DbType.Date;
                    objParamerArr[1].Value = Convert.ToDateTime(p_strEndDate);
                    objParamerArr[2].DbType = DbType.Date;
                    objParamerArr[2].Value = Convert.ToDateTime(p_strStartDate);
                    objParamerArr[3].DbType = DbType.Date;
                    objParamerArr[3].Value = Convert.ToDateTime(p_strEndDate);
                }


                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, objParamerArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            objHRPSvc.Dispose();
            return lngRes;
        }

        #region 根据操作员Id和日期查找门诊重打挂号发票信息
        /// <summary>
        /// 根据操作员Id和日期查找门诊重打挂号发票信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strOperatorId"></param>
        /// <param name="p_strStartDate"></param>
        /// <param name="p_strEndDate"></param>
        /// <param name="p_dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetRegisterBillReprintByDate(string p_strOperatorId, string p_strStartDate, string p_strEndDate, out DataTable p_dtResult)
        {
            long lngRes = 0;
            p_dtResult = new DataTable();
            StringBuilder strSQL = new StringBuilder(@"
                            select a.sourceinvono_vchr,
                                   a.repprninvono_vchr,
                                   a.printemp_chr
                            from t_opr_invoicerepeatprint a,t_opr_patientregister b
                            where a.sourceinvono_vchr = b.invno_chr
                              and a.type_chr = 2
                              and a.printstatus_int = 0
                              and b.balanceemp_chr is not null
                              and b.returnemp_chr is null      
                              and b.balance_dat >= ?
                              and b.balance_dat <= ?");

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] objParamerArr = null;
            IDataParameter[] tmp_objParamerArr = null;
            objHRPSvc.CreateDatabaseParameter(3, out tmp_objParamerArr);
            int m_intParamCount = 2;
            tmp_objParamerArr[0].DbType = DbType.Date;
            tmp_objParamerArr[0].Value = Convert.ToDateTime(p_strStartDate);
            tmp_objParamerArr[1].DbType = DbType.Date;
            tmp_objParamerArr[1].Value = Convert.ToDateTime(p_strEndDate);

            try
            {
                if (p_strOperatorId.Trim().Length > 0)
                {
                    strSQL.Append(@" and a.printemp_chr = ?");
                    ++m_intParamCount;
                    tmp_objParamerArr[m_intParamCount - 1].Value = p_strOperatorId;
                }

                objHRPSvc.CreateDatabaseParameter(m_intParamCount, out objParamerArr);
                for (int i1 = 0; i1 < m_intParamCount; i1++)
                {
                    objParamerArr[i1].Value = tmp_objParamerArr[i1].Value;
                    objParamerArr[i1].DbType = tmp_objParamerArr[i1].DbType;
                }


                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL.ToString(), ref p_dtResult, objParamerArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            objHRPSvc.Dispose();
            return lngRes;
        }
        #endregion //根据操作员Id和日期查找门诊重打发票信息

        #endregion //收费处挂号发票月统计报表(新)

        #region 获取所有医保对应的患者类型
        /// <summary>
        /// 获取所有医保对应的患者类型
        /// </summary>
        /// <param name="arrPatientType"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetYBPatientPayType(out clsPatientType_VO[] arrPatientType)
        {
            long lngRes = 0;
            DataTable table = null;
            arrPatientType = null;

            string sql = @"
                               select paytypeid_chr, 
                                      paytypename_vchr
                                 from t_bse_patientpaytype
                                where (internalflag_int = 1 or internalflag_int = 2)
                          ";
            try
            {
                clsHRPTableService hrpService = new clsHRPTableService();
                lngRes = hrpService.lngGetDataTableWithoutParameters(sql, ref table);

                if (IsTableNull(table))
                {
                    arrPatientType = null;
                }
                else
                {
                    //arrPatientType = ConstructArrayVO(table);
                    int intRow = table.Rows.Count;
                    arrPatientType = new clsPatientType_VO[intRow];

                    DataRow dtRow = null;
                    for (int i1 = 0; i1 < intRow; i1++)
                    {
                        dtRow = table.Rows[i1];
                        arrPatientType[i1] = new clsPatientType_VO();
                        arrPatientType[i1].m_strPayTypeID = dtRow["paytypeid_chr"].ToString();
                        arrPatientType[i1].m_strPayTypeName = dtRow["paytypename_vchr"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 获取患者类型
        /// </summary>
        /// <param name="patientPayTypeId"></param>
        /// <param name="patientType"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientPayType(string patientPayTypeId, out clsPatientType_VO patientType)
        {
            long lngRes = 0;
            DataTable table = null;
            patientType = null;

            string sql = @" select paytypeid_chr, paytypename_vchr
                              from t_bse_patientpaytype
                             where (internalflag_int = 1 or internalflag_int = 2)
                               and paytypeid_chr = ?
                          ";
            try
            {
                clsHRPTableService hrpService = new clsHRPTableService();
                IDataParameter[] objParams = null;
                hrpService.CreateDatabaseParameter(1, out objParams);
                objParams[0].Value = patientPayTypeId;

                lngRes = hrpService.lngGetDataTableWithParameters(sql, ref table, objParams);

                if (IsTableNull(table))
                {
                    patientType = null;
                }
                else
                {
                    patientType = ConstructVO(table.Rows[0]);
                }
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        private clsPatientType_VO ConstructVO(DataRow dtRow)
        {
            clsPatientType_VO patientType = new clsPatientType_VO();
            patientType.m_strPayTypeID = dtRow["paytypeid_chr"].ToString();
            patientType.m_strPayTypeName = dtRow["paytypename_vchr"].ToString();

            return patientType;
        }

        private clsPatientType_VO[] ConstructArrayVO(DataTable table)
        {
            if (IsTableNull(table))
            {
                return new clsPatientType_VO[0];
            }

            int rowCount = table.Rows.Count;
            clsPatientType_VO[] arrPatientType = new clsPatientType_VO[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                arrPatientType[i] = ConstructVO(table.Rows[i]);
            }

            return arrPatientType;
        }
        private bool IsTableNull(DataTable table)
        {
            return table == null || table.Rows.Count == 0;
        }
        #endregion

        #region YBDefPayType

        #region ConstructVO

        private clsYBDefPayTypeVO ConstructVO2(DataRow dtRow)
        {
            clsYBDefPayTypeVO ybDefPayTypeVO = new clsYBDefPayTypeVO();
            ybDefPayTypeVO.m_strPayTypeId = dtRow["paytypeid_chr"].ToString();
            ybDefPayTypeVO.m_strPayTypeId = dtRow["jslx"].ToString();
            ybDefPayTypeVO.m_strPayTypeId = dtRow["rylb"].ToString();

            return ybDefPayTypeVO;
        }

        private clsYBDefPayTypeVO[] ConstructArrayVO2(DataTable table)
        {
            if (IsTableNull(table))
            {
                return new clsYBDefPayTypeVO[0];
            }

            int rowCount = table.Rows.Count;
            clsYBDefPayTypeVO[] arrYBDefPayType = new clsYBDefPayTypeVO[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                arrYBDefPayType[i] = ConstructVO2(table.Rows[i]);
            }

            return arrYBDefPayType;
        }

        #endregion

        #region FindAll
        /// <summary>
        /// 返回所有的关系
        /// </summary>
        /// <param name="arrYBDefPayType"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindAll(out clsYBDefPayTypeVO[] arrYBDefPayType)
        {
            long lngRes = 0;
            DataTable table = null;
            arrYBDefPayType = null;

            string sql = @"select paytypeid_chr,jslx,rylb from t_opr_bih_ybdefpaytype";
            try
            {
                clsHRPTableService hrpService = new clsHRPTableService();
                lngRes = hrpService.lngGetDataTableWithoutParameters(sql, ref table);
                arrYBDefPayType = ConstructArrayVO2(table);

            }
            catch (Exception objEx)
            {
                lngRes = 0;
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        [AutoComplete]
        public long m_lngFind(string payTypeId, out clsYBDefPayTypeVO ybDefPayType)
        {
            long lngRes = 0;
            DataTable table = null;
            ybDefPayType = null;

            string sql = @"select paytypeid_chr, jslx, rylb
                                 from t_opr_bih_ybdefpaytype
                                where paytypeid_chr = ?";
            try
            {
                clsHRPTableService hrpService = new clsHRPTableService();
                IDataParameter[] objParams = null;
                hrpService.CreateDatabaseParameter(1, out objParams);
                objParams[0].Value = payTypeId;

                lngRes = hrpService.lngGetDataTableWithParameters(sql, ref table, objParams);

                if (IsTableNull(table))
                {
                    ybDefPayType = null;
                }
                else
                {
                    ybDefPayType = ConstructVO2(table.Rows[0]);
                }
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngDelete(clsYBDefPayTypeVO objVo)
        {
            long lngRes = 0;
            long lngAffter = 0;
            string strSQL = @"delete from t_opr_bih_ybdefpaytype where paytypeid_chr = ?";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(1, out param);
                param[0].Value = objVo.m_strPayTypeId;

                lngRes = objHRPSvc.lngExecuteParameterSQL(strSQL, ref lngAffter, param);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        [AutoComplete]
        public long m_lngUpdate(clsYBDefPayTypeVO objVo)
        {
            long lngRes = 0;
            string SQL = @"update t_opr_bih_ybdefpaytype
                               set jslx = ?,
                                   rylb = ?
                             where paytypeid_chr = ? ";
            long lngAffter = 0;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(3, out param);
                param[0].Value = objVo.m_strYBJslx;
                param[1].Value = objVo.m_strYBRylb;
                param[2].Value = objVo.m_strPayTypeId;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffter, param);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        /// <summary>
        /// 插入新记录
        /// </summary>
        /// <param name="ybDefPayTypeVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngInsert(clsYBDefPayTypeVO ybDefPayTypeVO)
        {
            long lngRes = 0;
            DataTable table = null;

            string sql = @"insert into t_opr_bih_ybdefpaytype(paytypeid_chr, jslx, rylb) values (?, ?, ?)";
            try
            {
                clsHRPTableService hrpService = new clsHRPTableService();
                IDataParameter[] objParams = null;
                hrpService.CreateDatabaseParameter(3, out objParams);
                objParams[0].Value = ybDefPayTypeVO.m_strPayTypeId;
                objParams[1].Value = ybDefPayTypeVO.m_strYBJslx;
                objParams[2].Value = ybDefPayTypeVO.m_strYBRylb;

                lngRes = hrpService.lngGetDataTableWithParameters(sql, ref table, objParams);
            }
            catch (Exception objEx)
            {
                lngRes = 0;
                new clsLogText().LogError(objEx);
            }
            return lngRes;
        }

        #endregion

        #region 获取科室信息
        /// <summary>
        /// 获取科室信息
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="m_dtDept"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptInfo(out DataTable m_dtDept, string strINTERNALFLAG)
        {
            long lngRes = 0;
            m_dtDept = new DataTable();
            string strSQL = string.Empty;
            if (strINTERNALFLAG == "-1")
            {
                strSQL = @"SELECT DISTINCT (b.deptid_chr), b.deptname_vchr
           FROM t_opr_outpatientrecipeinv a,
                t_bse_deptdesc b,
                t_bse_deptemp c,
                t_bse_employee d
          WHERE a.balanceemp_chr = d.empid_chr
            AND b.deptid_chr = c.deptid_chr
            AND c.empid_chr = d.empid_chr
       ORDER BY b.deptname_vchr";
            }
            else
            {
                strSQL = @"SELECT DISTINCT (b.deptid_chr), b.deptname_vchr
           FROM t_opr_outpatientrecipeinv a,
                t_bse_deptdesc b,
                t_bse_deptemp c,
                t_bse_employee d
          WHERE a.balanceemp_chr = d.empid_chr
            AND b.deptid_chr = c.deptid_chr
            AND c.empid_chr = d.empid_chr and a.INTERNALFLAG_INT=" + strINTERNALFLAG + @"
       ORDER BY b.deptname_vchr ";
            }
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.DoGetDataTable(strSQL, ref m_dtDept);
            }
            catch (Exception objEx)
            {
                string strTmp1 = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger1 = new clsLogText();
                bool blnRes1 = objLogger1.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region 收费结算日报表(未结账的发票信息)
        /// <summary>
        /// 收费结算日报表(未结账的发票信息)
        /// </summary> 
        /// <param name="strDate"></param>
        /// <param name="dtCheckOut"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetCheckOutData(string OPREMPID, string strDate, string strRptId, out DataTable dtCheckOut, out DataTable dtDiffSum)
        {
            if (strRptId == null || strRptId == "")
            {
                throw new Exception("报表的Id号为空，请从功能菜单传入报表Id号。");
            }
            long lngRes = 0;
            dtDiffSum = new DataTable();
            dtCheckOut = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"SELECT a.seqid_chr,a.invoiceno_vchr,
                                       a.recorddate_dat,
                                       a.opremp_chr,
                                       a.status_int,
                                       a.seqid_chr,
                                       a.balanceemp_chr,
                                       a.paytypeid_chr,
                                       a.acctsum_mny,
                                       a.sbsum_mny,
                                       a.totalsum_mny,a.totaldiffcost_mny,
                                       a.paytype_int,
                                       f.internalflag_int,
                                       b.itemcatid_chr,
                                       b.tolfee_mny,
                                       e.groupid_chr,
                                       e.groupname_chr, nvl(a.Totaldiffcost_Mny,0) diffPriceSum
                                  FROM t_opr_outpatientrecipeinv   a,
                                       t_opr_outpatientrecipesumde b,
                                       t_bse_patientpaytype f,
                                       (select c.typeid_chr, d.groupid_chr, d.groupname_chr from t_aid_rpt_gop_rla           c,
                                                     t_aid_rpt_gop_def           d
                                                     where  D.GROUPID_CHR = C.GROUPID_CHR 
                                                           AND D.RPTID_CHR = C.RPTID_CHR
                                                           AND d.rptid_chr = ?) e
                                 WHERE a.invoiceno_vchr = b.invoiceno_vchr
                                   AND a.seqid_chr = b.seqid_chr
                                   AND b.itemcatid_chr = e.TYPEID_CHR(+)
                                   AND A.PAYTYPEID_CHR = F.PAYTYPEID_CHR
                                   AND a.balanceflag_int = 0
                                   AND a.recorddate_dat < TO_DATE(?, 'yyyy-mm-dd HH24:mi:ss')
                                   AND a.recordemp_chr = ?
                                   and (a.isvouchers_int < 2 or a.isvouchers_int is null)
                                 ORDER BY a.INVOICENO_VCHR,a.seqid_chr";

            try
            {
                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                paramArr[0].Value = strRptId;
                paramArr[1].Value = strDate;
                paramArr[2].Value = OPREMPID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtCheckOut, paramArr);
                if (dtCheckOut != null && dtCheckOut.Rows.Count > 0)
                {
                    DataView dv = dtCheckOut.DefaultView;
                    dtDiffSum = dv.ToTable(true, new string[] { "seqid_chr", "invoiceno_vchr", "diffPriceSum" });
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

        #region 根据结帐人、结帐时间获取相应的重打发票信息
        /// <summary>
        /// 根据结帐人、结帐时间获取相应的重打发票信息
        /// </summary>
        /// <param name="BalanceEmp"></param>
        /// <param name="BalanceTime"></param>
        /// <returns></returns>
        [AutoComplete]
        public void m_mthGetbalancerepeatinvoinfo(string BalanceEmp, string BalanceTime, out string[] InvonoArr, int status, out decimal[] invoMoneyArr)
        {
            InvonoArr = null;
            invoMoneyArr = null;
            DataTable dt = new DataTable();
            long lngRes = 0;

            string strSQL1 = @"select distinct max(balance_dat)
                                  from t_opr_outpatientrecipeinv
                                 where balanceemp_chr = ?
                                   and balance_dat < to_date(?,'yyyy-mm-dd hh24:mi:ss')
                                   and balanceflag_int = 1
                                 order by balance_dat";


            string strSQL2 = @"select b.repprninvono_vchr, b.sourceinvono_vchr, a.totalsum_mny, a.totaldiffcost_mny  
                      from t_opr_outpatientrecipeinv a,
                           t_opr_invoicerepeatprint b
                     where a.seqid_chr = b.seqid_chr 
                       and b.type_chr = '1' 
                       and a.recordemp_chr = ? 
                       and b.printdate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                       and (a.isvouchers_int < 2 or a.isvouchers_int is null)
                    order by b.repprninvono_vchr";


            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();

                IDataParameter[] ParamArr1 = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr1);
                ParamArr1[0].Value = BalanceEmp;
                ParamArr1[1].Value = Convert.ToDateTime(BalanceTime).ToString("yyyy-MM-dd HH:mm:ss");
                DataTable dtResult = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL1, ref dtResult, ParamArr1);

                string strBeginDate = "";
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    strBeginDate = dtResult.Rows[0][0].ToString();
                }
                if (strBeginDate == "")
                    strBeginDate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd HH:mm:ss");

                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = BalanceEmp;
                ParamArr[1].Value = strBeginDate;
                ParamArr[2].Value = Convert.ToDateTime(BalanceTime).ToString("yyyy-MM-dd HH:mm:ss");

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL2, ref dt, ParamArr);
                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            if (lngRes > 0 && dt.Rows.Count > 0)
            {
                InvonoArr = new string[dt.Rows.Count];
                invoMoneyArr = new decimal[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    InvonoArr[i] = dt.Rows[i]["repprninvono_vchr"].ToString() + "(" + dt.Rows[i]["sourceinvono_vchr"].ToString() + ")";

                    decimal d1 = 0;
                    decimal d2 = 0;
                    decimal.TryParse(dt.Rows[i]["totalsum_mny"].ToString(), out d1);
                    decimal.TryParse(dt.Rows[i]["totaldiffcost_mny"].ToString(), out d2);
                    invoMoneyArr[i] = d1 - d2;
                }
            }
        }
        #endregion

        #region GetCheckOutHistory
        [AutoComplete]
        public long GetCheckOutHistory(string strDate, string BALANCEEMP, string strRptId, out DataTable dtCheckOut, out DataTable dtTemp)
        {
            if (strRptId == null || strRptId == "")
            {
                throw new Exception("报表的Id号为空，请从功能菜单传入报表Id号。");
            }
            long lngRes = 0;
            dtTemp = new DataTable();
            //dtPayType = null;
            dtCheckOut = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL;

            strSQL = @"SELECT a.seqid_chr, a.invoiceno_vchr,
                           a.recorddate_dat,
                           a.opremp_chr,
                           a.status_int,
                           a.seqid_chr,
                           a.balanceemp_chr,
                           a.paytypeid_chr,
                           a.acctsum_mny,
                           a.sbsum_mny,
                           a.totalsum_mny,
                           a.paytype_int,
                           f.internalflag_int,
                           b.itemcatid_chr,
                           b.tolfee_mny,
                           e.groupid_chr,
                           e.groupname_chr, nvl(a.Totaldiffcost_Mny,0) diffPriceSum
                      FROM t_opr_outpatientrecipeinv   a,
                           t_opr_outpatientrecipesumde b,
                           t_bse_patientpaytype f,
                           (select c.typeid_chr, d.groupid_chr, d.groupname_chr 
                                    from t_aid_rpt_gop_rla           c,
                                         t_aid_rpt_gop_def           d
                                 where  D.GROUPID_CHR = C.GROUPID_CHR 
                                       AND D.RPTID_CHR = C.RPTID_CHR
                                       AND d.rptid_chr = ?) e
                     WHERE a.invoiceno_vchr = b.invoiceno_vchr
                       AND a.seqid_chr = b.seqid_chr
                       AND b.itemcatid_chr = e.TYPEID_CHR(+)
                       AND A.PAYTYPEID_CHR = F.PAYTYPEID_CHR(+)
                       AND a.balanceflag_int = 1
                       and (a.isvouchers_int < 2 or a.isvouchers_int is null)
                       AND a.balance_dat = TO_DATE(?, 'yyyy-mm-dd HH24:mi:ss')
                       AND a.balanceemp_chr = ?
                     order by a.INVOICENO_VCHR, a.SEQID_CHR";
            try
            {

                System.Data.IDataParameter[] paramArr = null;
                objHRPSvc.CreateDatabaseParameter(3, out paramArr);
                paramArr[0].Value = strRptId;
                paramArr[1].Value = strDate;
                paramArr[2].Value = BALANCEEMP;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtCheckOut, paramArr);
                com.digitalwave.Utility.clsLogText objLogg = new clsLogText();
                objLogg.LogError("1 " + strRptId + "2 " + strDate + " 3" + BALANCEEMP);
                if (dtCheckOut != null && dtCheckOut.Rows.Count > 0)
                {
                    DataView dv = dtCheckOut.DefaultView;
                    dtTemp = dv.ToTable(true, new string[] { "seqid_chr", "invoiceno_vchr", "diffPriceSum" });
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

        #region 根据选择的科室ID重新加载收费员
        [AutoComplete]
        public long m_lngGetCheckManByDeptId(out DataTable dt, string strdeptId)
        {
            string strSQL = "";
            long lngRes = 0;
            dt = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                System.Data.IDataParameter[] paramArr = null;
                if (strdeptId != "1000")
                {
                    strSQL = @"select distinct a.BALANCEEMP_CHR, b.LASTNAME_VCHR
  from t_opr_outpatientrecipeinv a, t_bse_employee b,t_bse_deptemp c,t_bse_deptdesc d
 where BALANCEFLAG_INT = 1
   and a.BALANCEEMP_CHR = b.EMPID_CHR
   and b.empid_chr=c.empid_chr
   and c.deptid_chr=d.deptid_chr
   and d.deptid_chr=?";
                    objHRPSvc.CreateDatabaseParameter(1, out paramArr);
                    paramArr[0].Value = strdeptId;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, paramArr);
                }
                else if (strdeptId == "1000")
                {
                    strSQL = @"select distinct a.BALANCEEMP_CHR, b.LASTNAME_VCHR
  from t_opr_outpatientrecipeinv a, t_bse_employee b
 where BALANCEFLAG_INT = 1
   and a.BALANCEEMP_CHR = b.EMPID_CHR";
                    lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dt);
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














    }
}
