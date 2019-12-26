using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ����(����)סԺ��Ʊ��������ӡ֮������ĿUI��
    /// </summary>
    public partial class frmDefInvoiceBillItems : Form
    {
        /// <summary>
        /// ����
        /// </summary>
        public frmDefInvoiceBillItems()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���� 2 ���﷢Ʊ 4 סԺ��Ʊ
        /// </summary>
        private string CatScope = "4";

        /// <summary>
        /// �ⲿ����
        /// </summary>
        /// <param name="scope">2 ���﷢Ʊ 4 סԺ��Ʊ</param>
        public void m_mthShow(string scope)
        {
            CatScope = scope;

            if (CatScope == "2")
            {
                this.Text = "���﷢Ʊ��������ӡ�ķ�����Ŀ���崰��";
            }
            else if (CatScope == "4")
            {
                this.Text = "סԺ��Ʊ��������ӡ�ķ�����Ŀ���崰��";
            }
            else if (CatScope == "5")
            {
                this.Text = "[�°汾]סԺ��Ʊ��������ӡ�ķ�����Ŀ���崰��";
            }
            else if (CatScope == "8")
            {
                this.Text = "[�°汾]���﷢Ʊ��������ӡ�ķ�����Ŀ���崰��";
            }

            this.ShowDialog();
        }

        private void frmDefInvoiceBillItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            int nums = 0;

            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (this.dataGridView1.Rows[i].Cells["ColPrtCtrl"].Value.ToString().Trim() != "")
                {
                    nums++;
                }
            }

            if (this.hasDef.Count != nums && blnSave == false)
            {
                if (MessageBox.Show("������Ϣ�������ģ��Ƿ񱣴棿", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    this.m_blnSave();
                }
            }

            this.Close();
        }

        #region ����
        private DataTable dtOrg = new DataTable();
        private Hashtable hasDef = new Hashtable();
        private bool blnSave = false;
        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void m_mthInit()
        {
            string CatID = "";

            DataTable dt;

            clsDcl_Charge objCharge = new clsDcl_Charge();

            long l = 0;

            if (CatScope == "2")//�ɰ�����
                l = objCharge.m_lngGetChargeItemCat(int.Parse(CatScope), out dtOrg);
            else if (CatScope == "4")//�ɰ�סԺ
                l = objCharge.m_lngGetChargeItemCat(int.Parse(CatScope), out dtOrg);
            else if (CatScope == "8")//�°�����
                l = objCharge.m_lngGetChargeItemCat(2, out dtOrg);
            else if (CatScope == "5")//�°�סԺ
                l = objCharge.m_lngGetChargeItemCat(4, out dtOrg);

            if (l > 0)
            {
                if (CatScope == "8")
                {
                    l = objCharge.m_lngGetDefChargeCat("8", "%", out dt);
                }
                else if (CatScope == "5")
                {
                    l = objCharge.m_lngGetDefChargeCat("5", "%", out dt);
                }
                else
                    l = objCharge.m_lngGetDefChargeCat(CatScope, "%", out dt);

                if (l > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CatID = dt.Rows[i]["catid_chr"].ToString();

                        if (!hasDef.ContainsKey(CatID))
                        {
                            hasDef.Add(CatID, dt.Rows[i]);
                        }
                    }
                }                
            }            
        }
        #endregion

        #region ��ȡ���סԺ��Ʊ������Ϣ
        /// <summary>
        /// ��ȡ���סԺ��Ʊ������Ϣ
        /// </summary>
        public void m_mthGetIPInvoCat()
        {
            this.m_mthInit();

            if (dtOrg.Rows.Count == 0)
            {
                return;
            }

            string CatID = "";

            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                CatID = dtOrg.Rows[i]["typeid_chr"].ToString();

                string[] s = new string[7];

                s[0] = Convert.ToString(i + 1);
                s[1] = CatID;
                s[2] = dtOrg.Rows[i]["typename_vchr"].ToString();

                if (hasDef.ContainsKey(CatID))
                {
                    DataRow dr = hasDef[CatID] as DataRow;

                    if (dr["type_int"].ToString() == "0")
                    {
                        s[3] = "��ͨ��";
                    }
                    else if (dr["type_int"].ToString() == "1")
                    {
                        s[3] = "������";
                    }

                    if (dr["compexp_vchr"].ToString().Trim() == "*")
                    {
                        s[4] = "";
                    }
                    else
                    {
                        s[4] = dr["compexp_vchr"].ToString();
                    }                    
                    
                    s[5] = dr["prtclt_vchr"].ToString();

                    if (dr["status_int"].ToString() == "0")
                    {
                        s[6] = "ͣ��";
                    }
                    else if (dr["status_int"].ToString() == "1")
                    {
                        s[6] = "����";
                    }                 
                }
                else
                {                                        
                    s[3] = "��ͨ��";
                    s[4] = "";
                    s[5] = "";
                    s[6] = "ͣ��";
                }

                int row = this.dataGridView1.Rows.Add(s);
                this.dataGridView1.Rows[row].Tag = dtOrg.Rows[i];
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public bool m_blnSave()
        {
            this.dataGridView1.Refresh();

            List<clsBihChargeItemCat_VO> ChargeItemCatArr = new List<clsBihChargeItemCat_VO>();

            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (this.dataGridView1.Rows[i].Cells["ColPrtCtrl"].Value.ToString().Trim() != "")
                {
                    DataRow dr = this.dataGridView1.Rows[i].Tag as DataRow;

                    clsBihChargeItemCat_VO ChargeItemCat_VO = new clsBihChargeItemCat_VO();

                    ChargeItemCat_VO.Scope = CatScope;
                    ChargeItemCat_VO.CatID = dr["typeid_chr"].ToString();
                    ChargeItemCat_VO.CatName = dr["typename_vchr"].ToString();                    
                    ChargeItemCat_VO.Type = (this.dataGridView1.Rows[i].Cells["ColType"].Value.ToString().Trim() == "��ͨ��" ? "0" : "1");
                    if (this.dataGridView1.Rows[i].Cells["ColComputeExp"].Value == null || this.dataGridView1.Rows[i].Cells["ColComputeExp"].Value.ToString().Trim() == "")
                    {
                        ChargeItemCat_VO.CompExp = "*";
                    }
                    else
                    {
                        ChargeItemCat_VO.CompExp = this.dataGridView1.Rows[i].Cells["ColComputeExp"].Value.ToString().Trim();
                    }
                    ChargeItemCat_VO.DispCtl = "*";
                    ChargeItemCat_VO.PrtClt = this.dataGridView1.Rows[i].Cells["ColPrtCtrl"].Value.ToString().Trim();
                    ChargeItemCat_VO.Status = (this.dataGridView1.Rows[i].Cells["ColStatus"].Value.ToString().Trim() == "ͣ��" ? "0" : "1");

                    ChargeItemCatArr.Add(ChargeItemCat_VO);
                }
            }

            clsDcl_Charge objCharge = new clsDcl_Charge();

            long l = objCharge.m_lngSaveInvoiceSet(ChargeItemCatArr, CatScope);
            if (l > 0)
            {
                blnSave = true;
                MessageBox.Show("������Ϣ����ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("����������Ϣʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }
        #endregion

        private void frmDefInvoiceBillItems_Load(object sender, EventArgs e)
        {
            this.m_mthGetIPInvoCat();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.m_blnSave();
        }
    }
}
