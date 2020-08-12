using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 药品基本信息列表 Create by Sam 2004-5-24
    /// </summary>
    public class frmMedicine : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        private System.Windows.Forms.ToolBar m_tb;
        private System.Windows.Forms.ToolBarButton Add;
        private System.Windows.Forms.ToolBarButton Del;
        private System.Windows.Forms.ToolBarButton Esc;
        private System.Windows.Forms.ToolBarButton Find;
        private System.Windows.Forms.ToolBarButton reNew;
        private System.Windows.Forms.ToolBarButton Re;
        private System.Windows.Forms.ContextMenu m_Menu;
        private System.Windows.Forms.MenuItem mu_Unit;
        private System.Windows.Forms.MenuItem mu_MedType;
        private System.Windows.Forms.MenuItem mu_PrepType;
        private System.Windows.Forms.MenuItem mu_MedAndUnit;
        private bool IsReturn = false;
        private string ReturnID = null;
        private string ReturnName = null;
        private System.Windows.Forms.MenuItem mu_MedPrice;
        private System.Windows.Forms.MenuItem mu_MedAndSto;
        private System.Windows.Forms.MenuItem mu_MedLimit;
        internal com.digitalwave.controls.datagrid.ctlDataGrid m_dgList;
        internal System.Windows.Forms.GroupBox gbFind;
        private PinkieControls.ButtonXP btnClose;
        private PinkieControls.ButtonXP m_btnFind;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        internal PinkieControls.ButtonXP btnNewAdd;
        private PinkieControls.ButtonXP btnDele;
        private PinkieControls.ButtonXP buttonXP4;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.TextBox m_txtNo;
        private System.Windows.Forms.Label m_lbNo;
        internal System.Windows.Forms.TextBox m_txtName;
        private System.Windows.Forms.Label m_lbName;
        internal System.Windows.Forms.TextBox m_txtWB;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox m_txtPY;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox m_txtEnName;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox m_txtSpec;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.ComboBox m_cboPreType;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.TextBox m_txtDosage;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.ComboBox m_CobDosageUnit;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.TextBox m_txtTRADEPRICE;
        private System.Windows.Forms.Label label14;
        internal System.Windows.Forms.TextBox m_txtUNITPRICE;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.ComboBox m_CobUnit;
        internal System.Windows.Forms.ComboBox m_CobIpUnit;
        private System.Windows.Forms.Label label16;
        internal System.Windows.Forms.TextBox m_txtPackQty;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        internal System.Windows.Forms.TextBox mixuser;
        private System.Windows.Forms.Label label19;
        internal System.Windows.Forms.TextBox maxuser;
        private System.Windows.Forms.Label label20;
        internal System.Windows.Forms.TextBox m_txtInsuranceID;
        internal System.Windows.Forms.ComboBox m_cmbIsInDocAdv;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        internal PinkieControls.ButtonXP btnChang;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.CheckBox m_chkIsAnaesthesia;
        internal System.Windows.Forms.CheckBox m_chkIsCostly;
        internal System.Windows.Forms.CheckBox m_chkIsChlorpromazine;
        internal System.Windows.Forms.CheckBox m_chkIsSelfPay;
        internal System.Windows.Forms.CheckBox m_chkIsSelf;
        internal System.Windows.Forms.ComboBox cboOPCHARGEFLG;
        internal System.Windows.Forms.ComboBox cboIPCHARGEFLG;
        internal System.Windows.Forms.CheckBox isSTANDARD;
        private System.Windows.Forms.Label label26;
        internal System.Windows.Forms.TextBox txt_NMLDOSAGE;
        internal System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label30;
        internal System.Windows.Forms.TextBox m_txtChild;
        private System.Windows.Forms.Label label32;
        internal System.Windows.Forms.TextBox m_txtAdul;
        private System.Windows.Forms.Label label33;
        internal System.Windows.Forms.TextBox ctlvendor;
        internal com.digitalwave.controls.ctlTextBoxFind m_txtUse;
        internal com.digitalwave.controls.ctlTextBoxFind m_txtITEMOPCALCTYPE;
        internal com.digitalwave.controls.ctlTextBoxFind m_txtITEMOPINVTYPE;
        internal com.digitalwave.controls.ctlTextBoxFind m_txtITEMIPCALCTYPE;
        internal com.digitalwave.controls.ctlTextBoxFind m_txtITEMIPINVTYPE;
        private PinkieControls.ButtonXP buttonXP1;
        internal System.Windows.Forms.Label LableMed;
        private System.Windows.Forms.Label label34;
        internal System.Windows.Forms.CheckBox checkBox1;
        private PinkieControls.ButtonXP buttonXP2;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        internal System.Windows.Forms.ComboBox m_CboSelect;
        internal System.Windows.Forms.ComboBox ComType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        internal com.digitalwave.iCare.gui.HIS.exComboBox ctlCARETYPE;
        internal System.Windows.Forms.TextBox txtMEDNORMALNAME;
        internal Label label2;
        internal PinkieControls.ButtonXP btnDele1;
        internal com.digitalwave.controls.ctlTextBoxFind ctlTextBoxFind1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label31;
        internal System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Panel panel3;
        private PinkieControls.ButtonXP buttonXP3;
        private System.Windows.Forms.Label label37;
        internal com.digitalwave.controls.ctlTextBoxFind m_txtPharMatype;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label38;
        internal com.digitalwave.iCare.gui.HIS.exComboBox cobSelectType;
        private System.Windows.Forms.Label label39;
        internal com.digitalwave.iCare.gui.HIS.exComboBox m_cboMedType;
        private PinkieControls.ButtonXP buttonXP5;
        internal System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        internal exComboBox exComboBox1;
        private Label label40;
        internal CheckBox checkBox4;
        internal CheckBox checkBox3;
        private Label label42;
        internal exComboBox m_cobCat;
        private PinkieControls.ButtonXP buttonXP7;
        internal System.Windows.Forms.TextBox textBoxTypedNumeric1;
        private Label label43;
        internal CheckBox isStop;
        internal com.digitalwave.controls.ctlTextBoxFind textboxFreq;
        internal CheckBox checkBox5;
        internal exComboBox exComboBox2;
        private Label label41;
        internal exComboBox m_cboPUTMEDTYPE;
        private Label label45;
        private Label label44;
        internal exComboBox m_cboCATE1;
        internal ComboBox m_cboFindContent;
        internal Label m_txtOPChargeCode;
        private Label label46;
        private Label label47;
        internal System.Windows.Forms.TextBox m_txtIpUnitPrice;
        private PinkieControls.ButtonXP m_btnSetStandardYear;
        private IContainer components;

        /// <summary>
        /// 中标年份
        /// </summary>
        int m_intYear = 0;
        private ContextMenuStrip m_cmsCopy;
        private ToolStripMenuItem 复制新增ToolStripMenuItem;

        /// <summary>
        /// 初始化和返回的年份
        /// </summary>
        internal string m_strStandarddateReturn = string.Empty;
        internal RadioButton m_rbxH;
        internal RadioButton m_rbxT;
        internal RadioButton m_rbxF;
        /// <summary>
        /// 字典类型，默认为0，表示药库的药品，为1时表示物资仓库的物资
        /// </summary>
        internal string m_strType = "0";
        internal System.Windows.Forms.TextBox m_txtRequestPackQty;
        private Label label50;
        internal ComboBox m_CobRequestUnit;
        private Label label49;
        internal System.Windows.Forms.TextBox m_txtExpenseLimit;
        private Label label48;
        internal System.Windows.Forms.TextBox m_txtDiffPrice;
        private Label label51;
        private Label label52;
        public ComboBox cboMedBagUnit;
        public ComboBox cboHighRisk;
        private Label label53;
        internal CheckBox chkIsProduceDrugs;
        private Label label55;
        private Label label54;
        internal TextBox txtTransNo;
        internal TextBox txtVarietyCode;
        /// <summary>
        /// 收费项目比例
        /// </summary>
        internal DataTable m_dtbChargeItem = null;
        public frmMedicine()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

        /// <summary>
        /// 清理所有正在使用的资源。
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

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMedicine));
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo1 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo2 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo3 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo4 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo5 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo6 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo7 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo8 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo9 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo10 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo11 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo12 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo13 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo14 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo15 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo16 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo17 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo18 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo19 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo20 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo21 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo22 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo23 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo24 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo25 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo26 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo27 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo28 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo29 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo30 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo31 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo32 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo33 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo34 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo35 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo36 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo37 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo38 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo39 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo40 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo41 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo42 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo43 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo44 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo45 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo46 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo47 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo48 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo49 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo50 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo51 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo52 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo53 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo54 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo55 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            this.m_Menu = new System.Windows.Forms.ContextMenu();
            this.mu_Unit = new System.Windows.Forms.MenuItem();
            this.mu_MedType = new System.Windows.Forms.MenuItem();
            this.mu_PrepType = new System.Windows.Forms.MenuItem();
            this.mu_MedAndSto = new System.Windows.Forms.MenuItem();
            this.mu_MedLimit = new System.Windows.Forms.MenuItem();
            this.mu_MedAndUnit = new System.Windows.Forms.MenuItem();
            this.mu_MedPrice = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIsProduceDrugs = new System.Windows.Forms.CheckBox();
            this.m_rbxH = new System.Windows.Forms.RadioButton();
            this.m_rbxT = new System.Windows.Forms.RadioButton();
            this.m_rbxF = new System.Windows.Forms.RadioButton();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.m_chkIsChlorpromazine = new System.Windows.Forms.CheckBox();
            this.isStop = new System.Windows.Forms.CheckBox();
            this.m_chkIsCostly = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.isSTANDARD = new System.Windows.Forms.CheckBox();
            this.m_chkIsSelfPay = new System.Windows.Forms.CheckBox();
            this.m_chkIsSelf = new System.Windows.Forms.CheckBox();
            this.m_chkIsAnaesthesia = new System.Windows.Forms.CheckBox();
            this.m_btnSetStandardYear = new PinkieControls.ButtonXP();
            this.buttonXP7 = new PinkieControls.ButtonXP();
            this.panel4 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonXP5 = new PinkieControls.ButtonXP();
            this.buttonXP3 = new PinkieControls.ButtonXP();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.buttonXP2 = new PinkieControls.ButtonXP();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtTransNo = new System.Windows.Forms.TextBox();
            this.txtVarietyCode = new System.Windows.Forms.TextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.cboHighRisk = new System.Windows.Forms.ComboBox();
            this.label53 = new System.Windows.Forms.Label();
            this.cboMedBagUnit = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.m_txtDiffPrice = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.m_txtExpenseLimit = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.m_txtRequestPackQty = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.m_CobRequestUnit = new System.Windows.Forms.ComboBox();
            this.label49 = new System.Windows.Forms.Label();
            this.m_txtIpUnitPrice = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.txtMEDNORMALNAME = new System.Windows.Forms.TextBox();
            this.m_txtPY = new System.Windows.Forms.TextBox();
            this.m_txtOPChargeCode = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.m_cboCATE1 = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.m_cboPUTMEDTYPE = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.exComboBox2 = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.textboxFreq = new com.digitalwave.controls.ctlTextBoxFind();
            this.textBoxTypedNumeric1 = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.m_cobCat = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.exComboBox1 = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label40 = new System.Windows.Forms.Label();
            this.m_cboMedType = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.m_txtPharMatype = new com.digitalwave.controls.ctlTextBoxFind();
            this.label37 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ctlTextBoxFind1 = new com.digitalwave.controls.ctlTextBoxFind();
            this.btnDele1 = new PinkieControls.ButtonXP();
            this.label2 = new System.Windows.Forms.Label();
            this.ctlCARETYPE = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.LableMed = new System.Windows.Forms.Label();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.m_txtITEMIPINVTYPE = new com.digitalwave.controls.ctlTextBoxFind();
            this.m_txtITEMIPCALCTYPE = new com.digitalwave.controls.ctlTextBoxFind();
            this.m_txtITEMOPINVTYPE = new com.digitalwave.controls.ctlTextBoxFind();
            this.m_txtITEMOPCALCTYPE = new com.digitalwave.controls.ctlTextBoxFind();
            this.m_txtUse = new com.digitalwave.controls.ctlTextBoxFind();
            this.m_txtNo = new System.Windows.Forms.TextBox();
            this.m_txtChild = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.m_txtAdul = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txt_NMLDOSAGE = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cboIPCHARGEFLG = new System.Windows.Forms.ComboBox();
            this.cboOPCHARGEFLG = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.m_cmbIsInDocAdv = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.m_txtInsuranceID = new System.Windows.Forms.TextBox();
            this.maxuser = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.mixuser = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.m_txtPackQty = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.m_CobIpUnit = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.m_CobUnit = new System.Windows.Forms.ComboBox();
            this.m_txtUNITPRICE = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.m_txtTRADEPRICE = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.m_CobDosageUnit = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.m_txtDosage = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_cboPreType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.m_txtSpec = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.m_txtEnName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_txtWB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_txtName = new System.Windows.Forms.TextBox();
            this.m_lbName = new System.Windows.Forms.Label();
            this.m_lbNo = new System.Windows.Forms.Label();
            this.ctlvendor = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cobSelectType = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_cboFindContent = new System.Windows.Forms.ComboBox();
            this.m_CboSelect = new System.Windows.Forms.ComboBox();
            this.m_btnFind = new PinkieControls.ButtonXP();
            this.btnClose = new PinkieControls.ButtonXP();
            this.label1 = new System.Windows.Forms.Label();
            this.ComType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDele = new PinkieControls.ButtonXP();
            this.m_dgList = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.m_cmsCopy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制新增ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbFind = new System.Windows.Forms.GroupBox();
            this.btnChang = new PinkieControls.ButtonXP();
            this.buttonXP4 = new PinkieControls.ButtonXP();
            this.btnNewAdd = new PinkieControls.ButtonXP();
            this.m_tb = new System.Windows.Forms.ToolBar();
            this.Add = new System.Windows.Forms.ToolBarButton();
            this.Del = new System.Windows.Forms.ToolBarButton();
            this.Find = new System.Windows.Forms.ToolBarButton();
            this.reNew = new System.Windows.Forms.ToolBarButton();
            this.Re = new System.Windows.Forms.ToolBarButton();
            this.Esc = new System.Windows.Forms.ToolBarButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgList)).BeginInit();
            this.m_cmsCopy.SuspendLayout();
            this.gbFind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_Menu
            // 
            this.m_Menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mu_Unit,
            this.mu_MedType,
            this.mu_PrepType,
            this.mu_MedAndSto,
            this.mu_MedLimit});
            // 
            // mu_Unit
            // 
            this.mu_Unit.Index = 0;
            this.mu_Unit.Text = "单位维护";
            // 
            // mu_MedType
            // 
            this.mu_MedType.Index = 1;
            this.mu_MedType.Text = "药品类型";
            // 
            // mu_PrepType
            // 
            this.mu_PrepType.Index = 2;
            this.mu_PrepType.Text = "剂型维护";
            // 
            // mu_MedAndSto
            // 
            this.mu_MedAndSto.Index = 3;
            this.mu_MedAndSto.Text = "仓库药品维护";
            this.mu_MedAndSto.Click += new System.EventHandler(this.mu_MedAndSto_Click);
            // 
            // mu_MedLimit
            // 
            this.mu_MedLimit.Index = 4;
            this.mu_MedLimit.Text = "药库限额管理";
            this.mu_MedLimit.Click += new System.EventHandler(this.mu_MedLimit_Click);
            // 
            // mu_MedAndUnit
            // 
            this.mu_MedAndUnit.Index = -1;
            this.mu_MedAndUnit.Text = "";
            // 
            // mu_MedPrice
            // 
            this.mu_MedPrice.Index = -1;
            this.mu_MedPrice.Text = "";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.m_btnSetStandardYear);
            this.panel1.Controls.Add(this.buttonXP7);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.buttonXP5);
            this.panel1.Controls.Add(this.buttonXP3);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.buttonXP2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.m_dgList);
            this.panel1.Controls.Add(this.gbFind);
            this.panel1.Location = new System.Drawing.Point(0, -8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1016, 571);
            this.panel1.TabIndex = 40;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkIsProduceDrugs);
            this.groupBox1.Controls.Add(this.m_rbxH);
            this.groupBox1.Controls.Add(this.m_rbxT);
            this.groupBox1.Controls.Add(this.m_rbxF);
            this.groupBox1.Controls.Add(this.checkBox5);
            this.groupBox1.Controls.Add(this.m_chkIsChlorpromazine);
            this.groupBox1.Controls.Add(this.isStop);
            this.groupBox1.Controls.Add(this.m_chkIsCostly);
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.isSTANDARD);
            this.groupBox1.Controls.Add(this.m_chkIsSelfPay);
            this.groupBox1.Controls.Add(this.m_chkIsSelf);
            this.groupBox1.Controls.Add(this.m_chkIsAnaesthesia);
            this.groupBox1.Location = new System.Drawing.Point(696, 225);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 167);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkIsProduceDrugs
            // 
            this.chkIsProduceDrugs.ForeColor = System.Drawing.Color.Red;
            this.chkIsProduceDrugs.Location = new System.Drawing.Point(15, 128);
            this.chkIsProduceDrugs.Name = "chkIsProduceDrugs";
            this.chkIsProduceDrugs.Size = new System.Drawing.Size(82, 24);
            this.chkIsProduceDrugs.TabIndex = 13;
            this.chkIsProduceDrugs.Text = "易制毒";
            // 
            // m_rbxH
            // 
            this.m_rbxH.AutoSize = true;
            this.m_rbxH.Location = new System.Drawing.Point(16, 70);
            this.m_rbxH.Name = "m_rbxH";
            this.m_rbxH.Size = new System.Drawing.Size(81, 18);
            this.m_rbxH.TabIndex = 12;
            this.m_rbxH.Text = "合资药品";
            this.m_rbxH.UseVisualStyleBackColor = true;
            // 
            // m_rbxT
            // 
            this.m_rbxT.AutoSize = true;
            this.m_rbxT.Location = new System.Drawing.Point(16, 43);
            this.m_rbxT.Name = "m_rbxT";
            this.m_rbxT.Size = new System.Drawing.Size(81, 18);
            this.m_rbxT.TabIndex = 12;
            this.m_rbxT.Text = "进口药品";
            this.m_rbxT.UseVisualStyleBackColor = true;
            // 
            // m_rbxF
            // 
            this.m_rbxF.AutoSize = true;
            this.m_rbxF.Checked = true;
            this.m_rbxF.Location = new System.Drawing.Point(16, 16);
            this.m_rbxF.Name = "m_rbxF";
            this.m_rbxF.Size = new System.Drawing.Size(81, 18);
            this.m_rbxF.TabIndex = 12;
            this.m_rbxF.TabStop = true;
            this.m_rbxF.Text = "国产药品";
            this.m_rbxF.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.Location = new System.Drawing.Point(216, 106);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(82, 24);
            this.checkBox5.TabIndex = 11;
            this.checkBox5.Text = "科室自备";
            this.checkBox5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.checkBox5_KeyDown);
            // 
            // m_chkIsChlorpromazine
            // 
            this.m_chkIsChlorpromazine.Location = new System.Drawing.Point(116, 34);
            this.m_chkIsChlorpromazine.Name = "m_chkIsChlorpromazine";
            this.m_chkIsChlorpromazine.Size = new System.Drawing.Size(82, 24);
            this.m_chkIsChlorpromazine.TabIndex = 2;
            this.m_chkIsChlorpromazine.Text = "精神一类";
            this.m_chkIsChlorpromazine.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            this.m_chkIsChlorpromazine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_chkIsChlorpromazine_KeyDown);
            // 
            // isStop
            // 
            this.isStop.Location = new System.Drawing.Point(15, 99);
            this.isStop.Name = "isStop";
            this.isStop.Size = new System.Drawing.Size(82, 24);
            this.isStop.TabIndex = 10;
            this.isStop.Text = "是否停用";
            this.isStop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.isStop_KeyDown);
            // 
            // m_chkIsCostly
            // 
            this.m_chkIsCostly.Location = new System.Drawing.Point(116, 58);
            this.m_chkIsCostly.Name = "m_chkIsCostly";
            this.m_chkIsCostly.Size = new System.Drawing.Size(82, 24);
            this.m_chkIsCostly.TabIndex = 4;
            this.m_chkIsCostly.Text = "贵重药品";
            this.m_chkIsCostly.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_chkIsCostly_KeyDown);
            // 
            // checkBox4
            // 
            this.checkBox4.Location = new System.Drawing.Point(216, 10);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(82, 24);
            this.checkBox4.TabIndex = 1;
            this.checkBox4.Text = "毒性药品";
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            this.checkBox4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.checkBox4_KeyDown);
            // 
            // checkBox3
            // 
            this.checkBox3.Location = new System.Drawing.Point(216, 34);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(82, 24);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "精神二类";
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            this.checkBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.checkBox3_KeyDown);
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(116, 106);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 24);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "需要皮试";
            this.checkBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.checkBox1_KeyDown);
            // 
            // isSTANDARD
            // 
            this.isSTANDARD.Location = new System.Drawing.Point(216, 82);
            this.isSTANDARD.Name = "isSTANDARD";
            this.isSTANDARD.Size = new System.Drawing.Size(82, 24);
            this.isSTANDARD.TabIndex = 8;
            this.isSTANDARD.Text = "中标药品";
            // 
            // m_chkIsSelfPay
            // 
            this.m_chkIsSelfPay.Location = new System.Drawing.Point(116, 82);
            this.m_chkIsSelfPay.Name = "m_chkIsSelfPay";
            this.m_chkIsSelfPay.Size = new System.Drawing.Size(82, 24);
            this.m_chkIsSelfPay.TabIndex = 7;
            this.m_chkIsSelfPay.Text = "自费药品";
            this.m_chkIsSelfPay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_chkIsSelfPay_KeyDown);
            // 
            // m_chkIsSelf
            // 
            this.m_chkIsSelf.Location = new System.Drawing.Point(216, 58);
            this.m_chkIsSelf.Name = "m_chkIsSelf";
            this.m_chkIsSelf.Size = new System.Drawing.Size(82, 24);
            this.m_chkIsSelf.TabIndex = 5;
            this.m_chkIsSelf.Text = "院内制剂";
            this.m_chkIsSelf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_chkIsSelf_KeyDown);
            // 
            // m_chkIsAnaesthesia
            // 
            this.m_chkIsAnaesthesia.Location = new System.Drawing.Point(116, 10);
            this.m_chkIsAnaesthesia.Name = "m_chkIsAnaesthesia";
            this.m_chkIsAnaesthesia.Size = new System.Drawing.Size(82, 24);
            this.m_chkIsAnaesthesia.TabIndex = 0;
            this.m_chkIsAnaesthesia.Text = "麻醉药品";
            this.m_chkIsAnaesthesia.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            this.m_chkIsAnaesthesia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_chkIsAnaesthesia_KeyDown);
            // 
            // m_btnSetStandardYear
            // 
            this.m_btnSetStandardYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnSetStandardYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_btnSetStandardYear.DefaultScheme = true;
            this.m_btnSetStandardYear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnSetStandardYear.Hint = "";
            this.m_btnSetStandardYear.Location = new System.Drawing.Point(901, 451);
            this.m_btnSetStandardYear.Name = "m_btnSetStandardYear";
            this.m_btnSetStandardYear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnSetStandardYear.Size = new System.Drawing.Size(64, 35);
            this.m_btnSetStandardYear.TabIndex = 330;
            this.m_btnSetStandardYear.Text = "中标年份";
            this.m_btnSetStandardYear.Click += new System.EventHandler(this.m_btnSetStandardYear_Click);
            // 
            // buttonXP7
            // 
            this.buttonXP7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXP7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.buttonXP7.DefaultScheme = true;
            this.buttonXP7.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP7.Hint = "";
            this.buttonXP7.Location = new System.Drawing.Point(971, 450);
            this.buttonXP7.Name = "buttonXP7";
            this.buttonXP7.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP7.Size = new System.Drawing.Size(38, 35);
            this.buttonXP7.TabIndex = 105;
            this.buttonXP7.Text = "导出";
            this.buttonXP7.Click += new System.EventHandler(this.buttonXP7_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.listView1);
            this.panel4.Location = new System.Drawing.Point(392, 125);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(296, 136);
            this.panel4.TabIndex = 329;
            this.panel4.Visible = false;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(296, 136);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "仓库名称";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "库存量";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "单位";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "仓库类别";
            this.columnHeader4.Width = 80;
            // 
            // buttonXP5
            // 
            this.buttonXP5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXP5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.buttonXP5.DefaultScheme = true;
            this.buttonXP5.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP5.Hint = "";
            this.buttonXP5.Location = new System.Drawing.Point(855, 451);
            this.buttonXP5.Name = "buttonXP5";
            this.buttonXP5.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP5.Size = new System.Drawing.Size(40, 35);
            this.buttonXP5.TabIndex = 328;
            this.buttonXP5.Text = "库存";
            this.buttonXP5.Click += new System.EventHandler(this.buttonXP5_Click);
            // 
            // buttonXP3
            // 
            this.buttonXP3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXP3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.buttonXP3.DefaultScheme = true;
            this.buttonXP3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP3.Hint = "";
            this.buttonXP3.Location = new System.Drawing.Point(695, 451);
            this.buttonXP3.Name = "buttonXP3";
            this.buttonXP3.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP3.Size = new System.Drawing.Size(88, 35);
            this.buttonXP3.TabIndex = 327;
            this.buttonXP3.Text = "历史价格(&D)";
            this.buttonXP3.Click += new System.EventHandler(this.buttonXP3_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.PapayaWhip;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.checkBox2);
            this.panel3.Location = new System.Drawing.Point(912, 398);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(96, 49);
            this.panel3.TabIndex = 326;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(3, 13);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(96, 24);
            this.checkBox2.TabIndex = 324;
            this.checkBox2.Text = "显示停用药";
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // buttonXP2
            // 
            this.buttonXP2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.buttonXP2.DefaultScheme = true;
            this.buttonXP2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP2.Hint = "";
            this.buttonXP2.Location = new System.Drawing.Point(789, 451);
            this.buttonXP2.Name = "buttonXP2";
            this.buttonXP2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP2.Size = new System.Drawing.Size(60, 35);
            this.buttonXP2.TabIndex = 323;
            this.buttonXP2.Text = "比例(&E)";
            this.buttonXP2.Click += new System.EventHandler(this.buttonXP2_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.txtTransNo);
            this.panel2.Controls.Add(this.txtVarietyCode);
            this.panel2.Controls.Add(this.label55);
            this.panel2.Controls.Add(this.label54);
            this.panel2.Controls.Add(this.cboHighRisk);
            this.panel2.Controls.Add(this.label53);
            this.panel2.Controls.Add(this.cboMedBagUnit);
            this.panel2.Controls.Add(this.label52);
            this.panel2.Controls.Add(this.m_txtDiffPrice);
            this.panel2.Controls.Add(this.label51);
            this.panel2.Controls.Add(this.m_txtExpenseLimit);
            this.panel2.Controls.Add(this.label48);
            this.panel2.Controls.Add(this.m_txtRequestPackQty);
            this.panel2.Controls.Add(this.label50);
            this.panel2.Controls.Add(this.m_CobRequestUnit);
            this.panel2.Controls.Add(this.label49);
            this.panel2.Controls.Add(this.m_txtIpUnitPrice);
            this.panel2.Controls.Add(this.label47);
            this.panel2.Controls.Add(this.txtMEDNORMALNAME);
            this.panel2.Controls.Add(this.m_txtPY);
            this.panel2.Controls.Add(this.m_txtOPChargeCode);
            this.panel2.Controls.Add(this.label46);
            this.panel2.Controls.Add(this.m_cboCATE1);
            this.panel2.Controls.Add(this.m_cboPUTMEDTYPE);
            this.panel2.Controls.Add(this.label45);
            this.panel2.Controls.Add(this.label44);
            this.panel2.Controls.Add(this.exComboBox2);
            this.panel2.Controls.Add(this.label41);
            this.panel2.Controls.Add(this.textboxFreq);
            this.panel2.Controls.Add(this.textBoxTypedNumeric1);
            this.panel2.Controls.Add(this.label43);
            this.panel2.Controls.Add(this.m_cobCat);
            this.panel2.Controls.Add(this.label42);
            this.panel2.Controls.Add(this.exComboBox1);
            this.panel2.Controls.Add(this.label40);
            this.panel2.Controls.Add(this.m_cboMedType);
            this.panel2.Controls.Add(this.label39);
            this.panel2.Controls.Add(this.m_txtPharMatype);
            this.panel2.Controls.Add(this.label37);
            this.panel2.Controls.Add(this.label31);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.ctlTextBoxFind1);
            this.panel2.Controls.Add(this.btnDele1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.ctlCARETYPE);
            this.panel2.Controls.Add(this.label36);
            this.panel2.Controls.Add(this.label35);
            this.panel2.Controls.Add(this.label34);
            this.panel2.Controls.Add(this.LableMed);
            this.panel2.Controls.Add(this.buttonXP1);
            this.panel2.Controls.Add(this.m_txtITEMIPINVTYPE);
            this.panel2.Controls.Add(this.m_txtITEMIPCALCTYPE);
            this.panel2.Controls.Add(this.m_txtITEMOPINVTYPE);
            this.panel2.Controls.Add(this.m_txtITEMOPCALCTYPE);
            this.panel2.Controls.Add(this.m_txtUse);
            this.panel2.Controls.Add(this.m_txtNo);
            this.panel2.Controls.Add(this.m_txtChild);
            this.panel2.Controls.Add(this.label32);
            this.panel2.Controls.Add(this.m_txtAdul);
            this.panel2.Controls.Add(this.label33);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label30);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.label28);
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.txt_NMLDOSAGE);
            this.panel2.Controls.Add(this.label26);
            this.panel2.Controls.Add(this.cboIPCHARGEFLG);
            this.panel2.Controls.Add(this.cboOPCHARGEFLG);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.label23);
            this.panel2.Controls.Add(this.label29);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.m_cmbIsInDocAdv);
            this.panel2.Controls.Add(this.label25);
            this.panel2.Controls.Add(this.m_txtInsuranceID);
            this.panel2.Controls.Add(this.maxuser);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.mixuser);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.m_txtPackQty);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.m_CobIpUnit);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.m_CobUnit);
            this.panel2.Controls.Add(this.m_txtUNITPRICE);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.m_txtTRADEPRICE);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.m_CobDosageUnit);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.m_txtDosage);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.m_cboPreType);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.m_txtSpec);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.m_txtEnName);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.m_txtWB);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.m_txtName);
            this.panel2.Controls.Add(this.m_lbName);
            this.panel2.Controls.Add(this.m_lbNo);
            this.panel2.Controls.Add(this.ctlvendor);
            this.panel2.Location = new System.Drawing.Point(3, 224);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(688, 339);
            this.panel2.TabIndex = 0;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // txtTransNo
            // 
            this.txtTransNo.BackColor = System.Drawing.Color.White;
            this.txtTransNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTransNo.Location = new System.Drawing.Point(296, 312);
            this.txtTransNo.MaxLength = 100;
            this.txtTransNo.Name = "txtTransNo";
            this.txtTransNo.Size = new System.Drawing.Size(224, 23);
            this.txtTransNo.TabIndex = 372;
            // 
            // txtVarietyCode
            // 
            this.txtVarietyCode.BackColor = System.Drawing.Color.White;
            this.txtVarietyCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVarietyCode.Location = new System.Drawing.Point(61, 312);
            this.txtVarietyCode.MaxLength = 100;
            this.txtVarietyCode.Name = "txtVarietyCode";
            this.txtVarietyCode.Size = new System.Drawing.Size(168, 23);
            this.txtVarietyCode.TabIndex = 371;
            // 
            // label55
            // 
            this.label55.Location = new System.Drawing.Point(232, 316);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(64, 17);
            this.label55.TabIndex = 370;
            this.label55.Text = "药交编号";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label54
            // 
            this.label54.Location = new System.Drawing.Point(-1, 316);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(64, 17);
            this.label54.TabIndex = 369;
            this.label54.Text = "品种代码";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboHighRisk
            // 
            this.cboHighRisk.BackColor = System.Drawing.Color.White;
            this.cboHighRisk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHighRisk.ItemHeight = 14;
            this.cboHighRisk.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cboHighRisk.Location = new System.Drawing.Point(585, 289);
            this.cboHighRisk.Name = "cboHighRisk";
            this.cboHighRisk.Size = new System.Drawing.Size(96, 22);
            this.cboHighRisk.TabIndex = 368;
            this.cboHighRisk.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label53
            // 
            this.label53.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label53.Location = new System.Drawing.Point(522, 291);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(63, 17);
            this.label53.TabIndex = 367;
            this.label53.Text = "高危药品";
            this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label53.Click += new System.EventHandler(this.label53_Click);
            // 
            // cboMedBagUnit
            // 
            this.cboMedBagUnit.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cboMedBagUnit.Location = new System.Drawing.Point(585, 344);
            this.cboMedBagUnit.Name = "cboMedBagUnit";
            this.cboMedBagUnit.Size = new System.Drawing.Size(96, 22);
            this.cboMedBagUnit.TabIndex = 366;
            this.cboMedBagUnit.Visible = false;
            // 
            // label52
            // 
            this.label52.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label52.Location = new System.Drawing.Point(522, 336);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(63, 17);
            this.label52.TabIndex = 365;
            this.label52.Text = "药袋单位";
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label52.Visible = false;
            // 
            // m_txtDiffPrice
            // 
            this.m_txtDiffPrice.BackColor = System.Drawing.Color.White;
            this.m_txtDiffPrice.CausesValidation = false;
            this.m_txtDiffPrice.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtDiffPrice.Location = new System.Drawing.Point(440, 288);
            this.m_txtDiffPrice.MaxLength = 10;
            this.m_txtDiffPrice.Name = "m_txtDiffPrice";
            this.m_txtDiffPrice.ReadOnly = true;
            this.m_txtDiffPrice.Size = new System.Drawing.Size(80, 23);
            this.m_txtDiffPrice.TabIndex = 364;
            this.m_txtDiffPrice.Text = "0";
            this.m_txtDiffPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label51
            // 
            this.label51.Location = new System.Drawing.Point(376, 291);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(64, 17);
            this.label51.TabIndex = 363;
            this.label51.Text = "药品让利";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtExpenseLimit
            // 
            this.m_txtExpenseLimit.BackColor = System.Drawing.Color.White;
            this.m_txtExpenseLimit.CausesValidation = false;
            this.m_txtExpenseLimit.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtExpenseLimit.Location = new System.Drawing.Point(296, 288);
            this.m_txtExpenseLimit.MaxLength = 10;
            this.m_txtExpenseLimit.Name = "m_txtExpenseLimit";
            this.m_txtExpenseLimit.Size = new System.Drawing.Size(80, 23);
            this.m_txtExpenseLimit.TabIndex = 362;
            this.m_txtExpenseLimit.Text = "0";
            this.m_txtExpenseLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label48
            // 
            this.label48.Location = new System.Drawing.Point(232, 291);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(64, 17);
            this.label48.TabIndex = 361;
            this.label48.Text = "限报金额";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtRequestPackQty
            // 
            this.m_txtRequestPackQty.BackColor = System.Drawing.Color.White;
            this.m_txtRequestPackQty.CausesValidation = false;
            this.m_txtRequestPackQty.Font = new System.Drawing.Font("宋体", 9F);
            this.m_txtRequestPackQty.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtRequestPackQty.Location = new System.Drawing.Point(61, 289);
            this.m_txtRequestPackQty.MaxLength = 5;
            this.m_txtRequestPackQty.Name = "m_txtRequestPackQty";
            this.m_txtRequestPackQty.Size = new System.Drawing.Size(168, 21);
            this.m_txtRequestPackQty.TabIndex = 11;
            this.m_txtRequestPackQty.Text = "0";
            this.m_txtRequestPackQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("宋体", 9F);
            this.label50.Location = new System.Drawing.Point(-2, 293);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(65, 12);
            this.label50.TabIndex = 360;
            this.label50.Text = "请领包装量";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_CobRequestUnit
            // 
            this.m_CobRequestUnit.BackColor = System.Drawing.Color.White;
            this.m_CobRequestUnit.Font = new System.Drawing.Font("宋体", 9F);
            this.m_CobRequestUnit.Location = new System.Drawing.Point(61, 268);
            this.m_CobRequestUnit.Name = "m_CobRequestUnit";
            this.m_CobRequestUnit.Size = new System.Drawing.Size(169, 20);
            this.m_CobRequestUnit.TabIndex = 10;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label49.Location = new System.Drawing.Point(-1, 269);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(63, 14);
            this.label49.TabIndex = 359;
            this.label49.Text = "请领单位";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtIpUnitPrice
            // 
            this.m_txtIpUnitPrice.AccessibleDescription = "最小单位零售价";
            this.m_txtIpUnitPrice.BackColor = System.Drawing.Color.White;
            this.m_txtIpUnitPrice.CausesValidation = false;
            this.m_txtIpUnitPrice.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtIpUnitPrice.Location = new System.Drawing.Point(296, 240);
            this.m_txtIpUnitPrice.MaxLength = 10;
            this.m_txtIpUnitPrice.Name = "m_txtIpUnitPrice";
            this.m_txtIpUnitPrice.Size = new System.Drawing.Size(80, 23);
            this.m_txtIpUnitPrice.TabIndex = 22;
            this.m_txtIpUnitPrice.Text = "0";
            this.m_txtIpUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label47
            // 
            this.label47.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label47.Location = new System.Drawing.Point(236, 242);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(54, 26);
            this.label47.TabIndex = 131;
            this.label47.Text = "最小单位零售价";
            this.label47.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtMEDNORMALNAME
            // 
            this.txtMEDNORMALNAME.BackColor = System.Drawing.Color.White;
            this.txtMEDNORMALNAME.Location = new System.Drawing.Point(61, 51);
            this.txtMEDNORMALNAME.MaxLength = 20;
            this.txtMEDNORMALNAME.Name = "txtMEDNORMALNAME";
            this.txtMEDNORMALNAME.Size = new System.Drawing.Size(168, 23);
            this.txtMEDNORMALNAME.TabIndex = 1;
            this.txtMEDNORMALNAME.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMEDNORMALNAME_KeyDown);
            this.txtMEDNORMALNAME.Leave += new System.EventHandler(this.txtMEDNORMALNAME_Leave);
            // 
            // m_txtPY
            // 
            this.m_txtPY.BackColor = System.Drawing.Color.White;
            this.m_txtPY.Location = new System.Drawing.Point(61, 196);
            this.m_txtPY.MaxLength = 5;
            this.m_txtPY.Name = "m_txtPY";
            this.m_txtPY.Size = new System.Drawing.Size(168, 23);
            this.m_txtPY.TabIndex = 7;
            this.m_txtPY.TabStop = false;
            // 
            // m_txtOPChargeCode
            // 
            this.m_txtOPChargeCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtOPChargeCode.ForeColor = System.Drawing.Color.Red;
            this.m_txtOPChargeCode.Location = new System.Drawing.Point(415, 6);
            this.m_txtOPChargeCode.Name = "m_txtOPChargeCode";
            this.m_txtOPChargeCode.Size = new System.Drawing.Size(101, 18);
            this.m_txtOPChargeCode.TabIndex = 354;
            this.m_txtOPChargeCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label46
            // 
            this.label46.Location = new System.Drawing.Point(315, 8);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(113, 18);
            this.label46.TabIndex = 355;
            this.label46.Text = "门诊收费编号：";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_cboCATE1
            // 
            this.m_cboCATE1.BackColor = System.Drawing.Color.White;
            this.m_cboCATE1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCATE1.Location = new System.Drawing.Point(585, 241);
            this.m_cboCATE1.Name = "m_cboCATE1";
            this.m_cboCATE1.Size = new System.Drawing.Size(96, 22);
            this.m_cboCATE1.TabIndex = 44;
            this.m_cboCATE1.SelectedIndexChanged += new System.EventHandler(this.m_cboCATE1_SelectedIndexChanged);
            this.m_cboCATE1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboCATE1_KeyDown_1);
            // 
            // m_cboPUTMEDTYPE
            // 
            this.m_cboPUTMEDTYPE.BackColor = System.Drawing.Color.White;
            this.m_cboPUTMEDTYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboPUTMEDTYPE.Location = new System.Drawing.Point(585, 265);
            this.m_cboPUTMEDTYPE.Name = "m_cboPUTMEDTYPE";
            this.m_cboPUTMEDTYPE.Size = new System.Drawing.Size(96, 22);
            this.m_cboPUTMEDTYPE.TabIndex = 45;
            this.m_cboPUTMEDTYPE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboPUTMEDTYPE_KeyDown);
            // 
            // label45
            // 
            this.label45.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label45.Location = new System.Drawing.Point(522, 269);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(63, 17);
            this.label45.TabIndex = 353;
            this.label45.Text = "摆药分类";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label45.Click += new System.EventHandler(this.label45_Click);
            // 
            // label44
            // 
            this.label44.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label44.Location = new System.Drawing.Point(522, 246);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(63, 17);
            this.label44.TabIndex = 351;
            this.label44.Text = "医嘱分类";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // exComboBox2
            // 
            this.exComboBox2.BackColor = System.Drawing.Color.White;
            this.exComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exComboBox2.Location = new System.Drawing.Point(585, 194);
            this.exComboBox2.Name = "exComboBox2";
            this.exComboBox2.Size = new System.Drawing.Size(96, 22);
            this.exComboBox2.TabIndex = 42;
            this.exComboBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.exComboBox2_KeyDown);
            // 
            // label41
            // 
            this.label41.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label41.Location = new System.Drawing.Point(521, 198);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(63, 17);
            this.label41.TabIndex = 349;
            this.label41.Text = "住院医保";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textboxFreq
            // 
            this.textboxFreq.BackColor = System.Drawing.Color.White;
            this.textboxFreq.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textboxFreq.intHeight = 120;
            this.textboxFreq.IsEnterShow = true;
            this.textboxFreq.isHide = 2;
            this.textboxFreq.isTxt = 1;
            this.textboxFreq.isUpOrDn = 1;
            this.textboxFreq.isValuse = 2;
            this.textboxFreq.Location = new System.Drawing.Point(440, 243);
            this.textboxFreq.m_IsHaveParent = false;
            this.textboxFreq.m_strParentName = "";
            this.textboxFreq.Name = "textboxFreq";
            this.textboxFreq.nextCtl = null;
            this.textboxFreq.Size = new System.Drawing.Size(80, 24);
            this.textboxFreq.TabIndex = 33;
            this.textboxFreq.txtValuse = "";
            this.textboxFreq.VsLeftOrRight = 0;
            // 
            // textBoxTypedNumeric1
            // 
            this.textBoxTypedNumeric1.BackColor = System.Drawing.Color.White;
            this.textBoxTypedNumeric1.CausesValidation = false;
            this.textBoxTypedNumeric1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxTypedNumeric1.Location = new System.Drawing.Point(296, 147);
            this.textBoxTypedNumeric1.MaxLength = 10;
            this.textBoxTypedNumeric1.Name = "textBoxTypedNumeric1";
            this.textBoxTypedNumeric1.Size = new System.Drawing.Size(80, 23);
            this.textBoxTypedNumeric1.TabIndex = 18;
            this.textBoxTypedNumeric1.Text = "0";
            this.textBoxTypedNumeric1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxTypedNumeric1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtUNITPRICE_KeyDown);
            // 
            // label43
            // 
            this.label43.Location = new System.Drawing.Point(232, 150);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(64, 17);
            this.label43.TabIndex = 347;
            this.label43.Text = "国家限价";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_cobCat
            // 
            this.m_cobCat.BackColor = System.Drawing.Color.White;
            this.m_cobCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cobCat.Location = new System.Drawing.Point(585, 217);
            this.m_cobCat.Name = "m_cobCat";
            this.m_cobCat.Size = new System.Drawing.Size(96, 22);
            this.m_cobCat.TabIndex = 43;
            this.m_cobCat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtName_KeyDown);
            // 
            // label42
            // 
            this.label42.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label42.Location = new System.Drawing.Point(521, 221);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(63, 17);
            this.label42.TabIndex = 345;
            this.label42.Text = "执行分类";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label42.Click += new System.EventHandler(this.label42_Click);
            // 
            // exComboBox1
            // 
            this.exComboBox1.BackColor = System.Drawing.Color.White;
            this.exComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exComboBox1.Items.AddRange(new object[] {
            "",
            "西药",
            "中药",
            "材料",
            "中西药"});
            this.exComboBox1.Location = new System.Drawing.Point(61, 245);
            this.exComboBox1.Name = "exComboBox1";
            this.exComboBox1.Size = new System.Drawing.Size(169, 22);
            this.exComboBox1.TabIndex = 9;
            this.exComboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.exComboBox1_KeyDown);
            // 
            // label40
            // 
            this.label40.Location = new System.Drawing.Point(-1, 246);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(64, 17);
            this.label40.TabIndex = 341;
            this.label40.Text = "药房类型";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_cboMedType
            // 
            this.m_cboMedType.BackColor = System.Drawing.Color.White;
            this.m_cboMedType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboMedType.Location = new System.Drawing.Point(61, 221);
            this.m_cboMedType.Name = "m_cboMedType";
            this.m_cboMedType.Size = new System.Drawing.Size(169, 22);
            this.m_cboMedType.TabIndex = 8;
            this.m_cboMedType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboMedType_KeyDown_1);
            // 
            // label39
            // 
            this.label39.Location = new System.Drawing.Point(-1, 221);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(64, 17);
            this.label39.TabIndex = 339;
            this.label39.Text = "药品类型";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtPharMatype
            // 
            this.m_txtPharMatype.BackColor = System.Drawing.Color.White;
            this.m_txtPharMatype.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtPharMatype.intHeight = 120;
            this.m_txtPharMatype.IsEnterShow = true;
            this.m_txtPharMatype.isHide = 4;
            this.m_txtPharMatype.isTxt = 1;
            this.m_txtPharMatype.isUpOrDn = 1;
            this.m_txtPharMatype.isValuse = 4;
            this.m_txtPharMatype.Location = new System.Drawing.Point(296, 264);
            this.m_txtPharMatype.m_IsHaveParent = true;
            this.m_txtPharMatype.m_strParentName = "";
            this.m_txtPharMatype.Name = "m_txtPharMatype";
            this.m_txtPharMatype.nextCtl = null;
            this.m_txtPharMatype.Size = new System.Drawing.Size(80, 24);
            this.m_txtPharMatype.TabIndex = 23;
            this.m_txtPharMatype.txtValuse = "";
            this.m_txtPharMatype.VsLeftOrRight = 0;
            // 
            // label37
            // 
            this.label37.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label37.Location = new System.Drawing.Point(232, 271);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(64, 17);
            this.label37.TabIndex = 338;
            this.label37.Text = "药    理";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(376, 173);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(64, 17);
            this.label31.TabIndex = 336;
            this.label31.Text = "批准文号";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(440, 170);
            this.textBox1.MaxLength = 50;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(80, 23);
            this.textBox1.TabIndex = 30;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label3
            // 
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(521, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 17);
            this.label3.TabIndex = 334;
            this.label3.Text = "病案核算";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctlTextBoxFind1
            // 
            this.ctlTextBoxFind1.BackColor = System.Drawing.Color.White;
            this.ctlTextBoxFind1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctlTextBoxFind1.intHeight = 120;
            this.ctlTextBoxFind1.IsEnterShow = true;
            this.ctlTextBoxFind1.isHide = 2;
            this.ctlTextBoxFind1.isTxt = 1;
            this.ctlTextBoxFind1.isUpOrDn = 1;
            this.ctlTextBoxFind1.isValuse = 2;
            this.ctlTextBoxFind1.Location = new System.Drawing.Point(585, 146);
            this.ctlTextBoxFind1.m_IsHaveParent = false;
            this.ctlTextBoxFind1.m_strParentName = "";
            this.ctlTextBoxFind1.Name = "ctlTextBoxFind1";
            this.ctlTextBoxFind1.nextCtl = null;
            this.ctlTextBoxFind1.Size = new System.Drawing.Size(96, 24);
            this.ctlTextBoxFind1.TabIndex = 40;
            this.ctlTextBoxFind1.txtValuse = "";
            this.ctlTextBoxFind1.VsLeftOrRight = 0;
            this.ctlTextBoxFind1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ctlTextBoxFind1_KeyDown);
            // 
            // btnDele1
            // 
            this.btnDele1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnDele1.DefaultScheme = true;
            this.btnDele1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDele1.Hint = "";
            this.btnDele1.Location = new System.Drawing.Point(616, 2);
            this.btnDele1.Name = "btnDele1";
            this.btnDele1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnDele1.Size = new System.Drawing.Size(64, 28);
            this.btnDele1.TabIndex = 332;
            this.btnDele1.TabStop = false;
            this.btnDele1.Text = "删除(&D)";
            this.btnDele1.Click += new System.EventHandler(this.btnDele1_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 331;
            this.label2.Text = "通 用 名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctlCARETYPE
            // 
            this.ctlCARETYPE.BackColor = System.Drawing.Color.White;
            this.ctlCARETYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlCARETYPE.Location = new System.Drawing.Point(585, 170);
            this.ctlCARETYPE.Name = "ctlCARETYPE";
            this.ctlCARETYPE.Size = new System.Drawing.Size(96, 22);
            this.ctlCARETYPE.TabIndex = 41;
            this.ctlCARETYPE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ctlCARETYPE_KeyDown);
            // 
            // label36
            // 
            this.label36.Location = new System.Drawing.Point(0, 4);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(64, 17);
            this.label36.TabIndex = 327;
            this.label36.Text = "所属分类";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label35
            // 
            this.label35.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label35.Location = new System.Drawing.Point(521, 173);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(63, 17);
            this.label35.TabIndex = 326;
            this.label35.Text = "门诊医保";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label34
            // 
            this.label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label34.Location = new System.Drawing.Point(64, 26);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(456, 1);
            this.label34.TabIndex = 0;
            this.label34.Text = "label34";
            // 
            // LableMed
            // 
            this.LableMed.Location = new System.Drawing.Point(64, 3);
            this.LableMed.Name = "LableMed";
            this.LableMed.Size = new System.Drawing.Size(456, 23);
            this.LableMed.TabIndex = 323;
            this.LableMed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonXP1
            // 
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(520, 2);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(96, 28);
            this.buttonXP1.TabIndex = 322;
            this.buttonXP1.TabStop = false;
            this.buttonXP1.Text = "对应药典(&P)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // m_txtITEMIPINVTYPE
            // 
            this.m_txtITEMIPINVTYPE.BackColor = System.Drawing.Color.White;
            this.m_txtITEMIPINVTYPE.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtITEMIPINVTYPE.intHeight = 120;
            this.m_txtITEMIPINVTYPE.IsEnterShow = true;
            this.m_txtITEMIPINVTYPE.isHide = 2;
            this.m_txtITEMIPINVTYPE.isTxt = 1;
            this.m_txtITEMIPINVTYPE.isUpOrDn = 1;
            this.m_txtITEMIPINVTYPE.isValuse = 2;
            this.m_txtITEMIPINVTYPE.Location = new System.Drawing.Point(585, 123);
            this.m_txtITEMIPINVTYPE.m_IsHaveParent = false;
            this.m_txtITEMIPINVTYPE.m_strParentName = "";
            this.m_txtITEMIPINVTYPE.Name = "m_txtITEMIPINVTYPE";
            this.m_txtITEMIPINVTYPE.nextCtl = null;
            this.m_txtITEMIPINVTYPE.Size = new System.Drawing.Size(96, 24);
            this.m_txtITEMIPINVTYPE.TabIndex = 39;
            this.m_txtITEMIPINVTYPE.txtValuse = "";
            this.m_txtITEMIPINVTYPE.VsLeftOrRight = 0;
            // 
            // m_txtITEMIPCALCTYPE
            // 
            this.m_txtITEMIPCALCTYPE.BackColor = System.Drawing.Color.White;
            this.m_txtITEMIPCALCTYPE.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtITEMIPCALCTYPE.intHeight = 120;
            this.m_txtITEMIPCALCTYPE.IsEnterShow = true;
            this.m_txtITEMIPCALCTYPE.isHide = 2;
            this.m_txtITEMIPCALCTYPE.isTxt = 1;
            this.m_txtITEMIPCALCTYPE.isUpOrDn = 1;
            this.m_txtITEMIPCALCTYPE.isValuse = 2;
            this.m_txtITEMIPCALCTYPE.Location = new System.Drawing.Point(585, 100);
            this.m_txtITEMIPCALCTYPE.m_IsHaveParent = false;
            this.m_txtITEMIPCALCTYPE.m_strParentName = "";
            this.m_txtITEMIPCALCTYPE.Name = "m_txtITEMIPCALCTYPE";
            this.m_txtITEMIPCALCTYPE.nextCtl = null;
            this.m_txtITEMIPCALCTYPE.Size = new System.Drawing.Size(96, 24);
            this.m_txtITEMIPCALCTYPE.TabIndex = 38;
            this.m_txtITEMIPCALCTYPE.txtValuse = "";
            this.m_txtITEMIPCALCTYPE.VsLeftOrRight = 0;
            // 
            // m_txtITEMOPINVTYPE
            // 
            this.m_txtITEMOPINVTYPE.BackColor = System.Drawing.Color.White;
            this.m_txtITEMOPINVTYPE.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtITEMOPINVTYPE.intHeight = 120;
            this.m_txtITEMOPINVTYPE.IsEnterShow = true;
            this.m_txtITEMOPINVTYPE.isHide = 2;
            this.m_txtITEMOPINVTYPE.isTxt = 1;
            this.m_txtITEMOPINVTYPE.isUpOrDn = 1;
            this.m_txtITEMOPINVTYPE.isValuse = 2;
            this.m_txtITEMOPINVTYPE.Location = new System.Drawing.Point(585, 77);
            this.m_txtITEMOPINVTYPE.m_IsHaveParent = false;
            this.m_txtITEMOPINVTYPE.m_strParentName = "";
            this.m_txtITEMOPINVTYPE.Name = "m_txtITEMOPINVTYPE";
            this.m_txtITEMOPINVTYPE.nextCtl = null;
            this.m_txtITEMOPINVTYPE.Size = new System.Drawing.Size(96, 24);
            this.m_txtITEMOPINVTYPE.TabIndex = 37;
            this.m_txtITEMOPINVTYPE.txtValuse = "";
            this.m_txtITEMOPINVTYPE.VsLeftOrRight = 0;
            // 
            // m_txtITEMOPCALCTYPE
            // 
            this.m_txtITEMOPCALCTYPE.BackColor = System.Drawing.Color.White;
            this.m_txtITEMOPCALCTYPE.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtITEMOPCALCTYPE.intHeight = 120;
            this.m_txtITEMOPCALCTYPE.IsEnterShow = true;
            this.m_txtITEMOPCALCTYPE.isHide = 2;
            this.m_txtITEMOPCALCTYPE.isTxt = 1;
            this.m_txtITEMOPCALCTYPE.isUpOrDn = 0;
            this.m_txtITEMOPCALCTYPE.isValuse = 2;
            this.m_txtITEMOPCALCTYPE.Location = new System.Drawing.Point(585, 54);
            this.m_txtITEMOPCALCTYPE.m_IsHaveParent = false;
            this.m_txtITEMOPCALCTYPE.m_strParentName = "";
            this.m_txtITEMOPCALCTYPE.Name = "m_txtITEMOPCALCTYPE";
            this.m_txtITEMOPCALCTYPE.nextCtl = null;
            this.m_txtITEMOPCALCTYPE.Size = new System.Drawing.Size(96, 24);
            this.m_txtITEMOPCALCTYPE.TabIndex = 36;
            this.m_txtITEMOPCALCTYPE.txtValuse = "";
            this.m_txtITEMOPCALCTYPE.VsLeftOrRight = 0;
            // 
            // m_txtUse
            // 
            this.m_txtUse.BackColor = System.Drawing.Color.White;
            this.m_txtUse.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtUse.intHeight = 120;
            this.m_txtUse.IsEnterShow = true;
            this.m_txtUse.isHide = 2;
            this.m_txtUse.isTxt = 1;
            this.m_txtUse.isUpOrDn = 1;
            this.m_txtUse.isValuse = 2;
            this.m_txtUse.Location = new System.Drawing.Point(440, 218);
            this.m_txtUse.m_IsHaveParent = false;
            this.m_txtUse.m_strParentName = "";
            this.m_txtUse.Name = "m_txtUse";
            this.m_txtUse.nextCtl = null;
            this.m_txtUse.Size = new System.Drawing.Size(80, 24);
            this.m_txtUse.TabIndex = 32;
            this.m_txtUse.txtValuse = "";
            this.m_txtUse.VsLeftOrRight = 1;
            // 
            // m_txtNo
            // 
            this.m_txtNo.BackColor = System.Drawing.Color.White;
            this.m_txtNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtNo.Location = new System.Drawing.Point(61, 27);
            this.m_txtNo.MaxLength = 10;
            this.m_txtNo.Name = "m_txtNo";
            this.m_txtNo.Size = new System.Drawing.Size(168, 23);
            this.m_txtNo.TabIndex = 0;
            this.m_txtNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtNo_KeyDown);
            // 
            // m_txtChild
            // 
            this.m_txtChild.BackColor = System.Drawing.Color.White;
            this.m_txtChild.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtChild.Location = new System.Drawing.Point(440, 124);
            this.m_txtChild.MaxLength = 22;
            this.m_txtChild.Name = "m_txtChild";
            this.m_txtChild.Size = new System.Drawing.Size(80, 23);
            this.m_txtChild.TabIndex = 28;
            this.m_txtChild.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtChild.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtChild_KeyDown);
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(376, 127);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(64, 17);
            this.label32.TabIndex = 170;
            this.label32.Text = "儿童用量";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtAdul
            // 
            this.m_txtAdul.BackColor = System.Drawing.Color.White;
            this.m_txtAdul.CausesValidation = false;
            this.m_txtAdul.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtAdul.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtAdul.Location = new System.Drawing.Point(440, 101);
            this.m_txtAdul.MaxLength = 5;
            this.m_txtAdul.Name = "m_txtAdul";
            this.m_txtAdul.Size = new System.Drawing.Size(80, 23);
            this.m_txtAdul.TabIndex = 27;
            this.m_txtAdul.Text = "0";
            this.m_txtAdul.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtAdul.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtAdul_KeyDown);
            // 
            // label33
            // 
            this.label33.Location = new System.Drawing.Point(376, 104);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(64, 17);
            this.label33.TabIndex = 169;
            this.label33.Text = "成人用量";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(521, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 17);
            this.label9.TabIndex = 166;
            this.label9.Text = "住院发票";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label30
            // 
            this.label30.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label30.Location = new System.Drawing.Point(521, 104);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(63, 17);
            this.label30.TabIndex = 165;
            this.label30.Text = "住院核算";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label22.Location = new System.Drawing.Point(521, 81);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(63, 17);
            this.label22.TabIndex = 162;
            this.label22.Text = "门诊发票";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label28.Location = new System.Drawing.Point(521, 58);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(63, 17);
            this.label28.TabIndex = 161;
            this.label28.Text = "门诊核算";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(376, 150);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(64, 17);
            this.label27.TabIndex = 158;
            this.label27.Text = "医保编码";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_NMLDOSAGE
            // 
            this.txt_NMLDOSAGE.BackColor = System.Drawing.Color.White;
            this.txt_NMLDOSAGE.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txt_NMLDOSAGE.Location = new System.Drawing.Point(440, 78);
            this.txt_NMLDOSAGE.MaxLength = 20;
            this.txt_NMLDOSAGE.Name = "txt_NMLDOSAGE";
            this.txt_NMLDOSAGE.Size = new System.Drawing.Size(80, 23);
            this.txt_NMLDOSAGE.TabIndex = 26;
            this.txt_NMLDOSAGE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_NMLDOSAGE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_NMLDOSAGE_KeyDown);
            // 
            // label26
            // 
            this.label26.Location = new System.Drawing.Point(376, 81);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(64, 17);
            this.label26.TabIndex = 157;
            this.label26.Text = "常 用 量";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboIPCHARGEFLG
            // 
            this.cboIPCHARGEFLG.BackColor = System.Drawing.Color.White;
            this.cboIPCHARGEFLG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIPCHARGEFLG.Items.AddRange(new object[] {
            "",
            "基本单位",
            "最小单位"});
            this.cboIPCHARGEFLG.Location = new System.Drawing.Point(585, 32);
            this.cboIPCHARGEFLG.Name = "cboIPCHARGEFLG";
            this.cboIPCHARGEFLG.Size = new System.Drawing.Size(96, 22);
            this.cboIPCHARGEFLG.TabIndex = 35;
            this.cboIPCHARGEFLG.SelectedIndexChanged += new System.EventHandler(this.cboIPCHARGEFLG_SelectedIndexChanged);
            this.cboIPCHARGEFLG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboIPCHARGEFLG_KeyDown);
            // 
            // cboOPCHARGEFLG
            // 
            this.cboOPCHARGEFLG.BackColor = System.Drawing.Color.White;
            this.cboOPCHARGEFLG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOPCHARGEFLG.Items.AddRange(new object[] {
            "",
            "基本单位",
            "最小单位"});
            this.cboOPCHARGEFLG.Location = new System.Drawing.Point(440, 268);
            this.cboOPCHARGEFLG.Name = "cboOPCHARGEFLG";
            this.cboOPCHARGEFLG.Size = new System.Drawing.Size(80, 22);
            this.cboOPCHARGEFLG.TabIndex = 34;
            this.cboOPCHARGEFLG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboOPCHARGEFLG_KeyDown);
            // 
            // label24
            // 
            this.label24.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label24.Location = new System.Drawing.Point(521, 35);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(63, 17);
            this.label24.TabIndex = 155;
            this.label24.Text = "住院收费";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label24.Click += new System.EventHandler(this.label24_Click);
            // 
            // label23
            // 
            this.label23.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label23.Location = new System.Drawing.Point(376, 271);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(63, 17);
            this.label23.TabIndex = 153;
            this.label23.Text = "门诊收费";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label29
            // 
            this.label29.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label29.Location = new System.Drawing.Point(376, 246);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(63, 19);
            this.label29.TabIndex = 150;
            this.label29.Text = "执行频率";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label21.Location = new System.Drawing.Point(376, 221);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(63, 19);
            this.label21.TabIndex = 149;
            this.label21.Text = "用    法";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_cmbIsInDocAdv
            // 
            this.m_cmbIsInDocAdv.BackColor = System.Drawing.Color.White;
            this.m_cmbIsInDocAdv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbIsInDocAdv.Items.AddRange(new object[] {
            "",
            "否",
            "是"});
            this.m_cmbIsInDocAdv.Location = new System.Drawing.Point(440, 194);
            this.m_cmbIsInDocAdv.Name = "m_cmbIsInDocAdv";
            this.m_cmbIsInDocAdv.Size = new System.Drawing.Size(80, 22);
            this.m_cmbIsInDocAdv.TabIndex = 31;
            this.m_cmbIsInDocAdv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cmbIsInDocAdv_KeyDown);
            // 
            // label25
            // 
            this.label25.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label25.Location = new System.Drawing.Point(376, 196);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(64, 19);
            this.label25.TabIndex = 141;
            this.label25.Text = "进入医嘱";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_txtInsuranceID
            // 
            this.m_txtInsuranceID.BackColor = System.Drawing.Color.White;
            this.m_txtInsuranceID.Location = new System.Drawing.Point(440, 147);
            this.m_txtInsuranceID.MaxLength = 20;
            this.m_txtInsuranceID.Name = "m_txtInsuranceID";
            this.m_txtInsuranceID.Size = new System.Drawing.Size(80, 23);
            this.m_txtInsuranceID.TabIndex = 29;
            this.m_txtInsuranceID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtInsuranceID_KeyDown);
            // 
            // maxuser
            // 
            this.maxuser.BackColor = System.Drawing.Color.White;
            this.maxuser.CausesValidation = false;
            this.maxuser.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maxuser.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.maxuser.Location = new System.Drawing.Point(440, 55);
            this.maxuser.MaxLength = 5;
            this.maxuser.Name = "maxuser";
            this.maxuser.Size = new System.Drawing.Size(80, 23);
            this.maxuser.TabIndex = 25;
            this.maxuser.Text = "0";
            this.maxuser.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maxuser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maxuser_KeyDown);
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(376, 58);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(64, 17);
            this.label20.TabIndex = 137;
            this.label20.Text = "最大用量";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mixuser
            // 
            this.mixuser.BackColor = System.Drawing.Color.White;
            this.mixuser.CausesValidation = false;
            this.mixuser.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mixuser.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.mixuser.Location = new System.Drawing.Point(440, 32);
            this.mixuser.MaxLength = 5;
            this.mixuser.Name = "mixuser";
            this.mixuser.Size = new System.Drawing.Size(80, 23);
            this.mixuser.TabIndex = 24;
            this.mixuser.Text = "0";
            this.mixuser.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mixuser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mixuser_KeyDown);
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(376, 35);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(64, 17);
            this.label19.TabIndex = 135;
            this.label19.Text = "最小用量";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(-3, 145);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(64, 17);
            this.label18.TabIndex = 133;
            this.label18.Text = "生产厂家";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtPackQty
            // 
            this.m_txtPackQty.BackColor = System.Drawing.Color.White;
            this.m_txtPackQty.CausesValidation = false;
            this.m_txtPackQty.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtPackQty.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtPackQty.Location = new System.Drawing.Point(296, 216);
            this.m_txtPackQty.MaxLength = 5;
            this.m_txtPackQty.Name = "m_txtPackQty";
            this.m_txtPackQty.Size = new System.Drawing.Size(80, 23);
            this.m_txtPackQty.TabIndex = 21;
            this.m_txtPackQty.Text = "0";
            this.m_txtPackQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtPackQty.TextChanged += new System.EventHandler(this.m_txtPackQty_TextChanged);
            this.m_txtPackQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtPackQty_KeyDown);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(232, 220);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(64, 17);
            this.label17.TabIndex = 131;
            this.label17.Text = "包 装 量";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_CobIpUnit
            // 
            this.m_CobIpUnit.BackColor = System.Drawing.Color.White;
            this.m_CobIpUnit.Location = new System.Drawing.Point(296, 193);
            this.m_CobIpUnit.Name = "m_CobIpUnit";
            this.m_CobIpUnit.Size = new System.Drawing.Size(80, 22);
            this.m_CobIpUnit.TabIndex = 20;
            this.m_CobIpUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_CobIpUnit_KeyDown);
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(232, 196);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 17);
            this.label16.TabIndex = 129;
            this.label16.Text = "最小单位";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(232, 173);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 17);
            this.label15.TabIndex = 127;
            this.label15.Text = "基本单位";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_CobUnit
            // 
            this.m_CobUnit.BackColor = System.Drawing.Color.White;
            this.m_CobUnit.Location = new System.Drawing.Point(296, 170);
            this.m_CobUnit.Name = "m_CobUnit";
            this.m_CobUnit.Size = new System.Drawing.Size(80, 22);
            this.m_CobUnit.TabIndex = 19;
            this.m_CobUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_CobUnit_KeyDown);
            // 
            // m_txtUNITPRICE
            // 
            this.m_txtUNITPRICE.BackColor = System.Drawing.Color.White;
            this.m_txtUNITPRICE.CausesValidation = false;
            this.m_txtUNITPRICE.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtUNITPRICE.Location = new System.Drawing.Point(296, 124);
            this.m_txtUNITPRICE.MaxLength = 10;
            this.m_txtUNITPRICE.Name = "m_txtUNITPRICE";
            this.m_txtUNITPRICE.Size = new System.Drawing.Size(80, 23);
            this.m_txtUNITPRICE.TabIndex = 17;
            this.m_txtUNITPRICE.Text = "0";
            this.m_txtUNITPRICE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtUNITPRICE.TextChanged += new System.EventHandler(this.m_txtUNITPRICE_TextChanged);
            this.m_txtUNITPRICE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtUNITPRICE_KeyDown);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(232, 127);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 17);
            this.label13.TabIndex = 125;
            this.label13.Text = "零 售 价";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtTRADEPRICE
            // 
            this.m_txtTRADEPRICE.BackColor = System.Drawing.Color.White;
            this.m_txtTRADEPRICE.CausesValidation = false;
            this.m_txtTRADEPRICE.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtTRADEPRICE.Location = new System.Drawing.Point(296, 101);
            this.m_txtTRADEPRICE.MaxLength = 10;
            this.m_txtTRADEPRICE.Name = "m_txtTRADEPRICE";
            this.m_txtTRADEPRICE.Size = new System.Drawing.Size(80, 23);
            this.m_txtTRADEPRICE.TabIndex = 15;
            this.m_txtTRADEPRICE.Text = "0";
            this.m_txtTRADEPRICE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtTRADEPRICE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtTRADEPRICE_KeyDown);
            this.m_txtTRADEPRICE.Leave += new System.EventHandler(this.m_txtTRADEPRICE_Leave);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(232, 104);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 17);
            this.label14.TabIndex = 123;
            this.label14.Text = "购 进 价";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_CobDosageUnit
            // 
            this.m_CobDosageUnit.BackColor = System.Drawing.Color.White;
            this.m_CobDosageUnit.Location = new System.Drawing.Point(296, 78);
            this.m_CobDosageUnit.Name = "m_CobDosageUnit";
            this.m_CobDosageUnit.Size = new System.Drawing.Size(80, 22);
            this.m_CobDosageUnit.TabIndex = 14;
            this.m_CobDosageUnit.SelectedIndexChanged += new System.EventHandler(this.m_CobDosageUnit_SelectedIndexChanged);
            this.m_CobDosageUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_CobDosageUnit_KeyDown);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(232, 81);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 17);
            this.label12.TabIndex = 121;
            this.label12.Text = "剂量单位";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtDosage
            // 
            this.m_txtDosage.BackColor = System.Drawing.Color.White;
            this.m_txtDosage.CausesValidation = false;
            this.m_txtDosage.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtDosage.Location = new System.Drawing.Point(296, 55);
            this.m_txtDosage.MaxLength = 7;
            this.m_txtDosage.Name = "m_txtDosage";
            this.m_txtDosage.Size = new System.Drawing.Size(80, 23);
            this.m_txtDosage.TabIndex = 13;
            this.m_txtDosage.Text = "0";
            this.m_txtDosage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtDosage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtDosage_KeyDown);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(232, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 17);
            this.label11.TabIndex = 119;
            this.label11.Text = "剂    量";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_cboPreType
            // 
            this.m_cboPreType.BackColor = System.Drawing.Color.White;
            this.m_cboPreType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboPreType.Items.AddRange(new object[] {
            ""});
            this.m_cboPreType.Location = new System.Drawing.Point(296, 32);
            this.m_cboPreType.Name = "m_cboPreType";
            this.m_cboPreType.Size = new System.Drawing.Size(80, 22);
            this.m_cboPreType.TabIndex = 12;
            this.m_cboPreType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboPreType_KeyDown);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(232, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 17);
            this.label10.TabIndex = 117;
            this.label10.Text = "剂    型";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtSpec
            // 
            this.m_txtSpec.BackColor = System.Drawing.Color.White;
            this.m_txtSpec.Location = new System.Drawing.Point(61, 123);
            this.m_txtSpec.MaxLength = 100;
            this.m_txtSpec.Name = "m_txtSpec";
            this.m_txtSpec.Size = new System.Drawing.Size(168, 23);
            this.m_txtSpec.TabIndex = 4;
            this.m_txtSpec.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtSpec_KeyDown);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(-9, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 17);
            this.label8.TabIndex = 113;
            this.label8.Text = "规    格";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtEnName
            // 
            this.m_txtEnName.BackColor = System.Drawing.Color.White;
            this.m_txtEnName.Location = new System.Drawing.Point(61, 99);
            this.m_txtEnName.MaxLength = 50;
            this.m_txtEnName.Name = "m_txtEnName";
            this.m_txtEnName.Size = new System.Drawing.Size(168, 23);
            this.m_txtEnName.TabIndex = 3;
            this.m_txtEnName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtEnName_KeyDown);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 17);
            this.label7.TabIndex = 111;
            this.label7.Text = "英 文 名";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(-2, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 17);
            this.label6.TabIndex = 108;
            this.label6.Text = "拼 音 码";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtWB
            // 
            this.m_txtWB.BackColor = System.Drawing.Color.White;
            this.m_txtWB.Location = new System.Drawing.Point(61, 171);
            this.m_txtWB.MaxLength = 5;
            this.m_txtWB.Name = "m_txtWB";
            this.m_txtWB.Size = new System.Drawing.Size(168, 23);
            this.m_txtWB.TabIndex = 6;
            this.m_txtWB.TabStop = false;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(-2, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 17);
            this.label5.TabIndex = 106;
            this.label5.Text = "五 笔 码";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtName
            // 
            this.m_txtName.BackColor = System.Drawing.Color.White;
            this.m_txtName.Location = new System.Drawing.Point(61, 75);
            this.m_txtName.MaxLength = 200;
            this.m_txtName.Name = "m_txtName";
            this.m_txtName.Size = new System.Drawing.Size(168, 23);
            this.m_txtName.TabIndex = 2;
            this.m_txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtName_KeyDown);
            this.m_txtName.Leave += new System.EventHandler(this.m_txtName_Leave);
            // 
            // m_lbName
            // 
            this.m_lbName.Location = new System.Drawing.Point(0, 77);
            this.m_lbName.Name = "m_lbName";
            this.m_lbName.Size = new System.Drawing.Size(64, 17);
            this.m_lbName.TabIndex = 105;
            this.m_lbName.Text = "商 品 名";
            this.m_lbName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lbNo
            // 
            this.m_lbNo.Location = new System.Drawing.Point(0, 30);
            this.m_lbNo.Name = "m_lbNo";
            this.m_lbNo.Size = new System.Drawing.Size(64, 17);
            this.m_lbNo.TabIndex = 103;
            this.m_lbNo.Text = "项目编码";
            this.m_lbNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctlvendor
            // 
            this.ctlvendor.BackColor = System.Drawing.Color.White;
            this.ctlvendor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctlvendor.Location = new System.Drawing.Point(61, 147);
            this.ctlvendor.Name = "ctlvendor";
            this.ctlvendor.Size = new System.Drawing.Size(168, 23);
            this.ctlvendor.TabIndex = 5;
            this.ctlvendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ctlvendor_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.cobSelectType);
            this.groupBox2.Controls.Add(this.label38);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.ComType);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnDele);
            this.groupBox2.Location = new System.Drawing.Point(696, 483);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(312, 80);
            this.groupBox2.TabIndex = 50;
            this.groupBox2.TabStop = false;
            // 
            // cobSelectType
            // 
            this.cobSelectType.BackColor = System.Drawing.Color.White;
            this.cobSelectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobSelectType.Location = new System.Drawing.Point(8, 48);
            this.cobSelectType.Name = "cobSelectType";
            this.cobSelectType.Size = new System.Drawing.Size(72, 22);
            this.cobSelectType.TabIndex = 126;
            this.cobSelectType.SelectedIndexChanged += new System.EventHandler(this.cobSelectType_SelectedIndexChanged);
            // 
            // label38
            // 
            this.label38.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label38.Location = new System.Drawing.Point(8, 21);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(64, 17);
            this.label38.TabIndex = 125;
            this.label38.Text = "药品类型";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_cboFindContent);
            this.groupBox3.Controls.Add(this.m_CboSelect);
            this.groupBox3.Controls.Add(this.m_btnFind);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(80, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(232, 80);
            this.groupBox3.TabIndex = 124;
            this.groupBox3.TabStop = false;
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // m_cboFindContent
            // 
            this.m_cboFindContent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_cboFindContent.BackColor = System.Drawing.Color.White;
            this.m_cboFindContent.Location = new System.Drawing.Point(141, 21);
            this.m_cboFindContent.Name = "m_cboFindContent";
            this.m_cboFindContent.Size = new System.Drawing.Size(86, 22);
            this.m_cboFindContent.TabIndex = 124;
            this.m_cboFindContent.SelectedIndexChanged += new System.EventHandler(this.m_cboFindContent_SelectedIndexChanged);
            this.m_cboFindContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboFindContent_KeyDown);
            // 
            // m_CboSelect
            // 
            this.m_CboSelect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_CboSelect.BackColor = System.Drawing.Color.White;
            this.m_CboSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_CboSelect.Items.AddRange(new object[] {
            "药品助记码",
            "药品名称",
            "药品英文名",
            "药品拼音码",
            "药品五笔码",
            "剂型"});
            this.m_CboSelect.Location = new System.Drawing.Point(66, 21);
            this.m_CboSelect.Name = "m_CboSelect";
            this.m_CboSelect.Size = new System.Drawing.Size(72, 22);
            this.m_CboSelect.TabIndex = 48;
            // 
            // m_btnFind
            // 
            this.m_btnFind.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_btnFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_btnFind.DefaultScheme = true;
            this.m_btnFind.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnFind.Hint = "";
            this.m_btnFind.Location = new System.Drawing.Point(32, 48);
            this.m_btnFind.Name = "m_btnFind";
            this.m_btnFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnFind.Size = new System.Drawing.Size(88, 28);
            this.m_btnFind.TabIndex = 44;
            this.m_btnFind.Text = "查找(&F)";
            this.m_btnFind.Click += new System.EventHandler(this.m_btnFind_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.DefaultScheme = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Hint = "";
            this.btnClose.Location = new System.Drawing.Point(136, 48);
            this.btnClose.Name = "btnClose";
            this.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnClose.Size = new System.Drawing.Size(88, 28);
            this.btnClose.TabIndex = 45;
            this.btnClose.Text = "返回(&R)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 122;
            this.label1.Text = "查找方式";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ComType
            // 
            this.ComType.Enabled = false;
            this.ComType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ComType.ItemHeight = 14;
            this.ComType.Location = new System.Drawing.Point(32, -32);
            this.ComType.Name = "ComType";
            this.ComType.Size = new System.Drawing.Size(80, 22);
            this.ComType.TabIndex = 40;
            this.ComType.SelectedIndexChanged += new System.EventHandler(this.ComType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(-32, -32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 32);
            this.label4.TabIndex = 20;
            this.label4.Text = "药品分类";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDele
            // 
            this.btnDele.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnDele.DefaultScheme = true;
            this.btnDele.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDele.Hint = "";
            this.btnDele.Location = new System.Drawing.Point(16, 80);
            this.btnDele.Name = "btnDele";
            this.btnDele.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnDele.Size = new System.Drawing.Size(64, 28);
            this.btnDele.TabIndex = 32;
            this.btnDele.Text = "删除(&C)";
            this.btnDele.Visible = false;
            this.btnDele.Click += new System.EventHandler(this.btnDele_Click);
            // 
            // m_dgList
            // 
            this.m_dgList.AllowAddNew = false;
            this.m_dgList.AllowDelete = false;
            this.m_dgList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dgList.AutoAppendRow = false;
            this.m_dgList.AutoScroll = true;
            this.m_dgList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.m_dgList.CaptionText = "";
            this.m_dgList.CaptionVisible = false;
            this.m_dgList.CausesValidation = false;
            this.m_dgList.ColumnHeadersVisible = true;
            clsColumnInfo1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo1.BackColor = System.Drawing.Color.White;
            clsColumnInfo1.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo1.ColumnIndex = 0;
            clsColumnInfo1.ColumnName = "ASSISTCODE_CHR";
            clsColumnInfo1.ColumnWidth = 75;
            clsColumnInfo1.Enabled = false;
            clsColumnInfo1.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo1.HeadText = "助记码";
            clsColumnInfo1.ReadOnly = true;
            clsColumnInfo1.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo2.BackColor = System.Drawing.Color.White;
            clsColumnInfo2.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo2.ColumnIndex = 1;
            clsColumnInfo2.ColumnName = "MEDNORMALNAME_VCHR";
            clsColumnInfo2.ColumnWidth = 75;
            clsColumnInfo2.Enabled = false;
            clsColumnInfo2.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo2.HeadText = "通用名";
            clsColumnInfo2.ReadOnly = true;
            clsColumnInfo2.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo3.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo3.BackColor = System.Drawing.Color.White;
            clsColumnInfo3.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo3.ColumnIndex = 2;
            clsColumnInfo3.ColumnName = "MEDICINENAME_VCHR";
            clsColumnInfo3.ColumnWidth = 140;
            clsColumnInfo3.Enabled = false;
            clsColumnInfo3.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo3.HeadText = "药品名称";
            clsColumnInfo3.ReadOnly = true;
            clsColumnInfo3.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo4.BackColor = System.Drawing.Color.White;
            clsColumnInfo4.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo4.ColumnIndex = 3;
            clsColumnInfo4.ColumnName = "MEDICINEENGNAME_VCHR";
            clsColumnInfo4.ColumnWidth = 75;
            clsColumnInfo4.Enabled = false;
            clsColumnInfo4.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo4.HeadText = "英文名";
            clsColumnInfo4.ReadOnly = true;
            clsColumnInfo4.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo5.BackColor = System.Drawing.Color.White;
            clsColumnInfo5.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo5.ColumnIndex = 4;
            clsColumnInfo5.ColumnName = "MEDICINETYPENAME_VCHR";
            clsColumnInfo5.ColumnWidth = 40;
            clsColumnInfo5.Enabled = false;
            clsColumnInfo5.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo5.HeadText = "类别";
            clsColumnInfo5.ReadOnly = true;
            clsColumnInfo5.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo6.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo6.BackColor = System.Drawing.Color.White;
            clsColumnInfo6.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo6.ColumnIndex = 5;
            clsColumnInfo6.ColumnName = "MEDICINEPREPTYPENAME_VCHR";
            clsColumnInfo6.ColumnWidth = 40;
            clsColumnInfo6.Enabled = false;
            clsColumnInfo6.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo6.HeadText = "制剂";
            clsColumnInfo6.ReadOnly = true;
            clsColumnInfo6.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo7.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo7.BackColor = System.Drawing.Color.White;
            clsColumnInfo7.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo7.ColumnIndex = 6;
            clsColumnInfo7.ColumnName = "MEDSPEC_VCHR";
            clsColumnInfo7.ColumnWidth = 120;
            clsColumnInfo7.Enabled = false;
            clsColumnInfo7.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo7.HeadText = "规格";
            clsColumnInfo7.ReadOnly = true;
            clsColumnInfo7.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo8.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo8.BackColor = System.Drawing.Color.White;
            clsColumnInfo8.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo8.ColumnIndex = 7;
            clsColumnInfo8.ColumnName = "TRADEPRICE_MNY";
            clsColumnInfo8.ColumnWidth = 75;
            clsColumnInfo8.Enabled = false;
            clsColumnInfo8.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo8.HeadText = "购进价";
            clsColumnInfo8.ReadOnly = true;
            clsColumnInfo8.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo9.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo9.BackColor = System.Drawing.Color.White;
            clsColumnInfo9.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo9.ColumnIndex = 8;
            clsColumnInfo9.ColumnName = "UNITPRICE_MNY";
            clsColumnInfo9.ColumnWidth = 75;
            clsColumnInfo9.Enabled = false;
            clsColumnInfo9.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo9.HeadText = "单价";
            clsColumnInfo9.ReadOnly = true;
            clsColumnInfo9.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo10.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo10.BackColor = System.Drawing.Color.White;
            clsColumnInfo10.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo10.ColumnIndex = 9;
            clsColumnInfo10.ColumnName = "DIFFPRICE_MNY";
            clsColumnInfo10.ColumnWidth = 75;
            clsColumnInfo10.Enabled = false;
            clsColumnInfo10.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo10.HeadText = "药品让利";
            clsColumnInfo10.ReadOnly = true;
            clsColumnInfo10.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo11.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo11.BackColor = System.Drawing.Color.White;
            clsColumnInfo11.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo11.ColumnIndex = 10;
            clsColumnInfo11.ColumnName = "LIMITUNITPRICE_MNY";
            clsColumnInfo11.ColumnWidth = 75;
            clsColumnInfo11.Enabled = false;
            clsColumnInfo11.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo11.HeadText = "国家限价";
            clsColumnInfo11.ReadOnly = true;
            clsColumnInfo11.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo12.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo12.BackColor = System.Drawing.Color.White;
            clsColumnInfo12.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo12.ColumnIndex = 11;
            clsColumnInfo12.ColumnName = "PRODUCTORID_CHR";
            clsColumnInfo12.ColumnWidth = 80;
            clsColumnInfo12.Enabled = false;
            clsColumnInfo12.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo12.HeadText = "生产厂家";
            clsColumnInfo12.ReadOnly = true;
            clsColumnInfo12.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo13.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo13.BackColor = System.Drawing.Color.White;
            clsColumnInfo13.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo13.ColumnIndex = 12;
            clsColumnInfo13.ColumnName = "DOSAGE_DEC";
            clsColumnInfo13.ColumnWidth = 40;
            clsColumnInfo13.Enabled = false;
            clsColumnInfo13.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo13.HeadText = "剂量";
            clsColumnInfo13.ReadOnly = true;
            clsColumnInfo13.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo14.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo14.BackColor = System.Drawing.Color.White;
            clsColumnInfo14.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo14.ColumnIndex = 13;
            clsColumnInfo14.ColumnName = "DOSAGEUNIT_CHR";
            clsColumnInfo14.ColumnWidth = 60;
            clsColumnInfo14.Enabled = false;
            clsColumnInfo14.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo14.HeadText = "剂量单位";
            clsColumnInfo14.ReadOnly = true;
            clsColumnInfo14.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo15.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo15.BackColor = System.Drawing.Color.White;
            clsColumnInfo15.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo15.ColumnIndex = 14;
            clsColumnInfo15.ColumnName = "OPUNIT_CHR";
            clsColumnInfo15.ColumnWidth = 60;
            clsColumnInfo15.Enabled = false;
            clsColumnInfo15.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo15.HeadText = "门诊单位";
            clsColumnInfo15.ReadOnly = true;
            clsColumnInfo15.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo16.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo16.BackColor = System.Drawing.Color.White;
            clsColumnInfo16.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo16.ColumnIndex = 15;
            clsColumnInfo16.ColumnName = "IPUNIT_CHR";
            clsColumnInfo16.ColumnWidth = 60;
            clsColumnInfo16.Enabled = false;
            clsColumnInfo16.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo16.HeadText = "住院单位";
            clsColumnInfo16.ReadOnly = true;
            clsColumnInfo16.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo17.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo17.BackColor = System.Drawing.Color.White;
            clsColumnInfo17.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo17.ColumnIndex = 16;
            clsColumnInfo17.ColumnName = "PACKQTY_DEC";
            clsColumnInfo17.ColumnWidth = 50;
            clsColumnInfo17.Enabled = false;
            clsColumnInfo17.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo17.HeadText = "包装量";
            clsColumnInfo17.ReadOnly = true;
            clsColumnInfo17.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo18.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo18.BackColor = System.Drawing.Color.White;
            clsColumnInfo18.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo18.ColumnIndex = 17;
            clsColumnInfo18.ColumnName = "MINDOSAGE_DEC";
            clsColumnInfo18.ColumnWidth = 75;
            clsColumnInfo18.Enabled = false;
            clsColumnInfo18.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo18.HeadText = "最小用量";
            clsColumnInfo18.ReadOnly = true;
            clsColumnInfo18.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo19.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo19.BackColor = System.Drawing.Color.White;
            clsColumnInfo19.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo19.ColumnIndex = 18;
            clsColumnInfo19.ColumnName = "MAXDOSAGE_DEC";
            clsColumnInfo19.ColumnWidth = 75;
            clsColumnInfo19.Enabled = false;
            clsColumnInfo19.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo19.HeadText = "最大用量";
            clsColumnInfo19.ReadOnly = true;
            clsColumnInfo19.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo20.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo20.BackColor = System.Drawing.Color.White;
            clsColumnInfo20.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo20.ColumnIndex = 19;
            clsColumnInfo20.ColumnName = "NMLDOSAGE_DEC";
            clsColumnInfo20.ColumnWidth = 75;
            clsColumnInfo20.Enabled = false;
            clsColumnInfo20.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo20.HeadText = "常用量";
            clsColumnInfo20.ReadOnly = true;
            clsColumnInfo20.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo21.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo21.BackColor = System.Drawing.Color.White;
            clsColumnInfo21.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo21.ColumnIndex = 20;
            clsColumnInfo21.ColumnName = "pharmaname_vchr";
            clsColumnInfo21.ColumnWidth = 100;
            clsColumnInfo21.Enabled = false;
            clsColumnInfo21.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo21.HeadText = "药理分类";
            clsColumnInfo21.ReadOnly = true;
            clsColumnInfo21.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo22.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo22.BackColor = System.Drawing.Color.White;
            clsColumnInfo22.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo22.ColumnIndex = 21;
            clsColumnInfo22.ColumnName = "DEPTPREP";
            clsColumnInfo22.ColumnWidth = 70;
            clsColumnInfo22.Enabled = false;
            clsColumnInfo22.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo22.HeadText = "科室自备";
            clsColumnInfo22.ReadOnly = true;
            clsColumnInfo22.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo23.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo23.BackColor = System.Drawing.Color.White;
            clsColumnInfo23.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo23.ColumnIndex = 22;
            clsColumnInfo23.ColumnName = "ISANAESTHESIA";
            clsColumnInfo23.ColumnWidth = 70;
            clsColumnInfo23.Enabled = false;
            clsColumnInfo23.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo23.HeadText = "麻醉药品";
            clsColumnInfo23.ReadOnly = true;
            clsColumnInfo23.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo24.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo24.BackColor = System.Drawing.Color.White;
            clsColumnInfo24.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo24.ColumnIndex = 23;
            clsColumnInfo24.ColumnName = "ISPOISON";
            clsColumnInfo24.ColumnWidth = 70;
            clsColumnInfo24.Enabled = false;
            clsColumnInfo24.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo24.HeadText = "毒性药品";
            clsColumnInfo24.ReadOnly = true;
            clsColumnInfo24.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo25.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo25.BackColor = System.Drawing.Color.White;
            clsColumnInfo25.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo25.ColumnIndex = 24;
            clsColumnInfo25.ColumnName = "ISCHLORPROMAZIN";
            clsColumnInfo25.ColumnWidth = 70;
            clsColumnInfo25.Enabled = false;
            clsColumnInfo25.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo25.HeadText = "精神一类";
            clsColumnInfo25.ReadOnly = true;
            clsColumnInfo25.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo26.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo26.BackColor = System.Drawing.Color.White;
            clsColumnInfo26.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo26.ColumnIndex = 25;
            clsColumnInfo26.ColumnName = "ISCHLORPROMAZINE2";
            clsColumnInfo26.ColumnWidth = 70;
            clsColumnInfo26.Enabled = false;
            clsColumnInfo26.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo26.HeadText = "精神二类";
            clsColumnInfo26.ReadOnly = true;
            clsColumnInfo26.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo27.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo27.BackColor = System.Drawing.Color.White;
            clsColumnInfo27.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo27.ColumnIndex = 26;
            clsColumnInfo27.ColumnName = "ISCOSTLY";
            clsColumnInfo27.ColumnWidth = 70;
            clsColumnInfo27.Enabled = false;
            clsColumnInfo27.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo27.HeadText = "贵重药品";
            clsColumnInfo27.ReadOnly = true;
            clsColumnInfo27.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo28.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo28.BackColor = System.Drawing.Color.White;
            clsColumnInfo28.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo28.ColumnIndex = 27;
            clsColumnInfo28.ColumnName = "ISSELF";
            clsColumnInfo28.ColumnWidth = 70;
            clsColumnInfo28.Enabled = false;
            clsColumnInfo28.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo28.HeadText = "院内制剂";
            clsColumnInfo28.ReadOnly = true;
            clsColumnInfo28.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo29.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo29.BackColor = System.Drawing.Color.White;
            clsColumnInfo29.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo29.ColumnIndex = 28;
            clsColumnInfo29.ColumnName = "ISIMPORT";
            clsColumnInfo29.ColumnWidth = 70;
            clsColumnInfo29.Enabled = false;
            clsColumnInfo29.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo29.HeadText = "进口药品";
            clsColumnInfo29.ReadOnly = true;
            clsColumnInfo29.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo30.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo30.BackColor = System.Drawing.Color.White;
            clsColumnInfo30.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo30.ColumnIndex = 29;
            clsColumnInfo30.ColumnName = "ISSELFPAY";
            clsColumnInfo30.ColumnWidth = 70;
            clsColumnInfo30.Enabled = false;
            clsColumnInfo30.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo30.HeadText = "自费药品";
            clsColumnInfo30.ReadOnly = true;
            clsColumnInfo30.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo31.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo31.BackColor = System.Drawing.Color.White;
            clsColumnInfo31.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo31.ColumnIndex = 30;
            clsColumnInfo31.ColumnName = "isSTANDARD";
            clsColumnInfo31.ColumnWidth = 75;
            clsColumnInfo31.Enabled = true;
            clsColumnInfo31.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo31.HeadText = "中标药品";
            clsColumnInfo31.ReadOnly = false;
            clsColumnInfo31.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo32.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo32.BackColor = System.Drawing.Color.White;
            clsColumnInfo32.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo32.ColumnIndex = 31;
            clsColumnInfo32.ColumnName = "POFLAG";
            clsColumnInfo32.ColumnWidth = 100;
            clsColumnInfo32.Enabled = false;
            clsColumnInfo32.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo32.HeadText = "是否进入医属";
            clsColumnInfo32.ReadOnly = true;
            clsColumnInfo32.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo33.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo33.BackColor = System.Drawing.Color.White;
            clsColumnInfo33.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo33.ColumnIndex = 32;
            clsColumnInfo33.ColumnName = "USAGENAME_VCHR";
            clsColumnInfo33.ColumnWidth = 75;
            clsColumnInfo33.Enabled = false;
            clsColumnInfo33.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo33.HeadText = "用法";
            clsColumnInfo33.ReadOnly = true;
            clsColumnInfo33.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo34.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo34.BackColor = System.Drawing.Color.White;
            clsColumnInfo34.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo34.ColumnIndex = 33;
            clsColumnInfo34.ColumnName = "FREQNAME_CHR";
            clsColumnInfo34.ColumnWidth = 75;
            clsColumnInfo34.Enabled = false;
            clsColumnInfo34.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo34.HeadText = "频率";
            clsColumnInfo34.ReadOnly = true;
            clsColumnInfo34.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo35.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo35.BackColor = System.Drawing.Color.White;
            clsColumnInfo35.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo35.ColumnIndex = 34;
            clsColumnInfo35.ColumnName = "OPCHARGEFLG";
            clsColumnInfo35.ColumnWidth = 75;
            clsColumnInfo35.Enabled = false;
            clsColumnInfo35.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo35.HeadText = "门诊收费单位";
            clsColumnInfo35.ReadOnly = true;
            clsColumnInfo35.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo36.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo36.BackColor = System.Drawing.Color.White;
            clsColumnInfo36.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo36.ColumnIndex = 35;
            clsColumnInfo36.ColumnName = "IPCHARGEFLG";
            clsColumnInfo36.ColumnWidth = 75;
            clsColumnInfo36.Enabled = false;
            clsColumnInfo36.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo36.HeadText = "住院收费单位";
            clsColumnInfo36.ReadOnly = true;
            clsColumnInfo36.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo37.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo37.BackColor = System.Drawing.Color.White;
            clsColumnInfo37.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo37.ColumnIndex = 36;
            clsColumnInfo37.ColumnName = "IFSTOP";
            clsColumnInfo37.ColumnWidth = 75;
            clsColumnInfo37.Enabled = false;
            clsColumnInfo37.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo37.HeadText = "是否停用";
            clsColumnInfo37.ReadOnly = true;
            clsColumnInfo37.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo38.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo38.BackColor = System.Drawing.Color.White;
            clsColumnInfo38.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo38.ColumnIndex = 37;
            clsColumnInfo38.ColumnName = "INSURANCEID_VCHR";
            clsColumnInfo38.ColumnWidth = 75;
            clsColumnInfo38.Enabled = false;
            clsColumnInfo38.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo38.HeadText = "医保号码";
            clsColumnInfo38.ReadOnly = true;
            clsColumnInfo38.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo39.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo39.BackColor = System.Drawing.Color.White;
            clsColumnInfo39.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo39.ColumnIndex = 38;
            clsColumnInfo39.ColumnName = "pharmaid_chr";
            clsColumnInfo39.ColumnWidth = 0;
            clsColumnInfo39.Enabled = true;
            clsColumnInfo39.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo39.HeadText = "pharmaid_chr";
            clsColumnInfo39.ReadOnly = false;
            clsColumnInfo39.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo40.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo40.BackColor = System.Drawing.Color.White;
            clsColumnInfo40.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo40.ColumnIndex = 39;
            clsColumnInfo40.ColumnName = "TYPENAME_VCHR1";
            clsColumnInfo40.ColumnWidth = 75;
            clsColumnInfo40.Enabled = false;
            clsColumnInfo40.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo40.HeadText = "住院医保";
            clsColumnInfo40.ReadOnly = true;
            clsColumnInfo40.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo41.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo41.BackColor = System.Drawing.Color.White;
            clsColumnInfo41.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo41.ColumnIndex = 40;
            clsColumnInfo41.ColumnName = "TYPENAME_VCHR";
            clsColumnInfo41.ColumnWidth = 75;
            clsColumnInfo41.Enabled = false;
            clsColumnInfo41.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo41.HeadText = "门诊医保";
            clsColumnInfo41.ReadOnly = true;
            clsColumnInfo41.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo42.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo42.BackColor = System.Drawing.Color.White;
            clsColumnInfo42.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo42.ColumnIndex = 41;
            clsColumnInfo42.ColumnName = "ORDERCATENAME_VCHR";
            clsColumnInfo42.ColumnWidth = 75;
            clsColumnInfo42.Enabled = false;
            clsColumnInfo42.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo42.HeadText = "执行分类";
            clsColumnInfo42.ReadOnly = true;
            clsColumnInfo42.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo43.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo43.BackColor = System.Drawing.Color.White;
            clsColumnInfo43.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo43.ColumnIndex = 42;
            clsColumnInfo43.ColumnName = "NAME_CHR";
            clsColumnInfo43.ColumnWidth = 75;
            clsColumnInfo43.Enabled = false;
            clsColumnInfo43.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo43.HeadText = "医嘱分类";
            clsColumnInfo43.ReadOnly = true;
            clsColumnInfo43.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo44.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo44.BackColor = System.Drawing.Color.White;
            clsColumnInfo44.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo44.ColumnIndex = 43;
            clsColumnInfo44.ColumnName = "PUTMEDTYPEName";
            clsColumnInfo44.ColumnWidth = 75;
            clsColumnInfo44.Enabled = true;
            clsColumnInfo44.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo44.HeadText = "摆药分类";
            clsColumnInfo44.ReadOnly = false;
            clsColumnInfo44.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo45.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo45.BackColor = System.Drawing.Color.White;
            clsColumnInfo45.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo45.ColumnIndex = 44;
            clsColumnInfo45.ColumnName = "itemopcode_chr";
            clsColumnInfo45.ColumnWidth = 0;
            clsColumnInfo45.Enabled = true;
            clsColumnInfo45.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo45.HeadText = "门诊收费编码";
            clsColumnInfo45.ReadOnly = false;
            clsColumnInfo45.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo46.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo46.BackColor = System.Drawing.Color.White;
            clsColumnInfo46.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo46.ColumnIndex = 45;
            clsColumnInfo46.ColumnName = "IPUNITPRICE_MNY";
            clsColumnInfo46.ColumnWidth = 0;
            clsColumnInfo46.Enabled = true;
            clsColumnInfo46.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo46.HeadText = "最小单位零售单价";
            clsColumnInfo46.ReadOnly = true;
            clsColumnInfo46.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo47.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo47.BackColor = System.Drawing.Color.White;
            clsColumnInfo47.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo47.ColumnIndex = 46;
            clsColumnInfo47.ColumnName = "STANDARDDATE";
            clsColumnInfo47.ColumnWidth = 75;
            clsColumnInfo47.Enabled = true;
            clsColumnInfo47.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo47.HeadText = "中标年份";
            clsColumnInfo47.ReadOnly = true;
            clsColumnInfo47.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo48.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo48.BackColor = System.Drawing.Color.White;
            clsColumnInfo48.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo48.ColumnIndex = 47;
            clsColumnInfo48.ColumnName = "REQUESTUNIT_CHR";
            clsColumnInfo48.ColumnWidth = 75;
            clsColumnInfo48.Enabled = true;
            clsColumnInfo48.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo48.HeadText = "请领单位";
            clsColumnInfo48.ReadOnly = true;
            clsColumnInfo48.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo49.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo49.BackColor = System.Drawing.Color.White;
            clsColumnInfo49.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo49.ColumnIndex = 48;
            clsColumnInfo49.ColumnName = "REQUESTPACKQTY_DEC";
            clsColumnInfo49.ColumnWidth = 75;
            clsColumnInfo49.Enabled = true;
            clsColumnInfo49.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo49.HeadText = "请领包装量";
            clsColumnInfo49.ReadOnly = true;
            clsColumnInfo49.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo50.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo50.BackColor = System.Drawing.Color.White;
            clsColumnInfo50.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo50.ColumnIndex = 49;
            clsColumnInfo50.ColumnName = "EXPENSELIMIT_MNY";
            clsColumnInfo50.ColumnWidth = 75;
            clsColumnInfo50.Enabled = false;
            clsColumnInfo50.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo50.HeadText = "限报金额";
            clsColumnInfo50.ReadOnly = true;
            clsColumnInfo50.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo51.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo51.BackColor = System.Drawing.Color.White;
            clsColumnInfo51.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo51.ColumnIndex = 50;
            clsColumnInfo51.ColumnName = "medbagunit";
            clsColumnInfo51.ColumnWidth = 75;
            clsColumnInfo51.Enabled = false;
            clsColumnInfo51.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo51.HeadText = "药袋单位";
            clsColumnInfo51.ReadOnly = true;
            clsColumnInfo51.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo52.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo52.BackColor = System.Drawing.Color.White;
            clsColumnInfo52.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo52.ColumnIndex = 51;
            clsColumnInfo52.ColumnName = "highriskflag";
            clsColumnInfo52.ColumnWidth = 0;
            clsColumnInfo52.Enabled = false;
            clsColumnInfo52.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo52.HeadText = "高危药品";
            clsColumnInfo52.ReadOnly = true;
            clsColumnInfo52.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo53.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo53.BackColor = System.Drawing.Color.White;
            clsColumnInfo53.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo53.ColumnIndex = 52;
            clsColumnInfo53.ColumnName = "isproducedrugs";
            clsColumnInfo53.ColumnWidth = 0;
            clsColumnInfo53.Enabled = false;
            clsColumnInfo53.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo53.HeadText = "易制毒";
            clsColumnInfo53.ReadOnly = true;
            clsColumnInfo53.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo54.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo54.BackColor = System.Drawing.Color.White;
            clsColumnInfo54.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo54.ColumnIndex = 53;
            clsColumnInfo54.ColumnName = "transno";
            clsColumnInfo54.ColumnWidth = 0;
            clsColumnInfo54.Enabled = false;
            clsColumnInfo54.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo54.HeadText = "交易编号";
            clsColumnInfo54.ReadOnly = true;
            clsColumnInfo54.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo55.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo55.BackColor = System.Drawing.Color.White;
            clsColumnInfo55.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo55.ColumnIndex = 54;
            clsColumnInfo55.ColumnName = "varietycode";
            clsColumnInfo55.ColumnWidth = 0;
            clsColumnInfo55.Enabled = false;
            clsColumnInfo55.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo55.HeadText = "品种代码";
            clsColumnInfo55.ReadOnly = true;
            clsColumnInfo55.TextFont = new System.Drawing.Font("宋体", 10F);
            this.m_dgList.Columns.Add(clsColumnInfo1);
            this.m_dgList.Columns.Add(clsColumnInfo2);
            this.m_dgList.Columns.Add(clsColumnInfo3);
            this.m_dgList.Columns.Add(clsColumnInfo4);
            this.m_dgList.Columns.Add(clsColumnInfo5);
            this.m_dgList.Columns.Add(clsColumnInfo6);
            this.m_dgList.Columns.Add(clsColumnInfo7);
            this.m_dgList.Columns.Add(clsColumnInfo8);
            this.m_dgList.Columns.Add(clsColumnInfo9);
            this.m_dgList.Columns.Add(clsColumnInfo10);
            this.m_dgList.Columns.Add(clsColumnInfo11);
            this.m_dgList.Columns.Add(clsColumnInfo12);
            this.m_dgList.Columns.Add(clsColumnInfo13);
            this.m_dgList.Columns.Add(clsColumnInfo14);
            this.m_dgList.Columns.Add(clsColumnInfo15);
            this.m_dgList.Columns.Add(clsColumnInfo16);
            this.m_dgList.Columns.Add(clsColumnInfo17);
            this.m_dgList.Columns.Add(clsColumnInfo18);
            this.m_dgList.Columns.Add(clsColumnInfo19);
            this.m_dgList.Columns.Add(clsColumnInfo20);
            this.m_dgList.Columns.Add(clsColumnInfo21);
            this.m_dgList.Columns.Add(clsColumnInfo22);
            this.m_dgList.Columns.Add(clsColumnInfo23);
            this.m_dgList.Columns.Add(clsColumnInfo24);
            this.m_dgList.Columns.Add(clsColumnInfo25);
            this.m_dgList.Columns.Add(clsColumnInfo26);
            this.m_dgList.Columns.Add(clsColumnInfo27);
            this.m_dgList.Columns.Add(clsColumnInfo28);
            this.m_dgList.Columns.Add(clsColumnInfo29);
            this.m_dgList.Columns.Add(clsColumnInfo30);
            this.m_dgList.Columns.Add(clsColumnInfo31);
            this.m_dgList.Columns.Add(clsColumnInfo32);
            this.m_dgList.Columns.Add(clsColumnInfo33);
            this.m_dgList.Columns.Add(clsColumnInfo34);
            this.m_dgList.Columns.Add(clsColumnInfo35);
            this.m_dgList.Columns.Add(clsColumnInfo36);
            this.m_dgList.Columns.Add(clsColumnInfo37);
            this.m_dgList.Columns.Add(clsColumnInfo38);
            this.m_dgList.Columns.Add(clsColumnInfo39);
            this.m_dgList.Columns.Add(clsColumnInfo40);
            this.m_dgList.Columns.Add(clsColumnInfo41);
            this.m_dgList.Columns.Add(clsColumnInfo42);
            this.m_dgList.Columns.Add(clsColumnInfo43);
            this.m_dgList.Columns.Add(clsColumnInfo44);
            this.m_dgList.Columns.Add(clsColumnInfo45);
            this.m_dgList.Columns.Add(clsColumnInfo46);
            this.m_dgList.Columns.Add(clsColumnInfo47);
            this.m_dgList.Columns.Add(clsColumnInfo48);
            this.m_dgList.Columns.Add(clsColumnInfo49);
            this.m_dgList.Columns.Add(clsColumnInfo50);
            this.m_dgList.Columns.Add(clsColumnInfo51);
            this.m_dgList.Columns.Add(clsColumnInfo52);
            this.m_dgList.Columns.Add(clsColumnInfo53);
            this.m_dgList.Columns.Add(clsColumnInfo54);
            this.m_dgList.Columns.Add(clsColumnInfo55);
            this.m_dgList.ContextMenuStrip = this.m_cmsCopy;
            this.m_dgList.FullRowSelect = true;
            this.m_dgList.Location = new System.Drawing.Point(3, 10);
            this.m_dgList.MultiSelect = false;
            this.m_dgList.Name = "m_dgList";
            this.m_dgList.ReadOnly = true;
            this.m_dgList.RowHeadersVisible = false;
            this.m_dgList.RowHeaderWidth = 35;
            this.m_dgList.SelectedRowBackColor = System.Drawing.Color.Silver;
            this.m_dgList.SelectedRowForeColor = System.Drawing.Color.White;
            this.m_dgList.Size = new System.Drawing.Size(1010, 214);
            this.m_dgList.TabIndex = 300;
            this.m_dgList.m_evtCurrentCellChanged += new System.EventHandler(this.m_dgList_m_evtCurrentCellChanged);
            this.m_dgList.m_evtClickCell += new com.digitalwave.controls.datagrid.clsDGTextMouseClickEventHandler(this.m_dgList_m_evtClickCell);
            this.m_dgList.m_evtDoubleClickCell += new com.digitalwave.controls.datagrid.clsDGTextMouseClickEventHandler(this.m_dgList_m_evtDoubleClickCell);
            // 
            // m_cmsCopy
            // 
            this.m_cmsCopy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制新增ToolStripMenuItem});
            this.m_cmsCopy.Name = "m_cmsCopy";
            this.m_cmsCopy.Size = new System.Drawing.Size(149, 26);
            // 
            // 复制新增ToolStripMenuItem
            // 
            this.复制新增ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("复制新增ToolStripMenuItem.Image")));
            this.复制新增ToolStripMenuItem.Name = "复制新增ToolStripMenuItem";
            this.复制新增ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.复制新增ToolStripMenuItem.Text = "复制（新增）";
            this.复制新增ToolStripMenuItem.Click += new System.EventHandler(this.复制新增ToolStripMenuItem_Click);
            // 
            // gbFind
            // 
            this.gbFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFind.Controls.Add(this.btnChang);
            this.gbFind.Controls.Add(this.buttonXP4);
            this.gbFind.Controls.Add(this.btnNewAdd);
            this.gbFind.Location = new System.Drawing.Point(696, 391);
            this.gbFind.Name = "gbFind";
            this.gbFind.Size = new System.Drawing.Size(208, 56);
            this.gbFind.TabIndex = 40;
            this.gbFind.TabStop = false;
            // 
            // btnChang
            // 
            this.btnChang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnChang.DefaultScheme = true;
            this.btnChang.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnChang.Hint = "";
            this.btnChang.Location = new System.Drawing.Point(72, 16);
            this.btnChang.Name = "btnChang";
            this.btnChang.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnChang.Size = new System.Drawing.Size(64, 35);
            this.btnChang.TabIndex = 31;
            this.btnChang.Text = "新建(&N)";
            this.btnChang.Click += new System.EventHandler(this.btnChang_Click);
            // 
            // buttonXP4
            // 
            this.buttonXP4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.buttonXP4.DefaultScheme = true;
            this.buttonXP4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP4.Hint = "";
            this.buttonXP4.Location = new System.Drawing.Point(136, 16);
            this.buttonXP4.Name = "buttonXP4";
            this.buttonXP4.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP4.Size = new System.Drawing.Size(64, 35);
            this.buttonXP4.TabIndex = 104;
            this.buttonXP4.Text = "退出(&E)";
            this.buttonXP4.Click += new System.EventHandler(this.buttonXP4_Click);
            // 
            // btnNewAdd
            // 
            this.btnNewAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnNewAdd.DefaultScheme = true;
            this.btnNewAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNewAdd.Hint = "";
            this.btnNewAdd.Location = new System.Drawing.Point(8, 16);
            this.btnNewAdd.Name = "btnNewAdd";
            this.btnNewAdd.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnNewAdd.Size = new System.Drawing.Size(64, 35);
            this.btnNewAdd.TabIndex = 30;
            this.btnNewAdd.Text = "保存(&A)";
            this.btnNewAdd.Click += new System.EventHandler(this.btnNewAdd_Click);
            // 
            // m_tb
            // 
            this.m_tb.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.m_tb.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.Add,
            this.Del,
            this.Find,
            this.reNew,
            this.Re,
            this.Esc});
            this.m_tb.DropDownArrows = true;
            this.m_tb.Location = new System.Drawing.Point(0, 0);
            this.m_tb.Name = "m_tb";
            this.m_tb.ShowToolTips = true;
            this.m_tb.Size = new System.Drawing.Size(1016, 51);
            this.m_tb.TabIndex = 11;
            this.m_tb.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.m_tb_ButtonClick);
            // 
            // Add
            // 
            this.Add.Name = "Add";
            this.Add.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.Add.Tag = "";
            this.Add.Text = "新增";
            this.Add.ToolTipText = "新增";
            // 
            // Del
            // 
            this.Del.Name = "Del";
            this.Del.Tag = "";
            this.Del.Text = "删除";
            this.Del.ToolTipText = "删除";
            // 
            // Find
            // 
            this.Find.Name = "Find";
            this.Find.Text = "查找";
            this.Find.ToolTipText = "查找";
            // 
            // reNew
            // 
            this.reNew.Name = "reNew";
            this.reNew.Text = "刷新";
            this.reNew.ToolTipText = "刷新";
            // 
            // Re
            // 
            this.Re.DropDownMenu = this.m_Menu;
            this.Re.Name = "Re";
            this.Re.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.Re.Text = "维护";
            this.Re.ToolTipText = "维护";
            // 
            // Esc
            // 
            this.Esc.Name = "Esc";
            this.Esc.Text = "关闭";
            this.Esc.ToolTipText = "关闭";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmMedicine
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1016, 565);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_tb);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmMedicine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "药品基本信息";
            this.TransparencyKey = System.Drawing.Color.DarkRed;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMedicine_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMedicine_KeyDown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_dgList)).EndInit();
            this.m_cmsCopy.ResumeLayout(false);
            this.gbFind.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// 读取参数5033是否屏蔽控件
        /// </summary>
        internal string m_strEblControll = null;

        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.clsControlMedicine();
            objController.Set_GUI_Apperance(this);
        }
        internal string Readonly = "";
        public void ackMedTypeShow(string MedType, string StrReadonly)
        {
            Readonly = StrReadonly;
            ((clsControlMedicine)this.objController).m_lngLoad(MedType);
            if (StrReadonly == "0")
            {

                btnNewAdd.Enabled = false;
                btnChang.Enabled = false;
                btnDele1.Enabled = false;
                buttonXP1.Enabled = false;
            }
            this.Show();
        }

        /// <summary>
        /// 物资专用调用方法
        /// </summary>
        /// <param name="MedType"></param>
        /// <param name="StrReadonly"></param>
        /// <param name="p_strType"></param>
        public void ackMedTypeShowForMaterial(string MedType, string StrReadonly, string p_strType)
        {
            Readonly = StrReadonly;
            ((clsControlMedicine)this.objController).m_lngLoad(MedType);
            if (StrReadonly == "0")
            {

                btnNewAdd.Enabled = false;
                btnChang.Enabled = false;
                btnDele1.Enabled = false;
                buttonXP1.Enabled = false;
            }
            m_strType = p_strType;
            this.Show();
        }


        private void frmMedicine_Load(object sender, System.EventArgs e)
        {
            this.m_mthSetEnter2Tab(new System.Windows.Forms.Control[] { });
            m_intYear = ((clsControlMedicine)this.objController).GetStandardDate();
            m_strEblControll = this.objController.m_objComInfo.m_lonGetModuleInfo("5033");
        }

        private void m_tb_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            switch (this.m_tb.Buttons.IndexOf(e.Button))
            {
                case 0: //新增
                    ((clsControlMedicine)this.objController).m_SetItem(true);
                    break;
                case 1://删除
                    ((clsControlMedicine)this.objController).m_lngDelMedInfo();
                    break;
                case 2://查找
                    gbFind.Visible = true;
                    break;
                case 3:
                    //                    ((clsControlMedicine)this.objController).m_lngLoad();
                    break;
                case 5: //退出
                    this.Close();
                    break;

            }
        }

        private void m_lvMed_DoubleClick(object sender, System.EventArgs e)
        {
            if (this.IsReturn == true)//要返回值
            {
                this.Close();
            }
            else
                ((clsControlMedicine)this.objController).m_SetItem(false);
        }


        private void mu_MedAndUnit_Click(object sender, System.EventArgs e)
        {
            frmMedAndUnit frm = new frmMedAndUnit();
            frm.ShowDialog();
        }
        public void ShowMe(bool IsReturn, out string strID, out string strName)
        {
            this.IsReturn = IsReturn;
            this.ShowDialog();
            strID = this.ReturnID;
            strName = this.ReturnName;
        }

        private void mu_MedPrice_Click(object sender, System.EventArgs e)
        {
            frmMedPriceList frm = new frmMedPriceList();
            frm.ShowDialog();
        }

        private void mu_MedAndSto_Click(object sender, System.EventArgs e)
        {
            frmMedAndStorage frm = new frmMedAndStorage();
            frm.ShowDialog();
        }

        private void mu_MedLimit_Click(object sender, System.EventArgs e)
        {
            frmStorageLimitMgr frm = new frmStorageLimitMgr();
            frm.ShowDialog();
        }

        private void frmMedicine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (this.panel4.Visible == true)
                {
                    panel4.Visible = false;
                    return;
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (this.IsReturn == true)
                    this.Close();
                else
                {
                    if (MessageBox.Show("是否退出药品维护系统?", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        this.Close();
                }
            }
        }

        private void m_lvMed_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {

        }

        private void m_dgList_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
        {

        }

        private void m_btnFind_Click(object sender, System.EventArgs e)
        {
            ((clsControlMedicine)this.objController).m_lngFind();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            ((clsControlMedicine)this.objController).m_lngReture();
        }

        private void ComType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ((clsControlMedicine)this.objController).m_lngSeleType();
        }

        private void m_txtNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
            //if(e.KeyCode==Keys.Enter)
            //{
            //    clsDomainConrol_Medicne DomainMedicne=new clsDomainConrol_Medicne();
            //    DataTable dt=new DataTable();
            //    long lngRes=DomainMedicne.m_lngCheckIsUse(m_txtNo.Text.Trim(),out dt);
            //    if(lngRes==1&&dt.Rows.Count>0)
            //    {
            //        clsPublicParm publicClass=new clsPublicParm();
            //        publicClass.m_mthShowWarning(this.m_txtNo,"系统检测到'"+m_txtNo.Text.Trim()+"'助记码，已经被'"+dt.Rows[0]["MEDICINENAME_VCHR"].ToString()+"'药品使用，不可以再使用该助记码！");
            //        m_txtNo.Focus();
            //    }

            //}
        }

        private void m_txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            this.m_mthSetKeyTab(e);
        }

        private void m_txtEnName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_txtSpec_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_cboProduct_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
            if (e.KeyCode == Keys.Enter)
                m_cboPreType.Focus();
        }

        private void m_cboMedType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_cboPreType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_txtDosage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_CobDosageUnit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_txtTRADEPRICE_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_txtUNITPRICE_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_CobUnit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_CobIpUnit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_txtPackQty_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void mixuser_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void maxuser_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_txtInsuranceID_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_cmbIsInDocAdv_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void cbouse_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void cmbIsStop_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void cboOPCHARGEFLG_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void cboIPCHARGEFLG_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_chkIsAnaesthesia_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_chkIsChlorpromazine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_chkIsCostly_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_chkIsSelf_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_chkIsImport_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_chkIsSelfPay_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);

        }

        private void btnNewAdd_Click(object sender, System.EventArgs e)
        {
            ((clsControlMedicine)this.objController).m_lngSaveClick();
        }

        private void btnChang_Click(object sender, System.EventArgs e)
        {
            ((clsControlMedicine)this.objController).m_lngClearfrm();
        }

        private void buttonXP4_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnDele_Click(object sender, System.EventArgs e)
        {
            ((clsControlMedicine)this.objController).m_lngDelMedInfo();
        }

        private void txt_NMLDOSAGE_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void isSTANDARD_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);

        }

        private void panel2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }
        private void m_cobType1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_cobType2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_cobType3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_cobType4_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_txtAdul_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_txtChild_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
        }

        private void buttonXP1_Click(object sender, System.EventArgs e)
        {
            com.digitalwave.controls.UCMedFind ShowUcMed = new com.digitalwave.controls.UCMedFind();
            ShowUcMed.CurSTDID = (string)LableMed.Tag;
            if (ShowUcMed.ShowDialog() == DialogResult.OK)
            {
                LableMed.Text = ShowUcMed.CurSTDName;
                LableMed.Tag = ShowUcMed.CurSTDID;
                ((clsControlMedicine)this.objController).m_modify(ShowUcMed.CurSTDID, ShowUcMed.CurSTDName);
            }
        }

        private void buttonXP2_Click(object sender, System.EventArgs e)
        {
            frmSingleItemDiscount objfrm = new frmSingleItemDiscount();
            objfrm.Text += " ─" + this.m_txtName.Text.Trim();
            if (this.btnNewAdd.Tag == null || Convert.ToString(this.btnNewAdd.Tag).Length == 0)
            {
                objfrm.m_blnUseByAddingNewMedicine = true;
                if (m_dtbChargeItem != null)
                    objfrm.m_dtbChargeItem = m_dtbChargeItem.Copy();
                else
                    objfrm.strItemID = "";
                objfrm.OnConfirm_Handler += new OnConfirmHanlder(objfrm_OnConfirm_Handler);
            }
            else
            {
                objfrm.strItemID = this.btnNewAdd.Tag.ToString().Trim();
            }
            if (Readonly == "0")
            {
                objfrm.isEndle = false;
            }
            objfrm.ShowDialog();
        }

        void objfrm_OnConfirm_Handler(DataTable p_dtbResult)
        {
            m_dtbChargeItem = p_dtbResult.Copy();
        }

        private void ShowUcMed_OnSortSelected(object sender, EventArgs e)
        {


        }

        private void checkBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_dgList_m_evtClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
        {

        }

        private void m_dgList_m_evtCurrentCellChanged(object sender, System.EventArgs e)
        {
            ((clsControlMedicine)this.objController).m_lngFillEditFrm();
        }

        private void txtMEDNORMALNAME_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void txtMEDNORMALNAME_Leave(object sender, System.EventArgs e)
        {
            if (m_txtName.Text != "")
                ((clsControlMedicine)this.objController).m_lngGetpywb();
        }

        private void m_txtName_Leave(object sender, System.EventArgs e)
        {
            ((clsControlMedicine)this.objController).m_lngGetpywb();
        }

        private void btnDele1_Click(object sender, System.EventArgs e)
        {
            ((clsControlMedicine)this.objController).m_mthDeleMedByMedId();
        }

        private void ctlCARETYPE_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void ctlTextBoxFind1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            this.m_mthSetKeyTab(e);

        }

        private void ctlvendor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void textBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void checkBox2_CheckedChanged(object sender, System.EventArgs e)
        {
            ((clsControlMedicine)this.objController).m_mthShowMed();
        }

        private void panel3_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void buttonXP3_Click(object sender, System.EventArgs e)
        {
            if (this.btnNewAdd.Tag == null || this.btnNewAdd.Tag.ToString() == "")
            {
                return;
            }
            frmChangePriceInfo priceInfo = new frmChangePriceInfo();
            priceInfo.ItemID = this.btnNewAdd.Tag.ToString().Trim();
            priceInfo.ShowDialog();
        }

        private void m_cboMedType_KeyDown_1(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void cobSelectType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ((clsControlMedicine)this.objController).m_CobMedTypeSele();
        }

        private void buttonXP5_Click(object sender, System.EventArgs e)
        {
            ((clsControlMedicine)this.objController).m_showStorage();
        }

        private void exComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                if (((CheckBox)sender).Name != "m_chkIsAnaesthesia")
                    m_chkIsAnaesthesia.Checked = false;
                if (((CheckBox)sender).Name != "checkBox4")
                    checkBox4.Checked = false;
                if (((CheckBox)sender).Name != "m_chkIsChlorpromazine")
                    m_chkIsChlorpromazine.Checked = false;
                if (((CheckBox)sender).Name != "checkBox3")
                    checkBox3.Checked = false;
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void checkBox4_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void checkBox3_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void cboIPCHARGEFLG_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void m_CobDosageUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonXP7_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("助记码");
            dt.Columns.Add("通用名");
            dt.Columns.Add("药品名称");
            dt.Columns.Add("英文名");
            dt.Columns.Add("类别");
            dt.Columns.Add("摆药分类");
            dt.Columns.Add("制剂");
            dt.Columns.Add("规格");
            dt.Columns.Add("批发价");
            dt.Columns.Add("单价");
            dt.Columns.Add("国家限价");
            dt.Columns.Add("生产厂家");
            dt.Columns.Add("剂量");
            dt.Columns.Add("剂量单位");
            dt.Columns.Add("门诊单位");
            dt.Columns.Add("住院单位");
            dt.Columns.Add("包装量");
            dt.Columns.Add("最小用量");
            dt.Columns.Add("最大用量");
            dt.Columns.Add("常用量");
            dt.Columns.Add("药理分类");
            dt.Columns.Add("麻醉药品");
            dt.Columns.Add("毒性药品");
            dt.Columns.Add("精神一类");
            dt.Columns.Add("精神二类");
            dt.Columns.Add("贵重药品");
            dt.Columns.Add("院内制剂");
            dt.Columns.Add("进口药品");
            dt.Columns.Add("自费药品");
            dt.Columns.Add("中标药品");
            dt.Columns.Add("医保号码");
            dt.Columns.Add("是否停用");
            dt.Columns.Add("中标年份");
            DataTable tempdt = null;
            if ((string)m_dgList.Tag == "objResultArr")
            {
                tempdt = ((clsControlMedicine)this.objController).objResultArr;
            }
            else
            {
                tempdt = ((clsControlMedicine)this.objController).FidTable;
            }
            if (tempdt.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < tempdt.Rows.Count; i1++)
                {
                    DataRow row = dt.NewRow();
                    row["助记码"] = tempdt.Rows[i1]["ASSISTCODE_CHR"];
                    row["通用名"] = tempdt.Rows[i1]["MEDNORMALNAME_VCHR"];
                    row["药品名称"] = tempdt.Rows[i1]["MEDICINENAME_VCHR"];
                    row["英文名"] = tempdt.Rows[i1]["MEDICINEENGNAME_VCHR"];
                    row["类别"] = tempdt.Rows[i1]["MEDICINETYPENAME_VCHR"];
                    row["摆药分类"] = tempdt.Rows[i1]["PUTMEDTYPEName"];
                    row["制剂"] = tempdt.Rows[i1]["MEDICINEPREPTYPENAME_VCHR"];
                    row["规格"] = tempdt.Rows[i1]["MEDSPEC_VCHR"];
                    row["批发价"] = tempdt.Rows[i1]["TRADEPRICE_MNY"];
                    row["单价"] = tempdt.Rows[i1]["UNITPRICE_MNY"];
                    row["生产厂家"] = tempdt.Rows[i1]["PRODUCTORID_CHR"];
                    row["剂量"] = tempdt.Rows[i1]["DOSAGE_DEC"];
                    row["剂量单位"] = tempdt.Rows[i1]["DOSAGEUNIT_CHR"];
                    row["门诊单位"] = tempdt.Rows[i1]["OPUNIT_CHR"];
                    row["住院单位"] = tempdt.Rows[i1]["IPUNIT_CHR"];
                    row["包装量"] = tempdt.Rows[i1]["PACKQTY_DEC"];
                    row["最小用量"] = tempdt.Rows[i1]["MINDOSAGE_DEC"];
                    row["最大用量"] = tempdt.Rows[i1]["MAXDOSAGE_DEC"];
                    row["常用量"] = tempdt.Rows[i1]["NMLDOSAGE_DEC"];
                    row["药理分类"] = tempdt.Rows[i1]["pharmaname_vchr"];
                    row["麻醉药品"] = tempdt.Rows[i1]["ISANAESTHESIA"];
                    row["毒性药品"] = tempdt.Rows[i1]["ISPOISON"];
                    row["毒性药品"] = tempdt.Rows[i1]["ISPOISON"];
                    row["精神一类"] = tempdt.Rows[i1]["ISCHLORPROMAZIN"];
                    row["精神二类"] = tempdt.Rows[i1]["ISCHLORPROMAZINE2"];
                    row["贵重药品"] = tempdt.Rows[i1]["ISCOSTLY"];
                    row["院内制剂"] = tempdt.Rows[i1]["ISSELF"];
                    row["进口药品"] = tempdt.Rows[i1]["ISIMPORT"];
                    row["自费药品"] = tempdt.Rows[i1]["ISSELFPAY"];
                    row["中标药品"] = tempdt.Rows[i1]["isSTANDARD"];
                    row["医保号码"] = tempdt.Rows[i1]["INSURANCEID_VCHR"];
                    row["是否停用"] = tempdt.Rows[i1]["IFSTOP"];
                    row["国家限价"] = tempdt.Rows[i1]["LIMITUNITPRICE_MNY"];
                    row["中标年份"] = tempdt.Rows[i1]["STANDARDDATE"];
                    dt.Rows.Add(row);
                }
            }
            ((clsControlMedicine)this.objController).m_mthOutExcel(dt);
        }

        private void isStop_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void exComboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void checkBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.btnNewAdd.Focus();
        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void m_cboPUTMEDTYPE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                m_chkIsAnaesthesia.Focus();
        }

        private void m_cboCATE1_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_cboCATE1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void m_cboCATE1_KeyDown_1(object sender, KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_cboFindContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!this.m_cboFindContent.Items.Contains(this.m_cboFindContent.Text))
                {
                    this.m_cboFindContent.Items.Add(this.m_cboFindContent.Text);
                }
                this.m_btnFind.PerformClick();
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void m_cboFindContent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void m_txtTRADEPRICE_Leave(object sender, EventArgs e)
        {
            ((clsControlMedicine)this.objController).m_mthGetGrossprofitrate();
        }

        #region 获取最小单位零售价
        private void m_txtUNITPRICE_TextChanged(object sender, EventArgs e)
        {
            m_mthGetIPUnitprice();
        }

        private void m_txtPackQty_TextChanged(object sender, EventArgs e)
        {
            m_mthGetIPUnitprice();
        }

        /// <summary>
        /// 获取最小单位零售价
        /// </summary>
        /// <returns></returns>
        public void m_mthGetIPUnitprice()
        {
            double dblUnitPrice = 0d;
            double dblPackQty = 0d;
            if (double.TryParse(m_txtUNITPRICE.Text, out dblUnitPrice) && double.TryParse(m_txtPackQty.Text, out dblPackQty))
            {
                m_txtIpUnitPrice.Text = (dblUnitPrice / dblPackQty).ToString("0.0000");
                m_txtDiffPrice.Text = (dblUnitPrice - Convert.ToDouble(m_txtTRADEPRICE.Text)).ToString("0.0000");//Added by: 吴汉明 2014-12-9
            }
            else
            {
                m_txtIpUnitPrice.Text = "0";
            }
        }
        #endregion

        private void m_btnSetStandardYear_Click(object sender, EventArgs e)
        {
            if (this.btnNewAdd.Tag != null)
            {
                frmMedicineStandardYear frmMSY = new frmMedicineStandardYear((string)this.m_txtNo.Tag, m_intYear, m_strStandarddateReturn);
                frmMSY.OnReturnValue += new frmMedicineStandardYear.OnReturn(frmMSY_OnReturnValue);
                if (frmMSY.ShowDialog() == DialogResult.OK)
                {
                    if ((string)m_dgList.Tag == "objResultArr")
                    {
                        ((clsControlMedicine)this.objController).objResultArr.Rows[m_dgList.CurrentCell.RowNumber]["STANDARDDATE"] = m_strStandarddateReturn;
                    }
                    else
                    {
                        ((clsControlMedicine)this.objController).FidTable.Rows[m_dgList.CurrentCell.RowNumber]["STANDARDDATE"] = m_strStandarddateReturn;
                    }
                    ((clsControlMedicine)this.objController).objResultArr.AcceptChanges();
                    ((clsControlMedicine)this.objController).FidTable.AcceptChanges();
                }
            }
        }

        void frmMSY_OnReturnValue(string strYear)
        {
            m_strStandarddateReturn = strYear;
            m_dgList[m_dgList.CurrentCell.RowNumber, "STANDARDDATE"] = m_strStandarddateReturn;
        }

        private void 复制新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((clsControlMedicine)this.objController).m_lngFillEditFrm();
            ((clsControlMedicine)this.objController).isAddNew = 1;
            ((clsControlMedicine)this.objController).m_lngFillChargeItem(Convert.ToString(btnNewAdd.Tag), out m_dtbChargeItem);
            m_txtNo.Tag = "";
            btnNewAdd.Tag = "";
            //((clsControlMedicine)this.objController).m_mthControllEnabled();
            this.m_txtSpec.Enabled = true;
            this.m_cboCATE1.Enabled = true;
            this.m_cboPUTMEDTYPE.Enabled = true;
            this.cboIPCHARGEFLG.Enabled = true;
            this.cboOPCHARGEFLG.Enabled = true;
            this.m_txtPackQty.Enabled = true;
            this.m_txtUNITPRICE.Enabled = true;
        }

        private void label45_Click(object sender, EventArgs e)
        {


        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
