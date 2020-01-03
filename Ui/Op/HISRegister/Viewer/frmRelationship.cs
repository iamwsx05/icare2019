using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmRelationship : Form
    {
        /// <summary>
        /// ��Ŀ�ɣ�
        /// </summary>
        /// <param name="p_strITEMID_CHR"></param>
        public frmRelationship(string p_strITEMID_CHR)
        {
            strITEMID_CHR = p_strITEMID_CHR;
            InitializeComponent();
        }
        /// <summary>
        /// ��Ŀ�ɣ�
        /// </summary>
        private string strITEMID_CHR = "";
        private void frmRelationship_Load(object sender, EventArgs e)
        {
            if (strITEMID_CHR != "")
            {
                clsDomainControl_ChargeItem objsvc = new clsDomainControl_ChargeItem();
                DataTable dt = new DataTable();
                long res = objsvc.m_lngGetData(@"SELECT   a.itemcode_vchr as ��Ŀ����, trim(a.itemname_vchr) as ��Ŀ����, a.itemspec_vchr as ��Ŀ���,a.ITEMUNIT_CHR as ��Ŀ��λ,
                                             a.itemprice_mny as ��Ŀ�۸� ,a.PDCAREA_VCHR as ����
                                                     FROM t_bse_chargeitem a, t_bse_subchargeitem b
                                                     WHERE b.ITEMID_CHR = a.itemid_chr AND b.subitemid_chr = '" + strITEMID_CHR + @"'
                                                        ORDER BY a.itemcode_vchr", out dt);
                if (res > 0)
                {
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void frmRelationship_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}