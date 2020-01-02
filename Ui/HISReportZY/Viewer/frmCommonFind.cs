using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS.Reports
{
    public partial class frmCommonFind : com.digitalwave.GUI_Base.frmMDI_Child_Base 
    {
        #region ����
        /// <summary>
        /// ���ڱ���
        /// </summary>
        internal string Titleinfo = "";
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
        /// ��������
        /// </summary>
        private string patname = "";
        /// <summary>
        /// ״̬ 1 ����Ժ 2 �Ѱ��Ŵ�λ 3 ��Ժ
        /// </summary>
        private string instatus = "";
        /// <summary>
        /// ��Ժʱ��
        /// </summary>
        private string outdate = "";
        /// <summary>
        /// ��Ժ���� 1 ��ͨסԺ 2 ����סԺ
        /// </summary>
        private int intype = 0;
        /// <summary>
        /// ���ƿ���
        /// </summary>
        private string cardno = "";
        #endregion

        #region ����
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
        /// <summary>
        /// ���ƿ���
        /// </summary>
        public string CardNo
        {
            set
            {
                cardno = value;
            }
            get
            {
                return cardno;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string PatName
        {
            set
            {
                patname = value;
            }
            get
            {
                return patname;
            }
        }
        /// <summary>
        /// ״̬ 1 ����Ժ 2 �Ѱ��Ŵ�λ 3 ��Ժ
        /// </summary>
        public string InStatus
        {
            set
            {
                instatus = value;
            }
            get
            {
                return instatus;
            }
        }
        /// <summary>
        /// ��Ժʱ��
        /// </summary>
        public string OutDate
        {
            set
            {
                outdate = value;
            }
            get
            {
                return outdate;
            }
        }
        /// <summary>
        /// ��Ժ���� 1 ��ͨסԺ 2 ����סԺ
        /// </summary>
        public int InType
        {
            set
            {
                intype = value;
            }
            get
            {
                return intype;
            }
        }
        /// <summary>
        /// �Ƿ���Ժ�Ǽ�(����)
        /// </summary>
        public bool IsBihReg
        {
            set
            {
                ((clsCtl_CommonFind)this.objController).BlnInReg = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        private string frmTitle = "";
        /// <summary>
        /// ��ѯ״̬ 0 ȫ�� 1 ��Ժ 2 Ԥ��Ժ 3 ��ʽ��Ժ 4 ��� 8 ��Ժ���� 9 Ԥ��������         
        /// </summary>
        internal int Status = 0;
        #endregion

        /// <summary>
        /// ����
        /// </summary>
        public frmCommonFind()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="title">�������</param>
        /// <param name="status">�������ͣ� 0 ȫ�� 1 ��Ժ 2 Ԥ��Ժ 3 ��ʽ��Ժ 9 Ԥ�������� </param>
        public frmCommonFind(string title, int status)
        {
            InitializeComponent();
            frmTitle = title;
            Status = status;
        }

        /// <summary>
        /// ����������
        /// </summary>
        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.Reports.clsCtl_CommonFind();
            objController.Set_GUI_Apperance(this);
        }

        private void frmCommonFind_Load(object sender, EventArgs e)
        {
            if (frmTitle.Trim() != "")
            {
                this.Text = frmTitle;
            }
            if (Status == 3)
            {
                this.btnArea.Enabled = false;
            }

            if (this.lsvPatient.Items.Count > 0)
            {
                this.timer1.Enabled = true;
                this.timer1.Interval = 100;
            }

            this.chkInDate.Checked = true;
        }

        private void frmCommonFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();   
            }
            else if (e.KeyCode == Keys.F3)
            {
                ((clsCtl_CommonFind)this.objController).m_mthFind();
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.m_ShortCutFind(1);                
            }
        }

        #region ��ݲ���
        /// <summary>
        /// ��ݲ���
        /// </summary>
        /// <param name="flag">1 ����סԺ�� 2 �������ƿ���</param>
        private void m_ShortCutFind(int flag)
        {
            string tmpZyh = clsPublic.m_strReadParmZyh();
            string tmpCardNo = clsPublic.m_strReadParmCardNo();
            if (tmpZyh == "")
            {
                MessageBox.Show("�Բ���û����ʷ��ѯ��¼��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int count = this.m_intFindByZyh(tmpZyh, false, false);
                
                if (flag == 2)
                {
                    this.txtZyh.Text = "";
                    this.txtCardNo.Text = tmpCardNo;
                }

                if (count == 0)
                {
                    MessageBox.Show("�Բ���û���ҵ�������������Ĳ�����Ϣ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            }

            if (this.lsvPatient.Items.Count > 0)
            {
                this.lsvPatient.Items[0].Selected = true;
                this.lsvPatient.Focus();
            }
            else
            {
                if (flag == 1)
                {
                    this.txtZyh.Focus();
                    this.txtZyh.SelectAll();
                }
                else if (flag == 2)
                {                
                    this.txtCardNo.Focus();
                    this.txtCardNo.SelectAll();
                }
            }
        }
        #endregion
            
        private void btnFind_Click(object sender, EventArgs e)
        {
            ((clsCtl_CommonFind)this.objController).m_mthFind();
        }

        private void lsvPatient_DoubleClick(object sender, EventArgs e)
        {
            ((clsCtl_CommonFind)this.objController).m_mthGetPatientinfo();
        }

        private void btnArea_Click(object sender, EventArgs e)
        {
            ((clsCtl_CommonFind)this.objController).m_mthFindArea();
        }

        private void lsvPatient_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                return;
            }
            else
            {
                lsvPatient.Sorting = (lsvPatient.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending);
                lsvPatient.ListViewItemSorter = new ListViewItemSort(e.Column, lsvPatient.Sorting, lsvPatient);
                lsvPatient.Sort();

                for (int i = 1; i <= lsvPatient.Items.Count; i++)
                {
                    lsvPatient.Items[i - 1].SubItems[0].Text = i.ToString();
                }
            }
        }

        private void chkUnionAnd_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUnionAnd.Checked)
            {
                if (chkUnionOr.Checked)
                {
                    chkUnionOr.Checked = false;
                }
            }
        }

        private void chkUnionOr_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUnionOr.Checked)
            {
                if (chkUnionAnd.Checked)
                {
                    chkUnionAnd.Checked = false;
                }
            }
        }

        #region ��ʽ����
        /// <summary>
        /// ��סԺ�Ų���
        /// </summary>
        /// <param name="Zyh">סԺ��</param>
        /// <param name="Ismatch">�Ƿ�ģ������ true �� false ��</param>
        /// <param name="IsIncludeMZ">�Ƿ����������Ϣ true �� false ��</param>
        /// <returns>��¼��</returns>
        public int m_intFindByZyh(string Zyh, bool Ismatch, bool IsIncludeMZ)
        {
            this.txtZyh.Text = Zyh;
            this.chkMatch.Checked = Ismatch;
            this.chkMZ.Checked = IsIncludeMZ;
            return ((clsCtl_CommonFind)this.objController).m_mthFind(Zyh, Ismatch, 0, IsIncludeMZ);
        }

        /// <summary>
        /// ��סԺ�Ų���
        /// </summary>
        /// <param name="Zyh">סԺ��</param>
        /// <param name="Ismatch">�Ƿ�ģ������ true �� false ��</param>        
        /// <returns>��¼��</returns>
        public int m_intFindByZyh(string Zyh, bool Ismatch)
        {
            this.txtZyh.Text = Zyh;
            this.chkMatch.Checked = Ismatch;
            return ((clsCtl_CommonFind)this.objController).m_mthFind(Zyh, Ismatch, 0, false);
        }

        /// <summary>
        /// �����ƿ��Ų���
        /// </summary>
        /// <param name="Cardno">���ƿ���</param>
        /// <param name="Ismatch">�Ƿ�ģ������ true �� false ��</param>
        /// <param name="IsIncludeMZ">�Ƿ����������Ϣ true �� false ��</param>
        /// <returns>��¼��</returns>
        public int m_intFindByCardno(string Cardno, bool Ismatch, bool IsIncludeMZ)
        {
            this.txtCardNo.Text = Cardno;
            this.chkMatch.Checked = Ismatch;
            this.chkMZ.Checked = IsIncludeMZ;
            return ((clsCtl_CommonFind)this.objController).m_mthFind(Cardno, Ismatch, 1, IsIncludeMZ);
        }

        /// <summary>
        /// �����ƿ��Ų���
        /// </summary>
        /// <param name="Cardno">���ƿ���</param>
        /// <param name="Ismatch">�Ƿ�ģ������ true �� false ��</param>        
        /// <returns>��¼��</returns>
        public int m_intFindByCardno(string Cardno, bool Ismatch)
        {
            this.txtCardNo.Text = Cardno;
            this.chkMatch.Checked = Ismatch;
            return ((clsCtl_CommonFind)this.objController).m_mthFind(Cardno, Ismatch, 1, false);
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="Name">����</param>
        /// <param name="Ismatch">�Ƿ�ģ������ true �� false ��</param>
        /// <param name="IsIncludeMZ">�Ƿ����������Ϣ true �� false ��</param>
        /// <returns>��¼��</returns>
        public int m_intFindByName(string Name, bool Ismatch, bool IsIncludeMZ)
        {
            this.txtName.Text = Name;
            this.chkMatch.Checked = Ismatch;
            this.chkMZ.Checked = IsIncludeMZ;
            return ((clsCtl_CommonFind)this.objController).m_mthFind(Name, Ismatch, 2, IsIncludeMZ);
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="Name">����</param>
        /// <param name="Ismatch">�Ƿ�ģ������ true �� false ��</param>        
        /// <returns>��¼��</returns>
        public int m_intFindByName(string Name, bool Ismatch)
        {
            this.txtName.Text = Name;
            this.chkMatch.Checked = Ismatch;
            return ((clsCtl_CommonFind)this.objController).m_mthFind(Name, Ismatch, 2, false);
        }
        #endregion

        /// <summary>
        /// ���������Ա���Ժ����(��ͨ����)����
        /// </summary>
        /// <param name="Name">����</param>
        /// <param name="Sex">�Ա�</param>
        /// <param name="Type">���� 1 ��ͨ 2 ����</param>
        /// <param name="Ismatch">�Ƿ�ģ������ true �� false ��</param>
        /// <param name="IsIncludeMZ">�Ƿ����������Ϣ true �� false ��</param>
        /// <returns></returns>
        public int m_intFindByNameSexType(string Name, string Sex, int Type, bool Ismatch, bool IsIncludeMZ)
        {
            this.txtName.Text = Name;
            this.chkMatch.Checked = Ismatch;
            this.chkMZ.Checked = IsIncludeMZ;            
            return ((clsCtl_CommonFind)this.objController).m_mthFind(Name, Sex, Type, Ismatch, IsIncludeMZ);
        }

        /// <summary>
        /// ���������Ա���Ժ����(��ͨ����)����
        /// </summary>
        /// <param name="Name">����</param>
        /// <param name="Sex">�Ա�</param>
        /// <param name="Type">���� 1 ��ͨ 2 ����</param>
        /// <param name="Ismatch">�Ƿ�ģ������ true �� false ��</param>        
        /// <returns></returns>
        public int m_intFindByNameSexType(string Name, string Sex, int Type, bool Ismatch)
        {
            this.txtName.Text = Name;
            this.chkMatch.Checked = Ismatch;
            return ((clsCtl_CommonFind)this.objController).m_mthFind(Name, Sex, Type, Ismatch, false);
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string s = this.txtCardNo.Text.Trim();
                if (s != "")
                {
                    s = s.PadLeft(10, '0');
                    this.txtCardNo.Text = s;
                }
            }
        }

        private void lsvPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsCtl_CommonFind)this.objController).m_mthGetPatientinfo();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lsvPatient.Items[0].Selected = true;
            this.lsvPatient.Focus();
            this.timer1.Enabled = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblZyh_MouseEnter(object sender, EventArgs e)
        {
            this.lblZyh.ForeColor = Color.FromArgb(255, 154, 0);
        }

        private void lblZyh_MouseLeave(object sender, EventArgs e)
        {
            this.lblZyh.ForeColor = Color.White;
        }

        private void lblZyh_Click(object sender, EventArgs e)
        {
            this.m_ShortCutFind(1);
        }

        private void lblCardNo_MouseEnter(object sender, EventArgs e)
        {
            this.lblCardNo.ForeColor = Color.FromArgb(255, 154, 0);
        }

        private void lblCardNo_MouseLeave(object sender, EventArgs e)
        {
            this.lblCardNo.ForeColor = Color.White;
        }

        private void lblCardNo_Click(object sender, EventArgs e)
        {
            this.m_ShortCutFind(2);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ((clsCtl_CommonFind)this.objController).m_mthGetPatientinfo();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            this.m_ShortCutFind(1);
        }

        private void chkInDate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkInDate.Checked)
            {
                this.dtBegin_in.Enabled = true;
                this.dtEnd_in.Enabled = true;
            }
            else
            {
                this.dtBegin_in.Enabled = false;
                this.dtEnd_in.Enabled = false;
            }
        }

        private void chkOutDate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkOutDate.Checked)
            {
                this.dtBegin_out.Enabled = true;
                this.dtEnd_out.Enabled = true;
            }
            else
            {
                this.dtBegin_out.Enabled = false;
                this.dtEnd_out.Enabled = false;
            }
        }       
    }
}