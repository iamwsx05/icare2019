using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using weCare.Core.Entity;


namespace com.digitalwave.iCare.BIHOrder
{
    public partial class frmSelectBox : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// ����
        /// </summary>
        private string m_strTitle = "";
        /// <summary>
        /// ״̬0��ҽ����ʼʱ�䣬1-ҽ������ʱ����޸�,2-��ʾ������Ϣ,3-�޸ķ�������,4-���δ���,5-���˻�ԭ��,6-�޸�����,7-�¿�ҽ�����úϼ�
        /// </summary>
        private int m_intStatue = 0;
        /// <summary>
        /// ҽ���ı�����
        /// </summary>
        private string m_strName = "";
        /// <summary>
        /// Ԥ�����
        /// </summary>
        private decimal m_decPreMoney = 0;
        /// <summary>
        /// ���ý��
        /// </summary>
        private decimal m_decUseMoney = 0;
        /// <summary>
        /// �ѽ���
        /// </summary>
        private decimal m_decClearMoney = 0;
        /// <summary>
        /// �������
        /// </summary>
        private decimal m_decPrePayMoney = 0;
        /// <summary>
        /// ��ע��Ϣ��������ע��
        /// </summary>
        private string m_strDec = "";
        /// <summary>
        /// �޸ķ�������
        /// </summary>
        public double m_dblLIMITRATE_MNY = 0;
        /// <summary>
        /// ���δ���
        /// </summary>
        public int m_intATTACHTIMES_INT = 0;
        /// <summary>
        /// �¿�ҽ�����úϼ�
        /// </summary>
        public decimal m_decNewOrderMoney = 0;
        /// <summary>
        /// �˻�ԭ��
        /// </summary>
        public string m_strBackReasion = "";
        private clsBIHOrder ReOrder = null;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime m_datValue = DateTime.MinValue;


        public frmSelectBox()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        /// <summary>
        /// �޸ķ�������
        /// </summary>
        /// <param name="m_intValue"></param>
        public frmSelectBox(string m_strReasion, int m_intValue)
        {
            m_intStatue = m_intValue;
            m_strBackReasion = m_strReasion;
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="m_intValue"></param>
        public frmSelectBox(clsBIHOrder order, int m_intValue)
        {
            ReOrder = order;
            m_intStatue = m_intValue;
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }
        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="m_intValue"></param>
        public frmSelectBox(decimal PreMoney, decimal UseMoney, decimal ClearMoney, decimal PrePayMoney, string m_Dec, int m_intValue)
        {
            m_intStatue = m_intValue;
            m_decPreMoney = PreMoney;
            m_decUseMoney = UseMoney;
            m_decClearMoney = ClearMoney;
            m_decPrePayMoney = PrePayMoney;
            m_strDec = m_Dec;
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        /// <summary>
        /// �޸ķ�������
        /// </summary>
        /// <param name="m_intValue"></param>
        public frmSelectBox(double LIMITRATE_MNY, int m_intValue)
        {
            m_intStatue = m_intValue;
            m_dblLIMITRATE_MNY = LIMITRATE_MNY;
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        /// <summary>
        /// �޸ķ�������
        /// </summary>
        /// <param name="ATTACHTIMES_INT">���δ���</param>
        /// <param name="m_intValue"></param>
        public frmSelectBox(int ATTACHTIMES_INT, int m_intValue)
        {
            m_intStatue = m_intValue;
            m_intATTACHTIMES_INT = ATTACHTIMES_INT;
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        /// <summary>
        /// �޸ķ�������
        /// </summary>
        /// <param name="ATTACHTIMES_INT">���δ���</param>
        /// <param name="m_intValue"></param>
        public frmSelectBox(decimal m_decMoney, int m_intValue)
        {
            m_intStatue = m_intValue;
            m_decNewOrderMoney = m_decMoney;
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        public override void CreateController()
        {
        }
        private void cmdSaveBihRegister_Click(object sender, EventArgs e)
        {

            if (m_intStatue == 0)//��ʼ����
            {
                m_datValue = this.m_dtSelectTime.Value;
                if (ReOrder != null)
                {
                    if (ReOrder.m_dtFinishDate > DateTime.MinValue && m_datValue > ReOrder.m_dtFinishDate)
                    {
                        MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.m_dtSelectTime.Focus();
                        return;
                    }
                }
            }
            else if (m_intStatue == 1)//��������
            {
                m_datValue = this.m_dtSelectTime.Value;
                if (ReOrder != null)
                {
                    if (m_datValue < ReOrder.m_dtStartDate && ReOrder.m_intStatus != 2)
                    {
                        MessageBox.Show("����ʱ�䲻�����ڿ�ʼʱ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.m_dtSelectTime.Focus();
                        return;
                    }
                    else if (m_datValue < ReOrder.m_dtExecutedate && ReOrder.m_intStatus == 2)
                    {
                        MessageBox.Show("��ִ�е�ҽ��������ʱ�䲻������ִ�е�ʱ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.m_dtSelectTime.Focus();
                        return;
                    }
                }
            }
            else if (m_intStatue == 3)//��������
            {
                try
                {
                    m_dblLIMITRATE_MNY = double.Parse(double.Parse(this.m_txtLIMITRATE_MNY.Text).ToString("0.0000"));
                    if (m_dblLIMITRATE_MNY > 999999.9999)
                    {
                        MessageBox.Show("���ܴ���999999.9999", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        this.m_txtLIMITRATE_MNY.Focus();
                        return;
                    }

                }
                catch
                {
                    MessageBox.Show("��������ָ�ʽ����ȷ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.m_txtLIMITRATE_MNY.Focus();
                    return;
                }
            }
            else if (m_intStatue == 4)//����
            {
                try
                {
                    m_intATTACHTIMES_INT = int.Parse(this.m_txtLIMITRATE_MNY.Text);
                    if (m_intATTACHTIMES_INT < 0)
                    {
                        MessageBox.Show("���δ���ֻ�ܴ��ڻ����0!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        this.m_txtLIMITRATE_MNY.Focus();
                        return;
                    }

                }
                catch
                {
                    MessageBox.Show("��������ָ�ʽ����ȷ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.m_txtLIMITRATE_MNY.Focus();
                    return;
                }

            }
            else if (m_intStatue == 6)//�޸�����
            {
                try
                {
                    m_intATTACHTIMES_INT = int.Parse(this.m_txtLIMITRATE_MNY.Text);
                    if (m_intATTACHTIMES_INT < 0)
                    {
                        MessageBox.Show("�޸�����ֻ�ܴ��ڻ����0!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        this.m_txtLIMITRATE_MNY.Focus();
                        return;
                    }

                }
                catch
                {
                    MessageBox.Show("��������ָ�ʽ����ȷ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.m_txtLIMITRATE_MNY.Focus();
                    return;
                }

            }
            this.DialogResult = DialogResult.Yes;
        }

        private void buttonXP1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmdSaveBihRegister.Focus();
            }
        }

        private void DotorComfirmBox_Load(object sender, EventArgs e)
        {

        }

        private void DotorComfirmBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    buttonXP1_Click(null, null);
                    break;
            }
            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.S:
                        cmdSaveBihRegister_Click(null, null);
                        break;
                }

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void frmSelectBox_Load(object sender, EventArgs e)
        {
            switch (m_intStatue)
            {
                case 0:
                    this.Text = "�޸�����ʱ��";
                    this.m_lblName.Text = "ҽ������ʱ��";
                    this.m_lblName.Visible = true;
                    this.m_dtSelectTime.Visible = true;
                    break;
                case 1:
                    this.Text = "�޸�ͣ��ʱ��";
                    this.m_lblName.Text = "ҽ��ͣ��ʱ��";
                    this.m_lblName.Visible = true;
                    this.m_dtSelectTime.Visible = true;

                    break;
                case 2:
                    this.Text = "�շ���Ϣ";
                    this.m_lblMoney1.Text = this.m_lblMoney1.Text.ToString() + m_decPreMoney.ToString();
                    this.m_lblMoney2.Text = this.m_lblMoney2.Text.ToString() + m_decUseMoney.ToString();
                    this.m_lblMoney3.Text = this.m_lblMoney3.Text.ToString() + m_decClearMoney.ToString();
                    this.m_lblMoney4.Text = this.m_lblMoney4.Text.ToString() + m_decPrePayMoney.ToString();
                    this.m_lblDec.Text = this.m_lblDec.Text.ToString() + m_strDec;
                    this.m_lblMoney1.Visible = true;
                    this.m_lblMoney2.Visible = true;
                    this.m_lblMoney3.Visible = true;
                    this.m_lblMoney4.Visible = true;
                    this.m_lblDec.Visible = true;


                    break;
                case 3:
                    this.Text = "���÷�������";
                    this.m_lblName.Text = "��������";
                    this.m_lblName.Visible = true;
                    this.m_txtLIMITRATE_MNY.Text = m_dblLIMITRATE_MNY.ToString();
                    m_txtLIMITRATE_MNY.Visible = true;
                    break;
                case 4://����
                    this.Text = "����ҽ������";
                    this.m_lblName.Text = "ҽ������";
                    this.m_lblName.Visible = true;
                    this.m_txtLIMITRATE_MNY.Text = this.m_intATTACHTIMES_INT.ToString();
                    m_txtLIMITRATE_MNY.Visible = true;
                    break;
                case 5://�鿴�˻�ԭ��
                    this.m_lblBackReasion.Text = this.m_lblBackReasion.Text.ToString() + " " + m_strBackReasion.Trim();
                    this.m_lblBackReasion.Visible = true;
                    break;
                case 6://�޸�����
                    this.Text = "����ҽ������";
                    this.m_lblName.Text = "ҽ������";
                    this.m_lblName.Visible = true;
                    this.m_txtLIMITRATE_MNY.Text = this.m_intATTACHTIMES_INT.ToString();
                    m_txtLIMITRATE_MNY.Visible = true;
                    break;
                case 7://�¿�ҽ�����úϼ�
                    this.Text = "�¿�ҽ�����úϼ�";

                    this.m_lblNewOrderMoney.Visible = true;
                    this.m_lblNewOrderMoney.Text = this.m_lblNewOrderMoney.Text + " " + decimal.Round(this.m_decNewOrderMoney, 2);
                    break;
            }
        }

        private void m_txtLIMITRATE_MNY_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;

            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            { }
            else if ((e.KeyChar == 8) || (e.KeyChar == 13))
            { }
            else if ((e.KeyChar == '.'))
            { }
            else
            {
                e.Handled = true;
            }
        }

        private void m_txtLIMITRATE_MNY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdSaveBihRegister.Focus();
            }
        }




    }
}