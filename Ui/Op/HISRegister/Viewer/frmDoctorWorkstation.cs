using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using com.digitalwave.iCare.middletier.HI;
using weCare.Core.Entity;
using iCare.CustomForm;
using System.Drawing.Printing;
using iCare;
using System.Reflection;
using System.Data;
using com.digitalwave.controls.DGCS;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// frmDoctorWorkstation 门诊医生工作站。
    /// </summary>
    public class frmDoctorWorkstation : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary> 
        /// 处理快捷键输入 true 取消,false输入
        /// </summary>
        internal bool HandleInput = false;
        /// <summary>
        /// 记录中文输入法
        /// </summary>
        private InputLanguage myInputLanguage = InputLanguage.DefaultInputLanguage;
        /// <summary>
        /// 是否开分门诊西药房和急诊药房，0为合并，1为分开
        /// </summary>
        public int IsDetachWMedStore = 0;
        #region 全局变量,保存datagrid输入数据时的行号,为了不回车也能计算金额,-1代表不计算
        public int intRowNo1 = -1;//西药
        public int intRowNo2 = -1;//中药
        public int intRowNo3 = -1;//检验
        public int intRowNo4 = -1;//检查
        public int intRowNo5 = -1;//手术
        public int intRowNo6 = -1;//其他
        public int intRowNoLis = -1;//检验诊疗项目
        public int intRowNoTest = -1;//检查诊疗项目
        public int intRowNoOps = -1;//手术治疗诊疗项目
        #endregion
        //		private clsTemplateClient m_objTemplate;
        internal System.Windows.Forms.Panel panel1;
        internal com.digitalwave.controls.ctlPatientBasicInfo m_PatInfo;
        internal Panel panel2;
        internal System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Splitter splitter1;
        internal System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        internal TabPage tabPage5;
        internal TabPage tabPage6;
        internal TabPage tabPage7;
        internal TabPage tabPage8;
        private System.Windows.Forms.Panel panel3;
        internal TabPage tabPage9;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.GroupBox groupBox1;
        internal com.digitalwave.controls.datagrid.ctlDataGrid ctlDataGrid1;
        internal com.digitalwave.controls.datagrid.ctlDataGrid ctlDataGrid2;
        internal com.digitalwave.controls.datagrid.ctlDataGrid ctlDataGrid3;
        internal com.digitalwave.controls.datagrid.ctlDataGrid ctlDataGrid4;
        internal com.digitalwave.controls.datagrid.ctlDataGrid ctlDataGrid5;
        internal com.digitalwave.controls.datagrid.ctlDataGrid ctlDataGrid6;
        internal System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label21;
        internal System.Windows.Forms.Label lbeSumMoney;
        internal com.digitalwave.controls.ctlCooking cmbCooking;
        internal PinkieControls.ButtonXP BtExit;
        internal PinkieControls.ButtonXP btClear;
        internal PinkieControls.ButtonXP btSave;
        internal System.Windows.Forms.ComboBox m_cmbFind;
        internal System.Windows.Forms.ComboBox m_cmbRecipeType;
        internal System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        internal System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        internal PinkieControls.ButtonXP btPrint;
        internal PinkieControls.ButtonXP btDel;
        internal com.digitalwave.controls.datagrid.ctlDataGrid m_dtgWaitReg;
        internal com.digitalwave.controls.datagrid.ctlDataGrid m_dtgTake;
        internal System.Windows.Forms.Label lbeChargeUp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblIsVip;
        internal System.Windows.Forms.Label lbeSelfPay;
        private System.Windows.Forms.Label label23;
        internal PinkieControls.ButtonXP m_btnEndTake;
        internal PinkieControls.ButtonXP m_btnBackWait;
        internal PinkieControls.ButtonXP m_btnRefReg;
        internal System.Windows.Forms.ComboBox cmbFindAccordRecipe;
        internal System.Windows.Forms.TextBox txtFindAccordRecipe;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        internal System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        internal com.digitalwave.controls.AlertLight alertLight1;
        private System.Windows.Forms.GroupBox groupBox6;
        internal System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btAssistantDiagnose;
        private System.Windows.Forms.ColumnHeader columnHeader24;
        internal PinkieControls.ButtonXP btReUse;
        internal com.digitalwave.controls.DGCS.txtLoadRecipeNo txtLoadRecipeNo1;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Label FF;
        internal System.Windows.Forms.Label EE;
        internal System.Windows.Forms.Label DD;
        internal System.Windows.Forms.Label CC;
        internal System.Windows.Forms.Label BB;
        internal System.Windows.Forms.Label AA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label26;
        internal System.Windows.Forms.Label lbeTimes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        internal PinkieControls.ButtonXP btCaseyHistory;
        internal System.Windows.Forms.ListView listView4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        internal PinkieControls.ButtonXP btInject;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ColumnHeader columnHeader20;
        internal PinkieControls.ButtonXP btNew;
        internal PinkieControls.ButtonXP btReGroup;
        internal frmCaseHistory objCaseHistory;
        internal PinkieControls.ButtonXP btCaseHistory;
        internal PinkieControls.ButtonXP btPutIn;
        internal System.Windows.Forms.ComboBox cmbRecipeType;
        internal System.Windows.Forms.Label lbeFlag;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ToolTip toolTip1;
        internal com.digitalwave.iCare.gui.HIS.exComboBox cmbDeparment;
        internal System.Windows.Forms.RadioButton ra_Person;
        internal System.Windows.Forms.RadioButton ra_department;
        internal System.Windows.Forms.RadioButton ra_both;
        internal PinkieControls.ButtonXP btSelect;
        internal System.Windows.Forms.ListView listView5;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.ColumnHeader columnHeader22;
        private System.Windows.Forms.ColumnHeader columnHeader23;
        internal System.Windows.Forms.Label lblFunction;
        internal System.Windows.Forms.ContextMenu ctmDel;
        private System.Windows.Forms.MenuItem mniDelAll;
        private System.Windows.Forms.MenuItem mniDelCash;
        private System.Windows.Forms.MenuItem muiDelRec;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ColumnHeader columnHeader25;
        private System.ComponentModel.IContainer components;
        private clsDcl_DoctorWorkstation objDclDoctor;

        #region 列参
        private int intURGENCY_INT = 0;

        #endregion

        internal System.Windows.Forms.ContextMenu ctmPat;
        internal System.Windows.Forms.MenuItem muiInfo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.LinkLabel llblDate;
        private System.Windows.Forms.Label lblToday;
        internal PinkieControls.ButtonXP btnSelect;
        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.DateTimePicker dtpEnd;
        internal System.Windows.Forms.DateTimePicker dtpBegin;
        internal System.Windows.Forms.Label lblEnd;
        internal System.Windows.Forms.Label lblBegin;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label lblPersonTimes;

        private int intMedDays = 0;
        private System.Windows.Forms.Timer timer;
        internal frmAllergichint frmAllergich;
        internal PictureBox picops;
        internal PictureBox picris;
        internal PictureBox piclis;
        internal Label label13;
        internal Label label12;
        internal Label label11;
        private Timer timerrec;
        internal frmAllergiclist frmAllergicl;
        internal ComboBox cboquick;
        private ColumnHeader columnHeader26;
        internal PinkieControls.ButtonXP btnRecalc;
        internal ComboBox cboDeptmed1;
        internal ComboBox cboDeptmed2;
        internal ComboBox cboDeptmed6;
        private Timer timerRecpur;
        private Label label16;
        private Label label15;
        private Label label14;
        private GroupBox groupBox3;
        private ColumnHeader columnHeader27;
        internal frmRecipeconfirmfalllist frmRecconf;
        internal com.digitalwave.controls.datagrid.ctlDataGrid ctlDataGridLis;
        internal Panel plLis;
        private Label label17;
        private PictureBox pictureBox2;
        internal ListView lvLis;
        private ColumnHeader columnHeader28;
        private ColumnHeader columnHeader29;
        private ColumnHeader columnHeader30;
        private ColumnHeader columnHeader31;
        private ColumnHeader columnHeader32;
        private Label label18;
        private Panel panel5;
        private Panel panel6;
        internal com.digitalwave.controls.datagrid.ctlDataGrid ctlDataGridTest;
        internal com.digitalwave.controls.datagrid.ctlDataGrid ctlDataGridOps;
        internal Panel panel7;
        private Panel panel8;
        private Label label19;
        private ColumnHeader columnHeader33;
        private ColumnHeader columnHeader34;
        private ColumnHeader columnHeader35;
        private ColumnHeader columnHeader36;
        private ColumnHeader columnHeader37;
        internal Panel panel9;
        private Panel panel10;
        private Label label25;
        internal Panel plTest;
        private Panel panel12;
        private Label label20;
        internal ListView lvTest;
        private ColumnHeader columnHeader43;
        private ColumnHeader columnHeader44;
        private ColumnHeader columnHeader45;
        private ColumnHeader columnHeader46;
        private ColumnHeader columnHeader47;
        private Panel panel13;
        private Label label28;
        private PictureBox pictureBox3;
        internal Panel plOps;
        private Panel panel14;
        private Label label27;
        internal ListView lvOps;
        private ColumnHeader columnHeader38;
        private ColumnHeader columnHeader39;
        private ColumnHeader columnHeader40;
        private ColumnHeader columnHeader41;
        private ColumnHeader columnHeader42;
        private Panel panel15;
        private Label label29;
        private PictureBox pictureBox4;
        private ColumnHeader columnHeader48;
        private ColumnHeader columnHeader49;
        private ColumnHeader columnHeader50;
        private ColumnHeader columnHeader51;
        private ColumnHeader columnHeader52;
        private ColumnHeader columnHeader53;
        private DiagnoseClient.frmDiagClientMain frmDiag = null;
        private Label label30;
        internal Label lbeSbAccPay;
        internal PinkieControls.ButtonXP btnPrefer;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem tsmiDrugInfo;
        private Label label31;
        internal TextBox txtWeight;
        internal ComboBox cboProxyBoilMed;
        private Label label32;
        internal PinkieControls.ButtonXP btnBooking;
        internal PinkieControls.ButtonXP btnWAC;
        internal PinkieControls.ButtonXP btnNotice;
        internal PinkieControls.ButtonXP btnApp;
        private Label lblESBCardVerify;
        internal DevExpress.XtraEditors.RadioGroup rdoZzsq;
        private Label label33;
        private bool DelRecGroupFlag = false;

        public frmDoctorWorkstation()
        {

            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();
            this.m_PatInfo.HotKey = "F2";

            this.tabControl1_SelectedIndexChanged(null, null);
            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
            objCaseHistory = new frmCaseHistory();
            objCaseHistory.TopLevel = false;
            this.groupBox1.Controls.Add(objCaseHistory);
            objCaseHistory.Dock = DockStyle.Fill;
            objCaseHistory.Show();
            objCaseHistory.BringToFront();

            objDclDoctor = new clsDcl_DoctorWorkstation();
            frmAllergich = new frmAllergichint();
            frmAllergicl = new frmAllergiclist();
            frmRecconf = new frmRecipeconfirmfalllist();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDoctorWorkstation));
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo636 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo637 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo638 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo639 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo640 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo641 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo642 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo643 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo644 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo645 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo646 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo647 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo648 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo649 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo650 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo651 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo652 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo653 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo654 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo655 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo656 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo657 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo464 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo465 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo466 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo467 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo468 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo469 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo470 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo471 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo472 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo473 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo474 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo658 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo659 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo660 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo661 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo662 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo663 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo664 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo665 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo666 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo667 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo668 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo669 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo670 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo671 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo672 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo673 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo674 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo675 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo676 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo677 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo678 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo679 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo680 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo681 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo682 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo683 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo684 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo685 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo686 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo687 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo688 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo281 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo282 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo283 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo284 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo285 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo286 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo287 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo288 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo289 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo290 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo291 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo475 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo476 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo477 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo478 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo479 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo480 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo481 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo482 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo483 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo484 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo485 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo486 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo487 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo488 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo489 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo490 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo491 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo492 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo493 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo494 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo495 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo496 = new com.digitalwave.controls.datagrid.clsColumnInfo();
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
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo292 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo293 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo294 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo295 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo296 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo297 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo298 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo299 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo300 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo301 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo302 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo303 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo304 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo305 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo306 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo307 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo308 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo309 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo310 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo311 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo312 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo313 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo497 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo498 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo499 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo500 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo501 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo502 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo503 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo504 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo505 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo689 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo690 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo691 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo692 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo693 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo694 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo695 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo696 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo697 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo698 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo699 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo700 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo701 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo702 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo703 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo704 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo705 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo706 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo707 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo708 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo709 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo710 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo711 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo712 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo713 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo714 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo715 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo716 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo717 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo718 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo719 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo720 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo721 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo722 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo723 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo724 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo725 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo726 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo727 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo728 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo729 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo730 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo731 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo732 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo733 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo734 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo735 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo736 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo737 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo738 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo739 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo740 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo741 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo742 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo743 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo744 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo745 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo746 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo747 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo748 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo749 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo750 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo751 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo752 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo753 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo754 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo755 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo756 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo757 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo758 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo759 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo760 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo761 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo762 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo763 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo764 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo765 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo766 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo767 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo768 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo769 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo770 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo771 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo772 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo773 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo774 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo775 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo776 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo777 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo778 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo779 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo780 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo781 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo782 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo783 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo784 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo785 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo786 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo787 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo788 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo789 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo790 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo791 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo792 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo793 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo794 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo795 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo796 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo797 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo798 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo799 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo800 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo801 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo802 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo803 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo804 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo805 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo806 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo807 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo808 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo809 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo810 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo811 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo812 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo813 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo814 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo815 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo816 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo817 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            com.digitalwave.controls.datagrid.clsColumnInfo clsColumnInfo818 = new com.digitalwave.controls.datagrid.clsColumnInfo();
            this.ctmPat = new System.Windows.Forms.ContextMenu();
            this.muiInfo = new System.Windows.Forms.MenuItem();
            this.cboquick = new System.Windows.Forms.ComboBox();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmbDeparment = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.ra_both = new System.Windows.Forms.RadioButton();
            this.ra_department = new System.Windows.Forms.RadioButton();
            this.ra_Person = new System.Windows.Forms.RadioButton();
            this.cmbCooking = new com.digitalwave.controls.ctlCooking();
            this.txtLoadRecipeNo1 = new com.digitalwave.controls.DGCS.txtLoadRecipeNo();
            this.m_cmbFind = new System.Windows.Forms.ComboBox();
            this.btnRecalc = new PinkieControls.ButtonXP();
            this.picops = new System.Windows.Forms.PictureBox();
            this.picris = new System.Windows.Forms.PictureBox();
            this.piclis = new System.Windows.Forms.PictureBox();
            this.m_cmbRecipeType = new System.Windows.Forms.ComboBox();
            this.ctmDel = new System.Windows.Forms.ContextMenu();
            this.mniDelAll = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.mniDelCash = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.muiDelRec = new System.Windows.Forms.MenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timerrec = new System.Windows.Forms.Timer(this.components);
            this.columnHeader33 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader34 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader35 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader36 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader37 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cboDeptmed1 = new System.Windows.Forms.ComboBox();
            this.cboDeptmed2 = new System.Windows.Forms.ComboBox();
            this.cboDeptmed6 = new System.Windows.Forms.ComboBox();
            this.timerRecpur = new System.Windows.Forms.Timer(this.components);
            this.label26 = new System.Windows.Forms.Label();
            this.lbeTimes = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.alertLight1 = new com.digitalwave.controls.AlertLight();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_dtgWaitReg = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btSelect = new PinkieControls.ButtonXP();
            this.m_btnRefReg = new PinkieControls.ButtonXP();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_dtgTake = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblPersonTimes = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSelect = new PinkieControls.ButtonXP();
            this.lblToday = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblBegin = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.llblDate = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btAssistantDiagnose = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.listView3 = new System.Windows.Forms.ListView();
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctlDataGrid1 = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDrugInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listView4 = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctlDataGrid2 = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.listView5 = new System.Windows.Forms.ListView();
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctlDataGridLis = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.ctlDataGrid3 = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.plLis = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.lvLis = new System.Windows.Forms.ListView();
            this.columnHeader28 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader29 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader30 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader31 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader32 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader48 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader51 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel6 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.plTest = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.lvTest = new System.Windows.Forms.ListView();
            this.columnHeader43 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader44 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader45 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader46 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader47 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader49 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader52 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel13 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.ctlDataGridTest = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.ctlDataGrid4 = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.plOps = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.lvOps = new System.Windows.Forms.ListView();
            this.columnHeader38 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader39 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader40 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader41 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader42 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader50 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader53 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel15 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.ctlDataGridOps = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.ctlDataGrid5 = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.ctlDataGrid6 = new com.digitalwave.controls.datagrid.ctlDataGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.FF = new System.Windows.Forms.Label();
            this.EE = new System.Windows.Forms.Label();
            this.DD = new System.Windows.Forms.Label();
            this.CC = new System.Windows.Forms.Label();
            this.BB = new System.Windows.Forms.Label();
            this.AA = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoZzsq = new DevExpress.XtraEditors.RadioGroup();
            this.label33 = new System.Windows.Forms.Label();
            this.lblESBCardVerify = new System.Windows.Forms.Label();
            this.cboProxyBoilMed = new System.Windows.Forms.ComboBox();
            this.cmbRecipeType = new System.Windows.Forms.ComboBox();
            this.btnApp = new PinkieControls.ButtonXP();
            this.btnNotice = new PinkieControls.ButtonXP();
            this.btnWAC = new PinkieControls.ButtonXP();
            this.btnBooking = new PinkieControls.ButtonXP();
            this.label32 = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.btnPrefer = new PinkieControls.ButtonXP();
            this.lbeSbAccPay = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbeFlag = new System.Windows.Forms.Label();
            this.btPutIn = new PinkieControls.ButtonXP();
            this.btCaseHistory = new PinkieControls.ButtonXP();
            this.btReGroup = new PinkieControls.ButtonXP();
            this.btNew = new PinkieControls.ButtonXP();
            this.btInject = new PinkieControls.ButtonXP();
            this.btCaseyHistory = new PinkieControls.ButtonXP();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbeSelfPay = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lbeChargeUp = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.BtExit = new PinkieControls.ButtonXP();
            this.btClear = new PinkieControls.ButtonXP();
            this.lbeSumMoney = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.btSave = new PinkieControls.ButtonXP();
            this.m_btnEndTake = new PinkieControls.ButtonXP();
            this.btPrint = new PinkieControls.ButtonXP();
            this.m_btnBackWait = new PinkieControls.ButtonXP();
            this.btDel = new PinkieControls.ButtonXP();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtFindAccordRecipe = new System.Windows.Forms.TextBox();
            this.cmbFindAccordRecipe = new System.Windows.Forms.ComboBox();
            this.btReUse = new PinkieControls.ButtonXP();
            this.lblIsVip = new System.Windows.Forms.Label();
            this.lblFunction = new System.Windows.Forms.Label();
            this.m_PatInfo = new com.digitalwave.controls.ctlPatientBasicInfo();
            ((System.ComponentModel.ISupportInitialize)(this.picops)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picris)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.piclis)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgWaitReg)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgTake)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid2)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGridLis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid3)).BeginInit();
            this.plLis.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPage8.SuspendLayout();
            this.plTest.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGridTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid4)).BeginInit();
            this.tabPage9.SuspendLayout();
            this.plOps.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGridOps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid5)).BeginInit();
            this.tabPage10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid6)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoZzsq.Properties)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctmPat
            // 
            this.ctmPat.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.muiInfo});
            // 
            // muiInfo
            // 
            this.muiInfo.Index = 0;
            this.muiInfo.Text = "既往就诊资料";
            this.muiInfo.Click += new System.EventHandler(this.muiInfo_Click);
            // 
            // cboquick
            // 
            this.cboquick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboquick.Font = new System.Drawing.Font("宋体", 11F);
            this.cboquick.FormattingEnabled = true;
            this.cboquick.Items.AddRange(new object[] {
            "",
            "是"});
            this.cboquick.Location = new System.Drawing.Point(764, 52);
            this.cboquick.Name = "cboquick";
            this.cboquick.Size = new System.Drawing.Size(56, 23);
            this.cboquick.TabIndex = 37;
            this.cboquick.SelectedIndexChanged += new System.EventHandler(this.cboquick_SelectedIndexChanged);
            this.cboquick.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboquick_KeyDown);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_EndPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // cmbDeparment
            // 
            this.cmbDeparment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeparment.Enabled = false;
            this.cmbDeparment.Location = new System.Drawing.Point(288, 17);
            this.cmbDeparment.Name = "cmbDeparment";
            this.cmbDeparment.Size = new System.Drawing.Size(192, 22);
            this.cmbDeparment.TabIndex = 24;
            this.toolTip1.SetToolTip(this.cmbDeparment, "选择科室后可直接回车");
            this.cmbDeparment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDeparment_KeyDown);
            // 
            // ra_both
            // 
            this.ra_both.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ra_both.Location = new System.Drawing.Point(176, 18);
            this.ra_both.Name = "ra_both";
            this.ra_both.Size = new System.Drawing.Size(96, 24);
            this.ra_both.TabIndex = 2;
            this.ra_both.Text = "个人和科室";
            this.toolTip1.SetToolTip(this.ra_both, "只显示挂号到当前登陆医生和科室的病人");
            this.ra_both.CheckedChanged += new System.EventHandler(this.ra_Person_CheckedChanged);
            // 
            // ra_department
            // 
            this.ra_department.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ra_department.Location = new System.Drawing.Point(104, 18);
            this.ra_department.Name = "ra_department";
            this.ra_department.Size = new System.Drawing.Size(88, 24);
            this.ra_department.TabIndex = 1;
            this.ra_department.Text = "科室";
            this.toolTip1.SetToolTip(this.ra_department, "显示挂号到当前登陆医生所属科室的病人");
            this.ra_department.CheckedChanged += new System.EventHandler(this.ra_Person_CheckedChanged);
            // 
            // ra_Person
            // 
            this.ra_Person.Checked = true;
            this.ra_Person.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ra_Person.Location = new System.Drawing.Point(32, 18);
            this.ra_Person.Name = "ra_Person";
            this.ra_Person.Size = new System.Drawing.Size(72, 24);
            this.ra_Person.TabIndex = 0;
            this.ra_Person.TabStop = true;
            this.ra_Person.Text = "个人";
            this.toolTip1.SetToolTip(this.ra_Person, "只显示挂号到当前登陆医生的病人");
            this.ra_Person.CheckedChanged += new System.EventHandler(this.ra_Person_CheckedChanged);
            // 
            // cmbCooking
            // 
            this.cmbCooking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCooking.CookID = "";
            this.cmbCooking.CookName = "";
            this.cmbCooking.Location = new System.Drawing.Point(47, 378);
            this.cmbCooking.MaxLength = 200;
            this.cmbCooking.Name = "cmbCooking";
            this.cmbCooking.Size = new System.Drawing.Size(665, 22);
            this.cmbCooking.TabIndex = 0;
            this.toolTip1.SetToolTip(this.cmbCooking, "中药整剂用法");
            this.cmbCooking.SelectedIndexChanged += new System.EventHandler(this.cmbCooking_SelectedIndexChanged);
            // 
            // txtLoadRecipeNo1
            // 
            this.txtLoadRecipeNo1.Location = new System.Drawing.Point(9, 53);
            this.txtLoadRecipeNo1.Name = "txtLoadRecipeNo1";
            this.txtLoadRecipeNo1.Size = new System.Drawing.Size(152, 25);
            this.txtLoadRecipeNo1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.txtLoadRecipeNo1, "输入处方号或直接回车选择处方号");
            this.txtLoadRecipeNo1.RecipeSelected += new System.EventHandler(this.txtLoadRecipeNo1_RecipeSelected);
            // 
            // m_cmbFind
            // 
            this.m_cmbFind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbFind.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_cmbFind.Items.AddRange(new object[] {
            "编号",
            "项目名称",
            "拼音码",
            "五笔码",
            "英文名"});
            this.m_cmbFind.Location = new System.Drawing.Point(75, 3);
            this.m_cmbFind.Name = "m_cmbFind";
            this.m_cmbFind.Size = new System.Drawing.Size(86, 22);
            this.m_cmbFind.TabIndex = 0;
            this.toolTip1.SetToolTip(this.m_cmbFind, "查询项目条件");
            this.m_cmbFind.SelectedIndexChanged += new System.EventHandler(this.m_cmbFind_SelectedIndexChanged);
            // 
            // btnRecalc
            // 
            this.btnRecalc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnRecalc.DefaultScheme = true;
            this.btnRecalc.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRecalc.Hint = "";
            this.btnRecalc.Location = new System.Drawing.Point(5, 427);
            this.btnRecalc.Name = "btnRecalc";
            this.btnRecalc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnRecalc.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnRecalc.Size = new System.Drawing.Size(76, 32);
            this.btnRecalc.TabIndex = 47;
            this.btnRecalc.Text = "重算(&C)";
            this.toolTip1.SetToolTip(this.btnRecalc, "重新计算检验项目带出的子项目费用");
            this.btnRecalc.Click += new System.EventHandler(this.btnRecalc_Click);
            // 
            // picops
            // 
            this.picops.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picops.Image = ((System.Drawing.Image)(resources.GetObject("picops.Image")));
            this.picops.Location = new System.Drawing.Point(104, 21);
            this.picops.Name = "picops";
            this.picops.Size = new System.Drawing.Size(30, 30);
            this.picops.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picops.TabIndex = 6;
            this.picops.TabStop = false;
            this.toolTip1.SetToolTip(this.picops, "手术申请单");
            this.picops.Visible = false;
            this.picops.Click += new System.EventHandler(this.picops_Click);
            this.picops.MouseEnter += new System.EventHandler(this.picops_MouseEnter);
            this.picops.MouseLeave += new System.EventHandler(this.picops_MouseLeave);
            // 
            // picris
            // 
            this.picris.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picris.Image = ((System.Drawing.Image)(resources.GetObject("picris.Image")));
            this.picris.Location = new System.Drawing.Point(58, 21);
            this.picris.Name = "picris";
            this.picris.Size = new System.Drawing.Size(30, 30);
            this.picris.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picris.TabIndex = 5;
            this.picris.TabStop = false;
            this.toolTip1.SetToolTip(this.picris, "检查申请单");
            this.picris.Visible = false;
            this.picris.Click += new System.EventHandler(this.picris_Click);
            this.picris.MouseEnter += new System.EventHandler(this.picris_MouseEnter);
            this.picris.MouseLeave += new System.EventHandler(this.picris_MouseLeave);
            // 
            // piclis
            // 
            this.piclis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.piclis.Image = ((System.Drawing.Image)(resources.GetObject("piclis.Image")));
            this.piclis.Location = new System.Drawing.Point(12, 21);
            this.piclis.Name = "piclis";
            this.piclis.Size = new System.Drawing.Size(30, 30);
            this.piclis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.piclis.TabIndex = 4;
            this.piclis.TabStop = false;
            this.toolTip1.SetToolTip(this.piclis, "检验申请单");
            this.piclis.Visible = false;
            this.piclis.Click += new System.EventHandler(this.piclis_Click);
            this.piclis.MouseEnter += new System.EventHandler(this.piclis_MouseEnter);
            this.piclis.MouseLeave += new System.EventHandler(this.piclis_MouseLeave);
            // 
            // m_cmbRecipeType
            // 
            this.m_cmbRecipeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbRecipeType.Items.AddRange(new object[] {
            "正方",
            "副方"});
            this.m_cmbRecipeType.Location = new System.Drawing.Point(91, 85);
            this.m_cmbRecipeType.Name = "m_cmbRecipeType";
            this.m_cmbRecipeType.Size = new System.Drawing.Size(60, 22);
            this.m_cmbRecipeType.TabIndex = 1;
            this.toolTip1.SetToolTip(this.m_cmbRecipeType, "选择处方的类型");
            this.m_cmbRecipeType.Visible = false;
            this.m_cmbRecipeType.SelectedIndexChanged += new System.EventHandler(this.m_cmbRecipeType_SelectedIndexChanged);
            // 
            // ctmDel
            // 
            this.ctmDel.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniDelAll,
            this.menuItem4,
            this.mniDelCash,
            this.menuItem5,
            this.muiDelRec});
            // 
            // mniDelAll
            // 
            this.mniDelAll.Index = 0;
            this.mniDelAll.Text = "删除病历和处方";
            this.mniDelAll.Click += new System.EventHandler(this.mniDelAll_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "-";
            // 
            // mniDelCash
            // 
            this.mniDelCash.Index = 2;
            this.mniDelCash.Text = "删除病历";
            this.mniDelCash.Click += new System.EventHandler(this.mniDelCash_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 3;
            this.menuItem5.Text = "-";
            // 
            // muiDelRec
            // 
            this.muiDelRec.Index = 4;
            this.muiDelRec.Text = "删除处方";
            this.muiDelRec.Click += new System.EventHandler(this.muiDelRec_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timerrec
            // 
            this.timerrec.Tick += new System.EventHandler(this.timerrec_Tick);
            // 
            // columnHeader33
            // 
            this.columnHeader33.Width = 28;
            // 
            // columnHeader34
            // 
            this.columnHeader34.Width = 137;
            // 
            // columnHeader35
            // 
            this.columnHeader35.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader35.Width = 54;
            // 
            // columnHeader36
            // 
            this.columnHeader36.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader36.Width = 55;
            // 
            // columnHeader37
            // 
            this.columnHeader37.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader37.Width = 64;
            // 
            // cboDeptmed1
            // 
            this.cboDeptmed1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeptmed1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.cboDeptmed1.FormattingEnabled = true;
            this.cboDeptmed1.Items.AddRange(new object[] {
            "",
            "符合",
            "不符合"});
            this.cboDeptmed1.Location = new System.Drawing.Point(604, 168);
            this.cboDeptmed1.Name = "cboDeptmed1";
            this.cboDeptmed1.Size = new System.Drawing.Size(56, 22);
            this.cboDeptmed1.TabIndex = 36;
            this.cboDeptmed1.SelectedIndexChanged += new System.EventHandler(this.cboDeptmed1_SelectedIndexChanged);
            this.cboDeptmed1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboDeptmed1_KeyDown);
            // 
            // cboDeptmed2
            // 
            this.cboDeptmed2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeptmed2.Font = new System.Drawing.Font("宋体", 11F);
            this.cboDeptmed2.FormattingEnabled = true;
            this.cboDeptmed2.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cboDeptmed2.Location = new System.Drawing.Point(432, 256);
            this.cboDeptmed2.Name = "cboDeptmed2";
            this.cboDeptmed2.Size = new System.Drawing.Size(56, 23);
            this.cboDeptmed2.TabIndex = 38;
            this.cboDeptmed2.SelectedIndexChanged += new System.EventHandler(this.cboDeptmed2_SelectedIndexChanged);
            this.cboDeptmed2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboDeptmed2_KeyDown);
            // 
            // cboDeptmed6
            // 
            this.cboDeptmed6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeptmed6.Font = new System.Drawing.Font("宋体", 11F);
            this.cboDeptmed6.FormattingEnabled = true;
            this.cboDeptmed6.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cboDeptmed6.Location = new System.Drawing.Point(336, 172);
            this.cboDeptmed6.Name = "cboDeptmed6";
            this.cboDeptmed6.Size = new System.Drawing.Size(56, 23);
            this.cboDeptmed6.TabIndex = 36;
            this.cboDeptmed6.SelectedIndexChanged += new System.EventHandler(this.cboDeptmed6_SelectedIndexChanged);
            this.cboDeptmed6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboDeptmed6_KeyDown);
            // 
            // timerRecpur
            // 
            this.timerRecpur.Tick += new System.EventHandler(this.timerRecpur_Tick);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(787, 101);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(41, 12);
            this.label26.TabIndex = 32;
            this.label26.Text = "次就诊";
            // 
            // lbeTimes
            // 
            this.lbeTimes.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.lbeTimes.ForeColor = System.Drawing.Color.Black;
            this.lbeTimes.Location = new System.Drawing.Point(770, 100);
            this.lbeTimes.Name = "lbeTimes";
            this.lbeTimes.Size = new System.Drawing.Size(18, 14);
            this.lbeTimes.TabIndex = 31;
            this.lbeTimes.Text = "1";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(752, 101);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(17, 12);
            this.label24.TabIndex = 30;
            this.label24.Text = "第";
            // 
            // alertLight1
            // 
            this.alertLight1.IsTabu = true;
            this.alertLight1.Location = new System.Drawing.Point(704, 97);
            this.alertLight1.Name = "alertLight1";
            this.alertLight1.Size = new System.Drawing.Size(32, 16);
            this.alertLight1.TabIndex = 26;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Location = new System.Drawing.Point(0, 88);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 503);
            this.panel2.TabIndex = 25;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.tabControl1.Location = new System.Drawing.Point(0, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(850, 440);
            this.tabControl1.TabIndex = 34;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_dtgWaitReg);
            this.tabPage1.Controls.Add(this.groupBox8);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(842, 409);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "[0]候诊";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // m_dtgWaitReg
            // 
            this.m_dtgWaitReg.AllowAddNew = false;
            this.m_dtgWaitReg.AllowDelete = false;
            this.m_dtgWaitReg.AutoAppendRow = false;
            this.m_dtgWaitReg.AutoScroll = true;
            this.m_dtgWaitReg.BackgroundColor = System.Drawing.SystemColors.Window;
            this.m_dtgWaitReg.CaptionText = "";
            this.m_dtgWaitReg.CaptionVisible = false;
            this.m_dtgWaitReg.ColumnHeadersVisible = true;
            clsColumnInfo636.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo636.BackColor = System.Drawing.Color.White;
            clsColumnInfo636.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo636.ColumnIndex = 0;
            clsColumnInfo636.ColumnName = "WAITDIAGLISTID_CHR";
            clsColumnInfo636.ColumnWidth = 0;
            clsColumnInfo636.Enabled = false;
            clsColumnInfo636.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo636.HeadText = "候诊ID";
            clsColumnInfo636.ReadOnly = true;
            clsColumnInfo636.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo637.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo637.BackColor = System.Drawing.Color.White;
            clsColumnInfo637.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo637.ColumnIndex = 1;
            clsColumnInfo637.ColumnName = "REGISTERNO_CHR";
            clsColumnInfo637.ColumnWidth = 100;
            clsColumnInfo637.Enabled = false;
            clsColumnInfo637.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo637.HeadText = "流水号";
            clsColumnInfo637.ReadOnly = true;
            clsColumnInfo637.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo638.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo638.BackColor = System.Drawing.Color.White;
            clsColumnInfo638.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo638.ColumnIndex = 2;
            clsColumnInfo638.ColumnName = "ORDER_INT";
            clsColumnInfo638.ColumnWidth = 100;
            clsColumnInfo638.Enabled = false;
            clsColumnInfo638.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo638.HeadText = "候诊队号";
            clsColumnInfo638.ReadOnly = true;
            clsColumnInfo638.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo639.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo639.BackColor = System.Drawing.Color.White;
            clsColumnInfo639.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo639.ColumnIndex = 3;
            clsColumnInfo639.ColumnName = "PATIENTCARDID_CHR";
            clsColumnInfo639.ColumnWidth = 100;
            clsColumnInfo639.Enabled = false;
            clsColumnInfo639.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo639.HeadText = "诊疗卡号";
            clsColumnInfo639.ReadOnly = true;
            clsColumnInfo639.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo640.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo640.BackColor = System.Drawing.Color.White;
            clsColumnInfo640.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo640.ColumnIndex = 4;
            clsColumnInfo640.ColumnName = "LASTNAME_VCHR";
            clsColumnInfo640.ColumnWidth = 100;
            clsColumnInfo640.Enabled = false;
            clsColumnInfo640.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo640.HeadText = "病人名称";
            clsColumnInfo640.ReadOnly = true;
            clsColumnInfo640.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo641.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo641.BackColor = System.Drawing.Color.White;
            clsColumnInfo641.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo641.ColumnIndex = 5;
            clsColumnInfo641.ColumnName = "SEX_CHR";
            clsColumnInfo641.ColumnWidth = 50;
            clsColumnInfo641.Enabled = false;
            clsColumnInfo641.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo641.HeadText = "性别";
            clsColumnInfo641.ReadOnly = true;
            clsColumnInfo641.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo642.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo642.BackColor = System.Drawing.Color.White;
            clsColumnInfo642.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo642.ColumnIndex = 6;
            clsColumnInfo642.ColumnName = "BIRTH_DAT";
            clsColumnInfo642.ColumnWidth = 60;
            clsColumnInfo642.Enabled = false;
            clsColumnInfo642.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo642.HeadText = "年龄";
            clsColumnInfo642.ReadOnly = true;
            clsColumnInfo642.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo643.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo643.BackColor = System.Drawing.Color.White;
            clsColumnInfo643.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo643.ColumnIndex = 7;
            clsColumnInfo643.ColumnName = "PAYTYPENAME_VCHR";
            clsColumnInfo643.ColumnWidth = 100;
            clsColumnInfo643.Enabled = false;
            clsColumnInfo643.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo643.HeadText = "病人类型";
            clsColumnInfo643.ReadOnly = true;
            clsColumnInfo643.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo644.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo644.BackColor = System.Drawing.Color.White;
            clsColumnInfo644.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo644.ColumnIndex = 8;
            clsColumnInfo644.ColumnName = "DEPTNAME_VCHR";
            clsColumnInfo644.ColumnWidth = 100;
            clsColumnInfo644.Enabled = false;
            clsColumnInfo644.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo644.HeadText = "挂号科室";
            clsColumnInfo644.ReadOnly = true;
            clsColumnInfo644.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo645.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo645.BackColor = System.Drawing.Color.White;
            clsColumnInfo645.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo645.ColumnIndex = 9;
            clsColumnInfo645.ColumnName = "DOCNAME";
            clsColumnInfo645.ColumnWidth = 90;
            clsColumnInfo645.Enabled = false;
            clsColumnInfo645.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo645.HeadText = "挂号医生";
            clsColumnInfo645.ReadOnly = true;
            clsColumnInfo645.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo646.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo646.BackColor = System.Drawing.Color.White;
            clsColumnInfo646.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo646.ColumnIndex = 10;
            clsColumnInfo646.ColumnName = "registerid_chr";
            clsColumnInfo646.ColumnWidth = 0;
            clsColumnInfo646.Enabled = true;
            clsColumnInfo646.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo646.HeadText = "registerid_chr";
            clsColumnInfo646.ReadOnly = true;
            clsColumnInfo646.TextFont = new System.Drawing.Font("宋体", 10F);
            this.m_dtgWaitReg.Columns.Add(clsColumnInfo636);
            this.m_dtgWaitReg.Columns.Add(clsColumnInfo637);
            this.m_dtgWaitReg.Columns.Add(clsColumnInfo638);
            this.m_dtgWaitReg.Columns.Add(clsColumnInfo639);
            this.m_dtgWaitReg.Columns.Add(clsColumnInfo640);
            this.m_dtgWaitReg.Columns.Add(clsColumnInfo641);
            this.m_dtgWaitReg.Columns.Add(clsColumnInfo642);
            this.m_dtgWaitReg.Columns.Add(clsColumnInfo643);
            this.m_dtgWaitReg.Columns.Add(clsColumnInfo644);
            this.m_dtgWaitReg.Columns.Add(clsColumnInfo645);
            this.m_dtgWaitReg.Columns.Add(clsColumnInfo646);
            this.m_dtgWaitReg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dtgWaitReg.Font = new System.Drawing.Font("宋体", 11F);
            this.m_dtgWaitReg.FullRowSelect = true;
            this.m_dtgWaitReg.Location = new System.Drawing.Point(0, 0);
            this.m_dtgWaitReg.MultiSelect = false;
            this.m_dtgWaitReg.Name = "m_dtgWaitReg";
            this.m_dtgWaitReg.ReadOnly = true;
            this.m_dtgWaitReg.RowHeadersVisible = true;
            this.m_dtgWaitReg.RowHeaderWidth = 20;
            this.m_dtgWaitReg.SelectedRowBackColor = System.Drawing.Color.Navy;
            this.m_dtgWaitReg.SelectedRowForeColor = System.Drawing.Color.White;
            this.m_dtgWaitReg.Size = new System.Drawing.Size(842, 361);
            this.m_dtgWaitReg.TabIndex = 35;
            this.m_dtgWaitReg.m_evtCurrentCellChanged += new System.EventHandler(this.m_dtgWaitReg_m_evtCurrentCellChanged);
            this.m_dtgWaitReg.m_evtDoubleClickCell += new com.digitalwave.controls.datagrid.clsDGTextMouseClickEventHandler(this.m_dtgWaitReg_m_evtDoubleClickCell);
            this.m_dtgWaitReg.m_evtDataGridKeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtgWaitReg_m_evtDataGridKeyDown);
            this.m_dtgWaitReg.DoubleClick += new System.EventHandler(this.m_dtgWaitReg_DoubleClick);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.cmbDeparment);
            this.groupBox8.Controls.Add(this.btSelect);
            this.groupBox8.Controls.Add(this.ra_both);
            this.groupBox8.Controls.Add(this.ra_department);
            this.groupBox8.Controls.Add(this.ra_Person);
            this.groupBox8.Controls.Add(this.m_btnRefReg);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox8.Location = new System.Drawing.Point(0, 361);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(842, 48);
            this.groupBox8.TabIndex = 36;
            this.groupBox8.TabStop = false;
            // 
            // btSelect
            // 
            this.btSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btSelect.DefaultScheme = true;
            this.btSelect.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btSelect.Hint = "把西药处方按方号重新排列";
            this.btSelect.Location = new System.Drawing.Point(508, 12);
            this.btSelect.Name = "btSelect";
            this.btSelect.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btSelect.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btSelect.Size = new System.Drawing.Size(73, 30);
            this.btSelect.TabIndex = 14;
            this.btSelect.Text = "确定(&S)";
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // m_btnRefReg
            // 
            this.m_btnRefReg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_btnRefReg.DefaultScheme = true;
            this.m_btnRefReg.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnRefReg.Hint = "刷新候诊列表";
            this.m_btnRefReg.Location = new System.Drawing.Point(608, 12);
            this.m_btnRefReg.Name = "m_btnRefReg";
            this.m_btnRefReg.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_btnRefReg.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnRefReg.Size = new System.Drawing.Size(73, 30);
            this.m_btnRefReg.TabIndex = 8;
            this.m_btnRefReg.Text = "刷新(&R)";
            this.m_btnRefReg.Click += new System.EventHandler(this.m_btnRefReg_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_dtgTake);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(842, 409);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "[1]就诊";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // m_dtgTake
            // 
            this.m_dtgTake.AllowAddNew = false;
            this.m_dtgTake.AllowDelete = false;
            this.m_dtgTake.AutoAppendRow = false;
            this.m_dtgTake.AutoScroll = true;
            this.m_dtgTake.BackgroundColor = System.Drawing.SystemColors.Window;
            this.m_dtgTake.CaptionText = "";
            this.m_dtgTake.CaptionVisible = false;
            this.m_dtgTake.ColumnHeadersVisible = true;
            clsColumnInfo647.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo647.BackColor = System.Drawing.Color.White;
            clsColumnInfo647.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo647.ColumnIndex = 0;
            clsColumnInfo647.ColumnName = "TAKEDIAGRECID_CHR";
            clsColumnInfo647.ColumnWidth = 0;
            clsColumnInfo647.Enabled = false;
            clsColumnInfo647.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo647.HeadText = "接诊ID";
            clsColumnInfo647.ReadOnly = true;
            clsColumnInfo647.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo648.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo648.BackColor = System.Drawing.Color.White;
            clsColumnInfo648.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo648.ColumnIndex = 1;
            clsColumnInfo648.ColumnName = "REGISTERID_CHR";
            clsColumnInfo648.ColumnWidth = 0;
            clsColumnInfo648.Enabled = false;
            clsColumnInfo648.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo648.HeadText = "挂号ID";
            clsColumnInfo648.ReadOnly = true;
            clsColumnInfo648.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo649.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo649.BackColor = System.Drawing.Color.White;
            clsColumnInfo649.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo649.ColumnIndex = 2;
            clsColumnInfo649.ColumnName = "PATIENTCARDID_CHR";
            clsColumnInfo649.ColumnWidth = 100;
            clsColumnInfo649.Enabled = false;
            clsColumnInfo649.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo649.HeadText = "诊疗卡号";
            clsColumnInfo649.ReadOnly = true;
            clsColumnInfo649.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo650.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo650.BackColor = System.Drawing.Color.White;
            clsColumnInfo650.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo650.ColumnIndex = 3;
            clsColumnInfo650.ColumnName = "LASTNAME_VCHR";
            clsColumnInfo650.ColumnWidth = 100;
            clsColumnInfo650.Enabled = false;
            clsColumnInfo650.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo650.HeadText = "病人名称";
            clsColumnInfo650.ReadOnly = true;
            clsColumnInfo650.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo651.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo651.BackColor = System.Drawing.Color.White;
            clsColumnInfo651.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo651.ColumnIndex = 4;
            clsColumnInfo651.ColumnName = "SEX_CHR";
            clsColumnInfo651.ColumnWidth = 55;
            clsColumnInfo651.Enabled = false;
            clsColumnInfo651.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo651.HeadText = "性别";
            clsColumnInfo651.ReadOnly = true;
            clsColumnInfo651.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo652.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo652.BackColor = System.Drawing.Color.White;
            clsColumnInfo652.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo652.ColumnIndex = 5;
            clsColumnInfo652.ColumnName = "BIRTH_DAT";
            clsColumnInfo652.ColumnWidth = 60;
            clsColumnInfo652.Enabled = false;
            clsColumnInfo652.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo652.HeadText = "年龄";
            clsColumnInfo652.ReadOnly = true;
            clsColumnInfo652.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo653.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo653.BackColor = System.Drawing.Color.White;
            clsColumnInfo653.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo653.ColumnIndex = 6;
            clsColumnInfo653.ColumnName = "PAYTYPENAME_VCHR";
            clsColumnInfo653.ColumnWidth = 100;
            clsColumnInfo653.Enabled = false;
            clsColumnInfo653.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo653.HeadText = "病人类型";
            clsColumnInfo653.ReadOnly = true;
            clsColumnInfo653.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo654.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo654.BackColor = System.Drawing.Color.White;
            clsColumnInfo654.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo654.ColumnIndex = 7;
            clsColumnInfo654.ColumnName = "TAKEDIAGTIME_DAT";
            clsColumnInfo654.ColumnWidth = 150;
            clsColumnInfo654.Enabled = false;
            clsColumnInfo654.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo654.HeadText = "接诊时间";
            clsColumnInfo654.ReadOnly = true;
            clsColumnInfo654.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo655.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo655.BackColor = System.Drawing.Color.White;
            clsColumnInfo655.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo655.ColumnIndex = 8;
            clsColumnInfo655.ColumnName = "ENDTIME_DAT";
            clsColumnInfo655.ColumnWidth = 150;
            clsColumnInfo655.Enabled = false;
            clsColumnInfo655.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo655.HeadText = "结束时间";
            clsColumnInfo655.ReadOnly = true;
            clsColumnInfo655.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo656.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo656.BackColor = System.Drawing.Color.White;
            clsColumnInfo656.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo656.ColumnIndex = 9;
            clsColumnInfo656.ColumnName = "state";
            clsColumnInfo656.ColumnWidth = 85;
            clsColumnInfo656.Enabled = false;
            clsColumnInfo656.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo656.HeadText = "状态";
            clsColumnInfo656.ReadOnly = true;
            clsColumnInfo656.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo657.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo657.BackColor = System.Drawing.Color.White;
            clsColumnInfo657.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo657.ColumnIndex = 10;
            clsColumnInfo657.ColumnName = "PSTATUS_INT";
            clsColumnInfo657.ColumnWidth = 0;
            clsColumnInfo657.Enabled = false;
            clsColumnInfo657.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo657.HeadText = "状态值";
            clsColumnInfo657.ReadOnly = true;
            clsColumnInfo657.TextFont = new System.Drawing.Font("宋体", 11F);
            this.m_dtgTake.Columns.Add(clsColumnInfo647);
            this.m_dtgTake.Columns.Add(clsColumnInfo648);
            this.m_dtgTake.Columns.Add(clsColumnInfo649);
            this.m_dtgTake.Columns.Add(clsColumnInfo650);
            this.m_dtgTake.Columns.Add(clsColumnInfo651);
            this.m_dtgTake.Columns.Add(clsColumnInfo652);
            this.m_dtgTake.Columns.Add(clsColumnInfo653);
            this.m_dtgTake.Columns.Add(clsColumnInfo654);
            this.m_dtgTake.Columns.Add(clsColumnInfo655);
            this.m_dtgTake.Columns.Add(clsColumnInfo656);
            this.m_dtgTake.Columns.Add(clsColumnInfo657);
            this.m_dtgTake.ContextMenu = this.ctmPat;
            this.m_dtgTake.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dtgTake.Font = new System.Drawing.Font("宋体", 11F);
            this.m_dtgTake.FullRowSelect = true;
            this.m_dtgTake.Location = new System.Drawing.Point(0, 0);
            this.m_dtgTake.MultiSelect = false;
            this.m_dtgTake.Name = "m_dtgTake";
            this.m_dtgTake.ReadOnly = true;
            this.m_dtgTake.RowHeadersVisible = true;
            this.m_dtgTake.RowHeaderWidth = 20;
            this.m_dtgTake.SelectedRowBackColor = System.Drawing.Color.Navy;
            this.m_dtgTake.SelectedRowForeColor = System.Drawing.Color.White;
            this.m_dtgTake.Size = new System.Drawing.Size(842, 361);
            this.m_dtgTake.TabIndex = 36;
            this.m_dtgTake.m_evtDoubleClickCell += new com.digitalwave.controls.datagrid.clsDGTextMouseClickEventHandler(this.m_dtgTake_m_evtDoubleClickCell);
            this.m_dtgTake.m_evtDataGridKeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtgTake_m_evtDataGridKeyDown);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblPersonTimes);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.pictureBox1);
            this.groupBox5.Controls.Add(this.btnSelect);
            this.groupBox5.Controls.Add(this.lblToday);
            this.groupBox5.Controls.Add(this.lblEnd);
            this.groupBox5.Controls.Add(this.lblBegin);
            this.groupBox5.Controls.Add(this.dtpEnd);
            this.groupBox5.Controls.Add(this.dtpBegin);
            this.groupBox5.Controls.Add(this.llblDate);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox5.Location = new System.Drawing.Point(0, 361);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(842, 48);
            this.groupBox5.TabIndex = 37;
            this.groupBox5.TabStop = false;
            // 
            // lblPersonTimes
            // 
            this.lblPersonTimes.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPersonTimes.ForeColor = System.Drawing.Color.Black;
            this.lblPersonTimes.Location = new System.Drawing.Point(312, 20);
            this.lblPersonTimes.Name = "lblPersonTimes";
            this.lblPersonTimes.Size = new System.Drawing.Size(60, 16);
            this.lblPersonTimes.TabIndex = 12;
            this.lblPersonTimes.Text = "00000";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(236, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 16);
            this.label10.TabIndex = 11;
            this.label10.Text = "就诊人次:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(15, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnSelect.DefaultScheme = true;
            this.btnSelect.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSelect.Font = new System.Drawing.Font("宋体", 10.5F);
            this.btnSelect.Hint = "";
            this.btnSelect.Location = new System.Drawing.Point(773, 14);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnSelect.Size = new System.Drawing.Size(56, 28);
            this.btnSelect.TabIndex = 9;
            this.btnSelect.Text = "查询";
            this.btnSelect.Visible = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lblToday
            // 
            this.lblToday.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblToday.ForeColor = System.Drawing.Color.Black;
            this.lblToday.Location = new System.Drawing.Point(89, 20);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(130, 16);
            this.lblToday.TabIndex = 8;
            this.lblToday.Text = "0000年00月00日";
            // 
            // lblEnd
            // 
            this.lblEnd.Location = new System.Drawing.Point(622, 21);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(16, 16);
            this.lblEnd.TabIndex = 7;
            this.lblEnd.Text = "到";
            this.lblEnd.Visible = false;
            // 
            // lblBegin
            // 
            this.lblBegin.Location = new System.Drawing.Point(473, 21);
            this.lblBegin.Name = "lblBegin";
            this.lblBegin.Size = new System.Drawing.Size(16, 16);
            this.lblBegin.TabIndex = 6;
            this.lblBegin.Text = "从";
            this.lblBegin.Visible = false;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(645, 17);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(122, 23);
            this.dtpEnd.TabIndex = 4;
            this.dtpEnd.Visible = false;
            // 
            // dtpBegin
            // 
            this.dtpBegin.Location = new System.Drawing.Point(496, 17);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(122, 23);
            this.dtpBegin.TabIndex = 3;
            this.dtpBegin.Visible = false;
            // 
            // llblDate
            // 
            this.llblDate.ActiveLinkColor = System.Drawing.Color.OrangeRed;
            this.llblDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llblDate.LinkArea = new System.Windows.Forms.LinkArea(0, 8);
            this.llblDate.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llblDate.LinkColor = System.Drawing.Color.Black;
            this.llblDate.Location = new System.Drawing.Point(387, 21);
            this.llblDate.Name = "llblDate";
            this.llblDate.Size = new System.Drawing.Size(108, 16);
            this.llblDate.TabIndex = 2;
            this.llblDate.TabStop = true;
            this.llblDate.Text = "按时间段查询>>";
            this.llblDate.VisitedLinkColor = System.Drawing.Color.Blue;
            this.llblDate.Click += new System.EventHandler(this.llblDate_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(42, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "今天:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(842, 409);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "[2]病历";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btAssistantDiagnose);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(842, 410);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btAssistantDiagnose
            // 
            this.btAssistantDiagnose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btAssistantDiagnose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btAssistantDiagnose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAssistantDiagnose.Location = new System.Drawing.Point(600, 372);
            this.btAssistantDiagnose.Name = "btAssistantDiagnose";
            this.btAssistantDiagnose.Size = new System.Drawing.Size(96, 32);
            this.btAssistantDiagnose.TabIndex = 26;
            this.btAssistantDiagnose.Text = "辅助诊疗";
            this.btAssistantDiagnose.Visible = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.listView3);
            this.tabPage5.Controls.Add(this.listView2);
            this.tabPage5.Controls.Add(this.ctlDataGrid1);
            this.tabPage5.Location = new System.Drawing.Point(4, 27);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(842, 409);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "[3]西药";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // listView3
            // 
            this.listView3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18});
            this.listView3.FullRowSelect = true;
            this.listView3.GridLines = true;
            this.listView3.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView3.Location = new System.Drawing.Point(376, 168);
            this.listView3.MultiSelect = false;
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(208, 128);
            this.listView3.TabIndex = 35;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            this.listView3.Visible = false;
            this.listView3.DoubleClick += new System.EventHandler(this.listView3_DoubleClick);
            this.listView3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView3_KeyDown);
            this.listView3.Leave += new System.EventHandler(this.listView3_Leave);
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "频率ID";
            this.columnHeader14.Width = 0;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "助记码";
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "名称";
            this.columnHeader16.Width = 120;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "次数";
            this.columnHeader17.Width = 0;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "天数";
            this.columnHeader18.Width = 0;
            // 
            // listView2
            // 
            this.listView2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader11});
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView2.Location = new System.Drawing.Point(152, 168);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(208, 128);
            this.listView2.TabIndex = 29;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.Visible = false;
            this.listView2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView2_KeyDown);
            this.listView2.Leave += new System.EventHandler(this.listView2_Leave);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "频率ID";
            this.columnHeader7.Width = 0;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "助记码";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "名称";
            this.columnHeader11.Width = 120;
            // 
            // ctlDataGrid1
            // 
            this.ctlDataGrid1.AllowAddNew = true;
            this.ctlDataGrid1.AllowDelete = true;
            this.ctlDataGrid1.AutoAppendRow = false;
            this.ctlDataGrid1.AutoScroll = true;
            this.ctlDataGrid1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ctlDataGrid1.CaptionText = "";
            this.ctlDataGrid1.CaptionVisible = false;
            this.ctlDataGrid1.ColumnHeadersVisible = true;
            clsColumnInfo464.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo464.BackColor = System.Drawing.Color.White;
            clsColumnInfo464.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo464.ColumnIndex = 0;
            clsColumnInfo464.ColumnName = "Column32";
            clsColumnInfo464.ColumnWidth = 40;
            clsColumnInfo464.Enabled = true;
            clsColumnInfo464.ForeColor = System.Drawing.Color.Blue;
            clsColumnInfo464.HeadText = "方号";
            clsColumnInfo464.ReadOnly = false;
            clsColumnInfo464.TextFont = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            clsColumnInfo465.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo465.BackColor = System.Drawing.Color.White;
            clsColumnInfo465.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo465.ColumnIndex = 1;
            clsColumnInfo465.ColumnName = "Column1";
            clsColumnInfo465.ColumnWidth = 60;
            clsColumnInfo465.Enabled = true;
            clsColumnInfo465.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo465.HeadText = "查询";
            clsColumnInfo465.ReadOnly = false;
            clsColumnInfo465.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo466.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo466.BackColor = System.Drawing.Color.White;
            clsColumnInfo466.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo466.ColumnIndex = 2;
            clsColumnInfo466.ColumnName = "Column2";
            clsColumnInfo466.ColumnWidth = 50;
            clsColumnInfo466.Enabled = true;
            clsColumnInfo466.ForeColor = System.Drawing.Color.Red;
            clsColumnInfo466.HeadText = "用量";
            clsColumnInfo466.ReadOnly = false;
            clsColumnInfo466.TextFont = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            clsColumnInfo467.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo467.BackColor = System.Drawing.Color.White;
            clsColumnInfo467.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo467.ColumnIndex = 3;
            clsColumnInfo467.ColumnName = "Column5";
            clsColumnInfo467.ColumnWidth = 45;
            clsColumnInfo467.Enabled = false;
            clsColumnInfo467.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo467.HeadText = "单位";
            clsColumnInfo467.ReadOnly = true;
            clsColumnInfo467.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo468.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo468.BackColor = System.Drawing.Color.White;
            clsColumnInfo468.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo468.ColumnIndex = 4;
            clsColumnInfo468.ColumnName = "Column3";
            clsColumnInfo468.ColumnWidth = 110;
            clsColumnInfo468.Enabled = false;
            clsColumnInfo468.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo468.HeadText = "项目名称";
            clsColumnInfo468.ReadOnly = true;
            clsColumnInfo468.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo469.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo469.BackColor = System.Drawing.Color.White;
            clsColumnInfo469.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo469.ColumnIndex = 5;
            clsColumnInfo469.ColumnName = "Column4";
            clsColumnInfo469.ColumnWidth = 70;
            clsColumnInfo469.Enabled = true;
            clsColumnInfo469.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo469.HeadText = "规格";
            clsColumnInfo469.ReadOnly = true;
            clsColumnInfo469.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo470.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo470.BackColor = System.Drawing.Color.White;
            clsColumnInfo470.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo470.ColumnIndex = 6;
            clsColumnInfo470.ColumnName = "Column14";
            clsColumnInfo470.ColumnWidth = 70;
            clsColumnInfo470.Enabled = true;
            clsColumnInfo470.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo470.HeadText = "用法";
            clsColumnInfo470.ReadOnly = false;
            clsColumnInfo470.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo471.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo471.BackColor = System.Drawing.Color.White;
            clsColumnInfo471.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo471.ColumnIndex = 7;
            clsColumnInfo471.ColumnName = "Column15";
            clsColumnInfo471.ColumnWidth = 50;
            clsColumnInfo471.Enabled = true;
            clsColumnInfo471.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo471.HeadText = "频率";
            clsColumnInfo471.ReadOnly = false;
            clsColumnInfo471.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo472.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            clsColumnInfo472.BackColor = System.Drawing.Color.White;
            clsColumnInfo472.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Int;
            clsColumnInfo472.ColumnIndex = 8;
            clsColumnInfo472.ColumnName = "Column22";
            clsColumnInfo472.ColumnWidth = 40;
            clsColumnInfo472.Enabled = true;
            clsColumnInfo472.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo472.HeadText = "天";
            clsColumnInfo472.ReadOnly = false;
            clsColumnInfo472.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo473.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo473.BackColor = System.Drawing.Color.White;
            clsColumnInfo473.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo473.ColumnIndex = 9;
            clsColumnInfo473.ColumnName = "Column6";
            clsColumnInfo473.ColumnWidth = 50;
            clsColumnInfo473.Enabled = true;
            clsColumnInfo473.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo473.HeadText = "单价";
            clsColumnInfo473.ReadOnly = false;
            clsColumnInfo473.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo474.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo474.BackColor = System.Drawing.Color.White;
            clsColumnInfo474.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo474.ColumnIndex = 10;
            clsColumnInfo474.ColumnName = "Column7";
            clsColumnInfo474.ColumnWidth = 56;
            clsColumnInfo474.Enabled = true;
            clsColumnInfo474.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo474.HeadText = "总价";
            clsColumnInfo474.ReadOnly = true;
            clsColumnInfo474.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo658.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo658.BackColor = System.Drawing.Color.White;
            clsColumnInfo658.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo658.ColumnIndex = 11;
            clsColumnInfo658.ColumnName = "Column10";
            clsColumnInfo658.ColumnWidth = 0;
            clsColumnInfo658.Enabled = true;
            clsColumnInfo658.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo658.HeadText = "ID";
            clsColumnInfo658.ReadOnly = true;
            clsColumnInfo658.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo659.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo659.BackColor = System.Drawing.Color.White;
            clsColumnInfo659.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo659.ColumnIndex = 12;
            clsColumnInfo659.ColumnName = "Column16";
            clsColumnInfo659.ColumnWidth = 0;
            clsColumnInfo659.Enabled = true;
            clsColumnInfo659.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo659.HeadText = "包装量";
            clsColumnInfo659.ReadOnly = true;
            clsColumnInfo659.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo660.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo660.BackColor = System.Drawing.Color.White;
            clsColumnInfo660.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo660.ColumnIndex = 13;
            clsColumnInfo660.ColumnName = "Column17";
            clsColumnInfo660.ColumnWidth = 43;
            clsColumnInfo660.Enabled = true;
            clsColumnInfo660.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo660.HeadText = "总数";
            clsColumnInfo660.ReadOnly = false;
            clsColumnInfo660.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo661.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo661.BackColor = System.Drawing.Color.White;
            clsColumnInfo661.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo661.ColumnIndex = 14;
            clsColumnInfo661.ColumnName = "Column18";
            clsColumnInfo661.ColumnWidth = 0;
            clsColumnInfo661.Enabled = true;
            clsColumnInfo661.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo661.HeadText = "";
            clsColumnInfo661.ReadOnly = true;
            clsColumnInfo661.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo662.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo662.BackColor = System.Drawing.Color.White;
            clsColumnInfo662.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo662.ColumnIndex = 15;
            clsColumnInfo662.ColumnName = "Column19";
            clsColumnInfo662.ColumnWidth = 0;
            clsColumnInfo662.Enabled = true;
            clsColumnInfo662.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo662.HeadText = "";
            clsColumnInfo662.ReadOnly = true;
            clsColumnInfo662.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo663.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo663.BackColor = System.Drawing.Color.White;
            clsColumnInfo663.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo663.ColumnIndex = 16;
            clsColumnInfo663.ColumnName = "Column20";
            clsColumnInfo663.ColumnWidth = 0;
            clsColumnInfo663.Enabled = true;
            clsColumnInfo663.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo663.HeadText = "";
            clsColumnInfo663.ReadOnly = true;
            clsColumnInfo663.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo664.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo664.BackColor = System.Drawing.Color.White;
            clsColumnInfo664.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo664.ColumnIndex = 17;
            clsColumnInfo664.ColumnName = "Column21";
            clsColumnInfo664.ColumnWidth = 0;
            clsColumnInfo664.Enabled = true;
            clsColumnInfo664.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo664.HeadText = "";
            clsColumnInfo664.ReadOnly = true;
            clsColumnInfo664.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo665.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            clsColumnInfo665.BackColor = System.Drawing.Color.White;
            clsColumnInfo665.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo665.ColumnIndex = 18;
            clsColumnInfo665.ColumnName = "Column23";
            clsColumnInfo665.ColumnWidth = 45;
            clsColumnInfo665.Enabled = true;
            clsColumnInfo665.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo665.HeadText = "单位";
            clsColumnInfo665.ReadOnly = true;
            clsColumnInfo665.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo666.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo666.BackColor = System.Drawing.Color.White;
            clsColumnInfo666.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo666.ColumnIndex = 19;
            clsColumnInfo666.ColumnName = "Column24";
            clsColumnInfo666.ColumnWidth = 0;
            clsColumnInfo666.Enabled = false;
            clsColumnInfo666.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo666.HeadText = "";
            clsColumnInfo666.ReadOnly = true;
            clsColumnInfo666.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo667.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo667.BackColor = System.Drawing.Color.White;
            clsColumnInfo667.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo667.ColumnIndex = 20;
            clsColumnInfo667.ColumnName = "Column25";
            clsColumnInfo667.ColumnWidth = 0;
            clsColumnInfo667.Enabled = false;
            clsColumnInfo667.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo667.HeadText = "";
            clsColumnInfo667.ReadOnly = true;
            clsColumnInfo667.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo668.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo668.BackColor = System.Drawing.Color.White;
            clsColumnInfo668.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo668.ColumnIndex = 21;
            clsColumnInfo668.ColumnName = "Column26";
            clsColumnInfo668.ColumnWidth = 0;
            clsColumnInfo668.Enabled = true;
            clsColumnInfo668.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo668.HeadText = "";
            clsColumnInfo668.ReadOnly = true;
            clsColumnInfo668.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo669.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo669.BackColor = System.Drawing.Color.White;
            clsColumnInfo669.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo669.ColumnIndex = 22;
            clsColumnInfo669.ColumnName = "Column27";
            clsColumnInfo669.ColumnWidth = 0;
            clsColumnInfo669.Enabled = true;
            clsColumnInfo669.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo669.HeadText = "";
            clsColumnInfo669.ReadOnly = true;
            clsColumnInfo669.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo670.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo670.BackColor = System.Drawing.Color.White;
            clsColumnInfo670.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo670.ColumnIndex = 23;
            clsColumnInfo670.ColumnName = "Column28";
            clsColumnInfo670.ColumnWidth = 0;
            clsColumnInfo670.Enabled = true;
            clsColumnInfo670.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo670.HeadText = "";
            clsColumnInfo670.ReadOnly = true;
            clsColumnInfo670.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo671.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo671.BackColor = System.Drawing.Color.White;
            clsColumnInfo671.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo671.ColumnIndex = 24;
            clsColumnInfo671.ColumnName = "Column29";
            clsColumnInfo671.ColumnWidth = 0;
            clsColumnInfo671.Enabled = true;
            clsColumnInfo671.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo671.HeadText = "";
            clsColumnInfo671.ReadOnly = true;
            clsColumnInfo671.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo672.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo672.BackColor = System.Drawing.Color.White;
            clsColumnInfo672.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo672.ColumnIndex = 25;
            clsColumnInfo672.ColumnName = "Column30";
            clsColumnInfo672.ColumnWidth = 0;
            clsColumnInfo672.Enabled = true;
            clsColumnInfo672.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo672.HeadText = "";
            clsColumnInfo672.ReadOnly = true;
            clsColumnInfo672.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo673.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo673.BackColor = System.Drawing.Color.White;
            clsColumnInfo673.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo673.ColumnIndex = 26;
            clsColumnInfo673.ColumnName = "Column31";
            clsColumnInfo673.ColumnWidth = 0;
            clsColumnInfo673.Enabled = false;
            clsColumnInfo673.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo673.HeadText = "";
            clsColumnInfo673.ReadOnly = true;
            clsColumnInfo673.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo674.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo674.BackColor = System.Drawing.Color.White;
            clsColumnInfo674.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo674.ColumnIndex = 27;
            clsColumnInfo674.ColumnName = "Column33";
            clsColumnInfo674.ColumnWidth = 0;
            clsColumnInfo674.Enabled = false;
            clsColumnInfo674.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo674.HeadText = "";
            clsColumnInfo674.ReadOnly = true;
            clsColumnInfo674.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo675.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo675.BackColor = System.Drawing.Color.White;
            clsColumnInfo675.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo675.ColumnIndex = 28;
            clsColumnInfo675.ColumnName = "Column34";
            clsColumnInfo675.ColumnWidth = 0;
            clsColumnInfo675.Enabled = false;
            clsColumnInfo675.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo675.HeadText = "";
            clsColumnInfo675.ReadOnly = true;
            clsColumnInfo675.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo676.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo676.BackColor = System.Drawing.Color.White;
            clsColumnInfo676.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Int;
            clsColumnInfo676.ColumnIndex = 29;
            clsColumnInfo676.ColumnName = "Column35";
            clsColumnInfo676.ColumnWidth = 0;
            clsColumnInfo676.Enabled = false;
            clsColumnInfo676.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo676.HeadText = "";
            clsColumnInfo676.ReadOnly = true;
            clsColumnInfo676.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo677.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo677.BackColor = System.Drawing.Color.White;
            clsColumnInfo677.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo677.ColumnIndex = 30;
            clsColumnInfo677.ColumnName = "Column36";
            clsColumnInfo677.ColumnWidth = 0;
            clsColumnInfo677.Enabled = false;
            clsColumnInfo677.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo677.HeadText = "";
            clsColumnInfo677.ReadOnly = true;
            clsColumnInfo677.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo678.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo678.BackColor = System.Drawing.Color.White;
            clsColumnInfo678.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo678.ColumnIndex = 31;
            clsColumnInfo678.ColumnName = "Column37";
            clsColumnInfo678.ColumnWidth = 0;
            clsColumnInfo678.Enabled = false;
            clsColumnInfo678.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo678.HeadText = "";
            clsColumnInfo678.ReadOnly = true;
            clsColumnInfo678.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo679.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo679.BackColor = System.Drawing.Color.White;
            clsColumnInfo679.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo679.ColumnIndex = 32;
            clsColumnInfo679.ColumnName = "Column38";
            clsColumnInfo679.ColumnWidth = 0;
            clsColumnInfo679.Enabled = false;
            clsColumnInfo679.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo679.HeadText = "";
            clsColumnInfo679.ReadOnly = true;
            clsColumnInfo679.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo680.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo680.BackColor = System.Drawing.Color.White;
            clsColumnInfo680.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo680.ColumnIndex = 33;
            clsColumnInfo680.ColumnName = "Column39";
            clsColumnInfo680.ColumnWidth = 0;
            clsColumnInfo680.Enabled = false;
            clsColumnInfo680.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo680.HeadText = "";
            clsColumnInfo680.ReadOnly = true;
            clsColumnInfo680.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo681.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo681.BackColor = System.Drawing.Color.White;
            clsColumnInfo681.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo681.ColumnIndex = 34;
            clsColumnInfo681.ColumnName = "Column40";
            clsColumnInfo681.ColumnWidth = 0;
            clsColumnInfo681.Enabled = false;
            clsColumnInfo681.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo681.HeadText = "";
            clsColumnInfo681.ReadOnly = true;
            clsColumnInfo681.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo682.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo682.BackColor = System.Drawing.Color.White;
            clsColumnInfo682.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo682.ColumnIndex = 35;
            clsColumnInfo682.ColumnName = "Column41";
            clsColumnInfo682.ColumnWidth = 0;
            clsColumnInfo682.Enabled = false;
            clsColumnInfo682.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo682.HeadText = "";
            clsColumnInfo682.ReadOnly = true;
            clsColumnInfo682.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo683.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo683.BackColor = System.Drawing.Color.White;
            clsColumnInfo683.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo683.ColumnIndex = 36;
            clsColumnInfo683.ColumnName = "Column42";
            clsColumnInfo683.ColumnWidth = 0;
            clsColumnInfo683.Enabled = false;
            clsColumnInfo683.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo683.HeadText = "";
            clsColumnInfo683.ReadOnly = true;
            clsColumnInfo683.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo684.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo684.BackColor = System.Drawing.Color.White;
            clsColumnInfo684.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo684.ColumnIndex = 37;
            clsColumnInfo684.ColumnName = "Column43";
            clsColumnInfo684.ColumnWidth = 0;
            clsColumnInfo684.Enabled = false;
            clsColumnInfo684.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo684.HeadText = "";
            clsColumnInfo684.ReadOnly = true;
            clsColumnInfo684.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo685.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            clsColumnInfo685.BackColor = System.Drawing.Color.White;
            clsColumnInfo685.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo685.ColumnIndex = 38;
            clsColumnInfo685.ColumnName = "Column44";
            clsColumnInfo685.ColumnWidth = 72;
            clsColumnInfo685.Enabled = true;
            clsColumnInfo685.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo685.HeadText = "适应症";
            clsColumnInfo685.ReadOnly = false;
            clsColumnInfo685.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo686.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo686.BackColor = System.Drawing.Color.White;
            clsColumnInfo686.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo686.ColumnIndex = 39;
            clsColumnInfo686.ColumnName = "Column45";
            clsColumnInfo686.ColumnWidth = 0;
            clsColumnInfo686.Enabled = false;
            clsColumnInfo686.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo686.HeadText = "";
            clsColumnInfo686.ReadOnly = true;
            clsColumnInfo686.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo687.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo687.BackColor = System.Drawing.Color.White;
            clsColumnInfo687.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo687.ColumnIndex = 40;
            clsColumnInfo687.ColumnName = "Column46";
            clsColumnInfo687.ColumnWidth = 68;
            clsColumnInfo687.Enabled = false;
            clsColumnInfo687.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo687.HeadText = "总让利";
            clsColumnInfo687.ReadOnly = true;
            clsColumnInfo687.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo688.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo688.BackColor = System.Drawing.Color.White;
            clsColumnInfo688.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo688.ColumnIndex = 41;
            clsColumnInfo688.ColumnName = "Column47";
            clsColumnInfo688.ColumnWidth = 0;
            clsColumnInfo688.Enabled = false;
            clsColumnInfo688.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo688.HeadText = "";
            clsColumnInfo688.ReadOnly = true;
            clsColumnInfo688.TextFont = new System.Drawing.Font("宋体", 10F);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo464);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo465);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo466);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo467);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo468);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo469);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo470);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo471);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo472);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo473);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo474);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo658);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo659);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo660);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo661);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo662);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo663);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo664);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo665);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo666);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo667);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo668);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo669);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo670);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo671);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo672);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo673);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo674);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo675);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo676);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo677);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo678);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo679);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo680);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo681);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo682);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo683);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo684);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo685);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo686);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo687);
            this.ctlDataGrid1.Columns.Add(clsColumnInfo688);
            this.ctlDataGrid1.ContextMenuStrip = this.contextMenuStrip1;
            this.ctlDataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlDataGrid1.Font = new System.Drawing.Font("宋体", 11F);
            this.ctlDataGrid1.FullRowSelect = false;
            this.ctlDataGrid1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctlDataGrid1.Location = new System.Drawing.Point(0, 0);
            this.ctlDataGrid1.MultiSelect = true;
            this.ctlDataGrid1.Name = "ctlDataGrid1";
            this.ctlDataGrid1.ReadOnly = false;
            this.ctlDataGrid1.RowHeadersVisible = false;
            this.ctlDataGrid1.RowHeaderWidth = 35;
            this.ctlDataGrid1.SelectedRowBackColor = System.Drawing.Color.Navy;
            this.ctlDataGrid1.SelectedRowForeColor = System.Drawing.Color.White;
            this.ctlDataGrid1.Size = new System.Drawing.Size(842, 409);
            this.ctlDataGrid1.TabIndex = 34;
            this.ctlDataGrid1.m_evtDataGridTextBoxKeyDown += new com.digitalwave.controls.datagrid.clsDGTextKeyEventHandler(this.ctlDataGrid1_m_evtDataGridTextBoxKeyDown);
            this.ctlDataGrid1.m_evtCurrentCellChanged += new System.EventHandler(this.ctlDataGrid1_m_evtCurrentCellChanged);
            this.ctlDataGrid1.m_evtDataGridKeyDown += new System.Windows.Forms.KeyEventHandler(this.ctlDataGrid1_m_evtDataGridKeyDown);
            this.ctlDataGrid1.Leave += new System.EventHandler(this.ctlDataGrid1_Leave);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDrugInfo});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(141, 26);
            // 
            // tsmiDrugInfo
            // 
            this.tsmiDrugInfo.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmiDrugInfo.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDrugInfo.Image")));
            this.tsmiDrugInfo.Name = "tsmiDrugInfo";
            this.tsmiDrugInfo.Size = new System.Drawing.Size(140, 22);
            this.tsmiDrugInfo.Text = "药品说明书";
            this.tsmiDrugInfo.Click += new System.EventHandler(this.tsmiDrugInfo_Click);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.numericUpDown1);
            this.tabPage6.Controls.Add(this.cmbCooking);
            this.tabPage6.Controls.Add(this.label9);
            this.tabPage6.Controls.Add(this.label1);
            this.tabPage6.Controls.Add(this.listView4);
            this.tabPage6.Controls.Add(this.ctlDataGrid2);
            this.tabPage6.Location = new System.Drawing.Point(4, 27);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(842, 409);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "[4]中药";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(766, 378);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(56, 23);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.Location = new System.Drawing.Point(720, 378);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 24);
            this.label9.TabIndex = 37;
            this.label9.Text = "服数:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(3, 378);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 24);
            this.label1.TabIndex = 36;
            this.label1.Text = "用法:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listView4
            // 
            this.listView4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView4.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader10,
            this.columnHeader13});
            this.listView4.FullRowSelect = true;
            this.listView4.GridLines = true;
            this.listView4.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView4.Location = new System.Drawing.Point(336, 96);
            this.listView4.MultiSelect = false;
            this.listView4.Name = "listView4";
            this.listView4.Size = new System.Drawing.Size(208, 128);
            this.listView4.TabIndex = 33;
            this.listView4.UseCompatibleStateImageBehavior = false;
            this.listView4.View = System.Windows.Forms.View.Details;
            this.listView4.Visible = false;
            this.listView4.DoubleClick += new System.EventHandler(this.listView4_DoubleClick);
            this.listView4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView4_KeyDown);
            this.listView4.Leave += new System.EventHandler(this.listView4_Leave);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "频率ID";
            this.columnHeader6.Width = 0;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "助记码";
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "名称";
            this.columnHeader13.Width = 120;
            // 
            // ctlDataGrid2
            // 
            this.ctlDataGrid2.AllowAddNew = true;
            this.ctlDataGrid2.AllowDelete = true;
            this.ctlDataGrid2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlDataGrid2.AutoAppendRow = false;
            this.ctlDataGrid2.AutoScroll = true;
            this.ctlDataGrid2.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ctlDataGrid2.CaptionText = "";
            this.ctlDataGrid2.CaptionVisible = false;
            this.ctlDataGrid2.ColumnHeadersVisible = true;
            clsColumnInfo281.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo281.BackColor = System.Drawing.Color.White;
            clsColumnInfo281.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo281.ColumnIndex = 0;
            clsColumnInfo281.ColumnName = "Column1";
            clsColumnInfo281.ColumnWidth = 75;
            clsColumnInfo281.Enabled = true;
            clsColumnInfo281.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo281.HeadText = "查询";
            clsColumnInfo281.ReadOnly = false;
            clsColumnInfo281.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo282.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo282.BackColor = System.Drawing.Color.White;
            clsColumnInfo282.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo282.ColumnIndex = 1;
            clsColumnInfo282.ColumnName = "Column2";
            clsColumnInfo282.ColumnWidth = 60;
            clsColumnInfo282.Enabled = true;
            clsColumnInfo282.ForeColor = System.Drawing.Color.Red;
            clsColumnInfo282.HeadText = "数量";
            clsColumnInfo282.ReadOnly = false;
            clsColumnInfo282.TextFont = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            clsColumnInfo283.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo283.BackColor = System.Drawing.Color.White;
            clsColumnInfo283.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo283.ColumnIndex = 2;
            clsColumnInfo283.ColumnName = "Column5";
            clsColumnInfo283.ColumnWidth = 45;
            clsColumnInfo283.Enabled = false;
            clsColumnInfo283.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo283.HeadText = "单位";
            clsColumnInfo283.ReadOnly = true;
            clsColumnInfo283.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo284.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo284.BackColor = System.Drawing.Color.White;
            clsColumnInfo284.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo284.ColumnIndex = 3;
            clsColumnInfo284.ColumnName = "Column3";
            clsColumnInfo284.ColumnWidth = 180;
            clsColumnInfo284.Enabled = false;
            clsColumnInfo284.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo284.HeadText = "项目名称";
            clsColumnInfo284.ReadOnly = true;
            clsColumnInfo284.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo285.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo285.BackColor = System.Drawing.Color.White;
            clsColumnInfo285.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo285.ColumnIndex = 4;
            clsColumnInfo285.ColumnName = "Column4";
            clsColumnInfo285.ColumnWidth = 110;
            clsColumnInfo285.Enabled = false;
            clsColumnInfo285.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo285.HeadText = "规格";
            clsColumnInfo285.ReadOnly = true;
            clsColumnInfo285.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo286.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo286.BackColor = System.Drawing.Color.White;
            clsColumnInfo286.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo286.ColumnIndex = 5;
            clsColumnInfo286.ColumnName = "Column23";
            clsColumnInfo286.ColumnWidth = 86;
            clsColumnInfo286.Enabled = true;
            clsColumnInfo286.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo286.HeadText = "用法";
            clsColumnInfo286.ReadOnly = false;
            clsColumnInfo286.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo287.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo287.BackColor = System.Drawing.Color.White;
            clsColumnInfo287.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo287.ColumnIndex = 6;
            clsColumnInfo287.ColumnName = "Column6";
            clsColumnInfo287.ColumnWidth = 60;
            clsColumnInfo287.Enabled = false;
            clsColumnInfo287.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo287.HeadText = "单价";
            clsColumnInfo287.ReadOnly = true;
            clsColumnInfo287.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo288.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo288.BackColor = System.Drawing.Color.White;
            clsColumnInfo288.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo288.ColumnIndex = 7;
            clsColumnInfo288.ColumnName = "Column7";
            clsColumnInfo288.ColumnWidth = 60;
            clsColumnInfo288.Enabled = false;
            clsColumnInfo288.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo288.HeadText = "总价";
            clsColumnInfo288.ReadOnly = true;
            clsColumnInfo288.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo289.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo289.BackColor = System.Drawing.Color.White;
            clsColumnInfo289.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo289.ColumnIndex = 8;
            clsColumnInfo289.ColumnName = "Column10";
            clsColumnInfo289.ColumnWidth = 0;
            clsColumnInfo289.Enabled = false;
            clsColumnInfo289.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo289.HeadText = "ID";
            clsColumnInfo289.ReadOnly = true;
            clsColumnInfo289.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo290.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo290.BackColor = System.Drawing.Color.White;
            clsColumnInfo290.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Int;
            clsColumnInfo290.ColumnIndex = 9;
            clsColumnInfo290.ColumnName = "Column11";
            clsColumnInfo290.ColumnWidth = 0;
            clsColumnInfo290.Enabled = false;
            clsColumnInfo290.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo290.HeadText = "行号";
            clsColumnInfo290.ReadOnly = true;
            clsColumnInfo290.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo291.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo291.BackColor = System.Drawing.Color.White;
            clsColumnInfo291.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo291.ColumnIndex = 10;
            clsColumnInfo291.ColumnName = "Column12";
            clsColumnInfo291.ColumnWidth = 60;
            clsColumnInfo291.Enabled = false;
            clsColumnInfo291.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo291.HeadText = "比例";
            clsColumnInfo291.ReadOnly = true;
            clsColumnInfo291.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo475.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo475.BackColor = System.Drawing.Color.White;
            clsColumnInfo475.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo475.ColumnIndex = 11;
            clsColumnInfo475.ColumnName = "Column13";
            clsColumnInfo475.ColumnWidth = 0;
            clsColumnInfo475.Enabled = true;
            clsColumnInfo475.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo475.HeadText = "比例值";
            clsColumnInfo475.ReadOnly = true;
            clsColumnInfo475.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo476.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo476.BackColor = System.Drawing.Color.White;
            clsColumnInfo476.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo476.ColumnIndex = 12;
            clsColumnInfo476.ColumnName = "Column14";
            clsColumnInfo476.ColumnWidth = 0;
            clsColumnInfo476.Enabled = true;
            clsColumnInfo476.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo476.HeadText = "常量";
            clsColumnInfo476.ReadOnly = true;
            clsColumnInfo476.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo477.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo477.BackColor = System.Drawing.Color.White;
            clsColumnInfo477.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo477.ColumnIndex = 13;
            clsColumnInfo477.ColumnName = "Column15";
            clsColumnInfo477.ColumnWidth = 0;
            clsColumnInfo477.Enabled = true;
            clsColumnInfo477.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo477.HeadText = "上限";
            clsColumnInfo477.ReadOnly = true;
            clsColumnInfo477.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo478.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo478.BackColor = System.Drawing.Color.White;
            clsColumnInfo478.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo478.ColumnIndex = 14;
            clsColumnInfo478.ColumnName = "Column16";
            clsColumnInfo478.ColumnWidth = 0;
            clsColumnInfo478.Enabled = true;
            clsColumnInfo478.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo478.HeadText = "下限";
            clsColumnInfo478.ReadOnly = true;
            clsColumnInfo478.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo479.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo479.BackColor = System.Drawing.Color.White;
            clsColumnInfo479.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo479.ColumnIndex = 15;
            clsColumnInfo479.ColumnName = "Column17";
            clsColumnInfo479.ColumnWidth = 0;
            clsColumnInfo479.Enabled = true;
            clsColumnInfo479.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo479.HeadText = "总数";
            clsColumnInfo479.ReadOnly = true;
            clsColumnInfo479.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo480.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo480.BackColor = System.Drawing.Color.White;
            clsColumnInfo480.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo480.ColumnIndex = 16;
            clsColumnInfo480.ColumnName = "Column18";
            clsColumnInfo480.ColumnWidth = 0;
            clsColumnInfo480.Enabled = true;
            clsColumnInfo480.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo480.HeadText = "大单价";
            clsColumnInfo480.ReadOnly = true;
            clsColumnInfo480.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo481.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo481.BackColor = System.Drawing.Color.White;
            clsColumnInfo481.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Int;
            clsColumnInfo481.ColumnIndex = 17;
            clsColumnInfo481.ColumnName = "Column19";
            clsColumnInfo481.ColumnWidth = 0;
            clsColumnInfo481.Enabled = true;
            clsColumnInfo481.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo481.HeadText = "大小单位标志";
            clsColumnInfo481.ReadOnly = true;
            clsColumnInfo481.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo482.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo482.BackColor = System.Drawing.Color.White;
            clsColumnInfo482.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo482.ColumnIndex = 18;
            clsColumnInfo482.ColumnName = "Column20";
            clsColumnInfo482.ColumnWidth = 0;
            clsColumnInfo482.Enabled = true;
            clsColumnInfo482.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo482.HeadText = "包装量";
            clsColumnInfo482.ReadOnly = true;
            clsColumnInfo482.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo483.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo483.BackColor = System.Drawing.Color.White;
            clsColumnInfo483.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Int;
            clsColumnInfo483.ColumnIndex = 19;
            clsColumnInfo483.ColumnName = "Column21";
            clsColumnInfo483.ColumnWidth = 0;
            clsColumnInfo483.Enabled = false;
            clsColumnInfo483.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo483.HeadText = "颜色";
            clsColumnInfo483.ReadOnly = true;
            clsColumnInfo483.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo484.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo484.BackColor = System.Drawing.Color.White;
            clsColumnInfo484.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo484.ColumnIndex = 20;
            clsColumnInfo484.ColumnName = "Column22";
            clsColumnInfo484.ColumnWidth = 0;
            clsColumnInfo484.Enabled = false;
            clsColumnInfo484.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo484.HeadText = "发票分类";
            clsColumnInfo484.ReadOnly = true;
            clsColumnInfo484.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo485.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo485.BackColor = System.Drawing.Color.White;
            clsColumnInfo485.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo485.ColumnIndex = 21;
            clsColumnInfo485.ColumnName = "用法ID";
            clsColumnInfo485.ColumnWidth = 0;
            clsColumnInfo485.Enabled = false;
            clsColumnInfo485.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo485.HeadText = "用法ID";
            clsColumnInfo485.ReadOnly = true;
            clsColumnInfo485.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo486.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo486.BackColor = System.Drawing.Color.White;
            clsColumnInfo486.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo486.ColumnIndex = 22;
            clsColumnInfo486.ColumnName = "Column24";
            clsColumnInfo486.ColumnWidth = 0;
            clsColumnInfo486.Enabled = false;
            clsColumnInfo486.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo486.HeadText = "附加项目ID";
            clsColumnInfo486.ReadOnly = true;
            clsColumnInfo486.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo487.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo487.BackColor = System.Drawing.Color.White;
            clsColumnInfo487.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo487.ColumnIndex = 23;
            clsColumnInfo487.ColumnName = "Column25";
            clsColumnInfo487.ColumnWidth = 0;
            clsColumnInfo487.Enabled = false;
            clsColumnInfo487.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo487.HeadText = "附加项目原数量";
            clsColumnInfo487.ReadOnly = true;
            clsColumnInfo487.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo488.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo488.BackColor = System.Drawing.Color.White;
            clsColumnInfo488.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo488.ColumnIndex = 24;
            clsColumnInfo488.ColumnName = "Column26";
            clsColumnInfo488.ColumnWidth = 0;
            clsColumnInfo488.Enabled = false;
            clsColumnInfo488.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo488.HeadText = "英文名";
            clsColumnInfo488.ReadOnly = true;
            clsColumnInfo488.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo489.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo489.BackColor = System.Drawing.Color.White;
            clsColumnInfo489.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo489.ColumnIndex = 25;
            clsColumnInfo489.ColumnName = "Column27";
            clsColumnInfo489.ColumnWidth = 0;
            clsColumnInfo489.Enabled = false;
            clsColumnInfo489.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo489.HeadText = "关联项目ID";
            clsColumnInfo489.ReadOnly = true;
            clsColumnInfo489.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo490.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo490.BackColor = System.Drawing.Color.White;
            clsColumnInfo490.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo490.ColumnIndex = 26;
            clsColumnInfo490.ColumnName = "Column28";
            clsColumnInfo490.ColumnWidth = 0;
            clsColumnInfo490.Enabled = false;
            clsColumnInfo490.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo490.HeadText = "主项默认用量";
            clsColumnInfo490.ReadOnly = true;
            clsColumnInfo490.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo491.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            clsColumnInfo491.BackColor = System.Drawing.Color.White;
            clsColumnInfo491.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo491.ColumnIndex = 27;
            clsColumnInfo491.ColumnName = "Column29";
            clsColumnInfo491.ColumnWidth = 0;
            clsColumnInfo491.Enabled = true;
            clsColumnInfo491.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo491.HeadText = "        科备药";
            clsColumnInfo491.ReadOnly = false;
            clsColumnInfo491.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo492.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo492.BackColor = System.Drawing.Color.White;
            clsColumnInfo492.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo492.ColumnIndex = 28;
            clsColumnInfo492.ColumnName = "Column30";
            clsColumnInfo492.ColumnWidth = 0;
            clsColumnInfo492.Enabled = false;
            clsColumnInfo492.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo492.HeadText = "科备药ID";
            clsColumnInfo492.ReadOnly = true;
            clsColumnInfo492.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo493.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo493.BackColor = System.Drawing.Color.White;
            clsColumnInfo493.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo493.ColumnIndex = 29;
            clsColumnInfo493.ColumnName = "Column31";
            clsColumnInfo493.ColumnWidth = 0;
            clsColumnInfo493.Enabled = true;
            clsColumnInfo493.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo493.HeadText = "详细用法";
            clsColumnInfo493.ReadOnly = true;
            clsColumnInfo493.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo494.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo494.BackColor = System.Drawing.Color.White;
            clsColumnInfo494.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo494.ColumnIndex = 30;
            clsColumnInfo494.ColumnName = "Column32";
            clsColumnInfo494.ColumnWidth = 120;
            clsColumnInfo494.Enabled = false;
            clsColumnInfo494.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo494.HeadText = "总让利金额";
            clsColumnInfo494.ReadOnly = true;
            clsColumnInfo494.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo495.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo495.BackColor = System.Drawing.Color.White;
            clsColumnInfo495.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo495.ColumnIndex = 31;
            clsColumnInfo495.ColumnName = "Column33";
            clsColumnInfo495.ColumnWidth = 0;
            clsColumnInfo495.Enabled = false;
            clsColumnInfo495.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo495.HeadText = "最小让利金额";
            clsColumnInfo495.ReadOnly = true;
            clsColumnInfo495.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo496.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo496.BackColor = System.Drawing.Color.White;
            clsColumnInfo496.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo496.ColumnIndex = 32;
            clsColumnInfo496.ColumnName = "Column34";
            clsColumnInfo496.ColumnWidth = 0;
            clsColumnInfo496.Enabled = false;
            clsColumnInfo496.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo496.HeadText = "大批发单价";
            clsColumnInfo496.ReadOnly = true;
            clsColumnInfo496.TextFont = new System.Drawing.Font("宋体", 10F);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo281);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo282);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo283);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo284);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo285);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo286);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo287);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo288);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo289);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo290);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo291);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo475);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo476);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo477);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo478);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo479);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo480);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo481);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo482);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo483);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo484);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo485);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo486);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo487);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo488);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo489);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo490);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo491);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo492);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo493);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo494);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo495);
            this.ctlDataGrid2.Columns.Add(clsColumnInfo496);
            this.ctlDataGrid2.ContextMenuStrip = this.contextMenuStrip1;
            this.ctlDataGrid2.Font = new System.Drawing.Font("宋体", 11F);
            this.ctlDataGrid2.FullRowSelect = false;
            this.ctlDataGrid2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctlDataGrid2.Location = new System.Drawing.Point(0, 0);
            this.ctlDataGrid2.MultiSelect = true;
            this.ctlDataGrid2.Name = "ctlDataGrid2";
            this.ctlDataGrid2.ReadOnly = false;
            this.ctlDataGrid2.RowHeadersVisible = false;
            this.ctlDataGrid2.RowHeaderWidth = 35;
            this.ctlDataGrid2.SelectedRowBackColor = System.Drawing.Color.Navy;
            this.ctlDataGrid2.SelectedRowForeColor = System.Drawing.Color.White;
            this.ctlDataGrid2.Size = new System.Drawing.Size(842, 372);
            this.ctlDataGrid2.TabIndex = 35;
            this.ctlDataGrid2.m_evtDataGridTextBoxKeyDown += new com.digitalwave.controls.datagrid.clsDGTextKeyEventHandler(this.ctlDataGrid2_m_evtDataGridTextBoxKeyDown);
            this.ctlDataGrid2.m_evtCurrentCellChanged += new System.EventHandler(this.ctlDataGrid2_m_evtCurrentCellChanged);
            this.ctlDataGrid2.m_evtDataGridKeyDown += new System.Windows.Forms.KeyEventHandler(this.ctlDataGrid2_m_evtDataGridKeyDown);
            this.ctlDataGrid2.Leave += new System.EventHandler(this.ctlDataGrid2_Leave);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.listView5);
            this.tabPage7.Controls.Add(this.ctlDataGridLis);
            this.tabPage7.Controls.Add(this.ctlDataGrid3);
            this.tabPage7.Controls.Add(this.plLis);
            this.tabPage7.Location = new System.Drawing.Point(4, 27);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(842, 409);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "[5]检验";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // listView5
            // 
            this.listView5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView5.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader21,
            this.columnHeader22,
            this.columnHeader23});
            this.listView5.FullRowSelect = true;
            this.listView5.GridLines = true;
            this.listView5.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView5.Location = new System.Drawing.Point(304, 120);
            this.listView5.MultiSelect = false;
            this.listView5.Name = "listView5";
            this.listView5.Size = new System.Drawing.Size(208, 128);
            this.listView5.TabIndex = 36;
            this.listView5.UseCompatibleStateImageBehavior = false;
            this.listView5.View = System.Windows.Forms.View.Details;
            this.listView5.Visible = false;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "频率ID";
            this.columnHeader21.Width = 0;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "助记码";
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "名称";
            this.columnHeader23.Width = 120;
            // 
            // ctlDataGridLis
            // 
            this.ctlDataGridLis.AllowAddNew = true;
            this.ctlDataGridLis.AllowDelete = true;
            this.ctlDataGridLis.AutoAppendRow = false;
            this.ctlDataGridLis.AutoScroll = true;
            this.ctlDataGridLis.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ctlDataGridLis.CaptionText = "";
            this.ctlDataGridLis.CaptionVisible = false;
            this.ctlDataGridLis.ColumnHeadersVisible = true;
            clsColumnInfo12.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo12.BackColor = System.Drawing.Color.White;
            clsColumnInfo12.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo12.ColumnIndex = 0;
            clsColumnInfo12.ColumnName = "Column1";
            clsColumnInfo12.ColumnWidth = 75;
            clsColumnInfo12.Enabled = true;
            clsColumnInfo12.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo12.HeadText = "查询";
            clsColumnInfo12.ReadOnly = false;
            clsColumnInfo12.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo13.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo13.BackColor = System.Drawing.Color.White;
            clsColumnInfo13.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo13.ColumnIndex = 1;
            clsColumnInfo13.ColumnName = "Column2";
            clsColumnInfo13.ColumnWidth = 50;
            clsColumnInfo13.Enabled = true;
            clsColumnInfo13.ForeColor = System.Drawing.Color.Red;
            clsColumnInfo13.HeadText = "数量";
            clsColumnInfo13.ReadOnly = false;
            clsColumnInfo13.TextFont = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            clsColumnInfo14.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo14.BackColor = System.Drawing.Color.White;
            clsColumnInfo14.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo14.ColumnIndex = 2;
            clsColumnInfo14.ColumnName = "Column3";
            clsColumnInfo14.ColumnWidth = 200;
            clsColumnInfo14.Enabled = true;
            clsColumnInfo14.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo14.HeadText = "项目名称";
            clsColumnInfo14.ReadOnly = true;
            clsColumnInfo14.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo15.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo15.BackColor = System.Drawing.Color.White;
            clsColumnInfo15.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo15.ColumnIndex = 3;
            clsColumnInfo15.ColumnName = "Column4";
            clsColumnInfo15.ColumnWidth = 100;
            clsColumnInfo15.Enabled = false;
            clsColumnInfo15.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo15.HeadText = "规格";
            clsColumnInfo15.ReadOnly = true;
            clsColumnInfo15.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo16.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo16.BackColor = System.Drawing.Color.White;
            clsColumnInfo16.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo16.ColumnIndex = 4;
            clsColumnInfo16.ColumnName = "Column21";
            clsColumnInfo16.ColumnWidth = 75;
            clsColumnInfo16.Enabled = true;
            clsColumnInfo16.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo16.HeadText = "样本类型";
            clsColumnInfo16.ReadOnly = false;
            clsColumnInfo16.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo17.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo17.BackColor = System.Drawing.Color.White;
            clsColumnInfo17.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo17.ColumnIndex = 5;
            clsColumnInfo17.ColumnName = "Column5";
            clsColumnInfo17.ColumnWidth = 60;
            clsColumnInfo17.Enabled = true;
            clsColumnInfo17.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo17.HeadText = "单位";
            clsColumnInfo17.ReadOnly = true;
            clsColumnInfo17.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo18.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo18.BackColor = System.Drawing.Color.White;
            clsColumnInfo18.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo18.ColumnIndex = 6;
            clsColumnInfo18.ColumnName = "Column6";
            clsColumnInfo18.ColumnWidth = 65;
            clsColumnInfo18.Enabled = true;
            clsColumnInfo18.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo18.HeadText = "单价";
            clsColumnInfo18.ReadOnly = false;
            clsColumnInfo18.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo19.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo19.BackColor = System.Drawing.Color.White;
            clsColumnInfo19.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo19.ColumnIndex = 7;
            clsColumnInfo19.ColumnName = "Column7";
            clsColumnInfo19.ColumnWidth = 65;
            clsColumnInfo19.Enabled = true;
            clsColumnInfo19.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo19.HeadText = "总价";
            clsColumnInfo19.ReadOnly = true;
            clsColumnInfo19.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo20.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo20.BackColor = System.Drawing.Color.White;
            clsColumnInfo20.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo20.ColumnIndex = 8;
            clsColumnInfo20.ColumnName = "Column10";
            clsColumnInfo20.ColumnWidth = 0;
            clsColumnInfo20.Enabled = true;
            clsColumnInfo20.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo20.HeadText = "";
            clsColumnInfo20.ReadOnly = false;
            clsColumnInfo20.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo21.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo21.BackColor = System.Drawing.Color.White;
            clsColumnInfo21.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo21.ColumnIndex = 9;
            clsColumnInfo21.ColumnName = "Column11";
            clsColumnInfo21.ColumnWidth = 0;
            clsColumnInfo21.Enabled = true;
            clsColumnInfo21.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo21.HeadText = "";
            clsColumnInfo21.ReadOnly = true;
            clsColumnInfo21.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo22.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo22.BackColor = System.Drawing.Color.White;
            clsColumnInfo22.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Int;
            clsColumnInfo22.ColumnIndex = 10;
            clsColumnInfo22.ColumnName = "Column12";
            clsColumnInfo22.ColumnWidth = 0;
            clsColumnInfo22.Enabled = true;
            clsColumnInfo22.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo22.HeadText = "";
            clsColumnInfo22.ReadOnly = true;
            clsColumnInfo22.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo292.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo292.BackColor = System.Drawing.Color.White;
            clsColumnInfo292.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo292.ColumnIndex = 11;
            clsColumnInfo292.ColumnName = "Column13";
            clsColumnInfo292.ColumnWidth = 70;
            clsColumnInfo292.Enabled = true;
            clsColumnInfo292.ForeColor = System.Drawing.Color.Green;
            clsColumnInfo292.HeadText = "";
            clsColumnInfo292.ReadOnly = true;
            clsColumnInfo292.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo293.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo293.BackColor = System.Drawing.Color.White;
            clsColumnInfo293.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo293.ColumnIndex = 12;
            clsColumnInfo293.ColumnName = "Column14";
            clsColumnInfo293.ColumnWidth = 0;
            clsColumnInfo293.Enabled = true;
            clsColumnInfo293.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo293.HeadText = "";
            clsColumnInfo293.ReadOnly = true;
            clsColumnInfo293.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo294.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo294.BackColor = System.Drawing.Color.White;
            clsColumnInfo294.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo294.ColumnIndex = 13;
            clsColumnInfo294.ColumnName = "Column15";
            clsColumnInfo294.ColumnWidth = 0;
            clsColumnInfo294.Enabled = false;
            clsColumnInfo294.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo294.HeadText = "";
            clsColumnInfo294.ReadOnly = true;
            clsColumnInfo294.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo295.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo295.BackColor = System.Drawing.Color.White;
            clsColumnInfo295.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo295.ColumnIndex = 14;
            clsColumnInfo295.ColumnName = "Column16";
            clsColumnInfo295.ColumnWidth = 0;
            clsColumnInfo295.Enabled = false;
            clsColumnInfo295.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo295.HeadText = "";
            clsColumnInfo295.ReadOnly = true;
            clsColumnInfo295.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo296.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo296.BackColor = System.Drawing.Color.White;
            clsColumnInfo296.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo296.ColumnIndex = 15;
            clsColumnInfo296.ColumnName = "Column17";
            clsColumnInfo296.ColumnWidth = 0;
            clsColumnInfo296.Enabled = false;
            clsColumnInfo296.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo296.HeadText = "";
            clsColumnInfo296.ReadOnly = true;
            clsColumnInfo296.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo297.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo297.BackColor = System.Drawing.Color.White;
            clsColumnInfo297.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo297.ColumnIndex = 16;
            clsColumnInfo297.ColumnName = "Column18";
            clsColumnInfo297.ColumnWidth = 0;
            clsColumnInfo297.Enabled = false;
            clsColumnInfo297.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo297.HeadText = "";
            clsColumnInfo297.ReadOnly = true;
            clsColumnInfo297.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo298.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo298.BackColor = System.Drawing.Color.White;
            clsColumnInfo298.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo298.ColumnIndex = 17;
            clsColumnInfo298.ColumnName = "Column19";
            clsColumnInfo298.ColumnWidth = 0;
            clsColumnInfo298.Enabled = false;
            clsColumnInfo298.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo298.HeadText = "";
            clsColumnInfo298.ReadOnly = true;
            clsColumnInfo298.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo299.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo299.BackColor = System.Drawing.Color.White;
            clsColumnInfo299.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo299.ColumnIndex = 18;
            clsColumnInfo299.ColumnName = "Column20";
            clsColumnInfo299.ColumnWidth = 0;
            clsColumnInfo299.Enabled = false;
            clsColumnInfo299.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo299.HeadText = "";
            clsColumnInfo299.ReadOnly = true;
            clsColumnInfo299.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo300.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo300.BackColor = System.Drawing.Color.White;
            clsColumnInfo300.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo300.ColumnIndex = 19;
            clsColumnInfo300.ColumnName = "Column22";
            clsColumnInfo300.ColumnWidth = 0;
            clsColumnInfo300.Enabled = false;
            clsColumnInfo300.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo300.HeadText = "";
            clsColumnInfo300.ReadOnly = true;
            clsColumnInfo300.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo301.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo301.BackColor = System.Drawing.Color.White;
            clsColumnInfo301.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo301.ColumnIndex = 20;
            clsColumnInfo301.ColumnName = "Column23";
            clsColumnInfo301.ColumnWidth = 0;
            clsColumnInfo301.Enabled = false;
            clsColumnInfo301.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo301.HeadText = "";
            clsColumnInfo301.ReadOnly = true;
            clsColumnInfo301.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo302.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            clsColumnInfo302.BackColor = System.Drawing.Color.White;
            clsColumnInfo302.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo302.ColumnIndex = 21;
            clsColumnInfo302.ColumnName = "Column24";
            clsColumnInfo302.ColumnWidth = 50;
            clsColumnInfo302.Enabled = true;
            clsColumnInfo302.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo302.HeadText = "急查";
            clsColumnInfo302.ReadOnly = false;
            clsColumnInfo302.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo303.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo303.BackColor = System.Drawing.Color.White;
            clsColumnInfo303.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo303.ColumnIndex = 22;
            clsColumnInfo303.ColumnName = "Column25";
            clsColumnInfo303.ColumnWidth = 0;
            clsColumnInfo303.Enabled = false;
            clsColumnInfo303.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo303.HeadText = "";
            clsColumnInfo303.ReadOnly = true;
            clsColumnInfo303.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo304.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo304.BackColor = System.Drawing.Color.White;
            clsColumnInfo304.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo304.ColumnIndex = 23;
            clsColumnInfo304.ColumnName = "Column26";
            clsColumnInfo304.ColumnWidth = 0;
            clsColumnInfo304.Enabled = false;
            clsColumnInfo304.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo304.HeadText = "";
            clsColumnInfo304.ReadOnly = true;
            clsColumnInfo304.TextFont = new System.Drawing.Font("宋体", 10F);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo12);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo13);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo14);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo15);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo16);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo17);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo18);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo19);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo20);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo21);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo22);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo292);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo293);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo294);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo295);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo296);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo297);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo298);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo299);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo300);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo301);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo302);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo303);
            this.ctlDataGridLis.Columns.Add(clsColumnInfo304);
            this.ctlDataGridLis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlDataGridLis.Font = new System.Drawing.Font("宋体", 11F);
            this.ctlDataGridLis.FullRowSelect = false;
            this.ctlDataGridLis.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctlDataGridLis.Location = new System.Drawing.Point(0, 0);
            this.ctlDataGridLis.MultiSelect = false;
            this.ctlDataGridLis.Name = "ctlDataGridLis";
            this.ctlDataGridLis.ReadOnly = false;
            this.ctlDataGridLis.RowHeadersVisible = false;
            this.ctlDataGridLis.RowHeaderWidth = 35;
            this.ctlDataGridLis.SelectedRowBackColor = System.Drawing.Color.Navy;
            this.ctlDataGridLis.SelectedRowForeColor = System.Drawing.Color.White;
            this.ctlDataGridLis.Size = new System.Drawing.Size(842, 409);
            this.ctlDataGridLis.TabIndex = 37;
            this.ctlDataGridLis.m_evtDataGridTextBoxKeyDown += new com.digitalwave.controls.datagrid.clsDGTextKeyEventHandler(this.ctlDataGridLis_m_evtDataGridTextBoxKeyDown);
            this.ctlDataGridLis.m_evtCurrentCellChanged += new System.EventHandler(this.ctlDataGridLis_m_evtCurrentCellChanged);
            this.ctlDataGridLis.m_evtDoubleClickCell += new com.digitalwave.controls.datagrid.clsDGTextMouseClickEventHandler(this.ctlDataGridLis_m_evtDoubleClickCell);
            this.ctlDataGridLis.m_evtDataGridKeyDown += new System.Windows.Forms.KeyEventHandler(this.ctlDataGridLis_m_evtDataGridKeyDown);
            this.ctlDataGridLis.Leave += new System.EventHandler(this.ctlDataGridLis_Leave);
            // 
            // ctlDataGrid3
            // 
            this.ctlDataGrid3.AllowAddNew = true;
            this.ctlDataGrid3.AllowDelete = true;
            this.ctlDataGrid3.AutoAppendRow = false;
            this.ctlDataGrid3.AutoScroll = true;
            this.ctlDataGrid3.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ctlDataGrid3.CaptionText = "";
            this.ctlDataGrid3.CaptionVisible = false;
            this.ctlDataGrid3.ColumnHeadersVisible = true;
            clsColumnInfo305.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo305.BackColor = System.Drawing.Color.White;
            clsColumnInfo305.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo305.ColumnIndex = 0;
            clsColumnInfo305.ColumnName = "Column1";
            clsColumnInfo305.ColumnWidth = 75;
            clsColumnInfo305.Enabled = true;
            clsColumnInfo305.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo305.HeadText = "查询";
            clsColumnInfo305.ReadOnly = false;
            clsColumnInfo305.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo306.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo306.BackColor = System.Drawing.Color.White;
            clsColumnInfo306.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo306.ColumnIndex = 1;
            clsColumnInfo306.ColumnName = "Column2";
            clsColumnInfo306.ColumnWidth = 50;
            clsColumnInfo306.Enabled = true;
            clsColumnInfo306.ForeColor = System.Drawing.Color.Red;
            clsColumnInfo306.HeadText = "数量";
            clsColumnInfo306.ReadOnly = false;
            clsColumnInfo306.TextFont = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            clsColumnInfo307.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo307.BackColor = System.Drawing.Color.White;
            clsColumnInfo307.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo307.ColumnIndex = 2;
            clsColumnInfo307.ColumnName = "Column3";
            clsColumnInfo307.ColumnWidth = 200;
            clsColumnInfo307.Enabled = true;
            clsColumnInfo307.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo307.HeadText = "项目名称";
            clsColumnInfo307.ReadOnly = true;
            clsColumnInfo307.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo308.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo308.BackColor = System.Drawing.Color.White;
            clsColumnInfo308.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo308.ColumnIndex = 3;
            clsColumnInfo308.ColumnName = "Column4";
            clsColumnInfo308.ColumnWidth = 100;
            clsColumnInfo308.Enabled = false;
            clsColumnInfo308.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo308.HeadText = "规格";
            clsColumnInfo308.ReadOnly = true;
            clsColumnInfo308.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo309.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo309.BackColor = System.Drawing.Color.White;
            clsColumnInfo309.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo309.ColumnIndex = 4;
            clsColumnInfo309.ColumnName = "Column21";
            clsColumnInfo309.ColumnWidth = 75;
            clsColumnInfo309.Enabled = true;
            clsColumnInfo309.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo309.HeadText = "样本类型";
            clsColumnInfo309.ReadOnly = false;
            clsColumnInfo309.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo310.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo310.BackColor = System.Drawing.Color.White;
            clsColumnInfo310.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo310.ColumnIndex = 5;
            clsColumnInfo310.ColumnName = "Column5";
            clsColumnInfo310.ColumnWidth = 60;
            clsColumnInfo310.Enabled = true;
            clsColumnInfo310.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo310.HeadText = "单位";
            clsColumnInfo310.ReadOnly = true;
            clsColumnInfo310.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo311.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo311.BackColor = System.Drawing.Color.White;
            clsColumnInfo311.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo311.ColumnIndex = 6;
            clsColumnInfo311.ColumnName = "Column6";
            clsColumnInfo311.ColumnWidth = 65;
            clsColumnInfo311.Enabled = true;
            clsColumnInfo311.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo311.HeadText = "单价";
            clsColumnInfo311.ReadOnly = false;
            clsColumnInfo311.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo312.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo312.BackColor = System.Drawing.Color.White;
            clsColumnInfo312.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo312.ColumnIndex = 7;
            clsColumnInfo312.ColumnName = "Column7";
            clsColumnInfo312.ColumnWidth = 65;
            clsColumnInfo312.Enabled = true;
            clsColumnInfo312.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo312.HeadText = "总价";
            clsColumnInfo312.ReadOnly = true;
            clsColumnInfo312.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo313.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo313.BackColor = System.Drawing.Color.White;
            clsColumnInfo313.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo313.ColumnIndex = 8;
            clsColumnInfo313.ColumnName = "Column10";
            clsColumnInfo313.ColumnWidth = 0;
            clsColumnInfo313.Enabled = true;
            clsColumnInfo313.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo313.HeadText = "ID";
            clsColumnInfo313.ReadOnly = false;
            clsColumnInfo313.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo497.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo497.BackColor = System.Drawing.Color.White;
            clsColumnInfo497.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo497.ColumnIndex = 9;
            clsColumnInfo497.ColumnName = "Column11";
            clsColumnInfo497.ColumnWidth = 0;
            clsColumnInfo497.Enabled = true;
            clsColumnInfo497.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo497.HeadText = "是否自定义价格";
            clsColumnInfo497.ReadOnly = true;
            clsColumnInfo497.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo498.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo498.BackColor = System.Drawing.Color.White;
            clsColumnInfo498.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Int;
            clsColumnInfo498.ColumnIndex = 10;
            clsColumnInfo498.ColumnName = "Column12";
            clsColumnInfo498.ColumnWidth = 0;
            clsColumnInfo498.Enabled = true;
            clsColumnInfo498.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo498.HeadText = "行号";
            clsColumnInfo498.ReadOnly = true;
            clsColumnInfo498.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo499.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo499.BackColor = System.Drawing.Color.White;
            clsColumnInfo499.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo499.ColumnIndex = 11;
            clsColumnInfo499.ColumnName = "Column13";
            clsColumnInfo499.ColumnWidth = 70;
            clsColumnInfo499.Enabled = true;
            clsColumnInfo499.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo499.HeadText = "收费比例";
            clsColumnInfo499.ReadOnly = true;
            clsColumnInfo499.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo500.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo500.BackColor = System.Drawing.Color.White;
            clsColumnInfo500.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo500.ColumnIndex = 12;
            clsColumnInfo500.ColumnName = "Column14";
            clsColumnInfo500.ColumnWidth = 0;
            clsColumnInfo500.Enabled = true;
            clsColumnInfo500.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo500.HeadText = "比例值";
            clsColumnInfo500.ReadOnly = true;
            clsColumnInfo500.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo501.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo501.BackColor = System.Drawing.Color.White;
            clsColumnInfo501.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo501.ColumnIndex = 13;
            clsColumnInfo501.ColumnName = "Column15";
            clsColumnInfo501.ColumnWidth = 0;
            clsColumnInfo501.Enabled = false;
            clsColumnInfo501.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo501.HeadText = "发票分类";
            clsColumnInfo501.ReadOnly = true;
            clsColumnInfo501.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo502.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo502.BackColor = System.Drawing.Color.White;
            clsColumnInfo502.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo502.ColumnIndex = 14;
            clsColumnInfo502.ColumnName = "Column16";
            clsColumnInfo502.ColumnWidth = 0;
            clsColumnInfo502.Enabled = false;
            clsColumnInfo502.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo502.HeadText = "附加项目ID";
            clsColumnInfo502.ReadOnly = true;
            clsColumnInfo502.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo503.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo503.BackColor = System.Drawing.Color.White;
            clsColumnInfo503.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo503.ColumnIndex = 15;
            clsColumnInfo503.ColumnName = "Column17";
            clsColumnInfo503.ColumnWidth = 0;
            clsColumnInfo503.Enabled = false;
            clsColumnInfo503.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo503.HeadText = "附加项目原数量";
            clsColumnInfo503.ReadOnly = true;
            clsColumnInfo503.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo504.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo504.BackColor = System.Drawing.Color.White;
            clsColumnInfo504.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo504.ColumnIndex = 16;
            clsColumnInfo504.ColumnName = "Column18";
            clsColumnInfo504.ColumnWidth = 0;
            clsColumnInfo504.Enabled = false;
            clsColumnInfo504.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo504.HeadText = "英文名";
            clsColumnInfo504.ReadOnly = true;
            clsColumnInfo504.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo505.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo505.BackColor = System.Drawing.Color.White;
            clsColumnInfo505.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo505.ColumnIndex = 17;
            clsColumnInfo505.ColumnName = "Column19";
            clsColumnInfo505.ColumnWidth = 0;
            clsColumnInfo505.Enabled = false;
            clsColumnInfo505.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo505.HeadText = "样本ID";
            clsColumnInfo505.ReadOnly = true;
            clsColumnInfo505.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo689.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo689.BackColor = System.Drawing.Color.White;
            clsColumnInfo689.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo689.ColumnIndex = 18;
            clsColumnInfo689.ColumnName = "Column20";
            clsColumnInfo689.ColumnWidth = 0;
            clsColumnInfo689.Enabled = false;
            clsColumnInfo689.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo689.HeadText = "申请单ID";
            clsColumnInfo689.ReadOnly = true;
            clsColumnInfo689.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo690.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo690.BackColor = System.Drawing.Color.White;
            clsColumnInfo690.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo690.ColumnIndex = 19;
            clsColumnInfo690.ColumnName = "Column22";
            clsColumnInfo690.ColumnWidth = 0;
            clsColumnInfo690.Enabled = false;
            clsColumnInfo690.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo690.HeadText = "关联项目ID";
            clsColumnInfo690.ReadOnly = true;
            clsColumnInfo690.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo691.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo691.BackColor = System.Drawing.Color.White;
            clsColumnInfo691.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo691.ColumnIndex = 20;
            clsColumnInfo691.ColumnName = "Column23";
            clsColumnInfo691.ColumnWidth = 0;
            clsColumnInfo691.Enabled = false;
            clsColumnInfo691.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo691.HeadText = "主项默认用量";
            clsColumnInfo691.ReadOnly = true;
            clsColumnInfo691.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo692.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            clsColumnInfo692.BackColor = System.Drawing.Color.White;
            clsColumnInfo692.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo692.ColumnIndex = 21;
            clsColumnInfo692.ColumnName = "Column24";
            clsColumnInfo692.ColumnWidth = 50;
            clsColumnInfo692.Enabled = true;
            clsColumnInfo692.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo692.HeadText = "速诊";
            clsColumnInfo692.ReadOnly = false;
            clsColumnInfo692.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo693.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo693.BackColor = System.Drawing.Color.White;
            clsColumnInfo693.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo693.ColumnIndex = 22;
            clsColumnInfo693.ColumnName = "Column25";
            clsColumnInfo693.ColumnWidth = 0;
            clsColumnInfo693.Enabled = false;
            clsColumnInfo693.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo693.HeadText = "速诊ID";
            clsColumnInfo693.ReadOnly = true;
            clsColumnInfo693.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo694.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo694.BackColor = System.Drawing.Color.White;
            clsColumnInfo694.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo694.ColumnIndex = 23;
            clsColumnInfo694.ColumnName = "Column26";
            clsColumnInfo694.ColumnWidth = 0;
            clsColumnInfo694.Enabled = false;
            clsColumnInfo694.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo694.HeadText = "详细用法";
            clsColumnInfo694.ReadOnly = true;
            clsColumnInfo694.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo695.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo695.BackColor = System.Drawing.Color.White;
            clsColumnInfo695.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo695.ColumnIndex = 24;
            clsColumnInfo695.ColumnName = "Column27";
            clsColumnInfo695.ColumnWidth = 0;
            clsColumnInfo695.Enabled = false;
            clsColumnInfo695.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo695.HeadText = "主诊疗项目ID";
            clsColumnInfo695.ReadOnly = true;
            clsColumnInfo695.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo696.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo696.BackColor = System.Drawing.Color.White;
            clsColumnInfo696.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo696.ColumnIndex = 25;
            clsColumnInfo696.ColumnName = "Column28";
            clsColumnInfo696.ColumnWidth = 0;
            clsColumnInfo696.Enabled = false;
            clsColumnInfo696.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo696.HeadText = "主诊疗项目原数量";
            clsColumnInfo696.ReadOnly = true;
            clsColumnInfo696.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo697.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo697.BackColor = System.Drawing.Color.White;
            clsColumnInfo697.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo697.ColumnIndex = 26;
            clsColumnInfo697.ColumnName = "Column29";
            clsColumnInfo697.ColumnWidth = 0;
            clsColumnInfo697.Enabled = false;
            clsColumnInfo697.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo697.HeadText = "法定打折比例";
            clsColumnInfo697.ReadOnly = true;
            clsColumnInfo697.TextFont = new System.Drawing.Font("宋体", 10F);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo305);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo306);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo307);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo308);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo309);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo310);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo311);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo312);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo313);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo497);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo498);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo499);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo500);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo501);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo502);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo503);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo504);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo505);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo689);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo690);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo691);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo692);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo693);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo694);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo695);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo696);
            this.ctlDataGrid3.Columns.Add(clsColumnInfo697);
            this.ctlDataGrid3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlDataGrid3.Font = new System.Drawing.Font("宋体", 11F);
            this.ctlDataGrid3.FullRowSelect = false;
            this.ctlDataGrid3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctlDataGrid3.Location = new System.Drawing.Point(0, 0);
            this.ctlDataGrid3.MultiSelect = false;
            this.ctlDataGrid3.Name = "ctlDataGrid3";
            this.ctlDataGrid3.ReadOnly = false;
            this.ctlDataGrid3.RowHeadersVisible = false;
            this.ctlDataGrid3.RowHeaderWidth = 35;
            this.ctlDataGrid3.SelectedRowBackColor = System.Drawing.Color.Navy;
            this.ctlDataGrid3.SelectedRowForeColor = System.Drawing.Color.White;
            this.ctlDataGrid3.Size = new System.Drawing.Size(842, 409);
            this.ctlDataGrid3.TabIndex = 35;
            this.ctlDataGrid3.m_evtDataGridTextBoxKeyDown += new com.digitalwave.controls.datagrid.clsDGTextKeyEventHandler(this.ctlDataGrid3_m_evtDataGridTextBoxKeyDown);
            this.ctlDataGrid3.m_evtCurrentCellChanged += new System.EventHandler(this.ctlDataGrid3_m_evtCurrentCellChanged);
            this.ctlDataGrid3.m_evtDoubleClickCell += new com.digitalwave.controls.datagrid.clsDGTextMouseClickEventHandler(this.ctlDataGrid3_m_evtDoubleClickCell);
            this.ctlDataGrid3.m_evtDataGridKeyDown += new System.Windows.Forms.KeyEventHandler(this.ctlDataGrid3_m_evtDataGridKeyDown);
            this.ctlDataGrid3.Leave += new System.EventHandler(this.ctlDataGrid3_Leave);
            // 
            // plLis
            // 
            this.plLis.AutoScroll = true;
            this.plLis.BackColor = System.Drawing.Color.White;
            this.plLis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plLis.Controls.Add(this.panel5);
            this.plLis.Controls.Add(this.panel6);
            this.plLis.Dock = System.Windows.Forms.DockStyle.Right;
            this.plLis.Location = new System.Drawing.Point(842, 0);
            this.plLis.Name = "plLis";
            this.plLis.Size = new System.Drawing.Size(0, 409);
            this.plLis.TabIndex = 38;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label18);
            this.panel5.Controls.Add(this.lvLis);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 28);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(0, 381);
            this.panel5.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(0, 16);
            this.label18.TabIndex = 3;
            this.label18.Text = "序号  名称                     单价    数量      金额   自付  自付金额";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvLis
            // 
            this.lvLis.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvLis.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader28,
            this.columnHeader29,
            this.columnHeader30,
            this.columnHeader31,
            this.columnHeader32,
            this.columnHeader48,
            this.columnHeader51});
            this.lvLis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLis.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvLis.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lvLis.Location = new System.Drawing.Point(0, 0);
            this.lvLis.MultiSelect = false;
            this.lvLis.Name = "lvLis";
            this.lvLis.Size = new System.Drawing.Size(0, 381);
            this.lvLis.TabIndex = 2;
            this.lvLis.UseCompatibleStateImageBehavior = false;
            this.lvLis.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader28
            // 
            this.columnHeader28.Width = 28;
            // 
            // columnHeader29
            // 
            this.columnHeader29.Width = 137;
            // 
            // columnHeader30
            // 
            this.columnHeader30.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader30.Width = 54;
            // 
            // columnHeader31
            // 
            this.columnHeader31.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader31.Width = 55;
            // 
            // columnHeader32
            // 
            this.columnHeader32.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader32.Width = 64;
            // 
            // columnHeader48
            // 
            this.columnHeader48.Width = 40;
            // 
            // columnHeader51
            // 
            this.columnHeader51.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label17);
            this.panel6.Controls.Add(this.pictureBox2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(0, 28);
            this.panel6.TabIndex = 5;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label17.Location = new System.Drawing.Point(32, 8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 1;
            this.label17.Text = "费用明细";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(8, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.plTest);
            this.tabPage8.Controls.Add(this.panel7);
            this.tabPage8.Controls.Add(this.ctlDataGridTest);
            this.tabPage8.Controls.Add(this.ctlDataGrid4);
            this.tabPage8.Location = new System.Drawing.Point(4, 27);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(842, 409);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "[6]检查";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // plTest
            // 
            this.plTest.AutoScroll = true;
            this.plTest.BackColor = System.Drawing.Color.White;
            this.plTest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plTest.Controls.Add(this.panel12);
            this.plTest.Controls.Add(this.panel13);
            this.plTest.Dock = System.Windows.Forms.DockStyle.Right;
            this.plTest.Location = new System.Drawing.Point(842, 0);
            this.plTest.Name = "plTest";
            this.plTest.Size = new System.Drawing.Size(0, 409);
            this.plTest.TabIndex = 40;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.label20);
            this.panel12.Controls.Add(this.lvTest);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 28);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(0, 381);
            this.panel12.TabIndex = 4;
            // 
            // label20
            // 
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(0, 16);
            this.label20.TabIndex = 3;
            this.label20.Text = "序号  名称                     单价    数量      金额   自付  自付金额";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvTest
            // 
            this.lvTest.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvTest.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader43,
            this.columnHeader44,
            this.columnHeader45,
            this.columnHeader46,
            this.columnHeader47,
            this.columnHeader49,
            this.columnHeader52});
            this.lvTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTest.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvTest.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lvTest.Location = new System.Drawing.Point(0, 0);
            this.lvTest.MultiSelect = false;
            this.lvTest.Name = "lvTest";
            this.lvTest.Size = new System.Drawing.Size(0, 381);
            this.lvTest.TabIndex = 2;
            this.lvTest.UseCompatibleStateImageBehavior = false;
            this.lvTest.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader43
            // 
            this.columnHeader43.Width = 28;
            // 
            // columnHeader44
            // 
            this.columnHeader44.Width = 137;
            // 
            // columnHeader45
            // 
            this.columnHeader45.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader45.Width = 54;
            // 
            // columnHeader46
            // 
            this.columnHeader46.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader46.Width = 55;
            // 
            // columnHeader47
            // 
            this.columnHeader47.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader47.Width = 64;
            // 
            // columnHeader49
            // 
            this.columnHeader49.Width = 40;
            // 
            // columnHeader52
            // 
            this.columnHeader52.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.label28);
            this.panel13.Controls.Add(this.pictureBox3);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Location = new System.Drawing.Point(0, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(0, 28);
            this.panel13.TabIndex = 5;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label28.Location = new System.Drawing.Point(32, 8);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(53, 12);
            this.label28.TabIndex = 1;
            this.label28.Text = "费用明细";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(8, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // panel7
            // 
            this.panel7.AutoScroll = true;
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(842, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(0, 409);
            this.panel7.TabIndex = 39;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label19);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(0, 409);
            this.panel8.TabIndex = 4;
            // 
            // label19
            // 
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label19.Location = new System.Drawing.Point(0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(0, 16);
            this.label19.TabIndex = 3;
            this.label19.Text = "序号  名称                     单价    数量      金额";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlDataGridTest
            // 
            this.ctlDataGridTest.AllowAddNew = true;
            this.ctlDataGridTest.AllowDelete = true;
            this.ctlDataGridTest.AutoAppendRow = false;
            this.ctlDataGridTest.AutoScroll = true;
            this.ctlDataGridTest.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ctlDataGridTest.CaptionText = "";
            this.ctlDataGridTest.CaptionVisible = false;
            this.ctlDataGridTest.ColumnHeadersVisible = true;
            clsColumnInfo698.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo698.BackColor = System.Drawing.Color.White;
            clsColumnInfo698.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo698.ColumnIndex = 0;
            clsColumnInfo698.ColumnName = "Column1";
            clsColumnInfo698.ColumnWidth = 75;
            clsColumnInfo698.Enabled = true;
            clsColumnInfo698.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo698.HeadText = "查询";
            clsColumnInfo698.ReadOnly = false;
            clsColumnInfo698.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo699.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo699.BackColor = System.Drawing.Color.White;
            clsColumnInfo699.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo699.ColumnIndex = 1;
            clsColumnInfo699.ColumnName = "Column2";
            clsColumnInfo699.ColumnWidth = 60;
            clsColumnInfo699.Enabled = true;
            clsColumnInfo699.ForeColor = System.Drawing.Color.Red;
            clsColumnInfo699.HeadText = "数量";
            clsColumnInfo699.ReadOnly = false;
            clsColumnInfo699.TextFont = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            clsColumnInfo700.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo700.BackColor = System.Drawing.Color.White;
            clsColumnInfo700.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo700.ColumnIndex = 2;
            clsColumnInfo700.ColumnName = "Column3";
            clsColumnInfo700.ColumnWidth = 225;
            clsColumnInfo700.Enabled = true;
            clsColumnInfo700.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo700.HeadText = "项目名称";
            clsColumnInfo700.ReadOnly = true;
            clsColumnInfo700.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo701.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo701.BackColor = System.Drawing.Color.White;
            clsColumnInfo701.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo701.ColumnIndex = 3;
            clsColumnInfo701.ColumnName = "Column4";
            clsColumnInfo701.ColumnWidth = 110;
            clsColumnInfo701.Enabled = true;
            clsColumnInfo701.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo701.HeadText = "规格";
            clsColumnInfo701.ReadOnly = true;
            clsColumnInfo701.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo702.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo702.BackColor = System.Drawing.Color.White;
            clsColumnInfo702.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo702.ColumnIndex = 4;
            clsColumnInfo702.ColumnName = "Column21";
            clsColumnInfo702.ColumnWidth = 75;
            clsColumnInfo702.Enabled = true;
            clsColumnInfo702.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo702.HeadText = "检查部位";
            clsColumnInfo702.ReadOnly = false;
            clsColumnInfo702.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo703.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo703.BackColor = System.Drawing.Color.White;
            clsColumnInfo703.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo703.ColumnIndex = 5;
            clsColumnInfo703.ColumnName = "Column5";
            clsColumnInfo703.ColumnWidth = 60;
            clsColumnInfo703.Enabled = true;
            clsColumnInfo703.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo703.HeadText = "单位";
            clsColumnInfo703.ReadOnly = true;
            clsColumnInfo703.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo704.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo704.BackColor = System.Drawing.Color.White;
            clsColumnInfo704.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo704.ColumnIndex = 6;
            clsColumnInfo704.ColumnName = "Column6";
            clsColumnInfo704.ColumnWidth = 65;
            clsColumnInfo704.Enabled = true;
            clsColumnInfo704.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo704.HeadText = "单价";
            clsColumnInfo704.ReadOnly = false;
            clsColumnInfo704.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo705.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo705.BackColor = System.Drawing.Color.White;
            clsColumnInfo705.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo705.ColumnIndex = 7;
            clsColumnInfo705.ColumnName = "Column7";
            clsColumnInfo705.ColumnWidth = 65;
            clsColumnInfo705.Enabled = true;
            clsColumnInfo705.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo705.HeadText = "总价";
            clsColumnInfo705.ReadOnly = true;
            clsColumnInfo705.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo706.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo706.BackColor = System.Drawing.Color.White;
            clsColumnInfo706.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo706.ColumnIndex = 8;
            clsColumnInfo706.ColumnName = "Column10";
            clsColumnInfo706.ColumnWidth = 0;
            clsColumnInfo706.Enabled = true;
            clsColumnInfo706.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo706.HeadText = "ID";
            clsColumnInfo706.ReadOnly = false;
            clsColumnInfo706.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo707.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo707.BackColor = System.Drawing.Color.White;
            clsColumnInfo707.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo707.ColumnIndex = 9;
            clsColumnInfo707.ColumnName = "Column11";
            clsColumnInfo707.ColumnWidth = 0;
            clsColumnInfo707.Enabled = true;
            clsColumnInfo707.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo707.HeadText = "是否自定义价格";
            clsColumnInfo707.ReadOnly = true;
            clsColumnInfo707.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo708.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo708.BackColor = System.Drawing.Color.White;
            clsColumnInfo708.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Int;
            clsColumnInfo708.ColumnIndex = 10;
            clsColumnInfo708.ColumnName = "Column12";
            clsColumnInfo708.ColumnWidth = 0;
            clsColumnInfo708.Enabled = true;
            clsColumnInfo708.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo708.HeadText = "行号";
            clsColumnInfo708.ReadOnly = true;
            clsColumnInfo708.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo709.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo709.BackColor = System.Drawing.Color.White;
            clsColumnInfo709.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo709.ColumnIndex = 11;
            clsColumnInfo709.ColumnName = "Column13";
            clsColumnInfo709.ColumnWidth = 75;
            clsColumnInfo709.Enabled = true;
            clsColumnInfo709.ForeColor = System.Drawing.Color.Green;
            clsColumnInfo709.HeadText = "收费比例";
            clsColumnInfo709.ReadOnly = true;
            clsColumnInfo709.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo710.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo710.BackColor = System.Drawing.Color.White;
            clsColumnInfo710.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo710.ColumnIndex = 12;
            clsColumnInfo710.ColumnName = "Column14";
            clsColumnInfo710.ColumnWidth = 0;
            clsColumnInfo710.Enabled = true;
            clsColumnInfo710.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo710.HeadText = "比例值";
            clsColumnInfo710.ReadOnly = true;
            clsColumnInfo710.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo711.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo711.BackColor = System.Drawing.Color.White;
            clsColumnInfo711.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo711.ColumnIndex = 13;
            clsColumnInfo711.ColumnName = "Column15";
            clsColumnInfo711.ColumnWidth = 0;
            clsColumnInfo711.Enabled = false;
            clsColumnInfo711.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo711.HeadText = "发票分类";
            clsColumnInfo711.ReadOnly = true;
            clsColumnInfo711.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo712.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo712.BackColor = System.Drawing.Color.White;
            clsColumnInfo712.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo712.ColumnIndex = 14;
            clsColumnInfo712.ColumnName = "Column16";
            clsColumnInfo712.ColumnWidth = 0;
            clsColumnInfo712.Enabled = false;
            clsColumnInfo712.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo712.HeadText = "附加项目ID";
            clsColumnInfo712.ReadOnly = true;
            clsColumnInfo712.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo713.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo713.BackColor = System.Drawing.Color.White;
            clsColumnInfo713.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo713.ColumnIndex = 15;
            clsColumnInfo713.ColumnName = "Column17";
            clsColumnInfo713.ColumnWidth = 0;
            clsColumnInfo713.Enabled = false;
            clsColumnInfo713.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo713.HeadText = "附加项目原数量";
            clsColumnInfo713.ReadOnly = true;
            clsColumnInfo713.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo714.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo714.BackColor = System.Drawing.Color.White;
            clsColumnInfo714.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo714.ColumnIndex = 16;
            clsColumnInfo714.ColumnName = "Column18";
            clsColumnInfo714.ColumnWidth = 0;
            clsColumnInfo714.Enabled = false;
            clsColumnInfo714.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo714.HeadText = "英文名";
            clsColumnInfo714.ReadOnly = true;
            clsColumnInfo714.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo715.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo715.BackColor = System.Drawing.Color.White;
            clsColumnInfo715.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo715.ColumnIndex = 17;
            clsColumnInfo715.ColumnName = "Column19";
            clsColumnInfo715.ColumnWidth = 0;
            clsColumnInfo715.Enabled = false;
            clsColumnInfo715.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo715.HeadText = "预留";
            clsColumnInfo715.ReadOnly = true;
            clsColumnInfo715.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo716.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo716.BackColor = System.Drawing.Color.White;
            clsColumnInfo716.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo716.ColumnIndex = 18;
            clsColumnInfo716.ColumnName = "Column20";
            clsColumnInfo716.ColumnWidth = 0;
            clsColumnInfo716.Enabled = false;
            clsColumnInfo716.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo716.HeadText = "申请单ID";
            clsColumnInfo716.ReadOnly = true;
            clsColumnInfo716.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo717.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo717.BackColor = System.Drawing.Color.White;
            clsColumnInfo717.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo717.ColumnIndex = 19;
            clsColumnInfo717.ColumnName = "Column22";
            clsColumnInfo717.ColumnWidth = 0;
            clsColumnInfo717.Enabled = false;
            clsColumnInfo717.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo717.HeadText = "关联项目ID";
            clsColumnInfo717.ReadOnly = true;
            clsColumnInfo717.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo718.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo718.BackColor = System.Drawing.Color.White;
            clsColumnInfo718.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo718.ColumnIndex = 20;
            clsColumnInfo718.ColumnName = "Column23";
            clsColumnInfo718.ColumnWidth = 0;
            clsColumnInfo718.Enabled = false;
            clsColumnInfo718.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo718.HeadText = "主项默认用量";
            clsColumnInfo718.ReadOnly = true;
            clsColumnInfo718.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo719.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo719.BackColor = System.Drawing.Color.White;
            clsColumnInfo719.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo719.ColumnIndex = 21;
            clsColumnInfo719.ColumnName = "Column24";
            clsColumnInfo719.ColumnWidth = 0;
            clsColumnInfo719.Enabled = false;
            clsColumnInfo719.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo719.HeadText = "详细用法";
            clsColumnInfo719.ReadOnly = true;
            clsColumnInfo719.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo720.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo720.BackColor = System.Drawing.Color.White;
            clsColumnInfo720.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo720.ColumnIndex = 22;
            clsColumnInfo720.ColumnName = "Column25";
            clsColumnInfo720.ColumnWidth = 0;
            clsColumnInfo720.Enabled = false;
            clsColumnInfo720.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo720.HeadText = "用法ID";
            clsColumnInfo720.ReadOnly = true;
            clsColumnInfo720.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo721.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo721.BackColor = System.Drawing.Color.White;
            clsColumnInfo721.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo721.ColumnIndex = 23;
            clsColumnInfo721.ColumnName = "Column26";
            clsColumnInfo721.ColumnWidth = 0;
            clsColumnInfo721.Enabled = false;
            clsColumnInfo721.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo721.HeadText = "费用明细";
            clsColumnInfo721.ReadOnly = true;
            clsColumnInfo721.TextFont = new System.Drawing.Font("宋体", 10F);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo698);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo699);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo700);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo701);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo702);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo703);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo704);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo705);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo706);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo707);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo708);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo709);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo710);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo711);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo712);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo713);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo714);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo715);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo716);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo717);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo718);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo719);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo720);
            this.ctlDataGridTest.Columns.Add(clsColumnInfo721);
            this.ctlDataGridTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlDataGridTest.Font = new System.Drawing.Font("宋体", 11F);
            this.ctlDataGridTest.FullRowSelect = false;
            this.ctlDataGridTest.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctlDataGridTest.Location = new System.Drawing.Point(0, 0);
            this.ctlDataGridTest.MultiSelect = false;
            this.ctlDataGridTest.Name = "ctlDataGridTest";
            this.ctlDataGridTest.ReadOnly = false;
            this.ctlDataGridTest.RowHeadersVisible = false;
            this.ctlDataGridTest.RowHeaderWidth = 35;
            this.ctlDataGridTest.SelectedRowBackColor = System.Drawing.Color.Navy;
            this.ctlDataGridTest.SelectedRowForeColor = System.Drawing.Color.White;
            this.ctlDataGridTest.Size = new System.Drawing.Size(842, 409);
            this.ctlDataGridTest.TabIndex = 36;
            this.ctlDataGridTest.m_evtDataGridTextBoxKeyDown += new com.digitalwave.controls.datagrid.clsDGTextKeyEventHandler(this.ctlDataGridTest_m_evtDataGridTextBoxKeyDown);
            this.ctlDataGridTest.m_evtCurrentCellChanged += new System.EventHandler(this.ctlDataGridTest_m_evtCurrentCellChanged);
            this.ctlDataGridTest.m_evtDoubleClickCell += new com.digitalwave.controls.datagrid.clsDGTextMouseClickEventHandler(this.ctlDataGridTest_m_evtDoubleClickCell);
            this.ctlDataGridTest.m_evtDataGridKeyDown += new System.Windows.Forms.KeyEventHandler(this.ctlDataGridTest_m_evtDataGridKeyDown);
            this.ctlDataGridTest.Leave += new System.EventHandler(this.ctlDataGridTest_Leave);
            // 
            // ctlDataGrid4
            // 
            this.ctlDataGrid4.AllowAddNew = true;
            this.ctlDataGrid4.AllowDelete = true;
            this.ctlDataGrid4.AutoAppendRow = false;
            this.ctlDataGrid4.AutoScroll = true;
            this.ctlDataGrid4.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ctlDataGrid4.CaptionText = "";
            this.ctlDataGrid4.CaptionVisible = false;
            this.ctlDataGrid4.ColumnHeadersVisible = true;
            clsColumnInfo722.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo722.BackColor = System.Drawing.Color.White;
            clsColumnInfo722.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo722.ColumnIndex = 0;
            clsColumnInfo722.ColumnName = "Column1";
            clsColumnInfo722.ColumnWidth = 75;
            clsColumnInfo722.Enabled = true;
            clsColumnInfo722.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo722.HeadText = "查询";
            clsColumnInfo722.ReadOnly = false;
            clsColumnInfo722.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo723.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo723.BackColor = System.Drawing.Color.White;
            clsColumnInfo723.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo723.ColumnIndex = 1;
            clsColumnInfo723.ColumnName = "Column2";
            clsColumnInfo723.ColumnWidth = 60;
            clsColumnInfo723.Enabled = true;
            clsColumnInfo723.ForeColor = System.Drawing.Color.Red;
            clsColumnInfo723.HeadText = "数量";
            clsColumnInfo723.ReadOnly = false;
            clsColumnInfo723.TextFont = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            clsColumnInfo724.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo724.BackColor = System.Drawing.Color.White;
            clsColumnInfo724.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo724.ColumnIndex = 2;
            clsColumnInfo724.ColumnName = "Column3";
            clsColumnInfo724.ColumnWidth = 225;
            clsColumnInfo724.Enabled = true;
            clsColumnInfo724.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo724.HeadText = "项目名称";
            clsColumnInfo724.ReadOnly = true;
            clsColumnInfo724.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo725.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo725.BackColor = System.Drawing.Color.White;
            clsColumnInfo725.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo725.ColumnIndex = 3;
            clsColumnInfo725.ColumnName = "Column4";
            clsColumnInfo725.ColumnWidth = 110;
            clsColumnInfo725.Enabled = true;
            clsColumnInfo725.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo725.HeadText = "规格";
            clsColumnInfo725.ReadOnly = true;
            clsColumnInfo725.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo726.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo726.BackColor = System.Drawing.Color.White;
            clsColumnInfo726.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo726.ColumnIndex = 4;
            clsColumnInfo726.ColumnName = "Column21";
            clsColumnInfo726.ColumnWidth = 75;
            clsColumnInfo726.Enabled = true;
            clsColumnInfo726.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo726.HeadText = "检查部位";
            clsColumnInfo726.ReadOnly = false;
            clsColumnInfo726.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo727.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo727.BackColor = System.Drawing.Color.White;
            clsColumnInfo727.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo727.ColumnIndex = 5;
            clsColumnInfo727.ColumnName = "Column5";
            clsColumnInfo727.ColumnWidth = 60;
            clsColumnInfo727.Enabled = true;
            clsColumnInfo727.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo727.HeadText = "单位";
            clsColumnInfo727.ReadOnly = true;
            clsColumnInfo727.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo728.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo728.BackColor = System.Drawing.Color.White;
            clsColumnInfo728.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo728.ColumnIndex = 6;
            clsColumnInfo728.ColumnName = "Column6";
            clsColumnInfo728.ColumnWidth = 65;
            clsColumnInfo728.Enabled = true;
            clsColumnInfo728.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo728.HeadText = "单价";
            clsColumnInfo728.ReadOnly = false;
            clsColumnInfo728.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo729.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo729.BackColor = System.Drawing.Color.White;
            clsColumnInfo729.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo729.ColumnIndex = 7;
            clsColumnInfo729.ColumnName = "Column7";
            clsColumnInfo729.ColumnWidth = 65;
            clsColumnInfo729.Enabled = true;
            clsColumnInfo729.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo729.HeadText = "总价";
            clsColumnInfo729.ReadOnly = true;
            clsColumnInfo729.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo730.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo730.BackColor = System.Drawing.Color.White;
            clsColumnInfo730.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo730.ColumnIndex = 8;
            clsColumnInfo730.ColumnName = "Column10";
            clsColumnInfo730.ColumnWidth = 0;
            clsColumnInfo730.Enabled = true;
            clsColumnInfo730.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo730.HeadText = "ID";
            clsColumnInfo730.ReadOnly = false;
            clsColumnInfo730.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo731.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo731.BackColor = System.Drawing.Color.White;
            clsColumnInfo731.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo731.ColumnIndex = 9;
            clsColumnInfo731.ColumnName = "Column11";
            clsColumnInfo731.ColumnWidth = 0;
            clsColumnInfo731.Enabled = true;
            clsColumnInfo731.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo731.HeadText = "是否自定义价格";
            clsColumnInfo731.ReadOnly = true;
            clsColumnInfo731.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo732.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo732.BackColor = System.Drawing.Color.White;
            clsColumnInfo732.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Int;
            clsColumnInfo732.ColumnIndex = 10;
            clsColumnInfo732.ColumnName = "Column12";
            clsColumnInfo732.ColumnWidth = 0;
            clsColumnInfo732.Enabled = true;
            clsColumnInfo732.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo732.HeadText = "行号";
            clsColumnInfo732.ReadOnly = true;
            clsColumnInfo732.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo733.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo733.BackColor = System.Drawing.Color.White;
            clsColumnInfo733.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo733.ColumnIndex = 11;
            clsColumnInfo733.ColumnName = "Column13";
            clsColumnInfo733.ColumnWidth = 75;
            clsColumnInfo733.Enabled = true;
            clsColumnInfo733.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo733.HeadText = "收费比例";
            clsColumnInfo733.ReadOnly = true;
            clsColumnInfo733.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo734.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo734.BackColor = System.Drawing.Color.White;
            clsColumnInfo734.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo734.ColumnIndex = 12;
            clsColumnInfo734.ColumnName = "Column14";
            clsColumnInfo734.ColumnWidth = 0;
            clsColumnInfo734.Enabled = true;
            clsColumnInfo734.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo734.HeadText = "比例值";
            clsColumnInfo734.ReadOnly = true;
            clsColumnInfo734.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo735.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo735.BackColor = System.Drawing.Color.White;
            clsColumnInfo735.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo735.ColumnIndex = 13;
            clsColumnInfo735.ColumnName = "Column15";
            clsColumnInfo735.ColumnWidth = 0;
            clsColumnInfo735.Enabled = false;
            clsColumnInfo735.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo735.HeadText = "发票分类";
            clsColumnInfo735.ReadOnly = true;
            clsColumnInfo735.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo736.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo736.BackColor = System.Drawing.Color.White;
            clsColumnInfo736.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo736.ColumnIndex = 14;
            clsColumnInfo736.ColumnName = "Column16";
            clsColumnInfo736.ColumnWidth = 0;
            clsColumnInfo736.Enabled = false;
            clsColumnInfo736.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo736.HeadText = "附加项目ID";
            clsColumnInfo736.ReadOnly = true;
            clsColumnInfo736.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo737.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo737.BackColor = System.Drawing.Color.White;
            clsColumnInfo737.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo737.ColumnIndex = 15;
            clsColumnInfo737.ColumnName = "Column17";
            clsColumnInfo737.ColumnWidth = 0;
            clsColumnInfo737.Enabled = false;
            clsColumnInfo737.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo737.HeadText = "附加项目原数量";
            clsColumnInfo737.ReadOnly = true;
            clsColumnInfo737.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo738.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo738.BackColor = System.Drawing.Color.White;
            clsColumnInfo738.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo738.ColumnIndex = 16;
            clsColumnInfo738.ColumnName = "Column18";
            clsColumnInfo738.ColumnWidth = 0;
            clsColumnInfo738.Enabled = false;
            clsColumnInfo738.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo738.HeadText = "英文名";
            clsColumnInfo738.ReadOnly = true;
            clsColumnInfo738.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo739.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo739.BackColor = System.Drawing.Color.White;
            clsColumnInfo739.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo739.ColumnIndex = 17;
            clsColumnInfo739.ColumnName = "Column19";
            clsColumnInfo739.ColumnWidth = 0;
            clsColumnInfo739.Enabled = false;
            clsColumnInfo739.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo739.HeadText = "预留";
            clsColumnInfo739.ReadOnly = true;
            clsColumnInfo739.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo740.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo740.BackColor = System.Drawing.Color.White;
            clsColumnInfo740.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo740.ColumnIndex = 18;
            clsColumnInfo740.ColumnName = "Column20";
            clsColumnInfo740.ColumnWidth = 0;
            clsColumnInfo740.Enabled = false;
            clsColumnInfo740.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo740.HeadText = "申请单ID";
            clsColumnInfo740.ReadOnly = true;
            clsColumnInfo740.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo741.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo741.BackColor = System.Drawing.Color.White;
            clsColumnInfo741.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo741.ColumnIndex = 19;
            clsColumnInfo741.ColumnName = "Column22";
            clsColumnInfo741.ColumnWidth = 0;
            clsColumnInfo741.Enabled = false;
            clsColumnInfo741.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo741.HeadText = "关联项目ID";
            clsColumnInfo741.ReadOnly = true;
            clsColumnInfo741.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo742.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo742.BackColor = System.Drawing.Color.White;
            clsColumnInfo742.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo742.ColumnIndex = 20;
            clsColumnInfo742.ColumnName = "Column23";
            clsColumnInfo742.ColumnWidth = 0;
            clsColumnInfo742.Enabled = false;
            clsColumnInfo742.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo742.HeadText = "主项默认用量";
            clsColumnInfo742.ReadOnly = true;
            clsColumnInfo742.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo743.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo743.BackColor = System.Drawing.Color.White;
            clsColumnInfo743.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo743.ColumnIndex = 21;
            clsColumnInfo743.ColumnName = "Column24";
            clsColumnInfo743.ColumnWidth = 0;
            clsColumnInfo743.Enabled = false;
            clsColumnInfo743.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo743.HeadText = "详细用法";
            clsColumnInfo743.ReadOnly = true;
            clsColumnInfo743.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo744.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo744.BackColor = System.Drawing.Color.White;
            clsColumnInfo744.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo744.ColumnIndex = 22;
            clsColumnInfo744.ColumnName = "Column25";
            clsColumnInfo744.ColumnWidth = 0;
            clsColumnInfo744.Enabled = false;
            clsColumnInfo744.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo744.HeadText = "用法ID";
            clsColumnInfo744.ReadOnly = true;
            clsColumnInfo744.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo745.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo745.BackColor = System.Drawing.Color.White;
            clsColumnInfo745.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo745.ColumnIndex = 23;
            clsColumnInfo745.ColumnName = "Column26";
            clsColumnInfo745.ColumnWidth = 0;
            clsColumnInfo745.Enabled = false;
            clsColumnInfo745.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo745.HeadText = "主诊疗项目ID";
            clsColumnInfo745.ReadOnly = true;
            clsColumnInfo745.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo746.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo746.BackColor = System.Drawing.Color.White;
            clsColumnInfo746.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo746.ColumnIndex = 24;
            clsColumnInfo746.ColumnName = "Column27";
            clsColumnInfo746.ColumnWidth = 0;
            clsColumnInfo746.Enabled = false;
            clsColumnInfo746.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo746.HeadText = "主诊疗项目带出时基数";
            clsColumnInfo746.ReadOnly = true;
            clsColumnInfo746.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo747.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo747.BackColor = System.Drawing.Color.White;
            clsColumnInfo747.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo747.ColumnIndex = 25;
            clsColumnInfo747.ColumnName = "Column28";
            clsColumnInfo747.ColumnWidth = 0;
            clsColumnInfo747.Enabled = false;
            clsColumnInfo747.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo747.HeadText = "费用明细";
            clsColumnInfo747.ReadOnly = true;
            clsColumnInfo747.TextFont = new System.Drawing.Font("宋体", 10F);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo722);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo723);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo724);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo725);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo726);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo727);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo728);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo729);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo730);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo731);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo732);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo733);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo734);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo735);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo736);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo737);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo738);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo739);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo740);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo741);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo742);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo743);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo744);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo745);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo746);
            this.ctlDataGrid4.Columns.Add(clsColumnInfo747);
            this.ctlDataGrid4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlDataGrid4.Font = new System.Drawing.Font("宋体", 11F);
            this.ctlDataGrid4.FullRowSelect = false;
            this.ctlDataGrid4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctlDataGrid4.Location = new System.Drawing.Point(0, 0);
            this.ctlDataGrid4.MultiSelect = false;
            this.ctlDataGrid4.Name = "ctlDataGrid4";
            this.ctlDataGrid4.ReadOnly = false;
            this.ctlDataGrid4.RowHeadersVisible = false;
            this.ctlDataGrid4.RowHeaderWidth = 35;
            this.ctlDataGrid4.SelectedRowBackColor = System.Drawing.Color.Navy;
            this.ctlDataGrid4.SelectedRowForeColor = System.Drawing.Color.White;
            this.ctlDataGrid4.Size = new System.Drawing.Size(842, 409);
            this.ctlDataGrid4.TabIndex = 35;
            this.ctlDataGrid4.m_evtDataGridTextBoxKeyDown += new com.digitalwave.controls.datagrid.clsDGTextKeyEventHandler(this.ctlDataGrid4_m_evtDataGridTextBoxKeyDown);
            this.ctlDataGrid4.m_evtCurrentCellChanged += new System.EventHandler(this.ctlDataGrid4_m_evtCurrentCellChanged);
            this.ctlDataGrid4.m_evtDoubleClickCell += new com.digitalwave.controls.datagrid.clsDGTextMouseClickEventHandler(this.ctlDataGrid4_m_evtDoubleClickCell);
            this.ctlDataGrid4.m_evtDataGridKeyDown += new System.Windows.Forms.KeyEventHandler(this.ctlDataGrid4_m_evtDataGridKeyDown);
            this.ctlDataGrid4.Leave += new System.EventHandler(this.ctlDataGrid4_Leave);
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.plOps);
            this.tabPage9.Controls.Add(this.panel9);
            this.tabPage9.Controls.Add(this.ctlDataGridOps);
            this.tabPage9.Controls.Add(this.ctlDataGrid5);
            this.tabPage9.Location = new System.Drawing.Point(4, 27);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(842, 409);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "[7]手术/治疗";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // plOps
            // 
            this.plOps.AutoScroll = true;
            this.plOps.BackColor = System.Drawing.Color.White;
            this.plOps.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plOps.Controls.Add(this.panel14);
            this.plOps.Controls.Add(this.panel15);
            this.plOps.Dock = System.Windows.Forms.DockStyle.Right;
            this.plOps.Location = new System.Drawing.Point(842, 0);
            this.plOps.Name = "plOps";
            this.plOps.Size = new System.Drawing.Size(0, 409);
            this.plOps.TabIndex = 41;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.label27);
            this.panel14.Controls.Add(this.lvOps);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(0, 28);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(0, 381);
            this.panel14.TabIndex = 4;
            // 
            // label27
            // 
            this.label27.Dock = System.Windows.Forms.DockStyle.Top;
            this.label27.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label27.Location = new System.Drawing.Point(0, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(0, 16);
            this.label27.TabIndex = 3;
            this.label27.Text = "序号  名称                     单价    数量      金额   自付  自付金额";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvOps
            // 
            this.lvOps.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvOps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader38,
            this.columnHeader39,
            this.columnHeader40,
            this.columnHeader41,
            this.columnHeader42,
            this.columnHeader50,
            this.columnHeader53});
            this.lvOps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvOps.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvOps.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lvOps.Location = new System.Drawing.Point(0, 0);
            this.lvOps.MultiSelect = false;
            this.lvOps.Name = "lvOps";
            this.lvOps.Size = new System.Drawing.Size(0, 381);
            this.lvOps.TabIndex = 2;
            this.lvOps.UseCompatibleStateImageBehavior = false;
            this.lvOps.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader38
            // 
            this.columnHeader38.Width = 28;
            // 
            // columnHeader39
            // 
            this.columnHeader39.Width = 137;
            // 
            // columnHeader40
            // 
            this.columnHeader40.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader40.Width = 54;
            // 
            // columnHeader41
            // 
            this.columnHeader41.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader41.Width = 55;
            // 
            // columnHeader42
            // 
            this.columnHeader42.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader42.Width = 64;
            // 
            // columnHeader50
            // 
            this.columnHeader50.Width = 40;
            // 
            // columnHeader53
            // 
            this.columnHeader53.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.label29);
            this.panel15.Controls.Add(this.pictureBox4);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(0, 28);
            this.panel15.TabIndex = 5;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label29.Location = new System.Drawing.Point(32, 8);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(53, 12);
            this.label29.TabIndex = 1;
            this.label29.Text = "费用明细";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(8, 4);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(16, 16);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            // 
            // panel9
            // 
            this.panel9.AutoScroll = true;
            this.panel9.BackColor = System.Drawing.Color.White;
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(842, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(0, 409);
            this.panel9.TabIndex = 40;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.label25);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(0, 409);
            this.panel10.TabIndex = 4;
            // 
            // label25
            // 
            this.label25.Dock = System.Windows.Forms.DockStyle.Top;
            this.label25.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(0, 16);
            this.label25.TabIndex = 3;
            this.label25.Text = "序号  名称                     单价    数量      金额";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlDataGridOps
            // 
            this.ctlDataGridOps.AllowAddNew = true;
            this.ctlDataGridOps.AllowDelete = true;
            this.ctlDataGridOps.AutoAppendRow = false;
            this.ctlDataGridOps.AutoScroll = true;
            this.ctlDataGridOps.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ctlDataGridOps.CaptionText = "";
            this.ctlDataGridOps.CaptionVisible = false;
            this.ctlDataGridOps.ColumnHeadersVisible = true;
            clsColumnInfo748.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo748.BackColor = System.Drawing.Color.White;
            clsColumnInfo748.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo748.ColumnIndex = 0;
            clsColumnInfo748.ColumnName = "Column1";
            clsColumnInfo748.ColumnWidth = 75;
            clsColumnInfo748.Enabled = true;
            clsColumnInfo748.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo748.HeadText = "查询";
            clsColumnInfo748.ReadOnly = false;
            clsColumnInfo748.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo749.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo749.BackColor = System.Drawing.Color.White;
            clsColumnInfo749.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo749.ColumnIndex = 1;
            clsColumnInfo749.ColumnName = "Column2";
            clsColumnInfo749.ColumnWidth = 65;
            clsColumnInfo749.Enabled = true;
            clsColumnInfo749.ForeColor = System.Drawing.Color.Red;
            clsColumnInfo749.HeadText = "数量";
            clsColumnInfo749.ReadOnly = false;
            clsColumnInfo749.TextFont = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            clsColumnInfo750.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo750.BackColor = System.Drawing.Color.White;
            clsColumnInfo750.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo750.ColumnIndex = 2;
            clsColumnInfo750.ColumnName = "Column3";
            clsColumnInfo750.ColumnWidth = 242;
            clsColumnInfo750.Enabled = true;
            clsColumnInfo750.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo750.HeadText = "项目名称";
            clsColumnInfo750.ReadOnly = true;
            clsColumnInfo750.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo751.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo751.BackColor = System.Drawing.Color.White;
            clsColumnInfo751.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo751.ColumnIndex = 3;
            clsColumnInfo751.ColumnName = "Column4";
            clsColumnInfo751.ColumnWidth = 148;
            clsColumnInfo751.Enabled = true;
            clsColumnInfo751.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo751.HeadText = "规格";
            clsColumnInfo751.ReadOnly = true;
            clsColumnInfo751.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo752.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo752.BackColor = System.Drawing.Color.White;
            clsColumnInfo752.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo752.ColumnIndex = 4;
            clsColumnInfo752.ColumnName = "Column5";
            clsColumnInfo752.ColumnWidth = 55;
            clsColumnInfo752.Enabled = true;
            clsColumnInfo752.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo752.HeadText = "单位";
            clsColumnInfo752.ReadOnly = true;
            clsColumnInfo752.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo753.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo753.BackColor = System.Drawing.Color.White;
            clsColumnInfo753.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo753.ColumnIndex = 5;
            clsColumnInfo753.ColumnName = "Column6";
            clsColumnInfo753.ColumnWidth = 75;
            clsColumnInfo753.Enabled = true;
            clsColumnInfo753.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo753.HeadText = "单价";
            clsColumnInfo753.ReadOnly = false;
            clsColumnInfo753.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo754.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo754.BackColor = System.Drawing.Color.White;
            clsColumnInfo754.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo754.ColumnIndex = 6;
            clsColumnInfo754.ColumnName = "Column7";
            clsColumnInfo754.ColumnWidth = 75;
            clsColumnInfo754.Enabled = true;
            clsColumnInfo754.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo754.HeadText = "总价";
            clsColumnInfo754.ReadOnly = true;
            clsColumnInfo754.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo755.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo755.BackColor = System.Drawing.Color.White;
            clsColumnInfo755.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo755.ColumnIndex = 7;
            clsColumnInfo755.ColumnName = "Column10";
            clsColumnInfo755.ColumnWidth = 0;
            clsColumnInfo755.Enabled = true;
            clsColumnInfo755.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo755.HeadText = "ID";
            clsColumnInfo755.ReadOnly = false;
            clsColumnInfo755.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo756.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo756.BackColor = System.Drawing.Color.White;
            clsColumnInfo756.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo756.ColumnIndex = 8;
            clsColumnInfo756.ColumnName = "Column11";
            clsColumnInfo756.ColumnWidth = 0;
            clsColumnInfo756.Enabled = true;
            clsColumnInfo756.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo756.HeadText = "是否自定义价格";
            clsColumnInfo756.ReadOnly = true;
            clsColumnInfo756.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo757.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo757.BackColor = System.Drawing.Color.White;
            clsColumnInfo757.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Int;
            clsColumnInfo757.ColumnIndex = 9;
            clsColumnInfo757.ColumnName = "Column12";
            clsColumnInfo757.ColumnWidth = 0;
            clsColumnInfo757.Enabled = true;
            clsColumnInfo757.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo757.HeadText = "行号";
            clsColumnInfo757.ReadOnly = true;
            clsColumnInfo757.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo758.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo758.BackColor = System.Drawing.Color.White;
            clsColumnInfo758.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo758.ColumnIndex = 10;
            clsColumnInfo758.ColumnName = "Column13";
            clsColumnInfo758.ColumnWidth = 75;
            clsColumnInfo758.Enabled = true;
            clsColumnInfo758.ForeColor = System.Drawing.Color.Green;
            clsColumnInfo758.HeadText = "收费比例";
            clsColumnInfo758.ReadOnly = true;
            clsColumnInfo758.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo759.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo759.BackColor = System.Drawing.Color.White;
            clsColumnInfo759.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo759.ColumnIndex = 11;
            clsColumnInfo759.ColumnName = "Column14";
            clsColumnInfo759.ColumnWidth = 0;
            clsColumnInfo759.Enabled = true;
            clsColumnInfo759.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo759.HeadText = "比例值";
            clsColumnInfo759.ReadOnly = true;
            clsColumnInfo759.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo760.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo760.BackColor = System.Drawing.Color.White;
            clsColumnInfo760.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo760.ColumnIndex = 12;
            clsColumnInfo760.ColumnName = "Column15";
            clsColumnInfo760.ColumnWidth = 0;
            clsColumnInfo760.Enabled = false;
            clsColumnInfo760.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo760.HeadText = "发票分类";
            clsColumnInfo760.ReadOnly = true;
            clsColumnInfo760.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo761.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo761.BackColor = System.Drawing.Color.White;
            clsColumnInfo761.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo761.ColumnIndex = 13;
            clsColumnInfo761.ColumnName = "Column16";
            clsColumnInfo761.ColumnWidth = 0;
            clsColumnInfo761.Enabled = false;
            clsColumnInfo761.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo761.HeadText = "附加项目ID";
            clsColumnInfo761.ReadOnly = true;
            clsColumnInfo761.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo762.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo762.BackColor = System.Drawing.Color.White;
            clsColumnInfo762.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo762.ColumnIndex = 14;
            clsColumnInfo762.ColumnName = "Column17";
            clsColumnInfo762.ColumnWidth = 0;
            clsColumnInfo762.Enabled = false;
            clsColumnInfo762.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo762.HeadText = "附加项目原数量";
            clsColumnInfo762.ReadOnly = true;
            clsColumnInfo762.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo763.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo763.BackColor = System.Drawing.Color.White;
            clsColumnInfo763.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo763.ColumnIndex = 15;
            clsColumnInfo763.ColumnName = "Column18";
            clsColumnInfo763.ColumnWidth = 0;
            clsColumnInfo763.Enabled = false;
            clsColumnInfo763.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo763.HeadText = "英文名";
            clsColumnInfo763.ReadOnly = true;
            clsColumnInfo763.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo764.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo764.BackColor = System.Drawing.Color.White;
            clsColumnInfo764.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo764.ColumnIndex = 16;
            clsColumnInfo764.ColumnName = "Column19";
            clsColumnInfo764.ColumnWidth = 0;
            clsColumnInfo764.Enabled = false;
            clsColumnInfo764.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo764.HeadText = "预留";
            clsColumnInfo764.ReadOnly = true;
            clsColumnInfo764.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo765.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo765.BackColor = System.Drawing.Color.White;
            clsColumnInfo765.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo765.ColumnIndex = 17;
            clsColumnInfo765.ColumnName = "Column20";
            clsColumnInfo765.ColumnWidth = 0;
            clsColumnInfo765.Enabled = false;
            clsColumnInfo765.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo765.HeadText = "申请单号";
            clsColumnInfo765.ReadOnly = true;
            clsColumnInfo765.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo766.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo766.BackColor = System.Drawing.Color.White;
            clsColumnInfo766.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo766.ColumnIndex = 18;
            clsColumnInfo766.ColumnName = "Column21";
            clsColumnInfo766.ColumnWidth = 0;
            clsColumnInfo766.Enabled = false;
            clsColumnInfo766.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo766.HeadText = "关联项目ID";
            clsColumnInfo766.ReadOnly = true;
            clsColumnInfo766.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo767.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo767.BackColor = System.Drawing.Color.White;
            clsColumnInfo767.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo767.ColumnIndex = 19;
            clsColumnInfo767.ColumnName = "Column22";
            clsColumnInfo767.ColumnWidth = 0;
            clsColumnInfo767.Enabled = false;
            clsColumnInfo767.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo767.HeadText = "主项默认用量";
            clsColumnInfo767.ReadOnly = true;
            clsColumnInfo767.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo768.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo768.BackColor = System.Drawing.Color.White;
            clsColumnInfo768.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo768.ColumnIndex = 20;
            clsColumnInfo768.ColumnName = "Column23";
            clsColumnInfo768.ColumnWidth = 0;
            clsColumnInfo768.Enabled = false;
            clsColumnInfo768.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo768.HeadText = "申请单标志";
            clsColumnInfo768.ReadOnly = true;
            clsColumnInfo768.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo769.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo769.BackColor = System.Drawing.Color.White;
            clsColumnInfo769.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo769.ColumnIndex = 21;
            clsColumnInfo769.ColumnName = "Column24";
            clsColumnInfo769.ColumnWidth = 0;
            clsColumnInfo769.Enabled = false;
            clsColumnInfo769.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo769.HeadText = "详细用法";
            clsColumnInfo769.ReadOnly = true;
            clsColumnInfo769.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo770.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo770.BackColor = System.Drawing.Color.White;
            clsColumnInfo770.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo770.ColumnIndex = 22;
            clsColumnInfo770.ColumnName = "Column25";
            clsColumnInfo770.ColumnWidth = 0;
            clsColumnInfo770.Enabled = false;
            clsColumnInfo770.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo770.HeadText = "用法ID";
            clsColumnInfo770.ReadOnly = true;
            clsColumnInfo770.TextFont = new System.Drawing.Font("宋体", 10F);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo748);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo749);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo750);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo751);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo752);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo753);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo754);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo755);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo756);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo757);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo758);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo759);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo760);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo761);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo762);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo763);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo764);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo765);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo766);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo767);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo768);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo769);
            this.ctlDataGridOps.Columns.Add(clsColumnInfo770);
            this.ctlDataGridOps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlDataGridOps.Font = new System.Drawing.Font("宋体", 11F);
            this.ctlDataGridOps.FullRowSelect = false;
            this.ctlDataGridOps.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctlDataGridOps.Location = new System.Drawing.Point(0, 0);
            this.ctlDataGridOps.MultiSelect = false;
            this.ctlDataGridOps.Name = "ctlDataGridOps";
            this.ctlDataGridOps.ReadOnly = false;
            this.ctlDataGridOps.RowHeadersVisible = false;
            this.ctlDataGridOps.RowHeaderWidth = 35;
            this.ctlDataGridOps.SelectedRowBackColor = System.Drawing.Color.Navy;
            this.ctlDataGridOps.SelectedRowForeColor = System.Drawing.Color.White;
            this.ctlDataGridOps.Size = new System.Drawing.Size(842, 409);
            this.ctlDataGridOps.TabIndex = 36;
            this.ctlDataGridOps.m_evtDataGridTextBoxKeyDown += new com.digitalwave.controls.datagrid.clsDGTextKeyEventHandler(this.ctlDataGridOps_m_evtDataGridTextBoxKeyDown);
            this.ctlDataGridOps.m_evtCurrentCellChanged += new System.EventHandler(this.ctlDataGridOps_m_evtCurrentCellChanged);
            this.ctlDataGridOps.m_evtDoubleClickCell += new com.digitalwave.controls.datagrid.clsDGTextMouseClickEventHandler(this.ctlDataGridOps_m_evtDoubleClickCell);
            this.ctlDataGridOps.m_evtDataGridKeyDown += new System.Windows.Forms.KeyEventHandler(this.ctlDataGridOps_m_evtDataGridKeyDown);
            this.ctlDataGridOps.Leave += new System.EventHandler(this.ctlDataGridOps_Leave);
            // 
            // ctlDataGrid5
            // 
            this.ctlDataGrid5.AllowAddNew = true;
            this.ctlDataGrid5.AllowDelete = true;
            this.ctlDataGrid5.AutoAppendRow = false;
            this.ctlDataGrid5.AutoScroll = true;
            this.ctlDataGrid5.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ctlDataGrid5.CaptionText = "";
            this.ctlDataGrid5.CaptionVisible = false;
            this.ctlDataGrid5.ColumnHeadersVisible = true;
            clsColumnInfo771.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo771.BackColor = System.Drawing.Color.White;
            clsColumnInfo771.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo771.ColumnIndex = 0;
            clsColumnInfo771.ColumnName = "Column1";
            clsColumnInfo771.ColumnWidth = 75;
            clsColumnInfo771.Enabled = true;
            clsColumnInfo771.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo771.HeadText = "查询";
            clsColumnInfo771.ReadOnly = false;
            clsColumnInfo771.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo772.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo772.BackColor = System.Drawing.Color.White;
            clsColumnInfo772.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo772.ColumnIndex = 1;
            clsColumnInfo772.ColumnName = "Column2";
            clsColumnInfo772.ColumnWidth = 65;
            clsColumnInfo772.Enabled = true;
            clsColumnInfo772.ForeColor = System.Drawing.Color.Red;
            clsColumnInfo772.HeadText = "数量";
            clsColumnInfo772.ReadOnly = false;
            clsColumnInfo772.TextFont = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            clsColumnInfo773.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo773.BackColor = System.Drawing.Color.White;
            clsColumnInfo773.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo773.ColumnIndex = 2;
            clsColumnInfo773.ColumnName = "Column3";
            clsColumnInfo773.ColumnWidth = 242;
            clsColumnInfo773.Enabled = true;
            clsColumnInfo773.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo773.HeadText = "项目名称";
            clsColumnInfo773.ReadOnly = true;
            clsColumnInfo773.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo774.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo774.BackColor = System.Drawing.Color.White;
            clsColumnInfo774.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo774.ColumnIndex = 3;
            clsColumnInfo774.ColumnName = "Column4";
            clsColumnInfo774.ColumnWidth = 148;
            clsColumnInfo774.Enabled = true;
            clsColumnInfo774.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo774.HeadText = "规格";
            clsColumnInfo774.ReadOnly = true;
            clsColumnInfo774.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo775.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo775.BackColor = System.Drawing.Color.White;
            clsColumnInfo775.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo775.ColumnIndex = 4;
            clsColumnInfo775.ColumnName = "Column5";
            clsColumnInfo775.ColumnWidth = 55;
            clsColumnInfo775.Enabled = true;
            clsColumnInfo775.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo775.HeadText = "单位";
            clsColumnInfo775.ReadOnly = true;
            clsColumnInfo775.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo776.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo776.BackColor = System.Drawing.Color.White;
            clsColumnInfo776.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo776.ColumnIndex = 5;
            clsColumnInfo776.ColumnName = "Column6";
            clsColumnInfo776.ColumnWidth = 75;
            clsColumnInfo776.Enabled = true;
            clsColumnInfo776.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo776.HeadText = "单价";
            clsColumnInfo776.ReadOnly = false;
            clsColumnInfo776.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo777.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo777.BackColor = System.Drawing.Color.White;
            clsColumnInfo777.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo777.ColumnIndex = 6;
            clsColumnInfo777.ColumnName = "Column7";
            clsColumnInfo777.ColumnWidth = 75;
            clsColumnInfo777.Enabled = true;
            clsColumnInfo777.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo777.HeadText = "总价";
            clsColumnInfo777.ReadOnly = true;
            clsColumnInfo777.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo778.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo778.BackColor = System.Drawing.Color.White;
            clsColumnInfo778.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo778.ColumnIndex = 7;
            clsColumnInfo778.ColumnName = "Column10";
            clsColumnInfo778.ColumnWidth = 0;
            clsColumnInfo778.Enabled = true;
            clsColumnInfo778.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo778.HeadText = "ID";
            clsColumnInfo778.ReadOnly = false;
            clsColumnInfo778.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo779.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo779.BackColor = System.Drawing.Color.White;
            clsColumnInfo779.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo779.ColumnIndex = 8;
            clsColumnInfo779.ColumnName = "Column11";
            clsColumnInfo779.ColumnWidth = 0;
            clsColumnInfo779.Enabled = true;
            clsColumnInfo779.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo779.HeadText = "是否自定义价格";
            clsColumnInfo779.ReadOnly = true;
            clsColumnInfo779.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo780.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo780.BackColor = System.Drawing.Color.White;
            clsColumnInfo780.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Int;
            clsColumnInfo780.ColumnIndex = 9;
            clsColumnInfo780.ColumnName = "Column12";
            clsColumnInfo780.ColumnWidth = 0;
            clsColumnInfo780.Enabled = true;
            clsColumnInfo780.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo780.HeadText = "行号";
            clsColumnInfo780.ReadOnly = true;
            clsColumnInfo780.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo781.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo781.BackColor = System.Drawing.Color.White;
            clsColumnInfo781.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo781.ColumnIndex = 10;
            clsColumnInfo781.ColumnName = "Column13";
            clsColumnInfo781.ColumnWidth = 75;
            clsColumnInfo781.Enabled = true;
            clsColumnInfo781.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo781.HeadText = "收费比例";
            clsColumnInfo781.ReadOnly = true;
            clsColumnInfo781.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo782.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo782.BackColor = System.Drawing.Color.White;
            clsColumnInfo782.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo782.ColumnIndex = 11;
            clsColumnInfo782.ColumnName = "Column14";
            clsColumnInfo782.ColumnWidth = 0;
            clsColumnInfo782.Enabled = true;
            clsColumnInfo782.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo782.HeadText = "比例值";
            clsColumnInfo782.ReadOnly = true;
            clsColumnInfo782.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo783.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo783.BackColor = System.Drawing.Color.White;
            clsColumnInfo783.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo783.ColumnIndex = 12;
            clsColumnInfo783.ColumnName = "Column15";
            clsColumnInfo783.ColumnWidth = 0;
            clsColumnInfo783.Enabled = false;
            clsColumnInfo783.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo783.HeadText = "发票分类";
            clsColumnInfo783.ReadOnly = true;
            clsColumnInfo783.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo784.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo784.BackColor = System.Drawing.Color.White;
            clsColumnInfo784.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo784.ColumnIndex = 13;
            clsColumnInfo784.ColumnName = "Column16";
            clsColumnInfo784.ColumnWidth = 0;
            clsColumnInfo784.Enabled = false;
            clsColumnInfo784.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo784.HeadText = "附加项目ID";
            clsColumnInfo784.ReadOnly = true;
            clsColumnInfo784.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo785.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo785.BackColor = System.Drawing.Color.White;
            clsColumnInfo785.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo785.ColumnIndex = 14;
            clsColumnInfo785.ColumnName = "Column17";
            clsColumnInfo785.ColumnWidth = 0;
            clsColumnInfo785.Enabled = false;
            clsColumnInfo785.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo785.HeadText = "附加项目原数量";
            clsColumnInfo785.ReadOnly = true;
            clsColumnInfo785.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo786.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo786.BackColor = System.Drawing.Color.White;
            clsColumnInfo786.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo786.ColumnIndex = 15;
            clsColumnInfo786.ColumnName = "Column18";
            clsColumnInfo786.ColumnWidth = 0;
            clsColumnInfo786.Enabled = false;
            clsColumnInfo786.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo786.HeadText = "英文名";
            clsColumnInfo786.ReadOnly = true;
            clsColumnInfo786.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo787.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo787.BackColor = System.Drawing.Color.White;
            clsColumnInfo787.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo787.ColumnIndex = 16;
            clsColumnInfo787.ColumnName = "Column19";
            clsColumnInfo787.ColumnWidth = 0;
            clsColumnInfo787.Enabled = false;
            clsColumnInfo787.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo787.HeadText = "预留";
            clsColumnInfo787.ReadOnly = true;
            clsColumnInfo787.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo788.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo788.BackColor = System.Drawing.Color.White;
            clsColumnInfo788.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo788.ColumnIndex = 17;
            clsColumnInfo788.ColumnName = "Column20";
            clsColumnInfo788.ColumnWidth = 0;
            clsColumnInfo788.Enabled = false;
            clsColumnInfo788.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo788.HeadText = "申请单号";
            clsColumnInfo788.ReadOnly = true;
            clsColumnInfo788.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo789.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo789.BackColor = System.Drawing.Color.White;
            clsColumnInfo789.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo789.ColumnIndex = 18;
            clsColumnInfo789.ColumnName = "Column21";
            clsColumnInfo789.ColumnWidth = 0;
            clsColumnInfo789.Enabled = false;
            clsColumnInfo789.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo789.HeadText = "关联项目ID";
            clsColumnInfo789.ReadOnly = true;
            clsColumnInfo789.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo790.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo790.BackColor = System.Drawing.Color.White;
            clsColumnInfo790.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo790.ColumnIndex = 19;
            clsColumnInfo790.ColumnName = "Column22";
            clsColumnInfo790.ColumnWidth = 0;
            clsColumnInfo790.Enabled = false;
            clsColumnInfo790.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo790.HeadText = "主项默认用量";
            clsColumnInfo790.ReadOnly = true;
            clsColumnInfo790.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo791.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo791.BackColor = System.Drawing.Color.White;
            clsColumnInfo791.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo791.ColumnIndex = 20;
            clsColumnInfo791.ColumnName = "Column23";
            clsColumnInfo791.ColumnWidth = 0;
            clsColumnInfo791.Enabled = false;
            clsColumnInfo791.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo791.HeadText = "申请单标志";
            clsColumnInfo791.ReadOnly = true;
            clsColumnInfo791.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo792.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo792.BackColor = System.Drawing.Color.White;
            clsColumnInfo792.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo792.ColumnIndex = 21;
            clsColumnInfo792.ColumnName = "Column24";
            clsColumnInfo792.ColumnWidth = 0;
            clsColumnInfo792.Enabled = false;
            clsColumnInfo792.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo792.HeadText = "详细用法";
            clsColumnInfo792.ReadOnly = true;
            clsColumnInfo792.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo793.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo793.BackColor = System.Drawing.Color.White;
            clsColumnInfo793.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo793.ColumnIndex = 22;
            clsColumnInfo793.ColumnName = "Column25";
            clsColumnInfo793.ColumnWidth = 0;
            clsColumnInfo793.Enabled = false;
            clsColumnInfo793.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo793.HeadText = "用法ID";
            clsColumnInfo793.ReadOnly = true;
            clsColumnInfo793.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo794.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo794.BackColor = System.Drawing.Color.White;
            clsColumnInfo794.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo794.ColumnIndex = 23;
            clsColumnInfo794.ColumnName = "Column26";
            clsColumnInfo794.ColumnWidth = 0;
            clsColumnInfo794.Enabled = false;
            clsColumnInfo794.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo794.HeadText = "主诊疗项目ID";
            clsColumnInfo794.ReadOnly = true;
            clsColumnInfo794.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo795.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo795.BackColor = System.Drawing.Color.White;
            clsColumnInfo795.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo795.ColumnIndex = 24;
            clsColumnInfo795.ColumnName = "Column27";
            clsColumnInfo795.ColumnWidth = 0;
            clsColumnInfo795.Enabled = false;
            clsColumnInfo795.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo795.HeadText = "主诊疗项目带出时原基数";
            clsColumnInfo795.ReadOnly = true;
            clsColumnInfo795.TextFont = new System.Drawing.Font("宋体", 10F);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo771);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo772);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo773);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo774);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo775);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo776);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo777);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo778);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo779);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo780);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo781);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo782);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo783);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo784);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo785);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo786);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo787);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo788);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo789);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo790);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo791);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo792);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo793);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo794);
            this.ctlDataGrid5.Columns.Add(clsColumnInfo795);
            this.ctlDataGrid5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlDataGrid5.Font = new System.Drawing.Font("宋体", 11F);
            this.ctlDataGrid5.FullRowSelect = false;
            this.ctlDataGrid5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctlDataGrid5.Location = new System.Drawing.Point(0, 0);
            this.ctlDataGrid5.MultiSelect = false;
            this.ctlDataGrid5.Name = "ctlDataGrid5";
            this.ctlDataGrid5.ReadOnly = false;
            this.ctlDataGrid5.RowHeadersVisible = false;
            this.ctlDataGrid5.RowHeaderWidth = 35;
            this.ctlDataGrid5.SelectedRowBackColor = System.Drawing.Color.Navy;
            this.ctlDataGrid5.SelectedRowForeColor = System.Drawing.Color.White;
            this.ctlDataGrid5.Size = new System.Drawing.Size(842, 409);
            this.ctlDataGrid5.TabIndex = 35;
            this.ctlDataGrid5.m_evtDataGridTextBoxKeyDown += new com.digitalwave.controls.datagrid.clsDGTextKeyEventHandler(this.ctlDataGrid5_m_evtDataGridTextBoxKeyDown);
            this.ctlDataGrid5.m_evtCurrentCellChanged += new System.EventHandler(this.ctlDataGrid5_m_evtCurrentCellChanged);
            this.ctlDataGrid5.m_evtDoubleClickCell += new com.digitalwave.controls.datagrid.clsDGTextMouseClickEventHandler(this.ctlDataGrid5_m_evtDoubleClickCell);
            this.ctlDataGrid5.m_evtDataGridKeyDown += new System.Windows.Forms.KeyEventHandler(this.ctlDataGrid5_m_evtDataGridKeyDown);
            this.ctlDataGrid5.Leave += new System.EventHandler(this.ctlDataGrid5_Leave);
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.ctlDataGrid6);
            this.tabPage10.Location = new System.Drawing.Point(4, 27);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(842, 409);
            this.tabPage10.TabIndex = 9;
            this.tabPage10.Text = "[8]其他";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // ctlDataGrid6
            // 
            this.ctlDataGrid6.AllowAddNew = true;
            this.ctlDataGrid6.AllowDelete = true;
            this.ctlDataGrid6.AutoAppendRow = false;
            this.ctlDataGrid6.AutoScroll = true;
            this.ctlDataGrid6.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ctlDataGrid6.CaptionText = "";
            this.ctlDataGrid6.CaptionVisible = false;
            this.ctlDataGrid6.ColumnHeadersVisible = true;
            clsColumnInfo796.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo796.BackColor = System.Drawing.Color.White;
            clsColumnInfo796.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo796.ColumnIndex = 0;
            clsColumnInfo796.ColumnName = "Column1";
            clsColumnInfo796.ColumnWidth = 75;
            clsColumnInfo796.Enabled = true;
            clsColumnInfo796.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo796.HeadText = "查询";
            clsColumnInfo796.ReadOnly = false;
            clsColumnInfo796.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo797.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo797.BackColor = System.Drawing.Color.White;
            clsColumnInfo797.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo797.ColumnIndex = 1;
            clsColumnInfo797.ColumnName = "Column2";
            clsColumnInfo797.ColumnWidth = 60;
            clsColumnInfo797.Enabled = true;
            clsColumnInfo797.ForeColor = System.Drawing.Color.Red;
            clsColumnInfo797.HeadText = "数量";
            clsColumnInfo797.ReadOnly = false;
            clsColumnInfo797.TextFont = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            clsColumnInfo798.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo798.BackColor = System.Drawing.Color.White;
            clsColumnInfo798.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo798.ColumnIndex = 2;
            clsColumnInfo798.ColumnName = "Column3";
            clsColumnInfo798.ColumnWidth = 222;
            clsColumnInfo798.Enabled = true;
            clsColumnInfo798.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo798.HeadText = "项目名称";
            clsColumnInfo798.ReadOnly = true;
            clsColumnInfo798.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo799.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo799.BackColor = System.Drawing.Color.White;
            clsColumnInfo799.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo799.ColumnIndex = 3;
            clsColumnInfo799.ColumnName = "Column4";
            clsColumnInfo799.ColumnWidth = 130;
            clsColumnInfo799.Enabled = true;
            clsColumnInfo799.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo799.HeadText = "规格";
            clsColumnInfo799.ReadOnly = true;
            clsColumnInfo799.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo800.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo800.BackColor = System.Drawing.Color.White;
            clsColumnInfo800.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo800.ColumnIndex = 4;
            clsColumnInfo800.ColumnName = "Column5";
            clsColumnInfo800.ColumnWidth = 50;
            clsColumnInfo800.Enabled = true;
            clsColumnInfo800.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo800.HeadText = "单位";
            clsColumnInfo800.ReadOnly = true;
            clsColumnInfo800.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo801.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo801.BackColor = System.Drawing.Color.White;
            clsColumnInfo801.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo801.ColumnIndex = 5;
            clsColumnInfo801.ColumnName = "Column6";
            clsColumnInfo801.ColumnWidth = 69;
            clsColumnInfo801.Enabled = true;
            clsColumnInfo801.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo801.HeadText = "单价";
            clsColumnInfo801.ReadOnly = false;
            clsColumnInfo801.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo802.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo802.BackColor = System.Drawing.Color.White;
            clsColumnInfo802.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo802.ColumnIndex = 6;
            clsColumnInfo802.ColumnName = "Column7";
            clsColumnInfo802.ColumnWidth = 69;
            clsColumnInfo802.Enabled = true;
            clsColumnInfo802.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo802.HeadText = "总价";
            clsColumnInfo802.ReadOnly = true;
            clsColumnInfo802.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo803.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo803.BackColor = System.Drawing.Color.White;
            clsColumnInfo803.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo803.ColumnIndex = 7;
            clsColumnInfo803.ColumnName = "Column10";
            clsColumnInfo803.ColumnWidth = 0;
            clsColumnInfo803.Enabled = true;
            clsColumnInfo803.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo803.HeadText = "ID";
            clsColumnInfo803.ReadOnly = false;
            clsColumnInfo803.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo804.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo804.BackColor = System.Drawing.Color.White;
            clsColumnInfo804.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo804.ColumnIndex = 8;
            clsColumnInfo804.ColumnName = "Column11";
            clsColumnInfo804.ColumnWidth = 0;
            clsColumnInfo804.Enabled = true;
            clsColumnInfo804.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo804.HeadText = "是否自定义价格";
            clsColumnInfo804.ReadOnly = true;
            clsColumnInfo804.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo805.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo805.BackColor = System.Drawing.Color.White;
            clsColumnInfo805.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Int;
            clsColumnInfo805.ColumnIndex = 9;
            clsColumnInfo805.ColumnName = "Column12";
            clsColumnInfo805.ColumnWidth = 0;
            clsColumnInfo805.Enabled = true;
            clsColumnInfo805.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo805.HeadText = "行号";
            clsColumnInfo805.ReadOnly = true;
            clsColumnInfo805.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo806.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            clsColumnInfo806.BackColor = System.Drawing.Color.White;
            clsColumnInfo806.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo806.ColumnIndex = 10;
            clsColumnInfo806.ColumnName = "Column13";
            clsColumnInfo806.ColumnWidth = 75;
            clsColumnInfo806.Enabled = true;
            clsColumnInfo806.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo806.HeadText = "收费比例";
            clsColumnInfo806.ReadOnly = true;
            clsColumnInfo806.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo807.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo807.BackColor = System.Drawing.Color.White;
            clsColumnInfo807.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo807.ColumnIndex = 11;
            clsColumnInfo807.ColumnName = "Column14";
            clsColumnInfo807.ColumnWidth = 0;
            clsColumnInfo807.Enabled = true;
            clsColumnInfo807.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo807.HeadText = "比例值";
            clsColumnInfo807.ReadOnly = true;
            clsColumnInfo807.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo808.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo808.BackColor = System.Drawing.Color.White;
            clsColumnInfo808.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo808.ColumnIndex = 12;
            clsColumnInfo808.ColumnName = "Column15";
            clsColumnInfo808.ColumnWidth = 0;
            clsColumnInfo808.Enabled = false;
            clsColumnInfo808.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo808.HeadText = "发票分类";
            clsColumnInfo808.ReadOnly = true;
            clsColumnInfo808.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo809.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo809.BackColor = System.Drawing.Color.White;
            clsColumnInfo809.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo809.ColumnIndex = 13;
            clsColumnInfo809.ColumnName = "Column16";
            clsColumnInfo809.ColumnWidth = 0;
            clsColumnInfo809.Enabled = false;
            clsColumnInfo809.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo809.HeadText = "附加项目ID";
            clsColumnInfo809.ReadOnly = true;
            clsColumnInfo809.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo810.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo810.BackColor = System.Drawing.Color.White;
            clsColumnInfo810.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo810.ColumnIndex = 14;
            clsColumnInfo810.ColumnName = "Column17";
            clsColumnInfo810.ColumnWidth = 0;
            clsColumnInfo810.Enabled = false;
            clsColumnInfo810.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo810.HeadText = "附加项目原数量";
            clsColumnInfo810.ReadOnly = true;
            clsColumnInfo810.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo811.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo811.BackColor = System.Drawing.Color.White;
            clsColumnInfo811.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo811.ColumnIndex = 15;
            clsColumnInfo811.ColumnName = "Column18";
            clsColumnInfo811.ColumnWidth = 0;
            clsColumnInfo811.Enabled = false;
            clsColumnInfo811.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo811.HeadText = "英文名";
            clsColumnInfo811.ReadOnly = true;
            clsColumnInfo811.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo812.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo812.BackColor = System.Drawing.Color.White;
            clsColumnInfo812.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo812.ColumnIndex = 16;
            clsColumnInfo812.ColumnName = "Column19";
            clsColumnInfo812.ColumnWidth = 0;
            clsColumnInfo812.Enabled = false;
            clsColumnInfo812.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo812.HeadText = "预留";
            clsColumnInfo812.ReadOnly = true;
            clsColumnInfo812.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo813.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo813.BackColor = System.Drawing.Color.White;
            clsColumnInfo813.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo813.ColumnIndex = 17;
            clsColumnInfo813.ColumnName = "Column20";
            clsColumnInfo813.ColumnWidth = 0;
            clsColumnInfo813.Enabled = false;
            clsColumnInfo813.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo813.HeadText = "申请单ID";
            clsColumnInfo813.ReadOnly = true;
            clsColumnInfo813.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo814.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo814.BackColor = System.Drawing.Color.White;
            clsColumnInfo814.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo814.ColumnIndex = 18;
            clsColumnInfo814.ColumnName = "Column21";
            clsColumnInfo814.ColumnWidth = 0;
            clsColumnInfo814.Enabled = false;
            clsColumnInfo814.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo814.HeadText = "关联项目ID";
            clsColumnInfo814.ReadOnly = true;
            clsColumnInfo814.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo815.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo815.BackColor = System.Drawing.Color.White;
            clsColumnInfo815.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_Decimal;
            clsColumnInfo815.ColumnIndex = 19;
            clsColumnInfo815.ColumnName = "Column22";
            clsColumnInfo815.ColumnWidth = 0;
            clsColumnInfo815.Enabled = false;
            clsColumnInfo815.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo815.HeadText = "主项默认用量";
            clsColumnInfo815.ReadOnly = true;
            clsColumnInfo815.TextFont = new System.Drawing.Font("宋体", 11F);
            clsColumnInfo816.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo816.BackColor = System.Drawing.Color.White;
            clsColumnInfo816.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo816.ColumnIndex = 20;
            clsColumnInfo816.ColumnName = "Column23";
            clsColumnInfo816.ColumnWidth = 0;
            clsColumnInfo816.Enabled = false;
            clsColumnInfo816.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo816.HeadText = "详细用法";
            clsColumnInfo816.ReadOnly = true;
            clsColumnInfo816.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo817.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            clsColumnInfo817.BackColor = System.Drawing.Color.White;
            clsColumnInfo817.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo817.ColumnIndex = 21;
            clsColumnInfo817.ColumnName = "Column24";
            clsColumnInfo817.ColumnWidth = 60;
            clsColumnInfo817.Enabled = true;
            clsColumnInfo817.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo817.HeadText = "科备药";
            clsColumnInfo817.ReadOnly = false;
            clsColumnInfo817.TextFont = new System.Drawing.Font("宋体", 10F);
            clsColumnInfo818.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            clsColumnInfo818.BackColor = System.Drawing.Color.White;
            clsColumnInfo818.Column_Type = com.digitalwave.controls.datagrid.enum_DataType.System_String;
            clsColumnInfo818.ColumnIndex = 22;
            clsColumnInfo818.ColumnName = "Column25";
            clsColumnInfo818.ColumnWidth = 0;
            clsColumnInfo818.Enabled = false;
            clsColumnInfo818.ForeColor = System.Drawing.Color.Black;
            clsColumnInfo818.HeadText = "科备药ID";
            clsColumnInfo818.ReadOnly = true;
            clsColumnInfo818.TextFont = new System.Drawing.Font("宋体", 10F);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo796);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo797);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo798);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo799);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo800);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo801);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo802);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo803);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo804);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo805);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo806);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo807);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo808);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo809);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo810);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo811);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo812);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo813);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo814);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo815);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo816);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo817);
            this.ctlDataGrid6.Columns.Add(clsColumnInfo818);
            this.ctlDataGrid6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlDataGrid6.Font = new System.Drawing.Font("宋体", 11F);
            this.ctlDataGrid6.FullRowSelect = false;
            this.ctlDataGrid6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctlDataGrid6.Location = new System.Drawing.Point(0, 0);
            this.ctlDataGrid6.MultiSelect = false;
            this.ctlDataGrid6.Name = "ctlDataGrid6";
            this.ctlDataGrid6.ReadOnly = false;
            this.ctlDataGrid6.RowHeadersVisible = false;
            this.ctlDataGrid6.RowHeaderWidth = 35;
            this.ctlDataGrid6.SelectedRowBackColor = System.Drawing.Color.Navy;
            this.ctlDataGrid6.SelectedRowForeColor = System.Drawing.Color.White;
            this.ctlDataGrid6.Size = new System.Drawing.Size(842, 409);
            this.ctlDataGrid6.TabIndex = 35;
            this.ctlDataGrid6.m_evtDataGridTextBoxKeyDown += new com.digitalwave.controls.datagrid.clsDGTextKeyEventHandler(this.ctlDataGrid6_m_evtDataGridTextBoxKeyDown);
            this.ctlDataGrid6.m_evtCurrentCellChanged += new System.EventHandler(this.ctlDataGrid6_m_evtCurrentCellChanged);
            this.ctlDataGrid6.m_evtDataGridKeyDown += new System.Windows.Forms.KeyEventHandler(this.ctlDataGrid6_m_evtDataGridKeyDown);
            this.ctlDataGrid6.Leave += new System.EventHandler(this.ctlDataGrid6_Leave);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 446);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(850, 1);
            this.splitter1.TabIndex = 33;
            this.splitter1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader2,
            this.columnHeader26,
            this.columnHeader24,
            this.columnHeader20,
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader19,
            this.columnHeader27,
            this.columnHeader12,
            this.columnHeader25});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView1.Font = new System.Drawing.Font("宋体", 12F);
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(0, 447);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(850, 24);
            this.listView1.TabIndex = 32;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Visible = false;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.Leave += new System.EventHandler(this.listView1_Leave);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "查询码";
            this.columnHeader9.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "商品名";
            this.columnHeader2.Width = 155;
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "通用名";
            this.columnHeader26.Width = 100;
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "英文名";
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "类型";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "规格";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "常用量";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "单位";
            this.columnHeader4.Width = 45;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "单价";
            this.columnHeader5.Width = 71;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "比例";
            this.columnHeader19.Width = 45;
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "医保";
            this.columnHeader27.Width = 45;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "";
            this.columnHeader12.Width = 45;
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "发票分类";
            this.columnHeader25.Width = 0;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(850, 6);
            this.panel3.TabIndex = 35;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.FF);
            this.panel4.Controls.Add(this.EE);
            this.panel4.Controls.Add(this.DD);
            this.panel4.Controls.Add(this.CC);
            this.panel4.Controls.Add(this.BB);
            this.panel4.Controls.Add(this.AA);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Font = new System.Drawing.Font("宋体", 10.5F);
            this.panel4.Location = new System.Drawing.Point(0, 471);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(850, 32);
            this.panel4.TabIndex = 36;
            // 
            // FF
            // 
            this.FF.AutoSize = true;
            this.FF.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FF.ForeColor = System.Drawing.Color.Black;
            this.FF.Location = new System.Drawing.Point(749, 6);
            this.FF.Name = "FF";
            this.FF.Size = new System.Drawing.Size(24, 16);
            this.FF.TabIndex = 11;
            this.FF.Text = "FF";
            // 
            // EE
            // 
            this.EE.AutoSize = true;
            this.EE.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EE.ForeColor = System.Drawing.Color.Black;
            this.EE.Location = new System.Drawing.Point(629, 6);
            this.EE.Name = "EE";
            this.EE.Size = new System.Drawing.Size(24, 16);
            this.EE.TabIndex = 10;
            this.EE.Text = "EE";
            // 
            // DD
            // 
            this.DD.AutoSize = true;
            this.DD.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DD.ForeColor = System.Drawing.Color.Black;
            this.DD.Location = new System.Drawing.Point(462, 6);
            this.DD.Name = "DD";
            this.DD.Size = new System.Drawing.Size(24, 16);
            this.DD.TabIndex = 9;
            this.DD.Text = "DD";
            // 
            // CC
            // 
            this.CC.AutoSize = true;
            this.CC.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CC.ForeColor = System.Drawing.Color.Black;
            this.CC.Location = new System.Drawing.Point(330, 6);
            this.CC.Name = "CC";
            this.CC.Size = new System.Drawing.Size(24, 16);
            this.CC.TabIndex = 8;
            this.CC.Text = "CC";
            // 
            // BB
            // 
            this.BB.AutoSize = true;
            this.BB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BB.ForeColor = System.Drawing.Color.Black;
            this.BB.Location = new System.Drawing.Point(203, 6);
            this.BB.Name = "BB";
            this.BB.Size = new System.Drawing.Size(24, 16);
            this.BB.TabIndex = 7;
            this.BB.Text = "BB";
            // 
            // AA
            // 
            this.AA.AutoSize = true;
            this.AA.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AA.ForeColor = System.Drawing.Color.Black;
            this.AA.Location = new System.Drawing.Point(70, 6);
            this.AA.Name = "AA";
            this.AA.Size = new System.Drawing.Size(24, 16);
            this.AA.TabIndex = 6;
            this.AA.Text = "AA";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(692, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "其他费:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(536, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "手术/治疗费:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(404, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "检查费:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "检验费:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "中药费:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 8);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(56, 14);
            this.label22.TabIndex = 0;
            this.label22.Text = "西药费:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.rdoZzsq);
            this.panel1.Controls.Add(this.label33);
            this.panel1.Controls.Add(this.lblESBCardVerify);
            this.panel1.Controls.Add(this.cboProxyBoilMed);
            this.panel1.Controls.Add(this.cmbRecipeType);
            this.panel1.Controls.Add(this.m_cmbFind);
            this.panel1.Controls.Add(this.btnApp);
            this.panel1.Controls.Add(this.btnNotice);
            this.panel1.Controls.Add(this.btnWAC);
            this.panel1.Controls.Add(this.btnBooking);
            this.panel1.Controls.Add(this.label32);
            this.panel1.Controls.Add(this.txtWeight);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.btnPrefer);
            this.panel1.Controls.Add(this.lbeSbAccPay);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.txtLoadRecipeNo1);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.btnRecalc);
            this.panel1.Controls.Add(this.lbeFlag);
            this.panel1.Controls.Add(this.btPutIn);
            this.panel1.Controls.Add(this.btCaseHistory);
            this.panel1.Controls.Add(this.btReGroup);
            this.panel1.Controls.Add(this.btNew);
            this.panel1.Controls.Add(this.btInject);
            this.panel1.Controls.Add(this.btCaseyHistory);
            this.panel1.Controls.Add(this.groupBox7);
            this.panel1.Controls.Add(this.lbeSelfPay);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.lbeChargeUp);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.BtExit);
            this.panel1.Controls.Add(this.btClear);
            this.panel1.Controls.Add(this.lbeSumMoney);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.btSave);
            this.panel1.Controls.Add(this.m_btnEndTake);
            this.panel1.Controls.Add(this.btPrint);
            this.panel1.Controls.Add(this.m_btnBackWait);
            this.panel1.Controls.Add(this.btDel);
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.m_cmbRecipeType);
            this.panel1.Controls.Add(this.btReUse);
            this.panel1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.panel1.Location = new System.Drawing.Point(856, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(172, 600);
            this.panel1.TabIndex = 0;
            // 
            // rdoZzsq
            // 
            this.rdoZzsq.Location = new System.Drawing.Point(77, 175);
            this.rdoZzsq.Name = "rdoZzsq";
            this.rdoZzsq.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rdoZzsq.Properties.Appearance.Options.UseBackColor = true;
            this.rdoZzsq.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rdoZzsq.Properties.Columns = 2;
            this.rdoZzsq.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "否"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "是")});
            this.rdoZzsq.Size = new System.Drawing.Size(99, 28);
            this.rdoZzsq.TabIndex = 136;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("宋体", 10F);
            this.label33.ForeColor = System.Drawing.Color.Black;
            this.label33.Location = new System.Drawing.Point(5, 180);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(70, 14);
            this.label33.TabIndex = 135;
            this.label33.Text = "转诊社区:";
            // 
            // lblESBCardVerify
            // 
            this.lblESBCardVerify.AutoSize = true;
            this.lblESBCardVerify.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblESBCardVerify.Font = new System.Drawing.Font("宋体", 10F);
            this.lblESBCardVerify.ForeColor = System.Drawing.Color.Black;
            this.lblESBCardVerify.Location = new System.Drawing.Point(5, 211);
            this.lblESBCardVerify.Name = "lblESBCardVerify";
            this.lblESBCardVerify.Size = new System.Drawing.Size(154, 14);
            this.lblESBCardVerify.TabIndex = 134;
            this.lblESBCardVerify.Text = "电子社保卡照片验证 »»";
            this.lblESBCardVerify.Click += new System.EventHandler(this.lblESBCardVerify_Click);
            this.lblESBCardVerify.MouseEnter += new System.EventHandler(this.lblESBCardVerify_MouseEnter);
            this.lblESBCardVerify.MouseLeave += new System.EventHandler(this.lblESBCardVerify_MouseLeave);
            // 
            // cboProxyBoilMed
            // 
            this.cboProxyBoilMed.DropDownHeight = 125;
            this.cboProxyBoilMed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProxyBoilMed.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboProxyBoilMed.ForeColor = System.Drawing.Color.Blue;
            this.cboProxyBoilMed.FormattingEnabled = true;
            this.cboProxyBoilMed.IntegralHeight = false;
            this.cboProxyBoilMed.ItemHeight = 14;
            this.cboProxyBoilMed.Items.AddRange(new object[] {
            "",
            "代煎代送",
            "中药代送"});
            this.cboProxyBoilMed.Location = new System.Drawing.Point(75, 145);
            this.cboProxyBoilMed.Name = "cboProxyBoilMed";
            this.cboProxyBoilMed.Size = new System.Drawing.Size(86, 22);
            this.cboProxyBoilMed.TabIndex = 129;
            this.cboProxyBoilMed.SelectedIndexChanged += new System.EventHandler(this.cboProxyBoilMed_SelectedIndexChanged);
            // 
            // cmbRecipeType
            // 
            this.cmbRecipeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecipeType.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbRecipeType.ItemHeight = 14;
            this.cmbRecipeType.Location = new System.Drawing.Point(75, 85);
            this.cmbRecipeType.Name = "cmbRecipeType";
            this.cmbRecipeType.Size = new System.Drawing.Size(86, 22);
            this.cmbRecipeType.TabIndex = 0;
            // 
            // btnApp
            // 
            this.btnApp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnApp.DefaultScheme = true;
            this.btnApp.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnApp.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnApp.Hint = "先诊后结通道";
            this.btnApp.Location = new System.Drawing.Point(85, 427);
            this.btnApp.Name = "btnApp";
            this.btnApp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnApp.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnApp.Size = new System.Drawing.Size(76, 31);
            this.btnApp.TabIndex = 133;
            this.btnApp.Text = "电子申请单";
            this.btnApp.Click += new System.EventHandler(this.btnApp_Click);
            // 
            // btnNotice
            // 
            this.btnNotice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnNotice.DefaultScheme = true;
            this.btnNotice.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnNotice.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnNotice.Hint = "先诊后结通道";
            this.btnNotice.Location = new System.Drawing.Point(85, 391);
            this.btnNotice.Name = "btnNotice";
            this.btnNotice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnNotice.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnNotice.Size = new System.Drawing.Size(76, 31);
            this.btnNotice.TabIndex = 132;
            this.btnNotice.Text = "入院通知书";
            this.btnNotice.Click += new System.EventHandler(this.btnNotice_Click);
            // 
            // btnWAC
            // 
            this.btnWAC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnWAC.DefaultScheme = true;
            this.btnWAC.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnWAC.Hint = "先诊后结通道";
            this.btnWAC.Location = new System.Drawing.Point(85, 463);
            this.btnWAC.Name = "btnWAC";
            this.btnWAC.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnWAC.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnWAC.Size = new System.Drawing.Size(76, 31);
            this.btnWAC.TabIndex = 131;
            this.btnWAC.Text = "妇幼平台";
            this.btnWAC.Click += new System.EventHandler(this.btnWAC_Click);
            // 
            // btnBooking
            // 
            this.btnBooking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnBooking.DefaultScheme = true;
            this.btnBooking.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnBooking.Hint = "先诊后结通道";
            this.btnBooking.Location = new System.Drawing.Point(5, 357);
            this.btnBooking.Name = "btnBooking";
            this.btnBooking.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnBooking.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnBooking.Size = new System.Drawing.Size(76, 31);
            this.btnBooking.TabIndex = 130;
            this.btnBooking.Text = "诊间预约";
            this.btnBooking.Click += new System.EventHandler(this.btnBooking_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("宋体", 10F);
            this.label32.ForeColor = System.Drawing.Color.Black;
            this.label32.Location = new System.Drawing.Point(5, 149);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(70, 14);
            this.label32.TabIndex = 128;
            this.label32.Text = "院外代送:";
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(75, 115);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(86, 23);
            this.txtWeight.TabIndex = 126;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("宋体", 10F);
            this.label31.Location = new System.Drawing.Point(5, 118);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(70, 14);
            this.label31.TabIndex = 125;
            this.label31.Text = "体重(KG):";
            // 
            // btnPrefer
            // 
            this.btnPrefer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnPrefer.DefaultScheme = true;
            this.btnPrefer.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPrefer.Hint = "先诊后结通道";
            this.btnPrefer.Location = new System.Drawing.Point(5, 391);
            this.btnPrefer.Name = "btnPrefer";
            this.btnPrefer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnPrefer.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnPrefer.Size = new System.Drawing.Size(76, 31);
            this.btnPrefer.TabIndex = 124;
            this.btnPrefer.Text = "先诊后结";
            this.btnPrefer.Click += new System.EventHandler(this.btnPrefer_Click);
            // 
            // lbeSbAccPay
            // 
            this.lbeSbAccPay.AutoSize = true;
            this.lbeSbAccPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbeSbAccPay.ForeColor = System.Drawing.Color.Black;
            this.lbeSbAccPay.Location = new System.Drawing.Point(90, 273);
            this.lbeSbAccPay.Name = "lbeSbAccPay";
            this.lbeSbAccPay.Size = new System.Drawing.Size(0, 20);
            this.lbeSbAccPay.TabIndex = 53;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("宋体", 10F);
            this.label30.ForeColor = System.Drawing.Color.Black;
            this.label30.Location = new System.Drawing.Point(5, 273);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(70, 14);
            this.label30.TabIndex = 52;
            this.label30.Text = "社保记账:";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(126, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(27, 13);
            this.groupBox3.TabIndex = 51;
            this.groupBox3.TabStop = false;
            this.groupBox3.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(5, 89);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 14);
            this.label16.TabIndex = 50;
            this.label16.Text = "处方类型:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 10F);
            this.label15.Location = new System.Drawing.Point(5, 34);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 14);
            this.label15.TabIndex = 49;
            this.label15.Text = "调处方:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10F);
            this.label14.Location = new System.Drawing.Point(5, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 14);
            this.label14.TabIndex = 48;
            this.label14.Text = "查找方式:";
            // 
            // lbeFlag
            // 
            this.lbeFlag.Location = new System.Drawing.Point(8, 664);
            this.lbeFlag.Name = "lbeFlag";
            this.lbeFlag.Size = new System.Drawing.Size(80, 24);
            this.lbeFlag.TabIndex = 46;
            this.lbeFlag.Text = "lalbe";
            this.lbeFlag.Visible = false;
            this.lbeFlag.TextChanged += new System.EventHandler(this.lbeFlag_TextChanged);
            // 
            // btPutIn
            // 
            this.btPutIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btPutIn.DefaultScheme = true;
            this.btPutIn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btPutIn.Hint = "提交已开的处方,提交后不能修改";
            this.btPutIn.Location = new System.Drawing.Point(85, 357);
            this.btPutIn.Name = "btPutIn";
            this.btPutIn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btPutIn.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btPutIn.Size = new System.Drawing.Size(76, 32);
            this.btPutIn.TabIndex = 1;
            this.btPutIn.Text = "提交(&T)";
            this.btPutIn.Click += new System.EventHandler(this.btPutIn_Click);
            // 
            // btCaseHistory
            // 
            this.btCaseHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btCaseHistory.DefaultScheme = true;
            this.btCaseHistory.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btCaseHistory.Hint = "查看病人的历史病史";
            this.btCaseHistory.Location = new System.Drawing.Point(5, 564);
            this.btCaseHistory.Name = "btCaseHistory";
            this.btCaseHistory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btCaseHistory.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btCaseHistory.Size = new System.Drawing.Size(76, 32);
            this.btCaseHistory.TabIndex = 12;
            this.btCaseHistory.Text = "查病历(&B)";
            this.btCaseHistory.Click += new System.EventHandler(this.btCaseHistory_Click);
            // 
            // btReGroup
            // 
            this.btReGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btReGroup.DefaultScheme = true;
            this.btReGroup.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btReGroup.Enabled = false;
            this.btReGroup.Hint = "把西药处方按方号重新排列";
            this.btReGroup.Location = new System.Drawing.Point(5, 598);
            this.btReGroup.Name = "btReGroup";
            this.btReGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btReGroup.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btReGroup.Size = new System.Drawing.Size(76, 32);
            this.btReGroup.TabIndex = 13;
            this.btReGroup.Text = "重组(&M)";
            this.btReGroup.Click += new System.EventHandler(this.btReGroup_Click);
            // 
            // btNew
            // 
            this.btNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btNew.DefaultScheme = true;
            this.btNew.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btNew.Hint = "开一张新的处方";
            this.btNew.Location = new System.Drawing.Point(5, 322);
            this.btNew.Name = "btNew";
            this.btNew.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btNew.Size = new System.Drawing.Size(76, 32);
            this.btNew.TabIndex = 7;
            this.btNew.Text = "新建(&N)";
            this.btNew.Click += new System.EventHandler(this.btNew_Click);
            // 
            // btInject
            // 
            this.btInject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btInject.DefaultScheme = true;
            this.btInject.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btInject.Hint = "打印治疗注射单";
            this.btInject.Location = new System.Drawing.Point(176, 509);
            this.btInject.Name = "btInject";
            this.btInject.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btInject.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btInject.Size = new System.Drawing.Size(76, 32);
            this.btInject.TabIndex = 4;
            this.btInject.Text = "注射单(&I)";
            this.btInject.Visible = false;
            this.btInject.Click += new System.EventHandler(this.btInject_Click);
            // 
            // btCaseyHistory
            // 
            this.btCaseyHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btCaseyHistory.DefaultScheme = true;
            this.btCaseyHistory.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btCaseyHistory.Hint = "打印病历";
            this.btCaseyHistory.Location = new System.Drawing.Point(85, 530);
            this.btCaseyHistory.Name = "btCaseyHistory";
            this.btCaseyHistory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btCaseyHistory.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btCaseyHistory.Size = new System.Drawing.Size(76, 32);
            this.btCaseyHistory.TabIndex = 3;
            this.btCaseyHistory.Text = "打病历(&P)";
            this.btCaseyHistory.Click += new System.EventHandler(this.btCaseyHistory_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.picops);
            this.groupBox7.Controls.Add(this.picris);
            this.groupBox7.Controls.Add(this.piclis);
            this.groupBox7.Font = new System.Drawing.Font("新宋体", 9F);
            this.groupBox7.Location = new System.Drawing.Point(-172, 194);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(151, 56);
            this.groupBox7.TabIndex = 44;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "开检验检查手术申请";
            this.groupBox7.Visible = false;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("新宋体", 10F);
            this.label13.Location = new System.Drawing.Point(104, 92);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 16);
            this.label13.TabIndex = 9;
            this.label13.Text = "手术";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("新宋体", 10F);
            this.label12.Location = new System.Drawing.Point(58, 92);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 16);
            this.label12.TabIndex = 8;
            this.label12.Text = "检查";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("新宋体", 10F);
            this.label11.Location = new System.Drawing.Point(12, 92);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 16);
            this.label11.TabIndex = 7;
            this.label11.Text = "检验";
            // 
            // lbeSelfPay
            // 
            this.lbeSelfPay.AutoSize = true;
            this.lbeSelfPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbeSelfPay.ForeColor = System.Drawing.Color.Black;
            this.lbeSelfPay.Location = new System.Drawing.Point(90, 244);
            this.lbeSelfPay.Name = "lbeSelfPay";
            this.lbeSelfPay.Size = new System.Drawing.Size(0, 20);
            this.lbeSelfPay.TabIndex = 33;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("宋体", 10F);
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Location = new System.Drawing.Point(82, 248);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(70, 14);
            this.label23.TabIndex = 32;
            this.label23.Text = "记账金额:";
            this.label23.Visible = false;
            // 
            // lbeChargeUp
            // 
            this.lbeChargeUp.AutoSize = true;
            this.lbeChargeUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbeChargeUp.ForeColor = System.Drawing.Color.Black;
            this.lbeChargeUp.Location = new System.Drawing.Point(158, 254);
            this.lbeChargeUp.Name = "lbeChargeUp";
            this.lbeChargeUp.Size = new System.Drawing.Size(0, 20);
            this.lbeChargeUp.TabIndex = 31;
            this.lbeChargeUp.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(5, 247);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 14);
            this.label8.TabIndex = 30;
            this.label8.Text = "自付金额:";
            // 
            // BtExit
            // 
            this.BtExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.BtExit.DefaultScheme = true;
            this.BtExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.BtExit.Hint = "退出系统";
            this.BtExit.Location = new System.Drawing.Point(85, 598);
            this.BtExit.Name = "BtExit";
            this.BtExit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.BtExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.BtExit.Size = new System.Drawing.Size(76, 32);
            this.BtExit.TabIndex = 6;
            this.BtExit.Text = "退出(ESC)";
            this.BtExit.Click += new System.EventHandler(this.BtExit_Click);
            // 
            // btClear
            // 
            this.btClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btClear.DefaultScheme = true;
            this.btClear.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btClear.Hint = "清空病历和处方内容";
            this.btClear.Location = new System.Drawing.Point(5, 530);
            this.btClear.Name = "btClear";
            this.btClear.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btClear.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btClear.Size = new System.Drawing.Size(76, 32);
            this.btClear.TabIndex = 11;
            this.btClear.Text = "清空(&Q)";
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // lbeSumMoney
            // 
            this.lbeSumMoney.AutoSize = true;
            this.lbeSumMoney.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbeSumMoney.ForeColor = System.Drawing.Color.Black;
            this.lbeSumMoney.Location = new System.Drawing.Point(90, 298);
            this.lbeSumMoney.Name = "lbeSumMoney";
            this.lbeSumMoney.Size = new System.Drawing.Size(0, 20);
            this.lbeSumMoney.TabIndex = 20;
            this.lbeSumMoney.TextChanged += new System.EventHandler(this.lbeSumMoney_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 10F);
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(5, 299);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(70, 14);
            this.label21.TabIndex = 19;
            this.label21.Text = "总    额:";
            // 
            // btSave
            // 
            this.btSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btSave.DefaultScheme = true;
            this.btSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btSave.Hint = "保存所有数据";
            this.btSave.Location = new System.Drawing.Point(85, 322);
            this.btSave.Name = "btSave";
            this.btSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btSave.Size = new System.Drawing.Size(76, 32);
            this.btSave.TabIndex = 0;
            this.btSave.Text = "保存(F3)";
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // m_btnEndTake
            // 
            this.m_btnEndTake.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_btnEndTake.DefaultScheme = true;
            this.m_btnEndTake.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnEndTake.Hint = "把当前病人结论诊";
            this.m_btnEndTake.Location = new System.Drawing.Point(5, 462);
            this.m_btnEndTake.Name = "m_btnEndTake";
            this.m_btnEndTake.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_btnEndTake.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnEndTake.Size = new System.Drawing.Size(76, 32);
            this.m_btnEndTake.TabIndex = 9;
            this.m_btnEndTake.Text = "结诊(&O)";
            this.m_btnEndTake.Click += new System.EventHandler(this.m_btnEndTake_Click);
            // 
            // btPrint
            // 
            this.btPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btPrint.DefaultScheme = true;
            this.btPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btPrint.Hint = "打印处方";
            this.btPrint.Location = new System.Drawing.Point(85, 496);
            this.btPrint.Name = "btPrint";
            this.btPrint.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btPrint.Size = new System.Drawing.Size(76, 32);
            this.btPrint.TabIndex = 2;
            this.btPrint.Text = "打处方(&L)";
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // m_btnBackWait
            // 
            this.m_btnBackWait.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_btnBackWait.DefaultScheme = true;
            this.m_btnBackWait.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnBackWait.Hint = "把病人退回候诊列表";
            this.m_btnBackWait.Location = new System.Drawing.Point(5, 496);
            this.m_btnBackWait.Name = "m_btnBackWait";
            this.m_btnBackWait.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_btnBackWait.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnBackWait.Size = new System.Drawing.Size(76, 32);
            this.m_btnBackWait.TabIndex = 10;
            this.m_btnBackWait.Text = "候诊(&W)";
            this.m_btnBackWait.Click += new System.EventHandler(this.m_btnBackWait_Click);
            // 
            // btDel
            // 
            this.btDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btDel.DefaultScheme = true;
            this.btDel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btDel.Hint = "作废处方,已收费处方不能作废";
            this.btDel.Location = new System.Drawing.Point(85, 564);
            this.btDel.Name = "btDel";
            this.btDel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btDel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btDel.Size = new System.Drawing.Size(76, 32);
            this.btDel.TabIndex = 5;
            this.btDel.Text = "作废(&Z)";
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtFindAccordRecipe);
            this.groupBox6.Controls.Add(this.cmbFindAccordRecipe);
            this.groupBox6.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(-168, 202);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(146, 16);
            this.groupBox6.TabIndex = 43;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "处方模板";
            this.groupBox6.Visible = false;
            // 
            // txtFindAccordRecipe
            // 
            this.txtFindAccordRecipe.Location = new System.Drawing.Point(8, 44);
            this.txtFindAccordRecipe.Name = "txtFindAccordRecipe";
            this.txtFindAccordRecipe.Size = new System.Drawing.Size(136, 23);
            this.txtFindAccordRecipe.TabIndex = 39;
            this.txtFindAccordRecipe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFindAccordRecipe_KeyDown);
            // 
            // cmbFindAccordRecipe
            // 
            this.cmbFindAccordRecipe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFindAccordRecipe.Items.AddRange(new object[] {
            "按名称查找",
            "按助记码查找",
            "按拼音码查找",
            "按五笔码查找"});
            this.cmbFindAccordRecipe.Location = new System.Drawing.Point(8, 20);
            this.cmbFindAccordRecipe.Name = "cmbFindAccordRecipe";
            this.cmbFindAccordRecipe.Size = new System.Drawing.Size(136, 22);
            this.cmbFindAccordRecipe.TabIndex = 0;
            this.cmbFindAccordRecipe.SelectedIndexChanged += new System.EventHandler(this.cmbFindAccordRecipe_SelectedIndexChanged);
            // 
            // btReUse
            // 
            this.btReUse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btReUse.DefaultScheme = true;
            this.btReUse.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btReUse.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.btReUse.Hint = "重新使用当前处方内容";
            this.btReUse.Location = new System.Drawing.Point(110, 56);
            this.btReUse.Name = "btReUse";
            this.btReUse.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btReUse.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btReUse.Size = new System.Drawing.Size(38, 15);
            this.btReUse.TabIndex = 1;
            this.btReUse.Text = "复用";
            this.btReUse.Visible = false;
            this.btReUse.Click += new System.EventHandler(this.btReUse_Click);
            // 
            // lblIsVip
            // 
            this.lblIsVip.BackColor = System.Drawing.Color.Transparent;
            this.lblIsVip.Enabled = false;
            this.lblIsVip.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIsVip.ForeColor = System.Drawing.Color.Blue;
            this.lblIsVip.Location = new System.Drawing.Point(527, 40);
            this.lblIsVip.Name = "lblIsVip";
            this.lblIsVip.Size = new System.Drawing.Size(119, 13);
            this.lblIsVip.TabIndex = 123;
            this.lblIsVip.Text = "符合先诊疗后结算";
            this.lblIsVip.Click += new System.EventHandler(this.lblIsVip_Click);
            // 
            // lblFunction
            // 
            this.lblFunction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFunction.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFunction.Location = new System.Drawing.Point(852, 1);
            this.lblFunction.Name = "lblFunction";
            this.lblFunction.Size = new System.Drawing.Size(176, 607);
            this.lblFunction.TabIndex = 33;
            // 
            // m_PatInfo
            // 
            this.m_PatInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_PatInfo.Charge = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_PatInfo.CurrentDeptID = "";
            this.m_PatInfo.CurrentDeptName = "";
            this.m_PatInfo.CurrentDoctorID = "";
            this.m_PatInfo.CurrentDoctorName = "";
            this.m_PatInfo.CurrentDoctorNo = "";
            this.m_PatInfo.CurrentDoctTechnicalRank = "";
            this.m_PatInfo.DeptID = "";
            this.m_PatInfo.DeptName = "";
            this.m_PatInfo.Discount = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_PatInfo.DoctorID = "";
            this.m_PatInfo.DoctorName = "";
            this.m_PatInfo.DoctorNo = "";
            this.m_PatInfo.DoctTechnicalRank = "";
            this.m_PatInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_PatInfo.HotKey = "F3";
            this.m_PatInfo.Hypersusceptibility = "";
            this.m_PatInfo.IDcard = "";
            this.m_PatInfo.Limit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_PatInfo.Location = new System.Drawing.Point(0, 0);
            this.m_PatInfo.m_strIsVip = "";
            this.m_PatInfo.Name = "m_PatInfo";
            this.m_PatInfo.PatientAge = "";
            this.m_PatInfo.PatientBirth = "";
            this.m_PatInfo.PatientCardID = "";
            this.m_PatInfo.PatientHomeAddress = "";
            this.m_PatInfo.PatientID = "";
            this.m_PatInfo.PatientName = "";
            this.m_PatInfo.PatientSex = "";
            this.m_PatInfo.PatientTelephoneNo = "";
            this.m_PatInfo.PatientType = 0;
            this.m_PatInfo.PayTypeID = "";
            this.m_PatInfo.PayTypeName = "";
            this.m_PatInfo.RegisterID = "";
            this.m_PatInfo.RegisterNo = "";
            this.m_PatInfo.RegTypeID = "";
            this.m_PatInfo.Size = new System.Drawing.Size(848, 96);
            this.m_PatInfo.TabIndex = 24;
            this.m_PatInfo.TakeDiagRecID = "";
            this.m_PatInfo.PatientChanged += new com.digitalwave.controls.TextChangeEvent(this.m_PatInfo_PatientChanged);
            this.m_PatInfo.SelectDoctor += new System.EventHandler(this.m_PatInfo_SelectDoctor);
            this.m_PatInfo.PatientTypeChanged += new com.digitalwave.controls.HandlePatientTypeChange(this.m_PatInfo_PatientTypeChanged);
            this.m_PatInfo.PatientEnd += new System.EventHandler(this.m_PatInfo_PatientEnd);
            // 
            // frmDoctorWorkstation
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(1028, 611);
            this.Controls.Add(this.lblIsVip);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.lbeTimes);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.alertLight1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.m_PatInfo);
            this.Controls.Add(this.lblFunction);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmDoctorWorkstation";
            this.Text = "医生工作站";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmDoctorWorkstation_Closing);
            this.Load += new System.EventHandler(this.frmDoctorWorkstation_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDoctorWorkstation_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picops)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picris)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.piclis)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgWaitReg)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgTake)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid2)).EndInit();
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGridLis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid3)).EndInit();
            this.plLis.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPage8.ResumeLayout(false);
            this.plTest.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGridTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid4)).EndInit();
            this.tabPage9.ResumeLayout(false);
            this.plOps.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGridOps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid5)).EndInit();
            this.tabPage10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctlDataGrid6)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoZzsq.Properties)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.clsCtl_DoctorWorkstation();
            objController.Set_GUI_Apperance(this);
        }

        #region 公共属性
        //
        public string PatientTypeName
        {
            get
            {
                return this.m_PatInfo.txtType.Text;
            }
        }
        public string PatientTypeID
        {
            get
            {
                return this.m_PatInfo.PayTypeID;
            }
        }
        public string DoctorName
        {
            get
            {
                return this.m_PatInfo.txtRegisterDoctor.Text;
            }
        }
        public string DoctorID
        {
            get
            {
                return this.m_PatInfo.DoctorID;
            }
        }
        public string DepName
        {
            get
            {
                return this.m_PatInfo.txtRegisterDept.Text;
            }
        }
        public string DepID
        {
            get
            {
                return this.m_PatInfo.DeptID;
            }
        }
        /////////////
        public string ExamineResult
        {
            get
            {
                return this.objCaseHistory.ExamineResult;
            }
        }
        public string AidCheck
        {
            get
            {
                return this.objCaseHistory.AidCheck;
            }
        }
        public string Anaphylaxis
        {
            get
            {
                return this.objCaseHistory.Anaphylaxis;
            }
        }
        public string Treatment
        {
            get
            {
                return this.objCaseHistory.Treatment;
            }
        }
        public string ReMark
        {
            get
            {
                return this.objCaseHistory.ReMark;
            }
        }
        /// ///////////////////////////
        public string PatientName
        {
            get
            {
                return this.m_PatInfo.PatientName;
            }
        }
        public string PatientID
        {
            get
            {
                return this.m_PatInfo.PatientID;
            }
        }
        public string PatientSex
        {
            get
            {
                return this.m_PatInfo.PatientSex;
            }
        }
        public string PatientAge
        {
            get
            {
                return this.m_PatInfo.PatientAge;
            }
        }
        public string PatientCardID
        {
            get
            {
                return this.m_PatInfo.PatientCardID;
            }
        }
        public string DiagMain
        {
            get
            {
                return this.objCaseHistory.DiagMain;
            }
        }
        public string DiagCurr
        {
            get
            {
                return this.objCaseHistory.DiagCurr;
            }
        }
        public string DiagHis
        {
            get
            {
                return this.objCaseHistory.DiagHis;
            }
        }
        public string Diag
        {
            get
            {
                return this.objCaseHistory.Diag;
            }
        }
        public string ChargeItemName
        {
            get
            {
                string temp = "";
                int int_Count = 1;
                for (int i = 0; i < this.ctlDataGrid1.RowCount; i++)
                {
                    if (objUsageArr.IndexOf(ctlDataGrid1[i, 16].ToString().Trim()) > -1)
                    {
                        temp += int_Count.ToString() + "、" + ctlDataGrid1[i, 4].ToString().Trim() + "\n";
                        int_Count++;
                    }
                }
                return temp;
            }
        }

        public string ChargeItemCount
        {
            get
            {
                string temp = "";
                int int_Count = 1;
                for (int i = 0; i < this.ctlDataGrid1.RowCount; i++)
                {
                    if (objUsageArr.IndexOf(ctlDataGrid1[i, 16].ToString().Trim()) > -1)
                    {
                        temp += int_Count.ToString() + "、" + ctlDataGrid1[i, 2].ToString().Trim() + "\n";
                        int_Count++;
                    }
                }
                return temp;
            }
        }
        public string ChargeItemUsage
        {
            get
            {
                string temp = "";
                int int_Count = 1;
                for (int i = 0; i < this.ctlDataGrid1.RowCount; i++)
                {
                    if (objUsageArr.IndexOf(ctlDataGrid1[i, 16].ToString().Trim()) > -1)
                    {
                        temp += int_Count.ToString() + "、" + ctlDataGrid1[i, 6].ToString().Trim() + "\n";
                        int_Count++;
                    }
                }
                return temp;
            }
        }

        public int URGENCY_INT
        {
            get
            {
                return intURGENCY_INT;
            }
        }

        /// <summary>
        /// 记录注射用法的ID
        /// </summary>
        public ArrayList objUsageArr;

        /// <summary>
        /// 当前操作员无处方权限时直接退出该窗体
        /// </summary>
        private bool noopen = false;
        #endregion

        #region 控制DataGrid列
        private void m_mthHandleDataGridInput()
        {
            foreach (System.Windows.Forms.Control cc in this.tabControl1.Controls)
            {
                foreach (System.Windows.Forms.Control c in cc.Controls)
                {
                    com.digitalwave.controls.datagrid.ctlDataGrid dategrid = c as com.digitalwave.controls.datagrid.ctlDataGrid;
                    if (dategrid != null)
                    {
                        for (int i = 0; i < dategrid.Columns.Count; i++)
                        {
                            if (((com.digitalwave.controls.datagrid.clsColumnInfo)dategrid.Columns[i]).DataGridTextBoxColumn.TextBox.Enabled == false || ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[i]).DataGridTextBoxColumn.TextBox.MaxLength < 10)
                            {
                                continue;
                            }
                            ((com.digitalwave.controls.datagrid.clsColumnInfo)dategrid.Columns[i]).DataGridTextBoxColumn.TextBox.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);
                        }
                    }
                }
            }

        }
        private void m_mthSetDataGridFormat()
        {
            m_mthHandleDataGridInput();
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[2]).DataGridTextBoxColumn.TextBox.KeyPress += new KeyPressEventHandler(txtCount_KeyPress);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[13]).DataGridTextBoxColumn.TextBox.KeyPress += new KeyPressEventHandler(txtCount_KeyPress);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[8]).DataGridTextBoxColumn.TextBox.KeyPress += new KeyPressEventHandler(txtCount_KeyPress2);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[0]).DataGridTextBoxColumn.TextBox.KeyPress += new KeyPressEventHandler(txtCount_KeyPress2);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid2.Columns[1]).DataGridTextBoxColumn.TextBox.KeyPress += new KeyPressEventHandler(txtCount_KeyPress);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid3.Columns[1]).DataGridTextBoxColumn.TextBox.KeyPress += new KeyPressEventHandler(txtCount_KeyPress);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridLis.Columns[1]).DataGridTextBoxColumn.TextBox.KeyPress += new KeyPressEventHandler(txtCount_KeyPress);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid4.Columns[1]).DataGridTextBoxColumn.TextBox.KeyPress += new KeyPressEventHandler(txtCount_KeyPress);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridTest.Columns[1]).DataGridTextBoxColumn.TextBox.KeyPress += new KeyPressEventHandler(txtCount_KeyPress);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid5.Columns[1]).DataGridTextBoxColumn.TextBox.KeyPress += new KeyPressEventHandler(txtCount_KeyPress);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridOps.Columns[1]).DataGridTextBoxColumn.TextBox.KeyPress += new KeyPressEventHandler(TextBox_KeyPress3);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid6.Columns[1]).DataGridTextBoxColumn.TextBox.KeyPress += new KeyPressEventHandler(txtCount_KeyPress);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[1]).DataGridTextBoxColumn.TextBox.Enter += new EventHandler(TextBox_Enter);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[0]).DataGridTextBoxColumn.TextBox.Enter += new EventHandler(RecipeTextBox_Enter);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid2.Columns[0]).DataGridTextBoxColumn.TextBox.Enter += new EventHandler(TextBox_Enter);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid3.Columns[0]).DataGridTextBoxColumn.TextBox.Enter += new EventHandler(TextBox_Enter);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridLis.Columns[0]).DataGridTextBoxColumn.TextBox.Enter += new EventHandler(TextBox_Enter);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid4.Columns[0]).DataGridTextBoxColumn.TextBox.Enter += new EventHandler(TextBox_Enter);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridTest.Columns[0]).DataGridTextBoxColumn.TextBox.Enter += new EventHandler(TextBox_Enter);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid5.Columns[0]).DataGridTextBoxColumn.TextBox.Enter += new EventHandler(TextBox_Enter);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridOps.Columns[0]).DataGridTextBoxColumn.TextBox.Enter += new EventHandler(TextBox_Enter);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid6.Columns[0]).DataGridTextBoxColumn.TextBox.Enter += new EventHandler(TextBox_Enter);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[0]).DataGridTextBoxColumn.TextBox.Leave += new EventHandler(RecipeTextBox_Leave);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[1]).DataGridTextBoxColumn.TextBox.Leave += new EventHandler(TextBox_Leave);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid2.Columns[0]).DataGridTextBoxColumn.TextBox.Leave += new EventHandler(TextBox_Leave);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid3.Columns[0]).DataGridTextBoxColumn.TextBox.Leave += new EventHandler(TextBox_Leave);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridLis.Columns[0]).DataGridTextBoxColumn.TextBox.Leave += new EventHandler(TextBox_Leave);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid4.Columns[0]).DataGridTextBoxColumn.TextBox.Leave += new EventHandler(TextBox_Leave);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridTest.Columns[0]).DataGridTextBoxColumn.TextBox.Leave += new EventHandler(TextBox_Leave);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid5.Columns[0]).DataGridTextBoxColumn.TextBox.Leave += new EventHandler(TextBox_Leave);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridOps.Columns[0]).DataGridTextBoxColumn.TextBox.Leave += new EventHandler(TextBox_Leave);
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid6.Columns[0]).DataGridTextBoxColumn.TextBox.Leave += new EventHandler(TextBox_Leave);

            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[2]).DataGridTextBoxColumn.TextBox.MaxLength = 7;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[0]).DataGridTextBoxColumn.TextBox.MaxLength = 2;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid2.Columns[1]).DataGridTextBoxColumn.TextBox.MaxLength = 4;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid3.Columns[1]).DataGridTextBoxColumn.TextBox.MaxLength = 4;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid4.Columns[1]).DataGridTextBoxColumn.TextBox.MaxLength = 4;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid5.Columns[1]).DataGridTextBoxColumn.TextBox.MaxLength = 4;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid6.Columns[1]).DataGridTextBoxColumn.TextBox.MaxLength = 4;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[2]).DataGridTextBoxColumn.TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[8]).DataGridTextBoxColumn.TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid2.Columns[1]).DataGridTextBoxColumn.TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid3.Columns[1]).DataGridTextBoxColumn.TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridLis.Columns[1]).DataGridTextBoxColumn.TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid4.Columns[1]).DataGridTextBoxColumn.TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridTest.Columns[1]).DataGridTextBoxColumn.TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid5.Columns[1]).DataGridTextBoxColumn.TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridOps.Columns[1]).DataGridTextBoxColumn.TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid6.Columns[1]).DataGridTextBoxColumn.TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;

            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[1]).DataGridTextBoxColumn.TextBox.CharacterCasing = CharacterCasing.Upper;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid2.Columns[0]).DataGridTextBoxColumn.TextBox.CharacterCasing = CharacterCasing.Upper;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid3.Columns[0]).DataGridTextBoxColumn.TextBox.CharacterCasing = CharacterCasing.Upper;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridLis.Columns[0]).DataGridTextBoxColumn.TextBox.CharacterCasing = CharacterCasing.Upper;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid4.Columns[0]).DataGridTextBoxColumn.TextBox.CharacterCasing = CharacterCasing.Upper;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridTest.Columns[0]).DataGridTextBoxColumn.TextBox.CharacterCasing = CharacterCasing.Upper;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid5.Columns[0]).DataGridTextBoxColumn.TextBox.CharacterCasing = CharacterCasing.Upper;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridOps.Columns[0]).DataGridTextBoxColumn.TextBox.CharacterCasing = CharacterCasing.Upper;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid6.Columns[0]).DataGridTextBoxColumn.TextBox.CharacterCasing = CharacterCasing.Upper;

            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[1]).DataGridTextBoxColumn.TextBox.MaxLength = 20;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid2.Columns[0]).DataGridTextBoxColumn.TextBox.MaxLength = 20;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid3.Columns[0]).DataGridTextBoxColumn.TextBox.MaxLength = 20;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridLis.Columns[0]).DataGridTextBoxColumn.TextBox.MaxLength = 20;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid4.Columns[0]).DataGridTextBoxColumn.TextBox.MaxLength = 20;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridTest.Columns[0]).DataGridTextBoxColumn.TextBox.MaxLength = 20;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid5.Columns[0]).DataGridTextBoxColumn.TextBox.MaxLength = 20;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridOps.Columns[0]).DataGridTextBoxColumn.TextBox.MaxLength = 20;
            ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid6.Columns[0]).DataGridTextBoxColumn.TextBox.MaxLength = 20;

        }
        #endregion

        private void frmDoctorWorkstation_Load(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ((clsCtl_DoctorWorkstation)this.objController).m_mthIniComponent();
            if (((clsCtl_DoctorWorkstation)this.objController).Recpur == "0")
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("对不起，您暂时无处方权限。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                noopen = true;
                this.Hide();
                this.timerRecpur.Enabled = true;
                this.timerRecpur.Interval = 2000;
                return;
            }
            this.m_mthSetDataGridFormat();
            m_mthLoadApplyBill();
            //读取是否合并门、急诊药房
            IsDetachWMedStore = ((clsCtl_DoctorWorkstation)this.objController).m_strReadXML("register", "IsDetachWMedStore", "AnyOne") != "" ? int.Parse(((clsCtl_DoctorWorkstation)this.objController).m_strReadXML("register", "IsDetachWMedStore", "AnyOne")) : 0;
            ((clsCtl_DoctorWorkstation)this.objController).m_mthGetinjectInfo(out objUsageArr);
            if (this.LoginInfo.m_strDepartmentID != null && this.LoginInfo.m_strDepartmentID.Trim() != "")
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_GetWaitReg(this.LoginInfo.m_strEmpID, this.LoginInfo.m_strDepartmentID, this.LoginInfo.m_strdepartmentName);
            }
            //药品天数参数
            intMedDays = ((clsCtl_DoctorWorkstation)this.objController).m_intGetMedDays();
            //当天日期
            lblToday.Text = DateTime.Now.Date.ToString("yyyy年MM月dd日");
            //过敏提示窗口坐标
            frmAllergich.DesktopLocation = new Point(630, 620);
            //设置记时器(过敏)		
            int ti = ((clsCtl_DoctorWorkstation)this.objController).Timerinterval;
            if (ti != 0)
            {
                this.timer.Enabled = true;
                this.timer.Interval = ti * 60 * 1000;
            }
            //护士工作站：过敏人员列表窗口
            frmAllergicl.StartPosition = FormStartPosition.Manual;
            frmAllergicl.Location = new Point(10, 208);

            //设置记时器(审核未通过处方)		
            int tirec = ((clsCtl_DoctorWorkstation)this.objController).Timerinterval_rec;
            if (tirec != 0)
            {
                this.timerrec.Enabled = true;
                this.timerrec.Interval = tirec * 60 * 1000;
            }

            //处方审核未通过列表窗口
            frmRecconf.StartPosition = FormStartPosition.Manual;
            frmRecconf.Location = new Point(10, 208);

            //绑定速诊CBO控件
            //TextBox tb = ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid3.Columns[((clsCtl_DoctorWorkstation)this.objController).t_quick]).DataGridTextBoxColumn.TextBox;
            //if (tb != null)
            //{
            //    this.ctlDataGrid3.Controls.Add(this.cboquick);
            //    tb.Enter += new EventHandler(TB_Enter);
            //}
            TextBox tb = ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridLis.Columns[((clsCtl_DoctorWorkstation)this.objController).t_quick]).DataGridTextBoxColumn.TextBox;
            if (tb != null)
            {
                this.ctlDataGridLis.Controls.Add(this.cboquick);
                tb.Enter += new EventHandler(TB_Enter);
            }

            //控制修改医生姓名
            this.m_PatInfo.txtRegisterDoctor.Enabled = !(((clsCtl_DoctorWorkstation)this.objController).isCanChangeDoctor);

            //西药栏-》适应症
            tb = ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[((clsCtl_DoctorWorkstation)this.objController).c_Deptmed]).DataGridTextBoxColumn.TextBox;
            if (tb != null)
            {
                this.ctlDataGrid1.Controls.Add(this.cboDeptmed1);
                tb.Enter += new EventHandler(cboDeptmed1_Enter);
            }

            #region 明细处方栏绑定科备药标志
            if (((clsCtl_DoctorWorkstation)this.objController).Isdeptmed)
            {
                ////西药
                //tb = ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[((clsCtl_DoctorWorkstation)this.objController).c_Deptmed]).DataGridTextBoxColumn.TextBox;
                //if (tb != null)
                //{
                //    this.ctlDataGrid1.Controls.Add(this.cboDeptmed1);
                //    tb.Enter += new EventHandler(cboDeptmed1_Enter);
                //}
                //中药
                tb = ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid2.Columns[((clsCtl_DoctorWorkstation)this.objController).cm_Deptmed]).DataGridTextBoxColumn.TextBox;
                if (tb != null)
                {
                    this.ctlDataGrid2.Controls.Add(this.cboDeptmed2);
                    tb.Enter += new EventHandler(cboDeptmed2_Enter);
                }
                //其他
                tb = ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid6.Columns[((clsCtl_DoctorWorkstation)this.objController).o_Deptmed]).DataGridTextBoxColumn.TextBox;
                if (tb != null)
                {
                    this.ctlDataGrid6.Controls.Add(this.cboDeptmed6);
                    tb.Enter += new EventHandler(cboDeptmed6_Enter);
                }
            }
            #endregion

            if (((clsCtl_DoctorWorkstation)this.objController).m_strReadXML("register", "UseCall", "AnyOne") == "1")
            {
                frmDiag = new DiagnoseClient.frmDiagClientMain();
                frmDiag.ShowTopLevel();
                DiagnoseClient.frmDiagClientMain.PatientCalled += new DiagnoseClient.PatientCalledEventHandler(frmDiagClientMain_PatientCalled);
            }

            m_PatInfo.m_strUserCall = ((clsCtl_DoctorWorkstation)this.objController).m_strReadXML("register", "EnableCallMSMQ", "AnyOne");
            this.btnPrefer.Enabled = false;
            this.Cursor = Cursors.Default;
        }

        private delegate void v1(string id);
        private void frmDiagClientMain_PatientCalled(object sender, DiagnoseClient.PatientCalledArgs e)
        {
            this.m_PatInfo.Invoke(new v1(this.m_PatInfo.m_mthGetPatientInfoByCard), e.CardNo);
        }

        private void m_cmbFind_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (m_cmbFind.SelectedIndex)
            {
                case 0://项目编码
                    m_cmbFind.Tag = "ITEMCODE_VCHR";
                    cmbFindAccordRecipe.Tag = "USERCODE_CHR";
                    this.groupBox3.Tag = "";
                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                    break;
                case 1://项目名称
                    m_cmbFind.Tag = "ITEMNAME_VCHR";
                    cmbFindAccordRecipe.Tag = "RECIPENAME_CHR";
                    this.groupBox3.Tag = "%";
                    break;
                case 2://项目拼音
                    m_cmbFind.Tag = "ITEMPYCODE_CHR";
                    cmbFindAccordRecipe.Tag = "PYCODE_CHR";
                    this.groupBox3.Tag = "";
                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                    break;
                case 3://项目五笔
                    m_cmbFind.Tag = "ITEMWBCODE_CHR";
                    cmbFindAccordRecipe.Tag = "WBCODE_CHR";
                    this.groupBox3.Tag = "";
                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                    break;
                case 4:
                    m_cmbFind.Tag = "ITEMENGNAME_VCHR";
                    this.groupBox3.Tag = "";
                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                    break;
            }

        }

        private void m_cmbRecipeType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (m_cmbRecipeType.SelectedIndex == 0)
            {
                m_cmbRecipeType.Tag = 1;//正方
            }
            else
            {
                m_cmbRecipeType.Tag = 2;//副方
            }
        }



        private void listView1_Leave(object sender, System.EventArgs e)
        {
            listView1.Height = 0;
            listView1.Visible = false;
        }

        private void listView1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthListViewKeyDown(e);
        }

        private void listView1_DoubleClick(object sender, System.EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthListViewDoubleClick();
        }
        #region DataGrid回车事件
        private void ctlDataGrid1_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
        {
            //			((clsCtl_DoctorWorkstation)this.objController).m_mthShowPriceInfo("",1,e);
            string m_strText = e.m_strText.Replace("'", "’");
            this.intRowNo1 = e.m_intRowNumber;

            if (e.KeyCode == Keys.Enter)
            {
                switch (e.m_intColNumber)
                {
                    case 0://方号
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthSetRowNo(m_strText, e.m_intRowNumber, this.ctlDataGrid1[e.m_intRowNumber, 0].ToString().Trim(), 0);
                        break;
                    case 1://查询
                        if (m_strText.Trim() == "")
                        {
                            return;
                        }
                        else
                        {
                            if (this.m_cmbFind.SelectedIndex == 2)
                            {
                                m_strText = m_strText.ToUpper();
                            }
                            if (m_strText.IndexOf("\\") == 0)
                            {
                                if (((clsCtl_DoctorWorkstation)this.objController).ItemInputMode == 0)
                                {
                                    ((clsCtl_DoctorWorkstation)this.objController).m_mthFindAccordRecipe(m_strText.Substring(1, m_strText.Length - 1));
                                }
                                else if (((clsCtl_DoctorWorkstation)this.objController).ItemInputMode == 1)
                                {
                                    ((clsCtl_DoctorWorkstation)this.objController).m_mthGetAccordRecipe(m_strText.Substring(1, m_strText.Length - 1));
                                }
                            }
                            else if (m_strText.Replace("？", "?").IndexOf("?") == 0)
                            {
                                ((clsCtl_DoctorWorkstation)this.objController).m_mthGetMedicineByCodex(e.m_intRowNumber);
                            }
                            else
                            {
                                ((clsCtl_DoctorWorkstation)this.objController).m_mthFindWMedicineByID(this.groupBox3.Tag.ToString() + m_strText, e.m_intRowNumber);
                            }
                        }
                        this.cboDeptmed1.SelectedIndex = 1;
                        break;
                    case 2://数量
                        if (m_strText.Trim() == "")
                        {
                            return;
                        }
                        else
                        {
                            //							((clsCtl_DoctorWorkstation)this.objController).IsCalculateAmount=true;
                            ctlDataGrid1[e.m_intRowNumber, 2] = m_strText;

                            //							((clsCtl_DoctorWorkstation)this.objController).m_mthCheckMainItemNum(this.ctlDataGrid1[e.m_intRowNumber,c_resubitem].ToString(), this.ctlDataGrid1[e.m_intRowNumber,c_MainItemNum].ToString(), m_strText);

                            if (!((clsCtl_DoctorWorkstation)this.objController).m_mthIsOverflow(this.ctlDataGrid1.CurrentCell.RowNumber, m_strText))
                            {
                                return;
                            }

                            ctlDataGrid1[e.m_intRowNumber, 26] = 1;
                            ctlDataGrid1.CurrentCell = new DataGridCell(e.m_intRowNumber, 6);
                        }
                        break;
                    case 6://用法
                        if (((clsCtl_DoctorWorkstation)this.objController).m_mthFindUsage(m_strText, e.m_intRowNumber) > 0)
                        {
                            this.listView2.Location = e.m_ptPositionInDataGrid;
                            this.listView2.Top += e.m_szTextBoxSize.Height;
                            this.listView2.Show();
                            this.listView2.BringToFront();
                            this.listView2.Items[0].Selected = true;
                            this.listView2.Select();
                            this.listView2.Focus();
                        }
                        break;
                    case 7://频率
                        if (((clsCtl_DoctorWorkstation)this.objController).m_mthFindFrequency(m_strText, e.m_intRowNumber) > 0)
                        {
                            this.listView3.Location = e.m_ptPositionInDataGrid;
                            this.listView3.Top += e.m_szTextBoxSize.Height;
                            this.listView3.Show();
                            this.listView3.BringToFront();
                            this.listView3.Items[0].Selected = true;
                            this.listView3.Select();
                            this.listView3.Focus();
                        }
                        break;
                    case 8://天数
                        if (m_strText.Trim() != "")
                        {
                            if (intMedDays > 0 && intMedDays < int.Parse(m_strText))
                            {
                                MessageBox.Show(m_strText + "天超过了医院规定的最大天数，最大天数是" + intMedDays.ToString() + "天，请修改!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                            ctlDataGrid1[e.m_intRowNumber, 26] = 1;
                            ctlDataGrid1[e.m_intRowNumber, 8] = m_strText;
                            ((clsCtl_DoctorWorkstation)this.objController).m_mthDaysEnter(m_strText);
                        }
                        break;
                    case 13://天数
                        if (m_strText.Trim() != "")
                        {
                            //								this.ctlDataGrid1.CurrentCell=new DataGridCell(e.m_intRowNumber+1,0);
                            SendKeys.SendWait("{Tab}");
                        }
                        break;

                }
            }
            else
            {
                if (e.m_intColNumber == 13 && m_strText.Trim() != "")
                {
                    ctlDataGrid1[e.m_intRowNumber, 13] = m_strText;
                    ctlDataGrid1[e.m_intRowNumber, 26] = 0;
                    ((clsCtl_DoctorWorkstation)this.objController).b_IndexChangeFlag = false;
                }

            }

            //			((clsCtl_DoctorWorkstation)this.objController).m_mthFormKeyDown(e);
        }
        private void ctlDataGrid2_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
        {
            //			((clsCtl_DoctorWorkstation)this.objController).m_mthShowPriceInfo("",2,e);
            //			((clsCtl_DoctorWorkstation)this.objController).m_mthFormKeyDown(e);
            string m_strText = e.m_strText.Replace("'", "’");
            intRowNo2 = e.m_intRowNumber;
            if (e.KeyCode == Keys.Enter)//查询
            {

                if (e.m_intColNumber == 0)
                {
                    if (m_strText.Trim() == "")
                    {
                        return;
                    }
                    //					intRowNo2=e.m_intRowNumber;
                    //					string m_strText =e.m_strText;
                    if (this.m_cmbFind.SelectedIndex == 2)
                    {
                        m_strText = m_strText.ToUpper();
                    }
                    if (m_strText.IndexOf("\\") == 0)
                    {
                        if (((clsCtl_DoctorWorkstation)this.objController).ItemInputMode == 0)
                        {
                            ((clsCtl_DoctorWorkstation)this.objController).m_mthFindAccordRecipe(m_strText.Substring(1, m_strText.Length - 1));
                        }
                        else if (((clsCtl_DoctorWorkstation)this.objController).ItemInputMode == 1)
                        {
                            ((clsCtl_DoctorWorkstation)this.objController).m_mthGetAccordRecipe(m_strText.Substring(1, m_strText.Length - 1));
                        }
                    }
                    else if (m_strText.Replace("？", "?").IndexOf("?") == 0)
                    {
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthGetMedicineByCodex(e.m_intRowNumber);
                    }
                    else
                    {
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthFindCMedicineByID(this.groupBox3.Tag.ToString() + m_strText, e.m_intRowNumber);
                    }
                    this.cboDeptmed2.SelectedIndex = 1;
                }

                if (e.m_intColNumber == 1)//输入数量
                {
                    if (m_strText.Trim() == "")
                    {
                        return;
                    }
                    if (!((clsCtl_DoctorWorkstation)this.objController).m_mthIsOverflow2(this.ctlDataGrid2.CurrentCell.RowNumber, m_strText))
                    {
                        return;
                    }
                    this.ctlDataGrid2[e.m_intRowNumber, 1] = m_strText;
                    intRowNo2 = e.m_intRowNumber;
                    if (e.KeyCode == Keys.Enter)
                    {
                        //					ctlDataGrid2.CurrentCell=new DataGridCell(e.m_intRowNumber+1,0);
                        ctlDataGrid2.CurrentCell = new DataGridCell(e.m_intRowNumber, 5);
                    }
                }
                if (e.m_intColNumber == 5)//选择用法
                {
                    if (((clsCtl_DoctorWorkstation)this.objController).m_mthFindUsage2(m_strText, e.m_intRowNumber) > 0)
                    {
                        this.listView4.Location = e.m_ptPositionInDataGrid;
                        this.listView4.Top += e.m_szTextBoxSize.Height;
                        this.listView4.Show();
                        this.listView4.BringToFront();
                        this.listView4.Items[0].Selected = true;
                        this.listView4.Select();
                        this.listView4.Focus();
                    }
                }
            }

        }
        private void ctlDataGrid3_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
        {
            string m_strText = e.m_strText.Replace("'", "’");
            if (e.KeyCode == Keys.Enter)//查询
            {

                if (e.m_intColNumber == 0)
                {
                    if (m_strText.Trim() == "")
                    {
                        return;
                    }
                    if (m_strText.IndexOf("\\") == 0)
                    {
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthFindAccordRecipe(m_strText.Substring(1, m_strText.Length - 1));
                    }
                    else
                    {
                        intRowNo3 = e.m_intRowNumber;
                        //						string m_strText =m_strText;
                        if (this.m_cmbFind.SelectedIndex == 2)
                        {
                            m_strText = m_strText.ToUpper();
                        }
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthFindTestChargeByID(this.groupBox3.Tag.ToString() + m_strText, e.m_intRowNumber);
                    }
                }
            }
            if (e.m_intColNumber == 1)//输入数量
            {
                intRowNo3 = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthCheckMainItemNum(this.ctlDataGrid3[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_resubitem].ToString(), this.ctlDataGrid3[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_MainItemNum].ToString(), m_strText, null);
                    SendKeys.SendWait("{Tab}");
                }
            }
            if (e.m_intColNumber == 4)//查询检验类型
            {
                intRowNo3 = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    if (((clsCtl_DoctorWorkstation)this.objController).m_lngGetLisSampletyType(m_strText, e.m_intRowNumber) > 0)
                    {
                        this.listView5.Location = e.m_ptPositionInDataGrid;
                        this.listView5.Top += e.m_szTextBoxSize.Height;
                        this.listView5.Show();
                        this.listView5.BringToFront();
                        this.listView5.Items[0].Selected = true;
                        this.listView5.Select();
                        this.listView5.Focus();
                    }
                }
            }
            if (e.m_intColNumber == 6)//输入单价
            {
                intRowNo3 = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.SendWait("{Tab}");
                }
            }


            //			((clsCtl_DoctorWorkstation)this.objController).m_mthFormKeyDown(e);
        }
        private void ctlDataGrid4_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
        {
            string m_strText = e.m_strText.Replace("'", "’");
            if (e.KeyCode == Keys.Enter)//查询
            {

                if (e.m_intColNumber == 0)
                {
                    if (m_strText.Trim() == "")
                    {
                        return;
                    }
                    if (m_strText.IndexOf("\\") == 0)
                    {
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthFindAccordRecipe(m_strText.Substring(1, m_strText.Length - 1));
                    }
                    else
                    {
                        intRowNo4 = e.m_intRowNumber;
                        //						string m_strText =m_strText;
                        if (this.m_cmbFind.SelectedIndex == 2)
                        {
                            m_strText = m_strText.ToUpper();
                        }
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthFindExamineChargeByID(this.groupBox3.Tag.ToString() + m_strText, e.m_intRowNumber);
                    }
                }
            }
            if (e.m_intColNumber == 1)//输入数量
            {
                //				this.ctlDataGrid4[e.m_intRowNumber,1]=m_strText;
                intRowNo4 = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthCheckMainItemNum(this.ctlDataGrid4[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_resubitem].ToString(), this.ctlDataGrid4[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_MainItemNum].ToString(), m_strText, null);
                    SendKeys.SendWait("{Tab}");
                }
            }
            if (e.m_intColNumber == 4)//输入检查部位
            {
                intRowNo4 = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    if (((clsCtl_DoctorWorkstation)this.objController).m_mthLoadCheckPart(m_strText, e.m_intRowNumber) > 0)
                    {
                        this.listView5.Location = e.m_ptPositionInDataGrid;
                        this.listView5.Top += e.m_szTextBoxSize.Height;
                        this.listView5.Show();
                        this.listView5.BringToFront();
                        this.listView5.Items[0].Selected = true;
                        this.listView5.Select();
                        this.listView5.Focus();
                    }
                }
            }
            if (e.m_intColNumber == 5)//输入单价
            {
                intRowNo4 = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.SendWait("{Tab}");
                }
            }
            //	
            //			((clsCtl_DoctorWorkstation)this.objController).m_mthFormKeyDown(e);
        }
        private void ctlDataGrid5_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
        {
            string m_strText = e.m_strText.Replace("'", "’");
            if (e.KeyCode == Keys.Enter)//查询
            {
                if (e.m_intColNumber == 0)
                {
                    if (m_strText.Trim() == "")
                    {
                        return;
                    }
                    if (m_strText.IndexOf("\\") == 0)
                    {
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthFindAccordRecipe(m_strText.Substring(1, m_strText.Length - 1));
                    }
                    else
                    {
                        intRowNo5 = e.m_intRowNumber;
                        //						string m_strText =m_strText;
                        if (this.m_cmbFind.SelectedIndex == 2)
                        {
                            m_strText = m_strText.ToUpper();
                        }
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthFindOPSChargeByID(this.groupBox3.Tag.ToString() + m_strText, e.m_intRowNumber);
                    }
                }
            }
            if (e.m_intColNumber == 1)//输入数量
            {
                intRowNo5 = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthCheckMainItemNum(this.ctlDataGrid5[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_resubitem].ToString(), this.ctlDataGrid5[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_MainItemNum].ToString(), m_strText, null);
                    SendKeys.SendWait("{Tab}");
                }
            }
            if (e.m_intColNumber == 5)//输入单价
            {
                intRowNo5 = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.SendWait("{Tab}");
                }
            }


        }
        private void ctlDataGrid6_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
        {
            string m_strText = e.m_strText.Replace("'", "’");
            if (e.KeyCode == Keys.Enter)//查询
            {
                if (e.m_intColNumber == 0)
                {

                    if (m_strText.Trim() == "")
                    {
                        return;
                    }
                    if (m_strText.IndexOf("\\") == 0)
                    {
                        if (((clsCtl_DoctorWorkstation)this.objController).ItemInputMode == 0)
                        {
                            ((clsCtl_DoctorWorkstation)this.objController).m_mthFindAccordRecipe(m_strText.Substring(1, m_strText.Length - 1));
                        }
                        else if (((clsCtl_DoctorWorkstation)this.objController).ItemInputMode == 1)
                        {
                            ((clsCtl_DoctorWorkstation)this.objController).m_mthGetAccordRecipe(m_strText.Substring(1, m_strText.Length - 1));
                        }
                    }
                    else
                    {
                        intRowNo6 = e.m_intRowNumber;
                        //						string m_strText =m_strText;
                        if (this.m_cmbFind.SelectedIndex == 2)
                        {
                            m_strText = m_strText.ToUpper();
                        }
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthFindOtherChargeByID(this.groupBox3.Tag.ToString() + m_strText, e.m_intRowNumber);
                    }
                    this.cboDeptmed6.SelectedIndex = 1;
                }
            }
            if (e.m_intColNumber == 1)//输入数量
            {
                intRowNo6 = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthCheckMainItemNum(this.ctlDataGrid6[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_resubitem].ToString(), this.ctlDataGrid6[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_MainItemNum].ToString(), m_strText, null);
                    SendKeys.SendWait("{Tab}");
                }
            }
            if (e.m_intColNumber == 5)//输入单价
            {
                intRowNo6 = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.SendWait("{Tab}");
                }
            }
        }
        #endregion
        #region CellChange事件
        private void ctlDataGrid1_m_evtCurrentCellChanged(object sender, System.EventArgs e)
        {
            try
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateAmount();
                ((clsCtl_DoctorWorkstation)this.objController).m_mthSetColNo1();
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();
                intRowNo1 = -1;
                int row = ctlDataGrid1.CurrentCell.RowNumber;
                if (this.ctlDataGrid1[row, 0].ToString().Trim() == "" || this.ctlDataGrid1[row, 0].ToString().Trim() == "0")
                {
                    this.ctlDataGrid1[row, 29] = -4;
                }
                int col = ctlDataGrid1.CurrentCell.ColumnNumber;
                if (col != 1)
                {
                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                }
                if ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[col] != null)
                {
                    //设置前背景色
                    ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid1.Columns[col]).DataGridTextBoxColumn.TextBox.BackColor = efc; //Color.SteelBlue;						
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
        }

        private void ctlDataGrid2_m_evtCurrentCellChanged(object sender, System.EventArgs e)
        {
            try
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthSetColNo2();

                if (this.intRowNo2 > -1)
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateAmount2(this.intRowNo2);
                    this.intRowNo2 = -1;
                }
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();
                int row = ctlDataGrid1.CurrentCell.RowNumber;
                int col = ctlDataGrid1.CurrentCell.ColumnNumber;
                if (col != 0)
                {
                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                }
                if ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid2.Columns[col] != null)
                {
                    //设置前背景色
                    ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid2.Columns[col]).DataGridTextBoxColumn.TextBox.BackColor = efc; //Color.SteelBlue;			
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
        }

        private void ctlDataGrid3_m_evtCurrentCellChanged(object sender, System.EventArgs e)
        {
            try
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthSetColNo3();
                if (this.intRowNo3 > -1)
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthAddItemToCulateClass(this.intRowNo3, 3);
                    this.intRowNo3 = -1;
                }
                //设置前背景色
                int row = ctlDataGrid3.CurrentCell.RowNumber;
                int col = ctlDataGrid3.CurrentCell.ColumnNumber;
                if (col != 0)
                {
                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                }
                if ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid3.Columns[col] != null)
                {
                    ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid3.Columns[col]).DataGridTextBoxColumn.TextBox.BackColor = efc; //Color.SteelBlue;			
                }
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
        }

        private void ctlDataGrid4_m_evtCurrentCellChanged(object sender, System.EventArgs e)
        {
            try
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthSetColNo4();
                if (this.intRowNo4 > -1)
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthAddItemToCulateClass(this.intRowNo4, 4);
                    this.intRowNo4 = -1;
                }
                //设置前背景色
                int row = ctlDataGrid4.CurrentCell.RowNumber;
                int col = ctlDataGrid4.CurrentCell.ColumnNumber;
                if (col != 0)
                {
                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                }
                if ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid4.Columns[col] != null)
                {
                    ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid4.Columns[col]).DataGridTextBoxColumn.TextBox.BackColor = efc; //Color.SteelBlue;			
                }
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
        }

        private void ctlDataGrid5_m_evtCurrentCellChanged(object sender, System.EventArgs e)
        {
            try
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthSetColNo5();
                if (this.intRowNo5 > -1)
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthAddItemToCulateClass(this.intRowNo5, 5);
                    this.intRowNo5 = -1;
                }

                //设置前背景色
                int row = ctlDataGrid5.CurrentCell.RowNumber;
                int col = ctlDataGrid5.CurrentCell.ColumnNumber;

                if (col != 0)
                {
                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                }
                if ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid5.Columns[col] != null)
                {
                    ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid5.Columns[col]).DataGridTextBoxColumn.TextBox.BackColor = efc; //Color.SteelBlue;			
                }
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
        }

        private void ctlDataGrid6_m_evtCurrentCellChanged(object sender, System.EventArgs e)
        {
            try
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthSetColNo6();
                if (this.intRowNo6 > -1)
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthAddItemToCulateClass(this.intRowNo6, 6);
                    this.intRowNo6 = -1;
                }
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();
                //设置前背景色
                int row = ctlDataGrid6.CurrentCell.RowNumber;
                int col = ctlDataGrid6.CurrentCell.ColumnNumber;
                if (col != 0)
                {
                    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                }
                if ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid6.Columns[col] != null)
                {
                    ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGrid6.Columns[col]).DataGridTextBoxColumn.TextBox.BackColor = efc; //Color.SteelBlue;			
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
        }
        #endregion

        private void listView3_Leave(object sender, System.EventArgs e)
        {
            this.listView3.Hide();
        }

        private void listView3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthListViewKeyDown3(e);
        }

        private void listView3_DoubleClick(object sender, System.EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthListViewDoubleClick3();
        }

        private void listView2_Leave(object sender, System.EventArgs e)
        {
            this.listView2.Hide();
            //			((clsCtl_DoctorWorkstation)this.objController).m_mthListViewDoubleClick2();
        }

        private void listView2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthListViewKeyDown2(e);
        }

        private void btSave_Click(object sender, System.EventArgs e)
        {
            this.btSave.Enabled = false;
            ((clsCtl_DoctorWorkstation)this.objController).m_mthSaveAllData();
            this.btSave.Enabled = true;
        }

        private void BtExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void lblIsVip_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("该病人符合先诊疗后结算！");
        }

        public void m_mthEnableIsVip()
        {
            this.lblIsVip.ForeColor = System.Drawing.Color.Red;
            this.lblIsVip.Enabled = true;
            this.lblIsVip.Cursor = Cursors.Hand;
        }

        public void m_mthDisableIsVip()
        {
            this.lblIsVip.ForeColor = System.Drawing.Color.Gray;
            this.lblIsVip.Enabled = false;
        }

        private void m_PatInfo_PatientChanged(object sender, string RegID)
        {
            this.rdoZzsq.SelectedIndex = 0;
            //记录调处方前的医生ID
            m_mthClearDoctorInof();
            //赋值给处方查询控件
            this.txtLoadRecipeNo1.PatientID = this.m_PatInfo.PatientID;
            //赋值病人类型给计费类，因为计费是按病人身份来计算的。
            ((clsCtl_DoctorWorkstation)this.objController).objCalPatientCharge.PatientType = this.m_PatInfo.PatientType;
            //病人费用限额
            ((clsCtl_DoctorWorkstation)this.objController).objCalPatientCharge.ChargeLimit = this.m_PatInfo.Limit;

            //只要病人变更就关闭之前的欠费提醒窗口
            if (((clsCtl_DoctorWorkstation)this.objController).blnIfDMShow == true && !((clsCtl_DoctorWorkstation)this.objController).frmDM.IsDisposed)
            {
                ((clsCtl_DoctorWorkstation)this.objController).frmDM.Close();
            }
            //判断病人是否为VIP
            if (this.m_PatInfo.m_strIsVip != null && this.m_PatInfo.m_strIsVip == "1")
            {
                m_mthEnableIsVip();
                //显示欠费信息
                ((clsCtl_DoctorWorkstation)this.objController).m_mthShowDebtMessage(this.m_PatInfo.PatientCardID);
            }
            else
            {
                m_mthDisableIsVip();
            }
            //更新病人就诊状态和保存数据在就诊列表。
            if (((clsCtl_DoctorWorkstation)this.objController).m_blnInsertData())
            {
                //显示急诊 
                if (objDclDoctor.m_blnCheckRegEmer(this.m_PatInfo.RegTypeID))
                {
                    intURGENCY_INT = 1;
                }
                else
                {
                    intURGENCY_INT = 0;
                }
                ((clsCtl_DoctorWorkstation)this.objController).m_mthShowRecipeEmer(intURGENCY_INT);
            }
            else
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthShowRecipeEmer(0);
            }

            if (this.m_PatInfo.Hypersusceptibility.Trim() != "")
            {
                if (frmAllergich != null)
                {
                    frmAllergich.txtInfo.Text = this.m_PatInfo.Hypersusceptibility.Trim();
                    frmAllergich.Show();
                }
            }
            else
            {
                if (frmAllergich != null)
                {
                    frmAllergich.Hide();
                    frmAllergich.txtInfo.Text = "";
                }
            }
            this.Focus();

            if (this.m_PatInfo.PayTypeID.Trim() == ((clsCtl_DoctorWorkstation)this.objController).YBSpecialPayTypeID)
            {
                this.cmbRecipeType.SelectedIndex = ((clsCtl_DoctorWorkstation)this.objController).YBSpecialRecTypeID;
            }

            //更换病人时清空上一病人历史处方号
            this.txtLoadRecipeNo1.m_mthClearText();

            //清空中药用法
            this.cmbCooking.Text = "";
            this.cmbCooking.SelectedIndex = -1;

            ((clsCtl_DoctorWorkstation)this.objController).m_mthCheckYBPayType();

            clsDcl_DoctorWorkstation dcl = new clsDcl_DoctorWorkstation();
            this.txtWeight.Text = dcl.GetPatientWeight(this.m_PatInfo.PatientID);
            dcl = null;
        }

        private void btClear_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("是否要清空所有数据?", "ICare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            m_mthFillPrimaryDoctor();
            this.txtLoadRecipeNo1.m_mthClearText();
            ((clsCtl_DoctorWorkstation)this.objController).m_mthClearAllData();
        }

        private void radioButton1_CheckedChanged(object sender, System.EventArgs e)
        {
            //			if(radioButton1.Checked)
            //			{
            //			radioButton1.Tag="2";
            //			this.btReUse.Enabled=true;
            //			
            //			}
            //			else
            //			{
            //			radioButton1.Tag="0";
            //			this.btReUse.Enabled=false;
            //			}
            //			((clsCtl_DoctorWorkstation)this.objController).m_mthGetRepiceNo();
        }

        private void cmbRipecNo_Enter(object sender, System.EventArgs e)
        {
            //			cmbRipecNo.DroppedDown=true;
            //			if(cmbRipecNo.SelectedIndex<1&&cmbRipecNo.Items.Count>0)
            //			{
            //				cmbRipecNo.SelectedIndex=0;
            //			}

        }

        private void cmbRipecNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //			((clsCtl_DoctorWorkstation)this.objController).m_mthCreatCalObj();
                //			((clsCtl_DoctorWorkstation)this.objController).m_mthFindRecipeByID(cmbRipecNo.Text);
            }
        }

        private void m_btVindicateTem_Click(object sender, System.EventArgs e)
        {
            //			m_txtDiagPort.Focus();
            //			m_objTemplate.m_mthManageTemplate();
        }

        private void frmDoctorWorkstation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.N:
                        this.m_btVindicateTem_Click(null, null);
                        break;
                    case Keys.M:
                        this.btShowTemplate2_Click(null, null);
                        break;
                }

            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthFormKeyDown(e);
        }

        private void numericUpDown1_ValueChanged(object sender, System.EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthChangeCMTimes();
        }

        private void m_dtgWaitReg_DoubleClick(object sender, System.EventArgs e)
        {
            //MessageBox.Show("a");
        }

        private void m_dtgWaitReg_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_AddNewTake();
        }
        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            if (this.tabControl1.SelectedIndex > 2)
            {
                this.panel4.Height = 32;
            }
            else
            {
                this.panel4.Height = 0;
            }
            switch (this.tabControl1.SelectedIndex)
            {
                case 0:
                    //					this.m_btnBackWait.Visible = true;
                    //					this.m_btnEndTake.Visible = true;
                    //					this.m_btnEndTake.Enabled = false;
                    //					this.m_btnRefReg.Visible = true;
                    this.m_btnBackWait.Text = "接诊(&J)";
                    break;
                case 1:
                    //					this.m_btnBackWait.Visible = true;
                    //					this.m_btnEndTake.Visible = true;
                    //					this.m_btnRefReg.Visible = false;
                    //					this.m_btnEndTake.Enabled = true;
                    this.m_btnBackWait.Text = "候诊(&W)";
                    this.m_btnEndTake.Text = "结诊(&O)";
                    break;
                default:
                    //					this.m_btnBackWait.Visible = false;
                    //					this.m_btnEndTake.Visible = false;
                    //					this.m_btnRefReg.Visible = false;
                    break;
            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthSetFocus();
        }

        private void m_btnEndTake_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_EndTakeReg(1);
        }

        private void m_btnBackWait_Click(object sender, System.EventArgs e)
        {
            //			if(this.tabControl1.SelectedIndex == 1)
            //			{
            //				((clsCtl_DoctorWorkstation)this.objController).m_mthReturnWait();
            //			}
            if (this.m_btnBackWait.Text == "候诊(&W)")
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthReturnWait();
            }
            if (this.m_btnBackWait.Text == "接诊(&J)")
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_AddNewTake();

            }

        }

        private void m_dtgWaitReg_m_evtCurrentCellChanged(object sender, System.EventArgs e)
        {

        }

        private void m_btnRefReg_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_GetWaitReg();
        }

        private void cmbFindAccordRecipe_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (cmbFindAccordRecipe.SelectedIndex)
            {
                case 0://项目名称
                    cmbFindAccordRecipe.Tag = "RECIPENAME_CHR";
                    break;
                case 1://助记码
                    cmbFindAccordRecipe.Tag = "USERCODE_CHR";
                    break;
                case 2://拼音
                    cmbFindAccordRecipe.Tag = "PYCODE_CHR";
                    break;
                case 3://五笔
                    cmbFindAccordRecipe.Tag = "WBCODE_CHR";
                    break;
            }
            this.txtFindAccordRecipe.Select();
        }

        private void txtFindAccordRecipe_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthFindAccordRecipe(this.txtFindAccordRecipe.Text);
            }
        }

        private void btPrint_Click(object sender, System.EventArgs e)
        {
            this.btPrint.Tag = "1";
            if (((clsCtl_DoctorWorkstation)this.objController).objCalPatientCharge.HospitalFlag == 0)
            {
                this.printDocument1.DefaultPageSettings.Landscape = true;
            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthShowPrint();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthPrint(e);
        }
        #region 删除DataGrid的数据
        private void ctlDataGrid1_m_evtDataGridKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            int row = this.ctlDataGrid1.CurrentCell.RowNumber;
            int col = this.ctlDataGrid1.CurrentCell.ColumnNumber;

            if (e.KeyCode == Keys.F6)
            {
                string strSubItemID = this.ctlDataGrid1[row, ((clsCtl_DoctorWorkstation)this.objController).c_SubItemID].ToString();
                if (strSubItemID.StartsWith("[PK]"))
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthGetChargeItemByUsageID(ctlDataGrid1[row, ((clsCtl_DoctorWorkstation)this.objController).c_UsageID].ToString().Trim(), true, strSubItemID.Replace("[PK]", ""), row);
                }

                string strReItemID = this.ctlDataGrid1[row, ((clsCtl_DoctorWorkstation)this.objController).c_resubitem].ToString();
                if (strReItemID.StartsWith("[PK]"))
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthGetChargeItemByItem(strReItemID.Replace("[PK]", ""), -1, null);
                }

                if (this.ctlDataGrid1[row, ((clsCtl_DoctorWorkstation)this.objController).c_GroupNo].ToString().Trim() != "")
                {
                    DelRecGroupFlag = true;
                }

                ((clsCtl_DoctorWorkstation)this.objController).m_mthDeleteGroup(row);
                ((clsCtl_DoctorWorkstation)this.objController).m_mthSetRecipeType(row + 1);
                ctlDataGrid1.CurrentCell = new DataGridCell(this.ctlDataGrid1.CurrentCell.RowNumber, 0);
                //因为DataGrid有个Bug所以发送下面的模拟键
                SendKeys.SendWait("{Right}");
                SendKeys.SendWait("{Left}");
                SendKeys.SendWait("{Left}");
                DelRecGroupFlag = false;
                return;
            }

            if (e.KeyCode == Keys.Left)
            {
                if (col == ((clsCtl_DoctorWorkstation)this.objController).c_UsageName)
                {
                    this.ctlDataGrid1.CurrentCell = new DataGridCell(row, ((clsCtl_DoctorWorkstation)this.objController).c_Count);
                }
                else if (col == ((clsCtl_DoctorWorkstation)this.objController).c_Total)
                {
                    this.ctlDataGrid1.CurrentCell = new DataGridCell(row, ((clsCtl_DoctorWorkstation)this.objController).c_Day);
                }
            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthFormKeyDown(e);
        }
        private void ctlDataGrid2_m_evtDataGridKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                int intRow = ctlDataGrid2.CurrentCell.RowNumber;

                string strReItemID = this.ctlDataGrid2[intRow, ((clsCtl_DoctorWorkstation)this.objController).cm_resubitem].ToString();
                if (strReItemID.StartsWith("[PK]"))
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthGetChargeItemByItem(strReItemID.Replace("[PK]", ""), -1, null);
                }

                if (this.ctlDataGrid2[intRow, 9].ToString().Trim() != "")
                {
                    ((clsCtl_DoctorWorkstation)this.objController).objCalPatientCharge.m_mthDelteChargeItem(int.Parse(this.ctlDataGrid2[intRow, 9].ToString()));
                }
                this.ctlDataGrid2.m_mthDeleteRow(intRow);
                ctlDataGrid2.CurrentCell = new DataGridCell(this.ctlDataGrid2.CurrentCell.RowNumber, 0);
                //因为DataGrid有个Bug所以发送下面的模拟键
                SendKeys.SendWait("{Right}");
                SendKeys.SendWait("{Left}");
                SendKeys.SendWait("{Left}");
                return;

            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthFormKeyDown(e);
        }
        private void ctlDataGrid3_m_evtDataGridKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                int intRow = ctlDataGrid3.CurrentCell.RowNumber;
                string strReItemID = this.ctlDataGrid3[intRow, ((clsCtl_DoctorWorkstation)this.objController).t_resubitem].ToString();

                if (strReItemID.StartsWith("[PK]"))
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthGetChargeItemByItem(strReItemID.Replace("[PK]", ""), -1, null);
                }

                if (this.ctlDataGrid3[intRow, 10].ToString().Trim() != "")//如果行号不为空
                {
                    if (this.ctlDataGrid3[intRow, 18].ToString().Trim() != "")
                    {
                        string strTemp = this.ctlDataGrid3[intRow, 18].ToString().Trim();
                        if (MessageBox.Show("注意!此项目已开申请单,删除些项目要把该申请单\n的所有项目删除,是否继续?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            for (int i = this.ctlDataGrid3.RowCount - 1; i > -1; i--)
                            {
                                if (this.ctlDataGrid3[i, 18].ToString().Trim() == strTemp)
                                {
                                    ((clsCtl_DoctorWorkstation)this.objController).objCalPatientCharge.m_mthDelteChargeItem(int.Parse(this.ctlDataGrid3[intRow, 10].ToString()));
                                    this.ctlDataGrid3.m_mthDeleteRow(i);
                                }
                            }
                        }
                    }
                    else
                    {
                        ((clsCtl_DoctorWorkstation)this.objController).objCalPatientCharge.m_mthDelteChargeItem(int.Parse(this.ctlDataGrid3[intRow, 10].ToString()));
                        this.ctlDataGrid3.m_mthDeleteRow(intRow);
                    }
                }
                else
                {
                    this.ctlDataGrid3.m_mthDeleteRow(intRow);
                }

                ctlDataGrid3.CurrentCell = new DataGridCell(this.ctlDataGrid3.CurrentCell.RowNumber, 0);
                SendKeys.SendWait("{Right}");
                SendKeys.SendWait("{Left}");
                SendKeys.SendWait("{Left}");
                return;

            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthFormKeyDown(e);
        }
        private void ctlDataGrid4_m_evtDataGridKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                int intRow = ctlDataGrid4.CurrentCell.RowNumber;
                string strReItemID = this.ctlDataGrid4[intRow, ((clsCtl_DoctorWorkstation)this.objController).t_resubitem].ToString();

                if (strReItemID.StartsWith("[PK]"))
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthGetChargeItemByItem(strReItemID.Replace("[PK]", ""), -1, null);
                }

                if (this.ctlDataGrid4[intRow, 10].ToString().Trim() != "")
                {
                    ((clsCtl_DoctorWorkstation)this.objController).objCalPatientCharge.m_mthDelteChargeItem(int.Parse(this.ctlDataGrid4[intRow, 10].ToString()));
                }
                ((clsCtl_DoctorWorkstation)this.objController).m_mthDelApplyBill(intRow);
                this.ctlDataGrid4.m_mthDeleteRow(intRow);
                ctlDataGrid4.CurrentCell = new DataGridCell(this.ctlDataGrid4.CurrentCell.RowNumber, 0);
                SendKeys.SendWait("{Right}");
                SendKeys.SendWait("{Left}");
                SendKeys.SendWait("{Left}");
                return;

            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthFormKeyDown(e);
        }
        private void ctlDataGrid5_m_evtDataGridKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                int intRow = ctlDataGrid5.CurrentCell.RowNumber;
                string strReItemID = this.ctlDataGrid5[intRow, ((clsCtl_DoctorWorkstation)this.objController).o_resubitem].ToString();

                if (strReItemID.StartsWith("[PK]"))
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthGetChargeItemByItem(strReItemID.Replace("[PK]", ""), -1, null);
                }

                if (this.ctlDataGrid5[intRow, 9].ToString().Trim() != "")
                {
                    ((clsCtl_DoctorWorkstation)this.objController).objCalPatientCharge.m_mthDelteChargeItem(int.Parse(this.ctlDataGrid5[intRow, 9].ToString()));
                }
                this.ctlDataGrid5.m_mthDeleteRow(intRow);
                ctlDataGrid5.CurrentCell = new DataGridCell(this.ctlDataGrid5.CurrentCell.RowNumber, 0);
                //因为DataGrid有个Bug所以发送下面的模拟键
                SendKeys.SendWait("{Right}");
                SendKeys.SendWait("{Left}");
                SendKeys.SendWait("{Left}");
                return;
            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthFormKeyDown(e);
        }
        private void ctlDataGrid6_m_evtDataGridKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                int intRow = ctlDataGrid6.CurrentCell.RowNumber;
                string strReItemID = this.ctlDataGrid6[intRow, ((clsCtl_DoctorWorkstation)this.objController).o_resubitem].ToString();

                if (strReItemID.StartsWith("[PK]"))
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthGetChargeItemByItem(strReItemID.Replace("[PK]", ""), -1, null);
                }

                if (this.ctlDataGrid6[intRow, 9].ToString().Trim() != "")
                {
                    ((clsCtl_DoctorWorkstation)this.objController).objCalPatientCharge.m_mthDelteChargeItem(int.Parse(this.ctlDataGrid6[intRow, 9].ToString()));
                }
                this.ctlDataGrid6.m_mthDeleteRow(intRow);
                ctlDataGrid6.CurrentCell = new DataGridCell(this.ctlDataGrid6.CurrentCell.RowNumber, 0);
                SendKeys.SendWait("{Right}");
                SendKeys.SendWait("{Left}");
                SendKeys.SendWait("{Left}");
                return;


            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthFormKeyDown(e);
        }
        #endregion

        private void m_dtgWaitReg_m_evtDataGridKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {


        }

        private void m_dtgTake_m_evtDataGridKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }

        private void btDel_Click(object sender, System.EventArgs e)
        {
            if (this.btSave.Tag == null)
            {
                return;
            }

            if (((clsCtl_DoctorWorkstation)this.objController).m_blnCheckRecipe(this.btSave.Tag.ToString()) == false)
                return;

            if (this.btDel.Text.IndexOf("删") > -1)
            {
                ctmDel.Show(btDel, new Point(btDel.Width, btDel.Height));
            }
            else
            {
                if (MessageBox.Show("是否要" + this.btDel.Text.Substring(0, 2) + "当前处方?", "ICare", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthDelRecipe("0");
                }
            }
        }

        private void cmbApply_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_mthShowApplyBill();
            }
        }
        #region 打开申请单
        /// <summary>
        /// 填充申请单信息
        /// </summary>
        /// <param name="objApplyReportVO"></param>
        private void m_mthFillApplyData(out clsApplyReport_T_VO objApplyReportVO)
        {
            objApplyReportVO = new clsApplyReport_T_VO();
            objApplyReportVO.m_DtmAppryDate = DateTime.Now;
            objApplyReportVO.m_StrAddress = this.m_PatInfo.PatientHomeAddress;
            objApplyReportVO.m_StrClinicDiagnose = this.objCaseHistory.Diag;
            objApplyReportVO.m_StrDeptID = this.m_PatInfo.DeptID;
            objApplyReportVO.m_StrDeptName = this.m_PatInfo.DeptName;
            if (this.m_PatInfo.DoctorName != null)
                objApplyReportVO.m_StrDoctor = this.m_PatInfo.DoctorName;

            objApplyReportVO.m_StrPatientAge = this.m_PatInfo.PatientAge;
            objApplyReportVO.m_StrPatientCardID = this.m_PatInfo.PatientCardID;
            objApplyReportVO.m_StrPatientID = this.m_PatInfo.PatientID;
            objApplyReportVO.m_StrPatientName = this.m_PatInfo.PatientName;
            objApplyReportVO.m_StrPatientSex = this.m_PatInfo.PatientSex;
            objApplyReportVO.m_StrRelationCall = this.m_PatInfo.PatientTelephoneNo;

            objApplyReportVO.m_ObjApply_LungFunction_T_VO = new clsApply_LungFunction_T_VO();
            if (this.objCaseHistory.DiagCurr != "")
            {
                objApplyReportVO.m_ObjApply_LungFunction_T_VO.m_StrSickHistory = this.objCaseHistory.DiagCurr;
            }
            if (this.objCaseHistory.DiagHis != "")
            {
                objApplyReportVO.m_ObjApply_LungFunction_T_VO.m_StrSickHistory += "\r\n" + this.objCaseHistory.DiagHis;
            }
            if (this.objCaseHistory.PersonHis != "")
            {
                objApplyReportVO.m_ObjApply_LungFunction_T_VO.m_StrSickHistory += "\r\n" + this.objCaseHistory.PersonHis;
            }

            objApplyReportVO.m_ObjApply_B_Ultrasonic_T_VO = new clsApply_B_Ultrasonic_T_VO();
            objApplyReportVO.m_ObjApply_B_Ultrasonic_T_VO.m_StrMedicaHistory = this.objCaseHistory.DiagCurr + "\r\n" + this.objCaseHistory.DiagHis + "\r\n" + this.objCaseHistory.PersonHis;
            objApplyReportVO.m_ObjApply_B_Ultrasonic_T_VO.m_StrOtherCheck = this.objCaseHistory.AidCheck;

            objApplyReportVO.m_ObjApply_BCH_T_VO = new clsApply_BCH_T_VO();
            objApplyReportVO.m_StrCaseHistory = this.objCaseHistory.DiagCurr + "\r\n" + this.objCaseHistory.DiagHis + "\r\n" + this.objCaseHistory.PersonHis + "\r\n" + this.objCaseHistory.Anaphylaxis + "\r\n" + this.objCaseHistory.ExamineResult + this.objCaseHistory.AidCheck;

            objApplyReportVO.m_ObjApply_Cardiogram_T_VO = new clsApply_Cardiogram_T_VO();
            objApplyReportVO.m_ObjApply_Cardiogram_T_VO.m_StrNowSick = this.objCaseHistory.DiagCurr;
            objApplyReportVO.m_ObjApply_Cardiogram_T_VO.m_StrSickHistory = this.objCaseHistory.DiagHis;

            objApplyReportVO.m_ObjApply_DynCardiogram_T_VO = new clsApply_DynCardiogram_T_VO();
            objApplyReportVO.m_ObjApply_DynCardiogram_T_VO.m_StrNowSick = this.objCaseHistory.DiagCurr;
            objApplyReportVO.m_ObjApply_DynCardiogram_T_VO.m_StrSickHistory = this.objCaseHistory.DiagHis;

            objApplyReportVO.m_ObjApply_EEGBrain_T_VO = new clsApply_EEGBrain_T_VO();
            objApplyReportVO.m_ObjApply_EEGBrain_T_VO.m_StrNowSick = this.objCaseHistory.DiagCurr;
            objApplyReportVO.m_ObjApply_EEGBrain_T_VO.m_StrSickHistory = this.objCaseHistory.DiagHis;

            objApplyReportVO.m_ObjApply_FlatCardiogram_T_VO = new clsApply_FlatCardiogram_T_VO();
            objApplyReportVO.m_ObjApply_FlatCardiogram_T_VO.m_StrNowSick = this.objCaseHistory.DiagCurr;
            objApplyReportVO.m_ObjApply_FlatCardiogram_T_VO.m_StrSickHistory = this.objCaseHistory.DiagHis;

            objApplyReportVO.m_ObjApply_TCDBrain_T_VO = new clsApply_TCDBrain_T_VO();
            objApplyReportVO.m_ObjApply_TCDBrain_T_VO.m_StrNowSick = this.objCaseHistory.DiagCurr;
            objApplyReportVO.m_ObjApply_TCDBrain_T_VO.m_StrSickHistory = this.objCaseHistory.DiagHis;

            objApplyReportVO.m_ObjApply_CTCheck_T_VO = new clsApply_CTCheck_T_VO();
            objApplyReportVO.m_ObjApply_CTCheck_T_VO.m_StrBodyCheck = this.objCaseHistory.AidCheck + "\r\n" + this.objCaseHistory.ExamineResult;
            objApplyReportVO.m_ObjApply_CTCheck_T_VO.m_StrSickHistory = this.objCaseHistory.DiagCurr + "\r\n" + this.objCaseHistory.DiagHis + "\r\n" + this.objCaseHistory.PersonHis;

            objApplyReportVO.m_ObjApply_DR_T_VO = new clsApply_DR_T_VO();
            objApplyReportVO.m_ObjApply_DR_T_VO.m_Str_ADR_CaseOrClinic = this.objCaseHistory.DiagCurr + "\r\n" + this.objCaseHistory.DiagHis + "\r\n" + this.objCaseHistory.PersonHis + "\r\n" + this.objCaseHistory.Anaphylaxis + "\r\n" + this.objCaseHistory.ExamineResult + "\r\n" + this.objCaseHistory.AidCheck;

            objApplyReportVO.m_ObjApply_Gastroscopy_T_VO = new clsApply_Gastroscopy_T_VO();
            objApplyReportVO.m_ObjApply_Gastroscopy_T_VO.m_StrBodyCheck = this.objCaseHistory.ExamineResult;
            objApplyReportVO.m_ObjApply_Gastroscopy_T_VO.m_StrSickHistory = this.objCaseHistory.DiagCurr + "\r\n" + this.objCaseHistory.DiagHis + "\r\n" + this.objCaseHistory.PersonHis;



            objApplyReportVO.m_ObjApply_IntestineMirror_T_VO = new clsApply_IntestineMirror_T_VO();
            objApplyReportVO.m_ObjApply_IntestineMirror_T_VO.m_StrBodyCheck = this.objCaseHistory.ExamineResult;
            objApplyReportVO.m_ObjApply_IntestineMirror_T_VO.m_StrSickHistory = this.objCaseHistory.DiagCurr + "\r\n" + this.objCaseHistory.DiagHis + "\r\n" + this.objCaseHistory.PersonHis;

            objApplyReportVO.m_StrDeliverDoctorName = this.LoginInfo.m_strEmpName.Trim();

        }
        private void m_mthShowApplyBill()
        {
            if (this.m_PatInfo.PatientID.Trim() == "")
            {
                MessageBox.Show("请先输入病人资料!", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_PatInfo.txtCardID.Focus();
                return;
            }

        }
        #endregion
        private void m_PatInfo_PatientTypeChanged()
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthPatientTypeChanged();

            if (this.m_PatInfo.PayTypeID.Trim() == ((clsCtl_DoctorWorkstation)this.objController).YBSpecialPayTypeID)
            {
                this.cmbRecipeType.SelectedIndex = ((clsCtl_DoctorWorkstation)this.objController).YBSpecialRecTypeID;
            }

            ((clsCtl_DoctorWorkstation)this.objController).m_mthCheckYBPayType();
        }
        private void m_mthFillPrimaryDoctor()
        {
            if (strDoctorName == "-1")
            {
                return;
            }
            this.m_PatInfo.txtRegisterDoctor.Text = strDoctorName;
            this.m_PatInfo.DoctorID = strDoctorID;
            this.m_PatInfo.DoctorNo = strDoctorNo;
            this.m_PatInfo.txtRegisterDept.Text = strDepName;
            this.m_PatInfo.DeptID = strDepID;
            this.m_PatInfo.txtType.Text = strPatientTypeName;
            this.m_PatInfo.PayTypeID = strPatientTypeID;
        }
        /// <summary>
        /// 复用按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btReUse_Click(object sender, System.EventArgs e)
        {
            this.btReUse.Enabled = false;
            if (this.txtLoadRecipeNo1.RecipeInfo == null)
            {
                this.btReUse.Enabled = true;
                return;
            }
            m_mthFillPrimaryDoctor();
            ((clsCtl_DoctorWorkstation)this.objController).m_mthCreatCalObj();

            clsRecipeInfo_VO obj = this.txtLoadRecipeNo1.RecipeInfo;
            ((clsCtl_DoctorWorkstation)this.objController).m_mthFindChageItemByID(obj.m_strOUTPATRECIPEID_CHR, false);
            if (this.strHisPatientTypeID != this.strPatientTypeID)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthPatientTypeChanged();
            }
            else
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCheckItemDiscount();
            }

            this.btSave.Tag = null;
            this.btPutIn.Tag = null;
            this.btReUse.Enabled = true;
            if (this.lbeSumMoney.Text.Trim() == "0")
            {
                MessageBox.Show("对不起,找不到可复用项目,\n该处方可能是门诊收费直接开的处方。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthSetFocus();
        }

        //下面变量记录当前登陆医生的信息和病人的身份类型，目的是为了在复用处方时能还原。
        private string strDoctorName = "-1";
        private string strDoctorID = "-1";
        private string strDoctorNo = "-1";
        private string strDepName = "-1";
        private string strDepID = "-1";
        private string strPatientTypeName = "-1";
        private string strPatientTypeID = "-1";
        private string strHisPatientTypeID = "-1"; //记录复用处方所对应的费别

        private void m_mthClearDoctorInof()
        {
            strDoctorName = "-1";
            strDoctorID = "-1";
            strDoctorNo = "-1";
            strDepName = "-1";
            strDepID = "-1";
            strPatientTypeName = "-1";
            strPatientTypeID = "-1";
        }
        /// <summary>
        /// 选择处方时产生的事件处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLoadRecipeNo1_RecipeSelected(object sender, System.EventArgs e)
        {
            #region 旧码
            /***
            //当第一次调处方时，记录当前登陆医生的信息和病人的身份类型，目的是为了在复用处方时能还原
			if( strDoctorName =="-1")
			{
				strDoctorName=this.m_PatInfo.txtRegisterDoctor.Text;
				strDoctorID=this.m_PatInfo.DoctorID;
				strDoctorNo=this.m_PatInfo.DoctorNo;
				strDepName=this.m_PatInfo.txtRegisterDept.Text;
				strDepID=this.m_PatInfo.DeptID;
				strPatientTypeName=this.m_PatInfo.txtType.Text;
				strPatientTypeID=this.m_PatInfo.PayTypeID;
			}
			clsRecipeInfo_VO obj=this.txtLoadRecipeNo1.RecipeInfo;           
			this.m_PatInfo.txtRegisterDoctor.Text=obj.m_strDoctorName;
			this.m_PatInfo.DoctorID=obj.m_strDoctorID;
			this.m_PatInfo.DoctorNo=obj.m_strDoctorNo;
			this.m_PatInfo.txtRegisterDept.Text=obj.m_strDepName;
			this.m_PatInfo.DeptID=obj.m_strDepID;
			this.m_PatInfo.txtType.Text=obj.m_strPatientTypeName;
			this.m_PatInfo.PayTypeID = obj.m_strPatientTypeID;
            this.strHisPatientTypeID = obj.m_strPatientTypeID;
            if (Convert.ToInt32(obj.m_strRECIPEFLAG_INT) <= this.m_cmbRecipeType.Items.Count)
            {
                this.m_cmbRecipeType.SelectedIndex = Convert.ToInt32(obj.m_strRECIPEFLAG_INT) - 1;
            }
            if (obj.m_intRecipetypeid <= this.cmbRecipeType.Items.Count - 1)
            {
                this.cmbRecipeType.SelectedIndex = obj.m_intRecipetypeid;
            }
			((clsCtl_DoctorWorkstation)this.objController).m_mthFindRecipeByID(obj);
			this.btReUse.Focus();
            ***/
            #endregion

            clsRecipeInfo_VO RecipeInfo_VO = this.txtLoadRecipeNo1.RecipeInfo;

            frmDoctReUseInfo fdru = new frmDoctReUseInfo(RecipeInfo_VO);
            fdru.IsChildPrice = ((clsCtl_DoctorWorkstation)this.objController).IsChildPrice;
            fdru.LackMedicine = ((clsCtl_DoctorWorkstation)this.objController).isShowLackMedicine;
            fdru.ItemInputMode = ((clsCtl_DoctorWorkstation)this.objController).ItemInputMode;
            DialogResult dg = fdru.ShowDialog();
            if (dg == DialogResult.OK || dg == DialogResult.Retry || dg == DialogResult.Yes)
            {
                if (dg == DialogResult.Yes)
                {
                    this.txtLoadRecipeNo1.m_mthClearText();
                }

                //复用历史处方类型                
                if (RecipeInfo_VO.m_intRecipetypeid <= this.cmbRecipeType.Items.Count - 1)
                {
                    this.cmbRecipeType.SelectedIndex = RecipeInfo_VO.m_intRecipetypeid;
                }

                //初始化服数为1;	
                this.numericUpDown1.Value = 1;

                if (dg == DialogResult.OK || dg == DialogResult.Yes)
                {
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthCreatCalObj();
                    this.btSave.Tag = null;
                    this.btPutIn.Tag = null;
                }
                else if (dg == DialogResult.Retry)
                {
                    //赋值给一个隐藏了的lable控件，为了用它的TextChange事件来判断控钮的可用状态
                    this.lbeFlag.Text = RecipeInfo_VO.m_intPSTATUS_INT.ToString();

                    ((clsCtl_DoctorWorkstation)this.objController).objCalPatientCharge = new clsCalcPatientCharge(this.m_PatInfo.PatientID, this.m_PatInfo.PayTypeID, this.m_PatInfo.Limit, ((clsCtl_DoctorWorkstation)this.objController).m_objComInfo.m_strGetHospitalTitle(), this.m_PatInfo.PatientType, this.m_PatInfo.Discount);
                    ((clsCtl_DoctorWorkstation)this.objController).OPSApplyarr = new List<clsOutops_VO>();
                    this.btSave.Tag = RecipeInfo_VO.m_strOUTPATRECIPEID_CHR;
                    this.btPutIn.Tag = null;
                }

                ((clsCtl_DoctorWorkstation)this.objController).m_mthReUseRecipeItem(fdru.hasItem, fdru.hasEntry);
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCheckItemDiscount();
                ((clsCtl_DoctorWorkstation)this.objController).m_mthFormatDataGrid();
                ((clsCtl_DoctorWorkstation)this.objController).m_mthSetFocus();
            }
            else if (dg == DialogResult.Abort)
            {
                this.txtLoadRecipeNo1.m_mthClearText();
            }
        }

        private void frmDoctorWorkstation_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!noopen)
            {
                if (MessageBox.Show("退出窗口前请保存所有未保存的数据。\r\n\r\n是否继续退出？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            //为了关闭窗口时DataGrid存的BUG引发程序出错，下面代码不能少。
            this.ctlDataGrid1.Enabled = false;
            this.ctlDataGrid2.Enabled = false;
            this.ctlDataGrid3.Enabled = false;
            this.ctlDataGrid4.Enabled = false;
            this.ctlDataGrid5.Enabled = false;
            this.ctlDataGrid6.Enabled = false;
            if (frmAllergich != null)
            {
                frmAllergich.Close();
            }
            frmAllergicl.Close();
            frmRecconf.Close();
            if (frmDiag != null)
            {
                frmDiag.Close();
                DiagnoseClient.frmDiagClientMain.PatientCalled -= new DiagnoseClient.PatientCalledEventHandler(frmDiagClientMain_PatientCalled);
            }
        }
        #region 加载申请单
        private void m_mthLoadApplyBill()
        {

            if (this.groupBox7.Visible == false)
            {
                return;
            }

        }
        #endregion

        private void btShowTemplate2_Click(object sender, System.EventArgs e)
        {

        }

        private void btCreatBill_Click(object sender, System.EventArgs e)
        {
            this.m_mthShowApplyBill();
        }

        private void m_PatInfo_PatientEnd(object sender, System.EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_EndTakeReg(2);
        }

        private void btCaseyHistory_Click(object sender, System.EventArgs e)
        {
            this.btPrint.Tag = "2";
            this.printDocument1.DefaultPageSettings.Landscape = false;
            ((clsCtl_DoctorWorkstation)this.objController).m_mthShowPrint();
        }

        private void txtCount_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if ((int)e.KeyChar >= 46 && (int)e.KeyChar <= 57 && (int)e.KeyChar != 47 || (int)e.KeyChar == 8)
            {

            }
            else
            {
                e.Handled = true;

            }
            if (e.KeyChar == '.')
            {
                if (tb.Text.Trim() == "")
                {
                    tb.Text = "0.";
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                    e.Handled = true;
                }
                if (tb.Text.IndexOf(".") > -1)
                {
                    e.Handled = true;
                }
            }
        }
        private void txtCount_KeyPress2(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if ((int)e.KeyChar > 46 && (int)e.KeyChar <= 57 && (int)e.KeyChar != 47 || (int)e.KeyChar == 8)
            {

            }
            else
            {
                e.Handled = true;

            }

        }
        private void TextBox_KeyPress3(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if ((int)e.KeyChar >= 46 && (int)e.KeyChar <= 57 && (int)e.KeyChar != 47 || (int)e.KeyChar == 8)
            {
                //-- 特殊处理 by kenny
                decimal decRes = 0;
                string strInput = string.Empty;
                if (tb.SelectedText.Length > 0)
                {
                    strInput = tb.Text.Replace(tb.SelectedText, "") + e.KeyChar.ToString();
                }
                else
                {
                    strInput = tb.Text + e.KeyChar.ToString();
                }
                if (decimal.TryParse(strInput, out decRes))
                {
                    if (decRes != 0 && decRes % 0.5M != 0)
                    {
                        com.digitalwave.iCare.gui.HIS.clsMain objMain = new com.digitalwave.iCare.gui.HIS.clsMain();
                        objMain.m_mthShowWarning(tb, "数量必须为0.5的倍数");
                        e.Handled = true;
                    }
                }
                //--
            }
            else
            {
                e.Handled = true;

            }
            if (e.KeyChar == '.')
            {
                if (tb.Text.Trim() == "")
                {
                    tb.Text = "0.";
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                    e.Handled = true;
                }
                if (tb.Text.IndexOf(".") > -1)
                {
                    e.Handled = true;
                }
            }
        }
        private void TextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (HandleInput)
            {
                e.Handled = true;
                HandleInput = false;
            }

        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.btPrint.Tag != null)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthBeginPrint();
            }

        }

        private void btGroup_Click(object sender, System.EventArgs e)
        {

        }

        private void btCancelGroup_Click(object sender, System.EventArgs e)
        {

        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (this.m_cmbFind.SelectedIndex == 1)
            {
                InputLanguage.CurrentInputLanguage = myInputLanguage;
            }

        }
        private int intCurRecipeRow = 0;
        private string strCurString = "";
        private void RecipeTextBox_Enter(object sender, EventArgs e)
        {
            intCurRecipeRow = this.ctlDataGrid1.CurrentCell.RowNumber;
            strCurString = this.ctlDataGrid1[intCurRecipeRow, 0].ToString().Trim();

        }
        private void RecipeTextBox_Leave(object sender, EventArgs e)
        {
            if (DelRecGroupFlag)
            {
                return;
            }

            string strtemp = this.ctlDataGrid1[intCurRecipeRow, 0].ToString().Trim();
            ((clsCtl_DoctorWorkstation)this.objController).m_mthSetRowNo(strtemp, intCurRecipeRow, strCurString, 1);
        }
        private void listView4_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthListView4KeyDown(e);

        }

        private void listView4_DoubleClick(object sender, System.EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthListViewDoubleClick4();
        }

        private void listView4_Leave(object sender, System.EventArgs e)
        {
            this.listView4.Hide();
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            myInputLanguage = InputLanguage.CurrentInputLanguage;
        }

        private void btInject_Click(object sender, System.EventArgs e)
        {
            this.btPrint.Tag = "3";
            if (((clsCtl_DoctorWorkstation)this.objController).m_objComInfo.m_strGetHospitalTitle().IndexOf("佛") > -1)
            {

                this.printDocument1.DefaultPageSettings.Landscape = true;
            }
            else
            {
                this.printDocument1.DefaultPageSettings.Landscape = false;
            }

            ((clsCtl_DoctorWorkstation)this.objController).m_mthShowPrint();
        }

        private void chkGroup_CheckedChanged(object sender, System.EventArgs e)
        {
            this.ctlDataGrid1.CurrentCell = new DataGridCell(this.ctlDataGrid1.RowCount, 0);
        }

        private void btNew_Click(object sender, System.EventArgs e)
        {
            //显示急诊 
            ((clsCtl_DoctorWorkstation)this.objController).m_mthShowRecipeEmer(intURGENCY_INT);

            //默认主方
            this.m_cmbRecipeType.SelectedIndex = 0;

            ((clsCtl_DoctorWorkstation)this.objController).m_mthNewRecipe();
        }

        private void btReGroup_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthReGroup();
        }

        private void lbeSumMoney_TextChanged(object sender, System.EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthIsOverFlow();
        }

        private void btCaseHistory_Click(object sender, System.EventArgs e)
        {
            if (this.m_PatInfo.PatientID.Trim() != "")
            {
                frmShowCaseHistory objfrm = new frmShowCaseHistory();
                objfrm.PatientID = this.m_PatInfo.PatientID.Trim();
                objfrm.PatientCardID = this.m_PatInfo.PatientCardID.Trim();
                objfrm.PatientName = this.m_PatInfo.PatientName.Trim();
                objfrm.PatientSex = this.m_PatInfo.PatientSex.Trim();
                objfrm.PatientAge = this.m_PatInfo.PatientAge.Trim();
                objfrm.m_IntStat = 0;
                DialogResult ret = objfrm.ShowDialog();
                if (ret == DialogResult.OK)
                {
                    if (objfrm.m_IntStat == 1)
                    {
                        this.objCaseHistory.m_mthClearData(1);
                        this.objCaseHistory.CaseHistoryStatus = 1;
                        this.objCaseHistory.ParentCaseHistoryID = objfrm.CaseHistoryInfo.m_strRecipeID;
                        this.objCaseHistory.Diag = objfrm.CaseHistoryInfo.strDiag;
                        this.objCaseHistory.DiagCurr = objfrm.CaseHistoryInfo.strDiagCurr;
                        this.objCaseHistory.DiagHis = objfrm.CaseHistoryInfo.strDiagHis;
                        this.objCaseHistory.DiagMain = DateTime.Now.ToString("yyyy-MM-dd");
                        this.objCaseHistory.ExamineResult = objfrm.CaseHistoryInfo.strExamineResult;
                        this.objCaseHistory.ReMark = objfrm.CaseHistoryInfo.strReMark;
                        this.objCaseHistory.Treatment = objfrm.CaseHistoryInfo.strTreatMent;
                        this.objCaseHistory.ChangeDepartment = objfrm.CaseHistoryInfo.strChangeDeparement;
                        this.objCaseHistory.PersonHis = objfrm.CaseHistoryInfo.m_strPRIHIS_VCHR;
                        this.objCaseHistory.AidCheck = objfrm.CaseHistoryInfo.strAidCheck;
                        this.objCaseHistory.Anaphylaxis = objfrm.CaseHistoryInfo.strAnaPhyLaXis;
                        this.tabControl1.SelectedIndex = 2;
                    }
                    else if (objfrm.m_IntStat == 2 || objfrm.m_IntStat == 3)
                    {
                        this.objCaseHistory.CaseHistoryStatus = 0;
                        this.objCaseHistory.ParentCaseHistoryID = "";
                        this.objCaseHistory.Diag = objfrm.CaseHistoryInfo.strDiag;
                        this.objCaseHistory.DiagCurr = objfrm.CaseHistoryInfo.strDiagCurr;
                        this.objCaseHistory.DiagHis = objfrm.CaseHistoryInfo.strDiagHis;
                        this.objCaseHistory.DiagMain = objfrm.CaseHistoryInfo.strDiagMain;
                        this.objCaseHistory.ExamineResult = objfrm.CaseHistoryInfo.strExamineResult;
                        this.objCaseHistory.ReMark = objfrm.CaseHistoryInfo.strReMark;
                        this.objCaseHistory.Treatment = objfrm.CaseHistoryInfo.strTreatMent;
                        this.objCaseHistory.PersonHis = objfrm.CaseHistoryInfo.m_strPRIHIS_VCHR;
                        this.objCaseHistory.AidCheck = objfrm.CaseHistoryInfo.strAidCheck;
                        this.objCaseHistory.ChangeDepartment = objfrm.CaseHistoryInfo.strChangeDeparement;
                        this.objCaseHistory.Anaphylaxis = objfrm.CaseHistoryInfo.strAnaPhyLaXis;
                        this.tabControl1.SelectedIndex = 2;

                        string CaseID = objfrm.CaseHistoryInfo.m_strRecipeID;
                        if (CaseID.Trim() != "")
                        {
                            clsICD10_VO[] objArrTemp;
                            clsDcl_DoctorWorkstation objDoct = new clsDcl_DoctorWorkstation();
                            long l = objDoct.m_mthIllnessInfo(CaseID, out objArrTemp);
                            if (l > 0 && objArrTemp != null)
                            {
                                System.Collections.Generic.List<clsICD10_VO> objArrList = new System.Collections.Generic.List<clsICD10_VO>();
                                objArrList.AddRange(objArrTemp);
                                this.objCaseHistory.ICD10 = objArrList;
                            }
                        }

                        if (objfrm.m_IntStat == 3)
                        {
                            ((clsCtl_DoctorWorkstation)this.objController).m_mthReUseRecipe(objfrm.CaseHistoryInfo.m_strRecipeID);
                        }
                    }
                }
                objfrm.Close();
            }
        }

        private void btPutIn_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthPutIn(false);
            if (this.m_PatInfo.m_strIsVip == "1")
            {
                this.btnPrefer.Enabled = true;
            }
        }

        private void m_mthSetEnabled(bool enable)
        {
            this.btSave.Enabled = enable;
            this.btnRecalc.Enabled = enable;
            this.ctlDataGrid1.Enabled = enable;
            this.ctlDataGrid2.Enabled = enable;
            this.ctlDataGrid3.Enabled = enable;
            this.ctlDataGrid4.Enabled = enable;
            this.ctlDataGrid5.Enabled = enable;
            this.ctlDataGrid6.Enabled = enable;
            this.m_PatInfo.txtRegisterDept.Enabled = enable;
            this.m_PatInfo.txtRegisterDoctor.Enabled = enable;
        }
        //这里用一个隐藏的Lable控件来控制按钮的状态
        private void lbeFlag_TextChanged(object sender, System.EventArgs e)
        {
            //0医生新建,1收费处新建,2已收费,-1作废,-2退票,3恢复,4 提交

            this.picris.Enabled = this.piclis.Enabled;
            switch (this.lbeFlag.Text.Trim())
            {
                case "-2":
                    m_mthSetEnabled(false);
                    this.btDel.Enabled = false;
                    this.btPutIn.Enabled = false;
                    break;
                case "0":
                    m_mthSetEnabled(true);
                    this.btPutIn.Enabled = true;
                    this.btDel.Text = "删除(Z)";
                    this.btDel.Enabled = true;
                    this.m_PatInfo.txtRegisterDoctor.Enabled = !((clsCtl_DoctorWorkstation)this.objController).isCanChangeDoctor;
                    break;
                case "2":
                    m_mthSetEnabled(false);
                    this.btPutIn.Enabled = false;
                    this.btDel.Enabled = false;
                    break;
                case "3":
                    m_mthSetEnabled(false);
                    this.btPutIn.Enabled = false;
                    this.btDel.Enabled = false;
                    break;
                case "4":
                    m_mthSetEnabled(false);
                    this.btDel.Text = "作废(Z)";
                    this.btDel.Enabled = true;
                    this.btPutIn.Enabled = false;
                    this.picris.Enabled = false;
                    break;
                default:
                    this.btDel.Text = "删除(Z)";
                    this.btDel.Enabled = false;
                    m_mthSetEnabled(true);
                    this.btPutIn.Enabled = false;
                    break;
            }
        }

        private void printDocument1_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.cmbRecipeType.SelectedIndex > -1)
            {
                ((clsRecipeType_VO)this.cmbRecipeType.SelectedItem).REMARK_VCHR = "";
            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthEndPrint();
        }

        private void ctlDataGrid4_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
        {
            if (this.ctlDataGrid4[e.m_intRowNumber, 18].ToString().Trim() != "")
            {
                MessageBox.Show("此项目已开申请单!", "提示");
                return;
            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthApplyCheckByItem(e.m_intRowNumber, 4);
        }

        private void ctlDataGrid3_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
        {
            if (!this.groupBox7.Visible)
            {
                return;
            }
            if (this.ctlDataGrid3[e.m_intRowNumber, 18].ToString().Trim() != "")
            {
                MessageBox.Show("此项目已开申请单!", "提示");
                return;
            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthShowTestApply(e.m_intRowNumber);
        }

        private void ra_Person_CheckedChanged(object sender, System.EventArgs e)
        {
            if (ra_Person.Checked)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_GetWaitReg();
                this.cmbDeparment.Enabled = false;
            }
            else
            {
                this.cmbDeparment.Enabled = true;
                this.cmbDeparment.Focus();
            }

        }

        private void cmbDeparment_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_GetWaitReg();
            }
        }

        private void btSelect_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_GetWaitReg();
        }
        #region
        private void ctlDataGrid2_Leave(object sender, System.EventArgs e)
        {
            if (this.intRowNo2 > -1)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateAmount2(intRowNo2);
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();
            }
        }

        private void ctlDataGrid1_Leave(object sender, System.EventArgs e)
        {
            if (intRowNo1 > -1)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateAmount(intRowNo1);
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();
                intRowNo1 = -1;
            }
        }

        private void ctlDataGrid3_Leave(object sender, System.EventArgs e)
        {
            if (intRowNo3 > -1)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthAddItemToCulateClass(this.intRowNo3, 3);
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();
                intRowNo3 = -1;
            }
        }

        private void ctlDataGrid4_Leave(object sender, System.EventArgs e)
        {
            if (intRowNo4 > -1)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthAddItemToCulateClass(this.intRowNo4, 4);
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();
                intRowNo4 = -1;
            }
        }

        private void ctlDataGrid5_Leave(object sender, System.EventArgs e)
        {
            if (intRowNo5 > -1)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthAddItemToCulateClass(this.intRowNo5, 5);
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();
                intRowNo5 = -1;
            }
        }

        private void ctlDataGrid6_Leave(object sender, System.EventArgs e)
        {
            if (intRowNo6 > -1)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthAddItemToCulateClass(this.intRowNo6, 6);
                ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();
                intRowNo6 = -1;
            }
        }
        #endregion

        private void m_PatInfo_SelectDoctor(object sender, System.EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthSetFocus();
        }

        private void mniDelAll_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("确认是否删除当前病历和处方？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //伪删除处方
                ((clsCtl_DoctorWorkstation)this.objController).m_mthDelRecipe("0");
                //伪删除病历
                ((clsCtl_DoctorWorkstation)this.objController).m_mthDelCashHistory("0");
            }
        }

        private void muiDelRec_Click(object sender, System.EventArgs e)
        {
            //伪删除处方
            if (MessageBox.Show("确认是否删除当前处方？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthDelRecipe("0");
            }
        }

        private void mniDelCash_Click(object sender, System.EventArgs e)
        {
            //伪删除病历
            if (MessageBox.Show("确认是否删除当前病历？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthDelCashHistory("0");
            }
        }

        private void m_dtgTake_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
        {
            if (e.m_intRowNumber >= 0)
            {
                if (m_dtgTake[e.m_intRowNumber, 7].ToString().Substring(0, 10) == System.DateTime.Now.Date.ToString("yyyy-MM-dd"))
                {
                    string CardID = m_dtgTake[e.m_intRowNumber, 2].ToString();
                    this.m_PatInfo.txtCardID.Text = CardID;
                    this.m_PatInfo.m_mthGetPatientInfoByCard(CardID);
                }
            }
        }

        private void muiInfo_Click(object sender, System.EventArgs e)
        {
            if (this.m_dtgTake.RowCount == 0)
            {
                return;
            }

            int currRow = this.m_dtgTake.CurrentCell.RowNumber;
            if (currRow < 0)
            {
                currRow = 0;
            }

            string strCardNO = this.m_dtgTake[currRow, 2].ToString();

            frmShowReports objReport = new frmShowReports(strCardNO);
            objReport.ShowDialog();
            objReport = null;
        }

        private void llblDate_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ((clsCtl_DoctorWorkstation)this.objController).m_mthSelectMode();
            this.Cursor = Cursors.Default;
        }

        private void btnSelect_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ((clsCtl_DoctorWorkstation)this.objController).m_mthSelectPat(1);
            this.Cursor = Cursors.Default;
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthGetAllergiclist();
        }

        private void piclis_Click(object sender, EventArgs e)
        {
            if (btSave.Enabled || btPutIn.Enabled)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthShowTestApply(-1);
            }
        }

        private void picris_Click(object sender, EventArgs e)
        {
            if (btSave.Enabled || btPutIn.Enabled)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthApplyCheck();
            }
        }

        private void picops_Click(object sender, EventArgs e)
        {
            if (btSave.Enabled || btPutIn.Enabled)
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthOPSApply(-1, "", "");
            }
        }

        private void ctlDataGrid5_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
        {
            if (this.ctlDataGrid5[e.m_intRowNumber, 7].ToString().Trim() == "")
            {
                return;
            }

            //if (this.ctlDataGrid5[e.m_intRowNumber, 20].ToString().Trim() != "")
            //{
            //    MessageBox.Show("此项目已开申请单!", "提示");
            //    return;
            //}

            //((clsCtl_DoctorWorkstation)this.objController).m_mthOPSApply(e.m_intRowNumber, this.ctlDataGrid5[e.m_intRowNumber, 7].ToString().Trim(), this.ctlDataGrid5[e.m_intRowNumber, 2].ToString().Trim());

            if (this.ctlDataGrid5[e.m_intRowNumber, 17].ToString().Trim() != "")
            {
                MessageBox.Show("此项目已开申请单!", "提示");
                return;
            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthApplyCheckByItem(e.m_intRowNumber, 5);
        }

        private void timerrec_Tick(object sender, EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthGetrecipeconfirmfalllist();
        }

        private void TB_Enter(object sender, EventArgs e)
        {
            this.cboquick.Hide();
            TextBox temp = sender as TextBox;
            try
            {
                if (temp.Text.Trim() != "")
                {
                    this.cboquick.Text = temp.Text;
                }
                this.cboquick.Bounds = temp.Bounds;
                this.cboquick.Show();
                this.cboquick.Focus();
                this.cboquick.Select();
                this.cboquick.BringToFront();
            }
            catch
            {
            }
        }

        private void cboquick_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ctlDataGridLis[this.ctlDataGridLis.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_quick] = this.cboquick.Text;
            if (this.cboquick.Text.Trim() == "是" /*&& this.ctlDataGridLis[this.ctlDataGridLis.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_lis_orderitem].ToString().Trim() != ""*/)
            {
                this.ctlDataGridLis.m_mthSetRowColor(this.ctlDataGridLis.CurrentCell.RowNumber, Color.FromArgb(255, 255, 255), Color.FromArgb(250, 89, 69));
                this.ctlDataGridLis[this.ctlDataGridLis.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_quickid] = "1";
            }
            else
            {
                this.ctlDataGridLis.m_mthSetRowColor(this.ctlDataGridLis.CurrentCell.RowNumber, nfc, nbc);
                this.ctlDataGridLis[this.ctlDataGridLis.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_quickid] = "0";
            }

            //this.ctlDataGrid3[this.ctlDataGrid3.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_quick] = this.cboquick.Text;
            //if (this.cboquick.Text.Trim() == "是" && this.ctlDataGrid3[this.ctlDataGrid3.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_ItemID].ToString().Trim() != "")
            //{
            //    this.ctlDataGrid3.m_mthSetRowColor(this.ctlDataGrid3.CurrentCell.RowNumber, Color.FromArgb(255, 255, 255), Color.FromArgb(250, 89, 69));
            //    this.ctlDataGrid3[this.ctlDataGrid3.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_quickid] = "1";
            //}
            //else
            //{
            //    this.ctlDataGrid3.m_mthSetRowColor(this.ctlDataGrid3.CurrentCell.RowNumber, nfc, nbc);
            //    this.ctlDataGrid3[this.ctlDataGrid3.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_quickid] = "0";
            //}
        }

        private void cboquick_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ctlDataGridLis[this.ctlDataGridLis.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_quick] = this.cboquick.Text;
                if (this.cboquick.Text.Trim() == "是" /*&& this.ctlDataGridLis[this.ctlDataGridLis.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_lis_orderitem].ToString().Trim() != ""*/)
                {
                    this.ctlDataGridLis.m_mthSetRowColor(this.ctlDataGridLis.CurrentCell.RowNumber, Color.FromArgb(255, 255, 255), Color.FromArgb(250, 89, 69));
                    this.ctlDataGridLis[this.ctlDataGridLis.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_quickid] = "1";
                }
                this.ctlDataGridLis.CurrentCell = new DataGridCell(this.ctlDataGridLis.CurrentCell.RowNumber + 1, 0);
            }

            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.ctlDataGrid3[this.ctlDataGrid3.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_quick] = this.cboquick.Text;
            //    if (this.cboquick.Text.Trim() == "是" && this.ctlDataGrid3[this.ctlDataGrid3.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_ItemID].ToString().Trim() != "")
            //    {
            //        this.ctlDataGrid3.m_mthSetRowColor(this.ctlDataGrid3.CurrentCell.RowNumber, Color.FromArgb(255, 255, 255), Color.FromArgb(250, 89, 69));
            //        this.ctlDataGrid3[this.ctlDataGrid3.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_quickid] = "1";
            //    }
            //    this.ctlDataGrid3.CurrentCell = new DataGridCell(this.ctlDataGrid3.CurrentCell.RowNumber + 1, 0);
            //}
        }

        private void cboDeptmed1_Enter(object sender, EventArgs e)
        {
            this.cboDeptmed1.Hide();
            TextBox temp = sender as TextBox;
            try
            {
                if (temp.Text.Trim() != "")
                {
                    this.cboDeptmed1.Text = temp.Text;
                }
                this.cboDeptmed1.Bounds = temp.Bounds;
                this.cboDeptmed1.Show();
                this.cboDeptmed1.Focus();
                this.cboDeptmed1.Select();
                this.cboDeptmed1.BringToFront();
            }
            catch
            {
            }
        }

        private void cboDeptmed2_Enter(object sender, EventArgs e)
        {
            this.cboDeptmed2.Hide();
            TextBox temp = sender as TextBox;
            try
            {
                if (temp.Text.Trim() != "")
                {
                    this.cboDeptmed2.Text = temp.Text;
                }
                this.cboDeptmed2.Bounds = temp.Bounds;
                this.cboDeptmed2.Show();
                this.cboDeptmed2.Focus();
                this.cboDeptmed2.Select();
                this.cboDeptmed2.BringToFront();
            }
            catch
            {
            }
        }

        private void cboDeptmed6_Enter(object sender, EventArgs e)
        {
            this.cboDeptmed6.Hide();
            TextBox temp = sender as TextBox;
            try
            {
                if (temp.Text.Trim() != "")
                {
                    this.cboDeptmed6.Text = temp.Text;
                }
                this.cboDeptmed6.Bounds = temp.Bounds;
                this.cboDeptmed6.Show();
                this.cboDeptmed6.Focus();
                this.cboDeptmed6.Select();
                this.cboDeptmed6.BringToFront();
            }
            catch
            {
            }
        }

        private void piclis_MouseLeave(object sender, EventArgs e)
        {
            this.piclis.Location = new Point(this.piclis.Location.X + 1, this.piclis.Location.Y + 1);
        }

        private void piclis_MouseEnter(object sender, EventArgs e)
        {
            this.piclis.Location = new Point(this.piclis.Location.X - 1, this.piclis.Location.Y - 1);
        }

        private void picris_MouseEnter(object sender, EventArgs e)
        {
            this.picris.Location = new Point(this.picris.Location.X - 1, this.picris.Location.Y - 1);
        }

        private void picris_MouseLeave(object sender, EventArgs e)
        {
            this.picris.Location = new Point(this.picris.Location.X + 1, this.picris.Location.Y + 1);
        }

        private void picops_MouseEnter(object sender, EventArgs e)
        {
            this.picops.Location = new Point(this.picops.Location.X - 1, this.picops.Location.Y - 1);
        }

        private void picops_MouseLeave(object sender, EventArgs e)
        {
            this.picops.Location = new Point(this.picops.Location.X + 1, this.picops.Location.Y + 1);
        }

        private void btnRecalc_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ((clsCtl_DoctorWorkstation)this.objController).m_mthRecalculate();
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 科备药前景色
        /// </summary>
        private Color dfc = Color.FromArgb(255, 255, 255);
        /// <summary>
        /// 科备药背景色
        /// </summary>
        private Color dbc = Color.FromArgb(99, 143, 97);
        /// <summary>
        /// 正常前景色
        /// </summary>
        private Color nfc = Color.FromArgb(0, 0, 0);
        /// <summary>
        /// 正常背景色
        /// </summary>
        private Color nbc = Color.FromArgb(255, 255, 255);
        /// <summary>
        /// 编辑前景色
        /// </summary>
        private Color efc = Color.FromArgb(222, 239, 165);

        private void cboDeptmed1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.ctlDataGrid1[this.ctlDataGrid1.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).c_ItemID].ToString().Trim() != "")
            {
                string syzId = "0";
                string syzName = this.cboDeptmed1.Text.Trim();
                if (syzName == "符合")
                    syzId = "2";
                else if (syzName == "不符合")
                    syzId = "3";
                this.ctlDataGrid1[this.ctlDataGrid1.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).c_Deptmed] = syzName;
                this.ctlDataGrid1[this.ctlDataGrid1.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).c_DeptmedID] = syzId;

                //if (this.cboDeptmed1.Text.Trim() == "是" && this.ctlDataGrid1[this.ctlDataGrid1.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).c_DeptmedID].ToString().Trim() == "*")
                //{
                //    MessageBox.Show("该药品不允许科室自备。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //}
                //else
                //{
                //    this.ctlDataGrid1[this.ctlDataGrid1.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).c_Deptmed] = this.cboDeptmed1.Text;
                //    if (this.cboDeptmed1.Text.Trim() == "是")
                //    {
                //        this.ctlDataGrid1.m_mthSetRowColor(this.ctlDataGrid1.CurrentCell.RowNumber, dfc, dbc);
                //        this.ctlDataGrid1[this.ctlDataGrid1.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).c_DeptmedID] = "1";
                //    }
                //}
                this.ctlDataGrid1.CurrentCell = new DataGridCell(this.ctlDataGrid1.CurrentCell.RowNumber + 1, 0);
            }
        }

        private void cboDeptmed2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.ctlDataGrid2[this.ctlDataGrid2.CurrentCell.RowNumber, 8].ToString().Trim() != "")
            {
                if (this.cboDeptmed2.Text.Trim() == "是" && this.ctlDataGrid2[this.ctlDataGrid2.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).cm_DeptmedID].ToString().Trim() == "*")
                {
                    MessageBox.Show("该药品不允许科室自备。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    this.ctlDataGrid2[this.ctlDataGrid2.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).cm_Deptmed] = this.cboDeptmed2.Text;
                    if (this.cboDeptmed2.Text.Trim() == "是")
                    {
                        this.ctlDataGrid2.m_mthSetRowColor(this.ctlDataGrid2.CurrentCell.RowNumber, dfc, dbc);
                        this.ctlDataGrid2[this.ctlDataGrid2.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).cm_DeptmedID] = "1";
                    }
                }
                this.ctlDataGrid2.CurrentCell = new DataGridCell(this.ctlDataGrid2.CurrentCell.RowNumber + 1, 0);
            }
        }

        private void cboDeptmed6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.ctlDataGrid6[this.ctlDataGrid6.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_ItemID].ToString().Trim() != "")
            {
                if (this.cboDeptmed6.Text.Trim() == "是" && this.ctlDataGrid6[this.ctlDataGrid6.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_DeptmedID].ToString().Trim() == "*")
                {
                    MessageBox.Show("该项目不允许科室自备。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    this.ctlDataGrid6[this.ctlDataGrid6.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_Deptmed] = this.cboDeptmed6.Text;
                    if (this.cboDeptmed6.Text.Trim() == "是")
                    {
                        this.ctlDataGrid6.m_mthSetRowColor(this.ctlDataGrid6.CurrentCell.RowNumber, dfc, dbc);
                        this.ctlDataGrid6[this.ctlDataGrid6.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_DeptmedID] = "1";
                    }
                }
                this.ctlDataGrid6.CurrentCell = new DataGridCell(this.ctlDataGrid6.CurrentCell.RowNumber + 1, 0);
            }
        }

        private void cboDeptmed1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ctlDataGrid1[this.ctlDataGrid1.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).c_ItemID].ToString().Trim() != "")
            {
                string syzId = "0";
                string syzName = this.cboDeptmed1.Text.Trim();
                if (syzName == "符合")
                    syzId = "2";
                else if (syzName == "不符合")
                    syzId = "3";
                this.ctlDataGrid1[this.ctlDataGrid1.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).c_Deptmed] = syzName;
                this.ctlDataGrid1[this.ctlDataGrid1.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).c_DeptmedID] = syzId;


                //if (this.cboDeptmed1.Text.Trim() == "是" && this.ctlDataGrid1[this.ctlDataGrid1.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).c_DeptmedID].ToString().Trim() == "*")
                //{
                //    MessageBox.Show("该药品不允许科室自备。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    return;
                //}

                //this.ctlDataGrid1[this.ctlDataGrid1.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).c_Deptmed] = this.cboDeptmed1.Text;
                //if (this.cboDeptmed1.Text.Trim() == "是")
                //{
                //    this.ctlDataGrid1.m_mthSetRowColor(this.ctlDataGrid1.CurrentCell.RowNumber, dfc, dbc);
                //    this.ctlDataGrid1[this.ctlDataGrid1.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).c_DeptmedID] = "1";
                //}
                //else
                //{
                //    if (this.ctlDataGrid1[this.ctlDataGrid1.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).c_DeptmedID].ToString().Trim() != "*")
                //    {
                //        this.ctlDataGrid1.m_mthSetRowColor(this.ctlDataGrid1.CurrentCell.RowNumber, nfc, nbc);
                //        this.ctlDataGrid1[this.ctlDataGrid1.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).c_DeptmedID] = "0";
                //    }
                //}
            }
        }

        private void cboDeptmed2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ctlDataGrid2[this.ctlDataGrid2.CurrentCell.RowNumber, 8].ToString().Trim() != "")
            {
                if (this.cboDeptmed2.Text.Trim() == "是" && this.ctlDataGrid2[this.ctlDataGrid2.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).cm_DeptmedID].ToString().Trim() == "*")
                {
                    MessageBox.Show("该药品不允许科室自备。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                this.ctlDataGrid2[this.ctlDataGrid2.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).cm_Deptmed] = this.cboDeptmed2.Text;
                if (this.cboDeptmed2.Text.Trim() == "是")
                {
                    this.ctlDataGrid2.m_mthSetRowColor(this.ctlDataGrid2.CurrentCell.RowNumber, dfc, dbc);
                    this.ctlDataGrid2[this.ctlDataGrid2.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).cm_DeptmedID] = "1";
                }
                else
                {
                    if (this.ctlDataGrid2[this.ctlDataGrid2.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).cm_DeptmedID].ToString().Trim() != "*")
                    {
                        this.ctlDataGrid2.m_mthSetRowColor(this.ctlDataGrid2.CurrentCell.RowNumber, nfc, nbc);
                        this.ctlDataGrid2[this.ctlDataGrid2.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).cm_DeptmedID] = "0";
                    }
                }
            }
        }

        private void cboDeptmed6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ctlDataGrid6[this.ctlDataGrid6.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_ItemID].ToString().Trim() != "")
            {
                if (this.cboDeptmed6.Text.Trim() == "是" && this.ctlDataGrid6[this.ctlDataGrid6.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_DeptmedID].ToString().Trim() == "*")
                {
                    MessageBox.Show("该项目不允许科室自备。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                this.ctlDataGrid6[this.ctlDataGrid6.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_Deptmed] = this.cboDeptmed6.Text;
                if (this.cboDeptmed6.Text.Trim() == "是")
                {
                    this.ctlDataGrid6.m_mthSetRowColor(this.ctlDataGrid6.CurrentCell.RowNumber, dfc, dbc);
                    this.ctlDataGrid6[this.ctlDataGrid6.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_DeptmedID] = "1";
                }
                else
                {
                    if (this.ctlDataGrid6[this.ctlDataGrid6.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_DeptmedID].ToString().Trim() != "*")
                    {
                        this.ctlDataGrid6.m_mthSetRowColor(this.ctlDataGrid6.CurrentCell.RowNumber, nfc, nbc);
                        this.ctlDataGrid6[this.ctlDataGrid6.CurrentCell.RowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_DeptmedID] = "0";
                    }
                }
            }
        }

        private void timerRecpur_Tick(object sender, EventArgs e)
        {
            if (noopen)
            {
                foreach (Form f in this.MdiParent.MdiChildren)
                {
                    f.WindowState = FormWindowState.Maximized;
                }
                this.MdiParent.Refresh();
                this.Close();
            }
        }

        private void ctlDataGridLis_Leave(object sender, EventArgs e)
        {
            if (intRowNoLis > -1)
            {
                intRowNoLis = -1;
            }
        }

        private void ctlDataGridLis_m_evtCurrentCellChanged(object sender, EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthSetColNoLis();

            //设置前背景色
            int row = ctlDataGridLis.CurrentCell.RowNumber;
            int col = ctlDataGridLis.CurrentCell.ColumnNumber;
            if (col != 0)
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            }
            if ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridLis.Columns[col] != null)
            {
                ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridLis.Columns[col]).DataGridTextBoxColumn.TextBox.BackColor = efc;	//Color.SteelBlue;			
            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();

            if (this.plLis.Width > 0)
            {
                this.plLis.Width = 0;
            }
        }

        private void ctlDataGridLis_m_evtDataGridKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                int intRow = ctlDataGridLis.CurrentCell.RowNumber;

                string strReItemID = this.ctlDataGridLis[intRow, ((clsCtl_DoctorWorkstation)this.objController).t_resubitem].ToString();

                if (strReItemID.StartsWith("[PK]"))
                {
                    decimal d1, d2;
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthGetChargeItemByOrderItem(strReItemID.Replace("[PK]", ""), null, null, -1, null, "lis", out d1, out d2, 1, 0);
                    ((clsCtl_DoctorWorkstation)this.objController).hasOrderID.Remove("lis->" + strReItemID.Replace("[PK]", ""));
                }

                this.ctlDataGridLis.m_mthDeleteRow(intRow);

                ctlDataGridLis.CurrentCell = new DataGridCell(this.ctlDataGridLis.CurrentCell.RowNumber, 0);
                SendKeys.SendWait("{Right}");
                SendKeys.SendWait("{Left}");
                SendKeys.SendWait("{Left}");
                return;
            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthFormKeyDown(e);
        }

        private void ctlDataGridLis_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
        {
            string m_strText = e.m_strText.Replace("'", "’");
            if (e.KeyCode == Keys.Enter)//查询
            {
                if (e.m_intColNumber == 0)
                {
                    if (m_strText.Trim() == "")
                    {
                        return;
                    }

                    if (m_strText.Trim().Substring(0, 1) == @"\")
                    {
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthGetAccordRecipe(m_strText.Substring(1, m_strText.Length - 1));
                    }
                    else
                    {
                        intRowNoLis = e.m_intRowNumber;
                        if (this.m_cmbFind.SelectedIndex == 2)
                        {
                            m_strText = m_strText.ToUpper();
                        }
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthFindOrderLisByID(m_strText, e.m_intRowNumber);
                    }
                }
            }
            if (e.m_intColNumber == 1)//输入数量
            {
                if (m_strText.IndexOf(".") >= 0)
                {
                    MessageBox.Show("对不起，数量不能是小数。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                intRowNoLis = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    if (m_strText.Trim() == "")
                    {
                        this.ctlDataGridLis[intRowNoLis, ((clsCtl_DoctorWorkstation)this.objController).t_SumMoney] = "0";
                    }
                    else
                    {
                        this.ctlDataGridLis[intRowNoLis, ((clsCtl_DoctorWorkstation)this.objController).t_SumMoney] = Convert.ToString(Convert.ToDecimal(this.ctlDataGridLis[intRowNoLis, ((clsCtl_DoctorWorkstation)this.objController).t_Discount].ToString()) * Convert.ToDecimal(m_strText));
                    }
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthCheckMainItemNum(this.ctlDataGridLis[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_resubitem].ToString(), this.ctlDataGridLis[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_MainItemNum].ToString(), m_strText, "lis");
                    SendKeys.SendWait("{Tab}");
                }
            }
            if (e.m_intColNumber == 4)//查询检验类型
            {
                intRowNoLis = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    if (((clsCtl_DoctorWorkstation)this.objController).m_lngGetLisSampletyType(m_strText, e.m_intRowNumber) > 0)
                    {
                        this.listView5.Location = e.m_ptPositionInDataGrid;
                        this.listView5.Top += e.m_szTextBoxSize.Height;
                        this.listView5.Show();
                        this.listView5.BringToFront();
                        this.listView5.Items[0].Selected = true;
                        this.listView5.Select();
                        this.listView5.Focus();
                    }
                }
            }
            if (e.m_intColNumber == ((clsCtl_DoctorWorkstation)this.objController).t_quick)
            {
                SendKeys.SendWait("{Tab}");
            }
        }

        private void ctlDataGridLis_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
        {
            if (ctlDataGridLis[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_ItemID] == null || ctlDataGridLis[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_ItemID].ToString().Trim() == "")
            {
                return;
            }

            string id = "lis->" + e.m_intRowNumber.ToString() + "->" + ctlDataGridLis[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_ItemID].ToString();

            clsOrder_VO Order_VO = ((clsCtl_DoctorWorkstation)this.objController).hasOrderID[id] as clsOrder_VO;

            this.lvLis.Items.Clear();

            for (int i = 0; i < Order_VO.EntryDT.Rows.Count; i++)
            {
                DataRow dr = Order_VO.EntryDT.Rows[i];

                ListViewItem Item = new ListViewItem();

                this.lvLis.Items.Add(Item);

                decimal d = ((clsCtl_DoctorWorkstation)this.objController).m_mthConvertObjToDecimal(dr["itemprice_mny"]) * ((clsCtl_DoctorWorkstation)this.objController).m_mthConvertObjToDecimal(dr["totalqty_dec"]);

                Item = new ListViewItem(Convert.ToString(i + 1));
                Item.SubItems.Add(dr["itemname"].ToString().Trim());
                Item.SubItems.Add(dr["itemprice_mny"].ToString().Trim());
                Item.SubItems.Add(dr["totalqty_dec"].ToString().Trim());
                Item.SubItems.Add(d.ToString());
                Item.SubItems.Add(dr["precent_dec"].ToString().Trim() + "%");
                d = d * ((clsCtl_DoctorWorkstation)this.objController).m_mthConvertObjToDecimal(dr["precent_dec"]) / 100;
                Item.SubItems.Add(d.ToString("0.00"));
                this.lvLis.Items.Add(Item);
            }

            this.plLis.BringToFront();
            this.plLis.Width = 460;
        }

        private void ctlDataGridTest_Leave(object sender, EventArgs e)
        {
            if (intRowNoTest > -1)
            {
                intRowNoTest = -1;
            }
        }

        private void ctlDataGridTest_m_evtCurrentCellChanged(object sender, EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthSetColNoTest();

            //设置前背景色
            int row = ctlDataGridTest.CurrentCell.RowNumber;
            int col = ctlDataGridTest.CurrentCell.ColumnNumber;
            if (col != 0)
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            }
            if ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridTest.Columns[col] != null)
            {
                ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridTest.Columns[col]).DataGridTextBoxColumn.TextBox.BackColor = efc; //Color.SteelBlue;			
            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();

            if (this.plTest.Width > 0)
            {
                this.plTest.Width = 0;
            }
        }

        private void ctlDataGridTest_m_evtDataGridKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                int intRow = ctlDataGridTest.CurrentCell.RowNumber;

                string strReItemID = this.ctlDataGridTest[intRow, ((clsCtl_DoctorWorkstation)this.objController).t_resubitem].ToString();

                if (strReItemID.StartsWith("[PK]"))
                {
                    decimal d1, d2;
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthGetChargeItemByOrderItem(strReItemID.Replace("[PK]", ""), null, null, -1, null, "test", out d1, out d2, 1, 0);
                    ((clsCtl_DoctorWorkstation)this.objController).hasOrderID.Remove("test->" + strReItemID.Replace("[PK]", ""));
                }

                ((clsCtl_DoctorWorkstation)this.objController).m_mthDelApplyBill(intRow);
                this.ctlDataGridTest.m_mthDeleteRow(intRow);
                ctlDataGridTest.CurrentCell = new DataGridCell(this.ctlDataGridTest.CurrentCell.RowNumber, 0);
                SendKeys.SendWait("{Right}");
                SendKeys.SendWait("{Left}");
                SendKeys.SendWait("{Left}");
                return;

            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthFormKeyDown(e);
        }

        private void ctlDataGridTest_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
        {
            string m_strText = e.m_strText.Replace("'", "’");
            if (e.KeyCode == Keys.Enter)//查询
            {

                if (e.m_intColNumber == 0)
                {
                    if (m_strText.Trim() == "")
                    {
                        return;
                    }

                    if (m_strText.Trim().Substring(0, 1) == @"\")
                    {
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthGetAccordRecipe(m_strText.Substring(1, m_strText.Length - 1));
                    }
                    else
                    {
                        intRowNoTest = e.m_intRowNumber;

                        if (this.m_cmbFind.SelectedIndex == 2)
                        {
                            m_strText = m_strText.ToUpper();
                        }
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthFindExamineChargeByOrderID(m_strText, e.m_intRowNumber);
                    }
                }
            }
            if (e.m_intColNumber == 1)//输入数量
            {
                intRowNoTest = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    if (m_strText.Trim() == "")
                    {
                        this.ctlDataGridTest[intRowNoTest, ((clsCtl_DoctorWorkstation)this.objController).t_SumMoney] = "0";
                    }
                    else
                    {
                        this.ctlDataGridTest[intRowNoTest, ((clsCtl_DoctorWorkstation)this.objController).t_SumMoney] = Convert.ToString(Convert.ToDecimal(this.ctlDataGridTest[intRowNoTest, ((clsCtl_DoctorWorkstation)this.objController).t_Discount].ToString()) * Convert.ToDecimal(m_strText));
                    }
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthCheckMainItemNum(this.ctlDataGridTest[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_resubitem].ToString(), this.ctlDataGridTest[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_MainItemNum].ToString(), m_strText, "test");
                    SendKeys.SendWait("{Tab}");
                }
            }
            if (e.m_intColNumber == 4)//输入检查部位
            {
                intRowNoTest = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    if (((clsCtl_DoctorWorkstation)this.objController).m_mthLoadCheckPart(m_strText, e.m_intRowNumber) > 0)
                    {
                        this.listView5.Location = e.m_ptPositionInDataGrid;
                        this.listView5.Top += e.m_szTextBoxSize.Height;
                        this.listView5.Show();
                        this.listView5.BringToFront();
                        this.listView5.Items[0].Selected = true;
                        this.listView5.Select();
                        this.listView5.Focus();
                    }
                }
            }
        }

        private void ctlDataGridTest_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
        {
            if (ctlDataGridTest[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_ItemID] == null || ctlDataGridTest[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_ItemID].ToString().Trim() == "")
            {
                return;
            }

            string id = "test->" + e.m_intRowNumber.ToString() + "->" + ctlDataGridTest[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).t_ItemID].ToString();

            clsOrder_VO Order_VO = ((clsCtl_DoctorWorkstation)this.objController).hasOrderID[id] as clsOrder_VO;

            this.lvTest.Items.Clear();

            for (int i = 0; i < Order_VO.EntryDT.Rows.Count; i++)
            {
                DataRow dr = Order_VO.EntryDT.Rows[i];

                ListViewItem Item = new ListViewItem();

                this.lvTest.Items.Add(Item);

                decimal d = ((clsCtl_DoctorWorkstation)this.objController).m_mthConvertObjToDecimal(dr["itemprice_mny"]) * ((clsCtl_DoctorWorkstation)this.objController).m_mthConvertObjToDecimal(dr["totalqty_dec"]);

                Item = new ListViewItem(Convert.ToString(i + 1));
                Item.SubItems.Add(dr["itemname"].ToString().Trim());
                Item.SubItems.Add(dr["itemprice_mny"].ToString().Trim());
                Item.SubItems.Add(dr["totalqty_dec"].ToString().Trim());
                Item.SubItems.Add(d.ToString());
                Item.SubItems.Add(dr["precent_dec"].ToString().Trim() + "%");
                d = d * ((clsCtl_DoctorWorkstation)this.objController).m_mthConvertObjToDecimal(dr["precent_dec"]) / 100;
                Item.SubItems.Add(d.ToString("0.00"));

                this.lvTest.Items.Add(Item);
            }

            this.plTest.BringToFront();
            this.plTest.Width = 460;
        }

        private void ctlDataGridOps_Leave(object sender, EventArgs e)
        {
            if (intRowNoOps > -1)
            {
                intRowNoOps = -1;
            }
        }

        private void ctlDataGridOps_m_evtDataGridKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                this.DeleteOpsRow();
            }
            else
            {
                ((clsCtl_DoctorWorkstation)this.objController).m_mthFormKeyDown(e);
            }
        }

        #region DeleteOpsRow
        /// <summary>
        /// DeleteOpsRow
        /// </summary>
        internal void DeleteOpsRow()
        {
            int intRow = ctlDataGridOps.CurrentCell.RowNumber;
            string strReItemID = this.ctlDataGridOps[intRow, ((clsCtl_DoctorWorkstation)this.objController).o_resubitem].ToString();
            if (strReItemID.StartsWith("[PK]"))
            {
                decimal d1, d2;
                ((clsCtl_DoctorWorkstation)this.objController).m_mthGetChargeItemByOrderItem(strReItemID.Replace("[PK]", ""), null, null, -1, null, "ops", out d1, out d2, 1, 0);
                ((clsCtl_DoctorWorkstation)this.objController).hasOrderID.Remove("ops->" + strReItemID.Replace("[PK]", ""));
            }
            this.ctlDataGridOps.m_mthDeleteRow(intRow);
            ctlDataGridOps.CurrentCell = new DataGridCell(this.ctlDataGridOps.CurrentCell.RowNumber, 0);
            //因为DataGrid有个Bug所以发送下面的模拟键
            SendKeys.SendWait("{Right}");
            SendKeys.SendWait("{Left}");
            SendKeys.SendWait("{Left}");
        }
        #endregion

        private void ctlDataGridOps_m_evtCurrentCellChanged(object sender, EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).m_mthSetColNoOps();

            //设置前背景色
            int row = ctlDataGridOps.CurrentCell.RowNumber;
            int col = ctlDataGridOps.CurrentCell.ColumnNumber;

            if (col != 0)
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            }
            if ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridOps.Columns[col] != null)
            {
                ((com.digitalwave.controls.datagrid.clsColumnInfo)ctlDataGridOps.Columns[col]).DataGridTextBoxColumn.TextBox.BackColor = efc;	//Color.SteelBlue;			
            }
            ((clsCtl_DoctorWorkstation)this.objController).m_mthCalculateTotalMoney();

            if (this.plOps.Width > 0)
            {
                this.plOps.Width = 0;
            }
        }

        private void ctlDataGridOps_m_evtDataGridTextBoxKeyDown(object sender, com.digitalwave.controls.datagrid.clsDGTextKeyEventArgs e)
        {
            string m_strText = e.m_strText.Replace("'", "’");
            if (e.KeyCode == Keys.Enter)//查询
            {
                if (e.m_intColNumber == 0)
                {
                    if (m_strText.Trim() == "")
                    {
                        return;
                    }

                    if (m_strText.Trim().Substring(0, 1) == @"\")
                    {
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthGetAccordRecipe(m_strText.Substring(1, m_strText.Length - 1));
                    }
                    else
                    {
                        intRowNoOps = e.m_intRowNumber;

                        if (this.m_cmbFind.SelectedIndex == 2)
                        {
                            m_strText = m_strText.ToUpper();
                        }
                        ((clsCtl_DoctorWorkstation)this.objController).m_mthFindOPSChargeByOrderID(m_strText, e.m_intRowNumber);
                    }
                }
            }
            if (e.m_intColNumber == 1)//输入数量
            {
                intRowNoOps = e.m_intRowNumber;
                if (e.KeyCode == Keys.Enter)
                {
                    if (m_strText.Trim() == "")
                    {
                        this.ctlDataGridOps[intRowNoOps, ((clsCtl_DoctorWorkstation)this.objController).o_SumMoney] = "0";
                    }
                    else
                    {
                        this.ctlDataGridOps[intRowNoOps, ((clsCtl_DoctorWorkstation)this.objController).o_SumMoney] = Convert.ToString(Convert.ToDecimal(ctlDataGridOps[intRowNoOps, ((clsCtl_DoctorWorkstation)this.objController).o_Discount].ToString()) * Convert.ToDecimal(m_strText));
                    }
                    ((clsCtl_DoctorWorkstation)this.objController).m_mthCheckMainItemNum(ctlDataGridOps[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_resubitem].ToString(), ctlDataGridOps[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_MainItemNum].ToString(), m_strText, "ops", "1");
                    SendKeys.SendWait("{Tab}");
                }
            }
        }

        private void ctlDataGridOps_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
        {
            if (ctlDataGridOps[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_ItemID] == null || ctlDataGridOps[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_ItemID].ToString().Trim() == "")
            {
                return;
            }

            string id = "ops->" + e.m_intRowNumber.ToString() + "->" + ctlDataGridOps[e.m_intRowNumber, ((clsCtl_DoctorWorkstation)this.objController).o_ItemID].ToString();

            clsOrder_VO Order_VO = ((clsCtl_DoctorWorkstation)this.objController).hasOrderID[id] as clsOrder_VO;

            this.lvOps.Items.Clear();

            for (int i = 0; i < Order_VO.EntryDT.Rows.Count; i++)
            {
                DataRow dr = Order_VO.EntryDT.Rows[i];

                ListViewItem Item = new ListViewItem();

                this.lvOps.Items.Add(Item);

                decimal d = ((clsCtl_DoctorWorkstation)this.objController).m_mthConvertObjToDecimal(dr["itemprice_mny"]) * ((clsCtl_DoctorWorkstation)this.objController).m_mthConvertObjToDecimal(dr["totalqty_dec"]);

                Item = new ListViewItem(Convert.ToString(i + 1));
                Item.SubItems.Add(dr["itemname"].ToString().Trim());
                Item.SubItems.Add(dr["itemprice_mny"].ToString().Trim());
                Item.SubItems.Add(dr["totalqty_dec"].ToString().Trim());
                Item.SubItems.Add(d.ToString());
                Item.SubItems.Add(dr["precent_dec"].ToString().Trim() + "%");
                d = d * ((clsCtl_DoctorWorkstation)this.objController).m_mthConvertObjToDecimal(dr["precent_dec"]) / 100;
                Item.SubItems.Add(d.ToString("0.00"));

                this.lvOps.Items.Add(Item);
            }

            this.plOps.BringToFront();
            this.plOps.Width = 460;
        }

        private void btnPrefer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("请确认该病人：是否要进行【先诊后结】的结算？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.btnPrefer.Enabled = false;
                ((clsCtl_DoctorWorkstation)this.objController).m_mthPayBillTrade();
            }
        }

        private void tsmiDrugInfo_Click(object sender, EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).GetDrugInfo();
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            // 2019-10-21 测试用
            //com.digitalwave.iCare.Template.Client.clsTemplateClient m_objTemplate = new com.digitalwave.iCare.Template.Client.clsTemplateClient(this, this.LoginInfo.m_strEmpID, this.LoginInfo.m_strDepartmentID);
            //m_objTemplate.m_mthCreateTemplate();
            //return;

            ((clsCtl_DoctorWorkstation)this.objController).RegBooking();
        }

        private void btnWAC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_PatInfo.PatientID))
            {
                MessageBox.Show("请刷卡（选择病人）", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            frmWAC frmwac = new frmWAC(this.m_PatInfo.PatientID, this.m_PatInfo.PatientName);
            frmwac.ShowDialog();
        }

        private void cboProxyBoilMed_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).AddProxyBoilMedFee();
        }

        private void btnNotice_Click(object sender, EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).InNotice();
        }

        private void cmbCooking_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string usageDesc = this.cmbCooking.Text.Trim();
            //if (usageDesc.IndexOf("代煎代送") >= 0)
            //{
            //    this.cboProxyBoilMed.SelectedIndex = 1;
            //}
            //else if (usageDesc.IndexOf("中药代送") >= 0)
            //{
            //    this.cboProxyBoilMed.SelectedIndex = 2;
            //}
            //else
            //{
            //    this.cboProxyBoilMed.SelectedIndex = 0;
            //}
        }

        private void btnApp_Click(object sender, EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).EApp();
        }

        private void lblESBCardVerify_Click(object sender, EventArgs e)
        {
            ((clsCtl_DoctorWorkstation)this.objController).ESBCardVerify();
        }

        private void lblESBCardVerify_MouseEnter(object sender, EventArgs e)
        {
            this.lblESBCardVerify.ForeColor = Color.Blue;
        }

        private void lblESBCardVerify_MouseLeave(object sender, EventArgs e)
        {
            this.lblESBCardVerify.ForeColor = Color.Black;
        }
    }
}