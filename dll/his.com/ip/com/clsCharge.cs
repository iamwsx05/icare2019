using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;
using com.digitalwave.Utility;
using weCare.Core.Utils;
using System.Linq;

namespace com.digitalwave.iCare.middletier.HIS
{
    #region clsCharge
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsCharge : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsCharge()
        {
        }
        #endregion

        #region ��ȡϵͳ����
        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        /// <param name="parmcode"></param>
        /// <returns></returns>
        [AutoComplete]
        public int m_intGetSysParm(string setid)
        {
            int val = -999;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = setid;

                string SQL = @"select setstatus_int from t_sys_setting where setid_chr = ?";

                DataTable dt = new DataTable();

                long l = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                if (l > 0 && dt.Rows.Count == 1)
                {
                    val = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return val;
        }

        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        /// <param name="parmcodearr">��������</param>
        /// <returns>ֵ</returns>
        [AutoComplete]
        public System.Collections.Generic.Dictionary<string, string> m_hasGetSysParm(List<string> parmcodearr)
        {
            System.Collections.Generic.Dictionary<string, string> hasValues = new System.Collections.Generic.Dictionary<string, string>();
            try
            {
                string strValue = "";

                for (int i = 0; i < parmcodearr.Count; i++)
                {
                    strValue += "'" + parmcodearr[i].Trim() + "',";
                }
                strValue = strValue.Substring(0, strValue.Length - 1);

                string SQL = @"select parmcode_chr, parmvalue_vchr
                                 from t_bse_sysparm 
                                where status_int = 1 
                                  and parmcode_chr in (" + strValue + ")";

                DataTable dt = new DataTable();
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                long l = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
                if (l > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        hasValues.Add(dt.Rows[i]["parmcode_chr"].ToString(), dt.Rows[i]["parmvalue_vchr"].ToString());
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return hasValues;
        }

        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        /// <param name="setidarr">��������</param>
        /// <returns>ֵ</returns>
        [AutoComplete]
        public System.Collections.Generic.Dictionary<string, string> m_hasGetSysSetting(List<string> setidarr)
        {
            System.Collections.Generic.Dictionary<string, string> hasValues = new System.Collections.Generic.Dictionary<string, string>();
            try
            {
                string strValue = "";

                for (int i = 0; i < setidarr.Count; i++)
                {
                    strValue += "'" + setidarr[i].Trim() + "',";
                }
                strValue = strValue.Substring(0, strValue.Length - 1);

                string SQL = @"select setid_chr, setstatus_int 
                                 from t_sys_setting 
                                where setid_chr in (" + strValue + ")";

                DataTable dt = new DataTable();
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                long l = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
                if (l > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        hasValues.Add(dt.Rows[i]["setid_chr"].ToString(), dt.Rows[i]["setstatus_int"].ToString());
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return hasValues;
        }
        #endregion

        #region ���ݷ��෶Χ��ȡ���÷���(������㡢��Ʊ��סԺ���㡢��Ʊ)������Ϣ
        /// <summary>
        /// ���ݷ��෶Χ��ȡ���÷���(������㡢��Ʊ��סԺ���㡢��Ʊ)������Ϣ
        /// </summary>
        /// <param name="Scope">��Χ: 1 ������� 2 ���﷢Ʊ 3 סԺ���� 4 סԺ��Ʊ</param>
        /// <param name="Status">% ȫ�� 0 ͣ�� 1 ����</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDefChargeCat(string Scope, string Status, out DataTable dt)
        {
            long lngRes = 0;

            string SQL = @" select scope_chr, catid_chr, catname_vchr, type_int, compexp_vchr,
                                   dispctl_vchr, prtclt_vchr, status_int 
                              from t_bse_defchargecat
                             where status_int like ? 
                               and scope_chr = ? 
                          order by catid_chr";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = Status;
                ParamArr[1].Value = Scope;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ��ȡԱ�������
        /// <summary>
        /// ��ȡԱ�������
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployee(out DataTable dt)
        {
            long lngRes = 0;
            string SQL = string.Empty;

            SQL = @"select a.empid_chr,
                           a.empno_chr,
                           a.lastname_vchr,
                           a.technicalrank_chr,
                           a.status_int,
                           c.deptid_chr
                      from t_bse_employee a
                     inner join t_bse_deptemp b
                        on a.empid_chr = b.empid_chr
                     inner join t_bse_deptdesc c
                        on b.deptid_chr = c.deptid_chr
                     where a.status_int >= 0
                       and b.default_inpatient_dept_int = 1
                     order by a.empno_chr";

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
        /// <summary>
        /// ��ȡԱ�������
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="empTypeId">0 ȫ��; 1 ҽ��; 3 ����ҩ����ר��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployee(out DataTable dt, int empTypeId)
        {
            long lngRes = 0;
            string SQL = string.Empty;
            dt = new DataTable();

            if (empTypeId == 1)
            {
                SQL = @"select a.empid_chr,
                               a.empno_chr,
                               a.lastname_vchr,
                               a.technicalrank_chr,
                               a.status_int,
                               c.deptid_chr
                          from t_bse_employee a
                         inner join t_bse_deptemp b
                            on a.empid_chr = b.empid_chr
                         inner join t_bse_deptdesc c
                            on b.deptid_chr = c.deptid_chr
                         where a.technicalrank_chr like '%ҽʦ%'
                           and a.status_int >= 0
                           and b.default_inpatient_dept_int = 1
                         order by a.empno_chr";
            }
            else if (empTypeId == 3)
            {
                SQL = @"select a.empid_chr,
                               a.empno_chr,
                               a.lastname_vchr,
                               a.technicalrank_chr,
                               a.status_int,
                               c.deptid_chr
                          from t_bse_employee a
                         inner join t_bse_deptemp b
                            on a.empid_chr = b.empid_chr
                         inner join t_bse_deptdesc c
                            on b.deptid_chr = c.deptid_chr
                         where a.isAntiExpert = 1 
                           and a.status_int >= 0
                           and b.default_inpatient_dept_int = 1
                         order by a.empno_chr";
            }
            else
            {
                SQL = @"select a.empid_chr,
                               a.empno_chr,
                               a.lastname_vchr,
                               a.technicalrank_chr,
                               a.status_int,
                               c.deptid_chr
                          from t_bse_employee a
                         inner join t_bse_deptemp b
                            on a.empid_chr = b.empid_chr
                         inner join t_bse_deptdesc c
                            on b.deptid_chr = c.deptid_chr
                         where a.status_int >= 0
                           and b.default_inpatient_dept_int = 1
                         order by a.empno_chr";
            }

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

        /// <summary>
        /// ����Ա�����Ż�ȡID������
        /// </summary>
        /// <param name="EmpNo"></param>
        /// <param name="EmpID"></param>
        /// <param name="EmpName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetEmployee(string EmpNo, out string EmpID, out string EmpName)
        {
            long lngRes = 0;
            DataTable dt = new DataTable();

            string SQL = @"select a.empid_chr, a.lastname_vchr
                             from t_bse_employee a 
                            where a.empno_chr = ?";

            EmpID = "";
            EmpName = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = EmpNo;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    EmpID = dt.Rows[0]["empid_chr"].ToString();
                    EmpName = dt.Rows[0]["lastname_vchr"].ToString();
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

        #region ����סԺ�Ǽ���ˮ�Ų��Ҳ�������������Ϣ
        /// <summary>
        /// ����סԺ�Ǽ���ˮ�Ų��Ҳ�������������Ϣ
        /// </summary>
        /// <param name="RegID">סԺ�Ǽ���ˮ��</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientDayaccountsByRegID(string RegID, out DataTable dt)
        {
            long lngRes = 0;

            string SQL = @" select dayaccountid_chr, patientid_chr, registerid_chr, orderno_int,
                                   square_dat, charge_dec, clearchg_dec, chgofmepay_dec, clearchgofme_dec,
                                   chgofhepay_dec, clearchgofhe_dec, clearmpoptid_chr, chearmp_dat,
                                   clearhpoptid_chr, clearhp_dat, areaid_chr, create_dat, note_vchr,
                                   currareaid_chr, operid_chr, type_int
                              from t_opr_bih_dayaccount where registerid_chr = ? order by orderno_int";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = RegID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ��ȡ��������Ч������Ϣ
        /// <summary>
        /// ��ȡ��������Ч������Ϣ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="type">1 ������Ժ�Ǽ�ID 2 ��������ID </param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeInfoByID(string ID, int type, out DataTable dt)
        {
            long lngRes = 0;
            string SQL = "";
            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                if (type == 1)
                {
                    SQL = @"select a.pchargeid_chr, a.patientid_chr, a.registerid_chr, a.active_dat,
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
                                   a.chargedoctor_vchr, a.pchargeidorg_chr, b.itemcode_vchr, b.itemopcode_chr, 
                                   b.itemname_vchr, c.orderno_int,
                                   (case a.pstatus_int
                                       when 3
                                          then a.discount_dec
                                       when 4
                                          then a.discount_dec
                                       else e.precent_dec
                                    end
                                   ) as precent_dec,
                                   f.typename_vchr as ipinvoname, b.insuranceid_chr as ybcode,
                                   g.chargetotalsum,a.itemchargetype_vchr,a.totaldiffcostmoney_dec,
                                   DECODE (b.ipchargeflg_int,
                                     1, round (b.tradeprice_mny / b.packqty_dec, 4),
                                     0, b.tradeprice_mny,
                                 round (b.tradeprice_mny / b.packqty_dec, 4)
                                ) tradeprice_mny, a.buyprice_dec 
                              from t_opr_bih_patientcharge a,
                                   t_bse_chargeitem b,
                                   t_opr_bih_dayaccount c,
                                   t_opr_bih_register d,
                                   t_aid_inschargeitem e,
                                   t_bse_chargeitemextype f,
                                   (select   a.pchargeid_chr, sum (a.totalmoney_dec) as chargetotalsum
                                        from t_opr_bih_chargeitementry a, 
                                             t_opr_bih_register b,
                                             t_opr_bih_charge c 
                                       where a.registerid_chr = b.registerid_chr
                                         and a.chargeno_chr = c.chargeno_chr
                                         and b.status_int = 1
                                         and b.feestatus_int = 4
                                         and c.class_int = 3
                                         and a.registerid_chr = ?
                                    group by a.pchargeid_chr) g
                             where a.chargeitemid_chr = b.itemid_chr
                               and a.dayaccountid_chr = c.dayaccountid_chr(+)
                               and a.registerid_chr = d.registerid_chr
                               and a.chargeitemid_chr = e.itemid_chr
                               and b.itemipinvtype_chr = f.typeid_chr
                               and d.paytypeid_chr = e.copayid_chr
                               and a.pchargeid_chr = g.pchargeid_chr(+)
                               and a.pstatus_int <> 0
                               and a.status_int = 1
                               and f.flag_int = 4
                               and a.chargeactive_dat is not null
                               and a.registerid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = ID;
                    ParamArr[1].Value = ID;
                }
                else if (type == 2)
                {
                    SQL = @"select a.pchargeid_chr, a.patientid_chr, a.registerid_chr, a.active_dat,
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
                                   a.chargedoctor_vchr, a.pchargeidorg_chr, b.itemcode_vchr, b.itemname_vchr, itemopcode_chr, c.orderno_int,
                                  (case a.pstatus_int
                                     when 3
                                        then a.discount_dec
                                     when 4
                                        then a.discount_dec
                                     else e.precent_dec
                                  end
                                  ) as precent_dec,
                                  f.typename_vchr as ipinvoname, b.insuranceid_chr as ybcode ,a.itemchargetype_vchr,a.totaldiffcostmoney_dec,
                                  DECODE (b.ipchargeflg_int,
                                     1, round (b.tradeprice_mny / b.packqty_dec, 4),
                                     0, b.tradeprice_mny,
                                 round (b.tradeprice_mny / b.packqty_dec, 4)
                                ) tradeprice_mny, a.buyprice_dec 
                             from t_opr_bih_patientcharge a,
                                  t_bse_chargeitem b,
                                  t_opr_bih_dayaccount c, 
                                  t_opr_bih_register d,
                                  t_aid_inschargeitem e, 
                                  t_bse_chargeitemextype f                                            
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
                              and a.dayaccountid_chr = ?                              
                         order by a.chargeactive_dat, a.orderid_chr, a.chargeitemid_chr";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = ID;
                }

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ĸӤ�ϲ��������һ���ѯucpatien�ؼ�ʹ��

        /// <summary>
        /// ĸӤ�ϲ��������һ���ѯucpatien�ؼ�ʹ��
        /// </summary>
        /// <param name="p_strRegisterID">����registerId</param>
        /// <param name="p_dtbCharge"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeInfoByIDForBaby(string p_strRegisterID, out DataTable p_dtbCharge)
        {
            long lngRes = 0;
            string SQL = "";
            p_dtbCharge = new DataTable();
            clsHRPTableService objHRPSvc = null; ;
            IDataParameter[] ParamArr = null;
            try
            {
                SQL = @"select a.pchargeid_chr, a.patientid_chr, a.registerid_chr, a.active_dat,
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
                                   a.chargedoctor_vchr, a.pchargeidorg_chr, b.itemcode_vchr, b.itemopcode_chr, 
                                   b.itemname_vchr, c.orderno_int,
                                   (case a.pstatus_int
                                       when 3
                                          then a.discount_dec
                                       when 4
                                          then a.discount_dec
                                       else e.precent_dec
                                    end
                                   ) as precent_dec,
                                   f.typename_vchr as ipinvoname, b.insuranceid_chr as ybcode,
                                   g.chargetotalsum,a.itemchargetype_vchr,
                                   DECODE (b.ipchargeflg_int,
                                     1, round (b.tradeprice_mny / b.packqty_dec, 4),
                                     0, b.tradeprice_mny,
                                 round (b.tradeprice_mny / b.packqty_dec, 4)
                                ) tradeprice_mny,a.totaldiffcostmoney_dec, a.buyprice_dec 
                              from t_opr_bih_patientcharge a,
                                   t_bse_chargeitem b,
                                   t_opr_bih_dayaccount c,
                                   t_opr_bih_register d,
                                   t_aid_inschargeitem e,
                                   t_bse_chargeitemextype f,
                                   (select   a.pchargeid_chr, sum (a.totalmoney_dec) as chargetotalsum
                                        from t_opr_bih_chargeitementry a, 
                                             t_opr_bih_register b,
                                             t_opr_bih_charge c 
                                       where a.registerid_chr = b.registerid_chr
                                         and a.chargeno_chr = c.chargeno_chr
                                         and b.status_int = 1
                                         and b.feestatus_int = 4
                                         and c.class_int = 3
                                         and a.registerid_chr = ?
                                    group by a.pchargeid_chr) g
                             where a.chargeitemid_chr = b.itemid_chr
                               and a.dayaccountid_chr = c.dayaccountid_chr(+)
                               and a.registerid_chr = d.registerid_chr
                               and a.chargeitemid_chr = e.itemid_chr
                               and b.itemipinvtype_chr = f.typeid_chr
                               and d.paytypeid_chr = e.copayid_chr
                               and a.pchargeid_chr = g.pchargeid_chr(+)
                               and a.pstatus_int in (1,2,3,4)
                               and a.status_int = 1
                               and f.flag_int = 4
                               and a.chargeactive_dat is not null
                               and a.registerid_chr = ?";
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = p_strRegisterID;
                ParamArr[1].Value = p_strRegisterID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref p_dtbCharge, ParamArr);
                //�жϲ����Ƿ���Ӥ���������ã��еĻ��ͺϲ�δ����
                SQL = @"select t.registerid_chr
  from t_opr_bih_register t
 where t.pstatus_int in (2,3)
 and t.relateregisterid_chr = ?";
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = p_strRegisterID;
                DataTable dtbBabyId = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtbBabyId, ParamArr);
                if (lngRes > 0 && dtbBabyId.Rows.Count > 0)
                {
                    SQL = @"select a.pchargeid_chr, a.patientid_chr, a.registerid_chr, a.active_dat,
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
                                   a.chargedoctor_vchr, a.pchargeidorg_chr, b.itemcode_vchr, b.itemopcode_chr, 
                                   b.itemname_vchr, c.orderno_int,
                                   (case a.pstatus_int
                                       when 3
                                          then a.discount_dec
                                       when 4
                                          then a.discount_dec
                                       else e.precent_dec
                                    end
                                   ) as precent_dec,
                                   f.typename_vchr as ipinvoname, b.insuranceid_chr as ybcode,
                                   g.chargetotalsum,a.itemchargetype_vchr,
                                   DECODE (b.ipchargeflg_int,
                                     1, round (b.tradeprice_mny / b.packqty_dec, 4),
                                     0, b.tradeprice_mny,
                                 round (b.tradeprice_mny / b.packqty_dec, 4)
                                ) tradeprice_mny,a.totaldiffcostmoney_dec, a.buyprice_dec 
                              from t_opr_bih_patientcharge a,
                                   t_bse_chargeitem b,
                                   t_opr_bih_dayaccount c,
                                   t_opr_bih_register d,
                                   t_aid_inschargeitem e,
                                   t_bse_chargeitemextype f,
                                   (select   a.pchargeid_chr, sum (a.totalmoney_dec) as chargetotalsum
                                        from t_opr_bih_chargeitementry a, 
                                             t_opr_bih_register b,
                                             t_opr_bih_charge c 
                                       where a.registerid_chr = b.registerid_chr
                                         and a.chargeno_chr = c.chargeno_chr
                                         and b.status_int = 1
                                         and b.feestatus_int = 4
                                         and c.class_int = 3
                                         and b.relateregisterid_chr = ?
                                    group by a.pchargeid_chr) g
                             where a.chargeitemid_chr = b.itemid_chr
                               and a.dayaccountid_chr = c.dayaccountid_chr(+)
                               and a.registerid_chr = d.registerid_chr
                               and a.chargeitemid_chr = e.itemid_chr
                               and b.itemipinvtype_chr = f.typeid_chr
                               and d.paytypeid_chr = e.copayid_chr
                               and a.pchargeid_chr = g.pchargeid_chr(+)
                               and a.pstatus_int in (1,2,3,4)
                               and a.status_int = 1
                               and f.flag_int = 4
                               and a.chargeactive_dat is not null
                               and d.relateregisterid_chr = ?";
                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = p_strRegisterID;
                    ParamArr[1].Value = p_strRegisterID;
                    DataTable dtbTemp = new DataTable();
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtbTemp, ParamArr);
                    if (lngRes > 0 && dtbTemp.Rows.Count > 0)
                    {
                        p_dtbCharge.Merge(dtbTemp);
                    }

                    dtbTemp.Dispose();
                    dtbTemp = null;
                }
                dtbBabyId.Dispose();
                dtbBabyId = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                SQL = null;
            }
            return lngRes;
        }
        #endregion

        #region ����Ӥ��δ����� by yibin.zheng 09-07-03
        /// <summary>
        /// ����Ӥ��δ�����
        /// </summary>
        /// <param name="p_strRegisterId">Ӥ��ID</param>
        /// <param name="p_dtbResult">���ؽ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCheckBabyNoPayCharge(string p_strRegisterId, out DataTable p_dtbResult)
        {
            long lngRes = 0;
            string strSQL = @"select a.pchargeid_chr, a.patientid_chr, a.registerid_chr, a.active_dat,
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
                                   a.chargedoctor_vchr, a.pchargeidorg_chr, b.itemcode_vchr, b.itemopcode_chr, 
                                   b.itemname_vchr, c.orderno_int,
                                   (case a.pstatus_int
                                       when 3
                                          then a.discount_dec
                                       when 4
                                          then a.discount_dec
                                       else e.precent_dec
                                    end
                                   ) as precent_dec,
                                   f.typename_vchr as ipinvoname, b.insuranceid_chr as ybcode,
                                   g.chargetotalsum,a.itemchargetype_vchr,a.totaldiffcostmoney_dec,
                                   DECODE (b.ipchargeflg_int,
                                     1, round (b.tradeprice_mny / b.packqty_dec, 4),
                                     0, b.tradeprice_mny,
                                 round (b.tradeprice_mny / b.packqty_dec, 4)
                                ) tradeprice_mny, a.buyprice_dec 
                              from t_opr_bih_patientcharge a,
                                   t_bse_chargeitem b,
                                   t_opr_bih_dayaccount c,
                                   t_opr_bih_register d,
                                   t_aid_inschargeitem e,
                                   t_bse_chargeitemextype f,
                                   (select   a.pchargeid_chr, sum (a.totalmoney_dec) as chargetotalsum
                                        from t_opr_bih_chargeitementry a, 
                                             t_opr_bih_register b,
                                             t_opr_bih_charge c 
                                       where a.registerid_chr = b.registerid_chr
                                         and a.chargeno_chr = c.chargeno_chr
                                         and b.status_int = 1
                                         and b.feestatus_int = 4
                                         and c.class_int = 3
                                         and a.registerid_chr in ([registerId1])
                                    group by a.pchargeid_chr) g
                             where a.chargeitemid_chr = b.itemid_chr
                               and a.dayaccountid_chr = c.dayaccountid_chr(+)
                               and a.registerid_chr = d.registerid_chr
                               and a.chargeitemid_chr = e.itemid_chr
                               and b.itemipinvtype_chr = f.typeid_chr
                               and d.paytypeid_chr = e.copayid_chr
                               and a.pchargeid_chr = g.pchargeid_chr(+)
                               and a.pstatus_int in(1,2,3)
                               and a.status_int = 1
                               and f.flag_int = 4
                               and a.chargeactive_dat is not null
                               and a.registerid_chr in([registerId2])";
            p_dtbResult = null;
            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                strSQL = strSQL.Replace("[registerId1]", p_strRegisterId);
                strSQL = strSQL.Replace("[registerId2]", p_strRegisterId);
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                strSQL = null;

            }
            return lngRes;

        }
        #endregion

        #region ��ȡĸ�׵�Ӥ��registerID
        /// <summary>
        /// ��ȡĸ�׵�Ӥ��registerID
        /// </summary>
        /// <param name="p_strRegisterId">ĸ��RegisterID</param>
        /// <param name="p_dtbBabyInfo">Ӥ����Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBabyRegisterId(string p_strRegisterId, out DataTable p_dtbBabyInfo)
        {
            long lngRes = 0;
            string strSQL = @"select t.registerid_chr,
       t.patientid_chr,
       t.areaid_chr,
       t.inpatientnotype_int,
       t.paytypeid_chr
  from t_opr_bih_register t
 where t.pstatus_int in (2,3) 
 and t.relateregisterid_chr = ?";
            clsHRPTableService objHRPSvc = null;
            IDataParameter[] ParamArr = null;
            p_dtbBabyInfo = null;
            try
            {
                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = p_strRegisterId;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbBabyInfo, ParamArr);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                objHRPSvc.Dispose();
                objHRPSvc = null;
                strSQL = null;

            }
            return lngRes;
        }
        #endregion

        #region ��ȡ��Ŀ������Ϣ(������ࡢ��Ʊ����)
        /// <summary>
        /// ��ȡ��Ŀ������Ϣ(������ࡢ��Ʊ����)
        /// </summary>
        /// <param name="flag">�������ͣ�1 ������� 2 ���﷢Ʊ 3 סԺ���� 4 סԺ��Ʊ 5 ��������</param>
        /// <param name="dt">
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeItemCat(int flag, out DataTable dt)
        {
            long lngRes = 0;

            string SQL = @" select typeid_chr, typename_vchr, flag_int, usercode_chr, sortcode_int,
                                   govtopcharge_mny, emrcat_vchr 
                              from t_bse_chargeitemextype
                             where flag_int = ? 
                          order by sortcode_int";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = flag;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ����סԺ�Ǽ���ˮ�ż����Ŀ״̬
        /// <summary>
        /// ����סԺ�Ǽ���ˮ�ż����Ŀ״̬
        /// </summary>
        /// <param name="RegID">סԺ�Ǽ���ˮ��</param>
        /// <param name="status">0 ��ȷ�� 1 ���� 2 ���� 3 ���� 4 ֱ��</param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnCheckChargeItemStatus(string RegID, int status)
        {
            long lngRes = 0;
            bool IsExist = false;

            string SQL = @"select count(registerid_chr) from t_opr_bih_patientcharge where registerid_chr = ? and status_int = 1 and pstatus_int = ?";

            DataTable dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = RegID;
                ParamArr[1].Value = status;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);

                if (lngRes > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0][0].ToString()) > 0)
                    {
                        IsExist = true;
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return IsExist;
        }
        #endregion

        #region ����סԺ�Ǽ���ˮ�����ɸ�״̬��Ŀ�ܷ���
        /// <summary>
        /// ����סԺ�Ǽ���ˮ�����ɸ�״̬��Ŀ�ܷ���
        /// </summary>
        /// <param name="RegID">סԺ�Ǽ���ˮ��</param>
        /// <param name="status">0 ��ȷ�� 1 ���� 2 ���� 3 ���� 4 ֱ�� 9 ��������</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetChargeItemFee(string RegID, int status, out DataTable dt)
        {
            long lngRes = 0;

            string SubWhere = "";

            if (status == 9)
            {
                SubWhere = @"and a.pstatus_int <> 0 
                             and a.dayaccountid_chr is null";
            }
            else
            {
                SubWhere = "and a.pstatus_int = ?";
            }

            string SQL = @"select  a.pchargeid_chr, a.patientid_chr, a.registerid_chr, a.active_dat,
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
                                   a.chargedoctor_vchr, a.pchargeidorg_chr, 
                                   (case a.pstatus_int
                                     when 3
                                        then a.discount_dec
                                     when 4
                                        then a.discount_dec
                                     else c.precent_dec
                                  end
                                  ) as precent_dec  
                             from t_opr_bih_patientcharge a,
                                  t_opr_bih_register b,
                                  t_aid_inschargeitem c
                            where a.registerid_chr = ?                                     
                              and a.registerid_chr = b.registerid_chr 
                              and a.chargeitemid_chr = c.itemid_chr 
                              and b.paytypeid_chr = c.copayid_chr 
                              and a.status_int = 1 ";

            SQL += SubWhere;

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                if (status == 9)
                {
                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = RegID;
                }
                else
                {
                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = RegID;
                    ParamArr[1].Value = status;
                }

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="DayAccounts_VO">����VO</param>      
        /// <param name="EmpID">�շ�ԱID</param>     
        /// <param name="ChargeType">0 ��ͨ���� 1 ��Ժ���� 2 ��Ժ����</param>     
        /// <returns></returns>
        [AutoComplete]
        public long m_lngBuildDayAccounts(clsBihDayAccounts_VO DayAccounts_VO, string EmpID, int ChargeType)
        {
            long lngRes = 0, lngAffects = 0;
            string SQL = "";
            string NewOrderNO = "";
            string DayAccountsID = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                DataTable dt = new DataTable();

                //ȡ��������ID
                SQL = "select lpad(seq_dayaccountid.NEXTVAL, 18, '0') from dual";
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
                if (lngRes > 0)
                {
                    DayAccountsID = dt.Rows[0][0].ToString();
                }

                //ȡ�ں�
                SQL = @"select nvl(max(orderno_int),0) + 1
                          from t_opr_bih_dayaccount
                         where registerid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = DayAccounts_VO.RegisterID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                if (lngRes > 0)
                {
                    NewOrderNO = dt.Rows[0][0].ToString();
                }

                if (ChargeType == 2)
                {
                    //��������
                    SQL = @"insert into t_opr_bih_dayaccount (dayaccountid_chr, patientid_chr, registerid_chr, orderno_int, square_dat, charge_dec, clearchg_dec, chgofmepay_dec, clearchgofme_dec, chgofhepay_dec, 
                                                          clearchgofhe_dec, clearmpoptid_chr, chearmp_dat, clearhpoptid_chr, clearhp_dat, areaid_chr, note_vchr, currareaid_chr, operid_chr, type_int) values ( 
                                                          ?, ?, ?, ?, sysdate, ?, ?, ?, ?, ?, ?, null, null, null, null, 'outhosp', ?, ?, ?, ?)";

                    objHRPSvc.CreateDatabaseParameter(14, out ParamArr);
                    ParamArr[0].Value = DayAccountsID;
                    ParamArr[1].Value = DayAccounts_VO.PatientID;
                    ParamArr[2].Value = DayAccounts_VO.RegisterID;
                    ParamArr[3].Value = NewOrderNO;
                    ParamArr[4].Value = DayAccounts_VO.TotalSum;
                    ParamArr[5].Value = DayAccounts_VO.TotalSum;
                    ParamArr[6].Value = DayAccounts_VO.SbSum;
                    ParamArr[7].Value = DayAccounts_VO.SbSum;
                    ParamArr[8].Value = DayAccounts_VO.AcctSum;
                    ParamArr[9].Value = DayAccounts_VO.AcctSum;
                    ParamArr[10].Value = DayAccounts_VO.Note;
                    ParamArr[11].Value = DayAccounts_VO.CurrAreaID;
                    ParamArr[12].Value = DayAccounts_VO.OperID;
                    ParamArr[13].Value = DayAccounts_VO.Type;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                }
                else
                {
                    //��������
                    SQL = @"insert into t_opr_bih_dayaccount (dayaccountid_chr, patientid_chr, registerid_chr, orderno_int, square_dat, charge_dec, clearchg_dec, chgofmepay_dec, clearchgofme_dec, chgofhepay_dec, 
                                                          clearchgofhe_dec, clearmpoptid_chr, chearmp_dat, clearhpoptid_chr, clearhp_dat, areaid_chr, note_vchr, currareaid_chr, operid_chr, type_int) values ( 
                                                          ?, ?, ?, ?, sysdate, ?, ?, ?, ?, ?, ?, null, null, null, null, 'inhosp', ?, ?, ?, ?)";

                    objHRPSvc.CreateDatabaseParameter(14, out ParamArr);
                    ParamArr[0].Value = DayAccountsID;
                    ParamArr[1].Value = DayAccounts_VO.PatientID;
                    ParamArr[2].Value = DayAccounts_VO.RegisterID;
                    ParamArr[3].Value = NewOrderNO;
                    ParamArr[4].Value = DayAccounts_VO.TotalSum;
                    ParamArr[5].Value = 0;
                    ParamArr[6].Value = DayAccounts_VO.SbSum;
                    ParamArr[7].Value = 0;
                    ParamArr[8].Value = DayAccounts_VO.AcctSum;
                    ParamArr[9].Value = 0;
                    ParamArr[10].Value = DayAccounts_VO.Note;
                    ParamArr[11].Value = DayAccounts_VO.CurrAreaID;
                    ParamArr[12].Value = DayAccounts_VO.OperID;
                    ParamArr[13].Value = DayAccounts_VO.Type;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    //�����д���״̬���շ���Ϣ�ĳɴ���״̬
                    SQL = @"update t_opr_bih_patientcharge 
                                                set pstatus_int = 2 
                                              where status_int = 1 
                                                and pstatus_int = 1 
                                                and registerid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = DayAccounts_VO.RegisterID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    if (ChargeType == 1)
                    {
                        //���²��˷���״̬ FEESTATUS_INT = 5 : ��Ժ����
                        SQL = @"update t_opr_bih_register 
                                    set feestatus_int = 5 
                                  where registerid_chr = ?";

                        objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                        ParamArr[0].Value = DayAccounts_VO.RegisterID;

                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                    }
                }

                //�շ���Ϣ������ID
                SQL = @"update t_opr_bih_patientcharge 
                            set dayaccountid_chr = ?,  
                                operator_chr = ?, 
                                modify_dat = sysdate  
                          where status_int = 1 
                            and pstatus_int <> 0 
                            and registerid_chr = ?                           
                            and dayaccountid_chr is null";

                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = DayAccountsID;
                ParamArr[1].Value = EmpID;
                ParamArr[2].Value = DayAccounts_VO.RegisterID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
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

        #region ��Ժ�����������ڳ���λ
        /// <summary>
        /// ��Ժ�����������ڳ���λ
        /// </summary>
        /// <param name="RegID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngClearBed(string RegID)
        {
            long lngRes = 0, lngAffects = 0;
            string SQL = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                //��λ��
                SQL = @"update t_bse_bed set status_int = 1, bihregisterid_chr = '' where bihregisterid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = RegID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
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

        #region ��鷢Ʊ���Ƿ��ظ�
        /// <summary>
        /// ��鷢Ʊ���Ƿ��ظ�
        /// </summary>
        /// <param name="CurrNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnCheckInvoiceNo(string CurrNo)
        {
            long lngRes = 0;
            bool IsExist = false;

            string SQL = @"select invoiceno_vchr as nos 
                             from t_opr_bih_invoice2 
                            where invoiceno_vchr = ? 

                           union all 

                           select repprnbillno_vchr as nos
                             from t_opr_bih_billrepeatprint
                               where billtype_chr = '2' 
                                 and repprnbillno_vchr = ?";

            DataTable dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = CurrNo;
                ParamArr[1].Value = CurrNo;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);

                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    IsExist = true;
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return IsExist;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="DayChrgType">���ʽ������ͣ�1 ���� 2 ��ϸ</param>
        /// <param name="DayAccountsArr"></param>
        /// <param name="Charge_VO"></param>
        /// <param name="ChargeCatArr"></param>
        /// <param name="Invoice_VO"></param>
        /// <param name="InvoCatArr"></param>
        /// <param name="PaymentArr"></param>
        /// <param name="PrePayDeal">Ԥ������ 0 ������ 1 �˻� 2 ת����</param>   
        /// <param name="PrePayIDArr"></param>
        /// <param name="ChargeType">�������ͣ�1 ��;���� 2 ��Ժ���� 3 ���ʽ��� 4 ֱ�� 5 ȷ���շ� 6 ���ʲ��������</param>
        /// <param name="Confirm_VO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngReckoning(DataTable dtSource, int DayChrgType, List<clsBihDayAccounts_VO> DayAccountsArr, clsBihCharge_VO Charge_VO, List<clsBihChargeCat_VO> ChargeCatArr, clsBihInvoice_VO Invoice_VO, List<clsBihInvoiceCat_VO> InvoCatArr, List<clsBihPayment_VO> PaymentArr, int PrePayDeal, List<string> PrePayIDArr, int ChargeType, clsBihConfirm_VO Confirm_VO, out string ChargeNo)
        {
            long lngRes = 0, lngAffects = 0;
            //ִ��SQL
            string SQL = "";
            //����ʱ�����ɽ����
            ChargeNo = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

            try
            {
                #region ������ǰ�Է�Ʊ��������ȡС�����4λ�������ܽ��ͷ���Ϳ������0.01���ʽ��и�Ϊ��ȡ��2λ 2019-11-14

                foreach (clsBihInvoiceCat_VO item in InvoCatArr)
                {
                    // �������������
                    if (item.ItemCatID == "3026") continue;
                    item.TotalSum = Function.Round(item.TotalSum, 2);
                }
                #endregion

                #region ��Ʊ����->ʵ��(������)
                string catId = string.Empty;
                decimal origSum = 0;
                decimal factSum = 0;
                decimal factSumT = 0;
                List<EntityInvoCate> lstInvoCate = new List<EntityInvoCate>();
                foreach (DataRow dr in dtSource.Rows)
                {
                    catId = dr["invcateid_chr"].ToString();
                    origSum = Function.Round(Function.Dec(dr["unitprice_dec"].ToString()) * Function.Dec(dr["amount_dec"].ToString()), 2);
                    factSum = Function.Round(Function.Dec(dr["buyprice_dec"].ToString()) * Function.Dec(dr["amount_dec"].ToString()), 2);
                    if (factSum == 0 && origSum != 0) // ����buyprice_dec = 0 ������
                    {
                        factSum = origSum;
                    }
                    factSumT += factSum;
                    if (lstInvoCate.Any(t => t.catId == catId))
                    {
                        (lstInvoCate.FirstOrDefault(t => t.catId == catId)).catOrigSum += origSum;
                        (lstInvoCate.FirstOrDefault(t => t.catId == catId)).catFactSum += factSum;
                    }
                    else
                    {
                        lstInvoCate.Add(new EntityInvoCate() { catId = catId, catOrigSum = origSum, catFactSum = factSum });
                    }
                }
                decimal diffMny = Invoice_VO.TotalSum - factSumT;
                if (diffMny != 0)
                {
                    for (int i = lstInvoCate.Count - 1; i >= 0; i--)
                    {
                        if (lstInvoCate[i].catFactSum + diffMny > 0)
                        {
                            lstInvoCate[i].catFactSum += diffMny;
                            break;
                        }
                    }
                }
                #endregion

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                //��Ժ�ǼǺ�
                string RegID = Charge_VO.RegisterID;

                //��Ʊ��(����ҵ��Ĭ��һ�ν����Ӧһ�ŷ�Ʊ)
                string Invono = Invoice_VO.InvoiceNo;

                //�����
                SQL = @"insert into t_opr_bih_charge(chargeno_chr, registerid_chr, paytypeid_chr, patienttype_chr, totalsum_mny, sbsum_mny, acctsum_mny, operemp_chr, 
                                                     operdate_dat, recflag_int, recemp_chr, recdate_dat, class_int, type_int, status_int, billno_int, areaid_chr) values (  
                                                     ?, ?, ?, ?, ?, ?, ?, ?, sysdate, 0, null, null, ?, 1, 1, ?, ?)";

                objHRPSvc.CreateDatabaseParameter(11, out ParamArr);
                ParamArr[0].Value = ChargeNo;
                ParamArr[1].Value = Charge_VO.RegisterID;
                ParamArr[2].Value = Charge_VO.PayTypeID;
                ParamArr[3].Value = Charge_VO.PatientType;
                ParamArr[4].Value = Charge_VO.TotalSum;
                ParamArr[5].Value = Charge_VO.SbSum;
                ParamArr[6].Value = Charge_VO.AcctSum;
                ParamArr[7].Value = Charge_VO.OperEmp;
                ParamArr[8].Value = ChargeType;
                ParamArr[9].Value = Charge_VO.BillNO;
                ParamArr[10].Value = Charge_VO.CurrAreaID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //��������
                for (int i = 0; i < ChargeCatArr.Count; i++)
                {
                    clsBihChargeCat_VO ChargeCat_VO = ChargeCatArr[i];

                    if (ChargeCat_VO.TotalSum == 0)
                    {
                        continue;
                    }

                    SQL = @"insert into t_opr_bih_chargecat(chargeno_chr, deptid_chr, itemcatid_chr, totalsum_mny, acctsum_mny) values(  
                                                            ?, ?, ?, ?, ?)";

                    objHRPSvc.CreateDatabaseParameter(5, out ParamArr);
                    ParamArr[0].Value = ChargeNo;
                    ParamArr[1].Value = ChargeCat_VO.DeptID;
                    ParamArr[2].Value = ChargeCat_VO.ItemCatID;
                    ParamArr[3].Value = ChargeCat_VO.TotalSum;
                    ParamArr[4].Value = ChargeCat_VO.AcctSum;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                }

                //���㷢Ʊ��Ӧ��
                SQL = "insert into t_opr_bih_chargedefinv(chargeno_chr, invoiceno_vchr) values(?, ?)";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = ChargeNo;
                ParamArr[1].Value = Invono;
                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //��Ʊ��
                SQL = @"insert into t_opr_bih_invoice2(invoiceno_vchr, invdate_dat, totalsum_mny, sbsum_mny, acctsum_mny, status_int, confirmemp_chr, confirmdate_dat, split_int) values(  
                                                       ?, sysdate, ?, ?, ?, ?, null, null, ?)";

                objHRPSvc.CreateDatabaseParameter(6, out ParamArr);
                ParamArr[0].Value = Invoice_VO.InvoiceNo;
                ParamArr[1].Value = Invoice_VO.TotalSum;
                ParamArr[2].Value = Invoice_VO.SbSum;
                ParamArr[3].Value = Invoice_VO.AcctSum;
                ParamArr[4].Value = Invoice_VO.Status;
                ParamArr[5].Value = Invoice_VO.Split;
                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                // 0812.У�鷢Ʊ�������
                decimal catSum = 0;
                foreach (clsBihInvoiceCat_VO item in InvoCatArr)
                {
                    // �������������
                    if (item.ItemCatID == "3026") continue;
                    catSum += item.TotalSum;
                }
                if (catSum != Invoice_VO.TotalSum)
                {
                    bool isOk = false;
                    diffMny = Invoice_VO.TotalSum - catSum;
                    foreach (clsBihInvoiceCat_VO item in InvoCatArr)
                    {
                        // �������������
                        if (item.ItemCatID == "3026") continue;
                        // ������ҩ�� 2020-06-22
                        if (item.ItemCatID == "3008" && item.TotalSum > 0 && (item.TotalSum + diffMny > 0)) // && ((int)item.TotalSum != item.TotalSum))
                        {
                            item.TotalSum = item.TotalSum + diffMny;
                            isOk = true;
                            break;
                        }
                    }
                    if (isOk == false)
                    {
                        foreach (clsBihInvoiceCat_VO item in InvoCatArr)
                        {
                            // �������������
                            if (item.ItemCatID == "3026") continue;
                            if (item.TotalSum > 0 && (item.TotalSum + diffMny > 0))
                            {
                                item.TotalSum = item.TotalSum + diffMny;
                                break;
                            }
                        }
                    }
                }
                /////////////////////

                //��Ʊ��ϸ��
                for (int i = 0; i < InvoCatArr.Count; i++)
                {
                    clsBihInvoiceCat_VO InvoiceCat_VO = InvoCatArr[i];

                    if (InvoiceCat_VO.TotalSum == 0)
                    {
                        continue;
                    }

                    SQL = "insert into t_opr_bih_invoice2de(invoiceno_vchr, itemcatid_chr, totalsum_mny, acctsum_mny, factsum) values(?, ?, ?, ?, ?)";

                    objHRPSvc.CreateDatabaseParameter(5, out ParamArr);
                    ParamArr[0].Value = Invono;
                    ParamArr[1].Value = InvoiceCat_VO.ItemCatID;
                    ParamArr[2].Value = InvoiceCat_VO.TotalSum;
                    ParamArr[3].Value = InvoiceCat_VO.AcctSum;
                    if (InvoiceCat_VO.ItemCatID == "3026")
                    {
                        ParamArr[4].Value = 0;
                    }
                    else
                    {
                        if (lstInvoCate.Any(t => t.catId == InvoiceCat_VO.ItemCatID))
                        {
                            ParamArr[4].Value = lstInvoCate.FirstOrDefault(t => t.catId == InvoiceCat_VO.ItemCatID).catFactSum;
                        }
                        else
                        {
                            ParamArr[4].Value = InvoiceCat_VO.TotalSum;
                        }
                    }
                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                }

                //֧����
                for (int i = 0; i < PaymentArr.Count; i++)
                {
                    clsBihPayment_VO Payment_VO = PaymentArr[i];

                    SQL = @"insert into t_opr_bih_payment(chargeno_vchr, paytype_int, paycardtype_int, paycardno_vchr, paysum_mny, refusum_mny) values(  
                                                          ?, ?, null, null, ?, ?)";

                    objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                    ParamArr[0].Value = ChargeNo;
                    ParamArr[1].Value = Payment_VO.PayType;
                    ParamArr[2].Value = Payment_VO.PaySum;
                    ParamArr[3].Value = Payment_VO.RefuSum;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                }

                //������ϸ����ID(�鴮)
                string StrChargeID = "";
                List<string> lstPChargeId = new List<string>();

                //������
                decimal RoundingVal = 0;

                //������ϸ����ID
                string rpid = "";

                //���·�����ϸ��¼(��pchargeid_chr ���Ѽ�¼id ��ʱ��Ϊ�����)
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    #region ����
                    string RoundingFlag = dtSource.Rows[i]["des_vchr"].ToString().Trim();
                    if (RoundingFlag.ToLower() == "rounding")
                    {
                        DataTable dt = new DataTable();
                        SQL = "select lpad (seq_pchargeid.nextval, 18, '0') from dual";
                        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
                        if (lngRes > 0)
                        {
                            rpid = dt.Rows[0][0].ToString();
                            dtSource.Rows[i]["pchargeid_chr"] = rpid;
                        }

                        SQL = @"insert into t_opr_bih_patientcharge (patientid_chr, registerid_chr, active_dat, orderid_chr, orderexectype_int, orderexecid_chr,
                                                             clacarea_chr, createarea_chr, calccateid_chr, invcateid_chr, chargeitemid_chr, chargeitemname_chr, unit_vchr,
                                                             unitprice_dec, amount_dec, discount_dec, ismepay_int, des_vchr, createtype_int, creator_chr, create_dat, operator_chr,
                                                             modify_dat, deactivator_chr, deactivate_dat, status_int, pstatus_int, chearaccount_dat, paymoneyid_chr,
                                                             activator_chr, activatetype_int, isrich_int, isconfirmrefundment, refundmentchecker, refundmentdate, bmstatus_int, curareaid_chr,
                                                             curbedid_chr, doctorid_chr, doctor_vchr, doctorgroupid_chr, needconfirm_int, confirmerid_chr, confirmer_vchr, confirm_dat, 
                                                             chargeactive_dat, TotalMoney_dec, AcctMoney_dec, attachorderid_vchr, attachorderbasenum_dec, spec_vchr, dayaccountid_chr, 
                                                             pchargeid_chr, chargedoctorid_chr, chargedoctor_vchr, chargedoctorgroupid_chr,totaldiffcostmoney_dec, buyprice_dec) values (                
                                                            ?, ?, null, ?, ?, null, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 
                                                            ?, ?, sysdate, ?, sysdate, null, null, 1, ?, null, null, null, ?, ?, null, null, null, null, 
                                                            ?, ?, ?, ?, ?, ?, null, null, null, to_date(?, 'yyyy-mm-dd hh24:mi:ss'), ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?, ?)";

                        objHRPSvc.CreateDatabaseParameter(41, out ParamArr);

                        DataRow drR = dtSource.Rows[i];
                        ParamArr[0].Value = drR["patientid_chr"].ToString();
                        ParamArr[1].Value = drR["registerid_chr"].ToString();
                        ParamArr[2].Value = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                        ParamArr[3].Value = drR["orderexectype_int"].ToString();
                        ParamArr[4].Value = drR["clacarea_chr"].ToString();
                        ParamArr[5].Value = drR["createarea_chr"].ToString();
                        ParamArr[6].Value = drR["calccateid_chr"].ToString();
                        ParamArr[7].Value = drR["invcateid_chr"].ToString();
                        ParamArr[8].Value = drR["chargeitemid_chr"].ToString();
                        ParamArr[9].Value = drR["chargeitemname_chr"].ToString();
                        ParamArr[10].Value = drR["unit_vchr"].ToString();
                        ParamArr[11].Value = drR["unitprice_dec"].ToString();
                        ParamArr[12].Value = drR["amount_dec"].ToString();
                        ParamArr[13].Value = drR["discount_dec"].ToString();
                        ParamArr[14].Value = "0";
                        ParamArr[15].Value = drR["des_vchr"].ToString();
                        ParamArr[16].Value = drR["createtype_int"].ToString();
                        ParamArr[17].Value = drR["creator_chr"].ToString();
                        ParamArr[18].Value = drR["activator_chr"].ToString();
                        ParamArr[19].Value = "3";
                        ParamArr[20].Value = drR["activatetype_int"].ToString();
                        ParamArr[21].Value = "0";
                        ParamArr[22].Value = drR["curareaid_chr"].ToString();
                        ParamArr[23].Value = drR["curbedid_chr"].ToString();
                        ParamArr[24].Value = drR["doctorid_chr"].ToString();
                        ParamArr[25].Value = drR["doctor_vchr"].ToString();
                        ParamArr[26].Value = drR["doctorgroupid_chr"].ToString();
                        ParamArr[27].Value = "0";
                        ParamArr[28].Value = drR["active_dat"].ToString();
                        ParamArr[29].Value = drR["totalmoney_dec"].ToString();
                        ParamArr[30].Value = drR["acctmoney_dec"].ToString();
                        ParamArr[31].Value = "";
                        ParamArr[32].Value = "";
                        ParamArr[33].Value = drR["spec_vchr"].ToString();

                        if (DayAccountsArr != null && DayAccountsArr.Count > 0)
                        {
                            ParamArr[34].Value = (DayAccountsArr[0]).AccountsID;
                        }
                        else
                        {
                            ParamArr[34].Value = "";
                        }
                        ParamArr[35].Value = rpid;

                        ParamArr[36].Value = drR["chargedoctorid_chr"].ToString();
                        ParamArr[37].Value = drR["chargedoctor_vchr"].ToString();
                        ParamArr[38].Value = drR["doctorgroupid_chr"].ToString();
                        ParamArr[39].Value = weCare.Core.Utils.Function.Round(weCare.Core.Utils.Function.Dec(drR["totaldiffcostmoney_dec"].ToString()), 2); //�������
                        if (drR["totaldiffcostmoney_dec"] != DBNull.Value && drR["totaldiffcostmoney_dec"].ToString() != "" && Convert.ToDecimal(drR["totaldiffcostmoney_dec"].ToString()) == 0)
                            ParamArr[40].Value = 0;
                        else
                            ParamArr[40].Value = drR["buyprice_dec"].ToString();

                        RoundingVal = decimal.Parse(drR["totalmoney_dec"].ToString());

                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                        if (ChargeType != 6)
                        {
                            StrChargeID += "a.pchargeid_chr = '" + rpid + "' or ";
                            lstPChargeId.Add(rpid);
                        }
                    }
                    #endregion

                    string id = dtSource.Rows[i]["pchargeid_chr"].ToString();
                    decimal totalmoney = Convert.ToDecimal(dtSource.Rows[i]["totalmoney_dec"].ToString());
                    decimal acctmoney = Convert.ToDecimal(dtSource.Rows[i]["acctmoney_dec"].ToString());
                    decimal precent = Convert.ToDecimal(dtSource.Rows[i]["precent_dec"].ToString());
                    decimal decTotalDiffCost = weCare.Core.Utils.Function.Round(weCare.Core.Utils.Function.Dec(dtSource.Rows[i]["totaldiffcostmoney_dec"].ToString()), 2);      // �������
                    decimal buyPrice = weCare.Core.Utils.Function.Dec(dtSource.Rows[i]["buyprice_dec"].ToString());     // �����

                    if (ChargeType == 4)
                    {
                        SQL = @"update t_opr_bih_patientcharge
                                   set pstatus_int = 4,
                                       chearaccount_dat = sysdate, 
                                       chargeactive_dat = sysdate,  
                                       discount_dec = ?, 
                                       paymoneyid_chr = ?,
                                       totalmoney_dec = ?,
                                       acctmoney_dec = ?,
                                       totaldiffcostmoney_dec = ?,
                                       buyprice_dec = ? 
                                 where pchargeid_chr = ?";

                        objHRPSvc.CreateDatabaseParameter(7, out ParamArr);
                        ParamArr[0].Value = precent;
                        ParamArr[1].Value = ChargeNo;
                        ParamArr[2].Value = totalmoney - decTotalDiffCost;
                        ParamArr[3].Value = acctmoney;
                        ParamArr[4].Value = decTotalDiffCost;
                        ParamArr[5].Value = buyPrice;
                        ParamArr[6].Value = id;
                    }
                    else if (ChargeType == 5)
                    {
                        SQL = @"update t_opr_bih_patientcharge
                                   set pstatus_int = 4,                                   
                                       chearaccount_dat = sysdate, 
                                       chargeactive_dat = sysdate,
                                       confirm_dat = sysdate,
                                       confirmerid_chr = ?,
                                       confirmer_vchr = ?,    
                                       discount_dec = ?, 
                                       paymoneyid_chr = ?,
                                       totalmoney_dec = ?,
                                       acctmoney_dec = ?,
                                       totaldiffcostmoney_dec = ?,
                                       buyprice_dec = ?  
                                 where pchargeid_chr = ?";

                        //ȷ���շѸ���ҽ��ִ�е�
                        string ts = @"update t_opr_bih_orderexecute 
                                        set confirm_dat = sysdate, 
                                            confirmerid_chr = ?,
                                            confirmer_vchr = ?  
                                      where needconfirm_int = 1 
                                        and orderexecid_chr = ( select orderexecid_chr 
                                                                  from t_opr_bih_patientcharge
                                                                 where pchargeid_chr = ?)";

                        objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                        ParamArr[0].Value = Confirm_VO.EmpId;
                        ParamArr[1].Value = Confirm_VO.EmpName;
                        ParamArr[2].Value = id;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(ts, ref lngAffects, ParamArr);

                        objHRPSvc.CreateDatabaseParameter(9, out ParamArr);
                        ParamArr[0].Value = Confirm_VO.EmpId;
                        ParamArr[1].Value = Confirm_VO.EmpName;
                        ParamArr[2].Value = precent;
                        ParamArr[3].Value = ChargeNo;
                        ParamArr[4].Value = totalmoney - decTotalDiffCost;
                        ParamArr[5].Value = acctmoney;
                        ParamArr[6].Value = decTotalDiffCost;
                        ParamArr[7].Value = buyPrice;
                        ParamArr[8].Value = id;
                    }
                    else
                    {
                        SQL = @"update t_opr_bih_patientcharge
                                   set pstatus_int = 3,
                                       chearaccount_dat = sysdate,   
                                       discount_dec = ?, 
                                       paymoneyid_chr = ?,
                                       totalmoney_dec = ?,
                                       acctmoney_dec = ?,
                                       totaldiffcostmoney_dec = ?,
                                       buyprice_dec = ?    
                                 where pchargeid_chr = ?";

                        objHRPSvc.CreateDatabaseParameter(7, out ParamArr);
                        ParamArr[0].Value = precent;
                        ParamArr[1].Value = ChargeNo;
                        ParamArr[2].Value = totalmoney - decTotalDiffCost;
                        ParamArr[3].Value = acctmoney;
                        ParamArr[4].Value = decTotalDiffCost;
                        ParamArr[5].Value = buyPrice;
                        ParamArr[6].Value = id;
                    }
                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    StrChargeID += "a.pchargeid_chr = '" + id + "' or ";
                    lstPChargeId.Add(id);
                }

                //���ɽ�����Ŀ��ϸ
                if (ChargeType == 6)
                {
                    string SubStr = "";
                    if (rpid != "")
                    {
                        SQL = @"insert into t_opr_bih_chargeitementry (sid_int, chargeno_chr, operdate_dat, pchargeid_chr, registerid_chr, createarea_chr, clacarea_chr, curareaid_chr, 
                                                                   curbedid_chr, doctorid_chr, doctorgroupid_chr, calccateid_chr, invcateid_chr, chargeitemid_chr, chargeitemname_chr, 
                                                                   unit_vchr, spec_vchr, unitprice_dec, amount_dec, totalmoney_dec, discount_dec, newdiscount_dec, acctmoney_dec, 
                                                                   chargedoctorid_chr, chargedoctorgroupid_chr,totaldiffcostmoney_dec)
                                                            select seq_chargeitementry.NEXTVAL,
                                                                   trim(a.paymoneyid_chr) as chargeno,
                                                                   sysdate,
                                                                   a.pchargeid_chr,
                                                                   a.registerid_chr,
                                                                   a.createarea_chr,  
                                                                   a.clacarea_chr,
                                                                   a.curareaid_chr,
                                                                   a.curbedid_chr,
                                                                   a.doctorid_chr,
                                                                   a.doctorgroupid_chr,
                                                                   a.calccateid_chr,
                                                                   a.invcateid_chr,
                                                                   a.chargeitemid_chr,
                                                                   a.chargeitemname_chr,
                                                                   a.unit_vchr,
                                                                   a.spec_vchr,
                                                                   a.unitprice_dec,
                                                                   a.amount_dec,
                                                                   a.totalmoney_dec,
                                                                   a.discount_dec,
                                                                   a.newdiscount_dec,
                                                                   a.acctmoney_dec,
                                                                   a.chargedoctorid_chr,
                                                                   a.chargedoctorgroupid_chr,
                                                                   a.totaldiffcostmoney_dec 
                                                              from t_opr_bih_patientcharge a
                                                             where trim(a.paymoneyid_chr) = ?
                                                               and a.pchargeid_chr = ?";

                        objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                        ParamArr[0].Value = ChargeNo;
                        ParamArr[1].Value = rpid;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                        SubStr = " and a.pchargeid_chr <> '" + rpid + "'";
                    }

                    if (StrChargeID != "")
                    {
                        SQL = @"insert into t_opr_bih_chargeitementry (sid_int, chargeno_chr, operdate_dat, pchargeid_chr, registerid_chr, createarea_chr, clacarea_chr, curareaid_chr, 
                                                                   curbedid_chr, doctorid_chr, doctorgroupid_chr, calccateid_chr, invcateid_chr, chargeitemid_chr, chargeitemname_chr, 
                                                                   unit_vchr, spec_vchr, unitprice_dec, amount_dec, totalmoney_dec, discount_dec, newdiscount_dec, acctmoney_dec, 
                                                                   chargedoctorid_chr, chargedoctorgroupid_chr,totaldiffcostmoney_dec)
                                                            select seq_chargeitementry.NEXTVAL,
                                                                   trim(a.paymoneyid_chr) as chargeno,
                                                                   sysdate,
                                                                   a.pchargeid_chr,
                                                                   a.registerid_chr,
                                                                   a.createarea_chr,  
                                                                   a.clacarea_chr,
                                                                   a.curareaid_chr,
                                                                   a.curbedid_chr,
                                                                   a.doctorid_chr,
                                                                   a.doctorgroupid_chr,
                                                                   a.calccateid_chr,
                                                                   a.invcateid_chr,
                                                                   a.chargeitemid_chr,
                                                                   a.chargeitemname_chr,
                                                                   a.unit_vchr,
                                                                   a.spec_vchr,
                                                                   a.unitprice_dec,
                                                                   a.amount_dec,
                                                                   (a.totalmoney_dec - nvl(b.totalmoney_dec, 0)) as totalmoney_dec,
                                                                   a.discount_dec,
                                                                   a.newdiscount_dec,
                                                                   round((a.totalmoney_dec - nvl(b.totalmoney_dec, 0)) * (100 - a.discount_dec) / 100, 2) as acctmoney_dec,
                                                                   a.chargedoctorid_chr,
                                                                   a.chargedoctorgroupid_chr,
                                                                   a.totaldiffcostmoney_dec  
                                                              from t_opr_bih_patientcharge a,
                                                                   t_opr_bih_chargeitementry b                                                                    
                                                             where a.pchargeid_chr = b.pchargeid_chr(+) 
                                                               and a.status_int = 1 
                                                               and (a.pstatus_int = 3 or a.pstatus_int = 4)
                                                               and a.chargeactive_dat is not null 
                                                               and trim(a.paymoneyid_chr) = ? 
                                                               and a.registerid_chr = ? " + SubStr;

                        objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                        ParamArr[0].Value = ChargeNo;
                        ParamArr[1].Value = RegID;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                    }
                }
                else
                {
                    if (StrChargeID != "")
                    {
                        StrChargeID = StrChargeID.Trim();
                        StrChargeID = StrChargeID.Substring(0, StrChargeID.Length - 2);

                        if (ChargeType == 2)
                        {
                            SQL = @"insert into t_opr_bih_chargeitementry (sid_int, chargeno_chr, operdate_dat, pchargeid_chr, registerid_chr, createarea_chr, clacarea_chr, curareaid_chr, 
                                                                   curbedid_chr, doctorid_chr, doctorgroupid_chr, calccateid_chr, invcateid_chr, chargeitemid_chr, chargeitemname_chr, 
                                                                   unit_vchr, spec_vchr, unitprice_dec, amount_dec, totalmoney_dec, discount_dec, newdiscount_dec, acctmoney_dec, 
                                                                   chargedoctorid_chr, chargedoctorgroupid_chr,totaldiffcostmoney_dec)
                                                            select seq_chargeitementry.NEXTVAL,
                                                                   trim(a.paymoneyid_chr) as chargeno,
                                                                   sysdate,
                                                                   a.pchargeid_chr,
                                                                   a.registerid_chr,
                                                                   a.createarea_chr,  
                                                                   a.clacarea_chr,
                                                                   a.curareaid_chr,
                                                                   a.curbedid_chr,
                                                                   a.doctorid_chr,
                                                                   a.doctorgroupid_chr,
                                                                   a.calccateid_chr,
                                                                   a.invcateid_chr,
                                                                   a.chargeitemid_chr,
                                                                   a.chargeitemname_chr,
                                                                   a.unit_vchr,
                                                                   a.spec_vchr,
                                                                   a.unitprice_dec,
                                                                   a.amount_dec,
                                                                   a.totalmoney_dec,
                                                                   a.discount_dec,
                                                                   a.newdiscount_dec,
                                                                   a.acctmoney_dec,
                                                                   a.chargedoctorid_chr,
                                                                   a.chargedoctorgroupid_chr,
                                                                   a.totaldiffcostmoney_dec  
                                                              from t_opr_bih_patientcharge a
                                                             where a.status_int = 1 
                                                               and (a.pstatus_int = 3 or a.pstatus_int = 4)
                                                               and a.chargeactive_dat is not null 
                                                               and a.paymoneyid_chr = ?  
                                                              ";// and a.registerid_chr = ? 

                            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                            ParamArr[0].Value = ChargeNo;
                            //  ParamArr[1].Value = RegID;
                            lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                        }
                        else
                        {
                            SQL = @"insert into t_opr_bih_chargeitementry (sid_int, chargeno_chr, operdate_dat, pchargeid_chr, registerid_chr, createarea_chr, clacarea_chr, curareaid_chr, 
                                                                   curbedid_chr, doctorid_chr, doctorgroupid_chr, calccateid_chr, invcateid_chr, chargeitemid_chr, chargeitemname_chr, 
                                                                   unit_vchr, spec_vchr, unitprice_dec, amount_dec, totalmoney_dec, discount_dec, newdiscount_dec, acctmoney_dec, 
                                                                   chargedoctorid_chr, chargedoctorgroupid_chr,totaldiffcostmoney_dec)
                                                            select seq_chargeitementry.NEXTVAL,
                                                                   trim(a.paymoneyid_chr) as chargeno,
                                                                   sysdate,
                                                                   a.pchargeid_chr,
                                                                   a.registerid_chr,
                                                                   a.createarea_chr,  
                                                                   a.clacarea_chr,
                                                                   a.curareaid_chr,
                                                                   a.curbedid_chr,
                                                                   a.doctorid_chr,
                                                                   a.doctorgroupid_chr,
                                                                   a.calccateid_chr,
                                                                   a.invcateid_chr,
                                                                   a.chargeitemid_chr,
                                                                   a.chargeitemname_chr,
                                                                   a.unit_vchr,
                                                                   a.spec_vchr,
                                                                   a.unitprice_dec,
                                                                   a.amount_dec,
                                                                   a.totalmoney_dec,
                                                                   a.discount_dec,
                                                                   a.newdiscount_dec,
                                                                   a.acctmoney_dec,
                                                                   a.chargedoctorid_chr,
                                                                   a.chargedoctorgroupid_chr,
                                                                   a.totaldiffcostmoney_dec  
                                                              from t_opr_bih_patientcharge a
                                                             where trim(a.paymoneyid_chr) = ?
                                                               and " + StrChargeID;



                            SQL = @"insert into t_opr_bih_chargeitementry (sid_int, chargeno_chr, operdate_dat, pchargeid_chr, registerid_chr, createarea_chr, clacarea_chr, curareaid_chr, 
                                                                   curbedid_chr, doctorid_chr, doctorgroupid_chr, calccateid_chr, invcateid_chr, chargeitemid_chr, chargeitemname_chr, 
                                                                   unit_vchr, spec_vchr, unitprice_dec, amount_dec, totalmoney_dec, discount_dec, newdiscount_dec, acctmoney_dec, 
                                                                   chargedoctorid_chr, chargedoctorgroupid_chr,totaldiffcostmoney_dec)
                                                            select seq_chargeitementry.NEXTVAL,
                                                                   trim(a.paymoneyid_chr) as chargeno,
                                                                   sysdate,
                                                                   a.pchargeid_chr,
                                                                   a.registerid_chr,
                                                                   a.createarea_chr,  
                                                                   a.clacarea_chr,
                                                                   a.curareaid_chr,
                                                                   a.curbedid_chr,
                                                                   a.doctorid_chr,
                                                                   a.doctorgroupid_chr,
                                                                   a.calccateid_chr,
                                                                   a.invcateid_chr,
                                                                   a.chargeitemid_chr,
                                                                   a.chargeitemname_chr,
                                                                   a.unit_vchr,
                                                                   a.spec_vchr,
                                                                   a.unitprice_dec,
                                                                   a.amount_dec,
                                                                   a.totalmoney_dec,
                                                                   a.discount_dec,
                                                                   a.newdiscount_dec,
                                                                   a.acctmoney_dec,
                                                                   a.chargedoctorid_chr,
                                                                   a.chargedoctorgroupid_chr,
                                                                   a.totaldiffcostmoney_dec  
                                                              from t_opr_bih_patientcharge a
                                                             where trim(a.paymoneyid_chr) = ?
                                                               and a.pchargeid_chr in ({0})";

                            int num = 0;
                            string sql = string.Empty;
                            List<string> lstSql = new List<string>();
                            foreach (string item in lstPChargeId)
                            {
                                if (num >= 100)
                                {
                                    num = 1;
                                    lstSql.Add(sql);
                                    sql = string.Empty;
                                }
                                else
                                {
                                    num++;
                                }
                                sql += "'" + item + "',";
                            }
                            if (sql != string.Empty) lstSql.Add(sql);

                            foreach (string item in lstSql)
                            {
                                sql = string.Format(SQL, item.TrimEnd(','));

                                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                                ParamArr[0].Value = ChargeNo;
                                lngRes = objHRPSvc.lngExecuteParameterSQL(sql, ref lngAffects, ParamArr);
                            }
                        }
                    }
                }

                //��Ժ�����������һ������
                if (ChargeType == 2 || ChargeType == 6)
                {
                    clsBihDayAccounts_VO DayAccounts_VO = new clsBihDayAccounts_VO();
                    DayAccounts_VO.RegisterID = RegID;
                    DayAccounts_VO.PatientID = Charge_VO.PatientID;
                    DayAccounts_VO.AreaID = Charge_VO.CurrAreaID;

                    //�����ܽ��Ը����ͼ��ʽ��
                    decimal decTotalSum = 0;
                    decimal decSbSum = 0;
                    decimal decAcctSum = 0;

                    DataView dv = new DataView(dtSource);
                    dv.RowFilter = "pstatus_int <> 0 and dayaccountid_chr is null and registerid_chr='" + RegID + "'";

                    foreach (DataRowView drv in dv)
                    {
                        decimal d = decimal.Parse(drv["unitprice_dec"].ToString()) * decimal.Parse(drv["amount_dec"].ToString());
                        decTotalSum += this.Round(d, 2);
                        decSbSum += this.Round(d * decimal.Parse(drv["precent_dec"].ToString()) / 100, 2);
                    }

                    if (decTotalSum > 0)
                    {
                        DayAccounts_VO.TotalSum = decTotalSum;
                        DayAccounts_VO.SbSum = decSbSum;
                        DayAccounts_VO.AcctSum = decAcctSum;

                        lngRes = this.m_lngBuildDayAccounts(DayAccounts_VO, Charge_VO.OperEmp, 2);
                    }

                    RoundingVal = 0;

                    //�ж��Ƿ���Ӥ��
                    if (Charge_VO.m_objBabyInfoVo != null)
                    {
                        //Ӥ�����˲���
                        for (int i2 = 0; i2 < Charge_VO.m_objBabyInfoVo.Length; i2++)
                        {
                            //clsBihDayAccounts_VO DayAccounts_VO = new clsBihDayAccounts_VO();
                            DayAccounts_VO.RegisterID = Charge_VO.m_objBabyInfoVo[i2].RegisterID;
                            DayAccounts_VO.PatientID = Charge_VO.m_objBabyInfoVo[i2].PatientID;
                            DayAccounts_VO.AreaID = Charge_VO.m_objBabyInfoVo[i2].AreaID;

                            //�����ܽ��Ը����ͼ��ʽ��
                            decTotalSum = 0;
                            decSbSum = 0;
                            decAcctSum = 0;

                            dv = new DataView(dtSource);
                            dv.RowFilter = "pstatus_int <> 0 and dayaccountid_chr is null and registerid_chr='" + Charge_VO.m_objBabyInfoVo[i2].RegisterID + "'";

                            foreach (DataRowView drv in dv)
                            {
                                decimal d = decimal.Parse(drv["unitprice_dec"].ToString()) * decimal.Parse(drv["amount_dec"].ToString());
                                decTotalSum += this.Round(d, 2);
                                decSbSum += this.Round(d * decimal.Parse(drv["precent_dec"].ToString()) / 100, 2);
                            }

                            if (decTotalSum > 0)
                            {
                                DayAccounts_VO.TotalSum = decTotalSum;
                                DayAccounts_VO.SbSum = decSbSum;
                                DayAccounts_VO.AcctSum = decAcctSum;

                                lngRes = this.m_lngBuildDayAccounts(DayAccounts_VO, Charge_VO.OperEmp, 2);
                            }

                            RoundingVal = 0;
                        }

                    }
                }

                //��ֱ�ա���ȷ���շѸ�������
                if (ChargeType != 4 && ChargeType != 5)
                {
                    for (int i = 0; i < DayAccountsArr.Count; i++)
                    {
                        clsBihDayAccounts_VO DayAccounts = DayAccountsArr[i];
                        if (DayChrgType == 1)
                        {
                            SQL = @"update t_opr_bih_dayaccount
                                       set charge_dec = ?,
                                           clearchg_dec = ?, 
                                           chgofmepay_dec = ?, 
                                           clearchgofme_dec = ?, 
                                           chgofhepay_dec = ?, 
                                           clearchgofhe_dec = ?, 
                                           clearmpoptid_chr = ?, 
                                           chearmp_dat = sysdate, 
                                           clearhpoptid_chr = ?,       
                                           clearhp_dat = sysdate 
                                     where dayaccountid_chr = ?";

                            objHRPSvc.CreateDatabaseParameter(9, out ParamArr);
                            ParamArr[0].Value = DayAccounts.TotalSum + RoundingVal;
                            ParamArr[1].Value = DayAccounts.TotalSum + RoundingVal;
                            ParamArr[2].Value = DayAccounts.SbSum + RoundingVal;
                            ParamArr[3].Value = DayAccounts.SbSum + RoundingVal;
                            ParamArr[4].Value = DayAccounts.AcctSum;
                            ParamArr[5].Value = DayAccounts.AcctSum;
                            ParamArr[6].Value = DayAccounts.ChargeEmp;
                            ParamArr[7].Value = DayAccounts.ChargeEmp;
                            ParamArr[8].Value = DayAccounts.AccountsID;
                        }
                        else if (DayChrgType == 2)
                        {
                            SQL = @"update t_opr_bih_dayaccount
                                       set charge_dec = ?,
                                           clearchg_dec = nvl(clearchg_dec,0) + ? + ?, 
                                           chgofmepay_dec = ?, 
                                           clearchgofme_dec = nvl(clearchgofme_dec,0) + ?, 
                                           chgofhepay_dec = ?, 
                                           clearchgofhe_dec = nvl(clearchgofhe_dec,0) + ?, 
                                           clearmpoptid_chr = ?, 
                                           chearmp_dat = null, 
                                           clearhpoptid_chr = ?,       
                                           clearhp_dat = null 
                                     where dayaccountid_chr = ?";

                            objHRPSvc.CreateDatabaseParameter(10, out ParamArr);
                            ParamArr[0].Value = DayAccounts.TotalSum + RoundingVal;
                            ParamArr[1].Value = DayAccounts.ClearSbSum + RoundingVal;
                            ParamArr[2].Value = DayAccounts.ClearAcctSum;
                            ParamArr[3].Value = DayAccounts.SbSum + RoundingVal;
                            ParamArr[4].Value = DayAccounts.ClearSbSum + RoundingVal;
                            ParamArr[5].Value = DayAccounts.AcctSum;
                            ParamArr[6].Value = DayAccounts.ClearAcctSum;
                            ParamArr[7].Value = DayAccounts.ChargeEmp;
                            ParamArr[8].Value = DayAccounts.ChargeEmp;
                            ParamArr[9].Value = DayAccounts.AccountsID;
                        }
                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                        RoundingVal = 0;
                    }
                }

                //Ԥ�������
                if (PrePayDeal == 1 && PrePayIDArr != null)
                {
                    for (int i = 0; i < PrePayIDArr.Count; i++)
                    {
                        string prepayid = PrePayIDArr[i].ToString();

                        SQL = @"update t_opr_bih_prepay set isclear_int = 1, chargeno_chr = ? where prepayid_chr = ?";

                        objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                        ParamArr[0].Value = ChargeNo;
                        ParamArr[1].Value = prepayid;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                    }
                }

                /***��Ժ����***/
                if (ChargeType == 2)
                {
                    //��Ժ�ǼǱ�
                    SQL = @"update t_opr_bih_register set pstatus_int = 3, feestatus_int = 3, operatorid_chr = ? where registerid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = Charge_VO.OperEmp;
                    ParamArr[1].Value = Charge_VO.RegisterID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    //��Ժ��¼��
                    SQL = @"update t_opr_bih_leave set modify_dat = sysdate, pstatus_int = 1, operatorid_chr = ? where status_int = 1 and registerid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = Charge_VO.OperEmp;
                    ParamArr[1].Value = Charge_VO.RegisterID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    //��λ��
                    SQL = @"update t_bse_bed set status_int = 1, bihregisterid_chr = '' where bihregisterid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = Charge_VO.RegisterID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    //��ת��Ϣ��
                    SQL = @"insert into t_opr_bih_transfer (transferid_chr, sourcebedid_chr, targetbedid_chr, type_int, des_vchr, operatorid_chr, registerid_chr, 
                                                            modify_dat, sourcedeptid_chr, sourceareaid_chr, targetdeptid_chr, targetareaid_chr)
                                       select LPAD (seq_opr_bih_transfer.NEXTVAL, 12, '0'),
                                              a.sourcebedid_chr,
                                              a.targetbedid_chr,
                                              6,
                                              '', ?,                                               
                                              a.registerid_chr,
                                              sysdate,
                                              a.sourcedeptid_chr,
                                              a.sourceareaid_chr, 
                                              a.targetdeptid_chr, 
                                              a.targetareaid_chr     
                                          from t_opr_bih_transfer a 
                                         where a.transferid_chr = (
                                                                    select max(b.transferid_chr) 
                                                                      from t_opr_bih_transfer b 
                                                                     where b.type_int = 7                                
                                                                       and b.registerid_chr = ?)";

                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = Charge_VO.OperEmp;
                    ParamArr[1].Value = Charge_VO.RegisterID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);


                    #region Ӥ������  by yibin.zheng 09-07-03
                    if (Charge_VO.m_objBabyInfoVo != null)
                    {
                        for (int i3 = 0; i3 < Charge_VO.m_objBabyInfoVo.Length; i3++)
                        {
                            string strBabyRegisterId = Charge_VO.m_objBabyInfoVo[i3].RegisterID;
                            //��Ժ�ǼǱ�
                            SQL = @"update t_opr_bih_register set pstatus_int = 3, feestatus_int = 3 where registerid_chr = ?";

                            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                            //ParamArr[0].Value = Charge_VO.OperEmp;
                            ParamArr[0].Value = strBabyRegisterId;

                            lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                            //��Ժ��¼��
                            SQL = @"update t_opr_bih_leave set modify_dat = sysdate, pstatus_int = 1, operatorid_chr = ? where status_int = 1 and registerid_chr = ?";

                            objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                            ParamArr[0].Value = Charge_VO.OperEmp;
                            ParamArr[1].Value = strBabyRegisterId;

                            lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                            //��λ��
                            SQL = @"update t_bse_bed set status_int = 1, bihregisterid_chr = '' where bihregisterid_chr = ?";

                            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                            ParamArr[0].Value = strBabyRegisterId;

                            lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                            //��ת��Ϣ��
                            SQL = @"insert into t_opr_bih_transfer (transferid_chr, sourcebedid_chr, targetbedid_chr, type_int, des_vchr, operatorid_chr, registerid_chr, 
                                                            modify_dat, sourcedeptid_chr, sourceareaid_chr, targetdeptid_chr, targetareaid_chr)
                                       select LPAD (seq_opr_bih_transfer.NEXTVAL, 12, '0'),
                                              a.sourcebedid_chr,
                                              a.targetbedid_chr,
                                              6,
                                              '', ?,                                               
                                              a.registerid_chr,
                                              sysdate,
                                              a.sourcedeptid_chr,
                                              a.sourceareaid_chr, 
                                              a.targetdeptid_chr, 
                                              a.targetareaid_chr     
                                          from t_opr_bih_transfer a 
                                         where a.transferid_chr = (
                                                                    select max(b.transferid_chr) 
                                                                      from t_opr_bih_transfer b 
                                                                     where b.type_int = 7                                
                                                                       and b.registerid_chr = ?)";

                            objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                            ParamArr[0].Value = Charge_VO.OperEmp;
                            ParamArr[1].Value = strBabyRegisterId;

                            lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);


                        }
                    }
                    #endregion
                }
                /***���ʽ���***/
                else if (ChargeType == 3)
                {

                }
                else if (ChargeType == 4)
                {

                }
                else if (ChargeType == 5)
                {

                }
                /***���ʲ�����-�ݲ�������***/
                else if (ChargeType == 6)
                {
                    //��Ժ�ǼǱ�
                    SQL = @"update t_opr_bih_register set pstatus_int = 3, feestatus_int = 6, operatorid_chr = ? where registerid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = Charge_VO.OperEmp;
                    ParamArr[1].Value = Charge_VO.RegisterID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                }
                else
                {
                    //������Ժ�ǼǱ����״̬ 2 - ��;����
                    SQL = @"update t_opr_bih_register set feestatus_int = 2 where registerid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = Charge_VO.RegisterID;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                }
                /******/
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
        /// ����
        /// </summary>
        /// <param name="Charge_VO"></param>
        /// <param name="ChargeNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngReckoning(clsBihCharge_VO Charge_VO, out string ChargeNo)
        {
            long lngRes = 0, lngAffects = 0;
            //ִ��SQL
            string SQL = "";
            //����ʱ�����ɽ����
            ChargeNo = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                //��Ժ�ǼǺ�
                string RegID = Charge_VO.RegisterID;

                //��Ժ�ǼǱ�
                SQL = @"update t_opr_bih_register set pstatus_int = 3, feestatus_int = 3, operatorid_chr = ? where registerid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = Charge_VO.OperEmp;
                ParamArr[1].Value = Charge_VO.RegisterID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //��Ժ��¼��
                SQL = @"update t_opr_bih_leave set modify_dat = sysdate, pstatus_int = 1, operatorid_chr = ? where status_int = 1 and registerid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = Charge_VO.OperEmp;
                ParamArr[1].Value = Charge_VO.RegisterID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //��λ��
                SQL = @"update t_bse_bed set status_int = 1, bihregisterid_chr = '' where bihregisterid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = Charge_VO.RegisterID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //��ת��Ϣ��
                SQL = @"insert into t_opr_bih_transfer (transferid_chr, sourcebedid_chr, targetbedid_chr, type_int, des_vchr, operatorid_chr, registerid_chr, 
                                                            modify_dat, sourcedeptid_chr, sourceareaid_chr, targetdeptid_chr, targetareaid_chr)
                                       select LPAD (seq_opr_bih_transfer.NEXTVAL, 12, '0'),
                                              a.sourcebedid_chr,
                                              a.targetbedid_chr,
                                              6,
                                              '', ?,                                               
                                              a.registerid_chr,
                                              sysdate,
                                              a.sourcedeptid_chr,
                                              a.sourceareaid_chr, 
                                              a.targetdeptid_chr, 
                                              a.targetareaid_chr     
                                          from t_opr_bih_transfer a 
                                         where a.transferid_chr = (
                                                                    select max(b.transferid_chr) 
                                                                      from t_opr_bih_transfer b 
                                                                     where b.type_int = 7                                
                                                                       and b.registerid_chr = ?)";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = Charge_VO.OperEmp;
                ParamArr[1].Value = Charge_VO.RegisterID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

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

        #region �˿�
        /// <summary>
        /// �˿�
        /// </summary>
        /// <param name="ChargeNo"></param>
        /// <param name="Invono"></param>
        /// <param name="EmpID"></param>
        /// <param name="ChargeType">�������ͣ�1 ��;���� 2 ��Ժ���� 3 ���ʽ��� 4 ֱ�� 5 ȷ���շ� 6 ���ʲ��������</param>
        /// <param name="PayMode"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngRefundment(string ChargeNo, string Invono, string EmpID, int ChargeType, int PayMode)
        {
            long lngRes = 0, lngAffects = 0;
            //ִ��SQL
            string SQL = "";
            //����ʱ�������˿�����
            string RefChargeNo = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                //��������帺��¼
                SQL = @"insert into t_opr_bih_charge(chargeno_chr, registerid_chr, paytypeid_chr, patienttype_chr, totalsum_mny, sbsum_mny, acctsum_mny, operemp_chr, 
                                                     operdate_dat, recflag_int, recemp_chr, recdate_dat, class_int, type_int, status_int, billno_int, areaid_chr)
                                             select ?, 
                                                    registerid_chr,  
                                                    paytypeid_chr,                             
                                                    patienttype_chr,
                                                    -totalsum_mny,
                                                    -sbsum_mny,
                                                    -acctsum_mny, ?, 
                                                    sysdate,
                                                    0,
                                                    null,
                                                    null,
                                                    class_int,
                                                    2,
                                                    1,
                                                    billno_int,
                                                    areaid_chr     
                                               from t_opr_bih_charge 
                                              where chargeno_chr = ?";

                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = RefChargeNo;
                ParamArr[1].Value = EmpID;
                ParamArr[2].Value = ChargeNo;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //������ϸ��帺��¼
                SQL = @"insert into t_opr_bih_chargecat(chargeno_chr, deptid_chr, itemcatid_chr, totalsum_mny, acctsum_mny) 
                                                 select ?, 
                                                        deptid_chr,
                                                        itemcatid_chr,
                                                        -totalsum_mny,
                                                        -acctsum_mny 
                                                   from t_opr_bih_chargecat 
                                                  where chargeno_chr = ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = RefChargeNo;
                ParamArr[1].Value = ChargeNo;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //����-��Ʊ��Ӧ��                
                SQL = "insert into t_opr_bih_chargedefinv(chargeno_chr, invoiceno_vchr) values(?, ?)";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = RefChargeNo;
                ParamArr[1].Value = Invono;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //��Ʊ����״̬���˿��¼����ˡ����ʱ��
                SQL = @"update t_opr_bih_invoice2
                            set status_int = 2,
                                confirmemp_chr = ?,
                                confirmdate_dat = sysdate 
                          where invoiceno_vchr = ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = EmpID;
                ParamArr[1].Value = Invono;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //֧������븺��
                if (PayMode > 0)
                {
                    SQL = @"insert into t_opr_bih_payment(chargeno_vchr, paytype_int, paycardtype_int, paycardno_vchr, paysum_mny, refusum_mny)
                                               select ?, 
                                                      ?,
                                                      null,
                                                      null,    
                                                      sum(-paysum_mny) as paysum_mny,
                                                      sum(-refusum_mny) as refusum_mny 
                                                 from t_opr_bih_payment 
                                                where chargeno_vchr = ? 
                                             group by chargeno_vchr";

                    objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                    ParamArr[0].Value = RefChargeNo;
                    ParamArr[1].Value = PayMode;
                    ParamArr[2].Value = ChargeNo;
                }
                else
                {
                    SQL = @"insert into t_opr_bih_payment(chargeno_vchr, paytype_int, paycardtype_int, paycardno_vchr, paysum_mny, refusum_mny)
                                               select ?, 
                                                      paytype_int,
                                                      paycardtype_int,
                                                      paycardno_vchr,    
                                                      -paysum_mny,
                                                      -refusum_mny 
                                                from  t_opr_bih_payment 
                                               where  chargeno_vchr = ?";

                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = RefChargeNo;
                    ParamArr[1].Value = ChargeNo;
                }
                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //���ɽ�����Ŀ��ϸ              
                SQL = @"insert into t_opr_bih_chargeitementry (sid_int, chargeno_chr, operdate_dat, pchargeid_chr, registerid_chr, createarea_chr, clacarea_chr, curareaid_chr, 
                                                               curbedid_chr, doctorid_chr, doctorgroupid_chr, calccateid_chr, invcateid_chr, chargeitemid_chr, chargeitemname_chr, 
                                                               unit_vchr, spec_vchr, unitprice_dec, amount_dec, totalmoney_dec, discount_dec, newdiscount_dec, acctmoney_dec, 
                                                               chargedoctorid_chr, chargedoctorgroupid_chr)
                                                        select seq_chargeitementry.NEXTVAL,
                                                               ?,
                                                               sysdate,
                                                               pchargeid_chr,
                                                               registerid_chr,
                                                               createarea_chr,  
                                                               clacarea_chr,
                                                               curareaid_chr,
                                                               curbedid_chr,
                                                               doctorid_chr,
                                                               doctorgroupid_chr,
                                                               calccateid_chr,
                                                               invcateid_chr,
                                                               chargeitemid_chr,
                                                               chargeitemname_chr,
                                                               unit_vchr,
                                                               spec_vchr,
                                                               unitprice_dec,
                                                               -amount_dec,
                                                               -totalmoney_dec,
                                                               discount_dec,
                                                               newdiscount_dec,
                                                               -acctmoney_dec,
                                                               chargedoctorid_chr,
                                                               chargedoctorgroupid_chr
                                                          from t_opr_bih_patientcharge 
                                                         where paymoneyid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = RefChargeNo;
                ParamArr[1].Value = ChargeNo;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                if (ChargeType == 4 || ChargeType == 5)
                {
                    //������ϸ���Ϊ��Ч
                    SQL = @"update t_opr_bih_patientcharge
                               set status_int = 0,
                                   chearaccount_dat = null,
                                   paymoneyid_chr = null                                 
                             where paymoneyid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = ChargeNo;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                }
                else
                {
                    //���ʱ�
                    SQL = @"update t_opr_bih_dayaccount ta
                           set (ta.clearchg_dec, ta.clearchgofme_dec, ta.clearchgofhe_dec,
                                ta.chearmp_dat, ta.clearhp_dat) =
                                  (select ta.clearchg_dec - tb.totalmny,
                                          ta.clearchgofme_dec - tb.sbmny,
                                          ta.clearchgofhe_dec - (tb.totalmny - tb.sbmny), null, null
                                     from (select   a.dayaccountid_chr,
                                                    sum (round (a.unitprice_dec * a.amount_dec, 2)
                                                        ) as totalmny,
                                                    sum (round (  a.unitprice_dec
                                                                * a.amount_dec
                                                                * a.discount_dec
                                                                / 100,
                                                                2
                                                               )
                                                        ) as sbmny
                                               from t_opr_bih_patientcharge a
                                              where a.status_int = 1 and a.paymoneyid_chr = ? 
                                           group by a.dayaccountid_chr) tb
                                    where ta.dayaccountid_chr = tb.dayaccountid_chr)
                         where exists (select 1
                                         from (select distinct a.dayaccountid_chr
                                                          from t_opr_bih_patientcharge a
                                                         where a.status_int = 1 and a.paymoneyid_chr = ?) tb
                                        where ta.dayaccountid_chr = tb.dayaccountid_chr)";

                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = ChargeNo;
                    ParamArr[1].Value = ChargeNo;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    //������ϸ���Ϊ����
                    SQL = @"update t_opr_bih_patientcharge
                               set pstatus_int = 2,
                                   chearaccount_dat = null,
                                   paymoneyid_chr = null                                 
                             where paymoneyid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = ChargeNo;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    //�ж��Ƿ��ս�
                    bool IsRec = false;
                    SQL = @"select a.chargeno_chr
                              from t_opr_bih_prepay a, t_opr_bih_charge b
                             where a.chargeno_chr = b.chargeno_chr
                               and b.recflag_int = 1
                               and b.chargeno_chr = ?";

                    DataTable dt = new DataTable();
                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = ChargeNo;

                    lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                    if (lngRes > 0 && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString().Trim() != "")
                        {
                            IsRec = true;
                        }
                    }

                    //�ָ�Ԥ����״̬������->δ��
                    if (IsRec)
                    {
                        SQL = @"update t_opr_bih_prepay set isclear_int = 0, chargeno_chr = null, origchargeno_chr = ? where chargeno_chr = ?";

                        objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                        ParamArr[0].Value = ChargeNo;
                        ParamArr[1].Value = ChargeNo;
                    }
                    else
                    {
                        SQL = @"update t_opr_bih_prepay set isclear_int = 0, chargeno_chr = null, origchargeno_chr = null where chargeno_chr = ?";

                        objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                        ParamArr[0].Value = ChargeNo;
                    }

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                }

                if (ChargeType == 2)
                {
                    //״̬: ��Ժ->Ԥ��Ժ������: ��Ժ����->����
                    SQL = @"update t_opr_bih_register a
                               set a.pstatus_int = 2,
                                   a.feestatus_int = 1 
                             where a.registerid_chr = (
                                                      select b.registerid_chr
                                                        from t_opr_bih_charge b 
                                                       where b.chargeno_chr = ? 
                                                      )";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = ChargeNo;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    //��Ժ��¼�� ��Ժ->Ԥ��Ժ
                    SQL = @"update t_opr_bih_leave a
                               set a.pstatus_int = 0 
                             where a.registerid_chr = (
                                                      select b.registerid_chr
                                                        from t_opr_bih_charge b 
                                                       where b.chargeno_chr = ? 
                                                      )";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = ChargeNo;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                }
                else if (ChargeType == 6)
                {
                    //״̬: ����: ���ʲ��������->���ʽ���
                    SQL = @"update t_opr_bih_register a
                               set a.feestatus_int = 4 
                             where a.registerid_chr = (
                                                      select b.registerid_chr
                                                        from t_opr_bih_charge b 
                                                       where b.chargeno_chr = ? 
                                                      )";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = ChargeNo;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
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

        #region ���ݽ���Ż�ȡ��Ʊ��ϸ��Ϣ
        /// <summary>
        /// ���ݽ���Ż�ȡ��Ʊ��ϸ��Ϣ
        /// </summary>
        /// <param name="ChargeNo">�����</param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <param name="dtPrepay"></param>
        /// <param name="dtPayMode"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInvoiceByChargeNo(string ChargeNo, out DataTable dtMain, out DataTable dtDet, out DataTable dtPrepay, out DataTable dtPayMode, out DataTable dtItemDate)
        {
            long lngRes = 0;
            string SQL = "";

            dtMain = new DataTable();
            dtDet = new DataTable();
            dtPrepay = new DataTable();
            dtPayMode = new DataTable();
            dtItemDate = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                //��Ʊ����Ϣ
                SQL = @"select d.inpatientid_chr, g.deptname_vchr, c.invoiceno_vchr, c.invdate_dat, c.totalsum_mny, c.status_int, c.sbsum_mny, c.acctsum_mny, 
                               e.lastname_vchr,e.sex_chr,e.insuranceid_vchr, d.inpatientcount_int, d.inpatient_dat, h.paytypename_vchr, f.empno_chr, i.outhospital_dat as outpatient_dat, 0 as dbyljzj, 0 as ybjzfpje , '' as bcyltczf1 , '' as bcyltczf2 , '' as bcyltczf3 ,'' as bcyltczf4 ,'' as qtzhifu
                         from t_opr_bih_charge a,
                              t_opr_bih_chargedefinv b,
                              t_opr_bih_invoice2 c,
                              t_opr_bih_register d,
                              t_opr_bih_registerdetail e,
                              t_bse_employee f,
                              t_bse_deptdesc g, 
                              t_bse_patientpaytype h, 
                              (select registerid_chr, outhospital_dat from t_opr_bih_leave where status_int = 1) i                                 
                        where a.chargeno_chr = b.chargeno_chr 
                          and b.invoiceno_vchr = c.invoiceno_vchr 
                          and a.registerid_chr = d.registerid_chr 
                          and a.registerid_chr = e.registerid_chr                           
                          and d.areaid_chr = g.deptid_chr 
                          and a.status_int = 1   
                          and a.operemp_chr = f.empid_chr(+)
                          and d.paytypeid_chr = h.paytypeid_chr(+) 
                          and a.registerid_chr = i.registerid_chr(+)   
                          and g.attributeid = '0000003'                                                      
                          and a.chargeno_chr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ChargeNo;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtMain, ParamArr);

                if (lngRes > 0 && dtMain.Rows.Count > 0)
                {
                    //ĿǰĬ��һ�ν����Ӧһ�ŷ�Ʊ
                    string invono = dtMain.Rows[0]["invoiceno_vchr"].ToString().Trim();

                    //��Ʊ������Ϣ
                    SQL = @"select invoiceno_vchr, itemcatid_chr, 
                                 --  sum(totalsum_mny) as totalsum_mny, 
                                   sum(factsum) as totalsum_mny, 
                                   sum(acctsum_mny) as acctsum_mny 
                              from t_opr_bih_invoice2de 
                             where invoiceno_vchr = ? 
                          group by invoiceno_vchr, itemcatid_chr";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = invono;

                    lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtDet, ParamArr);

                    //Ԥ���������Ϣ
                    SQL = @"select a.chargeno_chr, a.registerid_chr, a.patienttype_chr, a.totalsum_mny,
                                   a.sbsum_mny, a.acctsum_mny, a.operemp_chr, a.operdate_dat,
                                   a.recflag_int, a.recemp_chr, a.recdate_dat, a.class_int, a.type_int,
                                   a.status_int, a.paytypeid_chr, a.billno_int, a.areaid_chr, a.note_vchr, 
                                   b.money_dec, c.paysum_mny 
                              from t_opr_bih_charge a,
                                   t_opr_bih_prepay b,
                                   t_opr_bih_payment c  
                             where a.chargeno_chr = b.chargeno_chr 
                               and a.chargeno_chr = c.chargeno_vchr 
                               and a.status_int = 1          
                               and c.paytype_int = 0 
                               and a.chargeno_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = ChargeNo;

                    lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtPrepay, ParamArr);

                    //֧����ʽ(��Ԥ����)
                    SQL = @"select b.paytype_int, b.paysum_mny 
                              from t_opr_bih_charge a,
                                   t_opr_bih_payment b  
                             where a.chargeno_chr = b.chargeno_vchr 
                               and a.status_int = 1          
                               and b.paytype_int <> 0 
                               and a.chargeno_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = ChargeNo;

                    lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtPayMode, ParamArr);

                    #region Ƕ��ʽ�籣�������

                    SQL = @"select a.zyfyze, a.grzfeije, a.sbzfje, a.ylbz, a.jzje, a.dbyljzj,a.ybjzfpje, a.bcyltczf1,a.bcyltczf2, a.bcyltczf3,a.bcyltczf4,a.qtzhifu
                                  from t_ins_chargezy_csyb a, t_opr_bih_chargedefinv b
                                 where a.invoiceno_vchr = b.invoiceno_vchr
                                   and a.charge_status=1
                                   and b.chargeno_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = ChargeNo;
                    DataTable dtSbDate = new DataTable();
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtSbDate, ParamArr);

                    if (dtSbDate.Rows.Count > 0)
                    {
                        //�� �籣������� = YBJZFPJE + BCYLTCZF1 + BCYLTCZF2 + BCYLTCZF3 + BCYLTCZF4 + QTZHIFU
                        //�Է��ܶ� = GRZFEIJE
                        //�� ҽ�Ʒ����ܶYLFYZE��= YBJZFPJE + BCYLTCZF1 + BCYLTCZF2 + BCYLTCZF3 + BCYLTCZF4 + QTZHIFU+ GRZFEIJE

                        //סԺ�ܽ��
                        decimal decAcct = Convert.ToDecimal(dtSbDate.Rows[0]["zyfyze"].ToString().Trim().Length == 0 ? "0" : dtSbDate.Rows[0]["zyfyze"].ToString().Trim());
                        //�����Էѽ��
                        decimal decZfje = Convert.ToDecimal(dtSbDate.Rows[0]["grzfeije"].ToString().Trim().Length == 0 ? "0" : dtSbDate.Rows[0]["grzfeije"].ToString().Trim());
                        //�ͱ�ҽ�ƾ�����
                        decimal decDbyljzj = Convert.ToDecimal(dtSbDate.Rows[0]["dbyljzj"].ToString().Trim().Length == 0 ? "0" : dtSbDate.Rows[0]["dbyljzj"].ToString().Trim());
                        //����֧�����
                        decimal decQtzhifu = Convert.ToDecimal(dtSbDate.Rows[0]["qtzhifu"].ToString().Trim().Length == 0 ? "0" : dtSbDate.Rows[0]["qtzhifu"].ToString().Trim());
                        //����֧��1
                        decimal decBcyltczf1 = Convert.ToDecimal(dtSbDate.Rows[0]["bcyltczf1"].ToString().Trim().Length == 0 ? "0" : dtSbDate.Rows[0]["bcyltczf1"].ToString().Trim());
                        //����֧��2
                        decimal decBcyltczf2 = Convert.ToDecimal(dtSbDate.Rows[0]["bcyltczf2"].ToString().Trim().Length == 0 ? "0" : dtSbDate.Rows[0]["bcyltczf2"].ToString().Trim());
                        //����֧��3
                        decimal decBcyltczf3 = Convert.ToDecimal(dtSbDate.Rows[0]["bcyltczf3"].ToString().Trim().Length == 0 ? "0" : dtSbDate.Rows[0]["bcyltczf3"].ToString().Trim());
                        //����֧��4
                        decimal decBcyltczf4 = Convert.ToDecimal(dtSbDate.Rows[0]["bcyltczf4"].ToString().Trim().Length == 0 ? "0" : dtSbDate.Rows[0]["bcyltczf4"].ToString().Trim());
                        //ҽ�����˷�Ʊ���
                        decimal decYbjzfpje = Convert.ToDecimal(dtSbDate.Rows[0]["ybjzfpje"].ToString().Trim().Length == 0 ? "0" : dtSbDate.Rows[0]["ybjzfpje"].ToString().Trim());
                        dtMain.Rows[0]["ybjzfpje"] = decYbjzfpje;
                        dtMain.Rows[0]["bcyltczf1"] = decBcyltczf1;
                        dtMain.Rows[0]["bcyltczf2"] = decBcyltczf2;
                        dtMain.Rows[0]["bcyltczf3"] = decBcyltczf3;
                        dtMain.Rows[0]["qtzhifu"] = decQtzhifu;
                        //dtMain.Rows[0]["acctsum_mny"] = decAcct - decZfje - decQtzhifu - decBcyltczf1 - decBcyltczf2 - decBcyltczf3;
                        //�� �籣������� = YBJZFPJE + BCYLTCZF1 + BCYLTCZF2 + BCYLTCZF3 + BCYLTCZF4 + QTZHIFU
                        dtMain.Rows[0]["acctsum_mny"] = decYbjzfpje + decBcyltczf1 + decBcyltczf2 + decBcyltczf3 + decBcyltczf4 + decQtzhifu;
                        dtMain.Rows[0]["dbyljzj"] = decDbyljzj;
                        dtMain.Rows[0]["sbsum_mny"] = Convert.ToDecimal(dtSbDate.Rows[0]["grzfeije"].ToString().Trim().Length == 0 ? "0" : dtSbDate.Rows[0]["grzfeije"].ToString().Trim());
                    }
                    #endregion

                    if (dtMain.Rows[0]["outpatient_dat"].ToString() == "")
                    {
                        //�޳�������
                        SQL = @"select parmvalue_vchr
                                  from t_bse_sysparm 
                                 where status_int = 1 
                                   and parmcode_chr = '0016'";

                        string parmid = "";
                        DataTable dtTmp = new DataTable();
                        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtTmp);
                        if (lngRes > 0)
                        {
                            parmid = dtTmp.Rows[0][0].ToString().Trim();
                        }

                        //������Ŀ��ֹʱ��
                        SQL = @"select (select min(to_char(a.chargeactive_dat, 'yyyy-mm-dd'))
                                          from t_opr_bih_patientcharge a
                                         where a.chargeitemid_chr <> ?
                                           and a.chargeactive_dat is not null
                                           and a.paymoneyid_chr = ?) as mindate,
                                       (select max(to_char(a.chargeactive_dat, 'yyyy-mm-dd'))
                                          from t_opr_bih_patientcharge a
                                         where a.chargeitemid_chr <> ?
                                           and a.chargeactive_dat is not null
                                           and a.paymoneyid_chr = ?) as maxdate
                                  from dual";

                        objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                        ParamArr[0].Value = parmid;
                        ParamArr[1].Value = ChargeNo;
                        ParamArr[2].Value = parmid;
                        ParamArr[3].Value = ChargeNo;

                        lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtItemDate, ParamArr);

                    }
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

        #region ������Ժ�Ǽ���ˮID��ȡ�����𡢴�λ������ʱ��
        /// <summary>
        /// ������Ժ�Ǽ���ˮID��ȡ�����𡢴�λ������ʱ��
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="FinallyDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFinallyDiagFeeDateByRegID(string RegID, out string FinallyDate)
        {
            long lngRes = 0;

            FinallyDate = "";

            string SQL = @"select nvl(max(to_char(a.create_dat, 'yyyy-mm-dd hh24:mi:ss')), '') as finallydate 
                             from t_opr_bih_patientcharge a,
                                  t_bse_bih_specordercate b,
                                  ( select a.chargeitemid_chr
                                      from t_bse_bed a,
                                        (select a.sourcebedid_chr as bedid_chr
                                          from t_opr_bih_transfer a 
                                         where a.transferid_chr = (
                                                                    select max(b.transferid_chr) 
                                                                      from t_opr_bih_transfer b 
                                                                     where b.type_int = 7                                
                                                                       and b.registerid_chr = ? 
                                                                   )) b  
                                     where a.bedid_chr = b.bedid_chr) c
                            where a.status_int = 1                               
                              and (a.chargeitemid_chr = b.autochargeitemtype or a.chargeitemid_chr = c.chargeitemid_chr) 
                              and a.registerid_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = RegID;
                ParamArr[1].Value = RegID;

                DataTable dt = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                if (lngRes > 0)
                {
                    FinallyDate = dt.Rows[0]["finallydate"].ToString();
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

        #region ������Ժ�Ǽ���ˮ�Ż�ȡԤ��Ժʱ��
        /// <summary>
        /// ������Ժ�Ǽ���ˮ�Ż�ȡԤ��Ժʱ��
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="PrepDate"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPrepLHDateByRegID(string RegID, out string PrepDate)
        {
            long lngRes = 0;

            PrepDate = "";

            string SQL = "select to_char(a.outhospital_dat, 'yyyy-mm-dd hh24:mi:ss') as cysj from t_opr_bih_leave a where a.status_int = 1 and a.pstatus_int = 0 and a.registerid_chr = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = RegID;

                DataTable dt = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                if (lngRes > 0)
                {
                    PrepDate = dt.Rows[0]["cysj"].ToString();
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

        #region ������Ժ�Ǽ���ˮID��ȡ��Ʊ����Ϣ
        /// <summary>
        /// ������Ժ�Ǽ���ˮID��ȡ��Ʊ����Ϣ
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="Type">��Ʊ���ͷ�Χ��1 ���� 2 ����+�ش�</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetInvoiceByRegID(string RegID, int Type, out DataTable dt)
        {
            long lngRes = 0;
            string SQL = "";
            dt = new DataTable();

            if (Type == 1)
            {
                SQL = @"select a.invoiceno_vchr, a.invdate_dat, a.totalsum_mny, a.sbsum_mny,
                               a.acctsum_mny, a.status_int, a.confirmemp_chr, a.confirmdate_dat,
                               a.split_int, c.chargeno_chr as chargeno 
                          from t_opr_bih_invoice2 a,
                               t_opr_bih_chargedefinv b,
                               t_opr_bih_charge c
                         where a.invoiceno_vchr = b.invoiceno_vchr 
                           and b.chargeno_chr = c.chargeno_chr 
                           and c.status_int = 1  
                           and c.registerid_chr = ? 
                        order by a.invoiceno_vchr";
            }
            else if (Type == 2)
            {
                SQL = @"select a.status_int as status, a.status_int, a.invoiceno_vchr as invono, '' as sourceinvono, c.chargeno_chr as chargeno 
                          from t_opr_bih_invoice2 a,
                               t_opr_bih_chargedefinv b,
                               t_opr_bih_charge c
                         where a.invoiceno_vchr = b.invoiceno_vchr 
                           and b.chargeno_chr = c.chargeno_chr 
                           and c.status_int = 1      
                           and c.registerid_chr = ?  
                           
                        union all
                           
                        select 999 as status, a.status_int, d.repprnbillno_vchr as invono, d.sourcebillno_vchr as sourceinvono, c.chargeno_chr as chargeno 
                          from t_opr_bih_invoice2 a,
                               t_opr_bih_chargedefinv b,
                               t_opr_bih_charge c,
                               t_opr_bih_billrepeatprint d
                         where a.invoiceno_vchr = b.invoiceno_vchr                          
                           and b.chargeno_chr = c.chargeno_chr 
                           and b.chargeno_chr = d.billid_chr
                           and c.status_int = 1  
                           and c.registerid_chr = ?";   //-- and a.invoiceno_vchr = d.sourcebillno_vchr  
            }

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                if (Type == 1)
                {
                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = RegID;
                }
                else if (Type == 2)
                {
                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = RegID;
                    ParamArr[1].Value = RegID;
                }

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ��ѯ�շ���Ŀ
        /// <summary>
        /// ��ѯ�շ���Ŀ
        /// </summary>
        /// <param name="FindStr">��������</param>
        /// <param name="PatType">�������</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindChargeItem(string FindStr, string PatType, out DataTable dt, bool isChildPrice)
        {
            long lngRes = 0;

            string SQL = @"select a.itemid_chr, a.itemname_vchr, a.itemcode_vchr, a.itempycode_chr,
                                  a.itemwbcode_chr, a.itemsrcid_vchr, a.itemsrctype_int, a.itemspec_vchr,
                                  {0}, a.itemunit_chr, a.itemopunit_chr, a.itemipunit_chr,
                                  a.itemopcalctype_chr, a.itemipcalctype_chr, a.itemopinvtype_chr,
                                  a.itemipinvtype_chr, a.dosage_dec, a.dosageunit_chr, a.isgroupitem_int,
                                  a.itemcatid_chr, a.usageid_chr, a.itemopcode_chr, a.insuranceid_chr,
                                  a.selfdefine_int, a.packqty_dec, a.tradeprice_mny, a.poflag_int,
                                  a.isrich_int, a.opchargeflg_int, a.itemsrcname_vchr,
                                  a.itemsrctypename_vchr, a.itemengname_vchr, a.ifstop_int,
                                  a.pdcarea_vchr, a.ipchargeflg_int, a.insurancetype_vchr,
                                  a.apply_type_int, a.itembihctype_chr, a.defaultpart_vchr,
                                  a.itemchecktype_chr, a.itemcommname_vchr, a.ordercateid_chr,
                                  a.freqid_chr, a.inpinsurancetype_vchr, a.ordercateid1_chr,
                                  a.isselfpay_chr, a.itemprice_mny_old, a.itemprice_mny_new,
                                  a.keepuse_int, a.itemspec_vchr1, 
								  b.ipnoqtyflag_int, b.ifstop_int, c.precent_dec, e.typename_vchr as ybtypename,  
								  {1}, b.putmedtype_int 
							 from t_bse_chargeitem a, 
                                  t_bse_medicine b,
                                  (select itemid_chr, precent_dec from t_aid_inschargeitem where copayid_chr = ?) c, 
                                  t_aid_medicaretype e 
  						    where trim(a.itemsrcid_vchr) = trim(b.medicineid_chr(+))
							  and a.ifstop_int = 0
                              and a.itemid_chr = c.itemid_chr(+) 
                              and a.inpinsurancetype_vchr = e.typeid_chr(+)   
							  and ((lower(a.itemname_vchr) like ?)
								   or (lower(a.itemcode_vchr) like ?)
								   or (lower(a.itempycode_chr) like ?)
								   or (lower(a.itemwbcode_chr) like ?)
								   or (lower(a.itemopcode_chr) like ?))
						 order by a.itemcatid_chr";

            SQL = SQL.Replace("[FindStr]", FindStr.Trim().ToLower());
            if (isChildPrice)
                SQL = string.Format(SQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny", "(case a.ischildprice when 1 then round(a.itemprice_mny * " + EntityChildPrice.AddScale + " / a.packqty_dec, 4) else round(a.itemprice_mny / a.packqty_dec, 4) end) as submoney");
            else
                SQL = string.Format(SQL, "a.itemprice_mny", "round(a.itemprice_mny / a.packqty_dec, 4) as submoney");

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out ParamArr);
                ParamArr[0].Value = PatType;
                ParamArr[1].Value = FindStr.Trim().ToLower() + "%";
                ParamArr[2].Value = FindStr.Trim().ToLower() + "%";
                ParamArr[3].Value = FindStr.Trim().ToLower() + "%";
                ParamArr[4].Value = FindStr.Trim().ToLower() + "%";
                ParamArr[5].Value = FindStr.Trim().ToLower() + "%";

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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
        /// ������ĿID�����շ���Ŀ
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindChargeItem(string ItemID, out DataTable dt, bool isChildPrice)
        {
            long lngRes = 0;

            string SQL = @"select a.itemid_chr, a.itemname_vchr, a.itemcode_vchr, a.itempycode_chr,
                                  a.itemwbcode_chr, a.itemsrcid_vchr, a.itemsrctype_int, a.itemspec_vchr,
                                  {0}, a.itemunit_chr, a.itemopunit_chr, a.itemipunit_chr,
                                  a.itemopcalctype_chr, a.itemipcalctype_chr, a.itemopinvtype_chr,
                                  a.itemipinvtype_chr, a.dosage_dec, a.dosageunit_chr, a.isgroupitem_int,
                                  a.itemcatid_chr, a.usageid_chr, a.itemopcode_chr, a.insuranceid_chr,
                                  a.selfdefine_int, a.packqty_dec, a.tradeprice_mny, a.poflag_int,
                                  a.isrich_int, a.opchargeflg_int, a.itemsrcname_vchr,
                                  a.itemsrctypename_vchr, a.itemengname_vchr, a.ifstop_int,
                                  a.pdcarea_vchr, a.ipchargeflg_int, a.insurancetype_vchr,
                                  a.apply_type_int, a.itembihctype_chr, a.defaultpart_vchr,
                                  a.itemchecktype_chr, a.itemcommname_vchr, a.ordercateid_chr,
                                  a.freqid_chr, a.inpinsurancetype_vchr, a.ordercateid1_chr,
                                  a.isselfpay_chr, a.itemprice_mny_old, a.itemprice_mny_new,
                                  a.keepuse_int, a.itemspec_vchr1, 100 as precent_dec 								 
							 from t_bse_chargeitem a
  						    where a.itemid_chr = ? ";

            if (isChildPrice)
                SQL = string.Format(SQL, "(case a.ischildprice when 1 then (a.itemprice_mny * " + EntityChildPrice.AddScale + ") else a.itemprice_mny end) as itemprice_mny");
            else
                SQL = string.Format(SQL, "a.itemprice_mny");

            dt = new DataTable();
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ItemID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ֱ���շ����ɷ�����ϸ
        /// <summary>
        /// ֱ���շ����ɷ�����ϸ
        /// </summary>
        /// <param name="OrderDicArr">��������Ŀ����</param>
        /// <param name="PatientChargeArr">������ϸ����</param>
        /// <param name="Type">8 ֱ�� 9 ������ 7 ������</param>
        /// <param name="OrderID">���صķ���ID��(ҽ�����ֶ�)</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGenPatientChargeByDir(List<clsBihOrderDic_VO> OrderDicArr, List<clsBihPatientCharge_VO> PatientChargeArr, int Type, ref string OrderID)
        {
            long lngRes = 0, lngAffects = 0;
            //ִ��SQL
            string SQL = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                if (OrderID != "")
                {
                    SQL = @"delete from t_opr_bih_orderdic where orderid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = OrderID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    SQL = @"delete from t_opr_bih_patientcharge where orderid_chr = ? and pstatus_int < 1";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = OrderID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                }
                else
                {
                    //����ʱ�����ɷ���ID��
                    OrderID = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                }

                if (OrderDicArr != null)
                {
                    for (int i = 0; i < OrderDicArr.Count; i++)
                    {
                        clsBihOrderDic_VO OrderDic_VO = OrderDicArr[i];

                        SQL = @"insert into t_opr_bih_orderdic (orderid_int, orderid_chr, type_int, orderque_int, orderdicid_chr, orderdicname_vchr,
                                                            spec_vchr, qty_dec, pricemny_dec, totalmny_dec, attachorderid_vchr, attachorderbasenum_dec, sbbasemny_dec) values (                
                                                            seq_recipeorderid.NEXTVAL, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";


                        objHRPSvc.CreateDatabaseParameter(12, out ParamArr);
                        ParamArr[0].Value = OrderID;
                        ParamArr[1].Value = OrderDic_VO.Type;
                        ParamArr[2].Value = OrderDic_VO.OrderQue;
                        ParamArr[3].Value = OrderDic_VO.OrderDicID;
                        ParamArr[4].Value = OrderDic_VO.OrderDicName;
                        ParamArr[5].Value = OrderDic_VO.Spec;
                        ParamArr[6].Value = OrderDic_VO.Qty;
                        ParamArr[7].Value = OrderDic_VO.PriceMny;
                        ParamArr[8].Value = OrderDic_VO.TotalMny;
                        ParamArr[9].Value = OrderDic_VO.AttachOrderID;
                        ParamArr[10].Value = OrderDic_VO.AttachOrderBaseNum;
                        ParamArr[11].Value = OrderDic_VO.SbBaseMny;

                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                    }
                }

                List<clsBihPatientCharge_VO> lstCheckItem = new List<clsBihPatientCharge_VO>();
                for (int i = 0; i < PatientChargeArr.Count; i++)
                {
                    clsBihPatientCharge_VO PatientCharge_VO = PatientChargeArr[i];
                    // 2020-11-17
                    if (lstCheckItem.Any(t => t.AttachOrderID == PatientCharge_VO.AttachOrderID && t.ChargeItemID == PatientCharge_VO.ChargeItemID && t.Amount == PatientCharge_VO.Amount &&
                                              t.UnitPrice == PatientCharge_VO.UnitPrice))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("�����˳���ͬһ������Ŀ�ظ��շ���Ŀ��");
                        sb.AppendLine("RegisterId:" + PatientCharge_VO.RegisterID + "  OrderId:" + OrderID + "  ChargeItemId:" + PatientCharge_VO.ChargeItemID + "  ChargeItemName:" + PatientCharge_VO.ChargeItemName + "  Amount:" + PatientCharge_VO.Amount.ToString());
                        weCare.Core.Utils.Log.Output(sb.ToString());
                        continue;
                    }
                    else
                    {
                        lstCheckItem.Add(PatientCharge_VO);
                    }

                    SQL = @"insert into t_opr_bih_patientcharge (pchargeid_chr, patientid_chr, registerid_chr, active_dat, orderid_chr, orderexectype_int, orderexecid_chr,
                                                             clacarea_chr, createarea_chr, calccateid_chr, invcateid_chr, chargeitemid_chr, chargeitemname_chr, unit_vchr,
                                                             unitprice_dec, amount_dec, discount_dec, ismepay_int, des_vchr, createtype_int, creator_chr, create_dat, operator_chr,
                                                             modify_dat, deactivator_chr, deactivate_dat, status_int, pstatus_int, chearaccount_dat, dayaccountid_chr, paymoneyid_chr,
                                                             activator_chr, activatetype_int, isrich_int, isconfirmrefundment, refundmentchecker, refundmentdate, bmstatus_int, curareaid_chr,
                                                             curbedid_chr, doctorid_chr, doctor_vchr, doctorgroupid_chr, needconfirm_int, confirmerid_chr, confirmer_vchr, confirm_dat, 
                                                             chargeactive_dat, TotalMoney_dec, AcctMoney_dec, attachorderid_vchr, attachorderbasenum_dec, spec_vchr, putmedicineflag_int, 
                                                             chargedoctorid_chr, chargedoctor_vchr, chargedoctorgroupid_chr,totaldiffcostmoney_dec, buyprice_dec) values (                
                                                            lpad (seq_pchargeid.nextval, 18, '0'), ?, ?, null, ?, ?, null, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 
                                                            to_date(?, 'yyyy-mm-dd hh24:mi:ss'), ?, sysdate, null, null, 1, ?, null, null, null, null, ?, ?, null, null, null, null, 
                                                            ?, ?, ?, ?, ?, ?, null, null, null, to_date(?, 'yyyy-mm-dd hh24:mi:ss'), ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    objHRPSvc.CreateDatabaseParameter(41, out ParamArr);
                    ParamArr[0].Value = PatientCharge_VO.PatientID;
                    ParamArr[1].Value = PatientCharge_VO.RegisterID;
                    ParamArr[2].Value = OrderID;
                    ParamArr[3].Value = Type;
                    ParamArr[4].Value = PatientCharge_VO.ClacArea;
                    ParamArr[5].Value = PatientCharge_VO.CreateArea;
                    ParamArr[6].Value = PatientCharge_VO.CalcCateID;
                    ParamArr[7].Value = PatientCharge_VO.InvCateID;
                    ParamArr[8].Value = PatientCharge_VO.ChargeItemID;
                    ParamArr[9].Value = PatientCharge_VO.ChargeItemName;
                    ParamArr[10].Value = PatientCharge_VO.Unit;
                    ParamArr[11].Value = PatientCharge_VO.UnitPrice;
                    ParamArr[12].Value = PatientCharge_VO.Amount;
                    ParamArr[13].Value = PatientCharge_VO.Discount;
                    ParamArr[14].Value = PatientCharge_VO.Ismepay;
                    ParamArr[15].Value = PatientCharge_VO.Des;
                    ParamArr[16].Value = PatientCharge_VO.CreateType;
                    ParamArr[17].Value = PatientCharge_VO.Creator;
                    if (PatientCharge_VO.CreateDat == null || PatientCharge_VO.CreateDat.Trim() == "")
                    {
                        ParamArr[18].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        ParamArr[18].Value = PatientCharge_VO.CreateDat;
                    }
                    ParamArr[19].Value = PatientCharge_VO.Operator;
                    ParamArr[20].Value = PatientCharge_VO.PStatus;
                    ParamArr[21].Value = PatientCharge_VO.ActivateType;
                    ParamArr[22].Value = PatientCharge_VO.IsRich;
                    ParamArr[23].Value = PatientCharge_VO.CurAreaID;
                    ParamArr[24].Value = PatientCharge_VO.CurBedID;
                    ParamArr[25].Value = PatientCharge_VO.DoctorID;
                    ParamArr[26].Value = PatientCharge_VO.Doctor;
                    ParamArr[27].Value = PatientCharge_VO.DoctorGroupID;
                    ParamArr[28].Value = PatientCharge_VO.NeedConfirm;
                    ParamArr[29].Value = PatientCharge_VO.ActiveDat;
                    ParamArr[30].Value = PatientCharge_VO.TotalMoney_dec;
                    ParamArr[31].Value = PatientCharge_VO.AcctMoney_dec;
                    ParamArr[32].Value = PatientCharge_VO.AttachOrderID;
                    ParamArr[33].Value = PatientCharge_VO.AttachOrderBaseNum;
                    ParamArr[34].Value = PatientCharge_VO.SPEC_VCHR;
                    ParamArr[35].Value = PatientCharge_VO.PutMedicineFlag;
                    ParamArr[36].Value = PatientCharge_VO.CHARGEDOCTORID_CHR;
                    ParamArr[37].Value = PatientCharge_VO.CHARGEDOCTOR_VCHR;
                    ParamArr[38].Value = PatientCharge_VO.CHARGEDOCTORGROUPID_CHR;
                    if (PatientCharge_VO.TotalDiffCostMoney_dec != 0)    //null)
                    {
                        ParamArr[39].Value = Function.Round(PatientCharge_VO.TotalDiffCostMoney_dec, 2);
                        ParamArr[40].Value = PatientCharge_VO.BuyPrice;
                    }
                    else
                    {
                        ParamArr[39].Value = 0;
                        ParamArr[40].Value = 0;
                    }
                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
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

        #region �ύ�����ʷ�����ϸ
        /// <summary>
        /// �ύ�����ʷ�����ϸ
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngCommitPatchCharge(string OrderID, string RegID, string OperID, int Type)
        {
            long lngRes = 0, lngAffects = 0;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                string SQL = @"update t_opr_bih_patientcharge 
                                set chargeactive_dat = sysdate, 		
	                                pstatus_int = 1		                        
                              where	status_int = 1 and orderid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = OrderID;
                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //��ҩ
                List<string> RegIDArr = new List<string>();
                RegIDArr.Add(RegID);
                clsPutMedRequisitionSvc objPut = new clsPutMedRequisitionSvc();
                lngRes = objPut.CreatePutMedDetailByDptType(RegIDArr, OperID, Type);
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

        #region �������˷�
        /// <summary>
        /// �������˷�
        /// </summary>
        /// <param name="ChargeIDArr"></param>        
        /// <param name="EmpID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngPatchRefundment(List<clsBihRefCharge_VO> ChargeIDArr, string EmpID)
        //public long m_lngPatchRefundment(ArrayList ChargeIDArr, string RoundingCode, string EmpID)
        {
            long lngRes = 0, lngAffects = 0;
            //ִ��SQL
            string SQL = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                DataTable dtbTmp = new DataTable();
                int m_intManyReturnMed = 0;
                for (int i = 0; i < ChargeIDArr.Count; i++)
                {
                    string OrderID = DateTime.Now.ToString("yyyyMMddHHmmssffff");

                    clsBihRefCharge_VO Ref_VO = ChargeIDArr[i];
                    SQL = @"select t.pchargeid_chr
                              from t_opr_bih_patientcharge t
                             where t.status_int = 1
                               and t.pchargeidorg_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = Ref_VO.PChargeID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtbTmp, ParamArr);
                    if (lngRes > 0 && dtbTmp.Rows.Count > 0)
                    {
                        m_intManyReturnMed = 1;
                    }
                    //��ѯ�Ƿ��Ѱ�ҩ
                    SQL = @"select t.putmeddetailid_chr, t.isclinicsub 
                              from t_bih_opr_putmeddetail t
                             where t.status_int = 1
                               and t.isput_int = 0
                               and t.pchargeid_chr = ?";
                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = Ref_VO.PChargeID;
                    lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtbTmp, ParamArr);

                    bool isCliniSub = false;
                    int intAuditintMed = 0;//�Ƿ��ҩ�Ͷ����Ƿ���ҩ��ˣ�0Ϊδ��ҩ������ҩ��ˣ�1Ϊ��ҩ״̬δ�����ҩ״̬��
                    if (lngRes > 0 && dtbTmp.Rows.Count > 0)//δ��ҩ
                    {
                        intAuditintMed = 0;
                        // �����ѿۿ��
                        if (dtbTmp.Rows[0]["isclinicsub"] != DBNull.Value && Convert.ToInt32(dtbTmp.Rows[0]["isclinicsub"].ToString()) == 1) isCliniSub = true;
                    }
                    else//�Ѱ�ҩ  (Ԥ��ҩ)
                    {
                        intAuditintMed = 1;
                    }

                    SQL = @"insert into t_opr_bih_patientcharge (pchargeid_chr, patientid_chr, registerid_chr, active_dat, orderid_chr, orderexectype_int, orderexecid_chr,
                                                             clacarea_chr, createarea_chr, calccateid_chr, invcateid_chr, chargeitemid_chr, chargeitemname_chr, unit_vchr,
                                                             unitprice_dec, amount_dec, discount_dec, ismepay_int, des_vchr, createtype_int, creator_chr, create_dat, operator_chr,
                                                             modify_dat, deactivator_chr, deactivate_dat, status_int, pstatus_int, chearaccount_dat, dayaccountid_chr, paymoneyid_chr,
                                                             activator_chr, activatetype_int, isrich_int, isconfirmrefundment, refundmentchecker, refundmentdate, bmstatus_int, curareaid_chr,
                                                             curbedid_chr, doctorid_chr, doctor_vchr, doctorgroupid_chr, needconfirm_int, confirmerid_chr, confirmer_vchr, confirm_dat, 
                                                             chargeactive_dat, TotalMoney_dec, AcctMoney_dec, attachorderid_vchr, attachorderbasenum_dec, spec_vchr, putmedicineflag_int, 
                                                             chargedoctorid_chr, chargedoctor_vchr, chargedoctorgroupid_chr, pchargeidorg_chr,manyreturnmedill_int,returnmedbillno,itemchargetype_vchr,totaldiffcostmoney_dec, buyprice_dec)
                                                      select lpad (seq_pchargeid.nextval, 18, '0'),
                                                             patientid_chr,
                                                             registerid_chr,
                                                             sysdate,
                                                             nvl(orderid_chr, ?),
                                                             9,
                                                             orderexecid_chr,
                                                             clacarea_chr,
                                                             createarea_chr,
                                                             calccateid_chr,                
                                                             invcateid_chr,
                                                             chargeitemid_chr,
                                                             chargeitemname_chr,
                                                             unit_vchr,
                                                             unitprice_dec,
                                                             ? * -1,
                                                             discount_dec,
                                                             ismepay_int,
                                                             des_vchr,
                                                             createtype_int,
                                                             ?,
                                                             sysdate,
                                                             ?,
                                                             sysdate,
                                                             deactivator_chr,
                                                             deactivate_dat,
                                                             status_int,
                                                             1,
                                                             chearaccount_dat,
                                                             null,
                                                             paymoneyid_chr,   
                                                             ?,
                                                             2,                                                           
                                                             isrich_int,
                                                             ?,
                                                             ?,
                                                             sysdate,
                                                             bmstatus_int,
                                                             curareaid_chr,
                                                             curbedid_chr, doctorid_chr, doctor_vchr, doctorgroupid_chr, needconfirm_int, confirmerid_chr, confirmer_vchr, confirm_dat,
                                                             sysdate,
                                                             round(? * unitprice_dec, 2) * -1,                                                             
                                                             0,
                                                             null,
                                                             null,
                                                             spec_vchr, putmedicineflag_int, chargedoctorid_chr, chargedoctor_vchr, chargedoctorgroupid_chr, 
                                                             ? ,?,lpad (seq_returnmedbillno.nextval, 18, '0'),itemchargetype_vchr, round(abs(decode(nvl(totaldiffcostmoney_dec, 0),
                                                                                                                                               0,
                                                                                                                                               0,
                                                                                                                                               decode(nvl(buyprice_dec, 0),
                                                                                                                                                      0,
                                                                                                                                                      totaldiffcostmoney_dec,
                                                                                                                                                      ((unitprice_dec - buyprice_dec) * ?)))),2) as newdiffmoney, buyprice_dec  
                                                        from t_opr_bih_patientcharge 
                                                       where pchargeid_chr = ? ";//,manyreturnmedill_int,returnmedbillno ,?,lpad (seq_returnmedbillno.nextval, 18, '0') //

                    objHRPSvc.CreateDatabaseParameter(12, out ParamArr);
                    ParamArr[0].Value = OrderID;
                    ParamArr[1].Value = Ref_VO.RefAmount;
                    ParamArr[2].Value = EmpID;
                    ParamArr[3].Value = EmpID;
                    ParamArr[4].Value = EmpID;
                    ParamArr[5].DbType = DbType.Int32;
                    ParamArr[5].Value = intAuditintMed;
                    ParamArr[6].Value = EmpID;
                    ParamArr[7].Value = Ref_VO.RefAmount;
                    ParamArr[8].Value = Ref_VO.PChargeID;
                    ParamArr[9].Value = m_intManyReturnMed;
                    ParamArr[10].Value = Ref_VO.RefAmount;
                    ParamArr[11].Value = Ref_VO.PChargeID;
                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    if (intAuditintMed == 0)//δ��ҩ
                    {
                        string strPutmeddetailid = dtbTmp.Rows[0]["putmeddetailid_chr"].ToString();
                        SQL = @"update t_bih_opr_putmeddetail t
                                   set t.status_int = 0
                                 where t.isput_int = 0 
                                   and t.status_int=1
                                   and t.putmeddetailid_chr = ?";
                        objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                        ParamArr[0].Value = strPutmeddetailid;
                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                        if (lngAffects > 0 && Ref_VO.m_blnIsAllRef == false)
                        {
                            SQL = @"insert into t_bih_opr_putmeddetail
                                              (putmeddetailid_chr, areaid_chr, paientid_chr, registerid_chr,
                                               orderid_chr, orderexecid_chr, orderexectype_int, recipeno_int, dosage_dec,
                                               dosageunit_vchr, chargeitemid_chr, medid_chr, medname_vchr, isrich_int,
                                               dosetypeid_chr, execfreqid_chr, exectimes_int, execdays_int,
                                               unitprice_mny, unit_vchr, get_dec, pchargeid_chr, creator_chr, create_dat,
                                               isput_int, puttype_int, putmedreqid_chr, exectime_vchr, pubdate_dat,
                                               needconfirm_int, activatetype_int, isrecruit_int, areaseq_int,
                                               outgetmeddays_int, medstoreid_chr, medicnetype_int, putmedtype_int,
                                               bedid_chr, examreturnmed_int, examreturnmed_dat, status_int, pretestdays, get_dec2 
                                               )
                                              select lpad(seq_putmeddetail.nextval, 18, '0'), a.areaid_chr,
                                                     a.paientid_chr, a.registerid_chr, a.orderid_chr, a.orderexecid_chr,
                                                     a.orderexectype_int, a.recipeno_int, a.dosage_dec,
                                                     a.dosageunit_vchr, a.chargeitemid_chr, a.medid_chr, a.medname_vchr,
                                                     a.isrich_int, a.dosetypeid_chr, a.execfreqid_chr, a.exectimes_int,
                                                     a.execdays_int, a.unitprice_mny, a.unit_vchr, a.get_dec - ?, ?, ?,
                                                     sysdate, a.isput_int, a.puttype_int, a.putmedreqid_chr,
                                                     a.exectime_vchr, a.pubdate_dat, a.needconfirm_int,
                                                     a.activatetype_int, a.isrecruit_int, a.areaseq_int,
                                                     a.outgetmeddays_int, a.medstoreid_chr, a.medicnetype_int,
                                                     a.putmedtype_int, a.bedid_chr, a.examreturnmed_int,
                                                     a.examreturnmed_dat, 1, a.pretestdays, a.get_dec2 
                                                from t_bih_opr_putmeddetail a
                                               where a.status_int = 0
                                                 and a.isput_int = 0
                                                 and a.putmeddetailid_chr = ?
                                                 ";
                            objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                            ParamArr[0].Value = Ref_VO.RefAmount;
                            ParamArr[1].Value = Ref_VO.PChargeID;
                            ParamArr[2].Value = EmpID;
                            ParamArr[3].Value = strPutmeddetailid;
                            lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                        }

                        #region �ָ������ѿۿ��
                        if (isCliniSub)
                        {
                            SQL = @"select a.get_dec - (a.get_dec2 * nvl(a.pretestdays, 0) / (nvl(a.pretestdays, 0) + 1)) as returnamount,
                                           c.medicineid_chr,
                                           c.packqty_dec,
                                           c.ipchargeflg_int,
                                           d.deptid_chr as drugstoreid_chr
                                      from t_bih_opr_putmeddetail a
                                     inner join t_bse_medicine c
                                        on a.medid_chr = c.medicineid_chr
                                     inner join t_bse_medstore d
                                        on a.medstoreid_chr = d.medstoreid_chr
                                     where a.putmeddetailid_chr = ?";

                            DataTable dtPut = new DataTable();
                            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                            ParamArr[0].Value = strPutmeddetailid;
                            objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtPut, ParamArr);
                            if (dtPut != null && dtPut.Rows.Count > 0)
                            {
                                SQL = @"select a.seriesid_int, a.iprealgross_int, a.oprealgross_int
                                          from t_ds_storage_detail a
                                         where a.drugstoreid_chr = ?
                                           and a.medicineid_chr = ?
                                         order by a.validperiod_dat desc";

                                DbType[] dbTypes = new DbType[] { DbType.Double, DbType.Double, DbType.Double, DbType.Double, DbType.Int64 };
                                object[][] objValues = new object[5][];
                                for (int j = 0; j < objValues.Length; j++)
                                {
                                    objValues[j] = new object[dtPut.Rows.Count];
                                }
                                for (int k1 = 0; k1 < dtPut.Rows.Count; k1++)
                                {
                                    if (dtPut.Rows[k1]["ipchargeflg_int"].ToString() == "0")
                                    {
                                        objValues[0][k1] = Math.Abs(Convert.ToDouble(dtPut.Rows[k1]["returnamount"]) * Convert.ToDouble(dtPut.Rows[k1]["packqty_dec"]));
                                        objValues[1][k1] = Math.Abs(Convert.ToDouble(dtPut.Rows[k1]["returnamount"]));
                                        objValues[2][k1] = Math.Abs(Convert.ToDouble(dtPut.Rows[k1]["returnamount"]) * Convert.ToDouble(dtPut.Rows[k1]["packqty_dec"]));
                                        objValues[3][k1] = Math.Abs(Convert.ToDouble(dtPut.Rows[k1]["returnamount"]));
                                    }
                                    else
                                    {
                                        objValues[0][k1] = Math.Abs(Convert.ToDouble(dtPut.Rows[k1]["returnamount"]));
                                        objValues[1][k1] = Math.Abs(Math.Round(Convert.ToDouble(dtPut.Rows[k1]["returnamount"]) / Convert.ToDouble(dtPut.Rows[k1]["packqty_dec"]), 4));
                                        objValues[2][k1] = Math.Abs(Convert.ToDouble(dtPut.Rows[k1]["returnamount"]));
                                        objValues[3][k1] = Math.Abs(Math.Round(Convert.ToDouble(dtPut.Rows[k1]["returnamount"]) / Convert.ToDouble(dtPut.Rows[k1]["packqty_dec"]), 4));
                                    }
                                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                                    ParamArr[0].Value = dtPut.Rows[k1]["drugstoreid_chr"].ToString();
                                    ParamArr[1].Value = dtPut.Rows[k1]["medicineid_chr"].ToString();
                                    objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtbTmp, ParamArr);
                                    objValues[4][k1] = Convert.ToDecimal(dtbTmp.Rows[0]["seriesid_int"]);     // ��油��������Ч�ڵ�һ��ҩƷ
                                }
                                SQL = @"update t_ds_storage_detail a
                                           set a.iprealgross_int      = a.iprealgross_int + ?,
                                               a.oprealgross_int      = a.oprealgross_int + ?,
                                               a.ipavailablegross_num = a.ipavailablegross_num + ?,
                                               a.opavailablegross_num = a.opavailablegross_num + ?
                                         where a.seriesid_int = ?";
                                try
                                {
                                    long lngRecEff = -1;
                                    lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(SQL, objValues, ref lngRecEff, dbTypes);
                                    if (lngRecEff <= 0)
                                        throw new Exception("���²�����ӦҩƷ�����ϸ��");
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
                                    objValues[j] = new object[dtPut.Rows.Count];//��ʼ��
                                }

                                for (int k1 = 0; k1 < dtPut.Rows.Count; k1++)
                                {
                                    if (dtPut.Rows[k1]["ipchargeflg_int"].ToString() == "0")
                                    {
                                        objValues[0][k1] = Math.Abs(Convert.ToDouble(dtPut.Rows[k1]["returnamount"]) * Convert.ToDouble(dtPut.Rows[k1]["packqty_dec"]));
                                        objValues[1][k1] = Math.Abs(Convert.ToDouble(dtPut.Rows[k1]["returnamount"]));
                                    }
                                    else
                                    {
                                        objValues[0][k1] = Math.Abs(Convert.ToDouble(dtPut.Rows[k1]["returnamount"]));
                                        objValues[1][k1] = Math.Abs(Math.Round(Convert.ToDouble(dtPut.Rows[k1]["returnamount"]) / Convert.ToDouble(dtPut.Rows[k1]["packqty_dec"]), 4));
                                    }
                                    objValues[2][k1] = dtPut.Rows[k1]["drugstoreid_chr"].ToString();
                                    objValues[3][k1] = dtPut.Rows[k1]["medicineid_chr"].ToString();
                                }
                                SQL = @"update t_ds_storage a
                                           set a.ipcurrentgross_num = a.ipcurrentgross_num + ?,
                                               a.opcurrentgross_num = a.opcurrentgross_num + ?
                                         where a.drugstoreid_chr = ?
                                           and a.medicineid_chr = ?";
                                try
                                {
                                    long lngRecEff = -1;
                                    lngRes = objHRPSvc.m_lngSaveArrayWithParametersWithAffected(SQL, objValues, ref lngRecEff, dbTypes);
                                    if (lngRecEff <= 0)
                                        throw new Exception("���²�����ӦҩƷ�����棡");
                                }
                                catch (Exception objEx)
                                {
                                    string strTmp = objEx.Message;
                                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                                    bool blnRes = objLogger.LogError(objEx);
                                }
                            }
                        }
                        #endregion
                    }
                }
                #region
                #endregion
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

        #region ��ȡ�����ʡ�ֱ�շ���(������Ŀ)�����¼
        /// <summary>
        /// ��ȡ�����ʡ�ֱ�շ���(������Ŀ)�����¼
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOrderDic(string OrderID, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                string SQL = @" select a.orderid_int, a.orderid_chr, a.type_int, a.orderque_int,
                                       a.orderdicid_chr, a.orderdicname_vchr, a.spec_vchr, a.qty_dec,
                                       a.pricemny_dec, a.totalmny_dec, a.attachorderid_vchr,
                                       a.attachorderbasenum_dec, a.sbbasemny_dec 
                                  from t_opr_bih_orderdic a
                                 where a.orderid_chr = ?                                        
                              order by a.orderque_int ";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = OrderID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ����ҽ��ID(ֱ�շ���ID)��ȡ������ϸ
        /// <summary>
        /// ����ҽ��ID(ֱ�շ���ID)��ȡ������ϸ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientChargeByID(string ID, out DataTable dt)
        {
            long lngRes = 0;

            string SQL = @"select a.pchargeid_chr, a.patientid_chr, a.registerid_chr, a.active_dat,
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
                                   a.chargedoctor_vchr, a.pchargeidorg_chr
                              from t_opr_bih_patientcharge a
                             where a.status_int = 1 
                               and a.activatetype_int = 5 
                               and a.orderid_chr = ?";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ������
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="PatientChargeArr">������ϸ����</param>        
        /// <param name="DayAccountID">����ID</param>                
        /// <returns></returns>
        [AutoComplete]
        public long m_lngPatchDayAccount(List<clsBihPatientCharge_VO> PatientChargeArr, string DayAccountID)
        {
            long lngRes = 0, lngAffects = 0;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                decimal total = 0;
                for (int i = 0; i < PatientChargeArr.Count; i++)
                {
                    clsBihPatientCharge_VO PatientCharge_VO = PatientChargeArr[i];

                    total += this.Round(PatientCharge_VO.Amount * PatientCharge_VO.UnitPrice, 2);
                }

                string SQL = @"update t_opr_bih_dayaccount
                                   set charge_dec = charge_dec + ? 
                                 where dayaccountid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = total;
                ParamArr[1].Value = DayAccountID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                if (lngRes > 0)
                {
                    string orderid = "";
                    lngRes = this.m_lngGenPatientChargeByDir(null, PatientChargeArr, 7, ref orderid);
                    if (lngRes > 0)
                    {
                        SQL = @"update t_opr_bih_patientcharge
                                   set dayaccountid_chr = ?  
                                 where orderid_chr = ?";

                        objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                        ParamArr[0].Value = DayAccountID;
                        ParamArr[1].Value = orderid;

                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                    }
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

        #region ��ȡ�շ���ĿĬ��ִ�еص�
        /// <summary>
        /// ��ȡ�շ���ĿĬ��ִ�еص�
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ApplyAreaID"></param>
        /// <param name="AreaName"></param>
        /// <returns></returns>
        [AutoComplete]
        public string m_strGetChargeItemDefaultExecAreaID(string ItemID, string ApplyAreaID, out string ExecAreaName)
        {
            long lngRes = 0;
            string ExecAreaID = "";

            ExecAreaName = "";

            string SQL = @"select nvl(b.clacarea_chr, '') as areaid1, d1.deptname_vchr as areaname1,
                                  nvl(c.clacarea_chr, '') as areaid2, d2.deptname_vchr as areaname2 
                             from t_bse_chargeitem a,
                                  (select ordercateid_chr, clacarea_chr from t_aid_bih_ocdeptdefault where createarea_chr = ?) b,
                                  t_aid_bih_ocdeptlist c,
                                  t_bse_deptdesc d1,
                                  t_bse_deptdesc d2      
                            where a.ordercateid_chr = b.ordercateid_chr(+)                                                 
                              and a.ordercateid_chr = c.ordercateid_chr(+)
                              and b.clacarea_chr = d1.deptid_chr(+)
                              and c.clacarea_chr = d2.deptid_chr(+)    
                              and rownum = 1                       
                              and a.itemid_chr = ?";

            DataTable dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = ApplyAreaID;
                ParamArr[1].Value = ItemID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    string s1 = dt.Rows[0]["areaid1"].ToString();
                    string s2 = dt.Rows[0]["areaid2"].ToString();

                    if (s1 == "")
                    {
                        if (s2 != "")
                        {
                            ExecAreaID = s2;
                            ExecAreaName = dt.Rows[0]["areaname2"].ToString();
                        }
                    }
                    else
                    {
                        ExecAreaID = s1;
                        ExecAreaName = dt.Rows[0]["areaname1"].ToString();
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return ExecAreaID;
        }
        #endregion

        #region ���淢Ʊ��ӡ����
        /// <summary>
        /// ���淢Ʊ��ӡ����
        /// </summary>
        /// <param name="ChargeItemCatArr">���÷�������VO</param>
        /// <param name="Scope">��Χ: 1 ������� 2 ���﷢Ʊ 3 סԺ���� 4 סԺ��Ʊ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveInvoiceSet(List<clsBihChargeItemCat_VO> ChargeItemCatArr, string Scope)
        {
            long lngRes = 0, lngAffects = 0;
            //ִ��SQL
            string SQL = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                SQL = "delete from t_bse_defchargecat where scope_chr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = Scope;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                for (int i = 0; i < ChargeItemCatArr.Count; i++)
                {
                    clsBihChargeItemCat_VO ChargeItemCat_VO = ChargeItemCatArr[i];

                    SQL = @"insert into t_bse_defchargecat (scope_chr, catid_chr, catname_vchr, type_int, compexp_vchr, dispctl_vchr, prtclt_vchr, status_int) values ( 
                                                            ?, ?, ?, ?, ?, ?, ?, ?)";

                    objHRPSvc.CreateDatabaseParameter(8, out ParamArr);
                    ParamArr[0].Value = ChargeItemCat_VO.Scope;
                    ParamArr[1].Value = ChargeItemCat_VO.CatID;
                    ParamArr[2].Value = ChargeItemCat_VO.CatName;
                    ParamArr[3].Value = ChargeItemCat_VO.Type;
                    ParamArr[4].Value = ChargeItemCat_VO.CompExp;
                    ParamArr[5].Value = ChargeItemCat_VO.DispCtl;
                    ParamArr[6].Value = ChargeItemCat_VO.PrtClt;
                    ParamArr[7].Value = ChargeItemCat_VO.Status;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
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

        #region ��ȡϵͳ����
        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        /// <param name="parmcode">��������</param>
        /// <returns>ֵ</returns>
        [AutoComplete]
        public string m_strGetSysparm(string parmcode)
        {
            string parmvalue = "";
            try
            {
                string SQL = @"select parmvalue_vchr
                                 from t_bse_sysparm 
                                where status_int = 1 
                                  and parmcode_chr = ?";

                DataTable dt = new DataTable();

                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = parmcode;

                long l = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                if (l > 0 && dt.Rows.Count > 0)
                {
                    parmvalue = dt.Rows[0][0].ToString().Trim();
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return parmvalue;
        }
        #endregion

        #region �տ�Ա�ս�(��Ʊ+����)
        /// <summary>
        /// �տ�Ա�ս�(��Ʊ+����)
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="RecDate"></param>
        /// <param name="RemarkInfo"></param>
        /// <param name="RecType">0 ȫ�� 1 ��Ʊ 2 ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDayReckoningUnion(string EmpID, string RecDate, string RemarkInfo, int RecType)
        {
            long ret = 0;

            if (RecType == 0)
            {
                // ��Ʊ
                ret = this.m_lngDayReckoning(EmpID, RecDate, RemarkInfo);
                // ����
                RemarkInfo = "";
                ret = this.m_lngDayReckoningPre(EmpID, RecDate, RemarkInfo);
            }
            else if (RecType == 1)
            {
                // ��Ʊ
                ret = this.m_lngDayReckoning(EmpID, RecDate, RemarkInfo);
            }
            else if (RecType == 2)
            {
                // ����
                ret = this.m_lngDayReckoningPre(EmpID, RecDate, RemarkInfo);
            }

            return ret;
        }
        #endregion

        #region �տ�Ա�ս�(��Ʊ)
        /// <summary>
        /// �տ�Ա�ս�(��Ʊ)
        /// </summary>
        /// <param name="EmpID">�տ�ԱID</param>
        /// <param name="RecDate"></param>
        /// <param name="RemarkInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDayReckoning(string EmpID, string RecDate, string RemarkInfo)
        {
            long lngRes = 0, lngAffects = 0;
            //ִ��SQL            
            string SQL = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                SQL = @"update t_opr_bih_charge a
                                set a.recflag_int = 1,
                                    a.recdate_dat = to_date(?, 'yyyy-mm-dd hh24:mi:ss'), 
                                    a.recemp_chr = ?                               
                             where a.status_int = 1                                  
                               and a.recflag_int = 0 
                               and a.operemp_chr = ?";

                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = RecDate;
                ParamArr[1].Value = EmpID;
                ParamArr[2].Value = EmpID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                if (RemarkInfo.Trim() != "")
                {
                    SQL = "insert into t_opr_bih_ReckoningRemark (operemp_chr, recdate_dat, remark_vchr) values (?, to_date(?, 'yyyy-mm-dd hh24:mi:ss'), ?)";

                    objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                    ParamArr[0].Value = EmpID;
                    ParamArr[1].Value = RecDate;
                    ParamArr[2].Value = RemarkInfo.Trim();

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

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

        #region �տ�Ա�ս�(����)
        /// <summary>
        /// �տ�Ա�ս�(����)
        /// </summary>
        /// <param name="EmpID">�տ�ԱID</param>
        /// <param name="RecDate"></param>      
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDayReckoningPre(string EmpID, string RecDate, string RemarkInfo)
        {
            long lngRes = 0, lngAffects = 0;
            //ִ��SQL            
            string SQL = "";
            //�ս�ID
            string BalanceID = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                DataTable dt = new DataTable();

                //ȡ��������ID
                SQL = "select seq_prepaybalance.NEXTVAL from dual";
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);
                if (lngRes > 0)
                {
                    BalanceID = dt.Rows[0][0].ToString();
                }

                //����ʱ�ע��
                SQL = "insert into t_opr_bih_prepaybalance (balanceid_vchr, balanceemp_chr, balance_dat, remark_vchr) values (?, ?, to_date(?, 'yyyy-mm-dd hh24:mi:ss'), ?)";

                objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                ParamArr[0].Value = BalanceID;
                ParamArr[1].Value = EmpID;
                ParamArr[2].Value = RecDate;
                ParamArr[3].Value = RemarkInfo.Trim();

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //��������
                SQL = @"update t_opr_bih_prepay 
                           set balanceemp_chr = ?, balance_dat = to_date(?, 'yyyy-mm-dd hh24:mi:ss'), balanceid_vchr = ?, balanceflag_int = 1 
                         where balanceflag_int = 0
                           and creatorid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                ParamArr[0].Value = EmpID;
                ParamArr[1].Value = RecDate;
                ParamArr[2].Value = BalanceID;
                ParamArr[3].Value = EmpID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
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

        #region �޸ı�ע��Ϣ
        /// <summary>
        /// �޸ı�ע��Ϣ
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="RecDate"></param>
        /// <param name="RemarkInfo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateDayRecRemark(string EmpID, string RecDate, string RemarkInfo)
        {
            long lngRes = 0, lngAffects = 0;
            //ִ��SQL            
            string SQL = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                SQL = @"delete from t_opr_bih_ReckoningRemark where operemp_chr = ? and to_char(recdate_dat, 'yyyy-mm-dd hh24:mi:ss') = ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = EmpID;
                ParamArr[1].Value = RecDate;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                SQL = "insert into t_opr_bih_ReckoningRemark (operemp_chr, recdate_dat, remark_vchr) values (?, to_date(?, 'yyyy-mm-dd hh24:mi:ss'), ?)";

                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = EmpID;
                ParamArr[1].Value = RecDate;
                ParamArr[2].Value = RemarkInfo.Trim();

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
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

        #region ��ȡ�տ�Ա�ս�ʱ���б�
        /// <summary>
        /// ��ȡ�տ�Ա�ս�ʱ���б�
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDayReckoningTime(string EmpID, string BeginDate, string EndDate, out DataTable dt)
        {
            long lngRes = 0;

            string SQL = @"select distinct recdate
                           from (
                                   select to_char(a.recdate_dat, 'yyyy-mm-dd hh24:mi:ss') as recdate 
                                     from t_opr_bih_charge a 
                                    where a.status_int = 1                                  
                                      and a.recflag_int = 1 
                                      and a.recemp_chr = ? 
                                      and (a.recdate_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')) 

                                   union all

                                   select to_char(a.balance_dat, 'yyyy-mm-dd hh24:mi:ss') as recdate
                                     from t_opr_bih_prepaybalance a 
                                    where a.balanceemp_chr = ?
                                      and (a.balance_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))   
                                 )
                           order by recdate";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(6, out ParamArr);
                ParamArr[0].Value = EmpID;
                ParamArr[1].Value = BeginDate;
                ParamArr[2].Value = EndDate;
                ParamArr[3].Value = EmpID;
                ParamArr[4].Value = BeginDate;
                ParamArr[5].Value = EndDate;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ��¼��ǰ�շ�Աʹ��֮��Ʊ��
        /// <summary>
        /// ��¼��ǰ�շ�Աʹ��֮��Ʊ��
        /// </summary>
        /// <param name="OperID"></param>
        /// <param name="InvoNo"></param>
        /// <param name="Type">���ͣ� 1 סԺ��Ʊ 2 Ѻ�� 3 ���﷢Ʊ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngRegOperInvoNO(string OperID, string InvoNo, int Type)
        {
            long lngRes = 0, lngAffects = 0;
            string SQL = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                bool IsExists = false;
                DataTable dt = new DataTable();

                SQL = @"select count(a.operid_chr) from t_opr_bih_operinvonorecord a where a.operid_chr = ? and a.type_int = ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = OperID;
                ParamArr[1].Value = Type;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);

                if (lngRes > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0][0].ToString()) > 0)
                    {
                        IsExists = true;
                    }
                }

                if (IsExists)
                {
                    SQL = @"update t_opr_bih_operinvonorecord a set a.invono_vchr = ?, a.usedate_dat = sysdate where a.operid_chr = ? and a.type_int = ?";

                    objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                    ParamArr[0].Value = InvoNo;
                    ParamArr[1].Value = OperID;
                    ParamArr[2].Value = Type;
                }
                else
                {
                    SQL = @"insert into t_opr_bih_operinvonorecord (operid_chr, invono_vchr, usedate_dat, type_int) 
                                                            values (?, ?, sysdate, ?)";

                    objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                    ParamArr[0].Value = OperID;
                    ParamArr[1].Value = InvoNo;
                    ParamArr[2].Value = Type;
                }

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
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

        #region ��ȡ��ǰ�շ�Աʹ��֮��Ʊ��
        /// <summary>
        /// ��ȡ��ǰ�շ�Աʹ��֮��Ʊ��
        /// </summary>
        /// <param name="OperID"></param>
        /// <param name="Type">���ͣ� 1 סԺ��Ʊ 2 Ѻ�� 3 ���﷢Ʊ</param>
        /// <param name="InvoNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOperInvoNO(string OperID, int Type, out string InvoNo)
        {
            long lngRes = 0;

            InvoNo = "";

            string SQL = "select a.invono_vchr from t_opr_bih_operinvonorecord a where a.operid_chr = ? and a.type_int = ?";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = OperID;
                ParamArr[1].Value = Type;

                DataTable dt = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                if (lngRes > 0 && dt.Rows.Count > 0)
                {
                    InvoNo = dt.Rows[0]["invono_vchr"].ToString().Trim();
                    if (InvoNo != "")
                    {
                        if (Type == 1)
                        {
                            InvoNo = InvoNo.Substring(0, 2) + Convert.ToString(Convert.ToDecimal(InvoNo.Substring(2)) + 1).PadLeft(8, '0');
                        }
                        //else if (Type == 2)
                        //{
                        //    //�����Ҫ��Ѻ�𵥲��ù�
                        //    InvoNo =  Convert.ToString(int.Parse(InvoNo) + 1);
                        //}
                    }
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

        #region (ҽ������)

        #region (ҽ������)��������
        /// <summary>
        /// (ҽ������)��������
        /// </summary>
        /// <param name="HospCode"></param>
        /// <param name="RegID"></param>        
        /// <param name="Mode">1 ģʽһ��ȫ��δ����Ŀ 2 ģʽ����ָ����Ŀ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngBudgetSendData(string HospCode, string RegID, int Mode)
        {
            long lngRes = 0;
            long lngAffects = 0;
            string SQL = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                if (Mode == 1)
                {
                    SQL = @"delete from ybad10 where trim(zyhm) || to_char(zych) = ( select a.inpatientid_chr || to_char(a.inpatientcount_int)
                                                                                       from t_opr_bih_register a
                                                                                      where a.registerid_chr = ? )";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = RegID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    SQL = @"insert into ybad10 (yydm, zyhm, zych, xinm, xinb, sfzh, csrq, ksdm, ryrq, cyrq, zyts, 
                                                zdfl, zddm, sczt, rylb, dylb, jslx, yyfd, jjfd, ybcs, zfje)
                                        select ?,
                                               a.inpatientid_chr,
                                               a.inpatientcount_int,
                                               b.lastname_vchr,
                                               b.sex_chr,
                                               b.idcard_chr,
                                               b.birth_dat,
                                               a.deptid_chr,
                                               a.inareadate_dat,
                                               sysdate,
                                               floor(sysdate - a.inareadate_dat),
                                               null,
                                               null,
                                               null,
                                               c.jslx,
                                               null,
                                               c.rylb,
                                               100,
                                               nvl(b.insuredpayscale_dec, 100),
                                               b.insuredpaytime_int,
                                               b.insuredpaymoney_mny 
                                          from t_opr_bih_register a,
                                               t_opr_bih_registerdetail b,
                                               t_opr_bih_ybdefpaytype c          
                                         where a.registerid_chr = b.registerid_chr 
                                           and a.paytypeid_chr = c.paytypeid_chr  
                                           and a.registerid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = HospCode;
                    ParamArr[1].Value = RegID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    SQL = @"delete from ybad13 where trim(zyhm) || to_char(zych) = ( select a.inpatientid_chr || to_char(a.inpatientcount_int)
                                                                                       from t_opr_bih_register a
                                                                                      where a.registerid_chr = ? )";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = RegID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    SQL = @"insert into ybad13 ( yydm, zyhm, zych, xmcode, xmdes, xmunt, xmqnt, xmprc, xmamt, trndate, trnflg) 
                                        select ?,
                                               a.inpatientid_chr,
                                               a.inpatientcount_int,
                                               c.itemcode_vchr,
                                               max(substr(c.itemname_vchr,0,15)),
                                               max(b.unit_vchr),
                                               sum(b.amount_dec),
                                               max(b.unitprice_dec),
                                               sum(round(b.amount_dec * b.unitprice_dec, 2)),
                                               sysdate,
                                               '0'        
                                          from t_opr_bih_register a,
                                               t_opr_bih_patientcharge b,
                                               t_bse_chargeitem c 
                                         where a.registerid_chr = b.registerid_chr 
                                           and b.chargeitemid_chr = c.itemid_chr 
                                           and length(c.itemcode_vchr) <= 20 
                                           and (b.pstatus_int = 1 or b.pstatus_int = 2)
                                           and a.registerid_chr = ? 
                                      group by a.inpatientid_chr, a.inpatientcount_int, c.itemcode_vchr";

                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = HospCode;
                    ParamArr[1].Value = RegID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                }
                else if (Mode == 2)
                {
                    //��д
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

        #region (ҽ������)��������
        /// <summary>
        /// (ҽ������)��������
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngBudgetGetData(string RegID, out DataTable dtMain, out DataTable dtDet)
        {
            long lngRes = 0;
            //long lngAffects = 0;
            string SQL = "";

            dtMain = new DataTable();
            dtDet = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                SQL = @"select zl19, yb02 from ybad11 where trim(zyhm) || to_char(zych) = ( select a.inpatientid_chr || to_char(a.inpatientcount_int)
                                                                                              from t_opr_bih_register a
                                                                                             where a.registerid_chr = ? )";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = RegID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtMain, ParamArr);

                //                SQL = @"select * from ybad12 where trim(zyhm) || to_char(zych) = ( select a.inpatientid_chr || to_char(a.inpatientcount_int)
                //                                                                                     from t_opr_bih_register a
                //                                                                                    where a.registerid_chr = ? )";

                //                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                //                ParamArr[0].Value = RegID;

                //                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtDet, ParamArr);

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

        #endregion

        #region (ҽ��)����ҽ��ǰ�û�����->���ɵ�����
        /// <summary>
        /// (ҽ��)����ҽ��ǰ�û�����->���ɵ�����
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDownloadYBData(DataTable dt)
        {
            long lngRes = 0, lngAffects = 0;
            //ִ��SQL
            string SQL = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    SQL = @"insert into t_tmp_mashxm (hos_code, zyno, zysno, xmcode, xmdes, xmunt, xmqnt, 
                                                xmprc, xmamt, trndate, trnflag, memoa, u_version) values (
                                                ?, ?, ?, ?, ?, ?, ?, ?, ?, to_date(?, 'yyyy-mm-dd'), ?, ?, ?)";

                    objHRPSvc.CreateDatabaseParameter(13, out ParamArr);
                    ParamArr[0].Value = dr["hos_code"].ToString();
                    ParamArr[1].Value = dr["zyno"].ToString();
                    ParamArr[2].Value = dr["zysno"].ToString();
                    ParamArr[3].Value = dr["xmcode"].ToString();
                    ParamArr[4].Value = dr["xmdes"].ToString();
                    ParamArr[5].Value = dr["xmunt"].ToString();
                    ParamArr[6].Value = dr["xmqnt"].ToString();
                    ParamArr[7].Value = dr["xmprc"].ToString();
                    ParamArr[8].Value = dr["xmamt"].ToString();
                    ParamArr[9].Value = dr["trndate"].ToString();
                    ParamArr[10].Value = dr["trnflag"].ToString();
                    ParamArr[11].Value = dr["memoa"].ToString();
                    ParamArr[12].Value = dr["u_version"].ToString();

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
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

        #region (ҽ��)ɾ��������ҽ��ǰ�û�����
        /// <summary>
        /// (ҽ��)ɾ��������ҽ��ǰ�û�����
        /// </summary>
        /// <param name="Zyh"></param>
        /// <param name="Zycs"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDelDownloadYBData(string Zyh, int Zycs)
        {
            long lngRes = 0, lngAffects = 0;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                //ִ��SQL           
                string SQL = @"delete from t_tmp_mashxm where zyno = ? and zysno = ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = Zyh;
                ParamArr[1].Value = Zycs;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
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

        #region (ҽ��)��ȡ������ҽ��ǰ�û�����
        /// <summary>
        /// (ҽ��)��ȡ������ҽ��ǰ�û�����
        /// </summary>
        /// <param name="Zyh"></param>
        /// <param name="Zycs"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDownloadYBData(string Zyh, int Zycs, out DataTable dt)
        {
            long lngRes = 0;
            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                //ִ��SQL           
                string SQL = @"select * from t_tmp_mashxm where zyno = ? and zysno = ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = Zyh;
                ParamArr[1].Value = Zycs;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region (ҽ��)����ҽ��ͳ�����
        /// <summary>
        /// (ҽ��)����ҽ��ͳ�����
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="InsuredSum"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngUpdateInsuredSum(string RegID, decimal InsuredSum)
        {
            long lngRes = 0, lngAffects = 0;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                //ִ��SQL           
                string SQL = @"update t_opr_bih_register a set a.insuredsum_mny = ? where a.registerid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = InsuredSum;
                ParamArr[1].Value = RegID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
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

        #region ������ת��Ϊ����
        /// <summary>
        /// ������ת��Ϊ����
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public decimal ConvertObjToDecimal(object obj)
        //{
        //    try
        //    {
        //        if (obj != null && obj.ToString() != "")
        //        {
        //            return ConvertObjToDecimal(obj.ToString());

        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}
        #endregion

        #region ����ֵ��������
        /// <summary>
        /// ����ֵ��������
        /// </summary>
        /// <param name="d">��ֵ</param>
        /// <param name="decimals">С��λ��</param>
        /// <returns></returns>
        public decimal Round(decimal d, int decimals)
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

        #region ��ȡ��ͬ�ѱ�ķ�����ϸ
        /// <summary>
        /// ��ȡ��ͬ�ѱ�ķ�����ϸ
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="PayTypeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientFeeDetByPayType(string RegID, string PayTypeID, out DataTable dt)
        {
            long lngRes = 0;
            string SQL = @"select  a.pchargeid_chr, a.patientid_chr, a.registerid_chr, a.active_dat,
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
                                   a.chargedoctor_vchr, a.pchargeidorg_chr, 
                                   d.deptname_vchr as curarea, e.itemcode_vchr, e.itemspec_vchr, e.insuranceid_chr as ybcode, 
                                   (case a.pstatus_int
                                       when 3
                                          then a.discount_dec
                                       when 4
                                          then a.discount_dec
                                       else c.precent_dec
                                    end
                                  ) as precent_dec, round(a.amount_dec * a.unitprice_dec, 2) as totalmony
                             from t_opr_bih_patientcharge a,
                                  t_opr_bih_register b,
                                  t_aid_inschargeitem c,
                                  t_bse_deptdesc d,                                           
                                  t_bse_chargeitem e                                     
                            where a.registerid_chr = b.registerid_chr 
                              and a.chargeitemid_chr = c.itemid_chr                               
                              and a.curareaid_chr = d.deptid_chr(+)
                              and a.chargeitemid_chr = e.itemid_chr(+)
                              and a.status_int = 1 
                              and a.pstatus_int <> 0 
                              and a.chargeactive_dat is not null 
                              and a.registerid_chr = ?  
                              and c.copayid_chr = ? 
                         order by a.chargeitemname_chr";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = RegID;
                ParamArr[1].Value = PayTypeID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ������ϸ��Ӧ֮������Ŀ
        /// <summary>
        /// ������ϸ��Ӧ֮������Ŀ
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="ActiveType">��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ���շ�;888=����״̬����;999=ȫ��}</param>
        /// <param name="Pstatus"></param>
        /// <param name="AreaID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFeeDiagItem(string RegID, int ActiveType, string Pstatus, string AreaID, string BeginDate, string EndDate, out DataTable dt)
        {
            long lngRes = 0;
            string SQL = "";
            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                if (ActiveType == 999)
                {
                    if (AreaID == "00")
                    {
                        SQL = @"select distinct to_char (b.chargeactive_dat, 'yyyymmdd') as orderdate, b.orderexectype_int as ordertype,
                                                a.name_vchr as ordername, b.orderid_chr as orderid, b.orderexecid_chr as orderexecid,
                                                c.ordercateid_chr as ordercateid, c.pycode_chr as orderpycode, '#1' as orderflag 
                                           from t_opr_bih_order a,
                                                t_opr_bih_patientcharge b,
                                                t_bse_bih_orderdic c
                                          where a.orderid_chr = b.orderid_chr
                                            and a.orderdicid_chr = c.orderdicid_chr(+) 
                                            and (b.orderexectype_int in (1, 2, 3, 4)) 
                                            and b.status_int = 1
                                            and b.pstatus_int <> 0
                                            and b.registerid_chr = ? 
                                            and (b.chargeactive_dat 
                                                        between to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                                            and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                union all
                                select distinct to_char (b.chargeactive_dat, 'yyyymmdd') as orderdate, b.orderexectype_int as ordertype,
                                                a.orderdicname_vchr as ordername, b.orderid_chr as orderid, b.attachorderid_vchr as orderexecid,
                                                c.ordercateid_chr as ordercateid, c.pycode_chr as orderpycode, '#2' as orderflag 
                                           from t_opr_bih_orderdic a, 
                                                t_opr_bih_patientcharge b,
                                                t_bse_bih_orderdic c
                                          where a.orderid_chr = b.orderid_chr
                                            and a.orderdicid_chr = c.orderdicid_chr
                                            and b.status_int = 1
                                            and b.pstatus_int <> 0 
                                            and b.attachorderid_vchr is not null    
                                            and b.registerid_chr = ? 
                                            and (b.chargeactive_dat 
                                                        between to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                                            and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                union all
                                select to_char (b.chargeactive_dat, 'yyyymmdd') as orderdate, b.orderexectype_int as ordertype,
                                                b.chargeitemname_chr as ordername, b.orderid_chr as orderid, b.pchargeid_chr as orderexecid,
                                                '&' as ordercateid, c.itempycode_chr as orderpycode, '#3' as orderflag 
                                           from t_opr_bih_patientcharge b,
  	                                            t_bse_chargeitem c
                                          where b.chargeitemid_chr = c.itemid_chr(+)
                                            and b.status_int = 1
                                            and b.pstatus_int <> 0 
                                            and b.amount_dec > 0 
                                            and b.orderexecid_chr is null
                                            and b.attachorderid_vchr is null
                                            and b.registerid_chr = ? 
                                            and (b.chargeactive_dat 
                                                        between to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                                            and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))";

                        objHRPSvc.CreateDatabaseParameter(9, out ParamArr);
                        ParamArr[0].Value = RegID;
                        ParamArr[1].Value = BeginDate + " 00:00:00";
                        ParamArr[2].Value = EndDate + " 23:59:59";
                        ParamArr[3].Value = RegID;
                        ParamArr[4].Value = BeginDate + " 00:00:00";
                        ParamArr[5].Value = EndDate + " 23:59:59";
                        ParamArr[6].Value = RegID;
                        ParamArr[7].Value = BeginDate + " 00:00:00";
                        ParamArr[8].Value = EndDate + " 23:59:59";
                    }
                    else
                    {
                        SQL = @"select distinct to_char (b.chargeactive_dat, 'yyyymmdd') as orderdate, b.orderexectype_int as ordertype,
                                                a.name_vchr as ordername, b.orderid_chr as orderid, b.orderexecid_chr as orderexecid,
                                                c.ordercateid_chr as ordercateid, c.pycode_chr as orderpycode, '#1' as orderflag 
                                           from t_opr_bih_order a,
                                                t_opr_bih_patientcharge b,
                                                t_bse_bih_orderdic c
                                          where a.orderid_chr = b.orderid_chr
                                            and a.orderdicid_chr = c.orderdicid_chr(+) 
                                            and (b.orderexectype_int in (1, 2, 3, 4)) 
                                            and b.status_int = 1
                                            and b.pstatus_int <> 0
                                            and b.registerid_chr = ? 
                                            and b.curareaid_chr = ?
                                            and (b.chargeactive_dat 
                                                        between to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                                            and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                union all
                                select distinct to_char (b.chargeactive_dat, 'yyyymmdd') as orderdate, b.orderexectype_int as ordertype,
                                                a.orderdicname_vchr as ordername, b.orderid_chr as orderid, b.attachorderid_vchr as orderexecid,
                                                c.ordercateid_chr as ordercateid, c.pycode_chr as orderpycode, '#2' as orderflag 
                                           from t_opr_bih_orderdic a,
                                                t_opr_bih_patientcharge b,
                                                t_bse_bih_orderdic c
                                          where a.orderid_chr = b.orderid_chr
                                            and a.orderdicid_chr = c.orderdicid_chr
                                            and b.status_int = 1
                                            and b.pstatus_int <> 0 
                                            and b.attachorderid_vchr is not null 
                                            and b.registerid_chr = ? 
                                            and b.curareaid_chr = ?
                                            and (b.chargeactive_dat 
                                                        between to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                                            and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                union all
                                select to_char (b.chargeactive_dat, 'yyyymmdd') as orderdate, b.orderexectype_int as ordertype,
                                                b.chargeitemname_chr as ordername, b.orderid_chr as orderid, b.pchargeid_chr as orderexecid,
                                                '&' as ordercateid, c.itempycode_chr as orderpycode, '#3' as orderflag 
                                           from t_opr_bih_patientcharge b,
  	                                            t_bse_chargeitem c
                                          where b.chargeitemid_chr = c.itemid_chr(+)
                                            and b.status_int = 1
                                            and b.pstatus_int <> 0 
                                            and b.amount_dec > 0 
                                            and b.orderexecid_chr is null
                                            and b.attachorderid_vchr is null
                                            and b.registerid_chr = ? 
                                            and b.curareaid_chr = ?
                                            and (b.chargeactive_dat 
                                                        between to_date(?, 'yyyy-mm-dd hh24:mi:ss')
                                                            and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))";

                        objHRPSvc.CreateDatabaseParameter(12, out ParamArr);
                        ParamArr[0].Value = RegID;
                        ParamArr[1].Value = AreaID;
                        ParamArr[2].Value = BeginDate + " 00:00:00";
                        ParamArr[3].Value = EndDate + " 23:59:59";
                        ParamArr[4].Value = RegID;
                        ParamArr[5].Value = AreaID;
                        ParamArr[6].Value = BeginDate + " 00:00:00";
                        ParamArr[7].Value = EndDate + " 23:59:59";
                        ParamArr[8].Value = RegID;
                        ParamArr[9].Value = AreaID;
                        ParamArr[10].Value = BeginDate + " 00:00:00";
                        ParamArr[11].Value = EndDate + " 23:59:59";
                    }
                }

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ���ݷ�����ϸID���ҷ�����Ϣ
        /// <summary>
        /// ���ݷ�����ϸID���ҷ�����Ϣ
        /// </summary>
        /// <param name="DiagArr"></param>
        /// <param name="dtNormal"></param>
        /// <param name="dtRefundment"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFeeItemByActiveType(List<clsParmDiagItem_VO> DiagArr, out DataTable dtNormal, out DataTable dtRefundment)
        {
            long lngRes = 0;
            string strPChargeID = "";
            string SQL = "";

            for (int i = 0; i < DiagArr.Count; i++)
            {
                strPChargeID += "'" + (DiagArr[i]).PchargeID + "',";
            }
            strPChargeID = strPChargeID.Substring(0, strPChargeID.Length - 1);

            dtNormal = new DataTable();
            dtRefundment = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();

                SQL = @"select a.pchargeid_chr, a.chargeitemid_chr, a.chargeitemname_chr,
                               a.amount_dec, a.pchargeidorg_chr, b.itemcode_vchr, a.unitprice_dec,a.totaldiffcostmoney_dec, a.buyprice_dec  
                          from t_opr_bih_patientcharge a,
                               t_bse_chargeitem b  
                         where a.chargeitemid_chr = b.itemid_chr(+)
                           and a.pchargeid_chr in (" + strPChargeID + ")";

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtNormal);

                SQL = @"select a.pchargeid_chr, a.chargeitemid_chr, a.chargeitemname_chr,
                               a.amount_dec, a.pchargeidorg_chr, b.itemcode_vchr, a.unitprice_dec,a.totaldiffcostmoney_dec, a.buyprice_dec 
                          from t_opr_bih_patientcharge a,
                               t_bse_chargeitem b  
                         where a.chargeitemid_chr = b.itemid_chr(+)
                           and a.pchargeidorg_chr in (" + strPChargeID + @") and a.amount_dec < 0";

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtRefundment);

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

        #region ���������ͻ�ȡ���˷��÷���
        /// <summary>
        /// ���������ͻ�ȡ���˷��÷���
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="DeptClass">1 ִ�п��� 2 �������� 3 ���ڲ���</param>
        /// <param name="Status">0 δ���ʽ��� 1 �Ѵ��ʽ���</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFeeCatByDeptClass(string RegID, int DeptClass, int Status, out DataTable dt)
        {
            long lngRes = 0;
            string ChargeNo = "";
            string SQL = "";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                if (Status == 1)
                {
                    SQL = @"select a.chargeno_chr from t_opr_bih_charge a where a.class_int = 3 and a.registerid_chr = ?";
                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = RegID;

                    DataTable dt1 = new DataTable();

                    lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt1, ParamArr);
                    if (dt1.Rows.Count > 0)
                    {
                        ChargeNo = dt1.Rows[0][0].ToString();
                        if (ChargeNo.Trim() == "")
                        {
                            Status = 0;
                        }
                        else
                        {
                            if (DeptClass == 1)
                            {
                                SQL = @"select  a.clacarea_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                                sum(a.totalmoney_dec) as catsum
                                          from  t_opr_bih_chargeitementry a, 
                                                (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                                t_bse_deptdesc c
                                          where a.calccateid_chr = b.typeid_chr(+)
                                            and a.clacarea_chr = c.deptid_chr(+)
                                            and a.chargeno_chr = ?                  
                                            and a.registerid_chr = ? 
                                       group by a.clacarea_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                       order by a.clacarea_chr, a.calccateid_chr";
                            }
                            else if (DeptClass == 2)
                            {
                                SQL = @"select   a.createarea_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                                 sum(a.totalmoney_dec) as catsum
                                            from t_opr_bih_chargeitementry a, 
                                                 (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                                 t_bse_deptdesc c
                                           where a.calccateid_chr = b.typeid_chr(+)
                                             and a.createarea_chr = c.deptid_chr(+)
                                             and a.chargeno_chr = ?
                                             and a.registerid_chr = ? 
                                        group by a.createarea_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                        order by a.createarea_chr, a.calccateid_chr";
                            }
                            else if (DeptClass == 3)
                            {
                                SQL = @"select   a.curareaid_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                                 sum(a.totalmoney_dec) as catsum
                                            from t_opr_bih_patientcharge a, 
                                                 (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                                 t_bse_deptdesc c
                                           where a.calccateid_chr = b.typeid_chr(+)
                                             and a.curareaid_chr = c.deptid_chr(+)
                                             and a.chargeno_chr = ?                           
                                             and a.registerid_chr = ? 
                                        group by a.curareaid_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                        order by a.curareaid_chr, a.calccateid_chr";
                            }

                            objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                            ParamArr[0].Value = ChargeNo;
                            ParamArr[1].Value = RegID;
                        }
                    }
                    else
                    {
                        Status = 0;
                    }
                }

                if (Status == 0)
                {
                    if (DeptClass == 1)
                    {
                        SQL = @"select   a.clacarea_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                         sum (round (a.amount_dec * a.unitprice_dec, 2)) as catsum
                                    from t_opr_bih_patientcharge a, 
                                         (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                         t_bse_deptdesc c
                                   where a.calccateid_chr = b.typeid_chr(+)
                                     and a.clacarea_chr = c.deptid_chr(+)
                                     and a.status_int = 1
                                     and (a.pstatus_int = 1 or a.pstatus_int = 2)
                                     and a.chargeactive_dat is not null                            
                                     and a.registerid_chr = ? 
                                group by a.clacarea_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                order by a.clacarea_chr, a.calccateid_chr";
                    }
                    else if (DeptClass == 2)
                    {
                        SQL = @"select   a.createarea_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                         sum (round (a.amount_dec * a.unitprice_dec, 2)) as catsum
                                    from t_opr_bih_patientcharge a, 
                                         (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                         t_bse_deptdesc c
                                   where a.calccateid_chr = b.typeid_chr(+)
                                     and a.createarea_chr = c.deptid_chr(+)
                                     and a.status_int = 1
                                     and (a.pstatus_int = 1 or a.pstatus_int = 2)
                                     and a.chargeactive_dat is not null
                                     and a.registerid_chr = ? 
                                group by a.createarea_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                order by a.createarea_chr, a.calccateid_chr";
                    }
                    else if (DeptClass == 3)
                    {
                        SQL = @"select   a.curareaid_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                         sum (round (a.amount_dec * a.unitprice_dec, 2)) as catsum
                                    from t_opr_bih_patientcharge a, 
                                         (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                         t_bse_deptdesc c
                                   where a.calccateid_chr = b.typeid_chr(+)
                                     and a.curareaid_chr = c.deptid_chr(+)
                                     and a.status_int = 1
                                     and (a.pstatus_int = 1 or a.pstatus_int = 2)
                                     and a.chargeactive_dat is not null                            
                                     and a.registerid_chr = ? 
                                group by a.curareaid_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                order by a.curareaid_chr, a.calccateid_chr";
                    }

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = RegID;
                }

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ���������ͻ�ȡ���˷��÷���ĸӤ�ϲ�����ʹ�� by yibing.zheng 09-07-04

        /// <summary>
        /// ���������ͻ�ȡ���˷��÷���ĸӤ�ϲ�����ʹ��
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="DeptClass">1 ִ�п��� 2 �������� 3 ���ڲ���</param>
        /// <param name="Status">0 δ���ʽ��� 1 �Ѵ��ʽ���</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFeeCatByDeptClassForMortherBaby(string RegID, int DeptClass, int Status, out DataTable dt)
        {
            long lngRes = 0;
            string ChargeNo = "";
            string SQL = "";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                #region ���˷���
                if (Status == 1)
                {
                    SQL = @"select a.chargeno_chr from t_opr_bih_charge a where a.class_int = 3 and a.registerid_chr = ?";
                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = RegID;

                    DataTable dt1 = new DataTable();

                    lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt1, ParamArr);
                    if (dt1.Rows.Count > 0)
                    {
                        ChargeNo = dt1.Rows[0][0].ToString();
                        if (ChargeNo.Trim() == "")
                        {
                            Status = 0;
                        }
                        else
                        {
                            if (DeptClass == 1)
                            {
                                SQL = @"select  a.clacarea_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                                sum(a.totalmoney_dec) as catsum
                                          from  t_opr_bih_chargeitementry a, 
                                                (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                                t_bse_deptdesc c
                                          where a.calccateid_chr = b.typeid_chr(+)
                                            and a.clacarea_chr = c.deptid_chr(+)
                                            and a.chargeno_chr = ?                  
                                            and a.registerid_chr = ? 
                                       group by a.clacarea_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                       order by a.clacarea_chr, a.calccateid_chr";
                            }
                            else if (DeptClass == 2)
                            {
                                SQL = @"select   a.createarea_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                                 sum(a.totalmoney_dec) as catsum
                                            from t_opr_bih_chargeitementry a, 
                                                 (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                                 t_bse_deptdesc c
                                           where a.calccateid_chr = b.typeid_chr(+)
                                             and a.createarea_chr = c.deptid_chr(+)
                                             and a.chargeno_chr = ?
                                             and a.registerid_chr = ? 
                                        group by a.createarea_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                        order by a.createarea_chr, a.calccateid_chr";
                            }
                            else if (DeptClass == 3)
                            {
                                SQL = @"select   a.curareaid_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                                 sum(a.totalmoney_dec) as catsum
                                            from t_opr_bih_patientcharge a, 
                                                 (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                                 t_bse_deptdesc c
                                           where a.calccateid_chr = b.typeid_chr(+)
                                             and a.curareaid_chr = c.deptid_chr(+)
                                             and a.chargeno_chr = ?                           
                                             and a.registerid_chr = ? 
                                        group by a.curareaid_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                        order by a.curareaid_chr, a.calccateid_chr";
                            }

                            objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                            ParamArr[0].Value = ChargeNo;
                            ParamArr[1].Value = RegID;
                        }
                    }
                    else
                    {
                        Status = 0;
                    }
                }

                if (Status == 0)
                {
                    if (DeptClass == 1)
                    {
                        SQL = @"select   a.clacarea_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                         sum (round (a.amount_dec * a.unitprice_dec, 2)) as catsum
                                    from t_opr_bih_patientcharge a, 
                                         (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                         t_bse_deptdesc c
                                   where a.calccateid_chr = b.typeid_chr(+)
                                     and a.clacarea_chr = c.deptid_chr(+)
                                     and a.status_int = 1
                                     and a.pstatus_int in (1,2)
                                     and a.chargeactive_dat is not null                            
                                     and a.registerid_chr = ? 
                                group by a.clacarea_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                order by a.clacarea_chr, a.calccateid_chr";
                    }
                    else if (DeptClass == 2)
                    {
                        SQL = @"select   a.createarea_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                         sum (round (a.amount_dec * a.unitprice_dec, 2)) as catsum
                                    from t_opr_bih_patientcharge a, 
                                         (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                         t_bse_deptdesc c
                                   where a.calccateid_chr = b.typeid_chr(+)
                                     and a.createarea_chr = c.deptid_chr(+)
                                     and a.status_int = 1
                                     and a.pstatus_int in (1,2)
                                     and a.chargeactive_dat is not null
                                     and a.registerid_chr = ? 
                                group by a.createarea_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                order by a.createarea_chr, a.calccateid_chr";
                    }
                    else if (DeptClass == 3)
                    {
                        SQL = @"select   a.curareaid_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                         sum (round (a.amount_dec * a.unitprice_dec, 2)) as catsum
                                    from t_opr_bih_patientcharge a, 
                                         (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                         t_bse_deptdesc c
                                   where a.calccateid_chr = b.typeid_chr(+)
                                     and a.curareaid_chr = c.deptid_chr(+)
                                     and a.status_int = 1
                                     and a.pstatus_int in (1,2)
                                     and a.chargeactive_dat is not null                            
                                     and a.registerid_chr = ? 
                                group by a.curareaid_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                order by a.curareaid_chr, a.calccateid_chr";
                    }

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = RegID;
                }

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                #endregion

                #region �ж��Ƿ���Ӥ������
                SQL = @"select t.registerid_chr
  from t_opr_bih_register t
 where t.pstatus_int in (2,3) 
 and t.relateregisterid_chr = ?";
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = RegID;
                DataTable dtbBagyRegisterId = new DataTable();
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtbBagyRegisterId, ParamArr);
                int intRowCount = dtbBagyRegisterId.Rows.Count;
                string[] strBabyRegisterIdArr = new string[intRowCount];
                if (lngRes > 0 && intRowCount > 0)
                {
                    string strChargeNoRegId = null, strNOChargeNoRegId = null, strChargeNo = null;
                    for (int i1 = 0; i1 < intRowCount; i1++)
                    {
                        strBabyRegisterIdArr[i1] = "'" + dtbBagyRegisterId.Rows[i1]["registerid_chr"].ToString() + "',";
                        strNOChargeNoRegId += strBabyRegisterIdArr[i1];

                    }
                    strNOChargeNoRegId = strNOChargeNoRegId.Remove(strNOChargeNoRegId.Length - 1);

                    DataTable dtbBabyCharge = new DataTable();
                    #region ��ȡӤ������
                    if (Status == 1)
                    {
                        DataTable dtbTemp = new DataTable();

                        strNOChargeNoRegId = null;
                        //�ж�Ӥ���Ƿ��з�Ʊ�ţ��Ѵ��˽��㣬�з�Ʊ�ŵ�Ӥ�������£�û��Ʊ�ŵ�Ӥ����ִ��
                        for (int i2 = 0; i2 < strBabyRegisterIdArr.Length; i2++)
                        {
                            SQL = @"select a.chargeno_chr
                                  from t_opr_bih_charge a
                                 where a.class_int = 3
                                   and a.registerid_chr =?
                                order by a.operdate_dat desc";
                            objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                            ParamArr[0].Value = strBabyRegisterIdArr[i2];
                            lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtbTemp, ParamArr);
                            if (lngRes > 0 && dtbTemp.Rows.Count > 0)
                            {
                                strChargeNoRegId += strBabyRegisterIdArr[i2];
                                strChargeNo += "'" + dtbTemp.Rows[0][0].ToString() + "',";
                            }
                            else
                            {
                                strNOChargeNoRegId += strBabyRegisterIdArr[i2];
                            }

                        }
                        if (strChargeNoRegId != null)
                        {
                            strChargeNoRegId = strChargeNoRegId.Remove(strChargeNoRegId.Length - 1);
                            strChargeNo = strChargeNo.Remove(strChargeNo.Length - 1);
                        }
                        if (strNOChargeNoRegId != null)
                        {
                            strNOChargeNoRegId = strNOChargeNoRegId.Remove(strNOChargeNoRegId.Length - 1);
                        }
                        if (!string.IsNullOrEmpty(strChargeNo))
                        {
                            //ChargeNo = dtbTemp.Rows[0][0].ToString();

                            if (strChargeNo.Trim() == "")
                            {
                                Status = 0;
                            }
                            else
                            {
                                if (DeptClass == 1)
                                {
                                    SQL = @"select  a.clacarea_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                                sum(a.totalmoney_dec) as catsum
                                          from  t_opr_bih_chargeitementry a, 
                                                (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                                t_bse_deptdesc c
                                          where a.calccateid_chr = b.typeid_chr(+)
                                            and a.clacarea_chr = c.deptid_chr(+)
                                            and a.chargeno_chr in ([strChargeNo])                 
                                            and a.registerid_chr in ([babyRegisterId])
                                       group by a.clacarea_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                       order by a.clacarea_chr, a.calccateid_chr";
                                }
                                else if (DeptClass == 2)
                                {
                                    SQL = @"select   a.createarea_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                                 sum(a.totalmoney_dec) as catsum
                                            from t_opr_bih_chargeitementry a, 
                                                 (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                                 t_bse_deptdesc c
                                           where a.calccateid_chr = b.typeid_chr(+)
                                             and a.createarea_chr = c.deptid_chr(+)
                                             and a.chargeno_chr in ([strChargeNo])
                                             and a.registerid_chr in ([babyRegisterId]) 
                                        group by a.createarea_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                        order by a.createarea_chr, a.calccateid_chr";
                                }
                                else if (DeptClass == 3)
                                {
                                    SQL = @"select   a.curareaid_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                                 sum(a.totalmoney_dec) as catsum
                                            from t_opr_bih_chargeitementry a, 
                                                 (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                                 t_bse_deptdesc c
                                           where a.calccateid_chr = b.typeid_chr(+)
                                             and a.curareaid_chr = c.deptid_chr(+)
                                             and a.chargeno_chr in ([strChargeNo])                           
                                             and a.registerid_chr in ([babyRegisterId])
                                        group by a.curareaid_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                        order by a.curareaid_chr, a.calccateid_chr";
                                }

                                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                                ParamArr[0].Value = ChargeNo;
                                SQL = SQL.Replace("[strChargeNo]", strChargeNo);
                                SQL = SQL.Replace("[babyRegisterId]", strChargeNoRegId);
                                DataTable dtbTemp2 = null;
                                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtbTemp2);
                                if (dtbTemp.Rows.Count > 0)
                                {
                                    dtbBabyCharge = dtbTemp2;
                                    dtbTemp2 = null;
                                }

                            }
                        }
                        else
                        {
                            Status = 0;
                        }
                        if (!string.IsNullOrEmpty(strNOChargeNoRegId))
                        {
                            Status = 0;
                        }
                        dtbTemp.Dispose();
                        dtbTemp = null;
                    }

                    if (Status == 0)
                    {
                        if (DeptClass == 1)
                        {
                            SQL = @"select   a.clacarea_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                         sum (round (a.totalmoney_dec, 2)) as catsum
                                    from t_opr_bih_patientcharge a, 
                                         (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                         t_bse_deptdesc c
                                   where a.calccateid_chr = b.typeid_chr(+)
                                     and a.clacarea_chr = c.deptid_chr(+)
                                     and a.status_int = 1
                                     and a.pstatus_int in (1,2)
                                     and a.chargeactive_dat is not null                            
                                     and a.registerid_chr in ([babyRegisterId]) 
                                group by a.clacarea_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                order by a.clacarea_chr, a.calccateid_chr";
                        }
                        else if (DeptClass == 2)
                        {
                            SQL = @"select   a.createarea_chr as deptid, c.deptname_vchr, a.calccateid_chr, b.typename_vchr,
                                         sum (round (a.totalmoney_dec, 2)) as catsum
                                    from t_opr_bih_patientcharge a, 
                                         (select typeid_chr, typename_vchr from t_bse_chargeitemextype where flag_int = 3) b,
                                         t_bse_deptdesc c
                                   where a.calccateid_chr = b.typeid_chr(+)
                                     and a.createarea_chr = c.deptid_chr(+)
                                     and a.status_int = 1
                                     and  a.pstatus_int in (1,2)
                                     and a.chargeactive_dat is not null
                                     and a.registerid_chr in ([babyRegisterId]) 
                                group by a.createarea_chr, c.deptname_vchr, a.calccateid_chr, b.typename_vchr
                                order by a.createarea_chr, a.calccateid_chr";
                        }
                        else if (DeptClass == 3)
                        {
                            SQL = @"select a.curareaid_chr as deptid,
                                       c.deptname_vchr,
                                       a.calccateid_chr,
                                       b.typename_vchr,
                                       sum(round(a.totalmoney_dec, 2)) as catsum
                                  from t_opr_bih_patientcharge a,
                                       (select typeid_chr, typename_vchr
                                          from t_bse_chargeitemextype
                                         where flag_int = 3) b,
                                       t_bse_deptdesc c
                                 where a.calccateid_chr = b.typeid_chr(+)
                                   and a.curareaid_chr = c.deptid_chr(+)
                                   and a.status_int = 1
                                   and a.pstatus_int in (1,2)
                                   and a.chargeactive_dat is not null
                                   and a.registerid_chr in ([babyRegisterId])
                                 group by a.curareaid_chr,
                                          c.deptname_vchr,
                                          a.calccateid_chr,
                                          b.typename_vchr
                                 order by a.curareaid_chr, a.calccateid_chr";
                        }

                        SQL = SQL.Replace("[babyRegisterId]", strNOChargeNoRegId);
                        DataTable dtbTemp2 = null;
                        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtbTemp2);
                        if (dtbTemp2.Rows.Count > 0)
                        {
                            if (dtbBabyCharge.Rows.Count > 0)
                            {
                                dtbBabyCharge.Merge(dtbTemp2);
                            }
                            else
                            {
                                dtbBabyCharge = dtbTemp2;
                            }
                            dtbTemp2 = null;
                        }
                    }

                    // lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtbBabyCharge, ParamArr);
                    //lngRes=objHRPSvc.lngGetDataTableWithoutParameters(SQL,ref dtbBabyCharge);
                    if (lngRes > 0 && dtbBabyCharge.Rows.Count > 0)
                    {
                        //string strTmp = dt.Compute("sum(catsum)", "").ToString();
                        //strTmp = dtbBabyCharge.Compute("sum(catsum)", "").ToString();
                        string[] sarr = new string[5];
                        sarr[1] = "Ӥ������";
                        dt.Rows.Add(sarr);

                        if (dt != null && dtbBabyCharge != null && dtbBabyCharge.Rows.Count > 0)
                            dt.Merge(dtbBabyCharge);

                        //strTmp = dt.Compute("sum(catsum)", "").ToString();

                        //clsFieldInfo[] colSelectFields = new clsFieldInfo[5];
                        //colSelectFields[0] = new clsFieldInfo();
                        //colSelectFields[0].strFieldName = "catsum";
                        //colSelectFields[0].strFieldAlias = "catsum";
                        //colSelectFields[0].strAggregate = "sum";
                        //colSelectFields[0].strRelationName = "";
                        //colSelectFields[1] = new clsFieldInfo();
                        //colSelectFields[1].strFieldName = "deptid";
                        //colSelectFields[1].strFieldAlias = "deptid";
                        //colSelectFields[1].strAggregate = "";
                        //colSelectFields[1].strRelationName = "";
                        //colSelectFields[2] = new clsFieldInfo();
                        //colSelectFields[2].strFieldName = "deptname_vchr";
                        //colSelectFields[2].strFieldAlias = "deptname_vchr";
                        //colSelectFields[2].strAggregate = "";
                        //colSelectFields[2].strRelationName = "";
                        //colSelectFields[3] = new clsFieldInfo();
                        //colSelectFields[3].strFieldName = "calccateid_chr";
                        //colSelectFields[3].strFieldAlias = "calccateid_chr";
                        //colSelectFields[3].strAggregate = "";
                        //colSelectFields[3].strRelationName = "";
                        //colSelectFields[4] = new clsFieldInfo();
                        //colSelectFields[4].strFieldName = "typename_vchr";
                        //colSelectFields[4].strFieldAlias = "typename_vchr";
                        //colSelectFields[4].strAggregate = "";
                        //colSelectFields[4].strRelationName = "";
                        //clsFieldInfo[] colGroupByFields = new clsFieldInfo[4];
                        //colGroupByFields[0] = new clsFieldInfo();
                        //colGroupByFields[0].strFieldName = "deptid";
                        //colGroupByFields[0].strFieldAlias = "deptid";
                        //colGroupByFields[0].strAggregate = "";
                        //colGroupByFields[0].strRelationName = "";
                        //colGroupByFields[1] = new clsFieldInfo();
                        //colGroupByFields[1].strFieldName = "deptname_vchr";
                        //colGroupByFields[1].strFieldAlias = "deptname_vchr";
                        //colGroupByFields[1].strAggregate = "";
                        //colGroupByFields[1].strRelationName = "";
                        //colGroupByFields[2] = new clsFieldInfo();
                        //colGroupByFields[2].strFieldName = "calccateid_chr";
                        //colGroupByFields[2].strFieldAlias = "calccateid_chr";
                        //colGroupByFields[2].strAggregate = "";
                        //colGroupByFields[2].strRelationName = "";
                        //colGroupByFields[3] = new clsFieldInfo();
                        //colGroupByFields[3].strFieldName = "typename_vchr";
                        //colGroupByFields[3].strFieldAlias = "typename_vchr";
                        //colGroupByFields[3].strAggregate = "";
                        //colGroupByFields[3].strRelationName = "";
                        //DataTable dtbNewCharge = null;
                        //clsDataSetHelper objDSHelper = new clsDataSetHelper();
                        //objDSHelper.CreateGroupByTable("MatherBabyCharge", dt, colSelectFields, ref dtbNewCharge);
                        //strTmp = dtbNewCharge.Compute("sum(catsum)", "").ToString();

                        //objDSHelper.InsertGroupByInto(ref dtbNewCharge, dt, colSelectFields, "", colGroupByFields);
                        //dt = null;

                        //dt = dtbNewCharge;


                    }
                    dtbBabyCharge.Dispose();
                    dtbBabyCharge = null;
                    #endregion
                }
                dtbBagyRegisterId.Dispose();
                dtbBagyRegisterId = null;

                #endregion


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

        #region ��ȡ���ʲ���δ�����
        /// <summary>
        /// ��ȡ���ʲ���δ�����
        /// </summary>
        /// <param name="RegID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBadChargeFeeInfo(string RegID, out DataTable dt)
        {
            long lngRes = 0;

            string SQL = @"select a.chargeitemid_chr, a.invcateid_chr, a.amount_dec, a.unitprice_dec 
                             from t_opr_bih_patientcharge a 
                            where a.status_int = 1
                              and (a.pstatus_int = 1 or a.pstatus_int = 2)
                              and a.chargeactive_dat is not null                            
                              and a.registerid_chr = ? ";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = RegID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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
        /// ��ȡ���ʲ���δ�����(ĸӤ�ϲ��������) by yibin.zheng 09-07
        /// </summary>
        /// <param name="RegID">����ID</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBadChargeFeeInfoMotherBaby(string RegID, out DataTable dt)
        {
            long lngRes = 0;
            string SQL = @"select t.registerid_chr
  from t_opr_bih_register t
 where t.pstatus_int in (2,3) 
   and t.relateregisterid_chr = ?
";

            DataTable dtbBaby = null;
            dt = new DataTable();
            clsHRPTableService objHRPSvc = null;
            try
            {

                objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = RegID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtbBaby, ParamArr);
                string strBabyRegId = "";
                if (lngRes > 0 && dtbBaby.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbBaby.Rows.Count; i++)
                    {
                        strBabyRegId += "'" + dtbBaby.Rows[i][0].ToString() + "',";
                    }
                    strBabyRegId = strBabyRegId.Remove(strBabyRegId.Length - 1);
                }
                if (strBabyRegId != "")
                {
                    RegID = "'" + RegID + "'," + strBabyRegId;
                }
                else
                {
                    RegID = "'" + RegID + "'";
                }
                strBabyRegId = null;
                SQL = @"select a.chargeitemid_chr, a.invcateid_chr, a.amount_dec, a.unitprice_dec 
                             from t_opr_bih_patientcharge a 
                            where a.status_int = 1
                              and a.pstatus_int in (1,2)
                              and a.chargeactive_dat is not null                            
                              and a.registerid_chr in ([RegID]) ";
                SQL = SQL.Replace("[RegID]", RegID);

                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dt);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                SQL = null;
                objHRPSvc.Dispose();
                objHRPSvc = null;
                dtbBaby.Dispose();
                dtbBaby = null;

            }
            return lngRes;
        }
        #endregion

        #region ���ʽ���
        /// <summary>
        /// ���ʽ���
        /// </summary>
        /// <param name="Charge_VO"></param>
        /// <param name="ChargeCatArr"></param>
        /// <param name="FactTotalMoney">ʵ��δ�ᡢδ���ܽ��</param>
        /// <param name="FactPreMoney">ʵ�ʷ�̯�ܽ��</param>
        /// <param name="DiffValDeptID">��ֵ�����ID</param>
        /// <param name="DiffValCatID">��ֵ��������ID</param>
        /// <param name="IsHavePrepayMoney">�Ƿ���Ԥ���� (true �� false ��)</param>
        /// <param name="ChargeNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngBadCharge(clsBihCharge_VO Charge_VO, List<clsBihChargeCat_VO> ChargeCatArr, clsBihInvoice_VO Invoice_VO, List<clsBihInvoiceCat_VO> InvoCatArr, List<clsBihPayment_VO> PaymentArr, decimal FactTotalMoney, decimal FactPreMoney, string DiffValDeptID, string DiffValCatID, bool IsHavePrepayMoney, out string ChargeNo)
        {
            long lngRes = 0, lngAffects = 0;
            //ִ��SQL
            string SQL = "";
            //����ʱ�����ɽ����
            ChargeNo = DateTime.Now.ToString("yyyyMMddHHmmssffffff");

            // 2019-11-12 ���ַ�Ʊ����ϼƽ�� <> ��Ʊ�� ��ʱ�����ж�
            decimal decTmpSum = 0;
            foreach (clsBihInvoiceCat_VO item in InvoCatArr)
            {
                decTmpSum += item.TotalSum;
            }
            if (decTmpSum != Invoice_VO.TotalSum)
            {
                decimal diff = Invoice_VO.TotalSum - decTmpSum;
                for (int i = InvoCatArr.Count - 1; i >= 0; i--)
                {
                    if (InvoCatArr[i].TotalSum + diff > 0)
                    {
                        InvoCatArr[i].TotalSum = InvoCatArr[i].TotalSum + diff;
                        break;
                    }
                }
                //weCare.Core.Utils.ExceptionLog.OutPutException("��Ʊ��:" + Invoice_VO.InvoiceNo + "  ��Ʊ��� �� ��Ʊ������ܲ���ȡ�");
                //return -3;
            }

            #region ������ǰ�Է�Ʊ��������ȡС�����4λ�������ܽ��ͷ���Ϳ������0.01���ʽ��и�Ϊ��ȡ��2λ 2019-11-14

            foreach (clsBihInvoiceCat_VO item in InvoCatArr)
            {
                // �������������
                if (item.ItemCatID == "3026") continue;
                item.TotalSum = Function.Round(item.TotalSum, 2);
            }

            // У�鷢Ʊ�������
            decimal catSum = 0;
            decimal diffMny = 0;
            foreach (clsBihInvoiceCat_VO item in InvoCatArr)
            {
                // �������������
                if (item.ItemCatID == "3026") continue;
                catSum += item.TotalSum;
            }
            if (catSum != Invoice_VO.TotalSum)
            {
                bool isOk = false;
                diffMny = Invoice_VO.TotalSum - catSum;
                foreach (clsBihInvoiceCat_VO item in InvoCatArr)
                {
                    // �������������
                    if (item.ItemCatID == "3026") continue;
                    if (item.TotalSum > 0 && (item.TotalSum + diffMny > 0) && ((int)item.TotalSum != item.TotalSum))
                    {
                        item.TotalSum = item.TotalSum + diffMny;
                        isOk = true;
                        break;
                    }
                }
                if (isOk == false)
                {
                    foreach (clsBihInvoiceCat_VO item in InvoCatArr)
                    {
                        // �������������
                        if (item.ItemCatID == "3026") continue;
                        if (item.TotalSum > 0 && (item.TotalSum + diffMny > 0))
                        {
                            item.TotalSum = item.TotalSum + diffMny;
                            break;
                        }
                    }
                }
            }
            #endregion

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                //��Ժ�ǼǺ�
                string RegID = Charge_VO.RegisterID;

                if (IsHavePrepayMoney)
                {
                    //�����
                    SQL = @"insert into t_opr_bih_charge(chargeno_chr, registerid_chr, paytypeid_chr, patienttype_chr, totalsum_mny, sbsum_mny, acctsum_mny, operemp_chr, 
                                                     operdate_dat, recflag_int, recemp_chr, recdate_dat, class_int, type_int, status_int, billno_int, areaid_chr) values (  
                                                     ?, ?, ?, ?, ?, ?, ?, ?, sysdate, 0, null, null, ?, 1, 1, ?, ?)";

                    objHRPSvc.CreateDatabaseParameter(11, out ParamArr);
                    ParamArr[0].Value = ChargeNo;
                    ParamArr[1].Value = Charge_VO.RegisterID;
                    ParamArr[2].Value = Charge_VO.PayTypeID;
                    ParamArr[3].Value = Charge_VO.PatientType;
                    ParamArr[4].Value = Charge_VO.TotalSum;
                    ParamArr[5].Value = Charge_VO.SbSum;
                    ParamArr[6].Value = Charge_VO.AcctSum;
                    ParamArr[7].Value = Charge_VO.OperEmp;
                    ParamArr[8].Value = 3;
                    ParamArr[9].Value = Charge_VO.BillNO;
                    ParamArr[10].Value = Charge_VO.CurrAreaID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    //��������
                    for (int i = 0; i < ChargeCatArr.Count; i++)
                    {
                        clsBihChargeCat_VO ChargeCat_VO = ChargeCatArr[i];

                        if (ChargeCat_VO.TotalSum == 0)
                        {
                            continue;
                        }

                        SQL = @"insert into t_opr_bih_chargecat(chargeno_chr, deptid_chr, itemcatid_chr, totalsum_mny, acctsum_mny) values(  
                                                            ?, ?, ?, ?, ?)";

                        objHRPSvc.CreateDatabaseParameter(5, out ParamArr);
                        ParamArr[0].Value = ChargeNo;
                        ParamArr[1].Value = ChargeCat_VO.DeptID;
                        ParamArr[2].Value = ChargeCat_VO.ItemCatID;
                        ParamArr[3].Value = ChargeCat_VO.TotalSum;
                        ParamArr[4].Value = ChargeCat_VO.AcctSum;

                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                    }

                    //��Ʊ��(����ҵ��Ĭ��һ�ν����Ӧһ�ŷ�Ʊ)
                    string Invono = Invoice_VO.InvoiceNo;

                    //���㷢Ʊ��Ӧ��
                    SQL = "insert into t_opr_bih_chargedefinv(chargeno_chr, invoiceno_vchr) values(?, ?)";

                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = ChargeNo;
                    ParamArr[1].Value = Invono;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    //��Ʊ��
                    SQL = @"insert into t_opr_bih_invoice2(invoiceno_vchr, invdate_dat, totalsum_mny, sbsum_mny, acctsum_mny, status_int, confirmemp_chr, confirmdate_dat, split_int) values(  
                                                       ?, sysdate, ?, ?, ?, ?, null, null, ?)";

                    objHRPSvc.CreateDatabaseParameter(6, out ParamArr);
                    ParamArr[0].Value = Invoice_VO.InvoiceNo;
                    ParamArr[1].Value = Invoice_VO.TotalSum;
                    ParamArr[2].Value = Invoice_VO.SbSum;
                    ParamArr[3].Value = Invoice_VO.AcctSum;
                    ParamArr[4].Value = Invoice_VO.Status;
                    ParamArr[5].Value = Invoice_VO.Split;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    //��Ʊ��ϸ��
                    for (int i = 0; i < InvoCatArr.Count; i++)
                    {
                        clsBihInvoiceCat_VO InvoiceCat_VO = InvoCatArr[i];

                        if (InvoiceCat_VO.TotalSum == 0)
                        {
                            continue;
                        }

                        SQL = "insert into t_opr_bih_invoice2de(invoiceno_vchr, itemcatid_chr, totalsum_mny, acctsum_mny, factsum) values(?, ?, ?, ?, ?)";

                        objHRPSvc.CreateDatabaseParameter(5, out ParamArr);
                        ParamArr[0].Value = Invono;
                        ParamArr[1].Value = InvoiceCat_VO.ItemCatID;
                        ParamArr[2].Value = InvoiceCat_VO.TotalSum;
                        ParamArr[3].Value = InvoiceCat_VO.AcctSum;
                        ParamArr[4].Value = InvoiceCat_VO.TotalSum;

                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                    }

                    //֧����
                    for (int i = 0; i < PaymentArr.Count; i++)
                    {
                        clsBihPayment_VO Payment_VO = PaymentArr[i];

                        SQL = @"insert into t_opr_bih_payment(chargeno_vchr, paytype_int, paycardtype_int, paycardno_vchr, paysum_mny, refusum_mny) values(  
                                                          ?, ?, null, null, ?, ?)";

                        objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                        ParamArr[0].Value = ChargeNo;
                        ParamArr[1].Value = Payment_VO.PayType;
                        ParamArr[2].Value = Payment_VO.PaySum;
                        ParamArr[3].Value = Payment_VO.RefuSum;

                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                    }

                    //���ɽ�����Ŀ��ϸ
                    SQL = @"insert into t_opr_bih_chargeitementry (sid_int, chargeno_chr, operdate_dat, pchargeid_chr, registerid_chr, createarea_chr, clacarea_chr, curareaid_chr, 
                                                                   curbedid_chr, doctorid_chr, doctorgroupid_chr, calccateid_chr, invcateid_chr, chargeitemid_chr, chargeitemname_chr, 
                                                                   unit_vchr, spec_vchr, unitprice_dec, amount_dec, totalmoney_dec, discount_dec, newdiscount_dec, acctmoney_dec, 
                                                                   chargedoctorid_chr, chargedoctorgroupid_chr)
                                                            select seq_chargeitementry.NEXTVAL,
                                                                   ?,
                                                                   sysdate,
                                                                   pchargeid_chr,
                                                                   registerid_chr,
                                                                   createarea_chr,  
                                                                   clacarea_chr,
                                                                   curareaid_chr,
                                                                   curbedid_chr,
                                                                   doctorid_chr,
                                                                   doctorgroupid_chr,
                                                                   calccateid_chr,
                                                                   invcateid_chr,
                                                                   chargeitemid_chr,
                                                                   chargeitemname_chr,
                                                                   unit_vchr,
                                                                   spec_vchr,
                                                                   unitprice_dec,
                                                                   amount_dec,
                                                                   round(unitprice_dec * amount_dec * " + FactPreMoney + "/" + FactTotalMoney + @", 2),
                                                                   0,
                                                                   0,
                                                                   0,
                                                                   chargedoctorid_chr,
                                                                   chargedoctorgroupid_chr 
                                                              from t_opr_bih_patientcharge 
                                                             where status_int = 1
                                                               and (pstatus_int = 1 or pstatus_int = 2)
                                                               and chargeactive_dat is not null
                                                               and registerid_chr = ? ";

                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = ChargeNo;
                    ParamArr[1].Value = RegID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                    //����ֵ
                    if (DiffValCatID != "")
                    {
                        SQL = @"select sum(totalmoney_dec) from t_opr_bih_chargeitementry where chargeno_chr = ? and registerid_chr = ?";
                        objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                        ParamArr[0].Value = ChargeNo;
                        ParamArr[1].Value = RegID;

                        DataTable dt = new DataTable();

                        lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                        if (lngRes > 0 && dt.Rows.Count == 1)
                        {
                            decimal val = FactPreMoney - Convert.ToDecimal(dt.Rows[0][0].ToString());

                            SQL = @"update t_opr_bih_chargeitementry 
                                   set totalmoney_dec = " + val + @" + totalmoney_dec
                                 where chargeno_chr = ? 
                                   and registerid_chr = ? 
                                   and clacarea_chr = ?  
                                   and calccateid_chr = ? 
                                   and rownum = 1 ";

                            objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                            ParamArr[0].Value = ChargeNo;
                            ParamArr[1].Value = RegID;
                            ParamArr[2].Value = DiffValDeptID;
                            ParamArr[3].Value = DiffValCatID;

                            lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                        }
                    }

                    #region Ӥ������������Ŀ��ϸ����
                    if (Charge_VO.m_objBabyInfoVo != null)
                    {
                        int intBabyCount = Charge_VO.m_objBabyInfoVo.Length;
                        string strBabyRegisterId = "";
                        for (int i1 = 0; i1 < intBabyCount; i1++)
                        {
                            strBabyRegisterId = Charge_VO.m_objBabyInfoVo[i1].RegisterID;
                            SQL = @"insert into t_opr_bih_chargeitementry (sid_int, chargeno_chr, operdate_dat, pchargeid_chr, registerid_chr, createarea_chr, clacarea_chr, curareaid_chr, 
                                                                   curbedid_chr, doctorid_chr, doctorgroupid_chr, calccateid_chr, invcateid_chr, chargeitemid_chr, chargeitemname_chr, 
                                                                   unit_vchr, spec_vchr, unitprice_dec, amount_dec, totalmoney_dec, discount_dec, newdiscount_dec, acctmoney_dec, 
                                                                   chargedoctorid_chr, chargedoctorgroupid_chr)
                                                            select seq_chargeitementry.NEXTVAL,
                                                                   ?,
                                                                   sysdate,
                                                                   pchargeid_chr,
                                                                   registerid_chr,
                                                                   createarea_chr,  
                                                                   clacarea_chr,
                                                                   curareaid_chr,
                                                                   curbedid_chr,
                                                                   doctorid_chr,
                                                                   doctorgroupid_chr,
                                                                   calccateid_chr,
                                                                   invcateid_chr,
                                                                   chargeitemid_chr,
                                                                   chargeitemname_chr,
                                                                   unit_vchr,
                                                                   spec_vchr,
                                                                   unitprice_dec,
                                                                   amount_dec,
                                                                   round(unitprice_dec * amount_dec * " + FactPreMoney + "/" + FactTotalMoney + @", 2),
                                                                   0,
                                                                   0,
                                                                   0,
                                                                   chargedoctorid_chr,
                                                                   chargedoctorgroupid_chr 
                                                              from t_opr_bih_patientcharge 
                                                             where status_int = 1
                                                               and (pstatus_int = 1 or pstatus_int = 2)
                                                               and chargeactive_dat is not null
                                                               and registerid_chr = ? ";

                            objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                            ParamArr[0].Value = ChargeNo;
                            ParamArr[1].Value = strBabyRegisterId;

                            lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                            //����ֵ
                            if (DiffValCatID != "")
                            {
                                SQL = @"select sum(totalmoney_dec) from t_opr_bih_chargeitementry where chargeno_chr = ? and registerid_chr = ?";
                                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                                ParamArr[0].Value = ChargeNo;
                                ParamArr[1].Value = strBabyRegisterId;

                                DataTable dt = new DataTable();

                                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
                                if (lngRes > 0 && dt.Rows.Count == 1)
                                {
                                    decimal val = FactPreMoney - Convert.ToDecimal(dt.Rows[0][0].ToString());

                                    SQL = @"update t_opr_bih_chargeitementry 
                                   set totalmoney_dec = " + val + @" + totalmoney_dec
                                 where chargeno_chr = ? 
                                   and registerid_chr = ? 
                                   and clacarea_chr = ?  
                                   and calccateid_chr = ? 
                                   and rownum = 1 ";

                                    objHRPSvc.CreateDatabaseParameter(4, out ParamArr);
                                    ParamArr[0].Value = ChargeNo;
                                    ParamArr[1].Value = strBabyRegisterId;
                                    ParamArr[2].Value = DiffValDeptID;
                                    ParamArr[3].Value = DiffValCatID;

                                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                                }
                            }
                        }

                    }

                    #endregion

                    //Ԥ������
                    SQL = @"update t_opr_bih_prepay set isclear_int = 1, chargeno_chr = ? where chargeno_chr is null and registerid_chr = ?";
                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = ChargeNo;
                    ParamArr[1].Value = RegID;

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                }

                //��Ժ�ǼǱ�
                SQL = @"update t_opr_bih_register set pstatus_int = 3, feestatus_int = 4, operatorid_chr = ? where registerid_chr = ?";
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = Charge_VO.OperEmp;
                ParamArr[1].Value = RegID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //��Ժ��¼��
                SQL = @"update t_opr_bih_leave set modify_dat = sysdate, pstatus_int = 1, operatorid_chr = ? where status_int = 1 and registerid_chr = ?";
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = Charge_VO.OperEmp;
                ParamArr[1].Value = RegID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //��λ��
                SQL = @"update t_bse_bed set status_int = 1, bihregisterid_chr = '' where bihregisterid_chr = ?";
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = RegID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                //��ת��Ϣ��
                SQL = @"insert into t_opr_bih_transfer (transferid_chr, sourcebedid_chr, targetbedid_chr, type_int, des_vchr, operatorid_chr, registerid_chr, 
                                                                            modify_dat, sourcedeptid_chr, sourceareaid_chr, targetdeptid_chr, targetareaid_chr)
                                                       select LPAD (seq_opr_bih_transfer.NEXTVAL, 12, '0'),
                                                              a.sourcebedid_chr,
                                                              a.targetbedid_chr,
                                                              6,
                                                              '', ?,                                               
                                                              a.registerid_chr,
                                                              sysdate,
                                                              a.sourcedeptid_chr,
                                                              a.sourceareaid_chr, 
                                                              a.targetdeptid_chr, 
                                                              a.targetareaid_chr     
                                                          from t_opr_bih_transfer a 
                                                         where a.transferid_chr = (
                                                                                    select max(b.transferid_chr) 
                                                                                      from t_opr_bih_transfer b 
                                                                                     where b.type_int = 7                                
                                                                                       and b.registerid_chr = ?)";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = Charge_VO.OperEmp;
                ParamArr[1].Value = RegID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                #region Ӥ���������

                if (Charge_VO.m_objBabyInfoVo != null)
                {

                    for (int i2 = 0; i2 < Charge_VO.m_objBabyInfoVo.Length; i2++)
                    {
                        string strBabyRegisterId = Charge_VO.m_objBabyInfoVo[i2].RegisterID;
                        //��Ժ�ǼǱ�
                        SQL = @"update t_opr_bih_register set pstatus_int = 3, feestatus_int = 4, operatorid_chr = ? where registerid_chr = ?";
                        objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                        ParamArr[0].Value = Charge_VO.OperEmp;
                        ParamArr[1].Value = strBabyRegisterId;

                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                        //��Ժ��¼��
                        SQL = @"update t_opr_bih_leave set modify_dat = sysdate, pstatus_int = 1, operatorid_chr = ? where status_int = 1 and registerid_chr = ?";
                        objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                        ParamArr[0].Value = Charge_VO.OperEmp;
                        ParamArr[1].Value = strBabyRegisterId;

                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                        //��λ��
                        SQL = @"update t_bse_bed set status_int = 1, bihregisterid_chr = '' where bihregisterid_chr = ?";
                        objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                        ParamArr[0].Value = strBabyRegisterId;

                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                        //��ת��Ϣ��
                        SQL = @"insert into t_opr_bih_transfer (transferid_chr, sourcebedid_chr, targetbedid_chr, type_int, des_vchr, operatorid_chr, registerid_chr, 
                                                                            modify_dat, sourcedeptid_chr, sourceareaid_chr, targetdeptid_chr, targetareaid_chr)
                                                       select LPAD (seq_opr_bih_transfer.NEXTVAL, 12, '0'),
                                                              a.sourcebedid_chr,
                                                              a.targetbedid_chr,
                                                              6,
                                                              '', ?,                                               
                                                              a.registerid_chr,
                                                              sysdate,
                                                              a.sourcedeptid_chr,
                                                              a.sourceareaid_chr, 
                                                              a.targetdeptid_chr, 
                                                              a.targetareaid_chr     
                                                          from t_opr_bih_transfer a 
                                                         where a.transferid_chr = (
                                                                                    select max(b.transferid_chr) 
                                                                                      from t_opr_bih_transfer b 
                                                                                     where b.type_int = 7                                
                                                                                       and b.registerid_chr = ?)";

                        objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                        ParamArr[0].Value = Charge_VO.OperEmp;
                        ParamArr[1].Value = strBabyRegisterId;

                        lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);



                    }
                }
                #endregion
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

        #region <����>���������������

        #region ��ȡ�������д���ID
        /// <summary>
        /// ��ȡ�������д���ID
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngMzGetAcctRecipeID(string BeginDate, string EndDate, out DataTable dt)
        {
            long lngRes = 0;
            string SQL = @"select distinct a.outpatrecipeid_chr, a.sbsum_mny,
                                     count (b.outpatrecipeid_chr) as nums
                                from t_opr_outpatientrecipeinv a, t_opr_reciperelation b
                               where a.outpatrecipeid_chr = b.seqid
                                 and a.split_int = 0
                                 and a.acctsum_mny > 0
                                 and a.status_int <> 0
                                 and a.recorddate_dat between to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                                                          and to_date (?, 'yyyy-mm-dd hh24:mi:ss')
                            group by a.outpatrecipeid_chr, a.sbsum_mny
                            order by a.outpatrecipeid_chr";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = BeginDate;
                ParamArr[1].Value = EndDate;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region �������շѴ���ID��ȡ�������
        /// <summary>
        /// �������շѴ���ID��ȡ�������
        /// </summary>
        /// <param name="RecipeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngMzGetRecipeCat(string RecipeID, out DataTable dt)
        {
            long lngRes = 0;
            string SQL = @"select b.itemopcalctype_chr,
                                 sum (round (a.tolprice_mny * a.discount_dec, 2)) as catsum,
                                 sum (a.tolprice_mny) as totalsum
                            from t_opr_oprecipeitemde a, t_bse_chargeitem b
                           where a.itemid_chr = b.itemid_chr(+) and a.outpatrecipeid_chr = ?
                        group by b.itemopcalctype_chr";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = RecipeID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ���ݴ���ID��ȡSEQID�б�
        /// <summary>
        /// ���ݴ���ID��ȡSEQID�б�
        /// </summary>
        /// <param name="RecipeID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngMzGetSeqIDList(string RecipeID, out DataTable dt)
        {
            long lngRes = 0;
            string SQL = @" select a.seqid_chr, a.status_int, a.outpatrecipeid_chr
                              from t_opr_outpatientrecipeinv a
                             where a.outpatrecipeid_chr = ?
                             order by a.outpatrecipeid_chr, a.status_int";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = RecipeID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ����SEQID��ȡ�������
        /// <summary>
        /// ����SEQID��ȡ�������
        /// </summary>
        /// <param name="SeqID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngMzGetChargeCat(string SeqID, out DataTable dt)
        {
            long lngRes = 0;
            string SQL = @"select a.itemcatid_chr, a.tolfee_mny, a.sbsum_mny
                              from t_opr_outpatientrecipesumde a
                             where a.seqid_chr = ?";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = SeqID;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dt, ParamArr);
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

        #region ����SEQID��CatID���º������
        /// <summary>
        /// ����SEQID��CatID���º������
        /// </summary>
        /// <param name="SeqID"></param>
        /// <param name="CatIDArr"></param>
        /// <param name="CatSumArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngMzUpdateChargeCat(string SeqID, List<string> CatIDArr, List<decimal> CatSumArr, string PStatus)
        {
            long lngRes = 0, lngAffects = 0;
            string SQL = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                int sign = 1;
                if (PStatus == "2")
                {
                    sign = -1;
                }

                for (int i = 0; i < CatIDArr.Count; i++)
                {
                    SQL = @"update t_opr_outpatientrecipesumde a
                               set a.sbsum_mny = ?
                             where a.seqid_chr = ? and a.itemcatid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                    ParamArr[0].Value = Convert.ToDecimal(CatSumArr[i].ToString()) * sign;
                    ParamArr[1].Value = SeqID;
                    ParamArr[2].Value = CatIDArr[i].ToString();

                    lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
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

        #endregion

        #region (ҽ��)����ӿ� new
        /// <summary>
        /// (ҽ��)����ӿ�
        /// </summary>
        /// <param name="vyydm">ҽԺ����</param>
        /// <param name="vzyhm">סԺ��</param>
        /// <param name="vzych">סԺ����</param>
        /// <param name="verdm">����ֵ����</param>
        /// <param name="verms">����ֵ��Ϣ</param>
        /// <param name="blnIsInsertDetail">�Ƿ�������ϸ��YBAD12(������ѯʱ������ΪFalse)</param>
        /// <param name="m_strConn">���ݿ������ַ���</param>
        /// <returns></returns>
        [DllImport("ado_zyss.dll")]
        public static extern bool ZYSS_CONN(string vyydm, string vzyhm, int vzych, ref string verdm, ref string verms, bool blnIsInsertDetail, string m_strConn);
        [AutoComplete]
        public long m_lngZYSS(string HospCode, string Zyh, int Zycs, out decimal TotalMoney, out decimal InsuredMoney, out string OutErrMsg)
        {

            string OutDm = "";
            string OutMs = "";
            TotalMoney = 0;
            InsuredMoney = 0;
            OutErrMsg = "";
            try
            {
                string m_strConn = "Provider=OraOLEDB.Oracle.1;";
                m_strConn += HRPService.clsHRPTableService.strbytOracleICare;
                bool ret = ZYSS_CONN(HospCode, Zyh, Zycs, ref OutDm, ref OutMs, true, m_strConn);
                if (!ret)
                {
                    OutErrMsg = OutDm + "   " + OutMs;
                    return 0;
                }
                else
                {
                    if (OutDm.Contains("000"))
                    {
                        OutErrMsg = OutDm + "   " + OutMs;
                        string[] m_strArr = OutMs.Split('��');
                        if (m_strArr.Length == 4)
                        {
                            TotalMoney = Convert.ToDecimal(m_strArr[1].Trim().Replace("ҽ��֧�����", string.Empty));
                            InsuredMoney = Convert.ToDecimal(m_strArr[2].Trim().Replace("����֧��", string.Empty));
                        }

                    }
                    else
                    {
                        OutErrMsg = OutDm + "   " + OutMs;
                        return 0;
                    }

                }
            }
            catch (Exception ex)
            {
                OutErrMsg = ex.Message;
                return 0;
            }
            return 1;

        }
        #region (ҽ��)ҽ������
        /// <summary>
        /// (ҽ��)ҽ������
        /// </summary>
        /// <param name="HospCode"></param>
        /// <param name="RegID"></param>
        /// <param name="Zyh"></param>
        /// <param name="Zycs"></param>
        /// <param name="TotalMoney"></param>
        /// <param name="InsuredMoney"></param>
        /// <param name="OutErrMsg"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngYBBudget(string HospCode, string RegID, string Zyh, int Zycs, out decimal TotalMoney, out decimal InsuredMoney, out string OutErrMsg)
        {
            long lngRes = 0;
            long lngAffects = 0;
            string SQL = "";

            TotalMoney = 0;
            InsuredMoney = 0;
            OutErrMsg = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                SQL = @"delete from ybad10 where trim(zyhm) || to_char(zych) = ( select a.inpatientid_chr || to_char(?)
                                                                                   from t_opr_bih_register a
                                                                                  where a.registerid_chr = ? )";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = Zycs;
                ParamArr[1].Value = RegID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                SQL = @"insert into ybad10 (yydm, zyhm, zych, xinm, xinb, sfzh, csrq, ksdm, ryrq, cyrq, zyts, 
                                            zdfl, zddm, sczt, rylb, dylb, jslx, yyfd, jjfd, ybcs, zfje)
                                    select ?,
                                           a.inpatientid_chr,
                                           ?,
                                           b.lastname_vchr,
                                           b.sex_chr,
                                           b.idcard_chr,
                                           b.birth_dat,
                                           a.deptid_chr,
                                           a.inareadate_dat,
                                           sysdate,
                                           floor(sysdate - a.inareadate_dat),
                                           null,
                                           null,
                                           null,
                                           c.rylb,
                                           null,
                                           c.jslx,
                                           100,
                                           nvl(b.insuredpayscale_dec, 100),
                                           b.insuredpaytime_int,
                                           b.insuredpaymoney_mny 
                                      from t_opr_bih_register a,
                                           t_opr_bih_registerdetail b,
                                           t_opr_bih_ybdefpaytype c          
                                     where a.registerid_chr = b.registerid_chr 
                                       and a.paytypeid_chr = c.paytypeid_chr  
                                       and a.registerid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = HospCode;
                ParamArr[1].Value = Zycs;
                ParamArr[2].Value = RegID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                SQL = @"delete from ybad13 where trim(zyhm) || to_char(zych) = ( select a.inpatientid_chr || to_char(?)
                                                                                   from t_opr_bih_register a
                                                                                  where a.registerid_chr = ? )";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = Zycs;
                ParamArr[1].Value = RegID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

                SQL = @"insert into ybad13 ( yydm, zyhm, zych, xmcode, xmdes, xmunt, xmqnt, xmprc, xmamt, trndate, trnflg) 
                                    select ?,
                                           a.inpatientid_chr,
                                           ?,
                                           c.insuranceid_chr,
                                           max(substr(c.itemname_vchr,0,15)),
                                           max(b.unit_vchr),
                                           sum(b.amount_dec),
                                           max(b.unitprice_dec),
                                           sum(round(b.amount_dec * b.unitprice_dec, 2)),
                                           sysdate,
                                           '0'        
                                      from t_opr_bih_register a,
                                           t_opr_bih_patientcharge b,
                                           t_bse_chargeitem c 
                                     where a.registerid_chr = b.registerid_chr 
                                       and b.chargeitemid_chr = c.itemid_chr 
                                       and length(c.insuranceid_chr) <= 20
                                       and (b.pstatus_int = 1 or b.pstatus_int = 2)
                                       and a.registerid_chr = ? 
                                  group by a.inpatientid_chr, a.inpatientcount_int, c.insuranceid_chr";

                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = HospCode;
                ParamArr[1].Value = Zycs;
                ParamArr[2].Value = RegID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);
                #region old
                //if (lngRes > 0)
                //{
                //    string OutDm = "";
                //    string OutMs = "";

                //    try
                //    {
                //        // bool ret = ZYSS(HospCode, Zyh, Zycs, ref OutDm, ref OutMs);
                //        string m_strConn = "Provider=OraOLEDB.Oracle.1;";
                //        m_strConn += HRPService.clsHRPTableService.strbytOracleICare;
                //        bool ret = ZYSS_CONN(HospCode, Zyh, Zycs, ref OutDm, ref OutMs, true, m_strConn);
                //        if (!ret)
                //        {
                //            OutErrMsg = OutDm + "   " + OutMs;
                //            return 0;
                //        }
                //        else
                //        {
                //            OutErrMsg = OutDm + "   " + OutMs;
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        OutErrMsg = ex.Message;
                //        return 0;
                //    }
                //}

                //                SQL = @"select zl19, yb02 from ybad11 where trim(zyhm) || to_char(zych) = ( select a.inpatientid_chr || to_char(?)
                //                                                                                              from t_opr_bih_register a
                //                                                                                             where a.registerid_chr = ? )";

                //                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                //                ParamArr[0].Value = Zycs;
                //                ParamArr[1].Value = RegID;

                //                DataTable dtMain = new DataTable();

                //                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtMain, ParamArr);

                //                if (lngRes > 0 && dtMain.Rows.Count == 1)
                //                {
                //                    TotalMoney = this.ConvertObjToDecimal(dtMain.Rows[0]["zl19"]);
                //                    InsuredMoney = this.ConvertObjToDecimal(dtMain.Rows[0]["yb02"]);
                //                }
                #endregion
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
        #endregion

        #region ������ת��Ϊ����
        /// <summary>
        /// ������ת��Ϊ����
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public decimal ConvertObjToDecimal(object obj)
        {
            try
            {
                if (obj != null && obj.ToString() != "")
                {
                    return Convert.ToDecimal(obj.ToString());

                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region <����>��Ʊ��Ϣ
        /// <summary>
        /// ���﷢Ʊ��Ϣ 
        /// </summary>
        /// <param name="ChargeNo"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <param name="dtPayMode"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOPInvoiceByChargeNo(string ChargeNo, out DataTable dtMain, out DataTable dtDet, out DataTable dtPayMode)
        {
            long lngRes = 0;
            string SQL = "";
            dtMain = null;
            dtDet = null;
            dtPayMode = null;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                //��Ʊ����Ϣ
                SQL = @"select c.invoiceno_vchr, c.outpatrecipeid_chr, c.invdate_dat, c.acctsum_mny,
                               c.sbsum_mny, c.opremp_chr, c.recordemp_chr, c.recorddate_dat,
                               c.status_int, c.seqid_chr, c.balanceemp_chr, c.balance_dat,
                               c.balanceflag_int, c.totalsum_mny, c.patientid_chr,
                               c.patientname_chr, c.deptid_chr, c.deptname_chr, c.doctorid_chr,
                               c.doctorname_chr, c.confirmemp_chr, a.paytypeid_chr,
                               c.internalflag_int, c.baseseqid_chr, a.groupid_chr,
                               c.confirmdeptid_chr, c.split_int, d.paytypename_vchr, e.empno_chr a,
                               f.empno_chr b, g.patientcardid_chr, h.shortno_chr confdept,
                               e.empno_chr confemp, c.medsendwininfo_vchr,c.totaldiffcost_mny
                         from t_opr_charge a,
                              t_opr_chargedefinv b,
                              t_opr_outpatientrecipeinv c,
                              t_bse_patientpaytype d,
                              t_bse_employee e,
                              t_bse_employee f,
                              t_bse_patientcard g,
                              t_bse_deptdesc h                                                            
                        where a.chargeno_chr = b.chargeno_chr 
                          and b.invoiceno_vchr = c.invoiceno_vchr 
                          and c.paytypeid_chr = d.paytypeid_chr(+)  
                          and a.patientid_chr = g.patientid_chr(+)
                          and a.operemp_chr = e.empid_chr(+)
                          and a.doctorid_chr = f.empid_chr(+)
                          and a.deptid_chr = h.deptid_chr(+)
                          and a.status_int = 1 
                          and a.chargeno_chr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = ChargeNo;
                //((Oracle.DataAccess.Client.OracleParameter)ParamArr[0]).OracleDbType = OracleDbType.Char;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtMain, ParamArr);

                if (lngRes > 0 && dtMain.Rows.Count > 0)
                {
                    //ĿǰĬ��һ�ν����Ӧһ�ŷ�Ʊ                    
                    string invono = "";
                    if (dtMain.Rows.Count == 1)
                    {
                        invono = dtMain.Rows[0]["invoiceno_vchr"].ToString().Trim();

                        //��Ʊ������Ϣ
                        SQL = @"select invoiceno_vchr, itemcatid_chr, abs(sum(tolfee_mny)) as totalsum_mny 
                                  from t_opr_outpatientrecipeinvde 
                                 where invoiceno_vchr = ? 
                              group by invoiceno_vchr, itemcatid_chr";

                        objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                        ParamArr[0].Value = invono;

                        lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtDet, ParamArr);
                    }
                    else
                    {
                        for (int i = 0; i < dtMain.Rows.Count; i++)
                        {
                            invono += "'" + dtMain.Rows[i]["invoiceno_vchr"].ToString().Trim() + "',";
                        }
                        invono = invono.Substring(0, invono.Length - 1);

                        //��Ʊ������Ϣ
                        SQL = @"select invoiceno_vchr, itemcatid_chr, abs(sum(tolfee_mny)) as totalsum_mny 
                                  from t_opr_outpatientrecipeinvde 
                                 where invoiceno_vchr in (" + invono + @") 
                              group by invoiceno_vchr, itemcatid_chr";

                        lngRes = objHRPSvc.lngGetDataTableWithoutParameters(SQL, ref dtDet);
                    }

                    //֧����ʽ
                    SQL = @"select b.paytype_int, b.paysum_mny 
                              from t_opr_charge a,
                                   t_opr_payment b  
                             where a.chargeno_chr = b.chargeno_vchr 
                               and a.status_int = 1                                         
                               and a.chargeno_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = ChargeNo;

                    lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtPayMode, ParamArr);
                }

                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }

        /// <summary>
        /// ���﷢Ʊ��Ϣ 
        /// </summary>
        /// <param name="InvoNo"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <param name="dtPayMode"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOPInvoiceByInvoNo(string InvoNo, out DataTable dtMain, out DataTable dtDet, out DataTable dtPayMode)
        {
            long lngRes = 0;
            string SQL = "";
            dtMain = null;
            dtDet = null;
            dtPayMode = null;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                //��Ʊ����Ϣ
                SQL = @"select c.invoiceno_vchr, c.outpatrecipeid_chr, c.invdate_dat, c.acctsum_mny,
                               c.sbsum_mny, c.opremp_chr, c.recordemp_chr, c.recorddate_dat,
                               c.status_int, c.seqid_chr, c.balanceemp_chr, c.balance_dat,
                               c.balanceflag_int, c.totalsum_mny, c.patientid_chr, c.patientname_chr,
                               c.deptid_chr, c.deptname_chr, c.doctorid_chr, c.doctorname_chr,
                               c.confirmemp_chr, c.paytypeid_chr, c.internalflag_int, c.baseseqid_chr,
                               c.groupid_chr, c.confirmdeptid_chr, c.split_int, d.paytypename_vchr,
                               e.empno_chr a, f.empno_chr b, g.patientcardid_chr,
                               h.shortno_chr confdept, e.empno_chr confemp, c.medsendwininfo_vchr,c.totaldiffcost_mny
                          from t_opr_outpatientrecipeinv c,
                               t_bse_patientpaytype d,
                               t_bse_employee e,
                               t_bse_employee f,
                               t_bse_patientcard g,
                               t_bse_deptdesc h
                         where c.paytypeid_chr = d.paytypeid_chr(+)
                           and c.patientid_chr = g.patientid_chr(+)
                           and c.opremp_chr = e.empid_chr(+)
                           and c.doctorid_chr = f.empid_chr(+)
                           and c.deptid_chr = h.deptid_chr(+)
                           and c.status_int = 1
                           and c.invoiceno_vchr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = InvoNo;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtMain, ParamArr);

                //��Ʊ������Ϣ
                SQL = @"select invoiceno_vchr, itemcatid_chr, abs(sum(tolfee_mny)) as totalsum_mny 
                          from t_opr_outpatientrecipeinvde 
                         where invoiceno_vchr = ? 
                      group by invoiceno_vchr, itemcatid_chr";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = InvoNo;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtDet, ParamArr);

                //֧����ʽ
                SQL = @"select distinct p.paytype_int, p.paysum_mny
                           from t_opr_charge a,
                                t_opr_chargedefinv b,
                                t_opr_outpatientrecipeinv c,
                                t_opr_payment p
                          where a.chargeno_chr = b.chargeno_chr
                            and b.invoiceno_vchr = c.invoiceno_vchr
                            and a.chargeno_chr = p.chargeno_vchr
                            and a.status_int = 1
                            and c.invoiceno_vchr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = InvoNo;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtPayMode, ParamArr);

                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }

        /// <summary>
        /// ���﷢Ʊ��Ϣ
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="InvoNo"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtDet"></param>
        /// <param name="dtPayMode"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOPInvoiceByInvoNo(int mode, string InvoNo, out DataTable dtMain, out DataTable dtDet, out DataTable dtPayMode)
        {
            long lngRes = 0;
            string SQL = "";
            dtMain = null;
            dtDet = null;
            dtPayMode = null;

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                //��Ʊ����Ϣ
                SQL = @"select c.invoiceno_vchr, c.outpatrecipeid_chr, c.invdate_dat, c.acctsum_mny,
                               c.sbsum_mny, c.opremp_chr, c.recordemp_chr, c.recorddate_dat,
                               c.status_int, c.seqid_chr, c.balanceemp_chr, c.balance_dat,
                               c.balanceflag_int, c.totalsum_mny, c.patientid_chr, c.patientname_chr,
                               c.deptid_chr, c.deptname_chr, c.doctorid_chr, c.doctorname_chr,
                               c.confirmemp_chr, c.paytypeid_chr, c.internalflag_int, c.baseseqid_chr,
                               c.groupid_chr, c.confirmdeptid_chr, c.split_int, d.paytypename_vchr,
                               e.empno_chr a, f.empno_chr b, g.patientcardid_chr,
                               h.shortno_chr confdept, e.empno_chr confemp, c.medsendwininfo_vchr
                          from t_opr_outpatientrecipeinv c,
                               t_bse_patientpaytype d,
                               t_bse_employee e,
                               t_bse_employee f,
                               t_bse_patientcard g,
                               t_bse_deptdesc h
                         where c.paytypeid_chr = d.paytypeid_chr(+)
                           and c.patientid_chr = g.patientid_chr(+)
                           and c.opremp_chr = e.empid_chr(+)
                           and c.doctorid_chr = f.empid_chr(+)
                           and c.deptid_chr = h.deptid_chr(+)
                           and c.status_int = 1
                           and c.invoiceno_vchr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = InvoNo;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtMain, ParamArr);

                //��Ʊ������Ϣ
                SQL = @"select invoiceno_vchr, itemcatid_chr,
                               sum (abs (tolfee_mny)) / count (itemcatid_chr) as totalsum_mny
                          from t_opr_outpatientrecipeinvde
                         where invoiceno_vchr = ?
                      group by invoiceno_vchr, itemcatid_chr";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = InvoNo;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtDet, ParamArr);

                //֧����ʽ
                SQL = @"select distinct p.paytype_int
                          from t_opr_charge a,
                               t_opr_chargedefinv b,
                               t_opr_outpatientrecipeinv c,
                               t_opr_payment p
                         where a.chargeno_chr = b.chargeno_chr
                           and b.invoiceno_vchr = c.invoiceno_vchr
                           and a.chargeno_chr = p.chargeno_vchr
                           and a.status_int = 1
                           and c.invoiceno_vchr = ?";

                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = InvoNo;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(SQL, ref dtPayMode, ParamArr);

                objHRPSvc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ���Ĳ��˷��ú˶�״̬
        /// <summary>
        /// ���Ĳ��˷��ú˶�״̬
        /// </summary>
        /// <param name="RegisterID"></param>
        /// <param name="CheckStatus"></param>
        [AutoComplete]
        public long m_lngUpdatePatientChargeCheckStatus(string RegisterID, string CheckStatus)
        {
            long lngRes = 0, lngAffects = 0;
            string SQL = "";

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                SQL = @"update t_opr_bih_register a
                           set a.checkstatus_int = ?
                         where a.registerid_chr = ?";

                objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                ParamArr[0].Value = CheckStatus;
                ParamArr[1].Value = RegisterID;

                lngRes = objHRPSvc.lngExecuteParameterSQL(SQL, ref lngAffects, ParamArr);

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

        #region ��ȡ�����б�
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDepts(out DataTable dtbResult)
        {
            dtbResult = null;
            long lngRes = -1;
            string SQL = @"select t.deptname_vchr,t.wbcode_chr,t.pycode_chr,t.usercode_vchr,t.deptid_chr from t_bse_deptdesc t";
            try
            {
                clsHRPTableService objHRPTSvc = new clsHRPTableService();
                lngRes = objHRPTSvc.lngGetDataTableWithoutParameters(SQL, ref dtbResult);
                objHRPTSvc.Dispose();
                objHRPTSvc = null;
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

        #region �����Ƿ������Ӧ֢��־
        /// <summary>
        /// �����Ƿ������Ӧ֢��־
        /// </summary>
        /// <param name="m_glstSFLB"></param>
        /// <param name="p_strEmpID"></param>
        /// <param name="p_strEmpName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSetChargeSFLB_Zjwsy(List<clsSFLB_log> m_glstSFLB, string p_strEmpID, string p_strEmpName)
        {
            long lngRes = -1;
            try
            {
                DateTime dtmNow = DateTime.Now;
                string strSQL = @"insert into t_opr_bih_changesflb_log
                                      (seq_vchr, pchargeid_chr, empid_vchr, empname_vchr, createdat_dat,
                                       newsflbbh_vchr, oldsflbbh_vchr, itemid_chr)
                                    values
                                      (seq_changesflb_log.nextval, ?, ?, ?, sysdate, ?, ?, ?)";
                DbType[] dbTypes = new DbType[] { DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String };
                object[][] objValues = new object[dbTypes.Length][];

                for (int i = 0; i < objValues.Length; i++)
                {
                    objValues[i] = new object[m_glstSFLB.Count];
                }

                int n = 0;
                for (int i = 0; i < m_glstSFLB.Count; i++)
                {
                    n = 0;
                    objValues[n++][i] = m_glstSFLB[i].m_strPChargeID;
                    objValues[n++][i] = p_strEmpID;
                    objValues[n++][i] = p_strEmpName;
                    objValues[n++][i] = m_glstSFLB[i].m_strNewSFLBBH;
                    objValues[n++][i] = m_glstSFLB[i].m_strOLDSFLBBH;
                    objValues[n++][i] = m_glstSFLB[i].m_strItemID;
                }
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);

                strSQL = "update t_opr_bih_patientcharge a set a.itemchargetype_vchr = ? where a.pchargeid_chr = ?";
                dbTypes = new DbType[] { DbType.String, DbType.String };
                objValues = new object[dbTypes.Length][];

                for (int i = 0; i < objValues.Length; i++)
                {
                    objValues[i] = new object[m_glstSFLB.Count];
                }

                n = 0;
                for (int i = 0; i < m_glstSFLB.Count; i++)
                {
                    n = 0;
                    objValues[n++][i] = m_glstSFLB[i].m_strNewSFLBBH;
                    objValues[n++][i] = m_glstSFLB[i].m_strPChargeID;
                }
                lngRes = objHRPSvc.m_lngSaveArrayWithParameters(strSQL, objValues, dbTypes);
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

        #region ����籣�����籣����û�гɹ��Ͳ�����HIS����
        /// <summary>
        /// ����籣�����籣����û�гɹ��Ͳ�����HIS����
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool m_blnCheckYBChargeSuccessFull(string p_strRegisterID)
        {
            DataTable dtbResult = new DataTable();
            bool blnSuccess = false; // true ��ʾ �籣���� û�����籣���㣬false ��ʾ ���˿���HIS����
            long lngRes = -1;
            string SQL = @"select t.status_chr
                              from t_ins_cszyreg t
                             where t.registerid_vchr = ?
                               and t.status_chr > -1";
            try
            {
                clsHRPTableService objHRPTSvc = new clsHRPTableService();
                IDataParameter[] Parars = null;
                objHRPTSvc.CreateDatabaseParameter(1, out Parars);
                Parars[0].Value = p_strRegisterID;
                lngRes = objHRPTSvc.lngGetDataTableWithParameters(SQL, ref dtbResult, Parars);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    if (dtbResult.Rows[0]["status_chr"].ToString() == "0")
                    {
                        blnSuccess = true;
                    }
                }
                objHRPTSvc.Dispose();
                objHRPTSvc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return blnSuccess;
        }
        #endregion

        #region ��˷���

        #region ��ȡ���߷��������Ϣ
        /// <summary>
        /// ��ȡ���߷��������Ϣ
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetPatientCheckFee(string registerId)
        {
            DataTable dt = null;
            string Sql = string.Empty;
            try
            {
                Sql = @"select t.registerid,
                               t.ischeckfee,
                               t.checkfeeoperid,
                               t.checkfeedate,
                               t.canceloperid,
                               t.canceldate
                          from t_opr_bih_checkfee t
                         where t.registerid = ?";

                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = registerId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();
                svc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dt;
        }
        #endregion

        #region ���滼�߷��������Ϣ
        /// <summary>
        /// ���滼�߷��������Ϣ
        /// </summary>
        /// <param name="registerId"></param>
        /// <param name="operId"></param>
        /// <returns></returns>
        [AutoComplete]
        public int SavePatientCheckFee(string registerId, string operId)
        {
            long ret = 0;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                DataTable dt = null;

                #region �ٴ�У������. ���������籣�ϴ�ʧ�� 10Ԫ��  --> 2019-11-13 

                Sql = @"select t.pchargeid_chr,
                               t.unitprice_dec,
                               t.amount_dec,
                               t.buyprice_dec,
                               t.totaldiffcostmoney_dec
                          from t_opr_bih_patientcharge t
                         where t.registerid_chr = ?";

                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = registerId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    decimal decProfit = 0;
                    decimal decNew = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["totaldiffcostmoney_dec"] != DBNull.Value && Function.Dec(dr["totaldiffcostmoney_dec"]) != 0 &&
                            Function.Dec(dr["buyprice_dec"]) != 0 && Function.Dec(dr["amount_dec"]) != 0)
                        {
                            decProfit = Function.Round(Function.Dec(dr["totaldiffcostmoney_dec"]), 2);
                            decNew = Function.Round((Function.Dec(dr["unitprice_dec"]) - Function.Dec(dr["buyprice_dec"])) * Function.Dec(dr["amount_dec"]), 2);
                            if (Math.Abs(decProfit) != Math.Abs(decNew) && decNew != 0)
                            {
                                Sql = "update t_opr_bih_patientcharge set totaldiffcostmoney_dec = ? where pchargeid_chr = ?";
                                svc.CreateDatabaseParameter(2, out parm);
                                parm[0].Value = decNew * -1;
                                parm[1].Value = dr["pchargeid_chr"].ToString();
                                svc.lngExecuteParameterSQL(Sql, ref ret, parm);
                            }
                        }
                    }
                }
                #endregion

                dt = this.GetPatientCheckFee(registerId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Sql = @"update t_opr_bih_checkfee
                               set ischeckfee = 1, checkfeeoperid = ?, checkfeedate = sysdate
                             where registerid = ?";
                }
                else
                {
                    Sql = @"insert into t_opr_bih_checkfee
                              (ischeckfee, checkfeeoperid, checkfeedate, registerid)
                            values
                              (1, ?, sysdate, ?)";
                }
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = operId;
                parm[1].Value = registerId;
                svc.lngExecuteParameterSQL(Sql, ref ret, parm);
                svc.Dispose();
                svc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return (int)ret;
        }
        #endregion

        #region ȡ�����߷��������Ϣ
        /// <summary>
        /// ȡ�����߷��������Ϣ
        /// </summary>
        /// <param name="registerId"></param>
        /// <param name="operId"></param>
        /// <returns></returns>
        [AutoComplete]
        public int CancelPatientCheckFee(string registerId, string operId)
        {
            long ret = 0;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;

                Sql = @"update t_opr_bih_checkfee
                           set ischeckfee = 0, canceloperid = ?, canceldate = sysdate
                         where registerid = ?";

                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = operId;
                parm[1].Value = registerId;
                svc.lngExecuteParameterSQL(Sql, ref ret, parm);
                svc.Dispose();
                svc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return (int)ret;
        }
        #endregion

        #endregion

        #region �ѷ�(��)�ڷ�ҩ�������˷�
        /// <summary>
        /// �ѷ�(��)�ڷ�ҩ�������˷�
        /// </summary>
        /// <param name="pchargeId"></param>
        /// <returns></returns>
        [AutoComplete]
        public bool IsCanRefundment(string pchargeId)
        {
            bool isCan = true;
            DataTable dt = null;
            string Sql = string.Empty;
            try
            {
                Sql = @"select a.putmeddetailid_chr, b.medicineid_chr, b.medicinename_vchr
                          from t_bih_opr_putmeddetail a
                         inner join t_bse_medicine b
                            on a.medid_chr = b.medicineid_chr
                         inner join t_aid_medicinepreptype c
                            on b.medicinepreptype_chr = c.medicinepreptype_chr
                           and c.flaga_int = 1
                         where a.status_int = 1
                           and a.isput_int = 1
                           and a.pchargeid_chr = ?";

                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = pchargeId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    isCan = false;
                }
                svc.Dispose();
                svc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return isCan;
        }
        #endregion

        #region ������ɫ����

        #region GetEmpInfo
        /// <summary>
        /// GetEmpInfo
        /// </summary>
        /// <param name="empNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetEmpInfo(string empNo)
        {
            DataTable dt = null;
            string Sql = string.Empty;
            try
            {
                Sql = @"select a.empid_chr,
                               a.firstname_vchr,
                               a.lastname_vchr,
                               a.empno_chr,
                               a.psw_chr,
                               a.technicalrank_chr,
                               b.deptid_chr,
                               b.default_inpatient_dept_int
                          from t_bse_employee a, t_bse_deptemp b
                         where a.status_int = 1
                           and a.empid_chr = b.empid_chr
                           and a.empno_chr = ?";

                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = empNo;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();
                svc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dt;
        }
        #endregion

        #region AddCaseRole
        /// <summary>
        /// AddCaseRole
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        [AutoComplete]
        public int AddCaseRole(EntityLogSetCaseRole vo)
        {
            long ret = 0;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                int n = 0;
                string mapId = string.Empty;
                svc.lngGenerateID(7, "MAPID_CHR", "T_SYS_EMPROLEMAP", out mapId);

                Sql = @"insert into t_sys_emprolemap
                          (mapid_chr, typeid_chr, empid_chr, roleid_chr)
                        values
                          (?, ?, ?, ?)";

                svc.CreateDatabaseParameter(4, out parm);
                n = -1;
                parm[++n].Value = mapId;
                parm[++n].Value = "1";
                parm[++n].Value = vo.empId;
                parm[++n].Value = vo.roleId;
                svc.lngExecuteParameterSQL(Sql, ref ret, parm);

                Sql = @"insert into t_log_setcaserole
                          (serno,
                           mapid,
                           empid,
                           roleid,
                           areaid,
                           giveoperid,
                           givedate,
                           recycleoperid,
                           recycledate,
                           status)
                        values
                          (seq_setcaserole.nextval, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                svc.CreateDatabaseParameter(9, out parm);
                n = -1;
                parm[++n].Value = mapId;
                parm[++n].Value = vo.empId;
                parm[++n].Value = vo.roleId;
                parm[++n].Value = vo.areaId;
                parm[++n].Value = vo.giveOperId;
                parm[++n].Value = DateTime.Now;
                parm[++n].Value = null;
                parm[++n].Value = null;
                parm[++n].Value = 1;
                svc.lngExecuteParameterSQL(Sql, ref ret, parm);

                svc.Dispose();
                svc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return (int)ret;
        }
        #endregion

        #region DelCaseRole
        /// <summary>
        /// DelCaseRole
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        [AutoComplete]
        public int DelCaseRole(EntityLogSetCaseRole vo)
        {
            long ret = 0;
            string Sql = string.Empty;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                int n = 0;

                Sql = @"delete from t_sys_emprolemap where mapid_chr = ?";
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = vo.mapId;
                svc.lngExecuteParameterSQL(Sql, ref ret, parm);

                Sql = @"update t_log_setcaserole
                           set recycleoperid = ?, recycledate = ?, status = ?
                         where serno = ?";

                svc.CreateDatabaseParameter(4, out parm);
                n = -1;
                parm[++n].Value = vo.recycleOperId;
                parm[++n].Value = DateTime.Now;
                parm[++n].Value = 2;
                parm[++n].Value = vo.serNo;
                svc.lngExecuteParameterSQL(Sql, ref ret, parm);

                svc.Dispose();
                svc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return (int)ret;
        }
        #endregion

        #region QueryCaseRole
        /// <summary>
        /// QueryCaseRole
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="doctCode"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable QueryCaseRole(string startDate, string endDate, string doctCode)
        {
            DataTable dt = null;
            string Sql = string.Empty;
            try
            {
                Sql = @"select a.serno,
                               a.mapid,
                               a.empid,
                               b.lastname_vchr as empname,
                               a.roleid,
                               c.name_vchr as rolename,
                               a.areaid,
                               d.deptname_vchr as areaname,
                               a.giveoperid,
                               e.lastname_vchr as giveopername,
                               to_char(a.givedate, 'yyyy-mm-dd hh24:mi') as givetime,
                               a.recycleoperid,
                               f.lastname_vchr as recycleopername,
                               to_char(a.recycledate, 'yyyy-mm-dd hh24:mi') as recycledate,
                               status,
                               (case status
                                 when 1 then
                                  '��Ȩ'
                                 when 2 then
                                  '����'
                               end) as statusname
                          from t_log_setcaserole a
                         inner join t_bse_employee b
                            on a.empid = b.empid_chr
                         inner join t_sys_role c
                            on a.roleid = c.roleid_chr
                          left join t_bse_deptdesc d
                            on a.areaid = d.deptid_chr
                         inner join t_bse_employee e
                            on a.giveoperid = e.empid_chr
                          left join t_bse_employee f
                            on a.recycleoperid = f.empid_chr 
                        ";

                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate) && !string.IsNullOrEmpty(doctCode))
                {
                    Sql += @"where (a.givedate between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                    to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                               and b.empno_chr = ?";

                    svc.CreateDatabaseParameter(3, out parm);
                    parm[0].Value = startDate + " 00:00:00";
                    parm[1].Value = endDate + " 23:59:59";
                    parm[2].Value = doctCode;
                }
                else
                {
                    if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate) && string.IsNullOrEmpty(doctCode))
                    {
                        Sql += @"where (a.givedate between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                        to_date(?, 'yyyy-mm-dd hh24:mi:ss'))";

                        svc.CreateDatabaseParameter(2, out parm);
                        parm[0].Value = startDate + " 00:00:00";
                        parm[1].Value = endDate + " 23:59:59";
                    }
                    if (string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate) && !string.IsNullOrEmpty(doctCode))
                    {
                        Sql += @"where b.empno_chr = ?";

                        svc.CreateDatabaseParameter(1, out parm);
                        parm[0].Value = doctCode;
                    }
                }
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();
                svc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dt;
        }
        #endregion

        #endregion

        #region ��Ʊ�˿�ԭ��

        #region ��ѯ��Ʊ�˿�ԭ��ģ��
        /// <summary>
        /// ��ѯ��Ʊ�˿�ԭ��ģ��
        /// </summary>
        /// <param name="flagId"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetRefundReasonList(int flagId)
        {
            DataTable dt = null;
            string Sql = string.Empty;
            try
            {
                Sql = @"select flagId, fno, freason
                          from t_aid_refundreason
                         where flagId = ?
                         order by to_number(fno) ";

                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(1, out parm);
                parm[0].Value = flagId;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                svc.Dispose();
                svc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dt;
        }
        #endregion

        #region ��ѯ��Ʊ�˿�ԭ��
        /// <summary>
        /// ��ѯ��Ʊ�˿�ԭ��
        /// </summary>
        /// <param name="flagId"></param>
        /// <param name="invoNo"></param>
        /// <returns></returns>
        [AutoComplete]
        public EntityInvoiceRefundReason GetInvoiceRefundReason(int flagId, string invoNo)
        {
            EntityInvoiceRefundReason vo = null;
            DataTable dt = null;
            string Sql = string.Empty;
            try
            {
                Sql = @"select a.flagid,
                               a.invono,
                               a.reason,
                               a.operid,
                               a.operdate,
                               b.lastname_vchr as operName
                          from t_invoice_refundreason a
                          left join t_bse_employee b
                            on a.operid = b.empid_chr
                         where a.flagid = ?
                           and a.invono = ?
                           and a.status = 1 ";

                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = flagId;
                parm[1].Value = invoNo;
                svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    vo = new EntityInvoiceRefundReason();
                    vo.flagId = Convert.ToInt32(dr["flagid"].ToString());
                    vo.invoNo = dr["invono"].ToString();
                    vo.reason = dr["reason"].ToString();
                    vo.operId = dr["operid"].ToString();
                    vo.operName = dr["operName"].ToString();
                    vo.operDate = Convert.ToDateTime(dr["operdate"].ToString());
                }
                svc.Dispose();
                svc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return vo;
        }
        #endregion

        #region ���淢Ʊ�˿�ԭ��
        /// <summary>
        /// ���淢Ʊ�˿�ԭ��
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        [AutoComplete]
        public int SaveInvoiceRefundReason(EntityInvoiceRefundReason vo)
        {
            long affectRows = 0;
            string Sql = string.Empty;
            try
            {
                EntityInvoiceRefundReason tmpVo = GetInvoiceRefundReason(vo.flagId, vo.invoNo);
                if (tmpVo == null)
                {
                    Sql = @"insert into t_invoice_refundreason
                              (reason, operid, operdate, status, flagid, invono)
                            values
                              (?, ?, ?, ?, ?, ?)";
                }
                else
                {
                    Sql = @"update t_invoice_refundreason
                               set reason = ?, operid = ?, operdate = ?, status = ? 
                             where flagid = ?
                               and invono = ?";
                }

                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] parm = null;
                svc.CreateDatabaseParameter(6, out parm);
                parm[0].Value = vo.reason;
                parm[1].Value = vo.operId;
                parm[2].DbType = DbType.DateTime;
                parm[2].Value = vo.operDate;
                parm[3].Value = vo.status;
                parm[4].Value = vo.flagId;
                parm[5].Value = vo.invoNo;
                svc.lngExecuteParameterSQL(Sql, ref affectRows, parm);
                svc.Dispose();
                svc = null;
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return (int)affectRows;
        }
        #endregion

        #endregion
    }
    #endregion

    #region clsChargeQuery
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(true)]
    public class clsChargeQuery : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ����סԺ�Ǽ���ˮ�Ż�ȡ����Ч����(����״̬)������Ϣ
        /// <summary>
        /// ����סԺ�Ǽ���ˮ�Ż�ȡ����Ч����(����״̬)������Ϣ
        /// </summary>
        /// <param name="RegID">סԺ�Ǽ���ˮ��</param>
        /// <param name="ActiveType">��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ���շ�;888=����״̬����;999=ȫ��}</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetFeeItemByActiveType(string RegID, int ActiveType, string Pstatus, string AreaID, string BeginDate, string EndDate, out DataTable dt)
        {
            long lngRes = 0;

            string Sql = @"select a.pchargeid_chr, a.patientid_chr, a.registerid_chr, a.active_dat,
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
                                   a.chargedoctor_vchr, a.chargedoctorgroupid_chr, a.pchargeidorg_chr, 
                                   d.deptname_vchr as execarea, e.itemcode_vchr, e.itemspec_vchr, e.insuranceid_chr as ybcode, 
                                   (case a.pstatus_int
                                      when 3
                                         then a.discount_dec
                                      when 4
                                         then a.discount_dec
                                      else c.precent_dec
                                    end
                                   ) as precent_dec, (a.amount_dec * a.unitprice_dec) as totalmony,
                                  f.recipeno2_int as recno, e.itemopcode_chr, e.itempycode_chr, 0 as chargetotalsum,a.itemchargetype_vchr,
                                  a.totaldiffcostmoney_dec, a.buyprice_dec, 0 as rptStatus, e.itemsex, e.itemunit2       
                             from t_opr_bih_patientcharge a,
                                  t_opr_bih_register b,
                                  t_aid_inschargeitem c,
                                  t_bse_deptdesc d,                                           
                                  t_bse_chargeitem e,
                                  t_opr_bih_order f                                      
                            where a.registerid_chr = ?                                     
                              and a.registerid_chr = b.registerid_chr 
                              and a.chargeitemid_chr = c.itemid_chr 
                              and b.paytypeid_chr = c.copayid_chr 
                              and a.clacarea_chr = d.deptid_chr(+)
                              and a.chargeitemid_chr = e.itemid_chr(+) 
                              and a.orderid_chr = f.orderid_chr(+)   
                              and a.status_int = 1 ";

            dt = new DataTable();
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                if (ActiveType == 888)
                {
                    Sql += "and a.pstatus_int = ?";

                    svc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = RegID;
                    ParamArr[1].Value = Pstatus;
                }
                else if (ActiveType == 999)
                {
                    if (AreaID == "00")
                    {
                        Sql += " and a.pstatus_int <> 0 and (a.chargeactive_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))";

                        svc.CreateDatabaseParameter(3, out ParamArr);
                        ParamArr[0].Value = RegID;
                        ParamArr[1].Value = BeginDate + " 00:00:00";
                        ParamArr[2].Value = EndDate + " 23:59:59";
                    }
                    else
                    {
                        Sql += " and a.pstatus_int <> 0 and a.curareaid_chr = ? and (a.chargeactive_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss'))";

                        svc.CreateDatabaseParameter(4, out ParamArr);
                        ParamArr[0].Value = RegID;
                        ParamArr[1].Value = AreaID;
                        ParamArr[2].Value = BeginDate + " 00:00:00";
                        ParamArr[3].Value = EndDate + " 23:59:59";
                    }
                }
                else
                {
                    Sql += "and a.activatetype_int = ?";

                    svc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = RegID;
                    ParamArr[1].Value = ActiveType;
                }

                lngRes = svc.lngGetDataTableWithParameters(Sql, ref dt, ParamArr);
                svc.Dispose();
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

        #region GetFeeItemByActiveTypeLis
        /// <summary>
        /// GetFeeItemByActiveTypeLis
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public DataTable GetFeeItemByActiveTypeLis(DataTable dt)
        {
            string Sql = string.Empty;
            DataTable dtRpt = null;
            DataTable dtTmp = null;
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                string orderId = string.Empty;
                List<string> lstOrderId = new List<string>();
                foreach (DataRow dr in dt.Rows)
                {
                    orderId = dr["orderid_chr"].ToString();
                    if (lstOrderId.IndexOf(orderId) < 0)
                    {
                        lstOrderId.Add(orderId);
                    }
                }
                Sql = @"select distinct e.sourceitemid_vchr as orderId, d.confirm_dat
                              from t_opr_lis_application a
                             inner join t_opr_lis_sample c
                                on a.application_id_chr = c.application_id_chr
                             inner join t_opr_lis_app_report d
                                on c.application_id_chr = d.application_id_chr
                             inner join t_opr_attachrelation e
                                on a.application_id_chr = e.attachid_vchr
                             where e.attachtype_int = 3
                               and e.sourceitemid_vchr in ({0}) 
                               and d.confirm_dat is not null";

                int times = 0;
                int perCount = 100;     // 100ҽ��idһ��.in���
                int remainCount = 0;
                List<string> lstOrderId2 = new List<string>();
                times = lstOrderId.Count / perCount;
                for (int i = 0; i < times; i++)
                {
                    orderId = string.Empty;
                    for (int j = 0; j < perCount; j++)
                    {
                        orderId += "'" + lstOrderId[j + i * perCount] + "',";
                    }
                    lstOrderId2.Add(orderId.TrimEnd(','));
                }
                // ʣ��
                remainCount = lstOrderId.Count - perCount * times;
                if (remainCount > 0)
                {
                    orderId = string.Empty;
                    for (int i = 0; i < remainCount; i++)
                    {
                        orderId += "'" + lstOrderId[i + times * perCount] + "',";
                    }
                    lstOrderId2.Add(orderId.TrimEnd(','));
                }
                foreach (string item in lstOrderId2)
                {
                    string Sql1 = string.Format(Sql, item.TrimEnd(','));
                    svc.lngGetDataTableWithoutParameters(Sql1, ref dtTmp);
                    if (dtTmp != null && dtTmp.Rows.Count > 0)
                    {
                        if (dtRpt == null) dtRpt = dtTmp.Clone();

                        if (dtRpt != null && dtTmp != null && dtTmp.Rows.Count > 0)
                            dtRpt.Merge(dtTmp);
                        dtRpt.AcceptChanges();
                    }
                    dtTmp = null;
                }
                svc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return dtRpt;
        }
        #endregion

        #region GetFeeItemByActiveTypePacs
        /// <summary>
        /// GetFeeItemByActiveTypePacs
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public List<string> GetFeeItemByActiveTypePacs(DataTable dt)
        {
            string Sql = string.Empty;
            List<string> lstOrderId2 = new List<string>();
            try
            {
                clsHRPTableService svc = new clsHRPTableService();
                string orderId = string.Empty;
                DataRow[] drr = null;
                List<string> lstOrderId = new List<string>();
                foreach (DataRow dr in dt.Rows)
                {
                    orderId = dr["orderid_chr"].ToString();
                    if (lstOrderId.IndexOf(orderId) < 0)
                    {
                        lstOrderId.Add(orderId);
                    }
                }
                DataTable dtTmp = null;
                int times = 0;
                int perCount = 100;     // 100ҽ��idһ��.in���
                int remainCount = 0;
                times = lstOrderId.Count / perCount;
                for (int i = 0; i < times; i++)
                {
                    orderId = string.Empty;
                    for (int j = 0; j < perCount; j++)
                    {
                        orderId += "'" + lstOrderId[j + i * perCount] + "',";
                    }
                    lstOrderId2.Add(orderId.TrimEnd(','));
                }
                // ʣ��
                remainCount = lstOrderId.Count - perCount * times;
                if (remainCount > 0)
                {
                    orderId = string.Empty;
                    for (int i = 0; i < remainCount; i++)
                    {
                        orderId += "'" + lstOrderId[i + times * perCount] + "',";
                    }
                    lstOrderId2.Add(orderId.TrimEnd(','));
                }

                #region pacs

                Sql = @"select distinct a.attachid_vchr as applyId, a.sourceitemid_vchr as orderId
                              from t_opr_attachrelation a
                             where a.attachtype_int <> 3
                               and a.sourceitemid_vchr in ({0})";

                DataTable dtApplyId = null;
                foreach (string item in lstOrderId2)
                {
                    Sql = string.Format(Sql, item.TrimEnd(','));
                    svc.lngGetDataTableWithoutParameters(Sql, ref dtTmp);
                    if (dtTmp != null && dtTmp.Rows.Count > 0)
                    {
                        if (dtApplyId == null) dtApplyId = dtTmp.Clone();

                        if (dtApplyId != null && dtTmp != null && dtTmp.Rows.Count > 0)
                            dtApplyId.Merge(dtTmp);
                        dtApplyId.AcceptChanges();
                    }
                    dtTmp = null;
                }
                lstOrderId2.Clear();
                if (dtApplyId != null && dtApplyId.Rows.Count > 0)
                {
                    string applyId = string.Empty;
                    string applyIdArr = string.Empty;
                    List<string> lstApplyId = new List<string>();
                    foreach (DataRow dr in dtApplyId.Rows)
                    {
                        applyId = dr["applyId"].ToString();
                        if (lstApplyId.IndexOf(applyId) < 0)
                        {
                            applyIdArr += "'" + applyId + "',";
                            lstApplyId.Add(applyId);
                        }
                    }

                    DataTable dtPacs = null;
                    clsHRPTableService svcPacs = null;
                    try
                    {
                        Sql = @"select t.sqdh
                                  from pacstohispwoer t
                                 where t.jczt in ('�Ѵ�ӡ����', '�Ѽ��δ����', '�ѱ���', '�����')
                                   and t.sqdh in ({0})";
                        svcPacs = new clsHRPTableService();
                        svcPacs.m_mthSetDataBase_Selector(1, 16);
                        svcPacs.lngGetDataTableWithoutParameters(string.Format(Sql, applyIdArr.TrimEnd(',')), ref dtPacs);
                        if (dtPacs != null && dtPacs.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dtPacs.Rows)
                            {
                                drr = dtApplyId.Select("applyId = '" + dr2["sqdh"].ToString() + "'");
                                if (drr != null && drr.Length > 0)
                                {
                                    lstOrderId2.Add(drr[0]["orderId"].ToString());
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        objLogger.LogError(ex);
                    }
                    finally
                    {
                        svcPacs.Dispose();
                        svcPacs = null;
                    }
                }
                #endregion
                svc.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lstOrderId2;
        }
        #endregion

    }
    #endregion

    #region EntityInvoCate
    /// <summary>
    /// EntityInvoCate
    /// </summary>
    public class EntityInvoCate
    {
        public string catId { get; set; }
        public decimal catOrigSum { get; set; }
        public decimal catFactSum { get; set; }
    }
    #endregion

}
