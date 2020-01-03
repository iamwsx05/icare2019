using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;
using com.digitalwave.iCare.middletier.HI;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// frmreckoning ��ժҪ˵����
    /// </summary>
    public class frmreckoning : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        internal Label lbePayBack;
        internal System.Windows.Forms.TextBox txtFactPay;
        public System.Windows.Forms.Label lbeSumMoney;
        public System.Windows.Forms.Label lbeSelfPay;
        public System.Windows.Forms.Label lbeChargeUp;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        internal System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        internal com.digitalwave.iCare.gui.HIS.exComboBox cmbHopital;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbPayType;
        internal System.Windows.Forms.Button btBreak;
        private System.Windows.Forms.Label lblpaycardtype;
        internal System.Windows.Forms.ComboBox cbopaycardtype;
        private System.Windows.Forms.Label lblpaycardno;
        internal System.Windows.Forms.TextBox txtpaycardno;
        internal ListView lvcardnolist;
        private ColumnHeader columnHeader1;
        private Label lblIdcard;
        internal TextBox txtIdcard;
        private System.ComponentModel.IContainer components;
        private bool succeed = false;
        private bool IsPress = false;//true��ʾ�Ѿ����˻س�,false��ʾû��.

        /// <summary>
        /// �������ÿ���
        /// </summary>
        internal bool m_blnDiffOn = false;
        internal string m_strPayBack = "";
        internal DevExpress.XtraEditors.SimpleButton btnYb;
        internal DevExpress.XtraEditors.SimpleButton btnNewYb;
        internal DevExpress.XtraEditors.SimpleButton btnGS;
        internal DevExpress.XtraEditors.SimpleButton btTurn;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarStaticItem lblInfo;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal string m_strPersonPay = "";

        public frmreckoning()
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();
            IsPress = false;
            Application.Idle += new EventHandler(Application_Idle);
            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
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

        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmreckoning));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbeSumMoney = new System.Windows.Forms.Label();
            this.lbeSelfPay = new System.Windows.Forms.Label();
            this.lbeChargeUp = new System.Windows.Forms.Label();
            this.lbePayBack = new System.Windows.Forms.Label();
            this.txtFactPay = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbHopital = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnYb = new DevExpress.XtraEditors.SimpleButton();
            this.btnNewYb = new DevExpress.XtraEditors.SimpleButton();
            this.btnGS = new DevExpress.XtraEditors.SimpleButton();
            this.btTurn = new DevExpress.XtraEditors.SimpleButton();
            this.btBreak = new System.Windows.Forms.Button();
            this.txtIdcard = new System.Windows.Forms.TextBox();
            this.lblIdcard = new System.Windows.Forms.Label();
            this.lvcardnolist = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtpaycardno = new System.Windows.Forms.TextBox();
            this.cbopaycardtype = new System.Windows.Forms.ComboBox();
            this.lblpaycardno = new System.Windows.Forms.Label();
            this.lblpaycardtype = new System.Windows.Forms.Label();
            this.cmbPayType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.lblInfo = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("����", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(20, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 35);
            this.label1.TabIndex = 11;
            this.label1.Text = "�� �� ��:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("����", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(20, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 35);
            this.label2.TabIndex = 1;
            this.label2.Text = "�Ը����:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("����", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(20, 320);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 35);
            this.label3.TabIndex = 3;
            this.label3.Text = "ʵ    ��:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("����", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(20, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 35);
            this.label4.TabIndex = 4;
            this.label4.Text = "���ʽ��:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("����", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(20, 392);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(182, 35);
            this.label5.TabIndex = 5;
            this.label5.Text = "��    ��:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbeSumMoney
            // 
            this.lbeSumMoney.Font = new System.Drawing.Font("����", 34F, System.Drawing.FontStyle.Bold);
            this.lbeSumMoney.Location = new System.Drawing.Point(212, 22);
            this.lbeSumMoney.Name = "lbeSumMoney";
            this.lbeSumMoney.Size = new System.Drawing.Size(232, 46);
            this.lbeSumMoney.TabIndex = 6;
            this.lbeSumMoney.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbeSelfPay
            // 
            this.lbeSelfPay.Font = new System.Drawing.Font("����", 34F, System.Drawing.FontStyle.Bold);
            this.lbeSelfPay.ForeColor = System.Drawing.Color.Blue;
            this.lbeSelfPay.Location = new System.Drawing.Point(212, 166);
            this.lbeSelfPay.Name = "lbeSelfPay";
            this.lbeSelfPay.Size = new System.Drawing.Size(232, 46);
            this.lbeSelfPay.TabIndex = 7;
            this.lbeSelfPay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbeChargeUp
            // 
            this.lbeChargeUp.Font = new System.Drawing.Font("����", 34F, System.Drawing.FontStyle.Bold);
            this.lbeChargeUp.Location = new System.Drawing.Point(212, 94);
            this.lbeChargeUp.Name = "lbeChargeUp";
            this.lbeChargeUp.Size = new System.Drawing.Size(232, 46);
            this.lbeChargeUp.TabIndex = 8;
            this.lbeChargeUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbePayBack
            // 
            this.lbePayBack.Font = new System.Drawing.Font("����", 30F, System.Drawing.FontStyle.Bold);
            this.lbePayBack.ForeColor = System.Drawing.Color.Red;
            this.lbePayBack.Location = new System.Drawing.Point(212, 393);
            this.lbePayBack.Name = "lbePayBack";
            this.lbePayBack.Size = new System.Drawing.Size(200, 46);
            this.lbePayBack.TabIndex = 10;
            this.lbePayBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFactPay
            // 
            this.txtFactPay.CausesValidation = false;
            this.txtFactPay.Font = new System.Drawing.Font("����", 30F, System.Drawing.FontStyle.Bold);
            this.txtFactPay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.txtFactPay.Location = new System.Drawing.Point(212, 320);
            this.txtFactPay.Name = "txtFactPay";
            this.txtFactPay.Size = new System.Drawing.Size(184, 53);
            this.txtFactPay.TabIndex = 0;
            this.txtFactPay.Text = "0";
            this.txtFactPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFactPay.TextChanged += new System.EventHandler(this.txtFactPay_TextChanged);
            this.txtFactPay.Enter += new System.EventHandler(this.txtFactPay_Enter);
            this.txtFactPay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFactPay_KeyDown);
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(404, 312);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(123, 32);
            this.radioButton1.TabIndex = 14;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "��ӡ��Ʊ(F12)";
            this.radioButton1.Visible = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(404, 344);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(83, 24);
            this.radioButton2.TabIndex = 15;
            this.radioButton2.Text = "Ԥ����Ʊ";
            this.radioButton2.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.cmbHopital);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(820, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 32);
            this.panel1.TabIndex = 16;
            this.panel1.Visible = false;
            // 
            // cmbHopital
            // 
            this.cmbHopital.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHopital.Font = new System.Drawing.Font("����", 24F);
            this.cmbHopital.Location = new System.Drawing.Point(200, 208);
            this.cmbHopital.Name = "cmbHopital";
            this.cmbHopital.Size = new System.Drawing.Size(200, 41);
            this.cmbHopital.TabIndex = 44;
            this.cmbHopital.SelectedIndexChanged += new System.EventHandler(this.cmbHopital_SelectedIndexChanged);
            this.cmbHopital.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbHopital_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("����", 28F);
            this.label7.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label7.Location = new System.Drawing.Point(8, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(188, 38);
            this.label7.TabIndex = 2;
            this.label7.Text = "����ҽԺ:";
            // 
            // btnYb
            // 
            this.btnYb.Appearance.Font = new System.Drawing.Font("����", 9.5F);
            this.btnYb.Appearance.Options.UseFont = true;
            this.btnYb.Image = ((System.Drawing.Image)(resources.GetObject("btnYb.Image")));
            this.btnYb.Location = new System.Drawing.Point(364, 458);
            this.btnYb.Name = "btnYb";
            this.btnYb.Size = new System.Drawing.Size(112, 34);
            this.btnYb.TabIndex = 26;
            this.btnYb.Text = "���ļ��籣 ��";
            this.btnYb.Click += new System.EventHandler(this.btnYb_Click);
            // 
            // btnNewYb
            // 
            this.btnNewYb.Appearance.Font = new System.Drawing.Font("����", 9.5F);
            this.btnNewYb.Appearance.Options.UseFont = true;
            this.btnNewYb.Image = ((System.Drawing.Image)(resources.GetObject("btnNewYb.Image")));
            this.btnNewYb.Location = new System.Drawing.Point(234, 458);
            this.btnNewYb.Name = "btnNewYb";
            this.btnNewYb.Size = new System.Drawing.Size(112, 34);
            this.btnNewYb.TabIndex = 25;
            this.btnNewYb.Text = "��Ƕ��ʽ�籣";
            this.btnNewYb.Click += new System.EventHandler(this.btnNewYb_Click);
            // 
            // btnGS
            // 
            this.btnGS.Appearance.Font = new System.Drawing.Font("����", 9.5F);
            this.btnGS.Appearance.Options.UseFont = true;
            this.btnGS.Enabled = false;
            this.btnGS.Image = ((System.Drawing.Image)(resources.GetObject("btnGS.Image")));
            this.btnGS.Location = new System.Drawing.Point(120, 458);
            this.btnGS.Name = "btnGS";
            this.btnGS.Size = new System.Drawing.Size(96, 34);
            this.btnGS.TabIndex = 24;
            this.btnGS.Text = "ʡ�����籣";
            this.btnGS.Click += new System.EventHandler(this.btnGS_Click);
            // 
            // btTurn
            // 
            this.btTurn.Appearance.Font = new System.Drawing.Font("����", 9.5F);
            this.btTurn.Appearance.Options.UseFont = true;
            this.btTurn.Image = ((System.Drawing.Image)(resources.GetObject("btTurn.Image")));
            this.btTurn.Location = new System.Drawing.Point(22, 458);
            this.btTurn.Name = "btTurn";
            this.btTurn.Size = new System.Drawing.Size(80, 34);
            this.btTurn.TabIndex = 23;
            this.btTurn.Text = "ת��";
            this.btTurn.Click += new System.EventHandler(this.btTurn_Click);
            // 
            // btBreak
            // 
            this.btBreak.BackColor = System.Drawing.Color.White;
            this.btBreak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btBreak.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btBreak.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBreak.Image = ((System.Drawing.Image)(resources.GetObject("btBreak.Image")));
            this.btBreak.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btBreak.Location = new System.Drawing.Point(524, 467);
            this.btBreak.Name = "btBreak";
            this.btBreak.Size = new System.Drawing.Size(116, 37);
            this.btBreak.TabIndex = 18;
            this.btBreak.Text = "�ַ�Ʊ(F5)";
            this.btBreak.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btBreak.UseVisualStyleBackColor = false;
            this.btBreak.Visible = false;
            this.btBreak.Click += new System.EventHandler(this.btBreak_Click);
            // 
            // txtIdcard
            // 
            this.txtIdcard.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdcard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtIdcard.Location = new System.Drawing.Point(484, 174);
            this.txtIdcard.Name = "txtIdcard";
            this.txtIdcard.Size = new System.Drawing.Size(194, 29);
            this.txtIdcard.TabIndex = 22;
            this.txtIdcard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdcard_KeyDown);
            // 
            // lblIdcard
            // 
            this.lblIdcard.AutoSize = true;
            this.lblIdcard.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdcard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lblIdcard.Location = new System.Drawing.Point(395, 176);
            this.lblIdcard.Name = "lblIdcard";
            this.lblIdcard.Size = new System.Drawing.Size(85, 16);
            this.lblIdcard.TabIndex = 21;
            this.lblIdcard.Text = "���֤��:";
            this.lblIdcard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIdcard.Visible = false;
            // 
            // lvcardnolist
            // 
            this.lvcardnolist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvcardnolist.Font = new System.Drawing.Font("Arial Narrow", 18F);
            this.lvcardnolist.ForeColor = System.Drawing.Color.Black;
            this.lvcardnolist.FullRowSelect = true;
            this.lvcardnolist.GridLines = true;
            this.lvcardnolist.Location = new System.Drawing.Point(421, 16);
            this.lvcardnolist.Name = "lvcardnolist";
            this.lvcardnolist.Size = new System.Drawing.Size(257, 155);
            this.lvcardnolist.TabIndex = 20;
            this.lvcardnolist.UseCompatibleStateImageBehavior = false;
            this.lvcardnolist.View = System.Windows.Forms.View.Details;
            this.lvcardnolist.Visible = false;
            this.lvcardnolist.DoubleClick += new System.EventHandler(this.lvcardnolist_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "��/�ʺ�";
            this.columnHeader1.Width = 251;
            // 
            // txtpaycardno
            // 
            this.txtpaycardno.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpaycardno.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtpaycardno.Location = new System.Drawing.Point(484, 240);
            this.txtpaycardno.Name = "txtpaycardno";
            this.txtpaycardno.Size = new System.Drawing.Size(194, 29);
            this.txtpaycardno.TabIndex = 19;
            this.txtpaycardno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpaycardno_KeyDown);
            // 
            // cbopaycardtype
            // 
            this.cbopaycardtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbopaycardtype.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbopaycardtype.ForeColor = System.Drawing.Color.Black;
            this.cbopaycardtype.Location = new System.Drawing.Point(484, 174);
            this.cbopaycardtype.Name = "cbopaycardtype";
            this.cbopaycardtype.Size = new System.Drawing.Size(194, 24);
            this.cbopaycardtype.TabIndex = 15;
            this.cbopaycardtype.Visible = false;
            this.cbopaycardtype.SelectedIndexChanged += new System.EventHandler(this.cbopaycardtype_SelectedIndexChanged);
            // 
            // lblpaycardno
            // 
            this.lblpaycardno.AutoSize = true;
            this.lblpaycardno.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpaycardno.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lblpaycardno.Location = new System.Drawing.Point(428, 240);
            this.lblpaycardno.Name = "lblpaycardno";
            this.lblpaycardno.Size = new System.Drawing.Size(51, 16);
            this.lblpaycardno.TabIndex = 16;
            this.lblpaycardno.Text = "����:";
            this.lblpaycardno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblpaycardno.Visible = false;
            // 
            // lblpaycardtype
            // 
            this.lblpaycardtype.AutoSize = true;
            this.lblpaycardtype.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpaycardtype.ForeColor = System.Drawing.Color.Black;
            this.lblpaycardtype.Location = new System.Drawing.Point(412, 176);
            this.lblpaycardtype.Name = "lblpaycardtype";
            this.lblpaycardtype.Size = new System.Drawing.Size(68, 16);
            this.lblpaycardtype.TabIndex = 14;
            this.lblpaycardtype.Text = "������:";
            this.lblpaycardtype.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblpaycardtype.Visible = false;
            // 
            // cmbPayType
            // 
            this.cmbPayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayType.Font = new System.Drawing.Font("����", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPayType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.cmbPayType.Location = new System.Drawing.Point(212, 240);
            this.cmbPayType.Name = "cmbPayType";
            this.cmbPayType.Size = new System.Drawing.Size(184, 37);
            this.cmbPayType.TabIndex = 13;
            this.cmbPayType.SelectedIndexChanged += new System.EventHandler(this.cmbPayType_SelectedIndexChanged);
            this.cmbPayType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPayType_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("����", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.label8.Location = new System.Drawing.Point(20, 240);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(178, 35);
            this.label8.TabIndex = 12;
            this.label8.Text = "���ʽ:";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.lblInfo});
            this.barManager1.MaxItemId = 1;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.lblInfo)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // lblInfo
            // 
            this.lblInfo.Caption = "����";
            this.lblInfo.Id = 0;
            this.lblInfo.ItemAppearance.Disabled.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ItemAppearance.Disabled.Options.UseFont = true;
            this.lblInfo.ItemAppearance.Hovered.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ItemAppearance.Hovered.Options.UseFont = true;
            this.lblInfo.ItemAppearance.Normal.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ItemAppearance.Normal.Options.UseFont = true;
            this.lblInfo.ItemAppearance.Pressed.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ItemAppearance.Pressed.Options.UseFont = true;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(482, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 505);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(482, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 505);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(482, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 505);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnYb);
            this.panelControl1.Controls.Add(this.lvcardnolist);
            this.panelControl1.Controls.Add(this.btnNewYb);
            this.panelControl1.Controls.Add(this.txtIdcard);
            this.panelControl1.Controls.Add(this.btnGS);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.btTurn);
            this.panelControl1.Controls.Add(this.lblIdcard);
            this.panelControl1.Controls.Add(this.btBreak);
            this.panelControl1.Controls.Add(this.lbeChargeUp);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.radioButton1);
            this.panelControl1.Controls.Add(this.txtpaycardno);
            this.panelControl1.Controls.Add(this.radioButton2);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.txtFactPay);
            this.panelControl1.Controls.Add(this.cbopaycardtype);
            this.panelControl1.Controls.Add(this.lbePayBack);
            this.panelControl1.Controls.Add(this.lbeSumMoney);
            this.panelControl1.Controls.Add(this.lblpaycardno);
            this.panelControl1.Controls.Add(this.lbeSelfPay);
            this.panelControl1.Controls.Add(this.lblpaycardtype);
            this.panelControl1.Controls.Add(this.label8);
            this.panelControl1.Controls.Add(this.cmbPayType);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(482, 505);
            this.panelControl1.TabIndex = 23;
            // 
            // frmreckoning
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(482, 528);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Font = new System.Drawing.Font("����", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmreckoning";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "���㴰��";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmreckoning_Closing);
            this.Load += new System.EventHandler(this.frmreckoning_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmreckoning_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        #region ���ô���
        private com.digitalwave.iCare.gui.HIS.frmOPCharge objfrm;
        public void m_mthSetParentFrom(com.digitalwave.iCare.gui.HIS.frmOPCharge obj)
        {
            objfrm = obj;
        }

        #endregion

        private void txtFactPay_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (txtFactPay.Text.Trim() != "")
                {
                    decimal decPayback = Convert.ToDecimal(txtFactPay.Text) - Convert.ToDecimal(lbeSelfPay.Text);
                    lbePayBack.Text = decPayback.ToString();

                }
                else
                {
                    lbePayBack.Text = "";
                }
            }
            catch
            { }
        }

        private void txtFactPay_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (IsPress)
                    {
                        return;
                    }

                    #region У��
                    if (this.txtIdcard.Visible)
                    {
                        if (this.txtIdcard.Text.Trim() == "" || (this.txtIdcard.Text.Trim().Length != 15 && this.txtIdcard.Text.Trim().Length != 18))
                        {
                            MessageBox.Show("���֤�Ų���Ϊ�ղ���λ��������15λ��18λ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            this.txtIdcard.Focus();
                            return;
                        }

                        if (this.txtpaycardno.Text.Trim() == "")
                        {
                            MessageBox.Show("IC���Ų���Ϊ�գ������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            this.txtpaycardno.Focus();
                            return;
                        }

                        idcardno = this.txtIdcard.Text.Trim();
                    }

                    if (this.m_Invoice.Count == 1)
                    {
                        string str = "";
                        if (this.txtFactPay.Text.Trim() == "")
                        {
                            str = "ʵ�ս��Ϊ0���Ƿ������";
                        }
                        else
                        {
                            if (Convert.ToDecimal(txtFactPay.Text) - Convert.ToDecimal(lbeSelfPay.Text) < 0)
                            {
                                str = "ʵ�ս��С���Ը����Ƿ������";
                            }
                        }

                        if (str != "")
                        {
                            if (MessageBox.Show(str, "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            {
                                return;
                            }
                        }
                    }
                    #endregion

                    //�����ƺ����Ĳ��˽�ԭ�����Ż�ͨ���������Ҫ�˵�
                    if (this.objfrm.m_PatientBasicInfo.m_strIsVip == "1")
                    {
                        //�����ѡ���Խ��ѣ��ڽ����ʱ�������û��Ƿ����
                        if (this.objfrm.m_blnIsSelectChargeItem)
                        {
                            if (MessageBox.Show("��ȷ��ִ�в��ֽ��Ѳ����𣿴˲��������棡", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }
                        }

                        clsDcl_InvoiceManage m_objManage = new clsDcl_InvoiceManage();
                        string Seqid = "";
                        if (this.objfrm.txtLoadRecipeNO.Tag != null && !string.IsNullOrEmpty(this.objfrm.txtLoadRecipeNO.Tag.ToString()))
                        {
                            int intFlag = 1;
                            long lngRet = m_objManage.m_lngReturnTicket(this.objfrm.txtLoadRecipeNO.Tag.ToString(), this.objfrm.LoginInfo.m_strEmpID, ref Seqid, intFlag);
                            if (lngRet > 0)
                            {
                                lngRet = this.objfrm.m_mthManualNewRecipe();
                            }
                            else
                            {
                                MessageBox.Show("�����ƺ�����˷�ʧ�ܣ�����ϵ����Ա��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }

                    IsPress = true;

                    this.Cursor = Cursors.WaitCursor;

                    this.txtFactPay.Enabled = false;
                    this.btBreak.Enabled = false;

                    this.m_mthShowinfo("���ڱ�������");
                    //����0��ʾ����ɹ�                   
                    if (m_mthSaveData() > 0)
                    {
                        //���淢Ʊ��Ϣ
                        this.m_mthSaveInvoice();

                        this.succeed = true;

                        objfrm.m_mthWriteChangeMoney(this.lbePayBack.Text);

                        //��ӡ
                        this.m_mthShowinfo("���ڴ�ӡ��Ʊ");
                        m_mthPrint();

                        this.m_strPayBack = this.lbePayBack.Text.Trim();
                        this.m_strPersonPay = this.txtFactPay.Text.Trim();

                        #region ��ҩ��������Ϣ��������ҩ����Ƭ��

                        if (/*objfrm.IsRecipeHttpPost &&*/ objfrm.IsCmRecipe)
                        {
                            this.RecipeHttpPost(objfrm.txtLoadRecipeNO.Text);
                        }
                        #endregion

                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.Abort;
                        this.m_mthShowinfo("��������ʧ��");
                    }

                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
                IsPress = false;
                this.m_mthShowinfo("����ʧ��");
                this.btBreak.Enabled = true;
            }
        }
        #region ��ӡ��Ʊ
        private void m_mthPrint()
        {
            string strInvoiceNO;
            for (int i = 0; i < this.m_Invoice.Count; i++)
            {
                clsPatientChargeCal objPCC2 = (clsPatientChargeCal)this.m_Invoice[i];

                if (this.m_Invoice.Count == 1)
                {
                    objPCC2.m_strBalanceMode = this.cmbPayType.Text;
                }
                else
                {
                    switch (objPCC2.m_strPayTypeIndex)
                    {
                        case "0":
                            objPCC2.m_strBalanceMode = "�ֽ�";
                            break;
                        case "1":
                            objPCC2.m_strBalanceMode = "���п�";
                            break;
                        case "2":
                            objPCC2.m_strBalanceMode = "֧Ʊ";
                            break;
                        case "3":
                            objPCC2.m_strBalanceMode = "IC��";
                            break;
                        case "4":
                            objPCC2.m_strBalanceMode = "�籣Ԥ�Ʒ�";
                            break;
                        case "5":
                            objPCC2.m_strBalanceMode = "΢��2";
                            break;
                        case "6":
                            objPCC2.m_strBalanceMode = "֧����";
                            break;
                    }
                }

                if (objfrm.m_mthPrintInvoice(objPCC2, out strInvoiceNO) > 0)
                {
                }
                else
                {
                    MessageBox.Show("��ӡ����");
                    break;
                }
            }
        }
        #endregion

        #region ��������
        private long m_mthSaveData()
        {
            clsPatientChargeCal[] objPCC = new clsPatientChargeCal[this.m_Invoice.Count];

            if (this.m_Invoice.Count == 1)
            {
                //��־λ
                int pos = 0;
                //֧��������
                string paycardtype = "";
                //֧������/�ʺ�
                string paycardno = "";

                paycardtype = this.cbopaycardtype.Text.Trim();
                if (paycardtype != "")
                {
                    pos = paycardtype.IndexOf("]");
                    paycardtype = paycardtype.Substring(1, pos - 1);
                }

                paycardno = this.txtpaycardno.Text.Trim();

                ((clsPatientChargeCal)m_Invoice[0]).Paycardtype = paycardtype;
                ((clsPatientChargeCal)m_Invoice[0]).Paycardno = paycardno;
            }

            string strTemp = DateTime.Now.ToString("yyMMddHHmmssffffff");
            for (int i = 0; i < this.m_Invoice.Count; i++)
            {
                ((clsPatientChargeCal)m_Invoice[i]).m_strInvoiceNO = this.m_strInvoiceNo;
                ((clsPatientChargeCal)m_Invoice[i]).m_strSeriesNumber = strTemp;
                objPCC[i] = (clsPatientChargeCal)m_Invoice[i];
                this.m_mthInvoiceOpertator();
                //�ȴ���Ϊ�˷�ֹ�������ظ�
                while (strTemp == DateTime.Now.ToString("yyMMddHHmmssffffff"))
                {

                }
                strTemp = DateTime.Now.ToString("yyMMddHHmmssffffff");
            }

            objfrm.txtIDcard.Text = this.idcardno;
            return objfrm.m_mthSaveAllData(objPCC);
        }
        #endregion

        #region RecipeHttpPost
        /// <summary>
        /// RecipeHttpPost
        /// </summary>
        /// <param name="recipeId"></param>
        void RecipeHttpPost(string recipeId)
        {
            try
            {
                string xmlIn = string.Empty;
                xmlIn += "<req>" + Environment.NewLine;
                xmlIn += string.Format("<recipeId>{0}</recipeId>", recipeId) + Environment.NewLine;
                xmlIn += string.Format("<putMedId>{0}</putMedId>", "") + Environment.NewLine;
                xmlIn += string.Format("<opIp>{0}</opIp>", "1") + Environment.NewLine;
                xmlIn += "</req>" + Environment.NewLine;
                using (MedService ms = new MedService())
                {
                    string res = ms.FireBird(xmlIn);
                }
            }
            catch (Exception ex)
            {
                weCare.Core.Utils.Log.Output(ex.Message);
            }

            #region ����WSDL

            ////��ȡ������Ϣ
            //string res = string.Empty;
            //if (string.IsNullOrEmpty(objfrm.HttpUri)) return;

            //string postData = HttpUtility.UrlEncode("request") + "=" + HttpUtility.UrlEncode(recipeId);
            //byte[] dataArray = Encoding.Default.GetBytes(postData);
            ////��������
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(objfrm.HttpUri);
            //request.Method = "POST";
            //request.ContentLength = dataArray.Length;
            //request.ContentType = "application/x-www-form-urlencoded";
            ////����������
            //Stream dataStream = null;
            //try
            //{
            //    dataStream = request.GetRequestStream();
            //}
            //catch (WebException ex)
            //{
            //    res = ex.Message;
            //    Log.Output(res);
            //    return;//���ӷ�����ʧ��
            //}
            ////��������
            //dataStream.Write(dataArray, 0, dataArray.Length);
            //dataStream.Close();

            ////// ����ֵ
            ////try
            ////{
            ////    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            ////    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            ////    res = reader.ReadToEnd();
            ////    reader.Close();
            ////}
            ////catch (WebException ex)
            ////{
            ////    res = ex.Message;
            ////}
            ////MessageBox.Show(res);
            #endregion
        }
        #endregion

        private void m_mthSaveInvoice()
        {
            objfrm.m_mthSaveInvoiceNO(m_strInvoiceNo);
        }
        #region ��Ʊ��
        /// <summary>
        /// ��Ʊ��
        /// </summary>
        private string m_strInvoiceNo = "";
        private void m_mthInvoiceOpertator()
        {
            try
            {
                int maxint = Convert.ToInt32(m_strInvoiceNo.Substring(2, 8)) + 1;
                m_strInvoiceNo = m_strInvoiceNo.Substring(0, 2) + maxint.ToString("00000000");
            }
            catch
            {
                m_strInvoiceNo = "0000000000";
            }
        }

        #endregion
        private void frmreckoning_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter && (this.radioButton1.Focused | this.radioButton2.Focused))
            {
                this.txtFactPay.Focus();
            }

            if (e.KeyCode == Keys.F5 && this.btBreak.Enabled == true)
            {
                this.btBreak_Click(null, null);
            }
            if (e.KeyCode == Keys.F3 && this.btTurn.Enabled == true)
            {
                this.btTurn_Click(null, null);
            }
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, System.EventArgs e)
        {

        }

        private void txtPrint_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "0" || e.KeyChar.ToString() == "1")
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void frmreckoning_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.succeed)
            {
                this.m_mthShowinfo("����");
                this.DialogResult = DialogResult.OK;
            }
        }

        private void cmbHopital_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtFactPay.Focus();
            }
        }

        private void cmbHopital_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.cmbHopital.SelectedIndex > -1)
            {
                objfrm.SetHospital = this.cmbHopital.SelectItemValue;
            }
        }
        private frmBreakInvoice objfrmBI = null;
        private void btBreak_Click(object sender, System.EventArgs e)
        {
            if (objfrmBI == null)
            {
                objfrmBI = new frmBreakInvoice(objPCVO);
            }
            else
            {
                objfrmBI.ShowTimes = 2;
            }
            objfrmBI.cmbPayTpye.Items.Clear();
            foreach (Object o in this.cmbPayType.Items)
            {
                objfrmBI.cmbPayTpye.Items.Add(o);
            }
            objfrmBI.cmbPayTpye.SelectedIndex = this.cmbPayType.SelectedIndex;

            objfrmBI.cbopaycardtype.Items.Clear();
            foreach (Object o in this.cbopaycardtype.Items)
            {
                objfrmBI.cbopaycardtype.Items.Add(o);
            }

            if (objfrmBI.ShowDialog() != DialogResult.OK)
            {
                objfrmBI.m_mthHandleMoney(objPCVO, true);
            }
            if (objfrmBI.InvoiceInfoArr != null)
            {
                m_Invoice = objfrmBI.InvoiceInfoArr;
                if (m_Invoice.Count > 1)
                {
                    this.btTurn.Enabled = false;
                }
            }
            objfrmBI.Hide();
            this.txtFactPay.Focus();
        }
        #region ��ȡ���ַ�Ʊ��ļ���
        private ArrayList m_Invoice;
        public ArrayList InvoiceInfoArr
        {
            get
            {
                return m_Invoice;
            }
        }
        #endregion
        #region ԭ��Ʊ��Ϣ����
        private clsPatientChargeCal objPCVO;
        /// <summary>
        /// ԭ��Ʊ��Ϣ����
        /// </summary>
        public clsPatientChargeCal PreInvoiceInfo
        {
            set
            {
                objPCVO = value;
                m_Invoice = new ArrayList();
                m_Invoice.Add(value);
                this.m_strInvoiceNo = objPCVO.m_strInvoiceNO;
            }
        }
        #endregion

        #region ����û���շѵĴ�������
        /// <summary>
        /// ����û���շѵĴ�������
        /// </summary>
        /// <param name="count"></param>
        public void m_mthShowRecipeCount(int count)
        {
            if (count > 0)
            {
                this.Text = "�ò��˻���" + count.ToString() + "�Ŵ���û�н���";
            }

        }
        #endregion

        private void Application_Idle(object sender, EventArgs e)
        {
            if (this.succeed)
            {
                this.btBreak.Enabled = false;
                this.btTurn.Enabled = false;
            }
            else
            {
                this.btBreak.Enabled = true;
                this.btTurn.Enabled = true;
            }
        }

        private void btTurn_Click(object sender, System.EventArgs e)
        {
            frmTurnMoney objfrm = new frmTurnMoney();
            objfrm.SumMondey = objPCVO.m_decTotalCost;
            objfrm.PersonMondey = objPCVO.m_decPersonCost;
            objfrm.ChargeUpMondey = objPCVO.m_decChargeUpCost;
            if (objfrm.ShowDialog() == DialogResult.OK)
            {
                objPCVO.m_decChargeUpCost = objfrm.ChargeUpMondey;
                objPCVO.m_decPersonCost = objfrm.PersonMondey;
                if (this.m_Invoice.Count == 1)
                {
                    ((clsPatientChargeCal)m_Invoice[0]).m_decChargeUpCost = objfrm.ChargeUpMondey;
                    this.lbeChargeUp.Text = objfrm.ChargeUpMondey.ToString("0.00");
                    ((clsPatientChargeCal)m_Invoice[0]).m_decPersonCost = objfrm.PersonMondey;
                    this.lbeSelfPay.Text = objfrm.PersonMondey.ToString("0.00");
                }
                objfrm.Close();
            }
        }

        private void cmbPayType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string index = this.cmbPayType.SelectedIndex.ToString();

            objfrm.m_cmbPayMode.SelectedIndex = cmbPayType.SelectedIndex;
            objPCVO.m_strPayTypeIndex = index;

            this.lblpaycardtype.Visible = false;
            this.cbopaycardtype.Visible = false;
            this.lblpaycardno.Visible = false;
            this.txtpaycardno.Visible = false;
            this.lvcardnolist.Visible = false;
            this.lblpaycardno.Text = "����:";
            this.txtpaycardno.Text = "";
            this.lblIdcard.Visible = false;
            this.txtIdcard.Visible = false;
            this.Width = 688;

            switch (index)
            {
                case "0":
                    this.Width = 488;
                    break;
                case "1":
                    this.lblpaycardtype.Visible = true;
                    this.cbopaycardtype.Visible = true;
                    this.lblpaycardno.Visible = true;
                    this.txtpaycardno.Visible = true;
                    break;
                case "2":
                    this.lblpaycardno.Text = "�ʺ�:";
                    this.lblpaycardno.Visible = true;
                    this.txtpaycardno.Visible = true;
                    break;
                case "3":
                    this.lblIdcard.Visible = true;
                    this.txtIdcard.Visible = true;
                    this.txtIdcard.Text = this.idcardno;
                    this.lblpaycardno.Visible = true;
                    this.txtpaycardno.Visible = true;
                    this.txtpaycardno.Text = this.iccardno;
                    break;
                case "4":
                    this.Width = 488;
                    break;
                case "5":
                    this.Width = 488;
                    break;
            }
        }

        private void frmreckoning_Load(object sender, System.EventArgs e)
        {
            for (int i = 0; i < this.objfrm.m_cmbPayMode.Items.Count; i++)
            {
                this.cmbPayType.Items.Add(this.objfrm.m_cmbPayMode.Items[i]);
            }
            this.cmbPayType.SelectedIndex = this.objfrm.m_cmbPayMode.SelectedIndex;
            this.cmbPayType.Select();
            this.txtFactPay.Text = "";
            this.m_mthShowpaycardlist();
            this.m_mthGetpaycardno();
            this.m_mthShowinfo("����");
            this.btBreak.Enabled = true;

            this.Top += 120;
        }

        private void cmbPayType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cmbPayType.Text.Trim() == "�ֽ�" || this.cmbPayType.Text.StartsWith("΢��") || this.cmbPayType.Text.StartsWith("֧����"))
                {
                    this.txtFactPay.Select();
                }
                else if (this.cmbPayType.Text.Trim() == "IC��")
                {
                    this.txtIdcard.Focus();
                }
                else
                {
                    this.txtpaycardno.Focus();
                }
            }
        }

        #region
        public int PatientType
        {
            set
            {
                if (value == 2)//����ҽ�����˲���ʹת�˹���
                {
                    this.btTurn.Visible = true;

                }
                else
                {
                    this.btTurn.Visible = true;
                }

            }
        }
        /// <summary>
        /// �����ܷ�ת��,true ��,false����
        /// </summary>
        public bool IsCanTurn
        {
            set
            {
                if (!value)
                {
                    this.btTurn.Visible = false;
                }

            }
        }
        #endregion

        #region ��ʾ֧�����б�
        /// <summary>
        /// ��ʾ֧�����б�
        /// </summary>
        private void m_mthShowpaycardlist()
        {
            DataTable dtRecord = new DataTable();
            clsDcl_OPCharge objOP = new clsDcl_OPCharge();
            long ret = objOP.m_lngGetPaycardtype(out dtRecord);
            if (dtRecord != null && dtRecord.Rows.Count > 0)
            {
                this.cbopaycardtype.BeginUpdate();
                this.cbopaycardtype.Items.Clear();
                for (int i = 0; i < dtRecord.Rows.Count; i++)
                {
                    this.cbopaycardtype.Items.Add("[" + dtRecord.Rows[i]["paycardtype_int"].ToString() + "]" + dtRecord.Rows[i]["paycarddesc_vchr"].ToString());
                }
                this.cbopaycardtype.SelectedIndex = 0;
                this.cbopaycardtype.EndUpdate();
            }
            objOP = null;
        }
        #endregion

        #region ������ǰ���ߵ����н��㿨��Ϣ
        /// <summary>
        /// ������ǰ���ߵ����н��㿨��Ϣ
        /// </summary>
        private void m_mthGetpaycardno()
        {
            clsDcl_OPCharge objOP = new clsDcl_OPCharge();
            long ret = objOP.m_lngGetPaycardno(out dtCard, pid);
            objOP = null;
        }
        #endregion

        private void txtpaycardno_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtFactPay.Focus();
            }
        }

        private void cbopaycardtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lvcardnolist.Visible = false;
            int pos = 0;
            string paycardtype = this.cbopaycardtype.Text.Trim();
            if (paycardtype != "")
            {
                pos = paycardtype.IndexOf("]");
                paycardtype = paycardtype.Substring(1, pos - 1);

                if (dtCard != null && dtCard.Rows.Count > 0)
                {
                    DataRow[] drCard = dtCard.Select("paycardtype_int = " + paycardtype);

                    if (drCard.Length > 0)
                    {
                        this.lvcardnolist.BeginUpdate();
                        this.lvcardnolist.Items.Clear();
                        for (int i = 0; i < drCard.Length; i++)
                        {
                            lvcardnolist.Items.Add(drCard[i]["paycardno_vchr"].ToString());
                        }
                        this.lvcardnolist.EndUpdate();
                        this.lvcardnolist.Visible = true;
                    }
                }
            }
        }

        private void lvcardnolist_DoubleClick(object sender, EventArgs e)
        {
            if (this.lvcardnolist.SelectedItems.Count > 0)
            {
                this.txtpaycardno.Text = this.lvcardnolist.SelectedItems[0].SubItems[0].Text.Trim();
            }
        }

        #region ����
        private DataTable dtCard = new DataTable();

        private string pid = "";
        public string Pid
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

        private string idcardno = "";
        /// <summary>
        /// ���֤��
        /// </summary>
        public string Idcardno
        {
            set
            {
                idcardno = value;
            }
            get
            {
                return idcardno;
            }
        }

        private string iccardno = "";
        /// <summary>
        /// IC������
        /// </summary>
        public string Iccardno
        {
            set
            {
                iccardno = value;
            }
            get
            {
                return iccardno;
            }
        }

        /// <summary>
        /// 001 ��ɽ�� 002 ��ݸ�� 003 ��ɽ˳����
        /// </summary>
        internal string YBType = "001";
        #endregion

        private void txtIdcard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtpaycardno.Focus();
            }
        }

        private void m_mthShowinfo(string str)
        {
            this.lblInfo.Caption = "" + str;
        }

        private void btnYb_Click(object sender, EventArgs e)
        {
            if (this.m_Invoice.Count > 1)
            {
                MessageBox.Show("ҽ������ǰ���ַܷ�Ʊ�������½��㡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            string strDBFile = string.Empty;
            bool b = objfrm.m_blnCreateDBFData(ref strDBFile);
            if (!b)
            {
                return;
            }
            else
            {
                frmOPSB frm = new frmOPSB(strDBFile);
                frm.m_Setform(objfrm);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    objfrm.m_mthSetYBValue(frm.m_strChargeNo, frm.m_decYBSum, frm.m_decTotalSum);
                    this.YBHint = false;
                    this.DialogResult = DialogResult.Retry;
                }
            }

        }

        internal bool YBHint = true;
        private void txtFactPay_Enter(object sender, EventArgs e)
        {
            if (this.btnYb.Enabled && YBHint)
            {
                objfrm.m_mthYBHint(this.btnYb, "    ����ʹ��ҽ������!!!");
                YBHint = false;
                //this.objfrm.m_objOPChargeRight.label3.Text = "����ʹ��ҽ������";
                //this.objfrm.m_objOPChargeRight.label3.Text = "�뽻�� " + this.lbeSelfPay.Text.Trim() + " Ԫ";
            }
        }

        private void btnNewYb_Click(object sender, EventArgs e)
        {
            if (this.m_Invoice.Count > 1)
            {
                MessageBox.Show("ҽ������ǰ���ַܷ�Ʊ�������½��㡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (string.IsNullOrEmpty(this.objfrm.txtLoadRecipeNO.Text))
            {
                MessageBox.Show("�����Ų���Ϊ�գ����顣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            bool b = objfrm.m_blnNewYbInterface();
            if (!b)
            {
                return;
            }
            else
            {
                //��ݸ��ɽǶ��ʽ��ҽ���ӿ�
                //need modify ��ɸ�ֵ ��Ҫ����HISYB_CS.dll
                frmYBChargeMZ frmDgcsyb = new frmYBChargeMZ();
                frmDgcsyb.strRecipeID = this.objfrm.txtLoadRecipeNO.Text;//����id
                frmDgcsyb.strEmpNo = this.objfrm.LoginInfo.m_strEmpNo;//��ǰ��¼Ա����
                frmDgcsyb.strInvNO = this.objfrm.m_txtInvoiceNO.Text;//��Ʊ��m_strInvoiceNO
                frmDgcsyb.strPatientCardNo = this.objfrm.m_PatientBasicInfo.txtCardID.Text;
                frmDgcsyb.strPatientName = this.objfrm.m_PatientBasicInfo.txtPatient.Text;
                frmDgcsyb.strIDCardNo = this.objfrm.txtIDcard.Text;
                frmDgcsyb.IsBirthInsurance = (this.objfrm.m_PatientBasicInfo.PayTypeID == this.objfrm.BirthInsuranceCode ? true : false);
                this.objfrm.RowNum = -1;
                this.objfrm.YBnewFlag = -1;
                #region ������ϸ��ֵclsDGMzxmcs_VO
                int intRowCount = this.objfrm.ctlDataGrid1.RowCount;
                clsDGMzxmcs_VO vo = null;
                int intSortno = 1;
                decimal delRatio = 0;
                string regDate = DateTime.Now.ToString("yyyy-MM-dd");
                DataRow[] drr = null;
                DataTable dtRegNo = null;
                for (int i = 0; i < intRowCount; i++)
                {
                    if (string.IsNullOrEmpty(this.objfrm.ctlDataGrid1[i, 2].ToString()))
                    {
                        continue;
                    }
                    if (Convert.ToDecimal(this.objfrm.ctlDataGrid1[i, 1]) < 0)//������С��0���ϴ�
                    {
                        frmDgcsyb.m_decCZF += Convert.ToDecimal(this.objfrm.ctlDataGrid1[i, 7].ToString());//������С��0���ϴ�    
                        this.objfrm.RowNum = i;
                        this.objfrm.YBnewFlag = 1;
                        continue;
                    }
                    //need modify ��ֵ��ע����Щ�ֶ��г�������
                    vo = new clsDGMzxmcs_VO();
                    vo.ZYH = this.objfrm.m_PatientBasicInfo.PatientCardID;//�Һű��
                    vo.CFH = this.objfrm.txtLoadRecipeNO.Text;//������ =strRecipeID;
                    vo.GMSFHM = this.objfrm.txtIDcard.Text;//���֤��
                    vo.JZLB = "";//������� = this.m_objViewer.lbljzlb.Tag.ToString()
                    vo.FYRQ = this.objfrm.txtLoadRecipeNO.Text.Substring(0, 8);//�������� = �������� yyyyMMdd
                    intSortno = intSortno + i;
                    vo.XMXH = intSortno.ToString().PadLeft(6, '0');//ͬһ���������У���Ψһ��ʶһ������=�����е���ϸ��id����Ҫ��ȡ12λ
                    vo.XMBH = this.objfrm.ctlDataGrid1[i, 0].ToString();//this.objfrm.ctlDataGrid1[i, 10].ToString(); //ҽԺ��Ŀ���=ҽԺ���û����ҽ����Ŀ���գ��˴�Ӧ�ô�ҽ�����룬���Կ����ȴ�ҽ�����뿴Ч��
                    vo.XMMC = this.objfrm.ctlDataGrid1[i, 2].ToString();//��Ŀ����
                    if (this.m_blnDiffOn)
                    {
                        vo.JG = m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 38].ToString());//�������� NUMBER	(12,4)
                        if (vo.JG == 0)
                            vo.JG = m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 6].ToString());//���� NUMBER	(12,4)
                    }
                    else
                    {
                        vo.JG = m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 6].ToString());//���� NUMBER	(12,4)
                    }
                    // 2019-10-24
                    decimal zyfs = this.objfrm.numericUpDown1.Value;
                    string invoCateName = this.objfrm.ctlDataGrid1[i, 3].ToString();    // ��Ʊ����
                    if (zyfs <= 0) zyfs = 1;
                    if (!string.IsNullOrEmpty(invoCateName) && invoCateName.Contains("�в�ҩ") && zyfs > 1)
                    {
                        vo.MCYL = m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 1].ToString()) * zyfs;
                    }
                    else
                    {
                        vo.MCYL = m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 1].ToString());    //���� NUMBER	(8,2)
                    }
                    if (!this.m_blnDiffOn)
                        vo.JE = m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 7]);//��� NUMBER	(12,2) ������¼���ܷ��ý��
                    else
                        vo.JE = m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 7]) - Math.Abs(clsPublic.Round(m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 37]), 2));//��� NUMBER	(12,2) ������¼���ܷ��ý��

                    #region 2016.05.30 �籣���߹Һű���¶���: ͬһ�ξ��һ��Ϊͬһ���ҡ�ͬһ��ϡ��������Ķ�ν��㣨��顢���顢��ҩ��������ͬһ��Ψһ��
                    // ��ʱ��ͬһ�졢ͬһ���ҡ�ͬһҽ��
                    //string regNo = DateTime.Now.ToString("yyMMddHHmm");
                    //string deptId = this.objfrm.m_PatientBasicInfo.CurrentDeptID;
                    //if (string.IsNullOrEmpty(deptId))
                    //    deptId = this.objfrm.m_PatientBasicInfo.DeptID;
                    //string doctId = this.objfrm.m_PatientBasicInfo.CurrentDoctorID;
                    //if (string.IsNullOrEmpty(doctId))
                    //    doctId = this.objfrm.m_PatientBasicInfo.DoctorID;
                    //clsDcl_OPCharge opSvc = new clsDcl_OPCharge();
                    //if (dtRegNo == null)
                    //{
                    //    dtRegNo = opSvc.GetOpRegNo(regDate, deptId, doctId, this.objfrm.m_PatientBasicInfo.PatientID);
                    //    if (dtRegNo != null && dtRegNo.Rows.Count > 0)
                    //    {
                    //        regNo = dtRegNo.Rows[0]["regNo"].ToString();
                    //    }
                    //    else
                    //    {
                    //        regNo = regNo + this.objfrm.m_PatientBasicInfo.PatientID.PadLeft(10, '0');
                    //        opSvc.SaveOpRegNo(regNo, regDate, this.objfrm.m_PatientBasicInfo.PatientID, deptId, doctId, "/");
                    //    }
                    //    objDgmzxmVo.ZYH = regNo;
                    //}
                    //else
                    //{
                    //    drr = dtRegNo.Select("regDate = '" + regDate + "' and deptId = '" + deptId + "' and doctId = '" + doctId + "'");
                    //    if (drr != null && drr.Length > 0)
                    //    {
                    //        regNo = drr[0]["regNo"].ToString();
                    //    }
                    //    else
                    //    {
                    //        dtRegNo = opSvc.GetOpRegNo(regDate, deptId, doctId, this.objfrm.m_PatientBasicInfo.PatientID);
                    //        if (dtRegNo != null && dtRegNo.Rows.Count > 0)
                    //        {
                    //            regNo = dtRegNo.Rows[0]["regNo"].ToString();
                    //        }
                    //        else
                    //        {
                    //            regNo = regNo + this.objfrm.m_PatientBasicInfo.PatientID.PadLeft(10, '0');

                    //            opSvc.SaveOpRegNo(regNo, regDate, this.objfrm.m_PatientBasicInfo.PatientID, deptId, doctId, "/");
                    //        }
                    //    }
                    //    objDgmzxmVo.ZYH = regNo;
                    //}
                    //opSvc = null;
                    #endregion

                    delRatio = m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 14].ToString()) / 100;
                    vo.ZFBL = delRatio;  //�Էѱ�����NUMBER	(5,2) ҽ��Ӧ�ò��ᰴ����������㣬�ɴ�0
                    vo.YSGH = this.objfrm.m_PatientBasicInfo.DoctorNo;//ҽ������=����ҽ��
                    vo.BZ = "";//��ע���ĵ���ûע����ʲô������
                    vo.FHXZBZ = this.objfrm.ctlDataGrid1[i, this.objfrm.DeptMedIdx].ToString();     // �������Ʊ�־ �ֵ���: 2 ����; 3 ������

                    // �����ж� 2019-10-24  ( ��ͯ�۸����...)
                    if (vo.MCYL == 1)
                    {
                        if (vo.JE - vo.JG > 10)
                        {
                            vo.JG = vo.JE;
                        }
                    }
                    else if (vo.MCYL > 1)
                    {
                        decimal tmpPrice = weCare.Core.Utils.Function.Round(vo.JE / vo.MCYL, 2);
                        if (tmpPrice - vo.JG > 10)
                        {
                            vo.JG = tmpPrice;
                        }
                    }
                    //�����쳣�������ͽ�����ʾ����
                    //1�����ۻ�����Ϊ0����Ϊ0��
                    //2���ܽ�� / �����ľ���ֵ �뵥�۵ľ���ֵ�����10Ԫ���ϣ�
                    //3�����ü���ʱ�����������Էѽ������ܽ�
                    if ((vo.JG == 0 || vo.MCYL == 0) && vo.JE != 0)
                    {
                        continue;
                    }

                    frmDgcsyb.lstDGMzxmcsVo.Add(vo);
                }
                #endregion
                frmDgcsyb.decTotal = m_mthConvertObjToDecimal(this.lbeSumMoney.Text) - frmDgcsyb.m_decCZF;//�ܷ��ü�ȥ�����ѣ����ϴ������ѣ���ô�ܷ���Ҳ��Ҫ��ȥ�����ѣ�
                if (frmDgcsyb.ShowDialog() == DialogResult.OK)
                {
                    //������� ���˽�� �����ܽ�ҽ�����صģ�
                    objfrm.m_mthSetYBValue(frmDgcsyb.strSDYWH, frmDgcsyb.decAcc, frmDgcsyb.decYBTotal, frmDgcsyb.m_decBCYLTCZF1, frmDgcsyb.m_decBCYLTCZF2, frmDgcsyb.m_decBCYLTCZF3, frmDgcsyb.m_decQTZHIFU, frmDgcsyb.m_decYBJZFPJE);
                    this.YBHint = false;
                    this.DialogResult = DialogResult.Retry;
                }
            }
        }

        #region ת��������
        private decimal m_mthConvertObjToDecimal(object obj)
        {
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToDecimal(obj.ToString());

            }
            else
            {
                return 0;
            }
        }
        private decimal m_mthConvertObjToDecimal(string str)
        {
            try
            {
                return Convert.ToDecimal(str.Trim());
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        private void btnGS_Click(object sender, EventArgs e)
        {
            if (this.m_Invoice.Count > 1)
            {
                MessageBox.Show("ҽ������ǰ���ַܷ�Ʊ�������½��㡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (string.IsNullOrEmpty(this.objfrm.txtLoadRecipeNO.Text))
            {
                MessageBox.Show("�����Ų���Ϊ�գ����顣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            bool b = objfrm.m_blnNewYbInterface();
            if (!b)
            {
                return;
            }
            else
            {
                #region �㶫ʡ��������

                frmSgsMZSF frm = new frmSgsMZSF();
                frm.loginVo = new GSSB.EntitySGS_Login();
                frm.lstRecipeItem = new List<GSSB.EntitySGS_RecipeItem>();
                // ��¼��Ϣ
                frm.loginVo.empNo = this.objfrm.LoginInfo.m_strEmpNo;
                frm.loginVo.empName = this.objfrm.LoginInfo.m_strEmpName;
                frm.loginVo.recipeNo = this.objfrm.txtLoadRecipeNO.Text;
                frm.loginVo.invoNo = this.objfrm.m_txtInvoiceNO.Text;
                frm.loginVo.patientId = this.objfrm.m_PatientBasicInfo.PatientID;
                frm.loginVo.idCardNo = this.objfrm.txtIDcard.Text;
                frm.loginVo.cardNo = this.objfrm.m_PatientBasicInfo.txtCardID.Text;
                frm.loginVo.payTypeId = this.objfrm.m_PatientBasicInfo.PayTypeID;
                frm.loginVo.doctId = this.objfrm.m_PatientBasicInfo.DoctorID;
                frm.loginVo.doctNo = this.objfrm.m_PatientBasicInfo.DoctorNo;
                frm.loginVo.doctName = this.objfrm.m_PatientBasicInfo.DoctorName;
                // ������Ŀ                
                GSSB.EntitySGS_RecipeItem vo = null;
                string feeDate = DateTime.Now.ToString("yyyyMMdd");
                int rowCount = this.objfrm.ctlDataGrid1.RowCount;
                for (int i = 0; i < rowCount; i++)
                {
                    if (string.IsNullOrEmpty(this.objfrm.ctlDataGrid1[i, 2].ToString()))
                    {
                        continue;
                    }
                    if (Convert.ToDecimal(this.objfrm.ctlDataGrid1[i, 1]) < 0)//������С��0���ϴ�
                    {
                        continue;
                    }
                    // ��ֵ��ע����Щ�ֶ��г�������
                    vo = new GSSB.EntitySGS_RecipeItem();
                    vo.recipeNo = this.objfrm.txtLoadRecipeNO.Text;     // ������
                    vo.itemCode = this.objfrm.ctlDataGrid1[i, 0].ToString();
                    vo.itemName = this.objfrm.ctlDataGrid1[i, 2].ToString();
                    vo.dosageForm = "";
                    vo.vender = "";
                    vo.spec = this.objfrm.ctlDataGrid1[i, 4].ToString();
                    vo.feeDate = feeDate;
                    vo.unit = this.objfrm.ctlDataGrid1[i, 5].ToString();
                    vo.price = m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 38].ToString());        // �������� NUMBER	(12,4)
                    if (vo.price == 0)
                        vo.price = m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 6].ToString());     // ���� NUMBER	(12,4)

                    decimal zyfs = this.objfrm.numericUpDown1.Value;
                    string invoCateName = this.objfrm.ctlDataGrid1[i, 3].ToString();    // ��Ʊ����
                    if (zyfs <= 0) zyfs = 1;
                    if (!string.IsNullOrEmpty(invoCateName) && invoCateName.Contains("�в�ҩ") && zyfs > 1)
                    {
                        vo.amount = m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 1].ToString()) * zyfs;
                    }
                    else
                    {
                        vo.amount = m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 1].ToString());    // ���� NUMBER	(8,2)
                    }
                    vo.total = m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 7]) - Math.Abs(clsPublic.Round(m_mthConvertObjToDecimal(this.objfrm.ctlDataGrid1[i, 37]), 2));     // ��� NUMBER	(12,2)
                    vo.doctNo = frm.loginVo.doctNo;
                    vo.doctName = frm.loginVo.doctName;
                    vo.feeId = Convert.ToString(i + 1).PadLeft(6, '0');
                    vo.limitFlag = 0;

                    // �����ж� ( ��ͯ�۸����...)
                    if (vo.amount == 1)
                    {
                        if (vo.total - vo.price > 10)
                        {
                            vo.price = vo.total;
                        }
                    }
                    else if (vo.amount > 1)
                    {
                        decimal tmpPrice = weCare.Core.Utils.Function.Round(vo.total / vo.amount, 2);
                        if (tmpPrice - vo.price > 10)
                        {
                            vo.price = tmpPrice;
                        }
                    }
                    //�����쳣�������ͽ�����ʾ����
                    //1�����ۻ�����Ϊ0����Ϊ0��
                    //2���ܽ�� / �����ľ���ֵ �뵥�۵ľ���ֵ�����10Ԫ���ϣ�
                    //3�����ü���ʱ�����������Էѽ������ܽ�
                    if ((vo.price == 0 || vo.amount == 0) && vo.total != 0)
                    {
                        continue;
                    }
                    frm.lstRecipeItem.Add(vo);
                }
                frm.ShowDialog();

                #endregion

            }
        }
    }
}
