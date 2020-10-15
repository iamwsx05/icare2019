using System;
using System.EnterpriseServices;
using System.Data;
using System.Text;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.Collections;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// 
    /// </summary>
    [System.EnterpriseServices.Transaction(System.EnterpriseServices.TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsPatchMedicine_Svc:com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        public clsPatchMedicine_Svc()
        {

        }

        /// <summary>
        /// 查询含有退费记录的病人
        /// </summary>
        /// <param name="ExeDept"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="DeptArr"></param>
        /// <param name="dtResult"></param>
        /// <param name="intTimeType">0 入院时间 1 出院时间</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatient(string ExeDept, string BeginTime, string EndTime, string DeptArr, out DataTable dtResult,int intTimeType)
        {
            long lngRes = -1;
            string strSQL = string.Empty;
            dtResult = null;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;

                if (intTimeType == 0)
                {
                    strSQL = @"select  ta.registerid_chr, ta.inpatientid_chr, ta.deptid_chr,
                                       ta.inpatientcount_int, ta.inpatient_dat, tb.lastname_vchr, tb.sex_chr,
                                       ta.pstatus_int, ta.inpatient_dat as inpatienttime,td.deptname_vchr
                                  from t_opr_bih_register ta,
                                       t_opr_bih_registerdetail tb,
                                       (select distinct a.registerid_chr
                                                   from t_opr_bih_patientcharge a, t_opr_bih_register b
                                                  where a.registerid_chr = b.registerid_chr
                                                    and (b.inpatient_dat between ? and ? )
                                                    and b.areaid_chr in ([DeptArr])
                                                    and a.clacarea_chr = ?
                                                    and a.orderexectype_int = 9
                                                    and a.isconfirmrefundment = 0
                                                    and a.status_int = 1
                                                    and a.pstatus_int <> 2
                                                    and a.pstatus_int <> 3) tc,
                                                    t_bse_deptdesc td
                                 where ta.registerid_chr = tb.registerid_chr
                                   and ta.registerid_chr = tc.registerid_chr
                                   and ta.deptid_chr=td.deptid_chr
                                union all
                                select ta.registerid_chr, ta.inpatientid_chr, ta.deptid_chr,
                                       ta.inpatientcount_int, ta.inpatient_dat, tb.lastname_vchr, tb.sex_chr,
                                       ta.pstatus_int, outhospital_dat as inpatienttime,te.deptname_vchr
                                  from t_opr_bih_register ta,
                                       t_opr_bih_registerdetail tb,
                                       (select distinct a.registerid_chr
                                                   from t_opr_bih_patientcharge a, t_opr_bih_register b
                                                  where a.registerid_chr = b.registerid_chr
                                                    and (b.inpatient_dat between ? and ?)
                                                    and b.areaid_chr in ([DeptArr])
                                                    and a.clacarea_chr = ? 
                                                    and a.orderexectype_int = 9
                                                    and a.isconfirmrefundment = 0
                                                    and a.status_int = 1
                                                    and (a.pstatus_int = 2 or a.pstatus_int = 3)) tc,
                                       t_opr_bih_leave td,
                                       t_bse_deptdesc te
                                 where ta.registerid_chr = tb.registerid_chr
                                   and ta.registerid_chr = tc.registerid_chr
                                   and ta.registerid_chr = td.registerid_chr
                                   and td.outdeptid_chr=te.deptid_chr
                                   and td.status_int = 1 ";
                    if(DeptArr.Length>0)
                    {
                        strSQL = strSQL.Replace("[DeptArr]", DeptArr);
                    }
                    objHRPSvc.CreateDatabaseParameter(6, out param);
                    param[0].DbType = DbType.DateTime;
                    param[0].Value = DateTime.Parse(BeginTime);
                    param[1].DbType = DbType.DateTime;
                    param[1].Value = DateTime.Parse(EndTime);
                    param[2].Value = ExeDept;
                    param[3].DbType = DbType.DateTime;
                    param[3].Value = DateTime.Parse(BeginTime);
                    param[4].DbType = DbType.DateTime;
                    param[4].Value = DateTime.Parse(EndTime);
                    param[5].Value = ExeDept;


                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, param);
                    objHRPSvc.Dispose();
                }
                else if (intTimeType == 1)
                {
                    strSQL = @"select   ta.registerid_chr, ta.inpatientid_chr, ta.deptid_chr,
                                         ta.inpatientcount_int, ta.inpatient_dat, tb.lastname_vchr,
                                         tb.sex_chr, ta.pstatus_int, td.outareaid_chr, td.outhospital_dat as inpatienttime ,
                                         te.deptname_vchr
                                    from t_opr_bih_register ta,
                                         t_opr_bih_registerdetail tb,
                                         (select distinct a.registerid_chr
                                                     from t_opr_bih_patientcharge a, t_opr_bih_leave c
                                                    where a.registerid_chr = c.registerid_chr
                                                      and (c.outhospital_dat between ? and ?)
                                                      and c.outareaid_chr in ([DeptArr])
                                                      and a.clacarea_chr = ?
                                                      and a.orderexectype_int = 9
                                                      and a.isconfirmrefundment = 0
                                                      and a.status_int = 1) tc,
                                         t_opr_bih_leave td,
                                         t_bse_deptdesc te
                                   where ta.registerid_chr = tb.registerid_chr
                                     and ta.registerid_chr = tc.registerid_chr
                                     and ta.registerid_chr = td.registerid_chr
                                     and td.outareaid_chr = te.deptid_chr
                                order by ta.inpatientid_chr, tb.lastname_vchr, ta.inpatientcount_int desc  ";
                    if (DeptArr.Length > 0)
                    {
                        strSQL = strSQL.Replace("[DeptArr]", DeptArr);
                    }
                    objHRPSvc.CreateDatabaseParameter(3, out param);
                    param[0].DbType = DbType.DateTime;
                    param[0].Value = DateTime.Parse(BeginTime);
                    param[1].DbType = DbType.DateTime;
                    param[1].Value = DateTime.Parse(EndTime);
                    param[2].Value = ExeDept;

                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, param);
                    objHRPSvc.Dispose();
                }
                else if (intTimeType == 2)
                {
                    strSQL = @"select  ta.registerid_chr, ta.inpatientid_chr, ta.deptid_chr,
                                       ta.inpatientcount_int, ta.inpatient_dat, tb.lastname_vchr, tb.sex_chr,
                                       ta.pstatus_int, ta.inpatient_dat as inpatienttime, td.deptname_vchr
                                  from t_opr_bih_register ta,
                                       t_opr_bih_registerdetail tb,
                                       (select distinct a.registerid_chr
                                                   from t_opr_bih_patientcharge a, t_opr_bih_register b
                                                  where a.registerid_chr = b.registerid_chr
                                                    and (a.active_dat between ? and ?)
                                                    and b.areaid_chr in ([DeptArr])
                                                    and a.clacarea_chr = ?
                                                    and a.orderexectype_int = 9
                                                    and a.isconfirmrefundment = 0
                                                    and a.status_int = 1
                                                    and a.pstatus_int <> 2
                                                    and a.pstatus_int <> 3) tc,
                                       t_bse_deptdesc td
                                 where ta.registerid_chr = tb.registerid_chr
                                   and ta.registerid_chr = tc.registerid_chr
                                   and ta.deptid_chr = td.deptid_chr
                                union all
                                select ta.registerid_chr, ta.inpatientid_chr, ta.deptid_chr,
                                       ta.inpatientcount_int, ta.inpatient_dat, tb.lastname_vchr, tb.sex_chr,
                                       ta.pstatus_int, outhospital_dat as inpatienttime, te.deptname_vchr
                                  from t_opr_bih_register ta,
                                       t_opr_bih_registerdetail tb,
                                       (select distinct a.registerid_chr
                                                   from t_opr_bih_patientcharge a, t_opr_bih_register b
                                                  where a.registerid_chr = b.registerid_chr
                                                    and (a.active_dat between ? and ?)
                                                    and b.areaid_chr in ([DeptArr])
                                                    and a.clacarea_chr = ?
                                                    and a.orderexectype_int = 9
                                                    and a.isconfirmrefundment = 0
                                                    and a.status_int = 1
                                                    and (a.pstatus_int = 2 or a.pstatus_int = 3)) tc,
                                       t_opr_bih_leave td,
                                       t_bse_deptdesc te
                                 where ta.registerid_chr = tb.registerid_chr
                                   and ta.registerid_chr = tc.registerid_chr
                                   and ta.registerid_chr = td.registerid_chr
                                   and td.outdeptid_chr = te.deptid_chr
                                   and td.status_int = 1 ";
                    if (DeptArr.Length > 0)
                    {
                        strSQL = strSQL.Replace("[DeptArr]", DeptArr);
                    }
                    objHRPSvc.CreateDatabaseParameter(6, out param);
                    param[0].DbType = DbType.DateTime;
                    param[0].Value = DateTime.Parse(BeginTime);
                    param[1].DbType = DbType.DateTime;
                    param[1].Value = DateTime.Parse(EndTime);
                    param[2].Value = ExeDept;
                    param[3].DbType = DbType.DateTime;
                    param[3].Value = DateTime.Parse(BeginTime);
                    param[4].DbType = DbType.DateTime;
                    param[4].Value = DateTime.Parse(EndTime);
                    param[5].Value = ExeDept;


                    lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, param);
                    objHRPSvc.Dispose();
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

        /// <summary>
        /// 按ID号来查找
        /// </summary>
        /// <param name="ExeDept"></param>
        /// <param name="InpatientID"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientByID(string ExeDept, string InpatientID, out DataTable dtResult)
        {
            long lngRes = -1;
            string strSQL = @"select   ta.registerid_chr, ta.inpatientid_chr, ta.deptid_chr,
                                       ta.inpatientcount_int, ta.inpatient_dat, tc.lastname_vchr, tc.sex_chr,
                                       ta.pstatus_int, td.outareaid_chr, td.outhospital_dat as inpatienttime,
                                       te.deptname_vchr
                                  from t_opr_bih_register ta,
                                       (select distinct a.registerid_chr
                                                   from t_opr_bih_register a, t_opr_bih_patientcharge b
                                                  where a.registerid_chr = b.registerid_chr
                                                    and b.orderexectype_int = 9
                                                    and b.status_int = 1
                                                    and (a.pstatus_int = 2 or a.pstatus_int = 3)
                                                    and b.clacarea_chr = ?
                                                    and a.inpatientid_chr = ?) tb,
                                       t_opr_bih_registerdetail tc,
                                       t_opr_bih_leave td,
                                       t_bse_deptdesc te
                                 where ta.registerid_chr = tb.registerid_chr
                                   and ta.registerid_chr = tc.registerid_chr
                                   and ta.registerid_chr = td.registerid_chr
                                   and te.deptid_chr = ta.deptid_chr
                                   and td.status_int = 1
                                union all
                                select ta.registerid_chr, ta.inpatientid_chr, ta.deptid_chr,
                                       ta.inpatientcount_int, ta.inpatient_dat, tc.lastname_vchr, tc.sex_chr,
                                       ta.pstatus_int, '' as outareaid_chr, ta.inpatient_dat as inpatienttime,
                                       te.deptname_vchr
                                  from t_opr_bih_register ta,
                                       (select distinct a.registerid_chr
                                                   from t_opr_bih_register a, t_opr_bih_patientcharge b
                                                  where a.registerid_chr = b.registerid_chr
                                                    and b.orderexectype_int = 9
                                                    and b.status_int = 1
                                                    and (   a.pstatus_int = 0
                                                         or a.pstatus_int = 1
                                                         or a.pstatus_int = 4
                                                        )
                                                    and b.clacarea_chr = ?
                                                    and a.inpatientid_chr = ?) tb,
                                       t_opr_bih_registerdetail tc,
                                       t_bse_deptdesc te
                                 where ta.registerid_chr = tb.registerid_chr
                                   and ta.registerid_chr = tc.registerid_chr
                                   and te.deptid_chr = ta.deptid_chr ";
            dtResult = null;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(4, out param);
                param[0].Value = ExeDept;
                param[1].Value = InpatientID;
                param[2].Value = ExeDept;
                param[3].Value = InpatientID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtResult, param);
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

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="PchargeArr"></param>
        /// <param name="EmpID"></param>
        /// <param name="EmpName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngPatchMed(System.Collections.Generic.List<string> PchargeArr, string EmpID, string EmpName)
        {
            string strSQL = @"update   t_opr_bih_patientcharge
                                   set isconfirmrefundment = 1,
                                       confirmerid_chr = ?,
                                       confirmer_vchr = ?,
                                       confirm_dat = sysdate
                                 where pchargeid_chr = ? ";
            long lngRes = -1;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                DbType[] dbtypes = new DbType[] { DbType.String, DbType.String, DbType.String };

                object[][] objValues = new object[3][];

                for (int i1 = 0; i1 < objValues.Length; i1++)
                {
                    objValues[i1] = new object[PchargeArr.Count];
                }

                for (int k = 0; k < PchargeArr.Count; k++)
                {
                    int n = 0;
                    objValues[n++][k] = EmpID;
                    objValues[n++][k] = EmpName;
                    objValues[n++][k] = PchargeArr[k].ToString();
                }

                lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbtypes);
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

        /// <summary>
        /// 通过ID获得退药信息
        /// </summary>
        /// <param name="RegisterID"></param>
        /// <param name="ExeDept"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedByRegID(string RegisterID, string ExeDept, out DataTable dt)
        {
            string strSQL = @"select   a.pchargeid_chr, a.patientid_chr, a.registerid_chr, a.active_dat,
                                       a.orderid_chr, a.orderexectype_int, a.orderexecid_chr, a.clacarea_chr,
                                       a.createarea_chr, a.calccateid_chr, a.invcateid_chr,
                                       a.chargeitemid_chr, a.chargeitemname_chr, a.unit_vchr, a.unitprice_dec,
                                       a.amount_dec, a.discount_dec, a.ismepay_int, a.des_vchr,
                                       a.createtype_int, a.creator_chr, a.create_dat, a.operator_chr,
                                       a.modify_dat, a.deactivator_chr, a.deactivate_dat, a.status_int,
                                       a.pstatus_int, a.chearaccount_dat, a.dayaccountid_chr,
                                       a.paymoneyid_chr, a.activator_chr, a.activatetype_int, a.isrich_int,
                                       a.isconfirmrefundment, a.refundmentchecker, a.refundmentdate,
                                       a.bmstatus_int, a.curareaid_chr, a.curbedid_chr, a.doctorid_chr,
                                       a.doctor_vchr, a.doctorgroupid_chr, a.needconfirm_int,
                                       a.confirmerid_chr, a.confirmer_vchr, a.confirm_dat, a.chargeactive_dat,
                                       a.insuracedesc_vchr, a.spec_vchr, a.totalmoney_dec, a.acctmoney_dec,
                                       a.patientnurse_int, a.newdiscount_dec, a.attachorderid_vchr,
                                       a.attachorderbasenum_dec, a.putmedicineflag_int, a.chargedoctorid_chr,
                                       a.chargedoctor_vchr, a.pchargeidorg_chr, b.itemcode_vchr,
                                       b.itemopcode_chr, b.itemname_vchr, c.orderno_int,
                                       (case a.pstatus_int
                                           when 3
                                              then a.discount_dec
                                           when 4
                                              then a.discount_dec
                                           else e.precent_dec
                                        end
                                       ) as precent_dec,
                                       f.typename_vchr as ipinvoname, b.insuranceid_chr as ybcode,
                                       g.lastname_vchr
                                  from t_opr_bih_patientcharge a,
                                       t_bse_chargeitem b,
                                       t_opr_bih_dayaccount c,
                                       t_opr_bih_register d,
                                       t_aid_inschargeitem e,
                                       t_bse_chargeitemextype f,
                                       t_bse_employee g
                                 where a.chargeitemid_chr = b.itemid_chr
                                   and a.dayaccountid_chr = c.dayaccountid_chr(+)
                                   and a.registerid_chr = d.registerid_chr
                                   and a.chargeitemid_chr = e.itemid_chr
                                   and b.itemipinvtype_chr = f.typeid_chr
                                   and d.paytypeid_chr = e.copayid_chr
                                   and a.pstatus_int <> 0
                                   and a.status_int = 1
                                   and f.flag_int = 4
                                   and a.chargeactive_dat is not null
                                   and a.orderexectype_int = 9
                                   and g.empid_chr = a.refundmentchecker
                                   and a.clacarea_chr = ?
                                   and a.registerid_chr = ? ";
            dt = null;
            long lngRes = -1;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(2, out param);
                param[0].Value = ExeDept;
                param[1].Value = RegisterID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, param);
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
    }
}
