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
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<EntityOprecipeitemde> lstOprecipeitemde = new List<EntityOprecipeitemde>();
        List<EntityOprecipeitemde> lstOprecipeout = new List<EntityOprecipeitemde>();


        void Query()
        {
            lstOprecipeitemde.Clear();
            lstOprecipeout.Clear();
            string cfh = string.Empty;
            cfh = txtCfh.Text.Trim();
            if (string.IsNullOrEmpty(cfh))
                return;
            SqlHelper svc = null;
            string sql = string.Empty;

            sql = @" select  t.outpatrecipeid_chr,
                                t.itemid_chr,
                                t.qty_dec,
                                t.unitid_chr,
                                t.price_mny,
                                t.tolprice_mny,
                                t.buyprice_dec, 
                                round(t.buyprice_dec * t.qty_dec - t.price_mny * t.qty_dec, 2) as difPrice
                                  from t_opr_oprecipeitemde t
                            left join t_opr_outpatientrecipeinv b
                            on t.outpatrecipeid_chr = b.outpatrecipeid_chr
                            where b.invoiceno_vchr = ";

            sql += "'" + cfh + "' order by itemid_chr";

            svc = new SqlHelper(EnumBiz.onlineDB);
            DataTable dt = svc.GetDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                EntityOprecipeitemde vo = null;
                foreach (DataRow dr in dt.Rows)
                {
                    vo = new EntityOprecipeitemde();
                    vo.outpatrecipeid = dr["outpatrecipeid_chr"].ToString();
                    vo.itemid = dr["itemid_chr"].ToString();
                    vo.qty = Function.Dec(dr["qty_dec"]);
                    vo.price = Function.Dec(dr["price_mny"]);
                    vo.buyprice = Function.Dec(dr["buyprice_dec"]);
                    vo.difPrice = Function.Dec(dr["difPrice"]);//Decimal.Round((vo.qty * vo.buyprice), 2) - Decimal.Round((vo.qty * vo.price),2) ;
                    lstOprecipeitemde.Add(vo);
                }
            }


            sql = @" select * from (select a.outpatrecipeid_chr,a.itemid_chr,a.qty_dec,a.unitprice_mny,a.tolprice_mny,a.toldiffprice_mny 
                            from t_opr_outpatientothrecipede a 
                            left join t_opr_outpatientrecipeinv b
                            on a.outpatrecipeid_chr = b.outpatrecipeid_chr
                            where b.invoiceno_vchr = ?
                            union  all
                            select a.outpatrecipeid_chr,a.itemid_chr,a.qty_dec,a.unitprice_mny,a.tolprice_mny,a.toldiffprice_mny 
                            from  t_opr_outpatientcmrecipede a 
                            left join t_opr_outpatientrecipeinv b
                            on a.outpatrecipeid_chr = b.outpatrecipeid_chr
                            where b.invoiceno_vchr = ?
                            union all
                            select a.outpatrecipeid_chr,a.itemid_chr,a.qty_dec,a.unitprice_mny,a.tolprice_mny,a.toldiffprice_mny 
                            from  t_opr_outpatientpwmrecipede a 
                            left join t_opr_outpatientrecipeinv b
                            on a.outpatrecipeid_chr = b.outpatrecipeid_chr
                            where b.invoiceno_vchr = ? ) order by itemid_chr
";

            IDataParameter[] parm = svc.CreateParm(3);
            parm[0].Value = cfh;
            parm[1].Value = cfh;
            parm[2].Value = cfh;
            DataTable dt2 = svc.GetDataTable(sql, parm);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                EntityOprecipeitemde vo = null;
                foreach (DataRow dr in dt2.Rows)
                {
                    vo = new EntityOprecipeitemde();
                    vo.outpatrecipeid = dr["outpatrecipeid_chr"].ToString();
                    vo.itemid = dr["itemid_chr"].ToString();
                    vo.qty = Function.Dec(dr["qty_dec"]);
                    vo.price = Function.Dec(dr["unitprice_mny"]);
                    vo.difPrice = Function.Dec(dr["toldiffprice_mny"]);

                    EntityOprecipeitemde opVo = lstOprecipeitemde.Find(r => r.itemid == vo.itemid);

                    if (vo.difPrice != opVo.difPrice)
                    {
                        vo.isEqual = true;
                        opVo.isEqual = true;
                    }
                    lstOprecipeout.Add(vo);

                }
            }

            this.gridControl1.DataSource = lstOprecipeitemde;
            this.gridControl1.RefreshDataSource();
            this.gridControl2.DataSource = lstOprecipeout;
            this.gridControl2.RefreshDataSource();
        }
        DataTable dtConfig { get; set; }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Query();
            
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            int hand = e.RowHandle;
            if (hand < 0) return;
            EntityOprecipeitemde vo = gridView1.GetRow(hand) as EntityOprecipeitemde;
            if (vo.isEqual)
                e.Appearance.ForeColor = Color.FromArgb(0, 0, 156);

            gridView1.Invalidate();
        }

        private void gridView2_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            int hand = e.RowHandle;
            if (hand < 0) return;
            EntityOprecipeitemde vo = gridView2.GetRow(hand) as EntityOprecipeitemde;
            if (vo.isEqual)
                e.Appearance.ForeColor = Color.FromArgb(0, 0, 156);

            gridView2.Invalidate();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lstOprecipeitemde.Exists(r => r.isEqual == true))
            {
                int affect = -1;
                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                List<DacParm> lstParm = new List<DacParm>();
                foreach (var vo in lstOprecipeitemde)
                {
                    EntityOprecipeitemde outVo = lstOprecipeout.Find(r => r.itemid == vo.itemid);

                    if (outVo != null)
                    {
                        if (vo.difPrice != outVo.difPrice)
                        {
                            string sql1 = @"update t_opr_outpatientothrecipede set toldiffprice_mny = ? where outpatrecipeid_chr = ? and itemid_chr = ?";
                            IDataParameter[] parm1 = svc.CreateParm(3);
                            parm1[0].Value = vo.difPrice;
                            parm1[1].Value = vo.outpatrecipeid;
                            parm1[2].Value = vo.itemid;


                            string sql2 = @"update t_opr_outpatientcmrecipede set toldiffprice_mny = ? where outpatrecipeid_chr = ? and itemid_chr = ?";
                            IDataParameter[] parm2 = svc.CreateParm(3);
                            parm2[0].Value = vo.difPrice;
                            parm2[1].Value = vo.outpatrecipeid;
                            parm2[2].Value = vo.itemid;

                            string sql3 = @"update t_opr_outpatientpwmrecipede set toldiffprice_mny = ? where outpatrecipeid_chr = ? and itemid_chr = ?";
                            IDataParameter[] parm3 = svc.CreateParm(3);
                            parm3[0].Value = vo.difPrice;
                            parm3[1].Value = vo.outpatrecipeid;
                            parm3[2].Value = vo.itemid;

                            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql1, parm1));
                            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql2, parm2));
                            lstParm.Add(svc.GetDacParm(EnumExecType.ExecSql, sql3, parm3));
                        }
                    }

                }

                if (lstParm.Count > 0)
                {
                    affect = svc.Commit(lstParm);
                }

                if (affect > 0)
                {
                    MessageBox.Show("成功！");
                    this.Query();
                }  
                else
                    MessageBox.Show("失败！");
            }
        }
    }

    public class EntityOprecipeitemde
    {
        public string outpatrecipeid { get; set; }
        public string itemid { get; set; }
        public decimal qty { get; set; }
        public decimal price { get; set; }
        public decimal buyprice { get; set;}
        public decimal difPrice { get; set; }

        public bool isEqual { get; set; }
    }
}
