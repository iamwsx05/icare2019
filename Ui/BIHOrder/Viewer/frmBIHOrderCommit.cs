using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using com.digitalwave.iCare.BIHOrder.Control;
using weCare.Core.Entity;
using com.digitalwave.iCare.gui.LIS;
using com.digitalwave.iCare.gui.HIS;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// ҽ���ύ	
    /// </summary>
    public class frmBIHOrderCommit : System.Windows.Forms.Form, iLoginInfo
    {
        #region �ؼ���������

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private PinkieControls.ButtonXP m_cmdCommit;
        private PinkieControls.ButtonXP m_cmdCancel;
        private System.Windows.Forms.Label m_lblDoctor;
        private PinkieControls.ButtonXP m_cmdFind;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;
        /// <summary>
        /// �ύ��رմ���
        /// </summary>
        public bool m_blnCloseAfterCommit = false;
        private const string c_strOkChar = "��";
        public clsBIHDoctor m_objCurrentDoctor;
        clsCommitOrder[] m_objCommitOrderArr = null;
        clsDcl_CommitOrder m_objManage = null;
        private PinkieControls.ButtonXP m_cmdDelete;
        /// <summary>
        /// ��ǰҽ��ID
        /// </summary>
        string m_strCurOrderID = "";
        private clsDcl_InputOrder m_objInputOrder;
        internal System.Windows.Forms.ListView m_lsvSelectBed;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        internal System.Windows.Forms.TextBox m_txtBed;
        internal com.digitalwave.controls.ctlFindTextBox m_txtArea;
        private System.Windows.Forms.Splitter splitter1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private Panel panel2;
        private PinkieControls.ButtonXP buttonXP3;
        private PinkieControls.ButtonXP buttonXP2;
        private PinkieControls.ButtonXP buttonXP1;
        public DataGridView m_dtvOrder;
        public DataGridView m_dtvOrderdicCharge;
        private DataGridViewTextBoxColumn CanCommit;
        private DataGridViewTextBoxColumn NO;
        private DataGridViewTextBoxColumn BedName;
        private DataGridViewTextBoxColumn PatientName;
        private DataGridViewTextBoxColumn RecipeNo;
        private DataGridViewTextBoxColumn ExecuteType;
        private DataGridViewTextBoxColumn Name;
        private DataGridViewTextBoxColumn DosageUnit;
        private DataGridViewTextBoxColumn Use;
        private DataGridViewTextBoxColumn Get;
        private DataGridViewTextBoxColumn Freq;
        private DataGridViewTextBoxColumn UseType;
        private DataGridViewTextBoxColumn Creator;
        private DataGridViewTextBoxColumn CreateDate;
        private DataGridViewTextBoxColumn OrderID;
        private DataGridViewTextBoxColumn RegisterID;
        private DataGridViewTextBoxColumn NO2;
        private DataGridViewTextBoxColumn ItemName;
        private DataGridViewTextBoxColumn IsChiefItem;
        private DataGridViewTextBoxColumn MinPrice;
        private DataGridViewTextBoxColumn SumNumber;
        private DataGridViewTextBoxColumn SumMoney;
        private DataGridViewTextBoxColumn CONTINUEUSETYPE_INT;
        private DataGridViewTextBoxColumn excuteDept;
        private DataGridViewTextBoxColumn seq_int;
        private DataGridViewTextBoxColumn m_intType;
        //public clsBIHOrderService m_objService;
        #endregion

        #region ���������ؿ���
        /// <summary>
        /// 4006����Ϊ8��������м��飨��Ʊ����Ϊ���飩�շ���Ŀ>8��ʱ���ô��۹���
        /// </summary>
        public int m_intLisDiscountNum = 0;
        /// <summary>
        /// 4007�������ô��۹���ʱ�������շ���Ŀ�Ĵ��۱�����80��������
        /// </summary>
        public decimal m_decLisDiscountMount = 0;
        /// <summary>
        /// 4008  0-false������ 1-true �������
        /// </summary>
        public bool m_blLisDiscount = false;
        /// <summary>
        /// ϵͳ������(ICARE����) 0013 ������ϴ��۷�Ʊ���� ������������ݸ���
        /// </summary>
        public string m_strLisPARMVALUE_VCHR = "";
        #endregion
        /// <summary>
        /// סԺ�������ñ�VO
        /// </summary>
        public clsSPECORDERCATE m_objSpecateVo;

        //1067 ҽ��¼��ʱ���ҽ���Ƿ������д���뵥
        /// <summary>
        /// 1067 ҽ��¼��ʱ���ҽ���Ƿ������д���뵥 true �� false ��
        /// </summary>
        internal bool blnFillApplyBill = false;

        // ������ҩ�����ַ
        static string DrugServiceUrl { get; set; }
        // �Ƿ�ʹ�ú�����ҩ�ӿ�
        static bool IsUseMedItf { get; set; }

        #region ���캯��
        public frmBIHOrderCommit()
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
            m_objManage = new clsDcl_CommitOrder();
            //m_dtgOrder.m_mthAppendNotVisibleColumn("OrderID",typeof(string));
            //m_dtgOrder.m_mthAppendNotVisibleColumn("RegisterID",typeof(string));
            m_objInputOrder = new clsDcl_InputOrder();
            //m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
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

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Comfirm">�ύ�Ƿ���ʾ���ȷ�Ͽ�</param>
        /// <param name="SendLisBill">�Ƿ��ͼ������뵥</param>
        /// <param name="m_htCate">ҽ�����ͱ�</param>
        public frmBIHOrderCommit(bool Comfirm, bool SendLisBill, Hashtable m_htCate, string m_strDiagnose)
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
            m_objManage = new clsDcl_CommitOrder();
            //m_dtgOrder.m_mthAppendNotVisibleColumn("OrderID",typeof(string));
            //m_dtgOrder.m_mthAppendNotVisibleColumn("RegisterID",typeof(string));
            m_objInputOrder = new clsDcl_InputOrder();
            //m_objService = new clsBIHOrderService();
            //m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            m_blComfirm = Comfirm;
            m_blSendLisBill = SendLisBill;
            m_htOrderCate = m_htCate;
            strDiagnose = m_strDiagnose;
            if (m_htOrderCate.Count <= 0)
            {
                clsT_aid_bih_ordercate_VO[] p_objItemArr = null;
                long lngRes = m_objInputOrder.m_lngGetAidOrderCate(out p_objItemArr);
                m_htOrderCate.Clear();
                for (int i = 0; i < p_objItemArr.Length; i++)
                {
                    if (!m_htOrderCate.Contains(p_objItemArr[i].m_strORDERCATEID_CHR))
                    {
                        m_htOrderCate.Add(p_objItemArr[i].m_strORDERCATEID_CHR, p_objItemArr[i]);
                    }
                }
            }
        }
        #endregion

        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cmdCommit = new PinkieControls.ButtonXP();
            this.m_cmdCancel = new PinkieControls.ButtonXP();
            this.m_lblDoctor = new System.Windows.Forms.Label();
            this.m_cmdFind = new PinkieControls.ButtonXP();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_txtArea = new com.digitalwave.controls.ctlFindTextBox();
            this.m_txtBed = new System.Windows.Forms.TextBox();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.label4 = new System.Windows.Forms.Label();
            this.m_lsvSelectBed = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonXP3 = new PinkieControls.ButtonXP();
            this.buttonXP2 = new PinkieControls.ButtonXP();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.m_dtvOrder = new System.Windows.Forms.DataGridView();
            this.CanCommit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BedName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecipeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExecuteType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DosageUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Use = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Get = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Freq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UseType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Creator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegisterID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_dtvOrderdicCharge = new System.Windows.Forms.DataGridView();
            this.NO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsChiefItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SumNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SumMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONTINUEUSETYPE_INT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.excuteDept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seq_int = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_intType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtvOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtvOrderdicCharge)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(245, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 14);
            this.label2.TabIndex = 20;
            this.label2.Text = "����:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 19;
            this.label1.Text = "ѡ����:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_cmdCommit
            // 
            this.m_cmdCommit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdCommit.DefaultScheme = true;
            this.m_cmdCommit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdCommit.Hint = "";
            this.m_cmdCommit.Location = new System.Drawing.Point(696, 11);
            this.m_cmdCommit.Name = "m_cmdCommit";
            this.m_cmdCommit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCommit.Size = new System.Drawing.Size(96, 28);
            this.m_cmdCommit.TabIndex = 20;
            this.m_cmdCommit.Text = "�ύ(F4)";
            this.m_cmdCommit.Click += new System.EventHandler(this.m_cmdCommit_Click);
            // 
            // m_cmdCancel
            // 
            this.m_cmdCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdCancel.DefaultScheme = true;
            this.m_cmdCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdCancel.Hint = "";
            this.m_cmdCancel.Location = new System.Drawing.Point(908, 11);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCancel.Size = new System.Drawing.Size(96, 28);
            this.m_cmdCancel.TabIndex = 25;
            this.m_cmdCancel.Text = "�ر�(Esc)";
            this.m_cmdCancel.Click += new System.EventHandler(this.m_cmdCancel_Click);
            // 
            // m_lblDoctor
            // 
            this.m_lblDoctor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblDoctor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_lblDoctor.Location = new System.Drawing.Point(0, 613);
            this.m_lblDoctor.Name = "m_lblDoctor";
            this.m_lblDoctor.Size = new System.Drawing.Size(1014, 20);
            this.m_lblDoctor.TabIndex = 100;
            // 
            // m_cmdFind
            // 
            this.m_cmdFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdFind.DefaultScheme = true;
            this.m_cmdFind.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdFind.Hint = "";
            this.m_cmdFind.Location = new System.Drawing.Point(588, 11);
            this.m_cmdFind.Name = "m_cmdFind";
            this.m_cmdFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdFind.Size = new System.Drawing.Size(96, 28);
            this.m_cmdFind.TabIndex = 15;
            this.m_cmdFind.Text = "��ѯ(F3)";
            this.m_cmdFind.Click += new System.EventHandler(this.m_cmdFind_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_txtArea);
            this.panel1.Controls.Add(this.m_txtBed);
            this.panel1.Controls.Add(this.m_cmdDelete);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.m_cmdCommit);
            this.panel1.Controls.Add(this.m_cmdCancel);
            this.panel1.Controls.Add(this.m_cmdFind);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1014, 48);
            this.panel1.TabIndex = 1;
            // 
            // m_txtArea
            // 
            this.m_txtArea.Font = new System.Drawing.Font("����", 10.5F);
            this.m_txtArea.Location = new System.Drawing.Point(76, 16);
            this.m_txtArea.Name = "m_txtArea";
            this.m_txtArea.Size = new System.Drawing.Size(168, 23);
            this.m_txtArea.TabIndex = 2;
            this.m_txtArea.m_evtFindItem += new com.digitalwave.controls.EventHandler_OnFindItem(this.m_txtArea_m_evtFindItem);
            this.m_txtArea.m_evtInitListView += new com.digitalwave.controls.EventHandler_InitListView(this.m_txtArea_m_evtInitListView);
            this.m_txtArea.m_evtSelectItem += new com.digitalwave.controls.EventHandler_OnSelectItem(this.m_txtArea_m_evtSelectItem);
            // 
            // m_txtBed
            // 
            this.m_txtBed.BackColor = System.Drawing.Color.LightCyan;
            this.m_txtBed.Location = new System.Drawing.Point(284, 16);
            this.m_txtBed.Name = "m_txtBed";
            this.m_txtBed.ReadOnly = true;
            this.m_txtBed.Size = new System.Drawing.Size(296, 23);
            this.m_txtBed.TabIndex = 1;
            this.m_txtBed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBed_KeyDown);
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(800, 12);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete.Size = new System.Drawing.Size(96, 28);
            this.m_cmdDelete.TabIndex = 20;
            this.m_cmdDelete.Text = "ɾ��(F5)";
            this.m_cmdDelete.Click += new System.EventHandler(this.m_cmdDelete_Click);
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1014, 48);
            this.label4.TabIndex = 43;
            // 
            // m_lsvSelectBed
            // 
            this.m_lsvSelectBed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lsvSelectBed.CheckBoxes = true;
            this.m_lsvSelectBed.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.m_lsvSelectBed.FullRowSelect = true;
            this.m_lsvSelectBed.GridLines = true;
            this.m_lsvSelectBed.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_lsvSelectBed.Location = new System.Drawing.Point(284, 40);
            this.m_lsvSelectBed.Name = "m_lsvSelectBed";
            this.m_lsvSelectBed.Size = new System.Drawing.Size(296, 210);
            this.m_lsvSelectBed.TabIndex = 88;
            this.m_lsvSelectBed.UseCompatibleStateImageBehavior = false;
            this.m_lsvSelectBed.View = System.Windows.Forms.View.Details;
            this.m_lsvSelectBed.Visible = false;
            this.m_lsvSelectBed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvSelectBed_KeyDown);
            this.m_lsvSelectBed.Leave += new System.EventHandler(this.m_lsvSelectBed_Leave);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "�� ��";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "�� ��";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "�� ��";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 492);
            this.splitter1.MinExtra = 200;
            this.splitter1.MinSize = 50;
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1014, 4);
            this.splitter1.TabIndex = 89;
            this.splitter1.TabStop = false;
            this.splitter1.DoubleClick += new System.EventHandler(this.splitter1_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.buttonXP3);
            this.panel2.Controls.Add(this.buttonXP2);
            this.panel2.Controls.Add(this.buttonXP1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 465);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1014, 27);
            this.panel2.TabIndex = 90;
            // 
            // buttonXP3
            // 
            this.buttonXP3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP3.DefaultScheme = true;
            this.buttonXP3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP3.Hint = "";
            this.buttonXP3.Location = new System.Drawing.Point(925, -1);
            this.buttonXP3.Name = "buttonXP3";
            this.buttonXP3.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP3.Size = new System.Drawing.Size(79, 28);
            this.buttonXP3.TabIndex = 23;
            this.buttonXP3.Text = "ɾ ��";
            this.buttonXP3.Click += new System.EventHandler(this.buttonXP3_Click);
            // 
            // buttonXP2
            // 
            this.buttonXP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP2.DefaultScheme = true;
            this.buttonXP2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP2.Hint = "";
            this.buttonXP2.Location = new System.Drawing.Point(837, -1);
            this.buttonXP2.Name = "buttonXP2";
            this.buttonXP2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP2.Size = new System.Drawing.Size(79, 28);
            this.buttonXP2.TabIndex = 22;
            this.buttonXP2.Text = "�� ��";
            this.buttonXP2.Click += new System.EventHandler(this.buttonXP2_Click);
            // 
            // buttonXP1
            // 
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(752, -1);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(79, 28);
            this.buttonXP1.TabIndex = 21;
            this.buttonXP1.Text = "�� ��";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // m_dtvOrder
            // 
            this.m_dtvOrder.AllowUserToAddRows = false;
            this.m_dtvOrder.BackgroundColor = System.Drawing.Color.White;
            this.m_dtvOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dtvOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CanCommit,
            this.NO,
            this.BedName,
            this.PatientName,
            this.RecipeNo,
            this.ExecuteType,
            this.Name,
            this.DosageUnit,
            this.Use,
            this.Get,
            this.Freq,
            this.UseType,
            this.Creator,
            this.CreateDate,
            this.OrderID,
            this.RegisterID});
            this.m_dtvOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dtvOrder.Location = new System.Drawing.Point(0, 48);
            this.m_dtvOrder.MultiSelect = false;
            this.m_dtvOrder.Name = "m_dtvOrder";
            this.m_dtvOrder.ReadOnly = true;
            this.m_dtvOrder.RowHeadersWidth = 15;
            this.m_dtvOrder.RowTemplate.Height = 23;
            this.m_dtvOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_dtvOrder.Size = new System.Drawing.Size(1014, 417);
            this.m_dtvOrder.TabIndex = 101;
            this.m_dtvOrder.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_dtvOrder_CellClick);
            this.m_dtvOrder.CurrentCellChanged += new System.EventHandler(this.m_dtvOrder_CurrentCellChanged);
            // 
            // CanCommit
            // 
            this.CanCommit.HeaderText = "�ύ";
            this.CanCommit.Name = "CanCommit";
            this.CanCommit.ReadOnly = true;
            this.CanCommit.Width = 60;
            // 
            // NO
            // 
            this.NO.FillWeight = 60F;
            this.NO.HeaderText = "���";
            this.NO.Name = "NO";
            this.NO.ReadOnly = true;
            // 
            // BedName
            // 
            this.BedName.HeaderText = "����";
            this.BedName.Name = "BedName";
            this.BedName.ReadOnly = true;
            this.BedName.Width = 60;
            // 
            // PatientName
            // 
            this.PatientName.HeaderText = "��������";
            this.PatientName.Name = "PatientName";
            this.PatientName.ReadOnly = true;
            // 
            // RecipeNo
            // 
            this.RecipeNo.HeaderText = "����";
            this.RecipeNo.Name = "RecipeNo";
            this.RecipeNo.ReadOnly = true;
            this.RecipeNo.Width = 60;
            // 
            // ExecuteType
            // 
            this.ExecuteType.HeaderText = "��/��";
            this.ExecuteType.Name = "ExecuteType";
            this.ExecuteType.ReadOnly = true;
            this.ExecuteType.Width = 70;
            // 
            // Name
            // 
            this.Name.HeaderText = "����";
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            // 
            // DosageUnit
            // 
            this.DosageUnit.HeaderText = "����";
            this.DosageUnit.Name = "DosageUnit";
            this.DosageUnit.ReadOnly = true;
            this.DosageUnit.Width = 60;
            // 
            // Use
            // 
            this.Use.HeaderText = "����";
            this.Use.Name = "Use";
            this.Use.ReadOnly = true;
            this.Use.Width = 60;
            // 
            // Get
            // 
            this.Get.HeaderText = "����";
            this.Get.Name = "Get";
            this.Get.ReadOnly = true;
            this.Get.Width = 60;
            // 
            // Freq
            // 
            this.Freq.HeaderText = "Ƶ��";
            this.Freq.Name = "Freq";
            this.Freq.ReadOnly = true;
            this.Freq.Width = 60;
            // 
            // UseType
            // 
            this.UseType.HeaderText = "�÷�";
            this.UseType.Name = "UseType";
            this.UseType.ReadOnly = true;
            this.UseType.Width = 60;
            // 
            // Creator
            // 
            this.Creator.HeaderText = "������";
            this.Creator.Name = "Creator";
            this.Creator.ReadOnly = true;
            // 
            // CreateDate
            // 
            this.CreateDate.HeaderText = "����ʱ��";
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.ReadOnly = true;
            // 
            // OrderID
            // 
            this.OrderID.HeaderText = "OrderID";
            this.OrderID.Name = "OrderID";
            this.OrderID.ReadOnly = true;
            this.OrderID.Visible = false;
            // 
            // RegisterID
            // 
            this.RegisterID.HeaderText = "RegisterID";
            this.RegisterID.Name = "RegisterID";
            this.RegisterID.ReadOnly = true;
            this.RegisterID.Visible = false;
            // 
            // m_dtvOrderdicCharge
            // 
            this.m_dtvOrderdicCharge.AllowUserToAddRows = false;
            this.m_dtvOrderdicCharge.BackgroundColor = System.Drawing.Color.White;
            this.m_dtvOrderdicCharge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dtvOrderdicCharge.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NO2,
            this.ItemName,
            this.IsChiefItem,
            this.MinPrice,
            this.SumNumber,
            this.SumMoney,
            this.CONTINUEUSETYPE_INT,
            this.excuteDept,
            this.seq_int,
            this.m_intType});
            this.m_dtvOrderdicCharge.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_dtvOrderdicCharge.Location = new System.Drawing.Point(0, 496);
            this.m_dtvOrderdicCharge.MultiSelect = false;
            this.m_dtvOrderdicCharge.Name = "m_dtvOrderdicCharge";
            this.m_dtvOrderdicCharge.ReadOnly = true;
            this.m_dtvOrderdicCharge.RowHeadersWidth = 15;
            this.m_dtvOrderdicCharge.RowTemplate.Height = 23;
            this.m_dtvOrderdicCharge.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_dtvOrderdicCharge.Size = new System.Drawing.Size(1014, 117);
            this.m_dtvOrderdicCharge.TabIndex = 102;
            this.m_dtvOrderdicCharge.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_dtvOrderdicCharge_CellDoubleClick);
            // 
            // NO2
            // 
            this.NO2.HeaderText = "���";
            this.NO2.Name = "NO2";
            this.NO2.ReadOnly = true;
            this.NO2.Width = 60;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "�շ���Ŀ";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 230;
            // 
            // IsChiefItem
            // 
            this.IsChiefItem.HeaderText = "��������";
            this.IsChiefItem.Name = "IsChiefItem";
            this.IsChiefItem.ReadOnly = true;
            // 
            // MinPrice
            // 
            this.MinPrice.HeaderText = "����";
            this.MinPrice.Name = "MinPrice";
            this.MinPrice.ReadOnly = true;
            // 
            // SumNumber
            // 
            this.SumNumber.HeaderText = "����";
            this.SumNumber.Name = "SumNumber";
            this.SumNumber.ReadOnly = true;
            // 
            // SumMoney
            // 
            this.SumMoney.HeaderText = "���";
            this.SumMoney.Name = "SumMoney";
            this.SumMoney.ReadOnly = true;
            // 
            // CONTINUEUSETYPE_INT
            // 
            this.CONTINUEUSETYPE_INT.HeaderText = "��������";
            this.CONTINUEUSETYPE_INT.Name = "CONTINUEUSETYPE_INT";
            this.CONTINUEUSETYPE_INT.ReadOnly = true;
            // 
            // excuteDept
            // 
            this.excuteDept.HeaderText = "ִ�п���";
            this.excuteDept.Name = "excuteDept";
            this.excuteDept.ReadOnly = true;
            this.excuteDept.Width = 200;
            // 
            // seq_int
            // 
            this.seq_int.HeaderText = "��ˮ��";
            this.seq_int.Name = "seq_int";
            this.seq_int.ReadOnly = true;
            this.seq_int.Visible = false;
            // 
            // m_intType
            // 
            this.m_intType.HeaderText = "�շ����";
            this.m_intType.Name = "m_intType";
            this.m_intType.ReadOnly = true;
            this.m_intType.Visible = false;
            // 
            // frmBIHOrderCommit
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1014, 633);
            this.Controls.Add(this.m_lsvSelectBed);
            this.Controls.Add(this.m_dtvOrder);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_dtvOrderdicCharge);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_lblDoctor);
            this.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.MaximizeBox = false;

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ҽ���ύ";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBIHOrderCommit_KeyDown);
            this.Load += new System.EventHandler(this.frmBIHOrderCommit_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtvOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtvOrderdicCharge)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region �Զ������
        /// <summary>
        /// �ύʱ�Ƿ���Ҫ�����ʾ
        /// </summary>
        private bool m_blComfirm = false;
        /// <summary>
        /// �ύʱ�Ƿ���Ҫ�����ʾ �������Ҫ�� ֵΪ2 ���������޸Ĺ��ŵ��ύȷ�ϴ���
        /// </summary>
        internal int m_intComfirm = -1;
        /// <summary>
        /// �ύʱ�Ƿ�Լ������Ŀ�������뵥����
        /// </summary>
        private bool m_blSendLisBill = false;
        private Hashtable m_htOrderCate = new Hashtable();
        private string strDiagnose = null;
        /// <summary>
        /// ���ͼ������뵥����ҪVO
        /// </summary>
        public clsLisApplMainVO objLisMainVO;
        /// <summary>
        /// ���ͼ������뵥��VO
        /// </summary>
        public clsTestApplyItme_VO[] objLisApplyItmeVOArr;
        /// <summary>
        /// �ύ�ļ���ҽ��ID
        /// </summary>
        public string[] strLisOrderIDArr;
        /// <summary>
        /// ��Ҫ���͵ļ��������������ж����ɼ��鷢������ʧ�ܻ���û�м���
        /// </summary>
        public int intNeedSendCount = 0;

        #endregion
        #region �����¼�
        private void frmBIHOrderCommit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    //����DataGrid�ؼ��˳�����
                    if (MessageBox.Show("�Ƿ�ȷ���˳�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                    {
                        if (ActiveControl.ToString() != m_dtvOrder.ToString())
                        {
                            m_cmdCancel_Click(sender, e);
                        }
                    }
                    break;
                case Keys.F3://��ѯ
                    m_cmdFind_Click(sender, e);
                    break;
                case Keys.F4://�ύ
                    m_cmdCommit_Click(sender, e);
                    break;
                case Keys.F5://ɾ��ҽ��
                    if (m_cmdDelete.Enabled) m_cmdDelete_Click(sender, e);
                    break;
            }
        }
        #endregion
        #region ��ť�¼�
        public void m_cmdCommit_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            m_CommitOrder();
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// �ύҽ��
        /// </summary>
        private void m_CommitOrder()
        {
            if (m_objCurrentDoctor == null)
            {
                MessageBox.Show("�Ҳ�����ǰ�û���Ϣ!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool isGetPat = false;
            Hisitf.EntityDrugUse patVo = new Hisitf.EntityDrugUse();
            Hisitf.EntityDrugUse drugVo = null;
            System.Collections.Generic.List<Hisitf.EntityDrugUse> lstDrug = new System.Collections.Generic.List<Hisitf.EntityDrugUse>();
            bool isSkipLevel = false;   // Խ��ʹ�ÿ�����(��һ��): ���� -> �м� -> ����
            DateTime dtmNow = DateTime.Now;
            if ((Convert.ToDateTime(dtmNow.ToString("yyyy-MM-dd") + " 17:30:00") < dtmNow && dtmNow < Convert.ToDateTime(dtmNow.ToString("yyyy-MM-dd") + " 23:59:59")) ||
                 (Convert.ToDateTime(dtmNow.AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00") < dtmNow && dtmNow < Convert.ToDateTime(dtmNow.AddDays(1).ToString("yyyy-MM-dd") + " 07:59:59")))
            {
                isSkipLevel = true;
            }

            #region ��ȡ��Ҫ�ύ��ҽ��
            ArrayList arlID = new ArrayList();

            clsCommitOrder objItem = new clsCommitOrder();
            //���ύ��ҽ�������б�
            ArrayList CommitArr = new ArrayList();
            for (int i = 0; i < m_dtvOrder.RowCount; i++)
            {
                string strChar = m_dtvOrder["CanCommit", i].Value.ToString().Trim();
                if (strChar == c_strOkChar)
                {
                    string strID = m_dtvOrder["OrderID", i].Value.ToString().Trim();
                    //objItem = m_objCommitOrderArr[i];

                    objItem = (clsCommitOrder)m_dtvOrder.Rows[i].Tag;
                    arlID.Add(strID);
                    objItem.m_strDIAGNOSE_VCHR = strDiagnose;
                    CommitArr.Add(objItem);

                    #region use drug
                    // 01 -- ҩ��; 17 -- ��ҩ
                    if (objItem.m_strOrderDicCateID == "01" || objItem.m_strOrderDicCateID == "17")
                    {
                        drugVo = new Hisitf.EntityDrugUse();
                        drugVo.drug = objItem.m_strOrderDicID;
                        drugVo.drugName = objItem.m_strName;
                        drugVo.specification = objItem.m_strSpec;
                        drugVo.package = objItem.m_dmlPACKQTY_DEC.ToString();
                        drugVo.quantity = objItem.m_dmlGet.ToString();
                        drugVo.packUnit = objItem.m_strGetunit;
                        drugVo.unitPrice = objItem.m_dmlPrice.ToString();
                        drugVo.amount = clsPublic.Round(objItem.m_dmlPrice * objItem.m_dmlGet, 2).ToString();
                        drugVo.groupNo = objItem.m_intRecipenNo.ToString();
                        drugVo.firstUse = "false";   // ?
                        drugVo.prepForm = "";   // ����?
                        drugVo.adminRoute = objItem.m_strDosetypeName;
                        //drugVo.adminArea 
                        drugVo.adminFrequency = objItem.m_strExecFreqName;
                        drugVo.adminDose = objItem.m_dmlDosage.ToString() + objItem.m_strDosageUnit;    // +��λ? 
                        //drugVo.adminMethod
                        if (objItem.m_intExecuteType == 1)
                            drugVo.type1 = "����";
                        else if (objItem.m_intExecuteType == 2)
                            drugVo.type1 = "��ʱ";
                        else if (objItem.m_intExecuteType == 3)
                            drugVo.type1 = "��ҩ";
                        //drugVo.adminGoal
                        drugVo.docID1 = objItem.m_strCreatorID;
                        drugVo.docName1 = objItem.m_strCreator;
                        drugVo.docTitle1 = this.m_objLoginInfo.m_strTechnicalRank;
                        drugVo.departID1 = objItem.m_strCREATEAREA_ID;
                        drugVo.department1 = objItem.m_strCREATEAREA_Name;
                        if (string.IsNullOrEmpty(drugVo.department1)) drugVo.department1 = this.m_objLoginInfo.m_strdepartmentName;
                        drugVo.startTime = objItem.m_dtStartDate.ToString("yyyy-MM-dd HH:mm:ss");
                        drugVo.endTime = objItem.m_dtFinishDate.ToString("yyyy-MM-dd HH:mm:ss");
                        if (drugVo.endTime.StartsWith("0001-01-01")) drugVo.endTime = string.Empty;
                        // ֵ���ڼ�Խ��
                        if (isSkipLevel && !string.IsNullOrEmpty(drugVo.docTitle1))
                        {
                            if (drugVo.docTitle1.Trim() == "סԺҽʦ")
                                drugVo.docTitle1 = "����ҽʦ";
                            else if (drugVo.docTitle1.Trim() == "����ҽʦ")
                                drugVo.docTitle1 = "������ҽʦ";
                            else if (drugVo.docTitle1.Trim() == "������ҽʦ")
                                drugVo.docTitle1 = "����ҽʦ";
                        }

                        lstDrug.Add(drugVo);
                    }
                    #endregion

                    #region pat
                    if (!isGetPat)
                    {
                        isGetPat = true;
                        //patVo.departID
                        //patVo.department
                        patVo.bedNo = objItem.m_strBedID;
                        //patVo.presType
                        patVo.presSource = "סԺ";
                        patVo.presDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        patVo.payType = "";// ? objItem.
                        patVo.patientNo = objItem.m_strPATIENTCARDID_CHR;
                        patVo.presNo = "Z0";
                        patVo.name = objItem.m_strPatientName;
                        patVo.diagnose = objItem.m_strDIAGNOSE_VCHR;
                        //patVo.address 
                        //patVo.IDCard 
                        //patVo.phoneNo 
                        patVo.age = objItem.m_strAge;
                        patVo.sex = (objItem.m_strsex_chr == "��" ? "M" : "F");  // ? M F ��
                        //patVo.height 
                        //patVo.weight
                        //patVo.birthWeight
                        //patVo.ccr
                        //patVo.allergyList 
                        //patVo.pregnancy = "false";
                        //patVo.timeOfPreg = 
                        //patVo.breastFeeding
                        //patVo.dialysis
                        //patVo.proxName
                        //patVo.proxIDCard
                        if (lstDrug.Count > 0)
                        {
                            patVo.docID = lstDrug[lstDrug.Count - 1].docID1;
                            patVo.docName = lstDrug[lstDrug.Count - 1].docName1;
                            patVo.docTitle = lstDrug[lstDrug.Count - 1].docTitle1;
                        }
                        else
                        {
                            patVo.docID = this.LoginInfo.m_strEmpID;
                            patVo.docName = this.LoginInfo.m_strEmpName;
                            patVo.docTitle = this.LoginInfo.m_strTechnicalRank;
                        }

                        // ֵ���ڼ�Խ��
                        if (isSkipLevel && !string.IsNullOrEmpty(patVo.docTitle))
                        {
                            if (patVo.docTitle.Trim() == "סԺҽʦ")
                                patVo.docTitle = "����ҽʦ";
                            else if (patVo.docTitle.Trim() == "����ҽʦ")
                                patVo.docTitle = "������ҽʦ";
                            else if (patVo.docTitle.Trim() == "������ҽʦ")
                                patVo.docTitle = "����ҽʦ";
                        }

                        //patVo.pharmChkId
                        //patVo.pharmDelvId
                        //patVo.pharmChkName
                        //patVo.pharmDelvName
                        //patVo.totalAmount
                        //patVo.incisionType
                        //patVo.scr
                        //patVo.alt
                        //patVo.ast
                        //patVo.bsa
                        patVo.drugSensivity = "false";      // ����
                    }
                    #endregion
                }
            }

            string[] arrID = (string[])(arlID.ToArray(typeof(string)));
            strLisOrderIDArr = arrID;
            if (arrID.Length <= 0)
            {
                MessageBox.Show(this, "û��ѡ�����ύ��ҽ��!\nѡ�񷽷����ڵ�һ��[�ύ]��!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion

            #region ������֤
            //if(!PassCommitRecipeNOValidate(arrID))
            string error = string.Empty;
            int ret = PassCommitRecipeNOValidate(ref error);
            if (ret == -1)
            {
                MessageBox.Show(this, "ͬ���ŵ�ҽ������ͬʱ�ύ!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (ret == -2)
            {
                MessageBox.Show(this, "��ҽ���ġ�Ժ����͡���������ҽ����һ�£������ύ��", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion

            long lngRes = 0;

            #region �ų���֤	2005-01-24��
            /*
			clsDcl_ExecuteOrder objExecuteOrder =new clsDcl_ExecuteOrder();
			string[] strExcludeOrderIDArr,strExcludeOrderNameArr;
			string[] strCanExecuteOrderIDArr =null;
			int intExcludeType =0;
			lngRes =objExecuteOrder.m_lngGetCanExecuteOrderByOrderID(arrID[0],out strCanExecuteOrderIDArr);
			lngRes =objExecuteOrder.m_lngJudgeExcludeOrder(arrID,strCanExecuteOrderIDArr,1,out intExcludeType,out strExcludeOrderIDArr,out strExcludeOrderNameArr);
			if(lngRes>0 && intExcludeType!=0)
			{
				string strMessageBox ="";
				//{0=û�ų⣻1=ȫ�ų���ʱҽ����2=ȫ�ųⳤ��ҽ����3=ȫ�ų��ٳ�ҽ����4=��ͨ�ų⣻}
				switch(intExcludeType)
				{
					case 1:
						strMessageBox +="����ҽ������ȫ�ų�[���ڡ���ʱ]��\r\n";
						break;
					case 2:
						strMessageBox +="����ҽ������ȫ�ų�[����]��\r\n";
						break;
					case 3:
						strMessageBox +="����ҽ������ȫ�ų�[��ʱ]��\r\n";
						break;
					case 4:
						strMessageBox +="����ҽ�������ų⣺\r\n";
						break;
				}
				for(int i1=0;i1<strExcludeOrderNameArr.Length;i1++)
				{
					strMessageBox +=strExcludeOrderNameArr[i1] + "\r\n";					
				}			
				MessageBox.Show(strMessageBox,"����!",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
             */
            #endregion

            #region Ա�������������ʾ
            if (m_blComfirm == true)
            {
                string doctid = "";
                string doctname = "";

                DialogResult dlg = clsPublic.m_dlgConfirm(this.LoginInfo.m_strEmpNo, out doctid, out doctname);
                //DialogResult dlg = clsPublic.m_dlgConfirm(out doctid);
                if (dlg == DialogResult.Yes)
                {
                    m_objCurrentDoctor.m_strDoctorID = doctid;
                    m_objCurrentDoctor.m_strDoctorName = doctname;
                }
                else
                {
                    return;
                }
                //DotorComfirmBox comfirmBox1 = new DotorComfirmBox();
                //comfirmBox1.m_txtName.Text = this.LoginInfo.m_strEmpNo;
                //comfirmBox1.m_txtPassword.Focus();
                //if (comfirmBox1.ShowDialog() == DialogResult.OK)
                //{
                //    m_objCurrentDoctor.m_strDoctorID = comfirmBox1.empid_chr;//���ĵ�ǰ¼����ID
                //    m_objCurrentDoctor.m_strDoctorName = comfirmBox1.lastname_vchr;//���ĵ�ǰ¼��������
                //}
                //else
                //{
                //    return;
                //}

            }
            else if (m_blComfirm == false && m_intComfirm == 2)
            {
                DialogResult dlg = clsPublic.m_dlgConfimByDefault(this.LoginInfo.m_strEmpNo);
                if (dlg == DialogResult.Yes)
                {
                    m_objCurrentDoctor.m_strDoctorID = this.LoginInfo.m_strEmpID;
                    m_objCurrentDoctor.m_strDoctorName = this.LoginInfo.m_strEmpName;
                }
                else
                {
                    return;
                }
            }


            #endregion

            this.Cursor = Cursors.WaitCursor;

            #region ���ɼ������뵥
            if (m_blSendLisBill == true)
            {
                ArrayList m_arrHadSend = new ArrayList();
                ArrayList m_arrAllNeedSend = new ArrayList();
                // ���ɼ������뵥
                ArrayList SendCheckArr = new ArrayList();
                bool isHave = false;
                for (int i = 0; i < CommitArr.Count; i++)
                {
                    isHave = false;

                    if (m_objSpecateVo != null && m_objSpecateVo.m_strORDERCATEID_LIS_CHR.Trim().Equals(((clsCommitOrder)CommitArr[i]).m_strOrderDicCateID))
                    {
                        isHave = true;
                    }
                    if (isHave)
                    {
                        m_arrAllNeedSend.Add(CommitArr[i]);
                    }
                }
                while (true)
                {
                    SendCheckArr.Clear();
                    m_arrHadSend.Clear();
                    intNeedSendCount = m_arrAllNeedSend.Count;
                    for (int i = 0; i < m_arrAllNeedSend.Count; i++)
                    {
                        clsCommitOrder order = (clsCommitOrder)m_arrAllNeedSend[i];
                        SendCheckArr.Add(order);
                        if (!m_arrHadSend.Contains(order.m_strLISAPPLYUNITID_CHR))
                        {
                            m_arrHadSend.Add(order.m_strLISAPPLYUNITID_CHR);
                            m_arrAllNeedSend.Remove(order);
                            i--;

                            // 11.10
                            if (string.IsNullOrEmpty(order.m_strSAMPLEID_VCHR) && string.IsNullOrEmpty(order.m_strSAMPLEName_VCHR))
                            {
                                MessageBox.Show(order.m_strName + ": ��¼�����������", "�����ύ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            SendCheckArr.Remove(order);
                        }
                    }
                    if (SendCheckArr.Count > 0)
                    {
                        ArrayList m_arrLisOrders = null;
                        //Ϊҽ�����õ��ۣ��ϼƽ���
                        SetThePrice(ref SendCheckArr);

                        //���ﲢ�����ͼ��飬ֻ�����ɷ��ͼ�����Ҫ������
                        if (sendTheCheck(ref SendCheckArr, out m_arrLisOrders) == false)
                        {
                            MessageBox.Show("���ͼ��鵥ʧ�ܣ�������ؿ�����ϵ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    if (m_arrAllNeedSend.Count <= 0 || SendCheckArr.Count <= 0)
                    {
                        break;
                    }
                }
            }
            #endregion

            #region drug use
            // ������ҩ�ӿ�
            if (lstDrug.Count > 0)
            {
                // ������ҩ�����ַ
                if (string.IsNullOrEmpty(frmBIHOrderCommit.DrugServiceUrl))
                {
                    frmBIHOrderCommit.DrugServiceUrl = clsPublic.m_strGetSysparm("0080");
                    frmBIHOrderCommit.IsUseMedItf = (clsPublic.ConvertObjToDecimal(clsPublic.m_strGetSysparm("0082")) == 1 ? true : false);
                }
                if (frmBIHOrderCommit.IsUseMedItf)
                {
                    string orderDicID = string.Empty;
                    //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                    foreach (Hisitf.EntityDrugUse item in lstDrug)
                    {
                        orderDicID += "'" + item.drug + "',";
                    }
                    System.Collections.Generic.Dictionary<string, clsMedicine_VO> dicMed = (new weCare.Proxy.ProxyIP()).Service.GetMedInfoByOrderDicId(orderDicID.TrimEnd(','));
                    //svc = null;
                    foreach (Hisitf.EntityDrugUse item in lstDrug)
                    {
                        orderDicID = item.drug;
                        if (dicMed.ContainsKey(orderDicID))
                        {
                            item.drug = dicMed[orderDicID].m_strMedicineID;
                            item.drugName = dicMed[orderDicID].m_strMedicineName;
                        }
                    }
                    using (Hisitf.RationalDrugUseItf itf = new Hisitf.RationalDrugUseItf())
                    {
                        if (itf.CheckDrugUse(3, frmBIHOrderCommit.DrugServiceUrl, patVo, lstDrug) == false) return;
                    }
                }
            }
            #endregion

            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc =
            //                                                                   (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));

            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngPostOrder(arrID, m_objCurrentDoctor.m_strDoctorID, m_objCurrentDoctor.m_strDoctorName, DateTime.Now);

            this.Cursor = Cursors.Default;
            if (lngRes > 0)
            {
                //MessageBox.Show(this,"�ύ���!","ҽ���ύ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                m_dtvOrderdicCharge.Rows.Clear();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(this, "�ύҽ��ʧ��!", "ҽ���ύ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.Cancel;
            }

            if (m_blnCloseAfterCommit)
            {
                this.Close();
            }
            else
            {
                m_cmdFind_Click(null, null);
            }
        }

        internal void SetThePrice(ref ArrayList SendCheckArr)
        {
            ArrayList m_strOrderIds = new ArrayList();
            Hashtable m_htOrders = new Hashtable();
            for (int i = 0; i < SendCheckArr.Count; i++)
            {
                clsCommitOrder order = (clsCommitOrder)SendCheckArr[i];
                if (!m_strOrderIds.Contains(order.m_strOrderID))
                {
                    m_strOrderIds.Add(order.m_strOrderID);

                }
                if (!m_htOrders.Contains(order.m_strOrderID))
                {
                    m_htOrders.Add(order.m_strOrderID, order);
                }
            }
            if (m_strOrderIds.Count <= 0)
            {
                return;
            }
            string[] m_arrOrders = (string[])m_strOrderIds.ToArray(typeof(string));
            DataTable m_dtOrderSign = null;
            m_objManage.m_lngGetOrderLisSign(m_arrOrders, out m_dtOrderSign);
            if (m_dtOrderSign != null && m_dtOrderSign.Rows.Count > 0)
            {
                string m_strOrderID = "", m_OrderID = "";
                int m_intDisCount = 0;
                string InvCateID = "";//���÷�Ʊ���id
                decimal AMOUNT_DEC = 0, UNITPRICE_DEC = 0, m_dmlGet = 0, m_dmlPrice = 0;
                decimal m_decTotalPrice = 0;
                for (int i = 0; i < m_strOrderIds.Count; i++)
                {
                    m_dmlGet = 0;
                    m_dmlPrice = 0;
                    m_intDisCount = 0;
                    m_decTotalPrice = 0;
                    m_strOrderID = (string)m_strOrderIds[i];

                    for (int j = 0; j < m_dtOrderSign.Rows.Count; j++)
                    {

                        m_OrderID = m_dtOrderSign.Rows[j]["orderid_chr"].ToString();
                        if (!m_strOrderID.Equals(m_OrderID))
                        {
                            continue;
                        }
                        InvCateID = m_dtOrderSign.Rows[j]["ItemIpInvType_Chr"].ToString();
                        decimal.TryParse(m_dtOrderSign.Rows[j]["AMOUNT_DEC"].ToString(), out AMOUNT_DEC);
                        decimal.TryParse(m_dtOrderSign.Rows[j]["UNITPRICE_DEC"].ToString(), out UNITPRICE_DEC);
                        if (m_dtOrderSign.Rows[j]["FLAG_INT"].ToString().Trim().Equals("0"))//���շ���Ŀ
                        {
                            m_dmlGet = AMOUNT_DEC;
                            m_dmlPrice = UNITPRICE_DEC;
                        }
                        m_decTotalPrice += AMOUNT_DEC * UNITPRICE_DEC;
                        if (m_blLisDiscount == true && m_strLisPARMVALUE_VCHR.Contains(InvCateID) && !InvCateID.Equals(""))
                        {
                            m_intDisCount++;
                        }
                    }
                    //������۵��߼�
                    clsCommitOrder order = (clsCommitOrder)m_htOrders[m_strOrderID];
                    order.m_decTotalPrice = m_decTotalPrice;
                    order.m_dmlGet = m_dmlGet;
                    order.m_dmlPrice = m_dmlPrice;
                    if (m_intDisCount > m_intLisDiscountNum)
                    {
                        order.m_decDiscount = m_decLisDiscountMount;
                    }
                    else
                    {
                        order.m_decDiscount = 100;
                    }
                }
            }
        }
        private void m_cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }

        public void Find_Order2(string m_strCreatorID, string m_strRegisterID)
        {
            long lngRes = m_objManage.m_lngGetOrderCommitByEmpIDAndRegisterID(m_strCreatorID, m_strRegisterID, out m_objCommitOrderArr);
            if (lngRes > 0 && m_objCommitOrderArr != null && m_objCommitOrderArr.Length > 0)
            {
                DataGridViewRow objRow;
                #region ��ֵ
                for (int i1 = 0; i1 < m_objCommitOrderArr.Length; i1++)
                {
                    if (this.blnFillApplyBill)
                    {
                        //������뵥���
                        if (!m_objCommitOrderArr[i1].m_strAPPLYTYPEID_CHR.Trim().Equals("") && !m_objCommitOrderArr[i1].m_strPARTID_VCHR.Trim().Equals(""))
                        {
                            clsEMR_HIS_CheckRequisitionValue CheckRequisition_VO = null;

                            //com.digitalwave.AssistantToolService.clsEMR_HIS_CheckRequisitionServ objSvc =
                            //                     (com.digitalwave.AssistantToolService.clsEMR_HIS_CheckRequisitionServ)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.AssistantToolService.clsEMR_HIS_CheckRequisitionServ));

                            (new weCare.Proxy.ProxyEmr01()).Service.m_lngGetCheckRequisitionValue(m_objCommitOrderArr[i1].m_strRegisterID, m_objCommitOrderArr[i1].m_strOrderID, out CheckRequisition_VO);
                            if (CheckRequisition_VO == null)
                            {
                                MessageBox.Show(m_objCommitOrderArr[i1].m_strName + ": �Ը���ҽ��������д�����뵥���ݺ��ٴ��ύ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                continue;
                            }
                        }
                    }

                    m_dtvOrder.Rows.Add();
                    objRow = m_dtvOrder.Rows[m_dtvOrder.RowCount - 1];
                    objRow.Cells["OrderID"].Value = m_objCommitOrderArr[i1].m_strOrderID;
                    objRow.Cells["RegisterID"].Value = m_objCommitOrderArr[i1].m_strRegisterID;
                    objRow.Cells["CanCommit"].Value = c_strOkChar;

                    objRow.Cells["NO"].Value = i1 + 1;
                    if (i1 > 0 && m_objCommitOrderArr[i1].m_strRegisterID.Trim() == m_objCommitOrderArr[i1 - 1].m_strRegisterID.Trim())
                    {
                        objRow.Cells["BedName"].Value = "";
                        objRow.Cells["PatientName"].Value = "";

                    }
                    else
                    {
                        objRow.Cells["BedName"].Value = m_objCommitOrderArr[i1].m_strBedName;
                        objRow.Cells["PatientName"].Value = m_objCommitOrderArr[i1].m_strPatientName;
                    }
                    if (m_objCommitOrderArr[i1].m_intExecuteType == 1)
                    {
                        objRow.Cells["ExecuteType"].Value = "��";
                    }
                    else
                    {
                        if (m_objCommitOrderArr[i1].m_intExecuteType == 2)
                            objRow.Cells["ExecuteType"].Value = "��";
                        else
                            objRow.Cells["ExecuteType"].Value = "";
                    }
                    objRow.Cells["RecipeNo"].Value = m_objCommitOrderArr[i1].m_intRecipenNo.ToString();
                    objRow.Cells["Name"].Value = m_objCommitOrderArr[i1].m_strName;
                    if (m_objCommitOrderArr[i1].m_dmlDosage > 0)
                    {
                        objRow.Cells["DosageUnit"].Value = m_objCommitOrderArr[i1].m_dmlDosage.ToString() + " " + m_objCommitOrderArr[i1].m_strDosageUnit;
                    }
                    else
                    {
                        objRow.Cells["DosageUnit"].Value = "";
                    }
                    if (m_objCommitOrderArr[i1].m_dmlUse > 0)
                    {
                        objRow.Cells["Use"].Value = m_objCommitOrderArr[i1].m_dmlUse.ToString() + " " + m_objCommitOrderArr[i1].m_strUseunit;
                    }
                    else
                    {
                        objRow.Cells["Use"].Value = "";
                    }
                    if (m_objCommitOrderArr[i1].m_dmlGet > 0)
                    {
                        objRow.Cells["Get"].Value = m_objCommitOrderArr[i1].m_dmlGet.ToString() + " " + m_objCommitOrderArr[i1].m_strGetunit;
                    }
                    else
                    {
                        objRow.Cells["Get"].Value = "";
                    }
                    objRow.Cells["Freq"].Value = m_objCommitOrderArr[i1].m_strExecFreqName;
                    objRow.Cells["UseType"].Value = m_objCommitOrderArr[i1].m_strDosetypeName;
                    objRow.Cells["Creator"].Value = m_objCommitOrderArr[i1].m_strCreator;
                    objRow.Cells["CreateDate"].Value = DateTimeToString(m_objCommitOrderArr[i1].m_dtCreatedate);

                    objRow.Tag = m_objCommitOrderArr[i1];

                }
                #endregion
            }
        }

        public void m_cmdFind_Click(object sender, System.EventArgs e)
        {
            m_cmdFindCommitOrder();
        }
        public void m_cmdFindCommitOrder()
        {
            //m_dtOrderdicCharge.m_mthDeleteAllRow();
            //m_dtgOrder.BeginUpdate();
            //m_dtgOrder.m_mthDeleteAllRow();
            //m_dtgOrder.m_mthFormatReset();
            m_dtvOrder.Rows.Clear();
            #region ��ѯ����
            string strAreaID = "", strBedIDs = "";
            if (m_txtArea.Tag != null && m_txtArea.Tag.ToString().Trim() != "" && m_txtArea.Text.Trim() != "")
            {
                strAreaID = clsConverter.ToString(m_txtArea.Tag).Trim();
                strBedIDs = m_BedIDs;
            }
            else
            {
                m_txtArea.Text = "";
                m_txtArea.Tag = "";
                m_BedIDs = "";
            }
            if (strAreaID.Trim() == "")
            {
                MessageBox.Show("��������ѡ��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_txtArea.Focus();
                m_txtArea.SelectAll();
                return;
            }
            #endregion
            //long lngRes =m_objManage.m_lngGetOrderCommit(strAreaID,strBedIDs,out m_objCommitOrderArr);
            string Empid = this.LoginInfo.m_strEmpID;
            long lngRes = m_objManage.m_lngGetOrderCommitByEmpID(Empid, strAreaID, strBedIDs, out m_objCommitOrderArr);
            if (lngRes > 0 && m_objCommitOrderArr != null && m_objCommitOrderArr.Length > 0)
            {
                //DataRow objRow;
                DataGridViewRow objRow;
                #region ��ֵ
                for (int i1 = 0; i1 < m_objCommitOrderArr.Length; i1++)
                {
                    m_dtvOrder.Rows.Add();
                    objRow = m_dtvOrder.Rows[m_dtvOrder.RowCount - 1];
                    objRow.Cells["OrderID"].Value = m_objCommitOrderArr[i1].m_strOrderID;
                    objRow.Cells["RegisterID"].Value = m_objCommitOrderArr[i1].m_strRegisterID;
                    objRow.Cells["CanCommit"].Value = c_strOkChar;

                    objRow.Cells["NO"].Value = i1 + 1;
                    if (i1 > 0 && m_objCommitOrderArr[i1].m_strRegisterID.Trim() == m_objCommitOrderArr[i1 - 1].m_strRegisterID.Trim())
                    {
                        objRow.Cells["BedName"].Value = "";
                        objRow.Cells["PatientName"].Value = "";
                        //						if(i1>0 && m_objCommitOrderArr[i1].m_intRecipenNo==m_objCommitOrderArr[i1-1].m_intRecipenNo)
                        //						{
                        //							objRow["RecipeNo"] ="";
                        //						}
                        //						else
                        //						{
                        //							objRow["RecipeNo"]=m_objCommitOrderArr[i1].m_intRecipenNo.ToString();						
                        //						}
                    }
                    else
                    {
                        objRow.Cells["BedName"].Value = m_objCommitOrderArr[i1].m_strBedName;
                        objRow.Cells["PatientName"].Value = m_objCommitOrderArr[i1].m_strPatientName;
                        //						objRow["RecipeNo"]=m_objCommitOrderArr[i1].m_intRecipenNo.ToString();
                    }
                    if (m_objCommitOrderArr[i1].m_intExecuteType == 1)
                    {
                        objRow.Cells["ExecuteType"].Value = "��";
                    }
                    else
                    {
                        if (m_objCommitOrderArr[i1].m_intExecuteType == 2)
                            objRow.Cells["ExecuteType"].Value = "��";
                        else
                            objRow.Cells["ExecuteType"].Value = "";
                    }
                    objRow.Cells["RecipeNo"].Value = m_objCommitOrderArr[i1].m_intRecipenNo.ToString();
                    objRow.Cells["Name"].Value = m_objCommitOrderArr[i1].m_strName;
                    if (m_objCommitOrderArr[i1].m_dmlDosage > 0)
                    {
                        objRow.Cells["DosageUnit"].Value = m_objCommitOrderArr[i1].m_dmlDosage.ToString() + " " + m_objCommitOrderArr[i1].m_strDosageUnit;
                    }
                    else
                    {
                        objRow.Cells["DosageUnit"].Value = "";
                    }
                    if (m_objCommitOrderArr[i1].m_dmlUse > 0)
                    {
                        objRow.Cells["Use"].Value = m_objCommitOrderArr[i1].m_dmlUse.ToString() + " " + m_objCommitOrderArr[i1].m_strUseunit;
                    }
                    else
                    {
                        objRow.Cells["Use"].Value = "";
                    }
                    if (m_objCommitOrderArr[i1].m_dmlGet > 0)
                    {
                        /* update by xzf (05-09-08)
                         * ��ֵ����
                         */
                        //@ objRow["Get"] =m_objCommitOrderArr[i1].m_dmlDosage.ToString() + " " + m_objCommitOrderArr[i1].m_strGetunit;
                        objRow.Cells["Get"].Value = m_objCommitOrderArr[i1].m_dmlGet.ToString() + " " + m_objCommitOrderArr[i1].m_strGetunit;
                        /* <<====================== */
                    }
                    else
                    {
                        objRow.Cells["Get"].Value = "";
                    }
                    //����
                    //objRow["Price"]=m_objCommitOrderArr[i1].m_dmlPrice.ToString("0.0000");
                    //�ϼ�
                    //objRow["TotalMoney"] =(double.Parse(m_objCommitOrderArr[i1].m_dmlGet.ToString()) * double.Parse(m_objCommitOrderArr[i1].m_dmlPrice.ToString())).ToString("0.00");
                    objRow.Cells["Freq"].Value = m_objCommitOrderArr[i1].m_strExecFreqName;
                    objRow.Cells["UseType"].Value = m_objCommitOrderArr[i1].m_strDosetypeName;
                    objRow.Cells["Creator"].Value = m_objCommitOrderArr[i1].m_strCreator;
                    objRow.Cells["CreateDate"].Value = DateTimeToString(m_objCommitOrderArr[i1].m_dtCreatedate);

                    objRow.Tag = m_objCommitOrderArr[i1];
                    if (m_objCommitOrderArr[i1].m_intExecuteType == 2)
                    {
                        objRow.DefaultCellStyle.ForeColor = Color.Black;
                        objRow.DefaultCellStyle.BackColor = clsOrderColor.BackColorTemOrder;
                    }
                }
                #endregion
            }
            if (m_dtvOrder.Rows.Count > 0)
            {
                m_dtvOrder.CurrentCell = m_dtvOrder.Rows[0].Cells[1];
            }
            //m_dtgOrder.EndUpdate();
        }
        private void splitter1_DoubleClick(object sender, System.EventArgs e)
        {
            if (splitter1.SplitPosition < 150)
                splitter1.SplitPosition = 1000;
            else
                splitter1.SplitPosition = splitter1.MinSize;
        }
        #region ɾ��ҽ��
        private void m_cmdDelete_Click(object sender, System.EventArgs e)
        {
            if (m_dtvOrder.SelectedRows.Count <= 0) return;
            clsCommitOrder objItem = (clsCommitOrder)m_dtvOrder.SelectedRows[0].Tag;
            //���ڸ��ӵ��ݵĲ���ɾ��
            if (m_objInputOrder.m_blnExistAttchOrder(objItem.m_strOrderID))
            {
                MessageBox.Show("��ҽ���и��ӵ��ݣ�����ɾ�����ӵ��ݣ�", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            long lngRes = 0;
            #region ɾ��
            if (!IsHaveSonOrder(objItem.m_strOrderID))
            {
                if (MessageBox.Show("�Ƿ�ɾ��ҽ��: " + objItem.m_strName + " ?", "ɾ��ҽ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                else
                {
                    lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngDeleteOrder(new string[] { objItem.m_strOrderID });
                }
            }
            else
            {
                frmConfirmOrderOperate objfrmConfirmOrderOperate = new frmConfirmOrderOperate("ɾ��", objItem.m_strOrderID, false);
                objfrmConfirmOrderOperate.m_txbPatientName.Text = objItem.m_strPatientName;
                objfrmConfirmOrderOperate.m_txbOrderName.Text = objItem.m_strName;
                objfrmConfirmOrderOperate.ShowDialog();
                if (objfrmConfirmOrderOperate.m_intResult == 0)
                {
                    return;
                }
                else
                {
                    //�ݹ�ɾ��ҽ��
                    try
                    {
                        lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngDeleteOrder(objItem.m_strOrderID, true);
                    }
                    catch
                    { }
                }
            }
            #endregion
            //����������
            if (lngRes > 0)
            {
                //MessageBox.Show("ɾ���ɹ���","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
                m_cmdFindCommitOrder();//ˢ��
            }
            else
            {
                MessageBox.Show("ɾ��ʧ�ܣ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// �Ƿ������ҽ��
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <returns></returns>
        private bool IsHaveSonOrder(string p_strOrderID)
        {
            clsBIHOrder[] objResultArr;
            //clsBIHOrderService m_objManage = new clsBIHOrderService();
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderByParentID(p_strOrderID, out objResultArr);
            if (lngRes <= 0 || objResultArr == null || objResultArr.Length <= 0) return false;
            return true;
        }
        #endregion
        #endregion
        #region DataGrid�¼�
        private void m_dtgOrder_m_evtClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
        {
            //if(e.m_intColNumber==0)
            //{
            //    string strChar=m_dtgOrder[e.m_intRowNumber,"CanCommit"].ToString().Trim();
            //    if(strChar==c_strOkChar)
            //        m_dtgOrder[e.m_intRowNumber,"CanCommit"]="";
            //    else
            //        m_dtgOrder[e.m_intRowNumber,"CanCommit"]=c_strOkChar;

            //    m_dtgOrder.Refresh();
            //}
        }


        private string strGetNumber(string p_str)
        {
            if (p_str.Trim() == "") return "";
            string strTem = "";
            int i1 = 0;
            for (i1 = 0; i1 < p_str.Length; i1++)
            {
                try
                {
                    Int32.Parse(p_str.Substring(i1, 1));
                }
                catch
                {
                    break;
                }
                strTem = p_str.Substring(0, i1);
            }
            return strTem;
        }
        #endregion

        #region iLoginInfo ��Ա
        private clsLoginInfo m_objLoginInfo;
        public clsLoginInfo LoginInfo
        {
            get
            {
                return m_objLoginInfo;
            }
            set
            {
                m_objLoginInfo = value;

                if (m_objLoginInfo == null)
                {
                    m_objCurrentDoctor = null;
                    m_lblDoctor.Text = "��ǰҽ��:";
                }
                else
                {

                    m_objCurrentDoctor = new clsBIHDoctor();
                    m_objCurrentDoctor.m_strDoctorID = m_objLoginInfo.m_strEmpID;
                    m_objCurrentDoctor.m_strDoctorName = m_objLoginInfo.m_strEmpName;
                    m_objCurrentDoctor.m_strDoctorNo = m_objLoginInfo.m_strEmpNo;

                    m_lblDoctor.Text = "��ǰҽ��:" + m_objCurrentDoctor.m_strDoctorName;
                }
            }
        }

        #endregion
        #region ����
        /// <summary>
        /// ��ȡ�ų�ҽ��������
        /// </summary>
        /// <param name="strExcludeOrderIDArr">�ų�ҽ��ID</param>
        /// <returns></returns>
        private string GetExcludeOrderName(string[] strExcludeOrderIDArr)
        {
            string strExcludeOrderName = "";
            for (int i1 = 0; i1 < strExcludeOrderIDArr.Length; i1++)
            {
                if (strExcludeOrderIDArr[i1] == null || strExcludeOrderIDArr[i1].Trim() == "") continue;
                for (int i2 = 0; i2 < m_dtvOrder.RowCount; i2++)
                {
                    if (m_dtvOrder["CanCommit", i2].Value.ToString().Trim() == c_strOkChar && m_dtvOrder["OrderID", i2].Value.ToString().Trim() == strExcludeOrderIDArr[i1].Trim())
                    {
                        strExcludeOrderName += "\n����" + m_dtvOrder["RecipeNo", i2].Value.ToString().Trim() + "-" + m_dtvOrder["Name", i2].Value.ToString().Trim();//NO
                    }
                }
            }
            return strExcludeOrderName;
        }
        /// <summary>
        /// �ύʱ������֤
        /// </summary>
        /// <param name="p_strOrderIDArr">ҽ��ID [����]</param>
        /// <returns></returns>
        private int PassCommitRecipeNOValidate(ref string error)
        {
            string recipeNo = string.Empty;

            Dictionary<string, int> dicProxy = new Dictionary<string, int>();
            for (int i1 = 0; i1 < m_dtvOrder.RowCount; i1++)
            {
                if (c_strOkChar == m_dtvOrder["CanCommit", i1].Value.ToString().Trim())//�Ƿ�ѡ��
                {
                    for (int i2 = 0; i2 < m_dtvOrder.RowCount; i2++)
                    {
                        //if(סԺ�Ǽ�ID && ���� && û��ѡ��) return false;
                        if (i1 == i2)
                        {
                            continue;
                        }
                        if (m_dtvOrder["RegisterID", i1].Value.ToString().Trim() == m_dtvOrder["RegisterID", i2].Value.ToString().Trim()
                            && m_dtvOrder["RecipeNo", i1].Value.ToString().Trim() == m_dtvOrder["RecipeNo", i2].Value.ToString().Trim())
                        {
                            if (c_strOkChar != m_dtvOrder["CanCommit", i2].Value.ToString().Trim())
                            {
                                return -1;
                            }
                            else
                            {
                                if (((clsBIHOrder)this.m_dtvOrder.Rows[i1].Tag).IsProxyBoilMed != ((clsBIHOrder)this.m_dtvOrder.Rows[i2].Tag).IsProxyBoilMed)
                                {
                                    return -2;
                                }
                            }
                        }
                    }
                }
            }
            return 1;

        }
        /// <summary>
        /// �ύʱ������֤
        /// </summary>
        /// <param name="p_strOrderIDArr">ҽ��ID [����]</param>
        /// <returns></returns>
        private bool PassCommitRecipeNOValidate(string[] p_strOrderIDArr)
        {
            string[] p_strOrderIDTempArr = new clsDcl_ExecuteOrder().GetOrderIDSameRecipeNOForCommit(p_strOrderIDArr, 0);
            //if(p_strOrderIDArr.Length!=p_strOrderIDTempArr.Length) return false; else return true; //��Ϊ���ﷵ�ص�ҽ��IDû����ͬ��

            bool blnHaveSame = false;
            for (int i1 = 0; i1 < p_strOrderIDTempArr.Length; i1++)
            {
                blnHaveSame = false;
                for (int i2 = 0; i2 < p_strOrderIDArr.Length; i2++)
                {
                    if (p_strOrderIDTempArr[i1].Trim() == p_strOrderIDArr[i2].Trim())
                    {
                        blnHaveSame = true;
                        break;
                    }
                }
                if (!blnHaveSame) return false;
            }
            return true;
        }
        private string DateTimeToString(DateTime dtValue)
        {
            if (dtValue.Date == DateTime.MinValue.Date || dtValue.Date == System.DateTime.MinValue)
                return "";
            else
                return dtValue.ToString("yyyy-MM-dd HH:mm");
        }
        /// <summary>
        /// �洢������Ϣ	[��������] {ҽ��ID[�ؼ���],���ö���(ArrayList)}
        /// </summary>
        System.Collections.Hashtable m_htbToolTip = new Hashtable();
        /// <summary>
        /// ��DataGrid����Ϣ
        /// </summary>
        /// <param name="p_intRow">��ǰ�к�</param>
        /// <param name="p_dgDataGrid">DataGrid �ؼ�</param>
        /// <returns></returns>
        public void m_DisPlayToolTipDataGrid(clsCommitOrder objItem)
        {
            m_dtvOrderdicCharge.Rows.Clear();
            //�Ƿ�����շ�
            //	if((m_objCommitOrderArr[p_intRow].m_intExecuteType==2 && m_objCommitOrderArr[p_intRow].m_intIsRepare>=3 && m_objCommitOrderArr[p_intRow].m_intIsRepare<=4))
            if ((objItem.m_intExecuteType == 2 && objItem.m_intIsRepare >= 3 && objItem.m_intIsRepare <= 4))
            {
                if (m_htbToolTip.ContainsKey(m_strCurOrderID))
                {
                    m_htbToolTip.Remove(m_strCurOrderID);
                }
                return;
            }
            else
            {
                if (!m_htbToolTip.ContainsKey(m_strCurOrderID))
                {
                    FillToolTipHashtable(objItem);
                }
            }
            if (m_htbToolTip.ContainsKey(m_strCurOrderID))
            {
                ArrayList alItem = new ArrayList();
                alItem = (m_htbToolTip[m_strCurOrderID] as ArrayList);
                if (alItem != null && alItem.Count > 0)
                {
                    clsChargeForDisplay[] objItemArr = (clsChargeForDisplay[])(alItem.ToArray(typeof(clsChargeForDisplay)));
                    //��ʾListView
                    DisplayCharge(objItemArr);
                }
            }
        }
        /// <summary>
        /// ����ϣ����ֵ ToopTip
        /// </summary>
        /// <param name="p_objItem">ҽ����¼����</param>
        /// <returns></returns>
        private void FillToolTipHashtable(clsCommitOrder objItem)
        {
            long lngRes = 0;

            //��ȡҽ��ID
            string strOrderID = objItem.m_strOrderID;
            //���շѵ�����
            double dblNumber = double.Parse(objItem.m_dmlGet.ToString());
            clsT_aid_bih_ordercate_VO objOrdercate;
            lngRes = m_objInputOrder.m_lngGetAidOrderCateByID(objItem.m_strOrderDicCateID, out objOrdercate);
            if (lngRes > 0 && objOrdercate != null && objOrdercate.m_intDOSAGEVIEWTYPE == 2) dblNumber = 1;
            //ִ��Ƶ��ID
            string strFreqID = objItem.m_strExecFreqID;
            //�÷�ID
            string strUsageID = objItem.m_strDosetypeID;
            //�Ƿ��Ӽ�ҽ��	{0=���Ӽ�ҽ��;1=�Ӽ�ҽ��}
            int intIsSonOrder = 0;
            if (objItem.m_strParentID != null && objItem.m_strParentID.Trim() != "")
                intIsSonOrder = 1;

            clsChargeForDisplay[] objItemArr;
            lngRes = m_objInputOrder.m_lngGetBIHCharge(strOrderID, intIsSonOrder, dblNumber, strFreqID, strUsageID, out objItemArr, false);
            if (objItemArr != null && objItemArr.Length > 0)
            {
                ArrayList alItem = new ArrayList();
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    alItem.Add(objItemArr[i1]);
                }
                if (alItem != null && alItem.Count > 0 && (!m_htbToolTip.ContainsKey(strOrderID)))
                {
                    m_htbToolTip.Add(strOrderID, alItem);
                }
            }
        }
        /// <summary>
        /// ��ջ�������
        /// </summary>
        public void m_ClearBuffer()
        {
            m_htbToolTip.Clear();
        }
        #endregion
        #region �������
        private void m_txtArea_m_evtInitListView(System.Windows.Forms.ListView lvwList)
        {
            m_txtAreaInitListView(lvwList);
        }

        private void m_txtArea_m_evtFindItem(object sender, string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            m_txtAreaFindItem(strFindCode, lvwList);
        }

        private void m_txtArea_m_evtSelectItem(object sender, System.Windows.Forms.ListViewItem lviSelected)
        {
            m_txtAreaSelectItem(lviSelected);
            m_txtBed.Focus();
        }
        private void m_lsvSelectBed_Leave(object sender, System.EventArgs e)
        {
            m_lsvSelectBedLeave();
        }
        private void m_lsvSelectBed_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_lsvSelectBedLeave();
                m_cmdFind.Focus();
            }
            else if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        selectAll();
                        break;
                    case Keys.Z:
                        selectAllNo();
                        break;
                    case Keys.X:
                        ConvertSelect();
                        break;
                }

            }
        }

        #region ��λѡ���ݼ�
        private void selectAllNo()
        {
            for (int i = 0; i < m_lsvSelectBed.Items.Count; i++)
            {
                m_lsvSelectBed.Items[i].Checked = false;
            }
        }

        private void ConvertSelect()
        {
            for (int i = 0; i < m_lsvSelectBed.Items.Count; i++)
            {
                m_lsvSelectBed.Items[i].Checked = !m_lsvSelectBed.Items[i].Checked;
            }
        }

        private void selectAll()
        {
            for (int i = 0; i < m_lsvSelectBed.Items.Count; i++)
            {
                m_lsvSelectBed.Items[i].Checked = true;
            }
        }
        #endregion

        private void m_txtBed_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_lsvSelectBed.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Clickable;
                m_txtBedKeyDown();
            }
        }
        /// <summary>
        /// ���ò���
        /// </summary>
        /// <param name="strAreaID">����ID</param>
        /// <param name="strAreaName">��������</param>
        public void m_mthSetArea(string strAreaID, string strAreaName)
        {
            m_txtArea.Text = strAreaName;
            m_txtArea.Tag = strAreaID;
            LoadBedListView();
        }
        /// <summary>
        ///  ��ȡ�����ò���	����ID
        /// </summary>
        public string m_BedIDs
        {
            get
            {
                if (this.m_txtBed.Tag == null) this.m_txtBed.Tag = "";
                return this.m_txtBed.Tag.ToString().Trim();
            }
            set
            {
                string strText = "";
                string strID = value;
                string[] strIDArr = strID.Split(new char[] { ',' });
                if (m_lsvSelectBed.Items.Count <= 0)
                {
                    if (this.m_txtArea.Tag != null && this.m_txtArea.Tag.ToString().Trim() != "")
                    {
                        LoadBedListView();
                    }
                    else
                    {
                        return;
                    }
                }
                clsT_Bse_Bed_VO objItem = new clsT_Bse_Bed_VO();
                for (int i1 = 0; i1 < m_lsvSelectBed.Items.Count; i1++)
                {
                    objItem = ((m_lsvSelectBed.Items[i1].Tag) as clsT_Bse_Bed_VO);
                    for (int i2 = 0; i2 < strIDArr.Length; i2++)
                    {
                        if (objItem.m_strBEDID_CHR.Trim() == strIDArr[i2].Trim())
                        {
                            if (strText.Trim() == "")
                                strText = objItem.m_strCODE_CHR.Trim();
                            else
                                strText += "," + objItem.m_strCODE_CHR.Trim();
                        }
                    }
                }
                m_txtBed.Text = strText;
                m_txtBed.Tag = strID;
            }
        }
        /// <summary>
        /// ���벡����Ϣ
        /// </summary>
        public void LoadBedListView()
        {
            m_txtBed.Text = "";
            m_txtBed.Tag = "";
            m_lsvSelectBed.Items.Clear();
            if (m_txtArea.Tag == null) m_txtArea.Tag = "";
            string strAreaID = m_txtArea.Tag.ToString().Trim();
            if (strAreaID.Trim() == "") return;
            clsT_Bse_Bed_VO[] objItemArr;
            long lngRes = m_objInputOrder.m_lngGetBedInfoByAreaID(strAreaID, out objItemArr);
            if (lngRes > 0 && objItemArr != null && objItemArr.Length > 0)
            {
                #region ���ListView
                ListViewItem lviTemp = null;
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    //���
                    //lviTemp = new ListViewItem((i1+1).ToString());
                    //����
                    //lviTemp.SubItems.Add(objItemArr[i1].m_strCODE_CHR);
                    //���
                    //lviTemp.SubItems.Add(objItemArr[i1].m_strSexName);
                    //ռ��״̬
                    //lviTemp.SubItems.Add(objItemArr[i1].m_strStatusName);					
                    lviTemp = new ListViewItem(objItemArr[i1].m_strCODE_CHR);
                    lviTemp.SubItems.Add(objItemArr[i1].m_strPatientName);
                    lviTemp.SubItems.Add(objItemArr[i1].m_strPatientSex);
                    //lviTemp = new ListViewItem(objItemArr[i1].m_strPatientName);
                    lviTemp.Tag = objItemArr[i1];
                    m_lsvSelectBed.Items.Add(lviTemp);
                }
                #endregion
            }
        }
        /// <summary>
        /// �ָ���","��	Text���洲��	Tag����ID
        /// </summary>
        public void m_lsvSelectBedLeave()
        {
            string strText = "";
            string strID = "";
            clsT_Bse_Bed_VO objItem = new clsT_Bse_Bed_VO();
            for (int i1 = 0; i1 < m_lsvSelectBed.Items.Count; i1++)
            {
                objItem = ((m_lsvSelectBed.Items[i1].Tag) as clsT_Bse_Bed_VO);
                if (m_lsvSelectBed.Items[i1].Checked)
                {
                    if (strText.Length > 0)
                    {
                        strText += ",";
                        strID += ",";
                    }
                    strText += objItem.m_strCODE_CHR.Trim();
                    strID += objItem.m_strBEDID_CHR.Trim();
                }
            }
            m_txtBed.Text = strText;
            m_txtBed.Tag = strID;
            m_lsvSelectBed.Visible = false;
        }
        public void m_txtBedKeyDown()
        {
            LoadBedListView();

            //��������	
            #region ��������
            if (m_txtBed.Tag == null) m_txtBed.Tag = "";
            string strID = m_txtBed.Tag.ToString().Trim();
            string[] strIDArr = strID.Split(new char[] { ',' });
            if (strIDArr != null && strIDArr.Length > 0)
            {
                for (int i = 0; i < m_lsvSelectBed.Items.Count; i++)
                {
                    clsT_Bse_Bed_VO objItem = (m_lsvSelectBed.Items[i].Tag as clsT_Bse_Bed_VO);
                    if (objItem == null) continue;
                    strID = objItem.m_strBEDID_CHR.Trim();
                    m_lsvSelectBed.Items[i].Checked = false;
                    for (int j = 0; j < strIDArr.Length; j++)
                    {
                        if (strID == strIDArr[j].Trim())
                        {
                            m_lsvSelectBed.Items[i].Checked = true;
                            break;
                        }
                    }
                }
            }
            #endregion
            m_lsvSelectBed.Visible = true;
            m_lsvSelectBed.Focus();
        }
        public void m_txtAreaInitListView(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Columns.Add("", 100, HorizontalAlignment.Left);
            lvwList.Width = 120;
        }
        public void m_txtAreaFindItem(string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            clsBIHArea[] objItemArr;
            long lngRes = m_objInputOrder.m_lngFindArea(strFindCode, out objItemArr);
            //��ȡ��Ȩ�޷��ʵĲ���ID����
            if (this.LoginInfo != null)
            {
                IList ilUsableAreaID = this.LoginInfo.m_ilUsableAreaID;
                clsDcl_InputOrder objInputOrder = new clsDcl_InputOrder();
                objItemArr = (clsBIHArea[])(objInputOrder.GetUsableAreaObject(objItemArr, ilUsableAreaID)).ToArray(typeof(clsBIHArea));
            }
            if (lngRes > 0 && objItemArr != null && objItemArr.Length > 0)
            {
                for (int i = 0; i < objItemArr.Length; i++)
                {
                    ListViewItem lvi = lvwList.Items.Add(objItemArr[i].m_strAreaName);
                    lvi.Tag = objItemArr[i].m_strAreaID;
                }
            }
        }
        public void m_txtAreaSelectItem(System.Windows.Forms.ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                m_txtArea.Text = lviSelected.Text;
                m_txtArea.Tag = lviSelected.Tag;
                //LoadBedListView();
            }
        }
        #endregion

        #region �������
        /// <summary>
        /// ��������
        /// </summary>
        /*
        public bool sendTheCheck(ref ArrayList SendCheckArr, out ArrayList m_arrLisOrders)
        {
            clsBIHLis obj = new clsBIHLis();
            //frmLisAppl obj = new frmLisAppl();
            m_arrLisOrders = new ArrayList();
            clsLisApplMainVO objLMVO;
            clsTestApplyItme_VO[] itemArr_VO;
            ArrayList personArr;

            //  m_objService.m_lngAddNewCheckByOrderID(objOrder, out p_dtResultArr);
            m_getPersonGroupList(SendCheckArr, out personArr);
            //Ϊÿһ��������һ���������뵥
            for (int i = 0; i < personArr.Count; i++)
            {
                ArrayList commitArr = new ArrayList();
                for (int k = 0; k < SendCheckArr.Count; k++)
                {
                    if (((clsCommitOrder)SendCheckArr[k]).m_strPatientID == (string)personArr[i])
                    {
                        commitArr.Add((clsCommitOrder)SendCheckArr[k]);
                    }
                }
                if (commitArr.Count <= 0)
                {
                    continue;
                }
                long m_lngRef = m_objService.m_mthSendTestApplyBillByCommit(commitArr, out objLMVO, out itemArr_VO);
                if (m_lngRef <= 0)
                {
                    return false;
                }

                objLisMainVO = objLMVO;
                objLisApplyItmeVOArr = itemArr_VO;

                //if (obj.m_mthNewApp(objLMVO, itemArr_VO, true))
                //{
                //    //��ʱע��
                //    clsLISAppResults[] objAppResult = obj.m_objGetMutiResults();
                //    if (objAppResult != null)
                //    {
                //        for (int i2 = 0; i2 < objAppResult.Length; i2++)
                //        {
                //            if (objAppResult[i2].m_arrOrderId != null && objAppResult[i2].m_arrOrderId.Length > 0)
                //            {
                //                for (int j = 0; j < objAppResult[i2].m_arrOrderId.Length; j++)
                //                {
                //                    if (!m_arrLisOrders.Contains(objAppResult[i2].m_arrOrderId[j]))
                //                    {
                //                        m_arrLisOrders.Add(objAppResult[i2].m_arrOrderId[j]);
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    if (objAppResult.Length > 0)
                //    {
                //        //   objOrder.m_strLISAPPID_VCHR = objAppResult[0].m_StrApplicationID.ToString().Trim();
                //        //    objOrder.m_strSAMPLEID_VCHR = m_strSampleid;
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}
                //else
                //{
                //    return false;
                //}
            }
            return true;
            //  frmLisAppl obj = new frmLisAppl();

            //  clsLisApplMainVO objLMVO;
            //  clsTestApplyItme_VO[] itemArr_VO;
            //  ArrayList personArr;

            ////  m_objService.m_lngAddNewCheckByOrderID(objOrder, out p_dtResultArr);
            //  m_getPersonGroupList(SendCheckArr, out personArr);
            //  //Ϊÿһ��������һ���������뵥
            //  for (int i = 0; i < personArr.Count; i++)
            //  {
            //      ArrayList commitArr=new ArrayList();
            //      for(int k=0;k<SendCheckArr.Count;k++)
            //      {
            //          if(((clsCommitOrder)SendCheckArr[k]).m_strPatientID==(string)personArr[i])
            //          {
            //              commitArr.Add((clsCommitOrder)SendCheckArr[k]);
            //          }
            //      }
            //      if (commitArr.Count <= 0)
            //      {
            //          continue;
            //      }
            //      m_objService.m_mthSendTestApplyBillByCommit(commitArr,out objLMVO, out itemArr_VO);
            //      if (obj.m_mthNewApp(objLMVO, itemArr_VO, false) == System.Windows.Forms.DialogResult.OK)
            //      {
            //          //��ʱע��
            //          clsLISAppResults[] objAppResult = obj.m_objGetMutiResults();
            //          if (objAppResult.Length > 0)
            //          {
            //           //   objOrder.m_strLISAPPID_VCHR = objAppResult[0].m_StrApplicationID.ToString().Trim();
            //          //    objOrder.m_strSAMPLEID_VCHR = m_strSampleid;
            //          }
            //      }
            //      else
            //      {

            //          return false;
            //      }
            //  }
            //  return true;

        } */

        public bool sendTheCheck(ref ArrayList SendCheckArr, out ArrayList m_arrLisOrders)
        {
            ArrayList list;
            clsBIHLis lis = new clsBIHLis();
            m_arrLisOrders = new ArrayList();
            this.m_getPersonGroupList(SendCheckArr, out list);
            for (int i = 0; i < list.Count; i++)
            {
                List<clsCommitOrder> commitArr = new List<clsCommitOrder>();
                for (int j = 0; j < SendCheckArr.Count; j++)
                {
                    if (((clsCommitOrder)SendCheckArr[j]).m_strPatientID == ((string)list[i]))
                    {
                        commitArr.Add((clsCommitOrder)SendCheckArr[j]);
                    }
                }
                if (commitArr.Count > 0)
                {
                    clsLisApplMainVO applyMainVo;
                    clsTestApplyItme_VO[] applyItemVoArr;
                    if ((new weCare.Proxy.ProxyIP()).Service.m_mthSendTestApplyBillByCommit(commitArr, out applyMainVo, out applyItemVoArr) <= 0)
                    {
                        return false;
                    }
                    if (!lis.m_mthNewApp(applyMainVo, applyItemVoArr, true))
                    {
                        return false;
                    }
                    clsLISAppResults[] resultsArray = lis.m_objGetMutiResults();
                    if (resultsArray != null)
                    {
                        for (int k = 0; k < resultsArray.Length; k++)
                        {
                            if ((resultsArray[k].m_arrOrderId != null) && (resultsArray[k].m_arrOrderId.Length > 0))
                            {
                                for (int m = 0; m < resultsArray[k].m_arrOrderId.Length; m++)
                                {
                                    if (!m_arrLisOrders.Contains(resultsArray[k].m_arrOrderId[m]))
                                    {
                                        m_arrLisOrders.Add(resultsArray[k].m_arrOrderId[m]);
                                    }
                                }
                            }
                        }
                    }
                    if (resultsArray.Length <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// �����������ύ�����б�
        /// </summary>
        /// <param name="SendCheckArr"></param>
        /// <param name="p_dtResultArr"></param>
        private void m_getPersonGroupList(ArrayList SendCheckArr, out ArrayList personArr)
        {
            personArr = new ArrayList();
            for (int i = 0; i < SendCheckArr.Count; i++)
            {
                string m_strperID = ((clsCommitOrder)SendCheckArr[i]).m_strPatientID.ToString().Trim();
                if (!personArr.Contains(m_strperID))
                {
                    personArr.Add(m_strperID);
                }
            }

        }

        /// <summary>
        /// �ύ���ͼ�����뵥
        /// </summary>
        private bool m_mthSendCheckApplyBill(ref ArrayList SendCheckArr)
        {

            clsApplyRecord objApplyVO;

            com.digitalwave.GLS_WS.clsApplyForm objfrm = new com.digitalwave.GLS_WS.clsApplyForm();
            for (int i = 0; i < SendCheckArr.Count; i++)
            {
                clsTestApplyItme_VO item_VO = new clsTestApplyItme_VO();
                clsCommitOrder p_objCommitOrder = (clsCommitOrder)SendCheckArr[i];
                m_mthFillApplyInfo(p_objCommitOrder, out objApplyVO);
                item_VO.m_decDiscount = 0;
                item_VO.m_decPrice = p_objCommitOrder.m_dmlPrice;
                item_VO.m_decQty = p_objCommitOrder.m_dmlGet;
                item_VO.m_decTolPrice = item_VO.m_decQty * item_VO.m_decPrice;
                item_VO.m_strItemID = p_objCommitOrder.m_strChargeITEMID_CHR;
                item_VO.m_strItemName = p_objCommitOrder.m_strCharegITEMName;
                item_VO.m_strSpec = p_objCommitOrder.m_strSpec;
                item_VO.m_strUnit = p_objCommitOrder.m_strUseunit;
                item_VO.m_strOutpatRecipeID = "";
                item_VO.m_strRowNo = i.ToString();
                item_VO.m_strOprDeptID = "";

                string strTypeID = "";
                DataTable dt;
                long l = (new weCare.Proxy.ProxyIP()).Service.m_mthGetApplyTypeByID(p_objCommitOrder.m_strChargeITEMID_CHR, out dt);
                if (l > 0 && dt.Rows.Count > 0)
                {
                    strTypeID = dt.Rows[0]["APPLY_TYPE_INT"].ToString().Trim();
                    objApplyVO.m_strDiagnosePart = p_objCommitOrder.m_strPARTNAME_VCHR;
                }
                if (strTypeID == "" || strTypeID == "-1")
                {
                    continue;
                }
                objApplyVO.m_strTypeID = strTypeID;
                objApplyVO.m_objChargeItem = item_VO;
                objApplyVO.m_intChargeStatus = 1;
                objApplyVO.m_objAttachRelation.m_strSourceItemID = p_objCommitOrder.m_strOrderID;
                clsCheckType objCTArr = objfrm.GetIDWithVO(objApplyVO);
                if (objCTArr != null)
                {

                }

            }
            return true;
        }

        private void m_mthFillApplyInfo(clsCommitOrder p_objCommitOrder, out clsApplyRecord objApplyVO)
        {
            objApplyVO = new clsApplyRecord();
            objApplyVO.m_datApplyDate = DateTime.Now;
            //objApplyVO.m_strAddress = p_objCommitOrder.;
            objApplyVO.m_strAge = p_objCommitOrder.m_strAge;
            objApplyVO.m_strCardNO = p_objCommitOrder.m_strPATIENTCARDID_CHR;
            objApplyVO.m_strDiagnose = p_objCommitOrder.m_strDIAGNOSE_VCHR;
            objApplyVO.m_strDoctorName = p_objCommitOrder.m_strCreator;
            //objApplyVO.m_strDoctorNO = p_objCommitOrder.m_strCreatorID;
            objApplyVO.m_strDoctorID = p_objCommitOrder.m_strCreatorID;
            objApplyVO.m_strName = p_objCommitOrder.m_strPatientName;
            objApplyVO.m_strSex = p_objCommitOrder.m_strsex_chr;
            //objApplyVO.m_strTel = this.m_objViewer.m_PatInfo.PatientTelephoneNo;
            //objApplyVO.m_strSummary = this.m_mthGetCaseHistory();
            objApplyVO.m_objAttachRelation.m_intSysFrom = 2;
            objApplyVO.m_strDeptID = p_objCommitOrder.m_strDEPTID_CHR;
            objApplyVO.m_strDepartment = p_objCommitOrder.m_strDEPTNAME_VCHR;
            objApplyVO.m_strBedNO = p_objCommitOrder.m_strBedName;
            objApplyVO.m_strBIHNO = p_objCommitOrder.m_strINPATIENTID_CHR;
            objApplyVO.m_strDiagnosePart = p_objCommitOrder.m_strPARTID_VCHR;
            objApplyVO.m_intSubmitted = 1;
        }

        #endregion

        private void m_dtOrderdicCharge_m_evtCurrentCellChanged(object sender, EventArgs e)
        {


        }

        private void m_dtOrderdicCharge_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
        {
            buttonXP2_Click(null, null);

        }

        private void buttonXP1_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.CurrentRow == null || this.m_dtvOrder.CurrentRow.Tag == null)
            {
                return;
            }

            clsCommitOrder objItem = (clsCommitOrder)this.m_dtvOrder.CurrentRow.Tag;

            frmChargeItem frmCharge = new frmChargeItem(objItem);
            if (frmCharge.ShowDialog() == DialogResult.OK)
            {
                m_htbToolTip.Remove(m_strCurOrderID);
                //��ȡҽ��ID
                string strOrderID = objItem.m_strOrderID;

                m_strCurOrderID = strOrderID;

                //��ʾ������Ϣ
                m_DisPlayToolTipDataGrid(objItem);


            };
        }

        private void buttonXP2_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.CurrentRow == null || this.m_dtvOrder.CurrentRow.Tag == null)
            {
                return;
            }
            if (m_dtvOrderdicCharge.CurrentRow == null || this.m_dtvOrderdicCharge.CurrentRow.Tag == null)
            {
                return;
            }
            clsChargeForDisplay objItem = (clsChargeForDisplay)m_dtvOrderdicCharge.CurrentRow.Tag;
            //���շ���Ŀ�������޸� �շ����m_intType	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
            string m_intType = objItem.m_intType.ToString().Trim();
            //if (m_intType.Trim().Equals("2"))
            //{
            //    MessageBox.Show("���շ���Ŀ�������޸�!","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            //    return;
            //}
            /*<---------------------------*/

            string m_strSeq_int = objItem.m_strSeq_int;
            string m_strGet_DEC = m_dtvOrderdicCharge["SumNumber", m_dtvOrderdicCharge.CurrentRow.Index].Value.ToString().Trim();

            //MessageBox.Show(strOrderID);
            frmChargeItem frmCharge = new frmChargeItem(m_strSeq_int);
            //��ʼ��������Ϣ
            try
            {
                frmCharge.m_intType = int.Parse(m_intType);
            }
            catch
            {
            }
            /*<========================*/
            if (frmCharge.ShowDialog() == DialogResult.OK)
            {
                m_htbToolTip.Remove(m_strCurOrderID);


                clsCommitOrder objItem2 = (clsCommitOrder)this.m_dtvOrder.CurrentRow.Tag;

                //��ȡҽ��ID
                string strOrderID = objItem2.m_strOrderID;

                m_strCurOrderID = strOrderID;

                //��ʾ������Ϣ
                m_DisPlayToolTipDataGrid(objItem2);


            };
        }

        private void buttonXP3_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.CurrentRow == null || this.m_dtvOrder.CurrentRow.Tag == null)
            {
                return;
            }
            if (m_dtvOrderdicCharge.CurrentRow == null || this.m_dtvOrderdicCharge.CurrentRow.Tag == null)
            {
                return;
            }
            clsChargeForDisplay objItem = (clsChargeForDisplay)m_dtvOrderdicCharge.CurrentRow.Tag;

            //���շ���Ŀ�������޸�
            string m_intType = m_dtvOrderdicCharge["m_intType", m_dtvOrderdicCharge.CurrentRow.Index].Value.ToString().Trim();
            if (m_intType.Trim().Equals("2"))
            {
                MessageBox.Show("���շ���Ŀ������ɾ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            /*<---------------------------*/
            string m_strSeq_int = objItem.m_strSeq_int;
            long reg = m_objInputOrder.m_lngDellORDERCHARGEDEPT(m_strSeq_int);
            if (reg <= 0)
            {
                MessageBox.Show("ɾ��ʧ��!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                m_htbToolTip.Remove(m_strCurOrderID);
                clsCommitOrder objItem2 = (clsCommitOrder)this.m_dtvOrder.CurrentRow.Tag;
                //��ȡҽ��ID
                string strOrderID = objItem2.m_strOrderID;

                m_strCurOrderID = strOrderID;

                //��ʾ������Ϣ
                m_DisPlayToolTipDataGrid(objItem2);
            }
        }

        private void frmBIHOrderCommit_Load(object sender, EventArgs e)
        {
            if (m_txtArea.Tag == null)
            {
                m_txtArea.Tag = this.LoginInfo.m_strInpatientAreaID;
                m_txtArea.Text = this.LoginInfo.m_strInpatientAreaName;

            }

        }

        private void m_dtvOrder_CurrentCellChanged(object sender, EventArgs e)
        {
            //if (this.m_dtvOrder.CurrentRow ==null|| this.m_dtvOrder.CurrentRow.Tag == null)
            //{
            //    return;
            //}

            //clsCommitOrder objItem = (clsCommitOrder)m_dtvOrder.CurrentRow.Tag;

            ////��ȡҽ��ID
            //string strOrderID = objItem.m_strOrderID.ToString().Trim();
            //if (m_strCurOrderID == strOrderID) return;
            //m_strCurOrderID = strOrderID;

            ////��ʾ������Ϣ
            //m_DisPlayToolTipDataGrid(objItem);
        }

        private void m_dtvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                string strChar = m_dtvOrder["CanCommit", e.RowIndex].Value.ToString().Trim();
                if (strChar == c_strOkChar)
                    m_dtvOrder["CanCommit", e.RowIndex].Value = "";
                else
                    m_dtvOrder["CanCommit", e.RowIndex].Value = c_strOkChar;
            }
        }

        #region ��ʾ������Ϣ
        /// <summary>
        /// ��ʾ������Ϣ	{ҩƷ���á��÷�����}
        /// </summary>
        /// <param name="objItemArr">������������</param>
        /// <param name="p_dgDataGrid"></param>
        public void DisplayCharge(clsChargeForDisplay[] objItemArr)
        {
            //ҩƷ������ɫ
            System.Drawing.Color clMedicineBackColor = System.Drawing.SystemColors.Window;
            System.Drawing.Color clMedicineForeColor = System.Drawing.SystemColors.WindowText;
            //�÷�������ɫ
            System.Drawing.Color clUsageBackColor = System.Drawing.Color.LightGreen;
            System.Drawing.Color clUsageForeColor = System.Drawing.SystemColors.WindowText;

            m_dtvOrderdicCharge.Rows.Clear();
            if (objItemArr != null && objItemArr.Length > 0)
            {
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    DataGridViewRow objRow;
                    m_dtvOrderdicCharge.Rows.Add();
                    objRow = m_dtvOrderdicCharge.Rows[m_dtvOrderdicCharge.RowCount - 1];
                    //���
                    objRow.Cells["NO2"].Value = (i1 + 1).ToString();
                    //�շ���Ŀ����
                    objRow.Cells["ItemName"].Value = objItemArr[i1].m_strName;
                    //�շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                    switch (objItemArr[i1].m_intType)
                    {
                        case 1:
                            objRow.Cells["IsChiefItem"].Value = "�����շ�";
                            break;
                        case 2:
                            objRow.Cells["IsChiefItem"].Value = "���շ�";
                            break;
                        case 3:
                            objRow.Cells["IsChiefItem"].Value = "�÷��շ�";
                            break;
                        default:
                            objRow.Cells["IsChiefItem"].Value = "";
                            break;
                    }
                    //����		
                    if (!double.IsInfinity(objItemArr[i1].m_dblPrice))
                        objRow.Cells["MinPrice"].Value = objItemArr[i1].m_dblPrice.ToString("0.0000");
                    else
                        objRow.Cells["MinPrice"].Value = "0.0000";
                    if (objItemArr[i1].m_intIsContinueOrder == 1 && objItemArr[i1].m_intType != 3)
                    {
                        //����					
                        objRow.Cells["SumNumber"].Value = "-";
                        //�ϼƽ��
                        objRow.Cells["SumMoney"].Value = "-";
                    }
                    else
                    {
                        //����					
                        objRow.Cells["SumNumber"].Value = objItemArr[i1].m_dblDrawAmount.ToString();
                        //�ϼƽ��
                        if (!double.IsInfinity(objItemArr[i1].m_dblMoney))
                            objRow.Cells["SumMoney"].Value = objItemArr[i1].m_dblMoney.ToString("0.00");
                        else
                            objRow.Cells["SumMoney"].Value = "0.00";
                    }
                    //�������� {-1=���÷��շѣ�ҩƷ�շѵȣ�;0=������;1=ȫ������;2-��������}
                    switch (objItemArr[i1].m_intCONTINUEUSETYPE_INT)
                    {
                        case -1:
                            objRow.Cells["CONTINUEUSETYPE_INT"].Value = "-";
                            break;
                        case 0:
                            objRow.Cells["CONTINUEUSETYPE_INT"].Value = "������";
                            break;
                        case 1:
                            objRow.Cells["CONTINUEUSETYPE_INT"].Value = "ȫ������";
                            break;
                        case 2:
                            objRow.Cells["CONTINUEUSETYPE_INT"].Value = "��������";
                            break;
                        default:
                            objRow.Cells["CONTINUEUSETYPE_INT"].Value = "";
                            break;
                    }
                    // ����ִ�п���
                    objRow.Cells["excuteDept"].Value = objItemArr[i1].m_strClacareaName_chr;
                    //�ݴ�סԺ������Ŀ�շ���Ŀִ�пͻ������ˮ��
                    objRow.Cells["seq_int"].Value = objItemArr[i1].m_strSeq_int;
                    // �շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                    objRow.Cells["m_intType"].Value = objItemArr[i1].m_intType.ToString();
                    objRow.Tag = objItemArr[i1];
                    //�����ɫ
                    #region �����ɫ
                    switch (objItemArr[i1].m_intType)//{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                    {
                        case 1:
                            //for(int intCol=0;intCol<p_dgDataGrid.Columns.Count;intCol++)
                            //{
                            //	p_dgDataGrid.m_mthFormatCell(p_dgDataGrid.RowCount-1,intCol,p_dgDataGrid.Font,clMedicineBackColor,clMedicineForeColor);
                            //}
                            break;
                        case 2:
                            //for(int intCol=0;intCol<p_dgDataGrid.Columns.Count;intCol++)
                            //{
                            //	p_dgDataGrid.m_mthFormatCell(p_dgDataGrid.RowCount-1,intCol,p_dgDataGrid.Font,clMedicineBackColor,clMedicineForeColor);
                            //}
                            break;
                        case 3:
                            //for(int intCol=0;intCol<p_dgDataGrid.Columns.Count;intCol++)
                            //{
                            //	p_dgDataGrid.m_mthFormatCell(p_dgDataGrid.RowCount-1,intCol,p_dgDataGrid.Font,clUsageBackColor,clUsageForeColor);
                            //}
                            break;
                    }
                    #endregion
                }
            }

        }
        /// <summary>
        /// ��ʾ������Ϣ	{ҩƷ���á��÷�����}
        /// </summary>
        /// <param name="objItemArr">������������</param>
        /// <param name="p_dgDataGrid"></param>
        public void DisplayCharge(clsChargeForDisplay[] objItemArr, ListView p_lsvListView)
        {
            //ҩƷ������ɫ
            System.Drawing.Color clMedicineBackColor = System.Drawing.SystemColors.Window;
            System.Drawing.Color clMedicineForeColor = System.Drawing.SystemColors.WindowText;
            //�÷�������ɫ
            System.Drawing.Color clUsageBackColor = System.Drawing.Color.LightGreen;
            System.Drawing.Color clUsageForeColor = System.Drawing.SystemColors.WindowText;
            p_lsvListView.Items.Clear();

            if (objItemArr != null && objItemArr.Length > 0)
            {
                ListViewItem lviTemp = null;
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    //���
                    lviTemp = new ListViewItem((i1 + 1).ToString());
                    //�շ���Ŀ����
                    lviTemp.SubItems.Add(objItemArr[i1].m_strName);
                    //�շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                    switch (objItemArr[i1].m_intType)
                    {
                        case 1:
                            lviTemp.SubItems.Add("�����շ�");
                            break;
                        case 2:
                            lviTemp.SubItems.Add("���շ�");
                            break;
                        case 3:
                            lviTemp.SubItems.Add("�÷��շ�");
                            break;
                        default:
                            lviTemp.SubItems.Add("");
                            break;
                    }
                    //����			
                    if (!double.IsInfinity(objItemArr[i1].m_dblPrice))
                        lviTemp.SubItems.Add(objItemArr[i1].m_dblPrice.ToString("0.0000"));
                    else
                        lviTemp.SubItems.Add("0.0000");
                    if (objItemArr[i1].m_intIsContinueOrder == 1 && objItemArr[i1].m_intType != 3)
                    {
                        //����					
                        lviTemp.SubItems.Add("-");
                        //�ϼƽ��
                        lviTemp.SubItems.Add("-");
                    }
                    else
                    {
                        //����					
                        lviTemp.SubItems.Add(objItemArr[i1].m_dblDrawAmount.ToString());
                        //�ϼƽ��
                        if (!double.IsInfinity(objItemArr[i1].m_dblMoney))
                            lviTemp.SubItems.Add(objItemArr[i1].m_dblMoney.ToString("0.00"));
                        else
                            lviTemp.SubItems.Add("0.00");
                    }
                    //�������� {-1=���÷��շѣ�ҩƷ�շѵȣ�;0=������;1=ȫ������;2-��������}
                    switch (objItemArr[i1].m_intCONTINUEUSETYPE_INT)
                    {
                        case -1:
                            lviTemp.SubItems.Add("-");
                            break;
                        case 0:
                            lviTemp.SubItems.Add("������");
                            break;
                        case 1:
                            lviTemp.SubItems.Add("ȫ������");
                            break;
                        case 2:
                            lviTemp.SubItems.Add("��������");
                            break;
                        default:
                            lviTemp.SubItems.Add("");
                            break;
                    }
                    // ����ִ�п���
                    lviTemp.SubItems.Add(objItemArr[i1].m_strClacareaName_chr);
                    if (objItemArr[i1].m_strIsYB == 1)
                    {
                        lviTemp.SubItems.Add("��");
                        lviTemp.SubItems.Add(objItemArr[i1].m_strYBClass.ToString().Trim());
                    }
                    else
                    {
                        lviTemp.SubItems.Add("��");
                        lviTemp.SubItems.Add("-");
                    }

                    lviTemp.Tag = objItemArr[i1];
                    #region �����ɫ
                    switch (objItemArr[i1].m_intType)//{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                    {
                        case 1:
                            //lviTemp.BackColor =clMedicineBackColor;
                            //lviTemp.ForeColor =clMedicineForeColor;
                            break;
                        case 2:
                            //lviTemp.BackColor =clMedicineBackColor;
                            //lviTemp.ForeColor =clMedicineForeColor;
                            break;
                        case 3:
                            //lviTemp.BackColor =clUsageBackColor;
                            //lviTemp.ForeColor =clUsageForeColor;
                            break;
                    }
                    #endregion

                    #region ͻ����ʾȱҩ��Ŀ	glzhang	2005.10.14
                    if (objItemArr[i1].m_strNoqtyFLag == "1")
                    {
                        lviTemp.ForeColor = System.Drawing.Color.Red;
                        lviTemp.SubItems.Add("ȱҩ");
                    }
                    #endregion

                    p_lsvListView.Items.Add(lviTemp);
                    //�����ɫ
                }
            }
        }
        #endregion

        private void m_dtvOrderdicCharge_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            m_dtOrderdicCharge_m_evtDoubleClickCell(null, null);
        }
    }
}
