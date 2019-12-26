using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.Utility.Controls;
using com.digitalwave.iCare.gui.HIS;

namespace com.digitalwave.iCare.BIHOrder.Control
{
    /// <summary>
    /// ctlBIHOrderDetail ��ժҪ˵����
    /// </summary>
    public class ctlBIHOrderDetail : System.Windows.Forms.UserControl
    {
        #region �Զ������
        /// <summary>
        /// �Ƿ�����ҽ��,��¼����ҽ��ʱ������������
        /// </summary>
        public bool IsSubOrder = false;
        /// <summary>
        /// ����
        /// </summary>
        public int m_intAge = 0;
        /// <summary>
        /// ��ҽ��
        /// </summary>
        public clsBIHOrder ParentOrder;
        /*<========================*/
        /// <summary>
        /// ҽ�������޸ı�־
        /// </summary>
        public bool m_blOrderName2ReadOnly = false;
        /// <summary>
        /// ҩ�����ͷ���ID 1-��ҩ 2-��ҩ 3-����
        /// </summary>
        public int m_intMEDICNETYPE_INT = 0;
        /// <summary>
        /// �÷�VO��
        /// </summary>
        public clsBSEUsageType[] m_arrUsage = null;
        /// <summary>
        /// Ƶ��VO��
        /// </summary>
        public clsAIDRecipeFreq[] m_arrFreq = null;
        /// <summary>
        /// �Ѳ�ѯ����ҩƷ�Ƽ�����key-orderdic_id,value-�Ƽ���������
        /// </summary>
        public Hashtable m_htMEDICINEPREPTYPE = new Hashtable();
        /// <summary>
        /// ҩƷ�����
        /// </summary>
        public float m_fotOpcurrentgross_num = 0;
        /// <summary>
        /// ��¼�Ƿ���ҩƷ 1��ҩƷ  2Ϊ����
        /// </summary>
        public int m_intITEMSRCTYPE_INT = 0;
        #endregion

        #region �ؼ�����

        private System.Windows.Forms.Label label3;
        public TextBox m_txtRecipeNo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox m_txtExecDept;
        internal System.Windows.Forms.TextBox m_txtGetUnit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        internal Label m_lblDosage;
        private System.Windows.Forms.Label label8;
        internal Label m_lblExecuteFreq;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        public Label m_lblDosageType;
        private System.Windows.Forms.CheckBox m_chkIsRepare;
        private System.Windows.Forms.TextBox m_txtOrderSpec;
        private System.Windows.Forms.TextBox m_txtPackage;
        internal System.Windows.Forms.TextBox m_txtDosage;
        internal System.Windows.Forms.TextBox m_txtUse;
        internal System.Windows.Forms.TextBox m_txtGet;
        /// <summary>
        /// ҽ������
        /// </summary>
        public System.Windows.Forms.TextBox m_txtEntrust;
        private System.Windows.Forms.CheckBox m_chkIsRich;
        internal System.Windows.Forms.TextBox m_txtUseUnit;
        internal System.Windows.Forms.TextBox m_txtDosageUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label m_lblBackground;
        public System.Windows.Forms.TextBox m_txtPrice;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox m_txtInputDate;
        private System.Windows.Forms.PictureBox m_picInfo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ImageList m_imgIcons;
        private System.ComponentModel.IContainer components;
        private clsTextFocusHighlight m_objHighlight;
        /// <summary>
        /// ���ñ�־
        /// </summary>
        public com.digitalwave.controls.ctlQComboBox m_cboRateType;
        private System.Windows.Forms.CheckBox m_chkISNEEDFEEL;
        internal System.Windows.Forms.Label m_lblDay;
        public System.Windows.Forms.TextBox m_txtDays;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox m_TxbStartTime;
        private System.Windows.Forms.TextBox m_txbFinishTime;
        private com.digitalwave.controls.ctlQComboBox m_cboRepare;
        //private clsBIHOrderService m_objService;
        internal com.digitalwave.controls.ctlQComboBox m_cboExecuteType;
        //private clsBIHOrderGroupService m_objService2;	//����Service
        /// <summary>
        /// ��������¼�
        /// </summary>
        public event EventHandler m_evtInputEnd;
        public event EventHandler m_evtInputNO;
        public event EventHandler evtInputOrder;
        /// <summary>
        /// ִ��Ƶ��
        /// </summary>
        internal com.digitalwave.controls.ctlFindTextBox m_txtExecuteFreq;
        public com.digitalwave.controls.ctlFindTextBox m_txtDosageType;
        /// <summary>
        /// ����ҽ��
        /// </summary>
        internal com.digitalwave.controls.ctlFindTextBox m_txtFatherOrder;
        /// <summary>
        /// ¼��ҽ��
        /// </summary>
        internal com.digitalwave.controls.ctlFindTextBox m_txtDoctor;
        /// <summary>
        /// ҽ������
        /// </summary>
        internal com.digitalwave.controls.ctlFindTextBox m_txtOrderName;
        private clsBIHOrderGroup m_objCurrentGroup = null;
        /// <summary>
        /// ����Ƶ����Ϣ����ʱ����,��Ϊ��������.
        /// </summary>
        private clsAIDRecipeFreq m_objTempFreq = null;
        /// <summary>
        /// ����Ƶ����Ϣ
        /// </summary>
        public Hashtable m_htTempFreq = null;
        /// <summary>
        /// �����÷���Ϣ
        /// </summary>
        public Hashtable m_htTempUsage = null;
        /// <summary>
        ///	OnceƵ����Ϣ����
        /// </summary>
        private clsAIDRecipeFreq m_objOnceFreq = null;
        /// <summary>
        /// �������� 0�û�����   1�����	2ƴ����
        /// </summary>
        private int m_intInputType = 0;
        /// <summary>
        /// ָ����ǰ¼����Ŀ�Ƿ�������
        /// </summary>
        private bool m_blnCurrentItemIsGroup = false;
        private clsBIHOrderDic m_objCurrentDic = null;
        //private clsBIHPatientInfo m_objPatient;
        public clsBIHOrder m_objCurrentOrder;
        private clsBIHDoctor m_objCurrentDoctor;
        public string m_strDeptID = "";
        private bool m_blnReadOnly;
        private const int c_intItem_Group = 0;
        private System.Windows.Forms.TextBox m_txtISNEEDFEEL;
        private const int c_intItem_Order = 1;
        /// <summary>
        /// ����ID
        /// </summary>
        private string m_strPatientID_Chr = "";
        internal System.Windows.Forms.Label m_lblSaveOrderID;
        /// <summary>
        /// ��Ժ�Ǽ���ˮ��
        /// </summary>
        private string m_strRegisterID = "";
        private System.Windows.Forms.DateTimePicker m_dtStartTime;
        private System.Windows.Forms.DateTimePicker m_dtFinishTime;
        private System.Windows.Forms.Label m_lblSaveConOrderFreqID;
        internal System.Windows.Forms.Label m_lblxmclsa;
        internal System.Windows.Forms.CheckBox m_chkIsMedicare;
        internal com.digitalwave.controls.ctlFindTextBox m_txtSample;
        private Label m_lblSample;
        clsDcl_InputOrder m_objInputOrder = null;
        public com.digitalwave.iCare.BIHOrder.Control.ctlFindTextBox m_txtCheck;
        public Label m_lblCheck;

        /// <summary>
        /// �����־(true-�ǣ�false-��)
        /// </summary>
        public bool m_blSampleItem = false;
        public TextBox m_txtOrderName2;
        private Label label1;
        private Button m_btnBedList;
        public ctlFindTextBox m_txtBedNo;
        public ctlFindTextBox m_txtArea;
        private Label label19;
        private Label label10;
        private CheckBox checkBox1;
        private Label label14;
        public TextBox m_txtATTACHTIMES_INT;
        private Label label20;
        public ctlFindTextBox m_txtDoctorList;
        internal Label m_txtMedicareType;
        private Label label22;
        internal NullableDateControls.MaskDateEdit m_dtStartTime2;
        internal NullableDateControls.MaskDateEdit m_dtFinishTime2;
        internal TextBox m_txtGetUnit2;
        internal TextBox m_txtGet2;
        internal ComboBox m_cobOrderCate;
        private Label label17;
        internal com.digitalwave.controls.ctlFindTextBox m_txtOrderCate;
        public TextBox m_txtREMARK_VCHR;
        internal ContextMenuStrip m_MenuStripSelect;
        internal ToolStripMenuItem m_mnuCommonItems;
        internal ToolStripMenuItem m_mnuGroupItems;
        internal ToolStripMenuItem m_mnuNormalItems;
        private ToolStripMenuItem m_mnuNewPriceItems;
        internal ContextMenuStrip m_MenuStripREMARK;
        private ToolStripMenuItem m_mnuNormalRemark;
        internal ToolStripMenuItem m_mnuNormalAdd;
        internal TextBox m_hideDosage;
        internal TextBox m_hideDosageUnit;
        public com.digitalwave.controls.ctlFindTextBox m_hideDosageType;
        internal com.digitalwave.controls.ctlFindTextBox m_hideExecuteFreq;
        public TextBox m_hideDays;
        /// <summary>
        /// ����־(true-�ǣ�false����)
        /// </summary>
        public bool m_blCheckItem = false;
        public Label label7;
        public com.digitalwave.controls.ctlQComboBox cboShiying;
        private TextBox m_txtItemTradePrice;
        public Label lblKJ;
        public ComboBox cboKJ;
        public ComboBox cboQK;
        public Label lblQK;
        internal Label label9;
        internal Label label15;
        internal Label label21;
        public ComboBox cboProxyBoil;
        public com.digitalwave.controls.ctlQComboBox cboEmer;
        internal Label label23;
        public com.digitalwave.controls.ctlQComboBox cboOps;
        /*<=====================================*/
        /// <summary>
        /// ���ι�ϣ��
        /// </summary>
        internal Hashtable hasAppendViewType = new Hashtable();

        #endregion
        #region ���캯��
        public ctlBIHOrderDetail()
        {
            // �õ����� Windows.Forms ���������������ġ�
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;

            // TODO: �� InitializeComponent ���ú�����κγ�ʼ��
            //m_objService=clsGenerator.CreateObject(typeof(clsBIHOrderService)) as clsBIHOrderService;
            //m_objService2=clsGenerator.CreateObject(typeof(clsBIHOrderGroupService)) as clsBIHOrderGroupService;

            m_objHighlight = new clsTextFocusHighlight();
            m_objInputOrder = new clsDcl_InputOrder();

            timer1.Enabled = false;
            m_picInfo.Visible = false;
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

        #endregion
        #region �����������ɵĴ���
        /// <summary> 
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
        /// �޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlBIHOrderDetail));
            this.m_cboExecuteType = new com.digitalwave.controls.ctlQComboBox();
            this.m_lblSaveOrderID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtRecipeNo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.m_txtExecDept = new System.Windows.Forms.TextBox();
            this.m_txtGetUnit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_lblDosage = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.m_lblExecuteFreq = new System.Windows.Forms.Label();
            this.m_lblSaveConOrderFreqID = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.m_lblDosageType = new System.Windows.Forms.Label();
            this.m_chkIsRepare = new System.Windows.Forms.CheckBox();
            this.m_txtOrderSpec = new System.Windows.Forms.TextBox();
            this.m_txtPackage = new System.Windows.Forms.TextBox();
            this.m_txtDosage = new System.Windows.Forms.TextBox();
            this.m_txtUse = new System.Windows.Forms.TextBox();
            this.m_txtGet = new System.Windows.Forms.TextBox();
            this.m_cboRateType = new com.digitalwave.controls.ctlQComboBox();
            this.m_txtEntrust = new System.Windows.Forms.TextBox();
            this.m_chkIsRich = new System.Windows.Forms.CheckBox();
            this.m_txtUseUnit = new System.Windows.Forms.TextBox();
            this.m_txtDosageUnit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_lblBackground = new System.Windows.Forms.Label();
            this.m_txtPrice = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.m_txtInputDate = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.m_picInfo = new System.Windows.Forms.PictureBox();
            this.m_txtItemTradePrice = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.m_imgIcons = new System.Windows.Forms.ImageList(this.components);
            this.m_cboRepare = new com.digitalwave.controls.ctlQComboBox();
            this.m_chkISNEEDFEEL = new System.Windows.Forms.CheckBox();
            this.m_txtDays = new System.Windows.Forms.TextBox();
            this.m_lblDay = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.m_txtISNEEDFEEL = new System.Windows.Forms.TextBox();
            this.m_TxbStartTime = new System.Windows.Forms.TextBox();
            this.m_txbFinishTime = new System.Windows.Forms.TextBox();
            this.m_txtFatherOrder = new com.digitalwave.controls.ctlFindTextBox();
            this.m_txtOrderName = new com.digitalwave.controls.ctlFindTextBox();
            this.m_MenuStripSelect = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_mnuCommonItems = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuGroupItems = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuNormalItems = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuNewPriceItems = new System.Windows.Forms.ToolStripMenuItem();
            this.m_txtExecuteFreq = new com.digitalwave.controls.ctlFindTextBox();
            this.m_txtDoctor = new com.digitalwave.controls.ctlFindTextBox();
            this.m_txtDosageType = new com.digitalwave.controls.ctlFindTextBox();
            this.m_dtStartTime = new System.Windows.Forms.DateTimePicker();
            this.m_dtFinishTime = new System.Windows.Forms.DateTimePicker();
            this.m_lblxmclsa = new System.Windows.Forms.Label();
            this.m_chkIsMedicare = new System.Windows.Forms.CheckBox();
            this.m_txtSample = new com.digitalwave.controls.ctlFindTextBox();
            this.m_lblSample = new System.Windows.Forms.Label();
            this.m_lblCheck = new System.Windows.Forms.Label();
            this.m_txtOrderName2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnBedList = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.m_txtATTACHTIMES_INT = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.m_txtMedicareType = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.m_txtGetUnit2 = new System.Windows.Forms.TextBox();
            this.m_txtGet2 = new System.Windows.Forms.TextBox();
            this.m_dtFinishTime2 = new NullableDateControls.MaskDateEdit();
            this.m_dtStartTime2 = new NullableDateControls.MaskDateEdit();
            this.m_cobOrderCate = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.m_txtOrderCate = new com.digitalwave.controls.ctlFindTextBox();
            this.m_txtREMARK_VCHR = new System.Windows.Forms.TextBox();
            this.m_MenuStripREMARK = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_mnuNormalRemark = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuNormalAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.m_hideDosage = new System.Windows.Forms.TextBox();
            this.m_hideDosageUnit = new System.Windows.Forms.TextBox();
            this.m_hideDosageType = new com.digitalwave.controls.ctlFindTextBox();
            this.m_hideExecuteFreq = new com.digitalwave.controls.ctlFindTextBox();
            this.m_hideDays = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboShiying = new com.digitalwave.controls.ctlQComboBox();
            this.lblKJ = new System.Windows.Forms.Label();
            this.cboKJ = new System.Windows.Forms.ComboBox();
            this.cboQK = new System.Windows.Forms.ComboBox();
            this.lblQK = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cboProxyBoil = new System.Windows.Forms.ComboBox();
            this.cboEmer = new com.digitalwave.controls.ctlQComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cboOps = new com.digitalwave.controls.ctlQComboBox();
            this.m_txtCheck = new com.digitalwave.iCare.BIHOrder.Control.ctlFindTextBox();
            this.m_txtDoctorList = new com.digitalwave.iCare.BIHOrder.Control.ctlFindTextBox();
            this.m_txtBedNo = new com.digitalwave.iCare.BIHOrder.Control.ctlFindTextBox();
            this.m_txtArea = new com.digitalwave.iCare.BIHOrder.Control.ctlFindTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_picInfo)).BeginInit();
            this.m_MenuStripSelect.SuspendLayout();
            this.m_MenuStripREMARK.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_cboExecuteType
            // 
            this.m_cboExecuteType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboExecuteType.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboExecuteType.Location = new System.Drawing.Point(63, 10);
            this.m_cboExecuteType.Name = "m_cboExecuteType";
            this.m_cboExecuteType.Size = new System.Drawing.Size(79, 22);
            this.m_cboExecuteType.TabIndex = 7;
            this.m_cboExecuteType.SelectedIndexChanged += new System.EventHandler(this.m_cboExecuteType_SelectedIndexChanged);
            this.m_cboExecuteType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboExecuteType_KeyDown);
            // 
            // m_lblSaveOrderID
            // 
            this.m_lblSaveOrderID.AutoSize = true;
            this.m_lblSaveOrderID.Font = new System.Drawing.Font("����", 9.5F);
            this.m_lblSaveOrderID.Location = new System.Drawing.Point(274, 12);
            this.m_lblSaveOrderID.Name = "m_lblSaveOrderID";
            this.m_lblSaveOrderID.Size = new System.Drawing.Size(66, 13);
            this.m_lblSaveOrderID.TabIndex = 37;
            this.m_lblSaveOrderID.Text = "ҽ������:";
            this.m_lblSaveOrderID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 44;
            this.label3.Text = "��  ��";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtRecipeNo
            // 
            this.m_txtRecipeNo.Location = new System.Drawing.Point(51, 127);
            this.m_txtRecipeNo.Name = "m_txtRecipeNo";
            this.m_txtRecipeNo.Size = new System.Drawing.Size(87, 23);
            this.m_txtRecipeNo.TabIndex = 6;
            this.m_txtRecipeNo.TabStop = false;
            this.m_txtRecipeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtRecipeNo_KeyDown);
            this.m_txtRecipeNo.Leave += new System.EventHandler(this.m_txtRecipeNo_Leave);
            this.m_txtRecipeNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtRecipeNo_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("����", 9.5F);
            this.label13.Location = new System.Drawing.Point(1, 12);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 13);
            this.label13.TabIndex = 38;
            this.label13.Text = "ҽ������:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtExecDept
            // 
            this.m_txtExecDept.Location = new System.Drawing.Point(135, 358);
            this.m_txtExecDept.Name = "m_txtExecDept";
            this.m_txtExecDept.ReadOnly = true;
            this.m_txtExecDept.Size = new System.Drawing.Size(100, 23);
            this.m_txtExecDept.TabIndex = 69;
            // 
            // m_txtGetUnit
            // 
            this.m_txtGetUnit.Location = new System.Drawing.Point(386, 179);
            this.m_txtGetUnit.Name = "m_txtGetUnit";
            this.m_txtGetUnit.ReadOnly = true;
            this.m_txtGetUnit.Size = new System.Drawing.Size(38, 23);
            this.m_txtGetUnit.TabIndex = 21;
            this.m_txtGetUnit.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(288, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 14);
            this.label4.TabIndex = 46;
            this.label4.Text = "����";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(310, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 14);
            this.label5.TabIndex = 47;
            this.label5.Text = "�ϼ�����:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("����", 9.5F);
            this.label6.Location = new System.Drawing.Point(1, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 48;
            this.label6.Text = "ҩƷ��Դ:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblDosage
            // 
            this.m_lblDosage.AutoSize = true;
            this.m_lblDosage.Font = new System.Drawing.Font("����", 9.5F);
            this.m_lblDosage.Location = new System.Drawing.Point(472, 12);
            this.m_lblDosage.Name = "m_lblDosage";
            this.m_lblDosage.Size = new System.Drawing.Size(40, 13);
            this.m_lblDosage.TabIndex = 49;
            this.m_lblDosage.Text = "����:";
            this.m_lblDosage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lblDosage.DoubleClick += new System.EventHandler(this.m_lblDosage_DoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(644, 236);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 14);
            this.label8.TabIndex = 50;
            this.label8.Text = "���";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblExecuteFreq
            // 
            this.m_lblExecuteFreq.AutoSize = true;
            this.m_lblExecuteFreq.Font = new System.Drawing.Font("����", 9.5F);
            this.m_lblExecuteFreq.Location = new System.Drawing.Point(696, 12);
            this.m_lblExecuteFreq.Name = "m_lblExecuteFreq";
            this.m_lblExecuteFreq.Size = new System.Drawing.Size(40, 13);
            this.m_lblExecuteFreq.TabIndex = 51;
            this.m_lblExecuteFreq.Text = "Ƶ��:";
            this.m_lblExecuteFreq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lblExecuteFreq.DoubleClick += new System.EventHandler(this.m_lblExecuteFreq_DoubleClick);
            // 
            // m_lblSaveConOrderFreqID
            // 
            this.m_lblSaveConOrderFreqID.AutoSize = true;
            this.m_lblSaveConOrderFreqID.Location = new System.Drawing.Point(162, 133);
            this.m_lblSaveConOrderFreqID.Name = "m_lblSaveConOrderFreqID";
            this.m_lblSaveConOrderFreqID.Size = new System.Drawing.Size(63, 14);
            this.m_lblSaveConOrderFreqID.TabIndex = 52;
            this.m_lblSaveConOrderFreqID.Text = "����ʱ��";
            this.m_lblSaveConOrderFreqID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lblSaveConOrderFreqID.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(527, 181);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 14);
            this.label11.TabIndex = 53;
            this.label11.Text = "ͣ��ʱ��";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("����", 9.5F);
            this.label12.Location = new System.Drawing.Point(272, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 13);
            this.label12.TabIndex = 54;
            this.label12.Text = "����|˵��";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblDosageType
            // 
            this.m_lblDosageType.AutoSize = true;
            this.m_lblDosageType.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_lblDosageType.Font = new System.Drawing.Font("����", 9.5F);
            this.m_lblDosageType.Location = new System.Drawing.Point(601, 12);
            this.m_lblDosageType.Name = "m_lblDosageType";
            this.m_lblDosageType.Size = new System.Drawing.Size(40, 13);
            this.m_lblDosageType.TabIndex = 55;
            this.m_lblDosageType.Text = "�÷�:";
            this.m_lblDosageType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lblDosageType.DoubleClick += new System.EventHandler(this.m_lblDosageType_DoubleClick);
            // 
            // m_chkIsRepare
            // 
            this.m_chkIsRepare.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_chkIsRepare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkIsRepare.Location = new System.Drawing.Point(19, 237);
            this.m_chkIsRepare.Name = "m_chkIsRepare";
            this.m_chkIsRepare.Size = new System.Drawing.Size(86, 24);
            this.m_chkIsRepare.TabIndex = 22;
            this.m_chkIsRepare.TabStop = false;
            this.m_chkIsRepare.Text = "�Ƿ񲹵�:";
            this.m_chkIsRepare.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_chkIsRepare.CheckedChanged += new System.EventHandler(this.m_chkIsRepare_CheckedChanged);
            this.m_chkIsRepare.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_chkIsRepare_KeyDown);
            // 
            // m_txtOrderSpec
            // 
            this.m_txtOrderSpec.Location = new System.Drawing.Point(684, 233);
            this.m_txtOrderSpec.Name = "m_txtOrderSpec";
            this.m_txtOrderSpec.ReadOnly = true;
            this.m_txtOrderSpec.Size = new System.Drawing.Size(102, 23);
            this.m_txtOrderSpec.TabIndex = 25;
            this.m_txtOrderSpec.TabStop = false;
            this.toolTip1.SetToolTip(this.m_txtOrderSpec, "���");
            // 
            // m_txtPackage
            // 
            this.m_txtPackage.Location = new System.Drawing.Point(24, 358);
            this.m_txtPackage.Name = "m_txtPackage";
            this.m_txtPackage.ReadOnly = true;
            this.m_txtPackage.Size = new System.Drawing.Size(50, 23);
            this.m_txtPackage.TabIndex = 26;
            this.m_txtPackage.TabStop = false;
            this.toolTip1.SetToolTip(this.m_txtPackage, "��װ");
            // 
            // m_txtDosage
            // 
            this.m_txtDosage.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDosage.ForeColor = System.Drawing.Color.Red;
            this.m_txtDosage.Location = new System.Drawing.Point(507, 10);
            this.m_txtDosage.MaxLength = 6;
            this.m_txtDosage.Name = "m_txtDosage";
            this.m_txtDosage.Size = new System.Drawing.Size(56, 23);
            this.m_txtDosage.TabIndex = 10;
            this.m_txtDosage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtDosage_KeyDown);
            this.m_txtDosage.Leave += new System.EventHandler(this.m_txtDosage_Leave);
            this.m_txtDosage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_mthOnlyNumber);
            // 
            // m_txtUse
            // 
            this.m_txtUse.Location = new System.Drawing.Point(379, 233);
            this.m_txtUse.MaxLength = 9;
            this.m_txtUse.Name = "m_txtUse";
            this.m_txtUse.ReadOnly = true;
            this.m_txtUse.Size = new System.Drawing.Size(76, 23);
            this.m_txtUse.TabIndex = 18;
            this.m_txtUse.TabStop = false;
            this.m_txtUse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtUse_KeyDown);
            this.m_txtUse.Leave += new System.EventHandler(this.m_txtUse_Leave);
            this.m_txtUse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_mthOnlyNumber);
            // 
            // m_txtGet
            // 
            this.m_txtGet.ForeColor = System.Drawing.Color.Red;
            this.m_txtGet.Location = new System.Drawing.Point(322, 179);
            this.m_txtGet.MaxLength = 6;
            this.m_txtGet.Name = "m_txtGet";
            this.m_txtGet.Size = new System.Drawing.Size(61, 23);
            this.m_txtGet.TabIndex = 12;
            this.m_txtGet.TabStop = false;
            this.m_txtGet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtGet_KeyDown);
            this.m_txtGet.Leave += new System.EventHandler(this.m_txtGet_Leave);
            this.m_txtGet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_mthOnlyNumber);
            // 
            // m_cboRateType
            // 
            this.m_cboRateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboRateType.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboRateType.Location = new System.Drawing.Point(63, 41);
            this.m_cboRateType.Name = "m_cboRateType";
            this.m_cboRateType.Size = new System.Drawing.Size(79, 22);
            this.m_cboRateType.TabIndex = 14;
            this.m_cboRateType.SelectionChangeCommitted += new System.EventHandler(this.m_cboRateType_SelectionChangeCommitted);
            this.m_cboRateType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboRateType_KeyDown);
            // 
            // m_txtEntrust
            // 
            this.m_txtEntrust.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtEntrust.Location = new System.Drawing.Point(281, 158);
            this.m_txtEntrust.MaxLength = 100;
            this.m_txtEntrust.Name = "m_txtEntrust";
            this.m_txtEntrust.Size = new System.Drawing.Size(129, 23);
            this.m_txtEntrust.TabIndex = 13;
            this.m_txtEntrust.TabStop = false;
            this.m_txtEntrust.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtEntrust_KeyDown);
            // 
            // m_chkIsRich
            // 
            this.m_chkIsRich.BackColor = System.Drawing.SystemColors.Control;
            this.m_chkIsRich.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_chkIsRich.Enabled = false;
            this.m_chkIsRich.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkIsRich.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_chkIsRich.Location = new System.Drawing.Point(201, 237);
            this.m_chkIsRich.Name = "m_chkIsRich";
            this.m_chkIsRich.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_chkIsRich.Size = new System.Drawing.Size(86, 24);
            this.m_chkIsRich.TabIndex = 24;
            this.m_chkIsRich.TabStop = false;
            this.m_chkIsRich.Text = "�Ƿ����:";
            this.m_chkIsRich.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_chkIsRich.UseVisualStyleBackColor = false;
            this.m_chkIsRich.CheckedChanged += new System.EventHandler(this.m_chkIsRich_CheckedChanged);
            this.m_chkIsRich.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_chkIsRich_KeyDown);
            // 
            // m_txtUseUnit
            // 
            this.m_txtUseUnit.Location = new System.Drawing.Point(457, 233);
            this.m_txtUseUnit.Name = "m_txtUseUnit";
            this.m_txtUseUnit.ReadOnly = true;
            this.m_txtUseUnit.Size = new System.Drawing.Size(38, 23);
            this.m_txtUseUnit.TabIndex = 19;
            this.m_txtUseUnit.TabStop = false;
            // 
            // m_txtDosageUnit
            // 
            this.m_txtDosageUnit.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDosageUnit.Location = new System.Drawing.Point(562, 10);
            this.m_txtDosageUnit.Name = "m_txtDosageUnit";
            this.m_txtDosageUnit.ReadOnly = true;
            this.m_txtDosageUnit.Size = new System.Drawing.Size(38, 23);
            this.m_txtDosageUnit.TabIndex = 17;
            this.m_txtDosageUnit.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 292);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 78;
            this.label2.Text = "����ҽ��:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Visible = false;
            // 
            // m_lblBackground
            // 
            this.m_lblBackground.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblBackground.Location = new System.Drawing.Point(30, 405);
            this.m_lblBackground.Name = "m_lblBackground";
            this.m_lblBackground.Size = new System.Drawing.Size(24, 12);
            this.m_lblBackground.TabIndex = 79;
            // 
            // m_txtPrice
            // 
            this.m_txtPrice.Location = new System.Drawing.Point(892, 168);
            this.m_txtPrice.Name = "m_txtPrice";
            this.m_txtPrice.ReadOnly = true;
            this.m_txtPrice.Size = new System.Drawing.Size(56, 23);
            this.m_txtPrice.TabIndex = 27;
            this.m_txtPrice.TabStop = false;
            this.toolTip1.SetToolTip(this.m_txtPrice, "����");
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(476, 292);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 14);
            this.label16.TabIndex = 81;
            this.label16.Text = "¼��ʱ��:";
            this.label16.Visible = false;
            // 
            // m_txtInputDate
            // 
            this.m_txtInputDate.Location = new System.Drawing.Point(552, 289);
            this.m_txtInputDate.Name = "m_txtInputDate";
            this.m_txtInputDate.ReadOnly = true;
            this.m_txtInputDate.Size = new System.Drawing.Size(187, 23);
            this.m_txtInputDate.TabIndex = 34;
            this.m_txtInputDate.TabStop = false;
            this.m_txtInputDate.Visible = false;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            // 
            // m_picInfo
            // 
            this.m_picInfo.Image = ((System.Drawing.Image)(resources.GetObject("m_picInfo.Image")));
            this.m_picInfo.Location = new System.Drawing.Point(566, 325);
            this.m_picInfo.Name = "m_picInfo";
            this.m_picInfo.Size = new System.Drawing.Size(24, 24);
            this.m_picInfo.TabIndex = 101;
            this.m_picInfo.TabStop = false;
            this.toolTip1.SetToolTip(this.m_picInfo, "dfssd");
            this.m_picInfo.MouseEnter += new System.EventHandler(this.m_picInfo_MouseEnter);
            // 
            // m_txtItemTradePrice
            // 
            this.m_txtItemTradePrice.Location = new System.Drawing.Point(442, 200);
            this.m_txtItemTradePrice.Name = "m_txtItemTradePrice";
            this.m_txtItemTradePrice.ReadOnly = true;
            this.m_txtItemTradePrice.Size = new System.Drawing.Size(50, 23);
            this.m_txtItemTradePrice.TabIndex = 803;
            this.m_txtItemTradePrice.TabStop = false;
            this.toolTip1.SetToolTip(this.m_txtItemTradePrice, "������");
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // m_imgIcons
            // 
            this.m_imgIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imgIcons.ImageStream")));
            this.m_imgIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imgIcons.Images.SetKeyName(0, "");
            this.m_imgIcons.Images.SetKeyName(1, "");
            this.m_imgIcons.Images.SetKeyName(2, "");
            this.m_imgIcons.Images.SetKeyName(3, "");
            this.m_imgIcons.Images.SetKeyName(4, "");
            this.m_imgIcons.Images.SetKeyName(5, "");
            // 
            // m_cboRepare
            // 
            this.m_cboRepare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboRepare.Enabled = false;
            this.m_cboRepare.Location = new System.Drawing.Point(114, 238);
            this.m_cboRepare.Name = "m_cboRepare";
            this.m_cboRepare.Size = new System.Drawing.Size(82, 22);
            this.m_cboRepare.TabIndex = 23;
            this.m_cboRepare.TabStop = false;
            this.m_cboRepare.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboRepare_KeyDown);
            // 
            // m_chkISNEEDFEEL
            // 
            this.m_chkISNEEDFEEL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_chkISNEEDFEEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkISNEEDFEEL.Location = new System.Drawing.Point(18, 260);
            this.m_chkISNEEDFEEL.Name = "m_chkISNEEDFEEL";
            this.m_chkISNEEDFEEL.Size = new System.Drawing.Size(86, 24);
            this.m_chkISNEEDFEEL.TabIndex = 28;
            this.m_chkISNEEDFEEL.TabStop = false;
            this.m_chkISNEEDFEEL.Text = "�Ƿ�Ƥ��:";
            this.m_chkISNEEDFEEL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_chkISNEEDFEEL.Visible = false;
            this.m_chkISNEEDFEEL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_chkISNEEDFEEL_KeyDown);
            // 
            // m_txtDays
            // 
            this.m_txtDays.Location = new System.Drawing.Point(786, 10);
            this.m_txtDays.MaxLength = 2;
            this.m_txtDays.Name = "m_txtDays";
            this.m_txtDays.Size = new System.Drawing.Size(24, 23);
            this.m_txtDays.TabIndex = 13;
            this.m_txtDays.Visible = false;
            this.m_txtDays.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtDays_KeyDown);
            this.m_txtDays.Leave += new System.EventHandler(this.m_txtDays_Leave);
            this.m_txtDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_mthOnlyNumber);
            // 
            // m_lblDay
            // 
            this.m_lblDay.AutoSize = true;
            this.m_lblDay.Font = new System.Drawing.Font("����", 9.5F);
            this.m_lblDay.Location = new System.Drawing.Point(810, 12);
            this.m_lblDay.Name = "m_lblDay";
            this.m_lblDay.Size = new System.Drawing.Size(20, 13);
            this.m_lblDay.TabIndex = 54;
            this.m_lblDay.Text = "��";
            this.m_lblDay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lblDay.Visible = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(21, 292);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(70, 14);
            this.label18.TabIndex = 104;
            this.label18.Text = "����ҽ��:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label18.Visible = false;
            // 
            // m_txtISNEEDFEEL
            // 
            this.m_txtISNEEDFEEL.Location = new System.Drawing.Point(113, 262);
            this.m_txtISNEEDFEEL.Name = "m_txtISNEEDFEEL";
            this.m_txtISNEEDFEEL.ReadOnly = true;
            this.m_txtISNEEDFEEL.Size = new System.Drawing.Size(84, 23);
            this.m_txtISNEEDFEEL.TabIndex = 29;
            this.m_txtISNEEDFEEL.Visible = false;
            this.m_txtISNEEDFEEL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtISNEEDFEEL_KeyDown);
            // 
            // m_TxbStartTime
            // 
            this.m_TxbStartTime.Location = new System.Drawing.Point(272, 261);
            this.m_TxbStartTime.Name = "m_TxbStartTime";
            this.m_TxbStartTime.ReadOnly = true;
            this.m_TxbStartTime.Size = new System.Drawing.Size(187, 23);
            this.m_TxbStartTime.TabIndex = 30;
            this.m_TxbStartTime.TabStop = false;
            this.m_TxbStartTime.Visible = false;
            // 
            // m_txbFinishTime
            // 
            this.m_txbFinishTime.Location = new System.Drawing.Point(552, 263);
            this.m_txbFinishTime.Name = "m_txbFinishTime";
            this.m_txbFinishTime.ReadOnly = true;
            this.m_txbFinishTime.Size = new System.Drawing.Size(187, 23);
            this.m_txbFinishTime.TabIndex = 31;
            this.m_txbFinishTime.TabStop = false;
            this.m_txbFinishTime.Visible = false;
            // 
            // m_txtFatherOrder
            // 
            this.m_txtFatherOrder.Enabled = false;
            this.m_txtFatherOrder.Font = new System.Drawing.Font("����", 10.5F);
            this.m_txtFatherOrder.Location = new System.Drawing.Point(89, 288);
            this.m_txtFatherOrder.Name = "m_txtFatherOrder";
            this.m_txtFatherOrder.ReadOnly = true;
            this.m_txtFatherOrder.Size = new System.Drawing.Size(109, 23);
            this.m_txtFatherOrder.TabIndex = 15;
            this.m_txtFatherOrder.TabStop = false;
            this.m_txtFatherOrder.Visible = false;
            this.m_txtFatherOrder.m_evtSelectItem += new com.digitalwave.controls.EventHandler_OnSelectItem(this.m_txtFatherOrder_m_evtSelectItem);
            this.m_txtFatherOrder.m_evtFindItem += new com.digitalwave.controls.EventHandler_OnFindItem(this.m_txtFatherOrder_m_evtFindItem);
            this.m_txtFatherOrder.m_evtInitListView += new com.digitalwave.controls.EventHandler_InitListView(this.m_txtFatherOrder_m_evtInitListView);
            // 
            // m_txtOrderName
            // 
            this.m_txtOrderName.ContextMenuStrip = this.m_MenuStripSelect;
            this.m_txtOrderName.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtOrderName.Location = new System.Drawing.Point(336, 10);
            this.m_txtOrderName.MaxLength = 25;
            this.m_txtOrderName.Name = "m_txtOrderName";
            this.m_txtOrderName.Size = new System.Drawing.Size(134, 23);
            this.m_txtOrderName.TabIndex = 9;
            this.m_txtOrderName.m_evtSelectItem += new com.digitalwave.controls.EventHandler_OnSelectItem(this.m_txtOrderName_m_evtSelectItem);
            this.m_txtOrderName.m_evtFindItem += new com.digitalwave.controls.EventHandler_OnFindItem(this.m_txtOrderName_m_evtFindItem);
            this.m_txtOrderName.m_evtInitListView += new com.digitalwave.controls.EventHandler_InitListView(this.m_txtOrderName_m_evtInitListView);
            // 
            // m_MenuStripSelect
            // 
            this.m_MenuStripSelect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuCommonItems,
            this.m_mnuGroupItems,
            this.m_mnuNormalItems,
            this.m_mnuNewPriceItems});
            this.m_MenuStripSelect.Name = "m_MenuStripSelect";
            this.m_MenuStripSelect.Size = new System.Drawing.Size(125, 92);
            // 
            // m_mnuCommonItems
            // 
            this.m_mnuCommonItems.Name = "m_mnuCommonItems";
            this.m_mnuCommonItems.Size = new System.Drawing.Size(124, 22);
            this.m_mnuCommonItems.Text = "������Ŀ";
            this.m_mnuCommonItems.Click += new System.EventHandler(this.m_mnuCommonItems_Click);
            // 
            // m_mnuGroupItems
            // 
            this.m_mnuGroupItems.Name = "m_mnuGroupItems";
            this.m_mnuGroupItems.Size = new System.Drawing.Size(124, 22);
            this.m_mnuGroupItems.Text = "����ģ��";
            this.m_mnuGroupItems.Click += new System.EventHandler(this.m_mnuGroupItems_Click);
            // 
            // m_mnuNormalItems
            // 
            this.m_mnuNormalItems.Name = "m_mnuNormalItems";
            this.m_mnuNormalItems.Size = new System.Drawing.Size(124, 22);
            this.m_mnuNormalItems.Text = "��ͨ��Ŀ";
            this.m_mnuNormalItems.Click += new System.EventHandler(this.m_mnuNormalItems_Click);
            // 
            // m_mnuNewPriceItems
            // 
            this.m_mnuNewPriceItems.Name = "m_mnuNewPriceItems";
            this.m_mnuNewPriceItems.Size = new System.Drawing.Size(124, 22);
            this.m_mnuNewPriceItems.Text = "�¼���Ŀ";
            this.m_mnuNewPriceItems.Click += new System.EventHandler(this.m_mnuNewPriceItems_Click);
            // 
            // m_txtExecuteFreq
            // 
            this.m_txtExecuteFreq.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtExecuteFreq.Location = new System.Drawing.Point(731, 10);
            this.m_txtExecuteFreq.MaxLength = 20;
            this.m_txtExecuteFreq.Name = "m_txtExecuteFreq";
            this.m_txtExecuteFreq.Size = new System.Drawing.Size(54, 23);
            this.m_txtExecuteFreq.TabIndex = 12;
            this.m_txtExecuteFreq.EnabledChanged += new System.EventHandler(this.m_txtExecuteFreq_EnabledChanged);
            this.m_txtExecuteFreq.DoubleClick += new System.EventHandler(this.m_txtExecuteFreq_DoubleClick);
            this.m_txtExecuteFreq.m_evtSelectItem += new com.digitalwave.controls.EventHandler_OnSelectItem(this.m_txtExecuteFreq_m_evtSelectItem);
            this.m_txtExecuteFreq.Leave += new System.EventHandler(this.m_txtExecuteFreq_Leave);
            this.m_txtExecuteFreq.m_evtFindItem += new com.digitalwave.controls.EventHandler_OnFindItem(this.m_txtExecuteFreq_m_evtFindItem);
            this.m_txtExecuteFreq.m_evtInitListView += new com.digitalwave.controls.EventHandler_InitListView(this.m_txtExecuteFreq_m_evtInitListView);
            // 
            // m_txtDoctor
            // 
            this.m_txtDoctor.Enabled = false;
            this.m_txtDoctor.Font = new System.Drawing.Font("����", 10.5F);
            this.m_txtDoctor.Location = new System.Drawing.Point(271, 288);
            this.m_txtDoctor.MaxLength = 9;
            this.m_txtDoctor.Name = "m_txtDoctor";
            this.m_txtDoctor.ReadOnly = true;
            this.m_txtDoctor.Size = new System.Drawing.Size(190, 23);
            this.m_txtDoctor.TabIndex = 33;
            this.m_txtDoctor.Visible = false;
            this.m_txtDoctor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtDoctor_KeyDown);
            this.m_txtDoctor.m_evtSelectItem += new com.digitalwave.controls.EventHandler_OnSelectItem(this.m_txtDoctor_m_evtSelectItem);
            this.m_txtDoctor.Enter += new System.EventHandler(this.m_txtDoctor_Enter);
            this.m_txtDoctor.m_evtFindItem += new com.digitalwave.controls.EventHandler_OnFindItem(this.m_txtDoctor_m_evtFindItem);
            this.m_txtDoctor.m_evtInitListView += new com.digitalwave.controls.EventHandler_InitListView(this.m_txtDoctor_m_evtInitListView);
            // 
            // m_txtDosageType
            // 
            this.m_txtDosageType.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDosageType.Location = new System.Drawing.Point(636, 10);
            this.m_txtDosageType.MaxLength = 20;
            this.m_txtDosageType.Name = "m_txtDosageType";
            this.m_txtDosageType.Size = new System.Drawing.Size(60, 23);
            this.m_txtDosageType.TabIndex = 11;
            this.m_txtDosageType.EnabledChanged += new System.EventHandler(this.m_txtDosageType_EnabledChanged);
            this.m_txtDosageType.DoubleClick += new System.EventHandler(this.m_txtDosageType_DoubleClick);
            this.m_txtDosageType.m_evtSelectItem += new com.digitalwave.controls.EventHandler_OnSelectItem(this.m_txtDosageType_m_evtSelectItem);
            this.m_txtDosageType.m_evtFindItem += new com.digitalwave.controls.EventHandler_OnFindItem(this.m_txtDosageType_m_evtFindItem);
            this.m_txtDosageType.m_evtInitListView += new com.digitalwave.controls.EventHandler_InitListView(this.m_txtDosageType_m_evtInitListView);
            // 
            // m_dtStartTime
            // 
            this.m_dtStartTime.CustomFormat = "yyyy��MM��dd�� HHʱmm��";
            this.m_dtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtStartTime.Location = new System.Drawing.Point(30, 317);
            this.m_dtStartTime.Name = "m_dtStartTime";
            this.m_dtStartTime.Size = new System.Drawing.Size(187, 23);
            this.m_dtStartTime.TabIndex = 107;
            this.m_dtStartTime.TabStop = false;
            // 
            // m_dtFinishTime
            // 
            this.m_dtFinishTime.CustomFormat = "yyyy��MM��dd�� HHʱmm��";
            this.m_dtFinishTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtFinishTime.Location = new System.Drawing.Point(240, 317);
            this.m_dtFinishTime.Name = "m_dtFinishTime";
            this.m_dtFinishTime.Size = new System.Drawing.Size(187, 23);
            this.m_dtFinishTime.TabIndex = 107;
            this.m_dtFinishTime.TabStop = false;
            this.m_dtFinishTime.Leave += new System.EventHandler(this.m_dtFinishTime_Leave);
            // 
            // m_lblxmclsa
            // 
            this.m_lblxmclsa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblxmclsa.Font = new System.Drawing.Font("����", 9F);
            this.m_lblxmclsa.Location = new System.Drawing.Point(702, 325);
            this.m_lblxmclsa.Name = "m_lblxmclsa";
            this.m_lblxmclsa.Size = new System.Drawing.Size(42, 20);
            this.m_lblxmclsa.TabIndex = 108;
            this.m_lblxmclsa.Text = "����";
            this.m_lblxmclsa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_chkIsMedicare
            // 
            this.m_chkIsMedicare.Enabled = false;
            this.m_chkIsMedicare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkIsMedicare.Location = new System.Drawing.Point(479, 325);
            this.m_chkIsMedicare.Name = "m_chkIsMedicare";
            this.m_chkIsMedicare.Size = new System.Drawing.Size(60, 24);
            this.m_chkIsMedicare.TabIndex = 109;
            this.m_chkIsMedicare.TabStop = false;
            this.m_chkIsMedicare.Text = "ҽ��";
            this.m_chkIsMedicare.CheckedChanged += new System.EventHandler(this.m_chkIsMedicare_CheckedChanged);
            // 
            // m_txtSample
            // 
            this.m_txtSample.Font = new System.Drawing.Font("����", 10.5F);
            this.m_txtSample.Location = new System.Drawing.Point(196, 210);
            this.m_txtSample.MaxLength = 10;
            this.m_txtSample.Name = "m_txtSample";
            this.m_txtSample.Size = new System.Drawing.Size(60, 23);
            this.m_txtSample.TabIndex = 11;
            this.m_txtSample.Tag = "13";
            this.m_txtSample.Visible = false;
            this.m_txtSample.DoubleClick += new System.EventHandler(this.m_txtSample_DoubleClick);
            this.m_txtSample.m_evtSelectItem += new com.digitalwave.controls.EventHandler_OnSelectItem(this.m_txtSample_m_evtSelectItem);
            this.m_txtSample.m_evtFindItem += new com.digitalwave.controls.EventHandler_OnFindItem(this.m_txtSample_m_evtFindItem);
            this.m_txtSample.m_evtInitListView += new com.digitalwave.controls.EventHandler_InitListView(this.m_txtSample_m_evtInitListView);
            // 
            // m_lblSample
            // 
            this.m_lblSample.AutoSize = true;
            this.m_lblSample.Font = new System.Drawing.Font("����", 10.5F);
            this.m_lblSample.Location = new System.Drawing.Point(150, 216);
            this.m_lblSample.Name = "m_lblSample";
            this.m_lblSample.Size = new System.Drawing.Size(35, 14);
            this.m_lblSample.TabIndex = 111;
            this.m_lblSample.Text = "����";
            this.m_lblSample.Visible = false;
            // 
            // m_lblCheck
            // 
            this.m_lblCheck.AutoSize = true;
            this.m_lblCheck.Font = new System.Drawing.Font("����", 10.5F);
            this.m_lblCheck.Location = new System.Drawing.Point(148, 186);
            this.m_lblCheck.Name = "m_lblCheck";
            this.m_lblCheck.Size = new System.Drawing.Size(35, 14);
            this.m_lblCheck.TabIndex = 113;
            this.m_lblCheck.Text = "��λ";
            this.m_lblCheck.Visible = false;
            // 
            // m_txtOrderName2
            // 
            this.m_txtOrderName2.Location = new System.Drawing.Point(508, 233);
            this.m_txtOrderName2.MaxLength = 50;
            this.m_txtOrderName2.Name = "m_txtOrderName2";
            this.m_txtOrderName2.Size = new System.Drawing.Size(116, 23);
            this.m_txtOrderName2.TabIndex = 8;
            this.m_txtOrderName2.TabStop = false;
            this.m_txtOrderName2.DoubleClick += new System.EventHandler(this.m_txtOrderName2_DoubleClick);
            this.m_txtOrderName2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtOrderName2_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(458, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 120;
            this.label1.Text = "����ҽ��";
            this.label1.Visible = false;
            // 
            // m_btnBedList
            // 
            this.m_btnBedList.Location = new System.Drawing.Point(254, 154);
            this.m_btnBedList.Name = "m_btnBedList";
            this.m_btnBedList.Size = new System.Drawing.Size(19, 23);
            this.m_btnBedList.TabIndex = 3;
            this.m_btnBedList.Text = "��";
            this.m_btnBedList.UseVisualStyleBackColor = true;
            this.m_btnBedList.Click += new System.EventHandler(this.m_btnBedList_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label19.Location = new System.Drawing.Point(157, 159);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(35, 14);
            this.label19.TabIndex = 117;
            this.label19.Text = "����";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label10.Location = new System.Drawing.Point(11, 159);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 14);
            this.label10.TabIndex = 118;
            this.label10.Text = "����";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox1
            // 
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox1.Location = new System.Drawing.Point(597, 325);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 24);
            this.checkBox1.TabIndex = 122;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "�Ƿ񲹴�:";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(491, 182);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 14);
            this.label14.TabIndex = 125;
            this.label14.Text = "����";
            // 
            // m_txtATTACHTIMES_INT
            // 
            this.m_txtATTACHTIMES_INT.Location = new System.Drawing.Point(526, 178);
            this.m_txtATTACHTIMES_INT.MaxLength = 1;
            this.m_txtATTACHTIMES_INT.Name = "m_txtATTACHTIMES_INT";
            this.m_txtATTACHTIMES_INT.Size = new System.Drawing.Size(30, 23);
            this.m_txtATTACHTIMES_INT.TabIndex = 14;
            this.m_txtATTACHTIMES_INT.TabStop = false;
            this.m_txtATTACHTIMES_INT.Text = "0";
            this.m_txtATTACHTIMES_INT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtATTACHTIMES_INT_KeyDown);
            this.m_txtATTACHTIMES_INT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_mthOnlyNumber);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(950, 172);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(21, 14);
            this.label20.TabIndex = 127;
            this.label20.Text = "Ԫ";
            // 
            // m_txtMedicareType
            // 
            this.m_txtMedicareType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_txtMedicareType.Font = new System.Drawing.Font("����", 9F);
            this.m_txtMedicareType.ForeColor = System.Drawing.Color.Red;
            this.m_txtMedicareType.Location = new System.Drawing.Point(850, 263);
            this.m_txtMedicareType.Name = "m_txtMedicareType";
            this.m_txtMedicareType.Size = new System.Drawing.Size(56, 20);
            this.m_txtMedicareType.TabIndex = 131;
            this.m_txtMedicareType.Text = "       ";
            this.m_txtMedicareType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(811, 266);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(35, 14);
            this.label22.TabIndex = 132;
            this.label22.Text = "ҽ��";
            // 
            // m_txtGetUnit2
            // 
            this.m_txtGetUnit2.Location = new System.Drawing.Point(291, 201);
            this.m_txtGetUnit2.Name = "m_txtGetUnit2";
            this.m_txtGetUnit2.ReadOnly = true;
            this.m_txtGetUnit2.Size = new System.Drawing.Size(38, 23);
            this.m_txtGetUnit2.TabIndex = 133;
            this.m_txtGetUnit2.TabStop = false;
            // 
            // m_txtGet2
            // 
            this.m_txtGet2.Location = new System.Drawing.Point(442, 179);
            this.m_txtGet2.MaxLength = 6;
            this.m_txtGet2.Name = "m_txtGet2";
            this.m_txtGet2.ReadOnly = true;
            this.m_txtGet2.Size = new System.Drawing.Size(41, 23);
            this.m_txtGet2.TabIndex = 134;
            this.m_txtGet2.Visible = false;
            // 
            // m_dtFinishTime2
            // 
            this.m_dtFinishTime2.BackColor = System.Drawing.SystemColors.HighlightText;
            this.m_dtFinishTime2.Location = new System.Drawing.Point(593, 177);
            this.m_dtFinishTime2.Mask = "yyyy��MM��dd��HHʱmm��";
            this.m_dtFinishTime2.Name = "m_dtFinishTime2";
            this.m_dtFinishTime2.Size = new System.Drawing.Size(188, 23);
            this.m_dtFinishTime2.TabIndex = 19;
            this.m_dtFinishTime2.TabStop = false;
            this.m_dtFinishTime2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtFinishTime2_KeyDown);
            this.m_dtFinishTime2.Leave += new System.EventHandler(this.m_dtFinishTime2_Leave);
            // 
            // m_dtStartTime2
            // 
            this.m_dtStartTime2.BackColor = System.Drawing.SystemColors.HighlightText;
            this.m_dtStartTime2.Location = new System.Drawing.Point(235, 129);
            this.m_dtStartTime2.Mask = "yyyy��MM��dd��HHʱmm��";
            this.m_dtStartTime2.Name = "m_dtStartTime2";
            this.m_dtStartTime2.Size = new System.Drawing.Size(175, 23);
            this.m_dtStartTime2.TabIndex = 15;
            this.m_dtStartTime2.TabStop = false;
            this.m_dtStartTime2.Visible = false;
            this.m_dtStartTime2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtStartTime2_KeyDown);
            // 
            // m_cobOrderCate
            // 
            this.m_cobOrderCate.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.m_cobOrderCate.Location = new System.Drawing.Point(14, 183);
            this.m_cobOrderCate.Name = "m_cobOrderCate";
            this.m_cobOrderCate.Size = new System.Drawing.Size(109, 22);
            this.m_cobOrderCate.TabIndex = 800;
            this.m_cobOrderCate.TabStop = false;
            this.m_cobOrderCate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cobOrderCate_KeyDown);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("����", 9.5F);
            this.label17.Location = new System.Drawing.Point(157, 14);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(40, 13);
            this.label17.TabIndex = 136;
            this.label17.Text = "����:";
            // 
            // m_txtOrderCate
            // 
            this.m_txtOrderCate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtOrderCate.Location = new System.Drawing.Point(192, 11);
            this.m_txtOrderCate.MaxLength = 20;
            this.m_txtOrderCate.Name = "m_txtOrderCate";
            this.m_txtOrderCate.Size = new System.Drawing.Size(79, 23);
            this.m_txtOrderCate.TabIndex = 8;
            this.m_txtOrderCate.DoubleClick += new System.EventHandler(this.m_txtOrderCate_DoubleClick);
            this.m_txtOrderCate.m_evtSelectItem += new com.digitalwave.controls.EventHandler_OnSelectItem(this.m_txtOrderCate_m_evtSelectItem);
            this.m_txtOrderCate.m_evtFindItem += new com.digitalwave.controls.EventHandler_OnFindItem(this.m_txtOrderCate_m_evtFindItem);
            this.m_txtOrderCate.m_evtInitListView += new com.digitalwave.controls.EventHandler_InitListView(this.m_txtOrderCate_m_evtInitListView);
            // 
            // m_txtREMARK_VCHR
            // 
            this.m_txtREMARK_VCHR.ContextMenuStrip = this.m_MenuStripREMARK;
            this.m_txtREMARK_VCHR.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtREMARK_VCHR.Location = new System.Drawing.Point(336, 40);
            this.m_txtREMARK_VCHR.MaxLength = 100;
            this.m_txtREMARK_VCHR.Name = "m_txtREMARK_VCHR";
            this.m_txtREMARK_VCHR.Size = new System.Drawing.Size(360, 23);
            this.m_txtREMARK_VCHR.TabIndex = 15;
            this.m_txtREMARK_VCHR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtREMARK_VCHR_KeyDown);
            // 
            // m_MenuStripREMARK
            // 
            this.m_MenuStripREMARK.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuNormalRemark,
            this.m_mnuNormalAdd});
            this.m_MenuStripREMARK.Name = "m_MenuStripREMARK";
            this.m_MenuStripREMARK.Size = new System.Drawing.Size(125, 48);
            // 
            // m_mnuNormalRemark
            // 
            this.m_mnuNormalRemark.Name = "m_mnuNormalRemark";
            this.m_mnuNormalRemark.Size = new System.Drawing.Size(124, 22);
            this.m_mnuNormalRemark.Text = "����˵��";
            this.m_mnuNormalRemark.Click += new System.EventHandler(this.m_mnuNormalRemark_Click);
            // 
            // m_mnuNormalAdd
            // 
            this.m_mnuNormalAdd.Name = "m_mnuNormalAdd";
            this.m_mnuNormalAdd.Size = new System.Drawing.Size(124, 22);
            this.m_mnuNormalAdd.Text = "��������";
            this.m_mnuNormalAdd.Click += new System.EventHandler(this.m_mnuNormalAdd_Click);
            // 
            // m_hideDosage
            // 
            this.m_hideDosage.BackColor = System.Drawing.SystemColors.Control;
            this.m_hideDosage.Enabled = false;
            this.m_hideDosage.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_hideDosage.ForeColor = System.Drawing.Color.Red;
            this.m_hideDosage.Location = new System.Drawing.Point(461, 124);
            this.m_hideDosage.MaxLength = 6;
            this.m_hideDosage.Name = "m_hideDosage";
            this.m_hideDosage.Size = new System.Drawing.Size(56, 23);
            this.m_hideDosage.TabIndex = 139;
            this.m_hideDosage.Visible = false;
            // 
            // m_hideDosageUnit
            // 
            this.m_hideDosageUnit.Enabled = false;
            this.m_hideDosageUnit.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_hideDosageUnit.Location = new System.Drawing.Point(523, 124);
            this.m_hideDosageUnit.Name = "m_hideDosageUnit";
            this.m_hideDosageUnit.ReadOnly = true;
            this.m_hideDosageUnit.Size = new System.Drawing.Size(38, 23);
            this.m_hideDosageUnit.TabIndex = 140;
            this.m_hideDosageUnit.TabStop = false;
            this.m_hideDosageUnit.Visible = false;
            // 
            // m_hideDosageType
            // 
            this.m_hideDosageType.BackColor = System.Drawing.SystemColors.Control;
            this.m_hideDosageType.Enabled = false;
            this.m_hideDosageType.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_hideDosageType.Location = new System.Drawing.Point(564, 124);
            this.m_hideDosageType.MaxLength = 20;
            this.m_hideDosageType.Name = "m_hideDosageType";
            this.m_hideDosageType.Size = new System.Drawing.Size(60, 23);
            this.m_hideDosageType.TabIndex = 141;
            this.m_hideDosageType.Visible = false;
            // 
            // m_hideExecuteFreq
            // 
            this.m_hideExecuteFreq.BackColor = System.Drawing.SystemColors.Control;
            this.m_hideExecuteFreq.Enabled = false;
            this.m_hideExecuteFreq.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_hideExecuteFreq.Location = new System.Drawing.Point(637, 124);
            this.m_hideExecuteFreq.MaxLength = 20;
            this.m_hideExecuteFreq.Name = "m_hideExecuteFreq";
            this.m_hideExecuteFreq.Size = new System.Drawing.Size(54, 23);
            this.m_hideExecuteFreq.TabIndex = 142;
            this.m_hideExecuteFreq.Visible = false;
            // 
            // m_hideDays
            // 
            this.m_hideDays.BackColor = System.Drawing.SystemColors.Control;
            this.m_hideDays.Enabled = false;
            this.m_hideDays.Location = new System.Drawing.Point(698, 124);
            this.m_hideDays.MaxLength = 2;
            this.m_hideDays.Name = "m_hideDays";
            this.m_hideDays.Size = new System.Drawing.Size(24, 23);
            this.m_hideDays.TabIndex = 143;
            this.m_hideDays.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.label7.Font = new System.Drawing.Font("����", 9.5F);
            this.label7.Location = new System.Drawing.Point(144, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 801;
            this.label7.Text = "��Ӧ֢:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboShiying
            // 
            this.cboShiying.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShiying.Enabled = false;
            this.cboShiying.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboShiying.Items.AddRange(new object[] {
            "2����",
            "3������"});
            this.cboShiying.Location = new System.Drawing.Point(192, 41);
            this.cboShiying.Name = "cboShiying";
            this.cboShiying.Size = new System.Drawing.Size(79, 22);
            this.cboShiying.TabIndex = 802;
            this.cboShiying.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboShiying_KeyDown);
            // 
            // lblKJ
            // 
            this.lblKJ.AutoSize = true;
            this.lblKJ.Font = new System.Drawing.Font("����", 9.5F);
            this.lblKJ.Location = new System.Drawing.Point(157, 72);
            this.lblKJ.Name = "lblKJ";
            this.lblKJ.Size = new System.Drawing.Size(40, 13);
            this.lblKJ.TabIndex = 804;
            this.lblKJ.Text = "��;:";
            this.lblKJ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboKJ
            // 
            this.cboKJ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKJ.FormattingEnabled = true;
            this.cboKJ.Items.AddRange(new object[] {
            "",
            "������ҩ",
            "Ԥ����ҩ",
            "Ԥ�����"});
            this.cboKJ.Location = new System.Drawing.Point(192, 70);
            this.cboKJ.Name = "cboKJ";
            this.cboKJ.Size = new System.Drawing.Size(79, 22);
            this.cboKJ.TabIndex = 16;
            this.cboKJ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboKJ_KeyDown);
            // 
            // cboQK
            // 
            this.cboQK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQK.FormattingEnabled = true;
            this.cboQK.Items.AddRange(new object[] {
            "",
            "I���п�",
            "II���п�",
            "III���п�",
            "����"});
            this.cboQK.Location = new System.Drawing.Point(336, 70);
            this.cboQK.Name = "cboQK";
            this.cboQK.Size = new System.Drawing.Size(134, 22);
            this.cboQK.TabIndex = 805;
            this.cboQK.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboQK_KeyDown);
            // 
            // lblQK
            // 
            this.lblQK.AutoSize = true;
            this.lblQK.Font = new System.Drawing.Font("����", 9.5F);
            this.lblQK.Location = new System.Drawing.Point(274, 72);
            this.lblQK.Name = "lblQK";
            this.lblQK.Size = new System.Drawing.Size(66, 13);
            this.lblQK.TabIndex = 806;
            this.lblQK.Text = "�пڷ���:";
            this.lblQK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("����", 9.5F);
            this.label9.Location = new System.Drawing.Point(1, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 807;
            this.label9.Text = "��ͷҽ��:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("����", 9.5F);
            this.label15.Location = new System.Drawing.Point(116, 116);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(20, 13);
            this.label15.TabIndex = 809;
            this.label15.Text = "��";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("����", 9.5F);
            this.label21.Location = new System.Drawing.Point(472, 72);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(66, 13);
            this.label21.TabIndex = 810;
            this.label21.Text = "Ժ�����:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboProxyBoil
            // 
            this.cboProxyBoil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProxyBoil.FormattingEnabled = true;
            this.cboProxyBoil.ItemHeight = 14;
            this.cboProxyBoil.Items.AddRange(new object[] {
            "",
            "�������",
            "��ҩ����"});
            this.cboProxyBoil.Location = new System.Drawing.Point(536, 70);
            this.cboProxyBoil.Name = "cboProxyBoil";
            this.cboProxyBoil.Size = new System.Drawing.Size(80, 22);
            this.cboProxyBoil.TabIndex = 811;
            this.cboProxyBoil.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboProxyBoil_KeyDown);
            // 
            // cboEmer
            // 
            this.cboEmer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmer.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboEmer.Items.AddRange(new object[] {
            "",
            "��"});
            this.cboEmer.Location = new System.Drawing.Point(731, 40);
            this.cboEmer.Name = "cboEmer";
            this.cboEmer.Size = new System.Drawing.Size(54, 22);
            this.cboEmer.TabIndex = 812;
            this.cboEmer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboEmer_KeyDown);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("����", 9.5F);
            this.label23.Location = new System.Drawing.Point(696, 43);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(40, 13);
            this.label23.TabIndex = 813;
            this.label23.Text = "����:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboOps
            // 
            this.cboOps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOps.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboOps.Items.AddRange(new object[] {
            "",
            "��"});
            this.cboOps.Location = new System.Drawing.Point(63, 70);
            this.cboOps.Name = "cboOps";
            this.cboOps.Size = new System.Drawing.Size(79, 22);
            this.cboOps.TabIndex = 814;
            this.cboOps.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboOps_KeyDown);
            // 
            // m_txtCheck
            // 
            this.m_txtCheck.Font = new System.Drawing.Font("����", 10.5F);
            this.m_txtCheck.Location = new System.Drawing.Point(196, 179);
            this.m_txtCheck.MaxLength = 10;
            this.m_txtCheck.Name = "m_txtCheck";
            this.m_txtCheck.Size = new System.Drawing.Size(60, 23);
            this.m_txtCheck.TabIndex = 11;
            this.m_txtCheck.Visible = false;
            this.m_txtCheck.DoubleClick += new System.EventHandler(this.m_txtCheck_DoubleClick);
            this.m_txtCheck.m_evtSelectItem += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_OnSelectItem(this.m_txtCheck_m_evtSelectItem);
            this.m_txtCheck.m_evtFindItem += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_OnFindItem(this.m_txtCheck_m_evtFindItem);
            this.m_txtCheck.m_evtInitListView += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_InitListView(this.m_txtCheck_m_evtInitListView);
            // 
            // m_txtDoctorList
            // 
            this.m_txtDoctorList.Location = new System.Drawing.Point(527, 152);
            this.m_txtDoctorList.Name = "m_txtDoctorList";
            this.m_txtDoctorList.Size = new System.Drawing.Size(227, 23);
            this.m_txtDoctorList.TabIndex = 16;
            this.m_txtDoctorList.TabStop = false;
            this.m_txtDoctorList.Visible = false;
            this.m_txtDoctorList.m_evtSelectItem += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_OnSelectItem(this.m_txtDoctorList_m_evtSelectItem);
            this.m_txtDoctorList.m_evtFindItem += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_OnFindItem(this.m_txtDoctorList_m_evtFindItem);
            this.m_txtDoctorList.m_evtInitListView += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_InitListView(this.m_txtDoctorList_m_evtInitListView);
            // 
            // m_txtBedNo
            // 
            this.m_txtBedNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtBedNo.Location = new System.Drawing.Point(191, 155);
            this.m_txtBedNo.Name = "m_txtBedNo";
            this.m_txtBedNo.Size = new System.Drawing.Size(63, 23);
            this.m_txtBedNo.TabIndex = 2;
            this.m_txtBedNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBedNo_KeyDown);
            this.m_txtBedNo.DoubleClick += new System.EventHandler(this.m_txtBedNo_DoubleClick);
            this.m_txtBedNo.m_evtSelectItem += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_OnSelectItem(this.m_txtBedNo2_m_evtSelectItem);
            this.m_txtBedNo.m_evtInitListView += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_InitListView(this.m_txtBedNo2_m_evtInitListView);
            // 
            // m_txtArea
            // 
            this.m_txtArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtArea.Location = new System.Drawing.Point(47, 155);
            this.m_txtArea.Name = "m_txtArea";
            this.m_txtArea.Size = new System.Drawing.Size(108, 23);
            this.m_txtArea.TabIndex = 1;
            this.m_txtArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtArea_KeyDown);
            this.m_txtArea.DoubleClick += new System.EventHandler(this.m_txtArea_DoubleClick);
            this.m_txtArea.m_evtSelectItem += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_OnSelectItem(this.m_txtArea_m_evtSelectItem);
            this.m_txtArea.Leave += new System.EventHandler(this.m_txtArea_Leave);
            this.m_txtArea.m_evtInitListView += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_InitListView(this.m_txtArea_m_evtInitListView);
            // 
            // ctlBIHOrderDetail
            // 
            this.Controls.Add(this.cboOps);
            this.Controls.Add(this.cboEmer);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.cboProxyBoil);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.m_txtDosageUnit);
            this.Controls.Add(this.m_txtDosage);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cboKJ);
            this.Controls.Add(this.m_cboRateType);
            this.Controls.Add(this.m_cboExecuteType);
            this.Controls.Add(this.cboQK);
            this.Controls.Add(this.m_txtItemTradePrice);
            this.Controls.Add(this.lblQK);
            this.Controls.Add(this.lblKJ);
            this.Controls.Add(this.cboShiying);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.m_hideDays);
            this.Controls.Add(this.m_hideExecuteFreq);
            this.Controls.Add(this.m_hideDosageUnit);
            this.Controls.Add(this.m_hideDosage);
            this.Controls.Add(this.m_hideDosageType);
            this.Controls.Add(this.m_txtREMARK_VCHR);
            this.Controls.Add(this.m_txtOrderCate);
            this.Controls.Add(this.m_txtCheck);
            this.Controls.Add(this.m_txtSample);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.m_txtGet2);
            this.Controls.Add(this.m_cobOrderCate);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_lblDosage);
            this.Controls.Add(this.m_txtGetUnit2);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.m_txtMedicareType);
            this.Controls.Add(this.m_dtFinishTime2);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.m_dtStartTime2);
            this.Controls.Add(this.m_txtATTACHTIMES_INT);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.m_txtDoctorList);
            this.Controls.Add(this.m_btnBedList);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.m_txtBedNo);
            this.Controls.Add(this.m_txtArea);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_txtDosageType);
            this.Controls.Add(this.m_txtOrderName2);
            this.Controls.Add(this.m_lblCheck);
            this.Controls.Add(this.m_picInfo);
            this.Controls.Add(this.m_lblDosageType);
            this.Controls.Add(this.m_dtStartTime);
            this.Controls.Add(this.m_lblSample);
            this.Controls.Add(this.m_txtEntrust);
            this.Controls.Add(this.m_lblxmclsa);
            this.Controls.Add(this.m_chkIsMedicare);
            this.Controls.Add(this.m_dtFinishTime);
            this.Controls.Add(this.m_txbFinishTime);
            this.Controls.Add(this.m_TxbStartTime);
            this.Controls.Add(this.m_txtDoctor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_txtExecuteFreq);
            this.Controls.Add(this.m_txtOrderName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.m_txtFatherOrder);
            this.Controls.Add(this.m_txtISNEEDFEEL);
            this.Controls.Add(this.m_lblSaveConOrderFreqID);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.m_cboRepare);
            this.Controls.Add(this.m_txtInputDate);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.m_txtPrice);
            this.Controls.Add(this.m_txtGetUnit);
            this.Controls.Add(this.m_txtGet);
            this.Controls.Add(this.m_txtPackage);
            this.Controls.Add(this.m_txtUse);
            this.Controls.Add(this.m_txtUseUnit);
            this.Controls.Add(this.m_txtRecipeNo);
            this.Controls.Add(this.m_chkIsRepare);
            this.Controls.Add(this.m_txtOrderSpec);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_lblExecuteFreq);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.m_chkIsRich);
            this.Controls.Add(this.m_lblSaveOrderID);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.m_chkISNEEDFEEL);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_txtDays);
            this.Controls.Add(this.m_lblDay);
            this.Controls.Add(this.m_lblBackground);
            this.Controls.Add(this.m_txtExecDept);
            this.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ctlBIHOrderDetail";
            this.Size = new System.Drawing.Size(875, 101);
            this.Load += new System.EventHandler(this.ctlBIHOrderDetail_Load);
            this.Resize += new System.EventHandler(this.ctlBIHOrderDetail_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.m_picInfo)).EndInit();
            this.m_MenuStripSelect.ResumeLayout(false);
            this.m_MenuStripREMARK.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region ���
        /// <summary>
        /// �����������
        /// </summary>
        public void EmptyInput()
        {
            #region ��տؼ�
            //ҽ������:
            if (((frmBIHOrderInput)this.ParentForm).m_strView.Equals("0") || ((frmBIHOrderInput)this.ParentForm).m_strView.Equals("3"))
            {
                m_cboExecuteType.Enabled = true;
            }
            else
            {
                m_cboExecuteType.Enabled = false;
            }
            //����
            m_txtRecipeNo.Enabled = true;
            //ҽ������:
            //			m_cboExecuteType.m_blnFindItem("1");
            //m_cboExecuteType_SelectedIndexChanged(null,null);
            //m_cboExecuteType.Enabled=true;
            //ҽ������:
            m_txtOrderName.Text = "";
            m_txtOrderName.Tag = "";
            m_txtOrderName.Enabled = true;
            m_txtOrderName.ReadOnly = false;
            m_txtOrderName.Text = "";
            m_txtOrderName.Tag = "";
            m_txtOrderName2.Text = "";
            m_txtOrderName2.Tag = "";
            m_txtOrderName2.Enabled = true;
            m_lblSaveOrderID.Tag = "";
            //ҽ�����
            this.m_chkIsMedicare.Checked = false;
            this.m_lblxmclsa.Text = "";
            this.m_lblxmclsa.Visible = false;
            //���鷽��:
            //m_txtRecipeNo.Text="";
            //ִ��Ƶ��
            m_txtExecuteFreq.Tag = "";
            m_txtExecuteFreq.Text = "";
            m_txtExecuteFreq.Enabled = true;
            m_txtExecuteFreq.ReadOnly = false;
            //��ҩ��ʽ
            m_txtDosageType.Tag = "";
            m_txtDosageType.Text = "";
            m_txtDosageType.Enabled = true;
            m_txtDosageType.ReadOnly = false;
            //ҩƷ���:
            m_txtOrderSpec.Text = "";
            //��װ
            m_txtPackage.Text = "";
            m_txtPackage.Tag = 0;
            //סԺ����
            m_txtPrice.Text = "";
            m_txtPrice.Tag = 1;
            //���ñ�־:
            m_cboRateType.m_blnFindItem("0");
            m_cboRateType.Enabled = true;
            //��Ժ��ҩ����
            m_txtDays.Text = "";
            m_txtDays.Enabled = true;
            m_lblDay.Text = "��";
            //m_txtDays.ReadOnly =true;
            //һ�μ���:
            m_txtDosage.Text = "";
            m_txtDosageUnit.Text = "";
            m_txtDosage.Enabled = true;
            m_txtDosage.ReadOnly = false;
            //һ������
            m_txtUse.Text = "";
            m_txtUseUnit.Text = "";
            //һ������
            m_txtGetUnit.Text = "";
            m_txtGetUnit2.Text = "";
            m_txtGet.Text = "";
            m_txtGet2.Text = "";
            m_txtGet.Enabled = true;
            m_txtGet.ReadOnly = false;
            //����		
            m_chkIsRich.Checked = false;
            //����
            m_chkIsRepare.Checked = false;
            m_cboRepare.SelectedIndex = -1;
            m_cboRepare.Enabled = false;
            //��ʼʱ��:ֹͣʱ��
            m_TxbStartTime.Text = "";
            m_txbFinishTime.Text = "";
            m_dtStartTime.Visible = false;
            m_dtFinishTime.Visible = false;
            m_dtStartTime2.Enabled = true;
            m_dtStartTime2.BackColor = Color.White;
            m_dtStartTime2.ReadOnly = false;
            m_dtFinishTime2.Enabled = true;
            m_dtFinishTime2.BackColor = Color.White;
            m_dtFinishTime2.ReadOnly = false;
            //Ƥ��
            m_chkISNEEDFEEL.Checked = false;
            m_chkISNEEDFEEL.Enabled = true;
            m_txtISNEEDFEEL.Text = "";
            m_chkISNEEDFEEL.Tag = null;		//����ҩƷҽ������ID
            m_txtISNEEDFEEL.Enabled = false;
            //ҽ������:
            m_txtEntrust.Text = "";
            m_txtEntrust.Enabled = true;
            //¼��ʱ��
            m_txtInputDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            //ҽ��
            m_txtDoctor.Tag = "";
            m_txtDoctor.Text = "";
            m_txtDoctor.Enabled = true;
            m_txtDoctorList.Tag = "";
            m_txtDoctorList.Text = "";
            m_txtDoctorList.Enabled = true;
            m_txtDoctorList.ReadOnly = false;
            m_txtDoctorList.BackColor = Color.White;
            //����ҽ��
            m_txtFatherOrder.Text = "";
            m_txtFatherOrder.Tag = "";
            //ͼ��
            m_picInfo.Tag = "";
            m_txtExecDept.Tag = "";
            m_txtExecDept.Text = "";
            //˵��
            m_txtREMARK_VCHR.Text = "";
            m_txtREMARK_VCHR.Enabled = true;
            m_txtREMARK_VCHR.BackColor = Color.White;
            // ����ֵ���ؼ���ʼ��
            m_txtSample.Visible = false;
            m_lblSample.Visible = false;
            m_lblCheck.Visible = false;
            m_txtCheck.Visible = false;
            m_lblDosageType.Visible = true;
            m_txtDosageType.Visible = true;
            m_txtCheck.Text = "";
            m_txtCheck.Tag = "";
            m_txtSample.Text = "";
            m_txtSample.Tag = "";
            //�������÷���Ƶ�ʵ��ڸǳ�ʼ��
            m_hideDosage.Visible = false;
            m_hideDosageUnit.Visible = false;
            m_hideDosageType.Visible = false;
            m_hideExecuteFreq.Visible = false;
            this.cboShiying.SelectedIndex = 0;

            #region ����ҩ��;
            this.cboKJ.SelectedIndex = 0;
            this.cboKJ.Enabled = false;
            this.cboQK.SelectedIndex = 0;
            this.cboQK.Enabled = false;
            #endregion

            // Ԥ��ҩ
            //this.txtCureDays.Text = string.Empty;
            //this.txtCureDays.Enabled = false;

            // ���ʹ���
            this.cboProxyBoil.SelectedIndex = 0;
            this.cboProxyBoil.Enabled = false;

            // ����
            this.cboEmer.SelectedIndex = 0;
            this.cboEmer.Enabled = false;

            this.cboOps.SelectedIndex = 0;

            #endregion
        }
        #endregion
        #region ListViewTextBox����[ҽ�����ơ�ִ��Ƶ�ʡ���ҩ��ʽ������ҽ����¼��ҽ��]
        #region ҽ������
        private void m_txtOrderName_m_evtInitListView(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Width = 690;
            lvwList.Height = 160;
            lvwList.HeaderStyle = ColumnHeaderStyle.Clickable;
            lvwList.SmallImageList = this.m_imgIcons;
            //�����ͷ
            lvwList.Columns.Add("��    ��", 80, HorizontalAlignment.Center);
            lvwList.Columns.Add("��    ��", 240, HorizontalAlignment.Left);
            lvwList.Columns.Add("��    ��", 150, HorizontalAlignment.Left);
            lvwList.Columns.Add("��    װ", 120, HorizontalAlignment.Left);
            lvwList.Columns.Add("סԺ����", 80, HorizontalAlignment.Right);
        }

        private void m_txtOrderName_m_evtFindItem(object sender, string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            //ҽ������
            string m_strORDERCATEID_CHR = "";
            m_strORDERCATEID_CHR = (string)m_txtOrderCate.Tag;
            //����ҽ����ȡ����
            clsT_aid_bih_ordercate_VO p_objItem;
            p_objItem = (clsT_aid_bih_ordercate_VO)((frmBIHOrderInput)this.ParentForm).m_htOrderCate[m_strORDERCATEID_CHR];
            if (p_objItem != null && p_objItem.m_intAUTOSHOW_INT == 1)
            {

            }
            else if (strFindCode.Trim().Equals(""))
            {
                return;
            }

            if (strFindCode.Trim().StartsWith(@"/"))
            {
                //������Ŀ
                m_lngCommonItems(strFindCode.TrimStart(@"/".ToCharArray()));

            }
            else if (strFindCode.Trim().StartsWith(@"\"))
            {
                //����
                m_lngGroupItems(strFindCode.TrimStart(@"\".ToCharArray()));

            }
            else if (strFindCode.Trim().StartsWith(@"?"))
            {
                //�¼۱�
                m_lngNewPriceItems(strFindCode.TrimStart(@"?".ToCharArray()));

                /*<======================================*/
            }
            else //һ����Ŀ��ѯ
            {
                m_lngNormalItems(strFindCode.Trim());

            }
        }

        /// <summary>
        /// ������Ŀ�б���
        /// </summary>
        /// <param name="arrDic"></param>
        public void m_OrderDicListView(clsBIHOrderDic[] arrDic, DataSet m_dsDicChargeSet)
        {
            ArrayList m_arlItems = new ArrayList();
            int m_intClass = ((frmBIHOrderInput)this.ParentForm).seachClass.SelectedIndex;
            //ҽ������
            string m_strORDERCATEID_CHR = "";
            m_strORDERCATEID_CHR = (string)m_txtOrderCate.Tag;

            if (arrDic.Length > 0)
            {
                for (int i = 0; i < arrDic.Length; i++)
                {
                    string strDicCode = arrDic[i].m_strUserCode;
                    switch (m_intClass)
                    {
                        case -1:
                            strDicCode = arrDic[i].m_strPYCode;
                            break;
                        case 0:
                            strDicCode = arrDic[i].m_strPYCode;
                            break;
                        case 1:
                            strDicCode = arrDic[i].m_strWBCode;
                            break;
                        case 2:
                            strDicCode = arrDic[i].m_strName;
                            break;
                        case 3:
                            strDicCode = arrDic[i].m_strUserCode;
                            break;
                    }
                    //�û�����
                    ListViewItem objItem = new ListViewItem((i + 1).ToString(), c_intItem_Order);
                    objItem.SubItems.Add(arrDic[i].m_strUserCode);
                    //objItem.SubItems.Add(arrDic[i].m_strUserCode);
                    objItem.SubItems.Add(strDicCode);
                    //��Ŀ����(��������
                    objItem.SubItems.Add(arrDic[i].m_strName);
                    //��Ŀ����(ҩƷ��������)
                    objItem.SubItems.Add(arrDic[i].m_strITEMCOMMNAME_VCHR);
                    //��Ŀ���
                    objItem.SubItems.Add(arrDic[i].m_strSpec);
                    //��װ
                    //objItem.SubItems.Add(arrDic[i].m_StrPackage);
                    //סԺ����
                    //objItem.SubItems.Add(arrDic[i].m_dmlPrice.ToString("0.0000"));
                    //��λ
                    //objItem.SubItems.Add(arrDic[i].m_strDosageUnit);
                    //ҩ����ʾ
                    if (arrDic[i].m_intITEMSRCTYPE_INT == 1 && arrDic[i].m_intIPNOQTYFLAG_INT == 1)
                    {
                        objItem.SubItems.Add("ȱҩ");
                        objItem.ForeColor = Color.Red;
                    }
                    else
                    {
                        //ҽ����Ŀ->��ɫ
                        if (arrDic[i].m_strYBTypeID.Trim() != "")
                        {
                            objItem.SubItems.Add("ҽ����");
                            objItem.ForeColor = Color.Green;
                        }
                        else
                        {
                            objItem.SubItems.Add("");
                        }
                    }
                    //����
                    objItem.SubItems.Add(arrDic[i].m_strMEDICINEPREPTYPENAME_VCHR);

                    objItem.Tag = arrDic[i];
                    m_arlItems.Add(objItem);
                }
            }
            ListViewItem[] arrItem = new ListViewItem[0];
            if (m_arlItems.Count > 0)
            {
                arrItem = (ListViewItem[])(m_arlItems.ToArray(typeof(ListViewItem)));
            }
            //lvwList.Visible = false;
            frmChargeList m_frmCList = new frmChargeList(this.m_txtOrderName.Text, m_strORDERCATEID_CHR, ((frmBIHOrderInput)this.ParentForm).m_blLessMedControl, m_intClass, ((frmBIHOrderInput)this.ParentForm).m_htOrderCate, ((frmBIHOrderInput)this.ParentForm).m_blStopControl, ((frmBIHOrderInput)this.ParentForm).m_blDeableMedControl, ((frmBIHOrderInput)this.ParentForm).IsChildPrice);
            m_frmCList.m_strMedDeptId = ((frmBIHOrderInput)this.ParentForm).m_strMedDeptGross;
            m_frmCList.strPayType = ((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_objPatient.m_strPayTypeID;            
            if (m_intClass == 3)
            {
                m_frmCList.m_lvwList.Columns[1].Width = 0;
            }
            m_frmCList.arrItem = arrItem;
            if (m_dsDicChargeSet != null && m_dsDicChargeSet.Tables.Count > 0)
            {
                m_frmCList.m_dsDicChargeSet = m_dsDicChargeSet;
            }
            m_frmCList.WindowState = FormWindowState.Maximized;
            DialogResult result = m_frmCList.ShowDialog(this);
            try
            {

                if (result == DialogResult.OK)
                {
                    //��¼ҩƷ�����
                    this.m_fotOpcurrentgross_num = m_frmCList.m_dmlOpcurrentgross_num;
                    //��¼¼ҩƷ���� 1ΪҩƷ 2Ϊ����
                    this.m_intITEMSRCTYPE_INT = m_frmCList.m_intITEMSRCTYPE_INT;
                    m_frmCList.Hide();
                    m_txtOrderName_m_evtSelectItem(null, m_frmCList.m_lviSItem);

                    if (!string.IsNullOrEmpty(m_frmCList.syzRemark))
                    {
                        // �޶�������������ҽԺ
                        if (m_frmCList.syzRemark.Trim() == "�޶�������������ҽԺ" || m_frmCList.syzRemark.Trim() == "�޶�������ҽԺ")
                        {
                            this.cboShiying.Text = "3������";
                        }
                    }
                }
                else if (result == DialogResult.Yes)//������ҽ�� ---> 2018.11.23 ���ںܶ�׶ˣ�������ٴ�ҽʦ�Ƿ����ã�����
                {
                    m_frmCList.Hide();
                    this.Cursor = Cursors.WaitCursor;
                    for (int i = 0; i < m_frmCList.m_lviSItemArr.Length; i++)
                    {
                        clsBIHOrderDic m_objOrderDic = (clsBIHOrderDic)(m_frmCList.m_lviSItemArr[i].Tag);
                        clsBIHOrder objOrder = m_objGetOrderByOrderDic(((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_objPatient, m_objOrderDic);
                        //��¼ҩƷ�����
                        this.m_fotOpcurrentgross_num = m_frmCList.m_dmlOpcurrentgross_num;
                        //��¼¼ҩƷ���� 1ΪҩƷ 2Ϊ����
                        this.m_intITEMSRCTYPE_INT = m_frmCList.m_intITEMSRCTYPE_INT;
                        objOrder.m_intCHARGE_INT = -1;//���⴦��
                        if (IsSubOrder == true)//��ҽ������
                        {
                            //����ҽ����������״̬�£�������ҽ����
                            ((frmBIHOrderInput)this.ParentForm).m_objDomain.lngAddNewSubOrder(ref objOrder);
                        }
                        else
                        {
                            //����ҽ����������״̬�£���������ҽ����
                            ((frmBIHOrderInput)this.ParentForm).m_objDomain.lngAddNewNOSubOrder(ref objOrder);
                        }
                    }
                    this.Cursor = Cursors.Default;
                    ((frmBIHOrderInput)this.ParentForm).cmdRefurbish_Click(null, null);
                }
                else
                {
                    m_frmCList.Hide();
                    return;
                }
            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message, "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void m_txtOrderName_m_evtSelectItem(object sender, System.Windows.Forms.ListViewItem lviSelected)
        {
            m_blSampleItem = false;
            m_blCheckItem = false;
            /*<===================================*/
            if (lviSelected != null)
            {
                if (lviSelected.Tag == null) return;

                if (lviSelected.Tag is clsBIHOrderDic)
                {
                    clsBIHOrderDic m_objDicItem = (clsBIHOrderDic)lviSelected.Tag;
                    if (!m_htMEDICINEPREPTYPE.ContainsKey(m_objDicItem.m_strOrderDicID))
                    {
                        m_htMEDICINEPREPTYPE.Add(m_objDicItem.m_strOrderDicID, m_objDicItem.m_strMEDICINEPREPTYPENAME_VCHR);
                    }
                    m_mthShowOrderDic(m_objDicItem);
                    // ��������߼�(ҽ�������߼�)
                    ExecuteType_SelectedIndexChanged();
                }
            }

            RateTypeOldIndex = m_cboRateType.SelectedIndex;
            setTheControlOrder("m_txtOrderName");
        }

        /// <summary>
        /// ҽ������������Ŀ�������
        /// </summary>
        /// <param name="m_objDicItem"></param>
        public void OrderCateViewControl(clsBIHOrderDic m_objDicItem)
        {
            clsT_aid_bih_ordercate_VO p_objItem;
            p_objItem = (clsT_aid_bih_ordercate_VO)((frmBIHOrderInput)this.ParentForm).m_htOrderCate[m_objDicItem.m_strOrderCateID];
            OrdercateLogic(p_objItem);

            if (m_objDicItem.m_intCheckTypeID != -1 && m_objDicItem.m_strCheckType.Trim().Length > 0 && m_objDicItem.m_intDELETED == 0)
            {
                m_blCheckItem = true;
            }
            else
            {
                m_blCheckItem = false;
            }
            //�¼���������߼�--�������뵥Ԫ�߼�
            if (!m_objDicItem.m_strLISAPPLYUNITID_CHR.Trim().Equals("") && m_objDicItem.m_strOrderCateID.Trim().Equals(((frmBIHOrderInput)this.ParentForm).m_objSpecateVo.m_strORDERCATEID_LIS_CHR.Trim()))
            {
                m_blSampleItem = true;
            }
            else
            {
                m_blSampleItem = false;
            }
            if (((frmBIHOrderInput)this.ParentForm).m_objSpecateVo.m_strCONFREQID_CHR.Trim().Equals(m_objDicItem.m_strFREQID_CHR))
            {
                m_txtDosageControl(false);
                m_txtDosageTypeControl(false);

            }
            //��ҩ������ʾ(�������ؼ��Ŀ�����ʾ�������ֶ�)
            if (p_objItem.m_strORDERCATEID_CHR.Equals(((frmBIHOrderInput)this.ParentForm).m_objSpecateVo.m_strMID_MEDICINE_CHR))
            {
                m_txtMid_MedicineControl(true);
            }
            /*<============================================*/
            //-------------------------->����ҩ�÷�����
            this.m_intMEDICNETYPE_INT = m_objDicItem.m_intMEDICNETYPE_INT;
            /*<===================================*/
            //ҩƷ��Ϣ������ʾ
            if (((frmBIHOrderInput)this.ParentForm).IsShowCodexRemarkFrm)
            {
                string Remark = m_objDicItem.m_strMedcineREMARK;
                if (!Remark.Equals(""))
                {
                    int ShowCodexRemarkFrmTimerinterval = ((frmBIHOrderInput)this.ParentForm).ShowCodexRemarkFrmTimerinterval;
                    frmCodexRemark frmRemark = new frmCodexRemark(Remark, ShowCodexRemarkFrmTimerinterval);
                    frmRemark.StartPosition = FormStartPosition.CenterScreen;
                    frmRemark.Show();
                }
            }
            // �������
            setTheSampleBox();
            // ������
            setTheCheckBox();
        }

        private void SubChangeControl()
        {
            if (this.IsSubOrder)
            {
                if (this.ParentOrder != null)
                {
                    // Ƶ��
                    m_txtExecuteFreq.Tag = ParentOrder.m_strExecFreqID;
                    m_txtExecuteFreq.Text = ParentOrder.m_strExecFreqName;

                    m_txtDosageType.Text = ParentOrder.m_strDosetypeName;
                    m_txtDosageType.Tag = ParentOrder.m_strDosetypeID;

                    m_txtExecuteFreq.Enabled = false;
                    m_txtExecuteFreq.ReadOnly = true;
                    m_txtDosageType.Enabled = false;
                    m_txtDosageType.ReadOnly = true;

                    m_txtDays.Text = ParentOrder.m_intOUTGETMEDDAYS_INT.ToString();
                }
            }
        }
        /// <summary>
        /// �������  TRUE����ʾ�������룬FALSE����������
        /// </summary>
        public void setTheSampleBox()
        {
            if (m_blSampleItem)
            {
                //m_txtSample_m_evtFindItem(null, "", new ListView());
                m_lblDosageType.Visible = false;
                m_txtDosageType.Visible = false;
                m_lblCheck.Visible = false;
                m_txtCheck.Visible = false;
                m_lblSample.Location = m_lblDosageType.Location;
                m_txtSample.Location = m_txtDosageType.Location;
                m_hideDosageType.Visible = false;
                m_lblSample.Visible = true;
                m_txtSample.Visible = true;
            }
            else
            {

                m_lblSample.Visible = false;
                m_txtSample.Visible = false;

            }
        }

        /// <summary>
        /// ������  TRUE����ʾ�������룬FALSE����������
        /// </summary>
        public void setTheCheckBox()
        {
            if (m_blCheckItem)
            {
                //m_txtCheck_m_evtFindItem(null, "", new ListView());
                m_lblDosageType.Visible = false;
                m_txtDosageType.Visible = false;
                m_lblSample.Visible = false;
                m_txtSample.Visible = false;
                m_lblCheck.Location = m_lblDosageType.Location;
                m_txtCheck.Location = m_txtDosageType.Location;
                m_hideDosageType.Visible = false;
                m_lblCheck.Visible = true;
                m_txtCheck.Visible = true;

            }
            else
            {

                m_lblCheck.Visible = false;
                m_txtCheck.Visible = false;

            }
            if (!m_blCheckItem && !m_blSampleItem)
            {
                m_lblDosageType.Visible = true;
                m_txtDosageType.Visible = true;
            }
        }

        /// <summary>
        /// ��ʾ������Ŀ��Ϣ
        ///		ҵ��˵����
        ///			1��ѡ����ת������Ժ��ֻ������ʱҽ����
        /// </summary>
        /// <param name="objDic"></param>
        private void m_mthShowOrderDic(clsBIHOrderDic objDic)
        {
            if (objDic == null) return;
            m_blnCurrentItemIsGroup = false;
            m_objCurrentDic = objDic;
            //Ƶ��VO,�÷�VO
            clsAIDRecipeFreq m_objTempFreq = GetFreqVoByFreqID(objDic.m_strFREQID_CHR);
            clsBSEUsageType m_objUsage = GetUsageVoByUsageID(objDic.m_strUsageID_chr);
            /*<============================*/
            // Ƶ��
            if (!IsSubOrder)
            {
                m_txtExecuteFreq.Tag = m_objTempFreq.m_strFreqID;
                m_txtExecuteFreq.Text = m_objTempFreq.m_strFreqName;
                //Ĭ�ϸ�ҩ����
                m_txtDosageType.Tag = m_objUsage.m_strUsageID;
                m_txtDosageType.Text = m_objUsage.m_strUsageName;
            }
            /*<====================*/

            // ����
            if (objDic.m_intITEMSRCTYPE_INT == 1)//�����ҩ(����ҩ�������,�ͱ���ԭҵֵ����)
            {
                if (m_intAge >= 12)
                {
                    m_txtDosage.Text = objDic.m_decADULTDOSAGE_DEC.ToString();
                }
                else
                {
                    m_txtDosage.Text = objDic.m_decCHILDDOSAGE_DEC.ToString();
                }

            }
            else
            {
                m_txtDosage.Text = objDic.m_dmlDosageRate.ToString();
            }
            //��"0"ֵ
            if (m_txtDosage.Text.ToString().Trim().Equals("0"))
            {
                m_txtDosage.Text = "";
            }
            /*<========================*/
            m_picInfo.Tag = objDic.m_strOrderCateID;//ҽ������ID
            m_txtOrderName2.Tag = objDic.m_strOrderDicID;
            m_txtOrderName.Text = objDic.m_strName;
            m_txtOrderName.Tag = objDic;
            /*<=======================================*/
            m_txtOrderSpec.Text = objDic.m_strSpec;//��Ŀ���			
            m_txtPackage.Text = objDic.m_dmlPACKQTY_DEC.ToString();//��װ��   //objDic.m_StrPackage;//��װ=[���� ������λ / סԺ��λ]
            m_txtPackage.Tag = objDic.m_intIPCHARGEFLG_INT;//סԺ�շѵ�λ 0 ��������λ 1����С��λ
            m_txtPrice.Text = clsConverter.ToString(objDic.m_dmlPrice);//סԺ����
            m_txtPrice.Tag = objDic.m_dmlDosageRate;//����
            m_txtItemTradePrice.Text = clsConverter.ToString(objDic.m_dmlTradePrice);//סԺ�������� 
            m_txtDosageUnit.Text = objDic.m_strDosageUnit.Trim();
            if (objDic.m_intITEMSRCTYPE_INT == 1)//�����ҩ
            {
                if (objDic.m_intIPCHARGEFLG_INT == 1)
                {
                    m_txtUseUnit.Text = objDic.m_strUseUnit.Trim();
                    m_txtGetUnit.Text = objDic.m_strUseUnit.Trim();
                    m_txtGetUnit2.Text = objDic.m_strUseUnit.Trim();
                }
                else
                {
                    m_txtUseUnit.Text = objDic.m_strITEMOPUNIT_CHR;
                    m_txtGetUnit.Text = objDic.m_strITEMOPUNIT_CHR;
                    m_txtGetUnit2.Text = objDic.m_strITEMOPUNIT_CHR;
                }
            }
            else
            {
                m_txtUseUnit.Text = objDic.m_strITEMUNIT_CHR;
                m_txtGetUnit.Text = objDic.m_strITEMUNIT_CHR;
                m_txtGetUnit2.Text = objDic.m_strITEMUNIT_CHR;
            }
            m_chkIsRich.Checked = (objDic.m_intIsRich == 1);
            m_txtExecDept.Text = objDic.m_strExecDept;	//��������,����ʾ��ȡ
            m_txtExecDept.Tag = objDic.m_strExecDept;
            // ��Ӽ�����Ʒ���ؼ�
            m_txtSample.Tag = objDic.m_strSAMPLEID_VCHR.ToString().Trim();
            m_txtSample.Text = objDic.m_strSAMPLE_NAME.ToString().Trim();
            /*<==============================================*/
            //  ��Ӽ�鲿λ���ؼ�
            m_txtCheck.Tag = objDic.m_strPARTID_VCHR.ToString().Trim();
            m_txtCheck.Text = objDic.m_strPART_NAME.ToString().Trim();
            /*<==============================================*/
            // ���ҽ����Ϣ
            m_txtMedicareType.Text = objDic.m_strMedicareTypeName.ToString().Trim();
            m_txtMedicareType.Tag = objDic.m_strMedicareTypeID.ToString().Trim();
            /*<=========================================*/
            if (objDic.m_intHYPE_INT == 1)
            {
                m_chkISNEEDFEEL.Checked = true;
            }
            else
            {
                m_chkISNEEDFEEL.Checked = false;
            }
            /*<================================*/

            #region ����ҩ��Ԥ��ҩ����ҩ��Ƭ
            //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            if ((new weCare.Proxy.ProxyIP()).Service.IsAutiMed(objDic.m_strChargeItemID))
            {
                this.cboKJ.Enabled = true;
                this.cboQK.Enabled = true;
            }
            else
            {
                this.cboKJ.Enabled = false;
                this.cboQK.Enabled = false;
            }

            // ҩƷ.���
            //if (svc.IsMedInjection(objDic.m_strChargeItemID, 2) && m_cboExecuteType.Text.Contains("����"))
            //{
            //    this.txtCureDays.Enabled = true;
            //    this.txtCureDays.BackColor = Color.White;
            //}
            //else
            //{
            //    this.txtCureDays.Enabled = false;
            //    this.txtCureDays.BackColor = this.label9.BackColor;
            //}
            this.cboEmer.Enabled = (objDic.m_strOrderCateID == "03" ? true : false);

            // ��ҩ.��Ƭ
            if ((new weCare.Proxy.ProxyIP()).Service.IsMedPieces(objDic.m_strChargeItemID))
            {
                if (!IsSubOrder)
                {
                    clsBSEUsageType[] usageArr = null;
                    (new weCare.Proxy.ProxyIP()).Service.m_lngGetUsageType("70", out usageArr);
                    if (usageArr != null && usageArr.Length > 0)
                    {
                        this.m_txtDosageType.Text = usageArr[0].m_strUsageName;
                        this.m_txtDosageType.Tag = usageArr[0].m_strUsageID;
                    }
                }
                this.cboProxyBoil.Enabled = true;
                this.InputOrder();
                // this.cboProxyBoil.SelectedIndex = (m_cboExecuteType.Text.Contains("��ҩ") ? 0 : 1);
            }
            else
            {
                this.cboProxyBoil.Enabled = false;
                this.cboProxyBoil.SelectedIndex = 0;
            }

            //svc = null;
            #endregion

            m_mthRefreshTipInfo();
        }
        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="objGroup"></param>
        private void m_mthShowOrderGroup(clsBIHOrderGroup objGroup)
        {
            if (objGroup == null) return;
            m_blnCurrentItemIsGroup = true;
            m_objCurrentGroup = objGroup;

            m_txtOrderName.Tag = objGroup;
            m_txtOrderName.Text = objGroup.m_strName;
            m_txtOrderSpec.Text = objGroup.m_strDes;
            m_mthRefreshTipInfo();
            m_txtExecuteFreq.Focus();
        }
        #endregion
        #region ִ��Ƶ��
        private void m_txtExecuteFreq_m_evtInitListView(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Columns.Clear();
            ColumnHeader ch1 = lvwList.Columns.Add("No", 60, HorizontalAlignment.Left);
            ColumnHeader ch2 = lvwList.Columns.Add("Name", 100, HorizontalAlignment.Left);
            lvwList.Width = 180;
        }
        private void m_txtExecuteFreq_m_evtFindItem(object sender, string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            // ��ȡ��ʱҽ������ID 
            clsAIDRecipeFreq[] arrFreq;
            m_lngGetRecipeFreq(strFindCode, out arrFreq);
            long ret = 1;
            string m_strOldFreq = "";
            if (m_txtExecuteFreq.Tag != null)
            {
                m_strOldFreq = m_txtExecuteFreq.Tag.ToString().Trim();
            }
            if ((ret > 0) && (arrFreq != null) && (arrFreq.Length > 0))
            {
                string m_strCONFREQID_CHR = ((frmBIHOrderInput)this.ParentForm).m_objSpecateVo.m_strCONFREQID_CHR.Trim();
                for (int i = 0; i < arrFreq.Length; i++)
                {
                    //����Ĭ�ϴ�������������Ƶ��,������ǽ����˵����������÷�
                    if (m_strOldFreq.Equals(m_strCONFREQID_CHR))
                    {
                    }
                    else if (arrFreq[i].m_strFreqID.Trim().Equals(m_strCONFREQID_CHR))
                    {
                        continue;
                    }
                    /*<===================*/
                    ListViewItem lvi = lvwList.Items.Add(arrFreq[i].m_strUserCode);
                    lvi.SubItems.Add(arrFreq[i].m_strFreqName);
                    lvi.Tag = arrFreq[i];

                    //����Ƿ��Ѿ���ѡ�õ�	{Text��Vlue����}
                    if (m_txtExecuteFreq.Tag != null && m_txtExecuteFreq.Tag.ToString() != null)
                    {
                        if (m_txtExecuteFreq.Text.Trim() == arrFreq[i].m_strFreqName.Trim() && m_txtExecuteFreq.Tag.ToString().Trim() == arrFreq[i].m_strFreqID.Trim())
                        {
                            lvwList.Items.Clear();
                            lvi = lvwList.Items.Add(arrFreq[i].m_strUserCode);
                            lvi.SubItems.Add(arrFreq[i].m_strFreqName);
                            lvi.Tag = arrFreq[i];
                            //����ת��
                            m_txtDosageType.Focus();
                            return;
                        }
                    }
                }
                m_txtExecuteFreq.Tag = null;
            }
            else
            {
                MessageBox.Show("û���ҵ���Ӧ��ִ��Ƶ�ʣ������������Ĳ�ѯ����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_txtExecuteFreq.Tag = "";
                m_txtExecuteFreq.SelectAll();
                m_txtExecuteFreq.Focus();
            }
        }



        private void m_txtExecuteFreq_m_evtSelectItem(object sender, System.Windows.Forms.ListViewItem lviSelected)
        {
            if (lviSelected != null && lviSelected.Tag is clsAIDRecipeFreq)
            {
                //��ʱҽ��ʱ��Ƶ�ʱ���Ϊonce
                if (m_cboExecuteType.SelectedIndex == 0)//��������
                {
                    m_objTempFreq = lviSelected.Tag as clsAIDRecipeFreq;//����.				

                    //������ҽ��
                    //DisplayDateTimePicker(m_objTempFreq.m_strFreqID,0);
                    m_txtExecuteFreq.Text = m_objTempFreq.m_strFreqName;
                    m_txtExecuteFreq.Tag = m_objTempFreq.m_strFreqID;
                    // ����ҽ��ִ��ʱ��
                    m_txtEntrust.Text = m_objTempFreq.m_strLExecTime;
                }
                else
                {
                    //��Ժ��ҩ����ʱҽ��,��Ƶ�ʿ��Ա༭,����Ƶ�ʾ���once�Ҳ����Ա༭;
                    m_txtExecuteFreq.Text = lviSelected.SubItems[1].Text;
                    m_txtExecuteFreq.Tag = (lviSelected.Tag as clsAIDRecipeFreq).m_strFreqID;
                    m_objTempFreq = lviSelected.Tag as clsAIDRecipeFreq;//����.
                    // ��ʱҽ��ִ��ʱ��
                    m_txtEntrust.Text = m_objTempFreq.m_strTExecTime;

                }
                setTheControlOrder("m_txtExecuteFreq");
            }
        }

        /// <summary>
        /// ����ִ��Ƶ��ΪOnce
        /// </summary>
        private void m_mthSetOnceFreq()
        {
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            //��ȡ��ʱҽ������ID
            string strTemOrderRecipefreqID = new clsDcl_ExecuteOrder().m_strGetTemOrderRecipefreqID();
            if (m_objOnceFreq == null)
            {
                long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetRecipeFreqByID(strTemOrderRecipefreqID, out m_objOnceFreq);
            }
            if (m_objOnceFreq == null) return;
            m_txtExecuteFreq.Text = m_objOnceFreq.m_strFreqName;
            m_txtExecuteFreq.Tag = m_objOnceFreq.m_strFreqID;
            m_objTempFreq = m_objOnceFreq;

            //Ƶ�ʸı�,������������
            if (m_txtUse.Text.Trim() != "")
            {
                decimal dmlUse = clsConverter.ToDecimal(m_txtUse.Text);
                decimal dmlGet = m_dmlComputeGet(dmlUse);
                m_txtGet.Text = clsConverter.ToString(dmlGet);
            }
        }
        #endregion
        #region ��ҩ��ʽ
        private void m_txtDosageType_m_evtFindItem(object sender, string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            clsBSEUsageType[] arrType;
            m_lngGetUsageType(strFindCode, out arrType, this.m_intMEDICNETYPE_INT);
            long ret = 1;
            if ((ret > 0) && (arrType != null) && (arrType.Length > 0))
            {
                for (int i = 0; i < arrType.Length; i++)
                {
                    ListViewItem lvi = lvwList.Items.Add(arrType[i].m_strUserCode);
                    lvi.SubItems.Add(arrType[i].m_strUsageName);
                    lvi.Tag = arrType[i];
                    //����Ƿ��Ѿ���ѡ�õ�	{Text��Vlue����}
                    #region
                    if (m_txtDosageType.Tag != null && m_txtDosageType.Tag.ToString() != null)
                    {
                        if (m_txtDosageType.Text.Trim() == arrType[i].m_strUsageName.Trim() && m_txtDosageType.Tag.ToString().Trim() == arrType[i].m_strUsageID.Trim())
                        {
                            lvwList.Items.Clear();
                            lvi = lvwList.Items.Add(arrType[i].m_strUserCode);
                            lvi.SubItems.Add(arrType[i].m_strUsageName);
                            lvi.Tag = arrType[i];
                            //m_cboRateType.Focus();
                            return;
                        }
                    }
                    #endregion
                }
                m_txtDosageType.Tag = null;
            }
            else
            {
                MessageBox.Show("û���ҵ���Ӧ�ĸ�ҩ��ʽ�������������Ĳ�ѯ����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //m_txtDosageType.Tag ="";
                //m_txtDosageType.SelectAll();
                //m_txtDosageType.Focus();
            }
        }

        /// <summary>
        /// ��ȡ�������˵��÷�VO����
        /// </summary>
        /// <param name="strFindCode"></param>
        /// <param name="arrType"></param>
        /// <param name="p"></param>
        public void m_lngGetUsageType(string strFindCode, out clsBSEUsageType[] arrType, int MEDICNETYPE_INT)
        {
            arrType = null;
            if (m_arrUsage == null)
            {
                //clsBIHOrderService m_objService = clsGenerator.CreateObject(typeof(clsBIHOrderService)) as clsBIHOrderService;
                //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetUsageType("", out  m_arrUsage);
            }
            ArrayList UsageType = new ArrayList();
            bool m_blCan = false;
            bool m_blScope = false;
            for (int i = 0; i < m_arrUsage.Length; i++)
            {
                m_blCan = false;
                m_blScope = false;
                switch (MEDICNETYPE_INT)
                {
                    case 1:
                        if (m_arrUsage[i].m_intSCOPE_INT == 0 || m_arrUsage[i].m_intSCOPE_INT == 1)
                        {
                            m_blScope = true;
                        }
                        break;
                    case 2:
                        if (m_arrUsage[i].m_intSCOPE_INT == 0 || m_arrUsage[i].m_intSCOPE_INT == 2)
                        {
                            m_blScope = true;
                        }
                        break;
                    default:
                        m_blScope = true;
                        break;
                }
                if (m_blScope == false)
                {
                    continue;
                }
                if (m_arrUsage[i].m_strUserCode.ToUpper().Contains(strFindCode.Trim().ToUpper()) || m_arrUsage[i].m_strUsageName.ToUpper().Contains(strFindCode.Trim().ToUpper()) || m_arrUsage[i].m_strPYCODE_VCHR.ToUpper().Contains(strFindCode.Trim().ToUpper()) || m_arrUsage[i].m_strWBCODE_VCHR.ToUpper().Contains(strFindCode.Trim().ToUpper()))
                {
                    m_blCan = true;
                }
                if (m_blCan == true)
                {
                    UsageType.Add(m_arrUsage[i]);
                }
            }
            if (UsageType.Count > 0)
            {
                arrType = (clsBSEUsageType[])(UsageType.ToArray(typeof(clsBSEUsageType)));

            }

        }

        /// <summary>
        /// ��ȡ�������˵�Ƶ��VO����
        /// </summary>
        /// <param name="strFindCode"></param>
        /// <param name="arrFreq"></param>
        public void m_lngGetRecipeFreq(string strFindCode, out clsAIDRecipeFreq[] arrFreq)
        {
            arrFreq = null;
            if (m_arrFreq == null)
            {
                //clsBIHOrderService m_objService = clsGenerator.CreateObject(typeof(clsBIHOrderService)) as clsBIHOrderService;
                //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                (new weCare.Proxy.ProxyIP()).Service.m_lngGetRecipeFreq("", out m_arrFreq);
            }
            ArrayList FreqType = new ArrayList();
            bool m_blCan = false;
            for (int i = 0; i < m_arrFreq.Length; i++)
            {
                m_blCan = false;

                if (m_arrFreq[i].m_strUserCode.ToUpper().Contains(strFindCode.Trim().ToUpper()) || m_arrFreq[i].m_strFreqName.ToUpper().Contains(strFindCode.Trim().ToUpper()))
                {
                    m_blCan = true;
                }
                if (m_blCan == true)
                {
                    FreqType.Add(m_arrFreq[i]);
                }
            }
            if (FreqType.Count > 0)
            {
                arrFreq = (clsAIDRecipeFreq[])(FreqType.ToArray(typeof(clsAIDRecipeFreq)));

            }

        }

        private void m_txtDosageType_m_evtInitListView(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Columns.Clear();
            ColumnHeader ch1 = lvwList.Columns.Add("No", 50, HorizontalAlignment.Left);
            ColumnHeader ch2 = lvwList.Columns.Add("Name", 160, HorizontalAlignment.Left);
            lvwList.Width = 230;
        }

        private void m_txtDosageType_m_evtSelectItem(object sender, System.Windows.Forms.ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                m_txtDosageType.Text = lviSelected.SubItems[1].Text;
                m_txtDosageType.Tag = (lviSelected.Tag as clsBSEUsageType).m_strUsageID;

                /*=================>*/
                setTheControlOrder("m_txtDosageType");
                /*<============================*/
            }

        }
        private void m_txtDosageType_EnabledChanged(object sender, System.EventArgs e)
        {
            if (m_txtDosageType.Enabled)
                m_txtDosageType.BackColor = Color.White;
            else
                m_txtDosageType.BackColor = SystemColors.Control;
        }
        #endregion
        #region ����ҽ��
        private void m_txtFatherOrder_m_evtInitListView(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Width = 455;
            lvwList.Height = 150;
            lvwList.HeaderStyle = ColumnHeaderStyle.Clickable;
            lvwList.SmallImageList = this.m_imgIcons;
            //�����ͷ
            lvwList.Columns.Add("  ����", 80, HorizontalAlignment.Left);
            lvwList.Columns.Add("    ����", 120, HorizontalAlignment.Left);
            lvwList.Columns.Add("    ҽ������", 120, HorizontalAlignment.Left);
            lvwList.Columns.Add("    �÷�", 150, HorizontalAlignment.Left);
            lvwList.Columns.Add("    ִ��Ƶ��", 100, HorizontalAlignment.Left);
        }

        private void m_txtFatherOrder_m_evtFindItem(object sender, string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            //���׽�ֹ���Ƕ��:���һ��ҽ������������ҽ������ҽ��,������Ϊ��ҽ��.(��ѡ��ҽ���б���в���ʾ)
            lvwList.Items.Clear();
            if (m_strPatientID_Chr == string.Empty || m_strRegisterID == string.Empty) return;
            clsBIHOrder[] arrOrder;
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderByPatient(m_strRegisterID, m_strPatientID_Chr, out arrOrder );
            if ((ret > 0) && (arrOrder != null) && (arrOrder.Length > 0))
            {
                ListViewItem objItem;
                for (int i = 0; i < arrOrder.Length; i++)
                {
                    if (arrOrder[i].m_intStatus == 0 || arrOrder[i].m_intStatus == 7)//ֻ���½�״̬��ҽ����������Ϊ��ҽ��
                    {
                        objItem = new ListViewItem(arrOrder[i].m_intRecipenNo.ToString());//����
                        if (arrOrder[i].m_intExecuteType == 1)//����
                            objItem.SubItems.Add("����ҽ��");
                        else if (arrOrder[i].m_intExecuteType == 2)
                            objItem.SubItems.Add("��ʱҽ��");
                        else
                            objItem.SubItems.Add("");
                        objItem.SubItems.Add(arrOrder[i].m_strName);//ҽ������
                        objItem.SubItems.Add(arrOrder[i].m_strDosetypeName);//�÷�
                        objItem.SubItems.Add(arrOrder[i].m_strExecFreqName);//ִ��Ƶ��
                        objItem.Tag = arrOrder[i];
                        lvwList.Items.Add(objItem);
                    }
                }
            }
            else
            {
                //���û��ֵ�򣬱���û�в鵽�������ת����
                if (m_txtFatherOrder.Tag == null || m_txtFatherOrder.Tag.ToString() == "")
                {
                    MessageBox.Show("û��ҽ���ɹ���Ϊ����ҽ����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_txtDoctor.Focus();
                }
                else
                {
                    m_txtDoctor.Focus();
                }
            }
        }

        private void m_txtFatherOrder_m_evtSelectItem(object sender, System.Windows.Forms.ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                if (lviSelected.Tag == null) return;
                clsBIHOrder objBIHOrder = new clsBIHOrder();
                try
                {
                    objBIHOrder = lviSelected.Tag as clsBIHOrder;
                }
                catch
                { }
                if (objBIHOrder != null)
                {
                    if (m_lblSaveOrderID.Tag.ToString().Trim() != objBIHOrder.m_strOrderID.Trim())
                    {
                        m_txtFatherOrder.Text = objBIHOrder.m_strName;
                        m_txtFatherOrder.Tag = objBIHOrder.m_strOrderID;
                        //����ҽ��Ĭ��ͬ����	{�Ը�Ϊ׼}
                        try
                        {
                            m_txtRecipeNo.Text = objBIHOrder.m_intRecipenNo.ToString();
                        }
                        catch { }
                    }
                    else
                    {
                        MessageBox.Show("���������Լ�Ϊ����ҽ����", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            m_txtDoctor.Focus();
        }
        #endregion
        #region ¼��ҽ��
        private void m_txtDoctor_m_evtFindItem(object sender, string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            clsBIHDoctor[] arrDoctor;
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngFindDoctor(strFindCode, out arrDoctor);
            if ((ret > 0) && (arrDoctor != null) && (arrDoctor.Length > 0))
            {
                for (int i = 0; i < arrDoctor.Length; i++)
                {
                    ListViewItem lvi = lvwList.Items.Add(arrDoctor[i].m_strDoctorNo);
                    lvi.SubItems.Add(arrDoctor[i].m_strDoctorName);
                    lvi.Tag = arrDoctor[i];
                }
            }
            else
            {
                //���û��ֵ�򣬱���û�в鵽�������ת����
                if (m_txtDoctor.Tag == null || m_txtDoctor.Tag.ToString() == "")
                {
                    MessageBox.Show("û���ҵ���Ӧ��¼��ҽ���������������Ĳ�ѯ����!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_txtDoctor.SelectAll();
                }
                else
                {
                    m_mthEndInput();
                }
            }
        }

        private void m_txtDoctor_m_evtSelectItem(object sender, System.Windows.Forms.ListViewItem lviSelected)
        {
            m_txtDoctor.Text = lviSelected.SubItems[1].Text;
            m_txtDoctor.Tag = (lviSelected.Tag as clsBIHDoctor).m_strDoctorID;
            m_mthEndInput();
        }

        private void m_txtDoctor_m_evtInitListView(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Columns.Clear();
            ColumnHeader ch1 = lvwList.Columns.Add("No", 60, HorizontalAlignment.Left);
            ColumnHeader ch2 = lvwList.Columns.Add("Name", 60, HorizontalAlignment.Left);
            lvwList.Width = 140;
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="strDoctorID"></param>
        /// <param name="strDoctorName"></param>
        public void m_mthSetDoctor(string strDoctorID, string strDoctorName)
        {
            m_txtDoctor.Text = strDoctorName;
            m_txtDoctor.Tag = strDoctorID;
            m_objCurrentDoctor = new clsBIHDoctor();
            m_objCurrentDoctor.m_strDoctorID = strDoctorID;
            m_objCurrentDoctor.m_strDoctorName = strDoctorName;


        }

        /// <summary>
        /// ҽ���Ƿ���Ա༭
        /// </summary>
        public bool DoctorEditable
        {
            get
            {
                return m_txtDoctor.ReadOnly;
            }
            set
            {
                //m_txtDoctor.ReadOnly=value;
                m_txtDoctor.ReadOnly = true;//���� {������}
            }
        }
        #endregion
        #endregion
        #region �¼�
        #region Load

        //1066 ҽ��¼��ʱĬ��¼ҽ����Ϊ�ܴ�ҽ��
        /// <summary>
        /// 1066 ҽ��¼��ʱĬ��¼ҽ����Ϊ�ܴ�ҽ�� true �ܴ�ҽ�� false ��ǰ��¼��
        /// </summary>
        private bool blnInputIsDoctor = false;

        private void ctlBIHOrderDetail_Load(object sender, System.EventArgs e)
        {
            if (DesignMode) return;
            m_cboExecuteType.m_intAddItem("1", "1-����", "1");
            m_cboExecuteType.m_intAddItem("2", "2-��ʱ", "2");
            m_cboExecuteType.m_intAddItem("3", "3-��ҩ", "3");
            m_cboExecuteType.SelectedIndex = 0;
            //����:{1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
            m_cboRepare.Items.Add("��ҩ");
            m_cboRepare.Items.Add("����ҩ");
            m_cboRepare.SelectedIndex = -1;
            SetComboBoxRepare();
            m_cboRepare.Enabled = false;
            //m_objHighlight.m_mthBindForm(this);
            #region ��ǰ����ؼ�����ı���ɫ����
            Color m_BackColor = Color.FromArgb(222, 239, 165);
            ctlCtl_HighLightFocus highLight = new ctlCtl_HighLightFocus(m_BackColor);
            highLight.m_mthAddControlInContainer(this);
            #endregion

            blnInputIsDoctor = (clsPublic.m_intGetSysParm("1066") == 1 ? true : false);
        }
        /// <summary>
        /// ���÷������
        /// ҵ��˵��������û�г�Ժ��ҩ�������г�Ժ��ҩ��
        /// </summary>
        private void SetComboBoxRepare()
        {
            m_cboRateType.Items.Clear();
            /*
            m_cboRateType.m_intAddItem("0","����","0");
            m_cboRateType.m_intAddItem("1","�Ա�","1");
            m_cboRateType.m_intAddItem("2","����","2");
            m_cboRateType.m_intAddItem("3","����ҩ","3");
            if(m_cboExecuteType.SelectedIndex==1)//��ʱ
            {
                m_cboRateType.m_intAddItem("4","��Ժ��ҩ","4");
            }
             */
            m_cboRateType.m_intAddItem("0", "ҩ��", "1");
            m_cboRateType.m_intAddItem("1", "�����Ա�", "2");
            m_cboRateType.m_intAddItem("2", "���һ���", "3");

            m_cboRateType.SelectedIndex = 0;
        }
        #endregion
        #region Resize
        private void ctlBIHOrderDetail_Resize(object sender, System.EventArgs e)
        {
            m_lblBackground.Location = new Point(0, 0);
            m_lblBackground.Size = new Size(this.Width - 1, this.Height - 1);
        }
        #endregion
        #region ����
        private void m_txtDosage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                #region �����������0
                double dbl1 = -1;
                if (m_txtDosage.Text.Trim() != "")
                {
                    try
                    {
                        dbl1 = double.Parse(m_txtDosage.Text.Trim());
                    }
                    catch { }
                }
                if (dbl1 <= 0)
                {
                    //MessageBox.Show("�������0��","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //m_txtDosage.Focus();
                    //m_txtDosage.SelectAll();
                    m_txtDosage.Focus();
                    return;
                }
                #endregion
                AutoFillData();
                //if(m_txtGet.Enabled && m_txtGet.Visible)
                //    m_txtGet.Focus();
                //else if(m_chkIsRepare.Enabled && m_chkIsRepare.Visible)
                //    m_chkIsRepare.Focus();
                //else if(m_chkISNEEDFEEL.Enabled && m_chkISNEEDFEEL.Visible)
                //    m_chkISNEEDFEEL.Focus();
                //else if(m_dtStartTime.Enabled && m_dtStartTime.Visible)
                //    m_dtStartTime.Focus();
                //else if(m_dtFinishTime.Enabled && m_dtFinishTime.Visible)
                //    m_dtFinishTime.Focus();
                //else if(m_txtDoctor.Enabled && m_txtDoctor.Visible)
                //    m_txtDoctor.Focus();
                //else
                //    m_mthEndInput();
            }

            if (e.KeyCode == Keys.Enter)
            {

                /*=================>*/
                setTheControlOrder(((TextBox)sender).Name);
                /*<============================*/
            }

        }


        private int AutoFillPZData()
        {
            int intDays = 1;		 //����-��Ժ��ҩ
            decimal dmlDosage = -1;//ҽ���µļ���[һ�μ���]
            decimal dmlUse = -1;	 //����
            decimal dmlGet = -1;	 //����
            decimal dmlUnitDosage = -1;//���� [�շ���Ŀ���еļ�����DOSAGE_DEC��]
            bool m_blIsType;

            try { dmlDosage = decimal.Parse(m_txtDosage.Text); }
            catch { }
            try { dmlUnitDosage = decimal.Parse(m_txtPrice.Tag.ToString()); }
            catch { }

            intDays = 1;
            if (dmlDosage < 0 || dmlUnitDosage <= 0) { ; return 1; }
            string strFreqID = "";
            if (m_txtExecuteFreq.Tag != null)
                strFreqID = m_txtExecuteFreq.Tag.ToString();
            if (strFreqID == string.Empty) return -1;
            long lngRes = 1;

            m_objTempFreq = GetFreqVoByFreqID(strFreqID.Trim());
            if (m_objTempFreq == null) return -1;
            if (((frmBIHOrderInput)this.ParentForm).m_intTypePControl == 0)
            {
                dmlGet = (dmlDosage * m_objTempFreq.m_intTimes * intDays) / dmlUnitDosage;	//  ������(����*����*����/��װ��)
            }
            else
            {

                dmlGet = decimal.Ceiling(dmlDosage / dmlUnitDosage) * m_objTempFreq.m_intTimes * intDays;	//  ������(����*����*����/��װ��)
            }
            //ȡ��
            dmlGet = decimal.Ceiling(dmlGet);

            if (dmlGet < 0) { ; return -1; }

            m_txtGet.Text = Convert.ToString(dmlGet);
            return 1;

        }

        /// <summary>
        /// ��Ժ��ҩ�����ϼ�
        /// </summary>
        public void AutoFillData2()
        {
            int intDays = 1;		 //����-��Ժ��ҩ
            decimal dmlGet = 1;
            if (m_txtDays.Enabled == true && m_txtDays.ReadOnly == false)
            {
                try
                {
                    intDays = int.Parse(m_txtDays.Text.Trim());
                }
                catch
                {
                }
                try
                {
                    dmlGet = int.Parse(m_txtGet.Text.Trim());
                }
                catch
                {
                }
                m_txtGet2.Text = Convert.ToString(dmlGet * intDays) + m_txtGetUnit.Text;
            }
            else
            {
                m_txtGet2.Text = "";
            }

        }
        #endregion
        #region ����
        //�������ܸ���
        private void m_txtUse_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //				if(m_txtUse.Text.Trim()=="") return;
                //				decimal dmlUse=decimal.Parse(m_txtUse.Text);
                //				decimal dmlGet=m_dmlComputeGet(dmlUse);
                //				if(dmlGet<0)
                //					m_txtGet.Text =clsConverter.ToString(dmlGet);
                //				else
                //					m_txtGet.Text ="";
                //				e.Handled=true;
                m_txtGet.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                m_txtDosage.Focus();
            }
        }
        private void m_txtUse_Leave(object sender, System.EventArgs e)
        {
            //			if(m_txtUse.Text.Trim()=="") return;
            //			decimal dmlUse=decimal.Parse(m_txtUse.Text);
            //			decimal dmlGet=m_dmlComputeGet(dmlUse);
            //			if(dmlGet<0)
            //				m_txtGet.Text =clsConverter.ToString(dmlGet);
            //			else
            //				m_txtGet.Text ="";
        }
        #endregion
        #region ����
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_txtGet_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double db1 = double.Parse(m_txtGet.Text.ToString().Trim());
                    if (db1 <= 0)
                    {
                        m_txtGet.Focus();
                        return;
                    }
                }
                catch
                {
                    m_txtGet.Focus();
                    return;
                }


            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                //m_txtOrderName.Focus();
                //m_txtRecipeNo.Focus();
                /*=================>*/
                setTheControlOrder(((TextBox)sender).Name);
                /*<============================*/
            }
        }
        #endregion
        #region ���ñ�־
        private void m_cboRateType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if((e.KeyCode==Keys.Enter)||(e.KeyCode==Keys.Right))
            //{
            //    //���ñ�־��{0��"";1���Ա�;2������;3������ҩ;4����Ժ��ҩ;}
            //    if(m_cboRateType.SelectedIndex==4)
            //        m_txtDays.Focus();
            //    else if(m_txtDosage.Enabled && m_txtDosage.Visible)
            //        m_txtDosage.Focus();
            //    else if(m_txtGet.Enabled && m_txtGet.Visible)
            //        m_txtGet.Focus();
            //    else if(m_chkIsRepare.Enabled && m_chkIsRepare.Visible)
            //        m_chkIsRepare.Focus();
            //    else if(m_chkISNEEDFEEL.Enabled && m_chkISNEEDFEEL.Visible)
            //        m_chkISNEEDFEEL.Focus();
            //    else if(m_dtStartTime.Enabled && m_dtStartTime.Visible)
            //        m_dtStartTime.Focus();
            //    else if(m_dtFinishTime.Enabled && m_dtFinishTime.Visible)
            //        m_dtFinishTime.Focus();
            //    else if(m_txtDoctor.Enabled && m_txtDoctor.Visible)
            //        m_txtDoctor.Focus();
            //    else
            //        m_mthEndInput();
            //}
            //else if(e.KeyCode==Keys.Left)
            //{
            //    m_txtDosageType.Focus();
            //}
            //else
            //{
            //    m_cboRateType.Focus();
            //}
            if (e.KeyCode == Keys.Enter)
            {
                //m_txtOrderName.Focus();
                //m_txtRecipeNo.Focus();
                /*=================>*/
                setTheControlOrder(((com.digitalwave.controls.ctlQComboBox)sender).Name);
                /*<============================*/
            }
        }

        int RateTypeOldIndex = -1;
        private void m_cboRateType_Leave(object sender, System.EventArgs e)
        {
            //if (m_cboRateType.SelectedIndex != RateTypeOldIndex)
            //{
            //    if (MessageBox.Show("ȷʵҪ���ĵ�ǰ���ñ�־��", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    {
            //        m_cboRateType.SelectedIndex = RateTypeOldIndex;
            //        m_cboRateType.Focus();
            //        return;
            //    }
            //}
        }
        private void m_cboRateType_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            //m_cboRateType_Leave(sender,e);
        }
        private void m_txtDays_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && (m_txtDays.Enabled))
            {
                try
                {
                    int i1 = int.Parse(m_txtDays.Text.ToString().Trim());
                }
                catch
                {
                    m_txtDays.Focus();
                    return;
                }
                AutoFillData2();
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                //m_txtOrderName.Focus();
                //m_txtRecipeNo.Focus();
                /*=================>*/
                setTheControlOrder(((TextBox)sender).Name);
                /*<============================*/
            }
            /*
			if(e.KeyCode==Keys.Enter && (!m_txtDays.ReadOnly))
			{
				AutoFillData();
				if(m_txtDosage.Enabled && m_txtDosage.Visible)
					m_txtDosage.Focus();
				else if(m_txtGet.Enabled && m_txtGet.Visible)
					m_txtGet.Focus();
				else if(m_chkIsRepare.Enabled && m_chkIsRepare.Visible)
					m_chkIsRepare.Focus();
				else if(m_chkISNEEDFEEL.Enabled && m_chkISNEEDFEEL.Visible)
					m_chkISNEEDFEEL.Focus();
				else if(m_dtStartTime.Enabled && m_dtStartTime.Visible)
					m_dtStartTime.Focus();
				else if(m_dtFinishTime.Enabled && m_dtFinishTime.Visible)
					m_dtFinishTime.Focus();
				else if(m_txtDoctor.Enabled && m_txtDoctor.Visible)
					m_txtDoctor.Focus();
				else
					m_mthEndInput();
			}
			else if(e.KeyCode==Keys.Left)
			{
				m_cboRateType.Focus();
			}
             */
        }
        private void m_txtDays_Leave(object sender, System.EventArgs e)
        {
            if (m_txtDays.Enabled)
            {
                AutoFillData2();
            }
        }
        #endregion
        #region ҽ������
        private void m_cboExecuteType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                //m_txtOrderName.Focus();
                //m_txtRecipeNo.Focus();
                /*=================>*/
                setTheControlOrder(((com.digitalwave.controls.ctlQComboBox)sender).Name);
                /*<============================*/
            }
            else if (e.KeyCode == Keys.Space)
            {
                //EmptyInput();
                if (((frmBIHOrderInput)this.ParentForm).m_dtvOrder.SelectedRows.Count > 0)
                {
                    clsBIHOrder order = (clsBIHOrder)((frmBIHOrderInput)this.ParentForm).m_dtvOrder.SelectedRows[0].Tag;
                    //ҽ������
                    clsT_aid_bih_ordercate_VO p_objItem = (clsT_aid_bih_ordercate_VO)((frmBIHOrderInput)this.ParentForm).m_htOrderCate[((clsBIHOrder)((frmBIHOrderInput)this.ParentForm).GetTheFaterOrder(order.m_intRecipenNo)).m_strOrderDicCateID];
                    if (p_objItem != null && p_objItem.m_intSAMEORDER_INT == 2)
                    {
                        MessageBox.Show("����Ϊ��ǰ���Ž�����ҽ������!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (order.m_intStatus != 0 && order.m_intStatus != 7)
                    {

                        MessageBox.Show("����Ϊ��ǰ���Ž�����ҽ������!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (!order.m_strCreatorID.Equals(((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpID))
                    {
                        MessageBox.Show("����Ϊ���˵�ҽ��������ҽ������!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        return;
                    }
                    else if (order.m_intSOURCETYPE_INT != ((frmBIHOrderInput)this.ParentForm).m_intSOURCETYPE_INT)
                    {
                        MessageBox.Show("ҽ��¼�������治ͬ��ҽ�����ܽ�����ҽ������!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    m_txtRecipeNo.Text = ((clsBIHOrder)((frmBIHOrderInput)this.ParentForm).m_dtvOrder.SelectedRows[0].Tag).m_intRecipenNo.ToString().Trim();
                    //m_objCurrentOrder = null;//����ǰҽ����ʼ�����ѽ���������ҽ���Ĳ���
                    m_txtRecipeNo.Focus();
                    SendKeys.Send("{Enter}");
                }


            }

        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="m_strName">�ؼ���</param>
        public void setTheControlOrder(string m_strName)
        {
            switch (m_strName)
            {
                case "m_cboExecuteType":
                    if (m_txtOrderCate.Enabled)
                    {
                        m_txtOrderCate.Focus();
                    }
                    else
                    {
                        setTheControlOrder(m_txtOrderCate.Name);
                    }
                    break;
                case "m_txtOrderCate":
                    if (m_txtRecipeNo.Enabled)
                    {
                        m_txtRecipeNo.Focus();
                    }
                    else
                    {
                        setTheControlOrder(m_txtRecipeNo.Name);
                    }
                    break;
                case "m_txtRecipeNo":
                    if (m_txtOrderName.Enabled)
                    {
                        m_txtOrderName.Focus();
                    }
                    else
                    {
                        setTheControlOrder(m_txtOrderName.Name);
                    }
                    break;
                case "m_txtOrderName":
                    if (m_txtOrderName2.Enabled && m_txtOrderName2.ReadOnly)
                    {
                        m_txtOrderName2.Focus();
                    }
                    else
                    {
                        setTheControlOrder(m_txtOrderName2.Name);
                    }
                    break;
                case "m_txtOrderName2":
                    if (m_txtDosage.Enabled)
                    {
                        m_txtDosage.Focus();
                    }
                    else
                    {
                        setTheControlOrder(m_txtDosage.Name);
                    }
                    break;
                case "m_txtDosage":
                    if (m_txtDosageType.Enabled && m_txtDosageType.Visible)
                    {
                        m_txtDosageType.Focus();
                    }
                    else
                    {
                        if (m_txtCheck.Enabled && m_txtCheck.Visible)
                        {
                            m_txtCheck.Focus();
                        }
                        if (m_txtSample.Enabled && m_txtSample.Visible)
                        {
                            m_txtSample.Focus();
                        }
                        else
                        {
                            setTheControlOrder(m_txtDosageType.Name);
                        }
                    }
                    break;
                case "m_txtDosageType":
                    if (m_txtExecuteFreq.Enabled)
                    {
                        m_txtExecuteFreq.Focus();
                    }
                    else
                    {
                        setTheControlOrder(m_txtExecuteFreq.Name);
                    }
                    break;
                case "m_txtExecuteFreq":
                    if (m_txtDays.Enabled)
                    {
                        m_txtDays.Focus();
                    }
                    else
                    {
                        if (m_cboRateType.Enabled)
                        {
                            m_cboRateType.Focus();
                        }
                        else
                        {
                            setTheControlOrder(m_cboRateType.Name);
                        }
                    }
                    break;
                case "m_txtGet":
                    if (m_txtDays.Enabled)
                    {
                        m_txtDays.Focus();
                    }
                    else
                    {
                        setTheControlOrder(m_txtDays.Name);
                    }
                    break;
                case "m_txtDays":
                    if (m_cboRateType.Enabled)
                    {
                        m_cboRateType.Focus();
                    }
                    else
                    {
                        setTheControlOrder(m_cboRateType.Name);
                    }
                    break;
                case "m_txtATTACHTIMES_INT":
                    if (m_txtDoctorList.Enabled)
                    {
                        m_txtDoctorList.Focus();
                    }
                    else
                    {
                        setTheControlOrder(m_txtDoctorList.Name);
                    }
                    break;
                case "m_txtDoctorList":
                    m_mthEndInput();
                    break;
                case "m_cboRateType":
                    if (cboShiying.Enabled)
                    {
                        this.cboShiying.Focus();
                    }
                    else
                    {
                        setTheControlOrder(cboShiying.Name);
                    }

                    break;
                case "cboShiying":
                    if (m_txtREMARK_VCHR.Enabled)
                    {
                        this.m_txtREMARK_VCHR.Focus();
                    }
                    else
                    {
                        setTheControlOrder(m_txtREMARK_VCHR.Name);
                    }

                    break;
                case "m_txtREMARK_VCHR":
                    if (this.cboEmer.Enabled)
                        this.cboEmer.Focus();
                    else if (this.cboOps.Enabled)
                        this.cboOps.Focus();
                    else
                        m_mthEndInput();
                    break;
                case "m_dtStartTime2":
                    if (m_txtDoctorList.Enabled)
                    {
                        m_txtDoctorList.Focus();
                    }
                    else
                    {
                        setTheControlOrder(m_txtDoctorList.Name);
                    }
                    break;
                case "m_txtSample":
                    if (m_txtExecuteFreq.Enabled)
                    {
                        m_txtExecuteFreq.Focus();
                    }
                    else
                    {
                        setTheControlOrder(m_txtExecuteFreq.Name);
                    }
                    break;
                case "m_txtCheck":
                    if (m_txtExecuteFreq.Enabled)
                    {
                        m_txtExecuteFreq.Focus();
                    }
                    else
                    {
                        setTheControlOrder(m_txtExecuteFreq.Name);
                    }
                    break;
                case "cboEmer":
                    cboOps.Focus();
                    break;
                case "cboOps":    //"txtCureDays":
                    if (cboKJ.Enabled)
                        cboKJ.Focus();
                    else if (cboProxyBoil.Enabled)
                        cboProxyBoil.Focus();
                    else
                        m_mthEndInput();
                    break;
                case "end":
                    m_mthEndInput();
                    break;
                case "cboKJ":
                    if (cboKJ.SelectedIndex == 2)   // Ԥ��
                        cboQK.Focus();
                    else if (cboProxyBoil.Enabled)
                        cboProxyBoil.Focus();
                    else
                        m_mthEndInput();
                    break;
                case "cboQK":
                    m_mthEndInput();
                    break;
                case "cboProxyBoil":
                    m_mthEndInput();
                    break;
                default:
                    m_mthEndInput();
                    break;
            }
        }

        /// <summary>
        /// ҽ��¼�뷽ʽ�������
        /// ҵ��˵����
        ///	if(����ҽ��-1) then �Ƿ񲹵ǲ�����
        /// </summary>
        public void m_cboExecuteType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ExecuteType_SelectedIndexChanged();
        }

        public void ExecuteType_SelectedIndexChanged()
        {
            int m_intExecuteType = clsConverter.ToInt(m_cboExecuteType.m_strGetID(m_cboExecuteType.SelectedIndex));

            //��ҩ������ʾ������/���ؼ�
            if (IsSubOrder == true && ParentOrder != null)
            {
                clsT_aid_bih_ordercate_VO p_objItem;
                p_objItem = (clsT_aid_bih_ordercate_VO)((frmBIHOrderInput)this.ParentForm).m_htOrderCate[ParentOrder.m_strOrderDicCateID];
                //��ҩ������ʾ(�������ؼ��Ŀ�����ʾ�������ֶ�)
                if (p_objItem.m_strORDERCATEID_CHR.Equals(((frmBIHOrderInput)this.ParentForm).m_objSpecateVo.m_strMID_MEDICINE_CHR))
                {
                    m_intExecuteType = 3;
                }
                /*<============================================*/
            }
            /*<====================================*/

            m_cboExecuteTypeChanged(m_intExecuteType);
            // ҽ�����ͽ������
            if (this.m_txtOrderName.Tag != null && this.m_txtOrderName.Tag is clsBIHOrderDic)
            {
                clsBIHOrderDic objDic = (clsBIHOrderDic)this.m_txtOrderName.Tag;
                OrderCateViewControl(objDic);
            }
        }

        /// <summary>
        /// ҽ��¼�뷽ʽ�������
        /// </summary>
        public void m_cboExecuteTypeChanged(int m_intExecuteType)
        {

            /*����ҽ��:��������.��������Ĭ��Ϊ1. 
         ��ʱҽ��:��������,����,��ʼʱ��,ֹͣʱ��.��������Ĭ��Ϊ0. 
         ��Ժ��ҩ:���ò���,��ʼʱ��,ֹͣʱ��.��������Ĭ��Ϊ0*/

            //switch (clsConverter.ToInt(m_cboExecuteType.m_strGetID(m_cboExecuteType.SelectedIndex)))
            switch (m_intExecuteType)
            {
                case 1:
                    //m_txtATTACHTIMES_INT.Text = "1"; //����                 
                    m_txtATTACHTIMES_INT.Enabled = true;
                    m_txtDays.Enabled = false;
                    m_dtStartTime2.Enabled = true;
                    m_dtStartTime2.BackColor = Color.White;
                    m_dtFinishTime2.Enabled = true;
                    m_dtFinishTime2.BackColor = Color.White;
                    //Ƶ�� 
                    m_txtExecuteFreqControl(true);
                    // ��ʾ�����������ؼ�
                    m_txtDays.Visible = false;
                    m_txtDays.Enabled = false;
                    m_lblDay.Visible = false;
                    m_txtGet2.Visible = false;

                    break;

                case 2:
                    m_txtATTACHTIMES_INT.Text = "0"; //����
                    m_txtATTACHTIMES_INT.Enabled = false;
                    m_dtStartTime2.Enabled = false;
                    m_dtStartTime2.BackColor = SystemColors.Control;
                    m_dtFinishTime2.Enabled = false;
                    m_dtFinishTime2.BackColor = SystemColors.Control;
                    //Ƶ��
                    //m_txtExecuteFreq.Enabled = false;
                    //m_txtExecuteFreq.BackColor = SystemColors.Control;
                    // ��ʾ�����������ؼ�
                    m_txtDays.Visible = false;
                    m_txtDays.Enabled = false;
                    m_lblDay.Visible = false;
                    m_txtGet2.Visible = false;
                    m_txtDays.Enabled = false;

                    break;
                case 3:
                    m_txtATTACHTIMES_INT.Text = "0"; //����
                    m_txtATTACHTIMES_INT.Enabled = false;
                    m_dtStartTime2.Enabled = false;
                    m_dtStartTime2.BackColor = SystemColors.Control;
                    m_dtFinishTime2.Enabled = false;
                    m_dtFinishTime2.BackColor = SystemColors.Control;
                    //Ƶ�� 
                    m_txtExecuteFreqControl(true);
                    // ��ʾ�����������ؼ�
                    m_txtDays.Visible = true;
                    m_txtDays.Enabled = true;
                    m_lblDay.Visible = true;
                    m_txtGet2.Visible = true;
                    m_txtDays.Enabled = true;
                    break;
            }
        }

        #endregion
        #region ҽ������
        private void m_txtEntrust_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                //m_txtOrderName.Focus();
                //m_txtRecipeNo.Focus();
                /*=================>*/
                setTheControlOrder(((TextBox)sender).Name);
                /*<============================*/
            }
        }
        #endregion
        #region �Ƿ񲹵�
        private void m_chkIsRepare_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if((e.KeyCode==Keys.Enter)||(e.KeyCode==Keys.Right))
            //{
            //    if(m_chkIsRepare.Checked)
            //    {
            //        m_cboRepare.Focus();
            //        m_cboRepare.DroppedDown =true;
            //    }
            //    else if(m_chkISNEEDFEEL.Enabled && m_chkISNEEDFEEL.Visible)
            //        m_chkISNEEDFEEL.Focus();
            //    else if(m_dtStartTime.Enabled && m_dtStartTime.Visible)
            //        m_dtStartTime.Focus();
            //    else if(m_dtFinishTime.Enabled && m_dtFinishTime.Visible)
            //        m_dtFinishTime.Focus();
            //    else if(m_txtDoctor.Enabled && m_txtDoctor.Visible)
            //        m_txtDoctor.Focus();
            //    else
            //        m_mthEndInput();
            //}
            //else if(e.KeyCode==Keys.Left)
            //{
            //    m_txtGet.Focus();
            //}
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                //m_txtOrderName.Focus();
                //m_txtRecipeNo.Focus();
                /*=================>*/
                setTheControlOrder(((CheckBox)sender).Name);
                /*<============================*/
            }
        }
        private void m_chkIsRepare_CheckedChanged(object sender, System.EventArgs e)
        {
            if (m_cboExecuteType.SelectedIndex == 0)//����ҽ��
            {
                if (m_chkIsRepare.Checked) m_chkIsRepare.Checked = false;
                m_cboRepare.SelectedIndex = -1;
                m_chkIsRepare.Enabled = false;
                m_cboRepare.Enabled = false;
                return;
            }
            if (m_chkIsRepare.Checked)
            {
                m_cboRepare.Enabled = true;
                if (m_cboRepare.Items.Count > 0) m_cboRepare.SelectedIndex = 0;
            }
            else
            {
                m_cboRepare.SelectedIndex = -1;
                m_cboRepare.Enabled = false;
            }
        }
        private void m_cboRepare_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if((e.KeyCode==Keys.Enter)||(e.KeyCode==Keys.Right))
            //{
            //    if(m_chkISNEEDFEEL.Enabled && m_chkISNEEDFEEL.Visible)
            //        m_chkISNEEDFEEL.Focus();
            //    else if(m_dtStartTime.Enabled && m_dtStartTime.Visible)
            //        m_dtStartTime.Focus();
            //    else if(m_dtFinishTime.Enabled && m_dtFinishTime.Visible)
            //        m_dtFinishTime.Focus();
            //    else if(m_txtDoctor.Enabled && m_txtDoctor.Visible)
            //        m_txtDoctor.Focus();
            //    else
            //        m_mthEndInput();

            //}
            //else if(e.KeyCode==Keys.Left)
            //{
            //    m_cboRepare.Focus();
            //}
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                //m_txtOrderName.Focus();
                //m_txtRecipeNo.Focus();
                /*=================>*/
                setTheControlOrder(((com.digitalwave.controls.ctlQComboBox)sender).Name);
                /*<============================*/
            }
        }
        #endregion
        #region �Ƿ�Ƥ��
        private void m_chkISNEEDFEEL_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if((e.KeyCode==Keys.Enter)||(e.KeyCode==Keys.Right))
            //{	
            //    if(m_txtDoctor.Enabled && m_txtDoctor.Visible)
            //        m_txtDoctor.Focus();
            //    else
            //        m_mthEndInput();
            //}
            //else if(e.KeyCode==Keys.Left)
            //{
            //    if(m_chkIsRepare.Enabled)
            //        m_chkIsRepare.Focus();
            //    else if(m_txtDoctor.Enabled && m_txtDoctor.Visible)
            //        m_txtDoctor.Focus();
            //    else
            //        m_mthEndInput();
            //}
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                //m_txtOrderName.Focus();
                //m_txtRecipeNo.Focus();
                /*=================>*/
                setTheControlOrder(((CheckBox)sender).Name);
                /*<============================*/
            }
        }
        private void m_txtISNEEDFEEL_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if((e.KeyCode==Keys.Enter)||(e.KeyCode==Keys.Right))
            //{
            //    if(m_txtDoctor.Enabled && m_txtDoctor.Visible)
            //        m_txtDoctor.Focus();
            //    else
            //        m_mthEndInput();
            //}
            //else if(e.KeyCode==Keys.Left)
            //{
            //    m_chkISNEEDFEEL.Focus();
            //}
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                /*=================>*/
                setTheControlOrder(((TextBox)sender).Name);
                /*<============================*/
            }
        }
        #endregion
        #region �Ƿ����
        private void m_chkIsRich_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Right))
            {
                m_chkISNEEDFEEL.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                m_chkIsRepare.Focus();
            }
        }
        private void m_chkIsRich_CheckedChanged(object sender, System.EventArgs e)
        {
            /** @update by xzf (05-09-20) */
            if (m_chkIsRich.Checked)
            {
                //@m_chkIsRich.BackColor =SystemColors.Highlight;
                //@m_chkIsRich.ForeColor =SystemColors.HighlightText;
                m_chkIsRich.BackColor = SystemColors.Desktop;
            }
            else
            {
                m_chkIsRich.BackColor = SystemColors.Control;
            }
            /* <<=============================================== */
        }
        #endregion
        #region ֹͣʱ��
        private void m_dtFinishTime_Leave(object sender, System.EventArgs e)
        {
            if (m_lblSaveConOrderFreqID.Tag == null)
                m_lblSaveConOrderFreqID.Tag = new clsDcl_ExecuteOrder().m_strGetConfreqID();
            if (m_lblSaveConOrderFreqID.Tag.ToString().Trim() == m_txtExecuteFreq.Tag.ToString())
            {
                if (m_dtFinishTime.Value < m_dtStartTime.Value)
                {
                    m_dtFinishTime.Value = (System.DateTime.Now > m_dtStartTime.Value) ? (System.DateTime.Now) : (m_dtStartTime.Value);
                    m_dtFinishTime.Refresh();
                    MessageBox.Show("ֹͣʱ����������ʼʱ�䣡", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }
        #endregion
        #region ����ҽ��
        private void m_txtDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            m_mthEndInput();
        }

        private void m_txtDoctor_Enter(object sender, System.EventArgs e)
        {
            if (!m_txtDoctor.Enabled)
            {
                m_mthEndInput();
            }
        }
        #endregion
        #endregion
        #region ˢ�¸��ӵ�����Ϣ
        /// <summary>
        /// ˢ�¸��ӵ�����Ϣ
        /// </summary>
        private void m_mthRefreshTipInfo()
        {
            string strCateID = clsConverter.ToString(m_picInfo.Tag);

            string strInfo = "";
            if (strCateID == "")
            {
                strInfo = "";
            }
            else
            {
                clsBIHOrderCate objCate = clsOrderDicCates.m_objGetCate(strCateID);
                if (objCate.IsMedicineCate || objCate.m_intISATTACH_INT == 0)//���Ϊҩ��û�и��ӵ�������ʾͼ��
                    strInfo = "";
                else
                    strInfo = "���ӵ���:" + objCate.m_strName;
            }
            this.toolTip1.SetToolTip(this.m_picInfo, strInfo);

            if (strInfo.Trim() == "")
            {
                timer1.Enabled = false;
                m_picInfo.Visible = false;
            }
            else
            {
                m_intCount = 0;
                m_picInfo.Visible = true;
                timer1.Enabled = true;
            }
        }

        private void m_picInfo_MouseEnter(object sender, System.EventArgs e)
        {
            toolTip1.Active = true;
        }

        private int m_intCount = 0;
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            m_picInfo.Visible = !(m_picInfo.Visible);
            m_intCount++;
            if ((m_intCount > 3) && (m_picInfo.Visible)) timer1.Enabled = false;
        }

        #endregion
        #region ����, ����,����
        #region ��ȡ����
        /// <summary>
        /// ��ȡ����	����ҽ���µļ����ͼ�����λ [ʧ�ܷ���-1]
        /// </summary>
        /// <param name="dmlDosage">ҽ���µļ���</param>
        /// <param name="dmlUnitDosage">������λ</param>
        /// <returns>���� [ʧ�ܷ���-1]</returns>
        private decimal dmlComputeUse(decimal dmlDosage, decimal dmlUnitDosage)
        {
            decimal dmlUse = -1;
            try { dmlUse = dmlDosage / dmlUnitDosage; }
            catch { }
            return dmlUse;
        }
        #endregion
        #region ��ȡ��λƵ������������
        /// <summary>
        /// ��ȡ��λƵ������������	����������û�п��ǳ�Ժ��ҩ.[���ܼ����򷵻�-1]
        /// </summary>
        /// <param name="dmlUse">����</param>
        /// <param name="strFreqID">Ƶ��ID</param>
        /// <returns>��������,�����������򷵻�-1;</returns>
        /// <remarks>
        /// ҵ��������[������������Ƶ��]
        ///		���� = ���� * ������ҩ����
        ///		����:����=2,Ƶ��=3��4��,�� ����(3���)=2*4;
        /// </remarks>
        private decimal m_dmlComputeGet(decimal dmlUse, string strFreqID)
        {

            decimal dmlGet = dmlUse;
            if (strFreqID == string.Empty) return -1;
            long lngRes = 1;
            //if(m_objTempFreq==null || m_objTempFreq.m_strFreqID.Trim()!=strFreqID.Trim())
            //{
            //    lngRes =m_objService.m_lngGetRecipeFreqByID(strFreqID,out m_objTempFreq);	//��ȡ��ǰƵ����Ϣ			
            //}

            m_objTempFreq = GetFreqVoByFreqID(strFreqID.Trim());
            if (m_objTempFreq == null) return -1;
            dmlGet = dmlUse * m_objTempFreq.m_intTimes;	//����*����

            //ȡ��
            dmlGet = Decimal.Negate(decimal.Floor(decimal.Negate(dmlGet)));
            return dmlGet;
        }
        /// <summary>
        /// ��ȡ��λƵ������������	����������û�п��ǳ�Ժ��ҩ.[���ܼ����򷵻�-1]
        /// </summary>
        /// <param name="dmlUse">����</param>
        /// <returns>��������,�����������򷵻�-1;</returns>
        /// <remarks>
        /// ҵ��������[������������Ƶ��]
        ///		���� = ���� * ������ҩ����
        ///		����:����=2,Ƶ��=3��4��,�� ����(3���)=2*4;
        /// </remarks>
        private decimal m_dmlComputeGet(decimal dmlUse)
        {
            if (m_txtExecuteFreq.Tag != null)
            {
                string strFreqID = m_txtExecuteFreq.Tag.ToString();
                return m_dmlComputeGet(dmlUse, strFreqID);
            }
            else
                return -1;
        }
        #endregion
        #region �Զ����㲢��� [����,����,����]�ı���
        /// <summary>
        /// �Զ����㲢��� [����,����,����]�ı���
        /// </summary>
        /// <remarks>
        /// ҵ������:
        ///		if(���ñ�־="��Ժ��ҩ") then ���ǳ�Ժ��ҩ;else �����ǳ�Ժ��ҩ;
        ///		if(��ط��ü���ʧ��) then �����ı���Ϊ��;else ����������;
        ///		if(���ܼ��� || ����ʧ��) then m_txtUse.Text="";m_txtGet.Text="";
        ///		�û������,���������Ҫ,���¸�ֵ���ı���;(ȡ����)
        /// </remarks>
        private void AutoFillData()
        {
            int intDays = 1;		 //����-��Ժ��ҩ
            decimal dmlDosage = -1;//ҽ���µļ���[һ�μ���]
            decimal dmlUse = -1;	 //����
            decimal dmlGet = -1;	 //����
            decimal dmlUnitDosage = -1;//���� [�շ���Ŀ���еļ�����DOSAGE_DEC��]
            bool m_blIsType;
            #region ��ȡ-����ֵ

            try { dmlDosage = decimal.Parse(m_txtDosage.Text); }
            catch { }
            try { dmlUnitDosage = decimal.Parse(m_txtPrice.Tag.ToString()); }
            catch { }
            if (dmlDosage < 0 || dmlUnitDosage <= 0) { ; return; }
            dmlUse = dmlComputeUse(dmlDosage, dmlUnitDosage);
            if (dmlUse < 0) { ;return; }
            dmlGet = m_dmlComputeGet(dmlUse);
            if (dmlGet < 0) { ; return; }

            #endregion
            #region ��ֵ���ı��ؼ�

            intDays = 1;
            m_txtGet.Text = Convert.ToString(dmlGet * intDays);
            #endregion
        }
        #endregion
        #endregion
        #region ����
        /// <summary>
        /// ��ȡҽ����Ŀ�Ƿ�Ϊ����
        /// </summary>
        public bool CurrentItemIsGroup
        {
            get
            {
                return m_blnCurrentItemIsGroup;
            }
        }
        /// <summary>
        /// ��ȡ�������Ƿ�ֻ��
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return m_blnReadOnly;
            }
            set
            {
                /*
				m_blnReadOnly=value;
				m_txtOrderName.ReadOnly =m_blnReadOnly;
				//m_cboExecuteType.Enabled=(!m_blnReadOnly);
				//				m_txtRecipeNo.ReadOnly=m_blnReadOnly;
				m_txtExecuteFreq.ReadOnly=m_blnReadOnly;				
				m_txtDosageType.ReadOnly=m_blnReadOnly;
				m_txtDosage.ReadOnly=m_blnReadOnly;
				//m_txtUse.ReadOnly=m_blnReadOnly;//�������ܱ༭
				m_txtGet.ReadOnly=m_blnReadOnly;				
				m_chkIsRepare.AutoCheck=(!m_blnReadOnly);
				if(m_chkIsRepare.Checked)
					m_cboRepare.Enabled =(!m_blnReadOnly);
				else
					m_cboRepare.Enabled =false;
				m_chkISNEEDFEEL.AutoCheck =(!m_blnReadOnly);
				m_txtEntrust.ReadOnly=m_blnReadOnly;
				m_cboRateType.Enabled=(!m_blnReadOnly);
				if(m_cboRateType.SelectedIndex==4)
					m_txtDays.ReadOnly =m_blnReadOnly;
				else
					m_txtDays.ReadOnly =true;
				m_txtFatherOrder.ReadOnly=m_blnReadOnly;
				DoctorEditable=m_blnReadOnly;
                 */
            }
        }
        /// <summary>
        /// ���ò���ID
        /// </summary>
        public string PatientID
        {
            set { m_strPatientID_Chr = value; }
        }
        /// <summary>
        /// ���ò�����Ժ�Ǽ���ˮ��
        /// </summary>
        public string RegisterID
        {
            set { m_strRegisterID = value; }
        }
        #endregion
        #region ����
        /// <summary>
        /// ��ʾҽ������
        /// </summary>
        /// <param name="objOrder">ҽ����¼VO����</param>
        public void m_mthSetOrder(clsBIHOrder objOrder)
        {
            m_objCurrentOrder = objOrder;
            //EmptyInput();
            if (objOrder != null)
            {
                #region ��ʾ
                RateTypeOldIndex = -1;
                m_txtRecipeNo.Text = objOrder.m_intRecipenNo.ToString();//����
                //��ʾ�ķ���Ҫ�ر�������������ͽ�������Ϊ0������ǳ����ͽ����ű��ֲ���
                if (objOrder.m_intExecuteType == 1)
                {
                    m_txtRecipeNo.Tag = objOrder.m_intRecipenNo2;//��ʾ�ķ���
                }
                else
                {
                    m_txtRecipeNo.Tag = 0;
                }
                m_cboExecuteType.m_blnFindItem(objOrder.m_intExecuteType.ToString().Trim());
                // ҩƷ��Դ: 0 ҩ��(ȫ�Ʒ�,��ҩ); 1 �����Ա�(ֻ�շ��÷�������Ŀ,����ҩ); 2 ���һ���(ȫ�Ʒѣ�����ҩ)
                m_cboRateType.m_blnFindItem(objOrder.RateType.ToString().Trim());
                //���� 
                m_picInfo.Tag = objOrder.m_strOrderDicCateID;
                m_txtOrderName2.Text = objOrder.m_strName;
                m_txtOrderName2.Tag = objOrder.m_strOrderDicID;
                m_txtOrderName.Text = objOrder.m_strName;
                //m_txtOrderName.Tag ��������������ĿVO 
                m_lblSaveOrderID.Tag = objOrder.m_strOrderID;
                m_txtOrderSpec.Text = objOrder.m_strSpec;
                m_txtDosageUnit.Text = objOrder.m_strDosageUnit;
                m_txtUseUnit.Text = objOrder.m_strUseunit;
                m_txtGetUnit.Text = objOrder.m_strGetunit;
                m_txtGetUnit2.Text = objOrder.m_strGetunit;
                m_txtPackage.Text = objOrder.m_dmlPACKQTY_DEC.ToString();//��װ��   //objDic.m_StrPackage;//��װ=[���� ������λ / סԺ��λ]
                m_txtPackage.Tag = objOrder.m_intIPCHARGEFLG_INT;//סԺ�շѵ�λ 0 ��������λ 1����С��λ

                m_txtExecDept.Tag = objOrder.m_strExecDeptID;
                m_txtExecDept.Text = objOrder.m_strExecDeptID;

                m_chkIsRich.Checked = (objOrder.m_intIsRich == 1);
                m_txtPrice.Text = objOrder.m_dmlPrice.ToString();
                m_txtPrice.Tag = objOrder.m_dmlDosageRate;//�������

                //
                m_txtDosage.Text = objOrder.m_dmlDosage.ToString();
                //������ʾΪ0ʱ������ʾ0
                if (m_txtDosage.Text.Trim().Equals("0"))
                {
                    m_txtDosage.Text = "";
                }
                /*<================================*/
                m_txtUse.Text = objOrder.m_dmlUse.ToString();
                m_txtGet.Text = objOrder.m_dmlGet.ToString();
                m_txtGet2.Text = "";
                //
                m_txtExecuteFreq.Tag = objOrder.m_strExecFreqID;
                m_txtExecuteFreq.Text = objOrder.m_strExecFreqName;

                m_txtDosageType.Tag = objOrder.m_strDosetypeID;
                m_txtDosageType.Text = objOrder.m_strDosetypeName;

                // Ϊ�������������ֵ
                m_txtSample.Text = objOrder.m_strSAMPLEName_VCHR.ToString().Trim();
                m_txtSample.Tag = objOrder.m_strSAMPLEID_VCHR.ToString().Trim();
                // Ϊ�������������ֵ
                m_txtCheck.Text = objOrder.m_strPARTNAME_VCHR.ToString().Trim();
                m_txtCheck.Tag = objOrder.m_strPARTID_VCHR.ToString().Trim();
                m_TxbStartTime.Text = DateTimeToString(objOrder.m_dtStartDate);
                m_txbFinishTime.Text = DateTimeToString(objOrder.m_dtStopdate);
                m_txtInputDate.Text = DateTimeToString(objOrder.m_dtCreatedate);
                m_txtEntrust.Text = objOrder.m_strEntrust;
                //��Ժ��ҩ���� 
                m_txtDays.Text = objOrder.m_intOUTGETMEDDAYS_INT.ToString();
                AutoFillData2();//��Ժ��ҩ�����ϼ�

                //ҽ��
                m_txtMedicareType.Text = objOrder.m_strMedicareTypeName;
                //����
                m_txtATTACHTIMES_INT.Text = objOrder.m_intATTACHTIMES_INT.ToString();
                //��ʼ������ʱ��
                if (objOrder.m_dtStartDate != DateTime.MinValue)
                {
                    m_dtStartTime2.Text = objOrder.m_dtStartDate.ToString("yyyy��MM��dd��HHʱmm��");
                }
                else
                {
                    m_dtStartTime2.Text = "";
                }
                if (objOrder.m_dtFinishDate != DateTime.MinValue)
                {
                    m_dtFinishTime2.Text = objOrder.m_dtFinishDate.ToString("yyyy��MM��dd��HHʱmm��");
                }
                else
                {
                    m_dtFinishTime2.Text = "";
                }
                //����:{0=�����ǣ�1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
                if (objOrder.m_intIsRepare > 0)
                {
                    m_chkIsRepare.Checked = true;
                    if (objOrder.m_intIsRepare <= 4) m_cboRepare.SelectedIndex = objOrder.m_intIsRepare - 1;
                }
                else
                {
                    m_chkIsRepare.Checked = false;
                    m_cboRepare.SelectedIndex = -1;
                    m_cboRepare.Enabled = false;
                }
                //�Ƿ�Ƥ��
                if (objOrder.m_intISNEEDFEEL == 1)
                {
                    m_chkISNEEDFEEL.Checked = true;
                    //���Ƥ�Խ��
                    //clsT_Opr_Bih_OrderFeel_VO objResult;
                    //clsDcl_ExecuteOrder objTem =new clsDcl_ExecuteOrder();
                    //objTem.m_lngGetOrderFeelByOrderID(objOrder.m_strOrderID,out objResult);
                    //m_txtISNEEDFEEL.Text =objResult.m_strResultTypeName;
                }
                else
                {
                    m_chkISNEEDFEEL.Checked = false;
                    //m_txtISNEEDFEEL.Text ="";
                }
                //if(m_chkISNEEDFEEL.Tag==null || m_chkISNEEDFEEL.Tag.ToString().Trim()=="")
                //{	//����ҩƷҽ������ID
                //    m_chkISNEEDFEEL.Tag =new clsDcl_ExecuteOrder().m_strGetMedicineOrderTypeID().Trim();
                //}
                //if(m_chkISNEEDFEEL.Tag.ToString().Trim()==objOrder.m_strOrderDicCateID.Trim())
                //{
                //    m_chkISNEEDFEEL.Enabled =true;
                //}
                //else
                //{
                //    m_chkISNEEDFEEL.Enabled =false;
                //}
                m_txtDoctor.Tag = objOrder.m_strCreatorID;
                m_txtDoctor.Text = objOrder.m_strCreator;
                m_txtDoctorList.Text = objOrder.m_strDOCTOR_VCHR;
                m_txtDoctorList.Tag = objOrder.m_strDOCTORID_CHR;
                //����ҽ��
                m_txtFatherOrder.Text = objOrder.m_strParentName;
                m_txtFatherOrder.Tag = objOrder.m_strParentID;

                //ҽ��˵��
                m_txtREMARK_VCHR.Text = objOrder.m_strREMARK_VCHR;
                #endregion

                RateTypeOldIndex = 0;

                #region ����ҩ��;

                this.cboKJ.SelectedIndex = objOrder.AntiUse;
                this.cboQK.SelectedIndex = objOrder.AntiUse_YFLX;
                if (objOrder.AntiUse > 0)
                {
                    this.cboKJ.Enabled = true;
                    this.cboQK.Enabled = true;
                }
                //if (objOrder.CureDays == 0)
                //{
                //    this.txtCureDays.Text = string.Empty;
                //    this.txtCureDays.Enabled = false;
                //    this.txtCureDays.BackColor = this.label9.BackColor;
                //}
                //else
                //{
                //    this.txtCureDays.Text = objOrder.CureDays.ToString();
                //    this.txtCureDays.Enabled = true;
                //    this.txtCureDays.BackColor = Color.White;
                //}
                this.cboEmer.SelectedIndex = objOrder.IsEmer;
                this.cboEmer.Enabled = (objOrder.IsEmer > 0 ? true : false);

                #endregion

                // ���ʹ���
                this.cboProxyBoil.SelectedIndex = objOrder.IsProxyBoilMed;
                this.cboProxyBoil.Enabled = (objOrder.IsProxyBoilMed > 0 ? true : false);

                // ��ͷҽ��(����ҽ��)
                this.cboOps.SelectedIndex = objOrder.IsOps;

            }
            m_blnCurrentItemIsGroup = false;
            m_mthRefreshTipInfo();
        }

        /// <summary>
        /// ��ȡҽ��-����������Ŀ
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns></returns>
        public clsBIHOrder m_objGetOrder(clsBIHPatientInfo objPatient, clsBIHOrder objOrder)
        {
            if (m_blnCurrentItemIsGroup) return null;

            if (objOrder == null)
            {
                objOrder = new clsBIHOrder();
                objOrder.m_strOrderID = "";
            }
            else
            {
                //��ˮ��
                objOrder.m_strOrderID = m_lblSaveOrderID.Tag.ToString();
            }
            //����
            try
            {
                objOrder.m_intRecipenNo = Int32.Parse(m_txtRecipeNo.Text);
            }
            catch
            {
                objOrder.m_intRecipenNo = 0;

            }
            //��ʾ�ķ���
            try
            {
                objOrder.m_intRecipenNo2 = Int32.Parse(m_txtRecipeNo.Tag.ToString());
            }
            catch
            {
                objOrder.m_intRecipenNo2 = 0;

            }

            //����ҽ��1 ��ʱҽ��2 ��Ժ��ҩ3
            objOrder.m_intExecuteType = clsConverter.ToInt(m_cboExecuteType.m_strGetID(m_cboExecuteType.SelectedIndex));

            //��ǰ����
            objOrder.m_strRegisterID = objPatient.m_strRegisterID;
            objOrder.m_strPatientID = objPatient.m_strPatientID;

            //������Ŀ��Ϣ 
            objOrder.m_strOrderDicID = clsConverter.ToString(m_txtOrderName2.Tag);

            objOrder.m_strName = m_txtOrderName.Text;
            objOrder.m_strSpec = m_txtOrderSpec.Text;
            objOrder.m_strDosageUnit = m_txtDosageUnit.Text;
            objOrder.m_strUseunit = m_txtUseUnit.Text;
            objOrder.m_strGetunit = m_txtGetUnit.Text;
            objOrder.m_strExecDeptID = clsConverter.ToString(m_txtExecDept.Tag);		//ִ�п���
            objOrder.m_strExecDeptName = m_txtExecDept.Text;
            objOrder.m_intIsRich = (m_chkIsRich.Checked ? 1 : 0);
            objOrder.m_intISNEEDFEEL = (m_chkISNEEDFEEL.Checked ? 1 : 0);
            objOrder.m_dmlPrice = clsConverter.ToDecimal(m_txtPrice.Text);
            objOrder.m_dmlTradePrice = clsConverter.ToDecimal(this.m_txtItemTradePrice.Text);
            objOrder.m_dmlDosageRate = clsConverter.ToDecimal(m_txtPrice.Tag);
            objOrder.m_dmlPACKQTY_DEC = clsConverter.ToDecimal(m_txtPackage.Text);//��װ��   
            objOrder.m_intIPCHARGEFLG_INT = clsConverter.ToInt(m_txtPackage.Tag);//סԺ�շѵ�λ 0 ��������λ 1����С��λ
            //����
            objOrder.m_dmlDosage = clsConverter.ToDecimal(m_txtDosage.Text);
            objOrder.m_dmlUse = clsConverter.ToDecimal(m_txtUse.Text);
            if (m_txtGet.Text.ToString().Equals("0") || m_txtGet.Text.Trim().Equals(""))
            {
                m_txtGet.Text = "1";
            }
            objOrder.m_dmlGet = clsConverter.ToDecimal(m_txtGet.Text);

            //Ƶ��
            if (m_txtExecuteFreq.Text.Trim() != "" && m_txtExecuteFreq.Tag != null && m_txtExecuteFreq.Tag.ToString().Trim() != "")
            {
                objOrder.m_strExecFreqID = clsConverter.ToString(m_txtExecuteFreq.Tag);
                m_objTempFreq = GetFreqVoByFreqID(objOrder.m_strExecFreqID);
                if (m_objTempFreq != null)
                {
                    objOrder.m_strExecFreqName = m_objTempFreq.m_strFreqName;
                    objOrder.m_intFreqTime = m_objTempFreq.m_intTimes;
                    objOrder.m_intFreqDays = m_objTempFreq.m_intDays;
                }
                m_txtExecuteFreq.Text = objOrder.m_strExecFreqName;
            }
            else
            {
                objOrder.m_strExecFreqID = "";
                objOrder.m_strExecFreqName = "";
                m_txtExecuteFreq.Text = "";
                m_txtExecuteFreq.Tag = "";
            }

            //�÷�
            if (m_txtDosageType.Text.Trim() != "" && m_txtDosageType.Tag != null && m_txtDosageType.Tag.ToString().Trim() != "")
            {
                objOrder.m_strDosetypeID = clsConverter.ToString(m_txtDosageType.Tag);
                clsBSEUsageType m_objUsage = GetUsageVoByUsageID(objOrder.m_strDosetypeID);
                objOrder.m_strDosetypeName = m_objUsage.m_strUsageName;
            }
            else
            {
                objOrder.m_strDosetypeID = "";
                objOrder.m_strDosetypeName = "";
                m_txtDosageType.Text = "";
                m_txtDosageType.Tag = "";
            }
            //����
            //objOrder.m_strEntrust=m_txtEntrust.Text.Trim();

            objOrder.m_strParentID = "";
            objOrder.m_intStatus = 0;

            // ҩƷ��Դ: 0 ҩ��(ȫ�Ʒ�,��ҩ); 1 �����Ա�(ֻ�շ��÷�������Ŀ,����ҩ); 2 ���һ���(ȫ�Ʒѣ�����ҩ)
            objOrder.RateType = clsConverter.ToInt(m_cboRateType.m_strGetID(m_cboRateType.SelectedIndex));

            if (blnInputIsDoctor)
            {
                objOrder.m_strCreatorID = clsConverter.ToString(m_txtDoctorList.Tag);
                objOrder.m_strCreator = m_txtDoctorList.Text;
            }
            else
            {
                objOrder.m_strCreatorID = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpID;
                objOrder.m_strCreator = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpName;
            }

            objOrder.m_strDOCTORID_CHR = clsConverter.ToString(m_txtDoctorList.Tag);
            objOrder.m_strDOCTOR_VCHR = m_txtDoctorList.Text;

            objOrder.m_strCHARGEDOCTORGROUPID = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strGroupID;

            objOrder.m_dtCreatedate = DateTime.Now;
            m_txtInputDate.Text = objOrder.m_dtCreatedate.ToString("yyyy-MM-dd HH:mm");

            objOrder.m_strPosterId = "";
            objOrder.m_strPoster = "";

            objOrder.m_strExecutorID = "";
            objOrder.m_strExecutor = "";

            objOrder.m_strStoperID = "";
            objOrder.m_strStoper = "";
            objOrder.m_dtStopdate = DateTime.MinValue;

            objOrder.m_strRetractorID = "";
            objOrder.m_strRetractor = "";

            //����ҽ��
            objOrder.m_strParentName = m_txtFatherOrder.Text;
            if (objOrder.m_strParentName.Trim() != "")
                objOrder.m_strParentID = clsConverter.ToString(m_txtFatherOrder.Tag);
            else
                objOrder.m_strParentID = "";

            objOrder.m_strOrderDicCateID = clsConverter.ToString(m_picInfo.Tag);
            clsT_aid_bih_ordercate_VO p_objItem;
            p_objItem = (clsT_aid_bih_ordercate_VO)((frmBIHOrderInput)this.ParentForm).m_htOrderCate[objOrder.m_strOrderDicCateID];
            if (p_objItem != null)
            {
                objOrder.m_strOrderDicCateName = p_objItem.m_strVIEWNAME_VCHR;
            }
            try
            {
                objOrder.m_dtStartDate = Convert.ToDateTime(m_dtStartTime2.Text.ToString().Trim());
            }
            catch
            {
                objOrder.m_dtStartDate = DateTime.MinValue;
            }
            try
            {
                objOrder.m_dtFinishDate = DateTime.Parse(m_dtFinishTime2.Text.ToString().Trim());
            }
            catch
            {
                objOrder.m_dtFinishDate = DateTime.MinValue;
            }
            // �����ǰ¼����ֹͣʱ�䣬���õ�ǰ������Ϊֹͣ��
            if (objOrder.m_dtFinishDate != DateTime.MinValue)
            {
                objOrder.m_strStoperID = objOrder.m_strCreatorID;
                objOrder.m_strStoper = objOrder.m_strCreator;
                objOrder.m_dtStopdate = DateTime.Now;
            }
            objOrder.isYB_int = (m_chkIsMedicare.Checked) ? "1" : "0";
            if (!string.IsNullOrEmpty(m_txtSample.Tag.ToString()))
            {
                #region �޸�ҽ��������Ŀ����
                //Ī���� 2007.9.14 modify
                long lngRes = 0;
                DataTable dtbResult = null;

                //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetSampleByFindString(m_txtSample.Text, out dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    //m_txtSample.Tag = dtbResult.Rows[0]["sample_code"].ToString().Trim();
                    //m_txtSample.Text = dtbResult.Rows[0]["sample_name"].ToString().Trim();
                    //objOrder.m_strSAMPLEID_VCHR = (string)m_txtSample.Tag;
                    //objOrder.m_strSAMPLEName_VCHR = m_txtSample.Text;

                    //2014-3-17 ѡ������������ʱ�Ĵ���
                    string temp = m_txtSample.Text.ToString();
                    foreach (DataRow dtr in dtbResult.Rows)
                    {
                        if (dtr["sample_name"].ToString() == temp.Trim())
                        {
                            m_txtSample.Tag = dtr["sample_code"].ToString().Trim();
                            m_txtSample.Text = dtr["sample_name"].ToString().Trim();
                            objOrder.m_strSAMPLEID_VCHR = (string)m_txtSample.Tag;
                            objOrder.m_strSAMPLEName_VCHR = m_txtSample.Text;
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("�����޸�ʧ�ܣ���ȷ�ϼ�����Ƿ���ڸ�������Ϣ��");
                    objOrder.m_strSAMPLEID_VCHR = (string)m_txtSample.Tag;
                    objOrder.m_strSAMPLEName_VCHR = m_txtSample.Text;
                }
                #endregion
            }
            if (m_txtCheck.Tag != null)
            {
                objOrder.m_strPARTID_VCHR = (string)m_txtCheck.Tag;
                objOrder.m_strPARTNAME_VCHR = m_txtCheck.Text;
            }
            // ��ӿ���������Ϣ
            objOrder.m_strCREATEAREA_ID = (string)((frmBIHOrderInput)this.ParentForm).m_txtCREATEAREA.Tag;
            objOrder.m_strCREATEAREA_Name = (string)((frmBIHOrderInput)this.ParentForm).m_txtCREATEAREA.Text;
            //���� 
            if (m_txtDays.Enabled == true && m_txtDays.Visible == true)
            {
                try
                {
                    objOrder.m_intOUTGETMEDDAYS_INT = int.Parse(m_txtDays.Text.ToString().Trim());
                }
                catch
                {
                    objOrder.m_intOUTGETMEDDAYS_INT = 0;
                }
            }
            else
            {
                objOrder.m_intOUTGETMEDDAYS_INT = 0;
            }
            //����
            if (objOrder.m_intExecuteType == 1)
            {
                try
                {
                    string CateName = objOrder.m_strOrderDicCateName.Trim();
                    if (this.hasAppendViewType.ContainsKey(CateName))
                    {
                        if (this.hasAppendViewType[CateName].ToString().Trim() == "1")
                        {
                            objOrder.m_intATTACHTIMES_INT = 1;
                        }
                        else
                        {
                            objOrder.m_intATTACHTIMES_INT = 0;
                        }
                    }
                    else
                    {
                        objOrder.m_intATTACHTIMES_INT = 0;
                    }
                }
                catch
                {
                    objOrder.m_intATTACHTIMES_INT = 0;
                }
            }
            else
            {
                objOrder.m_intATTACHTIMES_INT = 0;
            }

            //ҽ�����ڹ�����ID
            objOrder.m_strDOCTORGROUPID_CHR = (string)((frmBIHOrderInput)this.ParentForm).m_strDOCTORGROUPID_CHR;
            //��ҽ��ʱ�������ڲ���ID
            objOrder.m_strCURAREAID_CHR = objPatient.m_strAreaID;
            //��ҽ��ʱ�������ڲ���ID
            objOrder.m_strCURBEDID_CHR = objPatient.m_strBedID;
            //ҽ��ǩ��
            if (((frmBIHOrderInput)this.ParentForm).m_intDoctorAutoSign == 1)
            {
                objOrder.SIGN_INT = 1;
            }
            else
            {
                objOrder.SIGN_INT = 0;
            }
            //ҽ��˵��
            objOrder.m_strREMARK_VCHR = m_txtREMARK_VCHR.Text.Trim();
            if (m_objTempFreq != null)
            {
                if (objOrder.m_intExecuteType == 1)
                {
                    objOrder.m_strEntrust = m_objTempFreq.m_strLExecTime;
                }
                else
                {
                    objOrder.m_strEntrust = m_objTempFreq.m_strTExecTime;
                }
            }
            //ҽ����Դ
            objOrder.m_intSOURCETYPE_INT = ((frmBIHOrderInput)this.ParentForm).m_intSOURCETYPE_INT;
            if (clsPublic.m_strGetSysparm("0008").Contains(objPatient.m_strPayTypeID))
            {
                if (this.cboShiying.Text.Trim() != "")
                {
                    objOrder.strShiying = this.cboShiying.Text.Substring(0, 1);
                }
            }
            else
            {
                objOrder.strShiying = "";
            }
            objOrder.AntiUse = this.cboKJ.SelectedIndex;
            objOrder.AntiUse_YFLX = this.cboQK.SelectedIndex;
            //objOrder.CureDays = (this.txtCureDays.Text.Trim() == "" ? 0 : Convert.ToInt32(this.txtCureDays.Text.Trim()));
            if (this.cboKJ.Enabled && this.cboKJ.SelectedIndex == 0)
            {
                MessageBox.Show("����ҩ�����ѡ����;.", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cboKJ.Focus();
                return null;
            }
            objOrder.IsProxyBoilMed = this.cboProxyBoil.SelectedIndex;
            objOrder.IsEmer = this.cboEmer.SelectedIndex;
            objOrder.IsOps = this.cboOps.SelectedIndex;
            return objOrder;
        }

        /// <summary>
        /// ����Ƶ��id��ȡƵ��VO
        /// </summary>
        /// <param name="m_strExecFreqID"></param>
        /// <returns></returns>
        public clsAIDRecipeFreq GetFreqVoByFreqID(string m_strExecFreqID)
        {
            if (m_htTempFreq == null)
            {
                m_htTempFreq = new Hashtable();
                if (m_arrFreq == null)
                {
                    //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                    (new weCare.Proxy.ProxyIP()).Service.m_lngGetRecipeFreq("", out m_arrFreq);
                }
                for (int i = 0; i < m_arrFreq.Length; i++)
                {
                    m_htTempFreq.Add(m_arrFreq[i].m_strFreqID, m_arrFreq[i]);
                }
            }
            clsAIDRecipeFreq m_objTemp = (clsAIDRecipeFreq)m_htTempFreq[m_strExecFreqID];
            if (m_objTemp == null)
            {
                m_objTemp = new clsAIDRecipeFreq();
            }
            return m_objTemp;
        }

        /// <summary>
        /// �����÷�id��ȡ�÷�VO
        /// </summary>
        /// <param name="m_strExecFreqID"></param>
        /// <returns></returns>
        public clsBSEUsageType GetUsageVoByUsageID(string m_strUsageID)
        {
            if (m_htTempUsage == null)
            {
                m_htTempUsage = new Hashtable();
                if (m_arrUsage == null)
                {
                    //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                    long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetUsageType("", out  m_arrUsage);
                }
                for (int i = 0; i < m_arrUsage.Length; i++)
                {
                    m_htTempUsage.Add(m_arrUsage[i].m_strUsageID, m_arrUsage[i]);
                }

            }
            clsBSEUsageType m_objUsage = (clsBSEUsageType)m_htTempUsage[m_strUsageID];
            if (m_objUsage == null)
            {
                m_objUsage = new clsBSEUsageType();
            }
            return m_objUsage;
        }

        /// <summary>
        /// ��ȡҽ��-(������Ŀתҽ��)
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns></returns>
        public clsBIHOrder m_objGetOrderByOrderDic(clsBIHPatientInfo objPatient, clsBIHOrderDic objDic)
        {
            clsBIHOrder objOrder = new clsBIHOrder();
            objOrder.m_strOrderID = "";//ҽ����ˮ��
            //����
            try
            {
                objOrder.m_intRecipenNo = Int32.Parse(m_txtRecipeNo.Text);

            }
            catch
            {
                objOrder.m_intRecipenNo = 0;

            }
            //��ʾ�ķ���
            try
            {
                objOrder.m_intRecipenNo2 = Int32.Parse(m_txtRecipeNo.Tag.ToString());
            }
            catch
            {
                objOrder.m_intRecipenNo2 = 0;

            }
            //����ҽ��1 ��ʱҽ��2 ��Ժ��ҩ3
            objOrder.m_intExecuteType = clsConverter.ToInt(m_cboExecuteType.m_strGetID(m_cboExecuteType.SelectedIndex));
            if (objOrder.m_intExecuteType == 3)
            {
                try
                {
                    objOrder.m_intOUTGETMEDDAYS_INT = Int32.Parse(m_txtDays.Text);
                }
                catch
                { }
            }
            //��ǰ����
            objOrder.m_strRegisterID = objPatient.m_strRegisterID;
            objOrder.m_strPatientID = objPatient.m_strPatientID;

            //������Ŀ��Ϣ  
            objOrder.m_strOrderDicID = objDic.m_strOrderDicID;
            objOrder.m_strName = objDic.m_strName;// ҽ������ 

            objOrder.m_strSpec = objDic.m_strSpec;//��Ŀ���	
            objOrder.m_strDosageUnit = objDic.m_strDosageUnit.Trim();//������λ
            if (objDic.m_intITEMSRCTYPE_INT == 1)//�����ҩ
            {
                if (objDic.m_intIPCHARGEFLG_INT == 1)
                {
                    objOrder.m_strUseunit = objDic.m_strUseUnit.Trim();
                    objOrder.m_strGetunit = objDic.m_strUseUnit.Trim();

                }
                else
                {
                    objOrder.m_strUseunit = objDic.m_strITEMOPUNIT_CHR;
                    objOrder.m_strGetunit = objDic.m_strITEMOPUNIT_CHR;

                }
            }
            else
            {
                objOrder.m_strUseunit = objDic.m_strITEMUNIT_CHR;
                objOrder.m_strGetunit = objDic.m_strITEMUNIT_CHR;
            }
            objOrder.m_strExecDeptID = objDic.m_strExecDept;	//ִ�п���
            objOrder.m_strExecDeptName = m_txtExecDept.Text;
            objOrder.m_dmlPrice = objDic.m_dmlPrice;//סԺ����;
            objOrder.m_dmlDosageRate = objDic.m_dmlDosageRate;//����
            //objOrder.m_dmlPACKQTY_DEC = clsConverter.ToDecimal(m_txtPackage.Text);//��װ��   
            //objOrder.m_intIPCHARGEFLG_INT = clsConverter.ToInt(m_txtPackage.Tag);//סԺ�շѵ�λ 0 ��������λ 1����С��λ
            objOrder.m_dmlPACKQTY_DEC = objDic.m_dmlPACKQTY_DEC;//��װ��   
            objOrder.m_intIPCHARGEFLG_INT = objDic.m_intIPCHARGEFLG_INT;//סԺ�շѵ�λ 0 ��������λ 1����С��λ

            // ����
            string m_strDosage = "";
            if (objDic.m_intITEMSRCTYPE_INT == 1)//�����ҩ(����ҩ�������,�ͱ���ԭҵֵ����)
            {
                if (m_intAge >= 12)
                {
                    m_strDosage = objDic.m_decADULTDOSAGE_DEC.ToString();
                }
                else
                {
                    m_strDosage = objDic.m_decCHILDDOSAGE_DEC.ToString();
                }

            }
            else
            {
                m_strDosage = objDic.m_dmlDosageRate.ToString();
            }
            //����
            objOrder.m_dmlDosage = clsConverter.ToDecimal(m_strDosage);

            //Ƶ��
            m_objTempFreq = GetFreqVoByFreqID(objDic.m_strFREQID_CHR);
            objOrder.m_strExecFreqID = m_objTempFreq.m_strFreqID;
            objOrder.m_strExecFreqName = m_objTempFreq.m_strFreqName;

            objOrder.m_intFreqTime = m_objTempFreq.m_intTimes;
            objOrder.m_intFreqDays = m_objTempFreq.m_intDays;
            //�÷�
            clsBSEUsageType m_objUsage = GetUsageVoByUsageID(objDic.m_strUsageID_chr);
            objOrder.m_strDosetypeID = m_objUsage.m_strUsageID;
            objOrder.m_strDosetypeName = m_objUsage.m_strUsageName;

            //����
            //objOrder.m_strEntrust = m_txtEntrust.Text.Trim();

            objOrder.m_strParentID = "";
            objOrder.m_intStatus = 0;

            // ҩƷ��Դ: 0 ҩ��(ȫ�Ʒ�,��ҩ); 1 �����Ա�(ֻ�շ��÷�������Ŀ,����ҩ); 2 ���һ���(ȫ�Ʒѣ�����ҩ) 
            objOrder.RateType = clsConverter.ToInt(m_cboRateType.m_strGetID(m_cboRateType.SelectedIndex));
            objOrder.m_strCreatorID = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpID;
            objOrder.m_strCreator = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpName;
            objOrder.m_strDOCTORID_CHR = clsConverter.ToString(m_txtDoctorList.Tag);
            objOrder.m_strDOCTOR_VCHR = m_txtDoctorList.Text;

            objOrder.m_strCHARGEDOCTORGROUPID = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strGroupID;

            objOrder.m_dtCreatedate = DateTime.Now;
            m_txtInputDate.Text = objOrder.m_dtCreatedate.ToString("yyyy-MM-dd HH:mm");

            objOrder.m_strPosterId = "";
            objOrder.m_strPoster = "";

            objOrder.m_strExecutorID = "";
            objOrder.m_strExecutor = "";

            objOrder.m_strStoperID = "";
            objOrder.m_strStoper = "";
            objOrder.m_dtStopdate = DateTime.MinValue;

            objOrder.m_strRetractorID = "";
            objOrder.m_strRetractor = "";

            //����ҽ��
            objOrder.m_strParentName = m_txtFatherOrder.Text;
            if (objOrder.m_strParentName.Trim() != "")
                objOrder.m_strParentID = clsConverter.ToString(m_txtFatherOrder.Tag);
            else
                objOrder.m_strParentID = "";

            objOrder.m_strOrderDicCateID = objDic.m_strOrderCateID;
            //�õ���ǰ��ҽ������
            clsT_aid_bih_ordercate_VO p_objItem;
            p_objItem = (clsT_aid_bih_ordercate_VO)((frmBIHOrderInput)this.ParentForm).m_htOrderCate[objDic.m_strOrderCateID];
            if (p_objItem != null)
            {
                objOrder.m_strOrderDicCateName = p_objItem.m_strVIEWNAME_VCHR;
            }
            //��ʼ�ͽ���ʱ��  
            try
            {
                objOrder.m_dtStartDate = Convert.ToDateTime(m_dtStartTime2.Text.ToString().Trim());
            }
            catch
            {
                objOrder.m_dtStartDate = DateTime.MinValue;
            }


            try
            {
                objOrder.m_dtFinishDate = DateTime.Parse(m_dtFinishTime2.Text.ToString().Trim());
            }
            catch
            {
                objOrder.m_dtFinishDate = DateTime.MinValue;
            }
            // �����ǰ¼����ֹͣʱ�䣬���õ�ǰ������Ϊֹͣ��
            if (objOrder.m_dtFinishDate != DateTime.MinValue)
            {
                objOrder.m_strStoperID = objOrder.m_strCreatorID;
                objOrder.m_strStoper = objOrder.m_strCreator;
                objOrder.m_dtStopdate = DateTime.Now;
            }
            if (m_txtSample.Tag != null)
            {
                objOrder.m_strSAMPLEID_VCHR = objDic.m_strSAMPLEID_VCHR;
                objOrder.m_strSAMPLEName_VCHR = objDic.m_strSAMPLE_NAME;
            }
            if (m_txtCheck.Tag != null)
            {
                objOrder.m_strPARTID_VCHR = objDic.m_strPARTID_VCHR;
                objOrder.m_strPARTNAME_VCHR = objDic.m_strPART_NAME;
            }
            // ��ӿ���������Ϣ
            objOrder.m_strCREATEAREA_ID = (string)((frmBIHOrderInput)this.ParentForm).m_txtCREATEAREA.Tag;
            objOrder.m_strCREATEAREA_Name = (string)((frmBIHOrderInput)this.ParentForm).m_txtCREATEAREA.Text;
            // ���������Σ���ʼ������ʱ��
            //����
            if (objOrder.m_intExecuteType == 3)
            {
                try
                {
                    objOrder.m_intOUTGETMEDDAYS_INT = int.Parse(m_txtDays.Text.ToString().Trim());
                }
                catch
                {
                    objOrder.m_intOUTGETMEDDAYS_INT = 0;
                }
            }
            else
            {
                objOrder.m_intOUTGETMEDDAYS_INT = 0;
            }
            //����
            if (objOrder.m_intExecuteType == 1)
            {
                try
                {
                    //����
                    string CateName = objOrder.m_strOrderDicCateName.Trim();
                    if (this.hasAppendViewType.ContainsKey(CateName))
                    {
                        if (this.hasAppendViewType[CateName].ToString().Trim() == "1")
                        {
                            objOrder.m_intATTACHTIMES_INT = 1;
                        }
                        else
                        {
                            objOrder.m_intATTACHTIMES_INT = 0;
                        }
                    }
                    else
                    {
                        objOrder.m_intATTACHTIMES_INT = 0;
                    }
                }
                catch
                {
                    objOrder.m_intATTACHTIMES_INT = 0;
                }
            }
            else
            {
                objOrder.m_intATTACHTIMES_INT = 0;
            }

            //ҽ�����ڹ�����ID
            objOrder.m_strDOCTORGROUPID_CHR = (string)((frmBIHOrderInput)this.ParentForm).m_strDOCTORGROUPID_CHR;
            //��ҽ��ʱ�������ڲ���ID
            objOrder.m_strCURAREAID_CHR = objPatient.m_strAreaID;
            //��ҽ��ʱ�������ڲ���ID
            objOrder.m_strCURBEDID_CHR = objPatient.m_strBedID;
            //ҽ��ǩ��
            if (((frmBIHOrderInput)this.ParentForm).m_intDoctorAutoSign == 1)
            {
                objOrder.SIGN_INT = 1;
            }
            else
            {
                objOrder.SIGN_INT = 0;
            }
            //ҽ��˵��
            objOrder.m_strREMARK_VCHR = m_txtREMARK_VCHR.Text.Trim();

            if (m_objTempFreq != null)
            {
                if (objOrder.m_intExecuteType == 1)
                {
                    objOrder.m_strEntrust = m_objTempFreq.m_strLExecTime;
                }
                else
                {
                    objOrder.m_strEntrust = m_objTempFreq.m_strTExecTime;
                }
            }
            //ҽ����Դ
            objOrder.m_intSOURCETYPE_INT = ((frmBIHOrderInput)this.ParentForm).m_intSOURCETYPE_INT;
            return objOrder;
        }

        /// <summary>
        /// Ϊ��ǰҽ�������������ü�һ�ε�����,����
        /// </summary>
        /// <param name="objOrder"></param>
        public void SetTheOrderGetMoust(ref clsBIHOrder objOrder)
        {
            clsT_aid_bih_ordercate_VO p_objItem;
            p_objItem = (clsT_aid_bih_ordercate_VO)((frmBIHOrderInput)this.ParentForm).m_htOrderCate[objOrder.m_strOrderDicCateID];

            ((frmBIHOrderInput)this.ParentForm).m_blTypeP = false;
            string m_strMEDICINEPREPTYPENAME_VCHR = "";//ҩƷ�Ƽ�����
            if (m_htMEDICINEPREPTYPE.ContainsKey(objOrder.m_strOrderDicID))
            {
                m_strMEDICINEPREPTYPENAME_VCHR = (string)m_htMEDICINEPREPTYPE[objOrder.m_strOrderDicID];
                if (m_strMEDICINEPREPTYPENAME_VCHR.Trim().Equals("Ƭ��"))
                {
                    ((frmBIHOrderInput)this.ParentForm).m_blTypeP = true;
                }
                else
                {
                    ((frmBIHOrderInput)this.ParentForm).m_blTypeP = false;
                }
            }
            else
            {
                //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                clsMEDICINEPREPTYPE_VO m_objMEDICINEPREPTYPE;
                (new weCare.Proxy.ProxyIP()).Service.m_lngGetPrepTypeP(objOrder.m_strOrderDicID, out m_objMEDICINEPREPTYPE);

                if (m_objMEDICINEPREPTYPE == null)
                {
                    ((frmBIHOrderInput)this.ParentForm).m_blTypeP = false;
                }
                else
                {
                    if (m_objMEDICINEPREPTYPE.m_strMEDICINEPREPTYPENAME_VCHR.Trim().Contains("Ƭ��"))
                    {
                        ((frmBIHOrderInput)this.ParentForm).m_blTypeP = true;
                    }

                }
            }
            //����  �����ҩƷ��Ϊ�ڷ��༰Ƶ�ʲ�Ϊ1��1�ε�Ĭ��Ϊ�����Σ����ϣ�
            //����  �����ҩƷ��Ϊ�÷�Ϊ��ע����Ƶ��Ϊ1��1�ε�Ĭ��Ϊ�����Σ�������
            clsBSEUsageType UsageVo = GetUsageVoByUsageID(objOrder.m_strDosetypeID);
            if (objOrder.m_intATTACHTIMES_INT == 1 && UsageVo.m_intPUTMED_INT == 1)//������������Ϊ���οɼ��ģ�����Ϊ1��
            {
                if (objOrder.m_intFreqDays == 1 && objOrder.m_intFreqTime == 1)//һ��һ��
                {
                    objOrder.m_intATTACHTIMES_INT = 1;
                }
                else
                {
                    objOrder.m_intATTACHTIMES_INT = 0;
                }
            }
            //ͬ������ҽ���Ĳ���
            if (IsSubOrder == true && ParentOrder != null)
            {
                objOrder.m_intATTACHTIMES_INT = ParentOrder.m_intATTACHTIMES_INT;
            }
            /*<========================*/
            decimal dmlGet = 0;	 //����
            decimal m_dmlSigle = 0;//��һ�ε�����
            int intDays = 1;		 //����-��Ժ��ҩ
            decimal dmlDosage = objOrder.m_dmlDosage;//ҽ���µļ���[һ�μ���]
            decimal dmlUse = -1;	 //����

            //decimal dmlUnitDosage = objOrder.m_dmlDosageRate;//���� [�շ���Ŀ���еļ�����DOSAGE_DEC��/ҽ�����е�use_dec]
            decimal dmlUnitDosage = objOrder.m_dmlPACKQTY_DEC;//��װ��
            decimal m_dmlDosageRate = objOrder.m_dmlDosageRate;//ԭ�������ļ���

            if (dmlUnitDosage <= 0 || objOrder.m_intIPCHARGEFLG_INT == 1)//סԺ�շѵ�λm_intIPCHARGEFLG_INT 0 ��������λ 1����С��λ
            {
                dmlUnitDosage = 1;
            }
            if (m_dmlDosageRate == 0)
            {
                m_dmlDosageRate = 1;
            }

            if (((frmBIHOrderInput)this.ParentForm).m_blTypeP == true)//�Ƿ�Ƭ���Ŀ���
            {

                bool m_blIsType;
                if (objOrder.m_intExecuteType == 3 || (p_objItem != null && p_objItem.m_strORDERCATEID_CHR.Equals(((frmBIHOrderInput)this.ParentForm).m_objSpecateVo.m_strMID_MEDICINE_CHR)))
                {
                    intDays = objOrder.m_intOUTGETMEDDAYS_INT;
                }

                string strFreqID = "";
                strFreqID = objOrder.m_strExecFreqID;
                if (strFreqID == string.Empty)
                {
                    dmlGet = 0;
                }
                long lngRes = 1;
                if (m_objTempFreq == null)
                {
                    m_objTempFreq = GetFreqVoByFreqID(strFreqID.Trim());
                }
                else if (m_objTempFreq.m_strFreqID.Trim() != strFreqID.Trim())
                {
                    m_objTempFreq = GetFreqVoByFreqID(strFreqID.Trim());
                }
                if (lngRes <= 0 || m_objTempFreq == null)
                {
                    dmlGet = 1;
                    m_dmlSigle = 1;
                }
                if (((frmBIHOrderInput)this.ParentForm).m_intTypePControl == 0)
                {
                    dmlUse = dmlDosage * m_objTempFreq.m_intTimes * intDays / m_dmlDosageRate;
                    //dmlGet = (dmlUse * m_objTempFreq.m_intTimes * intDays) / dmlUnitDosage;	//  ������(����*����*����/��װ��)
                    dmlGet = dmlUse / dmlUnitDosage;	//  ������(����*����*����/��װ��)

                }
                else
                {
                    // dmlGet = decimal.Ceiling(dmlUse / dmlUnitDosage) * m_objTempFreq.m_intTimes * intDays;	//  ������(����*����*����/��װ��)
                    dmlUse = decimal.Ceiling(dmlDosage / m_dmlDosageRate);
                    dmlUse = dmlUse * m_objTempFreq.m_intTimes * intDays;
                    dmlGet = dmlUse / dmlUnitDosage;
                }
                //ȡ��
                dmlGet = decimal.Ceiling(dmlGet);
                m_dmlSigle = decimal.Ceiling((dmlDosage / m_dmlDosageRate) / dmlUnitDosage);	//  ��һ�ε��� 
            }
            else
            {

                if (objOrder.m_intExecuteType == 3 || (p_objItem != null && p_objItem.m_strORDERCATEID_CHR.Equals(((frmBIHOrderInput)this.ParentForm).m_objSpecateVo.m_strMID_MEDICINE_CHR)))
                {
                    intDays = objOrder.m_intOUTGETMEDDAYS_INT;
                }
                if (dmlDosage < 0 || dmlUnitDosage <= 0) { dmlGet = 1; }
                string strFreqID = "";
                strFreqID = objOrder.m_strExecFreqID;
                if (strFreqID == string.Empty)
                {
                    dmlUse = 0;
                }
                long lngRes = 1;
                if (m_objTempFreq == null || m_objTempFreq.m_strFreqID.Trim() != strFreqID.Trim())
                {
                    m_objTempFreq = GetFreqVoByFreqID(strFreqID.Trim());
                }
                if (lngRes <= 0 || m_objTempFreq == null)
                {
                    dmlUse = 1;
                    dmlGet = dmlUse * intDays;
                }
                else
                {
                    dmlUse = decimal.Ceiling(dmlDosage / m_dmlDosageRate);
                    dmlGet = dmlUse * m_objTempFreq.m_intTimes * intDays / dmlUnitDosage;	//����*����
                }

                //ȡ��
                dmlGet = Decimal.Negate(decimal.Floor(decimal.Negate(dmlGet)));
                m_dmlSigle = Decimal.Negate(decimal.Floor(decimal.Negate(dmlUse / dmlUnitDosage))); ;	//  ��һ�ε���

            }
            if (dmlGet <= 0)
            {
                dmlGet = 1;
            }
            if (m_dmlSigle <= 0)
            {
                m_dmlSigle = 1;
            }
            objOrder.m_dmlGet = dmlGet;
            objOrder.m_dmlOneUse = m_dmlSigle;//��һ�ε���
            //������ҽ����ҽ��������ǿ��Ϊ��λ������Ƶ�ʴ���Ϊ1
            if (objOrder.m_strExecFreqID.Trim().Equals(((frmBIHOrderInput)this.ParentForm).m_objSpecateVo.m_strCONFREQID_CHR.Trim()))
            {
                objOrder.m_dmlGet = 1;
                objOrder.m_dmlOneUse = 1;
                objOrder.m_intFreqTime = 1;
                objOrder.m_intFreqDays = 1;
                objOrder.m_dmlDosage = 1;

            }
            else
            {
                objOrder.m_strExecFreqName = m_objTempFreq.m_strFreqName;
                objOrder.m_intFreqTime = m_objTempFreq.m_intTimes;
                objOrder.m_intFreqDays = m_objTempFreq.m_intDays;
            }
        }

        /// <summary>
        /// Ϊ��ǰҽ������һ�ε���������(��Ҫ�����������ɵ�ҽ����һ�������ļ���)
        /// </summary>
        /// <param name="objOrder"></param>
        public void SetTheOrderGetOneUseMoust(ref clsBIHOrder objOrder)
        {
            clsT_aid_bih_ordercate_VO p_objItem;
            p_objItem = (clsT_aid_bih_ordercate_VO)((frmBIHOrderInput)this.ParentForm).m_htOrderCate[objOrder.m_strOrderDicCateID];

            ((frmBIHOrderInput)this.ParentForm).m_blTypeP = false;
            string m_strMEDICINEPREPTYPENAME_VCHR = "";//ҩƷ�Ƽ�����
            if (m_htMEDICINEPREPTYPE.ContainsKey(objOrder.m_strOrderDicID))
            {
                m_strMEDICINEPREPTYPENAME_VCHR = (string)m_htMEDICINEPREPTYPE[objOrder.m_strOrderDicID];
                if (m_strMEDICINEPREPTYPENAME_VCHR.Trim().Equals("Ƭ��"))
                {
                    ((frmBIHOrderInput)this.ParentForm).m_blTypeP = true;
                }
                else
                {
                    ((frmBIHOrderInput)this.ParentForm).m_blTypeP = false;
                }
            }
            else
            {
                //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                clsMEDICINEPREPTYPE_VO m_objMEDICINEPREPTYPE;
                (new weCare.Proxy.ProxyIP()).Service.m_lngGetPrepTypeP(objOrder.m_strOrderDicID, out m_objMEDICINEPREPTYPE);
                if (m_objMEDICINEPREPTYPE == null)
                {
                    ((frmBIHOrderInput)this.ParentForm).m_blTypeP = false;
                }
                else
                {
                    if (m_objMEDICINEPREPTYPE.m_strMEDICINEPREPTYPENAME_VCHR.Trim().Contains("Ƭ��"))
                    {
                        ((frmBIHOrderInput)this.ParentForm).m_blTypeP = true;
                    }
                }
            }
            decimal dmlGet = 0;	 //����
            decimal m_dmlSigle = 0;//��һ�ε�����
            int intDays = 1;		 //����-��Ժ��ҩ
            decimal dmlDosage = objOrder.m_dmlDosage;//ҽ���µļ���[һ�μ���]
            decimal dmlUse = -1;	 //����

            //decimal dmlUnitDosage = objOrder.m_dmlDosageRate;//���� [�շ���Ŀ���еļ�����DOSAGE_DEC��/ҽ�����е�use_dec]
            decimal dmlUnitDosage = objOrder.m_dmlPACKQTY_DEC;//��װ��
            decimal m_dmlDosageRate = objOrder.m_dmlDosageRate;//ԭ�������ļ���

            if (dmlUnitDosage <= 0 || objOrder.m_intIPCHARGEFLG_INT == 1)//סԺ�շѵ�λm_intIPCHARGEFLG_INT 0 ��������λ 1����С��λ
            {
                dmlUnitDosage = 1;
            }
            if (m_dmlDosageRate == 0)
            {
                m_dmlDosageRate = 1;
            }

            if (((frmBIHOrderInput)this.ParentForm).m_blTypeP == true)//�Ƿ�Ƭ���Ŀ���
            {

                bool m_blIsType;
                if (objOrder.m_intExecuteType == 3 || (p_objItem != null && p_objItem.m_strORDERCATEID_CHR.Equals(((frmBIHOrderInput)this.ParentForm).m_objSpecateVo.m_strMID_MEDICINE_CHR)))
                {
                    intDays = objOrder.m_intOUTGETMEDDAYS_INT;
                }

                string strFreqID = "";
                strFreqID = objOrder.m_strExecFreqID;
                if (strFreqID == string.Empty)
                {
                    dmlGet = 0;
                }
                long lngRes = 1;
                if (m_objTempFreq == null)
                {
                    m_objTempFreq = GetFreqVoByFreqID(strFreqID.Trim());
                }
                else if (m_objTempFreq.m_strFreqID.Trim() != strFreqID.Trim())
                {
                    m_objTempFreq = GetFreqVoByFreqID(strFreqID.Trim());
                }
                if (lngRes <= 0 || m_objTempFreq == null)
                {
                    dmlGet = 1;
                    m_dmlSigle = 1;
                }
                if (((frmBIHOrderInput)this.ParentForm).m_intTypePControl == 0)
                {
                    dmlUse = dmlDosage * m_objTempFreq.m_intTimes * intDays / m_dmlDosageRate;
                    //dmlGet = (dmlUse * m_objTempFreq.m_intTimes * intDays) / dmlUnitDosage;	//  ������(����*����*����/��װ��)
                    dmlGet = dmlUse / dmlUnitDosage;	//  ������(����*����*����/��װ��)

                }
                else
                {
                    // dmlGet = decimal.Ceiling(dmlUse / dmlUnitDosage) * m_objTempFreq.m_intTimes * intDays;	//  ������(����*����*����/��װ��)
                    dmlUse = decimal.Ceiling(dmlDosage / m_dmlDosageRate);
                    dmlUse = dmlUse * m_objTempFreq.m_intTimes * intDays;
                    dmlGet = dmlUse / dmlUnitDosage;
                }
                //ȡ��
                dmlGet = decimal.Ceiling(dmlGet);
                m_dmlSigle = decimal.Ceiling((dmlDosage / m_dmlDosageRate) / dmlUnitDosage);	//  ��һ�ε��� 
            }
            else
            {

                if (objOrder.m_intExecuteType == 3 || (p_objItem != null && p_objItem.m_strORDERCATEID_CHR.Equals(((frmBIHOrderInput)this.ParentForm).m_objSpecateVo.m_strMID_MEDICINE_CHR)))
                {
                    intDays = objOrder.m_intOUTGETMEDDAYS_INT;
                }
                if (dmlDosage < 0 || dmlUnitDosage <= 0) { dmlGet = 1; }
                string strFreqID = "";
                strFreqID = objOrder.m_strExecFreqID;
                if (strFreqID == string.Empty)
                {
                    dmlUse = 0;
                }
                long lngRes = 1;
                if (m_objTempFreq == null || m_objTempFreq.m_strFreqID.Trim() != strFreqID.Trim())
                {
                    m_objTempFreq = GetFreqVoByFreqID(strFreqID.Trim());
                }
                if (lngRes <= 0 || m_objTempFreq == null)
                {
                    dmlUse = 1;
                    dmlGet = dmlUse * intDays;
                }
                else
                {
                    dmlUse = decimal.Ceiling(dmlDosage / m_dmlDosageRate);
                    dmlGet = dmlUse * m_objTempFreq.m_intTimes * intDays / dmlUnitDosage;	//����*����
                }

                //ȡ��
                dmlGet = Decimal.Negate(decimal.Floor(decimal.Negate(dmlGet)));
                m_dmlSigle = Decimal.Negate(decimal.Floor(decimal.Negate(dmlUse / dmlUnitDosage))); ;	//  ��һ�ε���

            }
            if (dmlGet <= 0)
            {
                dmlGet = 1;
            }
            if (m_dmlSigle <= 0)
            {
                m_dmlSigle = 1;
            }
            objOrder.m_dmlOneUse = m_dmlSigle;//��һ�ε���
            //������ҽ����ҽ��������ǿ��Ϊ��λ������Ƶ�ʴ���Ϊ1
            if (objOrder.m_strExecFreqID.Trim().Equals(((frmBIHOrderInput)this.ParentForm).m_objSpecateVo.m_strCONFREQID_CHR.Trim()))
            {
                objOrder.m_dmlGet = objOrder.m_dmlOneUse;
                objOrder.m_dmlOneUse = 1;
                objOrder.m_intFreqTime = 1;
                objOrder.m_intFreqDays = 1;
                objOrder.m_dmlDosage = 1;
            }
            else
            {
                objOrder.m_strExecFreqName = m_objTempFreq.m_strFreqName;
                objOrder.m_intFreqTime = m_objTempFreq.m_intTimes;
                objOrder.m_intFreqDays = m_objTempFreq.m_intDays;
            }
        }

        /// <summary>
        /// ��ȡҽ��-����ҽ������
        /// </summary>
        public clsBIHOrder[] m_objGetOrderGroup(clsBIHPatientInfo objPatient)
        {
            //clsBIHOrderGroupService m_objService2 = new clsDcl_GetSvcObject().m_GetOrderGroupSvcObject();
            if (!m_blnCurrentItemIsGroup) return null;

            string strGroupID = m_objCurrentGroup.m_strGroupID.Trim();
            clsBIHOrder[] arrOrder;
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetGroupItems(strGroupID, out arrOrder);
            if ((ret > 0) && (arrOrder != null))
            {
                int intExecType = clsConverter.ToInt(m_cboExecuteType.m_strGetID(m_cboExecuteType.SelectedIndex));
                //���ñ�־��{0��"";1���Ա�;2������;3������ҩ;4����Ժ��ҩ;}	        ||      ҩƷ��Դ: 1 ҩ��; 2 �����Ա�; 3 ���һ��� 			
                int intRateType = clsConverter.ToInt(m_cboRateType.m_strGetID(m_cboRateType.SelectedIndex));
                int intDays = 0;
                if (intRateType == 4)
                {
                    try
                    {
                        intDays = Int32.Parse(m_txtDays.Text);
                    }
                    catch
                    { }
                }
                //����:{1=�Ʒ�-��ҩ;2=�Ʒ�-����ҩ;3=���Ʒ�-��ҩ;4=���Ʒ�-����ҩ};ֻ������ʱҽ��
                int intIsRepare = 0;
                if (m_chkIsRepare.Checked)
                {
                    //���û��ѡ����Ĭ��Ϊ1=�Ʒ�-��ҩ;
                    intIsRepare = 1;
                    intIsRepare = m_cboRepare.SelectedIndex + 1;
                }

                string strDocID = clsConverter.ToString(m_txtDoctor.Tag).Trim();
                string strDocName = m_txtDoctor.Text.Trim();
                DateTime dtCreate = DateTime.Now;
                m_txtInputDate.Text = dtCreate.ToString("yyyy-MM-dd HH:mm");

                for (int i = 0; i < arrOrder.Length; i++)
                {
                    arrOrder[i].m_strOrderID = "";
                    arrOrder[i].m_intRecipenNo = 0;
                    arrOrder[i].m_strRegisterID = objPatient.m_strRegisterID;
                    arrOrder[i].m_strPatientID = objPatient.m_strPatientID;

                    arrOrder[i].m_intExecuteType = intExecType;

                    arrOrder[i].m_intExecuteType = intExecType;
                    arrOrder[i].m_intIsRepare = intIsRepare;

                    arrOrder[i].RateType = intRateType;
                    arrOrder[i].m_intOUTGETMEDDAYS_INT = intDays;

                    arrOrder[i].m_strCreator = strDocName;
                    arrOrder[i].m_strCreatorID = strDocID;
                    arrOrder[i].m_dtCreatedate = dtCreate;
                    arrOrder[i].m_strCHARGEDOCTORGROUPID = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strGroupID;
                    //�Ƿ�Ƥ��
                    arrOrder[i].m_intISNEEDFEEL = m_chkISNEEDFEEL.Checked ? 1 : 0;
                    arrOrder[i].AntiUse = this.cboKJ.SelectedIndex;
                    arrOrder[i].AntiUse_YFLX = this.cboQK.SelectedIndex;
                    //arrOrder[i].CureDays = (this.txtCureDays.Text.Trim() == "" ? 0 : Convert.ToInt32(this.txtCureDays.Text.Trim()));
                    if (this.cboKJ.Enabled && this.cboKJ.SelectedIndex == 0)
                    {
                        MessageBox.Show("����ҩ�����ѡ����;.", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.cboKJ.Focus();
                        return null;
                    }
                    arrOrder[i].IsProxyBoilMed = this.cboProxyBoil.SelectedIndex;
                    arrOrder[i].IsEmer = this.cboEmer.SelectedIndex;
                    arrOrder[i].IsOps = this.cboOps.SelectedIndex; 
                }
                return arrOrder;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// ��ʼ����
        /// </summary>
        public void m_mthStartInput()
        {
            EmptyInput();
            m_cboExecuteType.Focus();
        }
        /// <summary>
        /// �������,�����¼�
        /// </summary>
        private void m_mthEndInput()
        {
            if (m_evtInputEnd != null)
            {
                m_evtInputEnd(this, new EventArgs());
            }
        }

        private void m_mthInputNO()
        {
            if (m_evtInputNO != null)
            {
                m_evtInputNO(this, new EventArgs());
            }
        }

        public void InputOrder()
        {
            if (evtInputOrder != null)
            {
                evtInputOrder(this, new EventArgs());
            }
        }

        private void m_mthOnlyNumber(object sender, System.Windows.Forms.KeyPressEventArgs e)
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
        /// <summary>
        /// ʱ������ת��
        /// </summary>
        /// <param name="dtValue"></param>
        /// <returns></returns>
        private string DateTimeToString(DateTime dtValue)
        {
            if (dtValue.Date == DateTime.MinValue.Date)
                return "";
            else
                return dtValue.ToString("yyyy-MM-dd HH:mm");
        }

        public long m_mthGetDoctor(string p_strDoctorID)
        {
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            return (new weCare.Proxy.ProxyIP()).Service.m_lngGetHASPRESCRIPTIONRIGHT(p_strDoctorID);
        }

        /// <summary>
        /// �����Ƿ���ʵ��ʼ��ֹͣʱ��¼��ؼ�
        /// </summary>
        /// <param name="p_strFreqID">ִ��Ƶ��ID</param>
        private void DisplayDateTimePicker(string p_strFreqID, int p_intStatus)
        {
            if (m_lblSaveConOrderFreqID.Tag == null)
                m_lblSaveConOrderFreqID.Tag = new clsDcl_ExecuteOrder().m_strGetConfreqID();
            if (m_lblSaveConOrderFreqID.Tag.ToString().Trim() == p_strFreqID.Trim())
            {
                DateTime dtStart = System.DateTime.Now;
                DateTime dtEnd = System.DateTime.Now;
                if (m_TxbStartTime.Text.Trim() != "")
                {
                    try
                    {
                        dtStart = System.Convert.ToDateTime(m_TxbStartTime.Text);
                    }
                    catch { }
                }
                if (m_txbFinishTime.Text.Trim() != "")
                {
                    try
                    {
                        dtEnd = System.Convert.ToDateTime(m_txbFinishTime.Text);
                    }
                    catch { }
                }
                m_dtStartTime.Value = dtStart;
                m_dtFinishTime.Value = dtEnd;
                if (p_intStatus == 0) m_dtStartTime.Visible = true;//�½�ҽ���������뿪ʼʱ��
                if (p_intStatus == 2) m_dtFinishTime.Visible = true;//ֹͣҽ����������ֹͣʱ��
                //д������������
                m_txtDosage.Text = "1";
                m_txtDosage.Enabled = false;
                m_txtGet.Text = "1";
                m_txtGet2.Text = "";
                m_txtGet.Enabled = false;
                m_txtUse.Text = "1";
            }
            else
            {
                m_dtStartTime.Visible = false;
                m_dtFinishTime.Visible = false;
                if (m_txtDosage.Text.Trim() == "1") m_txtDosage.Enabled = true;
                if (m_txtGet.Text.Trim() == "1") m_txtGet.Enabled = true;
            }
        }
        /// <summary>
        /// ���ÿ�ʼ��ֹͣʱ��
        /// </summary>
        /// <param name="p_strFreqID"></param>
        /// <param name="p_objItem"></param>
        private void SetStartAndEndTime(string p_strFreqID, ref clsBIHOrder p_objItem)
        {
            if (m_lblSaveConOrderFreqID.Tag == null)
                m_lblSaveConOrderFreqID.Tag = new clsDcl_ExecuteOrder().m_strGetConfreqID();
            if (m_lblSaveConOrderFreqID.Tag.ToString().Trim() == p_strFreqID.Trim())
            {
                //��ʼʱ��
                if (m_dtStartTime.Visible)
                {
                    p_objItem.m_dtStartDate = m_dtStartTime.Value;
                }
                //ֹͣʱ��
                if (m_dtFinishTime.Visible)
                {
                    p_objItem.m_dtStopdate = m_dtFinishTime.Value;
                }
            }
        }
        /// <summary>
        /// ��֤������ҽ��
        /// ҵ��˵��: ������ҽ�������/����/������λ���붼�ǡ�Сʱ��!
        /// </summary>
        /// <param name="p_strFreqID"></param>
        /// <returns></returns>
        private bool PassConOrder(string p_strFreqID)
        {
            bool blnRes = true;
            if (m_lblSaveConOrderFreqID.Tag == null)
                m_lblSaveConOrderFreqID.Tag = new clsDcl_ExecuteOrder().m_strGetConfreqID();
            if (m_lblSaveConOrderFreqID.Tag.ToString().Trim() == p_strFreqID.Trim())
            {
                if (blnRes && m_txtDosageUnit.Text.Trim() != "Сʱ")
                {
                    MessageBox.Show("������ҽ��������������������λ����Ϊ��Сʱ��!", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    blnRes = false;
                }
                if (blnRes && m_txtUseUnit.Text.Trim() != "Сʱ")
                {
                    MessageBox.Show("������ҽ��������������������λ����Ϊ��Сʱ��!", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    blnRes = false;
                }
                if (blnRes && m_txtGetUnit.Text.Trim() != "Сʱ")
                {
                    MessageBox.Show("������ҽ��������������������λ����Ϊ��Сʱ��!", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    blnRes = false;
                }
            }
            return blnRes;
        }
        /// <summary>
        /// ҽ�������߼�
        /// </summary>
        /// <param name="p_strOrdercateID">ҽ������ID</param>
        /// <remarks>
        /// ҵ��˵����
        ///		1���÷���ʾ����{1=����;2=��},=2ʱ���Ʒѣ�
        ///		2��������ʾ����{1=����;2=��},=2ʱ�Ʒ�ʱȡ����Ϊ1��
        ///		3��ֻ��ҩƷ����ҽ��������Ƥ�ԣ�
        /// </remarks>
        public void OrdercateLogic(clsT_aid_bih_ordercate_VO p_objItem)
        {
            long lngRes = 1;
            if (lngRes <= 0 || p_objItem == null) return;
            //����ҽ�� m_intType=1
            //��ʱҽ�� m_intType=2
            //��Ժ��ҩ m_intType=3
            int m_intType = clsConverter.ToInt(m_cboExecuteType.m_strGetID(m_cboExecuteType.SelectedIndex));
            //������ʾ����{1=����;2=��},=2ʱ���Ʒѣ�
            if (p_objItem.m_intAPPENDVIEWTYPE_INT == 2)
            {
                m_txtATTACHTIMES_INT.Text = "0";
                m_txtATTACHTIMES_INT.Enabled = false;
            }
            else if (m_intType == 1)
            {
                m_txtATTACHTIMES_INT.Enabled = true;
            }
            //�÷���ʾ����{1=����;2=��},=2ʱ���Ʒѣ�
            if (p_objItem.m_intUSAGEVIEWTYPE == 2 && IsSubOrder == false)
            {
                m_txtDosageTypeControl(false);
            }
            else
            {
                m_txtDosageTypeControl(true);
            }
            //ִ��Ƶ����ʾ����{1=����;2=��}
            if (p_objItem.m_intExecuFrenquenceType == 2 && IsSubOrder == false)
            {
                m_txtExecuteFreqControl(false);
            }
            else
            {
                m_txtExecuteFreqControl(true);

            }
            //������ʾ���޸�
            if (clsConverter.ToInt(m_cboExecuteType.m_strGetID(m_cboExecuteType.SelectedIndex)) == 2)
            {
                if (IsSubOrder == false)
                {
                    //���û��Ĭ�ϵ�Ƶ�ʾ�ȡϵͳƵ��
                    clsAIDRecipeFreq m_objfreq = GetFreqVoByFreqID(((frmBIHOrderInput)this.ParentForm).m_objSpecateVo.m_strFREQID_CHR);
                    m_txtExecuteFreq.Tag = m_objfreq.m_strFreqID;
                    m_txtExecuteFreq.Text = m_objfreq.m_strFreqName;
                    this.m_txtExecuteFreq.Enabled = false;
                    this.m_txtExecuteFreq.ReadOnly = true;
                    m_hideExecuteFreq.Site = m_txtExecuteFreq.Site;
                    m_hideExecuteFreq.Location = m_txtExecuteFreq.Location;
                    m_hideExecuteFreq.Visible = true;
                }
                else
                {
                    this.m_txtExecuteFreq.Enabled = false;
                    this.m_txtExecuteFreq.ReadOnly = true;
                    m_hideExecuteFreq.Site = m_txtExecuteFreq.Site;
                    m_hideExecuteFreq.Location = m_txtExecuteFreq.Location;
                    m_hideExecuteFreq.Visible = true;
                    if (ParentOrder != null)
                    {
                        OrderSpecialLogic(ParentOrder);
                    }
                }
            }
            //������ʾ����{1=����;2=��},=2ʱ�Ʒ�ʱȡ����Ϊ1��
            if (p_objItem.m_intDOSAGEVIEWTYPE == 2 && IsSubOrder == false)
            {
                m_txtDosageControl(false);
            }
            else
            {
                m_txtDosageControl(true);
            }

            //������ʾ����{1=����;2=��},=2ʱ�Ʒ�ʱȡ����Ϊ1��
            if (p_objItem.m_intQTYVIEWTYPE_INT == 2)
            {
                m_txtGet.Enabled = false;
                m_txtGet.Text = "1";
            }
            else
            {
                m_txtGet.Enabled = true;
            }
        }

        /// <summary>
        /// ִ��Ƶ�ʿؼ�����ʾ����ÿ���
        /// </summary>
        /// <param name="m_blView">true-��ʾ,false-����ʾ</param>
        public void m_txtExecuteFreqControl(bool m_blView)
        {
            if (m_blView == true)
            {
                this.m_txtExecuteFreq.Enabled = true;
                this.m_txtExecuteFreq.ReadOnly = false;
                m_hideExecuteFreq.Visible = false;
            }
            else
            {
                this.m_txtExecuteFreq.Enabled = false;
                this.m_txtExecuteFreq.ReadOnly = true;
                m_hideExecuteFreq.Site = m_txtExecuteFreq.Site;
                m_hideExecuteFreq.Location = m_txtExecuteFreq.Location;
                m_hideExecuteFreq.Visible = true;
            }
        }

        /// <summary>
        /// �����ؼ�����ʾ����ÿ���
        /// </summary>
        /// <param name="m_blView">true-��ʾ,false-����ʾ</param>
        public void m_txtDosageControl(bool m_blView)
        {
            if (m_blView == true)
            {
                m_txtDosage.Enabled = true;
                m_hideDosage.Visible = false;
                m_hideDosageUnit.Visible = false;

            }
            else
            {
                m_txtDosage.Enabled = false;
                m_hideDosage.Site = m_txtDosage.Site;
                m_hideDosage.Location = m_txtDosage.Location;
                m_hideDosage.Visible = true;

                m_hideDosageUnit.Site = m_txtDosageUnit.Site;
                m_hideDosageUnit.Location = m_txtDosageUnit.Location;
                m_hideDosageUnit.Visible = true;
            }
        }

        /// <summary>
        /// �÷��ؼ�����ʾ����ÿ���
        /// </summary>
        /// <param name="m_blView">true-��ʾ,false-����ʾ</param>
        public void m_txtDosageTypeControl(bool m_blView)
        {
            if (m_blView == true)
            {
                m_txtDosageType.Enabled = true;
                m_txtDosageType.ReadOnly = false;
                m_hideDosageType.Visible = false;
            }
            else
            {
                m_txtDosageType.Enabled = false;
                m_txtDosageType.ReadOnly = true;
                m_hideDosageType.Site = m_txtDosageType.Site;
                m_hideDosageType.Location = m_txtDosageType.Location;
                m_hideDosageType.Visible = true;
            }
        }


        /// <summary>
        /// ��ҩ�÷��ؼ�����ʾ����ÿ���
        /// </summary>
        /// <param name="m_blView">true-��ʾ,false-����ʾ</param>
        public void m_txtMid_MedicineControl(bool m_blView)
        {
            if (m_blView == true)
            {
                m_txtDays.Visible = true;
                m_txtDays.Enabled = true;
                m_lblDay.Visible = true;
                m_lblDay.Text = "��";
            }

        }

        /// <summary>
        /// ����ҽ����������߼�
        /// </summary>
        /// <param name="order"></param>
        public void OrderSpecialLogic(clsBIHOrder order)
        {

            //ִ��Ƶ����ʾ����{1=����;2=��}   Ƶ���Ƿ���ʾ
            if (order.m_intCHARGE_INT == 1)
            {
                this.m_txtExecuteFreq.Enabled = true;
                this.m_txtExecuteFreq.ReadOnly = false;
                m_hideExecuteFreq.Visible = false;

                this.m_txtExecuteFreq.Tag = order.m_strExecFreqID;
                this.m_txtExecuteFreq.Text = order.m_strExecFreqName;
            }

        }
        /// <summary>
        /// �Ƿ�ҽ������
        /// </summary>
        private bool m_blnISMedicareMan = false;
        /// <summary>
        /// �Ƿ�ҽ������
        /// </summary>
        public bool IsMedicareMan
        {
            get
            {
                return m_blnISMedicareMan;
            }
            set
            {
                m_blnISMedicareMan = value;
            }
        }
        /// <summary>
        /// ��ʾ����ҩ
        /// ҵ��˵��: 
        ///		1����ҽ������¼���ҽ��ҩƷ�������Զ���Ϊ���Ա���;
        ///		2������ʱ����¼�룬��Ҫ�����趨�ò��˷�������;
        ///		3����ʾ�ס����ࣻ 
        ///		4����ʾ�Ƿ�ҽ����Ŀ 
        /// </summary>
        /// <param name="p_strOrderdicID"></param>
        public void DisplayXmclsa(string p_strUserCode)
        {
            string str = strGetXmclsa(p_strUserCode).Trim();
            if (str == "999")
            {
                this.m_chkIsMedicare.Checked = false;
                this.m_lblxmclsa.Text = "";
                this.m_lblxmclsa.Visible = false;
                //���ñ�־��{0��"";1���Ա�;2������;3������ҩ;4����Ժ��ҩ(������);}
                if (m_blnISMedicareMan)
                {
                    m_cboRateType.SelectedIndex = 1;//������ҽ��ҩ��Ĭ��Ϊ�Ա�ҩ
                }
                m_cboRateType.Enabled = true;
            }
            else
            {
                this.m_chkIsMedicare.Checked = true;
                this.m_lblxmclsa.Text = str;
                this.m_lblxmclsa.Visible = true;
                //���ñ�־��{0��"";1���Ա�;2������;3������ҩ;4����Ժ��ҩ(������);}
                m_cboRateType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// ����������Ŀid,��ʾҽ������
        /// </summary>
        /// <param name="orderDicId"></param>
        public void displayYB(string orderDicId)
        {
            string userCode = "";
            userCode = m_objInputOrder.getOrderDicUserCodeById(orderDicId);
            string str = strGetXmclsa(userCode);
            if (str == "999")
            {
                this.m_chkIsMedicare.Checked = false;
                this.m_lblxmclsa.Text = "";
                this.m_lblxmclsa.Visible = false;
            }
            else
            {
                this.m_chkIsMedicare.Checked = true;
                this.m_lblxmclsa.Text = str;
                this.m_lblxmclsa.Visible = true;
            }
        }
        /// <summary>
        /// ��ȡ�ס�����ҩ
        /// ����999 ,����ҽ����Ŀ
        /// </summary>
        /// <param name="p_strOrderdicID">������ĿID</param>
        /// <returns></returns>
        private string strGetXmclsa(string p_strUserCode)
        {
            long lngRes = 0;
            string strRes = "999";
            DataTable dt = new DataTable();
            lngRes = m_objInputOrder.m_lngGetMedicareByUserCode(p_strUserCode, out dt);
            if (lngRes > 0 && dt != null && dt.Rows.Count > 0)
            {
                switch (dt.Rows[0]["xmclsa"].ToString().Trim().ToUpper())
                {
                    case "F":
                        strRes = "����";
                        break;
                    case "O":
                        strRes = "����";
                        break;
                    case "T":
                        strRes = "����";
                        break;
                    default:
                        strRes = "";
                        break;
                }
            }
            return strRes;
        }
        /// <summary>
        /// Ƥ���Ƿ����
        /// </summary>
        /// <param name="p_strOrderTypeID">ҽ������ID</param>
        private void IsEnableNeedFeel(string p_strOrderTypeID)
        {
            if (p_strOrderTypeID == null)
            {
                this.m_chkISNEEDFEEL.Enabled = false;
                return;
            }

            string strMedicineOrderTypeID = "";
            if (this.m_chkISNEEDFEEL.Tag == null)
            {
                strMedicineOrderTypeID = new clsDcl_ExecuteOrder().m_strGetMedicineOrderTypeID().Trim();
                this.m_chkISNEEDFEEL.Tag = strMedicineOrderTypeID;
            }
            else
            {
                strMedicineOrderTypeID = this.m_chkISNEEDFEEL.Tag.ToString().Trim();
            }

            if (p_strOrderTypeID.Trim() == strMedicineOrderTypeID)
            {
                this.m_chkISNEEDFEEL.Enabled = true;
            }
            else
            {
                this.m_chkISNEEDFEEL.Enabled = false;
            }
        }
        #endregion

        private void m_chkIsMedicare_CheckedChanged(object sender, System.EventArgs e)
        {
            if (m_chkIsMedicare.Checked)
            {
                m_chkIsMedicare.BackColor = SystemColors.Desktop;
            }
            else
            {
                m_chkIsMedicare.BackColor = SystemColors.Control;
            }
        }

        private void m_txtExecuteFreq_EnabledChanged(object sender, EventArgs e)
        {
            if (m_txtExecuteFreq.Enabled)
                m_txtExecuteFreq.BackColor = Color.White;
            else
                m_txtExecuteFreq.BackColor = SystemColors.Control;
        }

        private void m_txtSample_m_evtInitListView(ListView lvwList)
        {
            lvwList.Columns.Clear();
            lvwList.Width = 420;
            lvwList.Height = 360;
            lvwList.HeaderStyle = ColumnHeaderStyle.Clickable;
            //�����ͷ
            lvwList.Columns.Add("����", 100, HorizontalAlignment.Center);
            lvwList.Columns.Add("��������", 300, HorizontalAlignment.Center);
        }


        private void m_txtSample_m_evtFindItem(object sender, string strFindCode, ListView lvwList)
        {
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            lvwList.Items.Clear();
            DataTable m_dtTemArr = null;
            if (!m_blSampleItem)
            {
                return;
            }
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetSampleByFindString(strFindCode, out m_dtTemArr);
            //����
            ListViewItem lsvItem = null;
            for (int iRow = 0; iRow < m_dtTemArr.Rows.Count; iRow++)
            {
                lsvItem = null;
                lsvItem = new ListViewItem(m_dtTemArr.Rows[iRow]["sample_code"].ToString().Trim());
                lsvItem.SubItems.Add(m_dtTemArr.Rows[iRow]["sample_name"].ToString());
                lsvItem.Tag = m_dtTemArr.Rows[iRow]["sample_code"].ToString();
                lvwList.Items.Add(lsvItem);
            }
        }

        private void m_txtSample_m_evtSelectItem(object sender, ListViewItem lviSelected)
        {
            if (lviSelected == null) return;
            m_txtSample.Text = lviSelected.SubItems[1].Text;
            m_txtSample.Tag = lviSelected.Tag.ToString();
            setTheControlOrder("m_txtSample");
        }

        private void m_txtCheck_m_evtInitListView(ListView lvwList)
        {
            lvwList.Columns.Clear();
            lvwList.Width = 420;
            lvwList.Height = 360;
            lvwList.HeaderStyle = ColumnHeaderStyle.Clickable;
            if (((frmBIHOrderInput)this.ParentForm).m_objCurrentOrder == null)
            {
                lvwList.CheckBoxes = true;
            }
            else
            {
                lvwList.CheckBoxes = false;
            }
            //�����ͷ
            lvwList.Columns.Add("����", 100, HorizontalAlignment.Center);
            lvwList.Columns.Add("��������", 300, HorizontalAlignment.Center);
        }

        private void m_txtCheck_m_evtFindItem(object sender, string strFindCode, ListView lvwList)
        {
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            lvwList.Items.Clear();
            this.m_txtCheck.m_CheckItem = null;
            DataTable m_dtTemArr = null;
            if (!m_blCheckItem)
            {
                return;
            }
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetCheckByFindString(strFindCode, out m_dtTemArr);
            //����
            ListViewItem lsvItem = null;
            clsBIHOrderDic m_objDic = null;
            if (m_txtOrderName.Tag is clsBIHOrderDic)
            {
                m_objDic = (clsBIHOrderDic)m_txtOrderName.Tag;
            }
            //���ڼ�����뵥Ԫʱ���в�λ����
            if (m_objDic.m_intCheckTypeID != -1 && m_objDic.m_strCheckType.Trim().Length > 0 && m_objDic.m_intDELETED == 0)
            {
            }
            else
            {
                m_objDic = null;
            }

            for (int iRow = 0; iRow < m_dtTemArr.Rows.Count; iRow++)
            {
                lsvItem = null;
                lsvItem = new ListViewItem(m_dtTemArr.Rows[iRow]["assistcode_chr"].ToString().Trim());
                lsvItem.SubItems.Add(m_dtTemArr.Rows[iRow]["partname"].ToString());
                lsvItem.Tag = m_dtTemArr.Rows[iRow]["partid"].ToString();
                if (m_objDic != null)
                {
                    if (m_objDic.m_intCheckTypeID.ToString().Equals(m_dtTemArr.Rows[iRow]["TYPEID"].ToString()))
                    {
                        lvwList.Items.Add(lsvItem);
                    }
                }
                else
                {
                    lvwList.Items.Add(lsvItem);
                }
            }
            if (m_txtCheck.Tag != null)
            {
                string m_strChecks = m_txtCheck.Tag.ToString();
                for (int i = 0; i < lvwList.Items.Count; i++)
                {
                    if (m_strChecks.Contains((string)lvwList.Items[i].Tag))
                    {
                        lvwList.Items[i].Checked = true;
                    }
                }
            }

        }

        private void m_txtCheck_m_evtSelectItem(object sender, ListViewItem lviSelected)
        {
            m_txtCheck.Text = "";
            m_txtCheck.Tag = "";
            if (m_txtCheck.m_CheckItem != null && m_txtCheck.m_CheckItem.Length > 0)
            {
                for (int i = 0; i < m_txtCheck.m_CheckItem.Length; i++)
                {
                    if (i > 0)
                    {
                        m_txtCheck.Text += ",";
                        m_txtCheck.Tag += ",";
                    }
                    m_txtCheck.Text += m_txtCheck.m_CheckItem[i].SubItems[1].Text;
                    m_txtCheck.Tag += m_txtCheck.m_CheckItem[i].Tag.ToString();
                }
            }
            else
            {
                if (lviSelected == null) return;
                m_txtCheck.Text = lviSelected.SubItems[1].Text;
                m_txtCheck.Tag = lviSelected.Tag.ToString();
            }
            setTheControlOrder("m_txtCheck");

        }

        #region ��ע��
        //		private void m_txbFinishTime_Leave(object sender, System.EventArgs e)
        //		{
        ////			if(m_txbFinishTime.ReadOnly==false)
        ////			{
        ////				if(m_txbFinishTime.Text.Trim()!="")
        ////				{
        ////					((frmBIHOrderInput)this.Parent.Parent).m_cmdStop_Click(null,null);
        ////				}
        ////				else
        ////				{
        ////					m_txbFinishTime.Focus();
        ////					m_txbFinishTime.Text="������ֹͣʱ��";
        ////					m_txbFinishTime.SelectAll();
        ////				}
        ////			}
        //	}
        //
        //		private void m_txbFinishTime_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //		{
        //			if(e.KeyCode==Keys.Enter)
        //			{
        //				if(m_txbFinishTime.ReadOnly==false)
        //				{
        //					((frmBIHOrderInput)this.Parent.Parent).m_cmdStop_Click(null,null);
        //				}
        //			}
        //		}
        #endregion

        /// <summary>
        /// ����ת��
        /// </summary>
        public void SetTheButtonTab()
        {
            if (m_txtDosageType.Enabled && m_txtDosageType.Visible)
            {
                m_txtDosageType.Focus();
            }
            else if (m_txtSample.Enabled && m_txtSample.Visible)
            {
                m_txtSample.Focus();
            }
            else if (m_txtCheck.Enabled && m_txtCheck.Visible)
            {
                m_txtCheck.Focus();
            }
            else
            {
                m_cboRateType.Focus();//SendKeys.Send("{TAB}");
            }
        }

        /// <summary>
        /// ��ҽ������Ĺ�����Ϣ��һ��ҽ��VO�У�Ϊ��ҽ�����崫��ֵʹ��
        /// </summary>
        /// <param name="order"></param>
        private void SetTheBaseOrder(out clsBIHOrder order)
        {
            order = new clsBIHOrder();

            int intExecType = clsConverter.ToInt(m_cboExecuteType.m_strGetID(m_cboExecuteType.SelectedIndex));
            //���ñ�־��{0��"";1���Ա�;2������;3������ҩ;4����Ժ��ҩ;}	        ||      ҩƷ��Դ: 1 ҩ��; 2 �����Ա�; 3 ���һ��� 			
            int intRateType = clsConverter.ToInt(m_cboRateType.m_strGetID(m_cboRateType.SelectedIndex));
            string strDocID = clsConverter.ToString(m_txtDoctor.Tag).Trim();
            string strDocName = m_txtDoctor.Text.Trim();
            DateTime dtCreate = DateTime.Now;
            m_txtInputDate.Text = dtCreate.ToString("yyyy-MM-dd HH:mm");

            order.m_strOrderID = "";
            order.m_intRecipenNo = 0;
            order.m_strRegisterID = ((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_objPatient.m_strRegisterID;
            order.m_strPatientID = ((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_objPatient.m_strPatientID;

            order.m_strCREATEAREA_ID = (string)((frmBIHOrderInput)this.ParentForm).m_txtCREATEAREA.Tag;
            order.m_strCREATEAREA_Name = (string)((frmBIHOrderInput)this.ParentForm).m_txtCREATEAREA.Text;

            order.m_intExecuteType = intExecType;
            order.RateType = intRateType;

            order.m_strCreator = strDocName;
            order.m_strCreatorID = strDocID;
            order.m_dtCreatedate = dtCreate;
            order.m_strCHARGEDOCTORGROUPID = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strGroupID;
            //�Ƿ�Ƥ��
            order.m_intISNEEDFEEL = m_chkISNEEDFEEL.Checked ? 1 : 0;

            // ���������Σ���ʼ������ʱ��
            //����
            if (order.m_intExecuteType == 3)
            {
                try
                {
                    order.m_intOUTGETMEDDAYS_INT = int.Parse(m_txtDays.Text.ToString().Trim());
                }
                catch
                {
                    order.m_intOUTGETMEDDAYS_INT = 0;
                }
            }
            else
            {
                order.m_intOUTGETMEDDAYS_INT = 0;
            }
            //����
            if (order.m_intExecuteType == 1)
            {
                try
                {
                    order.m_intATTACHTIMES_INT = int.Parse(m_txtATTACHTIMES_INT.Text.ToString().Trim());
                }
                catch
                {
                    order.m_intATTACHTIMES_INT = 0;
                }
            }
            else
            {
                order.m_intATTACHTIMES_INT = 0;
            }

            try
            {
                order.m_dtStartDate = DateTime.Parse(m_dtStartTime2.Text.ToString().Trim());
            }
            catch
            {
                order.m_dtStartDate = DateTime.MinValue;
            }
            try
            {
                order.m_dtFinishDate = DateTime.Parse(m_dtFinishTime2.Text.ToString().Trim());
            }
            catch
            {
                order.m_dtFinishDate = DateTime.MinValue;
            }

            //ҽ�����ڹ�����ID
            order.m_strDOCTORGROUPID_CHR = (string)((frmBIHOrderInput)this.ParentForm).m_strDOCTORGROUPID_CHR;

            order.m_strDOCTORID_CHR = clsConverter.ToString(m_txtDoctorList.Tag);
            order.m_strDOCTOR_VCHR = m_txtDoctorList.Text;
            //��ҽ��ʱ�������ڲ���ID
            order.m_strCURAREAID_CHR = (string)((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_objPatient.m_strAreaID;
            //��ҽ��ʱ�������ڲ���ID
            order.m_strCURBEDID_CHR = (string)((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_objPatient.m_strBedID;

            //ҽ��ǩ��
            if (((frmBIHOrderInput)this.ParentForm).m_intDoctorAutoSign == 1)
            {
                order.SIGN_INT = 1;
            }
            else
            {
                order.SIGN_INT = 0;
            }
        }

        private void m_txtRecipeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            { }
            else if ((e.KeyChar == 8) || (e.KeyChar == 13))
            { }
            else
            {
                e.Handled = true;
            }
        }

        private void m_txtRecipeNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int k = int.Parse(m_txtRecipeNo.Text);
            }
            catch
            {
                m_txtRecipeNo.Text = "";
                m_txtRecipeNo.Focus();
            }
        }

        private void m_txtRecipeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                setTheControlOrder(((TextBox)sender).Name);
            }
        }

        private void m_txtRecipeNo_Leave(object sender, EventArgs e)
        {
            m_mthInputNO();
        }

        private void m_txtOrderName2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                setTheControlOrder(((TextBox)sender).Name);
            }
        }

        #region �����¼�
        private void m_txtArea_m_evtFindItem(object sender, string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            m_txtArea.Tag = null;
            m_txtBedNo.Tag = null;
            m_txtBedNo.Text = "";
            clsBIHArea[] arrArea;
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngFindArea(strFindCode, out arrArea);
            if ((ret > 0) && (arrArea != null))
            {
                //��ȡ��Ȩ�޷��ʵĲ���ID����
                if (((frmBIHOrderInput)this.ParentForm).m_objLoginInfo != null)
                {
                    IList ilUsableAreaID = ((frmBIHOrderInput)this.ParentForm).m_objLoginInfo.m_ilUsableAreaID;
                    clsDcl_InputOrder objInputOrder = new clsDcl_InputOrder();
                    arrArea = (clsBIHArea[])(objInputOrder.GetUsableAreaObject(arrArea, ilUsableAreaID)).ToArray(typeof(clsBIHArea));
                }
                for (int i = 0; i < arrArea.Length; i++)
                {
                    /** @update by xzf (05-09-20)
                     * 
                     */
                    ListViewItem lvi = lvwList.Items.Add(arrArea[i].code);
                    lvi.SubItems.Add(arrArea[i].m_strAreaName);
                    /* <<================================== */
                    lvi.Tag = arrArea[i];
                }
            }
        }

        private void m_txtArea_m_evtInitListView(System.Windows.Forms.ListView lvwList)
        {
            //@lvwList.Columns.Add("",120,HorizontalAlignment.Left);
            //@lvwList.Width=140;
            /** update by xzf (05-09-20) */
            lvwList.Columns.Add("�������", 60, HorizontalAlignment.Left);
            lvwList.Columns.Add("��������", 100, HorizontalAlignment.Left);
            lvwList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            lvwList.Width = 180;
            /* <<================================= */
        }

        private void m_txtArea_m_evtSelectItem(object sender, System.Windows.Forms.ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                m_txtArea.Text = lviSelected.SubItems[1].Text;
                m_txtArea.Tag = lviSelected.Tag;

                m_txtBedNo.Text = "";
                m_txtBedNo.Tag = null;

                m_txtBedNo.Focus();
            }
        }
        #endregion

        #region ��λ���¼�
        private void m_txtBedNo2_m_evtFindItem(object sender, string strFindCode, ListView lvwList)
        {
            for (int i1 = 0; i1 < ((frmBIHOrderInput)this.ParentForm).m_dtvOrder.RowCount; i1++)
            {
                clsBIHOrder order1 = (clsBIHOrder)((frmBIHOrderInput)this.ParentForm).m_dtvOrder.Rows[i1].Tag;
                if (order1.m_intStatus == 0)
                {
                    if (MessageBox.Show("��ǰ��δ�ύ��ҽ��,ȷʵҪ���ĵ�ǰ������", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        //if (!oldBedNoValue.Trim().Equals(""))
                        //{

                        //    m_txtBedNo.Text = oldBedNoValue;
                        //    oldBedNoValue = "";
                        //}
                        if (m_txtBedNo.Tag != null)
                        {
                            m_txtBedNo.Text = ((clsBIHBed)this.m_txtBedNo.Tag).m_strBedName;
                        }
                        return;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            this.m_txtBedNo.Tag = null;
            //clsBIHOrderService m_objService = new clsBIHOrderService();
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            /*<----------------------------------------*/

            if (m_txtArea.Tag == null)
            {
                //if (m_blnPrompt) MessageBox.Show("����ָ������!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("����ָ������!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_txtArea.Text = "";
                m_txtArea.Tag = null;
                m_txtArea.Focus();
                return;
            }
            string strAreaID = (m_txtArea.Tag as clsBIHArea).m_strAreaID;
            clsBIHBed[] arrBed;
            string strBedNo = m_txtBedNo.Text.Trim();
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetBedByArea(strAreaID, strBedNo, out arrBed);
            if ((ret > 0) && (arrBed != null))
            {
                if (arrBed.Length == 0)
                {
                    MessageBox.Show("��ǰ����û�д�λ��������ѡ����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_txtBedNo.Focus();
                    return;
                }
                for (int i = 0; i < arrBed.Length; i++)
                {
                    ListViewItem objItem = new ListViewItem(arrBed[i].m_strBedName);
                    objItem.SubItems.Add(arrBed[i].m_objPatient.m_strPatientName);
                    objItem.SubItems.Add(arrBed[i].m_objPatient.m_strSex);
                    objItem.Tag = arrBed[i];
                    lvwList.Items.Add(objItem);
                }
            }
        }

        private void m_txtBedNo2_m_evtInitListView(ListView lvwList)
        {
            lvwList.Columns.Add("������", 40, HorizontalAlignment.Left);
            lvwList.Columns.Add("�ա���", 80, HorizontalAlignment.Left);
            lvwList.Columns.Add("�ԡ���", 40, HorizontalAlignment.Left);
            lvwList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            lvwList.Width = 180;
        }

        private void m_txtBedNo2_m_evtSelectItem(object sender, ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {

                m_txtBedNo.Text = lviSelected.SubItems[0].Text;
                m_txtBedNo.Tag = lviSelected.Tag;
                ((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_txtArea.Tag = m_txtArea.Tag;
                ((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_txtBedNo.Tag = m_txtBedNo.Tag;
                ((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_mthGetPatientByAreaBed();

            }

        }
        #endregion

        string oldBedNoValue = "";
        private void m_txtBedNo_DoubleClick(object sender, EventArgs e)
        {
            oldBedNoValue = m_txtBedNo.Text;
            m_txtBedNo.Text = "";
            SendKeys.Send("{ENTER}");
        }

        private void m_txtArea_DoubleClick(object sender, EventArgs e)
        {
            oldBedNoValue = m_txtBedNo.Text;
            m_txtArea.Text = "";
            SendKeys.Send("{ENTER}");
        }

        private void m_btnBedList_Click(object sender, EventArgs e)
        {
            m_txtBedNo.Focus();
            m_txtBedNo_DoubleClick(null, null);
        }

        private void m_txtDoctorList_m_evtFindItem(object sender, string strFindCode, ListView lvwList)
        {
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            clsBIHDoctor[] m_objDoctorArr;
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetDoctorsList(strFindCode, m_objCurrentDoctor.m_strDoctorID, out m_objDoctorArr);
            if ((ret > 0))
            {
                for (int i = 0; i < m_objDoctorArr.Length; i++)
                {
                    ListViewItem lvi = lvwList.Items.Add(m_objDoctorArr[i].m_strDoctorNo);
                    lvi.SubItems.Add(m_objDoctorArr[i].m_strDoctorName);
                    lvi.Tag = m_objDoctorArr[i].m_strDoctorID;
                }
            }
        }

        private void m_txtDoctorList_m_evtInitListView(ListView lvwList)
        {
            lvwList.Columns.Add("���", 60, HorizontalAlignment.Left);
            lvwList.Columns.Add("ҽ������", 100, HorizontalAlignment.Left);
            //lvwList.Columns.Add("ƴ����", 60, HorizontalAlignment.Left);

            lvwList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            lvwList.Width = 170;
        }

        private void m_txtDoctorList_m_evtSelectItem(object sender, ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                m_txtDoctorList.Text = lviSelected.SubItems[1].Text;
                m_txtDoctorList.Tag = lviSelected.Tag;

                setTheControlOrder("m_txtDoctorList");
                // ��ѡ����һҽ���󣬽��ŵĲ�����Ĭ��Ϊ��ǰҽ��
                ((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_objPatient.m_strDOCTORID_CHR = (string)lviSelected.Tag;
                ((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_objPatient.m_strDOCTOR_VCHR = lviSelected.SubItems[1].Text;
            }
        }

        private void m_txtATTACHTIMES_INT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                try
                {
                    int i1 = int.Parse(m_txtATTACHTIMES_INT.Text.ToString().Trim());
                }
                catch
                {
                    m_txtATTACHTIMES_INT.Focus();
                    return;
                }
                setTheControlOrder(((TextBox)sender).Name);
            }
        }

        private void m_dtStartTime2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                int i = clsConverter.ToInt(m_cboExecuteType.m_strGetID(m_cboExecuteType.SelectedIndex));
                if (i == 1)
                {
                    setTheControlOrder("m_dtFinishTime2");
                }
                else
                {
                    setTheControlOrder("m_dtStartTime2");
                }
            }
        }

        private void m_dtFinishTime2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                m_dtFinishTime2_Leave(null, null);
            }
        }

        private void m_txtOrderName2_DoubleClick(object sender, EventArgs e)
        {
            m_txtOrderName2.ReadOnly = m_blOrderName2ReadOnly;
        }

        internal void m_objGetCommitOrder(clsBIHPatientInfo objPatient, clsBIHOrder objBihOrder, ref clsCommitOrder objOrder)
        {
            /*�����޸ĵ��ֶ�Ϊ��
            objOrder.m_intRecipenNo  m_intExecuteType m_strOrderDicID m_strName m_strSpec
            m_strDosageUnit m_strUseunit m_strGetunit m_strExecDeptID m_strExecDeptName
            m_intIsRich m_dmlPrice m_dmlDosageRate m_dmlDosage m_dmlUse
            m_dmlGet m_strExecFreqID m_strExecFreqName m_strDosetypeID m_strDosetypeName
            m_strEntrust m_intRateType m_strDOCTORID_CHR m_strDOCTOR_VCHR m_strParentName
            m_strParentID m_strOrderDicCateID m_strSAMPLEID_VCHR m_strSAMPLEName_VCHR m_strPARTID_VCHR
            m_strPARTNAME_VCHR m_strCREATEAREA_ID  m_strCREATEAREA_Name m_intOUTGETMEDDAYS_INT m_intATTACHTIMES_INT
            m_dtStartDate m_dtFinishDate m_strDOCTORGROUPID_CHR  m_strCURAREAID_CHR m_strCURBEDID_CHR
            *<==================================================*/

            objOrder.m_strOrderID = objBihOrder.m_strOrderID;
            objOrder.m_strAge = objPatient.m_strAge;
            objOrder.m_strAreaID = objPatient.m_strAreaID;
            objOrder.m_strAreaName = objPatient.m_strAreaName;
            objOrder.m_strPatientID = objPatient.m_strPatientID;
            objOrder.m_strPatientName = objPatient.m_strPatientName;
            objOrder.m_strsex_chr = objPatient.m_strSex;
            objOrder.m_strDIAGNOSE_VCHR = objPatient.m_strDiagnose;

            //����
            try
            {
                objOrder.m_intRecipenNo = Int32.Parse(m_txtRecipeNo.Text);
            }
            catch
            {
                objOrder.m_intRecipenNo = 0;
            }
            //��ʾ�ķ���
            try
            {
                objOrder.m_intRecipenNo2 = Int32.Parse(m_txtRecipeNo.Tag.ToString());
            }
            catch
            {
                objOrder.m_intRecipenNo2 = 0;
            }

            //����ҽ��1 ��ʱҽ��2 ��Ժ��ҩ3
            objOrder.m_intExecuteType = clsConverter.ToInt(m_cboExecuteType.m_strGetID(m_cboExecuteType.SelectedIndex));

            //��ǰ����
            //������Ŀ��Ϣ
            objOrder.m_strOrderDicID = clsConverter.ToString(m_txtOrderName2.Tag);
            objOrder.m_strName = m_txtOrderName.Text;//�����޸ġ�
            objOrder.m_strSpec = m_txtOrderSpec.Text;
            objOrder.m_strDosageUnit = m_txtDosageUnit.Text;
            objOrder.m_strUseunit = m_txtUseUnit.Text;
            objOrder.m_strGetunit = m_txtGetUnit.Text;
            objOrder.m_strExecDeptID = clsConverter.ToString(m_txtExecDept.Tag);		//ִ�п���
            objOrder.m_strExecDeptName = m_txtExecDept.Text;
            objOrder.m_intIsRich = (m_chkIsRich.Checked ? 1 : 0);
            objOrder.m_intISNEEDFEEL = (m_chkISNEEDFEEL.Checked ? 1 : 0);
            objOrder.m_dmlPrice = clsConverter.ToDecimal(m_txtPrice.Text);
            objOrder.m_dmlDosageRate = clsConverter.ToDecimal(m_txtPrice.Tag);
            objOrder.m_dmlPACKQTY_DEC = clsConverter.ToDecimal(m_txtPackage.Text);//��װ��   
            objOrder.m_intIPCHARGEFLG_INT = clsConverter.ToInt(m_txtPackage.Tag);//סԺ�շѵ�λ 0 ��������λ 1����С��λ
            //����
            objOrder.m_dmlDosage = clsConverter.ToDecimal(m_txtDosage.Text);
            objOrder.m_dmlUse = clsConverter.ToDecimal(m_txtUse.Text);
            objOrder.m_dmlGet = clsConverter.ToDecimal(m_txtGet.Text);

            //Ƶ��
            if (m_txtExecuteFreq.Text.Trim() != "" && m_txtExecuteFreq.Tag != null && m_txtExecuteFreq.Tag.ToString().Trim() != "")
            {
                objOrder.m_strExecFreqID = clsConverter.ToString(m_txtExecuteFreq.Tag);
                m_objTempFreq = GetFreqVoByFreqID(objOrder.m_strExecFreqID);
                objOrder.m_strExecFreqName = m_objTempFreq.m_strFreqName;
                objOrder.m_intFreqTime = m_objTempFreq.m_intTimes;
                objOrder.m_intFreqDays = m_objTempFreq.m_intDays;
            }
            else
            {
                objOrder.m_strExecFreqID = "";
                objOrder.m_strExecFreqName = "";
            }
            //�÷�
            if (m_txtDosageType.Text.Trim() != "" && m_txtDosageType.Tag != null && m_txtDosageType.Tag.ToString().Trim() != "")
            {
                objOrder.m_strDosetypeID = clsConverter.ToString(m_txtDosageType.Tag);
                clsBSEUsageType m_objUsage = GetUsageVoByUsageID(objOrder.m_strDosetypeID);
                objOrder.m_strDosetypeName = m_objUsage.m_strUsageName;
            }
            else
            {
                objOrder.m_strDosetypeID = "";
                objOrder.m_strDosetypeName = "";
            }
            //����
            objOrder.m_strEntrust = m_txtEntrust.Text.Trim();

            // ҩƷ��Դ: 0 ҩ��(ȫ�Ʒ�,��ҩ); 1 �����Ա�(ֻ�շ��÷�������Ŀ,����ҩ); 2 ���һ���(ȫ�Ʒѣ�����ҩ)
            objOrder.RateType = clsConverter.ToInt(m_cboRateType.m_strGetID(m_cboRateType.SelectedIndex));
            objOrder.m_strDOCTORID_CHR = clsConverter.ToString(m_txtDoctorList.Tag);
            objOrder.m_strDOCTOR_VCHR = m_txtDoctorList.Text;

            //����ҽ��
            objOrder.m_strParentName = m_txtFatherOrder.Text;
            if (objOrder.m_strParentName.Trim() != "")
                objOrder.m_strParentID = clsConverter.ToString(m_txtFatherOrder.Tag);
            else
                objOrder.m_strParentID = "";

            objOrder.m_strOrderDicCateID = clsConverter.ToString(m_picInfo.Tag);
            clsT_aid_bih_ordercate_VO p_objItem;
            p_objItem = (clsT_aid_bih_ordercate_VO)((frmBIHOrderInput)this.ParentForm).m_htOrderCate[objOrder.m_strOrderDicCateID];
            if (p_objItem != null)
            {
                objOrder.m_strOrderDicCateName = p_objItem.m_strVIEWNAME_VCHR;
            }
            if (m_txtSample.Tag != null)
            {
                objOrder.m_strSAMPLEID_VCHR = (string)m_txtSample.Tag;
                objOrder.m_strSAMPLEName_VCHR = m_txtSample.Text;
            }
            if (m_txtCheck.Tag != null)
            {
                objOrder.m_strPARTID_VCHR = (string)m_txtCheck.Tag;
                objOrder.m_strPARTNAME_VCHR = m_txtCheck.Text;
            }
            // ��ӿ���������Ϣ
            objOrder.m_strCREATEAREA_ID = (string)((frmBIHOrderInput)this.ParentForm).m_txtCREATEAREA.Tag;
            objOrder.m_strCREATEAREA_Name = (string)((frmBIHOrderInput)this.ParentForm).m_txtCREATEAREA.Text;
            // ���������Σ���ʼ������ʱ��
            //���� 
            if (m_txtDays.Enabled == true && m_txtDays.Visible == true)
            {
                try
                {
                    objOrder.m_intOUTGETMEDDAYS_INT = int.Parse(m_txtDays.Text.ToString().Trim());
                }
                catch
                {
                    objOrder.m_intOUTGETMEDDAYS_INT = 0;
                }
            }
            else
            {
                objOrder.m_intOUTGETMEDDAYS_INT = 0;
            }
            //����
            if (objOrder.m_intExecuteType == 1)
            {
                try
                {
                    objOrder.m_intATTACHTIMES_INT = int.Parse(m_txtATTACHTIMES_INT.Text.ToString().Trim());
                }
                catch
                {
                    objOrder.m_intATTACHTIMES_INT = 0;
                }
            }
            else
            {
                objOrder.m_intATTACHTIMES_INT = 0;
            }
            //��ʼ������ʱ��
            try
            {
                objOrder.m_dtStartDate = DateTime.Parse(m_dtStartTime2.Text.ToString().Trim());
            }
            catch
            {
                objOrder.m_dtStartDate = DateTime.MinValue;
            }
            try
            {
                objOrder.m_dtFinishDate = DateTime.Parse(m_dtFinishTime2.Text.ToString().Trim());
            }
            catch
            {
                objOrder.m_dtFinishDate = DateTime.MinValue;
            }

            // �����ǰ¼����ֹͣʱ�䣬���õ�ǰ������Ϊֹͣ��
            if (objOrder.m_dtFinishDate != DateTime.MinValue)
            {
                objOrder.m_strStoperID = objOrder.m_strCreatorID;
                objOrder.m_strStoper = objOrder.m_strCreator;
                objOrder.m_dtStopdate = DateTime.Now;
            }
            else
            {
                objOrder.m_strStoperID = "";
                objOrder.m_strStoper = "";
                objOrder.m_dtStopdate = DateTime.MinValue;
            }
            //ҽ�����ڹ�����ID
            objOrder.m_strDOCTORGROUPID_CHR = (string)((frmBIHOrderInput)this.ParentForm).m_strDOCTORGROUPID_CHR;
            //��ҽ��ʱ�������ڲ���ID
            objOrder.m_strCURAREAID_CHR = objPatient.m_strAreaID;
            //��ҽ��ʱ�������ڲ���ID
            objOrder.m_strCURBEDID_CHR = objPatient.m_strBedID;
            //˵��
            objOrder.m_strREMARK_VCHR = m_txtREMARK_VCHR.Text.Trim();
            //�޸���ID
            objOrder.m_strChangedID_CHR = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpID;
            //�޸�������
            objOrder.m_strChangedName_CHR = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpName;

            // �޸ļ���ҽ��ʱ���뵥��ʧ
            objOrder.m_strBedID = (string)((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_objPatient.m_strBedID;
            objOrder.m_strBedName = (string)((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_objPatient.m_strBedName;
            objOrder.m_strINPATIENTID_CHR = (string)((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_objPatient.m_strInHospitalNo;
            objOrder.m_strCreatorID = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpID;

            objOrder.AntiUse = this.cboKJ.SelectedIndex;
            objOrder.AntiUse_YFLX = this.cboQK.SelectedIndex;
            //objOrder.CureDays = (this.txtCureDays.Text.Trim() == "" ? 0 : Convert.ToInt32(this.txtCureDays.Text.Trim()));
            if (this.cboKJ.Enabled && this.cboKJ.SelectedIndex == 0)
            {
                MessageBox.Show("����ҩ�����ѡ����;.", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cboKJ.Focus();
            }
            objOrder.IsProxyBoilMed = this.cboProxyBoil.SelectedIndex;
            objOrder.IsEmer = this.cboEmer.SelectedIndex;
            objOrder.IsOps = this.cboOps.SelectedIndex; 
        }

        internal void m_objGetChangedOrder(clsBIHPatientInfo objPatient, ref clsBIHOrder objOrder)
        {
            /*�����޸ĵ��ֶ�Ϊ��
            objOrder.m_intRecipenNo  m_intExecuteType m_strOrderDicID m_strName m_strSpec
            m_strDosageUnit m_strUseunit m_strGetunit m_strExecDeptID m_strExecDeptName
            m_intIsRich m_dmlPrice m_dmlDosageRate m_dmlDosage m_dmlUse
            m_dmlGet m_strExecFreqID m_strExecFreqName m_strDosetypeID m_strDosetypeName
            m_strEntrust m_intRateType m_strDOCTORID_CHR m_strDOCTOR_VCHR m_strParentName
            m_strParentID m_strOrderDicCateID m_strSAMPLEID_VCHR m_strSAMPLEName_VCHR m_strPARTID_VCHR
            m_strPARTNAME_VCHR m_strCREATEAREA_ID  m_strCREATEAREA_Name m_intOUTGETMEDDAYS_INT m_intATTACHTIMES_INT
            m_dtStartDate m_dtFinishDate m_strDOCTORGROUPID_CHR  m_strCURAREAID_CHR m_strCURBEDID_CHR
            *<==================================================*/

            //����
            try
            {
                objOrder.m_intRecipenNo = Int32.Parse(m_txtRecipeNo.Text);
            }
            catch
            {
                objOrder.m_intRecipenNo = 0;
            }
            //��ʾ�ķ���
            try
            {
                objOrder.m_intRecipenNo2 = Int32.Parse(m_txtRecipeNo.Tag.ToString());
            }
            catch
            {
                objOrder.m_intRecipenNo2 = 0;
            }

            //����ҽ��1 ��ʱҽ��2 ��Ժ��ҩ3
            objOrder.m_intExecuteType = clsConverter.ToInt(m_cboExecuteType.m_strGetID(m_cboExecuteType.SelectedIndex));

            //��ǰ����
            //������Ŀ��Ϣ
            objOrder.m_strOrderDicID = clsConverter.ToString(m_txtOrderName2.Tag);
            objOrder.m_strName = m_txtOrderName.Text;//�����޸ġ�
            objOrder.m_strSpec = m_txtOrderSpec.Text;
            objOrder.m_strDosageUnit = m_txtDosageUnit.Text;
            objOrder.m_strUseunit = m_txtUseUnit.Text;
            objOrder.m_strGetunit = m_txtGetUnit.Text;
            objOrder.m_strExecDeptID = clsConverter.ToString(m_txtExecDept.Tag);		//ִ�п���
            objOrder.m_strExecDeptName = m_txtExecDept.Text;
            objOrder.m_intIsRich = (m_chkIsRich.Checked ? 1 : 0);
            objOrder.m_intISNEEDFEEL = (m_chkISNEEDFEEL.Checked ? 1 : 0);
            objOrder.m_dmlPrice = clsConverter.ToDecimal(m_txtPrice.Text);
            objOrder.m_dmlDosageRate = clsConverter.ToDecimal(m_txtPrice.Tag);
            objOrder.m_dmlPACKQTY_DEC = clsConverter.ToDecimal(m_txtPackage.Text);//��װ��   
            objOrder.m_intIPCHARGEFLG_INT = clsConverter.ToInt(m_txtPackage.Tag);//סԺ�շѵ�λ 0 ��������λ 1����С��λ
            //����
            objOrder.m_dmlDosage = clsConverter.ToDecimal(m_txtDosage.Text);
            objOrder.m_dmlUse = clsConverter.ToDecimal(m_txtUse.Text);
            objOrder.m_dmlGet = clsConverter.ToDecimal(m_txtGet.Text);

            //Ƶ��
            if (m_txtExecuteFreq.Text.Trim() != "" && m_txtExecuteFreq.Tag != null && m_txtExecuteFreq.Tag.ToString().Trim() != "")
            {
                objOrder.m_strExecFreqID = clsConverter.ToString(m_txtExecuteFreq.Tag);
                m_objTempFreq = GetFreqVoByFreqID(objOrder.m_strExecFreqID);
                objOrder.m_strExecFreqName = m_objTempFreq.m_strFreqName;
                objOrder.m_intFreqTime = m_objTempFreq.m_intTimes;
                objOrder.m_intFreqDays = m_objTempFreq.m_intDays;
            }
            else
            {
                objOrder.m_strExecFreqID = "";
                objOrder.m_strExecFreqName = "";
            }
            //�÷�
            if (m_txtDosageType.Text.Trim() != "" && m_txtDosageType.Tag != null && m_txtDosageType.Tag.ToString().Trim() != "")
            {
                objOrder.m_strDosetypeID = clsConverter.ToString(m_txtDosageType.Tag);
                clsBSEUsageType m_objUsage = GetUsageVoByUsageID(objOrder.m_strDosetypeID);
                objOrder.m_strDosetypeName = m_objUsage.m_strUsageName;
            }
            else
            {
                objOrder.m_strDosetypeID = "";
                objOrder.m_strDosetypeName = "";
            }
            //����
            objOrder.m_strEntrust = m_txtEntrust.Text.Trim();

            // ҩƷ��Դ: 0 ҩ��(ȫ�Ʒ�,��ҩ); 1 �����Ա�(ֻ�շ��÷�������Ŀ,����ҩ); 2 ���һ���(ȫ�Ʒѣ�����ҩ) 
            objOrder.RateType = clsConverter.ToInt(m_cboRateType.m_strGetID(m_cboRateType.SelectedIndex));
            objOrder.m_strDOCTORID_CHR = clsConverter.ToString(m_txtDoctorList.Tag);
            objOrder.m_strDOCTOR_VCHR = m_txtDoctorList.Text;

            //����ҽ��
            objOrder.m_strParentName = m_txtFatherOrder.Text;
            if (objOrder.m_strParentName.Trim() != "")
                objOrder.m_strParentID = clsConverter.ToString(m_txtFatherOrder.Tag);
            else
                objOrder.m_strParentID = "";

            objOrder.m_strOrderDicCateID = clsConverter.ToString(m_picInfo.Tag);
            clsT_aid_bih_ordercate_VO p_objItem;
            p_objItem = (clsT_aid_bih_ordercate_VO)((frmBIHOrderInput)this.ParentForm).m_htOrderCate[objOrder.m_strOrderDicCateID];
            if (p_objItem != null)
            {
                objOrder.m_strOrderDicCateName = p_objItem.m_strVIEWNAME_VCHR;
            }
            if (m_txtSample.Tag != null)
            {
                objOrder.m_strSAMPLEID_VCHR = (string)m_txtSample.Tag;
                objOrder.m_strSAMPLEName_VCHR = m_txtSample.Text;
            }
            if (m_txtCheck.Tag != null)
            {
                objOrder.m_strPARTID_VCHR = (string)m_txtCheck.Tag;
                objOrder.m_strPARTNAME_VCHR = m_txtCheck.Text;
            }
            // ��ӿ���������Ϣ
            objOrder.m_strCREATEAREA_ID = (string)((frmBIHOrderInput)this.ParentForm).m_txtCREATEAREA.Tag;
            objOrder.m_strCREATEAREA_Name = (string)((frmBIHOrderInput)this.ParentForm).m_txtCREATEAREA.Text;
            // ���������Σ���ʼ������ʱ��
            //���� 
            if (m_txtDays.Enabled == true && m_txtDays.Visible == true)
            {
                try
                {
                    objOrder.m_intOUTGETMEDDAYS_INT = int.Parse(m_txtDays.Text.ToString().Trim());
                }
                catch
                {
                    objOrder.m_intOUTGETMEDDAYS_INT = 0;
                }
            }
            else
            {
                objOrder.m_intOUTGETMEDDAYS_INT = 0;
            }
            //����
            if (objOrder.m_intExecuteType == 1)
            {
                try
                {
                    objOrder.m_intATTACHTIMES_INT = int.Parse(m_txtATTACHTIMES_INT.Text.ToString().Trim());
                }
                catch
                {
                    objOrder.m_intATTACHTIMES_INT = 0;
                }
            }
            else
            {
                objOrder.m_intATTACHTIMES_INT = 0;
            }
            //��ʼ������ʱ��

            try
            {
                objOrder.m_dtStartDate = DateTime.Parse(m_dtStartTime2.Text.ToString().Trim());
            }
            catch
            {
                objOrder.m_dtStartDate = DateTime.MinValue;
            }
            try
            {
                objOrder.m_dtFinishDate = DateTime.Parse(m_dtFinishTime2.Text.ToString().Trim());
            }
            catch
            {
                objOrder.m_dtFinishDate = DateTime.MinValue;
            }

            // �����ǰ¼����ֹͣʱ�䣬���õ�ǰ������Ϊֹͣ��
            if (objOrder.m_dtFinishDate != DateTime.MinValue)
            {
                objOrder.m_strStoperID = objOrder.m_strCreatorID;
                objOrder.m_strStoper = objOrder.m_strCreator;
                objOrder.m_dtStopdate = DateTime.Now;
            }
            else
            {
                objOrder.m_strStoperID = "";
                objOrder.m_strStoper = "";
                objOrder.m_dtStopdate = DateTime.MinValue;

            }
            //ҽ�����ڹ�����ID
            objOrder.m_strDOCTORGROUPID_CHR = (string)((frmBIHOrderInput)this.ParentForm).m_strDOCTORGROUPID_CHR;
            //��ҽ��ʱ�������ڲ���ID
            objOrder.m_strCURAREAID_CHR = objPatient.m_strAreaID;
            //��ҽ��ʱ�������ڲ���ID
            objOrder.m_strCURBEDID_CHR = objPatient.m_strBedID;
            //˵��
            objOrder.m_strREMARK_VCHR = m_txtREMARK_VCHR.Text.Trim();
            //�޸���ID
            objOrder.m_strChangedID_CHR = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpID;
            //�޸�������
            objOrder.m_strChangedName_CHR = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpName;
            objOrder.strShiying = this.cboShiying.Text.Substring(0, 1).ToString();

            objOrder.AntiUse = this.cboKJ.SelectedIndex;
            objOrder.AntiUse_YFLX = this.cboQK.SelectedIndex;
            //objOrder.CureDays = (this.txtCureDays.Text.Trim() == "" ? 0 : Convert.ToInt32(this.txtCureDays.Text.Trim()));
            if (this.cboKJ.Enabled && this.cboKJ.SelectedIndex == 0)
            {
                MessageBox.Show("����ҩ�����ѡ����;.", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cboKJ.Focus();
            }
            objOrder.IsProxyBoilMed = this.cboProxyBoil.SelectedIndex;
            objOrder.IsEmer = this.cboEmer.SelectedIndex;
            objOrder.IsOps = this.cboOps.SelectedIndex; 
        }

        private void m_txtGet_Leave(object sender, EventArgs e)
        {
            //�����ļ���
            m_txtDays_Leave(null, null);
        }

        private void m_dtFinishTime2_Leave(object sender, EventArgs e)
        {
            DateTime m_StopTime = DateTime.MinValue;
            DateTime m_StartTime = DateTime.MinValue;
            try
            {
                m_StartTime = Convert.ToDateTime(this.m_dtStartTime2.Text.ToString().Trim());

            }
            catch
            {
                m_StartTime = DateTime.MinValue;
            }
            try
            {
                m_StopTime = Convert.ToDateTime(m_dtFinishTime2.Text.ToString().Trim());

            }
            catch
            {
                m_StopTime = DateTime.MinValue;
            }
            if (m_StartTime > m_StopTime && m_StopTime != DateTime.MinValue)
            {

                MessageBox.Show("����ʱ�䲻�����ڿ�ʼʱ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                m_dtFinishTime2.Focus();
                return;
            }
            if (((frmBIHOrderInput)this.ParentForm).m_objCurrentOrder != null && m_StopTime < ((frmBIHOrderInput)this.ParentForm).m_objCurrentOrder.m_dtExecutedate && m_StopTime != DateTime.MinValue)
            {

                MessageBox.Show("����ʱ�䲻������ִ��ʱ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                m_dtFinishTime2.Focus();
                return;
            }
            setTheControlOrder("m_dtFinishTime2");
        }



        private void m_txtArea_Leave(object sender, EventArgs e)
        {
            ((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_txtArea.Focus();

        }

        private void m_cobOrderCate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                //m_txtOrderName.Focus();
                //m_txtRecipeNo.Focus();
                /*=================>*/
                setTheControlOrder(((System.Windows.Forms.ComboBox)sender).Name);
                /*<============================*/
            }
        }

        private void m_txtOrderCate_m_evtFindItem(object sender, string strFindCode, ListView lvwList)
        {
            if (m_cobOrderCate.Items.Count > 0)
            {
                //clsOrderCate
                for (int i = 0; i < m_cobOrderCate.Items.Count; i++)
                {

                    clsOrderCate p_objItem = (clsOrderCate)m_cobOrderCate.Items[i];
                    if (p_objItem != null && p_objItem.m_objOrderCate.m_strUSERCODE_VCHR.ToUpper().Contains(strFindCode.ToUpper().Trim()) || p_objItem.m_objOrderCate.m_strVIEWNAME_VCHR.ToUpper().Contains(strFindCode.ToUpper().Trim()))
                    {
                        ListViewItem lvi = lvwList.Items.Add(p_objItem.m_objOrderCate.m_strUSERCODE_VCHR);
                        lvi.SubItems.Add(p_objItem.m_objOrderCate.m_strVIEWNAME_VCHR);
                        lvi.Tag = p_objItem;
                    }
                }
            }
        }

        private void m_txtOrderCate_m_evtInitListView(ListView lvwList)
        {
            lvwList.Columns.Clear();
            lvwList.Width = 180;
            lvwList.Height = 260;
            ColumnHeader ch1 = lvwList.Columns.Add("USERCODE", 60, HorizontalAlignment.Left);
            ColumnHeader ch2 = lvwList.Columns.Add("Name", 100, HorizontalAlignment.Left);


        }

        private void m_txtOrderCate_m_evtSelectItem(object sender, ListViewItem lviSelected)
        {

            if (lviSelected != null)
            {
                m_txtOrderCate.Text = lviSelected.SubItems[1].Text;
                m_txtOrderCate.Tag = ((clsOrderCate)lviSelected.Tag).m_objOrderCate.m_strORDERCATEID_CHR;
                for (int i = 0; i < this.m_cobOrderCate.Items.Count; i++)
                {
                    if (((clsOrderCate)this.m_cobOrderCate.Items[i]).m_objOrderCate.m_strORDERCATEID_CHR == ((clsOrderCate)lviSelected.Tag).m_objOrderCate.m_strORDERCATEID_CHR)
                    {
                        this.m_cobOrderCate.SelectedIndex = i;
                        break;
                    }

                }
                //�Զ���ʾ����
                if (((clsOrderCate)lviSelected.Tag).m_objOrderCate.m_intAUTOSHOW_INT == 1)
                {
                    setTheControlOrder("m_txtRecipeNo");
                    m_txtOrderName.Text = "%";
                    SendKeys.Send("{Enter}");
                }
                else
                {
                    setTheControlOrder("m_txtRecipeNo");
                }
            }


        }

        private void m_txtOrderCate_DoubleClick(object sender, EventArgs e)
        {
            m_txtOrderCate.Text = "";
            SendKeys.Send("{Enter}");
        }


        internal void m_lblDosageType_Click(object sender, EventArgs e)
        {
            m_txtDosageType.Enabled = true;
            m_txtDosageType.Focus();
        }

        #region ��ע��Ϣ��ѯ
        private void m_lngREMARK_VCHR(string m_strFindCode)
        {
            DataTable objDT;
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            (new weCare.Proxy.ProxyIP()).Service.m_lngGetORDERDESCByCode(m_strFindCode, out  objDT);
            frmORDERDESC frm = new frmORDERDESC(objDT);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.m_txtREMARK_VCHR.Text = frm.getTheORDERDESC();
                this.m_txtREMARK_VCHR.SelectAll();
                SendKeys.Send("{Right}");
            }

        }
        #endregion
        private void m_txtREMARK_VCHR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && m_txtREMARK_VCHR.Text.Trim().StartsWith(@"/"))
            {
                string m_strFindCode = this.m_txtREMARK_VCHR.Text.TrimStart(@"/".ToCharArray());
                m_lngREMARK_VCHR(m_strFindCode);
                return;
            }
            else if (e.KeyCode == Keys.Enter && m_txtREMARK_VCHR.Text.Trim().StartsWith(@"\"))
            {
                int m_intStatue = 1;
                frmORDERDESC frm = new frmORDERDESC(m_intStatue, ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpID, ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpName);
                if (frm.ShowDialog() == DialogResult.OK)
                {

                }
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                setTheControlOrder(((TextBox)sender).Name);
            }
        }

        /// <summary>
        /// ������Ŀ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mnuCommonItems_Click(object sender, EventArgs e)
        {
            string strFindCode = m_txtOrderName.Text.Trim().TrimStart(@"/".ToCharArray());
            m_lngCommonItems(strFindCode);
        }
        #region ����������Ŀ��ѯ
        /// <summary>
        /// ������Ŀ
        /// </summary>
        /// <param name="strFindCode"></param>
        private void m_lngCommonItems(string strFindCode)
        {

            //clsBIHOrderService m_objService = clsGenerator.CreateObject(typeof(clsBIHOrderService)) as clsBIHOrderService;
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            //ҽ��¼����
            if (!((frmBIHOrderInput)this.ParentForm).m_OrderInputPreCheck())
            {
                return;
            }

            ArrayList m_arlItems = new ArrayList();

            // �õ����ƶ�Ӧ���շ��б�
            DataSet m_dsDicChargeSet = new DataSet();
            //��ѯ����
            int m_intClass = ((frmBIHOrderInput)this.ParentForm).seachClass.SelectedIndex;
            //ҽ������
            string m_strORDERCATEID_CHR = "";
            m_strORDERCATEID_CHR = (string)m_txtOrderCate.Tag;

            //����������Ŀ ���� ������Ŀ
            #region ����������Ŀ ���� ������Ŀ
            clsBIHOrderDic[] arrDic = null;
            long ret2 = 0;
            string strEmpid_chr = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpID;
            //������Ŀ
            m_strORDERCATEID_CHR = "";//���������͹���
            ret2 = (new weCare.Proxy.ProxyIP()).Service.m_lngGetCommonOrderDicChargeByCode(((frmBIHOrderInput)this.ParentForm).m_strMedDeptGross, strFindCode, strEmpid_chr, m_intClass, m_strORDERCATEID_CHR, ((frmBIHOrderInput)this.ParentForm).m_blLessMedControl, out arrDic, out m_dsDicChargeSet, ((frmBIHOrderInput)this.ParentForm).IsChildPrice);

            //��ʾ������Ŀ�б���
            if ((ret2 > 0) && (arrDic != null))
            {
                m_OrderDicListView(arrDic, m_dsDicChargeSet);
            }
            #endregion

        }
        /// <summary>
        /// ������Ŀ
        /// </summary>
        /// <param name="strFindCode"></param>
        private void m_lngGroupItems(string strFindCode)
        {

            //ҽ��¼����
            if (!((frmBIHOrderInput)this.ParentForm).m_OrderInputPreCheck())
            {
                return;
            }
            // ��ҽ������Ĺ�����Ϣ��һ��ҽ��VO�У�Ϊ��ҽ�����崫��ֵʹ��
            clsBIHOrder bihOrder;
            SetTheBaseOrder(out bihOrder);
            //��ѯ����
            int m_intClass = ((frmBIHOrderInput)this.ParentForm).seachClass.SelectedIndex;

            long ret2 = 0;
            //���� 
            //clsBIHOrderGroupService m_objService2 = new clsDcl_GetSvcObject().m_GetOrderGroupSvcObject();
            string strEmpid_chr = ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpID;

            clsBIHOrderGroup[] arrGroup;
            long ret1 = (new weCare.Proxy.ProxyIP()).Service.m_lngFindGroup(strFindCode, (m_objCurrentDoctor == null ? "" : m_objCurrentDoctor.m_strDoctorID), ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strInpatientAreaID, m_intClass, out arrGroup);
            if ((ret1 > 0) && (arrGroup != null))
            {
                com.digitalwave.iCare.BIHOrder.frmBIHOrderGroupInput frmGroup = new frmBIHOrderGroupInput(arrGroup, bihOrder, ((frmBIHOrderInput)this.ParentForm).m_htOrderCate, ((frmBIHOrderInput)this.ParentForm).m_dtvOrder, ((frmBIHOrderInput)this.ParentForm).m_objSpecateVo, strFindCode, m_intClass, 0, ((frmBIHOrderInput)this.ParentForm).m_blStopControl, ((frmBIHOrderInput)this.ParentForm).m_blDeableMedControl, this, ((frmBIHOrderInput)this.ParentForm).IsChildPrice);
                frmGroup.strPayType = ((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_objPatient.m_strPayTypeID;
                if (frmGroup.ShowDialog() == DialogResult.OK)
                {
                    if (frmGroup.m_arrGroupOrder.Count > 0)
                    {
                        Hashtable m_htTable = new Hashtable();
                        for (int i = 0; i < frmGroup.m_arrGroupOrder.Count; i++)
                        {
                            ////��������
                            clsBIHOrder order = (clsBIHOrder)frmGroup.m_arrGroupOrder[i];
                            /*<=========================*/
                            //ҽ����Դ
                            order.m_intSOURCETYPE_INT = ((frmBIHOrderInput)this.ParentForm).m_intSOURCETYPE_INT;
                            /*<=========================================*/
                            //SetTheOrderGetOneUseMoust(ref order);
                            if (!m_htTable.Contains(order.m_intRecipenNo.ToString()))
                            {
                                order.m_intIFPARENTID_INT = 1;
                                m_htTable.Add(order.m_intRecipenNo.ToString(), order);
                            }

                        }
                        //ͬ��ͬ��ҽ�����������
                        for (int i = 0; i < frmGroup.m_arrGroupOrder.Count; i++)
                        {
                            //��������
                            clsBIHOrder order = (clsBIHOrder)frmGroup.m_arrGroupOrder[i];

                            if (m_htTable.Contains(order.m_intRecipenNo.ToString()))
                            {
                                clsBIHOrder order2 = (clsBIHOrder)m_htTable[order.m_intRecipenNo.ToString()];
                                order.m_intExecuteType = order2.m_intExecuteType;
                                order.m_strDosetypeID = order2.m_strDosetypeID;
                                order.m_strDosetypeName = order2.m_strDosetypeName;
                                order.m_strExecFreqID = order2.m_strExecFreqID;
                                order.m_strExecFreqName = order2.m_strExecFreqName;
                                order.m_intOUTGETMEDDAYS_INT = order2.m_intOUTGETMEDDAYS_INT;
                                order.m_intATTACHTIMES_INT = order2.m_intATTACHTIMES_INT;

                            }
                            SetTheOrderGetOneUseMoust(ref order);

                        }
                        /*<============================================*/
                        //clsBIHOrderService m_objService = clsGenerator.CreateObject(typeof(clsBIHOrderService)) as clsBIHOrderService;
                        //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                        string[] strRecordIDArr = null;
                        try
                        {
                            (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewOrderByGroup(out strRecordIDArr, frmGroup.m_arrGroupOrder, ((frmBIHOrderInput)this.ParentForm).IsChildPrice);
                            ((frmBIHOrderInput)this.ParentForm).cmdRefurbish_Click(null, null);
                        }
                        catch (Exception objEx)
                        {
                            MessageBox.Show(objEx.Message, "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                return;

            }


        }
        /// <summary>
        /// �¼۲�ѯ
        /// </summary>
        /// <param name="strFindCode"></param>
        private void m_lngNewPriceItems(string strFindCode)
        {

            //ҽ��¼����
            if (!((frmBIHOrderInput)this.ParentForm).m_OrderInputPreCheck())
            {
                return;
            }
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            clsBIHOrderDic[] arrDic = null;
            long ret2 = 0;

            // �¼۱�
            frmItemStandardPrice price1 = new frmItemStandardPrice();
            if (price1.ShowDialog() == DialogResult.OK)
            {
                ret2 = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderDicByID(price1.m_strOrderDicId.ToString().Trim(), ((frmBIHOrderInput)this.ParentForm).m_strMedDeptGross, out arrDic);
                ListViewItem m_lviItem = new ListViewItem();
                m_lviItem.Tag = arrDic[0];
                clsCheckPart[] m_objPark = price1.m_checkPart;
                if (m_objPark != null && m_objPark.Length > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    for (int i = 0; i < m_objPark.Length; i++)
                    {
                        clsBIHOrderDic m_objOrderDic = (clsBIHOrderDic)(m_lviItem.Tag);
                        clsBIHOrder m_objOrder = m_objGetOrderByOrderDic(((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_objPatient, m_objOrderDic);
                        m_objOrder.m_strPARTID_VCHR = m_objPark[i].m_strPartID;
                        m_objOrder.m_strPARTNAME_VCHR = m_objPark[i].m_strPartName;
                        ((frmBIHOrderInput)this.ParentForm).m_objDomain.m_blnAddNew(m_objOrder);
                    }
                    this.Cursor = Cursors.Default;
                    ((frmBIHOrderInput)this.ParentForm).cmdRefurbish_Click(null, null);
                }
                else
                {
                    m_txtOrderName_m_evtSelectItem(null, m_lviItem);
                }
                return;
            }

            /*<======================================*/

        }
        /// <summary>
        /// һ����Ŀ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_lngNormalItems(string strFindCode)
        {

            //clsBIHOrderService m_objService = clsGenerator.CreateObject(typeof(clsBIHOrderService)) as clsBIHOrderService;
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            //ҽ��¼����
            if (!((frmBIHOrderInput)this.ParentForm).m_OrderInputPreCheck())
            {
                return;
            }
            ArrayList m_arlItems = new ArrayList();
            DataSet m_dsDicChargeSet = new DataSet();
            /*<==========================*/
            //��ѯ����
            int m_intClass = ((frmBIHOrderInput)this.ParentForm).seachClass.SelectedIndex;
            //ҽ������
            string m_strORDERCATEID_CHR = "";
            //m_strORDERCATEID_CHR = ((clsOrderCate)m_cobOrderCate.SelectedItem).m_objOrderCate.m_strORDERCATEID_CHR.ToString().Trim();
            m_strORDERCATEID_CHR = (string)m_txtOrderCate.Tag;
            clsBIHOrderDic[] arrDic = null;
            long ret2 = 0;
            ret2 = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderDicChargeByCode(strFindCode, m_intClass, m_strORDERCATEID_CHR, ((frmBIHOrderInput)this.ParentForm).m_blLessMedControl, ((frmBIHOrderInput)this.ParentForm).m_strMedDeptGross, out arrDic, out m_dsDicChargeSet, ((frmBIHOrderInput)this.ParentForm).IsChildPrice);
            //ret2 = m_objService.m_lngGetOrderDicChargeByCode(strFindCode, m_intClass, m_strORDERCATEID_CHR, ((frmBIHOrderInput)this.ParentForm).m_blLessMedControl, out arrDic, out m_dsDicChargeSet);
            //��ʾ������Ŀ�б���
            if ((ret2 > 0) && (arrDic != null))
            {
                m_OrderDicListView(arrDic, m_dsDicChargeSet);
            }


        }
        #endregion

        private void m_mnuGroupItems_Click(object sender, EventArgs e)
        {
            string strFindCode = m_txtOrderName.Text.Trim().TrimStart(@"\".ToCharArray());
            m_lngGroupItems(strFindCode); ;
        }

        /// <summary>
        /// һ����Ŀ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mnuNormalItems_Click(object sender, EventArgs e)
        {
            string strFindCode = m_txtOrderName.Text.Trim();
            m_lngNormalItems(strFindCode);


        }
        /// <summary>
        /// �¼���Ŀ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mnuNewPriceItems_Click(object sender, EventArgs e)
        {

            string strFindCode = m_txtOrderName.Text.Trim().TrimStart(@"?".ToCharArray());
            m_lngNewPriceItems(strFindCode);

        }

        private void m_mnuNormalRemark_Click(object sender, EventArgs e)
        {
            //m_txtREMARK_VCHR.Text=@"/"+m_txtREMARK_VCHR.Text.Trim().TrimStart(@"/".ToCharArray());
            //m_txtREMARK_VCHR.Focus();
            //SendKeys.Send("{Enter}");
            string m_strFindCode = this.m_txtREMARK_VCHR.Text.TrimStart(@"/".ToCharArray());
            m_lngREMARK_VCHR(m_strFindCode);
        }

        private void m_mnuNormalAdd_Click(object sender, EventArgs e)
        {
            //string m_strOld = m_txtREMARK_VCHR.Text;
            //m_txtREMARK_VCHR.Text=@"\";
            //m_txtREMARK_VCHR.Focus();
            //SendKeys.Send("{Enter}");
            //m_txtREMARK_VCHR.Text = m_strOld;
            int m_intStatue = 1;
            frmORDERDESC frm = new frmORDERDESC(m_intStatue, ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpID, ((frmBIHOrderInput)this.ParentForm).LoginInfo.m_strEmpName);
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void m_txtCheck_DoubleClick(object sender, EventArgs e)
        {
            m_txtCheck.Text = "";
            SendKeys.Send("{Enter}");
        }

        private void m_txtSample_DoubleClick(object sender, EventArgs e)
        {
            m_txtSample.Text = "";
            SendKeys.Send("{Enter}");
        }



        private void m_txtDosageType_DoubleClick(object sender, EventArgs e)
        {

            m_txtDosageType.Text = "";
            SendKeys.Send("{Enter}");

        }

        private void m_txtExecuteFreq_DoubleClick(object sender, EventArgs e)
        {

            m_txtExecuteFreq.Text = "";
            SendKeys.Send("{Enter}");
        }


        private void m_lblExecuteFreq_DoubleClick(object sender, EventArgs e)
        {
            m_txtExecuteFreq.Enabled = true;
            m_txtExecuteFreq.ReadOnly = false;
            m_txtExecuteFreq.BackColor = Color.White;
            m_hideExecuteFreq.Visible = false;
        }

        private void m_lblDosageType_DoubleClick(object sender, EventArgs e)
        {
            m_txtDosageType.Enabled = true;
            m_txtDosageType.ReadOnly = false;
            m_txtDosageType.BackColor = Color.White;
            m_hideDosageType.Visible = false;
        }

        private void m_lblDosage_DoubleClick(object sender, EventArgs e)
        {
            m_txtDosage.Enabled = true;
            m_txtDosage.BackColor = Color.White;
            m_hideDosage.Visible = false;
            m_hideDosageUnit.Visible = false;
        }

        /// <summary>
        /// ����Ƥ��(�÷��Ƿ���Ҫ��Ƥ��)
        /// </summary>
        /// <param name="objOrder"></param>
        internal void SetTheOrderNeelFeel(ref clsBIHOrder objOrder)
        {
            // if (objOrder.m_intISNEEDFEEL == 1)
            // {
            if ((GetUsageVoByUsageID(objOrder.m_strDosetypeID)).m_intTEST_INT == 1)
            {
                objOrder.m_intISNEEDFEEL = 1;
            }
            else
            {
                objOrder.m_intISNEEDFEEL = 0;
            }
            // }
        }

        /// <summary>
        ///  //����ҽ���������ֶ� ��������Ƥ�ԣ������޸ı�־
        /// </summary>
        /// <param name="objOrder"></param>
        internal void SetTheOrderSpecial(ref clsBIHOrder objOrder)
        {
            //��������
            SetTheOrderGetMoust(ref objOrder);
            //����Ƥ��
            SetTheOrderNeelFeel(ref objOrder);
            //�����޸ı�־
            SetTheOrderChargeTag(ref objOrder);
        }

        internal void SetTheOrderChargeTag(ref clsBIHOrder objOrder)
        {
            //�������������ֱ�����ɵ�ҽ��������ֹ����
            if (objOrder.m_intCHARGE_INT == -1)
            {
                objOrder.m_intCHARGE_INT = 0;
                return;
            }
            /*<=========================*/
            objOrder.m_intCHARGE_INT = 0;
            clsT_aid_bih_ordercate_VO p_objItem;
            p_objItem = (clsT_aid_bih_ordercate_VO)((frmBIHOrderInput)this.ParentForm).m_htOrderCate[objOrder.m_strOrderDicCateID];
            if (p_objItem != null && p_objItem.m_intExecuFrenquenceType == 2 && m_hideExecuteFreq.Visible == false)
            {
                if (this.m_txtExecuteFreq.Enabled == true && this.m_txtExecuteFreq.ReadOnly == false)
                {
                    // �޸ı�־(0-��ͨ״̬,1-Ƶ���޸�)
                    objOrder.m_intCHARGE_INT = 1;
                }
            }
            else if (objOrder.m_intExecuteType != 1 && m_txtExecuteFreq.Enabled == true && m_txtExecuteFreq.ReadOnly == false)
            {
                // �޸ı�־(0-��ͨ״̬,1-Ƶ���޸�)
                objOrder.m_intCHARGE_INT = 1;
            }
        }

        private void m_txtExecuteFreq_Leave(object sender, EventArgs e)
        {
            //������Ƶ�ʽ��洦��
            if (m_txtExecuteFreq.Tag != null)
            {
                SetTheContinueFreqView(m_txtExecuteFreq.Tag.ToString());

            }
        }

        /// <summary>
        ///  //������Ƶ�ʽ��洦��
        /// </summary>
        /// <param name="m_strFREQID_CHR">Ƶ��ID</param>
        public void SetTheContinueFreqView(string m_strFREQID_CHR)
        {
            if (((frmBIHOrderInput)this.ParentForm).m_objSpecateVo.m_strCONFREQID_CHR.Trim().Equals(m_strFREQID_CHR))
            {
                m_txtDosageControl(false);
                m_txtDosageTypeControl(false);
                //д������������
                //m_txtDosage.Text = "1";
                //m_txtDosage.Enabled = false;
                //m_txtGet.Text = "1";
                //m_txtGet2.Text = "";
                //m_txtGet.Enabled = false;
                //m_txtUse.Text = "1";
            }


        }

        private void m_txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_txtArea.Focus();
            }
        }

        private void m_txtBedNo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                ((frmBIHOrderInput)this.ParentForm).m_ctlPatient.m_txtArea.Focus();
            }
        }

        private void m_txtDosage_Leave(object sender, EventArgs e)
        {

            #region ��������
            double dbl1 = -1;
            if (m_txtDosage.Text.Trim() != "")
            {
                try
                {
                    dbl1 = double.Parse(m_txtDosage.Text.Trim());
                }
                catch { }
            }
            string strFreqID = clsConverter.ToString(m_txtExecuteFreq.Tag);
            if (m_objTempFreq == null)
            {
                m_objTempFreq = GetFreqVoByFreqID(strFreqID.Trim());
            }
            else if (m_objTempFreq.m_strFreqID.Trim() != strFreqID.Trim())
            {
                m_objTempFreq = GetFreqVoByFreqID(strFreqID.Trim());
            }

            #endregion
            //������ҽ����ҽ��������ǿ��Ϊ��λ������Ƶ�ʴ���Ϊ1
            if (dbl1 != 1 && m_objTempFreq != null && m_objTempFreq.m_strFreqID.Trim().Equals(((frmBIHOrderInput)this.ParentForm).m_objSpecateVo.m_strCONFREQID_CHR.Trim()))
            {
                MessageBox.Show("������ҽ������ֻ����1", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_txtDosage.Text = "1";
                m_txtDosage.Focus();
                m_txtDosage.SelectAll();
                return;
            }
        }

        private void cboShiying_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setTheControlOrder(((com.digitalwave.controls.ctlQComboBox)sender).Name);
            }
        }

        private void cboKJ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setTheControlOrder("cboKJ");
            }
        }

        private void cboQK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setTheControlOrder("cboQK");
            }
        }

        private void txtCureDays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setTheControlOrder("txtCureDays");
            }
        }

        private void cboProxyBoil_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setTheControlOrder("cboProxyBoil");
            }
        }

        private void cboEmer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setTheControlOrder("cboEmer");
            }
        }

        private void cboOps_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setTheControlOrder("cboOps");
            }
        }

    }
}

