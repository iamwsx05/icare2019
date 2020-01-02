using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using weCare.Core.Entity;
using com.digitalwave.Utility;


namespace com.digitalwave.iCare.BIHOrder
{
    public partial class frmORDERDESC : Form
    {
        /// <summary>
        /// �𶨵�ҽ�������ֵ��
        /// </summary>
        DataTable m_dtORDERDESC;
        /// <summary>
        /// 0-��ѯѡ��,1-����
        /// </summary>
        int m_intStatue = 0;
        /// <summary>
        /// ҽ������
        /// </summary>
        string m_strORDERDESC = "";
        /// <summary>
        /// Ա��ID
        /// </summary>
        private string  m_strEmpId="";
        /// <summary>
        /// Ա������
        /// </summary>
        private string m_strEmpName = "";
        /// <summary>
        /// ������ҽ��������ϢVO
        /// </summary>
        private ArrayList m_arrOrderdescVO = new ArrayList();
        clsDcl_OrderDesc m_objManager = null;
        public frmORDERDESC()
        {
            InitializeComponent();
        }

        public frmORDERDESC(int statue,string EmpId,string EmpName)
        {
            InitializeComponent();
            m_intStatue = statue;
            m_strEmpId=EmpId;
            m_strEmpName = EmpName;
            m_objManager = new clsDcl_OrderDesc();
        }

        public string getTheORDERDESC()
        {
            return m_strORDERDESC;
        }

        public ArrayList getTheOrderdescVOarr()
        {
            return m_arrOrderdescVO;
        }

        public frmORDERDESC(DataTable m_dtDesc)
        {
            InitializeComponent();
            this.m_dtORDERDESC = m_dtDesc;
        }

        private void frmORDERDESC_Load(object sender, EventArgs e)
        {
            if (this.m_dtORDERDESC != null && m_intStatue == 0)
            {
                this.m_dgvORDERDESC.AutoGenerateColumns = false;
                this.m_dgvORDERDESC.DataSource = m_dtORDERDESC;
                this.m_plButtons.Visible = false;
            }
            else if(m_intStatue==1)//����״̬
            {
                this.m_dgvORDERDESC.AllowUserToAddRows = true;
                this.m_dgvORDERDESC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
                this.m_dgvORDERDESC.ReadOnly = false;
                this.m_dgvORDERDESC.Columns["m_colWBCODE_VCHR"].ReadOnly = true;
                this.m_dgvORDERDESC.Columns["m_colPYCODE_CHR"].ReadOnly = true;
                this.m_dgvORDERDESC.CurrentCell=this.m_dgvORDERDESC.Rows[0].Cells[1];
                
            }
        }

        private void m_dgvORDERDESC_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.m_intStatue == 0)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (this.m_dgvORDERDESC.SelectedRows.Count > 0)
                        {
                            this.m_strORDERDESC = this.m_dgvORDERDESC.SelectedRows[0].Cells["m_colDESC_VCHR"].Value.ToString();
                        }
                        this.DialogResult = DialogResult.OK;
                        break;
                    case Keys.Escape:
                        this.DialogResult = DialogResult.Cancel;
                        break;
                }
            }
           
        }

        private void m_dgvORDERDESC_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (m_intStatue == 0)
            {
                if (this.m_dgvORDERDESC.SelectedRows.Count > 0)
                {
                    this.m_strORDERDESC = this.m_dgvORDERDESC.SelectedRows[0].Cells["m_colDESC_VCHR"].Value.ToString();

                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void m_dgvORDERDESC_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 1)
            {
                string strAny="";
                string m_strPY="";
                string m_strWB="";
                if (m_dgvORDERDESC.Rows[e.RowIndex].Cells["m_colDESC_VCHR"].Value == null)
                {
                    m_dgvORDERDESC.Rows[e.RowIndex].Cells["m_colDESC_VCHR"].Value = "";
                }
                strAny = m_dgvORDERDESC.Rows[e.RowIndex].Cells["m_colDESC_VCHR"].Value.ToString().Trim();
                //����ƴ���������
                m_lngGetpywb( strAny,out  m_strPY,out  m_strWB);
                m_dgvORDERDESC.Rows[e.RowIndex].Cells["m_colPYCODE_CHR"].Value = m_strPY;
                m_dgvORDERDESC.Rows[e.RowIndex].Cells["m_colWBCODE_VCHR"].Value = m_strWB;

                /*<==========================*/
            }
           
        }

        internal void m_lngGetpywb(string strAny,out string m_strPY,out string m_strWB)
        {
            m_strPY = "";
            m_strWB = "";
            try
            {
               
                clsCreateChinaCode getChinaCode = new clsCreateChinaCode();
                m_strPY = getChinaCode.m_strCreateChinaCode(strAny, ChinaCode.PY).Trim();
                m_strWB = getChinaCode.m_strCreateChinaCode(strAny, ChinaCode.WB).Trim();
            }
            catch
            {
                MessageBox.Show("�������������/ƴ��������벻Ҫ��Ӣ����ĸ", "icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public class MyDataGridView : DataGridView
        {
            public MyDataGridView()
            {
            }
            protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
            {
                if (this.AllowUserToAddRows == true)
                {
                    if (keyData == Keys.Enter)
                    {
                        if (this.CurrentCell.ColumnIndex == 2)
                        {
                            this.CurrentCell = this.Rows[this.CurrentCell.RowIndex].Cells[4];
                        }

                        SendKeys.Send("{Tab}");
                        return true;

                    }
                }
                   
                return base.ProcessCmdKey(ref msg, keyData);

            }

       

        }

        private void m_btnSave_Click(object sender, EventArgs e)
        {
            m_arrOrderdescVO = new ArrayList();
            for (int i = 0; i < this.m_dgvORDERDESC.RowCount; i++)
            {
                if (m_dgvORDERDESC.Rows[i].Cells["m_colDESC_VCHR"].Value == null)
                {
                    m_dgvORDERDESC.Rows[i].Cells["m_colDESC_VCHR"].Value = "";
                }
              
                if (m_dgvORDERDESC.Rows[i].Cells["m_colDESC_VCHR"].Value.ToString().Trim().Equals(""))
                {
                    continue;
                }
                if (m_dgvORDERDESC.Rows[i].Cells["m_colUSERCODE_VCHR"].Value == null)
                {
                    m_dgvORDERDESC.Rows[i].Cells["m_colUSERCODE_VCHR"].Value = "";
                }
                if (m_dgvORDERDESC.Rows[i].Cells["m_colUSERCODE_VCHR"].Value.ToString().Trim().Equals(""))
                {
                    MessageBox.Show("�û����벻��Ϊ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			
                    this.m_dgvORDERDESC.CurrentCell = this.m_dgvORDERDESC.Rows[i].Cells["m_colUSERCODE_VCHR"];
                    return;
                }
                if (m_dgvORDERDESC.Rows[i].Cells["m_colWBCODE_VCHR"].Value == null)
                {
                    m_dgvORDERDESC.Rows[i].Cells["m_colWBCODE_VCHR"].Value = "";
                }
                if (m_dgvORDERDESC.Rows[i].Cells["m_colPYCODE_CHR"].Value == null)
                {
                    m_dgvORDERDESC.Rows[i].Cells["m_colPYCODE_CHR"].Value = "";
                }

                clsOrderdescVO OrderDec = new clsOrderdescVO();
                OrderDec.strDescID = "";
                OrderDec.strDesc = m_dgvORDERDESC.Rows[i].Cells["m_colDESC_VCHR"].Value.ToString().Trim();
                OrderDec.strUserCode = m_dgvORDERDESC.Rows[i].Cells["m_colUSERCODE_VCHR"].Value.ToString().Trim();
                OrderDec.strWbCode = m_dgvORDERDESC.Rows[i].Cells["m_colWBCODE_VCHR"].Value.ToString().Trim();
                OrderDec.strPyCode = m_dgvORDERDESC.Rows[i].Cells["m_colPYCODE_CHR"].Value.ToString().Trim();
                OrderDec.strEmpID = m_strEmpId;
                OrderDec.strEmpName = m_strEmpName;
                m_arrOrderdescVO.Add(OrderDec);
            }
            if (m_arrOrderdescVO.Count > 0)
            {
               long lngRef= m_objManager.m_lngAddNewOrderdescVO((clsOrderdescVO[])(m_arrOrderdescVO.ToArray(typeof(clsOrderdescVO))));
               if (lngRef > 0)
               {
                   MessageBox.Show("����ɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
				
               }
            }

            //this.DialogResult = DialogResult.OK;
        }

        private void frmORDERDESC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (m_intStatue == 1)
                {
                    m_btnSave_Click(null, null);
                }
            }
        }


     
    }
}