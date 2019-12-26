using System;
using System.Data;
using System.Collections.Generic;
using System.EnterpriseServices;
using com.digitalwave.Utility;
using com.digitalwave.iCare.middletier.HRPService;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.middletier.LIS
{
    /// <summary>
    /// 报表
    /// </summary>
    [Transaction(TransactionOption.Supported)]
    [ObjectPooling(Enabled = true)]
    public class bizLisReport : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        #region m_lngGetLISCheck_Category
        [AutoComplete]
        public long m_lngGetLISCheck_Category(out DataTable p_dtCategory)
        {
            p_dtCategory = null;
            clsHRPTableService svc = new clsHRPTableService();

            string Sql = @"select check_category_id_chr, check_category_desc_vchr from t_bse_lis_check_category";
            long rec = svc.lngGetDataTableWithoutParameters(Sql, ref p_dtCategory);
            return rec;
        }
        #endregion

        #region m_lngQueryWorkLoadByAppDept
        [AutoComplete]
        public long m_lngQueryWorkLoadByAppDept(string p_strCatoryID, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult)
        {
            p_dtResult = null;
            long rec = 0;
            string Sql = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;

            if (string.IsNullOrEmpty(p_strCatoryID))
            {
                Sql = @"select nvl(t1.appl_deptid_chr, '+') appl_deptid_chr,
                               t3.deptname_vchr,
                               t1.patient_type_id_chr,
                               count(t2.apply_unit_id_chr) reportcount,
                               sum(t2.itemcount) itemcount,
                               sum(t2.totalmoney) totalmoney
                          from t_opr_lis_application t1
                         inner join (select a.application_id_chr,
                                            a.confirmer_id_chr,
                                            b.apply_unit_id_chr,
                                            count(c.check_item_id_chr) itemcount,
                                            sum(c.itemprice_mny) totalmoney
                                       from t_opr_lis_app_report a
                                      inner join t_opr_lis_app_check_item c
                                         on a.application_id_chr = c.application_id_chr
                                        and c.report_group_id_chr = a.report_group_id_chr
                                      inner join t_opr_lis_app_sample d
                                         on a.application_id_chr = d.application_id_chr
                                        and d.sample_group_id_chr = c.sample_group_id_chr
                                        and d.report_group_id_chr = c.report_group_id_chr
                                      inner join t_aid_lis_sample_group_unit b
                                         on b.sample_group_id_chr = d.sample_group_id_chr
                                      inner join t_opr_lis_check_result e
                                         on e.sample_id_chr = d.sample_id_chr
                                        and d.sample_group_id_chr = e.groupid_chr
                                        and c.check_item_id_chr = e.check_item_id_chr
                                        and e.result_vchr <> '\\'
                                        and e.status_int = 1
                                      where a.status_int = 2
                                        and (a.confirm_dat between
                                            to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                            to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                      group by a.application_id_chr,
                                               a.confirmer_id_chr,
                                               b.apply_unit_id_chr) t2
                            on t2.application_id_chr = t1.application_id_chr
                          left outer join t_bse_deptdesc t3
                            on t3.deptid_chr = t1.appl_deptid_chr
                         where t1.pstatus_int = 2
                         group by t1.appl_deptid_chr, t3.deptname_vchr, t1.patient_type_id_chr
                        ";

                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm[1].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");

            }
            else
            {
                Sql = @"select nvl(t1.appl_deptid_chr, '+') appl_deptid_chr,
                               t3.deptname_vchr,
                               t1.patient_type_id_chr,
                               count(t2.apply_unit_id_chr) reportcount,
                               sum(t2.itemcount) itemcount,
                               sum(t2.totalmoney) totalmoney
                          from t_opr_lis_application t1
                         inner join (select a.application_id_chr,
                                            a.confirmer_id_chr,
                                            b.apply_unit_id_chr,
                                            count(c.check_item_id_chr) itemcount,
                                            sum(c.itemprice_mny) totalmoney
                                       from t_opr_lis_app_report a
                                      inner join t_opr_lis_app_check_item c
                                         on a.application_id_chr = c.application_id_chr
                                        and c.report_group_id_chr = a.report_group_id_chr
                                      inner join t_opr_lis_app_sample d
                                         on a.application_id_chr = d.application_id_chr
                                        and d.sample_group_id_chr = c.sample_group_id_chr
                                        and d.report_group_id_chr = c.report_group_id_chr
                                      inner join t_aid_lis_sample_group_unit b
                                         on b.sample_group_id_chr = d.sample_group_id_chr
                                      inner join t_aid_lis_apply_unit b1
                                         on b.apply_unit_id_chr = b1.apply_unit_id_chr
                                        and b1.check_category_id_chr = ?
                                      inner join t_opr_lis_check_result e
                                         on e.sample_id_chr = d.sample_id_chr
                                        and d.sample_group_id_chr = e.groupid_chr
                                        and c.check_item_id_chr = e.check_item_id_chr
                                        and e.result_vchr <> '\\'
                                        and e.status_int = 1
                                      where a.status_int = 2
                                        and (a.confirm_dat between
                                            to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                            to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                      group by a.application_id_chr,
                                               a.confirmer_id_chr,
                                               b.apply_unit_id_chr) t2
                            on t2.application_id_chr = t1.application_id_chr
                          left outer join t_bse_deptdesc t3
                            on t3.deptid_chr = t1.appl_deptid_chr
                         where t1.pstatus_int = 2
                         group by t1.appl_deptid_chr, t3.deptname_vchr, t1.patient_type_id_chr
                        ";

                svc.CreateDatabaseParameter(3, out parm);
                parm[0].Value = p_strCatoryID;
                parm[1].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm[2].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            }

            DataTable dt = null;
            rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.TableName = "resulttemp";
                p_dtResult = dt.DefaultView.ToTable("p_dtResult", true, new string[] { "deptname_vchr", "appl_deptid_chr" });
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables.Add(p_dtResult);

                DataRelation relation = new DataRelation("drdeptapprelation", p_dtResult.Columns["appl_deptid_chr"], dt.Columns["appl_deptid_chr"]);
                ds.Relations.Add(relation);

                string str2 = "1";
                string str3 = "2";
                string str4 = "3";
                DataColumn column = new DataColumn("dcishisreport", typeof(int));
                column.DefaultValue = (int)0;
                column.Expression = "iif(patient_type_id_chr = '" + str3 + "', reportcount, 0)";
                DataColumn column2 = new DataColumn("dcishisitemcount", typeof(int));
                column2.DefaultValue = (int)0;
                column2.Expression = "iif(patient_type_id_chr = '" + str3 + "', itemcount, 0)";
                DataColumn column3 = new DataColumn("dcisbihreport", typeof(int));
                column3.DefaultValue = (int)0;
                column3.Expression = "iif(patient_type_id_chr = '" + str2 + "', reportcount, 0)";
                DataColumn column4 = new DataColumn("dcisbihitemcount", typeof(int));
                column4.DefaultValue = (int)0;
                column4.Expression = "iif(patient_type_id_chr = '" + str2 + "', itemcount, 0)";
                DataColumn column5 = new DataColumn("dcispereport", typeof(int));
                column5.DefaultValue = (int)0;
                column5.Expression = "iif(patient_type_id_chr='" + str4 + "',reportcount,0)";
                DataColumn column6 = new DataColumn("dcispeitemcount", typeof(int));
                column6.DefaultValue = (int)0;
                column6.Expression = "iif(patient_type_id_chr='" + str4 + "',itemcount,0)";

                dt.Columns.AddRange(new DataColumn[] { column, column2, column3, column4, column5, column6 });

                DataColumn column7 = new DataColumn("reportcount", typeof(int));
                column7.DefaultValue = (int)0;
                column7.Expression = "sum(child(drdeptapprelation).reportcount)";
                DataColumn column8 = new DataColumn("hisreportcount", typeof(int));
                column8.DefaultValue = (int)0;
                column8.Expression = "sum(child(drdeptapprelation).dcishisreport)";
                DataColumn column9 = new DataColumn("bihreportcount", typeof(int));
                column9.DefaultValue = (int)0;
                column9.Expression = "sum(child(drdeptapprelation).dcisbihreport)";
                DataColumn column10 = new DataColumn("pereportcount", typeof(int));
                column10.DefaultValue = (int)0;
                column10.Expression = "sum(child(drdeptapprelation).dcispereport)";
                DataColumn column11 = new DataColumn("hisitemcount", typeof(int));
                column11.DefaultValue = (int)0;
                column11.Expression = "sum(child(drdeptapprelation).dcishisitemcount)";
                DataColumn column12 = new DataColumn("bihitemcount", typeof(int));
                column12.DefaultValue = (int)0;
                column12.Expression = "sum(child(drdeptapprelation).dcisbihitemcount)";
                DataColumn column13 = new DataColumn("peitemcount", typeof(int));
                column13.DefaultValue = (int)0;
                column13.Expression = "sum(child(drdeptapprelation).dcispeitemcount)";
                DataColumn column14 = new DataColumn("totalmoney", typeof(decimal));
                column14.DefaultValue = (int)0;
                column14.Expression = "sum(child(drdeptapprelation).totalmoney)";

                p_dtResult.Columns.AddRange(new DataColumn[] { column7, column8, column9, column11, column12, column14, column10, column13 });
                p_dtResult.DefaultView.Sort = "appl_deptid_chr";
                p_dtResult = p_dtResult.DefaultView.ToTable(false, new string[] { "deptname_vchr", "reportcount", "hisreportcount", "bihreportcount", "hisitemcount", "bihitemcount", "totalmoney", "pereportcount", "peitemcount" });
            }
            return rec;
        }
        #endregion

        #region m_lngQueryWorkLoadByAppDoctor
        [AutoComplete]
        public long m_lngQueryWorkLoadByAppDoctor(string p_strCatory, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult)
        {
            p_dtResult = null;
            long rec = 0;
            string Sql = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;

            if (string.IsNullOrEmpty(p_strCatory))
            {
                Sql = @"select nvl(t.appl_deptid_chr, '+') appl_deptid_chr,
                               nvl(t.appl_empid_chr, '+') appl_empid_chr,
                               t.patient_type_id_chr,
                               t.apply_unit_id_chr,
                               t.itemcount,
                               t.appunitcharge,
                               t1.lastname_vchr appdoctor,
                               t2.deptname_vchr
                          from (select a.application_id_chr,
                                       a.appl_deptid_chr,
                                       a.appl_empid_chr,
                                       a.patient_type_id_chr,
                                       b2.apply_unit_id_chr,
                                       count(b.check_item_id_chr) itemcount,
                                       sum(b.itemprice_mny) appunitcharge
                                  from t_opr_lis_application a
                                 inner join t_opr_lis_app_report a1
                                    on a.application_id_chr = a1.application_id_chr
                                   and a1.status_int = 2
                                 inner join t_opr_lis_app_check_item b
                                    on b.application_id_chr = a.application_id_chr
                                 inner join t_opr_lis_app_sample b1
                                    on b1.application_id_chr = b.application_id_chr
                                   and b1.sample_group_id_chr = b.sample_group_id_chr
                                   and b.report_group_id_chr = b1.report_group_id_chr
                                 inner join t_opr_lis_app_apply_unit b2
                                    on b.application_id_chr = b2.application_id_chr
                                 inner join t_aid_lis_sample_group_unit b3
                                    on b3.sample_group_id_chr = b.sample_group_id_chr
                                   and b3.apply_unit_id_chr = b2.apply_unit_id_chr
                                 inner join t_opr_lis_check_result b4
                                    on b4.sample_id_chr = b1.sample_id_chr
                                   and b4.groupid_chr = b1.sample_group_id_chr
                                   and b.check_item_id_chr = b4.check_item_id_chr
                                   and b4.status_int = 1
                                   and b4.result_vchr <> '\\'
                                 where a.pstatus_int = 2
                                   and (a1.confirm_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                        to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                 group by a.application_id_chr,
                                          a.appl_deptid_chr,
                                          a.appl_empid_chr,
                                          a.patient_type_id_chr,
                                          b2.apply_unit_id_chr) t
                          left outer join t_bse_employee t1
                            on t.appl_empid_chr = t1.empid_chr
                          left outer join t_bse_deptdesc t2
                            on t.appl_deptid_chr = t2.deptid_chr
                        ";

                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm[1].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                Sql = @"select nvl(t.appl_deptid_chr, '+') appl_deptid_chr,
                               nvl(t.appl_empid_chr, '+') appl_empid_chr,
                               t.patient_type_id_chr,
                               t.apply_unit_id_chr,
                               t.itemcount,
                               t.appunitcharge,
                               t1.lastname_vchr appdoctor,
                               t2.deptname_vchr
                          from (select a.application_id_chr,
                                       a.appl_deptid_chr,
                                       a.appl_empid_chr,
                                       a.patient_type_id_chr,
                                       b2.apply_unit_id_chr,
                                       count(b.check_item_id_chr) itemcount,
                                       sum(b.itemprice_mny) appunitcharge
                                  from t_opr_lis_application a
                                 inner join t_opr_lis_app_report a1
                                    on a.application_id_chr = a1.application_id_chr
                                   and a1.status_int = 2
                                 inner join t_opr_lis_app_check_item b
                                    on b.application_id_chr = a.application_id_chr
                                 inner join t_opr_lis_app_sample b1
                                    on b1.application_id_chr = b.application_id_chr
                                   and b1.sample_group_id_chr = b.sample_group_id_chr
                                   and b.report_group_id_chr = b1.report_group_id_chr
                                 inner join t_opr_lis_app_apply_unit b2
                                    on b.application_id_chr = b2.application_id_chr
                                 inner join t_aid_lis_sample_group_unit b3
                                    on b3.sample_group_id_chr = b.sample_group_id_chr
                                   and b3.apply_unit_id_chr = b2.apply_unit_id_chr
                                 inner join t_opr_lis_check_result b4
                                    on b4.sample_id_chr = b1.sample_id_chr
                                   and b4.groupid_chr = b1.sample_group_id_chr
                                   and b.check_item_id_chr = b4.check_item_id_chr
                                   and b4.status_int = 1
                                   and b4.result_vchr <> '\\'
                                 inner join t_aid_lis_apply_unit d
                                    on d.apply_unit_id_chr = b3.apply_unit_id_chr
                                 where a.pstatus_int = 2
                                   and d.check_category_id_chr = ?
                                   and a1.confirm_dat between ? and ?
                                   and (a1.confirm_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                 group by a.application_id_chr,
                                          a.appl_deptid_chr,
                                          a.appl_empid_chr,
                                          a.patient_type_id_chr,
                                          b2.apply_unit_id_chr) t
                          left outer join t_bse_employee t1
                            on t.appl_empid_chr = t1.empid_chr
                          left outer join t_bse_deptdesc t2
                            on t.appl_deptid_chr = t2.deptid_chr
                        ";

                svc.CreateDatabaseParameter(3, out parm);
                parm[0].Value = p_strCatory;
                parm[1].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm[2].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            }

            DataTable dt = null;
            rec = svc.lngGetDataTableWithParameters(Sql, ref dt, parm);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.TableName = "resulttemp";
                p_dtResult = dt.DefaultView.ToTable("p_dtResult", true, new string[] { "deptname_vchr", "appdoctor", "appl_deptid_chr", "appl_empid_chr" });
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables.Add(p_dtResult);

                DataRelation relation = new DataRelation("drdeptapprelation", new DataColumn[] { p_dtResult.Columns["appl_deptid_chr"], dt.Columns["appl_deptid_chr"] }, new DataColumn[] { dt.Columns["appl_deptid_chr"], dt.Columns["appl_empid_chr"] });
                ds.Relations.Add(relation);

                string str2 = "1";
                string str3 = "2";
                string str4 = "3";
                DataColumn column = new DataColumn("dcishisreport", typeof(int));
                column.DefaultValue = (int)0;
                column.Expression = "iif(patient_type_id_chr = '" + str3 + "', 1, 0)";
                DataColumn column2 = new DataColumn("dcishisitemcount", typeof(int));
                column2.DefaultValue = (int)0;
                column2.Expression = "iif(patient_type_id_chr = '" + str3 + "', itemcount, 0)";
                DataColumn column3 = new DataColumn("dcisbihreport", typeof(int));
                column3.DefaultValue = (int)0;
                column3.Expression = "iif(patient_type_id_chr = '" + str2 + "', 1, 0)";
                DataColumn column4 = new DataColumn("dcisbihitemcount", typeof(int));
                column4.DefaultValue = (int)0;
                column4.Expression = "iif(patient_type_id_chr = '" + str2 + "', itemcount, 0)";
                DataColumn column5 = new DataColumn("dcispereportcount", typeof(int));
                column5.DefaultValue = (int)0;
                column5.Expression = "iif(patient_type_id_chr='" + str4 + "',1,0)";
                DataColumn column6 = new DataColumn("dcispeitemcount", typeof(int));
                column6.DefaultValue = (int)0;
                column6.Expression = "iif(patient_type_id_chr='" + str4 + "',itemcount,0)";
                dt.Columns.AddRange(new DataColumn[] { column, column2, column3, column4, column5, column6 });
                DataColumn column7 = new DataColumn("dcreportcount", typeof(int));
                column7.DefaultValue = (int)0;
                column7.Expression = "count(child(drdeptapprelation).apply_unit_id_chr)";
                DataColumn column8 = new DataColumn("dchisreportcount", typeof(int));
                column8.DefaultValue = (int)0;
                column8.Expression = "sum(child(drdeptapprelation).dcishisreport)";
                DataColumn column9 = new DataColumn("dcbihreportcount", typeof(int));
                column9.DefaultValue = (int)0;
                column9.Expression = "sum(child(drdeptapprelation).dcisbihreport)";
                DataColumn column10 = new DataColumn("dcpereportcount", typeof(int));
                column10.DefaultValue = (int)0;
                column10.Expression = "sum(child(drdeptapprelation).dcispereportcount)";
                DataColumn column11 = new DataColumn("dchisitemcount", typeof(int));
                column11.DefaultValue = (int)0;
                column11.Expression = "sum(child(drdeptapprelation).dcishisitemcount)";
                DataColumn column12 = new DataColumn("dcbihitemcount", typeof(int));
                column12.DefaultValue = (int)0;
                column12.Expression = "sum(child(drdeptapprelation).dcisbihitemcount)";
                DataColumn column13 = new DataColumn("dcpeitemcount", typeof(int));
                column13.DefaultValue = (int)0;
                column13.Expression = "sum(child(drdeptapprelation).dcispeitemcount)";
                DataColumn column14 = new DataColumn("dctotalmoney", typeof(decimal));
                column14.DefaultValue = (int)0;
                column14.Expression = "sum(child(drdeptapprelation).appunitcharge)";
                p_dtResult.Columns.AddRange(new DataColumn[] { column7, column8, column9, column11, column12, column14, column10, column13 });
                p_dtResult.DefaultView.Sort = "appl_deptid_chr, appl_empid_chr";
                p_dtResult = p_dtResult.DefaultView.ToTable(false, new string[] { "deptname_vchr", "appdoctor", "dcreportcount", "dchisreportcount", "dcbihreportcount", "dchisitemcount", "dcbihitemcount", "dctotalmoney", "dcpereportcount", "dcpeitemcount" });

            }
            return rec;
        }
        #endregion

        #region m_lngQueryWorkLoadByCheckItem
        [AutoComplete]
        public long m_lngQueryWorkLoadByCheckItem(string p_strCatoryID, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult)
        {
            p_dtResult = null;
            long rec = 0;
            string Sql1 = string.Empty;
            string Sql2 = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm1 = null;
            IDataParameter[] parm2 = null;

            if (string.IsNullOrEmpty(p_strCatoryID))
            {
                Sql1 = @"select a.check_item_id_chr,
                               a.check_item_name_vchr,
                               nvl(a.abnormal_flag_chr, '正常') abnormal_flag_chr,
                               count(a.check_item_id_chr) itemcount,
                               sum(f.itemprice_mny) itemsmoney
                          from t_opr_lis_check_result a
                         inner join t_opr_lis_sample b
                            on a.sample_id_chr = b.sample_id_chr
                           and b.status_int = 6
                         inner join t_opr_lis_application b1
                            on b.application_id_chr = b1.application_id_chr
                           and b1.pstatus_int = 2
                         inner join t_bse_lis_check_item c
                            on a.check_item_id_chr = c.check_item_id_chr
                           and c.resulttype_chr = '0'
                         inner join t_opr_lis_app_check_item f
                            on f.application_id_chr = b.application_id_chr
                           and f.check_item_id_chr = c.check_item_id_chr
                           and f.sample_group_id_chr = a.groupid_chr
                         where a.status_int = 1
                           and a.result_vchr <> '\\'
                           and a.is_graph_result_num = 0
                           and (b.confirm_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                         group by a.check_item_id_chr, a.check_item_name_vchr, a.abnormal_flag_chr";

                Sql2 = @"select a.check_item_id_chr,
                               a.check_item_name_vchr,
                               a.result_vchr,
                               c.ref_value_range_vchr,
                               count(a.check_item_id_chr) itemcount,
                               sum(f.itemprice_mny) itemsmoney
                          from t_opr_lis_check_result a
                         inner join t_opr_lis_sample b
                            on a.sample_id_chr = b.sample_id_chr
                           and b.status_int = 6
                         inner join t_opr_lis_application b1
                            on b.application_id_chr = b1.application_id_chr
                           and b1.pstatus_int = 2
                         inner join t_bse_lis_check_item c
                            on a.check_item_id_chr = c.check_item_id_chr
                           and (c.resulttype_chr = '1' or c.resulttype_chr = '2')
                         inner join t_opr_lis_app_check_item f
                            on f.application_id_chr = b.application_id_chr
                           and f.check_item_id_chr = c.check_item_id_chr
                           and f.sample_group_id_chr = a.groupid_chr
                         where a.status_int = 1
                           and a.result_vchr <> '\\'
                           and a.is_graph_result_num = 0
                           and (b.confirm_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                         group by a.check_item_id_chr,
                                  a.check_item_name_vchr,
                                  a.result_vchr,
                                  c.ref_value_range_vchr";

                svc.CreateDatabaseParameter(2, out parm1);
                parm1[0].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm1[1].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");

                svc.CreateDatabaseParameter(2, out parm2);
                parm2[0].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm2[1].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                Sql1 = @"select a.check_item_id_chr,
                               a.check_item_name_vchr,
                               nvl(a.abnormal_flag_chr, '正常') abnormal_flag_chr,
                               count(a.check_item_id_chr) itemcount,
                               sum(f.itemprice_mny) itemsmoney
                          from t_opr_lis_check_result a
                         inner join t_opr_lis_sample b
                            on a.sample_id_chr = b.sample_id_chr
                           and b.status_int = 6
                         inner join t_opr_lis_application b1
                            on b.application_id_chr = b1.application_id_chr
                           and b1.pstatus_int = 2
                         inner join t_bse_lis_check_item c
                            on a.check_item_id_chr = c.check_item_id_chr
                           and c.resulttype_chr = '0'
                         inner join t_opr_lis_app_apply_unit d
                            on b.application_id_chr = d.application_id_chr
                         inner join t_aid_lis_apply_unit e
                            on e.apply_unit_id_chr = d.apply_unit_id_chr
                         inner join t_opr_lis_app_check_item f
                            on f.application_id_chr = b.application_id_chr
                           and f.check_item_id_chr = c.check_item_id_chr
                           and f.sample_group_id_chr = a.groupid_chr
                         inner join t_aid_lis_sample_group_unit g
                            on f.sample_group_id_chr = g.sample_group_id_chr
                           and e.apply_unit_id_chr = g.apply_unit_id_chr
                         where a.status_int = 1
                           and a.is_graph_result_num = 0
                           and a.result_vchr <> '\\'
                           and e.check_category_id_chr = ?
                           and (b.confirm_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                         group by a.check_item_id_chr, a.check_item_name_vchr, a.abnormal_flag_chr";

                Sql2 = @"select a.check_item_id_chr,
                               a.check_item_name_vchr,
                               a.result_vchr,
                               c.ref_value_range_vchr,
                               a.groupid_chr,
                               count(a.check_item_id_chr) itemcount,
                               sum(f.itemprice_mny) itemsmoney
                          from t_opr_lis_check_result a
                         inner join t_opr_lis_sample b
                            on a.sample_id_chr = b.sample_id_chr
                           and b.status_int = 6
                         inner join t_opr_lis_application b1
                            on b.application_id_chr = b1.application_id_chr
                           and b1.pstatus_int = 2
                         inner join t_bse_lis_check_item c
                            on a.check_item_id_chr = c.check_item_id_chr
                           and (c.resulttype_chr = '1' or c.resulttype_chr = '2')
                         inner join t_opr_lis_app_apply_unit d
                            on b.application_id_chr = d.application_id_chr
                         inner join t_aid_lis_apply_unit e
                            on e.apply_unit_id_chr = d.apply_unit_id_chr
                         inner join t_opr_lis_app_check_item f
                            on f.application_id_chr = b.application_id_chr
                           and f.check_item_id_chr = c.check_item_id_chr
                           and f.sample_group_id_chr = a.groupid_chr
                         inner join t_aid_lis_sample_group_unit g
                            on f.sample_group_id_chr = g.sample_group_id_chr
                           and e.apply_unit_id_chr = g.apply_unit_id_chr
                         where a.status_int = 1
                           and a.is_graph_result_num = 0
                           and a.result_vchr <> '\\'
                           and e.check_category_id_chr = ?
                           and (b.confirm_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                         group by a.check_item_id_chr,
                                  a.check_item_name_vchr,
                                  a.result_vchr,
                                  c.ref_value_range_vchr,
                                  a.groupid_chr";

                svc.CreateDatabaseParameter(3, out parm1);
                parm1[0].Value = p_strCatoryID;
                parm1[1].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm1[2].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");

                svc.CreateDatabaseParameter(3, out parm2);
                parm2[0].Value = p_strCatoryID;
                parm2[1].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm2[2].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");

            }
            DataColumn column = null;
            DataColumn column2 = null;
            DataColumn column3 = null;
            DataColumn column4 = null;
            DataTable dt = null;
            DataTable dt2 = null;
            DataSet ds = new DataSet();
            DataRelation relation = null;
            rec = svc.lngGetDataTableWithParameters(Sql1, ref dt, parm1);
            if (dt != null && dt.Rows.Count > 0)
            {
                column = new DataColumn("dcnormalcount", typeof(int));
                column.DefaultValue = (int)0;
                column.Expression = "iif(abnormal_flag_chr = '正常', itemcount, 0)";
                column2 = new DataColumn("dcunnormalcount", typeof(int));
                column2.DefaultValue = (int)0;
                column2.Expression = "iif(abnormal_flag_chr = '正常', 0, itemcount)";
                dt.Columns.AddRange(new DataColumn[] { column, column2 });
                dt2 = dt.DefaultView.ToTable(true, new string[] { "check_item_id_chr", "check_item_name_vchr" });
                dt.TableName = "dtResult0";
                dt2.TableName = "dtReport0";
                ds.Tables.AddRange(new DataTable[] { dt, dt2 });
                relation = new DataRelation("drrealationitem", dt2.Columns["check_item_id_chr"], dt.Columns["check_item_id_chr"]);
                ds.Relations.Add(relation);
                column3 = new DataColumn("reportcount", typeof(int));
                column3.DefaultValue = (int)0;
                column3.Expression = "sum(child(drrealationitem).itemcount)";
                column = new DataColumn("normalcount", typeof(int));
                column.DefaultValue = (int)0;
                column.Expression = "sum(child(drrealationitem).dcnormalcount)";
                column2 = new DataColumn("unnormalcount", typeof(int));
                column2.DefaultValue = (int)0;
                column2.Expression = "sum(child(drrealationitem).dcunnormalcount)";
                column4 = new DataColumn("itemtotalmoney", typeof(double));
                column4.DefaultValue = (int)0;
                column4.Expression = "sum(child(drrealationitem).itemsmoney)";
                dt2.Columns.AddRange(new DataColumn[] { column3, column, column2, column4 });
                p_dtResult = dt2.DefaultView.ToTable();
            }
            else
            {
                rec = svc.lngGetDataTableWithParameters(Sql2, ref dt, parm2);
                if (dt != null && dt.Rows.Count > 0)
                {
                    column = new DataColumn("dcnormalcount", typeof(int));
                    column.DefaultValue = (int)0;
                    column.Expression = "iif(iif(isnull(ref_value_range_vchr, '@') = '@', result_vchr, ref_value_range_vchr) = result_vchr, itemcount, 0)";
                    column2 = new DataColumn("dcunnormalcount", typeof(int));
                    column2.DefaultValue = (int)0;
                    column2.Expression = "iif(iif(isnull(ref_value_range_vchr, '@') = '@', result_vchr, ref_value_range_vchr) = result_vchr, 0, itemcount)";
                    dt.Columns.AddRange(new DataColumn[] { column, column2 });
                    dt2 = dt.DefaultView.ToTable(true, new string[] { "check_item_id_chr", "check_item_name_vchr" });
                    dt.TableName = "dtResult0";
                    dt2.TableName = "dtReport0";
                    ds.Tables.AddRange(new DataTable[] { dt, dt2 });
                    relation = new DataRelation("drrealationitem", dt2.Columns["check_item_id_chr"], dt.Columns["check_item_id_chr"]);
                    ds.Relations.Add(relation);
                    column3 = new DataColumn("reportcount", typeof(int));
                    column3.DefaultValue = (int)0;
                    column3.Expression = "sum(child(drrealationitem).itemcount)";
                    column = new DataColumn("normalcount", typeof(int));
                    column.DefaultValue = (int)0;
                    column.Expression = "sum(child(drrealationitem).dcnormalcount)";
                    column2 = new DataColumn("unnormalcount", typeof(int));
                    column2.DefaultValue = (int)0;
                    column2.Expression = "sum(child(drrealationitem).dcunnormalcount)";
                    column4 = new DataColumn("itemtotalmoney", typeof(double));
                    column4.DefaultValue = (int)0;
                    column4.Expression = "sum(child(drrealationitem).itemsmoney)";
                    dt2.Columns.AddRange(new DataColumn[] { column3, column, column2, column4 });
                    dt2.AcceptChanges();
                    if (p_dtResult != null)
                    {
                        p_dtResult.Merge(dt2);
                    }
                    else
                    {
                        p_dtResult = dt2;
                    }
                    p_dtResult.DefaultView.Sort = "check_item_id_chr";
                    p_dtResult = p_dtResult.DefaultView.ToTable();
                }
            }
            return rec;
        }
        #endregion

        #region m_lngQueryWorkLoadByCheckResult
        [AutoComplete]
        public long m_lngQueryWorkLoadByCheckResult(string p_strCatory, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult)
        {
            p_dtResult = null;
            long rec = 0;
            string Sql1 = string.Empty;
            string Sql2 = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm1 = null;
            IDataParameter[] parm2 = null;

            if (string.IsNullOrEmpty(p_strCatory))
            {
                Sql1 = @"select a.check_item_id_chr itemid,
                               a.check_item_name_vchr itemname,
                               decode(a.abnormal_flag_chr, 'H', '偏高', 'L', '偏低', '', '正常') abnor,
                               count(a.check_item_id_chr) countitem
                          from t_opr_lis_check_result a
                         inner join t_opr_lis_sample b
                            on a.sample_id_chr = b.sample_id_chr
                           and b.status_int = 6
                         inner join t_opr_lis_application b1
                            on b.application_id_chr = b1.application_id_chr
                           and b1.pstatus_int = 2
                         inner join t_bse_lis_check_item c
                            on a.check_item_id_chr = c.check_item_id_chr
                           and c.resulttype_chr = '0'
                         where a.status_int = 1
                           and a.is_graph_result_num = 0
                           and a.result_vchr <> '\\'
                           and (b.confirm_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                         group by a.check_item_id_chr, a.check_item_name_vchr, a.abnormal_flag_chr";


                Sql2 = @"select a.check_item_id_chr itemid,
                               a.check_item_name_vchr itemname,
                               a.result_vchr abnor,
                               count(a.check_item_id_chr) countitem
                          from t_opr_lis_check_result a
                         inner join t_opr_lis_sample b
                            on a.sample_id_chr = b.sample_id_chr
                           and b.status_int = 6
                         inner join t_opr_lis_application b1
                            on b.application_id_chr = b1.application_id_chr
                           and b1.pstatus_int = 2
                         inner join t_bse_lis_check_item c
                            on a.check_item_id_chr = c.check_item_id_chr
                           and (c.resulttype_chr = '1' or c.resulttype_chr = '2')
                         where a.status_int = 1
                           and a.is_graph_result_num = 0
                           and a.result_vchr != '\\'
                           and (b.confirm_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                         group by a.check_item_id_chr, a.check_item_name_vchr, a.result_vchr";

                svc.CreateDatabaseParameter(2, out parm1);
                parm1[0].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm1[1].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");

                svc.CreateDatabaseParameter(2, out parm2);
                parm2[0].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm2[1].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                Sql1 = @"select a.check_item_id_chr itemid,
                               a.check_item_name_vchr itemname,
                               decode(a.abnormal_flag_chr, 'H', '偏高', 'L', '偏低', '', '正常') abnor,
                               count(a.check_item_id_chr) countitem
                          from t_opr_lis_check_result a
                         inner join t_opr_lis_sample b
                            on a.sample_id_chr = b.sample_id_chr
                           and b.status_int = 6
                         inner join t_opr_lis_application b1
                            on b.application_id_chr = b1.application_id_chr
                           and b1.pstatus_int = 2
                         inner join t_bse_lis_check_item c
                            on a.check_item_id_chr = c.check_item_id_chr
                           and c.resulttype_chr = '0'
                         inner join t_opr_lis_app_sample d
                            on d.application_id_chr = b.application_id_chr
                           and d.sample_group_id_chr = a.groupid_chr
                         inner join t_aid_lis_sample_group_unit e
                            on e.sample_group_id_chr = d.sample_group_id_chr
                         inner join t_aid_lis_apply_unit f
                            on f.apply_unit_id_chr = e.apply_unit_id_chr
                         where a.status_int = 1
                           and a.is_graph_result_num = 0
                           and a.result_vchr <> '\\'
                           and f.check_category_id_chr = ?
                           and (b.confirm_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                         group by a.check_item_id_chr, a.check_item_name_vchr, a.abnormal_flag_chr";


                Sql2 = @"select a.check_item_id_chr itemid,
                               a.check_item_name_vchr itemname,
                               a.result_vchr abnor,
                               count(a.check_item_id_chr) countitem
                          from t_opr_lis_check_result a
                         inner join t_opr_lis_sample b
                            on a.sample_id_chr = b.sample_id_chr
                           and b.status_int = 6
                         inner join t_opr_lis_application b1
                            on b.application_id_chr = b1.application_id_chr
                           and b1.pstatus_int = 2
                         inner join t_bse_lis_check_item c
                            on a.check_item_id_chr = c.check_item_id_chr
                           and (c.resulttype_chr = '1' or c.resulttype_chr = '2')
                         inner join t_opr_lis_app_sample d
                            on d.application_id_chr = b.application_id_chr
                           and d.sample_group_id_chr = a.groupid_chr
                         inner join t_aid_lis_sample_group_unit e
                            on e.sample_group_id_chr = d.sample_group_id_chr
                         inner join t_aid_lis_apply_unit f
                            on f.apply_unit_id_chr = e.apply_unit_id_chr
                         where a.status_int = 1
                           and a.is_graph_result_num = 0
                           and a.result_vchr <> '\\'
                           and f.check_category_id_chr = ?
                           and (b.confirm_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                         group by a.check_item_id_chr, a.check_item_name_vchr, a.result_vchr";

                svc.CreateDatabaseParameter(3, out parm1);
                parm1[0].Value = p_strCatory;
                parm1[1].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm1[2].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");

                svc.CreateDatabaseParameter(3, out parm2);
                parm2[0].Value = p_strCatory;
                parm2[1].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm2[2].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            }

            DataTable dt = null;
            rec = svc.lngGetDataTableWithParameters(Sql1, ref p_dtResult, parm1);
            if (p_dtResult != null && p_dtResult.Rows.Count > 0)
            {
                p_dtResult.TableName = "itemresult";
                p_dtResult.DefaultView.Sort = "itemid";
                p_dtResult = p_dtResult.DefaultView.ToTable();
            }
            else
            {
                rec = svc.lngGetDataTableWithParameters(Sql2, ref dt, parm2);
                if (dt != null && dt.Rows.Count > 0)
                {
                    p_dtResult = new DataTable();
                    p_dtResult.PrimaryKey = new DataColumn[] { p_dtResult.Columns["check_item_id_chr"], p_dtResult.Columns["check_item_name_vchr"], p_dtResult.Columns["result_vchr"] };
                    p_dtResult.Merge(dt);
                }
            }
            return rec;
        }
        #endregion

        #region m_lngQueryWorkLoadByCommitorID
        [AutoComplete]
        public long m_lngQueryWorkLoadByCommitorID(string p_strCatoryID, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult)
        {
            p_dtResult = null;
            long rec = 0;
            string Sql = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;

            if (string.IsNullOrEmpty(p_strCatoryID))
            {
                Sql = @"select t2.empno_chr,
                               t2.lastname_vchr,
                               count(t1.apply_unit_id_chr) reportcount,
                               sum(t1.itemcount) itemcount,
                               sum(t1.totalmoney) totalmoney
                          from (select a.application_id_chr,
                                       a.confirmer_id_chr,
                                       b.apply_unit_id_chr,
                                       count(c.check_item_id_chr) itemcount,
                                       sum(c.itemprice_mny) totalmoney
                                  from t_opr_lis_app_report a
                                 inner join t_opr_lis_application a1
                                    on a1.application_id_chr = a.application_id_chr
                                   and a1.pstatus_int = 2
                                 inner join t_opr_lis_app_check_item c
                                    on a.application_id_chr = c.application_id_chr
                                   and c.report_group_id_chr = a.report_group_id_chr
                                 inner join t_opr_lis_app_sample d
                                    on a.application_id_chr = d.application_id_chr
                                   and d.sample_group_id_chr = c.sample_group_id_chr
                                   and d.report_group_id_chr = c.report_group_id_chr
                                 inner join t_aid_lis_sample_group_unit b
                                    on b.sample_group_id_chr = d.sample_group_id_chr
                                 inner join t_opr_lis_check_result e
                                    on e.sample_id_chr = d.sample_id_chr
                                   and d.sample_group_id_chr = e.groupid_chr
                                   and c.check_item_id_chr = e.check_item_id_chr
                                   and e.result_vchr <> '\\'
                                   and e.status_int = 1
                                 where a.status_int = 2
                                   and (a.confirm_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                 group by a.application_id_chr,
                                          a.confirmer_id_chr,
                                          b.apply_unit_id_chr) t1
                         inner join t_bse_employee t2
                            on t1.confirmer_id_chr = t2.empid_chr
                         group by t2.empno_chr, t2.lastname_vchr";

                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm[1].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                Sql = @"select t2.empno_chr,
                               t2.lastname_vchr,
                               count(t1.apply_unit_id_chr) reportcount,
                               sum(t1.itemcount) itemcount,
                               sum(t1.totalmoney) totalmoney
                          from (select a.application_id_chr,
                                       a.confirmer_id_chr,
                                       b.apply_unit_id_chr,
                                       count(c.check_item_id_chr) itemcount,
                                       sum(c.itemprice_mny) totalmoney
                                  from t_opr_lis_app_report a
                                 inner join t_opr_lis_application a1
                                    on a1.application_id_chr = a.application_id_chr
                                   and a1.pstatus_int = 2
                                 inner join t_opr_lis_app_check_item c
                                    on a.application_id_chr = c.application_id_chr
                                   and c.report_group_id_chr = a.report_group_id_chr
                                 inner join t_opr_lis_app_sample d
                                    on a.application_id_chr = d.application_id_chr
                                   and d.sample_group_id_chr = c.sample_group_id_chr
                                   and d.report_group_id_chr = c.report_group_id_chr
                                 inner join t_aid_lis_sample_group_unit b
                                    on b.sample_group_id_chr = d.sample_group_id_chr
                                 inner join t_aid_lis_apply_unit b1
                                    on b1.apply_unit_id_chr = b.apply_unit_id_chr
                                   and b1.check_category_id_chr = ?
                                 inner join t_opr_lis_check_result e
                                    on e.sample_id_chr = d.sample_id_chr
                                   and d.sample_group_id_chr = e.groupid_chr
                                   and c.check_item_id_chr = e.check_item_id_chr
                                   and e.result_vchr <> '\\'
                                   and e.status_int = 1
                                 where a.status_int = 2
                                   and (a.confirm_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                                       to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                                 group by a.application_id_chr,
                                          a.confirmer_id_chr,
                                          b.apply_unit_id_chr) t1
                         inner join t_bse_employee t2
                            on t1.confirmer_id_chr = t2.empid_chr
                         group by t2.empno_chr, t2.lastname_vchr
                        ";

                svc.CreateDatabaseParameter(3, out parm);
                parm[0].Value = p_strCatoryID;
                parm[1].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm[2].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            }

            rec = svc.lngGetDataTableWithParameters(Sql, ref p_dtResult, parm);
            if (p_dtResult != null)
            {
                p_dtResult.DefaultView.Sort = "lastname_vchr";
                p_dtResult = p_dtResult.DefaultView.ToTable();
            }
            return rec;
        }
        #endregion

        #region m_lngQueryWorkLoadByDevice
        [AutoComplete]
        public long m_lngQueryWorkLoadByDevice(string p_strCatory, string p_strStartDat, string p_strEndDat, out DataTable p_dtResult)
        {
            p_dtResult = null;
            long rec = 0;
            string Sql = string.Empty;
            clsHRPTableService svc = new clsHRPTableService();
            IDataParameter[] parm = null;

            if (string.IsNullOrEmpty(p_strCatory))
            {
                Sql = @"select d.device_code_chr,
                               d.devicename_vchr,
                               count(c.application_id_chr) devicecount,
                               sum(c.itemcount) checkcount,
                               sum(c.appmoney) moneytotal
                          from t_opr_lis_sample a
                         inner join t_opr_lis_application a1
                            on a1.application_id_chr = a.application_id_chr
                           and a1.pstatus_int = 2
                         inner join (select b.application_id_chr,
                                            b2.apply_unit_id_chr,
                                            count(b.check_item_id_chr) itemcount,
                                            sum(b.itemprice_mny) appmoney
                                       from t_opr_lis_app_check_item b
                                      inner join t_opr_lis_app_sample b1
                                         on b1.application_id_chr = b.application_id_chr
                                        and b1.sample_group_id_chr = b.sample_group_id_chr
                                        and b.report_group_id_chr = b1.report_group_id_chr
                                      inner join t_opr_lis_app_apply_unit b2
                                         on b.application_id_chr = b2.application_id_chr
                                      inner join t_aid_lis_sample_group_unit b3
                                         on b3.sample_group_id_chr = b.sample_group_id_chr
                                        and b3.apply_unit_id_chr = b2.apply_unit_id_chr
                                      inner join t_opr_lis_check_result b4
                                         on b4.sample_id_chr = b1.sample_id_chr
                                        and b4.groupid_chr = b1.sample_group_id_chr
                                        and b.check_item_id_chr = b4.check_item_id_chr
                                        and b4.status_int = 1
                                        and b4.result_vchr <> '\\'
                                      group by b.application_id_chr, b2.apply_unit_id_chr) c
                            on a.application_id_chr = c.application_id_chr
                          left outer join (select b1.sample_id_chr, b1.deviceid_chr
                                             from t_opr_lis_device_relation b1
                                            where (b1.status_int = 1 or b1.status_int = 2)
                                            group by b1.sample_id_chr, b1.deviceid_chr) b
                            on a.sample_id_chr = b.sample_id_chr
                          left outer join t_bse_lis_device d
                            on b.deviceid_chr = d.deviceid_chr
                         where a.status_int = 6
                           and (a.confirm_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                         group by d.device_code_chr, d.devicename_vchr";

                svc.CreateDatabaseParameter(2, out parm);
                parm[0].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm[1].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                Sql = @"select d.device_code_chr,
                               d.devicename_vchr,
                               count(c.application_id_chr) devicecount,
                               sum(c.itemcount) checkcount,
                               sum(c.appmoney) moneytotal
                          from t_opr_lis_sample a
                         inner join t_opr_lis_application a1
                            on a1.application_id_chr = a.application_id_chr
                           and a1.pstatus_int = 2
                         inner join (select b.application_id_chr,
                                            b2.apply_unit_id_chr,
                                            count(b.check_item_id_chr) itemcount,
                                            sum(b.itemprice_mny) appmoney
                                       from t_opr_lis_app_check_item b
                                      inner join t_opr_lis_app_sample b1
                                         on b1.application_id_chr = b.application_id_chr
                                        and b1.sample_group_id_chr = b.sample_group_id_chr
                                        and b.report_group_id_chr = b1.report_group_id_chr
                                      inner join t_opr_lis_app_apply_unit b2
                                         on b.application_id_chr = b2.application_id_chr
                                      inner join t_aid_lis_sample_group_unit b3
                                         on b3.sample_group_id_chr = b.sample_group_id_chr
                                        and b3.apply_unit_id_chr = b2.apply_unit_id_chr
                                      inner join t_opr_lis_check_result b4
                                         on b4.sample_id_chr = b1.sample_id_chr
                                        and b4.groupid_chr = b1.sample_group_id_chr
                                        and b.check_item_id_chr = b4.check_item_id_chr
                                        and b4.status_int = 1
                                        and b4.result_vchr <> '\\'
                                      inner join t_aid_lis_apply_unit b5
                                         on b5.apply_unit_id_chr = b2.apply_unit_id_chr
                                        and b5.check_category_id_chr = ?
                                      group by b.application_id_chr, b2.apply_unit_id_chr) c
                            on a.application_id_chr = c.application_id_chr
                          left outer join (select b1.sample_id_chr, b1.deviceid_chr
                                             from t_opr_lis_device_relation b1
                                            where (b1.status_int = 1 or b1.status_int = 2)
                                            group by b1.sample_id_chr, b1.deviceid_chr) b
                            on a.sample_id_chr = b.sample_id_chr
                          left outer join t_bse_lis_device d
                            on b.deviceid_chr = d.deviceid_chr
                         where a.status_int = 6
                           and d.check_category_id_chr = ?
                           and (a.confirm_dat between to_date(?, 'yyyy-mm-dd hh24:mi:ss') and
                               to_date(?, 'yyyy-mm-dd hh24:mi:ss'))
                         group by d.device_code_chr, d.devicename_vchr";

                svc.CreateDatabaseParameter(4, out parm);
                parm[0].Value = p_strCatory;
                parm[1].Value = p_strCatory;
                parm[2].Value = Convert.ToDateTime(p_strStartDat).Date.ToString("yyyy-MM-dd HH:mm:ss");
                parm[3].Value = Convert.ToDateTime(p_strEndDat).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            }
            rec = svc.lngGetDataTableWithParameters(Sql, ref p_dtResult, parm);
            if (p_dtResult != null)
            {
                p_dtResult.DefaultView.Sort = "device_code_chr";
                p_dtResult = p_dtResult.DefaultView.ToTable();
            }
            return rec; 
        }
        #endregion

    }
}
