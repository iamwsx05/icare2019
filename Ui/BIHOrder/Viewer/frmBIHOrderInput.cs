using com.digitalwave.GUI_Base;
using com.digitalwave.iCare.BIHOrder.Control;
using com.digitalwave.iCare.gui.HIS;
using com.digitalwave.iCare.gui.LIS;
using iCare;
using iCare.Anaesthesia.Requisition;
using iCare.CustomForm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// ҽ��¼���ʾ��
    /// </summary>
    public class frmBIHOrderInput : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ���ڱ���
        frmListDoctor fl = new frmListDoctor();
        frmOrderTemplate fo = new frmOrderTemplate();
        //		int intParentRow=-1;
        string CurrentPatientInhospitalNo;
        /// <summary>
        /// ҽ�����ڹ�����ID
        /// </summary>
        public string m_strDOCTORGROUPID_CHR = "";
        /// <summary>
        /// �Ƿ�ҽ������
        /// </summary>
        public bool m_blnISMedicareMan = false;
        /// <summary>
        /// //0-��ͨ�����1����Դ�ڴ�λ����
        /// </summary>
        public int m_intInvoSrc = 0;

        //����/�������濪��
        /// <summary>
        /// ����/�������� ���� -1-������ҿ���ҽ��, 0-��/��,1-��,2-��,3-�ɵ��Ӳ�����
        /// </summary>
        public string m_strView = "0";
        /// <summary>
        /// ҽ��Դ����(0-��ͨ¼��,1-�ر����¼��)
        /// </summary>
        public int m_intSOURCETYPE_INT = 0;
        #region ����
        /// <summary>
        /// Ƭ������Ĺ�ʽ����(0-����������* Ƶ��*����/��װ��)��1������������/��װ��*Ƶ��*����))
        /// </summary>
        public int m_intTypePControl = 0;
        /// <summary>
        /// �Ƿ�Ƭ��
        /// </summary>
        public bool m_blTypeP = false;
        /// <summary>
        /// ҽ���������Ͽ��ƿ���
        /// </summary>
        public bool m_blBlankOutControl = false;
        /// <summary>
        /// ȱҩ��ʾ���ƿ���
        /// </summary>
        public bool m_blLessMedControl = false;
        /// <summary>
        /// ҽ��ǩ�����ƿ���
        /// </summary>
        public bool m_blDoctorSign = false;
        /// <summary>
        /// ҽ���Զ�ǩ��
        /// </summary>
        public int m_intDoctorAutoSign = 0;
        /// <summary>
        ///����ҽ��ת��������̣�0false����������1true������
        /// </summary>
        public bool m_blZCaoControl = false;
        /// <summary>
        ///����ҽ���ύʱ�Ƿ������ʾ
        /// </summary>
        public bool m_blCommitControl = false;
        /// <summary>
        /// ����ҽ���ύʱ�Ƿ������ʾ��1032������ ---- �������Ҫ�� ֵΪ2 ���������޸Ĺ��ŵ��ύȷ�ϴ���
        /// </summary>
        public int m_intCommitControl2 = -1;
        /// <summary>
        /// ҽ��¼��Ȩ�޷������޿���1003 0-�أ�1-��
        /// </summary>
        public bool m_blUpControl = false;
        /// <summary>
        /// ҽ��¼���Ƿ����¼����ͣ�õ��շ���Ŀ 0-��,1-�� 1037
        /// </summary>
        public bool m_blStopControl = false;
        /// <summary>
        /// ҽ��¼���Ƿ����¼��ȱҩ���շ���Ŀ 0-��1-�� 1036
        /// </summary>
        public bool m_blDeableMedControl = false;
        /// <summary>
        /// �Ƿ���ʾҩ�䱸ע���� '0059' 
        /// </summary>
        public bool IsShowCodexRemarkFrm = false;
        /// <summary>
        /// �Ƿ���������ҽ���޸ķǱ��˿���ת��ǰ��ҽ��', '0-�����ԣ�1-����'
        /// </summary>
        public bool m_blCanChangeOrder = false;
        /// <summary>
        /// �鿴��ǰ�����Ƿ��д���Ȩ
        /// </summary>
        bool m_blAccess = false;
        /// <summary>
        ///ҽ��ͣҽ��ʱ�Ƿ������ʾ
        /// </summary>
        public bool m_blStopTipControl = false;
        /// <summary>
        /// ҽ������վҩ�䱸ע������ʾʱ�� ��λ���� '0060'
        /// </summary>
        public int ShowCodexRemarkFrmTimerinterval = 1;
        /// <summary>
        /// �ύʱ�Ƿ�Լ������Ŀ�������뵥����(true-����,false-������)
        /// </summary>
        public bool m_blSendLisBill = false;
        /// <summary>
        /// ҽ���޸�����ʱ���ʱ������ 0-������,>0Ϊ���Ƶ�ʱ��,��24,��Ϊ������24Сʱ�ڿ����޸�  1054
        /// </summary>
        public int m_intStartTimeSwitth = 0;
        /// <summary>
        /// ҽ���޸�ͣ��ʱ���ʱ������ 0-������,>0Ϊ���Ƶ�ʱ��,��24,��Ϊ������24Сʱ�ڿ����޸�  1051
        /// </summary>
        public int m_intStopTimeSwitth = 0;
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
        ///'1053', 'סԺҽ��¼������Ƿ��Զ���ʾ��ǰ���˴���ͣ�û�ȱҩ��δͣҽ��', '0-��1-��', 1, '0010' 
        /// </summary>
        public bool m_blAutoStopAlert = false;
        /// <summary>
        /// ��������δ�ᴦ������ʱ���Ƴ�Ժ������ҽ��¼��(ҽ��¼��1��2״̬��Ϊ��ʾѡ��)0-�ر�;1-��ʾѡ��2-��ס
        /// </summary>
        internal int m_intParm1068 = 0;
        /// <summary>
        /// ϵͳ������(ICARE����) 0013 ������ϴ��۷�Ʊ���� ������������ݸ���
        /// </summary>
        public string m_strLisPARMVALUE_VCHR = "";
        /// <summary>
        /// סԺ����ҩ��ID(0009)����
        /// </summary>
        public string m_strMedDeptId = "";
        /// <summary>
        /// סԺҩƷ����ж�ҩ������
        /// </summary>
        public string m_strMedDeptGross = "";
        #endregion
        /// <summary>
        /// �Ƿ���Ӧdatagridview ��cellchanged �¼�
        /// </summary>
        public bool m_blnCurrentCellChanged = false;

        /// <summary>
        ///  ��ǰ����״̬(0-������ͨҽ��,1-������ҽ��,2-�޸���ͨҽ��,3-�޸���ҽ��,4-,5-,6-,7-)
        /// </summary>
        public int m_intOperateStatus = 0;
        /// <summary>
        /// ҽ�������б�
        /// </summary>
        public Hashtable m_htOrderCate = new Hashtable();
        /// <summary>
        /// סԺ�������ñ�VO
        /// </summary>
        public clsSPECORDERCATE m_objSpecateVo;

        public Color selectOldColor;


        /// <summary>
        ///��ʼʱ��������
        /// </summary>
        public int m_intStarClickCout = 1;
        /// <summary>
        ///����ʱ��������
        /// </summary>
        public int m_intFinishClickCout = 1;

        /// <summary>
        /// ҩƷ�����
        /// </summary>
        public float m_fotOpcurrentgross_num = 0;
        /// <summary>
        /// �Ƿ���ҩƷ
        /// </summary>
        public int m_intITEMSRCTYPE_INT = 0;
        /// <summary>
        /// �Ƿ����޸���  falseΪ����  trueΪ�޸�ҽ��
        /// </summary>
        public bool m_blIsChange = false;
        /// <summary>
        /// סԺ��ҩ��ID
        /// </summary>
        public string m_strMedCineStorgeId = "";
        /// <summary>
        /// ��ҩ��ID
        /// </summary>
        public string m_strMidMedCineSorgeId = "";
        /// <summary>
        /// סԺҽ���������ID
        /// 0008�������ݿ���
        /// </summary>
        public List<string> m_lstSocialSecurity = new List<string>();
        #endregion

        #region �ؼ���������
        private PinkieControls.ButtonXP m_cmdAdd;
        internal PinkieControls.ButtonXP m_cmdSave;
        internal PinkieControls.ButtonXP m_cmdCommitAll;
        internal PinkieControls.ButtonXP m_cmdStop;
        internal PinkieControls.ButtonXP m_cmdRetract;
        private System.Windows.Forms.ContextMenu m_ctxGridMenu;
        private System.Windows.Forms.MenuItem m_mnuStop;
        private System.Windows.Forms.MenuItem m_mnuRetract;
        private System.Windows.Forms.MenuItem m_mnuCommitAll;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.RadioButton m_rdoAllDay;
        private System.Windows.Forms.RadioButton m_rdoToday;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public RadioButton m_rdoTempType;
        public RadioButton m_rdoLongType;
        public RadioButton m_rdoAllType;
        public Panel m_plControls;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private PinkieControls.ButtonXP m_cmdClose;
        private System.Windows.Forms.CheckBox m_chkStatus0;
        private System.Windows.Forms.CheckBox m_chkStatus1;
        private System.ComponentModel.IContainer components;
        //internal clsBIHOrderService m_objService;
        internal ArrayList m_arlOrder;
        internal com.digitalwave.iCare.BIHOrder.Control.ctlBIHPatientInfo m_ctlPatient;
        /// <summary>
        /// ��ǰҽ��
        /// </summary>
        internal clsBIHDoctor m_objCurrentDoctor = null;
        internal PinkieControls.ButtonXP m_cmdToCommit;
        public System.Windows.Forms.LinkLabel m_lblOtherBill;
        public System.Windows.Forms.ContextMenu m_ctxOtherBill;

        private com.digitalwave.iCare.BIHOrder.Control.clsTextFocusHighlight m_objHighlight = null;
        internal com.digitalwave.iCare.BIHOrder.Control.ctlBIHOrderDetail m_ctlOrderDetail;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox m_chkStatus2;
        private System.Windows.Forms.CheckBox m_chkStatus3;
        private System.Windows.Forms.CheckBox m_chkNeedFeel;
        internal PinkieControls.ButtonXP m_cmdBlankOut;
        private System.Windows.Forms.MenuItem m_mnuBlankOut;
        internal PinkieControls.ButtonXP m_cmdDelete;
        private System.Windows.Forms.MenuItem m_mnuDelete;
        private PinkieControls.ButtonXP cmdRefurbish;
        private PinkieControls.ButtonXP cmdPrintOrder;
        internal System.Windows.Forms.ComboBox m_cobOrderCate;
        internal System.Windows.Forms.ToolTip toolTip1;
        public clsBIHOrderInputDomain m_objDomain;
        /// <summary>
        /// �Ƿ���ʾToolTip
        /// </summary>
        private static bool m_IsDisPlayToolTip = false;
        internal System.Windows.Forms.Label m_lblEditState;
        internal System.Windows.Forms.ListView m_lsvToolTip;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        /// <summary>
        /// ToolTip��ʾ���ı�����
        /// </summary>
        //string m_strToolTip ="";
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.MenuItem m_mnuCopy;
        private System.Windows.Forms.Panel pnTabs;
        private System.Windows.Forms.Label lblBoard;
        private System.Windows.Forms.Panel pnSel;
        internal PinkieControls.ButtonXP m_cmdSub;
        internal PinkieControls.ButtonXP btCreatBill;
        internal System.Windows.Forms.ComboBox cmbApply;
        private System.Windows.Forms.Label label14;
        internal PinkieControls.ButtonXP m_btnAddBills;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        internal System.Windows.Forms.TextBox m_lblOrderStatus;
        internal System.Windows.Forms.TextBox m_txtBackReason;
        private System.Windows.Forms.MenuItem menuItem2;
        internal com.digitalwave.controls.AlertLight alertLight1;
        private System.Windows.Forms.Label lbePriceInfo;
        internal System.Windows.Forms.ToolTip toolTip2;
        private CheckBox m_chkStatus4;
        public ComboBox seachClass;
        private Label label16;
        /// <summary>
        /// �Ƿ��Ǵ����һ��LOAD������־��Ϊ�˿��Ʋ�ִ�е�ǰ��ѡ���¼���
        /// </summary>

        private com.digitalwave.Utility.Controls.CollapsibleSplitter collapsibleSplitter1;
        public DataGridView m_dtvOrder;
        private Label label1;
        private MenuItem menuItem3;
        private ContextMenuStrip m_ctxGridMenu2;
        private ToolStripMenuItem m_mnuDelete2;
        private ToolStripMenuItem m_mnuBlankOut2;
        private ToolStripMenuItem m_mnuStop2;
        private ToolStripMenuItem m_mnuRetract2;
        private ToolStripMenuItem m_mnuCommitAll2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem m_mnuCopy2;
        private ToolStripMenuItem m_mnuCopyBihorder2;
        internal PinkieControls.ButtonXP m_cmdChgView;
        private Label label2;
        private Label label17;
        private GroupBox groupBox4;
        public RadioButton radioButton1;
        private Label label4;
        internal PictureBox pictureBox6;
        internal PictureBox pictureBox3;
        private PinkieControls.ButtonXP m_cmdChange;
        private Label label10;
        public com.digitalwave.iCare.BIHOrder.Control.ctlFindTextBox m_txtCREATEAREA;
        private ToolStripMenuItem TSMenuTurnBack;
        internal PinkieControls.ButtonXP m_cmdDelete2;
        private ToolStripMenuItem m_mnuDoctorSign;
        private MenuItem menuItem4;
        private MenuItem menuItem5;
        private ToolStripMenuItem menuItem7;
        private ToolStripMenuItem menuItem8;
        internal ToolStripMenuItem menuItemStopAll;
        internal ToolStripMenuItem m_MenuCopy;
        internal ToolStripMenuItem m_MenuPase;
        internal ToolStripMenuItem m_MenuATTACHTIMES_INT;
        internal ToolStripMenuItem m_MenuOPERATION;
        internal ToolStripMenuItem m_MenuOrderTemp;
        internal ToolStripMenuItem m_MenuOrderNornal;
        internal ToolStripMenuItem m_MenuChangeArea;
        internal ToolStripMenuItem m_MenuReSortOrderNO;
        internal ToolStripMenuItem m_MenuOrderSTsign;
        protected ToolStripMenuItem m_MenuCheckBill;
        private ToolStripMenuItem m_MenuCheckBillEdit;
        private ToolStripMenuItem m_MenuCheckBillView;
        internal ToolStripMenuItem m_MenuViewBackReasion;
        internal PictureBox m_imgBackAlert;
        public ComboBox m_cboOrderList;
        internal ToolStripMenuItem m_MenuOutHis;
        internal ToolStripMenuItem m_MenuOutTomorrow;
        internal ToolStripMenuItem m_MenuOutToday;
        internal ToolStripMenuItem m_MenuCHNAGEAMOUNT_INT;
        internal ToolStripMenuItem m_MenuMoneyCount;
        private ToolStripMenuItem m_MenuSeeBill;
        private ToolStripMenuItem m_MenuSurgery;
        internal PictureBox m_pcBoxAlert;
        private ToolStripMenuItem tsmiDrugInfo;
        private ToolStripMenuItem tsbOpApplyNew;
        internal ComboBox cboPrintType;
        private ToolStripMenuItem tsmiTskjyhz_response;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem tsmiTskjyhz_apply;
        private ToolStripMenuItem tsmiTskjyhz_check;
        private Label label5;
        private DataGridViewTextBoxColumn dtv_ExecuteType;
        private DataGridViewTextBoxColumn m_dtPOSTDATE_DAT;
        private DataGridViewTextBoxColumn CREATOR_CHR;
        private DataGridViewTextBoxColumn ASSESSORFOREXEC_CHR;
        private DataGridViewTextBoxColumn dtv_RecipeNo;
        private DataGridViewTextBoxColumn dtv_Name;
        private DataGridViewTextBoxColumn dtv_Dosage;
        private DataGridViewTextBoxColumn dtv_UseType;
        private DataGridViewTextBoxColumn dtv_Freq;
        private DataGridViewTextBoxColumn dtv_REMARK;
        private DataGridViewTextBoxColumn dtv_FinishDate;
        private DataGridViewTextBoxColumn dtv_Stoper;
        private DataGridViewTextBoxColumn ASSESSORFORSTOP_CHR;
        private DataGridViewTextBoxColumn ATTACHTIMES_INT;
        private DataGridViewTextBoxColumn STATUS_INT;
        private DataGridViewTextBoxColumn RATETYPE_INT;
        private DataGridViewTextBoxColumn isOps;
        private DataGridViewTextBoxColumn MedicareTypeName;
        private DataGridViewTextBoxColumn dtv_Get;
        private DataGridViewTextBoxColumn dtv_Sum;
        private DataGridViewTextBoxColumn dtv_StartDate;
        private DataGridViewTextBoxColumn dtv_Executor;
        private DataGridViewTextBoxColumn dtv_DELETE_DAT;
        private DataGridViewTextBoxColumn dtv_DELETERNAME_VCHR;
        private DataGridViewTextBoxColumn viewname_vchr;
        private DataGridViewTextBoxColumn dtv_method;
        private DataGridViewTextBoxColumn dtv_NO;
        private DataGridViewTextBoxColumn dtv_OUTGETMEDDAYS_INT;
        private DataGridViewTextBoxColumn dtv_CREATEAREA_Name;
        private DataGridViewTextBoxColumn dtv_DOCTOR_VCHR;
        private DataGridViewImageColumn dtv_DOCTOR_SIGN;
        private DataGridViewTextBoxColumn CREATEDATE_DAT;
        private DataGridViewTextBoxColumn dtv_ChangedID;
        private DataGridViewTextBoxColumn dtv_ChangedDate;
        private DataGridViewTextBoxColumn m_dtStartDate;
        private DataGridViewTextBoxColumn m_dtvSENDBACKER_CHR;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem tsmiApp;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem tsmiBloodApp;

        /// <summary>
        /// ��ʾToopTip��λ��
        /// </summary>
        System.Drawing.Point m_poToolTip = new Point(0, 0);
        #endregion
        #region ���캯��
        public frmBIHOrderInput()
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //

            m_arlOrder = new ArrayList();

            m_objHighlight = new com.digitalwave.iCare.BIHOrder.Control.clsTextFocusHighlight();

            m_objDomain = new clsBIHOrderInputDomain(this);

            this.objController = new clsController_Base();

            //m_objService=m_objDomain.m_objService;
        }
        /// <summary>
        /// ���캯��m_intSrc   0-��ͨ�����1����Դ�ڴ�λ����
        /// </summary>
        /// <param name="m_intSrc"></param>
        public frmBIHOrderInput(int m_intSrc)
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //

            m_arlOrder = new ArrayList();

            m_objHighlight = new com.digitalwave.iCare.BIHOrder.Control.clsTextFocusHighlight();

            m_objDomain = new clsBIHOrderInputDomain(this);

            this.objController = new clsController_Base();
            m_intInvoSrc = m_intSrc;
            //m_objService=m_objDomain.m_objService;
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
        #region ����ҽ��������������
        /// <summary>
        /// ���õ�ǰҽ��
        /// </summary>
        /// <param name="strDoctorID"></param>
        /// <param name="strDoctorName"></param>
        public void m_mthSetCurrentDoctor(string strDoctorID, string strDoctorName)
        {
            m_objCurrentDoctor = new clsBIHDoctor();
            m_objCurrentDoctor.m_strDoctorID = strDoctorID;
            m_objCurrentDoctor.m_strDoctorName = strDoctorName;
            m_ctlOrderDetail.m_mthSetDoctor(m_objCurrentDoctor.m_strDoctorID, m_objCurrentDoctor.m_strDoctorName);
        }

        /// <summary>
        /// ���õ�ǰ����
        /// </summary>
        /// <param name="strAreaID"></param>
        public void m_mthSetCurrentArea(string strAreaID)
        {
            m_ctlPatient.m_mthSetArea(strAreaID);
        }

        /// <summary>
        /// ���õ�ǰ����
        /// </summary>
        /// <param name="strInHospitalNo"></param>
        public void m_mthSetCurrentPatient(string strInHospitalNo)
        {
            m_ctlPatient.m_mthSetPatient(strInHospitalNo);
            this.CurrentPatientInhospitalNo = strInHospitalNo;
        }
        #endregion
        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBIHOrderInput));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.m_ctxGridMenu = new System.Windows.Forms.ContextMenu();
            this.m_mnuDelete = new System.Windows.Forms.MenuItem();
            this.m_mnuBlankOut = new System.Windows.Forms.MenuItem();
            this.m_mnuStop = new System.Windows.Forms.MenuItem();
            this.m_mnuRetract = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.m_mnuCommitAll = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.m_mnuCopy = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.m_ctxOtherBill = new System.Windows.Forms.ContextMenu();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdPrintOrder = new PinkieControls.ButtonXP();
            this.m_cmdStop = new PinkieControls.ButtonXP();
            this.m_lblOrderStatus = new System.Windows.Forms.TextBox();
            this.m_chkStatus3 = new System.Windows.Forms.CheckBox();
            this.m_chkStatus4 = new System.Windows.Forms.CheckBox();
            this.m_chkStatus2 = new System.Windows.Forms.CheckBox();
            this.m_chkStatus1 = new System.Windows.Forms.CheckBox();
            this.m_chkStatus0 = new System.Windows.Forms.CheckBox();
            this.m_imgBackAlert = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.m_ctxGridMenu2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_mnuDelete2 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuBlankOut2 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuStop2 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuRetract2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuCommitAll2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuCopy2 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuPase = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuCopyBihorder2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiApp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBloodApp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuSeeBill = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMenuTurnBack = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuDoctorSign = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemStopAll = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuChangeArea = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuOPERATION = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuATTACHTIMES_INT = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuCHNAGEAMOUNT_INT = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuOrderTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuOrderNornal = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuReSortOrderNO = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuOrderSTsign = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuCheckBill = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuCheckBillEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuCheckBillView = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuViewBackReasion = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuOutHis = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuOutToday = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuOutTomorrow = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuMoneyCount = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbOpApplyNew = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuSurgery = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDrugInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTskjyhz_apply = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTskjyhz_check = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTskjyhz_response = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.collapsibleSplitter1 = new com.digitalwave.Utility.Controls.CollapsibleSplitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.m_txtCREATEAREA = new com.digitalwave.iCare.BIHOrder.Control.ctlFindTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_cboOrderList = new System.Windows.Forms.ComboBox();
            this.m_lblEditState = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.m_cobOrderCate = new System.Windows.Forms.ComboBox();
            this.seachClass = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_rdoToday = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.m_rdoAllDay = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.alertLight1 = new com.digitalwave.controls.AlertLight();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_rdoTempType = new System.Windows.Forms.RadioButton();
            this.m_rdoLongType = new System.Windows.Forms.RadioButton();
            this.m_rdoAllType = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.m_chkNeedFeel = new System.Windows.Forms.CheckBox();
            this.cmbApply = new System.Windows.Forms.ComboBox();
            this.m_txtBackReason = new System.Windows.Forms.TextBox();
            this.btCreatBill = new PinkieControls.ButtonXP();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_dtvOrder = new System.Windows.Forms.DataGridView();
            this.dtv_ExecuteType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_dtPOSTDATE_DAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CREATOR_CHR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASSESSORFOREXEC_CHR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_RecipeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_Dosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_UseType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_Freq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_REMARK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_FinishDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_Stoper = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASSESSORFORSTOP_CHR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ATTACHTIMES_INT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS_INT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RATETYPE_INT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isOps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MedicareTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_Get = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_Sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_Executor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_DELETE_DAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_DELETERNAME_VCHR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.viewname_vchr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_method = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_OUTGETMEDDAYS_INT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_CREATEAREA_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_DOCTOR_VCHR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_DOCTOR_SIGN = new System.Windows.Forms.DataGridViewImageColumn();
            this.CREATEDATE_DAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_ChangedID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtv_ChangedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_dtStartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_dtvSENDBACKER_CHR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnSel = new System.Windows.Forms.Panel();
            this.pnTabs = new System.Windows.Forms.Panel();
            this.lbePriceInfo = new System.Windows.Forms.Label();
            this.lblBoard = new System.Windows.Forms.Label();
            this.lblLeft = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cboPrintType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_plControls = new System.Windows.Forms.Panel();
            this.m_cmdDelete2 = new PinkieControls.ButtonXP();
            this.m_cmdChange = new PinkieControls.ButtonXP();
            this.m_cmdToCommit = new PinkieControls.ButtonXP();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.m_cmdAdd = new PinkieControls.ButtonXP();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.m_cmdRetract = new PinkieControls.ButtonXP();
            this.m_cmdBlankOut = new PinkieControls.ButtonXP();
            this.cmdRefurbish = new PinkieControls.ButtonXP();
            this.m_cmdSub = new PinkieControls.ButtonXP();
            this.m_cmdCommitAll = new PinkieControls.ButtonXP();
            this.m_cmdClose = new PinkieControls.ButtonXP();
            this.m_cmdChgView = new PinkieControls.ButtonXP();
            this.m_ctlOrderDetail = new com.digitalwave.iCare.BIHOrder.Control.ctlBIHOrderDetail();
            this.label14 = new System.Windows.Forms.Label();
            this.m_btnAddBills = new PinkieControls.ButtonXP();
            this.m_lsvToolTip = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label13 = new System.Windows.Forms.Label();
            this.m_lblOtherBill = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.m_pcBoxAlert = new System.Windows.Forms.PictureBox();
            this.m_ctlPatient = new com.digitalwave.iCare.BIHOrder.Control.ctlBIHPatientInfo();
            ((System.ComponentModel.ISupportInitialize)(this.m_imgBackAlert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.m_ctxGridMenu2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtvOrder)).BeginInit();
            this.panel5.SuspendLayout();
            this.pnTabs.SuspendLayout();
            this.panel3.SuspendLayout();
            this.m_plControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pcBoxAlert)).BeginInit();
            this.SuspendLayout();
            // 
            // m_ctxGridMenu
            // 
            this.m_ctxGridMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_mnuDelete,
            this.m_mnuBlankOut,
            this.m_mnuStop,
            this.m_mnuRetract,
            this.menuItem1,
            this.m_mnuCommitAll,
            this.menuItem2,
            this.m_mnuCopy,
            this.menuItem3,
            this.menuItem4,
            this.menuItem5});
            this.m_ctxGridMenu.Popup += new System.EventHandler(this.m_ctxGridMenu_Popup);
            // 
            // m_mnuDelete
            // 
            this.m_mnuDelete.Index = 0;
            this.m_mnuDelete.Text = "ɾ��";
            this.m_mnuDelete.Click += new System.EventHandler(this.m_mnuDelete_Click);
            // 
            // m_mnuBlankOut
            // 
            this.m_mnuBlankOut.Index = 1;
            this.m_mnuBlankOut.Text = "����";
            this.m_mnuBlankOut.Click += new System.EventHandler(this.m_mnuBlankOut_Click);
            // 
            // m_mnuStop
            // 
            this.m_mnuStop.Index = 2;
            this.m_mnuStop.Text = "ֹͣ";
            this.m_mnuStop.Click += new System.EventHandler(this.m_mnuStop_Click);
            // 
            // m_mnuRetract
            // 
            this.m_mnuRetract.Index = 3;
            this.m_mnuRetract.Text = "����";
            this.m_mnuRetract.Click += new System.EventHandler(this.m_mnuRetract_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.Text = "-";
            // 
            // m_mnuCommitAll
            // 
            this.m_mnuCommitAll.Index = 5;
            this.m_mnuCommitAll.Text = "�ύ�����½�ҽ��";
            this.m_mnuCommitAll.Click += new System.EventHandler(this.m_mnuCommitAll_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 6;
            this.menuItem2.Text = "-";
            // 
            // m_mnuCopy
            // 
            this.m_mnuCopy.Index = 7;
            this.m_mnuCopy.Text = "���Ƶ���ʱģ��";
            this.m_mnuCopy.Click += new System.EventHandler(this.m_mnuCopy_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 8;
            this.menuItem3.Text = "����ҽ��";
            this.menuItem3.Click += new System.EventHandler(this.m_mnuCopyBihOrder_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 9;
            this.menuItem4.Text = "�޸�����ʱ��";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 10;
            this.menuItem5.Text = "�޸�ͣ��ʱ��";
            // 
            // m_ctxOtherBill
            // 
            this.m_ctxOtherBill.Popup += new System.EventHandler(this.m_ctxOtherBill_Popup);
            // 
            // cmdPrintOrder
            // 
            this.cmdPrintOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cmdPrintOrder.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cmdPrintOrder.DefaultScheme = true;
            this.cmdPrintOrder.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdPrintOrder.Font = new System.Drawing.Font("����", 9.5F);
            this.cmdPrintOrder.Hint = "";
            this.cmdPrintOrder.Location = new System.Drawing.Point(4, 64);
            this.cmdPrintOrder.Name = "cmdPrintOrder";
            this.cmdPrintOrder.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdPrintOrder.Size = new System.Drawing.Size(61, 29);
            this.cmdPrintOrder.TabIndex = 47;
            this.cmdPrintOrder.Text = "��ӡ";
            this.toolTip1.SetToolTip(this.cmdPrintOrder, "��ݼ�[Ctrl + P]");
            this.cmdPrintOrder.Click += new System.EventHandler(this.cmdPrintOrder_Click);
            // 
            // m_cmdStop
            // 
            this.m_cmdStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdStop.DefaultScheme = true;
            this.m_cmdStop.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdStop.Font = new System.Drawing.Font("����", 9.5F);
            this.m_cmdStop.Hint = "";
            this.m_cmdStop.Location = new System.Drawing.Point(125, 33);
            this.m_cmdStop.Name = "m_cmdStop";
            this.m_cmdStop.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdStop.Size = new System.Drawing.Size(61, 29);
            this.m_cmdStop.TabIndex = 45;
            this.m_cmdStop.Text = "ͣ�� X";
            this.toolTip1.SetToolTip(this.m_cmdStop, "��Ctrl������ҳ�����");
            this.m_cmdStop.Click += new System.EventHandler(this.m_cmdStop_Click);
            // 
            // m_lblOrderStatus
            // 
            this.m_lblOrderStatus.BackColor = System.Drawing.Color.Wheat;
            this.m_lblOrderStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblOrderStatus.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.m_lblOrderStatus.Location = new System.Drawing.Point(589, 25);
            this.m_lblOrderStatus.Multiline = true;
            this.m_lblOrderStatus.Name = "m_lblOrderStatus";
            this.m_lblOrderStatus.ReadOnly = true;
            this.m_lblOrderStatus.Size = new System.Drawing.Size(191, 33);
            this.m_lblOrderStatus.TabIndex = 58;
            this.m_lblOrderStatus.TabStop = false;
            this.toolTip1.SetToolTip(this.m_lblOrderStatus, "ҽ��״̬");
            this.m_lblOrderStatus.Visible = false;
            // 
            // m_chkStatus3
            // 
            this.m_chkStatus3.AutoSize = true;
            this.m_chkStatus3.Checked = true;
            this.m_chkStatus3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkStatus3.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_chkStatus3.Location = new System.Drawing.Point(333, 12);
            this.m_chkStatus3.Name = "m_chkStatus3";
            this.m_chkStatus3.Size = new System.Drawing.Size(68, 18);
            this.m_chkStatus3.TabIndex = 8;
            this.m_chkStatus3.Text = "��ֹͣ";
            this.toolTip1.SetToolTip(this.m_chkStatus3, "ֹͣ�����ֹͣ״̬��ҽ��");
            this.m_chkStatus3.CheckedChanged += new System.EventHandler(this.m_mthShowStatusChange);
            // 
            // m_chkStatus4
            // 
            this.m_chkStatus4.Location = new System.Drawing.Point(267, 9);
            this.m_chkStatus4.Name = "m_chkStatus4";
            this.m_chkStatus4.Size = new System.Drawing.Size(68, 24);
            this.m_chkStatus4.TabIndex = 7;
            this.m_chkStatus4.Text = "������";
            this.toolTip1.SetToolTip(this.m_chkStatus4, "ִ��״̬��ҽ��");
            this.m_chkStatus4.CheckedChanged += new System.EventHandler(this.m_mthShowStatusChange);
            // 
            // m_chkStatus2
            // 
            this.m_chkStatus2.Checked = true;
            this.m_chkStatus2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkStatus2.Location = new System.Drawing.Point(202, 9);
            this.m_chkStatus2.Name = "m_chkStatus2";
            this.m_chkStatus2.Size = new System.Drawing.Size(68, 25);
            this.m_chkStatus2.TabIndex = 6;
            this.m_chkStatus2.Text = "��ִ��";
            this.toolTip1.SetToolTip(this.m_chkStatus2, "ִ��״̬��ҽ��");
            this.m_chkStatus2.CheckedChanged += new System.EventHandler(this.m_mthShowStatusChange);
            // 
            // m_chkStatus1
            // 
            this.m_chkStatus1.Checked = true;
            this.m_chkStatus1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkStatus1.Location = new System.Drawing.Point(136, 9);
            this.m_chkStatus1.Name = "m_chkStatus1";
            this.m_chkStatus1.Size = new System.Drawing.Size(68, 24);
            this.m_chkStatus1.TabIndex = 5;
            this.m_chkStatus1.Text = "���ύ";
            this.toolTip1.SetToolTip(this.m_chkStatus1, "�ύ������ύ״̬��ҽ��");
            this.m_chkStatus1.CheckedChanged += new System.EventHandler(this.m_mthShowStatusChange);
            // 
            // m_chkStatus0
            // 
            this.m_chkStatus0.Checked = true;
            this.m_chkStatus0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkStatus0.Location = new System.Drawing.Point(70, 10);
            this.m_chkStatus0.Name = "m_chkStatus0";
            this.m_chkStatus0.Size = new System.Drawing.Size(78, 23);
            this.m_chkStatus0.TabIndex = 4;
            this.m_chkStatus0.Text = "δ�ύ";
            this.toolTip1.SetToolTip(this.m_chkStatus0, "�½����˻�״̬��ҽ��");
            this.m_chkStatus0.CheckedChanged += new System.EventHandler(this.m_mthShowStatusChange);
            // 
            // m_imgBackAlert
            // 
            this.m_imgBackAlert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_imgBackAlert.Image = ((System.Drawing.Image)(resources.GetObject("m_imgBackAlert.Image")));
            this.m_imgBackAlert.Location = new System.Drawing.Point(212, 6);
            this.m_imgBackAlert.Name = "m_imgBackAlert";
            this.m_imgBackAlert.Size = new System.Drawing.Size(17, 20);
            this.m_imgBackAlert.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_imgBackAlert.TabIndex = 121;
            this.m_imgBackAlert.TabStop = false;
            this.toolTip1.SetToolTip(this.m_imgBackAlert, "���˻ص�ҽ��!");
            this.m_imgBackAlert.Visible = false;
            this.m_imgBackAlert.Click += new System.EventHandler(this.m_imgBackAlert_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(977, 3);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(20, 24);
            this.pictureBox6.TabIndex = 117;
            this.pictureBox6.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox6, "����ҽ�� ��ע��ˢ�� ����");
            this.pictureBox6.Visible = false;
            this.pictureBox6.DoubleClick += new System.EventHandler(this.pictureBox6_DoubleClick);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(958, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 24);
            this.pictureBox3.TabIndex = 116;
            this.pictureBox3.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox3, "����ҽ�� ��ע��ˢ�� ����");
            this.pictureBox3.Visible = false;
            this.pictureBox3.DoubleClick += new System.EventHandler(this.pictureBox3_DoubleClick);
            // 
            // m_ctxGridMenu2
            // 
            this.m_ctxGridMenu2.BackColor = System.Drawing.Color.White;
            this.m_ctxGridMenu2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuDelete2,
            this.m_mnuBlankOut2,
            this.m_mnuStop2,
            this.m_mnuRetract2,
            this.toolStripSeparator1,
            this.m_mnuCommitAll2,
            this.toolStripSeparator2,
            this.m_mnuCopy2,
            this.m_MenuCopy,
            this.m_MenuPase,
            this.m_mnuCopyBihorder2,
            this.toolStripSeparator5,
            this.tsmiApp,
            this.tsmiBloodApp,
            this.toolStripSeparator3,
            this.m_MenuSeeBill,
            this.TSMenuTurnBack,
            this.m_mnuDoctorSign,
            this.menuItem7,
            this.menuItem8,
            this.menuItemStopAll,
            this.m_MenuChangeArea,
            this.m_MenuOPERATION,
            this.m_MenuATTACHTIMES_INT,
            this.m_MenuCHNAGEAMOUNT_INT,
            this.m_MenuOrderTemp,
            this.m_MenuOrderNornal,
            this.m_MenuReSortOrderNO,
            this.m_MenuOrderSTsign,
            this.m_MenuCheckBill,
            this.m_MenuViewBackReasion,
            this.m_MenuOutHis,
            this.m_MenuMoneyCount,
            this.tsbOpApplyNew,
            this.m_MenuSurgery,
            this.tsmiDrugInfo,
            this.toolStripSeparator4,
            this.tsmiTskjyhz_apply,
            this.tsmiTskjyhz_check,
            this.tsmiTskjyhz_response});
            this.m_ctxGridMenu2.Name = "m_ctxGridMenu2";
            this.m_ctxGridMenu2.Size = new System.Drawing.Size(222, 804);
            this.m_ctxGridMenu2.Opening += new System.ComponentModel.CancelEventHandler(this.m_ctxGridMenu2_Opening);
            // 
            // m_mnuDelete2
            // 
            this.m_mnuDelete2.Image = global::Order.Properties.Resources.delete;
            this.m_mnuDelete2.Name = "m_mnuDelete2";
            this.m_mnuDelete2.ShortcutKeyDisplayString = "F6";
            this.m_mnuDelete2.Size = new System.Drawing.Size(221, 22);
            this.m_mnuDelete2.Text = "ɾ��";
            this.m_mnuDelete2.Click += new System.EventHandler(this.m_mnuDelete_Click);
            // 
            // m_mnuBlankOut2
            // 
            this.m_mnuBlankOut2.Image = global::Order.Properties.Resources.warning;
            this.m_mnuBlankOut2.Name = "m_mnuBlankOut2";
            this.m_mnuBlankOut2.ShortcutKeyDisplayString = "";
            this.m_mnuBlankOut2.Size = new System.Drawing.Size(221, 22);
            this.m_mnuBlankOut2.Text = "����";
            this.m_mnuBlankOut2.Click += new System.EventHandler(this.m_mnuBlankOut_Click);
            // 
            // m_mnuStop2
            // 
            this.m_mnuStop2.Image = global::Order.Properties.Resources.stoporder;
            this.m_mnuStop2.Name = "m_mnuStop2";
            this.m_mnuStop2.ShortcutKeyDisplayString = "Ctrl+X";
            this.m_mnuStop2.Size = new System.Drawing.Size(221, 22);
            this.m_mnuStop2.Text = "ֹͣ";
            this.m_mnuStop2.Click += new System.EventHandler(this.m_mnuStop_Click);
            // 
            // m_mnuRetract2
            // 
            this.m_mnuRetract2.Image = global::Order.Properties.Resources.resort;
            this.m_mnuRetract2.Name = "m_mnuRetract2";
            this.m_mnuRetract2.ShortcutKeyDisplayString = "F9";
            this.m_mnuRetract2.Size = new System.Drawing.Size(221, 22);
            this.m_mnuRetract2.Text = "����";
            this.m_mnuRetract2.Click += new System.EventHandler(this.m_mnuRetract_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(218, 6);
            // 
            // m_mnuCommitAll2
            // 
            this.m_mnuCommitAll2.Image = global::Order.Properties.Resources.yes;
            this.m_mnuCommitAll2.Name = "m_mnuCommitAll2";
            this.m_mnuCommitAll2.ShortcutKeyDisplayString = "Ctrl+A";
            this.m_mnuCommitAll2.Size = new System.Drawing.Size(221, 22);
            this.m_mnuCommitAll2.Text = "�ύ�����½�ҽ��";
            this.m_mnuCommitAll2.Click += new System.EventHandler(this.m_mnuCommitAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(218, 6);
            // 
            // m_mnuCopy2
            // 
            this.m_mnuCopy2.Image = global::Order.Properties.Resources.databaseLarge;
            this.m_mnuCopy2.Name = "m_mnuCopy2";
            this.m_mnuCopy2.Size = new System.Drawing.Size(221, 22);
            this.m_mnuCopy2.Text = "���Ƶ���ʱģ��";
            this.m_mnuCopy2.Click += new System.EventHandler(this.m_mnuCopy_Click);
            // 
            // m_MenuCopy
            // 
            this.m_MenuCopy.Image = global::Order.Properties.Resources.copy1;
            this.m_MenuCopy.Name = "m_MenuCopy";
            this.m_MenuCopy.ShortcutKeyDisplayString = "";
            this.m_MenuCopy.Size = new System.Drawing.Size(221, 22);
            this.m_MenuCopy.Text = "����";
            this.m_MenuCopy.Click += new System.EventHandler(this.m_MenuCopy_Click);
            // 
            // m_MenuPase
            // 
            this.m_MenuPase.Image = global::Order.Properties.Resources.parse;
            this.m_MenuPase.Name = "m_MenuPase";
            this.m_MenuPase.ShortcutKeyDisplayString = "";
            this.m_MenuPase.Size = new System.Drawing.Size(221, 22);
            this.m_MenuPase.Text = "ճ��";
            this.m_MenuPase.Click += new System.EventHandler(this.m_MenuPase_Click);
            // 
            // m_mnuCopyBihorder2
            // 
            this.m_mnuCopyBihorder2.Image = global::Order.Properties.Resources.copyorder;
            this.m_mnuCopyBihorder2.Name = "m_mnuCopyBihorder2";
            this.m_mnuCopyBihorder2.Size = new System.Drawing.Size(221, 22);
            this.m_mnuCopyBihorder2.Text = "����ҽ��";
            this.m_mnuCopyBihorder2.Visible = false;
            this.m_mnuCopyBihorder2.Click += new System.EventHandler(this.m_mnuCopyBihOrder_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(218, 6);
            // 
            // tsmiApp
            // 
            this.tsmiApp.Image = ((System.Drawing.Image)(resources.GetObject("tsmiApp.Image")));
            this.tsmiApp.Name = "tsmiApp";
            this.tsmiApp.ShortcutKeyDisplayString = "Ctrl+E";
            this.tsmiApp.Size = new System.Drawing.Size(221, 22);
            this.tsmiApp.Text = "�������뵥";
            this.tsmiApp.Click += new System.EventHandler(this.tsmiApp_Click);
            // 
            // tsmiBloodApp
            // 
            this.tsmiBloodApp.Image = ((System.Drawing.Image)(resources.GetObject("tsmiBloodApp.Image")));
            this.tsmiBloodApp.Name = "tsmiBloodApp";
            this.tsmiBloodApp.Size = new System.Drawing.Size(221, 22);
            this.tsmiBloodApp.Text = "�ٴ���Ѫ���뵥";
            this.tsmiBloodApp.Click += new System.EventHandler(this.tsmiBloodApp_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(218, 6);
            // 
            // m_MenuSeeBill
            // 
            this.m_MenuSeeBill.Name = "m_MenuSeeBill";
            this.m_MenuSeeBill.ShortcutKeyDisplayString = "Ctrl+S";
            this.m_MenuSeeBill.Size = new System.Drawing.Size(221, 22);
            this.m_MenuSeeBill.Text = "���˱����ѯ";
            this.m_MenuSeeBill.Click += new System.EventHandler(this.m_MenuSeeBill_Click);
            // 
            // TSMenuTurnBack
            // 
            this.TSMenuTurnBack.Image = global::Order.Properties.Resources.restore;
            this.TSMenuTurnBack.Name = "TSMenuTurnBack";
            this.TSMenuTurnBack.Size = new System.Drawing.Size(221, 22);
            this.TSMenuTurnBack.Text = "���ϻָ�";
            this.TSMenuTurnBack.Click += new System.EventHandler(this.TSMenuTurnBack_Click);
            // 
            // m_mnuDoctorSign
            // 
            this.m_mnuDoctorSign.Image = global::Order.Properties.Resources.sign;
            this.m_mnuDoctorSign.Name = "m_mnuDoctorSign";
            this.m_mnuDoctorSign.Size = new System.Drawing.Size(221, 22);
            this.m_mnuDoctorSign.Text = "ҽ��ǩ��";
            this.m_mnuDoctorSign.Click += new System.EventHandler(this.m_mnuDoctorSign_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Image = global::Order.Properties.Resources.stoptime;
            this.menuItem7.Name = "menuItem7";
            this.menuItem7.Size = new System.Drawing.Size(221, 22);
            this.menuItem7.Text = "�޸�����ʱ��";
            this.menuItem7.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Image = global::Order.Properties.Resources.starttime;
            this.menuItem8.Name = "menuItem8";
            this.menuItem8.Size = new System.Drawing.Size(221, 22);
            this.menuItem8.Text = "�޸�ͣ��ʱ��";
            this.menuItem8.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItemStopAll
            // 
            this.menuItemStopAll.Name = "menuItemStopAll";
            this.menuItemStopAll.Size = new System.Drawing.Size(221, 22);
            this.menuItemStopAll.Text = "ֹͣ����ҽ��";
            this.menuItemStopAll.Click += new System.EventHandler(this.menuItemStopAll_Click);
            // 
            // m_MenuChangeArea
            // 
            this.m_MenuChangeArea.Name = "m_MenuChangeArea";
            this.m_MenuChangeArea.Size = new System.Drawing.Size(221, 22);
            this.m_MenuChangeArea.Text = "ת��ҽ��";
            this.m_MenuChangeArea.Click += new System.EventHandler(this.m_MenuChangeArea_Click);
            // 
            // m_MenuOPERATION
            // 
            this.m_MenuOPERATION.Image = global::Order.Properties.Resources.operate;
            this.m_MenuOPERATION.Name = "m_MenuOPERATION";
            this.m_MenuOPERATION.Size = new System.Drawing.Size(221, 22);
            this.m_MenuOPERATION.Text = "����ҽ��";
            this.m_MenuOPERATION.Click += new System.EventHandler(this.m_MenuOPERATION_Click);
            // 
            // m_MenuATTACHTIMES_INT
            // 
            this.m_MenuATTACHTIMES_INT.Checked = true;
            this.m_MenuATTACHTIMES_INT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_MenuATTACHTIMES_INT.Image = global::Order.Properties.Resources.editone;
            this.m_MenuATTACHTIMES_INT.Name = "m_MenuATTACHTIMES_INT";
            this.m_MenuATTACHTIMES_INT.Size = new System.Drawing.Size(221, 22);
            this.m_MenuATTACHTIMES_INT.Text = "�޸Ĳ���";
            this.m_MenuATTACHTIMES_INT.Click += new System.EventHandler(this.m_MenuATTACHTIMES_INT_Click);
            // 
            // m_MenuCHNAGEAMOUNT_INT
            // 
            this.m_MenuCHNAGEAMOUNT_INT.Name = "m_MenuCHNAGEAMOUNT_INT";
            this.m_MenuCHNAGEAMOUNT_INT.Size = new System.Drawing.Size(221, 22);
            this.m_MenuCHNAGEAMOUNT_INT.Text = "�޸�����";
            this.m_MenuCHNAGEAMOUNT_INT.Click += new System.EventHandler(this.m_MenuCHNAGEAMOUNT_INT_Click);
            // 
            // m_MenuOrderTemp
            // 
            this.m_MenuOrderTemp.Name = "m_MenuOrderTemp";
            this.m_MenuOrderTemp.Size = new System.Drawing.Size(221, 22);
            this.m_MenuOrderTemp.Text = "������ҽ��ģ��";
            this.m_MenuOrderTemp.Click += new System.EventHandler(this.m_MenuOrderTemp_Click);
            // 
            // m_MenuOrderNornal
            // 
            this.m_MenuOrderNornal.Name = "m_MenuOrderNornal";
            this.m_MenuOrderNornal.Size = new System.Drawing.Size(221, 22);
            this.m_MenuOrderNornal.Text = "���ӵ�������Ŀ";
            this.m_MenuOrderNornal.Click += new System.EventHandler(this.m_MenuOrderNornal_Click);
            // 
            // m_MenuReSortOrderNO
            // 
            this.m_MenuReSortOrderNO.Name = "m_MenuReSortOrderNO";
            this.m_MenuReSortOrderNO.Size = new System.Drawing.Size(221, 22);
            this.m_MenuReSortOrderNO.Text = "��������";
            this.m_MenuReSortOrderNO.Click += new System.EventHandler(this.m_MenuReSortOrderNO_Click);
            // 
            // m_MenuOrderSTsign
            // 
            this.m_MenuOrderSTsign.Name = "m_MenuOrderSTsign";
            this.m_MenuOrderSTsign.Size = new System.Drawing.Size(221, 22);
            this.m_MenuOrderSTsign.Text = "����ִ��ҽ��(st��ʶ)";
            this.m_MenuOrderSTsign.Click += new System.EventHandler(this.m_MenuOrderSTsign_Click);
            // 
            // m_MenuCheckBill
            // 
            this.m_MenuCheckBill.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuCheckBillEdit,
            this.m_MenuCheckBillView});
            this.m_MenuCheckBill.Name = "m_MenuCheckBill";
            this.m_MenuCheckBill.Size = new System.Drawing.Size(221, 22);
            this.m_MenuCheckBill.Text = "������뵥";
            // 
            // m_MenuCheckBillEdit
            // 
            this.m_MenuCheckBillEdit.Name = "m_MenuCheckBillEdit";
            this.m_MenuCheckBillEdit.Size = new System.Drawing.Size(100, 22);
            this.m_MenuCheckBillEdit.Text = "�༭";
            this.m_MenuCheckBillEdit.Click += new System.EventHandler(this.m_MenuCheckBillEdit_Click);
            // 
            // m_MenuCheckBillView
            // 
            this.m_MenuCheckBillView.Name = "m_MenuCheckBillView";
            this.m_MenuCheckBillView.Size = new System.Drawing.Size(100, 22);
            this.m_MenuCheckBillView.Text = "�鿴";
            this.m_MenuCheckBillView.Click += new System.EventHandler(this.m_MenuCheckBillView_Click);
            // 
            // m_MenuViewBackReasion
            // 
            this.m_MenuViewBackReasion.Name = "m_MenuViewBackReasion";
            this.m_MenuViewBackReasion.Size = new System.Drawing.Size(221, 22);
            this.m_MenuViewBackReasion.Text = "�鿴�˻�ԭ��";
            this.m_MenuViewBackReasion.Click += new System.EventHandler(this.m_MenuViewBackReasion_Click);
            // 
            // m_MenuOutHis
            // 
            this.m_MenuOutHis.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuOutToday,
            this.m_MenuOutTomorrow});
            this.m_MenuOutHis.Name = "m_MenuOutHis";
            this.m_MenuOutHis.Size = new System.Drawing.Size(221, 22);
            this.m_MenuOutHis.Text = "��Ժҽ��";
            // 
            // m_MenuOutToday
            // 
            this.m_MenuOutToday.Name = "m_MenuOutToday";
            this.m_MenuOutToday.Size = new System.Drawing.Size(124, 22);
            this.m_MenuOutToday.Text = "�����Ժ";
            this.m_MenuOutToday.Click += new System.EventHandler(this.m_MenuOutToday_Click);
            // 
            // m_MenuOutTomorrow
            // 
            this.m_MenuOutTomorrow.Name = "m_MenuOutTomorrow";
            this.m_MenuOutTomorrow.Size = new System.Drawing.Size(124, 22);
            this.m_MenuOutTomorrow.Text = "�����Ժ";
            this.m_MenuOutTomorrow.Click += new System.EventHandler(this.m_MenuOutTomorrow_Click);
            // 
            // m_MenuMoneyCount
            // 
            this.m_MenuMoneyCount.Name = "m_MenuMoneyCount";
            this.m_MenuMoneyCount.ShortcutKeyDisplayString = "Ctrl+W";
            this.m_MenuMoneyCount.Size = new System.Drawing.Size(221, 22);
            this.m_MenuMoneyCount.Text = "�¿�ҽ�����úϼ�";
            this.m_MenuMoneyCount.Click += new System.EventHandler(this.m_MenuMoneyCount_Click);
            // 
            // tsbOpApplyNew
            // 
            this.tsbOpApplyNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpApplyNew.Image")));
            this.tsbOpApplyNew.Name = "tsbOpApplyNew";
            this.tsbOpApplyNew.Size = new System.Drawing.Size(221, 22);
            this.tsbOpApplyNew.Text = "�������뵥-��";
            this.tsbOpApplyNew.Click += new System.EventHandler(this.tsbOpApplyNew_Click);
            // 
            // m_MenuSurgery
            // 
            this.m_MenuSurgery.Name = "m_MenuSurgery";
            this.m_MenuSurgery.Size = new System.Drawing.Size(221, 22);
            this.m_MenuSurgery.Text = "�������뵥-��";
            this.m_MenuSurgery.Visible = false;
            this.m_MenuSurgery.Click += new System.EventHandler(this.m_MenuSurgery_Click);
            // 
            // tsmiDrugInfo
            // 
            this.tsmiDrugInfo.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDrugInfo.Image")));
            this.tsmiDrugInfo.Name = "tsmiDrugInfo";
            this.tsmiDrugInfo.Size = new System.Drawing.Size(221, 22);
            this.tsmiDrugInfo.Text = "ҩƷ˵����";
            this.tsmiDrugInfo.Click += new System.EventHandler(this.tsmiDrugInfo_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(218, 6);
            // 
            // tsmiTskjyhz_apply
            // 
            this.tsmiTskjyhz_apply.Image = ((System.Drawing.Image)(resources.GetObject("tsmiTskjyhz_apply.Image")));
            this.tsmiTskjyhz_apply.Name = "tsmiTskjyhz_apply";
            this.tsmiTskjyhz_apply.Size = new System.Drawing.Size(221, 22);
            this.tsmiTskjyhz_apply.Text = "���⿹��ҩ����-����";
            this.tsmiTskjyhz_apply.Click += new System.EventHandler(this.tsmiTskjyhz_apply_Click);
            // 
            // tsmiTskjyhz_check
            // 
            this.tsmiTskjyhz_check.Image = ((System.Drawing.Image)(resources.GetObject("tsmiTskjyhz_check.Image")));
            this.tsmiTskjyhz_check.Name = "tsmiTskjyhz_check";
            this.tsmiTskjyhz_check.Size = new System.Drawing.Size(221, 22);
            this.tsmiTskjyhz_check.Text = "���⿹��ҩ����-���";
            this.tsmiTskjyhz_check.Click += new System.EventHandler(this.tsmiTskjyhz_check_Click);
            // 
            // tsmiTskjyhz_response
            // 
            this.tsmiTskjyhz_response.Image = ((System.Drawing.Image)(resources.GetObject("tsmiTskjyhz_response.Image")));
            this.tsmiTskjyhz_response.Name = "tsmiTskjyhz_response";
            this.tsmiTskjyhz_response.Size = new System.Drawing.Size(221, 22);
            this.tsmiTskjyhz_response.Text = "���⿹��ҩ����-�ظ�";
            this.tsmiTskjyhz_response.Click += new System.EventHandler(this.tsmiTskjyhz_response_Click);
            // 
            // toolTip2
            // 
            this.toolTip2.AutoPopDelay = 1000;
            this.toolTip2.InitialDelay = 200;
            this.toolTip2.ReshowDelay = 100;
            // 
            // collapsibleSplitter1
            // 
            this.collapsibleSplitter1.AnimationDelay = 20;
            this.collapsibleSplitter1.AnimationStep = 20;
            this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.Etched;
            this.collapsibleSplitter1.ControlToHide = this.panel2;
            this.collapsibleSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.collapsibleSplitter1.ExpandParentForm = false;
            this.collapsibleSplitter1.Location = new System.Drawing.Point(0, 547);
            this.collapsibleSplitter1.MinExtra = 200;
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.Size = new System.Drawing.Size(1023, 8);
            this.collapsibleSplitter1.TabIndex = 59;
            this.collapsibleSplitter1.TabStop = false;
            this.toolTip2.SetToolTip(this.collapsibleSplitter1, "��ʾ\\����ҽ���༭��");
            this.collapsibleSplitter1.UseAnimations = false;
            this.collapsibleSplitter1.VisualStyle = com.digitalwave.Utility.Controls.VisualStyles.XP;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.m_imgBackAlert);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.m_txtCREATEAREA);
            this.panel2.Controls.Add(this.pictureBox6);
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.m_cboOrderList);
            this.panel2.Controls.Add(this.m_lblEditState);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.m_lblOrderStatus);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.m_chkNeedFeel);
            this.panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 555);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1023, 36);
            this.panel2.TabIndex = 100;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("����", 9.5F);
            this.label10.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label10.Location = new System.Drawing.Point(468, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 120;
            this.label10.Text = "��������:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtCREATEAREA
            // 
            this.m_txtCREATEAREA.Location = new System.Drawing.Point(535, 6);
            this.m_txtCREATEAREA.Name = "m_txtCREATEAREA";
            this.m_txtCREATEAREA.Size = new System.Drawing.Size(157, 23);
            this.m_txtCREATEAREA.TabIndex = 119;
            this.m_txtCREATEAREA.m_evtFindItem += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_OnFindItem(this.m_txtCREATEAREA_m_evtFindItem);
            this.m_txtCREATEAREA.m_evtSelectItem += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_OnSelectItem(this.m_txtCREATEAREA_m_evtSelectItem);
            this.m_txtCREATEAREA.m_evtInitListView += new com.digitalwave.iCare.BIHOrder.Control.EventHandler_InitListView(this.m_txtCREATEAREA_m_evtInitListView);
            this.m_txtCREATEAREA.DoubleClick += new System.EventHandler(this.m_txtCREATEAREA_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("����", 9.5F);
            this.label4.Location = new System.Drawing.Point(236, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 61;
            this.label4.Text = "״ ̬:";
            // 
            // m_cboOrderList
            // 
            this.m_cboOrderList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboOrderList.FormattingEnabled = true;
            this.m_cboOrderList.Location = new System.Drawing.Point(285, 6);
            this.m_cboOrderList.MaxDropDownItems = 12;
            this.m_cboOrderList.Name = "m_cboOrderList";
            this.m_cboOrderList.Size = new System.Drawing.Size(181, 22);
            this.m_cboOrderList.TabIndex = 60;
            this.m_cboOrderList.SelectionChangeCommitted += new System.EventHandler(this.m_cboOrderList_SelectionChangeCommitted);
            // 
            // m_lblEditState
            // 
            this.m_lblEditState.AutoSize = true;
            this.m_lblEditState.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.m_lblEditState.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_lblEditState.Font = new System.Drawing.Font("����", 9.5F, System.Drawing.FontStyle.Bold);
            this.m_lblEditState.ForeColor = System.Drawing.Color.Red;
            this.m_lblEditState.Location = new System.Drawing.Point(708, 7);
            this.m_lblEditState.Name = "m_lblEditState";
            this.m_lblEditState.Size = new System.Drawing.Size(63, 13);
            this.m_lblEditState.TabIndex = 44;
            this.m_lblEditState.Text = "ҽ��״̬";
            this.m_lblEditState.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.m_cobOrderCate);
            this.groupBox4.Controls.Add(this.seachClass);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Location = new System.Drawing.Point(1, -7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(203, 38);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            // 
            // m_cobOrderCate
            // 
            this.m_cobOrderCate.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.m_cobOrderCate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cobOrderCate.Location = new System.Drawing.Point(37, 32);
            this.m_cobOrderCate.Name = "m_cobOrderCate";
            this.m_cobOrderCate.Size = new System.Drawing.Size(110, 22);
            this.m_cobOrderCate.TabIndex = 2;
            this.m_cobOrderCate.Visible = false;
            this.m_cobOrderCate.SelectedIndexChanged += new System.EventHandler(this.m_mthShowStatusChange);
            // 
            // seachClass
            // 
            this.seachClass.Cursor = System.Windows.Forms.Cursors.Default;
            this.seachClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.seachClass.FormattingEnabled = true;
            this.seachClass.Items.AddRange(new object[] {
            "ƴ����",
            "�����",
            "��Ŀ����",
            "�û�����"});
            this.seachClass.Location = new System.Drawing.Point(59, 12);
            this.seachClass.Name = "seachClass";
            this.seachClass.Size = new System.Drawing.Size(133, 22);
            this.seachClass.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("����", 9.5F);
            this.label16.Location = new System.Drawing.Point(7, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 13);
            this.label16.TabIndex = 4;
            this.label16.Text = "��  ѯ:";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_rdoToday);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.m_rdoAllDay);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(787, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(167, 37);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            // 
            // m_rdoToday
            // 
            this.m_rdoToday.Font = new System.Drawing.Font("����", 9.5F);
            this.m_rdoToday.Location = new System.Drawing.Point(102, 9);
            this.m_rdoToday.Name = "m_rdoToday";
            this.m_rdoToday.Size = new System.Drawing.Size(53, 24);
            this.m_rdoToday.TabIndex = 101;
            this.m_rdoToday.Text = "����";
            this.m_rdoToday.CheckedChanged += new System.EventHandler(this.m_mthShowConditionChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("����", 9.5F);
            this.label2.Location = new System.Drawing.Point(4, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "��Χ";
            // 
            // m_rdoAllDay
            // 
            this.m_rdoAllDay.Checked = true;
            this.m_rdoAllDay.Font = new System.Drawing.Font("����", 9.5F);
            this.m_rdoAllDay.Location = new System.Drawing.Point(44, 9);
            this.m_rdoAllDay.Name = "m_rdoAllDay";
            this.m_rdoAllDay.Size = new System.Drawing.Size(53, 24);
            this.m_rdoAllDay.TabIndex = 100;
            this.m_rdoAllDay.TabStop = true;
            this.m_rdoAllDay.Text = "����";
            this.m_rdoAllDay.CheckedChanged += new System.EventHandler(this.m_mthShowConditionChange);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.alertLight1);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.m_rdoTempType);
            this.groupBox2.Controls.Add(this.m_rdoLongType);
            this.groupBox2.Controls.Add(this.m_rdoAllType);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.Location = new System.Drawing.Point(769, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 38);
            this.groupBox2.TabIndex = 52;
            this.groupBox2.TabStop = false;
            this.groupBox2.Visible = false;
            // 
            // alertLight1
            // 
            this.alertLight1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.alertLight1.IsTabu = true;
            this.alertLight1.Location = new System.Drawing.Point(17, -7);
            this.alertLight1.Name = "alertLight1";
            this.alertLight1.Size = new System.Drawing.Size(33, 21);
            this.alertLight1.TabIndex = 3;
            this.alertLight1.Visible = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(153, 15);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(53, 18);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.Text = "��ҩ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_chkStatus3);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.m_chkStatus4);
            this.groupBox3.Controls.Add(this.m_chkStatus2);
            this.groupBox3.Controls.Add(this.m_chkStatus1);
            this.groupBox3.Controls.Add(this.m_chkStatus0);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox3.Location = new System.Drawing.Point(218, 30);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(411, 37);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Visible = false;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(6, 14);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 14);
            this.label17.TabIndex = 5;
            this.label17.Text = "״    ̬";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "����";
            // 
            // m_rdoTempType
            // 
            this.m_rdoTempType.Location = new System.Drawing.Point(111, 12);
            this.m_rdoTempType.Name = "m_rdoTempType";
            this.m_rdoTempType.Size = new System.Drawing.Size(36, 24);
            this.m_rdoTempType.TabIndex = 3;
            this.m_rdoTempType.Text = "��";
            this.m_rdoTempType.CheckedChanged += new System.EventHandler(this.m_mthShowConditionChange);
            // 
            // m_rdoLongType
            // 
            this.m_rdoLongType.Location = new System.Drawing.Point(75, 12);
            this.m_rdoLongType.Name = "m_rdoLongType";
            this.m_rdoLongType.Size = new System.Drawing.Size(35, 24);
            this.m_rdoLongType.TabIndex = 2;
            this.m_rdoLongType.Text = "��";
            this.m_rdoLongType.CheckedChanged += new System.EventHandler(this.m_mthShowConditionChange);
            // 
            // m_rdoAllType
            // 
            this.m_rdoAllType.Checked = true;
            this.m_rdoAllType.Location = new System.Drawing.Point(41, 12);
            this.m_rdoAllType.Name = "m_rdoAllType";
            this.m_rdoAllType.Size = new System.Drawing.Size(34, 24);
            this.m_rdoAllType.TabIndex = 1;
            this.m_rdoAllType.TabStop = true;
            this.m_rdoAllType.Text = "ȫ";
            this.m_rdoAllType.CheckedChanged += new System.EventHandler(this.m_mthShowConditionChange);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(1, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(288, 89);
            this.label3.TabIndex = 0;
            // 
            // m_chkNeedFeel
            // 
            this.m_chkNeedFeel.AutoSize = true;
            this.m_chkNeedFeel.Location = new System.Drawing.Point(786, 16);
            this.m_chkNeedFeel.Name = "m_chkNeedFeel";
            this.m_chkNeedFeel.Size = new System.Drawing.Size(68, 18);
            this.m_chkNeedFeel.TabIndex = 3;
            this.m_chkNeedFeel.Text = "��Ƥ��";
            this.m_chkNeedFeel.Visible = false;
            this.m_chkNeedFeel.CheckedChanged += new System.EventHandler(this.m_mthShowConditionChange);
            // 
            // cmbApply
            // 
            this.cmbApply.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApply.Location = new System.Drawing.Point(771, 80);
            this.cmbApply.Name = "cmbApply";
            this.cmbApply.Size = new System.Drawing.Size(112, 22);
            this.cmbApply.TabIndex = 68;
            this.cmbApply.Visible = false;
            this.cmbApply.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbApply_KeyDown);
            // 
            // m_txtBackReason
            // 
            this.m_txtBackReason.BackColor = System.Drawing.Color.Snow;
            this.m_txtBackReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtBackReason.Location = new System.Drawing.Point(184, 216);
            this.m_txtBackReason.Multiline = true;
            this.m_txtBackReason.Name = "m_txtBackReason";
            this.m_txtBackReason.ReadOnly = true;
            this.m_txtBackReason.Size = new System.Drawing.Size(292, 48);
            this.m_txtBackReason.TabIndex = 44;
            this.m_txtBackReason.Visible = false;
            // 
            // btCreatBill
            // 
            this.btCreatBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btCreatBill.DefaultScheme = true;
            this.btCreatBill.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btCreatBill.Font = new System.Drawing.Font("������", 7F);
            this.btCreatBill.Hint = "";
            this.btCreatBill.Location = new System.Drawing.Point(884, 80);
            this.btCreatBill.Name = "btCreatBill";
            this.btCreatBill.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btCreatBill.Size = new System.Drawing.Size(24, 22);
            this.btCreatBill.TabIndex = 69;
            this.btCreatBill.Text = ">>";
            this.btCreatBill.Visible = false;
            this.btCreatBill.Click += new System.EventHandler(this.btCreatBill_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.m_dtvOrder);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 68);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1023, 377);
            this.panel4.TabIndex = 66;
            // 
            // m_dtvOrder
            // 
            this.m_dtvOrder.AllowUserToAddRows = false;
            this.m_dtvOrder.AllowUserToDeleteRows = false;
            this.m_dtvOrder.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_dtvOrder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.m_dtvOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dtvOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dtv_ExecuteType,
            this.m_dtPOSTDATE_DAT,
            this.CREATOR_CHR,
            this.ASSESSORFOREXEC_CHR,
            this.dtv_RecipeNo,
            this.dtv_Name,
            this.dtv_Dosage,
            this.dtv_UseType,
            this.dtv_Freq,
            this.dtv_REMARK,
            this.dtv_FinishDate,
            this.dtv_Stoper,
            this.ASSESSORFORSTOP_CHR,
            this.ATTACHTIMES_INT,
            this.STATUS_INT,
            this.RATETYPE_INT,
            this.isOps,
            this.MedicareTypeName,
            this.dtv_Get,
            this.dtv_Sum,
            this.dtv_StartDate,
            this.dtv_Executor,
            this.dtv_DELETE_DAT,
            this.dtv_DELETERNAME_VCHR,
            this.viewname_vchr,
            this.dtv_method,
            this.dtv_NO,
            this.dtv_OUTGETMEDDAYS_INT,
            this.dtv_CREATEAREA_Name,
            this.dtv_DOCTOR_VCHR,
            this.dtv_DOCTOR_SIGN,
            this.CREATEDATE_DAT,
            this.dtv_ChangedID,
            this.dtv_ChangedDate,
            this.m_dtStartDate,
            this.m_dtvSENDBACKER_CHR});
            this.m_dtvOrder.ContextMenuStrip = this.m_ctxGridMenu2;
            this.m_dtvOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dtvOrder.Location = new System.Drawing.Point(0, 0);
            this.m_dtvOrder.Name = "m_dtvOrder";
            this.m_dtvOrder.ReadOnly = true;
            this.m_dtvOrder.RowHeadersVisible = false;
            this.m_dtvOrder.RowTemplate.Height = 28;
            this.m_dtvOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_dtvOrder.Size = new System.Drawing.Size(527, 377);
            this.m_dtvOrder.TabIndex = 43;
            this.m_dtvOrder.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.m_dtvOrder_CellMouseClick);
            this.m_dtvOrder.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.m_dtvOrder_ColumnHeaderMouseClick);
            this.m_dtvOrder.CurrentCellChanged += new System.EventHandler(this.m_dtgOrder_m_evtCurrentCellChanged);
            this.m_dtvOrder.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.m_dtvOrder_DataError);
            this.m_dtvOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_dtvOrder_KeyDown);
            // 
            // dtv_ExecuteType
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dtv_ExecuteType.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtv_ExecuteType.HeaderText = "����";
            this.dtv_ExecuteType.Name = "dtv_ExecuteType";
            this.dtv_ExecuteType.ReadOnly = true;
            this.dtv_ExecuteType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_ExecuteType.Width = 42;
            // 
            // m_dtPOSTDATE_DAT
            // 
            this.m_dtPOSTDATE_DAT.HeaderText = "����ʱ��";
            this.m_dtPOSTDATE_DAT.Name = "m_dtPOSTDATE_DAT";
            this.m_dtPOSTDATE_DAT.ReadOnly = true;
            this.m_dtPOSTDATE_DAT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_dtPOSTDATE_DAT.Width = 85;
            // 
            // CREATOR_CHR
            // 
            this.CREATOR_CHR.HeaderText = "������";
            this.CREATOR_CHR.Name = "CREATOR_CHR";
            this.CREATOR_CHR.ReadOnly = true;
            this.CREATOR_CHR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CREATOR_CHR.Width = 55;
            // 
            // ASSESSORFOREXEC_CHR
            // 
            this.ASSESSORFOREXEC_CHR.HeaderText = "������";
            this.ASSESSORFOREXEC_CHR.Name = "ASSESSORFOREXEC_CHR";
            this.ASSESSORFOREXEC_CHR.ReadOnly = true;
            this.ASSESSORFOREXEC_CHR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ASSESSORFOREXEC_CHR.Width = 55;
            // 
            // dtv_RecipeNo
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("����", 10F);
            this.dtv_RecipeNo.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtv_RecipeNo.HeaderText = "��";
            this.dtv_RecipeNo.Name = "dtv_RecipeNo";
            this.dtv_RecipeNo.ReadOnly = true;
            this.dtv_RecipeNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_RecipeNo.Width = 30;
            // 
            // dtv_Name
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(239)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("����", 10F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dtv_Name.DefaultCellStyle = dataGridViewCellStyle4;
            this.dtv_Name.HeaderText = "ҽ������";
            this.dtv_Name.Name = "dtv_Name";
            this.dtv_Name.ReadOnly = true;
            this.dtv_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_Name.Width = 365;
            // 
            // dtv_Dosage
            // 
            this.dtv_Dosage.HeaderText = "����";
            this.dtv_Dosage.Name = "dtv_Dosage";
            this.dtv_Dosage.ReadOnly = true;
            this.dtv_Dosage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_Dosage.Visible = false;
            this.dtv_Dosage.Width = 60;
            // 
            // dtv_UseType
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dtv_UseType.DefaultCellStyle = dataGridViewCellStyle5;
            this.dtv_UseType.HeaderText = "�÷�";
            this.dtv_UseType.Name = "dtv_UseType";
            this.dtv_UseType.ReadOnly = true;
            this.dtv_UseType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_UseType.Visible = false;
            this.dtv_UseType.Width = 60;
            // 
            // dtv_Freq
            // 
            this.dtv_Freq.HeaderText = "Ƶ��";
            this.dtv_Freq.Name = "dtv_Freq";
            this.dtv_Freq.ReadOnly = true;
            this.dtv_Freq.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_Freq.Visible = false;
            this.dtv_Freq.Width = 60;
            // 
            // dtv_REMARK
            // 
            this.dtv_REMARK.HeaderText = "˵��";
            this.dtv_REMARK.Name = "dtv_REMARK";
            this.dtv_REMARK.ReadOnly = true;
            this.dtv_REMARK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_REMARK.Width = 80;
            // 
            // dtv_FinishDate
            // 
            this.dtv_FinishDate.HeaderText = "ͣ��ʱ��";
            this.dtv_FinishDate.Name = "dtv_FinishDate";
            this.dtv_FinishDate.ReadOnly = true;
            this.dtv_FinishDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_FinishDate.Width = 85;
            // 
            // dtv_Stoper
            // 
            this.dtv_Stoper.HeaderText = "ͣ����";
            this.dtv_Stoper.Name = "dtv_Stoper";
            this.dtv_Stoper.ReadOnly = true;
            this.dtv_Stoper.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_Stoper.Width = 55;
            // 
            // ASSESSORFORSTOP_CHR
            // 
            this.ASSESSORFORSTOP_CHR.HeaderText = "������";
            this.ASSESSORFORSTOP_CHR.Name = "ASSESSORFORSTOP_CHR";
            this.ASSESSORFORSTOP_CHR.ReadOnly = true;
            this.ASSESSORFORSTOP_CHR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ASSESSORFORSTOP_CHR.Width = 55;
            // 
            // ATTACHTIMES_INT
            // 
            this.ATTACHTIMES_INT.HeaderText = "����";
            this.ATTACHTIMES_INT.Name = "ATTACHTIMES_INT";
            this.ATTACHTIMES_INT.ReadOnly = true;
            this.ATTACHTIMES_INT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ATTACHTIMES_INT.Width = 30;
            // 
            // STATUS_INT
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.STATUS_INT.DefaultCellStyle = dataGridViewCellStyle6;
            this.STATUS_INT.HeaderText = "ҽ��״̬";
            this.STATUS_INT.Name = "STATUS_INT";
            this.STATUS_INT.ReadOnly = true;
            this.STATUS_INT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.STATUS_INT.Width = 70;
            // 
            // RATETYPE_INT
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RATETYPE_INT.DefaultCellStyle = dataGridViewCellStyle7;
            this.RATETYPE_INT.HeaderText = "ҩƷ��Դ";
            this.RATETYPE_INT.Name = "RATETYPE_INT";
            this.RATETYPE_INT.ReadOnly = true;
            this.RATETYPE_INT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RATETYPE_INT.Width = 45;
            // 
            // isOps
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.isOps.DefaultCellStyle = dataGridViewCellStyle8;
            this.isOps.HeaderText = "��ͷҽ��";
            this.isOps.Name = "isOps";
            this.isOps.ReadOnly = true;
            this.isOps.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.isOps.Width = 42;
            // 
            // MedicareTypeName
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MedicareTypeName.DefaultCellStyle = dataGridViewCellStyle9;
            this.MedicareTypeName.HeaderText = "ҽ������";
            this.MedicareTypeName.Name = "MedicareTypeName";
            this.MedicareTypeName.ReadOnly = true;
            this.MedicareTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MedicareTypeName.Width = 45;
            // 
            // dtv_Get
            // 
            this.dtv_Get.HeaderText = "����";
            this.dtv_Get.Name = "dtv_Get";
            this.dtv_Get.ReadOnly = true;
            this.dtv_Get.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_Get.Width = 60;
            // 
            // dtv_Sum
            // 
            this.dtv_Sum.HeaderText = "����";
            this.dtv_Sum.Name = "dtv_Sum";
            this.dtv_Sum.ReadOnly = true;
            this.dtv_Sum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_Sum.Width = 60;
            // 
            // dtv_StartDate
            // 
            this.dtv_StartDate.HeaderText = "ִ��ʱ��";
            this.dtv_StartDate.Name = "dtv_StartDate";
            this.dtv_StartDate.ReadOnly = true;
            this.dtv_StartDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_StartDate.Width = 120;
            // 
            // dtv_Executor
            // 
            this.dtv_Executor.HeaderText = "ִ����";
            this.dtv_Executor.Name = "dtv_Executor";
            this.dtv_Executor.ReadOnly = true;
            this.dtv_Executor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_Executor.Width = 55;
            // 
            // dtv_DELETE_DAT
            // 
            this.dtv_DELETE_DAT.HeaderText = "����ʱ��";
            this.dtv_DELETE_DAT.Name = "dtv_DELETE_DAT";
            this.dtv_DELETE_DAT.ReadOnly = true;
            this.dtv_DELETE_DAT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_DELETE_DAT.Width = 120;
            // 
            // dtv_DELETERNAME_VCHR
            // 
            this.dtv_DELETERNAME_VCHR.HeaderText = "������";
            this.dtv_DELETERNAME_VCHR.Name = "dtv_DELETERNAME_VCHR";
            this.dtv_DELETERNAME_VCHR.ReadOnly = true;
            this.dtv_DELETERNAME_VCHR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_DELETERNAME_VCHR.Width = 55;
            // 
            // viewname_vchr
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.viewname_vchr.DefaultCellStyle = dataGridViewCellStyle10;
            this.viewname_vchr.HeaderText = "���";
            this.viewname_vchr.Name = "viewname_vchr";
            this.viewname_vchr.ReadOnly = true;
            this.viewname_vchr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.viewname_vchr.Width = 45;
            // 
            // dtv_method
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dtv_method.DefaultCellStyle = dataGridViewCellStyle11;
            this.dtv_method.HeaderText = "����";
            this.dtv_method.Name = "dtv_method";
            this.dtv_method.ReadOnly = true;
            this.dtv_method.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_method.Width = 45;
            // 
            // dtv_NO
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtv_NO.DefaultCellStyle = dataGridViewCellStyle12;
            this.dtv_NO.HeaderText = "���";
            this.dtv_NO.Name = "dtv_NO";
            this.dtv_NO.ReadOnly = true;
            this.dtv_NO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtv_NO.Width = 45;
            // 
            // dtv_OUTGETMEDDAYS_INT
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dtv_OUTGETMEDDAYS_INT.DefaultCellStyle = dataGridViewCellStyle13;
            this.dtv_OUTGETMEDDAYS_INT.HeaderText = "����";
            this.dtv_OUTGETMEDDAYS_INT.Name = "dtv_OUTGETMEDDAYS_INT";
            this.dtv_OUTGETMEDDAYS_INT.ReadOnly = true;
            this.dtv_OUTGETMEDDAYS_INT.Visible = false;
            this.dtv_OUTGETMEDDAYS_INT.Width = 60;
            // 
            // dtv_CREATEAREA_Name
            // 
            this.dtv_CREATEAREA_Name.HeaderText = "��������";
            this.dtv_CREATEAREA_Name.Name = "dtv_CREATEAREA_Name";
            this.dtv_CREATEAREA_Name.ReadOnly = true;
            this.dtv_CREATEAREA_Name.Visible = false;
            // 
            // dtv_DOCTOR_VCHR
            // 
            this.dtv_DOCTOR_VCHR.HeaderText = "ҽ������ ";
            this.dtv_DOCTOR_VCHR.Name = "dtv_DOCTOR_VCHR";
            this.dtv_DOCTOR_VCHR.ReadOnly = true;
            this.dtv_DOCTOR_VCHR.Visible = false;
            this.dtv_DOCTOR_VCHR.Width = 60;
            // 
            // dtv_DOCTOR_SIGN
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.dtv_DOCTOR_SIGN.DefaultCellStyle = dataGridViewCellStyle14;
            this.dtv_DOCTOR_SIGN.HeaderText = "ǩ��";
            this.dtv_DOCTOR_SIGN.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.dtv_DOCTOR_SIGN.Name = "dtv_DOCTOR_SIGN";
            this.dtv_DOCTOR_SIGN.ReadOnly = true;
            this.dtv_DOCTOR_SIGN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dtv_DOCTOR_SIGN.Width = 60;
            // 
            // CREATEDATE_DAT
            // 
            this.CREATEDATE_DAT.HeaderText = "¼��ʱ��";
            this.CREATEDATE_DAT.Name = "CREATEDATE_DAT";
            this.CREATEDATE_DAT.ReadOnly = true;
            this.CREATEDATE_DAT.Width = 120;
            // 
            // dtv_ChangedID
            // 
            this.dtv_ChangedID.HeaderText = "�޸���";
            this.dtv_ChangedID.Name = "dtv_ChangedID";
            this.dtv_ChangedID.ReadOnly = true;
            this.dtv_ChangedID.Width = 80;
            // 
            // dtv_ChangedDate
            // 
            this.dtv_ChangedDate.HeaderText = "�޸�ʱ��";
            this.dtv_ChangedDate.Name = "dtv_ChangedDate";
            this.dtv_ChangedDate.ReadOnly = true;
            this.dtv_ChangedDate.Width = 120;
            // 
            // m_dtStartDate
            // 
            this.m_dtStartDate.HeaderText = "����ʱ��";
            this.m_dtStartDate.Name = "m_dtStartDate";
            this.m_dtStartDate.ReadOnly = true;
            this.m_dtStartDate.Visible = false;
            this.m_dtStartDate.Width = 85;
            // 
            // m_dtvSENDBACKER_CHR
            // 
            this.m_dtvSENDBACKER_CHR.HeaderText = "�˻���";
            this.m_dtvSENDBACKER_CHR.Name = "m_dtvSENDBACKER_CHR";
            this.m_dtvSENDBACKER_CHR.ReadOnly = true;
            this.m_dtvSENDBACKER_CHR.Width = 80;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pnSel);
            this.panel5.Controls.Add(this.pnTabs);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(527, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(496, 377);
            this.panel5.TabIndex = 41;
            // 
            // pnSel
            // 
            this.pnSel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnSel.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnSel.Location = new System.Drawing.Point(24, 0);
            this.pnSel.Name = "pnSel";
            this.pnSel.Size = new System.Drawing.Size(472, 377);
            this.pnSel.TabIndex = 3;
            this.pnSel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnSel_MouseMove);
            // 
            // pnTabs
            // 
            this.pnTabs.Controls.Add(this.lbePriceInfo);
            this.pnTabs.Controls.Add(this.lblBoard);
            this.pnTabs.Controls.Add(this.lblLeft);
            this.pnTabs.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnTabs.Location = new System.Drawing.Point(0, 0);
            this.pnTabs.Name = "pnTabs";
            this.pnTabs.Size = new System.Drawing.Size(24, 377);
            this.pnTabs.TabIndex = 2;
            // 
            // lbePriceInfo
            // 
            this.lbePriceInfo.BackColor = System.Drawing.SystemColors.Control;
            this.lbePriceInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbePriceInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbePriceInfo.Font = new System.Drawing.Font("����", 9.5F);
            this.lbePriceInfo.ForeColor = System.Drawing.Color.Black;
            this.lbePriceInfo.Location = new System.Drawing.Point(0, 192);
            this.lbePriceInfo.Name = "lbePriceInfo";
            this.lbePriceInfo.Size = new System.Drawing.Size(24, 92);
            this.lbePriceInfo.TabIndex = 0;
            this.lbePriceInfo.Text = "�շ���Ϣ";
            this.lbePriceInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbePriceInfo.Click += new System.EventHandler(this.lbePriceInfo_Click);
            // 
            // lblBoard
            // 
            this.lblBoard.BackColor = System.Drawing.SystemColors.Control;
            this.lblBoard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBoard.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBoard.Font = new System.Drawing.Font("����", 9.5F);
            this.lblBoard.ForeColor = System.Drawing.Color.Black;
            this.lblBoard.Location = new System.Drawing.Point(0, 92);
            this.lblBoard.Name = "lblBoard";
            this.lblBoard.Size = new System.Drawing.Size(24, 100);
            this.lblBoard.TabIndex = 2;
            this.lblBoard.Text = "��ʱģ��";
            this.lblBoard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBoard.Click += new System.EventHandler(this.lblBoard_Click);
            // 
            // lblLeft
            // 
            this.lblLeft.BackColor = System.Drawing.SystemColors.Control;
            this.lblLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLeft.Font = new System.Drawing.Font("����", 9.5F);
            this.lblLeft.ForeColor = System.Drawing.Color.Black;
            this.lblLeft.Location = new System.Drawing.Point(0, 0);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(24, 92);
            this.lblLeft.TabIndex = 1;
            this.lblLeft.Text = "ѡ����";
            this.lblLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeft.Click += new System.EventHandler(this.lblLeft_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cboPrintType);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.m_plControls);
            this.panel3.Controls.Add(this.m_ctlOrderDetail);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 445);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1023, 102);
            this.panel3.TabIndex = 20;
            // 
            // cboPrintType
            // 
            this.cboPrintType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrintType.FormattingEnabled = true;
            this.cboPrintType.Items.AddRange(new object[] {
            "ҽ����",
            "��ҩ����",
            "����ҩƷ����"});
            this.cboPrintType.Location = new System.Drawing.Point(680, 72);
            this.cboPrintType.Name = "cboPrintType";
            this.cboPrintType.Size = new System.Drawing.Size(107, 22);
            this.cboPrintType.TabIndex = 71;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("����", 9.5F);
            this.label5.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label5.Location = new System.Drawing.Point(619, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 122;
            this.label5.Text = "��ӡѡ��:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_plControls
            // 
            this.m_plControls.BackColor = System.Drawing.SystemColors.Control;
            this.m_plControls.Controls.Add(this.m_cmdDelete2);
            this.m_plControls.Controls.Add(this.m_cmdChange);
            this.m_plControls.Controls.Add(this.m_cmdToCommit);
            this.m_plControls.Controls.Add(this.m_cmdDelete);
            this.m_plControls.Controls.Add(this.cmdPrintOrder);
            this.m_plControls.Controls.Add(this.m_cmdAdd);
            this.m_plControls.Controls.Add(this.m_cmdSave);
            this.m_plControls.Controls.Add(this.m_cmdStop);
            this.m_plControls.Controls.Add(this.m_cmdRetract);
            this.m_plControls.Controls.Add(this.m_cmdBlankOut);
            this.m_plControls.Controls.Add(this.cmdRefurbish);
            this.m_plControls.Controls.Add(this.m_cmdSub);
            this.m_plControls.Controls.Add(this.m_cmdCommitAll);
            this.m_plControls.Controls.Add(this.m_cmdClose);
            this.m_plControls.Controls.Add(this.m_cmdChgView);
            this.m_plControls.Location = new System.Drawing.Point(832, 3);
            this.m_plControls.Name = "m_plControls";
            this.m_plControls.Size = new System.Drawing.Size(189, 97);
            this.m_plControls.TabIndex = 300;
            // 
            // m_cmdDelete2
            // 
            this.m_cmdDelete2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdDelete2.DefaultScheme = true;
            this.m_cmdDelete2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdDelete2.Font = new System.Drawing.Font("����", 9.5F);
            this.m_cmdDelete2.Hint = "";
            this.m_cmdDelete2.Location = new System.Drawing.Point(4, 33);
            this.m_cmdDelete2.Name = "m_cmdDelete2";
            this.m_cmdDelete2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete2.Size = new System.Drawing.Size(61, 29);
            this.m_cmdDelete2.TabIndex = 43;
            this.m_cmdDelete2.Text = "ɾ�� F6";
            this.m_cmdDelete2.Click += new System.EventHandler(this.m_cmdDelete2_Click);
            // 
            // m_cmdChange
            // 
            this.m_cmdChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdChange.DefaultScheme = true;
            this.m_cmdChange.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdChange.Font = new System.Drawing.Font("����", 9.5F);
            this.m_cmdChange.Hint = "";
            this.m_cmdChange.Location = new System.Drawing.Point(125, 2);
            this.m_cmdChange.Name = "m_cmdChange";
            this.m_cmdChange.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdChange.Size = new System.Drawing.Size(61, 29);
            this.m_cmdChange.TabIndex = 42;
            this.m_cmdChange.Text = "�޸� M";
            this.m_cmdChange.Click += new System.EventHandler(this.m_cmdChange_Click);
            // 
            // m_cmdToCommit
            // 
            this.m_cmdToCommit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdToCommit.DefaultScheme = true;
            this.m_cmdToCommit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdToCommit.Font = new System.Drawing.Font("����", 9.5F);
            this.m_cmdToCommit.Hint = "";
            this.m_cmdToCommit.Location = new System.Drawing.Point(64, 33);
            this.m_cmdToCommit.Name = "m_cmdToCommit";
            this.m_cmdToCommit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdToCommit.Size = new System.Drawing.Size(61, 29);
            this.m_cmdToCommit.TabIndex = 44;
            this.m_cmdToCommit.Text = "�ύ A";
            this.m_cmdToCommit.Click += new System.EventHandler(this.m_cmdToCommit_Click);
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdDelete.Enabled = false;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(3, 152);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete.Size = new System.Drawing.Size(71, 26);
            this.m_cmdDelete.TabIndex = 43;
            this.m_cmdDelete.Text = "ɾ�� F6";
            this.m_cmdDelete.Visible = false;
            this.m_cmdDelete.Click += new System.EventHandler(this.m_cmdDelete_Click);
            // 
            // m_cmdAdd
            // 
            this.m_cmdAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdAdd.DefaultScheme = true;
            this.m_cmdAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdAdd.Font = new System.Drawing.Font("����", 9.5F);
            this.m_cmdAdd.Hint = "";
            this.m_cmdAdd.Location = new System.Drawing.Point(4, 2);
            this.m_cmdAdd.Name = "m_cmdAdd";
            this.m_cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_cmdAdd.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdAdd.Size = new System.Drawing.Size(61, 29);
            this.m_cmdAdd.TabIndex = 40;
            this.m_cmdAdd.Text = "��� F2";
            this.m_cmdAdd.Click += new System.EventHandler(this.m_cmdAdd_Click);
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdSave.Font = new System.Drawing.Font("����", 9.5F);
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(64, 2);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(61, 29);
            this.m_cmdSave.TabIndex = 41;
            this.m_cmdSave.Text = "���� F12";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // m_cmdRetract
            // 
            this.m_cmdRetract.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdRetract.DefaultScheme = true;
            this.m_cmdRetract.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdRetract.Hint = "";
            this.m_cmdRetract.Location = new System.Drawing.Point(146, 152);
            this.m_cmdRetract.Name = "m_cmdRetract";
            this.m_cmdRetract.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdRetract.Size = new System.Drawing.Size(63, 26);
            this.m_cmdRetract.TabIndex = 47;
            this.m_cmdRetract.Text = "���� S";
            this.m_cmdRetract.Visible = false;
            this.m_cmdRetract.Click += new System.EventHandler(this.m_cmdRetract_Click);
            // 
            // m_cmdBlankOut
            // 
            this.m_cmdBlankOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdBlankOut.DefaultScheme = true;
            this.m_cmdBlankOut.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdBlankOut.Enabled = false;
            this.m_cmdBlankOut.Hint = "";
            this.m_cmdBlankOut.Location = new System.Drawing.Point(80, 152);
            this.m_cmdBlankOut.Name = "m_cmdBlankOut";
            this.m_cmdBlankOut.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdBlankOut.Size = new System.Drawing.Size(62, 26);
            this.m_cmdBlankOut.TabIndex = 44;
            this.m_cmdBlankOut.Text = "���� F6";
            this.m_cmdBlankOut.Visible = false;
            this.m_cmdBlankOut.Click += new System.EventHandler(this.m_cmdBlankOut_Click);
            // 
            // cmdRefurbish
            // 
            this.cmdRefurbish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cmdRefurbish.DefaultScheme = true;
            this.cmdRefurbish.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdRefurbish.Font = new System.Drawing.Font("����", 9.5F);
            this.cmdRefurbish.Hint = "";
            this.cmdRefurbish.Location = new System.Drawing.Point(64, 64);
            this.cmdRefurbish.Name = "cmdRefurbish";
            this.cmdRefurbish.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdRefurbish.Size = new System.Drawing.Size(61, 29);
            this.cmdRefurbish.TabIndex = 46;
            this.cmdRefurbish.Text = "ˢ�� F10";
            this.cmdRefurbish.Click += new System.EventHandler(this.cmdRefurbish_Click);
            // 
            // m_cmdSub
            // 
            this.m_cmdSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdSub.DefaultScheme = true;
            this.m_cmdSub.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdSub.Enabled = false;
            this.m_cmdSub.Hint = "";
            this.m_cmdSub.Location = new System.Drawing.Point(149, 120);
            this.m_cmdSub.Name = "m_cmdSub";
            this.m_cmdSub.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSub.Size = new System.Drawing.Size(56, 26);
            this.m_cmdSub.TabIndex = 41;
            this.m_cmdSub.Text = "��ҽ��(F9)";
            this.m_cmdSub.Click += new System.EventHandler(this.m_cmdSub_Click);
            // 
            // m_cmdCommitAll
            // 
            this.m_cmdCommitAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdCommitAll.DefaultScheme = true;
            this.m_cmdCommitAll.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdCommitAll.Hint = "";
            this.m_cmdCommitAll.Location = new System.Drawing.Point(77, 120);
            this.m_cmdCommitAll.Name = "m_cmdCommitAll";
            this.m_cmdCommitAll.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCommitAll.Size = new System.Drawing.Size(71, 26);
            this.m_cmdCommitAll.TabIndex = 70;
            this.m_cmdCommitAll.Text = "�ύ����";
            this.m_cmdCommitAll.Visible = false;
            this.m_cmdCommitAll.Click += new System.EventHandler(this.m_cmdCommitAll_Click);
            // 
            // m_cmdClose
            // 
            this.m_cmdClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdClose.DefaultScheme = true;
            this.m_cmdClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdClose.Font = new System.Drawing.Font("����", 9.5F);
            this.m_cmdClose.Hint = "";
            this.m_cmdClose.Location = new System.Drawing.Point(125, 64);
            this.m_cmdClose.Name = "m_cmdClose";
            this.m_cmdClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdClose.Size = new System.Drawing.Size(61, 29);
            this.m_cmdClose.TabIndex = 48;
            this.m_cmdClose.Text = "�˳� Esc";
            this.m_cmdClose.Click += new System.EventHandler(this.m_cmdClose_Click);
            // 
            // m_cmdChgView
            // 
            this.m_cmdChgView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdChgView.DefaultScheme = true;
            this.m_cmdChgView.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdChgView.Hint = "";
            this.m_cmdChgView.Location = new System.Drawing.Point(-1, 120);
            this.m_cmdChgView.Name = "m_cmdChgView";
            this.m_cmdChgView.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdChgView.Size = new System.Drawing.Size(71, 26);
            this.m_cmdChgView.TabIndex = 50;
            this.m_cmdChgView.Tag = "0";
            this.m_cmdChgView.Text = "��/����";
            this.m_cmdChgView.Visible = false;
            this.m_cmdChgView.Click += new System.EventHandler(this.m_cmdChgView_Click);
            // 
            // m_ctlOrderDetail
            // 
            this.m_ctlOrderDetail.DoctorEditable = true;
            this.m_ctlOrderDetail.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_ctlOrderDetail.IsMedicareMan = false;
            this.m_ctlOrderDetail.Location = new System.Drawing.Point(0, 0);
            this.m_ctlOrderDetail.Name = "m_ctlOrderDetail";
            this.m_ctlOrderDetail.ReadOnly = false;
            this.m_ctlOrderDetail.Size = new System.Drawing.Size(832, 100);
            this.m_ctlOrderDetail.TabIndex = 30;
            this.m_ctlOrderDetail.m_evtInputEnd += new System.EventHandler(this.m_ctlOrderDetail_m_evtInputEnd);
            this.m_ctlOrderDetail.m_evtInputNO += new System.EventHandler(this.m_ctlOrderDetail_m_evtInputNO);
            this.m_ctlOrderDetail.evtInputOrder += new System.EventHandler(this.m_ctlOrderDetail_evtInputOrder);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(707, 84);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 14);
            this.label14.TabIndex = 71;
            this.label14.Text = "�������:";
            this.label14.Visible = false;
            // 
            // m_btnAddBills
            // 
            this.m_btnAddBills.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_btnAddBills.DefaultScheme = true;
            this.m_btnAddBills.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnAddBills.Font = new System.Drawing.Font("����", 9.5F);
            this.m_btnAddBills.Hint = "";
            this.m_btnAddBills.Location = new System.Drawing.Point(872, 34);
            this.m_btnAddBills.Name = "m_btnAddBills";
            this.m_btnAddBills.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnAddBills.Size = new System.Drawing.Size(136, 28);
            this.m_btnAddBills.TabIndex = 72;
            this.m_btnAddBills.Text = "���ӵ���";
            this.m_btnAddBills.Visible = false;
            // 
            // m_lsvToolTip
            // 
            this.m_lsvToolTip.BackColor = System.Drawing.SystemColors.Window;
            this.m_lsvToolTip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lsvToolTip.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader8,
            this.columnHeader3,
            this.columnHeader2,
            this.columnHeader5,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader7});
            this.m_lsvToolTip.Font = new System.Drawing.Font("����", 10F);
            this.m_lsvToolTip.FullRowSelect = true;
            this.m_lsvToolTip.GridLines = true;
            this.m_lsvToolTip.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lsvToolTip.HoverSelection = true;
            this.m_lsvToolTip.Location = new System.Drawing.Point(-44, 276);
            this.m_lsvToolTip.Name = "m_lsvToolTip";
            this.m_lsvToolTip.ShowItemToolTips = true;
            this.m_lsvToolTip.Size = new System.Drawing.Size(528, 97);
            this.m_lsvToolTip.TabIndex = 60;
            this.m_lsvToolTip.UseCompatibleStateImageBehavior = false;
            this.m_lsvToolTip.View = System.Windows.Forms.View.Details;
            this.m_lsvToolTip.Visible = false;
            this.m_lsvToolTip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvToolTip_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "���";
            this.columnHeader1.Width = 30;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "��Ŀ����";
            this.columnHeader8.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "��Ŀ����";
            this.columnHeader3.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "��������";
            this.columnHeader2.Width = 70;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "����";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "����";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "�ܽ��";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 70;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "��������";
            this.columnHeader7.Width = 90;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(324, 164);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(609, 182);
            this.label13.TabIndex = 62;
            this.label13.Text = resources.GetString("label13.Text");
            // 
            // m_lblOtherBill
            // 
            this.m_lblOtherBill.Enabled = false;
            this.m_lblOtherBill.Location = new System.Drawing.Point(935, 48);
            this.m_lblOtherBill.Name = "m_lblOtherBill";
            this.m_lblOtherBill.Size = new System.Drawing.Size(64, 16);
            this.m_lblOtherBill.TabIndex = 55;
            this.m_lblOtherBill.TabStop = true;
            this.m_lblOtherBill.Text = "���ӵ���";
            this.m_lblOtherBill.Visible = false;
            this.m_lblOtherBill.Click += new System.EventHandler(this.m_lblOtherBill_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(968, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 20);
            this.button1.TabIndex = 61;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_pcBoxAlert
            // 
            this.m_pcBoxAlert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.m_pcBoxAlert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_pcBoxAlert.Image = ((System.Drawing.Image)(resources.GetObject("m_pcBoxAlert.Image")));
            this.m_pcBoxAlert.Location = new System.Drawing.Point(959, 34);
            this.m_pcBoxAlert.Name = "m_pcBoxAlert";
            this.m_pcBoxAlert.Size = new System.Drawing.Size(30, 30);
            this.m_pcBoxAlert.TabIndex = 102;
            this.m_pcBoxAlert.TabStop = false;
            this.m_pcBoxAlert.Visible = false;
            this.m_pcBoxAlert.Click += new System.EventHandler(this.m_pcBoxAlert_Click);
            // 
            // m_ctlPatient
            // 
            this.m_ctlPatient.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_ctlPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_ctlPatient.Font = new System.Drawing.Font("����", 9.5F);
            this.m_ctlPatient.Location = new System.Drawing.Point(0, 0);
            this.m_ctlPatient.Name = "m_ctlPatient";
            this.m_ctlPatient.Size = new System.Drawing.Size(1023, 68);
            this.m_ctlPatient.TabIndex = 1;
            this.m_ctlPatient.TabStop = false;
            this.m_ctlPatient.m_evtPatientChanged += new System.EventHandler(this.ctlBIHPatientInfo1_m_evtPatientChanged);
            this.m_ctlPatient.m_evtPatientFromBedAdmin += new System.EventHandler(this.m_ctlPatient_m_evtPatientFromBedAdmin);
            // 
            // frmBIHOrderInput
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1023, 591);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_pcBoxAlert);
            this.Controls.Add(this.cmbApply);
            this.Controls.Add(this.m_txtBackReason);
            this.Controls.Add(this.btCreatBill);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.collapsibleSplitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.m_btnAddBills);
            this.Controls.Add(this.m_lsvToolTip);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.m_lblOtherBill);
            this.Controls.Add(this.m_ctlPatient);
            this.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmBIHOrderInput";
            this.Text = "ҽ��ҽ������վ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBIHOrderInput_FormClosing);
            this.Load += new System.EventHandler(this.frmBIHOrderInput_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBIHOrderInput_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.m_imgBackAlert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.m_ctxGridMenu2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_dtvOrder)).EndInit();
            this.panel5.ResumeLayout(false);
            this.pnTabs.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.m_plControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_pcBoxAlert)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        //1067 ҽ��¼��ʱ���ҽ���Ƿ������д���뵥
        /// <summary>
        /// 1067 ҽ��¼��ʱ���ҽ���Ƿ������д���뵥 true �� false ��
        /// </summary>
        internal bool blnFillApplyBill = false;

        /// <summary>
        /// ��鷿����
        /// </summary>
        internal int dayGrandRounds = 0;

        /// <summary>
        /// ƽ��סԺ��
        /// </summary>
        internal int dayAverageStay = 0;

        /// <summary>
        /// �Ƿ�ʹ�ö�ͯ�۸� 9015
        /// </summary>
        bool isUseChildPrice { get; set; }

        #region ��ǰҽ��
        /// <summary>
        /// ��ǰҽ��Vo����
        /// </summary>
        internal clsBIHOrder m_objCurrentOrderValue = null;
        /// <summary>
        /// ��ȡ�����õ�ǰҽ��Vo����
        /// </summary>
        public clsBIHOrder m_objCurrentOrder
        {
            get
            {
                return m_objCurrentOrderValue;
            }
            set
            {
                m_objCurrentOrderValue = value;
            }
        }

        /// <summary>
        /// ҽ�����ID
        /// </summary>
        public List<string> lstYbPayTypeId { get; set; }

        #endregion
        #region ����
        private string m_strGetStatusMessage(int intStatus)
        {
            return m_objDomain.m_strGetStatusMessage(intStatus);
        }

        private string m_strGetOrderMessage(clsBIHOrder objOrder)
        {
            return m_objDomain.m_strGetOrderMessage(objOrder);
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


        internal void m_mthShowCurrentOrder(clsBIHOrder objOrder)
        {
            m_ctlOrderDetail.m_mthSetOrder(objOrder);

            if (objOrder != null && objOrder.m_intStatus == 7)
            {
                m_lblOrderStatus.Tag = objOrder;
                string strTool = "�˻�ԭ��" + ((objOrder.m_strBACKREASON == null) ? ("") : (objOrder.m_strBACKREASON.Trim()));
                strTool += "\r\n�˻ػ�ʿ��" + ((objOrder.m_strSENDBACKER_CHR == null) ? ("") : (objOrder.m_strSENDBACKER_CHR.Trim()));
                strTool += "\r\n�˻�ʱ�䣺" + ((objOrder.m_strSENDBACK_DAT == null) ? ("") : (objOrder.m_strSENDBACK_DAT.Trim()));
                toolTip1.SetToolTip(m_lblOrderStatus, strTool);
            }
            else
            {
                m_lblOrderStatus.Tag = null;
                try
                {
                    toolTip1.SetToolTip(m_lblOrderStatus, "ҽ��״̬");
                }
                catch
                {
                }
            }
            m_lblOrderStatus.Text = m_objDomain.m_strGetOrderMessage(objOrder);
        }

        internal clsBIHOrder m_objGetOrder(clsBIHOrder objOrder)
        {
            if (m_ctlPatient.m_objPatient == null) return null;
            return m_ctlOrderDetail.m_objGetOrder(m_ctlPatient.m_objPatient, objOrder);
        }
        #endregion
        #region �¼�
        private void frmBIHOrderInput_Load(object sender, System.EventArgs e)
        {

            //��������LABEL
            this.label13.Visible = false;
            button1_Click(null, null);
            this.label13.Visible = false;
            //��/�ٽ������ 
            if (m_strView.Equals("1"))
            {
                LongBihorderSet();
                m_cmdChgView.Tag = m_strView;
                setTheChgViewButton();
            }
            else if (m_strView.Equals("2"))
            {
                ShortBihorderSet();
                m_cmdChgView.Tag = m_strView;
                setTheChgViewButton();
            }
            selectOldColor = this.m_dtvOrder.DefaultCellStyle.SelectionForeColor;

            if (m_objLoginInfo != null)
            {
                m_mthSetCurrentDoctor(m_objLoginInfo.m_strEmpID, m_objLoginInfo.m_strEmpName);
            }
            else if (com.digitalwave.GUI_Base.frmMDI_Child_Base.CurrentLoginInfo != null)
            {
                this.LoginInfo = com.digitalwave.GUI_Base.frmMDI_Child_Base.CurrentLoginInfo;
            }
            this.m_ctlPatient.LoginInfo = this.LoginInfo;
            //�鿴��ǰ�����Ƿ��д���Ȩ
            m_objDomain.m_lngAddGetAccessPower(this.LoginInfo.m_strEmpID, out m_blAccess);
            if (m_blAccess == false)
            {
                m_plControls.Enabled = false;
                m_ctlOrderDetail.Enabled = false;
            }
            m_ctlOrderDetail.cboShiying.SelectedIndex = 0;
            /*<=============================*/
            // ����ҽ������     
            m_objDomain.m_Loadm_lngGetOrderCate();
            this.m_objDomain.m_Loadm_lngGetcboOrderList();
            //�����������ñ�
            m_objDomain.m_LoadGetSPECORDERCATE();
            // ҽ�������޸Ŀ���
            SetTheOrderPowerControls();
            //��ȡϵͳ�� -�������
            if (m_blLisDiscount)
            {
                LoadThePARMVALUE();
            }
            //��ҽ���б������ƣ��Ƿ���ʾҽ��ǩ����
            if (m_blDoctorSign)
            {
                this.m_dtvOrder.Columns["dtv_DOCTOR_VCHR"].Visible = false;
                this.m_dtvOrder.Columns["dtv_DOCTOR_SIGN"].Visible = true;
            }
            else
            {
                this.m_dtvOrder.Columns["dtv_DOCTOR_VCHR"].Visible = true;
                this.m_dtvOrder.Columns["dtv_DOCTOR_SIGN"].Visible = false;
            }
            if (m_ctlOrderDetail.m_mthGetDoctor(this.LoginInfo.m_strEmpID) == 0)
            {
                m_mthSetCurrentDoctor(this.LoginInfo.m_strEmpID, this.LoginInfo.m_strEmpName);
            }
            //�ұ��б�Ŀ���
            lblLeft.BackColor = SystemColors.Control;
            lblLeft.ForeColor = Color.Black;
            pnSel.Hide();
            fl.Visible = false;
            m_IsDisPlayToolTip = false;
            lblLeft.Parent.Parent.Width = lblLeft.Width;

            fl.setLoginDoctor(this.LoginInfo);
            //�ڵ���ʱĬ�Ͽ���Ϊ��ǰԱ�����ڿ��� 
            clsBIHArea area1 = new clsBIHArea();
            area1.m_strAreaID = this.LoginInfo.m_strInpatientAreaID;
            area1.m_strAreaName = this.LoginInfo.m_strInpatientAreaName;
            if (this.LoginInfo.m_strInpatientAreaID.Trim().Equals(""))
            {
                area1.m_strAreaID = this.LoginInfo.m_strDepartmentID;
                area1.m_strAreaName = this.LoginInfo.m_strdepartmentName;
            }
            m_ctlPatient.m_txtArea.Text = area1.m_strAreaName;
            m_ctlPatient.m_txtArea.Tag = area1;

            //m_ctlOrderDetail.m_txtArea.Text = area1.m_strAreaName;
            //m_ctlOrderDetail.m_txtArea.Tag = area1;
            //������������
            m_txtCREATEAREA.Tag = area1.m_strAreaID;
            m_txtCREATEAREA.Text = area1.m_strAreaName;
            /*<=========================================*/
            //ҽ�����ڹ�����ɣ�
            m_strDOCTORGROUPID_CHR = GetDOCTORGROUPID();
            /*<================================================*/

            if (m_strView.Equals("3") && m_intInvoSrc != 1 && this.m_ctlPatient.m_objPatient == null && ((com.digitalwave.iCare.gui.frmMain)this.MdiParent).m_StrPatientID != null && !((com.digitalwave.iCare.gui.frmMain)this.MdiParent).m_StrPatientID.Trim().Equals(""))
            {
                m_intInvoSrc = 0;
                m_ctlPatient.m_mthGetPatientByPATIENTID(((com.digitalwave.iCare.gui.frmMain)this.MdiParent).m_StrPatientID);

                m_ctlOrderDetail.Focus();
                SendKeys.Send("{TAB}");
                SendKeys.Send("{TAB}");
                SendKeys.Send("{TAB}");
            }
            else if (m_intInvoSrc != 1 && this.m_ctlPatient.m_objPatient == null)
            {

                m_intInvoSrc = 0;
                m_ctlPatient.Focus();
                m_ctlPatient.m_txtArea.Focus();
                SendKeys.Send("{TAB}");
                SendKeys.Send("{TAB}");
            }
            m_mthGetAllStorgeId();//��ѯ��ҩ��ID����ֵ
            #region ����ȫ�ֱ����Զ�ҽ��
            //if (GlobalData.clsPublic.m_ObjGlobalPatient != null)
            //{
            //    this.m_objDomain.m_mthLoadOrderList();
            //}            
            #endregion
            blnFillApplyBill = (com.digitalwave.iCare.gui.HIS.clsPublic.m_intGetSysParm("1067") == 1 ? true : false);

            string p1013 = com.digitalwave.iCare.gui.HIS.clsPublic.m_strGetSysparm("1013");
            string p1014 = com.digitalwave.iCare.gui.HIS.clsPublic.m_strGetSysparm("1014");
            int.TryParse(p1013, out this.dayGrandRounds);
            int.TryParse(p1014, out this.dayAverageStay);

            this.isUseChildPrice = (new clsDcl_ExecuteOrder()).IsUseChildPrice();

            this.cboPrintType.SelectedIndex = 0;

            this.lstYbPayTypeId = clsPublic.m_mthGetYBPayID();
        }

        /// <summary>
        /// ��ѯ��ҩ��ID����ֵ
        /// </summary>
        private void m_mthGetAllStorgeId()
        {
            string strMedDeptId = "";
            string[] strMedDeptIdArr = m_strMedDeptGross.Split('*');
            int intArrCount = strMedDeptIdArr.Length;
            for (int i = 0; i < intArrCount; i++)
            {
                if (i == 0)
                {
                    m_strMedCineStorgeId = strMedDeptIdArr[i];
                }
                else if (i == 1)
                {
                    m_strMidMedCineSorgeId = strMedDeptIdArr[i];
                }
            }
        }

        /// <summary>
        /// ϵͳ������(ICARE����)
        /// </summary>
        internal long LoadThePARMVALUE()
        {

            List<string> PARMCODE_CHR = new List<string>();
            DataTable m_dtPARMVALUE_VCHR = new DataTable();
            // PARMCODE_CHR.Add("1008");//1008 סԺȷ�ϼ������̶�Ӧ�����ID ������������ݸ���
            PARMCODE_CHR.Add("0013");//0013 ������ϴ��۷�Ʊ���� ������������ݸ���
            PARMCODE_CHR.Add("0008");//0008�������ã�����Ϊҽ���������ID��Ŀ���ڳ�Ժ��ҩ������7�죩
            PARMCODE_CHR.Add("0009");//0009סԺ����ҩ������ID
            PARMCODE_CHR.Add("1009");//סԺ���ҩ���ж�
            //long lngRes = m_objManage.LoadThePARMVALUE(PARMCODE_CHR, out m_strPARMVALUE_VCHR);
            long lngRes = m_objDomain.LoadThePARMVALUE(PARMCODE_CHR, out m_dtPARMVALUE_VCHR);
            if (lngRes > 0 && m_dtPARMVALUE_VCHR != null)
            {
                string m_strPARMCODE = "";
                string m_strPARMVALUE = "";
                for (int i = 0; i < m_dtPARMVALUE_VCHR.Rows.Count; i++)
                {
                    m_strPARMCODE = m_dtPARMVALUE_VCHR.Rows[i]["PARMCODE_CHR"].ToString().Trim();
                    m_strPARMVALUE = m_dtPARMVALUE_VCHR.Rows[i]["PARMVALUE_VCHR"].ToString().Trim();

                    switch (m_strPARMCODE)
                    {
                        //case "1008":
                        //    m_strPARMVALUE_VCHR = m_strPARMVALUE;
                        //    break;
                        case "0013":
                            m_strLisPARMVALUE_VCHR = m_strPARMVALUE;
                            break;
                        case "0009":
                            m_strMedDeptId = m_strPARMVALUE;
                            break;
                        case "1009":
                            m_strMedDeptGross = m_strPARMVALUE;
                            break;
                        case "0008":
                            string[] strTemp = m_strPARMVALUE.Split(';');
                            for (int i2 = 0; i2 < strTemp.Length; i2++)
                            {
                                m_lstSocialSecurity.Add(strTemp[i2]);
                            }
                            strTemp = null;
                            break;
                    }
                }
            }
            return lngRes;
        }

        private void SetTheOrderPowerControls()
        {
            //m_intShowCodexRemarkFrm ҽ������վ�Ƿ���ʾҩ�䱸ע 0 ����ʾ 1 ��ʾ 0059
            //ShowCodexRemarkFrmTimerinterval ҽ������վҩ�䱸ע������ʾʱ�� 0060
            //m_intUpControl ҽ��¼��Ȩ�޷������޿���1003 0-�أ�1-��
            //m_intBihNameOpen ���ܿ������������Ƿ������޸�ҽ��������Ŀ���� 1017
            //m_intBihBlankOutOpen ���ܿ������������Ƿ������޸�ҽ������ 1023
            //m_intTypePControl Ƭ���������㿪��1024
            //m_intLessMedControl ȱҩ��ʾ���ƿ���
            //m_intDoctorSign ҽ��ǩ�����ƿ���
            //m_intZCaoControl ����ҽ��ת��������̣�0����������1������
            //m_intCommitControl �ύʱ�Ƿ���Ҫ���빤�ż����� 1032
            //m_intDeableMedControl ҽ��¼���Ƿ����¼��ȱҩ���շ���Ŀ 0-��1-�� 1036
            //m_intStopControl ҽ��¼���Ƿ����¼����ͣ�õ��շ���Ŀ 0-��,1-�� 1037
            //m_intStopTipControl סԺҽ��ͣҽ���Ƿ���Ҫ��֤ 0-����Ҫ��1-��Ҫ 1044
            //m_intCanChangeOrder �Ƿ���������ҽ���޸ķǱ��˿���ת��ǰ��ҽ�� 1045
            //m_intSendLisBill �ύʱ�Ƿ��ͼ������뵥 1050
            //m_intStopTimeSwitth ҽ���޸�ͣ��ʱ���ʱ������ 0-������,>0Ϊ���Ƶ�ʱ��,��24,��Ϊ������24Сʱ�ڿ����޸�  1051</param>
            //m_intLisDiscountNum  4006����Ϊ8��������м��飨��Ʊ����Ϊ���飩�շ���Ŀ>8��ʱ���ô��۹���
            //m_decLisDiscountMount 4007�������ô��۹���ʱ�������շ���Ŀ�Ĵ��۱�����80��������
            //m_intLisDiscount 4008  0-false������ 1-true �������
            //m_intAutoStopAlert '1053', 'סԺҽ��¼������Ƿ��Զ���ʾ��ǰ���˴���ͣ�û�ȱҩ��δͣҽ��', '0-��1-��', 1, '0010' 
            //m_intStartTimeSwitth ҽ���޸�����ʱ���ʱ������ 0-������,>0Ϊ���Ƶ�ʱ��,��24,��Ϊ������24Сʱ�ڿ����޸�  1054
            int m_intBihNameOpen, m_intBihBlankOutOpen, m_intShowCodexRemarkFrm, m_intLessMedControl, m_intDoctorSign, m_intZCaoControl, m_intCommitControl, m_intUpControl, m_intStopControl, m_intDeableMedControl, m_intStopTipControl, m_intCanChangeOrder, m_intSendLisBill, m_intLisDiscount, m_intAutoStopAlert, p_intParm1068;
            m_objDomain.m_lngGetBihOrderControls(out m_intBihNameOpen, out m_intBihBlankOutOpen, out m_intTypePControl, out m_intShowCodexRemarkFrm, out ShowCodexRemarkFrmTimerinterval, out m_intLessMedControl, out m_intDoctorSign, out m_intZCaoControl, out m_intCommitControl, out m_intUpControl, out m_intStopControl, out m_intDeableMedControl, out m_intStopTipControl, out m_intCanChangeOrder, out m_intSendLisBill, out m_intStopTimeSwitth, out m_intLisDiscount, out m_intLisDiscountNum, out m_decLisDiscountMount, out m_intAutoStopAlert, out m_intStartTimeSwitth, out p_intParm1068);
            if (m_intCommitControl == 1)
            {
                m_blCommitControl = true;
            }
            else
            {
                m_blCommitControl = false;
            }
            m_intCommitControl2 = com.digitalwave.iCare.gui.HIS.clsPublic.m_intGetSysParm("1032");
            if (m_intZCaoControl == 0)
            {
                m_blZCaoControl = false;
            }
            else
            {
                m_blZCaoControl = true;
            }
            if (m_intBihNameOpen == 0)
            {
                m_ctlOrderDetail.m_blOrderName2ReadOnly = true;
            }
            else if (m_intBihNameOpen == 1)
            {
                m_ctlOrderDetail.m_blOrderName2ReadOnly = false;
            }
            if (m_intBihBlankOutOpen == 1)
            {
                m_blBlankOutControl = true;
                m_cmdBlankOut.Enabled = true;
            }
            else if (m_intBihBlankOutOpen == 0)
            {
                m_blBlankOutControl = false;
                m_cmdBlankOut.Enabled = false;
            }
            if (m_intShowCodexRemarkFrm == 0)
            {
                IsShowCodexRemarkFrm = false;
            }
            else
            {
                IsShowCodexRemarkFrm = true;
            }
            if (m_intLessMedControl == 1)
            {
                m_blLessMedControl = true;
            }
            else
            {
                m_blLessMedControl = false;
            }
            if (m_intDoctorSign == 1)
            {
                m_blDoctorSign = true;
            }
            else if (m_intDoctorSign == 2)
            {
                m_blDoctorSign = true;
                m_intDoctorAutoSign = 1;
            }
            else
            {
                m_blDoctorSign = false;
            }
            if (m_intUpControl == 0)
            {
                m_blUpControl = false;
            }
            else
            {
                m_blUpControl = true;
            }
            if (m_intStopControl == 0)
            {
                m_blStopControl = false;
            }
            else
            {
                m_blStopControl = true;
            }
            if (m_intDeableMedControl == 0)
            {
                m_blDeableMedControl = false;
            }
            else
            {
                m_blDeableMedControl = true;
            }
            if (m_intStopTipControl == 1)
            {
                m_blStopTipControl = true;
            }
            else
            {
                m_blStopTipControl = false;
            }
            if (m_intCanChangeOrder == 1)
            {
                m_blCanChangeOrder = true;
            }
            else
            {
                m_blCanChangeOrder = false;
            }
            if (m_intSendLisBill == 1)
            {
                m_blSendLisBill = true;
            }
            else
            {
                m_blSendLisBill = false;
            }
            if (m_intLisDiscount == 1)
            {
                m_blLisDiscount = true;
            }
            else
            {
                m_blLisDiscount = false;

            }
            if (m_intAutoStopAlert == 1)
            {
                m_blAutoStopAlert = true;
            }
            else
            {
                m_blAutoStopAlert = false;
            }
            m_intParm1068 = p_intParm1068;
        }

        private string GetDOCTORGROUPID()
        {
            string m_strGroupID = "";
            DataTable dtbResult = new DataTable();
            long m_lngRet = 0;

            //com.digitalwave.iCare.middletier.baseInfo.clsEmployeeSvc EmpSvc =
            //                                               (com.digitalwave.iCare.middletier.baseInfo.clsEmployeeSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.baseInfo.clsEmployeeSvc));

            m_lngRet = (new weCare.Proxy.ProxyBase()).Service.m_lngGetGroupEmp(this.LoginInfo.m_strEmpID, out dtbResult);
            if (m_lngRet > 0 && dtbResult.Rows.Count > 0)
            {
                m_strGroupID = dtbResult.Rows[0]["groupid_chr"].ToString().Trim();
            }
            return m_strGroupID;

        }

        private void frmBIHOrderInput_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //����Ȩ
            if (m_blAccess == false)
            {

                return;
            }
            //��Ͽ�ݼ�
            if (e.Modifiers == Keys.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        if (m_ctlOrderDetail.m_dtFinishTime2.Enabled == true)
                        {
                            m_ctlOrderDetail.m_dtFinishTime2.Text = DateTime.Now.ToString("yyyy��MM��dd��HHʱ") + "00��";
                        }
                        break;
                    case Keys.S:
                        if (m_ctlOrderDetail.m_dtFinishTime2.Enabled == true)
                        {
                            m_ctlOrderDetail.m_dtFinishTime2.Text = "";
                        }
                        break;
                    case Keys.Q://���Ƶ�ǰ����ҽ��������ģ��
                        // m_mnuCopytoFoAll();
                        break;
                }
            }
            else if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.P://��ӡҽ�� [Ctrl + P] 
                        if (cmdPrintOrder.Enabled && cmdPrintOrder.Visible)
                        {
                            cmdPrintOrder_Click(sender, e);
                        }
                        break;
                    case Keys.D0:
                        this.m_cboOrderList.SelectedIndex = 10;
                        m_cboOrderList_SelectionChangeCommitted(null, null);
                        break;

                    case Keys.D1:
                        this.m_cboOrderList.SelectedIndex = 1;
                        m_cboOrderList_SelectionChangeCommitted(null, null);
                        break;

                    case Keys.D2:
                        this.m_cboOrderList.SelectedIndex = 2;
                        m_cboOrderList_SelectionChangeCommitted(null, null);
                        break;

                    case Keys.D3:
                        this.m_cboOrderList.SelectedIndex = 3;
                        m_cboOrderList_SelectionChangeCommitted(null, null);
                        break;

                    case Keys.D4:
                        this.m_cboOrderList.SelectedIndex = 4;
                        m_cboOrderList_SelectionChangeCommitted(null, null);
                        break;

                    case Keys.D5:
                        this.m_cboOrderList.SelectedIndex = 5;
                        m_cboOrderList_SelectionChangeCommitted(null, null);
                        break;

                    case Keys.D6:
                        this.m_cboOrderList.SelectedIndex = 6;
                        m_cboOrderList_SelectionChangeCommitted(null, null);
                        break;

                    case Keys.D7:
                        this.m_cboOrderList.SelectedIndex = 7;
                        m_cboOrderList_SelectionChangeCommitted(null, null);
                        break;

                    case Keys.D8:
                        this.m_cboOrderList.SelectedIndex = 8;
                        m_cboOrderList_SelectionChangeCommitted(null, null);
                        break;

                    case Keys.D9:
                        this.m_cboOrderList.SelectedIndex = 9;
                        m_cboOrderList_SelectionChangeCommitted(null, null);
                        break;

                    case Keys.Oemplus:
                        this.m_rdoAllDay.Checked = true;
                        break;

                    case Keys.OemMinus:
                        this.m_rdoToday.Checked = true;
                        break;

                    case Keys.Oemtilde:
                        this.m_cboOrderList.SelectedIndex = 0;
                        m_cboOrderList_SelectionChangeCommitted(null, null);
                        break;
                    case Keys.M:
                        this.m_cmdChange_Click(null, null);
                        break;
                    case Keys.X://ֹͣ(&X)
                        this.m_cmdStop_Click(null, null);
                        break;
                    case Keys.A://�ύ(&A)
                        this.m_cmdToCommit_Click(null, null);
                        break;
                    //case Keys.S://����(&S)
                    //    this.m_cmdRetract_Click(null, null);
                    //    break;
                    //case Keys.C://����(&C)
                    //    //this.m_cmdChange_Click(null, null);
                    //    if (this.m_dtvOrder.Focus())
                    //    {
                    //        if (m_MenuCopy.Enabled == true)
                    //        {
                    //            m_MenuCopy_Click(null, null);
                    //        }
                    //    }
                    //    break;
                    //case Keys.V://ճ��(&V)
                    //    //this.m_cmdChange_Click(null, null);
                    //    if (this.m_dtvOrder.Focus())
                    //    {
                    //        if (m_MenuPase.Enabled == true)
                    //        {
                    //            m_MenuPase_Click(null, null);
                    //        }
                    //    }
                    //    break;
                    case Keys.W://�¿�ҽ������ͳ��
                        m_cmdMoneyCount_Click(null, null);
                        break;
                    case Keys.Q://��������
                        CopytoGroup();
                        break;
                    case Keys.S://���˱����ѯ
                        m_MenuSeeBill_Click(null, null);
                        break;
                    case Keys.E:
                        tsmiApp_Click(null, null);
                        break;
                }

            }
            else
            {
                switch (e.KeyCode)
                {
                    #region ��ݼ�
                    case Keys.Escape:
                        //�����Ĺر�
                        if (this.label13.Visible == true)
                        {
                            this.label13.Visible = false;
                            break;
                        }
                        /*<=================*/
                        if (pnSel.Visible == true)
                        {
                            if (fo.Visible)
                            {
                                lblBoard_Click(null, null);

                            }
                            else if (m_lsvToolTip.Visible)
                            {
                                lbePriceInfo_Click(null, null);
                            }
                            else if (fl.Visible)
                            {
                                lblLeft_Click(null, null);
                            }
                            break;
                        }
                        if (m_cmdClose.Enabled && m_cmdClose.Visible)
                        {
                            //if(ActiveControl.ToString()!=m_dtgOrder.ToString())
                            m_cmdClose_Click(null, null);
                        }
                        break;
                    case Keys.F1://��ʾ����
                        if (this.label13.Visible == false)
                        {
                            this.label13.Show();
                        }

                        break;
                    case Keys.F2://���
                        if (m_cmdAdd.Enabled && m_cmdAdd.Visible)
                        {
                            m_cmdAdd_Click(sender, e);
                        }
                        break;

                    case Keys.F12://����
                        if (m_cmdSave.Enabled && m_cmdSave.Visible)
                        {
                            m_cmdSave_Click(sender, e);
                        }
                        break;
                    case Keys.F4://ʹ�÷�����ؼ�����
                        //if (m_cobOrderCate.SelectedIndex < m_cobOrderCate.Items.Count - 1)
                        //{
                        //    m_cobOrderCate.SelectedIndex = m_cobOrderCate.SelectedIndex + 1;
                        //}
                        //else
                        //{
                        //    m_cobOrderCate.SelectedIndex = 0;
                        //}
                        // this.m_ctlOrderDetail.m_lblDosageType_Click(null, null);
                        break;
                    case Keys.F5://���Ĳ�ѯ�뷽ʽ
                        if (seachClass.SelectedIndex < seachClass.Items.Count - 1)
                        {
                            seachClass.SelectedIndex = seachClass.SelectedIndex + 1;
                        }
                        else
                        {
                            seachClass.SelectedIndex = 0;
                        }
                        break;
                    case Keys.F6://ɾ��
                        //if (this.m_dtvOrder.SelectedRows.Count <= 0)
                        //{
                        //    MessageBox.Show("����ѡ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return;
                        //}
                        //clsBIHOrder BihOrder = (clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag;
                        //if (BihOrder != null && (BihOrder.m_intStatus == 0 || BihOrder.m_intStatus == 7))
                        //{
                        //    m_cmdDelete_Click(sender, e);
                        //}
                        //else if ((BihOrder.m_intStatus == 1))//����
                        //{
                        //    m_cmdBlankOut_Click(sender, e);
                        //}
                        m_cmdDelete2_Click(null, null);
                        break;

                    case Keys.F7://ֹͣ ȡϵͳʱ��
                        if (m_cmdStop.Enabled && m_cmdStop.Visible)
                        {
                            m_cmdStop_Click(sender, e);
                        }
                        break;
                    case Keys.F8://ֹͣ �˹�����ʱ��
                        if (m_objCurrentOrder != null && (m_objCurrentOrder.m_intStatus == 2))
                        {
                            m_ctlOrderDetail.m_dtFinishTime2.Enabled = true;
                            m_ctlOrderDetail.m_dtFinishTime2.Focus();
                        }
                        break;
                    case Keys.F9://���
                        m_mthShowMedicineInfo();
                        break;
                    case Keys.F10://ˢ��
                        //��ʾҽ������
                        //ˢ��
                        if (cmdRefurbish.Enabled && cmdRefurbish.Visible)
                        {
                            cmdRefurbish_Click(sender, e);
                        }
                        break;
                        //��ʱע�� 2005-10-11 by gphuang
                        //					if(m_IsDisPlayToolTip)
                        //					{
                        //						#region Label �ؼ�
                        //						//						m_lblToolTip.Visible =false;
                        //						//						m_IsDisPlayToolTip =false;
                        //						//						m_strToolTip ="";
                        //						#endregion
                        //						#region ListView �ؼ�
                        //						m_lsvToolTip.Visible =false;
                        //						m_IsDisPlayToolTip =false;
                        //						#endregion
                        //					}
                        //					else
                        //					{
                        //						if(m_objCurrentOrder!=null)
                        //						{
                        //							DisplayToolTip(new Point(100,m_poToolTip.Y));
                        //						}
                        //						m_IsDisPlayToolTip =true;
                        //					}
                        break;

                    case Keys.F11:
                        lblLeft_Click(null, null);
                        break;

                    case Keys.PageUp:
                        if (this.m_dtvOrder.SelectedRows.Count > 0)
                        {
                            if (this.m_dtvOrder.SelectedRows[0].Index > 0)
                            {
                                this.m_dtvOrder.Rows[this.m_dtvOrder.SelectedRows[0].Index - 1].Selected = true;
                                m_dtgOrder_m_evtCurrentCellChanged(null, null);

                            }
                        }
                        break;
                    case Keys.PageDown:
                        if (this.m_dtvOrder.SelectedRows.Count > 0)
                        {
                            if (this.m_dtvOrder.SelectedRows[0].Index < this.m_dtvOrder.RowCount - 1)
                            {
                                this.m_dtvOrder.Rows[this.m_dtvOrder.SelectedRows[0].Index + 1].Selected = true;
                                m_dtgOrder_m_evtCurrentCellChanged(null, null);

                            }
                        }
                        break;
                        #endregion
                }

            }
        }

        #region ��ʾҩƷ��ϸ��Ϣ
        /// <summary>
        /// ����ҩƷ������Ϣ���ҩ�����ʾҩƷ����Ϣ������һ��������ʾ
        /// </summary>
        private void m_mthShowMedicineInfo()
        {
            // 1 ��ҩ 2 ��ҩ 3 ����
            clsBIHOrder order;
            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag;
            }
            else
            {
                return;
            }
            clsDcl_DoctorWorkstation objSvc = new clsDcl_DoctorWorkstation();
            int Flag = 0;
            string strText = "";
            string strRemark = "";
            string ID = order.m_strCHARGEITEMID_CHR;
            if (m_objSpecateVo.m_strORDERCATEID_MEDICINE_CHR.Trim().Equals(order.m_strOrderDicCateID.Trim()))//��ҩ�ж�
            {
                Flag = 1;
            }
            else if (m_objSpecateVo.m_strMID_MEDICINE_CHR.Trim().Equals(order.m_strOrderDicCateID.Trim()))//��ҩ�ж�
            {
                Flag = 2;
            }
            else if (m_objSpecateVo.m_strORDERCATEID_LIS_CHR.Trim().Equals(order.m_strOrderDicCateID.Trim()))//�����ж�
            {
                Flag = 3;
            }
            if (Flag == 1 || Flag == 2)
            {
                objSvc.m_mthGetMedicineInfo(ID, out strText, out strRemark);
            }
            else if (Flag == 3)
            {
                DataTable dt;
                long l = objSvc.m_lngGetLisItemClinicMeaning(ID, out dt);
                if (l > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strText += "��" + dt.Rows[i]["check_item_name_vchr"].ToString().Trim() + "�� " + dt.Rows[i]["clinic_meaning_vchr"].ToString().Trim() + "\r\n\r\n";
                    }
                }
            }

            frmMedicineInfo obj = new frmMedicineInfo();
            obj.SetText = strText;
            obj.ShowDialog();
        }
        #endregion

        #region ��������
        /// <summary>
        /// �������� ��ǰ��������ҽ��
        /// </summary>
        private void CopytoGroup()
        {
            ArrayList m_arrBihOrder = null;

            if (this.m_dtvOrder.RowCount > 0)
            {
                m_arrBihOrder = new ArrayList();
                for (int i = 0; i < m_dtvOrder.RowCount; i++)
                {
                    //ҽ������Ϊ����ҽ����Ϊ����ҽ���Ĳ���������
                    clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.Rows[i].Tag;
                    if (!CanCopyToGroup(order))
                    {
                        continue;
                    }
                    m_arrBihOrder.Add((clsBIHOrder)m_dtvOrder.Rows[i].Tag);
                }

            }
            if (m_arrBihOrder.Count > 0)
            {
                frmOrderTemplate_Group_Add m_frmGroupAdd = new frmOrderTemplate_Group_Add((clsBIHOrder[])(m_arrBihOrder.ToArray(typeof(clsBIHOrder))), m_htOrderCate, m_objSpecateVo);
                m_frmGroupAdd.ShowDialog();
            }

        }

        public bool CanCopyToGroup(clsBIHOrder order)
        {
            bool m_blCan = true;
            //ҽ������Ϊ����ҽ���Ĳ���������
            if (order.m_intTYPE_INT != 0)
            {
                m_blCan = false;
            }
            /*<==============================*/
            return m_blCan;
        }

        /// <summary>
        /// �������� ��ǰ����ѡ�е�ҽ��
        /// </summary>
        private void CopySelectItemtoGroup()
        {

            List<clsBIHOrder> m_arrBihOrder = GetTheSelectItemWithSon();

            frmOrderTemplate_Group_Add m_frmGroupAdd = new frmOrderTemplate_Group_Add((m_arrBihOrder.ToArray()), m_htOrderCate, m_objSpecateVo);
            m_frmGroupAdd.m_htOrderCate = m_htOrderCate;
            m_frmGroupAdd.ShowDialog();
        }
        #endregion

        private void m_cmdToCommit_Click(object sender, System.EventArgs e)
        {
            #region ��ͯ�۸��ж�
            if (m_ctlPatient.m_objPatient != null && this.isUseChildPrice && this.lstYbPayTypeId != null && this.lstYbPayTypeId.IndexOf(m_ctlPatient.m_objPatient.m_strPayTypeID) >= 0)     // 2019-10-11 m_ctlPatient.m_objPatient.m_strPayTypeName.Contains("�Է�"))
            {
                clsBrithdayToAge clsB = new clsBrithdayToAge();
                DateTime dtmBirth = Convert.ToDateTime(m_ctlPatient.m_objPatient.m_dtBorn.ToString("yyyy-MM-dd"));
                DateTime dtmIn = Convert.ToDateTime(m_ctlPatient.m_objPatient.m_dtInHospital.ToString("yyyy-MM-dd"));
                DateTime dtmNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                // ��Ժʱ��!=����, �����Ƕ�ͯ, ����պõ�6��ʱ�ж��Ƿ�����;����
                if (dtmIn != dtmNow && clsB.IsChild(dtmBirth, dtmIn))
                {

                    TimeSpan ts = dtmNow.AddYears(-6).Subtract(dtmBirth);
                    if (ts.Days == 0)
                    {
                        DateTime? dtmMiddCharge = (new clsDcl_ExecuteOrder()).GetMiddChargeDate(m_ctlPatient.m_objPatient.m_strRegisterID);
                        // ����;���������;����ʱ��������ǰ?��?
                        if (dtmMiddCharge == null)
                        {
                            MessageBox.Show("���߽����������6�꣬���ڴ��ڶ�ͯ���ü�����Ŀ��\r\n\r\n���Ƚ�����;���㣬���ύҽ����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                //// ��Ժʱ�Ƕ�ͯ, ���ڲ��Ƕ�ͯ���ж��Ƿ�����;����
                //if (clsB.IsChild(m_ctlPatient.m_objPatient.m_dtBorn, dtmIn) == true && clsB.IsChild(m_ctlPatient.m_objPatient.m_dtBorn) == false)
                //{
                //    DateTime? dtmMiddCharge = (new clsDcl_ExecuteOrder()).GetMiddChargeDate(m_ctlPatient.m_objPatient.m_strRegisterID);
                //    // ����;���������;����ʱ��������ǰ?��?
                //    if (dtmMiddCharge == null)
                //    {
                //        MessageBox.Show("������ԺʱΪ6�����¶�ͯ����ǰ�����ѳ���6�꣬���ڴ��ڶ�ͯ���ü�����Ŀ��\r\n\r\n���Ƚ�����;���㣬���ύҽ����", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //}
                clsB = null;
            }
            #endregion

            if (this.m_ctlPatient.m_txtDiagnose.Text.Trim() == string.Empty)
            {
                MessageBox.Show("������ϲ���Ϊ�գ�����д��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_ctlPatient.m_txtDiagnose.Focus();
                return;
            }
            m_objDomain.m_mthShowCommitForm();
        }

        private void m_ctxOtherBill_Popup(object sender, System.EventArgs e)
        {
            m_objDomain.m_ctxOtherBill_Popup(m_ctxOtherBill);
        }

        private void m_lblOtherBill_Click(object sender, System.EventArgs e)
        {
            m_objDomain.m_lblOtherBill_Click(sender, e);
        }

        private void m_cmdClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private void m_ctlOrderDetail_m_evtInputEnd(object sender, System.EventArgs e)
        {
            m_cmdSave.Focus();
        }

        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="strMessage">��ʾ���ݴ�</param>
        internal void m_mthShowMessage(string strMessage)
        {
            MessageBox.Show(this, strMessage, "ҽ������", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// ���²˵�״̬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_ctxGridMenu_Popup(object sender, System.EventArgs e)
        {
            m_mnuBlankOut.Enabled = false;
            m_mnuDelete.Enabled = false;
            m_mnuStop.Enabled = false;
            m_mnuRetract.Enabled = false;
            m_mnuCommitAll.Enabled = false;

            if (m_objDomain.m_blnExistCurrentOrder())
            {
                int intStatus = m_objCurrentOrder.m_intStatus;
                int intType = m_objCurrentOrder.m_intExecuteType;
                switch (intStatus)
                {
                    case 0:
                        m_mnuDelete.Enabled = true;
                        break;
                    case 1:
                        m_mnuBlankOut.Enabled = true;
                        break;
                    case 2:
                        m_mnuStop.Enabled = true;
                        m_mnuRetract.Enabled = true;
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
                m_mnuCommitAll.Enabled = true;
            }
            else
            {
                m_mnuCommitAll.Enabled = true;
            }
        }

        public void cmdRefurbish_Click(object sender, System.EventArgs e)
        {
            //m_objDomain.m_mthLoadOrderList();
            //ˢ�²�������
            m_ctlPatient.m_mthGetPatientByAreaBed();
            //��ջ�������
            m_objDomain.m_ClearBuffer();
        }

        #endregion
        #region ���˸ı�ʱ,����ҽ��
        private void ctlBIHPatientInfo1_m_evtPatientChanged(object sender, System.EventArgs e)
        {
            m_objDomain.HintInfo();
            m_objDomain.m_mthLoadOrderList();
        }

        #endregion
        #region ����,�޸�
        /// <summary>
        /// ����,�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void m_cmdAdd_Click(object sender, System.EventArgs e)
        {
            // ��ǰ�Ƿ���ѡ������
            if (m_ConfirmPatient() == 0)
            {
                return;
            }
            m_ctlOrderDetail.m_lblSaveOrderID.Text = "ҽ������";
            m_ctlOrderDetail.m_txtOrderName2.ReadOnly = true;
            m_ctlOrderDetail.m_dtStartTime2.Text = DateTime.Now.ToString("yyyy��MM��dd��HHʱmm��");
            m_ctlOrderDetail.m_dtFinishTime2.Text = "";
            m_objCurrentOrder = null;
            m_intCurrentRow = -1;
            //��ʼ��ҽ���������
            //m_ctlOrderDetail.EmptyInput();
            //��ʾҽ����ϸ������Ϣ
            m_mthShowCurrentOrder(m_objCurrentOrder);

            m_ctlOrderDetail.IsSubOrder = false;//��ҽ����־
            m_ctlOrderDetail.ParentOrder = null;
            /*<===================================*/
            m_ctlOrderDetail.m_mthStartInput();
            if (m_objCurrentDoctor != null)
            {
                m_ctlOrderDetail.m_mthSetDoctor(m_objCurrentDoctor.m_strDoctorID, m_objCurrentDoctor.m_strDoctorName);
            }
            //ҽ�����͸ı�ʱ�Ŀ���(����/����/��Ժ��ҩ)
            int m_intExecuteType = clsConverter.ToInt(m_ctlOrderDetail.m_cboExecuteType.m_strGetID(m_ctlOrderDetail.m_cboExecuteType.SelectedIndex));
            m_ctlOrderDetail.m_cboExecuteTypeChanged(m_intExecuteType);
            m_cmdSave.Enabled = true;

            m_ctlOrderDetail.m_txtSample.Enabled = true;
            m_ctlOrderDetail.m_txtCheck.Enabled = true;

            // ���÷���
            if (this.m_dtvOrder.RowCount > 0)
            {
                m_ctlOrderDetail.m_txtRecipeNo.Text = m_objDomain.m_intBigRecipeNo.ToString();
            }
            // ����Ĭ�ϲ��˵�����ҽ��
            if (m_ctlPatient.m_objPatient != null)
            {
                m_ctlOrderDetail.m_txtDoctorList.Tag = m_ctlPatient.m_objPatient.m_strDOCTORID_CHR;
                m_ctlOrderDetail.m_txtDoctorList.Text = m_ctlPatient.m_objPatient.m_strDOCTOR_VCHR;
            }
            m_objDomain.m_SetDisplayOrderEditState(1);
        }

        #region �Ƿ��ͯ.������ö�ͯ�۸�
        /// <summary>
        /// �Ƿ��ͯ.������ö�ͯ�۸�
        /// </summary>
        internal bool IsChildPrice
        {
            get
            {
                if (m_ctlPatient.m_objPatient == null)
                {
                    return false;
                }
                else
                {
                    if (this.isUseChildPrice)
                        return new clsBrithdayToAge().IsChild(m_ctlPatient.m_objPatient.m_dtBorn);
                    else
                        return false;
                }
            }
        }
        #endregion

        #region ������������ж�

        // ��ǰ�Ƿ���ѡ������
        /// <summary>
        /// ��ǰ�Ƿ���ѡ������
        /// </summary>
        /// <returns>0-�޲���,1���в���</returns>
        public int m_ConfirmPatient()
        {
            int m_intP = 1;
            if (m_ctlPatient.m_objPatient == null)
            {
                m_mthShowMessage("����ָ������!");
                m_intP = 0;
                m_ctlPatient.Focus();

            }
            return m_intP;
        }

        // ��ǰ�Ƿ���ѡ��ҽ�������û�У���ѡ��ǰ���һ����
        /// <summary>
        /// ��ǰ�Ƿ���ѡ��ҽ��
        /// </summary>
        /// <returns>0-��ѡ��,1����ѡ��</returns>
        public int m_ConfirmHaveOrder()
        {
            int m_intP = 1;

            if (this.m_dtvOrder.RowCount <= 0)
            {
                m_mthShowMessage("��ѡ��ҽ��!");
                m_intP = 0;

            }
            else
            {
                if (this.m_dtvOrder.SelectedRows.Count <= 0)
                {
                    MessageBox.Show("����ѡ��һ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_intP = 0;
                }
            }
            return m_intP;
        }
        #endregion

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cmdSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.m_ctlPatient.m_txtDiagnose.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("������ϲ���Ϊ�գ�����д��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.m_ctlPatient.m_txtDiagnose.Focus();
                    return;
                }
                if (this.m_ctlOrderDetail.cboKJ.Enabled && this.m_ctlOrderDetail.cboKJ.SelectedIndex == 0)
                {
                    MessageBox.Show("����ҩ�����ѡ����;.", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.m_ctlOrderDetail.cboKJ.Focus();
                    return;
                }

                m_cmdSave.Enabled = false;
                if (m_ConfirmPatient() == 0)
                {
                    return;
                }
                ////ҽ������: ����ʱ����¼�룬��Ҫ�����趨�ò��˷������ޡ�
                //if(m_blnISMedicareMan)
                //{
                //    double dblPrePayMoney =0;
                //    double dblLIMITRATE_MNY =m_ctlPatient.m_objPatient.m_dblLIMITRATE_MNY;
                //    try{dblPrePayMoney =double.Parse(m_ctlPatient.m_txtPrePayMoney.Text.ToString());}
                //    catch{}

                //    if(dblLIMITRATE_MNY>dblPrePayMoney)
                //    {
                //        MessageBox.Show("Ԥ�������,������������,���ܿ�ҽ����","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
                //        return;
                //    }
                //}
                //��������֤
                //if(this.m_ctlOrderDetail.m_txtExecuteFreq.Tag==null) this.m_ctlOrderDetail.m_txtExecuteFreq.Tag ="";
                //if(!m_objDomain.PassConOrder(this.m_ctlOrderDetail.m_txtExecuteFreq.Tag.ToString())) 
                //{
                //    return;
                //}
                // ҽ������¼����
                if (!CheckTheOrderDetailData())
                {
                    return;
                }
                bool IsSubOrder = m_ctlOrderDetail.m_txtFatherOrder.Text.Trim() != "";
                int intParentIndex = -1;
                if (IsSubOrder)
                {
                    intParentIndex = (int)m_lngGetParentOrderIndex(m_objCurrentOrder.m_intRecipenNo.ToString().Trim());
                }
                this.Cursor = Cursors.WaitCursor;
                int iSaveRes = 0;
                //�������޸ĺ��ҽ����ˮ��
                ArrayList m_arrOrderId = new ArrayList();
                try
                {
                    m_objDomain.m_mthSave(out iSaveRes, ref m_arrOrderId);
                }
                catch (Exception objEx)
                {
                    MessageBox.Show(objEx.Message, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (iSaveRes < 0)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                this.m_objDomain.m_mthLoadOrderList();
                this.Cursor = Cursors.Default;

                m_ctlOrderDetail.m_txtExecuteFreq.Enabled = true;
                m_ctlOrderDetail.m_txtExecuteFreq.BackColor = Color.White;
                m_ctlOrderDetail.m_txtDosageType.Enabled = true;
                m_ctlOrderDetail.m_txtDosageType.BackColor = Color.White;

                this.Cursor = Cursors.Default;
            }
            finally
            {
                m_cmdSave.Enabled = true;
            }
        }

        public bool CheckTheOrderDetailData()
        {
            #region ҽ������
            if (this.m_ctlOrderDetail.m_txtOrderName2.Tag == null)
            {
                MessageBox.Show("��������ҽ��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_ctlOrderDetail.m_txtOrderName.Focus();
                return false;
            }
            if (this.m_ctlOrderDetail.m_txtOrderName.Text.Trim().Equals(""))
            {
                MessageBox.Show("��������ҽ��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_ctlOrderDetail.m_txtOrderName.Focus();
                return false;
            }
            #endregion

            #region �����������0
            double dbl1 = -1;
            if (m_ctlOrderDetail.m_txtDosage.Enabled == true && m_ctlOrderDetail.m_txtDosage.Visible == true)
            {
                try
                {
                    dbl1 = double.Parse(m_ctlOrderDetail.m_txtDosage.Text.Trim());
                }
                catch
                {

                }

                if (dbl1 <= 0)
                {
                    MessageBox.Show("�����������0��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_ctlOrderDetail.m_txtDosage.Focus();
                    return false;
                }
            }
            #endregion

            #region �÷�
            if (m_ctlOrderDetail.m_txtDosageType.Enabled == true && m_ctlOrderDetail.m_txtDosageType.Visible == true)
            {
                if (m_ctlOrderDetail.m_txtDosageType.Tag == null)
                {
                    MessageBox.Show("�÷�����Ϊ�գ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    m_ctlOrderDetail.m_txtDosageType.Focus();
                    return false;
                }
                else if (((string)m_ctlOrderDetail.m_txtDosageType.Tag).Equals("") || m_ctlOrderDetail.m_txtDosageType.Text.Trim().Equals(""))
                {
                    MessageBox.Show("�÷�����Ϊ�գ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    m_ctlOrderDetail.m_txtDosageType.Focus();
                    return false;
                }
            }
            #endregion

            #region �����������0
            /*
            dbl1 = -1;
            if (m_ctlOrderDetail.m_txtGet.Enabled == true)
            {
                try
                {
                    dbl1 = double.Parse(m_ctlOrderDetail.m_txtGet.Text.Trim());
                }
                catch
                {

                }

                if (dbl1 <= 0)
                {
                    MessageBox.Show("�����������0��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                    m_ctlOrderDetail.m_txtGet.Focus();
                    return false;
                }
            }
             */
            #endregion

            #region �����Ŀ�ʼʱ�䲻��Ϊ��
            /*
            if(int.Parse(m_ctlOrderDetail.m_cboExecuteType.m_strGetID(m_ctlOrderDetail.m_cboExecuteType.SelectedIndex))==1)
            {
             
                try
                {
                 DateTime dt=Convert.ToDateTime( m_ctlOrderDetail.m_dtStartTime2.Text.Trim());
                }
                catch
                {
                  MessageBox.Show("�����뿪ʼ����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                m_ctlOrderDetail.m_dtStartTime2.Focus();
                return false;
                }
             
            }
             */
            #endregion

            #region ����ҽ��
            /*
            if(m_ctlOrderDetail.m_txtDoctorList.Tag==null||((string)m_ctlOrderDetail.m_txtDoctorList.Tag).Equals("")||m_ctlOrderDetail.m_txtDoctorList.Text.Trim().Equals(""))
            {
                  MessageBox.Show("����ҽ������Ϊ��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                m_ctlOrderDetail.m_txtDoctorList.Focus();
                return false;
              
            }
             */
            #endregion

            #region Ƶ��
            string freqId = string.Empty;
            if (m_ctlOrderDetail.m_txtExecuteFreq.Enabled == true && m_ctlOrderDetail.m_txtExecuteFreq.Visible == true)
            {
                if (m_ctlOrderDetail.m_txtExecuteFreq.Tag == null)
                {
                    MessageBox.Show("Ƶ�ʲ���Ϊ��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_ctlOrderDetail.m_txtExecuteFreq.Focus();
                    return false;
                }
                else if (((string)m_ctlOrderDetail.m_txtExecuteFreq.Tag).Equals("") || m_ctlOrderDetail.m_txtExecuteFreq.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Ƶ�ʲ���Ϊ��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    m_ctlOrderDetail.m_txtExecuteFreq.Focus();
                    return false;
                }
                freqId = clsConverter.ToString(m_ctlOrderDetail.m_txtExecuteFreq.Tag);
            }
            #endregion

            #region �����������0
            dbl1 = -1;
            if (m_ctlOrderDetail.m_txtDays.Enabled == true && m_ctlOrderDetail.m_txtDays.Visible == true)
            {
                try
                {
                    dbl1 = double.Parse(m_ctlOrderDetail.m_txtDays.Text.Trim());
                }
                catch
                {
                }
                if (dbl1 <= 0)
                {
                    MessageBox.Show(m_ctlOrderDetail.m_lblDay.Text + "������Ϊ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_ctlOrderDetail.m_txtDays.Focus();
                    return false;
                }

                //ҽ�����˳�Ժ��ҩ�������������ܳ���7��
                if (this.m_ctlOrderDetail.m_cboExecuteType.SelectedIndex == 2)
                {
                    if (m_lstSocialSecurity.Contains(m_ctlPatient.m_objPatient.m_strPayTypeID))
                    {
                        if (dbl1 > 7)
                        {
                            MessageBox.Show("�ò���Ϊ" + m_ctlPatient.m_objPatient.m_strPayTypeName + "��ݣ���Ժ��ҩ���ܳ���7��!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            m_ctlOrderDetail.m_txtDays.Focus();
                            return false;

                        }
                    }
                }
            }
            #endregion

            #region �Ƴ���ҩ����

            //if (this.m_ctlOrderDetail.txtCureDays.Enabled && this.m_ctlOrderDetail.txtCureDays.Text.Trim() != "")
            //{
            //    int days = Convert.ToInt32(this.m_ctlOrderDetail.txtCureDays.Text.Trim());
            //    if (days > 0)
            //    {
            //        if (string.IsNullOrEmpty(freqId))
            //        {
            //            MessageBox.Show("Ƶ�ʲ���Ϊ��");
            //            this.m_ctlOrderDetail.txtCureDays.Focus();
            //            return false;
            //        }
            //        using (clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject())
            //        {
            //            DataTable dt = svc.GetFreqDict(freqId);
            //            if (dt != null && dt.Rows.Count == 1)
            //            {
            //                int freqDay = dt.Rows[0]["days_int"] == DBNull.Value ? 1 : Convert.ToInt32(dt.Rows[0]["days_int"]);
            //                if (freqDay == 0) freqDay = 1;
            //                int mod = days % freqDay;
            //                if (mod != 0)
            //                {
            //                    MessageBox.Show("�Ƴ�����������Ƶ�������ı���");
            //                    this.m_ctlOrderDetail.txtCureDays.Focus();
            //                    return false;
            //                }
            //            }
            //        }
            //        if (days > 15)
            //        {
            //            MessageBox.Show("�Ƴ��������ܴ���15��");
            //            this.m_ctlOrderDetail.txtCureDays.Focus();
            //            return false;
            //        }
            //    }
            //}
            #endregion

            return true;
        }
        #endregion

        #region �ύ����ҽ��
        private void m_mnuCommitAll_Click(object sender, System.EventArgs e)
        {
            this.m_cmdToCommit_Click(sender, e);
        }

        private void m_cmdCommitAll_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(this, "�Ƿ��ύ�����½�ҽ��?", "�ύҽ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            m_objDomain.m_mthCommitAll();
        }
        #endregion
        #region ֹͣҽ��
        private void m_mnuStop_Click(object sender, System.EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count <= 0)
            {
                m_mthShowMessage("����ѡ��ҽ��!");
                return;
            }
            m_lngStopOrder();
        }

        public void m_cmdStop_Click(object sender, System.EventArgs e)
        {
            if (m_ConfirmHaveOrder() == 0)
            {
                return;
            }

            m_lngStopOrder();
            //��Ctrl������ҳ������
            //if(System.Windows.Forms.Control.ModifierKeys==Keys.Control || System.Windows.Forms.Control.ModifierKeys==Keys.ControlKey)
            //{
            //    clsBIHPatientInfo objPatient =m_ctlPatient.m_objPatient;
            //    if(objPatient!=null)
            //    {	
            //        string strRegisterID =objPatient.m_strRegisterID;
            //        frmReformingOrder objfrmReformingOrder =new frmReformingOrder(strRegisterID,3);
            //        objfrmReformingOrder.m_txbPatientName.Text =m_ctlPatient.m_objPatient.m_strPatientName;
            //        objfrmReformingOrder.ShowDialog();
            //        //ˢ������
            //        m_objDomain.m_mthLoadOrderList();
            //    }
            //}
            //else
            //{
            //    //����ֹͣ
            //    if (this.m_dtvOrder.SelectedRows.Count <= 0)
            //    {
            //        m_mthShowMessage("����ѡ��ҽ��!");
            //        return;
            //    }
            //    clsBIHOrder BihOrder = (clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag;
            //    m_objDomain.m_mthStopCurrentOrder(true, BihOrder);

            //}
        }
        #endregion
        #region ����ҽ��
        private void m_mnuRetract_Click(object sender, System.EventArgs e)
        {
            m_cmdRetract_Click(sender, e);
            //m_objDomain.m_mthRetractCurrentOrder(false);
        }

        private void m_cmdRetract_Click(object sender, System.EventArgs e)
        {
            if (m_ConfirmHaveOrder() == 0)
            {
                return;
            }
            //��Ctrl������ҳ������
            //if(System.Windows.Forms.Control.ModifierKeys==Keys.Control || System.Windows.Forms.Control.ModifierKeys==Keys.ControlKey)
            //{
            clsBIHPatientInfo objPatient = m_ctlPatient.m_objPatient;
            if (objPatient != null)
            {
                string strRegisterID = objPatient.m_strRegisterID;
                frmReformingOrder objfrmReformingOrder = new frmReformingOrder(strRegisterID, 4, this.IsChildPrice);
                objfrmReformingOrder.m_txbPatientName.Text = m_ctlPatient.m_objPatient.m_strPatientName;
                objfrmReformingOrder.ShowDialog();
                //ˢ������
                m_objDomain.m_mthLoadOrderList();
            }
            //}
            //else
            //{
            //��������
            //m_objDomain.m_mthRetractCurrentOrder(true);
            //}
        }
        #endregion
        #region ����ҽ��
        private void m_mnuBlankOut_Click(object sender, System.EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count <= 0)
            {

                MessageBox.Show("����ѡ��һ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //m_objDomain.m_mthBlankOutCurrentOrder(true,(clsBIHOrder) this.m_dtvOrder.SelectedRows[0].Tag);
            m_lngBlankOut();
        }

        private void m_cmdBlankOut_Click(object sender, System.EventArgs e)
        {
            if (m_ConfirmHaveOrder() == 0)
            {
                return;
            }
            clsBIHOrder BihOrder = (clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag;

            if (BihOrder.m_strCreatorID == this.LoginInfo.m_strEmpID || BihOrder.m_strDOCTORID_CHR == this.LoginInfo.m_strEmpID)
            {

            }
            else
            {
                MessageBox.Show("ֻ������¼��Ա���Լ��򿪷�ҽ�����Լ���ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            m_objDomain.m_mthBlankOutCurrentOrder(true, BihOrder);
        }

        #endregion
        #region ɾ��ҽ��
        private void m_cmdDelete_Click(object sender, System.EventArgs e)
        {

            if (m_ConfirmHaveOrder() == 0)
            {
                return;
            }
            clsBIHOrder BihOrder = (clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag;
            if (BihOrder.m_strCreatorID == this.LoginInfo.m_strEmpID || BihOrder.m_strDOCTORID_CHR == this.LoginInfo.m_strEmpID)
            {

            }
            else
            {
                MessageBox.Show(" {��ǰҽ����" + BihOrder.m_strName + "} " + "ֻ��ɾ��¼��Ա���Լ��򿪷�ҽ�����Լ���ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            m_objDomain.m_mthDeleteCurrentOrder(true, BihOrder);
            this.alertLight1.m_mthClear();
            //ҽ�����͸ı�ʱ�Ŀ���(����/����/��Ժ��ҩ)
            m_ctlOrderDetail.m_cboExecuteType_SelectedIndexChanged(null, null);
            if (!m_ctlOrderDetail.IsSubOrder)
            {

                m_ctlOrderDetail.m_txtDosageType.Enabled = true;
                m_ctlOrderDetail.m_txtDosageType.BackColor = Color.White;
            }
        }

        private void m_mnuDelete_Click(object sender, System.EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count <= 0)
            {
                MessageBox.Show("����ѡ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //m_objDomain.m_mthDeleteCurrentOrder(true,(clsBIHOrder) this.m_dtvOrder.SelectedRows[0].Tag);		
            m_cmdDelete2_Click(null, null);

        }
        #endregion
        #region Grid
        /// <summary>
        /// ��ǰ��
        /// </summary>
        internal int m_intCurrentRow = -1;
        private void m_dtgOrder_m_evtCurrentCellChanged(object sender, System.EventArgs e)
        {
            //���ϼ�ɾ������

            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                clsBIHOrder BihOrder = (clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag;
                if (m_blDoctorSign)
                {
                    if (BihOrder.SIGN_INT == 0)
                    {
                        this.m_dtvOrder.DefaultCellStyle.SelectionForeColor = Color.Red;
                    }
                    else
                    {
                        this.m_dtvOrder.DefaultCellStyle.SelectionForeColor = selectOldColor;
                    }
                }
                ///ѡ�еķ����б�Ϊ��ͬ��ѡ��
                ArrayList m_arrRecipenNo = new ArrayList();
                for (int i = 0; i < this.m_dtvOrder.SelectedRows.Count; i++)
                {
                    clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[i].Tag;
                    if (m_arrRecipenNo.Contains(order.m_intRecipenNo.ToString()) == false)
                    {
                        m_arrRecipenNo.Add(BihOrder.m_intRecipenNo.ToString());
                    }

                }
                for (int i = 0; i < this.m_dtvOrder.Rows.Count; i++)
                {
                    clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.Rows[i].Tag;
                    if (m_arrRecipenNo.Contains(order.m_intRecipenNo.ToString()))
                    {
                        this.m_dtvOrder.Rows[i].Selected = true;
                    }
                }
                /*<====================================*/
            }
            //ˢ�»������Ϣ	{����}
            if (m_lsvToolTip.Visible == true)
            {
                m_dtvOrder_CellMouseClick(null, null);
            }
        }

        public void CurrentBihOrderChanged()
        {
            // 2019-09-17 ���ύ�ļ�����ҽ���������޸�
            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                m_objCurrentOrder = this.m_dtvOrder.SelectedRows[0].Tag as clsBIHOrder;
                string barCode = (new weCare.Proxy.ProxyIP()).Service.GetBarCodeByOrderId(m_objCurrentOrder.m_strOrderID);
                if (string.IsNullOrEmpty(barCode))
                {
                    // �����޸�
                }
                else
                {
                    m_objCurrentOrder = null;
                    for (int i = 0; i < this.m_dtvOrder.Rows.Count; i++)
                    {
                        if (this.m_dtvOrder.Rows[i].Selected)
                            this.m_dtvOrder.Rows[i].Selected = false;
                    }
                    MessageBox.Show("���ύ�ļ�����ҽ�����������޸ġ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            // ����ֵ��ʼ��
            m_ctlOrderDetail.m_blSampleItem = false;
            m_ctlOrderDetail.m_blCheckItem = false;
            m_ctlOrderDetail.m_lblDosageType.Visible = true;
            m_ctlOrderDetail.m_txtDosageType.Visible = true;
            this.m_txtBackReason.Visible = false;

            #region ҽ����ϸ��ʾ
            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                m_objCurrentOrder = this.m_dtvOrder.SelectedRows[0].Tag as clsBIHOrder;

                #region ˢ�µ�ǰҽ�����ݣ�Ȼ�����ж�
                List<string> m_arrORDERID_CHR = new List<string>();
                m_arrORDERID_CHR.Add(m_objCurrentOrder.m_strOrderID);
                clsBIHOrder[] arrOrder = null;
                (new weCare.Proxy.ProxyIP()).Service.m_lngGetArrOrderByOrderID(m_arrORDERID_CHR, out arrOrder);
                if (arrOrder != null && arrOrder.Length > 0)
                {
                    m_objCurrentOrder = arrOrder[0];
                    DataGridViewRow row = GetTheGridRowByOrder(m_objCurrentOrder.m_strOrderID);
                    this.m_objDomain.m_objGetDataViewRow(m_objCurrentOrder, row, row.Index + 1);
                    this.m_objDomain.m_mthRefreshSameReqNoColor();
                }
                else
                {
                    DataGridViewRow row = GetTheGridRowByOrder(m_objCurrentOrder.m_strOrderID);
                    m_dtvOrder.Rows.Remove(row);
                    MessageBox.Show("����ҽ���Ѳ�����!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                if (!alertFromTheStatue(m_objCurrentOrder))
                {
                    m_objCurrentOrder = null;
                    return;
                }
                //��ʼ��ҽ���������
                m_ctlOrderDetail.EmptyInput();

                //���ݵ�ǰ����ID���õ�ǰ������Ŀ
                clsBIHOrderDic[] arrDic = null;
                (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderDicByID(m_objCurrentOrder.m_strOrderDicID.ToString().Trim(), m_strMedDeptGross, out arrDic);
                if (arrDic.Length > 0)
                {
                    m_ctlOrderDetail.m_txtOrderName.Tag = arrDic[0];
                    m_objCurrentOrder.m_dmlDosageRate = arrDic[0].m_dmlDosageRate;
                    //��¼��ҽ����ҩƷ�����
                    this.m_fotOpcurrentgross_num = Convert.ToSingle(arrDic[0].m_dmlIPCURRENTGROSS_NUM);

                    //��¼�Ƿ���ҩƷ��־ 1��ҩƷ 2Ϊ����
                    this.m_intITEMSRCTYPE_INT = arrDic[0].m_intITEMSRCTYPE_INT;
                    //�޸�ҽ����־
                    this.m_blIsChange = true;
                    m_objCurrentOrder.m_dmlPrice = arrDic[0].m_dmlPrice;
                    m_objCurrentOrder.m_dmlPACKQTY_DEC = arrDic[0].m_dmlPACKQTY_DEC;
                    m_objCurrentOrder.m_intIPCHARGEFLG_INT = arrDic[0].m_intIPCHARGEFLG_INT;
                    if (!m_ctlOrderDetail.m_htMEDICINEPREPTYPE.ContainsKey(arrDic[0].m_strOrderDicID))
                    {
                        m_ctlOrderDetail.m_htMEDICINEPREPTYPE.Add(arrDic[0].m_strOrderDicID, arrDic[0].m_strMEDICINEPREPTYPENAME_VCHR);
                    }
                }
                //��ʾҽ����ϸ������Ϣ
                m_mthShowCurrentOrder(m_objCurrentOrder);

                //��ʾ������Ϣ 
                m_objDomain.m_DisPlayToolTipListView(m_objCurrentOrder, m_lsvToolTip);
                m_objDomain.SetButtonToEnable();
                //��ʾ��ҩ��ʽ����ʾ���������߼�
                if (m_objCurrentOrder != null)
                {
                    m_ctlOrderDetail.IsSubOrder = false;
                    m_ctlOrderDetail.ParentOrder = null;
                    if (GetTheSameNoOrders(m_objCurrentOrder.m_intRecipenNo).Count > 1)
                    {
                        m_ctlOrderDetail.IsSubOrder = true;
                    }

                    clsT_aid_bih_ordercate_VO p_objItem;
                    string p_strOrdercateID = m_objCurrentOrder.m_strOrderDicCateID;
                    p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[p_strOrdercateID];

                    //����ҽ��¼�����ؼ�
                    EnableTheBihDetailControl(true);
                    //ҽ��¼�뷽ʽ�������
                    //m_ctlOrderDetail.m_cboExecuteTypeChanged();
                    m_ctlOrderDetail.m_cboExecuteType_SelectedIndexChanged(null, null);
                    //ҽ�����ͽ������
                    m_ctlOrderDetail.OrdercateLogic(p_objItem);
                    //ҽ������������(��Ƶ���Ƿ�����޸�)
                    m_ctlOrderDetail.OrderSpecialLogic(m_objCurrentOrder);
                    //�¼۱��߼�
                    if (m_ctlOrderDetail.m_txtOrderName.Tag is clsBIHOrderDic)
                    {
                        clsBIHOrderDic m_objDic = (clsBIHOrderDic)m_ctlOrderDetail.m_txtOrderName.Tag;
                        if (m_objDic.m_intCheckTypeID != -1 && m_objDic.m_strCheckType.Trim().Length > 0 && m_objDic.m_intDELETED == 0)
                        {
                            m_ctlOrderDetail.m_blCheckItem = true;
                        }
                        else
                        {
                            m_ctlOrderDetail.m_blCheckItem = false;
                        }
                        //�¼���������߼�--�������뵥Ԫ�߼�
                        if (!m_objDic.m_strLISAPPLYUNITID_CHR.Trim().Equals("") && m_objDic.m_strOrderCateID.Trim().Equals(m_objSpecateVo.m_strORDERCATEID_LIS_CHR.Trim()))
                        {
                            m_ctlOrderDetail.m_blSampleItem = true;
                        }
                        else
                        {
                            m_ctlOrderDetail.m_blSampleItem = false;
                        }
                    }
                    //������ҽ���������
                    if (m_objSpecateVo.m_strCONFREQID_CHR.Trim().Equals(m_objCurrentOrder.m_strExecFreqID.Trim()))
                    {
                        m_ctlOrderDetail.m_txtDosageControl(false);
                        m_ctlOrderDetail.m_txtDosageTypeControl(false);
                    }
                    //ͬ��ҽ���޸�ʱ�Ľ������
                    bool m_blParentOrder, m_blSubOrder;
                    TheChangedOrderParentOrSub(m_objCurrentOrder, out m_blParentOrder, out m_blSubOrder);
                    if (m_blParentOrder == false && m_blSubOrder == true)
                    {
                        //���û���� ��ҽ��¼�����Ŀؼ�
                        EnableTheBihDetailControl(false);
                    }
                    m_objDomain.m_SetDisplayOrderEditState(2);
                }
            }
            #endregion
            // �������
            m_ctlOrderDetail.setTheSampleBox();
            // ������
            m_ctlOrderDetail.setTheCheckBox();
            // ��Ӧ֢
            string strShiying = string.Empty;
            long lngRes1 = this.m_objDomain.m_lngGetShiying(m_objCurrentOrder.m_strOrderID, m_objCurrentOrder.m_strCHARGEITEMID_CHR, out strShiying);
            if (lngRes1 > 0)
            {
                if (strShiying == "3")
                {
                    m_ctlOrderDetail.cboShiying.SelectedIndex = 1;
                }
                else
                {
                    m_ctlOrderDetail.cboShiying.SelectedIndex = 0;
                }
            }

            //if (m_ctlOrderDetail.txtCureDays.Text.Trim() != "") m_ctlOrderDetail.txtCureDays.Enabled = true;
            if (m_ctlOrderDetail.cboEmer.SelectedIndex > 0) m_ctlOrderDetail.cboEmer.Enabled = true;
            if (m_ctlOrderDetail.cboKJ.SelectedIndex > 0) m_ctlOrderDetail.cboKJ.Enabled = true;
            if (m_ctlOrderDetail.cboQK.SelectedIndex > 0) m_ctlOrderDetail.cboQK.Enabled = true;
            if (m_ctlOrderDetail.cboProxyBoil.SelectedIndex > 0) m_ctlOrderDetail.cboProxyBoil.Enabled = true;
        }

        /// <summary>
        /// ��ֹҽ���������ؼ�
        /// </summary>
        public void DeableTheDetailControl()
        {
            //��ʼ������ؼ�
            m_ctlOrderDetail.m_cboExecuteType.Enabled = false;
            m_ctlOrderDetail.m_txtRecipeNo.Enabled = false;
            m_ctlOrderDetail.m_txtOrderName.Enabled = false;
            m_ctlOrderDetail.m_txtOrderName2.Enabled = false;
            m_ctlOrderDetail.m_txtDosage.Enabled = false;
            m_ctlOrderDetail.m_txtDosageType.Enabled = false;
            m_ctlOrderDetail.m_txtExecuteFreq.Enabled = false;
            m_ctlOrderDetail.m_txtGet.Enabled = false;
            m_ctlOrderDetail.m_txtDays.Enabled = false;
            m_ctlOrderDetail.m_txtATTACHTIMES_INT.Enabled = false;
            m_ctlOrderDetail.m_txtDoctorList.Enabled = false;
            m_ctlOrderDetail.m_txtDoctorList.BackColor = SystemColors.Control;
            m_ctlOrderDetail.m_cboRateType.Enabled = false;
            m_ctlOrderDetail.m_txtEntrust.Enabled = false;
            m_ctlOrderDetail.m_dtStartTime2.Enabled = false;
            m_ctlOrderDetail.m_dtStartTime2.BackColor = SystemColors.Control;
            m_ctlOrderDetail.m_dtFinishTime2.Enabled = false;
            m_ctlOrderDetail.m_dtFinishTime2.BackColor = SystemColors.Control;
            //m_ctlOrderDetail.txtCureDays.Enabled = false;
            m_ctlOrderDetail.cboKJ.Enabled = false;
            m_ctlOrderDetail.cboQK.Enabled = false;
            m_ctlOrderDetail.cboProxyBoil.Enabled = false;
            m_ctlOrderDetail.cboEmer.Enabled = false;
        }

        /// <summary>
        /// �жϵ�ǰҽ���Ǹ�ҽ��������ҽ�����ڴ���ͬ��������£�
        /// </summary>
        /// <param name="m_objCurrentOrder">��ǰҽ��</param>
        /// <param name="m_blParentOrder">��ǰҽ���Ǹ�ҽ��(true-��,false-����)</param>
        /// <param name="m_blSubOrder">��ǰҽ������ҽ��(true-��,false-����)</param>
        public void TheChangedOrderParentOrSub(clsBIHOrder m_objCurrentOrder, out bool m_blParentOrder, out bool m_blSubOrder)
        {
            int Count = 0;
            m_blParentOrder = false;
            m_blSubOrder = false;
            for (int i = 0; i < this.m_dtvOrder.Rows.Count; i++)
            {
                clsBIHOrder bihOrder = (clsBIHOrder)this.m_dtvOrder.Rows[i].Tag;
                //if (bihOrder.m_strOrderID == m_objCurrentOrder.m_strOrderID)
                //{
                //    continue;
                //}
                if (bihOrder.m_intRecipenNo == m_objCurrentOrder.m_intRecipenNo)
                {
                    Count++;
                }
                if (Count > 0 && bihOrder.m_intRecipenNo == m_objCurrentOrder.m_intRecipenNo)
                {
                    if (Count == 1)
                    {
                        m_blParentOrder = true;
                        if (bihOrder.m_strOrderID == m_objCurrentOrder.m_strOrderID)
                        {
                            return;
                        }
                    }
                    else
                    {
                        m_blParentOrder = false;
                    }

                }

            }
            if (Count > 1)
            {
                m_blSubOrder = true;

            }
        }

        /// <summary>
        /// ��ǰҽ�����Ƿ�����޸�ҽ���ľ���
        /// </summary>
        /// <param name="m_objCurrentOrder"></param>
        /// <returns></returns>
        private bool alertFromTheStatue(clsBIHOrder m_objCurrentOrder)
        {
            if (m_objCurrentOrder.m_intStatus == 0 || m_objCurrentOrder.m_intStatus == 1 || m_objCurrentOrder.m_intStatus == 7)
            {
            }
            else
            {
                MessageBox.Show("ת�����ҽ�����ܽ����޸�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //if (m_objCurrentOrder.m_strCREATEAREA_ID != this.LoginInfo.m_strInpatientAreaID)
            //{
            //    MessageBox.Show("��ҽ���Ǳ���ҽ���������Խ����޸�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            if (!m_blUsePowerHander(m_objCurrentOrder))
            {
                MessageBox.Show("û���㹻Ȩ�޴���ǰҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        public bool m_blUsePowerHander(clsBIHOrder m_objCurrentOrder)
        {
            bool m_blHander = true;
            if (!m_objCurrentOrder.m_strCreatorID.Equals(this.LoginInfo.m_strEmpID))
            {
                if (!m_objCurrentOrder.m_strDOCTORID_CHR.Equals(this.LoginInfo.m_strEmpID))
                {
                    m_blHander = false;
                }
            }
            return m_blHander;
        }

        /// <summary>
        /// ��DataGrid������Ҳ��ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_dtgOrder_m_evtMouseDownCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMousePressEventArgs e)
        {
            //m_dtgOrder.CurrentCell=new DataGridCell(e.m_intRowNumber,e.m_intColNumber);
            System.Drawing.Point pt = new Point(e.X, e.Y);
            pt.Offset(this.m_dtvOrder.Location.X, this.m_dtvOrder.Location.Y);
            System.Drawing.Point po;
            m_poToolTip = new System.Drawing.Point(pt.X, pt.Y + 20 + 66);
            if (m_IsDisPlayToolTip)
            {
                DisplayToolTip(m_poToolTip);
            }
            #region ToolTip Lable�ؼ���ʾ
            //			//m_strToolTip =m_objDomain.strGetToolTipText(m_objCurrentOrder);
            //			if(!m_IsDisPlayToolTip || m_strToolTip.Trim()=="") return;
            //			#region ��ʾToolTip
            //			m_lblToolTip.Text = m_strToolTip + "��ܰ��ʾ��{F10��---����ҽ��������ʾ��Ϣ��}";
            //			m_lblToolTip.Visible =true;
            //			System.Drawing.Point pt=new Point(e.X,e.Y);
            //			pt.Offset(m_dtgOrder.Location.X,m_dtgOrder.Location.Y);
            //			System.Drawing.Point po;		
            //			po =new System.Drawing.Point(pt.X,pt.Y + 20);
            //			m_lblToolTip.Location =po;
            //			#endregion
            #endregion
        }

        private void m_dtgOrder_m_evtDoubleClickCell(object sender, com.digitalwave.controls.datagrid.clsDGTextMouseClickEventArgs e)
        {
            DisplayBackReason(m_poToolTip);
        }

        /// <summary>
        /// ��ʾToolTip	ListView�ؼ�
        /// </summary>
        /// <param name="po">�Ѿ���õ�</param>
        private void DisplayToolTip(System.Drawing.Point po)
        {
            //m_objDomain.m_DisPlayToolTipListView(m_objCurrentOrder,m_lsvToolTip);
            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag;
                m_objDomain.m_DisPlayToolTipListView(order, m_lsvToolTip);
            }
            m_lsvToolTip.Visible = true;
            m_lsvToolTip.Location = po;
        }
        /// <summary>
        /// ��ʾ�˻�ԭ��
        /// </summary>
        /// <param name="po">�Ѿ���õ�</param>
        private void DisplayBackReason(System.Drawing.Point po)
        {
            if (m_objCurrentOrder == null || m_objCurrentOrder.m_intStatus != 7) return;
            string strTem = "";
            strTem += "�˻�ԭ��" + ((m_objCurrentOrder.m_strBACKREASON == null) ? ("") : (m_objCurrentOrder.m_strBACKREASON.Trim()));
            strTem += "\r\n�˻ػ�ʿ��" + m_objCurrentOrder.m_strSENDBACKER_CHR;
            strTem += "\r\n�˻�ʱ�䣺" + m_objCurrentOrder.m_strSENDBACK_DAT;
            this.m_txtBackReason.Text = strTem;
            this.m_txtBackReason.Visible = true;
            this.m_txtBackReason.Location = po;
        }
        #endregion
        #region ��ʾ����
        /// <summary>
        /// ����ʾ����
        /// </summary>
        internal bool m_blnShowOnlyToday = false;
        /// <summary>
        /// ��ʾҽ��������
        /// </summary>
        internal int m_intShowOrderExecuteType = 0;
        /// <summary>
        /// �Ƿ����ʾƤ�Ե�ҽ��
        /// </summary>
        internal bool m_blnNeedFeel = false;
        internal void m_mthShowConditionChange(object sender, EventArgs e)
        {
            bool blnOnlyToday = m_rdoToday.Checked;
            int intExecuteType = 0;
            if (m_rdoLongType.Checked)
                intExecuteType = 1;
            else if (m_rdoTempType.Checked)
                intExecuteType = 2;
            bool blnNeedFeel = m_chkNeedFeel.Checked;

            if ((m_blnShowOnlyToday != blnOnlyToday) || (m_intShowOrderExecuteType != intExecuteType) || (m_blnNeedFeel != blnNeedFeel))
            {
                m_blnShowOnlyToday = blnOnlyToday;
                //m_intShowOrderExecuteType=intExecuteType;
                m_blnNeedFeel = blnNeedFeel;
                this.Cursor = Cursors.WaitCursor;
                m_objDomain.m_mthLoadOrderList();
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;}
        /// </summary>
        internal int[] m_arrStatus = new int[] { 0, 1, 2, 3 };		//������ʾ������ҽ����״̬
        internal void m_mthShowStatusChange(object sender, EventArgs e)
        {	//ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;}
            //ArrayList arlStatus=new ArrayList();
            //if(m_chkStatus0.Checked) 
            //{
            //    arlStatus.Add(0);
            //    arlStatus.Add(7);
            //}
            //if(m_chkStatus1.Checked) 
            //{
            //    arlStatus.Add(1);
            //    arlStatus.Add(5);
            //}
            //if(m_chkStatus2.Checked) arlStatus.Add(2);
            //if(m_chkStatus3.Checked) 
            //{
            //    arlStatus.Add(3);
            //    arlStatus.Add(6);
            //} 
            //if (m_chkStatus4.Checked)
            //{
            //    arlStatus.Add(-1);
            //}
            ///*<-----------------------------------*/

            //m_arrStatus=(int[])(arlStatus.ToArray(typeof(int)));
            //this.Cursor =Cursors.WaitCursor;
            //m_objDomain.m_mthLoadOrderList();
            //this.Cursor =Cursors.Default;
        }

        #endregion
        #region iLoginInfo ��Ա
        internal weCare.Core.Entity.clsLoginInfo m_objLoginInfo = null;
        public new weCare.Core.Entity.clsLoginInfo LoginInfo
        {
            get
            {
                // TODO:  ��� frmBIHOrderInput.LoginInfo getter ʵ��
                return m_objLoginInfo;
            }
            set
            {
                // TODO:  ��� frmBIHOrderInput.LoginInfo setter ʵ��
                m_objLoginInfo = value;
                if ((m_objCurrentDoctor == null) && (m_objLoginInfo != null))
                {
                    m_objCurrentDoctor = new clsBIHDoctor();
                    m_objCurrentDoctor.m_strDoctorID = m_objLoginInfo.m_strEmpID;
                    m_objCurrentDoctor.m_strDoctorName = m_objLoginInfo.m_strEmpName;
                }
            }
        }

        #endregion
        #region	����
        /// <summary>
        /// ��ȡҽ����ɫ����	�������͡�ִ��״̬��
        /// ע�⣺
        ///		clrBack		ҽ������	{1=����;2=��ʱ}
        ///		clrFore		ִ��״̬	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;7-�˻�;}
        /// </summary>
        /// <param name="intType">ҽ������	{1=����;2=��ʱ}</param>
        /// <param name="intStatus">ִ��״̬	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;7-�˻�;}</param>
        /// <param name="clrBack">[��������ҽ������]</param>
        /// <param name="clrFore">[��������ִ��״̬]</param>
        public void m_mthGetColorByStatus(int intType, int intStatus, out Color clrBack, out Color clrFore)
        {
            clsOrderStatus.s_mthGetColorByStatus(intType, intStatus, out clrBack, out clrFore);
        }
        #endregion
        #region ����
        /// <summary>
        /// �Ƿ���ʾToolTip��ʾ��Ϣ		{ֻ��}
        /// </summary>
        public bool IsDisPlayToolTip
        {
            get { return m_IsDisPlayToolTip; }
        }
        #endregion
        #region ��ӡҽ��
        private void cmdPrintOrder_Click(object sender, System.EventArgs e)
        {
            if (this.cboPrintType.SelectedIndex == 0)
            {
                m_objDomain.m_PrintOrder();
            }
            else if (this.cboPrintType.SelectedIndex == 1)
            {
                clsBIHPatientInfo patVo = this.m_ctlPatient.m_objPatient;
                if (patVo == null)
                {
                    m_mthShowMessage("����ѡ����!");
                    this.m_ctlPatient.Focus();
                    return;
                }
                string registerId = this.m_ctlPatient.m_objPatient.m_strRegisterID;
                //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                DataTable dtMed = (new weCare.Proxy.ProxyIP()).Service.GetOutMedicine(registerId);
                if (dtMed == null || dtMed.Rows.Count == 0)
                {
                    m_mthShowMessage("�޳�Ժ��ҩ��Ŀ");
                    return;
                }
                frmChooseRecipe frm = new frmChooseRecipe(patVo, dtMed);
                frm.ShowDialog();
            }
            else if (this.cboPrintType.SelectedIndex == 2)
            {
                clsBIHPatientInfo patVo = this.m_ctlPatient.m_objPatient;
                if (patVo == null)
                {
                    m_mthShowMessage("����ѡ���ˡ�");
                    this.m_ctlPatient.Focus();
                    return;
                }
                if (this.m_dtvOrder.SelectedRows.Count == 0)
                {
                    m_mthShowMessage("��ѡ��Ҫ��ӡ������ҩƷҽ����");
                    return;
                }
                string orderIdArr = string.Empty;
                if (this.m_dtvOrder.SelectedRows.Count > 0)
                {
                    for (int i = 0; i < this.m_dtvOrder.SelectedRows.Count; i++)
                    {
                        clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[i].Tag;
                        orderIdArr += "'" + order.m_strOrderID + "',";
                    }
                }
                if (orderIdArr != string.Empty) orderIdArr = orderIdArr.TrimEnd(',');

                //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                DataTable dtMed = (new weCare.Proxy.ProxyIP()).Service.GetAneMedicine(orderIdArr);
                if (dtMed == null || dtMed.Rows.Count == 0)
                {
                    m_mthShowMessage("������ҩƷ��������ѡ��");
                    return;
                }
                frmChooseRecipe frm = new frmChooseRecipe(patVo, dtMed);
                frm.ShowDialog();
            }
        }
        #endregion

        #region Find the parent order's index of grid.Add by jli in 2005-03-30
        private long m_lngGetParentOrderIndex(string p_strRecipeNO)
        {
            for (int i = 0; i < m_arlOrder.Count; i++)
            {
                if (((clsBIHOrder)m_arlOrder[i]).m_intRecipenNo.ToString().Trim() == p_strRecipeNO && ((clsBIHOrder)m_arlOrder[i]).m_strParentID.Trim() == "")
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #region ���뵥���
        #region �������뵥
        private clsCustom_SubmitValue[] objCSVOArr = null;
        private void m_mthLoadApplyBill()
        {
            //clsCustomFormServ obj = new clsCustomFormServ();
            clsDepartmentVO[] dpvo = new clsDepartmentVO[0];
            new clsController_Base().m_objComInfo.m_mthGetDepartmentByUserID(m_objLoginInfo.m_strEmpID, out dpvo);
            string[] strDpArr = new string[1];
            if (dpvo != null)
            {
                strDpArr = new string[dpvo.Length];
                for (int ii = 0; ii < dpvo.Length; ii++)
                {
                    strDpArr[ii] = dpvo[ii].strDeptID;
                }
            }
            else
            {
                strDpArr[0] = "";
            }

            long l = (new weCare.Proxy.ProxyEmr02()).Service.m_lngGetSubmitForms(this.LoginInfo.m_strEmpID, strDpArr, out objCSVOArr);
            if (objCSVOArr != null && objCSVOArr.Length > 0)
            {
                this.cmbApply.Items.Clear();
                for (int i = 0; i < objCSVOArr.Length; i++)
                {
                    this.cmbApply.Items.Add(objCSVOArr[i].m_strFormName);
                }
            }
            //this.cmbApply.Items.Add("�������뵥");
        }
        private void cmbApply_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_mthShowApplyBill();
            }
        }
        private void btCreatBill_Click(object sender, System.EventArgs e)
        {
            this.m_mthShowApplyBill();
        }
        #endregion
        #region �����뵥
        private void m_mthShowApplyBill()
        {
            if (m_ctlPatient.m_objPatient == null || m_ctlPatient.m_objPatient.m_strPatientID == null)
            {
                m_mthShowMessage("����ָ������!");
                m_ctlPatient.Focus();
                return;
            }
            //��ȡ����ID���������ƿ���
            string strPatientID = m_ctlPatient.m_objPatient.m_strPatientID;
            string strPatientCardID = m_strPatientCardID;
            if (this.cmbApply.SelectedIndex < 0)
            {
                return;
            }
            if (this.objCSVOArr == null || this.cmbApply.SelectedIndex == this.objCSVOArr.Length)
            {
            }
            else
            {
                frmCustomFormBase objfrm = new frmCustomFormBase(strPatientID, strPatientCardID, this.objCSVOArr[this.cmbApply.SelectedIndex], this);
                //frmCustomFormBase objfrm =new frmCustomFormBase(this.m_PatInfo.PatientID,this.m_PatInfo.PatientCardID,this.objCSVOArr[this.cmbApply.SelectedIndex],this);
                objfrm.ShowDialog();
            }
        }
        #endregion
        #region ��������
        /// <summary>
        /// ��������	[ֻ��]
        /// </summary>
        public string PatientName
        {
            get
            {
                if (m_ctlPatient.m_objPatient == null || m_ctlPatient.m_objPatient.m_strPatientName == null)
                {
                    return "";
                }
                else
                {
                    return m_ctlPatient.m_objPatient.m_strPatientName;
                }
            }
        }
        /// <summary>
        /// ���ˣɣ�	[ֻ��]
        /// </summary>
        public string PatientID
        {
            get
            {
                if (m_ctlPatient.m_objPatient == null || m_ctlPatient.m_objPatient.m_strPatientID == null)
                {
                    return "";
                }
                else
                {
                    return m_ctlPatient.m_objPatient.m_strPatientID;
                }
            }
        }
        /// <summary>
        /// �����Ա�	[ֻ��]
        /// </summary>
        public string PatientSex
        {
            get
            {
                if (m_ctlPatient.m_objPatient == null || m_ctlPatient.m_objPatient.m_strSex == null)
                {
                    return "";
                }
                else
                {
                    return m_ctlPatient.m_objPatient.m_strSex;
                }
            }
        }
        /// <summary>
        /// ��������	[ֻ��]	
        /// </summary>
        public string PatientAge
        {
            get
            {
                if (m_ctlPatient.m_objPatient == null || m_ctlPatient.m_objPatient.m_dtBorn < System.DateTime.Now.AddYears(120) || m_ctlPatient.m_objPatient.m_dtBorn > System.DateTime.Now)
                {
                    return "";
                }
                else
                {
                    return strGetAge(m_ctlPatient.m_objPatient.m_dtBorn);
                }
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_dtBorn"></param>
        /// <returns></returns>
        private string strGetAge(DateTime p_dtBorn)
        {
            //change zhu.w.t 2007.4.23
            //if(p_dtBorn.Year==DateTime.Now.Year)
            //{
            //    if(p_dtBorn.Month==DateTime.Now.Month)
            //    {
            //        return (DateTime.Now.Day - p_dtBorn.Day).ToString() + "��";
            //    }
            //    else
            //    {
            //        return (DateTime.Now.Month - p_dtBorn.Month).ToString() + "��";				
            //    }
            //}
            //else
            //{
            //    return (DateTime.Now.Year - p_dtBorn.Year).ToString() + "��";
            //}
            //=================================>>
            string strAge = com.digitalwave.iCare.gui.LIS.clsAgeConverter.s_strGetAge(p_dtBorn);
            return strAge;
            /*<<================================*/
        }
        /// <summary>
        /// ���ƿ���
        /// </summary>
        string m_strPatientCardID = "";
        /// <summary>
        /// ���ƿ���
        /// </summary>
        public string PatientCardID
        {
            get
            {
                return m_strPatientCardID;
            }
            set
            {
                m_strPatientCardID = value;
            }
        }
        /// <summary>
        /// סԺ��	[ֻ��]
        /// </summary>
        public string InHospitalNo
        {
            get
            {
                if (m_ctlPatient.m_objPatient == null || m_ctlPatient.m_objPatient.m_strInHospitalNo == null)
                {
                    return "";
                }
                else
                {
                    return m_ctlPatient.m_objPatient.m_strInHospitalNo;
                }
            }
        }
        /// <summary>
        /// ��������	[ֻ��]
        /// </summary>
        public string AreaName
        {
            get
            {
                if (m_ctlPatient.m_objPatient == null || m_ctlPatient.m_objPatient.m_strAreaName == null)
                {
                    return "";
                }
                else
                {
                    return m_ctlPatient.m_objPatient.m_strAreaName;
                }
            }
        }
        /// <summary>
        /// ����	[ֻ��]
        /// </summary>
        public string BedNo
        {
            get
            {
                if (m_ctlPatient.m_objPatient == null || m_ctlPatient.m_objPatient.m_strBedName == null)
                {
                    return "";
                }
                else
                {
                    return m_ctlPatient.m_objPatient.m_strBedName;
                }
            }
        }
        /// <summary>
        /// ����������	[ֻ��]
        /// </summary>
        public string OperatorName
        {
            get
            {
                if (m_objLoginInfo == null || m_objLoginInfo.m_strEmpName == null)
                {
                    return "";
                }
                else
                {
                    return m_objLoginInfo.m_strEmpName;
                }
            }
        }
        /// <summary>
        /// �����˹���	[ֻ��]
        /// </summary>
        public string OperatorNo
        {
            get
            {
                if (m_objLoginInfo == null || m_objLoginInfo.m_strEmpNo == null)
                {
                    return "";
                }
                else
                {
                    return m_objLoginInfo.m_strEmpNo;
                }
            }
        }
        #endregion
        #endregion

        private void label13_Click(object sender, System.EventArgs e)
        {
            this.label13.Visible = false;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (this.label13.Visible == true)
            {
                this.label13.Visible = false;
            }
            else
            {

                this.label13.Show();
                this.label13.BringToFront();
            }
        }

        private void m_dtgOrder_m_evtDataGridKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            System.Drawing.Point pt = new Point(0, 0);
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                m_lsvToolTip.Visible = false;
            }
            cmbApply_KeyDown(null, e);
        }

        internal void lblLeft_Click()
        {
            lblLeft_Click(null, null);
        }
        private void lblLeft_Click(object sender, System.EventArgs e)
        {
            m_IsDisPlayToolTip = false;
            if (m_ctlOrderDetail.m_txtDoctor.Tag != null)
            {
                frmListDoctor.DoctorID = m_ctlOrderDetail.m_txtDoctor.Tag.ToString();
                frmListDoctor.DoctorName = m_ctlOrderDetail.m_txtDoctor.Text;
            }
            else
            {
                frmListDoctor.DoctorID = "";
                frmListDoctor.DoctorName = "";
            }
            if (!fl.Visible)
            {
                lblLeft.BackColor = Color.DarkGray;
                lblLeft.ForeColor = Color.White;
                lblBoard.BackColor = SystemColors.Control;
                lblBoard.ForeColor = Color.Black;
                lbePriceInfo.BackColor = SystemColors.Control;
                lbePriceInfo.ForeColor = Color.Black;
                fo.Hide();
                this.m_lsvToolTip.Hide();
                fl.TopLevel = false;
                fl.Visible = true;
                fl.FormBorderStyle = FormBorderStyle.None;
                fl.Parent = this.pnSel;
                fl.Show();
                pnSel.Show();
                lblLeft.Parent.Parent.Width = lblLeft.Width + 500;
                fl.lsvPatientItemSelected();
                fl.SetPatient(this.CurrentPatientInhospitalNo);
            }
            else
            {
                lblLeft.BackColor = SystemColors.Control;
                lblLeft.ForeColor = Color.Black;
                pnSel.Hide();
                fl.Visible = false;
                lblLeft.Parent.Parent.Width = lblLeft.Width;
            }
        }

        private void m_mnuCopy_Click(object sender, System.EventArgs e)
        {

            if (!fo.Visible)
            {
                lblBoard_Click(sender, e);
            }
            List<clsBIHOrder> m_arrOrder = GetTheSelectItemWithSon();
            //���ҽ���Ƿ�ͣ��
            ArrayList m_arrOrderIDs = new ArrayList();
            for (int i = 0; i < m_arrOrder.Count; i++)
            {
                if (!m_arrOrderIDs.Contains((m_arrOrder[i]).m_strOrderID))
                {
                    m_arrOrderIDs.Add((m_arrOrder[i]).m_strOrderID);
                }
            }
            ArrayList m_strOrders = GetTheStopOrders(m_arrOrderIDs);
            if (m_strOrders != null && m_arrOrder.Count > 0)
            {
                string m_strErrMessage = "";
                for (int i = 0; i < m_arrOrder.Count; i++)
                {
                    clsBIHOrder order = m_arrOrder[i];
                    if (m_strOrders.Contains(order.m_strOrderID))
                    {
                        m_strErrMessage += order.m_strName + "\r\n";
                    }
                }
                if (!m_strErrMessage.Trim().Equals(""))
                {
                    m_strErrMessage = "����ҽ��������շ���Ŀͣ�û�ҩƷͣҩ,���ܽ��и���!" + "\r\n" + m_strErrMessage;
                    MessageBox.Show(m_strErrMessage, "ҽ������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            fo.m_lngAddOrderGroupList(m_arrOrder);

        }

        private void m_mnuPaste_Click(object sender, System.EventArgs e)
        {
            try
            {
                DataObject dob = (DataObject)Clipboard.GetDataObject();
                object[] objPaste = ((object[])(dob.GetData(typeof(object[]))));
            }
            catch
            {
            }
        }

        private void lblBoard_Click(object sender, System.EventArgs e)
        {
            m_IsDisPlayToolTip = false;

            if (!fo.Visible)
            {
                lblLeft.BackColor = SystemColors.Control;
                lblLeft.ForeColor = Color.Black;
                lblBoard.BackColor = Color.DarkGray;
                lblBoard.ForeColor = Color.White;
                lbePriceInfo.BackColor = SystemColors.Control;
                lbePriceInfo.ForeColor = Color.Black;
                fl.Hide();
                this.m_lsvToolTip.Hide();
                fo.frmParent = (frmBIHOrderInput)panel4.Parent;
                fo.TopLevel = false;
                fo.FormBorderStyle = FormBorderStyle.None;
                fo.Parent = this.pnSel;
                fo.Visible = true;
                fo.Show();
                pnSel.Show();
                lblLeft.Parent.Parent.Width = lblLeft.Width + 500;
            }
            else
            {
                lblBoard.BackColor = SystemColors.Control;
                lblBoard.ForeColor = Color.Black;
                pnSel.Hide();
                fo.Visible = false;
                lblLeft.Parent.Parent.Width = lblLeft.Width;
            }
        }

        private void pnTabs_Leave(object sender, System.EventArgs e)
        {
            lblLeft_Click(null, null);
        }

        private void pnSel_Leave(object sender, System.EventArgs e)
        {
            lblLeft_Click(null, null);
        }

        private void pnSel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.X < pnSel.Location.X && e.Y < pnSel.Location.Y) && (e.X > pnSel.Location.X + pnSel.Size.Width && e.Y > pnSel.Location.Y + pnSel.Size.Height))
            {
                lblLeft_Click(null, null);
            }
        }

        private void m_cmdSub_Click(object sender, System.EventArgs e)
        {
            if (m_ConfirmPatient() == 0)
            {
                return;
            }
            if (m_ConfirmHaveOrder() == 0)
            {
                return;
            }
            if (m_objCurrentOrder == null)
            {
                m_objCurrentOrder = (clsBIHOrder)m_dtvOrder.Rows[this.m_dtvOrder.RowCount - 1].Tag;
            }
            if (m_objCurrentOrder.m_intStatus != 0 && m_objCurrentOrder.m_intStatus != 7)
            {
                m_mthShowMessage("ֻ�ܶ��¿���ҽ��������ҽ������!");
                return;
            }
            if (m_objCurrentOrder.m_strCreatorID != this.LoginInfo.m_strEmpID)
            {
                m_mthShowMessage("ֻ�ܶ��Լ��¿���ҽ��������ҽ������!");
                return;
            }
            m_ctlOrderDetail.IsSubOrder = true;

            int currentrow = m_intCurrentRow;
            clsBIHOrder ParentOrder = (clsBIHOrder)m_arlOrder[m_intCurrentRow];
            m_ctlOrderDetail.IsSubOrder = true;
            m_ctlOrderDetail.ParentOrder = ParentOrder;
            m_objCurrentOrder = new clsBIHOrder();

            m_objCurrentOrder.m_intExecuteType = ParentOrder.m_intExecuteType;
            m_objCurrentOrder.m_intRecipenNo = ParentOrder.m_intRecipenNo;
            m_objCurrentOrder.m_strDosetypeID = ParentOrder.m_strDosetypeID;

            //�����ǰҽ������ҽ��,ȡ�丸ҽ����Ϊ��ҽ��
            if (ParentOrder.m_strParentID.Trim() != "")
            {
                m_objCurrentOrder.m_strParentID = ParentOrder.m_strParentID;
                m_objCurrentOrder.m_strParentName = ParentOrder.m_strParentName;
            }
            else
            {
                m_objCurrentOrder.m_strParentID = ParentOrder.m_strOrderID;
                m_objCurrentOrder.m_strParentName = ParentOrder.m_strName;
            }

            m_objCurrentOrder.m_strDosetypeName = ParentOrder.m_strDosetypeName;
            m_objCurrentOrder.m_strExecFreqID = ParentOrder.m_strExecFreqID;
            m_objCurrentOrder.m_strExecFreqName = ParentOrder.m_strExecFreqName;
            /* ����*/
            m_objCurrentOrder.m_strSAMPLEName_VCHR = ParentOrder.m_strSAMPLEName_VCHR.ToString().Trim();
            m_objCurrentOrder.m_strSAMPLEID_VCHR = ParentOrder.m_strSAMPLEID_VCHR.ToString().Trim();
            /*<==============================================*/
            /* ���*/
            m_objCurrentOrder.m_strPARTNAME_VCHR = ParentOrder.m_strPARTNAME_VCHR.ToString().Trim();
            m_objCurrentOrder.m_strPARTID_VCHR = ParentOrder.m_strPARTID_VCHR.ToString().Trim();
            m_objCurrentOrder.m_strPatientID = ParentOrder.m_strPatientID;

            //ҽ�����͸ı�ʱ�Ŀ���(����/����/��Ժ��ҩ)
            m_ctlOrderDetail.m_cboExecuteType_SelectedIndexChanged(null, null);
            m_mthShowCurrentOrder(m_objCurrentOrder);

            m_ctlOrderDetail.m_txtExecuteFreq.Enabled = false;
            m_ctlOrderDetail.m_txtExecuteFreq.BackColor = SystemColors.Control;
            m_ctlOrderDetail.m_txtDosageType.Enabled = false;
            m_ctlOrderDetail.m_txtDosageType.BackColor = SystemColors.Control;
            m_ctlOrderDetail.m_mthSetDoctor(m_objCurrentDoctor.m_strDoctorID, m_objCurrentDoctor.m_strDoctorName);

            m_ctlOrderDetail.m_txtOrderName.Enabled = true;
            m_ctlOrderDetail.m_txtOrderName.BackColor = System.Drawing.Color.White;
            m_ctlOrderDetail.m_txtOrderName.Focus();
            // ����(���ύ/ִ��ҽ����)��ҽ�����ܱ�������
            this.m_cmdSave.Enabled = true;
            this.m_cmdDelete.Enabled = false;
            m_cmdBlankOut.Enabled = false;
            m_cmdStop.Enabled = false;
            m_cmdSub.Enabled = false;
        }

        private void lbePriceInfo_Click(object sender, System.EventArgs e)
        {
            if (!m_lsvToolTip.Visible)
            {
                if (m_IsDisPlayToolTip == false)
                {
                    m_IsDisPlayToolTip = true;
                    m_dtvOrder_CellMouseClick(null, null);
                }

                lbePriceInfo.BackColor = Color.DarkGray;
                lbePriceInfo.ForeColor = Color.White;
                lblBoard.BackColor = SystemColors.Control;
                lblBoard.ForeColor = Color.Black;
                lblLeft.BackColor = SystemColors.Control;
                lblLeft.ForeColor = Color.Black;
                foreach (System.Windows.Forms.Control c in this.pnSel.Controls)
                {
                    c.Visible = false;
                }
                this.pnSel.Controls.Add(this.m_lsvToolTip);
                this.m_lsvToolTip.Dock = DockStyle.Fill;
                this.m_lsvToolTip.Show();
                this.m_lsvToolTip.BringToFront();
                pnSel.Show();
                lbePriceInfo.Parent.Parent.Width = lbePriceInfo.Width + 500;
            }
            else
            {
                m_IsDisPlayToolTip = false;
                lblLeft.BackColor = SystemColors.Control;
                lblLeft.ForeColor = Color.Black;
                pnSel.Hide();
                m_lsvToolTip.Hide();
                lbePriceInfo.Parent.Parent.Width = lbePriceInfo.Width;
            }
        }

        #region	��ʾҩƷ��ϸ��Ϣ	glzhang	2005.10.13
        private void m_lsvToolTip_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //case Keys.F9:
                //    if(m_lsvToolTip.SelectedItems.Count>0)
                //    {
                //        string strMedicineInfo;
                //        clsDcl_InputOrder m_input = new clsDcl_InputOrder();
                //        m_input.m_mthGetMedicineInfo(((clsChargeForDisplay)m_lsvToolTip.SelectedItems[0].Tag).m_strChargeID,out strMedicineInfo);
                //        frmShowMedicineInfo m_frmTemp = new frmShowMedicineInfo(strMedicineInfo);
                //        m_frmTemp.ShowDialog();
                //        m_frmTemp.Dispose();
                //    }
                //    break;
                case Keys.Enter:
                    if (m_lsvToolTip.SelectedItems.Count > 0)
                    {
                        string strMedicineInfo;
                        clsDcl_InputOrder m_input = new clsDcl_InputOrder();
                        m_input.m_mthGetMedicineInfo(((clsChargeForDisplay)m_lsvToolTip.SelectedItems[0].Tag).m_strChargeID, out strMedicineInfo);
                        frmShowMedicineInfo m_frmTemp = new frmShowMedicineInfo(strMedicineInfo);
                        m_frmTemp.ShowDialog();
                        m_frmTemp.Dispose();
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        /// <summary>
        /// ҽ��¼�����ǰ����Ϣ���
        /// </summary>
        public bool m_OrderInputPreCheck()
        {
            clsBIHPatientInfo objPatient = m_ctlPatient.m_objPatient;
            if (objPatient == null)
            {
                m_mthShowMessage("����ָ������!");
                m_ctlPatient.Focus();
                return false;
            }
            //ҽ������: ����ʱ����¼�룬��Ҫ�����趨�ò��˷������ޡ�
            //if (m_blnISMedicareMan)
            //{
            //    double dblPrePayMoney = 0;
            //    double dblLIMITRATE_MNY = m_ctlPatient.m_objPatient.m_dblLIMITRATE_MNY;
            //    try { dblPrePayMoney = double.Parse(m_ctlPatient.m_txtPrePayMoney.Text.ToString()); }
            //    catch { }

            //    if (dblLIMITRATE_MNY > dblPrePayMoney)
            //    {
            //        MessageBox.Show("Ԥ�������,������������,���ܿ�ҽ����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return false;
            //    }
            //}
            return true;
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            this.panel3.Visible = !this.panel3.Visible;
        }

        private void alertLight1_Click(object sender, EventArgs e)
        {
            this.panel3.Visible = !this.panel3.Visible;
        }

        private void m_mnuCopyBihOrder_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count == 0)
            {
                MessageBox.Show("����ѡ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string[] p_strRecordIDArr = null;
            List<clsBIHOrder> m_arrOrderSameNo = GetTheSelectItemWithSon();
            m_objDomain.m_lngAddNewOrderByGroup(out p_strRecordIDArr, m_arrOrderSameNo);
            this.m_objDomain.m_mthLoadOrderList();
        }

        /// <summary>
        ///��õ�ǰѡ�е�ҽ���б�(��������ҽ��)
        /// </summary>
        /// <returns></returns>
        internal List<clsBIHOrder> GetTheSelectItemWithSon()
        {
            //�����Ѵ��ڵ�ҽ����
            ArrayList m_arrOrderHave = new ArrayList();
            List<clsBIHOrder> m_arrOrderSameNo = new List<clsBIHOrder>();
            int m_intRecipenNo = -1;

            for (int i = 0; i < this.m_dtvOrder.Rows.Count; i++)
            {
                if (this.m_dtvOrder.Rows[i].Selected == true)
                {
                    m_intRecipenNo = ((clsBIHOrder)this.m_dtvOrder.Rows[i].Tag).m_intRecipenNo;
                    for (int j = 0; j < this.m_dtvOrder.Rows.Count; j++)
                    {
                        //ҽ������Ϊ����ҽ����Ϊ����ҽ���Ĳ�������������ģ��
                        clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.Rows[j].Tag;
                        if (!CanCopyToGroup(order))
                        {
                            continue;
                        }
                        if (m_intRecipenNo == order.m_intRecipenNo)
                        {
                            if (m_arrOrderHave.Contains(order.m_strOrderID))
                            {
                                continue;
                            }
                            m_arrOrderSameNo.Add(CopyNewOrder(order));
                            m_arrOrderHave.Add(order.m_strOrderID);
                        }
                    }
                }

            }

            //���õ�һ��ҽ��Ϊ��ҽ����������ڸ��ӵ�����£�
            if (m_arrOrderSameNo.Count > 0)
            {
                ArrayList m_arrParentSet = new ArrayList();
                for (int i = 0; i < m_arrOrderSameNo.Count; i++)
                {
                    SetTheCurrentOrder(m_arrOrderSameNo[i]);
                    if (i < m_arrOrderSameNo.Count - 1)
                    {
                        if (m_arrParentSet.Contains((m_arrOrderSameNo[i]).m_intRecipenNo))
                        {
                            continue;
                        }
                        if ((m_arrOrderSameNo[i]).m_intRecipenNo == (m_arrOrderSameNo[i + 1]).m_intRecipenNo)
                        {
                            (m_arrOrderSameNo[i]).m_intIFPARENTID_INT = 1;
                            m_arrParentSet.Add((m_arrOrderSameNo[i]).m_intRecipenNo);
                        }
                    }
                }

            }
            return m_arrOrderSameNo;
        }

        /// <summary>
        /// �����µ�ҽ��(ԭ��ҽ������ҽ��)
        /// </summary>
        /// <param name="clsBIHOrder"></param>
        /// <returns></returns>
        public clsBIHOrder CopyNewOrder(clsBIHOrder clsBIHOrder)
        {
            clsBIHOrder order = new clsBIHOrder();
            order.m_dmlDosage = clsBIHOrder.m_dmlDosage;
            order.m_dmlDosageRate = clsBIHOrder.m_dmlDosageRate;
            order.m_dmlGet = clsBIHOrder.m_dmlGet;
            order.m_dmlPrice = clsBIHOrder.m_dmlPrice;
            order.m_dmlUse = clsBIHOrder.m_dmlUse;
            order.m_intATTACHTIMES_INT = clsBIHOrder.m_intATTACHTIMES_INT;
            order.m_intExecuteType = clsBIHOrder.m_intExecuteType;
            order.m_intIFPARENTID_INT = clsBIHOrder.m_intIFPARENTID_INT;
            order.m_intOUTGETMEDDAYS_INT = clsBIHOrder.m_intOUTGETMEDDAYS_INT;
            order.RateType = clsBIHOrder.RateType;
            order.m_intRecipenNo = clsBIHOrder.m_intRecipenNo;
            order.m_intRecipenNo2 = clsBIHOrder.m_intRecipenNo2;
            order.m_intStatus = 0;
            order.m_intCHARGE_INT = clsBIHOrder.m_intCHARGE_INT;
            order.m_strCHARGEITEMID_CHR = clsBIHOrder.m_strCHARGEITEMID_CHR;
            order.m_strCHARGEITEMNAME_CHR = clsBIHOrder.m_strCHARGEITEMNAME_CHR;
            order.m_strDosageUnit = clsBIHOrder.m_strDosageUnit;
            order.m_strDosetypeID = clsBIHOrder.m_strDosetypeID;
            order.m_strDosetypeName = clsBIHOrder.m_strDosetypeName;
            order.m_strEntrust = clsBIHOrder.m_strEntrust;
            order.m_strExecFreqID = clsBIHOrder.m_strExecFreqID;
            order.m_strExecFreqName = clsBIHOrder.m_strExecFreqName;
            clsAIDRecipeFreq m_objTempFreq = this.m_ctlOrderDetail.GetFreqVoByFreqID(clsBIHOrder.m_strExecFreqID);
            if (m_objTempFreq != null)
            {
                order.m_intFreqTime = m_objTempFreq.m_intTimes;
                order.m_intFreqDays = m_objTempFreq.m_intDays;
            }
            order.m_strGetunit = clsBIHOrder.m_strGetunit;
            order.m_strLISAPPID_VCHR = clsBIHOrder.m_strLISAPPID_VCHR;
            order.m_strMedicareTypeName = clsBIHOrder.m_strMedicareTypeName;
            //ȡ������Ŀ���ƣ�����������ҽ����ȡ��ԭ����ҽ������---->
            //ҽ������
            clsT_aid_bih_ordercate_VO p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[clsBIHOrder.m_strOrderDicCateID];
            if (p_objItem != null && p_objItem.m_strVIEWNAME_VCHR.Trim().Equals("����ҽ��"))
            {
                order.m_strName = clsBIHOrder.m_strName;
            }
            else if (clsBIHOrder.m_intTYPE_INT != 0)
            {
                order.m_strName = clsBIHOrder.m_strName;
            }
            else
            {
                if (clsBIHOrder.m_strName.Trim().Equals(clsBIHOrder.m_strDicName.Trim()) || clsBIHOrder.m_strDicName.Trim().Equals(""))
                {
                    order.m_strName = clsBIHOrder.m_strName;
                }
                else
                {
                    order.m_strName = clsBIHOrder.m_strDicName;
                }
            }
            order.m_strOrderDicCateID = clsBIHOrder.m_strOrderDicCateID;
            order.m_strOrderDicCateName = clsBIHOrder.m_strOrderDicCateName;
            order.m_strOrderDicID = clsBIHOrder.m_strOrderDicID;
            order.m_strOrderID = clsBIHOrder.m_strOrderID;
            order.m_strPARTID_VCHR = clsBIHOrder.m_strPARTID_VCHR;
            order.m_strPARTNAME_VCHR = clsBIHOrder.m_strPARTNAME_VCHR;
            order.m_strSAMPLEID_VCHR = clsBIHOrder.m_strSAMPLEID_VCHR;
            order.m_strSAMPLEName_VCHR = clsBIHOrder.m_strSAMPLEName_VCHR;
            order.m_strSpec = clsBIHOrder.m_strSpec;
            order.m_strUseunit = clsBIHOrder.m_strUseunit;
            order.m_intISNEEDFEEL = clsBIHOrder.m_intISNEEDFEEL;

            order.m_strRegisterID = this.m_ctlPatient.m_objPatient.m_strRegisterID;
            order.m_strPatientID = this.m_ctlPatient.m_objPatient.m_strPatientID;
            order.m_strREMARK_VCHR = clsBIHOrder.m_strREMARK_VCHR;
            //ҽ�����ڹ�����ID
            order.m_strDOCTORGROUPID_CHR = m_strDOCTORGROUPID_CHR;
            //��ҽ��ʱ�������ڲ���ID
            order.m_strCURAREAID_CHR = this.m_ctlPatient.m_objPatient.m_strAreaID;
            //��ҽ��ʱ�������ڲ���ID
            order.m_strCURBEDID_CHR = this.m_ctlPatient.m_objPatient.m_strBedID;
            //��������
            order.m_dmlGet = clsBIHOrder.m_dmlGet;
            order.m_dmlOneUse = clsBIHOrder.m_dmlOneUse;//��һ�ε���
            //ҽ����Դ
            order.m_intSOURCETYPE_INT = m_intSOURCETYPE_INT;
            //������ҽ����ҽ��������ǿ��Ϊ��λ������Ƶ�ʴ���Ϊ1
            if (order.m_strExecFreqID.Trim().Equals(m_objSpecateVo.m_strCONFREQID_CHR.Trim()))
            {
                order.m_dmlGet = 1;
                order.m_dmlOneUse = 1;
                order.m_intFreqTime = 1;
                order.m_intFreqDays = 1;
                order.m_dmlDosage = 1;
            }
            order.m_intIPCHARGEFLG_INT = clsBIHOrder.m_intIPCHARGEFLG_INT;
            order.m_dmlPACKQTY_DEC = clsBIHOrder.m_dmlPACKQTY_DEC;
            order.IsProxyBoilMed = clsBIHOrder.IsProxyBoilMed;
            return order;
        }

        /// <summary>
        /// Ϊ��ǰҽ�����ϸ��ӵ���Ϣ
        /// </summary>
        /// <param name="clsBIHOrder"></param>
        public void SetTheCurrentOrder(clsBIHOrder objOrder)
        {
            //������Ŀ��Ϣ
            objOrder.m_intStatus = 0;
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
            //��ʼ�ͽ���ʱ�� 
            try
            {
                objOrder.m_dtStartDate = Convert.ToDateTime(this.m_ctlOrderDetail.m_dtStartTime2.Text.ToString().Trim());
            }
            catch
            {
                objOrder.m_dtStartDate = DateTime.MinValue;
            }

            objOrder.m_strCreatorID = this.LoginInfo.m_strEmpID;
            objOrder.m_strCreator = this.LoginInfo.m_strEmpName;
            objOrder.m_strDOCTORID_CHR = clsConverter.ToString(this.m_ctlOrderDetail.m_txtDoctorList.Tag);
            objOrder.m_strDOCTOR_VCHR = this.m_ctlOrderDetail.m_txtDoctorList.Text.Trim();

            // ��ӿ���������Ϣ
            objOrder.m_strCREATEAREA_ID = Convert.ToString(m_txtCREATEAREA.Tag);
            objOrder.m_strCREATEAREA_Name = Convert.ToString(m_txtCREATEAREA.Text.Trim());

            //ҽ�����ڹ�����ID
            objOrder.m_strDOCTORGROUPID_CHR = m_strDOCTORGROUPID_CHR;
            objOrder.m_strCHARGEDOCTORGROUPID = this.LoginInfo.m_strGroupID;

            //��ҽ��ʱ�������ڲ���ID
            objOrder.m_strCURAREAID_CHR = this.m_ctlPatient.m_objPatient.m_strAreaID;
            //��ҽ��ʱ�������ڲ���ID
            objOrder.m_strCURBEDID_CHR = this.m_ctlPatient.m_objPatient.m_strBedID;
            //���˵ǼǺ�
            objOrder.m_strRegisterID = this.m_ctlPatient.m_objPatient.m_strRegisterID;
            //����סԺ��
            objOrder.m_strParentID = this.m_ctlPatient.m_objPatient.m_strPatientID;
            objOrder.m_dtCreatedate = DateTime.Now;
        }

        private void m_ctxGridMenu_Popup(object sender, CancelEventArgs e)
        {

        }

        private void m_ctxGridMenu2_Opening(object sender, CancelEventArgs e)
        {
            m_mnuBlankOut2.Enabled = false;//����
            m_mnuDelete2.Enabled = false;//ɾ��
            m_mnuStop2.Enabled = false;//ֹͣ
            m_mnuRetract2.Enabled = false;//����
            m_mnuCommitAll2.Enabled = false;//�ύ�����½�ҽ��
            m_mnuCopyBihorder2.Enabled = false;//����ҽ��
            TSMenuTurnBack.Enabled = false;//���ϻָ�
            m_mnuDoctorSign.Enabled = false;//ҽ��ǩ��
            menuItem7.Enabled = false;//�޸�����ʱ��
            menuItem8.Enabled = false;//�޸�ͣ��ʱ��
            this.m_MenuPase.Enabled = false;//ճ��
            this.m_mnuCopy.Enabled = false;//���Ƶ���ʱģ��
            this.m_MenuOrderTemp.Enabled = false;//������ҽ��ģ��
            this.m_MenuATTACHTIMES_INT.Enabled = false;//�޸Ĳ���
            this.m_MenuOrderSTsign.Enabled = false;//����ִ��ҽ��(st��ʶ)
            this.m_MenuCheckBill.Enabled = false;//������뵥
            this.m_MenuViewBackReasion.Enabled = false;//�鿴�˻�ԭ��
            this.m_MenuOutHis.Enabled = false;//��Ժҽ��
            //����Ȩ
            if (m_blAccess == false)
            {
                this.m_ctxGridMenu2.Enabled = false;
                return;
            }
            /*<===========================*/
            if (fo.getTheTempCout() > 0)
            {
                this.m_MenuPase.Enabled = true;
            }
            if (m_blDoctorSign == false)
            {
                m_mnuDoctorSign.Visible = false;
            }
            //��Ժҽ��
            if (m_objDomain.m_intOutHisCout == 0)
            {
                this.m_MenuOutHis.Enabled = true;
            }
            /*<===============================*/
            m_mnuCommitAll2.Enabled = true;
            //����ѡ��ҽ����˵�
            if (m_dtvOrder.SelectedRows.Count <= 0)
            {
                return;
            }
            this.m_mnuDelete2.Enabled = true;//ɾ��
            this.m_mnuBlankOut2.Enabled = true;//����
            this.m_MenuOrderTemp.Enabled = true;//COPY ����ʱģ��
            this.m_mnuStop2.Enabled = true;//ֹͣ
            m_mnuCopyBihorder2.Enabled = true;
            m_mnuCopy2.Enabled = true;
            this.m_mnuCopy.Enabled = true;

            clsBIHOrder order = (clsBIHOrder)m_dtvOrder.SelectedRows[0].Tag;
            //ҽ������
            clsT_aid_bih_ordercate_VO p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[order.m_strOrderDicCateID];

            if (order.SIGN_INT == 0)
            {
                m_mnuDoctorSign.Enabled = true;
            }
            //�鿴�˻�
            if (order.m_intStatus == 7)
            {
                this.m_MenuViewBackReasion.Enabled = true;
            }

            int intStatus = order.m_intStatus;
            int intType = order.m_intExecuteType;


            if (m_blBlankOutControl == false) m_mnuBlankOut2.Enabled = false;
            if (m_dtvOrder.SelectedRows.Count > 0)
            {
                if (order.m_intStatus == 0 || order.m_intStatus == 1 || order.m_intStatus == 5 || order.m_intStatus == 7)
                {
                    m_MenuOrderSTsign.Enabled = true;
                    //����ʱ��˵�����
                    TimeSpan span = DateTime.Now - order.m_dtCreatedate;
                    bool m_blTime = false;
                    if (m_intStartTimeSwitth != 0)
                    {
                        if (span.Days * 24 + span.Hours > m_intStartTimeSwitth)
                        {
                            m_blTime = false;//�޸�ͣ��ʱ��
                        }
                        else
                        {
                            m_blTime = true;
                        }
                    }
                    else
                    {
                        m_blTime = true;
                    }
                    menuItem7.Enabled = m_blTime;//�޸�����ʱ��
                    if (!((clsBIHOrder)m_dtvOrder.SelectedRows[0].Tag).m_strAPPLYTYPEID_CHR.Trim().Equals("") && !((clsBIHOrder)m_dtvOrder.SelectedRows[0].Tag).m_strPARTID_VCHR.Trim().Equals(""))
                    {
                        m_MenuCheckBill.Enabled = true;
                    }

                }
                if (order.m_intStatus == 0 || order.m_intStatus == 1 || order.m_intStatus == 5 || order.m_intStatus == 7 || (order.m_intExecuteType == 1 && order.m_intStatus == 2))
                {
                    TimeSpan span = DateTime.Now - order.m_dtCreatedate;
                    bool m_blTime = false;
                    if (m_intStopTimeSwitth != 0)
                    {
                        if (span.Days * 24 + span.Hours > m_intStopTimeSwitth)
                        {
                            m_blTime = false;//�޸�ͣ��ʱ��
                        }
                        else
                        {
                            m_blTime = true;
                        }
                    }
                    else
                    {
                        m_blTime = true;
                    }
                    menuItem8.Enabled = m_blTime;//�޸�ͣ��ʱ��
                }
                //����ѡ�Ķ��ʺ�ʱ���ܲ���
                bool m_blCan = false;
                for (int i = 0; i < m_dtvOrder.SelectedRows.Count; i++)
                {
                    m_blCan = m_getCanAPPENDVIEWTYPE((clsBIHOrder)m_dtvOrder.SelectedRows[i].Tag);
                    if (m_blCan == false)
                    {
                        break;
                    }

                }
                m_MenuATTACHTIMES_INT.Enabled = m_blCan;
                /*<==============================*/
            }
            //��Ժҽ���ж�,�Ѵ��ڵĲ����ٲ��롣
        }

        private bool m_getCanAPPENDVIEWTYPE(clsBIHOrder order)
        {
            bool m_blCan = false;
            clsBIHOrder m_objOrder = GetTheFaterOrder(order.m_intRecipenNo);
            //ҽ������
            clsT_aid_bih_ordercate_VO p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[m_objOrder.m_strOrderDicCateID];
            if (!m_blUsePowerHander(m_objOrder))
            {
                m_blCan = false;
                return m_blCan;
            }
            p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[m_objOrder.m_strOrderDicCateID];
            if (m_objOrder.m_intExecuteType == 1 && p_objItem != null && p_objItem.m_intAPPENDVIEWTYPE_INT == 1)
            {
                if ((order.m_intStatus == 0 || order.m_intStatus == 1))
                {
                    m_blCan = true;
                }
            }
            return m_blCan;

        }

        /// <summary>
        /// ���ſ����Ƿ������ҽ������
        /// </summary>
        private void m_cmdSubOrderByRecipeNo()
        {
            //��ǰ�Ƿ���ѡ������
            if (m_ConfirmPatient() == 0)
            {
                return;
            }

            //��ʼ��===========================================>
            m_ctlOrderDetail.IsSubOrder = false; //��ҽ��������־��
            m_ctlOrderDetail.ParentOrder = null;
            //����ҽ��¼�����ؼ�
            EnableTheBihDetailControl(true);
            /*<===============================================*/

            //�������ڽ�����ҽ���������ٴ���ҽ�������ĳ�ʼ��
            if (this.m_objCurrentOrder != null && this.m_objCurrentOrder.m_strOrderID == "")
            {
                this.m_objCurrentOrder = null;
            }
            int Count = 0;//ͬ��ҽ������Ŀ
            int m_intIsParent = 0;//0-���Ǹ�ҽ���������Ǹ�ҽ��
            clsBIHOrder ParentOrder = new clsBIHOrder();
            int m_intRecipeNo = 0;
            try
            {
                m_intRecipeNo = int.Parse(m_ctlOrderDetail.m_txtRecipeNo.Text.ToString().Trim());
            }
            catch
            {

                m_ctlOrderDetail.m_txtRecipeNo.Text = "";
                m_ctlOrderDetail.m_txtRecipeNo.Focus();
                return;
            }
            /*  ���ͬ��ǰѡ�е�ҽ��������ͬ,�������κδ���*/
            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                clsBIHOrder Order = (clsBIHOrder)m_arlOrder[this.m_dtvOrder.SelectedRows[0].Index];
            }
            bool m_blOrders = false;
            if (this.m_objCurrentOrder == null)//��ǰ�����ڵ�ǰҽ��ʱ����ҽ������
            {
                for (int i = 0; i < m_arlOrder.Count; i++)//�жϵ�ǰҽ���Ƿ��ǿɽ�����ҽ��������״̬���¿����˻�ʱ��
                {
                    if (((clsBIHOrder)m_arlOrder[i]).m_intRecipenNo == m_intRecipeNo)
                    {
                        ParentOrder = (clsBIHOrder)m_arlOrder[i];
                        if (ParentOrder.m_intStatus != 0 && ParentOrder.m_intStatus != 7)
                        {
                            MessageBox.Show("����Ϊ��ǰ���Ž�����ҽ������!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            m_blOrders = false;
                            break;
                        }
                        m_blOrders = true;
                        break;
                    }
                }
                if (m_blOrders == false)
                {
                    m_cmdAdd_Click(null, null);
                    return;
                }
                //
                m_ctlOrderDetail.IsSubOrder = true; //��ҽ��������־��
                m_ctlOrderDetail.ParentOrder = ParentOrder;
                SetTheNewSubOrder();
            }
            else if (this.m_objCurrentOrder.m_intRecipenNo != m_intRecipeNo)//��������ı���¼�벻�ǵ�ǰҽ���ķ���
            {
                int i = 0;
                while (i < this.m_arlOrder.Count)
                {
                    if (((clsBIHOrder)this.m_arlOrder[i]).m_intRecipenNo == this.m_objCurrentOrder.m_intRecipenNo)
                    {
                        Count++;
                    }
                    //�����ǰ����ҽ���Ǹ�ҽ���Ҵ�����ҽ����¼
                    if (((clsBIHOrder)this.m_arlOrder[i]).m_strParentID == this.m_objCurrentOrder.m_strOrderID)
                    {
                        m_intIsParent = 1;
                        if (Count > 0)
                            break;
                    }
                    i++;
                }
                if ((Count > 0) && (m_intIsParent == 1))
                {
                    MessageBox.Show("������ҽ���ĸ�ҽ�����Ų������޸�!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.m_ctlOrderDetail.m_txtRecipeNo.Text = this.m_objCurrentOrder.m_intRecipenNo.ToString();
                    this.m_ctlOrderDetail.m_txtRecipeNo.Focus();
                }
                else
                {
                    m_blOrders = false;//������ҽ���Ĳ���(false-���ǣ�true-�ǡ�)
                    for (i = 0; i < this.m_arlOrder.Count; i++)
                    {
                        //��ǰҽ���Ƿ��ǽ��У���������ҽ��������
                        if ((((clsBIHOrder)this.m_arlOrder[i]).m_strOrderID != this.m_objCurrentOrder.m_strOrderID) && (((clsBIHOrder)this.m_arlOrder[i]).m_intRecipenNo == m_intRecipeNo))
                        {
                            ParentOrder = (clsBIHOrder)this.m_arlOrder[i];
                            m_blOrders = true;
                            break;
                        }
                    }
                    if (!m_blOrders)//������������
                    {

                    }
                    else//��ǰҽ��Ϊ������ҽ���Ĳ���
                    {
                        m_ctlOrderDetail.IsSubOrder = true; //��ҽ��������־��
                        m_ctlOrderDetail.ParentOrder = ParentOrder;
                        SetTheOldSubOrder();
                    }
                }
            }

        }

        /// <summary>
        /// ���ſ����Ƿ������ҽ������
        /// </summary>
        private void m_cmdSubOrderByRecipeNo2()
        {
            //��ǰ�Ƿ���ѡ������
            if (m_ConfirmPatient() == 0)
            {
                return;
            }

            //��ʼ��===========================================>
            if (m_ctlOrderDetail.m_objCurrentOrder != null)//�޸Ĳ���������ҽ������������
            {
                return;
            }
            m_ctlOrderDetail.IsSubOrder = false; //��ҽ��������־��
            m_ctlOrderDetail.ParentOrder = null;
            int m_intRecipeNo = 0;
            try
            {
                m_intRecipeNo = int.Parse(m_ctlOrderDetail.m_txtRecipeNo.Text.ToString().Trim());
            }
            catch
            {

                m_ctlOrderDetail.m_txtRecipeNo.Text = m_objDomain.m_intBigRecipeNo.ToString();
                return;
            }
            //�жϵ�ǰҽ���Ƿ��ǿɽ�����ҽ��������״̬���¿����˻�ʱ�������ø�ҽ��
            bool m_blOrders = IsSudOrderManager(m_intRecipeNo);

            if (m_blOrders == false)//���ܽ�����ҽ������������ҽ������
            {
                return;
            }
            #region ��ҽ������


            SetTheNewSubOrder();//���ø�ҽ���ĵ����������
            //ҽ��¼�뷽ʽ�������
            int m_intExecuteType = clsConverter.ToInt(m_ctlOrderDetail.m_cboExecuteType.m_strGetID(m_ctlOrderDetail.m_cboExecuteType.SelectedIndex));
            m_ctlOrderDetail.m_cboExecuteTypeChanged(m_intExecuteType);
            //��ҩ�������
            //��ҩ������ʾ(�������ؼ��Ŀ�����ʾ�������ֶ�)
            if (m_ctlOrderDetail.ParentOrder != null)
            {
                clsT_aid_bih_ordercate_VO p_objItem;
                p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[m_ctlOrderDetail.ParentOrder.m_strOrderDicCateID];

                if (p_objItem.m_strORDERCATEID_CHR.Equals(m_objSpecateVo.m_strMID_MEDICINE_CHR))
                {
                    m_ctlOrderDetail.m_txtMid_MedicineControl(true);

                }
            }
            /*<============================================*/

            /*<=============================*/
            //ͬ��ҽ���޸�ʱ�Ľ������
            EnableTheBihDetailControl(false);
            m_objDomain.m_SetDisplayOrderEditState(3);
            //��ת��λ
            m_ctlOrderDetail.setTheControlOrder("m_cboExecuteType");
            SendKeys.Send("{Enter}");
            /*<================*/
            #endregion

            //��ҽ����ҽ��LABEL�Ŀ���
            if (m_ctlOrderDetail.IsSubOrder == true)
            {
                m_ctlOrderDetail.m_lblSaveOrderID.Text = "ͬ��ҽ��";
            }

        }

        /// <summary>
        ///�жϵ�ǰҽ���Ƿ��ǿɽ�����ҽ��������״̬���¿����˻�ʱ��
        /// </summary>
        /// <returns></returns>
        public bool IsSudOrderManager(int m_intRecipeNo)
        {
            clsBIHOrder ParentOrder = null;
            bool m_blOrders = false;
            for (int i = 0; i < this.m_dtvOrder.RowCount; i++)//�жϵ�ǰҽ���Ƿ��ǿɽ�����ҽ��������״̬���¿����˻�ʱ��
            {
                ParentOrder = (clsBIHOrder)m_dtvOrder.Rows[i].Tag;
                if (ParentOrder.m_intRecipenNo == m_intRecipeNo)
                {
                    m_blOrders = true;
                    break;
                }
            }
            m_ctlOrderDetail.IsSubOrder = true; //��ҽ��������־��
            m_ctlOrderDetail.ParentOrder = ParentOrder;
            return m_blOrders;
        }

        /// <summary>
        /// ���û���� ��ҽ��¼�����Ŀؼ�
        /// </summary>
        public void EnableTheBihDetailControl(bool m_blTag)
        {
            if (m_blTag)//����
            {
                this.m_ctlOrderDetail.m_cboExecuteType.Enabled = true;//ҽ����ʽ
                //this.m_ctlOrderDetail.m_txtExecuteFreq.Enabled = true;//Ƶ������
                //this.m_ctlOrderDetail.m_txtExecuteFreq.BackColor = Color.White;
                //this.m_ctlOrderDetail.m_txtDosageType.Enabled = true;//�÷�����
                //this.m_ctlOrderDetail.m_txtDosageType.BackColor = Color.White;
                //this.m_ctlOrderDetail.m_dtStartTime2.Enabled = true;//��ʼʱ��
                //this.m_ctlOrderDetail.m_dtFinishTime2.Enabled = true;//����ʱ��
                //this.m_ctlOrderDetail.m_txtDoctorList.Enabled = true;//ҽ��¼��
                //this.m_ctlOrderDetail.m_txtDoctorList.BackColor = Color.White;
                //this.m_ctlOrderDetail.m_txtATTACHTIMES_INT.Enabled = true;//����
                //this.m_ctlOrderDetail.m_txtREMARK_VCHR.Enabled = true;//˵��
                //this.m_ctlOrderDetail.m_txtREMARK_VCHR.BackColor = Color.White;
            }
            else//����
            {
                this.m_ctlOrderDetail.m_cboExecuteType.Enabled = false;//ҽ����ʽ
                //this.m_ctlOrderDetail.m_txtExecuteFreq.Enabled = false;//Ƶ������
                //this.m_ctlOrderDetail.m_txtExecuteFreq.BackColor = SystemColors.Control;
                //this.m_ctlOrderDetail.m_txtDosageType.Enabled = false;//�÷�����
                //this.m_ctlOrderDetail.m_txtDosageType.BackColor = SystemColors.Control;
                //this.m_ctlOrderDetail.m_dtStartTime2.Enabled = false;//��ʼʱ��
                //this.m_ctlOrderDetail.m_dtStartTime2.BackColor = SystemColors.Control;
                //this.m_ctlOrderDetail.m_dtFinishTime2.Enabled = false;//����ʱ��
                //this.m_ctlOrderDetail.m_dtFinishTime2.BackColor = SystemColors.Control;
                //this.m_ctlOrderDetail.m_txtDoctorList.Enabled = false;//ҽ��¼��
                //this.m_ctlOrderDetail.m_txtDoctorList.BackColor = SystemColors.Control;
                //this.m_ctlOrderDetail.m_txtATTACHTIMES_INT.Enabled = false;//����
                //this.m_ctlOrderDetail.m_txtREMARK_VCHR.Enabled = false;//˵��
                //this.m_ctlOrderDetail.m_txtREMARK_VCHR.BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// �¿���ҽ���Ľ������
        /// </summary>
        /// <param name="ParentOrder"></param>
        private void SetTheNewSubOrder()
        {
            //��ҽ�������ĳ�ʼ������  
            //��ʼ��ҽ���������
            m_ctlOrderDetail.EmptyInput();
            ////ҽ�����͸ı�ʱ�Ŀ���(����/����/��Ժ��ҩ)
            //this.m_ctlOrderDetail.m_cboExecuteType_SelectedIndexChanged(null, null);

            m_objCurrentOrder = new clsBIHOrder();
            clsBIHOrder ParentOrder = m_ctlOrderDetail.ParentOrder;
            m_objCurrentOrder.m_intExecuteType = ParentOrder.m_intExecuteType;
            m_objCurrentOrder.m_intRecipenNo = ParentOrder.m_intRecipenNo;//����
            m_objCurrentOrder.m_intRecipenNo2 = ParentOrder.m_intRecipenNo2;//��ʾ�ķ���
            m_objCurrentOrder.m_strDosetypeID = ParentOrder.m_strDosetypeID;
            m_objCurrentOrder.m_dtStartDate = ParentOrder.m_dtStartDate;
            m_objCurrentOrder.m_dtFinishDate = ParentOrder.m_dtFinishDate;
            m_objCurrentOrder.m_strEntrust = ParentOrder.m_strEntrust;
            ////�����ǰҽ������ҽ��,ȡ�丸ҽ����Ϊ��ҽ��
            //if (ParentOrder.m_strParentID.Trim() != "")
            //{
            //    m_objCurrentOrder.m_strParentID = ParentOrder.m_strParentID;
            //    m_objCurrentOrder.m_strParentName = ParentOrder.m_strParentName;
            //}
            //else
            //{
            //    m_objCurrentOrder.m_strParentID = ParentOrder.m_strOrderID;
            //    m_objCurrentOrder.m_strParentName = ParentOrder.m_strName;
            //}
            //�����ǰҽ������ҽ��,ȡ�丸ҽ����Ϊ��ҽ��
            if (ParentOrder.m_strParentID.Trim() != "" && ParentIDOrderHave(ParentOrder))
            {
                this.m_ctlOrderDetail.m_txtFatherOrder.Text = ParentOrder.m_strParentName;
                this.m_ctlOrderDetail.m_txtFatherOrder.Tag = ParentOrder.m_strParentID;
                this.m_objCurrentOrder.m_strParentID = ParentOrder.m_strParentID;
                this.m_objCurrentOrder.m_strParentName = ParentOrder.m_strParentName;
            }
            else
            {
                this.m_ctlOrderDetail.m_txtFatherOrder.Text = ParentOrder.m_strName;
                this.m_ctlOrderDetail.m_txtFatherOrder.Tag = ParentOrder.m_strOrderID;
                this.m_objCurrentOrder.m_strParentID = ParentOrder.m_strOrderID;
                this.m_objCurrentOrder.m_strParentName = ParentOrder.m_strName;

            }
            m_objCurrentOrder.m_strDosetypeName = ParentOrder.m_strDosetypeName;
            m_objCurrentOrder.m_strExecFreqID = ParentOrder.m_strExecFreqID;
            m_objCurrentOrder.m_strExecFreqName = ParentOrder.m_strExecFreqName;
            /* ����*/
            m_objCurrentOrder.m_strSAMPLEName_VCHR = ParentOrder.m_strSAMPLEName_VCHR.ToString().Trim();
            m_objCurrentOrder.m_strSAMPLEID_VCHR = ParentOrder.m_strSAMPLEID_VCHR.ToString().Trim();
            /* ���*/
            m_objCurrentOrder.m_strPARTNAME_VCHR = ParentOrder.m_strPARTNAME_VCHR.ToString().Trim();
            m_objCurrentOrder.m_strPARTID_VCHR = ParentOrder.m_strPARTID_VCHR.ToString().Trim();
            //����/����
            m_objCurrentOrder.m_intOUTGETMEDDAYS_INT = ParentOrder.m_intOUTGETMEDDAYS_INT;
            m_objCurrentOrder.m_strPatientID = ParentOrder.m_strPatientID;
            //����
            m_objCurrentOrder.m_intATTACHTIMES_INT = ParentOrder.m_intATTACHTIMES_INT;
            //����ҽ��
            m_objCurrentOrder.m_strDOCTORID_CHR = ParentOrder.m_strDOCTORID_CHR;
            m_objCurrentOrder.m_strDOCTOR_VCHR = ParentOrder.m_strDOCTOR_VCHR;

            m_intCurrentRow = -1;
            //��ʾҽ����ϸ������Ϣ
            m_mthShowCurrentOrder(m_objCurrentOrder);
            m_ctlOrderDetail.m_mthSetDoctor(m_objCurrentDoctor.m_strDoctorID, m_objCurrentDoctor.m_strDoctorName);

            m_ctlOrderDetail.m_txtOrderName.Enabled = true;
            m_ctlOrderDetail.m_txtOrderName.BackColor = System.Drawing.Color.White;

            this.m_cmdSave.Enabled = true;
        }

        /// <summary>
        /// ������ҽ�����з��Ÿı����һҽ������ҽ��ʱ�Ľ������
        /// </summary>
        /// <param name="ParentOrder"></param>
        private void SetTheOldSubOrder()
        {
            //��ҽ�������ĳ�ʼ������ ===========================>
            //ҽ�����͸ı�ʱ�Ŀ���(����/����/��Ժ��ҩ)
            this.m_ctlOrderDetail.m_cboExecuteType_SelectedIndexChanged(null, null);
            //���� ��ҽ��¼�����Ŀؼ�
            //EnableTheBihDetailControl(false);
            /*<==================================================*/
            clsBIHOrder ParentOrder = m_ctlOrderDetail.ParentOrder;
            //�����ǰҽ������ҽ��,ȡ�丸ҽ����Ϊ��ҽ��
            if (ParentOrder.m_strParentID.Trim() != "" && ParentIDOrderHave(ParentOrder))
            {
                this.m_ctlOrderDetail.m_txtFatherOrder.Text = ParentOrder.m_strParentName;
                this.m_ctlOrderDetail.m_txtFatherOrder.Tag = ParentOrder.m_strParentID;
                this.m_objCurrentOrder.m_strParentID = ParentOrder.m_strParentID;
                this.m_objCurrentOrder.m_strParentName = ParentOrder.m_strParentName;
            }
            else
            {
                this.m_ctlOrderDetail.m_txtFatherOrder.Text = ParentOrder.m_strName;
                this.m_ctlOrderDetail.m_txtFatherOrder.Tag = ParentOrder.m_strOrderID;
                this.m_objCurrentOrder.m_strParentID = ParentOrder.m_strOrderID;
                this.m_objCurrentOrder.m_strParentName = ParentOrder.m_strName;
            }

            this.m_ctlOrderDetail.m_cboExecuteType.SelectedIndex = ParentOrder.m_intExecuteType - 1;
            this.m_ctlOrderDetail.m_txtDosageType.Tag = ParentOrder.m_strDosetypeID;
            this.m_ctlOrderDetail.m_txtDosageType.Text = ParentOrder.m_strDosetypeName;
            this.m_ctlOrderDetail.m_txtExecuteFreq.Tag = ParentOrder.m_strExecFreqID;
            this.m_ctlOrderDetail.m_txtExecuteFreq.Text = ParentOrder.m_strExecFreqName;
            //����
            this.m_ctlOrderDetail.m_txtATTACHTIMES_INT.Text = ParentOrder.m_intATTACHTIMES_INT.ToString();
            //����ҽ��
            this.m_ctlOrderDetail.m_txtDoctorList.Text = ParentOrder.m_strDOCTOR_VCHR;
            this.m_ctlOrderDetail.m_txtDoctorList.Tag = ParentOrder.m_strDOCTORID_CHR;


            if (ParentOrder.m_dtStartDate != DateTime.MinValue)
            {
                this.m_ctlOrderDetail.m_dtStartTime2.Text = ParentOrder.m_dtStartDate.ToString("yyyy��MM��dd��HHʱmm��");
            }
            else
            {
                this.m_ctlOrderDetail.m_dtStartTime2.Text = "";
            }
            if (ParentOrder.m_dtFinishDate != DateTime.MinValue)
            {
                this.m_ctlOrderDetail.m_dtFinishTime2.Text = ParentOrder.m_dtFinishDate.ToString("yyyy��MM��dd��HHʱmm��");
            }
            else
            {
                this.m_ctlOrderDetail.m_dtFinishTime2.Text = "";
            }
            //this.m_ctlOrderDetail.m_cboExecuteType.Enabled = false;
            //this.m_ctlOrderDetail.m_txtExecuteFreq.Enabled = false;
            //this.m_ctlOrderDetail.m_txtExecuteFreq.BackColor = SystemColors.Control;
            //this.m_ctlOrderDetail.m_txtDosageType.Enabled = false;
            //this.m_ctlOrderDetail.m_txtDosageType.BackColor = SystemColors.Control;
            //this.m_ctlOrderDetail.m_dtStartTime2.Enabled = false;
            //this.m_ctlOrderDetail.m_dtFinishTime2.Enabled = false;



        }

        /// <summary>
        /// ��ǰҽ���ĸ�ҽ���Ƿ���ڵ�ǰҽ���б���
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        private bool ParentIDOrderHave(clsBIHOrder Order)
        {
            bool m_blHave = false;
            for (int i = 0; i < this.m_dtvOrder.RowCount; i++)
            {
                if (((clsBIHOrder)this.m_dtvOrder.Rows[i].Tag).m_strOrderID == Order.m_strParentID)
                {
                    m_blHave = true;
                    break;
                }
            }
            return m_blHave;
        }

        private void m_ctlOrderDetail_m_evtInputNO(object sender, EventArgs e)
        {
            m_cmdSubOrderByRecipeNo2();
        }

        #region �۷�ҽ������
        /* add by wjwqin(06-7-21)*/
        public void m_mthShow(string m_strClass)
        {
            switch (m_strClass)
            {
                case "-1"://����/��������m_strView
                    //MessageBox.Show("-1");
                    m_strView = "0";
                    m_intSOURCETYPE_INT = 1;
                    break;
                case "0"://����/��������m_strView
                    //MessageBox.Show("0");
                    m_strView = "0";
                    break;
                case "1"://��������
                    //MessageBox.Show("1");
                    m_strView = "1";

                    break;
                case "2"://��������
                    //MessageBox.Show("2");
                    m_strView = "2";
                    break;
                case "3"://ҽ���ɵ��Ӳ�����
                    m_strView = "3";
                    break;
            }

            this.Show();


        }


        #endregion

        /// <summary>
        /// �����������
        /// </summary>
        public void LongBihorderSet()
        {
            m_rdoAllType.Checked = false;
            m_rdoLongType.Checked = true;
            m_rdoAllType.Enabled = false;
            m_rdoTempType.Enabled = false;
            m_ctlOrderDetail.m_cboExecuteType.SelectedIndex = 0;
            m_ctlOrderDetail.m_cboExecuteType.Enabled = false;
        }
        /*<===========================================*/
        /// <summary>
        /// �����������
        /// </summary>
        public void ShortBihorderSet()
        {
            m_rdoAllType.Checked = false;
            m_rdoLongType.Checked = false;

            m_rdoLongType.Enabled = false;
            m_rdoAllType.Enabled = false;

            m_rdoTempType.Enabled = true;
            m_rdoTempType.Checked = true;

            m_ctlOrderDetail.m_cboExecuteType.SelectedIndex = 1;
            m_ctlOrderDetail.m_cboExecuteType.Enabled = false;
        }

        private void m_cmdChgView_Click(object sender, EventArgs e)
        {
            //��/�ٽ������ 
            if (m_cmdChgView.Tag == null)
            {
                m_cmdChgView.Tag = "0";
            }
            string m_strState = (string)m_cmdChgView.Tag;
            if (m_strState.Equals("1") || m_strState.Equals("0"))
            {
                m_strView = "1";
                LongBihorderSet();

            }
            else if (m_strState.Equals("2"))
            {
                m_strView = "2";
                ShortBihorderSet();

            }

            /*<========================*/
            cmdRefurbish_Click(null, null);
            setTheChgViewButton();
        }

        public void setTheChgViewButton()
        {
            if (m_cmdChgView.Tag == null)
            {
                m_cmdChgView.Tag = "0";
            }
            switch ((string)m_cmdChgView.Tag)
            {
                case "0":
                    m_cmdChgView.Text = "ת������";
                    m_cmdChgView.Tag = "2";
                    break;
                case "1":
                    m_cmdChgView.Text = "ת������";
                    m_cmdChgView.Tag = "2";
                    break;
                case "2":
                    m_cmdChgView.Text = "ת������";
                    m_cmdChgView.Tag = "1";
                    break;
                default:
                    m_cmdChgView.Text = "��/����";
                    m_cmdChgView.Tag = "0";
                    break;
            }
        }


        private void frmBIHOrderInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_blAccess == false)
            {
                e.Cancel = false;
                return;
            }
            for (int i1 = 0; i1 < m_dtvOrder.RowCount; i1++)
            {
                clsBIHOrder order1 = (clsBIHOrder)m_dtvOrder.Rows[i1].Tag;
                if (order1.m_intStatus == 0 && order1.m_strCreatorID.Trim().Equals(this.LoginInfo.m_strEmpID))
                {
                    if (MessageBox.Show("��ҽ����δ�ύ���Ƿ��ύҽ����", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        e.Cancel = true;

                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (e.Cancel == true)
            {
                m_cmdToCommit_Click(null, null);
            }
            if (e.Cancel == false)
            {
                if (MessageBox.Show("ȷ��Ҫ�˳���ǰҽ��������", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    e.Cancel = false;

                }
                else
                {
                    e.Cancel = true;

                }
            }

        }

        private void m_cboOrderList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.m_objDomain.m_mthLoadOrderList();
            this.Cursor = Cursors.Default;
            //ͬ��ҽ�����Ͳ���
            if (m_ctlOrderDetail.m_objCurrentOrder == null)//�޸Ĳ���������ҽ������������
            {
                if (m_cboOrderList.SelectedIndex >= 1 && m_cboOrderList.SelectedIndex <= 3)
                {
                    m_ctlOrderDetail.m_cboExecuteType.SelectedIndex = m_cboOrderList.SelectedIndex - 1;
                }
            }
        }

        private void m_cmdChange_Click(object sender, EventArgs e)
        {
            CurrentBihOrderChanged();
        }

        #region �����¼�

        private void m_txtCREATEAREA_m_evtFindItem(object sender, string strFindCode, ListView lvwList)
        {
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            clsBIHArea[] arrArea;
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngFindArea(strFindCode, out arrArea);
            if ((ret > 0) && (arrArea != null))
            {
                //��ȡ��Ȩ�޷��ʵĲ���ID����
                if (this.m_objLoginInfo != null)
                {
                    IList ilUsableAreaID = this.m_objLoginInfo.m_ilUsableAreaID;
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
                    lvi.Tag = arrArea[i].m_strAreaID;
                }
            }
        }

        private void m_txtCREATEAREA_m_evtInitListView(ListView lvwList)
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

        private void m_txtCREATEAREA_m_evtSelectItem(object sender, ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                m_txtCREATEAREA.Text = lviSelected.SubItems[1].Text;
                m_txtCREATEAREA.Tag = lviSelected.Tag;


            }
        }

        #endregion

        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            this.m_cboOrderList.SelectedIndex = 6;
            m_cboOrderList_SelectionChangeCommitted(null, null);
        }

        private void pictureBox6_DoubleClick(object sender, EventArgs e)
        {
            this.m_cboOrderList.SelectedIndex = 6;
            m_cboOrderList_SelectionChangeCommitted(null, null);
        }

        private void m_dtvOrder_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.PageUp:
                    m_dtgOrder_m_evtCurrentCellChanged(null, null);
                    e.Handled = true;
                    break;
                case Keys.PageDown:
                    m_dtgOrder_m_evtCurrentCellChanged(null, null);
                    e.Handled = true;
                    break;
            }

        }

        public void m_dtvOrder_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (m_IsDisPlayToolTip)
            {
                System.Drawing.Point pt = new Point();
                pt.Offset(this.m_dtvOrder.Location.X, this.m_dtvOrder.Location.Y);
                System.Drawing.Point po;
                m_poToolTip = new System.Drawing.Point(pt.X, pt.Y + 20 + 66);
                //������Ϣ
                DisplayToolTip(m_poToolTip);
            }
        }

        private void TSMenuTurnBack_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count <= 0)
            {
                MessageBox.Show("����ѡ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            m_objDomain.BihOrderDrawBack(((clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag).m_strOrderID);
        }

        internal void m_objGetChangedOrder(ref clsBIHOrder objOrder)
        {
            m_ctlOrderDetail.m_objGetChangedOrder(m_ctlPatient.m_objPatient, ref objOrder);
        }

        internal void m_cmdMoneyCount_Click(object sender, EventArgs e)
        {
            decimal m_decMoney = 0;
            clsBIHPatientInfo objPatient = m_ctlPatient.m_objPatient;
            if (objPatient == null)
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            long m_lngRef = m_objDomain.m_lngMoneyCountNewOrder(objPatient.m_strRegisterID, out m_decMoney);
            this.Cursor = Cursors.Default;
            if (m_lngRef > 0)
            {
                frmSelectBox selectBox = new frmSelectBox(m_decMoney, 7);
                selectBox.ShowDialog();
            }
        }

        internal void m_cmdDelete2_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count <= 0)
            {
                MessageBox.Show("����ѡ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string m_strMessage = "";
            ArrayList m_arrOrderSameNo;
            m_blComfirmDeleteMessage(out m_strMessage, out m_arrOrderSameNo);
            if (!m_strMessage.Trim().Equals(""))
            {
                MessageBox.Show(m_strMessage, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //ɾ��ҽ��
            if (m_arrOrderSameNo.Count <= 0)
            {
                return;
            }
            else
            {
                if (MessageBox.Show("ȷ�Ͻ���ɾ��������?", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    ArrayList m_arrOrderIDs = new ArrayList();//������ҽ��Ҫɾ������
                    ArrayList m_arrContinue = new ArrayList();//������ҽ��Ҫɾ������
                    List<string> lstOrderId_Lis = new List<string>(); //ɾ���������뵥
                    for (int i = 0; i < m_arrOrderSameNo.Count; i++)
                    {
                        m_arrOrderIDs.Add(((clsBIHOrder)m_arrOrderSameNo[i]).m_strOrderID);
                        if (m_objSpecateVo.m_strCONFREQID_CHR.Trim().Equals(((clsBIHOrder)m_arrOrderSameNo[i]).m_strExecFreqID.Trim()))
                        {
                            m_arrContinue.Add(((clsBIHOrder)m_arrOrderSameNo[i]).m_strOrderID);
                        }

                        if (((clsBIHOrder)m_arrOrderSameNo[i]).m_intStatus != 0 && m_objSpecateVo.m_strORDERCATEID_LIS_CHR.Trim().Equals(((clsBIHOrder)m_arrOrderSameNo[i]).m_strOrderDicCateID))
                        {
                            lstOrderId_Lis.Add(((clsBIHOrder)m_arrOrderSameNo[i]).m_strOrderID);
                        }
                    }
                    List<string> lstSameGroupOrderId = new List<string>();
                    if (lstOrderId_Lis.Count > 0)
                    {
                        List<string> lstTmpId = new List<string>();
                        //using (clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject())
                        //{
                        foreach (string orderId in lstOrderId_Lis)
                        {
                            lstTmpId = (new weCare.Proxy.ProxyIP()).Service.GetSameGroupLisOrderId(orderId);
                            if (lstTmpId.Count > 0)
                            {
                                lstSameGroupOrderId.AddRange(lstTmpId);
                            }
                        }
                        //}
                        if (lstSameGroupOrderId.Count > 0)
                        {
                            string orderName = string.Empty;
                            for (int i = 0; i < this.m_dtvOrder.RowCount; i++)
                            {
                                clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.Rows[i].Tag;
                                if (lstSameGroupOrderId.IndexOf(order.m_strOrderID) >= 0)
                                {
                                    orderName += order.m_strName + Environment.NewLine;
                                }
                            }
                            if (MessageBox.Show("����ɾ����ͬʱɾ������ͬ�����������Ŀ��" + Environment.NewLine + orderName + Environment.NewLine + Environment.NewLine + "�Ƿ��������Ҫ�ֹ���¼���ҽ��������", "���棡����", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }
                            lstOrderId_Lis.AddRange(lstSameGroupOrderId);
                        }
                        //ɾ���������뵥
                        clsBIHLis obj = new clsBIHLis();
                        bool m_blOK = false;
                        foreach (string orderId in lstOrderId_Lis)
                        {
                            m_blOK = obj.m_mthDeleteApp(orderId, out m_strMessage);
                            if (!m_blOK)
                            {
                                m_arrContinue.Remove(orderId);
                                m_arrOrderIDs.Remove(orderId);
                                MessageBox.Show("����ʧ��! " + m_strMessage, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                    }

                    string[] m_strOrderID = null;
                    List<string> lstOrderId = new List<string>();
                    if (m_arrOrderIDs.Count > 0)
                    {
                        //m_strOrderID = (string[])m_arrOrderIDs.ToArray(typeof(string));
                        lstOrderId.AddRange((string[])m_arrOrderIDs.ToArray(typeof(string)));
                    }
                    if (lstSameGroupOrderId.Count > 0)
                    {
                        lstOrderId.AddRange(lstSameGroupOrderId);
                    }

                    string[] m_strOrderID2 = null;
                    if (m_arrContinue.Count > 0)
                    {
                        m_strOrderID2 = (string[])m_arrContinue.ToArray(typeof(string));
                    }

                    if (lstOrderId.Count > 0) m_strOrderID = lstOrderId.ToArray();
                    long m_lngRef = m_objDomain.m_lngDeleteOrder(m_strOrderID, m_strOrderID2);
                    this.Cursor = Cursors.Default;
                    if (m_lngRef > 0)
                    {
                        //MessageBox.Show("�����ɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.m_objDomain.m_mthLoadOrderList();
                    }
                }
            }
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        internal void m_lngBlankOut()
        {
            if (this.m_dtvOrder.SelectedRows.Count <= 0)
            {
                MessageBox.Show("����ѡ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string m_strMessage = "";
            ArrayList m_arrOrderSameNo;
            string m_strAlert = m_blComfirmBlankOutMessage(out m_strMessage, out m_arrOrderSameNo);
            if (!m_strMessage.Trim().Equals(""))
            {
                MessageBox.Show(m_strMessage, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //����ҽ��
            if (m_arrOrderSameNo.Count <= 0)
            {
                return;
            }
            else
            {
                if (MessageBox.Show("ȷ�Ͻ������ϲ�����?", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    ArrayList m_arrOrderIDs = new ArrayList();//������ҽ��Ҫɾ������
                    ArrayList m_arrLis = new ArrayList();//ɾ���������뵥
                    for (int i = 0; i < m_arrOrderSameNo.Count; i++)
                    {
                        m_arrOrderIDs.Add(((clsBIHOrder)m_arrOrderSameNo[i]).m_strOrderID);
                        if (((clsBIHOrder)m_arrOrderSameNo[i]).m_intStatus != 0 && m_objSpecateVo.m_strORDERCATEID_LIS_CHR.Trim().Equals(((clsBIHOrder)m_arrOrderSameNo[i]).m_strOrderDicCateID))
                        {
                            m_arrLis.Add(((clsBIHOrder)m_arrOrderSameNo[i]).m_strOrderID);
                        }
                    }

                    //ɾ���������뵥
                    clsBIHLis obj = new clsBIHLis();
                    bool m_blOK = false;
                    for (int i = 0; i < m_arrLis.Count; i++)
                    {
                        m_blOK = obj.m_mthDeleteApp((string)m_arrLis[i], out m_strMessage);
                        if (!m_blOK)
                        {
                            m_arrOrderIDs.Remove((string)m_arrLis[i]);
                            MessageBox.Show("����ʧ��! " + m_strMessage, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                    string[] m_strOrderID = null;
                    if (m_arrOrderIDs.Count > 0)
                    {
                        m_strOrderID = (string[])m_arrOrderIDs.ToArray(typeof(string));
                    }
                    if (m_strOrderID == null)
                    {
                        return;
                    }
                    long m_lngRef = m_objDomain.m_lngBlankOutOrder(m_strOrderID, this.LoginInfo.m_strEmpID, this.LoginInfo.m_strEmpName);
                    if (m_lngRef > 0)
                    {
                        if (!m_strAlert.Trim().Equals(""))//������ִ��ҽ�������Ϻ���˷���ʾ
                        {
                            m_strAlert = "��������ҽ��������˷�,��֪ͨ��Ա�����˷Ѳ���!" + m_strAlert;
                        }
                        m_strAlert = "�����ɹ�!" + "\r\n" + m_strAlert;
                        MessageBox.Show(m_strAlert, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.m_objDomain.m_mthLoadOrderList();
                    }
                }
            }
        }

        /// <summary>
        /// ֹͣҽ��
        /// </summary>
        internal void m_lngStopOrder()
        {
            if (this.m_dtvOrder.SelectedRows.Count <= 0)
            {
                MessageBox.Show("����ѡ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string m_strMessage = "";
            ArrayList m_arrOrderSameNo;
            m_blComfirmStopOrderMessage(out m_strMessage, out m_arrOrderSameNo);
            if (!m_strMessage.Trim().Equals(""))
            {
                MessageBox.Show(m_strMessage, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //ֹͣҽ��
            if (m_arrOrderSameNo.Count <= 0)
            {
                return;
            }
            else
            {
                bool m_blComfirm = false;
                string m_strComfirmId = "";
                string m_strComfirmer = "";

                if (m_blStopTipControl == true)
                {
                    #region Ա�������������ʾ

                    string empid = "";
                    string empname = "";

                    DialogResult dlg = com.digitalwave.iCare.gui.HIS.clsPublic.m_dlgConfirm(this.LoginInfo.m_strEmpNo, out empid, out empname);
                    if (dlg == DialogResult.Yes)
                    {
                        m_strComfirmId = empid;
                        m_strComfirmer = empname;
                        m_blComfirm = true;
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
                    //    m_strComfirmId = comfirmBox1.empid_chr;//���ĵ�ǰ¼����ID
                    //    m_strComfirmer = comfirmBox1.lastname_vchr;//���ĵ�ǰ¼��������
                    //    m_blComfirm = true;
                    //}

                    #endregion
                }
                else
                {
                    if (MessageBox.Show("ȷ�Ͻ���ֹͣ������?", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        m_strComfirmId = this.LoginInfo.m_strEmpID;//���ĵ�ǰ¼����ID
                        m_strComfirmer = this.LoginInfo.m_strEmpName;//���ĵ�ǰ¼��������
                        m_blComfirm = true;
                    }
                }
                if (m_blComfirm == true)
                {
                    string[] m_strOrderID = new string[m_arrOrderSameNo.Count];
                    ArrayList m_arrOrderID = new ArrayList();
                    for (int i = 0; i < m_arrOrderSameNo.Count; i++)
                    {
                        m_strOrderID[i] = ((clsBIHOrder)m_arrOrderSameNo[i]).m_strOrderID;
                        m_arrOrderID.Add(m_strOrderID[i]);
                    }
                    long m_lngRef = m_objDomain.m_lngStopOrder(m_strOrderID, m_strComfirmId, m_strComfirmer);
                    if (m_lngRef > 0)
                    {
                        MessageBox.Show("�����ɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        for (int i = 0; i < this.m_dtvOrder.Rows.Count; i++)
                        {
                            if (m_arrOrderID.Contains(((clsBIHOrder)this.m_dtvOrder.Rows[i].Tag).m_strOrderID))
                            {
                                clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.Rows[i].Tag;
                                order.m_intStatus = 3;
                                order.m_dtFinishDate = DateTime.Now;
                                order.m_dtStopdate = DateTime.Now;
                                order.m_strStoperID = m_strComfirmId;
                                order.m_strStoper = m_strComfirmer;
                                this.m_objDomain.m_objGetDataViewRow(order, this.m_dtvOrder.Rows[i], i + 1);
                            }
                        }
                        //ˢ��ͬ��ҽ���ķ�����ɫ
                        this.m_objDomain.m_mthRefreshSameReqNoColor();
                    }
                }
            }

        }
        /// <summary>
        ///  �жϵ�ǰѡ�е�ҽ���Ƿ���Խ���ɾ������
        /// </summary>
        /// <returns></returns>
        private void m_blComfirmDeleteMessage(out string m_strMessage, out ArrayList m_arrOrderSameNo)
        {
            m_arrOrderSameNo = new ArrayList();
            m_arrOrderSameNo = getSelectRowItemWithSon();

            #region ˢ�µ�ǰҽ�����ݣ�Ȼ�����ж�

            List<string> m_arrORDERID_CHR = new List<string>();
            string m_strOrderID = "";
            for (int i = 0; i < m_arrOrderSameNo.Count; i++)
            {
                m_strOrderID = ((clsBIHOrder)m_arrOrderSameNo[i]).m_strOrderID;
                if (!m_arrORDERID_CHR.Contains(m_strOrderID))
                {
                    m_arrORDERID_CHR.Add(m_strOrderID);
                }
            }

            clsBIHOrder[] arrOrder = null;
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            (new weCare.Proxy.ProxyIP()).Service.m_lngGetArrOrderByOrderID(m_arrORDERID_CHR, out arrOrder);
            if (arrOrder != null && arrOrder.Length > 0)
            {
                for (int i = 0; i < arrOrder.Length; i++)
                {
                    clsBIHOrder order = arrOrder[i];
                    DataGridViewRow row = GetTheGridRowByOrder(order.m_strOrderID);
                    this.m_objDomain.m_objGetDataViewRow(order, row, row.Index + 1);
                }
                m_arrOrderSameNo = new ArrayList();
                m_arrOrderSameNo = getSelectRowItemWithSon();
            }

            this.m_objDomain.m_mthRefreshSameReqNoColor();
            #endregion

            m_strMessage = "";
            string m_strMessage2 = "", m_strMessage3 = "";
            for (int i = 0; i < m_arrOrderSameNo.Count; i++)
            {
                clsBIHOrder BihOrder = (clsBIHOrder)m_arrOrderSameNo[i];
                if (BihOrder.m_strCreatorID == this.LoginInfo.m_strEmpID || BihOrder.m_strDOCTORID_CHR == this.LoginInfo.m_strEmpID)
                {

                }
                else
                {
                    m_strMessage2 += "\r\n" + "  { " + BihOrder.m_strName + " }";
                }
                if (BihOrder.m_intStatus == 0 || BihOrder.m_intStatus == 1 || BihOrder.m_intStatus == 7)
                {

                }
                else if (BihOrder.m_intTYPE_INT == 3 || BihOrder.m_intTYPE_INT == 4 || BihOrder.m_intTYPE_INT == 2 || BihOrder.m_intTYPE_INT == 1)
                {
                }
                else
                {
                    m_strMessage3 += "\r\n" + "  { " + BihOrder.m_strName + " }";
                }
            }
            if (!m_strMessage2.Trim().Equals(""))
            {
                m_strMessage2 = "\r\n" + " û���㹻Ȩ��ɾ������ҽ��" + m_strMessage2;
            }
            if (!m_strMessage3.Trim().Equals(""))
            {
                m_strMessage3 = "\r\n" + " ����ɾ����ǰ״̬��ҽ��" + m_strMessage3;
            }
            m_strMessage = m_strMessage2 + m_strMessage3;
        }

        /// <summary>
        ///  �жϵ�ǰѡ�е�ҽ���Ƿ���Խ������ϲ���
        /// </summary>
        /// <returns></returns>
        private string m_blComfirmBlankOutMessage(out string m_strMessage, out ArrayList m_arrOrderSameNo)
        {
            m_arrOrderSameNo = new ArrayList();
            m_arrOrderSameNo = getSelectRowItemWithSon();
            m_strMessage = "";
            string m_strMessage2 = "", m_strMessage3 = "", m_strMessage4 = "";
            // �ѱ���/�����������ҽ����������
            clsBIHLis obj = new clsBIHLis();
            clsT_OPR_LIS_SAMPLE_VO objSample = new clsT_OPR_LIS_SAMPLE_VO();

            for (int i = 0; i < m_arrOrderSameNo.Count; i++)
            {
                clsBIHOrder BihOrder = (clsBIHOrder)m_arrOrderSameNo[i];
                objSample = null;
                if (!string.IsNullOrEmpty(BihOrder.m_strLISAPPLYUNITID_CHR) && !string.IsNullOrEmpty(BihOrder.m_strOrderID))
                {
                    obj.m_mthGetLisSample(BihOrder.m_strOrderID, out objSample);
                }

                if (objSample != null)
                {
                    if (objSample.m_intSTATUS_INT > 2 && objSample.m_intSTATUS_INT < 7 && !string.IsNullOrEmpty(objSample.m_strBARCODE_VCHR))
                    {
                        m_strMessage3 += "\r\n" + "  { " + BihOrder.m_strName + " }";//��������
                        continue;
                    }
                    else
                    { }
                }
                if (BihOrder.m_intStatus == 0 || BihOrder.m_intStatus == 1 || BihOrder.m_intStatus == 5 || BihOrder.m_intStatus == 7 || (BihOrder.m_intStatus == 2 && BihOrder.m_intExecuteType == 2))
                {
                    if (BihOrder.m_intStatus == 2)
                    {
                        m_strMessage4 += "\r\n" + "  { " + BihOrder.m_strName + " }";
                    }
                }
                else
                {
                    m_strMessage3 += "\r\n" + "  { " + BihOrder.m_strName + " }";
                }
            }
            if (!m_strMessage2.Trim().Equals(""))
            {
                m_strMessage2 = "\r\n" + " û���㹻Ȩ����������ҽ��" + m_strMessage2;
            }
            if (!m_strMessage3.Trim().Equals(""))
            {
                m_strMessage3 = "\r\n" + " ֻ�������¿�,�ύ,�˻ػ�������ִ�е�ҽ��,�Ѻ��ձ걾���ѱ���ļ���ҽ����������" + m_strMessage3;
            }
            m_strMessage = m_strMessage2 + m_strMessage3;
            return m_strMessage4;
        }

        /// <summary>
        ///  �жϵ�ǰѡ�е�ҽ���Ƿ���Խ���ֹͣ����
        /// </summary>
        /// <returns></returns>
        private void m_blComfirmStopOrderMessage(out string m_strMessage, out ArrayList m_arrOrderSameNo)
        {
            m_arrOrderSameNo = new ArrayList();
            m_arrOrderSameNo = getSelectRowItemWithSon();
            m_strMessage = "";
            string m_strMessage2 = "", m_strMessage3 = "";
            for (int i = 0; i < m_arrOrderSameNo.Count; i++)
            {
                clsBIHOrder BihOrder = (clsBIHOrder)m_arrOrderSameNo[i];
                if (m_blCanChangeOrder == false)
                {
                    if (BihOrder.m_strCreatorID == this.LoginInfo.m_strEmpID || BihOrder.m_strDOCTORID_CHR == this.LoginInfo.m_strEmpID)
                    {
                    }
                    else
                    {
                        m_strMessage2 += "\r\n" + "  { " + BihOrder.m_strName + " }";
                    }
                }
                if (BihOrder.m_intStatus == 2 && BihOrder.m_intExecuteType == 1)
                {
                }
                else
                {
                    m_strMessage3 += "\r\n" + "  { " + BihOrder.m_strName + " }";
                }
            }
            if (!m_strMessage2.Trim().Equals(""))
            {
                m_strMessage2 = "\r\n" + " û���㹻Ȩ�޴�������ҽ��" + m_strMessage2;
            }
            if (!m_strMessage3.Trim().Equals(""))
            {
                m_strMessage3 = "\r\n" + " ֻ�ܶ�δͣ�ĳ������д˲���,����ҽ������������ " + m_strMessage3;
            }
            m_strMessage = m_strMessage2 + m_strMessage3;
        }

        /// <summary>
        /// �õ�ѡ�е�ҽ������������ҽ����
        /// </summary>
        /// <returns></returns>
        public ArrayList getSelectRowItemWithSon()
        {
            ArrayList m_arrOrderSameNo = new ArrayList();
            ArrayList m_arrRecipenNo = new ArrayList();
            for (int i = 0; i < this.m_dtvOrder.SelectedRows.Count; i++)
            {
                clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[i].Tag;
                if (m_arrRecipenNo.Contains(order.m_intRecipenNo.ToString()) == false)
                {
                    m_arrRecipenNo.Add(order.m_intRecipenNo.ToString());
                }
            }
            bool m_blCan = true;//������ҩ�ģ�ѡ�еĲ���ɾ������ͬ����û��ѡ�еĲ�����
            for (int i = 0; i < this.m_dtvOrder.RowCount; i++)
            {
                m_blCan = true;
                clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.Rows[i].Tag;
                if (!this.m_dtvOrder.Rows[i].Selected)
                {
                    m_blCan = false;
                }
                if (m_arrRecipenNo.Contains(order.m_intRecipenNo.ToString()) && m_blCan)
                {
                    m_arrOrderSameNo.Add(order);
                }
            }
            return m_arrOrderSameNo;
        }




        public void m_mnuDoctorSign_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count <= 0)
            {
                MessageBox.Show("����ѡ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string empid = "";
            string empname = "";

            DialogResult dlg = com.digitalwave.iCare.gui.HIS.clsPublic.m_dlgConfirm(out empid, out empname);
            if (dlg == DialogResult.Yes)
            {
                if (!((clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag).m_strDOCTORID_CHR.Equals(empid))
                {
                    MessageBox.Show("ֻ���Ǳ�ҽ����ҽ������ǩ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                long lngRes = m_objDomain.m_mthCurrentOrderDoctorSign((clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag, empid, empname);
                if (lngRes > 0)
                {
                    MessageBox.Show("ǩ���ɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ((clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag).SIGN_INT = 1;
                    clsBIHOrder objOrder = (clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag;
                    if (objOrder.SIGN_INT == 1)
                    {
                        this.m_dtvOrder.SelectedRows[0].DefaultCellStyle.ForeColor = Color.Black;
                        this.m_dtvOrder.DefaultCellStyle.SelectionForeColor = selectOldColor;
                    }
                    if (objOrder.SIGN_GRP != null && objOrder.SIGN_INT == 1)
                    {
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(objOrder.SIGN_GRP);
                        Bitmap m_bpSign = new Bitmap(ms);
                        this.m_dtvOrder.SelectedRows[0].Cells["dtv_DOCTOR_SIGN"].Value = m_bpSign;

                        ms.Close();
                    }
                    else
                    {
                        this.m_dtvOrder.SelectedRows[0].Cells["dtv_DOCTOR_SIGN"].Style.NullValue = null;
                        this.m_dtvOrder.SelectedRows[0].Cells["dtv_DOCTOR_SIGN"].Value = null;
                    }
                }
            }

            #region
            //DotorComfirmBox comfirmBox1 = new DotorComfirmBox();
            //if (comfirmBox1.ShowDialog() == DialogResult.OK)
            //{
            //    if (!((clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag).m_strDOCTORID_CHR.Equals(comfirmBox1.empid_chr))
            //    {
            //        MessageBox.Show("ֻ���Ǳ�ҽ����ҽ������ǩ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        return;
            //    }
            //    long lngRes = m_objDomain.m_mthCurrentOrderDoctorSign((clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag, comfirmBox1.empid_chr, comfirmBox1.lastname_vchr);
            //    if (lngRes > 0)
            //    {
            //        MessageBox.Show("ǩ���ɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        ((clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag).SIGN_INT = 1;
            //        clsBIHOrder objOrder=(clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag;
            //        if (objOrder.SIGN_INT == 1)
            //        {
            //            this.m_dtvOrder.SelectedRows[0].DefaultCellStyle.ForeColor = Color.Black;
            //            this.m_dtvOrder.DefaultCellStyle.SelectionForeColor = selectOldColor;
            //        }
            //        if (objOrder.SIGN_GRP != null && objOrder.SIGN_INT == 1)
            //        {
            //            System.IO.MemoryStream ms = new System.IO.MemoryStream(objOrder.SIGN_GRP);
            //            Bitmap m_bpSign = new Bitmap(ms);
            //            this.m_dtvOrder.SelectedRows[0].Cells["dtv_DOCTOR_SIGN"].Value = m_bpSign;

            //            ms.Close();
            //        }
            //        else
            //        {
            //            this.m_dtvOrder.SelectedRows[0].Cells["dtv_DOCTOR_SIGN"].Style.NullValue = null;
            //            this.m_dtvOrder.SelectedRows[0].Cells["dtv_DOCTOR_SIGN"].Value = null;
            //        }
            //     }
            //}
            //comfirmBox1.Close();
            #endregion
        }

        private void m_dtvOrder_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void m_mthFillApplyInfo(out clsApplyRecord objApplyVO)
        {
            objApplyVO = new clsApplyRecord();
            objApplyVO.m_datApplyDate = DateTime.Now;
            objApplyVO.m_strAddress = "";
            objApplyVO.m_strAge = m_ctlPatient.m_objPatient.m_intAge.ToString();
            objApplyVO.m_strCardNO = m_ctlPatient.m_objPatient.m_strPATIENTCARDID_CHR;
            objApplyVO.m_strDiagnose = m_ctlPatient.m_objPatient.m_strDiagnose;
            objApplyVO.m_strDoctorName = m_ctlPatient.m_objPatient.m_strDOCTOR_VCHR;
            objApplyVO.m_strDoctorNO = m_ctlPatient.m_objPatient.m_strDOCTORNO_CHR;
            objApplyVO.m_strDoctorID = m_ctlPatient.m_objPatient.m_strDOCTORID_CHR;
            objApplyVO.m_strName = m_ctlPatient.m_objPatient.m_strPatientName;
            objApplyVO.m_strSex = m_ctlPatient.m_objPatient.m_strSex;
            objApplyVO.m_strTel = m_ctlPatient.m_objPatient.m_strHOMEPHONE_VCHR;
            objApplyVO.m_objAttachRelation.m_intSysFrom = 2;
            objApplyVO.m_strDeptID = m_ctlPatient.m_objPatient.m_strDeptID;
            objApplyVO.m_strArea = m_ctlPatient.m_objPatient.m_strAreaName;
            objApplyVO.m_strAreaID = m_ctlPatient.m_objPatient.m_strAreaID;
            objApplyVO.m_intSubmitted = 1;

        }

        /// <summary>
        /// ͬ�����˴Ӵ�λ����ת��ʱ�Ĳ�������λ����ͬ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void m_ctlPatient_m_evtPatientFromBedAdmin(object sender, EventArgs e)
        {
            clsBIHPatientInfo objPatient = m_ctlPatient.m_objPatient;
            // )����
            if (objPatient != null)
            {
                clsBIHArea clsArea = new clsBIHArea();
                clsArea.m_strAreaID = objPatient.m_strAreaID;
                clsArea.m_strAreaName = objPatient.m_strAreaName;

                clsBIHBed clsBed = new clsBIHBed();
                clsBed.m_strBedID = objPatient.m_strBedID;
                clsBed.m_strBedName = objPatient.m_strBedName;

                m_ctlOrderDetail.m_txtArea.Tag = clsArea;
                m_ctlOrderDetail.m_txtArea.Text = clsArea.m_strAreaName;

                m_ctlOrderDetail.m_txtBedNo.Tag = clsBed;
                m_ctlOrderDetail.m_txtBedNo.Text = clsBed.m_strBedName;
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// �޸�ҽ����ʼʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem4_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                ArrayList m_arrOrder = new ArrayList();//���޸Ĳ��ε�����
                for (int i = 0; i < this.m_dtvOrder.SelectedRows.Count; i++)
                {
                    clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[i].Tag;
                    if (!m_blUsePowerHander(order))
                    {
                        continue;
                    }
                    if (order.m_intStatus == 0 || order.m_intStatus == 1 || order.m_intStatus == 5 || order.m_intStatus == 7)
                    {
                        m_arrOrder.Add(order);
                    }
                }
                if (m_arrOrder.Count > 0)
                {
                    frmSelectBox selectBox = new frmSelectBox(((clsBIHOrder)m_arrOrder[0]), 0);
                    if (selectBox.ShowDialog() == DialogResult.Yes)
                    {

                        DateTime m_dtPostdate = selectBox.m_datValue;
                        for (int i = 0; i < m_arrOrder.Count; i++)
                        {
                            if (((clsBIHOrder)m_arrOrder[i]).m_dtFinishDate > DateTime.MinValue && m_dtPostdate > ((clsBIHOrder)m_arrOrder[i]).m_dtFinishDate)
                            {
                                MessageBox.Show("����ʱ�䲻�ܴ���ͣ��ʱ��!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        for (int i = 0; i < m_arrOrder.Count; i++)
                        {
                            ((clsBIHOrder)m_arrOrder[i]).m_dtPostdate = m_dtPostdate;
                        }
                        //clsBIHORDERCHARGEDService m_objManager = new clsDcl_GetSvcObject().m_GetBIHORDERCHARGEDSvc();
                        long lngRef = (new weCare.Proxy.ProxyIP()).Service.m_lngUpdateOrderBeginDate((clsBIHOrder[])(m_arrOrder.ToArray(typeof(clsBIHOrder))));
                        if (lngRef > 0)
                        {
                            MessageBox.Show("���ĳɹ�!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            for (int k = 0; k < m_arrOrder.Count; k++)
                            {
                                ArrayList m_arrOrders = GetTheSameNoOrders(((clsBIHOrder)m_arrOrder[k]).m_intRecipenNo);
                                for (int i = 0; i < m_arrOrders.Count; i++)
                                {

                                    clsBIHOrder m_objOrder = (clsBIHOrder)m_arrOrders[i];
                                    m_objOrder.m_dtPostdate = m_dtPostdate;
                                    DataGridViewRow row = GetTheGridRowByOrder(m_objOrder.m_strOrderID);
                                    this.m_objDomain.m_objGetDataViewRow(m_objOrder, row, row.Index + 1);
                                    if (m_intStarClickCout % 2 == 0)//��ʾ��
                                    {
                                        row.Cells["m_dtPOSTDATE_DAT"].Value = DateTimeToShortDateString(m_objOrder.m_dtPostdate);
                                    }
                                    else//����ʾ��
                                    {
                                        row.Cells["m_dtPOSTDATE_DAT"].Value = DateTimeToCutYearDateString(m_objOrder.m_dtPostdate);
                                    }
                                }
                            }
                            //ˢ��ͬ��ҽ���ķ�����ɫ
                            this.m_objDomain.m_mthRefreshSameReqNoColor();
                            /*<======================*/
                        }
                    }
                }
                else
                {
                    MessageBox.Show("û�п��޸�����ʱ���ҽ��!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        public string DateTimeToString(DateTime dtValue)
        {
            if (dtValue.Date == DateTime.MinValue)
                return "";
            else
                return dtValue.ToString("yyyy-MM-dd HH:mm");
        }

        public string DateTimeToShortDateString(DateTime dtValue)
        {
            if (dtValue.Date == DateTime.MinValue)
                return "";
            else
                return dtValue.ToString("yy-MM-dd HH:mm");
        }

        public string DateTimeToCutYearDateString(DateTime dtValue)
        {
            if (dtValue.Date == DateTime.MinValue)
                return "";
            else
                return dtValue.ToString("MM-dd HH:mm");
        }

        /// <summary>
        /// �޸�ͣ��ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem5_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                ArrayList m_arrOrder = new ArrayList();//���޸Ĳ��ε�����

                for (int i = 0; i < this.m_dtvOrder.SelectedRows.Count; i++)
                {
                    clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[i].Tag;
                    if (!m_blUsePowerHander(order))
                    {
                        continue;
                    }
                    if (order.m_intStatus == 0 || order.m_intStatus == 1 || order.m_intStatus == 5 || order.m_intStatus == 7 || (order.m_intExecuteType == 1 && order.m_intStatus == 2))
                    {
                        m_arrOrder.Add(order);
                    }
                }
                if (m_arrOrder.Count > 0)
                {
                    frmSelectBox selectBox = new frmSelectBox(((clsBIHOrder)m_arrOrder[0]), 1);
                    if (selectBox.ShowDialog() == DialogResult.Yes)
                    {

                        DateTime m_dtFinishDate = selectBox.m_datValue;
                        for (int i = 0; i < m_arrOrder.Count; i++)
                        {
                            if (m_dtFinishDate < ((clsBIHOrder)m_arrOrder[i]).m_dtStartDate && ((clsBIHOrder)m_arrOrder[i]).m_intStatus != 2)
                            {
                                MessageBox.Show("����ʱ�䲻�����ڿ�ʼʱ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else if (m_dtFinishDate < ((clsBIHOrder)m_arrOrder[i]).m_dtExecutedate && ((clsBIHOrder)m_arrOrder[i]).m_intStatus == 2)
                            {
                                MessageBox.Show("��ִ�е�ҽ��������ʱ�䲻������ִ�е�ʱ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        for (int i = 0; i < m_arrOrder.Count; i++)
                        {

                            ((clsBIHOrder)m_arrOrder[i]).m_dtFinishDate = m_dtFinishDate;
                        }
                        //clsBIHORDERCHARGEDService m_objManager = new clsDcl_GetSvcObject().m_GetBIHORDERCHARGEDSvc();
                        long lngRef = (new weCare.Proxy.ProxyIP()).Service.m_lngUpdateOrderEndDate((clsBIHOrder[])(m_arrOrder.ToArray(typeof(clsBIHOrder))));
                        if (lngRef > 0)
                        {
                            MessageBox.Show("���ĳɹ�!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            for (int k = 0; k < m_arrOrder.Count; k++)
                            {
                                ArrayList m_arrOrders = GetTheSameNoOrders(((clsBIHOrder)m_arrOrder[k]).m_intRecipenNo);
                                for (int i = 0; i < m_arrOrders.Count; i++)
                                {

                                    clsBIHOrder m_objOrder = (clsBIHOrder)m_arrOrders[i];
                                    m_objOrder.m_dtFinishDate = m_dtFinishDate;
                                    DataGridViewRow row = GetTheGridRowByOrder(m_objOrder.m_strOrderID);
                                    this.m_objDomain.m_objGetDataViewRow(m_objOrder, row, row.Index + 1);
                                    if (m_intStarClickCout % 2 == 0)//��ʾ��
                                    {
                                        row.Cells["dtv_FinishDate"].Value = DateTimeToShortDateString(m_objOrder.m_dtFinishDate);
                                    }
                                    else//����ʾ��
                                    {
                                        row.Cells["dtv_FinishDate"].Value = DateTimeToCutYearDateString(m_objOrder.m_dtFinishDate);
                                    }

                                }
                            }
                            //ˢ��ͬ��ҽ���ķ�����ɫ
                            this.m_objDomain.m_mthRefreshSameReqNoColor();
                            /*<======================*/
                        }
                    }
                }
                else
                {
                    MessageBox.Show("û�п��޸�ͣ��ʱ���ҽ��!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void menuItemStopAll_Click(object sender, EventArgs e)
        {
            if (this.m_ctlPatient.m_objPatient != null)
            {
                int m_intCount = 0;
                m_objDomain.m_lngGetNotExecuteOrderByRegID(this.m_ctlPatient.m_objPatient.m_strRegisterID, out m_intCount);
                if (m_intCount > 0)
                {
                    MessageBox.Show("��ǰ������δִ�е�ҽ�����봦�����ͣ����ҽ��?", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("ȷ��ֹͣ����ҽ�������ѣ��ò�������ҽ�����ᱻֹͣ����?", "��ʾ��!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (m_objDomain.m_lngStopAllOrderByRegID(this.m_ctlPatient.m_objPatient.m_strRegisterID, this.LoginInfo.m_strEmpID, this.LoginInfo.m_strEmpName))
                    {
                        this.m_objDomain.m_mthLoadOrderList();
                    }
                }
            }

        }

        private void m_MenuCopy_Click(object sender, EventArgs e)
        {
            //if (!this.m_dtvOrder.Focus())
            //{
            //    return;
            //}
            if (fo.frmParent == null)
            {
                fo.frmParent = (frmBIHOrderInput)panel4.Parent;
            }
            List<clsBIHOrder> m_arrOrder = GetTheSelectItemWithSon();
            if (m_arrOrder.Count > 0)
            {
                //���ҽ���Ƿ�ͣ��
                ArrayList m_arrOrderIDs = new ArrayList();
                for (int i = 0; i < m_arrOrder.Count; i++)
                {
                    if (!m_arrOrderIDs.Contains((m_arrOrder[i]).m_strOrderID))
                    {
                        m_arrOrderIDs.Add((m_arrOrder[i]).m_strOrderID);
                    }
                }
                ArrayList m_strOrders = GetTheStopOrders(m_arrOrderIDs);
                if (m_strOrders != null && m_arrOrder.Count > 0)
                {
                    string m_strErrMessage = "";
                    for (int i = 0; i < m_arrOrder.Count; i++)
                    {
                        clsBIHOrder order = m_arrOrder[i];
                        if (m_strOrders.Contains(order.m_strOrderID))
                        {
                            m_strErrMessage += order.m_strName + "\r\n";
                        }
                    }
                    if (!m_strErrMessage.Trim().Equals(""))
                    {
                        m_strErrMessage = "����ҽ��������շ���Ŀͣ�û�ҩƷͣҩ,���ܽ��и���!" + "\r\n" + m_strErrMessage;
                        MessageBox.Show(m_strErrMessage, "ҽ������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }
                /*<==================================*/
                fo.m_lngAddOrderGroupTempList(m_arrOrder);
            }
        }

        /// <summary>
        /// ���ص�ǰͣ�û�ͣҩ��ҽ����ˮ����
        /// </summary>
        /// <param name="m_arrOrderIDs"></param>
        /// <returns></returns>
        private ArrayList GetTheStopOrders(ArrayList m_arrOrderIDs)
        {
            ArrayList m_arrStopOrderIds = new ArrayList();
            if (m_arrOrderIDs.Count <= 0)
            {
                return m_arrStopOrderIds;
            }
            string[] m_strOrders = (string[])m_arrOrderIDs.ToArray(typeof(string));
            DataTable m_dtOrderSign = null;
            m_objDomain.m_lngGetOrderStopSign(m_strOrders, out m_dtOrderSign);
            if (m_dtOrderSign != null)
            {
                string orderid_chr = "";
                string STATUS_INT = "";//(������Ŀ״̬ 0-ͣ�� 1-����)
                string IFSTOP_INT = "";//ͣ�ñ�־ 1-ͣ�� 0-����
                string ITEMSRCTYPE_INT = "";//��Ŀ��Դ����1��ҩƷ��
                string IPNOQTYFLAG_INT = "";//����ҩ��ȱҩ��־ 0-��ҩ 1��ȱҩ
                bool m_blStop = false;
                for (int i = 0; i < m_dtOrderSign.Rows.Count; i++)
                {
                    m_blStop = false;
                    DataRow row = m_dtOrderSign.Rows[i];
                    orderid_chr = row["orderid_chr"].ToString().Trim();
                    STATUS_INT = row["STATUS_INT"].ToString().Trim();//(������Ŀ״̬ 0-ͣ�� 1-����)
                    IFSTOP_INT = row["IFSTOP_INT"].ToString().Trim();//ͣ�ñ�־ 1-ͣ�� 0-����
                    ITEMSRCTYPE_INT = row["ITEMSRCTYPE_INT"].ToString().Trim();//��Ŀ��Դ����1��ҩƷ��
                    IPNOQTYFLAG_INT = row["IPNOQTYFLAG_INT"].ToString().Trim();//����ҩ��ȱҩ��־ 0-��ҩ 1��ȱҩ
                    if ((STATUS_INT.Equals("0") || IFSTOP_INT.Equals("1")))
                    {
                        if (!m_blStopControl)
                        {
                            m_blStop = true;
                        }
                    }

                    if (!m_blDeableMedControl)
                    {
                        if (ITEMSRCTYPE_INT.Equals("1") && IPNOQTYFLAG_INT.Equals("1"))
                        {
                            m_blStop = true;
                        }
                    }


                    if (m_blStop)
                    {
                        if (!m_arrStopOrderIds.Contains(orderid_chr))
                        {
                            m_arrStopOrderIds.Add(orderid_chr);
                        }
                    }

                }

            }
            return m_arrStopOrderIds;

        }



        private void m_MenuPase_Click(object sender, EventArgs e)
        {
            if (!this.m_dtvOrder.Focus())
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            fo.m_cmdParseOrder();
            this.Cursor = Cursors.Default;
        }

        private void m_MenuATTACHTIMES_INT_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                ArrayList m_arrOrder = new ArrayList();//���޸Ĳ��ε�����

                for (int i = 0; i < this.m_dtvOrder.SelectedRows.Count; i++)
                {
                    //clsBIHOrder order = GetTheFaterOrder(((clsBIHOrder)m_dtvOrder.SelectedRows[i].Tag).m_intRecipenNo);

                    //  // clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[i].Tag;
                    //   if (!m_blUsePowerHander(order))
                    //   {
                    //       continue;
                    //   }
                    //   p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[order.m_strOrderDicCateID];
                    //   if (m_objOrder.m_intExecuteType == 1 && p_objItem != null && p_objItem.m_intAPPENDVIEWTYPE_INT == 1)

                    //   if (order.m_intExecuteType==1&&(order.m_intStatus == 0 || order.m_intStatus == 1 || order.m_intStatus == 5))
                    //   {
                    //       m_arrOrder.Add(order);
                    //   }
                    clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[i].Tag;
                    if (m_getCanAPPENDVIEWTYPE(order) == true)
                    {
                        m_arrOrder.Add(order);
                    }
                }
                if (m_arrOrder.Count > 0)
                {

                    frmSelectBox selectBox = new frmSelectBox(((clsBIHOrder)m_arrOrder[0]).m_intATTACHTIMES_INT, 4);
                    if (selectBox.ShowDialog() == DialogResult.Yes)
                    {

                        int m_intATTACHTIMES_INT = selectBox.m_intATTACHTIMES_INT;
                        for (int i = 0; i < m_arrOrder.Count; i++)
                        {
                            ((clsBIHOrder)m_arrOrder[i]).m_intATTACHTIMES_INT = m_intATTACHTIMES_INT;
                        }
                        //clsBIHORDERCHARGEDService m_objManager = clsGenerator.CreateObject(typeof(clsBIHORDERCHARGEDService)) as clsBIHORDERCHARGEDService;
                        //clsBIHORDERCHARGEDService m_objManager = new clsDcl_GetSvcObject().m_GetBIHORDERCHARGEDSvc();
                        long lngRef = (new weCare.Proxy.ProxyIP()).Service.m_lngUpdateOrderATTACHTIMES((clsBIHOrder[])(m_arrOrder.ToArray(typeof(clsBIHOrder))));
                        if (lngRef > 0)
                        {
                            MessageBox.Show("���ĳɹ�!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            for (int k = 0; k < m_arrOrder.Count; k++)
                            {
                                ArrayList m_arrOrders = GetTheSameNoOrders(((clsBIHOrder)m_arrOrder[k]).m_intRecipenNo);
                                for (int i = 0; i < m_arrOrders.Count; i++)
                                {

                                    clsBIHOrder m_objOrder = (clsBIHOrder)m_arrOrders[i];
                                    m_objOrder.m_intATTACHTIMES_INT = m_intATTACHTIMES_INT;
                                    DataGridViewRow row = GetTheGridRowByOrder(m_objOrder.m_strOrderID);
                                    this.m_objDomain.m_objGetDataViewRow(m_objOrder, row, row.Index + 1);

                                }
                            }
                            //ˢ��ͬ��ҽ���ķ�����ɫ
                            this.m_objDomain.m_mthRefreshSameReqNoColor();
                            /*<======================*/
                        }
                        else if (lngRef == -10)
                        {
                            MessageBox.Show(this, "��ҽ���Ѿ��˶�ת���������޸Ĳ��ε�������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.cmdRefurbish_Click(null, null);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("û�пɲ��ε�ҽ��!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        /// <summary>
        /// ���ݷ��ŷ���ͬ��ҽ������
        /// </summary>
        /// <param name="m_intRecipenNo">����</param>
        /// <returns></returns>
        private ArrayList GetTheSameNoOrders(int m_intRecipenNo)
        {
            ArrayList m_arrOrders = new ArrayList();
            for (int i = 0; i < this.m_dtvOrder.Rows.Count; i++)
            {
                clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.Rows[i].Tag;
                if (order.m_intRecipenNo == m_intRecipenNo)
                {
                    m_arrOrders.Add(order);
                }
            }
            return m_arrOrders;
        }



        private void m_MenuOPERATION_Click(object sender, EventArgs e)
        {
            if (this.m_ctlPatient.m_objPatient != null)
            {
                int m_intCount = 0;
                m_objDomain.m_lngGetNotExecuteOrderByRegID(this.m_ctlPatient.m_objPatient.m_strRegisterID, out m_intCount);
                if (m_intCount > 0)
                {
                    MessageBox.Show("��ǰ������δִ�е�ҽ�����봦������������ҽ��?", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("��ǰ����������һ������ҽ��,��ֹͣ�ò������е�ҽ��,ȷ��ִ��?", "��ʾ��!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    clsBIHOrder order = new clsBIHOrder();
                    //Ϊ��ǰҽ�����ϸ��ӵ���Ϣ
                    order.m_intExecuteType = 1;
                    order.m_strName = "����ҽ��";
                    order.m_intTYPE_INT = 1;
                    SetTheCurrentOrder(order);

                    if (m_objDomain.m_lngInsertOPERATIONOrder(order, this.LoginInfo.m_strEmpID, this.LoginInfo.m_strEmpName) > 0)
                    {
                        this.m_objDomain.m_mthLoadOrderList();
                    }

                }
            }

        }


        private void m_dtvOrder_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (this.m_dtvOrder.Columns[e.ColumnIndex].Name.Trim().Equals("m_dtPOSTDATE_DAT"))
            {
                m_intStarClickCout++;
                if (m_intStarClickCout % 2 == 0)//��ʾ��
                {
                    for (int i = 0; i < m_dtvOrder.RowCount; i++)
                    {
                        clsBIHOrder order = (clsBIHOrder)m_dtvOrder.Rows[i].Tag;
                        if (this.m_dtvOrder.Rows[i].Cells["m_dtPOSTDATE_DAT"].Value != null && !this.m_dtvOrder.Rows[i].Cells["m_dtPOSTDATE_DAT"].Value.ToString().Trim().Equals(""))
                        {
                            this.m_dtvOrder.Rows[i].Cells["m_dtPOSTDATE_DAT"].Value = this.DateTimeToShortDateString(order.m_dtPostdate);
                        }
                    }
                }
                else//����ʾ��
                {
                    for (int i = 0; i < m_dtvOrder.RowCount; i++)
                    {
                        clsBIHOrder order = (clsBIHOrder)m_dtvOrder.Rows[i].Tag;
                        if (this.m_dtvOrder.Rows[i].Cells["m_dtPOSTDATE_DAT"].Value != null && !this.m_dtvOrder.Rows[i].Cells["m_dtPOSTDATE_DAT"].Value.ToString().Trim().Equals(""))
                        {
                            this.m_dtvOrder.Rows[i].Cells["m_dtPOSTDATE_DAT"].Value = this.DateTimeToCutYearDateString(order.m_dtPostdate);
                        }
                    }
                }
            }
            else if (this.m_dtvOrder.Columns[e.ColumnIndex].Name.Trim().Equals("dtv_FinishDate"))
            {

                m_intFinishClickCout++;
                if (m_intFinishClickCout % 2 == 0)//��ʾ��
                {
                    for (int i = 0; i < m_dtvOrder.RowCount; i++)
                    {
                        clsBIHOrder order = (clsBIHOrder)m_dtvOrder.Rows[i].Tag;
                        if (this.m_dtvOrder.Rows[i].Cells["dtv_FinishDate"].Value != null && !this.m_dtvOrder.Rows[i].Cells["dtv_FinishDate"].Value.ToString().Trim().Equals(""))
                        {
                            this.m_dtvOrder.Rows[i].Cells["dtv_FinishDate"].Value = this.DateTimeToShortDateString(order.m_dtFinishDate);
                        }
                    }
                }
                else//����ʾ��
                {
                    for (int i = 0; i < m_dtvOrder.RowCount; i++)
                    {
                        clsBIHOrder order = (clsBIHOrder)m_dtvOrder.Rows[i].Tag;
                        if (this.m_dtvOrder.Rows[i].Cells["dtv_FinishDate"].Value != null && !this.m_dtvOrder.Rows[i].Cells["dtv_FinishDate"].Value.ToString().Trim().Equals(""))
                        {
                            this.m_dtvOrder.Rows[i].Cells["dtv_FinishDate"].Value = this.DateTimeToCutYearDateString(order.m_dtFinishDate);
                        }
                    }
                }
            }
        }

        private void m_MenuOrderTemp_Click(object sender, EventArgs e)
        {
            //��������
            CopySelectItemtoGroup();
        }

        private void m_MenuOrderNornal_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            clsComuseorderdic m_arrOrderdic = null;
            ArrayList OrderdicList = new ArrayList();
            for (int i = 0; i < this.m_dtvOrder.SelectedRows.Count; i++)
            {

                //ҽ������Ϊ����ҽ���Ĳ���������
                clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[i].Tag;
                if (!CanCopyToGroup(order))
                {
                    continue;
                }
                /*<==============================*/
                m_arrOrderdic = new clsComuseorderdic();
                m_arrOrderdic.m_strSEQID_CHR = "";
                m_arrOrderdic.m_intPRIVILEGE_INT = 0;
                m_arrOrderdic.m_intTYPE_INT = 0;
                m_arrOrderdic.m_strDEPTID_CHR = this.LoginInfo.m_strInpatientAreaID;
                m_arrOrderdic.m_strORDERDICID_CHR = order.m_strOrderDicID;
                m_arrOrderdic.m_strCREATERID_CHR = this.LoginInfo.m_strEmpID;
                //ҽ������
                clsT_aid_bih_ordercate_VO p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[order.m_strOrderDicCateID];
                if (p_objItem != null && p_objItem.m_strVIEWNAME_VCHR.Trim().Equals("����ҽ��"))
                {
                    m_arrOrderdic.m_strDES_VCHR = order.m_strName;
                }
                if (!m_arrOrderdic.m_strORDERDICID_CHR.ToString().Trim().Equals(""))
                {
                    OrderdicList.Add(m_arrOrderdic);
                }

            }
            if (OrderdicList.Count > 0)
            {
                long m_lngRef = m_objDomain.m_cmdAddOrderNornal((clsComuseorderdic[])(OrderdicList.ToArray(typeof(clsComuseorderdic))));
                if (m_lngRef > 0)
                {
                    MessageBox.Show("��ӳɹ�!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("û�к��ʵĴ������!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Cursor = Cursors.Default;
        }

        private void m_MenuChangeArea_Click(object sender, EventArgs e)
        {
            if (this.m_ctlPatient.m_objPatient != null)
            {
                int m_intCount = 0;
                m_objDomain.m_lngGetNotStopOrderByRegID(this.m_ctlPatient.m_objPatient.m_strRegisterID, out m_intCount);
                if (m_intCount > 0)
                {
                    MessageBox.Show("��ǰ������δͣҽ�����봦����ٲ���ת��ҽ��?", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("��ǰ����������һ��ת��ҽ��,ȷ��ִ��?", "��ʾ��!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    clsBIHOrder order = new clsBIHOrder();
                    //Ϊ��ǰҽ�����ϸ��ӵ���Ϣ
                    order.m_intExecuteType = 1;
                    order.m_strName = "ת��ҽ��";
                    order.m_intTYPE_INT = 2;
                    SetTheCurrentOrder(order);

                    if (m_objDomain.m_lngInsertChangeAreaOrder(order, this.LoginInfo.m_strEmpID, this.LoginInfo.m_strEmpName) > 0)
                    {
                        this.m_objDomain.m_mthLoadOrderList();
                    }

                }
            }
        }

        private void m_MenuReSortOrderNO_Click(object sender, EventArgs e)
        {
            if (this.m_ctlPatient.m_objPatient != null)
            {
                long ret = m_objDomain.m_lngReSortOrderNO(this.m_ctlPatient.m_objPatient.m_strRegisterID);
                if (ret > 0)
                {
                    this.m_objDomain.m_mthLoadOrderList();
                }
            }
        }

        private void m_MenuOrderSTsign_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag;
                if (order.m_intStatus == 0 || order.m_intStatus == 1 || order.m_intStatus == 5 || order.m_intStatus == 7)
                {
                    clsBIHOrder m_objOrder = GetTheFaterOrder(order.m_intRecipenNo);
                    if (m_objOrder != null)
                    {
                        m_objOrder.m_strREMARK_VCHR = "st " + m_objOrder.m_strREMARK_VCHR;
                        //clsBIHORDERCHARGEDService m_objManager = clsGenerator.CreateObject(typeof(clsBIHORDERCHARGEDService)) as clsBIHORDERCHARGEDService;
                        //clsBIHORDERCHARGEDService m_objManager = new clsDcl_GetSvcObject().m_GetBIHORDERCHARGEDSvc();
                        long lngRef = (new weCare.Proxy.ProxyIP()).Service.m_lngUpdateOrderREMARK_VCHR(m_objOrder);
                        if (lngRef > 0)
                        {
                            MessageBox.Show("�����ɹ�!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //˵��
                            DataGridViewRow row = GetTheGridRowByOrder(m_objOrder.m_strOrderID);
                            if (row != null)
                            {
                                row.Cells["dtv_REMARK"].Value = m_objOrder.m_strREMARK_VCHR;
                            }

                        }
                    }
                }
            }

        }

        /// <summary>
        /// ���ݵ�ǰҽ���Ż�����ڵ���
        /// </summary>
        /// <param name="m_strOrderID"></param>
        /// <returns></returns>
        public DataGridViewRow GetTheGridRowByOrder(string m_strOrderID)
        {
            DataGridViewRow row = null;
            for (int i = 0; i < this.m_dtvOrder.RowCount; i++)
            {
                if (((clsBIHOrder)this.m_dtvOrder.Rows[i].Tag).m_strOrderID.Equals(m_strOrderID))
                {
                    row = this.m_dtvOrder.Rows[i];
                }
            }
            return row;
        }



        /// <summary>
        /// ���ظ�ҽ����ͬ���£�����ͬ���£����ر�������ҽ��
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public clsBIHOrder GetTheFaterOrder(int m_intRecipenNo)
        {
            clsBIHOrder order = null;
            for (int i = 0; i < this.m_dtvOrder.RowCount; i++)
            {
                if (((clsBIHOrder)this.m_dtvOrder.Rows[i].Tag).m_intRecipenNo == m_intRecipenNo)
                {
                    order = (clsBIHOrder)this.m_dtvOrder.Rows[i].Tag;
                    break;
                }
            }
            return order;
        }

        private void m_MenuCheckBillEdit_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag;
                if (order.m_intStatus == 0 || order.m_intStatus == 1 || order.m_intStatus == 5 || order.m_intStatus == 7)
                {
                    //clsT_aid_bih_ordercate_VO p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[order.m_strOrderDicCateID];
                    //if (p_objItem.m_strVIEWNAME_VCHR.Trim().Equals("���"))
                    //{
                    clsEMR_HIS_CheckRequisition CheckRequest = new clsEMR_HIS_CheckRequisition(order.m_strRegisterID, order.m_strOrderID);
                    CheckRequest.m_mthShowCheckRequisitionForm();
                    // }
                }
            }
        }

        private void m_MenuCheckBillView_Click(object sender, EventArgs e)
        {
            int m_intTag = 0;
            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                //if (!((clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag).m_strOrderDicCateName.Trim().Equals("���"))
                //{
                //    m_intTag = 0;
                //}
                //else if (((clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag).m_intStatus != 0)
                //{
                //    m_intTag = 1;
                //}
                //else
                //{
                string m_strChargeDetail = "";
                clsBIHOrder order1 = (clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag;
                m_objDomain.m_DisPlayToolTipListView(order1, m_lsvToolTip);
                if (m_objDomain.m_htbToolTip.ContainsKey(order1.m_strOrderID.Trim()))
                {
                    ArrayList alItem = new ArrayList();
                    alItem = (m_objDomain.m_htbToolTip[order1.m_strOrderID.Trim()] as ArrayList);
                    if (alItem != null && alItem.Count > 0)
                    {
                        clsChargeForDisplay[] objItemArr = (clsChargeForDisplay[])(alItem.ToArray(typeof(clsChargeForDisplay)));
                        //��һ��ͳ�ƽ��
                        //�ϼƽ��
                        double m_dblSum = 0;
                        for (int i = 0; i < objItemArr.Length; i++)
                        {
                            if (!double.IsInfinity(objItemArr[i].m_dblMoney))
                                m_dblSum += objItemArr[i].m_dblMoney;

                            m_strChargeDetail += objItemArr[i].m_strName + " ����:" + objItemArr[i].m_dblPrice + " Ԫ " + objItemArr[i].m_dblDrawAmount + objItemArr[i].m_strUNIT_VCHR + " �ϼ�:" + objItemArr[i].m_dblMoney + " Ԫ";
                            m_strChargeDetail += "\r\n";
                            //if (objItemArr.Length > 0)
                            //{

                            //    ListViewItem item1 = new ListViewItem("");
                            //    item1.SubItems.Add("");
                            //    item1.SubItems.Add("");
                            //    item1.SubItems.Add("");
                            //    item1.SubItems.Add("�ϼ�:");
                            //    item1.SubItems.Add(m_dblSum.ToString("0.00"));
                            //    item1.ForeColor = Color.Red;
                            //    p_lsvToolTip.Items.Add(item1);
                            //}
                            /*<===============*/
                        }
                    }
                }
                clsEMR_HIS_CheckRequisition CheckRequest = new clsEMR_HIS_CheckRequisition(order1.m_strRegisterID, order1.m_strOrderID);
                clsEMR_HIS_CheckRequisitionValue CheckMessage = CheckRequest.m_objGetCheckRequisition();


                clsTestApplyItme_VO item_VO = new clsTestApplyItme_VO();

                clsDcl_InputOrder objInputOrder = new clsDcl_InputOrder();
                DataTable dt;
                string strTypeID = "";
                string strPartName = "";
                long l = objInputOrder.m_mthGetApplyTypeByID(order1.m_strCHARGEITEMID_CHR, out dt);
                if (l > 0 && dt.Rows.Count > 0)
                {
                    strTypeID = dt.Rows[0]["APPLY_TYPE_INT"].ToString().Trim();
                }
                // item_VO.m_decDiscount = 0;
                item_VO.m_decPrice = order1.m_dmlPrice;
                item_VO.m_decQty = order1.m_dmlUse;
                item_VO.m_decTolPrice = order1.m_dmlGet * order1.m_dmlPrice;
                item_VO.m_strItemID = order1.m_strCHARGEITEMID_CHR;
                item_VO.m_strItemName = order1.m_strCHARGEITEMNAME_CHR;
                item_VO.m_strSpec = order1.m_strSpec;
                item_VO.m_strUnit = order1.m_strUseunit;
                item_VO.m_strOutpatRecipeID = "";
                // item_VO.m_strRowNo = p_row.ToString();
                item_VO.m_strOprDeptID = "";
                com.digitalwave.GLS_WS.clsApplyForm objfrm = new com.digitalwave.GLS_WS.clsApplyForm();
                clsApplyRecord objApplyVO;
                m_mthFillApplyInfo(out objApplyVO);
                objApplyVO.m_strTypeID = strTypeID;
                objApplyVO.m_objChargeItem = item_VO;
                objApplyVO.m_intChargeStatus = 1;
                objApplyVO.m_strDiagnosePart = order1.m_strPARTNAME_VCHR;
                objApplyVO.m_objAttachRelation.m_strSourceItemID = order1.m_strOrderID;
                ///����ժҪ
                if (CheckMessage != null)
                {
                    objApplyVO.m_strSummary = CheckMessage.m_strCASESUMMARY_VCHR + "\r\n" + CheckMessage.m_strPHYSEXAM_VCHR;
                    objApplyVO.m_strDiagnose = CheckMessage.m_strADMISSIONDIAGNOSIS_VCHR;
                }
                objApplyVO.m_strDoctorID = order1.m_strCreatorID;
                objApplyVO.m_strDoctorName = order1.m_strCreator;
                objApplyVO.m_strBIHNO = m_ctlPatient.m_objPatient.m_strInHospitalNo;
                objApplyVO.m_strArea = m_ctlPatient.m_objPatient.m_strAreaName;
                objApplyVO.m_strAreaID = m_ctlPatient.m_objPatient.m_strAreaID;
                objApplyVO.m_strBedNO = m_ctlPatient.m_objPatient.m_strBedName;
                objApplyVO.m_strDeptID = m_ctlPatient.m_objPatient.m_strDeptID;
                objApplyVO.m_strDepartment = m_ctlPatient.m_objPatient.m_strDeptName;
                objfrm.mainForm.m_intFlag = 1;
                objfrm.mainForm.m_strFormTitle = "סԺ���뵥";
                objApplyVO.m_strChargeDetail = m_strChargeDetail;
                clsCheckType[] objCTArr = objfrm.OpenWithVO(objApplyVO);

                m_intTag = -1;


                //}

            }
            //switch (m_intTag)
            //{
            //    case 0:
            //        MessageBox.Show("����ѡ������Ŀ!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        break;
            //    case 1:
            //        MessageBox.Show("ֹ��Ŀ�ѿ����뵥!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        break;
            //}
        }

        private void m_MenuViewBackReasion_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[0].Tag;
                frmSelectBox selectBox = new frmSelectBox(order.m_strBACKREASON, 5);
                if (selectBox.ShowDialog() == DialogResult.Yes)
                {

                }

            }
        }

        private void m_imgBackAlert_Click(object sender, EventArgs e)
        {
            this.m_cboOrderList.SelectedIndex = 11;
            m_cboOrderList_SelectionChangeCommitted(null, null);
        }

        private void m_MenuOutToday_Click(object sender, EventArgs e)
        {
            if (this.m_ctlPatient.m_objPatient != null)
            {
                //int m_intCount = 0;
                //m_objDomain.m_lngGetNotExecuteOrderByRegID(this.m_ctlPatient.m_objPatient.m_strRegisterID, out m_intCount);
                //if (m_intCount > 0)
                //{
                //    MessageBox.Show("��ǰ������δִ�е�ҽ�����봦�������ӳ�Ժҽ��!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}


                clsBIHOrder order = new clsBIHOrder();
                //Ϊ��ǰҽ�����ϸ��ӵ���Ϣ
                order.m_intExecuteType = 1;
                order.m_strName = "�����Ժ";
                order.m_intTYPE_INT = 3;
                SetTheCurrentOrder(order);

                com.digitalwave.iCare.gui.HIS.frmDiagnoses frmDialog = new frmDiagnoses("", order.m_strRegisterID, 1);
                if (frmDialog.ShowDialog() == DialogResult.OK)
                {
                    bool m_blAuto = false;
                    if (MessageBox.Show("�����Ժҽ��ʱ,Ҫ�Զ�ֹͣ�ò������е���ִ�й��ĳ���ҽ����?", "��ʾ��!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        m_blAuto = true;
                    }

                    if (m_objDomain.m_lngInsertOutHisOrder(order, this.LoginInfo.m_strEmpID, this.LoginInfo.m_strEmpName, m_blAuto) > 0)
                    {
                        this.m_objDomain.m_mthLoadOrderList();
                    }
                }


            }
        }

        private void m_MenuOutTomorrow_Click(object sender, EventArgs e)
        {
            if (this.m_ctlPatient.m_objPatient != null)
            {
                //int m_intCount = 0;
                //m_objDomain.m_lngGetNotExecuteOrderByRegID(this.m_ctlPatient.m_objPatient.m_strRegisterID, out m_intCount);
                //if (m_intCount > 0)
                //{
                //    MessageBox.Show("��ǰ������δִ�е�ҽ�����봦�������ӳ�Ժҽ��!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}


                clsBIHOrder order = new clsBIHOrder();
                //Ϊ��ǰҽ�����ϸ��ӵ���Ϣ
                order.m_intExecuteType = 1;
                order.m_strName = "�����Ժ";
                order.m_intTYPE_INT = 4;
                SetTheCurrentOrder(order);

                com.digitalwave.iCare.gui.HIS.frmDiagnoses frmDialog = new frmDiagnoses("", order.m_strRegisterID, 1);
                if (frmDialog.ShowDialog() == DialogResult.OK)
                {
                    bool m_blAuto = false;
                    if (MessageBox.Show("�����Ժҽ��ʱ,Ҫ�Զ�ֹͣ�ò������е���ִ�й��ĳ���ҽ����?", "��ʾ��!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        m_blAuto = true;
                    }

                    if (m_objDomain.m_lngInsertOutHisOrder(order, this.LoginInfo.m_strEmpID, this.LoginInfo.m_strEmpName, m_blAuto) > 0)
                    {
                        this.m_objDomain.m_mthLoadOrderList();
                    }
                }

            }
        }

        private void m_MenuCHNAGEAMOUNT_INT_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                ArrayList m_arrOrder = new ArrayList();//���޸Ĳ��ε�����

                for (int i = 0; i < this.m_dtvOrder.SelectedRows.Count; i++)
                {
                    clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[i].Tag;
                    if (!m_blUsePowerHander(order))
                    {
                        continue;
                    }

                    if (order.m_intStatus == 0 || order.m_intStatus == 1 || order.m_intStatus == 5 || order.m_intStatus == 7)
                    {
                        m_arrOrder.Add(order);
                    }
                }
                if (m_arrOrder.Count > 0)
                {

                    frmSelectBox selectBox = new frmSelectBox(int.Parse(((clsBIHOrder)m_arrOrder[0]).m_dmlGet.ToString()), 6);
                    if (selectBox.ShowDialog() == DialogResult.Yes)
                    {

                        int m_intAMOUNT = selectBox.m_intATTACHTIMES_INT;
                        for (int i = 0; i < m_arrOrder.Count; i++)
                        {
                            ((clsBIHOrder)m_arrOrder[i]).m_dmlGet = decimal.Parse(m_intAMOUNT.ToString());
                        }
                        //clsBIHORDERCHARGEDService m_objManager = clsGenerator.CreateObject(typeof(clsBIHORDERCHARGEDService)) as clsBIHORDERCHARGEDService;
                        //clsBIHORDERCHARGEDService m_objManager = new clsDcl_GetSvcObject().m_GetBIHORDERCHARGEDSvc();
                        long lngRef = (new weCare.Proxy.ProxyIP()).Service.m_lngUpdateOrderAmount((clsBIHOrder[])(m_arrOrder.ToArray(typeof(clsBIHOrder))));
                        if (lngRef > 0)
                        {
                            MessageBox.Show("���ĳɹ�!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ArrayList m_arrChangeOrderID = new ArrayList();
                            for (int k = 0; k < m_arrOrder.Count; k++)
                            {
                                ArrayList m_arrOrders = GetTheSameNoOrders(((clsBIHOrder)m_arrOrder[k]).m_intRecipenNo);
                                for (int i = 0; i < m_arrOrders.Count; i++)
                                {

                                    clsBIHOrder m_objOrder = (clsBIHOrder)m_arrOrders[i];
                                    //m_objOrder.m_intATTACHTIMES_INT = m_intATTACHTIMES_INT;
                                    DataGridViewRow row = GetTheGridRowByOrder(m_objOrder.m_strOrderID);
                                    this.m_objDomain.m_objGetDataViewRow(m_objOrder, row, row.Index + 1);
                                    m_arrChangeOrderID.Add(m_objOrder.m_strOrderID);
                                }
                            }
                            //ˢ��ͬ��ҽ���ķ�����ɫ
                            this.m_objDomain.m_mthRefreshSameReqNoColor();
                            /*<======================*/
                            //ˢ��ҽ����صķ�����Ϣ
                            if (m_arrChangeOrderID.Count > 0)
                            {
                                this.m_objDomain.lngRefreshChargePool(m_arrChangeOrderID);
                            }
                        }
                        else if (lngRef == -10)
                        {
                            MessageBox.Show(this, "��ҽ���Ѿ�ִ�У������޸���������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.cmdRefurbish_Click(null, null);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("û�п��޸�������ҽ��!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void m_MenuMoneyCount_Click(object sender, EventArgs e)
        {
            //if (this.m_dtvOrder.SelectedRows.Count <= 0)
            //{
            //    MessageBox.Show("����ѡ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //m_objDomain.m_mthDeleteCurrentOrder(true,(clsBIHOrder) this.m_dtvOrder.SelectedRows[0].Tag);		
            m_cmdMoneyCount_Click(null, null);
        }

        private void m_MenuSeeBill_Click(object sender, EventArgs e)
        {
            if (m_ctlPatient.m_objPatient != null)
            {
                string m_strPATIENTCARDID_CHR = m_ctlPatient.m_objPatient.m_strPATIENTCARDID_CHR;
                if (!m_strPATIENTCARDID_CHR.Trim().Equals(""))
                {
                    frmShowReports frm = new frmShowReports(m_strPATIENTCARDID_CHR);
                    frm.ShowDialog();
                }
            }
        }

        private void m_txtCREATEAREA_DoubleClick(object sender, EventArgs e)
        {
            m_txtCREATEAREA.Text = "";
            SendKeys.Send("{Enter}");

        }

        private void m_pcBoxAlert_Click(object sender, EventArgs e)
        {
            if (m_ctlPatient.m_objPatient == null)
            {
                return;
            }
            frmStopOrderConfirm m_frmAlert = new frmStopOrderConfirm(m_ctlPatient.m_objPatient.m_strRegisterID);
            m_frmAlert.m_htOrderCate = this.m_htOrderCate;
            m_frmAlert.m_objSpecateVo = this.m_objSpecateVo;
            m_frmAlert.m_blStopControl = this.m_blStopControl;
            m_frmAlert.m_blDeableMedControl = this.m_blDeableMedControl;
            m_frmAlert.ShowDialog();
            cmdRefurbish_Click(null, null);
        }

        private void m_MenuSurgery_Click(object sender, EventArgs e)
        {
            Assembly assem = Assembly.LoadFrom(Application.StartupPath + @"\ANA_Requisition.dll");
            object obj = assem.CreateInstance("iCare.Anaesthesia.Requisition.frmRequisition", true);
            Type typ = obj.GetType();

            MethodInfo med = typ.GetMethod("Show", new Type[0]);


            med.Invoke(obj, null);
            if (m_ctlPatient.m_objPatient != null)
            {

                FieldInfo fld = typ.GetField("m_ctlAppDept", BindingFlags.NonPublic | BindingFlags.Instance);
                object objbutton = fld.GetValue(obj);


                PropertyInfo propInfo = typ.GetProperty("M_strAppDeptID");

                propInfo.SetValue(obj, m_ctlPatient.m_objPatient.m_strAreaID, null);
            }
        }

        private void tsmiDrugInfo_Click(object sender, EventArgs e)
        {
            if (this.m_dtvOrder.SelectedRows.Count > 0)
            {
                for (int i = 0; i < this.m_dtvOrder.SelectedRows.Count; i++)
                {
                    clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.SelectedRows[i].Tag;
                    m_objDomain.GetDrugInfo(order.m_strOrderDicID);
                    return;
                }
            }
        }

        private void tsbOpApplyNew_Click(object sender, EventArgs e)
        {
            if (m_ctlPatient.m_objPatient == null || string.IsNullOrEmpty(m_ctlPatient.m_objPatient.m_strRegisterID)) return;
            frmOpsApply frm = new frmOpsApply(m_ctlPatient.m_objPatient);
            frm.ShowDialog();
        }

        #region ���⿹��ҩ����

        private void tsmiTskjyhz_apply_Click(object sender, EventArgs e)
        {
            if (m_ctlPatient.m_objPatient == null || string.IsNullOrEmpty(m_ctlPatient.m_objPatient.m_strRegisterID)) return;
            frmAntiApply frm = new frmAntiApply(m_ctlPatient.m_objPatient, 0);
            frm.ShowDialog();
        }

        private void tsmiTskjyhz_check_Click(object sender, EventArgs e)
        {
            if (m_ctlPatient.m_objPatient == null || string.IsNullOrEmpty(m_ctlPatient.m_objPatient.m_strRegisterID)) return;
            frmAntiApply frm = new frmAntiApply(m_ctlPatient.m_objPatient, 1);
            frm.ShowDialog();
        }

        private void tsmiTskjyhz_response_Click(object sender, EventArgs e)
        {
            if (m_ctlPatient.m_objPatient == null || string.IsNullOrEmpty(m_ctlPatient.m_objPatient.m_strRegisterID)) return;
            frmAntiConsultation frm = new frmAntiConsultation(m_ctlPatient.m_objPatient);
            frm.ShowDialog();
        }

        #endregion

        #region InputOrder �¼�

        private void m_ctlOrderDetail_evtInputOrder(object sender, EventArgs e)
        {
            int recipeNo = 0;
            try
            {
                recipeNo = Int32.Parse(m_ctlOrderDetail.m_txtRecipeNo.Text);
            }
            catch
            {
                recipeNo = 0;

            }
            // ���ʹ����ж�
            bool isOk = false;
            for (int i = 0; i < this.m_dtvOrder.RowCount; i++)
            {
                clsBIHOrder order = (clsBIHOrder)this.m_dtvOrder.Rows[i].Tag;
                if (order.m_intRecipenNo == recipeNo)
                {
                    m_ctlOrderDetail.cboProxyBoil.SelectedIndex = order.IsProxyBoilMed;
                    isOk = true;
                    break;
                }
            }
            if (isOk == false)
            {
                m_ctlOrderDetail.cboProxyBoil.SelectedIndex = (m_ctlOrderDetail.m_cboExecuteType.Text.Contains("��ҩ") ? 0 : 1);
            }
        }
        #endregion

        #region �������뵥
        /// <summary>
        /// �������뵥
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiApp_Click(object sender, EventArgs e)
        {
            if (m_ctlPatient.m_objPatient != null)
            {
                string patientId = m_ctlPatient.m_objPatient.m_strPatientID;

                clsBIHPatientInfo patVo = m_ctlPatient.m_objPatient;
                DataTable dt = null;
                //com.digitalwave.iCare.middletier.HIS.clsHisBase svc = (com.digitalwave.iCare.middletier.HIS.clsHisBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisBase));
                (new weCare.Proxy.ProxyHisBase()).Service.m_mthGetPatientInfoByCardID(patientId, out dt, true);
                //svc = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    string request = string.Empty;
                    request += "<request>" + Environment.NewLine;
                    request += string.Format("<sourceId>{0}</sourceId>", "2") + Environment.NewLine;
                    request += string.Format("<registerId>{0}</registerId>", patVo.m_strRegisterID) + Environment.NewLine;
                    request += string.Format("<patientId>{0}</patientId>", patVo.m_strPatientID) + Environment.NewLine;
                    request += string.Format("<patientName>{0}</patientName>", patVo.m_strPatientName) + Environment.NewLine;
                    request += string.Format("<sex>{0}</sex>", patVo.m_strSex) + Environment.NewLine;
                    request += string.Format("<birthday>{0}</birthday>", patVo.m_dtBorn.ToString("yyyy-MM-dd")) + Environment.NewLine;
                    request += string.Format("<cardNo>{0}</cardNo>", patVo.m_strPATIENTCARDID_CHR) + Environment.NewLine;
                    request += string.Format("<ipNo>{0}</ipNo>", patVo.m_strInHospitalNo) + Environment.NewLine;
                    request += string.Format("<bedNo>{0}</bedNo>", patVo.m_strBedName) + Environment.NewLine;
                    request += string.Format("<homeTel>{0}</homeTel>", patVo.m_strHOMEPHONE_VCHR) + Environment.NewLine;
                    request += string.Format("<homeAddr>{0}</homeAddr>", dr["homeaddress_vchr"].ToString()) + Environment.NewLine;
                    request += string.Format("<marriage>{0}</marriage>", dr["married_chr"].ToString()) + Environment.NewLine;
                    request += string.Format("<occupation>{0}</occupation>", "") + Environment.NewLine;
                    request += string.Format("<nativeplace>{0}</nativeplace>", dr["nativeplace_vchr"].ToString()) + Environment.NewLine;
                    request += string.Format("<appDeptId>{0}</appDeptId>", this.LoginInfo.m_strInpatientAreaID) + Environment.NewLine;
                    request += string.Format("<appDeptName>{0}</appDeptName>", this.LoginInfo.m_strInpatientAreaName) + Environment.NewLine;
                    request += string.Format("<appDoctId>{0}</appDoctId>", this.LoginInfo.m_strEmpID) + Environment.NewLine;
                    request += string.Format("<appDoctName>{0}</appDoctName>", this.LoginInfo.m_strEmpName) + Environment.NewLine;
                    request += string.Format("<payTypeId>{0}</payTypeId>", patVo.m_strPayTypeID) + Environment.NewLine;
                    request += string.Format("<currAreaId>{0}</currAreaId>", patVo.m_strAreaID) + Environment.NewLine;
                    request += string.Format("<currBedId>{0}</currBedId>", patVo.m_strBedID) + Environment.NewLine;
                    request += string.Format("<clinicDiag><![CDATA[{0}]]></clinicDiag>", this.m_ctlPatient.m_txtDiagnose.Text.Trim()) + Environment.NewLine;
                    request += "</request>" + Environment.NewLine;
                    //Log.Output(request);
                    //System.Diagnostics.Process.Start(file, request.Replace(" ", ""));
                    
                    string path = Application.StartupPath + "\\eApp.dll";
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(path);
                    Type type = assembly.GetType("weCare.eApp.Access");
                    object obj = Activator.CreateInstance(type);
                    System.Reflection.MethodInfo objMethodInfo = type.GetMethod("Invoke");
                    object[] objParamArr = new object[1];
                    objParamArr[0] = request.Replace(" ", "");
                    objMethodInfo.Invoke(obj, objParamArr);
                }
            }
        }
        #endregion

        #region �ٴ���Ѫ���뵥
        /// <summary>
        /// �ٴ���Ѫ���뵥
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiBloodApp_Click(object sender, EventArgs e)
        {
            //frmBloodConfirm frm = new frmBloodConfirm();
            //frm.ShowDialog();

            if (m_ctlPatient.m_objPatient != null)
            {
                frmBloodApply frm = new frmBloodApply(m_ctlPatient.m_objPatient);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("����ѡ���ˡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }

    #region ��-ҽ������Domain
    /// <summary>
    /// ҽ������Domain
    /// </summary>
    public class clsBIHOrderInputDomain : com.digitalwave.GUI_Base.clsDomainController_Base
    {
        #region ȫ�ֱ�������
        private frmBIHOrderInput m_frmInput;
        //public clsBIHOrderService m_objService;
        private clsDcl_InputOrder m_objInputOrder;
        internal System.Drawing.Printing.PrintDocument printDoc = null;

        /// <summary>
        /// ������Ƶ��
        /// </summary>
        string m_strConfreqID = "";

        /// <summary>
        /// ҽ����������
        /// </summary>
        public string[] m_arrOrderList = new string[] { "0-ȫ��ҽ��", "1-����ҽ��", "2-��ʱҽ��", "3-��ҩҽ��", "4-δͣҽ��", "5-��ͣҽ��", "6-�¿�ҽ��", "7-�ύҽ��", "8-ת��ҽ��", "9-ִ��ҽ��", "10-����ҽ��", "11-�˻�ҽ��" };

        /// <summary>
        /// ��ǰ��󷽺ż�1
        /// </summary>
        public int m_intBigRecipeNo = 1;
        /// <summary>
        /// ��Ժҽ����Ŀ
        /// </summary>
        public int m_intOutHisCout = 0;
        /// <summary>
        /// ���ص��������(intCount-���ύ��ҽ����¼��Ŀ,MaxRecipeno-��¼�����󷽺�,intBackCount-�˻ص�ҽ����Ŀ)
        /// </summary>
        public ArrayList m_arrCount = new ArrayList();
        #endregion
        #region ���캯��
        public clsBIHOrderInputDomain()
        {

        }
        public clsBIHOrderInputDomain(frmBIHOrderInput frmInput)
        {
            m_frmInput = frmInput;
            //m_objService=(clsBIHOrderService)(clsGenerator.CreateObject(typeof(clsBIHOrderService)));
            //m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            m_objInputOrder = new clsDcl_InputOrder();
        }

        #endregion
        #region ���
        public void m_mthClearOrderList()
        {
            m_frmInput.m_arlOrder.Clear();
            //m_frmInput.m_dtgOrder.m_mthDeleteAllRow();
            m_frmInput.m_dtvOrder.Rows.Clear();
            m_frmInput.m_objCurrentOrder = null;
            m_frmInput.m_intCurrentRow = -1;
            m_frmInput.m_mthShowCurrentOrder(m_frmInput.m_objCurrentOrder);
            m_mthRefreshOtherBillInfo();
            if (m_frmInput.LoginInfo != null)
            {
                m_frmInput.m_ctlOrderDetail.m_mthSetDoctor(m_frmInput.LoginInfo.m_strEmpID, m_frmInput.LoginInfo.m_strEmpName);
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        internal void HintInfo()
        {
            if (m_frmInput.m_ctlPatient.m_objPatient != null)
            {
                string dayS = string.Empty;
                int dayI = 0;
                dayS = m_frmInput.m_ctlPatient.m_txtInDays.Text.Trim();
                int.TryParse(dayS, out dayI);
                if (dayI > 0)
                {
                    bool b1 = false;
                    bool b2 = false;
                    if (m_frmInput.dayGrandRounds > 0 && m_frmInput.dayGrandRounds < dayI) b1 = true;
                    if (m_frmInput.dayAverageStay > 0 && m_frmInput.dayAverageStay < dayI) b2 = true;
                    if (b1 && b2)
                    {
                        MessageBox.Show("��������: " + m_frmInput.m_ctlPatient.m_objPatient.m_strPatientName + "\r\nסԺ����: " + dayS + "\r\n�밲�Ŵ�鷿!!!\r\n\r\n" + " ������Ժƽ��סԺ����(" + m_frmInput.dayAverageStay.ToString() + "��)\r\n�뼰ʱ��д���̼�¼!!!", "��ܰ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (b1) MessageBox.Show("��������: " + m_frmInput.m_ctlPatient.m_objPatient.m_strPatientName + "\r\nסԺ����: " + dayS + "\r\n�밲�Ŵ�鷿!!!", "��ܰ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (b2) MessageBox.Show("��������: " + m_frmInput.m_ctlPatient.m_objPatient.m_strPatientName + "\r\nסԺ����: " + dayS + " ������Ժƽ��סԺ����(" + m_frmInput.dayAverageStay.ToString() + "��)\r\n�뼰ʱ��д���̼�¼!!!", "��ܰ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        #endregion

        #region ���� ����ҽ����Ϣ(ҽ������������)
        /// <summary>
        /// ���뻼��ҽ����Ϣ
        /// </summary>
        public void m_mthLoadOrderList()
        {
            if (m_frmInput.m_ctlPatient.m_objPatient == null && clsPublicPatient.m_ObjGlobalPatient != null)
            {
                if (clsPublicPatient.m_ObjGlobalPatient.m_strInPatientID.Trim() != "")
                {
                    m_frmInput.m_ctlPatient.m_txtInHospitalNo.Text = clsPublicPatient.m_ObjGlobalPatient.m_strInPatientID;
                    m_frmInput.m_ctlPatient.m_mthGetPatientByInHospitalNo();
                    return;
                }
            }

            m_frmInput.m_blnCurrentCellChanged = false;

            //���
            m_frmInput.Cursor = Cursors.WaitCursor;
            //ˢ��ʱ����ղ��˷�����Ϣ�� 
            m_frmInput.m_lsvToolTip.Items.Clear();
            m_mthClearOrderList();
            clsBIHPatientInfo objPatient = m_frmInput.m_ctlPatient.m_objPatient;

            clsPatientBaseInfo_VO obj = new clsPatientBaseInfo_VO();
            obj.m_strPatientCardNO = objPatient.m_strPATIENTCARDID_CHR;
            obj.m_strPatientID = objPatient.m_strPatientID;
            obj.m_strRegisterID = objPatient.m_strRegisterID;
            obj.m_strInPatientID = objPatient.m_strInHospitalNo;
            obj.m_strDeptID = objPatient.m_strDeptID;
            obj.m_strName = objPatient.m_strPatientName;
            obj.m_strAge = objPatient.m_strAge;
            clsPublicPatient.m_ObjGlobalPatient = obj;

            // ����
            if (objPatient != null && !m_frmInput.m_ctlPatient.m_txtBedNo.Text.Trim().Equals("") && !m_frmInput.m_ctlPatient.m_txtArea.Text.Trim().Equals(""))
            {
                m_frmInput.m_ctlOrderDetail.m_intAge = objPatient.m_intAge;
            }
            else
            {
                m_frmInput.Cursor = Cursors.Default;
                return;
            }
            if (objPatient != null)
            {
                //�ȳ�ʼ����ǰ������ʼֵ
                m_intBigRecipeNo = 1;
                //�Ƿ�ҽ������
                m_frmInput.m_blnISMedicareMan = m_frmInput.m_ctlPatient.m_chkIsMedicareMan.Checked;
                m_frmInput.m_ctlOrderDetail.PatientID = objPatient.m_strPatientID;
                m_frmInput.m_ctlOrderDetail.RegisterID = objPatient.m_strRegisterID;
                m_frmInput.m_ctlOrderDetail.IsMedicareMan = m_frmInput.m_blnISMedicareMan;
                clsBIHOrderInputDomain m_objDomain = new clsBIHOrderInputDomain();
                List<string> PARMCODE_CHR = new List<string>();
                PARMCODE_CHR.Add("0008");
                DataTable m_dtPARMVALUE_VCHR = new DataTable();
                string str = string.Empty;
                long lngRes = m_objDomain.LoadThePARMVALUE(PARMCODE_CHR, out m_dtPARMVALUE_VCHR);
                if (m_dtPARMVALUE_VCHR.Rows.Count > 0)
                {
                    str = m_dtPARMVALUE_VCHR.Rows[0]["PARMVALUE_VCHR"].ToString();
                }
                if (str.Contains(objPatient.m_strPayTypeID))
                {
                    this.m_frmInput.m_ctlOrderDetail.cboShiying.Enabled = true;
                }
                else
                {
                    this.m_frmInput.m_ctlOrderDetail.cboShiying.Enabled = false;
                }
                int intCount = 0;//���ύ��ҽ����¼��Ŀ
                int intBackCount = 0;//�˻ص�ҽ����Ŀ
                int[] arrStatus = m_frmInput.m_arrStatus;
                clsBIHOrder[] arrOrder = null;
                //��ȡҽ��	���ݲ��˺�ҽ��״̬ 
                //long ret = this.m_objService.m_lngGetOrderByPatientAndState(objPatient.m_strRegisterID, this.m_frmInput.m_blnShowOnlyToday, this.m_frmInput.m_cboOrderList.SelectedIndex, out intCount, out m_intBigRecipeNo, out arrOrder);
                long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderByPatientAndState2(objPatient.m_strRegisterID, this.m_frmInput.LoginInfo.m_strEmpID, this.m_frmInput.m_blnShowOnlyToday, this.m_frmInput.m_cboOrderList.SelectedIndex, out intCount, out m_intBigRecipeNo, out intBackCount, out m_intOutHisCout, out arrOrder);
                if ((ret > 0) && (arrOrder != null))
                {
                    //��һ��ҽ���ķ���
                    int m_intNo = 0;
                    for (int i = 0; i < arrOrder.Length; i++)
                    {
                        m_frmInput.m_arlOrder.Add(arrOrder[i]);
                        // ͬ���ŵ���ҽ����������ʾ����/�١�����÷���Ƶ�ʡ�״̬������ҽ��
                        this.m_frmInput.m_dtvOrder.Rows.Add();
                        DataGridViewRow objRow = this.m_frmInput.m_dtvOrder.Rows[this.m_frmInput.m_dtvOrder.RowCount - 1];
                        m_intNo = this.m_frmInput.m_dtvOrder.RowCount;
                        m_objGetDataViewRow(arrOrder[i], objRow, m_intNo);
                    }
                    //m_mthRefreshGridColor();
                    //ˢ��ͬ��ҽ���ķ�����ɫ
                    m_mthRefreshSameReqNoColor();
                }
                //���������һ���հ׵�ҽ��
                //this.m_frmInput.m_dtvOrder.Rows.Add();
                /*<=============================*/
                //����ʾ״̬���ύ��ҽ����Ŀ
                if (intCount > 0)
                {
                    this.m_frmInput.pictureBox3.Visible = true;
                    this.m_frmInput.pictureBox6.Visible = true;
                    this.m_frmInput.m_cmdToCommit.Enabled = true;
                }
                else
                {
                    this.m_frmInput.pictureBox3.Visible = false;
                    this.m_frmInput.pictureBox6.Visible = false;
                    this.m_frmInput.m_cmdToCommit.Enabled = false;
                }
                //�˻�ҽ����ʾ��
                if (intBackCount > 0)
                {
                    this.m_frmInput.m_imgBackAlert.Visible = true;

                }
                else
                {
                    this.m_frmInput.m_imgBackAlert.Visible = false;
                }
                /*<========================================*/
                //���ƿ���
                //m_frmInput.PatientCardID =m_objInputOrder.m_strGetCardIDByID(objPatient.m_strPatientID);
                //��ҽ�������л����˺�ϵͳҲ�Զ���¼���л��Ĳ���Ϊѡ�в��ˡ� ͬ���Ӳ���ͬ��  
                ((com.digitalwave.iCare.gui.frmMain)this.m_frmInput.MdiParent).m_StrPatientID = objPatient.m_strPatientID;
            }
            ResetButtonToEnable();
            m_frmInput.m_cmdSave.Enabled = true;
            //m_mthRefreshOtherBillInfo();//���ӵ���

            if (this.m_frmInput.m_dtvOrder.RowCount > 0)
            {
                m_frmInput.m_dtvOrder.CurrentCell = m_frmInput.m_dtvOrder[1, this.m_frmInput.m_dtvOrder.RowCount - 1];
            }
            m_frmInput.m_cmdAdd_Click(null, null);
            m_frmInput.m_ctlOrderDetail.m_txtRecipeNo.Text = m_intBigRecipeNo.ToString();

            /*ת��ҽ�����뽹��*/
            // m_frmInput.m_ctlOrderDetail.m_txtOrderName.Focus();
            // m_frmInput.m_ctlOrderDetail.m_txtRecipeNo.Focus();
            m_frmInput.m_ctlOrderDetail.m_cboExecuteType.Focus();
            /*<-----------------*/
            m_frmInput.Cursor = Cursors.Default;
            m_frmInput.m_blnCurrentCellChanged = true;
            m_SetDisplayOrderEditState(0);
            //���ݵ�ǰ������Ŀͣ�û�ȱҩ����
            if (m_frmInput.m_blAutoStopAlert)
            {
                OrderStopSignByRegisterId();
            }


        }

        /// <summary>
        /// ���ݵ�ǰ������Ŀͣ�û�ȱҩ����
        /// </summary>
        public void OrderStopSignByRegisterId()
        {
            //ͣ����Ŀ����ͣҩ��Ŀ�б�
            DataTable m_dtOrderSign = null;
            ArrayList m_arrRecipenNo = new ArrayList();
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderStopSignByRegisterId(m_frmInput.m_ctlPatient.m_objPatient.m_strRegisterID, out m_dtOrderSign);
            if (m_dtOrderSign != null && m_dtOrderSign.Rows.Count > 0)
            {
                ArrayList m_arrStopOrderIds = GetTheAllStopOrders(m_dtOrderSign);
                if (m_arrStopOrderIds.Count > 0)
                {

                    //if (MessageBox.Show("��ǰ��δͣҽ���ѱ�ͣ�û�ͣҩ,Ҫ�Զ�ѡ����Щҽ����!", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    for (int i = 0; i < this.m_frmInput.m_dtvOrder.RowCount; i++)
                    //    {
                    //        if (m_arrStopOrderIds.Contains(((clsBIHOrder)this.m_frmInput.m_dtvOrder.Rows[i].Tag).m_strOrderID))
                    //        {
                    //            //this.m_frmInput.m_dtvOrder.Rows[i].Selected = true;
                    //            if (!m_arrRecipenNo.Contains(((clsBIHOrder)this.m_frmInput.m_dtvOrder.Rows[i].Tag).m_intRecipenNo.ToString()))
                    //            {
                    //                m_arrRecipenNo.Add(((clsBIHOrder)this.m_frmInput.m_dtvOrder.Rows[i].Tag).m_intRecipenNo.ToString());
                    //            }
                    //        }
                    //        else
                    //        {
                    //            //this.m_frmInput.m_dtvOrder.Rows[i].Selected = false;
                    //        }
                    //    }
                    //    //ѡ�иò���Ҫ�����ҽ������
                    //    ArrayList m_arrRow = new ArrayList();
                    //    for (int i = 0; i < this.m_frmInput.m_dtvOrder.RowCount; i++)
                    //    {
                    //        this.m_frmInput.m_dtvOrder.Rows[i].Selected = false;
                    //        if (m_arrRecipenNo.Contains(((clsBIHOrder)this.m_frmInput.m_dtvOrder.Rows[i].Tag).m_intRecipenNo.ToString()))
                    //        {
                    //            clsBIHOrder order1 = (clsBIHOrder)this.m_frmInput.m_dtvOrder.Rows[i].Tag;
                    //            this.m_frmInput.m_dtvOrder.Rows.Remove(this.m_frmInput.m_dtvOrder.Rows[i]);
                    //            i--;
                    //            m_arrRow.Add(order1);
                    //        }

                    //    }
                    //    if (this.m_frmInput.m_dtvOrder.RowCount > 0)
                    //    {
                    //        this.m_frmInput.m_dtvOrder.Rows[this.m_frmInput.m_dtvOrder.RowCount - 1].Selected = false;
                    //    }

                    //    //��������¼�����Щ�У���ѡ��
                    //    if (m_arrRow.Count > 0)
                    //    {
                    //        for (int i = 0; i < m_arrRow.Count; i++)
                    //        {
                    //            this.m_frmInput.m_dtvOrder.Rows.Add();
                    //            DataGridViewRow row = this.m_frmInput.m_dtvOrder.Rows[this.m_frmInput.m_dtvOrder.RowCount - 1];
                    //            m_objGetDataViewRow((clsBIHOrder)m_arrRow[i], row, row.Index + 1);
                    //            row.Selected = true;
                    //            if (i== 0)
                    //            {
                    //                m_frmInput.m_dtvOrder.CurrentCell = m_frmInput.m_dtvOrder[1, this.m_frmInput.m_dtvOrder.RowCount - 1];
                    //            }
                    //        }
                    //    }
                    //    //ˢ��ͬ��ҽ���ķ�����ɫ
                    //    m_mthRefreshSameReqNoColor();
                    //    /*<=======================*/

                    //}
                    if (MessageBox.Show("��ǰ��δͣҽ���ѱ�ͣ�û�ͣҩ,���ڴ�����Щҽ����!", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        frmStopOrderConfirm m_frmAlert = new frmStopOrderConfirm(m_frmInput.m_ctlPatient.m_objPatient.m_strRegisterID);
                        m_frmAlert.m_htOrderCate = m_frmInput.m_htOrderCate;
                        m_frmAlert.m_objSpecateVo = m_frmInput.m_objSpecateVo;
                        m_frmAlert.m_blStopControl = m_frmInput.m_blStopControl;
                        m_frmAlert.m_blDeableMedControl = m_frmInput.m_blDeableMedControl;
                        m_frmAlert.ShowDialog();
                        m_frmInput.cmdRefurbish_Click(null, null);
                    }
                    m_frmInput.m_pcBoxAlert.Visible = true;
                }
                else
                {
                    m_frmInput.m_pcBoxAlert.Visible = false;
                }
            }
            /*<=============================*/
            //ѡ�иò���Ҫ�����ҽ������
        }

        /// <summary>
        /// ���ص�ǰ����ͣ�û�ͣҩ��ҽ����ˮ����
        /// </summary>
        /// <param name="m_arrOrderIDs"></param>
        /// <returns></returns>
        private ArrayList GetTheAllStopOrders(DataTable m_dtOrderSign)
        {
            ArrayList m_arrStopOrderIds = new ArrayList();
            ArrayList m_arrOrderIDs = new ArrayList();
            if (m_dtOrderSign != null)
            {
                string orderid_chr = "";
                string STATUS_INT = "";//(������Ŀ״̬ 0-ͣ�� 1-����)
                string IFSTOP_INT = "";//ͣ�ñ�־ 1-ͣ�� 0-����
                string ITEMSRCTYPE_INT = "";//��Ŀ��Դ����1��ҩƷ��
                string IPNOQTYFLAG_INT = "";//����ҩ��ȱҩ��־ 0-��ҩ 1��ȱҩ
                bool m_blStop = false;
                for (int i = 0; i < m_dtOrderSign.Rows.Count; i++)
                {
                    m_blStop = false;
                    DataRow row = m_dtOrderSign.Rows[i];
                    orderid_chr = row["orderid_chr"].ToString().Trim();
                    STATUS_INT = row["STATUS_INT"].ToString().Trim();//(������Ŀ״̬ 0-ͣ�� 1-����)
                    IFSTOP_INT = row["IFSTOP_INT"].ToString().Trim();//ͣ�ñ�־ 1-ͣ�� 0-����
                    ITEMSRCTYPE_INT = row["ITEMSRCTYPE_INT"].ToString().Trim();//��Ŀ��Դ����1��ҩƷ��
                    IPNOQTYFLAG_INT = row["IPNOQTYFLAG_INT"].ToString().Trim();//����ҩ��ȱҩ��־ 0-��ҩ 1��ȱҩ
                    if ((STATUS_INT.Equals("0") || IFSTOP_INT.Equals("1")))
                    {
                        if (!this.m_frmInput.m_blStopControl)
                        {
                            m_blStop = true;
                        }
                    }

                    if (!this.m_frmInput.m_blDeableMedControl)
                    {
                        if (ITEMSRCTYPE_INT.Equals("1") && IPNOQTYFLAG_INT.Equals("1"))
                        {
                            m_blStop = true;
                        }
                    }


                    if (m_blStop)
                    {
                        if (!m_arrStopOrderIds.Contains(orderid_chr))
                        {
                            m_arrStopOrderIds.Add(orderid_chr);
                        }
                    }

                }
                //if (m_arrStopOrderIds.Count > 0)
                //{
                //    m_strStopOrderIds = (string[])m_arrStopOrderIds.ToArray(typeof(string));
                //}
            }
            return m_arrStopOrderIds;

        }


        /// <summary>
        /// ˢ��ͬ��ҽ���ķ�����ɫ��������ͬ���ʵ��ֶ�
        /// </summary>
        public void m_mthRefreshSameReqNoColor()
        {
            for (int i = 1; i < m_frmInput.m_dtvOrder.Rows.Count; i++)
            {
                clsBIHOrder order = (clsBIHOrder)m_frmInput.m_dtvOrder.Rows[i - 1].Tag;
                DataGridViewRow objRow = m_frmInput.m_dtvOrder.Rows[i];

                if (order.m_intRecipenNo == ((clsBIHOrder)m_frmInput.m_dtvOrder.Rows[i].Tag).m_intRecipenNo)
                {
                    //m_frmInput.m_dtvOrder.Rows[i - 1].Cells["dtv_RecipeNo"].Style.ForeColor = Color.SkyBlue;
                    //objRow.Cells["dtv_RecipeNo"].Style.ForeColor = Color.SkyBlue;
                    //���ص��ֶ�
                    //����dtv_RecipeNo
                    objRow.Cells["dtv_RecipeNo"].Value = "";
                    //����dtv_ExecuteType
                    objRow.Cells["dtv_ExecuteType"].Value = "";
                    //����ʱ��m_dtStartDate
                    //objRow.Cells["m_dtStartDate"].Value = "";
                    objRow.Cells["m_dtPOSTDATE_DAT"].Value = "";
                    //��ҽ����CREATOR_CHR
                    objRow.Cells["CREATOR_CHR"].Value = "";
                    //��ҽ����ASSESSORFOREXEC_CHR
                    objRow.Cells["ASSESSORFOREXEC_CHR"].Value = "";
                    //��ҩ��ͬ��Ҳ��ʾ�÷�
                    if (this.m_frmInput.m_objSpecateVo.m_strMID_MEDICINE_CHR.Trim().Equals(((clsBIHOrder)objRow.Tag).m_strOrderDicCateID))//��ҩ�����߼�
                    {
                        //�÷�dtv_UseType
                        objRow.Cells["dtv_UseType"].Value = ((clsBIHOrder)objRow.Tag).m_strDosetypeName;
                        objRow.Cells["dtv_REMARK"].Value = "";
                    }
                    else
                    {
                        //�÷�dtv_UseType
                        objRow.Cells["dtv_UseType"].Value = "";

                    }


                    // Ƶ��dtv_Freq
                    objRow.Cells["dtv_Freq"].Value = "";
                    //˵��dtv_ENTRUST
                    //objRow.Cells["dtv_ENTRUST"].Value = "";
                    //ͣ��ʱ��dtv_FinishDate
                    objRow.Cells["dtv_FinishDate"].Value = "";
                    //ͣҽ����dtv_Stoper
                    objRow.Cells["dtv_Stoper"].Value = "";
                    //��ҽ���� 
                    //objRow.Cells[""].Value = "";
                    //����ATTACHTIMES_INT
                    objRow.Cells["ATTACHTIMES_INT"].Value = "";
                    //ҽ��״̬STATUS_INT
                    objRow.Cells["STATUS_INT"].Value = "";
                    //ִ��ʱ��dtv_StartDate
                    objRow.Cells["dtv_StartDate"].Value = "";

                    //ִ����dtv_Executor
                    objRow.Cells["dtv_Executor"].Value = "";
                    //����ʱ��dtv_DELETE_DAT
                    objRow.Cells["dtv_DELETE_DAT"].Value = "";
                    //����ʱ��dtv_DELETE_DAT
                    objRow.Cells["dtv_DELETE_DAT"].Value = "";
                    //������dtv_DELETERNAME_VCHR
                    objRow.Cells["dtv_DELETERNAME_VCHR"].Value = "";

                    /*<=================================*/
                    //Ƥ��
                    string m_strFeel = "";
                    if (((clsBIHOrder)objRow.Tag).m_intISNEEDFEEL == 1)
                    {

                        switch (((clsBIHOrder)objRow.Tag).m_intFEEL_INT)
                        {
                            case 0:
                                m_strFeel = " AST( ) ";
                                break;
                            case 1:
                                m_strFeel = " AST(-) ";
                                break;
                            case 2:
                                m_strFeel = " AST(+) ";
                                break;
                        }

                    }

                    //����  ͬһ���ŵ�ҽ������ҽ�����÷���Ƶ�ʲ�����ʾ
                    objRow.Cells["dtv_Name"].Value = ((clsBIHOrder)objRow.Tag).m_strName + " " + objRow.Cells["dtv_Dosage"].Value.ToString() + "  " + objRow.Cells["dtv_UseType"].Value.ToString() + " " + m_strFeel;


                }

                //��ͣ������ͣ��ҽ��,��ִ�й��������ú�ɫ��ʾ(����ִ�г�Ժ��ҩ)

                if (order.m_intStatus == 3 || order.m_intStatus == 6 || (order.m_intExecuteType == 2 && order.m_intStatus == 2) || (order.m_intExecuteType == 3 && order.m_intStatus == 2))
                {
                    m_frmInput.m_dtvOrder.Rows[i - 1].DefaultCellStyle.ForeColor = Color.Red;
                }
                else if (order.m_intStatus == -1)
                {
                    m_frmInput.m_dtvOrder.Rows[i - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(255)))));

                }

            }
            if (m_frmInput.m_dtvOrder.RowCount > 0)
            {
                //��ͣ������ͣ��ҽ��,��ִ�й��������ú�ɫ��ʾ(���һ���Ĵ���)
                clsBIHOrder order2 = (clsBIHOrder)m_frmInput.m_dtvOrder.Rows[m_frmInput.m_dtvOrder.RowCount - 1].Tag;
                if (order2.m_intStatus == 3 || order2.m_intStatus == 6 || (order2.m_intExecuteType == 2 && order2.m_intStatus == 2) || (order2.m_intExecuteType == 3 && order2.m_intStatus == 2))
                {
                    m_frmInput.m_dtvOrder.Rows[m_frmInput.m_dtvOrder.RowCount - 1].DefaultCellStyle.ForeColor = Color.Red;
                }
                else if (order2.m_intStatus == -1)
                {
                    m_frmInput.m_dtvOrder.Rows[m_frmInput.m_dtvOrder.RowCount - 1].DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(255)))));

                }
            }
        }

        /// <summary>
        /// ҽ�����DATAGRIDVIEW
        /// </summary>
        /// <param name="objOrder">ҽ������</param>
        /// <param name="m_intRecipenNoUp">��һ��ҽ���ķ���(ͬ���ŵ���ҽ����������ʾ����/�١�����÷���Ƶ�ʡ�״̬������ҽ��)</param>
        public void m_objGetDataViewRow(clsBIHOrder objOrder, DataGridViewRow objRow, int m_intNo)
        {
            objRow.Height = 20;
            decimal m_dmlOneUse = 0;//��һ�ε�����
            //��
            objRow.Cells["dtv_NO"].Value = m_intNo;
            //ҽ������
            clsT_aid_bih_ordercate_VO p_objItem = (clsT_aid_bih_ordercate_VO)this.m_frmInput.m_htOrderCate[objOrder.m_strOrderDicCateID];
            if (p_objItem == null)
            {
                //if (objOrder.m_strName.ToString().Trim().Equals("����ҽ��"))
                //{
                //    objRow.Cells["dtv_Name"].Value = objOrder.m_strName.ToString();
                //    objRow.Cells["dtv_Name"].Style.Alignment = DataGridViewContentAlignment.BottomCenter;
                //    objRow.Tag = objOrder;
                //    return;
                //}
                //else if (objOrder.m_strName.ToString().Trim().Equals("ת��ҽ��"))
                //{
                //    objRow.Cells["dtv_Name"].Value = objOrder.m_strName.ToString();
                //    objRow.Cells["dtv_Name"].Style.Alignment = DataGridViewContentAlignment.BottomCenter;
                //    objRow.Tag = objOrder;
                //    return;
                //}
                if (objOrder.m_intTYPE_INT > 0)
                {
                    objRow.Cells["dtv_Name"].Value = objOrder.m_strName.ToString();
                    objRow.Cells["dtv_Name"].Style.Alignment = DataGridViewContentAlignment.BottomCenter;
                    objRow.Tag = objOrder;
                    return;
                }
            }
            if (objOrder.m_intExecuteType == 1)
            {
                //��
                objRow.Cells["dtv_RecipeNo"].Value = " " + objOrder.m_intRecipenNo2.ToString();
            }
            //�۸�
            //objRow["Price"] =objOrder.m_dmlPrice.ToString("0.0000");

            //���������С�������ʾ����ҽ�����������ͣ��ͼ��ҽ���Ĳ�λ��Ϣ
            if (!objOrder.m_strPARTID_VCHR.Trim().Equals(""))
            {
                objRow.Cells["dtv_method"].Value = objOrder.m_strPARTNAME_VCHR;
            }
            else if (!objOrder.m_strSAMPLEID_VCHR.Trim().Equals(""))
            {
                objRow.Cells["dtv_method"].Value = objOrder.m_strSAMPLEName_VCHR;
            }



            //��ʼִ��ʱ��
            objRow.Cells["dtv_StartDate"].Value = DateTimeToString(objOrder.m_dtExecutedate);
            //ͣ����
            objRow.Cells["dtv_Stoper"].Value = objOrder.m_strStoper;
            //���ֹͣ��
            objRow.Cells["ASSESSORFORSTOP_CHR"].Value = objOrder.m_strASSESSORFORSTOP_CHR;
            //ͣ��ʱ��
            objRow.Cells["dtv_FinishDate"].Value = this.m_frmInput.DateTimeToCutYearDateString(objOrder.m_dtFinishDate);
            //objRow.Cells["dtv_ParentName"].Value = objOrder.m_strParentName;
            //ִ��ʱ��/����
            objRow.Cells["dtv_REMARK"].Value = objOrder.m_strREMARK_VCHR;
            if (objOrder.IsOps == 1) objRow.Cells["isOps"].Value = "��";

            //У�Ի�ʿ
            objRow.Cells["ASSESSORFOREXEC_CHR"].Value = objOrder.m_strASSESSORFOREXEC_CHR;
            //¼��ʱ��
            objRow.Cells["CREATEDATE_DAT"].Value = DateTimeToString(objOrder.m_dtCreatedate);
            //����ʱ��(��ʼʱ�䣩
            objRow.Cells["m_dtPOSTDATE_DAT"].Value = this.m_frmInput.DateTimeToCutYearDateString(objOrder.m_dtPostdate);
            objRow.Cells["m_dtStartDate"].Value = this.m_frmInput.DateTimeToCutYearDateString(objOrder.m_dtStartDate);
            //Ƥ��
            string m_strFeel = "";
            if (objOrder.m_intISNEEDFEEL == 1)
            {

                switch (objOrder.m_intFEEL_INT)
                {
                    case 0:
                        m_strFeel = " AST( ) ";
                        break;
                    case 1:
                        m_strFeel = " AST(-) ";
                        break;
                    case 2:
                        m_strFeel = " AST(+) ";
                        break;
                }

            }

            #region ҽ�����Ϳ����б����
            if (p_objItem != null)
            {
                objOrder.m_strOrderDicCateName = p_objItem.m_strVIEWNAME_VCHR;

                if (!objOrder.m_strExecFreqID.Trim().Equals(this.m_frmInput.m_objSpecateVo.m_strCONFREQID_CHR.Trim()))//������ҽ������ʾ����
                {
                    if (p_objItem.m_intDOSAGEVIEWTYPE == 1)
                    {
                        //����
                        if (objOrder.m_dmlDosage > 0)
                        {
                            objRow.Cells["dtv_Dosage"].Value = objOrder.m_dmlDosage.ToString() + "" + objOrder.m_strDosageUnit;
                        }
                        else
                        {
                            objRow.Cells["dtv_Dosage"].Value = "";

                        }
                    }
                    else
                    {
                        objRow.Cells["dtv_Dosage"].Value = "";
                    }
                }
                else
                {
                    objRow.Cells["dtv_Dosage"].Value = "";
                }
                if (!objOrder.m_strExecFreqID.Trim().Equals(this.m_frmInput.m_objSpecateVo.m_strCONFREQID_CHR.Trim()))//������ҽ������ʾ�÷�
                {
                    if (p_objItem.m_intUSAGEVIEWTYPE == 1)
                    {
                        //�÷�
                        objRow.Cells["dtv_UseType"].Value = objOrder.m_strDosetypeName;
                    }
                    else
                    {
                        //�÷�
                        objRow.Cells["dtv_UseType"].Value = "";
                    }
                }
                else
                {
                    //�÷�
                    objRow.Cells["dtv_UseType"].Value = "";
                }
                if (objOrder.m_intExecuteType == 1 || objOrder.m_intExecuteType == 2)//���ٲ���ʾƵ�ʣ���Ժ��ҩ����ʾ
                //if (objOrder.m_intExecuteType == 1)//���ٲ���ʾƵ�ʣ���������ʾ
                {
                    if (p_objItem.m_intExecuFrenquenceType == 1)
                    {
                        //Ƶ��
                        objRow.Cells["dtv_Freq"].Value = objOrder.m_strExecFreqName;
                    }
                    else
                    {
                        //������ʾʱ��ҽ�����е�Ϊ�޸ı�־=1ʱҲ��ʾ���� (0-��ͨ״̬,1-Ƶ���޸�)
                        if (objOrder.m_intCHARGE_INT == 1)
                        {
                            objRow.Cells["dtv_Freq"].Value = objOrder.m_strExecFreqName;//Ƶ��
                        }
                        else
                        {
                            objRow.Cells["dtv_Freq"].Value = "";//Ƶ��
                        }
                    }
                }
                else
                {
                    //������ʾʱ��ҽ�����е�Ϊ�޸ı�־=1ʱҲ��ʾ���� (0-��ͨ״̬,1-Ƶ���޸�)
                    if (objOrder.m_intCHARGE_INT == 1)
                    {
                        objRow.Cells["dtv_Freq"].Value = objOrder.m_strExecFreqName;//Ƶ��
                    }
                    else
                    {
                        objRow.Cells["dtv_Freq"].Value = "";//Ƶ��
                    }

                }

                if (p_objItem.m_intAPPENDVIEWTYPE_INT == 1)
                {
                    //����
                    objRow.Cells["ATTACHTIMES_INT"].Value = objOrder.m_intATTACHTIMES_INT;
                    m_dmlOneUse = objOrder.m_dmlOneUse * objOrder.m_intATTACHTIMES_INT;
                }
                else
                {
                    //����
                    objRow.Cells["ATTACHTIMES_INT"].Value = "";
                    m_dmlOneUse = 0;
                }
                //����
                if (p_objItem.m_intQTYVIEWTYPE_INT == 1)
                {
                    if (objOrder.m_dmlGet > 0)
                    {
                        objRow.Cells["dtv_Get"].Value = objOrder.m_dmlGet.ToString() + " " + objOrder.m_strGetunit;

                    }
                    else
                    {
                        objRow.Cells["dtv_Get"].Value = "";

                    }
                }
                else
                {
                    //����
                    objRow.Cells["dtv_Get"].Value = "";
                }
            }
            else
            {
                //����
                objRow.Cells["dtv_Dosage"].Value = "";
                //Ƶ��
                objRow.Cells["dtv_Freq"].Value = "";
                //�÷�
                objRow.Cells["dtv_UseType"].Value = "";
                //����
                objRow.Cells["ATTACHTIMES_INT"].Value = "";
                //����
                objRow.Cells["dtv_Get"].Value = "";

            }
            #endregion

            // ҩƷ��Դ: 0 ҩ��(ȫ�Ʒ�,��ҩ); 1 �����Ա�(ֻ�շ��÷�������Ŀ,����ҩ); 2 ���һ���(ȫ�Ʒѣ�����ҩ) 
            if (objOrder.m_strOrderDicCateName != null && objOrder.m_strOrderDicCateName.ToString().Trim() == "ҩ��")
            {
                switch (objOrder.RateType)
                {
                    //case 0:
                    //    objRow.Cells["RATETYPE_INT"].Value = "��";
                    //    break;
                    //case 1:
                    //    objRow.Cells["RATETYPE_INT"].Value = "��";
                    //    break;
                    //case 2:
                    //    objRow.Cells["RATETYPE_INT"].Value = "";
                    //    break;
                    case 0:
                        objRow.Cells["RATETYPE_INT"].Value = "ҩ��";
                        break;
                    case 1:
                        objRow.Cells["RATETYPE_INT"].Value = "�Ա�";
                        break;
                    case 2:
                        objRow.Cells["RATETYPE_INT"].Value = "����";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                objRow.Cells["RATETYPE_INT"].Value = "";
            }

            //��Ժ��ҩ����
            string m_strOUTGETMEDDAYS_INT = "";
            //�����ֶεĿ���
            if (objOrder.m_strOrderDicCateID.Equals(this.m_frmInput.m_objSpecateVo.m_strMID_MEDICINE_CHR))//��ҩ�����߼�
            {
                objRow.Cells["dtv_sum"].Value = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "����" + Convert.ToString(objOrder.m_dmlGet + m_dmlOneUse) + objOrder.m_strGetunit;
                m_strOUTGETMEDDAYS_INT = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "��"; ;
            }
            else
            {

                if (objOrder.m_intExecuteType == 3)
                {
                    objRow.Cells["dtv_sum"].Value = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "�칲" + Convert.ToString(objOrder.m_dmlGet + m_dmlOneUse) + objOrder.m_strGetunit;
                    m_strOUTGETMEDDAYS_INT = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "��";
                    //objRow.Cells["dtv_OUTGETMEDDAYS_INT"].Value = objOrder.m_intOUTGETMEDDAYS_INT.ToString() + "��";
                }
                else
                {
                    objRow.Cells["dtv_sum"].Value = "��" + Convert.ToString(objOrder.m_dmlGet + m_dmlOneUse) + objOrder.m_strGetunit;
                    m_strOUTGETMEDDAYS_INT = "";
                    //objRow.Cells["dtv_OUTGETMEDDAYS_INT"].Value = "";
                }
                objRow.Cells["dtv_OUTGETMEDDAYS_INT"].Value = m_strOUTGETMEDDAYS_INT;
            }

            //����
            objRow.Cells["dtv_Name"].Value = objOrder.m_strName + " " + objRow.Cells["dtv_Dosage"].Value.ToString() + " " + objRow.Cells["dtv_UseType"].Value.ToString() + " " + objRow.Cells["dtv_Freq"].Value.ToString() + m_strFeel + " " + m_strOUTGETMEDDAYS_INT;
            //���Ƹ�ʽ����
            if (p_objItem != null)
            {
                if (p_objItem.m_strVIEWNAME_VCHR.ToString().Trim() == "����ҽ��")
                {
                    objRow.Cells["dtv_Name"].Value = "   " + objRow.Cells["dtv_Name"].Value.ToString();

                }
            }

            /*<=====================================================================*/
            //ҽ��
            objRow.Cells["MedicareTypeName"].Value = objOrder.m_strMedicareTypeName;
            //ҽ������ 
            objRow.Cells["dtv_DOCTOR_VCHR"].Value = objOrder.m_strDOCTOR_VCHR;
            //�������� 
            objRow.Cells["dtv_CREATEAREA_Name"].Value = objOrder.m_strCREATEAREA_Name;
            //������ 
            objRow.Cells["dtv_DELETERNAME_VCHR"].Value = objOrder.m_strDELETERNAME_VCHR;
            //����ʱ�� 
            objRow.Cells["dtv_DELETE_DAT"].Value = objOrder.m_strDELETE_DAT;
            //�޸�������
            objRow.Cells["dtv_ChangedID"].Value = objOrder.m_strChangedName_CHR;
            //�޸���ʱ��
            objRow.Cells["dtv_ChangedDate"].Value = DateTimeToString(objOrder.m_dtChanged_DAT);
            // ͬ���ŵ���ҽ����������ʾ����/�١�����÷���Ƶ�ʡ�״̬������ҽ��
            //��/��
            if (objOrder.m_intExecuteType == 1)
            {
                objRow.Cells["dtv_ExecuteType"].Value = "����";

            }
            else if (objOrder.m_intExecuteType == 2)
            {
                objRow.Cells["dtv_ExecuteType"].Value = "��ʱ";

            }
            else if (objOrder.m_intExecuteType == 3)
            {
                objRow.Cells["dtv_ExecuteType"].Value = "��ҩ";

            }
            else
            {
                objRow.Cells["dtv_ExecuteType"].Value = "";
            }


            //ҽ����������
            objRow.Cells["viewname_vchr"].Value = objOrder.m_strOrderDicCateName.ToString().Trim();
            //ҽ��״̬ ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ������ύ;6-���ֹͣ;7-�˻�;}
            switch (objOrder.m_intStatus)
            {
                case -2:
                    objRow.Cells["STATUS_INT"].Value = "��ɾ��";
                    break;
                case -1:
                    objRow.Cells["STATUS_INT"].Value = "����";
                    break;
                case 0:
                    objRow.Cells["STATUS_INT"].Value = "�¿�";
                    break;
                case 1:
                    objRow.Cells["STATUS_INT"].Value = "�ύ";
                    break;
                case 2:
                    objRow.Cells["STATUS_INT"].Value = "ִ��";
                    break;
                case 3:
                    objRow.Cells["STATUS_INT"].Value = "ֹͣ";
                    break;
                case 4:
                    objRow.Cells["STATUS_INT"].Value = "����";
                    break;
                case 5:
                    objRow.Cells["STATUS_INT"].Value = "ת��";
                    break;
                case 6:
                    objRow.Cells["STATUS_INT"].Value = "���ֹͣ";
                    break;
                case 7:
                    objRow.Cells["STATUS_INT"].Value = "�˻�";
                    break;
                default:
                    objRow.Cells["STATUS_INT"].Value = "";
                    break;
            }
            //����ҽ��
            objRow.Cells["CREATOR_CHR"].Value = objOrder.m_strCreator;
            //ִ����
            objRow.Cells["dtv_Executor"].Value = objOrder.m_strExecutor;
            ////ҽ��ǩ��dtv_DOCTOR_SIGN
            //if (this.m_frmInput.m_blDoctorSign)
            //{
            //    if (objOrder.SIGN_GRP != null && objOrder.SIGN_INT == 1)
            //    {
            //        System.IO.MemoryStream ms = new System.IO.MemoryStream(objOrder.SIGN_GRP);
            //        Bitmap m_bpSign = new Bitmap(ms);
            //        objRow.Cells["dtv_DOCTOR_SIGN"].Value = m_bpSign;
            //        ms.Close();
            //    }
            //    else if (objOrder.SIGN_INT == 0)
            //    {

            //        Bitmap m_bpSign = new Bitmap("Picture//unsign.bmp");
            //        objRow.Cells["dtv_DOCTOR_SIGN"].Value = m_bpSign;

            //    }
            //    else
            //    {

            //        objRow.Cells["dtv_DOCTOR_SIGN"].Style.NullValue = null;
            //    }

            //    if (this.m_frmInput.m_blDoctorSign && objOrder.SIGN_INT != 1)
            //    {
            //        objRow.DefaultCellStyle.ForeColor = Color.Red;
            //    }
            //}
            //�˻���
            objRow.Cells["m_dtvSENDBACKER_CHR"].Value = objOrder.m_strSENDBACKER_CHR;

            objRow.Tag = objOrder;
        }

        private void m_objGetDataViewRow2(int intNo, clsBIHOrder objOrder, object p_3)
        {
            string[] m_arrValue = new string[this.m_frmInput.m_dtvOrder.ColumnCount];
            int k = 0;

            //objRow.Cells["dtv_NO"].Value = intNo;
            m_arrValue[k++] = intNo.ToString();
            //objRow.Cells["dtv_RecipeNo"].Value = objOrder.m_intRecipenNo;
            m_arrValue[k++] = objOrder.m_intRecipenNo.ToString();
            //�۸�
            //objRow["Price"] =objOrder.m_dmlPrice.ToString("0.0000");
            if (objOrder.m_intExecuteType == 1)
            {
                //  objRow.Cells["dtv_ExecuteType"].Value = "��";
                m_arrValue[k++] = "��";
            }
            else
            {
                if (objOrder.m_intExecuteType == 2)
                {
                    // objRow.Cells["dtv_ExecuteType"].Value = "��";
                    m_arrValue[k++] = "��";
                }
                else
                {
                    //objRow.Cells["dtv_ExecuteType"].Value = "";
                    m_arrValue[k++] = "";
                }
            }
            //objRow.Cells["dtv_Name"].Value = objOrder.m_strName;
            m_arrValue[k++] = objOrder.m_strName;

            if (objOrder.m_dmlDosage > 0)
            {
                //objRow.Cells["dtv_Dosage"].Value = objOrder.m_dmlDosage.ToString() + " " + objOrder.m_strDosageUnit;
                m_arrValue[k++] = objOrder.m_dmlDosage.ToString() + " " + objOrder.m_strDosageUnit;
            }
            else
            {
                //objRow.Cells["dtv_Dosage"].Value = "";
                m_arrValue[k++] = "";
            }
            if (objOrder.m_dmlGet > 0)
            {
                //objRow.Cells["dtv_Get"].Value = objOrder.m_dmlGet.ToString() + " " + objOrder.m_strGetunit;
                m_arrValue[k++] = objOrder.m_dmlGet.ToString() + " " + objOrder.m_strGetunit;
            }
            else
            {
                //objRow.Cells["dtv_Get"].Value = "";
                m_arrValue[k++] = "";
            }
            //�ϼ�
            //objRow["TotalMoney"] =(double.Parse(objOrder.m_dmlGet.ToString()) * double.Parse(objOrder.m_dmlPrice.ToString())).ToString("0.00");
            //objRow.Cells["dtv_Freq"].Value = objOrder.m_strExecFreqName;
            m_arrValue[k++] = objOrder.m_strExecFreqName;
            //objRow.Cells["dtv_UseType"].Value = objOrder.m_strDosetypeName;
            m_arrValue[k++] = objOrder.m_strDosetypeName;
            if (objOrder.m_intISNEEDFEEL == 1)
            {
                //objRow.Cells["dtv_ISNEEDFEEL"].Value = "��";
                m_arrValue[k++] = "��";
            }
            else
            {
                //objRow.Cells["dtv_ISNEEDFEEL"].Value = "";//��
                m_arrValue[k++] = "";
            }
            //objRow.Cells["dtv_StartDate"].Value = DateTimeToString(objOrder.m_dtStartDate);
            m_arrValue[k++] = DateTimeToString(objOrder.m_dtStartDate);
            //objRow.Cells["dtv_Stoper"].Value = objOrder.m_strStoper;
            m_arrValue[k++] = objOrder.m_strStoper;
            //objRow.Cells["dtv_StopDate"].Value = DateTimeToString(objOrder.m_dtStopdate);
            m_arrValue[k++] = DateTimeToString(objOrder.m_dtStopdate);
            //objRow.Cells["dtv_ParentName"].Value = objOrder.m_strParentName;
            m_arrValue[k++] = objOrder.m_strParentName;

            this.m_frmInput.m_dtvOrder.Rows.Add(m_arrValue);
            //if (m_frmInput.m_blLoad)
            //{
            //    this.m_frmInput.m_dtvOrder.CurrentCell = null;
            //    this.m_frmInput.m_blLoad = false;


            //}

        }

        /// <summary>
        /// ����ҽ������
        /// </summary>
        public void m_Loadm_lngGetOrderCate()
        {
            long lngRes = 0;
            clsT_aid_bih_ordercate_VO[] p_objItemArr = null;
            lngRes = m_objInputOrder.m_lngGetAidOrderCate(out p_objItemArr);
            this.m_frmInput.m_htOrderCate.Clear();
            for (int i = 0; i < p_objItemArr.Length; i++)
            {
                if (!this.m_frmInput.m_htOrderCate.Contains(p_objItemArr[i].m_strORDERCATEID_CHR))
                {
                    this.m_frmInput.m_htOrderCate.Add(p_objItemArr[i].m_strORDERCATEID_CHR, p_objItemArr[i]);
                }
            }

            //DelItems(ref p_objItemArr);
            if (lngRes > 0 && p_objItemArr != null && p_objItemArr.Length > 0)
            {
                m_frmInput.m_ctlOrderDetail.m_cobOrderCate.Items.Clear();
                //����ҽ�����Ͷ���
                clsOrderCate objItem2 = new clsOrderCate();
                objItem2.m_objOrderCate.m_strVIEWNAME_VCHR = "ȫ��";
                objItem2.m_objOrderCate.m_strORDERCATEID_CHR = "";

                //ҽ����������Ĭ�����Ͳ�ѯ����
                m_frmInput.m_ctlOrderDetail.m_txtOrderCate.Text = objItem2.m_objOrderCate.m_strVIEWNAME_VCHR;
                m_frmInput.m_ctlOrderDetail.m_txtOrderCate.Tag = objItem2.m_objOrderCate.m_strORDERCATEID_CHR;
                /*<================================*/

                String CatName = "";
                for (int i1 = 0; i1 < p_objItemArr.Length; i1++)
                {
                    clsOrderCate objItem = new clsOrderCate();
                    objItem.m_objOrderCate = p_objItemArr[i1];
                    if (objItem.m_objOrderCate.m_intORDERSELECT_INT == 1)
                    {
                        m_frmInput.m_ctlOrderDetail.m_cobOrderCate.Items.Add(objItem);
                    }

                    CatName = objItem.m_objOrderCate.m_strVIEWNAME_VCHR.Trim();
                    if (CatName != "" && !m_frmInput.m_ctlOrderDetail.hasAppendViewType.ContainsKey(CatName))
                    {
                        m_frmInput.m_ctlOrderDetail.hasAppendViewType.Add(CatName, objItem.m_objOrderCate.m_intAPPENDVIEWTYPE_INT.ToString());
                    }
                }
                m_frmInput.m_ctlOrderDetail.m_cobOrderCate.Items.Insert(0, objItem2);
            }
        }
        private void DelItems(ref clsT_aid_bih_ordercate_VO[] p_objItemArr)
        {
            System.Collections.ArrayList aa = new ArrayList();
            for (int i = 0; i < p_objItemArr.Length; i++)
            {
                if (!aa.Contains(p_objItemArr[i].m_strVIEWNAME_VCHR))
                {
                    aa.Add(p_objItemArr[i].m_strVIEWNAME_VCHR);
                }
            }
            p_objItemArr = new clsT_aid_bih_ordercate_VO[aa.Count];
            for (int i = 0; i < aa.Count; i++)
            {
                p_objItemArr[i] = new clsT_aid_bih_ordercate_VO();
                p_objItemArr[i].m_strVIEWNAME_VCHR = aa[i].ToString();
            }
        }
        #endregion
        #region �¼�	{�ύҽ�����ύ����ҽ����ֹͣҽ��������ҽ��������ҽ��}
        #region �ύҽ��
        /// <summary>
        /// �ύҽ��
        /// ִ��״̬	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;7-�˻�;}
        ///  ҵ��˵����	ֻ���״̬��0-����;
        /// </summary>
        public void m_mthShowCommitForm()
        {
            int count = 0;
            if (m_frmInput.m_blDoctorSign)
            {
                count = CountTheSignOrder();
                if (count > 0)
                {
                    MessageBox.Show("��ûǩ����ҽ�������ܽ����ύ!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }
            }
            count = 0;
            //��ǰ����Ա���ύ��ҽ����Ŀ
            count = CountTheAdminOrder();
            if (count <= 0)
            {
                return;
            }
            if (MessageBox.Show("�Ƿ��ύ�¿�ҽ��?", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                frmBIHOrderCommit objForm = new frmBIHOrderCommit(this.m_frmInput.m_blCommitControl, this.m_frmInput.m_blSendLisBill, this.m_frmInput.m_htOrderCate, this.m_frmInput.m_ctlPatient.m_objPatient.m_strDiagnose);
                objForm.LoginInfo = m_frmInput.LoginInfo;
                objForm.m_blnCloseAfterCommit = true;
                if (m_frmInput.m_ctlPatient.m_objPatient != null)
                {
                }
                else
                {
                    m_mthShowMessage("����ѡ����ύҽ���Ĳ���!");
                    m_frmInput.m_ctlOrderDetail.m_txtBedNo.Focus();
                    return;
                }
                //���ݵ�ǰ������Ŀͣ�û�ȱҩ����
                if (m_frmInput.m_blAutoStopAlert)
                {
                    OrderStopSignByRegisterId();
                }
                // 4006����Ϊ8��������м��飨��Ʊ����Ϊ���飩�շ���Ŀ>8��ʱ���ô��۹���
                objForm.m_intLisDiscountNum = m_frmInput.m_intLisDiscountNum;
                // 4007�������ô��۹���ʱ�������շ���Ŀ�Ĵ��۱�����80��������
                objForm.m_decLisDiscountMount = m_frmInput.m_decLisDiscountMount;
                // 4008  0-false������ 1-true �������
                objForm.m_blLisDiscount = m_frmInput.m_blLisDiscount;
                // ϵͳ������(ICARE����) 0013 ������ϴ��۷�Ʊ���� ������������ݸ���
                objForm.m_strLisPARMVALUE_VCHR = m_frmInput.m_strLisPARMVALUE_VCHR;
                //�������ñ�
                objForm.m_objSpecateVo = m_frmInput.m_objSpecateVo;
                //1067
                objForm.blnFillApplyBill = m_frmInput.blnFillApplyBill;
                // 1032
                objForm.m_intComfirm = m_frmInput.m_intCommitControl2;
                this.m_frmInput.Cursor = Cursors.WaitCursor;
                objForm.Find_Order2(m_frmInput.LoginInfo.m_strEmpID, m_frmInput.m_ctlPatient.m_objPatient.m_strRegisterID);
                if (objForm.m_dtvOrder.RowCount <= 0)
                {
                    this.m_frmInput.Cursor = Cursors.Default;
                    return;
                }
                objForm.m_cmdCommit_Click(null, null);
                this.m_frmInput.Cursor = Cursors.Default;
                m_mthLoadOrderList();
                this.m_frmInput.m_ctlOrderDetail.m_txtBedNo.Focus();
            }
        }

        private int CountTheAdminOrder()
        {
            int count = 0;
            for (int i = 0; i < this.m_frmInput.m_dtvOrder.RowCount; i++)
            {
                clsBIHOrder order = (clsBIHOrder)m_frmInput.m_dtvOrder.Rows[i].Tag;
                if (order.m_strCreatorID == this.m_frmInput.LoginInfo.m_strEmpID && (order.m_intStatus == 0 || order.m_intStatus == 7))
                {
                    count++;
                }
            }
            return count;
        }
        private int CountTheSignOrder()
        {
            int count = 0;
            for (int i = 0; i < this.m_frmInput.m_dtvOrder.RowCount; i++)
            {
                clsBIHOrder order = (clsBIHOrder)m_frmInput.m_dtvOrder.Rows[i].Tag;
                if (order.m_strCreatorID == this.m_frmInput.LoginInfo.m_strEmpID && order.m_intStatus == 0 && order.SIGN_INT == 0)
                {
                    count++;
                }
            }
            return count;
        }
        #endregion
        #region �ύ����ҽ��	����
        /// <summary>
        /// �ύ����ҽ��
        /// ִ��״̬	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;7-�˻�;}
        ///  ҵ��˵����	ֻ���״̬��0-����;
        /// </summary>
        public void m_mthCommitAll()
        {
            ArrayList arlOrder = new ArrayList();
            for (int i = 0; i < m_frmInput.m_arlOrder.Count; i++)
            {
                if ((m_frmInput.m_arlOrder[i] as clsBIHOrder).m_intStatus == 0 || (m_frmInput.m_arlOrder[i] as clsBIHOrder).m_intStatus == 7)
                    arlOrder.Add(m_frmInput.m_arlOrder[i]);
            }
            clsBIHOrder[] arrOrder = (arlOrder.ToArray(typeof(clsBIHOrder))) as clsBIHOrder[];
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngPostOrder(arrOrder, m_frmInput.m_objCurrentDoctor.m_strDoctorID, m_frmInput.m_objCurrentDoctor.m_strDoctorName);
            if (ret > 0)
            {
                m_mthRefreshGridColor();

                if ((m_frmInput.m_objCurrentOrder != null) && (m_frmInput.m_objCurrentOrder.m_intStatus != 0) && (m_frmInput.m_objCurrentOrder.m_intStatus != 7))
                {
                    m_frmInput.m_ctlOrderDetail.ReadOnly = true;
                }
                //m_mthShowMessage("�����ɹ���");
            }
            else
            {
                m_mthShowMessage("�ύʧ��!");
            }
            m_mthRefreshOtherBillInfo();
        }
        #endregion
        #region ֹͣҽ��
        /// <summary>
        /// ֹͣ����ҽ��	
        /// ִ��״̬	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;7-�˻�;}
        /// ҵ��˵����	ֻ���״̬��2-ִ��
        /// </summary>
        /// <param name="blnPrompt"></param>
        public void m_mthStopCurrentOrder(bool blnPrompt, clsBIHOrder BihOrder)
        {
            //if (m_frmInput.m_objCurrentOrder == null)
            //{
            //    int intCurRow =m_frmInput.m_dtgOrder.CurrentCell.RowNumber;
            //    if ((intCurRow >= 0) && (intCurRow < m_frmInput.m_arlOrder.Count))
            //    {

            //        m_frmInput.m_intCurrentRow = intCurRow;
            //        m_frmInput.m_objCurrentOrder = m_frmInput.m_arlOrder[intCurRow] as clsBIHOrder;
            //    }
            //    else
            //    {
            //        MessageBox.Show("����ѡ��һ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //}
            /*<=====================================================*/

            if (BihOrder.m_intExecuteType == 2 || BihOrder.m_intStatus != 2)
            {
                m_mthShowMessage("ֻ��ֹͣ������ִ�г���ҽ������");
                return;
            }
            clsBIHOrder objOrder = BihOrder;
            DateTime m_dtStopTime = DateTime.MinValue;
            if (m_frmInput.m_ctlOrderDetail.m_dtFinishTime2.Enabled == true)
            {
                try
                {
                    m_dtStopTime = Convert.ToDateTime(m_frmInput.m_ctlOrderDetail.m_dtFinishTime2.Text.ToString().Trim());
                }
                catch
                {
                    m_dtStopTime = DateTime.MinValue;
                }
            }
            /*
            long ret =0;
            if(blnPrompt)
            {
                #region ��ʾ
                //�Ƿ�Ϊ����Ŀ¼	
                if(!IsHaveSonOrder(m_frmInput.m_objCurrentOrder.m_strOrderID))
                {
                    if(MessageBox.Show(m_frmInput,"�Ƿ�ȷ��ֹͣҽ����" + m_frmInput.m_objCurrentOrder.m_strName + " ����","��ʾ��",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
                    {
                        return;
                    }
                    else
                    {					
                        ret=m_objService.m_lngStopOrder(new clsBIHOrder[]{objOrder},m_frmInput.m_objCurrentDoctor.m_strDoctorID,m_frmInput.m_objCurrentDoctor.m_strDoctorName);
                    }
                }
                else
                {
                    frmConfirmOrderOperate objfrmConfirmOrderOperate =new frmConfirmOrderOperate("ֹͣ",m_frmInput.m_objCurrentOrder.m_strOrderID);
                    objfrmConfirmOrderOperate.m_txbPatientName.Text =m_frmInput.m_ctlPatient.m_objPatient.m_strPatientName;
                    objfrmConfirmOrderOperate.m_txbOrderName.Text =m_frmInput.m_objCurrentOrder.m_strName;
                   
                    objfrmConfirmOrderOperate.ShowDialog();
                   
                    if(objfrmConfirmOrderOperate.m_intResult==0) 
                    {
                        return;
                    }
                    else
                    {
                        //�ݹ�ֹͣҽ��
                        try
                        {
                            ret=m_objService.m_lngStopOrder(m_frmInput.m_objCurrentOrder,m_frmInput.m_objCurrentDoctor.m_strDoctorID,m_frmInput.m_objCurrentDoctor.m_strDoctorName,true);
                        }
                        catch(System.Exception e)
                        {
                            m_mthShowMessage(e.Message);
                            return;
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region ����ʾ
                //�Ƿ�Ϊ����Ŀ¼	
                if(!IsHaveSonOrder(m_frmInput.m_objCurrentOrder.m_strOrderID))
                {
                    ret=m_objService.m_lngStopOrder(new clsBIHOrder[]{m_frmInput.m_objCurrentOrder},m_frmInput.m_objCurrentDoctor.m_strDoctorID,m_frmInput.m_objCurrentDoctor.m_strDoctorName);
                }
                else
                {
                    //�ݹ�ֹͣҽ��
                    try
                    {
                        ret=m_objService.m_lngStopOrder(objOrder,m_frmInput.m_objCurrentDoctor.m_strDoctorID,m_frmInput.m_objCurrentDoctor.m_strDoctorName,true);
                    }
                    catch(System.Exception e)
                    {
                        m_mthShowMessage(e.Message);
                        return;
                    }
                }
                #endregion
            }
             */
            long ret = 0;
            bool have_parent = false;//��ǰѡȡ��ҽ���Ƿ��и�ҽ��(true-��,false-��)
            bool first = false;
            int sign = 0;//0,ѡ���˸�ҽ����1��ѡ�����и�ҽ������ҽ��
            if (blnPrompt)
            {
                #region ��ʾ
                //�Ƿ�Ϊ����Ŀ¼	
                if (!IsHaveSonOrder(BihOrder.m_strOrderID))
                {

                    if (BihOrder.m_strParentID != null)
                    {
                        if (!BihOrder.m_strParentID.ToString().Trim().Equals(""))
                        {
                            first = false;
                            if (IsHaveSonOrder(BihOrder.m_strParentID.ToString().Trim()))
                            {
                                have_parent = true;
                                sign = 1;
                            }
                        }
                        else
                        {
                            first = true;
                        }
                    }
                    if (first)
                    {
                        if (MessageBox.Show(m_frmInput, "�Ƿ�ȷ��ֹͣҽ����" + BihOrder.m_strName + " ����", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            ret = (new weCare.Proxy.ProxyIP()).Service.m_lngStopOrder(new clsBIHOrder[] { objOrder }, m_frmInput.m_objCurrentDoctor.m_strDoctorID, m_frmInput.m_objCurrentDoctor.m_strDoctorName, m_dtStopTime);
                        }
                    }
                }
                else
                {
                    have_parent = true;
                }
                if (have_parent)
                {
                    if (sign == 1)
                    {
                        clsBIHOrder objResult = new clsBIHOrder();
                        //clsBIHOrderService m_objManage = new clsBIHOrderService();
                        (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderByOrderID(BihOrder.m_strParentID.ToString().Trim(), out objResult);
                        if (objResult == null)
                        {
                            return;
                        }
                        frmConfirmOrderOperate objfrmConfirmOrderOperate = new frmConfirmOrderOperate("ֹͣ", objResult.m_strOrderID, m_frmInput.IsChildPrice);
                        objfrmConfirmOrderOperate.m_txbPatientName.Text = m_frmInput.m_ctlPatient.m_objPatient.m_strPatientName;
                        objfrmConfirmOrderOperate.m_txbOrderName.Text = objResult.m_strName;

                        objfrmConfirmOrderOperate.ShowDialog();

                        if (objfrmConfirmOrderOperate.m_intResult == 0)
                        {
                            return;
                        }
                        else
                        {
                            //�ݹ�ֹͣҽ��
                            try
                            {
                                ret = (new weCare.Proxy.ProxyIP()).Service.m_lngStopOrder(objResult, m_frmInput.m_objCurrentDoctor.m_strDoctorID, m_frmInput.m_objCurrentDoctor.m_strDoctorName, true, m_dtStopTime, m_frmInput.IsChildPrice);
                            }
                            catch (System.Exception e)
                            {
                                m_mthShowMessage(e.Message);
                                return;
                            }
                        }
                    }
                    else
                    {
                        frmConfirmOrderOperate objfrmConfirmOrderOperate = new frmConfirmOrderOperate("ֹͣ", BihOrder.m_strOrderID, m_frmInput.IsChildPrice);
                        objfrmConfirmOrderOperate.m_txbPatientName.Text = m_frmInput.m_ctlPatient.m_objPatient.m_strPatientName;
                        objfrmConfirmOrderOperate.m_txbOrderName.Text = BihOrder.m_strName;

                        objfrmConfirmOrderOperate.ShowDialog();

                        if (objfrmConfirmOrderOperate.m_intResult == 0)
                        {
                            return;
                        }
                        else
                        {
                            //�ݹ�ֹͣҽ��
                            try
                            {
                                ret = (new weCare.Proxy.ProxyIP()).Service.m_lngStopOrder(BihOrder, m_frmInput.m_objCurrentDoctor.m_strDoctorID, m_frmInput.m_objCurrentDoctor.m_strDoctorName, true, m_dtStopTime, m_frmInput.IsChildPrice);
                            }
                            catch (System.Exception e)
                            {
                                m_mthShowMessage(e.Message);
                                return;
                            }
                        }
                    }
                }
                #endregion
            }

            /*<==============================================================================*/
            if (ret > 0)
            {
                m_mthLoadOrderList();
                //m_mthUpdateOrderByStatus();
                //m_mthShowMessage("�����ɹ���");
            }
            else
            {
                m_mthShowMessage("ֹͣʧ��!");
            }

            m_mthRefreshOtherBillInfo();
        }
        #endregion
        #region ����ҽ��
        /// <summary>
        /// ����ҽ��
        /// ִ��״̬	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;7-�˻�;}
        /// ҵ��˵����	ֻ���״̬��6-���ֹͣ
        /// </summary>
        /// <param name="blnPrompt"></param>
        public void m_mthRetractCurrentOrder(bool blnPrompt)
        {
            if (!m_blnExistCurrentOrder()) return;

            if (m_frmInput.m_objCurrentOrder.m_intStatus != 6)
            {
                m_mthShowMessage("ֻ�����������ֹͣ��״̬��ҽ����");
                return;

            }
            long ret = 0;
            if (blnPrompt)
            {
                #region ��ʾ
                if (!IsHaveSonOrder(m_frmInput.m_objCurrentOrder.m_strOrderID))
                {
                    if (MessageBox.Show(this.m_frmInput, "�Ƿ�����ҽ��: " + m_frmInput.m_objCurrentOrder.m_strName + " ?", "����ҽ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        ret = (new weCare.Proxy.ProxyIP()).Service.m_lngRetractOrder(new clsBIHOrder[] { m_frmInput.m_objCurrentOrder }, m_frmInput.m_objCurrentDoctor.m_strDoctorID, m_frmInput.m_objCurrentDoctor.m_strDoctorName);
                    }
                }
                else
                {
                    frmConfirmOrderOperate objfrmConfirmOrderOperate = new frmConfirmOrderOperate("����", m_frmInput.m_objCurrentOrder.m_strOrderID, m_frmInput.IsChildPrice);
                    objfrmConfirmOrderOperate.m_txbPatientName.Text = m_frmInput.m_ctlPatient.m_objPatient.m_strPatientName;
                    objfrmConfirmOrderOperate.m_txbOrderName.Text = m_frmInput.m_objCurrentOrder.m_strName;
                    objfrmConfirmOrderOperate.ShowDialog();
                    if (objfrmConfirmOrderOperate.m_intResult == 0)
                    {
                        return;
                    }
                    else
                    {
                        //�ݹ�����ҽ��
                        try
                        {
                            ret = (new weCare.Proxy.ProxyIP()).Service.m_lngRetractOrder(m_frmInput.m_objCurrentOrder, m_frmInput.m_objCurrentDoctor.m_strDoctorID, m_frmInput.m_objCurrentDoctor.m_strDoctorName, true, m_frmInput.IsChildPrice);
                        }
                        catch (System.Exception e)
                        {
                            m_mthShowMessage(e.Message);
                            return;
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region ����ʾ
                if (!IsHaveSonOrder(m_frmInput.m_objCurrentOrder.m_strOrderID))
                {
                    ret = (new weCare.Proxy.ProxyIP()).Service.m_lngRetractOrder(new clsBIHOrder[] { m_frmInput.m_objCurrentOrder }, m_frmInput.m_objCurrentDoctor.m_strDoctorID, m_frmInput.m_objCurrentDoctor.m_strDoctorName);
                }
                else
                {
                    //�ݹ�����ҽ��
                    try
                    {
                        ret = (new weCare.Proxy.ProxyIP()).Service.m_lngRetractOrder(m_frmInput.m_objCurrentOrder, m_frmInput.m_objCurrentDoctor.m_strDoctorID, m_frmInput.m_objCurrentDoctor.m_strDoctorName, true, m_frmInput.IsChildPrice);
                    }
                    catch (System.Exception e)
                    {
                        m_mthShowMessage(e.Message);
                        return;
                    }
                }
                #endregion
            }

            //����������
            if (ret > 0)
            {
                //m_mthUpdateOrderByStatus();
                m_mthLoadOrderList();
                //m_mthShowMessage("�����ɹ���");
            }
            else
            {
                m_mthShowMessage("����ʧ��!");
            }
            m_mthRefreshOtherBillInfo();
        }
        #endregion
        #region ����ҽ��
        /// <summary>
        /// ���ϵ�ǰҽ��	
        /// ִ��״̬	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;7-�˻�;}
        /// ҵ��˵����	ֻ���״̬��1-�ύ;
        /// </summary>
        /// <param name="blnPrompt"></param>
        public void m_mthBlankOutCurrentOrder(bool blnPrompt, clsBIHOrder BihOrder)
        {
            if (m_frmInput.m_blBlankOutControl == false) return;
            if (BihOrder.m_intExecuteType == 1)
            {
                if (BihOrder.m_intStatus == 3 || BihOrder.m_intStatus == 6)
                {
                    if (MessageBox.Show("��ҽ���Ѿ�ִ�У��Ƿ�ǿ������?", "���棡", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    m_mthShowMessage("����ֻ����ֹͣ���ܽ�������!");
                    return;
                }
            }
            else
            {
                if (BihOrder.m_intStatus == 2)
                {
                    if (MessageBox.Show("��ҽ���Ѿ�ִ�У��Ƿ�ǿ������?", "���棡", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    m_mthShowMessage("ֻ��ִ�й���ҽ�����ܽ�������!");
                    return;
                }
            }
            //���ڸ��ӵ��ݵĲ�������
            if (m_objInputOrder.m_blnExistAttchOrder(BihOrder.m_strOrderID))
            {
                //MessageBox.Show(m_frmInput,"��ҽ���и��ӵ��ݣ�����ɾ�����ӵ��ݣ�","���棡",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            long ret = 0;
            if (blnPrompt)
            {
                #region ��ʾ
                if (!IsHaveSonOrder(BihOrder.m_strOrderID))
                {
                    //if(MessageBox.Show(m_frmInput,"�Ƿ�����ҽ��: " + BihOrder.m_strName + " ?","����ҽ��",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
                    //{
                    //    return;
                    //}
                    //else
                    //{
                    ret = (new weCare.Proxy.ProxyIP()).Service.m_lngBlankOutOrder(new string[] { BihOrder.m_strOrderID }, this.m_frmInput.LoginInfo.m_strEmpID, this.m_frmInput.LoginInfo.m_strEmpName);
                    //}
                }
                else
                {
                    frmConfirmOrderOperate objfrmConfirmOrderOperate = new frmConfirmOrderOperate("����", BihOrder.m_strOrderID, m_frmInput.IsChildPrice);
                    objfrmConfirmOrderOperate.m_txbPatientName.Text = m_frmInput.m_ctlPatient.m_objPatient.m_strPatientName;
                    objfrmConfirmOrderOperate.m_txbOrderName.Text = BihOrder.m_strName;
                    objfrmConfirmOrderOperate.ShowDialog();
                    if (objfrmConfirmOrderOperate.m_intResult == 0)
                    {
                        return;
                    }
                    else
                    {
                        //�ݹ�����ҽ��
                        try
                        {
                            ret = (new weCare.Proxy.ProxyIP()).Service.m_lngBlankOutOrder(BihOrder.m_strOrderID, true, this.m_frmInput.LoginInfo.m_strEmpID, this.m_frmInput.LoginInfo.m_strEmpName, m_frmInput.IsChildPrice);
                        }
                        catch (System.Exception e)
                        {
                            m_mthShowMessage(e.Message);
                            return;
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region ����ʾ
                if (!IsHaveSonOrder(BihOrder.m_strOrderID))
                {
                    ret = (new weCare.Proxy.ProxyIP()).Service.m_lngBlankOutOrder(new string[] { BihOrder.m_strOrderID }, this.m_frmInput.LoginInfo.m_strEmpID, this.m_frmInput.LoginInfo.m_strEmpName);
                }
                else
                {
                    //�ݹ�����ҽ��
                    try
                    {
                        ret = (new weCare.Proxy.ProxyIP()).Service.m_lngBlankOutOrder(BihOrder.m_strOrderID, true, this.m_frmInput.LoginInfo.m_strEmpID, this.m_frmInput.LoginInfo.m_strEmpName, m_frmInput.IsChildPrice);
                    }
                    catch (System.Exception e)
                    {
                        m_mthShowMessage(e.Message);
                        return;
                    }
                }
                #endregion
            }

            //����������
            if (ret > 0)
            {
                //m_mthShowMessage("�����ɹ���");
                BihOrder.m_intStatus = -1;
                m_mthLoadOrderList();
                //m_mthUpdateOrderByStatus();
            }
            else
            {
                m_mthShowMessage("����ҽ��ʧ��!");
            }
            m_mthRefreshOtherBillInfo();
        }
        #endregion

        #region ɾ��ҽ��
        /// <summary>
        /// ɾ����ǰҽ��	
        /// ִ��״̬	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;7-�˻�;}
        /// ҵ��˵����	ֻ���״̬��0-����;
        /// </summary>
        /// <param name="blnPrompt"></param>
        public void m_mthDeleteCurrentOrder(bool blnPrompt, clsBIHOrder BihOrder)
        {
            if (BihOrder.m_intStatus != 0 && BihOrder.m_intStatus != 7 && BihOrder.m_intStatus != 1 && BihOrder.m_intStatus != 5)
            {
                MessageBox.Show(" {��ǰҽ����" + BihOrder.m_strName + "} " + "ֻ���¿����˻أ��ύ����ת������״̬��ҽ������ɾ��ҽ����", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (BihOrder.m_intStatus == 0)
            {
                if (MessageBox.Show(" {��ǰҽ����" + BihOrder.m_strName + "} " + "��ҽ�����¿���ҽ�����Ƿ�ɾ��?", "���棡", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                }
                else
                {
                    return;
                }
            }
            if (BihOrder.m_intStatus == 7)
            {
                if (MessageBox.Show(" {��ǰҽ����" + BihOrder.m_strName + "} " + "��ҽ�����˻ص�ҽ�����Ƿ�ɾ��?", "���棡", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                }
                else
                {
                    return;
                }
            }
            if (BihOrder.m_intStatus == 1)
            {
                if (MessageBox.Show(" {��ǰҽ����" + BihOrder.m_strName + "} " + "��ҽ�������ύ��ҽ�����Ƿ�ɾ��?", "���棡", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                }
                else
                {
                    return;
                }
            }
            if (BihOrder.m_intStatus == 5)
            {
                if (MessageBox.Show(" {��ǰҽ����" + BihOrder.m_strName + "} " + "��ҽ������ת����ҽ�����Ƿ�ɾ��?", "���棡", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                }
                else
                {
                    return;
                }
            }
            //���ڸ��ӵ��ݵĲ���ɾ��
            if (m_objInputOrder.m_blnExistAttchOrder(BihOrder.m_strOrderID))
            {
                if (MessageBox.Show(" {��ǰҽ����" + BihOrder.m_strName + "} " + "��ҽ���и��ӵ��ݣ�����ɾ�����ӵ��ݣ�", "���棡", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                }
                else
                {
                    return;
                }
            }
            long ret = 0;
            if (blnPrompt)
            {
                #region ��ʾ
                if (!IsHaveSonOrder(BihOrder.m_strOrderID))
                {
                    ret = (new weCare.Proxy.ProxyIP()).Service.m_lngDeleteOrder(new string[] { BihOrder.m_strOrderID });
                }
                else
                {
                    frmConfirmOrderOperate objfrmConfirmOrderOperate = new frmConfirmOrderOperate("ɾ��", BihOrder.m_strOrderID, m_frmInput.IsChildPrice);
                    objfrmConfirmOrderOperate.m_txbPatientName.Text = m_frmInput.m_ctlPatient.m_objPatient.m_strPatientName;
                    objfrmConfirmOrderOperate.m_txbOrderName.Text = BihOrder.m_strName;
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
                            ret = (new weCare.Proxy.ProxyIP()).Service.m_lngDeleteOrder(BihOrder.m_strOrderID, true);
                        }
                        catch (System.Exception e)
                        {
                            m_mthShowMessage(e.Message);
                            return;
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region ����ʾ
                if (!IsHaveSonOrder(BihOrder.m_strOrderID))
                {
                    ret = (new weCare.Proxy.ProxyIP()).Service.m_lngBlankOutOrder(new string[] { BihOrder.m_strOrderID }, this.m_frmInput.LoginInfo.m_strEmpID, this.m_frmInput.LoginInfo.m_strEmpName);
                }
                else
                {
                    //�ݹ�ɾ��ҽ��
                    try
                    {
                        ret = (new weCare.Proxy.ProxyIP()).Service.m_lngBlankOutOrder(BihOrder.m_strOrderID, true, this.m_frmInput.LoginInfo.m_strEmpID, this.m_frmInput.LoginInfo.m_strEmpName, m_frmInput.IsChildPrice);
                    }
                    catch (System.Exception e)
                    {
                        m_mthShowMessage(e.Message);
                        return;
                    }
                }
                #endregion
            }

            //����������
            if (ret > 0)
            {
                //m_mthShowMessage("�����ɹ���");
                m_mthLoadOrderList();
                //m_mthUpdateOrderByStatus();
            }
            else
            {
                m_mthShowMessage("ɾ��ҽ��ʧ��!");
            }
            m_mthRefreshOtherBillInfo();
        }
        #endregion

        /// <summary>
        /// ���ӵ�ǰҽ����������
        /// </summary>
        /// <returns></returns>
        public long m_lngAddCurrentOrderToClipBoard()
        {
            try
            {
                if (!m_blnExistCurrentOrder()) return -1;
                if (m_frmInput.m_objCurrentOrder.m_strParentID != null && m_frmInput.m_objCurrentOrder.m_strParentID.Trim() != "")
                {
                    Clipboard.SetDataObject(m_frmInput.m_objCurrentOrder, false);
                }
                return 0;
            }
            catch
            {
                return -1;
            }
        }


        public long m_lngIfIsGroupOrder()
        {
            try
            {
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region ����ɾ����ҽ��
        /// <summary>
        /// ����ҽ����Ϣ
        /// </summary>
        public void m_mthSave()
        {
            clsBIHPatientInfo objPatient = m_frmInput.m_ctlPatient.m_objPatient;
            if (objPatient == null)
            {
                m_mthShowMessage("����ָ������!");
                m_frmInput.m_ctlPatient.Focus();
                return;
            }

            long lngRes = -1;
            if (m_frmInput.m_objCurrentOrder == null)
            {
                #region ����
                if (m_frmInput.m_ctlOrderDetail.CurrentItemIsGroup)
                {
                    #region ����
                    clsBIHOrder[] arrOrder = m_frmInput.m_ctlOrderDetail.m_objGetOrderGroup(objPatient);
                    if ((arrOrder == null) || (arrOrder.Length <= 0)) return;

                    //����Ƿ񷽺�һ��
                    bool blnIsSameNO = false;
                    string strGroupID = "";
                    string[] strRecordIDArr;
                    //clsBIHOrderGroupService objTem =new clsBIHOrderGroupService();
                    //clsBIHOrderGroupService objTem = new clsDcl_GetSvcObject().m_GetOrderGroupSvcObject();
                    try
                    {
                        strGroupID = ((clsBIHOrderGroup)m_frmInput.m_ctlOrderDetail.m_txtOrderName.Tag).m_strGroupID;
                    }
                    catch { }
                    if (strGroupID == string.Empty)
                    {
                        m_mthShowMessage("����ҽ��ʧ�ܣ�");
                        return;
                    }
                    lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngCheckIsSameNOByGroupID(strGroupID, out blnIsSameNO);
                    if (lngRes > 0)
                    {
                        try
                        {
                            #region Add by jli in 2005-04-28 �������������ҽ��
                            if (arrOrder[0].m_strExecFreqID.Trim() == new clsDcl_ExecuteOrder().m_strGetConfreqID().Trim())
                            {
                                for (int i = 0; i < arrOrder.Length; i++)
                                {
                                    if (arrOrder[i].m_dtStartDate == DateTime.MinValue)
                                    {
                                        arrOrder[i].m_dtStartDate = DateTime.Now;
                                    }
                                }
                            }
                            #endregion
                            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewOrder(out strRecordIDArr, arrOrder, blnIsSameNO, m_frmInput.IsChildPrice);
                        }
                        catch (System.Exception ex)
                        {
                            m_mthShowMessage(ex.Message);
                            return;
                        }
                    }
                    if (lngRes > 0)
                    {
                        //m_mthShowMessage("����ɹ���");
                        //ˢ������
                        m_frmInput.Cursor = Cursors.WaitCursor;
                        m_mthLoadOrderList();
                        m_frmInput.Cursor = Cursors.Default;
                    }
                    else
                    {
                        m_mthShowMessage("����ҽ��ʧ�ܣ�");
                    }
                    #endregion //end ����
                }
                else
                {
                    #region ������Ŀ
                    clsBIHOrder objOrder = m_frmInput.m_objGetOrder(null);
                    if (objOrder == null) return;
                    //��֤��������
                    if (!ValidateInput(objOrder)) return;

                    if (m_blnAddNew(objOrder))
                    {
                    }
                    else
                    {
                        m_mthShowMessage("����ʧ��!");
                    }
                    #endregion
                }
                #endregion
            }
            else if (m_frmInput.m_objCurrentOrder.m_strParentID.Trim() != ""
                && m_frmInput.m_objCurrentOrder.m_strOrderID.Trim() == ""
                && (m_frmInput.m_objCurrentOrder.m_intStatus == 0
                    || m_frmInput.m_objCurrentOrder.m_intStatus == 7))
            {
                #region ����
                if (m_frmInput.m_ctlOrderDetail.CurrentItemIsGroup)
                {
                    #region ����
                    clsBIHOrder[] arrOrder = m_frmInput.m_ctlOrderDetail.m_objGetOrderGroup(objPatient);
                    if ((arrOrder == null) || (arrOrder.Length <= 0)) return;

                    //����Ƿ񷽺�һ��
                    bool blnIsSameNO = false;
                    string strGroupID = "";
                    string[] strRecordIDArr;
                    //clsBIHOrderGroupService objTem = new clsDcl_GetSvcObject().m_GetOrderGroupSvcObject();
                    try
                    {
                        strGroupID = ((clsBIHOrderGroup)m_frmInput.m_ctlOrderDetail.m_txtOrderName.Tag).m_strGroupID;
                    }
                    catch { }
                    if (strGroupID == string.Empty)
                    {
                        m_mthShowMessage("����ҽ��ʧ�ܣ�");
                        return;
                    }
                    lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngCheckIsSameNOByGroupID(strGroupID, out blnIsSameNO);
                    if (lngRes > 0)
                    {
                        try
                        {
                            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewOrder(out strRecordIDArr, arrOrder, blnIsSameNO, m_frmInput.IsChildPrice);
                        }
                        catch (System.Exception ex)
                        {
                            m_mthShowMessage(ex.Message);
                            return;
                        }
                    }
                    if (lngRes > 0)
                    {
                        //m_mthShowMessage("����ɹ���");
                        //ˢ������
                        m_frmInput.Cursor = Cursors.WaitCursor;
                        m_mthLoadOrderList();
                        m_frmInput.Cursor = Cursors.Default;
                    }
                    else
                    {
                        m_mthShowMessage("����ҽ��ʧ�ܣ�");
                    }
                    #endregion //end ����
                }
                else
                {
                    #region ������Ŀ
                    clsBIHOrder objOrder = m_frmInput.m_objGetOrder(null);
                    if (objOrder == null) return;
                    //��֤��������
                    if (!ValidateInput(objOrder)) return;

                    if (m_blnAddNew(objOrder))
                    {
                        //m_frmInput.m_objCurrentOrder=objOrder;
                        //m_frmInput.m_arlOrder.Add(objOrder);
                        //m_frmInput.m_dtgOrder.m_mthAppendRow(m_objGetDataRow(m_frmInput.m_arlOrder.Count,objOrder,null));
                        //m_frmInput.m_dtgOrder.CurrentCell=new DataGridCell(m_frmInput.m_arlOrder.Count-1,0);

                        //m_mthUpdateOrderByStatus();

                        //m_frmInput.m_cmdAdd_Click(null,null);
                        //ˢ��

                        //Modify by jli in 2004-05-06
                        //						m_frmInput.Cursor =Cursors.WaitCursor;
                        //						m_mthLoadOrderList();
                        //						m_frmInput.Cursor =Cursors.Default;

                        //Modify End

                    }
                    else
                    {
                        m_mthShowMessage("����ʧ��!");
                    }
                    #endregion //end ����
                }
                #endregion
            }
            else if (m_frmInput.m_objCurrentOrder != null && m_frmInput.m_objCurrentOrder.m_strOrderID != null && (m_frmInput.m_objCurrentOrder.m_intStatus == 0 || m_frmInput.m_objCurrentOrder.m_intStatus == 7))
            {
                #region �޸�
                clsBIHOrder objOrder = m_frmInput.m_objGetOrder(m_frmInput.m_objCurrentOrder);
                if (objOrder.m_strCreatorID.Trim() != m_frmInput.LoginInfo.m_strEmpID.Trim())
                {
                    MessageBox.Show(m_frmInput, "ֻ���޸��Լ�������ҽ����", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //��֤��������
                int intIsFather = 0;//�Ƿ�Ϊ����ҽ�� {0=���Ǹ���ҽ����1=�Ǹ���ҽ��}
                if (!ValidateInput(objOrder, out intIsFather)) return;
                long ret = 0;
                if (intIsFather == 0)
                    ret = m_objInputOrder.m_lngModifyOrder(objOrder);
                else
                    ret = m_objInputOrder.m_lngModifyOrderWithSon(objOrder);
                if (ret > 0)
                {
                    if (intIsFather == 0)
                    { }
                    else
                    {
                        m_mthLoadOrderList();
                    }
                }
                else
                {
                    m_mthShowMessage("����ʧ��!");
                }
                #endregion
            }
            else if (m_frmInput.m_objCurrentOrder.m_intStatus == 2)//ִ��״̬
            {
                #region �޸�������ҽ����ֹͣʱ��
                clsBIHOrder objOrder = m_frmInput.m_objGetOrder(m_frmInput.m_objCurrentOrder);
                if (m_strConfreqID.Trim() == "") m_strConfreqID = new clsDcl_ExecuteOrder().m_strGetConfreqID();
                if (objOrder.m_strExecFreqID.Trim() == m_strConfreqID.Trim())
                {
                    if (objOrder.m_dtStopdate > objOrder.m_dtStartDate)
                    {
                        long ret = m_objInputOrder.m_lngFillConOrderStopTime(objOrder.m_strOrderID, objOrder.m_dtStopdate);
                        m_mthLoadOrderList();
                        return;
                    }
                    else
                    {
                        m_mthShowMessage("ֹͣʱ������ڿ�ʼ֮��!");
                    }
                }
                else
                {
                    m_mthShowMessage("ҽ��״̬��ʾ: ���ܸ���!");
                }
                #endregion
            }
            else
            {
                m_mthShowMessage("ҽ��״̬��ʾ: ���ܸ���!");
            }
            //ˢ�»������Ϣ	{����}
            if (m_frmInput.m_objCurrentOrder != null && m_frmInput.m_objCurrentOrder.m_strOrderID != null)
            {
                if (m_htbToolTip.Contains(m_frmInput.m_objCurrentOrder.m_strOrderID))
                {
                    m_htbToolTip.Remove(m_frmInput.m_objCurrentOrder.m_strOrderID);
                }
                if (m_frmInput.m_lsvToolTip.Visible)
                {
                    m_DisPlayToolTipListView(m_frmInput.m_objCurrentOrder, m_frmInput.m_lsvToolTip);
                    m_frmInput.m_lsvToolTip.Refresh();
                }
            }
            m_mthRefreshOtherBillInfo();
            m_frmInput.m_ctlOrderDetail.m_mthStartInput();
        }

        /// <summary>
        /// ����ҽ����Ϣ���ر���״̬  p_intResult(1-����ҽ��,2-�޸�ҽ��)
        /// </summary>
        public void m_mthSave(out int p_intResult, ref ArrayList m_arrOrderId)
        {
            p_intResult = 0;
            clsBIHPatientInfo objPatient = m_frmInput.m_ctlPatient.m_objPatient;
            if (objPatient == null)
            {
                m_mthShowMessage("����ָ������!");
                m_frmInput.m_ctlPatient.Focus();
                return;
            }
            //clsBIHOrderGroupService objTem = new clsDcl_GetSvcObject().m_GetOrderGroupSvcObject();
            /*  �ж��Ƿ��д󴦷�*/
            if (m_frmInput.m_blUpControl == true)
            {
                int MaxValue = 0;//��ǰ�û�Ȩ�޵�ҩƷ��������
                MaxValue = getMaxValue();//ȡ��ǰ�û�Ȩ�޵�ҩƷ��������
                bool m_blPass = true;
                m_blPass = CheckTheUp(MaxValue);
                if (!m_blPass)
                {
                    p_intResult = -1;
                    return;
                }
            }
            if (m_frmInput.m_objCurrentOrder == null)
            {
                string[] m_arrPartID = null;
                string[] m_arrPartName = null;
                if (m_frmInput.m_ctlOrderDetail.m_txtCheck.Tag != null)
                {
                    m_arrPartID = ((string)m_frmInput.m_ctlOrderDetail.m_txtCheck.Tag).Split(",".ToCharArray());
                    m_arrPartName = m_frmInput.m_ctlOrderDetail.m_txtCheck.Text.Trim().Split(",".ToCharArray());
                }
                if (m_arrPartID == null || m_arrPartID.Length <= 1)//�Ƕಿλ������һ��ҽ��
                {
                    #region ����ҽ����������״̬�£�����ҽ��,�Ƕಿλ������һ��ҽ����
                    clsBIHOrder objOrder = m_frmInput.m_objGetOrder(null);
                    if (objOrder == null)
                    {
                        return;
                    }
                    lngAddNewNOSubOrder(ref objOrder);
                    m_arrOrderId.Add(objOrder.m_strOrderID);
                    #endregion
                }
                else//�ಿλ�����ɶ���ҽ��
                {
                    for (int i = 0; i < m_arrPartID.Length; i++)
                    {
                        #region ����ҽ����������״̬�£�����ҽ��,�ಿλ�����ɶ���ҽ����
                        clsBIHOrder objOrder = m_frmInput.m_objGetOrder(null);
                        if (objOrder == null)
                        {
                            return;
                        }

                        objOrder.m_strPARTID_VCHR = m_arrPartID[i];
                        if (m_arrPartName.Length > i)
                        {
                            objOrder.m_strPARTNAME_VCHR = m_arrPartName[i];
                        }
                        lngAddNewNOSubOrder(ref objOrder);
                        m_arrOrderId.Add(objOrder.m_strOrderID);
                        #endregion
                    }
                }
            }
            else if (m_frmInput.m_objCurrentOrder.m_strParentID.Trim() != "" && m_frmInput.m_objCurrentOrder.m_strOrderID.Trim() == "" && (m_frmInput.m_objCurrentOrder.m_intStatus == 0 || m_frmInput.m_objCurrentOrder.m_intStatus == 7))
            {
                #region ����ҽ����������״̬�£�������ҽ����
                clsBIHOrder objOrder = m_frmInput.m_objGetOrder(null);
                if (objOrder == null)
                {
                    return;
                }
                #region ���ʹ����ж�

                for (int i = 0; i < this.m_frmInput.m_dtvOrder.RowCount; i++)
                {
                    clsBIHOrder order = (clsBIHOrder)this.m_frmInput.m_dtvOrder.Rows[i].Tag;
                    if (order.m_intRecipenNo == objOrder.m_intRecipenNo)
                    {
                        if (order.IsProxyBoilMed != objOrder.IsProxyBoilMed)
                        {
                            string bmedInfo = string.Empty;
                            if (objOrder.IsProxyBoilMed == 0)
                                bmedInfo = "������ҩ��";
                            else if (objOrder.IsProxyBoilMed == 1)
                                bmedInfo = "�������";
                            else if (objOrder.IsProxyBoilMed == 2)
                                bmedInfo = "��ҩ����";
                            DialogResult dialog = MessageBox.Show("��ҽ���ġ�Ժ����͡���������ҽ����һ�£��Ƿ�������棿" + Environment.NewLine + Environment.NewLine + "ѡ���ǡ�ͬ��ҽ����������Ϊ--" + bmedInfo + ", ѡ�񡾷񡿽������淵������¼��ҽ����", "��ע��", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dialog == DialogResult.Yes)
                            {
                                break;
                            }
                            else if (dialog == DialogResult.No)
                            {
                                return;
                            }
                        }
                    }
                }
                #endregion
                ////��֤��������
                //if (!ValidateInput(objOrder))
                //{
                //    p_intResult = -1;
                //    return;
                //}
                lngAddNewSubOrder(ref objOrder);
                m_arrOrderId.Add(objOrder.m_strOrderID);
                #endregion
            }
            else if (m_frmInput.m_objCurrentOrder != null && m_frmInput.m_objCurrentOrder.m_strOrderID != null && (m_frmInput.m_objCurrentOrder.m_intStatus == 0 || m_frmInput.m_objCurrentOrder.m_intStatus == 1 || m_frmInput.m_objCurrentOrder.m_intStatus == 5 || m_frmInput.m_objCurrentOrder.m_intStatus == 7 || (m_frmInput.m_objCurrentOrder.m_intStatus == 2 && m_frmInput.m_objCurrentOrder.m_intExecuteType == 1)))
            {
                #region �޸�ҽ��
                clsBIHOrder objOrder = m_frmInput.m_objCurrentOrder;
                m_frmInput.m_objGetChangedOrder(ref objOrder);
                lngChangedOrder(ref objOrder);
                m_arrOrderId.Add(objOrder.m_strOrderID);
                //��֤��������
                //int intIsFather = 0;//�Ƿ�Ϊ����ҽ�� {0=���Ǹ���ҽ����1=�Ǹ���ҽ��}
                //if (!ValidateInput(objOrder, out intIsFather))
                //{
                //    p_intResult = -8;
                //    return;
                //}

                /*<===================================*/
                //long ret = 0;
                //if (intIsFather == 0)//����ҽ�����޸�
                //{
                //    //���ҽ�������Ƿ���ڵ�ǰҽ���б��У��粻���ھ���ҽ������Ϊ���������е�ǰҽ�����������ŵ��޸Ĳ���
                //    SetOrderByRecipenNo(objOrder);
                //    if (objOrder.m_intRecipenNo == 0)//���跽�� ����ҽ�����͵�ת��ҽ�����޸�
                //    {
                //        ret = m_objInputOrder.m_lngModifyNewRecipenNoOrder(objOrder);
                //    }
                //    else
                //    {
                //        if (CheckTheSubOrder(objOrder))
                //        {
                //            //���ڸ�ҽ��ʱ������ҽ�����޸�
                //            ret = m_objInputOrder.m_lngModifyCurrentSubOrder(objOrder);
                //        }
                //        else
                //        {
                //            //һ������µ��޸�(���޸ķ��ţ�
                //           ret = m_objInputOrder.m_lngModifyOrder(objOrder);
                //        }
                //    }
                //}
                //else//����ҽ�����޸�(����ҽ�����޸�)
                //{
                //    ret = m_objInputOrder.m_lngModifyOrderWithSon(objOrder);
                //}


                #endregion //end �޸�
            }

            m_frmInput.m_objCurrentOrder = null;//���õ�ǰ ҽ������
            m_mthRefreshOtherBillInfo();
            m_frmInput.m_ctlOrderDetail.m_mthStartInput();

            /*ת��ҽ�����뽹��*/
            m_frmInput.m_ctlOrderDetail.m_txtOrderName.Focus();
        }

        /// <summary>
        /// //ˢ�»������Ϣ	{����}
        /// </summary>
        /// <param name="m_arrChangeOrderID">��ˢ�µ�ҽ����ˮ��</param>
        public void lngRefreshChargePool(ArrayList m_arrChangeOrderID)
        {
            //ˢ�»������Ϣ	{����}
            for (int i = 0; i < m_arrChangeOrderID.Count; i++)
            {
                m_htbToolTip.Remove(m_arrChangeOrderID[i].ToString());
            }
            if (m_frmInput.m_lsvToolTip.Visible == true)
            {
                m_frmInput.m_dtvOrder_CellMouseClick(null, null);
            }
            /*<==============================*/
        }

        #region ҽ��������/�޸�
        /// <summary>
        /// ����ҽ��(��������ҽ��)
        /// </summary>
        /// <param name="objOrder"></param>
        public void lngAddNewNOSubOrder(ref clsBIHOrder objOrder)
        {
            //����ҽ���������ֶ� ��������Ƥ�ԣ������޸ı�־
            this.m_frmInput.m_ctlOrderDetail.SetTheOrderSpecial(ref objOrder);
            //�ж���ȡҩ�������Ƿ���ڿ����
            if (m_blChcekOpcurrentgross(ref objOrder) == false)
            {
                this.m_frmInput.m_ctlOrderDetail.m_txtOrderName.Focus();
                return;
            }
            if (m_blnAddNew(objOrder))
            {
            }

        }
        /// <summary>
        /// �ж���ȡҩ�������Ƿ���ڿ����
        /// </summary>
        /// <param name="p_objOrder"></param>
        /// <returns>true ҩƷ����� false ҩƷ��治��</returns>
        private bool m_blChcekOpcurrentgross(clsBIHOrder p_objOrder)
        {
            bool bl = true;
            //ҽ����ҩ������һ�������벹��������
            float fltUseMediconeCount = Convert.ToSingle(p_objOrder.m_dmlGet + p_objOrder.m_intATTACHTIMES_INT * p_objOrder.m_dmlOneUse);
            if (p_objOrder.m_intIPCHARGEFLG_INT == 0)
            {
                fltUseMediconeCount = fltUseMediconeCount * Convert.ToSingle(p_objOrder.m_dmlPACKQTY_DEC);
            }
            if (p_objOrder.m_strOrderDicCateID == this.m_frmInput.m_objSpecateVo.m_strORDERCATEID_MEDICINE_CHR || p_objOrder.m_strOrderDicCateID == this.m_frmInput.m_objSpecateVo.m_strMID_MEDICINE_CHR)
            {
                //m_blIsChangeΪtrueʱ�����޸�ҽ����� falseΪ�¼�ҽ�����
                if (this.m_frmInput.m_blIsChange == true)
                {
                    if ((this.m_frmInput.m_fotOpcurrentgross_num < fltUseMediconeCount) && this.m_frmInput.m_intITEMSRCTYPE_INT == 1)
                    {
                        MessageBox.Show(p_objOrder.m_strName + "��治�㣬���ܱ���ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bl = false;
                    }
                    this.m_frmInput.m_blIsChange = false;
                    this.m_frmInput.m_fotOpcurrentgross_num = 0;
                    this.m_frmInput.m_intITEMSRCTYPE_INT = 0;
                }
                else
                {
                    if ((this.m_frmInput.m_ctlOrderDetail.m_fotOpcurrentgross_num < fltUseMediconeCount) && this.m_frmInput.m_ctlOrderDetail.m_intITEMSRCTYPE_INT == 1)
                    {
                        MessageBox.Show(p_objOrder.m_strName + "��治�㣬���ܱ���ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bl = false;
                    }
                }
            }
            return bl;
        }

        /// <summary>
        /// �ж���ȡҩ�������Ƿ���ڿ����
        /// </summary>
        /// <param name="p_objOrder"></param>
        /// <returns>true ҩƷ����� false ҩƷ��治��</returns>
        internal bool m_blChcekOpcurrentgross(ref clsBIHOrder p_objOrder)
        {
            bool bl = true;
            //ҽ����ҩ������һ�������벹��������
            decimal fltUseMediconeCount = Convert.ToDecimal(p_objOrder.m_dmlGet + p_objOrder.m_dmlOneUse * p_objOrder.m_intATTACHTIMES_INT);
            // �Ƴ���ҩ
            if (p_objOrder.CureDays > 0) fltUseMediconeCount += p_objOrder.m_dmlGet * p_objOrder.CureDays;

            if (p_objOrder.m_intIPCHARGEFLG_INT == 0)
            {
                fltUseMediconeCount = fltUseMediconeCount * p_objOrder.m_dmlPACKQTY_DEC;
            }
            string m_strMedStore = "";
            //����ҽ�������ж�ҩ��ID
            if (p_objOrder.m_strOrderDicCateID == this.m_frmInput.m_objSpecateVo.m_strORDERCATEID_MEDICINE_CHR)
            {
                m_strMedStore = this.m_frmInput.m_strMedCineStorgeId;
            }
            else if (p_objOrder.m_strOrderDicCateID == this.m_frmInput.m_objSpecateVo.m_strMID_MEDICINE_CHR)
            {
                m_strMedStore = this.m_frmInput.m_strMidMedCineSorgeId;
            }
            string m_strExecDeptID = "";
            if (!string.IsNullOrEmpty(m_strMedStore))
            {
                if (p_objOrder.RateType == 0 && this.m_frmInput.m_ctlOrderDetail.cboProxyBoil.SelectedIndex == 0)
                {
                    long l = -1;
                    string medCode = string.Empty;
                    l = this.m_lngGetMedStoreByDoctorWorkStation(m_strMedStore, p_objOrder.m_strOrderDicID, fltUseMediconeCount, out bl, out m_strExecDeptID, out medCode);

                    if (l < 0 || string.IsNullOrEmpty(m_strExecDeptID))
                    {
                        MessageBox.Show("ҩ��û��ҩƷ(" + medCode + " " + p_objOrder.m_strName + ")�Ŀ��ÿ�棬������ѡ��", "��ʾ��");
                        return false;
                    }

                    if (bl == false)
                    {
                        MessageBox.Show(medCode + " " + p_objOrder.m_strName + "\r\n��治�㣬���ܱ���ҽ����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }

            // bl = this.m_blnCheckOrderSubItemGross(p_objOrder);//�ж��÷������͸�����Ŀ
            return bl;
        }

        /// <summary>
        ///  ����ҽ��(������ҽ��)
        /// </summary>
        /// <param name="objOrder"></param>
        /// <param name="m_arrChangeOrderID">���û���ˢ����Ӱ�����ˮ��</param>
        public void lngAddNewSubOrder(ref clsBIHOrder objOrder)
        {
            //���û���ˢ����Ӱ�����ˮ��
            ArrayList m_arrChangeOrderID = new ArrayList();
            //����ҽ���������ֶ� ��������Ƥ�ԣ������޸ı�־
            this.m_frmInput.m_ctlOrderDetail.SetTheOrderSpecial(ref objOrder);
            //�ж���ȡҩ�������Ƿ���ڿ����
            if (m_blChcekOpcurrentgross(ref objOrder) == false)
            {
                this.m_frmInput.m_ctlOrderDetail.m_txtOrderName.Focus();
                return;
            }
            //���Ҫ�޸ĵ���Ӧͬ��ҽ��
            ArrayList m_arrChangeList = getChangeListWithSonAdd(objOrder);
            clsBIHOrder[] arrOrder = null;
            if (m_arrChangeList.Count > 0)
            {
                arrOrder = (clsBIHOrder[])(m_arrChangeList.ToArray(typeof(clsBIHOrder)));
            }
            //����ͬ��ҽ����Ƥ��
            SetTheGroupFeel(ref objOrder, ref arrOrder);
            /*<=======================================*/
            if (m_blnAddNew(objOrder, arrOrder))
            {
                if (arrOrder != null && arrOrder.Length > 0)
                {
                    for (int i = 0; i < arrOrder.Length; i++)
                    {
                        m_arrChangeOrderID.Add(arrOrder[i].m_strOrderID);
                    }
                }

            }
            lngRefreshChargePool(m_arrChangeOrderID);

        }

        /// <summary>
        ///  �޸�ҽ��
        /// </summary>
        /// <param name="objOrder"></param>
        /// <param name="m_arrChangeOrderID">���û���ˢ����Ӱ�����ˮ��</param>
        public void lngChangedOrder(ref clsBIHOrder objOrder)
        {
            //���û���ˢ����Ӱ�����ˮ��
            ArrayList m_arrChangeOrderID = new ArrayList();
            //����ҽ���������ֶ� ��������Ƥ�ԣ������޸ı�־
            this.m_frmInput.m_ctlOrderDetail.SetTheOrderSpecial(ref objOrder);
            //�ж���ȡҩ�������Ƿ���ڿ����
            if (m_blChcekOpcurrentgross(ref objOrder) == false)
            {
                this.m_frmInput.m_ctlOrderDetail.m_txtOrderName.Focus();
                return;
            }
            //���Ҫ�޸ĵ���Ӧͬ��ҽ��  //�õ�ͬ����ҽ����һ������
            ArrayList m_arrChangeList = getChangeListWithSonAdd(objOrder);
            clsBIHOrder[] arrOrder = null;
            if (m_arrChangeList.Count > 0)
            {
                arrOrder = (clsBIHOrder[])(m_arrChangeList.ToArray(typeof(clsBIHOrder)));
            }
            //����ͬ��ҽ����Ƥ��
            SetTheGroupFeel(ref objOrder, ref arrOrder);
            if (m_frmInput.m_objCurrentOrder == null)
            {
                return;
            }
            objOrder = m_frmInput.m_objCurrentOrder;
            if (!string.IsNullOrEmpty(m_frmInput.m_objCurrentOrder.m_strLISAPPLYUNITID_CHR) && m_frmInput.m_objCurrentOrder.m_intStatus == 1)
            {
                clsBIHLis objLis = new clsBIHLis();
                if (this.m_frmInput.m_blSendLisBill == true)
                {
                    clsCommitOrder objCommitLis = new clsCommitOrder();
                    clsDcl_CommitOrder m_objManage = new clsDcl_CommitOrder();
                    string strAp = "";
                    int intSampleFlag = 1;
                    long lngRes = m_objManage.m_lngGetOrderCommitByOrderID(ref strAp, objOrder.m_strOrderDicID, ref intSampleFlag, objOrder.m_strOrderID);

                    if (lngRes == -10)
                    {
                        MessageBox.Show(this.m_frmInput, "��ǰ��ҽ��״̬�Ѿ��ı䣬�����޸ġ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (intSampleFlag > 1)
                    {
                        MessageBox.Show(this.m_frmInput, "��ǰ������Ŀ�ı걾�Ѿ��ɼ��������޸ġ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (intSampleFlag < 1)
                    {
                        MessageBox.Show(this.m_frmInput, "��ǰ������Ŀ״̬�Ѿ��ı䣬�����޸ġ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bool m_blnOK = false;
                    string message = "";
                    m_blnOK = objLis.m_mthDeleteApp(objOrder.m_strOrderID, out message);
                    if (m_blnOK == false)
                    {
                        MessageBox.Show(this.m_frmInput, "���鵥�޸�ʧ�ܣ�\r\n" + message, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    objCommitLis.m_strLISAPPLYUNITID_CHR = strAp;
                    this.m_frmInput.m_ctlOrderDetail.m_objGetCommitOrder(this.m_frmInput.m_ctlPatient.m_objPatient, objOrder, ref objCommitLis);

                    ArrayList m_arrHadSend = new ArrayList();
                    ArrayList m_arrAllNeedSend = new ArrayList();
                    ArrayList SendCheckArr = new ArrayList();

                    bool isHave = false;
                    if (this.m_frmInput.m_objSpecateVo != null && this.m_frmInput.m_objSpecateVo.m_strORDERCATEID_LIS_CHR.Trim().Equals(objCommitLis.m_strOrderDicCateID))
                    {
                        isHave = true;
                    }
                    if (isHave)
                    {
                        m_arrAllNeedSend.Add(objCommitLis);
                    }
                    SendCheckArr.Clear();
                    m_arrHadSend.Clear();
                    SendCheckArr.Add(objCommitLis);
                    m_arrHadSend.Add(objCommitLis.m_strLISAPPLYUNITID_CHR);
                    m_arrAllNeedSend.Remove(objCommitLis);
                    frmBIHOrderCommit objForm = new frmBIHOrderCommit();
                    if (SendCheckArr.Count > 0)
                    {
                        ArrayList m_arrLisOrder = null;
                        objForm.SetThePrice(ref SendCheckArr);


                        if (!objForm.sendTheCheck(ref SendCheckArr, out m_arrLisOrder))
                        {
                            MessageBox.Show(this.m_frmInput, "�������뵥����ʧ�ܡ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }

            // ������ҩ�ж�
            if (DrugUseItf(objOrder, arrOrder) == false) return;

            long ret = m_objInputOrder.m_lngModifyOrder(objOrder, arrOrder, m_frmInput.IsChildPrice);
            if (ret == -10)
            {
                MessageBox.Show(this.m_frmInput, "���޸ĵ�ҽ��״̬�Ѿ��ı䣬�����ٽ����޸ġ�", "ҽ��¼��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.m_frmInput.cmdRefurbish_Click(null, null);
                return;
            }
            if (arrOrder != null && arrOrder.Length > 0)
            {
                for (int i = 0; i < arrOrder.Length; i++)
                {
                    m_arrChangeOrderID.Add(arrOrder[i].m_strOrderID);
                }
            }
            lngRefreshChargePool(m_arrChangeOrderID);
        }
        #endregion



        /// <summary>
        /// ����ͬ��ҽ����Ƥ��
        /// </summary>
        /// <param name="objOrder"></param>
        /// <param name="arrOrder"></param>
        public void SetTheGroupFeel(ref clsBIHOrder objOrder, ref clsBIHOrder[] arrOrder)
        {
            if (arrOrder == null || arrOrder.Length == 0)
            {
                return;
            }
            if (objOrder.m_intISNEEDFEEL == 0)//˵����ǰ�÷�����Ƥ�ԣ���ͬ����ҽ��Ҳ����Ƥ��
            {
                for (int i = 0; i < arrOrder.Length; i++)
                {
                    arrOrder[i].m_intISNEEDFEEL = 0;
                }
                return;
            }
            /*<======================================*/
            //************** �����ǰ�÷���Ƥ����ȥ�µ�Ƥ�Դ����߼�   ************************===>

            //����ҽ����Ƥ��ҩƷ��־
            bool m_blSelf = false;
            //����ͬ��ҽ����Ƥ��ҩƷ��־
            bool m_blOther = false;
            List<string> m_arrOrderDic = new List<string>();
            m_arrOrderDic.Add(objOrder.m_strOrderDicID);
            for (int i = 0; i < arrOrder.Length; i++)
            {
                if (!m_arrOrderDic.Contains(arrOrder[i].m_strOrderDicID))
                {
                    m_arrOrderDic.Add(arrOrder[i].m_strOrderDicID);
                }
            }
            List<string> m_arrFeelList = new List<string>();
            //��ѯ������Ŀ��Ӧ�����շ���Ŀ�Ƿ���ҪƤ��
            m_objInputOrder.m_lngGetFeelListbyOrderDic(m_arrOrderDic, out m_arrFeelList);
            /*<====================================*/
            if (m_arrFeelList != null && m_arrFeelList.Count > 0)
            {
                //�����������ҽ������ʱʹ��,��ΪarrOrder��������������ҽ��
                if (m_arrFeelList.Contains(objOrder.m_strOrderDicID))
                {
                    objOrder.m_intISNEEDFEEL = 1;
                    m_blSelf = true;
                }
                /*<===============================*/
                for (int i = 0; i < arrOrder.Length; i++)
                {
                    if (m_arrFeelList.Contains(arrOrder[i].m_strOrderDicID))
                    {
                        arrOrder[i].m_intISNEEDFEEL = 1;
                        if (arrOrder[i].m_strOrderDicID.Equals(objOrder.m_strOrderDicID))
                        {
                            m_blSelf = true;
                        }
                        else
                        {
                            m_blOther = true;
                        }
                    }
                    else
                    {
                        arrOrder[i].m_intISNEEDFEEL = 0;
                    }
                }
            }
            /*Ƥ�������1.��ǰ�ı���ҽ����ͬ�����鶼����Ƥ��ҩʱ������ͬ��ҽ���ĸ�ҽ��ΪƤ�ԣ��������ǡ�
                        2.��ǰ��ͬ��ҽ����������Ƥ��ҩʱ������Ƥ��ҩ��ΪƤ��ҽ������Ƥ��ҩ��ͬ��ҽ��Ϊ��Ƥ��ҽ��
                        
             */
            if (m_blSelf == false && m_blOther == false)//����ͬ��ҽ��������Ƥ��ҩ
            {

                for (int i = 0; i < arrOrder.Length; i++)
                {
                    arrOrder[i].m_intISNEEDFEEL = 0;//ͬ����ҽ������Ϊ��Ƥ��
                }
                if (arrOrder.Length > 0)//���踸ΪƤ��
                {
                    objOrder.m_intISNEEDFEEL = 0;//������ҽ����Ϊ��Ƥ��
                    arrOrder[0].m_intISNEEDFEEL = 1;//���������Ϊ��ҽ��
                }
            }
            else if (m_blSelf == false && m_blOther == true)//��ǰҽ������Ƥ��ҩ����ͬ������Ƥ��ҩ 
            {
                objOrder.m_intISNEEDFEEL = 0;//������ҽ����Ϊ��Ƥ��
            }



        }

        /// <summary>
        /// ������ҽ�����޸���Ӧ�ĸ�ҽ��
        /// </summary>
        /// <param name="objOrder"></param>
        /// <param name="arrOrder"></param>
        /// <returns></returns>
        private bool m_blnAddNew(clsBIHOrder objOrder, clsBIHOrder[] arrOrder)
        {
            objOrder.m_intStatus = 0;
            // ��ǰ�Ƿ�����ҽ������
            if (this.m_frmInput.m_ctlOrderDetail.IsSubOrder == true)
            {
                objOrder.m_intIsSubOrderAdd = 1;
                objOrder.m_intIFPARENTID_INT = 0;
            }

            // ������ҩ�ж�
            if (DrugUseItf(objOrder, arrOrder) == false) return false;

            /*<==============================*/
            //long ret=m_objService.m_lngCreateOrder(objOrder);
            string strRecordID = "";
            //long ret = m_objService.m_lngAddNewOrder(out strRecordID, objOrder);
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewOrderAndChanged(objOrder, arrOrder);
            if (ret > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ��ø���ǰ����ҽ����صĴ��޸ĵ�ͬ��ҽ��(ͬ��ҽ��)
        /// </summary>
        /// <param name="m_intRecipenNo"></param>
        /// <returns></returns>
        private ArrayList getChangeListWithSonAdd(clsBIHOrder objOrder)
        {
            ArrayList m_arrChangeList = new ArrayList();
            bool m_blNeedChange = false;//trueΪ��Ҫ�޸ģ�falseΪ����Ҫ�޸�
            for (int i = 0; i < this.m_frmInput.m_dtvOrder.RowCount; i++)
            {
                m_blNeedChange = false;
                clsBIHOrder order = (clsBIHOrder)this.m_frmInput.m_dtvOrder.Rows[i].Tag;
                if (order.m_intRecipenNo == objOrder.m_intRecipenNo)
                {
                    if (objOrder.m_intISNEEDFEEL == 0)//�����ҪƤ�Ե�����£�Ҫ����ȫ�������ٴ����÷�Ϊ��ҪƤ��ʱ
                    {

                        if (!order.m_strOrderID.Equals(objOrder.m_strOrderID) && order.m_intExecuteType == objOrder.m_intExecuteType && order.m_strExecFreqID.Equals(objOrder.m_strExecFreqID) && order.m_strDosetypeID.Equals(objOrder.m_strDosetypeID) && order.m_intOUTGETMEDDAYS_INT == objOrder.m_intOUTGETMEDDAYS_INT)
                        {
                            m_blNeedChange = false;
                            //��ҩ˵�����⴦��
                            if (objOrder.m_strOrderDicCateID.Trim().Equals(this.m_frmInput.m_objSpecateVo.m_strMID_MEDICINE_CHR.Trim()))//��ҩ�ж�
                            {
                                if (!order.m_strREMARK_VCHR.Trim().Equals(objOrder.m_strREMARK_VCHR.Trim()))
                                {
                                    m_blNeedChange = true;
                                }
                            }
                            if (order.IsProxyBoilMed != objOrder.IsProxyBoilMed)
                            {
                                m_blNeedChange = true;
                            }
                        }
                        else
                        {
                            m_blNeedChange = true;
                        }
                    }
                    else
                    {
                        m_blNeedChange = true;
                    }
                    if (m_blNeedChange == true)
                    {
                        if (!order.m_strOrderID.Equals(objOrder.m_strOrderID))
                        {
                            //����ͬ��ҽ��
                            order.m_intExecuteType = objOrder.m_intExecuteType;//ҽ������
                            order.m_strExecFreqID = objOrder.m_strExecFreqID;//ҽ��Ƶ��
                            order.m_strExecFreqName = objOrder.m_strExecFreqName;
                            order.m_strEXECTIME_VCHR = objOrder.m_strEXECTIME_VCHR;
                            order.m_intATTACHTIMES_INT = objOrder.m_intATTACHTIMES_INT;//����
                            order.m_intFreqTime = objOrder.m_intFreqTime;//Ƶ�ʴ���
                            //��ҩ��ͬ��ҽ���÷��ɲ�ͬ
                            if (objOrder.m_strOrderDicCateID.Trim().Equals(this.m_frmInput.m_objSpecateVo.m_strMID_MEDICINE_CHR.Trim()))//��ҩ�ж�
                            {
                                order.m_strREMARK_VCHR = objOrder.m_strREMARK_VCHR;
                            }
                            else
                            {
                                //ҽ���÷�
                                order.m_strDosetypeID = objOrder.m_strDosetypeID;
                                order.m_strDosetypeName = objOrder.m_strDosetypeName;
                            }
                            //����/����
                            order.m_intOUTGETMEDDAYS_INT = objOrder.m_intOUTGETMEDDAYS_INT;
                            // Ժ�����
                            order.IsProxyBoilMed = objOrder.IsProxyBoilMed;
                            //��������
                            this.m_frmInput.m_ctlOrderDetail.SetTheOrderGetMoust(ref order);
                            //����Ƥ��
                            this.m_frmInput.m_ctlOrderDetail.SetTheOrderNeelFeel(ref objOrder);

                            m_arrChangeList.Add(order);
                            this.m_frmInput.m_dtvOrder.Rows[i].Tag = order;
                        }
                        else
                        {
                            m_arrChangeList.Add(order);
                        }
                    }
                }
            }
            return m_arrChangeList;
        }

        /// <summary>
        /// ��鵱ǰҽ���ķ����Ƿ�����丸ҽ���ķ���
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns></returns>
        private bool CheckTheSubOrder(clsBIHOrder objOrder)
        {
            bool m_blHave = false;
            for (int i = 0; i < m_frmInput.m_dtvOrder.RowCount; i++)
            {
                if (((clsBIHOrder)m_frmInput.m_dtvOrder.Rows[i].Tag).m_intRecipenNo == objOrder.m_intRecipenNo && ((clsBIHOrder)m_frmInput.m_dtvOrder.Rows[i].Tag).m_strOrderID == objOrder.m_strParentID)
                {
                    m_blHave = true;
                    break;
                }

            }
            return m_blHave;
        }

        /// <summary>
        /// ���ҽ�������Ƿ���ڵ�ǰҽ���б��У��粻���ھ���ҽ������Ϊ���������е�ǰҽ�����������ŵ��޸Ĳ���
        /// </summary>
        /// <param name="objOrder"></param>
        private void SetOrderByRecipenNo(clsBIHOrder objOrder)
        {
            bool m_blHave = false;
            for (int i = 0; i < m_frmInput.m_dtvOrder.RowCount; i++)
            {
                if (((clsBIHOrder)m_frmInput.m_dtvOrder.Rows[i].Tag).m_intRecipenNo == objOrder.m_intRecipenNo)
                {
                    m_blHave = true;
                    break;
                }

            }
            if (m_blHave == false)
            {
                objOrder.m_intRecipenNo = 0;
                objOrder.m_strParentID = "";
                objOrder.m_strParentName = "";
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public bool sendTheCheck(ref clsBIHOrder objOrder)
        {

            string m_strSampleid = ((string)m_frmInput.m_ctlOrderDetail.m_txtSample.Tag).ToString().Trim();
            frmLisAppl obj = new frmLisAppl();
            clsLisApplMainVO objLMVO;
            clsTestApplyItme_VO[] itemArr_VO;
            DataTable p_dtResultArr;
            (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewCheckByOrderID(objOrder, out p_dtResultArr);
            (new weCare.Proxy.ProxyIP()).Service.m_mthSendTestApplyBill(p_dtResultArr, m_strSampleid, objOrder, out objLMVO, out itemArr_VO);
            if (obj.m_mthNewAppInpatient(objLMVO, itemArr_VO, false) == System.Windows.Forms.DialogResult.OK)
            {
                //��ʱע��
                clsLISAppResults[] objAppResult = obj.m_objGetMutiResults();
                if (objAppResult.Length > 0)
                {
                    objOrder.m_strLISAPPID_VCHR = objAppResult[0].m_StrApplicationID.ToString().Trim();
                    objOrder.m_strSAMPLEID_VCHR = m_strSampleid;
                }
            }
            else
            {

                return false;
            }
            return true;

        }

        #region ��ȡ��ǰ�û���ҩƷ����ֵ
        /// <summary>
        /// ��ȡ��ǰ�û���ҩƷ����ֵ
        /// </summary>
        /// <returns></returns>
        public int getMaxValue()
        {
            string strEmpID_chr = m_frmInput.LoginInfo.m_strEmpID.ToString().Trim();
            int maxvalue = 0;

            //clsBIHOrderGroupService objTem = new clsBIHOrderGroupService();
            //clsBIHOrderGroupService objTem = new clsDcl_GetSvcObject().m_GetOrderGroupSvcObject();
            (new weCare.Proxy.ProxyIP()).Service.getMaxValue(strEmpID_chr, out maxvalue);
            return maxvalue;
        }
        #endregion

        #region ���鵱ǰ�û��Ƿ񳬹���������
        /// <summary>
        /// ���鵱ǰ�û��Ƿ񳬹���������
        /// </summary>
        /// <param name="MaxValue"></param>
        /// <returns></returns>
        public bool CheckTheUp(int MaxValue)
        {
            //clsBIHOrderGroupService objTem = new clsBIHOrderGroupService();
            //clsBIHOrderGroupService objTem = new clsDcl_GetSvcObject().m_GetOrderGroupSvcObject();
            DataTable m_dtTable = new DataTable();
            if (m_frmInput.m_ctlOrderDetail.m_txtOrderName.Tag is clsBIHOrderGroup)
            {
                clsBIHOrderGroup bihGroup = (clsBIHOrderGroup)m_frmInput.m_ctlOrderDetail.m_txtOrderName.Tag;

                long m_lng = (new weCare.Proxy.ProxyIP()).Service.m_lngCheckGroupMoney(bihGroup.m_strGroupID.ToString().Trim(), MaxValue, out m_dtTable);
                if (m_lng > 0)
                {
                    if (m_dtTable.Rows.Count > 0)
                    {

                        frmOrderSaveCheck thefrmCheck = new frmOrderSaveCheck();
                        thefrmCheck.m_dtTable = m_dtTable;
                        thefrmCheck.m_strMessage = "[��ѡҽ������������Ŀ���ôﵽ��ǰ�û���ɫ��������]";
                        if (thefrmCheck.ShowDialog() == DialogResult.OK)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }

            }
            else if (!((string)m_frmInput.m_ctlOrderDetail.m_txtOrderName.Tag).Equals(""))
            {
                double get_dec = 0.0;
                if (!m_frmInput.m_ctlOrderDetail.m_txtGet.Text.Trim().Equals(""))
                {
                    get_dec = System.Convert.ToDouble(m_frmInput.m_ctlOrderDetail.m_txtGet.Text.ToString().Trim());
                }
                else
                {
                    get_dec = 1;
                }
                string BIHOrderDic = (string)m_frmInput.m_ctlOrderDetail.m_txtOrderName.Tag;

                long m_lng = (new weCare.Proxy.ProxyIP()).Service.m_lngCheckMoneyByorderdicid(BIHOrderDic.ToString().Trim(), MaxValue, get_dec, out m_dtTable);
                if (m_lng > 0)
                {
                    if (m_dtTable.Rows.Count > 0)
                    {

                        frmOrderSaveCheck thefrmCheck = new frmOrderSaveCheck();
                        thefrmCheck.m_dtTable = m_dtTable;
                        thefrmCheck.m_strMessage = "[��ѡҽ������Ŀ���ôﵽ��ǰ�û���ɫ��������]";
                        if (thefrmCheck.ShowDialog() == DialogResult.OK)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }

                //string m_decPrice = m_frmInput.m_ctlOrderDetail.m_txtPrice.Text.ToString().Trim();
                //double m_dblPrice = 0.0;
                //if (!m_decPrice.Equals(""))
                //{
                //    m_dblPrice = Convert.ToDouble(m_decPrice);
                //}
                //double dblNumber = 0.0;
                //if (!m_frmInput.m_ctlOrderDetail.m_txtGet.Text.Trim().Equals(""))
                //{
                //    dblNumber = System.Convert.ToDouble(m_frmInput.m_ctlOrderDetail.m_txtGet.Text.ToString().Trim());
                //}
                ////�ϼƽ��
                //double m_dbMoney = m_dblPrice * dblNumber;
                //if (m_dbMoney > MaxValue)
                //{
                //    m_dtTable.Columns.AddRange(new DataColumn[]
                //                                             {
                //                                            new DataColumn("NAME_CHR"),
                //                                            new DataColumn("ItemPrice"),
                //                                            new DataColumn("get_dec"),
                //                                            new DataColumn("pricesum") 
                //                                             });
                //    DataRow row1 = m_dtTable.NewRow();
                //    row1["NAME_CHR"] = m_frmInput.m_ctlOrderDetail.m_txtOrderName.Text.ToString().Trim();
                //    row1["ItemPrice"] = m_decPrice.ToString().Trim();
                //    row1["get_dec"] = dblNumber.ToString().Trim();
                //    row1["pricesum"] = m_dbMoney.ToString();
                //    m_dtTable.Rows.Add(row1);

                //    frmOrderSaveCheck thefrmCheck = new frmOrderSaveCheck();
                //    thefrmCheck.m_dtTable = m_dtTable;
                //    thefrmCheck.m_strMessage = "[��ѡҽ����Ŀ���ôﵽ��ǰ�û���ɫ��������]";
                //    if (thefrmCheck.ShowDialog() == DialogResult.OK)
                //    {
                //        return true;
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}

            }

            return true;
        }
        #endregion

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="objOrder">ҽ����¼Vo����</param>
        /// <returns>�����Ƿ�����ɹ�</returns>
        public bool m_blnAddNew(clsBIHOrder objOrder)
        {
            objOrder.m_intStatus = 0;
            // ��ǰ�Ƿ�����ҽ������
            if (this.m_frmInput.m_ctlOrderDetail.IsSubOrder == true)
            {
                objOrder.m_intIsSubOrderAdd = 1;
                objOrder.m_intIFPARENTID_INT = 0;
            }
            else
            {
                objOrder.m_intIFPARENTID_INT = 1;
            }

            // ������ҩ�ж�
            if (DrugUseItf(objOrder) == false) return false;
            string strRecordID = "";
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewOrder(out strRecordID, objOrder);
            if (ret > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region ������ҩ
        /// <summary>
        /// ������ҩ.�����ַ
        /// </summary>
        string DrugServiceUrl { get; set; }
        /// <summary>
        /// �Ƿ�ʹ�ú�����ҩ�ӿ�
        /// </summary>
        bool IsUseMedItf { get; set; }
        /// <summary>
        /// ������ҩ.�ӿ�
        /// </summary>
        /// <param name="orderVo"></param>
        /// <returns></returns>
        bool DrugUseItf(clsBIHOrder orderVo)
        {
            return DrugUseItf(orderVo, null);
        }
        /// <summary>
        /// ������ҩ.�ӿ�
        /// </summary>
        /// <returns></returns>
        bool DrugUseItf(clsBIHOrder orderMain, clsBIHOrder[] orderArr)
        {
            if (string.IsNullOrEmpty(DrugServiceUrl))
            {
                DrugServiceUrl = com.digitalwave.iCare.gui.HIS.clsPublic.m_strGetSysparm("0080");
                IsUseMedItf = (Convert.ToDecimal(com.digitalwave.iCare.gui.HIS.clsPublic.m_strGetSysparm("0082")) == 1 ? true : false);
            }

            List<clsBIHOrder> lstDeptMed = new List<clsBIHOrder>();
            Hisitf.EntityDrugUse patVo = new Hisitf.EntityDrugUse();
            Hisitf.EntityDrugUse drugVo = null;
            System.Collections.Generic.List<Hisitf.EntityDrugUse> lstDrug = new System.Collections.Generic.List<Hisitf.EntityDrugUse>();

            #region pat
            patVo.bedNo = m_frmInput.m_ctlPatient.m_objPatient.m_strBedID;
            //patVo.presType
            patVo.presSource = "סԺ";
            patVo.presDatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            patVo.payType = "";// ? 
            patVo.patientNo = m_frmInput.m_ctlPatient.m_objPatient.m_strPATIENTCARDID_CHR;
            patVo.presNo = "Z0";
            patVo.name = m_frmInput.m_ctlPatient.m_objPatient.m_strPatientName;
            patVo.diagnose = m_frmInput.m_ctlPatient.m_objPatient.m_strDiagnose;
            patVo.age = m_frmInput.m_ctlPatient.m_objPatient.m_strAge;
            patVo.sex = (m_frmInput.m_ctlPatient.m_objPatient.m_strSex == "��" ? "M" : "F");  // ? M F ��
            patVo.drugSensivity = "false";      // ����

            #endregion

            #region use drug

            List<clsBIHOrder> lstOrder = new List<clsBIHOrder>();
            if (orderMain != null) lstOrder.Add(orderMain);
            if (orderArr != null) lstOrder.AddRange(orderArr);
            bool isSkipLevel = false;   // Խ��ʹ�ÿ�����(��һ��): ���� -> �м� -> ����
            DateTime dtmNow = DateTime.Now;
            if ((Convert.ToDateTime(dtmNow.ToString("yyyy-MM-dd") + " 17:30:00") < dtmNow && dtmNow < Convert.ToDateTime(dtmNow.ToString("yyyy-MM-dd") + " 23:59:59")) ||
                 (Convert.ToDateTime(dtmNow.AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00") < dtmNow && dtmNow < Convert.ToDateTime(dtmNow.AddDays(1).ToString("yyyy-MM-dd") + " 07:59:59")))
            {
                isSkipLevel = true;
            }
            foreach (clsBIHOrder orderVo in lstOrder)
            {
                // 01 -- ҩ��; 17 -- ��ҩ
                if (orderVo.m_strOrderDicCateID == "01" || orderVo.m_strOrderDicCateID == "17")
                {
                    drugVo = new Hisitf.EntityDrugUse();
                    drugVo.drug = orderVo.m_strOrderDicID;
                    drugVo.drugName = orderVo.m_strName;
                    drugVo.specification = orderVo.m_strSpec;
                    drugVo.package = orderVo.m_dmlPACKQTY_DEC.ToString();
                    drugVo.quantity = orderVo.m_dmlGet.ToString();
                    drugVo.packUnit = orderVo.m_strGetunit;
                    drugVo.unitPrice = orderVo.m_dmlPrice.ToString();
                    drugVo.amount = com.digitalwave.iCare.gui.HIS.clsPublic.Round(orderVo.m_dmlPrice * orderVo.m_dmlGet, 2).ToString();
                    drugVo.groupNo = orderVo.m_intRecipenNo.ToString();
                    drugVo.firstUse = "false";
                    drugVo.adminRoute = orderVo.m_strDosetypeName;
                    drugVo.adminFrequency = orderVo.m_strExecFreqName;
                    drugVo.adminDose = orderVo.m_dmlDosage.ToString() + orderVo.m_strDosageUnit;    // +��λ? 
                    //drugVo.adminMethod
                    if (orderVo.m_intExecuteType == 1)
                        drugVo.type1 = "����";
                    else if (orderVo.m_intExecuteType == 2)
                        drugVo.type1 = "��ʱ";
                    else if (orderVo.m_intExecuteType == 3)
                        drugVo.type1 = "��ҩ";
                    //drugVo.adminGoal
                    drugVo.docID1 = orderVo.m_strCreatorID;
                    drugVo.docName1 = orderVo.m_strCreator;
                    if (isSkipLevel && !string.IsNullOrEmpty(m_frmInput.m_objLoginInfo.m_strTechnicalRank))
                    {
                        if (m_frmInput.m_objLoginInfo.m_strTechnicalRank.Trim() == "סԺҽʦ")
                            drugVo.docTitle1 = "����ҽʦ";
                        else if (m_frmInput.m_objLoginInfo.m_strTechnicalRank.Trim() == "����ҽʦ")
                            drugVo.docTitle1 = "������ҽʦ";
                        else if (m_frmInput.m_objLoginInfo.m_strTechnicalRank.Trim() == "������ҽʦ")
                            drugVo.docTitle1 = "����ҽʦ";
                        else
                            drugVo.docTitle1 = m_frmInput.m_objLoginInfo.m_strTechnicalRank;

                        // 24Сʱ��������Խ������ͬһ����ҩ��  -- ��Ϊ -->  24Сʱ����סԺ�ڼ䶼������Խ����ͬһ����
                        //using (clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject())
                        //{
                        DataTable dt = (new weCare.Proxy.ProxyIP()).Service.GetSkipLevelAntiMedcine(m_frmInput.m_ctlPatient.m_objPatient.m_strRegisterID, orderVo.m_strOrderDicID);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DateTime dtmPost = Convert.ToDateTime(dt.Rows[0]["postdate_dat"].ToString());
                            DateTime dtmTmp = Convert.ToDateTime(dtmNow.ToString("yyyy-MM-dd") + " " + dtmPost.ToString("HH:mm:ss"));
                            TimeSpan ts = dtmNow.Subtract(dtmPost);
                            // 24Сʱ�Ժ������� Խ��ʱ��ο��ߵ�
                            if (ts.Hours >= 24 && ((Convert.ToDateTime(dtmNow.ToString("yyyy-MM-dd") + " 17:30:00") < dtmTmp && dtmTmp < Convert.ToDateTime(dtmNow.ToString("yyyy-MM-dd") + " 23:59:59")) ||
                                                   (Convert.ToDateTime(dtmNow.ToString("yyyy-MM-dd") + " 00:00:00") < dtmTmp && dtmTmp < Convert.ToDateTime(dtmNow.ToString("yyyy-MM-dd") + " 07:59:59"))))
                            {
                                MessageBox.Show("24Сʱ��������Խ������ͬһ����ҩ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                        }
                        //}
                    }
                    else
                    {
                        drugVo.docTitle1 = m_frmInput.m_objLoginInfo.m_strTechnicalRank;
                    }
                    drugVo.departID1 = orderVo.m_strCREATEAREA_ID;
                    drugVo.department1 = orderVo.m_strCREATEAREA_Name;
                    if (string.IsNullOrEmpty(drugVo.department1)) drugVo.department1 = m_frmInput.m_objLoginInfo.m_strdepartmentName;
                    //drugVo.nurseName
                    drugVo.startTime = orderVo.m_dtStartDate.ToString("yyyy-MM-dd HH:mm:ss");
                    drugVo.endTime = orderVo.m_dtFinishDate.ToString("yyyy-MM-dd HH:mm:ss");
                    if (drugVo.endTime.StartsWith("0001-01-01")) drugVo.endTime = string.Empty;
                    lstDrug.Add(drugVo);

                    // ����ҩ
                    if (orderVo.RateType == 2)
                    {
                        lstDeptMed.Add(orderVo);
                    }
                }
            }
            if (lstDrug.Count > 0)
            {
                patVo.docID = lstDrug[0].docID1;
                patVo.docName = lstDrug[0].docName1;
                patVo.docTitle = lstDrug[0].docTitle1;
            }

            #region ����:��ע���������ִ�б����������
            if (lstDeptMed.Count > 0)
            {
                //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                foreach (clsBIHOrder item in lstDeptMed)
                {
                    if ((new weCare.Proxy.ProxyIP()).Service.IsMedInjection(item.m_strOrderDicID, 1) == false)
                    {
                        MessageBox.Show("����ҩ(ִ�б���)ֻ����ע���ҩƷ�����顣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }
            #endregion

            #endregion

            // ������ҩ�ӿ�
            if (lstDrug.Count > 0 && IsUseMedItf)
            {
                string orderDicID = string.Empty;
                //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                foreach (Hisitf.EntityDrugUse item in lstDrug)
                {
                    orderDicID += "'" + item.drug + "',";
                }
                Dictionary<string, clsMedicine_VO> dicMed = (new weCare.Proxy.ProxyIP()).Service.GetMedInfoByOrderDicId(orderDicID.TrimEnd(','));
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
                    return itf.CheckDrugUse(3, DrugServiceUrl, patVo, lstDrug);
                }
            }
            return true;
        }
        #endregion

        /// <summary>
        /// ����ҽ��(����һ��ҽ����ͬ����ͬ��������£�
        /// </summary>
        /// <param name="p_strRecordIDArr"></param>
        /// <param name="objOrder">ҽ����¼Vo����</param>
        /// <returns>�����Ƿ�����ɹ�</returns>
        public long m_lngAddNewOrderByGroup(out string[] p_strRecordIDArr, List<clsBIHOrder> p_RecordArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc =
            //    (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewOrderByGroup(out p_strRecordIDArr, p_RecordArr, m_frmInput.IsChildPrice);
            return lngRes;

        }

        /// <summary>
        /// ��ȡҽ��(����ҽ����ˮ��)
        /// </summary>
        /// <param name="p_strRecordIDArr"></param>
        /// <param name="objOrder">ҽ����¼Vo����</param>
        /// <returns>�����Ƿ�����ɹ�</returns>
        public long m_lngGetOrderByOrderIDs(string m_strORDERID_CHR, out clsBIHOrder[] m_objOrder)
        {
            long lngRes = 0;
            m_objOrder = null;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc =
            //    (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            //lngRes = objSvc.m_lngGetOrderByOrderIDs( m_strORDERID_CHR, out  m_objOrder);
            return lngRes;

        }

        /// <summary>
        /// ����סԺ�Ǽ���ˮ��ͣҽ��
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_strHandlersID"></param>
        /// <param name="p_strHandlers"></param>
        /// <returns></returns>
        public bool m_lngStopANDAuditingOrderByRegID(string p_strRegisterID, string p_strHandlersID, string p_strHandlers)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngStopANDAuditingOrderByRegID(p_strRegisterID, p_strHandlersID, p_strHandlers);
            if (lngRes > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ����סԺ�Ǽ���ˮ��ͣҽ��
        /// </summary>
        /// <param name="p_strRegisterID"></param>
        /// <param name="p_strHandlersID"></param>
        /// <param name="p_strHandlers"></param>
        /// <returns></returns>
        public bool m_lngStopAllOrderByRegID(string p_strRegisterID, string p_strHandlersID, string p_strHandlers)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngStopAllOrderByRegID(p_strRegisterID, p_strHandlersID, p_strHandlers);
            if (lngRes > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public long m_lngGetOrderStopSign(string[] m_arrOrders, out DataTable m_dtOrderSign)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderStopSign(m_arrOrders, out m_dtOrderSign);
            return lngRes;
        }

        /// <summary>
        /// ����ҽ�� [����û��������]
        /// </summary>
        /// <param name="arrOrder">ҽ����¼Vo���� [����]</param>
        /// <returns>�����Ƿ�����ɹ�</returns>
        public bool m_blnAddNew(clsBIHOrder[] arrOrder)
        {
            if ((arrOrder == null) || (arrOrder.Length <= 0)) return true;

            for (int i = 0; i < arrOrder.Length; i++) arrOrder[i].m_intStatus = 0;

            string strParentID = "";
            string strRecordID = "";
            //long ret=m_objService.m_lngCreateOrder(arrOrder[0]);
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewOrder(out strRecordID, arrOrder[0]);

            if (ret > 0)
            {
                strParentID = arrOrder[0].m_strOrderID;
            }
            else
            {
                return false;
            }
            for (int i = 1; i < arrOrder.Length; i++)
            {
                arrOrder[i].m_strParentID = strParentID;
                long ret2 = (new weCare.Proxy.ProxyIP()).Service.m_lngCreateOrder(arrOrder[i]);
            }
            return true;
        }
        /// <summary>
        /// ��֤ҽ��¼�������Ƿ�Ϸ�
        /// </summary>
        /// <param name="objOrder">ҽ��Vo����</param>
        /// <returns></returns>
        public bool ValidateInput(clsBIHOrder objOrder)
        {
            int intIsFather = 0;
            return ValidateInput(objOrder, out intIsFather);
        }
        /// <summary>
        /// ��֤ҽ��¼�������Ƿ�Ϸ�
        /// </summary>
        /// <param name="objOrder"></param>
        /// <param name="p_intIsFather">�Ƿ�Ϊ����ҽ�� {0=���Ǹ���ҽ����1=�Ǹ���ҽ��}</param>
        /// <returns></returns>
        public bool ValidateInput(clsBIHOrder objOrder, out int p_intIsFather)
        {
            long lngRes = 0;
            p_intIsFather = 0;
            if (objOrder == null) return false;
            //ִ��Ƶ��
            if (m_frmInput.m_ctlOrderDetail.m_txtExecuteFreq.Enabled == true)
            {
                if (objOrder.m_strExecFreqID == null || objOrder.m_strExecFreqID.Trim() == "")
                {
                    m_mthShowMessage("ִ��Ƶ�ʲ�����!");
                    m_frmInput.m_ctlOrderDetail.m_txtExecuteFreq.Focus();
                    return false;
                }
            }
            else
            {
                if (objOrder.m_strExecFreqID == null || objOrder.m_strExecFreqID.Trim() == "")
                {
                    objOrder.m_strExecFreqID = "15";
                }
            }
            //��ҩ��ʽ��֤	{����ҽ������ID���÷���ʾ����{1=����;2=��},=2ʱ���Ʒ�}
            clsT_aid_bih_ordercate_VO p_objOrdercate = null;
            if (objOrder.m_strDosetypeID == null || objOrder.m_strDosetypeID.Trim() == "")
            {
                lngRes = m_objInputOrder.m_lngGetAidOrderCateByID(objOrder.m_strOrderDicCateID, out p_objOrdercate);
                if (lngRes > 0 && p_objOrdercate != null && p_objOrdercate.m_intUSAGEVIEWTYPE != 2)
                {
                    m_mthShowMessage("��ҩ��ʽ������!");
                    m_frmInput.m_ctlOrderDetail.m_txtDosageType.Focus();
                    return false;
                }
            }
            //������Ŀ
            if (objOrder.m_strOrderDicID == null || objOrder.m_strOrderDicID.Trim() == "")
            {
                m_mthShowMessage("ҽ��¼�����ݲ��Ϸ�,��ˢ�º���������!");
                m_frmInput.m_ctlOrderDetail.m_txtOrderName.Focus();
                return false;
            }
            //ͬ�������÷���Ƶ�ʱ�����ͬ
            //if(!PassSameRecipeNO(objOrder))
            //{
            //    if(objOrder.m_strParentID!=null && objOrder.m_strParentID.Trim()!="")
            //    { //��ҽ��ͬ�������÷���Ƶ�ʱ�����ͬ
            //        m_mthShowMessage("ͬ�������÷���Ƶ�ʱ�����ͬ!");
            //        return false;
            //    }
            //    else
            //    {
            //        p_intIsFather =1;
            //    }
            //}
            //ͬ��ҽ���޸�ʱ�Ľ������
            bool m_blParentOrder, m_blSubOrder;
            m_frmInput.TheChangedOrderParentOrSub(objOrder, out m_blParentOrder, out m_blSubOrder);
            if (m_blParentOrder == true)
            {
                p_intIsFather = 1;
            }
            //[�������ԡ�������ʾ����Ϊ������ҽ��]���������������� �������0			
            if (objOrder.m_dmlDosage <= 0 || objOrder.m_dmlGet <= 0 || objOrder.m_dmlUse <= 0)
            {
                if (m_strConfreqID.Trim() == "") m_strConfreqID = new clsDcl_ExecuteOrder().m_strGetConfreqID();
                if (p_objOrdercate == null)
                {
                    lngRes = m_objInputOrder.m_lngGetAidOrderCateByID(objOrder.m_strOrderDicCateID, out p_objOrdercate);
                }
                if (objOrder.m_strExecFreqID.Trim() != m_strConfreqID.Trim() && p_objOrdercate.m_intDOSAGEVIEWTYPE != 2)
                {
                    //m_mthShowMessage("���������������� �������0��");
                    //m_frmInput.m_ctlOrderDetail.m_txtDosage.Focus();
                    //return false;
                }
            }
            //������ҽ��: ��ʼʱ��	{��൱ǰʱ��ǰ6Сʱ}
            if (m_strConfreqID.Trim() == "") m_strConfreqID = new clsDcl_ExecuteOrder().m_strGetConfreqID();
            if (objOrder.m_strExecFreqID.Trim() == m_strConfreqID.Trim())
            {
                if (objOrder.m_dtStartDate < System.DateTime.Now.AddHours(-6))
                {
                    m_mthShowMessage("��ʼʱ�����ȵ�ǰʱ��ǰ6Сʱ��");
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region ���ӵ���
        private void m_mthRefreshOtherBillInfo()
        {
            m_arlOtherBillInfo.Clear();

            clsBIHOrder[] arrOrder = (clsBIHOrder[])(m_frmInput.m_arlOrder.ToArray(typeof(clsBIHOrder)));
            clsOrderDicCates.Cate_tag = false;
            for (int i = 0; i < arrOrder.Length; i++)
            {
                if (clsOrderDicCates.IsMedOrder(arrOrder[i].m_strOrderDicCateID))
                {
                    clsBIHOrderCate objCate = clsOrderDicCates.m_objGetCate(arrOrder[i].m_strOrderDicCateID);
                    clsOtherBillInfo objInfo = new clsOtherBillInfo();
                    objInfo.m_objOrder = arrOrder[i];
                    objInfo.m_objOrderCate = objCate;
                    //objInfo.m_objService = this.m_objService;
                    objInfo.m_ParentForm = this.m_frmInput;
                    objInfo.m_ParentForm.objController = this.m_frmInput.objController;
                    m_arlOtherBillInfo.Add(objInfo);
                }
            }

            m_frmInput.m_lblOtherBill.Text = "���ӵ���(" + m_arlOtherBillInfo.Count.ToString() + ")";
            if (m_arlOtherBillInfo.Count <= 0)
            {
                this.m_frmInput.m_btnAddBills.Enabled = false;
            }
            else
            {
                this.m_frmInput.m_btnAddBills.Enabled = true;
            }
        }

        //���浱ǰ���ӵ�����Ϣ
        public ArrayList m_arlOtherBillInfo = new ArrayList();
        public class clsOtherBillInfo
        {
            public clsBIHOrder m_objOrder;
            public clsBIHOrderCate m_objOrderCate;
            //public clsBIHOrderService m_objService;

            public string m_strRelationID = "";

            public clsApplyReport_T_VO m_objART = new clsApplyReport_T_VO();
            public com.digitalwave.GUI_Base.frmMDI_Child_Base m_ParentForm;

            public static clsBIHPatientInfo s_objPatient;

            #region ���ӵ�����Ŀ
            private int m_intAttachOrderCount = -1;
            /// <summary>
            /// ���ӵ�����Ŀ
            /// </summary>
            private void m_mthRefreshAttachOrderCount()
            {
                string[] arrID;
                long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetAttachOrder(m_objOrder.m_strOrderID, out arrID);
                if ((ret > 0) && (arrID != null))
                    m_intAttachOrderCount = arrID.Length;
                else
                    m_intAttachOrderCount = -1;
            }
            /// <summary>
            /// ��ȡ���ӵ�����Ŀ
            /// </summary>
            public int AttachOrderCount
            {
                get
                {
                    if (m_intAttachOrderCount < 0) m_mthRefreshAttachOrderCount();
                    return m_intAttachOrderCount;
                }
            }

            #endregion

            #region ��ʾ���ӵ��ݴ���
            /// <summary>
            /// ��ʾ���ӵ��ݴ���
            /// </summary>
            /// <param name="objEmp">��½��Ϣ</param>
            public void m_mthShowUI(clsLoginInfo objEmp)
            {
                //ҽ������ID
                string strOrderCateID = m_objOrderCate.m_strOrderCateID.Trim();
                //����ID
                string strPatientID = m_objOrder.m_strPatientID;
                //ҽ����ID
                string strOrderID = m_objOrder.m_strOrderID;
                //���ӵ���ID
                string strAttachID = GetAttachID(strOrderID);

                int intEditState = 0;
                long lngRes = 0;

                //�����ѯ���ӵ��ݵĴ���.
                DataTable dtbAddBills = null;
                lngRes = m_lngGetAddBillByOrderID(strOrderID.Trim(), out dtbAddBills);
                if (lngRes >= 0 && dtbAddBills.Rows.Count > 0)
                {
                    intEditState = 1;
                    m_objART.m_StrRecordID = dtbAddBills.Rows[0]["ATTACHID_VCHR"].ToString().Trim();
                }
                else
                {
                    intEditState = 0;
                }

                //�༭״̬{0=����;1=�༭;2=ֻ��;}
                //				if(m_objOrder.m_intStatus==0 || m_objOrder.m_intStatus==7)//ֻ�ܱ༭����״̬�ĸ��ӵ���
                //				{
                //					if(strAttachID.Trim()!="")
                //						intEditState =1;
                //					else
                //						intEditState =0;
                //				}
                //				else
                //				{
                //					if(strAttachID.Trim()!="")
                //						intEditState =2;
                //					else
                //					{	//ִ��״̬��Ϊ��0=����������û�и��ӵ��ݣ�����ʾ��
                //						//����ʾ�Ͳ���ʾ,������ô����? by jli in 2005-04-12
                //						return;
                //					}
                //				}

                //��ȡҽ������Vo
                clsT_aid_bih_ordercate_VO objResult = null;
                clsDcl_InputOrder objTem = new clsDcl_InputOrder();
                lngRes = objTem.m_lngGetAidOrderCateByID(strOrderCateID, out objResult);
                if (lngRes <= 0 || objResult == null) return;

                try
                {
                    m_mthOpenEditAidOrderForm(dtbAddBills, objResult, strAttachID.Trim());
                    m_mthRefreshAttachOrderCount();
                }
                catch (Exception err)
                {
                    string strMsg = err.Message.ToString();
                    try
                    {
                        com.digitalwave.Utility.clsLogText objText = new com.digitalwave.Utility.clsLogText();
                        objText.Log2File("d:\\code\\log.txt", err.Message.ToString() + "\r\n" + err.InnerException.Message.ToString() + "\r\n" + err.StackTrace.ToString());
                    }
                    catch
                    { }
                    MessageBox.Show("���ظ��ӵ���ʧ��!", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            /// <summary>
            /// ��ȡҽ�����ӵ���ID
            /// </summary>
            /// <param name="strOrderID">ҽ��ID</param>
            /// <returns>���ӵ���ID</returns>
            private string GetAttachID(string strOrderID)
            {
                //���ӱ�id
                string[] strAttachIDArr;
                long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetAttachOrder(strOrderID, out strAttachIDArr);
                if (ret > 0 && strAttachIDArr != null && strAttachIDArr.Length > 0)
                    return strAttachIDArr[0];
                else
                    return "";
            }
            #region ��ʾ���ӵ��ݴ��ڱ༭����-����༭�������
            /// <summary>
            /// ��ʾ���ӵ��ݴ��ڱ༭����
            /// </summary>
            /// <param name="strPatientID">����ID</param>
            /// <param name="strOrderID">ҽ����ID</param>
            /// <param name="strAttachID">���ӵ���ID</param>
            /// <param name="intEditState">�༭״̬{0=����;1=�༭;2=ֻ��;}</param>
            /// <param name="objResult">ҽ������Vo</param>
            private void OpenEditAidOrderForm(string strPatientID, string strOrderID, string strAttachID, int intEditState, clsT_aid_bih_ordercate_VO objResult)
            {
                string strDllName = objResult.m_strDLLNAME_VCHR;
                string strClassName = objResult.m_strCLASSNAME_VCHR;
                string strInsertName = objResult.m_strOPRADD_VCHR;
                string strUpdateName = objResult.m_strOPRUPD_VCHR;

                System.Reflection.Assembly objAsm = System.Reflection.Assembly.LoadFrom(strDllName);
                if (objAsm == null) return;
                //���ò���
                object[] objParams = new object[4];
                objParams[0] = strPatientID;
                objParams[1] = strOrderID;
                objParams[2] = strAttachID;
                objParams[3] = intEditState;
                object obj;
                try
                {
                    obj = objAsm.CreateInstance(strClassName, true, System.Reflection.BindingFlags.Default, null, objParams, null, new object[0] { });
                }
                catch (System.Exception err)
                {
                    string strMsg = err.Message.ToString();
                    MessageBox.Show(strMsg, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (obj == null) return;
                //�򿪴���
                Type objType = obj.GetType();
                System.Reflection.MethodInfo objMi = objType.GetMethod("ShowDialog", new Type[0]);
                if (objMi == null) return;
                objMi.Invoke(obj, new object[0] { });
            }
            #endregion
            #endregion

            #region ��ʾ���ӵ��ݱ༭����:����༭������� Add by jli in 2005-04-11
            private void m_mthOpenEditAidOrderForm(DataTable p_dtbAddBill, clsT_aid_bih_ordercate_VO objResult, string p_strAttachID)
            {
                string strDllName = objResult.m_strDLLNAME_VCHR;
                string strClassName = objResult.m_strCLASSNAME_VCHR;
                string strInsertName = objResult.m_strOPRADD_VCHR;
                string strUpdateName = objResult.m_strOPRUPD_VCHR;

                System.Reflection.Assembly objAsm = System.Reflection.Assembly.LoadFrom(strDllName);

                if (objAsm == null) return;
                //���ò���

                #region ���ò�����ֵ

                //����ID
                if (s_objPatient.m_strAreaID != null)
                {
                    m_objART.m_StrAreaID = s_objPatient.m_strAreaID;
                }
                else
                {
                    m_objART.m_StrAreaID = "";
                }

                //�ͼ�ҽ��ID
                if (m_ParentForm.LoginInfo.m_strEmpID != null)
                {
                    m_objART.m_StrDeliverDoctorID = m_ParentForm.LoginInfo.m_strEmpID;
                }
                else
                {
                    m_objART.m_StrDeliverDoctorID = "";
                }

                //��������
                if (s_objPatient.m_strAreaName != null)
                {
                    m_objART.m_StrAreaName = s_objPatient.m_strAreaName;
                }
                else
                {
                    m_objART.m_StrAreaName = "";
                }

                //����
                if (s_objPatient.m_strBedID != null)
                {
                    m_objART.m_StrBedID = s_objPatient.m_strBedID;
                }
                else
                {
                    m_objART.m_StrBedID = "";
                }

                //������
                if (s_objPatient.m_strBedName != null)
                {
                    m_objART.m_StrBedName = s_objPatient.m_strBedName;
                }
                else
                {
                    m_objART.m_StrBedName = "";
                }

                //����ID
                if (s_objPatient.m_strDeptID != null)
                {
                    m_objART.m_StrDeptID = s_objPatient.m_strDeptID;
                }
                else
                {
                    m_objART.m_StrDeptID = "";
                }

                //��������
                if (s_objPatient.m_strBedID != null)
                {
                    try
                    {
                        clsDepartmentVO[] m_objDeptArr = new clsDepartmentVO[] { new clsDepartmentVO() };
                        m_ParentForm.objController.m_objComInfo.m_mthGetDepInfoByDepID(m_objART.m_StrDeptID, out m_objDeptArr);
                        m_objART.m_StrDeptName = m_objDeptArr[0].strDeptName;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    m_objART.m_StrDeptName = "";
                }

                //סԺ��
                if (s_objPatient.m_strInHospitalNo != null)
                {
                    m_objART.m_StrInPatientID = s_objPatient.m_strInHospitalNo;
                }
                else
                {
                    m_objART.m_StrInPatientID = "";
                }

                if (s_objPatient.m_dtBorn != DateTime.MinValue)
                {
                    //change 2007.4.13
                    //TimeSpan ts=DateTime.Now-s_objPatient.m_dtBorn;
                    //string strAge="";
                    //strAge = com.digitalwave.iCare.gui.LIS.clsAgeConverter.s_strToAge(s_objPatient.m_dtBorn);
                    //=====================>>
                    string strAge = "";
                    strAge = com.digitalwave.iCare.gui.LIS.clsAgeConverter.s_strGetAge(s_objPatient.m_dtBorn);
                    /*<<====================*/
                    //					if(ts.TotalDays>365d)
                    //					{
                    //						strAge=((int)(ts.TotalDays/365d)).ToString()+"��";
                    //					}
                    //					else
                    //					{
                    //						strAge=((int)(ts.TotalDays/30d)).ToString()+"��";
                    //					}
                    //					m_objART.m_StrInPatientID=s_objPatient.m_strInHospitalNo;
                }
                else
                {
                    m_objART.m_StrInPatientID = "";
                }

                //�ٴ����
                //Add by jli in 2005-06-17
                DataTable dtbRegister = new DataTable();
                if ((new weCare.Proxy.ProxyIP02()).Service.m_lngGetRegisterInfoByFilter(" INPATIENTID_CHR='" + s_objPatient.m_strInHospitalNo.Trim() + "'", out dtbRegister) > 0)
                {
                    m_objART.m_StrClinicDiagnose = dtbRegister.Rows[0]["DIAGNOSEID_CHR"].ToString().Trim();
                }
                else
                {
                    m_objART.m_StrClinicDiagnose = "";
                }
                //Add end

                //����ID
                if (s_objPatient.m_strPatientID != null)
                {
                    m_objART.m_StrPatientID = s_objPatient.m_strPatientID;
                }
                else
                {
                    m_objART.m_StrPatientID = "";
                }

                //����

                if (s_objPatient.m_strPatientID != null)
                {
                    try
                    {
                        clsPatientVO[] objPatient = new clsPatientVO[0];
                        m_ParentForm.objController.m_objComInfo.m_mthGetPatientInfo(s_objPatient.m_strPatientID, out objPatient, true);
                        //����
                        m_objART.m_StrPatientCardID = ((frmBIHOrderInput)m_ParentForm).PatientCardID.Trim();
                        //����
                        m_objART.m_StrNation = objPatient[0].strNationality.Trim();
                        //ְҵ
                        m_objART.m_StrOccupy = objPatient[0].strOccupation.Trim();
                        //�����
                    }
                    catch
                    {
                    }
                }
                else
                {
                    m_objART.m_StrPatientCardID = "";
                    m_objART.m_StrNation = "";
                    m_objART.m_StrOccupy = "";

                }



                //��������
                if (s_objPatient.m_strPatientName != null)
                {
                    m_objART.m_StrPatientName = s_objPatient.m_strPatientName;
                }
                else
                {
                    m_objART.m_StrPatientName = "";
                }

                //�����Ա�
                if (s_objPatient.m_strSex != null)
                {
                    m_objART.m_StrPatientSex = s_objPatient.m_strSex;
                }
                else
                {
                    m_objART.m_StrPatientSex = "";
                }

                #endregion

                object[] objParams = new object[1];

                if (p_strAttachID.Trim() == "")
                {
                    objParams[0] = m_objART;
                    m_strRelationID = "";
                }
                else
                {
                    objParams[0] = p_strAttachID.Trim();
                    m_strRelationID = p_dtbAddBill.Rows[0]["ATTARELAID_CHR"].ToString().Trim();
                }

                object obj;
                try
                {

                    obj = objAsm.CreateInstance(strClassName, true, System.Reflection.BindingFlags.Default, null, objParams, null, new object[0] { });
                }
                catch (System.Exception err)
                {
                    string strMsg = err.Message.ToString();
                    MessageBox.Show(strMsg, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (obj == null) return;
                //�򿪴���
                ((Form)obj).ShowDialog();
                Type objType = obj.GetType();
                System.Reflection.PropertyInfo objMi = objType.GetProperty("m_StrRecordID");
                string strAddBillRecordID = objMi.GetValue(obj, null).ToString();
                if (strAddBillRecordID.Trim() != "")
                {
                    m_lngSaveAddBill(strAddBillRecordID.Trim());
                }
                return;
            }
            #endregion

            /// <summary>
            /// ���渽�ӵ���
            /// </summary>
            /// <param name="p_strAddBillRecordID">���ӵ���ID</param>
            /// <returns></returns>
            private long m_lngSaveAddBill(string p_strAddBillRecordID)
            {
                string[,] strValuesArr = new string[1, 5];

                //clsRelation_StrArr objAddBill = new clsRelation_StrArr();
                //objAddBill.m_strValues = new string[1, 5];

                strValuesArr[0, 0] = m_strRelationID.Trim();
                strValuesArr[0, 1] = "2";
                strValuesArr[0, 2] = "";
                strValuesArr[0, 3] = this.m_objOrder.m_strOrderID.Trim();
                strValuesArr[0, 4] = p_strAddBillRecordID.Trim();

                DataTable dt = new DataTable();
                dt.Columns.Add("col0", typeof(string));
                dt.Columns.Add("col1", typeof(string));
                dt.Columns.Add("col2", typeof(string));
                dt.Columns.Add("col3", typeof(string));
                dt.Columns.Add("col4", typeof(string));

                DataRow dr = dt.NewRow();
                dr["col0"] = m_strRelationID.Trim();
                dr["col1"] = "2";
                dr["col2"] = "";
                dr["col3"] = this.m_objOrder.m_strOrderID.Trim();
                dr["col4"] = p_strAddBillRecordID.Trim();
                dt.LoadDataRow(dr.ItemArray, true);
                dt.AcceptChanges();

                try
                {
                    //clsRelationService_STR objAddBillSRV = new clsRelationService_STR();
                    long lngRes = 0;
                    if (m_strRelationID.Trim() != "")
                    {
                        lngRes = (new weCare.Proxy.ProxyIP02()).Service.clsRelation_StrArr_m_lngUpdate(dt);

                    }
                    else
                    {
                        lngRes = (new weCare.Proxy.ProxyIP02()).Service.clsRelation_StrArr_m_lngInsertRelation(dt);
                    }
                    return lngRes;
                }
                catch
                {
                    return -1;
                }
            }

            /// <summary>
            /// ��ѯҽ���ĸ��ӵ���
            /// </summary>
            /// <param name="p_strOrderID">ҽ��ID</param>
            /// <param name="dtbAddBills">���ӵ����б�</param>
            /// <returns></returns>
            public long m_lngGetAddBillByOrderID(string p_strOrderID, out DataTable dtbCurrAddBill)
            {
                dtbCurrAddBill = new DataTable();
                try
                {
                    //clsRelation_DTable dtbRes = new clsRelation_DTable();
                    long lngRes = (new weCare.Proxy.ProxyIP02()).Service.clsRelation_DTable_m_lngGetRelation(out dtbCurrAddBill, " sourceitemid_vchr='" + p_strOrderID.Trim() + "'");
                    //dtbCurrAddBill = dtbRes.m_dtbValues;
                    return lngRes;
                }
                catch
                {
                    return -1;
                }
            }

            public override string ToString()
            {
                //return m_objOrderCate.m_strName + " : " + m_objOrder.m_intRecipenNo + "-" + m_objOrder.m_strName;
                return m_objOrder.m_intRecipenNo + ":" + m_objOrder.m_strName + "\n[" + m_objOrderCate.m_strName + "]";
            }

        }

        public void m_ctxOtherBill_Popup(ContextMenu objMenu)
        {
            clsOtherBillInfo.s_objPatient = this.m_frmInput.m_ctlPatient.m_objPatient;

            objMenu.MenuItems.Clear();
            for (int i = 0; i < m_arlOtherBillInfo.Count; i++)
            {
                string strText = ((int)(i + 1)).ToString() + "\t" + (m_arlOtherBillInfo[i] as clsOtherBillInfo).ToString();
                MenuItem objItem = new MenuItem(strText);
                objItem.Checked = ((m_arlOtherBillInfo[i] as clsOtherBillInfo).AttachOrderCount > 0);
                objItem.Click += new EventHandler(objItem_Click);
                objItem.Index = i;

                objMenu.MenuItems.Add(objItem);
            }
        }
        private void objItem_Click(object sender, EventArgs e)
        {
            MenuItem objItem = sender as MenuItem;
            if (objItem == null) return;
            int intIndex = objItem.Index;
            if ((intIndex >= 0) && (intIndex < m_arlOtherBillInfo.Count))
            {
                clsOtherBillInfo objBillInfo = ((clsOtherBillInfo)m_arlOtherBillInfo[intIndex]);
                objBillInfo.m_ParentForm = this.m_frmInput;
                objBillInfo.m_ParentForm.objController = this.m_frmInput.objController;
                objBillInfo.m_mthShowUI(m_frmInput.LoginInfo);
            }
        }

        public void m_lblOtherBill_Click(object sender, System.EventArgs e)
        {
            if (m_arlOtherBillInfo.Count > 0)
            {
                try
                {
                    m_frmInput.m_ctxOtherBill.Show(m_frmInput.m_lblOtherBill, new Point(m_frmInput.m_lblOtherBill.Location.X, 0));
                }
                catch//(System.Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion
        #region ����
        public void m_mthSetGridRowColor(int intType, int intStatus, int intRow)
        {
            Color clrBack, clrFore;
            m_frmInput.m_mthGetColorByStatus(intType, intStatus, out clrBack, out clrFore);
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.ForeColor = clrFore;
            style1.BackColor = clrBack;
            m_frmInput.m_dtvOrder.Rows[intRow].DefaultCellStyle = style1;
        }
        public void m_mthRefreshGridColor()
        {
            try
            {
                for (int i = 0; i < m_frmInput.m_arlOrder.Count; i++)
                {
                    int intStatus = (m_frmInput.m_arlOrder[i] as clsBIHOrder).m_intStatus;
                    int intType = (m_frmInput.m_arlOrder[i] as clsBIHOrder).m_intExecuteType;
                    m_mthSetGridRowColor(intType, intStatus, i);
                }
            }
            catch
            { }
        }

        /// <summary>
        /// ��֤������ҽ��
        /// ҵ��˵��: ������ҽ�������/����/������λ���붼�ǡ�Сʱ��!
        /// </summary>
        /// <param name="p_strFreqID"></param>
        /// <returns></returns>
        public bool PassConOrder(string p_strFreqID)
        {
            bool blnRes = true;
            if (m_strConfreqID == "") m_strConfreqID = new clsDcl_ExecuteOrder().m_strGetConfreqID();
            if (m_strConfreqID == p_strFreqID.Trim())
            {
                if (blnRes && m_frmInput.m_ctlOrderDetail.m_txtDosageUnit.Text.Trim() != "Сʱ")
                {
                    MessageBox.Show("������ҽ��������������������λ����Ϊ��Сʱ��!", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    blnRes = false;
                }
                if (blnRes && m_frmInput.m_ctlOrderDetail.m_txtUseUnit.Text.Trim() != "Сʱ")
                {
                    MessageBox.Show("������ҽ��������������������λ����Ϊ��Сʱ��!", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    blnRes = false;
                }
                if (blnRes && m_frmInput.m_ctlOrderDetail.m_txtGetUnit.Text.Trim() != "Сʱ")
                {
                    MessageBox.Show("������ҽ��������������������λ����Ϊ��Сʱ��!", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    blnRes = false;
                }
            }
            return blnRes;
        }
        /// <summary>
        /// ʱ������ת��
        /// </summary>
        /// <param name="dtValue"></param>
        /// <returns></returns>
        public string DateTimeToString(DateTime dtValue)
        {
            //if(dtValue.Date==clsBIHOrder.m_dtNullDate.Date)
            if (dtValue.Date == DateTime.MinValue)
                return "";
            else
                return dtValue.ToString("yy-MM-dd HH:mm");
        }
        /// <summary>
        /// ˢ���б��ҽ��
        /// </summary>
        /// <param name="intRow"></param>
        public void m_mthRefreshGrid(int intRow)
        {
            //if((intRow>=0) && (intRow<m_frmInput.m_arlOrder.Count) && (intRow<m_frmInput.m_dtgOrder.RowCount))
            //{
            //    System.Data.DataRow objRow=new com.digitalwave.controls.datagrid.clsDataGridProxy(m_frmInput.m_dtgOrder).m_objDataTable.Rows[intRow];
            //    clsBIHOrder objOrder=(m_frmInput.m_arlOrder[intRow] as clsBIHOrder);

            //    objRow=m_objGetDataRow(intRow+1,objOrder,objRow);

            //}
        }

        /// <summary>
        /// ��ȡ��ǰ�Ƿ����ҽ��
        /// </summary>
        /// <returns></returns>
        public bool m_blnExistCurrentOrder()
        {
            //if((m_frmInput.m_objCurrentOrder!=null) && (m_frmInput.m_intCurrentRow>=0) && (m_frmInput.m_intCurrentRow<m_frmInput.m_arlOrder.Count))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            if (this.m_frmInput.m_dtvOrder.SelectedRows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ��ʾ�Ի���
        /// </summary>
        /// <param name="strMsg">��ʾ��Ϣ���ݴ�</param>
        private void m_mthShowMessage(string strMsg)
        {
            m_frmInput.m_mthShowMessage(strMsg);
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

        //Add By jli in 2005-03-14
        /// <summary>
        /// ��ȡͬ���ŵ�ҽ������
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_SameOrdersArr">���ص�ͬ���ŵ�ҽ������</param>
        /// <returns></returns>
        public long GetSameOrders(string p_strOrderID, out clsBIHOrder[] p_SameOrdersArr)
        {
            p_SameOrdersArr = new clsBIHOrder[0];
            return 0;
        }

        //Add End

        /// <summary>
        /// ������֤
        /// ҵ��˵����ͬ�������÷���Ƶ�ʱ�����ͬ��[ֻ���û���ύ���½����˻ص�ҽ������]
        /// </summary>
        /// <param name="intRecipeNO">ҽ����¼Vo</param>
        /// <returns></returns>
        private bool PassSameRecipeNO(clsBIHOrder p_objBIHOrder)
        {
            if (p_objBIHOrder == null) return true;
            clsBIHOrder[] objBIHOrderArr = null;
            int[] intArr = { 0 };
            long lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderByPatient(p_objBIHOrder.m_strRegisterID, p_objBIHOrder.m_strPatientID, intArr, false, 0, false, out objBIHOrderArr);
            if (lngRes > 0 && objBIHOrderArr != null)
            {
                for (int i = 0; i < objBIHOrderArr.Length; i++)
                {
                    if (objBIHOrderArr[i].m_strOrderID != p_objBIHOrder.m_strOrderID && (objBIHOrderArr[i].m_intStatus == 0 || objBIHOrderArr[i].m_intStatus == 7) && objBIHOrderArr[i].m_intRecipenNo == p_objBIHOrder.m_intRecipenNo)
                    {
                        if (objBIHOrderArr[i].m_strDosetypeID != p_objBIHOrder.m_strDosetypeID || objBIHOrderArr[i].m_strExecFreqID != p_objBIHOrder.m_strExecFreqID)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// ����Button
        /// </summary>
        private void ResetButtonToEnable()
        {
            /*------------------------------------------------------------------------>
            m_frmInput.m_cmdDelete.Enabled =false;
            m_frmInput.m_cmdSave.Enabled =false;
            //m_frmInput.m_cmdStop.Enabled =false;
            //Add by jli in 2005-03-24
            //			m_frmInput.m_ctlOrderDetail.m_txbFinishTime.Enabled=false;
            //			m_frmInput.m_ctlOrderDetail.m_txbFinishTime.ReadOnly=true;
            //			m_frmInput.m_ctlOrderDetail.m_txbFinishTime.Text="";
            //			m_frmInput.m_ctlOrderDetail.m_txbFinishTime.BackColor=SystemColors.Control;
            //			m_frmInput.m_cmdStop.Text=@"ֹͣ(F4)";
            //Add End
            //m_frmInput.m_cmdRetract.Enabled =false;
            m_frmInput.m_cmdBlankOut.Enabled =false;
            m_frmInput.m_cmdSub.Enabled=false;
            ---------------------------------------------------------------------------------*/
            m_frmInput.m_cmdDelete.Enabled = true;
            m_frmInput.m_cmdSave.Enabled = true;
            m_frmInput.m_cmdBlankOut.Enabled = true;
            m_frmInput.m_cmdSub.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_txtSample.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_txtCheck.Enabled = true;
        }
        /// <summary>
        /// ����Button
        /// </summary>
        public void SetButtonToEnable()
        {
            ResetButtonToEnable();
            if (m_frmInput.m_objCurrentOrderValue == null) return;
            //ִ��״̬	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;7-�˻�;}
            //��ʼ��ҽ���������ؼ�
            //ResetTheDetailControl();
            /*<==========================*/
            //Add by jli in 2005-03-21
            //if(m_frmInput.m_objCurrentOrderValue.m_strParentID.Trim()!="")
            //{
            //    if(m_frmInput.m_objCurrentOrderValue.m_intStatus==0 || m_frmInput.m_objCurrentOrderValue.m_intStatus==7)
            //    {
            //        m_frmInput.m_ctlOrderDetail.m_txtOrderName.Enabled=true;
            //        m_frmInput.m_ctlOrderDetail.m_txtOrderName.BackColor=Color.White;
            //    }
            //    else
            //    {
            //        m_frmInput.m_ctlOrderDetail.m_txtOrderName.Enabled=false;
            //        m_frmInput.m_ctlOrderDetail.m_txtOrderName.BackColor=SystemColors.Control;
            //    }
            //    m_frmInput.m_ctlOrderDetail.m_txtExecuteFreq.Enabled=false;
            //    m_frmInput.m_ctlOrderDetail.m_txtExecuteFreq.BackColor=SystemColors.Control;
            //    m_frmInput.m_ctlOrderDetail.m_txtDosageType.Enabled=false;
            //    m_frmInput.m_ctlOrderDetail.m_txtDosageType.BackColor=SystemColors.Control;
            //    m_frmInput.m_cmdSub.Enabled=false;
            //}
            //else if(m_frmInput.m_objCurrentOrderValue.m_intStatus==0 || m_frmInput.m_objCurrentOrderValue.m_intStatus==7)
            //{
            //    m_frmInput.m_cmdSub.Enabled=true;
            //    m_frmInput.m_ctlOrderDetail.m_txtOrderName.Enabled=true;
            //    m_frmInput.m_ctlOrderDetail.m_txtOrderName.BackColor=Color.White;
            //    m_frmInput.m_ctlOrderDetail.m_txtExecuteFreq.Enabled=true;
            //    m_frmInput.m_ctlOrderDetail.m_txtExecuteFreq.BackColor=Color.White;
            //    m_frmInput.m_ctlOrderDetail.m_txtDosageType.Enabled=true;
            //    m_frmInput.m_ctlOrderDetail.m_txtDosageType.BackColor=Color.White;
            //}
            //else if(m_frmInput.m_objCurrentOrderValue.m_intStatus==1
            //    || m_frmInput.m_objCurrentOrderValue.m_intStatus==2
            //    || m_frmInput.m_objCurrentOrderValue.m_intStatus==5)
            //{
            //    m_frmInput.m_ctlOrderDetail.m_cboExecuteType.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtRecipeNo.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtOrderName.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtOrderName2.Enabled = false;
            //    //m_txtOrderName2
            //    m_frmInput.m_ctlOrderDetail.m_txtDosage.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtDosageType.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtExecuteFreq.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtGet.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtDays.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtATTACHTIMES_INT.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtDoctorList.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_cboRateType.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtEntrust.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_dtStartTime2.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_dtFinishTime2.Enabled = false;
            //}
            //else
            //{
            //    m_frmInput.m_ctlOrderDetail.m_cboExecuteType.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtRecipeNo.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtOrderName.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtOrderName2.Enabled = false;
            //    //m_txtOrderName2
            //    m_frmInput.m_ctlOrderDetail.m_txtDosage.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtDosageType.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtExecuteFreq.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtGet.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtDays.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtATTACHTIMES_INT.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtDoctorList.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_cboRateType.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_txtEntrust.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_dtStartTime2.Enabled = false;
            //    m_frmInput.m_ctlOrderDetail.m_dtFinishTime2.Enabled = false;
            //}
            //Add End  
            m_frmInput.m_cmdSub.Enabled = false;//��ҽ��(F3)
            m_frmInput.m_cmdDelete.Enabled = false;//ɾ��(Del)
            m_frmInput.m_cmdBlankOut.Enabled = false;//����(F5)
            m_frmInput.m_cmdSave.Enabled = false;//����(F4)
            //��ҽ����ť״̬
            //1����ҽ�������ڡ��½���״̬��¼�룻2�������˻ء���ҽ������ҽ��ʱ������������ҽ����������������ҽ����
            if (m_frmInput.m_objCurrentOrderValue.m_intStatus == 0)
            {
                m_frmInput.m_cmdSub.Enabled = true;//��ҽ��(F3)
            }
            else if (m_frmInput.m_objCurrentOrderValue.m_intStatus == 7)
            {
                m_frmInput.m_cmdSub.Enabled = true;//��ҽ��(F3)
                if (m_frmInput.m_objCurrentOrderValue.m_strParentID != null)
                {
                    if (!m_frmInput.m_objCurrentOrderValue.m_strParentID.ToString().Trim().Equals(""))
                    {
                        m_frmInput.m_cmdSub.Enabled = false;//��ҽ��(F3)
                    }
                }

            }


            switch (m_frmInput.m_objCurrentOrderValue.m_intStatus)
            {
                case -1:

                    m_frmInput.m_cmdDelete.Enabled = false;//ɾ��(Del)
                    m_frmInput.m_cmdBlankOut.Enabled = false;//����(F5)
                    m_frmInput.m_cmdSave.Enabled = false;//����(F4)

                    break;
                case 0:

                    m_frmInput.m_cmdDelete.Enabled = true;//ɾ��(Del)
                    m_frmInput.m_cmdBlankOut.Enabled = true;//����(F5)
                    m_frmInput.m_cmdSave.Enabled = true;//����(F4)
                    break;
                case 1:

                    m_frmInput.m_cmdDelete.Enabled = false;//ɾ��(Del)
                    m_frmInput.m_cmdBlankOut.Enabled = true;//����(F5)
                    m_frmInput.m_cmdSave.Enabled = true;//����(F4)
                    break;
                case 2://��������ͣ
                    if (m_frmInput.m_objCurrentOrderValue.m_intExecuteType == 1)
                    {
                        m_frmInput.m_cmdStop.Enabled = true;
                    }

                    m_frmInput.m_cmdDelete.Enabled = false;//ɾ��(Del)
                    m_frmInput.m_cmdBlankOut.Enabled = false;//����(F5)
                    m_frmInput.m_cmdSave.Enabled = false;//����(F4)
                    break;
                case 3:

                    m_frmInput.m_cmdDelete.Enabled = false;//ɾ��(Del)
                    m_frmInput.m_cmdBlankOut.Enabled = false;//����(F5)
                    m_frmInput.m_cmdSave.Enabled = false;//����(F4)
                    break;
                case 4:

                    m_frmInput.m_cmdDelete.Enabled = false;//ɾ��(Del)
                    m_frmInput.m_cmdBlankOut.Enabled = false;//����(F5)
                    m_frmInput.m_cmdSave.Enabled = false;//����(F4)
                    break;
                case 5:

                    m_frmInput.m_cmdDelete.Enabled = false;//ɾ��(Del)
                    m_frmInput.m_cmdBlankOut.Enabled = false;//����(F5)
                    m_frmInput.m_cmdSave.Enabled = true;//����(F4)
                    m_frmInput.m_cmdBlankOut.Enabled = true;
                    break;
                case 6:

                    m_frmInput.m_cmdDelete.Enabled = false;//ɾ��(Del)
                    m_frmInput.m_cmdBlankOut.Enabled = false;//����(F5)
                    m_frmInput.m_cmdSave.Enabled = false;//����(F4)
                    break;
                case 7:
                    //m_frmInput.m_cmdSub.Enabled = false;//��ҽ��(F3)
                    m_frmInput.m_cmdDelete.Enabled = true;//ɾ��(Del)
                    m_frmInput.m_cmdBlankOut.Enabled = true;//����(F5)
                    m_frmInput.m_cmdSave.Enabled = true;//����(F4)
                    break;
                default:

                    break;
            }

            //���Ͽ���
            if (m_frmInput.m_blBlankOutControl == false)
            {
                m_frmInput.m_cmdBlankOut.Enabled = false;
            }

        }
        /// <summary>
        /// ��ʼ��ҽ���������ؼ�
        /// </summary>
        public void ResetTheDetailControl()
        {
            //��ʼ������ؼ�
            m_frmInput.m_ctlOrderDetail.m_cboExecuteType.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_txtRecipeNo.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_txtOrderName.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_txtOrderName2.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_txtDosage.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_txtDosageType.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_txtExecuteFreq.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_txtGet.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_txtDays.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_txtATTACHTIMES_INT.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_txtDoctorList.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_cboRateType.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_txtEntrust.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_dtStartTime2.Enabled = true;
            m_frmInput.m_ctlOrderDetail.m_dtFinishTime2.Enabled = true;
        }

        /// <summary>
        /// ����ҽ���༭״̬��Ϣ	{0=����ʾ��1=������2=�༭��3=������ҽ������}
        /// </summary>
        /// <param name="p_intState">�༭״ֵ̬{0=����ʾ��1=������2=�༭��3=������ҽ����}</param>
        public void m_SetDisplayOrderEditState(int p_intState)
        {
            switch (p_intState)
            {
                case 0://����ʾ
                    m_frmInput.m_lblEditState.Visible = false;
                    break;
                case 1://����
                    m_frmInput.m_lblEditState.Visible = true;
                    if (m_frmInput.m_ctlOrderDetail.m_txtFatherOrder.Text.Trim() != "")
                    {
                        m_frmInput.m_lblEditState.Text = "�����ҽ��";
                    }
                    else
                    {
                        m_frmInput.m_lblEditState.Text = "���ҽ��";
                    }
                    break;
                case 2://�༭					
                    m_frmInput.m_lblEditState.Visible = true;
                    if (m_frmInput.m_ctlOrderDetail.m_txtFatherOrder.Text.Trim() != "")
                    {
                        m_frmInput.m_lblEditState.Text = "�޸���ҽ��";
                    }
                    else
                    {
                        m_frmInput.m_lblEditState.Text = "�޸�ҽ��";
                    }
                    break;
                case 3://ֻ��
                    m_frmInput.m_lblEditState.Visible = true;
                    m_frmInput.m_lblEditState.Text = "������ҽ��";
                    break;
                default://����ʾ
                    m_frmInput.m_lblEditState.Visible = false;
                    break;
            }
        }
        #region ����ToolTip
        #region Label �ؼ�
        //		/// <summary>
        //		/// ����ToolTip
        //		/// </summary>
        //		/// <param name="p_objItem">ҽ����¼����</param>
        //		public void m_mthSetToolTips(clsBIHOrder p_objItem)
        //		{
        //			if(p_objItem==null) return;
        //			string strToolTip =strGetToolTipText(p_objItem);
        //			m_frmInput.m_lblToolTip.Text =strToolTip;
        //			m_frmInput.m_lblToolTip.Visible =true;
        //			//m_frmInput.toolTip1.SetToolTip(m_frmInput.m_dtgOrder,strGetToolTipText(p_objItem));
        //			//m_frmInput.toolTip1.Active =true;
        //		}
        //		/// <summary>
        //		/// ��ȡToolTip��ʾ���ı�
        //		/// </summary>
        //		/// <param name="p_objItem">ҽ����¼����</param>
        //		/// <returns></returns>
        //		public string strGetToolTipText(clsBIHOrder p_objItem)
        //		{
        //			string strResult ="",strRow ="",strTem ="";
        //			clsT_aid_bih_orderdic_charge_VO[] objItemArr;
        //			clsDcl_CommitOrder objTem =new clsDcl_CommitOrder();
        //			long lngRes =objTem.m_lngGetOrderdicChargeByOrderID(p_objItem.m_strOrderID,out objItemArr);
        //			double dblNumber =0;
        //			dblNumber =System.Convert.ToDouble(p_objItem.m_dmlGet);
        //			if(lngRes>0 && objItemArr!=null && objItemArr.Length>0)
        //			{
        //				for(int i=0;i<objItemArr.Length;i++)
        //				{					
        //					strTem ="��Ŀ���ƣ�" + objItemArr[i].m_strItemName;
        //					strRow =strTem;//.PadRight(50,' ');
        //					strTem ="\r\n    ���շ���Ŀ��" + objItemArr[i].m_strIsChiefItem;
        //					strRow +=strTem;
        //					double dblNum =0;//����
        //					if(objItemArr[i].m_objChargeItem.m_strITEMID_CHR.Trim()==objItemArr[i].m_strChiefItemID.Trim())//�Ƿ����շ���Ŀ
        //					{
        //						dblNum =dblNumber;						
        //					}
        //					else
        //					{
        //						dblNum = System.Convert.ToDouble(m_objInputOrder.m_dmlGetChargeNotMainItem(p_objItem.m_strExecFreqID,objItemArr[i]));
        //					}
        //					strTem ="\r\n    ������" + dblNum.ToString();
        //					strRow +=strTem;//.PadRight(15,' ');
        //					strTem ="\r\n    ���ۣ�" + objItemArr[i].m_objChargeItem.m_dblMinPrice.ToString("0.0000");
        //					strRow +=strTem;//.PadRight(15,' ');
        //					strTem ="\r\n    �ܽ�" + (objItemArr[i].m_objChargeItem.m_dblMinPrice * dblNum).ToString("0.00");
        //					strRow +=strTem;
        //					strResult +=strRow +"\r\n";
        //					strResult +="\r\n";
        //				}
        //			}
        //			return strResult;
        //		}
        #endregion
        #region ListViw �ؼ�
        /// <summary>
        /// �洢������Ϣ	[��������] {ҽ��ID[�ؼ���],���ö���(ArrayList)}
        /// </summary>
        public System.Collections.Hashtable m_htbToolTip = new Hashtable();
        /// <summary>
        /// ��ListView����ϢToolTip
        /// </summary>
        /// <param name="p_objItem">ҽ����¼����</param>
        /// <param name="p_lsvToolTip">ListView �ؼ�</param>
        /// <returns></returns>
        /// <remarks>
        /// ҵ��˵����
        ///		1���Ա��Ȳ���ȡ���˷��õ�ҽ������ҽ����ʿ���鿴�շ���ϸʱ������Ӧ����ʾ������
        /// </remarks>
        public void m_DisPlayToolTipListView(clsBIHOrder p_objItem, System.Windows.Forms.ListView p_lsvToolTip)
        {
            p_lsvToolTip.Items.Clear();
            if (p_objItem == null || p_objItem.m_strOrderID == null || p_objItem.m_strOrderID.Trim() == "") return;
            if ((p_objItem.m_intExecuteType == 2 && p_objItem.m_intIsRepare >= 3 && p_objItem.m_intIsRepare <= 4))
            {
                if (m_htbToolTip.ContainsKey(p_objItem.m_strOrderID.Trim()))
                {
                    m_htbToolTip.Remove(p_objItem.m_strOrderID.Trim());
                }
                p_lsvToolTip.Visible = true;
                return;
            }
            else
            {
                if (!m_htbToolTip.ContainsKey(p_objItem.m_strOrderID.Trim()))
                {
                    FillToolTipHashtable(p_objItem);
                }
            }
            if (m_htbToolTip.ContainsKey(p_objItem.m_strOrderID.Trim()))
            {
                ArrayList alItem = new ArrayList();
                alItem = (m_htbToolTip[p_objItem.m_strOrderID.Trim()] as ArrayList);
                if (alItem != null && alItem.Count > 0)
                {
                    clsChargeForDisplay[] objItemArr = (clsChargeForDisplay[])(alItem.ToArray(typeof(clsChargeForDisplay)));
                    //��ʾListView
                    m_objInputOrder.DisplayCharge(objItemArr, p_lsvToolTip);
                    //��һ��ͳ�ƽ��
                    //�ϼƽ��
                    double m_dblSum = 0;
                    for (int i = 0; i < objItemArr.Length; i++)
                    {
                        if (!double.IsInfinity(objItemArr[i].m_dblMoney))
                            m_dblSum += objItemArr[i].m_dblMoney;
                    }

                    if (objItemArr.Length > 0)
                    {

                        ListViewItem item1 = new ListViewItem("");
                        item1.SubItems.Add("");
                        item1.SubItems.Add("");
                        item1.SubItems.Add("");
                        item1.SubItems.Add("�ϼ�:");
                        item1.SubItems.Add(m_dblSum.ToString("0.00"));
                        item1.ForeColor = Color.Red;
                        p_lsvToolTip.Items.Add(item1);
                    }
                    /*<===============*/
                }
            }

            /* ҽ��״̬������ʾ������ɫ����*/

            //for(int i1=0;i1<p_lsvToolTip.Items.Count;i1++)
            //{
            //    if(((clsChargeForDisplay)p_lsvToolTip.Items[i1].Tag).m_strYBClass.ToString().Trim().Equals(""))//��ҽ��
            //    {
            //        p_lsvToolTip.Items[i1].BackColor=Color.White;	
            //    }
            //    else if(((clsChargeForDisplay)p_lsvToolTip.Items[i1].Tag).m_strYBClass.ToString().Trim().Equals("����"))
            //    {
            //        p_lsvToolTip.Items[i1].BackColor=Color.SkyBlue;	
            //    }
            //    else if(((clsChargeForDisplay)p_lsvToolTip.Items[i1].Tag).m_strYBClass.ToString().Trim().Equals("����"))
            //    {
            //        p_lsvToolTip.Items[i1].BackColor=Color.Yellow;
            //    }
            //    else if(((clsChargeForDisplay)p_lsvToolTip.Items[i1].Tag).m_strYBClass.ToString().Trim().Equals("����"))
            //    {
            //        p_lsvToolTip.Items[i1].BackColor=Color.Green;
            //    }

            //}
            /* <<======================================= */
            //p_lsvToolTip.Visible =true;
        }
        /// <summary>
        /// ����ϣ����ֵ ToopTip
        /// </summary>
        /// <param name="p_objItem">ҽ����¼����</param>
        /// <returns></returns>
        private void FillToolTipHashtable(clsBIHOrder p_objItem)
        {


            long lngRes = 0;
            if (p_objItem == null || p_objItem.m_strOrderID == null || p_objItem.m_strOrderID.Trim() == "") return;
            //��ȡҽ��ID
            string strOrderID = p_objItem.m_strOrderID;
            ////���շѵ�����
            //double dblNumber =System.Convert.ToDouble(p_objItem.m_dmlGet);
            //clsT_aid_bih_ordercate_VO objOrdercate;
            //lngRes =m_objInputOrder.m_lngGetAidOrderCateByID(p_objItem.m_strOrderDicCateID,out objOrdercate);
            //if(lngRes>0 && objOrdercate!=null && objOrdercate.m_intDOSAGEVIEWTYPE==2) dblNumber =1;
            ////ִ��Ƶ��ID
            //string strFreqID =p_objItem.m_strExecFreqID;
            ////�÷�ID
            //string strUsageID =p_objItem.m_strDosetypeID;
            ////�Ƿ��Ӽ�ҽ��	{0=���Ӽ�ҽ��;1=�Ӽ�ҽ��}
            //int intIsSonOrder =0;
            //if(p_objItem.m_strParentID!=null && p_objItem.m_strParentID.Trim()!="")
            //    intIsSonOrder =1;

            clsChargeForDisplay[] objItemArr;
            //lngRes =m_objInputOrder.m_lngGetBIHCharge(strOrderID,intIsSonOrder,dblNumber,strFreqID,strUsageID,out objItemArr);
            DataTable m_dtChargeList;
            m_objInputOrder.m_lngGetBIHChargeFromDEPT(strOrderID, out m_dtChargeList);
            m_mthGetChargeListFromDateTable(m_dtChargeList, out objItemArr);
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
        /// ���ñ�ת��Ϊ������ϸ����
        /// </summary>
        /// <param name="objRow"></param>
        /// <param name="m_arrObjItem"></param>
        public void m_mthGetChargeListFromDateTable(DataTable m_dtChargeList, out clsChargeForDisplay[] m_arrObjItem)
        {

            m_arrObjItem = new clsChargeForDisplay[m_dtChargeList.Rows.Count];
            for (int i = 0; i < m_dtChargeList.Rows.Count; i++)
            {
                DataRow objRow = m_dtChargeList.Rows[i];

                m_arrObjItem[i] = new clsChargeForDisplay();
                m_arrObjItem[i].m_strChargeID = clsConverter.ToString(objRow["CHARGEITEMID_CHR"]).Trim();
                m_arrObjItem[i].m_strITEMCODE_VCHR = clsConverter.ToString(objRow["ITEMCODE_VCHR"]).Trim();
                //�շ���Ŀ����
                m_arrObjItem[i].m_strName = clsConverter.ToString(objRow["CHARGEITEMNAME_CHR"]).Trim();
                double dblNum = 0;
                //if (objMedicineItemArr[i1].m_objChargeItem.m_strITEMID_CHR.Trim() == objMedicineItemArr[i1].m_strChiefItemID.Trim())//�Ƿ����շ���Ŀ
                //{
                //    dblNum = p_dblDraw;
                //    //�շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                //    m_arrObjItem[i].m_intType = 2;
                //}
                //else
                //{
                //    dblNum = System.Convert.ToDouble(m_dmlGetChargeNotMainItem(objRecipeFreq, objMedicineItemArr[i1]));
                //    //�շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                //    m_arrObjItem[i].m_intType = 1;
                //}
                //����
                if (!objRow["UNITPRICE_DEC"].ToString().Trim().Equals(""))
                {
                    m_arrObjItem[i].m_dblPrice = double.Parse(clsConverter.ToString(objRow["UNITPRICE_DEC"]).Trim());
                }
                if (!objRow["AMOUNT_DEC"].ToString().Trim().Equals(""))
                {
                    dblNum = double.Parse(clsConverter.ToString(objRow["AMOUNT_DEC"]).Trim());
                }
                /*<---------------------------------*/
                //����
                m_arrObjItem[i].m_dblDrawAmount = dblNum;

                //�ϼƽ��
                m_arrObjItem[i].m_dblMoney = m_arrObjItem[i].m_dblPrice * dblNum;
                //�������� {-1=���÷��շѣ�ҩƷ�շѵȣ�;0=������;1=ȫ������;2-��������}
                if (!objRow["CONTINUEUSETYPE_INT"].ToString().Trim().Equals(""))
                {
                    m_arrObjItem[i].m_intCONTINUEUSETYPE_INT = int.Parse(objRow["CONTINUEUSETYPE_INT"].ToString().Trim());
                }

                //�Ƿ�������ҽ��	{0=��1=��} ������ҽ������ʾҩƷ������Ϣ��
                // m_arrObjItem[i].m_intIsContinueOrder = (blnIsConOrder) ? (1) : (0);
                //�Ƿ�ȱҩ
                // m_arrObjItem[i].m_strNoqtyFLag = objMedicineItemArr[i1].m_strNoqtyFLag;
                // ���Ͽ�������
                m_arrObjItem[i].m_strClacarea_chr = clsConverter.ToString(objRow["CLACAREA_CHR"]).Trim();
                m_arrObjItem[i].m_strClacareaName_chr = clsConverter.ToString(objRow["deptname_vchr"]).Trim();
                //�ݴ�סԺ������Ŀ�շ���Ŀִ�пͻ������ˮ��
                m_arrObjItem[i].m_strSeq_int = clsConverter.ToString(objRow["SEQ_INT"]).Trim(); ;
                m_arrObjItem[i].m_strYBClass = clsConverter.ToString(objRow["INSURACEDESC_VCHR"]).Trim();
                m_arrObjItem[i].m_strUNIT_VCHR = clsConverter.ToString(objRow["UNIT_VCHR"]).Trim();
                //�շ�����Դ�� 0-��������Ŀ��1-������Ŀ,2���������÷���3���Զ����¿�
                if (!objRow["FLAG_INT"].ToString().Trim().Equals(""))
                    m_arrObjItem[i].m_intType = clsConverter.ToInt(objRow["FLAG_INT"].ToString().Trim());
                // סԺ������Ŀ�շ���Ŀִ�пͻ���VO
                //objItem.m_objORDERCHARGEDEPT_VO = objMedicineItemArr[i1].m_objORDERCHARGEDEPT_VO;
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


        #endregion
        #region m_strGetStatusMessage
        /// <summary>
        /// ��ȡҽ����ִ��״̬	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;7-�˻�;}
        /// </summary>
        /// <param name="intStatus">ִ��״̬	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;7-�˻�;}</param>
        /// <returns>ִ��״̬����</returns>
        public string m_strGetStatusMessage(int intStatus)
        {
            string strMessage = "";
            switch (intStatus)
            {
                case 0:
                    strMessage = "�½�";
                    break;
                case 1:
                    strMessage = "���ύ";
                    break;
                case 2:
                    strMessage = "��ִ��";
                    break;
                case 3:
                    strMessage = "��ֹͣ";
                    break;
                case 4:
                    strMessage = "������";
                    break;
                case 5:
                    strMessage = "������ύ";
                    break;
                case 6:
                    strMessage = "�����ֹͣ";
                    break;
                case -1:
                    strMessage = "������";
                    break;
                default:
                    strMessage = "δ֪";
                    break;
            }
            return strMessage;

        }
        #endregion
        #region m_strGetOrderMessage
        /// <summary>
        /// ��ȡҽ����״̬��Ϣ	[��ʽ�������� ���ύ��]
        /// </summary>
        /// <param name="objOrder"></param>
        /// <returns>������Ϣ</returns>
        public string m_strGetOrderMessage(clsBIHOrder objOrder)
        {
            string strType = "";
            if (objOrder == null)
                strType = "";
            else if (objOrder.m_intExecuteType == 1)
                strType = "����";
            else if (objOrder.m_intExecuteType == 2)
                strType = "��ʱ";
            else
                strType = "";

            string strStatus = "";
            if (objOrder == null)
            {
                strStatus = "";
            }
            else
            {
                strStatus = objOrder.m_strStatusName; //m_strGetStatusMessage(objOrder.m_intStatus);
            }
            string strBackReason = "";
            if (objOrder != null && objOrder.m_intStatus == 7)
            {
                strBackReason = "ԭ��:" + ((objOrder.m_strBACKREASON == null) ? ("") : (objOrder.m_strBACKREASON.Trim()));
            }
            return strType + "  " + strStatus + " " + strBackReason;
        }
        #endregion
        #region m_objGetDataRow
        /// <summary>
        /// ��Order����DataRow,intNOֻ��ָ�����
        /// </summary>
        public DataRow m_objGetDataRow(int intNo, clsBIHOrder objOrder, DataRow objRow)
        {
            if (objRow == null)
            {
                //objRow =m_frmInput.m_dtgOrder.NewRow();
            }

            objRow["NO"] = intNo;
            objRow["RecipeNo"] = objOrder.m_intRecipenNo;
            objRow["Name"] = objOrder.m_strName;
            //�۸�
            //objRow["Price"] =objOrder.m_dmlPrice.ToString("0.0000");
            if (objOrder.m_intExecuteType == 1)
            {
                objRow["ExecuteType"] = "��";
            }
            else
            {
                if (objOrder.m_intExecuteType == 2)
                    objRow["ExecuteType"] = "��";
                else
                    objRow["ExecuteType"] = "";
            }
            if (objOrder.m_dmlDosage > 0)
            {
                objRow["Dosage"] = objOrder.m_dmlDosage.ToString() + " " + objOrder.m_strDosageUnit;
            }
            else
            {
                objRow["Dosage"] = "";
            }
            if (objOrder.m_dmlGet > 0)
            {
                objRow["Get"] = objOrder.m_dmlGet.ToString() + " " + objOrder.m_strGetunit;
            }
            else
            {
                objRow["Get"] = "";
            }
            //�ϼ�
            //objRow["TotalMoney"] =(double.Parse(objOrder.m_dmlGet.ToString()) * double.Parse(objOrder.m_dmlPrice.ToString())).ToString("0.00");
            objRow["Freq"] = objOrder.m_strExecFreqName;
            objRow["UseType"] = objOrder.m_strDosetypeName;
            if (objOrder.m_intISNEEDFEEL == 1)
                objRow["ISNEEDFEEL"] = "��";
            else
                objRow["ISNEEDFEEL"] = "";//��
            objRow["StartDate"] = DateTimeToString(objOrder.m_dtStartDate);
            objRow["Stoper"] = objOrder.m_strStoper;
            objRow["StopDate"] = DateTimeToString(objOrder.m_dtStopdate);
            objRow["ParentName"] = objOrder.m_strParentName;
            return objRow;
        }
        #endregion
        #endregion
        #region ��ӡҽ��
        /// <summary>
        /// ��ӡ��ʱҽ��
        /// </summary>
        public void m_PrintOrder()
        {
            clsBIHPatientInfo objPatient = m_frmInput.m_ctlPatient.m_objPatient;
            if (objPatient == null)
            {
                m_mthShowMessage("����ָ������!");
                m_frmInput.m_ctlPatient.Focus();
                return;
            }
            string strRegisterID = m_frmInput.m_ctlPatient.m_objPatient.m_strRegisterID;
            int intCount = 0, m_intBigRecipeNo = 0, intBackCout = 0;
            clsBIHOrder[] arrOrder = null;
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderByPatientAndState2(objPatient.m_strRegisterID, this.m_frmInput.LoginInfo.m_strEmpID, false, 0, out intCount, out m_intBigRecipeNo, out intBackCout, out m_intOutHisCout, out arrOrder);
            if (strRegisterID.Trim() == "") return;
            DataTable dtPatient = new DataTable();
            dtPatient.Columns.Add("PATIENTNAME");
            dtPatient.Columns.Add("PATIENTSEX");
            dtPatient.Columns.Add("PATIENTAGE");
            dtPatient.Columns.Add("CURRENTAREANAME");
            dtPatient.Columns.Add("CURRENTBEDNO");
            dtPatient.Columns.Add("INPATIENTID");
            DataRow row = dtPatient.NewRow();
            TimeSpan ts = DateTime.Now - m_frmInput.m_ctlPatient.m_objPatient.m_dtBorn;
            row["PATIENTNAME"] = m_frmInput.m_ctlPatient.m_objPatient.m_strPatientName;
            row["PATIENTSEX"] = m_frmInput.m_ctlPatient.m_objPatient.m_strSex;
            if (ts.Days < 30)
            {
                row["PATIENTAGE"] = "��";
            }
            else
            {
                row["PATIENTAGE"] = m_frmInput.m_ctlPatient.m_objPatient.m_strAge;
            }
            row["CURRENTAREANAME"] = m_frmInput.m_ctlPatient.m_objPatient.m_strAreaName;
            row["CURRENTBEDNO"] = m_frmInput.m_ctlPatient.m_objPatient.m_strBedName;
            row["INPATIENTID"] = m_frmInput.m_ctlPatient.m_objPatient.m_strInHospitalNo;
            dtPatient.Rows.Add(row);
            //��ʾ��ӡ����ҳ��
            frmPrintOrder objPrintOrder = new frmPrintOrder(strRegisterID, dtPatient, this.m_frmInput.m_htOrderCate, arrOrder, this.m_frmInput.m_objSpecateVo);
            objPrintOrder.m_strClass = m_frmInput.m_strView;
            objPrintOrder.ShowDialog();
        }
        #endregion
        #region ����ϵͳ����
        internal bool IsTabu = false;
        public void m_mthLoadSysConfig()
        {
            DataTable dt;
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_mthGetSysConfig(out dt, "");
            if (dt.Rows.Count > 0)
            {
                IsTabu = dt.Rows[0]["ISCHECKMED"].ToString().Trim() == "1";
            }
        }
        #endregion

        #region ���ͼ������뵥
        /// <summary>
        /// ��ü�����շ���ز���
        /// </summary>
        /// <param name="p_objBIHOrder"></param>
        /// <param name="p_dtResultArr"></param>
        /// <returns></returns>
        public bool m_lngAddNewCheckByOrderID(clsBIHOrder p_objBIHOrder, out DataTable p_dtResultArr)
        {
            p_dtResultArr = new DataTable();
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngAddNewCheckByOrderID(p_objBIHOrder, out p_dtResultArr);
            if (ret > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// ���������뵥������ֵ
        /// </summary>
        private bool m_mthSendTestApplyBill(DataTable p_dtResultArr, string strTypeID, clsBIHOrder p_objBIHOrder, out clsLisApplMainVO objLMVO, out clsTestApplyItme_VO[] itemArr_VO)
        {

            long ret = (new weCare.Proxy.ProxyIP()).Service.m_mthSendTestApplyBill(p_dtResultArr, strTypeID, p_objBIHOrder, out objLMVO, out itemArr_VO);
            if (ret > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ���ݵ�ǰ������ĿID�ж��Ƿ��ǿ��Զ����͵ļ�����Ŀ
        /// </summary>
        /// <param name="m_strOrderDicID"></param>
        /// <param name="m_blHave"></param>
        /// <returns></returns>
        public long m_mthGegCheckByID(string m_strOrderDicID, out bool m_blHave)
        {
            long ret = (new weCare.Proxy.ProxyIP()).Service.m_mthGegCheckByID(m_strOrderDicID, out m_blHave);
            return ret;
        }
        #endregion

        #region GetDrugInfo
        /// <summary>
        /// GetDrugInfo
        /// </summary>
        internal void GetDrugInfo(string orderDicId)
        {
            if (string.IsNullOrEmpty(DrugServiceUrl))
            {
                DrugServiceUrl = com.digitalwave.iCare.gui.HIS.clsPublic.m_strGetSysparm("0080");
                IsUseMedItf = (Convert.ToDecimal(com.digitalwave.iCare.gui.HIS.clsPublic.m_strGetSysparm("0082")) == 1 ? true : false);
            }
            if (IsUseMedItf == false) return;
            //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            Dictionary<string, clsMedicine_VO> dicMed = (new weCare.Proxy.ProxyIP()).Service.GetMedInfoByOrderDicId(orderDicId);
            //svc = null;
            using (Hisitf.RationalDrugUseItf itf = new Hisitf.RationalDrugUseItf())
            {
                if (dicMed.ContainsKey(orderDicId))
                {
                    itf.GetMedInfo(DrugServiceUrl, dicMed[orderDicId].m_strMedicineID);
                }
            }
        }
        #endregion

        internal bool m_lngGetBihOrderNameControl()
        {
            long lngRes = 0;
            bool m_blOpen;
            lngRes = m_objInputOrder.m_lngGetBihOrderNameControl(out m_blOpen);
            return m_blOpen;
        }

        internal bool m_lngGetm_cmdBlankOutControl()
        {
            long lngRes = 0;
            bool m_blOpen;
            lngRes = m_objInputOrder.m_lngGetm_cmdBlankOutControl(out m_blOpen);
            return m_blOpen;
        }

        public void m_Loadm_lngGetcboOrderList()
        {
            this.m_frmInput.m_cboOrderList.Items.AddRange(this.m_arrOrderList);
            this.m_frmInput.m_cboOrderList.SelectedIndex = 0;
            this.m_frmInput.seachClass.SelectedIndex = 0;
        }

        /// <summary>
        /// ���ϻָ�
        /// </summary>
        internal void BihOrderDrawBack(string m_strOrderID)
        {
            (new weCare.Proxy.ProxyIP()).Service.m_lngBihOrderDrawBack(m_strOrderID);
        }

        /// <summary>
        /// ҽ��¼����濪��
        /// </summary>
        /// <param name="m_intBihNameOpen">���ܿ������������Ƿ������޸�ҽ��������Ŀ���� 1017</param>
        /// <param name="m_intBihBlankOutOpen">���ܿ������������Ƿ������޸�ҽ������</param>
        /// <param name="m_intTypePControl">Ƭ���������㿪��1024</param>
        /// <param name="m_intShowCodexRemarkFrm">ҽ������վ�Ƿ���ʾҩ�䱸ע 0 ����ʾ 1 ��ʾ 0059</param>
        /// <param name="ShowCodexRemarkFrmTimerinterval">ҽ������վҩ�䱸ע������ʾʱ�� 0060</param>
        /// <param name="m_intLessMedControl">ȱҩ��ʾ���ƿ���</param>
        /// <param name="m_intDoctorSign">ҽ��ǩ�����ƿ���</param>
        /// <param name="m_intZCaoControl">����ҽ��ת��������̣�0����������1������</param>
        /// <param name="m_intCommitControl">�ύʱ�Ƿ���Ҫ���빤�ż����� 1032</param>
        /// <param name="m_intUpControl">ҽ��¼��Ȩ�޷������޿���1003 0-�أ�1-��</param>
        /// <param name="m_intStopControl">ҽ��¼���Ƿ����¼����ͣ�õ��շ���Ŀ 0-��,1-�� 1037</param>
        /// <param name="m_intDeableMedControl">ҽ��¼���Ƿ����¼��ȱҩ���շ���Ŀ 0-��1-�� 1036</param>
        ///<param name="m_intStopTipControl">סԺҽ��ͣҽ���Ƿ���Ҫ��֤ 0-����Ҫ��1-��Ҫ 1044</param>
        ///<param name="m_intCanChangeOrder">�Ƿ���������ҽ���޸ķǱ��˿���ת��ǰ��ҽ��  0-�����ԣ�1-���� 1045</param>
        ///<param name="m_intSendLisBill"> �ύʱ�Ƿ��ͼ������뵥 0-�����ԣ�1-����  1050</param>
        ///<param name="m_intStopTimeSwitth">ҽ���޸�ͣ��ʱ���ʱ������ 0-������,>0Ϊ���Ƶ�ʱ��,��24,��Ϊ������24Сʱ�ڿ����޸�  1051</param>
        ///<param name="m_intLisDiscount">4008  0-false������ 1-true �������</param>
        ///<param name="m_intLisDiscountNum">4006����Ϊ8��������м��飨��Ʊ����Ϊ���飩�շ���Ŀ>8��ʱ���ô��۹���</param>
        ///<param name="m_decLisDiscountMount">4007�������ô��۹���ʱ�������շ���Ŀ�Ĵ��۱�����80��������</param>
        ///<param name="m_intAutoStopAlert">m_intAutoStopAlert '1053', 'סԺҽ��¼������Ƿ��Զ���ʾ��ǰ���˴���ͣ�û�ȱҩ��δͣҽ��', '0-��1-��', 1 </param>
        ///<param name="m_intStartTimeSwitth">ҽ���޸�����ʱ���ʱ������ 0-������,>0Ϊ���Ƶ�ʱ��,��24,��Ϊ������24Сʱ�ڿ����޸�  1054</param>
        internal void m_lngGetBihOrderControls(out int m_intBihNameOpen, out int m_intBihBlankOutOpen, out int m_intTypePControl, out int m_intShowCodexRemarkFrm, out int ShowCodexRemarkFrmTimerinterval, out int m_intLessMedControl, out int m_intDoctorSign, out int m_intZCaoControl, out int m_intCommitControl, out int m_intUpControl, out int m_intStopControl, out int m_intDeableMedControl, out int m_intStopTipControl, out int m_intCanChangeOrder, out int m_intSendLisBill, out int m_intStopTimeSwitth, out int m_intLisDiscount, out int m_intLisDiscountNum, out decimal m_decLisDiscountMount, out int m_intAutoStopAlert, out int m_intStartTimeSwitth, out int m_intParm1068)
        {

            m_intBihNameOpen = -1;
            m_intBihBlankOutOpen = -1;
            m_intTypePControl = -1;
            m_intShowCodexRemarkFrm = -1;
            ShowCodexRemarkFrmTimerinterval = -1;//����ҽ��ת��������̣�0����������1������
            m_intZCaoControl = -1;
            m_intLessMedControl = -1;
            m_intDoctorSign = -1;
            m_intCommitControl = -1;
            m_intUpControl = -1;
            m_intStopControl = -1;
            m_intDeableMedControl = -1;
            m_intStopTipControl = -1;
            m_intCanChangeOrder = -1;
            m_intSendLisBill = -1;
            m_intStopTimeSwitth = -1;
            m_intLisDiscount = -1;
            m_intLisDiscountNum = -1;
            m_decLisDiscountMount = -1;
            m_intAutoStopAlert = -1;
            m_intStartTimeSwitth = -1;
            m_intParm1068 = 0;
            //ʹ��ArrayList
            //ArrayList m_arrControl = new ArrayList();
            //����List<T>
            System.Collections.Generic.List<string> m_arrControl = new System.Collections.Generic.List<string>();
            m_arrControl.Add("0059");//ҽ������վ�Ƿ���ʾҩ�䱸ע 0 ����ʾ 1 ��ʾ 0059
            m_arrControl.Add("0060");//ҽ������վҩ�䱸ע������ʾʱ�� 0060
            m_arrControl.Add("1003");//ҽ��¼��Ȩ�޷������޿���1003 0-�أ�1-��
            m_arrControl.Add("1017");//���ܿ������������Ƿ������޸�ҽ��������Ŀ���� 1017
            m_arrControl.Add("1023");//���ܿ������������Ƿ������޸�ҽ������ 1023
            m_arrControl.Add("1024");//Ƭ���������㿪��1024
            m_arrControl.Add("1025");//ȱҩ��ʾ���ƿ���
            m_arrControl.Add("1026");//ҽ��ǩ�����ƿ���
            m_arrControl.Add("1027");//����ҽ��ת��������̣�0����������1������
            m_arrControl.Add("1032");//�ύʱ�Ƿ���Ҫ���빤�ż����� 1032
            m_arrControl.Add("1036");//ҽ��¼���Ƿ����¼��ȱҩ���շ���Ŀ 0-��1-�� 1036
            m_arrControl.Add("1037");//ҽ��¼���Ƿ����¼����ͣ�õ��շ���Ŀ 0-��,1-�� 1037
            m_arrControl.Add("1044");//סԺҽ��ͣҽ���Ƿ���Ҫ��֤ 0-����Ҫ��1-��Ҫ 1044
            m_arrControl.Add("1045");//�Ƿ���������ҽ���޸ķǱ��˿���ת��ǰ��ҽ��  0-�����ԣ�1-���� 1045
            m_arrControl.Add("1050");//�ύʱ�Ƿ��ͼ������뵥 0-�����ԣ�1-����  1050
            m_arrControl.Add("1051");//ҽ���޸�ͣ��ʱ���ʱ������ 0-������,>0Ϊ���Ƶ�ʱ��,��24,��Ϊ������24Сʱ�ڿ����޸�  1051
            m_arrControl.Add("4006");//4006����Ϊ8��������м��飨��Ʊ����Ϊ���飩�շ���Ŀ>8��ʱ���ô��۹���
            m_arrControl.Add("4007");//4007�������ô��۹���ʱ�������շ���Ŀ�Ĵ��۱�����80��������
            m_arrControl.Add("4008");//�������4008  0-false������ 1-true �������
            m_arrControl.Add("1053");//'1053', 'סԺҽ��¼������Ƿ��Զ���ʾ��ǰ���˴���ͣ�û�ȱҩ��δͣҽ��', '0-��1-��', 1
            m_arrControl.Add("1054");//ҽ���޸�����ʱ���ʱ������ 0-������,>0Ϊ���Ƶ�ʱ��,��24,��Ϊ������24Сʱ�ڿ����޸�  1054
            m_arrControl.Add("1068");//��������δ�ᴦ������ʱ���Ƴ�Ժ������ҽ��¼��(ҽ��¼��1��2״̬��Ϊ��ʾѡ��)''0-�ر�;1-��ʾѡ��2-��ס'
            DataTable dtbResult = null;
            long lngRes = m_objInputOrder.GetTheHisControl(m_arrControl, out dtbResult);
            if (lngRes > 0 && dtbResult.Rows.Count > 0)
            {
                for (int i = 0; i < dtbResult.Rows.Count; i++)
                {
                    switch (dtbResult.Rows[i]["setid_chr"].ToString().TrimEnd())
                    {
                        case "1003":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("0"))
                            {
                                m_intUpControl = 0;
                            }
                            else
                            {
                                m_intUpControl = 1;
                            }
                            break;
                        case "1017":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("0"))
                            {
                                m_intBihNameOpen = 0;
                            }
                            else
                            {
                                m_intBihNameOpen = 1;
                            }
                            break;
                        case "1023":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("0"))
                            {
                                m_intBihBlankOutOpen = 0;
                            }
                            else
                            {
                                m_intBihBlankOutOpen = 1;
                            }
                            break;
                        case "1024":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("0"))
                            {
                                m_intTypePControl = 0;
                            }
                            else
                            {
                                m_intTypePControl = 1;
                            }
                            break;
                        case "1025":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("0"))
                            {
                                m_intLessMedControl = 0;
                            }
                            else
                            {
                                m_intLessMedControl = 1;
                            }
                            break;
                        case "1026":
                            if (!dtbResult.Rows[i]["setstatus_int"].ToString().Equals(""))
                            {
                                m_intDoctorSign = int.Parse(dtbResult.Rows[i]["setstatus_int"].ToString());
                            }
                            break;
                        case "0059":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("0"))
                            {
                                m_intShowCodexRemarkFrm = 0;
                            }
                            else
                            {
                                m_intShowCodexRemarkFrm = 1;
                            }
                            break;
                        case "0060":
                            if (!dtbResult.Rows[i]["setstatus_int"].ToString().Equals(""))
                            {
                                ShowCodexRemarkFrmTimerinterval = int.Parse(dtbResult.Rows[i]["setstatus_int"].ToString());
                            }
                            else
                            {
                                ShowCodexRemarkFrmTimerinterval = 0;
                            }
                            break;
                        case "1027":
                            if (!dtbResult.Rows[i]["setstatus_int"].ToString().Equals("1"))
                            {
                                m_intZCaoControl = 1;
                            }
                            else
                            {
                                m_intZCaoControl = 0;
                            }
                            break;
                        case "1032":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("1"))
                            {
                                m_intCommitControl = 1;
                            }
                            else
                            {
                                m_intCommitControl = 0;
                            }
                            break;
                        case "1036":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("1"))
                            {
                                m_intDeableMedControl = 1;
                            }
                            else
                            {
                                m_intDeableMedControl = 0;
                            }
                            break;
                        case "1037":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("1"))
                            {
                                m_intStopControl = 1;
                            }
                            else
                            {
                                m_intStopControl = 0;
                            }
                            break;
                        case "1044":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("1"))
                            {
                                m_intStopTipControl = 1;
                            }
                            else
                            {
                                m_intStopTipControl = 0;
                            }
                            break;
                        case "1045":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("1"))
                            {
                                m_intCanChangeOrder = 1;
                            }
                            else
                            {
                                m_intCanChangeOrder = 0;
                            }
                            break;
                        case "1050":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("1"))
                            {
                                m_intSendLisBill = 1;
                            }
                            else
                            {
                                m_intSendLisBill = 0;
                            }
                            break;
                        case "1051":
                            int.TryParse(dtbResult.Rows[i]["setstatus_int"].ToString(), out m_intStopTimeSwitth);
                            break;
                        case "4006":
                            int.TryParse(dtbResult.Rows[i]["setstatus_int"].ToString().Trim(), out m_intLisDiscountNum);
                            break;
                        case "4007":
                            decimal.TryParse(dtbResult.Rows[i]["setstatus_int"].ToString().Trim(), out m_decLisDiscountMount);
                            break;
                        case "4008":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("1"))
                            {
                                m_intLisDiscount = 1;
                            }
                            else
                            {
                                m_intLisDiscount = 0;
                            }
                            break;
                        case "1053":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("1"))
                            {
                                m_intAutoStopAlert = 1;
                            }
                            else
                            {
                                m_intAutoStopAlert = 0;
                            }
                            break;
                        case "1054":
                            int.TryParse(dtbResult.Rows[i]["setstatus_int"].ToString(), out m_intStartTimeSwitth);
                            break;
                        case "1068":
                            int.TryParse(dtbResult.Rows[i]["setstatus_int"].ToString(), out m_intParm1068);
                            break;
                    }
                }
            }
        }

        #region ҽ��ҽ��ǩ��

        internal long m_mthCurrentOrderDoctorSign(clsBIHOrder clsBIHOrder, string empid_chr, string lastname_vchr)
        {

            long ret = (new weCare.Proxy.ProxyIP()).Service.m_lngCurrentOrderDoctorSign(clsBIHOrder.m_strOrderID);
            return ret;
        }

        #endregion

        internal long m_lngGetNotExecuteOrderByRegID(string m_strRegisterID, out int m_intCount)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetNotExecuteOrderByRegID(m_strRegisterID, out m_intCount);
            return lngRes;
        }

        internal long m_lngGetNotStopOrderByRegID(string m_strRegisterID, out int m_intCount)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetNotStopOrderByRegID(m_strRegisterID, out m_intCount);
            return lngRes;
        }

        internal long m_lngDeleteOrder(string[] p_strDeleteOrderIDArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngDeleteOrder(p_strDeleteOrderIDArr);
            return lngRes;
        }

        internal long m_lngDeleteOrder(string[] p_strDeleteOrderIDArr, string[] p_strDeleteContinueIDArr)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngDeleteOrder(p_strDeleteOrderIDArr, p_strDeleteContinueIDArr);
            return lngRes;
        }


        internal long m_lngBlankOutOrder(string[] p_strBlankOutOrderIDArr, string DELETERID_CHR, string DELETERNAME_VCHR)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngBlankOutOrder(p_strBlankOutOrderIDArr, DELETERID_CHR, DELETERNAME_VCHR);
            return lngRes;
        }

        internal long m_lngStopOrder(string[] p_strBlankOutOrderIDArr, string strDoctorID, string strDoctorName)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngStopOrder(p_strBlankOutOrderIDArr, strDoctorID, strDoctorName);
            return lngRes;
        }

        /// <summary>
        /// ��������ҽ��
        /// </summary>
        /// <param name="order"></param>
        /// <param name="p_strHandlersID"></param>
        /// <param name="p_strHandlers"></param>
        /// <returns></returns>
        internal long m_lngInsertOPERATIONOrder(clsBIHOrder order, string p_strHandlersID, string p_strHandlers)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngInsertOPERATIONOrder(order, p_strHandlersID, p_strHandlers);
            return lngRes;
        }

        /// <summary>
        /// ����ת��ҽ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        internal long m_lngInsertChangeAreaOrder(clsBIHOrder order, string p_strHandlersID, string p_strHandlers)
        {
            long lngRes = 0;
            string p_strRecordID = "";
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngInsertChangeAreaOrder(order, p_strHandlersID, p_strHandlers);
            return lngRes;
        }

        /// <summary>
        /// �����Ժҽ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        internal long m_lngInsertOutHisOrder(clsBIHOrder order, string p_strHandlersID, string p_strHandlers, bool m_blAuto)
        {
            long lngRes = 0;
            string p_strRecordID = "";
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngInsertOutHisOrder(order, p_strHandlersID, p_strHandlers, m_blAuto);
            return lngRes;
        }

        internal long m_cmdAddOrderNornal(clsComuseorderdic[] m_arrOrderdic)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngAddOrderNornal(m_arrOrderdic);
            return lngRes;
        }

        internal long m_lngReSortOrderNO(string m_strRegisterid)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngReSortOrderNO(m_strRegisterid);
            return lngRes;
        }

        /// <summary>
        /// �����������ñ�
        /// </summary>
        internal void m_LoadGetSPECORDERCATE()
        {
            long lngRes = 0;
            lngRes = m_objInputOrder.m_lngAddGetSPECORDERCATE(out this.m_frmInput.m_objSpecateVo);
        }

        /// <summary>
        /// �鿴�Ƿ�ǰ�������Ƿ��д���Ȩ
        /// </summary>
        internal long m_lngAddGetAccessPower(string m_strEmpID, out bool m_blAccess)
        {
            long lngRes = 0;
            lngRes = m_objInputOrder.m_lngAddGetAccessPower(m_strEmpID, out m_blAccess);
            return lngRes;
        }

        internal long m_lngMoneyCountNewOrder(string m_strRegisterID, out decimal m_decMoneySum)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngMoneyCountNewOrder(m_strRegisterID, out m_decMoneySum);
            return lngRes;
        }

        internal long LoadThePARMVALUE(List<string> PARMCODE_CHR, out DataTable m_dtPARMVALUE_VCHR)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.LoadThePARMVALUE(PARMCODE_CHR, out m_dtPARMVALUE_VCHR);
            return lngRes;
        }

        /// <summary>
        /// ҽ������վ��ҩƷ���ļ��
        /// </summary>
        /// <param name="m_strMedStordID"></param>
        /// <param name="m_strItemID"></param>
        /// <param name="m_dclGetMed"></param>
        /// <param name="blnFlag">�Ƿ񹻿��</param>
        /// <param name="m_strExecDeptID">���ؿ���ID</param>
        /// <returns></returns>

        internal long m_lngGetMedStoreByDoctorWorkStation(string m_strMedStordID, string m_strItemID, decimal m_dclGetMed,
                                                        out bool blnFlag, out string m_strExecDeptID, out string medCode)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderService));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetMedStoreByDoctorWorkStation(m_strMedStordID, m_strItemID, m_dclGetMed, out blnFlag, out m_strExecDeptID, out medCode);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }

        #region �Ƿ���Ӧ֢ҩƷ
        /// <summary>
        /// �Ƿ���Ӧ֢ҩƷ
        /// </summary>
        /// <param name="strOrderID"></param>
        /// <returns></returns>
        internal bool m_blnShiying(string strOrderID, out string strRemark, out string strItemName)
        {
            bool blnRes = false;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            blnRes = (new weCare.Proxy.ProxyIP()).Service.m_blnShiying(strOrderID, out strRemark, out strItemName);
            //objSvc.Dispose();
            //objSvc = null;
            return blnRes;
        }
        #endregion

        #region  ����ҽ����Ӧ֢
        /// <summary>
        /// ����ҽ����Ӧ֢
        /// </summary>
        /// <param name="strOrderID"></param>
        /// <param name="strItemID"></param>
        /// <param name="strShiying"></param>
        /// <returns></returns>
        public long m_lngGetShiying(string strOrderID, string strItemID, out string strShiying)
        {
            long lngRes = 0;
            //com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface objSvc = (com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.BIHOrderServer.clsBIHOrderInterface));
            lngRes = (new weCare.Proxy.ProxyIP()).Service.m_lngGetShiying(strOrderID, strItemID, out strShiying);
            //objSvc.Dispose();
            //objSvc = null;
            return lngRes;
        }
        #endregion
    }
    #endregion

    #region ��-������Ŀ����clsOrderCate
    /// <summary>
    /// ������Ŀ������	����������
    /// </summary>
    public class clsOrderCate
    {
        public clsT_aid_bih_ordercate_VO m_objOrderCate = new clsT_aid_bih_ordercate_VO();
        public clsOrderCate()
        { }
        public override string ToString()
        {
            if (m_objOrderCate != null && m_objOrderCate.m_strVIEWNAME_VCHR != null)
                return m_objOrderCate.m_strVIEWNAME_VCHR;
            else
                return "";
        }
    }
    #endregion

}
