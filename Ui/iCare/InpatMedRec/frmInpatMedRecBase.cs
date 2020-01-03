using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;
using HRP;
using com.digitalwave.Utility.Controls;
using System.Drawing.Printing;
using com.digitalwave.Emr.Signature_gui;
namespace iCare
{
    /// <summary>
    /// ר�Ʋ�������
    /// </summary>
    public class frmInpatMedRecBase : frmHRPBaseForm, PublicFunction
    {
        #region Define
        protected DateTime m_dtmOutHospitalDate;
        private bool m_blnCanTreeAfterSelect;
        /// <summary>
        /// ��ǰ��¼����
        /// </summary>
        //private clsInpatMedRecContent m_objCurrentRecord;
        protected string m_strCreateUserID = string.Empty;
        protected DateTime m_dtmOpenDate = DateTime.MinValue;
        protected TreeNode m_trnRoot;
        protected clsPatient m_objCurrentPatient;
        protected DateTime m_dtmFirstPrintDate;
        // ����Ƿ��״δ�ӡ
        protected bool m_blnIsFirstPrint;
        /// <summary>
        /// ������Ŀ�б�
        /// </summary>
        private ArrayList m_arlItems = new ArrayList();
        protected clsInpatMedRecDomain m_objDomain;
        protected clsEmployeeSignTool m_objSignTool;
        //����ǩ����
        protected clsEmrSignToolCollection m_objSign;
        /// <summary>
        /// ��¼�Ѿ����������
        /// </summary>
        protected Dictionary<string, clsInpatMedRec_Item> m_objCurrentContent = new Dictionary<string, clsInpatMedRec_Item>(50);

        private System.ComponentModel.IContainer components = null;
        public PinkieControls.ButtonXP m_cmdCreateID;
        protected System.Windows.Forms.Panel m_pnlContent;
        public System.Windows.Forms.TreeView trvTime;
        protected System.Windows.Forms.ContextMenu m_cmuRichTextBoxMenu;
        private System.Windows.Forms.MenuItem mniDoubleStrikeOutDelete;
        public com.digitalwave.Utility.Controls.ctlTimePicker m_dtpCreateDate;
        public System.Windows.Forms.Label lblCreateDate;
        public System.Windows.Forms.Label lblNativePlace;
        public System.Windows.Forms.Label m_lblNativePlace;
        public System.Windows.Forms.Label lblOccupation;
        public System.Windows.Forms.Label m_lblOccupation;
        public System.Windows.Forms.Label m_lblMarriaged;
        public System.Windows.Forms.Label lblMarriaged;
        public System.Windows.Forms.Label m_lblLinkMan;
        public System.Windows.Forms.Label lblLinkMan;
        public System.Windows.Forms.Label lblAddress;
        public System.Windows.Forms.Label m_lblAddress;
        protected System.Windows.Forms.Label lblRepresentor;
        protected System.Windows.Forms.Label lblCredibility;
        protected com.digitalwave.Utility.Controls.ctlComboBox m_cboRepresentor;
        protected com.digitalwave.Utility.Controls.ctlComboBox m_cboCredibility;
        protected bool m_blnAlreadySetPrintTools = false;
        protected System.Windows.Forms.ListView m_lsvEmployee;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        protected System.Drawing.Printing.PrintDocument m_pdcPrintDocument;
        protected ListView lsvSign;
        private ColumnHeader columnHeader1;
        protected TextBox m_txtSign;
        #endregion

        #region Designer generated code

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
                if (m_arlItems != null)
                {
                    m_arlItems.Clear();
                    m_arlItems = null;
                }
                this.m_objDomain = null;
                this.m_objSignTool = null;
                if (m_pdcPrintDocument != null)
                {
                    m_pdcPrintDocument.Dispose();
                    m_pdcPrintDocument = null;
                }
                this.m_objCurrentPatient = null;
                if (m_objCurrentContent != null)
                {
                    m_objCurrentContent.Clear();
                }
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("��Ժ����");
            this.trvTime = new System.Windows.Forms.TreeView();
            this.m_cmuRichTextBoxMenu = new System.Windows.Forms.ContextMenu();
            this.mniDoubleStrikeOutDelete = new System.Windows.Forms.MenuItem();
            this.m_dtpCreateDate = new com.digitalwave.Utility.Controls.ctlTimePicker();
            this.lblCreateDate = new System.Windows.Forms.Label();
            this.lblNativePlace = new System.Windows.Forms.Label();
            this.m_lblNativePlace = new System.Windows.Forms.Label();
            this.lblOccupation = new System.Windows.Forms.Label();
            this.m_lblOccupation = new System.Windows.Forms.Label();
            this.m_lblMarriaged = new System.Windows.Forms.Label();
            this.lblMarriaged = new System.Windows.Forms.Label();
            this.m_lblLinkMan = new System.Windows.Forms.Label();
            this.lblLinkMan = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.m_lblAddress = new System.Windows.Forms.Label();
            this.m_cmdCreateID = new PinkieControls.ButtonXP();
            this.m_pnlContent = new System.Windows.Forms.Panel();
            this.lblRepresentor = new System.Windows.Forms.Label();
            this.lblCredibility = new System.Windows.Forms.Label();
            this.m_cboRepresentor = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_cboCredibility = new com.digitalwave.Utility.Controls.ctlComboBox();
            this.m_lsvEmployee = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.m_pdcPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.m_txtSign = new System.Windows.Forms.TextBox();
            this.lsvSign = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_pnlNewBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(640, 12);
            this.lblSex.Size = new System.Drawing.Size(40, 19);
            // 
            // lblAge
            // 
            this.lblAge.Location = new System.Drawing.Point(724, 12);
            this.lblAge.Size = new System.Drawing.Size(36, 19);
            // 
            // lblBedNoTitle
            // 
            this.lblBedNoTitle.Location = new System.Drawing.Point(360, 16);
            // 
            // lblInHospitalNoTitle
            // 
            this.lblInHospitalNoTitle.Location = new System.Drawing.Point(348, 36);
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.Location = new System.Drawing.Point(484, 16);
            // 
            // lblSexTitle
            // 
            this.lblSexTitle.Location = new System.Drawing.Point(600, 12);
            // 
            // lblAgeTitle
            // 
            this.lblAgeTitle.Location = new System.Drawing.Point(680, 12);
            // 
            // lblAreaTitle
            // 
            this.lblAreaTitle.Location = new System.Drawing.Point(184, 36);
            // 
            // m_lsvInPatientID
            // 
            this.m_lsvInPatientID.Location = new System.Drawing.Point(404, 60);
            this.m_lsvInPatientID.Size = new System.Drawing.Size(80, 104);
            // 
            // txtInPatientID
            // 
            this.txtInPatientID.Location = new System.Drawing.Point(404, 36);
            this.txtInPatientID.Size = new System.Drawing.Size(80, 23);
            this.txtInPatientID.TabIndex = 3;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(528, 12);
            this.m_txtPatientName.Size = new System.Drawing.Size(76, 23);
            this.m_txtPatientName.TabIndex = 2;
            // 
            // m_txtBedNO
            // 
            this.m_txtBedNO.Location = new System.Drawing.Point(404, 12);
            this.m_txtBedNO.Size = new System.Drawing.Size(56, 23);
            // 
            // m_cboArea
            // 
            this.m_cboArea.Location = new System.Drawing.Point(232, 32);
            this.m_cboArea.Size = new System.Drawing.Size(112, 23);
            // 
            // m_lsvPatientName
            // 
            this.m_lsvPatientName.Location = new System.Drawing.Point(528, 36);
            this.m_lsvPatientName.Size = new System.Drawing.Size(76, 104);
            // 
            // m_lsvBedNO
            // 
            this.m_lsvBedNO.Location = new System.Drawing.Point(404, 36);
            this.m_lsvBedNO.Size = new System.Drawing.Size(80, 104);
            // 
            // m_cboDept
            // 
            this.m_cboDept.Location = new System.Drawing.Point(232, 8);
            this.m_cboDept.Size = new System.Drawing.Size(112, 23);
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(184, 12);
            // 
            // m_cmdNext
            // 
            this.m_cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_cmdNext.Location = new System.Drawing.Point(460, 12);
            this.m_cmdNext.Visible = true;
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Location = new System.Drawing.Point(791, 8);
            this.m_lblForTitle.Size = new System.Drawing.Size(16, 24);
            this.m_lblForTitle.Text = "ס Ժ �� ��";
            // 
            // m_ctlPatientInfo
            // 
            this.m_ctlPatientInfo.m_BlnIsShowPatientAge = true;
            this.m_ctlPatientInfo.m_BlnIsShowPatientName = true;
            this.m_ctlPatientInfo.m_BlnIsShowSex = true;
            // 
            // trvTime
            // 
            this.trvTime.BackColor = System.Drawing.Color.White;
            this.trvTime.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvTime.ForeColor = System.Drawing.Color.Black;
            this.trvTime.HideSelection = false;
            this.trvTime.Location = new System.Drawing.Point(4, 8);
            this.trvTime.Name = "trvTime";
            treeNode2.Name = "";
            treeNode2.Text = "��Ժ����";
            this.trvTime.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.trvTime.ShowRootLines = false;
            this.trvTime.Size = new System.Drawing.Size(180, 52);
            this.trvTime.TabIndex = 10000088;
            this.trvTime.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvTime_AfterSelect);
            // 
            // m_cmuRichTextBoxMenu
            // 
            this.m_cmuRichTextBoxMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniDoubleStrikeOutDelete});
            // 
            // mniDoubleStrikeOutDelete
            // 
            this.mniDoubleStrikeOutDelete.Index = 0;
            this.mniDoubleStrikeOutDelete.Text = "˫����ɾ��";
            this.mniDoubleStrikeOutDelete.Click += new System.EventHandler(this.mniDoubleStrikeOutDelete_Click);
            // 
            // m_dtpCreateDate
            // 
            this.m_dtpCreateDate.BorderColor = System.Drawing.Color.Black;
            this.m_dtpCreateDate.CustomFormat = "yyyy��MM��dd�� HH:mm:ss";
            this.m_dtpCreateDate.DropButtonBackColor = System.Drawing.Color.Gainsboro;
            this.m_dtpCreateDate.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_dtpCreateDate.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_dtpCreateDate.flatFont = new System.Drawing.Font("����", 12F);
            this.m_dtpCreateDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpCreateDate.Location = new System.Drawing.Point(72, 64);
            this.m_dtpCreateDate.m_BlnOnlyTime = false;
            this.m_dtpCreateDate.m_EnmVisibleFlag = com.digitalwave.Utility.Controls.ctlTimePicker.enmDateTimeFlag.Second;
            this.m_dtpCreateDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.m_dtpCreateDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.m_dtpCreateDate.Name = "m_dtpCreateDate";
            this.m_dtpCreateDate.ReadOnly = false;
            this.m_dtpCreateDate.Size = new System.Drawing.Size(212, 22);
            this.m_dtpCreateDate.TabIndex = 5;
            this.m_dtpCreateDate.TextBackColor = System.Drawing.Color.White;
            this.m_dtpCreateDate.TextForeColor = System.Drawing.Color.Black;
            // 
            // lblCreateDate
            // 
            this.lblCreateDate.AutoSize = true;
            this.lblCreateDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCreateDate.Location = new System.Drawing.Point(4, 64);
            this.lblCreateDate.Name = "lblCreateDate";
            this.lblCreateDate.Size = new System.Drawing.Size(70, 14);
            this.lblCreateDate.TabIndex = 532;
            this.lblCreateDate.Text = "��¼����:";
            // 
            // lblNativePlace
            // 
            this.lblNativePlace.AutoSize = true;
            this.lblNativePlace.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNativePlace.Location = new System.Drawing.Point(4, 120);
            this.lblNativePlace.Name = "lblNativePlace";
            this.lblNativePlace.Size = new System.Drawing.Size(42, 14);
            this.lblNativePlace.TabIndex = 543;
            this.lblNativePlace.Text = "����:";
            // 
            // m_lblNativePlace
            // 
            this.m_lblNativePlace.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblNativePlace.Location = new System.Drawing.Point(76, 120);
            this.m_lblNativePlace.Name = "m_lblNativePlace";
            this.m_lblNativePlace.Size = new System.Drawing.Size(264, 20);
            this.m_lblNativePlace.TabIndex = 542;
            // 
            // lblOccupation
            // 
            this.lblOccupation.AutoSize = true;
            this.lblOccupation.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOccupation.Location = new System.Drawing.Point(336, 92);
            this.lblOccupation.Name = "lblOccupation";
            this.lblOccupation.Size = new System.Drawing.Size(42, 14);
            this.lblOccupation.TabIndex = 541;
            this.lblOccupation.Text = "ְҵ:";
            // 
            // m_lblOccupation
            // 
            this.m_lblOccupation.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblOccupation.Location = new System.Drawing.Point(380, 92);
            this.m_lblOccupation.Name = "m_lblOccupation";
            this.m_lblOccupation.Size = new System.Drawing.Size(56, 20);
            this.m_lblOccupation.TabIndex = 545;
            // 
            // m_lblMarriaged
            // 
            this.m_lblMarriaged.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblMarriaged.Location = new System.Drawing.Point(76, 92);
            this.m_lblMarriaged.Name = "m_lblMarriaged";
            this.m_lblMarriaged.Size = new System.Drawing.Size(60, 20);
            this.m_lblMarriaged.TabIndex = 544;
            // 
            // lblMarriaged
            // 
            this.lblMarriaged.AutoSize = true;
            this.lblMarriaged.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMarriaged.Location = new System.Drawing.Point(32, 92);
            this.lblMarriaged.Name = "lblMarriaged";
            this.lblMarriaged.Size = new System.Drawing.Size(42, 14);
            this.lblMarriaged.TabIndex = 535;
            this.lblMarriaged.Text = "���:";
            // 
            // m_lblLinkMan
            // 
            this.m_lblLinkMan.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblLinkMan.Location = new System.Drawing.Point(640, 64);
            this.m_lblLinkMan.Name = "m_lblLinkMan";
            this.m_lblLinkMan.Size = new System.Drawing.Size(56, 20);
            this.m_lblLinkMan.TabIndex = 539;
            // 
            // lblLinkMan
            // 
            this.lblLinkMan.AutoSize = true;
            this.lblLinkMan.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLinkMan.Location = new System.Drawing.Point(584, 64);
            this.lblLinkMan.Name = "lblLinkMan";
            this.lblLinkMan.Size = new System.Drawing.Size(56, 14);
            this.lblLinkMan.TabIndex = 540;
            this.lblLinkMan.Text = "��ϵ��:";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAddress.Location = new System.Drawing.Point(340, 120);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(42, 14);
            this.lblAddress.TabIndex = 537;
            this.lblAddress.Text = "��ַ:";
            // 
            // m_lblAddress
            // 
            this.m_lblAddress.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblAddress.Location = new System.Drawing.Point(384, 120);
            this.m_lblAddress.Name = "m_lblAddress";
            this.m_lblAddress.Size = new System.Drawing.Size(364, 20);
            this.m_lblAddress.TabIndex = 538;
            // 
            // m_cmdCreateID
            // 
            this.m_cmdCreateID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.m_cmdCreateID.DefaultScheme = true;
            this.m_cmdCreateID.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdCreateID.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cmdCreateID.Hint = "";
            this.m_cmdCreateID.Location = new System.Drawing.Point(136, 88);
            this.m_cmdCreateID.Name = "m_cmdCreateID";
            this.m_cmdCreateID.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCreateID.Size = new System.Drawing.Size(94, 28);
            this.m_cmdCreateID.TabIndex = 10000080;
            this.m_cmdCreateID.Tag = "1";
            this.m_cmdCreateID.Text = "��ʷ��¼��:";
            // 
            // m_pnlContent
            // 
            this.m_pnlContent.Location = new System.Drawing.Point(4, 144);
            this.m_pnlContent.Name = "m_pnlContent";
            this.m_pnlContent.Size = new System.Drawing.Size(831, 464);
            this.m_pnlContent.TabIndex = 10000081;
            // 
            // lblRepresentor
            // 
            this.lblRepresentor.AutoSize = true;
            this.lblRepresentor.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRepresentor.Location = new System.Drawing.Point(284, 64);
            this.lblRepresentor.Name = "lblRepresentor";
            this.lblRepresentor.Size = new System.Drawing.Size(56, 14);
            this.lblRepresentor.TabIndex = 10000085;
            this.lblRepresentor.Text = "������:";
            // 
            // lblCredibility
            // 
            this.lblCredibility.AutoSize = true;
            this.lblCredibility.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCredibility.Location = new System.Drawing.Point(428, 64);
            this.lblCredibility.Name = "lblCredibility";
            this.lblCredibility.Size = new System.Drawing.Size(56, 14);
            this.lblCredibility.TabIndex = 10000084;
            this.lblCredibility.Text = "�ɿ���:";
            // 
            // m_cboRepresentor
            // 
            this.m_cboRepresentor.BackColor = System.Drawing.Color.White;
            this.m_cboRepresentor.BorderColor = System.Drawing.Color.Black;
            this.m_cboRepresentor.DropButtonBackColor = System.Drawing.Color.Gainsboro;
            this.m_cboRepresentor.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboRepresentor.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboRepresentor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboRepresentor.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboRepresentor.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboRepresentor.ForeColor = System.Drawing.Color.White;
            this.m_cboRepresentor.ListBackColor = System.Drawing.Color.White;
            this.m_cboRepresentor.ListForeColor = System.Drawing.Color.Black;
            this.m_cboRepresentor.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboRepresentor.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboRepresentor.Location = new System.Drawing.Point(336, 60);
            this.m_cboRepresentor.m_BlnEnableItemEventMenu = true;
            this.m_cboRepresentor.Name = "m_cboRepresentor";
            this.m_cboRepresentor.SelectedIndex = -1;
            this.m_cboRepresentor.SelectedItem = null;
            this.m_cboRepresentor.SelectionStart = 0;
            this.m_cboRepresentor.Size = new System.Drawing.Size(92, 23);
            this.m_cboRepresentor.TabIndex = 10000082;
            this.m_cboRepresentor.TextBackColor = System.Drawing.Color.White;
            this.m_cboRepresentor.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_cboCredibility
            // 
            this.m_cboCredibility.BackColor = System.Drawing.Color.White;
            this.m_cboCredibility.BorderColor = System.Drawing.Color.Black;
            this.m_cboCredibility.DropButtonBackColor = System.Drawing.Color.Gainsboro;
            this.m_cboCredibility.DropButtonCursor = System.Windows.Forms.Cursors.Hand;
            this.m_cboCredibility.DropButtonForeColor = System.Drawing.Color.Black;
            this.m_cboCredibility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.m_cboCredibility.flatFont = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboCredibility.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_cboCredibility.ForeColor = System.Drawing.Color.Black;
            this.m_cboCredibility.ListBackColor = System.Drawing.Color.White;
            this.m_cboCredibility.ListForeColor = System.Drawing.Color.Black;
            this.m_cboCredibility.ListSelectedBackColor = System.Drawing.Color.Blue;
            this.m_cboCredibility.ListSelectedForeColor = System.Drawing.Color.White;
            this.m_cboCredibility.Location = new System.Drawing.Point(484, 60);
            this.m_cboCredibility.m_BlnEnableItemEventMenu = true;
            this.m_cboCredibility.Name = "m_cboCredibility";
            this.m_cboCredibility.SelectedIndex = -1;
            this.m_cboCredibility.SelectedItem = null;
            this.m_cboCredibility.SelectionStart = 0;
            this.m_cboCredibility.Size = new System.Drawing.Size(96, 23);
            this.m_cboCredibility.TabIndex = 10000083;
            this.m_cboCredibility.TextBackColor = System.Drawing.Color.White;
            this.m_cboCredibility.TextForeColor = System.Drawing.Color.Black;
            // 
            // m_lsvEmployee
            // 
            this.m_lsvEmployee.BackColor = System.Drawing.Color.White;
            this.m_lsvEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lsvEmployee.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7});
            this.m_lsvEmployee.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lsvEmployee.ForeColor = System.Drawing.Color.Black;
            this.m_lsvEmployee.FullRowSelect = true;
            this.m_lsvEmployee.GridLines = true;
            this.m_lsvEmployee.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_lsvEmployee.Location = new System.Drawing.Point(72, 12);
            this.m_lsvEmployee.Name = "m_lsvEmployee";
            this.m_lsvEmployee.Size = new System.Drawing.Size(28, 26);
            this.m_lsvEmployee.TabIndex = 10000087;
            this.m_lsvEmployee.UseCompatibleStateImageBehavior = false;
            this.m_lsvEmployee.View = System.Windows.Forms.View.Details;
            this.m_lsvEmployee.Visible = false;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Width = 0;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Width = 100;
            // 
            // m_pdcPrintDocument
            // 
            this.m_pdcPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.m_pdcPrintDocument_PrintPage);
            this.m_pdcPrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_EndPrint);
            this.m_pdcPrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.m_pdcPrintDocument_BeginPrint);
            // 
            // m_txtSign
            // 
            this.m_txtSign.BackColor = System.Drawing.Color.White;
            this.m_txtSign.Location = new System.Drawing.Point(530, 92);
            this.m_txtSign.Name = "m_txtSign";
            this.m_txtSign.ReadOnly = true;
            this.m_txtSign.Size = new System.Drawing.Size(168, 23);
            this.m_txtSign.TabIndex = 10000089;
            this.m_txtSign.Visible = false;
            // 
            // lsvSign
            // 
            this.lsvSign.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lsvSign.Location = new System.Drawing.Point(232, 91);
            this.lsvSign.Name = "lsvSign";
            this.lsvSign.Size = new System.Drawing.Size(181, 23);
            this.lsvSign.TabIndex = 10000090;
            this.lsvSign.UseCompatibleStateImageBehavior = false;
            this.lsvSign.View = System.Windows.Forms.View.SmallIcon;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 55;
            // 
            // frmInpatMedRecBase
            // 
            this.ClientSize = new System.Drawing.Size(847, 620);
            this.Controls.Add(this.lsvSign);
            this.Controls.Add(this.m_cboCredibility);
            this.Controls.Add(this.lblCredibility);
            this.Controls.Add(this.lblNativePlace);
            this.Controls.Add(this.lblOccupation);
            this.Controls.Add(this.lblMarriaged);
            this.Controls.Add(this.lblLinkMan);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.m_lsvEmployee);
            this.Controls.Add(this.m_cboRepresentor);
            this.Controls.Add(this.m_pnlContent);
            this.Controls.Add(this.m_cmdCreateID);
            this.Controls.Add(this.m_lblNativePlace);
            this.Controls.Add(this.m_lblOccupation);
            this.Controls.Add(this.m_lblMarriaged);
            this.Controls.Add(this.m_lblLinkMan);
            this.Controls.Add(this.m_lblAddress);
            this.Controls.Add(this.m_dtpCreateDate);
            this.Controls.Add(this.trvTime);
            this.Controls.Add(this.lblCreateDate);
            this.Controls.Add(this.lblRepresentor);
            this.Controls.Add(this.m_txtSign);
            this.Name = "frmInpatMedRecBase";
            this.Text = "סԺ����������";
            this.Load += new System.EventHandler(this.frmInpatMedRecBase_Load);
            this.Controls.SetChildIndex(this.m_cmdModifyPatientInfo, 0);
            this.Controls.SetChildIndex(this.m_pnlNewBase, 0);
            this.Controls.SetChildIndex(this.m_txtSign, 0);
            this.Controls.SetChildIndex(this.chkModifyWithoutMatk, 0);
            this.Controls.SetChildIndex(this.lblRepresentor, 0);
            this.Controls.SetChildIndex(this.lblCreateDate, 0);
            this.Controls.SetChildIndex(this.trvTime, 0);
            this.Controls.SetChildIndex(this.m_dtpCreateDate, 0);
            this.Controls.SetChildIndex(this.m_lblAddress, 0);
            this.Controls.SetChildIndex(this.m_lblLinkMan, 0);
            this.Controls.SetChildIndex(this.m_lblMarriaged, 0);
            this.Controls.SetChildIndex(this.m_lblOccupation, 0);
            this.Controls.SetChildIndex(this.m_lblNativePlace, 0);
            this.Controls.SetChildIndex(this.m_cmdCreateID, 0);
            this.Controls.SetChildIndex(this.m_pnlContent, 0);
            this.Controls.SetChildIndex(this.m_cboRepresentor, 0);
            this.Controls.SetChildIndex(this.m_lblForTitle, 0);
            this.Controls.SetChildIndex(this.m_lsvEmployee, 0);
            this.Controls.SetChildIndex(this.txtInPatientID, 0);
            this.Controls.SetChildIndex(this.lblAreaTitle, 0);
            this.Controls.SetChildIndex(this.lblAgeTitle, 0);
            this.Controls.SetChildIndex(this.lblSexTitle, 0);
            this.Controls.SetChildIndex(this.lblNameTitle, 0);
            this.Controls.SetChildIndex(this.lblInHospitalNoTitle, 0);
            this.Controls.SetChildIndex(this.lblBedNoTitle, 0);
            this.Controls.SetChildIndex(this.lblAge, 0);
            this.Controls.SetChildIndex(this.lblSex, 0);
            this.Controls.SetChildIndex(this.m_lsvInPatientID, 0);
            this.Controls.SetChildIndex(this.m_txtPatientName, 0);
            this.Controls.SetChildIndex(this.m_txtBedNO, 0);
            this.Controls.SetChildIndex(this.m_lsvPatientName, 0);
            this.Controls.SetChildIndex(this.m_lsvBedNO, 0);
            this.Controls.SetChildIndex(this.lblAddress, 0);
            this.Controls.SetChildIndex(this.lblLinkMan, 0);
            this.Controls.SetChildIndex(this.lblMarriaged, 0);
            this.Controls.SetChildIndex(this.lblOccupation, 0);
            this.Controls.SetChildIndex(this.lblNativePlace, 0);
            this.Controls.SetChildIndex(this.lblDept, 0);
            this.Controls.SetChildIndex(this.m_cboDept, 0);
            this.Controls.SetChildIndex(this.m_cmdNewTemplate, 0);
            this.Controls.SetChildIndex(this.m_cmdNext, 0);
            this.Controls.SetChildIndex(this.m_cmdPre, 0);
            this.Controls.SetChildIndex(this.lblCredibility, 0);
            this.Controls.SetChildIndex(this.m_cboArea, 0);
            this.Controls.SetChildIndex(this.m_cboCredibility, 0);
            this.Controls.SetChildIndex(this.lsvSign, 0);
            this.m_pnlNewBase.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public frmInpatMedRecBase()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
            #region ��ʾ��ƴ���ʱע��
            m_objDomain = new clsInpatMedRecDomain();
            //m_objBorderTool = new clsBorderTool(Color.White);
            //m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[] { trvTime });
            m_blnCanTreeAfterSelect = true;

            #region ��ͨ�ð�ǩ��
            m_objSign = new clsEmrSignToolCollection();
            //m_mthBindEmployeeSign(��ť, ǩ����, ҽ��1or��ʿ2, �����֤trueorfalse, Ա��ID);
            //m_objSign.m_mthBindEmployeeSign(m_cmdCreateID, m_txtSign, 1, true, clsEMRLogin.LoginInfo.m_strEmpID);
            m_objSign.m_mthBindEmployeeSign(m_cmdCreateID, lsvSign, 1, false, clsEMRLogin.LoginInfo.m_strEmpID);
            #endregion

            #endregion

        }
        #endregion

        #region ��ղ���
        /// <summary>
        ///  ��ս���
        /// </summary>
        protected void m_mthClearAll()
        {
            //��ղ��˻�����Ϣ
            base.m_mthClearPatientBaseInfo();

            //���ʱ���б���
            if (this.trvTime.Nodes[0].Nodes.Count > 0)
                trvTime.Nodes[0].Nodes.Clear();

            //���õ�ǰ���˱���
            m_objCurrentPatient = null;

            //��յ�ǰ���˼�¼
            m_mthClearPatientRecordInfo();
        }

        /// <summary>
        /// ��ղ��˼�¼������Ϣ
        /// </summary>
        protected void m_mthClearPatientRecordInfo()
        {
            m_mthEnableModify(true);
            //��ռ�¼����                       
            m_mthClearRecordInfo(true);
            //��յ�ǰ��¼�ı���
            m_dtmOpenDate = DateTime.MinValue;
        }

        /// <summary>
        /// ��ռ�¼��Ϣ
        /// </summary>
        protected virtual void m_mthClearRecordInfo(bool p_blnRichTextBoxReadOnly)
        {
            //Ĭ��ǩ��
            //MDIParent.m_mthSetDefaulEmployee(m_txtSign);
            MDIParent.m_mthSetDefaulEmployee(lsvSign);
            this.m_dtpCreateDate.Value = DateTime.Now;
            chkModifyWithoutMatk.Checked = true;
            m_cboCredibility.Text = "";
            m_cboRepresentor.Text = "";
            m_mthRecursiveClearControlValue(m_pnlContent, p_blnRichTextBoxReadOnly);
            m_mthSetModifyControl(null, true);
            m_mthClearSubInfo();
            if (m_objCurrentContent != null)
            {
                m_objCurrentContent.Clear();
            }
            m_objCurrentContent.Clear();
        }
        /// <summary>
        /// ����Ӵ���һЩ�������ݣ�
        /// ���Ӵ���ʵ��
        /// </summary>
        protected virtual void m_mthClearSubInfo()
        {
        }

        /// <summary>
        /// �ݹ���տؼ���Ϣ
        /// </summary>
        /// <param name="p_ctlParent"></param>
        private void m_mthRecursiveClearControlValue(Control p_ctlParent, bool p_blnRichTextBoxReadOnly)
        {
            if (!p_ctlParent.HasChildren || p_ctlParent is ctlComboBox || p_ctlParent is ctlTimePicker || p_ctlParent is ctlPaintContainer || p_ctlParent is com.digitalwave.Controls.ICustomValueControl)
            {
                if (p_ctlParent is com.digitalwave.Controls.ICustomValueControl)
                {
                    com.digitalwave.Controls.ICustomValueControl ctlCustomValue = (com.digitalwave.Controls.ICustomValueControl)p_ctlParent;
                    ctlCustomValue.m_mthClearValue();
                    //if (p_ctlParent is com.digitalwave.Controls.ICustomValueControl<string>)
                    //{
                    //    com.digitalwave.Controls.ICustomValueControl<string> ctlCustomValue = (com.digitalwave.Controls.ICustomValueControl<string>)p_ctlParent;
                    //    ctlCustomValue.m_mthClearValue();
                    //}
                    //else if (p_ctlParent is com.digitalwave.Controls.ICustomValueControl<DateTime>)
                    //{
                    //    com.digitalwave.Controls.ICustomValueControl<DateTime> ctlCustomValue = (com.digitalwave.Controls.ICustomValueControl<DateTime>)p_ctlParent;
                    //    ctlCustomValue.m_mthClearValue();
                    //}
                    //else if (p_ctlParent is com.digitalwave.Controls.ICustomValueControl<bool>)
                    //{
                    //    com.digitalwave.Controls.ICustomValueControl<bool> ctlCustomValue = (com.digitalwave.Controls.ICustomValueControl<bool>)p_ctlParent;
                    //    ctlCustomValue.m_mthClearValue();
                    //}
                }
                else
                {
                    switch (p_ctlParent.GetType().FullName)
                    {
                        case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                            ctlRichTextBox txtRTB = ((ctlRichTextBox)p_ctlParent);
                            txtRTB.m_mthClearText();
                            txtRTB.m_BlnReadOnly = p_blnRichTextBoxReadOnly;
                            break;
                        case "com.digitalwave.controls.ctlRichTextBox":
                            com.digitalwave.controls.ctlRichTextBox txtRTB1 = ((com.digitalwave.controls.ctlRichTextBox)p_ctlParent);
                            txtRTB1.m_mthClearText();
                            txtRTB1.m_BlnReadOnly = p_blnRichTextBoxReadOnly;
                            break;
                        case "com.digitalwave.Utility.Controls.ctlComboBox":
                            ((ctlComboBox)p_ctlParent).SelectedIndex = -1;
                            ((ctlComboBox)p_ctlParent).Text = "";
                            break;
                        case "System.Windows.Forms.CheckBox":
                            ((CheckBox)p_ctlParent).Checked = false;
                            break;
                        case "System.Windows.Forms.RadioButton":
                            ((RadioButton)p_ctlParent).Checked = false;
                            break;
                        case "com.digitalwave.Utility.Controls.ctlBorderTextBox":
                            ((ctlBorderTextBox)p_ctlParent).Text = "";
                            break;
                        case "com.digitalwave.Utility.Controls.ctlTimePicker":
                            ((ctlTimePicker)p_ctlParent).Value = DateTime.Now;
                            break;
                        case "com.digitalwave.Utility.Controls.ctlPaintContainer":
                            ((ctlPaintContainer)p_ctlParent).m_mthClear(true);
                            break;
                        case "System.Windows.Forms.TextBox":
                            ((TextBox)p_ctlParent).Text = "";
                            break;
                        case "System.Windows.Forms.RichTextBox":
                            ((RichTextBox)p_ctlParent).Text = "";
                            break;
                        case "System.Windows.Forms.MaskedTextBox":
                            ((MaskedTextBox)p_ctlParent).Text = "";
                            break;
                        case "System.Windows.Forms.ComboBox":
                            ((ComboBox)p_ctlParent).SelectedIndex = -1;
                            ((ComboBox)p_ctlParent).Text = "";
                            break;
                        //case "SourceLibrary.Windows.Forms.TextBoxTypedNumeric":
                        //    ((SourceLibrary.Windows.Forms.TextBoxTypedNumeric)p_ctlParent).Text = "";
                        //    break;

                        default:
                            break;
                    }
                }
            }
            else
            {
                foreach (Control ctlSub in p_ctlParent.Controls)
                    m_mthRecursiveClearControlValue(ctlSub, p_blnRichTextBoxReadOnly);
            }
        }
        /// <summary>
        /// �Ƿ������޸ļ�¼ʱ��ȼ�¼��Ϣ
        /// </summary>
        /// <param name="p_blnEnable"></param>
        protected void m_mthEnableModify(bool p_blnEnable)
        {
            this.m_dtpCreateDate.Tag = p_blnEnable;
            //			this.m_dtpCreateDate.Enabled = p_blnEnable;
        }
        #endregion

        #region �ݹ����ÿؼ��Ƿ�ֻ��
        /// <summary>
        /// �ݹ����ÿؼ��Ƿ�ֻ��
        /// </summary>
        /// <param name="p_ctlParent"></param>
        private void m_mthRecursiveSetControlReadOnly(Control p_ctlParent, bool p_blnReadOnly)
        {
            if (!p_ctlParent.HasChildren || p_ctlParent is ctlComboBox || p_ctlParent is ctlTimePicker)
            {
                switch (p_ctlParent.GetType().FullName)
                {
                    case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                        ctlRichTextBox txtRTB = ((ctlRichTextBox)p_ctlParent);
                        txtRTB.m_BlnReadOnly = p_blnReadOnly;
                        break;
                    case "com.digitalwave.controls.ctlRichTextBox":
                        com.digitalwave.controls.ctlRichTextBox txtRTB1 = ((com.digitalwave.controls.ctlRichTextBox)p_ctlParent);
                        txtRTB1.m_BlnReadOnly = p_blnReadOnly;
                        break;
                    #region
                    //					case "ctlComboBox":
                    //						((ctlComboBox)p_ctlParent).SelectedIndex = -1;
                    //						break;
                    //					case "CheckBox":
                    //						((CheckBox)p_ctlParent).Checked = false;
                    //						break;
                    //					case "RadioButton":
                    //						((RadioButton)p_ctlParent).Checked = false;
                    //						break;
                    //					case "ctlBorderTextBox":
                    //						((ctlBorderTextBox)p_ctlParent).Text= "";
                    //						break;
                    //					case "ctlTimePicker":
                    //						((ctlTimePicker)p_ctlParent).Value= DateTime.Now;
                    //						break;
                    #endregion
                    default:
                        break;
                }
            }
            else
            {
                foreach (Control ctlSub in p_ctlParent.Controls)
                    m_mthRecursiveSetControlReadOnly(ctlSub, p_blnReadOnly);
            }
        }

        #endregion

        #region �޸ĺۼ��������
        /// <summary>
        /// �����Ƿ�����޸ģ��޸����ۼ�����
        /// </summary>
        /// <param name="p_objRecordContent">������ʵ��</param>
        /// <param name="p_blnReset"></param>
        protected void m_mthSetModifyControl(clsInpatMedRecContent p_objRecordContent,
            bool p_blnReset)
        {
            if (m_BlnNeedContextMenu)
                m_mthAddRichTemplateInContainer(this);
            //������д�淶���þ��崰�����д����
            if (p_blnReset == true)
            {
                m_mthSetRichTextModifyColor(this, clsHRPColor.s_ClrInputFore);
                m_mthSetRichTextCanModifyLast(this, true);
            }
            else if (p_objRecordContent != null)
            {
                bool blnCanModify = m_blnGetCanModifyLast(p_objRecordContent.m_strCreateUserID, p_objRecordContent.m_intMarkStatus);
                if (blnCanModify)
                {
                    m_mthSetRichTextModifyColor(this, clsHRPColor.s_ClrInputFore);
                }
                else
                {
                    m_mthSetRichTextModifyColor(this, Color.Red);
                }
                m_mthSetRichTextCanModifyLast(this, blnCanModify);
            }
        }

        private void m_mthSetRichTextCanModifyLast(Control p_ctlControl, bool p_blnCanModifyLast)
        {
            #region ���ÿؼ������ı����Ƿ�����޸�,Jacky-2003-3-24
            string strTypeName = p_ctlControl.GetType().FullName;
            if (strTypeName == "com.digitalwave.Utility.Controls.ctlRichTextBox")
            {
                ((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlControl).m_BlnCanModifyLast = p_blnCanModifyLast;
            }
            else if (strTypeName == "com.digitalwave.controls.ctlRichTextBox")
            {
                ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_BlnCanModifyLast = p_blnCanModifyLast;
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
        /// ���ô����пؼ������ı�����ɫ
        /// </summary>
        /// <param name="p_ctlControl"></param>
        /// <param name="p_clrColor"></param>
        private void m_mthSetRichTextModifyColor(Control p_ctlControl, System.Drawing.Color p_clrColor)
        {
            #region ���ÿؼ������ı�����ɫ,Jacky-2003-3-24
            string strTypeName = p_ctlControl.GetType().FullName;
            if (strTypeName == "com.digitalwave.Utility.Controls.ctlRichTextBox")
                ((com.digitalwave.Utility.Controls.ctlRichTextBox)p_ctlControl).m_ClrOldPartInsertText = p_clrColor;
            else if (strTypeName == "com.digitalwave.controls.ctlRichTextBox")
            {
                //����/������� ������ʾ��ɫ
                //modify by tfzhang at 2006-03-20
                if (p_ctlControl.Name == "m_ModifyDiagnose" || p_ctlControl.Name == "m_AddtionDiagnose")
                    ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_ClrOldPartInsertText = Color.Red;
                else
                    ((com.digitalwave.controls.ctlRichTextBox)p_ctlControl).m_ClrOldPartInsertText = p_clrColor;
            }

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
        /// ������ڣ�������ɫ�����÷���
        /// ����ü�¼������޸��˾��ǵ�ǰ�ĵ�½�ˣ������޸ĸü�¼
        /// ���򣬲����޸ģ�����6Сʱ�Ŀ��ƣ���richtextbox�����п��ƣ�
        /// </summary>
        /// <param name="p_strModifyUserID"></param>
        /// <param name="p_intMarkStatus">ֱ����MarkStatus�ֶ��ж�</param>
        /// <returns></returns>
        public virtual bool m_blnGetCanModifyLast(string p_strModifyUserID, int p_intMarkStatus)
        {
            if (p_intMarkStatus == 0)
            {
                if (p_strModifyUserID == null || p_strModifyUserID.Trim() == com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjCurrentEmployee.m_strEMPID_CHR)
                {
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
                chkModifyWithoutMatk.Checked = false;
                return false;
            }
        }
        #endregion

        #region ���ò��˱���Ϣ
        /// <summary>
        /// �����ò��˵Ļ�����Ϣ
        /// </summary>
        /// <param name="p_objSelectedPatient"></param>
        protected override void m_mthOnlySetPatientInfo(clsPatient p_objSelectedPatient)
        {
            clsPeopleInfo objPeopleInfo = p_objSelectedPatient.m_ObjPeopleInfo;
            //lblSex.Text = p_objSelectedPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_ObjPeopleInfo.m_StrSex;
            //lblAge.Text = p_objSelectedPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_ObjPeopleInfo.m_StrAge;

            this.m_lblAddress.Text = objPeopleInfo.m_StrHomeAddress;
            this.m_lblLinkMan.Text = objPeopleInfo.m_StrLinkManFirstName;
            this.m_lblMarriaged.Text = objPeopleInfo.m_StrMarried;
            this.m_lblOccupation.Text = objPeopleInfo.m_StrOccupation;
            //this.m_lblNation.Text = objPeopleInfo.m_StrNation;
            this.m_lblNativePlace.Text = objPeopleInfo.m_StrNativePlace;

        }
        /// <summary>
        /// ���ò��˱���Ϣ
        /// </summary>
        /// <param name="p_objSelectedPatient">ѡ�в���</param>
        protected override void m_mthSetPatientFormInfo(clsPatient p_objSelectedPatient)
        {
            //�жϲ�����Ϣ�Ƿ�Ϊnull������ǣ�ֱ�ӷ��ء�
            if (p_objSelectedPatient == null)
                return;

            //��ղ��˼�¼��Ϣ
            m_mthClearPatientRecordInfo();

            ////��¼������Ϣ
            m_objCurrentPatient = p_objSelectedPatient;
            //this.m_lblAddress.Text = m_objCurrentPatient.m_ObjPeopleInfo.m_StrHomeAddress;
            //this.m_lblLinkMan.Text = m_objCurrentPatient.m_ObjPeopleInfo.m_StrLinkManFirstName;
            //this.m_lblMarriaged.Text = m_objCurrentPatient.m_ObjPeopleInfo.m_StrMarried;
            //this.m_lblOccupation.Text = m_objCurrentPatient.m_ObjPeopleInfo.m_StrOccupation;
            ////			this.m_lblCreateUserName.Text =MDIParent.strOperatorName;
            ////����
            //this.m_lblNativePlace.Text = m_objCurrentPatient.m_ObjPeopleInfo.m_StrNativePlace;

            //��ȡ���˼�¼�б�
            string[] strInPatientDateListArr = null;
            string[] strCreateTimeListArr = null;
            string[] strOpenTimeListArr = null;

            long lngRes = m_objDomain.m_lngGetRecordTimeList(this.Name, p_objSelectedPatient.m_StrInPatientID, out strInPatientDateListArr, out strCreateTimeListArr, out strOpenTimeListArr);

            if (lngRes <= 0)
                return;

            ////���ʱ���б�����ʱ��ڵ�   
            //if (trvTime.Nodes[0].Nodes.Count > 0)
            //    trvTime.Nodes[0].Nodes.Clear();
            ////��Ӳ�ѯ������Ժʱ�䵽ʱ������
            //for (int i = p_objSelectedPatient.m_ObjInBedInfo.m_intGetSessionCount() - 1; i >= 0; i--)
            //{
            //    TreeNode trnRecordDate = new TreeNode(p_objSelectedPatient.m_ObjInBedInfo.m_objGetSessionByIndex(i).m_DtmHISInDate.ToString("yyyy��MM��dd�� HH:mm:ss"));
            //    if (strOpenTimeListArr != null)
            //    {
            //        for (int j2 = 0; j2 < strInPatientDateListArr.Length; j2++)
            //        {
            //            if (DateTime.Parse(strInPatientDateListArr[j2]) == p_objSelectedPatient.m_ObjInBedInfo.m_objGetSessionByIndex(i).m_DtmEMRInDate)
            //            {
            //                trnRecordDate.Tag = (string)strOpenTimeListArr[j2];
            //                break;
            //            }
            //        }
            //    }
            //    trvTime.Nodes[0].Nodes.Add(trnRecordDate);
            //    trvTime.ExpandAll();
            //}

            //trvTime.SelectedNode = null;
            ////ѡ��Ĭ�Ͻڵ�
            //for (int i = 0; i < trvTime.Nodes[0].Nodes.Count; i++)
            //{
            //    if (trvTime.Nodes[0].Nodes[i].Text == p_objSelectedPatient.m_DtmSelectedHISInDate.ToString("yyyy��MM��dd�� HH:mm:ss"))
            //        trvTime.SelectedNode = trvTime.Nodes[0].Nodes[i];
            //}
            //if (trvTime.Nodes[0].Nodes.Count > 0 && (trvTime.SelectedNode == null))//������Ҫ�˾����Ĭ��ѡ�����ڵ��¼�				
            //    trvTime.SelectedNode = trvTime.Nodes[0].Nodes[0];

            //if (m_dtpCreateDate.Tag is bool)
            //{
            //    if ((bool)(m_dtpCreateDate.Tag) == false)
            //        m_EnmFormEditStatus = MDIParent.enmFormEditStatus.Modify;
            //}
            //else
            //    m_EnmFormEditStatus = MDIParent.enmFormEditStatus.None;
        }

        #endregion

        #region �ӽ����ȡͼƬ��Ϣ���麯���̳���ʵ��
        /// <summary>
        /// �ӽ����ȡͼƬ��Ϣ���麯���̳���ʵ��
        /// </summary>
        /// <param name="p_objContent"></param>
        protected virtual void m_mthGetPicContent(clsInpatMedRecContent p_objContent)
        {
        }
        #endregion

        #region �������ݵ�����
        /// <summary>
        /// ���ü�¼���ݵ�����
        /// </summary>
        private void m_mthSetSelectedRecord()
        {
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                return;
            clsInpatMedRecContent objCurrentRecord = null;
            long lngRes = m_objDomain.m_lngGetRecordContent(this.Name, m_objCurrentPatient.m_StrInPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), out objCurrentRecord);
            //m_objCurrentRecord = objCurrentRecord;

            if (lngRes <= 0 || objCurrentRecord == null || string.IsNullOrEmpty(objCurrentRecord.m_strInPatientID))
            {
                return;
            }
            m_objCurrentContent = m_objGetCurrentRecord(objCurrentRecord.m_objItemContents);
            m_dtmCreatedDate = objCurrentRecord.m_dtmCreateDate;
            m_strCreateUserID = objCurrentRecord.m_strCreateUserID;
            m_dtmOpenDate = objCurrentRecord.m_dtmOpenDate;
            m_mthSetGUIFromContent(objCurrentRecord);
            this.m_dtpCreateDate.Value = objCurrentRecord.m_dtmRecordDate;
            m_mthEnableModify(false);
            m_mthSetModifyControl(objCurrentRecord, false);
        }

        /// <summary>
        /// �������ݵ�����
        /// </summary>
        /// <param name="p_objContent"></param>
        protected virtual void m_mthSetGUIFromContent(clsInpatMedRecContent p_objContent)
        {
            this.m_dtpCreateDate.Value = p_objContent.m_dtmRecordDate;
            this.m_cboCredibility.Text = p_objContent.m_strCredibility;
            this.m_cboRepresentor.Text = p_objContent.m_strRepresentor;
            //m_objSignTool.m_mtSetSpecialEmployee(p_objContent.m_strCreateUserID);
            //���ݹ��Ż�ȡǩ����Ϣ
            //���ڼ��ݿ��ǣ�����ʹ�� tfzhang 2006-03-12
            com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain objEmployeeSign = new com.digitalwave.emr.BEDExplorer.clsHospitalManagerDomain();
            //if (p_objContent.m_strCreateUserID != null && p_objContent.m_strCreateUserID.Trim().Length != 0)
            //{
            //    clsEmrEmployeeBase_VO objSign4 = new clsEmrEmployeeBase_VO();
            //    objEmployeeSign.m_lngGetEmpByNO(p_objContent.m_strCreateUserID.Trim(), out objSign4);
            //    if (objSign4 != null)
            //    {
            //        m_txtSign.Text = objSign4.m_strGetTechnicalRankAndName;
            //        m_txtSign.Tag = objSign4;
            //        //m_txtSign.Enabled = false;
            //    }

            //}
            //for (int i = 0; i < p_objContent.objSignerArr.Length; i++)
            //{
            //    if (p_objContent.objSignerArr[i].controlName == "m_txtSign")
            //    {
            //        m_txtSign.Text = p_objContent.objSignerArr[i].objEmployee.m_strTECHNICALRANK_CHR + " " + p_objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR;
            //        m_txtSign.Tag = p_objContent.objSignerArr[i].objEmployee;
            //    }
            //}
            m_mthAddSignToListView(lsvSign, p_objContent.objSignerArr);
            m_mthRecursiveSetControlValue(m_pnlContent, p_objContent);
            m_mthSetPicFromContent(p_objContent.m_objPics);
            m_mthSubSetListSigns(p_objContent.objSignerArr);
        }
        /// <summary>
        /// ��Ӷ�ǩ����ListView
        /// ���Ӵ���ʵ��
        /// </summary>
        /// <param name="p_objSignerArr"></param>
        protected virtual void m_mthSubSetListSigns(clsEmrSigns_VO[] p_objSignerArr)
        {
            m_mthAddSignToListView(lsvSign, p_objSignerArr);
        }
        /// <summary>
        /// �ݹ鸳ֵ
        /// </summary>
        /// <param name="p_ctlControl"></param>
        protected virtual void m_mthRecursiveSetControlValue(Control p_ctlParent, clsInpatMedRecContent p_objContents)
        {
            if (p_objContents == null)
                return;
            if (p_objContents.m_objItemContents == null)
                return;
            if (!p_ctlParent.HasChildren || p_ctlParent is ctlComboBox || p_ctlParent is ctlBorderTextBox || p_ctlParent is ctlTimePicker || p_ctlParent is ctlPaintContainer || p_ctlParent is com.digitalwave.Controls.ICustomValueControl)
            {
                int intIndex = -1;
                for (int i = 0; i < p_objContents.m_objItemContents.Length; i++)
                {
                    if (p_objContents.m_objItemContents[i].m_strItemID == p_ctlParent.Name)
                    {
                        intIndex = i;
                        break;
                    }
                }

                if (intIndex != -1)
                {
                    clsInpatMedRec_Item objItem = p_objContents.m_objItemContents[intIndex];
                    if (p_ctlParent is com.digitalwave.Controls.ICustomValueControl)
                    {
                        if (p_ctlParent is com.digitalwave.Controls.ICustomValueControl<string>)
                        {
                            com.digitalwave.Controls.ICustomValueControl<string> ctlCustomValue = (com.digitalwave.Controls.ICustomValueControl<string>)p_ctlParent;
                            ctlCustomValue.m_mthSetValue(new string[] { objItem.m_strItemContent, p_objContents.m_objItemContents[intIndex].m_strItemContentXml, "" });
                        }
                        else if (p_ctlParent is com.digitalwave.Controls.ICustomValueControl<DateTime>)
                        {
                            com.digitalwave.Controls.ICustomValueControl<DateTime> ctlCustomValue = (com.digitalwave.Controls.ICustomValueControl<DateTime>)p_ctlParent;
                            DateTime dtmTemp = DateTime.Parse("1900-1-1");
                            DateTime.TryParse(objItem.m_strItemContent, out dtmTemp);
                            ctlCustomValue.m_mthSetValue(dtmTemp);
                        }
                        else if (p_ctlParent is com.digitalwave.Controls.ICustomValueControl<bool>)
                        {
                            com.digitalwave.Controls.ICustomValueControl<bool> ctlCustomValue = (com.digitalwave.Controls.ICustomValueControl<bool>)p_ctlParent;
                            bool blnTemp = false;
                            bool.TryParse(objItem.m_strItemContent, out blnTemp);
                            ctlCustomValue.m_mthSetValue(blnTemp);
                        }
                        else if (p_ctlParent is com.digitalwave.Controls.ICustomValueControl<decimal>)
                        {
                            com.digitalwave.Controls.ICustomValueControl<decimal> ctlCustomValue = (com.digitalwave.Controls.ICustomValueControl<decimal>)p_ctlParent;
                            decimal dcmTemp = decimal.MinusOne;
                            if (decimal.TryParse(objItem.m_strItemContent, out dcmTemp))
                            {
                                ctlCustomValue.m_mthSetValue(dcmTemp);
                            }
                        }
                    }
                    else
                    {
                        switch (p_ctlParent.GetType().FullName)
                        {
                            case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                                ctlRichTextBox txtRTB = ((ctlRichTextBox)p_ctlParent);
                                txtRTB.m_mthSetNewText(objItem.m_strItemContent, p_objContents.m_objItemContents[intIndex].m_strItemContentXml);
                                break;
                            case "com.digitalwave.controls.ctlRichTextBox":
                                com.digitalwave.controls.ctlRichTextBox txtRTB1 = ((com.digitalwave.controls.ctlRichTextBox)p_ctlParent);
                                if (txtRTB1.Name == "m_AddtionDiagnose" || txtRTB1.Name == "m_ModifyDiagnose")
                                {
                                    txtRTB1.m_mthSetCustomNewText(objItem.m_strItemContent, p_objContents.m_objItemContents[intIndex].m_strItemContentXml, Color.Red);
                                }
                                else
                                {
                                    txtRTB1.m_mthSetNewText(objItem.m_strItemContent, p_objContents.m_objItemContents[intIndex].m_strItemContentXml);
                                }
                                break;
                            case "com.digitalwave.Utility.Controls.ctlComboBox":
                                ((ctlComboBox)p_ctlParent).Text = objItem.m_strItemContent;
                                break;
                            case "System.Windows.Forms.CheckBox":
                                ((CheckBox)p_ctlParent).Checked = bool.Parse(objItem.m_strItemContent);
                                break;
                            case "System.Windows.Forms.RadioButton":
                                ((RadioButton)p_ctlParent).Checked = bool.Parse(objItem.m_strItemContent);
                                break;
                            case "com.digitalwave.Utility.Controls.ctlBorderTextBox":
                                ((ctlBorderTextBox)p_ctlParent).Text = objItem.m_strItemContent;
                                break;
                            case "com.digitalwave.Utility.Controls.ctlTimePicker":
                                try
                                {
                                    ((ctlTimePicker)p_ctlParent).Text = DateTime.Parse(objItem.m_strItemContent).ToString("yyyy��MM��dd�� HH:mm:ss");
                                }
                                catch { ((ctlTimePicker)p_ctlParent).Value = DateTime.Now; }
                                break;
                            case "System.Windows.Forms.DateTimePicker":
                                try
                                {
                                    ((DateTimePicker)p_ctlParent).Text = DateTime.Parse(objItem.m_strItemContent).ToString("yyyy��MM��dd�� HH:mm:ss");
                                }
                                catch { ((DateTimePicker)p_ctlParent).Value = DateTime.Now; }
                                break;
                            case "com.digitalwave.Utility.Controls.ctlPaintContainer":
                                ((ctlPaintContainer)p_ctlParent).m_mthSetPicValue(p_objContents.m_objPics);
                                break;
                            case "System.Windows.Forms.TextBox":
                                ((TextBox)p_ctlParent).Text = objItem.m_strItemContent;
                                break;
                            case "System.Windows.Forms.RichTextBox":
                                ((RichTextBox)p_ctlParent).Text = objItem.m_strItemContent;
                                break;
                            case "System.Windows.Forms.MaskedTextBox":
                                ((MaskedTextBox)p_ctlParent).Text = objItem.m_strItemContent;
                                break;
                            case "System.Windows.Forms.ComboBox":
                                ((ComboBox)p_ctlParent).Text = objItem.m_strItemContent;
                                break;
                            //case "SourceLibrary.Windows.Forms.TextBoxTypedNumeric":
                            //    ((SourceLibrary.Windows.Forms.TextBoxTypedNumeric)p_ctlParent).Text = objItem.m_strItemContent;
                            //    break;
                            default:
                                break;
                        }
                    }
                }
                else if (p_ctlParent is ctlPaintContainer)
                {
                    ((ctlPaintContainer)p_ctlParent).m_mthSetPicValue(p_objContents.m_objPics);
                }
            }
            else
            {
                foreach (Control ctlSub in p_ctlParent.Controls)
                    m_mthRecursiveSetControlValue(ctlSub, p_objContents);
            }
        }
        /// <summary>
        /// ����ͼƬ�����棬�麯���̳���ʵ��
        /// </summary>
        /// <param name="p_objPics"></param>
        protected virtual void m_mthSetPicFromContent(clsPictureBoxValue[] p_objPics)
        {
        }

        protected override void m_mthPerformSessionChanged(clsEmrPatientSessionInfo_VO p_objSelectedSession, int p_intIndex)
        {
            if (p_objSelectedSession == null) return;
            m_mthRecordChangedToSave();

            if (p_objSelectedSession != null)
            {
                m_objBaseCurrentPatient.m_DtmSelectedInDate = p_objSelectedSession.m_dtmEMRInpatientDate;
                m_objCurrentPatient.m_DtmSelectedInDate = p_objSelectedSession.m_dtmEMRInpatientDate;
                m_objCurrentPatient.m_StrHISInPatientID = p_objSelectedSession.m_strHISInpatientId;
                m_objCurrentPatient.m_DtmSelectedHISInDate = p_objSelectedSession.m_dtmHISInpatientDate;
                m_objBaseCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;
                m_objCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;
                //���ò��˵���סԺ�Ļ�����Ϣ
                m_mthOnlySetPatientInfo(m_objCurrentPatient);

                MDIParent.s_ObjCurrentPatient = m_objBaseCurrentPatient;
                //				this.trvTime.SelectedNode.Tag = m_objBaseCurrentPatient;

                m_mthClearRecordInfo(false);
                m_mthIsReadOnly();

                if (!m_blnCanShowRecordContent())
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                m_mthSetSelectedRecord();
                if (m_dtmOpenDate != DateTime.MinValue)
                {
                    m_mthEnableModify(false);
                    //��ǰ�����޸ļ�¼״̬
                    MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.Modify);
                }
                else
                {
                    m_mthSetDefaultValue(m_objCurrentPatient);
                    //��ǰ����������¼״̬
                    MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.AddNew);
                }
            }
            else
            {
                m_mthClearRecordInfo(true);
                m_dtmOpenDate = DateTime.MinValue;
                m_mthEnableModify(true);
                //				this.m_dtpCreateDate.Enabled =true;
                this.m_dtpCreateDate.Value = DateTime.Now;
                //��ǰ���ڽ�ֹ����״̬
                MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.None);
            }

            m_mthAddFormStatusForClosingSave();

            #region �����ר�Ʋ������в�����ϼ�������ϣ�������������ɫΪ��ɫ
            Control[] objControl;
            objControl = this.Controls.Find("m_AddtionDiagnose", true);
            if (objControl != null && objControl.Length == 1)
            {
                com.digitalwave.controls.ctlRichTextBox txtRTB = (com.digitalwave.controls.ctlRichTextBox)objControl[0];
                txtRTB.m_ClrOldPartInsertText = Color.Red;

            }
            Control[] objControl1;
            objControl1 = this.Controls.Find("m_ModifyDiagnose", true);
            if (objControl1 != null && objControl1.Length == 1)
            {
                com.digitalwave.controls.ctlRichTextBox txtRTB1 = (com.digitalwave.controls.ctlRichTextBox)objControl1[0];
                txtRTB1.m_ClrOldPartInsertText = Color.Red;

            }
            #endregion
        }

        #endregion

        #region ���漰����ص�ͼƬ����
        /// <summary>
        /// m_mthBindImageToPic
        /// </summary>
        /// <param name="p_objPics"></param>
        /// <param name="p_pic"></param>
        protected void m_mthBindImageToPic(clsPictureBoxValue[] p_objPics, PictureBox p_pic)
        {
            for (int i = 0; i < p_objPics.Length; i++)
            {
                if (p_objPics[i].m_StrPictureBoxName == p_pic.Name)
                {
                    if (p_objPics[i].m_imgBack == null)
                        p_objPics[i].m_imgBack = Convert2Bitmap(p_objPics[i].m_bytImage);
                    p_pic.Image = p_objPics[i].m_imgBack;
                    p_pic.Tag = p_objPics[i];
                    break;
                }
            }
        }
        /// <summary>
        /// m_imgBinaryToImage
        /// </summary>
        /// <param name="p_obj"></param>
        /// <returns></returns>
        private Image m_imgBinaryToImage(object p_obj)
        {
            System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])p_obj);

            Image img = new Bitmap(objStream);

            return img;
        }

        Bitmap Convert2Bitmap(object p_obj)
        {
            System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])p_obj);
            return new Bitmap(objStream);
        }
        /// <summary>
        /// ��ȡͼƬ
        /// </summary>
        /// <param name="p_imgPic"></param>
        /// <returns></returns>
        protected clsPictureBoxValue m_objGetPic(Image p_imgPic)
        {
            if (p_imgPic != null)
            {
                clsPictureBoxValue objPic = new clsPictureBoxValue();
                objPic.m_imgBack = p_imgPic as Bitmap;
                objPic.m_imgFront = null;
                objPic.m_bytImage = m_bytImageToBinary(p_imgPic);
                objPic.intHeight = p_imgPic.Height;
                objPic.intWidth = p_imgPic.Width;
                objPic.clrBack = Color.White;
                return objPic;
            }
            return null;
        }
        /// <summary>
        /// ͼƬת��Ϊ������
        /// </summary>
        /// <param name="p_img"></param>
        /// <returns></returns>
        private byte[] m_bytImageToBinary(Image p_img)
        {
            System.IO.MemoryStream objTempStream = new System.IO.MemoryStream();

            p_img.Save(objTempStream, System.Drawing.Imaging.ImageFormat.Bmp);

            return objTempStream.ToArray();
        }

        #endregion

        #region �����¼
        // �Ƿ�����Ӽ�¼��true������ӣ�false���޸ġ�
        protected override bool m_BlnIsAddNew
        {
            get
            {
                return m_dtmOpenDate == DateTime.MinValue;
            }
        }

        /// <summary>
        /// ��д���ౣ���¼
        /// </summary>
        /// <returns></returns>
        protected override long m_lngSubAddNew()
        {
            if (!m_blnCheckCreateDate())
            {
                return -1;
            }
            return m_lngAddNewRecord();
        }
        /// <summary>
        /// ����¼�¼�����ݿⱣ�档
        /// </summary>
        /// <returns></returns>
        private long m_lngAddNewRecord()
        {
            #region Check
            //��鵱ǰ���˱����Ƿ�Ϊnull
            if (m_objCurrentPatient == null)
                return (long)enmOperationResult.Parameter_Error;

            if (m_ObjCurrentEmrPatientSession == null)
            {
#if !Debug
                clsPublicFunction.ShowInformationMessageBox("��ѡ������Ժ���ڡ�");
#endif
                return -7;
            }
            #endregion

            #region
            Control[] objControl1;
            objControl1 = this.Controls.Find("m_AddtionDiagnose", true);
            if (objControl1 != null && objControl1.Length == 1)
            {
                com.digitalwave.controls.ctlRichTextBox txtRTB1 = (com.digitalwave.controls.ctlRichTextBox)objControl1[0];
                if (txtRTB1.Text.Trim().Length != 0)
                {
                    Control[] objControl10;
                    objControl10 = this.Controls.Find("m_txtAddtionSign", true);
                    if (objControl10 != null && objControl10.Length == 1)
                    {
                        System.Windows.Forms.TextBox txtRTB10 = (System.Windows.Forms.TextBox)objControl10[0];
                        if (txtRTB10.Text.Trim().Length == 0)
                        {
                            clsPublicFunction.ShowInformationMessageBox("����д������ϣ���ҽʦǩ��");
                            return -7;
                        }

                    }

                }

            }
            Control[] objControl2;
            objControl2 = this.Controls.Find("m_ModifyDiagnose", true);
            if (objControl2 != null && objControl2.Length == 1)
            {
                com.digitalwave.controls.ctlRichTextBox txtRTB2 = (com.digitalwave.controls.ctlRichTextBox)objControl2[0];
                if (txtRTB2.Text.Trim().Length != 0)
                {
                    Control[] objControl20;
                    objControl20 = this.Controls.Find("m_txtModifySign", true);
                    if (objControl20 != null && objControl20.Length == 1)
                    {
                        System.Windows.Forms.TextBox txtRTB20 = (System.Windows.Forms.TextBox)objControl20[0];
                        if (txtRTB20.Text.Trim().Length == 0)
                        {
                            clsPublicFunction.ShowInformationMessageBox("����д������ϣ���ҽʦǩ��");
                            return -7;
                        }

                    }

                }

            }
            #endregion

            #region ��ȡ��¼��Ϣ
            //��ȡ������ʱ��
            clsPublicDomain m_objPDomain = new clsPublicDomain();

            clsInpatMedRecContent objContent = new clsInpatMedRecContent();
            objContent.m_bytIfConfirm = 0;
            objContent.m_bytStatus = 0;
            objContent.m_dtmInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
            //����ۿƻ����¼Ҫ���¼���ڿ����޸ģ������recorddate����m_dtpCreateDate��ֵ����Ϊopendate��createdate����
            if (this.Name == "frmIMR_EyeTakecare")
            {
                objContent.m_dtmOpenDate = DateTime.Parse(m_dtpCreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                objContent.m_dtmOpenDate = m_objPDomain.m_dtmGetServerTime();
            }
            objContent.m_strCreateUserID = com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjCurrentEmployee.m_strEMPID_CHR;//lsvSign.Items.Count > 0 ? lsvSign.Items[0].Text : "";//((clsEmrEmployeeBase_VO)m_txtSign.Tag).m_strEMPNO_CHR;
            objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            objContent.m_dtmCreateDate = objContent.m_dtmOpenDate;
            objContent.m_dtmRecordDate = DateTime.Parse(m_dtpCreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            objContent.m_strCredibility = m_cboCredibility.Text.Trim();
            objContent.m_strRepresentor = m_cboRepresentor.Text.Trim();
            objContent.m_strTypeID = this.Name;
            //��ȡǩ��
            string strUserIDList = "";
            string strUserNameList = "";
            m_mthGetSignArr(m_objGetSignControlBySub(), ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);

            //����
            objContent.m_strDiseaseID = new clsTemplateDomain().m_strGetAssociateIDBySetID(m_strGetTemplateSetID(), (int)enmAssociate.Disease);
            objContent.m_strDeptID = clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID;

            //����Ŀ����
            objContent.m_objItemContents = m_objGetContentFromGUI(objContent);
            if (objContent.m_objItemContents == null)
                return (long)enmOperationResult.Parameter_Error;
            #endregion

            #region ��ǩ��ʱ��֤����ǩ���� ������
            string strRecordID = objContent.m_strInPatientID.Trim() + "-" + objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss");
            if (m_lngCheckSign(objContent, false, objContent.objSignerArr, strRecordID) == -1) return -1;
            #endregion

            //�����¼
            long lngRes = m_objDomain.m_lngAddNewRecord(objContent);

            #region �������
            //���ݽ������ͬ�Ĵ���
            switch ((enmOperationResult)lngRes)
            {
                case enmOperationResult.DB_Succeed:
                    if (com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_ObjCurrentEmployee.m_strTECHNICALRANK_CHR == "��ϰҽʦ")
                    {
                        clsEMR_CaseAuditVO objAuditVO = m_objGetAuditCase();
                        objAuditVO.m_dtmCREATEDATE = objContent.m_dtmOpenDate;
                        m_mthAddAuditCase(objAuditVO);
                    }

                    //m_objCurrentRecord = objContent;
                    m_strCreateUserID = objContent.m_strCreateUserID;
                    m_dtmCreatedDate = objContent.m_dtmOpenDate;
                    //if (trvTime.SelectedNode != null)
                    //    trvTime.SelectedNode.Tag = objContent.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss");
                    m_mthEnableModify(false);
                    break;
                case enmOperationResult.Record_Already_Exist:
                    m_mthShowRecordTimeDouble();
                    return lngRes;
            }
            //this.trvTime.ExpandAll();
            m_mthDoControlBySub(objContent.m_dtmOpenDate);
            return lngRes;
            #endregion
        }
        protected virtual void m_mthDoControlBySub(DateTime p_dtmOpenDate)
        {

        }
        protected virtual Control[] m_objGetSignControlBySub()
        {
            return new Control[] { lsvSign };
        }
        /// <summary>
        /// �ӽ����ȡ�������Ŀ���ݡ��������ֵ��������null��
        /// </summary>
        /// <returns></returns>
        private clsInpatMedRec_Item[] m_objGetContentFromGUI(clsInpatMedRecContent p_objContent)
        {
            if (m_objCurrentPatient == null)
                return null;

            m_arlItems.Clear();

            m_mthAddItemToArray(m_pnlContent, p_objContent);
            //��֤���������������� ���������ͱ���ǩ��
            m_mthGetPicContent(p_objContent);
            if (m_arlItems.Count > 0)
                return (clsInpatMedRec_Item[])m_arlItems.ToArray(typeof(clsInpatMedRec_Item));

            return null;
        }

        #region Item��ӵ�����
        /// <summary>
        /// Item��ӵ�����
        /// </summary>
        /// <param name="p_ctlParent"></param>
        /// <param name="p_objContent"></param>
        private void m_mthAddItemToArray(Control p_ctlParent, clsInpatMedRecContent p_objContent)
        {
            if (!p_ctlParent.HasChildren || p_ctlParent is ctlComboBox || p_ctlParent is ctlTimePicker || p_ctlParent is ctlPaintContainer || p_ctlParent is com.digitalwave.Controls.ICustomValueControl)
            {
                bool blnAdd = false;
                clsInpatMedRec_Item objItem = new clsInpatMedRec_Item();
                objItem.m_strItemID = p_ctlParent.Name;
                if (p_ctlParent is com.digitalwave.Controls.ICustomValueControl)
                {
                    if (p_ctlParent is com.digitalwave.Controls.ICustomValueControl<string>)
                    {
                        com.digitalwave.Controls.ICustomValueControl<string> ctlCustomValue = (com.digitalwave.Controls.ICustomValueControl<string>)p_ctlParent;
                        string[] strArr = ctlCustomValue.m_objGetValues();
                        objItem.m_strItemContent = strArr[0];
                        objItem.m_strItemContentXml = strArr[1];
                        blnAdd = true;
                        objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                    }
                    else if (p_ctlParent is com.digitalwave.Controls.ICustomValueControl<DateTime>)
                    {
                        com.digitalwave.Controls.ICustomValueControl<DateTime> ctlCustomValue = (com.digitalwave.Controls.ICustomValueControl<DateTime>)p_ctlParent;
                        objItem.m_strItemContent = ctlCustomValue.m_objGetValue().ToString("yyyy-MM-dd HH:mm:ss");
                        blnAdd = true;
                        objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                    }
                    else if (p_ctlParent is com.digitalwave.Controls.ICustomValueControl<bool>)
                    {
                        com.digitalwave.Controls.ICustomValueControl<bool> ctlCustomValue = (com.digitalwave.Controls.ICustomValueControl<bool>)p_ctlParent;
                        if (ctlCustomValue.m_objGetValue())
                        {
                            objItem.m_strItemContent = ctlCustomValue.m_objGetValue().ToString();
                            blnAdd = true;
                            objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                        }
                        else if (m_objCurrentContent.ContainsKey(p_ctlParent.Name))
                        {
                            objItem.m_enmTextStatus = EnmItemTextStatus.DELETE;
                            blnAdd = true;
                        }
                    }
                    else if (p_ctlParent is com.digitalwave.Controls.ICustomValueControl<decimal>)
                    {
                        com.digitalwave.Controls.ICustomValueControl<decimal> ctlCustomValue = (com.digitalwave.Controls.ICustomValueControl<decimal>)p_ctlParent;
                        objItem.m_strItemContent = (ctlCustomValue.m_BlnIsInvalidValue ? null : ctlCustomValue.m_objGetValue().ToString());
                        blnAdd = true;
                        objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                    }
                }
                else
                {
                    switch (p_ctlParent.GetType().FullName)
                    {
                        case "com.digitalwave.Utility.Controls.ctlRichTextBox":
                            ctlRichTextBox txtRTB = (ctlRichTextBox)p_ctlParent;
                            objItem.m_strItemContent = txtRTB.Text;
                            objItem.m_strItemContentXml = txtRTB.m_strGetXmlText();
                            objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                            blnAdd = true;
                            break;
                        case "com.digitalwave.controls.ctlRichTextBox":
                            com.digitalwave.controls.ctlRichTextBox txtRTB1 = (com.digitalwave.controls.ctlRichTextBox)p_ctlParent;
                            objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, txtRTB1.m_BlnIsTextChanged);//���жϱ����������2�丳ֵ֮ǰ
                            objItem.m_strItemContent = txtRTB1.Text;//�����ж�����ܸ�ֵ
                            objItem.m_strItemContentXml = txtRTB1.m_strGetXmlText();
                            blnAdd = true;
                            break;
                        case "com.digitalwave.Utility.Controls.ctlComboBox":
                            ctlComboBox cbo = (ctlComboBox)p_ctlParent;
                            objItem.m_strItemContent = cbo.Text;
                            objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                            blnAdd = true;
                            break;
                        case "System.Windows.Forms.CheckBox":
                            CheckBox cb = (CheckBox)p_ctlParent;
                            objItem.m_strItemContent = cb.Checked.ToString();
                            if (cb.Checked)
                            {
                                objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                                blnAdd = true;
                            }
                            else if (m_objCurrentContent.ContainsKey(p_ctlParent.Name))
                            {
                                objItem.m_enmTextStatus = EnmItemTextStatus.DELETE;
                                blnAdd = true;
                            }
                            break;
                        case "System.Windows.Forms.RadioButton":
                            RadioButton rb = (RadioButton)p_ctlParent;
                            objItem.m_strItemContent = rb.Checked.ToString();
                            if (rb.Checked)
                            {
                                objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                                blnAdd = true;
                            }
                            else if (m_objCurrentContent.ContainsKey(p_ctlParent.Name))
                            {
                                objItem.m_enmTextStatus = EnmItemTextStatus.DELETE;
                                blnAdd = true;
                            }
                            break;
                        case "com.digitalwave.Utility.Controls.ctlBorderTextBox":
                            ctlBorderTextBox txtBTB = (ctlBorderTextBox)p_ctlParent;
                            objItem.m_strItemContent = txtBTB.Text;
                            objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                            blnAdd = true;
                            break;
                        case "com.digitalwave.Utility.Controls.ctlTimePicker":
                            ctlTimePicker dtp = (ctlTimePicker)p_ctlParent;
                            objItem.m_strItemContent = dtp.Value.ToString("yyyy-MM-dd HH:mm:ss");
                            objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                            blnAdd = true;
                            break;
                        case "System.Windows.Forms.DateTimePicker":
                            DateTimePicker dtpSys = (DateTimePicker)p_ctlParent;
                            objItem.m_strItemContent = dtpSys.Value.ToString("yyyy-MM-dd HH:mm:ss");
                            objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                            blnAdd = true;
                            break;
                        case "com.digitalwave.Utility.Controls.ctlPaintContainer":
                            ctlPaintContainer ctlPic = (ctlPaintContainer)p_ctlParent;
                            p_objContent.m_objPics = ctlPic.m_objGetPicValue();
                            break;
                        case "System.Windows.Forms.TextBox":
                            TextBox txtB = (TextBox)p_ctlParent;
                            objItem.m_strItemContent = txtB.Text;
                            objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                            blnAdd = true;
                            break;
                        case "System.Windows.Forms.RichTextBox":
                            RichTextBox txtC = (RichTextBox)p_ctlParent;
                            objItem.m_strItemContent = txtC.Text;
                            objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                            blnAdd = true;
                            break;
                        case "System.Windows.Forms.MaskedTextBox":
                            MaskedTextBox txtD = (MaskedTextBox)p_ctlParent;
                            objItem.m_strItemContent = txtD.Text;
                            objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                            blnAdd = true;
                            break;
                        case "System.Windows.Forms.ComboBox":
                            ComboBox cboB = (ComboBox)p_ctlParent;
                            objItem.m_strItemContent = cboB.Text;
                            objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                            blnAdd = true;
                            break;
                        //case "SourceLibrary.Windows.Forms.TextBoxTypedNumeric":
                        //    SourceLibrary.Windows.Forms.TextBoxTypedNumeric txtN = (SourceLibrary.Windows.Forms.TextBoxTypedNumeric)p_ctlParent;
                        //    objItem.m_strItemContent = txtN.Text;
                        //    objItem.m_enmTextStatus = m_enmGetTextStatus(p_ctlParent.Name, objItem.m_strItemContent);
                        //    blnAdd = true;
                        //    break;
                        default:
                            break;
                    }
                }
                if (blnAdd && !(string.IsNullOrEmpty(objItem.m_strItemContent) && (objItem.m_enmTextStatus == EnmItemTextStatus.NONE || objItem.m_enmTextStatus == EnmItemTextStatus.NEW)))
                {
                    m_arlItems.Add(objItem);
                }
            }
            else
            {
                for (int i = 0; i < p_ctlParent.Controls.Count; i++)
                    m_mthAddItemToArray(p_ctlParent.Controls[i], p_objContent);
            }

        }

        #endregion

        /// <summary>
        /// m_strGetTemplateSetID
        /// </summary>
        /// <returns></returns>
        private string m_strGetTemplateSetID()
        {
            foreach (Control ctlSub in this.Controls)
            {
                if (ctlSub.Name == "m_lstTemplate" && ctlSub.Tag != null)
                    return ctlSub.Tag.ToString();
            }
            return "";
        }

        #endregion

        #region �޸ļ�¼
        /// <summary>
        /// �޸ļ�¼
        /// </summary>
        /// <returns></returns>
        protected override long m_lngSubModify()
        {
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                return (long)enmOperationResult.Parameter_Error;

            if (!m_blnCheckCreateDate())
            {
                return -1;
            }
            #region
            Control[] objControl1;
            objControl1 = this.Controls.Find("m_AddtionDiagnose", true);
            if (objControl1 != null && objControl1.Length == 1)
            {
                com.digitalwave.controls.ctlRichTextBox txtRTB1 = (com.digitalwave.controls.ctlRichTextBox)objControl1[0];
                if (txtRTB1.Text.Trim().Length != 0)
                {
                    Control[] objControl10;
                    objControl10 = this.Controls.Find("m_txtAddtionSign", true);
                    if (objControl10 != null && objControl10.Length == 1)
                    {
                        System.Windows.Forms.TextBox txtRTB10 = (System.Windows.Forms.TextBox)objControl10[0];
                        if (txtRTB10.Text.Trim().Length == 0)
                        {
                            clsPublicFunction.ShowInformationMessageBox("����д������ϣ���ҽʦǩ��");
                            return -7;
                        }

                    }

                }

            }
            Control[] objControl2;
            objControl2 = this.Controls.Find("m_ModifyDiagnose", true);
            if (objControl2 != null && objControl2.Length == 1)
            {
                com.digitalwave.controls.ctlRichTextBox txtRTB2 = (com.digitalwave.controls.ctlRichTextBox)objControl2[0];
                if (txtRTB2.Text.Trim().Length != 0)
                {
                    Control[] objControl20;
                    objControl20 = this.Controls.Find("m_txtModifySign", true);
                    if (objControl20 != null && objControl20.Length == 1)
                    {
                        System.Windows.Forms.TextBox txtRTB20 = (System.Windows.Forms.TextBox)objControl20[0];
                        if (txtRTB20.Text.Trim().Length == 0)
                        {
                            clsPublicFunction.ShowInformationMessageBox("����д������ϣ���ҽʦǩ��");
                            return -7;
                        }

                    }

                }

            }
            #endregion

            #region ��ȡ��¼��Ϣ
            clsInpatMedRecContent objContent = new clsInpatMedRecContent();
            objContent.m_bytIfConfirm = 0;
            objContent.m_bytStatus = 0;
            objContent.m_dtmInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
            objContent.m_dtmOpenDate = m_dtmOpenDate;
            objContent.m_strCreateUserID = m_strCreateUserID;//lsvSign.Items.Count > 0 ? lsvSign.Items[0].Text : "";//((clsEmrEmployeeBase_VO)m_txtSign.Tag).m_strEMPNO_CHR;
            objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            objContent.m_dtmCreateDate = m_dtmCreatedDate;
            objContent.m_dtmRecordDate = DateTime.Parse(this.m_dtpCreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            objContent.m_strCredibility = m_cboCredibility.Text.Trim();
            objContent.m_strRepresentor = m_cboRepresentor.Text.Trim();
            objContent.m_strTypeID = this.Name;
            //����
            objContent.m_strDiseaseID = new clsTemplateDomain().m_strGetAssociateIDBySetID(m_strGetTemplateSetID(), (int)enmAssociate.Disease);
            objContent.m_strDeptID = clsSystemContext.s_ObjCurrentContext.m_ObjDepartment.m_StrDeptID;
            //����Ŀ����
            objContent.m_objItemContents = m_objGetContentFromGUI(objContent);
            if (objContent.m_objItemContents == null)
                return (long)enmOperationResult.Parameter_Error;

            #region �Ƿ�����޺ۼ��޸�
            if (chkModifyWithoutMatk.Checked)
                objContent.m_intMarkStatus = 0;
            else
                objContent.m_intMarkStatus = 1;
            #endregion

            #endregion
            string strUserIDList = "";
            string strUserNameList = "";
            m_mthGetSignArr(m_objGetSignControlBySub(), ref objContent.objSignerArr, ref strUserIDList, ref strUserNameList);
            //����ǩ�� 
            //��¼IDͨ��Ϊ סԺ�ţ�סԺʱ�� || סԺ�ţ���¼ʱ�� ��ʶ��Ψһ ��ʽ 00000056-2005-10-10 10:20:20
            clsEmrDigitalSign_VO objSign_VO = new clsEmrDigitalSign_VO();
            objSign_VO.m_strFORMID_VCHR = this.Name;
            objSign_VO.m_strFORMRECORDID_VCHR = objContent.m_strInPatientID.Trim() + "-" + objContent.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss");
            objSign_VO.m_strSIGNIDID_VCHR = clsEMRLogin.LoginInfo.m_strEmpID;
            objSign_VO.m_strRegisterId = m_objBaseCurrentPatient.m_StrRegisterId;

            ArrayList objSignerArr = new ArrayList();
            if (objContent.objSignerArr != null)
            {
                for (int i = 0; i < objContent.objSignerArr.Length; i++)
                {
                    if (objContent.objSignerArr[i].controlName == "lsvSign")
                        objSignerArr.Add(objContent.objSignerArr[i].objEmployee);
                }
            }
            clsCheckSignersController objCheck = new clsCheckSignersController(objSignerArr, false);
            if (objCheck.CheckSigner(objContent, objSign_VO) == -1)
                return -1;


            long lngRes = m_objDomain.m_lngModifyRecord(objContent);

            //���ݽ������ͬ�Ĵ���
            switch ((enmOperationResult)lngRes)
            {
                case enmOperationResult.DB_Succeed:
                    /*���ı���������ݸ���ԭ���ļ�¼����m_objCurrentRecord��
                     * ���ڸı�����������û��FirstPrintDate�ģ����Ե������ڵļ�¼�д�ӡʱ��Ϊnull,
                     * ����������Ժʱ�䣬�ٵ��ĳ����¼ʱ��ʱ����ֱ�ӽ����ݿ��еļ�¼ȡ��������Ϊ��ǰ��¼�ģ�
                     * ���Ծ��д�ӡʱ�䡣
                     */
                    //m_objCurrentRecord = objContent;
                    m_dtmCreatedDate = objContent.m_dtmOpenDate;
                    m_strCreateUserID = objContent.m_strCreateUserID;
                    m_dtmOpenDate = objContent.m_dtmOpenDate;
                    break;
            }
            return lngRes;
        }
        #endregion

        #region ɾ����¼
        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <returns></returns>
        protected override long m_lngSubDelete()
        {
            //��鵱ǰ���˱����Ƿ�Ϊnull  
            if (m_objCurrentPatient == null)
            {
                clsPublicFunction.ShowInformationMessageBox("δѡ������,�޷�ɾ��!");//�޺�褣�2003-5-27
                return (long)enmOperationResult.Parameter_Error;
            }
            //��鵱ǰ��¼�Ƿ�Ϊnull
            //			if(m_objCurrentRecordContent==null)
            if (m_dtmOpenDate == DateTime.MinValue)
            {
                clsPublicFunction.ShowInformationMessageBox("��ǰ��¼����Ϊ��,�޷�ɾ��!");//�޺�褣�2003-5-27
                return (long)enmOperationResult.Parameter_Error;
            }
            //��ȡ������ʱ��      
            clsPublicDomain m_objPDomain = new clsPublicDomain();

            //ɾ����¼
            //			clsInPatientCaseHistoryContent objContent=m_objGetContentFromGUI();
            clsInpatMedRecContent objContent = new clsInpatMedRecContent();

            objContent.m_bytStatus = 0;
            objContent.m_strTypeID = this.Name;
            objContent.m_dtmCreateDate = DateTime.Parse(this.m_dtpCreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            objContent.m_dtmInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate;
            objContent.m_strInPatientID = m_objCurrentPatient.m_StrInPatientID;
            objContent.m_strDeActivedOperatorID = MDIParent.OperatorID;
            objContent.m_dtmDeActivedDate = DateTime.Now;
            objContent.m_dtmOpenDate = m_dtmOpenDate;

            //Ȩ���ж�
            string strDeptIDTemp = m_ObjCurrentEmrPatientSession.m_strDeptId;// ((clsDepartment)m_cboDept.SelectedItem).m_strDeptNewID;
            bool blnIsAllow = clsPublicFunction.IsAllowDelete(strDeptIDTemp, m_StrRecorder_ID, clsEMRLogin.LoginEmployee, 1);
            if (!blnIsAllow)
                return -1;

            //���� m_objCurrentRecordContent ����Ϣ��ʹ�÷�����ʱ������m_dtmDeActivedDate��
            objContent.m_dtmDeActivedDate = DateTime.Parse(m_objPDomain.m_strGetServerTime());

            long lngRes = m_objDomain.m_lngDeleteRecord(objContent);

            //���ݽ������ͬ�Ĵ���
            switch ((enmOperationResult)lngRes)
            {
                case enmOperationResult.DB_Succeed:
                    clsEMR_CaseAuditVO objAuditVO = m_objGetAuditCase();
                    objAuditVO.m_dtmCREATEDATE = objContent.m_dtmOpenDate;
                    m_mthDelAuditCase(objAuditVO);
                    //��ռ�¼��Ϣ   
                    m_dtmCreatedDate = DateTime.Now;
                    m_strCreateUserID = string.Empty;
                    m_dtmOpenDate = DateTime.MinValue;
                    m_mthClearPatientRecordInfo();
                    //ѡ�и��ڵ�
                    m_blnCanTreeAfterSelect = false;
                    m_mthRecursiveSetControlReadOnly(m_pnlContent, true);
                    m_blnCanTreeAfterSelect = true;
                    break;
            }

            //���ؽ��
            return lngRes;
        }
        #endregion

        #region ctlRichTextBox��˫���ߡ�������������
        /// <summary>
        /// ����˫����
        /// </summary>
        protected void m_mthSetRichTextBoxDoubleStrike()
        {
            //��ȡRichTextBox        
            //ctlRichTextBox objRichTextBox = (ctlRichTextBox)m_ctmRichTextBoxMenu.SourceControl;

            //objRichTextBox.m_mthSelectionDoubleStrikeThough(true);
            if (m_txtFocusedRichTextBox != null)
            {
                if (m_txtFocusedRichTextBox is com.digitalwave.Utility.Controls.ctlRichTextBox)
                    ((com.digitalwave.Utility.Controls.ctlRichTextBox)m_txtFocusedRichTextBox).m_mthSelectionDoubleStrikeThough(true);
                else if (m_txtFocusedRichTextBox is com.digitalwave.controls.ctlRichTextBox)
                    ((com.digitalwave.controls.ctlRichTextBox)m_txtFocusedRichTextBox).m_mthSelectionDoubleStrikeThough(true);
            }
        }

        /// <summary>
        /// ����RichTextBox���ԡ����Ҽ��˵����û��������û�ID����ɫ�ȣ���
        /// </summary>
        /// <param name="p_objRichTextBox"></param>
        protected void m_mthSetRichTextBoxAttrib(Control p_objRichTextBox)
        {
            if (p_objRichTextBox.GetType().FullName == "com.digitalwave.Utility.Controls.ctlRichTextBox")
            {
                //m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{	(ctlRichTextBox)p_objRichTextBox });
                //�����Ҽ��˵�			
                //			p_objRichTextBox.ContextMenu=m_cmuRichTextBoxMenu;
                ((ctlRichTextBox)p_objRichTextBox).GotFocus += new EventHandler(m_txtRichTextBox_GotFocus);

                //������������			
                ((ctlRichTextBox)p_objRichTextBox).m_StrUserID = MDIParent.strOperatorID.Trim();
                ((ctlRichTextBox)p_objRichTextBox).m_StrUserName = MDIParent.strOperatorName.Trim();

                ((ctlRichTextBox)p_objRichTextBox).m_ClrOldPartInsertText = Color.Black;
                ((ctlRichTextBox)p_objRichTextBox).m_ClrDST = Color.Red;
            }
            if (p_objRichTextBox.GetType().FullName == "com.digitalwave.controls.ctlRichTextBox")
            {
                //m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{	(com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox });
                //�����Ҽ��˵�			
                //			p_objRichTextBox.ContextMenu=m_cmuRichTextBoxMenu;
                ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).GotFocus += new EventHandler(m_txtRichTextBox_GotFocus);

                //������������			
                ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_StrUserID = MDIParent.strOperatorID.Trim();
                ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_StrUserName = MDIParent.strOperatorName.Trim();

                ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_ClrOldPartInsertText = Color.Black;
                ((com.digitalwave.controls.ctlRichTextBox)p_objRichTextBox).m_ClrDST = Color.Red;
            }
        }

        protected void m_mthSetRichTextBoxAttribInControl(Control p_ctlControl)
        {
            if (p_ctlControl.GetType().FullName == "com.digitalwave.Utility.Controls.ctlRichTextBox")
            {
                m_mthSetRichTextBoxAttrib((ctlRichTextBox)p_ctlControl);
            }
            if (p_ctlControl.GetType().FullName == "com.digitalwave.controls.ctlRichTextBox")
            {
                m_mthSetRichTextBoxAttrib((com.digitalwave.controls.ctlRichTextBox)p_ctlControl);
            }

            if (p_ctlControl.HasChildren && p_ctlControl.GetType().Name != "DataGrid")
            {
                foreach (Control subcontrol in p_ctlControl.Controls)
                {
                    m_mthSetRichTextBoxAttribInControl(subcontrol);
                }
            }
        }

        private void mniDoubleStrikeOutDelete_Click(object sender, System.EventArgs e)
        {
            m_mthSetRichTextBoxDoubleStrike();
        }
        private Control m_txtFocusedRichTextBox = null;//��ŵ�ǰ��ý����RichTextBox
        private void m_txtRichTextBox_GotFocus(object sender, System.EventArgs e)
        {
            m_txtFocusedRichTextBox = ((Control)(sender));
        }
        #endregion ctlRichTextBox��˫���ߡ�������������

        #region ��ӡ���
        #region �ⲿ��ӡ
        protected virtual void m_pdcPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            objPrintTool.m_mthPrintPage(e);

            if (ppdPrintPreview != null)
                while (!ppdPrintPreview.m_blnHandlePrint(e))
                    objPrintTool.m_mthPrintPage(e);
        }

        protected virtual void m_pdcPrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            objPrintTool.m_mthBeginPrint(e);
        }

        protected virtual void m_pdcPrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            objPrintTool.m_mthEndPrint(e);
        }

        clsInpatMedRecPrintBase objPrintTool;
        private void m_mthPrint_FromDataSource()
        {
            objPrintTool = clsInpatMedRecPrintToolFactory.s_objGeneratePrintTool(this.Name);
            if (objPrintTool == null)
                return;
            objPrintTool.m_mthInitPrintTool(null);
            if (m_objBaseCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, DateTime.MinValue, DateTime.MinValue);
            else if (m_dtmOpenDate == DateTime.MinValue)
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate, DateTime.MinValue);
            else //HB 2005-12-22�޸�InPatient����Ϊѡ�����ʱ�䣬���������סԺ���ڣ�����ѡ��ǰ��סԺ��¼ʱ���ܴ�ӡ
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, m_ObjCurrentEmrPatientSession.m_dtmEMRInpatientDate, m_dtmOpenDate);

            objPrintTool.m_mthInitPrintContent();

            m_mthStartPrint();
        }

        private void m_mthStartPrint()
        {
            if (m_blnDirectPrint)
            {
                m_pdcPrintDocument.Print();
            }
            else
            {
                ppdPrintPreview.Document = m_pdcPrintDocument;
                ppdPrintPreview.ShowDialog();
            }
        }
        #endregion �ⲿ��ӡ

        protected override long m_lngSubPrint()
        {
            m_mthPrint_FromDataSource();
            return 1;
        }

        // ��ʼ��ӡ��
        private PrintTool.frmPrintPreviewDialog ppdPrintPreview = new PrintTool.frmPrintPreviewDialog();

        #endregion

        #region �ӿں���
        public void Copy()
        {
            m_lngCopy();
        }
        public void Verify()
        {
            //��鵱ǰ���˱����Ƿ�Ϊnull  
            if (m_objCurrentPatient == null)
            {
                clsPublicFunction.ShowInformationMessageBox("δѡ������,�޷���֤!");
            }
            try
            {
                string strInPatientID = m_objCurrentPatient.m_StrInPatientID;
                string strInPatientDate = m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss");
                string strRecordID = strInPatientID.Trim() + "-" + strInPatientDate;
                long lngRes = m_lngSignVerify(this.Name.Trim(), strRecordID);
            }
            catch (Exception exp)
            {
                MessageBox.Show("ǩ����֤�����쳣��" + exp.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void Cut()
        {
            m_lngCut();
        }

        public void Delete()
        {
            //ָ��������Ϊҽ������վ
            intFormType = 1;
            long m_lngRe = m_lngDelete();
            if (m_lngRe > 0)
            {
                m_blnNeedCheckArchive = false;
                m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
                m_blnNeedCheckArchive = true;
            }

        }

        public void Display()
        {

        }

        public void Display(string cardno, string sendcheckdate)
        {

        }

        public void Paste()
        {
            m_lngPaste();
        }

        public void Print()
        {
            long lngRes = m_lngPrint();
            //if (lngRes == 1)
            //    m_mthSetSelectedRecord();//Ԥ����ˢ�½��棬����Ķ��������ݺ󲻱���ֱ��Ԥ��֮���ٱ�����������

        }

        public void Redo()
        {

        }

        public void Save()
        {
            long m_lngRe = m_lngSave();
            if (m_lngRe > 0)
            {
                //if (this.trvTime.SelectedNode != null)
                //{
                //    m_blnNeedCheckArchive = false;
                //    this.trvTime_AfterSelect(this.trvTime, new TreeViewEventArgs(this.trvTime.SelectedNode));
                //    m_blnNeedCheckArchive = true;
                //}
                m_blnNeedCheckArchive = false;
                m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
                m_blnNeedCheckArchive = true;
                clsPublicFunction.ShowInformationMessageBox("����ɹ���");
            }
            else
                clsPublicFunction.ShowInformationMessageBox("����ʧ�ܣ�");
        }
        public void Undo()
        {

        }
        #endregion

        #region �¼�
        /// <summary>
        /// Treeview�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void trvTime_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (!m_blnCanTreeAfterSelect)
                return;

            m_mthRecordChangedToSave();

            if (this.trvTime.SelectedNode.Parent != null)
            {
                string m_strInPatientID = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_StrEMRInPatientID;
                DateTime dtmInPatientDate = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_DtmEMRInDate;

                //���ò��˵���סԺ�Ļ�����Ϣ
                m_mthOnlySetPatientInfo(m_objCurrentPatient);
                m_objCurrentPatient.m_ObjPeopleInfo = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_ObjPeopleInfo;

                txtInPatientID.Text = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(trvTime.Nodes[0].Nodes.Count - trvTime.SelectedNode.Index - 1).m_StrHISInPatientID;

                m_objBaseCurrentPatient.m_DtmSelectedInDate = DateTime.Parse(dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"));
                m_objCurrentPatient.m_DtmSelectedInDate = m_objBaseCurrentPatient.m_DtmSelectedInDate;
                m_objCurrentPatient.m_StrHISInPatientID = txtInPatientID.Text;
                m_objCurrentPatient.m_DtmSelectedHISInDate = Convert.ToDateTime(trvTime.SelectedNode.Text);

                if (string.IsNullOrEmpty(m_objBaseCurrentPatient.m_StrRegisterId))
                {
                    string strRegisterID = string.Empty;
                    long lngRes = new clsPublicDomain().m_lngGetRegisterID(m_objCurrentPatient.m_StrPatientID, Convert.ToDateTime(trvTime.SelectedNode.Text).ToString("yyyy-MM-dd HH:mm:ss"), out strRegisterID);
                    m_objBaseCurrentPatient.m_StrRegisterId = strRegisterID;
                    m_objCurrentPatient.m_StrRegisterId = strRegisterID;
                }

                MDIParent.s_ObjCurrentPatient = m_objBaseCurrentPatient;
                //				this.trvTime.SelectedNode.Tag = m_objBaseCurrentPatient;

                m_mthClearRecordInfo(false);
                m_mthIsReadOnly();

                if (!m_blnCanShowRecordContent())
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                m_mthSetSelectedRecord();
                if (m_dtmOpenDate != DateTime.MinValue)
                {
                    m_mthEnableModify(false);
                    //��ǰ�����޸ļ�¼״̬
                    MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.Modify);
                }
                else
                {
                    m_mthSetDefaultValue(m_objCurrentPatient);
                    //��ǰ����������¼״̬
                    MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.AddNew);
                }
            }
            else
            {
                m_mthClearRecordInfo(true);
                m_dtmOpenDate = DateTime.MinValue;
                m_mthEnableModify(true);
                //				this.m_dtpCreateDate.Enabled =true;
                this.m_dtpCreateDate.Value = DateTime.Now;
                //��ǰ���ڽ�ֹ����״̬
                MDIParent.m_mthChangeFormText(this, MDIParent.enmFormEditStatus.None);
            }

            m_mthAddFormStatusForClosingSave();

            #region �����ר�Ʋ������в�����ϼ�������ϣ�������������ɫΪ��ɫ
            Control[] objControl;
            objControl = this.Controls.Find("m_AddtionDiagnose", true);
            if (objControl != null && objControl.Length == 1)
            {
                com.digitalwave.controls.ctlRichTextBox txtRTB = (com.digitalwave.controls.ctlRichTextBox)objControl[0];
                txtRTB.m_ClrOldPartInsertText = Color.Red;

            }
            Control[] objControl1;
            objControl1 = this.Controls.Find("m_ModifyDiagnose", true);
            if (objControl1 != null && objControl1.Length == 1)
            {
                com.digitalwave.controls.ctlRichTextBox txtRTB1 = (com.digitalwave.controls.ctlRichTextBox)objControl1[0];
                txtRTB1.m_ClrOldPartInsertText = Color.Red;

            }
            #endregion
        }



        /// <summary>
        /// ����load�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmInpatMedRecBase_Load(object sender, System.EventArgs e)
        {
            //�Ӵ��幹�캯����ʼ���ؼ�֮����ã�������ѣ�ֱ�����Ӵ��幹�캯��֮�����
            //			m_mthSetRichTextBoxAttribInControl(this);
            if (m_BlnNeedContextMenu)
                m_mthAddRichTemplateInContainer(this);
        }
        #endregion

        #region ���ø������͵�Ĭ��ֵ
        /// <summary>
        /// ���ø������͵�Ĭ��ֵ
        /// </summary>
        /// <param name="p_objPatient"></param>
        private void m_mthSetDefaultValue(clsPatient p_objPatient)
        {
            //Ĭ��ǩ��
            MDIParent.m_mthSetDefaulEmployee(lsvSign);

            new clsDefaultValueTool(this, p_objPatient).m_mthSetDefaultValue();
        }

        protected override enmFormState m_EnmCurrentFormState
        {
            get
            {
                return enmFormState.NowUser;
            }
        }
        #endregion

        #region ��ȡѡ���Ѿ�ɾ����¼�Ĵ������ �麯���̳���ʵ��
        /// <summary>
        /// ��ȡѡ���Ѿ�ɾ����¼�Ĵ������
        /// </summary>
        public virtual void m_strReloadFormTitle()
        {
            //���Ӵ�������ʵ��
        }
        #endregion

        #region ����
        /// <summary>
        /// ��ǰ��¼��
        /// </summary>
        public override string m_StrRecordID
        {
            get
            {
                if (m_dtmOpenDate == DateTime.MinValue)
                    return "";
                else
                {
                    return m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        protected override enmApproveType m_EnmAppType
        {
            get { return enmApproveType.CaseHistory; }
        }

        /// <summary>
        /// ��ʷ��¼�� 
        /// </summary>
        protected override string m_StrRecorder_ID
        {
            get
            {
                //if(m_txtSign.Tag != null)
                //    return ((clsEmrEmployeeBase_VO)m_txtSign.Tag).m_strEMPNO_CHR;
                if (lsvSign.Items.Count > 0)
                {
                    if (lsvSign.Items[0].SubItems.Count > 0)
                        return lsvSign.Items[0].SubItems[1].Text;
                }
                return "";
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        protected override string m_StrCurrentOpenDate
        {
            get
            {
                //m_objCurrentRecord.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss") ;
                if (m_dtmOpenDate == DateTime.MinValue)
                {
                    clsPublicFunction.ShowInformationMessageBox("����ѡ���¼");
                    return "";
                }
                return m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss");

            }
        }
        #endregion ����

        #region ��ȡ���˳�Ժʱ��
        /// <summary>
        /// ��ȡ���˳�Ժʱ�䣬��ʱ���ڸ��������ѯ
        /// </summary>
        /// <returns></returns>
        protected virtual long m_lngGetSetlectedOutDate(out DateTime p_dtmOutHospitalDate)
        {
            p_dtmOutHospitalDate = new DateTime(1900, 1, 1);
            string strRegisterID = "";
            long lngRes = 0;

            //clsPatientManagerService objServ =
            //    (clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsPatientManagerService));

            if (m_ObjCurrentEmrPatientSession != null)
            {
                lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetOutHospitalDate(m_ObjCurrentEmrPatientSession.m_strRegisterId, out p_dtmOutHospitalDate);
            }

            //objServ.Dispose();
            return lngRes;
        }

        #endregion

        #region ��ȡ�����ļ�¼��ӵ��ֵ�
        private Dictionary<string, clsInpatMedRec_Item> m_objGetCurrentRecord(clsInpatMedRec_Item[] p_objItemArr)
        {
            if (p_objItemArr == null || p_objItemArr.Length == 0) return null;
            Dictionary<string, clsInpatMedRec_Item> objContent = new Dictionary<string, clsInpatMedRec_Item>(p_objItemArr.Length);
            clsInpatMedRec_Item objItem = null;
            for (int i = 0; i < p_objItemArr.Length; i++)
            {
                objItem = p_objItemArr[i];
                objContent.Add(objItem.m_strItemID, objItem);
            }
            return objContent;
        }
        private EnmItemTextStatus m_enmGetTextStatus(string p_strCtlName, string p_strValue)
        {
            if (m_objCurrentContent.ContainsKey(p_strCtlName))
            {
                if (m_objCurrentContent[p_strCtlName].m_strItemContent == p_strValue)
                    return EnmItemTextStatus.NONE;
                else if (string.IsNullOrEmpty(p_strValue))
                    return EnmItemTextStatus.DELETE;
                else
                    return EnmItemTextStatus.MODIFY;
            }
            else
                return EnmItemTextStatus.NEW;
        }
        private EnmItemTextStatus m_enmGetTextStatus(string p_strCtlName, bool p_blnIsChanged)
        {
            if (m_objCurrentContent.ContainsKey(p_strCtlName))
            {
                if (p_blnIsChanged)
                {
                    return EnmItemTextStatus.MODIFY;
                }
                else
                    return EnmItemTextStatus.NONE;
            }
            else
                return EnmItemTextStatus.NEW;
        }
        #endregion ��ȡ�����ļ�¼��ӵ��ֵ�

        #region ��ȡ��ǰ���˵���������
        /// <summary>
        /// ��ȡ��ǰ���˵���������
        /// </summary>
        /// <param name="p_dtmRecordDate">��¼����</param>
        /// <param name="p_intFormID">����ID</param>
        protected override void m_mthGetDeactiveContent(DateTime p_dtmRecordDate, int p_intFormID)
        {
            if (m_objBaseCurrentPatient == null || m_objBaseCurrentPatient.m_StrInPatientID == null || m_objBaseCurrentPatient.m_DtmSelectedInDate == DateTime.MinValue)
                return;
            clsInpatMedRecContent p_objContent = new clsInpatMedRecContent();
            long lngRes = m_objDomain.m_lngGetDeactiveRecInfo(this.Name, m_objCurrentPatient.m_StrInPatientID, p_dtmRecordDate.ToString("yyyy-MM-dd HH:mm:ss"), out p_objContent);

            if (lngRes <= 0 || p_objContent == null)
            {
                switch (lngRes)
                {
                    case (long)(enmOperationResult.Not_permission):
                        m_mthShowNotPermitted(); break;
                    case (long)(enmOperationResult.DB_Fail):
                        m_mthShowDBError(); break;
                }
                return;
            }
            m_mthClearPatientRecordInfo();
            //if (this.trvTime.SelectedNode != null)
            //{
            //    this.trvTime_AfterSelect(this.trvTime, new TreeViewEventArgs(this.trvTime.SelectedNode));
            //}
            m_mthPerformSessionChanged(m_ObjCurrentEmrPatientSession, 0);
            m_mthSetGUIFromContent(p_objContent);
            this.m_txtSign.Enabled = true;
        }
        #endregion

        #region ��������
        protected override bool m_blnSubReuse(clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            bool blnIsOK = false;
            if (p_objSelectedValue != null)
            {
                clsInpatMedRecContent p_objContent = new clsInpatMedRecContent();
                long lngRes = m_objDomain.m_lngGetDeactiveRecInfo(this.Name, p_objSelectedValue.m_StrInpatientId, p_objSelectedValue.m_DtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out p_objContent);

                if (lngRes <= 0 || p_objContent == null)
                {
                    switch (lngRes)
                    {
                        case (long)(enmOperationResult.Not_permission):
                            m_mthShowNotPermitted(); break;
                        case (long)(enmOperationResult.DB_Fail):
                            m_mthShowDBError(); break;
                    }
                    return blnIsOK;
                }
                m_mthSetGUIFromContent(p_objContent);
                this.m_txtSign.Enabled = true;
                blnIsOK = true;
            }
            return blnIsOK;
        }

        protected override void m_mthSubPreviewInactiveRecord(IWin32Window p_infOwner, clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            if (p_objSelectedValue == null || m_objBaseCurrentPatient == null) return;

            objPrintTool = clsInpatMedRecPrintToolFactory.s_objGeneratePrintTool(this.Name);
            if (objPrintTool == null)
                return;
            objPrintTool.m_mthInitPrintTool(null);
            objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, p_objSelectedValue.m_DtmInpatientDate, p_objSelectedValue.m_DtmOpenDate);
            clsPrintInfo_InpatMedRec objPrintInfo = new clsPrintInfo_InpatMedRec();
            objPrintInfo.m_strInPatientID = p_objSelectedValue.m_StrInpatientId;
            objPrintInfo.m_strPatientName = m_objBaseCurrentPatient.m_ObjPeopleInfo.m_StrFirstName;
            objPrintInfo.m_strSex = m_objBaseCurrentPatient.m_ObjPeopleInfo.m_StrSex;
            objPrintInfo.m_strAge = m_objBaseCurrentPatient.m_ObjPeopleInfo.m_StrAge;
            objPrintInfo.m_strBedName = m_objBaseCurrentPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName;
            objPrintInfo.m_strDeptName = m_objBaseCurrentPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_objSelectedValue.m_DtmInpatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName;
            objPrintInfo.m_strAreaName = m_objBaseCurrentPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_objSelectedValue.m_DtmInpatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName;
            objPrintInfo.m_dtmInPatientDate = p_objSelectedValue.m_DtmInpatientDate;
            objPrintInfo.m_dtmOpenDate = p_objSelectedValue.m_DtmOpenDate;

            objPrintInfo.m_strHomeplace = m_objBaseCurrentPatient.m_ObjPeopleInfo.m_StrBirthPlace;//������
            objPrintInfo.m_strNativePlace = m_objBaseCurrentPatient.m_ObjPeopleInfo.m_StrNativePlace;//����
            objPrintInfo.m_strOccupation = m_objBaseCurrentPatient.m_ObjPeopleInfo.m_StrOccupation;//ְҵ
            objPrintInfo.m_strMarried = m_objBaseCurrentPatient.m_ObjPeopleInfo.m_StrMarried;//���
            objPrintInfo.m_strLinkManFirstName = m_objBaseCurrentPatient.m_ObjPeopleInfo.m_StrLinkManFirstName;//��ϵ��
            objPrintInfo.m_strNationality = m_objBaseCurrentPatient.m_ObjPeopleInfo.m_StrNation;//����
            objPrintInfo.m_strHomePhone = m_objBaseCurrentPatient.m_ObjPeopleInfo.m_StrLinkManPhone;//�绰
            objPrintInfo.m_StrHomePC = m_objBaseCurrentPatient.m_ObjPeopleInfo.m_StrHomePC;//Postcode
            objPrintInfo.m_strHomeAddress = m_objBaseCurrentPatient.m_ObjPeopleInfo.m_StrHomeAddress;//��ַ
            objPrintInfo.m_strOfficeName = m_objBaseCurrentPatient.m_ObjPeopleInfo.m_StrOffice_name;

            objPrintInfo.m_dtmHISInDate = m_objBaseCurrentPatient.m_DtmSelectedHISInDate;
            objPrintInfo.m_strHISInPatientID = m_objBaseCurrentPatient.m_StrHISInPatientID;

            clsInpatMedRecContent objContent = new clsInpatMedRecContent();
            long lngRes = m_objDomain.m_lngGetDeactiveRecInfo(this.Name, p_objSelectedValue.m_StrInpatientId, p_objSelectedValue.m_DtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);
            objPrintInfo.m_objContent = objContent;
            objPrintInfo.m_blnIsFirstPrint = false;

            objPrintTool.m_mthSetPrintContent(objPrintInfo);

            ppdPrintPreview.Document = m_pdcPrintDocument;
            ppdPrintPreview.m_mthCoverPrinter();
            ppdPrintPreview.ShowDialog(p_infOwner);
        }

        protected override clsInactiveRecordInfo_VO[] m_objSubGetAllInactiveInfo(clsInactiveRecordInfo_VO p_objSelectedValue)
        {
            clsInactiveRecordInfo_VO[] objArr = null;
            new clsInpatMedRecDomain().m_lngGetAllInactiveInfo(this.Name, p_objSelectedValue.m_StrInpatientId, p_objSelectedValue.m_DtmInpatientDate, out objArr);
            return objArr;
        }
        public override bool m_blnIsNewSetInactiveForm
        {
            get { return true; }
        }
        #endregion ��������

        /// <summary>
        /// ����¼�����Ƿ���Ч
        /// </summary>
        /// <returns></returns>
        private bool m_blnCheckCreateDate()
        {
            bool blnCheck = true;
            if (m_ObjCurrentEmrPatientSession != null)
            {
                if (m_ObjCurrentEmrPatientSession.m_dtmHISInpatientDate >= m_dtpCreateDate.Value)
                {
                    clsPublicFunction.ShowInformationMessageBox("��¼ʱ�䲻��С����Ժʱ��");
                    blnCheck = false;
                }
            }
            return blnCheck;
        }
    }
}

