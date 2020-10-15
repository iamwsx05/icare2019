using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Dac;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace test
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string pid = txtCfh.Text;
            string checkOutType = string.Empty;
            DataTable dt = null;
            long ret = YBCheckDiagnose(checkOutType, pid, out dt);

            if (ret >= 0)
            {
                this.gridControl1.DataSource = dt;

                this.gridControl1.RefreshDataSource();
            }
            else
            {
                MessageBox.Show(ret.ToString());
            }
        }


        #region 检测是否已填住院诊断
        /// <summary>
        /// 新的住院诊断检测
        /// </summary>
        /// <param name="checkOutType"></param>
        /// <param name="registerID"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public long YBCheckDiagnose(string checkOutType, string registerID, out DataTable dtResult)
        {
            dtResult = new DataTable();
            string Sql = @"select a.inpatientid,
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

            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            try
            {
                IDataParameter[] parm = svc.CreateParm(1, new List<object>() { registerID });
                dtResult = svc.GetDataTable(Sql, parm);
                if (dtResult == null || dtResult.Rows.Count == 0)
                {
                    Sql = @" select a.inpatientid,
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

                    parm = svc.CreateParm(1, new List<object>() { registerID });
                    dtResult = svc.GetDataTable(Sql, parm);

                    // 2020-08-31 JHEMR
                    if (dtResult == null || dtResult.Rows.Count == 0)
                    {
                        Sql = @"select v.Inp_No,
                                       (case
                                         when v.diagnosis_type = 2 then
                                          v.diagnosis_desc
                                         else
                                          ''
                                       end) as inhospitaldiagnose_right,
                                       (case
                                         when v.diagnosis_type = 3 then
                                          v.diagnosis_desc
                                         else
                                          ''
                                       end) as outhospitaldiagnose_right
                                  from jhemr.emr_zdxx v
                                 where v.patient_id = ?";

                        SqlHelper svcJhemr = new SqlHelper(EnumBiz.emrDB);
                        parm = svcJhemr.CreateParm(1, new List<object>() { registerID });
                        DataTable dt = svcJhemr.GetDataTable(Sql, parm);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string ipNo = dt.Rows[0]["Inp_No"].ToString();
                            string inDiag = string.Empty;
                            string outDiag = string.Empty;
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr["outhospitaldiagnose_right"] != DBNull.Value && dr["outhospitaldiagnose_right"].ToString().Trim() != "")
                                {
                                    outDiag += dr["outhospitaldiagnose_right"].ToString() + " ";
                                }
                                //inDiag += dr["inhospitaldiagnose_right"].ToString() + " ";
                                //outDiag += dr["outhospitaldiagnose_right"].ToString() + " ";
                            }
                            dtResult = dt.Clone();
                            if (outDiag != string.Empty)
                            {
                                DataRow drNew = dtResult.NewRow();
                                drNew["Inp_No"] = ipNo;
                                //drNew["inhospitaldiagnose_right"] = inDiag.Trim();
                                drNew["outhospitaldiagnose_right"] = outDiag.Trim();
                                dtResult.Rows.Add(drNew);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex.Message);
            }
            finally
            {
                svc = null;
            }
            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return dtResult.Rows.Count;
            }
        }
        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            string Sql = @"select distinct b.check_item_id_chr,b.seq_int from t_criticalvalue_ref_lisdept b 
                                    where b.check_item_id_chr in (select a.check_item_id_chr from t_criticalvalue_ref_lisdept a 
                                    where a.deptid_chr = '0000001' ) ";

            SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
            try
            {
                dtResult = svc.GetDataTable(Sql);
                if (dtResult == null || dtResult.Rows.Count > 0)
                {
                    foreach(DataRow dr in dtResult.Rows)
                    {
                        string item = dr["check_item_id_chr"].ToString();
                        string seq = dr["seq_int"].ToString();

                        string sql11 = "select * from t_criticalvalue_ref_lisdept where check_item_id_chr = '" + item + "' and seq_int = '" + seq + "' and deptid_chr = '0000376'"  ;
                        DataTable dt = svc.GetDataTable(sql11);
                        if(dt != null &&　dt.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            string Sql1 = "insert into t_criticalvalue_ref_lisdept values('" + item + "','" + seq + "'," + "'0000376','脾胃病科')";
                            svc.ExecSql(Sql1);
                        }

                        string sql22 = "select * from t_criticalvalue_ref_lisdept where check_item_id_chr = '" + item + "' and seq_int = '" + seq + "' and deptid_chr = '0000377'";
                        dt = svc.GetDataTable(sql11);
                        if (dt != null && dt.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            string Sql2 = "insert into t_criticalvalue_ref_lisdept values('" + item + "','" + seq + "'," + "'0000377','脾胃病科病区')";
                            svc.ExecSql(Sql2);
                        }

                        string sql33 = "select * from t_criticalvalue_ref_lisdept where check_item_id_chr = '" + item + "' and seq_int = '" + seq + "' and deptid_chr = '0000378'";
                        dt = svc.GetDataTable(sql11);
                        if (dt != null && dt.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            string Sql3 = "insert into t_criticalvalue_ref_lisdept values('" + item + "','" + seq + "'," + "'0000378','脾胃病科门诊')";
                            svc.ExecSql(Sql3);
                            svc.ExecSql(Sql3);
                        }   
                    }  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MessageBox.Show("1111111");
            }
        }
    }
}
