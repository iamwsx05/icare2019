using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using com.digitalwave.iCare.BIHOrder.Control;
using weCare.Core.Entity;
using com.digitalwave.iCare.gui.HIS;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// frmChargeItem ��ժҪ˵����
    /// </summary>
    public class frmChargeItem : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox m_txtUnitPrice;
        private System.Windows.Forms.TextBox m_txtUnit;
        public TextBox m_txtGet;
        private System.Windows.Forms.TextBox m_txtItemName;
        private System.Windows.Forms.TextBox m_txtOrderName;
        private System.Windows.Forms.TextBox m_txtPatientName;
        private System.Windows.Forms.TextBox m_txtBedNo;
        private System.Windows.Forms.Label label11;
        public TextBox m_txtDiscount;
        public TextBox m_txtDes;
        private System.Windows.Forms.TextBox m_txtSpec;
        private PinkieControls.ButtonXP m_cmdOk;
        private PinkieControls.ButtonXP m_cmdCancel;
        private System.Windows.Forms.CheckBox m_chkIsRich;
        public ctlFindTextBox m_txtInputCode;
        private System.Windows.Forms.Label label13;
        public ctlFindTextBox ctlCLACAREA_CHR;
        private Label label14;
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;


        private clsBIHPatientCharge m_objTempCharge = null;
        clsCommitOrder m_objCommitOrder = null;
        //private clsBIHChargeItemService m_objService;
        //private clsBIHOrderChangeService m_objService2;
        //private clsBIHORDERCHARGEDService m_ObjCHARGEDService;
        private clsBIHExecOrder m_objExecOrder;
        private string m_strDoctorID;
        /// <summary>
        /// T_OPR_BIH_ORDERCHARGEDEPT����ˮ��
        /// </summary>
        private string m_strSeq_int = "";

        /// <summary>
        /// 0-������1��������2���޸ģ�3��ɾ��
        /// </summary>
        private int m_intStatus = 0;
        /// <summary>
        /// 0-������1��������2���޸ģ�3��ɾ�� (סԺ������Ŀ�շ���Ŀִ�пͻ���ʹ��)
        /// </summary>
        private int m_intNewStatus = 0;
        /// <summary>
        /// �������� {0=������;1=ȫ������;2-��������}
        /// </summary>
        public int m_intCONTINUEUSETYPE_INT = 0;
        private Label label15;
        public com.digitalwave.controls.ctlQComboBox m_cboCONTINUEUSETYPE_INT;
        /// <summary>
        /// �շ���� 0-����Ŀ 1-������Ŀ 2-�÷����� 3-����¼����Ŀ
        /// </summary>
        public int m_intType = 3;


        public frmChargeItem(clsBIHExecOrder objExecOrder, string strDoctorID)
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
            //m_objService=new clsBIHChargeItemService();
            //m_objService = new clsDcl_GetSvcObject().m_GetBIHChargeItemSvc();
            m_objExecOrder = objExecOrder;
            m_txtBedNo.Text = objExecOrder.m_strBedName;
            m_txtPatientName.Text = objExecOrder.m_strPatientName;
            m_txtOrderName.Text = objExecOrder.m_strName;

            m_strDoctorID = strDoctorID;
        }

        public frmChargeItem(clsBIHPatientCharge m_objCharge, clsBIHExecOrder objExecOrder, string strDoctorID)
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
            //m_objService = new clsBIHChargeItemService();
            //m_objService = new clsDcl_GetSvcObject().m_GetBIHChargeItemSvc();
            m_objExecOrder = objExecOrder;
            m_txtBedNo.Text = objExecOrder.m_strBedName;
            m_txtPatientName.Text = objExecOrder.m_strPatientName;
            m_txtOrderName.Text = objExecOrder.m_strName;

            m_strDoctorID = strDoctorID;
            List<string> m_arclacarea = new List<string>();
            m_arclacarea.Add(m_objCharge.m_strClacArea);
            m_arclacarea.Add(m_objCharge.m_strExecDeptName);
            ctlCLACAREA_CHR.Tag = m_arclacarea;
            ctlCLACAREA_CHR.Text = m_objCharge.m_strExecDeptName;
        }
        // �޸������Ϣ����ִ�п���
        /// <summary>
        /// �޸������Ϣ����ִ�п���
        /// </summary>
        /// <param name="m_strSeq_int">T_OPR_BIH_ORDERCHARGEDEPT����ˮ��</param>
        public frmChargeItem(string strSeq_int)
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();
            clsBIHChargeItem objChargeItem;
            string m_strGet = "";
            clsBIHExecOrder objExecOrder;
            ((clsCtl_ChargeItem)this.objController).LoadData(strSeq_int, out objChargeItem, out objExecOrder);
            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
            // m_objService = new clsBIHChargeItemService();
            //m_objService = new clsDcl_GetSvcObject().m_GetBIHChargeItemSvc();
            // m_ObjCHARGEDService=new clsBIHORDERCHARGEDService();
            //m_ObjCHARGEDService = new clsDcl_GetSvcObject().m_GetBIHORDERCHARGEDSvc();
            m_txtBedNo.Text = objExecOrder.m_strBedName;
            m_txtGet.Text = objExecOrder.m_dmlGet.ToString();
            m_txtGet.Tag = objExecOrder.m_dmlDosageRate.ToString();//һ�μ���
            m_txtPatientName.Text = objExecOrder.m_strPatientName;
            m_txtOrderName.Text = objExecOrder.m_strName;
            m_intCONTINUEUSETYPE_INT = objChargeItem.m_intCONTINUEUSETYPE_INT;
            List<string> m_arclacarea = new List<string>();
            m_arclacarea.Add(objExecOrder.m_strExecDeptID);
            m_arclacarea.Add(objExecOrder.m_strExecDeptName);

            ctlCLACAREA_CHR.Tag = m_arclacarea;
            ctlCLACAREA_CHR.Text = objExecOrder.m_strExecDeptName;
            m_mthShowChargeItem(objChargeItem);
            m_intNewStatus = 2;
            m_strSeq_int = strSeq_int;
            m_objExecOrder = objExecOrder;

        }

        public frmChargeItem(clsCommitOrder objCommitOrder)
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //m_ObjCHARGEDService=new clsBIHORDERCHARGEDService();
            //m_ObjCHARGEDService = new clsDcl_GetSvcObject().m_GetBIHORDERCHARGEDSvc();
            //m_objService = new clsBIHChargeItemService();
            //m_objService = new clsDcl_GetSvcObject().m_GetBIHChargeItemSvc();
            m_objExecOrder = new clsBIHExecOrder();
            m_txtBedNo.Text = objCommitOrder.m_strBedName;
            m_txtPatientName.Text = objCommitOrder.m_strPatientName;
            m_txtOrderName.Text = objCommitOrder.m_strName;

            m_intNewStatus = 1;
            m_objExecOrder.m_strParentID = objCommitOrder.m_strPatientID;
            m_objExecOrder.m_strRegisterID = objCommitOrder.m_strRegisterID;
            m_objExecOrder.m_strOrderID = objCommitOrder.m_strOrderID;
            m_objExecOrder.m_intExecuteType = objCommitOrder.m_intExecuteType;
            m_objExecOrder.m_strParentName = objCommitOrder.m_strPatientName;
            m_objExecOrder.m_strBedName = objCommitOrder.m_strBedName;
            m_objExecOrder.m_strOrderDicID = objCommitOrder.m_strOrderDicID;
            m_objExecOrder.m_strCREATEAREA_ID = objCommitOrder.m_strCREATEAREA_ID;


        }

        public frmChargeItem(clsBIHCanExecOrder objCommitOrder)
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //m_ObjCHARGEDService = new clsBIHORDERCHARGEDService();
            //m_ObjCHARGEDService = new clsDcl_GetSvcObject().m_GetBIHORDERCHARGEDSvc();
            //m_objService = new clsBIHChargeItemService();
            //m_objService = new clsDcl_GetSvcObject().m_GetBIHChargeItemSvc();
            m_objExecOrder = new clsBIHExecOrder();
            m_txtBedNo.Text = objCommitOrder.m_strBedName;
            m_txtPatientName.Text = objCommitOrder.m_strPatientName;
            m_txtOrderName.Text = objCommitOrder.m_strName;

            m_intNewStatus = 1;
            m_objExecOrder.m_strParentID = objCommitOrder.m_strPatientID;
            m_objExecOrder.m_strRegisterID = objCommitOrder.m_strRegisterID;
            m_objExecOrder.m_strOrderID = objCommitOrder.m_strOrderID;
            m_objExecOrder.m_intExecuteType = objCommitOrder.m_intExecuteType;
            m_objExecOrder.m_strParentName = objCommitOrder.m_strPatientName;
            m_objExecOrder.m_strBedName = objCommitOrder.m_strBedName;
            m_objExecOrder.m_strOrderDicID = objCommitOrder.m_strOrderDicID;
            m_objExecOrder.m_strCREATEAREA_ID = objCommitOrder.m_strCREATEAREA_ID;


        }

        public frmChargeItem(clsCtl_BIHOrderExecute.clsOrderBaseInfo objCommitOrder)
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //m_ObjCHARGEDService = new clsBIHORDERCHARGEDService();
            //m_ObjCHARGEDService = new clsDcl_GetSvcObject().m_GetBIHORDERCHARGEDSvc();
            //m_objService = new clsBIHChargeItemService();
            //m_objService = new clsDcl_GetSvcObject().m_GetBIHChargeItemSvc();
            m_objExecOrder = new clsBIHExecOrder();
            m_txtBedNo.Text = objCommitOrder.m_objBIHCanExecOrder.m_strBedName;
            m_txtPatientName.Text = objCommitOrder.m_objBIHCanExecOrder.m_strPatientName;
            m_txtOrderName.Text = objCommitOrder.m_objBIHCanExecOrder.m_strName;

            m_intNewStatus = 1;
            m_objExecOrder.m_strParentID = objCommitOrder.m_objBIHCanExecOrder.m_strPatientID;
            m_objExecOrder.m_strRegisterID = objCommitOrder.m_objBIHCanExecOrder.m_strRegisterID;
            m_objExecOrder.m_strOrderID = objCommitOrder.m_objBIHCanExecOrder.m_strOrderID;
            m_objExecOrder.m_intExecuteType = objCommitOrder.m_objBIHCanExecOrder.m_intExecuteType;
            m_objExecOrder.m_strParentName = objCommitOrder.m_objBIHCanExecOrder.m_strPatientName;
            m_objExecOrder.m_strBedName = objCommitOrder.m_objBIHCanExecOrder.m_strBedName;
            m_objExecOrder.m_strOrderDicID = objCommitOrder.m_objBIHCanExecOrder.m_strOrderDicID;
            m_objExecOrder.m_strCREATEAREA_ID = objCommitOrder.m_objBIHCanExecOrder.m_strCREATEAREA_ID;



        }

        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.BIHOrder.clsCtl_ChargeItem();
            objController.Set_GUI_Apperance(this);
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.m_txtUnitPrice = new System.Windows.Forms.TextBox();
            this.m_txtUnit = new System.Windows.Forms.TextBox();
            this.m_txtGet = new System.Windows.Forms.TextBox();
            this.m_txtItemName = new System.Windows.Forms.TextBox();
            this.m_txtOrderName = new System.Windows.Forms.TextBox();
            this.m_txtPatientName = new System.Windows.Forms.TextBox();
            this.m_txtBedNo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_txtDiscount = new System.Windows.Forms.TextBox();
            this.m_txtDes = new System.Windows.Forms.TextBox();
            this.m_txtSpec = new System.Windows.Forms.TextBox();
            this.m_cmdOk = new PinkieControls.ButtonXP();
            this.m_cmdCancel = new PinkieControls.ButtonXP();
            this.m_chkIsRich = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.m_cboCONTINUEUSETYPE_INT = new com.digitalwave.controls.ctlQComboBox();
            this.ctlCLACAREA_CHR = new com.digitalwave.iCare.BIHOrder.Control.ctlFindTextBox();
            this.m_txtInputCode = new com.digitalwave.iCare.BIHOrder.Control.ctlFindTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "��������:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "��    ��:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(430, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "��    λ:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "��    ��:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(316, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "����:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(248, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "��    ��:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "ҽ������:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 14);
            this.label8.TabIndex = 9;
            this.label8.Text = "��    ��:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 143);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 14);
            this.label9.TabIndex = 8;
            this.label9.Text = "��    ע:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 299);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 14);
            this.label10.TabIndex = 7;
            this.label10.Text = "�ۿ۱���:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Visible = false;
            // 
            // m_txtUnitPrice
            // 
            this.m_txtUnitPrice.Enabled = false;
            this.m_txtUnitPrice.Location = new System.Drawing.Point(367, 207);
            this.m_txtUnitPrice.Name = "m_txtUnitPrice";
            this.m_txtUnitPrice.ReadOnly = true;
            this.m_txtUnitPrice.Size = new System.Drawing.Size(60, 23);
            this.m_txtUnitPrice.TabIndex = 25;
            // 
            // m_txtUnit
            // 
            this.m_txtUnit.Location = new System.Drawing.Point(502, 207);
            this.m_txtUnit.Name = "m_txtUnit";
            this.m_txtUnit.ReadOnly = true;
            this.m_txtUnit.Size = new System.Drawing.Size(74, 23);
            this.m_txtUnit.TabIndex = 40;
            // 
            // m_txtGet
            // 
            this.m_txtGet.Location = new System.Drawing.Point(324, 100);
            this.m_txtGet.Name = "m_txtGet";
            this.m_txtGet.Size = new System.Drawing.Size(104, 23);
            this.m_txtGet.TabIndex = 2;
            this.m_txtGet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtGet_KeyPress);
            this.m_txtGet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtGet_KeyDown);
            // 
            // m_txtItemName
            // 
            this.m_txtItemName.Enabled = false;
            this.m_txtItemName.Location = new System.Drawing.Point(88, 176);
            this.m_txtItemName.Name = "m_txtItemName";
            this.m_txtItemName.ReadOnly = true;
            this.m_txtItemName.Size = new System.Drawing.Size(340, 23);
            this.m_txtItemName.TabIndex = 10;
            // 
            // m_txtOrderName
            // 
            this.m_txtOrderName.Location = new System.Drawing.Point(88, 50);
            this.m_txtOrderName.Name = "m_txtOrderName";
            this.m_txtOrderName.ReadOnly = true;
            this.m_txtOrderName.Size = new System.Drawing.Size(340, 23);
            this.m_txtOrderName.TabIndex = 70;
            // 
            // m_txtPatientName
            // 
            this.m_txtPatientName.Location = new System.Drawing.Point(88, 17);
            this.m_txtPatientName.Name = "m_txtPatientName";
            this.m_txtPatientName.ReadOnly = true;
            this.m_txtPatientName.Size = new System.Drawing.Size(150, 23);
            this.m_txtPatientName.TabIndex = 60;
            // 
            // m_txtBedNo
            // 
            this.m_txtBedNo.Location = new System.Drawing.Point(324, 17);
            this.m_txtBedNo.Name = "m_txtBedNo";
            this.m_txtBedNo.ReadOnly = true;
            this.m_txtBedNo.Size = new System.Drawing.Size(104, 23);
            this.m_txtBedNo.TabIndex = 65;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 104);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 14);
            this.label11.TabIndex = 18;
            this.label11.Text = "��    ��:";
            // 
            // m_txtDiscount
            // 
            this.m_txtDiscount.Location = new System.Drawing.Point(97, 293);
            this.m_txtDiscount.Name = "m_txtDiscount";
            this.m_txtDiscount.Size = new System.Drawing.Size(60, 23);
            this.m_txtDiscount.TabIndex = 3;
            this.m_txtDiscount.Visible = false;
            this.m_txtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtGet_KeyPress);
            this.m_txtDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtDiscount_KeyDown);
            // 
            // m_txtDes
            // 
            this.m_txtDes.Location = new System.Drawing.Point(87, 140);
            this.m_txtDes.MaxLength = 100;
            this.m_txtDes.Name = "m_txtDes";
            this.m_txtDes.Size = new System.Drawing.Size(341, 23);
            this.m_txtDes.TabIndex = 4;
            this.m_txtDes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtDes_KeyDown);
            // 
            // m_txtSpec
            // 
            this.m_txtSpec.Enabled = false;
            this.m_txtSpec.Location = new System.Drawing.Point(88, 207);
            this.m_txtSpec.Name = "m_txtSpec";
            this.m_txtSpec.ReadOnly = true;
            this.m_txtSpec.Size = new System.Drawing.Size(216, 23);
            this.m_txtSpec.TabIndex = 20;
            // 
            // m_cmdOk
            // 
            this.m_cmdOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdOk.DefaultScheme = true;
            this.m_cmdOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdOk.Hint = "";
            this.m_cmdOk.Location = new System.Drawing.Point(455, 12);
            this.m_cmdOk.Name = "m_cmdOk";
            this.m_cmdOk.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdOk.Size = new System.Drawing.Size(108, 28);
            this.m_cmdOk.TabIndex = 5;
            this.m_cmdOk.Text = "ȷ��(F3)";
            this.m_cmdOk.Click += new System.EventHandler(this.m_cmdOk_Click);
            // 
            // m_cmdCancel
            // 
            this.m_cmdCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdCancel.DefaultScheme = true;
            this.m_cmdCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdCancel.Hint = "";
            this.m_cmdCancel.Location = new System.Drawing.Point(455, 45);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCancel.Size = new System.Drawing.Size(108, 28);
            this.m_cmdCancel.TabIndex = 6;
            this.m_cmdCancel.Text = "ȡ��(Esc)";
            this.m_cmdCancel.Click += new System.EventHandler(this.m_cmdCancel_Click);
            // 
            // m_chkIsRich
            // 
            this.m_chkIsRich.AutoCheck = false;
            this.m_chkIsRich.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_chkIsRich.Enabled = false;
            this.m_chkIsRich.ForeColor = System.Drawing.Color.DimGray;
            this.m_chkIsRich.Location = new System.Drawing.Point(429, 178);
            this.m_chkIsRich.Name = "m_chkIsRich";
            this.m_chkIsRich.Size = new System.Drawing.Size(93, 19);
            this.m_chkIsRich.TabIndex = 15;
            this.m_chkIsRich.Text = "��    ��:";
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Location = new System.Drawing.Point(0, 80);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(597, 2);
            this.label13.TabIndex = 28;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(431, 143);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 14);
            this.label14.TabIndex = 72;
            this.label14.Text = "ִ�п���:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(431, 104);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 14);
            this.label15.TabIndex = 73;
            this.label15.Text = "��������:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cboCONTINUEUSETYPE_INT
            // 
            this.m_cboCONTINUEUSETYPE_INT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCONTINUEUSETYPE_INT.Location = new System.Drawing.Point(501, 100);
            this.m_cboCONTINUEUSETYPE_INT.Name = "m_cboCONTINUEUSETYPE_INT";
            this.m_cboCONTINUEUSETYPE_INT.Size = new System.Drawing.Size(77, 22);
            this.m_cboCONTINUEUSETYPE_INT.TabIndex = 75;
            this.m_cboCONTINUEUSETYPE_INT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboCONTINUEUSETYPE_INT_KeyDown);
            // 
            // ctlCLACAREA_CHR
            // 
            this.ctlCLACAREA_CHR.Location = new System.Drawing.Point(501, 140);
            this.ctlCLACAREA_CHR.Name = "ctlCLACAREA_CHR";
            this.ctlCLACAREA_CHR.Size = new System.Drawing.Size(77, 23);
            this.ctlCLACAREA_CHR.TabIndex = 1;
            this.ctlCLACAREA_CHR.m_evtFindItem += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_OnFindItem(this.ctlCLACAREA_CHR_m_evtFindItem);
            this.ctlCLACAREA_CHR.m_evtInitListView += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_InitListView(this.ctlCLACAREA_CHR_m_evtInitListView);
            this.ctlCLACAREA_CHR.m_evtSelectItem += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_OnSelectItem(this.ctlCLACAREA_CHR_m_evtSelectItem);
            // 
            // m_txtInputCode
            // 
            this.m_txtInputCode.Location = new System.Drawing.Point(88, 100);
            this.m_txtInputCode.Name = "m_txtInputCode";
            this.m_txtInputCode.Size = new System.Drawing.Size(150, 23);
            this.m_txtInputCode.TabIndex = 0;
            this.m_txtInputCode.m_evtFindItem += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_OnFindItem(this.m_txtInputCode_m_evtFindItem);
            this.m_txtInputCode.m_evtInitListView += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_InitListView(this.m_txtInputCode_m_evtInitListView);
            this.m_txtInputCode.m_evtSelectItem += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_OnSelectItem(this.m_txtInputCode_m_evtSelectItem);
            // 
            // frmChargeItem
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(600, 274);
            this.Controls.Add(this.m_cmdCancel);
            this.Controls.Add(this.m_cmdOk);
            this.Controls.Add(this.m_cboCONTINUEUSETYPE_INT);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.ctlCLACAREA_CHR);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.m_txtInputCode);
            this.Controls.Add(this.m_txtSpec);
            this.Controls.Add(this.m_txtDes);
            this.Controls.Add(this.m_txtDiscount);
            this.Controls.Add(this.m_txtBedNo);
            this.Controls.Add(this.m_txtPatientName);
            this.Controls.Add(this.m_txtOrderName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_txtItemName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_txtUnitPrice);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_txtUnit);
            this.Controls.Add(this.m_txtGet);
            this.Controls.Add(this.m_chkIsRich);
            this.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChargeItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "�շ���Ŀ";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmChargeItem_KeyDown);
            this.Load += new System.EventHandler(this.frmChargeItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Input
        private void m_txtInputCode_m_evtInitListView(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Columns.Add("����", 80, HorizontalAlignment.Left);
            lvwList.Columns.Add("����", 200, HorizontalAlignment.Left);
            lvwList.Columns.Add("���", 100, HorizontalAlignment.Left);
            lvwList.Columns.Add("��λ", 40, HorizontalAlignment.Center);
            lvwList.Columns.Add("����", 60, HorizontalAlignment.Left);
            lvwList.Columns.Add("����", 0, HorizontalAlignment.Left);
            lvwList.Columns.Add("ҩ��", 60, HorizontalAlignment.Left);

            lvwList.Size = new Size(545, 200);
            lvwList.HeaderStyle = ColumnHeaderStyle.Clickable;
        }

        private void m_txtInputCode_m_evtFindItem(object sender, string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            clsBIHChargeItem[] arrItem;
            //long ret=m_objService.m_mthFindChargeItem(strFindCode,out arrItem);
            long ret = (new weCare.Proxy.ProxyIP02()).Service.m_mthFindChargeItemWithYB(strFindCode, out arrItem);
            if ((ret > 0) && (arrItem != null))
            {
                for (int i = 0; i < arrItem.Length; i++)
                {
                    ListViewItem lvi = lvwList.Items.Add(arrItem[i].m_strItemCode);
                    lvi.SubItems.Add(arrItem[i].m_strItemName);
                    lvi.SubItems.Add(arrItem[i].m_strSpec);
                    lvi.SubItems.Add(arrItem[i].m_strUnit);
                    lvi.SubItems.Add(arrItem[i].m_dmlPrice.ToString());
                    lvi.SubItems.Add((arrItem[i].m_intIsRich == 1 ? "��" : ""));
                    if (arrItem[i].m_intITEMSRCTYPE_INT == 1 && arrItem[i].m_intIPNOQTYFLAG_INT == 1)
                    {
                        lvi.SubItems.Add("ȱҩ");
                        lvi.ForeColor = Color.Red;
                    }
                    else
                    {
                        lvi.SubItems.Add("");
                    }
                    lvi.Tag = arrItem[i];

                }
            }
        }

        private void m_txtInputCode_m_evtSelectItem(object sender, System.Windows.Forms.ListViewItem lviSelected)
        {
            if ((lviSelected != null) && (lviSelected.Tag != null))
            {
                m_mthShowChargeItem(lviSelected.Tag as clsBIHChargeItem);
                m_txtInputCode.Enabled = true;
                //m_txtGet.Focus();
                System.Collections.Generic.List<string> arrItem = new System.Collections.Generic.List<string>();
                long ret = ((clsCtl_ChargeItem)this.objController).m_lngGetDEPTDefault(m_objExecOrder.m_strCREATEAREA_ID, ((clsBIHChargeItem)lviSelected.Tag).m_strItemID, out arrItem);
                ctlCLACAREA_CHR.Tag = null;
                ctlCLACAREA_CHR.Text = "";
                if (arrItem.Count > 1)
                {
                    ctlCLACAREA_CHR.Tag = arrItem;
                    ctlCLACAREA_CHR.Text = (string)arrItem[1];
                }
                m_txtGet.Focus();
                //ctlCLACAREA_CHR.Focus();
            }
        }

        #endregion

        private void m_mthShowChargeItem(clsBIHChargeItem objItem)
        {
            m_txtInputCode.Tag = objItem;
            if (objItem == null)
            {
                m_txtInputCode.Text = "";
                m_txtInputCode.Enabled = true;
                m_txtItemName.Text = "";
                m_txtSpec.Text = "";
                m_txtUnit.Text = "";
                m_chkIsRich.Checked = false;
                m_txtUnitPrice.Text = "";

                m_txtGet.Text = "";
                m_txtGet.Tag = null;
                m_txtDiscount.Text = "1";
                m_txtDes.Text = "";
            }
            else
            {
                m_txtInputCode.Text = objItem.m_strItemCode;
                m_txtItemName.Text = objItem.m_strItemName;
                m_txtSpec.Text = objItem.m_strSpec;
                m_txtUnit.Text = objItem.m_strUnit;
                m_txtDes.Text = objItem.REMARK;
                m_txtUnitPrice.Text = objItem.m_dmlPrice.ToString();
                m_chkIsRich.Checked = (objItem.m_intIsRich == 1);
                m_txtGet.Tag = objItem.m_dmlDOSAGE_DEC.ToString();
            }
        }

        #region Interface


        //������շ���Ŀ
        public bool m_mthAddNew(out clsBIHPatientCharge objCharge)
        {
            objCharge = null;
            m_intStatus = 1;
            m_mthShowChargeItem(null);
            if (this.ShowDialog() == DialogResult.OK)
            {
                objCharge = m_objTempCharge;
                if (m_objTempCharge == null)
                    return false;
                else
                    return true;
            }
            else
            {
                objCharge = null;
                return false;
            }
        }

        //�޸�
        public bool m_mthModify(clsBIHPatientCharge objCharge)
        {
            if (objCharge == null) return false;
            m_intStatus = 2;

            clsBIHChargeItem objItem;
            long ret = (new weCare.Proxy.ProxyIP02()).Service.m_lngGetChargeItem(objCharge.m_strChargeItemID, out objItem);
            if (ret > 0)
                m_mthShowChargeItem(objItem);
            else
                return false;

            m_objTempCharge = objCharge;
            m_txtGet.Text = objCharge.m_dmlAmount.ToString();
            m_txtDiscount.Text = objCharge.m_dmlDiscount.ToString();
            m_txtUnitPrice.Text = objCharge.m_dmlUnitPrice.ToString();      //��ʱ�ĵ���
            m_txtDes.Text = objCharge.m_strDes;

            if (this.ShowDialog() == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool m_blnDelete(string strPChargeID)
        {
            long ret = (new weCare.Proxy.ProxyIP02()).Service.m_mthDeletePatientCharge(strPChargeID);
            if (ret > 0)
                return true;
            else
                return false;
        }

        #endregion

        private bool m_blnCheck()
        {
            //
            if (m_txtInputCode.Tag == null)
            {
                MessageBox.Show("��ָ���շ���Ŀ!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_txtInputCode.Focus();
                m_txtInputCode.SelectAll();
                return false;
            }
            if (clsConverter.ToDecimal(m_txtGet.Text) <= 0)
            {
                MessageBox.Show("��ָ������!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_txtGet.Focus();
                return false;
            }

            if (clsConverter.ToDecimal(m_txtDiscount.Text) > 1)
            {
                MessageBox.Show("������ۿ۱���!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_txtDiscount.Focus();
                return false;
            }

            return true;
        }

        private clsBIHPatientCharge m_objGetCharge()
        {
            clsBIHChargeItem objItem = m_txtInputCode.Tag as clsBIHChargeItem;

            clsBIHPatientCharge objCharge = new clsBIHPatientCharge();
            if (m_intNewStatus == 0)
            {
                if (m_intStatus == 1)
                {

                    objCharge.m_strPChargeID = "";
                    objCharge.m_strPatientID = m_objExecOrder.m_strPatientID;
                    objCharge.m_strRegisterID = m_objExecOrder.m_strRegisterID;
                    objCharge.m_strOrderID = m_objExecOrder.m_strOrderID;
                    objCharge.m_intOrderExecType = m_objExecOrder.m_intExecuteType;
                    objCharge.m_strOrderExecID = m_objExecOrder.m_strEOrderExecID;
                    objCharge.m_strPatientName = m_objExecOrder.m_strPatientName;
                    objCharge.m_strBedNo = m_objExecOrder.m_strBedName;

                }
                else
                {
                    objCharge = m_objTempCharge;
                }
            }
            else
            {
                if (m_intNewStatus == 1)
                {

                    objCharge.m_strPChargeID = "";
                    objCharge.m_strPatientID = m_objExecOrder.m_strPatientID;
                    objCharge.m_strRegisterID = m_objExecOrder.m_strRegisterID;
                    objCharge.m_strOrderID = m_objExecOrder.m_strOrderID;
                    objCharge.m_intOrderExecType = m_objExecOrder.m_intExecuteType;
                    objCharge.m_strOrderExecID = m_objExecOrder.m_strEOrderExecID;
                    objCharge.m_strPatientName = m_objExecOrder.m_strPatientName;
                    objCharge.m_strBedNo = m_objExecOrder.m_strBedName;
                }
            }
            if (objItem.m_strItemIPCalcType != null)//��ռ�ã�Ҫ�Ļ�
                objCharge.m_strCalcCateID = objItem.m_strItemIPCalcType;
            objCharge.m_strInvCateID = objItem.m_strItemIPInvType;
            objCharge.m_strChargeItemID = objItem.m_strItemID;
            objCharge.m_strChargeItemName = objItem.m_strItemName;
            objCharge.m_strUnit = objItem.m_strUnit;
            objCharge.m_dmlUnitPrice = objItem.m_dmlPrice;
            // ҽ����Ϣ
            objCharge.m_strINSURACEDESC_VCHR = objItem.m_strINSURACEDESC_VCHR;

            objCharge.m_dmlAmount = clsConverter.ToDecimal(m_txtGet.Text);
            objCharge.m_dmlDiscount = clsConverter.ToDecimal(m_txtDiscount.Text);
            objCharge.m_intIsMepay = 0;
            objCharge.m_strDes = m_txtDes.Text.Trim();
            objCharge.m_intCreateType = 3;

            objCharge.m_strCreator = m_strDoctorID;
            objCharge.m_dtCreateDate = DateTime.Now;
            objCharge.m_intStatus = 1;
            objCharge.m_intPStatus = (objItem.m_intIsRich == 1 ? 0 : 1);
            // ������ִ�п���ID 
            string m_strClacarea_chr = "";
            string m_strExecDeptName = "";
            if (ctlCLACAREA_CHR.Tag != null)
            {
                m_strClacarea_chr = (ctlCLACAREA_CHR.Tag as List<string>)[0].ToString();
                m_strExecDeptName = (ctlCLACAREA_CHR.Tag as List<string>)[1].ToString();
            }
            objCharge.m_strClacArea = m_strClacarea_chr;
            objCharge.m_strExecDeptName = m_strExecDeptName;
            objCharge.m_strCreateArea = m_objExecOrder.m_strCREATEAREA_ID;
            /*<----------------------------------*/
            return objCharge;
        }

        private void m_cmdOk_Click(object sender, System.EventArgs e)
        {
            if (((clsCtl_ChargeItem)this.objController).DeptTag)
            {
                ((clsCtl_ChargeItem)this.objController).SaveTheDeptChange();
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            if (!m_blnCheck()) return;


            long ret = 0;
            clsBIHPatientCharge objCharge = m_objGetCharge();
            if (m_intNewStatus == 0)
            {

                if (m_intStatus == 1)
                {
                    ret = (new weCare.Proxy.ProxyIP02()).Service.m_lngAddPatientCharge(objCharge);
                }
                else if (m_intStatus == 2)
                {
                    ret = (new weCare.Proxy.ProxyIP02()).Service.m_mthModifyPatientCharge(objCharge);
                }
                else
                {
                    return;
                }
            }
            else
            {
                clsORDERCHARGEDEPT_VO objItem = new clsORDERCHARGEDEPT_VO();
                ChargeVoToOCDVo(objCharge, ref objItem);
                if (m_intNewStatus == 1)//������Ϣ����[סԺ������Ŀ�շ���Ŀִ�пͻ���-t_opr_bih_orderchargedept]
                {
                    ret = (new weCare.Proxy.ProxyIP()).Service.m_lngAddORDERCHARGEDEPT(objItem);
                }
                else if (m_intNewStatus == 2)
                {
                    ret = (new weCare.Proxy.ProxyIP()).Service.m_lngChangeORDERCHARGEDEPT(objItem);
                }

            }
            if (ret > 0)
            {
                m_objTempCharge = objCharge;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("����ʧ��!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ChargeVoToOCDVo(clsBIHPatientCharge objCharge, ref clsORDERCHARGEDEPT_VO objItem)
        {
            objItem.m_decAmount_dec = objCharge.m_dmlAmount;
            objItem.m_decUnitprice_dec = objCharge.m_dmlUnitPrice;
            objItem.m_strChargeitemid_chr = objCharge.m_strChargeItemID;
            objItem.m_strChargeitemname_chr = objCharge.m_strChargeItemName;
            objItem.m_strClacarea_chr = objCharge.m_strClacArea;
            objItem.m_strCreatearea_chr = objCharge.m_strCreateArea;
            objItem.m_strCreatedate_dat = objCharge.m_dtCreateDate;
            objItem.m_strCreator_vchr = LoginInfo.m_strEmpName;
            objItem.m_strCreatorid_chr = LoginInfo.m_strEmpID;
            objItem.m_strOrderdicid_chr = m_objExecOrder.m_strOrderDicID;
            objItem.m_strOrderid_chr = m_objExecOrder.m_strOrderID;
            objItem.m_strSeq_int = m_strSeq_int;
            objItem.m_strSpec_vchr = m_txtSpec.Text.Trim();
            objItem.m_strUnit_vchr = objCharge.m_strUnit;
            objItem.REMARK = m_txtDes.Text.ToString().Trim();
            //��������
            objItem.m_intCONTINUEUSETYPE_INT = com.digitalwave.iCare.gui.HIS.clsConverter.ToInt(this.m_cboCONTINUEUSETYPE_INT.m_strGetID(this.m_cboCONTINUEUSETYPE_INT.SelectedIndex));
            objItem.m_strINSURACEDESC_VCHR = objCharge.m_strINSURACEDESC_VCHR;
            //���ε�һ�εļ��� 
            objItem.m_decSINGLEAMOUNT_DEC = objItem.m_decAmount_dec;
            if (objItem.m_strClacarea_chr.ToString().Equals(""))
            {
                objItem.m_strClacarea_chr = this.LoginInfo.m_strInpatientAreaID;
            }
        }

        private void m_cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label5_Click(object sender, System.EventArgs e)
        {

        }

        private void frmChargeItem_Load(object sender, System.EventArgs e)
        {
            iniTheControl();
            m_EnableControl();
            new clsTextFocusHighlight().m_mthBindForm(this, true);
        }

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void iniTheControl()
        {
            this.m_cboCONTINUEUSETYPE_INT.m_intAddItem("0", "0-������", "0");
            this.m_cboCONTINUEUSETYPE_INT.m_intAddItem("1", "1-�״���", "1");
            this.m_cboCONTINUEUSETYPE_INT.m_blnFindItem(m_intCONTINUEUSETYPE_INT.ToString());

        }

        /// <summary>
        /// ����״̬���ƽ���ֻ���ؼ�-���շ���ֻ���޸Ŀ��Ҽ���ע
        /// </summary>
        private void m_EnableControl()
        {
            if (m_intType == 0)
            {
                m_txtInputCode.Enabled = false;
                m_txtGet.Enabled = false;
            }
        }

        private void m_txtGet_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.m_txtGet.Text.Trim().Equals(""))
                {
                    return;
                }
                m_cboCONTINUEUSETYPE_INT.Focus();
            }
            //m_txtDiscount.Focus();
            //m_txtDes.Focus();
        }

        private void m_txtDiscount_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) m_txtDes.Focus();
        }

        private void m_txtDes_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.m_cboCONTINUEUSETYPE_INT.Focus();
        }

        private void m_txtGet_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
            }
            else if ((e.KeyChar == '.') || (e.KeyChar == 8) || (e.KeyChar == 13))
            { }
            else
            {
                e.Handled = true;
            }
        }

        private void ctlCLACAREA_CHR_m_evtFindItem(object sender, string strFindCode, ListView lvwList)
        {
            System.Collections.Generic.List<string>[] arrItem;
            if (m_txtInputCode.Tag == null)
            {
                MessageBox.Show("����ѡ���շ���Ŀ!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            long ret = ((clsCtl_ChargeItem)this.objController).m_mthFindChargeItem(strFindCode, out arrItem);
            if ((ret > 0) && (arrItem != null))
            {
                for (int i = 0; i < arrItem.Length; i++)
                {
                    ListViewItem lvi = lvwList.Items.Add(arrItem[i][0].ToString());
                    lvi.SubItems.Add(arrItem[i][1].ToString());
                    lvi.Tag = arrItem[i];

                }
            }
        }

        private void ctlCLACAREA_CHR_m_evtInitListView(ListView lvwList)
        {

            lvwList.Columns.Add("����", 80, HorizontalAlignment.Left);
            lvwList.Columns.Add("����", 220, HorizontalAlignment.Left);

            lvwList.Size = new Size(300, 200);
            lvwList.HeaderStyle = ColumnHeaderStyle.Clickable;
        }

        private void ctlCLACAREA_CHR_m_evtSelectItem(object sender, ListViewItem lviSelected)
        {
            if ((lviSelected != null) && (lviSelected.Tag != null))
            {
                ctlCLACAREA_CHR.Text = (lviSelected.Tag as List<string>)[1].ToString();
                ctlCLACAREA_CHR.Tag = lviSelected.Tag as List<string>;

                m_cmdOk.Focus();
            }
        }

        private void m_cboCONTINUEUSETYPE_INT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                m_cmdOk.Focus();
        }

        private void frmChargeItem_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                #region ��ݼ�
                case Keys.Escape:
                    if (sender == this)
                        m_cmdCancel_Click(null, null);
                    break;
                case Keys.F3://
                    m_cmdOk_Click(null, null);
                    break;
                    #endregion
            }
        }



    }
}
