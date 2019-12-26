using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{   
    /// <summary>
    /// ҩƷ��ҩ�������
    /// </summary>
    public partial class frmReturnMedicine : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// �ۼ�������� ����0401
        /// </summary>
        internal int m_intDeductFlow = 0;
        /// <summary>
        /// ���캯��
        /// </summary>
        public frmReturnMedicine()
        {
            InitializeComponent();
        }
        public override void CreateController()
        {
            this.objController = new clsControlReturnMedicine();
            this.objController.Set_GUI_Apperance(this);
        }
        /// <summary>
        /// ��ǰҩ��id
        /// </summary>
        public string m_strMedStoreid;
        /// <summary>
        /// ��ʾ����ĳ��ҩ����ҩ��ҩ������Ϣ
        /// </summary>
        /// <param name="strMedStoreid"></param>
        public void m_mthSetShow(string strMedStoreid)
        {
            m_strMedStoreid = strMedStoreid.Trim();
            this.Show();
        }
        private void frmReturnMedicine_Load(object sender, EventArgs e)
        {

            this.m_cboFindType.SelectedIndex = 4;
            this.m_dgvReturnMed.AutoGenerateColumns = false;
            this.m_dgvSendMed.AutoGenerateColumns = false;
            this.m_dgvDetail.AutoGenerateColumns = false;
            this.m_dgvReturnDetail.AutoGenerateColumns = false;
            this.m_dgvSourceRecipeDetail.AutoGenerateColumns = false;

            m_intDeductFlow = int.Parse(objController.m_objComInfo.m_lonGetModuleInfo("0401"));
            ((clsControlReturnMedicine)this.objController).m_mthInitialGUI(m_strMedStoreid);
            ((clsControlReturnMedicine)this.objController).m_mthGetReturnMedicine();
            
        }

        private void m_btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_datCurrentDate_ValueChanged(object sender, EventArgs e)
        {
            //((clsControlReturnMedicine)this.objController).m_mthGetReturnMedicine();
        }

        private void m_dgvSendMed_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int iRow = 0; iRow < this.m_dgvSendMed.Rows.Count; iRow++)
            {
                m_dgvSendMed.Rows[iRow].Cells[0].Value = iRow + 1;
            }
        }

        private void m_dgvSendMed_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (int iRow = 0; iRow < this.m_dgvSendMed.Rows.Count; iRow++)
            {
                m_dgvSendMed.Rows[iRow].Cells[0].Value = iRow + 1;
            }
        }

        private void m_dgvReturnMed_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int iRow = 0; iRow < this.m_dgvReturnMed.Rows.Count; iRow++)
            {
                this.m_dgvReturnMed.Rows[iRow].Cells[0].Value = iRow + 1;
            }
        }

        private void m_dgvReturnMed_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (int iRow = 0; iRow < this.m_dgvReturnMed.Rows.Count; iRow++)
            {
                this.m_dgvReturnMed.Rows[iRow].Cells[0].Value = iRow + 1;
            }
        }

        private void ctlDataGridView_EnterAsTab1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void m_dgvSendMed_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                if (this.m_dgvSendMed.CurrentCell == null) return;
                ((clsControlReturnMedicine)this.objController).m_mthBindDetailData(0);
            }

        }

        private void m_dgvDetail_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int iRow = 0; iRow < this.m_dgvDetail.Rows.Count; iRow++)
            {
                m_dgvDetail.Rows[iRow].Cells[0].Value ="False";
            }
        }

        private void m_dgvDetail_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (int iRow = 0; iRow < this.m_dgvDetail.Rows.Count; iRow++)
            {
                m_dgvDetail.Rows[iRow].Cells[0].Value = "False";
            }
        }

        private void m_dgvDetail_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (this.m_dgvDetail.Rows.Count == 0) return;
                this.m_dgvDetail.CurrentCell = this.m_dgvDetail.Rows[0].Cells[1];
                if (this.m_dgvDetail.Columns[e.ColumnIndex].HeaderText.Trim() == "ȫѡ")
                {
                    this.m_dgvDetail.Columns[e.ColumnIndex].HeaderText = "��ѡ";
                    for (int i = 0; i < this.m_dgvDetail.Rows.Count; i++)
                    {
                        this.m_dgvDetail.Rows[i].Cells[0].Value = "True";
                        this.m_dgvDetail.CurrentCell = this.m_dgvDetail.Rows[i].Cells[1];
                    }
                }
                else
                {
                    this.m_dgvDetail.Columns[e.ColumnIndex].HeaderText = "ȫѡ";
                    for (int i = 0; i < this.m_dgvDetail.Rows.Count; i++)
                    {
                        if (this.m_dgvDetail.Rows[i].Cells[0].Value.ToString() == "True")
                        {
                            this.m_dgvDetail.Rows[i].Cells[0].Value = "False";
                        }
                        else
                        {
                            this.m_dgvDetail.Rows[i].Cells[0].Value = "True";
                        }
                        this.m_dgvDetail.CurrentCell = this.m_dgvDetail.Rows[i].Cells[1];
                    }
             
                }
             
            }
        }

        private void m_btnReturn_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 1) return;
            if (this.m_dgvSendMed.CurrentCell == null)
            {
                MessageBox.Show("����ѡ��һ���ѷ�ҩ�Ĵ���������ҩ��");
                return;
            }
            if (this.m_dgvDetail.Rows.Count == 0)
            {
                MessageBox.Show("�ô����������κ���Ҫ��ҩ����ϸ��Ϣ��");
                return;
            }
            else
            {
                this.m_dgvDetail.CurrentCell = this.m_dgvDetail.Rows[0].Cells[1];
                for (int i = 0; i < this.m_dgvDetail.Rows.Count; i++)
                {
                    if (this.m_dgvDetail.Rows[i].Cells[0].Value.ToString() == "True")
                    {
                        break;
                    }
                    if (i == this.m_dgvDetail.Rows.Count - 1)
                    {
                        MessageBox.Show("��ѡ����Ҫ��ҩ�Ĵ�����ϸ��Ϣ��");
                        return;
                    }
                }
            }
            this.m_dgvDetail.CurrentCell = this.m_dgvDetail.Rows[0].Cells[1];
            ((clsControlReturnMedicine)this.objController).m_mthReturnMedicine();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                this.m_dgvDetail.Visible = true ;
                this.m_dgvReturnDetail.Visible = false;
            }
            else
            {
                this.m_dgvDetail.Visible = false;
                this.m_dgvReturnDetail.Visible = true;
            }
        }

        private void m_dgvReturnMed_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 1)
            {
                if (this.m_dgvReturnMed.CurrentCell == null) return;
                ((clsControlReturnMedicine)this.objController).m_mthBindDetailData(1);
            }
        }

        private void m_dgvReturnDetail_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int iRow = 0; iRow < this.m_dgvReturnDetail.Rows.Count; iRow++)
            {
                this.m_dgvReturnDetail.Rows[iRow].Cells[0].Value = iRow + 1;
            }
        }

        private void m_dgvReturnDetail_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (int iRow = 0; iRow < this.m_dgvReturnDetail.Rows.Count; iRow++)
            {
                this.m_dgvReturnDetail.Rows[iRow].Cells[0].Value = iRow + 1;
            }
        }
        private void m_btnRefresh_Click(object sender, EventArgs e)
        {
            ((clsControlReturnMedicine)this.objController).m_mthGetReturnMedicine();
        }

        private void m_btnFind_Click(object sender, EventArgs e)
        {
            ((clsControlReturnMedicine)this.objController).m_mthGetReturnMedicine();
            string m_strRowFilter = "";
            switch (this.m_cboFindType.SelectedIndex)
            {
                case 0: m_strRowFilter = "patientcardid_chr='" + this.m_cboText.Text.Trim() + "'"; break;
                case 1: m_strRowFilter = "lastname_vchr='" + this.m_cboText.Text.Trim() + "'"; break;
                case 2: m_strRowFilter = "serno_chr='" + this.m_cboText.Text.Trim() + "'"; break;
                case 3: m_strRowFilter = "outpatrecipeid_chr='" + this.m_cboText.Text.Trim() + "'"; break;
                case 4: m_strRowFilter = "invoiceno_vchr='" + this.m_cboText.Text.Trim() + "'"; break;
            }
            if (this.m_dgvSendMed.DataSource != null)
            {
                if (this.m_dgvSendMed.DataSource is DataTable)
                {
                    this.m_dgvSendMed.DataSource = ((DataTable)this.m_dgvSendMed.DataSource).DefaultView;
                    ((DataView)this.m_dgvSendMed.DataSource).RowFilter = m_strRowFilter;
                }
                else
                {
                    ((DataView)this.m_dgvSendMed.DataSource).RowFilter = m_strRowFilter;
                }
                if (this.m_dgvSendMed.Rows.Count == 0)
                {
                    this.m_dgvDetail.DataSource = null;
                }
            }
            if (this.m_dgvReturnMed.DataSource != null)
            {
                if (this.m_dgvReturnMed.DataSource is DataTable)
                {
                    this.m_dgvReturnMed.DataSource = ((DataTable)this.m_dgvReturnMed.DataSource).DefaultView;
                    ((DataView)this.m_dgvReturnMed.DataSource).RowFilter = m_strRowFilter;
                }
                else
                {
                    ((DataView)this.m_dgvReturnMed.DataSource).RowFilter = m_strRowFilter;
                }
                if (this.m_dgvReturnMed.Rows.Count == 0)
                {
                    this.m_dgvReturnDetail.DataSource = null;
                }
               
            }
        }

        private void m_cboText_KeyDown(object sender, KeyEventArgs e)
        {   
            
            if (e.KeyCode == Keys.Enter)
            {
                if (this.m_cboFindType.SelectedIndex == 0)
                {
                    this.m_cboText.Text = this.m_cboText.Text.PadLeft(10, '0');
                }
                this.m_btnFind.Select();
            }
        }

        private void m_dgvDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void m_dgvDetail_EnterKeyPress(DataGridViewCell CurrentCell, out bool CancelJump)
        {
            CancelJump = false;
            if (CurrentCell == null)
            {
                return;
            }
            this.m_dgvDetail.EndEdit();
            if (CurrentCell.ColumnIndex == 6)
            {
                string strValue = string.Empty;
                if (CurrentCell.Value != null)
                {
                    strValue = CurrentCell.Value.ToString();
                    double dblResult=0d;
                     if(double.TryParse(strValue,out dblResult))
                     {
                         if (dblResult > Convert.ToDouble(this.m_dgvDetail.Rows[CurrentCell.RowIndex].Cells["m_txtOrgAmount"].Value))
                         {
                             MessageBox.Show("�������ҩ�������ܴ���ԭʼ��ҩ������","iCareϵͳ��ʾ��Ϣ",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                             CancelJump = true;
                             return;
                         }
                         else if (dblResult<=0)
                         {
                             MessageBox.Show("�������ҩ����Ӧ�����㣡", "iCareϵͳ��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                             CancelJump = true;
                             return;
                         }
                     
                     }
                     else
                     {
                         MessageBox.Show("��������ȷ����ҩ������", "iCareϵͳ��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         CancelJump = true;
                         return;
                     }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0) return;
            if (this.m_dgvReturnMed.CurrentCell == null)
            {
                MessageBox.Show("����ѡ��һ������ҩ�Ĵ��������ش���ҩƾ�ݣ�");
                return;
            }
            if (this.m_dgvReturnDetail.Rows.Count == 0)
            {
                MessageBox.Show("�ô����������κ���Ҫ��ҩ����ϸ��Ϣ��");
                return;
            }
            this.m_dgvReturnDetail.CurrentCell = this.m_dgvReturnDetail.Rows[0].Cells[1];
            ((clsControlReturnMedicine)this.objController).m_mthRePrintBill();
        }

        private void m_dgvSourceRecipeDetail_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int iRow = 0; iRow < this.m_dgvSourceRecipeDetail.Rows.Count; iRow++)
            {
                this.m_dgvSourceRecipeDetail.Rows[iRow].Cells[0].Value = iRow + 1;
            }
        }

        private void m_dgvSourceRecipeDetail_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (int iRow = 0; iRow < this.m_dgvSourceRecipeDetail.Rows.Count; iRow++)
            {
                this.m_dgvSourceRecipeDetail.Rows[iRow].Cells[0].Value = iRow + 1;
            }
        }

        private void m_dgvReturnDetail_DoubleClick(object sender, EventArgs e)
        {
            ((clsControlReturnMedicine)this.objController).m_mthShowSourceRecipeDetail();
        }

        private void m_dgvSourceRecipeDetail_Leave(object sender, EventArgs e)
        {
            this.m_dgvSourceRecipeDetail.Visible = false;
        }

        private void m_btnRollBack_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0) return;
            if (this.m_dgvReturnMed.CurrentCell == null)
            {
                MessageBox.Show("����ѡ��һ������ҩ�������г�����ҩ��");
                return;
            }
            if (this.m_dgvReturnDetail.Rows.Count == 0)
            {
                MessageBox.Show("�ô����������κ���Ҫ������ҩ����ϸ��Ϣ��");
                return;
            }
            ((clsControlReturnMedicine)this.objController).m_mthRollBackReturnMedicine();
        }


    }
}