//#define FunctionPrivilege
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;
using weCare.Core.Entity;
using iCare.iCareBaseForm;
using com.digitalwave.Utility.SQLConvert;
using com.digitalwave.emr.BEDExplorer;
using com.digitalwave.emr.DigitalSign;//����ǩ��
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security.Cryptography;
using com.digitalwave.Emr.Signature_gui;
using SO = com.digitalwave.Emr.StaticObject;
using com.digitalwave.iCare.Template.Client;
using com.digitalwave.emr.AssistModule;
using com.digitalwave.Emr.StaticObject;

namespace iCare
{
    /// <summary>
    /// Summary description for frmHRPBaseForm.
    /// </summary>
    public class frmHRPBaseForm : frmBaseForm, com.digitalwave.emr.AssistModule.infInactiveRecord
    {
        //		protected com.digitalwave.Utility.clsLogText m_objLog = new com.digitalwave.Utility.clsLogText();
        protected System.Windows.Forms.Label lblSex;
        protected System.Windows.Forms.Label lblAge;
        protected System.Windows.Forms.Label lblBedNoTitle;
        protected System.Windows.Forms.Label lblInHospitalNoTitle;
        protected System.Windows.Forms.Label lblNameTitle;
        protected System.Windows.Forms.Label lblSexTitle;
        protected System.Windows.Forms.Label lblAgeTitle;
        protected System.Windows.Forms.Label lblAreaTitle;
        protected System.Windows.Forms.ListView m_lsvInPatientID;
        protected com.digitalwave.Utility.Controls.ctlBorderTextBox txtInPatientID;
        private System.Windows.Forms.ToolTip m_ttpTextInfo;
        protected com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtPatientName;
        protected com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtBedNO;
        protected com.digitalwave.Utility.Controls.ctlComboBox m_cboArea;
        protected System.Windows.Forms.ListView m_lsvPatientName;
        protected System.Windows.Forms.ListView m_lsvBedNO;
        private System.Windows.Forms.ColumnHeader clmInPatientID_BaseForm;
        private System.Windows.Forms.ColumnHeader clmPatientName_BaseForm;
        private System.Windows.Forms.ColumnHeader clmBedNO_BaseForm;
        protected ctlComboBox m_cboDept;
        protected System.Windows.Forms.Label lblDept;
        protected PinkieControls.ButtonXP m_cmdNewTemplate;
        protected System.Windows.Forms.Button m_cmdNext;
        protected System.Windows.Forms.Button m_cmdPre;
        private System.Windows.Forms.Panel m_pnlFocus;
        protected System.Windows.Forms.Label m_lblForTitle;
        private System.ComponentModel.IContainer components;
        protected string m_strCanModifyTime = "";

        protected bool m_blnDeptOrAreaIsClear = true;

        private bool m_blnSetGetPatientByDept = true;
        //��סԺ�Ż�ȡ����
        protected bool m_BlnSetGetPatientByDept
        {
            get { return m_blnSetGetPatientByDept; }
            set { m_blnSetGetPatientByDept = value; }
        }

        /// <summary>
        /// ������СԪ�ؼ�ֵ
        /// </summary>
        public ArrayList m_arlMinElementColValue;
        /// <summary>
        /// ��¼��Ψһ��ʶ
        /// </summary>
        public virtual string m_StrRecordID
        {
            get { return ""; }
        }
        protected string m_strPatientID = "";
        protected System.Windows.Forms.CheckBox chkModifyWithoutMatk;

        public string m_StrPatientID
        {
            get { return m_strPatientID; }
            set { m_strPatientID = value; }
        }
        /// <summary>
        /// �س���ת����
        /// </summary>
        protected clsJumpControl m_objJump;
        /// <summary>
        /// �Ƿ�ֻ��
        /// </summary>
        protected bool m_blnReadOnly = true;
        /// <summary>
        /// ʣ��Ĺ鵵ʱ��
        /// </summary>
        protected string m_strTimeRemain = "";
        protected ToolTip m_tipMain;
        protected DateTime m_dtmCreatedDate;
        public DateTime m_DtmCreatedDate
        {
            get { return m_dtmCreatedDate; }
        }
        /// <summary>
        /// ������
        /// 1ҽ������վ 2��ʿ����վ
        /// ��Ҫ���ϼ���֤ʱ��ָ�������ͷ����г��������͵��ϼ��쵼 ����������λ��߻�ʿ��
        /// </summary>
        protected int intFormType;
        protected ExternalControlsLib.XPButton m_cmdModifyPatientInfo;
        /// <summary>
        /// �Ƿ��ֶ�ȡ���鵵�Ĳ��������ǣ����軤ʿ��������β����޸�
        /// </summary>
        private bool m_blnIsCancelArchivedCase = false;
        protected Panel m_pnlNewBase;
        protected com.digitalwave.Controls.Domain.EmrControls.ctlAreaPatientSelected m_ctlAreaPatientSelection;
        protected com.digitalwave.Controls.Domain.EmrControls.ctlEmrPatientInfo m_ctlPatientInfo;

        /// <summary>
        /// ģ��ͻ���
        /// </summary>
        private clsTemplateClient m_objTemplateClient = null;
        ///// <summary>
        ///// ��ȡģ��ͻ���
        ///// </summary>
        //public clsTemplateClient m_ObjTemplateClient
        //{
        //    get
        //    {
        //        if (m_objTemplateClient == null)
        //        {
        //            if (m_ctlAreaPatientSelection.CurrentArea != null)
        //            {
        //                m_objTemplateClient = new clsTemplateClient(this, clsEMRLogin.LoginEmployee.m_strEMPID_CHR, 
        //                    m_ctlAreaPatientSelection.CurrentArea.m_strDEPTID_CHR, false, true);
        //            }
        //        }
        //        return m_objTemplateClient;
        //    }
        //}
        /// <summary>
        /// ��ȡģ��ͻ���
        /// </summary>
        public clsTemplateClient m_ObjTemplateClient
        {
            get
            {
                string strDeptID = string.Empty;
                if (m_ObjCurrentEmrPatient != null)
                {
                    strDeptID = m_ObjCurrentEmrPatient.m_strDEPTID_CHR;
                }
                else if (m_ctlAreaPatientSelection.CurrentArea != null)
                {
                    strDeptID = m_ctlAreaPatientSelection.CurrentArea.m_strDEPTID_CHR;
                }

                if (m_objTemplateClient == null || (m_objTemplateClient.m_StrDepartmentID != strDeptID))
                {
                    m_objTemplateClient = new clsTemplateClient(this, clsEMRLogin.LoginEmployee.m_strEMPID_CHR,
                            strDeptID, false, true);
                }

                return m_objTemplateClient;
            }
        }
        //		private clsAdjustByDept m_objAdjustByDept;
        public frmHRPBaseForm()
        {
            InitializeComponent();
            m_arlMinElementColValue = new ArrayList();

            txtInPatientID.LostFocus += new EventHandler(m_mthPatientListControl);
            m_lsvInPatientID.LostFocus += new EventHandler(m_mthPatientListControl);
            m_txtPatientName.LostFocus += new EventHandler(m_lsvPatientName_LostFocus);
            m_lsvPatientName.LostFocus += new EventHandler(m_lsvPatientName_LostFocus);
            m_txtBedNO.LostFocus += new EventHandler(m_lsvBedNO_LostFocus);
            m_lsvBedNO.LostFocus += new EventHandler(m_lsvBedNO_LostFocus);
            //Liu RongGuo 2003.4.11
            m_strTemplatePath = m_strGetFilePathHeader() + "Templates\\\\";

            m_objHighLight = new ctlHighLightFocus(clsHRPColor.s_ClrHightLight);

            m_objTempTool = new clsTemplatesetInvoke();

            m_strCanModifyTime = clsEMRLogin.StrCanModifyTime;
            chkModifyWithoutMatk.Checked = true;

            if (SO.clsEMR_StaticObject.s_intGetEmrSettingValue("3016") == 1) this.m_cmdModifyPatientInfo.Visible = true;

            if (!DesignMode && this.Site == null)
                m_ctlAreaPatientSelection.m_mthInit(com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment);
        }

        /// <summary>
        /// �û����޸ļ�¼
        /// </summary>
        public const int c_intUserNotAllowToModify = -13;
        protected frmTemplatesetDialog m_frmSaveTemplateset;

        protected clsTemplatesetInvoke m_objTempTool;

        protected ctlHighLightFocus m_objHighLight;

        protected string m_strTemplatePath = "";

        protected clsSystemContext m_objCurrentContext
        {
            get
            {
                return clsSystemContext.s_ObjCurrentContext;
            }
        }

        protected bool m_blnCanTextChanged = true;

        public bool m_BlnIsMark
        {
            get { return !chkModifyWithoutMatk.Checked; }
        }

        public clsPatient m_objBaseCurrentPatient;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (m_arlMinElementColValue != null)
                {
                    m_arlMinElementColValue.Clear();
                    m_arlMinElementColValue = null;
                }
                m_objJump = null;
                //				objPIArr = null;
                if (m_frmSaveTemplateset != null)
                {
                    m_frmSaveTemplateset.Dispose();
                    m_frmSaveTemplateset = null;
                }
                m_objTempTool = null;
                m_objBaseCurrentPatient = null;
                if (m_arlRTB != null)
                {
                    m_arlRTB.Clear();
                    m_arlRTB = null;
                }

                if (m_frmSaveTemplateset != null)
                {
                    m_frmSaveTemplateset = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHRPBaseForm));
            this.lblSex = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblBedNoTitle = new System.Windows.Forms.Label();
            this.lblInHospitalNoTitle = new System.Windows.Forms.Label();
            this.lblNameTitle = new System.Windows.Forms.Label();
            this.lblSexTitle = new System.Windows.Forms.Label();
            this.lblAgeTitle = new System.Windows.Forms.Label();
            this.lblAreaTitle = new System.Windows.Forms.Label();
            this.txtInPatientID = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_lsvInPatientID = new System.Windows.Forms.ListView();
            this.clmInPatientID_BaseForm = new System.Windows.Forms.ColumnHeader();
            this.m_ttpTextInfo = new System.Windows.Forms.ToolTip(this.components);
            this.m_txtPatientName = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_txtBedNO = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.m_cboArea = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_lsvPatientName = new System.Windows.Forms.ListView();
            this.clmPatientName_BaseForm = new System.Windows.Forms.ColumnHeader();
            this.m_lsvBedNO = new System.Windows.Forms.ListView();
            this.clmBedNO_BaseForm = new System.Windows.Forms.ColumnHeader();
            this.m_cboDept = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.lblDept = new System.Windows.Forms.Label();
            this.m_cmdNewTemplate = new PinkieControls.ButtonXP();
            this.m_cmdNext = new System.Windows.Forms.Button();
            this.m_cmdPre = new System.Windows.Forms.Button();
            this.m_pnlFocus = new System.Windows.Forms.Panel();
            this.m_lblForTitle = new System.Windows.Forms.Label();
            this.chkModifyWithoutMatk = new System.Windows.Forms.CheckBox();
            this.m_tipMain = new System.Windows.Forms.ToolTip(this.components);
            this.m_cmdModifyPatientInfo = new ExternalControlsLib.XPButton();
            this.m_pnlNewBase = new System.Windows.Forms.Panel();
            this.m_ctlPatientInfo = new com.digitalwave.Controls.Domain.EmrControls.ctlEmrPatientInfo();
            this.m_ctlAreaPatientSelection = new com.digitalwave.Controls.Domain.EmrControls.ctlAreaPatientSelected();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSex
            // 
            this.lblSex.BackColor = System.Drawing.Color.Transparent;
            this.lblSex.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.ForeColor = System.Drawing.Color.Black;
            this.lblSex.Location = new System.Drawing.Point(628, 12);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(48, 19);
            this.lblSex.TabIndex = 498;
            // 
            // lblAge
            // 
            this.lblAge.BackColor = System.Drawing.Color.Transparent;
            this.lblAge.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAge.ForeColor = System.Drawing.Color.Black;
            this.lblAge.Location = new System.Drawing.Point(736, 12);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(52, 19);
            this.lblAge.TabIndex = 497;
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.AutoSize = true;
            this.lblBedNoTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBedNoTitle.ForeColor = System.Drawing.Color.Black;
            this.lblBedNoTitle.Location = new System.Drawing.Point(236, 16);
            this.lblBedNoTitle.Name = "lblBedNoTitle";
            this.lblBedNoTitle.Size = new System.Drawing.Size(42, 14);
            this.lblBedNoTitle.TabIndex = 495;
            this.lblBedNoTitle.Text = "����:";
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.AutoSize = true;
            this.lblInHospitalNoTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInHospitalNoTitle.ForeColor = System.Drawing.Color.Black;
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(224, 48);
            this.lblInHospitalNoTitle.Name = "lblInHospitalNoTitle";
            this.lblInHospitalNoTitle.Size = new System.Drawing.Size(56, 14);
            this.lblInHospitalNoTitle.TabIndex = 494;
            this.lblInHospitalNoTitle.Text = "סԺ��:";
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.AutoSize = true;
            this.lblNameTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNameTitle.ForeColor = System.Drawing.Color.Black;
            this.lblNameTitle.Location = new System.Drawing.Point(408, 16);
            this.lblNameTitle.Name = "lblNameTitle";
            this.lblNameTitle.Size = new System.Drawing.Size(42, 14);
            this.lblNameTitle.TabIndex = 493;
            this.lblNameTitle.Text = "����:";
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.AutoSize = true;
            this.lblSexTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSexTitle.ForeColor = System.Drawing.Color.Black;
            this.lblSexTitle.Location = new System.Drawing.Point(580, 12);
            this.lblSexTitle.Name = "lblSexTitle";
            this.lblSexTitle.Size = new System.Drawing.Size(42, 14);
            this.lblSexTitle.TabIndex = 492;
            this.lblSexTitle.Text = "�Ա�:";
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.AutoSize = true;
            this.lblAgeTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAgeTitle.ForeColor = System.Drawing.Color.Black;
            this.lblAgeTitle.Location = new System.Drawing.Point(688, 12);
            this.lblAgeTitle.Name = "lblAgeTitle";
            this.lblAgeTitle.Size = new System.Drawing.Size(42, 14);
            this.lblAgeTitle.TabIndex = 491;
            this.lblAgeTitle.Text = "����:";
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.AutoSize = true;
            this.lblAreaTitle.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAreaTitle.ForeColor = System.Drawing.Color.Black;
            this.lblAreaTitle.Location = new System.Drawing.Point(16, 48);
            this.lblAreaTitle.Name = "lblAreaTitle";
            this.lblAreaTitle.Size = new System.Drawing.Size(42, 14);
            this.lblAreaTitle.TabIndex = 490;
            this.lblAreaTitle.Text = "����:";
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.AccessibleName = "NoDefault";
            this.txtInPatientID.BackColor = System.Drawing.Color.White;
            this.txtInPatientID.BorderColor = System.Drawing.Color.Transparent;
            this.txtInPatientID.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInPatientID.ForeColor = System.Drawing.Color.Black;
            this.txtInPatientID.Location = new System.Drawing.Point(280, 44);
            this.txtInPatientID.Name = "txtInPatientID";
            this.txtInPatientID.Size = new System.Drawing.Size(116, 23);
            this.txtInPatientID.TabIndex = 5;
            this.txtInPatientID.TextChanged += new System.EventHandler(this.txtInPatientID_TextChanged);
            this.txtInPatientID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.BackColor = System.Drawing.Color.White;
            this.m_lsvInPatientID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lsvInPatientID.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmInPatientID_BaseForm});
            this.m_lsvInPatientID.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvInPatientID.ForeColor = System.Drawing.Color.Black;
            this.m_lsvInPatientID.FullRowSelect = true;
            this.m_lsvInPatientID.GridLines = true;
            this.m_lsvInPatientID.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_lsvInPatientID.Location = new System.Drawing.Point(280, 68);
            this.m_lsvInPatientID.Name = "m_lsvInPatientID";
            this.m_lsvInPatientID.Size = new System.Drawing.Size(116, 104);
            this.m_lsvInPatientID.TabIndex = 6;
            this.m_lsvInPatientID.UseCompatibleStateImageBehavior = false;
            this.m_lsvInPatientID.View = System.Windows.Forms.View.Details;
            this.m_lsvInPatientID.DoubleClick += new System.EventHandler(this.m_lsvInPatientID_DoubleClick);
            this.m_lsvInPatientID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);
            // 
            // clmInPatientID_BaseForm
            // 
            this.clmInPatientID_BaseForm.Width = 100;
            // 
            // m_ttpTextInfo
            // 
            this.m_ttpTextInfo.AutomaticDelay = 200;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.AccessibleName = "NoDefault";
            this.m_txtPatientName.BackColor = System.Drawing.Color.White;
            this.m_txtPatientName.BorderColor = System.Drawing.Color.Transparent;
            this.m_txtPatientName.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtPatientName.ForeColor = System.Drawing.Color.Black;
            this.m_txtPatientName.Location = new System.Drawing.Point(452, 12);
            this.m_txtPatientName.Name = "m_txtPatientName";
            this.m_txtPatientName.Size = new System.Drawing.Size(116, 23);
            this.m_txtPatientName.TabIndex = 3;
            this.m_txtPatientName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.AccessibleName = "NoDefault";
            this.m_txtBedNO.BackColor = System.Drawing.Color.White;
            this.m_txtBedNO.BorderColor = System.Drawing.Color.Transparent;
            this.m_txtBedNO.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtBedNO.ForeColor = System.Drawing.Color.Black;
            this.m_txtBedNO.Location = new System.Drawing.Point(280, 12);
            this.m_txtBedNO.Name = "m_txtBedNO";
            this.m_txtBedNO.Size = new System.Drawing.Size(116, 23);
            this.m_txtBedNO.TabIndex = 1;
            this.m_txtBedNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);
            // 
            // m_cboArea
            // 
            this.m_cboArea.AccessibleName = "NoDefault";
            this.m_cboArea.BackColor = System.Drawing.Color.White;
            this.m_cboArea.BorderColor = System.Drawing.Color.Black;
            this.m_cboArea.DropButtonBackColor = System.Drawing.SystemColors.ControlLight;
            this.m_cboArea.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboArea.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboArea.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboArea.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboArea.ForeColor = System.Drawing.Color.Black;
            this.m_cboArea.ListBackColor = System.Drawing.SystemColors.ControlLight;
            this.m_cboArea.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboArea.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboArea.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboArea.Location = new System.Drawing.Point(64, 44);
            this.m_cboArea.m_BlnEnableItemEventMenu = false;
            this.m_cboArea.Name = "m_cboArea";
            this.m_cboArea.SelectedIndex = -1;
            this.m_cboArea.SelectedItem = null;
            this.m_cboArea.SelectionStart = 0;
            this.m_cboArea.Size = new System.Drawing.Size(144, 23);
            this.m_cboArea.TabIndex = 0;
            this.m_cboArea.TabStop = false;
            this.m_cboArea.TextBackColor = System.Drawing.Color.White;
            this.m_cboArea.TextForeColor = System.Drawing.Color.Black;
            this.m_cboArea.SelectedIndexChanged += new System.EventHandler(this.m_cboArea_SelectedIndexChanged);
            this.m_cboArea.DropDown += new System.EventHandler(this.m_cboArea_DropDown);
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.BackColor = System.Drawing.Color.White;
            this.m_lsvPatientName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lsvPatientName.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmPatientName_BaseForm});
            this.m_lsvPatientName.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvPatientName.ForeColor = System.Drawing.Color.Black;
            this.m_lsvPatientName.FullRowSelect = true;
            this.m_lsvPatientName.GridLines = true;
            this.m_lsvPatientName.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_lsvPatientName.Location = new System.Drawing.Point(452, 36);
            this.m_lsvPatientName.Name = "m_lsvPatientName";
            this.m_lsvPatientName.Size = new System.Drawing.Size(116, 104);
            this.m_lsvPatientName.TabIndex = 4;
            this.m_lsvPatientName.UseCompatibleStateImageBehavior = false;
            this.m_lsvPatientName.View = System.Windows.Forms.View.Details;
            this.m_lsvPatientName.DoubleClick += new System.EventHandler(this.m_lsvPatientName_DoubleClick);
            this.m_lsvPatientName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);
            // 
            // clmPatientName_BaseForm
            // 
            this.clmPatientName_BaseForm.Width = 97;
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.BackColor = System.Drawing.Color.White;
            this.m_lsvBedNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lsvBedNO.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmBedNO_BaseForm});
            this.m_lsvBedNO.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvBedNO.ForeColor = System.Drawing.Color.Black;
            this.m_lsvBedNO.FullRowSelect = true;
            this.m_lsvBedNO.GridLines = true;
            this.m_lsvBedNO.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_lsvBedNO.Location = new System.Drawing.Point(280, 36);
            this.m_lsvBedNO.Name = "m_lsvBedNO";
            this.m_lsvBedNO.Size = new System.Drawing.Size(116, 104);
            this.m_lsvBedNO.TabIndex = 2;
            this.m_lsvBedNO.UseCompatibleStateImageBehavior = false;
            this.m_lsvBedNO.View = System.Windows.Forms.View.Details;
            this.m_lsvBedNO.DoubleClick += new System.EventHandler(this.m_lsvBedNO_DoubleClick);
            this.m_lsvBedNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);
            // 
            // clmBedNO_BaseForm
            // 
            this.clmBedNO_BaseForm.Width = 80;
            // 
            // m_cboDept
            // 
            this.m_cboDept.AccessibleName = "NoDefault";
            this.m_cboDept.BackColor = System.Drawing.Color.White;
            this.m_cboDept.BorderColor = System.Drawing.Color.Black;
            this.m_cboDept.DropButtonBackColor = System.Drawing.SystemColors.ControlLight;
            this.m_cboDept.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboDept.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboDept.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboDept.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboDept.ForeColor = System.Drawing.Color.Black;
            this.m_cboDept.ListBackColor = System.Drawing.SystemColors.ControlLight;
            this.m_cboDept.ListForeColor = System.Drawing.SystemColors.WindowText;
            this.m_cboDept.ListSelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.m_cboDept.ListSelectedForeColor = System.Drawing.SystemColors.HighlightText;
            this.m_cboDept.Location = new System.Drawing.Point(64, 12);
            this.m_cboDept.m_BlnEnableItemEventMenu = false;
            this.m_cboDept.Name = "m_cboDept";
            this.m_cboDept.SelectedIndex = -1;
            this.m_cboDept.SelectedItem = null;
            this.m_cboDept.SelectionStart = 0;
            this.m_cboDept.Size = new System.Drawing.Size(144, 23);
            this.m_cboDept.TabIndex = 9999999;
            this.m_cboDept.TabStop = false;
            this.m_cboDept.TextBackColor = System.Drawing.Color.White;
            this.m_cboDept.TextForeColor = System.Drawing.Color.Black;
            this.m_cboDept.SelectedIndexChanged += new System.EventHandler(this.m_cboDept_SelectedIndexChanged);
            this.m_cboDept.DropDown += new System.EventHandler(this.m_cboDept_DropDown);
            // 
            // lblDept
            // 
            this.lblDept.AccessibleName = "NoDefaultIn";
            this.lblDept.AutoSize = true;
            this.lblDept.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDept.ForeColor = System.Drawing.Color.Black;
            this.lblDept.Location = new System.Drawing.Point(16, 16);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(42, 14);
            this.lblDept.TabIndex = 500;
            this.lblDept.Text = "����:";
            // 
            // m_cmdNewTemplate
            // 
            this.m_cmdNewTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdNewTemplate.DefaultScheme = true;
            this.m_cmdNewTemplate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdNewTemplate.ForeColor = System.Drawing.Color.Black;
            this.m_cmdNewTemplate.Hint = "";
            this.m_cmdNewTemplate.Location = new System.Drawing.Point(704, 48);
            this.m_cmdNewTemplate.Name = "m_cmdNewTemplate";
            this.m_cmdNewTemplate.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdNewTemplate.Size = new System.Drawing.Size(84, 32);
            this.m_cmdNewTemplate.TabIndex = 10000000;
            this.m_cmdNewTemplate.Text = "����ģ��";
            this.m_cmdNewTemplate.Visible = false;
            this.m_cmdNewTemplate.Click += new System.EventHandler(this.m_blnNewTemplate_Click);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cmdNext.ForeColor = System.Drawing.Color.Black;
            this.m_cmdNext.Location = new System.Drawing.Point(196, 12);
            this.m_cmdNext.Name = "m_cmdNext";
            this.m_cmdNext.Size = new System.Drawing.Size(24, 21);
            this.m_cmdNext.TabIndex = 10000001;
            this.m_cmdNext.TabStop = false;
            this.m_cmdNext.Text = "��";
            this.m_cmdNext.Visible = false;
            this.m_cmdNext.Click += new System.EventHandler(this.m_cmdNext_Click);
            // 
            // m_cmdPre
            // 
            this.m_cmdPre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cmdPre.ForeColor = System.Drawing.Color.Black;
            this.m_cmdPre.Location = new System.Drawing.Point(156, 12);
            this.m_cmdPre.Name = "m_cmdPre";
            this.m_cmdPre.Size = new System.Drawing.Size(24, 21);
            this.m_cmdPre.TabIndex = 10000002;
            this.m_cmdPre.TabStop = false;
            this.m_cmdPre.Text = "<";
            this.m_cmdPre.Visible = false;
            this.m_cmdPre.Click += new System.EventHandler(this.m_cmdPre_Click);
            // 
            // m_pnlFocus
            // 
            this.m_pnlFocus.Location = new System.Drawing.Point(12, 16);
            this.m_pnlFocus.Name = "m_pnlFocus";
            this.m_pnlFocus.Size = new System.Drawing.Size(0, 0);
            this.m_pnlFocus.TabIndex = 0;
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(448, 8);
            this.m_lblForTitle.Name = "m_lblForTitle";
            this.m_lblForTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_lblForTitle.Size = new System.Drawing.Size(16, 23);
            this.m_lblForTitle.TabIndex = 10000003;
            this.m_lblForTitle.Visible = false;
            // 
            // chkModifyWithoutMatk
            // 
            this.chkModifyWithoutMatk.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkModifyWithoutMatk.ForeColor = System.Drawing.Color.Red;
            this.chkModifyWithoutMatk.Location = new System.Drawing.Point(580, 44);
            this.chkModifyWithoutMatk.Name = "chkModifyWithoutMatk";
            this.chkModifyWithoutMatk.Size = new System.Drawing.Size(88, 24);
            this.chkModifyWithoutMatk.TabIndex = 10000004;
            this.chkModifyWithoutMatk.Text = "�޺ۼ��޸�";
            this.chkModifyWithoutMatk.Visible = false;
            this.chkModifyWithoutMatk.Click += new System.EventHandler(this.chkModifyWithoutMatk_Click);
            // 
            // m_tipMain
            // 
            this.m_tipMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_tipMain.IsBalloon = true;
            this.m_tipMain.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.m_tipMain.ToolTipTitle = "��ܰ��ʾ";
            // 
            // m_cmdModifyPatientInfo
            // 
            this.m_cmdModifyPatientInfo.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.m_cmdModifyPatientInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdModifyPatientInfo.BtnShape = ExternalControlsLib.emunType.BtnShape.Rectangle;
            this.m_cmdModifyPatientInfo.BtnStyle = ExternalControlsLib.emunType.XPStyle.Blue;
            this.m_cmdModifyPatientInfo.ForeColor = System.Drawing.Color.Black;
            this.m_cmdModifyPatientInfo.Location = new System.Drawing.Point(629, 50);
            this.m_cmdModifyPatientInfo.Name = "m_cmdModifyPatientInfo";
            this.m_cmdModifyPatientInfo.Size = new System.Drawing.Size(69, 30);
            this.m_cmdModifyPatientInfo.TabIndex = 10000000;
            this.m_cmdModifyPatientInfo.Text = "��������";
            this.m_tipMain.SetToolTip(this.m_cmdModifyPatientInfo, "����鿴���޸Ļ�����ϸ��Ϣ(��ݼ�Alt+P)");
            this.m_cmdModifyPatientInfo.UseVisualStyleBackColor = false;
            this.m_cmdModifyPatientInfo.Visible = false;
            this.m_cmdModifyPatientInfo.Click += new System.EventHandler(this.m_cmdModifyPatientInfo_Click);
            // 
            // m_pnlNewBase
            // 
            this.m_pnlNewBase.AccessibleName = "NoDefaultIn";
            this.m_pnlNewBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pnlNewBase.Controls.Add(this.m_ctlPatientInfo);
            this.m_pnlNewBase.Controls.Add(this.m_ctlAreaPatientSelection);
            this.m_pnlNewBase.Location = new System.Drawing.Point(10, 7);
            this.m_pnlNewBase.Name = "m_pnlNewBase";
            this.m_pnlNewBase.Size = new System.Drawing.Size(785, 60);
            this.m_pnlNewBase.TabIndex = 10000005;
            this.m_pnlNewBase.Visible = false;
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ctlPatientInfo.Font = new System.Drawing.Font("����", 10.5F);
            this.m_ctlPatientInfo.Location = new System.Drawing.Point(0, 29);
            this.m_ctlPatientInfo.m_BlnIsShowAddres = false;
            this.m_ctlPatientInfo.m_BlnIsShowHomePlace = false;
            this.m_ctlPatientInfo.m_BlnIsShowMarriage = false;
            this.m_ctlPatientInfo.m_BlnIsShowOccupy = false;
            this.m_ctlPatientInfo.m_BlnIsShowOffice = false;
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowRace = false;
            this.m_ctlPatientInfo.m_BlnIsShowRelationName = false;
            this.m_ctlPatientInfo.m_BlnIsShowRelationPhone = false;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            this.m_ctlPatientInfo.Name = "m_ctlPatientInfo";
            this.m_ctlPatientInfo.Size = new System.Drawing.Size(783, 29);
            this.m_ctlPatientInfo.TabIndex = 10000006;
            // 
            // m_ctlAreaPatientSelection
            // 
            this.m_ctlAreaPatientSelection.AutoSize = false;
            this.m_ctlAreaPatientSelection.Font = new System.Drawing.Font("����", 10.5F);
            this.m_ctlAreaPatientSelection.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_ctlAreaPatientSelection.Location = new System.Drawing.Point(0, 0);
            this.m_ctlAreaPatientSelection.m_BlnIsInUse = true;
            this.m_ctlAreaPatientSelection.m_BlnIsShowArea = true;
            this.m_ctlAreaPatientSelection.m_BlnIsShowBorder = false;
            this.m_ctlAreaPatientSelection.m_BlnIsShowDefaultArea = true;
            this.m_ctlAreaPatientSelection.m_ClrEnd = System.Drawing.SystemColors.Control;
            this.m_ctlAreaPatientSelection.m_ClrStart = System.Drawing.SystemColors.Control;
            this.m_ctlAreaPatientSelection.Name = "m_ctlAreaPatientSelection";
            this.m_ctlAreaPatientSelection.Size = new System.Drawing.Size(783, 29);
            this.m_ctlAreaPatientSelection.TabIndex = 2;
            this.m_ctlAreaPatientSelection.evtAreaChanged += new com.digitalwave.Controls.Domain.EmrControls.AreaSelectedEventHandler(this.m_ctlAreaPatientSelection_evtAreaChanged);
            this.m_ctlAreaPatientSelection.evtBedChanged += new com.digitalwave.Controls.Domain.EmrControls.BedSelectedEventHandler(this.m_ctlAreaPatientSelection_evtBedChanged);
            this.m_ctlAreaPatientSelection.evtSessionSelected += new com.digitalwave.Controls.Domain.EmrControls.SelectedSessionEventHandler(this.m_ctlAreaPatientSelection_evtSessionSelected);
            this.m_ctlAreaPatientSelection.evtRefreshResult += new com.digitalwave.Controls.Domain.EmrControls.RefreshEventHandler(this.m_ctlAreaPatientSelection_evtRefreshResult);
            // 
            // frmHRPBaseForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(10, 10);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(797, 502);
            this.Controls.Add(this.m_pnlNewBase);
            this.Controls.Add(this.chkModifyWithoutMatk);
            this.Controls.Add(this.m_lblForTitle);
            this.Controls.Add(this.m_lsvInPatientID);
            this.Controls.Add(this.m_lsvPatientName);
            this.Controls.Add(this.m_lsvBedNO);
            this.Controls.Add(this.m_pnlFocus);
            this.Controls.Add(this.lblDept);
            this.Controls.Add(this.lblAreaTitle);
            this.Controls.Add(this.lblBedNoTitle);
            this.Controls.Add(this.m_txtBedNO);
            this.Controls.Add(this.lblInHospitalNoTitle);
            this.Controls.Add(this.txtInPatientID);
            this.Controls.Add(this.lblNameTitle);
            this.Controls.Add(this.m_txtPatientName);
            this.Controls.Add(this.lblAgeTitle);
            this.Controls.Add(this.lblSexTitle);
            this.Controls.Add(this.m_cmdModifyPatientInfo);
            this.Controls.Add(this.m_cmdNewTemplate);
            this.Controls.Add(this.m_cboDept);
            this.Controls.Add(this.m_cboArea);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.m_cmdPre);
            this.Controls.Add(this.m_cmdNext);
            this.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmHRPBaseForm";
            this.Deactivate += new System.EventHandler(this.frmHRPBaseForm_Deactivate);
            this.Enter += new System.EventHandler(this.frmHRPBaseForm_Enter);
            this.Click += new System.EventHandler(this.frmHRPBaseForm_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mth_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmHRPBaseForm_MouseDown);
            this.Load += new System.EventHandler(this.frmHRPBaseForm_Load);
            this.m_pnlNewBase.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region ���ƵĲ���
        /// <summary>
        /// ��ȡ��ǰ��״̬
        /// </summary>
        protected virtual enmFormState m_EnmCurrentFormState
        {
            get
            {
                //				throw new Exception("û��ʵ�� m_EnmCurrentFormState ����");
                return enmFormState.NowUser;
            }
        }

        private DateTime m_dtmPreSave;

        /// <summary>
        /// ��ȡ��ǰ�����SF
        /// </summary>
        /// <returns></returns>
        private enmPrivilegeSF m_enmGetPrivilegeSF()
        {
            try//��˽�������
            {
                return (enmPrivilegeSF)Enum.Parse(typeof(enmPrivilegeSF), this.GetType().Name);//��ʱ��thisΪ�Ӵ���
            }
            catch
            {
                return enmPrivilegeSF.frmInPatientCaseHistory;
            }
        }
        private bool m_blnNeedContextMenu = true;
        protected bool m_BlnNeedContextMenu
        {
            get { return m_blnNeedContextMenu; }
            set { m_blnNeedContextMenu = value; }
        }

        /// <summary>
        /// ��֤�����λ�ʿ��
        /// </summary>
        /// <returns></returns>
        protected bool m_BlnCheckValidateByDirector()
        {
            bool blnValidate = false;

            System.Text.StringBuilder strMes = new System.Text.StringBuilder(100);
            strMes.Append("�˲��˵����в���Ϊֻ�������ܴ��̡�");
            strMes.Append(Environment.NewLine);
            strMes.Append("��������辭��");
            if (m_EnmAppType == enmApproveType.Nurses)
            {
                strMes.Append("��ʿ��");
                intFormType = 2;
            }
            else
            {
                strMes.Append("������");
                intFormType = 1;
            }
            strMes.Append("����׼��");
            strMes.Append(Environment.NewLine);
            strMes.Append("�����밴���ǡ���ȡ���밴���񡿡�");

            string strMessageInf = strMes.ToString();
            if (MDIParent.ShowInformationMessageBox(strMessageInf, System.Windows.Forms.MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string strDeptIDTemp = m_ObjCurrentEmrPatientSession.m_strAreaId;//((clsDepartment)m_cboDept.SelectedItem).m_strDeptNewID;
                frmValidateByDirector frm = new frmValidateByDirector(intFormType, strDeptIDTemp);
                frm.ShowDialog(this);
                blnValidate = frm.BlnValidateResult;
            }
            return blnValidate;
        }

        /// <summary>
        /// ��ȡ��ǩ����
        /// </summary>
        protected clsEMR_CaseAuditVO m_objGetAuditCase()
        {
            clsEMR_CaseAuditVO objVO = new clsEMR_CaseAuditVO();
            objVO.m_strREGISTERID_CHR = m_ObjCurrentEmrPatientSession.m_strRegisterId;
            objVO.m_intSTATUS_INT = 0;
            objVO.m_strCREATORID = com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjCurrentEmployee.m_strEMPID_CHR;
            objVO.m_strFORMID_VCHR = com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_FrmMDI.ActiveMdiChild.Name;
            objVO.m_strFORMNAME_VCHR = this.AccessibleDescription;
            objVO.m_strDeptID = m_ObjCurrentEmrPatientSession.m_strDeptId;
            objVO.m_strAreaID = m_ObjCurrentEmrPatientSession.m_strAreaId;
            return objVO;
        }

        /// <summary>
        /// �����ǩ����
        /// </summary>
        /// <param name="p_objVO">��ǩ����</param>
        protected void m_mthAddAuditCase(clsEMR_CaseAuditVO p_objVO)
        {
            //com.digitalwave.emr.AssistModuleSev.clsAuditCaseServ_Modify objServ =
            //    (com.digitalwave.emr.AssistModuleSev.clsAuditCaseServ_Modify)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.emr.AssistModuleSev.clsAuditCaseServ_Modify));
            long lngRes = (new weCare.Proxy.ProxyEmr06()).Service.m_lngAddCaseAudit(p_objVO);
            //objServ = null;
        }

        /// <summary>
        /// ɾ����ǩ����
        /// </summary>
        /// <param name="p_objVO">��ǩ����</param>
        protected void m_mthDelAuditCase(clsEMR_CaseAuditVO p_objVO)
        {
            //com.digitalwave.emr.AssistModuleSev.clsAuditCaseServ_Modify objServ =
            //    (com.digitalwave.emr.AssistModuleSev.clsAuditCaseServ_Modify)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.emr.AssistModuleSev.clsAuditCaseServ_Modify));
            long lngRes = (new weCare.Proxy.ProxyEmr06()).Service.m_lngDelCaseAudit(p_objVO);
            //objServ = null;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <returns>�������</returns>
        public long m_lngSave()
        {
            //			enmPrivilegeSF enmSF = m_enmGetPrivilegeSF();
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID,enmSF,enmPrivilegeOperation.AddOrModify))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return 0;
			}			
#endif

            //			string s = "123";
            //			int i = int.Parse(s);
            //			enmPrivilegeSF enmSF = enmPrivilegeSF.BabyInjuryCaseEvaluation;

            //			if(m_objBaseCurrentPatient == null)
            //			{
            //				clsPublicFunction.ShowInformationMessageBox("����ѡ���ˡ�");
            //				return -10;
            //			}
            //
            //			if(m_objBaseCurrentPatient.m_StrInPatientID != txtInPatientID.Text)
            //			{
            //				clsPublicFunction.ShowInformationMessageBox("סԺ���벡����Ϣ��һ�£����ܴ��̡�");
            //				return -11;
            //			}

            //			if(!new clsEmployeeSignTool().m_blnCheckSignRightMethod(this)) return -10;

            if (DateTime.Now.Second == m_dtmPreSave.Second)
            {
                System.Threading.Thread.Sleep(1000);
            }
            string strTimeRemaining;
            //			bool blnReadOnly=false;
            //			new clsInPatientArchivingDomain().lngIsReadOnly(m_objBaseCurrentPatient.m_StrInPatientID,m_objBaseCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),out blnReadOnly,out strTimeRemaining);
            //			if(blnReadOnly)
            //			{
            //				clsPublicFunction.ShowInformationMessageBox("�˲��˵����в���Ϊֻ�������ܴ��̡�");
            //				return (long)enmOperationResult.Not_permission;
            //			}
            try
            {
                bool blnHasDirectorRole = false;
                if (m_EnmAppType == enmApproveType.Nurses)
                {
                    blnHasDirectorRole = new clsApprove_FlowDomain().blnEmployeeMatchingRole(clsLoginContext.s_ObjLoginContext.m_StrEmployeeID, "��ʿ��");
                }
                else
                {
                    blnHasDirectorRole = new clsApprove_FlowDomain().blnEmployeeMatchingRole(clsLoginContext.s_ObjLoginContext.m_StrEmployeeID, "������");
                }

                if (!blnHasDirectorRole)
                {
                    if (m_blnReadOnly)
                    {
                        if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_IntNeedRequestToSeeCase == 1)
                        {
                            return (long)enmOperationResult.Not_permission;
                        }
                        else
                        {
                            if (!m_BlnCheckValidateByDirector())
                            {
                                return (long)enmOperationResult.Not_permission;
                            }
                        }
                    }
                    else if (m_blnIsCancelArchivedCase)
                    {
                        if (!m_BlnCheckValidateByDirector())
                        {
                            return (long)enmOperationResult.Not_permission;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string strMes = e.Message;
            }


            if (m_BlnIsAddNew)
            {
                //				if(m_objCurrentContext.m_ObjControl.m_enmAddNewCheck(((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID,this,m_EnmCurrentFormState)
                //					== enmDBControlCheckResult.Disable)
                //				{
                //					clsPublicFunction.s_mthShowNotPermitMessage();
                //					return -1;
                //				}
                //				else
                //				{
                this.Cursor = Cursors.WaitCursor;
                //					txtInPatientID.Focus();
                long lngRes = m_lngSubAddNew();
                if (lngRes > 0)
                {
                    m_mthAfterSuccessfulSave();
                }

                m_mthSaveMinElementValue();
                m_mthAddFormStatusForClosingSave();
                this.Cursor = Cursors.Default;
                m_dtmPreSave = DateTime.Now;
                return lngRes;
                //				}
            }
            else
            {
                //				if(m_objCurrentContext.m_ObjControl.m_enmModifyCheck(((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID,this,m_EnmCurrentFormState)
                //					== enmDBControlCheckResult.Disable)
                //				{
                //					clsPublicFunction.s_mthShowNotPermitMessage();
                //					return -1;
                //				}
                if (!lngCanYouDoIt())
                {
                    clsPublicFunction.ShowInformationMessageBox("�õ��ѱ��ϼ���˹�������Ȩ�޸ģ�");
                    return -1;
                }
                else
                {
                    //					if(!clsPublicFunction.s_blnAskForModify())
                    //						return c_intUserNotAllowToModify;

                    this.Cursor = Cursors.WaitCursor;
                    //					txtInPatientID.Focus();
                    long lngRes = m_lngSubModify();
                    if (lngRes > 0)
                    {
                        m_mthAfterSuccessfulSave();
                    }
                    m_mthSaveMinElementValue();
                    m_mthAddFormStatusForClosingSave();
                    this.Cursor = Cursors.Default;
                    m_dtmPreSave = DateTime.Now;
                    return lngRes;
                }
            }
        }

        /// <summary>
        /// �ɹ���������Ĳ���(�����޸Ķ���ִ��)
        /// </summary>
        protected virtual void m_mthAfterSuccessfulSave()
        {
            return;
        }

        protected bool m_blnNeedCheckArchive = true;
        protected virtual void m_mthIsReadOnly()
        {
            if (m_blnNeedCheckArchive)
            {
                m_blnReadOnly = false;
                m_strTimeRemain = "";
                if (m_objBaseCurrentPatient != null)
                {
                    m_mthCheckCaseArchiving(m_objBaseCurrentPatient.m_StrRegisterId, out m_blnReadOnly, out m_strTimeRemain);
                    m_mthPromtForArchiving(m_blnReadOnly, m_strTimeRemain);
                    if (m_blnReadOnly || m_blnIsCancelArchivedCase)
                    {
                        m_objBaseCurrentPatient.m_IntCharacter = 1;
                        m_mthSetIfCanSelectPatient(false);
                    }
                    else
                    {
                        m_objBaseCurrentPatient.m_IntCharacter = 0;
                        m_mthSetIfCanSelectPatient(true);
                    }
                }
            }
        }

        /// <summary>
        /// ��鲡�˲����Ƿ��ѹ鵵��������Ӧ��ʾ
        /// </summary>
        /// <param name="p_strRegisterID">��Ժ�Ǽ���ˮ��</param>
        /// <param name="p_blnReadOnly">�Ƿ�ֻ��</param>
        /// <param name="p_strTimeRemain">ʣ���޸�ʱ����ʾ</param>
        private void m_mthCheckCaseArchiving(string p_strRegisterID, out bool p_blnReadOnly, out string p_strTimeRemain)
        {
            p_blnReadOnly = false;
            p_strTimeRemain = string.Empty;
            m_blnIsCancelArchivedCase = false;

            //clsEMR_CaseArchivingService objServ =
            //        (clsEMR_CaseArchivingService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsEMR_CaseArchivingService));

            clsEMR_CaseArchivingValue objVO = null;
            int intWorkDaysOnly = com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_intGetEmrSettingValue("3020");
            long lngRes = (new weCare.Proxy.ProxyEmr06()).Service.m_lngCheckFormReadOnly( p_strRegisterID, intWorkDaysOnly == 1, out objVO);

            if (objVO != null)
            {
                if (objVO.m_intARCHIVEDSTATUS_INT == 1 || objVO.m_intARCHIVEDSTATUS_INT == 2)
                {
                    p_blnReadOnly = true;
                    return;
                }

                if (objVO.m_intARCHIVEDSTATUS_INT == -1)
                {
                    m_blnIsCancelArchivedCase = true;
                }
                else
                {
                    m_blnIsCancelArchivedCase = false;

                    //string strDeadLine = "7";
                    //lngRes = new com.digitalwave.PublicMiddleTier.clsPublicMiddleTier().m_lngGetConfigBySettingID("3010", out strDeadLine);
                    int intDeadLine = com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_intGetEmrSettingValue("3010");
                    if (intDeadLine == -1)
                        intDeadLine = 7;

                    int intLeaveDay = (intDeadLine * 24 - objVO.m_intPassHour) / 24;
                    int intLeaveHour = (intDeadLine * 24 - objVO.m_intPassHour) % 24;

                    if (intLeaveDay < 0 || (intLeaveDay == 0 && intLeaveHour <= 0))
                    {
                        p_blnReadOnly = true;
                        return;
                    }

                    p_strTimeRemain = intLeaveDay.ToString() + "��" + intLeaveHour.ToString() + "Сʱ";
                }
            }
        }


        /// <summary>
        /// �Ƿ��ܴ�ӡ��ǰҳ���ݣ����Ƿ�����ʾ��������һ��
        /// </summary>
        protected bool m_blnCanPrint = false;
        /// <summary>
        /// ���鵵���Ƿ�����ʾ��������
        /// </summary>
        /// <returns></returns>
        protected bool m_blnCanShowRecordContent()
        {
            bool blnCanShow = false;
            if (!m_blnReadOnly || com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_IntNeedRequestToSeeCase == 0)
            {
                blnCanShow = true;
            }
            else
            {
                int[] intStatusArr = null;
                long lngRes = new clsPublicDomain().m_lngGetSpecifyApproveInfo(SO::clsEMR_StaticObject.s_ObjCurrentEmployee.m_strEMPID_CHR, m_objBaseCurrentPatient.m_StrRegisterId, out intStatusArr);
                if (intStatusArr != null && intStatusArr.Length > 0)
                {
                    for (int i1 = 0; i1 < intStatusArr.Length; i1++)
                    {
                        if (intStatusArr[i1] == 1)
                        {
                            blnCanShow = true;
                            break;
                        }
                    }
                }
                if (!blnCanShow)
                {
                    string strRoleID;
                    //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objServ =
                    //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));
                    lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngCheckRoleByEmpIDAndRoleName(SO::clsEMR_StaticObject.s_ObjCurrentEmployee.m_strEMPID_CHR, "������", out strRoleID);
                    if (!string.IsNullOrEmpty(strRoleID))
                    {
                        blnCanShow = true;
                    }
                }
            }
            m_blnCanPrint = blnCanShow;
            return blnCanShow;
        }

        /// <summary>
        /// �Ӵ������Ӳ�������ע�⣬�˲������ܱ����ã�
        /// </summary>
        /// <returns></returns>
        protected virtual long m_lngSubAddNew()
        {
            //			throw new Exception("û��ʵ�� m_lngSubAddNew ����");
            return 1;
        }

        /// <summary>
        /// �Ӵ�����޸Ĳ�������ע�⣬�˲������ܱ����ã�
        /// </summary>
        /// <returns></returns>
        protected virtual long m_lngSubModify()
        {
            //			throw new Exception("û��ʵ�� m_lngSubModify ����");
            return 1;
        }

        /// <summary>
        /// �Ƿ�������¼�¼�Ĳ�����true������¼�¼��false,�޸ļ�¼
        /// </summary>
        protected virtual bool m_BlnIsAddNew
        {
            get
            {
                //				throw new Exception("û��ʵ�� m_BlnIsAddNew ����");
                return true;
            }
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <returns>�������</returns>
        public long m_lngDelete()
        {
            //			enmPrivilegeSF enmSF = m_enmGetPrivilegeSF();
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID,enmSF,enmPrivilegeOperation.Delete))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return 0;
			}			
#endif
            //			if(m_objCurrentContext.m_ObjControl.m_enmDeleteCheck(((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID,this,m_EnmCurrentFormState)
            //				== enmDBControlCheckResult.Disable)
            //			{
            //				clsPublicFunction.s_mthShowNotPermitMessage();
            //				return -1;
            //			}


            if (m_objBaseCurrentPatient == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����ѡ����!");
                return -10;
            }
            ////��֤ɾ��
            //clsDeleteVerify objDeleteVerify = new clsDeleteVerify();
            //if (objDeleteVerify.m_mthIsDelete(null, null) == false)
            //{
            //    clsPublicFunction.ShowInformationMessageBox("��֤ʧ�ܲ���ɾ����");
            //    return -12;
            //}
            ////�ͷ�
            //objDeleteVerify = null;
            //if (m_objBaseCurrentPatient.m_StrHISInPatientID != txtInPatientID.Text)
            //{
            //    clsPublicFunction.ShowInformationMessageBox("סԺ���벡����Ϣ��һ�£�����ɾ��!");
            //    return -11;
            //}
            string strTimeRemaining;
            //bool blnReadOnly = false;
            //new clsInPatientArchivingDomain().lngIsReadOnly(m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), out blnReadOnly, out strTimeRemaining);
            if (m_blnReadOnly)
            {
                clsPublicFunction.ShowInformationMessageBox("�˲��˵����в���Ϊֻ��������ɾ��!");
                return (long)enmOperationResult.Not_permission;
            }


            if (m_StrCurrentOpenDate == "" || m_StrCurrentOpenDate == null)
            {
                //clsPublicFunction.ShowInformationMessageBox("��ǰû�м�¼���¼����Ϊ��");
                return -1;
            }
            if (!lngCanYouDoIt())
            {
                clsPublicFunction.ShowInformationMessageBox("�õ��ѱ��ϼ���˹�������Ȩɾ����");
                return -1;
            }

            if (!clsPublicFunction.s_blnAskForDelete())
                return -1;



            long lngRes = 0;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                lngRes = m_lngSubDelete();
                if (lngRes > 0)
                    chkModifyWithoutMatk.Checked = true;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return lngRes;

        }

        /// <summary>
        /// �Ӵ����ɾ����������ע�⣬�˲������ܱ����ã�
        /// </summary>
        /// <returns></returns>
        protected virtual long m_lngSubDelete()
        {
            //			throw new Exception("û��ʵ�� m_lngSubDelete ����");
            return 1;
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <returns>�������</returns>
        public long m_lngPrint()
        {
            //			enmPrivilegeSF enmSF = m_enmGetPrivilegeSF();
#if FunctionPrivilege
			if(!clsPublicFunction.s_blnCheckCurrentPrivilege(((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID,enmSF,enmPrivilegeOperation.Print))
			{
				clsPublicFunction.s_mthShowNotPermitMessage();
				return 0;
			}			
#endif

            //			if(m_objCurrentContext.m_ObjControl.m_enmPrintCheck(((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID,this,m_EnmCurrentFormState)
            //				== enmDBControlCheckResult.Disable)
            //			{
            //				clsPublicFunction.s_mthShowNotPermitMessage();
            //				return -1;
            //			}
            //			else
            //			{
            if (m_objBaseCurrentPatient != null && !m_blnCanPrint)
            {
                clsPublicFunction.ShowInformationMessageBox("�˲��˲����ѹ鵵����ǰ�û�û�в鿴��Ȩ�ޣ����ܴ�ӡ��");
                return 0;
            }

            if (!clsPublicFunction.m_blnCheckCanPrint(this.GetType().Namespace, this.Name, clsEMR_StaticObject.s_ObjCurrentEmployee.m_strRoleIDArr))
            {
                clsPublicFunction.ShowInformationMessageBox("��ǰ��½�û�û��Ȩ�޴�ӡ�˵���");
                return 0;
            }

            this.Cursor = Cursors.WaitCursor;
            long lngRes = 0;
            if (m_dlgHandleSaveBeforePrint() != DialogResult.Cancel)
                lngRes = m_lngSubPrint();
            this.Cursor = Cursors.Default;

            return lngRes;
            //			}
        }

        /// <summary>
        /// ��ӡǰ��ʾ����
        /// </summary>
        protected virtual DialogResult m_dlgHandleSaveBeforePrint()
        {

            DialogResult dlgResult = DialogResult.None;
            if (!MDIParent.s_ObjSaveCue.m_blnCheckStatusSame(this))
            {
                dlgResult = clsPublicFunction.ShowQuestionMessageBox("[" + this.Text + "]���˸Ķ����Ƿ񱣴棿", MessageBoxButtons.YesNoCancel);

                if (dlgResult == DialogResult.Yes)
                    m_lngSave();
            }
            return dlgResult;
        }

        /// <summary>
        /// �Ӵ���Ĵ�ӡ��������ע�⣬�˲������ܱ����ã�
        /// </summary>
        /// <returns></returns>
        protected virtual long m_lngSubPrint()
        {
            //			throw new Exception("û��ʵ�� m_lngSubPrint ����");
            return 1;
        }
        #endregion

        #region Copy,Cut,Paste
        /// <summary>
        /// ���Ʋ���
        /// </summary>
        /// <returns>�������</returns>
        public long m_lngCopy()
        {
            Control ctlControl = this.ActiveControl;
            string strTypeName = ctlControl.GetType().FullName;
            //			if(strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
            //			{
            switch (strTypeName)
            {
                case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                    if (((ctlRichTextBox)ctlControl).Text != "")
                    {
                        ((ctlRichTextBox)ctlControl).Copy();
                        return 1;
                    }
                    break;

                case "com.digitalwave.controls.ctlRichTextBox":
                    if (((com.digitalwave.controls.ctlRichTextBox)ctlControl).Text != "")
                    {
                        ((com.digitalwave.controls.ctlRichTextBox)ctlControl).Copy();
                        return 1;
                    }
                    break;

                case "System.Windows.Forms.RichTextBox":
                case "iCare.CustomForm.ctlRichTextBox":
                case "com.digitalwave.Utility.Controls.ctlTemplateEditer":
                    if (((RichTextBox)ctlControl).Text != "")
                    {
                        ((RichTextBox)ctlControl).Copy();
                        return 1;
                    }
                    break;

                case "System.Windows.Forms.TextBox":
                    if (((TextBox)ctlControl).Text != "")
                    {
                        ((TextBox)ctlControl).Copy();
                        return 1;
                    }
                    break;

                case "com.digitalwave.Utility.Controls.ctlBorderTextBox":
                    if (((ctlBorderTextBox)ctlControl).Text != "")
                    {
                        ((ctlBorderTextBox)ctlControl).Copy();
                        return 1;
                    }
                    break;

                case "System.Windows.Forms.DataGridTextBox":
                    if (((DataGridTextBox)ctlControl).Text != "")
                    {
                        ((DataGridTextBox)ctlControl).Copy();
                        return 1;
                    }
                    break;

                default:
                    Clipboard.SetDataObject("");
                    break;
            }
            //			}

            return 0;
        }

        /// <summary>
        /// ���в���
        /// </summary>
        /// <returns>�������</returns>
        public long m_lngCut()
        {
            Control ctlControl = this.ActiveControl;
            string strTypeName = ctlControl.GetType().FullName;
            //			if(strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
            //			{
            switch (strTypeName)
            {
                case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                    if (((ctlRichTextBox)ctlControl).Text != "")
                    {
                        ((ctlRichTextBox)ctlControl).m_mthCut();
                        return 1;
                    }
                    break;

                case "com.digitalwave.controls.ctlRichTextBox":
                    if (((com.digitalwave.controls.ctlRichTextBox)ctlControl).Text != "")
                    {
                        ((com.digitalwave.controls.ctlRichTextBox)ctlControl).Cut();
                        return 1;
                    }
                    break;

                case "System.Windows.Forms.RichTextBox":
                case "iCare.CustomForm.ctlRichTextBox":
                    if (((RichTextBox)ctlControl).Text != "")
                    {
                        ((RichTextBox)ctlControl).Cut();
                        return 1;
                    }
                    break;

                case "System.Windows.Forms.TextBox":
                    if (((TextBox)ctlControl).Text != "")
                    {
                        ((TextBox)ctlControl).Cut();
                        return 1;
                    }
                    break;

                case "com.digitalwave.Utility.Controls.ctlBorderTextBox":
                    if (((ctlBorderTextBox)ctlControl).Text != "")
                    {
                        ((ctlBorderTextBox)ctlControl).Cut();
                        return 1;
                    }
                    break;

                case "System.Windows.Forms.DataGridTextBox":
                    if (((DataGridTextBox)ctlControl).Text != "")
                    {
                        ((DataGridTextBox)ctlControl).Cut();
                        return 1;
                    }
                    break;
            }
            //			}

            return 0;
        }

        /// <summary>
        /// ճ������
        /// </summary>
        /// <returns>�������</returns>
        public long m_lngPaste()
        {
            Control ctlControl = this.ActiveControl;
            string strTypeName = ctlControl.GetType().FullName;

            //			if(strTypeName == "ctlRichTextBox" || strTypeName == "RichTextBox" || strTypeName == "TextBox" || strTypeName == "ctlBorderTextBox" || strTypeName == "DataGridTextBox")
            //			{
            switch (strTypeName)
            {
                case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                    ((ctlRichTextBox)ctlControl).Paste();
                    return 1;

                case "com.digitalwave.controls.ctlRichTextBox":
                    ((TextBoxBase)ctlControl).Paste();
                    return 1;

                case "System.Windows.Forms.RichTextBox":
                case "iCare.CustomForm.ctlRichTextBox":
                    ((RichTextBox)ctlControl).Paste();
                    return 1;

                case "System.Windows.Forms.TextBox":
                    ((TextBox)ctlControl).Paste();
                    return 1;

                case "com.digitalwave.Utility.Controls.ctlBorderTextBox":
                    ((ctlBorderTextBox)ctlControl).Paste();
                    return 1;

                case "System.Windows.Forms.DataGridTextBox":
                    ((DataGridTextBox)ctlControl).Paste();
                    return 1;
            }
            //			}

            return 0;
        }
        #endregion

        protected virtual clsPatient[] m_objGetPatientByInPatientID()
        {
            return m_objCurrentContext.m_ObjPatientManager.m_objGetAllPatientLike(txtInPatientID.Text.Trim());
        }

        protected void m_mthAddRichTemplate(RichTextBox p_txtControl)
        {
            m_objTempTool.m_mthAddTextBox(this, p_txtControl, m_strGetCurFormName(), p_txtControl.Name);
        }

        protected virtual void m_mthAddRichTemplateInContainer(Control p_ctlContainer)
        {
            //���ÿ���ģ��Ŀؼ��ĸ�����������ѡ���������Խ��--wf20080109
            m_objTempTool.m_IntTemplateNumber = 0;
            m_mthSetRichTemplateInContainer(p_ctlContainer);
        }

        protected void m_mthSetRichTemplateInContainer(Control p_ctlContainer)
        {
            foreach (Control ctlChild in p_ctlContainer.Controls)
            {
                if ((ctlChild.Name == "" && ctlChild.GetType().FullName != "System.Windows.Forms.TabPage") || ctlChild.GetType().Name == "ctlTimePicker" || ctlChild.GetType().Name == "ctlPaintContainer" || ctlChild.GetType().Name == "DataGrid")
                    continue;
                switch (ctlChild.GetType().FullName)
                {
                    case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                        m_mthAddRichTemplate((RichTextBox)ctlChild);
                        m_mthAddRichTextInfo((ctlRichTextBox)ctlChild);
                        break;
                    case "com.digitalwave.controls.ctlRichTextBox":
                        m_mthAddRichTemplate((RichTextBox)ctlChild);
                        m_mthAddRichTextInfo((com.digitalwave.controls.ctlRichTextBox)ctlChild);
                        break;
                    case "System.Windows.Forms.RichTextBox":
                    case "iCare.CustomForm.ctlRichTextBox":
                        m_mthAddRichTemplate((RichTextBox)ctlChild);
                        break;
                    case "com.digitalwave.Utility.Controls.ctlComboBox":
                    case "com.digitalwave.Controls.ctlComboBox":
                        m_mthAssociateComboBoxItemEvent(ctlChild);
                        break;
                    case "System.Windows.Forms.RadioButton":
                        ((System.Windows.Forms.RadioButton)ctlChild).MouseDown += new MouseEventHandler(m_mthrdb_MouseDown);
                        break;
                    default:
                        m_mthSetRichTemplateInContainer(ctlChild);
                        break;
                }
            }
        }

        private void m_mthPatientListControl(object sender, EventArgs e)
        {
            if ((!txtInPatientID.Focused) && (!m_lsvInPatientID.Focused))
            {
                m_lsvInPatientID.Visible = false;
            }
        }

        private void m_lsvPatientName_LostFocus(object sender, EventArgs e)
        {
            if (!m_lsvPatientName.Focused)
            {
                m_lsvPatientName.Visible = false;
            }
        }

        private void m_lsvBedNO_LostFocus(object sender, EventArgs e)
        {
            if (!m_lsvBedNO.Focused)
            {
                m_lsvBedNO.Visible = false;
            }
        }

        /// <summary>
        /// ���Ҳ��ˣ�ȱʡ�������в��ˣ����ɸ����ṩ�µ�ʵ�֣���
        /// </summary>
        /// <returns></returns>
        protected virtual clsPatient[] m_objGetPatient()
        {
            if (m_cboDept.GetItemsCount() > 0 && m_cboArea.GetItemsCount() <= 0)
                return null;//�����汾��ʱ����סԺ��ģ����ѯ
            if (m_cboArea.SelectedItem == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����ѡ����!");
                return null;
            }
            return m_objCurrentContext.m_ObjPatientManager.m_objGetInPatientByAreaIDLike(((clsInPatientArea)m_cboArea.SelectedItem).m_StrAreaID, txtInPatientID.Text);
            //return m_objCurrentContext.m_ObjPatientManager.m_objGetInPatientByDeptIDLike(MDIParent.s_ObjDepartment,txtInPatientID.Text);
        }

        /// <summary>
        /// ���Ҳ��ˣ�ȴʡ�������в��ˣ����ɸ����ṩ�µ�ʵ�֣���
        /// </summary>
        /// <returns></returns>
        protected virtual clsPatient[] m_objGetPatientByPatientName()
        {
            if (m_cboArea.SelectedItem == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����ѡ����!");
                return null;
            }
            return m_objCurrentContext.m_ObjPatientManager.m_objGetPatientByLikePatientName_InArea(((clsInPatientArea)m_cboArea.SelectedItem).m_StrAreaID, m_txtPatientName.Text);
            //return m_objCurrentContext.m_ObjPatientManager.m_objGetPatientByLikePatientName(MDIParent.s_ObjDepartment,m_txtPatientName.Text);
        }

        /// <summary>
        /// �Ƿ�������д��ţ�Ĭ��Ϊ��
        /// </summary>
        private bool m_blnGetAllBedNo = false;

        /// <summary>
        /// ���Ҳ��ˣ�ȴʡ�������в��ˣ����ɸ����ṩ�µ�ʵ�֣���
        /// </summary>
        /// <returns></returns>
        protected virtual clsPatient[] m_objGetPatientByBedNO()
        {
            //if (this.m_cboArea.SelectedItem != null)
            //{
            //    if (m_blnGetAllBedNo)
            //        return m_objCurrentContext.m_ObjPatientManager.m_objGetPatientByLikeBedNO_InArea(((clsInPatientArea)(this.m_cboArea.SelectedItem)).m_strAreaNewID, "");

            //    else
            //        return m_objCurrentContext.m_ObjPatientManager.m_objGetPatientByLikeBedNO_InArea(((clsInPatientArea)(this.m_cboArea.SelectedItem)).m_strAreaNewID, m_txtBedNO.Text);
            //}
            //else if (this.m_cboDept.SelectedItem != null)
            //{
            //    if (m_blnGetAllBedNo)
            //        return m_objCurrentContext.m_ObjPatientManager.m_objGetPatientByLikeBedNO_NoArea(((clsDepartment)(this.m_cboDept.SelectedItem)).m_strDeptNewID, "");
            //    else
            //        return m_objCurrentContext.m_ObjPatientManager.m_objGetPatientByLikeBedNO_NoArea(((clsDepartment)(this.m_cboDept.SelectedItem)).m_strDeptNewID, m_txtBedNO.Text);
            //}
            string strId = "";
            if (this.m_cboArea.SelectedItem != null)
                strId = ((clsInPatientArea)(this.m_cboArea.SelectedItem)).m_strAreaNewID;
            else if (this.m_cboDept.SelectedItem != null)
                strId = ((clsDepartment)(this.m_cboDept.SelectedItem)).m_strDeptNewID;
            com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objServ = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
            clsEmrBed_VO[] objResultArr = null;
            long lngRes = 0;
            if (m_blnGetAllBedNo)
                lngRes = objServ.m_lngGetInBedInfo(strId, false, out objResultArr);
            else
                lngRes = objServ.m_lngGetBedInfoLikeBedCode(strId, false, m_txtBedNO.Text.Trim(), out objResultArr);
            if (lngRes > 0 && objResultArr != null)
            {
                clsPatient[] objPatientArr = new clsPatient[objResultArr.Length];
                for (int i = 0; i < objResultArr.Length; i++)
                    objPatientArr[i] = new clsPatient(objResultArr[i]);
                return objPatientArr;
            }
            return null;
        }

        private void m_lsvInPatientID_DoubleClick(object sender, System.EventArgs e)
        {
            if (m_lsvInPatientID.SelectedItems.Count <= 0)
                return;

            clsPatient objPatient = (clsPatient)m_lsvInPatientID.SelectedItems[0].Tag;
            #region ת��VO
            //����com.digitalwave.BEDExplorer.frmHRPExplorer.objpCurrentPatient
            //��Ϊֻ��Ҫͨ��m_strINPATIENTID_CHR������ɵ�clspatient
            //����ʹ��
            clsHospitalManagerDomain objMain = new clsHospitalManagerDomain();
            clsEmrInBedPatient_VO objPatientTemp;
            if (m_cboArea.Text.Trim().Length == 0)
                objMain.m_lngGetSpecialMinPatinetInfoByDeptID(objPatient.m_StrPatientID, out objPatientTemp);
            else
                objMain.m_lngGetSpecialMinPatinetInfo(objPatient.m_StrPatientID, out objPatientTemp);
            frmHRPExplorer.objpCurrentPatient = objPatientTemp;

            if (objPatient.m_strDeptNewID == null)
                objPatient.m_strDeptNewID = ((clsDepartment)m_cboDept.SelectedItem).m_strDeptNewID;
            clsEmrDept_VO objDeptNew;
            objMain.m_lngGetSpecialDeptInfo(objPatient.m_strDeptNewID, out objDeptNew);
            frmHRPExplorer.objpCurrentDepartment = objDeptNew;

            if (objPatient.m_strAreaNewID == null)
            {
                if (m_cboArea.GetItemsCount() > 0)
                {
                    objPatient.m_strAreaNewID = ((clsInPatientArea)m_cboArea.SelectedItem).m_strAreaNewID;
                }
                else
                    objPatient.m_strAreaNewID = "";
            }
            clsEmrDept_VO objAreNew;
            objMain.m_lngGetSpecialAreaInfo(objPatient.m_strAreaNewID, out objAreNew);
            frmHRPExplorer.objpCurrentArea = objAreNew;
            #endregion
            m_mthSetPatientInfo(objPatient);
            MDIParent.s_ObjCurrentPatient = objPatient;
            m_lsvInPatientID.Visible = false;
        }

        /// <summary>
        /// �����겡�˵�������Ϣ���궨λ
        /// </summary>
        protected virtual void m_mthSetFocusAfterSetPatientInfo()
        {
            this.m_txtBedNO.Focus();
        }

        /// <summary>
        /// ���ò��˵�������Ϣ
        /// </summary>
        /// <param name="p_objSelectedPatient">����</param>
        protected void m_mthSetPatientInfo(clsPatient p_objSelectedPatient)
        {
            string strTempBedCode = p_objSelectedPatient.m_strBedCode;
            if (p_objSelectedPatient == null /*|| p_objSelectedPatient.m_ObjPeopleInfo == null*/)
            {
                m_mthShowNoPatient();
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            clsPatient p_objSelectedPatient2;
            p_objSelectedPatient2 = p_objSelectedPatient;

            string strTempBedCode1 = p_objSelectedPatient2.m_strBedCode;
            m_objBaseCurrentPatient = p_objSelectedPatient;
            //���ݼ���ʱ��Ҫ�õ���ǰѡ�����Ժʱ�䣬������������Ժʱ�䣬����δ�踳ֵ
            //			if(m_objBaseCurrentPatient.m_DtmSelectedInDate == DateTime.MinValue)
            //				m_objBaseCurrentPatient.m_DtmSelectedInDate=m_objBaseCurrentPatient.m_DtmLastInDate;
            MDIParent.s_ObjCurrentPatient = m_objBaseCurrentPatient;
            //m_mthIsReadOnly();
            //m_mthSetPatientBaseInfo(p_objSelectedPatient);

            if (this.GetType().Name == "frmOutHospital" || this.GetType().Name == "frmDeathRecord" || this.GetType().Name == "frmDeathCaseDiscuss")
            {
                m_mthSetPatientInHospitalDate(p_objSelectedPatient);
            }

            //m_blnNeedCheckArchive = false;
            if (!(this is frmDiseaseTrackBase) && !(this is frmThreeMeasureRecordGN) && !(this is frmThreeMeasureRecord))
            {
                m_mthSetPatientFormInfo(p_objSelectedPatient2);//�̳���frmDiseaseTrackBase�Ĵ�����m_mthPerformSessionChanged�����˲���
            }
            //m_blnNeedCheckArchive = true;

            m_mthSetFocusAfterSetPatientInfo();

            if (m_objBaseCurrentPatient == null)//��Ϊִ�����������������ܻ�ı䵱ǰֵ
                m_objBaseCurrentPatient = p_objSelectedPatient;

            m_blnPatientSelected = true;
            if (this.GetType().Name != "frmInPatientCaseHistoryArchiving")
            {
                string strTimeRemaing = null;
                bool blnIsReadOnly = false;
                if (m_objBaseCurrentPatient == null)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }
                //if (new clsInPatientArchivingDomain().lngIsReadOnly(m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyy-MM-dd HH:mm:ss"), out blnIsReadOnly, out strTimeRemaing) <= 0)
                //{
                //    this.Cursor = Cursors.Default;
                //    return;
                //}
                //else
                //{
                //m_mthPromtForArchiving(blnIsReadOnly, strTimeRemaing);
                //}
            }
            this.Cursor = Cursors.Default;

            //��ǰ����������¼״̬��DataGrid��ʽ�ļ�¼������Ҫ��
            if (!(this is frmRecordsBase) && !(this is frmDiseaseTrackBase))
                MDIParent.m_mthChangeFormText(this, m_enmFormEditStatus);

            m_mthAddFormStatusForClosingSave();

            if (p_objSelectedPatient.m_IntCharacter == 1)
            {
                m_mthSetIfCanSelectPatient(false);
            }
        }

        /// <summary>
        /// �鵵��ʾ
        /// </summary>
        /// <param name="p_blnIfReadOnly"></param>
        /// <param name="p_strTimeRemaing"></param>
        protected virtual void m_mthPromtForArchiving(bool p_blnIfReadOnly, string p_strTimeRemaing)
        {
            if (p_blnIfReadOnly)
            {
                //clsPublicFunction.ShowInformationMessageBox("�˲��˵����в���Ϊֻ���������޸ġ�");
            }
            else if (p_strTimeRemaing != null && p_strTimeRemaing.Trim().Length != 0)
            {
                clsPublicFunction.ShowInformationMessageBox("�˲��˵����в�����" + p_strTimeRemaing + "���Զ��鵵����Ҫ�޸���ע��ʱ�䡣");
            }
        }

        private MDIParent.enmFormEditStatus m_enmFormEditStatus = MDIParent.enmFormEditStatus.AddNew;
        protected MDIParent.enmFormEditStatus m_EnmFormEditStatus
        {
            set
            {
                m_enmFormEditStatus = value;
            }
        }

        /// <summary>
        /// ��¼���ô��嵱ǰ״̬�����ڴ���ر�ʱ�б�����ʾ���������Ҫ������ʾ�����ظú�����
        /// </summary>
        protected virtual void m_mthAddFormStatusForClosingSave()
        {
            //��¼���ô��嵱ǰ״̬
            MDIParent.s_ObjSaveCue.m_mthAddFormStatus(this);
        }

        /// <summary>
        /// �����¼���ݸı䣬�����¼��
        /// </summary>
        protected void m_mthRecordChangedToSave()
        {
            //���洰���¼
            MDIParent.s_ObjSaveCue.m_mthHandleRecordAfterSelect(this);
        }

        /// <summary>
        /// ���ò��˵Ļ���סԺ��Ϣ���ɸ����ṩ�µ�ʵ�֣�
        /// </summary>
        /// <param name="p_objSelectedPatient">����</param>
        protected virtual void m_mthSetPatientBaseInfo(clsPatient p_objSelectedPatient)
        {
            if (p_objSelectedPatient.m_ObjPeopleInfo == null)
            {
                m_mthShowNoPatient();
                return;
            }
            string strTempBedCode = p_objSelectedPatient.m_strBedCode;
            //������ص��������Է���m_cboArea��ֵ�󴥷���SelectedIndexChanged�¼�
            m_blnCanTextChanged = false;

            if (p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastSessionInfo != null)
            {
                if (p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastDeptInfo == null)
                    return;
                //���
                m_cboDept.ClearItem();

                //��ȡ����
                string str1 = p_objSelectedPatient.m_strDeptNewID;
                string str2;
                clsHospitalManagerDomain objDomain = new clsHospitalManagerDomain();
                clsEmrDept_VO objDeptNew;
                objDomain.m_lngGetSpecialDeptInfo(str1, out objDeptNew);
                str1 = objDeptNew.m_strSHORTNO_CHR.Trim();
                str2 = objDeptNew.m_strDEPTNAME_VCHR.Trim();
                string str11 = objDeptNew.m_strDEPTID_CHR.Trim();
                clsDepartment objDeptTemp = new clsDepartment(str1, str2);
                //ת��ʹ�ã��±��shortno���ɱ��ID�������¼�һ���ֶα����±�ID
                objDeptTemp.m_strDeptNewID = str11;
                m_cboDept.AddItem(objDeptTemp);
                m_cboDept.SelectedIndex = -1;
                m_cboDept.SelectedIndex = 0;

                //��ȡ����
                int intCount = m_cboArea.GetItemsCount();
                if (intCount > 0)
                {
                    for (int i = 0; i < intCount; i++)
                    {
                        if (((clsInPatientArea)m_cboArea.GetItem(i)).m_strAreaNewID == p_objSelectedPatient.m_strAreaNewID)
                        {
                            m_cboArea.SelectedIndex = i;
                            break;
                        }
                    }
                }
                //m_cboArea.ClearItem();
                //string str3 = p_objSelectedPatient.m_strAreaNewID;
                //if (str3 != null && str3.Trim().Length != 0)//������Ϊ��
                //{
                //    string str4;
                //    clsEmrDept_VO objAreNew;
                //    objDomain.m_lngGetSpecialAreaInfo(str3, out objAreNew);
                //    str3 = objAreNew.m_strSHORTNO_CHR;
                //    str4 = objAreNew.m_strDEPTNAME_VCHR;
                //    clsInPatientArea objInPatientArea = new clsInPatientArea(str3, str4, objAreNew.m_strDEPTID_CHR);
                //    //ת��ʹ�ã��±��shortno���ɱ��ID�������¼�һ���ֶα����±�ID
                //    objInPatientArea.m_strAreaNewID = objAreNew.m_strDEPTID_CHR;
                //    m_cboArea.AddItem(objInPatientArea);
                //    m_cboArea.SelectedIndex = 0;
                //}


                m_txtBedNO.Text = p_objSelectedPatient.m_strBedCode;

                #region RegionName
                //m_cboArea.Text = p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastAreaInfo.m_ObjArea.m_StrAreaName;
                //m_txtBedNO.Text=p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName;
                #endregion

            }
            else
            {
                m_txtBedNO.Text = "";
            }
            txtInPatientID.Text = p_objSelectedPatient.m_StrHISInPatientID;
            m_txtPatientName.Text = p_objSelectedPatient.m_ObjPeopleInfo.m_StrFirstName;
            try
            {
                lblSex.Text = p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_ObjPeopleInfo.m_StrSex.Trim();
                lblAge.Text = p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_ObjPeopleInfo.m_StrAge.Trim();
                //lblSex.Text = p_objSelectedPatient.m_ObjPeopleInfo.m_StrSex.Trim();
                //lblAge.Text = p_objSelectedPatient.m_ObjPeopleInfo.m_StrAge.Trim();

            }
            catch (Exception)
            {

                //throw;
            }
            lblAge.AutoSize = true;

            m_blnCanTextChanged = true;
        }

        /// <summary>
        /// ��ղ��˻���סԺ��Ϣ�Ľ��棨�ɸ����ṩ�µ�ʵ�֣�
        /// </summary>
        protected virtual void m_mthClearPatientBaseInfo()
        {
            m_blnCanTextChanged = false;

            txtInPatientID.Text = "";
            m_txtPatientName.Text = "";
            lblSex.Text = "";
            lblAge.Text = "";
            m_cboArea.Text = "";
            m_txtBedNO.Text = "";

            m_objBaseCurrentPatient = null;

            m_blnPatientSelected = false;

            m_blnCanTextChanged = true;
        }


        /// <summary>
        /// ���ò��˱���Ϣ�����븲��
        /// </summary>
        /// <param name="p_objSelectedPatient">����</param>
        protected virtual void m_mthSetPatientFormInfo(clsPatient p_objSelectedPatient)
        {
            //			throw new Exception("û��ʵ�� m_mthSetPatientFormInfo ����");
        }
        /// <summary>
        /// �����ò��˵Ļ�����Ϣ����������Ҫ��ʾ���˵Ļ�����Ϣ�븲��
        /// </summary>
        /// <param name="p_objSelectedPatient">����</param>
        protected virtual void m_mthOnlySetPatientInfo(clsPatient p_objSelectedPatient)
        {
            //lblSex.Text = p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_ObjPeopleInfo.m_StrSex.Trim();
            //lblAge.Text = p_objSelectedPatient.m_ObjInBedInfo.m_ObjLastSessionInfo.m_ObjPeopleInfo.m_StrAge.Trim();
        }

        /// <summary>
        /// �Ƿ��������ģ��
        /// </summary>
        protected bool m_blnHaveAssociateTemplate;

        /// <summary>
        /// �Զ������벡����������װģ��
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        protected void m_mthSetSpecialPatientTemplateSet(clsPatient p_objSelectedPatient)
        {
            if (p_objSelectedPatient == null || m_ObjCurrentEmrPatientSession == null)
            {
                m_mthShowNoPatient();
                return;
            }
            this.Cursor = Cursors.WaitCursor;

            #region old

            //			clsTemplatesetContentValue[][] objArr;
            //				objArr = new clsTemplateDomain().m_lngGetSpecialPatientTemplateSet(p_objSelectedPatient.m_StrInPatientID,p_objSelectedPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),this.Name,(int)enmAssociate.Disease);
            //
            //				if(objArr == null || objArr.Length <= 0)
            //				{
            //					m_blnHaveAssociateTemplate = false;
            //					this.Cursor = Cursors.Default;
            //					return;
            //				}
            //				else
            //				{
            //					m_blnHaveAssociateTemplate = true;
            //
            //					m_arlRTB.Clear();
            //					m_mthAddRichTextBox(this);
            //					RichTextBox[] RTBArr = (RichTextBox[])m_arlRTB.ToArray(typeof(RichTextBox));
            //
            //					for(int i=0;i<objArr.Length;i++)
            //					{
            //						#region �滻���ݸ�������
            //						ArrayList arlTemp = new ArrayList();
            //						for(int i1=0;i1<objArr[i].Length;i1++)
            //							arlTemp.Add(objArr[i][i1].m_strContent);
            //						string[] strContentArr = (string[])arlTemp.ToArray(typeof(string));
            //						clsDataShareTool.s_mthReplaceDataShareValue(p_objSelectedPatient,strContentArr);
            //						#endregion
            //						for(int i1=0;i1<objArr[i].Length;i1++)
            //						{
            //							for(int j=0;j<RTBArr.Length;j++)
            //							{
            //								if(RTBArr[j]!=null && RTBArr[j].Name==objArr[i][i1].m_strControl_ID)
            //								{
            //									switch(RTBArr[j].GetType().Name)
            //									{
            //										case "RichTextBox":
            //											if(RTBArr[j].Text=="")
            //												RTBArr[j].Text = strContentArr[i1].TrimEnd();
            //											else 
            //												RTBArr[j].Text += "\r\n" + strContentArr[i1].TrimEnd();
            //											break;
            //										case "ctlRichTextBox":
            //											int intLength=((com.digitalwave.Utility.Controls.ctlRichTextBox)RTBArr[j]).Text.Length ;
            //											if(RTBArr[j].Text=="")
            //												((com.digitalwave.Utility.Controls.ctlRichTextBox)RTBArr[j]).m_mthInsertText (strContentArr[i1].TrimEnd(),intLength);
            //											else
            //												((com.digitalwave.Utility.Controls.ctlRichTextBox)RTBArr[j]).m_mthInsertText ("\r\n" + strContentArr[i1].TrimEnd(),intLength);
            //											break;
            //									}
            //								}
            //							}
            //						}
            //					}
            //				}
            #endregion

            m_mthLoadAndShowTemplateSet(p_objSelectedPatient);
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// �Զ��������������ƹ�������װģ��
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        protected void m_mthSetSpecialPatientTemplateSet(clsPatient p_objSelectedPatient, enmAssociate p_enmType)
        {
            if (p_objSelectedPatient == null || p_objSelectedPatient.m_ObjPeopleInfo == null)
            {
                m_mthShowNoPatient();
                return;
            }
            this.Cursor = Cursors.WaitCursor;

            clsTemplatesetContentValue[][] objArr;
            try
            {
                objArr = new clsTemplateDomain().m_lngGetSpecialPatientTemplateSet(p_objSelectedPatient.m_StrInPatientID, p_objSelectedPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), this.Name, (int)p_enmType);

                if (objArr == null || objArr.Length <= 0)
                {
                    m_blnHaveAssociateTemplate = false;
                    this.Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    m_blnHaveAssociateTemplate = true;

                    m_arlRTB.Clear();
                    m_mthAddRichTextBox(this);
                    RichTextBox[] RTBArr = (RichTextBox[])m_arlRTB.ToArray(typeof(RichTextBox));

                    for (int i = 0; i < objArr.Length; i++)
                    {
                        #region �滻���ݸ�������
                        ArrayList arlTemp = new ArrayList();
                        for (int i1 = 0; i1 < objArr[i].Length; i1++)
                            arlTemp.Add(objArr[i][i1].m_strContent);
                        string[] strContentArr = (string[])arlTemp.ToArray(typeof(string));
                        com.digitalwave.Emr.Utility.DataShare.clsDataShareReplace.s_mthReplaceDataShareValue(p_objSelectedPatient, ref strContentArr);
                        #endregion
                        for (int i1 = 0; i1 < objArr[i].Length; i1++)
                        {
                            for (int j = 0; j < RTBArr.Length; j++)
                            {
                                if (RTBArr[j] != null && RTBArr[j].Name == objArr[i][i1].m_strControl_ID)
                                {
                                    switch (RTBArr[j].GetType().FullName)
                                    {
                                        case "System.Windows.Forms.RichTextBox":
                                            if (RTBArr[j].Text == "")
                                                RTBArr[j].Text = strContentArr[i1].TrimEnd();
                                            else
                                                RTBArr[j].Text += "\r\n" + strContentArr[i1].TrimEnd();
                                            break;
                                        case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                                            int intLength = ((com.digitalwave.Utility.Controls.ctlRichTextBox)RTBArr[j]).Text.Length;
                                            if (RTBArr[j].Text == "")
                                                ((com.digitalwave.Utility.Controls.ctlRichTextBox)RTBArr[j]).m_mthInsertText(strContentArr[i1].TrimEnd(), intLength);
                                            else
                                                ((com.digitalwave.Utility.Controls.ctlRichTextBox)RTBArr[j]).m_mthInsertText("\r\n" + strContentArr[i1].TrimEnd(), intLength);
                                            break;
                                        case "com.digitalwave.controls.ctlRichTextBox":
                                            int intLength1 = ((com.digitalwave.controls.ctlRichTextBox)RTBArr[j]).Text.Length;
                                            if (RTBArr[j].Text == "")
                                                ((com.digitalwave.controls.ctlRichTextBox)RTBArr[j]).m_mthInsertText(strContentArr[i1].TrimEnd(), intLength1);
                                            else
                                                ((com.digitalwave.controls.ctlRichTextBox)RTBArr[j]).m_mthInsertText("\r\n" + strContentArr[i1].TrimEnd(), intLength1);
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    //					clsDataShareTool.s_mthSetDataShare(p_objSelectedPatient,RTBArr);

                }
            }
            catch
            {
            }

            this.Cursor = Cursors.Default;
        }

        private ArrayList m_arlRTB = new ArrayList();
        private void m_mthAddRichTextBox(Control p_ctl)
        {
            foreach (Control ctlSub in p_ctl.Controls)
            {
                switch (ctlSub.GetType().FullName)
                {
                    case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                        m_arlRTB.Add(ctlSub);
                        break;
                    case "com.digitalwave.controls.ctlRichTextBox":
                        m_arlRTB.Add(ctlSub);
                        break;
                    case "System.Windows.Forms.RichTextBox":
                        m_arlRTB.Add(ctlSub);
                        break;
                }

                if (ctlSub.HasChildren)
                {
                    m_mthAddRichTextBox(ctlSub);
                }
            }

        }


        /// <summary>
        /// ���ò�����Ժ���ڣ����븲��
        /// </summary>
        /// <param name="p_objSelectedPatient">����</param>
        protected virtual void m_mthSetPatientInHospitalDate(clsPatient p_objSelectedPatient)
        {
            //			throw new Exception("û��ʵ�� m_mthSetPatient ����");
        }

        private void frmHRPBaseForm_Load(object sender, System.EventArgs e)
        {
            if (DesignMode || this.Site != null) return;
            try
            {
                m_lsvInPatientID.Visible = false;
                m_lsvPatientName.Visible = false;
                m_lsvBedNO.Visible = false;
                m_cmdNewTemplate.Visible = false;
                m_lsvInPatientID.BringToFront();
                m_lsvPatientName.BringToFront();
                m_lsvBedNO.BringToFront();

                m_objHighLight.m_mthAddControlInContainer(this);

                m_ctlAreaPatientSelection.Focus();
                //��ӱԴ���ӣ����ڳ�ʼ��ģ��ؼ�
                //				m_objTempTool.m_mthInitTemplateControls (this);
                //if (m_blnNeedContextMenu)
                //    m_mthAddRichTemplateInContainer(this);
                //��ӱԴ���ӣ����ڱ�����װģ���֧�֣�2003-9-10 12:35
                //m_frmSaveTemplateset = new frmTemplatesetDialog(this);

                m_cmdPre.Visible = false;
                m_lsvBedNO.Left = m_txtBedNO.Left;
                m_lsvBedNO.Top = m_txtBedNO.Bottom;

                //���ڹ���������ؼ���Ŀ�¼�
                //				m_mthAssociateComboBoxItemEvent(this);

                m_mthInitJump(m_objJump);
            }
            catch
            {
                //				throw new Exception("HRPBaseForm Load Exception");
            }

        }

        /// <summary>
        /// �Ƿ����˺����ݸı��¼������븲��
        /// </summary>
        protected virtual bool m_BlnCanTextChanged
        {
            get
            {
                //throw new Exception("û��ʵ�� m_BlnCanTextChanged ����");
                return true;
            }
        }

        /// <summary>
        /// ���õ����岡��
        /// </summary>
        /// <param name="p_objPatient">����</param>
        public void m_mthSetPatient(clsPatient p_objPatient)
        {
            if (p_objPatient == null) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (m_ctlAreaPatientSelection.m_BlnIsInUse)
                {
                    m_ctlAreaPatientSelection.m_mthSetPatient(p_objPatient.m_strAreaNewID, p_objPatient.m_StrPatientID, p_objPatient.m_StrRegisterId);
                    com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient = m_ctlAreaPatientSelection.CurrentBed.m_objInbedPatient;
                }
                else
                    m_mthSetPatientInfo(p_objPatient);
            }
            catch (Exception ex)
            {
                new com.digitalwave.Utility.clsLogText().LogDetailError(ex, false);
            }
            finally { this.Cursor = Cursors.Default; }
        }

        /// <summary>
        /// ��ʾ���ݿ�������Ϣ���ɸ����ṩ�µ�ʵ�֣�
        /// </summary>
        protected virtual void m_mthShowDBError()
        {
            clsPublicFunction.ShowInformationMessageBox("�Բ������ݿ�����");
        }

        /// <summary>
        /// ��ʾ��¼�ѱ�ɾ����Ϣ���ɸ����ṩ�µ�ʵ�֣�
        /// </summary>
        protected virtual void m_mthShowRecordDeleted(string p_strDeleteUserID, string p_strDeleteTime)
        {
            if (p_strDeleteUserID == null || p_strDeleteUserID == "")
                return;
            if (p_strDeleteTime == null || p_strDeleteTime == "")
                return;
            string m_strDeleteTime;
            string m_strDeleteUserName;
            try
            {
                m_strDeleteUserName = new clsEmployee(p_strDeleteUserID).m_StrFirstName;
                m_strDeleteTime = DateTime.Parse(p_strDeleteTime).ToString("yyyy��MM��dd�� HH:mm:ss");
            }
            catch
            {
                return;
            }

            clsPublicFunction.ShowInformationMessageBox("�Բ��𣬸ü�¼�ѱ� " + m_strDeleteUserName + " �� " + m_strDeleteTime + " ɾ����");
        }

        /// <summary>
        /// ��ʾ��¼�ѱ������޸���Ϣ���ɸ����ṩ�µ�ʵ�֣�
        /// </summary>
        protected virtual bool m_bolShowRecordModified(string p_strModifyUserID, string p_strModifyTime)
        {
            //			clsPublicFunction.ShowInformationMessageBox("�Բ��𣬸ü�¼�ѱ������޸ģ�");
            if (p_strModifyUserID == null || p_strModifyUserID == "")
                return false;
            if (p_strModifyTime == null || p_strModifyTime == "")
                return false;
            string m_strModifyTime;
            string m_strModifyUserName;
            try
            {
                m_strModifyUserName = new clsEmployee(p_strModifyUserID).m_StrFirstName;
                m_strModifyTime = DateTime.Parse(p_strModifyTime).ToString("yyyy��MM��dd�� HH:mm:ss");
            }
            catch
            {
                return false;
            }
            if (clsPublicFunction.ShowQuestionMessageBox("�Բ��𣬸ü�¼�ѱ� " + m_strModifyUserName + " �� " + m_strModifyTime + " �޸ģ��Ƿ���¼�¼��") == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ��ʾ��¼�ѱ�����������Ϣ���ɸ����ṩ�µ�ʵ�֣�
        /// </summary>
        protected virtual void m_mthShowRecordTimeDouble()
        {
            clsPublicFunction.ShowInformationMessageBox("�Բ��𣬸�ʱ���Ѿ��������û����ɹ���¼������Ҫ�޸ģ���رյ�ǰ���壬�����´򿪣�");
        }

        //		/// <summary>
        //		/// ��ʾ�Ƿ��޸Ĵ˼�¼��Ϣ���ɸ����ṩ�µ�ʵ�֣�
        //		/// </summary>
        //		protected virtual bool m_bolShowIfModify()
        //		{
        //			if(clsPublicFunction.ShowQuestionMessageBox("�Ƿ��޸Ĵ˼�¼��Ϣ��") == DialogResult.Yes)
        //			{
        //				return true;
        //			}
        //			else
        //			{
        //				return false;
        //			}
        //		}

        /// <summary>
        /// ��ʾû�иò��ˣ��ɸ����ṩ�µ�ʵ�֣�
        /// </summary>
        protected virtual void m_mthShowNoPatient()
        {
            clsPublicFunction.ShowInformationMessageBox("�Բ���û�д˲��ˣ�");
        }

        /// <summary>
        /// ��ʾû�иò��ˣ��ɸ����ṩ�µ�ʵ�֣�
        /// </summary>
        protected virtual void m_mthShowNotPermitted()
        {
            clsPublicFunction.ShowInformationMessageBox("�Բ�������Ȩ�޲�����");
        }

        private readonly DateTime m_dtmEmptyDate = new DateTime(1900, 1, 1);

        protected void m_mthAddRichTextInfo(Control p_ctlTextBox)
        {
            if (p_ctlTextBox is com.digitalwave.Utility.Controls.ctlRichTextBox)
            {
                ((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlTextBox).m_evtMouseEnterDeleteText += new EventHandler(m_mthHandleMouseEnterDeleteText);
                ((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlTextBox).m_evtMouseEnterInsertText += new EventHandler(m_mthHandleMouseEnterInsertText);
                ((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlTextBox).MouseLeave += new EventHandler(m_mthHandleMouseLeaveControl);
                ((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlTextBox).m_IntCanModifyTime = clsEMR_StaticObject.s_IntCanModifyTime;
            }
            else if (p_ctlTextBox is com.digitalwave.controls.ctlRichTextBox)
            {
                ((com.digitalwave.controls.ctlRichTextBox)p_ctlTextBox).m_evtMouseEnterDeleteText += new EventHandler(m_mthHandleMouseEnterDeleteText);
                ((com.digitalwave.controls.ctlRichTextBox)p_ctlTextBox).m_evtMouseEnterInsertText += new EventHandler(m_mthHandleMouseEnterInsertText);
                ((com.digitalwave.controls.ctlRichTextBox)p_ctlTextBox).MouseLeave += new EventHandler(m_mthHandleMouseLeaveControl);
                ((com.digitalwave.controls.ctlRichTextBox)p_ctlTextBox).m_IntCanModifyTime = clsEMR_StaticObject.s_IntCanModifyTime;
            }

        }

        private void m_mthHandleMouseLeaveControl(object p_objSender, EventArgs p_objArg)
        {
            m_ttpTextInfo.RemoveAll();
        }

        private void m_mthHandleMouseEnterDeleteText(object p_objSender, EventArgs p_objArg)
        {
            string strName = "";
            DateTime dtmDeleteTime = DateTime.MinValue;
            if (p_objArg is com.digitalwave.Utility.Controls.clsDoubleStrikeThoughEventArg)
            {
                com.digitalwave.Utility.Controls.clsDoubleStrikeThoughEventArg objarg = p_objArg as com.digitalwave.Utility.Controls.clsDoubleStrikeThoughEventArg;
                strName = objarg.m_strUserName;
                dtmDeleteTime = objarg.m_dtmDeleteTime;
            }
            else if (p_objArg is com.digitalwave.controls.clsDoubleStrikeThoughEventArg)
            {
                com.digitalwave.controls.clsDoubleStrikeThoughEventArg objarg = p_objArg as com.digitalwave.controls.clsDoubleStrikeThoughEventArg;
                strName = objarg.m_strUserName;
                dtmDeleteTime = objarg.m_dtmDeleteTime;
            }
            //			clsDoubleStrikeThoughEventArg objArg = (clsDoubleStrikeThoughEventArg)p_objArg;

            string strInfo = "�û����� : " +
                strName + "\r\nɾ��ʱ�� : ";

            if (dtmDeleteTime != m_dtmEmptyDate && dtmDeleteTime != DateTime.MinValue)
            {
                strInfo += dtmDeleteTime.ToLongDateString() + " " + dtmDeleteTime.ToLongTimeString();
            }
            else
            {
                strInfo += "----��--��--�� --:--:--";
            }

            m_ttpTextInfo.SetToolTip((Control)p_objSender, strInfo);
        }

        private void m_mthHandleMouseEnterInsertText(object p_objSender, EventArgs p_objArg)
        {
            //			com.digitalwave.Utility.Controls.clsInsertEventArg objArg = (com.digitalwave.Utility.Controls.clsInsertEventArg)p_objArg;

            string strName = "";
            DateTime dtmInsertTime = DateTime.MinValue;
            int intUserSeq = -1;
            if (p_objArg is com.digitalwave.Utility.Controls.clsInsertEventArg)
            {
                com.digitalwave.Utility.Controls.clsInsertEventArg objarg = p_objArg as com.digitalwave.Utility.Controls.clsInsertEventArg;
                strName = objarg.m_strUserName;
                dtmInsertTime = objarg.m_dtmInsertTime;
                intUserSeq = objarg.m_intUserSeq;
            }
            else if (p_objArg is com.digitalwave.controls.clsInsertEventArg)
            {
                com.digitalwave.controls.clsInsertEventArg objarg = p_objArg as com.digitalwave.controls.clsInsertEventArg;
                strName = objarg.m_strUserName;
                dtmInsertTime = objarg.m_dtmInsertTime;
                intUserSeq = objarg.m_intUserSeq;
            }

            if (intUserSeq == 1)
            {
                return;
            }

            string strInfo = "�û����� : " +
                strName + "\r\n���ʱ�� : ";

            if (dtmInsertTime != m_dtmEmptyDate && dtmInsertTime != DateTime.MinValue)
            {
                strInfo += dtmInsertTime.ToLongDateString() + " " + dtmInsertTime.ToLongTimeString();
            }
            else
            {
                strInfo += "----��--��--�� --:--:--";
            }

            m_ttpTextInfo.SetToolTip((Control)p_objSender, strInfo);
        }

        private void frmHRPBaseForm_Click(object sender, System.EventArgs e)
        {
            m_mthSetCanScroll();
        }

        /// <summary>
        /// ʹ������Թ���
        /// </summary>
        protected void m_mthSetCanScroll()
        {
            //			m_lblForTitle.Focus();
            //			txtInPatientID.Focus();
            //			txtInPatientID.SelectionLength = 0;
        }

        /// <summary>
        /// 2003.4.11 Liu RongGuo  ---��ȡ·��
        /// </summary>
        /// <returns></returns>
        public string m_strGetFilePathHeader()
        {
            string[] strFilePathAll = Application.ExecutablePath.Split('\\');
            string strFilePathHeader = "";
            if (strFilePathAll != null)
                for (int i = 0; i < strFilePathAll.Length - 3; i++)
                    strFilePathHeader += strFilePathAll[i] + "\\\\";
            return strFilePathHeader;
        }

        /// <summary>
        /// �õ���ǰҪ���ҵĲ���סԺ��
        /// </summary>
        public void m_mthGetInPatientIDArr()
        {
            if (txtInPatientID.Text == "")
            {
                m_lsvInPatientID.Visible = false;
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            //clsPatient [] objPatientArr = m_objGetPatient();

            #region ����ϵͳ���
            clsPatient[] objPatientArr = null;
            if (m_blnSetGetPatientByDept)
                objPatientArr = m_objGetPatient();
            else
                objPatientArr = m_objGetPatientByInPatientID();
            #endregion ����ϵͳ���


            m_lsvInPatientID.Items.Clear();

            if (objPatientArr == null || objPatientArr.Length == 0)
            {
                this.Cursor = Cursors.Default;
                m_lsvInPatientID.Visible = false;
                //				m_mthClearAllInfo(this);//��ճ���ǰ�ؼ���������д�������.
                //				m_mthShowNoPatient();
                return;
            }

            for (int i = 0; i < objPatientArr.Length; i++)
            {
                ListViewItem lviPatient = new ListViewItem(
                    new string[]{
                                    objPatientArr[i].m_StrInPatientID,
                });
                lviPatient.Tag = objPatientArr[i];

                m_lsvInPatientID.Items.Add(lviPatient);
            }

            m_mthChangeListViewLastColumnWidth(m_lsvInPatientID);
            m_lsvInPatientID.Visible = true;

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// �õ���ǰҪ���ҵĲ�������
        /// </summary>
        private void m_mthGetPatientNameArr()
        {
            if (m_txtPatientName.Text == "")
            {
                m_lsvPatientName.Visible = false;
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            clsPatient[] objPatientArr = m_objGetPatientByPatientName();

            m_lsvPatientName.Items.Clear();

            if (objPatientArr == null || objPatientArr.Length == 0)
            {
                this.Cursor = Cursors.Default;
                m_lsvPatientName.Visible = false;
                m_mthClearAllInfo(this);//��ճ���ǰ�ؼ���������д�������.
                m_mthShowNoPatient();
                return;
            }

            for (int i = 0; i < objPatientArr.Length; i++)
            {
                ListViewItem lviPatient = new ListViewItem(
                    new string[]{
                                    objPatientArr[i].m_StrName,
                });
                lviPatient.Tag = objPatientArr[i];

                m_lsvPatientName.Items.Add(lviPatient);
            }

            m_mthChangeListViewLastColumnWidth(m_lsvPatientName);
            m_lsvPatientName.Visible = true;

            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// �õ���ǰҪ���ҵĲ��˴���
        /// </summary>
        private void m_mthGetBedNOArr()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                clsPatient[] objPatientArr = m_objGetPatientByBedNO();

                m_lsvBedNO.Items.Clear();

                if (objPatientArr == null || objPatientArr.Length == 0)
                {
                    this.Cursor = Cursors.Default;
                    m_lsvBedNO.Visible = false;
                    //m_mthClearAllInfo(this);
                    m_mthClearAllInfo(this);//��ճ���ǰ�ؼ���������д�������.
                    m_mthShowNoPatient();
                    return;
                }

                //��������ID����Name,��ʾΪ����Name����				
                for (int i = 0; i < objPatientArr.Length; i++)
                {
                    ListViewItem lviPatient = new ListViewItem(
                        new string[]{
                                        objPatientArr[i].m_strBedCode
                                    });
                    lviPatient.Tag = objPatientArr[i];

                    m_lsvBedNO.Items.Add(lviPatient);
                }

                m_mthChangeListViewLastColumnWidth(m_lsvBedNO);

                m_lsvBedNO.Visible = true;

            }
            catch (Exception exp)
            {
                string strError = exp.Message;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }






        }

        /// <summary>
        /// ����ʾ����������6ʱ����С���һ�еĿ�ȣ�����ʾ������,Jacky-2003-7-21
        /// </summary>
        /// <param name="p_lsvControl"></param>
        public void m_mthChangeListViewLastColumnWidth(ListView p_lsvControl)
        {
            clsPublicFunction.s_mthChangeListViewLastColumnWidth(p_lsvControl);
        }

        public void m_mthChangeListViewLastColumnWidth(ListView p_lsvControl, int p_intRows)
        {
            clsPublicFunction.s_mthChangeListViewLastColumnWidth(p_lsvControl, p_intRows);
        }

        private void m_mthEvent_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyValue)
            {// enter	//Arrow Up //Arrow Down	
                case 13:// enter	

                    if (((Control)sender).Name == "txtInPatientID")
                    {
                        if (!txtInPatientID.ReadOnly)
                        {
                            m_mthGetInPatientIDArr();
                            if (m_lsvInPatientID.Items.Count == 1 && txtInPatientID.Text.Length == 7)
                            {
                                //								m_lsvInPatientID.Items[0].Selected=true;
                                //								m_mthSetPatientInfo((clsPatient)m_lsvInPatientID.SelectedItems[0].Tag);
                                //								m_lsvInPatientID.Visible = false;
                                break;
                            }
                        }
                    }
                    else if (((Control)sender).Name == "m_lsvInPatientID")
                    {
                        if (m_lsvInPatientID.SelectedItems.Count == 1)
                        {
                            //							m_mthSetPatientInfo((clsPatient)m_lsvInPatientID.SelectedItems[0].Tag);
                            //							m_lsvInPatientID.Visible = false;
                            m_lsvInPatientID_DoubleClick(null, null);
                            break;
                        }
                    }

                    //					else if( ((Control)sender).Name=="m_txtPatientName")
                    //					{
                    //						if(!m_txtPatientName.ReadOnly)
                    //						{
                    //							m_mthGetPatientNameArr();
                    //							if(m_lsvPatientName.Items.Count==1 && m_txtPatientName.Text==m_lsvPatientName.Items[0].SubItems[0].Text)
                    //							{
                    //								m_lsvPatientName.Items[0].Selected=true;
                    //								m_mthSetPatientInfo((clsPatient)m_lsvPatientName.SelectedItems[0].Tag);
                    //								m_lsvPatientName.Visible = false;
                    //								break;
                    //							}
                    //						}
                    //					}
                    else if (((Control)sender).Name == "m_lsvPatientName")
                    {
                        if (m_lsvPatientName.SelectedItems.Count == 1)
                        {
                            m_mthSetPatientInfo((clsPatient)m_lsvPatientName.SelectedItems[0].Tag);
                            m_lsvPatientName.Visible = false;
                            break;
                        }
                    }

                    else if (((Control)sender).Name == "m_txtBedNO")
                    {
                        //if (!m_txtBedNO.ReadOnly)
                        //{
                        //m_mthGetBedNOArr();
                        //    if (m_lsvBedNO.Items.Count == 1 && m_txtBedNO.Text == m_lsvBedNO.Items[0].SubItems[0].Text)
                        //    {
                        //        m_lsvBedNO.Items[0].Selected = true;
                        //        m_lsvBedNO_DoubleClick(null, null);
                        //        //m_mthSetPatientInfo((clsPatient)m_lsvBedNO.SelectedItems[0].Tag);
                        //        m_lsvBedNO.Visible = false;
                        //    }
                        //}
                    }
                    else if (((Control)sender).Name == "m_lsvBedNO")
                    {
                        if (m_lsvBedNO.SelectedItems.Count == 1)
                        {
                            //m_mthSetPatientInfo((clsPatient)m_lsvBedNO.SelectedItems[0].Tag);
                            m_lsvBedNO.Items[0].Selected = true;
                            m_lsvBedNO_DoubleClick(null, null);
                            m_lsvBedNO.Visible = false;
                            break;
                        }
                    }

                    break;

                case 38://Arrow Up
                    if (((Control)sender).Name == "m_lsvInPatientID")
                    {
                        if (m_lsvInPatientID.Items.Count > 0 && m_lsvInPatientID.Items[0].Selected)
                            txtInPatientID.Focus();
                    }
                    else if (((Control)sender).Name == "m_lsvPatientName")
                    {
                        if (m_lsvPatientName.Items.Count > 0 && m_lsvPatientName.Items[0].Selected)
                            m_txtPatientName.Focus();
                    }
                    else if (((Control)sender).Name == "m_lsvBedNO")
                    {
                        if (m_lsvBedNO.Items.Count > 0 && m_lsvBedNO.Items[0].Selected)
                            m_txtBedNO.Focus();
                    }
                    break;
                case 40://Arrow Down				
                    if (((Control)sender).Name == "txtInPatientID")
                    {
                        m_mthGetInPatientIDArr();
                        //m_lsvInPatientID.Visible=true;						
                        if (m_lsvInPatientID.Visible && m_lsvInPatientID.Items.Count > 0)
                        {
                            m_lsvInPatientID.Focus();
                            m_lsvInPatientID.Items[0].Selected = true;
                            m_lsvInPatientID.Items[0].Focused = true;
                        }

                    }

                    //					else if( ((Control)sender).Name=="m_txtPatientName")
                    //					{
                    //						m_mthGetPatientNameArr();
                    //						//m_lsvPatientName.Visible=true;						
                    //						if( m_lsvPatientName.Items.Count>0)
                    //						{
                    //							m_lsvPatientName.Focus();
                    //							m_lsvPatientName.Items[0].Selected=true;
                    //							m_lsvPatientName.Items[0].Focused=true;
                    //						}						
                    //					}
                    else if (((Control)sender).Name == "m_txtBedNO")
                    {
                        m_mthGetBedNOArr();
                        //m_lsvBedNO.Visible=true;						
                        if (m_lsvBedNO.Items.Count > 0)
                        {
                            m_lsvBedNO.Focus();
                            m_lsvBedNO.Items[0].Selected = true;
                            m_lsvBedNO.Items[0].Focused = true;
                        }
                    }
                    break;
            }
        }

        private void m_lsvBedNO_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                if (m_lsvBedNO.SelectedItems.Count <= 0)
                    return;

                if (m_objBaseCurrentPatient != null && m_dlgHandleSaveBeforePrint() == DialogResult.Cancel)
                    return;

                if (m_blnCheckRecordBase(this))
                    return;

                clsPatient objPatient = (clsPatient)m_lsvBedNO.SelectedItems[0].Tag;

                string strTemp = objPatient.m_StrPatientID;
                string strTempDeptID = objPatient.m_strDeptNewID;
                string strTempAreaID = objPatient.m_strAreaNewID;
                string strBedCode = objPatient.m_strBedCode;

                if (m_blnCheckSamePatientForm(objPatient.m_StrInPatientID))
                    return;

                #region ת��VO
                //����com.digitalwave.BEDExplorer.frmHRPExplorer.objpCurrentPatient
                //��Ϊֻ��Ҫͨ��m_strINPATIENTID_CHR������ɵ�clspatient
                //����ʹ��
                clsHospitalManagerDomain objMain = new clsHospitalManagerDomain();
                clsEmrInBedPatient_VO objPatientTemp;
                if (m_cboArea.Text.Trim().Length == 0)
                    objMain.m_lngGetSpecialMinPatinetInfoByDeptID(strTemp, out objPatientTemp);
                else
                    objMain.m_lngGetSpecialMinPatinetInfo(strTemp, out objPatientTemp);
                frmHRPExplorer.objpCurrentPatient = objPatientTemp;

                clsEmrDept_VO objDeptNew;
                objMain.m_lngGetSpecialDeptInfo(strTempDeptID, out objDeptNew);
                frmHRPExplorer.objpCurrentDepartment = objDeptNew;

                clsEmrDept_VO objAreNew;
                objMain.m_lngGetSpecialAreaInfo(strTempAreaID, out objAreNew);
                frmHRPExplorer.objpCurrentArea = objAreNew;
                #endregion

                m_mthSetPatientInfo(objPatient);
                //MDIParent.s_ObjCurrentPatient = objPatient;


                m_lsvBedNO.Visible = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private void m_lsvPatientName_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                if (m_lsvPatientName.SelectedItems.Count <= 0)
                    return;

                m_mthSetPatientInfo((clsPatient)m_lsvPatientName.SelectedItems[0].Tag);
                m_lsvPatientName.Visible = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        /// <summary>
        /// ��ճ���ǰ�ؼ���������д�������,(�ɸ����ṩ�µ�ʵ��)
        /// </summary>
        /// <param name="p_ctlControl"></param>
        /// <param name="p_blnReadOnly"></param>
        protected virtual void m_mthClearAllInfo(Control p_ctlControl)
        {
            try
            {
                string strTypeName = p_ctlControl.GetType().Name;
                if (strTypeName == "ctlRichTextBox")
                {
                    if (p_ctlControl is iCare.CustomForm.ctlRichTextBox)//�Զ�����е�cltRichTextBox
                        ((iCare.CustomForm.ctlRichTextBox)p_ctlControl).Text = "";
                    else if (p_ctlControl is com.digitalwave.Utility.Controls.ctlRichTextBox)
                        ((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlControl).m_mthClearText();
                    else if (p_ctlControl is com.digitalwave.controls.ctlRichTextBox)
                        ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_mthClearText();
                }
                else if (strTypeName == "ctlBorderTextBox" && p_ctlControl.Name != "txtInPatientID" && p_ctlControl.Name != "m_txtPatientName" && p_ctlControl.Name != "m_txtBedNO")
                    ((ctlBorderTextBox)p_ctlControl).Text = "";
                else if (strTypeName == "TreeView")
                {
                    if (((TreeView)p_ctlControl).Nodes.Count > 0)
                        ((TreeView)p_ctlControl).Nodes[0].Nodes.Clear();
                }
                else if (strTypeName == "ListView")
                    ((ListView)p_ctlControl).Items.Clear();
                else if (strTypeName == "DateTimePicker")
                    ((DateTimePicker)p_ctlControl).Value = DateTime.Now;

                if (p_ctlControl.HasChildren && strTypeName != "DataGrid" && strTypeName != "DateTimePicker" && strTypeName != "ctlTimePicker")
                {
                    foreach (Control subcontrol in p_ctlControl.Controls)
                    {
                        m_mthClearAllInfo(subcontrol);
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        /// <summary>
        /// ���ô����пؼ���ֻ������,
        /// </summary>
        /// <param name="p_ctlControl"></param>
        /// <param name="p_blnReadOnly"></param>
        protected virtual void m_mthSetControlReadOnly(Control p_ctlControl, bool p_blnReadOnly)
        {
            #region ���ô����пؼ���ֻ������,Jacky-2003-6-11
            string strTypeName = p_ctlControl.GetType().Name;
            if (strTypeName == "ctlRichTextBox")
            {
                if (p_ctlControl is iCare.CustomForm.ctlRichTextBox)//�Զ�����е�cltRichTextBox
                    ((iCare.CustomForm.ctlRichTextBox)p_ctlControl).ReadOnly = p_blnReadOnly;
                else if (p_ctlControl is com.digitalwave.Utility.Controls.ctlRichTextBox)
                    ((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlControl).m_BlnReadOnly = p_blnReadOnly;
                else if (p_ctlControl is com.digitalwave.controls.ctlRichTextBox)
                    ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_BlnReadOnly = p_blnReadOnly;
            }
            else if (strTypeName == "ctlBorderTextBox" && p_ctlControl.Name != "txtInPatientID" && p_ctlControl.Name != "m_txtBedNO" && p_ctlControl.Name != "m_txtPatientName")
                ((ctlBorderTextBox)p_ctlControl).ReadOnly = p_blnReadOnly;
            else if (strTypeName == "RichTextBox")
                ((RichTextBox)p_ctlControl).ReadOnly = p_blnReadOnly;
            else if (strTypeName == "DataGrid")
                ((DataGrid)p_ctlControl).ReadOnly = p_blnReadOnly;
            else if (strTypeName == "CheckBox" || strTypeName == "RadioButton")
                p_ctlControl.Enabled = !p_blnReadOnly;
            else if (strTypeName == "ctlComboBox" && p_ctlControl.Name != "m_cboDept" && p_ctlControl.Name != "m_cboArea")
                ((ctlComboBox)p_ctlControl).Enabled = !p_blnReadOnly;

            if (p_ctlControl.HasChildren && strTypeName != "DataGrid")
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthSetControlReadOnly(subcontrol, p_blnReadOnly);
                }
            }
            #endregion
        }

        private void m_cboArea_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            MDIParent.s_ObjCurrInPatientArea = (clsInPatientArea)m_cboArea.SelectedItem;
            m_mthSetOutPatientRevisitList();
        }
        protected virtual void m_mthSetOutPatientRevisitList()
        {
        }

        private void m_mthClearPatientBaseInfoButArea()
        {
            m_blnCanTextChanged = false;

            //txtInPatientID.Text = "";
            //m_txtPatientName.Text = "";
            //lblSex.Text = "";
            //lblAge.Text = "";
            //m_txtBedNO.Text = "";

            m_objBaseCurrentPatient = null;

            m_blnPatientSelected = false;

            m_blnCanTextChanged = true;
        }

        private void m_cboDept_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                m_objTemplateClient = null;
                this.Cursor = Cursors.WaitCursor;
                clsHospitalManagerDomain objDomain = new clsHospitalManagerDomain();
                clsEmrDept_VO[] objAreaInfoArr = null;
                long lngRes = objDomain.m_lngGetAreaInfo(((clsDepartment)(m_cboDept.SelectedItem)).m_strDeptNewID, out objAreaInfoArr);
                if (objAreaInfoArr != null)
                {
                    m_cboArea.ClearItem();
                    clsInPatientArea objSelect = null;
                    for (int i = 0; i < objAreaInfoArr.Length; i++)
                    {
                        //ת��Ϊ�ɵ�
                        clsInPatientArea objAreaTemp = new clsInPatientArea(objAreaInfoArr[i].m_strSHORTNO_CHR, objAreaInfoArr[i].m_strDEPTNAME_VCHR, objAreaInfoArr[i].m_strDEPTID_CHR);
                        //ת��ʹ�ã��±��shortno���ɱ��ID�������¼�һ���ֶα����±�ID
                        objAreaTemp.m_strAreaNewID = objAreaInfoArr[i].m_strDEPTID_CHR;
                        m_cboArea.AddItem(objAreaTemp);
                        if (i == 0)
                            objSelect = objAreaTemp;
                    }
                    m_cboArea.SelectedItem = objSelect;
                }

                MDIParent.s_ObjCurrDepartment = (clsDepartment)m_cboDept.SelectedItem;
                //			if(m_cboDept.GetItemsCount()>0)
                //			{
                //				com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment=(clsEmrDept_VO)m_cboDept.SelectedItem;
                //			}
                if (m_cboArea.GetItemsCount() > 0)
                {
                    //com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentArea=(clsEmrDept_VO)m_cboArea.SelectedItem;
                    MDIParent.s_ObjCurrInPatientArea = (clsInPatientArea)m_cboArea.SelectedItem;
                }


            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        protected bool m_blnCheckEmployeeSign(string p_strEmployeeID, string p_strEmployeeName)
        {
            //�ݲ���Ա��ǩ����֤
            return true;


            //			frmCheckSign objCheck = new frmCheckSign(p_strEmployeeID,p_strEmployeeName);
            //
            //			objCheck.ShowDialog(this);
            //
            //			if(objCheck.m_LngRes > 0 && objCheck.m_BlnIsPass)
            //			{
            //				return true;
            //			}
            //			else if(objCheck.m_LngRes > 0 && !objCheck.m_BlnIsPass)
            //			{
            //				clsPublicFunction.ShowInformationMessageBox("��֤ʧ�ܣ���Ա������ǩ����");
            //				return false;
            //			}
            //			else
            //			{
            //				return false;
            //			}
        }

        #region ���
        public long m_lngApprove()
        {
            #region old
            //			if(!m_BlnCanApprove || m_StrCurrentOpenDate=="")
            //				return 1;
            //			
            //			string strFormID = ((int)m_enmGetPrivilegeSF()).ToString();
            //
            //			long lngEff=0;
            //			lngEff = new clsApprove_FlowDomain().lngApproveDocument(clsSystemContext.s_ObjCurrentContext.m_ObjEmployee.m_StrEmployeeID,strFormID,m_objBaseCurrentPatient.m_StrInPatientID,m_objBaseCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),m_StrCurrentOpenDate,((clsDepartment)this.m_cboDept.SelectedItem).m_StrDeptID,ref lngEff);
            //			
            //			#region ���ݽ������ͬ�Ĵ���
            //			switch((enmApproveResult)lngEff)
            //			{
            //				case enmApproveResult.DB_Succeed:					
            //					clsPublicFunction.ShowInformationMessageBox("��˳ɹ�!");
            //					break;
            //				case enmApproveResult.System_Not_Define:					
            //					clsPublicFunction.ShowInformationMessageBox("ϵͳû�ж���õ���������̣�Ӧ�������ݿ��ж��壩!");
            //					break;
            //				case enmApproveResult.Document_Has_Been_Finished:					
            //					clsPublicFunction.ShowInformationMessageBox("�˵��Ѿ��������󣬲������������!");
            //					break;
            //				case enmApproveResult.No_Purview:					
            //					clsPublicFunction.ShowInformationMessageBox("�Բ�������Ȩ�޲�������Ȩ��˴˵�!");
            //					break;
            //				case enmApproveResult.EmployeeID_Error:					
            //					clsPublicFunction.ShowInformationMessageBox("Ա���Ŵ���!");
            //					break;
            //				case enmApproveResult.Not_Found_Approve_Info:					
            //					clsPublicFunction.ShowInformationMessageBox("�Բ�������������ȱ���˹���\n�˵�Ŀǰ��û�б���˹�!");
            //					break;
            //				case enmApproveResult.Is_Top_Level:					
            //					clsPublicFunction.ShowInformationMessageBox("�Ѿ��˻ص�����һ��!");
            //					break;
            //				case enmApproveResult.Document_Has_Been_Deleted:					
            //					clsPublicFunction.ShowInformationMessageBox("�õ��Ѿ�ɾ��!");
            //					break;
            //				default:
            //					break;
            //			}
            //			#endregion ���ݽ������ͬ�Ĵ���
            //
            //			return 1;
            #endregion old

            string strCurrUserID = MDIParent.OperatorID; //��ǰ�û�
            string strCreateUserID = m_StrRecorder_ID; //�������û�

            string strCurrUserLevel = ""; //��ǰ�û���˼���
            string strCreateUserLevel = ""; //�������û���˼���
            string StrCurrentOpenDate = "";//��ǰ��¼����ʱ��

            int intCurrAppNo = 0;

            if (m_EnmAppType == enmApproveType.none)
                return -1;
            new clsApprove_FlowDomain().m_mthGetUserLevel(strCurrUserID, ref strCurrUserLevel, m_EnmAppType);
            new clsApprove_FlowDomain().m_mthGetUserLevel(m_StrRecorder_ID, ref strCreateUserLevel, m_EnmAppType);
            StrCurrentOpenDate = m_StrCurrentOpenDate;
            if (StrCurrentOpenDate.Trim().Length == 0)
                return -1;
            if (new clsApprove_FlowDomain().m_blnCanAuditing(Name, m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString(), StrCurrentOpenDate, strCurrUserLevel, strCreateUserLevel, ref intCurrAppNo, true) == 1)
            {
                new clsApprove_FlowDomain().m_mthExecAuditing(Name, m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString(), StrCurrentOpenDate, strCurrUserLevel);
                clsPublicFunction.ShowInformationMessageBox("��˳ɹ�!");
            }
            else
            {
                MessageBox.Show("������˴˵���", "��ʾ");
                return -1;
            }

            return 1;

            //			else if(m_EnmAppType=enmApproveType.CaseHistory)
            //			{
            //				m_mthGetUserLevel(strCurrUserID,strCurrUserLevel,1);
            //				m_mthGetUserLevel(m_StrRecorder_ID,strCreateUserLevel,1);
            //				if(m_blnCanAuditing(Name,1,MDIParent.s_ObjCurrentPatient.m_StrInPatientID,MDIParent.s_ObjCurrentPatient.m_DtmSelectedInDate,m_StrCurrentOpenDate,strCurrUserLevel,strCreateUserLevel))
            //				{
            //					m_mthExecAuditing(Name,MDIParent.s_ObjCurrentPatient.m_StrInPatientID,MDIParent.s_ObjCurrentPatient.m_DtmSelectedInDate,m_StrCurrentOpenDate,strCurrUserLevel);
            //				}
            //				else
            //				{
            //					MessageBox.Show ("û�����Ȩ�ޣ�","��ʾ");
            //					return -1;
            //				}
            //			}
            //			else if(m_EnmAppType=enmApproveType.Nurses)
            //			{
            //				m_mthGetUserLevel(strCurrUserID,strCurrUserLevel,2);
            //				m_mthGetUserLevel(m_StrRecorder_ID,strCreateUserLevel,2);
            //				if(m_blnCanAuditing(Name,2,MDIParent.s_ObjCurrentPatient.m_StrInPatientID,MDIParent.s_ObjCurrentPatient.m_DtmSelectedInDate,m_StrCurrentOpenDate,strCurrUserLevel,strCreateUserLevel))
            //				{
            //					m_mthExecAuditing(Name,MDIParent.s_ObjCurrentPatient.m_StrInPatientID,MDIParent.s_ObjCurrentPatient.m_DtmSelectedInDate,m_StrCurrentOpenDate,strCurrUserLevel);
            //				}
            //				else
            //				{
            //					MessageBox.Show ("û�����Ȩ�ޣ�","��ʾ");
            //					return -1;
            //				}
            //			}
        }

        public long m_lngUnApprove()
        {
            #region old
            //			if(!m_BlnCanApprove || m_StrCurrentOpenDate=="")
            //				return 1;
            //
            //			string strFormID = ((int)m_enmGetPrivilegeSF()).ToString();
            //
            //			long lngEff=0;
            //			lngEff = new clsApprove_FlowDomain().lngUntreadDocumentOneLevel(clsSystemContext.s_ObjCurrentContext.m_ObjEmployee.m_StrEmployeeID,strFormID,m_objBaseCurrentPatient.m_StrInPatientID,m_objBaseCurrentPatient.m_DtmLastInDate.ToString("yyyy-MM-dd HH:mm:ss"),m_StrCurrentOpenDate,((clsDepartment)this.m_cboDept.SelectedItem).m_StrDeptID,ref lngEff);
            //			
            //			#region ���ݽ������ͬ�Ĵ���
            //			switch((enmApproveResult)lngEff)
            //			{
            //				case enmApproveResult.DB_Succeed:					
            //					clsPublicFunction.ShowInformationMessageBox("����ɹ�!");
            //					break;
            //				case enmApproveResult.System_Not_Define:					
            //					clsPublicFunction.ShowInformationMessageBox("ϵͳû�ж���õ���������̣�Ӧ�������ݿ��ж��壩!");
            //					break;
            //				case enmApproveResult.Document_Has_Been_Finished:					
            //					clsPublicFunction.ShowInformationMessageBox("���Ѿ��������󣬲������������!");
            //					break;
            //				case enmApproveResult.No_Purview:					
            //					clsPublicFunction.ShowInformationMessageBox("�Բ�������Ȩ�޲�������Ȩ��˴˵�!");
            //					break;
            //				case enmApproveResult.EmployeeID_Error:					
            //					clsPublicFunction.ShowInformationMessageBox("Ա���Ŵ���!");
            //					break;
            //				case enmApproveResult.Not_Found_Approve_Info:					
            //					clsPublicFunction.ShowInformationMessageBox("�Բ�������������ȱ���˹���\n�˵�Ŀǰ��û�б���˹�!");
            //					break;
            //				case enmApproveResult.Is_Top_Level:					
            //					clsPublicFunction.ShowInformationMessageBox("�Ѿ��˻ص�����һ��!");
            //					break;
            //				case enmApproveResult.Document_Has_Been_Deleted:					
            //					clsPublicFunction.ShowInformationMessageBox("�õ��Ѿ�ɾ��!");
            //					break;
            //				default:
            //					break;
            //			}
            //			#endregion ���ݽ������ͬ�Ĵ���
            //
            //			return 1;
            #endregion old
            string strCurrUserID = MDIParent.OperatorID; //��ǰ�û�
            string strCreateUserID = m_StrRecorder_ID; //�������û�

            string strCurrUserLevel = ""; //��ǰ�û���˼���
            string strCreateUserLevel = ""; //�������û���˼���
            int intCurrAppNo = 0;
            long lngRet;

            if (m_EnmAppType == enmApproveType.none)
                return -1;
            new clsApprove_FlowDomain().m_mthGetUserLevel(strCurrUserID, ref strCurrUserLevel, m_EnmAppType);
            new clsApprove_FlowDomain().m_mthGetUserLevel(m_StrRecorder_ID, ref strCreateUserLevel, m_EnmAppType);

            if (new clsApprove_FlowDomain().m_blnCanAuditing(Name, m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString(), m_StrCurrentOpenDate, strCurrUserLevel, strCreateUserLevel, ref intCurrAppNo, true) != -1)
            {
                lngRet = new clsApprove_FlowDomain().m_lngBackAuditing(Name, m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString(), m_StrCurrentOpenDate, intCurrAppNo);
                if (lngRet == -99)
                    clsPublicFunction.ShowInformationMessageBox("��ǰ��¼��������!");
                else
                    clsPublicFunction.ShowInformationMessageBox("����ɹ�!");
            }
            else
            {
                MessageBox.Show("��������˵���", "��ʾ");
                return -1;
            }

            return 1;

        }

        public long m_lngDeleteDocument()
        {
            if (!m_BlnCanApprove || m_StrCurrentOpenDate == "")
                return 1;

            string strFormID = ((int)m_enmGetPrivilegeSF()).ToString();

            long lngEff = 0;
            lngEff = new clsApprove_FlowDomain().lngDeleteDocument(clsSystemContext.s_ObjCurrentContext.m_ObjEmployee.m_StrEmployeeID, strFormID, m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), m_StrCurrentOpenDate, ((clsDepartment)this.m_cboDept.SelectedItem).m_StrDeptID, ref lngEff);

            return 1;
        }

        public virtual bool lngCanYouDoIt()
        {

            string strCurrUserID = MDIParent.OperatorID; //��ǰ�û�
            string strCreateUserID = m_StrRecorder_ID; //�������û�

            string strCurrUserLevel = ""; //��ǰ�û���˼���
            string strCreateUserLevel = ""; //�������û���˼���
            string StrCurrentOpenDate = "";//��ǰ��¼����ʱ��

            int intCurrAppNo = 0;

            if (m_EnmAppType == enmApproveType.none)
            {
                return true;
            }
            else
            {
                new clsApprove_FlowDomain().m_mthGetUserLevel(strCurrUserID, ref strCurrUserLevel, m_EnmAppType);
                new clsApprove_FlowDomain().m_mthGetUserLevel(m_StrRecorder_ID, ref strCreateUserLevel, m_EnmAppType);

                if (int.Parse(strCurrUserLevel) >= int.Parse(strCreateUserLevel))
                {
                    StrCurrentOpenDate = m_StrCurrentOpenDate;
                    if (StrCurrentOpenDate.Trim().Length == 0)
                        return false;
                    new clsApprove_FlowDomain().m_blnCanAuditing(Name, m_objBaseCurrentPatient.m_StrInPatientID, m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString(), StrCurrentOpenDate, strCurrUserLevel, strCreateUserLevel, ref intCurrAppNo, false);
                    if (int.Parse(strCurrUserLevel) >= intCurrAppNo)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }


        }


        /// <summary>
        /// �Ƿ�֧����ˡ���Щ����������뵥�ǲ���Ҫ�ġ�
        /// </summary>
        protected virtual bool m_BlnCanApprove
        {
            get
            {
                return false;
            }
        }
        #endregion


        #region ������װģ��
        /// <summary>
        /// ������װģ��
        /// </summary>
        public void m_mthNewTemplate()
        {
            //this.m_frmSaveTemplateset.mthShowSaveDialog(m_strGetCurFormName(), false);

            if (m_ObjTemplateClient != null)
            {
                m_ObjTemplateClient.m_mthCreateTemplate();
            }
        }

        /// <summary>
        /// ������װģ�壬���ҶԻ����Ա�����Ϊ������
        /// </summary>
        public void m_mthNewTemplateWithThis()
        {
            //this.m_frmSaveTemplateset.mthShowSaveDialog(this.Name, this, false);
            if (m_ObjTemplateClient != null)
            {
                m_ObjTemplateClient.m_mthCreateTemplate();
            }
        }

        private void m_blnNewTemplate_Click(object sender, System.EventArgs e)
        {
            m_mthNewTemplate();
        }
        #endregion ������װģ��

        #region ���ɳ���ֵ
        /// <summary>
        /// ���ɳ���ֵ
        /// </summary>
        public void m_mthNewCommonUse()
        {
            //if (this.ActiveControl == null)
            //{
            //    clsPublicFunction.ShowInformationMessageBox("����ѡ��Ҫ���ɳ���ֵ�������");
            //    return;
            //}
            //this.m_frmSaveTemplateset.m_CtlCurrent = this.ActiveControl;
            //this.m_frmSaveTemplateset.mthShowSaveDialog(this.Name, true);
            if (m_ObjTemplateClient != null)
            {
                m_ObjTemplateClient.m_mthCreateCommonUseTemplate();
            }
        }

        /// <summary>
        /// ���ɳ���ֵ�����ҶԻ����Ա�����Ϊ������
        /// </summary>
        public void m_mthNewCommonUseWithThis()
        {
            //if (this.ActiveControl == null)
            //{
            //    clsPublicFunction.ShowInformationMessageBox("����ѡ��Ҫ���ɳ���ֵ�������", this);
            //    return;
            //}
            //this.m_frmSaveTemplateset.m_CtlCurrent = this.ActiveControl;
            //this.m_frmSaveTemplateset.mthShowSaveDialog(this.Name, this, true);
            if (m_ObjTemplateClient != null)
            {
                m_ObjTemplateClient.m_mthCreateCommonUseTemplate();
            }
        }
        #endregion

        #region ���ܲ�ѯ�Ķ�̬����
        /// <summary>
        /// ��������������Reflection��ñ����塣�����������Ƕ�λһ����Ч��¼����Ĳ�����
        /// </summary>
        /// <param name="p_frmInvoker">�򿪱�����Ĵ���</param>
        /// <param name="p_strInPatientID"></param>
        /// <param name="p_strInPatientDate"></param>
        /// <param name="p_strOpenDate"></param>
        public void m_mthLoadRecordByReflection(System.Windows.Forms.Form p_frmInvoker, string p_strInPatientID, string p_strInPatientDate, string p_strOpenDate)
        {
            this.MdiParent = p_frmInvoker.MdiParent;
            this.WindowState = FormWindowState.Maximized;
            this.Show();

            clsPatient objPatient = new clsPatient(p_strInPatientID);

            m_mthSetPatientInfo(objPatient);

            m_mthLoadRecord(p_strInPatientDate, p_strOpenDate);
        }

        /// <summary>
        /// �����ò��˺����ò���ָ���ļ�¼
        /// </summary>
        /// <param name="p_strInPatientDate">����סԺ����</param>
        /// <param name="p_strOpenDate">��¼����</param>
        protected virtual void m_mthLoadRecord(string p_strInPatientDate, string p_strOpenDate)
        {
        }
        #endregion ���ܲ�ѯ�Ķ�̬����

        private void m_cmdNext_Click(object sender, System.EventArgs e)
        {

            try
            {
                m_blnGetAllBedNo = true;
                m_mthGetBedNOArr();
                m_blnGetAllBedNo = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private void m_cmdPre_Click(object sender, System.EventArgs e)
        {
            clsPatient objPatient;
            string strAreaID = ((clsInPatientArea)m_cboArea.SelectedItem).m_StrAreaID;
            new clsDepartmentManager().m_lngGetPreviousPatientInfoInArea(strAreaID, m_txtBedNO.Text.Trim(), out objPatient);

            m_mthSetPatientInfo(objPatient);
        }

        #region �Բ���Ϊ��������
        /// <summary>
        /// ʹѡ���˵Ĺ�����Ч
        /// </summary>
        /// <param name="p_blnDisable">ʹ��Ч</param>
        public void m_mthDisableSelectPatient(bool p_blnDisable)
        {
            m_txtBedNO.ReadOnly = p_blnDisable;
            m_txtPatientName.ReadOnly = p_blnDisable;
            txtInPatientID.ReadOnly = p_blnDisable;

            m_cboArea.Enabled = !p_blnDisable;
            m_cboDept.Enabled = !p_blnDisable;
        }
        #endregion �Բ���Ϊ��������

        private void frmHRPBaseForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            m_pnlFocus.Location = new Point(e.X - 10, e.Y - 10);
            m_pnlFocus.Focus();
        }

        protected bool m_blnDirectPrint;
        /// <summary>
        /// �Ƿ�ֱ�Ӵ�ӡ
        /// </summary>
        public bool m_BlnDirectPrint
        {
            set
            {
                m_blnDirectPrint = value;
            }
        }

        public void m_mthSetBedFocus()
        {
            this.m_ctlAreaPatientSelection.Focus();
        }

        protected bool m_blnIfNewDeletedRecord = false;
        /// <summary>
        /// �Ƿ�����һ��ɾ����¼
        /// </summary>
        public bool m_BlnIfNewDeletedRecord
        {
            set
            {
                m_blnIfNewDeletedRecord = value;
            }
        }

        /// <summary>
        /// ��ȡ��ǰ����,�Զ�������⴦��
        /// </summary>
        /// <returns></returns>
        private string m_strGetCurFormName()
        {
            string strFormName = this.Name;
            if (this is iCare.CustomForm.frmCustomFormBase)
                strFormName = ((iCare.CustomForm.frmCustomFormBase)this).m_strGetCurFormName();
            return strFormName;
        }

        #region �������¼�
        /// <summary>
        /// �����������Ŀ�¼�
        /// </summary>
        /// <param name="p_ctlParent"></param>
        protected virtual void m_mthAssociateComboBoxItemEvent(Control p_ctlParent)
        {
            ctlComboBox cboOld = p_ctlParent as ctlComboBox;
            com.digitalwave.Controls.ctlComboBox cboNew = p_ctlParent as com.digitalwave.Controls.ctlComboBox;
            if (cboOld != null)
            {
                if (cboOld.m_blnEnableItemEventMenu == true)
                {
                    cboOld.DropDown += new System.EventHandler(m_mthComboBox_DropDown);
                    cboOld.evtAddItem += new System.EventHandler(m_mthComboBox_Additem);
                    cboOld.evtModifyItem += new System.EventHandler(m_mthComboBox_Modifyitem);
                    cboOld.evtDelItem += new System.EventHandler(m_mthComboBox_Deleteitem);
                }
            }
            else if (cboNew != null)
            {
                if (cboNew.m_BlnEnableItemEventMenu == true)
                {
                    cboNew.DropDown += new EventHandler(cboNew_DropDown);
                    cboNew.evtAddItem += new EventHandler(cboNew_evtAddItem);
                    cboNew.evtModifyItem += new EventHandler(cboNew_evtModifyItem);
                    cboNew.evtDelItem += new EventHandler(cboNew_evtDelItem);
                    cboNew.TextChanged += new EventHandler(cboNew_TextChanged);
                    cboNew.SelectedIndexChanged += new EventHandler(cboNew_SelectedIndexChanged);
                }
            }
        }

        void cboNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = ((com.digitalwave.Controls.ctlComboBox)sender).SelectedItem.ToString();
        }

        void cboNew_TextChanged(object sender, EventArgs e)
        {
            int i = ((com.digitalwave.Controls.ctlComboBox)sender).SelectedIndex;
        }

        /// <summary>
        /// �����������Ŀ
        /// ��ȡvo��deptid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mthComboBox_Additem(object sender, System.EventArgs e)
        {
            if (m_ObjCurrentArea == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����ѡ�����");
                return;
            }

            ctlComboBox cbo = ((MenuItem)sender).GetContextMenu().SourceControl as ctlComboBox;
            if (cbo == null)
                return;
            if (cbo.Text == "")
                return;
            clsComboBoxValue objValue = new clsComboBoxValue();
            //objValue.m_strDeptID = MDIParent.s_ObjDepartment.m_StrDeptID;
            objValue.m_strDeptID = m_ObjCurrentArea.m_strDEPTID_CHR;
            objValue.m_strTypeID = this.Name;
            objValue.m_strItemID = cbo.Name;
            objValue.m_strItemContent = cbo.Text;
            long lngRef = new clsComboBoxDomainOld().m_lngAddItemToDB(objValue);
            if (lngRef < 1)
                return;
            cbo.InsertItem(0, objValue.m_strItemContent);
            cbo.SelectedIndex = 0;

        }

        /// <summary>
        /// �������޸���Ŀ
        /// ��ȡvo��deptid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mthComboBox_Modifyitem(object sender, System.EventArgs e)
        {
            if (m_ObjCurrentArea == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����ѡ�����");
                return;
            }

            ctlComboBox cbo = ((MenuItem)sender).GetContextMenu().SourceControl as ctlComboBox;
            if (cbo == null)
                return;
            if (cbo.Text == "" || cbo.SelectedItem == null)
                return;
            clsComboBoxValue objValue;
            objValue = new clsComboBoxValue();
            //objValue.m_strDeptID = MDIParent.s_ObjDepartment.m_StrDeptID;
            objValue.m_strDeptID = m_ObjCurrentArea.m_strDEPTID_CHR;
            objValue.m_strTypeID = this.Name;
            objValue.m_strItemID = cbo.Name;
            objValue.m_strItemContent = cbo.SelectedItem.ToString();
            long lngRef = new clsComboBoxDomainOld().m_lngModifyItem(objValue, cbo.Text);
            if (lngRef < 1)
                return;
            string strText = cbo.Text;
            cbo.RemoveItem(cbo.SelectedItem);
            cbo.Update();
            cbo.InsertItem(0, strText);
            cbo.SelectedIndex = 0;
        }

        /// <summary>
        /// ������ɾ����Ŀ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mthComboBox_Deleteitem(object sender, System.EventArgs e)
        {
            if (m_ObjCurrentArea == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����ѡ�����");
                return;
            }

            ctlComboBox cbo = ((MenuItem)sender).GetContextMenu().SourceControl as ctlComboBox;
            if (cbo == null)
                return;
            if (cbo.Text == "" || cbo.SelectedItem == null)
                return;
            clsComboBoxValue objValue;
            objValue = new clsComboBoxValue();
            //objValue.m_strDeptID = MDIParent.s_ObjDepartment.m_StrDeptID;
            objValue.m_strDeptID = m_ObjCurrentArea.m_strDEPTID_CHR;
            objValue.m_strTypeID = this.Name;
            objValue.m_strItemID = cbo.Name;
            objValue.m_strItemContent = cbo.SelectedItem.ToString();
            long lngRef = new clsComboBoxDomainOld().m_lngDeleteItem(objValue);
            if (lngRef < 1)
                return;
            cbo.RemoveItem(cbo.SelectedItem);
            cbo.Update();
        }


        /// <summary>
        /// ����������Ŀ
        /// ���ݵ�ǰ����ID��ȡ
        /// </summary>
        /// <param name="p_cboSender"></param>
        private void m_mthSetComboBoxListItem(ctlComboBox p_cboSender)
        {
            if (p_cboSender == null)
                return;

            if (m_ObjCurrentArea == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����ѡ��һ�����ҡ�");
                return;
            }

            clsComboBoxValue[] objValueArr = null;
            new clsComboBoxDomainOld().m_lngGetAllItem(m_ObjCurrentArea.m_strDEPTID_CHR, this.Name, p_cboSender.Name, out objValueArr);

            if (objValueArr == null)
                return;

            p_cboSender.ClearItem();
            for (int i = 0; i < objValueArr.Length; i++)
            {
                if (objValueArr[i].m_strItemContent != null && objValueArr[i].m_strItemContent != string.Empty)
                    p_cboSender.AddItem(objValueArr[i].m_strItemContent);
            }
        }

        private void m_mthComboBox_DropDown(object sender, System.EventArgs e)
        {
            m_mthSetComboBoxListItem((ctlComboBox)sender);
        }


        #region new

        void cboNew_evtDelItem(object sender, EventArgs e)
        {
            if (m_ObjCurrentArea == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����ѡ�����");
                return;
            }

            com.digitalwave.Controls.ctlComboBox cbo = sender as com.digitalwave.Controls.ctlComboBox;
            if (cbo == null)
                return;
            if (cbo.SelectedItem == null)
                return;
            clsComboBoxValue objValue;
            objValue = new clsComboBoxValue();
            objValue.m_strDeptID = m_ObjCurrentArea.m_strDEPTID_CHR;
            objValue.m_strTypeID = this.Name;
            objValue.m_strItemID = cbo.Name;
            objValue.m_strItemContent = cbo.SelectedItem.ToString();
            long lngRef = new clsComboBoxDomainOld().m_lngDeleteItem(objValue);
            if (lngRef < 1)
                return;
            cbo.Items.Remove(cbo.SelectedItem);
            cbo.Update();
        }
        void cboNew_evtModifyItem(object sender, EventArgs e)
        {
            if (m_ObjCurrentArea == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����ѡ�����");
                return;
            }

            com.digitalwave.Controls.ctlComboBox cbo = sender as com.digitalwave.Controls.ctlComboBox;
            if (cbo == null)
                return;
            if (cbo.Text.Trim() == "" || cbo.m_ObjCurrentItem == null)
                return;
            clsComboBoxValue objValue;
            objValue = new clsComboBoxValue();
            //objValue.m_strDeptID = MDIParent.s_ObjDepartment.m_StrDeptID;
            objValue.m_strDeptID = m_ObjCurrentArea.m_strDEPTID_CHR;
            objValue.m_strTypeID = this.Name;
            objValue.m_strItemID = cbo.Name;
            objValue.m_strItemContent = cbo.m_ObjCurrentItem.ToString();
            long lngRef = new clsComboBoxDomainOld().m_lngModifyItem(objValue, cbo.Text.Trim());
            if (lngRef < 1)
                return;
            string strText = cbo.Text;
            cbo.Items.Remove(cbo.SelectedItem);
            cbo.Update();
            cbo.Items.Insert(0, strText);
            cbo.SelectedIndex = 0;
        }

        void cboNew_evtAddItem(object sender, EventArgs e)
        {
            if (m_ObjCurrentArea == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����ѡ����");
                return;
            }

            com.digitalwave.Controls.ctlComboBox cbo = sender as com.digitalwave.Controls.ctlComboBox;
            if (cbo == null)
                return;
            if (cbo.Text.Trim() == "")
                return;
            clsComboBoxValue objValue = new clsComboBoxValue();
            //objValue.m_strDeptID = MDIParent.s_ObjDepartment.m_StrDeptID;
            objValue.m_strDeptID = m_ObjCurrentArea.m_strDEPTID_CHR;
            objValue.m_strTypeID = this.Name;
            objValue.m_strItemID = cbo.Name;
            objValue.m_strItemContent = cbo.Text.Trim();
            long lngRef = new clsComboBoxDomainOld().m_lngAddItemToDB(objValue);
            if (lngRef < 1)
                return;
            cbo.Items.Insert(0, objValue.m_strItemContent);
            cbo.SelectedIndex = 0;

        }

        void cboNew_DropDown(object sender, EventArgs e)
        {
            com.digitalwave.Controls.ctlComboBox cbo = sender as com.digitalwave.Controls.ctlComboBox;
            if (m_ObjCurrentArea == null)
            {
                clsPublicFunction.ShowInformationMessageBox("����ѡ��һ��������");
                return;
            }

            clsComboBoxValue[] objValueArr = null;
            new clsComboBoxDomainOld().m_lngGetAllItem(m_ObjCurrentArea.m_strDEPTID_CHR, this.Name, cbo.Name, out objValueArr);

            if (objValueArr == null)
                return;

            cbo.Items.Clear();
            for (int i = 0; i < objValueArr.Length; i++)
            {
                if (objValueArr[i].m_strItemContent != null && objValueArr[i].m_strItemContent != string.Empty)
                    cbo.Items.Add(objValueArr[i].m_strItemContent);
            }
        }
        #endregion New
        #endregion Combox Event

        #region ����ѡ�����Ҽ�ȡ������
        private void m_mthrdb_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (e.Button == MouseButtons.Right)
                rdb.Checked = false;

        }
        #endregion ����ѡ�����Ҽ�ȡ������

        #region ������ʾ����������ģ��
        private void m_mthLoadAndShowTemplateSet(clsPatient p_objSelectedPatient)
        {
            try
            {
                System.Data.DataTable dtRecords = null;
                string strSql = "";
                clsICD10Inf[] objValue;
                com.digitalwave.common.ICD10.Tool.clsIllnessQueryDomain objICD10InfDomain = new com.digitalwave.common.ICD10.Tool.clsIllnessQueryDomain();
                strSql = "select ID,ICD_Name from ticd10 where id in(select ICD_ID from tInPatient_ICD10 where InPatientID='" + p_objSelectedPatient.m_StrInPatientID + "' and InPatientDate=" + com.digitalwave.Utility.SQLConvert.clsDatabaseSQLConvert.s_strGetSQLDateTimeFormat(p_objSelectedPatient.m_DtmSelectedInDate) + ")";// to_date('" + p_objSelectedPatient.m_DtmSelectedInDate + "','yyyy-mm-dd hh24:mi:ss'))";
                objICD10InfDomain.m_lngGetMaxID(strSql, ref dtRecords);
                if (dtRecords != null || dtRecords.Rows.Count > 0)
                {
                    if (dtRecords.Rows.Count > 0)
                    {
                        objValue = new clsICD10Inf[dtRecords.Rows.Count];
                        for (int i = 0; i <= dtRecords.Rows.Count - 1; i++)
                        {
                            objValue[i] = new clsICD10Inf();
                            objValue[i].ICD10_ID = long.Parse(dtRecords.Rows[i]["ID"].ToString());
                            objValue[i].ICD10_Name = dtRecords.Rows[i]["ICD_Name"].ToString();
                        }
                        frmInvokeTemplateByICD10 frmTemp = new frmInvokeTemplateByICD10(objValue, this, true);
                        frmTemp.Show();
                        frmTemp.Focus();
                    }
                }
            }
            catch (Exception)
            { }

        }
        #endregion

        private void txtInPatientID_TextChanged(object sender, System.EventArgs e)
        {
            //			if(m_objBaseCurrentPatient!=null)
            //				m_mthLoadAndShowTemplateSet(m_objBaseCurrentPatient);

        }

        private void frmHRPBaseForm_Enter(object sender, System.EventArgs e)
        {
            if (this.m_objBaseCurrentPatient != null)
            {
                MDIParent.s_ObjCurrentPatient = this.m_objBaseCurrentPatient;
            }
        }

        /// <summary>
        /// ������СԪ�ؼ�ֵ
        /// </summary>
        private void m_mthSaveMinElementValue()
        {
            iCare.CustomForm.clsMinElementColDomain objDomain = new iCare.CustomForm.clsMinElementColDomain();
            for (int i = 0; i < m_arlMinElementColValue.Count; i++)
            {
                clsMinElementValues obj = (clsMinElementValues)m_arlMinElementColValue[i];
                if (obj == null)
                    continue;

                obj.m_strFormClsName = this.Name;
                obj.m_strRegisterId = m_objBaseCurrentPatient.m_StrRegisterId;
                obj.m_dtmCreatedDate = m_dtmCreatedDate;
                if (obj.m_objElementValueArr == null || obj.m_objElementValueArr.Length <= 0)
                    continue;

                objDomain.m_lngSaveTemplateData(obj);
            }
            m_arlMinElementColValue.Clear();
        }

        /// <summary>
        /// �ݹ���տؼ�����
        /// </summary>
        /// <param name="p_ctlInput">���ؼ�</param>
        /// <param name="p_ctlNotArr">������յĿؼ�</param>
        protected void m_mthClear_Recursive(Control p_ctlParent, Control[] p_ctlNotArr)
        {
            //�������
            if (p_ctlNotArr != null)
                foreach (Control ctl in p_ctlNotArr)
                    if (ctl.Name == p_ctlParent.Name)
                        return;

            switch (p_ctlParent.GetType().FullName)
            {
                case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                    ((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlParent).m_mthClearText();
                    return;
                case "com.digitalwave.controls.ctlRichTextBox":
                    ((com.digitalwave.controls.ctlRichTextBox)p_ctlParent).m_mthClearText();
                    return;
                case "com.digitalwave.Utility.Controls.ctlBorderTextBox":
                    ((com.digitalwave.Utility.Controls.ctlBorderTextBox)p_ctlParent).Text = "";
                    return;
                case "System.Windows.Forms.RichTextBox":
                    ((System.Windows.Forms.RichTextBox)p_ctlParent).Text = "";
                    return;
                case "System.Windows.Forms.CheckBox":
                    ((System.Windows.Forms.CheckBox)p_ctlParent).Checked = false;
                    return;
            }

            foreach (Control ctl in p_ctlParent.Controls)
                m_mthClear_Recursive(ctl, p_ctlNotArr);
        }
        /// <summary>
        /// �Ƿ�����Ҹı��¼�
        /// </summary>
        protected bool m_blnCanDeptSelectIndexChangeEventTakePlace = true;
        /// <summary>
        /// �Ƿ������ı��¼�
        /// </summary>
        protected bool m_blnCanAreaSelectIndexChangeEventTakePlace = true;

        protected void m_cboDept_DropDown(object sender, System.EventArgs e)
        {
            try
            {
                if (m_objBaseCurrentPatient != null && m_dlgHandleSaveBeforePrint() == DialogResult.Cancel)
                    return;

                if (m_blnCheckRecordBase(this))
                    return;

                m_blnCanDeptSelectIndexChangeEventTakePlace = false;
                //��ʼ�����
                m_cboDept.ClearItem();
                this.m_cboArea.ClearItem();
                this.m_cboArea.Text = "";
                this.m_mthClearPatientBaseInfoButArea();
                this.m_mthClearAllInfo(this);
                //��ȡ����
                clsManageExplorerDomain objDomain = new clsManageExplorerDomain();
                clsEmrDept_VO[] objDeptInfoArr = null;
                clsDepartment[] objOldDeptArr = null;
                long lngRes = objDomain.m_lngGetDeptInfo(clsEMRLogin.LoginInfo.m_strEmpID, out objDeptInfoArr, out objOldDeptArr);
                if (lngRes <= 0)
                {
                    if (lngRes == (long)enmOperationResult.Not_permission)
                        clsPublicFunction.ShowInformationMessageBox("Ȩ�޲���!");
                    else
                        clsPublicFunction.ShowInformationMessageBox("���ݿ�����ʧ��!");
                    return;
                }
                if (objOldDeptArr != null)
                {
                    m_cboDept.AddRangeItems(objOldDeptArr);
                }

            }
            catch (Exception exp)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.LogDetailError(exp, false);
            }
            finally
            {
                m_blnCanDeptSelectIndexChangeEventTakePlace = true;
            }
        }

        protected void m_cboArea_DropDown(object sender, System.EventArgs e)
        {
            try
            {
                if (m_objBaseCurrentPatient != null && m_dlgHandleSaveBeforePrint() == DialogResult.Cancel)
                    return;

                if (m_blnCheckRecordBase(this))
                    return;

                m_blnCanAreaSelectIndexChangeEventTakePlace = false;
                //��ʼ�����
                this.Cursor = Cursors.WaitCursor;

                this.m_mthClearPatientBaseInfoButArea();
                this.m_mthClearAllInfo(this);
                //��ȡ����
                m_cboArea.ClearItem();
                clsHospitalManagerDomain objDomain = new clsHospitalManagerDomain();
                clsEmrDept_VO[] objAreaInfoArr = null;
                long lngRes = objDomain.m_lngGetAreaInfo(((clsDepartment)(m_cboDept.SelectedItem)).m_strDeptNewID, out objAreaInfoArr);

                //long lngRes=objDomain.m_lngGetAreaInfo(((clsEmrDept_VO)(m_cboDept.SelectedItem)).m_strDEPTID_CHR, out objAreaInfoArr);
                if (lngRes <= 0)
                {
                    if (lngRes == (long)enmOperationResult.Not_permission)
                        clsPublicFunction.ShowInformationMessageBox("Ȩ�޲���!");
                    else
                        clsPublicFunction.ShowInformationMessageBox("���ݿ�����ʧ��!");
                    return;
                }
                if (objAreaInfoArr != null)
                {
                    m_cboArea.ClearItem();
                    for (int i = 0; i < objAreaInfoArr.Length; i++)
                    {
                        //ת��Ϊ�ɵ�
                        clsInPatientArea objAreaTemp = new clsInPatientArea(objAreaInfoArr[i].m_strSHORTNO_CHR, objAreaInfoArr[i].m_strDEPTNAME_VCHR, objAreaInfoArr[i].m_strDEPTID_CHR);
                        //ת��ʹ�ã��±��shortno���ɱ��ID�������¼�һ���ֶα����±�ID
                        objAreaTemp.m_strAreaNewID = objAreaInfoArr[i].m_strDEPTID_CHR;
                        m_cboArea.AddItem(objAreaTemp);
                        //m_cboArea.AddItem(objAreaInfoArr[i]);

                    }
                }
            }
            catch (Exception exp)
            {
                string strErrMessage = exp.Message + "\n at Module:[" + exp.TargetSite.ReflectedType.Name + "]\n  Method:[" + exp.TargetSite.Name + "]";
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                objLogger.Log2File(MDIParent.s_strErrorFilePath, "Exception: \r\n" + strErrMessage);
            }
            finally
            {
                m_blnCanAreaSelectIndexChangeEventTakePlace = true;
                this.Cursor = Cursors.Default;
            }


            #region RegionName
            //			this.Cursor=Cursors.WaitCursor;
            //			m_cboArea.ClearItem();
            //			this.m_mthClearPatientBaseInfoButArea();
            //			this.m_mthClearAllInfo(this);
            //			clsInPatientArea[] objAreaArr;
            //			if(this.m_cboDept.SelectedItem==null)
            //			{
            //				this.Cursor=Cursors.Default;
            //				return;
            //			}
            //			new clsDepartmentManager().m_lngGetAllAreaInDept(((clsDepartment)(this.m_cboDept.SelectedItem)).m_StrDeptID,out objAreaArr);
            //			if(objAreaArr !=null)
            //			{	
            //				this.m_cboArea.AddRangeItems(objAreaArr);
            //				//				this.m_cboArea.SelectedIndex = 0;
            //			}
            //			this.Cursor=Cursors.Default;
            #endregion
        }

        #region Property
        /// <summary>
        /// ��ȡ��ǰ��¼��OpenDate��
        /// </summary>
        protected virtual string m_StrCurrentOpenDate
        {
            get
            {
                return "1900-1-1";
            }
        }
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        protected virtual enmApproveType m_EnmAppType
        {
            get { return enmApproveType.none; }
        }
        /// <summary>
        /// ��ȡ��¼������
        /// </summary>
        protected virtual string m_StrRecorder_ID
        {
            get { return ""; }
        }
        /// <summary>
        /// 
        /// </summary>
        #endregion

        #region Jump Control
        protected virtual void m_mthInitJump(clsJumpControl p_objJump)
        {
            //			p_objJump=new clsJumpControl(this,new Control[]{m_txtOutPatientID,m_txtDept,m_txtArea},Keys.Enter);
        }
        #endregion

        #region ����ǩ�� created by tfzhang at 2005��11��1�� 11:53:38

        /// <summary>
        /// ��֤����ǩ��
        /// </summary>
        /// <param name="p_strFormID"></param>
        /// <returns></returns>
        public virtual long m_lngSignVerify(string p_strFormID, string p_strRecordID)
        {
            long lngRes = 0;
            try
            {
                if (p_strFormID == null || p_strFormID.Trim().Length == 0)
                    return 0;
                clsDigitalSign_domain objSvc = new clsDigitalSign_domain();
                clsEmrDigitalSign_VO objSign = new clsEmrDigitalSign_VO();
                clsDigitalSign clsSign = new clsDigitalSign();
                lngRes = objSvc.m_lngGetDigitalSign(p_strFormID, p_strRecordID, m_blnCheckPatientIsOut(m_objBaseCurrentPatient.m_StrRegisterId), out objSign);
                if (objSign == null)
                {
                    MessageBox.Show("��δ����ǩ��������֤", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }
                string strContent = System.Text.Encoding.UTF8.GetString(objSign.m_bteCONTENT_TXT);
                //��֤
                string strReturn = clsSign.verify(strContent, objSign.m_strDSCONTENT_TXT, 0);
                if (strReturn == null)
                {
                    MessageBox.Show("ǩ����֤ʧ��,��ȷ�ϲ���Key��", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(strReturn, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exp)
            {
                string strTmp = exp.Message;
                lngRes = 0;
            }
            return lngRes;
        }


        //����ǩ�����Ƴ�����ģ��

        /// <summary>
        /// ���ǩ����Ϣ
        /// </summary>
        /// <param name="p_strFormID"></param>
        /// <param name="objSign"></param>
        /// <returns></returns>
        protected virtual long m_lngGetSign(string p_strFormID, out clsEmrDigitalSign_VO objSign)
        {
            objSign = null;
            return 0;
        }

        #endregion

        #region �޺ۼ��޸Ŀ��� create by tfzhang 2006-01-17
        /// <summary>
        /// �޺ۼ��޸�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkModifyWithoutMatk_Click(object sender, System.EventArgs e)
        {
            chkModifyWithoutMatk.Checked = m_mthModifyWithoutMark();
        }
        /// <summary>
        /// �޺ۼ��޸ķ���
        /// </summary>
        protected virtual bool m_mthModifyWithoutMark()
        {
            //todo
            return false;
        }
        #endregion


        #region ���´���ǰѯ���Ƿ񱣴�ԭ�д���
        internal bool m_blnHasClosing = false;
        internal bool m_blnNotClickWindows = false;

        private void frmHRPBaseForm_Deactivate(object sender, EventArgs e)
        {
            if ((m_objBaseCurrentPatient != null && m_objBaseCurrentPatient.m_IntCharacter != 1)
                && !m_blnHasCheckSamePatient && !m_blnHasClosing && !this.TopLevel && !m_blnNotClickWindows)
            {
                m_blnIsSaveBeforeNewForm();
            }
            m_blnNotClickWindows = false;
        }

        /// <summary>
        /// ���´���ǰѯ���Ƿ񱣴�ԭ�д���(���"����"���б�ʱ����)
        /// </summary>
        /// <returns></returns>
        private void m_blnIsSaveBeforeNewForm()
        {

            DialogResult dlgResult = DialogResult.None;
            if (!MDIParent.s_ObjSaveCue.m_blnCheckStatusSame(this))
            {
                dlgResult = clsPublicFunction.ShowQuestionMessageBox("[" + this.Text + "]���˸Ķ����Ƿ񱣴棿", MessageBoxButtons.YesNo);

                if (dlgResult == DialogResult.Yes)
                {
                    this.m_lngSave();
                    this.Close();
                }
            }
        }
        #endregion

        internal bool m_blnHasCheckSamePatient = false;
        private bool m_blnCheckSamePatientForm(string p_strInPatientID)
        {
            if (p_strInPatientID == null)
                return false;
            if (this.MdiParent == null || this.MdiParent.MdiChildren == null)
                return false;
            m_blnHasCheckSamePatient = false;
            for (int i = 0; i < this.MdiParent.MdiChildren.Length; i++)
            {
                if (this.MdiParent.MdiChildren[i] is frmHRPBaseForm
                    && !this.MdiParent.MdiChildren[i].Equals(this)
                    && this.MdiParent.MdiChildren[i].Name == this.Name)
                {
                    if (((frmHRPBaseForm)this.MdiParent.MdiChildren[i]).m_objBaseCurrentPatient != null &&
                        ((frmHRPBaseForm)this.MdiParent.MdiChildren[i]).m_objBaseCurrentPatient.m_StrInPatientID == p_strInPatientID)
                    {
                        m_blnHasCheckSamePatient = true;
                        this.MdiParent.MdiChildren[i].Activate();
                        return true;
                    }
                }
            }
            return false;
        }

        private bool m_blnCheckRecordBase(Form p_frmRecordBase)
        {
            if (p_frmRecordBase is frmRecordsBase)
            {
                if (((frmRecordsBase)p_frmRecordBase).m_FrmCurrentSub != null)
                {
                    clsPublicFunction.ShowInformationMessageBox("���ȹر��Ӵ���[" + ((frmRecordsBase)p_frmRecordBase).m_FrmCurrentSub.Text + "]");
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// �ṩ�Ӵ�����ֶ���Դ�ͷ�
        /// </summary>
        //		protected override void m_mthReleaseSub()
        //		{
        //			if(m_objTempTool != null)
        //				m_objTempTool.Release();
        //			if(m_objJump != null)
        //				m_objJump.Release();
        //			if(m_frmSaveTemplateset != null)
        //				m_frmSaveTemplateset.Release();
        //			if(m_objHighLight != null)
        //				m_objHighLight.m_mthClear();
        //		}
        #region ǩ��
        /// <summary>
        /// ��ֵ��ǩ��
        /// </summary>
        /// <param name="p_lsvSign"></param>
        /// <param name="p_objSignerArr"></param>
        protected virtual void m_mthAddSignToListView(ListView p_lsvSign, clsEmrSigns_VO[] p_objSignerArr)
        {
            if (p_lsvSign != null && p_objSignerArr != null)
            {
                p_lsvSign.Items.Clear();
                for (int i = 0; i < p_objSignerArr.Length; i++)
                {
                    if (p_objSignerArr[i].controlName == p_lsvSign.Name)
                    {
                        ListViewItem lviNewItem = new ListViewItem(p_objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName);
                        //ID ����ظ���
                        lviNewItem.SubItems.Add(p_objSignerArr[i].objEmployee.m_strEMPID_CHR);
                        //���� ������(200609011�޸�Ϊ����ʷ�ļ���������ʷ�ļ����ڱ����ʱ���浽ǩ����bhuang)
                        lviNewItem.SubItems.Add(p_objSignerArr[i].objEmployee.m_StrHistroyLevel);
                        lviNewItem.SubItems.Add(p_objSignerArr[i].m_dtmModiftDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        //tag��Ϊ����
                        lviNewItem.Tag = p_objSignerArr[i].objEmployee;
                        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                        p_lsvSign.Items.Add(lviNewItem);
                    }
                }
            }
        }
        /// <summary>
        /// ��ֵ��ǩ��
        /// </summary>
        /// <param name="p_lsvSign"></param>
        /// <param name="p_objSignerArr"></param>
        protected virtual void m_mthAddSignToListView(ListView[] p_lsvSigns, clsEmrSigns_VO[] p_objSignerArr)
        {
            if (p_lsvSigns == null || p_objSignerArr == null || p_lsvSigns.Length == 0 || p_objSignerArr.Length == 0) return;

            for (int j2 = 0; j2 < p_lsvSigns.Length; j2++)
            {
                p_lsvSigns[j2].Items.Clear();
                for (int i = 0; i < p_objSignerArr.Length; i++)
                {
                    if (p_objSignerArr[i].controlName == p_lsvSigns[j2].Name)
                    {
                        ListViewItem lviNewItem = new ListViewItem(p_objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName);
                        //ID ����ظ���
                        lviNewItem.SubItems.Add(p_objSignerArr[i].objEmployee.m_strEMPID_CHR);
                        //���� ������(200609011�޸�Ϊ����ʷ�ļ���������ʷ�ļ����ڱ����ʱ���浽ǩ����bhuang)
                        lviNewItem.SubItems.Add(p_objSignerArr[i].objEmployee.m_StrHistroyLevel);
                        lviNewItem.SubItems.Add(p_objSignerArr[i].m_dtmModiftDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        //tag��Ϊ����
                        lviNewItem.Tag = p_objSignerArr[i].objEmployee;
                        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                        p_lsvSigns[j2].Items.Add(lviNewItem);
                    }
                }
            }
        }
        /// <summary>
        /// ��ֵ��ǩ��
        /// </summary>
        /// <param name="p_lsvSign"></param>
        /// <param name="p_objSignerArr"></param>
        /// <param name="p_intisshowlevel">��ȡ��ʷ��¼ʱ�Ƿ���ʾְ�ƣ�0-��ʾ��1-����ʾ</param>
        protected virtual void m_mthAddSignToListView(ListView[] p_lsvSigns, clsEmrSigns_VO[] p_objSignerArr, int p_intisshowlevel)
        {
            if (p_lsvSigns == null || p_objSignerArr == null || p_lsvSigns.Length == 0 || p_objSignerArr.Length == 0) return;

            for (int j2 = 0; j2 < p_lsvSigns.Length; j2++)
            {
                p_lsvSigns[j2].Items.Clear();
                for (int i = 0; i < p_objSignerArr.Length; i++)
                {
                    if (p_objSignerArr[i].controlName == p_lsvSigns[j2].Name)
                    {
                        ListViewItem lviNewItem = null;
                        if (p_intisshowlevel == 0)
                        {
                            lviNewItem = new ListViewItem(p_objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName);
                        }
                        else if (p_intisshowlevel == 1)
                        {
                            lviNewItem = new ListViewItem(p_objSignerArr[i].objEmployee.m_strLASTNAME_VCHR);
                        }
                        else
                        {
                            lviNewItem = new ListViewItem(p_objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName);

                        }
                        //ID ����ظ���
                        lviNewItem.SubItems.Add(p_objSignerArr[i].objEmployee.m_strEMPID_CHR);
                        //���� ������(200609011�޸�Ϊ����ʷ�ļ���������ʷ�ļ����ڱ����ʱ���浽ǩ����bhuang)
                        lviNewItem.SubItems.Add(p_objSignerArr[i].objEmployee.m_StrHistroyLevel);
                        lviNewItem.SubItems.Add(p_objSignerArr[i].m_dtmModiftDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        //tag��Ϊ����
                        lviNewItem.Tag = p_objSignerArr[i].objEmployee;
                        //�ǰ�˳�򱣴�ʻ�ȡ˳��Ҳһ��
                        p_lsvSigns[j2].Items.Add(lviNewItem);
                    }
                }
            }
        }
        /// <summary>
        /// ��ֵ��ǩ��,���ݹ��ţ�ѭ�����й��Ų�ѯԱ��������
        /// </summary>
        /// <param name="p_txtSignArr">����ǩ����Text�ؼ�</param>
        /// <param name="p_strEmpArr">ǩ����ID����</param>
        /// <param name="p_blnIsEnable">��ֵ���Ƿ��ÿؼ���Enable����</param>
        protected virtual void m_mthAddSignToTextBoxByEmpNo(TextBoxBase[] p_txtSignArr, string[] p_strEmpArr, bool[] p_blnIsEnable)
        {
            if (p_txtSignArr == null || p_strEmpArr == null || p_txtSignArr.Length != p_strEmpArr.Length)
                return;
            //���ݹ��Ż�ȡǩ����Ϣ
            //���ڼ��ݿ��ǣ�����ʹ�� tfzhang 2006-03-12
            com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
            for (int i = 0; i < p_strEmpArr.Length; i++)
            {
                if (!string.IsNullOrEmpty(p_strEmpArr[i]))
                {
                    clsEmrEmployeeBase_VO objSign4 = null;
                    objEmployeeSign.m_lngGetEmpByNO(p_strEmpArr[i].Trim(), out objSign4);
                    if (objSign4 != null)
                    {
                        objSign4.m_strTechnicalRank = objSign4.m_strTECHNICALRANK_CHR;
                        p_txtSignArr[i].Text = objSign4.m_strGetTechnicalRankAndName;
                        p_txtSignArr[i].Tag = objSign4;
                        p_txtSignArr[i].Enabled = p_blnIsEnable[i];
                    }

                }
            }
        }
        /// <summary>
        /// ��ֵǩ�����ı���
        /// ����Ա��Id��Ӧ����������Ա���ࡣ
        /// ���ھ������ǩ��ֻ��Ҫ�õ�Id���������Ż�Ч�ʣ�ʹ�������ַ�����
        /// ���Ա�Ӧ���ڻ�ȡ��¼ʱ�ó�Ա��Id��������
        /// </summary>
        /// <param name="p_txtSignArr"></param>
        /// <param name="p_strEmpArr">[Id][����][ְ��]</param>
        /// <param name="p_blnIsEnable"></param>
        /// <param name="p_blnIsIgnoreTag">�Ƿ���Կؼ���Tag����</param>
        protected virtual void m_mthAddSignToTextBoxByValue(TextBoxBase[] p_txtSignArr, string[][] p_strEmpArr, bool[] p_blnIsEnable, bool p_blnIsIgnoreTag)
        {
            if (p_txtSignArr == null || p_strEmpArr == null || p_txtSignArr.Length != p_strEmpArr.Length)
                return;
            for (int i = 0; i < p_strEmpArr.Length; i++)
            {
                clsEmrEmployeeBase_VO objSign4 = null;
                if (!string.IsNullOrEmpty(p_strEmpArr[i][0]))
                {
                    objSign4 = new clsEmrEmployeeBase_VO();
                    objSign4.m_strEMPID_CHR = p_strEmpArr[i][0];
                    objSign4.m_strLASTNAME_VCHR = p_strEmpArr[i][1];
                    if (!string.IsNullOrEmpty(p_strEmpArr[i][2]))
                        objSign4.m_strTechnicalRank = p_strEmpArr[i][2];

                }
                if (objSign4 != null || p_blnIsIgnoreTag)
                {
                    p_txtSignArr[i].Text = p_strEmpArr[i][1];
                    p_txtSignArr[i].Tag = objSign4;
                    p_txtSignArr[i].Enabled = p_blnIsEnable[i];
                }
            }
        }
        /// <summary>
        /// ��ֵǩ�����ı���
        /// </summary>
        /// <param name="p_txtSignArr"></param>
        /// <param name="p_objSignerArr"></param>
        /// <param name="p_blnIsEnable"></param>
        /// <param name="p_blnIsIgnoreTag"></param>
        protected virtual void m_mthAddSignToTextBoxBase(TextBoxBase[] p_txtSignArr, clsEmrSigns_VO[] p_objSignerArr, bool[] p_blnIsEnable, bool p_blnIsIgnoreTag)
        {
            if (p_txtSignArr == null || p_objSignerArr == null || p_objSignerArr.Length == 0)
                return;
            for (int i = 0; i < p_txtSignArr.Length; i++)
            {
                p_txtSignArr[i].Clear();
                p_txtSignArr[i].Tag = null;
                for (int j = 0; j < p_objSignerArr.Length; j++)
                {
                    if (p_objSignerArr[j].controlName == p_txtSignArr[i].Name && p_objSignerArr[j].objEmployee != null)
                    {
                        p_txtSignArr[i].Text = p_objSignerArr[j].objEmployee.m_strGetTechnicalRankAndName;
                        p_txtSignArr[i].Enabled = p_blnIsEnable[i];
                        if (!p_blnIsIgnoreTag)
                        {
                            p_txtSignArr[i].Tag = p_objSignerArr[j].objEmployee;
                        }
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// ��ȡǩ���б�
        /// </summary>
        /// <param name="p_ctlSigners"></param>
        /// <param name="p_objSignerArr"></param>
        /// <param name="p_strUserIDList"></param>
        /// <param name="p_strUserNameList"></param>
        protected virtual void m_mthGetSignArr(Control[] p_ctlSigners, ref clsEmrSigns_VO[] p_objSignerArr, ref string p_strUserIDList, ref string p_strUserNameList)
        {
            if (p_ctlSigners == null)
                return;
            string strUserIDList = "";
            string strUserNameList = "";
            string strFormName = p_ctlSigners[0].FindForm().Name;
            List<clsEmrSigns_VO> arlSignerArr = new List<clsEmrSigns_VO>();
            for (int j = 0; j < p_ctlSigners.Length; j++)
            {
                if (p_ctlSigners[j] is ListView)
                {
                    ListView lsvSign = (ListView)p_ctlSigners[j];
                    for (int i = 0; i < lsvSign.Items.Count; i++)
                    {
                        clsEmrSigns_VO objSigner = m_objGetSign(lsvSign.Items[i].Tag, lsvSign.Name, strFormName, ref strUserIDList, ref strUserNameList);

                        DateTime dtmModify = DateTime.MinValue;
                        if (lsvSign.Items[i].SubItems.Count > 3 && DateTime.TryParse(lsvSign.Items[i].SubItems[3].Text, out dtmModify))
                        {
                            objSigner.m_dtmModiftDate = dtmModify;
                        }
                        else
                        {
                            objSigner.m_dtmModiftDate = DateTime.MinValue;
                        }
                        if (objSigner != null)
                            arlSignerArr.Add(objSigner);
                    }
                }
                else if (p_ctlSigners[j] is TextBoxBase)
                {
                    clsEmrSigns_VO objSigner = m_objGetSign(p_ctlSigners[j].Tag, p_ctlSigners[j].Name, strFormName, ref strUserIDList, ref strUserNameList);
                    if (objSigner != null)
                        arlSignerArr.Add(objSigner);
                }
                if (strUserNameList.EndsWith(","))
                    strUserNameList = strUserNameList.Remove(strUserNameList.Length - 1);
                if (strUserIDList.EndsWith(","))
                    strUserIDList = strUserIDList.Remove(strUserIDList.Length - 1);
            }
            if (arlSignerArr.Count > 0)
            {
                p_objSignerArr = new clsEmrSigns_VO[arlSignerArr.Count];
                p_objSignerArr = arlSignerArr.ToArray();
            }
            p_strUserNameList = strUserNameList;
            p_strUserIDList = strUserIDList;
        }
        private clsEmrSigns_VO m_objGetSign(object p_objEmp, string p_strControlName, string p_strFormName, ref string p_strUserIDList, ref string p_strUserNameList)
        {
            clsEmrSigns_VO objSigner = new clsEmrSigns_VO();
            objSigner.objEmployee = (clsEmrEmployeeBase_VO)p_objEmp;
            if (objSigner.objEmployee == null)
                return null;
            objSigner.controlName = p_strControlName;
            objSigner.m_strFORMID_VCHR = p_strFormName;
            objSigner.m_strREGISTERID_CHR = com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR;
            if (p_strControlName == "lsvSign")
            {
                //�ۼ���ʽ 0972,0324,
                p_strUserIDList = p_strUserIDList + objSigner.objEmployee.m_strEMPID_CHR + ",";
                p_strUserNameList = p_strUserNameList + objSigner.objEmployee.m_strLASTNAME_VCHR + ",";
            }
            return objSigner;
        }
        /// <summary>
        /// ��ȡ�ͺۼ��йص�ǩ���ַ���
        /// </summary>
        /// <param name="p_strUserIDList">��ʽ 0000001,0000003,</param>
        /// <param name="p_strUserNameList"></param>
        protected virtual void m_mthGetSignUserList(ListView p_lsvSign, out string p_strUserIDList, out string p_strUserNameList)
        {
            p_strUserIDList = string.Empty;
            p_strUserNameList = string.Empty;
            if (p_lsvSign.Items.Count == 0)
                return;
            for (int i = 0; i < p_lsvSign.Items.Count; i++)
            {
                clsEmrSigns_VO objSigner = p_lsvSign.Items[0].Tag as clsEmrSigns_VO;
                if (objSigner != null)
                {
                    //�ۼ���ʽ 0972,0324,
                    p_strUserIDList = p_strUserIDList + objSigner.objEmployee.m_strEMPID_CHR + ",";
                    p_strUserNameList = p_strUserNameList + objSigner.objEmployee.m_strLASTNAME_VCHR + ",";
                }
            }
            if (p_strUserIDList.EndsWith(","))
                p_strUserIDList = p_strUserIDList.Remove(p_strUserIDList.Length - 1);
            if (p_strUserNameList.EndsWith(","))
                p_strUserNameList = p_strUserNameList.Remove(p_strUserNameList.Length - 1);
        }
        /// <summary>
        /// ��ǩ��ʱ��֤����ǩ���� ������
        /// </summary>
        /// <param name="p_objValues">ǩ��������</param>
        /// <param name="p_blnMarkStatus">true���޺ۼ�������֤����ǩ���ߣ�false���кۼ�</param>
        /// <param name="p_objSignArr">ǩ���б�</param>
        /// <param name="p_strRecordId">��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20</param>
        /// <returns></returns>
        protected virtual long m_lngCheckSign(object p_objValues, bool p_blnMarkStatus, clsEmrSigns_VO[] p_objSignArr, string p_strRecordId)
        {
            #region ��ǩ��ʱ��֤����ǩ���� ������

            //����ǩ�� 
            //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
            clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
            objSign_VO.m_strFORMID_VCHR = this.Name;
            objSign_VO.m_strFORMRECORDID_VCHR = p_strRecordId;// m_objBaseCurrentPatient.m_StrInPatientID.Trim() + "-" + m_objBaseCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss");
            objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
            objSign_VO.m_strRegisterId = m_objBaseCurrentPatient.m_StrRegisterId;

            if (p_objSignArr != null)
            {
                ArrayList objSignerArr = new ArrayList();
                for (int i = 0; i < p_objSignArr.Length; i++)
                {
                    if (p_objSignArr[i].controlName == "lsvSign" || p_objSignArr[i].controlName == "m_txtSign")
                        objSignerArr.Add(p_objSignArr[i].objEmployee);
                }

                clsCheckSignersController objCheck = new clsCheckSignersController(objSignerArr, p_blnMarkStatus);
                if (objCheck.CheckSigner(p_objValues, objSign_VO) == -1)
                    return -1;
            }
            else
            {
                clsCheckSignersController objCheck = new clsCheckSignersController();
                if (objCheck.m_lngSign(p_objValues, objSign_VO) == -1)
                    return -1;
            }
            return 1;
            #endregion ��ǩ��ʱ��֤����ǩ���� ������
        }

        #endregion ǩ��
        /// <summary>
        /// �����Ƿ��ѡ����������
        /// </summary>
        /// <param name="p_blnIfCanSelect">true��ѡfalse����ѡ</param>
        protected void m_mthSetIfCanSelectPatient(bool p_blnIfCanSelect)
        {
            m_cboArea.Enabled = p_blnIfCanSelect;
            m_cboDept.Enabled = p_blnIfCanSelect;
            m_cmdNext.Enabled = p_blnIfCanSelect;
            txtInPatientID.Enabled = p_blnIfCanSelect;
            m_txtBedNO.Enabled = p_blnIfCanSelect;
            m_txtPatientName.Enabled = p_blnIfCanSelect;
            m_ctlAreaPatientSelection.m_mthSetSubItemEnable(p_blnIfCanSelect, p_blnIfCanSelect, true, p_blnIfCanSelect);
        }


        #region �޸ĺۼ��������

        /// <summary>
        /// �����Ƿ�����޸ģ��޸����ۼ�����
        /// �˷����Ӳ��̼�¼�Ļ�������,Ŀǰ����ֻ�ʺϲ��̼�¼,�Ժ�Ӧ�ĳ��ʺ����еı����¼�Ĳ�����.
        /// </summary>
        /// <param name="p_objRecordContent">������ʵ��</param>
        /// <param name="p_blnReset"></param>
        protected virtual void m_mthSetModifyControl(clsTrackRecordContent p_objRecordContent,
            bool p_blnReset)
        {
            if (m_blnNeedContextMenu)
                m_mthAddRichTemplateInContainer(this);

            //������д�淶���þ��崰�����д���ƣ����Ӵ�������ʵ��
            if (p_blnReset == true)
            {
                //����������жϣ���ʹ���ֵ��Ӳ�������������ɫ��ɰ�ɫ
                //if(MDIParent.s_bolIAnaSystem)
                //    m_mthSetRichTextModifyColor(this,SystemColors.Info);
                //else
                m_mthSetRichTextModifyColor(this, clsHRPColor.s_ClrInputFore);

                m_mthSetRichTextCanModifyLast(this, true);
            }
            else if (p_objRecordContent != null)
            {
                bool blnTempCanMoify;
                if (this.Name == "frmICUNurseRecordContent" || this.Name == "frmWaitLayRecord_AcadCon_GX"
                    || this.Name == "frmOXTIntravenousDripCon")
                    blnTempCanMoify = m_blnGetCanModifyLast(p_objRecordContent.m_strRecordUserID, p_objRecordContent.m_dtmCreateDate, p_objRecordContent.m_intMarkStatus);
                else
                    blnTempCanMoify = m_blnGetCanModifyLast(p_objRecordContent.m_strModifyUserID, p_objRecordContent.m_dtmCreateDate, p_objRecordContent.m_intMarkStatus);
                if (blnTempCanMoify && blnIsModifyWithoutMark)//��������޺ۼ��޸��򲻺���
                    m_mthSetRichTextModifyColor(this, Color.Black);
                else//����������޺ۼ��޸������
                    m_mthSetRichTextModifyColor(this, Color.Red);


                m_mthSetRichTextCanModifyLast(this, blnTempCanMoify);
            }
        }
        /// <summary>
        /// ���ô����пؼ������ı�����ɫ
        /// </summary>
        /// <param name="p_ctlControl"></param>
        /// <param name="p_clrColor"></param>
        protected void m_mthSetRichTextModifyColor(Control p_ctlControl, System.Drawing.Color p_clrColor)
        {
            #region ���ÿؼ������ı�����ɫ,Jacky-2003-3-24
            string strTypeName = p_ctlControl.GetType().FullName;
            if (strTypeName == "com.digitalwave.Utility.Controls.ctlRichTextBox")
                ((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlControl).m_ClrOldPartInsertText = p_clrColor;
            else if (strTypeName == "com.digitalwave.controls.ctlRichTextBox")
                ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_ClrOldPartInsertText = p_clrColor;

            if (p_ctlControl.HasChildren && strTypeName != "System.Windows.Forms.DataGrid")
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthSetRichTextModifyColor(subcontrol, p_clrColor);
                }
            }
            #endregion
        }

        /// <summary>
        /// ���ÿؼ������ı����Ƿ�����޸�
        /// </summary>
        /// <param name="p_ctlControl"></param>
        /// <param name="p_blnCanModifyLast"></param>
        protected void m_mthSetRichTextCanModifyLast(Control p_ctlControl, bool p_blnCanModifyLast)
        {
            #region ���ÿؼ������ı����Ƿ������޺ۼ��޸� Jacky-2003-3-24
            string strTypeName = p_ctlControl.GetType().FullName;
            if (strTypeName == "com.digitalwave.controls.ctlRichTextBox")
            {

                if (blnIsModifyWithoutMark)
                    ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_BlnCanModifyLast = p_blnCanModifyLast;
                else
                    ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_BlnCanModifyLast = false;
            }

            if (p_ctlControl.HasChildren && strTypeName != "System.Windows.Forms.DataGrid")
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthSetRichTextCanModifyLast(subcontrol, p_blnCanModifyLast);
                }
            }
            #endregion
        }
        /// <summary>
        /// ������ڣ�������ɫ�����÷���
        /// ����ü�¼������޸��˾��ǵ�ǰ�ĵ�½�ˣ������޸ĸü�¼
        /// ���򣬲����޸ģ�����6Сʱ�Ŀ��ƣ���liyi��richtextbox�����п��ƣ�
        /// </summary>
        /// <param name="p_strModifyUserID">������ǩ���ַ��������԰������ǩ����</param>
        /// <param name="p_dtmCreatDate">���Ĵ���ʱ��</param>
        /// <param name="p_intMark">���ۼ��޸����� 1�������޺ۼ��޸� 0�����Ժۼ��޸�</param>
        /// <returns></returns>
        protected bool m_blnGetCanModifyLast(string p_strModifyUserID, DateTime p_dtmCreatDate, int p_intMark)
        {
            //if (p_ModifyUserB.m_strUserID.IndexOf(p_ModifyUserA.m_strUserID.Trim())< 0)
            //if(p_strModifyUserID==null || p_strModifyUserID.Trim() == MDIParent.OperatorID.Trim())
            //ʱЧ�жϣ��ԡ�����ʱ�䡿Ϊ��׼,����ʱ����richtextbox����
            //modify by tfzhang at 2006-01-17
            if (p_intMark == 0)
            {
                int intl = int.Parse(clsEMRLogin.StrCanModifyTime);
                //MessageBox.Show(p_dtmCreatDate.ToString());
                if (p_dtmCreatDate.AddHours(intl) >= DateTime.Now)
                {
                    //���������̳иû��൫��δ���޸ĵı�������ʹ��
                    if (this.Name == "frmGeneralNurseRecord"
                        || this.Name == "frmGeneralNurseRecord_GXRec"
                        || this.Name == "frmSubICUIntensiveTend"
                        || this.Name == "frmSubWatchItemRecord"
                        || this.Name == "frmSubICUBreath"
                        || this.Name == "frmPostPartum_AcadCon"
                        || this.Name == "frmPostartumSeeRecordCon"
                        || this.Name == "frmIntensiveTend_GXContent"
                        || this.Name == "frmQuickeningTutelar_AcadCon"
                        || this.Name == "frmCardiovascularTend_GX"
                        || this.Name == "frmICUNurseRecord_GXCon"
                        || this.Name == "frmIntensiveTend"
                        || this.Name == "frmIntensiveTend_FC"
                        || this.Name == "frmIntensiveTend_FContent"
                        || this.Name == "frmIntensiveTend_GX"
                        || this.Name == "frmIntensiveTend_GXContent"
                        || this.Name == "frmSurgeryICUWardshipEdit"
                        || this.Name == "frmVeinSpecialUseDrugCon"
                        || this.Name == "frmWaitLayRecord_AcadCon"
                        || this.Name == "frmConsultation"
                        || this.Name == "frmDeathCaseDiscuss"
                        || this.Name == "frmDeathRecord"
                        || this.Name == "frmDeathRecordIn24Hours"
                        || this.Name == "frmEMR_OutHospitalIn24Hours"
                        || this.Name == "frmOutHospital")
                    {
                        if (p_strModifyUserID == null || p_strModifyUserID.IndexOf(clsEMRLogin.LoginEmployee.m_strEMPID_CHR.Trim()) >= 0
                            || p_strModifyUserID.IndexOf(clsEMRLogin.LoginEmployee.m_strEMPNO_CHR.Trim()) >= 0)
                        {
                            chkModifyWithoutMatk.Visible = true;
                            chkModifyWithoutMatk.Checked = true;
                            return true;
                        }
                        else
                        {
                            chkModifyWithoutMatk.Checked = false;
                            return false;
                        }
                    }
                    else
                    {
                        if (p_strModifyUserID == null || p_strModifyUserID.IndexOf(clsEMRLogin.LoginEmployee.m_strEMPID_CHR.Trim()) >= 0
                            || p_strModifyUserID.IndexOf(clsEMRLogin.LoginEmployee.m_strEMPNO_CHR.Trim()) >= 0)
                        {
                            chkModifyWithoutMatk.Visible = true;
                            chkModifyWithoutMatk.Checked = true;
                            return true;
                        }
                        else
                        {
                            chkModifyWithoutMatk.Checked = false;
                            return false;
                        }
                    }

                }
                else
                {
                    chkModifyWithoutMatk.Visible = false;
                    chkModifyWithoutMatk.Checked = false;
                    return false;

                }
            }
            else
            {
                return false;
            }
        }



        #region �����޺ۼ�����Richtextbox���û�ID���û�����
        protected virtual bool blnIsModifyWithoutMark
        {
            get { return true; }
        }
        #region ǩ���߼����ֶ�
        /// <summary>
        /// ǩ����ID����
        /// </summary>
        protected string strUserIDList = "";
        /// <summary>
        /// ǩ�������Ƽ���
        /// </summary>
        protected string strUserNameList = "";

        #endregion
        /// <summary>
        /// �����޺ۼ���������Richtextbox���û�ID���û�����
        /// </summary>
        /// <param name="p_objRichTextBox"></param>
        protected void m_mthSetRichTextBoxAttribWithIDandName(Control p_objRichTextBox)
        {
            if (p_objRichTextBox.GetType().FullName == "com.digitalwave.controls.ctlRichTextBox")
            {
                //����ǩ��ID��ǩ������	
                //�޺ۼ��޸���Ϊǩ����
                if (chkModifyWithoutMatk.Checked)
                {
                    ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_StrUserID = strUserIDList;
                    ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_StrUserName = strUserNameList;
                }
                else//�кۼ��޸���Ϊ��½��
                {
                    ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_StrUserID = clsEMRLogin.LoginEmployee.m_strEMPID_CHR.Trim();
                    ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_StrUserName = clsEMRLogin.LoginEmployee.m_strLASTNAME_VCHR.Trim();
                }
            }
        }
        /// <summary>
        /// �����޺ۼ�ѭ����������
        /// </summary>
        /// <param name="p_ctlControl"></param>
        protected void m_mthSetRichTextBoxAttribInControlWithIDandName(Control p_ctlControl)
        {
            if (p_ctlControl.GetType().FullName == "com.digitalwave.controls.ctlRichTextBox")
            {
                m_mthSetRichTextBoxAttribWithIDandName((com.digitalwave.controls.ctlRichTextBox)p_ctlControl);
            }

            if (p_ctlControl.HasChildren && p_ctlControl.GetType().Name != "DataGrid")
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthSetRichTextBoxAttribInControlWithIDandName(subcontrol);
                }
            }
        }
        #endregion �����޺ۼ�����Richtextbox���û�ID���û�����

        #endregion �޸ĺۼ��������
        /// <summary>
        /// ��鲡���Ƿ��Ѿ�����Ժ(true������Ժ��false=δ����Ժ)
        /// </summary>
        /// <param name="p_strRegisterId"></param>
        /// <returns></returns>
        protected bool m_blnCheckPatientIsOut(string p_strRegisterId)
        {
            clsHospitalManagerDomain objDomain = new clsHospitalManagerDomain();
            bool blnIsOut = false;
            long lngRes = objDomain.m_lngCheckPatientIsOut(p_strRegisterId, out blnIsOut);
            return blnIsOut;
        }

        #region �޸Ļ�������
        private void m_cmdModifyPatientInfo_Click(object sender, EventArgs e)
        {
            if (m_objBaseCurrentPatient == null)
            {
                MessageBox.Show("��ѡ���ߣ�");
                return;
            }
            frmEditPatientBasInfo objInfo = new frmEditPatientBasInfo(m_objBaseCurrentPatient.m_StrRegisterId, false);
            objInfo.FormClosed += new FormClosedEventHandler(objInfo_FormClosed);
            objInfo.ShowDialog(this);
        }

        private void objInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_objBaseCurrentPatient != null && m_dlgHandleSaveBeforePrint() == DialogResult.Cancel)
            {
                return;
            }
            m_mthNewAreaSetPatient(m_ObjCurrentBed);
            m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
        }

        private void m_mth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.P)
            {
                m_cmdModifyPatientInfo.PerformClick();
            }
        }
        #endregion �޸Ļ�������

        #region �µĲ���ѡ��
        /// <summary>
        /// ����ѡ��ı���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_ctlAreaPatientSelection_evtAreaChanged(object sender, com.digitalwave.Controls.Domain.EmrControls.clsAreaDataEventArg e)
        {
            com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentDepartment = e.SelectedArea;
            com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjSelectArea = e.SelectedArea;

            if (m_objBaseCurrentPatient != null && m_dlgHandleSaveBeforePrint() == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            this.m_mthAreaChanged(e.SelectedArea);
        }

        /// <summary>
        /// ����ѡ��ı���¼�
        /// ѡ���������Ӵ���ʵ��(�����Ӵ��壬��һ�㻤���¼����¼��)
        /// </summary>
        /// <param name="p_objSelectedArea">ѡ��Ĳ���</param>
        protected virtual void m_mthAreaChanged(clsEmrDept_VO p_objSelectedArea)
        {
            //if (m_objBaseCurrentPatient != null && m_dlgHandleSaveBeforePrint() == DialogResult.Cancel)
            //    return;

            if (m_blnCheckRecordBase(this))
                return;
            this.m_mthClearPatientBaseInfoButArea();
            this.m_mthClearAllInfo(this);
        }
        /// <summary>
        /// �����Ÿı���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_ctlAreaPatientSelection_evtBedChanged(object sender, com.digitalwave.Controls.Domain.EmrControls.clsBedDataEventArg e)
        {
            try
            {
                if (m_objBaseCurrentPatient != null && m_dlgHandleSaveBeforePrint() == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                this.m_mthNewAreaSetPatient(e.CurrentBed);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                e.Cancel = true;
            }
        }
        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        /// <param name="p_objEmrBed"></param>
        private void m_mthNewAreaSetPatient(clsEmrBed_VO p_objEmrBed)
        {
            if (m_blnCheckRecordBase(this)) return;

            clsPatient objPatient = new clsPatient(p_objEmrBed);

            string strTemp = objPatient.m_StrPatientID;
            string strTempDeptID = objPatient.m_strDeptNewID;
            string strTempAreaID = objPatient.m_strAreaNewID;
            string strBedCode = objPatient.m_strBedCode;

            if (m_blnCheckSamePatientForm(objPatient.m_StrInPatientID))
                return;

            this.m_mthClearPatientBaseInfoButArea();
            this.m_mthClearAllInfo(this);

            #region ת��VO
            //����com.digitalwave.BEDExplorer.frmHRPExplorer.objpCurrentPatient
            //��Ϊֻ��Ҫͨ��m_strINPATIENTID_CHR������ɵ�clspatient
            //����ʹ��
            clsHospitalManagerDomain objMain = new clsHospitalManagerDomain();
            frmHRPExplorer.objpCurrentPatient = p_objEmrBed.m_objInbedPatient;

            clsEmrDept_VO objDeptNew;
            objMain.m_lngGetSpecialDeptInfo(strTempDeptID, out objDeptNew);
            frmHRPExplorer.objpCurrentDepartment = objDeptNew;

            clsEmrDept_VO objAreNew;
            objMain.m_lngGetSpecialAreaInfo(strTempAreaID, out objAreNew);
            frmHRPExplorer.objpCurrentArea = objAreNew;
            #endregion

            m_mthSetPatientInfo(objPatient);
            m_ctlPatientInfo.m_mthClearText();
            m_ctlPatientInfo.m_mthSetPatientBaseInfo(p_objEmrBed);
            m_mthAddFormStatusForClosingSave();
        }
        /// <summary>
        /// ��Ժ���ڸı���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_ctlAreaPatientSelection_evtSessionSelected(object sender, com.digitalwave.Controls.Domain.EmrControls.clsSessionEventArg e)
        {
            if (m_objBaseCurrentPatient != null && m_dlgHandleSaveBeforePrint() == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
            this.m_mthPerformSessionChanged(e.SelectedSession, e.Index);
        }

        private void m_ctlAreaPatientSelection_evtRefreshResult(object sender, com.digitalwave.Controls.Domain.EmrControls.clsBedDataEventArg e)
        {
            if (m_objBaseCurrentPatient != null && m_dlgHandleSaveBeforePrint() == DialogResult.Cancel)
            {
                return;
            }
            m_mthNewAreaSetPatient(e.CurrentBed);
            m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
        }
        /// <summary>
        /// ��Ժ���ڸı���¼�����ִ��(������ʵ��)
        /// </summary>
        /// <param name="p_objSelectedSession">��ǰѡ���סԺ��¼</param>
        /// <param name="p_intIndex">��ǰ��¼�����м�¼�������</param>
        protected virtual void m_mthPerformSessionChanged(clsEmrPatientSessionInfo_VO p_objSelectedSession, int p_intIndex)
        {
        }

        /// <summary>
        /// ��ȡ��ǰסԺ��¼
        /// </summary>
        public clsEmrPatientSessionInfo_VO m_ObjCurrentEmrPatientSession
        {

            get
            {
                return m_ctlAreaPatientSelection.CurrentSessionInfo;
            }
        }

        /// <summary>
        /// ��ȡ��ǰ������Ϣ
        /// </summary>
        public clsEmrPatient_VO m_ObjCurrentEmrPatient
        {
            get
            {
                return m_ctlPatientInfo.CurrentEmrPatient;
            }
        }



        /// <summary>
        ///  ��ȡ��ǰ��λ�����ߣ�
        /// </summary>
        protected clsEmrBed_VO m_ObjCurrentBed
        {
            get
            {
                return m_ctlAreaPatientSelection.CurrentBed;
            }
        }

        /// <summary>
        ///  ��ȡ��ǰ����
        /// </summary>
        protected clsEmrDept_VO m_ObjCurrentArea
        {
            get
            {
                return m_ctlAreaPatientSelection.CurrentArea;
            }
        }

        /// <summary>
        /// ��ȡ���һ��סԺ��¼
        /// </summary>
        protected clsEmrPatientSessionInfo_VO m_ObjLastEmrPatientSession
        {
            get
            {
                return m_ctlAreaPatientSelection.m_objGetLastPatientSession();
            }
        }
        #endregion �µĲ���ѡ��

        #region infInactiveRecord (���������߼�)

        public bool m_blnReused(clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            if (this.MdiParent == null)
            {
                this.MdiParent = clsEMR_StaticObject.s_FrmMDI;
                this.WindowState = FormWindowState.Maximized;
            }
            this.Activate();
            return m_blnSubReuse(p_objSelectedValue);
        }
        /// <summary>
        /// �ɼ̳���ʵ���������õ��߼�
        /// </summary>
        /// <param name="p_objSelectedValue"></param>
        /// <returns></returns>
        protected virtual bool m_blnSubReuse(clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            return false;
        }

        public void m_mthInitForm(clsEmrInBedPatient_VO p_objPatient)
        {
            m_mthSubInitForm(p_objPatient);
        }
        /// <summary>
        /// ���û���
        /// </summary>
        /// <param name="p_objPatient"></param>
        protected virtual void m_mthSubInitForm(clsEmrInBedPatient_VO p_objPatient)
        {
            if (this.m_ObjCurrentEmrPatientSession != null && this.m_ObjCurrentEmrPatientSession.m_strRegisterId == p_objPatient.m_strREGISTERID_CHR) return;
            this.m_ctlAreaPatientSelection.m_mthSetPatient(p_objPatient.m_strAREAID_CHR, p_objPatient.m_strPATIENTID_CHR, p_objPatient.m_strREGISTERID_CHR);
        }

        public void m_mthPreviewInactiveRecord(IWin32Window p_infOwner, clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            m_mthSubPreviewInactiveRecord(p_infOwner, p_objSelectedValue);
        }
        /// <summary>
        /// �ɼ̳���ʵ��Ԥ�����߼�
        /// </summary>
        /// <param name="p_infOwner"></param>
        /// <param name="p_objSelectedValue"></param>
        protected virtual void m_mthSubPreviewInactiveRecord(IWin32Window p_infOwner, clsInactiveRecordInfo_VO p_objSelectedValue)
        {
        }

        public clsInactiveRecordInfo_VO[] m_objGetAllInactiveInfo(clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            return m_objSubGetAllInactiveInfo(p_objSelectedValue);
        }
        /// <summary>
        /// �ɼ̳���ʵ�ֻ�ȡ�������ϼ�¼�߼�
        /// </summary>
        /// <returns></returns>
        protected virtual clsInactiveRecordInfo_VO[] m_objSubGetAllInactiveInfo(clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            return null;
        }
        /// <summary>
        /// get sub form Type
        /// </summary>
        public Type m_typForm
        {
            get { return this.GetType(); }
        }

        public weCare.Core.Entity.clsEmrPatientSessionInfo_VO m_objGetSessionInfo
        {
            get { return m_ObjCurrentEmrPatientSession; }
        }

        #region ��������
        private bool m_blnPatientSelected = false;
        public void m_mthSearchDeactiveInfo()
        {
            if (m_IntFormID > -1)
            {
                m_blnPatientSelected = this.m_objBaseCurrentPatient != null;

                frmDeactiveRecord frmDeactive = new frmDeactiveRecord();
                frmDeactive.m_mthSetForm(this, m_objBaseCurrentPatient);
                frmDeactive.ShowDialog(this);
            }
        }
        /// <summary>
        /// ����������������
        /// </summary>
        /// <param name="p_objPatient">���ˣ����סԺ����Ϣ��</param>
        /// <param name="p_dtmRecordDate">��¼����</param>
        /// <param name="p_intFormID">����ID</param>
        public void m_mthSetDeactiveContent(clsPatient p_objPatient, DateTime p_dtmRecordDate, int p_intFormID)
        {
            if (p_objPatient == null)
                return;

            if (!m_blnPatientSelected)
            {
                m_mthSetPatient(p_objPatient);
            }

            m_mthGetDeactiveContent(p_dtmRecordDate, p_intFormID);
        }

        /// <summary>
        /// ��ȡ��ǰ���˵���������
        /// </summary>
        /// <param name="p_dtmRecordDate">��¼����</param>
        /// <param name="p_intFormID">����ID</param>
        protected virtual void m_mthGetDeactiveContent(DateTime p_dtmRecordDate, int p_intFormID)
        {
        }

        /// <summary>
        /// ����ID��ֻ����������������Ĵ���
        /// </summary>
        public virtual int m_IntFormID
        {
            get
            {
                return -1;
            }
        }
        #endregion
        /// <summary>
        /// �Ƿ�ʵ�����µ����������߼���Ϊ�˼��ݾɰ汾
        /// </summary>
        public virtual bool m_blnIsNewSetInactiveForm
        {
            get { return false; }
        }
        #endregion
    }
}
