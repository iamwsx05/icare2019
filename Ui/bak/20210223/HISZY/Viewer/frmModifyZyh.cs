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
    /// �޸�סԺ�Ž�����
    /// </summary>
    public partial class frmModifyZyh : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ����
        /// <summary>
        ///����
        /// </summary>
        public frmModifyZyh()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="patientid">����ID��</param>
        /// <param name="no">סԺ(����)��</param>
        /// <param name="type">��Ժ���� 1 ��ͨ 2 ����</param>
        public frmModifyZyh(string patientid, string no, int type)
        {
            InitializeComponent();
            pid = patientid;
            zyh = no;
            intype = type;
        }
        #endregion

        #region ����������
        /// <summary>
        /// ������Ժ�Ǽ���ˮ��
        /// </summary>
        private string regid = "";
        /// <summary>
        /// ����ID
        /// </summary>
        private string pid = "";
        /// <summary>
        /// סԺ��
        /// </summary>
        private string zyh = "";
        /// <summary>
        /// סԺ����
        /// </summary>
        private int zycs = 0;
        /// <summary>
        /// ��Ժ���� 1 ��ͨסԺ 2 ����סԺ
        /// </summary>
        private int intype = 0;

        /// <summary>
        /// ������Ժ�Ǽ���ˮ��
        /// </summary>
        public string RegisterID
        {
            set
            {
                regid = value;
            }
            get
            {
                return regid;
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        public string PatientID
        {
            set
            {
                pid = value;
            }
            get
            {
                return pid;
            }
        }
        /// <summary>
        /// סԺ��
        /// </summary>
        public string Zyh
        {
            set
            {
                zyh = value;
            }
            get
            {
                return zyh;
            }
        }
        /// <summary>
        /// סԺ����
        /// </summary>
        public int Zycs
        {
            set
            {
                zycs = value;
            }
            get
            {
                return zycs;
            }
        }

        #endregion

        /// <summary>
        /// ����������
        /// </summary>
        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.clsCtl_ModifyZyh();
            objController.Set_GUI_Apperance(this);
        }

        private void frmModifyZyh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            ((clsCtl_ModifyZyh)this.objController).m_mthFind(1);
        }

        private void frmModifyZyh_Load(object sender, EventArgs e)
        {
            ((clsCtl_ModifyZyh)this.objController).m_mthSetval(zyh, 1);
            ((clsCtl_ModifyZyh)this.objController).m_mthCheckHisinfo(pid, intype);
            this.cboType.SelectedIndex = 0;
            this.chkAuto.Checked = true;
            this.txtNewNO.Focus();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ((clsCtl_ModifyZyh)this.objController).m_mthModifyNO();
        }

        private void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAuto.Checked)
            {
                this.txtNewNO.Enabled = false;
                this.chkUnion.Checked = false;
            }
            else
            {
                this.txtNewNO.Enabled = true;
            }
        }

        private void chkUnion_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkUnion.Checked)
            {
                this.txtNewNO.Text = "";
                this.chkAuto.Checked = false;                
            }
        }

        private void btnFindOldNO_Click(object sender, EventArgs e)
        {
            ((clsCtl_ModifyZyh)this.objController).m_mthFind(2);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}