using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// �շ���Ŀά��
    /// </summary>
    public class frmChargeItem3 : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        internal com.digitalwave.iCare.gui.HIS.exComboBox m_cmbType;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.DataGrid dataGrid1;
        internal PinkieControls.ButtonXP m_btFind;
        internal System.Windows.Forms.ComboBox m_cmbFind;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.TextBox m_txtPacQ;
        internal System.Windows.Forms.ComboBox m_cmbCoustomPrice;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        internal System.Windows.Forms.TextBox m_txtInsuranceID;
        private System.Windows.Forms.Label label22;
        internal System.Windows.Forms.TextBox m_txtchargeNO;
        private System.Windows.Forms.Label label19;
        internal System.Windows.Forms.TextBox m_txtSourName;
        internal System.Windows.Forms.TextBox m_txtSour;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        internal com.digitalwave.iCare.gui.HIS.exComboBox m_cboUsage;
        private System.Windows.Forms.Label label16;
        internal com.digitalwave.iCare.gui.HIS.exComboBox m_cboDosUnit;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        internal com.digitalwave.iCare.gui.HIS.exComboBox m_cboIPUnit;
        private System.Windows.Forms.Label label9;
        internal com.digitalwave.iCare.gui.HIS.exComboBox m_cboOPUnit;
        private System.Windows.Forms.Label label8;
        internal com.digitalwave.iCare.gui.HIS.exComboBox m_cboUnit;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox m_txtPrice;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox m_txtSpec;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox m_txtWB;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox m_txtPY;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox m_txtName;
        internal System.Windows.Forms.TextBox m_txtNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label20;
        internal System.Windows.Forms.TextBox m_txtDosage;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private PinkieControls.ButtonXP btExit;
        private PinkieControls.ButtonXP btClear;
        private PinkieControls.ButtonXP btDel;
        internal PinkieControls.ButtonXP btSave;
        private PinkieControls.ButtonXP btAdd;
        internal PinkieControls.ButtonXP bt_Refresh;
        internal System.Windows.Forms.ComboBox m_cmbOPUseUint;
        internal System.Windows.Forms.ComboBox m_cmbIsInDocAdv;
        internal System.Windows.Forms.ComboBox m_cmbIsExpensive;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        internal System.Windows.Forms.TextBox m_txtTradePrice;
        private System.Windows.Forms.Label label28;
        internal PinkieControls.ButtonXP btChangeCat;
        internal com.digitalwave.iCare.gui.HIS.exComboBox m_cmbType2;
        internal System.Windows.Forms.TextBox txtEnglishName;
        private System.Windows.Forms.Label label27;
        internal System.Windows.Forms.ComboBox cmbIsStop;
        private System.Windows.Forms.Label label29;
        internal com.digitalwave.controls.SearchComboBox m_txtFind;
        private System.Windows.Forms.Label label30;
        internal System.Windows.Forms.TextBox txtProduceing;
        private System.Windows.Forms.Label label31;
        internal System.Windows.Forms.ComboBox m_cmbIPUseUint;
        internal System.Windows.Forms.Label lbePrice;
        internal System.Windows.Forms.Label lbeIPPrice;
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;
        internal PinkieControls.ButtonXP btPrint;
        internal PinkieControls.ButtonXP btEditDiscount;
        private System.Windows.Forms.Label label35;
        internal com.digitalwave.iCare.gui.HIS.exComboBox COBINSURANCETYPE;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        internal com.digitalwave.iCare.gui.HIS.exComboBox m_cboApplyType;
        private System.Windows.Forms.Label label34;
        internal com.digitalwave.iCare.gui.HIS.exComboBox cmbCheckPart;
        internal PinkieControls.ButtonXP btPrice;
        internal System.Windows.Forms.Label lbePS;
        internal PinkieControls.ButtonXP buttonXP1;
        private Label label36;
        internal System.Windows.Forms.TextBox txtCommName;
        private GroupBox groupBox1;
        private Label label37;
        private Label label38;
        private Label label39;
        internal exComboBox cboInpInsuranceType;
        internal ListView lsvDefaultFreq;
        private ColumnHeader columnHeader14;
        private ColumnHeader columnHeader15;
        private ColumnHeader columnHeader16;
        internal TextBox m_txtDefaultFreq;
        private Label label40;
        internal ComboBox m_cmbSelf;
        private Label label41;
        private Label label42;
        internal exComboBox m_cboKeepUse;
        internal ListView m_lsvExcType;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        internal TextBox m_txtOPInvoice;
        internal TextBox m_txtIPInvoice;
        internal TextBox m_txtIPCal;
        internal TextBox m_txtOPCal;
        internal TextBox m_txtCaseCal;
        internal ListView m_lsvExeType;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        internal TextBox m_txtExeType;
        internal ListView m_lsvOrderType;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        internal TextBox m_txtOrderCateType;
        internal ComboBox cboSfcl;
        private Label label43;
        private Label label45;
        internal System.Windows.Forms.TextBox txtCityUnicode;
        private Label label46;
        internal PinkieControls.ButtonXP btnRegular;
        internal TextBox txtRegular;
        internal ComboBox cboItemScope;
        private Label label47;
        internal System.Windows.Forms.TextBox txtOriPrice;
        private Label label44;
        private Label label48;
        internal ComboBox cboIsChildPrice;
        /// <summary>
        /// ����Ȩ��
        /// </summary>
        public string strPopedom = "";
        public frmChargeItem3()
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
        }

        public void m_mthShow(string p_strPopedom)
        {

            strPopedom = p_strPopedom.Trim();
            this.Show();
        }
        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.clsCtl_ChargeItem2();
            objController.Set_GUI_Apperance(this);
        }
        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChargeItem3));
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.btPrice = new PinkieControls.ButtonXP();
            this.btPrint = new PinkieControls.ButtonXP();
            this.m_txtFind = new com.digitalwave.controls.SearchComboBox();
            this.btExit = new PinkieControls.ButtonXP();
            this.btClear = new PinkieControls.ButtonXP();
            this.btDel = new PinkieControls.ButtonXP();
            this.btSave = new PinkieControls.ButtonXP();
            this.btAdd = new PinkieControls.ButtonXP();
            this.m_btFind = new PinkieControls.ButtonXP();
            this.m_cmbFind = new System.Windows.Forms.ComboBox();
            this.m_cmbType2 = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.btChangeCat = new PinkieControls.ButtonXP();
            this.btEditDiscount = new PinkieControls.ButtonXP();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cboIsChildPrice = new System.Windows.Forms.ComboBox();
            this.label48 = new System.Windows.Forms.Label();
            this.txtOriPrice = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.cboItemScope = new System.Windows.Forms.ComboBox();
            this.label47 = new System.Windows.Forms.Label();
            this.btnRegular = new PinkieControls.ButtonXP();
            this.txtRegular = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.txtCityUnicode = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.cboSfcl = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.m_txtOrderCateType = new System.Windows.Forms.TextBox();
            this.m_lsvOrderType = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.m_lsvExeType = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.m_txtExeType = new System.Windows.Forms.TextBox();
            this.m_lsvExcType = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.m_txtCaseCal = new System.Windows.Forms.TextBox();
            this.m_txtOPInvoice = new System.Windows.Forms.TextBox();
            this.m_txtIPInvoice = new System.Windows.Forms.TextBox();
            this.m_txtIPCal = new System.Windows.Forms.TextBox();
            this.m_txtOPCal = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.m_cboKeepUse = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.cmbIsStop = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.m_cmbIsExpensive = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.m_cmbSelf = new System.Windows.Forms.ComboBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.m_txtDefaultFreq = new System.Windows.Forms.TextBox();
            this.lsvDefaultFreq = new System.Windows.Forms.ListView();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
            this.label39 = new System.Windows.Forms.Label();
            this.cboInpInsuranceType = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.txtCommName = new System.Windows.Forms.TextBox();
            this.lbePS = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.cmbCheckPart = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.m_cboApplyType = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.COBINSURANCETYPE = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.lbeIPPrice = new System.Windows.Forms.Label();
            this.lbePrice = new System.Windows.Forms.Label();
            this.txtProduceing = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.m_cmbIPUseUint = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtEnglishName = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.m_txtTradePrice = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.m_cmbOPUseUint = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.m_cmbIsInDocAdv = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.m_txtPacQ = new System.Windows.Forms.TextBox();
            this.m_cmbCoustomPrice = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.m_txtInsuranceID = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.m_txtchargeNO = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.m_txtSourName = new System.Windows.Forms.TextBox();
            this.m_txtSour = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.m_cboUsage = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.m_cboDosUnit = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.m_cboIPUnit = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_cboOPUnit = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.m_cboUnit = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_txtPrice = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_txtSpec = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_txtWB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_txtPY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtName = new System.Windows.Forms.TextBox();
            this.m_txtNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.m_txtDosage = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.bt_Refresh = new PinkieControls.ButtonXP();
            this.m_cmbType = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.buttonXP1);
            this.panel3.Controls.Add(this.btPrice);
            this.panel3.Controls.Add(this.btPrint);
            this.panel3.Controls.Add(this.m_txtFind);
            this.panel3.Controls.Add(this.btExit);
            this.panel3.Controls.Add(this.btClear);
            this.panel3.Controls.Add(this.btDel);
            this.panel3.Controls.Add(this.btSave);
            this.panel3.Controls.Add(this.btAdd);
            this.panel3.Controls.Add(this.m_btFind);
            this.panel3.Controls.Add(this.m_cmbFind);
            this.panel3.Controls.Add(this.m_cmbType2);
            this.panel3.Controls.Add(this.btChangeCat);
            this.panel3.Controls.Add(this.btEditDiscount);
            this.panel3.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel3.Location = new System.Drawing.Point(352, 661);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(684, 78);
            this.panel3.TabIndex = 2;
            // 
            // buttonXP1
            // 
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Font = new System.Drawing.Font("����", 10.5F);
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(481, 41);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(68, 32);
            this.buttonXP1.TabIndex = 13;
            this.buttonXP1.Text = "����(&U)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click_1);
            // 
            // btPrice
            // 
            this.btPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btPrice.DefaultScheme = true;
            this.btPrice.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btPrice.Font = new System.Drawing.Font("����", 10.5F);
            this.btPrice.Hint = "";
            this.btPrice.Location = new System.Drawing.Point(555, 6);
            this.btPrice.Name = "btPrice";
            this.btPrice.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btPrice.Size = new System.Drawing.Size(80, 32);
            this.btPrice.TabIndex = 12;
            this.btPrice.Text = "��ʷ�۸�";
            this.btPrice.Click += new System.EventHandler(this.btPrice_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btPrint.DefaultScheme = true;
            this.btPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btPrint.Hint = "";
            this.btPrint.Location = new System.Drawing.Point(327, 41);
            this.btPrint.Name = "btPrint";
            this.btPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btPrint.Size = new System.Drawing.Size(68, 32);
            this.btPrint.TabIndex = 9;
            this.btPrint.Text = "��ӡ";
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // m_txtFind
            // 
            this.m_txtFind.Location = new System.Drawing.Point(150, 9);
            this.m_txtFind.Name = "m_txtFind";
            this.m_txtFind.Size = new System.Drawing.Size(116, 22);
            this.m_txtFind.TabIndex = 1;
            this.m_txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtFind_KeyDown);
            // 
            // btExit
            // 
            this.btExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btExit.DefaultScheme = true;
            this.btExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btExit.Hint = "";
            this.btExit.Location = new System.Drawing.Point(555, 41);
            this.btExit.Name = "btExit";
            this.btExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btExit.Size = new System.Drawing.Size(80, 32);
            this.btExit.TabIndex = 7;
            this.btExit.Text = "�˳�(ESC)";
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btClear
            // 
            this.btClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btClear.DefaultScheme = true;
            this.btClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btClear.Hint = "";
            this.btClear.Location = new System.Drawing.Point(250, 41);
            this.btClear.Name = "btClear";
            this.btClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btClear.Size = new System.Drawing.Size(68, 32);
            this.btClear.TabIndex = 6;
            this.btClear.Text = "���(F5)";
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btDel
            // 
            this.btDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btDel.DefaultScheme = true;
            this.btDel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btDel.Enabled = false;
            this.btDel.Hint = "";
            this.btDel.Location = new System.Drawing.Point(173, 40);
            this.btDel.Name = "btDel";
            this.btDel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btDel.Size = new System.Drawing.Size(68, 32);
            this.btDel.TabIndex = 5;
            this.btDel.Text = "ɾ��(F4)";
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // btSave
            // 
            this.btSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btSave.DefaultScheme = true;
            this.btSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btSave.Hint = "";
            this.btSave.Location = new System.Drawing.Point(96, 40);
            this.btSave.Name = "btSave";
            this.btSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btSave.Size = new System.Drawing.Size(68, 32);
            this.btSave.TabIndex = 4;
            this.btSave.Text = "����(F3)";
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btAdd
            // 
            this.btAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btAdd.DefaultScheme = true;
            this.btAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btAdd.Hint = "";
            this.btAdd.Location = new System.Drawing.Point(19, 39);
            this.btAdd.Name = "btAdd";
            this.btAdd.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btAdd.Size = new System.Drawing.Size(68, 32);
            this.btAdd.TabIndex = 3;
            this.btAdd.Text = "����(F2)";
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // m_btFind
            // 
            this.m_btFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_btFind.DefaultScheme = true;
            this.m_btFind.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btFind.Hint = "";
            this.m_btFind.Location = new System.Drawing.Point(278, 6);
            this.m_btFind.Name = "m_btFind";
            this.m_btFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btFind.Size = new System.Drawing.Size(76, 32);
            this.m_btFind.TabIndex = 2;
            this.m_btFind.Text = "����(F7)";
            this.m_btFind.Click += new System.EventHandler(this.m_btFind_Click);
            // 
            // m_cmbFind
            // 
            this.m_cmbFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_cmbFind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbFind.Font = new System.Drawing.Font("����", 10.5F);
            this.m_cmbFind.Items.AddRange(new object[] {
            "����ĿID����",
            "����Ŀ���Ʋ���",
            "����Ŀ�������",
            "��ƴ���������",
            "����ʼ������",
            "��Ӣ�����Ʋ���"});
            this.m_cmbFind.Location = new System.Drawing.Point(20, 8);
            this.m_cmbFind.Name = "m_cmbFind";
            this.m_cmbFind.Size = new System.Drawing.Size(124, 22);
            this.m_cmbFind.TabIndex = 0;
            this.m_cmbFind.SelectedIndexChanged += new System.EventHandler(this.m_cmbFind_SelectedIndexChanged);
            // 
            // m_cmbType2
            // 
            this.m_cmbType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbType2.Location = new System.Drawing.Point(372, 10);
            this.m_cmbType2.Name = "m_cmbType2";
            this.m_cmbType2.Size = new System.Drawing.Size(88, 22);
            this.m_cmbType2.TabIndex = 11;
            // 
            // btChangeCat
            // 
            this.btChangeCat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btChangeCat.DefaultScheme = true;
            this.btChangeCat.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btChangeCat.Font = new System.Drawing.Font("����", 10.5F);
            this.btChangeCat.Hint = "";
            this.btChangeCat.Location = new System.Drawing.Point(469, 5);
            this.btChangeCat.Name = "btChangeCat";
            this.btChangeCat.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btChangeCat.Size = new System.Drawing.Size(80, 32);
            this.btChangeCat.TabIndex = 10;
            this.btChangeCat.Text = "���ķ���";
            this.btChangeCat.Click += new System.EventHandler(this.btChangeCat_Click);
            // 
            // btEditDiscount
            // 
            this.btEditDiscount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btEditDiscount.DefaultScheme = true;
            this.btEditDiscount.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btEditDiscount.Font = new System.Drawing.Font("����", 10.5F);
            this.btEditDiscount.Hint = "";
            this.btEditDiscount.Location = new System.Drawing.Point(404, 41);
            this.btEditDiscount.Name = "btEditDiscount";
            this.btEditDiscount.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btEditDiscount.Size = new System.Drawing.Size(68, 32);
            this.btEditDiscount.TabIndex = 8;
            this.btEditDiscount.Text = "����(&E)";
            this.btEditDiscount.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Location = new System.Drawing.Point(352, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(684, 656);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.cboIsChildPrice);
            this.panel4.Controls.Add(this.label48);
            this.panel4.Controls.Add(this.txtOriPrice);
            this.panel4.Controls.Add(this.label44);
            this.panel4.Controls.Add(this.cboItemScope);
            this.panel4.Controls.Add(this.label47);
            this.panel4.Controls.Add(this.btnRegular);
            this.panel4.Controls.Add(this.txtRegular);
            this.panel4.Controls.Add(this.label46);
            this.panel4.Controls.Add(this.txtCityUnicode);
            this.panel4.Controls.Add(this.label45);
            this.panel4.Controls.Add(this.cboSfcl);
            this.panel4.Controls.Add(this.label43);
            this.panel4.Controls.Add(this.m_txtOrderCateType);
            this.panel4.Controls.Add(this.m_lsvOrderType);
            this.panel4.Controls.Add(this.m_lsvExeType);
            this.panel4.Controls.Add(this.m_txtExeType);
            this.panel4.Controls.Add(this.m_lsvExcType);
            this.panel4.Controls.Add(this.m_txtCaseCal);
            this.panel4.Controls.Add(this.m_txtOPInvoice);
            this.panel4.Controls.Add(this.m_txtIPInvoice);
            this.panel4.Controls.Add(this.m_txtIPCal);
            this.panel4.Controls.Add(this.m_txtOPCal);
            this.panel4.Controls.Add(this.label42);
            this.panel4.Controls.Add(this.m_cboKeepUse);
            this.panel4.Controls.Add(this.cmbIsStop);
            this.panel4.Controls.Add(this.label29);
            this.panel4.Controls.Add(this.m_cmbIsExpensive);
            this.panel4.Controls.Add(this.label21);
            this.panel4.Controls.Add(this.m_cmbSelf);
            this.panel4.Controls.Add(this.label40);
            this.panel4.Controls.Add(this.label41);
            this.panel4.Controls.Add(this.m_txtDefaultFreq);
            this.panel4.Controls.Add(this.lsvDefaultFreq);
            this.panel4.Controls.Add(this.label39);
            this.panel4.Controls.Add(this.cboInpInsuranceType);
            this.panel4.Controls.Add(this.label38);
            this.panel4.Controls.Add(this.label37);
            this.panel4.Controls.Add(this.label36);
            this.panel4.Controls.Add(this.txtCommName);
            this.panel4.Controls.Add(this.lbePS);
            this.panel4.Controls.Add(this.label34);
            this.panel4.Controls.Add(this.cmbCheckPart);
            this.panel4.Controls.Add(this.m_cboApplyType);
            this.panel4.Controls.Add(this.label32);
            this.panel4.Controls.Add(this.label33);
            this.panel4.Controls.Add(this.label35);
            this.panel4.Controls.Add(this.COBINSURANCETYPE);
            this.panel4.Controls.Add(this.lbeIPPrice);
            this.panel4.Controls.Add(this.lbePrice);
            this.panel4.Controls.Add(this.txtProduceing);
            this.panel4.Controls.Add(this.label31);
            this.panel4.Controls.Add(this.m_cmbIPUseUint);
            this.panel4.Controls.Add(this.label30);
            this.panel4.Controls.Add(this.txtEnglishName);
            this.panel4.Controls.Add(this.label27);
            this.panel4.Controls.Add(this.m_txtTradePrice);
            this.panel4.Controls.Add(this.label28);
            this.panel4.Controls.Add(this.m_cmbOPUseUint);
            this.panel4.Controls.Add(this.label26);
            this.panel4.Controls.Add(this.m_cmbIsInDocAdv);
            this.panel4.Controls.Add(this.label25);
            this.panel4.Controls.Add(this.m_txtPacQ);
            this.panel4.Controls.Add(this.m_cmbCoustomPrice);
            this.panel4.Controls.Add(this.label24);
            this.panel4.Controls.Add(this.label23);
            this.panel4.Controls.Add(this.m_txtInsuranceID);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.m_txtchargeNO);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.m_txtSourName);
            this.panel4.Controls.Add(this.m_txtSour);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.m_cboUsage);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.m_cboDosUnit);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.m_cboIPUnit);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.m_cboOPUnit);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.m_cboUnit);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.m_txtPrice);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.m_txtSpec);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.m_txtWB);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.m_txtPY);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.m_txtName);
            this.panel4.Controls.Add(this.m_txtNo);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.m_txtDosage);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(672, 627);
            this.panel4.TabIndex = 0;
            // 
            // cboIsChildPrice
            // 
            this.cboIsChildPrice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIsChildPrice.Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboIsChildPrice.ForeColor = System.Drawing.Color.BlueViolet;
            this.cboIsChildPrice.Items.AddRange(new object[] {
            "��",
            "��"});
            this.cboIsChildPrice.Location = new System.Drawing.Point(303, 575);
            this.cboIsChildPrice.Name = "cboIsChildPrice";
            this.cboIsChildPrice.Size = new System.Drawing.Size(48, 23);
            this.cboIsChildPrice.TabIndex = 891;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label48.ForeColor = System.Drawing.Color.BlueViolet;
            this.label48.Location = new System.Drawing.Point(166, 577);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(135, 14);
            this.label48.TabIndex = 890;
            this.label48.Text = "�Ƿ��ͯ������Ŀ:";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOriPrice
            // 
            this.txtOriPrice.CausesValidation = false;
            //this.txtOriPrice.EnableAutoValidation = false;
            //this.txtOriPrice.EnableEnterKeyValidate = false;
            //this.txtOriPrice.EnableEscapeKeyUndo = true;
            //this.txtOriPrice.EnableLastValidValue = true;
            //this.txtOriPrice.ErrorProvider = null;
            //this.txtOriPrice.ErrorProviderMessage = "Invalid value";
            this.txtOriPrice.Font = new System.Drawing.Font("����", 11F);
            //this.txtOriPrice.ForceFormatText = true;
            this.txtOriPrice.Location = new System.Drawing.Point(144, 492);
            this.txtOriPrice.MaxLength = 10;
            this.txtOriPrice.Name = "txtOriPrice";
            //this.txtOriPrice.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator;
            this.txtOriPrice.Size = new System.Drawing.Size(152, 24);
            this.txtOriPrice.TabIndex = 18;
            this.txtOriPrice.Text = "0";
            this.txtOriPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("����", 10.5F);
            this.label44.Location = new System.Drawing.Point(24, 496);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(112, 14);
            this.label44.TabIndex = 364;
            this.label44.Text = "��Ŀԭʼ���ۼ�:";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboItemScope
            // 
            this.cboItemScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboItemScope.Font = new System.Drawing.Font("����", 11F);
            this.cboItemScope.ForeColor = System.Drawing.Color.Blue;
            this.cboItemScope.Items.AddRange(new object[] {
            "����",
            "����",
            "��ͯ"});
            this.cboItemScope.Location = new System.Drawing.Point(152, 602);
            this.cboItemScope.Name = "cboItemScope";
            this.cboItemScope.Size = new System.Drawing.Size(152, 23);
            this.cboItemScope.TabIndex = 888;
            this.cboItemScope.Visible = false;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("����", 10.5F);
            this.label47.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label47.Location = new System.Drawing.Point(52, 606);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(98, 14);
            this.label47.TabIndex = 363;
            this.label47.Text = "��Ŀʹ�÷�Χ:";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label47.Visible = false;
            // 
            // btnRegular
            // 
            this.btnRegular.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnRegular.DefaultScheme = true;
            this.btnRegular.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRegular.Font = new System.Drawing.Font("����", 9F);
            this.btnRegular.Hint = "";
            this.btnRegular.Location = new System.Drawing.Point(531, 519);
            this.btnRegular.Name = "btnRegular";
            this.btnRegular.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnRegular.Size = new System.Drawing.Size(61, 26);
            this.btnRegular.TabIndex = 362;
            this.btnRegular.Text = "�����ֵ�";
            this.btnRegular.Click += new System.EventHandler(this.btnRegular_Click);
            // 
            // txtRegular
            // 
            this.txtRegular.Font = new System.Drawing.Font("����", 11F);
            this.txtRegular.Location = new System.Drawing.Point(144, 520);
            this.txtRegular.Name = "txtRegular";
            this.txtRegular.ReadOnly = true;
            this.txtRegular.Size = new System.Drawing.Size(384, 24);
            this.txtRegular.TabIndex = 39;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("����", 10.5F);
            this.label46.Location = new System.Drawing.Point(23, 521);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(126, 14);
            this.label46.TabIndex = 360;
            this.label46.Text = "�����Զ��˶Թ���:";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCityUnicode
            // 
            //this.txtCityUnicode.EnableAutoValidation = false;
            //this.txtCityUnicode.EnableEnterKeyValidate = false;
            //this.txtCityUnicode.EnableEscapeKeyUndo = true;
            //this.txtCityUnicode.EnableLastValidValue = true;
            //this.txtCityUnicode.ErrorProvider = null;
            //this.txtCityUnicode.ErrorProviderMessage = "Invalid value";
            this.txtCityUnicode.Font = new System.Drawing.Font("����", 11F);
            //this.txtCityUnicode.ForceFormatText = true;
            this.txtCityUnicode.Location = new System.Drawing.Point(440, 493);
            this.txtCityUnicode.MaxLength = 20;
            this.txtCityUnicode.Name = "txtCityUnicode";
            this.txtCityUnicode.Size = new System.Drawing.Size(152, 24);
            this.txtCityUnicode.TabIndex = 38;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("����", 10.5F);
            this.label45.Location = new System.Drawing.Point(323, 497);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(84, 14);
            this.label45.TabIndex = 357;
            this.label45.Text = "ȫ��ͳһ��:";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboSfcl
            // 
            this.cboSfcl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSfcl.Font = new System.Drawing.Font("����", 11F);
            this.cboSfcl.Items.AddRange(new object[] {
            "",
            "��",
            "��"});
            this.cboSfcl.Location = new System.Drawing.Point(97, 575);
            this.cboSfcl.Name = "cboSfcl";
            this.cboSfcl.Size = new System.Drawing.Size(48, 23);
            this.cboSfcl.TabIndex = 45;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("����", 10.5F);
            this.label43.Location = new System.Drawing.Point(23, 577);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(70, 14);
            this.label43.TabIndex = 354;
            this.label43.Text = "�շѲ���:";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtOrderCateType
            // 
            this.m_txtOrderCateType.Font = new System.Drawing.Font("����", 11F);
            this.m_txtOrderCateType.Location = new System.Drawing.Point(440, 465);
            this.m_txtOrderCateType.Name = "m_txtOrderCateType";
            this.m_txtOrderCateType.Size = new System.Drawing.Size(152, 24);
            this.m_txtOrderCateType.TabIndex = 37;
            this.m_txtOrderCateType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtOrderCateType_KeyDown);
            // 
            // m_lsvOrderType
            // 
            this.m_lsvOrderType.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7});
            this.m_lsvOrderType.Font = new System.Drawing.Font("����", 10.5F);
            this.m_lsvOrderType.FullRowSelect = true;
            this.m_lsvOrderType.GridLines = true;
            this.m_lsvOrderType.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lsvOrderType.Location = new System.Drawing.Point(144, 331);
            this.m_lsvOrderType.MultiSelect = false;
            this.m_lsvOrderType.Name = "m_lsvOrderType";
            this.m_lsvOrderType.Size = new System.Drawing.Size(154, 128);
            this.m_lsvOrderType.TabIndex = 352;
            this.m_lsvOrderType.UseCompatibleStateImageBehavior = false;
            this.m_lsvOrderType.View = System.Windows.Forms.View.Details;
            this.m_lsvOrderType.Visible = false;
            this.m_lsvOrderType.Leave += new System.EventHandler(this.m_lsvOrderType_Leave);
            this.m_lsvOrderType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvOrderType_KeyDown);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "���ID";
            this.columnHeader6.Width = 0;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "ҽ������";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 150;
            // 
            // m_lsvExeType
            // 
            this.m_lsvExeType.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.m_lsvExeType.Font = new System.Drawing.Font("����", 10.5F);
            this.m_lsvExeType.FullRowSelect = true;
            this.m_lsvExeType.GridLines = true;
            this.m_lsvExeType.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lsvExeType.Location = new System.Drawing.Point(144, 331);
            this.m_lsvExeType.MultiSelect = false;
            this.m_lsvExeType.Name = "m_lsvExeType";
            this.m_lsvExeType.Size = new System.Drawing.Size(154, 128);
            this.m_lsvExeType.TabIndex = 351;
            this.m_lsvExeType.UseCompatibleStateImageBehavior = false;
            this.m_lsvExeType.View = System.Windows.Forms.View.Details;
            this.m_lsvExeType.Visible = false;
            this.m_lsvExeType.Leave += new System.EventHandler(this.m_lsvExeType_Leave);
            this.m_lsvExeType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvExeType_KeyDown);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "���ID";
            this.columnHeader4.Width = 0;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ִ�����";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 150;
            // 
            // m_txtExeType
            // 
            this.m_txtExeType.Font = new System.Drawing.Font("����", 11F);
            this.m_txtExeType.Location = new System.Drawing.Point(440, 438);
            this.m_txtExeType.Name = "m_txtExeType";
            this.m_txtExeType.Size = new System.Drawing.Size(152, 24);
            this.m_txtExeType.TabIndex = 36;
            this.m_txtExeType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtExeType_KeyDown);
            // 
            // m_lsvExcType
            // 
            this.m_lsvExcType.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.m_lsvExcType.Font = new System.Drawing.Font("����", 10.5F);
            this.m_lsvExcType.FullRowSelect = true;
            this.m_lsvExcType.GridLines = true;
            this.m_lsvExcType.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lsvExcType.Location = new System.Drawing.Point(144, 333);
            this.m_lsvExcType.MultiSelect = false;
            this.m_lsvExcType.Name = "m_lsvExcType";
            this.m_lsvExcType.Size = new System.Drawing.Size(189, 128);
            this.m_lsvExcType.TabIndex = 349;
            this.m_lsvExcType.UseCompatibleStateImageBehavior = false;
            this.m_lsvExcType.View = System.Windows.Forms.View.Details;
            this.m_lsvExcType.Visible = false;
            this.m_lsvExcType.Leave += new System.EventHandler(this.m_lsvExcType_Leave);
            this.m_lsvExcType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvExcType_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "���ID";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "������";
            this.columnHeader2.Width = 61;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "�������";
            this.columnHeader3.Width = 120;
            // 
            // m_txtCaseCal
            // 
            this.m_txtCaseCal.Font = new System.Drawing.Font("����", 11F);
            this.m_txtCaseCal.Location = new System.Drawing.Point(440, 288);
            this.m_txtCaseCal.Name = "m_txtCaseCal";
            this.m_txtCaseCal.Size = new System.Drawing.Size(152, 24);
            this.m_txtCaseCal.TabIndex = 30;
            this.m_txtCaseCal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtCaseCal_KeyDown);
            // 
            // m_txtOPInvoice
            // 
            this.m_txtOPInvoice.Font = new System.Drawing.Font("����", 11F);
            this.m_txtOPInvoice.Location = new System.Drawing.Point(440, 207);
            this.m_txtOPInvoice.Name = "m_txtOPInvoice";
            this.m_txtOPInvoice.Size = new System.Drawing.Size(152, 24);
            this.m_txtOPInvoice.TabIndex = 27;
            this.m_txtOPInvoice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtOPInvoice_KeyDown);
            // 
            // m_txtIPInvoice
            // 
            this.m_txtIPInvoice.Font = new System.Drawing.Font("����", 11F);
            this.m_txtIPInvoice.Location = new System.Drawing.Point(440, 261);
            this.m_txtIPInvoice.Name = "m_txtIPInvoice";
            this.m_txtIPInvoice.Size = new System.Drawing.Size(152, 24);
            this.m_txtIPInvoice.TabIndex = 29;
            this.m_txtIPInvoice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtIPInvoice_KeyDown);
            // 
            // m_txtIPCal
            // 
            this.m_txtIPCal.Font = new System.Drawing.Font("����", 11F);
            this.m_txtIPCal.Location = new System.Drawing.Point(440, 234);
            this.m_txtIPCal.Name = "m_txtIPCal";
            this.m_txtIPCal.Size = new System.Drawing.Size(152, 24);
            this.m_txtIPCal.TabIndex = 28;
            this.m_txtIPCal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtIPCal_KeyDown);
            // 
            // m_txtOPCal
            // 
            this.m_txtOPCal.Font = new System.Drawing.Font("����", 11F);
            this.m_txtOPCal.Location = new System.Drawing.Point(440, 180);
            this.m_txtOPCal.Name = "m_txtOPCal";
            this.m_txtOPCal.Size = new System.Drawing.Size(152, 24);
            this.m_txtOPCal.TabIndex = 26;
            this.m_txtOPCal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtOPCal_KeyDown);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("����", 10.5F);
            this.label42.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label42.Location = new System.Drawing.Point(323, 552);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(70, 14);
            this.label42.TabIndex = 348;
            this.label42.Text = "���þ���:";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cboKeepUse
            // 
            this.m_cboKeepUse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboKeepUse.Font = new System.Drawing.Font("����", 11F);
            this.m_cboKeepUse.Items.AddRange(new object[] {
            "����",
            "Сʱ"});
            this.m_cboKeepUse.Location = new System.Drawing.Point(394, 548);
            this.m_cboKeepUse.Name = "m_cboKeepUse";
            this.m_cboKeepUse.Size = new System.Drawing.Size(68, 23);
            this.m_cboKeepUse.TabIndex = 43;
            this.m_cboKeepUse.SelectedIndexChanged += new System.EventHandler(this.m_cboKeepUse_SelectedIndexChanged);
            // 
            // cmbIsStop
            // 
            this.cmbIsStop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIsStop.Font = new System.Drawing.Font("����", 11F);
            this.cmbIsStop.Items.AddRange(new object[] {
            "��(����)",
            "��(ͣ��)"});
            this.cmbIsStop.Location = new System.Drawing.Point(212, 548);
            this.cmbIsStop.Name = "cmbIsStop";
            this.cmbIsStop.Size = new System.Drawing.Size(84, 23);
            this.cmbIsStop.TabIndex = 42;
            this.cmbIsStop.SelectedIndexChanged += new System.EventHandler(this.cmbIsStop_SelectedIndexChanged);
            this.cmbIsStop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbIsStop_KeyDown);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("����", 10.5F);
            this.label29.Location = new System.Drawing.Point(166, 552);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(42, 14);
            this.label29.TabIndex = 147;
            this.label29.Text = "ͣ��:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cmbIsExpensive
            // 
            this.m_cmbIsExpensive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbIsExpensive.Font = new System.Drawing.Font("����", 11F);
            this.m_cmbIsExpensive.Items.AddRange(new object[] {
            "��",
            "��"});
            this.m_cmbIsExpensive.Location = new System.Drawing.Point(97, 548);
            this.m_cmbIsExpensive.Name = "m_cmbIsExpensive";
            this.m_cmbIsExpensive.Size = new System.Drawing.Size(48, 23);
            this.m_cmbIsExpensive.TabIndex = 41;
            this.m_cmbIsExpensive.SelectedIndexChanged += new System.EventHandler(this.m_cmbIsExpensive_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("����", 10.5F);
            this.label21.Location = new System.Drawing.Point(23, 552);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(70, 14);
            this.label21.TabIndex = 133;
            this.label21.Text = "����ҩƷ:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cmbSelf
            // 
            this.m_cmbSelf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbSelf.Font = new System.Drawing.Font("����", 11F);
            this.m_cmbSelf.Items.AddRange(new object[] {
            "��",
            "��"});
            this.m_cmbSelf.Location = new System.Drawing.Point(544, 548);
            this.m_cmbSelf.Name = "m_cmbSelf";
            this.m_cmbSelf.Size = new System.Drawing.Size(48, 23);
            this.m_cmbSelf.TabIndex = 44;
            this.m_cmbSelf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cmbSelf_KeyDown);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("����", 10.5F);
            this.label40.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label40.Location = new System.Drawing.Point(323, 470);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(70, 14);
            this.label40.TabIndex = 344;
            this.label40.Text = "ҽ������:";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label40.Click += new System.EventHandler(this.label40_Click);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("����", 10.5F);
            this.label41.Location = new System.Drawing.Point(471, 552);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(70, 14);
            this.label41.TabIndex = 346;
            this.label41.Text = "�Է�ҩƷ:";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtDefaultFreq
            // 
            this.m_txtDefaultFreq.Font = new System.Drawing.Font("����", 11F);
            this.m_txtDefaultFreq.Location = new System.Drawing.Point(144, 464);
            this.m_txtDefaultFreq.Name = "m_txtDefaultFreq";
            this.m_txtDefaultFreq.Size = new System.Drawing.Size(152, 24);
            this.m_txtDefaultFreq.TabIndex = 17;
            this.m_txtDefaultFreq.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtDefaultFreq_KeyDown);
            // 
            // lsvDefaultFreq
            // 
            this.lsvDefaultFreq.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16});
            this.lsvDefaultFreq.Font = new System.Drawing.Font("����", 10.5F);
            this.lsvDefaultFreq.FullRowSelect = true;
            this.lsvDefaultFreq.GridLines = true;
            this.lsvDefaultFreq.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvDefaultFreq.Location = new System.Drawing.Point(144, 331);
            this.lsvDefaultFreq.MultiSelect = false;
            this.lsvDefaultFreq.Name = "lsvDefaultFreq";
            this.lsvDefaultFreq.Size = new System.Drawing.Size(189, 128);
            this.lsvDefaultFreq.TabIndex = 3;
            this.lsvDefaultFreq.UseCompatibleStateImageBehavior = false;
            this.lsvDefaultFreq.View = System.Windows.Forms.View.Details;
            this.lsvDefaultFreq.Visible = false;
            this.lsvDefaultFreq.Leave += new System.EventHandler(this.lsvDefaultFreq_Leave);
            this.lsvDefaultFreq.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsvDefaultFreq_KeyDown);
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Ƶ��ID";
            this.columnHeader14.Width = 0;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "������";
            this.columnHeader15.Width = 61;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Ƶ������";
            this.columnHeader16.Width = 120;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("����", 10.5F);
            this.label39.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label39.Location = new System.Drawing.Point(322, 418);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(98, 14);
            this.label39.TabIndex = 341;
            this.label39.Text = "סԺҽ������:";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboInpInsuranceType
            // 
            this.cboInpInsuranceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInpInsuranceType.Font = new System.Drawing.Font("����", 11F);
            this.cboInpInsuranceType.Location = new System.Drawing.Point(440, 413);
            this.cboInpInsuranceType.Name = "cboInpInsuranceType";
            this.cboInpInsuranceType.Size = new System.Drawing.Size(152, 23);
            this.cboInpInsuranceType.TabIndex = 35;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("����", 10.5F);
            this.label38.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label38.Location = new System.Drawing.Point(24, 467);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(70, 14);
            this.label38.TabIndex = 339;
            this.label38.Text = "Ĭ��Ƶ��:";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("����", 10.5F);
            this.label37.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label37.Location = new System.Drawing.Point(324, 444);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(70, 14);
            this.label37.TabIndex = 338;
            this.label37.Text = "ִ�����:";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("����", 10.5F);
            this.label36.Location = new System.Drawing.Point(24, 119);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(70, 14);
            this.label36.TabIndex = 336;
            this.label36.Text = "ͨ������:";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCommName
            // 
            //this.txtCommName.EnableAutoValidation = false;
            //this.txtCommName.EnableEnterKeyValidate = false;
            //this.txtCommName.EnableEscapeKeyUndo = true;
            //this.txtCommName.EnableLastValidValue = true;
            //this.txtCommName.ErrorProvider = null;
            //this.txtCommName.ErrorProviderMessage = "Invalid value";
            this.txtCommName.Font = new System.Drawing.Font("����", 11F);
            //this.txtCommName.ForceFormatText = true;
            this.txtCommName.Location = new System.Drawing.Point(144, 117);
            this.txtCommName.MaxLength = 40;
            this.txtCommName.Name = "txtCommName";
            this.txtCommName.Size = new System.Drawing.Size(152, 24);
            this.txtCommName.TabIndex = 4;
            this.txtCommName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommName_KeyDown);
            // 
            // lbePS
            // 
            this.lbePS.AutoSize = true;
            this.lbePS.Font = new System.Drawing.Font("����", 10.5F);
            this.lbePS.ForeColor = System.Drawing.Color.Red;
            this.lbePS.Location = new System.Drawing.Point(380, 7);
            this.lbePS.Name = "lbePS";
            this.lbePS.Size = new System.Drawing.Size(0, 14);
            this.lbePS.TabIndex = 334;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("����", 10.5F);
            this.label34.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label34.Location = new System.Drawing.Point(324, 341);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(70, 14);
            this.label34.TabIndex = 333;
            this.label34.Text = "��鲿λ:";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCheckPart
            // 
            this.cmbCheckPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheckPart.Font = new System.Drawing.Font("����", 11F);
            this.cmbCheckPart.Location = new System.Drawing.Point(440, 338);
            this.cmbCheckPart.Name = "cmbCheckPart";
            this.cmbCheckPart.Size = new System.Drawing.Size(152, 23);
            this.cmbCheckPart.TabIndex = 32;
            // 
            // m_cboApplyType
            // 
            this.m_cboApplyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboApplyType.Font = new System.Drawing.Font("����", 11F);
            this.m_cboApplyType.Location = new System.Drawing.Point(440, 315);
            this.m_cboApplyType.Name = "m_cboApplyType";
            this.m_cboApplyType.Size = new System.Drawing.Size(152, 23);
            this.m_cboApplyType.TabIndex = 31;
            this.m_cboApplyType.SelectedIndexChanged += new System.EventHandler(this.m_cboApplyType_SelectedIndexChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("����", 10.5F);
            this.label32.Location = new System.Drawing.Point(324, 318);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(84, 14);
            this.label32.TabIndex = 331;
            this.label32.Text = "���뵥���:";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("����", 10.5F);
            this.label33.Location = new System.Drawing.Point(324, 292);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(98, 14);
            this.label33.TabIndex = 330;
            this.label33.Text = "�����������:";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("����", 10.5F);
            this.label35.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label35.Location = new System.Drawing.Point(322, 392);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(98, 14);
            this.label35.TabIndex = 327;
            this.label35.Text = "����ҽ������:";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // COBINSURANCETYPE
            // 
            this.COBINSURANCETYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COBINSURANCETYPE.Font = new System.Drawing.Font("����", 11F);
            this.COBINSURANCETYPE.Location = new System.Drawing.Point(440, 388);
            this.COBINSURANCETYPE.Name = "COBINSURANCETYPE";
            this.COBINSURANCETYPE.Size = new System.Drawing.Size(152, 23);
            this.COBINSURANCETYPE.TabIndex = 34;
            this.COBINSURANCETYPE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.COBINSURANCETYPE_KeyDown);
            // 
            // lbeIPPrice
            // 
            this.lbeIPPrice.AutoSize = true;
            this.lbeIPPrice.Font = new System.Drawing.Font("����", 10F);
            this.lbeIPPrice.Location = new System.Drawing.Point(598, 161);
            this.lbeIPPrice.Name = "lbeIPPrice";
            this.lbeIPPrice.Size = new System.Drawing.Size(49, 14);
            this.lbeIPPrice.TabIndex = 153;
            this.lbeIPPrice.Text = "      ";
            // 
            // lbePrice
            // 
            this.lbePrice.AutoSize = true;
            this.lbePrice.Font = new System.Drawing.Font("����", 10F);
            this.lbePrice.Location = new System.Drawing.Point(598, 135);
            this.lbePrice.Name = "lbePrice";
            this.lbePrice.Size = new System.Drawing.Size(42, 14);
            this.lbePrice.TabIndex = 152;
            this.lbePrice.Text = "     ";
            // 
            // txtProduceing
            // 
            //this.txtProduceing.EnableAutoValidation = false;
            //this.txtProduceing.EnableEnterKeyValidate = false;
            //this.txtProduceing.EnableEscapeKeyUndo = true;
            //this.txtProduceing.EnableLastValidValue = true;
            //this.txtProduceing.ErrorProvider = null;
            //this.txtProduceing.ErrorProviderMessage = "Invalid value";
            this.txtProduceing.Font = new System.Drawing.Font("����", 11F);
            //this.txtProduceing.ForceFormatText = true;
            this.txtProduceing.Location = new System.Drawing.Point(144, 225);
            this.txtProduceing.MaxLength = 25;
            this.txtProduceing.Name = "txtProduceing";
            this.txtProduceing.Size = new System.Drawing.Size(152, 24);
            this.txtProduceing.TabIndex = 8;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("����", 10.5F);
            this.label31.Location = new System.Drawing.Point(24, 228);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(42, 14);
            this.label31.TabIndex = 151;
            this.label31.Text = "����:";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cmbIPUseUint
            // 
            this.m_cmbIPUseUint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbIPUseUint.Font = new System.Drawing.Font("����", 11F);
            this.m_cmbIPUseUint.Items.AddRange(new object[] {
            "������λ",
            "��С��λ"});
            this.m_cmbIPUseUint.Location = new System.Drawing.Point(440, 155);
            this.m_cmbIPUseUint.Name = "m_cmbIPUseUint";
            this.m_cmbIPUseUint.Size = new System.Drawing.Size(152, 23);
            this.m_cmbIPUseUint.TabIndex = 25;
            this.m_cmbIPUseUint.SelectedIndexChanged += new System.EventHandler(this.m_cmbIPUseUint_SelectedIndexChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("����", 10.5F);
            this.label30.Location = new System.Drawing.Point(324, 157);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(98, 14);
            this.label30.TabIndex = 149;
            this.label30.Text = "סԺ�շѵ�λ:";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEnglishName
            // 
            //this.txtEnglishName.EnableAutoValidation = false;
            //this.txtEnglishName.EnableEnterKeyValidate = false;
            //this.txtEnglishName.EnableEscapeKeyUndo = true;
            //this.txtEnglishName.EnableLastValidValue = true;
            //this.txtEnglishName.ErrorProvider = null;
            //this.txtEnglishName.ErrorProviderMessage = "Invalid value";
            this.txtEnglishName.Font = new System.Drawing.Font("����", 11F);
            //this.txtEnglishName.ForceFormatText = true;
            this.txtEnglishName.Location = new System.Drawing.Point(144, 90);
            this.txtEnglishName.MaxLength = 40;
            this.txtEnglishName.Name = "txtEnglishName";
            this.txtEnglishName.Size = new System.Drawing.Size(152, 24);
            this.txtEnglishName.TabIndex = 3;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("����", 10.5F);
            this.label27.Location = new System.Drawing.Point(24, 90);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(56, 14);
            this.label27.TabIndex = 145;
            this.label27.Text = "Ӣ����:";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtTradePrice
            // 
            this.m_txtTradePrice.CausesValidation = false;
            //this.m_txtTradePrice.EnableAutoValidation = false;
            //this.m_txtTradePrice.EnableEnterKeyValidate = false;
            //this.m_txtTradePrice.EnableEscapeKeyUndo = true;
            //this.m_txtTradePrice.EnableLastValidValue = true;
            //this.m_txtTradePrice.ErrorProvider = null;
            //this.m_txtTradePrice.ErrorProviderMessage = "Invalid value";
            this.m_txtTradePrice.Font = new System.Drawing.Font("����", 11F);
            //this.m_txtTradePrice.ForceFormatText = true;
            this.m_txtTradePrice.Location = new System.Drawing.Point(144, 358);
            this.m_txtTradePrice.MaxLength = 10;
            this.m_txtTradePrice.Name = "m_txtTradePrice";
            //this.m_txtTradePrice.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator;
            this.m_txtTradePrice.Size = new System.Drawing.Size(152, 24);
            this.m_txtTradePrice.TabIndex = 13;
            this.m_txtTradePrice.Text = "0";
            this.m_txtTradePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtTradePrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtTradePrice_KeyPress);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("����", 10.5F);
            this.label28.Location = new System.Drawing.Point(24, 360);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(56, 14);
            this.label28.TabIndex = 141;
            this.label28.Text = "������:";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cmbOPUseUint
            // 
            this.m_cmbOPUseUint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbOPUseUint.Font = new System.Drawing.Font("����", 11F);
            this.m_cmbOPUseUint.Items.AddRange(new object[] {
            "������λ",
            "��С��λ"});
            this.m_cmbOPUseUint.Location = new System.Drawing.Point(440, 130);
            this.m_cmbOPUseUint.Name = "m_cmbOPUseUint";
            this.m_cmbOPUseUint.Size = new System.Drawing.Size(152, 23);
            this.m_cmbOPUseUint.TabIndex = 24;
            this.m_cmbOPUseUint.SelectedIndexChanged += new System.EventHandler(this.m_cmbOPUseUint_SelectedIndexChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("����", 10.5F);
            this.label26.Location = new System.Drawing.Point(324, 132);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(98, 14);
            this.label26.TabIndex = 137;
            this.label26.Text = "�����շѵ�λ:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cmbIsInDocAdv
            // 
            this.m_cmbIsInDocAdv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbIsInDocAdv.Font = new System.Drawing.Font("����", 11F);
            this.m_cmbIsInDocAdv.Items.AddRange(new object[] {
            "��",
            "��"});
            this.m_cmbIsInDocAdv.Location = new System.Drawing.Point(440, 363);
            this.m_cmbIsInDocAdv.Name = "m_cmbIsInDocAdv";
            this.m_cmbIsInDocAdv.Size = new System.Drawing.Size(152, 23);
            this.m_cmbIsInDocAdv.TabIndex = 33;
            this.m_cmbIsInDocAdv.SelectedIndexChanged += new System.EventHandler(this.m_cmbIsInDocAdv_SelectedIndexChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("����", 10.5F);
            this.label25.Location = new System.Drawing.Point(324, 367);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(98, 14);
            this.label25.TabIndex = 135;
            this.label25.Text = "�Ƿ����ҽ��:";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtPacQ
            // 
            this.m_txtPacQ.CausesValidation = false;
            //this.m_txtPacQ.EnableAutoValidation = false;
            //this.m_txtPacQ.EnableEnterKeyValidate = false;
            //this.m_txtPacQ.EnableEscapeKeyUndo = true;
            //this.m_txtPacQ.EnableLastValidValue = true;
            //this.m_txtPacQ.ErrorProvider = null;
            //this.m_txtPacQ.ErrorProviderMessage = "Invalid value";
            this.m_txtPacQ.Font = new System.Drawing.Font("����", 11F);
            //this.m_txtPacQ.ForceFormatText = true;
            this.m_txtPacQ.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.m_txtPacQ.Location = new System.Drawing.Point(440, 103);
            this.m_txtPacQ.Name = "m_txtPacQ";
            //this.m_txtPacQ.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
            this.m_txtPacQ.Size = new System.Drawing.Size(152, 24);
            this.m_txtPacQ.TabIndex = 23;
            this.m_txtPacQ.Text = "0";
            this.m_txtPacQ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtPacQ.TextChanged += new System.EventHandler(this.m_txtPacQ_TextChanged);
            this.m_txtPacQ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtPacQ_KeyPress);
            // 
            // m_cmbCoustomPrice
            // 
            this.m_cmbCoustomPrice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbCoustomPrice.Font = new System.Drawing.Font("����", 11F);
            this.m_cmbCoustomPrice.Items.AddRange(new object[] {
            "��",
            "��"});
            this.m_cmbCoustomPrice.Location = new System.Drawing.Point(144, 385);
            this.m_cmbCoustomPrice.Name = "m_cmbCoustomPrice";
            this.m_cmbCoustomPrice.Size = new System.Drawing.Size(152, 23);
            this.m_cmbCoustomPrice.TabIndex = 14;
            this.m_cmbCoustomPrice.SelectedIndexChanged += new System.EventHandler(this.m_cmbCoustomPrice_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("����", 10.5F);
            this.label24.Location = new System.Drawing.Point(24, 386);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(84, 14);
            this.label24.TabIndex = 131;
            this.label24.Text = "�Զ���۸�:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("����", 10.5F);
            this.label23.Location = new System.Drawing.Point(324, 105);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(70, 14);
            this.label23.TabIndex = 130;
            this.label23.Text = "��װ����:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtInsuranceID
            // 
            this.m_txtInsuranceID.Font = new System.Drawing.Font("����", 11F);
            this.m_txtInsuranceID.Location = new System.Drawing.Point(144, 252);
            this.m_txtInsuranceID.Name = "m_txtInsuranceID";
            this.m_txtInsuranceID.Size = new System.Drawing.Size(152, 24);
            this.m_txtInsuranceID.TabIndex = 9;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("����", 10.5F);
            this.label22.Location = new System.Drawing.Point(24, 255);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(70, 14);
            this.label22.TabIndex = 129;
            this.label22.Text = "ҽ������:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtchargeNO
            // 
            //this.m_txtchargeNO.EnableAutoValidation = false;
            //this.m_txtchargeNO.EnableEnterKeyValidate = false;
            //this.m_txtchargeNO.EnableEscapeKeyUndo = true;
            //this.m_txtchargeNO.EnableLastValidValue = true;
            //this.m_txtchargeNO.ErrorProvider = null;
            //this.m_txtchargeNO.ErrorProviderMessage = "Invalid value";
            this.m_txtchargeNO.Font = new System.Drawing.Font("����", 11F);
            //this.m_txtchargeNO.ForceFormatText = true;
            this.m_txtchargeNO.Location = new System.Drawing.Point(144, 9);
            this.m_txtchargeNO.MaxLength = 20;
            this.m_txtchargeNO.Name = "m_txtchargeNO";
            this.m_txtchargeNO.Size = new System.Drawing.Size(152, 24);
            this.m_txtchargeNO.TabIndex = 0;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("����", 10.5F);
            this.label19.Location = new System.Drawing.Point(24, 11);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(98, 14);
            this.label19.TabIndex = 128;
            this.label19.Text = "�����շѱ���:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtSourName
            // 
            this.m_txtSourName.Font = new System.Drawing.Font("����", 11F);
            this.m_txtSourName.Location = new System.Drawing.Point(144, 410);
            this.m_txtSourName.Name = "m_txtSourName";
            this.m_txtSourName.ReadOnly = true;
            this.m_txtSourName.Size = new System.Drawing.Size(152, 24);
            this.m_txtSourName.TabIndex = 15;
            // 
            // m_txtSour
            // 
            this.m_txtSour.Font = new System.Drawing.Font("����", 11F);
            this.m_txtSour.Location = new System.Drawing.Point(144, 437);
            this.m_txtSour.Name = "m_txtSour";
            this.m_txtSour.ReadOnly = true;
            this.m_txtSour.Size = new System.Drawing.Size(152, 24);
            this.m_txtSour.TabIndex = 16;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("����", 10.5F);
            this.label18.Location = new System.Drawing.Point(24, 413);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(56, 14);
            this.label18.TabIndex = 127;
            this.label18.Text = "Դ����:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("����", 10.5F);
            this.label17.Location = new System.Drawing.Point(24, 440);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(84, 14);
            this.label17.TabIndex = 126;
            this.label17.Text = "Դ��������:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cboUsage
            // 
            this.m_cboUsage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboUsage.Font = new System.Drawing.Font("����", 11F);
            this.m_cboUsage.Location = new System.Drawing.Point(440, 3);
            this.m_cboUsage.Name = "m_cboUsage";
            this.m_cboUsage.Size = new System.Drawing.Size(152, 23);
            this.m_cboUsage.TabIndex = 19;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("����", 10.5F);
            this.label16.Location = new System.Drawing.Point(324, 6);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(42, 14);
            this.label16.TabIndex = 125;
            this.label16.Text = "�÷�:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cboDosUnit
            // 
            this.m_cboDosUnit.Font = new System.Drawing.Font("����", 11F);
            this.m_cboDosUnit.Location = new System.Drawing.Point(144, 306);
            this.m_cboDosUnit.Name = "m_cboDosUnit";
            this.m_cboDosUnit.Size = new System.Drawing.Size(152, 23);
            this.m_cboDosUnit.TabIndex = 11;
            this.m_cboDosUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboUnit_KeyDown);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("����", 10.5F);
            this.label15.Location = new System.Drawing.Point(24, 308);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 14);
            this.label15.TabIndex = 124;
            this.label15.Text = "����������λ:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("����", 10.5F);
            this.label14.Location = new System.Drawing.Point(24, 281);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 14);
            this.label14.TabIndex = 123;
            this.label14.Text = "��������:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("����", 10.5F);
            this.label13.Location = new System.Drawing.Point(324, 264);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 14);
            this.label13.TabIndex = 122;
            this.label13.Text = "סԺ��Ʊ���:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("����", 10.5F);
            this.label12.Location = new System.Drawing.Point(324, 210);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 14);
            this.label12.TabIndex = 121;
            this.label12.Text = "���﷢Ʊ���:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("����", 10.5F);
            this.label11.Location = new System.Drawing.Point(324, 236);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 14);
            this.label11.TabIndex = 120;
            this.label11.Text = "סԺ�������:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("����", 10.5F);
            this.label10.Location = new System.Drawing.Point(324, 183);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 14);
            this.label10.TabIndex = 119;
            this.label10.Text = "����������:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // m_cboIPUnit
            // 
            this.m_cboIPUnit.Font = new System.Drawing.Font("����", 11F);
            this.m_cboIPUnit.Location = new System.Drawing.Point(440, 78);
            this.m_cboIPUnit.Name = "m_cboIPUnit";
            this.m_cboIPUnit.Size = new System.Drawing.Size(152, 23);
            this.m_cboIPUnit.TabIndex = 22;
            this.m_cboIPUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboUnit_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("����", 10.5F);
            this.label9.Location = new System.Drawing.Point(324, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 14);
            this.label9.TabIndex = 118;
            this.label9.Text = "��С��λ:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cboOPUnit
            // 
            this.m_cboOPUnit.Font = new System.Drawing.Font("����", 11F);
            this.m_cboOPUnit.Location = new System.Drawing.Point(440, 53);
            this.m_cboOPUnit.Name = "m_cboOPUnit";
            this.m_cboOPUnit.Size = new System.Drawing.Size(152, 23);
            this.m_cboOPUnit.TabIndex = 21;
            this.m_cboOPUnit.SelectedIndexChanged += new System.EventHandler(this.m_cboOPUnit_SelectedIndexChanged);
            this.m_cboOPUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboUnit_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("����", 10.5F);
            this.label8.Location = new System.Drawing.Point(324, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 14);
            this.label8.TabIndex = 117;
            this.label8.Text = "������λ:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // m_cboUnit
            // 
            this.m_cboUnit.Font = new System.Drawing.Font("����", 11F);
            this.m_cboUnit.Location = new System.Drawing.Point(440, 28);
            this.m_cboUnit.Name = "m_cboUnit";
            this.m_cboUnit.Size = new System.Drawing.Size(152, 23);
            this.m_cboUnit.TabIndex = 20;
            this.m_cboUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboUnit_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("����", 10.5F);
            this.label7.Location = new System.Drawing.Point(324, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 14);
            this.label7.TabIndex = 116;
            this.label7.Text = "��λ:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtPrice
            // 
            this.m_txtPrice.CausesValidation = false;
            //this.m_txtPrice.EnableAutoValidation = false;
            //this.m_txtPrice.EnableEnterKeyValidate = false;
            //this.m_txtPrice.EnableEscapeKeyUndo = true;
            //this.m_txtPrice.EnableLastValidValue = true;
            //this.m_txtPrice.ErrorProvider = null;
            //this.m_txtPrice.ErrorProviderMessage = "Invalid value";
            this.m_txtPrice.Font = new System.Drawing.Font("����", 11F);
            //this.m_txtPrice.ForceFormatText = true;
            this.m_txtPrice.Location = new System.Drawing.Point(144, 331);
            this.m_txtPrice.MaxLength = 10;
            this.m_txtPrice.Name = "m_txtPrice";
            //this.m_txtPrice.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator;
            this.m_txtPrice.Size = new System.Drawing.Size(152, 24);
            this.m_txtPrice.TabIndex = 12;
            this.m_txtPrice.Text = "0";
            this.m_txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtPrice.TextChanged += new System.EventHandler(this.m_txtPacQ_TextChanged);
            this.m_txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtPrice_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("����", 10.5F);
            this.label6.Location = new System.Drawing.Point(24, 334);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 14);
            this.label6.TabIndex = 115;
            this.label6.Text = "���ۼ�(Ԫ):";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtSpec
            // 
            //this.m_txtSpec.EnableAutoValidation = false;
            //this.m_txtSpec.EnableEnterKeyValidate = false;
            //this.m_txtSpec.EnableEscapeKeyUndo = true;
            //this.m_txtSpec.EnableLastValidValue = true;
            //this.m_txtSpec.ErrorProvider = null;
            //this.m_txtSpec.ErrorProviderMessage = "Invalid value";
            this.m_txtSpec.Font = new System.Drawing.Font("����", 11F);
            //this.m_txtSpec.ForceFormatText = true;
            this.m_txtSpec.Location = new System.Drawing.Point(144, 198);
            this.m_txtSpec.MaxLength = 50;
            this.m_txtSpec.Name = "m_txtSpec";
            this.m_txtSpec.Size = new System.Drawing.Size(152, 24);
            this.m_txtSpec.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("����", 10.5F);
            this.label5.Location = new System.Drawing.Point(24, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 14);
            this.label5.TabIndex = 114;
            this.label5.Text = "���:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtWB
            // 
            //this.m_txtWB.EnableAutoValidation = false;
            //this.m_txtWB.EnableEnterKeyValidate = false;
            //this.m_txtWB.EnableEscapeKeyUndo = true;
            //this.m_txtWB.EnableLastValidValue = true;
            //this.m_txtWB.ErrorProvider = null;
            //this.m_txtWB.ErrorProviderMessage = "Invalid value";
            this.m_txtWB.Font = new System.Drawing.Font("����", 11F);
            //this.m_txtWB.ForceFormatText = true;
            this.m_txtWB.Location = new System.Drawing.Point(144, 171);
            this.m_txtWB.MaxLength = 10;
            this.m_txtWB.Name = "m_txtWB";
            this.m_txtWB.Size = new System.Drawing.Size(152, 24);
            this.m_txtWB.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("����", 10.5F);
            this.label4.Location = new System.Drawing.Point(24, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 14);
            this.label4.TabIndex = 113;
            this.label4.Text = "�����:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtPY
            // 
            this.m_txtPY.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            //this.m_txtPY.EnableAutoValidation = false;
            //this.m_txtPY.EnableEnterKeyValidate = false;
            //this.m_txtPY.EnableEscapeKeyUndo = true;
            //this.m_txtPY.EnableLastValidValue = true;
            //this.m_txtPY.ErrorProvider = null;
            //this.m_txtPY.ErrorProviderMessage = "Invalid value";
            this.m_txtPY.Font = new System.Drawing.Font("����", 11F);
            //this.m_txtPY.ForceFormatText = true;
            this.m_txtPY.Location = new System.Drawing.Point(144, 144);
            this.m_txtPY.MaxLength = 10;
            this.m_txtPY.Name = "m_txtPY";
            this.m_txtPY.Size = new System.Drawing.Size(152, 24);
            this.m_txtPY.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("����", 10.5F);
            this.label3.Location = new System.Drawing.Point(24, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 112;
            this.label3.Text = "ƴ����:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtName
            // 
            //this.m_txtName.EnableAutoValidation = false;
            //this.m_txtName.EnableEnterKeyValidate = false;
            //this.m_txtName.EnableEscapeKeyUndo = true;
            //this.m_txtName.EnableLastValidValue = true;
            //this.m_txtName.ErrorProvider = null;
            //this.m_txtName.ErrorProviderMessage = "Invalid value";
            this.m_txtName.Font = new System.Drawing.Font("����", 11F);
            //this.m_txtName.ForceFormatText = true;
            this.m_txtName.Location = new System.Drawing.Point(144, 63);
            this.m_txtName.MaxLength = 24;
            this.m_txtName.Name = "m_txtName";
            this.m_txtName.Size = new System.Drawing.Size(152, 24);
            this.m_txtName.TabIndex = 2;
            this.m_txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtName_KeyDown);
            // 
            // m_txtNo
            // 
            //this.m_txtNo.EnableAutoValidation = false;
            //this.m_txtNo.EnableEnterKeyValidate = false;
            //this.m_txtNo.EnableEscapeKeyUndo = true;
            //this.m_txtNo.EnableLastValidValue = true;
            //this.m_txtNo.ErrorProvider = null;
            //this.m_txtNo.ErrorProviderMessage = "Invalid value";
            this.m_txtNo.Font = new System.Drawing.Font("����", 11F);
            //this.m_txtNo.ForceFormatText = true;
            this.m_txtNo.Location = new System.Drawing.Point(144, 36);
            this.m_txtNo.MaxLength = 20;
            this.m_txtNo.Name = "m_txtNo";
            this.m_txtNo.Size = new System.Drawing.Size(152, 24);
            this.m_txtNo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("����", 10.5F);
            this.label2.Location = new System.Drawing.Point(24, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 111;
            this.label2.Text = "��Ŀ����:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("����", 10.5F);
            this.label20.Location = new System.Drawing.Point(24, 37);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 14);
            this.label20.TabIndex = 110;
            this.label20.Text = "��Ŀ���:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtDosage
            // 
            this.m_txtDosage.CausesValidation = false;
            //this.m_txtDosage.EnableAutoValidation = true;
            //this.m_txtDosage.EnableEnterKeyValidate = true;
            //this.m_txtDosage.EnableEscapeKeyUndo = true;
            //this.m_txtDosage.EnableLastValidValue = true;
            //this.m_txtDosage.ErrorProvider = null;
            //this.m_txtDosage.ErrorProviderMessage = "Invalid value";
            this.m_txtDosage.Font = new System.Drawing.Font("����", 11F);
            //this.m_txtDosage.ForceFormatText = true;
            this.m_txtDosage.Location = new System.Drawing.Point(144, 279);
            this.m_txtDosage.MaxLength = 10;
            this.m_txtDosage.Name = "m_txtDosage";
            //this.m_txtDosage.NumericCharStyle = SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator;
            this.m_txtDosage.Size = new System.Drawing.Size(152, 24);
            this.m_txtDosage.TabIndex = 10;
            this.m_txtDosage.Text = "0";
            this.m_txtDosage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtDosage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtDosage_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.bt_Refresh);
            this.panel1.Controls.Add(this.m_cmbType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(2, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 725);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGrid1);
            this.groupBox1.Location = new System.Drawing.Point(6, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 673);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // dataGrid1
            // 
            this.dataGrid1.AllowSorting = false;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.White;
            this.dataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGrid1.CaptionBackColor = System.Drawing.Color.DarkGray;
            this.dataGrid1.CaptionFont = new System.Drawing.Font("����", 11F, System.Drawing.FontStyle.Bold);
            this.dataGrid1.CaptionVisible = false;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(3, 22);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ReadOnly = true;
            this.dataGrid1.RowHeaderWidth = 20;
            this.dataGrid1.SelectionBackColor = System.Drawing.Color.DarkGray;
            this.dataGrid1.Size = new System.Drawing.Size(325, 648);
            this.dataGrid1.TabIndex = 6;
            this.dataGrid1.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.AllowSorting = false;
            this.dataGridTableStyle1.DataGrid = this.dataGrid1;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "dt";
            this.dataGridTableStyle1.RowHeaderWidth = 10;
            this.dataGridTableStyle1.SelectionBackColor = System.Drawing.Color.DarkGray;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "��Ŀ���";
            this.dataGridTextBoxColumn1.MappingName = "ITEMCODE_VCHR";
            this.dataGridTextBoxColumn1.ReadOnly = true;
            this.dataGridTextBoxColumn1.Width = 85;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "��Ŀ����";
            this.dataGridTextBoxColumn2.MappingName = "ITEMNAME_VCHR";
            this.dataGridTextBoxColumn2.ReadOnly = true;
            this.dataGridTextBoxColumn2.Width = 205;
            // 
            // bt_Refresh
            // 
            this.bt_Refresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.bt_Refresh.DefaultScheme = true;
            this.bt_Refresh.DialogResult = System.Windows.Forms.DialogResult.None;
            this.bt_Refresh.Font = new System.Drawing.Font("����", 10.5F);
            this.bt_Refresh.Hint = "";
            this.bt_Refresh.Location = new System.Drawing.Point(260, 9);
            this.bt_Refresh.Name = "bt_Refresh";
            this.bt_Refresh.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.bt_Refresh.Size = new System.Drawing.Size(76, 32);
            this.bt_Refresh.TabIndex = 5;
            this.bt_Refresh.Text = "ˢ��";
            this.bt_Refresh.Click += new System.EventHandler(this.bt_Refresh_Click);
            // 
            // m_cmbType
            // 
            this.m_cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbType.Location = new System.Drawing.Point(87, 14);
            this.m_cmbType.Name = "m_cmbType";
            this.m_cmbType.Size = new System.Drawing.Size(170, 24);
            this.m_cmbType.TabIndex = 4;
            this.m_cmbType.SelectedIndexChanged += new System.EventHandler(this.m_cmbType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("����", 10.5F);
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "��Ŀ����:";
            // 
            // frmChargeItem3
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(1036, 738);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("����", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmChargeItem3";
            this.Text = "�շ���Ŀά��";
            this.Load += new System.EventHandler(this.frmChargeItem3_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmChargeItem3_Closing);
            this.Resize += new System.EventHandler(this.frmChargeItem3_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmChargeItem3_KeyDown);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void frmChargeItem3_Resize(object sender, System.EventArgs e)
        {
            this.panel4.Left = (this.panel2.Width - this.panel4.Width) / 2;
            this.panel4.Top = (this.panel2.Height - this.panel4.Height) / 2;
        }

        private void frmChargeItem3_Load(object sender, System.EventArgs e)
        {
            this.lsvDefaultFreq.Hide();
            m_mthSetEnter2Tab(new System.Windows.Forms.Control[] { this.m_txtFind, this.m_txtDefaultFreq, this.m_txtCaseCal, this.m_txtIPCal, this.m_txtIPInvoice, this.m_txtOPCal, this.m_txtOPInvoice, this.m_txtExeType, this.m_txtOrderCateType });
            ((clsCtl_ChargeItem2)this.objController).m_mthFormLoad();

        }

        internal void m_cmbType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ((clsCtl_ChargeItem2)this.objController).m_mthFindChargeItem(this.m_cmbType.SelectItemValue.Trim(), "", "");

            //			if(this.m_cmbType.SelectItemValue.Trim()=="0001"||this.m_cmbType.SelectItemValue.Trim()=="0002")//�����ҩ,�Ͳ���ѡ�����Զ���۸�
            //			{
            if (this.m_cmbType.Text.IndexOf("ҩ") > -1)//�����ҩ,�Ͳ���ѡ�����Զ���۸�
            {
                m_cmbCoustomPrice.Enabled = false;
                m_cboUnit.Text = "";
                m_cboUnit.Enabled = false;
                m_cboOPUnit.Enabled = true;
                m_cboIPUnit.Enabled = true;

            }
            else
            {

                m_cboUnit.Enabled = true;
                m_cmbCoustomPrice.Enabled = true;
                m_cboOPUnit.Enabled = false;
                m_cboIPUnit.Enabled = false;
            }

        }

        private void frmChargeItem3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("ȷ���˳���?", "iCare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                this.Close();
            }
            m_mthSetKeyTab(e);
            switch (e.KeyCode)
            {
                case Keys.F2:
                    this.btAdd_Click(null, null);
                    break;
                case Keys.F3:
                    this.btSave_Click(null, null);
                    break;
                case Keys.F4:
                    this.btDel_Click(null, null);
                    break;
                case Keys.F5:
                    this.btClear_Click(null, null);
                    break;
                case Keys.F7:
                    this.m_btFind_Click(null, null);
                    break;
                case Keys.Escape:
                    this.btExit_Click(null, null);
                    break;
            }
        }

        private void m_cmbFind_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ((clsCtl_ChargeItem2)this.objController).m_cmbFind_SelectedIndexChanged();
        }

        private void bt_Refresh_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_ChargeItem2)this.objController).m_mthFindChargeItem(this.m_cmbType.SelectItemValue.Trim(), "", "");
        }

        private void m_txtFind_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && m_txtFind.Text.Trim() != "")
            {
                this.m_btFind_Click(null, null);

            }
            else
            {
                this.frmChargeItem3_KeyDown(m_txtFind, e);
            }
        }

        private void m_btFind_Click(object sender, System.EventArgs e)
        {
            this.m_txtFind.m_mthAddItem();
            if (this.m_txtFind.Items.Count > 0)
            {
                this.m_txtFind.Text = this.m_txtFind.Items[0].ToString();
            }
            ((clsCtl_ChargeItem2)this.objController).m_mthFindChargeItem(this.m_cmbType.SelectItemValue.Trim(), m_cmbFind.Tag.ToString(), m_txtFind.Text);

        }

        private void btExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #region  �����ؼ��ı�ѡ��
        private void m_cmbOPUseUint_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (m_cmbOPUseUint.SelectedIndex)
            {
                case 0:
                    m_cmbOPUseUint.Tag = "0";
                    break;
                default:
                    m_cmbOPUseUint.Tag = "1";
                    break;
            }
            ((clsCtl_ChargeItem2)this.objController).m_mthCalPrice();
        }
        private void m_cmbIPUseUint_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (m_cmbIPUseUint.SelectedIndex)
            {
                case 0:
                    m_cmbIPUseUint.Tag = "0";
                    break;
                default:
                    m_cmbIPUseUint.Tag = "1";
                    break;
            }
            ((clsCtl_ChargeItem2)this.objController).m_mthCalPrice();
        }
        private void m_cmbCoustomPrice_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (m_cmbCoustomPrice.SelectedIndex)
            {
                case 0:
                    m_cmbCoustomPrice.Tag = "0";
                    break;
                default:
                    m_cmbCoustomPrice.Tag = "1";
                    break;
            }
        }

        private void m_cmbIsExpensive_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (m_cmbIsExpensive.SelectedIndex)
            {
                case 0:
                    m_cmbIsExpensive.Tag = "0";
                    break;
                default:
                    m_cmbIsExpensive.Tag = "1";
                    break;
            }
        }

        private void m_cmbIsInDocAdv_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (m_cmbIsInDocAdv.SelectedIndex)
            {
                case 0:
                    m_cmbIsInDocAdv.Tag = "0";
                    break;
                default:
                    m_cmbIsInDocAdv.Tag = "1";
                    break;
            }
        }


        #endregion

        private void btClear_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_ChargeItem2)this.objController).m_Clear();
        }

        private void dataGrid1_CurrentCellChanged(object sender, System.EventArgs e)
        {
            ((clsCtl_ChargeItem2)this.objController).m_mthDataGridCellChange();
        }

        private void btSave_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_ChargeItem2)this.objController).m_mthSave();
        }

        private void btAdd_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_ChargeItem2)this.objController).m_mthAddNew();
        }

        private void btDel_Click(object sender, System.EventArgs e)
        {
            //			((clsCtl_ChargeItem2)this.objController).m_mthDeleteChargeItem();
        }

        private void m_cboUnit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
        }

        private void btChangeCat_Click(object sender, System.EventArgs e)
        {
            DialogResult m_objDialogResult = DialogResult.Cancel;
            m_objDialogResult = MessageBox.Show("�Ƿ���ķ���?", "iCare��ʾ��Ϣ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (m_objDialogResult == DialogResult.OK)
            {
                ((clsCtl_ChargeItem2)this.objController).m_mthChangeCat();
            }
            else
            {
                return;
            }
        }

        private void cmbIsStop_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            cmbIsStop.Tag = cmbIsStop.SelectedIndex;
            //			switch(cmbIsStop.SelectedIndex)
            //			{
            //				case 0:
            //					cmbIsStop.Tag=0;
            //					break;
            //				default :
            //					cmbIsStop.Tag=1;
            //					break;
            //			}
        }

        private void m_txtPacQ_TextChanged(object sender, System.EventArgs e)
        {
            ((clsCtl_ChargeItem2)this.objController).m_mthCalPrice();
        }
        #region Լ������
        private void m_txtPacQ_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                if (m_txtPacQ.Text.Trim() == "")
                {
                    m_txtPacQ.Text = "0.";
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                    e.Handled = true;
                }
                if (this.m_txtPacQ.Text.IndexOf(".") > -1)
                {
                    e.Handled = true;
                }
            }
        }

        private void m_txtDosage_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                if (m_txtDosage.Text.Trim() == "")
                {
                    m_txtDosage.Text = "0.";
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                    e.Handled = true;
                }
                if (this.m_txtDosage.Text.IndexOf(".") > -1)
                {
                    e.Handled = true;
                }
            }
        }

        private void m_txtPrice_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                if (m_txtPrice.Text.Trim() == "")
                {
                    m_txtPrice.Text = "0.";
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                    e.Handled = true;
                }
                if (this.m_txtPrice.Text.IndexOf(".") > -1)
                {
                    e.Handled = true;
                }
            }
        }

        private void m_txtTradePrice_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                if (m_txtTradePrice.Text.Trim() == "")
                {
                    m_txtTradePrice.Text = "0.";
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                    e.Handled = true;
                }
                if (this.m_txtTradePrice.Text.IndexOf(".") > -1)
                {
                    e.Handled = true;
                }
            }
        }
        #endregion

        private void btPrint_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_ChargeItem2)this.objController).m_mthPrintChargeItem();
        }

        private void frmChargeItem3_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.m_txtPrice.TextChanged -= new System.EventHandler(this.m_txtPacQ_TextChanged);
            this.m_txtPacQ.TextChanged -= new System.EventHandler(this.m_txtPacQ_TextChanged);
        }

        private void buttonXP1_Click(object sender, System.EventArgs e)
        {
            if (this.btSave.Tag != null)//�޸�
            {
                frmSingleItemDiscount objfrm = new frmSingleItemDiscount();
                objfrm.Text += " ��" + this.m_txtName.Text.Trim();
                objfrm.strItemID = this.btSave.Tag.ToString().Trim();
                objfrm.ShowDialog();
            }
        }

        private void m_txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                com.digitalwave.Utility.clsCreateChinaCode Ccode = new com.digitalwave.Utility.clsCreateChinaCode();
                m_txtWB.Text = Ccode.m_strCreateChinaCode(m_txtName.Text.Trim() + txtCommName.Text.Trim(), ChinaCode.WB);
                m_txtPY.Text = Ccode.m_strCreateChinaCode(m_txtName.Text.Trim() + txtCommName.Text.Trim(), ChinaCode.PY);
            }
        }

        private void COBINSURANCETYPE_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }

        private void m_cboApplyType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ((clsCtl_ChargeItem2)this.objController).m_mthGetPartInfo();
        }
        private void m_mthShowPriceInfo()
        {
            if (this.btSave.Tag == null)
            {
                return;
            }
            frmChangePriceInfo frmObj = new frmChangePriceInfo();
            frmObj.ItemCode = this.m_txtNo.Text;
            frmObj.ItemID = this.btSave.Tag.ToString();
            frmObj.ItemName = this.m_txtName.Text;
            frmObj.ItemPrice = this.m_txtPrice.Text;
            frmObj.ShowDialog();
        }

        private void btPrice_Click(object sender, System.EventArgs e)
        {
            this.m_mthShowPriceInfo();
        }

        private void buttonXP1_Click_1(object sender, System.EventArgs e)
        {
            if (this.btSave.Tag == null)
                return;
            frmSUBCHARGEITEM frmshow = new frmSUBCHARGEITEM((string)this.btSave.Tag, m_txtName.Text);
            frmshow.ShowDialog();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void m_cboOPUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void m_cboOPCal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void m_txtDefaultFreq_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (((clsCtl_ChargeItem2)this.objController).m_mthFindDefaultFreq(m_txtDefaultFreq.Text.Trim().ToUpper()) > 0)
                {
                    this.lsvDefaultFreq.Show();
                    this.lsvDefaultFreq.BringToFront();
                    this.lsvDefaultFreq.Items[0].Selected = true;
                    this.lsvDefaultFreq.Select();
                    this.lsvDefaultFreq.Focus();
                }

            }
        }

        private void lsvDefaultFreq_Leave(object sender, EventArgs e)
        {
            this.lsvDefaultFreq.Hide();
        }

        private void lsvDefaultFreq_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsCtl_ChargeItem2)this.objController).m_mthFillDefaultFreq();
            }
        }

        private void cmbIsStop_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void m_cmbSelf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btSave.Focus();
            }
        }

        private void txtCommName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                com.digitalwave.Utility.clsCreateChinaCode Ccode = new com.digitalwave.Utility.clsCreateChinaCode();
                m_txtWB.Text = Ccode.m_strCreateChinaCode(m_txtName.Text.Trim() + txtCommName.Text.Trim(), ChinaCode.WB);
                m_txtPY.Text = Ccode.m_strCreateChinaCode(m_txtName.Text.Trim() + txtCommName.Text.Trim(), ChinaCode.PY);
            }
        }

        private void m_txtOPCal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (((clsCtl_ChargeItem2)this.objController).m_mthFindExeType(m_txtOPCal.Text.Trim(), 1, ref m_txtOPCal) > 0)
                {
                    this.m_lsvExcType.Tag = 1;
                    this.m_lsvExcType.Location = new Point(((TextBox)sender).Location.X, ((TextBox)sender).Location.Y - 128);
                    this.m_lsvExcType.Show();
                    this.m_lsvExcType.BringToFront();
                    this.m_lsvExcType.Items[0].Selected = true;
                    this.m_lsvExcType.Select();
                    this.m_lsvExcType.Focus();
                }

            }
        }

        private void m_txtOPInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (((clsCtl_ChargeItem2)this.objController).m_mthFindExeType(m_txtOPInvoice.Text.Trim(), 2, ref m_txtOPInvoice) > 0)
                {
                    this.m_lsvExcType.Tag = 2;
                    this.m_lsvExcType.Location = new Point(((TextBox)sender).Location.X, ((TextBox)sender).Location.Y - 128);
                    this.m_lsvExcType.Show();
                    this.m_lsvExcType.BringToFront();
                    this.m_lsvExcType.Items[0].Selected = true;
                    this.m_lsvExcType.Select();
                    this.m_lsvExcType.Focus();
                }

            }
        }

        private void m_txtIPCal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (((clsCtl_ChargeItem2)this.objController).m_mthFindExeType(m_txtIPCal.Text.Trim(), 3, ref m_txtIPCal) > 0)
                {
                    this.m_lsvExcType.Tag = 3;
                    this.m_lsvExcType.Location = new Point(((TextBox)sender).Location.X, ((TextBox)sender).Location.Y - 128);
                    this.m_lsvExcType.Show();
                    this.m_lsvExcType.BringToFront();
                    this.m_lsvExcType.Items[0].Selected = true;
                    this.m_lsvExcType.Select();
                    this.m_lsvExcType.Focus();
                }

            }
        }

        private void m_txtIPInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (((clsCtl_ChargeItem2)this.objController).m_mthFindExeType(m_txtIPInvoice.Text.Trim(), 4, ref m_txtIPInvoice) > 0)
                {
                    this.m_lsvExcType.Tag = 4;
                    this.m_lsvExcType.Location = new Point(((TextBox)sender).Location.X, ((TextBox)sender).Location.Y - 128);
                    this.m_lsvExcType.Show();
                    this.m_lsvExcType.BringToFront();
                    this.m_lsvExcType.Items[0].Selected = true;
                    this.m_lsvExcType.Select();
                    this.m_lsvExcType.Focus();
                }

            }
        }

        private void m_txtCaseCal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (((clsCtl_ChargeItem2)this.objController).m_mthFindExeType(m_txtCaseCal.Text.Trim(), 5, ref m_txtCaseCal) > 0)
                {
                    this.m_lsvExcType.Tag = 5;
                    this.m_lsvExcType.Location = new Point(((TextBox)sender).Location.X, ((TextBox)sender).Location.Y - 128);
                    this.m_lsvExcType.Show();
                    this.m_lsvExcType.BringToFront();
                    this.m_lsvExcType.Items[0].Selected = true;
                    this.m_lsvExcType.Select();
                    this.m_lsvExcType.Focus();
                }

            }
        }

        private void m_lsvExcType_Leave(object sender, EventArgs e)
        {
            this.m_lsvExcType.Hide();
        }

        private void m_lsvExcType_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                switch (Convert.ToInt32(m_lsvExcType.Tag))
                {
                    case 1: ((clsCtl_ChargeItem2)this.objController).m_mthFillExeType(ref this.m_txtOPCal); this.m_txtOPInvoice.Focus(); break;
                    case 2: ((clsCtl_ChargeItem2)this.objController).m_mthFillExeType(ref this.m_txtOPInvoice); this.m_txtIPCal.Focus(); break;
                    case 3: ((clsCtl_ChargeItem2)this.objController).m_mthFillExeType(ref this.m_txtIPCal); this.m_txtIPInvoice.Focus(); break;
                    case 4: ((clsCtl_ChargeItem2)this.objController).m_mthFillExeType(ref this.m_txtIPInvoice); this.m_txtCaseCal.Focus(); break;
                    case 5: ((clsCtl_ChargeItem2)this.objController).m_mthFillExeType(ref this.m_txtCaseCal); this.m_cboApplyType.Focus(); break;
                }

            }
        }

        private void m_txtExeType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (((clsCtl_ChargeItem2)this.objController).m_mthFindExeType(m_txtExeType.Text.Trim().ToUpper()) > 0)
                {
                    this.m_lsvExeType.Location = new Point(((TextBox)sender).Location.X, ((TextBox)sender).Location.Y - 128);
                    this.m_lsvExeType.Show();
                    this.m_lsvExeType.BringToFront();
                    this.m_lsvExeType.Items[0].Selected = true;
                    this.m_lsvExeType.Select();
                    this.m_lsvExeType.Focus();
                }

            }
        }

        private void m_txtOrderCateType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (((clsCtl_ChargeItem2)this.objController).m_mthFindOrderCateType(m_txtOrderCateType.Text.Trim().ToUpper()) > 0)
                {
                    this.m_lsvOrderType.Location = new Point(((TextBox)sender).Location.X, ((TextBox)sender).Location.Y - 128);
                    this.m_lsvOrderType.Show();
                    this.m_lsvOrderType.BringToFront();
                    this.m_lsvOrderType.Items[0].Selected = true;
                    this.m_lsvOrderType.Select();
                    this.m_lsvOrderType.Focus();
                }

            }
        }

        private void m_lsvExeType_Leave(object sender, EventArgs e)
        {
            this.m_lsvExeType.Hide();
        }

        private void m_lsvOrderType_Leave(object sender, EventArgs e)
        {
            this.m_lsvOrderType.Hide();
        }

        private void m_lsvExeType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsCtl_ChargeItem2)this.objController).m_mthFillExeType();
            }
        }

        private void m_lsvOrderType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsCtl_ChargeItem2)this.objController).m_mthFillOrderCateType();
            }
        }

        private void btnRegular_Click(object sender, EventArgs e)
        {
            frmAidChooseRegular frm = new frmAidChooseRegular();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.txtRegular.Text = frm.RuleExpress;
            }
        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void m_cboKeepUse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
