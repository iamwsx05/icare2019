using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.EnterpriseServices; 
using com.digitalwave.Utility;
using weCare.Core.Entity;
using com.digitalwave.iCare.middletier.HRPService;

namespace com.digitalwave.iCare.middletier.HIS.Report
{
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    public class clsReportZY_Svc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsReportZY_Svc()
        {

        }
        #endregion

        #region ��Ŀͳ�Ʒ�����ϸ����
        /// <summary>
        /// ��Ŀͳ�Ʒ�����ϸ����
        /// </summary>
        /// <param name="CodeNo">��Ŀ����</param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeptIDArr"></param>
        /// <param name="dtCharge"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngRptItemDetailStat(string CodeNo, string BeginDate, string EndDate, List<string> DeptIDArr, out DataTable dt)
        {
            long lngRes = 0;
            string SQL = "";
            string SubStr = "";

            SubStr = @" and e.itemcode_vchr = ? and (a.chargeactive_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and to_date(?, 'yyyy-mm-dd hh24:mi:ss')) ";

            if (DeptIDArr != null && DeptIDArr.Count > 0)
            {
                string str = "";

                for (int i = 0; i < DeptIDArr.Count; i++)
                {
                    str += "a.createarea_chr = '" + DeptIDArr[i].ToString() + "' or ";
                }

                str = str.Trim();
                SubStr += " and (" + str.Substring(0, str.Length - 2) + ")";
            }

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;

                SQL = @" select  e.itemcode_vchr, a.chargeitemname_chr, e.itemspec_vchr,
                                 c.deptname_vchr, d.inpatientid_chr, b.lastname_vchr, create_dat,
                                 a.amount_dec, a.totalmoney_dec, a.chearaccount_dat
                            from t_opr_bih_patientcharge a,
                                 t_bse_patient b,
                                 t_bse_deptdesc c,
                                 t_opr_bih_register d,
                                 t_bse_chargeitem e
                           where a.patientid_chr = b.patientid_chr
                             and a.createarea_chr = c.deptid_chr
                             and a.registerid_chr = d.registerid_chr
                             and a.chargeitemid_chr = e.itemid_chr
                             and a.status_int = 1 " + SubStr + @"
                        order by c.deptname_vchr, a.chargeactive_dat";

                objHRPSvc.CreateDatabaseParameter(3, out ParamArr);
                ParamArr[0].Value = CodeNo;
                ParamArr[1].Value = BeginDate + " 00:00:00";
                ParamArr[2].Value = EndDate + " 23:59:59";

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
        #region ��ȡ���ܿ���רҵ�����ͳ������
        /// <summary>
        /// ��ȡ���ܿ���רҵ�����ͳ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="objvalue_Param"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetGroupInComeByDoctor(ref clsGroupInComeByDoctorOrArea_VO objvalue_Param, ref DataTable dtbResult)
        {
            long lngRes = 0;

            clsHRPTableService objHRPServ = new clsHRPTableService();
            StringBuilder sbdSQL = new StringBuilder(
                    @"select c.groupname_vchr as groupname_vchr,
                             tb.groupname_chr as itemgroupname_chr,
                             sum(a.totalmoney_dec) as totalmoney_dec
                      from t_opr_bih_chargeitementry a
                        inner join t_opr_bih_charge b on a.chargeno_chr = b.chargeno_chr
                        inner join t_bse_groupdesc c  on a.doctorgroupid_chr = c.groupid_chr    
                        inner join
                            (select b.groupid_chr, b.groupname_chr, c.typeid_chr
                                from t_aid_rpt_def a 
                                    inner join t_aid_rpt_gop_def b  on a.rptid_chr = b.rptid_chr
                                    inner join t_aid_rpt_gop_rla c  on b.rptid_chr = c.rptid_chr
                                        and b.groupid_chr = c.groupid_chr
                                        and a.rptid_chr = ?) tb  on a.calccateid_chr = tb.typeid_chr
                     where b.status_int = 1
                        and b.recflag_int = 1
                        and a.createarea_chr = ?
                        and b.recdate_dat >= ? 
                        and b.recdate_dat <= ? 
                    group by tb.groupname_chr, c.groupname_vchr ");

            try
            {

                IDataParameter[] objDPArr = null;

                lngRes = objHRPServ.lngGetDataTableWithParameters(sbdSQL.ToString(), ref dtbResult, objDPArr);

                objHRPServ.Dispose();
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                dtbResult = null;
            }
            objHRPServ.Dispose();
            return lngRes;
        }
        #endregion

        #region ��ѯ������Ϣ
        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="strCode">��ѯ�ַ���</param>
        /// <param name="arrArea"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindArea(string strCode, out clsBIHArea[] arrArea)
        {
            string strSql = @"select deptid_chr, deptname_vchr, shortno_chr, pycode_chr, code_vchr
                                from t_bse_deptdesc
                               where attributeid = '0000003' and status_int = 1
                            order by deptid_chr";

            strCode = strCode.Trim();
            if (strCode.Length <= 0)
            {
                strSql = strSql.Replace("[FindCondition]", "");
            }
            else if ((strCode[0] >= '0') && (strCode[0] <= '9'))
            {
                strSql = strSql.Replace("[FindCondition]", " and Trim(LOWER(code_vchr)) Like '" + strCode.ToLower().Trim() + "%' ");
            }
            else if (((strCode[0] >= 'a') && (strCode[0] <= 'z')) || ((strCode[0] >= 'A') && (strCode[0] <= 'Z')))
            {
                strSql = strSql.Replace("[FindCondition]", " and Trim(LOWER(PYCode_Chr)) Like '" + strCode.ToLower().Trim() + "%' ");
            }
            else
            {
                strSql = strSql.Replace("[FindCondition]", " and LOWER(DeptName_VChr) Like '" + strCode.ToLower().Trim() + "%' ");
            }

            DataTable objDT = new DataTable();
            long ret = 0;
            try
            {
                ret = 0;
                ret = new clsHRPTableService().DoGetDataTable(strSql, ref objDT);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if ((ret > 0) && (objDT != null))
            {
                arrArea = new clsBIHArea[objDT.Rows.Count];
                for (int i = 0; i < arrArea.Length; i++)
                {
                    arrArea[i] = new clsBIHArea();
                    arrArea[i].m_strAreaID = Convert.ToString(objDT.Rows[i]["DeptID_Chr"]).Trim();
                    arrArea[i].m_strAreaName = Convert.ToString(objDT.Rows[i]["DeptName_VChr"]).Trim();
                    arrArea[i].code = Convert.ToString(objDT.Rows[i]["code_vchr"].ToString().Trim());

                }
                return 1;
            }
            else
            {
                arrArea = null;
                return 0;
            }
        }
        #endregion

        private string m_strSQL;

        #region ��ȡ���(�ѱ�)��Ϣ
        /// <summary>
        /// ��ȡ���(�ѱ�)��Ϣ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPayTypeInfo(out DataTable dt)
        {
            long lngRes = 0;
            string SQL = @"select paytypeid_chr, paytypename_vchr, memo_vchr, paylimit_mny, payflag_dec,
                                  paypercent_dec, paytypeno_vchr, isusing_num, copayid_chr,
                                  chargepercent_dec, internalflag_int, coalitionrecipeflag_int,
                                  bihlimitrate_dec
                             from t_bse_patientpaytype order by paytypeid_chr";

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

        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <param name="Flag">1 ���� 2 ����</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeptArea(out DataTable dt, int Flag)
        {
            long lngRes = 0;
            string SQL = "";

            if (Flag == 1)
            {
                SQL = @"select deptid_chr, modify_dat, deptname_vchr, category_int,
                               inpatientoroutpatient_int, operatorid_chr, address_vchr, pycode_chr,
                               attributeid, parentid, createdate_dat, status_int, deactivate_dat,
                               wbcode_chr, code_vchr, extendid_vchr, shortno_chr, stdbed_count_int,
                               putmed_int
                          from t_bse_deptdesc 
                         where status_int = 1 
                           and ((inpatientoroutpatient_int = 1 or inpatientoroutpatient_int = 2) or parentid = 0)
                      order by code_vchr";
            }
            else if (Flag == 2)
            {
                SQL = @"select deptid_chr, modify_dat, deptname_vchr, category_int,
                               inpatientoroutpatient_int, operatorid_chr, address_vchr, pycode_chr,
                               attributeid, parentid, createdate_dat, status_int, deactivate_dat,
                               wbcode_chr, code_vchr, extendid_vchr, shortno_chr, stdbed_count_int,
                               putmed_int
                          from t_bse_deptdesc 
                         where status_int = 1 
                           and ((inpatientoroutpatient_int = 1 or inpatientoroutpatient_int = 2) or parentid = 0)
                           and attributeid = '0000003'
                      order by code_vchr";
            }

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

        #region ���ݲ���ID��ȡ�ò�����λ��Ϣ
        /// <summary>
        /// ���ݲ���ID��ȡ�ò�����λ��Ϣ
        /// </summary>
        /// <param name="AreaID">����ID</param>
        /// <param name="status">��ѯ���� 0 ȫ�� 1 �մ� 2 ռ��(ռ��������) 3 ԤԼռ�� 4 ����ռ�� 5 ɾ�� 6 Ԥ��Ժռ��</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetBedinfo(string AreaID, int status, out DataTable dt)
        {
            long lngRes = 0;
            string sqlstatus = "";

            switch (status)
            {
                case 0:
                    sqlstatus = " and a.status_int <> 5 ";
                    break;
                case 1:
                    sqlstatus = " and a.status_int = 1 ";
                    break;
                case 2:
                    sqlstatus = " and (a.status_int = 2 or a.status_int = 4) ";
                    break;
                case 3:
                    sqlstatus = " and a.status_int = 3 ";
                    break;
            }

            string SQL = @"select a.areaid_chr, b.inpatientid_chr, b.inpatientcount_int, a.bedid_chr, a.code_chr, c.lastname_vchr, c.sex_chr, b.pstatus_int, 
                                  c.birth_dat, to_char(b.inpatient_dat,'yyyy/mm/dd hh24:mi:ss') as rysj, b.registerid_chr, b.patientid_chr, d.patientcardid_chr
                             from t_bse_bed a, t_opr_bih_register b, t_opr_bih_registerdetail c, t_bse_patientcard d   
                            where a.bihregisterid_chr = b.registerid_chr(+)
                              and b.registerid_chr = c.registerid_chr(+)
                              and a.bedid_chr = b.bedid_chr(+)  
                              and b.patientid_chr = d.patientid_chr(+)                            
                              and a.areaid_chr like ? " + sqlstatus + @"                               
                            order by a.code_chr";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr); 
                ParamArr[0].Value = AreaID;

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

        #region ����סԺ�Ż����ƿ��Ż�ȡ������Ϣ
        /// <summary>
        /// ����סԺ�Ż����ƿ��Ż�ȡ������Ϣ
        /// </summary>
        /// <param name="no"></param>
        /// <param name="dt"></param>
        /// <param name="type">0 ��סԺ�Ż����ƿ��Ų����в�����Ϣ 1 ��סԺ��(����Ժʱ��.����)����Ժ������Ϣ 2 �����ƿ��Ų���Ժ������Ϣ 3 ��סԺ��(����Ժʱ��.����)���Ժ������Ϣ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientinfoByZyh(string no, out DataTable dt, int type)
        {
            long lngRes = 0;

            clsHRPTableService objHRPSvc = new clsHRPTableService();
            IDataParameter[] ParamArr = null;

            string SqlWhere = "";
            switch (type)
            {
                case 0:
                    SqlWhere = "and (b.inpatientid_chr = ? or d.patientcardid_chr = ?)";

                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = no;
                    ParamArr[1].Value = no;

                    break;
                case 1:
                    SqlWhere = @"and (b.pstatus_int = 0 or b.pstatus_int = 1 or b.pstatus_int = 4) 
                                 and b.inpatientid_chr = ? order by b.inpatient_dat desc";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = no;

                    break;
                case 2:
                    SqlWhere = @"and (b.pstatus_int = 0 or b.pstatus_int = 1 or b.pstatus_int = 4) 
                                 and d.patientcardid_chr = ?";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = no;

                    break;
                case 3:
                    SqlWhere = @"and (b.pstatus_int = 2 or b.pstatus_int = 3) 
                                 and b.inpatientid_chr = ? order by t1.outhospital_dat desc";

                    objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                    ParamArr[0].Value = no;

                    break;
                default:
                    SqlWhere = "and (b.inpatientid_chr = ? or d.patientcardid_chr = ?)";

                    objHRPSvc.CreateDatabaseParameter(2, out ParamArr);
                    ParamArr[0].Value = no;
                    ParamArr[1].Value = no;

                    break;
            }

            string SQL = @"select b.areaid_chr, e.deptname_vchr, b.inpatientid_chr, b.inpatientcount_int, a.bedid_chr, a.code_chr, b.patientid_chr, 
                                  c.lastname_vchr, c.sex_chr, c.birth_dat, to_char(b.inpatient_dat,'yyyy/mm/dd hh24:mi:ss') as rysj,
                                  c.idcard_chr, c.homeaddress_vchr, b.inpatientnotype_int, b.registerid_chr, to_char(t1.outhospital_dat, 'yyyy/mm/dd hh24:mi:ss') as cysj,
                                  t2.deptname_vchr as cybq, t3.code_chr as cybc, b.pstatus_int, b.paytypeid_chr    
                             from t_bse_bed a, 
                                  (select tb1.registerid_chr, tb1.modify_dat, tb1.patientid_chr,
                                          tb1.isbooking_int, tb1.inpatientid_chr, tb1.inpatient_dat,
                                          tb1.deptid_chr, tb1.areaid_chr, tb1.bedid_chr, tb1.type_int,
                                          tb1.diagnose_vchr, tb1.limitrate_mny, tb1.inpatientcount_int,
                                          tb1.state_int, tb1.status_int, tb1.operatorid_chr, tb1.pstatus_int,
                                          tb1.casedoctor_chr, tb1.inpatientnotype_int, tb1.des_vchr,
                                          tb1.inareadate_dat, tb1.mzdoctor_chr, tb1.mzdiagnose_vchr,
                                          tb1.diagnoseid_chr, tb1.icd10diagid_vchr, tb1.icd10diagtext_vchr,
                                          tb1.isfromclinic, tb1.clinicsayprepay, tb1.paytypeid_chr,
                                          tb1.bornnum_int, tb1.relateregisterid_chr, tb1.feestatus_int,
                                          tb1.extendid_vchr, tb1.nursing_class, tb1.casedoctordept_chr,
                                          tb1.cancelerid_chr, tb1.cancel_dat, tb1.outdiagnose_vchr,
                                          tb1.insuredsum_mny
                                     from t_opr_bih_register tb1
                                    where exists (select 1
                                                    from (select max(tb2.registerid_chr) as registerid_chr
                                                            from t_opr_bih_register tb2
                                                           group by tb2.inpatientid_chr) tb2
                                                   where tb1.registerid_chr = tb2.registerid_chr)
                                  ) b, 
                                  t_opr_bih_registerdetail c, t_bse_patientcard d, t_bse_deptdesc e,
                                  (select registerid_chr, outareaid_chr, outbedid_chr, outhospital_dat from t_opr_bih_leave where status_int = 1) t1, t_bse_deptdesc t2, t_bse_bed t3   
                            where b.registerid_chr = a.bihregisterid_chr(+)
                              and b.registerid_chr = c.registerid_chr  
                              and b.bedid_chr = a.bedid_chr(+)                              
                              and b.patientid_chr = d.patientid_chr(+)
                              and b.areaid_chr = e.deptid_chr(+)
                              and b.status_int = 1                               
                              and b.registerid_chr = t1.registerid_chr(+) 
                              and t1.outareaid_chr = t2.deptid_chr(+)
                              and t1.outbedid_chr = t3.bedid_chr(+) ";

            SQL += SqlWhere;

            dt = new DataTable();

            try
            {
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

        #region ͨ�ò��Ҵ�����
        /// <summary>
        /// ͨ�ò��Ҵ�����
        /// </summary>
        /// <param name="SqlWhereZY"></param>
        /// <param name="Status">0 ȫ�� 1 ��Ժ 2 Ԥ��Ժ 3 ��ʽ��Ժ 4 ��� 8 ��Ժ���� 9 Ԥ��������</param>
        /// <param name="IsIncludeMZ">true �������� false ����������</param>
        /// <param name="SqlWhereMZ"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientinfo(string SqlWhereZY, int Status, bool IsIncludeMZ, string SqlWhereMZ, clsCommonQueryDate_VO CommonQueryDate_VO, out DataTable dt)
        {
            long lngRes = 0;
            string subSql = " ";

            if (Status == 1)
            {
                subSql = @" and (a.pstatus_int = 0 or a.pstatus_int = 1 or a.pstatus_int = 4) ";
            }
            else if (Status == 2)
            {
                subSql = @" and a.pstatus_int = 2";
            }
            else if (Status == 3)
            {
                subSql = @" and a.pstatus_int = 3";
            }
            else if (Status == 8)
            {
                subSql = @" and (a.pstatus_int = 2 or a.pstatus_int = 3)";
            }
            else if (Status == 9)
            {
                subSql = @" and (a.pstatus_int = 0 or a.pstatus_int = 1 or a.pstatus_int = 2 or a.pstatus_int = 4) ";
            }

            #region �����������Ͳ�ѯ
            string InDateSql = "";
            string OutDateSql = "";
            string OutSign = "(+)";
            if (CommonQueryDate_VO != null)
            {
                if (CommonQueryDate_VO.QueryType == 1 || CommonQueryDate_VO.QueryType == 3)
                {
                    InDateSql = " and (a.inpatient_dat between to_date('" + CommonQueryDate_VO.BeginDate_In + "', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + CommonQueryDate_VO.EndDate_In + "', 'yyyy-mm-dd hh24:mi:ss')) ";
                }

                if (CommonQueryDate_VO.QueryType == 2 || CommonQueryDate_VO.QueryType == 3)
                {
                    OutDateSql = " and (outhospital_dat between to_date('" + CommonQueryDate_VO.BeginDate_Out + "', 'yyyy-mm-dd hh24:mi:ss') and to_date('" + CommonQueryDate_VO.EndDate_Out + "', 'yyyy-mm-dd hh24:mi:ss')) ";
                    OutSign = "";
                }
            }
            #endregion

            string SQL = @" select a.registerid_chr, a.patientid_chr, a.inpatientid_chr, a.pstatus_int, a.inpatientcount_int, a.feestatus_int, 
                                   b.lastname_vchr, b.sex_chr, b.birth_dat, to_char(b.birth_dat, 'yyyy/mm/dd') as cssj,
                                   b.idcard_chr, to_char(a.inpatient_dat, 'yyyy/mm/dd hh24:mi:ss') as rysj, e.deptname_vchr, d.code_chr,
                                   to_char(c.outhospital_dat, 'yyyy/mm/dd hh24:mi:ss') as cysj, a.inpatientnotype_int, b.homeaddress_vchr, b.employer_vchr, f.patientcardid_chr  
                              from t_opr_bih_register a,
                                   t_opr_bih_registerdetail b,
                                   (select registerid_chr, 
                                           outhospital_dat 
                                      from t_opr_bih_leave 
                                     where status_int = 1 " + OutDateSql + @") c,
                                   t_bse_bed d,
                                   t_bse_deptdesc e,
                                   t_bse_patientcard f
                             where a.registerid_chr = b.registerid_chr
                               and a.patientid_chr = f.patientid_chr(+)
                               and a.status_int = 1                                   
                               and a.registerid_chr = c.registerid_chr " + OutSign + @" 
                               and a.registerid_chr = d.bihregisterid_chr(+)
                               and a.bedid_chr = d.bedid_chr(+)
                               and d.areaid_chr = e.deptid_chr(+) " + SqlWhereZY + subSql + InDateSql + " order by a.patientid_chr, a.inpatient_dat";

            if (IsIncludeMZ)
            {
                SQL = "select * from (" + SQL + @") ta 
                       union all
                       select * from ( 
                                       select '' as registerid_chr, b.patientid_chr, '' as inpatientid_chr, 999 as pstatus_int, 0 as inpatientcount_int, 0 as feestatus_int,
                                              b.lastname_vchr, b.sex_chr, b.birth_dat, to_char(b.birth_dat, 'yyyy/mm/dd') as cssj,
                                              b.idcard_chr, '' as rysj, '' as deptname_vchr, '' as code_chr,
                                              '' as cysj, 0 as inpatientnotype_int, b.homeaddress_vchr, b.employer_vchr, f.patientcardid_chr  
                                         from t_bse_patient b,
                                              t_bse_patientcard f
                                        where b.patientid_chr = f.patientid_chr 
                                          and not exists (
                                                            select 1
                                                              from t_opr_bih_register tr     
                                                             where b.patientid_chr = tr.patientid_chr           
                                                         ) 
                                          and " + SqlWhereMZ + " order by b.patientid_chr) tb "; ;
            }

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

        #region ����סԺ�Ż�ȡ���˻�������
        /// <summary>
        /// ����סԺ�Ż�ȡ���˻�������
        /// </summary>
        /// <param name="Zyh"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetPatientinfoByZyh(string Zyh, out DataTable dt)
        {
            long lngRes = 0;
            string SQL = @" select a.registerid_chr, a.inpatientid_chr, 
                                   a.inpatientcount_int, b.lastname_vchr
                              from t_opr_bih_register a,
                                   t_opr_bih_registerdetail b
                             where a.registerid_chr = b.registerid_chr
                               and a.status_int = 1
                               and a.inpatientid_chr = ? 
                          order by a.inpatientcount_int ";

            dt = new DataTable();

            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] ParamArr = null;
                objHRPSvc.CreateDatabaseParameter(1, out ParamArr);
                ParamArr[0].Value = Zyh;
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

        #region ��ѯ�շ���Ŀ
        /// <summary>
        /// ��ѯ�շ���Ŀ
        /// </summary>
        /// <param name="FindStr">��������</param>
        /// <param name="PatType">�������</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindChargeItem(string FindStr, string PatType, out DataTable dt)
        {
            long lngRes = 0;

            string SQL = @"select a.itemid_chr, a.itemname_vchr, a.itemcode_vchr, a.itempycode_chr,
                                  a.itemwbcode_chr, a.itemsrcid_vchr, a.itemsrctype_int, a.itemspec_vchr,
                                  a.itemprice_mny, a.itemunit_chr, a.itemopunit_chr, a.itemipunit_chr,
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
								  round (a.itemprice_mny / a.packqty_dec,4) as submoney, b.putmedtype_int 
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
        public long m_lngFindChargeItem(string ItemID, out DataTable dt)
        {
            long lngRes = 0;

            string SQL = @"select a.itemid_chr, a.itemname_vchr, a.itemcode_vchr, a.itempycode_chr,
                                  a.itemwbcode_chr, a.itemsrcid_vchr, a.itemsrctype_int, a.itemspec_vchr,
                                  a.itemprice_mny, a.itemunit_chr, a.itemopunit_chr, a.itemipunit_chr,
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

        #region ������Ժ��ͳ�Ʊ�  liuyingrui 2006.05.08
        /// <summary>
        /// ������Ժ��ͳ�Ʊ�  liuyingrui 2006.05.08
        /// </summary>
        /// <param name="dtStartTime">ͳ����ʼʱ��</param>
        /// <param name="dtEndTime">ͳ����ֹʱ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long GetPatientBihStatistics(DateTime dtStartime, DateTime dtEndTime, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strStartTime = dtStartime.ToString("yyyy-MM-dd HH:mm:ss");
            string strEndTime = dtEndTime.ToString("yyyy-MM-dd HH:mm:ss");
             
            string strSQL = @"SELECT   max(b.empno_chr) AS ҽ������, max (b.lastname_vchr)as ҽ������,max(c.deptname_vchr) as ���� , 
                            COUNT (*) AS  �˴� 
                            FROM t_opr_bih_register a, t_bse_employee b,t_bse_deptdesc c
                            WHERE a.MZDOCTOR_CHR = b.empid_chr(+) and a.casedoctordept_chr=c.deptid_chr(+)
                             and a.status_int=1
                             AND a.inpatient_dat BETWEEN TO_DATE ('" + strStartTime + @"',
                                          'YYYY-MM-DD HH24:MI:SS'
                                         )
                             AND TO_DATE ('" + strEndTime + @"',
                                          'YYYY-MM-DD HH24:MI:SS'
                                         )
                             GROUP BY a.MZDOCTOR_CHR,a.casedoctordept_chr";
            if (strSQL != "")
            {
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objService = new clsHRPTableService();
                    lngRes = 0;
                    lngRes = objService.DoGetDataTable(strSQL, ref dtbResult);
                    objService.Dispose();
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                return lngRes;

            }
            return 0;
        }
        #endregion

        #region ���˳�Ժ��ͳ�Ʊ� 2006.11.18
        /// <summary>
        /// ���˳�Ժ��ͳ�Ʊ�  2006.11.18
        /// </summary>
        /// <param name="dtStartTime">ͳ����ʼʱ��</param>
        /// <param name="dtEndTime">ͳ����ֹʱ��</param>
        /// <returns></returns>
        [AutoComplete]
        public long GetPatientLeftStatistics(DateTime dtStartime, DateTime dtEndTime, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strStartTime = dtStartime.ToString("yyyy-MM-dd HH:mm:ss");
            string strEndTime = dtEndTime.ToString("yyyy-MM-dd HH:mm:ss");
             
            string strSQL = @"SELECT   max(b.empno_chr) AS ҽ������, max (b.lastname_vchr)as ҽ������,max(c.deptname_vchr) as ���� , 
                            COUNT (*) AS  �˴� 
                            FROM t_opr_bih_register a, 
                                t_bse_employee b,
                                t_bse_deptdesc c,
                                t_opr_bih_transfer d
                            WHERE a.CASEDOCTOR_CHR = b.empid_chr(+) and 
                                  a.AREAID_CHR = c.deptid_chr  and
                                  a.REGISTERID_CHR = d.REGISTERID_CHR and
                                  a.status_int=1 and
                                  a.PSTATUS_INT = 3 and
                                  d.TYPE_INT = 6 
                             AND d.MODIFY_DAT BETWEEN TO_DATE ('" + strStartTime + @"',
                                          'YYYY-MM-DD HH24:MI:SS'
                                         )
                             AND TO_DATE ('" + strEndTime + @"',
                                          'YYYY-MM-DD HH24:MI:SS'
                                         )
                             GROUP BY a.casedoctor_chr,a.deptid_chr";
            if (strSQL != "")
            {
                try
                {
                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objService = new clsHRPTableService();
                    lngRes = 0;
                    lngRes = objService.DoGetDataTable(strSQL, ref dtbResult);
                    objService.Dispose();
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                return lngRes;

            }
            return 0;
        }
        #endregion

        //add 2007-4-17
        #region ������Ժ��ͳ�Ʊ��շ�����ѯ  zhu 2007.4.17
        /// <summary>
        /// ������Ժ��ͳ�Ʊ��շ�����ѯ  zhu 2007.4.17
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtStartime">ͳ����ʼʱ��</param>
        /// <param name="dtEndTime"></param>
        /// <param name="strPaytypeId"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetPatientBihStatistics(DateTime dtStartime, DateTime dtEndTime, string strPaytypeId, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strStarTime = dtStartime.ToShortDateString();
            string strEndTime = dtEndTime.ToShortDateString();
            DateTime dtStart = Convert.ToDateTime(strStarTime + " 00:00:00");
            DateTime dtEnd = Convert.ToDateTime(strEndTime + " 23:59:59");
             
            string strSQL = @"
                        select max(b.empno_chr) as ҽ������, max(b.lastname_vchr) as ҽ������,
                                max(c.deptname_vchr) as ����,count(*) as �˴� 
                            from t_opr_bih_register a, t_bse_employee b, t_bse_deptdesc c
                           where a.mzdoctor_chr = b.empid_chr(+)
                             and a.casedoctordept_chr = c.deptid_chr(+)
                             and a.status_int = 1
                             and a.paytypeid_chr = ?
                             and a.inpatient_dat between ? and ? 
                         group by a.mzdoctor_chr,  a.casedoctordept_chr
                           order by ҽ������
                    ";
            if (strSQL != "")
            {
                try
                {

                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objService = new clsHRPTableService();
                    System.Data.IDataParameter[] objLisAddItemRefArr;
                    objService.CreateDatabaseParameter(3, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].Value = strPaytypeId;
                    objLisAddItemRefArr[1].Value = dtStart;
                    objLisAddItemRefArr[2].Value = dtEnd;

                    lngRes = objService.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);

                    objService.Dispose();

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                return lngRes;

            }
            return 0;
        }
        #endregion

        //add 2007-4-17
        #region ���˳�Ժ��ͳ�Ʊ��շ�����ѯ  zhu 2007.4.17
        /// <summary>
        /// ���˳�Ժ��ͳ�Ʊ��շ�����ѯ  zhu 2007.4.17
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtStartime">ͳ����ʼʱ��</param>
        /// <param name="dtEndTime"></param>
        /// <param name="strPaytypeId"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long GetPatientLeftStatistics(DateTime dtStartime, DateTime dtEndTime, string strPaytypeId, out DataTable dtbResult)
        {
            dtbResult = new DataTable();
            long lngRes = 0;
            string strStarTime = dtStartime.ToShortDateString();
            string strEndTime = dtEndTime.ToShortDateString();
            DateTime dtStart = Convert.ToDateTime(strStarTime + " 00:00:00");
            DateTime dtEnd = Convert.ToDateTime(strEndTime + " 23:59:59");
             
            string strSQL = @"
                        select   max (b.empno_chr) as ҽ������, max (b.lastname_vchr) as ҽ������,
                                 max (c.deptname_vchr) as ����, count (*) as �˴�
                            from t_opr_bih_register a,
                                 t_bse_employee b,
                                 t_bse_deptdesc c,
                                 t_opr_bih_transfer d
                           where a.casedoctor_chr = b.empid_chr(+)
                             and a.areaid_chr = c.deptid_chr
                             and a.registerid_chr = d.registerid_chr
                             and a.status_int = 1
                             and a.pstatus_int = 3
                             and a.paytypeid_chr = ?
                             and d.type_int = 6
                             and d.modify_dat between ? and ?
                        group by a.casedoctor_chr, a.deptid_chr
                    ";

            if (strSQL != "")
            {
                try
                {

                    com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objService = new clsHRPTableService();
                    System.Data.IDataParameter[] objLisAddItemRefArr;
                    objService.CreateDatabaseParameter(3, out objLisAddItemRefArr);
                    objLisAddItemRefArr[0].Value = strPaytypeId;
                    objLisAddItemRefArr[1].Value = dtStart;
                    objLisAddItemRefArr[2].Value = dtEnd;

                    lngRes = objService.lngGetDataTableWithParameters(strSQL, ref dtbResult, objLisAddItemRefArr);

                    objService.Dispose();

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                return lngRes;

            }
            return 0;
        }
        #endregion

        #region ̨ɽ�л���ҽ�Ʊ���סԺ�Է���Ŀǩ�ֵ�
        /// <summary>
        /// ̨ɽ�л���ҽ�Ʊ���סԺ�Է���Ŀǩ�ֵ�
        /// </summary>
        /// <param name="strInpinsurancetype"></param>
        /// <param name="RegisterID"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetOwnCastItem(string strInpinsurancetype, string RegisterID,out DataTable dtResult)
        {
            long lngRes = -1;
            dtResult = null;

            try
            {
                string strSQL = @"select   b.registerid_chr, c.lastname_vchr, c.idcard_chr, c.insuranceid_vchr,
                                             c.sex_chr, to_char (sysdate, 'YYYY') - to_char (c.birth_dat, 'YYYY'),
                                             b.outdiagnose_vchr, b.inpatientid_chr, d.deptname_vchr,
                                             a.chargeitemname_chr, a.unitprice_dec, a.unit_vchr,         sum (a.amount_dec) as amount_sum,
         sum (a.unitprice_dec * a.amount_dec) as allmoney_sum,
                                             decode (a.invcateid_chr,
                                                     '0182', 0,
                                                     decode (a.invcateid_chr,
                                                             '0184', 0,
                                                             decode (a.invcateid_chr, '0186', 0, 1)
                                                            )
                                                    ) title0
                                        from t_opr_bih_patientcharge a,
                                             t_opr_bih_register b,
                                             t_bse_patient c,
                                             t_bse_deptdesc d,
                                             t_bse_chargeitem e
                                       where (a.registerid_chr = b.registerid_chr)
                                         and (b.patientid_chr = c.patientid_chr)
                                         and (b.areaid_chr = d.deptid_chr)
                                         and (a.chargeitemid_chr = e.itemid_chr)
                                         and (e.inpinsurancetype_vchr in ([inpinsurancetype]))
                                         and (b.registerid_chr = ? )
                                         and (a.status_int = 1)
                                    group by c.lastname_vchr,
                                             c.idcard_chr,
                                             c.insuranceid_vchr,
                                             c.birth_dat,
                                             c.sex_chr,
                                             b.outdiagnose_vchr,
                                             b.inpatientid_chr,
                                             d.deptname_vchr,
                                             a.chargeitemname_chr,
                                             a.unitprice_dec,
                                             a.unit_vchr,
                                             a.invcateid_chr,
                                             b.registerid_chr ";
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                strSQL = strSQL.Replace("[inpinsurancetype]", strInpinsurancetype);

                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(1, out param);
                param[0].Value = RegisterID;

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
        /// ͨ��ID�����ˮ��
        /// </summary>
        /// <param name="inPatientID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRegisterID(string inPatientID, out DataTable dt, int p_intType)
        {
            string strSQL = "";
            if (p_intType == 0)
            {
                strSQL = @"select a.registerid_chr, a.inpatient_dat,
                                         case
                                            when a.pstatus_int = 2
                                               then 'Ԥ��Ժ'
                                            when a.pstatus_int = 3
                                               then '�ѳ�Ժ'
                                         end as status
                                    from t_opr_bih_register a
                                   where a.inpatientid_chr = ?
                                order by a.inpatient_dat desc ";
            }
            else if (p_intType == 1)
            {
                strSQL = @"select    a.registerid_chr, a.inpatient_dat,
                                     case
                                        when a.pstatus_int = 2
                                           then 'Ԥ��Ժ'
                                        when a.pstatus_int = 3
                                           then '�ѳ�Ժ'
                                     end as status
                                from t_opr_bih_register a, t_bse_patient b, t_bse_patientcard c
                               where c.status_int = 1
                                 and c.patientid_chr = b.patientid_chr
                                 and b.patientid_chr = a.patientid_chr
                                 and c.patientcardid_chr = ?
                            order by a.inpatient_dat desc ";
            }
            else
            {
                strSQL = @"select a.registerid_chr, a.inpatient_dat,
                                         case
                                            when a.pstatus_int = 2
                                               then 'Ԥ��Ժ'
                                            when a.pstatus_int = 3
                                               then '�ѳ�Ժ'
                                         end as status
                                    from t_opr_bih_register a
                                   where a.inpatientid_chr = ?
                                order by a.inpatient_dat desc ";
            }

            long lngRes = -1;
            dt = null;
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(1, out param);
                param[0].Value = inPatientID;

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

        #endregion 

        #region ��ݸ�в�ɽҽԺ��������ͳ�Ʊ�
        /// <summary>
        /// ��ݸ�в�ɽҽԺ��������ͳ�Ʊ�
        /// </summary>
        /// <param name="dtmTmp"></param>
        /// <param name="DeptID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRptNursingLog(DateTime dtmTmp, string DeptID, out DataTable dt)
        {
            dt = null;
            long lngRes = -1;
            //DateTime dtmBegin = DateTime.Parse(dtmTmp.ToString("yyyy-MM") + "-01 00:00:00");
            //DateTime dtmEnd = DateTime.Parse(dtmTmp.AddMonths(1).ToString("yyyy-MM") + "-01 00:00:00");
            try
            {
                string strSQL = @"select td.execount, td.chargedate, td.seqid_chr, te.nurname_vchr,
       te.nurscore_int, te.nurunit_vchr, te.disporder_int
  from (select   count (a.pchargeid_chr) as execount,
                 trunc (a.chargeactive_dat) as chargedate, c.seqid_chr
            from t_opr_bih_patientcharge a,
                 t_opr_bih_order b,
                 t_bse_rptnursing c,
                 t_bse_bih_orderdic d
           where a.orderid_chr = b.orderid_chr
             and b.orderdicid_chr = c.orderdicid_chr
             and c.orderdicid_chr = d.orderdicid_chr
             and a.chargeitemid_chr = d.itemid_chr
             and c.status_int = 1
             and (a.chargeactive_dat between to_date (?,
                                                      'yyyy-mm-dd hh24:mi:ss'
                                                     )
                                         and to_date (?,
                                                      'yyyy-mm-dd hh24:mi:ss'
                                                     )
                 )
             and a.curareaid_chr = ?
        group by trunc (a.chargeactive_dat), c.seqid_chr) td,
       t_bse_rptnursing te
 where td.seqid_chr = te.seqid_chr";
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param = null;
                objHRPSvc.CreateDatabaseParameter(3, out param);
                ///�������⣬�������ԣ����������ֱ�Ӵ���DateTime�������ݣ��ڻ�ȡDataTableʱЧ�ʷ����ܵͣ����ֻع����д�����ϸ�Ĳ���
                //param[0].DbType = DbType.DateTime;
                //param[1].DbType = DbType.DateTime;
                //param[0].Value = dtmBegin;
                //param[1].Value = dtmEnd;
                //param[2].Value = DeptID;

                param[0].Value = dtmTmp.ToString("yyyy-MM") + "-01 00:00:00";
                param[1].Value = dtmTmp.AddMonths(1).ToString("yyyy-MM") + "-01 00:00:00";
                param[2].Value = DeptID;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dt, param); 
                dt.DefaultView.Sort = "disporder_int";
                objHRPSvc.Dispose();
                objHRPSvc = null;
                strSQL = string.Empty;  

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
        ///  ��ݸ�в�ɽҽԺ��������ͳ�Ʊ� -- ����Ժ
        /// </summary>
        /// <param name="dtmTmp"></param>
        /// <param name="DeptID"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetRptNusingPatientCount(DateTime dtmTmp, string DeptID, out DataTable dt)
        {
            DateTime dtmBegin = DateTime.Parse(dtmTmp.ToString("yyyy-MM") + "-01 00:00:00");
            DateTime dtmEnd = DateTime.Parse(dtmTmp.AddMonths(1).ToString("yyyy-MM") + "-01 00:00:00");
            long lngRes = -1;
            dt = null;
            DataTable dtTmp = null;
            string strSQL = @"select   count (ta.registerid_chr) as patientcount, tb.inareaddate, 1 as flag
                                    from t_opr_bih_register ta,
                                         (select a.registerid_chr, trunc (a.inareadate_dat) as inareaddate
                                            from t_opr_bih_register a
                                           where a.status_int = 1
                                             and a.areaid_chr = ?
                                             and a.pstatus_int <> -1
                                             and (a.inareadate_dat between ? and ?)) tb
                                   where ta.registerid_chr = tb.registerid_chr
                                group by inareaddate";
            try
            {
                clsHRPTableService objHRPSvc = new clsHRPTableService();
                IDataParameter[] param=null;
                objHRPSvc.CreateDatabaseParameter(3,out param);
                param[0].Value = DeptID;
                param[1].DbType=DbType.DateTime;
                param[1].Value = dtmBegin;
                param[2].DbType = DbType.DateTime;
                param[2].Value = dtmEnd;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtTmp, param);
                dt = dtTmp.Clone();
                if (dt != null && dtTmp != null && dtTmp.Rows.Count > 0)
                    dt.Merge(dtTmp);
                dt.AcceptChanges();

                strSQL = @"select   count (ta.registerid_chr) as patientcount, tb.inareaddate, 0 as flag
                                from t_opr_bih_leave ta,
                                     (select a.leaveid_chr, trunc (a.outhospital_dat) as inareaddate
                                        from t_opr_bih_leave a
                                       where a.status_int = 1
                                         and a.pstatus_int = 1
                                         and a.outareaid_chr = ?
                                         and (a.outhospital_dat between ? and ?)) tb
                               where ta.leaveid_chr = tb.leaveid_chr
                            group by inareaddate ";
                objHRPSvc.CreateDatabaseParameter(3, out param);
                param[0].Value = DeptID;
                param[1].DbType = DbType.DateTime;
                param[1].Value = dtmBegin;
                param[2].DbType = DbType.DateTime;
                param[2].Value = dtmEnd;

                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref dtTmp, param);
                if (dt != null && dtTmp != null && dtTmp.Rows.Count > 0)
                    dt.Merge(dtTmp);
                dt.AcceptChanges();
                dt.DefaultView.Sort = "flag,inareaddate";
                objHRPSvc.Dispose();
                objHRPSvc = null;
                strSQL = "";
                dtTmp.Dispose();
                dtTmp = null; 

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

        #region סԺЭ�鵥λ��ѯͳ�Ʊ���
       /// <summary>
        /// סԺЭ�鵥λ��ѯͳ�Ʊ���
       /// </summary>
       /// <param name="p_strStartDate"></param>
       /// <param name="p_strEndDate"></param>
       /// <param name="p_dtbResult"></param>
       /// <returns></returns>
        [AutoComplete]
        public long m_lngContractUnitPayType(string p_strStartDate,string p_strEndDate,out DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = new DataTable();
            string strSQL = @"select t1.registerid_chr,
       t2.lastname_vchr,
       t1.inpatientid_chr,
       t3.paytypename_vchr,
       t2.employer_vchr,
       t4.deptname_vchr,
        sum(t5.totalmoney_dec) as total,
       t1.inpatient_dat,
       
       t1.pstatus_int
       

      
  from t_opr_bih_patientcharge  t5,
       t_opr_bih_register       t1,
       t_opr_bih_registerdetail t2,
       t_bse_patientpaytype     t3,
       t_bse_deptdesc           t4
 where t1.registerid_chr = t5.registerid_chr(+)
   and t1.paytypeid_chr = t3.paytypeid_chr(+)
   and t1.areaid_chr = t4.deptid_chr(+)
   and t1.registerid_chr = t2.registerid_chr(+)
   and t5.status_int = 1
   and t5.chargeactive_dat is not null
   and t1.paytypeid_chr in ('0016,', '0009')
   and t1.inpatient_dat between
       to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
       to_date(?, 'yyyy-mm-dd hh24:mi:ss')
 group by t1.registerid_chr,
          t1.inpatientid_chr,
          t1.inpatient_dat,
          t1.pstatus_int,
          t2.lastname_vchr,
          t2.employer_vchr,
          t3.paytypename_vchr,
          t4.deptname_vchr
";
            clsHRPTableService objHRPSvc = null;
            IDataParameter[] parmArr = null;
            try
            {

                objHRPSvc = new clsHRPTableService();
                objHRPSvc.CreateDatabaseParameter(2, out parmArr);
              
                parmArr[0].Value = p_strStartDate;
              
                parmArr[1].Value = p_strEndDate;
                lngRes = objHRPSvc.lngGetDataTableWithParameters(strSQL, ref p_dtbResult, parmArr);
                int intRowsCount = p_dtbResult.Rows.Count;
                if (lngRes > 0 && intRowsCount>0)
                {
                    p_dtbResult.Columns.Add("hospitalstatus", typeof(string));
                    DataRow dr=null;
                    for(int i=0;i<intRowsCount;i++)
                    {
                        dr=p_dtbResult.Rows[i];
                        switch (dr["pstatus_int"].ToString())
                      {
                            case"0":
                                dr["hospitalstatus"]="�´�";
                                break;
                            case"1":
                                dr["hospitalstatus"] = "�ڴ�";
                                break;
                            case"2":
                                dr["hospitalstatus"] = "Ԥ��Ժ";
                                break;
                            case"3":
                                dr["hospitalstatus"] = "ʵ�ʳ�Ժ";
                                break;
                            case"4":
                                dr["hospitalstatus"] = "���";
                                break;

                      }

                    }
                    p_dtbResult.Columns.Remove("pstatus_int");
                    p_dtbResult.Columns.Remove("registerid_chr");
                }
            }
            catch(Exception objEx)
            {
                 string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                if(objHRPSvc!=null)
                {
                    objHRPSvc.Dispose();
                    objHRPSvc = null;
                }
                strSQL = null;
            }
            return lngRes;
        }
        #endregion

    
    } 
}
